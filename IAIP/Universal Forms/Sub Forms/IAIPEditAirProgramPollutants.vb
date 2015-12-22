Imports System.Collections.Generic
Imports Iaip.Apb.Facilities
Imports Iaip.Apb.Sscp
Imports Oracle.ManagedDataAccess.Client

Public Class IAIPEditAirProgramPollutants

    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property FacilityName As String
    Private Property AirPrograms() As AirProgram

#Region " Form load "

    Private Sub IAIPEditAirProgramPollutants_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)

        LoadPollutants()
        LoadComplianceStatuses()
        SetPermissions()
    End Sub

    Private Sub IAIPEditAirProgramPollutants_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If AirsNumber Is Nothing OrElse Not DAL.AirsNumberExists(AirsNumber) Then
            MessageBox.Show("Invalid AIRS number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        DisplayFacility()
        LoadFacilityData()
    End Sub

    Private Sub SetPermissions()
        Throw New NotImplementedException()
        'If AccountFormAccess(27, 3) = "1" Or AccountFormAccess(27, 2) = "1" Or (UserBranch = "1" And UserUnit = "---") Then
        '    ComplianceStatusSelect.Enabled = True
        '    ComplianceStatusSelect.SelectedValue = 4
        'Else
        '    ComplianceStatusSelect.Enabled = False
        '    ComplianceStatusSelect.SelectedValue = 4
        'End If

        'If UserProgram = "7" Or UserProgram = "9" Or UserProgram = "10" _
        '        Or UserProgram = "11" Or UserProgram = "12" Or UserProgram = "13" _
        '            Or UserProgram = "14" Or UserProgram = "15" Then
        '    SaveButton.Enabled = False
        'End If
    End Sub

#End Region

#Region " Common data "

    Private Sub LoadPollutants()
        With PollutantSelect
            .DataSource = SharedData.GetTable(SharedData.Tables.Pollutants)
            .DisplayMember = "Pollutant"
            .ValueMember = "Pollutant Code"
            .SelectedValue = 0
        End With
    End Sub

    Private Sub LoadComplianceStatuses()
        With ComplianceStatusSelect
            .DataSource = EnumToDataTable(GetType(ComplianceStatus))
            .DisplayMember = "Description"
            .ValueMember = "ID"
            .SelectedValue = 0
        End With
    End Sub

#End Region

#Region " Facility-specific data "

    Private Sub DisplayFacility()
        AirsNumberDisplay.Text = AirsNumber.FormattedString
        If FacilityName = "" Then FacilityName = DAL.GetFacilityName(AirsNumber)
        FacilityDisplay.Text = FacilityName
    End Sub

    Private Sub LoadFacilityData()
        LoadFacilityAirPrograms()
        LoadFacilityPollutants()
    End Sub

    Private Sub LoadFacilityAirPrograms()
        AirPrograms = DAL.FacilityHeaderDataData.GetFacilityAirPrograms(AirsNumber)
        AirProgramSelect.DataSource = AirPrograms.GetUniqueFlags
    End Sub

    Private Sub LoadFacilityPollutants()
        Dim query As String = "SELECT SUBSTR(app.STRAIRPOLLUTANTKEY, 13, 1) AS " &
            "  ""Air Program Code"", lkpl.STRPOLLUTANTCODE AS " &
            "  ""Pollutant Code"", lkpl.STRPOLLUTANTDESCRIPTION AS " &
            "  ""Pollutant"", lkcs.STRCOMPLIANCECODE AS " &
            "  ""Legacy Compliance Code"", app.DATMODIFINGDATE AS " &
            "  ""Date Modified"",(up.STRLASTNAME || ', ' || up.STRFIRSTNAME) " &
            "  AS ""Modified By"" " &
            "FROM AIRBRANCH.APBAirProgramPollutants app " &
            "INNER JOIN AIRBRANCH.LookUPPollutants lkpl ON " &
            "  app.STRPOLLUTANTKEY = lkpl.STRPOLLUTANTCODE " &
            "INNER JOIN AIRBRANCH.LookUpComplianceStatus lkcs ON " &
            "  lkcs.STRCOMPLIANCECODE = app.STRCOMPLIANCESTATUS " &
            "INNER JOIN AIRBRANCH.EPDUserProfiles up ON " &
            "  app.STRMODIFINGPERSON = up.NUMUSERID " &
            "WHERE app.STRAIRSNUMBER = :airsNumber " &
            "ORDER BY ""Air Program Code"", lkpl.STRPOLLUTANTCODE"

        Dim parameter As New OracleParameter("airsNumber", AirsNumber.DbFormattedString)

        Dim dt As DataTable = DB.GetDataTable(query, parameter)

        dt.Columns.Add("Air Program", GetType(String))
        dt.Columns.Add("Compliance Status", GetType(String))
        dt.Columns.Add("Compliance Status Code", GetType(String))

        Dim ap As AirProgram
        Dim lcs As LegacyComplianceStatus

        For Each row As DataRow In dt.Rows
            ap = [Enum].Parse(GetType(AirProgram), row("Air Program Code").ToString)
            row("Air Program") = ap.GetDescription
            lcs = [Enum].Parse(GetType(LegacyComplianceStatus), row("Legacy Compliance Code").ToString)
            row("Compliance Status Code") = EnforcementCase.ConvertLegacyComplianceStatus(lcs).ToString
            row("Compliance Status") = EnforcementCase.ConvertLegacyComplianceStatus(lcs).GetDescription
        Next

        With FacilityAirProgramPollutants
            .DataSource = dt
            .Columns("Air Program Code").Visible = False
            .Columns("Pollutant Code").Visible = False
            .Columns("Legacy Compliance Code").Visible = False
            .Columns("Compliance Status Code").Visible = False

            .Columns("Air Program").DisplayIndex = 0
            .Columns("Pollutant").DisplayIndex = 1
            .Columns("Compliance Status").DisplayIndex = 2
            .Columns("Date Modified").DisplayIndex = 3
            .Columns("Modified By").DisplayIndex = 4

            .SanelyResizeColumns
        End With
    End Sub

#End Region

#Region " Save data "

    Private Sub SaveValue()
        'If UserProgram <> "4" And Mid(UserAccounts, 27, 2) = "0" And Mid(UserAccounts, 27, 3) = "0" _
        '   And Mid(UserAccounts, 27, 4) = "0" Then
        '    MsgBox("You do not have sufficient privileges to save pollutant data.", MsgBoxStyle.Information, "Air Program Pollutants")
        'Else
        '    Dim AIRSPollutantKey As String
        '    If AirProgramSelect.Text <> "" And
        '                  PollutantSelect.SelectedValue <> Nothing And PollutantSelect.Text <> "" And
        '                       ComplianceStatusSelect.SelectedValue <> Nothing And ComplianceStatusSelect.Text <> "" Then
        '        Select Case AirProgramSelect.Text
        '            Case "0 - SIP"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "0"
        '            Case "1 - Fed. SIP"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "1"
        '            Case "3 - Non Fed."
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "3"
        '            Case "4 - CFC Tracking"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "4"
        '            Case "6 - PSD"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "6"
        '            Case "7 - NSR"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "7"
        '            Case "8 - NESHAP"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "8"
        '            Case "9 - NSPS"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "9"
        '            Case "F - FESOP"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "F"
        '            Case "A - Acid Precip."
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "A"
        '            Case "I - Native American"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "I"
        '            Case "M - MACT"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "M"
        '            Case "V - Title V"
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "V"
        '            Case Else
        '                AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "0"
        '        End Select

        '        Sql = "Select strairsnumber " &
        '            "from AIRBRANCH.APBAirProgramPollutants " &
        '            "where strAIRPollutantKey = '" & AIRSPollutantKey & "' " &
        '            "and strPollutantKey = '" & PollutantSelect.SelectedValue & "' "

        '        cmd = New OracleCommand(Sql, CurrentConnection)
        '        If CurrentConnection.State = ConnectionState.Closed Then
        '            CurrentConnection.Open()
        '        End If
        '        dr = cmd.ExecuteReader
        '        recExist = dr.Read

        '        dr.Close()

        '        If recExist = True Then
        '            Dim AirProgramCodes As String
        '            Dim ProgramStatus As String

        '            Sql = "select strAirProgramCodes " &
        '                "from AIRBRANCH.APBHeaderData  " &
        '                "where strAIRSnumber = '0413" & AirsNumberDisplay.Text & "' "
        '            cmd = New OracleCommand(Sql, CurrentConnection)
        '            If CurrentConnection.State = ConnectionState.Closed Then
        '                CurrentConnection.Open()
        '            End If
        '            dr = cmd.ExecuteReader
        '            recExist = dr.Read
        '            If recExist = True Then
        '                AirProgramCodes = dr.Item("strAirProgramCodes")
        '            Else
        '                AirProgramCodes = "000000000000000 "
        '            End If
        '            dr.Close()

        '            Select Case Me.AirProgramSelect.Text
        '                Case "0 - SIP"
        '                    If Mid(AirProgramCodes, 1, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "1 - Fed. SIP"
        '                    If Mid(AirProgramCodes, 2, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "3 - Non Fed."
        '                    If Mid(AirProgramCodes, 3, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "4 - CFC Tracking"
        '                    If Mid(AirProgramCodes, 4, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "6 - PSD"
        '                    If Mid(AirProgramCodes, 5, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "7 - NSR"
        '                    If Mid(AirProgramCodes, 6, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "8 - NESHAP"
        '                    If Mid(AirProgramCodes, 7, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "9 - NSPS"
        '                    If Mid(AirProgramCodes, 8, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "F - FESOP"
        '                    If Mid(AirProgramCodes, 9, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "A - Acid Precip."
        '                    If Mid(AirProgramCodes, 10, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "I - Native American"
        '                    If Mid(AirProgramCodes, 11, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "M - MACT"
        '                    If Mid(AirProgramCodes, 12, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case "V - Title V"
        '                    If Mid(AirProgramCodes, 13, 1) = "1" Then
        '                        ProgramStatus = "True"
        '                    Else
        '                        ProgramStatus = "False"
        '                    End If
        '                Case Else
        '                    ProgramStatus = "False"
        '            End Select
        '            If ProgramStatus = "True" Then
        '                Sql = "Update AIRBRANCH.APBAirProgramPollutants set " &
        '                    "strComplianceStatus = '" & ComplianceStatusSelect.SelectedValue & "', " &
        '                    "strModifingperson = '" & UserGCode & "', " &
        '                    "datModifingdate = '" & OracleDate & "' " &
        '                    "where strAIRPollutantKey = '" & AIRSPollutantKey & "' " &
        '                    "and strAIRSnumber = '0413" & AirsNumberDisplay.Text & "' " &
        '                    "and strPOllutantKey = '" & PollutantSelect.SelectedValue & "' "
        '            Else
        '                Sql = "Update AIRBRANCH.APBAirProgramPollutants set " &
        '                    "strComplianceStatus = '9', " &
        '                    "strModifingperson = '" & UserGCode & "', " &
        '                    "datModifingdate = '" & OracleDate & "', " &
        '                    "strOperationalStatus = 'X' " &
        '                    "where strAIRPollutantKey = '" & AIRSPollutantKey & "' " &
        '                    "and strAIRSnumber = '0413" & AirsNumberDisplay.Text & "' " &
        '                    "and strPOllutantKey = '" & PollutantSelect.SelectedValue & "' "
        '            End If
        '        Else
        '            Sql = "Insert into AIRBRANCH.APBAirProgramPollutants " &
        '                "(strAIRSnumber, strAIRPollutantKey, " &
        '                "strPOllutantKey, strComplianceStatus, " &
        '                "strModifingperson, datModifingdate, " &
        '                "strOperationalStatus) " &
        '                "values " &
        '                "('0413" & AirsNumberDisplay.Text & "', '" & AIRSPollutantKey & "', " &
        '                "'" & PollutantSelect.SelectedValue & "', '" & ComplianceStatusSelect.SelectedValue & "', " &
        '                "'" & UserGCode & "', '" & OracleDate & "', 'O') "
        '        End If

        '        cmd = New OracleCommand(Sql, CurrentConnection)
        '        If CurrentConnection.State = ConnectionState.Closed Then
        '            CurrentConnection.Open()
        '        End If
        '        Try

        '            dr = cmd.ExecuteReader
        '        Catch ex As Exception
        '            MsgBox(ex.ToString())
        '        End Try

        '        MsgBox("Pollutant added to Air Program Code.", MsgBoxStyle.Information, "Edit Air Program Code Pollutants")

        '        If MultiForm IsNot Nothing AndAlso MultiForm.ContainsKey(SscpEnforcement.Name) Then
        '            For Each kvp As KeyValuePair(Of Integer, BaseForm) In MultiForm(SscpEnforcement.Name)
        '                Dim enf As SscpEnforcement = kvp.Value
        '                'enf.LoadEnforcementPollutants2()
        '            Next
        '        End If

        '    Else
        '        MsgBox("No Data Saved", MsgBoxStyle.Exclamation, "Edit Air Program Code Pollutants")
        '    End If

        'End If

    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        SaveValue()
    End Sub

#End Region

    Private Sub FacilityAirProgramPollutants_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles FacilityAirProgramPollutants.CellClick
        Throw New NotImplementedException()
        If e.RowIndex <> -1 AndAlso e.RowIndex < FacilityAirProgramPollutants.RowCount Then
            AirProgramSelect.SelectedValue = FacilityAirProgramPollutants.Rows(e.RowIndex).Cells("Air Program Code")
            PollutantSelect.SelectedValue = FacilityAirProgramPollutants.Rows(e.RowIndex).Cells("Pollutant Code")
            ComplianceStatusSelect.SelectedValue = FacilityAirProgramPollutants.Rows(e.RowIndex).Cells("Compliance Status Code")
        End If
    End Sub

End Class