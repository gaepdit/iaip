Imports System.Collections.Generic

Public Class DmuEdtErrorMessages

#Region " Properties and variables "

    Private edtErrorMessagesTable As DataTable
    Private edtErrorMessagesBindingSource As BindingSource

#End Region

#Region " Load "

    Private Sub DmuEdtErrors_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        GetData()
        AddDisplayOptionHandlers()
    End Sub

    Private Sub AddDisplayOptionHandlers()
        AddHandler DisplayMine.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayEveryone.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayOpen.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayAll.CheckedChanged, AddressOf DisplayOptionsChanged
    End Sub

#End Region

#Region " Data "

    Private Sub ReloadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReloadButton.Click
        GetData()
    End Sub

    Private Sub GetData()
        edtErrorMessagesTable = DAL.Dmu.GetErrorCounts(CurrentUser.UserID)

        If edtErrorMessagesTable IsNot Nothing Then
            edtErrorMessagesBindingSource = New BindingSource
            edtErrorMessagesBindingSource.DataSource = edtErrorMessagesTable
            EdtErrorMessageGrid.DataSource = edtErrorMessagesBindingSource

            FormatGrid()
            SetGridFilter()
            OwnerGroupPanel.Enabled = True
            ResolvedStatusGroupPanel.Enabled = True
        Else
            OwnerGroupPanel.Enabled = False
            ResolvedStatusGroupPanel.Enabled = False
            edtErrorMessagesBindingSource = Nothing
            EdtErrorMessageGrid.DataSource = Nothing
            EdtErrorCountDisplay.Text = "No errors to display"
        End If
    End Sub

#End Region

#Region " Display "

    Private Sub FormatGrid()
        With EdtErrorMessageGrid
            With .Columns("ERRORCODE")
                .HeaderText = "Error Code"
                .DisplayIndex = 0
            End With
            With .Columns("DefaultUserName")
                .HeaderText = "Default User"
                .DisplayIndex = 1
            End With
            With .Columns("CountOpen")
                .HeaderText = "Total Open"
                .DisplayIndex = 2
            End With
            With .Columns("CountAll")
                .HeaderText = "Total"
                .DisplayIndex = 3
            End With
            With .Columns("ERRORMESSAGE")
                .HeaderText = "Error Message"
                .DisplayIndex = 4
            End With
            With .Columns("CATEGORY")
                .HeaderText = "Error Category"
                .DisplayIndex = 5
            End With
            .Columns("BUSINESSRULECODE").Visible = False
            .Columns("BUSINESSRULE").Visible = False
            .Columns("CountAllByUser").Visible = False
            .Columns("CountOpenByUser").Visible = False
            .Columns("DefaultUserId").Visible = False

            .MakeColumnsLookLikeLinks(0)
            .SanelyResizeColumns()
        End With
    End Sub

    Private Sub SetGridFilter()
        edtErrorMessagesBindingSource.RemoveFilter()
        Dim total As Integer = edtErrorMessagesBindingSource.Count

        If DisplayMine.Checked And DisplayOpen.Checked Then
            edtErrorMessagesBindingSource.Filter = "DefaultUserID = " & CurrentUser.UserID & " and CountOpenByUser > 0 "
        ElseIf DisplayMine.Checked And DisplayAll.Checked Then
            edtErrorMessagesBindingSource.Filter = "DefaultUserID = " & CurrentUser.UserID
        ElseIf DisplayEveryone.Checked And DisplayOpen.Checked Then
            edtErrorMessagesBindingSource.Filter = "CountOpen > 0 "
        ElseIf DisplayEveryone.Checked And DisplayAll.Checked Then
            edtErrorMessagesBindingSource.RemoveFilter()
        End If

        Dim shown As Integer = edtErrorMessagesBindingSource.Count
        EdtErrorCountDisplay.Text = shown.ToString & " error" & If(shown = 1, "", "s") & " shown / " & total.ToString & " total"
    End Sub

    Private Sub DisplayOptionsChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetGridFilter()
    End Sub

#End Region

#Region " DataGridView Selection and Events "

    Private Sub OpenErrorMessageDetail(ByVal errorCode As String)
        Dim edtErrorMessageDetail As DmuEdtErrorMessageDetail = OpenMultiForm(DmuEdtErrorMessageDetail, errorCode.GetHashCode)
        edtErrorMessageDetail.EdtErrorCode = errorCode
    End Sub

    Private Sub EdtErrorMessageGrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EdtErrorMessageGrid.CellClick
        ' Only within the cell content of first column
        If e.RowIndex <> -1 And e.RowIndex < EdtErrorMessageGrid.RowCount And e.ColumnIndex = 0 Then
            OpenErrorMessageDetail(EdtErrorMessageGrid.Rows(e.RowIndex).Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub EdtErrorMessageGrid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EdtErrorMessageGrid.CellDoubleClick
        'Double-click within the cell content (but exclude first column to avoid double-firing)
        If e.RowIndex <> -1 And e.RowIndex < EdtErrorMessageGrid.RowCount And e.ColumnIndex <> 0 Then
            OpenErrorMessageDetail(EdtErrorMessageGrid.Rows(e.RowIndex).Cells(0).Value.ToString)
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
            OpenErrorMessageDetail(EdtErrorMessageGrid.CurrentRow.Cells(0).Value.ToString)
        End If
    End Sub

#End Region

End Class