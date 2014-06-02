<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPEditHeaderData
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.FacilityDescription = New System.Windows.Forms.TextBox
        Me.FacilityDescriptionLabel = New System.Windows.Forms.Label
        Me.FacilityHistoryDisplay = New System.Windows.Forms.DataGridView
        Me.Comments = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.PermitRevocationDateLabel = New System.Windows.Forms.Label
        Me.StartupDateLabel = New System.Windows.Forms.Label
        Me.StartUpDate = New System.Windows.Forms.DateTimePicker
        Me.ShutdownDate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Classification = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.OperationalStatus = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.SicCode = New System.Windows.Forms.TextBox
        Me.AirProgramCodes = New System.Windows.Forms.GroupBox
        Me.ApcRmp = New System.Windows.Forms.CheckBox
        Me.HapMajor = New System.Windows.Forms.CheckBox
        Me.NsrMajor = New System.Windows.Forms.CheckBox
        Me.ApcAcid = New System.Windows.Forms.CheckBox
        Me.ApcFesop = New System.Windows.Forms.CheckBox
        Me.ApcTitleV = New System.Windows.Forms.CheckBox
        Me.ApcMact = New System.Windows.Forms.CheckBox
        Me.ApcNativeAmerican = New System.Windows.Forms.CheckBox
        Me.ApcNsps = New System.Windows.Forms.CheckBox
        Me.ApcNeshap = New System.Windows.Forms.CheckBox
        Me.ApcNsr = New System.Windows.Forms.CheckBox
        Me.ApcPsd = New System.Windows.Forms.CheckBox
        Me.ApcCfc = New System.Windows.Forms.CheckBox
        Me.ApcNonfederalSip = New System.Windows.Forms.CheckBox
        Me.ApcFederalSip = New System.Windows.Forms.CheckBox
        Me.ApcSip = New System.Windows.Forms.CheckBox
        Me.RmpLabel = New System.Windows.Forms.Label
        Me.RmpId = New System.Windows.Forms.MaskedTextBox
        Me.ModifiedDescDisplay = New System.Windows.Forms.Label
        Me.FacilityNameDisplay = New System.Windows.Forms.Label
        Me.AirsNumberDisplay = New System.Windows.Forms.Label
        Me.NonattainmentStatuses = New System.Windows.Forms.GroupBox
        Me.PmFine = New System.Windows.Forms.ComboBox
        Me.EightHourOzone = New System.Windows.Forms.ComboBox
        Me.OneHourOzone = New System.Windows.Forms.ComboBox
        Me.Label97 = New System.Windows.Forms.Label
        Me.Label96 = New System.Windows.Forms.Label
        Me.Label94 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.NaicsCode = New System.Windows.Forms.TextBox
        Me.SaveChangesButton = New System.Windows.Forms.Button
        Me.CancelEditButton = New System.Windows.Forms.Button
        Me.EditableButton = New System.Windows.Forms.CheckBox
        CType(Me.FacilityHistoryDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AirProgramCodes.SuspendLayout()
        Me.NonattainmentStatuses.SuspendLayout()
        Me.SuspendLayout()
        '
        'FacilityDescription
        '
        Me.FacilityDescription.AcceptsReturn = True
        Me.FacilityDescription.BackColor = System.Drawing.Color.White
        Me.FacilityDescription.Location = New System.Drawing.Point(116, 259)
        Me.FacilityDescription.MaxLength = 4000
        Me.FacilityDescription.Multiline = True
        Me.FacilityDescription.Name = "FacilityDescription"
        Me.FacilityDescription.Size = New System.Drawing.Size(333, 20)
        Me.FacilityDescription.TabIndex = 8
        '
        'FacilityDescriptionLabel
        '
        Me.FacilityDescriptionLabel.AutoSize = True
        Me.FacilityDescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityDescriptionLabel.Location = New System.Drawing.Point(12, 262)
        Me.FacilityDescriptionLabel.Name = "FacilityDescriptionLabel"
        Me.FacilityDescriptionLabel.Size = New System.Drawing.Size(98, 13)
        Me.FacilityDescriptionLabel.TabIndex = 376
        Me.FacilityDescriptionLabel.Text = "Facility Description:"
        Me.FacilityDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'FacilityHistoryDisplay
        '
        Me.FacilityHistoryDisplay.AllowUserToAddRows = False
        Me.FacilityHistoryDisplay.AllowUserToDeleteRows = False
        Me.FacilityHistoryDisplay.AllowUserToOrderColumns = True
        Me.FacilityHistoryDisplay.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FacilityHistoryDisplay.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.FacilityHistoryDisplay.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacilityHistoryDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FacilityHistoryDisplay.GridColor = System.Drawing.SystemColors.ControlLight
        Me.FacilityHistoryDisplay.Location = New System.Drawing.Point(0, 372)
        Me.FacilityHistoryDisplay.MultiSelect = False
        Me.FacilityHistoryDisplay.Name = "FacilityHistoryDisplay"
        Me.FacilityHistoryDisplay.ReadOnly = True
        Me.FacilityHistoryDisplay.RowHeadersVisible = False
        Me.FacilityHistoryDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FacilityHistoryDisplay.Size = New System.Drawing.Size(612, 188)
        Me.FacilityHistoryDisplay.TabIndex = 14
        '
        'Comments
        '
        Me.Comments.AcceptsReturn = True
        Me.Comments.BackColor = System.Drawing.Color.White
        Me.Comments.Location = New System.Drawing.Point(116, 285)
        Me.Comments.MaxLength = 4000
        Me.Comments.Multiline = True
        Me.Comments.Name = "Comments"
        Me.Comments.Size = New System.Drawing.Size(478, 40)
        Me.Comments.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 288)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 372
        Me.Label6.Text = "Comments:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'PermitRevocationDateLabel
        '
        Me.PermitRevocationDateLabel.AutoSize = True
        Me.PermitRevocationDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PermitRevocationDateLabel.Location = New System.Drawing.Point(217, 86)
        Me.PermitRevocationDateLabel.Name = "PermitRevocationDateLabel"
        Me.PermitRevocationDateLabel.Size = New System.Drawing.Size(97, 13)
        Me.PermitRevocationDateLabel.TabIndex = 4
        Me.PermitRevocationDateLabel.Text = "Permit Revocation:"
        '
        'StartupDateLabel
        '
        Me.StartupDateLabel.AutoSize = True
        Me.StartupDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartupDateLabel.Location = New System.Drawing.Point(12, 86)
        Me.StartupDateLabel.Name = "StartupDateLabel"
        Me.StartupDateLabel.Size = New System.Drawing.Size(44, 13)
        Me.StartupDateLabel.TabIndex = 370
        Me.StartupDateLabel.Text = "Startup:"
        Me.StartupDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'StartUpDate
        '
        Me.StartUpDate.CustomFormat = "dd-MMM-yyyy"
        Me.StartUpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.StartUpDate.Location = New System.Drawing.Point(89, 83)
        Me.StartUpDate.Name = "StartUpDate"
        Me.StartUpDate.ShowCheckBox = True
        Me.StartUpDate.Size = New System.Drawing.Size(121, 20)
        Me.StartUpDate.TabIndex = 3
        Me.StartUpDate.Value = New Date(2006, 11, 3, 0, 0, 0, 0)
        '
        'ShutdownDate
        '
        Me.ShutdownDate.CustomFormat = "dd-MMM-yyyy"
        Me.ShutdownDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ShutdownDate.Location = New System.Drawing.Point(320, 83)
        Me.ShutdownDate.Name = "ShutdownDate"
        Me.ShutdownDate.ShowCheckBox = True
        Me.ShutdownDate.Size = New System.Drawing.Size(121, 20)
        Me.ShutdownDate.TabIndex = 4
        Me.ShutdownDate.Value = New Date(2006, 11, 3, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 356
        Me.Label3.Text = "Classification:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Classification
        '
        Me.Classification.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Classification.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Classification.BackColor = System.Drawing.Color.White
        Me.Classification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Classification.Location = New System.Drawing.Point(89, 57)
        Me.Classification.Name = "Classification"
        Me.Classification.Size = New System.Drawing.Size(121, 21)
        Me.Classification.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(225, 59)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 13)
        Me.Label10.TabIndex = 359
        Me.Label10.Text = "Operating Status:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'OperationalStatus
        '
        Me.OperationalStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.OperationalStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.OperationalStatus.BackColor = System.Drawing.Color.White
        Me.OperationalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OperationalStatus.Location = New System.Drawing.Point(320, 56)
        Me.OperationalStatus.Name = "OperationalStatus"
        Me.OperationalStatus.Size = New System.Drawing.Size(121, 21)
        Me.OperationalStatus.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(462, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 357
        Me.Label8.Text = "SIC Code:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'SicCode
        '
        Me.SicCode.BackColor = System.Drawing.Color.White
        Me.SicCode.Location = New System.Drawing.Point(523, 56)
        Me.SicCode.Name = "SicCode"
        Me.SicCode.Size = New System.Drawing.Size(71, 20)
        Me.SicCode.TabIndex = 2
        '
        'AirProgramCodes
        '
        Me.AirProgramCodes.Controls.Add(Me.ApcRmp)
        Me.AirProgramCodes.Controls.Add(Me.HapMajor)
        Me.AirProgramCodes.Controls.Add(Me.NsrMajor)
        Me.AirProgramCodes.Controls.Add(Me.ApcAcid)
        Me.AirProgramCodes.Controls.Add(Me.ApcFesop)
        Me.AirProgramCodes.Controls.Add(Me.ApcTitleV)
        Me.AirProgramCodes.Controls.Add(Me.ApcMact)
        Me.AirProgramCodes.Controls.Add(Me.ApcNativeAmerican)
        Me.AirProgramCodes.Controls.Add(Me.ApcNsps)
        Me.AirProgramCodes.Controls.Add(Me.ApcNeshap)
        Me.AirProgramCodes.Controls.Add(Me.ApcNsr)
        Me.AirProgramCodes.Controls.Add(Me.ApcPsd)
        Me.AirProgramCodes.Controls.Add(Me.ApcCfc)
        Me.AirProgramCodes.Controls.Add(Me.ApcNonfederalSip)
        Me.AirProgramCodes.Controls.Add(Me.ApcFederalSip)
        Me.AirProgramCodes.Controls.Add(Me.ApcSip)
        Me.AirProgramCodes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AirProgramCodes.Location = New System.Drawing.Point(12, 119)
        Me.AirProgramCodes.Name = "AirProgramCodes"
        Me.AirProgramCodes.Size = New System.Drawing.Size(396, 126)
        Me.AirProgramCodes.TabIndex = 6
        Me.AirProgramCodes.TabStop = False
        Me.AirProgramCodes.Text = "Air Program Codes "
        '
        'ApcRmp
        '
        Me.ApcRmp.AutoSize = True
        Me.ApcRmp.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ApcRmp.Location = New System.Drawing.Point(265, 36)
        Me.ApcRmp.Margin = New System.Windows.Forms.Padding(2)
        Me.ApcRmp.Name = "ApcRmp"
        Me.ApcRmp.Size = New System.Drawing.Size(103, 17)
        Me.ApcRmp.TabIndex = 13
        Me.ApcRmp.Text = "Risk Mgmt. Plan"
        '
        'HapMajor
        '
        Me.HapMajor.AutoSize = True
        Me.HapMajor.Location = New System.Drawing.Point(265, 104)
        Me.HapMajor.Margin = New System.Windows.Forms.Padding(2)
        Me.HapMajor.Name = "HapMajor"
        Me.HapMajor.Size = New System.Drawing.Size(82, 17)
        Me.HapMajor.TabIndex = 15
        Me.HapMajor.Text = "HAPs Major"
        '
        'NsrMajor
        '
        Me.NsrMajor.AutoSize = True
        Me.NsrMajor.Location = New System.Drawing.Point(265, 87)
        Me.NsrMajor.Margin = New System.Windows.Forms.Padding(2)
        Me.NsrMajor.Name = "NsrMajor"
        Me.NsrMajor.Size = New System.Drawing.Size(105, 17)
        Me.NsrMajor.TabIndex = 14
        Me.NsrMajor.Text = "NSR/PSD Major"
        '
        'ApcAcid
        '
        Me.ApcAcid.AutoSize = True
        Me.ApcAcid.Location = New System.Drawing.Point(131, 53)
        Me.ApcAcid.Name = "ApcAcid"
        Me.ApcAcid.Size = New System.Drawing.Size(108, 17)
        Me.ApcAcid.TabIndex = 8
        Me.ApcAcid.Text = "Acid Precipitation"
        '
        'ApcFesop
        '
        Me.ApcFesop.AutoSize = True
        Me.ApcFesop.Location = New System.Drawing.Point(131, 70)
        Me.ApcFesop.Name = "ApcFesop"
        Me.ApcFesop.Size = New System.Drawing.Size(61, 17)
        Me.ApcFesop.TabIndex = 9
        Me.ApcFesop.Text = "FESOP"
        '
        'ApcTitleV
        '
        Me.ApcTitleV.AutoSize = True
        Me.ApcTitleV.Location = New System.Drawing.Point(265, 19)
        Me.ApcTitleV.Name = "ApcTitleV"
        Me.ApcTitleV.Size = New System.Drawing.Size(56, 17)
        Me.ApcTitleV.TabIndex = 12
        Me.ApcTitleV.Text = "Title V"
        '
        'ApcMact
        '
        Me.ApcMact.AutoSize = True
        Me.ApcMact.Location = New System.Drawing.Point(131, 104)
        Me.ApcMact.Name = "ApcMact"
        Me.ApcMact.Size = New System.Drawing.Size(99, 17)
        Me.ApcMact.TabIndex = 11
        Me.ApcMact.Text = "MACT (Part 63)"
        '
        'ApcNativeAmerican
        '
        Me.ApcNativeAmerican.AutoSize = True
        Me.ApcNativeAmerican.Location = New System.Drawing.Point(131, 87)
        Me.ApcNativeAmerican.Name = "ApcNativeAmerican"
        Me.ApcNativeAmerican.Size = New System.Drawing.Size(104, 17)
        Me.ApcNativeAmerican.TabIndex = 10
        Me.ApcNativeAmerican.Text = "Native American"
        '
        'ApcNsps
        '
        Me.ApcNsps.AutoSize = True
        Me.ApcNsps.Location = New System.Drawing.Point(131, 36)
        Me.ApcNsps.Name = "ApcNsps"
        Me.ApcNsps.Size = New System.Drawing.Size(55, 17)
        Me.ApcNsps.TabIndex = 7
        Me.ApcNsps.Text = "NSPS"
        '
        'ApcNeshap
        '
        Me.ApcNeshap.AutoSize = True
        Me.ApcNeshap.Location = New System.Drawing.Point(131, 19)
        Me.ApcNeshap.Name = "ApcNeshap"
        Me.ApcNeshap.Size = New System.Drawing.Size(113, 17)
        Me.ApcNeshap.TabIndex = 6
        Me.ApcNeshap.Text = "NESHAP (Part 61)"
        '
        'ApcNsr
        '
        Me.ApcNsr.AutoSize = True
        Me.ApcNsr.Location = New System.Drawing.Point(6, 104)
        Me.ApcNsr.Name = "ApcNsr"
        Me.ApcNsr.Size = New System.Drawing.Size(49, 17)
        Me.ApcNsr.TabIndex = 5
        Me.ApcNsr.Text = "NSR"
        '
        'ApcPsd
        '
        Me.ApcPsd.AutoSize = True
        Me.ApcPsd.Location = New System.Drawing.Point(6, 87)
        Me.ApcPsd.Name = "ApcPsd"
        Me.ApcPsd.Size = New System.Drawing.Size(48, 17)
        Me.ApcPsd.TabIndex = 4
        Me.ApcPsd.Text = "PSD"
        '
        'ApcCfc
        '
        Me.ApcCfc.AutoSize = True
        Me.ApcCfc.Location = New System.Drawing.Point(6, 70)
        Me.ApcCfc.Name = "ApcCfc"
        Me.ApcCfc.Size = New System.Drawing.Size(91, 17)
        Me.ApcCfc.TabIndex = 3
        Me.ApcCfc.Text = "CFC Tracking"
        '
        'ApcNonfederalSip
        '
        Me.ApcNonfederalSip.AutoSize = True
        Me.ApcNonfederalSip.Location = New System.Drawing.Point(6, 53)
        Me.ApcNonfederalSip.Name = "ApcNonfederalSip"
        Me.ApcNonfederalSip.Size = New System.Drawing.Size(104, 17)
        Me.ApcNonfederalSip.TabIndex = 2
        Me.ApcNonfederalSip.Text = "Non-Federal SIP"
        '
        'ApcFederalSip
        '
        Me.ApcFederalSip.AutoSize = True
        Me.ApcFederalSip.Location = New System.Drawing.Point(6, 36)
        Me.ApcFederalSip.Name = "ApcFederalSip"
        Me.ApcFederalSip.Size = New System.Drawing.Size(81, 17)
        Me.ApcFederalSip.TabIndex = 1
        Me.ApcFederalSip.Text = "Federal SIP"
        '
        'ApcSip
        '
        Me.ApcSip.AutoSize = True
        Me.ApcSip.Location = New System.Drawing.Point(6, 19)
        Me.ApcSip.Name = "ApcSip"
        Me.ApcSip.Size = New System.Drawing.Size(43, 17)
        Me.ApcSip.TabIndex = 0
        Me.ApcSip.Text = "SIP"
        '
        'RmpLabel
        '
        Me.RmpLabel.AutoSize = True
        Me.RmpLabel.Location = New System.Drawing.Point(455, 262)
        Me.RmpLabel.Name = "RmpLabel"
        Me.RmpLabel.Size = New System.Drawing.Size(48, 13)
        Me.RmpLabel.TabIndex = 163
        Me.RmpLabel.Text = "RMP ID:"
        '
        'RmpId
        '
        Me.RmpId.Location = New System.Drawing.Point(509, 259)
        Me.RmpId.Mask = "0000-0000-0000"
        Me.RmpId.Name = "RmpId"
        Me.RmpId.Size = New System.Drawing.Size(85, 20)
        Me.RmpId.TabIndex = 9
        '
        'ModifiedDescDisplay
        '
        Me.ModifiedDescDisplay.Location = New System.Drawing.Point(12, 338)
        Me.ModifiedDescDisplay.Name = "ModifiedDescDisplay"
        Me.ModifiedDescDisplay.Size = New System.Drawing.Size(267, 31)
        Me.ModifiedDescDisplay.TabIndex = 366
        Me.ModifiedDescDisplay.Text = "Modified:"
        '
        'FacilityNameDisplay
        '
        Me.FacilityNameDisplay.AutoSize = True
        Me.FacilityNameDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityNameDisplay.Location = New System.Drawing.Point(92, 20)
        Me.FacilityNameDisplay.Name = "FacilityNameDisplay"
        Me.FacilityNameDisplay.Size = New System.Drawing.Size(92, 17)
        Me.FacilityNameDisplay.TabIndex = 351
        Me.FacilityNameDisplay.Text = "Facility Name"
        '
        'AirsNumberDisplay
        '
        Me.AirsNumberDisplay.AutoSize = True
        Me.AirsNumberDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AirsNumberDisplay.Location = New System.Drawing.Point(12, 20)
        Me.AirsNumberDisplay.Name = "AirsNumberDisplay"
        Me.AirsNumberDisplay.Size = New System.Drawing.Size(77, 17)
        Me.AirsNumberDisplay.TabIndex = 352
        Me.AirsNumberDisplay.Text = "000-00000"
        '
        'NonattainmentStatuses
        '
        Me.NonattainmentStatuses.Controls.Add(Me.PmFine)
        Me.NonattainmentStatuses.Controls.Add(Me.EightHourOzone)
        Me.NonattainmentStatuses.Controls.Add(Me.OneHourOzone)
        Me.NonattainmentStatuses.Controls.Add(Me.Label97)
        Me.NonattainmentStatuses.Controls.Add(Me.Label96)
        Me.NonattainmentStatuses.Controls.Add(Me.Label94)
        Me.NonattainmentStatuses.Location = New System.Drawing.Point(414, 119)
        Me.NonattainmentStatuses.Name = "NonattainmentStatuses"
        Me.NonattainmentStatuses.Size = New System.Drawing.Size(186, 126)
        Me.NonattainmentStatuses.TabIndex = 7
        Me.NonattainmentStatuses.TabStop = False
        Me.NonattainmentStatuses.Text = "Nonattainment Statuses"
        '
        'PmFine
        '
        Me.PmFine.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PmFine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.PmFine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PmFine.BackColor = System.Drawing.Color.White
        Me.PmFine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PmFine.FormattingEnabled = True
        Me.PmFine.Location = New System.Drawing.Point(72, 67)
        Me.PmFine.Name = "PmFine"
        Me.PmFine.Size = New System.Drawing.Size(108, 21)
        Me.PmFine.TabIndex = 2
        '
        'EightHourOzone
        '
        Me.EightHourOzone.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EightHourOzone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EightHourOzone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EightHourOzone.BackColor = System.Drawing.Color.White
        Me.EightHourOzone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EightHourOzone.FormattingEnabled = True
        Me.EightHourOzone.Location = New System.Drawing.Point(72, 43)
        Me.EightHourOzone.Name = "EightHourOzone"
        Me.EightHourOzone.Size = New System.Drawing.Size(108, 21)
        Me.EightHourOzone.TabIndex = 1
        '
        'OneHourOzone
        '
        Me.OneHourOzone.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OneHourOzone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.OneHourOzone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.OneHourOzone.BackColor = System.Drawing.Color.White
        Me.OneHourOzone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OneHourOzone.FormattingEnabled = True
        Me.OneHourOzone.Location = New System.Drawing.Point(72, 19)
        Me.OneHourOzone.Name = "OneHourOzone"
        Me.OneHourOzone.Size = New System.Drawing.Size(108, 21)
        Me.OneHourOzone.TabIndex = 0
        '
        'Label97
        '
        Me.Label97.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label97.AutoSize = True
        Me.Label97.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(22, 70)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(44, 13)
        Me.Label97.TabIndex = 2
        Me.Label97.Text = "PM 2.5:"
        '
        'Label96
        '
        Me.Label96.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label96.AutoSize = True
        Me.Label96.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(6, 46)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(60, 13)
        Me.Label96.TabIndex = 1
        Me.Label96.Text = "8-hr ozone:"
        '
        'Label94
        '
        Me.Label94.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label94.AutoSize = True
        Me.Label94.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.Location = New System.Drawing.Point(6, 22)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(60, 13)
        Me.Label94.TabIndex = 0
        Me.Label94.Text = "1-hr ozone:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(447, 86)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 379
        Me.Label9.Text = "NAICS Code:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'NaicsCode
        '
        Me.NaicsCode.BackColor = System.Drawing.Color.White
        Me.NaicsCode.Location = New System.Drawing.Point(523, 83)
        Me.NaicsCode.Name = "NaicsCode"
        Me.NaicsCode.Size = New System.Drawing.Size(71, 20)
        Me.NaicsCode.TabIndex = 5
        '
        'SaveChangesButton
        '
        Me.SaveChangesButton.Enabled = False
        Me.SaveChangesButton.Location = New System.Drawing.Point(285, 338)
        Me.SaveChangesButton.Name = "SaveChangesButton"
        Me.SaveChangesButton.Size = New System.Drawing.Size(100, 23)
        Me.SaveChangesButton.TabIndex = 12
        Me.SaveChangesButton.Text = "Save Changes"
        Me.SaveChangesButton.UseVisualStyleBackColor = True
        '
        'CancelEditButton
        '
        Me.CancelEditButton.Enabled = False
        Me.CancelEditButton.Location = New System.Drawing.Point(390, 338)
        Me.CancelEditButton.Name = "CancelEditButton"
        Me.CancelEditButton.Size = New System.Drawing.Size(100, 23)
        Me.CancelEditButton.TabIndex = 13
        Me.CancelEditButton.Text = "Cancel"
        Me.CancelEditButton.UseVisualStyleBackColor = True
        '
        'EditableButton
        '
        Me.EditableButton.Appearance = System.Windows.Forms.Appearance.Button
        Me.EditableButton.AutoSize = True
        Me.EditableButton.Location = New System.Drawing.Point(496, 338)
        Me.EditableButton.Name = "EditableButton"
        Me.EditableButton.Size = New System.Drawing.Size(98, 23)
        Me.EditableButton.TabIndex = 380
        Me.EditableButton.Text = "Edit Current Data"
        Me.EditableButton.UseVisualStyleBackColor = True
        '
        'IAIPEditHeaderData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 560)
        Me.Controls.Add(Me.EditableButton)
        Me.Controls.Add(Me.CancelEditButton)
        Me.Controls.Add(Me.RmpLabel)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.RmpId)
        Me.Controls.Add(Me.NaicsCode)
        Me.Controls.Add(Me.NonattainmentStatuses)
        Me.Controls.Add(Me.FacilityDescription)
        Me.Controls.Add(Me.FacilityDescriptionLabel)
        Me.Controls.Add(Me.FacilityHistoryDisplay)
        Me.Controls.Add(Me.Comments)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PermitRevocationDateLabel)
        Me.Controls.Add(Me.StartupDateLabel)
        Me.Controls.Add(Me.StartUpDate)
        Me.Controls.Add(Me.ShutdownDate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Classification)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.OperationalStatus)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.SicCode)
        Me.Controls.Add(Me.AirProgramCodes)
        Me.Controls.Add(Me.ModifiedDescDisplay)
        Me.Controls.Add(Me.FacilityNameDisplay)
        Me.Controls.Add(Me.AirsNumberDisplay)
        Me.Controls.Add(Me.SaveChangesButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IAIPEditHeaderData"
        Me.Text = "Edit Header Data"
        CType(Me.FacilityHistoryDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AirProgramCodes.ResumeLayout(False)
        Me.AirProgramCodes.PerformLayout()
        Me.NonattainmentStatuses.ResumeLayout(False)
        Me.NonattainmentStatuses.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FacilityDescription As System.Windows.Forms.TextBox
    Friend WithEvents FacilityDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents FacilityHistoryDisplay As System.Windows.Forms.DataGridView
    Friend WithEvents Comments As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PermitRevocationDateLabel As System.Windows.Forms.Label
    Friend WithEvents StartupDateLabel As System.Windows.Forms.Label
    Friend WithEvents StartUpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents ShutdownDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Classification As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents OperationalStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents SicCode As System.Windows.Forms.TextBox
    Friend WithEvents AirProgramCodes As System.Windows.Forms.GroupBox
    Friend WithEvents ApcAcid As System.Windows.Forms.CheckBox
    Friend WithEvents ApcFesop As System.Windows.Forms.CheckBox
    Friend WithEvents ApcTitleV As System.Windows.Forms.CheckBox
    Friend WithEvents ApcMact As System.Windows.Forms.CheckBox
    Friend WithEvents ApcNativeAmerican As System.Windows.Forms.CheckBox
    Friend WithEvents ApcNsps As System.Windows.Forms.CheckBox
    Friend WithEvents ApcNeshap As System.Windows.Forms.CheckBox
    Friend WithEvents ApcNsr As System.Windows.Forms.CheckBox
    Friend WithEvents ApcPsd As System.Windows.Forms.CheckBox
    Friend WithEvents ApcCfc As System.Windows.Forms.CheckBox
    Friend WithEvents ApcNonfederalSip As System.Windows.Forms.CheckBox
    Friend WithEvents ApcFederalSip As System.Windows.Forms.CheckBox
    Friend WithEvents ApcSip As System.Windows.Forms.CheckBox
    Friend WithEvents ModifiedDescDisplay As System.Windows.Forms.Label
    Friend WithEvents FacilityNameDisplay As System.Windows.Forms.Label
    Friend WithEvents AirsNumberDisplay As System.Windows.Forms.Label
    Friend WithEvents HapMajor As System.Windows.Forms.CheckBox
    Friend WithEvents NsrMajor As System.Windows.Forms.CheckBox
    Friend WithEvents NonattainmentStatuses As System.Windows.Forms.GroupBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents OneHourOzone As System.Windows.Forms.ComboBox
    Friend WithEvents PmFine As System.Windows.Forms.ComboBox
    Friend WithEvents EightHourOzone As System.Windows.Forms.ComboBox
    Friend WithEvents ApcRmp As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents NaicsCode As System.Windows.Forms.TextBox
    Friend WithEvents RmpLabel As System.Windows.Forms.Label
    Friend WithEvents RmpId As System.Windows.Forms.MaskedTextBox
    Friend WithEvents SaveChangesButton As System.Windows.Forms.Button
    Friend WithEvents CancelEditButton As System.Windows.Forms.Button
    Friend WithEvents EditableButton As System.Windows.Forms.CheckBox
End Class
