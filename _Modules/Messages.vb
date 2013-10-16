Module Messages

    ''' <summary>
    ''' Displays a precomposed message. If message represents an error, also shows error provider.
    ''' </summary>
    ''' <param name="messageDisplay">The label to display the message</param>
    ''' <param name="message">The message to display</param>
    ''' <param name="isError">True if message represents an error. Defaults to False.</param>
    ''' <param name="errorProvider">The error provider control</param>
    ''' <param name="control">The control to attach the error provider to</param>
    Public Sub DisplayMessage(ByVal messageDisplay As Label, ByVal message As String, Optional ByVal isError As Boolean = False, Optional ByVal errorProvider As ErrorProvider = Nothing, Optional ByVal control As Control = Nothing)
        If isError Then
            If errorProvider IsNot Nothing AndAlso control IsNot Nothing Then errorProvider.SetError(control, message)
            messageDisplay.ForeColor = Color.Chocolate
        Else
            messageDisplay.ForeColor = Color.ForestGreen
        End If
        messageDisplay.Text = message
        messageDisplay.Visible = True
    End Sub

    ''' <summary>
    ''' Clears a message and error provider
    ''' </summary>
    ''' <param name="messageDisplay">The label containing the message to clear</param>
    ''' <param name="errorProvider">The error provider to clear</param>
    Public Sub ClearMessage(ByVal messageDisplay As Label, Optional ByVal errorProvider As ErrorProvider = Nothing)
        messageDisplay.Visible = False
        messageDisplay.Text = ""
        If errorProvider IsNot Nothing Then errorProvider.Clear()
    End Sub

End Module
