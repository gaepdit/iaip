Imports System.Collections.Generic
Imports Iaip.SharedData

Public Class DmuEdtErrorMessageDetail

#Region " Properties and variables "

    Private _edtErrorCode As String
    Public Property EdtErrorCode() As String
        Get
            Return _edtErrorCode
        End Get
        Set(ByVal value As String)
            If value = _edtErrorCode Then Return
            _edtErrorCode = value
            Init()
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
    Private edtErrorMessageDetails As Dmu.EdtErrorMessage
    Private statusOfSelectedRows As SelectedRowsState
    Private headerSuccess As Boolean
    Private activeUsersList As List(Of KeyValuePair(Of Integer, String))
    Private totalCount As Integer = 0
    Private shownCount As Integer = 0

#End Region

#Region " Load "

    Private Sub DmuEdtErrorMessageDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddDisplayOptionHandlers()
        PrepUserComboBoxes()
    End Sub

    Private Sub PrepUserComboBoxes()
        activeUsersList = GetSharedData(SharedKeyValueList.ActiveUsers)
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

    Private Sub Init()
        ErrorCodeDisplay.Text = EdtErrorCode
        Me.Text = "EDT Error Code " & EdtErrorCode

        headerSuccess = GetHeaderData()
        If headerSuccess Then
            GetTableData()
        End If
    End Sub

#End Region

#Region " Get Data "

    Private Sub ReloadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReloadButton.Click
        If headerSuccess Then
            GetTableData()
        End If
    End Sub

    Private Function GetHeaderData() As Boolean
        edtErrorMessageDetails = DAL.Dmu.GetErrorMessageDetail(EdtErrorCode)

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
        statusOfSelectedRows = SelectedRowsState.NoneSelected

        edtErrorMessagesTable = DAL.Dmu.GetErrors(EdtErrorCode)
        Dim keys(1) As DataColumn
        keys(0) = edtErrorMessagesTable.Columns("ERRORID")
        edtErrorMessagesTable.PrimaryKey = keys

        If edtErrorMessagesTable IsNot Nothing Then
            edtErrorMessagesBindingSource = New BindingSource
            edtErrorMessagesBindingSource.DataSource = edtErrorMessagesTable
            EdtErrorMessageGrid.DataSource = edtErrorMessagesBindingSource

            FormatGrid()
            SetGridFilter()

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
    End Function

#End Region

#Region " Grid Display "

    Private Sub FormatGrid()
        With EdtErrorMessageGrid
            With .Columns("ERRORID")
                .HeaderText = "Error ID"
                .DisplayIndex = 0
            End With
            With .Columns("RESOLVED")
                .HeaderText = "Resolved"
                .DefaultCellStyle.FormatProvider = New BooleanFormatProvider
                .DefaultCellStyle.Format = "YesNo"
                .DisplayIndex = 1
            End With
            With .Columns("AssignedToUserName")
                .HeaderText = "Assigned to"
                .DisplayIndex = 2
            End With
            With .Columns("SUBMITDATE")
                .HeaderText = "Date submitted"
                .DisplayIndex = 3
            End With
            With .Columns("STATUSDETAIL")
                .HeaderText = "Error Message"
                .DisplayIndex = 4
            End With
            With .Columns("EDTID")
                .HeaderText = "EPA record ID"
                .DisplayIndex = 5
            End With
            .Columns("ASSIGNEDTOUSER").Visible = False
            With .Columns("RESOLVEDDATE")
                .HeaderText = "Date resolved"
                .DisplayIndex = 6
            End With
            .Columns("ResolvedByUserID").Visible = False
            With .Columns("ResolvedByUserName")
                .HeaderText = "Resolved by"
                .DisplayIndex = 7
            End With
            With .Columns("IAIPID")
                .HeaderText = "IAIP ID"
                .DisplayIndex = 6
            End With
            .Columns("IDCATEGORY").Visible = False

            .MakeColumnsLookLikeLinks(0)
            .SanelyResizeColumns()
        End With

    End Sub

    Private Sub EdtErrorMessageGrid_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) _
    Handles EdtErrorMessageGrid.CellFormatting

        If TypeOf e.CellStyle.FormatProvider Is ICustomFormatter Then
            e.Value = TryCast(e.CellStyle.FormatProvider.GetFormat(GetType(ICustomFormatter)), ICustomFormatter).Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider)
            e.FormattingApplied = True
        End If

    End Sub

    Private Sub SetGridFilter()
        edtErrorMessagesBindingSource.RemoveFilter()
        totalCount = edtErrorMessagesBindingSource.Count

        If DisplayOwnerMine.Checked And DisplayResolutionOpen.Checked Then
            edtErrorMessagesBindingSource.Filter = "AssignedToUser = " & CurrentUser.UserID & " and Resolved = False"
        ElseIf DisplayOwnerMine.Checked And DisplayResolutionAll.Checked Then
            edtErrorMessagesBindingSource.Filter = "AssignedToUser = " & CurrentUser.UserID
        ElseIf DisplayOwnerEveryone.Checked And DisplayResolutionOpen.Checked Then
            edtErrorMessagesBindingSource.Filter = "Resolved = False"
        ElseIf DisplayOwnerEveryone.Checked And DisplayResolutionAll.Checked Then
            edtErrorMessagesBindingSource.RemoveFilter()
        End If

        SetCountDisplay()
    End Sub

    Private Sub SetCountDisplay()
        shownCount = edtErrorMessagesBindingSource.Count
        EdtErrorCountDisplay.Text = shownCount.ToString & " error" & If(shownCount = 1, "", "s") & " shown / " & totalCount.ToString & " total"
    End Sub

    Private Sub DisplayOptionsChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetGridFilter()
    End Sub

    Private Sub AddDisplayOptionHandlers()
        AddHandler DisplayOwnerMine.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayOwnerEveryone.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayResolutionOpen.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayResolutionAll.CheckedChanged, AddressOf DisplayOptionsChanged
    End Sub

