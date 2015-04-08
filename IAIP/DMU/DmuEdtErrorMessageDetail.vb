﻿Imports System.Collections.Generic

Public Class DmuEdtErrorMessageDetail

#Region " Properties and variables "

    Private _edtErrorCode As String
    Public Property EdtErrorCode() As String
        Get
            Return _edtErrorCode
        End Get
        Set(ByVal value As String)
            _edtErrorCode = value
        End Set
    End Property

    Private Enum SelectedRowsState
        NoneSelected
        AllOpen
        AllResolved
        Mixed
    End Enum

    Private edtErrorMessagesTable As DataTable
    Private edtErrorMessagesBindingSource As BindingSource
    Private edtErrorMessageDetails As DMU.EdtErrorMessage
    Private statusOfSelectedRows As SelectedRowsState
    Private headerSuccess As Boolean
    Private activeUsersList As List(Of KeyValuePair(Of Integer, String))

#End Region

#Region " Load "

    Private Sub DmuEdtErrorMessageDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddDisplayOptionHandlers()
        PrepUserComboBoxes()
    End Sub

    Private Sub PrepUserComboBoxes()
        activeUsersList = DAL.GetActiveUsers
        activeUsersList.Insert(0, New KeyValuePair(Of Integer, String)(0, "Unassigned"))
        UserAsDefault.BindToKeyValuePairs(activeUsersList)
        UserToAssign.BindToKeyValuePairs(activeUsersList)
    End Sub

#End Region

#Region " Form resize "

    Private Sub DmuEdtErrorMessageDetail_ResizeEnd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        ErrorMessageDisplay.MaximumSize = New Size(ErrorMessageDisplayContainer.Size.Width - 30, 0)
        BusinessRuleDisplay.MaximumSize = New Size(BusinessRuleDisplayContainer.Size.Width - 30, 0)
    End Sub

#End Region

