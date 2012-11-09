'Imports System.Data.OracleClient
Public Class IAIPFacilitySummaryPrint

    Private Sub IAIPFacilitySummaryPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            DTPFullPrintStartDate.Text = Format(CDate(OracleDate).AddMonths(-12), "dd-MMM-yyyy")
            DTPFullPrintEndDate.Text = OracleDate
            DTPExtendedPrintStartDate.Text = Format(CDate(OracleDate).AddMonths(-12), "dd-MMM-yyyy")
            DTPExtendedPrintEndDate.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRunReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunReport.Click
        Try
            If mtbAIRSNumber.Text <> "" And mtbAIRSNumber.Text.Length = 8 Then
                If rdbBasicReport.Checked = True Then
                    PrintOut = Nothing
                    If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                    PrintOut.txtPrintType.Text = "BasicFacilityReport"
                    PrintOut.txtAIRSNumber.Text = mtbAIRSNumber.Text
                    PrintOut.Show()
                    PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    Exit Sub
                End If
                If rdbFullReport.Checked = True Then
                    PrintOut = Nothing
                    If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                    PrintOut.txtPrintType.Text = "FullFacilityReport"
                    PrintOut.txtAIRSNumber.Text = mtbAIRSNumber.Text
                    PrintOut.txtStartDate.Text = DTPFullPrintStartDate.Text
                    PrintOut.txtEndDate.Text = DTPFullPrintEndDate.Text
                    PrintOut.Show()
                    PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    Exit Sub
                End If
                If rdbExtendedReport.Checked = True Then



                    Exit Sub
                End If


            Else
                MessageBox.Show("There is an invalid AIRS Number")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class