Imports System.Collections.Generic

Namespace Apb.Sscp

    Public Class Report
        Inherits WorkItem

        ' Data from SSCPREPORTS or SSCPREPORTSHISTORY table
        Public Property ReportingPeriod As ReportingPeriodTypes
        Public Property ReportingPeriodStart As Date
        Public Property ReportingPeriodEnd As Date
        Public Property ReportDue As Date
        Public Property SentByFacility As Date
        Public Property ReportComplete As Boolean
        Public Property EnforcementNeeded As Boolean
        Public Property ShowsDeviations As Boolean
        Public Property SubmittalNumber As Integer

        Public Enum ReportingPeriodTypes
            Other
            FirstQuarter
            SecondQuarter
            ThirdQuarter
            FourthQuarter
            FirstSemiannual
            SecondSemiannual
            Annual
            Monthly
            Malfunction
        End Enum

        Public ReportingPeriodStrings As New Dictionary(Of ReportingPeriodTypes, String) From {
            {ReportingPeriodTypes.FirstQuarter, "First Quarter"},
            {ReportingPeriodTypes.SecondQuarter, "Second Quarter"},
            {ReportingPeriodTypes.ThirdQuarter, "Third Quarter"},
            {ReportingPeriodTypes.FourthQuarter, "Fourth Quarter"},
            {ReportingPeriodTypes.FirstSemiannual, "First Semiannual"},
            {ReportingPeriodTypes.SecondSemiannual, "Second Semiannual"},
            {ReportingPeriodTypes.Annual, "Annual"},
            {ReportingPeriodTypes.Other, "Other"},
            {ReportingPeriodTypes.Monthly, "Monthly"},
            {ReportingPeriodTypes.Malfunction, "Malfunction/Deviation"}
        }

    End Class

End Namespace