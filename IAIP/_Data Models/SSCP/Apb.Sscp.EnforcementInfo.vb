﻿Imports Iaip.Apb.Facilities
Namespace Apb.Sscp

    Public Class EnforcementInfo

        Public Property Facility() As Facility
        Public Property EnforcementNumber() As Integer
        Public Property EnforcementTypeCode() As String
        Public Property DiscoveryDate() As Date?
        Public Property StaffResponsible() As IaipUser
        Public Property Open() As Boolean
        Public Property DateFinalized() As Date?
        Public Property IsDeleted() As Boolean = False

    End Class

End Namespace
