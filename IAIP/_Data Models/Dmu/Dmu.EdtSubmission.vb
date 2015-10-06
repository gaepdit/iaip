Namespace Dmu

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

    Public Enum EdtIdCategory
        None
        AIRFACILITY
        CASEFILE
        COMPLIANCEMONITORING
        COMPLIANCEMONITORINGFCE
        ENFORCEMENTACTION
    End Enum

End Namespace
