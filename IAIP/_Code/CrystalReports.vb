Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine

Module CrystalReports

#Region "Crystal Reports displayer"

    Public Sub SetUpCrystalReportViewer(report As ReportClass, crReportViewer As CrystalReportViewer, TabText As String)
        crReportViewer.ReportSource = report

        crReportViewer.ToolPanelView = ToolPanelViewType.None
        crReportViewer.DisplayToolbar = True
        crReportViewer.ShowRefreshButton = False
        crReportViewer.Visible = True
        crReportViewer.ShowGroupTreeButton = False
        crReportViewer.ShowLogo = False
        crReportViewer.ShowParameterPanelButton = False

        Dim i As Integer
        Do While i < crReportViewer.Controls.Count
            If TypeOf (crReportViewer.Controls(i)) Is PageView Then
                Dim j As Integer
                Do While j < crReportViewer.Controls(i).Controls.Count
                    If CType(crReportViewer.Controls(i).Controls(j), TabControl).TabPages.Count > 0 Then
                        ' Change the tab text
                        CType(crReportViewer.Controls(i).Controls(j), TabControl).TabPages.Item(0).Text = TabText
                        Exit Do
                    End If
                Loop
                Exit Do
            Else
                crReportViewer.Controls(i).Visible = False
            End If
        Loop

        crReportViewer.Refresh()
    End Sub

#End Region

End Module
