'Imports Oracle.DataAccess.Client
Public Class IaipFacilitySummaryPrint

    Private Sub IaipFacilitySummaryPrintLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        FullPrintStartDate.Text = Format(Today.AddMonths(-12), DateFormat)
        FullPrintEndDate.Text = TodayString
    End Sub

    Private Sub ShowReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowBasicReport.Click, ShowFullReport.Click
        If Not (AirsNumber.Text <> "" And AirsNumber.Text.Length = 8) Then
            MessageBox.Show("The AIRS Number is invalid")
            Exit Sub
        End If
        Try
            If sender IsNot Nothing Then
                sender.Cursor = Cursors.AppStarting
            End If
            PrintOut = New IAIPPrintOut
            Select Case sender.Name.ToString
                Case ShowBasicReport.Name.ToString
                    PrintOut.txtPrintType.Text = "BasicFacilityReport"
                Case ShowFullReport.Name.ToString
                    PrintOut.txtPrintType.Text = "FullFacilityReport"
                    PrintOut.txtStartDate.Text = FullPrintStartDate.Text
                    PrintOut.txtEndDate.Text = FullPrintEndDate.Text
            End Select
            With PrintOut
                .txtAIRSNumber.Text = AirsNumber.Text
                .Show()
                '.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If
        End Try

    End Sub

End Class