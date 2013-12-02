Namespace Apb
    Namespace SSPP

        Public Class ApplicationInfo
            Public Property Facility() As Facility
                Get
                    Return _facility
                End Get
                Set(ByVal value As Facility)
                    _facility = value
                End Set
            End Property
            Private _facility As Facility
            Public Property ApplicationNumber() As String
                Get
                    Return _applicationNumber
                End Get
                Set(ByVal value As String)
                    _applicationNumber = value
                End Set
            End Property
            Private _applicationNumber As String
            Public Property ApplicationType() As String
                Get
                    Return _applicationType
                End Get
                Set(ByVal value As String)
                    _applicationType = value
                End Set
            End Property
            Private _applicationType As String
            Public Property PermitType() As String
                Get
                    Return _permitType
                End Get
                Set(ByVal value As String)
                    _permitType = value
                End Set
            End Property
            Private _permitType As String
            Public Property DateIssued() As Date?
                Get
                    Return _dateIssued
                End Get
                Set(ByVal value As Date?)
                    _dateIssued = value
                End Set
            End Property
            Private _dateIssued As Date?
            Public Property StaffResponsible() As Staff
                Get
                    Return _staffResponsible
                End Get
                Set(ByVal value As Staff)
                    _staffResponsible = value
                End Set
            End Property
            Private _staffResponsible As Staff
        End Class

    End Namespace
End Namespace
