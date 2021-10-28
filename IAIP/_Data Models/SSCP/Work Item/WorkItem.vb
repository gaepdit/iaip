Namespace Apb.Sscp

    Public Class WorkItem

        ' Data from SSCPITEMMASTER table
        Public Property Facility As Facilities.Facility
        Public Property DateReceived As Date
        Public Property StaffResponsible As IaipUser
        Public Property DateComplete As Date?
        Public Property Deleted As Boolean
        Public Property DateAcknowledgmentLetterSent As Date?

        ' Common data from event-type-specific tables
        Public Property Comments As String

        Public Enum WorkItemEventType
            Unknown = 0
            Report = 1
            Inspection = 2
            StackTest = 3
            Notification = 5
            TvAcc = 4
            RmpInspection = 7
        End Enum

#Region " Read-only Display Properties"
        ' Mostly for use by Crystal Reports

        Public ReadOnly Property DisplayFacilityName() As String
            Get
                Return Facility.FacilityName
            End Get
        End Property
        Public ReadOnly Property DisplayAirsNumber() As String
            Get
                Return Facility.AirsNumber.FormattedString
            End Get
        End Property
        Public ReadOnly Property DisplayStaffName() As String
            Get
                Return StaffResponsible.FullName
            End Get
        End Property
        Public ReadOnly Property DisplayFacilityCity() As String
            Get
                Return Facility.FacilityLocation.Address.City
            End Get
        End Property

#End Region

    End Class

End Namespace