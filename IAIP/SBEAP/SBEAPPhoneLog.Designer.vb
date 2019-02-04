<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBEAPPhoneLog
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbClientSearch = New System.Windows.Forms.ToolStripButton()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiClearCaseID = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtCaseSummary = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label199 = New System.Windows.Forms.Label()
        Me.txtCaseID = New System.Windows.Forms.TextBox()
        Me.txtClientID = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboStaffResponsible = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtClientInformation = New System.Windows.Forms.TextBox()
        Me.txtOutstandingCases = New System.Windows.Forms.TextBox()
        Me.btnRefreshClient = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DTPCaseOpened = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DTPCaseClosed = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdbNewClient = New System.Windows.Forms.RadioButton()
        Me.rdbExistingClient = New System.Windows.Forms.RadioButton()
        Me.pnlExistingClient = New System.Windows.Forms.Panel()
        Me.pnlNewClient = New System.Windows.Forms.Panel()
        Me.mtbPhoneNumber = New System.Windows.Forms.MaskedTextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnCreateNewClient = New System.Windows.Forms.Button()
        Me.chbOnetimeAssist = New System.Windows.Forms.CheckBox()
        Me.chbFrontDeskCall = New System.Windows.Forms.CheckBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCallName = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtReferralInformation = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.pnlDetails = New System.Windows.Forms.Panel()
        Me.txtActionID = New System.Windows.Forms.TextBox()
        Me.txtPhoneCallNotes = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnNewCall = New System.Windows.Forms.Button()
        Me.pnlClientInfo = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlExistingClient.SuspendLayout()
        Me.pnlNewClient.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlClientInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbClientSearch})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(834, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSave.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(23, 22)
        Me.tsbSave.Text = "Save"
        '
        'tsbClientSearch
        '
        Me.tsbClientSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClientSearch.Image = Global.Iaip.My.Resources.Resources.FindIcon
        Me.tsbClientSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClientSearch.Name = "tsbClientSearch"
        Me.tsbClientSearch.Size = New System.Drawing.Size(23, 22)
        Me.tsbClientSearch.Text = "Client Search"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(834, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileMenuItem
        '
        Me.FileMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiClearCaseID})
        Me.FileMenuItem.Name = "FileMenuItem"
        Me.FileMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileMenuItem.Text = "&File"
        '
        'mmiClearCaseID
        '
        Me.mmiClearCaseID.Name = "mmiClearCaseID"
        Me.mmiClearCaseID.Size = New System.Drawing.Size(143, 22)
        Me.mmiClearCaseID.Text = "&Clear Case ID"
        '
        'txtCaseSummary
        '
        Me.txtCaseSummary.Location = New System.Drawing.Point(111, 87)
        Me.txtCaseSummary.Multiline = True
        Me.txtCaseSummary.Name = "txtCaseSummary"
        Me.txtCaseSummary.Size = New System.Drawing.Size(442, 56)
        Me.txtCaseSummary.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 90)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 13)
        Me.Label11.TabIndex = 51
        Me.Label11.Text = "Case Description:"
        '
        'Label199
        '
        Me.Label199.AutoSize = True
        Me.Label199.Location = New System.Drawing.Point(3, 12)
        Me.Label199.Name = "Label199"
        Me.Label199.Size = New System.Drawing.Size(48, 13)
        Me.Label199.TabIndex = 36
        Me.Label199.Text = "Case ID:"
        '
        'txtCaseID
        '
        Me.txtCaseID.Location = New System.Drawing.Point(52, 9)
        Me.txtCaseID.Name = "txtCaseID"
        Me.txtCaseID.ReadOnly = True
        Me.txtCaseID.Size = New System.Drawing.Size(83, 20)
        Me.txtCaseID.TabIndex = 35
        '
        'txtClientID
        '
        Me.txtClientID.Location = New System.Drawing.Point(72, 13)
        Me.txtClientID.Name = "txtClientID"
        Me.txtClientID.Size = New System.Drawing.Size(83, 20)
        Me.txtClientID.TabIndex = 45
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(200, 17)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(99, 13)
        Me.Label12.TabIndex = 49
        Me.Label12.Text = "Outstanding Cases:"
        '
        'cboStaffResponsible
        '
        Me.cboStaffResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboStaffResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaffResponsible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStaffResponsible.FormattingEnabled = True
        Me.cboStaffResponsible.Location = New System.Drawing.Point(111, 41)
        Me.cboStaffResponsible.Name = "cboStaffResponsible"
        Me.cboStaffResponsible.Size = New System.Drawing.Size(258, 21)
        Me.cboStaffResponsible.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Staff Responsible:"
        '
        'txtClientInformation
        '
        Me.txtClientInformation.Location = New System.Drawing.Point(6, 39)
        Me.txtClientInformation.Multiline = True
        Me.txtClientInformation.Name = "txtClientInformation"
        Me.txtClientInformation.ReadOnly = True
        Me.txtClientInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtClientInformation.Size = New System.Drawing.Size(333, 109)
        Me.txtClientInformation.TabIndex = 47
        '
        'txtOutstandingCases
        '
        Me.txtOutstandingCases.Location = New System.Drawing.Point(305, 13)
        Me.txtOutstandingCases.Name = "txtOutstandingCases"
        Me.txtOutstandingCases.ReadOnly = True
        Me.txtOutstandingCases.Size = New System.Drawing.Size(34, 20)
        Me.txtOutstandingCases.TabIndex = 48
        '
        'btnRefreshClient
        '
        Me.btnRefreshClient.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshClient.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefreshClient.Location = New System.Drawing.Point(161, 11)
        Me.btnRefreshClient.Name = "btnRefreshClient"
        Me.btnRefreshClient.Size = New System.Drawing.Size(22, 24)
        Me.btnRefreshClient.TabIndex = 50
        Me.btnRefreshClient.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Customer ID:"
        '
        'DTPCaseOpened
        '
        Me.DTPCaseOpened.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCaseOpened.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCaseOpened.Location = New System.Drawing.Point(111, 6)
        Me.DTPCaseOpened.Name = "DTPCaseOpened"
        Me.DTPCaseOpened.Size = New System.Drawing.Size(107, 20)
        Me.DTPCaseOpened.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(224, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 13)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Date Case Closed"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "Date Case Opened"
        '
        'DTPCaseClosed
        '
        Me.DTPCaseClosed.Checked = False
        Me.DTPCaseClosed.CustomFormat = "dd-MMM-yyyy"
        Me.DTPCaseClosed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCaseClosed.Location = New System.Drawing.Point(322, 6)
        Me.DTPCaseClosed.Name = "DTPCaseClosed"
        Me.DTPCaseClosed.ShowCheckBox = True
        Me.DTPCaseClosed.Size = New System.Drawing.Size(107, 20)
        Me.DTPCaseClosed.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.rdbNewClient)
        Me.Panel1.Controls.Add(Me.rdbExistingClient)
        Me.Panel1.Location = New System.Drawing.Point(146, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(215, 29)
        Me.Panel1.TabIndex = 53
        '
        'rdbNewClient
        '
        Me.rdbNewClient.AutoSize = True
        Me.rdbNewClient.Location = New System.Drawing.Point(118, 3)
        Me.rdbNewClient.Name = "rdbNewClient"
        Me.rdbNewClient.Size = New System.Drawing.Size(94, 17)
        Me.rdbNewClient.TabIndex = 1
        Me.rdbNewClient.TabStop = True
        Me.rdbNewClient.Text = "New Customer"
        Me.rdbNewClient.UseVisualStyleBackColor = True
        '
        'rdbExistingClient
        '
        Me.rdbExistingClient.AutoSize = True
        Me.rdbExistingClient.Location = New System.Drawing.Point(3, 3)
        Me.rdbExistingClient.Name = "rdbExistingClient"
        Me.rdbExistingClient.Size = New System.Drawing.Size(108, 17)
        Me.rdbExistingClient.TabIndex = 0
        Me.rdbExistingClient.TabStop = True
        Me.rdbExistingClient.Text = "Existing Customer"
        Me.rdbExistingClient.UseVisualStyleBackColor = True
        '
        'pnlExistingClient
        '
        Me.pnlExistingClient.Controls.Add(Me.txtClientID)
        Me.pnlExistingClient.Controls.Add(Me.Label10)
        Me.pnlExistingClient.Controls.Add(Me.btnRefreshClient)
        Me.pnlExistingClient.Controls.Add(Me.txtClientInformation)
        Me.pnlExistingClient.Controls.Add(Me.txtOutstandingCases)
        Me.pnlExistingClient.Controls.Add(Me.Label12)
        Me.pnlExistingClient.Location = New System.Drawing.Point(0, 0)
        Me.pnlExistingClient.Name = "pnlExistingClient"
        Me.pnlExistingClient.Size = New System.Drawing.Size(350, 152)
        Me.pnlExistingClient.TabIndex = 54
        Me.pnlExistingClient.Visible = False
        '
        'pnlNewClient
        '
        Me.pnlNewClient.Controls.Add(Me.mtbPhoneNumber)
        Me.pnlNewClient.Controls.Add(Me.Label26)
        Me.pnlNewClient.Controls.Add(Me.btnCreateNewClient)
        Me.pnlNewClient.Controls.Add(Me.chbOnetimeAssist)
        Me.pnlNewClient.Controls.Add(Me.chbFrontDeskCall)
        Me.pnlNewClient.Controls.Add(Me.txtDescription)
        Me.pnlNewClient.Controls.Add(Me.Label6)
        Me.pnlNewClient.Controls.Add(Me.txtCallName)
        Me.pnlNewClient.Controls.Add(Me.Label19)
        Me.pnlNewClient.Location = New System.Drawing.Point(356, 0)
        Me.pnlNewClient.Name = "pnlNewClient"
        Me.pnlNewClient.Size = New System.Drawing.Size(478, 152)
        Me.pnlNewClient.TabIndex = 55
        Me.pnlNewClient.Visible = False
        '
        'mtbPhoneNumber
        '
        Me.mtbPhoneNumber.Location = New System.Drawing.Point(74, 66)
        Me.mtbPhoneNumber.Mask = "(999) 000-0000 ext:00000"
        Me.mtbPhoneNumber.Name = "mtbPhoneNumber"
        Me.mtbPhoneNumber.Size = New System.Drawing.Size(135, 20)
        Me.mtbPhoneNumber.TabIndex = 2
        Me.mtbPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(5, 69)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(51, 13)
        Me.Label26.TabIndex = 15
        Me.Label26.Text = "Phone #:"
        '
        'btnCreateNewClient
        '
        Me.btnCreateNewClient.AutoSize = True
        Me.btnCreateNewClient.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnCreateNewClient.Location = New System.Drawing.Point(183, 121)
        Me.btnCreateNewClient.Name = "btnCreateNewClient"
        Me.btnCreateNewClient.Size = New System.Drawing.Size(115, 23)
        Me.btnCreateNewClient.TabIndex = 14
        Me.btnCreateNewClient.Text = "Open New Customer"
        Me.btnCreateNewClient.UseVisualStyleBackColor = True
        Me.btnCreateNewClient.Visible = False
        '
        'chbOnetimeAssist
        '
        Me.chbOnetimeAssist.AutoSize = True
        Me.chbOnetimeAssist.Location = New System.Drawing.Point(178, 92)
        Me.chbOnetimeAssist.Name = "chbOnetimeAssist"
        Me.chbOnetimeAssist.Size = New System.Drawing.Size(98, 17)
        Me.chbOnetimeAssist.TabIndex = 4
        Me.chbOnetimeAssist.Text = "One-time Assist"
        Me.chbOnetimeAssist.UseVisualStyleBackColor = True
        '
        'chbFrontDeskCall
        '
        Me.chbFrontDeskCall.AutoSize = True
        Me.chbFrontDeskCall.Location = New System.Drawing.Point(74, 92)
        Me.chbFrontDeskCall.Name = "chbFrontDeskCall"
        Me.chbFrontDeskCall.Size = New System.Drawing.Size(98, 17)
        Me.chbFrontDeskCall.TabIndex = 3
        Me.chbFrontDeskCall.Text = "Front Desk Call"
        Me.chbFrontDeskCall.UseVisualStyleBackColor = True
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(74, 40)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(224, 20)
        Me.txtDescription.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Description:"
        '
        'txtCallName
        '
        Me.txtCallName.Location = New System.Drawing.Point(74, 14)
        Me.txtCallName.Name = "txtCallName"
        Me.txtCallName.Size = New System.Drawing.Size(224, 20)
        Me.txtCallName.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(5, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(36, 13)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "Caller:"
        '
        'txtReferralInformation
        '
        Me.txtReferralInformation.AcceptsReturn = True
        Me.txtReferralInformation.Location = New System.Drawing.Point(111, 162)
        Me.txtReferralInformation.Multiline = True
        Me.txtReferralInformation.Name = "txtReferralInformation"
        Me.txtReferralInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReferralInformation.Size = New System.Drawing.Size(442, 82)
        Me.txtReferralInformation.TabIndex = 4
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(3, 165)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(102, 13)
        Me.Label28.TabIndex = 56
        Me.Label28.Text = "Referral Information:"
        '
        'pnlDetails
        '
        Me.pnlDetails.Controls.Add(Me.txtActionID)
        Me.pnlDetails.Controls.Add(Me.txtPhoneCallNotes)
        Me.pnlDetails.Controls.Add(Me.Label38)
        Me.pnlDetails.Controls.Add(Me.txtReferralInformation)
        Me.pnlDetails.Controls.Add(Me.Label28)
        Me.pnlDetails.Controls.Add(Me.txtCaseSummary)
        Me.pnlDetails.Controls.Add(Me.Label11)
        Me.pnlDetails.Controls.Add(Me.cboStaffResponsible)
        Me.pnlDetails.Controls.Add(Me.DTPCaseClosed)
        Me.pnlDetails.Controls.Add(Me.Label5)
        Me.pnlDetails.Controls.Add(Me.Label4)
        Me.pnlDetails.Controls.Add(Me.Label7)
        Me.pnlDetails.Controls.Add(Me.DTPCaseOpened)
        Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetails.Location = New System.Drawing.Point(0, 244)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(834, 369)
        Me.pnlDetails.TabIndex = 58
        Me.pnlDetails.Visible = False
        '
        'txtActionID
        '
        Me.txtActionID.Location = New System.Drawing.Point(445, 6)
        Me.txtActionID.Name = "txtActionID"
        Me.txtActionID.ReadOnly = True
        Me.txtActionID.Size = New System.Drawing.Size(46, 20)
        Me.txtActionID.TabIndex = 60
        Me.txtActionID.Visible = False
        '
        'txtPhoneCallNotes
        '
        Me.txtPhoneCallNotes.AcceptsReturn = True
        Me.txtPhoneCallNotes.Location = New System.Drawing.Point(111, 260)
        Me.txtPhoneCallNotes.Multiline = True
        Me.txtPhoneCallNotes.Name = "txtPhoneCallNotes"
        Me.txtPhoneCallNotes.Size = New System.Drawing.Size(442, 72)
        Me.txtPhoneCallNotes.TabIndex = 5
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(3, 263)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(38, 13)
        Me.Label38.TabIndex = 59
        Me.Label38.Text = "Notes:"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btnNewCall)
        Me.Panel5.Controls.Add(Me.txtCaseID)
        Me.Panel5.Controls.Add(Me.Label199)
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 49)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(834, 40)
        Me.Panel5.TabIndex = 59
        '
        'btnNewCall
        '
        Me.btnNewCall.AutoSize = True
        Me.btnNewCall.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNewCall.Location = New System.Drawing.Point(595, 7)
        Me.btnNewCall.Name = "btnNewCall"
        Me.btnNewCall.Size = New System.Drawing.Size(59, 23)
        Me.btnNewCall.TabIndex = 54
        Me.btnNewCall.Text = "New Call"
        Me.btnNewCall.UseVisualStyleBackColor = True
        '
        'pnlClientInfo
        '
        Me.pnlClientInfo.Controls.Add(Me.pnlExistingClient)
        Me.pnlClientInfo.Controls.Add(Me.pnlNewClient)
        Me.pnlClientInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlClientInfo.Location = New System.Drawing.Point(0, 89)
        Me.pnlClientInfo.Name = "pnlClientInfo"
        Me.pnlClientInfo.Size = New System.Drawing.Size(834, 155)
        Me.pnlClientInfo.TabIndex = 60
        Me.pnlClientInfo.Visible = False
        '
        'SBEAPPhoneLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 613)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.pnlClientInfo)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "SBEAPPhoneLog"
        Me.Text = "SBEAP Phone Log"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlExistingClient.ResumeLayout(False)
        Me.pnlExistingClient.PerformLayout()
        Me.pnlNewClient.ResumeLayout(False)
        Me.pnlNewClient.PerformLayout()
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlClientInfo.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbClientSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtCaseSummary As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label199 As System.Windows.Forms.Label
    Friend WithEvents txtCaseID As System.Windows.Forms.TextBox
    Friend WithEvents txtClientID As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboStaffResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtClientInformation As System.Windows.Forms.TextBox
    Friend WithEvents txtOutstandingCases As System.Windows.Forms.TextBox
    Friend WithEvents btnRefreshClient As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DTPCaseOpened As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DTPCaseClosed As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdbNewClient As System.Windows.Forms.RadioButton
    Friend WithEvents rdbExistingClient As System.Windows.Forms.RadioButton
    Friend WithEvents pnlExistingClient As System.Windows.Forms.Panel
    Friend WithEvents pnlNewClient As System.Windows.Forms.Panel
    Friend WithEvents txtCallName As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chbFrontDeskCall As System.Windows.Forms.CheckBox
    Friend WithEvents chbOnetimeAssist As System.Windows.Forms.CheckBox
    Friend WithEvents txtReferralInformation As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnNewCall As System.Windows.Forms.Button
    Friend WithEvents pnlClientInfo As System.Windows.Forms.Panel
    Friend WithEvents btnCreateNewClient As System.Windows.Forms.Button
    Friend WithEvents mmiClearCaseID As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mtbPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPhoneCallNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtActionID As System.Windows.Forms.TextBox
End Class
