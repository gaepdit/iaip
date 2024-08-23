Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient

Public Class SSCPEnforcementChecklist

#Region " Properties "

    Public Property EnforcementNumber As Integer
    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property SelectedDiscoveryEvent As Integer

#End Region

#Region " Page Load "

    Private Sub SSCPEnforcementChecklist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Me.Modal Then Me.Close()

        CollapseFilterOptions()
        LoadHeader()

        FilterWork()
    End Sub

    Private Sub LoadHeader()
        Dim facilityName As String = DAL.GetFacilityName(AirsNumber)
        FacilityInfo.Text = AirsNumber.FormattedString & " – " & facilityName
        EnforcementInfo.Text = "Enforcement Number: " & EnforcementNumber
        DTPEndDate.Value = Today
        DTPStartDate.Value = Today.AddYears(-1)
    End Sub

    Private Sub CollapseFilterOptions()
        FilterOptionsPanel.Size = New Size(FilterOptionsPanel.Width, 0)
    End Sub

#End Region

#Region " Display work items "

    Private Sub FilterWork()
        Dim SQL As String = "SELECT
                CONVERT(INT, i.STRTRACKINGNUMBER)           AS [Tracking Number],
                i.DATRECEIVEDDATE                           AS [Date Received],
                l.STRACTIVITYNAME                           AS [Work Type],
                CONCAT(u.STRLASTNAME, ', ', u.STRFIRSTNAME) AS [Responsible Staff]
            FROM SSCPITEMMASTER AS i
                INNER JOIN LOOKUPCOMPLIANCEACTIVITIES AS l ON i.STREVENTTYPE = l.STRACTIVITYTYPE
                INNER JOIN EPDUSERPROFILES AS u ON u.NUMUSERID = i.STRRESPONSIBLESTAFF
            WHERE i.STRAIRSNUMBER = @airsnumber
                  AND i.STREVENTTYPE <> '05'
                  AND i.STRTRACKINGNUMBER NOT IN (
                SELECT TrackingNumber
                FROM SSCP_EnforcementEvents
                WHERE EnforcementNumber = @enforcementNumber)"

        Dim eventTypes As New List(Of String)

        If chbACCs.Checked OrElse chbInspections.Checked OrElse chbPerformanceTests.Checked OrElse chbReports.Checked Then
            If chbACCs.Checked Then
                eventTypes.Add("'04'")
            End If

            If chbInspections.Checked Then
                eventTypes.Add("'02'")
            End If

            If chbPerformanceTests.Checked Then
                eventTypes.Add("'03'")
            End If

            If chbReports.Checked Then
                eventTypes.Add("'01'")
            End If

            SQL = SQL & " AND i.STREVENTTYPE in (" & String.Join(",", eventTypes) & ") "
        End If

        If chbFilterDates.Checked Then
            SQL = SQL & " AND i.DATRECEIVEDDATE BETWEEN @startdate AND @enddate "
        End If

        Dim p As SqlParameter() = {
            New SqlParameter("@airsnumber", AirsNumber.DbFormattedString),
            New SqlParameter("@enforcementNumber", EnforcementNumber),
            New SqlParameter("@startdate", DTPStartDate.Value),
            New SqlParameter("@enddate", DTPEndDate.Value)
        }

        Dim dt As DataTable = DB.GetDataTable(SQL, p)
        dt.DefaultView.Sort = "[Date Received] DESC"

        dgvComplianceEvents.DataSource = dt
        dgvComplianceEvents.SanelyResizeColumns

        txtWorkCount.Text = dgvComplianceEvents.RowCount
    End Sub

#End Region

#Region " Events "

    Private Sub OpenFilterOptions_CheckedChanged(sender As Object, e As EventArgs) Handles OpenFilterOptions.CheckedChanged
        If OpenFilterOptions.Checked Then
            FilterOptionsPanel.Size = New Size(FilterOptionsPanel.Width, 217)
        Else
            FilterOptionsPanel.Size = New Size(FilterOptionsPanel.Width, 0)
        End If
    End Sub

    Private Sub btnRunFilter_Click(sender As Object, e As EventArgs) Handles btnRunFilter.Click
        FilterWork()
    End Sub

    Private Sub chbFilterDates_CheckedChanged(sender As Object, e As EventArgs) Handles chbFilterDates.CheckedChanged
        DTPStartDate.Enabled = chbFilterDates.Checked
        DTPEndDate.Enabled = chbFilterDates.Checked
    End Sub

    Private Sub dgvComplianceEvents_SelectionChanged(sender As Object, e As EventArgs) Handles dgvComplianceEvents.SelectionChanged
        If dgvComplianceEvents.CurrentRow IsNot Nothing Then
            Dim row As DataGridViewRow = dgvComplianceEvents.CurrentRow
            TrackingNumberDisplay.Text = row.Cells("Tracking Number").Value
            SelectedDiscoveryEvent = row.Cells("Tracking Number").Value
        End If
    End Sub

#End Region

End Class
