Imports System.Collections.Generic

Namespace Apb.Sscp

    Public Class AccSubmittals

        ' Data from SSCPACCSHISTORY table
        Public Property SscpTrackingNumber() As String
        Public Property AllAccSubmittals As List(Of Acc)

        ' Data from SSCPACCS table
        Public Property PrimarySubmittalNumber As Integer

    End Class

End Namespace