#End Region

#Region " Grid Linkiness "

    Private Sub OpenEdtErrorDetail(ByVal errorID As Integer)
        Dim edtErrorDetail As DmuEdtErrorDetail = OpenMultiForm(DmuEdtErrorDetail, errorID)
        edtErrorDetail.ActiveUsersList = activeUsersList
        edtErrorDetail.EdtErrorID = errorID
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
                .Visible = True
                .Text = "Reopen Selected"
            End With
        ElseIf statusOfSelectedRows = SelectedRowsState.AllOpen Then
            With ChangeStatusForSelectedRows
                .Enabled = True
                .Visible = True
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
        If DAL.Dmu.SetDefaultUser(EdtErrorCode, UserAsDefault.SelectedValue) Then
            MessageBox.Show("Default user set.", "Success", MessageBoxButtons.OK)
        Else
            MessageBox.Show("There was an error setting the default user.", "Error", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub AssignSelectedToUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssignSelectedToUser.Click
        Dim idArray As Integer() = GetSelectedIDs()
        Dim result As Boolean = False

        If idArray IsNot Nothing Then
            result = DAL.Dmu.AssignErrorToUser(UserToAssign.SelectedValue, idArray)
        End If

        If result = True Then
            AssignUserInGrid()
        Else
            MessageBox.Show("There was an error assigning a user to the selected items.", "Error", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub AssignUserInGrid()
        If (EdtErrorMessageGrid.SelectedRows.Count = 0) Then Exit Sub

        For Each selectedRow As DataGridViewRow In EdtErrorMessageGrid.SelectedRows
            Dim tableRow As DataRow = edtErrorMessagesTable.Rows().Find(selectedRow.Cells("ERRORID").Value)
            With tableRow
                .Item("ASSIGNEDTOUSER") = UserToAssign.SelectedValue
                .Item("AssignedToUserName") = UserToAssign.Text
            End With
        Next

        SetCountDisplay()
    End Sub

    Private Sub ChangeStatusForSelectedRows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeStatusForSelectedRows.Click
        If statusOfSelectedRows = SelectedRowsState.AllOpen Or statusOfSelectedRows = SelectedRowsState.AllResolved Then
            Dim idArray As Integer() = GetSelectedIDs()
            Dim result As Boolean = False

            If idArray IsNot Nothing Then
                If statusOfSelectedRows = SelectedRowsState.AllOpen Then
                    result = DAL.Dmu.SetResolvedStatus(True, idArray)
                ElseIf statusOfSelectedRows = SelectedRowsState.AllResolved Then
                    result = DAL.Dmu.SetResolvedStatus(False, idArray)
                End If
            End If

            If result = True Then
                ChangeResolutionStatusInGrid()
            Else
                MessageBox.Show("There was an error changing the status for the selected items.", "Error", MessageBoxButtons.OK)
            End If
        End If
    End Sub

    Private Sub ChangeResolutionStatusInGrid()
        If (EdtErrorMessageGrid.SelectedRows.Count = 0) Then Exit Sub

        If statusOfSelectedRows = SelectedRowsState.AllOpen Then

            For Each selectedRow As DataGridViewRow In EdtErrorMessageGrid.SelectedRows
                Dim tableRow As DataRow = edtErrorMessagesTable.Rows().Find(selectedRow.Cells("ERRORID").Value)
                With tableRow
                    .Item("Resolved") = True
                    .Item("ResolvedDate") = Now
                    .Item("ResolvedByUserID") = CurrentUser.UserID
                    .Item("ResolvedByUserName") = CurrentUser.AlphaName
                End With
            Next

        ElseIf statusOfSelectedRows = SelectedRowsState.AllResolved Then

            For Each selectedRow As DataGridViewRow In EdtErrorMessageGrid.SelectedRows
                Dim tableRow As DataRow = edtErrorMessagesTable.Rows().Find(selectedRow.Cells("ERRORID").Value)
                With tableRow
                    .Item("Resolved") = False
                    .Item("ResolvedDate") = DBNull.Value
                    .Item("ResolvedByUserID") = Nothing
                    .Item("ResolvedByUserName") = Nothing
                End With
            Next

        End If

        SetCountDisplay()
        SetUpResolveOrReopenButton()
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