Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Iaip.Apb
Imports Iaip.Apb.Facilities.FacilityEnums
Imports Iaip.DAL

Public Class IAIPNewFacilityColocation

    Public Property AirsNumber As ApbFacilityId
    Public Property SomethingWasSaved As Boolean
    Private Property SelectedFacilitiesTable As DataTable

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If AirsNumber Is Nothing Then Return

        CheckForCurrentColocatedFacilities()

        AirsNumberDisplay.Text = AirsNumber.FormattedString
        FacilityNameDisplay.Text = DAL.GetFacilityName(AirsNumber)
    End Sub

    Private Sub CheckForCurrentColocatedFacilities()
        Dim AirsParam As New SqlParameter("@airsNumber", AirsNumber.DbFormattedString)
        Dim dt As DataTable = DB.SPGetDataTable("iaip_facility.GetColocatedFacilities", AirsParam)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            SomethingWasSaved = True
            MessageBox.Show("Facility is already in a co-location group.", "Closing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Close()
        End If
    End Sub

    Private Sub btnAddFacility_Click(sender As Object, e As EventArgs) Handles btnAddFacility.Click
        Dim newAirsNumber As ApbFacilityId

        Using lookup As New IAIPFacilityLookUpTool
            If lookup.ShowDialog() <> DialogResult.OK OrElse
                    lookup.SelectedAirsNumber Is Nothing OrElse
                    lookup.SelectedAirsNumber = AirsNumber Then
                Return
            End If

            newAirsNumber = lookup.SelectedAirsNumber
        End Using

        Dim dr As DataRow = DB.SPGetDataRow("iaip_facility.GetBasicFacilityView", New SqlParameter("@airsNumber", newAirsNumber.DbFormattedString))
        If dr Is Nothing Then Return

        If SelectedFacilitiesTable Is Nothing Then
            SelectedFacilitiesTable = {dr}.CopyToDataTable()
            SelectedFacilitiesTable.PrimaryKey = New DataColumn() {SelectedFacilitiesTable.Columns(0)}
            SelectedFacilitiesGrid.DataSource = SelectedFacilitiesTable
        Else
            SelectedFacilitiesTable.ImportRow(dr)
        End If

        btnRemoveFacility.Enabled = True
        btnSave.Enabled = True
    End Sub

    Private Sub btnRemoveFacility_Click(sender As Object, e As EventArgs) Handles btnRemoveFacility.Click
        If SelectedFacilitiesTable Is Nothing OrElse SelectedFacilitiesTable.Rows.Count = 0 Then
            btnRemoveFacility.Enabled = False
            btnSave.Enabled = False
            Return
        End If

        For Each row As DataGridViewRow In SelectedFacilitiesGrid.SelectedRows
            Dim dr As DataRow = SelectedFacilitiesTable.Rows.Find(row.Cells(0).Value)
            If dr IsNot Nothing Then
                SelectedFacilitiesTable.Rows.Remove(dr)
            Else
                MessageBox.Show("An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Return
            End If
        Next

        If SelectedFacilitiesTable.Rows.Count = 0 Then
            btnRemoveFacility.Enabled = False
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If SelectedFacilitiesTable Is Nothing OrElse SelectedFacilitiesTable.Rows.Count = 0 Then
            btnRemoveFacility.Enabled = False
            btnSave.Enabled = False
            MessageBox.Show("No facilities are selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim airsList As New List(Of String) From {AirsNumber.DbFormattedString}

        For Each row As DataRow In SelectedFacilitiesTable.Rows
            airsList.Add(row(0))
        Next

        Dim tableParam As SqlParameter = airsList.AsTvpSqlParameter("@facilityList")
        Dim userParam As New SqlParameter("@modifiedBy", CurrentUser.UserID)
        Dim locationParam As New SqlParameter("@fromLocation", Convert.ToInt32(HeaderDataModificationLocation.FacilityColocationEditor))

        If DB.SPRunCommand("iaip_facility.CreateFacilityColocationGroup", {tableParam, userParam, locationParam}) Then
            MessageBox.Show("The co-location group was created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("An unknown error occurred while trying to create the co-location group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        SomethingWasSaved = True
        Close()
    End Sub

    Private Sub SelectedFacilitiesGrid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles SelectedFacilitiesGrid.CellFormatting
        If e IsNot Nothing AndAlso e.ColumnIndex = 0 AndAlso
            e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
            e.Value = New ApbFacilityId(e.Value.ToString).FormattedString
        End If
    End Sub

End Class