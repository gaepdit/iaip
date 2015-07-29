Imports Oracle.ManagedDataAccess.Client


Public Class SSCPEnforcementChecklist
    Inherits BaseForm

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents FilterOptionsPanel As System.Windows.Forms.Panel
    Friend WithEvents EnforcementInfo As System.Windows.Forms.Label
    Friend WithEvents FacilityInfo As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OpenFilterOptions As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chbWorkType As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnRunFilter As System.Windows.Forms.Button
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chbFilterDates As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DTPStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgrComplianceEvents As System.Windows.Forms.DataGrid
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtWorkCount As System.Windows.Forms.TextBox
    Friend WithEvents chbAllWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbInspections As System.Windows.Forms.CheckBox
    Friend WithEvents chbPerformanceTests As System.Windows.Forms.CheckBox
    Friend WithEvents chbReports As System.Windows.Forms.CheckBox
    Friend WithEvents chbACCs As System.Windows.Forms.CheckBox
    Friend WithEvents TrackingNumberDisplay As System.Windows.Forms.Label
    Friend WithEvents LinkEventButton As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.OpenFilterOptions = New System.Windows.Forms.CheckBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.LinkEventButton = New System.Windows.Forms.Button()
        Me.TrackingNumberDisplay = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EnforcementInfo = New System.Windows.Forms.Label()
        Me.FacilityInfo = New System.Windows.Forms.Label()
        Me.dgrComplianceEvents = New System.Windows.Forms.DataGrid()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtWorkCount = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.DTPEndDate = New System.Windows.Forms.DateTimePicker()
        Me.DTPStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chbFilterDates = New System.Windows.Forms.CheckBox()
        Me.btnRunFilter = New System.Windows.Forms.Button()
        Me.chbWorkType = New System.Windows.Forms.CheckBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chbInspections = New System.Windows.Forms.CheckBox()
        Me.chbPerformanceTests = New System.Windows.Forms.CheckBox()
        Me.chbReports = New System.Windows.Forms.CheckBox()
        Me.chbACCs = New System.Windows.Forms.CheckBox()
        Me.chbAllWork = New System.Windows.Forms.CheckBox()
        Me.FilterOptionsPanel = New System.Windows.Forms.Panel()
        Me.Panel4.SuspendLayout()
        CType(Me.dgrComplianceEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.FilterOptionsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.OpenFilterOptions)
        Me.Panel4.Controls.Add(Me.Cancel)
        Me.Panel4.Controls.Add(Me.LinkEventButton)
        Me.Panel4.Controls.Add(Me.TrackingNumberDisplay)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.EnforcementInfo)
        Me.Panel4.Controls.Add(Me.FacilityInfo)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(367, 138)
        Me.Panel4.TabIndex = 0
        '
        'OpenFilterOptions
        '
        Me.OpenFilterOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OpenFilterOptions.Appearance = System.Windows.Forms.Appearance.Button
        Me.OpenFilterOptions.Location = New System.Drawing.Point(265, 109)
        Me.OpenFilterOptions.Name = "OpenFilterOptions"
        Me.OpenFilterOptions.Size = New System.Drawing.Size(90, 23)
        Me.OpenFilterOptions.TabIndex = 6
        Me.OpenFilterOptions.Text = "Filter Options »"
        Me.OpenFilterOptions.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(109, 94)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(86, 38)
        Me.Cancel.TabIndex = 5
        Me.Cancel.Text = "Cancel"
        '
        'LinkEventButton
        '
        Me.LinkEventButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.LinkEventButton.Location = New System.Drawing.Point(12, 94)
        Me.LinkEventButton.Name = "LinkEventButton"
        Me.LinkEventButton.Size = New System.Drawing.Size(91, 38)
        Me.LinkEventButton.TabIndex = 4
        Me.LinkEventButton.Text = "Link Event"
        '
        'TrackingNumberDisplay
        '
        Me.TrackingNumberDisplay.AutoSize = True
        Me.TrackingNumberDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrackingNumberDisplay.Location = New System.Drawing.Point(191, 65)
        Me.TrackingNumberDisplay.Name = "TrackingNumberDisplay"
        Me.TrackingNumberDisplay.Size = New System.Drawing.Size(16, 17)
        Me.TrackingNumberDisplay.TabIndex = 3
        Me.TrackingNumberDisplay.Text = "#"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(173, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Selected Discovery Event:"
        '
        'EnforcementInfo
        '
        Me.EnforcementInfo.AutoSize = True
        Me.EnforcementInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnforcementInfo.Location = New System.Drawing.Point(12, 37)
        Me.EnforcementInfo.Name = "EnforcementInfo"
        Me.EnforcementInfo.Size = New System.Drawing.Size(88, 17)
        Me.EnforcementInfo.TabIndex = 1
        Me.EnforcementInfo.Text = "Enforcement"
        Me.EnforcementInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'FacilityInfo
        '
        Me.FacilityInfo.AutoSize = True
        Me.FacilityInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityInfo.Location = New System.Drawing.Point(12, 9)
        Me.FacilityInfo.Name = "FacilityInfo"
        Me.FacilityInfo.Size = New System.Drawing.Size(51, 17)
        Me.FacilityInfo.TabIndex = 0
        Me.FacilityInfo.Text = "Facility"
        Me.FacilityInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'dgrComplianceEvents
        '
        Me.dgrComplianceEvents.DataMember = ""
        Me.dgrComplianceEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrComplianceEvents.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrComplianceEvents.Location = New System.Drawing.Point(0, 380)
        Me.dgrComplianceEvents.Name = "dgrComplianceEvents"
        Me.dgrComplianceEvents.ReadOnly = True
        Me.dgrComplianceEvents.Size = New System.Drawing.Size(367, 122)
        Me.dgrComplianceEvents.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(266, 190)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 13)
        Me.Label8.TabIndex = 289
        Me.Label8.Text = "Record Count"
        '
        'txtWorkCount
        '
        Me.txtWorkCount.Location = New System.Drawing.Point(266, 206)
        Me.txtWorkCount.Name = "txtWorkCount"
        Me.txtWorkCount.ReadOnly = True
        Me.txtWorkCount.Size = New System.Drawing.Size(90, 20)
        Me.txtWorkCount.TabIndex = 5
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.DTPEndDate)
        Me.Panel6.Controls.Add(Me.DTPStartDate)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Location = New System.Drawing.Point(17, 151)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(237, 75)
        Me.Panel6.TabIndex = 3
        '
        'DTPEndDate
        '
        Me.DTPEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEndDate.Enabled = False
        Me.DTPEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEndDate.Location = New System.Drawing.Point(121, 34)
        Me.DTPEndDate.Name = "DTPEndDate"
        Me.DTPEndDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPEndDate.TabIndex = 1
        Me.DTPEndDate.Value = New Date(2005, 5, 13, 0, 0, 0, 0)
        '
        'DTPStartDate
        '
        Me.DTPStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartDate.Enabled = False
        Me.DTPStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartDate.Location = New System.Drawing.Point(15, 34)
        Me.DTPStartDate.Name = "DTPStartDate"
        Me.DTPStartDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPStartDate.TabIndex = 0
        Me.DTPStartDate.Value = New Date(2005, 5, 13, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(124, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "End Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Start Date"
        '
        'chbFilterDates
        '
        Me.chbFilterDates.AutoSize = True
        Me.chbFilterDates.Location = New System.Drawing.Point(12, 142)
        Me.chbFilterDates.Name = "chbFilterDates"
        Me.chbFilterDates.Size = New System.Drawing.Size(98, 17)
        Me.chbFilterDates.TabIndex = 2
        Me.chbFilterDates.Text = "Date Received"
        '
        'btnRunFilter
        '
        Me.btnRunFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunFilter.Location = New System.Drawing.Point(266, 14)
        Me.btnRunFilter.Name = "btnRunFilter"
        Me.btnRunFilter.Size = New System.Drawing.Size(90, 23)
        Me.btnRunFilter.TabIndex = 4
        Me.btnRunFilter.Text = "Run Filter"
        '
        'chbWorkType
        '
        Me.chbWorkType.AutoSize = True
        Me.chbWorkType.Checked = True
        Me.chbWorkType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbWorkType.Location = New System.Drawing.Point(12, 6)
        Me.chbWorkType.Name = "chbWorkType"
        Me.chbWorkType.Size = New System.Drawing.Size(79, 17)
        Me.chbWorkType.TabIndex = 0
        Me.chbWorkType.Text = "Work Type"
        '
        'Panel5
        '
        Me.Panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.chbInspections)
        Me.Panel5.Controls.Add(Me.chbPerformanceTests)
        Me.Panel5.Controls.Add(Me.chbReports)
        Me.Panel5.Controls.Add(Me.chbACCs)
        Me.Panel5.Controls.Add(Me.chbAllWork)
        Me.Panel5.Location = New System.Drawing.Point(16, 14)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(238, 113)
        Me.Panel5.TabIndex = 1
        '
        'chbInspections
        '
        Me.chbInspections.Location = New System.Drawing.Point(16, 44)
        Me.chbInspections.Name = "chbInspections"
        Me.chbInspections.Size = New System.Drawing.Size(104, 24)
        Me.chbInspections.TabIndex = 2
        Me.chbInspections.Text = "Inspections"
        '
        'chbPerformanceTests
        '
        Me.chbPerformanceTests.Location = New System.Drawing.Point(16, 63)
        Me.chbPerformanceTests.Name = "chbPerformanceTests"
        Me.chbPerformanceTests.Size = New System.Drawing.Size(136, 24)
        Me.chbPerformanceTests.TabIndex = 3
        Me.chbPerformanceTests.Text = "Performance Tests"
        '
        'chbReports
        '
        Me.chbReports.Location = New System.Drawing.Point(16, 82)
        Me.chbReports.Name = "chbReports"
        Me.chbReports.Size = New System.Drawing.Size(104, 24)
        Me.chbReports.TabIndex = 4
        Me.chbReports.Text = "Reports"
        '
        'chbACCs
        '
        Me.chbACCs.Location = New System.Drawing.Point(16, 26)
        Me.chbACCs.Name = "chbACCs"
        Me.chbACCs.Size = New System.Drawing.Size(208, 24)
        Me.chbACCs.TabIndex = 1
        Me.chbACCs.Text = "Annual Compliance Certifications"
        '
        'chbAllWork
        '
        Me.chbAllWork.Checked = True
        Me.chbAllWork.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbAllWork.Location = New System.Drawing.Point(16, 8)
        Me.chbAllWork.Name = "chbAllWork"
        Me.chbAllWork.Size = New System.Drawing.Size(104, 24)
        Me.chbAllWork.TabIndex = 0
        Me.chbAllWork.Text = "All"
        '
        'FilterOptionsPanel
        '
        Me.FilterOptionsPanel.Controls.Add(Me.chbFilterDates)
        Me.FilterOptionsPanel.Controls.Add(Me.chbWorkType)
        Me.FilterOptionsPanel.Controls.Add(Me.txtWorkCount)
        Me.FilterOptionsPanel.Controls.Add(Me.Label8)
        Me.FilterOptionsPanel.Controls.Add(Me.Panel6)
        Me.FilterOptionsPanel.Controls.Add(Me.btnRunFilter)
        Me.FilterOptionsPanel.Controls.Add(Me.Panel5)
        Me.FilterOptionsPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterOptionsPanel.Location = New System.Drawing.Point(0, 138)
        Me.FilterOptionsPanel.Name = "FilterOptionsPanel"
        Me.FilterOptionsPanel.Size = New System.Drawing.Size(367, 242)
        Me.FilterOptionsPanel.TabIndex = 1
        '
        'SSCPEnforcementChecklist
        '
        Me.AcceptButton = Me.LinkEventButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(367, 502)
        Me.Controls.Add(Me.dgrComplianceEvents)
        Me.Controls.Add(Me.FilterOptionsPanel)
        Me.Controls.Add(Me.Panel4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(383, 510)
        Me.Name = "SSCPEnforcementChecklist"
        Me.Text = "Enforcement Linking tool"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgrComplianceEvents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.FilterOptionsPanel.ResumeLayout(False)
        Me.FilterOptionsPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

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
        monitor.TrackFeature("Forms." & Me.Name)
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

        Dim SQL As String = "Select substr(AIRBRANCH.SSCPItemMaster.strAIrsnumber, 5) as AIRSNumber, strfacilityName, " & _
        "strActivityName, " & _
        "to_char(datReceivedDate, 'yyyy-MM-dd') as ReceivedDate, " & _
        "strTrackingNumber, (strLastName|| ', ' ||strFirstName) as Staff " & _
        "from AIRBRANCH.SSCPItemMaster, " & _
        "AIRBRANCH.LookUPComplianceActivities, AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles " & _
        "where " & _
        "AIRBRANCH.SSCPItemMaster.strEventType = AIRBRANCH.LookUPComplianceActivities.strActivityType " & _
        "and AIRBRANCH.SSCPItemMaster.strairsnumber = AIRBRANCH.APBFacilityInformation.strairsnumber " & _
        "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCPItemMaster.strResponsibleStaff " & _
        "and AIRBRANCH.SSCPItemMaster.strAIRSNumber = :airsnumber " & _
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

        Dim oraparams(3) As OracleParameter

        If chbFilterDates.Checked Then
            oraparams = {
                New OracleParameter("airsnumber", AirsNumber.DbFormattedString),
                New OracleParameter("startdate", DTPStartDate.Value),
                New OracleParameter("enddate", DTPEndDate.Value)
            }
        Else
            oraparams = {
                New OracleParameter("airsnumber", AirsNumber.DbFormattedString)
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
