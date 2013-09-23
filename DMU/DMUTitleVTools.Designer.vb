<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DMUTitleVTools
    Inherits DefaultForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DMUTitleVTools))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MmiFile = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MmiView = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.bgwTransfer = New System.ComponentModel.BackgroundWorker
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TCDMUTools = New System.Windows.Forms.TabControl
        Me.TPWebPublishing = New System.Windows.Forms.TabPage
        Me.dgrWebPublisher = New System.Windows.Forms.DataGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtTVCount = New System.Windows.Forms.TextBox
        Me.Label101 = New System.Windows.Forms.Label
        Me.txtFacilityInformation = New System.Windows.Forms.TextBox
        Me.chbPNExpires = New System.Windows.Forms.CheckBox
        Me.DTPPNExpires = New System.Windows.Forms.DateTimePicker
        Me.DTPExperationDate = New System.Windows.Forms.DateTimePicker
        Me.Label68 = New System.Windows.Forms.Label
        Me.chbExpirationDate = New System.Windows.Forms.CheckBox
        Me.chbNotifiedAppReceived = New System.Windows.Forms.CheckBox
        Me.DTPNotifiedAppReceived = New System.Windows.Forms.DateTimePicker
        Me.lbLinkApplications = New System.Windows.Forms.ListBox
        Me.btnReloadGrid = New System.Windows.Forms.Button
        Me.btnViewApplication = New System.Windows.Forms.Button
        Me.lblLinkWarning = New System.Windows.Forms.Label
        Me.btnSearchForApplication = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.txtWebPublisherApplicationNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSaveWebPublisher = New System.Windows.Forms.Button
        Me.chbEffectiveDateOfPermit = New System.Windows.Forms.CheckBox
        Me.chbEPANotifiedPermitOnWeb = New System.Windows.Forms.CheckBox
        Me.chbFinalOnWeb = New System.Windows.Forms.CheckBox
        Me.chbEPAandStatesNotified = New System.Windows.Forms.CheckBox
        Me.chbDraftOnWeb = New System.Windows.Forms.CheckBox
        Me.txtEPATargetedComments = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.DTPEffectiveDateofPermit = New System.Windows.Forms.DateTimePicker
        Me.DTPEPANotifiedPermitOnWeb = New System.Windows.Forms.DateTimePicker
        Me.DTPFinalOnWeb = New System.Windows.Forms.DateTimePicker
        Me.DTPEPAStatesNotified = New System.Windows.Forms.DateTimePicker
        Me.DTPDraftOnWeb = New System.Windows.Forms.DateTimePicker
        Me.TPTVEmails = New System.Windows.Forms.TabPage
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.txtEmailLetter = New System.Windows.Forms.TextBox
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.clbTitleVEmailList = New System.Windows.Forms.CheckedListBox
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label70 = New System.Windows.Forms.Label
        Me.Label60 = New System.Windows.Forms.Label
        Me.txtApplicationCount = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.btnMinorModOnWebState = New System.Windows.Forms.Button
        Me.btnMinorModOnWebEPD = New System.Windows.Forms.Button
        Me.btnPreviewMinorMod = New System.Windows.Forms.Button
        Me.btnEmailDraftOnWebState = New System.Windows.Forms.Button
        Me.txtEmailType = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.btnAddApplicationToList = New System.Windows.Forms.Button
        Me.txtApplicationNumberToAdd = New System.Windows.Forms.TextBox
        Me.btnEmailESNReceived = New System.Windows.Forms.Button
        Me.btnPreviewFinalOnWeb = New System.Windows.Forms.Button
        Me.btnPreviewESNReceived = New System.Windows.Forms.Button
        Me.btnEmailFinalOnWeb = New System.Windows.Forms.Button
        Me.btnEmailDraftOnWeb = New System.Windows.Forms.Button
        Me.btnPreviewDraftOnWeb = New System.Windows.Forms.Button
        Me.TPTitleVRenewals = New System.Windows.Forms.TabPage
        Me.clbTitleVRenewals = New System.Windows.Forms.CheckedListBox
        Me.GBTitleVRenewals = New System.Windows.Forms.GroupBox
        Me.lblEndDate = New System.Windows.Forms.Label
        Me.lblStartDate = New System.Windows.Forms.Label
        Me.btnPrintSingleTitleVRenewal = New System.Windows.Forms.Button
        Me.txtTitleVSingleLetter = New System.Windows.Forms.TextBox
        Me.btnPrintRenewalLetters = New System.Windows.Forms.Button
        Me.DTPTitleVRenewalEnd = New System.Windows.Forms.DateTimePicker
        Me.DTPTitleVRenewalStart = New System.Windows.Forms.DateTimePicker
        Me.btnRunTitleVReport = New System.Windows.Forms.Button
        Me.txtRenewalCount = New System.Windows.Forms.TextBox
        Me.Label57 = New System.Windows.Forms.Label
        Me.Label56 = New System.Windows.Forms.Label
        Me.TPPermittingContact = New System.Windows.Forms.TabPage
        Me.txtContactDescriptionCompliance = New System.Windows.Forms.TextBox
        Me.txtContactEmailAddressCompliance = New System.Windows.Forms.TextBox
        Me.mtbContactFaxNumberCompliance = New System.Windows.Forms.MaskedTextBox
        Me.mtbContactPhoneNumberCompliance = New System.Windows.Forms.MaskedTextBox
        Me.mtbContactZipCodeCompliance = New System.Windows.Forms.MaskedTextBox
        Me.txtContactStateCompliance = New System.Windows.Forms.TextBox
        Me.txtContactCityCompliance = New System.Windows.Forms.TextBox
        Me.txtContactStreetAddressCompliance = New System.Windows.Forms.TextBox
        Me.txtContactTitleCompliance = New System.Windows.Forms.TextBox
        Me.txtContactCompanyNameCompliance = New System.Windows.Forms.TextBox
        Me.txtContactPedigreeCompliance = New System.Windows.Forms.TextBox
        Me.txtContactLastNameCompliance = New System.Windows.Forms.TextBox
        Me.txtContactFirstNameCompliance = New System.Windows.Forms.TextBox
        Me.txtContactSocialTitleCompliance = New System.Windows.Forms.TextBox
        Me.btnLoadFromWarehouse = New System.Windows.Forms.Button
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnSaveContactApp = New System.Windows.Forms.Button
        Me.btnLoadAppContact = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtApplicationNumber = New System.Windows.Forms.TextBox
        Me.btnGetCurrentPermittingContact = New System.Windows.Forms.Button
        Me.mtbContactZipCode = New System.Windows.Forms.MaskedTextBox
        Me.mtbContactFaxNumber = New System.Windows.Forms.MaskedTextBox
        Me.mtbContactPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.txtContactDescription = New System.Windows.Forms.TextBox
        Me.txtContactEmailAddress = New System.Windows.Forms.TextBox
        Me.txtContactState = New System.Windows.Forms.TextBox
        Me.txtContactCity = New System.Windows.Forms.TextBox
        Me.txtContactStreetAddress = New System.Windows.Forms.TextBox
        Me.txtContactTitle = New System.Windows.Forms.TextBox
        Me.txtContactCompanyName = New System.Windows.Forms.TextBox
        Me.txtContactPedigree = New System.Windows.Forms.TextBox
        Me.txtContactSocialTitle = New System.Windows.Forms.TextBox
        Me.txtContactLastName = New System.Windows.Forms.TextBox
        Me.txtContactFirstName = New System.Windows.Forms.TextBox
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TSDMUStaffTools = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1.SuspendLayout()
        Me.TCDMUTools.SuspendLayout()
        Me.TPWebPublishing.SuspendLayout()
        CType(Me.dgrWebPublisher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.TPTVEmails.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.TPTitleVRenewals.SuspendLayout()
        Me.GBTitleVRenewals.SuspendLayout()
        Me.TPPermittingContact.SuspendLayout()
        Me.TSDMUStaffTools.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 520)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(823, 22)
        Me.StatusStrip1.TabIndex = 254
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 17)
        Me.Panel1.Spring = True
        Me.Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(4, 17)
        '
        'Panel3
        '
        Me.Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(4, 17)
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiFile, Me.MmiView, Me.MmiHelp})
        '
        'MmiFile
        '
        Me.MmiFile.Index = 0
        Me.MmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiBack})
        Me.MmiFile.Text = "File"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 0
        Me.MmiBack.Text = "Back"
        '
        'MmiView
        '
        Me.MmiView.Index = 1
        Me.MmiView.Text = "View"
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 2
        Me.MmiHelp.Text = "Help"
        '
        'Image_List_All
        '
        Me.Image_List_All.ImageStream = CType(resources.GetObject("Image_List_All.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Image_List_All.TransparentColor = System.Drawing.Color.Transparent
        Me.Image_List_All.Images.SetKeyName(0, "")
        Me.Image_List_All.Images.SetKeyName(1, "")
        Me.Image_List_All.Images.SetKeyName(2, "")
        Me.Image_List_All.Images.SetKeyName(3, "")
        Me.Image_List_All.Images.SetKeyName(4, "")
        Me.Image_List_All.Images.SetKeyName(5, "")
        Me.Image_List_All.Images.SetKeyName(6, "")
        Me.Image_List_All.Images.SetKeyName(7, "")
        Me.Image_List_All.Images.SetKeyName(8, "")
        Me.Image_List_All.Images.SetKeyName(9, "")
        Me.Image_List_All.Images.SetKeyName(10, "")
        Me.Image_List_All.Images.SetKeyName(11, "")
        Me.Image_List_All.Images.SetKeyName(12, "")
        Me.Image_List_All.Images.SetKeyName(13, "")
        Me.Image_List_All.Images.SetKeyName(14, "")
        Me.Image_List_All.Images.SetKeyName(15, "")
        Me.Image_List_All.Images.SetKeyName(16, "")
        Me.Image_List_All.Images.SetKeyName(17, "")
        Me.Image_List_All.Images.SetKeyName(18, "")
        Me.Image_List_All.Images.SetKeyName(19, "")
        Me.Image_List_All.Images.SetKeyName(20, "")
        Me.Image_List_All.Images.SetKeyName(21, "")
        Me.Image_List_All.Images.SetKeyName(22, "")
        Me.Image_List_All.Images.SetKeyName(23, "")
        Me.Image_List_All.Images.SetKeyName(24, "")
        Me.Image_List_All.Images.SetKeyName(25, "")
        Me.Image_List_All.Images.SetKeyName(26, "")
        Me.Image_List_All.Images.SetKeyName(27, "")
        Me.Image_List_All.Images.SetKeyName(28, "")
        Me.Image_List_All.Images.SetKeyName(29, "")
        Me.Image_List_All.Images.SetKeyName(30, "")
        Me.Image_List_All.Images.SetKeyName(31, "")
        Me.Image_List_All.Images.SetKeyName(32, "")
        Me.Image_List_All.Images.SetKeyName(33, "")
        Me.Image_List_All.Images.SetKeyName(34, "")
        Me.Image_List_All.Images.SetKeyName(35, "")
        Me.Image_List_All.Images.SetKeyName(36, "")
        Me.Image_List_All.Images.SetKeyName(37, "")
        Me.Image_List_All.Images.SetKeyName(38, "")
        Me.Image_List_All.Images.SetKeyName(39, "")
        Me.Image_List_All.Images.SetKeyName(40, "")
        Me.Image_List_All.Images.SetKeyName(41, "")
        Me.Image_List_All.Images.SetKeyName(42, "")
        Me.Image_List_All.Images.SetKeyName(43, "")
        Me.Image_List_All.Images.SetKeyName(44, "")
        Me.Image_List_All.Images.SetKeyName(45, "")
        Me.Image_List_All.Images.SetKeyName(46, "")
        Me.Image_List_All.Images.SetKeyName(47, "")
        Me.Image_List_All.Images.SetKeyName(48, "")
        Me.Image_List_All.Images.SetKeyName(49, "")
        Me.Image_List_All.Images.SetKeyName(50, "")
        Me.Image_List_All.Images.SetKeyName(51, "")
        Me.Image_List_All.Images.SetKeyName(52, "")
        Me.Image_List_All.Images.SetKeyName(53, "")
        Me.Image_List_All.Images.SetKeyName(54, "")
        Me.Image_List_All.Images.SetKeyName(55, "")
        Me.Image_List_All.Images.SetKeyName(56, "")
        Me.Image_List_All.Images.SetKeyName(57, "")
        Me.Image_List_All.Images.SetKeyName(58, "")
        Me.Image_List_All.Images.SetKeyName(59, "")
        Me.Image_List_All.Images.SetKeyName(60, "")
        Me.Image_List_All.Images.SetKeyName(61, "")
        Me.Image_List_All.Images.SetKeyName(62, "")
        Me.Image_List_All.Images.SetKeyName(63, "")
        Me.Image_List_All.Images.SetKeyName(64, "")
        Me.Image_List_All.Images.SetKeyName(65, "")
        Me.Image_List_All.Images.SetKeyName(66, "")
        Me.Image_List_All.Images.SetKeyName(67, "")
        Me.Image_List_All.Images.SetKeyName(68, "")
        Me.Image_List_All.Images.SetKeyName(69, "")
        Me.Image_List_All.Images.SetKeyName(70, "")
        Me.Image_List_All.Images.SetKeyName(71, "")
        Me.Image_List_All.Images.SetKeyName(72, "")
        Me.Image_List_All.Images.SetKeyName(73, "")
        Me.Image_List_All.Images.SetKeyName(74, "")
        Me.Image_List_All.Images.SetKeyName(75, "")
        Me.Image_List_All.Images.SetKeyName(76, "")
        Me.Image_List_All.Images.SetKeyName(77, "")
        Me.Image_List_All.Images.SetKeyName(78, "")
        Me.Image_List_All.Images.SetKeyName(79, "")
        Me.Image_List_All.Images.SetKeyName(80, "")
        Me.Image_List_All.Images.SetKeyName(81, "")
        Me.Image_List_All.Images.SetKeyName(82, "")
        Me.Image_List_All.Images.SetKeyName(83, "")
        Me.Image_List_All.Images.SetKeyName(84, "")
        '
        'TCDMUTools
        '
        Me.TCDMUTools.Controls.Add(Me.TPWebPublishing)
        Me.TCDMUTools.Controls.Add(Me.TPTVEmails)
        Me.TCDMUTools.Controls.Add(Me.TPTitleVRenewals)
        Me.TCDMUTools.Controls.Add(Me.TPPermittingContact)
        Me.TCDMUTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDMUTools.Location = New System.Drawing.Point(0, 25)
        Me.TCDMUTools.Name = "TCDMUTools"
        Me.TCDMUTools.SelectedIndex = 0
        Me.TCDMUTools.Size = New System.Drawing.Size(823, 495)
        Me.TCDMUTools.TabIndex = 256
        '
        'TPWebPublishing
        '
        Me.TPWebPublishing.Controls.Add(Me.dgrWebPublisher)
        Me.TPWebPublishing.Controls.Add(Me.Panel4)
        Me.TPWebPublishing.Location = New System.Drawing.Point(4, 22)
        Me.TPWebPublishing.Name = "TPWebPublishing"
        Me.TPWebPublishing.Size = New System.Drawing.Size(815, 469)
        Me.TPWebPublishing.TabIndex = 0
        Me.TPWebPublishing.Text = "Title V Web Publishing"
        Me.TPWebPublishing.UseVisualStyleBackColor = True
        '
        'dgrWebPublisher
        '
        Me.dgrWebPublisher.DataMember = ""
        Me.dgrWebPublisher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrWebPublisher.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrWebPublisher.Location = New System.Drawing.Point(419, 0)
        Me.dgrWebPublisher.Name = "dgrWebPublisher"
        Me.dgrWebPublisher.ReadOnly = True
        Me.dgrWebPublisher.Size = New System.Drawing.Size(396, 469)
        Me.dgrWebPublisher.TabIndex = 2
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.txtTVCount)
        Me.Panel4.Controls.Add(Me.Label101)
        Me.Panel4.Controls.Add(Me.txtFacilityInformation)
        Me.Panel4.Controls.Add(Me.chbPNExpires)
        Me.Panel4.Controls.Add(Me.DTPPNExpires)
        Me.Panel4.Controls.Add(Me.DTPExperationDate)
        Me.Panel4.Controls.Add(Me.Label68)
        Me.Panel4.Controls.Add(Me.chbExpirationDate)
        Me.Panel4.Controls.Add(Me.chbNotifiedAppReceived)
        Me.Panel4.Controls.Add(Me.DTPNotifiedAppReceived)
        Me.Panel4.Controls.Add(Me.lbLinkApplications)
        Me.Panel4.Controls.Add(Me.btnReloadGrid)
        Me.Panel4.Controls.Add(Me.btnViewApplication)
        Me.Panel4.Controls.Add(Me.lblLinkWarning)
        Me.Panel4.Controls.Add(Me.btnSearchForApplication)
        Me.Panel4.Controls.Add(Me.btnClear)
        Me.Panel4.Controls.Add(Me.txtWebPublisherApplicationNumber)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.btnSaveWebPublisher)
        Me.Panel4.Controls.Add(Me.chbEffectiveDateOfPermit)
        Me.Panel4.Controls.Add(Me.chbEPANotifiedPermitOnWeb)
        Me.Panel4.Controls.Add(Me.chbFinalOnWeb)
        Me.Panel4.Controls.Add(Me.chbEPAandStatesNotified)
        Me.Panel4.Controls.Add(Me.chbDraftOnWeb)
        Me.Panel4.Controls.Add(Me.txtEPATargetedComments)
        Me.Panel4.Controls.Add(Me.Label43)
        Me.Panel4.Controls.Add(Me.DTPEffectiveDateofPermit)
        Me.Panel4.Controls.Add(Me.DTPEPANotifiedPermitOnWeb)
        Me.Panel4.Controls.Add(Me.DTPFinalOnWeb)
        Me.Panel4.Controls.Add(Me.DTPEPAStatesNotified)
        Me.Panel4.Controls.Add(Me.DTPDraftOnWeb)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(419, 469)
        Me.Panel4.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(278, 483)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(35, 13)
        Me.Label12.TabIndex = 380
        Me.Label12.Text = "Count"
        '
        'txtTVCount
        '
        Me.txtTVCount.Location = New System.Drawing.Point(319, 480)
        Me.txtTVCount.Name = "txtTVCount"
        Me.txtTVCount.ReadOnly = True
        Me.txtTVCount.Size = New System.Drawing.Size(74, 20)
        Me.txtTVCount.TabIndex = 379
        '
        'Label101
        '
        Me.Label101.AutoSize = True
        Me.Label101.Location = New System.Drawing.Point(9, 50)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(94, 13)
        Me.Label101.TabIndex = 378
        Me.Label101.Text = "Facility Information"
        '
        'txtFacilityInformation
        '
        Me.txtFacilityInformation.Location = New System.Drawing.Point(122, 50)
        Me.txtFacilityInformation.Multiline = True
        Me.txtFacilityInformation.Name = "txtFacilityInformation"
        Me.txtFacilityInformation.ReadOnly = True
        Me.txtFacilityInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityInformation.Size = New System.Drawing.Size(269, 45)
        Me.txtFacilityInformation.TabIndex = 377
        '
        'chbPNExpires
        '
        Me.chbPNExpires.AutoSize = True
        Me.chbPNExpires.Location = New System.Drawing.Point(122, 186)
        Me.chbPNExpires.Name = "chbPNExpires"
        Me.chbPNExpires.Size = New System.Drawing.Size(78, 17)
        Me.chbPNExpires.TabIndex = 376
        Me.chbPNExpires.Text = "PN Expires"
        '
        'DTPPNExpires
        '
        Me.DTPPNExpires.CustomFormat = "dd-MMM-yyyy"
        Me.DTPPNExpires.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPPNExpires.Location = New System.Drawing.Point(12, 184)
        Me.DTPPNExpires.Name = "DTPPNExpires"
        Me.DTPPNExpires.Size = New System.Drawing.Size(95, 20)
        Me.DTPPNExpires.TabIndex = 375
        Me.DTPPNExpires.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPPNExpires.Visible = False
        '
        'DTPExperationDate
        '
        Me.DTPExperationDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPExperationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPExperationDate.Location = New System.Drawing.Point(12, 249)
        Me.DTPExperationDate.Name = "DTPExperationDate"
        Me.DTPExperationDate.Size = New System.Drawing.Size(96, 20)
        Me.DTPExperationDate.TabIndex = 374
        Me.DTPExperationDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPExperationDate.Visible = False
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(139, 234)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(42, 13)
        Me.Label68.TabIndex = 373
        Me.Label68.Text = "And/Or"
        '
        'chbExpirationDate
        '
        Me.chbExpirationDate.AutoSize = True
        Me.chbExpirationDate.Location = New System.Drawing.Point(122, 251)
        Me.chbExpirationDate.Name = "chbExpirationDate"
        Me.chbExpirationDate.Size = New System.Drawing.Size(98, 17)
        Me.chbExpirationDate.TabIndex = 372
        Me.chbExpirationDate.Text = "Expiration Date"
        '
        'chbNotifiedAppReceived
        '
        Me.chbNotifiedAppReceived.AutoSize = True
        Me.chbNotifiedAppReceived.Location = New System.Drawing.Point(122, 104)
        Me.chbNotifiedAppReceived.Name = "chbNotifiedAppReceived"
        Me.chbNotifiedAppReceived.Size = New System.Drawing.Size(195, 17)
        Me.chbNotifiedAppReceived.TabIndex = 371
        Me.chbNotifiedAppReceived.Text = "EPA/States  Notified App Received"
        '
        'DTPNotifiedAppReceived
        '
        Me.DTPNotifiedAppReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNotifiedAppReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNotifiedAppReceived.Location = New System.Drawing.Point(12, 102)
        Me.DTPNotifiedAppReceived.Name = "DTPNotifiedAppReceived"
        Me.DTPNotifiedAppReceived.Size = New System.Drawing.Size(95, 20)
        Me.DTPNotifiedAppReceived.TabIndex = 370
        Me.DTPNotifiedAppReceived.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPNotifiedAppReceived.Visible = False
        '
        'lbLinkApplications
        '
        Me.lbLinkApplications.Location = New System.Drawing.Point(310, 352)
        Me.lbLinkApplications.Name = "lbLinkApplications"
        Me.lbLinkApplications.Size = New System.Drawing.Size(13, 4)
        Me.lbLinkApplications.TabIndex = 369
        Me.lbLinkApplications.Visible = False
        '
        'btnReloadGrid
        '
        Me.btnReloadGrid.AutoSize = True
        Me.btnReloadGrid.Location = New System.Drawing.Point(305, 309)
        Me.btnReloadGrid.Name = "btnReloadGrid"
        Me.btnReloadGrid.Size = New System.Drawing.Size(86, 23)
        Me.btnReloadGrid.TabIndex = 368
        Me.btnReloadGrid.Text = "Reload Grid"
        '
        'btnViewApplication
        '
        Me.btnViewApplication.AutoSize = True
        Me.btnViewApplication.Location = New System.Drawing.Point(284, 10)
        Me.btnViewApplication.Name = "btnViewApplication"
        Me.btnViewApplication.Size = New System.Drawing.Size(107, 23)
        Me.btnViewApplication.TabIndex = 367
        Me.btnViewApplication.Text = "View Application"
        '
        'lblLinkWarning
        '
        Me.lblLinkWarning.AutoSize = True
        Me.lblLinkWarning.ForeColor = System.Drawing.Color.Tomato
        Me.lblLinkWarning.Location = New System.Drawing.Point(119, 34)
        Me.lblLinkWarning.Name = "lblLinkWarning"
        Me.lblLinkWarning.Size = New System.Drawing.Size(94, 13)
        Me.lblLinkWarning.TabIndex = 366
        Me.lblLinkWarning.Text = "Application Linked"
        Me.lblLinkWarning.Visible = False
        '
        'btnSearchForApplication
        '
        Me.btnSearchForApplication.AutoSize = True
        Me.btnSearchForApplication.Location = New System.Drawing.Point(215, 10)
        Me.btnSearchForApplication.Name = "btnSearchForApplication"
        Me.btnSearchForApplication.Size = New System.Drawing.Size(63, 23)
        Me.btnSearchForApplication.TabIndex = 365
        Me.btnSearchForApplication.Text = "Search"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(328, 351)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(63, 20)
        Me.btnClear.TabIndex = 364
        Me.btnClear.Text = "Clear"
        '
        'txtWebPublisherApplicationNumber
        '
        Me.txtWebPublisherApplicationNumber.Location = New System.Drawing.Point(122, 11)
        Me.txtWebPublisherApplicationNumber.Name = "txtWebPublisherApplicationNumber"
        Me.txtWebPublisherApplicationNumber.Size = New System.Drawing.Size(83, 20)
        Me.txtWebPublisherApplicationNumber.TabIndex = 363
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 362
        Me.Label1.Text = "Application Number"
        '
        'btnSaveWebPublisher
        '
        Me.btnSaveWebPublisher.Location = New System.Drawing.Point(243, 351)
        Me.btnSaveWebPublisher.Name = "btnSaveWebPublisher"
        Me.btnSaveWebPublisher.Size = New System.Drawing.Size(62, 20)
        Me.btnSaveWebPublisher.TabIndex = 361
        Me.btnSaveWebPublisher.Text = "Save"
        '
        'chbEffectiveDateOfPermit
        '
        Me.chbEffectiveDateOfPermit.AutoSize = True
        Me.chbEffectiveDateOfPermit.Location = New System.Drawing.Point(122, 212)
        Me.chbEffectiveDateOfPermit.Name = "chbEffectiveDateOfPermit"
        Me.chbEffectiveDateOfPermit.Size = New System.Drawing.Size(138, 17)
        Me.chbEffectiveDateOfPermit.TabIndex = 360
        Me.chbEffectiveDateOfPermit.Text = "Effective Date of Permit"
        '
        'chbEPANotifiedPermitOnWeb
        '
        Me.chbEPANotifiedPermitOnWeb.AutoSize = True
        Me.chbEPANotifiedPermitOnWeb.Location = New System.Drawing.Point(122, 302)
        Me.chbEPANotifiedPermitOnWeb.Name = "chbEPANotifiedPermitOnWeb"
        Me.chbEPANotifiedPermitOnWeb.Size = New System.Drawing.Size(161, 17)
        Me.chbEPANotifiedPermitOnWeb.TabIndex = 359
        Me.chbEPANotifiedPermitOnWeb.Text = "EPA Notified Permit On Web"
        '
        'chbFinalOnWeb
        '
        Me.chbFinalOnWeb.AutoSize = True
        Me.chbFinalOnWeb.Location = New System.Drawing.Point(122, 277)
        Me.chbFinalOnWeb.Name = "chbFinalOnWeb"
        Me.chbFinalOnWeb.Size = New System.Drawing.Size(91, 17)
        Me.chbFinalOnWeb.TabIndex = 358
        Me.chbFinalOnWeb.Text = "Final On Web"
        '
        'chbEPAandStatesNotified
        '
        Me.chbEPAandStatesNotified.AutoSize = True
        Me.chbEPAandStatesNotified.Location = New System.Drawing.Point(122, 158)
        Me.chbEPAandStatesNotified.Name = "chbEPAandStatesNotified"
        Me.chbEPAandStatesNotified.Size = New System.Drawing.Size(140, 17)
        Me.chbEPAandStatesNotified.TabIndex = 357
        Me.chbEPAandStatesNotified.Text = "EPA and States Notified"
        '
        'chbDraftOnWeb
        '
        Me.chbDraftOnWeb.AutoSize = True
        Me.chbDraftOnWeb.Location = New System.Drawing.Point(122, 132)
        Me.chbDraftOnWeb.Name = "chbDraftOnWeb"
        Me.chbDraftOnWeb.Size = New System.Drawing.Size(92, 17)
        Me.chbDraftOnWeb.TabIndex = 356
        Me.chbDraftOnWeb.Text = "Draft On Web"
        '
        'txtEPATargetedComments
        '
        Me.txtEPATargetedComments.AcceptsReturn = True
        Me.txtEPATargetedComments.Location = New System.Drawing.Point(30, 379)
        Me.txtEPATargetedComments.Multiline = True
        Me.txtEPATargetedComments.Name = "txtEPATargetedComments"
        Me.txtEPATargetedComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEPATargetedComments.Size = New System.Drawing.Size(361, 86)
        Me.txtEPATargetedComments.TabIndex = 355
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(12, 359)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(129, 13)
        Me.Label43.TabIndex = 354
        Me.Label43.Text = "EPA Targeted Comments:"
        '
        'DTPEffectiveDateofPermit
        '
        Me.DTPEffectiveDateofPermit.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEffectiveDateofPermit.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEffectiveDateofPermit.Location = New System.Drawing.Point(12, 210)
        Me.DTPEffectiveDateofPermit.Name = "DTPEffectiveDateofPermit"
        Me.DTPEffectiveDateofPermit.Size = New System.Drawing.Size(96, 20)
        Me.DTPEffectiveDateofPermit.TabIndex = 353
        Me.DTPEffectiveDateofPermit.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPEffectiveDateofPermit.Visible = False
        '
        'DTPEPANotifiedPermitOnWeb
        '
        Me.DTPEPANotifiedPermitOnWeb.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEPANotifiedPermitOnWeb.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEPANotifiedPermitOnWeb.Location = New System.Drawing.Point(12, 300)
        Me.DTPEPANotifiedPermitOnWeb.Name = "DTPEPANotifiedPermitOnWeb"
        Me.DTPEPANotifiedPermitOnWeb.Size = New System.Drawing.Size(95, 20)
        Me.DTPEPANotifiedPermitOnWeb.TabIndex = 352
        Me.DTPEPANotifiedPermitOnWeb.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPEPANotifiedPermitOnWeb.Visible = False
        '
        'DTPFinalOnWeb
        '
        Me.DTPFinalOnWeb.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFinalOnWeb.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFinalOnWeb.Location = New System.Drawing.Point(12, 275)
        Me.DTPFinalOnWeb.Name = "DTPFinalOnWeb"
        Me.DTPFinalOnWeb.Size = New System.Drawing.Size(95, 20)
        Me.DTPFinalOnWeb.TabIndex = 351
        Me.DTPFinalOnWeb.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPFinalOnWeb.Visible = False
        '
        'DTPEPAStatesNotified
        '
        Me.DTPEPAStatesNotified.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEPAStatesNotified.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEPAStatesNotified.Location = New System.Drawing.Point(12, 156)
        Me.DTPEPAStatesNotified.Name = "DTPEPAStatesNotified"
        Me.DTPEPAStatesNotified.Size = New System.Drawing.Size(95, 20)
        Me.DTPEPAStatesNotified.TabIndex = 350
        Me.DTPEPAStatesNotified.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPEPAStatesNotified.Visible = False
        '
        'DTPDraftOnWeb
        '
        Me.DTPDraftOnWeb.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDraftOnWeb.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDraftOnWeb.Location = New System.Drawing.Point(12, 130)
        Me.DTPDraftOnWeb.Name = "DTPDraftOnWeb"
        Me.DTPDraftOnWeb.Size = New System.Drawing.Size(95, 20)
        Me.DTPDraftOnWeb.TabIndex = 349
        Me.DTPDraftOnWeb.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPDraftOnWeb.Visible = False
        '
        'TPTVEmails
        '
        Me.TPTVEmails.Controls.Add(Me.Panel8)
        Me.TPTVEmails.Controls.Add(Me.Panel7)
        Me.TPTVEmails.Controls.Add(Me.Panel6)
        Me.TPTVEmails.Location = New System.Drawing.Point(4, 22)
        Me.TPTVEmails.Name = "TPTVEmails"
        Me.TPTVEmails.Size = New System.Drawing.Size(815, 469)
        Me.TPTVEmails.TabIndex = 9
        Me.TPTVEmails.Text = "Title V Emails"
        Me.TPTVEmails.UseVisualStyleBackColor = True
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.txtEmailLetter)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 335)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(815, 134)
        Me.Panel8.TabIndex = 14
        '
        'txtEmailLetter
        '
        Me.txtEmailLetter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEmailLetter.Location = New System.Drawing.Point(0, 0)
        Me.txtEmailLetter.Multiline = True
        Me.txtEmailLetter.Name = "txtEmailLetter"
        Me.txtEmailLetter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEmailLetter.Size = New System.Drawing.Size(815, 134)
        Me.txtEmailLetter.TabIndex = 12
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.clbTitleVEmailList)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 150)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(815, 185)
        Me.Panel7.TabIndex = 13
        '
        'clbTitleVEmailList
        '
        Me.clbTitleVEmailList.CheckOnClick = True
        Me.clbTitleVEmailList.ColumnWidth = 200
        Me.clbTitleVEmailList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbTitleVEmailList.FormattingEnabled = True
        Me.clbTitleVEmailList.Location = New System.Drawing.Point(0, 0)
        Me.clbTitleVEmailList.Name = "clbTitleVEmailList"
        Me.clbTitleVEmailList.Size = New System.Drawing.Size(815, 184)
        Me.clbTitleVEmailList.TabIndex = 9
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label70)
        Me.Panel6.Controls.Add(Me.Label60)
        Me.Panel6.Controls.Add(Me.txtApplicationCount)
        Me.Panel6.Controls.Add(Me.Label26)
        Me.Panel6.Controls.Add(Me.btnMinorModOnWebState)
        Me.Panel6.Controls.Add(Me.btnMinorModOnWebEPD)
        Me.Panel6.Controls.Add(Me.btnPreviewMinorMod)
        Me.Panel6.Controls.Add(Me.btnEmailDraftOnWebState)
        Me.Panel6.Controls.Add(Me.txtEmailType)
        Me.Panel6.Controls.Add(Me.Label25)
        Me.Panel6.Controls.Add(Me.btnAddApplicationToList)
        Me.Panel6.Controls.Add(Me.txtApplicationNumberToAdd)
        Me.Panel6.Controls.Add(Me.btnEmailESNReceived)
        Me.Panel6.Controls.Add(Me.btnPreviewFinalOnWeb)
        Me.Panel6.Controls.Add(Me.btnPreviewESNReceived)
        Me.Panel6.Controls.Add(Me.btnEmailFinalOnWeb)
        Me.Panel6.Controls.Add(Me.btnEmailDraftOnWeb)
        Me.Panel6.Controls.Add(Me.btnPreviewDraftOnWeb)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(815, 150)
        Me.Panel6.TabIndex = 8
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(210, 8)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(195, 13)
        Me.Label70.TabIndex = 19
        Me.Label70.Text = "Generate Email from list of Application(s)"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(5, 8)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(142, 13)
        Me.Label60.TabIndex = 18
        Me.Label60.Text = "Preview List of Application(s)"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtApplicationCount
        '
        Me.txtApplicationCount.Location = New System.Drawing.Point(664, 91)
        Me.txtApplicationCount.Name = "txtApplicationCount"
        Me.txtApplicationCount.ReadOnly = True
        Me.txtApplicationCount.Size = New System.Drawing.Size(62, 20)
        Me.txtApplicationCount.TabIndex = 17
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(565, 96)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(93, 13)
        Me.Label26.TabIndex = 16
        Me.Label26.Text = "Application Count:"
        '
        'btnMinorModOnWebState
        '
        Me.btnMinorModOnWebState.AutoSize = True
        Me.btnMinorModOnWebState.Location = New System.Drawing.Point(358, 88)
        Me.btnMinorModOnWebState.Name = "btnMinorModOnWebState"
        Me.btnMinorModOnWebState.Size = New System.Drawing.Size(125, 23)
        Me.btnMinorModOnWebState.TabIndex = 9
        Me.btnMinorModOnWebState.Text = "Minor Proposed (State)"
        Me.btnMinorModOnWebState.UseVisualStyleBackColor = True
        '
        'btnMinorModOnWebEPD
        '
        Me.btnMinorModOnWebEPD.AutoSize = True
        Me.btnMinorModOnWebEPD.Location = New System.Drawing.Point(219, 88)
        Me.btnMinorModOnWebEPD.Name = "btnMinorModOnWebEPD"
        Me.btnMinorModOnWebEPD.Size = New System.Drawing.Size(121, 23)
        Me.btnMinorModOnWebEPD.TabIndex = 8
        Me.btnMinorModOnWebEPD.Text = "Minor Proposed (EPA)"
        Me.btnMinorModOnWebEPD.UseVisualStyleBackColor = True
        '
        'btnPreviewMinorMod
        '
        Me.btnPreviewMinorMod.AutoSize = True
        Me.btnPreviewMinorMod.Location = New System.Drawing.Point(12, 88)
        Me.btnPreviewMinorMod.Name = "btnPreviewMinorMod"
        Me.btnPreviewMinorMod.Size = New System.Drawing.Size(162, 23)
        Me.btnPreviewMinorMod.TabIndex = 3
        Me.btnPreviewMinorMod.Text = "Minor Modification(s) Proposed"
        Me.btnPreviewMinorMod.UseVisualStyleBackColor = True
        '
        'btnEmailDraftOnWebState
        '
        Me.btnEmailDraftOnWebState.AutoSize = True
        Me.btnEmailDraftOnWebState.Location = New System.Drawing.Point(352, 57)
        Me.btnEmailDraftOnWebState.Name = "btnEmailDraftOnWebState"
        Me.btnEmailDraftOnWebState.Size = New System.Drawing.Size(115, 23)
        Me.btnEmailDraftOnWebState.TabIndex = 7
        Me.btnEmailDraftOnWebState.Text = "Draft on Web (State)"
        Me.btnEmailDraftOnWebState.UseVisualStyleBackColor = True
        '
        'txtEmailType
        '
        Me.txtEmailType.Location = New System.Drawing.Point(765, 26)
        Me.txtEmailType.Name = "txtEmailType"
        Me.txtEmailType.Size = New System.Drawing.Size(10, 20)
        Me.txtEmailType.TabIndex = 11
        Me.txtEmailType.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(542, 30)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(116, 13)
        Me.Label25.TabIndex = 10
        Me.Label25.Text = "Add Application To List"
        '
        'btnAddApplicationToList
        '
        Me.btnAddApplicationToList.AutoSize = True
        Me.btnAddApplicationToList.Location = New System.Drawing.Point(664, 52)
        Me.btnAddApplicationToList.Name = "btnAddApplicationToList"
        Me.btnAddApplicationToList.Size = New System.Drawing.Size(91, 23)
        Me.btnAddApplicationToList.TabIndex = 12
        Me.btnAddApplicationToList.Text = "Add Application"
        Me.btnAddApplicationToList.UseVisualStyleBackColor = True
        '
        'txtApplicationNumberToAdd
        '
        Me.txtApplicationNumberToAdd.Location = New System.Drawing.Point(664, 26)
        Me.txtApplicationNumberToAdd.Name = "txtApplicationNumberToAdd"
        Me.txtApplicationNumberToAdd.Size = New System.Drawing.Size(100, 20)
        Me.txtApplicationNumberToAdd.TabIndex = 11
        '
        'btnEmailESNReceived
        '
        Me.btnEmailESNReceived.AutoSize = True
        Me.btnEmailESNReceived.Location = New System.Drawing.Point(219, 26)
        Me.btnEmailESNReceived.Name = "btnEmailESNReceived"
        Me.btnEmailESNReceived.Size = New System.Drawing.Size(183, 23)
        Me.btnEmailESNReceived.TabIndex = 5
        Me.btnEmailESNReceived.Text = "EPA/States Notified App Received"
        Me.btnEmailESNReceived.UseVisualStyleBackColor = True
        '
        'btnPreviewFinalOnWeb
        '
        Me.btnPreviewFinalOnWeb.AutoSize = True
        Me.btnPreviewFinalOnWeb.Location = New System.Drawing.Point(12, 119)
        Me.btnPreviewFinalOnWeb.Name = "btnPreviewFinalOnWeb"
        Me.btnPreviewFinalOnWeb.Size = New System.Drawing.Size(91, 23)
        Me.btnPreviewFinalOnWeb.TabIndex = 4
        Me.btnPreviewFinalOnWeb.Text = "Final(s) on Web"
        Me.btnPreviewFinalOnWeb.UseVisualStyleBackColor = True
        '
        'btnPreviewESNReceived
        '
        Me.btnPreviewESNReceived.AutoSize = True
        Me.btnPreviewESNReceived.Location = New System.Drawing.Point(12, 26)
        Me.btnPreviewESNReceived.Name = "btnPreviewESNReceived"
        Me.btnPreviewESNReceived.Size = New System.Drawing.Size(137, 23)
        Me.btnPreviewESNReceived.TabIndex = 1
        Me.btnPreviewESNReceived.Text = "Application(s) Received"
        Me.btnPreviewESNReceived.UseVisualStyleBackColor = True
        '
        'btnEmailFinalOnWeb
        '
        Me.btnEmailFinalOnWeb.AutoSize = True
        Me.btnEmailFinalOnWeb.Location = New System.Drawing.Point(219, 119)
        Me.btnEmailFinalOnWeb.Name = "btnEmailFinalOnWeb"
        Me.btnEmailFinalOnWeb.Size = New System.Drawing.Size(80, 23)
        Me.btnEmailFinalOnWeb.TabIndex = 10
        Me.btnEmailFinalOnWeb.Text = "Final on Web"
        Me.btnEmailFinalOnWeb.UseVisualStyleBackColor = True
        '
        'btnEmailDraftOnWeb
        '
        Me.btnEmailDraftOnWeb.AutoSize = True
        Me.btnEmailDraftOnWeb.Location = New System.Drawing.Point(219, 57)
        Me.btnEmailDraftOnWeb.Name = "btnEmailDraftOnWeb"
        Me.btnEmailDraftOnWeb.Size = New System.Drawing.Size(111, 23)
        Me.btnEmailDraftOnWeb.TabIndex = 6
        Me.btnEmailDraftOnWeb.Text = "Draft on Web (EPA)"
        Me.btnEmailDraftOnWeb.UseVisualStyleBackColor = True
        '
        'btnPreviewDraftOnWeb
        '
        Me.btnPreviewDraftOnWeb.AutoSize = True
        Me.btnPreviewDraftOnWeb.Location = New System.Drawing.Point(12, 57)
        Me.btnPreviewDraftOnWeb.Name = "btnPreviewDraftOnWeb"
        Me.btnPreviewDraftOnWeb.Size = New System.Drawing.Size(92, 23)
        Me.btnPreviewDraftOnWeb.TabIndex = 2
        Me.btnPreviewDraftOnWeb.Text = "Draft(s) on Web"
        Me.btnPreviewDraftOnWeb.UseVisualStyleBackColor = True
        '
        'TPTitleVRenewals
        '
        Me.TPTitleVRenewals.Controls.Add(Me.clbTitleVRenewals)
        Me.TPTitleVRenewals.Controls.Add(Me.GBTitleVRenewals)
        Me.TPTitleVRenewals.Location = New System.Drawing.Point(4, 22)
        Me.TPTitleVRenewals.Name = "TPTitleVRenewals"
        Me.TPTitleVRenewals.Size = New System.Drawing.Size(815, 469)
        Me.TPTitleVRenewals.TabIndex = 6
        Me.TPTitleVRenewals.Text = "Title V Renewals"
        Me.TPTitleVRenewals.UseVisualStyleBackColor = True
        '
        'clbTitleVRenewals
        '
        Me.clbTitleVRenewals.CheckOnClick = True
        Me.clbTitleVRenewals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbTitleVRenewals.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clbTitleVRenewals.FormattingEnabled = True
        Me.clbTitleVRenewals.HorizontalScrollbar = True
        Me.clbTitleVRenewals.Location = New System.Drawing.Point(0, 147)
        Me.clbTitleVRenewals.Name = "clbTitleVRenewals"
        Me.clbTitleVRenewals.ScrollAlwaysVisible = True
        Me.clbTitleVRenewals.Size = New System.Drawing.Size(815, 319)
        Me.clbTitleVRenewals.TabIndex = 1
        '
        'GBTitleVRenewals
        '
        Me.GBTitleVRenewals.Controls.Add(Me.lblEndDate)
        Me.GBTitleVRenewals.Controls.Add(Me.lblStartDate)
        Me.GBTitleVRenewals.Controls.Add(Me.btnPrintSingleTitleVRenewal)
        Me.GBTitleVRenewals.Controls.Add(Me.txtTitleVSingleLetter)
        Me.GBTitleVRenewals.Controls.Add(Me.btnPrintRenewalLetters)
        Me.GBTitleVRenewals.Controls.Add(Me.DTPTitleVRenewalEnd)
        Me.GBTitleVRenewals.Controls.Add(Me.DTPTitleVRenewalStart)
        Me.GBTitleVRenewals.Controls.Add(Me.btnRunTitleVReport)
        Me.GBTitleVRenewals.Controls.Add(Me.txtRenewalCount)
        Me.GBTitleVRenewals.Controls.Add(Me.Label57)
        Me.GBTitleVRenewals.Controls.Add(Me.Label56)
        Me.GBTitleVRenewals.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBTitleVRenewals.Location = New System.Drawing.Point(0, 0)
        Me.GBTitleVRenewals.Name = "GBTitleVRenewals"
        Me.GBTitleVRenewals.Size = New System.Drawing.Size(815, 147)
        Me.GBTitleVRenewals.TabIndex = 0
        Me.GBTitleVRenewals.TabStop = False
        Me.GBTitleVRenewals.Text = "Title V Renewal(s)"
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(164, 66)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(52, 13)
        Me.lblEndDate.TabIndex = 380
        Me.lblEndDate.Text = "End Date"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(30, 66)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(55, 13)
        Me.lblStartDate.TabIndex = 379
        Me.lblStartDate.Text = "Start Date"
        '
        'btnPrintSingleTitleVRenewal
        '
        Me.btnPrintSingleTitleVRenewal.AutoSize = True
        Me.btnPrintSingleTitleVRenewal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnPrintSingleTitleVRenewal.Location = New System.Drawing.Point(157, 96)
        Me.btnPrintSingleTitleVRenewal.Name = "btnPrintSingleTitleVRenewal"
        Me.btnPrintSingleTitleVRenewal.Size = New System.Drawing.Size(148, 23)
        Me.btnPrintSingleTitleVRenewal.TabIndex = 378
        Me.btnPrintSingleTitleVRenewal.Text = "Print Single Title V Renewal"
        Me.btnPrintSingleTitleVRenewal.UseVisualStyleBackColor = True
        '
        'txtTitleVSingleLetter
        '
        Me.txtTitleVSingleLetter.Location = New System.Drawing.Point(42, 96)
        Me.txtTitleVSingleLetter.Name = "txtTitleVSingleLetter"
        Me.txtTitleVSingleLetter.Size = New System.Drawing.Size(100, 20)
        Me.txtTitleVSingleLetter.TabIndex = 377
        '
        'btnPrintRenewalLetters
        '
        Me.btnPrintRenewalLetters.Location = New System.Drawing.Point(440, 42)
        Me.btnPrintRenewalLetters.Name = "btnPrintRenewalLetters"
        Me.btnPrintRenewalLetters.Size = New System.Drawing.Size(73, 20)
        Me.btnPrintRenewalLetters.TabIndex = 376
        Me.btnPrintRenewalLetters.Text = "Print Letters"
        '
        'DTPTitleVRenewalEnd
        '
        Me.DTPTitleVRenewalEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTitleVRenewalEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTitleVRenewalEnd.Location = New System.Drawing.Point(167, 42)
        Me.DTPTitleVRenewalEnd.Name = "DTPTitleVRenewalEnd"
        Me.DTPTitleVRenewalEnd.Size = New System.Drawing.Size(95, 20)
        Me.DTPTitleVRenewalEnd.TabIndex = 371
        Me.DTPTitleVRenewalEnd.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPTitleVRenewalStart
        '
        Me.DTPTitleVRenewalStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTitleVRenewalStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTitleVRenewalStart.Location = New System.Drawing.Point(33, 42)
        Me.DTPTitleVRenewalStart.Name = "DTPTitleVRenewalStart"
        Me.DTPTitleVRenewalStart.Size = New System.Drawing.Size(96, 20)
        Me.DTPTitleVRenewalStart.TabIndex = 372
        Me.DTPTitleVRenewalStart.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'btnRunTitleVReport
        '
        Me.btnRunTitleVReport.Location = New System.Drawing.Point(287, 42)
        Me.btnRunTitleVReport.Name = "btnRunTitleVReport"
        Me.btnRunTitleVReport.Size = New System.Drawing.Size(86, 20)
        Me.btnRunTitleVReport.TabIndex = 0
        Me.btnRunTitleVReport.Text = "Run Report"
        '
        'txtRenewalCount
        '
        Me.txtRenewalCount.Location = New System.Drawing.Point(400, 42)
        Me.txtRenewalCount.Name = "txtRenewalCount"
        Me.txtRenewalCount.ReadOnly = True
        Me.txtRenewalCount.Size = New System.Drawing.Size(27, 20)
        Me.txtRenewalCount.TabIndex = 373
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(140, 21)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(55, 13)
        Me.Label57.TabIndex = 375
        Me.Label57.Text = "End Date:"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(7, 21)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(58, 13)
        Me.Label56.TabIndex = 374
        Me.Label56.Text = "Start Date:"
        '
        'TPPermittingContact
        '
        Me.TPPermittingContact.Controls.Add(Me.txtContactDescriptionCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactEmailAddressCompliance)
        Me.TPPermittingContact.Controls.Add(Me.mtbContactFaxNumberCompliance)
        Me.TPPermittingContact.Controls.Add(Me.mtbContactPhoneNumberCompliance)
        Me.TPPermittingContact.Controls.Add(Me.mtbContactZipCodeCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactStateCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactCityCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactStreetAddressCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactTitleCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactCompanyNameCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactPedigreeCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactLastNameCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactFirstNameCompliance)
        Me.TPPermittingContact.Controls.Add(Me.txtContactSocialTitleCompliance)
        Me.TPPermittingContact.Controls.Add(Me.btnLoadFromWarehouse)
        Me.TPPermittingContact.Controls.Add(Me.txtAIRSNumber)
        Me.TPPermittingContact.Controls.Add(Me.Label5)
        Me.TPPermittingContact.Controls.Add(Me.btnSaveContactApp)
        Me.TPPermittingContact.Controls.Add(Me.btnLoadAppContact)
        Me.TPPermittingContact.Controls.Add(Me.Label4)
        Me.TPPermittingContact.Controls.Add(Me.txtApplicationNumber)
        Me.TPPermittingContact.Controls.Add(Me.btnGetCurrentPermittingContact)
        Me.TPPermittingContact.Controls.Add(Me.mtbContactZipCode)
        Me.TPPermittingContact.Controls.Add(Me.mtbContactFaxNumber)
        Me.TPPermittingContact.Controls.Add(Me.mtbContactPhoneNumber)
        Me.TPPermittingContact.Controls.Add(Me.txtContactDescription)
        Me.TPPermittingContact.Controls.Add(Me.txtContactEmailAddress)
        Me.TPPermittingContact.Controls.Add(Me.txtContactState)
        Me.TPPermittingContact.Controls.Add(Me.txtContactCity)
        Me.TPPermittingContact.Controls.Add(Me.txtContactStreetAddress)
        Me.TPPermittingContact.Controls.Add(Me.txtContactTitle)
        Me.TPPermittingContact.Controls.Add(Me.txtContactCompanyName)
        Me.TPPermittingContact.Controls.Add(Me.txtContactPedigree)
        Me.TPPermittingContact.Controls.Add(Me.txtContactSocialTitle)
        Me.TPPermittingContact.Controls.Add(Me.txtContactLastName)
        Me.TPPermittingContact.Controls.Add(Me.txtContactFirstName)
        Me.TPPermittingContact.Controls.Add(Me.Label46)
        Me.TPPermittingContact.Controls.Add(Me.Label45)
        Me.TPPermittingContact.Controls.Add(Me.Label40)
        Me.TPPermittingContact.Controls.Add(Me.Label39)
        Me.TPPermittingContact.Controls.Add(Me.Label44)
        Me.TPPermittingContact.Controls.Add(Me.Label41)
        Me.TPPermittingContact.Controls.Add(Me.Label38)
        Me.TPPermittingContact.Controls.Add(Me.Label32)
        Me.TPPermittingContact.Controls.Add(Me.Label27)
        Me.TPPermittingContact.Controls.Add(Me.Label2)
        Me.TPPermittingContact.Controls.Add(Me.Label20)
        Me.TPPermittingContact.Controls.Add(Me.Label18)
        Me.TPPermittingContact.Controls.Add(Me.Label3)
        Me.TPPermittingContact.Controls.Add(Me.Label11)
        Me.TPPermittingContact.Location = New System.Drawing.Point(4, 22)
        Me.TPPermittingContact.Name = "TPPermittingContact"
        Me.TPPermittingContact.Size = New System.Drawing.Size(815, 469)
        Me.TPPermittingContact.TabIndex = 10
        Me.TPPermittingContact.Text = " Permitting App Contact"
        Me.TPPermittingContact.UseVisualStyleBackColor = True
        '
        'txtContactDescriptionCompliance
        '
        Me.txtContactDescriptionCompliance.Location = New System.Drawing.Point(736, 276)
        Me.txtContactDescriptionCompliance.Name = "txtContactDescriptionCompliance"
        Me.txtContactDescriptionCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactDescriptionCompliance.TabIndex = 416
        Me.txtContactDescriptionCompliance.Visible = False
        '
        'txtContactEmailAddressCompliance
        '
        Me.txtContactEmailAddressCompliance.Location = New System.Drawing.Point(392, 241)
        Me.txtContactEmailAddressCompliance.Name = "txtContactEmailAddressCompliance"
        Me.txtContactEmailAddressCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactEmailAddressCompliance.TabIndex = 415
        Me.txtContactEmailAddressCompliance.Visible = False
        '
        'mtbContactFaxNumberCompliance
        '
        Me.mtbContactFaxNumberCompliance.Location = New System.Drawing.Point(194, 210)
        Me.mtbContactFaxNumberCompliance.Mask = "(999) 000-0000"
        Me.mtbContactFaxNumberCompliance.Name = "mtbContactFaxNumberCompliance"
        Me.mtbContactFaxNumberCompliance.Size = New System.Drawing.Size(16, 20)
        Me.mtbContactFaxNumberCompliance.TabIndex = 414
        Me.mtbContactFaxNumberCompliance.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbContactFaxNumberCompliance.Visible = False
        '
        'mtbContactPhoneNumberCompliance
        '
        Me.mtbContactPhoneNumberCompliance.Location = New System.Drawing.Point(194, 184)
        Me.mtbContactPhoneNumberCompliance.Mask = "(999) 000-0000"
        Me.mtbContactPhoneNumberCompliance.Name = "mtbContactPhoneNumberCompliance"
        Me.mtbContactPhoneNumberCompliance.Size = New System.Drawing.Size(16, 20)
        Me.mtbContactPhoneNumberCompliance.TabIndex = 413
        Me.mtbContactPhoneNumberCompliance.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbContactPhoneNumberCompliance.Visible = False
        '
        'mtbContactZipCodeCompliance
        '
        Me.mtbContactZipCodeCompliance.Location = New System.Drawing.Point(736, 149)
        Me.mtbContactZipCodeCompliance.Mask = "00000-9999"
        Me.mtbContactZipCodeCompliance.Name = "mtbContactZipCodeCompliance"
        Me.mtbContactZipCodeCompliance.Size = New System.Drawing.Size(13, 20)
        Me.mtbContactZipCodeCompliance.TabIndex = 412
        Me.mtbContactZipCodeCompliance.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbContactZipCodeCompliance.Visible = False
        '
        'txtContactStateCompliance
        '
        Me.txtContactStateCompliance.Location = New System.Drawing.Point(594, 150)
        Me.txtContactStateCompliance.Name = "txtContactStateCompliance"
        Me.txtContactStateCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactStateCompliance.TabIndex = 411
        Me.txtContactStateCompliance.Visible = False
        '
        'txtContactCityCompliance
        '
        Me.txtContactCityCompliance.Location = New System.Drawing.Point(502, 149)
        Me.txtContactCityCompliance.Name = "txtContactCityCompliance"
        Me.txtContactCityCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactCityCompliance.TabIndex = 410
        Me.txtContactCityCompliance.Visible = False
        '
        'txtContactStreetAddressCompliance
        '
        Me.txtContactStreetAddressCompliance.Location = New System.Drawing.Point(735, 122)
        Me.txtContactStreetAddressCompliance.Name = "txtContactStreetAddressCompliance"
        Me.txtContactStreetAddressCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactStreetAddressCompliance.TabIndex = 409
        Me.txtContactStreetAddressCompliance.Visible = False
        '
        'txtContactTitleCompliance
        '
        Me.txtContactTitleCompliance.Location = New System.Drawing.Point(735, 95)
        Me.txtContactTitleCompliance.Name = "txtContactTitleCompliance"
        Me.txtContactTitleCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactTitleCompliance.TabIndex = 408
        Me.txtContactTitleCompliance.Visible = False
        '
        'txtContactCompanyNameCompliance
        '
        Me.txtContactCompanyNameCompliance.Location = New System.Drawing.Point(735, 68)
        Me.txtContactCompanyNameCompliance.Name = "txtContactCompanyNameCompliance"
        Me.txtContactCompanyNameCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactCompanyNameCompliance.TabIndex = 407
        Me.txtContactCompanyNameCompliance.Visible = False
        '
        'txtContactPedigreeCompliance
        '
        Me.txtContactPedigreeCompliance.Location = New System.Drawing.Point(157, 150)
        Me.txtContactPedigreeCompliance.Name = "txtContactPedigreeCompliance"
        Me.txtContactPedigreeCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactPedigreeCompliance.TabIndex = 406
        Me.txtContactPedigreeCompliance.Visible = False
        '
        'txtContactLastNameCompliance
        '
        Me.txtContactLastNameCompliance.Location = New System.Drawing.Point(238, 122)
        Me.txtContactLastNameCompliance.Name = "txtContactLastNameCompliance"
        Me.txtContactLastNameCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactLastNameCompliance.TabIndex = 405
        Me.txtContactLastNameCompliance.Visible = False
        '
        'txtContactFirstNameCompliance
        '
        Me.txtContactFirstNameCompliance.Location = New System.Drawing.Point(238, 94)
        Me.txtContactFirstNameCompliance.Name = "txtContactFirstNameCompliance"
        Me.txtContactFirstNameCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactFirstNameCompliance.TabIndex = 404
        Me.txtContactFirstNameCompliance.Visible = False
        '
        'txtContactSocialTitleCompliance
        '
        Me.txtContactSocialTitleCompliance.Location = New System.Drawing.Point(159, 67)
        Me.txtContactSocialTitleCompliance.Name = "txtContactSocialTitleCompliance"
        Me.txtContactSocialTitleCompliance.Size = New System.Drawing.Size(13, 20)
        Me.txtContactSocialTitleCompliance.TabIndex = 403
        Me.txtContactSocialTitleCompliance.Visible = False
        '
        'btnLoadFromWarehouse
        '
        Me.btnLoadFromWarehouse.AutoSize = True
        Me.btnLoadFromWarehouse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadFromWarehouse.Location = New System.Drawing.Point(83, 38)
        Me.btnLoadFromWarehouse.Name = "btnLoadFromWarehouse"
        Me.btnLoadFromWarehouse.Size = New System.Drawing.Size(218, 23)
        Me.btnLoadFromWarehouse.TabIndex = 402
        Me.btnLoadFromWarehouse.Text = "Load Contact Info. from GATV Warehouse"
        Me.btnLoadFromWarehouse.UseVisualStyleBackColor = True
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(449, 13)
        Me.txtAIRSNumber.MaxLength = 12
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(67, 20)
        Me.txtAIRSNumber.TabIndex = 400
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(373, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 401
        Me.Label5.Text = "AIRS Number"
        '
        'btnSaveContactApp
        '
        Me.btnSaveContactApp.AutoSize = True
        Me.btnSaveContactApp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveContactApp.Location = New System.Drawing.Point(102, 393)
        Me.btnSaveContactApp.Name = "btnSaveContactApp"
        Me.btnSaveContactApp.Size = New System.Drawing.Size(149, 23)
        Me.btnSaveContactApp.TabIndex = 15
        Me.btnSaveContactApp.Text = "Save Contact to Application"
        Me.btnSaveContactApp.UseVisualStyleBackColor = True
        '
        'btnLoadAppContact
        '
        Me.btnLoadAppContact.AutoSize = True
        Me.btnLoadAppContact.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadAppContact.Location = New System.Drawing.Point(156, 10)
        Me.btnLoadAppContact.Name = "btnLoadAppContact"
        Me.btnLoadAppContact.Size = New System.Drawing.Size(183, 23)
        Me.btnLoadAppContact.TabIndex = 398
        Me.btnLoadAppContact.Text = "Load Contact Info. from Application"
        Me.btnLoadAppContact.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 397
        Me.Label4.Text = "Application No."
        '
        'txtApplicationNumber
        '
        Me.txtApplicationNumber.Location = New System.Drawing.Point(83, 12)
        Me.txtApplicationNumber.MaxLength = 10
        Me.txtApplicationNumber.Name = "txtApplicationNumber"
        Me.txtApplicationNumber.Size = New System.Drawing.Size(67, 20)
        Me.txtApplicationNumber.TabIndex = 0
        '
        'btnGetCurrentPermittingContact
        '
        Me.btnGetCurrentPermittingContact.AutoSize = True
        Me.btnGetCurrentPermittingContact.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnGetCurrentPermittingContact.Location = New System.Drawing.Point(522, 10)
        Me.btnGetCurrentPermittingContact.Name = "btnGetCurrentPermittingContact"
        Me.btnGetCurrentPermittingContact.Size = New System.Drawing.Size(189, 23)
        Me.btnGetCurrentPermittingContact.TabIndex = 395
        Me.btnGetCurrentPermittingContact.Text = "Load Contact Info. from Facility Data"
        Me.btnGetCurrentPermittingContact.UseVisualStyleBackColor = True
        '
        'mtbContactZipCode
        '
        Me.mtbContactZipCode.Location = New System.Drawing.Point(662, 150)
        Me.mtbContactZipCode.Mask = "00000-9999"
        Me.mtbContactZipCode.Name = "mtbContactZipCode"
        Me.mtbContactZipCode.Size = New System.Drawing.Size(68, 20)
        Me.mtbContactZipCode.TabIndex = 10
        Me.mtbContactZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbContactFaxNumber
        '
        Me.mtbContactFaxNumber.Location = New System.Drawing.Point(102, 210)
        Me.mtbContactFaxNumber.Mask = "(999) 000-0000"
        Me.mtbContactFaxNumber.Name = "mtbContactFaxNumber"
        Me.mtbContactFaxNumber.Size = New System.Drawing.Size(89, 20)
        Me.mtbContactFaxNumber.TabIndex = 12
        Me.mtbContactFaxNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbContactPhoneNumber
        '
        Me.mtbContactPhoneNumber.Location = New System.Drawing.Point(102, 184)
        Me.mtbContactPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbContactPhoneNumber.Name = "mtbContactPhoneNumber"
        Me.mtbContactPhoneNumber.Size = New System.Drawing.Size(89, 20)
        Me.mtbContactPhoneNumber.TabIndex = 11
        Me.mtbContactPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtContactDescription
        '
        Me.txtContactDescription.Location = New System.Drawing.Point(102, 276)
        Me.txtContactDescription.Multiline = True
        Me.txtContactDescription.Name = "txtContactDescription"
        Me.txtContactDescription.Size = New System.Drawing.Size(628, 96)
        Me.txtContactDescription.TabIndex = 14
        '
        'txtContactEmailAddress
        '
        Me.txtContactEmailAddress.Location = New System.Drawing.Point(102, 241)
        Me.txtContactEmailAddress.Name = "txtContactEmailAddress"
        Me.txtContactEmailAddress.Size = New System.Drawing.Size(284, 20)
        Me.txtContactEmailAddress.TabIndex = 13
        '
        'txtContactState
        '
        Me.txtContactState.Location = New System.Drawing.Point(553, 150)
        Me.txtContactState.MaxLength = 2
        Me.txtContactState.Name = "txtContactState"
        Me.txtContactState.Size = New System.Drawing.Size(40, 20)
        Me.txtContactState.TabIndex = 9
        '
        'txtContactCity
        '
        Me.txtContactCity.Location = New System.Drawing.Point(345, 150)
        Me.txtContactCity.Name = "txtContactCity"
        Me.txtContactCity.Size = New System.Drawing.Size(152, 20)
        Me.txtContactCity.TabIndex = 8
        '
        'txtContactStreetAddress
        '
        Me.txtContactStreetAddress.Location = New System.Drawing.Point(345, 122)
        Me.txtContactStreetAddress.Name = "txtContactStreetAddress"
        Me.txtContactStreetAddress.Size = New System.Drawing.Size(385, 20)
        Me.txtContactStreetAddress.TabIndex = 7
        '
        'txtContactTitle
        '
        Me.txtContactTitle.Location = New System.Drawing.Point(345, 94)
        Me.txtContactTitle.Name = "txtContactTitle"
        Me.txtContactTitle.Size = New System.Drawing.Size(385, 20)
        Me.txtContactTitle.TabIndex = 6
        '
        'txtContactCompanyName
        '
        Me.txtContactCompanyName.Location = New System.Drawing.Point(345, 68)
        Me.txtContactCompanyName.Name = "txtContactCompanyName"
        Me.txtContactCompanyName.Size = New System.Drawing.Size(385, 20)
        Me.txtContactCompanyName.TabIndex = 5
        '
        'txtContactPedigree
        '
        Me.txtContactPedigree.Location = New System.Drawing.Point(81, 150)
        Me.txtContactPedigree.Name = "txtContactPedigree"
        Me.txtContactPedigree.Size = New System.Drawing.Size(72, 20)
        Me.txtContactPedigree.TabIndex = 4
        '
        'txtContactSocialTitle
        '
        Me.txtContactSocialTitle.Location = New System.Drawing.Point(81, 68)
        Me.txtContactSocialTitle.Name = "txtContactSocialTitle"
        Me.txtContactSocialTitle.Size = New System.Drawing.Size(72, 20)
        Me.txtContactSocialTitle.TabIndex = 1
        '
        'txtContactLastName
        '
        Me.txtContactLastName.Location = New System.Drawing.Point(81, 122)
        Me.txtContactLastName.Name = "txtContactLastName"
        Me.txtContactLastName.Size = New System.Drawing.Size(153, 20)
        Me.txtContactLastName.TabIndex = 3
        '
        'txtContactFirstName
        '
        Me.txtContactFirstName.Location = New System.Drawing.Point(81, 94)
        Me.txtContactFirstName.Name = "txtContactFirstName"
        Me.txtContactFirstName.Size = New System.Drawing.Size(153, 20)
        Me.txtContactFirstName.TabIndex = 2
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(19, 279)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(60, 13)
        Me.Label46.TabIndex = 394
        Me.Label46.Text = "Description"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(257, 72)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(82, 13)
        Me.Label45.TabIndex = 393
        Me.Label45.Text = "Company Name"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(19, 72)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(59, 13)
        Me.Label40.TabIndex = 392
        Me.Label40.Text = "Social Title"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(19, 154)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(49, 13)
        Me.Label39.TabIndex = 391
        Me.Label39.Text = "Pedigree"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(522, 154)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(32, 13)
        Me.Label44.TabIndex = 390
        Me.Label44.Text = "State"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(606, 154)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(50, 13)
        Me.Label41.TabIndex = 389
        Me.Label41.Text = "Zip Code"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(19, 126)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(58, 13)
        Me.Label38.TabIndex = 388
        Me.Label38.Text = "Last Name"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(257, 98)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(27, 13)
        Me.Label32.TabIndex = 387
        Me.Label32.Text = "Title"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(18, 188)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(78, 13)
        Me.Label27.TabIndex = 386
        Me.Label27.Text = "Phone Number"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 213)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 385
        Me.Label2.Text = "Fax Number"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(19, 244)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 13)
        Me.Label20.TabIndex = 384
        Me.Label20.Text = "Email Address"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(257, 126)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(76, 13)
        Me.Label18.TabIndex = 383
        Me.Label18.Text = "Street Address"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(257, 154)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 382
        Me.Label3.Text = "City"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(19, 98)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(57, 13)
        Me.Label11.TabIndex = 381
        Me.Label11.Text = "First Name"
        '
        'TSDMUStaffTools
        '
        Me.TSDMUStaffTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack})
        Me.TSDMUStaffTools.Location = New System.Drawing.Point(0, 0)
        Me.TSDMUStaffTools.Name = "TSDMUStaffTools"
        Me.TSDMUStaffTools.Size = New System.Drawing.Size(823, 25)
        Me.TSDMUStaffTools.TabIndex = 259
        Me.TSDMUStaffTools.Text = "ToolStrip1"
        '
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = CType(resources.GetObject("tsbBack.Image"), System.Drawing.Image)
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        Me.tsbBack.Text = "Back"
        '
        'DMUTitleVTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(823, 542)
        Me.Controls.Add(Me.TCDMUTools)
        Me.Controls.Add(Me.TSDMUStaffTools)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Menu = Me.MainMenu1
        Me.Name = "DMUTitleVTools"
        Me.Text = "DMU Title V Tools"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TCDMUTools.ResumeLayout(False)
        Me.TPWebPublishing.ResumeLayout(False)
        CType(Me.dgrWebPublisher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.TPTVEmails.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.TPTitleVRenewals.ResumeLayout(False)
        Me.GBTitleVRenewals.ResumeLayout(False)
        Me.GBTitleVRenewals.PerformLayout()
        Me.TPPermittingContact.ResumeLayout(False)
        Me.TPPermittingContact.PerformLayout()
        Me.TSDMUStaffTools.ResumeLayout(False)
        Me.TSDMUStaffTools.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiView As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents bgwTransfer As System.ComponentModel.BackgroundWorker
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TCDMUTools As System.Windows.Forms.TabControl
    Friend WithEvents TPWebPublishing As System.Windows.Forms.TabPage
    Friend WithEvents dgrWebPublisher As System.Windows.Forms.DataGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtTVCount As System.Windows.Forms.TextBox
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityInformation As System.Windows.Forms.TextBox
    Friend WithEvents chbPNExpires As System.Windows.Forms.CheckBox
    Friend WithEvents DTPPNExpires As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPExperationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents chbExpirationDate As System.Windows.Forms.CheckBox
    Friend WithEvents chbNotifiedAppReceived As System.Windows.Forms.CheckBox
    Friend WithEvents DTPNotifiedAppReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbLinkApplications As System.Windows.Forms.ListBox
    Friend WithEvents btnReloadGrid As System.Windows.Forms.Button
    Friend WithEvents btnViewApplication As System.Windows.Forms.Button
    Friend WithEvents lblLinkWarning As System.Windows.Forms.Label
    Friend WithEvents btnSearchForApplication As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtWebPublisherApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSaveWebPublisher As System.Windows.Forms.Button
    Friend WithEvents chbEffectiveDateOfPermit As System.Windows.Forms.CheckBox
    Friend WithEvents chbEPANotifiedPermitOnWeb As System.Windows.Forms.CheckBox
    Friend WithEvents chbFinalOnWeb As System.Windows.Forms.CheckBox
    Friend WithEvents chbEPAandStatesNotified As System.Windows.Forms.CheckBox
    Friend WithEvents chbDraftOnWeb As System.Windows.Forms.CheckBox
    Friend WithEvents txtEPATargetedComments As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents DTPEffectiveDateofPermit As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPEPANotifiedPermitOnWeb As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPFinalOnWeb As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPEPAStatesNotified As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDraftOnWeb As System.Windows.Forms.DateTimePicker
    Friend WithEvents TPTVEmails As System.Windows.Forms.TabPage
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents txtEmailLetter As System.Windows.Forms.TextBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents clbTitleVEmailList As System.Windows.Forms.CheckedListBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents txtApplicationCount As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents btnMinorModOnWebState As System.Windows.Forms.Button
    Friend WithEvents btnMinorModOnWebEPD As System.Windows.Forms.Button
    Friend WithEvents btnPreviewMinorMod As System.Windows.Forms.Button
    Friend WithEvents btnEmailDraftOnWebState As System.Windows.Forms.Button
    Friend WithEvents txtEmailType As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnAddApplicationToList As System.Windows.Forms.Button
    Friend WithEvents txtApplicationNumberToAdd As System.Windows.Forms.TextBox
    Friend WithEvents btnEmailESNReceived As System.Windows.Forms.Button
    Friend WithEvents btnPreviewFinalOnWeb As System.Windows.Forms.Button
    Friend WithEvents btnPreviewESNReceived As System.Windows.Forms.Button
    Friend WithEvents btnEmailFinalOnWeb As System.Windows.Forms.Button
    Friend WithEvents btnEmailDraftOnWeb As System.Windows.Forms.Button
    Friend WithEvents btnPreviewDraftOnWeb As System.Windows.Forms.Button
    Friend WithEvents TPTitleVRenewals As System.Windows.Forms.TabPage
    Friend WithEvents clbTitleVRenewals As System.Windows.Forms.CheckedListBox
    Friend WithEvents GBTitleVRenewals As System.Windows.Forms.GroupBox
    Friend WithEvents lblEndDate As System.Windows.Forms.Label
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents btnPrintSingleTitleVRenewal As System.Windows.Forms.Button
    Friend WithEvents txtTitleVSingleLetter As System.Windows.Forms.TextBox
    Friend WithEvents btnPrintRenewalLetters As System.Windows.Forms.Button
    Friend WithEvents DTPTitleVRenewalEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPTitleVRenewalStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnRunTitleVReport As System.Windows.Forms.Button
    Friend WithEvents txtRenewalCount As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents TSDMUStaffTools As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents TPPermittingContact As System.Windows.Forms.TabPage
    Friend WithEvents btnGetCurrentPermittingContact As System.Windows.Forms.Button
    Friend WithEvents mtbContactZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbContactFaxNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbContactPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtContactDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtContactEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtContactState As System.Windows.Forms.TextBox
    Friend WithEvents txtContactCity As System.Windows.Forms.TextBox
    Friend WithEvents txtContactStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtContactTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtContactCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents txtContactPedigree As System.Windows.Forms.TextBox
    Friend WithEvents txtContactSocialTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtContactLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtContactFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnSaveContactApp As System.Windows.Forms.Button
    Friend WithEvents btnLoadAppContact As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtApplicationNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnLoadFromWarehouse As System.Windows.Forms.Button
    Friend WithEvents txtContactLastNameCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactFirstNameCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactSocialTitleCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactDescriptionCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactEmailAddressCompliance As System.Windows.Forms.TextBox
    Friend WithEvents mtbContactFaxNumberCompliance As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbContactPhoneNumberCompliance As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbContactZipCodeCompliance As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtContactStateCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactCityCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactStreetAddressCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactTitleCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactCompanyNameCompliance As System.Windows.Forms.TextBox
    Friend WithEvents txtContactPedigreeCompliance As System.Windows.Forms.TextBox
End Class
