Imports System.Data.SqlClient
Imports Iaip.Apb
Imports Iaip.Apb.Facilities.FacilityEnums

Public Class IAIPEditFacilityColocation

    Public Property AirsNumber As ApbFacilityId
    Public Property SomethingWasSaved As Boolean

    Private ReadOnly Property AirsParam As SqlParameter
        Get
            Return New SqlParameter("@airsNumber", AirsNumber.DbFormattedString)
        End Get
    End Property
    Private Shared ReadOnly Property UserParam As SqlParameter
        Get
            Return New SqlParameter("@modifiedBy", CurrentUser.UserID)
        End Get
    End Property
    Private Shared ReadOnly Property LocationParam As SqlParameter
        Get
            Return New SqlParameter("@fromLocation", Convert.ToInt32(HeaderDataModificationLocation.FacilityColocationEditor))
        End Get
    End Property

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If AirsNumber Is Nothing Then Return

        LoadCurrentColocatedFacilities()

        AirsNumberDisplay.Text = AirsNumber.FormattedString
        FacilityNameDisplay.Text = DAL.GetFacilityName(AirsNumber)
    End Sub

    Private Sub LoadCurrentColocatedFacilities()
        Dim dt As DataTable = DB.SPGetDataTable("iaip_facility.GetColocatedFacilities", AirsParam)

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            SomethingWasSaved = True
            MessageBox.Show("Facility is not currently in a co-location group.", "Closing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Close()
        Else
            CurrentFacilitiesGrid.DataSource = dt

            If dt.Rows.Count = 1 Then
                btnRemoveFromGroup.Enabled = False
            End If
        End If
    End Sub

    Private Sub CurrentFacilitiesGrid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles CurrentFacilitiesGrid.CellFormatting
        If e IsNot Nothing AndAlso e.ColumnIndex = 0 AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
            e.Value = New ApbFacilityId(e.Value.ToString).FormattedString
        End If
    End Sub

    Private Sub btnRemoveFromGroup_Click(sender As Object, e As EventArgs) Handles btnRemoveFromGroup.Click
        If DB.SPRunCommand("iaip_facility.RemoveFacilityFromColocation", {AirsParam, UserParam, LocationParam}) Then
            SomethingWasSaved = True
            MessageBox.Show("Facility was removed from the co-location group.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        Else
            MessageBox.Show("There was an error removing the facility from the co-location group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadCurrentColocatedFacilities()
        End If
    End Sub

    Private Sub btnRemoveGroup_Click(sender As Object, e As EventArgs) Handles btnRemoveGroup.Click
        If DB.SPRunCommand("iaip_facility.RemoveColocationGroup", {AirsParam, UserParam, LocationParam}) Then
            SomethingWasSaved = True
            MessageBox.Show("The co-location group was removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        Else
            MessageBox.Show("There was an error removing the co-location group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadCurrentColocatedFacilities()
        End If
    End Sub

End Class
