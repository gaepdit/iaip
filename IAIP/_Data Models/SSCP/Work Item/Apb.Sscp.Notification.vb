Imports System.Collections.Generic

Namespace Apb.Sscp

    Public Class Notification
        Inherits WorkItem

        ' Data from SSCPNOTIFICATIONS table
        Public Property NotificationDue As Date?
        Public Property NotificationSentByFacility As Date?
        Public Property TypeOfNotification As NotificationType
        Public Property OtherTypeOfNotification As String
        'End Property
        Public Property FollowUpActionTaken As Boolean

#Region " Enums "

        Public Enum NotificationType
            Other
            Startup
            PermitRevocation
            ResponseLetter
            Malfunction
            Deviation
        End Enum

        Public NotificationTypeStrings As New Dictionary(Of NotificationType, String) From {
            {NotificationType.Other, "Other"},
            {NotificationType.Startup, "Startup"},
            {NotificationType.PermitRevocation, "Permit Revocation"},
            {NotificationType.ResponseLetter, "Response Letter"},
            {NotificationType.Malfunction, "Malfunction"},
            {NotificationType.Deviation, "Deviation"}
        }

#End Region

    End Class

End Namespace