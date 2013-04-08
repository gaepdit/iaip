Imports System.Data.OracleClient

'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine

Public Class PassFailNoShow

    Private Sub PassFailNoShow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rpt As New crPassFailNoShow
        'Dim key As String = txtPassFailNoShow.Text

        Try
            Dim ds As DataSet
            Dim da As OracleDataAdapter

            SQL = "Select * from " & DBNameSpace & ".SmokeSchoolPrintInfo "

            ds = New DataSet

            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "SmokeSchoolPrintInfo")

            rpt = New crPassFailNoShow

            rpt.SetDataSource(ds)

            'Display the Report
            crPassFailNoShowViewer.ReportSource = rpt
            crPassFailNoShowViewer.Refresh()

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
End Class