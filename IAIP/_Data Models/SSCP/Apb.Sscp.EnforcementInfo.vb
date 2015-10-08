Imports Iaip.Apb.Facilities
Namespace Apb.Sscp

    Public Class EnforcementInfo

        Public Property Facility() As Facility
        Public Property EnforcementNumber() As String
        Public Property EnforcementTypeCode() As String
        Public Property DiscoveryDate() As Date?
        Public Property StaffResponsible() As Staff
        Public Property Open() As Boolean
        Public Property DateFinalized() As Date?

    End Class

End Namespace
