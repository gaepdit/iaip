Imports System.Collections.Generic

Namespace Apb.Sscp

    Public Class Notification
        Inherits WorkItem

        ' Data from SSCPNOTIFICATIONS table
        Public Property NotificationDue As Date?
        Public Property NotificationSentByFacility As Date?
        Public Property TypeOfNotification As NotificationType
        'Public Property NotificationTypeDbCode As String
        '    Get
        '        Return NotificationTypeDbCodes(TypeOfNotification)
        '    End Get
        '    Set(value As String)

        '    End Set
        'End Property

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

        'Public NotificationTypeDbCodes As New Dictionary(Of NotificationType, String) From {
        '    {NotificationType.Other, "01"},
        '    {NotificationType.Startup, "02"},
        '    {NotificationType.PermitRevocation, "03"},
        '    {NotificationType.ResponseLetter, "06"},
        '    {NotificationType.Malfunction, "07"},
        '    {NotificationType.Deviation, "08"}
        '}


    End Class

End Namespace