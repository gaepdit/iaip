Imports System.Data.OracleClient

Imports System.IO

Public Class SSCPManagerProfile

    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        Try
            Help.ShowHelp(Label1, "http://airpermit.dnr.state.ga.us/helpdocs/IAIP_help/")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TBSSPPPermitTrackingLog_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBSSPPPermitTrackingLog.ButtonClick
        Try
            Update_UC_Facility_Assignment_Profile()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub ClearSSCP_UC_Fac_Assignemnt()
        Try

            Me.chbAIRS_Number.Checked = True
            txtAIRS_Number.Text = "1"
            Me.chbCity.Checked = False
            txtCity.Text = ""
            Me.chbClass.Checked = False
            txtClassification.Text = ""
            Me.chbCounty.Checked = False
            txtCounty.Text = ""
            Me.chbDistrict.Checked = False
            txtDistrict.Text = ""
            Me.chbDistrict_Engineer.Checked = False
            txtDistrict_Engineer.Text = ""
            Me.chbDistrictResponsible.Checked = False
            txtDistrictResponsible.Text = ""
            Me.chbEngineer.Checked = False
            txtEngineer.Text = ""
            Me.chbFacility_Name.Checked = False
            txtFacility_Name.Text = ""
            Me.chbLast_FCE.Checked = False
            txtLast_FCE.Text = ""
            Me.chbLast_Inspection_Date.Checked = False
            txtLast_Inspection_Date.Text = ""
            Me.chbOperational_Status.Checked = False
            txtOperational_Status.Text = ""
            Me.chbSIC_Code.Checked = False
            txtSIC_Code.Text = ""
            Me.chbSSCP_Unit.Checked = False
            txtCheckBox_Count.Text = "1"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub Update_UC_Facility_Assignment_Profile()
        Dim SQL_Key As String

        Try


            Select Case txtCheckBox_Count.Text
                Case "0"
                    SQL_Key = 0
                Case "1"
                    SQL_Key = 1
                Case "2"
                    SQL_Key = 2
                Case "3"
                    SQL_Key = 3
                Case "4"
                    SQL_Key = 4
                Case "5"
                    SQL_Key = 5
                Case "6"
                    SQL_Key = 6
                Case "7"
                    SQL_Key = 7
                Case "8"
                    SQL_Key = 8
                Case "9"
                    SQL_Key = 9
                Case "10"
                    SQL_Key = "A"
                Case "11"
                    SQL_Key = "B"
                Case "12"
                    SQL_Key = "C"
                Case "13"
                    SQL_Key = "D"
                Case "14"
                    SQL_Key = "E"
                Case Else
                    SQL_Key = 0
            End Select

            Select Case txtAIRS_Number.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtCity.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtClassification.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtCounty.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtDistrict.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtDistrict_Engineer.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtDistrictResponsible.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtEngineer.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtFacility_Name.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtLast_FCE.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtLast_Inspection_Date.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtOperational_Status.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtSIC_Code.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtSSCP_Unit.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            SQL_Key = SQL_Key

            Dim DefaultsText As String = ""
            DefaultsText = ""
            If SQL_Key <> "" Then
                temp = "SSCPProfile-" & SQL_Key & "-eliforPPCSS"
            Else
                temp = "SSCPProfile-313000000200000-eliforPPCSS"
            End If

            If File.Exists("C:\APB\Defaults.txt") Then
                Dim reader As StreamReader = New StreamReader("C:\APB\Defaults.txt")
                Do
                    DefaultsText = DefaultsText & reader.ReadLine
                Loop Until reader.Peek = -1
                reader.Close()

                If DefaultsText.IndexOf("SSCPProfile-") <> -1 Then
                    DefaultsText = DefaultsText.Replace(Mid(DefaultsText, ((DefaultsText.IndexOf("SSCPProfile-")) + 1), (DefaultsText.IndexOf("-eliforPPCSS")) + 12), temp)
                Else
                    DefaultsText = DefaultsText & vbCrLf & temp
                End If

                Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.OpenOrCreate, FileAccess.Write)
                fs.Close()
                Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
                writer.WriteLine(DefaultsText)
                writer.Close()
            Else
                DefaultsText = temp
                Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.Create, IO.FileAccess.Write)
                fs.Close()
                Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
                writer.WriteLine(DefaultsText)
                writer.Close()
            End If

            If ManagersTools Is Nothing Then
            Else
                ManagersTools.lblProfileCode.Text = SQL_Key
            End If
            MsgBox("done", MsgBoxStyle.Information, "Profile Tool")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#Region "Click Actions for UC Facility Assignment Tab"
    Private Sub Click_Action_Remove(ByVal temp As Integer)
        'Dim temp As Integer = txtCheckBox_Count.Text

        Try


            Dim temp2 As Integer

            If txtAIRS_Number.Text <> "" Then
                temp2 = txtAIRS_Number.Text
                If temp2 >= temp Then
                    txtAIRS_Number.Text = txtAIRS_Number.Text - 1
                End If
            End If
            If txtCity.Text <> "" Then
                temp2 = txtCity.Text
                If temp2 >= temp Then
                    txtCity.Text = txtCity.Text - 1
                End If
            End If
            If txtClassification.Text <> "" Then
                temp2 = txtClassification.Text
                If temp2 >= temp Then
                    txtClassification.Text = txtClassification.Text - 1
                End If
            End If
            If txtCounty.Text <> "" Then
                temp2 = txtCounty.Text
                If temp2 >= temp Then
                    txtCounty.Text = txtCounty.Text - 1
                End If
            End If
            If txtDistrict.Text <> "" Then
                temp2 = txtDistrict.Text
                If temp2 >= temp Then
                    txtDistrict.Text = txtDistrict.Text - 1
                End If
            End If
            If txtDistrict_Engineer.Text <> "" Then
                temp2 = txtDistrict_Engineer.Text
                If temp2 >= temp Then
                    txtDistrict_Engineer.Text = txtDistrict_Engineer.Text - 1
                End If
            End If
            If txtDistrictResponsible.Text <> "" Then
                temp2 = txtDistrictResponsible.Text
                If temp2 >= temp Then
                    txtDistrictResponsible.Text = txtDistrictResponsible.Text - 1
                End If
            End If
            If txtEngineer.Text <> "" Then
                temp2 = txtEngineer.Text
                If temp2 >= temp Then
                    txtEngineer.Text = txtEngineer.Text - 1
                End If
            End If
            If txtFacility_Name.Text <> "" Then
                temp2 = txtFacility_Name.Text
                If temp2 >= temp Then
                    txtFacility_Name.Text = txtFacility_Name.Text - 1
                End If
            End If
            If txtLast_FCE.Text <> "" Then
                temp2 = txtLast_FCE.Text
                If temp2 >= temp Then
                    txtLast_FCE.Text = txtLast_FCE.Text - 1
                End If
            End If
            If txtLast_Inspection_Date.Text <> "" Then
                temp2 = txtLast_Inspection_Date.Text
                If temp2 >= temp Then
                    txtLast_Inspection_Date.Text = txtLast_Inspection_Date.Text - 1
                End If
            End If
            If txtOperational_Status.Text <> "" Then
                temp2 = txtOperational_Status.Text
                If temp2 >= temp Then
                    txtOperational_Status.Text = txtOperational_Status.Text - 1
                End If
            End If
            If txtSIC_Code.Text <> "" Then
                temp2 = txtSIC_Code.Text
                If temp2 >= temp Then
                    txtSIC_Code.Text = txtSIC_Code.Text - 1
                End If
            End If
            If txtSSCP_Unit.Text <> "" Then
                temp2 = txtSSCP_Unit.Text
                If temp2 >= temp Then
                    txtSSCP_Unit.Text = txtSSCP_Unit.Text - 1
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAIRS_Number_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbAIRS_Number.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try


            If chbAIRS_Number.Checked = True Then
                Select Case temp
                    Case "0"
                        txtAIRS_Number.Text = 1
                    Case "1"
                        txtAIRS_Number.Text = 2
                    Case "2"
                        txtAIRS_Number.Text = 3
                    Case "3"
                        txtAIRS_Number.Text = 4
                    Case "4"
                        txtAIRS_Number.Text = 5
                    Case "5"
                        txtAIRS_Number.Text = 6
                    Case "6"
                        txtAIRS_Number.Text = 7
                    Case "7"
                        txtAIRS_Number.Text = 8
                    Case "8"
                        txtAIRS_Number.Text = 9
                    Case "9"
                        txtAIRS_Number.Text = 10
                    Case "10"
                        txtAIRS_Number.Text = 11
                    Case "11"
                        txtAIRS_Number.Text = 12
                    Case "12"
                        txtAIRS_Number.Text = 13
                    Case "13"
                        txtAIRS_Number.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "13"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "12"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "11"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "10"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "9"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "8"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "7"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "6"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "5"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "4"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "3"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "2"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "1"
                        txtAIRS_Number.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbCity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbCity.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbCity.Checked = True Then
                Select Case temp
                    Case "0"
                        txtCity.Text = 1
                    Case "1"
                        txtCity.Text = 2
                    Case "2"
                        txtCity.Text = 3
                    Case "3"
                        txtCity.Text = 4
                    Case "4"
                        txtCity.Text = 5
                    Case "5"
                        txtCity.Text = 6
                    Case "6"
                        txtCity.Text = 7
                    Case "7"
                        txtCity.Text = 8
                    Case "8"
                        txtCity.Text = 9
                    Case "9"
                        txtCity.Text = 10
                    Case "10"
                        txtCity.Text = 11
                    Case "11"
                        txtCity.Text = 12
                    Case "12"
                        txtCity.Text = 13
                    Case "13"
                        txtCity.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "13"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "12"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "11"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "10"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "9"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "8"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "7"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "6"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "5"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "4"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "3"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "2"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "1"
                        txtCity.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbClass_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbClass.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbClass.Checked = True Then
                Select Case temp
                    Case "0"
                        txtClassification.Text = 1
                    Case "1"
                        txtClassification.Text = 2
                    Case "2"
                        txtClassification.Text = 3
                    Case "3"
                        txtClassification.Text = 4
                    Case "4"
                        txtClassification.Text = 5
                    Case "5"
                        txtClassification.Text = 6
                    Case "6"
                        txtClassification.Text = 7
                    Case "7"
                        txtClassification.Text = 8
                    Case "8"
                        txtClassification.Text = 9
                    Case "9"
                        txtClassification.Text = 10
                    Case "10"
                        txtClassification.Text = 11
                    Case "11"
                        txtClassification.Text = 12
                    Case "12"
                        txtClassification.Text = 13
                    Case "13"
                        txtClassification.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "13"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "12"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "11"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "10"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "9"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "8"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "7"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "6"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "5"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "4"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "3"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "2"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "1"
                        txtClassification.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbCounty_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbCounty.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbCounty.Checked = True Then
                Select Case temp
                    Case "0"
                        txtCounty.Text = 1
                    Case "1"
                        txtCounty.Text = 2
                    Case "2"
                        txtCounty.Text = 3
                    Case "3"
                        txtCounty.Text = 4
                    Case "4"
                        txtCounty.Text = 5
                    Case "5"
                        txtCounty.Text = 6
                    Case "6"
                        txtCounty.Text = 7
                    Case "7"
                        txtCounty.Text = 8
                    Case "8"
                        txtCounty.Text = 9
                    Case "9"
                        txtCounty.Text = 10
                    Case "10"
                        txtCounty.Text = 11
                    Case "11"
                        txtCounty.Text = 12
                    Case "12"
                        txtCounty.Text = 13
                    Case "13"
                        txtCounty.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "13"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "12"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "11"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "10"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "9"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "8"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "7"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "6"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "5"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "4"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "3"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "2"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "1"
                        txtCounty.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbDistrict_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbDistrict.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbDistrict.Checked = True Then
                Select Case temp
                    Case "0"
                        txtDistrict.Text = 1
                    Case "1"
                        txtDistrict.Text = 2
                    Case "2"
                        txtDistrict.Text = 3
                    Case "3"
                        txtDistrict.Text = 4
                    Case "4"
                        txtDistrict.Text = 5
                    Case "5"
                        txtDistrict.Text = 6
                    Case "6"
                        txtDistrict.Text = 7
                    Case "7"
                        txtDistrict.Text = 8
                    Case "8"
                        txtDistrict.Text = 9
                    Case "9"
                        txtDistrict.Text = 10
                    Case "10"
                        txtDistrict.Text = 11
                    Case "11"
                        txtDistrict.Text = 12
                    Case "12"
                        txtDistrict.Text = 13
                    Case "13"
                        txtDistrict.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "13"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "12"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "11"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "10"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "9"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "8"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "7"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "6"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "5"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "4"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "3"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "2"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "1"
                        txtDistrict.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbDistrict_Engineer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbDistrict_Engineer.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbDistrict_Engineer.Checked = True Then
                Select Case temp
                    Case "0"
                        txtDistrict_Engineer.Text = 1
                    Case "1"
                        txtDistrict_Engineer.Text = 2
                    Case "2"
                        txtDistrict_Engineer.Text = 3
                    Case "3"
                        txtDistrict_Engineer.Text = 4
                    Case "4"
                        txtDistrict_Engineer.Text = 5
                    Case "5"
                        txtDistrict_Engineer.Text = 6
                    Case "6"
                        txtDistrict_Engineer.Text = 7
                    Case "7"
                        txtDistrict_Engineer.Text = 8
                    Case "8"
                        txtDistrict_Engineer.Text = 9
                    Case "9"
                        txtDistrict_Engineer.Text = 10
                    Case "10"
                        txtDistrict_Engineer.Text = 11
                    Case "11"
                        txtDistrict_Engineer.Text = 12
                    Case "12"
                        txtDistrict_Engineer.Text = 13
                    Case "13"
                        txtDistrict_Engineer.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "13"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "12"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "11"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "10"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "9"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "8"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "7"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "6"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "5"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "4"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "3"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "2"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "1"
                        txtDistrict_Engineer.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbDistrictResponsible_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbDistrictResponsible.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbDistrictResponsible.Checked = True Then
                Select Case temp
                    Case "0"
                        txtDistrictResponsible.Text = 1
                    Case "1"
                        txtDistrictResponsible.Text = 2
                    Case "2"
                        txtDistrictResponsible.Text = 3
                    Case "3"
                        txtDistrictResponsible.Text = 4
                    Case "4"
                        txtDistrictResponsible.Text = 5
                    Case "5"
                        txtDistrictResponsible.Text = 6
                    Case "6"
                        txtDistrictResponsible.Text = 7
                    Case "7"
                        txtDistrictResponsible.Text = 8
                    Case "8"
                        txtDistrictResponsible.Text = 9
                    Case "9"
                        txtDistrictResponsible.Text = 10
                    Case "10"
                        txtDistrictResponsible.Text = 11
                    Case "11"
                        txtDistrictResponsible.Text = 12
                    Case "12"
                        txtDistrictResponsible.Text = 13
                    Case "13"
                        txtDistrictResponsible.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "13"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "12"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "11"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "10"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "9"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "8"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "7"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "6"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "5"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "4"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "3"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "2"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "1"
                        txtDistrictResponsible.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbEngineer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbEngineer.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbEngineer.Checked = True Then
                Select Case temp
                    Case "0"
                        txtEngineer.Text = 1
                    Case "1"
                        txtEngineer.Text = 2
                    Case "2"
                        txtEngineer.Text = 3
                    Case "3"
                        txtEngineer.Text = 4
                    Case "4"
                        txtEngineer.Text = 5
                    Case "5"
                        txtEngineer.Text = 6
                    Case "6"
                        txtEngineer.Text = 7
                    Case "7"
                        txtEngineer.Text = 8
                    Case "8"
                        txtEngineer.Text = 9
                    Case "9"
                        txtEngineer.Text = 10
                    Case "10"
                        txtEngineer.Text = 11
                    Case "11"
                        txtEngineer.Text = 12
                    Case "12"
                        txtEngineer.Text = 13
                    Case "13"
                        txtEngineer.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "13"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "12"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "11"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "10"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "9"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "8"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "7"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "6"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "5"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "4"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "3"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "2"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "1"
                        txtEngineer.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbFacility_Name_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbFacility_Name.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbFacility_Name.Checked = True Then
                Select Case temp
                    Case "0"
                        txtFacility_Name.Text = 1
                    Case "1"
                        txtFacility_Name.Text = 2
                    Case "2"
                        txtFacility_Name.Text = 3
                    Case "3"
                        txtFacility_Name.Text = 4
                    Case "4"
                        txtFacility_Name.Text = 5
                    Case "5"
                        txtFacility_Name.Text = 6
                    Case "6"
                        txtFacility_Name.Text = 7
                    Case "7"
                        txtFacility_Name.Text = 8
                    Case "8"
                        txtFacility_Name.Text = 9
                    Case "9"
                        txtFacility_Name.Text = 10
                    Case "10"
                        txtFacility_Name.Text = 11
                    Case "11"
                        txtFacility_Name.Text = 12
                    Case "12"
                        txtFacility_Name.Text = 13
                    Case "13"
                        txtFacility_Name.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "13"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "12"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "11"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "10"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "9"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "8"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "7"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "6"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "5"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "4"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "3"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "2"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "1"
                        txtFacility_Name.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbLast_FCE_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbLast_FCE.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbLast_FCE.Checked = True Then
                Select Case temp
                    Case "0"
                        txtLast_FCE.Text = 1
                    Case "1"
                        txtLast_FCE.Text = 2
                    Case "2"
                        txtLast_FCE.Text = 3
                    Case "3"
                        txtLast_FCE.Text = 4
                    Case "4"
                        txtLast_FCE.Text = 5
                    Case "5"
                        txtLast_FCE.Text = 6
                    Case "6"
                        txtLast_FCE.Text = 7
                    Case "7"
                        txtLast_FCE.Text = 8
                    Case "8"
                        txtLast_FCE.Text = 9
                    Case "9"
                        txtLast_FCE.Text = 10
                    Case "10"
                        txtLast_FCE.Text = 11
                    Case "11"
                        txtLast_FCE.Text = 12
                    Case "12"
                        txtLast_FCE.Text = 13
                    Case "13"
                        txtLast_FCE.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "13"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "12"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "11"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "10"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "9"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "8"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "7"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "6"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "5"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "4"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "3"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "2"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "1"
                        txtLast_FCE.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbLast_Inspection_Date_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbLast_Inspection_Date.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbLast_Inspection_Date.Checked = True Then
                Select Case temp
                    Case "0"
                        txtLast_Inspection_Date.Text = 1
                    Case "1"
                        txtLast_Inspection_Date.Text = 2
                    Case "2"
                        txtLast_Inspection_Date.Text = 3
                    Case "3"
                        txtLast_Inspection_Date.Text = 4
                    Case "4"
                        txtLast_Inspection_Date.Text = 5
                    Case "5"
                        txtLast_Inspection_Date.Text = 6
                    Case "6"
                        txtLast_Inspection_Date.Text = 7
                    Case "7"
                        txtLast_Inspection_Date.Text = 8
                    Case "8"
                        txtLast_Inspection_Date.Text = 9
                    Case "9"
                        txtLast_Inspection_Date.Text = 10
                    Case "10"
                        txtLast_Inspection_Date.Text = 11
                    Case "11"
                        txtLast_Inspection_Date.Text = 12
                    Case "12"
                        txtLast_Inspection_Date.Text = 13
                    Case "13"
                        txtLast_Inspection_Date.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "13"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "12"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "11"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "10"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "9"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "8"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "7"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "6"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "5"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "4"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "3"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "2"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "1"
                        txtLast_Inspection_Date.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbOperational_Status_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbOperational_Status.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbOperational_Status.Checked = True Then
                Select Case temp
                    Case "0"
                        txtOperational_Status.Text = 1
                    Case "1"
                        txtOperational_Status.Text = 2
                    Case "2"
                        txtOperational_Status.Text = 3
                    Case "3"
                        txtOperational_Status.Text = 4
                    Case "4"
                        txtOperational_Status.Text = 5
                    Case "5"
                        txtOperational_Status.Text = 6
                    Case "6"
                        txtOperational_Status.Text = 7
                    Case "7"
                        txtOperational_Status.Text = 8
                    Case "8"
                        txtOperational_Status.Text = 9
                    Case "9"
                        txtOperational_Status.Text = 10
                    Case "10"
                        txtOperational_Status.Text = 11
                    Case "11"
                        txtOperational_Status.Text = 12
                    Case "12"
                        txtOperational_Status.Text = 13
                    Case "13"
                        txtOperational_Status.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "13"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "12"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "11"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "10"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "9"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "8"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "7"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "6"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "5"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "4"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "3"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "2"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "1"
                        txtOperational_Status.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbSIC_Code_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbSIC_Code.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try


            If chbSIC_Code.Checked = True Then
                Select Case temp
                    Case "0"
                        txtSIC_Code.Text = 1
                    Case "1"
                        txtSIC_Code.Text = 2
                    Case "2"
                        txtSIC_Code.Text = 3
                    Case "3"
                        txtSIC_Code.Text = 4
                    Case "4"
                        txtSIC_Code.Text = 5
                    Case "5"
                        txtSIC_Code.Text = 6
                    Case "6"
                        txtSIC_Code.Text = 7
                    Case "7"
                        txtSIC_Code.Text = 8
                    Case "8"
                        txtSIC_Code.Text = 9
                    Case "9"
                        txtSIC_Code.Text = 10
                    Case "10"
                        txtSIC_Code.Text = 11
                    Case "11"
                        txtSIC_Code.Text = 12
                    Case "12"
                        txtSIC_Code.Text = 13
                    Case "13"
                        txtSIC_Code.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "13"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "12"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "11"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "10"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "9"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "8"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "7"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "6"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "5"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "4"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "3"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "2"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "1"
                        txtSIC_Code.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbSSCP_Unit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbSSCP_Unit.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try

            If chbSSCP_Unit.Checked = True Then
                Select Case temp
                    Case "0"
                        txtSSCP_Unit.Text = 1
                    Case "1"
                        txtSSCP_Unit.Text = 2
                    Case "2"
                        txtSSCP_Unit.Text = 3
                    Case "3"
                        txtSSCP_Unit.Text = 4
                    Case "4"
                        txtSSCP_Unit.Text = 5
                    Case "5"
                        txtSSCP_Unit.Text = 6
                    Case "6"
                        txtSSCP_Unit.Text = 7
                    Case "7"
                        txtSSCP_Unit.Text = 8
                    Case "8"
                        txtSSCP_Unit.Text = 9
                    Case "9"
                        txtSSCP_Unit.Text = 10
                    Case "10"
                        txtSSCP_Unit.Text = 11
                    Case "11"
                        txtSSCP_Unit.Text = 12
                    Case "12"
                        txtSSCP_Unit.Text = 13
                    Case "13"
                        txtSSCP_Unit.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "13"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "12"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "11"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "10"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "9"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "8"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "7"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "6"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "5"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "4"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "3"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "2"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "1"
                        txtSSCP_Unit.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#End Region



End Class