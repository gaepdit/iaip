Public Class IAIPLookUpTables
    Private AppTypesLoaded As Boolean

    Private Sub IAIPLookUpTables_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadAPBManagement()
    End Sub

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
        AcceptButton = btnSaveAPBManagement
    End Sub

    Private Sub txtApbManagementName_Leave(sender As Object, e As EventArgs) Handles txtApbManagementName.Leave
        AcceptButton = Nothing
    End Sub

#End Region

End Class