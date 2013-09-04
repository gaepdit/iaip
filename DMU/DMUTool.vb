Imports System.Data.OracleClient
Imports System.Data.OleDb
'Imports System.Data.Odbc

Public Class DMUTool
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim RecExist As Boolean
    Dim dsTemp As DataSet
    Dim daTemp As OracleDataAdapter

    Private Sub DMUTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            Panel1.Text = "Enter SQL statement above..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".btnRunSQL_Click")
        Finally

        End Try

    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try
            DMUOnly.Dispose()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".btnRunSQL_Click")
        Finally

        End Try

    End Sub
    Private Sub btnClearSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSQL.Click
        Try
            txtSQL.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".btnRunSQL_Click")
        Finally

        End Try

    End Sub
    Private Sub btnRunSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunSQL.Click
        Try
            If txtSQL.Text <> "" Then
                SQL = txtSQL.Text
                'SQL = Replace(txtSQL.Text, "'", "''")
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                Try
                    dr = cmd.ExecuteReader
                    dr.Close()
                    txtSQL.Text = "SQL Statement Ran" & vbCrLf & vbCrLf & txtSQL.Text
                Catch ex As Exception
                    txtSQL.Text = "ERROR" & vbCrLf & ex.ToString() & vbCrLf & txtSQL.Text
                End Try
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".btnRunSQL_Click")
        Finally

        End Try

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

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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

                        cmd = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "delete " & DBNameSpace & ".sscp_Auditedenforcement  " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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

                        cmd = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & ".btnDeleteEnforcement_Click")
        Finally
        End Try
    End Sub
    Private Sub btnDeleteACC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteACC.Click
        Try
            Dim result As String
            Dim tempAIRS As String = ""

            If txtACCNumber.Text <> "" Then
                result = InputBox("Are you sure you want to delete this ACC?", "ACC Delete", "False")
                Select Case result
                    Case "True"
                        SQL = "Select strAIRSNumber " & _
                        "from " & DBNameSpace & ".SSCPItemMaster " & _
                        "where strTrackingNumber = '" & txtACCNumber.Text & "' "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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

                        SQL = "Delete " & DBNameSpace & ".AFSSSCPRecords " & _
                        "where strTrackingNumber = '" & txtACCNumber.Text & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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

                        cmd = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & DBNameSpace & ".SSCPACCSHistory " & _
                        "where strTrackingNumber = '" & txtACCNumber.Text & "' "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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

                        cmd = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & DBNameSpace & ".SSCPACCS " & _
                        "Where strTrackingNumber = '" & txtACCNumber.Text & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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

                        cmd = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & DBNameSpace & ".SSCPItemMaster " & _
                        "where strTrackingNumber = '" & txtACCNumber.Text & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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

                        cmd = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & ".btnDeleteACC_Click")
        Finally
        End Try

    End Sub
    Private Sub btnUpdateVersionNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateVersionNumber.Click
        Try
            SQL = "Update " & DBNameSpace & ".APBMasterApp set " & _
            "strVersionNumber = '" & Replace(mtbVersionNumber.Text, "'", "''") & "' " & _
            "where strApplicationName = 'IAIP' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            MsgBox("Version Number Updated.", MsgBoxStyle.Information, Me.Name)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & ".btnUpdateVersionNumber_Click")
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Exit Sub

            'Dim temp2 As String
            'SQL = "Select strApplicationNumber, datPermitIssued " & _
            '"from AIRBranch.SSPPApplicationTracking, airbranch.apbpermits " & _
            '"where AIRBranch.SSPPApplicationTracking.strApplicationNumber = substr(airbranch.APBPermits.strFileName, 4) " & _
            '"and datFinalOnWeb is null "

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    temp = ""
            '    temp2 = ""
            '    If IsDBNull(dr.Item("datPermitIssued")) Then
            '        temp = ""
            '        temp2 = ""
            '    Else
            '        temp = dr.Item("datPermitIssued")
            '        temp2 = dr.Item("strApplicatioNNumber")
            '    End If

            '    If temp2 <> "" Then
            '        temp = Format(CDate(temp), "dd-MMM-yyyy")
            '        SQL = "Update AIRBranch.SSPPApplicationTracking set " & _
            '        "datFinalOnWeb = '" & temp & "' " & _
            '        "where strApplicationNumber = '" & temp2 & "' "

            '        cmd2 = New OracleCommand(SQL, conn)
            '        If conn.State = ConnectionState.Closed Then
            '            conn.Open()
            '        End If
            '        dr2 = cmd2.ExecuteReader
            '        dr2.Close()
            '    End If



            'End While
            'dr.Close()


        Catch ex As Exception

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

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Compliance
                SQL = "Update " & DBNameSpace & ".SSCPItemMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update " & DBNameSpace & ".SSCPFCEMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'SQL = "Select strAIRSNumber " & _
                '"from " & DBNameSpace & ".SSCPFacilityAssignment " & _
                '"where strAIRSNumber = '0413" & NewAIRS & "' "

                SQL = "Select " & _
                "strAIRSnumber " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    'SQL = "Update " & DBNameSpace & ".SSCPFacilityAssignment set " & _
                    '"strAIRSNumber = '0413" & NewAIRS & "' " & _
                    '"where strAIRSNumber = '0413" & OldAIRS & "' "

                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "strAIRSnumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    'SQL = "Delete " & DBNameSpace & ".SSCPFacilityAssignment " & _
                    '"where strAIRSNumber = '0413" & OldAIRS & "' "

                    SQL = "Delete " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Update " & DBNameSpace & ".SSCP_Enforcement set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update " & DBNameSpace & ".SSCPDistrictResponsible set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete " & DBNameSpace & ".SSCPDistrictResponsible " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "Select strAIRSNumber " & _
                "from " & DBNameSpace & ".SSCPDistrictAssignment " & _
                "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update " & DBNameSpace & ".SSCPDistrictAssignment set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete " & DBNameSpace & ".SSCPDistrictAssignment " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If


                SQL = "Select strAIRSNumber " & _
                              "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                              "where strAIRSNumber = '0413" & NewAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                RecExist = dr.Read
                dr.Close()
                If RecExist = False Then
                    SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                    "strAIRSNumber = '0413" & NewAIRS & "' " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Delete " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    "where strAIRSNumber = '0413" & OldAIRS & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If





                SQL = "Delete " & DBNameSpace & ".OLAPUserAccess " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Monitoring
                SQL = "Update " & DBNameSpace & ".ISMPMaster set " & _
                "strAIRSNumber = '0413" & NewAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".ISMPFacilityAssignment " & _
               "where strAIRSNumber = '0413" & OldAIRS & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Historical Tables
                SQL = "Update " & DBNameSpace & ".HB_APBHeaderData set " & _
                "strAIRSNumber = '0413" & NewAIRS & "', " & _
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update " & DBNameSpace & ".HB_APBFacilityInformation set " & _
                "strAIRSNumber = '0413" & NewAIRS & "', " & _
                "strComments = 'Data Merged from old AIRS # " & OldAIRS & "' " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".HB_APBAirProgramPollutants " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'Header Data
                SQL = "Delete " & DBNameSpace & ".APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBSubpartData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".AFSAIRPollutantData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".AFSFacilityData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBAirProgramPollutants " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBContactInformation " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBHeaderData " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBFacilityInformation " & _
               "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".APBMasterAIRS " & _
                "where strAIRSNumber = '0413" & OldAIRS & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
    Private Sub btnFixInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFixInvoice.Click
        Try
            Dim payID As String = ""
            Exit Sub

            SQL = "Select intPayID " & _
            "from AIRBranch.FSAddPaid, AIRBranch.FSPayAndSubmit " & _
            "where AIRBranch.FSAddPaid.strAIRSNumber = AIRBranch.FSPayAndSubmit.strAIRSNumber " & _
            "and AIRbranch.FSAddPaid.intYear = AIRbranch.FSPayAndSubmit.intYear " & _
            "and AIRbranch.FSPayAndSubmit.strPaymentType = 'Entire Annual Year' " & _
            "and AirBranch.FSAddPaid.intYear = '2007' " & _
            "and (strInvoiceNo like '%Q3%' or strInvoiceNo like '%Q4%') " & _
            "and datPayDate is null "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("intPayID")) Then
                    payID = ""
                Else
                    payID = dr.Item("intPayID")
                End If
                If payID <> "" Then
                    SQL = "Delete AIRBranch.FSAddPaid " & _
                    "where intPayID = '" & payID & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnFixStacks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFixStacks.Click
        Try
            Dim DevConn As New OracleConnection("Data Source = DEV; User ID = AirBranch; Password = " & SimpleCrypt("óíïçáìåòô") & ";")
            Dim StateFacilityIdentifier As String = ""
            Dim EmissionReleasePointID As String = ""
            Dim sngStackHeight As String = ""
            Dim sngStackDiameter As String = ""

            SQL = "Select " & _
            "strInventoryYear, strStateFacilityIdentifier, " & _
            "strEmissionReleasePointID, " & _
            "sngStackHeight, sngStackDiameter " & _
            "from EIER " & _
            "where strInventoryYear = '2008' " & _
            "and sngStackDiameter > sngStackHeight "

            cmd = New OracleCommand(SQL, DevConn)
            If DevConn.State = ConnectionState.Closed Then
                DevConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strStateFacilityIdentifier")) Then
                    StateFacilityIdentifier = ""
                Else
                    StateFacilityIdentifier = dr.Item("strStateFacilityIdentifier")
                End If
                If StateFacilityIdentifier <> "" Then
                    If IsDBNull(dr.Item("strEmissionReleasePointID")) Then
                        EmissionReleasePointID = ""
                    Else
                        EmissionReleasePointID = dr.Item("strEmissionReleasePointID")
                    End If
                    If IsDBNull(dr.Item("sngStackHeight")) Then
                        sngStackDiameter = ""
                    Else
                        sngStackDiameter = dr.Item("sngStackHeight")
                    End If
                    If IsDBNull(dr.Item("sngStackDiameter")) Then
                        sngStackHeight = ""
                    Else
                        sngStackHeight = dr.Item("sngStackDiameter")
                    End If
                    SQL = "update " & DBNameSpace & ".EIER set " & _
                    "sngStackHeight = '" & sngStackHeight & "', " & _
                    "sngStackDiameter = '" & sngStackDiameter & "' " & _
                    "where strStateFacilityIdentifier = '" & StateFacilityIdentifier & "' " & _
                    "and strEmissionReleasePointID = '" & EmissionReleasePointID & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            MsgBox("Finished", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUpdateComplianceContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateComplianceContacts.Click
        Try
            Dim GATVConn As Object = ""
            Dim GATVcmd As Object = ""
            Dim GATVdr As Object = ""

            Dim AIRSNumber As String = ""
            'Dim AppNumber As String = ""
            Dim ContactName As String = ""
            Dim ContactFirstName As String = ""
            Dim ContactLastName As String = ""
            Dim ContactPrefix As String = ""
            Dim ContactSuffix As String = ""
            Dim ContactTitle As String = ""
            Dim ContactCompany As String = ""
            Dim ContactPhone As String = ""
            Dim ContactFax As String = ""
            Dim ContactEmail As String = ""
            Dim ContactAddress As String = ""
            Dim ContactCity As String = ""
            Dim ContactState As String = ""
            Dim ContactZipCode As String = ""
            Dim ContactDescription As String = ""

            SQL = "Select " & _
            "substr(strAIRSNumber, 5) as strAIRSNumber  " & _
            "from " & DBNameSpace & ".APBHeaderData " & _
            "where substr(strAIRProgramCodes, 13, 1) = '1' " & _
            "and strOperationalStatus <> 'X' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr2.Item("strAIRSNumber")
                End If

                SQL = "SELECT " & _
                "tbl_ProjectManagement.ApplicationNumber, " & _
         "tbl_ProjectManagement.ProjectIdentifier, " & _
         "tblFacilityInformation_1_10_Contacts.ContactName, " & _
         "tblFacilityInformation_1_10_Contacts.ContactTitle, " & _
         "tblFacilityInformation_1_10_Contacts.ContactPhone, " & _
         "tblFacilityInformation_1_10_Contacts.ContactPhoneExt, " & _
         "tblFacilityInformation_1_10_Contacts.ContactFax, " & _
         "tblFacilityInformation_1_10_Contacts.ContactEMail, " & _
         "tblFacilityInformation_1_10_MailAddress.MailAddressCompany, " & _
         "tblFacilityInformation_1_10_MailAddress.MailAddressStreet, " & _
         "tblFacilityInformation_1_10_MailAddress.MailAddressCity, " & _
         "tblFacilityInformation_1_10_MailAddress.MailAddressState, " & _
         "tblFacilityInformation_1_10_MailAddress.MailingAddressZip " & _
         "FROM (tblFacilityInformation_1_10_MailAddress INNER JOIN (tblFacilityInformation_1_10_Contacts " & _
         "INNER JOIN tblFacilityInformation_1_10 " & _
         "ON (tblFacilityInformation_1_10_Contacts.ContactID = tblFacilityInformation_1_10.ContactEnforcement) " & _
         "AND (tblFacilityInformation_1_10_Contacts.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " & _
         "ON (tblFacilityInformation_1_10_MailAddress.MailAddressID = tblFacilityInformation_1_10.MailContactEnforcement) " & _
         "AND (tblFacilityInformation_1_10_MailAddress.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " & _
         "INNER JOIN tbl_ProjectManagement " & _
         "ON tblFacilityInformation_1_10.ProjectIdentifier = tbl_ProjectManagement.ProjectIdentifier " & _
         "WHERE (((tbl_ProjectManagement.ProjectIdentifier)=[tblFacilityInformation_1_10].[ProjectIdentifier]) " & _
         "AND ((tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_Contacts].[ProjectIdentifier] " & _
         "And (tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_MailAddress].[ProjectIdentifier]) " & _
         "AND ((tblFacilityInformation_1_10.ContactEnforcement)=[tblFacilityInformation_1_10_Contacts].[ContactID]) " & _
         "AND ((tblFacilityInformation_1_10.MailContactEnforcement)=[tblFacilityInformation_1_10_MailAddress].[MailAddressID]) " & _
         "AND tbl_ProjectManagement.FacilityId = '13" & Mid(AIRSNumber, 1, 3) & Mid(AIRSNumber, 5) & "') " & _
         "ORDER BY tbl_ProjectManagement.ProjectIdentifier "

                GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                GATVcmd = New OleDbCommand(SQL, GATVConn)
                If GATVConn.State = ConnectionState.Closed Then
                    GATVConn.Open()
                End If
                GATVdr = GATVcmd.ExecuteReader

                While GATVdr.Read
                    'If IsDBNull(GATVdr.item("ApplicationNumber")) Then
                    '    AppNumber = ""
                    'Else
                    '    AppNumber = GATVdr.item("ApplicationNumber")
                    'End If
                    If IsDBNull(GATVdr.Item("ContactName")) Then
                        ContactName = ""
                    Else
                        ContactName = GATVdr.Item("ContactName")
                    End If
                    ContactFirstName = ""
                    ContactLastName = ""
                    ContactPrefix = ""
                    ContactSuffix = ""
                    If ContactName <> "" Then
                        If ContactName.Contains("Mr.") Then
                            ContactPrefix = "Mr."
                            ContactName = ContactName.Replace("Mr. ", "")
                        End If
                        If ContactName.Contains(" ") Then
                            ContactFirstName = Mid(ContactName, 1, ContactName.IndexOf(" "))
                            ContactLastName = Mid(ContactName, ContactName.IndexOf(" ") + 2)
                        Else
                            ContactFirstName = ContactName
                            ContactLastName = ""
                        End If
                    Else
                        ContactFirstName = ""
                        ContactLastName = ""
                    End If
                    If IsDBNull(GATVdr.Item("MailAddressCompany")) Then
                        ContactCompany = ""
                    Else
                        ContactCompany = GATVdr.Item("MailAddressCompany")
                    End If
                    If IsDBNull(GATVdr.Item("ContactTitle")) Then
                        ContactTitle = ""
                    Else
                        ContactTitle = GATVdr.Item("ContactTitle")
                    End If
                    If IsDBNull(GATVdr.Item("MailAddressStreet")) Then
                        ContactAddress = ""
                    Else
                        ContactAddress = GATVdr.Item("MailAddressStreet")
                    End If
                    If IsDBNull(GATVdr.Item("MailAddressCity")) Then
                        ContactCity = ""
                    Else
                        ContactCity = GATVdr.Item("MailAddressCity")
                    End If
                    If IsDBNull(GATVdr.Item("MailAddressState")) Then
                        ContactState = ""
                    Else
                        ContactState = GATVdr.Item("MailAddressState")
                    End If
                    If IsDBNull(GATVdr.Item("MailingAddressZip")) Then
                        ContactZipCode = ""
                    Else
                        ContactZipCode = GATVdr.Item("MailingAddressZip")
                    End If
                    If IsDBNull(GATVdr.Item("ContactPhone")) Then
                        ContactPhone = ""
                    Else
                        ContactPhone = GATVdr.Item("ContactPhone")
                    End If
                    If IsDBNull(GATVdr.Item("ContactFax")) Then
                        ContactFax = ""
                    Else
                        ContactFax = GATVdr.Item("ContactFax")
                    End If
                    If IsDBNull(GATVdr.Item("ContactEMail")) Then
                        ContactEmail = ""
                    Else
                        ContactEmail = GATVdr.Item("ContactEMail")
                    End If
                    ContactDescription = ""
                    If IsDBNull(GATVdr.Item("ContactPhoneExt")) Then
                    Else
                        ContactDescription = "Contact extension - " & GATVdr.Item("ContactPhoneExt")
                    End If
                    If ContactDescription <> "" Then
                        ContactDescription = ContactDescription & vbCrLf & _
                            "Added from GATV Warehouse contact information"
                    Else
                        ContactDescription = "Added from GATV Warehouse contact information"
                    End If

                End While
                GATVdr.close()

                SQL = "Select count(*) as SSCPContact " & _
                "From " & DBNameSpace & ".APBContactInformation " & _
                "where strContactKey = '0413" & AIRSNumber & "20' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("SSCPContact")) Then
                        temp = "0"
                    Else
                        temp = dr.Item("SSCPContact")
                    End If
                End While
                dr.Close()

                If temp = "0" Then
                    Insert_APBContactInformation(AIRSNumber, "20", _
                                                  ContactFirstName, ContactLastName, _
                                                  ContactPrefix, ContactSuffix, _
                                                  ContactTitle, ContactCompany, _
                                                  ContactPhone, "", _
                                                  ContactFax, ContactEmail, _
                                                  ContactAddress, "", _
                                                  ContactCity, ContactState, _
                                                  ContactZipCode, ContactDescription)
                Else
                    Update_APBContactInformation(AIRSNumber, "20", _
                                                  ContactFirstName, ContactLastName, _
                                                  ContactPrefix, ContactSuffix, _
                                                  ContactTitle, ContactCompany, _
                                                  ContactPhone, "", _
                                                  ContactFax, ContactEmail, _
                                                  ContactAddress, "", _
                                                  ContactCity, ContactState, _
                                                  ContactZipCode, ContactDescription)
                End If

            End While
            dr2.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim numKey As Integer = 1
            Dim AIRSnumber As String = ""
            'Dim intyear As String = ""
            Dim numSSCPEngineer As String = ""
            Dim numSSCPUnit As String = ""
            Dim strInspectionRequired As String = ""
            Dim strFCERequired As String = ""
            Dim strAssigningManager As String = ""
            Dim datAssigningDate As String = ""

            SQL = "Delete AIRBranch.SSCPINSPECTIONSREQUIRED2 "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            'Fiscal Year 2011
            SQL = "select   " & _
            "(select " & _
            "case " & _
            "when max(NUMKEY) is null then 1 " & _
            "else max(NUMKEY) + 1 " & _
            "end NUMKEY " & _
            "from AIRBRANCH.SSCPINSPECTIONSREQUIRED2) as NUMKEY, " & _
            " AIRBRANCH.SSCPFACILITYASSIGNMENT.STRAIRSNUMBER, '2011', " & _
            " STRSSCPENGINEER, numUnit, " & _
            "STRINSPECTIONREQUIRED, " & _
            "case " & _
            "when FCECHECK.STRAIRSNUMBER is null then '' " & _
            "else 'True' " & _
            "end FCERequired, " & _
            "STRASSIGNINGMANAGER, " & _
            "to_char(DATASSIGNINGDATE) as datAssigningDate " & _
            "from AIRBRANCH.SSCPINSPECTIONSREQUIRED_OLD, AIRBRANCH.SSCPFACILITYASSIGNMENT, " & _
            "AIRBRANCH.EPDUSERPROFILES, " & _
            "(select SSCPFCEMASTER.* " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED_old, " & _
            "AIRBRANCH.SSCPFCE " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED_old.STRAIRSNUMBER " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED  > '30-Sep-2010') FCECheck " & _
            "where AIRBRANCH.SSCPFACILITYASSIGNMENT.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED_OLD.STRAIRSNUMBER (+) " & _
            "and AIRBRANCH.SSCPFACILITYASSIGNMENT.STRSSCPENGINEER = AIRBRANCH.EPDUSERPROFILES.NUMUSERID (+) " & _
            "and AIRBRANCH.SSCPFACILITYASSIGNMENT.STRAIRSNUMBER  = FCECheck.strAIRSNumber (+) " & _
            " and STRINSPECTIONREQUIRED is not null "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("STRAIRSNUMBER")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("STRAIRSNUMBER")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("STRSSCPENGINEER")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("strSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("strInspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("strInspectionRequired")
                    End If
                    If IsDBNull(dr.Item("FCERequired")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCERequired")
                    End If
                    If IsDBNull(dr.Item("strAssigningManager")) Then
                        strAssigningManager = ""
                    Else
                        strAssigningManager = dr.Item("strAssigningManager")
                    End If
                    If IsDBNull(dr.Item("datAssigningDate")) Then
                        datAssigningDate = ""
                    Else
                        datAssigningDate = dr.Item("datAssigningDate")
                    End If

                    SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                    "values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2011', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'" & strFCERequired & "', '" & strAssigningManager & "', " & _
                    "'" & datAssigningDate & "') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            'Fiscal year 2010
            SQL = "select " & _
            "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
            "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
            "numUnit, " & _
            "case " & _
            "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end INSPECTIONREQUIRED, " & _
            "'' as FCEREQUIRED  " & _
            "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
            "airbranch.epduserprofiles " & _
            "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and datInspectiondateend between '01-Oct-09' and '30-Sep-10' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2010', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2009' and  '30-Sep-2010' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2010' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2010' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2010', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()

            'Ficscal Year 2009
            SQL = "select " & _
          "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
          "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
          "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
          "numUnit, " & _
          "case " & _
          "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
          "else 'False' " & _
          "end INSPECTIONREQUIRED, " & _
          "'' as FCEREQUIRED  " & _
          "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
          "airbranch.epduserprofiles " & _
          "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
          "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
          "and datInspectiondateend between '01-Oct-08' and '30-Sep-09' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2009', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2008' and  '30-Sep-2009' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2009' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2009' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2009', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()

            'Fiscal year 2008 
            SQL = "select " & _
          "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
          "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
          "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
          "numUnit, " & _
          "case " & _
          "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
          "else 'False' " & _
          "end INSPECTIONREQUIRED, " & _
          "'' as FCEREQUIRED  " & _
          "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
          "airbranch.epduserprofiles " & _
          "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
          "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
          "and datInspectiondateend between '01-Oct-07' and '30-Sep-08' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2008', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2007' and  '30-Sep-2008' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2008' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2008' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2008', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()

            'Fiscal year 2007
            SQL = "select " & _
          "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
          "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
          "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
          "numUnit, " & _
          "case " & _
          "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
          "else 'False' " & _
          "end INSPECTIONREQUIRED, " & _
          "'' as FCEREQUIRED  " & _
          "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
          "airbranch.epduserprofiles " & _
          "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
          "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
          "and datInspectiondateend between '01-Oct-06' and '30-Sep-07' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2007', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2006' and  '30-Sep-2007' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2007' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2007' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2007', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()

            'Fiscal year 2006
            SQL = "select " & _
          "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
          "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
          "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
          "numUnit, " & _
          "case " & _
          "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
          "else 'False' " & _
          "end INSPECTIONREQUIRED, " & _
          "'' as FCEREQUIRED  " & _
          "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
          "airbranch.epduserprofiles " & _
          "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
          "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
          "and datInspectiondateend between '01-Oct-05' and '30-Sep-06' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2006', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2005' and  '30-Sep-2006' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2006' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2006' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2006', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()

            'Fiscal year 2005
            SQL = "select " & _
          "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
          "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
          "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
          "numUnit, " & _
          "case " & _
          "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
          "else 'False' " & _
          "end INSPECTIONREQUIRED, " & _
          "'' as FCEREQUIRED  " & _
          "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
          "airbranch.epduserprofiles " & _
          "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
          "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
          "and datInspectiondateend between '01-Oct-04' and '30-Sep-05' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2005', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2004' and  '30-Sep-2005' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2005' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2005' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2005', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()

            'Fiscal year 2004
            SQL = "select " & _
          "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
          "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
          "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
          "numUnit, " & _
          "case " & _
          "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
          "else 'False' " & _
          "end INSPECTIONREQUIRED, " & _
          "'' as FCEREQUIRED  " & _
          "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
          "airbranch.epduserprofiles " & _
          "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
          "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
          "and datInspectiondateend between '01-Oct-03' and '30-Sep-04' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2004', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2003' and  '30-Sep-2004' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2004' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2004' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2004', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()

            'Fiscal year 2003
            SQL = "select " & _
          "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
          "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
          "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
          "numUnit, " & _
          "case " & _
          "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
          "else 'False' " & _
          "end INSPECTIONREQUIRED, " & _
          "'' as FCEREQUIRED  " & _
          "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
          "airbranch.epduserprofiles " & _
          "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
          "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
          "and datInspectiondateend between '01-Oct-02' and '30-Sep-03' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2003', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2002' and  '30-Sep-2003' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2003' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2003' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2003', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            'Fiscal year 2002 
            SQL = "select " & _
          "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
          "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
          "AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON as SSCPENGINEER, " & _
          "numUnit, " & _
          "case " & _
          "when AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER is not null then 'True' " & _
          "else 'False' " & _
          "end INSPECTIONREQUIRED, " & _
          "'' as FCEREQUIRED  " & _
          "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
          "airbranch.epduserprofiles " & _
          "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
          "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
          "and datInspectiondateend between '01-Oct-01' and '30-Sep-02' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numUnit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("InspectionRequired")) Then
                        strInspectionRequired = ""
                    Else
                        strInspectionRequired = dr.Item("InspectionRequired")
                    End If

                    SQL2 = "Insert into airbranch.sscpInspectionsRequired2 " & _
                    "Values " & _
                    "('" & numKey & "', '" & AIRSnumber & "', " & _
                    "'2002', '" & numSSCPEngineer & "', " & _
                    "'" & numSSCPUnit & "', '" & strInspectionRequired & "', " & _
                    "'', '', '') "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
            "case " & _
            "when AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER is not null then 'True' " & _
            "else 'False' " & _
            "end FCEREQUIRED, " & _
            "AIRbranch.SSCPFCEMaster.strModifingPerson as numSSCPEngineer, numunit " & _
            "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED2, " & _
            "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
            "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED2.STRAIRSNUMBER " & _
            "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
            "and DATFCECOMPLETED between '01-Oct-2001' and  '30-Sep-2002' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSnumber = ""
                Else
                    AIRSnumber = dr.Item("strAIRSnumber")
                End If
                If AIRSnumber <> "" Then
                    If IsDBNull(dr.Item("numSSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("numSSCPEngineer")
                    End If
                    If IsDBNull(dr.Item("numunit")) Then
                        numSSCPUnit = ""
                    Else
                        numSSCPUnit = dr.Item("numUnit")
                    End If
                    If IsDBNull(dr.Item("FCEREQUIRED")) Then
                        strFCERequired = ""
                    Else
                        strFCERequired = dr.Item("FCEREQUIRED")
                    End If

                    SQL2 = "select numkey " & _
                    "from airbranch.SSCPInspectionsRequired2 " & _
                    "where strAIRSNumber = '" & AIRSnumber & "' " & _
                    "and intyear = '2002' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    RecExist = dr2.Read
                    If RecExist = True Then
                        SQL2 = "Update AIRBranch.SSCPInspectionsRequired2 set " & _
                        "strFCERequired = '" & strFCERequired & "' " & _
                        "Where strAIRSNumber = '" & AIRSnumber & "' " & _
                        "and intyear = '2002' "
                    Else
                        SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired2 " & _
                         "values " & _
                         "('" & numKey & "', '" & AIRSnumber & "', " & _
                         "'2002', '" & numSSCPEngineer & "', " & _
                         "'" & numSSCPUnit & "', '', " & _
                         "'" & strFCERequired & "', '', " & _
                         "'' ) "
                        numKey += 1
                    End If
                    dr2.Close()

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()


            MsgBox("successfully Done")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim transferConn As OracleConnection

            If Oracledll = "2.111.6.20" Then
                transferConn = New OracleConnection("Data Source = leia.dnr.state.ga.us:1521/DEV; User ID = AirBranch; " & _
                         "Password = " & SimpleCrypt("óíïçáìåòô") & ";")
            Else
                transferConn = New OracleConnection("data Source = DEV; " & _
                                   "User ID = AIRBranch; Password = smogalert;")
            End If

            SQL = "Delete SSCPInspectionsRequired "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "select " & _
            "numKey, strAIRSNumber, " & _
            "intYear, numSSCPEngineer, " & _
            "numSSCPUnit, strInspectionRequired, " & _
            "strFCERequired, strAssigningManager, " & _
            "to_char(datAssigningDate) as datAssigningDate " & _
            "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
            "order by numKey "

            cmd = New OracleCommand(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                SQL = "Insert into SSCPInspectionsRequired " & _
                "values " & _
                "('" & dr.Item("numkey") & "', " & _
                "'" & dr.Item("strAIRSNumber") & "', " & _
                "'" & dr.Item("intYEar") & "', " & _
                "'" & dr.Item("numSSCPEngineer") & "', " & _
                "'" & dr.Item("numSSCPUnit") & "', " & _
                "'" & dr.Item("strInspectionRequired") & "', " & _
                "'" & dr.Item("strFCERequired") & "', " & _
                "'" & dr.Item("strAssigningManager") & "', " & _
                "'" & dr.Item("datAssigningDate") & "' )  "

                cmd2 = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr2 = cmd2.ExecuteReader
                dr2.Close()

            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim AIRSNumber As String
            Dim numSSCPEngineer As String

            'Fiscal year 2010
            SQL = "select " & _
            "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
            "airbranch.sscpitemmaster.strResponsibleStaff as SSCPENGINEER  " & _
            "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
            "airbranch.epduserprofiles " & _
            "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and datInspectiondateend between '01-Oct-09' and '30-Sep-10' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2010' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "select  " & _
                    "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
                    "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
                    "strReviewer " & _
                    "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED, " & _
                    "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
                    "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER " & _
                    "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
                    "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
                    "and DATFCECOMPLETED between '01-Oct-09' and '30-Sep-10' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("strReviewer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("strReviewer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2010' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()



            'Fiscal year 2009
            SQL = "select " & _
            "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
            "airbranch.sscpitemmaster.strResponsibleStaff as SSCPENGINEER  " & _
            "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
            "airbranch.epduserprofiles " & _
            "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and datInspectiondateend between '01-Oct-08' and '30-Sep-09' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2009' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "select  " & _
                    "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
                    "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
                    "strReviewer " & _
                    "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED, " & _
                    "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
                    "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER " & _
                    "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
                    "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
                    "and DATFCECOMPLETED between '01-Oct-08' and '30-Sep-09' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("strReviewer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("strReviewer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2009' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()


            'Fiscal year 2008
            SQL = "select " & _
            "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
            "airbranch.sscpitemmaster.strResponsibleStaff as SSCPENGINEER  " & _
            "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
            "airbranch.epduserprofiles " & _
            "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and datInspectiondateend between '01-Oct-07' and '30-Sep-08' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2008' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "select  " & _
                    "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
                    "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
                    "strReviewer " & _
                    "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED, " & _
                    "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
                    "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER " & _
                    "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
                    "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
                    "and DATFCECOMPLETED between '01-Oct-07' and '30-Sep-08' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("strReviewer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("strReviewer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2008' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()


            'Fiscal year 2007
            SQL = "select " & _
            "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
            "airbranch.sscpitemmaster.strResponsibleStaff as SSCPENGINEER  " & _
            "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
            "airbranch.epduserprofiles " & _
            "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and datInspectiondateend between '01-Oct-06' and '30-Sep-07' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2007' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "select  " & _
                    "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
                    "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
                    "strReviewer " & _
                    "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED, " & _
                    "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
                    "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER " & _
                    "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
                    "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
                    "and DATFCECOMPLETED between '01-Oct-06' and '30-Sep-07' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("strReviewer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("strReviewer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2007' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()



            'Fiscal year 2006
            SQL = "select " & _
            "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
            "'20'||SUBSTR(DATINSPECTIONDATEEND, 8) as INTYEAR, " & _
            "airbranch.sscpitemmaster.strResponsibleStaff as SSCPENGINEER  " & _
            "from AIRBRANCH.SSCPINSPECTIONS, AIRBRANCH.SSCPITEMMASTER, " & _
            "airbranch.epduserprofiles " & _
            "where AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER = AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER " & _
            "and AIRBRANCH.SSCPITEMMASTER.STRMODIFINGPERSON = AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
            "and datInspectiondateend between '01-Oct-05' and '30-Sep-06' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("SSCPEngineer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("SSCPEngineer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2006' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "select  " & _
                    "AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER, " & _
                    "'20'||SUBSTR(DATFCECOMPLETED, 8) as INTYEAR, " & _
                    "strReviewer " & _
                    "from AIRBRANCH.SSCPFCEMASTER, AIRBRANCH.SSCPINSPECTIONSREQUIRED, " & _
                    "AIRBRANCH.SSCPFCE, AIRBranch.EPDUserProfiles  " & _
                    "where AIRBRANCH.SSCPFCEMASTER.STRAIRSNUMBER = AIRBRANCH.SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER " & _
                    "and AIRbranch.SSCPFCEMaster.strModifingPerson =  AIRBRANCH.EPDUSERPROFILES.NUMUSERID " & _
                    "and AIRBRANCH.SSCPFCEMASTER.STRFCENUMBER = AIRBRANCH.SSCPFCE.STRFCENUMBER " & _
                    "and DATFCECOMPLETED between '01-Oct-05' and '30-Sep-06' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIrsNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If AIRSNumber <> "" Then
                    If IsDBNull(dr.Item("strReviewer")) Then
                        numSSCPEngineer = ""
                    Else
                        numSSCPEngineer = dr.Item("strReviewer")
                    End If

                    SQL2 = "Update AIRBranch.SSCPInspectionsRequired set " & _
                    "numSSCPEngineer = '" & numSSCPEngineer & "' " & _
                    "where intyear = '2006' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()


            MsgBox("Done")

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim AIRSNUmber As String = ""
            Dim numSSCPEngineer As String = ""
            Dim numKey As Integer

            SQL = "Select (max(numKey) + 1) as numKey from airbranch.SSCPInspectionsRequired "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numKey")) Then
                    numKey = 0
                Else
                    numKey = dr.Item("NumKey")
                End If
            End While
            dr.Close()

            SQL = "select " & _
            "strairsnumber, strSSCPEngineer  " & _
            "from AIRBRANCH.SSCPFACILITYASSIGNMENT " & _
            "where not exists " & _
            "(select * " & _
            "from AIRBRANCH.SSCPINSPECTIONSREQUIRED " & _
            "where AIRbranch.SSCPFacilityAssignment.strairsnumber = AIRbranch.SSCPInspectionsRequired.strAIRSnumber " & _
            "and INTYEAR = '2011' ) " & _
            "and STRSSCPENGINEER is not null "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNUmber = ""
                Else
                    AIRSNUmber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("strSSCPEngineer")) Then
                    numSSCPEngineer = ""
                Else
                    numSSCPEngineer = dr.Item("strSSCPEngineer")
                End If
                If AIRSNUmber <> "" Then
                    SQL2 = "Insert into AIRBranch.SSCPInspectionsRequired " & _
                    "values " & _
                    "('" & numKey & "', '" & AIRSNUmber & "', " & _
                    "'2011', '" & numSSCPEngineer & "', " & _
                    "'', '', " & _
                    "'', '', " & _
                    "'' ) "
                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    numKey += 1
                End If
            End While
            dr.Close()



        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim NOVComment As String = ""
            Dim COComment As String = ""
            Dim AOComment As String = ""
            Dim EnforcementNumber As String = ""

            SQL = "Select " & _
            "strEnforcementNumber, strNOVComment " & _
            "From " & DBNameSpace & ".SSCPEnforcementNOVComments " & _
            "where strNOVComment is not null "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strNOVComment")) Then
                    NOVComment = ""
                Else
                    NOVComment = dr.Item("strNOVComment")
                End If
                If NOVComment <> "" And EnforcementNumber <> "N/A" Then
                    SQL = "select " & _
                    "strNOVComment " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where id = ( " & _
                    "select max(id) as MaxID " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where strEnforcementNumber = '" & EnforcementNumber & "' ) "

                    cmd2 = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("strNOVComment")) Then
                        Else
                            NOVComment = dr2.Item("strNOVComment") & vbCrLf & NOVComment
                        End If
                    End While
                    dr2.Close()

                    SQL = "Update " & DBNameSpace & ".SSCP_enforcement set " & _
                    "strNOVComment = '" & Replace(Mid(NOVComment, 1, 3999), "'", "''") & "' " & _
                    "where id = ( " & _
                    "select max(id) as MaxID " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where strEnforcementNumber = '" & EnforcementNumber & "' ) "

                    cmd2 = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "strEnforcementNumber, strCOComment " & _
            "From " & DBNameSpace & ".SSCPEnforcementCOComments " & _
            "where strcoComment is not null "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strCOComment")) Then
                    COComment = ""
                Else
                    COComment = dr.Item("strCOComment")
                End If
                If COComment <> "" And EnforcementNumber <> "N/A" Then
                    SQL = "select " & _
                    "strCOComment " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where id = ( " & _
                    "select max(id) as MaxID " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where strEnforcementNumber = '" & EnforcementNumber & "' ) "

                    cmd2 = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("strCOComment")) Then
                        Else
                            COComment = dr2.Item("strCOComment") & vbCrLf & NOVComment
                        End If
                    End While
                    dr2.Close()

                    SQL = "Update " & DBNameSpace & ".SSCP_enforcement set " & _
                    "strcoComment = '" & Replace(Mid(COComment, 1, 3999), "'", "''") & "' " & _
                    "where id = ( " & _
                    "select max(id) as MaxID " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where strEnforcementNumber = '" & EnforcementNumber & "' ) "

                    cmd2 = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If
            End While
            dr.Close()


            SQL = "Select " & _
        "strEnforcementNumber, strAOComment " & _
        "From " & DBNameSpace & ".SSCPEnforcementAOComments " & _
        "where strAOComment is not null "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("straoComment")) Then
                    AOComment = ""
                Else
                    AOComment = dr.Item("straoComment")
                End If
                If AOComment <> "" And EnforcementNumber <> "N/A" Then
                    SQL = "select " & _
                    "straoComment " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where id = ( " & _
                    "select max(id) as MaxID " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where strEnforcementNumber = '" & EnforcementNumber & "' ) "

                    cmd2 = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("straoComment")) Then
                        Else
                            AOComment = dr2.Item("straoComment") & vbCrLf & NOVComment
                        End If
                    End While
                    dr2.Close()

                    SQL = "Update " & DBNameSpace & ".SSCP_enforcement set " & _
                    "strAOComment = '" & Replace(Mid(AOComment, 1, 3999), "'", "''") & "' " & _
                    "where id = ( " & _
                    "select max(id) as MaxID " & _
                    "from " & DBNameSpace & ".SSCP_Enforcement " & _
                    "where strEnforcementNumber = '" & EnforcementNumber & "' ) "

                    cmd2 = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

 

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FeeYear As String = ""
            Dim VOCTons As String = ""
            Dim PMTons As String = ""
            Dim SO2Tons As String = ""
            Dim NOXTons As String = ""
            Dim Part70Fee As String = ""
            Dim SMFee As String = ""
            Dim NSPSFee As String = ""
            Dim TotalFee As String = ""
            Dim NSPSExempt As String = ""
            ' Dim NSPSReason As String = ""
            Dim Operate As String = ""
            Dim FeeRate As String = ""
            Dim NSPSExemptReason As String = ""
            Dim Part70 As String = ""
            Dim SyntheticMinor As String = ""
            Dim CalculatedFee As String = ""
            Dim Class1 As String = ""
            Dim NSPS1 As String = ""
            Dim ShutDate As String = ""
            'Dim VarianceCheck As String = ""
            'Dim VarianceComments As String = ""
            Dim AdminFee As String = ""

            SQL = "Select * " & _
            "from " & DBNameSpace & ".FSCalculations " & _
            "where intyear <> '2010' and intYear <> '3' " & _
             "order by intyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("intYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("intyear")
                End If
                If IsDBNull(dr.Item("intVOCTons")) Then
                    VOCTons = ""
                Else
                    VOCTons = dr.Item("intVOCTons")
                End If

                If IsDBNull(dr.Item("intPMTons")) Then
                    PMTons = ""
                Else
                    PMTons = dr.Item("intPMTons")
                End If
                If IsDBNull(dr.Item("intSO2Tons")) Then
                    SO2Tons = ""
                Else
                    SO2Tons = dr.Item("intSO2Tons")
                End If
                If IsDBNull(dr.Item("intNOXTons")) Then
                    NOXTons = ""
                Else
                    NOXTons = dr.Item("intNOXTons")
                End If
                If IsDBNull(dr.Item("numPart70Fee")) Then
                    Part70Fee = ""
                Else
                    Part70Fee = dr.Item("numPart70Fee")
                End If
                If IsDBNull(dr.Item("numSMFee")) Then
                    SMFee = ""
                Else
                    SMFee = dr.Item("numSMFee")
                End If
                If IsDBNull(dr.Item("numNSPSFee")) Then
                    NSPSFee = ""
                Else
                    NSPSFee = dr.Item("numNSPSFee")
                End If
                If IsDBNull(dr.Item("numTotalFee")) Then
                    TotalFee = ""
                Else
                    TotalFee = dr.Item("numtotalfee")
                End If
                If IsDBNull(dr.Item("strNSPSExempt")) Then
                    NSPSExempt = ""
                Else
                    NSPSExempt = dr.Item("strNSPSExempt")
                End If
                If IsDBNull(dr.Item("strNSPSReason")) Then
                    NSPSExemptReason = ""
                Else
                    NSPSExemptReason = dr.Item("strNSPSReason")
                End If
                If IsDBNull(dr.Item("strOperate")) Then
                    Operate = ""
                Else
                    If dr.Item("strOperate") = "YES" Then
                        Operate = "1"
                    Else
                        Operate = "0"
                    End If
                End If
                If IsDBNull(dr.Item("numFeeRate")) Then
                    FeeRate = ""
                Else
                    FeeRate = dr.Item("numfeerate")
                End If
                'If IsDBNull(dr.Item("strNSPSExemptReason")) Then
                '    NSPSExemptReason = ""
                'Else
                '    '  NSPSExemptReason = dr.Item("strNSPSExemptReason")
                '    NSPSExemptReason = ""
                'End If
                If IsDBNull(dr.Item("strPart70")) Then
                    Part70 = ""
                Else
                    If dr.Item("strPart70") = "YES" Then
                        Part70 = "1"
                    Else
                        Part70 = "0"
                    End If
                End If
                If IsDBNull(dr.Item("strsyntheticminor")) Then
                    SyntheticMinor = ""
                Else
                    If dr.Item("strsyntheticminor") = "YES" Then
                        SyntheticMinor = "1"
                    Else
                        SyntheticMinor = "0"
                    End If
                End If
                If IsDBNull(dr.Item("numcalculatedFee")) Then
                    CalculatedFee = ""
                Else
                    CalculatedFee = dr.Item("numcalculatedFee")
                End If
                If IsDBNull(dr.Item("strClass1")) Then
                    Class1 = ""
                Else
                    Class1 = dr.Item("strClass1")
                End If
                If IsDBNull(dr.Item("strNSPS1")) Then
                    NSPS1 = ""
                Else
                    If dr.Item("strNSPS1") = "YES" Then
                        NSPS1 = "1"
                    Else
                        NSPS1 = "0"
                    End If
                End If
                If IsDBNull(dr.Item("ShutDate")) Then
                    ShutDate = ""
                Else
                    ShutDate = Format(dr.Item("ShutDate"), "dd-MMM-yyyy")
                End If
                'If IsDBNull(dr.Item("VarianceCheck")) Then
                '    VarianceCheck = ""
                'Else
                '    VarianceCheck = dr.Item("VarianceCheck")
                'End If
                'If IsDBNull(dr.Item("VarianceComments")) Then
                '    VarianceComments = ""
                'Else
                '    VarianceComments = dr.Item("VarianceComments")
                'End If
                If IsDBNull(dr.Item("numAdminFee")) Then
                    AdminFee = ""
                Else
                    AdminFee = dr.Item("numAdminFee")
                End If

                If FeeYear <> "" And AIRSNumber <> "" Then
                    SQL = "Select strairsnumber " & _
                    "from " & DBNameSpace & ".FS_Admin " & _
                    "where numfeeyear = '" & FeeYear & "' " & _
                    "and strAIRSnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    RecExist = dr2.Read
                    dr2.Close()
                    If RecExist = False Then

                        SQL = "Insert into " & DBNameSpace & ".FS_Admin " & _
                        "values " & _
                        "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', " & _
                        "'1', 'Fee Populate', " & _
                        "sysdate, sysdate) "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into " & DBNameSpace & ".FS_Mailout " & _
                        "values " & _
                        "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '" & Operate & "', " & _
                        "'" & Class1 & "', '" & NSPS1 & "', " & _
                        "'" & Part70 & "', '" & ShutDate & "', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'1', 'Fee Populate', " & _
                        "sysdate, sysdate) "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into " & DBNameSpace & ".FS_Feedata " & _
                        "values " & _
                        "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                        "'" & SyntheticMinor & "', '" & SMFee & "', " & _
                        "'" & Part70 & "', '" & Part70Fee & "', " & _
                        "'" & VOCTons & "', '" & PMTons & "', " & _
                        "'" & SO2Tons & "', '" & NOXTons & "', " & _
                        "'" & CalculatedFee & "', '" & FeeRate & "', " & _
                        "'" & NSPS1 & "', '" & NSPSFee & "', " & _
                        "'" & NSPSExempt & "', '" & NSPSExemptReason & "', " & _
                        "'" & AdminFee & "', '" & TotalFee & "', " & _
                        "'" & Class1 & "', '" & Operate & "', " & _
                        "'" & ShutDate & "', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', " & _
                        "'1', 'Fee Populate', " & _
                        "sysdate, sysdate ) "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into " & DBNameSpace & ".FS_ContactInfo " & _
                        "values " & _
                        "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', '', " & _
                        "'', " & _
                        "'1', 'Fee Populate', " & _
                        "sysdate, sysdate ) "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        dr2.Close()

                    End If

                End If


            End While
            dr.Close()

            Dim ContactFirstName As String = ""
            Dim ContactLastName As String = ""
            Dim ContactPrefix As String = ""
            Dim ContactTitle As String = ""
            Dim ContactCompanyName As String = ""
            Dim ContactPhoneNumber As String = ""
            Dim ContactFaxNumber As String = ""
            Dim ContactEmail As String = ""
            Dim ContactAddress As String = ""
            Dim ContactCity As String = ""
            Dim ContactState As String = ""
            Dim ContactZipCode As String = ""
            Dim ModifingDate As String = ""

            SQL = "Select * " & _
            "From " & DBNameSpace & ".FSContactInfo " & _
            "where intyear <> '2010' and intYear <> '3' " & _
            "order by intyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AirsNumber = ""
                FeeYear = ""
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    ContactFirstName = ""
                Else
                    ContactFirstName = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    ContactLastName = ""
                Else
                    ContactLastName = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    ContactPrefix = ""
                Else
                    ContactPrefix = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    ContactTitle = ""
                Else
                    ContactTitle = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    ContactCompanyName = ""
                Else
                    ContactCompanyName = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber")) Then
                    ContactPhoneNumber = ""
                Else
                    ContactPhoneNumber = dr.Item("strContactPhoneNumber")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    ContactFaxNumber = ""
                Else
                    ContactFaxNumber = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    ContactEmail = ""
                Else
                    ContactEmail = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactAddress")) Then
                    ContactAddress = ""
                Else
                    ContactAddress = dr.Item("strContactAddress")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    ContactCity = ""
                Else
                    ContactCity = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    ContactState = ""
                Else
                    ContactState = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    ContactZipCode = ""
                Else
                    ContactZipCode = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("intYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("intYear")
                End If
                If IsDBNull(dr.Item("datModifyDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifyDate"), "dd-MMM-yyyy")
                End If

                If AIRSNumber <> "" And FeeYear <> "" Then
                    SQL = "Update " & DBNameSpace & ".FS_ContactInfo set " & _
                    "strContactFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " & _
                    "strContactLastName = '" & Replace(ContactLastName, "'", "''") & "', " & _
                    "strContactPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " & _
                    "strContactTitle = '" & Replace(ContactTitle, "'", "''") & "', " & _
                    "strContactCompanyName = '" & Replace(ContactCompanyName, "'", "''") & "', " & _
                    "strcontactAddress = '" & Replace(ContactAddress, "'", "''") & "', " & _
                    "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " & _
                    "strContactState = '" & Replace(ContactState, "'", "''") & "', " & _
                    "strContactZipCode = '" & Replace(ContactZipCode, "'", "''") & "', " & _
                    "strContactPhoneNumber = '" & Replace(ContactPhoneNumber, "'", "''") & "', " & _
                    "strContactFaxNumber = '" & Replace(ContactFaxNumber, "'", "''") & "', " & _
                    "strContactEmail = '" & Replace(ContactEmail, "'", "''") & "', " & _
                    "UpdateDateTime = '" & ModifingDate & "', " & _
                    "CreateDateTime = '" & ModifingDate & "' " & _
                    "where numFeeyear = '" & FeeYear & "' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            Dim PaymantType As String = ""
            Dim OfficialName As String = ""
            Dim OfficialTitle As String = ""
            Dim DateSubmit As String = ""
            Dim intSubmittal As String = ""

            SQL = "Select * " & _
            "From " & DBNameSpace & ".FSPayAndSubmit " & _
            "where intyear <> '2010' and intYear <> '3' " & _
            "order by intyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = ""
                FeeYear = ""

                If IsDBNull(dr.Item("strPaymentType")) Then
                    PaymantType = ""
                Else
                    PaymantType = dr.Item("strPaymentType")
                    If PaymantType = "N/A" Then
                        PaymantType = ""
                    End If
                End If
                If IsDBNull(dr.Item("strOfficialName")) Then
                    OfficialName = ""
                Else
                    OfficialName = dr.Item("strOfficialName")
                End If
                If IsDBNull(dr.Item("strOfficialTitle")) Then
                    OfficialTitle = ""
                Else
                    OfficialTitle = dr.Item("strOfficialTitle")
                End If
                If IsDBNull(dr.Item("dateSubmit")) Then
                    DateSubmit = OracleDate
                Else
                    DateSubmit = Format(dr.Item("dateSubmit"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("intSubmittal")) Then
                    intSubmittal = "0"
                Else
                    intSubmittal = dr.Item("intSubmittal")
                End If
                If IsDBNull(dr.Item("strAIRSNUmber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("intYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("intYear")
                End If

                If AIRSNumber <> "" And FeeYear <> "" Then
                    SQL = "Update " & DBNameSpace & ".FS_FeeData set " & _
                    "strOfficialName = '" & Replace(OfficialName, "'", "''") & "', " & _
                    "strOfficialTitle = '" & Replace(OfficialTitle, "'", "''") & "', " & _
                    "strPaymentPlan = '" & PaymantType & "', " & _
                    "UpdateDateTime = '" & DateSubmit & "' " & _
                    "where numFeeYear = '" & FeeYear & "' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()

                    SQL = "Update " & DBNameSpace & ".FS_Admin set " & _
                    "intSubmittal = '" & intSubmittal & "', " & _
                    "datSubmittal = '" & DateSubmit & "', " & _
                    "updateDateTime = '" & DateSubmit & "' " & _
                    "where numFeeYear = '" & FeeYear & "' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If

            End While
            dr.Close()

            Dim numPayment As String = ""
            Dim datPayDate As String = ""
            Dim CheckNo As String = ""
            Dim DepositNo As String = ""
            Dim PayType As String = ""
            Dim BatchNo As String = ""
            Dim EntryPerson As String = ""
            Dim Comments As String = ""
            'Dim PayId As String = ""
            Dim InvoiceNo As String = ""
            Dim InvoiceIDCounter As Integer = 19999
            

            SQL = "Select * " & _
            "from " & DBNameSpace & ".FSAddPaid " & _
            "where intyear <> '2010' and intYear <> '3' " & _
            "order by intyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = ""
                FeeYear = ""

                If IsDBNull(dr.Item("numPayment")) Then
                    numPayment = ""
                Else
                    numPayment = dr.Item("numPayment")
                End If
                If IsDBNull(dr.Item("datPayDate")) Then
                    datPayDate = OracleDate
                Else
                    datPayDate = Format(dr.Item("datPayDate"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strCheckNo")) Then
                    CheckNo = ""
                Else
                    CheckNo = dr.Item("strCheckNo")
                End If
                If IsDBNull(dr.Item("strDepositNo")) Then
                    DepositNo = ""
                Else
                    DepositNo = dr.Item("strDepositNo")
                End If
                If IsDBNull(dr.Item("strPayType")) Then
                    PayType = "8"
                Else
                    PayType = dr.Item("strPayType")
                    Select Case PayType
                        Case "ANNUAL"
                            PayType = "1"
                        Case "QUARTER ONE"
                            PayType = "2"
                        Case "QUARTER TWO"
                            PayType = "3"
                        Case "QUARTER THREE"
                            PayType = "4"
                        Case "QUARTER FOUR"
                            PayType = "5"
                        Case "AMENDMENT"
                            PayType = "6"
                        Case "REFUND"
                            PayType = "7"
                        Case "ONE-TIME"
                            PayType = "8"
                        Case Else
                            PayType = "8"
                    End Select
                End If
                If IsDBNull(dr.Item("strBatchNo")) Then
                    BatchNo = ""
                Else
                    BatchNo = dr.Item("strBatchNo")
                End If
                If IsDBNull(dr.Item("strEntryPerson")) Then
                    EntryPerson = ""
                Else
                    EntryPerson = dr.Item("strEntryPerson")
                End If
                If IsDBNull(dr.Item("strComments")) Then
                    COmments = ""
                Else
                    COmments = dr.Item("strComments")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("intFiscalYear")) Then
                    If IsDBNull(dr.Item("intYear")) Then
                        FeeYear = ""
                    Else
                        FeeYear = dr.Item("intYear")
                    End If
                Else
                    FeeYear = dr.Item("intFiscalYear")
                End If
                If IsDBNull(dr.Item("strINvoiceNo")) Then
                    InvoiceNo = ""
                Else
                    Comments = Comments & vbCrLf & "Old Fee System Invoice ID: " & dr.Item("strInvoiceNO")
                End If
                InvoiceNo = InvoiceIDCounter
                InvoiceIDCounter -= 1

                SQL = "Insert into " & DBNameSpace & ".FS_FeeInvoice " & _
                "values " & _
                "('" & InvoiceNo & "', '" & AIRSNumber & "', " & _
                "'" & FeeYear & "', '" & numPayment & "', " & _
                "'" & datPayDate & "', '" & Replace(Comments, "'", "''") & "', " & _
                "'1', 'Fee Populate', " & _
                "sysdate, sysdate, " & _
                "'" & PayType & "', '1') "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                dr2.Close()
 
                SQL = "Insert into " & DBNameSpace & ".FS_Transactions " & _
                "values " & _
                "((" & DBNameSpace & ".seq_fs_transactions.nextVal), '" & InvoiceNo & "', " & _
                "'1', '" & datPayDate & "', " & _
                "'" & numPayment & "', '" & CheckNo & "', " & _
                "'" & DepositNo & "', '" & BatchNo & "', " & _
                "'" & EntryPerson & "', '" & Replace(Comments, "'", "''") & "', " & _
                "'1', '', " & _
                "sysdate, sysdate, " & _
                "'" & AIRSNumber & "', '" & FeeYear & "', " & _
                "'') "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                dr2.Close()

            End While
            dr.Close()

            Dim Confirmation As String = ""
            'Dim DateConfirmation As String = ""
            Dim ConfirmUser As String = ""

            SQL = "select * " & _
            "from " & DBNameSpace & ".FSConfirmation " & _
            "where intyear <> '2010' and intyear <> '3' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strConfirmation")) Then
                    Confirmation = ""
                Else
                    Confirmation = dr.Item("strConfirmation")
                End If
                'If IsDBNull(dr.Item("datConfirmation")) Then
                '    DateConfirmation = ""
                'Else
                '    DateConfirmation = Format(dr.Item("datConfirmation"), "dd-MMM-yyyy")
                'End If
                If IsDBNull(dr.Item("numUserID")) Then
                    ConfirmUser = ""
                Else
                    ConfirmUser = dr.Item("numUserID")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("intYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("intYear")
                End If

                If AIRSNumber <> "" And FeeYear <> "" Then
                    SQL = "update " & DBNameSpace & ".FS_FeeData set " & _
                    "strConfirmationNumber = '" & Confirmation & "', " & _
                    "strCOnfirmationUser = '" & ConfirmUser & "' " & _
                    "where strAIRSNumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '" & FeeYear & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If

            End While
            dr.Close()

            Dim FacilityName As String = ""
            Dim CompanyName As String = ""
            Dim FacilityAddress As String = ""
            'Dim OperationalStatus As String = ""
            Dim ShutDownDate As String = ""
            Dim strClass As String = ""
            Dim APCPart70 As String = ""
            Dim APCNSPS As String = ""
            Dim FacilityStreet As String = ""
            Dim FacilityCity As String = ""
            Dim FacilityState As String = ""
            Dim FacilityZipCode As String = ""

            SQL = "select * " & _
            "From " & DBNameSpace & ".FEEMailOut " & _
            "where intYear <> '2010' and intyear <> '3' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If

                If IsDBNull(dr.Item("intYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("intyear")
                End If
                If IsDBNull(dr.Item("strFacilityname")) Then
                    FacilityName = ""
                Else
                    FacilityName = dr.Item("strFacilityname")
                    If FacilityName = "N/A" Then
                        FacilityName = ""
                    End If
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    ContactPrefix = ""
                Else
                    ContactPrefix = dr.Item("strContactPrefix")
                    If ContactPrefix = "N/A" Then
                        ContactPrefix = ""
                    End If
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    ContactFirstName = ""
                Else
                    ContactFirstName = dr.Item("strContactFirstName")
                    If ContactFirstName = "N/A" Then
                        ContactFirstName = ""
                    End If
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    ContactLastName = ""
                Else
                    ContactLastName = dr.Item("strContactLastName")
                    If ContactLastName = "N/A" Then
                        ContactLastName = ""
                    End If
                End If
                If IsDBNull(dr.Item("strCompanyname")) Then
                    CompanyName = ""
                Else
                    CompanyName = dr.Item("strCompanyname")
                    If CompanyName = "N/A" Then
                        CompanyName = ""
                    End If
                End If
                If IsDBNull(dr.Item("strContactAddress")) Then
                    ContactAddress = ""
                Else
                    ContactAddress = dr.Item("strContactAddress")
                    If ContactAddress = "N/A" Then
                        ContactAddress = ""
                    End If
                End If
                If IsDBNull(dr.Item("strFacilityAddress")) Then
                    FacilityAddress = ""
                Else
                    FacilityAddress = dr.Item("strFacilityAddress")
                    If FacilityAddress = "N/A" Then
                        FacilityAddress = ""
                    End If
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    ContactCity = ""
                Else
                    ContactCity = dr.Item("strContactCity")
                    If ContactCity = "N/A" Then
                        ContactCity = ""
                    End If
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    ContactState = ""
                Else
                    ContactState = dr.Item("strContactState")
                    If ContactState = "N/A" Then
                        ContactState = ""
                    End If
                    ContactState = Mid(ContactState, 1, 2)
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    ContactZipCode = ""
                Else
                    ContactZipCode = dr.Item("strContactZipCode")
                    If ContactZipCode = "N/A" Then
                        ContactZipCode = ""
                    End If
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    ContactEmail = ""
                Else
                    ContactEmail = dr.Item("strContactEmail")
                    If ContactEmail = "N/A" Then
                        ContactEmail = ""
                    End If
                End If
                'If IsDBNull(dr.Item("strOperationalStatus")) Then
                '    OperationalStatus = ""
                'Else
                '    OperationalStatus = dr.Item("strOperationalStatus")
                'End If
                If IsDBNull(dr.Item("datShutdownDate")) Then
                    ShutDownDate = ""
                Else
                    ShutDownDate = Format(dr.Item("datShutdownDate"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    strClass = ""
                Else
                    strClass = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strAPCPart70")) Then
                    APCPart70 = "0"
                Else
                    APCPart70 = dr.Item("strAPCPart70")
                    If APCPart70 = "YES" Then
                        APCPart70 = "1"
                    Else
                        APCPart70 = "0"
                    End If
                End If
                If IsDBNull(dr.Item("strAPCNSPS")) Then
                    APCNSPS = "0"
                Else
                    APCNSPS = dr.Item("strAPCNSPS")
                    If APCNSPS = "YES" Then
                        APCNSPS = "1"
                    Else
                        APCNSPS = "0"
                    End If
                End If
                If IsDBNull(dr.Item("strFacilityStreet")) Then
                    FacilityStreet = ""
                Else
                    FacilityStreet = dr.Item("strFacilityStreet")
                    If FacilityStreet = "N/A" Then
                        FacilityStreet = ""
                    End If
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    FacilityCity = ""
                Else
                    FacilityCity = dr.Item("strFacilityCity")
                    If FacilityCity = "N/A" Then
                        FacilityCity = ""
                    End If
                End If
                If IsDBNull(dr.Item("strFacilityState")) Then
                    FacilityState = ""
                Else
                    FacilityState = dr.Item("strFacilityState")
                    If FacilityState = "N/A" Then
                        FacilityState = ""
                    End If
                    FacilityState = Mid(FacilityState, 1, 2)
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    FacilityZipCode = ""
                Else
                    FacilityZipCode = dr.Item("strFacilityZipCode")
                    If FacilityZipCode = "N/A" Then
                        FacilityZipCode = ""
                    End If
                End If

                If AIRSNumber <> "" And FeeYear <> "" Then
                    SQL = "Update " & DBNameSpace & ".FS_ContactInfo set " & _
                    "strContactFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " & _
                    "strContactlastname = '" & Replace(ContactLastName, "'", "''") & "', " & _
                    "strContactPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " & _
                    "strContactCompanyname = '" & Replace(CompanyName, "'", "''") & "', " & _
                    "strContactaddress = '" & Replace(ContactAddress, "'", "''") & "', " & _
                    "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " & _
                    "strContactState = '" & Replace(ContactState, "'", "''") & "', " & _
                    "strContactZipCode = '" & Replace(Replace(ContactZipCode, "'", "''"), "-", "") & "', " & _
                    "strContactEmail = '" & Replace(ContactEmail, "'", "''") & "' " & _
                    "where numfeeyear = '" & FeeYear & "' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()

                    SQL = "Update " & DBNameSpace & ".FS_MailOut set " & _
                    "strFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " & _
                    "strLastName = '" & Replace(ContactLastName, "'", "''") & "', " & _
                    "strPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " & _
                    "strContactCoName = '" & Replace(CompanyName, "'", "''") & "', " & _
                    "strContactAddress1 = '" & Replace(ContactAddress, "'", "''") & "', " & _
                    "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " & _
                    "strContactstate = '" & Replace(ContactState, "'", "''") & "', " & _
                    "strContactZipCode = '" & Replace(Replace(ContactZipCode, "'", "''"), "-", "") & "', " & _
                    "strGECOUserEmail = '" & Replace(ContactEmail, "'", "''") & "', " & _
                    "strClass = '" & Replace(strClass, "'", "''") & "', " & _
                    "datShutDownDate = '" & ShutDownDate & "', " & _
                    "strFacilityName = '" & Replace(FacilityName, "'", "''") & "', " & _
                    "strFacilityAddress1 = '" & Replace(FacilityAddress, "'", "''") & "', " & _
                    "strFacilitycity = '" & Replace(FacilityCity, "'", "''") & "', " & _
                    "strFacilityZipCode = '" & Replace(Replace(FacilityZipCode, "'", "''"), "-", "") & "'  " & _
                    "where numfeeyear = '" & FeeYear & "' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "Update airbranch.fs_admin set " & _
            "strEnrolled = '1' " & _
            "where intSubmittal is not null "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select distinct strairsnumber, numfeeyear " & _
            "From " & DBNameSpace & ".FS_Mailout " & _
            "order by numFeeYear, strAIRSNumber "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("numFeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("nuMFeeYear")
                End If

                If AIRSNumber <> "" And FeeYear <> "" Then
                    SQL = "update airbranch.FS_Admin set " & _
                    "strInitialMailout = '1' " & _
                    "where strAIRSNumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '" & FeeYear & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "select numFeeYear, strAIRSNumber " & _
                       "from AIRBranch.FS_Admin " & _
                       "where numFeeyear = '2010' " & _
                       "order by numfeeyear, strAIRSNumber "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FeeYear = ""
                AIRSNumber = ""

                If IsDBNull(dr.Item("numFeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("numFeeYear")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                cmd = New OracleCommand("AIRBranch.PD_FeeAmendment", Conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = FeeYear
                cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = AIRSNumber

                cmd.ExecuteNonQuery()
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FeeYear As String = ""
            Dim invoiceID As String = ""
            Dim numAmountDue As String = ""

            SQL = "select invoiceID, " & _
            "strAIRSNumber, numFeeYear " & _
            "from airbranch.FS_FeeInvoice " & _
            "  "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("InvoiceID")) Then
                    invoiceID = ""
                Else
                    invoiceID = dr.Item("InvoiceID")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("numFeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("numFeeYear")
                End If
                If invoiceID <> "" And AIRSNumber <> "" And FeeYear <> "" Then
                    numAmountDue = ""

                    SQL = "select " & _
                    "airbranch.fscalculations.strairsnumber, " & _
                    "airbranch.fscalculations.intyear, " & _
                    "case " & _
                    "when strPAymentType like 'Entire%' then (numTotalFee + numAdminFee) " & _
                    "when strPaymentType like 'Four%' then (numTotalFee + numAdminFee)/4 " & _
                    "else (numTotalFee + numAdminFee)  " & _
                    "end TotalDue " & _
                    "from airbranch.fscalculations, airbranch.fsPayAndSubmit " & _
                    "where airbranch.fsCalculations.strAIRSNumber = AIRBranch.FSPayAndSubmit.strAIRSNumber " & _
                    "and AIRBranch.FsCalculations.intyear = airbranch.fsPayandSubmit.intyear " & _
                    "and airbranch.fscalculations.intyear = '" & FeeYear & "' " & _
                    "and airbranch.fscalculations.strairsnumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("TotalDue")) Then
                            numAmountDue = "0"
                        Else
                            numAmountDue = dr2.Item("TotalDue")
                        End If
                    End While
                    dr2.Close()

                    If numAmountDue <> "" Then
                        SQL = "Update airbranch.FS_feeinvoice set " & _
                        "numAmount = '" & numAmountDue & "' " & _
                        "where invoiceID = '" & invoiceID & "' "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        dr2.Close()
                    End If
                End If
            End While
            dr.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            Dim ReferenceNumber As String = ""
            Dim Comment As String = ""

            SQL = "Select strReferenceNumber, mmoCommentArea " & _
            "from AIRbranch.ISMPREportInformation " & _
            "where strReferenceNumber not like '20%' " & _
            "and strClosed = 'False' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                ReferenceNumber = ""
                Comment = ""

                If IsDBNull(dr.Item("strReferenceNumber")) Then
                    ReferenceNumber = ""
                Else
                    ReferenceNumber = dr.Item("strReferenceNumber")
                End If
                If IsDBNull(dr.Item("mmoCommentArea")) Then
                    Comment = ""
                Else
                    Comment = dr.Item("mmoCommentArea")
                End If
                If Comment = "N/A" Then
                    Comment = ""
                End If
                Comment = "Rpt closed during mass review, MFloyd." & vbCrLf & Comment

                If ReferenceNumber <> "" Then
                    SQL = "Update airbranch.ISMPREportInformation set " & _
                    "strClosed = 'True', " & _
                    "mmoCommentArea = '" & Replace(Comment, "'", "''") & "' " & _
                    "where strReferenceNumber = '" & ReferenceNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            Dim Feeyear As String = ""
            Dim AIRSNumber As String = ""

            SQL = "select " & _
            "numFeeyear, strAIRSNumber " & _
            "from airbranch.FS_Admin " & _
            "where numCurrentStatus is  null " & _
            "order by nuMFeeyear, strAIRSNumber "


            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Feeyear = ""
                AIRSNumber = ""

                If IsDBNull(dr.Item("numFeeYear")) Then
                    Feeyear = ""
                Else
                    Feeyear = dr.Item("numFeeYear")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If Feeyear <> "" And AIRSNumber <> "" Then

                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd = New OracleCommand("AIRBranch.PD_FEE_Status", Conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = Feeyear
                    cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = AIRSNumber

                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FeeYear As String = ""
            Dim VOCTons As String = ""
            Dim PMTons As String = ""
            Dim SO2Tons As String = ""
            Dim NOXTons As String = ""
            Dim Part70Fee As String = ""
            Dim SMFee As String = ""
            Dim NSPSFee As String = ""
            Dim TotalFee As String = ""
            Dim NSPSExempt As String = ""
            Dim Operate As String = ""
            Dim FeeRate As String = ""
            Dim NSPSExemptReason As String = ""
            Dim Part70 As String = ""
            Dim SyntheticMinor As String = ""
            Dim CalculatedFee As String = ""
            Dim Class1 As String = ""
            Dim NSPS1 As String = ""
            Dim ShutDate As String = ""
            'Dim VarianceCheck As String = ""
            'Dim VarianceComments As String = ""
            Dim AdminFee As String = ""
            Dim ContactFirstName As String = ""
            Dim ContactLastName As String = ""
            Dim ContactPrefix As String = ""
            Dim ContactTitle As String = ""
            Dim ContactCompanyName As String = ""
            Dim ContactPhoneNumber As String = ""
            Dim ContactFaxNumber As String = ""
            Dim ContactEmail As String = ""
            Dim ContactAddress As String = ""
            Dim ContactCity As String = ""
            Dim ContactState As String = ""
            Dim ContactZipCode As String = ""
            Dim ModifingDate As String = ""
            Dim PaymantType As String = ""
            Dim OfficialName As String = ""
            Dim OfficialTitle As String = ""
            Dim DateSubmit As String = ""
            Dim intSubmittal As String = ""
            Dim numPayment As String = ""
            Dim datPayDate As String = ""
            Dim CheckNo As String = ""
            Dim DepositNo As String = ""
            Dim PayType As String = ""
            Dim BatchNo As String = ""
            Dim EntryPerson As String = ""
            Dim Comments As String = ""
            'Dim PayId As String = ""
            Dim InvoiceNo As String = ""
            Dim InvoiceIDCounter As Integer = 3318
            Dim Confirmation As String = ""
            'Dim DateConfirmation As String = ""
            Dim ConfirmUser As String = ""
            Dim FacilityName As String = ""
            Dim CompanyName As String = ""
            Dim FacilityAddress As String = ""
            'Dim OperationalStatus As String = ""
            Dim ShutDownDate As String = ""
            Dim strClass As String = ""
            Dim APCPart70 As String = ""
            Dim APCNSPS As String = ""
            Dim FacilityStreet As String = ""
            Dim FacilityCity As String = ""
            Dim FacilityState As String = ""
            Dim FacilityZipCode As String = ""

            'Dim Enrolled As String = ""
            'Dim InitialEnrollment As String = ""
            'Dim Enrollment As String = ""
            'Dim InitialMailout As String = ""
            'Dim MailOutSent As String = ""
            'Dim DatMailoutSent As String = ""
            'Dim datSubmittal As String = ""
            'Dim CurrentStatus As String = ""
            'Dim StatusDate As String = ""

            SQL = "select min(invoiceID) as InvoiceID " & _
            "from AIRBranch.FS_FeeInvoice "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("InvoiceID")) Then
                    Exit Sub
                Else
                    InvoiceIDCounter = dr.Item("InvoiceID") - 1
                End If
            End While
            dr.Close()

            SQL = "select distinct strairsnumber, intyear " & _
            "from airbranch.FSAddPaid " & _
            "where not exists " & _
            "(select * from airbranch.fs_Admin " & _
            "where airbranch.FSAddPaid.strairsnumber = airbranch.fs_admin.strairsnumber " & _
            "and airbranch.FSAddPaid.intyear = airbranch.fs_admin.numFeeyear) " & _
            "and strairsnumber <> '0413 ' " & _
            "and strairsnumber <> '0413' " & _
            "and intyear <> '3' " & _
            "order by intyear, strairsnumber "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr3 = cmd.ExecuteReader
            While dr3.Read
                If IsDBNull(dr3.Item("intyear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr3.Item("intYear")
                End If
                If IsDBNull(dr3.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr3.Item("strAIRSNumber")
                End If

                If FeeYear <> "" And AIRSNumber <> "" Then
                    SQL = "select * " & _
                    "from airbranch.fscalculations " & _
                    "where intyear = '" & FeeYear & "' and strairsnumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strAIRSNumber")) Then
                            AIRSNumber = ""
                        Else
                            AIRSNumber = dr.Item("strAIRSNumber")
                        End If
                        If IsDBNull(dr.Item("intYear")) Then
                            FeeYear = ""
                        Else
                            FeeYear = dr.Item("intyear")
                        End If
                        If IsDBNull(dr.Item("intVOCTons")) Then
                            VOCTons = ""
                        Else
                            VOCTons = dr.Item("intVOCTons")
                        End If

                        If IsDBNull(dr.Item("intPMTons")) Then
                            PMTons = ""
                        Else
                            PMTons = dr.Item("intPMTons")
                        End If
                        If IsDBNull(dr.Item("intSO2Tons")) Then
                            SO2Tons = ""
                        Else
                            SO2Tons = dr.Item("intSO2Tons")
                        End If
                        If IsDBNull(dr.Item("intNOXTons")) Then
                            NOXTons = ""
                        Else
                            NOXTons = dr.Item("intNOXTons")
                        End If
                        If IsDBNull(dr.Item("numPart70Fee")) Then
                            Part70Fee = ""
                        Else
                            Part70Fee = dr.Item("numPart70Fee")
                        End If
                        If IsDBNull(dr.Item("numSMFee")) Then
                            SMFee = ""
                        Else
                            SMFee = dr.Item("numSMFee")
                        End If
                        If IsDBNull(dr.Item("numNSPSFee")) Then
                            NSPSFee = ""
                        Else
                            NSPSFee = dr.Item("numNSPSFee")
                        End If
                        If IsDBNull(dr.Item("numTotalFee")) Then
                            TotalFee = ""
                        Else
                            TotalFee = dr.Item("numtotalfee")
                        End If
                        If IsDBNull(dr.Item("strNSPSExempt")) Then
                            NSPSExempt = ""
                        Else
                            NSPSExempt = dr.Item("strNSPSExempt")
                        End If
                        If IsDBNull(dr.Item("strNSPSReason")) Then
                            NSPSExemptReason = ""
                        Else
                            NSPSExemptReason = dr.Item("strNSPSReason")
                        End If
                        If IsDBNull(dr.Item("strOperate")) Then
                            Operate = ""
                        Else
                            If dr.Item("strOperate") = "YES" Then
                                Operate = "1"
                            Else
                                Operate = "0"
                            End If
                        End If
                        If IsDBNull(dr.Item("numFeeRate")) Then
                            FeeRate = ""
                        Else
                            FeeRate = dr.Item("numfeerate")
                        End If
                        'If IsDBNull(dr.Item("strNSPSExemptReason")) Then
                        '    NSPSExemptReason = ""
                        'Else
                        '    '  NSPSExemptReason = dr.Item("strNSPSExemptReason")
                        '    NSPSExemptReason = ""
                        'End If
                        If IsDBNull(dr.Item("strPart70")) Then
                            Part70 = ""
                        Else
                            If dr.Item("strPart70") = "YES" Then
                                Part70 = "1"
                            Else
                                Part70 = "0"
                            End If
                        End If
                        If IsDBNull(dr.Item("strsyntheticminor")) Then
                            SyntheticMinor = ""
                        Else
                            If dr.Item("strsyntheticminor") = "YES" Then
                                SyntheticMinor = "1"
                            Else
                                SyntheticMinor = "0"
                            End If
                        End If
                        If IsDBNull(dr.Item("numcalculatedFee")) Then
                            CalculatedFee = ""
                        Else
                            CalculatedFee = dr.Item("numcalculatedFee")
                        End If
                        If IsDBNull(dr.Item("strClass1")) Then
                            Class1 = ""
                        Else
                            Class1 = dr.Item("strClass1")
                        End If
                        If IsDBNull(dr.Item("strNSPS1")) Then
                            NSPS1 = ""
                        Else
                            If dr.Item("strNSPS1") = "YES" Then
                                NSPS1 = "1"
                            Else
                                NSPS1 = "0"
                            End If
                        End If
                        If IsDBNull(dr.Item("ShutDate")) Then
                            ShutDate = ""
                        Else
                            ShutDate = Format(dr.Item("ShutDate"), "dd-MMM-yyyy")
                        End If
                        'If IsDBNull(dr.Item("VarianceCheck")) Then
                        '    VarianceCheck = ""
                        'Else
                        '    VarianceCheck = dr.Item("VarianceCheck")
                        'End If
                        'If IsDBNull(dr.Item("VarianceComments")) Then
                        '    VarianceComments = ""
                        'Else
                        '    VarianceComments = dr.Item("VarianceComments")
                        'End If
                        If IsDBNull(dr.Item("numAdminFee")) Then
                            AdminFee = ""
                        Else
                            AdminFee = dr.Item("numAdminFee")
                        End If
                    End While
                    dr.Close()

                    If FeeYear <> "" And AIRSNumber <> "" Then
                        SQL = "Select strairsnumber " & _
                        "from " & DBNameSpace & ".FS_Admin " & _
                        "where numfeeyear = '" & FeeYear & "' " & _
                        "and strAIRSnumber = '" & AIRSNumber & "' "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        RecExist = dr2.Read
                        dr2.Close()
                        If RecExist = False Then

                            SQL = "Insert into " & DBNameSpace & ".FS_Admin " & _
                            "values " & _
                            "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', " & _
                            "'1', 'Fee Populate', " & _
                            "sysdate, sysdate) "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()

                            SQL = "Insert into " & DBNameSpace & ".FS_Mailout " & _
                            "values " & _
                            "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '" & Operate & "', " & _
                            "'" & Class1 & "', '" & NSPS1 & "', " & _
                            "'" & Part70 & "', '" & ShutDate & "', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'1', 'Fee Populate', " & _
                            "sysdate, sysdate) "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()

                            SQL = "Insert into " & DBNameSpace & ".FS_Feedata " & _
                            "values " & _
                            "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                            "'" & SyntheticMinor & "', '" & SMFee & "', " & _
                            "'" & Part70 & "', '" & Part70Fee & "', " & _
                            "'" & VOCTons & "', '" & PMTons & "', " & _
                            "'" & SO2Tons & "', '" & NOXTons & "', " & _
                            "'" & CalculatedFee & "', '" & FeeRate & "', " & _
                            "'" & NSPS1 & "', '" & NSPSFee & "', " & _
                            "'" & NSPSExempt & "', '" & NSPSExemptReason & "', " & _
                            "'" & AdminFee & "', '" & TotalFee & "', " & _
                            "'" & Class1 & "', '" & Operate & "', " & _
                            "'" & ShutDate & "', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', " & _
                            "'1', 'Fee Populate', " & _
                            "sysdate, sysdate ) "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()

                            SQL = "Insert into " & DBNameSpace & ".FS_ContactInfo " & _
                            "values " & _
                            "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', '', " & _
                            "'', " & _
                            "'1', 'Fee Populate', " & _
                            "sysdate, sysdate ) "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()
                        End If
                    End If

                    SQL = "Select * " & _
                     "From " & DBNameSpace & ".FSContactInfo " & _
                     "where intyear = '" & FeeYear & "' and strairsnumber = '" & AIRSNumber & "' " & _
                     "order by intyear "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AIRSNumber = ""
                        FeeYear = ""
                        If IsDBNull(dr.Item("strContactFirstName")) Then
                            ContactFirstName = ""
                        Else
                            ContactFirstName = dr.Item("strContactFirstName")
                        End If
                        If IsDBNull(dr.Item("strContactLastName")) Then
                            ContactLastName = ""
                        Else
                            ContactLastName = dr.Item("strContactLastName")
                        End If
                        If IsDBNull(dr.Item("strContactPrefix")) Then
                            ContactPrefix = ""
                        Else
                            ContactPrefix = dr.Item("strContactPrefix")
                        End If
                        If IsDBNull(dr.Item("strContactTitle")) Then
                            ContactTitle = ""
                        Else
                            ContactTitle = dr.Item("strContactTitle")
                        End If
                        If IsDBNull(dr.Item("strContactCompanyName")) Then
                            ContactCompanyName = ""
                        Else
                            ContactCompanyName = dr.Item("strContactCompanyName")
                        End If
                        If IsDBNull(dr.Item("strContactPhoneNumber")) Then
                            ContactPhoneNumber = ""
                        Else
                            ContactPhoneNumber = dr.Item("strContactPhoneNumber")
                        End If
                        If IsDBNull(dr.Item("strContactFaxNumber")) Then
                            ContactFaxNumber = ""
                        Else
                            ContactFaxNumber = dr.Item("strContactFaxNumber")
                        End If
                        If IsDBNull(dr.Item("strContactEmail")) Then
                            ContactEmail = ""
                        Else
                            ContactEmail = dr.Item("strContactEmail")
                        End If
                        If IsDBNull(dr.Item("strContactAddress")) Then
                            ContactAddress = ""
                        Else
                            ContactAddress = dr.Item("strContactAddress")
                        End If
                        If IsDBNull(dr.Item("strContactCity")) Then
                            ContactCity = ""
                        Else
                            ContactCity = dr.Item("strContactCity")
                        End If
                        If IsDBNull(dr.Item("strContactState")) Then
                            ContactState = ""
                        Else
                            ContactState = dr.Item("strContactState")
                        End If
                        If IsDBNull(dr.Item("strContactZipCode")) Then
                            ContactZipCode = ""
                        Else
                            ContactZipCode = dr.Item("strContactZipCode")
                        End If
                        If IsDBNull(dr.Item("strAIRSNumber")) Then
                            AIRSNumber = ""
                        Else
                            AIRSNumber = dr.Item("strAIRSNumber")
                        End If
                        If IsDBNull(dr.Item("intYear")) Then
                            FeeYear = ""
                        Else
                            FeeYear = dr.Item("intYear")
                        End If
                        If IsDBNull(dr.Item("datModifyDate")) Then
                            ModifingDate = OracleDate
                        Else
                            ModifingDate = Format(dr.Item("datModifyDate"), "dd-MMM-yyyy")
                        End If

                        If AIRSNumber <> "" And FeeYear <> "" Then
                            SQL = "Update " & DBNameSpace & ".FS_ContactInfo set " & _
                            "strContactFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " & _
                            "strContactLastName = '" & Replace(ContactLastName, "'", "''") & "', " & _
                            "strContactPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " & _
                            "strContactTitle = '" & Replace(ContactTitle, "'", "''") & "', " & _
                            "strContactCompanyName = '" & Replace(ContactCompanyName, "'", "''") & "', " & _
                            "strcontactAddress = '" & Replace(ContactAddress, "'", "''") & "', " & _
                            "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " & _
                            "strContactState = '" & Replace(ContactState, "'", "''") & "', " & _
                            "strContactZipCode = '" & Replace(ContactZipCode, "'", "''") & "', " & _
                            "strContactPhoneNumber = '" & Replace(ContactPhoneNumber, "'", "''") & "', " & _
                            "strContactFaxNumber = '" & Replace(ContactFaxNumber, "'", "''") & "', " & _
                            "strContactEmail = '" & Replace(ContactEmail, "'", "''") & "', " & _
                            "UpdateDateTime = '" & ModifingDate & "', " & _
                            "CreateDateTime = '" & ModifingDate & "' " & _
                            "where numFeeyear = '" & FeeYear & "' " & _
                            "and strAIRSNumber = '" & AIRSNumber & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()
                        End If
                    End While
                    dr.Close()


                    SQL = "Select * " & _
                "From " & DBNameSpace & ".FSPayAndSubmit " & _
             "where intyear = '" & FeeYear & "' and strairsnumber = '" & AIRSNumber & "' " & _
                "order by intyear "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AIRSNumber = ""
                        FeeYear = ""

                        If IsDBNull(dr.Item("strPaymentType")) Then
                            PaymantType = ""
                        Else
                            PaymantType = dr.Item("strPaymentType")
                            If PaymantType = "N/A" Then
                                PaymantType = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strOfficialName")) Then
                            OfficialName = ""
                        Else
                            OfficialName = dr.Item("strOfficialName")
                        End If
                        If IsDBNull(dr.Item("strOfficialTitle")) Then
                            OfficialTitle = ""
                        Else
                            OfficialTitle = dr.Item("strOfficialTitle")
                        End If
                        If IsDBNull(dr.Item("dateSubmit")) Then
                            DateSubmit = OracleDate
                        Else
                            DateSubmit = Format(dr.Item("dateSubmit"), "dd-MMM-yyyy")
                        End If
                        If IsDBNull(dr.Item("intSubmittal")) Then
                            intSubmittal = "0"
                        Else
                            intSubmittal = dr.Item("intSubmittal")
                        End If
                        If IsDBNull(dr.Item("strAIRSNUmber")) Then
                            AIRSNumber = ""
                        Else
                            AIRSNumber = dr.Item("strAIRSNumber")
                        End If
                        If IsDBNull(dr.Item("intYear")) Then
                            FeeYear = ""
                        Else
                            FeeYear = dr.Item("intYear")
                        End If

                        If AIRSNumber <> "" And FeeYear <> "" Then
                            SQL = "Update " & DBNameSpace & ".FS_FeeData set " & _
                            "strOfficialName = '" & Replace(OfficialName, "'", "''") & "', " & _
                            "strOfficialTitle = '" & Replace(OfficialTitle, "'", "''") & "', " & _
                            "strPaymentPlan = '" & PaymantType & "', " & _
                            "UpdateDateTime = '" & DateSubmit & "' " & _
                            "where numFeeYear = '" & FeeYear & "' " & _
                            "and strAIRSNumber = '" & AIRSNumber & "' "
                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()

                            SQL = "Update " & DBNameSpace & ".FS_Admin set " & _
                            "intSubmittal = '" & intSubmittal & "', " & _
                            "datSubmittal = '" & DateSubmit & "', " & _
                            "updateDateTime = '" & DateSubmit & "' " & _
                            "where numFeeYear = '" & FeeYear & "' " & _
                            "and strAIRSNumber = '" & AIRSNumber & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()
                        End If

                    End While
                    dr.Close()


                    SQL = "Select * " & _
                    "from " & DBNameSpace & ".FSAddPaid " & _
                    "where intyear = '" & FeeYear & "' and strairsnumber = '" & AIRSNumber & "' " & _
                    "order by intyear "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AIRSNumber = ""
                        FeeYear = ""

                        If IsDBNull(dr.Item("numPayment")) Then
                            numPayment = ""
                        Else
                            numPayment = dr.Item("numPayment")
                        End If
                        If IsDBNull(dr.Item("datPayDate")) Then
                            datPayDate = OracleDate
                        Else
                            datPayDate = Format(dr.Item("datPayDate"), "dd-MMM-yyyy")
                        End If
                        If IsDBNull(dr.Item("strCheckNo")) Then
                            CheckNo = ""
                        Else
                            CheckNo = dr.Item("strCheckNo")
                        End If
                        If IsDBNull(dr.Item("strDepositNo")) Then
                            DepositNo = ""
                        Else
                            DepositNo = dr.Item("strDepositNo")
                        End If
                        If IsDBNull(dr.Item("strPayType")) Then
                            PayType = "8"
                        Else
                            PayType = dr.Item("strPayType")
                            Select Case PayType
                                Case "ANNUAL"
                                    PayType = "1"
                                Case "QUARTER ONE"
                                    PayType = "2"
                                Case "QUARTER TWO"
                                    PayType = "3"
                                Case "QUARTER THREE"
                                    PayType = "4"
                                Case "QUARTER FOUR"
                                    PayType = "5"
                                Case "AMENDMENT"
                                    PayType = "6"
                                Case "REFUND"
                                    PayType = "7"
                                Case "ONE-TIME"
                                    PayType = "8"
                                Case Else
                                    PayType = "8"
                            End Select
                        End If
                        If IsDBNull(dr.Item("strBatchNo")) Then
                            BatchNo = ""
                        Else
                            BatchNo = dr.Item("strBatchNo")
                        End If
                        If IsDBNull(dr.Item("strEntryPerson")) Then
                            EntryPerson = ""
                        Else
                            EntryPerson = dr.Item("strEntryPerson")
                        End If
                        If IsDBNull(dr.Item("strComments")) Then
                            Comments = ""
                        Else
                            Comments = dr.Item("strComments")
                        End If
                        If IsDBNull(dr.Item("strAIRSNumber")) Then
                            AIRSNumber = ""
                        Else
                            AIRSNumber = dr.Item("strAIRSNumber")
                        End If
                        If IsDBNull(dr.Item("intFiscalYear")) Then
                            If IsDBNull(dr.Item("intYear")) Then
                                FeeYear = ""
                            Else
                                FeeYear = dr.Item("intYear")
                            End If
                        Else
                            FeeYear = dr.Item("intFiscalYear")
                        End If
                        If IsDBNull(dr.Item("strINvoiceNo")) Then
                            InvoiceNo = ""
                        Else
                            Comments = Comments & vbCrLf & "Old Fee System Invoice ID: " & dr.Item("strInvoiceNO")
                        End If
                        InvoiceNo = InvoiceIDCounter
                        InvoiceIDCounter -= 1

                        SQL = "Insert into " & DBNameSpace & ".FS_FeeInvoice " & _
                        "values " & _
                        "('" & InvoiceNo & "', '" & AIRSNumber & "', " & _
                        "'" & FeeYear & "', '" & numPayment & "', " & _
                        "'" & datPayDate & "', '" & Replace(Comments, "'", "''") & "', " & _
                        "'1', 'Fee Populate', " & _
                        "sysdate, sysdate, " & _
                        "'" & PayType & "', '1') "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into " & DBNameSpace & ".FS_Transactions " & _
                        "values " & _
                        "((" & DBNameSpace & ".seq_fs_transactions.nextVal), '" & InvoiceNo & "', " & _
                        "'1', '" & datPayDate & "', " & _
                        "'" & numPayment & "', '" & CheckNo & "', " & _
                        "'" & DepositNo & "', '" & BatchNo & "', " & _
                        "'" & EntryPerson & "', '" & Replace(Comments, "'", "''") & "', " & _
                        "'1', '', " & _
                        "sysdate, sysdate, " & _
                        "'" & AIRSNumber & "', '" & FeeYear & "', " & _
                        "'') "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        dr2.Close()

                    End While
                    dr.Close()

                    SQL = "select * " & _
                    "from " & DBNameSpace & ".FSConfirmation " & _
                    "where intyear = '" & FeeYear & "' and strairsnumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strConfirmation")) Then
                            Confirmation = ""
                        Else
                            Confirmation = dr.Item("strConfirmation")
                        End If
                        'If IsDBNull(dr.Item("datConfirmation")) Then
                        '    DateConfirmation = ""
                        'Else
                        '    DateConfirmation = Format(dr.Item("datConfirmation"), "dd-MMM-yyyy")
                        'End If
                        If IsDBNull(dr.Item("numUserID")) Then
                            ConfirmUser = ""
                        Else
                            ConfirmUser = dr.Item("numUserID")
                        End If
                        If IsDBNull(dr.Item("strAIRSNumber")) Then
                            AIRSNumber = ""
                        Else
                            AIRSNumber = dr.Item("strAIRSNumber")
                        End If
                        If IsDBNull(dr.Item("intYear")) Then
                            FeeYear = ""
                        Else
                            FeeYear = dr.Item("intYear")
                        End If

                        If AIRSNumber <> "" And FeeYear <> "" Then
                            SQL = "update " & DBNameSpace & ".FS_FeeData set " & _
                            "strConfirmationNumber = '" & Confirmation & "', " & _
                            "strCOnfirmationUser = '" & ConfirmUser & "' " & _
                            "where strAIRSNumber = '" & AIRSNumber & "' " & _
                            "and numFeeYear = '" & FeeYear & "' "
                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()
                        End If

                    End While
                    dr.Close()

                    SQL = "select * " & _
                    "From " & DBNameSpace & ".FEEMailOut " & _
                    "where intyear = '" & FeeYear & "' and strairsnumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strAIRSNumber")) Then
                            AIRSNumber = ""
                        Else
                            AIRSNumber = dr.Item("strAIRSNumber")
                        End If

                        If IsDBNull(dr.Item("intYear")) Then
                            FeeYear = ""
                        Else
                            FeeYear = dr.Item("intyear")
                        End If
                        If IsDBNull(dr.Item("strFacilityname")) Then
                            FacilityName = ""
                        Else
                            FacilityName = dr.Item("strFacilityname")
                            If FacilityName = "N/A" Then
                                FacilityName = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactPrefix")) Then
                            ContactPrefix = ""
                        Else
                            ContactPrefix = dr.Item("strContactPrefix")
                            If ContactPrefix = "N/A" Then
                                ContactPrefix = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactFirstName")) Then
                            ContactFirstName = ""
                        Else
                            ContactFirstName = dr.Item("strContactFirstName")
                            If ContactFirstName = "N/A" Then
                                ContactFirstName = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactLastName")) Then
                            ContactLastName = ""
                        Else
                            ContactLastName = dr.Item("strContactLastName")
                            If ContactLastName = "N/A" Then
                                ContactLastName = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strCompanyname")) Then
                            CompanyName = ""
                        Else
                            CompanyName = dr.Item("strCompanyname")
                            If CompanyName = "N/A" Then
                                CompanyName = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactAddress")) Then
                            ContactAddress = ""
                        Else
                            ContactAddress = dr.Item("strContactAddress")
                            If ContactAddress = "N/A" Then
                                ContactAddress = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strFacilityAddress")) Then
                            FacilityAddress = ""
                        Else
                            FacilityAddress = dr.Item("strFacilityAddress")
                            If FacilityAddress = "N/A" Then
                                FacilityAddress = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactCity")) Then
                            ContactCity = ""
                        Else
                            ContactCity = dr.Item("strContactCity")
                            If ContactCity = "N/A" Then
                                ContactCity = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactState")) Then
                            ContactState = ""
                        Else
                            ContactState = dr.Item("strContactState")
                            If ContactState = "N/A" Then
                                ContactState = ""
                            End If
                            ContactState = Mid(ContactState, 1, 2)
                        End If
                        If IsDBNull(dr.Item("strContactZipCode")) Then
                            ContactZipCode = ""
                        Else
                            ContactZipCode = dr.Item("strContactZipCode")
                            If ContactZipCode = "N/A" Then
                                ContactZipCode = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactEmail")) Then
                            ContactEmail = ""
                        Else
                            ContactEmail = dr.Item("strContactEmail")
                            If ContactEmail = "N/A" Then
                                ContactEmail = ""
                            End If
                        End If
                        'If IsDBNull(dr.Item("strOperationalStatus")) Then
                        '    OperationalStatus = ""
                        'Else
                        '    OperationalStatus = dr.Item("strOperationalStatus")
                        'End If
                        If IsDBNull(dr.Item("datShutdownDate")) Then
                            ShutDownDate = ""
                        Else
                            ShutDownDate = Format(dr.Item("datShutdownDate"), "dd-MMM-yyyy")
                        End If
                        If IsDBNull(dr.Item("strClass")) Then
                            strClass = ""
                        Else
                            strClass = dr.Item("strClass")
                        End If
                        If IsDBNull(dr.Item("strAPCPart70")) Then
                            APCPart70 = "0"
                        Else
                            APCPart70 = dr.Item("strAPCPart70")
                            If APCPart70 = "YES" Then
                                APCPart70 = "1"
                            Else
                                APCPart70 = "0"
                            End If
                        End If
                        If IsDBNull(dr.Item("strAPCNSPS")) Then
                            APCNSPS = "0"
                        Else
                            APCNSPS = dr.Item("strAPCNSPS")
                            If APCNSPS = "YES" Then
                                APCNSPS = "1"
                            Else
                                APCNSPS = "0"
                            End If
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet")) Then
                            FacilityStreet = ""
                        Else
                            FacilityStreet = dr.Item("strFacilityStreet")
                            If FacilityStreet = "N/A" Then
                                FacilityStreet = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacilityCity = ""
                        Else
                            FacilityCity = dr.Item("strFacilityCity")
                            If FacilityCity = "N/A" Then
                                FacilityCity = ""
                            End If
                        End If
                        If IsDBNull(dr.Item("strFacilityState")) Then
                            FacilityState = ""
                        Else
                            FacilityState = dr.Item("strFacilityState")
                            If FacilityState = "N/A" Then
                                FacilityState = ""
                            End If
                            FacilityState = Mid(FacilityState, 1, 2)
                        End If
                        If IsDBNull(dr.Item("strFacilityZipCode")) Then
                            FacilityZipCode = ""
                        Else
                            FacilityZipCode = dr.Item("strFacilityZipCode")
                            If FacilityZipCode = "N/A" Then
                                FacilityZipCode = ""
                            End If
                        End If

                        If AIRSNumber <> "" And FeeYear <> "" Then
                            SQL = "Update " & DBNameSpace & ".FS_ContactInfo set " & _
                            "strContactFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " & _
                            "strContactlastname = '" & Replace(ContactLastName, "'", "''") & "', " & _
                            "strContactPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " & _
                            "strContactCompanyname = '" & Replace(CompanyName, "'", "''") & "', " & _
                            "strContactaddress = '" & Replace(ContactAddress, "'", "''") & "', " & _
                            "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " & _
                            "strContactState = '" & Replace(ContactState, "'", "''") & "', " & _
                            "strContactZipCode = '" & Replace(Replace(ContactZipCode, "'", "''"), "-", "") & "', " & _
                            "strContactEmail = '" & Replace(ContactEmail, "'", "''") & "' " & _
                            "where numfeeyear = '" & FeeYear & "' " & _
                            "and strAIRSNumber = '" & AIRSNumber & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()

                            SQL = "Update " & DBNameSpace & ".FS_MailOut set " & _
                            "strFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " & _
                            "strLastName = '" & Replace(ContactLastName, "'", "''") & "', " & _
                            "strPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " & _
                            "strContactCoName = '" & Replace(CompanyName, "'", "''") & "', " & _
                            "strContactAddress1 = '" & Replace(ContactAddress, "'", "''") & "', " & _
                            "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " & _
                            "strContactstate = '" & Replace(ContactState, "'", "''") & "', " & _
                            "strContactZipCode = '" & Replace(Replace(ContactZipCode, "'", "''"), "-", "") & "', " & _
                            "strGECOUserEmail = '" & Replace(ContactEmail, "'", "''") & "', " & _
                            "strClass = '" & Replace(strClass, "'", "''") & "', " & _
                            "datShutDownDate = '" & ShutDownDate & "', " & _
                            "strFacilityName = '" & Replace(FacilityName, "'", "''") & "', " & _
                            "strFacilityAddress1 = '" & Replace(FacilityAddress, "'", "''") & "', " & _
                            "strFacilitycity = '" & Replace(FacilityCity, "'", "''") & "', " & _
                            "strFacilityZipCode = '" & Replace(Replace(FacilityZipCode, "'", "''"), "-", "") & "'  " & _
                            "where numfeeyear = '" & FeeYear & "' " & _
                            "and strAIRSNumber = '" & AIRSNumber & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()
                        End If
                    End While
                    dr.Close()
                End If
            End While
            dr3.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Try
            Dim FeeYear As String = ""
            Dim AIRSNumber As String = ""
            Dim classification As String = ""
            Dim OpStauts As String = ""

            SQL = "select " & _
            "numfeeyear, strairsnumber " & _
            "from AIRBranch.FS_Admin " & _
            "where nuMFeeyear = '2010' "

            SQL = "select numfeeyear, airbranch.fs_admin.strairsnumber, " & _
            "strClass, " & _
            "case " & _
            "when strOPerationalstatus = 'O' then '1' " & _
            "else '0' " & _
            "end opStatus " & _
            "from AIRBranch.FS_Admin, AIRBranch.APBHeaderData " & _
            "where nuMFeeyear = '2010' " & _
            "and airbranch.fs_admin.strAIRSNumber = AIRBranch.APBHeaderData.strairsnumber "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                FeeYear = ""
                AIRSNumber = ""
                classification = ""
                OpStauts = ""

                If IsDBNull(dr.Item("numFeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("numFeeyear")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("OpStatus")) Then
                    OpStauts = ""
                Else
                    OpStauts = dr.Item("OpStatus")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    classification = ""
                Else
                    classification = dr.Item("strClass")
                End If

                If FeeYear <> "" And AIRSNumber <> "" Then
                    SQL = "Select " & _
                    "numFeeyear, strairsnumber " & _
                    "from airbranch.FS_Feedata  " & _
                    "where numFeeYear = '" & FeeYear & "' " & _
                    "and strAIRSNumber = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    RecExist = dr2.Read
                    dr2.Close()

                    If RecExist = False Then
                        SQL = "Insert into AIRBranch.FS_FeeData " & _
                        "(numFeeYear, strAIRSNumber, " & _
                        "strClass, strOperate, " & _
                        "active, updateUSer, " & _
                        "updateDateTime, CreateDateTime) " & _
                        "" & _
                        "values " & _
                        "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                        "'" & classification & "', '" & OpStauts & "',  " & _
                        "'1', 'Fee Populate', sysdate, sysdate) "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        dr2.Close()
                    End If
                End If

            End While
            dr.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FeeYear As String = ""
            'Dim DateSubmit As String = ""
            'Dim SyntheticMinor As String = ""
            'Dim SMFee As String = ""
            'Dim Part70 As String = ""
            'Dim Part70Fee As String = ""
            'Dim VOCTons As String = ""
            'Dim PMTons As String = ""
            'Dim SO2Tons As String = ""
            'Dim NOXTons As String = ""
            'Dim CalculatedFee As String = ""
            'Dim FeeRate As String = ""
            'Dim NSPS As String = ""
            'Dim NSPSFee As String = ""
            'Dim NSPSExempt As String = ""
            'Dim NSPSExemptReason As String = ""
            'Dim AdminFee As String = ""
            'Dim TotalFee As String = ""
            'Dim Classification As String = ""
            'Dim Operate As String = ""
            'Dim ShutDown As String = ""
            'Dim OfficialName As String = ""
            'Dim OfficialTitle As String = ""
            'Dim PaymentPlan As String = ""
            'Dim Active As String = ""
            'Dim UpdateUser As String = ""
            'Dim UpdateDateTime As String = ""
            'Dim TransactionID As String = ""
            Dim Comment As String = ""
            Dim NewFeeYear As String = ""
            Dim InvoiceID As String = ""


            SQL = "select strcomment, numfeeyear, strairsnumber, invoiceid, " & _
            "substr(strComment, length(strcomment) - 3) as newFeeYear " & _
            "from AIRBranch.FS_feeinvoice   " & _
            "where strComment not like '%'||numfeeyear " & _
            "and strcomment like '%Old Fee System Invoice%' " & _
            "and active = '1' " & _
            "order by numfeeyear desc , strairsnumber "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numFeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("nuMFeeYear")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("invoiceid")) Then
                    invoiceid = ""
                Else
                    invoiceid = dr.Item("invoiceid")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    Comment = ""
                Else
                    Comment = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("newFeeyear")) Then
                    NewFeeYear = ""
                Else
                    NewFeeYear = dr.Item("newFeeyear")
                End If

                If FeeYear <> "" And AIRSNumber <> "" And Comment <> "" And NewFeeYear <> "" Then
                    If IsNumeric(NewFeeYear) Then
                        If NewFeeYear.Length = 4 Then
                            SQL = "Update AIRBranch.FS_feeinvoice set " & _
                            "numFeeYear = '" & NewFeeYear & "' " & _
                            "where invoiceid = '" & InvoiceID & "' " & _
                            "and strAIRSNumber = '" & AIRSNumber & "' " & _
                            "and numFeeYear = '" & FeeYear & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            cmd.ExecuteNonQuery()

                        End If
                    End If
                End If

            End While
            dr.Close()



            'SQL = "select strcomment, numfeeyear, strairsnumber, transactionid, " & _
            '"substr(strComment, length(strcomment) - 3) as newFeeYear " & _
            '"from AIRBranch.FS_Transactions   " & _
            '"where strComment not like '%'||numfeeyear " & _
            '"and strcomment like '%Old Fee System Invoice%' " & _
            '"and active = '1' " & _
            '"order by numfeeyear desc , strairsnumber "

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    If IsDBNull(dr.Item("numFeeYear")) Then
            '        FeeYear = ""
            '    Else
            '        FeeYear = dr.Item("nuMFeeYear")
            '    End If
            '    If IsDBNull(dr.Item("strAIRSNumber")) Then
            '        AIRSNumber = ""
            '    Else
            '        AIRSNumber = dr.Item("strAIRSNumber")
            '    End If
            '    If IsDBNull(dr.Item("TransactionID")) Then
            '        TransactionID = ""
            '    Else
            '        TransactionID = dr.Item("TransactionID")
            '    End If
            '    If IsDBNull(dr.Item("strComment")) Then
            '        Comment = ""
            '    Else
            '        Comment = dr.Item("strComment")
            '    End If
            '    If IsDBNull(dr.Item("newFeeyear")) Then
            '        NewFeeYear = ""
            '    Else
            '        NewFeeYear = dr.Item("newFeeyear")
            '    End If

            '    If FeeYear <> "" And AIRSNumber <> "" And Comment <> "" And NewFeeYear <> "" Then
            '        If IsNumeric(NewFeeYear) Then
            '            If NewFeeYear.Length = 4 Then
            '                SQL = "Update AIRBranch.FS_Transactions set " & _
            '                "numFeeYear = '" & NewFeeYear & "' " & _
            '                "where TransactionID = '" & TransactionID & "' " & _
            '                "and strAIRSNumber = '" & AIRSNumber & "' " & _
            '                "and numFeeYear = '" & FeeYear & "' "

            '                cmd = New OracleCommand(SQL, conn)
            '                If conn.State = ConnectionState.Closed Then
            '                    conn.Open()
            '                End If
            '                cmd.ExecuteNonQuery()

            '            End If
            '        End If
            '    End If

            'End While
            'dr.Close()

            'SQL = "select * " & _
            '"from AIRBranch.FS_FeeData " & _
            '"where not exists (select * " & _
            '"from AIRBranch.FS_FeeAuditedData " & _
            '"where AIRBranch.FS_Feedata.strAIRSNumber = AIRBranch.FS_FeeAuditedData.strAIRSNumber " & _
            '"and airbranch.FS_FeeData.numFeeyear = AIRBranch.FS_FeeAuditedData.nuMFeeYear) "

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    If IsDBNull(dr.Item("nuMFeeYear")) Then
            '        FeeYear = ""
            '    Else
            '        FeeYear = dr.Item("nuMFeeYear")
            '    End If
            '    If IsDBNull(dr.Item("strAIRSNumber")) Then
            '        AIRSNumber = ""
            '    Else
            '        AIRSNumber = dr.Item("strAIRSNumber")
            '    End If

            '    If IsDBNull(dr.Item("strSyntheticMinor")) Then
            '        SyntheticMinor = ""
            '    Else
            '        SyntheticMinor = dr.Item("strSyntheticMinor")
            '    End If
            '    If IsDBNull(dr.Item("numSMFee")) Then
            '        SMFee = ""
            '    Else
            '        SMFee = dr.Item("numSMFee")
            '    End If
            '    If IsDBNull(dr.Item("strPart70")) Then
            '        Part70 = ""
            '    Else
            '        Part70 = dr.Item("strPart70")
            '    End If
            '    If IsDBNull(dr.Item("numPart70Fee")) Then
            '        Part70Fee = ""
            '    Else
            '        Part70Fee = dr.Item("numPart70Fee")
            '    End If
            '    If IsDBNull(dr.Item("intVoCTons")) Then
            '        VOCTons = ""
            '    Else
            '        VOCTons = dr.Item("intVOCTons")
            '    End If
            '    If IsDBNull(dr.Item("intPMTons")) Then
            '        PMTons = ""
            '    Else
            '        PMTons = dr.Item("intPMTons")
            '    End If
            '    If IsDBNull(dr.Item("intSO2Tons")) Then
            '        SO2Tons = ""
            '    Else
            '        SO2Tons = dr.Item("intSO2Tons")
            '    End If
            '    If IsDBNull(dr.Item("intNOXTons")) Then
            '        NOXTons = ""
            '    Else
            '        NOXTons = dr.Item("intNOXTons")
            '    End If
            '    If IsDBNull(dr.Item("numCalculatedFee")) Then
            '        CalculatedFee = ""
            '    Else
            '        CalculatedFee = dr.Item("numCalculatedFee")
            '    End If
            '    If IsDBNull(dr.Item("numFeeRate")) Then
            '        FeeRate = ""
            '    Else
            '        FeeRate = dr.Item("numFeeRate")
            '    End If
            '    If IsDBNull(dr.Item("strNSPS")) Then
            '        NSPS = ""
            '    Else
            '        NSPS = dr.Item("strNSPS")
            '    End If
            '    If IsDBNull(dr.Item("numNSPSFee")) Then
            '        NSPSFee = ""
            '    Else
            '        NSPSFee = dr.Item("numNSPSFee")
            '    End If
            '    If IsDBNull(dr.Item("strNSPSExempt")) Then
            '        NSPSExempt = ""
            '    Else
            '        NSPSExempt = dr.Item("strNSPSExempt")
            '    End If
            '    If IsDBNull(dr.Item("strNSPSExemptReason")) Then
            '        NSPSExemptReason = ""
            '    Else
            '        NSPSExemptReason = dr.Item("strNSPSExemptReason")
            '    End If
            '    If IsDBNull(dr.Item("numAdminFee")) Then
            '        AdminFee = ""
            '    Else
            '        AdminFee = dr.Item("numAdminFee")
            '    End If
            '    If IsDBNull(dr.Item("numtotalFee")) Then
            '        TotalFee = ""
            '    Else
            '        TotalFee = dr.Item("numTotalFee")
            '    End If
            '    If IsDBNull(dr.Item("strClass")) Then
            '        Classification = ""
            '    Else
            '        Classification = dr.Item("strClass")
            '    End If
            '    If IsDBNull(dr.Item("strOperate")) Then
            '        Operate = ""
            '    Else
            '        Operate = dr.Item("strOperate")
            '    End If
            '    If IsDBNull(dr.Item("datShutDown")) Then
            '        ShutDown = ""
            '    Else
            '        ShutDown = dr.Item("datShutDown")
            '    End If
            '    If IsDBNull(dr.Item("strOfficialName")) Then
            '        OfficialName = ""
            '    Else
            '        OfficialName = dr.Item("strOfficialName")
            '    End If
            '    If IsDBNull(dr.Item("strOfficialTitle")) Then
            '        OfficialTitle = ""
            '    Else
            '        OfficialTitle = dr.Item("strOfficialTitle")
            '    End If
            '    If IsDBNull(dr.Item("strPaymentPlan")) Then
            '        PaymentPlan = ""
            '    Else
            '        PaymentPlan = dr.Item("strPaymentPlan")
            '    End If
            '    If IsDBNull(dr.Item("Active")) Then
            '        Active = "0"
            '    Else
            '        Active = dr.Item("Active")
            '    End If
            '    If IsDBNull(dr.Item("UpdateUser")) Then
            '        UpdateUser = ""
            '    Else
            '        UpdateUser = dr.Item("updateUser")
            '    End If
            '    If IsDBNull(dr.Item("UpdateDateTime")) Then
            '        UpdateDateTime = ""
            '    Else
            '        UpdateDateTime = Format(dr.Item("UpdateDateTime"), "dd-MMM-yyyy")
            '    End If

            '    If FeeYear <> "" And AIRSNumber <> "" Then
            '        SQL = "select strAIRSNumber " & _
            '        "from AIRBranch.FS_FeeAuditedData " & _
            '        "where numFeeyear = '" & FeeYear & "' " & _
            '        "and strAIRSnumber = '" & AIRSNumber & "' "
            '        cmd = New OracleCommand(SQL, conn)
            '        If conn.State = ConnectionState.Closed Then
            '            conn.Open()
            '        End If

            '        dr2 = cmd.ExecuteReader
            '        RecExist = dr2.Read
            '        dr2.Close()

            '        If RecExist = False Then
            '            SQL = "insert into AIRBranch.FS_FeeAuditedData " & _
            '            "values " & _
            '            "('" & AIRSNumber & "', '" & FeeYear & "', " & _
            '            "'" & SyntheticMinor & "', '" & SMFee & "', " & _
            '            "'" & Part70 & "', '" & Part70Fee & "', " & _
            '            "'" & VOCTons & "', '" & PMTons & "', " & _
            '            "'" & SO2Tons & "', '" & NOXTons & "', " & _
            '            "'" & CalculatedFee & "', '" & FeeRate & "', " & _
            '            "'" & NSPS & "', '" & NSPSFee & "', " & _
            '            "'" & NSPSExempt & "', '" & NSPSExemptReason & "', " & _
            '            "'" & AdminFee & "', '" & TotalFee & "', " & _
            '            "'" & Classification & "', '" & Operate & "', " & _
            '            "'" & ShutDown & "', '" & OfficialName & "', " & _
            '            "'" & OfficialTitle & "', '" & PaymentPlan & "', " & _
            '            "'" & Active & "', '" & UpdateUser & "', " & _
            '            "'" & UpdateDateTime & "') "
            '            cmd = New OracleCommand(SQL, conn)
            '            If conn.State = ConnectionState.Closed Then
            '                conn.Open()
            '            End If
            '            cmd.ExecuteNonQuery()
            '        End If
            '    End If
            'End While
            'dr.Close()

            'SQL = "Select " & _
            '"strAIRSNumber, intYear " & _
            '"from AIRBranch.FeeMailout  " & _
            '"where intyear <> '3' and intyear <> '2010' " & _
            '"order by intyear, strairsnumber "

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    If IsDBNull(dr.Item("strAIRSNumber")) Then
            '        AIRSNumber = ""
            '    Else
            '        AIRSNumber = dr.Item("strAIRSNumber")
            '    End If
            '    If IsDBNull(dr.Item("intYear")) Then
            '        FeeYear = ""
            '    Else
            '        FeeYear = dr.Item("intYear")
            '    End If

            '    SQL = "Update airbranch.FS_Admin set " & _
            '    "strInitialMailout = '1',   " & _
            '    "strMailoutSent = '1' " & _
            '    "where nuMFeeyear = '" & FeeYear & "' " & _
            '    "and strAIRSNumber = '" & AIRSNumber & "' "

            '    cmd = New OracleCommand(SQL, conn)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    cmd.ExecuteNonQuery()

            'End While
            'dr.Close()

            ''''##This was to update enrollment status' 
            'SQL = "Select " & _
            '"strAIRSNumber, intYear, dateSubmit  " & _
            '"from AIRBranch.FSPayAndSubmit " & _
            '"where intyear <> '3' and intyear <> '2010' " & _
            '"order by intyear, strairsnumber "

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    If IsDBNull(dr.Item("strAIRSNumber")) Then
            '        AIRSNumber = ""
            '    Else
            '        AIRSNumber = dr.Item("strAIRSNumber")
            '    End If
            '    If IsDBNull(dr.Item("intYear")) Then
            '        FeeYear = ""
            '    Else
            '        FeeYear = dr.Item("intYear")
            '    End If
            '    If IsDBNull(dr.Item("dateSubmit")) Then
            '        DateSubmit = ""
            '    Else
            '        DateSubmit = Format(dr.Item("dateSubmit"), "dd-MMM-yyyy")
            '    End If

            '    SQL = "Update airbranch.FS_Admin set " & _
            '    "strEnrolled = '1', " & _
            '    "datInitialEnrollment = '" & DateSubmit & "' " & _
            '    "where nuMFeeyear = '" & FeeYear & "' " & _
            '    "and strAIRSNumber = '" & AIRSNumber & "' "
            '    cmd = New OracleCommand(SQL, conn)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    cmd.ExecuteNonQuery()

            'End While
            'dr.Close()

            'SQL = "Select " & _
            '"strAIRSNumber, intYear " & _
            '"from AIRBranch.FeeMailOut " & _
            '"where intyear <> '3' and intyear <> '2010' " & _
            '"order by intyear, strairsnumber "

            'SQL = "Select " & _
            '"strAIRSNumber, intYear " & _
            '"from AIRBranch.FSConfirmation " & _
            '"where intyear <> '3' and intyear <> '2010' " & _
            '"order by intyear, strairsnumber "

            'SQL = "Select " & _
            '"strAIRSNumber, intYear " & _
            '"from AIRBranch.FeeMailout  " & _
            '"where intyear <> '3' and intyear <> '2010' " & _
            '"order by intyear, strairsnumber "

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    If IsDBNull(dr.Item("strAIRSNumber")) Then
            '        AIRSNumber = ""
            '    Else
            '        AIRSNumber = dr.Item("strAIRSNumber")
            '    End If
            '    If IsDBNull(dr.Item("intYear")) Then
            '        FeeYear = ""
            '    Else
            '        FeeYear = dr.Item("intYear")
            '    End If

            '    SQL = "Update airbranch.FS_Admin set " & _
            '    "strEnrolled = '1'   " & _
            '    "where nuMFeeyear = '" & FeeYear & "' " & _
            '    "and strAIRSNumber = '" & AIRSNumber & "' " & _
            '    "and strEnrolled = '0' "

            '    cmd = New OracleCommand(SQL, conn)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    cmd.ExecuteNonQuery()

            'End While
            'dr.Close()



        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FeeYear As String = ""
            Dim Enrolled As String = ""
            Dim InitialEnrollment As String = ""
            Dim Enrollment As String = ""
            Dim InitialMailout As String = ""
            Dim MailoutSent As String = ""
            Dim DateMailoutSent As String = ""
            'Dim submittal As String = ""
            'Dim DateSubmittal As String = ""
            Dim currentstatus As String = ""
            Dim statusDate As String = ""
            Dim comment As String = ""

            'Dim SyntheticMinor As String = ""
            'Dim SMFee As String = ""
            'Dim Part70 As String = ""
            Dim Part70Fee As String = ""
            'Dim VOCTons As String = ""
            'Dim PMTons As String = ""
            'Dim SO2Tons As String = ""
            'Dim NOXTons As String = ""
            'Dim CalculatedFee As String = ""
            'Dim FeeRate As String = ""
            'Dim NSPS As String = ""
            'Dim NSPSExempt As String = ""
            'Dim NSPSExemptReason As String = ""
            'Dim AdminFee As String = ""
            'Dim TotalFee As String = ""
            Dim Class1 As String = ""
            Dim Operate As String = ""
            'Dim ShutDown As String = ""
            'Dim OfficialName As String = ""
            'Dim OfficialTitle As String = ""
            'Dim PaymentPlan As String = ""
            'Dim ConfirmNumber As String = ""
            'Dim ConfirmUser As String = ""
            'Dim Comment_Feedata As String = ""

            Dim FirstName As String = ""
            Dim LastName As String = ""
            Dim Prefix As String = ""
            Dim Title As String = ""
            Dim ContactCompanyName As String = ""
            Dim ContactAddress1 As String = ""
            Dim ContactAddress2 As String = ""
            Dim ContactCity As String = ""
            Dim ContactState As String = ""
            Dim ContactZipCode As String = ""
            Dim GECOEmail As String = ""
            Dim OperationalStatus As String = ""
            'Dim Class_Mailout As String = ""
            Dim NSPS_MailOut As String = ""
            Dim Part70_Mailout As String = ""
            Dim ShutDownDate As String = ""
            Dim FacilityName As String = ""
            Dim FacilityAddress1 As String = ""
            Dim FacilityAddress2 As String = ""
            Dim FacilityCity As String = ""
            Dim FacilityZipCode As String = ""
            Dim Comment_MailOut As String = ""

            SQL = "select * " & _
            "from airbranch.FeeMailOut " & _
            "where not exists (select * from AIRbranch.FS_Admin " & _
            "where airbranch.feemailout.strairsnumber = airbranch.fs_admin.strairsnumber  " & _
            "and airbranch.feemailout.intyear = airbranch.fs_admin.numfeeyear) " & _
            "order by intyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("intYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("intyear")
                End If
                Enrolled = "1"
                InitialEnrollment = "01-Jun-" & Mid(FeeYear, 3)
                Enrollment = InitialEnrollment
                InitialMailout = "1"
                MailoutSent = "1"
                DateMailoutSent = "01-Jun-" & Mid(FeeYear, 3)
                currentstatus = "4"
                statusDate = DateMailoutSent
                comment = ""
                If AIRSNumber <> "" And FeeYear <> "" And FeeYear.Length = 4 Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        FacilityName = ""
                    Else
                        FacilityName = dr.Item("strFacilityname")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        Prefix = ""
                    Else
                        Prefix = dr.Item("strCOntactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactFirstNAME")) Then
                        FirstName = ""
                    Else
                        FirstName = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        LastName = ""
                    Else
                        LastName = dr.Item("strcontactLastName")
                    End If
                    If IsDBNull(dr.Item("strCompanyname")) Then
                        ContactCompanyName = ""
                    Else
                        ContactCompanyName = dr.Item("strCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress")) Then
                        ContactAddress1 = ""
                    Else
                        ContactAddress1 = dr.Item("strContactAddress")
                    End If
                    If IsDBNull(dr.Item("strFacilityAddress")) Then
                        FacilityAddress1 = ""
                    Else
                        FacilityAddress1 = dr.Item("strFacilityAddress")
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        ContactCity = ""
                    Else
                        ContactCity = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        ContactState = ""
                    Else
                        ContactState = dr.Item("strContactState")
                    End If
                    If ContactState = "N/A" Or ContactState.Length > 2 Then
                        ContactState = ""
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        ContactZipCode = ""
                    Else
                        ContactZipCode = dr.Item("strContactZipCode")
                    End If
                    If ContactZipCode.Length > 9 Then
                        ContactZipCode = Mid(ContactZipCode, 1, 9)
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        GECOEmail = ""
                    Else
                        GECOEmail = dr.Item("strContactEmail")
                    End If
                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        Operate = ""
                    Else
                        Operate = dr.Item("strOperationalStatus")
                    End If
                    OperationalStatus = Operate
                    If IsDBNull(dr.Item("datShutDownDate")) Then
                        ShutDownDate = ""
                    Else
                        ShutDownDate = Format(dr.Item("datShutDownDate"), "dd-MMM-yyyy")
                    End If
                    'ShutDown = ShutDownDate
                    If IsDBNull(dr.Item("strClass")) Then
                        Class1 = ""
                    Else
                        Class1 = dr.Item("strClass")
                    End If
                    'Class_Mailout = Class1
                    If IsDBNull(dr.Item("strAPCPart70")) Then
                        Part70Fee = ""
                    Else
                        Part70Fee = dr.Item("strAPCPart70")
                    End If
                    If Part70Fee = "YES" Then
                        Part70Fee = "1"
                    Else
                        Part70Fee = "0"
                    End If
                    Part70_Mailout = Part70Fee
                    If IsDBNull(dr.Item("strAPCNSPS")) Then
                        NSPS_MailOut = ""
                    Else
                        NSPS_MailOut = dr.Item("strAPCNSPS")
                    End If
                    If NSPS_MailOut = "YES" Then
                        NSPS_MailOut = "1"
                    Else
                        NSPS_MailOut = "0"
                    End If
                    'NSPS = NSPS_MailOut
                    If IsDBNull(dr.Item("strFacilityStreet")) Then
                        FacilityAddress1 = ""
                    Else
                        FacilityAddress1 = dr.Item("strFacilityStreet")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        FacilityCity = ""
                    Else
                        FacilityCity = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        FacilityZipCode = ""
                    Else
                        FacilityZipCode = dr.Item("strFacilityZipCode")
                    End If
                    If FacilityZipCode.Length > 9 Then
                        FacilityZipCode = Mid(FacilityZipCode, 1, 9)
                    End If

                    SQL = "Insert into AIRBranch.FS_Admin " & _
                    "values " & _
                    "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                    "'" & Enrolled & "', '" & InitialEnrollment & "', " & _
                    "'" & Enrollment & "', '" & InitialMailout & "', " & _
                    "'" & MailoutSent & "', '" & DateMailoutSent & "', " & _
                    "'', '', " & _
                    "'" & currentstatus & "', '" & statusDate & "', " & _
                    "'" & comment & "', " & _
                    "'1', 'Fee Populate', " & _
                    "sysdate, sysdate) "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    SQL = "insert into AIRBranch.FS_FeeData " & _
                    "(numFeeYear, strAIRSNumber, " & _
                    "Active, UpdateUser, " & _
                    "UpdateDateTime, CreateDateTime) " & _
                    "values " & _
                    "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                    "'1', 'Fee Populate', " & _
                    "sysdate, sysdate) "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    SQL = "Insert into airbranch.FS_Mailout " & _
                    "values " & _
                    "('" & FeeYear & "', '" & AIRSNumber & "', " & _
                    "'" & Replace(FirstName, "'", "''") & "', '" & Replace(LastName, "'", "''") & "', " & _
                    "'" & Replace(Prefix, "'", "''") & "', '" & Replace(Title, "'", "''") & "', " & _
                    "'" & Replace(ContactCompanyName, "'", "''") & "', '" & Replace(ContactAddress1, "'", "''") & "', " & _
                    "'" & Replace(ContactAddress2, "'", "''") & "', '" & Replace(ContactCity, "'", "''") & "', " & _
                    "'" & ContactState & "', '" & ContactZipCode & "', " & _
                    "'" & GECOEmail & "', '" & OperationalStatus & "', " & _
                    "'" & Class1 & "', '" & NSPS_MailOut & "', " & _
                    "'" & Part70_Mailout & "', '" & ShutDownDate & "', " & _
                    "'" & Replace(FacilityName, "'", "''") & "', '" & Replace(FacilityAddress1, "'", "''") & "', " & _
                    "'" & Replace(FacilityAddress2, "'", "''") & "', '" & Replace(FacilityCity, "'", "''") & "', " & _
                    "'" & FacilityZipCode & "', " & _
                    "'" & Comment_MailOut & "', " & _
                    "'1', 'Fee Populate', " & _
                    "sysdate, sysdate ) "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While

            dr.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FeeYear As String = ""
            Dim Payment As String = ""
            Dim PayDate As String = ""
            Dim CheckNo As String = ""
            Dim DepositNo As String = ""
            'Dim PayType As String = ""
            Dim BatchNo As String = ""
            Dim EntryPerson As String = ""
            Dim Comments As String = ""
            'Dim PayID As String = ""
            Dim InvoiceNo As String = ""
            Dim InvoiceID As String = ""
            Dim count As Integer

            SQL = "select " & _
            "strairsnumber, intyear, " & _
            "numPayment, datPayDate, " & _
            "strCheckNo, strDepositNo, " & _
            "strPayType, strBatchNo, " & _
            "strEntryPerson, strComments, " & _
            "intFiscalYear, intPayId, " & _
            "strInvoiceNo " & _
            "from AIRBranch.FSAddPaid " & _
            "where not exists (select * from AIRBranch.FS_transactions " & _
            "where AIRBranch.FSAddPaid.Strairsnumber = AIRBranch.FS_Transactions.strAIRSNumber " & _
            "and airbranch.fsAddPaid.intyear = airbranch.FS_Transactions.numFeeyear " & _
            "and strComment <> 'Old Fee System Invoice ID: '||strInvoiceNo) " & _
            "and numPayment <> '0' " & _
            "order by intyear desc "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("intYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("intYear")
                End If
                If IsDBNull(dr.Item("numpayment")) Then
                    Payment = ""
                Else
                    Payment = dr.Item("numPayment")
                End If
                If IsDBNull(dr.Item("datPayDate")) Then
                    PayDate = ""
                Else
                    PayDate = Format(dr.Item("datPayDate"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strCheckNo")) Then
                    CheckNo = ""
                Else
                    CheckNo = dr.Item("strCheckNo")
                End If
                If IsDBNull(dr.Item("strDepositNo")) Then
                    DepositNo = ""
                Else
                    DepositNo = dr.Item("strDepositNo")
                End If
                'PayType = "1"
                If IsDBNull(dr.Item("strBatchNO")) Then
                    BatchNo = ""
                Else
                    BatchNo = dr.Item("strBatchNo")
                End If
                If IsDBNull(dr.Item("strEntryPerson")) Then
                    EntryPerson = ""
                Else
                    EntryPerson = dr.Item("strEntryPerson")
                End If
                If IsDBNull(dr.Item("strComments")) Then
                    Comments = ""
                Else
                    Comments = dr.Item("strComments")
                End If
                If IsDBNull(dr.Item("strInvoiceNo")) Then
                    InvoiceNo = ""
                Else
                    InvoiceNo = dr.Item("strInvoiceNo")
                    Comments = Comments & "Old Fee System Invoice ID: " & dr.Item("strInvoiceNO")
                End If

                If FeeYear <> "" And AIRSNumber <> "" Then
                    SQL = "select * " & _
                    "from airbranch.FS_Admin " & _
                    "where strairsnumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '" & FeeYear & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    RecExist = dr2.Read
                    dr2.Close()

                    If RecExist = True Then
                        SQL = "select * " & _
                        "from airbranch.FS_FeeData " & _
                        "where strairsnumber = '" & AIRSNumber & "' " & _
                        "and numFeeYear = '" & FeeYear & "' "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        RecExist = dr2.Read
                        dr2.Close()

                        If RecExist = True Then
                            SQL = "select * " & _
                            "from airbranch.FS_FeeAuditedData " & _
                            "where strairsnumber = '" & AIRSNumber & "' " & _
                            "and numFeeYear = '" & FeeYear & "' "
                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            RecExist = dr2.Read
                            dr2.Close()

                            If RecExist = True Then
                                If RecExist = True Then
                                    SQL = "select * " & _
                                    "from airbranch.FS_ContactInfo " & _
                                    "where strairsnumber = '" & AIRSNumber & "' " & _
                                    "and numFeeYear = '" & FeeYear & "' "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr2 = cmd.ExecuteReader
                                    RecExist = dr2.Read
                                    dr2.Close()

                                    If RecExist = True Then
                                        SQL = "select InvoiceID " & _
                                        "from airbranch.FS_FeeInvoice " & _
                                        "where strairsnumber = '" & AIRSNumber & "' " & _
                                        "and numFeeYear = '" & FeeYear & "' " & _
                                        "and strComment like '%" & InvoiceNo & "%'"

                                        cmd = New OracleCommand(SQL, Conn)
                                        If Conn.State = ConnectionState.Closed Then
                                            Conn.Open()
                                        End If
                                        dr2 = cmd.ExecuteReader
                                        While dr2.Read
                                            If IsDBNull(dr2.Item("InvoiceID")) Then
                                                InvoiceID = ""
                                            Else
                                                InvoiceID = dr2.Item("InvoiceID")
                                            End If
                                        End While
                                        dr2.Close()

                                        If InvoiceID <> "" Then
                                            SQL = "Insert into AIRBranch.FS_Transactions " & _
                                            "values " & _
                                            "((select max(TransactionID) + 1 from AIRBranch.FS_Transactions), " & _
                                            "'" & InvoiceID & "', '1', " & _
                                            "'" & PayDate & "', '" & Payment & "', " & _
                                            "'" & CheckNo & "', '" & DepositNo & "', " & _
                                            "'" & BatchNo & "', '" & EntryPerson & "', " & _
                                            "'" & Comments & "', '1', " & _
                                            "'" & EntryPerson & "', '" & PayDate & "', " & _
                                            "'" & PayDate & "', '" & AIRSNumber & "', " & _
                                            "'" & FeeYear & "', '') "

                                            cmd = New OracleCommand(SQL, Conn)
                                            If Conn.State = ConnectionState.Closed Then
                                                Conn.Open()
                                            End If
                                            cmd.ExecuteNonQuery()

                                            count += 1


                                            'Dim AIRSNumber As String = ""
                                            'Dim FeeYear As String = ""
                                            'Dim Payment As String = ""
                                            'Dim PayDate As String = ""
                                            'Dim CheckNo As String = ""
                                            'Dim DepositNo As String = ""
                                            'Dim PayType As String = ""
                                            'Dim BatchNo As String = ""
                                            'Dim EntryPerson As String = ""
                                            'Dim Comments As String = ""
                                            'Dim PayID As String = ""
                                            'Dim InvoiceNo As String = ""
                                            'Dim InvoiceID As String = ""


                                        End If

                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End While
            dr.Close()


            MsgBox(count.ToString)

            Exit Sub

            SQL = "select * " & _
            "from airbranch.FS_Transactions " & _
            "where strDepositNo is null " & _
            "and strCheckNo is null and numPayment = '0' " & _
            "and strentryperson is null "  

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSnumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If IsDBNull(dr.Item("numFeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("numFeeYear")
                End If
                If AIRSNumber <> "" And FeeYear <> "" Then
                    SQL = "delete airbranch.fs_transactions " & _
                    "where strairsnumber = '" & AIRSNumber & "' " & _
                    "and numfeeyear = '" & FeeYear & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                End If
            End While
            dr.Close()


            Exit Sub


            SQL = "select AIRbranch.fs_Admin.strAIRSNumber, numfeeyear " & _
            "from AIRbranch.fs_Admin, AIRBranch.FEEMAILOut  " & _
            "where not exists (select * from AIRbranch.FSPayAndSubmit " & _
            "where airbranch.FS_Admin.strAIRSNumber = AIRBranch.FSPayAndSubmit.strAIRSNumber " & _
            "and airbranch.FS_Admin.numfeeyear = AIRbranch.FSPayAndSubmit.intyear) " & _
            "and AIRBranch.FS_Admin.strAIRSNumber = AIRBranch.FeeMailOut.strairsnumber " & _
            "and AIRBranch.FS_Admin.numfeeyear = AIRBranch.Feemailout.intyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSnumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSnumber")
                End If
                If IsDBNull(dr.Item("numFeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("numFeeYear")
                End If
                If AIRSNumber <> "" And FeeYear <> "" Then
                    SQL = "update airbranch.FS_Admin set " & _
                    "strEnrolled = '0' " & _
                    "where strairsnumber = '" & AIRSNumber & "' " & _
                    "and numfeeyear = '" & FeeYear & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()




            Exit Sub



            SQL = "Select * " & _
            "from AIRBranch.FS_Admin " & _
            "where not exists (select * from AIRBranch.FEEmailOut " & _
            "where airbranch.FeeMailout.strAIRSNumber = AIRbranch.Fs_admin.strAIRSNumber " & _
            "and airbranch.FeeMailout.intyear = AIRbranch.FS_Admin.nuMFeeyear ) " & _
            "and numFeeYear = '2009' " & _ 
            "order by numfeeyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If AIRSNumber <> "" Then
                    SQL = "Update AIRbranch.FS_Admin set " & _
                    "strInitialMailout = '0' " & _
                    "where strAIRSnumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '2009' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                End If
            End While
            dr.Close()



            SQL = "Select * " & _
                     "from AIRBranch.FS_Admin " & _
                     "where not exists (select * from AIRBranch.FEEmailOut " & _
                     "where airbranch.FeeMailout.strAIRSNumber = AIRbranch.Fs_admin.strAIRSNumber " & _
                     "and airbranch.FeeMailout.intyear = AIRbranch.FS_Admin.nuMFeeyear ) " & _
                     "and numFeeYear = '2008' " & _
                     "order by numfeeyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If AIRSNumber <> "" Then
                    SQL = "Update AIRbranch.FS_Admin set " & _
                    "strInitialMailout = '0' " & _
                    "where strAIRSnumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '2008' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                End If
            End While
            dr.Close()


            SQL = "Select * " & _
         "from AIRBranch.FS_Admin " & _
         "where not exists (select * from AIRBranch.FEEmailOut " & _
         "where airbranch.FeeMailout.strAIRSNumber = AIRbranch.Fs_admin.strAIRSNumber " & _
         "and airbranch.FeeMailout.intyear = AIRbranch.FS_Admin.nuMFeeyear ) " & _
         "and numFeeYear = '2007' " & _
         "order by numfeeyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If AIRSNumber <> "" Then
                    SQL = "Update AIRbranch.FS_Admin set " & _
                    "strInitialMailout = '0' " & _
                    "where strAIRSnumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '2007' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                End If
            End While
            dr.Close()


            SQL = "Select * " & _
         "from AIRBranch.FS_Admin " & _
         "where not exists (select * from AIRBranch.FEEmailOut " & _
         "where airbranch.FeeMailout.strAIRSNumber = AIRbranch.Fs_admin.strAIRSNumber " & _
         "and airbranch.FeeMailout.intyear = AIRbranch.FS_Admin.nuMFeeyear ) " & _
         "and numFeeYear = '2006' " & _
         "order by numfeeyear "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If AIRSNumber <> "" Then
                    SQL = "Update AIRbranch.FS_Admin set " & _
                    "strInitialMailout = '0' " & _
                    "where strAIRSnumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '2006' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                End If
            End While
            dr.Close()








            Exit Sub
            SQL = "select * " & _
            "from airbranch.fs_ADmin " & _
            "where not exists (select * from AIRBranch.APBMasterAIRS " & _
            "where airbranch.FS_Admin.strAIRSNumber = AIRBranch.APBMasterAIRS.strAIRSNumber ) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If AIRSNumber <> "" Then
                    SQL = "Delete AIRBranch.FS_ContactInfo " & _
                    "where strairsnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    SQL = "Delete AIRBranch.FS_FeeAmendment " & _
                    "where strairsnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    'SQL = "Delete AIRBranch.FS_FeeAudit " & _
                    '"where strairsnumber = '" & AIRSNumber & "' "
                    'cmd = New OracleCommand(SQL, conn)
                    'If conn.State = ConnectionState.Closed Then
                    '    conn.Open()
                    'End If
                    'cmd.ExecuteNonQuery()

                    SQL = "Delete AIRBranch.FS_FeeAuditedData " & _
                    "where strairsnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    SQL = "Delete AIRBranch.FS_FeeData " & _
                    "where strairsnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    SQL = "Delete AIRBranch.FS_Transactions " & _
                                  "where strairsnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    SQL = "Delete AIRBranch.FS_FeeInvoice " & _
                    "where strairsnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    SQL = "Delete AIRBranch.FS_MailOut " & _
                    "where strairsnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()



                    SQL = "Delete AIRBranch.FS_Admin " & _
                    "where strairsnumber = '" & AIRSNumber & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FeeYear As String = ""
            Dim Comments As String = ""

            SQL = "select " & _
            "strAIRSNumber, intYear, " & _
            "strComments " & _
            "from AIRBranch.FSPayAndSubmit " & _
            "where strComments is not null "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("intYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("intYear")
                End If
                If IsDBNull(dr.Item("strComments")) Then
                    Comments = ""
                Else
                    Comments = dr.Item("strComments")
                End If
                If AIRSNumber <> "" And FeeYear <> "" Then
                    SQL = "update airbranch.FS_FeeData set " & _
                    "strComment = '" & Replace(Comments, "'", "''") & "' " & _
                    "where strAIRSNumber = '" & AIRSNumber & "' " & _
                    "and numFeeYear = '" & FeeYear & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FeeYear As String = ""

            SQL = "Select " & _
            "strAIRSNumber,  numFeeYear " & _
            "from AIRBranch.FS_Admin " & _
            "order by numFeeyear desc "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("numFeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("numFeeYear")
                End If
                If AIRSNumber <> "" And FeeYear <> "" Then
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd = New OracleCommand("AIRBranch.PD_FEE_Status", Conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = FeeYear
                    cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = "0413" & AIRSNumber

                    cmd.ExecuteNonQuery()
                End If
            End While
            dr.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Try
            Dim invoiceID As String = ""

            SQL = "select " & _
            "Invoices.invoiceid, InvoiceTotal, paymentTOtal,  " & _
            "(InvoiceTotal - PaymentTotal) as Balance  " & _
            "from  " & _
            "(select   " & _
            "sum(numAmount) as InvoiceTotal  , invoiceid  " & _
            "from airbranch.FS_Feeinvoice   " & _
            "where  Active = '1' group by invoiceid) Invoices,  " & _
            "( select   " & _
            "sum(numPayment) as PaymentTotal, invoiceid    " & _
            "from airbranch.FS_TRANSACTIONS   " & _
            "where  Active = '1' group by Invoiceid ) Transactions  " & _
            "where Invoices.invoiceid = Transactions.invoiceid  " & _
            "and InvoiceTotal - PaymentTotal = 0 "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("INVoiceID")) Then
                    invoiceID = ""
                Else
                    invoiceID = dr.Item("InvoiceID")
                End If
                If invoiceID <> "" Then
                    SQL = "Update AIRBranch.FS_Feeinvoice set " & _
                    "strInvoicesTATUS = '1' " & _
                    "WHERE InvoiceID = '" & invoiceID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "select " & _
         "Invoices.invoiceid, InvoiceTotal, paymentTOtal,  " & _
         "(InvoiceTotal - PaymentTotal) as Balance  " & _
         "from  " & _
         "(select   " & _
         "sum(numAmount) as InvoiceTotal  , invoiceid  " & _
         "from airbranch.FS_Feeinvoice   " & _
         "where  Active = '1' group by invoiceid) Invoices,  " & _
         "( select   " & _
         "sum(numPayment) as PaymentTotal, invoiceid    " & _
         "from airbranch.FS_TRANSACTIONS   " & _
         "where  Active = '1' group by Invoiceid ) Transactions  " & _
         "where Invoices.invoiceid = Transactions.invoiceid  " & _
         "and InvoiceTotal - PaymentTotal <> 0 "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("INVoiceID")) Then
                    invoiceID = ""
                Else
                    invoiceID = dr.Item("InvoiceID")
                End If
                If invoiceID <> "" Then
                    SQL = "Update AIRBranch.FS_Feeinvoice set " & _
                    "strInvoicesTATUS = '0' " & _
                    "WHERE InvoiceID = '" & invoiceID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Try
            Dim FacilityID As String = ""
            Dim InventoryYear As String = ""
            Dim UpdateUser As String = ""
            Dim Finalized As String = ""

            SQL = "select AIRBranch.EIS_Admin.FacilitySiteID ,  AIRBranch.EIS_Admin.InventoryYear , " & _
            "strUSERName, numUserID, " & _
            "updateUSer, strDateLastLogIn, strFinalize   " & _
            "from AIRBranch.EISI, AIRBranch.EIS_Admin " & _
            "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_Admin.FacilitySiteID " & _
            "and AIRBranch.EISI.strInventoryYear = AIRBranch.EIS_Admin.InventoryYear " & _
            "and  AIRBranch.EIS_Admin.InventoryYear = '2008' " & _
            "and strDateLastLogIn not like '%2011'  " & _
            "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    InventoryYear = ""
                Else
                    InventoryYear = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = ""
                Else
                    Finalized = dr.Item("strFinalize")
                End If
                Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_Admin set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' " & _
                    "and InventoryYear = '" & InventoryYear & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

            SQL = "select AIRBranch.EIS_EmissionsUnit.FacilitySiteID ,  AIRBranch.EIS_EmissionsUnit.intUnitStatusCodeYear , " & _
       "strUSERName, numUserID, " & _
       "updateUSer, strDateLastLogIn, strFinalize   " & _
       "from AIRBranch.EISI, AIRBranch.EIS_EmissionsUnit " & _
       "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_EmissionsUnit.FacilitySiteID " & _
       "and AIRBranch.EISI.strInventoryYear = AIRBranch.EIS_EmissionsUnit.intUnitStatusCodeYear " & _
       "and  AIRBranch.EIS_EmissionsUnit.intUnitStatusCodeYear = '2008' " & _
       "and strDateLastLogIn not like '%2011'  " & _
       "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("intUnitStatusCodeYear")) Then
                    InventoryYear = ""
                Else
                    InventoryYear = dr.Item("intUnitStatusCodeYear")
                End If
                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_EmissionsUnit set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' " & _
                    "and intUnitStatusCodeYear = '" & InventoryYear & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

            SQL = "select AIRBranch.EIS_FacilityGEOCoord.FacilitySiteID ,   " & _
     "strUSERName, numUserID, " & _
     "updateUSer, strDateLastLogIn, strFinalize   " & _
     "from AIRBranch.EISI, AIRBranch.EIS_FacilityGEOCoord " & _
     "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_FacilityGEOCoord.FacilitySiteID " & _
     "and AIRBranch.EISI.strInventoryYear = '2008' " & _
      "and strDateLastLogIn not like '%2011'  " & _
     "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If
               
                InventoryYear = "2008"
                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_FacilityGEOCoord set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()



            SQL = "select AIRBranch.EIS_FacilityIdentification.FacilitySiteID ,  " & _
          "strUSERName, numUserID, " & _
          "updateUSer, strDateLastLogIn, strFinalize   " & _
          "from AIRBranch.EISI, AIRBranch.EIS_FacilityIdentification " & _
          "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_FacilityIdentification.FacilitySiteID " & _
          "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
          "and strDateLastLogIn not like '%2011'  " & _
          "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"
                 
                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_FacilityIdentification set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()



            SQL = "select AIRBranch.EIS_FacilitySite.FacilitySiteID ,  " & _
         "strUSERName, numUserID, " & _
         "updateUSer, strDateLastLogIn, strFinalize   " & _
         "from AIRBranch.EISI, AIRBranch.EIS_FacilitySite " & _
         "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_FacilitySite.FacilitySiteID " & _
         "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
         "and strDateLastLogIn not like '%2011'  " & _
         "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_FacilitySite set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()



            SQL = "select AIRBranch.EIS_FacilitySiteAddress.FacilitySiteID ,  " & _
  "strUSERName, numUserID, " & _
  "updateUSer, strDateLastLogIn, strFinalize   " & _
  "from AIRBranch.EISI, AIRBranch.EIS_FacilitySiteAddress " & _
  "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_FacilitySiteAddress.FacilitySiteID " & _
  "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
  "and strDateLastLogIn not like '%2011'  " & _
  "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_FacilitySiteAddress set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()


            SQL = "select distinct AIRBranch.EIS_Process.FacilitySiteID ,  " & _
            "AIRBranch.EIS_Process.intLastEmissionsYear, " & _
          "strUSERName, numUserID, " & _
          "updateUSer, strDateLastLogIn, strFinalize   " & _
          "from AIRBranch.EISI, AIRBranch.EIS_Process " & _
          "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_Process.FacilitySiteID " & _
           "and AIRBranch.EISI.strInventoryYear = AIRBranch.EIS_Process.intLastEmissionsYear " & _
          "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
          "and strDateLastLogIn not like '%2011'  " & _
          "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                If IsDBNull(dr.Item("intLastEmissionsYear")) Then
                    InventoryYear = "2008"
                Else
                    InventoryYear = dr.Item("intLastEmissionsYear")
                End If


                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_Process set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' " & _
                    "and intLastEmissionsYear = '" & InventoryYear & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()


            SQL = "select distinct AIRBranch.EIS_ProcessControlApproach.FacilitySiteID ,  " & _
         "AIRBranch.EIS_ProcessControlApproach.intFirstInventoryYear, " & _
       "strUSERName, numUserID, " & _
       "updateUSer, strDateLastLogIn, strFinalize   " & _
       "from AIRBranch.EISI, AIRBranch.EIS_ProcessControlApproach " & _
       "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_ProcessControlApproach.FacilitySiteID " & _
        "and AIRBranch.EISI.strInventoryYear = AIRBranch.EIS_ProcessControlApproach.intFirstInventoryYear " & _
       "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
       "and strDateLastLogIn not like '%2011'  " & _
       "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                If IsDBNull(dr.Item("intFirstInventoryYear")) Then
                    InventoryYear = "2008"
                Else
                    InventoryYear = dr.Item("intFirstInventoryYear")
                End If


                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_ProcessControlApproach set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' " & _
                    "and intFirstInventoryYear = '" & InventoryYear & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

            SQL = "select AIRBranch.EIS_ProcessControlMeasure.FacilitySiteID ,  " & _
  "strUSERName, numUserID, " & _
  "updateUSer, strDateLastLogIn, strFinalize   " & _
  "from AIRBranch.EISI, AIRBranch.EIS_ProcessControlMeasure " & _
  "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_ProcessControlMeasure.FacilitySiteID " & _
  "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
  "and strDateLastLogIn not like '%2011'  " & _
  "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_ProcessControlMeasure set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()


            SQL = "select distinct AIRBranch.EIS_ProcessControlPollutant.FacilitySiteID ,  " & _
  "strUSERName, numUserID, " & _
  "updateUSer, strDateLastLogIn, strFinalize   " & _
  "from AIRBranch.EISI, AIRBranch.EIS_ProcessControlPollutant " & _
  "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_ProcessControlPollutant.FacilitySiteID " & _
  "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
  "and strDateLastLogIn not like '%2011'  " & _
  "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_ProcessControlPollutant set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()




            SQL = "select distinct AIRBranch.EIS_ReleasePoint.FacilitySiteID ,  " & _
  "strUSERName, numUserID, " & _
  "updateUSer, strDateLastLogIn, strFinalize   " & _
  "from AIRBranch.EISI, AIRBranch.EIS_ReleasePoint " & _
  "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_ReleasePoint.FacilitySiteID " & _
  "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
  "and strDateLastLogIn not like '%2011'  " & _
  "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_ReleasePoint set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()



            SQL = "select distinct AIRBranch.EIS_RPApportionment.FacilitySiteID ,  " & _
  "strUSERName, numUserID, " & _
  "updateUSer, strDateLastLogIn, strFinalize   " & _
  "from AIRBranch.EISI, AIRBranch.EIS_RPApportionment " & _
  "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_RPApportionment.FacilitySiteID " & _
  "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
  "and strDateLastLogIn not like '%2011'  " & _
  "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_RPApportionment set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()



            SQL = "select distinct AIRBranch.EIS_RPGeoCoordinates.FacilitySiteID ,  " & _
            "strUSERName, numUserID, " & _
            "updateUSer, strDateLastLogIn, strFinalize   " & _
            "from AIRBranch.EISI, AIRBranch.EIS_RPGeoCoordinates " & _
            "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_RPGeoCoordinates.FacilitySiteID " & _
            "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
            "and strDateLastLogIn not like '%2011'  " & _
            "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_RPGeoCoordinates set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

            SQL = "select distinct AIRBranch.EIS_TelephoneComm.FacilitySiteID ,  " & _
            "strUSERName, numUserID, " & _
            "updateUSer, strDateLastLogIn, strFinalize   " & _
            "from AIRBranch.EISI, AIRBranch.EIS_TelephoneComm " & _
            "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_TelephoneComm.FacilitySiteID " & _
            "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
            "and strDateLastLogIn not like '%2011'  " & _
            "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_TelephoneComm set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

            SQL = "select distinct AIRBranch.EIS_UnitControlApproach.FacilitySiteID ,  " & _
        "strUSERName, numUserID, " & _
        "updateUSer, strDateLastLogIn, strFinalize   " & _
        "from AIRBranch.EISI, AIRBranch.EIS_UnitControlApproach " & _
        "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_UnitControlApproach.FacilitySiteID " & _
        "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
        "and strDateLastLogIn not like '%2011'  " & _
        "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_UnitControlApproach set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

            SQL = "select distinct AIRBranch.EIS_UnitControlMeasure.FacilitySiteID ,  " & _
    "strUSERName, numUserID, " & _
    "updateUSer, strDateLastLogIn, strFinalize   " & _
    "from AIRBranch.EISI, AIRBranch.EIS_UnitControlMeasure " & _
    "where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_UnitControlMeasure.FacilitySiteID " & _
    "and AIRBranch.EISI.strInventoryYear =   '2008' " & _
    "and strDateLastLogIn not like '%2011'  " & _
    "and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_UnitControlMeasure set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

            SQL = "select distinct AIRBranch.EIS_UnitControlPollutant.FacilitySiteID ,  " & _
"strUSERName, numUserID, " & _
"updateUSer, strDateLastLogIn, strFinalize   " & _
"from AIRBranch.EISI, AIRBranch.EIS_UnitControlPollutant " & _
"where AIRBranch.EISI.strStateFacilityIdentifier = AIRBranch.EIS_UnitControlPollutant.FacilitySiteID " & _
"and AIRBranch.EISI.strInventoryYear =   '2008' " & _
"and strDateLastLogIn not like '%2011'  " & _
"and updateuser <> (numUserID||'-'||strUserName) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                FacilityID = ""
                InventoryYear = ""

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    FacilityID = ""
                Else
                    FacilityID = dr.Item("FacilitySiteID")
                End If

                InventoryYear = "2008"

                If IsDBNull(dr.Item("strFinalize")) Then
                    Finalized = "14-Jun-11"
                Else
                    Finalized = dr.Item("strFinalize")
                    Finalized = Mid(Finalized, 1, InStr(Finalized, " ", CompareMethod.Text) - 1)
                End If

                If IsDBNull(dr.Item("numUserID")) Then
                    UpdateUser = ""
                Else
                    If IsDBNull(dr.Item("strUserName")) Then
                        UpdateUser = ""
                    Else
                        UpdateUser = dr.Item("numUserID") & "-" & dr.Item("strUserName")
                    End If
                End If

                If FacilityID <> "" And InventoryYear <> "" And UpdateUser <> "" And Finalized <> "" Then
                    SQL = "Update AIRBranch.EIS_UnitControlPollutant set " & _
                    "updateUser = '" & Replace(UpdateUser, "'", "''") & "', " & _
                    "UpdateDateTime = '" & Replace(Finalized, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & FacilityID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()

       

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Try
            Dim AIRSNumber As String = ""

            SQL = "Select * " & _
            "from airbranch.APBMasterAIRS " & _
            "where not exists (Select * from airbranch.SSCPInspectionsrequired " & _
            "where airbranch.APBMasterAIRS.strAIRSNumber = airbranch.SSCPInspectionsrequired.strAIRSNumber) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If AIRSNumber <> "" Then
                    SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "(numKey, strAIRSnumber, intyear) " & _
                "values " & _
                "((Select max(numkey) + 1 from " & DBNameSpace & ".SSCPInspectionsRequired), " & _
                "'" & AIRSNumber & "', '" & Now.Year.ToString & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If

                    cmd.ExecuteReader()
                    AIRSNumber = ""
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            Dim AIRSNumber As String = ""
            'Dim coCompany As String = ""
            Dim Firstname As String = ""
            Dim Lastname As String = ""
            Dim Address1 As String = ""
            Dim Address2 As String = ""
            Dim contactCity As String = ""
            Dim contactState As String = ""
            Dim ContactZipCode As String = ""
            Dim contactprefix As String = ""
            Dim email As String = ""

            SQL = "select " & _
            "FacilitySiteID " & _
            "From Airbranch.EIS_Mailout " & _
            "where intinventoryYear = '2011' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("FacilitySiteID")
                End If

                If AIRSNumber <> "" Then
                    SQL = "Select " & _
                    "strContactCompanyname, " & _
                    "strContactFirstname, strContactLastName, " & _
                    "strContactAddress1, strContactAddress2, " & _
                    "strContactCity, strContactState, " & _
                    "strContactZipCode, strContactPrefix, " & _
                    "strContactEmail " & _
                    "from airbranch.apbcontactinformation " & _
                    "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                    "and strKey = '41' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        'coCompany = ""
                        Firstname = ""
                        Lastname = ""
                        Address1 = ""
                        Address2 = ""
                        contactCity = ""
                        contactState = ""
                        ContactZipCode = ""
                        contactprefix = ""
                        email = ""

                        'If IsDBNull(dr2.Item("strContactCompanyname")) Then
                        '    coCompany = "N/A"
                        'Else
                        '    coCompany = dr2.Item("strContactCompanyname")
                        'End If

                        If IsDBNull(dr2.Item("strContactFirstname")) Then
                            Firstname = "N/A"
                        Else
                            Firstname = dr2.Item("strContactFirstname")
                        End If
                        If IsDBNull(dr2.Item("strContactLastName")) Then
                            Lastname = "N/A"
                        Else
                            Lastname = dr2.Item("strContactLastName")
                        End If
                        If IsDBNull(dr2.Item("strContactAddress1")) Then
                            Address1 = "N/A"
                        Else
                            Address1 = dr2.Item("strContactAddress1")
                        End If
                        If IsDBNull(dr2.Item("strContactAddress2")) Then
                            Address2 = ""
                        Else
                            Address2 = dr2.Item("strContactAddress2")
                        End If
                        If IsDBNull(dr2.Item("strContactCity")) Then
                            contactCity = "N/A"
                        Else
                            contactCity = dr2.Item("strContactCity")
                        End If
                        If IsDBNull(dr2.Item("strContactState")) Then
                            contactState = "GA"
                        Else
                            contactState = dr2.Item("strContactState")
                        End If
                        If IsDBNull(dr2.Item("strContactZipCode")) Then
                            ContactZipCode = "12345"
                        Else
                            ContactZipCode = dr2.Item("strContactZipCode")
                        End If
                        If IsDBNull(dr2.Item("strContactPrefix")) Then
                            contactprefix = "N/A"
                        Else
                            contactprefix = dr2.Item("strContactPrefix")
                        End If
                        If IsDBNull(dr2.Item("strContactEmail")) Then
                            email = "no@email.com"
                        Else
                            email = dr2.Item("strContactEmail")
                        End If

                        SQL = "Update airbranch.EIS_Mailout set " & _
                        "strContactCompanyName = '" & Replace(CompanyName, "'", "''") & "', " & _
                        "strContactAddress1 = '" & Replace(Address1, "'", "''") & "', " & _
                        "strContactAddress2 = '" & Replace(Address2, "'", "''") & "', " & _
                        "strContactCity = '" & Replace(contactCity, "'", "''") & "', " & _
                        "strContactState = '" & Replace(contactState, "'", "''") & "', " & _
                        "strContactZipCode= '" & Replace(ContactZipCode, "'", "''") & "', " & _
                        "strContactFirstname = '" & Replace(Firstname, "'", "''") & "', " & _
                        "strContactLastName = '" & Replace(Lastname, "'", "''") & "', " & _
                        "strContactPrefix = '" & Replace(contactprefix, "'", "''") & "', " & _
                        "strContactEmail = '" & Replace(email, "'", "''") & "' " & _
                        "where intinventoryyear = '2011' " & _
                        "and facilitysiteid = '" & AIRSNumber & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If

                        dr3 = cmd.ExecuteReader
                        dr3.Close()

                    End While
                    dr2.Close()

                End If
            End While
            dr.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Try
            Dim AIRSNumber As String = ""
            'Dim coCompany As String = ""
            Dim Firstname As String = ""
            Dim Lastname As String = ""
            Dim Address1 As String = ""
            Dim Address2 As String = ""
            Dim contactCity As String = ""
            Dim contactState As String = ""
            Dim ContactZipCode As String = ""
            Dim contactprefix As String = ""
            Dim email As String = ""

            SQL = "Select  " & _
            " strairsnumber, " & _
            " strContactCompanyname,  " & _
            " strContactFirstname, strContactLastName,  " & _
            " strContactAddress1, strContactAddress2,  " & _
            " strContactCity, strContactState,  " & _
            " strContactZipCode, strContactPrefix,  " & _
            " strContactEmail  " & _
            " from airbranch.apbcontactinformation  " & _
            " where  strKey = '30'  " & _
            " and exists (select * from AIRBranch.EIS_Mailout " & _
            " where '0413'||airbranch.EIS_Mailout.FacilitySiteID = AIRBranch.APBContactInformation.strAIRSNumber " & _
            " and intinventoryyear = '2011' and strContactCompanyName = 'temp'  ) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                'coCompany = ""
                Firstname = ""
                Lastname = ""
                Address1 = ""
                Address2 = ""
                contactCity = ""
                contactState = ""
                ContactZipCode = ""
                contactprefix = ""
                email = ""

                If IsDBNull(dr.Item("strairsnumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = Mid(dr.Item("strairsnumber"), 5)
                End If

                If AIRSNumber <> "" Then

                    'If IsDBNull(dr.Item("strContactCompanyname")) Then
                    '    coCompany = "N/A"
                    'Else
                    '    coCompany = dr.Item("strContactCompanyname")
                    'End If

                    If IsDBNull(dr.Item("strContactFirstname")) Then
                        Firstname = "N/A"
                    Else
                        Firstname = dr.Item("strContactFirstname")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        Lastname = "N/A"
                    Else
                        Lastname = dr.Item("strContactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        Address1 = "N/A"
                    Else
                        Address1 = dr.Item("strContactAddress1")
                    End If
                    If IsDBNull(dr.Item("strContactAddress2")) Then
                        Address2 = ""
                    Else
                        Address2 = dr.Item("strContactAddress2")
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        contactCity = "N/A"
                    Else
                        contactCity = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        contactState = "GA"
                    Else
                        contactState = dr.Item("strContactState")
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        ContactZipCode = "12345"
                    Else
                        ContactZipCode = dr.Item("strContactZipCode")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        contactprefix = "N/A"
                    Else
                        contactprefix = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        email = "no@email.com"
                    Else
                        email = dr.Item("strContactEmail")
                    End If

                    SQL = "Update airbranch.EIS_Mailout set " & _
                     "strContactCompanyName = '" & Replace(CompanyName, "'", "''") & "', " & _
                     "strContactAddress1 = '" & Replace(Address1, "'", "''") & "', " & _
                     "strContactAddress2 = '" & Replace(Address2, "'", "''") & "', " & _
                     "strContactCity = '" & Replace(contactCity, "'", "''") & "', " & _
                     "strContactState = '" & Replace(contactState, "'", "''") & "', " & _
                     "strContactZipCode= '" & Replace(ContactZipCode, "'", "''") & "', " & _
                     "strContactFirstname = '" & Replace(Firstname, "'", "''") & "', " & _
                     "strContactLastName = '" & Replace(Lastname, "'", "''") & "', " & _
                     "strContactPrefix = '" & Replace(contactprefix, "'", "''") & "', " & _
                     "strContactEmail = '" & Replace(email, "'", "''") & "' " & _
                     "where intinventoryyear = '2011' " & _
                     "and facilitysiteid = '" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If

                    dr3 = cmd.ExecuteReader
                    dr3.Close()



                End If
            End While
            dr.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Try
            Dim airsnumber As String = ""
            Dim CompanyName As String = ""
            Dim count As Integer

            'SQL = "Select * " & _
            '"From AIRBranch.EIS_Mailout " & _
            '"where intINventoryYear = '2010' " & _
            '"order by FacilitySiteID "

            SQL = "select * from airbranch.eis_mailout " & _
            "where   intinventoryyear = '2011'  " & _
            "and strContactCompanyName = 'GA Department of Natural Resources' " & _
            "order by facilitysiteid "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("FacilitySIteID")) Then
                    airsnumber = ""
                Else
                    airsnumber = dr.Item("facilitySiteID")
                End If
                SQL = "Select " & _
                "strContactCompanyName " & _
                "from airbranch.APBContactInformation " & _
                "where strAIRSNumber = '0413" & airsnumber & "' " & _
                "and strkey = '30' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("strContactCompanyName")) Then
                        CompanyName = ""
                    Else
                        CompanyName = dr2.Item("strContactCompanyName")
                    End If

                    SQL = "Update airbranch.EIS_Mailout set " & _
                    "strContactCompanyName = '" & Replace(CompanyName, "'", "''") & "' " & _
                    "where FacilitySiteID = '" & airsnumber & "' " & _
                    "and intInventoryYear = '2011' " & _
                    "and strContactCompanyName = 'GA Department of Natural Resources' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If

                    dr3 = cmd.ExecuteReader
                    dr3.Close()

                    count += 1
                End While
                dr2.Close()
            End While
            dr.Close()


            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If

            'dr = cmd.ExecuteReader
            'While dr.Read
            '    If IsDBNull(dr.Item("FacilitySIteID")) Then
            '        airsnumber = ""
            '    Else
            '        airsnumber = dr.Item("facilitySiteID")
            '    End If
            '    If IsDBNull(dr.Item("strContactCompanYname")) Then
            '        CompanyName = ""
            '    Else
            '        CompanyName = dr.Item("strContactCompanyName")
            '    End If

            '    If airsnumber <> "" Then
            '        SQL = "Update airbranch.EIS_Mailout set " & _
            '        "strContactCompanyName = '" & Replace(CompanyName, "'", "''") & "' " & _
            '        "where FacilitySiteID = '" & airsnumber & "' " & _
            '        "and intInventoryYear = '2011' " & _
            '        "and strContactCompanyName = 'GA Department of Natural Resources' "

            '        cmd = New OracleCommand(SQL, conn)
            '        If conn.State = ConnectionState.Closed Then
            '            conn.Open()
            '        End If

            '        dr2 = cmd.ExecuteReader
            '        dr2.Close()

            '        count += 1

            '    End If
            'End While
            'dr.Close()
            MsgBox(count.ToString)
        Catch ex As Exception

        End Try
    End Sub

  
    'Private Sub btnOpenTitleV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenTitleV.Click
    '    Try
    '        If TitleVProject Is Nothing Then
    '            TitleVProject = New DMU_TITLEV_PROJECT
    '            TitleVProject.Show()
    '        Else
    '            TitleVProject.Show()
    '            TitleVProject.BringToFront()
    '        End If
    '        TitleVProject.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
    '    Catch ex As Exception

    '    End Try
    'End Sub

  
    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Try


            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_EIS_QASTART", Conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("AIRSNUMBER_IN", OracleType.VarChar)).Value = txtEISAIRSNumber.Text
            cmd.Parameters.Add(New OracleParameter("INTYEAR_IN", OracleType.Number)).Value = txtEISYear.Text

            cmd.ExecuteNonQuery()


        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")

        End Try
    End Sub
End Class