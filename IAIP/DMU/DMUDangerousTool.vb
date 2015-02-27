Imports Oracle.DataAccess.Client
Imports System.Data.OleDb
'Imports System.Data.Odbc

Public Class DMUDangerousTool
    Dim SQL, SQL2 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim RecExist As Boolean
    
    Private Sub DMUTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
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
                        "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
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

                        SQL = "delete " & DBNameSpace & ".sscpenforcementStipulated " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                            "values " & _
                            "(" & _
                            "(select " & _
                            "case when max(numCounter) is null then 1 " & _
                            "else max(numCounter) + 1 " & _
                            "end numCounter " & _
                            "from " & DBNameSpace & ".AFSDeletions), " & _
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

                        SQL = "delete " & DBNameSpace & ".sscp_Auditedenforcement  " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                           "values " & _
                           "(" & _
                           "(select " & _
                           "case when max(numCounter) is null then 1 " & _
                           "else max(numCounter) + 1 " & _
                           "end numCounter " & _
                           "from " & DBNameSpace & ".AFSDeletions), " & _
                           "'" & tempAIRS & "', " & _
                           "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                           "'" & OracleDate & "', '', " & _
                           "'') "

                        SQL = "delete " & DBNameSpace & ".AFSSSCPEnforcementRecords " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                           "values " & _
                           "(" & _
                           "(select " & _
                           "case when max(numCounter) is null then 1 " & _
                           "else max(numCounter) + 1 " & _
                           "end numCounter " & _
                           "from " & DBNameSpace & ".AFSDeletions), " & _
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
            ErrorReport(ex, Me.Name & ".btnDeleteEnforcement_Click")
        Finally
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
                SQL = "Update " & DBNameSpace & ".SSPPApplicationMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Compliance
                SQL = "Update " & DBNameSpace & ".SSCPItemMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update " & DBNameSpace & ".SSCPFCEMaster set " & _
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
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then

                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "strAIRSnumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Update " & DBNameSpace & ".SSCP_Enforcement set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update " & DBNameSpace & ".SSCPDistrictResponsible set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete " & DBNameSpace & ".SSCPDistrictResponsible " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPDistrictAssignment " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update " & DBNameSpace & ".SSCPDistrictAssignment set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete " & DBNameSpace & ".SSCPDistrictAssignment " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If


                SQL = "Select strAIRSNumber " & _
                              "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                              "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Delete " & DBNameSpace & ".OLAPUserAccess " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Monitoring
                SQL = "Update " & DBNameSpace & ".ISMPMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".ISMPFacilityAssignment " & _
               "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Historical Tables
                SQL = "Update " & DBNameSpace & ".HB_APBHeaderData set " & _
                "strAIRSNumber = '0413" & NewAIRS & "', " & _
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update " & DBNameSpace & ".HB_APBFacilityInformation set " & _
                "strAIRSNumber = '0413" & NewAIRS & "', " & _
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".HB_APBAirProgramPollutants " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Header Data
                SQL = "Delete " & DBNameSpace & ".APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBSubpartData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".AFSAIRPollutantData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".AFSFacilityData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBAirProgramPollutants " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBContactInformation " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBHeaderData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBFacilityInformation " & _
               "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBMasterAIRS " & _
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

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub

End Class