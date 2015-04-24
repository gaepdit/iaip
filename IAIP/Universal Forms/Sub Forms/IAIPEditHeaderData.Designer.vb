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
        Me.FacilityHistoryDataGridView = New System.Windows.Forms.DataGridView
        Me.Comments = New System.Windows.Forms.TextBox
        Me.CommentsLabel = New System.Windows.Forms.Label
        Me.PermitRevocationDateLabel = New System.Windows.Forms.Label
        Me.StartupDateLabel = New System.Windows.Forms.Label
        Me.StartUpDate = New System.Windows.Forms.DateTimePicker
        Me.ShutdownDate = New System.Windows.Forms.DateTimePicker
        Me.ClassificationLabel = New System.Windows.Forms.Label
        Me.ClassificationDropDown = New System.Windows.Forms.ComboBox
        Me.OperationalStatusLabel = New System.Windows.Forms.Label
        Me.OperationalDropDown = New System.Windows.Forms.ComboBox
        Me.SicCodeLabel = New System.Windows.Forms.Label
        Me.SicCode = New System.Windows.Forms.TextBox
        Me.AirProgramCodes = New System.Windows.Forms.GroupBox
        Me.ApcRmp = New System.Windows.Forms.CheckBox
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
        Me.HapMajor = New System.Windows.Forms.CheckBox
        Me.NsrMajor = New System.Windows.Forms.CheckBox
        Me.RmpIdLabel = New System.Windows.Forms.Label
        Me.RmpId = New System.Windows.Forms.MaskedTextBox
        Me.ModifiedDescDisplay = New System.Windows.Forms.Label
        Me.FacilityNameDisplay = New System.Windows.Forms.Label
        Me.AirsNumberDisplay = New System.Windows.Forms.Label
        Me.NonattainmentStatuses = New System.Windows.Forms.GroupBox
        Me.PmFineDropDown = New System.Windows.Forms.ComboBox
        Me.EightHourOzoneDropDown = New System.Windows.Forms.ComboBox
        Me.OneHourOzoneDropDown = New System.Windows.Forms.ComboBox
        Me.Label97 = New System.Windows.Forms.Label
        Me.Label96 = New System.Windows.Forms.Label
        Me.Label94 = New System.Windows.Forms.Label
        Me.NaicsCodeLabel = New System.Windows.Forms.Label
        Me.NaicsCode = New System.Windows.Forms.TextBox
        Me.SaveChangesButton = New System.Windows.Forms.Button
        Me.CancelEditButton = New System.Windows.Forms.Button
        Me.EditData = New System.Windows.Forms.CheckBox
        Me.AirProgramClassifications = New System.Windows.Forms.GroupBox
        CType(Me.FacilityHistoryDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AirProgramCodes.SuspendLayout()
        Me.NonattainmentStatuses.SuspendLayout()
        Me.AirProgramClassifications.SuspendLayout()
        Me.SuspendLayout()
        '
        'FacilityDescription
        '
        Me.FacilityDescription.AcceptsReturn = True
        Me.FacilityDescription.Location = New System.Drawing.Point(116, 274)
        Me.FacilityDescription.MaxLength = 4000
        Me.FacilityDescription.Multiline = True
        Me.FacilityDescription.Name = "FacilityDescription"
        Me.FacilityDescription.Size = New System.Drawing.Size(333, 20)
        Me.FacilityDescription.TabIndex = 11
        '
        'FacilityDescriptionLabel
        '
        Me.FacilityDescriptionLabel.AutoSize = True
        Me.FacilityDescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityDescriptionLabel.Location = New System.Drawing.Point(12, 277)
        Me.FacilityDescriptionLabel.Name = "FacilityDescriptionLabel"
        Me.FacilityDescriptionLabel.Size = New System.Drawing.Size(98, 13)
        Me.FacilityDescriptionLabel.TabIndex = 376
        Me.FacilityDescriptionLabel.Text = "Facility Description:"
        Me.FacilityDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'FacilityHistoryDataGridView
        '
        Me.FacilityHistoryDataGridView.AllowUserToAddRows = False
        Me.FacilityHistoryDataGridView.AllowUserToDeleteRows = False
        Me.FacilityHistoryDataGridView.AllowUserToOrderColumns = True
        Me.FacilityHistoryDataGridView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FacilityHistoryDataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.FacilityHistoryDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacilityHistoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FacilityHistoryDataGridView.GridColor = System.Drawing.SystemColors.ControlLight
        Me.FacilityHistoryDataGridView.Location = New System.Drawing.Point(0, 387)
        Me.FacilityHistoryDataGridView.MultiSelect = False
        Me.FacilityHistoryDataGridView.Name = "FacilityHistoryDataGridView"
        Me.FacilityHistoryDataGridView.ReadOnly = True
        Me.FacilityHistoryDataGridView.RowHeadersVisible = False
        Me.FacilityHistoryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FacilityHistoryDataGridView.Size = New System.Drawing.Size(612, 173)
        Me.FacilityHistoryDataGridView.TabIndex = 18
        '
        'Comments
        '
        Me.Comments.AcceptsReturn = True
        Me.Comments.Location = New System.Drawing.Point(116, 300)
        Me.Comments.MaxLength = 4000
        Me.Comments.Multiline = True
        Me.Comments.Name = "Comments"
        Me.Comments.Size = New System.Drawing.Size(478, 40)
        Me.Comments.TabIndex = 13
        '
        'CommentsLabel
        '
        Me.CommentsLabel.AutoSize = True
        Me.CommentsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CommentsLabel.Location = New System.Drawing.Point(12, 303)
        Me.CommentsLabel.Name = "CommentsLabel"
        Me.CommentsLabel.Size = New System.Drawing.Size(59, 13)
        Me.CommentsLabel.TabIndex = 372
        Me.CommentsLabel.Text = "Comments:"
        Me.CommentsLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'PermitRevocationDateLabel
        '
        Me.PermitRevocationDateLabel.AutoSize = True
        Me.PermitRevocationDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PermitRevocationDateLabel.Location = New System.Drawing.Point(217, 79)
        Me.PermitRevocationDateLabel.Name = "PermitRevocationDateLabel"
        Me.PermitRevocationDateLabel.Size = New System.Drawing.Size(91, 26)
        Me.PermitRevocationDateLabel.TabIndex = 4
        Me.PermitRevocationDateLabel.Text = "Final Permit " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Revocation Date:"
        '
        'StartupDateLabel
        '
        Me.StartupDateLabel.AutoSize = True
        Me.StartupDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartupDateLabel.Location = New System.Drawing.Point(12, 86)
        Me.StartupDateLabel.Name = "StartupDateLabel"
        Me.StartupDateLabel.Size = New System.Drawing.Size(70, 13)
        Me.StartupDateLabel.TabIndex = 370
        Me.StartupDateLabel.Text = "Startup Date:"
        Me.StartupDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'StartUpDate
        '
        Me.StartUpDate.Checked = False
        Me.StartUpDate.CustomFormat = "dd-MMM-yyyy"
        Me.StartUpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.StartUpDate.Location = New System.Drawing.Point(89, 84)
        Me.StartUpDate.Name = "StartUpDate"
        Me.StartUpDate.ShowCheckBox = True
        Me.StartUpDate.Size = New System.Drawing.Size(121, 20)
        Me.StartUpDate.TabIndex = 4
        '
        'ShutdownDate
        '
        Me.ShutdownDate.Checked = False
        Me.ShutdownDate.CustomFormat = "dd-MMM-yyyy"
        Me.ShutdownDate.Enabled = False
        Me.ShutdownDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ShutdownDate.Location = New System.Drawing.Point(320, 83)
        Me.ShutdownDate.Name = "ShutdownDate"
        Me.ShutdownDate.ShowCheckBox = True
        Me.ShutdownDate.Size = New System.Drawing.Size(121, 20)
        Me.ShutdownDate.TabIndex = 5
        '
        'ClassificationLabel
        '
        Me.ClassificationLabel.AutoSize = True
        Me.ClassificationLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClassificationLabel.Location = New System.Drawing.Point(12, 59)
        Me.ClassificationLabel.Name = "ClassificationLabel"
        Me.ClassificationLabel.Size = New System.Drawing.Size(71, 13)
        Me.ClassificationLabel.TabIndex = 356
        Me.ClassificationLabel.Text = "Classification:"
        Me.ClassificationLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'ClassificationDropDown
        '
        Me.ClassificationDropDown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ClassificationDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ClassificationDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ClassificationDropDown.Location = New System.Drawing.Point(89, 57)
        Me.ClassificationDropDown.Name = "ClassificationDropDown"
        Me.ClassificationDropDown.Size = New System.Drawing.Size(121, 21)
        Me.ClassificationDropDown.TabIndex = 2
        '
        'OperationalStatusLabel
        '
        Me.OperationalStatusLabel.AutoSize = True
        Me.OperationalStatusLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OperationalStatusLabel.Location = New System.Drawing.Point(217, 59)
        Me.OperationalStatusLabel.Name = "OperationalStatusLabel"
        Me.OperationalStatusLabel.Size = New System.Drawing.Size(89, 13)
        Me.OperationalStatusLabel.TabIndex = 359
        Me.OperationalStatusLabel.Text = "Operating Status:"
        Me.OperationalStatusLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'OperationalDropDown
        '
        Me.OperationalDropDown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.OperationalDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.OperationalDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OperationalDropDown.Location = New System.Drawing.Point(320, 56)
        Me.OperationalDropDown.Name = "OperationalDropDown"
        Me.OperationalDropDown.Size = New System.Drawing.Size(121, 21)
        Me.OperationalDropDown.TabIndex = 3
        '
        'SicCodeLabel
        '
        Me.SicCodeLabel.AutoSize = True
        Me.SicCodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SicCodeLabel.Location = New System.Drawing.Point(447, 59)
        Me.SicCodeLabel.Name = "SicCodeLabel"
        Me.SicCodeLabel.Size = New System.Drawing.Size(55, 13)
        Me.SicCodeLabel.TabIndex = 357
        Me.SicCodeLabel.Text = "SIC Code:"
        Me.SicCodeLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'SicCode
        '
        Me.SicCode.Location = New System.Drawing.Point(523, 56)
        Me.SicCode.Name = "SicCode"
        Me.SicCode.Size = New System.Drawing.Size(71, 20)
        Me.SicCode.TabIndex = 6
        '
        'AirProgramCodes
        '
        Me.AirProgramCodes.Controls.Add(Me.ApcRmp)
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
        Me.AirProgramCodes.Size = New System.Drawing.Size(246, 145)
        Me.AirProgramCodes.TabIndex = 8
        Me.AirProgramCodes.TabStop = False
        Me.AirProgramCodes.Text = "Air Program Codes "
        '
        'ApcRmp
        '
        Me.ApcRmp.AutoSize = True
        Me.ApcRmp.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ApcRmp.Location = New System.Drawing.Point(127, 121)
        Me.ApcRmp.Margin = New System.Windows.Forms.Padding(2)
        Me.ApcRmp.Name = "ApcRmp"
        Me.ApcRmp.Size = New System.Drawing.Size(103, 17)
        Me.ApcRmp.TabIndex = 13
        Me.ApcRmp.Text = "Risk Mgmt. Plan"
        '
        'ApcAcid
        '
        Me.ApcAcid.AutoSize = True
        Me.ApcAcid.Location = New System.Drawing.Point(127, 70)
        Me.ApcAcid.Name = "ApcAcid"
        Me.ApcAcid.Size = New System.Drawing.Size(108, 17)
        Me.ApcAcid.TabIndex = 10
        Me.ApcAcid.Text = "Acid Precipitation"
        '
        'ApcFesop
        '
        Me.ApcFesop.AutoSize = True
        Me.ApcFesop.Location = New System.Drawing.Point(127, 87)
        Me.ApcFesop.Name = "ApcFesop"
        Me.ApcFesop.Size = New System.Drawing.Size(61, 17)
        Me.ApcFesop.TabIndex = 11
        Me.ApcFesop.Text = "FESOP"
        '
        'ApcTitleV
        '
        Me.ApcTitleV.AutoSize = True
        Me.ApcTitleV.Location = New System.Drawing.Point(6, 121)
        Me.ApcTitleV.Name = "ApcTitleV"
        Me.ApcTitleV.Size = New System.Drawing.Size(56, 17)
        Me.ApcTitleV.TabIndex = 6
        Me.ApcTitleV.Text = "Title V"
        '
        'ApcMact
        '
        Me.ApcMact.AutoSize = True
        Me.ApcMact.Location = New System.Drawing.Point(127, 19)
        Me.ApcMact.Name = "ApcMact"
        Me.ApcMact.Size = New System.Drawing.Size(99, 17)
        Me.ApcMact.TabIndex = 7
        Me.ApcMact.Text = "MACT (Part 63)"
        '
        'ApcNativeAmerican
        '
        Me.ApcNativeAmerican.AutoSize = True
        Me.ApcNativeAmerican.Location = New System.Drawing.Point(127, 104)
        Me.ApcNativeAmerican.Name = "ApcNativeAmerican"
        Me.ApcNativeAmerican.Size = New System.Drawing.Size(104, 17)
        Me.ApcNativeAmerican.TabIndex = 12
        Me.ApcNativeAmerican.Text = "Native American"
        '
        'ApcNsps
        '
        Me.ApcNsps.AutoSize = True
        Me.ApcNsps.Location = New System.Drawing.Point(127, 53)
        Me.ApcNsps.Name = "ApcNsps"
        Me.ApcNsps.Size = New System.Drawing.Size(55, 17)
        Me.ApcNsps.TabIndex = 9
        Me.ApcNsps.Text = "NSPS"
        '
        'ApcNeshap
        '
        Me.ApcNeshap.AutoSize = True
        Me.ApcNeshap.Location = New System.Drawing.Point(127, 36)
        Me.ApcNeshap.Name = "ApcNeshap"
        Me.ApcNeshap.Size = New System.Drawing.Size(113, 17)
        Me.ApcNeshap.TabIndex = 8
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
        'HapMajor
        '
        Me.HapMajor.AutoSize = True
        Me.HapMajor.Location = New System.Drawing.Point(5, 36)
        Me.HapMajor.Margin = New System.Windows.Forms.Padding(2)
        Me.HapMajor.Name = "HapMajor"
        Me.HapMajor.Size = New System.Drawing.Size(82, 17)
        Me.HapMajor.TabIndex = 15
        Me.HapMajor.Text = "HAPs Major"
        '
        'NsrMajor
        '
        Me.NsrMajor.AutoSize = True
        Me.NsrMajor.Location = New System.Drawing.Point(5, 19)
        Me.NsrMajor.Margin = New System.Windows.Forms.Padding(2)
        Me.NsrMajor.Name = "NsrMajor"
        Me.NsrMajor.Size = New System.Drawing.Size(105, 17)
        Me.NsrMajor.TabIndex = 14
        Me.NsrMajor.Text = "NSR/PSD Major"
        '
        'RmpIdLabel
        '
        Me.RmpIdLabel.AutoSize = True
        Me.RmpIdLabel.Location = New System.Drawing.Point(455, 277)
        Me.RmpIdLabel.Name = "RmpIdLabel"
        Me.RmpIdLabel.Size = New System.Drawing.Size(48, 13)
        Me.RmpIdLabel.TabIndex = 163
        Me.RmpIdLabel.Text = "RMP ID:"
        '
        'RmpId
        '
        Me.RmpId.AllowPromptAsInput = False
        Me.RmpId.HidePromptOnLeave = True
        Me.RmpId.Location = New System.Drawing.Point(509, 274)
        Me.RmpId.Mask = "0000-0000-0000"
        Me.RmpId.Name = "RmpId"
        Me.RmpId.Size = New System.Drawing.Size(85, 20)
        Me.RmpId.TabIndex = 12
        Me.RmpId.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        '
        'ModifiedDescDisplay
        '
        Me.ModifiedDescDisplay.AutoSize = True
        Me.ModifiedDescDisplay.Location = New System.Drawing.Point(12, 353)
        Me.ModifiedDescDisplay.MaximumSize = New System.Drawing.Size(386, 0)
        Me.ModifiedDescDisplay.Name = "ModifiedDescDisplay"
        Me.ModifiedDescDisplay.Size = New System.Drawing.Size(50, 13)
        Me.ModifiedDescDisplay.TabIndex = 366
        Me.ModifiedDescDisplay.Text = "Modified:"
        '
        'FacilityNameDisplay
        '
        Me.FacilityNameDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FacilityNameDisplay.Location = New System.Drawing.Point(92, 20)
        Me.FacilityNameDisplay.Name = "FacilityNameDisplay"
        Me.FacilityNameDisplay.Size = New System.Drawing.Size(388, 34)
        Me.FacilityNameDisplay.TabIndex = 1
        Me.FacilityNameDisplay.Text = "Facility Name"
        '
        'AirsNumberDisplay
        '
        Me.AirsNumberDisplay.AutoSize = True
        Me.AirsNumberDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AirsNumberDisplay.Location = New System.Drawing.Point(12, 20)
        Me.AirsNumberDisplay.Name = "AirsNumberDisplay"
        Me.AirsNumberDisplay.Size = New System.Drawing.Size(77, 17)
        Me.AirsNumberDisplay.TabIndex = 0
        Me.AirsNumberDisplay.Text = "000-00000"
        '
        'NonattainmentStatuses
        '
        Me.NonattainmentStatuses.Controls.Add(Me.PmFineDropDown)
        Me.NonattainmentStatuses.Controls.Add(Me.EightHourOzoneDropDown)
        Me.NonattainmentStatuses.Controls.Add(Me.OneHourOzoneDropDown)
        Me.NonattainmentStatuses.Controls.Add(Me.Label97)
        Me.NonattainmentStatuses.Controls.Add(Me.Label96)
        Me.NonattainmentStatuses.Controls.Add(Me.Label94)
        Me.NonattainmentStatuses.Location = New System.Drawing.Point(414, 119)
        Me.NonattainmentStatuses.Name = "NonattainmentStatuses"
        Me.NonattainmentStatuses.Size = New System.Drawing.Size(186, 145)
        Me.NonattainmentStatuses.TabIndex = 10
        Me.NonattainmentStatuses.TabStop = False
        Me.NonattainmentStatuses.Text = "Nonattainment Statuses"
        '
        'PmFineDropDown
        '
        Me.PmFineDropDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PmFineDropDown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.PmFineDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PmFineDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PmFineDropDown.FormattingEnabled = True
        Me.PmFineDropDown.Location = New System.Drawing.Point(72, 73)
        Me.PmFineDropDown.Name = "PmFineDropDown"
        Me.PmFineDropDown.Size = New System.Drawing.Size(108, 21)
        Me.PmFineDropDown.TabIndex = 2
        '
        'EightHourOzoneDropDown
        '
        Me.EightHourOzoneDropDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EightHourOzoneDropDown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EightHourOzoneDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EightHourOzoneDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EightHourOzoneDropDown.FormattingEnabled = True
        Me.EightHourOzoneDropDown.Location = New System.Drawing.Point(72, 46)
        Me.EightHourOzoneDropDown.Name = "EightHourOzoneDropDown"
        Me.EightHourOzoneDropDown.Size = New System.Drawing.Size(108, 21)
        Me.EightHourOzoneDropDown.TabIndex = 1
        '
        'OneHourOzoneDropDown
        '
        Me.OneHourOzoneDropDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OneHourOzoneDropDown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.OneHourOzoneDropDown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.OneHourOzoneDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OneHourOzoneDropDown.FormattingEnabled = True
        Me.OneHourOzoneDropDown.Location = New System.Drawing.Point(72, 19)
        Me.OneHourOzoneDropDown.Name = "OneHourOzoneDropDown"
        Me.OneHourOzoneDropDown.Size = New System.Drawing.Size(108, 21)
        Me.OneHourOzoneDropDown.TabIndex = 0
        '
        'Label97
        '
        Me.Label97.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label97.AutoSize = True
        Me.Label97.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(6, 76)
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
        Me.Label96.Location = New System.Drawing.Point(6, 49)
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
        'NaicsCodeLabel
        '
        Me.NaicsCodeLabel.AutoSize = True
        Me.NaicsCodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NaicsCodeLabel.Location = New System.Drawing.Point(447, 86)
        Me.NaicsCodeLabel.Name = "NaicsCodeLabel"
        Me.NaicsCodeLabel.Size = New System.Drawing.Size(70, 13)
        Me.NaicsCodeLabel.TabIndex = 379
        Me.NaicsCodeLabel.Text = "NAICS Code:"
        Me.NaicsCodeLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'NaicsCode
        '
        Me.NaicsCode.Location = New System.Drawing.Point(523, 83)
        Me.NaicsCode.Name = "NaicsCode"
        Me.NaicsCode.Size = New System.Drawing.Size(71, 20)
        Me.NaicsCode.TabIndex = 7
        '
        'SaveChangesButton
        '
        Me.SaveChangesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.SaveChangesButton.Enabled = False
        Me.SaveChangesButton.Image = Global.Iaip.My.Resources.Resources.SaveButtonImage
        Me.SaveChangesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveChangesButton.Location = New System.Drawing.Point(486, 353)
        Me.SaveChangesButton.Name = "SaveChangesButton"
        Me.SaveChangesButton.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.SaveChangesButton.Size = New System.Drawing.Size(108, 23)
        Me.SaveChangesButton.TabIndex = 14
        Me.SaveChangesButton.Text = "     Save Changes"
        Me.SaveChangesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveChangesButton.UseVisualStyleBackColor = True
        '
        'CancelEditButton
        '
        Me.CancelEditButton.Enabled = False
        Me.CancelEditButton.Location = New System.Drawing.Point(404, 353)
        Me.CancelEditButton.Name = "CancelEditButton"
        Me.CancelEditButton.Size = New System.Drawing.Size(76, 23)
        Me.CancelEditButton.TabIndex = 15
        Me.CancelEditButton.Text = "Cancel"
        Me.CancelEditButton.UseVisualStyleBackColor = True
        '
        'EditData
        '
        Me.EditData.Appearance = System.Windows.Forms.Appearance.Button
        Me.EditData.Location = New System.Drawing.Point(486, 17)
        Me.EditData.Name = "EditData"
        Me.EditData.Size = New System.Drawing.Size(108, 23)
        Me.EditData.TabIndex = 17
        Me.EditData.Text = "Edit Data"
        Me.EditData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EditData.UseVisualStyleBackColor = True
        '
        'AirProgramClassifications
        '
        Me.AirProgramClassifications.Controls.Add(Me.NsrMajor)
        Me.AirProgramClassifications.Controls.Add(Me.HapMajor)
        Me.AirProgramClassifications.Location = New System.Drawing.Point(264, 119)
        Me.AirProgramClassifications.Name = "AirProgramClassifications"
        Me.AirProgramClassifications.Size = New System.Drawing.Size(144, 145)
        Me.AirProgramClassifications.TabIndex = 9
        Me.AirProgramClassifications.TabStop = False
        Me.AirProgramClassifications.Text = "Air Program Classifications"
        '
        'IAIPEditHeaderData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 560)
        Me.Controls.Add(Me.AirProgramClassifications)
        Me.Controls.Add(Me.EditData)
        Me.Controls.Add(Me.CancelEditButton)
        Me.Controls.Add(Me.RmpIdLabel)
        Me.Controls.Add(Me.NaicsCodeLabel)
        Me.Controls.Add(Me.RmpId)
        Me.Controls.Add(Me.NaicsCode)
        Me.Controls.Add(Me.NonattainmentStatuses)
        Me.Controls.Add(Me.FacilityDescription)
        Me.Controls.Add(Me.FacilityDescriptionLabel)
        Me.Controls.Add(Me.FacilityHistoryDataGridView)
        Me.Controls.Add(Me.Comments)
        Me.Controls.Add(Me.CommentsLabel)
        Me.Controls.Add(Me.PermitRevocationDateLabel)
        Me.Controls.Add(Me.StartupDateLabel)
        Me.Controls.Add(Me.StartUpDate)
        Me.Controls.Add(Me.ShutdownDate)
        Me.Controls.Add(Me.ClassificationLabel)
        Me.Controls.Add(Me.ClassificationDropDown)
        Me.Controls.Add(Me.OperationalStatusLabel)
        Me.Controls.Add(Me.OperationalDropDown)
        Me.Controls.Add(Me.SicCodeLabel)
        Me.Controls.Add(Me.SicCode)
        Me.Controls.Add(Me.AirProgramCodes)
        Me.Controls.Add(Me.ModifiedDescDisplay)
        Me.Controls.Add(Me.FacilityNameDisplay)
        Me.Controls.Add(Me.AirsNumberDisplay)
        Me.Controls.Add(Me.SaveChangesButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(628, 500)
        Me.Name = "IAIPEditHeaderData"
        Me.Text = "Edit Header Data"
        CType(Me.FacilityHistoryDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AirProgramCodes.ResumeLayout(False)
        Me.AirProgramCodes.PerformLayout()
        Me.NonattainmentStatuses.ResumeLayout(False)
        Me.NonattainmentStatuses.PerformLayout()
        Me.AirProgramClassifications.ResumeLayout(False)
        Me.AirProgramClassifications.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FacilityDescription As System.Windows.Forms.TextBox
    Friend WithEvents FacilityDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents FacilityHistoryDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Comments As System.Windows.Forms.TextBox
    Friend WithEvents CommentsLabel As System.Windows.Forms.Label
    Friend WithEvents PermitRevocationDateLabel As System.Windows.Forms.Label
    Friend WithEvents StartupDateLabel As System.Windows.Forms.Label
    Friend WithEvents StartUpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents ShutdownDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents ClassificationLabel As System.Windows.Forms.Label
    Friend WithEvents ClassificationDropDown As System.Windows.Forms.ComboBox
    Friend WithEvents OperationalStatusLabel As System.Windows.Forms.Label
    Friend WithEvents OperationalDropDown As System.Windows.Forms.ComboBox
    Friend WithEvents SicCodeLabel As System.Windows.Forms.Label
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
    Friend WithEvents OneHourOzoneDropDown As System.Windows.Forms.ComboBox
    Friend WithEvents PmFineDropDown As System.Windows.Forms.ComboBox
    Friend WithEvents EightHourOzoneDropDown As System.Windows.Forms.ComboBox
    Friend WithEvents ApcRmp As System.Windows.Forms.CheckBox
    Friend WithEvents NaicsCodeLabel As System.Windows.Forms.Label
    Friend WithEvents NaicsCode As System.Windows.Forms.TextBox
    Friend WithEvents RmpIdLabel As System.Windows.Forms.Label
    Friend WithEvents RmpId As System.Windows.Forms.MaskedTextBox
    Friend WithEvents SaveChangesButton As System.Windows.Forms.Button
    Friend WithEvents CancelEditButton As System.Windows.Forms.Button
    Friend WithEvents EditData As System.Windows.Forms.CheckBox
    Friend WithEvents AirProgramClassifications As System.Windows.Forms.GroupBox
End Class
