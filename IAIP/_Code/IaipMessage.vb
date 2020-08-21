Friend Class IaipMessage

#Region " Properties "

    Public Property MessageText As String
    Public Property WarningLevel As WarningLevels
    Public Enum WarningLevels
        None
        Success
        Info
        Warning
        ErrorReport
    End Enum

    Private Property DisplayLabel As Label
    Private Property ErrorProvider As ErrorProvider

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Creates a new empty IaipMessage
    ''' </summary>
    Public Sub New()
        Me.WarningLevel = WarningLevels.None
    End Sub

    ''' <summary>
    ''' Creates a new IaipMessage
    ''' </summary>
    ''' <param name="messageText">The text of the message</param>
    ''' <param name="warningLevel">The warning level of the message</param>
    Public Sub New(messageText As String, Optional warningLevel As WarningLevels = WarningLevels.None)
        Me.MessageText = messageText
        Me.WarningLevel = warningLevel
    End Sub

#End Region

#Region " Display/Clear procedures "

    ''' <summary>
    ''' Displays a precomposed message in a Label. Label is formatted based on the message WarningLevel. Optionally sets up an ErrorProvider.
    ''' </summary>
    ''' <param name="displayLabel">The label in which to display the message.</param>
    ''' <param name="errorProvider">The optional ErrorProvider.</param>
    Public Sub Display(displayLabel As Label, Optional errorProvider As ErrorProvider = Nothing)
        If Me Is Nothing Then
            Return
        End If

        Me.DisplayLabel = displayLabel
        Me.ErrorProvider = errorProvider

        Select Case Me.WarningLevel

            Case WarningLevels.ErrorReport, WarningLevels.Warning
                Me.DisplayLabel.ForeColor = IaipColors.ErrorForeColor
                Me.DisplayLabel.BackColor = IaipColors.ErrorBackColor

            Case WarningLevels.Info
                Me.DisplayLabel.ForeColor = IaipColors.InfoForeColor
                Me.DisplayLabel.BackColor = IaipColors.InfoBackColor

            Case WarningLevels.Success
                Me.DisplayLabel.ForeColor = IaipColors.SuccessForeColor
                Me.DisplayLabel.BackColor = IaipColors.SuccessBackColor

        End Select

        Me.DisplayLabel.Text = Me.MessageText
        Me.DisplayLabel.Visible = True

        If Me.ErrorProvider IsNot Nothing Then
            Me.ErrorProvider.SetError(displayLabel, Me.MessageText)
            Me.ErrorProvider.SetIconAlignment(displayLabel, System.Windows.Forms.ErrorIconAlignment.TopLeft)
        End If
    End Sub

    ''' <summary>
    ''' Clears a previously displayed message. 
    ''' </summary>
    Public Sub Clear()
        If DisplayLabel IsNot Nothing Then
            DisplayLabel.Text = ""
            DisplayLabel.Visible = False
            DisplayLabel.ForeColor = Color.Empty
            DisplayLabel.BackColor = Color.Empty
        End If

        If ErrorProvider IsNot Nothing Then ErrorProvider.Clear()

        DisplayLabel = Nothing
        ErrorProvider = Nothing
    End Sub

#End Region

End Class
