Imports Oracle.ManagedDataAccess.Client
Imports System.Data.OleDb
'Imports System.Data.Odbc

Public Class DMUDangerousTool
    Dim SQL, SQL2 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim RecExist As Boolean
    
    Private Sub DMUTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Private Sub btnDeleteEnforcement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEnforcement.Click
        Try
            Dim result As String
            Dim tempAIRS As String = ""
            If txtEnforcementNumber.Text <> "" Then
                result = InputBox("Are you sure you want to delete this Enforcement?", "Enforcement Delete", "False")
                Select Case result
                    Case "True"
                        SQL = "Select strAIRSNumber " & _
                        "from AIRBRANCH.SSCP_AuditedEnforcement " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("strAIRSNumber")) Then
                                tempAIRS = ""
                            Else
                                tempAIRS = dr.Item("strAIRSNumber")
                            End If
                        End While
                        dr.Close()

                        SQL = "delete AIRBRANCH.sscpenforcementStipulated " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                            "values " & _
                            "(" & _
                            "(select " & _
                            "case when max(numCounter) is null then 1 " & _
                            "else max(numCounter) + 1 " & _
                            "end numCounter " & _
                            "from AIRBRANCH.AFSDeletions), " & _
                            "'" & tempAIRS & "', " & _
                            "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                            "'" & OracleDate & "', '', " & _
                            "'') "

                        cmd = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "delete AIRBRANCH.sscp_Auditedenforcement  " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                           "values " & _
                           "(" & _
                           "(select " & _
                           "case when max(numCounter) is null then 1 " & _
                           "else max(numCounter) + 1 " & _
                           "end numCounter " & _
                           "from AIRBRANCH.AFSDeletions), " & _
                           "'" & tempAIRS & "', " & _
                           "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                           "'" & OracleDate & "', '', " & _
                           "'') "

                        SQL = "delete AIRBRANCH.AFSSSCPEnforcementRecords " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into AIRBRANCH.AFSDeletions " & _
                           "values " & _
                           "(" & _
                           "(select " & _
                           "case when max(numCounter) is null then 1 " & _
                           "else max(numCounter) + 1 " & _
                           "end numCounter " & _
                           "from AIRBRANCH.AFSDeletions), " & _
                           "'" & tempAIRS & "', " & _
                           "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                           "'" & OracleDate & "', '', " & _
                           "'') "

                        cmd = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        MsgBox("Deleted", MsgBoxStyle.Information, "DMU Tool")
                    Case "False"
                        MsgBox("False")
                    Case Else
                        MsgBox("Other")
                End Select

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnMoveAIRSData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveAIRSData.Click
        Try
            Dim OldAIRS As String
            Dim NewAIRS As String

            OldAIRS = txtOldAIRS.Text
            NewAIRS = txtNewAIRS.Text

            If OldAIRS <> "" And NewAIRS <> "" Then
                'Permitting 
                SQL = "Update AIRBRANCH.SSPPApplicationMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Compliance
                SQL = "Update AIRBRANCH.SSCPItemMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update AIRBRANCH.SSCPFCEMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select " & _
                "strAIRSnumber " & _
                "from AIRBRANCH.SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then

                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "strAIRSnumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete AIRBRANCH.SSCPInspectionsRequired " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Update AIRBRANCH.SSCP_Enforcement set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update AIRBRANCH.SSCPDistrictResponsible set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete AIRBRANCH.SSCPDistrictResponsible " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Select strAIRSNumber " & _
                "from AIRBRANCH.SSCPDistrictAssignment " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update AIRBRANCH.SSCPDistrictAssignment set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete AIRBRANCH.SSCPDistrictAssignment " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If


                SQL = "Select strAIRSNumber " & _
                              "from AIRBRANCH.SSCPInspectionsRequired " & _
                              "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete AIRBRANCH.SSCPInspectionsRequired " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Delete AIRBRANCH.OLAPUserAccess " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Monitoring
                SQL = "Update AIRBRANCH.ISMPMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.ISMPFacilityAssignment " & _
               "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Historical Tables
                SQL = "Update AIRBRANCH.HB_APBHeaderData set " & _
                "strAIRSNumber = '0413" & NewAIRS & "', " & _
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update AIRBRANCH.HB_APBFacilityInformation set " & _
                "strAIRSNumber = '0413" & NewAIRS & "', " & _
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.HB_APBAirProgramPollutants " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Header Data
                SQL = "Delete AIRBRANCH.APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBSubpartData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.AFSAIRPollutantData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.AFSFacilityData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBAirProgramPollutants " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBContactInformation " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBHeaderData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBFacilityInformation " & _
               "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBMasterAIRS " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtOldAIRS.Clear()
                txtNewAIRS.Clear()

            End If

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub PD_EIS_QASTART_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PD_EIS_QASTART.Click
        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_EIS_QASTART", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("AIRSNUMBER_IN", OracleDbType.Varchar2)).Value = txtEISAIRSNumber.Text
            cmd.Parameters.Add(New OracleParameter("INTYEAR_IN", OracleDbType.Decimal)).Value = txtEISYear.Text

            cmd.ExecuteNonQuery()

            MsgBox("done")
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

End Class