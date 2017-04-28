<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSCPEnforcementChecklist
    Inherits BaseForm


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

    Friend WithEvents LinkEventButton As System.Windows.Forms.Button
    Friend WithEvents TrackingNumberDisplay As System.Windows.Forms.Label
    Friend WithEvents chbACCs As System.Windows.Forms.CheckBox
    Friend WithEvents chbReports As System.Windows.Forms.CheckBox
    Friend WithEvents chbPerformanceTests As System.Windows.Forms.CheckBox
    Friend WithEvents chbInspections As System.Windows.Forms.CheckBox
    Friend WithEvents txtWorkCount As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DTPEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chbFilterDates As System.Windows.Forms.CheckBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnRunFilter As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents OpenFilterOptions As System.Windows.Forms.CheckBox
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents FacilityInfo As System.Windows.Forms.Label
    Friend WithEvents EnforcementInfo As System.Windows.Forms.Label
    Friend WithEvents FilterOptionsPanel As System.Windows.Forms.Panel
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.OpenFilterOptions = New System.Windows.Forms.CheckBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.LinkEventButton = New System.Windows.Forms.Button()
        Me.TrackingNumberDisplay = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EnforcementInfo = New System.Windows.Forms.Label()
        Me.FacilityInfo = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtWorkCount = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.DTPEndDate = New System.Windows.Forms.DateTimePicker()
        Me.DTPStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chbFilterDates = New System.Windows.Forms.CheckBox()
        Me.btnRunFilter = New System.Windows.Forms.Button()
        Me.chbInspections = New System.Windows.Forms.CheckBox()
        Me.chbPerformanceTests = New System.Windows.Forms.CheckBox()
        Me.chbReports = New System.Windows.Forms.CheckBox()
        Me.chbACCs = New System.Windows.Forms.CheckBox()
        Me.FilterOptionsPanel = New System.Windows.Forms.Panel()
        Me.dgvComplianceEvents = New System.Windows.Forms.DataGridView()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.FilterOptionsPanel.SuspendLayout()
        CType(Me.dgvComplianceEvents, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TrackingNumberDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrackingNumberDisplay.Location = New System.Drawing.Point(191, 65)
        Me.TrackingNumberDisplay.Name = "TrackingNumberDisplay"
        Me.TrackingNumberDisplay.Size = New System.Drawing.Size(17, 17)
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
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(266, 161)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 13)
        Me.Label8.TabIndex = 289
        Me.Label8.Text = "Record Count"
        '
        'txtWorkCount
        '
        Me.txtWorkCount.Location = New System.Drawing.Point(266, 177)
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
        Me.Panel6.Location = New System.Drawing.Point(17, 122)
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
        Me.chbFilterDates.Location = New System.Drawing.Point(12, 113)
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
        'chbInspections
        '
        Me.chbInspections.Location = New System.Drawing.Point(12, 34)
        Me.chbInspections.Name = "chbInspections"
        Me.chbInspections.Size = New System.Drawing.Size(104, 24)
        Me.chbInspections.TabIndex = 2
        Me.chbInspections.Text = "Inspections"
        '
        'chbPerformanceTests
        '
        Me.chbPerformanceTests.Location = New System.Drawing.Point(12, 54)
        Me.chbPerformanceTests.Name = "chbPerformanceTests"
        Me.chbPerformanceTests.Size = New System.Drawing.Size(136, 24)
        Me.chbPerformanceTests.TabIndex = 3
        Me.chbPerformanceTests.Text = "Performance Tests"
        '
        'chbReports
        '
        Me.chbReports.Location = New System.Drawing.Point(12, 74)
        Me.chbReports.Name = "chbReports"
        Me.chbReports.Size = New System.Drawing.Size(104, 24)
        Me.chbReports.TabIndex = 4
        Me.chbReports.Text = "Reports"
        '
        'chbACCs
        '
        Me.chbACCs.Location = New System.Drawing.Point(12, 14)
        Me.chbACCs.Name = "chbACCs"
        Me.chbACCs.Size = New System.Drawing.Size(208, 24)
        Me.chbACCs.TabIndex = 1
        Me.chbACCs.Text = "Annual Compliance Certifications"
        '
        'FilterOptionsPanel
        '
        Me.FilterOptionsPanel.Controls.Add(Me.chbInspections)
        Me.FilterOptionsPanel.Controls.Add(Me.chbFilterDates)
        Me.FilterOptionsPanel.Controls.Add(Me.chbPerformanceTests)
        Me.FilterOptionsPanel.Controls.Add(Me.txtWorkCount)
        Me.FilterOptionsPanel.Controls.Add(Me.chbReports)
        Me.FilterOptionsPanel.Controls.Add(Me.Label8)
        Me.FilterOptionsPanel.Controls.Add(Me.chbACCs)
        Me.FilterOptionsPanel.Controls.Add(Me.Panel6)
        Me.FilterOptionsPanel.Controls.Add(Me.btnRunFilter)
        Me.FilterOptionsPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterOptionsPanel.Location = New System.Drawing.Point(0, 138)
        Me.FilterOptionsPanel.Name = "FilterOptionsPanel"
        Me.FilterOptionsPanel.Size = New System.Drawing.Size(367, 217)
        Me.FilterOptionsPanel.TabIndex = 1
        '
        'dgvComplianceEvents
        '
        Me.dgvComplianceEvents.AllowUserToAddRows = False
        Me.dgvComplianceEvents.AllowUserToDeleteRows = False
        Me.dgvComplianceEvents.AllowUserToOrderColumns = True
        Me.dgvComplianceEvents.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvComplianceEvents.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvComplianceEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvComplianceEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvComplianceEvents.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvComplianceEvents.Location = New System.Drawing.Point(0, 355)
        Me.dgvComplianceEvents.MultiSelect = False
        Me.dgvComplianceEvents.Name = "dgvComplianceEvents"
        Me.dgvComplianceEvents.RowHeadersVisible = False
        Me.dgvComplianceEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvComplianceEvents.Size = New System.Drawing.Size(367, 147)
        Me.dgvComplianceEvents.TabIndex = 3
        '
        'SSCPEnforcementChecklist
        '
        Me.AcceptButton = Me.LinkEventButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(367, 502)
        Me.Controls.Add(Me.dgvComplianceEvents)
        Me.Controls.Add(Me.FilterOptionsPanel)
        Me.Controls.Add(Me.Panel4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(383, 510)
        Me.Name = "SSCPEnforcementChecklist"
        Me.Text = "Enforcement Linking tool"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.FilterOptionsPanel.ResumeLayout(False)
        Me.FilterOptionsPanel.PerformLayout()
        CType(Me.dgvComplianceEvents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvComplianceEvents As DataGridView
End Class
