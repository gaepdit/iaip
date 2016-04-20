Imports Iaip.Apb.Facilities
Namespace Apb.Sspp

    Public Class ApplicationInfo

        Public Property Facility() As Facility
        Public Property ApplicationNumber() As String
        Public Property ApplicationType() As String
        Public Property PermitType() As String
        Public Property DateIssued() As Date?
        Public Property StaffResponsible() As IaipUser

    End Class

End Namespace
