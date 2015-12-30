Imports System.Collections.Generic
Imports System.Linq
Imports Iaip.Apb.Facilities
Imports Iaip.Apb.Sscp
Imports Oracle.ManagedDataAccess.Client

Public Class IAIPEditAirProgramPollutants

#Region " Properties "

    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property FacilityName As String
    Public Property SomethingChanged As Boolean = False
    Public ReadOnly Property UniquePollutants As HashSet(Of String)
        Get
            Dim up As New HashSet(Of String)
            For Each row As DataGridViewRow In FacilityAirProgramPollutants.Rows
                If row.Cells("Pollutant Code") IsNot Nothing Then
                    up.Add(row.Cells("Pollutant Code").Value)
                End If
            Next
            Return up
        End Get
    End Property

    Private facilityOperatingStatus As FacilityOperationalStatus = FacilityOperationalStatus.O

#End Region

#Region " Form load "

    Private Sub IAIPEditAirProgramPollutants_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)

        If AirsNumber Is Nothing OrElse Not DAL.AirsNumberExists(AirsNumber) Then
            MessageBox.Show("Invalid AIRS number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        DisplayFacility()

        LoadFacilityAirPrograms()
        LoadPollutants()
        LoadComplianceStatuses()
        LoadOperatingStatuses()

        LoadFacilityProgramPollutants()

        SetPermissions()
    End Sub

#End Region

#Region " Selectors "

    Private Sub LoadFacilityAirPrograms()
        With AirProgramSelect
            .DataSource = DAL.GetFacilityAirProgramsAsDataTable(AirsNumber, False)
            .ValueMember = "Key"
            .DisplayMember = "Description"
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub LoadPollutants()
        With PollutantSelect
            .DataSource = SharedData.GetTable(SharedData.Tables.Pollutants)
            .ValueMember = "Pollutant Code"
            .DisplayMember = "Pollutant"
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub LoadComplianceStatuses()
        With ComplianceStatusSelect
            .DataSource = EnumToDataTable(GetType(ComplianceStatus))
            .ValueMember = "Key"
            .DisplayMember = "Description"
            .SelectedValue = ComplianceStatus.InCompliance
        End With
    End Sub

    Private Sub LoadOperatingStatuses()
        With OperatingStatusSelect
            .DataSource = EnumToDataTable(GetType(FacilityOperationalStatus))
            .ValueMember = "Key"
            .DisplayMember = "Description"
            .SelectedValue = facilityOperatingStatus
        End With
    End Sub

#End Region

#Region " Permissions "

    Private Sub SetPermissions()
        If Not UserPermissions.CheckAuth(UserCan.AddPollutantsToFacility) Then
            ControlPanel.Enabled = False
        Else
            If Not UserPermissions.CheckAuth(UserCan.ChangeComplianceStatus) Then
                ComplianceStatusSelect.Enabled = False
            End If
            If Not UserPermissions.CheckAuth(UserCan.EditHeaderData) Then
                OperatingStatusSelect.Enabled = False
            End If
        End If
    End Sub

#End Region

#Region " Facility data "

    Private Sub DisplayFacility()
        AirsNumberDisplay.Text = AirsNumber.FormattedString
        If FacilityName = "" Then FacilityName = DAL.GetFacilityName(AirsNumber)
        FacilityDisplay.Text = FacilityName
        facilityOperatingStatus = DAL.GetFacilityOperationalStatus(AirsNumber)
        FacilityOperatingStatusDisplay.Text = "Facility status: " & facilityOperatingStatus.GetDescription
    End Sub

    Private Sub LoadFacilityProgramPollutants()
        RemoveHandler FacilityAirProgramPollutants.SelectionChanged, AddressOf FacilityAirProgramPollutants_SelectionChanged

        Dim query As String = "SELECT SUBSTR(app.STRAIRPOLLUTANTKEY, 13, 1) AS " &
            "  ""Air Program Code"", lkpl.STRPOLLUTANTCODE AS " &
            "  ""Pollutant Code"", lkpl.STRPOLLUTANTDESCRIPTION AS " &
            "  ""Pollutant"", 'Status_' || lkcs.STRCOMPLIANCECODE AS " &
            "  ""Legacy Compliance Code"", app.STROPERATIONALSTATUS AS " &
            "  ""Operating Status Code"", app.DATMODIFINGDATE AS " &
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
            "ORDER BY ""Air Program Code"", ""Pollutant Code"""

        Dim parameter As New OracleParameter("airsNumber", AirsNumber.DbFormattedString)

        Dim dt As DataTable = DB.GetDataTable(query, parameter)

        dt.Columns.Add("Air Program", GetType(String))
        dt.Columns.Add("Air Program Enum", GetType(AirProgram))
        dt.Columns.Add("Compliance Status", GetType(String))
        dt.Columns.Add("Compliance Status Enum", GetType(ComplianceStatus))
        dt.Columns.Add("Operating Status", GetType(String))
        dt.Columns.Add("Operating Status Enum", GetType(FacilityOperationalStatus))

        Dim ap As AirProgram
        Dim lcs As LegacyComplianceStatus
        Dim os As FacilityOperationalStatus

        For Each row As DataRow In dt.Rows
            ap = FacilityHeaderData.ConvertAirProgramLegacyCodes(row("Air Program Code").ToString)
            row("Air Program Enum") = ap
            row("Air Program") = ap.GetDescription

            lcs = [Enum].Parse(GetType(LegacyComplianceStatus), row("Legacy Compliance Code").ToString)
            row("Compliance Status Enum") = EnforcementCase.ConvertLegacyComplianceStatus(lcs)
            row("Compliance Status") = EnforcementCase.ConvertLegacyComplianceStatus(lcs).GetDescription

            os = [Enum].Parse(GetType(FacilityOperationalStatus), row("Operating Status Code").ToString)
            row("Operating Status Enum") = os
            row("Operating Status") = os.GetDescription
        Next

        With FacilityAirProgramPollutants
            .DataSource = dt
            .Columns("Air Program Code").Visible = False
            .Columns("Air Program Enum").Visible = False
            .Columns("Pollutant Code").Visible = False
            .Columns("Legacy Compliance Code").Visible = False
            .Columns("Compliance Status Enum").Visible = False
            .Columns("Operating Status Code").Visible = False
            .Columns("Operating Status Enum").Visible = False

            .Columns("Air Program").DisplayIndex = 0
            .Columns("Pollutant").DisplayIndex = 1
            .Columns("Compliance Status").DisplayIndex = 2
            .Columns("Operating Status").DisplayIndex = 3
            .Columns("Date Modified").DisplayIndex = 4
            .Columns("Modified By").DisplayIndex = 5

            .SanelyResizeColumns
        End With

        AddHandler FacilityAirProgramPollutants.SelectionChanged, AddressOf FacilityAirProgramPollutants_SelectionChanged
    End Sub

#End Region

#Region " Save data "

    Private Sub SaveValue()
        Console.WriteLine(AirProgramSelect.SelectedValue)
        Console.WriteLine(PollutantSelect.SelectedValue)
        Console.WriteLine(ComplianceStatusSelect.SelectedValue)
        Console.WriteLine(OperatingStatusSelect.SelectedValue)

        If VerifyUpdate() Then

            Dim result As Boolean =
            DAL.PollutantsPrograms.SaveFacilityAirProgramPollutant(AirsNumber,
                                                                   AirProgramSelect.SelectedValue,
                                                                   PollutantSelect.SelectedValue,
                                                                   ComplianceStatusSelect.SelectedValue,
                                                                   OperatingStatusSelect.SelectedValue)

            If result Then
                SomethingChanged = True
                LoadFacilityProgramPollutants()
                MessageBox.Show("Successfully saved data.", "Saved", MessageBoxButtons.OK)
            Else
                MessageBox.Show("There was an error saving the data.", "Error", MessageBoxButtons.OK)
            End If

        End If
    End Sub

    Private Function VerifyUpdate() As Boolean
        If (OperatingStatusSelect.SelectedValue <> facilityOperatingStatus) Then
            Dim dr As DialogResult = MessageBox.Show("The selected pollutant operating status is not the same as the Facility operating status. Do you really want to save?",
                                                     "Confirm", MessageBoxButtons.OKCancel)
            If dr = DialogResult.No Then Return False
        End If
        Return True
    End Function

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        SaveValue()
    End Sub

#End Region

#Region " Selection events "

    Private Sub FacilityAirProgramPollutants_SelectionChanged(sender As Object, e As EventArgs) Handles FacilityAirProgramPollutants.SelectionChanged
        If FacilityAirProgramPollutants.CurrentRow IsNot Nothing Then
            Dim row As DataGridViewRow = FacilityAirProgramPollutants.CurrentRow

            AirProgramSelect.SelectedValue = row.Cells("Air Program Enum").Value
            PollutantSelect.SelectedValue = row.Cells("Pollutant Code").Value
            ComplianceStatusSelect.SelectedValue = row.Cells("Compliance Status Enum").Value
            OperatingStatusSelect.SelectedValue = row.Cells("Operating Status Enum").Value
        End If
    End Sub

    Private Sub SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles AirProgramSelect.SelectedIndexChanged, PollutantSelect.SelectedIndexChanged, ComplianceStatusSelect.SelectedIndexChanged, OperatingStatusSelect.SelectedIndexChanged
        If SelectionExists() Then
            If UserPermissions.CheckAuth(UserCan.ChangeComplianceStatus) OrElse
               UserPermissions.CheckAuth(UserCan.EditHeaderData) Then
                SaveButton.Text = "Update pollutant statuses"
                SaveButton.Visible = True
            Else
                SaveButton.Visible = False
            End If
        Else
            SaveButton.Text = "Add new air program/pollutant"
            SaveButton.Visible = True
        End If
    End Sub

    Private Function SelectionExists() As Boolean
        For Each row As DataGridViewRow In FacilityAirProgramPollutants.Rows
            If (AirProgramSelect.SelectedValue = row.Cells("Air Program Enum").Value) AndAlso
                (PollutantSelect.SelectedValue = row.Cells("Pollutant Code").Value) Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

End Class