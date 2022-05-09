Imports System.Collections.Generic
Imports Iaip.Apb.Facilities

Public Class IAIPEditAirProgramPollutants

#Region " Properties "

    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property FacilityName As String
    Public Property SomethingChanged As Boolean
    Public Property FacilityPollutantsSet As HashSet(Of String)

    Private facilityOperatingStatus As FacilityOperationalStatus = FacilityOperationalStatus.O

#End Region

#Region " Form load "

    Private Sub IAIPEditAirProgramPollutants_Load(sender As Object, e As EventArgs) Handles Me.Load
        If AirsNumber Is Nothing OrElse Not DAL.AirsNumberExists(AirsNumber) Then
            MessageBox.Show("Invalid AIRS number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        DisplayFacility()

        LoadFacilityAirPrograms()
        LoadPollutants()
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
            .DataSource = GetSharedData(SharedTable.Pollutants)
            .ValueMember = "Pollutant Code"
            .DisplayMember = "Pollutant"
            .SelectedIndex = -1
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
        If Not CurrentUser.HasPermission(UserCan.AddPollutantsToFacility) Then
            ControlPanel.Enabled = False
        ElseIf Not CurrentUser.HasPermission(UserCan.EditFacilityHeaderData) Then
            OperatingStatusSelect.Enabled = False
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
        Dim dt As DataTable = DAL.PollutantsPrograms.GetFacilityProgramPollutantStatuses(AirsNumber)
        dt.Columns.Add("Air Program", GetType(String))
        dt.Columns.Add("Air Program Enum", GetType(AirPrograms))
        dt.Columns.Add("Operating Status", GetType(String))
        dt.Columns.Add("Operating Status Enum", GetType(FacilityOperationalStatus))

        Dim ap As AirPrograms
        Dim os As FacilityOperationalStatus

        FacilityPollutantsSet = New HashSet(Of String)

        For Each row As DataRow In dt.Rows
            ap = FacilityHeaderData.ConvertAirProgramLegacyCodes(row("Air Program Code").ToString)
            row("Air Program Enum") = ap
            row("Air Program") = ap.GetDescription

            os = [Enum].Parse(GetType(FacilityOperationalStatus), row("Operating Status Code").ToString)
            row("Operating Status Enum") = os
            row("Operating Status") = os.GetDescription

            FacilityPollutantsSet.Add(row("Pollutant Code").ToString)
        Next

        With FacilityAirProgramPollutants
            .DataSource = dt
            .Columns("Air Program Code").Visible = False
            .Columns("Air Program Enum").Visible = False
            .Columns("Pollutant Code").Visible = False
            .Columns("Operating Status Code").Visible = False
            .Columns("Operating Status Enum").Visible = False

            .Columns("Air Program").DisplayIndex = 0
            .Columns("Pollutant").DisplayIndex = 1
            .Columns("Operating Status").DisplayIndex = 3
            .Columns("Date Modified").DisplayIndex = 4
            .Columns("Modified By").DisplayIndex = 5

            .SanelyResizeColumns
        End With
    End Sub

#End Region

#Region " Save data "

    Private Sub SaveValue()
        If VerifyUpdate() Then
            Dim result As Boolean =
            DAL.SaveFacilityAirProgramPollutant(AirsNumber,
                                                                   AirProgramSelect.SelectedValue,
                                                                   PollutantSelect.SelectedValue,
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
            Dim dr As DialogResult = MessageBox.Show("The selected pollutant operating status is not the same as the facility operating status. Do you really want to save?",
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
            OperatingStatusSelect.SelectedValue = row.Cells("Operating Status Enum").Value
        End If
    End Sub

    Private Sub SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles AirProgramSelect.SelectedIndexChanged, PollutantSelect.SelectedIndexChanged, OperatingStatusSelect.SelectedIndexChanged
        If SelectionExists() Then
            If CurrentUser.HasPermission(UserCan.EditFacilityHeaderData) Then
                SaveButton.Text = "Update pollutant status"
                SaveButton.Visible = True
                SaveButton.Location = New Point(270, SaveButton.Location.Y)
            Else
                SaveButton.Visible = False
            End If
        Else
            SaveButton.Text = "Add new air program/pollutant"
            SaveButton.Visible = True
            SaveButton.Location = New Point(15, SaveButton.Location.Y)
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