<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSCPComplianceLog
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pnlFilterPanel = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GBWorkTypes = New System.Windows.Forms.GroupBox()
        Me.chbRMPInspections = New System.Windows.Forms.CheckBox()
        Me.GBNotifications = New System.Windows.Forms.GroupBox()
        Me.clbNotifications = New System.Windows.Forms.CheckedListBox()
        Me.chbFCE = New System.Windows.Forms.CheckBox()
        Me.chbEnforcement = New System.Windows.Forms.CheckBox()
        Me.chbAllWork = New System.Windows.Forms.CheckBox()
        Me.chbNotifications = New System.Windows.Forms.CheckBox()
        Me.chbPerformanceTests = New System.Windows.Forms.CheckBox()
        Me.chbReports = New System.Windows.Forms.CheckBox()
        Me.chbInspections = New System.Windows.Forms.CheckBox()
        Me.chbACCs = New System.Windows.Forms.CheckBox()
        Me.GBEnforcementDates = New System.Windows.Forms.GroupBox()
        Me.chbLastModifiedDate = New System.Windows.Forms.CheckBox()
        Me.chbFilterDates = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.DTPFilterEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.DTPFilterStart = New System.Windows.Forms.DateTimePicker()
        Me.txtWorkCount = New System.Windows.Forms.TextBox()
        Me.btnRunFilter = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chbCompletedWork = New System.Windows.Forms.CheckBox()
        Me.chbOpenWork = New System.Windows.Forms.CheckBox()
        Me.chbDeletedWork = New System.Windows.Forms.CheckBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtFacilityNameFilter = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtFCENumberFilter = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtEnforcementNumberFilter = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTrackingNumberFilter = New System.Windows.Forms.TextBox()
        Me.txtAIRSNumberFilter = New System.Windows.Forms.TextBox()
        Me.GBEngineer = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.clbDistrictOffices = New System.Windows.Forms.CheckedListBox()
        Me.clbAirBranchUnits = New System.Windows.Forms.CheckedListBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdbUseUnits = New System.Windows.Forms.RadioButton()
        Me.rdbUseEngineer = New System.Windows.Forms.RadioButton()
        Me.rdbIgnoreEngineer = New System.Windows.Forms.RadioButton()
        Me.clbEngineer = New System.Windows.Forms.CheckedListBox()
        Me.chbEngineer = New System.Windows.Forms.CheckBox()
        Me.TCComplianceLog = New System.Windows.Forms.TabControl()
        Me.TPSelectWork = New System.Windows.Forms.TabPage()
        Me.btnOpenSummary = New System.Windows.Forms.Button()
        Me.btnUndeleteWork = New System.Windows.Forms.Button()
        Me.btnDeleteWork = New System.Windows.Forms.Button()
        Me.btnSelectWork = New System.Windows.Forms.Button()
        Me.lblWorkType = New System.Windows.Forms.Label()
        Me.txtWorkNumber = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFacilityCity = New System.Windows.Forms.TextBox()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTestType = New System.Windows.Forms.TextBox()
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TPStartNewWork = New System.Windows.Forms.TabPage()
        Me.pnlOtherEvents = New System.Windows.Forms.Panel()
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblOtherNumber = New System.Windows.Forms.Label()
        Me.cboEvent = New System.Windows.Forms.ComboBox()
        Me.LabelEventDescription = New System.Windows.Forms.Label()
        Me.DTPDateReceived = New System.Windows.Forms.DateTimePicker()
        Me.lblDateField = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rdbPerformanceTest = New System.Windows.Forms.RadioButton()
        Me.rdbOther = New System.Windows.Forms.RadioButton()
        Me.rdbFCE = New System.Windows.Forms.RadioButton()
        Me.rdbEnforcementAction = New System.Windows.Forms.RadioButton()
        Me.btnAddNewEntry = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFacilityInformation = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNewAIRSNumber = New System.Windows.Forms.TextBox()
        Me.dgvWork = New System.Windows.Forms.DataGridView()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FacilitySearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.pnlFilterPanel.SuspendLayout()
        Me.GBWorkTypes.SuspendLayout()
        Me.GBNotifications.SuspendLayout()
        Me.GBEnforcementDates.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GBEngineer.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TCComplianceLog.SuspendLayout()
        Me.TPSelectWork.SuspendLayout()
        Me.TPStartNewWork.SuspendLayout()
        Me.pnlOtherEvents.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgvWork, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pnlFilterPanel)
        Me.GroupBox1.Controls.Add(Me.TCComplianceLog)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(831, 430)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter and Sort Option"
        '
        'pnlFilterPanel
        '
        Me.pnlFilterPanel.Controls.Add(Me.Label7)
        Me.pnlFilterPanel.Controls.Add(Me.GBWorkTypes)
        Me.pnlFilterPanel.Controls.Add(Me.GBEnforcementDates)
        Me.pnlFilterPanel.Controls.Add(Me.txtWorkCount)
        Me.pnlFilterPanel.Controls.Add(Me.btnRunFilter)
        Me.pnlFilterPanel.Controls.Add(Me.GroupBox5)
        Me.pnlFilterPanel.Controls.Add(Me.GroupBox7)
        Me.pnlFilterPanel.Controls.Add(Me.GBEngineer)
        Me.pnlFilterPanel.Location = New System.Drawing.Point(4, 16)
        Me.pnlFilterPanel.Name = "pnlFilterPanel"
        Me.pnlFilterPanel.Size = New System.Drawing.Size(593, 417)
        Me.pnlFilterPanel.TabIndex = 303
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(104, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 302
        Me.Label7.Text = "Results"
        '
        'GBWorkTypes
        '
        Me.GBWorkTypes.Controls.Add(Me.chbRMPInspections)
        Me.GBWorkTypes.Controls.Add(Me.GBNotifications)
        Me.GBWorkTypes.Controls.Add(Me.chbFCE)
        Me.GBWorkTypes.Controls.Add(Me.chbEnforcement)
        Me.GBWorkTypes.Controls.Add(Me.chbAllWork)
        Me.GBWorkTypes.Controls.Add(Me.chbNotifications)
        Me.GBWorkTypes.Controls.Add(Me.chbPerformanceTests)
        Me.GBWorkTypes.Controls.Add(Me.chbReports)
        Me.GBWorkTypes.Controls.Add(Me.chbInspections)
        Me.GBWorkTypes.Controls.Add(Me.chbACCs)
        Me.GBWorkTypes.Location = New System.Drawing.Point(2, 96)
        Me.GBWorkTypes.Name = "GBWorkTypes"
        Me.GBWorkTypes.Size = New System.Drawing.Size(198, 310)
        Me.GBWorkTypes.TabIndex = 2
        Me.GBWorkTypes.TabStop = False
        Me.GBWorkTypes.Text = "Work Type"
        '
        'chbRMPInspections
        '
        Me.chbRMPInspections.AutoSize = True
        Me.chbRMPInspections.Enabled = False
        Me.chbRMPInspections.Location = New System.Drawing.Point(8, 132)
        Me.chbRMPInspections.Name = "chbRMPInspections"
        Me.chbRMPInspections.Size = New System.Drawing.Size(155, 17)
        Me.chbRMPInspections.TabIndex = 7
        Me.chbRMPInspections.Text = "Risk Mgmt. Plan Inspection"
        '
        'GBNotifications
        '
        Me.GBNotifications.BackColor = System.Drawing.SystemColors.Control
        Me.GBNotifications.Controls.Add(Me.clbNotifications)
        Me.GBNotifications.Enabled = False
        Me.GBNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GBNotifications.Location = New System.Drawing.Point(8, 173)
        Me.GBNotifications.Name = "GBNotifications"
        Me.GBNotifications.Size = New System.Drawing.Size(183, 133)
        Me.GBNotifications.TabIndex = 9
        Me.GBNotifications.TabStop = False
        Me.GBNotifications.Text = "Notification Types"
        '
        'clbNotifications
        '
        Me.clbNotifications.CheckOnClick = True
        Me.clbNotifications.FormattingEnabled = True
        Me.clbNotifications.Location = New System.Drawing.Point(6, 20)
        Me.clbNotifications.Name = "clbNotifications"
        Me.clbNotifications.Size = New System.Drawing.Size(169, 109)
        Me.clbNotifications.TabIndex = 0
        '
        'chbFCE
        '
        Me.chbFCE.AutoSize = True
        Me.chbFCE.Enabled = False
        Me.chbFCE.Location = New System.Drawing.Point(8, 65)
        Me.chbFCE.Name = "chbFCE"
        Me.chbFCE.Size = New System.Drawing.Size(153, 17)
        Me.chbFCE.TabIndex = 3
        Me.chbFCE.Text = "Full Compliance Evaluation"
        '
        'chbEnforcement
        '
        Me.chbEnforcement.AutoSize = True
        Me.chbEnforcement.Enabled = False
        Me.chbEnforcement.Location = New System.Drawing.Point(8, 48)
        Me.chbEnforcement.Name = "chbEnforcement"
        Me.chbEnforcement.Size = New System.Drawing.Size(86, 17)
        Me.chbEnforcement.TabIndex = 2
        Me.chbEnforcement.Text = "Enforcement"
        '
        'chbAllWork
        '
        Me.chbAllWork.Checked = True
        Me.chbAllWork.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbAllWork.Location = New System.Drawing.Point(8, 16)
        Me.chbAllWork.Name = "chbAllWork"
        Me.chbAllWork.Size = New System.Drawing.Size(40, 16)
        Me.chbAllWork.TabIndex = 0
        Me.chbAllWork.Text = "All"
        '
        'chbNotifications
        '
        Me.chbNotifications.AutoSize = True
        Me.chbNotifications.Enabled = False
        Me.chbNotifications.Location = New System.Drawing.Point(8, 150)
        Me.chbNotifications.Name = "chbNotifications"
        Me.chbNotifications.Size = New System.Drawing.Size(79, 17)
        Me.chbNotifications.TabIndex = 8
        Me.chbNotifications.Text = "Notification"
        '
        'chbPerformanceTests
        '
        Me.chbPerformanceTests.AutoSize = True
        Me.chbPerformanceTests.Enabled = False
        Me.chbPerformanceTests.Location = New System.Drawing.Point(8, 98)
        Me.chbPerformanceTests.Name = "chbPerformanceTests"
        Me.chbPerformanceTests.Size = New System.Drawing.Size(110, 17)
        Me.chbPerformanceTests.TabIndex = 5
        Me.chbPerformanceTests.Text = "Performance Test"
        '
        'chbReports
        '
        Me.chbReports.AutoSize = True
        Me.chbReports.Enabled = False
        Me.chbReports.Location = New System.Drawing.Point(8, 115)
        Me.chbReports.Name = "chbReports"
        Me.chbReports.Size = New System.Drawing.Size(58, 17)
        Me.chbReports.TabIndex = 6
        Me.chbReports.Text = "Report"
        '
        'chbInspections
        '
        Me.chbInspections.AutoSize = True
        Me.chbInspections.Enabled = False
        Me.chbInspections.Location = New System.Drawing.Point(8, 82)
        Me.chbInspections.Name = "chbInspections"
        Me.chbInspections.Size = New System.Drawing.Size(75, 17)
        Me.chbInspections.TabIndex = 4
        Me.chbInspections.Text = "Inspection"
        '
        'chbACCs
        '
        Me.chbACCs.AutoSize = True
        Me.chbACCs.Enabled = False
        Me.chbACCs.Location = New System.Drawing.Point(8, 32)
        Me.chbACCs.Name = "chbACCs"
        Me.chbACCs.Size = New System.Drawing.Size(175, 17)
        Me.chbACCs.TabIndex = 1
        Me.chbACCs.Text = "Annual Compliance Certification"
        '
        'GBEnforcementDates
        '
        Me.GBEnforcementDates.Controls.Add(Me.chbLastModifiedDate)
        Me.GBEnforcementDates.Controls.Add(Me.chbFilterDates)
        Me.GBEnforcementDates.Controls.Add(Me.Label13)
        Me.GBEnforcementDates.Controls.Add(Me.DTPFilterEnd)
        Me.GBEnforcementDates.Controls.Add(Me.Label14)
        Me.GBEnforcementDates.Controls.Add(Me.DTPFilterStart)
        Me.GBEnforcementDates.Location = New System.Drawing.Point(441, 260)
        Me.GBEnforcementDates.Name = "GBEnforcementDates"
        Me.GBEnforcementDates.Size = New System.Drawing.Size(144, 147)
        Me.GBEnforcementDates.TabIndex = 5
        Me.GBEnforcementDates.TabStop = False
        Me.GBEnforcementDates.Text = "Date Bias"
        '
        'chbLastModifiedDate
        '
        Me.chbLastModifiedDate.AutoSize = True
        Me.chbLastModifiedDate.Location = New System.Drawing.Point(9, 120)
        Me.chbLastModifiedDate.Name = "chbLastModifiedDate"
        Me.chbLastModifiedDate.Size = New System.Drawing.Size(122, 17)
        Me.chbLastModifiedDate.TabIndex = 2
        Me.chbLastModifiedDate.Text = "Include last modified"
        Me.chbLastModifiedDate.UseVisualStyleBackColor = True
        '
        'chbFilterDates
        '
        Me.chbFilterDates.AutoSize = True
        Me.chbFilterDates.Location = New System.Drawing.Point(9, 19)
        Me.chbFilterDates.Name = "chbFilterDates"
        Me.chbFilterDates.Size = New System.Drawing.Size(96, 17)
        Me.chbFilterDates.TabIndex = 0
        Me.chbFilterDates.Text = "Use Date Filter"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 78)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(55, 13)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "End Date:"
        '
        'DTPFilterEnd
        '
        Me.DTPFilterEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFilterEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFilterEnd.Location = New System.Drawing.Point(9, 94)
        Me.DTPFilterEnd.Name = "DTPFilterEnd"
        Me.DTPFilterEnd.Size = New System.Drawing.Size(100, 20)
        Me.DTPFilterEnd.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 39)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(58, 13)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Start Date:"
        '
        'DTPFilterStart
        '
        Me.DTPFilterStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFilterStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFilterStart.Location = New System.Drawing.Point(9, 55)
        Me.DTPFilterStart.Name = "DTPFilterStart"
        Me.DTPFilterStart.Size = New System.Drawing.Size(100, 20)
        Me.DTPFilterStart.TabIndex = 1
        '
        'txtWorkCount
        '
        Me.txtWorkCount.Location = New System.Drawing.Point(152, 1)
        Me.txtWorkCount.Name = "txtWorkCount"
        Me.txtWorkCount.ReadOnly = True
        Me.txtWorkCount.Size = New System.Drawing.Size(48, 20)
        Me.txtWorkCount.TabIndex = 301
        '
        'btnRunFilter
        '
        Me.btnRunFilter.AutoSize = True
        Me.btnRunFilter.Location = New System.Drawing.Point(4, -1)
        Me.btnRunFilter.Name = "btnRunFilter"
        Me.btnRunFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnRunFilter.TabIndex = 0
        Me.btnRunFilter.Text = "Run Filter"
        Me.btnRunFilter.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chbCompletedWork)
        Me.GroupBox5.Controls.Add(Me.chbOpenWork)
        Me.GroupBox5.Controls.Add(Me.chbDeletedWork)
        Me.GroupBox5.Location = New System.Drawing.Point(2, 26)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(198, 68)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Open/Closed/Deleted"
        '
        'chbCompletedWork
        '
        Me.chbCompletedWork.Location = New System.Drawing.Point(9, 33)
        Me.chbCompletedWork.Name = "chbCompletedWork"
        Me.chbCompletedWork.Size = New System.Drawing.Size(112, 16)
        Me.chbCompletedWork.TabIndex = 1
        Me.chbCompletedWork.Text = "Completed Work"
        '
        'chbOpenWork
        '
        Me.chbOpenWork.Checked = True
        Me.chbOpenWork.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbOpenWork.Location = New System.Drawing.Point(9, 17)
        Me.chbOpenWork.Name = "chbOpenWork"
        Me.chbOpenWork.Size = New System.Drawing.Size(80, 16)
        Me.chbOpenWork.TabIndex = 0
        Me.chbOpenWork.Text = "Open Work"
        '
        'chbDeletedWork
        '
        Me.chbDeletedWork.Location = New System.Drawing.Point(9, 50)
        Me.chbDeletedWork.Name = "chbDeletedWork"
        Me.chbDeletedWork.Size = New System.Drawing.Size(96, 16)
        Me.chbDeletedWork.TabIndex = 2
        Me.chbDeletedWork.Text = "Deleted Work"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label15)
        Me.GroupBox7.Controls.Add(Me.txtFacilityNameFilter)
        Me.GroupBox7.Controls.Add(Me.Label12)
        Me.GroupBox7.Controls.Add(Me.txtFCENumberFilter)
        Me.GroupBox7.Controls.Add(Me.Label10)
        Me.GroupBox7.Controls.Add(Me.txtEnforcementNumberFilter)
        Me.GroupBox7.Controls.Add(Me.Label11)
        Me.GroupBox7.Controls.Add(Me.Label9)
        Me.GroupBox7.Controls.Add(Me.txtTrackingNumberFilter)
        Me.GroupBox7.Controls.Add(Me.txtAIRSNumberFilter)
        Me.GroupBox7.Location = New System.Drawing.Point(206, 260)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(229, 146)
        Me.GroupBox7.TabIndex = 4
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Misc. "
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 114)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 13)
        Me.Label15.TabIndex = 39
        Me.Label15.Text = "Facility Name"
        '
        'txtFacilityNameFilter
        '
        Me.txtFacilityNameFilter.Location = New System.Drawing.Point(119, 110)
        Me.txtFacilityNameFilter.MaxLength = 8
        Me.txtFacilityNameFilter.Name = "txtFacilityNameFilter"
        Me.txtFacilityNameFilter.Size = New System.Drawing.Size(100, 20)
        Me.txtFacilityNameFilter.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 13)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "FCE Number"
        '
        'txtFCENumberFilter
        '
        Me.txtFCENumberFilter.Location = New System.Drawing.Point(119, 84)
        Me.txtFCENumberFilter.MaxLength = 8
        Me.txtFCENumberFilter.Name = "txtFCENumberFilter"
        Me.txtFCENumberFilter.Size = New System.Drawing.Size(100, 20)
        Me.txtFCENumberFilter.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(107, 13)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "Enforcement Number"
        '
        'txtEnforcementNumberFilter
        '
        Me.txtEnforcementNumberFilter.Location = New System.Drawing.Point(119, 61)
        Me.txtEnforcementNumberFilter.MaxLength = 8
        Me.txtEnforcementNumberFilter.Name = "txtEnforcementNumberFilter"
        Me.txtEnforcementNumberFilter.Size = New System.Drawing.Size(100, 20)
        Me.txtEnforcementNumberFilter.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 41)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Tracking Number"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "AIRS Number:"
        '
        'txtTrackingNumberFilter
        '
        Me.txtTrackingNumberFilter.Location = New System.Drawing.Point(119, 37)
        Me.txtTrackingNumberFilter.MaxLength = 8
        Me.txtTrackingNumberFilter.Name = "txtTrackingNumberFilter"
        Me.txtTrackingNumberFilter.Size = New System.Drawing.Size(100, 20)
        Me.txtTrackingNumberFilter.TabIndex = 1
        '
        'txtAIRSNumberFilter
        '
        Me.txtAIRSNumberFilter.Location = New System.Drawing.Point(119, 14)
        Me.txtAIRSNumberFilter.MaxLength = 8
        Me.txtAIRSNumberFilter.Name = "txtAIRSNumberFilter"
        Me.txtAIRSNumberFilter.Size = New System.Drawing.Size(100, 20)
        Me.txtAIRSNumberFilter.TabIndex = 0
        '
        'GBEngineer
        '
        Me.GBEngineer.Controls.Add(Me.Label17)
        Me.GBEngineer.Controls.Add(Me.Label16)
        Me.GBEngineer.Controls.Add(Me.clbDistrictOffices)
        Me.GBEngineer.Controls.Add(Me.clbAirBranchUnits)
        Me.GBEngineer.Controls.Add(Me.Panel5)
        Me.GBEngineer.Controls.Add(Me.clbEngineer)
        Me.GBEngineer.Controls.Add(Me.chbEngineer)
        Me.GBEngineer.Location = New System.Drawing.Point(206, 1)
        Me.GBEngineer.Name = "GBEngineer"
        Me.GBEngineer.Size = New System.Drawing.Size(379, 253)
        Me.GBEngineer.TabIndex = 3
        Me.GBEngineer.TabStop = False
        Me.GBEngineer.Text = "Staff Search Criteria"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(191, 136)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(70, 13)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "District Office"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(191, 46)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(78, 13)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "Air Branch Unit"
        '
        'clbDistrictOffices
        '
        Me.clbDistrictOffices.CheckOnClick = True
        Me.clbDistrictOffices.Location = New System.Drawing.Point(191, 152)
        Me.clbDistrictOffices.Name = "clbDistrictOffices"
        Me.clbDistrictOffices.Size = New System.Drawing.Size(179, 94)
        Me.clbDistrictOffices.TabIndex = 6
        '
        'clbAirBranchUnits
        '
        Me.clbAirBranchUnits.CheckOnClick = True
        Me.clbAirBranchUnits.Location = New System.Drawing.Point(191, 62)
        Me.clbAirBranchUnits.Name = "clbAirBranchUnits"
        Me.clbAirBranchUnits.Size = New System.Drawing.Size(179, 64)
        Me.clbAirBranchUnits.TabIndex = 4
        '
        'Panel5
        '
        Me.Panel5.AutoSize = True
        Me.Panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel5.Controls.Add(Me.rdbUseUnits)
        Me.Panel5.Controls.Add(Me.rdbUseEngineer)
        Me.Panel5.Controls.Add(Me.rdbIgnoreEngineer)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(3, 16)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(373, 24)
        Me.Panel5.TabIndex = 0
        '
        'rdbUseUnits
        '
        Me.rdbUseUnits.AutoSize = True
        Me.rdbUseUnits.Location = New System.Drawing.Point(186, 4)
        Me.rdbUseUnits.Name = "rdbUseUnits"
        Me.rdbUseUnits.Size = New System.Drawing.Size(134, 17)
        Me.rdbUseUnits.TabIndex = 2
        Me.rdbUseUnits.Text = "Use Unit/District Office"
        Me.rdbUseUnits.UseVisualStyleBackColor = True
        '
        'rdbUseEngineer
        '
        Me.rdbUseEngineer.AutoSize = True
        Me.rdbUseEngineer.Checked = True
        Me.rdbUseEngineer.Location = New System.Drawing.Point(89, 4)
        Me.rdbUseEngineer.Name = "rdbUseEngineer"
        Me.rdbUseEngineer.Size = New System.Drawing.Size(91, 17)
        Me.rdbUseEngineer.TabIndex = 1
        Me.rdbUseEngineer.TabStop = True
        Me.rdbUseEngineer.Text = "Use Inspector"
        Me.rdbUseEngineer.UseVisualStyleBackColor = True
        '
        'rdbIgnoreEngineer
        '
        Me.rdbIgnoreEngineer.AutoSize = True
        Me.rdbIgnoreEngineer.Location = New System.Drawing.Point(3, 4)
        Me.rdbIgnoreEngineer.Name = "rdbIgnoreEngineer"
        Me.rdbIgnoreEngineer.Size = New System.Drawing.Size(80, 17)
        Me.rdbIgnoreEngineer.TabIndex = 0
        Me.rdbIgnoreEngineer.Text = "Entire State"
        Me.rdbIgnoreEngineer.UseVisualStyleBackColor = True
        '
        'clbEngineer
        '
        Me.clbEngineer.CheckOnClick = True
        Me.clbEngineer.Enabled = False
        Me.clbEngineer.Location = New System.Drawing.Point(6, 62)
        Me.clbEngineer.Name = "clbEngineer"
        Me.clbEngineer.Size = New System.Drawing.Size(179, 184)
        Me.clbEngineer.TabIndex = 2
        '
        'chbEngineer
        '
        Me.chbEngineer.AutoSize = True
        Me.chbEngineer.Checked = True
        Me.chbEngineer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbEngineer.Location = New System.Drawing.Point(9, 45)
        Me.chbEngineer.Name = "chbEngineer"
        Me.chbEngineer.Size = New System.Drawing.Size(85, 17)
        Me.chbEngineer.TabIndex = 1
        Me.chbEngineer.Text = "Current Staff"
        '
        'TCComplianceLog
        '
        Me.TCComplianceLog.Controls.Add(Me.TPSelectWork)
        Me.TCComplianceLog.Controls.Add(Me.TPStartNewWork)
        Me.TCComplianceLog.Dock = System.Windows.Forms.DockStyle.Right
        Me.TCComplianceLog.Location = New System.Drawing.Point(596, 16)
        Me.TCComplianceLog.Name = "TCComplianceLog"
        Me.TCComplianceLog.SelectedIndex = 0
        Me.TCComplianceLog.Size = New System.Drawing.Size(232, 411)
        Me.TCComplianceLog.TabIndex = 1
        '
        'TPSelectWork
        '
        Me.TPSelectWork.Controls.Add(Me.btnOpenSummary)
        Me.TPSelectWork.Controls.Add(Me.btnUndeleteWork)
        Me.TPSelectWork.Controls.Add(Me.btnDeleteWork)
        Me.TPSelectWork.Controls.Add(Me.btnSelectWork)
        Me.TPSelectWork.Controls.Add(Me.lblWorkType)
        Me.TPSelectWork.Controls.Add(Me.txtWorkNumber)
        Me.TPSelectWork.Controls.Add(Me.Label3)
        Me.TPSelectWork.Controls.Add(Me.txtFacilityCity)
        Me.TPSelectWork.Controls.Add(Me.txtFacilityName)
        Me.TPSelectWork.Controls.Add(Me.Label6)
        Me.TPSelectWork.Controls.Add(Me.Label2)
        Me.TPSelectWork.Controls.Add(Me.txtTestType)
        Me.TPSelectWork.Controls.Add(Me.txtAIRSNumber)
        Me.TPSelectWork.Controls.Add(Me.Label1)
        Me.TPSelectWork.Location = New System.Drawing.Point(4, 22)
        Me.TPSelectWork.Name = "TPSelectWork"
        Me.TPSelectWork.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSelectWork.Size = New System.Drawing.Size(224, 385)
        Me.TPSelectWork.TabIndex = 0
        Me.TPSelectWork.Text = "Select Work Entry "
        Me.TPSelectWork.UseVisualStyleBackColor = True
        '
        'btnOpenSummary
        '
        Me.btnOpenSummary.AutoSize = True
        Me.btnOpenSummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenSummary.Location = New System.Drawing.Point(122, 61)
        Me.btnOpenSummary.Name = "btnOpenSummary"
        Me.btnOpenSummary.Size = New System.Drawing.Size(95, 23)
        Me.btnOpenSummary.TabIndex = 3
        Me.btnOpenSummary.Text = "Facility Summary"
        Me.btnOpenSummary.UseVisualStyleBackColor = True
        '
        'btnUndeleteWork
        '
        Me.btnUndeleteWork.AutoSize = True
        Me.btnUndeleteWork.Location = New System.Drawing.Point(128, 219)
        Me.btnUndeleteWork.Name = "btnUndeleteWork"
        Me.btnUndeleteWork.Size = New System.Drawing.Size(89, 23)
        Me.btnUndeleteWork.TabIndex = 9
        Me.btnUndeleteWork.Text = "Undelete Work"
        Me.btnUndeleteWork.UseVisualStyleBackColor = True
        '
        'btnDeleteWork
        '
        Me.btnDeleteWork.AutoSize = True
        Me.btnDeleteWork.Location = New System.Drawing.Point(9, 219)
        Me.btnDeleteWork.Name = "btnDeleteWork"
        Me.btnDeleteWork.Size = New System.Drawing.Size(77, 23)
        Me.btnDeleteWork.TabIndex = 8
        Me.btnDeleteWork.Text = "Delete Work"
        Me.btnDeleteWork.UseVisualStyleBackColor = True
        '
        'btnSelectWork
        '
        Me.btnSelectWork.AutoSize = True
        Me.btnSelectWork.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectWork.Location = New System.Drawing.Point(122, 22)
        Me.btnSelectWork.Name = "btnSelectWork"
        Me.btnSelectWork.Size = New System.Drawing.Size(72, 23)
        Me.btnSelectWork.TabIndex = 1
        Me.btnSelectWork.Text = "Open Work"
        Me.btnSelectWork.UseVisualStyleBackColor = True
        '
        'lblWorkType
        '
        Me.lblWorkType.AutoSize = True
        Me.lblWorkType.Location = New System.Drawing.Point(6, 8)
        Me.lblWorkType.Name = "lblWorkType"
        Me.lblWorkType.Size = New System.Drawing.Size(110, 13)
        Me.lblWorkType.TabIndex = 234
        Me.lblWorkType.Text = "Unique Work Number"
        '
        'txtWorkNumber
        '
        Me.txtWorkNumber.Location = New System.Drawing.Point(9, 24)
        Me.txtWorkNumber.Name = "txtWorkNumber"
        Me.txtWorkNumber.ReadOnly = True
        Me.txtWorkNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtWorkNumber.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 236
        Me.Label3.Text = "Facility Name"
        '
        'txtFacilityCity
        '
        Me.txtFacilityCity.Location = New System.Drawing.Point(9, 141)
        Me.txtFacilityCity.Name = "txtFacilityCity"
        Me.txtFacilityCity.ReadOnly = True
        Me.txtFacilityCity.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityCity.TabIndex = 6
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(9, 102)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityName.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 242
        Me.Label6.Text = "Facility City"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 238
        Me.Label2.Text = "AIRS Number"
        '
        'txtTestType
        '
        Me.txtTestType.Location = New System.Drawing.Point(9, 180)
        Me.txtTestType.Name = "txtTestType"
        Me.txtTestType.ReadOnly = True
        Me.txtTestType.Size = New System.Drawing.Size(208, 20)
        Me.txtTestType.TabIndex = 5
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(9, 63)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtAIRSNumber.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 164)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 240
        Me.Label1.Text = "Work Type"
        '
        'TPStartNewWork
        '
        Me.TPStartNewWork.Controls.Add(Me.pnlOtherEvents)
        Me.TPStartNewWork.Controls.Add(Me.Panel4)
        Me.TPStartNewWork.Controls.Add(Me.btnAddNewEntry)
        Me.TPStartNewWork.Controls.Add(Me.Label5)
        Me.TPStartNewWork.Controls.Add(Me.txtFacilityInformation)
        Me.TPStartNewWork.Controls.Add(Me.Label4)
        Me.TPStartNewWork.Controls.Add(Me.txtNewAIRSNumber)
        Me.TPStartNewWork.Location = New System.Drawing.Point(4, 22)
        Me.TPStartNewWork.Name = "TPStartNewWork"
        Me.TPStartNewWork.Padding = New System.Windows.Forms.Padding(3)
        Me.TPStartNewWork.Size = New System.Drawing.Size(224, 385)
        Me.TPStartNewWork.TabIndex = 1
        Me.TPStartNewWork.Text = "Start New Work"
        Me.TPStartNewWork.UseVisualStyleBackColor = True
        '
        'pnlOtherEvents
        '
        Me.pnlOtherEvents.Controls.Add(Me.txtTrackingNumber)
        Me.pnlOtherEvents.Controls.Add(Me.Label8)
        Me.pnlOtherEvents.Controls.Add(Me.lblOtherNumber)
        Me.pnlOtherEvents.Controls.Add(Me.cboEvent)
        Me.pnlOtherEvents.Controls.Add(Me.LabelEventDescription)
        Me.pnlOtherEvents.Controls.Add(Me.DTPDateReceived)
        Me.pnlOtherEvents.Controls.Add(Me.lblDateField)
        Me.pnlOtherEvents.Location = New System.Drawing.Point(0, 216)
        Me.pnlOtherEvents.Name = "pnlOtherEvents"
        Me.pnlOtherEvents.Size = New System.Drawing.Size(217, 137)
        Me.pnlOtherEvents.TabIndex = 2
        Me.pnlOtherEvents.Visible = False
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(9, 25)
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.ReadOnly = True
        Me.txtTrackingNumber.Size = New System.Drawing.Size(80, 20)
        Me.txtTrackingNumber.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 13)
        Me.Label8.TabIndex = 275
        Me.Label8.Text = "Compliance Event"
        '
        'lblOtherNumber
        '
        Me.lblOtherNumber.AutoSize = True
        Me.lblOtherNumber.Location = New System.Drawing.Point(6, 9)
        Me.lblOtherNumber.Name = "lblOtherNumber"
        Me.lblOtherNumber.Size = New System.Drawing.Size(92, 13)
        Me.lblOtherNumber.TabIndex = 279
        Me.lblOtherNumber.Text = "Tracking Number:"
        '
        'cboEvent
        '
        Me.cboEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEvent.Location = New System.Drawing.Point(9, 78)
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.Size = New System.Drawing.Size(198, 21)
        Me.cboEvent.TabIndex = 2
        '
        'LabelEventDescription
        '
        Me.LabelEventDescription.Location = New System.Drawing.Point(10, 102)
        Me.LabelEventDescription.Name = "LabelEventDescription"
        Me.LabelEventDescription.Size = New System.Drawing.Size(204, 29)
        Me.LabelEventDescription.TabIndex = 3
        Me.LabelEventDescription.Visible = False
        '
        'DTPDateReceived
        '
        Me.DTPDateReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateReceived.Location = New System.Drawing.Point(107, 25)
        Me.DTPDateReceived.Name = "DTPDateReceived"
        Me.DTPDateReceived.Size = New System.Drawing.Size(100, 20)
        Me.DTPDateReceived.TabIndex = 1
        Me.DTPDateReceived.Value = New Date(2007, 1, 23, 0, 0, 0, 0)
        '
        'lblDateField
        '
        Me.lblDateField.AutoSize = True
        Me.lblDateField.Location = New System.Drawing.Point(104, 8)
        Me.lblDateField.Name = "lblDateField"
        Me.lblDateField.Size = New System.Drawing.Size(103, 13)
        Me.lblDateField.TabIndex = 277
        Me.lblDateField.Text = "Received by GEPD:"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rdbPerformanceTest)
        Me.Panel4.Controls.Add(Me.rdbOther)
        Me.Panel4.Controls.Add(Me.rdbFCE)
        Me.Panel4.Controls.Add(Me.rdbEnforcementAction)
        Me.Panel4.Location = New System.Drawing.Point(9, 132)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(208, 84)
        Me.Panel4.TabIndex = 1
        '
        'rdbPerformanceTest
        '
        Me.rdbPerformanceTest.AutoSize = True
        Me.rdbPerformanceTest.Location = New System.Drawing.Point(7, 43)
        Me.rdbPerformanceTest.Name = "rdbPerformanceTest"
        Me.rdbPerformanceTest.Size = New System.Drawing.Size(109, 17)
        Me.rdbPerformanceTest.TabIndex = 2
        Me.rdbPerformanceTest.TabStop = True
        Me.rdbPerformanceTest.Text = "Performance Test"
        Me.rdbPerformanceTest.UseVisualStyleBackColor = True
        '
        'rdbOther
        '
        Me.rdbOther.AutoSize = True
        Me.rdbOther.Location = New System.Drawing.Point(7, 61)
        Me.rdbOther.Name = "rdbOther"
        Me.rdbOther.Size = New System.Drawing.Size(51, 17)
        Me.rdbOther.TabIndex = 3
        Me.rdbOther.TabStop = True
        Me.rdbOther.Text = "Other"
        Me.rdbOther.UseVisualStyleBackColor = True
        '
        'rdbFCE
        '
        Me.rdbFCE.AutoSize = True
        Me.rdbFCE.Location = New System.Drawing.Point(7, 25)
        Me.rdbFCE.Name = "rdbFCE"
        Me.rdbFCE.Size = New System.Drawing.Size(152, 17)
        Me.rdbFCE.TabIndex = 1
        Me.rdbFCE.TabStop = True
        Me.rdbFCE.Text = "Full Compliance Evaluation"
        Me.rdbFCE.UseVisualStyleBackColor = True
        '
        'rdbEnforcementAction
        '
        Me.rdbEnforcementAction.AutoSize = True
        Me.rdbEnforcementAction.Location = New System.Drawing.Point(7, 8)
        Me.rdbEnforcementAction.Name = "rdbEnforcementAction"
        Me.rdbEnforcementAction.Size = New System.Drawing.Size(118, 17)
        Me.rdbEnforcementAction.TabIndex = 0
        Me.rdbEnforcementAction.TabStop = True
        Me.rdbEnforcementAction.Text = "Enforcement Action"
        Me.rdbEnforcementAction.UseVisualStyleBackColor = True
        '
        'btnAddNewEntry
        '
        Me.btnAddNewEntry.AutoSize = True
        Me.btnAddNewEntry.Location = New System.Drawing.Point(9, 359)
        Me.btnAddNewEntry.Name = "btnAddNewEntry"
        Me.btnAddNewEntry.Size = New System.Drawing.Size(88, 23)
        Me.btnAddNewEntry.TabIndex = 3
        Me.btnAddNewEntry.Text = "&Add New Entry"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 242
        Me.Label5.Text = "Facility Information"
        '
        'txtFacilityInformation
        '
        Me.txtFacilityInformation.Location = New System.Drawing.Point(9, 62)
        Me.txtFacilityInformation.Multiline = True
        Me.txtFacilityInformation.Name = "txtFacilityInformation"
        Me.txtFacilityInformation.ReadOnly = True
        Me.txtFacilityInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityInformation.Size = New System.Drawing.Size(208, 64)
        Me.txtFacilityInformation.TabIndex = 100
        Me.txtFacilityInformation.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 240
        Me.Label4.Text = "AIRS Number"
        '
        'txtNewAIRSNumber
        '
        Me.txtNewAIRSNumber.Location = New System.Drawing.Point(9, 23)
        Me.txtNewAIRSNumber.MaxLength = 8
        Me.txtNewAIRSNumber.Name = "txtNewAIRSNumber"
        Me.txtNewAIRSNumber.Size = New System.Drawing.Size(208, 20)
        Me.txtNewAIRSNumber.TabIndex = 0
        '
        'dgvWork
        '
        Me.dgvWork.AllowUserToAddRows = False
        Me.dgvWork.AllowUserToDeleteRows = False
        Me.dgvWork.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvWork.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvWork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvWork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvWork.Location = New System.Drawing.Point(0, 454)
        Me.dgvWork.MultiSelect = False
        Me.dgvWork.Name = "dgvWork"
        Me.dgvWork.ReadOnly = True
        Me.dgvWork.RowHeadersVisible = False
        Me.dgvWork.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvWork.Size = New System.Drawing.Size(831, 234)
        Me.dgvWork.TabIndex = 5
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.Blue
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 454)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(831, 5)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FacilitySearchToolStripMenuItem, Me.ClearFormToolStripMenuItem, Me.ExportToExcelToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(831, 24)
        Me.MenuStrip1.TabIndex = 8
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FacilitySearchToolStripMenuItem
        '
        Me.FacilitySearchToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FacilitySearchToolStripMenuItem.Image = Global.Iaip.My.Resources.Resources.FindIcon
        Me.FacilitySearchToolStripMenuItem.Name = "FacilitySearchToolStripMenuItem"
        Me.FacilitySearchToolStripMenuItem.Size = New System.Drawing.Size(28, 20)
        Me.FacilitySearchToolStripMenuItem.Text = "&Facility Search"
        Me.FacilitySearchToolStripMenuItem.ToolTipText = "Facility Search"
        '
        'ClearFormToolStripMenuItem
        '
        Me.ClearFormToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ClearFormToolStripMenuItem.Image = Global.Iaip.My.Resources.Resources.EraseIcon
        Me.ClearFormToolStripMenuItem.Name = "ClearFormToolStripMenuItem"
        Me.ClearFormToolStripMenuItem.Size = New System.Drawing.Size(28, 20)
        Me.ClearFormToolStripMenuItem.Text = "&Clear Form"
        '
        'ExportToExcelToolStripMenuItem
        '
        Me.ExportToExcelToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ExportToExcelToolStripMenuItem.Image = Global.Iaip.My.Resources.Resources.SpreadsheetIcon
        Me.ExportToExcelToolStripMenuItem.Name = "ExportToExcelToolStripMenuItem"
        Me.ExportToExcelToolStripMenuItem.Size = New System.Drawing.Size(28, 20)
        Me.ExportToExcelToolStripMenuItem.Text = "&Export to Excel"
        '
        'SSCPComplianceLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(831, 688)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.dgvWork)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "SSCPComplianceLog"
        Me.Text = "Compliance Log"
        Me.GroupBox1.ResumeLayout(False)
        Me.pnlFilterPanel.ResumeLayout(False)
        Me.pnlFilterPanel.PerformLayout()
        Me.GBWorkTypes.ResumeLayout(False)
        Me.GBWorkTypes.PerformLayout()
        Me.GBNotifications.ResumeLayout(False)
        Me.GBEnforcementDates.ResumeLayout(False)
        Me.GBEnforcementDates.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GBEngineer.ResumeLayout(False)
        Me.GBEngineer.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.TCComplianceLog.ResumeLayout(False)
        Me.TPSelectWork.ResumeLayout(False)
        Me.TPSelectWork.PerformLayout()
        Me.TPStartNewWork.ResumeLayout(False)
        Me.TPStartNewWork.PerformLayout()
        Me.pnlOtherEvents.ResumeLayout(False)
        Me.pnlOtherEvents.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgvWork, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvWork As System.Windows.Forms.DataGridView
    Friend WithEvents txtFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTestType As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblWorkType As System.Windows.Forms.Label
    Friend WithEvents txtWorkNumber As System.Windows.Forms.TextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents btnSelectWork As System.Windows.Forms.Button
    Friend WithEvents TCComplianceLog As System.Windows.Forms.TabControl
    Friend WithEvents TPSelectWork As System.Windows.Forms.TabPage
    Friend WithEvents TPStartNewWork As System.Windows.Forms.TabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityInformation As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNewAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboEvent As System.Windows.Forms.ComboBox
    Friend WithEvents LabelEventDescription As System.Windows.Forms.Label
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblOtherNumber As System.Windows.Forms.Label
    Friend WithEvents btnAddNewEntry As System.Windows.Forms.Button
    Friend WithEvents DTPDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDateField As System.Windows.Forms.Label
    Friend WithEvents pnlOtherEvents As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbOther As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFCE As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEnforcementAction As System.Windows.Forms.RadioButton
    Friend WithEvents btnDeleteWork As System.Windows.Forms.Button
    Friend WithEvents btnUndeleteWork As System.Windows.Forms.Button
    Friend WithEvents btnOpenSummary As System.Windows.Forms.Button
    Friend WithEvents rdbPerformanceTest As System.Windows.Forms.RadioButton
    Friend WithEvents pnlFilterPanel As System.Windows.Forms.Panel
    Friend WithEvents GBWorkTypes As System.Windows.Forms.GroupBox
    Friend WithEvents chbRMPInspections As System.Windows.Forms.CheckBox
    Friend WithEvents GBNotifications As System.Windows.Forms.GroupBox
    Friend WithEvents clbNotifications As System.Windows.Forms.CheckedListBox
    Friend WithEvents chbFCE As System.Windows.Forms.CheckBox
    Friend WithEvents chbEnforcement As System.Windows.Forms.CheckBox
    Friend WithEvents chbAllWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbNotifications As System.Windows.Forms.CheckBox
    Friend WithEvents chbPerformanceTests As System.Windows.Forms.CheckBox
    Friend WithEvents chbReports As System.Windows.Forms.CheckBox
    Friend WithEvents chbInspections As System.Windows.Forms.CheckBox
    Friend WithEvents chbACCs As System.Windows.Forms.CheckBox
    Friend WithEvents GBEnforcementDates As System.Windows.Forms.GroupBox
    Friend WithEvents chbLastModifiedDate As System.Windows.Forms.CheckBox
    Friend WithEvents chbFilterDates As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DTPFilterEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DTPFilterStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtWorkCount As System.Windows.Forms.TextBox
    Friend WithEvents btnRunFilter As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chbCompletedWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbOpenWork As System.Windows.Forms.CheckBox
    Friend WithEvents chbDeletedWork As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityNameFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtFCENumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtEnforcementNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTrackingNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumberFilter As System.Windows.Forms.TextBox
    Friend WithEvents GBEngineer As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents clbDistrictOffices As System.Windows.Forms.CheckedListBox
    Friend WithEvents clbAirBranchUnits As System.Windows.Forms.CheckedListBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdbUseUnits As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUseEngineer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbIgnoreEngineer As System.Windows.Forms.RadioButton
    Friend WithEvents clbEngineer As System.Windows.Forms.CheckedListBox
    Friend WithEvents chbEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FacilitySearchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearFormToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportToExcelToolStripMenuItem As ToolStripMenuItem
End Class
