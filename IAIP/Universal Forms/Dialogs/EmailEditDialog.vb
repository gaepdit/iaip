Imports Iaip.ApiCalls.EmailQueue

Friend Class EmailEditDialog
    Public Sub New(emailMessage As EmailMessage)
        InitializeComponent()

        BodyText.Text = emailMessage.Body
        SubjectLabel.Text = emailMessage.Subject
        RecipientLabel.Text = emailMessage.Recipients.ConcatNonEmptyStrings(", ")
        CopiedLabel.Text = emailMessage.CopyRecipients.ConcatNonEmptyStrings(", ").IfEmpty("None")
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)

#If DEBUG Then
        Icon = My.Resources.DevIcon
#ElseIf UAT Then
        Icon = My.Resources.UatIcon
#End If

        MyBase.OnLoad(e)
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        btnCancel.Focus()
        MyBase.OnShown(e)
    End Sub

End Class
