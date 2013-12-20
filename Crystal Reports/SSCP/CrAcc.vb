Namespace CR.Data

    Public Class CrAcc
        Inherits Apb.SSCP.Acc

        Public Sub New(ByVal acc As Apb.SSCP.Acc)
            Me.AccReportingYear = acc.AccReportingYear
            Me.AllDeviationsReported = acc.AllDeviationsReported
            Me.AllTitleVConditionsListed = acc.AllTitleVConditionsListed
            Me.Comments = acc.Comments
            Me.CorrectFormsUsed = acc.CorrectFormsUsed
            Me.CorrectlyFilledOut = acc.CorrectlyFilledOut
            Me.DateAcknowledgmentLetterSent = acc.DateAcknowledgmentLetterSent
            Me.DateComplete = acc.DateComplete
            Me.DatePostmarked = acc.DatePostmarked
            Me.DateReceived = acc.DateReceived
            Me.Deleted = acc.Deleted
            Me.DeviationsReported = acc.DeviationsReported
            Me.EnforcementNeeded = acc.EnforcementNeeded
            Me.Facility = acc.Facility
            Me.ResubmittalRequested = acc.ResubmittalRequested
            Me.SignedByResponsibleOfficial = acc.SignedByResponsibleOfficial
            Me.StaffResponsible = acc.StaffResponsible
            Me.SubmittalNumber = acc.SubmittalNumber
            Me.TrackingNumber = acc.TrackingNumber
            Me.UnreportedDeviationsReported = acc.UnreportedDeviationsReported
        End Sub


        Public ReadOnly Property FacilityName() As String
            Get
                Return Me.Facility.Name
            End Get
        End Property

        Public ReadOnly Property FacilityAirsNumber() As String
            Get
                Return Me.Facility.AirsNumber
            End Get
        End Property

        Public ReadOnly Property StaffName() As String
            Get
                Return Me.StaffResponsible.FullName
            End Get
        End Property

        Public ReadOnly Property FacilityCity() As String
            Get
                Return Me.Facility.FacilityLocation.Address.City
            End Get
        End Property

    End Class

End Namespace
