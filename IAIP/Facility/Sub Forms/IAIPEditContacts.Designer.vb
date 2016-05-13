<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPEditContacts
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
        Me.ContactsDataGrid = New System.Windows.Forms.DataGridView
        Me.pnlInfoPanel = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.rdbNewDistrictContact = New System.Windows.Forms.RadioButton
        Me.rdbNewPlanningContact = New System.Windows.Forms.RadioButton
        Me.rdbNewAmbientContact = New System.Windows.Forms.RadioButton
        Me.rdbNewESContact = New System.Windows.Forms.RadioButton
        Me.rdbNewPermittingContact = New System.Windows.Forms.RadioButton
        Me.rdbNewEISContact = New System.Windows.Forms.RadioButton
        Me.rdbNewFeeContact = New System.Windows.Forms.RadioButton
        Me.rdbNewComplianceContact = New System.Windows.Forms.RadioButton
        Me.rdbNewMonitoringContact = New System.Windows.Forms.RadioButton
        Me.btnNewClear = New System.Windows.Forms.Button
        Me.btnNewUpdate = New System.Windows.Forms.Button
        Me.btnNewSave = New System.Windows.Forms.Button
        Me.mtbNewFaxNumber = New System.Windows.Forms.MaskedTextBox
        Me.mtbNewPhoneNumber2 = New System.Windows.Forms.MaskedTextBox
        Me.mtbNewZipCode = New System.Windows.Forms.MaskedTextBox
        Me.mtbNewPhoneNumber = New System.Windows.Forms.MaskedTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtNewCity = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtNewState = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtNewAddress = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtNewFirstName = New System.Windows.Forms.TextBox
        Me.txtNewCompany = New System.Windows.Forms.TextBox
        Me.txtNewEmail = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtNewDescrption = New System.Windows.Forms.TextBox
        Me.txtNewTitle = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtNewSuffix = New System.Windows.Forms.TextBox
        Me.txtNewPrefix = New System.Windows.Forms.TextBox
        Me.txtNewLastName = New System.Windows.Forms.TextBox
        Me.pnlHeaderPanel = New System.Windows.Forms.Panel
        Me.lblAirsNumber = New System.Windows.Forms.Label
        Me.lblFacilityName = New System.Windows.Forms.Label
        CType(Me.ContactsDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInfoPanel.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlHeaderPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContactsDataGrid
        '
        Me.ContactsDataGrid.AccessibleDescription = "Contacts"
        Me.ContactsDataGrid.AllowUserToAddRows = False
        Me.ContactsDataGrid.AllowUserToDeleteRows = False
        Me.ContactsDataGrid.AllowUserToOrderColumns = True
        Me.ContactsDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ContactsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ContactsDataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContactsDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.ContactsDataGrid.Location = New System.Drawing.Point(0, 294)
        Me.ContactsDataGrid.Name = "ContactsDataGrid"
        Me.ContactsDataGrid.ReadOnly = True
        Me.ContactsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ContactsDataGrid.Size = New System.Drawing.Size(799, 307)
        Me.ContactsDataGrid.TabIndex = 243
        '
        'pnlInfoPanel
        '
        Me.pnlInfoPanel.Controls.Add(Me.Panel6)
        Me.pnlInfoPanel.Controls.Add(Me.btnNewClear)
        Me.pnlInfoPanel.Controls.Add(Me.btnNewUpdate)
        Me.pnlInfoPanel.Controls.Add(Me.btnNewSave)
        Me.pnlInfoPanel.Controls.Add(Me.mtbNewFaxNumber)
        Me.pnlInfoPanel.Controls.Add(Me.mtbNewPhoneNumber2)
        Me.pnlInfoPanel.Controls.Add(Me.mtbNewZipCode)
        Me.pnlInfoPanel.Controls.Add(Me.mtbNewPhoneNumber)
        Me.pnlInfoPanel.Controls.Add(Me.Label9)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewCity)
        Me.pnlInfoPanel.Controls.Add(Me.Label11)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewState)
        Me.pnlInfoPanel.Controls.Add(Me.Label12)
        Me.pnlInfoPanel.Controls.Add(Me.Label13)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewAddress)
        Me.pnlInfoPanel.Controls.Add(Me.Label14)
        Me.pnlInfoPanel.Controls.Add(Me.Label15)
        Me.pnlInfoPanel.Controls.Add(Me.Label16)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewFirstName)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewCompany)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewEmail)
        Me.pnlInfoPanel.Controls.Add(Me.Label18)
        Me.pnlInfoPanel.Controls.Add(Me.Label19)
        Me.pnlInfoPanel.Controls.Add(Me.Label20)
        Me.pnlInfoPanel.Controls.Add(Me.Label21)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewDescrption)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewTitle)
        Me.pnlInfoPanel.Controls.Add(Me.Label22)
        Me.pnlInfoPanel.Controls.Add(Me.Label23)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewSuffix)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewPrefix)
        Me.pnlInfoPanel.Controls.Add(Me.txtNewLastName)
        Me.pnlInfoPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInfoPanel.Location = New System.Drawing.Point(0, 33)
        Me.pnlInfoPanel.Name = "pnlInfoPanel"
        Me.pnlInfoPanel.Size = New System.Drawing.Size(799, 261)
        Me.pnlInfoPanel.TabIndex = 246
        '
        'Panel6
        '
        Me.Panel6.AutoSize = True
        Me.Panel6.Controls.Add(Me.rdbNewDistrictContact)
        Me.Panel6.Controls.Add(Me.rdbNewPlanningContact)
        Me.Panel6.Controls.Add(Me.rdbNewAmbientContact)
        Me.Panel6.Controls.Add(Me.rdbNewESContact)
        Me.Panel6.Controls.Add(Me.rdbNewPermittingContact)
        Me.Panel6.Controls.Add(Me.rdbNewEISContact)
        Me.Panel6.Controls.Add(Me.rdbNewFeeContact)
        Me.Panel6.Controls.Add(Me.rdbNewComplianceContact)
        Me.Panel6.Controls.Add(Me.rdbNewMonitoringContact)
        Me.Panel6.Location = New System.Drawing.Point(661, 6)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(126, 209)
        Me.Panel6.TabIndex = 252
        '
        'rdbNewDistrictContact
        '
        Me.rdbNewDistrictContact.AutoSize = True
        Me.rdbNewDistrictContact.Location = New System.Drawing.Point(3, 189)
        Me.rdbNewDistrictContact.Name = "rdbNewDistrictContact"
        Me.rdbNewDistrictContact.Size = New System.Drawing.Size(97, 17)
        Me.rdbNewDistrictContact.TabIndex = 8
        Me.rdbNewDistrictContact.TabStop = True
        Me.rdbNewDistrictContact.Text = "District Contact"
        Me.rdbNewDistrictContact.UseVisualStyleBackColor = True
        '
        'rdbNewPlanningContact
        '
        Me.rdbNewPlanningContact.AutoSize = True
        Me.rdbNewPlanningContact.Location = New System.Drawing.Point(3, 166)
        Me.rdbNewPlanningContact.Name = "rdbNewPlanningContact"
        Me.rdbNewPlanningContact.Size = New System.Drawing.Size(106, 17)
        Me.rdbNewPlanningContact.TabIndex = 7
        Me.rdbNewPlanningContact.TabStop = True
        Me.rdbNewPlanningContact.Text = "Planning Contact"
        Me.rdbNewPlanningContact.UseVisualStyleBackColor = True
        '
        'rdbNewAmbientContact
        '
        Me.rdbNewAmbientContact.AutoSize = True
        Me.rdbNewAmbientContact.Location = New System.Drawing.Point(3, 143)
        Me.rdbNewAmbientContact.Name = "rdbNewAmbientContact"
        Me.rdbNewAmbientContact.Size = New System.Drawing.Size(103, 17)
        Me.rdbNewAmbientContact.TabIndex = 6
        Me.rdbNewAmbientContact.TabStop = True
        Me.rdbNewAmbientContact.Text = "Ambient Contact"
        Me.rdbNewAmbientContact.UseVisualStyleBackColor = True
        '
        'rdbNewESContact
        '
        Me.rdbNewESContact.AutoSize = True
        Me.rdbNewESContact.Location = New System.Drawing.Point(3, 120)
        Me.rdbNewESContact.Name = "rdbNewESContact"
        Me.rdbNewESContact.Size = New System.Drawing.Size(79, 17)
        Me.rdbNewESContact.TabIndex = 5
        Me.rdbNewESContact.TabStop = True
        Me.rdbNewESContact.Text = "ES Contact"
        Me.rdbNewESContact.UseVisualStyleBackColor = True
        '
        'rdbNewPermittingContact
        '
        Me.rdbNewPermittingContact.AutoSize = True
        Me.rdbNewPermittingContact.Location = New System.Drawing.Point(3, 53)
        Me.rdbNewPermittingContact.Name = "rdbNewPermittingContact"
        Me.rdbNewPermittingContact.Size = New System.Drawing.Size(111, 17)
        Me.rdbNewPermittingContact.TabIndex = 2
        Me.rdbNewPermittingContact.TabStop = True
        Me.rdbNewPermittingContact.Text = "Permitting Contact"
        Me.rdbNewPermittingContact.UseVisualStyleBackColor = True
        '
        'rdbNewEISContact
        '
        Me.rdbNewEISContact.AutoSize = True
        Me.rdbNewEISContact.Location = New System.Drawing.Point(3, 97)
        Me.rdbNewEISContact.Name = "rdbNewEISContact"
        Me.rdbNewEISContact.Size = New System.Drawing.Size(82, 17)
        Me.rdbNewEISContact.TabIndex = 4
        Me.rdbNewEISContact.TabStop = True
        Me.rdbNewEISContact.Text = "EIS Contact"
        Me.rdbNewEISContact.UseVisualStyleBackColor = True
        '
        'rdbNewFeeContact
        '
        Me.rdbNewFeeContact.AutoSize = True
        Me.rdbNewFeeContact.Location = New System.Drawing.Point(3, 74)
        Me.rdbNewFeeContact.Name = "rdbNewFeeContact"
        Me.rdbNewFeeContact.Size = New System.Drawing.Size(83, 17)
        Me.rdbNewFeeContact.TabIndex = 3
        Me.rdbNewFeeContact.TabStop = True
        Me.rdbNewFeeContact.Text = "Fee Contact"
        Me.rdbNewFeeContact.UseVisualStyleBackColor = True
        '
        'rdbNewComplianceContact
        '
        Me.rdbNewComplianceContact.AutoSize = True
        Me.rdbNewComplianceContact.Location = New System.Drawing.Point(3, 29)
        Me.rdbNewComplianceContact.Name = "rdbNewComplianceContact"
        Me.rdbNewComplianceContact.Size = New System.Drawing.Size(120, 17)
        Me.rdbNewComplianceContact.TabIndex = 1
        Me.rdbNewComplianceContact.TabStop = True
        Me.rdbNewComplianceContact.Text = "Compliance Contact"
        Me.rdbNewComplianceContact.UseVisualStyleBackColor = True
        '
        'rdbNewMonitoringContact
        '
        Me.rdbNewMonitoringContact.AutoSize = True
        Me.rdbNewMonitoringContact.Location = New System.Drawing.Point(3, 6)
        Me.rdbNewMonitoringContact.Name = "rdbNewMonitoringContact"
        Me.rdbNewMonitoringContact.Size = New System.Drawing.Size(114, 17)
        Me.rdbNewMonitoringContact.TabIndex = 0
        Me.rdbNewMonitoringContact.TabStop = True
        Me.rdbNewMonitoringContact.Text = "Monitoring Contact"
        Me.rdbNewMonitoringContact.UseVisualStyleBackColor = True
        '
        'btnNewClear
        '
        Me.btnNewClear.AutoSize = True
        Me.btnNewClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNewClear.Location = New System.Drawing.Point(582, 221)
        Me.btnNewClear.Name = "btnNewClear"
        Me.btnNewClear.Size = New System.Drawing.Size(67, 23)
        Me.btnNewClear.TabIndex = 17
        Me.btnNewClear.Text = "Clear Form"
        Me.btnNewClear.UseVisualStyleBackColor = True
        '
        'btnNewUpdate
        '
        Me.btnNewUpdate.AutoSize = True
        Me.btnNewUpdate.Location = New System.Drawing.Point(230, 221)
        Me.btnNewUpdate.Name = "btnNewUpdate"
        Me.btnNewUpdate.Size = New System.Drawing.Size(137, 23)
        Me.btnNewUpdate.TabIndex = 16
        Me.btnNewUpdate.Text = "Update Selected Contact"
        Me.btnNewUpdate.UseVisualStyleBackColor = True
        '
        'btnNewSave
        '
        Me.btnNewSave.AutoSize = True
        Me.btnNewSave.Location = New System.Drawing.Point(111, 221)
        Me.btnNewSave.Name = "btnNewSave"
        Me.btnNewSave.Size = New System.Drawing.Size(107, 23)
        Me.btnNewSave.TabIndex = 15
        Me.btnNewSave.Text = "Save New Contact"
        Me.btnNewSave.UseVisualStyleBackColor = True
        '
        'mtbNewFaxNumber
        '
        Me.mtbNewFaxNumber.Location = New System.Drawing.Point(488, 59)
        Me.mtbNewFaxNumber.Mask = "(999) 000-0000"
        Me.mtbNewFaxNumber.Name = "mtbNewFaxNumber"
        Me.mtbNewFaxNumber.Size = New System.Drawing.Size(92, 20)
        Me.mtbNewFaxNumber.TabIndex = 12
        Me.mtbNewFaxNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbNewPhoneNumber2
        '
        Me.mtbNewPhoneNumber2.Location = New System.Drawing.Point(488, 35)
        Me.mtbNewPhoneNumber2.Mask = "(999) 000-0000"
        Me.mtbNewPhoneNumber2.Name = "mtbNewPhoneNumber2"
        Me.mtbNewPhoneNumber2.Size = New System.Drawing.Size(92, 20)
        Me.mtbNewPhoneNumber2.TabIndex = 11
        Me.mtbNewPhoneNumber2.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbNewZipCode
        '
        Me.mtbNewZipCode.Location = New System.Drawing.Point(298, 180)
        Me.mtbNewZipCode.Mask = "00000-9999"
        Me.mtbNewZipCode.Name = "mtbNewZipCode"
        Me.mtbNewZipCode.Size = New System.Drawing.Size(69, 20)
        Me.mtbNewZipCode.TabIndex = 9
        Me.mtbNewZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbNewPhoneNumber
        '
        Me.mtbNewPhoneNumber.Location = New System.Drawing.Point(488, 11)
        Me.mtbNewPhoneNumber.Mask = "(999) 000-0000"
        Me.mtbNewPhoneNumber.Name = "mtbNewPhoneNumber"
        Me.mtbNewPhoneNumber.Size = New System.Drawing.Size(92, 20)
        Me.mtbNewPhoneNumber.TabIndex = 10
        Me.mtbNewPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(31, 183)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 13)
        Me.Label9.TabIndex = 230
        Me.Label9.Text = "City/State/Zip"
        '
        'txtNewCity
        '
        Me.txtNewCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCity.Location = New System.Drawing.Point(111, 180)
        Me.txtNewCity.Name = "txtNewCity"
        Me.txtNewCity.Size = New System.Drawing.Size(145, 20)
        Me.txtNewCity.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(389, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(93, 13)
        Me.Label11.TabIndex = 225
        Me.Label11.Text = "Phone Number 1: "
        '
        'txtNewState
        '
        Me.txtNewState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewState.Location = New System.Drawing.Point(262, 180)
        Me.txtNewState.MaxLength = 2
        Me.txtNewState.Name = "txtNewState"
        Me.txtNewState.Size = New System.Drawing.Size(28, 20)
        Me.txtNewState.TabIndex = 8
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(389, 37)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(93, 13)
        Me.Label12.TabIndex = 226
        Me.Label12.Text = "Phone Number 2: "
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(44, 61)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(61, 13)
        Me.Label13.TabIndex = 234
        Me.Label13.Text = "Last Name:"
        '
        'txtNewAddress
        '
        Me.txtNewAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewAddress.Location = New System.Drawing.Point(111, 157)
        Me.txtNewAddress.Name = "txtNewAddress"
        Me.txtNewAddress.Size = New System.Drawing.Size(256, 20)
        Me.txtNewAddress.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(69, 86)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(36, 13)
        Me.Label14.TabIndex = 235
        Me.Label14.Text = "Suffix:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(17, 160)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(88, 13)
        Me.Label15.TabIndex = 229
        Me.Label15.Text = "Contact Address:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(415, 61)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(67, 13)
        Me.Label16.TabIndex = 227
        Me.Label16.Text = "Fax Number:"
        '
        'txtNewFirstName
        '
        Me.txtNewFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewFirstName.Location = New System.Drawing.Point(111, 35)
        Me.txtNewFirstName.Name = "txtNewFirstName"
        Me.txtNewFirstName.Size = New System.Drawing.Size(176, 20)
        Me.txtNewFirstName.TabIndex = 1
        '
        'txtNewCompany
        '
        Me.txtNewCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCompany.Location = New System.Drawing.Point(111, 131)
        Me.txtNewCompany.Name = "txtNewCompany"
        Me.txtNewCompany.Size = New System.Drawing.Size(256, 20)
        Me.txtNewCompany.TabIndex = 5
        '
        'txtNewEmail
        '
        Me.txtNewEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewEmail.Location = New System.Drawing.Point(488, 83)
        Me.txtNewEmail.Name = "txtNewEmail"
        Me.txtNewEmail.Size = New System.Drawing.Size(161, 20)
        Me.txtNewEmail.TabIndex = 13
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(447, 86)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(35, 13)
        Me.Label18.TabIndex = 228
        Me.Label18.Text = "Email:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(11, 134)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(94, 13)
        Me.Label19.TabIndex = 224
        Me.Label19.Text = "Contact Company:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(379, 112)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(103, 13)
        Me.Label20.TabIndex = 231
        Me.Label20.Text = "Contact Description:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(53, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(52, 13)
        Me.Label21.TabIndex = 232
        Me.Label21.Text = "Honorific:"
        '
        'txtNewDescrption
        '
        Me.txtNewDescrption.AcceptsReturn = True
        Me.txtNewDescrption.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDescrption.Location = New System.Drawing.Point(488, 107)
        Me.txtNewDescrption.Multiline = True
        Me.txtNewDescrption.Name = "txtNewDescrption"
        Me.txtNewDescrption.Size = New System.Drawing.Size(161, 93)
        Me.txtNewDescrption.TabIndex = 14
        '
        'txtNewTitle
        '
        Me.txtNewTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewTitle.Location = New System.Drawing.Point(111, 107)
        Me.txtNewTitle.Name = "txtNewTitle"
        Me.txtNewTitle.Size = New System.Drawing.Size(256, 20)
        Me.txtNewTitle.TabIndex = 4
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(45, 38)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(60, 13)
        Me.Label22.TabIndex = 233
        Me.Label22.Text = "First Name:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(35, 112)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(70, 13)
        Me.Label23.TabIndex = 223
        Me.Label23.Text = "Contact Title:"
        '
        'txtNewSuffix
        '
        Me.txtNewSuffix.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSuffix.Location = New System.Drawing.Point(111, 83)
        Me.txtNewSuffix.Name = "txtNewSuffix"
        Me.txtNewSuffix.Size = New System.Drawing.Size(88, 20)
        Me.txtNewSuffix.TabIndex = 3
        '
        'txtNewPrefix
        '
        Me.txtNewPrefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPrefix.Location = New System.Drawing.Point(111, 11)
        Me.txtNewPrefix.Name = "txtNewPrefix"
        Me.txtNewPrefix.Size = New System.Drawing.Size(88, 20)
        Me.txtNewPrefix.TabIndex = 0
        '
        'txtNewLastName
        '
        Me.txtNewLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewLastName.Location = New System.Drawing.Point(111, 59)
        Me.txtNewLastName.Name = "txtNewLastName"
        Me.txtNewLastName.Size = New System.Drawing.Size(176, 20)
        Me.txtNewLastName.TabIndex = 2
        '
        'pnlHeaderPanel
        '
        Me.pnlHeaderPanel.Controls.Add(Me.lblAirsNumber)
        Me.pnlHeaderPanel.Controls.Add(Me.lblFacilityName)
        Me.pnlHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeaderPanel.Name = "pnlHeaderPanel"
        Me.pnlHeaderPanel.Size = New System.Drawing.Size(799, 33)
        Me.pnlHeaderPanel.TabIndex = 247
        '
        'lblAirsNumber
        '
        Me.lblAirsNumber.AutoSize = True
        Me.lblAirsNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAirsNumber.Location = New System.Drawing.Point(12, 9)
        Me.lblAirsNumber.Name = "lblAirsNumber"
        Me.lblAirsNumber.Size = New System.Drawing.Size(83, 13)
        Me.lblAirsNumber.TabIndex = 243
        Me.lblAirsNumber.Text = "AIRS Number"
        '
        'lblFacilityName
        '
        Me.lblFacilityName.AutoSize = True
        Me.lblFacilityName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityName.Location = New System.Drawing.Point(108, 9)
        Me.lblFacilityName.Name = "lblFacilityName"
        Me.lblFacilityName.Size = New System.Drawing.Size(83, 13)
        Me.lblFacilityName.TabIndex = 0
        Me.lblFacilityName.Text = "Facility Name"
        '
        'IAIPEditContacts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 601)
        Me.Controls.Add(Me.ContactsDataGrid)
        Me.Controls.Add(Me.pnlInfoPanel)
        Me.Controls.Add(Me.pnlHeaderPanel)
        Me.Name = "IAIPEditContacts"
        Me.Text = "Add/Edit Contacts"
        CType(Me.ContactsDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInfoPanel.ResumeLayout(False)
        Me.pnlInfoPanel.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.pnlHeaderPanel.ResumeLayout(False)
        Me.pnlHeaderPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ContactsDataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents pnlInfoPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rdbNewDistrictContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewPlanningContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewAmbientContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewESContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewPermittingContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewEISContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewFeeContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewComplianceContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewMonitoringContact As System.Windows.Forms.RadioButton
    Friend WithEvents btnNewClear As System.Windows.Forms.Button
    Friend WithEvents btnNewUpdate As System.Windows.Forms.Button
    Friend WithEvents btnNewSave As System.Windows.Forms.Button
    Friend WithEvents mtbNewFaxNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbNewPhoneNumber2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbNewZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbNewPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNewCity As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtNewState As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtNewAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtNewFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCompany As System.Windows.Forms.TextBox
    Friend WithEvents txtNewEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtNewDescrption As System.Windows.Forms.TextBox
    Friend WithEvents txtNewTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtNewSuffix As System.Windows.Forms.TextBox
    Friend WithEvents txtNewPrefix As System.Windows.Forms.TextBox
    Friend WithEvents txtNewLastName As System.Windows.Forms.TextBox
    Friend WithEvents pnlHeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblAirsNumber As System.Windows.Forms.Label
    Friend WithEvents lblFacilityName As System.Windows.Forms.Label
End Class
