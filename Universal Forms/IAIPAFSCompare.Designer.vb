<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPAFSCompare
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPAFSCompare))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.bgwTransfer = New System.ComponentModel.BackgroundWorker
        Me.TSAFSCompare = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MmiFile = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MmiView = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.PanelAFSTop = New System.Windows.Forms.Panel
        Me.GBCompare = New System.Windows.Forms.GroupBox
        Me.pnlCompareFull = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox
        Me.rdbAIRSNumber = New System.Windows.Forms.RadioButton
        Me.cboCounty = New System.Windows.Forms.ComboBox
        Me.rdbCounty = New System.Windows.Forms.RadioButton
        Me.rdbCompareFullIAIPdata = New System.Windows.Forms.RadioButton
        Me.rdbViewAFSDataOnly = New System.Windows.Forms.RadioButton
        Me.rdbCompareAFSDataOnly = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioButton9 = New System.Windows.Forms.RadioButton
        Me.rdbPollutants = New System.Windows.Forms.RadioButton
        Me.rdbSubPart = New System.Windows.Forms.RadioButton
        Me.rdbAirProgramCode = New System.Windows.Forms.RadioButton
        Me.rdbContact = New System.Windows.Forms.RadioButton
        Me.rdbSIC = New System.Windows.Forms.RadioButton
        Me.rdbCityZipCode = New System.Windows.Forms.RadioButton
        Me.rdbAddress = New System.Windows.Forms.RadioButton
        Me.rdbFacilityName = New System.Windows.Forms.RadioButton
        Me.txtAFSDataCount = New System.Windows.Forms.TextBox
        Me.Label176 = New System.Windows.Forms.Label
        Me.btnExportAFSData = New System.Windows.Forms.Button
        Me.GBAFSCompare = New System.Windows.Forms.GroupBox
        Me.chb181Card = New System.Windows.Forms.CheckBox
        Me.chb164Card = New System.Windows.Forms.CheckBox
        Me.chb163Card = New System.Windows.Forms.CheckBox
        Me.chb161Card = New System.Windows.Forms.CheckBox
        Me.chb131Card = New System.Windows.Forms.CheckBox
        Me.chb122Card = New System.Windows.Forms.CheckBox
        Me.chb121Card = New System.Windows.Forms.CheckBox
        Me.chb105Card = New System.Windows.Forms.CheckBox
        Me.chb103Card = New System.Windows.Forms.CheckBox
        Me.chb102Card = New System.Windows.Forms.CheckBox
        Me.chb101Card = New System.Windows.Forms.CheckBox
        Me.btnLoadAFSData = New System.Windows.Forms.Button
        Me.Label175 = New System.Windows.Forms.Label
        Me.btnSearchForAFSFile = New System.Windows.Forms.Button
        Me.txtAFSFile = New System.Windows.Forms.TextBox
        Me.dgvAFSData = New System.Windows.Forms.DataGridView
        Me.StatusStrip1.SuspendLayout()
        Me.TSAFSCompare.SuspendLayout()
        Me.PanelAFSTop.SuspendLayout()
        Me.GBCompare.SuspendLayout()
        Me.pnlCompareFull.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBAFSCompare.SuspendLayout()
        CType(Me.dgvAFSData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 513)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(772, 22)
        Me.StatusStrip1.TabIndex = 259
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(749, 17)
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
        'TSAFSCompare
        '
        Me.TSAFSCompare.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack})
        Me.TSAFSCompare.Location = New System.Drawing.Point(0, 0)
        Me.TSAFSCompare.Name = "TSAFSCompare"
        Me.TSAFSCompare.Size = New System.Drawing.Size(772, 25)
        Me.TSAFSCompare.TabIndex = 260
        Me.TSAFSCompare.Text = "ToolStrip1"
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
        'PanelAFSTop
        '
        Me.PanelAFSTop.Controls.Add(Me.GBCompare)
        Me.PanelAFSTop.Controls.Add(Me.GroupBox1)
        Me.PanelAFSTop.Controls.Add(Me.txtAFSDataCount)
        Me.PanelAFSTop.Controls.Add(Me.Label176)
        Me.PanelAFSTop.Controls.Add(Me.btnExportAFSData)
        Me.PanelAFSTop.Controls.Add(Me.GBAFSCompare)
        Me.PanelAFSTop.Controls.Add(Me.btnLoadAFSData)
        Me.PanelAFSTop.Controls.Add(Me.Label175)
        Me.PanelAFSTop.Controls.Add(Me.btnSearchForAFSFile)
        Me.PanelAFSTop.Controls.Add(Me.txtAFSFile)
        Me.PanelAFSTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelAFSTop.Location = New System.Drawing.Point(0, 25)
        Me.PanelAFSTop.Name = "PanelAFSTop"
        Me.PanelAFSTop.Size = New System.Drawing.Size(772, 298)
        Me.PanelAFSTop.TabIndex = 261
        '
        'GBCompare
        '
        Me.GBCompare.AutoSize = True
        Me.GBCompare.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GBCompare.Controls.Add(Me.pnlCompareFull)
        Me.GBCompare.Controls.Add(Me.rdbCompareFullIAIPdata)
        Me.GBCompare.Controls.Add(Me.rdbViewAFSDataOnly)
        Me.GBCompare.Controls.Add(Me.rdbCompareAFSDataOnly)
        Me.GBCompare.Location = New System.Drawing.Point(117, 78)
        Me.GBCompare.Name = "GBCompare"
        Me.GBCompare.Size = New System.Drawing.Size(212, 167)
        Me.GBCompare.TabIndex = 13
        Me.GBCompare.TabStop = False
        Me.GBCompare.Text = "Comparison  Type"
        '
        'pnlCompareFull
        '
        Me.pnlCompareFull.AutoSize = True
        Me.pnlCompareFull.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlCompareFull.Controls.Add(Me.Label1)
        Me.pnlCompareFull.Controls.Add(Me.mtbAIRSNumber)
        Me.pnlCompareFull.Controls.Add(Me.rdbAIRSNumber)
        Me.pnlCompareFull.Controls.Add(Me.cboCounty)
        Me.pnlCompareFull.Controls.Add(Me.rdbCounty)
        Me.pnlCompareFull.Location = New System.Drawing.Point(6, 82)
        Me.pnlCompareFull.Name = "pnlCompareFull"
        Me.pnlCompareFull.Size = New System.Drawing.Size(186, 66)
        Me.pnlCompareFull.TabIndex = 6
        Me.pnlCompareFull.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "or"
        '
        'mtbAIRSNumber
        '
        Me.mtbAIRSNumber.Location = New System.Drawing.Point(110, 43)
        Me.mtbAIRSNumber.Mask = "000-00000"
        Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
        Me.mtbAIRSNumber.Size = New System.Drawing.Size(67, 20)
        Me.mtbAIRSNumber.TabIndex = 7
        Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbAIRSNumber.Visible = False
        '
        'rdbAIRSNumber
        '
        Me.rdbAIRSNumber.AutoSize = True
        Me.rdbAIRSNumber.Location = New System.Drawing.Point(14, 46)
        Me.rdbAIRSNumber.Name = "rdbAIRSNumber"
        Me.rdbAIRSNumber.Size = New System.Drawing.Size(90, 17)
        Me.rdbAIRSNumber.TabIndex = 6
        Me.rdbAIRSNumber.TabStop = True
        Me.rdbAIRSNumber.Text = "AIRS Number"
        Me.rdbAIRSNumber.UseVisualStyleBackColor = True
        Me.rdbAIRSNumber.Visible = False
        '
        'cboCounty
        '
        Me.cboCounty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCounty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCounty.FormattingEnabled = True
        Me.cboCounty.Location = New System.Drawing.Point(73, 6)
        Me.cboCounty.Name = "cboCounty"
        Me.cboCounty.Size = New System.Drawing.Size(110, 21)
        Me.cboCounty.TabIndex = 4
        Me.cboCounty.Visible = False
        '
        'rdbCounty
        '
        Me.rdbCounty.AutoSize = True
        Me.rdbCounty.Location = New System.Drawing.Point(14, 7)
        Me.rdbCounty.Name = "rdbCounty"
        Me.rdbCounty.Size = New System.Drawing.Size(58, 17)
        Me.rdbCounty.TabIndex = 5
        Me.rdbCounty.TabStop = True
        Me.rdbCounty.Text = "County"
        Me.rdbCounty.UseVisualStyleBackColor = True
        Me.rdbCounty.Visible = False
        '
        'rdbCompareFullIAIPdata
        '
        Me.rdbCompareFullIAIPdata.AutoSize = True
        Me.rdbCompareFullIAIPdata.Location = New System.Drawing.Point(6, 56)
        Me.rdbCompareFullIAIPdata.Name = "rdbCompareFullIAIPdata"
        Me.rdbCompareFullIAIPdata.Size = New System.Drawing.Size(183, 17)
        Me.rdbCompareFullIAIPdata.TabIndex = 2
        Me.rdbCompareFullIAIPdata.Text = "Compare full IAIP data against file"
        Me.rdbCompareFullIAIPdata.UseVisualStyleBackColor = True
        '
        'rdbViewAFSDataOnly
        '
        Me.rdbViewAFSDataOnly.AutoSize = True
        Me.rdbViewAFSDataOnly.Checked = True
        Me.rdbViewAFSDataOnly.Location = New System.Drawing.Point(6, 19)
        Me.rdbViewAFSDataOnly.Name = "rdbViewAFSDataOnly"
        Me.rdbViewAFSDataOnly.Size = New System.Drawing.Size(121, 17)
        Me.rdbViewAFSDataOnly.TabIndex = 0
        Me.rdbViewAFSDataOnly.TabStop = True
        Me.rdbViewAFSDataOnly.Text = "View AFS Data Only"
        Me.rdbViewAFSDataOnly.UseVisualStyleBackColor = True
        '
        'rdbCompareAFSDataOnly
        '
        Me.rdbCompareAFSDataOnly.AutoSize = True
        Me.rdbCompareAFSDataOnly.Location = New System.Drawing.Point(6, 36)
        Me.rdbCompareAFSDataOnly.Name = "rdbCompareAFSDataOnly"
        Me.rdbCompareAFSDataOnly.Size = New System.Drawing.Size(200, 17)
        Me.rdbCompareAFSDataOnly.TabIndex = 1
        Me.rdbCompareAFSDataOnly.Text = "Compare AFS data against IAIP Data"
        Me.rdbCompareAFSDataOnly.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.AutoSize = True
        Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox1.Controls.Add(Me.RadioButton9)
        Me.GroupBox1.Controls.Add(Me.rdbPollutants)
        Me.GroupBox1.Controls.Add(Me.rdbSubPart)
        Me.GroupBox1.Controls.Add(Me.rdbAirProgramCode)
        Me.GroupBox1.Controls.Add(Me.rdbContact)
        Me.GroupBox1.Controls.Add(Me.rdbSIC)
        Me.GroupBox1.Controls.Add(Me.rdbCityZipCode)
        Me.GroupBox1.Controls.Add(Me.rdbAddress)
        Me.GroupBox1.Controls.Add(Me.rdbFacilityName)
        Me.GroupBox1.Location = New System.Drawing.Point(335, 84)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(182, 203)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Comparable Fields"
        '
        'RadioButton9
        '
        Me.RadioButton9.AutoSize = True
        Me.RadioButton9.Location = New System.Drawing.Point(8, 167)
        Me.RadioButton9.Name = "RadioButton9"
        Me.RadioButton9.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton9.TabIndex = 19
        Me.RadioButton9.TabStop = True
        Me.RadioButton9.Text = "RadioButton9"
        Me.RadioButton9.UseVisualStyleBackColor = True
        '
        'rdbPollutants
        '
        Me.rdbPollutants.AutoSize = True
        Me.rdbPollutants.Location = New System.Drawing.Point(8, 144)
        Me.rdbPollutants.Name = "rdbPollutants"
        Me.rdbPollutants.Size = New System.Drawing.Size(98, 17)
        Me.rdbPollutants.TabIndex = 18
        Me.rdbPollutants.TabStop = True
        Me.rdbPollutants.Text = "131 - Pollutants"
        Me.rdbPollutants.UseVisualStyleBackColor = True
        '
        'rdbSubPart
        '
        Me.rdbSubPart.AutoSize = True
        Me.rdbSubPart.Location = New System.Drawing.Point(8, 121)
        Me.rdbSubPart.Name = "rdbSubPart"
        Me.rdbSubPart.Size = New System.Drawing.Size(90, 17)
        Me.rdbSubPart.TabIndex = 17
        Me.rdbSubPart.TabStop = True
        Me.rdbSubPart.Text = "122 - SubPart"
        Me.rdbSubPart.UseVisualStyleBackColor = True
        '
        'rdbAirProgramCode
        '
        Me.rdbAirProgramCode.AutoSize = True
        Me.rdbAirProgramCode.Location = New System.Drawing.Point(6, 104)
        Me.rdbAirProgramCode.Name = "rdbAirProgramCode"
        Me.rdbAirProgramCode.Size = New System.Drawing.Size(170, 17)
        Me.rdbAirProgramCode.TabIndex = 16
        Me.rdbAirProgramCode.TabStop = True
        Me.rdbAirProgramCode.Text = "121 - Air Program Code, Status"
        Me.rdbAirProgramCode.UseVisualStyleBackColor = True
        '
        'rdbContact
        '
        Me.rdbContact.AutoSize = True
        Me.rdbContact.Location = New System.Drawing.Point(6, 86)
        Me.rdbContact.Name = "rdbContact"
        Me.rdbContact.Size = New System.Drawing.Size(89, 17)
        Me.rdbContact.TabIndex = 15
        Me.rdbContact.TabStop = True
        Me.rdbContact.Text = "105 - Contact"
        Me.rdbContact.UseVisualStyleBackColor = True
        '
        'rdbSIC
        '
        Me.rdbSIC.AutoSize = True
        Me.rdbSIC.Location = New System.Drawing.Point(6, 69)
        Me.rdbSIC.Name = "rdbSIC"
        Me.rdbSIC.Size = New System.Drawing.Size(69, 17)
        Me.rdbSIC.TabIndex = 14
        Me.rdbSIC.TabStop = True
        Me.rdbSIC.Text = "103 - SIC"
        Me.rdbSIC.UseVisualStyleBackColor = True
        '
        'rdbCityZipCode
        '
        Me.rdbCityZipCode.AutoSize = True
        Me.rdbCityZipCode.Location = New System.Drawing.Point(8, 49)
        Me.rdbCityZipCode.Name = "rdbCityZipCode"
        Me.rdbCityZipCode.Size = New System.Drawing.Size(118, 17)
        Me.rdbCityZipCode.TabIndex = 13
        Me.rdbCityZipCode.TabStop = True
        Me.rdbCityZipCode.Text = "103 - City, Zip Code"
        Me.rdbCityZipCode.UseVisualStyleBackColor = True
        '
        'rdbAddress
        '
        Me.rdbAddress.AutoSize = True
        Me.rdbAddress.Location = New System.Drawing.Point(7, 31)
        Me.rdbAddress.Name = "rdbAddress"
        Me.rdbAddress.Size = New System.Drawing.Size(90, 17)
        Me.rdbAddress.TabIndex = 12
        Me.rdbAddress.TabStop = True
        Me.rdbAddress.Text = "102 - Address"
        Me.rdbAddress.UseVisualStyleBackColor = True
        '
        'rdbFacilityName
        '
        Me.rdbFacilityName.AutoSize = True
        Me.rdbFacilityName.Location = New System.Drawing.Point(7, 13)
        Me.rdbFacilityName.Name = "rdbFacilityName"
        Me.rdbFacilityName.Size = New System.Drawing.Size(115, 17)
        Me.rdbFacilityName.TabIndex = 11
        Me.rdbFacilityName.TabStop = True
        Me.rdbFacilityName.Text = "101 - Facility Name"
        Me.rdbFacilityName.UseVisualStyleBackColor = True
        '
        'txtAFSDataCount
        '
        Me.txtAFSDataCount.Location = New System.Drawing.Point(660, 271)
        Me.txtAFSDataCount.Name = "txtAFSDataCount"
        Me.txtAFSDataCount.ReadOnly = True
        Me.txtAFSDataCount.Size = New System.Drawing.Size(100, 20)
        Me.txtAFSDataCount.TabIndex = 11
        '
        'Label176
        '
        Me.Label176.AutoSize = True
        Me.Label176.Location = New System.Drawing.Point(617, 274)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(38, 13)
        Me.Label176.TabIndex = 10
        Me.Label176.Text = "Count:"
        '
        'btnExportAFSData
        '
        Me.btnExportAFSData.AutoSize = True
        Me.btnExportAFSData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportAFSData.Location = New System.Drawing.Point(617, 10)
        Me.btnExportAFSData.Name = "btnExportAFSData"
        Me.btnExportAFSData.Size = New System.Drawing.Size(114, 23)
        Me.btnExportAFSData.TabIndex = 9
        Me.btnExportAFSData.Text = "Export Data to Excel"
        Me.btnExportAFSData.UseVisualStyleBackColor = True
        '
        'GBAFSCompare
        '
        Me.GBAFSCompare.Controls.Add(Me.chb181Card)
        Me.GBAFSCompare.Controls.Add(Me.chb164Card)
        Me.GBAFSCompare.Controls.Add(Me.chb163Card)
        Me.GBAFSCompare.Controls.Add(Me.chb161Card)
        Me.GBAFSCompare.Controls.Add(Me.chb131Card)
        Me.GBAFSCompare.Controls.Add(Me.chb122Card)
        Me.GBAFSCompare.Controls.Add(Me.chb121Card)
        Me.GBAFSCompare.Controls.Add(Me.chb105Card)
        Me.GBAFSCompare.Controls.Add(Me.chb103Card)
        Me.GBAFSCompare.Controls.Add(Me.chb102Card)
        Me.GBAFSCompare.Controls.Add(Me.chb101Card)
        Me.GBAFSCompare.Location = New System.Drawing.Point(12, 200)
        Me.GBAFSCompare.Name = "GBAFSCompare"
        Me.GBAFSCompare.Size = New System.Drawing.Size(56, 91)
        Me.GBAFSCompare.TabIndex = 4
        Me.GBAFSCompare.TabStop = False
        Me.GBAFSCompare.Text = "Compare AFS Cards"
        '
        'chb181Card
        '
        Me.chb181Card.AutoSize = True
        Me.chb181Card.Location = New System.Drawing.Point(6, 189)
        Me.chb181Card.Name = "chb181Card"
        Me.chb181Card.Size = New System.Drawing.Size(109, 17)
        Me.chb181Card.TabIndex = 15
        Me.chb181Card.Text = "181 - CMS Status"
        Me.chb181Card.UseVisualStyleBackColor = True
        '
        'chb164Card
        '
        Me.chb164Card.AutoSize = True
        Me.chb164Card.Location = New System.Drawing.Point(6, 172)
        Me.chb164Card.Name = "chb164Card"
        Me.chb164Card.Size = New System.Drawing.Size(145, 17)
        Me.chb164Card.TabIndex = 14
        Me.chb164Card.Text = "164 - HPV Violation Type"
        Me.chb164Card.UseVisualStyleBackColor = True
        '
        'chb163Card
        '
        Me.chb163Card.AutoSize = True
        Me.chb163Card.Location = New System.Drawing.Point(6, 155)
        Me.chb163Card.Name = "chb163Card"
        Me.chb163Card.Size = New System.Drawing.Size(199, 17)
        Me.chb163Card.TabIndex = 13
        Me.chb163Card.Text = "163 - Compliance Key Action Linking"
        Me.chb163Card.UseVisualStyleBackColor = True
        '
        'chb161Card
        '
        Me.chb161Card.AutoSize = True
        Me.chb161Card.Location = New System.Drawing.Point(6, 138)
        Me.chb161Card.Name = "chb161Card"
        Me.chb161Card.Size = New System.Drawing.Size(115, 17)
        Me.chb161Card.TabIndex = 12
        Me.chb161Card.Text = "161 - Action Types"
        Me.chb161Card.UseVisualStyleBackColor = True
        '
        'chb131Card
        '
        Me.chb131Card.AutoSize = True
        Me.chb131Card.Location = New System.Drawing.Point(6, 121)
        Me.chb131Card.Name = "chb131Card"
        Me.chb131Card.Size = New System.Drawing.Size(247, 17)
        Me.chb131Card.TabIndex = 11
        Me.chb131Card.Text = "131 - Pollutants, Class, Compliance, Attainment"
        Me.chb131Card.UseVisualStyleBackColor = True
        '
        'chb122Card
        '
        Me.chb122Card.AutoSize = True
        Me.chb122Card.Location = New System.Drawing.Point(6, 104)
        Me.chb122Card.Name = "chb122Card"
        Me.chb122Card.Size = New System.Drawing.Size(91, 17)
        Me.chb122Card.TabIndex = 10
        Me.chb122Card.Text = "122 - SubPart"
        Me.chb122Card.UseVisualStyleBackColor = True
        '
        'chb121Card
        '
        Me.chb121Card.AutoSize = True
        Me.chb121Card.Location = New System.Drawing.Point(6, 87)
        Me.chb121Card.Name = "chb121Card"
        Me.chb121Card.Size = New System.Drawing.Size(171, 17)
        Me.chb121Card.TabIndex = 9
        Me.chb121Card.Text = "121 - Air Program Code, Status"
        Me.chb121Card.UseVisualStyleBackColor = True
        '
        'chb105Card
        '
        Me.chb105Card.AutoSize = True
        Me.chb105Card.Location = New System.Drawing.Point(6, 70)
        Me.chb105Card.Name = "chb105Card"
        Me.chb105Card.Size = New System.Drawing.Size(90, 17)
        Me.chb105Card.TabIndex = 8
        Me.chb105Card.Text = "105 - Contact"
        Me.chb105Card.UseVisualStyleBackColor = True
        '
        'chb103Card
        '
        Me.chb103Card.AutoSize = True
        Me.chb103Card.Location = New System.Drawing.Point(6, 53)
        Me.chb103Card.Name = "chb103Card"
        Me.chb103Card.Size = New System.Drawing.Size(142, 17)
        Me.chb103Card.TabIndex = 7
        Me.chb103Card.Text = "103 - City, Zip Code, SIC"
        Me.chb103Card.UseVisualStyleBackColor = True
        '
        'chb102Card
        '
        Me.chb102Card.AutoSize = True
        Me.chb102Card.Location = New System.Drawing.Point(6, 36)
        Me.chb102Card.Name = "chb102Card"
        Me.chb102Card.Size = New System.Drawing.Size(91, 17)
        Me.chb102Card.TabIndex = 6
        Me.chb102Card.Text = "102 - Address"
        Me.chb102Card.UseVisualStyleBackColor = True
        '
        'chb101Card
        '
        Me.chb101Card.AutoSize = True
        Me.chb101Card.Location = New System.Drawing.Point(6, 19)
        Me.chb101Card.Name = "chb101Card"
        Me.chb101Card.Size = New System.Drawing.Size(116, 17)
        Me.chb101Card.TabIndex = 5
        Me.chb101Card.Text = "101 - Faciltiy Name"
        Me.chb101Card.UseVisualStyleBackColor = True
        '
        'btnLoadAFSData
        '
        Me.btnLoadAFSData.AutoSize = True
        Me.btnLoadAFSData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadAFSData.Location = New System.Drawing.Point(117, 37)
        Me.btnLoadAFSData.Name = "btnLoadAFSData"
        Me.btnLoadAFSData.Size = New System.Drawing.Size(90, 23)
        Me.btnLoadAFSData.TabIndex = 3
        Me.btnLoadAFSData.Text = "Load AFS Data"
        Me.btnLoadAFSData.UseVisualStyleBackColor = True
        '
        'Label175
        '
        Me.Label175.AutoSize = True
        Me.Label175.Location = New System.Drawing.Point(21, 15)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(90, 13)
        Me.Label175.TabIndex = 2
        Me.Label175.Text = "AFS File Location"
        '
        'btnSearchForAFSFile
        '
        Me.btnSearchForAFSFile.AutoSize = True
        Me.btnSearchForAFSFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchForAFSFile.Location = New System.Drawing.Point(443, 9)
        Me.btnSearchForAFSFile.Name = "btnSearchForAFSFile"
        Me.btnSearchForAFSFile.Size = New System.Drawing.Size(85, 23)
        Me.btnSearchForAFSFile.TabIndex = 1
        Me.btnSearchForAFSFile.Text = "Search for File"
        Me.btnSearchForAFSFile.UseVisualStyleBackColor = True
        '
        'txtAFSFile
        '
        Me.txtAFSFile.Location = New System.Drawing.Point(117, 11)
        Me.txtAFSFile.Name = "txtAFSFile"
        Me.txtAFSFile.Size = New System.Drawing.Size(320, 20)
        Me.txtAFSFile.TabIndex = 0
        '
        'dgvAFSData
        '
        Me.dgvAFSData.AllowDrop = True
        Me.dgvAFSData.AllowUserToAddRows = False
        Me.dgvAFSData.AllowUserToDeleteRows = False
        Me.dgvAFSData.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvAFSData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvAFSData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAFSData.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvAFSData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAFSData.Location = New System.Drawing.Point(0, 323)
        Me.dgvAFSData.Name = "dgvAFSData"
        Me.dgvAFSData.ReadOnly = True
        Me.dgvAFSData.Size = New System.Drawing.Size(772, 190)
        Me.dgvAFSData.TabIndex = 262
        '
        'IAIPAFSCompare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 535)
        Me.Controls.Add(Me.dgvAFSData)
        Me.Controls.Add(Me.PanelAFSTop)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TSAFSCompare)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPAFSCompare"
        Me.Text = "IAIP AFS Compare"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TSAFSCompare.ResumeLayout(False)
        Me.TSAFSCompare.PerformLayout()
        Me.PanelAFSTop.ResumeLayout(False)
        Me.PanelAFSTop.PerformLayout()
        Me.GBCompare.ResumeLayout(False)
        Me.GBCompare.PerformLayout()
        Me.pnlCompareFull.ResumeLayout(False)
        Me.pnlCompareFull.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBAFSCompare.ResumeLayout(False)
        Me.GBAFSCompare.PerformLayout()
        CType(Me.dgvAFSData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents bgwTransfer As System.ComponentModel.BackgroundWorker
    Friend WithEvents TSAFSCompare As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiView As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents PanelAFSTop As System.Windows.Forms.Panel
    Friend WithEvents txtAFSDataCount As System.Windows.Forms.TextBox
    Friend WithEvents Label176 As System.Windows.Forms.Label
    Friend WithEvents btnExportAFSData As System.Windows.Forms.Button
    Friend WithEvents rdbCompareFullIAIPdata As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCompareAFSDataOnly As System.Windows.Forms.RadioButton
    Friend WithEvents rdbViewAFSDataOnly As System.Windows.Forms.RadioButton
    Friend WithEvents GBAFSCompare As System.Windows.Forms.GroupBox
    Friend WithEvents chb181Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb164Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb163Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb161Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb131Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb122Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb121Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb105Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb103Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb102Card As System.Windows.Forms.CheckBox
    Friend WithEvents chb101Card As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoadAFSData As System.Windows.Forms.Button
    Friend WithEvents Label175 As System.Windows.Forms.Label
    Friend WithEvents btnSearchForAFSFile As System.Windows.Forms.Button
    Friend WithEvents txtAFSFile As System.Windows.Forms.TextBox
    Friend WithEvents dgvAFSData As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton9 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPollutants As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSubPart As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAirProgramCode As System.Windows.Forms.RadioButton
    Friend WithEvents rdbContact As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSIC As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCityZipCode As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAddress As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFacilityName As System.Windows.Forms.RadioButton
    Friend WithEvents GBCompare As System.Windows.Forms.GroupBox
    Friend WithEvents cboCounty As System.Windows.Forms.ComboBox
    Friend WithEvents pnlCompareFull As System.Windows.Forms.Panel
    Friend WithEvents rdbAIRSNumber As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCounty As System.Windows.Forms.RadioButton
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
