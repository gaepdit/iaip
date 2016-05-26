Imports System.Data.SqlClient

Public Class DMUDangerousTool
    Dim SQL As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim RecExist As Boolean

    Private Sub DMUDangerousTool_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub btnMoveAIRSData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMoveAIRSData.Click
        Try
            Dim OldAIRS As String
            Dim NewAIRS As String

            OldAIRS = txtOldAIRS.Text
            NewAIRS = txtNewAIRS.Text

            If OldAIRS <> "" And NewAIRS <> "" Then
                'Permitting 
                SQL = "Update SSPPApplicationMaster set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Compliance
                SQL = "Update SSCPItemMaster set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update SSCPFCEMaster set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select " &
                "strAIRSnumber " &
                "from SSCPInspectionsRequired " &
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then

                    SQL = "Update SSCPInspectionsRequired set " &
                    "strAIRSnumber = '0413" & NewAIRS & "' " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete SSCPInspectionsRequired " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Update SSCP_Enforcement set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strAIRSNumber " &
                "from SSCPDistrictResponsible " &
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update SSCPDistrictResponsible set " &
                    "strAIRSNumber = '0413" & NewAIRS & "' " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete SSCPDistrictResponsible " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Select strAIRSNumber " &
                "from SSCPDistrictAssignment " &
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update SSCPDistrictAssignment set " &
                    "strAIRSNumber = '0413" & NewAIRS & "' " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete SSCPDistrictAssignment " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If


                SQL = "Select strAIRSNumber " &
                              "from SSCPInspectionsRequired " &
                              "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update SSCPInspectionsRequired set " &
                    "strAIRSNumber = '0413" & NewAIRS & "' " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete SSCPInspectionsRequired " &
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Delete OLAPUserAccess " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Monitoring
                SQL = "Update ISMPMaster set " &
                "strAIRSNumber = '0413" & NewAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete ISMPFacilityAssignment " &
               "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Historical Tables
                SQL = "Update HB_APBHeaderData set " &
                "strAIRSNumber = '0413" & NewAIRS & "', " &
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update HB_APBFacilityInformation set " &
                "strAIRSNumber = '0413" & NewAIRS & "', " &
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete HB_APBAirProgramPollutants " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Header Data
                SQL = "Delete APBSupplamentalData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete APBSubpartData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AFSAIRPollutantData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete AFSFacilityData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete APBAirProgramPollutants " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete APBContactInformation " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete APBHeaderData " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete APBFacilityInformation " &
               "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete APBMasterAIRS " &
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
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
            cmd = New SqlCommand("PD_EIS_QASTART", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New SqlParameter("@AIRSNUMBER_IN", SqlDbType.VarChar)).Value = txtEISAIRSNumber.Text
            cmd.Parameters.Add(New SqlParameter("@INTYEAR_IN", SqlDbType.Decimal)).Value = txtEISYear.Text

            cmd.ExecuteNonQuery()

            MsgBox("done")
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

End Class