Imports System.Collections.Generic

Namespace Apb.Sscp

    Public Class ReportSubmittals

        ' Data from SSCPREPORTSHISTORY table
        Public Property SscpTrackingNumber() As String
        Public Property AllReportSubmittals As List(Of Report)

        ' Data from SSCPREPORTS table
        Public Property PrimarySubmittalNumber As Integer

    End Class

End Namespace