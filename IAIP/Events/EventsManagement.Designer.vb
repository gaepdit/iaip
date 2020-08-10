<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EventsManagement
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblEventMessage = New System.Windows.Forms.Label()
        Me.txtWebsiteURL = New Iaip.CueTextBox()
        Me.btnCancelEvent = New System.Windows.Forms.Button()
        Me.lnkGecoLink = New System.Windows.Forms.LinkLabel()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.txtEventTitle = New System.Windows.Forms.TextBox()
        Me.txtWebPhone = New System.Windows.Forms.TextBox()
        Me.txtEventEndTime = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEventDescription = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.btnGeneratePasscode = New System.Windows.Forms.Button()
        Me.txtEventVenue = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.DTPEventEndDate = New System.Windows.Forms.DateTimePicker()
        Me.chkEventPasscode = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.DTPEventDate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtEventNotes = New System.Windows.Forms.TextBox()
        Me.txtEventTime = New System.Windows.Forms.TextBox()
        Me.lblEventStatus = New System.Windows.Forms.Label()
        Me.lblEventStatusLabel = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboEventWebContact = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtEventAddress = New System.Windows.Forms.TextBox()
        Me.txtCapacity = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.txtEventCity = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnCancelCreateEvent = New System.Windows.Forms.Button()
        Me.tabsEventDetails = New System.Windows.Forms.TabControl()
        Me.tabEventManagement = New System.Windows.Forms.TabPage()
        Me.tabRegistrationManagement = New System.Windows.Forms.TabPage()
        Me.dgvRegistrants = New Iaip.IaipDataGridView()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.lblStatusUpdateResult = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtOvCancelled = New System.Windows.Forms.TextBox()
        Me.txtOvWaitingList = New System.Windows.Forms.TextBox()
        Me.txtOvEventCapacity = New System.Windows.Forms.TextBox()
        Me.txtOvNumberRegistered = New System.Windows.Forms.TextBox()
        Me.btnEmailWaitList = New System.Windows.Forms.Button()
        Me.lblCancelled = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.btnEmailConfirmed = New System.Windows.Forms.Button()
        Me.btnEmailAll = New System.Windows.Forms.Button()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.btnModifyRegistration = New System.Windows.Forms.Button()
        Me.lblSelectedRegistrantLabel = New System.Windows.Forms.Label()
        Me.cboRegStatus = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnlEventsAndFilter = New System.Windows.Forms.Panel()
        Me.pnlFilterEvents = New System.Windows.Forms.Panel()
        Me.chkShowPast = New System.Windows.Forms.CheckBox()
        Me.btnCreateNewEvent = New System.Windows.Forms.Button()
        Me.dgvEvents = New Iaip.IaipDataGridView()
        Me.pnlEventTitle = New System.Windows.Forms.Panel()
        Me.lblEventTitle = New System.Windows.Forms.Label()
        Me.Panel4.SuspendLayout()
        Me.tabsEventDetails.SuspendLayout()
        Me.tabEventManagement.SuspendLayout()
        Me.tabRegistrationManagement.SuspendLayout()
        CType(Me.dgvRegistrants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.pnlEventsAndFilter.SuspendLayout()
        Me.pnlFilterEvents.SuspendLayout()
        CType(Me.dgvEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEventTitle.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.Controls.Add(Me.lblEventMessage)
        Me.Panel4.Controls.Add(Me.txtWebsiteURL)
        Me.Panel4.Controls.Add(Me.btnCancelEvent)
        Me.Panel4.Controls.Add(Me.lnkGecoLink)
        Me.Panel4.Controls.Add(Me.Label48)
        Me.Panel4.Controls.Add(Me.Label47)
        Me.Panel4.Controls.Add(Me.txtEventTitle)
        Me.Panel4.Controls.Add(Me.txtWebPhone)
        Me.Panel4.Controls.Add(Me.txtEventEndTime)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.txtEventDescription)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.Label32)
        Me.Panel4.Controls.Add(Me.btnGeneratePasscode)
        Me.Panel4.Controls.Add(Me.txtEventVenue)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.DTPEventEndDate)
        Me.Panel4.Controls.Add(Me.chkEventPasscode)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.DTPEventDate)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.txtEventNotes)
        Me.Panel4.Controls.Add(Me.txtEventTime)
        Me.Panel4.Controls.Add(Me.lblEventStatus)
        Me.Panel4.Controls.Add(Me.lblEventStatusLabel)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.cboEventWebContact)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.txtEventAddress)
        Me.Panel4.Controls.Add(Me.txtCapacity)
        Me.Panel4.Controls.Add(Me.txtState)
        Me.Panel4.Controls.Add(Me.txtZip)
        Me.Panel4.Controls.Add(Me.txtEventCity)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.btnCancelCreateEvent)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(852, 308)
        Me.Panel4.TabIndex = 0
        '
        'lblEventMessage
        '
        Me.lblEventMessage.AutoSize = True
        Me.lblEventMessage.Location = New System.Drawing.Point(18, 10)
        Me.lblEventMessage.Name = "lblEventMessage"
        Me.lblEventMessage.Size = New System.Drawing.Size(79, 13)
        Me.lblEventMessage.TabIndex = 261
        Me.lblEventMessage.Text = "event message"
        '
        'txtWebsiteURL
        '
        Me.txtWebsiteURL.Cue = "Must begin with ""http"" or ""https"""
        Me.txtWebsiteURL.Location = New System.Drawing.Point(85, 134)
        Me.txtWebsiteURL.MaxLength = 400
        Me.txtWebsiteURL.Name = "txtWebsiteURL"
        Me.txtWebsiteURL.Size = New System.Drawing.Size(332, 20)
        Me.txtWebsiteURL.TabIndex = 2
        '
        'btnCancelEvent
        '
        Me.btnCancelEvent.AutoSize = True
        Me.btnCancelEvent.Location = New System.Drawing.Point(756, 269)
        Me.btnCancelEvent.Name = "btnCancelEvent"
        Me.btnCancelEvent.Size = New System.Drawing.Size(81, 23)
        Me.btnCancelEvent.TabIndex = 455
        Me.btnCancelEvent.Text = "Cancel Event"
        Me.btnCancelEvent.UseVisualStyleBackColor = True
        '
        'lnkGecoLink
        '
        Me.lnkGecoLink.AutoSize = True
        Me.lnkGecoLink.Location = New System.Drawing.Point(221, 283)
        Me.lnkGecoLink.Name = "lnkGecoLink"
        Me.lnkGecoLink.Size = New System.Drawing.Size(120, 13)
        Me.lnkGecoLink.TabIndex = 20
        Me.lnkGecoLink.TabStop = True
        Me.lnkGecoLink.Text = "View GECO event page"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(191, 189)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(55, 13)
        Me.Label48.TabIndex = 452
        Me.Label48.Text = "End Time:"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(2, 137)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(77, 13)
        Me.Label47.TabIndex = 449
        Me.Label47.Text = "Event website:"
        '
        'txtEventTitle
        '
        Me.txtEventTitle.Location = New System.Drawing.Point(85, 29)
        Me.txtEventTitle.MaxLength = 1000
        Me.txtEventTitle.Name = "txtEventTitle"
        Me.txtEventTitle.Size = New System.Drawing.Size(332, 20)
        Me.txtEventTitle.TabIndex = 0
        '
        'txtWebPhone
        '
        Me.txtWebPhone.Enabled = False
        Me.txtWebPhone.Location = New System.Drawing.Point(717, 108)
        Me.txtWebPhone.Name = "txtWebPhone"
        Me.txtWebPhone.Size = New System.Drawing.Size(120, 20)
        Me.txtWebPhone.TabIndex = 15
        '
        'txtEventEndTime
        '
        Me.txtEventEndTime.Location = New System.Drawing.Point(252, 186)
        Me.txtEventEndTime.MaxLength = 200
        Me.txtEventEndTime.Name = "txtEventEndTime"
        Me.txtEventEndTime.Size = New System.Drawing.Size(120, 20)
        Me.txtEventEndTime.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Event Title:*"
        '
        'txtEventDescription
        '
        Me.txtEventDescription.Location = New System.Drawing.Point(85, 55)
        Me.txtEventDescription.Multiline = True
        Me.txtEventDescription.Name = "txtEventDescription"
        Me.txtEventDescription.Size = New System.Drawing.Size(332, 73)
        Me.txtEventDescription.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Description:"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(191, 163)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(55, 13)
        Me.Label32.TabIndex = 447
        Me.Label32.Text = "End Date:"
        '
        'btnGeneratePasscode
        '
        Me.btnGeneratePasscode.AutoSize = True
        Me.btnGeneratePasscode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnGeneratePasscode.Location = New System.Drawing.Point(588, 210)
        Me.btnGeneratePasscode.Name = "btnGeneratePasscode"
        Me.btnGeneratePasscode.Size = New System.Drawing.Size(114, 23)
        Me.btnGeneratePasscode.TabIndex = 18
        Me.btnGeneratePasscode.Text = "Generate New Code"
        Me.btnGeneratePasscode.UseVisualStyleBackColor = True
        '
        'txtEventVenue
        '
        Me.txtEventVenue.Location = New System.Drawing.Point(505, 29)
        Me.txtEventVenue.MaxLength = 250
        Me.txtEventVenue.Name = "txtEventVenue"
        Me.txtEventVenue.Size = New System.Drawing.Size(224, 20)
        Me.txtEventVenue.TabIndex = 8
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(442, 215)
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
        Me.DTPEventEndDate.Location = New System.Drawing.Point(252, 160)
        Me.DTPEventEndDate.Name = "DTPEventEndDate"
        Me.DTPEventEndDate.ShowCheckBox = True
        Me.DTPEventEndDate.Size = New System.Drawing.Size(120, 20)
        Me.DTPEventEndDate.TabIndex = 4
        '
        'chkEventPasscode
        '
        Me.chkEventPasscode.AutoSize = True
        Me.chkEventPasscode.Location = New System.Drawing.Point(505, 214)
        Me.chkEventPasscode.Name = "chkEventPasscode"
        Me.chkEventPasscode.Size = New System.Drawing.Size(77, 17)
        Me.chkEventPasscode.TabIndex = 17
        Me.chkEventPasscode.Text = "GA123456"
        Me.chkEventPasscode.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(46, 163)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Date:*"
        '
        'btnSave
        '
        Me.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSave.Location = New System.Drawing.Point(85, 264)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 32)
        Me.btnSave.TabIndex = 19
        Me.btnSave.Text = "Save Changes"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'DTPEventDate
        '
        Me.DTPEventDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEventDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEventDate.Location = New System.Drawing.Point(85, 160)
        Me.DTPEventDate.Name = "DTPEventDate"
        Me.DTPEventDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPEventDate.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(458, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 415
        Me.Label7.Text = "Venue:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(446, 137)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 26)
        Me.Label9.TabIndex = 420
        Me.Label9.Text = "Additional" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Notes:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(28, 215)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 417
        Me.Label8.Text = "Capacity:*"
        '
        'txtEventNotes
        '
        Me.txtEventNotes.Location = New System.Drawing.Point(505, 136)
        Me.txtEventNotes.Multiline = True
        Me.txtEventNotes.Name = "txtEventNotes"
        Me.txtEventNotes.Size = New System.Drawing.Size(332, 70)
        Me.txtEventNotes.TabIndex = 16
        '
        'txtEventTime
        '
        Me.txtEventTime.Location = New System.Drawing.Point(85, 186)
        Me.txtEventTime.MaxLength = 200
        Me.txtEventTime.Name = "txtEventTime"
        Me.txtEventTime.Size = New System.Drawing.Size(100, 20)
        Me.txtEventTime.TabIndex = 5
        '
        'lblEventStatus
        '
        Me.lblEventStatus.AutoSize = True
        Me.lblEventStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEventStatus.Location = New System.Drawing.Point(298, 264)
        Me.lblEventStatus.Name = "lblEventStatus"
        Me.lblEventStatus.Size = New System.Drawing.Size(43, 13)
        Me.lblEventStatus.TabIndex = 3
        Me.lblEventStatus.Text = "Status"
        '
        'lblEventStatusLabel
        '
        Me.lblEventStatusLabel.AutoSize = True
        Me.lblEventStatusLabel.Location = New System.Drawing.Point(221, 264)
        Me.lblEventStatusLabel.Name = "lblEventStatusLabel"
        Me.lblEventStatusLabel.Size = New System.Drawing.Size(71, 13)
        Me.lblEventStatusLabel.TabIndex = 3
        Me.lblEventStatusLabel.Text = "Event Status:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(46, 189)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 426
        Me.Label12.Text = "Time:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(452, 111)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 13)
        Me.Label13.TabIndex = 427
        Me.Label13.Text = "Contact:*"
        '
        'cboEventWebContact
        '
        Me.cboEventWebContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEventWebContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEventWebContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEventWebContact.FormattingEnabled = True
        Me.cboEventWebContact.Location = New System.Drawing.Point(505, 107)
        Me.cboEventWebContact.Name = "cboEventWebContact"
        Me.cboEventWebContact.Size = New System.Drawing.Size(142, 21)
        Me.cboEventWebContact.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(653, 111)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(58, 13)
        Me.Label14.TabIndex = 430
        Me.Label14.Text = "Phone No:"
        '
        'txtEventAddress
        '
        Me.txtEventAddress.Location = New System.Drawing.Point(505, 55)
        Me.txtEventAddress.MaxLength = 2000
        Me.txtEventAddress.Name = "txtEventAddress"
        Me.txtEventAddress.Size = New System.Drawing.Size(224, 20)
        Me.txtEventAddress.TabIndex = 9
        '
        'txtCapacity
        '
        Me.txtCapacity.Location = New System.Drawing.Point(85, 212)
        Me.txtCapacity.MaxLength = 5
        Me.txtCapacity.Name = "txtCapacity"
        Me.txtCapacity.Size = New System.Drawing.Size(45, 20)
        Me.txtCapacity.TabIndex = 7
        Me.txtCapacity.Text = "0"
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(623, 81)
        Me.txtState.MaxLength = 2
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(24, 20)
        Me.txtState.TabIndex = 12
        '
        'txtZip
        '
        Me.txtZip.Location = New System.Drawing.Point(653, 81)
        Me.txtZip.MaxLength = 10
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(76, 20)
        Me.txtZip.TabIndex = 13
        '
        'txtEventCity
        '
        Me.txtEventCity.Location = New System.Drawing.Point(505, 81)
        Me.txtEventCity.MaxLength = 2000
        Me.txtEventCity.Name = "txtEventCity"
        Me.txtEventCity.Size = New System.Drawing.Size(112, 20)
        Me.txtEventCity.TabIndex = 10
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(451, 58)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(48, 13)
        Me.Label15.TabIndex = 433
        Me.Label15.Text = "Address:"
        '
        'btnCancelCreateEvent
        '
        Me.btnCancelCreateEvent.AutoSize = True
        Me.btnCancelCreateEvent.Location = New System.Drawing.Point(252, 264)
        Me.btnCancelCreateEvent.Name = "btnCancelCreateEvent"
        Me.btnCancelCreateEvent.Size = New System.Drawing.Size(120, 32)
        Me.btnCancelCreateEvent.TabIndex = 261
        Me.btnCancelCreateEvent.Text = "Cancel New Event"
        Me.btnCancelCreateEvent.UseVisualStyleBackColor = True
        Me.btnCancelCreateEvent.Visible = False
        '
        'tabsEventDetails
        '
        Me.tabsEventDetails.Controls.Add(Me.tabEventManagement)
        Me.tabsEventDetails.Controls.Add(Me.tabRegistrationManagement)
        Me.tabsEventDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tabsEventDetails.Location = New System.Drawing.Point(0, 258)
        Me.tabsEventDetails.Name = "tabsEventDetails"
        Me.tabsEventDetails.SelectedIndex = 0
        Me.tabsEventDetails.Size = New System.Drawing.Size(866, 340)
        Me.tabsEventDetails.TabIndex = 1
        '
        'tabEventManagement
        '
        Me.tabEventManagement.Controls.Add(Me.Panel4)
        Me.tabEventManagement.Location = New System.Drawing.Point(4, 22)
        Me.tabEventManagement.Name = "tabEventManagement"
        Me.tabEventManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEventManagement.Size = New System.Drawing.Size(858, 314)
        Me.tabEventManagement.TabIndex = 0
        Me.tabEventManagement.Text = "Event Management"
        Me.tabEventManagement.UseVisualStyleBackColor = True
        '
        'tabRegistrationManagement
        '
        Me.tabRegistrationManagement.Controls.Add(Me.dgvRegistrants)
        Me.tabRegistrationManagement.Controls.Add(Me.Panel8)
        Me.tabRegistrationManagement.Location = New System.Drawing.Point(4, 22)
        Me.tabRegistrationManagement.Name = "tabRegistrationManagement"
        Me.tabRegistrationManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRegistrationManagement.Size = New System.Drawing.Size(858, 314)
        Me.tabRegistrationManagement.TabIndex = 1
        Me.tabRegistrationManagement.Text = "Registration Management"
        Me.tabRegistrationManagement.UseVisualStyleBackColor = True
        '
        'dgvRegistrants
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvRegistrants.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvRegistrants.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvRegistrants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRegistrants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRegistrants.LinkifyColumnByName = Nothing
        Me.dgvRegistrants.Location = New System.Drawing.Point(3, 3)
        Me.dgvRegistrants.Name = "dgvRegistrants"
        Me.dgvRegistrants.ResultsCountLabel = Nothing
        Me.dgvRegistrants.ResultsCountLabelFormat = "{0} found"
        Me.dgvRegistrants.ShowEditingIcon = False
        Me.dgvRegistrants.Size = New System.Drawing.Size(852, 225)
        Me.dgvRegistrants.StandardTab = True
        Me.dgvRegistrants.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.lblStatusUpdateResult)
        Me.Panel8.Controls.Add(Me.lblEmail)
        Me.Panel8.Controls.Add(Me.txtOvCancelled)
        Me.Panel8.Controls.Add(Me.txtOvWaitingList)
        Me.Panel8.Controls.Add(Me.txtOvEventCapacity)
        Me.Panel8.Controls.Add(Me.txtOvNumberRegistered)
        Me.Panel8.Controls.Add(Me.btnEmailWaitList)
        Me.Panel8.Controls.Add(Me.lblCancelled)
        Me.Panel8.Controls.Add(Me.Label36)
        Me.Panel8.Controls.Add(Me.btnEmailConfirmed)
        Me.Panel8.Controls.Add(Me.btnEmailAll)
        Me.Panel8.Controls.Add(Me.Label38)
        Me.Panel8.Controls.Add(Me.Label40)
        Me.Panel8.Controls.Add(Me.btnModifyRegistration)
        Me.Panel8.Controls.Add(Me.lblSelectedRegistrantLabel)
        Me.Panel8.Controls.Add(Me.cboRegStatus)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel8.Location = New System.Drawing.Point(3, 228)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(852, 83)
        Me.Panel8.TabIndex = 1
        '
        'lblStatusUpdateResult
        '
        Me.lblStatusUpdateResult.AutoSize = True
        Me.lblStatusUpdateResult.Location = New System.Drawing.Point(361, 48)
        Me.lblStatusUpdateResult.Name = "lblStatusUpdateResult"
        Me.lblStatusUpdateResult.Size = New System.Drawing.Size(75, 13)
        Me.lblStatusUpdateResult.TabIndex = 480
        Me.lblStatusUpdateResult.Text = "Update Result"
        '
        'lblEmail
        '
        Me.lblEmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(584, 48)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(35, 13)
        Me.lblEmail.TabIndex = 474
        Me.lblEmail.Text = "Email:"
        '
        'txtOvCancelled
        '
        Me.txtOvCancelled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOvCancelled.Location = New System.Drawing.Point(810, 13)
        Me.txtOvCancelled.Multiline = True
        Me.txtOvCancelled.Name = "txtOvCancelled"
        Me.txtOvCancelled.ReadOnly = True
        Me.txtOvCancelled.Size = New System.Drawing.Size(37, 20)
        Me.txtOvCancelled.TabIndex = 7
        '
        'txtOvWaitingList
        '
        Me.txtOvWaitingList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOvWaitingList.Location = New System.Drawing.Point(704, 13)
        Me.txtOvWaitingList.Multiline = True
        Me.txtOvWaitingList.Name = "txtOvWaitingList"
        Me.txtOvWaitingList.ReadOnly = True
        Me.txtOvWaitingList.Size = New System.Drawing.Size(37, 20)
        Me.txtOvWaitingList.TabIndex = 5
        '
        'txtOvEventCapacity
        '
        Me.txtOvEventCapacity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOvEventCapacity.Location = New System.Drawing.Point(584, 13)
        Me.txtOvEventCapacity.Multiline = True
        Me.txtOvEventCapacity.Name = "txtOvEventCapacity"
        Me.txtOvEventCapacity.ReadOnly = True
        Me.txtOvEventCapacity.Size = New System.Drawing.Size(43, 20)
        Me.txtOvEventCapacity.TabIndex = 3
        '
        'txtOvNumberRegistered
        '
        Me.txtOvNumberRegistered.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOvNumberRegistered.Location = New System.Drawing.Point(529, 13)
        Me.txtOvNumberRegistered.Multiline = True
        Me.txtOvNumberRegistered.Name = "txtOvNumberRegistered"
        Me.txtOvNumberRegistered.ReadOnly = True
        Me.txtOvNumberRegistered.Size = New System.Drawing.Size(43, 20)
        Me.txtOvNumberRegistered.TabIndex = 2
        '
        'btnEmailWaitList
        '
        Me.btnEmailWaitList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEmailWaitList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailWaitList.Location = New System.Drawing.Point(701, 43)
        Me.btnEmailWaitList.Name = "btnEmailWaitList"
        Me.btnEmailWaitList.Size = New System.Drawing.Size(70, 23)
        Me.btnEmailWaitList.TabIndex = 9
        Me.btnEmailWaitList.Text = "Wait List"
        Me.btnEmailWaitList.UseVisualStyleBackColor = True
        '
        'lblCancelled
        '
        Me.lblCancelled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCancelled.AutoSize = True
        Me.lblCancelled.Location = New System.Drawing.Point(747, 16)
        Me.lblCancelled.Name = "lblCancelled"
        Me.lblCancelled.Size = New System.Drawing.Size(57, 13)
        Me.lblCancelled.TabIndex = 6
        Me.lblCancelled.Text = "Cancelled:"
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(633, 16)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(65, 13)
        Me.Label36.TabIndex = 4
        Me.Label36.Text = "Waiting List:"
        '
        'btnEmailConfirmed
        '
        Me.btnEmailConfirmed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEmailConfirmed.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailConfirmed.Location = New System.Drawing.Point(625, 43)
        Me.btnEmailConfirmed.Name = "btnEmailConfirmed"
        Me.btnEmailConfirmed.Size = New System.Drawing.Size(70, 23)
        Me.btnEmailConfirmed.TabIndex = 8
        Me.btnEmailConfirmed.Text = "Confirmed"
        Me.btnEmailConfirmed.UseVisualStyleBackColor = True
        '
        'btnEmailAll
        '
        Me.btnEmailAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEmailAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailAll.Location = New System.Drawing.Point(777, 43)
        Me.btnEmailAll.Name = "btnEmailAll"
        Me.btnEmailAll.Size = New System.Drawing.Size(70, 23)
        Me.btnEmailAll.TabIndex = 10
        Me.btnEmailAll.Text = "All"
        Me.btnEmailAll.UseVisualStyleBackColor = True
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(570, 16)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(16, 13)
        Me.Label38.TabIndex = 468
        Me.Label38.Text = "of"
        '
        'Label40
        '
        Me.Label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(462, 16)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(61, 13)
        Me.Label40.TabIndex = 470
        Me.Label40.Text = "Registered:"
        '
        'btnModifyRegistration
        '
        Me.btnModifyRegistration.AutoSize = True
        Me.btnModifyRegistration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnModifyRegistration.Location = New System.Drawing.Point(270, 43)
        Me.btnModifyRegistration.Name = "btnModifyRegistration"
        Me.btnModifyRegistration.Size = New System.Drawing.Size(85, 23)
        Me.btnModifyRegistration.TabIndex = 1
        Me.btnModifyRegistration.Text = "Update Status"
        Me.btnModifyRegistration.UseVisualStyleBackColor = True
        '
        'lblSelectedRegistrantLabel
        '
        Me.lblSelectedRegistrantLabel.AutoSize = True
        Me.lblSelectedRegistrantLabel.Location = New System.Drawing.Point(5, 16)
        Me.lblSelectedRegistrantLabel.Name = "lblSelectedRegistrantLabel"
        Me.lblSelectedRegistrantLabel.Size = New System.Drawing.Size(83, 13)
        Me.lblSelectedRegistrantLabel.TabIndex = 440
        Me.lblSelectedRegistrantLabel.Text = "Name and email"
        '
        'cboRegStatus
        '
        Me.cboRegStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRegStatus.FormattingEnabled = True
        Me.cboRegStatus.Location = New System.Drawing.Point(110, 45)
        Me.cboRegStatus.Name = "cboRegStatus"
        Me.cboRegStatus.Size = New System.Drawing.Size(154, 21)
        Me.cboRegStatus.TabIndex = 0
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(5, 48)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(99, 13)
        Me.Label22.TabIndex = 435
        Me.Label22.Text = "Registration Status:"
        '
        'pnlEventsAndFilter
        '
        Me.pnlEventsAndFilter.Controls.Add(Me.pnlFilterEvents)
        Me.pnlEventsAndFilter.Controls.Add(Me.pnlEventTitle)
        Me.pnlEventsAndFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEventsAndFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlEventsAndFilter.Name = "pnlEventsAndFilter"
        Me.pnlEventsAndFilter.Size = New System.Drawing.Size(866, 258)
        Me.pnlEventsAndFilter.TabIndex = 0
        '
        'pnlFilterEvents
        '
        Me.pnlFilterEvents.Controls.Add(Me.chkShowPast)
        Me.pnlFilterEvents.Controls.Add(Me.btnCreateNewEvent)
        Me.pnlFilterEvents.Controls.Add(Me.dgvEvents)
        Me.pnlFilterEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterEvents.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterEvents.Name = "pnlFilterEvents"
        Me.pnlFilterEvents.Size = New System.Drawing.Size(866, 223)
        Me.pnlFilterEvents.TabIndex = 0
        '
        'chkShowPast
        '
        Me.chkShowPast.AutoSize = True
        Me.chkShowPast.Location = New System.Drawing.Point(12, 16)
        Me.chkShowPast.Name = "chkShowPast"
        Me.chkShowPast.Size = New System.Drawing.Size(113, 17)
        Me.chkShowPast.TabIndex = 0
        Me.chkShowPast.Text = "Show Past Events"
        Me.chkShowPast.UseVisualStyleBackColor = True
        '
        'btnCreateNewEvent
        '
        Me.btnCreateNewEvent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateNewEvent.Location = New System.Drawing.Point(723, 12)
        Me.btnCreateNewEvent.Name = "btnCreateNewEvent"
        Me.btnCreateNewEvent.Size = New System.Drawing.Size(131, 23)
        Me.btnCreateNewEvent.TabIndex = 0
        Me.btnCreateNewEvent.Text = "+ Create New Event"
        Me.btnCreateNewEvent.UseVisualStyleBackColor = True
        '
        'dgvEvents
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvEvents.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvEvents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEvents.LinkifyColumnByName = Nothing
        Me.dgvEvents.Location = New System.Drawing.Point(12, 41)
        Me.dgvEvents.Name = "dgvEvents"
        Me.dgvEvents.ResultsCountLabel = Nothing
        Me.dgvEvents.ResultsCountLabelFormat = "{0} found"
        Me.dgvEvents.Size = New System.Drawing.Size(842, 176)
        Me.dgvEvents.StandardTab = True
        Me.dgvEvents.TabIndex = 1
        '
        'pnlEventTitle
        '
        Me.pnlEventTitle.Controls.Add(Me.lblEventTitle)
        Me.pnlEventTitle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlEventTitle.Location = New System.Drawing.Point(0, 223)
        Me.pnlEventTitle.Name = "pnlEventTitle"
        Me.pnlEventTitle.Size = New System.Drawing.Size(866, 35)
        Me.pnlEventTitle.TabIndex = 1
        '
        'lblEventTitle
        '
        Me.lblEventTitle.AutoSize = True
        Me.lblEventTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEventTitle.Location = New System.Drawing.Point(13, 7)
        Me.lblEventTitle.Name = "lblEventTitle"
        Me.lblEventTitle.Size = New System.Drawing.Size(208, 17)
        Me.lblEventTitle.TabIndex = 0
        Me.lblEventTitle.Text = "Select an event to view/edit"
        '
        'EventsManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(866, 598)
        Me.Controls.Add(Me.pnlEventsAndFilter)
        Me.Controls.Add(Me.tabsEventDetails)
        Me.MinimumSize = New System.Drawing.Size(882, 572)
        Me.Name = "EventsManagement"
        Me.Text = "EPD Events"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.tabsEventDetails.ResumeLayout(False)
        Me.tabEventManagement.ResumeLayout(False)
        Me.tabEventManagement.PerformLayout()
        Me.tabRegistrationManagement.ResumeLayout(False)
        CType(Me.dgvRegistrants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.pnlEventsAndFilter.ResumeLayout(False)
        Me.pnlFilterEvents.ResumeLayout(False)
        Me.pnlFilterEvents.PerformLayout()
        CType(Me.dgvEvents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEventTitle.ResumeLayout(False)
        Me.pnlEventTitle.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblEventStatusLabel As System.Windows.Forms.Label
    Friend WithEvents txtEventTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEventDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEventVenue As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DTPEventDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEventNotes As System.Windows.Forms.TextBox
    Friend WithEvents tabsEventDetails As System.Windows.Forms.TabControl
    Friend WithEvents tabEventManagement As System.Windows.Forms.TabPage
    Friend WithEvents tabRegistrationManagement As System.Windows.Forms.TabPage
    Friend WithEvents pnlEventsAndFilter As System.Windows.Forms.Panel
    Friend WithEvents dgvEvents As IaipDataGridView
    Friend WithEvents pnlFilterEvents As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtEventTime As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboEventWebContact As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtEventCity As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtEventAddress As System.Windows.Forms.TextBox
    Friend WithEvents btnGeneratePasscode As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkEventPasscode As System.Windows.Forms.CheckBox
    Friend WithEvents dgvRegistrants As IaipDataGridView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents lblSelectedRegistrantLabel As System.Windows.Forms.Label
    Friend WithEvents cboRegStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents DTPEventEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtEventEndTime As System.Windows.Forms.TextBox
    Friend WithEvents btnModifyRegistration As System.Windows.Forms.Button
    Friend WithEvents btnCreateNewEvent As System.Windows.Forms.Button
    Friend WithEvents pnlEventTitle As System.Windows.Forms.Panel
    Friend WithEvents chkShowPast As CheckBox
    Friend WithEvents lnkGecoLink As LinkLabel
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtOvCancelled As TextBox
    Friend WithEvents txtOvWaitingList As TextBox
    Friend WithEvents txtOvEventCapacity As TextBox
    Friend WithEvents txtOvNumberRegistered As TextBox
    Friend WithEvents btnEmailWaitList As Button
    Friend WithEvents lblCancelled As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents btnEmailConfirmed As Button
    Friend WithEvents btnEmailAll As Button
    Friend WithEvents Label38 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents lblEventTitle As Label
    Friend WithEvents btnCancelEvent As Button
    Friend WithEvents txtState As TextBox
    Friend WithEvents txtZip As TextBox
    Friend WithEvents txtWebPhone As TextBox
    Friend WithEvents txtCapacity As TextBox
    Friend WithEvents txtWebsiteURL As CueTextBox
    Friend WithEvents lblEventStatus As Label
    Friend WithEvents btnCancelCreateEvent As Button
    Friend WithEvents lblStatusUpdateResult As Label
    Friend WithEvents lblEventMessage As Label
End Class
