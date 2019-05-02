Public Class IAIPLookUpTables
    Private AppTypesLoaded As Boolean = False

    Private Sub IAIPLookUpTables_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadAPBManagement()
    End Sub

#Region "ApplicationTypes"

    Private Sub TCLookUpTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TCLookUpTables.SelectedIndexChanged
        If Not AppTypesLoaded AndAlso TCLookUpTables.SelectedTab Is TPApplicationTypes Then
            LoadApplicationTypes()
            AppTypesLoaded = True
        End If
    End Sub

    Private Sub LoadApplicationTypes()
        dgvApplicationType.DataSource = DAL.Sspp.GetApplicationTypes(True)

        With dgvApplicationType
            .RowHeadersVisible = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            .AllowUserToResizeColumns = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .SanelyResizeColumns
            .ClearSelection()
        End With
    End Sub

    Private Sub dgvApplicationType_SelectionChanged(sender As Object, e As EventArgs) Handles dgvApplicationType.SelectionChanged
        If dgvApplicationType.SelectedRows.Count = 1 Then
            txtSelectedAppType.Text = dgvApplicationType.SelectedRows(0).Cells("Application Type").Value.ToString
        Else
            txtSelectedAppType.Text = ""
        End If
    End Sub

    Private Sub btnAddNewAppType_Click(sender As Object, e As EventArgs) Handles btnAddNewAppType.Click
        If Not String.IsNullOrWhiteSpace(txtNewAppType.Text) Then
            DAL.Sspp.SaveNewApplicationType(txtNewAppType.Text)
            txtNewAppType.Text = ""
            LoadApplicationTypes()
        End If
    End Sub

    Private Sub btnChangeAppStatus_Click(sender As Object, e As EventArgs) Handles btnChangeAppStatus.Click
        If dgvApplicationType.SelectedRows.Count = 1 Then
            Dim newStatus As Boolean = (dgvApplicationType.SelectedRows(0).Cells("Status").Value = "Inactive")
            DAL.Sspp.UpdateApplicationTypeStatus(dgvApplicationType.SelectedRows(0).Cells("Application Type Code").Value, newStatus)
            LoadApplicationTypes()
        End If
    End Sub

#End Region

#Region " EPD Management "

    Private Sub LoadAPBManagement()
        dgvLookUpManagement.DataSource = GetSharedData(SharedTable.EpdManagers)

        With dgvLookUpManagement
            .RowHeadersVisible = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            .AllowUserToResizeColumns = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .SanelyResizeColumns
        End With
    End Sub

    Private Sub dgvLookUpManagement_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLookUpManagement.SelectionChanged
        If dgvLookUpManagement.SelectedRows.Count = 1 Then
            txtApbManagementType.Text = dgvLookUpManagement.SelectedRows(0).Cells("Role").Value
            txtApbManagementName.Text = dgvLookUpManagement.SelectedRows(0).Cells("Name").Value
        End If
    End Sub

    Private Sub SaveAPBManagement()
        If Not String.IsNullOrEmpty(txtApbManagementType.Text) Then
            Dim role As DAL.EpdManagementTypes
            If [Enum].TryParse(txtApbManagementType.Text, role) Then
                If DAL.SaveEpdManagerName(role, txtApbManagementName.Text) Then
                    MessageBox.Show("Success")
                Else
                    MessageBox.Show("Failure")
                End If
            Else
                MessageBox.Show("Did not recognize management type")
            End If
            LoadAPBManagement()
        End If
    End Sub

    Private Sub btnSaveAPBManagement_Click(sender As Object, e As EventArgs) Handles btnSaveAPBManagement.Click
        SaveAPBManagement()
    End Sub

    Private Sub txtApbManagementName_Enter(sender As Object, e As EventArgs) Handles txtApbManagementName.Enter
        Me.AcceptButton = btnSaveAPBManagement
    End Sub

    Private Sub txtApbManagementName_Leave(sender As Object, e As EventArgs) Handles txtApbManagementName.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

End Class