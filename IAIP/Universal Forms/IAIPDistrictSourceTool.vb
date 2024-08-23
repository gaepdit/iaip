Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient
Imports System.Linq
Imports Iaip.DAL

Public Class IAIPDistrictSourceTool
    Private DistrictCountyAssignments As DataTable

#Region "Page Load"

    Private Sub IAIPDistrictSourcesTool_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadCounties()
        LoadDistrictData()
        LoadCountyAssignments()
    End Sub

    Private Sub LoadCounties()
        With clbCounties
            .DataSource = GetSharedData(SharedTable.Counties).Copy
            .DisplayMember = "County"
            .ValueMember = "CountyCode"
        End With
    End Sub

    Private Sub LoadDistrictData()
        With cboDistricts
            .DataSource = GetSharedData(SharedTable.DistrictOffices).Copy
            .DisplayMember = "DistrictName"
            .ValueMember = "DistrictCode"
            .SelectedIndex = -1
            AddHandler .SelectedIndexChanged, AddressOf cboDistricts_SelectedIndexChanged
        End With

        With lsbDistricts
            .DataSource = GetSharedData(SharedTable.DistrictOffices).Copy
            .DisplayMember = "DistrictName"
            .ValueMember = "DistrictCode"
            .SelectedIndex = -1
            AddHandler .SelectedIndexChanged, AddressOf lsbDistricts_SelectedIndexChanged
        End With

        With cboDistrictManager
            .DataSource = GetStaffAsDataTableByBranch(5)
            .DisplayMember = "UserName"
            .ValueMember = "UserID"
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub LoadCountyAssignments()
        DistrictCountyAssignments = GetDistrictCountyAssignments()
    End Sub

#End Region

    Private Sub SaveDistrictList()
        Dim SQL As String = "UPDATE LOOKUPDISTRICTINFORMATION
            SET STRDISTRICTCODE = @districtcode
            WHERE STRDISTRICTCOUNTY IN (SELECT * FROM @counties)"

        Dim counties As New HashSet(Of String)

        For Each drv As DataRowView In clbCounties.CheckedItems
            counties.Add(drv.Item("CountyCode").ToString)
        Next

        If counties.Count > 0 Then
            Dim p As SqlParameter() = {
                New SqlParameter("@districtcode", cboDistricts.SelectedValue),
                counties.AsEnumerable.AsTvpSqlParameter("@counties")
            }

            DB.RunCommand(SQL, p)

            Dim dr As DataRow

            For Each cc As String In counties
                dr = DistrictCountyAssignments.Select("CountyCode=" & cc)(0)
                dr.Item("DistrictCode") = cboDistricts.SelectedValue
            Next
        End If
    End Sub

    Private Sub SaveNewDistricts()
        If txtNewDistrict.Text <> "" AndAlso txtNewDistrictCode.Text <> "" AndAlso cboDistrictManager.SelectedIndex > -1 Then
            Dim exists As Boolean = (GetSharedData(SharedTable.DistrictOffices).AsEnumerable.Any(Function(dr) dr.Item("DistrictCode") = txtNewDistrictCode.Text))
            Dim SQL As String
            Dim countycode As String = txtNewDistrictCode.Text

            If exists Then
                SQL = "Update LookUPDistricts set " &
                    "strDistrictName = @name, " &
                    "strDistrictManager = @mgr " &
                    "where strDistrictCode = @code "
            Else
                SQL = "Insert into LookUPDistricts " &
                    "(strDistrictCode, strDistrictName, strDistrictManager) " &
                    "values " &
                    "(@code, @name, @mgr) "
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@name", txtNewDistrict.Text),
                New SqlParameter("@code", countycode),
                New SqlParameter("@mgr", cboDistrictManager.SelectedValue)
            }

            DB.RunCommand(SQL, p)

            ClearSharedData(SharedTable.DistrictOffices)

            RemoveHandler lsbDistricts.SelectedIndexChanged, AddressOf lsbDistricts_SelectedIndexChanged
            RemoveHandler cboDistricts.SelectedIndexChanged, AddressOf cboDistricts_SelectedIndexChanged

            LoadDistrictData()
            ClearChecks()

            lsbDistricts.SelectedValue = countycode
        End If
    End Sub

#Region " Form events "

    Private Sub btnClearChecks_Click(sender As Object, e As EventArgs) Handles btnClearChecks.Click
        ClearChecks()
    End Sub

    Private Sub btnSaveDistricts_Click(sender As Object, e As EventArgs) Handles btnSaveDistricts.Click
        SaveDistrictList()
    End Sub

    Private Sub btnAddUpdateInfo_Click(sender As Object, e As EventArgs) Handles btnAddUpdateInfo.Click
        SaveNewDistricts()
    End Sub

    Private Sub cboDistricts_SelectedIndexChanged(sender As Object, e As EventArgs)
        ViewDistrictAssignments()
    End Sub

    Private Sub lsbDistricts_SelectedIndexChanged(sender As Object, e As EventArgs)
        DisplaySelectedDistrict()
    End Sub

#End Region

    Private Sub DisplaySelectedDistrict()
        If lsbDistricts.SelectedIndex >= 0 Then
            Dim drv As DataRowView = CType(lsbDistricts.SelectedItem, DataRowView)

            txtNewDistrictCode.Text = drv.Item("DistrictCode")
            txtNewDistrict.Text = drv.Item("DistrictName")

            Dim exists As Boolean = (CType(cboDistrictManager.DataSource, DataTable).Select("UserId=" & drv.Item("Manager").ToString).Any)
            If exists Then
                cboDistrictManager.SelectedValue = drv.Item("Manager")
            End If
        End If
    End Sub

    Private Sub ClearChecks()
        For i As Integer = 0 To clbCounties.Items.Count - 1
            clbCounties.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub ViewDistrictAssignments()
        Dim cty As String
        Dim dst As String = cboDistricts.SelectedValue

        ClearChecks()

        For i As Integer = 0 To clbCounties.Items.Count - 1
            cty = CType(clbCounties.Items.Item(i), DataRowView).Item("CountyCode")

            If DistrictCountyAssignments.AsEnumerable.Any(Function(dr) dr.Item("CountyCode") = cty AndAlso dr.Item("DistrictCode") = dst) Then
                clbCounties.SetItemChecked(i, True)
            End If
        Next
    End Sub

    'Form overrides dispose to clean up the component list. 
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If DistrictCountyAssignments IsNot Nothing Then DistrictCountyAssignments.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class