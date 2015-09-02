Namespace Apb.Sscp

    Public Class Fce
        Public Property FceNumber() As String
        Public Property Facility() As Apb.Facilities.Facility
        Public Property FceYear As String
        Public Property DateComplete() As Date
        Public Property SiteVisitType As FceSiteVisitType
        Public Property SiteVisitTypeDbCode As Boolean
            Get
                If SiteVisitType = FceSiteVisitType.OnSite Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(value As Boolean)
                If value Then
                    SiteVisitType = FceSiteVisitType.OnSite
                Else
                    SiteVisitType = FceSiteVisitType.OffSite
                End If
            End Set
        End Property
        Public Property StaffResponsible() As Staff
        Public Property Comments() As String

        Public Enum FceSiteVisitType
            OnSite
            OffSite
        End Enum

    End Class

End Namespace