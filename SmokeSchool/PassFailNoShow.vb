Imports Oracle.DataAccess.Client

'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine

Public Class PassFailNoShow

    Private Sub PassFailNoShow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Dim rpt As New crPassFailNoShow
        'Dim key As String = txtPassFailNoShow.Text

        Try
            Dim ds As DataSet
            Dim da As OracleDataAdapter

            SQL = "Select * from " & DBNameSpace & ".SmokeSchoolPrintInfo "

            ds = New DataSet

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "SmokeSchoolPrintInfo")

            rpt = New crPassFailNoShow
            monitor.TrackFeature("Report." & rpt.ResourceName)

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