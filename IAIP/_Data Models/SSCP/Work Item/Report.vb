Imports System.Collections.Generic

Namespace Apb.Sscp

    Public Class Report
        Inherits WorkItem

        ' Data from SSCPREPORTS or SSCPREPORTSHISTORY table
        Public Property ReportingPeriod As ReportingPeriodType
        Public Property ReportingPeriodComments As String
        Public Property ReportingPeriodStart As Date
        Public Property ReportingPeriodEnd As Date
        Public Property ReportDue As Date
        Public Property SentByFacility As Date
        Public Property ReportComplete As Boolean
        Public Property EnforcementNeeded As Boolean
        Public Property ShowsDeviations As Boolean
        Public Property SubmittalNumber As Integer

        Public Enum ReportingPeriodType
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

        Public ReadOnly Property ReportingPeriodStrings As New Dictionary(Of ReportingPeriodType, String) From {
            {ReportingPeriodType.FirstQuarter, "First Quarter"},
            {ReportingPeriodType.SecondQuarter, "Second Quarter"},
            {ReportingPeriodType.ThirdQuarter, "Third Quarter"},
            {ReportingPeriodType.FourthQuarter, "Fourth Quarter"},
            {ReportingPeriodType.FirstSemiannual, "First Semiannual"},
            {ReportingPeriodType.SecondSemiannual, "Second Semiannual"},
            {ReportingPeriodType.Annual, "Annual"},
            {ReportingPeriodType.Other, "Other"},
            {ReportingPeriodType.Monthly, "Monthly"},
            {ReportingPeriodType.Malfunction, "Malfunction/Deviation"}
        }

    End Class

End Namespace