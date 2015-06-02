Imports Iaip.Apb.Facilities
Namespace Apb.SSCP

    Public Class EnforcementInfo

        Public Property Facility() As Facility
            Get
                Return _facility
            End Get
            Set(ByVal value As Facility)
                _facility = value
            End Set
        End Property
        Private _facility As Facility

        Public Property EnforcementNumber() As String
            Get
                Return _enforcementNumber
            End Get
            Set(ByVal value As String)
                _enforcementNumber = value
            End Set
        End Property
        Private _enforcementNumber As String

        Public Property EnforcementTypeCode() As String
            Get
                Return _enforcementTypeCode
            End Get
            Set(ByVal value As String)
                _enforcementTypeCode = value
            End Set
        End Property
        Private _enforcementTypeCode As String

        Public Property DiscoveryDate() As Date?
            Get
                Return _discoveryDate
            End Get
            Set(ByVal value As Date?)
                _discoveryDate = value
            End Set
        End Property
        Private _discoveryDate As Date?

        Public Property StaffResponsible() As Staff
            Get
                Return _staffResponsible
            End Get
            Set(ByVal value As Staff)
                _staffResponsible = value
            End Set
        End Property
        Private _staffResponsible As Staff

        Private _open As Boolean
        Public Property Open() As Boolean
            Get
                Return _open
            End Get
            Set(ByVal value As Boolean)
                _open = value
            End Set
        End Property

        Private _dateFinalized As Date?
        Public Property DateFinalized() As Date?
            Get
                Return _dateFinalized
            End Get
            Set(ByVal value As Date?)
                _dateFinalized = value
            End Set
        End Property

    End Class

End Namespace
