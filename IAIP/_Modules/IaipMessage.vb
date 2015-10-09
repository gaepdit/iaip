Class IaipMessage

#Region " Properties "

    Public Property MessageText As String
    Public Property WarningLevel As WarningLevels
    Public Enum WarningLevels
        Success
        Info
        Warning
        ErrorReport
    End Enum

    Private Property DisplayLabel As Label = Nothing
    Private Property ErrorProvider As ErrorProvider = Nothing
    Private Property ErrorControl As Control = Nothing

#End Region

#Region " Constructor "

    ''' <summary>
    ''' Creates a new IaipMessage
    ''' </summary>
    ''' <param name="messageText">The text of the message</param>
    ''' <param name="warningLevel">The warning level of the message</param>
    Public Sub New(messageText As String, warningLevel As WarningLevels)
        Me.MessageText = messageText
        Me.WarningLevel = warningLevel
    End Sub

#End Region

#Region " Display/Clear procedures "

    ''' <summary>
    ''' Displays a precomposed message in a Label. Label is formatted based on the message WarningLevel. Optionally sets up an ErrorProvider.
    ''' </summary>
    ''' <param name="displayLabel">The label in which to display the message</param>
    ''' <param name="errorProvider">The optional ErrorProvider</param>
    ''' <param name="errorControl">The optional control to attach the ErrorProvider to</param>
    Public Sub Display(ByVal displayLabel As Label, Optional ByVal errorProvider As ErrorProvider = Nothing, Optional ByVal errorControl As Control = Nothing)
        Me.DisplayLabel = displayLabel
        Me.ErrorProvider = errorProvider
        Me.ErrorControl = errorControl

        Select Case Me.WarningLevel
            Case WarningLevels.ErrorReport
                Me.DisplayLabel.ForeColor = Color.DarkRed
                If errorProvider IsNot Nothing AndAlso errorControl IsNot Nothing Then
                    errorProvider.SetError(errorControl, Me.MessageText)
                    errorProvider.SetIconAlignment(errorControl, System.Windows.Forms.ErrorIconAlignment.TopLeft)
                End If

            Case WarningLevels.Warning
                Me.DisplayLabel.ForeColor = Color.DarkRed

            Case WarningLevels.Info
                Me.DisplayLabel.ForeColor = SystemColors.InfoText
                Me.DisplayLabel.BackColor = SystemColors.Info

            Case WarningLevels.Success
                Me.DisplayLabel.ForeColor = Color.DarkGreen
        End Select

        Me.DisplayLabel.Text = Me.MessageText
        Me.DisplayLabel.Visible = True

    End Sub

    ''' <summary>
    ''' Clears a previously displayed message. 
    ''' </summary>
    Public Sub Clear()
        If Me.DisplayLabel IsNot Nothing Then
            Me.DisplayLabel.Text = ""
            Me.DisplayLabel.Visible = False
        End If

        If Me.ErrorProvider IsNot Nothing Then
            Me.ErrorProvider.Clear()
        End If

        Me.DisplayLabel = Nothing
        Me.ErrorProvider = Nothing
        Me.ErrorControl = Nothing
    End Sub


#Region " Legacy shared methods "

    ''' <summary>
    ''' Displays a precomposed message. If message represents an error, also shows error provider.
    ''' </summary>
    ''' <param name="messageDisplay">The label to display the message</param>
    ''' <param name="message">The message to display</param>
    ''' <param name="isError">True if message represents an error. Defaults to False.</param>
    ''' <param name="errorProvider">The error provider control</param>
    ''' <param name="control">The control to attach the error provider to</param>
    Public Shared Sub DisplayMessage(ByVal messageDisplay As Label, ByVal message As String, Optional ByVal isError As Boolean = False, Optional ByVal errorProvider As ErrorProvider = Nothing, Optional ByVal errorControl As Control = Nothing)
        If isError Then
            messageDisplay.ForeColor = Color.DarkRed
            If errorProvider IsNot Nothing AndAlso errorControl IsNot Nothing Then
                errorProvider.SetError(errorControl, message)
                errorProvider.SetIconAlignment(errorControl, System.Windows.Forms.ErrorIconAlignment.TopLeft)
            End If
        Else
            messageDisplay.ForeColor = Color.DarkGreen
        End If
        messageDisplay.Text = message
        messageDisplay.Visible = True
    End Sub

    ''' <summary>
    ''' Clears a message and error provider
    ''' </summary>
    ''' <param name="messageDisplay">The label containing the message to clear</param>
    ''' <param name="errorProvider">The error provider to clear</param>
    Public Shared Sub ClearMessage(ByVal messageDisplay As Label, Optional ByVal errorProvider As ErrorProvider = Nothing)
        messageDisplay.Visible = False
        messageDisplay.Text = ""
        If errorProvider IsNot Nothing Then errorProvider.Clear()
    End Sub

#End Region

#End Region

End Class
