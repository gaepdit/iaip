<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MASPRegistrationTool
    Inherits BaseForm

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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chbGECOlogInRequired = New System.Windows.Forms.CheckBox()
        Me.txtWebsiteURL = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.txtEventTitle = New System.Windows.Forms.TextBox()
        Me.btnClearEventManagement = New System.Windows.Forms.Button()
        Me.txtEventEndTime = New System.Windows.Forms.TextBox()
        Me.mtbEventPhoneNumber = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtEventDescription = New System.Windows.Forms.TextBox()
        Me.cboEventContact = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.btnGeneratePasscode = New System.Windows.Forms.Button()
        Me.txtEventVenue = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.DTPEventEndDate = New System.Windows.Forms.DateTimePicker()
        Me.chbEventPasscode = New System.Windows.Forms.CheckBox()
        Me.btnDeleteEvent = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnUpdateEvent = New System.Windows.Forms.Button()
        Me.DTPEventDate = New System.Windows.Forms.DateTimePicker()
        Me.btnSaveNewEvent = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtEventNotes = New System.Windows.Forms.TextBox()
        Me.mtbEventCapacity = New System.Windows.Forms.MaskedTextBox()
        Me.cboEventStatus = New System.Windows.Forms.ComboBox()
        Me.txtEventTime = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboEventWebContact = New System.Windows.Forms.ComboBox()
        Me.btnMapEventLocation = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.mtbEventZipCode = New System.Windows.Forms.MaskedTextBox()
        Me.mtbEventWebPhoneNumber = New System.Windows.Forms.MaskedTextBox()
        Me.mtbEventState = New System.Windows.Forms.MaskedTextBox()
        Me.txtEventAddress = New System.Windows.Forms.TextBox()
        Me.txtEventCity = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.tabsEventDetails = New System.Windows.Forms.TabControl()
        Me.tabEventOverview = New System.Windows.Forms.TabPage()
        Me.dgvOverviewRegistrants = New System.Windows.Forms.DataGridView()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.chbOvLoginRequired = New System.Windows.Forms.CheckBox()
        Me.txtOvNotes = New System.Windows.Forms.TextBox()
        Me.txtOvVenue = New System.Windows.Forms.TextBox()
        Me.txtOvAPBContact = New System.Windows.Forms.TextBox()
        Me.txtOvWebContact = New System.Windows.Forms.TextBox()
        Me.txtOvCancelled = New System.Windows.Forms.TextBox()
        Me.txtOvWaitingList = New System.Windows.Forms.TextBox()
        Me.txtOvEventCapacity = New System.Windows.Forms.TextBox()
        Me.txtOvNumberRegistered = New System.Windows.Forms.TextBox()
        Me.txtOvEventStatus = New System.Windows.Forms.TextBox()
        Me.txtOvPassCode = New System.Windows.Forms.TextBox()
        Me.txtOvEventDateTime = New System.Windows.Forms.TextBox()
        Me.txtOvEvent = New System.Windows.Forms.TextBox()
        Me.txtOvDescription = New System.Windows.Forms.TextBox()
        Me.txtEmails = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.btnEmailWaitList = New System.Windows.Forms.Button()
        Me.lblCancelled = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.btnEmailRegistrants = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.btnEmailAll = New System.Windows.Forms.Button()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.btnExportRegistrantsToExcel = New System.Windows.Forms.Button()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.tabEventManagement = New System.Windows.Forms.TabPage()
        Me.tabRegistrationManagement = New System.Windows.Forms.TabPage()
        Me.dgvRegistrationManagement = New System.Windows.Forms.DataGridView()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.btnModifyRegistration = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtRegEmail = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtRegComments = New System.Windows.Forms.TextBox()
        Me.DTPRegDateRegistered = New System.Windows.Forms.DateTimePicker()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtGECOUserID = New System.Windows.Forms.TextBox()
        Me.cboRegUserType = New System.Windows.Forms.ComboBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.mtbRegPhoneExt = New System.Windows.Forms.MaskedTextBox()
        Me.mtbRegPhoneNo = New System.Windows.Forms.MaskedTextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.mtbRegZipCode = New System.Windows.Forms.MaskedTextBox()
        Me.mtbRegState = New System.Windows.Forms.MaskedTextBox()
        Me.txtRegCity = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtRegAddress = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtRegTitle = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtRegLastName = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtRegFirstName = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtRegSalutation = New System.Windows.Forms.TextBox()
        Me.cboRegStatus = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtRegConfirmationNum = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRegEventTitle = New System.Windows.Forms.TextBox()
        Me.pnlEventsAndFilter = New System.Windows.Forms.Panel()
        Me.dgvEvents = New System.Windows.Forms.DataGridView()
        Me.pnlFilterEvents = New System.Windows.Forms.Panel()
        Me.lblFilterEvents = New System.Windows.Forms.Label()
        Me.rdbEventsFilterAll = New System.Windows.Forms.RadioButton()
        Me.rdbEventsFilterPast = New System.Windows.Forms.RadioButton()
        Me.rdbEventsFilterFuture = New System.Windows.Forms.RadioButton()
        Me.pnlEventTitle = New System.Windows.Forms.Panel()
        Me.btnViewDetails = New System.Windows.Forms.Button()
        Me.lblEventTitle = New System.Windows.Forms.Label()
        Me.lblEventDate = New System.Windows.Forms.Label()
        Me.Panel4.SuspendLayout()
        Me.tabsEventDetails.SuspendLayout()
        Me.tabEventOverview.SuspendLayout()
        CType(Me.dgvOverviewRegistrants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.tabEventManagement.SuspendLayout()
        Me.tabRegistrationManagement.SuspendLayout()
        CType(Me.dgvRegistrationManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.pnlEventsAndFilter.SuspendLayout()
        CType(Me.dgvEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilterEvents.SuspendLayout()
        Me.pnlEventTitle.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chbGECOlogInRequired)
        Me.Panel4.Controls.Add(Me.txtWebsiteURL)
        Me.Panel4.Controls.Add(Me.Label48)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label47)
        Me.Panel4.Controls.Add(Me.txtEventTitle)
        Me.Panel4.Controls.Add(Me.btnClearEventManagement)
        Me.Panel4.Controls.Add(Me.txtEventEndTime)
        Me.Panel4.Controls.Add(Me.mtbEventPhoneNumber)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.txtEventDescription)
        Me.Panel4.Controls.Add(Me.cboEventContact)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label32)
        Me.Panel4.Controls.Add(Me.btnGeneratePasscode)
        Me.Panel4.Controls.Add(Me.txtEventVenue)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.DTPEventEndDate)
        Me.Panel4.Controls.Add(Me.chbEventPasscode)
        Me.Panel4.Controls.Add(Me.btnDeleteEvent)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.btnUpdateEvent)
        Me.Panel4.Controls.Add(Me.DTPEventDate)
        Me.Panel4.Controls.Add(Me.btnSaveNewEvent)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.txtEventNotes)
        Me.Panel4.Controls.Add(Me.mtbEventCapacity)
        Me.Panel4.Controls.Add(Me.cboEventStatus)
        Me.Panel4.Controls.Add(Me.txtEventTime)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.cboEventWebContact)
        Me.Panel4.Controls.Add(Me.btnMapEventLocation)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.mtbEventZipCode)
        Me.Panel4.Controls.Add(Me.mtbEventWebPhoneNumber)
        Me.Panel4.Controls.Add(Me.mtbEventState)
        Me.Panel4.Controls.Add(Me.txtEventAddress)
        Me.Panel4.Controls.Add(Me.txtEventCity)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(778, 380)
        Me.Panel4.TabIndex = 257
        '
        'chbGECOlogInRequired
        '
        Me.chbGECOlogInRequired.AutoSize = True
        Me.chbGECOlogInRequired.Location = New System.Drawing.Point(270, 153)
        Me.chbGECOlogInRequired.Name = "chbGECOlogInRequired"
        Me.chbGECOlogInRequired.Size = New System.Drawing.Size(134, 17)
        Me.chbGECOlogInRequired.TabIndex = 8
        Me.chbGECOlogInRequired.Text = "GECO Log in Required"
        Me.chbGECOlogInRequired.UseVisualStyleBackColor = True
        '
        'txtWebsiteURL
        '
        Me.txtWebsiteURL.Location = New System.Drawing.Point(465, 210)
        Me.txtWebsiteURL.Name = "txtWebsiteURL"
        Me.txtWebsiteURL.Size = New System.Drawing.Size(281, 20)
        Me.txtWebsiteURL.TabIndex = 21
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(213, 131)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(55, 13)
        Me.Label48.TabIndex = 452
        Me.Label48.Text = "End Time:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(462, 233)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(163, 26)
        Me.Label11.TabIndex = 449
        Me.Label11.Text = "Enter complete website address. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Must begin with ""http"" or ""https""." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(414, 214)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(49, 13)
        Me.Label47.TabIndex = 449
        Me.Label47.Text = "Website:"
        '
        'txtEventTitle
        '
        Me.txtEventTitle.Location = New System.Drawing.Point(72, 16)
        Me.txtEventTitle.Name = "txtEventTitle"
        Me.txtEventTitle.Size = New System.Drawing.Size(305, 20)
        Me.txtEventTitle.TabIndex = 0
        '
        'btnClearEventManagement
        '
        Me.btnClearEventManagement.AutoSize = True
        Me.btnClearEventManagement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearEventManagement.Location = New System.Drawing.Point(336, 252)
        Me.btnClearEventManagement.Name = "btnClearEventManagement"
        Me.btnClearEventManagement.Size = New System.Drawing.Size(67, 23)
        Me.btnClearEventManagement.TabIndex = 24
        Me.btnClearEventManagement.Text = "Clear Form"
        Me.btnClearEventManagement.UseVisualStyleBackColor = True
        '
        'txtEventEndTime
        '
        Me.txtEventEndTime.Location = New System.Drawing.Point(270, 127)
        Me.txtEventEndTime.Name = "txtEventEndTime"
        Me.txtEventEndTime.Size = New System.Drawing.Size(107, 20)
        Me.txtEventEndTime.TabIndex = 5
        '
        'mtbEventPhoneNumber
        '
        Me.mtbEventPhoneNumber.Location = New System.Drawing.Point(320, 207)
        Me.mtbEventPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbEventPhoneNumber.Name = "mtbEventPhoneNumber"
        Me.mtbEventPhoneNumber.ReadOnly = True
        Me.mtbEventPhoneNumber.Size = New System.Drawing.Size(86, 20)
        Me.mtbEventPhoneNumber.TabIndex = 444
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Event Title:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(264, 211)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 13)
        Me.Label17.TabIndex = 443
        Me.Label17.Text = "Phone No:"
        '
        'txtEventDescription
        '
        Me.txtEventDescription.Location = New System.Drawing.Point(72, 45)
        Me.txtEventDescription.Multiline = True
        Me.txtEventDescription.Name = "txtEventDescription"
        Me.txtEventDescription.Size = New System.Drawing.Size(305, 51)
        Me.txtEventDescription.TabIndex = 1
        '
        'cboEventContact
        '
        Me.cboEventContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEventContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEventContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventContact.FormattingEnabled = True
        Me.cboEventContact.Location = New System.Drawing.Point(113, 207)
        Me.cboEventContact.Name = "cboEventContact"
        Me.cboEventContact.Size = New System.Drawing.Size(142, 21)
        Me.cboEventContact.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Description:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(-1, 211)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(111, 13)
        Me.Label18.TabIndex = 441
        Me.Label18.Text = "APB/Internal Contact:"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(178, 106)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(101, 13)
        Me.Label32.TabIndex = 447
        Me.Label32.Text = "End Date (2+ days):"
        '
        'btnGeneratePasscode
        '
        Me.btnGeneratePasscode.AutoSize = True
        Me.btnGeneratePasscode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnGeneratePasscode.Location = New System.Drawing.Point(154, 150)
        Me.btnGeneratePasscode.Name = "btnGeneratePasscode"
        Me.btnGeneratePasscode.Size = New System.Drawing.Size(89, 23)
        Me.btnGeneratePasscode.TabIndex = 7
        Me.btnGeneratePasscode.Text = "Generate Code"
        Me.btnGeneratePasscode.UseVisualStyleBackColor = True
        '
        'txtEventVenue
        '
        Me.txtEventVenue.Location = New System.Drawing.Point(465, 42)
        Me.txtEventVenue.Name = "txtEventVenue"
        Me.txtEventVenue.Size = New System.Drawing.Size(121, 20)
        Me.txtEventVenue.TabIndex = 13
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(11, 155)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(57, 13)
        Me.Label16.TabIndex = 439
        Me.Label16.Text = "Passcode:"
        '
        'DTPEventEndDate
        '
        Me.DTPEventEndDate.Checked = False
        Me.DTPEventEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEventEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEventEndDate.Location = New System.Drawing.Point(281, 102)
        Me.DTPEventEndDate.Name = "DTPEventEndDate"
        Me.DTPEventEndDate.ShowCheckBox = True
        Me.DTPEventEndDate.Size = New System.Drawing.Size(120, 20)
        Me.DTPEventEndDate.TabIndex = 3
        '
        'chbEventPasscode
        '
        Me.chbEventPasscode.AutoSize = True
        Me.chbEventPasscode.Location = New System.Drawing.Point(72, 153)
        Me.chbEventPasscode.Name = "chbEventPasscode"
        Me.chbEventPasscode.Size = New System.Drawing.Size(77, 17)
        Me.chbEventPasscode.TabIndex = 6
        Me.chbEventPasscode.Text = "GA123456"
        Me.chbEventPasscode.UseVisualStyleBackColor = True
        '
        'btnDeleteEvent
        '
        Me.btnDeleteEvent.AutoSize = True
        Me.btnDeleteEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteEvent.Location = New System.Drawing.Point(667, 252)
        Me.btnDeleteEvent.Name = "btnDeleteEvent"
        Me.btnDeleteEvent.Size = New System.Drawing.Size(79, 23)
        Me.btnDeleteEvent.TabIndex = 25
        Me.btnDeleteEvent.Text = "Delete Event"
        Me.btnDeleteEvent.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(37, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Date:"
        '
        'btnUpdateEvent
        '
        Me.btnUpdateEvent.AutoSize = True
        Me.btnUpdateEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateEvent.Location = New System.Drawing.Point(200, 252)
        Me.btnUpdateEvent.Name = "btnUpdateEvent"
        Me.btnUpdateEvent.Size = New System.Drawing.Size(122, 23)
        Me.btnUpdateEvent.TabIndex = 23
        Me.btnUpdateEvent.Text = "Update Existing Event"
        Me.btnUpdateEvent.UseVisualStyleBackColor = True
        '
        'DTPEventDate
        '
        Me.DTPEventDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEventDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEventDate.Location = New System.Drawing.Point(72, 102)
        Me.DTPEventDate.Name = "DTPEventDate"
        Me.DTPEventDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPEventDate.TabIndex = 2
        '
        'btnSaveNewEvent
        '
        Me.btnSaveNewEvent.AutoSize = True
        Me.btnSaveNewEvent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveNewEvent.Location = New System.Drawing.Point(82, 252)
        Me.btnSaveNewEvent.Name = "btnSaveNewEvent"
        Me.btnSaveNewEvent.Size = New System.Drawing.Size(104, 23)
        Me.btnSaveNewEvent.TabIndex = 22
        Me.btnSaveNewEvent.Text = "Create New Event"
        Me.btnSaveNewEvent.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(422, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 415
        Me.Label7.Text = "Venue:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(425, 156)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 420
        Me.Label9.Text = "Notes:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(412, 128)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 417
        Me.Label8.Text = "Capacity:"
        '
        'txtEventNotes
        '
        Me.txtEventNotes.Location = New System.Drawing.Point(465, 153)
        Me.txtEventNotes.Multiline = True
        Me.txtEventNotes.Name = "txtEventNotes"
        Me.txtEventNotes.Size = New System.Drawing.Size(281, 51)
        Me.txtEventNotes.TabIndex = 20
        '
        'mtbEventCapacity
        '
        Me.mtbEventCapacity.Location = New System.Drawing.Point(465, 124)
        Me.mtbEventCapacity.Mask = "00000"
        Me.mtbEventCapacity.Name = "mtbEventCapacity"
        Me.mtbEventCapacity.Size = New System.Drawing.Size(42, 20)
        Me.mtbEventCapacity.TabIndex = 19
        Me.mtbEventCapacity.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbEventCapacity.ValidatingType = GetType(Integer)
        '
        'cboEventStatus
        '
        Me.cboEventStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEventStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEventStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventStatus.FormattingEnabled = True
        Me.cboEventStatus.Location = New System.Drawing.Point(89, 179)
        Me.cboEventStatus.Name = "cboEventStatus"
        Me.cboEventStatus.Size = New System.Drawing.Size(154, 21)
        Me.cboEventStatus.TabIndex = 9
        '
        'txtEventTime
        '
        Me.txtEventTime.Location = New System.Drawing.Point(72, 127)
        Me.txtEventTime.Name = "txtEventTime"
        Me.txtEventTime.Size = New System.Drawing.Size(121, 20)
        Me.txtEventTime.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 182)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Event Status:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(37, 131)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 426
        Me.Label12.Text = "Time:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 238)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Event ID:"
        Me.Label1.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(390, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 427
        Me.Label13.Text = "Web Contact:"
        '
        'cboEventWebContact
        '
        Me.cboEventWebContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEventWebContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEventWebContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventWebContact.FormattingEnabled = True
        Me.cboEventWebContact.Location = New System.Drawing.Point(465, 19)
        Me.cboEventWebContact.Name = "cboEventWebContact"
        Me.cboEventWebContact.Size = New System.Drawing.Size(142, 21)
        Me.cboEventWebContact.TabIndex = 11
        '
        'btnMapEventLocation
        '
        Me.btnMapEventLocation.AutoSize = True
        Me.btnMapEventLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMapEventLocation.Location = New System.Drawing.Point(699, 86)
        Me.btnMapEventLocation.Name = "btnMapEventLocation"
        Me.btnMapEventLocation.Size = New System.Drawing.Size(47, 23)
        Me.btnMapEventLocation.TabIndex = 18
        Me.btnMapEventLocation.Text = "Map It"
        Me.btnMapEventLocation.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(619, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(58, 13)
        Me.Label14.TabIndex = 430
        Me.Label14.Text = "Phone No:"
        '
        'mtbEventZipCode
        '
        Me.mtbEventZipCode.Location = New System.Drawing.Point(621, 87)
        Me.mtbEventZipCode.Mask = "00000-9999"
        Me.mtbEventZipCode.Name = "mtbEventZipCode"
        Me.mtbEventZipCode.Size = New System.Drawing.Size(68, 20)
        Me.mtbEventZipCode.TabIndex = 17
        Me.mtbEventZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbEventWebPhoneNumber
        '
        Me.mtbEventWebPhoneNumber.Location = New System.Drawing.Point(675, 19)
        Me.mtbEventWebPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbEventWebPhoneNumber.Name = "mtbEventWebPhoneNumber"
        Me.mtbEventWebPhoneNumber.Size = New System.Drawing.Size(86, 20)
        Me.mtbEventWebPhoneNumber.TabIndex = 12
        Me.mtbEventWebPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbEventState
        '
        Me.mtbEventState.Location = New System.Drawing.Point(583, 87)
        Me.mtbEventState.Mask = "aa"
        Me.mtbEventState.Name = "mtbEventState"
        Me.mtbEventState.Size = New System.Drawing.Size(32, 20)
        Me.mtbEventState.TabIndex = 16
        Me.mtbEventState.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtEventAddress
        '
        Me.txtEventAddress.Location = New System.Drawing.Point(465, 65)
        Me.txtEventAddress.Name = "txtEventAddress"
        Me.txtEventAddress.Size = New System.Drawing.Size(224, 20)
        Me.txtEventAddress.TabIndex = 14
        '
        'txtEventCity
        '
        Me.txtEventCity.Location = New System.Drawing.Point(465, 87)
        Me.txtEventCity.Name = "txtEventCity"
        Me.txtEventCity.Size = New System.Drawing.Size(112, 20)
        Me.txtEventCity.TabIndex = 15
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(415, 68)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(48, 13)
        Me.Label15.TabIndex = 433
        Me.Label15.Text = "Address:"
        '
        'tabsEventDetails
        '
        Me.tabsEventDetails.Controls.Add(Me.tabEventOverview)
        Me.tabsEventDetails.Controls.Add(Me.tabEventManagement)
        Me.tabsEventDetails.Controls.Add(Me.tabRegistrationManagement)
        Me.tabsEventDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tabsEventDetails.Location = New System.Drawing.Point(0, 160)
        Me.tabsEventDetails.Name = "tabsEventDetails"
        Me.tabsEventDetails.SelectedIndex = 0
        Me.tabsEventDetails.Size = New System.Drawing.Size(792, 412)
        Me.tabsEventDetails.TabIndex = 259
        '
        'tabEventOverview
        '
        Me.tabEventOverview.Controls.Add(Me.dgvOverviewRegistrants)
        Me.tabEventOverview.Controls.Add(Me.Panel9)
        Me.tabEventOverview.Location = New System.Drawing.Point(4, 22)
        Me.tabEventOverview.Name = "tabEventOverview"
        Me.tabEventOverview.Size = New System.Drawing.Size(784, 386)
        Me.tabEventOverview.TabIndex = 3
        Me.tabEventOverview.Text = "Event Overview"
        Me.tabEventOverview.UseVisualStyleBackColor = True
        '
        'dgvOverviewRegistrants
        '
        Me.dgvOverviewRegistrants.AllowUserToAddRows = False
        Me.dgvOverviewRegistrants.AllowUserToDeleteRows = False
        Me.dgvOverviewRegistrants.AllowUserToResizeRows = False
        Me.dgvOverviewRegistrants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOverviewRegistrants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvOverviewRegistrants.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvOverviewRegistrants.Location = New System.Drawing.Point(0, 168)
        Me.dgvOverviewRegistrants.MultiSelect = False
        Me.dgvOverviewRegistrants.Name = "dgvOverviewRegistrants"
        Me.dgvOverviewRegistrants.ReadOnly = True
        Me.dgvOverviewRegistrants.RowHeadersVisible = False
        Me.dgvOverviewRegistrants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOverviewRegistrants.ShowEditingIcon = False
        Me.dgvOverviewRegistrants.Size = New System.Drawing.Size(784, 218)
        Me.dgvOverviewRegistrants.TabIndex = 421
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.lblEmail)
        Me.Panel9.Controls.Add(Me.chbOvLoginRequired)
        Me.Panel9.Controls.Add(Me.txtOvNotes)
        Me.Panel9.Controls.Add(Me.txtOvVenue)
        Me.Panel9.Controls.Add(Me.txtOvAPBContact)
        Me.Panel9.Controls.Add(Me.txtOvWebContact)
        Me.Panel9.Controls.Add(Me.txtOvCancelled)
        Me.Panel9.Controls.Add(Me.txtOvWaitingList)
        Me.Panel9.Controls.Add(Me.txtOvEventCapacity)
        Me.Panel9.Controls.Add(Me.txtOvNumberRegistered)
        Me.Panel9.Controls.Add(Me.txtOvEventStatus)
        Me.Panel9.Controls.Add(Me.txtOvPassCode)
        Me.Panel9.Controls.Add(Me.txtOvEventDateTime)
        Me.Panel9.Controls.Add(Me.txtOvEvent)
        Me.Panel9.Controls.Add(Me.txtOvDescription)
        Me.Panel9.Controls.Add(Me.txtEmails)
        Me.Panel9.Controls.Add(Me.Label34)
        Me.Panel9.Controls.Add(Me.Label35)
        Me.Panel9.Controls.Add(Me.btnEmailWaitList)
        Me.Panel9.Controls.Add(Me.lblCancelled)
        Me.Panel9.Controls.Add(Me.Label36)
        Me.Panel9.Controls.Add(Me.btnEmailRegistrants)
        Me.Panel9.Controls.Add(Me.Label37)
        Me.Panel9.Controls.Add(Me.btnEmailAll)
        Me.Panel9.Controls.Add(Me.Label38)
        Me.Panel9.Controls.Add(Me.btnExportRegistrantsToExcel)
        Me.Panel9.Controls.Add(Me.Label39)
        Me.Panel9.Controls.Add(Me.Label40)
        Me.Panel9.Controls.Add(Me.Label42)
        Me.Panel9.Controls.Add(Me.Label43)
        Me.Panel9.Controls.Add(Me.Label44)
        Me.Panel9.Controls.Add(Me.Label45)
        Me.Panel9.Controls.Add(Me.Label46)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(784, 168)
        Me.Panel9.TabIndex = 422
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(559, 143)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(35, 13)
        Me.lblEmail.TabIndex = 438
        Me.lblEmail.Text = "Email:"
        '
        'chbOvLoginRequired
        '
        Me.chbOvLoginRequired.AutoCheck = False
        Me.chbOvLoginRequired.AutoSize = True
        Me.chbOvLoginRequired.Location = New System.Drawing.Point(11, 82)
        Me.chbOvLoginRequired.Name = "chbOvLoginRequired"
        Me.chbOvLoginRequired.Size = New System.Drawing.Size(98, 17)
        Me.chbOvLoginRequired.TabIndex = 437
        Me.chbOvLoginRequired.Text = "Login Required"
        Me.chbOvLoginRequired.UseVisualStyleBackColor = True
        '
        'txtOvNotes
        '
        Me.txtOvNotes.Location = New System.Drawing.Point(464, 90)
        Me.txtOvNotes.Multiline = True
        Me.txtOvNotes.Name = "txtOvNotes"
        Me.txtOvNotes.ReadOnly = True
        Me.txtOvNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOvNotes.Size = New System.Drawing.Size(303, 41)
        Me.txtOvNotes.TabIndex = 436
        '
        'txtOvVenue
        '
        Me.txtOvVenue.Location = New System.Drawing.Point(464, 43)
        Me.txtOvVenue.Multiline = True
        Me.txtOvVenue.Name = "txtOvVenue"
        Me.txtOvVenue.ReadOnly = True
        Me.txtOvVenue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOvVenue.Size = New System.Drawing.Size(303, 63)
        Me.txtOvVenue.TabIndex = 435
        '
        'txtOvAPBContact
        '
        Me.txtOvAPBContact.Location = New System.Drawing.Point(508, 23)
        Me.txtOvAPBContact.Multiline = True
        Me.txtOvAPBContact.Name = "txtOvAPBContact"
        Me.txtOvAPBContact.ReadOnly = True
        Me.txtOvAPBContact.Size = New System.Drawing.Size(259, 20)
        Me.txtOvAPBContact.TabIndex = 434
        '
        'txtOvWebContact
        '
        Me.txtOvWebContact.Location = New System.Drawing.Point(508, 3)
        Me.txtOvWebContact.Multiline = True
        Me.txtOvWebContact.Name = "txtOvWebContact"
        Me.txtOvWebContact.ReadOnly = True
        Me.txtOvWebContact.Size = New System.Drawing.Size(259, 20)
        Me.txtOvWebContact.TabIndex = 433
        '
        'txtOvCancelled
        '
        Me.txtOvCancelled.Location = New System.Drawing.Point(356, 140)
        Me.txtOvCancelled.Multiline = True
        Me.txtOvCancelled.Name = "txtOvCancelled"
        Me.txtOvCancelled.ReadOnly = True
        Me.txtOvCancelled.Size = New System.Drawing.Size(37, 20)
        Me.txtOvCancelled.TabIndex = 432
        '
        'txtOvWaitingList
        '
        Me.txtOvWaitingList.Location = New System.Drawing.Point(250, 140)
        Me.txtOvWaitingList.Multiline = True
        Me.txtOvWaitingList.Name = "txtOvWaitingList"
        Me.txtOvWaitingList.ReadOnly = True
        Me.txtOvWaitingList.Size = New System.Drawing.Size(37, 20)
        Me.txtOvWaitingList.TabIndex = 432
        '
        'txtOvEventCapacity
        '
        Me.txtOvEventCapacity.Location = New System.Drawing.Point(130, 140)
        Me.txtOvEventCapacity.Multiline = True
        Me.txtOvEventCapacity.Name = "txtOvEventCapacity"
        Me.txtOvEventCapacity.ReadOnly = True
        Me.txtOvEventCapacity.Size = New System.Drawing.Size(43, 20)
        Me.txtOvEventCapacity.TabIndex = 431
        '
        'txtOvNumberRegistered
        '
        Me.txtOvNumberRegistered.Location = New System.Drawing.Point(75, 140)
        Me.txtOvNumberRegistered.Multiline = True
        Me.txtOvNumberRegistered.Name = "txtOvNumberRegistered"
        Me.txtOvNumberRegistered.ReadOnly = True
        Me.txtOvNumberRegistered.Size = New System.Drawing.Size(43, 20)
        Me.txtOvNumberRegistered.TabIndex = 430
        '
        'txtOvEventStatus
        '
        Me.txtOvEventStatus.Location = New System.Drawing.Point(85, 106)
        Me.txtOvEventStatus.Multiline = True
        Me.txtOvEventStatus.Name = "txtOvEventStatus"
        Me.txtOvEventStatus.ReadOnly = True
        Me.txtOvEventStatus.Size = New System.Drawing.Size(140, 20)
        Me.txtOvEventStatus.TabIndex = 429
        '
        'txtOvPassCode
        '
        Me.txtOvPassCode.Location = New System.Drawing.Point(193, 80)
        Me.txtOvPassCode.Multiline = True
        Me.txtOvPassCode.Name = "txtOvPassCode"
        Me.txtOvPassCode.ReadOnly = True
        Me.txtOvPassCode.Size = New System.Drawing.Size(140, 20)
        Me.txtOvPassCode.TabIndex = 428
        '
        'txtOvEventDateTime
        '
        Me.txtOvEventDateTime.Location = New System.Drawing.Point(106, 56)
        Me.txtOvEventDateTime.Multiline = True
        Me.txtOvEventDateTime.Name = "txtOvEventDateTime"
        Me.txtOvEventDateTime.ReadOnly = True
        Me.txtOvEventDateTime.Size = New System.Drawing.Size(281, 20)
        Me.txtOvEventDateTime.TabIndex = 426
        '
        'txtOvEvent
        '
        Me.txtOvEvent.Location = New System.Drawing.Point(77, 3)
        Me.txtOvEvent.Multiline = True
        Me.txtOvEvent.Name = "txtOvEvent"
        Me.txtOvEvent.ReadOnly = True
        Me.txtOvEvent.Size = New System.Drawing.Size(310, 20)
        Me.txtOvEvent.TabIndex = 425
        '
        'txtOvDescription
        '
        Me.txtOvDescription.Location = New System.Drawing.Point(77, 23)
        Me.txtOvDescription.Multiline = True
        Me.txtOvDescription.Name = "txtOvDescription"
        Me.txtOvDescription.ReadOnly = True
        Me.txtOvDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOvDescription.Size = New System.Drawing.Size(310, 33)
        Me.txtOvDescription.TabIndex = 424
        '
        'txtEmails
        '
        Me.txtEmails.Location = New System.Drawing.Point(371, 106)
        Me.txtEmails.Multiline = True
        Me.txtEmails.Name = "txtEmails"
        Me.txtEmails.Size = New System.Drawing.Size(38, 20)
        Me.txtEmails.TabIndex = 423
        Me.txtEmails.Visible = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(8, 6)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(38, 13)
        Me.Label34.TabIndex = 4
        Me.Label34.Text = "Event:"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(417, 7)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(73, 13)
        Me.Label35.TabIndex = 5
        Me.Label35.Text = "Web Contact:"
        '
        'btnEmailWaitList
        '
        Me.btnEmailWaitList.AutoSize = True
        Me.btnEmailWaitList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailWaitList.Location = New System.Drawing.Point(675, 138)
        Me.btnEmailWaitList.Name = "btnEmailWaitList"
        Me.btnEmailWaitList.Size = New System.Drawing.Size(58, 23)
        Me.btnEmailWaitList.TabIndex = 420
        Me.btnEmailWaitList.Text = "Wait List"
        Me.btnEmailWaitList.UseVisualStyleBackColor = True
        '
        'lblCancelled
        '
        Me.lblCancelled.AutoSize = True
        Me.lblCancelled.Location = New System.Drawing.Point(293, 143)
        Me.lblCancelled.Name = "lblCancelled"
        Me.lblCancelled.Size = New System.Drawing.Size(57, 13)
        Me.lblCancelled.TabIndex = 6
        Me.lblCancelled.Text = "Cancelled:"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(179, 143)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(65, 13)
        Me.Label36.TabIndex = 6
        Me.Label36.Text = "Waiting List:"
        '
        'btnEmailRegistrants
        '
        Me.btnEmailRegistrants.AutoSize = True
        Me.btnEmailRegistrants.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailRegistrants.Location = New System.Drawing.Point(600, 138)
        Me.btnEmailRegistrants.Name = "btnEmailRegistrants"
        Me.btnEmailRegistrants.Size = New System.Drawing.Size(70, 23)
        Me.btnEmailRegistrants.TabIndex = 419
        Me.btnEmailRegistrants.Text = "Registrants"
        Me.btnEmailRegistrants.UseVisualStyleBackColor = True
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(8, 109)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(71, 13)
        Me.Label37.TabIndex = 7
        Me.Label37.Text = "Event Status:"
        '
        'btnEmailAll
        '
        Me.btnEmailAll.AutoSize = True
        Me.btnEmailAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailAll.Location = New System.Drawing.Point(739, 138)
        Me.btnEmailAll.Name = "btnEmailAll"
        Me.btnEmailAll.Size = New System.Drawing.Size(28, 23)
        Me.btnEmailAll.TabIndex = 418
        Me.btnEmailAll.Text = "All"
        Me.btnEmailAll.UseVisualStyleBackColor = True
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(116, 143)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(16, 13)
        Me.Label38.TabIndex = 8
        Me.Label38.Text = "of"
        '
        'btnExportRegistrantsToExcel
        '
        Me.btnExportRegistrantsToExcel.AutoSize = True
        Me.btnExportRegistrantsToExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportRegistrantsToExcel.Image = Global.Iaip.My.Resources.Resources.SpreadsheetIcon
        Me.btnExportRegistrantsToExcel.Location = New System.Drawing.Point(423, 138)
        Me.btnExportRegistrantsToExcel.Name = "btnExportRegistrantsToExcel"
        Me.btnExportRegistrantsToExcel.Size = New System.Drawing.Size(104, 23)
        Me.btnExportRegistrantsToExcel.TabIndex = 417
        Me.btnExportRegistrantsToExcel.Text = "Export to Excel"
        Me.btnExportRegistrantsToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExportRegistrantsToExcel.UseVisualStyleBackColor = True
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(129, 83)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(58, 13)
        Me.Label39.TabIndex = 9
        Me.Label39.Text = "PassCode:"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(8, 143)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(61, 13)
        Me.Label40.TabIndex = 10
        Me.Label40.Text = "Registered:"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(8, 60)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(92, 13)
        Me.Label42.TabIndex = 12
        Me.Label42.Text = "Event Date/Time:"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(8, 26)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(63, 13)
        Me.Label43.TabIndex = 13
        Me.Label43.Text = "Description:"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(420, 93)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(38, 13)
        Me.Label44.TabIndex = 15
        Me.Label44.Text = "Notes:"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(417, 47)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(41, 13)
        Me.Label45.TabIndex = 16
        Me.Label45.Text = "Venue:"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(417, 27)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(85, 13)
        Me.Label46.TabIndex = 17
        Me.Label46.Text = "Internal Contact:"
        '
        'tabEventManagement
        '
        Me.tabEventManagement.Controls.Add(Me.Panel4)
        Me.tabEventManagement.Location = New System.Drawing.Point(4, 22)
        Me.tabEventManagement.Name = "tabEventManagement"
        Me.tabEventManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEventManagement.Size = New System.Drawing.Size(784, 386)
        Me.tabEventManagement.TabIndex = 0
        Me.tabEventManagement.Text = "Event Management"
        Me.tabEventManagement.UseVisualStyleBackColor = True
        '
        'tabRegistrationManagement
        '
        Me.tabRegistrationManagement.Controls.Add(Me.dgvRegistrationManagement)
        Me.tabRegistrationManagement.Controls.Add(Me.Panel8)
        Me.tabRegistrationManagement.Location = New System.Drawing.Point(4, 22)
        Me.tabRegistrationManagement.Name = "tabRegistrationManagement"
        Me.tabRegistrationManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRegistrationManagement.Size = New System.Drawing.Size(784, 386)
        Me.tabRegistrationManagement.TabIndex = 1
        Me.tabRegistrationManagement.Text = "Registration Management"
        Me.tabRegistrationManagement.UseVisualStyleBackColor = True
        '
        'dgvRegistrationManagement
        '
        Me.dgvRegistrationManagement.AllowUserToAddRows = False
        Me.dgvRegistrationManagement.AllowUserToDeleteRows = False
        Me.dgvRegistrationManagement.AllowUserToResizeRows = False
        Me.dgvRegistrationManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRegistrationManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRegistrationManagement.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvRegistrationManagement.Location = New System.Drawing.Point(3, 3)
        Me.dgvRegistrationManagement.MultiSelect = False
        Me.dgvRegistrationManagement.Name = "dgvRegistrationManagement"
        Me.dgvRegistrationManagement.ReadOnly = True
        Me.dgvRegistrationManagement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRegistrationManagement.ShowEditingIcon = False
        Me.dgvRegistrationManagement.Size = New System.Drawing.Size(404, 380)
        Me.dgvRegistrationManagement.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.btnModifyRegistration)
        Me.Panel8.Controls.Add(Me.Label10)
        Me.Panel8.Controls.Add(Me.txtRegID)
        Me.Panel8.Controls.Add(Me.Label33)
        Me.Panel8.Controls.Add(Me.txtRegEmail)
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Controls.Add(Me.txtRegComments)
        Me.Panel8.Controls.Add(Me.DTPRegDateRegistered)
        Me.Panel8.Controls.Add(Me.Label31)
        Me.Panel8.Controls.Add(Me.txtGECOUserID)
        Me.Panel8.Controls.Add(Me.cboRegUserType)
        Me.Panel8.Controls.Add(Me.Label30)
        Me.Panel8.Controls.Add(Me.mtbRegPhoneExt)
        Me.Panel8.Controls.Add(Me.mtbRegPhoneNo)
        Me.Panel8.Controls.Add(Me.Label28)
        Me.Panel8.Controls.Add(Me.mtbRegZipCode)
        Me.Panel8.Controls.Add(Me.mtbRegState)
        Me.Panel8.Controls.Add(Me.txtRegCity)
        Me.Panel8.Controls.Add(Me.Label27)
        Me.Panel8.Controls.Add(Me.txtRegAddress)
        Me.Panel8.Controls.Add(Me.Label26)
        Me.Panel8.Controls.Add(Me.txtRegTitle)
        Me.Panel8.Controls.Add(Me.Label25)
        Me.Panel8.Controls.Add(Me.txtRegLastName)
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.txtRegFirstName)
        Me.Panel8.Controls.Add(Me.Label23)
        Me.Panel8.Controls.Add(Me.txtRegSalutation)
        Me.Panel8.Controls.Add(Me.cboRegStatus)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.txtRegConfirmationNum)
        Me.Panel8.Controls.Add(Me.Label19)
        Me.Panel8.Controls.Add(Me.Label6)
        Me.Panel8.Controls.Add(Me.txtRegEventTitle)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel8.Location = New System.Drawing.Point(407, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(374, 380)
        Me.Panel8.TabIndex = 1
        '
        'btnModifyRegistration
        '
        Me.btnModifyRegistration.AutoSize = True
        Me.btnModifyRegistration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnModifyRegistration.Location = New System.Drawing.Point(275, 70)
        Me.btnModifyRegistration.Name = "btnModifyRegistration"
        Me.btnModifyRegistration.Size = New System.Drawing.Size(85, 23)
        Me.btnModifyRegistration.TabIndex = 467
        Me.btnModifyRegistration.Text = "Update Status"
        Me.btnModifyRegistration.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(283, 31)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 466
        Me.Label10.Text = "Reg. ID"
        Me.Label10.Visible = False
        '
        'txtRegID
        '
        Me.txtRegID.Location = New System.Drawing.Point(333, 28)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(36, 20)
        Me.txtRegID.TabIndex = 465
        Me.txtRegID.Visible = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(16, 216)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(35, 13)
        Me.Label33.TabIndex = 464
        Me.Label33.Text = "Email:"
        '
        'txtRegEmail
        '
        Me.txtRegEmail.Location = New System.Drawing.Point(53, 212)
        Me.txtRegEmail.Name = "txtRegEmail"
        Me.txtRegEmail.ReadOnly = True
        Me.txtRegEmail.Size = New System.Drawing.Size(224, 20)
        Me.txtRegEmail.TabIndex = 463
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(4, 282)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(59, 13)
        Me.Label21.TabIndex = 462
        Me.Label21.Text = "Comments:"
        '
        'txtRegComments
        '
        Me.txtRegComments.AcceptsReturn = True
        Me.txtRegComments.Location = New System.Drawing.Point(65, 282)
        Me.txtRegComments.Multiline = True
        Me.txtRegComments.Name = "txtRegComments"
        Me.txtRegComments.ReadOnly = True
        Me.txtRegComments.Size = New System.Drawing.Size(281, 41)
        Me.txtRegComments.TabIndex = 461
        '
        'DTPRegDateRegistered
        '
        Me.DTPRegDateRegistered.CustomFormat = "dd-MMM-yy hh:mm:ss tt"
        Me.DTPRegDateRegistered.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPRegDateRegistered.Location = New System.Drawing.Point(102, 24)
        Me.DTPRegDateRegistered.Name = "DTPRegDateRegistered"
        Me.DTPRegDateRegistered.Size = New System.Drawing.Size(163, 20)
        Me.DTPRegDateRegistered.TabIndex = 460
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(261, 54)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(68, 13)
        Me.Label31.TabIndex = 459
        Me.Label31.Text = "GECOuserID"
        Me.Label31.Visible = False
        '
        'txtGECOUserID
        '
        Me.txtGECOUserID.Location = New System.Drawing.Point(333, 50)
        Me.txtGECOUserID.Name = "txtGECOUserID"
        Me.txtGECOUserID.ReadOnly = True
        Me.txtGECOUserID.Size = New System.Drawing.Size(36, 20)
        Me.txtGECOUserID.TabIndex = 458
        Me.txtGECOUserID.Visible = False
        '
        'cboRegUserType
        '
        Me.cboRegUserType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegUserType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegUserType.Enabled = False
        Me.cboRegUserType.FormattingEnabled = True
        Me.cboRegUserType.Location = New System.Drawing.Point(65, 258)
        Me.cboRegUserType.Name = "cboRegUserType"
        Me.cboRegUserType.Size = New System.Drawing.Size(154, 21)
        Me.cboRegUserType.TabIndex = 457
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(4, 262)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(59, 13)
        Me.Label30.TabIndex = 456
        Me.Label30.Text = "User Type:"
        '
        'mtbRegPhoneExt
        '
        Me.mtbRegPhoneExt.Location = New System.Drawing.Point(158, 235)
        Me.mtbRegPhoneExt.Mask = "00000"
        Me.mtbRegPhoneExt.Name = "mtbRegPhoneExt"
        Me.mtbRegPhoneExt.ReadOnly = True
        Me.mtbRegPhoneExt.Size = New System.Drawing.Size(32, 20)
        Me.mtbRegPhoneExt.TabIndex = 452
        Me.mtbRegPhoneExt.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbRegPhoneExt.ValidatingType = GetType(Integer)
        '
        'mtbRegPhoneNo
        '
        Me.mtbRegPhoneNo.Location = New System.Drawing.Point(66, 235)
        Me.mtbRegPhoneNo.Mask = "(999) 000-0000"
        Me.mtbRegPhoneNo.Name = "mtbRegPhoneNo"
        Me.mtbRegPhoneNo.ReadOnly = True
        Me.mtbRegPhoneNo.Size = New System.Drawing.Size(86, 20)
        Me.mtbRegPhoneNo.TabIndex = 450
        Me.mtbRegPhoneNo.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(8, 239)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(58, 13)
        Me.Label28.TabIndex = 451
        Me.Label28.Text = "Phone No:"
        '
        'mtbRegZipCode
        '
        Me.mtbRegZipCode.Location = New System.Drawing.Point(209, 189)
        Me.mtbRegZipCode.Mask = "00000-9999"
        Me.mtbRegZipCode.Name = "mtbRegZipCode"
        Me.mtbRegZipCode.ReadOnly = True
        Me.mtbRegZipCode.Size = New System.Drawing.Size(68, 20)
        Me.mtbRegZipCode.TabIndex = 448
        Me.mtbRegZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbRegState
        '
        Me.mtbRegState.Location = New System.Drawing.Point(171, 189)
        Me.mtbRegState.Mask = "aa"
        Me.mtbRegState.Name = "mtbRegState"
        Me.mtbRegState.ReadOnly = True
        Me.mtbRegState.Size = New System.Drawing.Size(32, 20)
        Me.mtbRegState.TabIndex = 447
        Me.mtbRegState.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtRegCity
        '
        Me.txtRegCity.Location = New System.Drawing.Point(53, 189)
        Me.txtRegCity.Name = "txtRegCity"
        Me.txtRegCity.ReadOnly = True
        Me.txtRegCity.Size = New System.Drawing.Size(112, 20)
        Me.txtRegCity.TabIndex = 446
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(3, 170)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(48, 13)
        Me.Label27.TabIndex = 449
        Me.Label27.Text = "Address:"
        '
        'txtRegAddress
        '
        Me.txtRegAddress.Location = New System.Drawing.Point(53, 167)
        Me.txtRegAddress.Name = "txtRegAddress"
        Me.txtRegAddress.ReadOnly = True
        Me.txtRegAddress.Size = New System.Drawing.Size(224, 20)
        Me.txtRegAddress.TabIndex = 445
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(4, 146)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(30, 13)
        Me.Label26.TabIndex = 444
        Me.Label26.Text = "Title:"
        '
        'txtRegTitle
        '
        Me.txtRegTitle.Location = New System.Drawing.Point(36, 143)
        Me.txtRegTitle.Name = "txtRegTitle"
        Me.txtRegTitle.ReadOnly = True
        Me.txtRegTitle.Size = New System.Drawing.Size(121, 20)
        Me.txtRegTitle.TabIndex = 443
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(188, 123)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 13)
        Me.Label25.TabIndex = 442
        Me.Label25.Text = "Last Name:"
        '
        'txtRegLastName
        '
        Me.txtRegLastName.Location = New System.Drawing.Point(249, 119)
        Me.txtRegLastName.Name = "txtRegLastName"
        Me.txtRegLastName.ReadOnly = True
        Me.txtRegLastName.Size = New System.Drawing.Size(121, 20)
        Me.txtRegLastName.TabIndex = 441
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(4, 123)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(60, 13)
        Me.Label24.TabIndex = 440
        Me.Label24.Text = "First Name:"
        '
        'txtRegFirstName
        '
        Me.txtRegFirstName.Location = New System.Drawing.Point(66, 119)
        Me.txtRegFirstName.Name = "txtRegFirstName"
        Me.txtRegFirstName.ReadOnly = True
        Me.txtRegFirstName.Size = New System.Drawing.Size(121, 20)
        Me.txtRegFirstName.TabIndex = 439
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(4, 99)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(57, 13)
        Me.Label23.TabIndex = 438
        Me.Label23.Text = "Salutation:"
        '
        'txtRegSalutation
        '
        Me.txtRegSalutation.Location = New System.Drawing.Point(64, 95)
        Me.txtRegSalutation.Name = "txtRegSalutation"
        Me.txtRegSalutation.ReadOnly = True
        Me.txtRegSalutation.Size = New System.Drawing.Size(81, 20)
        Me.txtRegSalutation.TabIndex = 437
        '
        'cboRegStatus
        '
        Me.cboRegStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRegStatus.FormattingEnabled = True
        Me.cboRegStatus.Location = New System.Drawing.Point(111, 70)
        Me.cboRegStatus.Name = "cboRegStatus"
        Me.cboRegStatus.Size = New System.Drawing.Size(154, 21)
        Me.cboRegStatus.TabIndex = 436
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(4, 74)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(99, 13)
        Me.Label22.TabIndex = 435
        Me.Label22.Text = "Registration Status:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(4, 50)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(78, 13)
        Me.Label20.TabIndex = 432
        Me.Label20.Text = "Confirmation #:"
        '
        'txtRegConfirmationNum
        '
        Me.txtRegConfirmationNum.Location = New System.Drawing.Point(89, 46)
        Me.txtRegConfirmationNum.Name = "txtRegConfirmationNum"
        Me.txtRegConfirmationNum.ReadOnly = True
        Me.txtRegConfirmationNum.Size = New System.Drawing.Size(121, 20)
        Me.txtRegConfirmationNum.TabIndex = 431
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(4, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(87, 13)
        Me.Label19.TabIndex = 430
        Me.Label19.Text = "Date Registered:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 428
        Me.Label6.Text = "Event Title:"
        '
        'txtRegEventTitle
        '
        Me.txtRegEventTitle.Location = New System.Drawing.Point(73, 3)
        Me.txtRegEventTitle.Name = "txtRegEventTitle"
        Me.txtRegEventTitle.ReadOnly = True
        Me.txtRegEventTitle.Size = New System.Drawing.Size(247, 20)
        Me.txtRegEventTitle.TabIndex = 427
        '
        'pnlEventsAndFilter
        '
        Me.pnlEventsAndFilter.Controls.Add(Me.dgvEvents)
        Me.pnlEventsAndFilter.Controls.Add(Me.pnlFilterEvents)
        Me.pnlEventsAndFilter.Controls.Add(Me.pnlEventTitle)
        Me.pnlEventsAndFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEventsAndFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlEventsAndFilter.Name = "pnlEventsAndFilter"
        Me.pnlEventsAndFilter.Size = New System.Drawing.Size(792, 160)
        Me.pnlEventsAndFilter.TabIndex = 260
        '
        'dgvEvents
        '
        Me.dgvEvents.AllowUserToAddRows = False
        Me.dgvEvents.AllowUserToDeleteRows = False
        Me.dgvEvents.AllowUserToOrderColumns = True
        Me.dgvEvents.AllowUserToResizeRows = False
        Me.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEvents.Location = New System.Drawing.Point(0, 0)
        Me.dgvEvents.MultiSelect = False
        Me.dgvEvents.Name = "dgvEvents"
        Me.dgvEvents.ReadOnly = True
        Me.dgvEvents.RowHeadersVisible = False
        Me.dgvEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEvents.Size = New System.Drawing.Size(653, 125)
        Me.dgvEvents.TabIndex = 0
        '
        'pnlFilterEvents
        '
        Me.pnlFilterEvents.Controls.Add(Me.lblFilterEvents)
        Me.pnlFilterEvents.Controls.Add(Me.rdbEventsFilterAll)
        Me.pnlFilterEvents.Controls.Add(Me.rdbEventsFilterPast)
        Me.pnlFilterEvents.Controls.Add(Me.rdbEventsFilterFuture)
        Me.pnlFilterEvents.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlFilterEvents.Location = New System.Drawing.Point(653, 0)
        Me.pnlFilterEvents.Name = "pnlFilterEvents"
        Me.pnlFilterEvents.Size = New System.Drawing.Size(139, 125)
        Me.pnlFilterEvents.TabIndex = 417
        '
        'lblFilterEvents
        '
        Me.lblFilterEvents.AutoSize = True
        Me.lblFilterEvents.Location = New System.Drawing.Point(6, 9)
        Me.lblFilterEvents.Name = "lblFilterEvents"
        Me.lblFilterEvents.Size = New System.Drawing.Size(33, 13)
        Me.lblFilterEvents.TabIndex = 3
        Me.lblFilterEvents.Text = "View:"
        '
        'rdbEventsFilterAll
        '
        Me.rdbEventsFilterAll.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdbEventsFilterAll.Location = New System.Drawing.Point(6, 92)
        Me.rdbEventsFilterAll.Name = "rdbEventsFilterAll"
        Me.rdbEventsFilterAll.Size = New System.Drawing.Size(109, 26)
        Me.rdbEventsFilterAll.TabIndex = 2
        Me.rdbEventsFilterAll.Text = "&All Events"
        Me.rdbEventsFilterAll.UseVisualStyleBackColor = True
        '
        'rdbEventsFilterPast
        '
        Me.rdbEventsFilterPast.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdbEventsFilterPast.Location = New System.Drawing.Point(6, 59)
        Me.rdbEventsFilterPast.Name = "rdbEventsFilterPast"
        Me.rdbEventsFilterPast.Size = New System.Drawing.Size(109, 27)
        Me.rdbEventsFilterPast.TabIndex = 1
        Me.rdbEventsFilterPast.Text = "&Past Events"
        Me.rdbEventsFilterPast.UseVisualStyleBackColor = True
        '
        'rdbEventsFilterFuture
        '
        Me.rdbEventsFilterFuture.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdbEventsFilterFuture.Checked = True
        Me.rdbEventsFilterFuture.Location = New System.Drawing.Point(6, 25)
        Me.rdbEventsFilterFuture.Name = "rdbEventsFilterFuture"
        Me.rdbEventsFilterFuture.Size = New System.Drawing.Size(109, 28)
        Me.rdbEventsFilterFuture.TabIndex = 0
        Me.rdbEventsFilterFuture.TabStop = True
        Me.rdbEventsFilterFuture.Text = "&Upcoming Events"
        Me.rdbEventsFilterFuture.UseVisualStyleBackColor = True
        '
        'pnlEventTitle
        '
        Me.pnlEventTitle.Controls.Add(Me.btnViewDetails)
        Me.pnlEventTitle.Controls.Add(Me.lblEventTitle)
        Me.pnlEventTitle.Controls.Add(Me.lblEventDate)
        Me.pnlEventTitle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlEventTitle.Location = New System.Drawing.Point(0, 125)
        Me.pnlEventTitle.Name = "pnlEventTitle"
        Me.pnlEventTitle.Size = New System.Drawing.Size(792, 35)
        Me.pnlEventTitle.TabIndex = 260
        '
        'btnViewDetails
        '
        Me.btnViewDetails.Location = New System.Drawing.Point(519, 6)
        Me.btnViewDetails.Name = "btnViewDetails"
        Me.btnViewDetails.Size = New System.Drawing.Size(119, 23)
        Me.btnViewDetails.TabIndex = 4
        Me.btnViewDetails.Text = "View Event Details"
        Me.btnViewDetails.UseVisualStyleBackColor = True
        '
        'lblEventTitle
        '
        Me.lblEventTitle.AutoSize = True
        Me.lblEventTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEventTitle.Location = New System.Drawing.Point(101, 11)
        Me.lblEventTitle.Name = "lblEventTitle"
        Me.lblEventTitle.Size = New System.Drawing.Size(69, 13)
        Me.lblEventTitle.TabIndex = 3
        Me.lblEventTitle.Text = "Event Title"
        '
        'lblEventDate
        '
        Me.lblEventDate.AutoSize = True
        Me.lblEventDate.Location = New System.Drawing.Point(12, 11)
        Me.lblEventDate.Name = "lblEventDate"
        Me.lblEventDate.Size = New System.Drawing.Size(61, 13)
        Me.lblEventDate.TabIndex = 5
        Me.lblEventDate.Text = "Event Date"
        '
        'MASPRegistrationTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 572)
        Me.Controls.Add(Me.pnlEventsAndFilter)
        Me.Controls.Add(Me.tabsEventDetails)
        Me.Name = "MASPRegistrationTool"
        Me.Text = "Registration Tool"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.tabsEventDetails.ResumeLayout(False)
        Me.tabEventOverview.ResumeLayout(False)
        CType(Me.dgvOverviewRegistrants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.tabEventManagement.ResumeLayout(False)
        Me.tabRegistrationManagement.ResumeLayout(False)
        CType(Me.dgvRegistrationManagement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.pnlEventsAndFilter.ResumeLayout(False)
        CType(Me.dgvEvents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilterEvents.ResumeLayout(False)
        Me.pnlFilterEvents.PerformLayout()
        Me.pnlEventTitle.ResumeLayout(False)
        Me.pnlEventTitle.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEventTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEventDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboEventStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEventVenue As System.Windows.Forms.TextBox
    Friend WithEvents mtbEventCapacity As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DTPEventDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnDeleteEvent As System.Windows.Forms.Button
    Friend WithEvents btnUpdateEvent As System.Windows.Forms.Button
    Friend WithEvents btnSaveNewEvent As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEventNotes As System.Windows.Forms.TextBox
    Friend WithEvents tabsEventDetails As System.Windows.Forms.TabControl
    Friend WithEvents tabEventManagement As System.Windows.Forms.TabPage
    Friend WithEvents tabRegistrationManagement As System.Windows.Forms.TabPage
    Friend WithEvents pnlEventsAndFilter As System.Windows.Forms.Panel
    Friend WithEvents dgvEvents As System.Windows.Forms.DataGridView
    Friend WithEvents lblEventDate As System.Windows.Forms.Label
    Friend WithEvents lblEventTitle As System.Windows.Forms.Label
    Friend WithEvents pnlFilterEvents As System.Windows.Forms.Panel
    Friend WithEvents rdbEventsFilterAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEventsFilterPast As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEventsFilterFuture As System.Windows.Forms.RadioButton
    Friend WithEvents tabEventOverview As System.Windows.Forms.TabPage
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtEventTime As System.Windows.Forms.TextBox
    Friend WithEvents mtbEventWebPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboEventWebContact As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnMapEventLocation As System.Windows.Forms.Button
    Friend WithEvents mtbEventZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbEventState As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEventCity As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtEventAddress As System.Windows.Forms.TextBox
    Friend WithEvents mtbEventPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cboEventContact As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnGeneratePasscode As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chbEventPasscode As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearEventManagement As System.Windows.Forms.Button
    Friend WithEvents dgvRegistrationManagement As System.Windows.Forms.DataGridView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRegEventTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtRegConfirmationNum As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtRegLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtRegFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtRegSalutation As System.Windows.Forms.TextBox
    Friend WithEvents cboRegStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtRegTitle As System.Windows.Forms.TextBox
    Friend WithEvents DTPRegDateRegistered As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtGECOUserID As System.Windows.Forms.TextBox
    Friend WithEvents cboRegUserType As System.Windows.Forms.ComboBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents mtbRegPhoneExt As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbRegPhoneNo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents mtbRegZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbRegState As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtRegCity As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtRegAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtRegComments As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents DTPEventEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtRegEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents dgvOverviewRegistrants As System.Windows.Forms.DataGridView
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents btnEmailWaitList As System.Windows.Forms.Button
    Friend WithEvents btnEmailRegistrants As System.Windows.Forms.Button
    Friend WithEvents btnEmailAll As System.Windows.Forms.Button
    Friend WithEvents btnExportRegistrantsToExcel As System.Windows.Forms.Button
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents txtWebsiteURL As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtEventEndTime As System.Windows.Forms.TextBox
    Friend WithEvents chbGECOlogInRequired As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents btnModifyRegistration As System.Windows.Forms.Button
    Friend WithEvents txtEmails As System.Windows.Forms.TextBox
    Friend WithEvents txtOvDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtOvNumberRegistered As System.Windows.Forms.TextBox
    Friend WithEvents txtOvEventStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtOvPassCode As System.Windows.Forms.TextBox
    Friend WithEvents txtOvEventDateTime As System.Windows.Forms.TextBox
    Friend WithEvents txtOvEvent As System.Windows.Forms.TextBox
    Friend WithEvents txtOvWaitingList As System.Windows.Forms.TextBox
    Friend WithEvents txtOvEventCapacity As System.Windows.Forms.TextBox
    Friend WithEvents txtOvAPBContact As System.Windows.Forms.TextBox
    Friend WithEvents txtOvWebContact As System.Windows.Forms.TextBox
    Friend WithEvents txtOvNotes As System.Windows.Forms.TextBox
    Friend WithEvents txtOvVenue As System.Windows.Forms.TextBox
    Friend WithEvents btnViewDetails As System.Windows.Forms.Button
    Friend WithEvents pnlEventTitle As System.Windows.Forms.Panel
    Friend WithEvents chbOvLoginRequired As System.Windows.Forms.CheckBox
    Friend WithEvents txtOvCancelled As System.Windows.Forms.TextBox
    Friend WithEvents lblCancelled As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblFilterEvents As System.Windows.Forms.Label
    Friend WithEvents Label11 As Label
End Class
