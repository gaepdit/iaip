Namespace DMU

    ''' <summary>
    ''' An EDT Error Message category and description
    ''' </summary>
    Public Class EdtErrorMessage
        Public Property ErrorCode() As String
        Public Property ErrorMessage() As String
        Public Property ErrorCategory() As String
        Public Property BusinessRuleCode() As String
        Public Property BusinessRuleMessage() As String
        Public Property DefaultUserID() As Integer
        Public Property DefaultUserName() As String
    End Class

    ''' <summary>
    ''' An individual record submitted to the ICIS-Air EDT process
    ''' </summary>
    Public Class EdtSubmission
        Public Property EdtTableName() As String
        Public Property EdtID() As String
        Public Property IaipID() As String
        Public Property IaipIDCategory() As EdtIdCategory
        Public Property IaipForeignID() As String
        Public Property IaipForeignIDCategory() As EdtIdCategory
        Public Property EdtForeignKeyID() As String
        Public Property EdtOperation() As String
        Public Property EdtStatus() As String
        Public Property EdtSubmitDate() As DateTime
    End Class

    ''' <summary>
    ''' An individual error record recorded by the ICIS-Air EDT submission process
    ''' </summary>
    Public Class EdtError
        Public Property ErrorID() As Integer
        Public Property ErrorMessage() As EdtErrorMessage
        Public Property EdtErrorMessageDetail() As String
        Public Property EdtSubmission() As EdtSubmission
        Public Property AssignedToUserID() As Integer
        Public Property Resolved() As Boolean
        Public Property ResolvedDate() As DateTime?
        Public Property ResolvedByUserID() As Integer
        Public Property ResolvedByUserName() As String
    End Class

    Public Enum EdtIdCategory
        None
        AIRFACILITY
        CASEFILE
        COMPLIANCEMONITORING
        COMPLIANCEMONITORINGFCE
        ENFORCEMENTACTION
    End Enum

End Namespace
