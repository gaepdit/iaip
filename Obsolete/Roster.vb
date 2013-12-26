Public Class Roster

    Private Sub Roster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Dim rpt As New crRoster
        monitor.TrackFeature("Report." & rpt.ResourceName)
        Dim location As String = txtLocationTerm.Text

        Try
            'Development
            'rpt.SetDatabaseLogon("AIRBranch", AIRDevUserPassword)

            'Testing
            'rpt.SetDatabaseLogon("AIRBranch_App_User", AIRTestuserPassword)

            'Production
            rpt.SetDatabaseLogon("AIRBranch_App_User", SimpleCrypt("зтбрт±м"))
            'rpt.SetDataSource(dsRes)

            'crRosterViewer.SelectionFormula = "{SmokeSchoolPrintInfo.strLocationTerm} = '" & location & "'"
            crRosterViewer.ReportSource = rpt

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try
    End Sub


    Private Sub crRosterViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crRosterViewer.Load

    End Sub
End Class