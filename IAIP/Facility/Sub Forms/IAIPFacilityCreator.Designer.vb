<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPFacilityCreator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPFacilityCreator))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GBFacilityInformation = New System.Windows.Forms.GroupBox()
        Me.llbOpenWebpage = New System.Windows.Forms.LinkLabel()
        Me.mtbFacilityLongitude = New System.Windows.Forms.MaskedTextBox()
        Me.mtbFacilityLatitude = New System.Windows.Forms.MaskedTextBox()
        Me.mtbCDSZipCode = New System.Windows.Forms.MaskedTextBox()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.txtCDSStreetAddress = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCDSFacilityName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCDSCity = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCDSState = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSaveNewFacility = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnEditFacilityData = New System.Windows.Forms.Button()
        Me.GBContactInformation = New System.Windows.Forms.GroupBox()
        Me.txtContactPhoneNumber = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtFacilityComments = New System.Windows.Forms.TextBox()
        Me.txtContactPedigree = New System.Windows.Forms.TextBox()
        Me.txtContactSocialTitle = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtContactLastName = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtContactFirstName = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtContactTitle = New System.Windows.Forms.TextBox()
        Me.GBAirProgramCodes = New System.Windows.Forms.GroupBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.mtbRiskManagementNumber = New System.Windows.Forms.MaskedTextBox()
        Me.chbCDS_14 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_15 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_7 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_4 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_13 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_3 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_12 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_9 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_10 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_2 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_6 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_1 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_5 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_11 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_8 = New System.Windows.Forms.CheckBox()
        Me.GBHeaderData = New System.Windows.Forms.GroupBox()
        Me.mtbCDSNAICSCode = New System.Windows.Forms.MaskedTextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtCDSRegionCode = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.mtbCDSSICCode = New System.Windows.Forms.MaskedTextBox()
        Me.cboCDSOperationalStatus = New System.Windows.Forms.ComboBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.cboCDSClassCode = New System.Windows.Forms.ComboBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtFacilityDescription = New System.Windows.Forms.TextBox()
        Me.GBMailingLocation = New System.Windows.Forms.GroupBox()
        Me.mtbMailingZipCode = New System.Windows.Forms.MaskedTextBox()
        Me.txtMailingState = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtMailingCity = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtMailingAddress = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtApplicationNumber = New Iaip.CueTextBox()
        Me.btnPreLoadNewFacility = New System.Windows.Forms.Button()
        Me.cboCounty = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCDSAIRSNumber = New System.Windows.Forms.TextBox()
        Me.TCFacilityTools = New System.Windows.Forms.TabControl()
        Me.TPCreateNewFacility = New System.Windows.Forms.TabPage()
        Me.TPApproveNewFacility = New System.Windows.Forms.TabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtCountFacilities = New System.Windows.Forms.TextBox()
        Me.btnFilterNewFacilities = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.chbFilterNewFacilities = New System.Windows.Forms.CheckBox()
        Me.dtpEndFilter = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartFilter = New System.Windows.Forms.DateTimePicker()
        Me.lblValidationCount = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtApprovialComments = New System.Windows.Forms.TextBox()
        Me.txtStreetAddress = New System.Windows.Forms.TextBox()
        Me.dgvValidatingAIRS = New System.Windows.Forms.DataGridView()
        Me.btnValidateFacility = New System.Windows.Forms.Button()
        Me.txtSSPPComments = New System.Windows.Forms.TextBox()
        Me.btnSaveSSPPApproval = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chbSSPPSignOff = New System.Windows.Forms.CheckBox()
        Me.DTPSSPPApproveDate = New System.Windows.Forms.DateTimePicker()
        Me.txtSSPPApprover = New System.Windows.Forms.TextBox()
        Me.txtSSCPComments = New System.Windows.Forms.TextBox()
        Me.btnSaveSSCPApproval = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chbSSCPSignOff = New System.Windows.Forms.CheckBox()
        Me.btnRemoveFromPlatform = New System.Windows.Forms.Button()
        Me.DTPSSCPApproveDate = New System.Windows.Forms.DateTimePicker()
        Me.btnSubmitFacilityToAFS = New System.Windows.Forms.Button()
        Me.txtSSCPApprover = New System.Windows.Forms.TextBox()
        Me.btnViewFacility = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNewFacilityName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtNewAIRSNumber = New System.Windows.Forms.TextBox()
        Me.dgvVerifyNewFacilities = New System.Windows.Forms.DataGridView()
        Me.TPDeleteFacility = New System.Windows.Forms.TabPage()
        Me.lblFacilityCannotBeDeletedOrDeactivated = New System.Windows.Forms.Label()
        Me.AirsNumberToRemove = New System.Windows.Forms.TextBox()
        Me.FacilityLongDisplay = New System.Windows.Forms.Label()
        Me.btnDeleteAirsNumber = New System.Windows.Forms.Button()
        Me.btnDeactivateFacility = New System.Windows.Forms.Button()
        Me.lblFacilityCannotBeDeleted = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.AirsNumberToDeleteLabel = New System.Windows.Forms.Label()
        Me.TPDeactivatedFacilities = New System.Windows.Forms.TabPage()
        Me.dgvDeactivatedFacilities = New Iaip.IaipDataGridView()
        Me.btnRefreshDeactivatedFacilities = New System.Windows.Forms.Button()
        Me.GBFacilityInformation.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GBContactInformation.SuspendLayout()
        Me.GBAirProgramCodes.SuspendLayout()
        Me.GBHeaderData.SuspendLayout()
        Me.GBMailingLocation.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TCFacilityTools.SuspendLayout()
        Me.TPCreateNewFacility.SuspendLayout()
        Me.TPApproveNewFacility.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgvValidatingAIRS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVerifyNewFacilities, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPDeleteFacility.SuspendLayout()
        Me.TPDeactivatedFacilities.SuspendLayout()
        CType(Me.dgvDeactivatedFacilities, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GBFacilityInformation
        '
        Me.GBFacilityInformation.Controls.Add(Me.llbOpenWebpage)
        Me.GBFacilityInformation.Controls.Add(Me.mtbFacilityLongitude)
        Me.GBFacilityInformation.Controls.Add(Me.mtbFacilityLatitude)
        Me.GBFacilityInformation.Controls.Add(Me.mtbCDSZipCode)
        Me.GBFacilityInformation.Controls.Add(Me.Label103)
        Me.GBFacilityInformation.Controls.Add(Me.Label102)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSStreetAddress)
        Me.GBFacilityInformation.Controls.Add(Me.Label5)
        Me.GBFacilityInformation.Controls.Add(Me.Label9)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSFacilityName)
        Me.GBFacilityInformation.Controls.Add(Me.Label7)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSCity)
        Me.GBFacilityInformation.Controls.Add(Me.Label8)
        Me.GBFacilityInformation.Controls.Add(Me.Label10)
        Me.GBFacilityInformation.Controls.Add(Me.txtCDSState)
        Me.GBFacilityInformation.Controls.Add(Me.Label27)
        Me.GBFacilityInformation.Controls.Add(Me.Label28)
        Me.GBFacilityInformation.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBFacilityInformation.Location = New System.Drawing.Point(0, 38)
        Me.GBFacilityInformation.Name = "GBFacilityInformation"
        Me.GBFacilityInformation.Size = New System.Drawing.Size(732, 92)
        Me.GBFacilityInformation.TabIndex = 1
        Me.GBFacilityInformation.TabStop = False
        Me.GBFacilityInformation.Text = "Facility Information"
        '
        'llbOpenWebpage
        '
        Me.llbOpenWebpage.AutoSize = True
        Me.llbOpenWebpage.Location = New System.Drawing.Point(557, 64)
        Me.llbOpenWebpage.Name = "llbOpenWebpage"
        Me.llbOpenWebpage.Size = New System.Drawing.Size(98, 13)
        Me.llbOpenWebpage.TabIndex = 6
        Me.llbOpenWebpage.TabStop = True
        Me.llbOpenWebpage.Text = "Mapping Webpage"
        '
        'mtbFacilityLongitude
        '
        Me.mtbFacilityLongitude.Location = New System.Drawing.Point(482, 38)
        Me.mtbFacilityLongitude.Mask = "-00.000000"
        Me.mtbFacilityLongitude.Name = "mtbFacilityLongitude"
        Me.mtbFacilityLongitude.Size = New System.Drawing.Size(69, 20)
        Me.mtbFacilityLongitude.TabIndex = 5
        '
        'mtbFacilityLatitude
        '
        Me.mtbFacilityLatitude.Location = New System.Drawing.Point(482, 14)
        Me.mtbFacilityLatitude.Mask = "00.000000"
        Me.mtbFacilityLatitude.Name = "mtbFacilityLatitude"
        Me.mtbFacilityLatitude.Size = New System.Drawing.Size(69, 20)
        Me.mtbFacilityLatitude.TabIndex = 4
        '
        'mtbCDSZipCode
        '
        Me.mtbCDSZipCode.Location = New System.Drawing.Point(344, 61)
        Me.mtbCDSZipCode.Mask = "00000-9999"
        Me.mtbCDSZipCode.Name = "mtbCDSZipCode"
        Me.mtbCDSZipCode.Size = New System.Drawing.Size(72, 20)
        Me.mtbCDSZipCode.TabIndex = 3
        '
        'Label103
        '
        Me.Label103.AutoSize = True
        Me.Label103.Location = New System.Drawing.Point(557, 41)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(159, 13)
        Me.Label103.TabIndex = 173
        Me.Label103.Text = "(Range = 80.84111 -- 85.60444)"
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.Location = New System.Drawing.Point(557, 17)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(147, 13)
        Me.Label102.TabIndex = 171
        Me.Label102.Text = "(Range = 30.3555 -- 35.0000)"
        '
        'txtCDSStreetAddress
        '
        Me.txtCDSStreetAddress.Location = New System.Drawing.Point(104, 38)
        Me.txtCDSStreetAddress.MaxLength = 100
        Me.txtCDSStreetAddress.Name = "txtCDSStreetAddress"
        Me.txtCDSStreetAddress.Size = New System.Drawing.Size(312, 20)
        Me.txtCDSStreetAddress.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(218, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "State"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(28, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Facility Name"
        '
        'txtCDSFacilityName
        '
        Me.txtCDSFacilityName.Location = New System.Drawing.Point(104, 14)
        Me.txtCDSFacilityName.MaxLength = 100
        Me.txtCDSFacilityName.Name = "txtCDSFacilityName"
        Me.txtCDSFacilityName.Size = New System.Drawing.Size(312, 20)
        Me.txtCDSFacilityName.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(74, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "City"
        '
        'txtCDSCity
        '
        Me.txtCDSCity.Location = New System.Drawing.Point(104, 61)
        Me.txtCDSCity.MaxLength = 50
        Me.txtCDSCity.Name = "txtCDSCity"
        Me.txtCDSCity.Size = New System.Drawing.Size(108, 20)
        Me.txtCDSCity.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(22, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Street Address"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(288, 64)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "Zip Code"
        '
        'txtCDSState
        '
        Me.txtCDSState.Location = New System.Drawing.Point(256, 61)
        Me.txtCDSState.MaxLength = 4
        Me.txtCDSState.Name = "txtCDSState"
        Me.txtCDSState.ReadOnly = True
        Me.txtCDSState.Size = New System.Drawing.Size(24, 20)
        Me.txtCDSState.TabIndex = 7
        Me.txtCDSState.TabStop = False
        Me.txtCDSState.Text = "GA"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(422, 41)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(54, 13)
        Me.Label27.TabIndex = 156
        Me.Label27.Text = "Longitude"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(422, 17)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(45, 13)
        Me.Label28.TabIndex = 155
        Me.Label28.Text = "Latitude"
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.Controls.Add(Me.btnSaveNewFacility)
        Me.Panel4.Controls.Add(Me.btnClear)
        Me.Panel4.Controls.Add(Me.btnEditFacilityData)
        Me.Panel4.Controls.Add(Me.GBContactInformation)
        Me.Panel4.Controls.Add(Me.GBAirProgramCodes)
        Me.Panel4.Controls.Add(Me.GBHeaderData)
        Me.Panel4.Controls.Add(Me.GBMailingLocation)
        Me.Panel4.Controls.Add(Me.GBFacilityInformation)
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(732, 513)
        Me.Panel4.TabIndex = 0
        '
        'btnSaveNewFacility
        '
        Me.btnSaveNewFacility.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveNewFacility.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.btnSaveNewFacility.Location = New System.Drawing.Point(5, 485)
        Me.btnSaveNewFacility.Name = "btnSaveNewFacility"
        Me.btnSaveNewFacility.Size = New System.Drawing.Size(143, 24)
        Me.btnSaveNewFacility.TabIndex = 11
        Me.btnSaveNewFacility.Text = "Save New Facility"
        Me.btnSaveNewFacility.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSaveNewFacility.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(635, 484)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(89, 24)
        Me.btnClear.TabIndex = 12
        Me.btnClear.Text = "Clear Form"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnEditFacilityData
        '
        Me.btnEditFacilityData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditFacilityData.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.btnEditFacilityData.Location = New System.Drawing.Point(5, 485)
        Me.btnEditFacilityData.Name = "btnEditFacilityData"
        Me.btnEditFacilityData.Size = New System.Drawing.Size(143, 24)
        Me.btnEditFacilityData.TabIndex = 376
        Me.btnEditFacilityData.Text = "Update Facility"
        Me.btnEditFacilityData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditFacilityData.UseVisualStyleBackColor = True
        Me.btnEditFacilityData.Visible = False
        '
        'GBContactInformation
        '
        Me.GBContactInformation.Controls.Add(Me.txtContactPhoneNumber)
        Me.GBContactInformation.Controls.Add(Me.Label14)
        Me.GBContactInformation.Controls.Add(Me.txtFacilityComments)
        Me.GBContactInformation.Controls.Add(Me.txtContactPedigree)
        Me.GBContactInformation.Controls.Add(Me.txtContactSocialTitle)
        Me.GBContactInformation.Controls.Add(Me.Label33)
        Me.GBContactInformation.Controls.Add(Me.Label34)
        Me.GBContactInformation.Controls.Add(Me.txtContactLastName)
        Me.GBContactInformation.Controls.Add(Me.Label35)
        Me.GBContactInformation.Controls.Add(Me.txtContactFirstName)
        Me.GBContactInformation.Controls.Add(Me.Label36)
        Me.GBContactInformation.Controls.Add(Me.Label22)
        Me.GBContactInformation.Controls.Add(Me.Label30)
        Me.GBContactInformation.Controls.Add(Me.txtContactTitle)
        Me.GBContactInformation.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBContactInformation.Location = New System.Drawing.Point(0, 357)
        Me.GBContactInformation.Name = "GBContactInformation"
        Me.GBContactInformation.Size = New System.Drawing.Size(732, 122)
        Me.GBContactInformation.TabIndex = 5
        Me.GBContactInformation.TabStop = False
        Me.GBContactInformation.Text = "Contact Information"
        '
        'txtContactPhoneNumber
        '
        Me.txtContactPhoneNumber.Location = New System.Drawing.Point(362, 40)
        Me.txtContactPhoneNumber.Name = "txtContactPhoneNumber"
        Me.txtContactPhoneNumber.Size = New System.Drawing.Size(189, 20)
        Me.txtContactPhoneNumber.TabIndex = 376
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(14, 68)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 13)
        Me.Label14.TabIndex = 375
        Me.Label14.Text = "Comments"
        '
        'txtFacilityComments
        '
        Me.txtFacilityComments.Location = New System.Drawing.Point(76, 65)
        Me.txtFacilityComments.Multiline = True
        Me.txtFacilityComments.Name = "txtFacilityComments"
        Me.txtFacilityComments.Size = New System.Drawing.Size(496, 47)
        Me.txtFacilityComments.TabIndex = 7
        '
        'txtContactPedigree
        '
        Me.txtContactPedigree.Location = New System.Drawing.Point(632, 17)
        Me.txtContactPedigree.Name = "txtContactPedigree"
        Me.txtContactPedigree.Size = New System.Drawing.Size(72, 20)
        Me.txtContactPedigree.TabIndex = 3
        '
        'txtContactSocialTitle
        '
        Me.txtContactSocialTitle.Location = New System.Drawing.Point(76, 17)
        Me.txtContactSocialTitle.Name = "txtContactSocialTitle"
        Me.txtContactSocialTitle.Size = New System.Drawing.Size(72, 20)
        Me.txtContactSocialTitle.TabIndex = 0
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(14, 21)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(59, 13)
        Me.Label33.TabIndex = 371
        Me.Label33.Text = "Social Title"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(580, 21)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(33, 13)
        Me.Label34.TabIndex = 370
        Me.Label34.Text = "Suffix"
        '
        'txtContactLastName
        '
        Me.txtContactLastName.Location = New System.Drawing.Point(423, 17)
        Me.txtContactLastName.Name = "txtContactLastName"
        Me.txtContactLastName.Size = New System.Drawing.Size(151, 20)
        Me.txtContactLastName.TabIndex = 2
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(364, 21)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(58, 13)
        Me.Label35.TabIndex = 369
        Me.Label35.Text = "Last Name"
        '
        'txtContactFirstName
        '
        Me.txtContactFirstName.Location = New System.Drawing.Point(212, 17)
        Me.txtContactFirstName.Name = "txtContactFirstName"
        Me.txtContactFirstName.Size = New System.Drawing.Size(151, 20)
        Me.txtContactFirstName.TabIndex = 1
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(156, 21)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(57, 13)
        Me.Label36.TabIndex = 368
        Me.Label36.Text = "First Name"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(278, 43)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(78, 13)
        Me.Label22.TabIndex = 130
        Me.Label22.Text = "Phone Number"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(46, 43)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(27, 13)
        Me.Label30.TabIndex = 128
        Me.Label30.Text = "Title"
        '
        'txtContactTitle
        '
        Me.txtContactTitle.Location = New System.Drawing.Point(76, 40)
        Me.txtContactTitle.Name = "txtContactTitle"
        Me.txtContactTitle.Size = New System.Drawing.Size(180, 20)
        Me.txtContactTitle.TabIndex = 4
        '
        'GBAirProgramCodes
        '
        Me.GBAirProgramCodes.Controls.Add(Me.Label32)
        Me.GBAirProgramCodes.Controls.Add(Me.mtbRiskManagementNumber)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_14)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_15)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_7)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_4)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_13)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_3)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_12)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_9)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_10)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_2)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_6)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_1)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_5)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_11)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_8)
        Me.GBAirProgramCodes.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBAirProgramCodes.Location = New System.Drawing.Point(0, 286)
        Me.GBAirProgramCodes.Name = "GBAirProgramCodes"
        Me.GBAirProgramCodes.Size = New System.Drawing.Size(732, 71)
        Me.GBAirProgramCodes.TabIndex = 4
        Me.GBAirProgramCodes.TabStop = False
        Me.GBAirProgramCodes.Text = "Air Program Codes && Pollutants (Select All that Apply)"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(601, 16)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(45, 13)
        Me.Label32.TabIndex = 161
        Me.Label32.Text = "RMP ID"
        '
        'mtbRiskManagementNumber
        '
        Me.mtbRiskManagementNumber.Location = New System.Drawing.Point(604, 33)
        Me.mtbRiskManagementNumber.Mask = "0000-0000-0000"
        Me.mtbRiskManagementNumber.Name = "mtbRiskManagementNumber"
        Me.mtbRiskManagementNumber.Size = New System.Drawing.Size(100, 20)
        Me.mtbRiskManagementNumber.TabIndex = 16
        '
        'chbCDS_14
        '
        Me.chbCDS_14.AutoSize = True
        Me.chbCDS_14.Location = New System.Drawing.Point(434, 35)
        Me.chbCDS_14.Name = "chbCDS_14"
        Me.chbCDS_14.Size = New System.Drawing.Size(153, 17)
        Me.chbCDS_14.TabIndex = 14
        Me.chbCDS_14.Text = "R - Risk Management Plan"
        '
        'chbCDS_15
        '
        Me.chbCDS_15.AutoSize = True
        Me.chbCDS_15.Location = New System.Drawing.Point(434, 51)
        Me.chbCDS_15.Name = "chbCDS_15"
        Me.chbCDS_15.Size = New System.Drawing.Size(128, 17)
        Me.chbCDS_15.TabIndex = 15
        Me.chbCDS_15.Text = "G - Green House Gas"
        Me.chbCDS_15.Visible = False
        '
        'chbCDS_7
        '
        Me.chbCDS_7.AutoSize = True
        Me.chbCDS_7.Location = New System.Drawing.Point(219, 19)
        Me.chbCDS_7.Name = "chbCDS_7"
        Me.chbCDS_7.Size = New System.Drawing.Size(85, 17)
        Me.chbCDS_7.TabIndex = 7
        Me.chbCDS_7.Text = "8 - NESHAP"
        '
        'chbCDS_4
        '
        Me.chbCDS_4.AutoSize = True
        Me.chbCDS_4.Location = New System.Drawing.Point(107, 19)
        Me.chbCDS_4.Name = "chbCDS_4"
        Me.chbCDS_4.Size = New System.Drawing.Size(106, 17)
        Me.chbCDS_4.TabIndex = 4
        Me.chbCDS_4.Text = "4 - CFC Tracking"
        '
        'chbCDS_13
        '
        Me.chbCDS_13.AutoSize = True
        Me.chbCDS_13.Location = New System.Drawing.Point(434, 19)
        Me.chbCDS_13.Name = "chbCDS_13"
        Me.chbCDS_13.Size = New System.Drawing.Size(72, 17)
        Me.chbCDS_13.TabIndex = 13
        Me.chbCDS_13.Text = "V - Title V"
        '
        'chbCDS_3
        '
        Me.chbCDS_3.AutoSize = True
        Me.chbCDS_3.Location = New System.Drawing.Point(20, 51)
        Me.chbCDS_3.Name = "chbCDS_3"
        Me.chbCDS_3.Size = New System.Drawing.Size(85, 17)
        Me.chbCDS_3.TabIndex = 3
        Me.chbCDS_3.Text = "3 - Non Fed."
        '
        'chbCDS_12
        '
        Me.chbCDS_12.AutoSize = True
        Me.chbCDS_12.Location = New System.Drawing.Point(310, 51)
        Me.chbCDS_12.Name = "chbCDS_12"
        Me.chbCDS_12.Size = New System.Drawing.Size(74, 17)
        Me.chbCDS_12.TabIndex = 12
        Me.chbCDS_12.Text = "M - MACT"
        '
        'chbCDS_9
        '
        Me.chbCDS_9.AutoSize = True
        Me.chbCDS_9.Location = New System.Drawing.Point(219, 51)
        Me.chbCDS_9.Name = "chbCDS_9"
        Me.chbCDS_9.Size = New System.Drawing.Size(79, 17)
        Me.chbCDS_9.TabIndex = 9
        Me.chbCDS_9.Text = "F - FESOP "
        '
        'chbCDS_10
        '
        Me.chbCDS_10.AutoSize = True
        Me.chbCDS_10.Location = New System.Drawing.Point(310, 19)
        Me.chbCDS_10.Name = "chbCDS_10"
        Me.chbCDS_10.Size = New System.Drawing.Size(99, 17)
        Me.chbCDS_10.TabIndex = 10
        Me.chbCDS_10.Text = "A - Acid Precip."
        '
        'chbCDS_2
        '
        Me.chbCDS_2.AutoSize = True
        Me.chbCDS_2.Location = New System.Drawing.Point(20, 35)
        Me.chbCDS_2.Name = "chbCDS_2"
        Me.chbCDS_2.Size = New System.Drawing.Size(82, 17)
        Me.chbCDS_2.TabIndex = 2
        Me.chbCDS_2.Text = "1 - Fed. SIP"
        '
        'chbCDS_6
        '
        Me.chbCDS_6.AutoSize = True
        Me.chbCDS_6.Location = New System.Drawing.Point(107, 51)
        Me.chbCDS_6.Name = "chbCDS_6"
        Me.chbCDS_6.Size = New System.Drawing.Size(64, 17)
        Me.chbCDS_6.TabIndex = 6
        Me.chbCDS_6.Text = "7 - NSR"
        '
        'chbCDS_1
        '
        Me.chbCDS_1.AutoSize = True
        Me.chbCDS_1.Checked = True
        Me.chbCDS_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbCDS_1.Location = New System.Drawing.Point(20, 19)
        Me.chbCDS_1.Name = "chbCDS_1"
        Me.chbCDS_1.Size = New System.Drawing.Size(58, 17)
        Me.chbCDS_1.TabIndex = 1
        Me.chbCDS_1.Text = "0 - SIP"
        '
        'chbCDS_5
        '
        Me.chbCDS_5.AutoSize = True
        Me.chbCDS_5.Location = New System.Drawing.Point(107, 35)
        Me.chbCDS_5.Name = "chbCDS_5"
        Me.chbCDS_5.Size = New System.Drawing.Size(63, 17)
        Me.chbCDS_5.TabIndex = 5
        Me.chbCDS_5.Text = "6 - PSD"
        '
        'chbCDS_11
        '
        Me.chbCDS_11.AutoSize = True
        Me.chbCDS_11.Location = New System.Drawing.Point(310, 35)
        Me.chbCDS_11.Name = "chbCDS_11"
        Me.chbCDS_11.Size = New System.Drawing.Size(116, 17)
        Me.chbCDS_11.TabIndex = 11
        Me.chbCDS_11.Text = "I - Native American"
        '
        'chbCDS_8
        '
        Me.chbCDS_8.AutoSize = True
        Me.chbCDS_8.Location = New System.Drawing.Point(219, 35)
        Me.chbCDS_8.Name = "chbCDS_8"
        Me.chbCDS_8.Size = New System.Drawing.Size(70, 17)
        Me.chbCDS_8.TabIndex = 8
        Me.chbCDS_8.Text = "9 - NSPS"
        '
        'GBHeaderData
        '
        Me.GBHeaderData.Controls.Add(Me.mtbCDSNAICSCode)
        Me.GBHeaderData.Controls.Add(Me.Label37)
        Me.GBHeaderData.Controls.Add(Me.txtCDSRegionCode)
        Me.GBHeaderData.Controls.Add(Me.Label21)
        Me.GBHeaderData.Controls.Add(Me.Label6)
        Me.GBHeaderData.Controls.Add(Me.mtbCDSSICCode)
        Me.GBHeaderData.Controls.Add(Me.cboCDSOperationalStatus)
        Me.GBHeaderData.Controls.Add(Me.Label51)
        Me.GBHeaderData.Controls.Add(Me.cboCDSClassCode)
        Me.GBHeaderData.Controls.Add(Me.Label49)
        Me.GBHeaderData.Controls.Add(Me.Label42)
        Me.GBHeaderData.Controls.Add(Me.txtFacilityDescription)
        Me.GBHeaderData.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBHeaderData.Location = New System.Drawing.Point(0, 194)
        Me.GBHeaderData.Name = "GBHeaderData"
        Me.GBHeaderData.Size = New System.Drawing.Size(732, 92)
        Me.GBHeaderData.TabIndex = 3
        Me.GBHeaderData.TabStop = False
        Me.GBHeaderData.Text = "Header Data"
        '
        'mtbCDSNAICSCode
        '
        Me.mtbCDSNAICSCode.Location = New System.Drawing.Point(104, 41)
        Me.mtbCDSNAICSCode.Mask = "000000"
        Me.mtbCDSNAICSCode.Name = "mtbCDSNAICSCode"
        Me.mtbCDSNAICSCode.Size = New System.Drawing.Size(44, 20)
        Me.mtbCDSNAICSCode.TabIndex = 1
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(24, 44)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(67, 13)
        Me.Label37.TabIndex = 174
        Me.Label37.Text = "NAICS Code"
        '
        'txtCDSRegionCode
        '
        Me.txtCDSRegionCode.Location = New System.Drawing.Point(571, 41)
        Me.txtCDSRegionCode.MaxLength = 4
        Me.txtCDSRegionCode.Name = "txtCDSRegionCode"
        Me.txtCDSRegionCode.ReadOnly = True
        Me.txtCDSRegionCode.Size = New System.Drawing.Size(153, 20)
        Me.txtCDSRegionCode.TabIndex = 171
        Me.txtCDSRegionCode.TabStop = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(494, 43)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(69, 13)
        Me.Label21.TabIndex = 172
        Me.Label21.Text = "Region Code"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 170
        Me.Label6.Text = "Plant Description"
        '
        'mtbCDSSICCode
        '
        Me.mtbCDSSICCode.Location = New System.Drawing.Point(104, 13)
        Me.mtbCDSSICCode.Mask = "0000"
        Me.mtbCDSSICCode.Name = "mtbCDSSICCode"
        Me.mtbCDSSICCode.Size = New System.Drawing.Size(44, 20)
        Me.mtbCDSSICCode.TabIndex = 0
        '
        'cboCDSOperationalStatus
        '
        Me.cboCDSOperationalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCDSOperationalStatus.Location = New System.Drawing.Point(270, 13)
        Me.cboCDSOperationalStatus.Name = "cboCDSOperationalStatus"
        Me.cboCDSOperationalStatus.Size = New System.Drawing.Size(153, 21)
        Me.cboCDSOperationalStatus.TabIndex = 2
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(170, 17)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(94, 13)
        Me.Label51.TabIndex = 168
        Me.Label51.Text = "Operational Status"
        '
        'cboCDSClassCode
        '
        Me.cboCDSClassCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCDSClassCode.Location = New System.Drawing.Point(571, 13)
        Me.cboCDSClassCode.Name = "cboCDSClassCode"
        Me.cboCDSClassCode.Size = New System.Drawing.Size(153, 21)
        Me.cboCDSClassCode.TabIndex = 3
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(464, 17)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(95, 13)
        Me.Label49.TabIndex = 164
        Me.Label49.Text = "Facility Class Code"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(44, 17)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(52, 13)
        Me.Label42.TabIndex = 161
        Me.Label42.Text = "SIC Code"
        '
        'txtFacilityDescription
        '
        Me.txtFacilityDescription.Location = New System.Drawing.Point(104, 66)
        Me.txtFacilityDescription.MaxLength = 4000
        Me.txtFacilityDescription.Name = "txtFacilityDescription"
        Me.txtFacilityDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityDescription.Size = New System.Drawing.Size(385, 20)
        Me.txtFacilityDescription.TabIndex = 4
        '
        'GBMailingLocation
        '
        Me.GBMailingLocation.Controls.Add(Me.mtbMailingZipCode)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingState)
        Me.GBMailingLocation.Controls.Add(Me.Label18)
        Me.GBMailingLocation.Controls.Add(Me.Label19)
        Me.GBMailingLocation.Controls.Add(Me.Label20)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingCity)
        Me.GBMailingLocation.Controls.Add(Me.Label24)
        Me.GBMailingLocation.Controls.Add(Me.txtMailingAddress)
        Me.GBMailingLocation.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBMailingLocation.Location = New System.Drawing.Point(0, 130)
        Me.GBMailingLocation.Name = "GBMailingLocation"
        Me.GBMailingLocation.Size = New System.Drawing.Size(732, 64)
        Me.GBMailingLocation.TabIndex = 2
        Me.GBMailingLocation.TabStop = False
        Me.GBMailingLocation.Text = "Mailing Location"
        '
        'mtbMailingZipCode
        '
        Me.mtbMailingZipCode.Location = New System.Drawing.Point(402, 36)
        Me.mtbMailingZipCode.Mask = "00000-9999"
        Me.mtbMailingZipCode.Name = "mtbMailingZipCode"
        Me.mtbMailingZipCode.Size = New System.Drawing.Size(72, 20)
        Me.mtbMailingZipCode.TabIndex = 3
        '
        'txtMailingState
        '
        Me.txtMailingState.Location = New System.Drawing.Point(301, 36)
        Me.txtMailingState.MaxLength = 2
        Me.txtMailingState.Name = "txtMailingState"
        Me.txtMailingState.Size = New System.Drawing.Size(24, 20)
        Me.txtMailingState.TabIndex = 2
        Me.txtMailingState.Text = "GA"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(338, 40)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 13)
        Me.Label18.TabIndex = 162
        Me.Label18.Text = "Mailing Zip"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(267, 40)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(32, 13)
        Me.Label19.TabIndex = 161
        Me.Label19.Text = "State"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(38, 39)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(60, 13)
        Me.Label20.TabIndex = 160
        Me.Label20.Text = "Mailing City"
        '
        'txtMailingCity
        '
        Me.txtMailingCity.Location = New System.Drawing.Point(104, 37)
        Me.txtMailingCity.Name = "txtMailingCity"
        Me.txtMailingCity.Size = New System.Drawing.Size(160, 20)
        Me.txtMailingCity.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(17, 17)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(81, 13)
        Me.Label24.TabIndex = 159
        Me.Label24.Text = "Mailing Address"
        '
        'txtMailingAddress
        '
        Me.txtMailingAddress.Location = New System.Drawing.Point(104, 14)
        Me.txtMailingAddress.Name = "txtMailingAddress"
        Me.txtMailingAddress.Size = New System.Drawing.Size(370, 20)
        Me.txtMailingAddress.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtApplicationNumber)
        Me.GroupBox1.Controls.Add(Me.btnPreLoadNewFacility)
        Me.GroupBox1.Controls.Add(Me.cboCounty)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtCDSAIRSNumber)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(732, 38)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Cue = "App No."
        Me.txtApplicationNumber.Location = New System.Drawing.Point(543, 10)
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtApplicationNumber.TabIndex = 2
        '
        'btnPreLoadNewFacility
        '
        Me.btnPreLoadNewFacility.Location = New System.Drawing.Point(649, 8)
        Me.btnPreLoadNewFacility.Name = "btnPreLoadNewFacility"
        Me.btnPreLoadNewFacility.Size = New System.Drawing.Size(75, 23)
        Me.btnPreLoadNewFacility.TabIndex = 3
        Me.btnPreLoadNewFacility.Text = "Preload"
        Me.btnPreLoadNewFacility.UseVisualStyleBackColor = True
        '
        'cboCounty
        '
        Me.cboCounty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboCounty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCounty.Location = New System.Drawing.Point(104, 10)
        Me.cboCounty.Name = "cboCounty"
        Me.cboCounty.Size = New System.Drawing.Size(123, 21)
        Me.cboCounty.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 13)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 13)
        Me.Label11.TabIndex = 166
        Me.Label11.Text = "Select A County"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(248, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "AIRS Number:"
        '
        'txtCDSAIRSNumber
        '
        Me.txtCDSAIRSNumber.Location = New System.Drawing.Point(328, 12)
        Me.txtCDSAIRSNumber.MaxLength = 12
        Me.txtCDSAIRSNumber.Name = "txtCDSAIRSNumber"
        Me.txtCDSAIRSNumber.ReadOnly = True
        Me.txtCDSAIRSNumber.Size = New System.Drawing.Size(88, 20)
        Me.txtCDSAIRSNumber.TabIndex = 1
        '
        'TCFacilityTools
        '
        Me.TCFacilityTools.Controls.Add(Me.TPCreateNewFacility)
        Me.TCFacilityTools.Controls.Add(Me.TPApproveNewFacility)
        Me.TCFacilityTools.Controls.Add(Me.TPDeleteFacility)
        Me.TCFacilityTools.Controls.Add(Me.TPDeactivatedFacilities)
        Me.TCFacilityTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCFacilityTools.Location = New System.Drawing.Point(0, 0)
        Me.TCFacilityTools.MinimumSize = New System.Drawing.Size(746, 545)
        Me.TCFacilityTools.Name = "TCFacilityTools"
        Me.TCFacilityTools.SelectedIndex = 0
        Me.TCFacilityTools.Size = New System.Drawing.Size(746, 545)
        Me.TCFacilityTools.TabIndex = 0
        '
        'TPCreateNewFacility
        '
        Me.TPCreateNewFacility.Controls.Add(Me.Panel4)
        Me.TPCreateNewFacility.Location = New System.Drawing.Point(4, 22)
        Me.TPCreateNewFacility.Name = "TPCreateNewFacility"
        Me.TPCreateNewFacility.Padding = New System.Windows.Forms.Padding(3)
        Me.TPCreateNewFacility.Size = New System.Drawing.Size(738, 519)
        Me.TPCreateNewFacility.TabIndex = 0
        Me.TPCreateNewFacility.Text = "Create New Facility"
        Me.TPCreateNewFacility.UseVisualStyleBackColor = True
        '
        'TPApproveNewFacility
        '
        Me.TPApproveNewFacility.Controls.Add(Me.Panel5)
        Me.TPApproveNewFacility.Controls.Add(Me.dgvVerifyNewFacilities)
        Me.TPApproveNewFacility.Location = New System.Drawing.Point(4, 22)
        Me.TPApproveNewFacility.Name = "TPApproveNewFacility"
        Me.TPApproveNewFacility.Padding = New System.Windows.Forms.Padding(3)
        Me.TPApproveNewFacility.Size = New System.Drawing.Size(738, 519)
        Me.TPApproveNewFacility.TabIndex = 1
        Me.TPApproveNewFacility.Text = "Approve New Facilities"
        Me.TPApproveNewFacility.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label31)
        Me.Panel5.Controls.Add(Me.Label29)
        Me.Panel5.Controls.Add(Me.txtCountFacilities)
        Me.Panel5.Controls.Add(Me.btnFilterNewFacilities)
        Me.Panel5.Controls.Add(Me.Label26)
        Me.Panel5.Controls.Add(Me.chbFilterNewFacilities)
        Me.Panel5.Controls.Add(Me.dtpEndFilter)
        Me.Panel5.Controls.Add(Me.dtpStartFilter)
        Me.Panel5.Controls.Add(Me.lblValidationCount)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Controls.Add(Me.txtApprovialComments)
        Me.Panel5.Controls.Add(Me.txtStreetAddress)
        Me.Panel5.Controls.Add(Me.dgvValidatingAIRS)
        Me.Panel5.Controls.Add(Me.btnValidateFacility)
        Me.Panel5.Controls.Add(Me.txtSSPPComments)
        Me.Panel5.Controls.Add(Me.btnSaveSSPPApproval)
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Controls.Add(Me.chbSSPPSignOff)
        Me.Panel5.Controls.Add(Me.DTPSSPPApproveDate)
        Me.Panel5.Controls.Add(Me.txtSSPPApprover)
        Me.Panel5.Controls.Add(Me.txtSSCPComments)
        Me.Panel5.Controls.Add(Me.btnSaveSSCPApproval)
        Me.Panel5.Controls.Add(Me.Label15)
        Me.Panel5.Controls.Add(Me.chbSSCPSignOff)
        Me.Panel5.Controls.Add(Me.btnRemoveFromPlatform)
        Me.Panel5.Controls.Add(Me.DTPSSCPApproveDate)
        Me.Panel5.Controls.Add(Me.btnSubmitFacilityToAFS)
        Me.Panel5.Controls.Add(Me.txtSSCPApprover)
        Me.Panel5.Controls.Add(Me.btnViewFacility)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.txtNewFacilityName)
        Me.Panel5.Controls.Add(Me.Label12)
        Me.Panel5.Controls.Add(Me.txtNewAIRSNumber)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 229)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(732, 287)
        Me.Panel5.TabIndex = 0
        '
        'Label31
        '
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label31.Location = New System.Drawing.Point(10, 29)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(750, 2)
        Me.Label31.TabIndex = 404
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(635, 8)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(35, 13)
        Me.Label29.TabIndex = 5
        Me.Label29.Text = "Count"
        '
        'txtCountFacilities
        '
        Me.txtCountFacilities.Location = New System.Drawing.Point(676, 5)
        Me.txtCountFacilities.MaxLength = 12
        Me.txtCountFacilities.Name = "txtCountFacilities"
        Me.txtCountFacilities.ReadOnly = True
        Me.txtCountFacilities.Size = New System.Drawing.Size(51, 20)
        Me.txtCountFacilities.TabIndex = 5
        '
        'btnFilterNewFacilities
        '
        Me.btnFilterNewFacilities.AutoSize = True
        Me.btnFilterNewFacilities.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterNewFacilities.Location = New System.Drawing.Point(396, 3)
        Me.btnFilterNewFacilities.Name = "btnFilterNewFacilities"
        Me.btnFilterNewFacilities.Size = New System.Drawing.Size(74, 23)
        Me.btnFilterNewFacilities.TabIndex = 3
        Me.btnFilterNewFacilities.Text = "Run Search"
        Me.btnFilterNewFacilities.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(250, 8)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(25, 13)
        Me.Label26.TabIndex = 399
        Me.Label26.Text = "and"
        '
        'chbFilterNewFacilities
        '
        Me.chbFilterNewFacilities.AutoSize = True
        Me.chbFilterNewFacilities.Location = New System.Drawing.Point(5, 7)
        Me.chbFilterNewFacilities.Name = "chbFilterNewFacilities"
        Me.chbFilterNewFacilities.Size = New System.Drawing.Size(133, 17)
        Me.chbFilterNewFacilities.TabIndex = 0
        Me.chbFilterNewFacilities.Text = "Filter Results between "
        Me.chbFilterNewFacilities.UseVisualStyleBackColor = True
        '
        'dtpEndFilter
        '
        Me.dtpEndFilter.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEndFilter.Enabled = False
        Me.dtpEndFilter.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndFilter.Location = New System.Drawing.Point(281, 6)
        Me.dtpEndFilter.Name = "dtpEndFilter"
        Me.dtpEndFilter.Size = New System.Drawing.Size(100, 20)
        Me.dtpEndFilter.TabIndex = 1
        Me.dtpEndFilter.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'dtpStartFilter
        '
        Me.dtpStartFilter.CustomFormat = "dd-MMM-yyyy"
        Me.dtpStartFilter.Enabled = False
        Me.dtpStartFilter.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartFilter.Location = New System.Drawing.Point(144, 6)
        Me.dtpStartFilter.Name = "dtpStartFilter"
        Me.dtpStartFilter.Size = New System.Drawing.Size(100, 20)
        Me.dtpStartFilter.TabIndex = 1
        Me.dtpStartFilter.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'lblValidationCount
        '
        Me.lblValidationCount.AutoSize = True
        Me.lblValidationCount.Location = New System.Drawing.Point(5, 226)
        Me.lblValidationCount.Name = "lblValidationCount"
        Me.lblValidationCount.Size = New System.Drawing.Size(0, 13)
        Me.lblValidationCount.TabIndex = 395
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(27, 64)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 13)
        Me.Label17.TabIndex = 393
        Me.Label17.Text = "Comments"
        '
        'txtApprovialComments
        '
        Me.txtApprovialComments.Location = New System.Drawing.Point(89, 61)
        Me.txtApprovialComments.MaxLength = 100
        Me.txtApprovialComments.Name = "txtApprovialComments"
        Me.txtApprovialComments.Size = New System.Drawing.Size(439, 20)
        Me.txtApprovialComments.TabIndex = 7
        '
        'txtStreetAddress
        '
        Me.txtStreetAddress.Location = New System.Drawing.Point(534, 35)
        Me.txtStreetAddress.Name = "txtStreetAddress"
        Me.txtStreetAddress.Size = New System.Drawing.Size(31, 20)
        Me.txtStreetAddress.TabIndex = 6
        Me.txtStreetAddress.Visible = False
        '
        'dgvValidatingAIRS
        '
        Me.dgvValidatingAIRS.AllowUserToAddRows = False
        Me.dgvValidatingAIRS.AllowUserToDeleteRows = False
        Me.dgvValidatingAIRS.AllowUserToResizeRows = False
        Me.dgvValidatingAIRS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvValidatingAIRS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvValidatingAIRS.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvValidatingAIRS.Location = New System.Drawing.Point(89, 187)
        Me.dgvValidatingAIRS.Name = "dgvValidatingAIRS"
        Me.dgvValidatingAIRS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvValidatingAIRS.Size = New System.Drawing.Size(638, 95)
        Me.dgvValidatingAIRS.TabIndex = 22
        '
        'btnValidateFacility
        '
        Me.btnValidateFacility.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnValidateFacility.Location = New System.Drawing.Point(5, 187)
        Me.btnValidateFacility.Name = "btnValidateFacility"
        Me.btnValidateFacility.Size = New System.Drawing.Size(78, 36)
        Me.btnValidateFacility.TabIndex = 21
        Me.btnValidateFacility.Text = "Find similar facilities"
        Me.btnValidateFacility.UseVisualStyleBackColor = True
        '
        'txtSSPPComments
        '
        Me.txtSSPPComments.Location = New System.Drawing.Point(426, 144)
        Me.txtSSPPComments.Multiline = True
        Me.txtSSPPComments.Name = "txtSSPPComments"
        Me.txtSSPPComments.Size = New System.Drawing.Size(224, 37)
        Me.txtSSPPComments.TabIndex = 16
        '
        'btnSaveSSPPApproval
        '
        Me.btnSaveSSPPApproval.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveSSPPApproval.Location = New System.Drawing.Point(342, 144)
        Me.btnSaveSSPPApproval.Name = "btnSaveSSPPApproval"
        Me.btnSaveSSPPApproval.Size = New System.Drawing.Size(78, 23)
        Me.btnSaveSSPPApproval.TabIndex = 17
        Me.btnSaveSSPPApproval.Text = "Save SSPP"
        Me.btnSaveSSPPApproval.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(342, 121)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(78, 13)
        Me.Label16.TabIndex = 386
        Me.Label16.Text = "SSPP Approve"
        '
        'chbSSPPSignOff
        '
        Me.chbSSPPSignOff.AutoSize = True
        Me.chbSSPPSignOff.Location = New System.Drawing.Point(426, 95)
        Me.chbSSPPSignOff.Name = "chbSSPPSignOff"
        Me.chbSSPPSignOff.Size = New System.Drawing.Size(95, 17)
        Me.chbSSPPSignOff.TabIndex = 13
        Me.chbSSPPSignOff.Text = "SSPP Sign Off"
        Me.chbSSPPSignOff.UseVisualStyleBackColor = True
        '
        'DTPSSPPApproveDate
        '
        Me.DTPSSPPApproveDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSSPPApproveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSSPPApproveDate.Location = New System.Drawing.Point(550, 118)
        Me.DTPSSPPApproveDate.Name = "DTPSSPPApproveDate"
        Me.DTPSSPPApproveDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPSSPPApproveDate.TabIndex = 15
        Me.DTPSSPPApproveDate.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'txtSSPPApprover
        '
        Me.txtSSPPApprover.Location = New System.Drawing.Point(426, 118)
        Me.txtSSPPApprover.MaxLength = 12
        Me.txtSSPPApprover.Name = "txtSSPPApprover"
        Me.txtSSPPApprover.ReadOnly = True
        Me.txtSSPPApprover.Size = New System.Drawing.Size(118, 20)
        Me.txtSSPPApprover.TabIndex = 14
        '
        'txtSSCPComments
        '
        Me.txtSSCPComments.Location = New System.Drawing.Point(89, 144)
        Me.txtSSCPComments.Multiline = True
        Me.txtSSCPComments.Name = "txtSSCPComments"
        Me.txtSSCPComments.Size = New System.Drawing.Size(224, 37)
        Me.txtSSCPComments.TabIndex = 11
        '
        'btnSaveSSCPApproval
        '
        Me.btnSaveSSCPApproval.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveSSCPApproval.Location = New System.Drawing.Point(5, 144)
        Me.btnSaveSSCPApproval.Name = "btnSaveSSCPApproval"
        Me.btnSaveSSCPApproval.Size = New System.Drawing.Size(78, 23)
        Me.btnSaveSSCPApproval.TabIndex = 12
        Me.btnSaveSSCPApproval.Text = "Save SSCP"
        Me.btnSaveSSCPApproval.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(5, 121)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(78, 13)
        Me.Label15.TabIndex = 380
        Me.Label15.Text = "SSCP Approve"
        '
        'chbSSCPSignOff
        '
        Me.chbSSCPSignOff.AutoSize = True
        Me.chbSSCPSignOff.Location = New System.Drawing.Point(89, 95)
        Me.chbSSCPSignOff.Name = "chbSSCPSignOff"
        Me.chbSSCPSignOff.Size = New System.Drawing.Size(95, 17)
        Me.chbSSCPSignOff.TabIndex = 8
        Me.chbSSCPSignOff.Text = "SSCP Sign Off"
        Me.chbSSCPSignOff.UseVisualStyleBackColor = True
        '
        'btnRemoveFromPlatform
        '
        Me.btnRemoveFromPlatform.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRemoveFromPlatform.Location = New System.Drawing.Point(580, 62)
        Me.btnRemoveFromPlatform.Name = "btnRemoveFromPlatform"
        Me.btnRemoveFromPlatform.Size = New System.Drawing.Size(147, 23)
        Me.btnRemoveFromPlatform.TabIndex = 19
        Me.btnRemoveFromPlatform.Text = "Remove Facility"
        Me.btnRemoveFromPlatform.UseVisualStyleBackColor = True
        '
        'DTPSSCPApproveDate
        '
        Me.DTPSSCPApproveDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSSCPApproveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSSCPApproveDate.Location = New System.Drawing.Point(213, 118)
        Me.DTPSSCPApproveDate.Name = "DTPSSCPApproveDate"
        Me.DTPSSCPApproveDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPSSCPApproveDate.TabIndex = 10
        Me.DTPSSCPApproveDate.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'btnSubmitFacilityToAFS
        '
        Me.btnSubmitFacilityToAFS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSubmitFacilityToAFS.Location = New System.Drawing.Point(580, 34)
        Me.btnSubmitFacilityToAFS.Name = "btnSubmitFacilityToAFS"
        Me.btnSubmitFacilityToAFS.Size = New System.Drawing.Size(147, 23)
        Me.btnSubmitFacilityToAFS.TabIndex = 18
        Me.btnSubmitFacilityToAFS.Text = "Approve Facility"
        Me.btnSubmitFacilityToAFS.UseVisualStyleBackColor = True
        '
        'txtSSCPApprover
        '
        Me.txtSSCPApprover.Location = New System.Drawing.Point(89, 118)
        Me.txtSSCPApprover.MaxLength = 12
        Me.txtSSCPApprover.Name = "txtSSCPApprover"
        Me.txtSSCPApprover.ReadOnly = True
        Me.txtSSCPApprover.Size = New System.Drawing.Size(118, 20)
        Me.txtSSCPApprover.TabIndex = 9
        '
        'btnViewFacility
        '
        Me.btnViewFacility.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnViewFacility.Location = New System.Drawing.Point(580, 90)
        Me.btnViewFacility.Name = "btnViewFacility"
        Me.btnViewFacility.Size = New System.Drawing.Size(147, 23)
        Me.btnViewFacility.TabIndex = 20
        Me.btnViewFacility.Text = "Edit Facility Information"
        Me.btnViewFacility.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(187, 38)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(70, 13)
        Me.Label13.TabIndex = 65
        Me.Label13.Text = "Facility Name"
        '
        'txtNewFacilityName
        '
        Me.txtNewFacilityName.Location = New System.Drawing.Point(263, 35)
        Me.txtNewFacilityName.MaxLength = 100
        Me.txtNewFacilityName.Name = "txtNewFacilityName"
        Me.txtNewFacilityName.Size = New System.Drawing.Size(265, 20)
        Me.txtNewFacilityName.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(11, 38)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 13)
        Me.Label12.TabIndex = 63
        Me.Label12.Text = "AIRS Number"
        '
        'txtNewAIRSNumber
        '
        Me.txtNewAIRSNumber.Location = New System.Drawing.Point(89, 35)
        Me.txtNewAIRSNumber.MaxLength = 12
        Me.txtNewAIRSNumber.Name = "txtNewAIRSNumber"
        Me.txtNewAIRSNumber.ReadOnly = True
        Me.txtNewAIRSNumber.Size = New System.Drawing.Size(88, 20)
        Me.txtNewAIRSNumber.TabIndex = 4
        '
        'dgvVerifyNewFacilities
        '
        Me.dgvVerifyNewFacilities.AllowUserToAddRows = False
        Me.dgvVerifyNewFacilities.AllowUserToDeleteRows = False
        Me.dgvVerifyNewFacilities.AllowUserToResizeRows = False
        Me.dgvVerifyNewFacilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVerifyNewFacilities.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvVerifyNewFacilities.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvVerifyNewFacilities.Location = New System.Drawing.Point(3, 3)
        Me.dgvVerifyNewFacilities.MultiSelect = False
        Me.dgvVerifyNewFacilities.Name = "dgvVerifyNewFacilities"
        Me.dgvVerifyNewFacilities.RowHeadersVisible = False
        Me.dgvVerifyNewFacilities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvVerifyNewFacilities.Size = New System.Drawing.Size(732, 226)
        Me.dgvVerifyNewFacilities.TabIndex = 0
        '
        'TPDeleteFacility
        '
        Me.TPDeleteFacility.Controls.Add(Me.lblFacilityCannotBeDeletedOrDeactivated)
        Me.TPDeleteFacility.Controls.Add(Me.AirsNumberToRemove)
        Me.TPDeleteFacility.Controls.Add(Me.FacilityLongDisplay)
        Me.TPDeleteFacility.Controls.Add(Me.btnDeleteAirsNumber)
        Me.TPDeleteFacility.Controls.Add(Me.btnDeactivateFacility)
        Me.TPDeleteFacility.Controls.Add(Me.lblFacilityCannotBeDeleted)
        Me.TPDeleteFacility.Controls.Add(Me.Label25)
        Me.TPDeleteFacility.Controls.Add(Me.Label23)
        Me.TPDeleteFacility.Controls.Add(Me.AirsNumberToDeleteLabel)
        Me.TPDeleteFacility.Location = New System.Drawing.Point(4, 22)
        Me.TPDeleteFacility.Name = "TPDeleteFacility"
        Me.TPDeleteFacility.Size = New System.Drawing.Size(738, 519)
        Me.TPDeleteFacility.TabIndex = 2
        Me.TPDeleteFacility.Text = "Remove Facility"
        Me.TPDeleteFacility.UseVisualStyleBackColor = True
        '
        'lblFacilityCannotBeDeletedOrDeactivated
        '
        Me.lblFacilityCannotBeDeletedOrDeactivated.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityCannotBeDeletedOrDeactivated.Location = New System.Drawing.Point(209, 198)
        Me.lblFacilityCannotBeDeletedOrDeactivated.Name = "lblFacilityCannotBeDeletedOrDeactivated"
        Me.lblFacilityCannotBeDeletedOrDeactivated.Size = New System.Drawing.Size(432, 121)
        Me.lblFacilityCannotBeDeletedOrDeactivated.TabIndex = 6
        Me.lblFacilityCannotBeDeletedOrDeactivated.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Facility has compliance or other data and can't be deleted or deactivated."
        Me.lblFacilityCannotBeDeletedOrDeactivated.Visible = False
        '
        'AirsNumberToRemove
        '
        Me.AirsNumberToRemove.Location = New System.Drawing.Point(37, 55)
        Me.AirsNumberToRemove.Name = "AirsNumberToRemove"
        Me.AirsNumberToRemove.Size = New System.Drawing.Size(100, 20)
        Me.AirsNumberToRemove.TabIndex = 0
        '
        'FacilityLongDisplay
        '
        Me.FacilityLongDisplay.AutoSize = True
        Me.FacilityLongDisplay.Location = New System.Drawing.Point(34, 78)
        Me.FacilityLongDisplay.Name = "FacilityLongDisplay"
        Me.FacilityLongDisplay.Size = New System.Drawing.Size(97, 104)
        Me.FacilityLongDisplay.TabIndex = 4
        Me.FacilityLongDisplay.Text = "Facility long display" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "6" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "7" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "8"
        '
        'btnDeleteAirsNumber
        '
        Me.btnDeleteAirsNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteAirsNumber.Enabled = False
        Me.btnDeleteAirsNumber.Location = New System.Drawing.Point(37, 268)
        Me.btnDeleteAirsNumber.Name = "btnDeleteAirsNumber"
        Me.btnDeleteAirsNumber.Size = New System.Drawing.Size(166, 51)
        Me.btnDeleteAirsNumber.TabIndex = 2
        Me.btnDeleteAirsNumber.Text = "Delete Facility && AIRS Number"
        Me.btnDeleteAirsNumber.UseVisualStyleBackColor = True
        '
        'btnDeactivateFacility
        '
        Me.btnDeactivateFacility.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeactivateFacility.Enabled = False
        Me.btnDeactivateFacility.Location = New System.Drawing.Point(37, 198)
        Me.btnDeactivateFacility.Name = "btnDeactivateFacility"
        Me.btnDeactivateFacility.Size = New System.Drawing.Size(166, 51)
        Me.btnDeactivateFacility.TabIndex = 1
        Me.btnDeactivateFacility.Text = "Deactivate Facility"
        Me.btnDeactivateFacility.UseVisualStyleBackColor = True
        '
        'lblFacilityCannotBeDeleted
        '
        Me.lblFacilityCannotBeDeleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityCannotBeDeleted.Location = New System.Drawing.Point(209, 268)
        Me.lblFacilityCannotBeDeleted.Name = "lblFacilityCannotBeDeleted"
        Me.lblFacilityCannotBeDeleted.Size = New System.Drawing.Size(432, 51)
        Me.lblFacilityCannotBeDeleted.TabIndex = 6
        Me.lblFacilityCannotBeDeleted.Text = "Facility has permit fees data and can't be deleted unless the fees are removed fi" &
    "rst."
        Me.lblFacilityCannotBeDeleted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFacilityCannotBeDeleted.Visible = False
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(209, 268)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(432, 51)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "If an AIRS number has been incorrectly created and no data exists other than basi" &
    "c facility info, then the AIRS number can be deleted."
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(209, 198)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(432, 51)
        Me.Label23.TabIndex = 5
        Me.Label23.Text = resources.GetString("Label23.Text")
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'AirsNumberToDeleteLabel
        '
        Me.AirsNumberToDeleteLabel.AutoSize = True
        Me.AirsNumberToDeleteLabel.Location = New System.Drawing.Point(34, 39)
        Me.AirsNumberToDeleteLabel.Name = "AirsNumberToDeleteLabel"
        Me.AirsNumberToDeleteLabel.Size = New System.Drawing.Size(72, 13)
        Me.AirsNumberToDeleteLabel.TabIndex = 3
        Me.AirsNumberToDeleteLabel.Text = "AIRS Number"
        '
        'TPDeactivatedFacilities
        '
        Me.TPDeactivatedFacilities.Controls.Add(Me.btnRefreshDeactivatedFacilities)
        Me.TPDeactivatedFacilities.Controls.Add(Me.dgvDeactivatedFacilities)
        Me.TPDeactivatedFacilities.Location = New System.Drawing.Point(4, 22)
        Me.TPDeactivatedFacilities.Name = "TPDeactivatedFacilities"
        Me.TPDeactivatedFacilities.Padding = New System.Windows.Forms.Padding(3)
        Me.TPDeactivatedFacilities.Size = New System.Drawing.Size(738, 519)
        Me.TPDeactivatedFacilities.TabIndex = 3
        Me.TPDeactivatedFacilities.Text = "Deactivated Facilities"
        Me.TPDeactivatedFacilities.UseVisualStyleBackColor = True
        '
        'dgvDeactivatedFacilities
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvDeactivatedFacilities.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDeactivatedFacilities.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDeactivatedFacilities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvDeactivatedFacilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDeactivatedFacilities.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvDeactivatedFacilities.LinkifyColumnByName = Nothing
        Me.dgvDeactivatedFacilities.Location = New System.Drawing.Point(0, 35)
        Me.dgvDeactivatedFacilities.Name = "dgvDeactivatedFacilities"
        Me.dgvDeactivatedFacilities.ResultsCountLabel = Nothing
        Me.dgvDeactivatedFacilities.ResultsCountLabelFormat = "{0} found"
        Me.dgvDeactivatedFacilities.Size = New System.Drawing.Size(738, 484)
        Me.dgvDeactivatedFacilities.StandardTab = True
        Me.dgvDeactivatedFacilities.TabIndex = 0
        '
        'btnRefreshDeactivatedFacilities
        '
        Me.btnRefreshDeactivatedFacilities.Location = New System.Drawing.Point(3, 6)
        Me.btnRefreshDeactivatedFacilities.Name = "btnRefreshDeactivatedFacilities"
        Me.btnRefreshDeactivatedFacilities.Size = New System.Drawing.Size(75, 23)
        Me.btnRefreshDeactivatedFacilities.TabIndex = 1
        Me.btnRefreshDeactivatedFacilities.Text = "Reload"
        Me.btnRefreshDeactivatedFacilities.UseVisualStyleBackColor = True
        '
        'IAIPFacilityCreator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(746, 545)
        Me.Controls.Add(Me.TCFacilityTools)
        Me.MinimumSize = New System.Drawing.Size(762, 583)
        Me.Name = "IAIPFacilityCreator"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Facility Creator Tool"
        Me.GBFacilityInformation.ResumeLayout(False)
        Me.GBFacilityInformation.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.GBContactInformation.ResumeLayout(False)
        Me.GBContactInformation.PerformLayout()
        Me.GBAirProgramCodes.ResumeLayout(False)
        Me.GBAirProgramCodes.PerformLayout()
        Me.GBHeaderData.ResumeLayout(False)
        Me.GBHeaderData.PerformLayout()
        Me.GBMailingLocation.ResumeLayout(False)
        Me.GBMailingLocation.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TCFacilityTools.ResumeLayout(False)
        Me.TPCreateNewFacility.ResumeLayout(False)
        Me.TPApproveNewFacility.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgvValidatingAIRS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVerifyNewFacilities, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPDeleteFacility.ResumeLayout(False)
        Me.TPDeleteFacility.PerformLayout()
        Me.TPDeactivatedFacilities.ResumeLayout(False)
        CType(Me.dgvDeactivatedFacilities, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents tpAIRSNumber As System.Windows.Forms.TabPage
    Friend WithEvents btnAIRSNumberSearch As System.Windows.Forms.Button
    Friend WithEvents txtAIRSNumberSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tpComplianceSearch As System.Windows.Forms.TabPage
    Friend WithEvents btnComplianceSearch As System.Windows.Forms.Button
    Friend WithEvents txtComplianceEngineer As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tpCity As System.Windows.Forms.TabPage
    Friend WithEvents btnCitySearch As System.Windows.Forms.Button
    Friend WithEvents txtCityNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents tpZipCode As System.Windows.Forms.TabPage
    Friend WithEvents btnZipCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtZipCodeSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents tpSIC As System.Windows.Forms.TabPage
    Friend WithEvents btnSICCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSICCodeSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents tpCounty As System.Windows.Forms.TabPage
    Friend WithEvents txtCountyNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnCountySearch As System.Windows.Forms.Button
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents tpFacilityName As System.Windows.Forms.TabPage
    Friend WithEvents btnFacilityNameSearch As System.Windows.Forms.Button
    Friend WithEvents txtFacilityNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents dgvPossibleMatches As System.Windows.Forms.DataGridView
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnSelectAIRSNumber As System.Windows.Forms.Button
    Friend WithEvents chbHistoricalNames As System.Windows.Forms.CheckBox
    Friend WithEvents tpSubpart As System.Windows.Forms.TabPage
    Friend WithEvents txtSubpartSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSubpartSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdbPart63 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPart60 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPart61 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGASIP As System.Windows.Forms.RadioButton
    Friend WithEvents GBFacilityInformation As System.Windows.Forms.GroupBox
    Friend WithEvents mtbCDSZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents txtCDSStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCDSFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCDSCity As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCDSState As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents GBMailingLocation As System.Windows.Forms.GroupBox
    Friend WithEvents mtbMailingZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtMailingState As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtMailingCity As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtMailingAddress As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCDSAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents GBAirProgramCodes As System.Windows.Forms.GroupBox
    Friend WithEvents chbCDS_15 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_13 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_12 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_10 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_11 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_8 As System.Windows.Forms.CheckBox
    Friend WithEvents GBHeaderData As System.Windows.Forms.GroupBox
    Friend WithEvents mtbCDSSICCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cboCDSOperationalStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents cboCDSClassCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GBContactInformation As System.Windows.Forms.GroupBox
    Friend WithEvents txtContactPedigree As System.Windows.Forms.TextBox
    Friend WithEvents txtContactSocialTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtContactLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtContactFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtContactTitle As System.Windows.Forms.TextBox
    Friend WithEvents cboCounty As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnSaveNewFacility As System.Windows.Forms.Button
    Friend WithEvents btnPreLoadNewFacility As System.Windows.Forms.Button
    Friend WithEvents mtbFacilityLongitude As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbFacilityLatitude As System.Windows.Forms.MaskedTextBox
    Friend WithEvents llbOpenWebpage As System.Windows.Forms.LinkLabel
    Friend WithEvents TCFacilityTools As System.Windows.Forms.TabControl
    Friend WithEvents TPCreateNewFacility As System.Windows.Forms.TabPage
    Friend WithEvents TPApproveNewFacility As System.Windows.Forms.TabPage
    Friend WithEvents dgvVerifyNewFacilities As System.Windows.Forms.DataGridView
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnSubmitFacilityToAFS As System.Windows.Forms.Button
    Friend WithEvents btnViewFacility As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtNewFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtNewAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnRemoveFromPlatform As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityComments As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtSSCPApprover As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveSSCPApproval As System.Windows.Forms.Button
    Friend WithEvents chbSSCPSignOff As System.Windows.Forms.CheckBox
    Friend WithEvents txtSSCPComments As System.Windows.Forms.TextBox
    Friend WithEvents DTPSSCPApproveDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSSPPComments As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveSSPPApproval As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chbSSPPSignOff As System.Windows.Forms.CheckBox
    Friend WithEvents DTPSSPPApproveDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSSPPApprover As System.Windows.Forms.TextBox
    Friend WithEvents btnValidateFacility As System.Windows.Forms.Button
    Friend WithEvents dgvValidatingAIRS As System.Windows.Forms.DataGridView
    Friend WithEvents txtStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtCDSRegionCode As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtApprovialComments As System.Windows.Forms.TextBox
    Friend WithEvents btnEditFacilityData As System.Windows.Forms.Button
    Friend WithEvents lblValidationCount As System.Windows.Forms.Label
    Friend WithEvents btnFilterNewFacilities As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents chbFilterNewFacilities As System.Windows.Forms.CheckBox
    Friend WithEvents dtpEndFilter As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartFilter As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtCountFacilities As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents chbCDS_14 As System.Windows.Forms.CheckBox
    Friend WithEvents mtbRiskManagementNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents mtbCDSNAICSCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents TPDeleteFacility As System.Windows.Forms.TabPage
    Friend WithEvents btnDeactivateFacility As System.Windows.Forms.Button
    Friend WithEvents AirsNumberToDeleteLabel As System.Windows.Forms.Label
    Friend WithEvents FacilityLongDisplay As System.Windows.Forms.Label
    Friend WithEvents txtApplicationNumber As CueTextBox
    Friend WithEvents btnClear As Button
    Friend WithEvents txtContactPhoneNumber As TextBox
    Friend WithEvents AirsNumberToRemove As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents btnDeleteAirsNumber As Button
    Friend WithEvents lblFacilityCannotBeDeleted As Label
    Friend WithEvents lblFacilityCannotBeDeletedOrDeactivated As Label
    Friend WithEvents TPDeactivatedFacilities As TabPage
    Friend WithEvents btnRefreshDeactivatedFacilities As Button
    Friend WithEvents dgvDeactivatedFacilities As IaipDataGridView
End Class
