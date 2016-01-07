Imports Oracle.ManagedDataAccess.Client

Public Class DMUDangerousTool
    Dim SQL, SQL2 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim RecExist As Boolean

    Private Sub DMUDangerousTool_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
    End Sub

    Private Sub btnMoveAIRSData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMoveAIRSData.Click
        Try
            Dim OldAIRS As String
            Dim NewAIRS As String

            OldAIRS = txtOldAIRS.Text
            NewAIRS = txtNewAIRS.Text

            If OldAIRS <> "" And NewAIRS <> "" Then
                'Permitting 
                SQL = "Update AIRBRANCH.SSPPApplicationMaster set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Compliance
                SQL = "Update AIRBRANCH.SSCPItemMaster set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update AIRBRANCH.SSCPFCEMaster set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select " &
                "strAIRSnumber " &
                "from AIRBRANCH.SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then

                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " &
                    "strAIRSnumber = '0413" & NewAIRS & "' " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete AIRBRANCH.SSCPInspectionsRequired " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Update AIRBRANCH.SSCP_Enforcement set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strAIRSNumber " &
                "from AIRBRANCH.SSCPDistrictResponsible " &
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update AIRBRANCH.SSCPDistrictResponsible set " &
                    "strAIRSNumber = '0413" & NewAIRS & "' " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete AIRBRANCH.SSCPDistrictResponsible " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Select strAIRSNumber " &
                "from AIRBRANCH.SSCPDistrictAssignment " &
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update AIRBRANCH.SSCPDistrictAssignment set " &
                    "strAIRSNumber = '0413" & NewAIRS & "' " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete AIRBRANCH.SSCPDistrictAssignment " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If


                SQL = "Select strAIRSNumber " &
                              "from AIRBRANCH.SSCPInspectionsRequired " &
                              "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update AIRBRANCH.SSCPInspectionsRequired set " &
                    "strAIRSNumber = '0413" & NewAIRS & "' " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete AIRBRANCH.SSCPInspectionsRequired " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Delete AIRBRANCH.OLAPUserAccess " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Monitoring
                SQL = "Update AIRBRANCH.ISMPMaster set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.ISMPFacilityAssignment " &
               "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Historical Tables
                SQL = "Update AIRBRANCH.HB_APBHeaderData set " &
                "strAIRSNumber = '0413" & NewAIRS & "', " &
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update AIRBRANCH.HB_APBFacilityInformation set " &
                "strAIRSNumber = '0413" & NewAIRS & "', " &
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.HB_APBAirProgramPollutants " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Header Data
                SQL = "Delete AIRBRANCH.APBSupplamentalData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBSubpartData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.AFSAIRPollutantData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.AFSFacilityData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBAirProgramPollutants " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBContactInformation " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBHeaderData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBFacilityInformation " &
               "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AIRBRANCH.APBMasterAIRS " &
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