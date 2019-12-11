Namespace Dmu

    ''' <summary>
    ''' An individual error record recorded by the ICIS-Air EDT submission process
    ''' </summary>
    Public Class EdtError
        Public Property ErrorID As Integer
        Public Property ErrorMessage As EdtErrorMessage
        Public Property EdtErrorMessageDetail As String
        Public Property EdtSubmission As EdtSubmission
        Public Property AssignedToUserID As Integer
        Public Property Resolved As Boolean
        Public Property ResolvedDate As DateTime?
        Public Property ResolvedByUserID As Integer
        Public Property ResolvedByUserName As String
    End Class

End Namespace
