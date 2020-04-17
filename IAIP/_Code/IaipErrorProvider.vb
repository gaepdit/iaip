Public Class IaipErrorProvider
    Implements IDisposable

    ' Properties/fields

    Public Property ErrorControl As Control
    Public Property DisplayLabel As Label
    Public Property Message As String
    Public ReadOnly Property HasError As Boolean
        Get
            Return _hasError
        End Get
    End Property

    Private errorProvider As ErrorProvider
    Private _hasError As Boolean = False

    ' Constructor

    ''' <summary>
    ''' Creates a new IaipErrorProvider.
    ''' </summary>
    ''' <param name="errorControl">The Control to display errors.</param>
    ''' <param name="displayLabel">The label to display messages.</param>
    Public Sub New(Optional errorControl As Control = Nothing, Optional displayLabel As Label = Nothing)
        Me.ErrorControl = errorControl
        Me.DisplayLabel = displayLabel
    End Sub

    ' Display/Clear 

    Public Sub SetError(messageText As String)
        _hasError = True
        Message = messageText

        If DisplayLabel IsNot Nothing Then
            DisplayLabel.ShowMessage(Message, ErrorLevel.Error)
        End If

        errorProvider = New ErrorProvider()

        If ErrorControl IsNot Nothing Then
            errorProvider.SetError(ErrorControl, Message)
            errorProvider.SetIconAlignment(ErrorControl, ErrorIconAlignment.MiddleLeft)
            errorProvider.SetIconPadding(ErrorControl, 3 - ErrorControl.Width) ' Inside right of Control with 3px of padding 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink
        ElseIf DisplayLabel IsNot Nothing Then
            errorProvider.SetError(DisplayLabel, Message)
            errorProvider.SetIconAlignment(DisplayLabel, ErrorIconAlignment.TopLeft)
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink
        End If
    End Sub

    ''' <summary>
    ''' Clears a previously displayed message. 
    ''' </summary>
    Public Sub ClearError()
        _hasError = False

        If DisplayLabel IsNot Nothing Then
            DisplayLabel.ClearMessage()
        End If

        If errorProvider IsNot Nothing Then
            If ErrorControl IsNot Nothing Then
                errorProvider.SetError(ErrorControl, String.Empty)
            ElseIf DisplayLabel IsNot Nothing Then
                errorProvider.SetError(DisplayLabel, String.Empty)
            End If
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue AndAlso disposing Then
            If errorProvider IsNot Nothing Then errorProvider.Dispose()
            If ErrorControl IsNot Nothing Then ErrorControl.Dispose()
            If DisplayLabel IsNot Nothing Then DisplayLabel.Dispose()
        End If
        disposedValue = True
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
