<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPEditAirProgramPollutants
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPEditAirProgramPollutants))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiSave = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiBack = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiCut = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiPaste = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.TSEditAirPrograms = New System.Windows.Forms.ToolStrip
        Me.tsbSave = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.cboComplianceStatus = New System.Windows.Forms.ComboBox
        Me.lblComplianceStatus = New System.Windows.Forms.Label
        Me.cboAirProgramCodes = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboPollutants = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtModifingDate = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtModifingPerson = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.labReferenceNumber = New System.Windows.Forms.Label
        Me.txtAirsNumber = New System.Windows.Forms.TextBox
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.GBEnforcementActions = New System.Windows.Forms.GroupBox
        Me.btnRemovePollutantsfromList = New System.Windows.Forms.Button
        Me.btnAddPollutants = New System.Windows.Forms.Button
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox
        Me.lblEnforcement = New System.Windows.Forms.Label
        Me.clbEnforcementPollutants = New System.Windows.Forms.CheckedListBox
        Me.dgvAirProgramPollutants = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSaveNewPollutant = New System.Windows.Forms.Button
        Me.TCEditPollutants = New System.Windows.Forms.TabControl
        Me.TPEditPollutants = New System.Windows.Forms.TabPage
        Me.TPEnforcementPollutants = New System.Windows.Forms.TabPage
        Me.PanelPollutants = New System.Windows.Forms.Panel
        Me.MenuStrip1.SuspendLayout()
        Me.TSEditAirPrograms.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GBEnforcementActions.SuspendLayout()
        CType(Me.dgvAirProgramPollutants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TCEditPollutants.SuspendLayout()
        Me.TPEditPollutants.SuspendLayout()
        Me.TPEnforcementPollutants.SuspendLayout()
        Me.PanelPollutants.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.mmiHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(742, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiSave, Me.mmiBack})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'mmiSave
        '
        Me.mmiSave.Name = "mmiSave"
        Me.mmiSave.Size = New System.Drawing.Size(109, 22)
        Me.mmiSave.Text = "Save"
        '
        'mmiBack
        '
        Me.mmiBack.Name = "mmiBack"
        Me.mmiBack.Size = New System.Drawing.Size(109, 22)
        Me.mmiBack.Text = "Back"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'mmiCut
        '
        Me.mmiCut.Name = "mmiCut"
        Me.mmiCut.Size = New System.Drawing.Size(112, 22)
        Me.mmiCut.Text = "Cut"
        '
        'mmiCopy
        '
        Me.mmiCopy.Name = "mmiCopy"
        Me.mmiCopy.Size = New System.Drawing.Size(112, 22)
        Me.mmiCopy.Text = "Copy"
        '
        'mmiPaste
        '
        Me.mmiPaste.Name = "mmiPaste"
        Me.mmiPaste.Size = New System.Drawing.Size(112, 22)
        Me.mmiPaste.Text = "Paste"
        '
        'mmiHelp
        '
        Me.mmiHelp.Name = "mmiHelp"
        Me.mmiHelp.Size = New System.Drawing.Size(40, 20)
        Me.mmiHelp.Text = "Help"
        '
        'TSEditAirPrograms
        '
        Me.TSEditAirPrograms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbBack})
        Me.TSEditAirPrograms.Location = New System.Drawing.Point(0, 24)
        Me.TSEditAirPrograms.Name = "TSEditAirPrograms"
        Me.TSEditAirPrograms.Size = New System.Drawing.Size(742, 25)
        Me.TSEditAirPrograms.TabIndex = 1
        Me.TSEditAirPrograms.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), System.Drawing.Image)
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(23, 22)
        '
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = CType(resources.GetObject("tsbBack.Image"), System.Drawing.Image)
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 563)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(742, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(719, 17)
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
        'cboComplianceStatus
        '
        Me.cboComplianceStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboComplianceStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboComplianceStatus.Location = New System.Drawing.Point(498, 63)
        Me.cboComplianceStatus.Name = "cboComplianceStatus"
        Me.cboComplianceStatus.Size = New System.Drawing.Size(216, 21)
        Me.cboComplianceStatus.TabIndex = 237
        '
        'lblComplianceStatus
        '
        Me.lblComplianceStatus.AutoSize = True
        Me.lblComplianceStatus.Location = New System.Drawing.Point(486, 46)
        Me.lblComplianceStatus.Name = "lblComplianceStatus"
        Me.lblComplianceStatus.Size = New System.Drawing.Size(95, 13)
        Me.lblComplianceStatus.TabIndex = 236
        Me.lblComplianceStatus.Text = "Compliance Status"
        '
        'cboAirProgramCodes
        '
        Me.cboAirProgramCodes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAirProgramCodes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirProgramCodes.Location = New System.Drawing.Point(18, 63)
        Me.cboAirProgramCodes.Name = "cboAirProgramCodes"
        Me.cboAirProgramCodes.Size = New System.Drawing.Size(136, 21)
        Me.cboAirProgramCodes.TabIndex = 229
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(170, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 232
        Me.Label3.Text = "Select a Pollutant"
        '
        'cboPollutants
        '
        Me.cboPollutants.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollutants.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollutants.Location = New System.Drawing.Point(176, 63)
        Me.cboPollutants.Name = "cboPollutants"
        Me.cboPollutants.Size = New System.Drawing.Size(295, 21)
        Me.cboPollutants.TabIndex = 231
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 13)
        Me.Label1.TabIndex = 230
        Me.Label1.Text = "Select an Air Program Code"
        '
        'txtModifingDate
        '
        Me.txtModifingDate.Location = New System.Drawing.Point(257, 156)
        Me.txtModifingDate.Name = "txtModifingDate"
        Me.txtModifingDate.ReadOnly = True
        Me.txtModifingDate.Size = New System.Drawing.Size(120, 20)
        Me.txtModifingDate.TabIndex = 235
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 160)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 13)
        Me.Label4.TabIndex = 234
        Me.Label4.Text = "Last Modified By/Date:"
        '
        'txtModifingPerson
        '
        Me.txtModifingPerson.Location = New System.Drawing.Point(131, 156)
        Me.txtModifingPerson.Name = "txtModifingPerson"
        Me.txtModifingPerson.ReadOnly = True
        Me.txtModifingPerson.Size = New System.Drawing.Size(120, 20)
        Me.txtModifingPerson.TabIndex = 233
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(381, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 226
        Me.Label2.Text = "AIRS Number:"
        '
        'labReferenceNumber
        '
        Me.labReferenceNumber.AutoSize = True
        Me.labReferenceNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labReferenceNumber.Location = New System.Drawing.Point(9, 19)
        Me.labReferenceNumber.Name = "labReferenceNumber"
        Me.labReferenceNumber.Size = New System.Drawing.Size(73, 13)
        Me.labReferenceNumber.TabIndex = 225
        Me.labReferenceNumber.Text = "Facility Name:"
        Me.labReferenceNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAirsNumber
        '
        Me.txtAirsNumber.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtAirsNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAirsNumber.Location = New System.Drawing.Point(460, 15)
        Me.txtAirsNumber.MaxLength = 8
        Me.txtAirsNumber.Name = "txtAirsNumber"
        Me.txtAirsNumber.ReadOnly = True
        Me.txtAirsNumber.Size = New System.Drawing.Size(72, 20)
        Me.txtAirsNumber.TabIndex = 228
        '
        'txtFacilityName
        '
        Me.txtFacilityName.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtFacilityName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityName.Location = New System.Drawing.Point(89, 15)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(272, 20)
        Me.txtFacilityName.TabIndex = 227
        '
        'GBEnforcementActions
        '
        Me.GBEnforcementActions.Controls.Add(Me.btnRemovePollutantsfromList)
        Me.GBEnforcementActions.Controls.Add(Me.btnAddPollutants)
        Me.GBEnforcementActions.Controls.Add(Me.txtEnforcementNumber)
        Me.GBEnforcementActions.Controls.Add(Me.lblEnforcement)
        Me.GBEnforcementActions.Controls.Add(Me.clbEnforcementPollutants)
        Me.GBEnforcementActions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GBEnforcementActions.Location = New System.Drawing.Point(3, 3)
        Me.GBEnforcementActions.Name = "GBEnforcementActions"
        Me.GBEnforcementActions.Size = New System.Drawing.Size(728, 201)
        Me.GBEnforcementActions.TabIndex = 5
        Me.GBEnforcementActions.TabStop = False
        Me.GBEnforcementActions.Text = "Enforcement Use Only"
        '
        'btnRemovePollutantsfromList
        '
        Me.btnRemovePollutantsfromList.AutoSize = True
        Me.btnRemovePollutantsfromList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRemovePollutantsfromList.Location = New System.Drawing.Point(14, 172)
        Me.btnRemovePollutantsfromList.Name = "btnRemovePollutantsfromList"
        Me.btnRemovePollutantsfromList.Size = New System.Drawing.Size(259, 23)
        Me.btnRemovePollutantsfromList.TabIndex = 235
        Me.btnRemovePollutantsfromList.Text = "Remove Unchecked Pollutants from the List Above"
        Me.btnRemovePollutantsfromList.UseVisualStyleBackColor = True
        '
        'btnAddPollutants
        '
        Me.btnAddPollutants.AutoSize = True
        Me.btnAddPollutants.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddPollutants.Location = New System.Drawing.Point(308, 172)
        Me.btnAddPollutants.Name = "btnAddPollutants"
        Me.btnAddPollutants.Size = New System.Drawing.Size(164, 23)
        Me.btnAddPollutants.TabIndex = 233
        Me.btnAddPollutants.Text = "Update Enforcement Pollutants"
        Me.btnAddPollutants.UseVisualStyleBackColor = True
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(3, 16)
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.ReadOnly = True
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(10, 20)
        Me.txtEnforcementNumber.TabIndex = 232
        Me.txtEnforcementNumber.Visible = False
        '
        'lblEnforcement
        '
        Me.lblEnforcement.AutoSize = True
        Me.lblEnforcement.Location = New System.Drawing.Point(11, 19)
        Me.lblEnforcement.Name = "lblEnforcement"
        Me.lblEnforcement.Size = New System.Drawing.Size(77, 13)
        Me.lblEnforcement.TabIndex = 231
        Me.lblEnforcement.Text = "Enforcement #"
        '
        'clbEnforcementPollutants
        '
        Me.clbEnforcementPollutants.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbEnforcementPollutants.CheckOnClick = True
        Me.clbEnforcementPollutants.FormattingEnabled = True
        Me.clbEnforcementPollutants.Location = New System.Drawing.Point(5, 35)
        Me.clbEnforcementPollutants.Name = "clbEnforcementPollutants"
        Me.clbEnforcementPollutants.Size = New System.Drawing.Size(717, 124)
        Me.clbEnforcementPollutants.TabIndex = 0
        '
        'dgvAirProgramPollutants
        '
        Me.dgvAirProgramPollutants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAirProgramPollutants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAirProgramPollutants.Location = New System.Drawing.Point(0, 0)
        Me.dgvAirProgramPollutants.Name = "dgvAirProgramPollutants"
        Me.dgvAirProgramPollutants.ReadOnly = True
        Me.dgvAirProgramPollutants.Size = New System.Drawing.Size(742, 281)
        Me.dgvAirProgramPollutants.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSaveNewPollutant)
        Me.GroupBox1.Controls.Add(Me.cboComplianceStatus)
        Me.GroupBox1.Controls.Add(Me.labReferenceNumber)
        Me.GroupBox1.Controls.Add(Me.lblComplianceStatus)
        Me.GroupBox1.Controls.Add(Me.txtFacilityName)
        Me.GroupBox1.Controls.Add(Me.cboAirProgramCodes)
        Me.GroupBox1.Controls.Add(Me.txtAirsNumber)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cboPollutants)
        Me.GroupBox1.Controls.Add(Me.txtModifingPerson)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtModifingDate)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(728, 201)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'btnSaveNewPollutant
        '
        Me.btnSaveNewPollutant.AutoSize = True
        Me.btnSaveNewPollutant.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveNewPollutant.Location = New System.Drawing.Point(9, 107)
        Me.btnSaveNewPollutant.Name = "btnSaveNewPollutant"
        Me.btnSaveNewPollutant.Size = New System.Drawing.Size(129, 23)
        Me.btnSaveNewPollutant.TabIndex = 238
        Me.btnSaveNewPollutant.Text = "Add/Edit Pollutant Data"
        Me.btnSaveNewPollutant.UseVisualStyleBackColor = True
        '
        'TCEditPollutants
        '
        Me.TCEditPollutants.Controls.Add(Me.TPEditPollutants)
        Me.TCEditPollutants.Controls.Add(Me.TPEnforcementPollutants)
        Me.TCEditPollutants.Dock = System.Windows.Forms.DockStyle.Top
        Me.TCEditPollutants.Location = New System.Drawing.Point(0, 49)
        Me.TCEditPollutants.Name = "TCEditPollutants"
        Me.TCEditPollutants.SelectedIndex = 0
        Me.TCEditPollutants.Size = New System.Drawing.Size(742, 233)
        Me.TCEditPollutants.TabIndex = 7
        '
        'TPEditPollutants
        '
        Me.TPEditPollutants.Controls.Add(Me.GroupBox1)
        Me.TPEditPollutants.Location = New System.Drawing.Point(4, 22)
        Me.TPEditPollutants.Name = "TPEditPollutants"
        Me.TPEditPollutants.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEditPollutants.Size = New System.Drawing.Size(734, 207)
        Me.TPEditPollutants.TabIndex = 0
        Me.TPEditPollutants.Text = "Add/Edit Facility Pollutants"
        Me.TPEditPollutants.UseVisualStyleBackColor = True
        '
        'TPEnforcementPollutants
        '
        Me.TPEnforcementPollutants.Controls.Add(Me.GBEnforcementActions)
        Me.TPEnforcementPollutants.Location = New System.Drawing.Point(4, 22)
        Me.TPEnforcementPollutants.Name = "TPEnforcementPollutants"
        Me.TPEnforcementPollutants.Padding = New System.Windows.Forms.Padding(3)
        Me.TPEnforcementPollutants.Size = New System.Drawing.Size(734, 207)
        Me.TPEnforcementPollutants.TabIndex = 1
        Me.TPEnforcementPollutants.Text = "Add/Remove Enforcement Pollutants"
        Me.TPEnforcementPollutants.UseVisualStyleBackColor = True
        '
        'PanelPollutants
        '
        Me.PanelPollutants.Controls.Add(Me.dgvAirProgramPollutants)
        Me.PanelPollutants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelPollutants.Location = New System.Drawing.Point(0, 282)
        Me.PanelPollutants.Name = "PanelPollutants"
        Me.PanelPollutants.Size = New System.Drawing.Size(742, 281)
        Me.PanelPollutants.TabIndex = 8
        '
        'IAIPEditAirProgramPollutants
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(742, 585)
        Me.Controls.Add(Me.PanelPollutants)
        Me.Controls.Add(Me.TCEditPollutants)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TSEditAirPrograms)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "IAIPEditAirProgramPollutants"
        Me.Text = "Edit Air Program Pollutants"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TSEditAirPrograms.ResumeLayout(False)
        Me.TSEditAirPrograms.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GBEnforcementActions.ResumeLayout(False)
        Me.GBEnforcementActions.PerformLayout()
        CType(Me.dgvAirProgramPollutants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TCEditPollutants.ResumeLayout(False)
        Me.TPEditPollutants.ResumeLayout(False)
        Me.TPEnforcementPollutants.ResumeLayout(False)
        Me.PanelPollutants.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiBack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSEditAirPrograms As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cboComplianceStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblComplianceStatus As System.Windows.Forms.Label
    Friend WithEvents cboAirProgramCodes As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboPollutants As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtModifingDate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtModifingPerson As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents labReferenceNumber As System.Windows.Forms.Label
    Friend WithEvents txtAirsNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents GBEnforcementActions As System.Windows.Forms.GroupBox
    Friend WithEvents dgvAirProgramPollutants As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblEnforcement As System.Windows.Forms.Label
    Friend WithEvents clbEnforcementPollutants As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnAddPollutants As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnSaveNewPollutant As System.Windows.Forms.Button
    Friend WithEvents TCEditPollutants As System.Windows.Forms.TabControl
    Friend WithEvents TPEditPollutants As System.Windows.Forms.TabPage
    Friend WithEvents TPEnforcementPollutants As System.Windows.Forms.TabPage
    Friend WithEvents PanelPollutants As System.Windows.Forms.Panel
    Friend WithEvents btnRemovePollutantsfromList As System.Windows.Forms.Button
End Class
