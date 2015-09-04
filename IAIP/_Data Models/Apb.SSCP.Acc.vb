Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Namespace Apb.SSCP

    Public Class Acc
        Inherits WorkItem

        Public Overrides Property EventType() As WorkItemEventType
            Get
                Return WorkItemEventType.TvAcc
            End Get
            Set(ByVal value As WorkItemEventType)
            End Set
        End Property

        Public Property AccReportingYear() As Integer
        Public Property SubmittalNumber() As Integer
        Public Property DatePostmarked() As Date
        Public Property PostmarkedByDeadline() As Boolean
        Public Property SignedByResponsibleOfficial() As Boolean
        Public Property CorrectFormsUsed() As Boolean
        Public Property AllTitleVConditionsListed() As Boolean
        Public Property CorrectlyFilledOut() As Boolean
        Public Property DeviationsReported() As Boolean
        Public Property UnreportedDeviationsReported() As Boolean
        Public Property EnforcementNeeded() As Boolean
        Public Property AllDeviationsReported() As Boolean
        Public Property ResubmittalRequested() As Boolean

    End Class

End Namespace