#Region " Init "

    ''' <summary>
    ''' Fetches information about the EDT Error Messsage code "ErrorCode" and conditionally fetches a list of all 
    ''' related error records
    ''' </summary>
    ''' <remarks>Should only be run once after ErrorCode is set</remarks>
    Public Sub Init()
        ErrorCodeDisplay.Text = EdtErrorCode
        Me.Text = "EDT Error Code " & EdtErrorCode

        headerSuccess = GetHeaderData()
        If headerSuccess Then
            GetTableData()
        End If
    End Sub

#End Region

#Region " Get Data "

    Private counter As Integer = 1
    Private Sub ReloadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReloadButton.Click
        'If headerSuccess Then
        '    GetTableData()
        'End If
        OpenEdtErrorDetail(counter) ' Testing purposes only
        counter += 1
    End Sub

    Private Function GetHeaderData() As Boolean
        edtErrorMessageDetails = DAL.DMU.GetErrorMessageDetail(EdtErrorCode)

        If edtErrorMessageDetails IsNot Nothing Then
            ErrorMessageDisplay.Text = edtErrorMessageDetails.ErrorCategory & vbNewLine & vbNewLine & edtErrorMessageDetails.ErrorMessage
            If edtErrorMessageDetails.BusinessRuleMessage Is Nothing Then
                BusinessRuleDisplay.Text = "N/A"
            Else
                BusinessRuleDisplay.Text = edtErrorMessageDetails.BusinessRuleMessage
            End If
            UserAsDefault.Enabled = True
            UserAsDefault.SelectedValue = edtErrorMessageDetails.DefaultUserID
            ReloadButton.Enabled = True
            Return True
        Else
            ErrorMessageDisplay.Text = "No information available"
            BusinessRuleDisplay.Text = ""
            UserAsDefault.Enabled = False
            UserAsDefault.SelectedValue = 0
            ReloadButton.Enabled = False
            Return False
        End If
    End Function

    Private Function GetTableData() As Boolean
        edtErrorMessagesTable = DAL.DMU.GetErrors(EdtErrorCode)

        If edtErrorMessagesTable IsNot Nothing Then
            edtErrorMessagesBindingSource = New BindingSource
            edtErrorMessagesBindingSource.DataSource = edtErrorMessagesTable
            EdtErrorMessageGrid.DataSource = edtErrorMessagesBindingSource

            FormatGrid()
            SetGridFilter()
            Dim shown As Integer = edtErrorMessagesBindingSource.Count
            EdtErrorCountDisplay.Text = shown.ToString & " errors" & If(shown = 1, "", "s") & " shown"

            OwnerGroupPanel.Enabled = True
            ResolvedStatusGroupPanel.Enabled = True
            EdtErrorMessageGrid.ClearSelection()
            Return True
        Else
            edtErrorMessagesBindingSource = Nothing
            EdtErrorMessageGrid.DataSource = Nothing

            OwnerGroupPanel.Enabled = False
            ResolvedStatusGroupPanel.Enabled = False
            EdtErrorCountDisplay.Text = "No errors to display"
            Return False
        End If

        statusOfSelectedRows = SelectedRowsState.NoneSelected
    End Function

#End Region

#Region " Grid Display "

    Private Sub FormatGrid()
        ' Stuff about column headers and visibility goes here...
        '
        '

        With EdtErrorMessageGrid
            .MakeColumnsLookLikeLinks(0)
            .SanelyResizeColumns()
        End With
    End Sub

    Private Sub SetGridFilter()
        If DisplayMine.Checked And DisplayOpen.Checked Then
            edtErrorMessagesBindingSource.Filter = "AssignedToUser = " & CurrentUser.UserID & " and Resolved = False"
        ElseIf DisplayMine.Checked And DisplayAll.Checked Then
            edtErrorMessagesBindingSource.Filter = "AssignedToUser = " & CurrentUser.UserID
        ElseIf DisplayEveryone.Checked And DisplayOpen.Checked Then
            edtErrorMessagesBindingSource.Filter = "Resolved = False"
        ElseIf DisplayEveryone.Checked And DisplayAll.Checked Then
            edtErrorMessagesBindingSource.RemoveFilter()
        End If
    End Sub

    Private Sub DisplayOptionsChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetGridFilter()
    End Sub

    Private Sub AddDisplayOptionHandlers()
        AddHandler DisplayMine.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayEveryone.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayOpen.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayAll.CheckedChanged, AddressOf DisplayOptionsChanged
    End Sub

#End Region

#Region " Grid Linkiness "

    Private Sub OpenEdtErrorDetail(ByVal errorID As Integer)
        Dim edtErrorDetail As DmuEdtErrorDetail = OpenMultiForm(DmuEdtErrorDetail, errorID)
        edtErrorDetail.EdtErrorID = errorID
        edtErrorDetail.ActiveUsersList = activeUsersList
        edtErrorDetail.Init()
    End Sub

    Private Sub EdtErrorMessageGrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EdtErrorMessageGrid.CellClick
        ' Only within the cell content of first column
        If e.RowIndex <> -1 And e.RowIndex < EdtErrorMessageGrid.RowCount And e.ColumnIndex = 0 Then
            OpenEdtErrorDetail(EdtErrorMessageGrid.Rows(e.RowIndex).Cells(0).Value)
        End If
    End Sub

    Private Sub EdtErrorMessageGrid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EdtErrorMessageGrid.CellDoubleClick
        'Double-click within the cell content (but exclude first column to avoid double-firing)
        If e.RowIndex <> -1 And e.RowIndex < EdtErrorMessageGrid.RowCount And e.ColumnIndex <> 0 Then
            OpenEdtErrorDetail(EdtErrorMessageGrid.Rows(e.RowIndex).Cells(0).Value)
        End If
    End Sub

    Private Sub EdtErrorMessageGrid_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EdtErrorMessageGrid.CellMouseEnter
        ' Change cursor and text color when hovering over first column (treats text like a hyperlink)
        If e.RowIndex <> -1 And e.RowIndex < EdtErrorMessageGrid.RowCount And e.ColumnIndex = 0 Then
            EdtErrorMessageGrid.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, True)
        End If
    End Sub

    Private Sub EdtErrorMessageGrid_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EdtErrorMessageGrid.CellMouseLeave
        ' Reset cursor and text color when mouse leaves (un-hovers) a cell
        If e.RowIndex <> -1 And e.RowIndex < EdtErrorMessageGrid.RowCount And e.ColumnIndex = 0 Then
            EdtErrorMessageGrid.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, False)
        End If
    End Sub

    Private Sub EdtErrorMessageGrid_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles EdtErrorMessageGrid.KeyUp
        If e.KeyCode = Keys.Enter Then
            OpenEdtErrorDetail(EdtErrorMessageGrid.CurrentRow.Cells(0).Value)
        End If
    End Sub

    Private Sub OpenEdtError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenEdtError.Click
        If EdtErrorMessageGrid.RowCount > 0 AndAlso EdtErrorMessageGrid.SelectedRows.Count = 1 Then
            OpenEdtErrorDetail(EdtErrorMessageGrid.SelectedRows(0).Cells(0).Value)
        End If
    End Sub

#End Region

#Region " Grid Selection "

    Private Sub EdtErrorMessageGrid_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EdtErrorMessageGrid.SelectionChanged
        OpenEdtError.Enabled = (EdtErrorMessageGrid.SelectedRows.Count = 1)
        GridSelectionActionPanel.Enabled = (EdtErrorMessageGrid.SelectedRows.Count > 0)
        DetermineSelectedRowStatuses()
        SetUpResolveOrReopenButton()
    End Sub

    Private Sub DetermineSelectedRowStatuses()
        If (EdtErrorMessageGrid.SelectedRows.Count > 0) Then
            Dim allOpen As Boolean = True
            Dim allResolved As Boolean = True

            For Each row As DataGridViewRow In EdtErrorMessageGrid.SelectedRows
                If row.Cells("Resolved").Value = True Then
                    allOpen = False
                Else
                    allResolved = False
                End If
            Next

            If allResolved Then
                statusOfSelectedRows = SelectedRowsState.AllResolved
            ElseIf allOpen Then
                statusOfSelectedRows = SelectedRowsState.AllOpen
            Else
                statusOfSelectedRows = SelectedRowsState.Mixed
            End If
        Else
            statusOfSelectedRows = SelectedRowsState.NoneSelected
        End If
    End Sub

    Private Sub SetUpResolveOrReopenButton()
        If statusOfSelectedRows = SelectedRowsState.AllResolved Then
            With ChangeStatusForSelectedRows
                .Enabled = True
                .Text = "Reopen Selected"
            End With
        ElseIf statusOfSelectedRows = SelectedRowsState.AllOpen Then
            With ChangeStatusForSelectedRows
                .Enabled = True
                .Text = "Resolve Selected"
            End With
        Else
            With ChangeStatusForSelectedRows
                .Enabled = False
                .Visible = False
            End With
        End If
    End Sub

#End Region

#Region " Update data "

    Private Sub AssignDefaultUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssignDefaultUser.Click
        DAL.DMU.SetDefaultUser(EdtErrorCode, UserAsDefault.SelectedValue)
    End Sub

    Private Sub AssignSelectedToUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssignSelectedToUser.Click
        Dim idArray As Integer() = GetSelectedIDs()
        If idArray IsNot Nothing Then
            DAL.DMU.AssignErrorToUser(UserToAssign.SelectedValue, idArray)
        End If
    End Sub

    Private Sub ChangeStatusForSelectedRows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeStatusForSelectedRows.Click
        If statusOfSelectedRows = SelectedRowsState.AllOpen Or statusOfSelectedRows = SelectedRowsState.AllResolved Then
            Dim idArray As Integer() = GetSelectedIDs()

            If idArray IsNot Nothing Then
                If statusOfSelectedRows = SelectedRowsState.AllOpen Then
                    DAL.DMU.SetResolvedStatus(True, idArray)
                ElseIf statusOfSelectedRows = SelectedRowsState.AllResolved Then
                    DAL.DMU.SetResolvedStatus(False, idArray)
                End If
            End If

        End If
    End Sub

    Private Function GetSelectedIDs() As Integer()
        If (EdtErrorMessageGrid.SelectedRows.Count > 0) Then
            Dim idList As New List(Of Integer)
            For Each row As DataGridViewRow In EdtErrorMessageGrid.SelectedRows
                idList.Add(row.Cells("ErrorID").Value)
            Next
            Return idList.ToArray
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class