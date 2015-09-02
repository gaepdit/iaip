Namespace Apb.Sscp

    Public Class Acc
        Inherits WorkItem

        ' Data from SSCPACCS or SSCPACCSHISTORY table
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