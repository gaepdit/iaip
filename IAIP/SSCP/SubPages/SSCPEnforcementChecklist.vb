Imports System.Data.SqlClient

Public Class SSCPEnforcementChecklist
    Inherits BaseForm

#Region " Properties "

    Public Property EnforcementNumber As String
    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property SelectedDiscoveryEvent As String
        Get
            Return TrackingNumberDisplay.Text
        End Get
        Set(value As String)
            TrackingNumberDisplay.Text = value
        End Set
    End Property

#End Region

#Region " Page Load "

    Private Sub SSCPEnforcementChecklist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Me.Modal Then Me.Close()

        CollapseFilterOptions()
        LoadHeader()
        FormatDataGridForWorkEnTry()

        FilterWork()
    End Sub

    Private Sub LoadHeader()
        Dim facilityName As String = DAL.GetFacilityName(AirsNumber)
        FacilityInfo.Text = AirsNumber.FormattedString & " " & facilityName
        EnforcementInfo.Text = "Enforcement Number: " & EnforcementNumber
        DTPEndDate.Value = Today
        DTPStartDate.Value = Today.AddYears(-1)
    End Sub

    Private Sub CollapseFilterOptions()
        FilterOptionsPanel.Size = New Size(FilterOptionsPanel.Width, 0)
    End Sub

    Private Sub FormatDataGridForWorkEnTry()

        'Formatting our DataGrid
        Dim objGrid As New DataGridTableStyle
        Dim objtextcol As New DataGridTextBoxColumn

        Try

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "tblWorkEntry"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTrackingNumber"
            objtextcol.HeaderText = "Tracking Number"
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strActivityName"
            objtextcol.HeaderText = "Compliance Event"
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReceivedDate"
            objtextcol.HeaderText = "Date Received by GEPD"
            objtextcol.Width = 130
            objtextcol.Format = "yyyy-MM-dd"
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "Staff"
            objtextcol.HeaderText = "Staff Member"
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrComplianceEvents.TableStyles.Clear()
            dgrComplianceEvents.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrComplianceEvents.CaptionText = "Work Currently Entered"
            dgrComplianceEvents.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Display work items "

    Private Sub FilterWork()
        Dim SQLLine As String = ""
        Dim SQLCount As Integer = 0

        Dim SQL As String = "Select substr(AIRBRANCH.SSCPItemMaster.strAIrsnumber, 5) as AIRSNumber, strfacilityName, " &
        "strActivityName, " &
        "to_char(datReceivedDate, 'yyyy-MM-dd') as ReceivedDate, " &
        "strTrackingNumber, (strLastName|| ', ' ||strFirstName) as Staff " &
        "from AIRBRANCH.SSCPItemMaster, " &
        "AIRBRANCH.LookUPComplianceActivities, AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles " &
        "where " &
        "AIRBRANCH.SSCPItemMaster.strEventType = AIRBRANCH.LookUPComplianceActivities.strActivityType " &
        "and AIRBRANCH.SSCPItemMaster.strairsnumber = AIRBRANCH.APBFacilityInformation.strairsnumber " &
        "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCPItemMaster.strResponsibleStaff " &
        "and AIRBRANCH.SSCPItemMaster.strAIRSNumber = :airsnumber " &
        "and AIRBRANCH.SSCPItemMaster.strEventType <> '05' "

        If chbWorkType.Checked = True Then
            If chbAllWork.Checked <> True Then
                If chbACCs.Checked = True Then
                    SQLLine = SQLLine & "AIRBRANCH.SSCPItemMaster.strEventType = '04' "
                    SQLCount += 1
                End If
                If chbInspections.Checked = True Then
                    If SQLCount <> 0 Then
                        SQLLine = SQLLine & "OR AIRBRANCH.SSCPItemMaster.strEventType = '02' "
                    Else
                        SQLLine = SQLLine & "AIRBRANCH.SSCPItemMaster.strEventType = '02' "
                    End If
                    SQLCount += 1
                End If
                If chbPerformanceTests.Checked = True Then
                    If SQLCount <> 0 Then
                        SQLLine = SQLLine & "OR AIRBRANCH.SSCPItemMaster.strEventType = '03' "
                    Else
                        SQLLine = SQLLine & "AIRBRANCH.SSCPItemMaster.strEventType = '03' "
                    End If
                    SQLCount += 1
                End If
                If chbReports.Checked = True Then
                    If SQLCount <> 0 Then
                        SQLLine = SQLLine & "OR AIRBRANCH.SSCPItemMaster.strEventType = '01' "
                    Else
                        SQLLine = SQLLine & "AIRBRANCH.SSCPItemMaster.strEventType = '01' "
                    End If
                    SQLCount += 1
                End If
                If SQLLine <> "" Then
                    SQLLine = " And (" & SQLLine & ") "
                End If
            End If
        End If

        If chbFilterDates.Checked Then
            SQLLine = SQLLine & " and AIRBRANCH.SSCPItemMaster.datReceivedDate between :startdate and :enddate "
        End If

        If SQLLine <> "" Then
            SQL = SQL & SQLLine & " Order by datReceivedDate DESC, strTrackingNumber DESC "
        End If

        Dim oraparams(3) As SqlParameter

        If chbFilterDates.Checked Then
            oraparams = {
                New SqlParameter("airsnumber", AirsNumber.DbFormattedString),
                New SqlParameter("startdate", DTPStartDate.Value),
                New SqlParameter("enddate", DTPEndDate.Value)
            }
        Else
            oraparams = {
                New SqlParameter("airsnumber", AirsNumber.DbFormattedString)
            }
        End If

        Dim dt As DataTable = DB.GetDataTable(SQL, oraparams)
        dt.TableName = "tblWorkEntry"
        dgrComplianceEvents.DataSource = dt

        txtWorkCount.Text = dt.Rows.Count
    End Sub

#End Region

#Region " Events "

    Private Sub OpenFilterOptions_CheckedChanged(sender As Object, e As EventArgs) Handles OpenFilterOptions.CheckedChanged
        If OpenFilterOptions.Checked Then
            FilterOptionsPanel.Size = New Size(FilterOptionsPanel.Width, 260)
        Else
            FilterOptionsPanel.Size = New Size(FilterOptionsPanel.Width, 0)
        End If
    End Sub

    Private Sub btnRunFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunFilter.Click
        FilterWork()
    End Sub

    Private Sub chbFilterDates_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbFilterDates.CheckedChanged
        If chbFilterDates.Checked = True Then
            DTPStartDate.Enabled = True
            DTPEndDate.Enabled = True
        Else
            DTPStartDate.Enabled = False
            DTPEndDate.Enabled = False
        End If
    End Sub

    Private Sub dgrComplianceEvents_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrComplianceEvents.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrComplianceEvents.HitTest(e.X, e.Y)
        If hti.Type = DataGrid.HitTestType.Cell AndAlso Not IsDBNull(dgrComplianceEvents(hti.Row, 0)) Then
            TrackingNumberDisplay.Text = dgrComplianceEvents(hti.Row, 0)
        End If
    End Sub

#End Region

End Class
