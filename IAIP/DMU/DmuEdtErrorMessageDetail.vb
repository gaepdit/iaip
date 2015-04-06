Public Class DmuEdtErrorMessageDetail

#Region " Properties and variables "

    Private edtErrorMessagesTable As DataTable
    Private edtErrorMessagesBindingSource As BindingSource
    Private edtErrorMessageDetails As DMU.EdtErrorMessage

    Private _errorCode As String
    Public Property ErrorCode() As String
        Get
            Return _errorCode
        End Get
        Set(ByVal value As String)
            _errorCode = value
        End Set
    End Property

#End Region

#Region " Load "

    Private Sub DmuEdtErrorMessageDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddDisplayOptionHandlers()
        PrepDefaultUserCombo()
    End Sub

    Private Sub AddDisplayOptionHandlers()
        AddHandler DisplayMine.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayEveryone.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayOpen.CheckedChanged, AddressOf DisplayOptionsChanged
        AddHandler DisplayAll.CheckedChanged, AddressOf DisplayOptionsChanged
    End Sub

    Private Sub PrepDefaultUserCombo()
        Dim activeUsers As Generic.Dictionary(Of Integer, String) = DAL.GetActiveUsers
        DefaultUser.BindToDictionary(activeUsers)
    End Sub

#End Region

#Region " Form-level events "

    Private Sub DmuEdtErrorMessageDetail_ResizeEnd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        ErrorMessageDisplay.MaximumSize = New Size(ErrorMessageDisplayContainer.Size.Width - 30, 0)
        BusinessRuleDisplay.MaximumSize = New Size(BusinessRuleDisplayContainer.Size.Width - 30, 0)
    End Sub

#End Region

#Region " Data "

    Private Sub ReloadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReloadButton.Click
        'GetData()
        OpenErrorDetail(4) ' testing only
    End Sub

    Public Sub GetData()
        ErrorCodeDisplay.Text = ErrorCode

        ' Header
        edtErrorMessageDetails = DAL.DMU.GetErrorMessageDetail(ErrorCode)

        If edtErrorMessageDetails IsNot Nothing Then
            ErrorMessageDisplay.Text = edtErrorMessageDetails.ErrorMessage
            BusinessRuleDisplay.Text = edtErrorMessageDetails.BusinessRuleMessage
            DefaultUser.SelectedValue = edtErrorMessageDetails.DefaultUserID
        End If

        ' Table
        edtErrorMessagesTable = DAL.DMU.GetErrors(ErrorCode)

        If edtErrorMessagesTable IsNot Nothing Then
            edtErrorMessagesBindingSource = New BindingSource
            edtErrorMessagesBindingSource.DataSource = edtErrorMessagesTable
            EdtErrorMessageGrid.DataSource = edtErrorMessagesBindingSource

            FormatGrid()
            SetGridDisplay()
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
        ' Stuff about column headers and visibility goes here...
        '
        '

        With EdtErrorMessageGrid
            .MakeColumnsLookLikeLinks(0)
            .SanelyResizeColumns()
        End With
    End Sub

    Private Sub SetGridDisplay()
        If DisplayMine.Checked And DisplayOpen.Checked Then
            edtErrorMessagesBindingSource.Filter = "AssignedToUser = " & CurrentUser.UserID & " and Resolved = False"
        ElseIf DisplayMine.Checked And DisplayAll.Checked Then
            edtErrorMessagesBindingSource.Filter = "AssignedToUser = " & CurrentUser.UserID
        ElseIf DisplayEveryone.Checked And DisplayOpen.Checked Then
            edtErrorMessagesBindingSource.Filter = "Resolved = False"
        ElseIf DisplayEveryone.Checked And DisplayAll.Checked Then
            edtErrorMessagesBindingSource.RemoveFilter()
        End If

        Dim shown As Integer = edtErrorMessagesBindingSource.Count
        EdtErrorCountDisplay.Text = shown.ToString & " errors" & If(shown = 1, "", "s") & " shown"
    End Sub

    Private Sub DisplayOptionsChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetGridDisplay()
    End Sub

#End Region

#Region " DataGridView Selection and Events "

    Private Sub OpenErrorDetail(ByVal errorID As Integer)
        Dim edtErrorDetail As DmuEdtErrorDetail = OpenMultiForm(DmuEdtErrorDetail, errorID)
        edtErrorDetail.ErrorID = errorID
        edtErrorDetail.GetData()
    End Sub

    Private Sub EdtErrorMessageGrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EdtErrorMessageGrid.CellClick
        ' Only within the cell content of first column
        If e.RowIndex <> -1 And e.RowIndex < EdtErrorMessageGrid.RowCount And e.ColumnIndex = 0 Then
            OpenErrorDetail(EdtErrorMessageGrid.Rows(e.RowIndex).Cells(0).Value)
        End If
    End Sub

    Private Sub EdtErrorMessageGrid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles EdtErrorMessageGrid.CellDoubleClick
        'Double-click within the cell content (but exclude first column to avoid double-firing)
        If e.RowIndex <> -1 And e.RowIndex < EdtErrorMessageGrid.RowCount And e.ColumnIndex <> 0 Then
            OpenErrorDetail(EdtErrorMessageGrid.Rows(e.RowIndex).Cells(0).Value)
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
            OpenErrorDetail(EdtErrorMessageGrid.CurrentRow.Cells(0).Value)
        End If
    End Sub

#End Region

    Private Sub DefaultUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultUser.SelectedIndexChanged
        ErrorCodeDisplay.Text = DefaultUser.SelectedValue.ToString & " | " & DefaultUser.SelectedText.ToString
    End Sub
End Class