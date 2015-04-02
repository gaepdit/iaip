Public Class DmuEdtErrorMessages

    Private EdtErrorMessages As DataTable
    Private bs As BindingSource

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

    Private Sub GetData()
        EdtErrorMessages = DAL.DMU.GetErrorCounts(CurrentUser.UserID)

        If EdtErrorMessages IsNot Nothing Then
            bs = New BindingSource
            bs.DataSource = EdtErrorMessages
            EdtErrorMessageGrid.DataSource = bs

            FormatGrid()
            SetGridDisplay()
        Else
            bs = Nothing
            EdtErrorMessageGrid.DataSource = Nothing
            EdtErrorCountDisplay.Text = "No errors to display"
        End If
    End Sub

    Private Sub FormatGrid()

    End Sub

    Private Sub SetGridDisplay()
        If DisplayMine.Checked And DisplayOpen.Checked Then
            bs.Filter = "DefaultUserID = " & CurrentUser.UserID & " and CountOpenAssignedToUser > 0 "
        ElseIf DisplayMine.Checked And DisplayAll.Checked Then
            bs.Filter = "DefaultUserID = " & CurrentUser.UserID
        ElseIf DisplayEveryone.Checked And DisplayOpen.Checked Then
            bs.Filter = "CountOpen > 0 "
        ElseIf DisplayEveryone.Checked And DisplayAll.Checked Then
            bs.RemoveFilter()
        End If

        Dim shown As Integer = bs.Count
        EdtErrorCountDisplay.Text = shown.ToString & " errors" & If(shown = 1, "", "s") & " shown"
    End Sub

    Private Sub ReloadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReloadButton.Click
        GetData()
    End Sub

    Private Sub DisplayOptionsChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetGridDisplay()
    End Sub

End Class