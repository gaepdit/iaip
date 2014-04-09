Module CrystalReports

#Region "Crystal Reports displayer"

    Public Sub DisplayReport(ByVal crReport As Object, ByVal TabText As String)
        Try
            crReport.DisplayGroupTree = True
            crReport.DisplayToolbar = True
            crReport.showrefreshbutton = False
            crReport.visible = True
            crReport.DisplayGroupTree = True

            Dim I As Integer
            Do While I < crReport.Controls.Count
                If TypeOf (crReport.Controls(I)) Is CrystalDecisions.Windows.Forms.PageView Then
                    Dim J As Integer
                    Do While J < crReport.Controls(I).Controls.Count
                        If CType(crReport.Controls(I).Controls(J), System.Windows.Forms.TabControl).TabPages.Count > 0 Then
                            'Change the tab text..
                            CType(crReport.Controls(I).Controls(J), System.Windows.Forms.TabControl).TabPages.Item(0).Text = TabText
                            Exit Do
                        End If
                    Loop
                    Exit Do
                Else
                    crReport.Controls(I).Visible = False
                End If
            Loop
        Catch ex As Exception
            ErrorReport(ex, "MRFunctions.DisplayReport")
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region

End Module
