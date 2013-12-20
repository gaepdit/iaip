Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Namespace Apb.SSCP

    Public Class Acc
        Inherits WorkItem

        Public Overrides ReadOnly Property EventType() As String
            Get
                Return "Annual Compliance Certification"
            End Get
        End Property

        Public Overrides ReadOnly Property EventTypeCode() As String
            Get
                Return "04"
            End Get
        End Property

        Public Property AccReportingYear() As Integer
            Get
                Return _accReportingYear
            End Get
            Set(ByVal value As Integer)
                _accReportingYear = value
            End Set
        End Property
        Private _accReportingYear As Integer

        Public Property SubmittalNumber() As Integer
            Get
                Return _submittalNumber
            End Get
            Set(ByVal value As Integer)
                _submittalNumber = value
            End Set
        End Property
        Private _submittalNumber As Integer

        Public Property DatePostmarked() As Date
            Get
                Return _datePostmarked
            End Get
            Set(ByVal value As Date)
                _datePostmarked = value
            End Set
        End Property
        Private _datePostmarked As Date

        Public Property PostmarkedByDeadline() As Boolean
            Get
                Return _correctFormsUsed
            End Get
            Set(ByVal value As Boolean)
                _postmarkedByDeadline = value
            End Set
        End Property
        Private _postmarkedByDeadline As Boolean

        Public Property SignedByResponsibleOfficial() As Boolean
            Get
                Return _signedByRO
            End Get
            Set(ByVal value As Boolean)
                _signedByRO = value
            End Set
        End Property
        Private _signedByRO As Boolean

        Public Property CorrectFormsUsed() As Boolean
            Get
                Return _correctFormsUsed
            End Get
            Set(ByVal value As Boolean)
                _correctFormsUsed = value
            End Set
        End Property
        Private _correctFormsUsed As Boolean

        Public Property AllTitleVConditionsListed() As Boolean
            Get
                Return _allTitleVConditionsListed
            End Get
            Set(ByVal value As Boolean)
                _allTitleVConditionsListed = value
            End Set
        End Property
        Private _allTitleVConditionsListed As Boolean

        Public Property CorrectlyFilledOut() As Boolean
            Get
                Return _correctlyFilledOut
            End Get
            Set(ByVal value As Boolean)
                _correctlyFilledOut = value
            End Set
        End Property
        Private _correctlyFilledOut As Boolean

        Public Property DeviationsReported() As Boolean
            Get
                Return _deviationsReported
            End Get
            Set(ByVal value As Boolean)
                _deviationsReported = value
            End Set
        End Property
        Private _deviationsReported As Boolean

        Public Property UnreportedDeviationsReported() As Boolean
            Get
                Return _unreportedDeviationsReported
            End Get
            Set(ByVal value As Boolean)
                _unreportedDeviationsReported = value
            End Set
        End Property
        Private _unreportedDeviationsReported As Boolean

        Public Property EnforcementNeeded() As Boolean
            Get
                Return _enforcementNeeded
            End Get
            Set(ByVal value As Boolean)
                _enforcementNeeded = value
            End Set
        End Property
        Private _enforcementNeeded As Boolean

        Public Property AllDeviationsReported() As Boolean
            Get
                Return _allDeviationsReported
            End Get
            Set(ByVal value As Boolean)
                _allDeviationsReported = value
            End Set
        End Property
        Private _allDeviationsReported As Boolean

        Public Property ResubmittalRequested() As Boolean
            Get
                Return _resubmittalRequested
            End Get
            Set(ByVal value As Boolean)
                _resubmittalRequested = value
            End Set
        End Property
        Private _resubmittalRequested As Boolean

    End Class

End Namespace