<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPFacilityLookUpTool
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tcSearchOptions = New System.Windows.Forms.TabControl()
        Me.tpFacilityName = New System.Windows.Forms.TabPage()
        Me.chbHistoricalNames = New System.Windows.Forms.CheckBox()
        Me.btnFacilityNameSearch = New System.Windows.Forms.Button()
        Me.txtFacilityNameSearch = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.tpAIRSNumber = New System.Windows.Forms.TabPage()
        Me.btnAIRSNumberSearch = New System.Windows.Forms.Button()
        Me.txtAIRSNumberSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tpComplianceSearch = New System.Windows.Forms.TabPage()
        Me.btnComplianceSearch = New System.Windows.Forms.Button()
        Me.txtComplianceEngineer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tpCity = New System.Windows.Forms.TabPage()
        Me.btnCitySearch = New System.Windows.Forms.Button()
        Me.txtCityNameSearch = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.tpZipCode = New System.Windows.Forms.TabPage()
        Me.btnZipCodeSearch = New System.Windows.Forms.Button()
        Me.txtZipCodeSearch = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.tpSIC = New System.Windows.Forms.TabPage()
        Me.btnSICCodeSearch = New System.Windows.Forms.Button()
        Me.txtSICCodeSearch = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.tpCounty = New System.Windows.Forms.TabPage()
        Me.txtCountyNameSearch = New System.Windows.Forms.TextBox()
        Me.btnCountySearch = New System.Windows.Forms.Button()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.tpSubpart = New System.Windows.Forms.TabPage()
        Me.rdbGASIP = New System.Windows.Forms.RadioButton()
        Me.rdbPart63 = New System.Windows.Forms.RadioButton()
        Me.rdbPart60 = New System.Windows.Forms.RadioButton()
        Me.rdbPart61 = New System.Windows.Forms.RadioButton()
        Me.txtSubpartSearch = New System.Windows.Forms.TextBox()
        Me.btnSubpartSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvResults = New System.Windows.Forms.DataGridView()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.lblSearchResults = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox()
        Me.btnUseAIRSNumber = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ClearButton = New System.Windows.Forms.ToolStripButton()
        Me.tcSearchOptions.SuspendLayout()
        Me.tpFacilityName.SuspendLayout()
        Me.tpAIRSNumber.SuspendLayout()
        Me.tpComplianceSearch.SuspendLayout()
        Me.tpCity.SuspendLayout()
        Me.tpZipCode.SuspendLayout()
        Me.tpSIC.SuspendLayout()
        Me.tpCounty.SuspendLayout()
        Me.tpSubpart.SuspendLayout()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcSearchOptions
        '
        Me.tcSearchOptions.Controls.Add(Me.tpFacilityName)
        Me.tcSearchOptions.Controls.Add(Me.tpAIRSNumber)
        Me.tcSearchOptions.Controls.Add(Me.tpComplianceSearch)
        Me.tcSearchOptions.Controls.Add(Me.tpCity)
        Me.tcSearchOptions.Controls.Add(Me.tpZipCode)
        Me.tcSearchOptions.Controls.Add(Me.tpSIC)
        Me.tcSearchOptions.Controls.Add(Me.tpCounty)
        Me.tcSearchOptions.Controls.Add(Me.tpSubpart)
        Me.tcSearchOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.tcSearchOptions.Location = New System.Drawing.Point(0, 25)
        Me.tcSearchOptions.Name = "tcSearchOptions"
        Me.tcSearchOptions.SelectedIndex = 0
        Me.tcSearchOptions.Size = New System.Drawing.Size(488, 104)
        Me.tcSearchOptions.TabIndex = 0
        '
        'tpFacilityName
        '
        Me.tpFacilityName.Controls.Add(Me.chbHistoricalNames)
        Me.tpFacilityName.Controls.Add(Me.btnFacilityNameSearch)
        Me.tpFacilityName.Controls.Add(Me.txtFacilityNameSearch)
        Me.tpFacilityName.Controls.Add(Me.Label69)
        Me.tpFacilityName.Location = New System.Drawing.Point(4, 22)
        Me.tpFacilityName.Name = "tpFacilityName"
        Me.tpFacilityName.Size = New System.Drawing.Size(480, 78)
        Me.tpFacilityName.TabIndex = 1
        Me.tpFacilityName.Text = "Facility Name"
        Me.tpFacilityName.UseVisualStyleBackColor = True
        '
        'chbHistoricalNames
        '
        Me.chbHistoricalNames.AutoSize = True
        Me.chbHistoricalNames.Location = New System.Drawing.Point(11, 53)
        Me.chbHistoricalNames.Name = "chbHistoricalNames"
        Me.chbHistoricalNames.Size = New System.Drawing.Size(105, 17)
        Me.chbHistoricalNames.TabIndex = 2
        Me.chbHistoricalNames.Text = "Historical Names"
        Me.chbHistoricalNames.UseVisualStyleBackColor = True
        '
        'btnFacilityNameSearch
        '
        Me.btnFacilityNameSearch.Location = New System.Drawing.Point(177, 25)
        Me.btnFacilityNameSearch.Name = "btnFacilityNameSearch"
        Me.btnFacilityNameSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnFacilityNameSearch.TabIndex = 1
        Me.btnFacilityNameSearch.Text = "Search"
        '
        'txtFacilityNameSearch
        '
        Me.txtFacilityNameSearch.Location = New System.Drawing.Point(11, 27)
        Me.txtFacilityNameSearch.Name = "txtFacilityNameSearch"
        Me.txtFacilityNameSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtFacilityNameSearch.TabIndex = 0
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(8, 11)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(70, 13)
        Me.Label69.TabIndex = 17
        Me.Label69.Text = "Facility Name"
        '
        'tpAIRSNumber
        '
        Me.tpAIRSNumber.Controls.Add(Me.btnAIRSNumberSearch)
        Me.tpAIRSNumber.Controls.Add(Me.txtAIRSNumberSearch)
        Me.tpAIRSNumber.Controls.Add(Me.Label1)
        Me.tpAIRSNumber.Location = New System.Drawing.Point(4, 22)
        Me.tpAIRSNumber.Name = "tpAIRSNumber"
        Me.tpAIRSNumber.Size = New System.Drawing.Size(480, 78)
        Me.tpAIRSNumber.TabIndex = 0
        Me.tpAIRSNumber.Text = "AIRS Number"
        Me.tpAIRSNumber.UseVisualStyleBackColor = True
        '
        'btnAIRSNumberSearch
        '
        Me.btnAIRSNumberSearch.Location = New System.Drawing.Point(177, 25)
        Me.btnAIRSNumberSearch.Name = "btnAIRSNumberSearch"
        Me.btnAIRSNumberSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnAIRSNumberSearch.TabIndex = 1
        Me.btnAIRSNumberSearch.Text = "Search"
        '
        'txtAIRSNumberSearch
        '
        Me.txtAIRSNumberSearch.Location = New System.Drawing.Point(11, 27)
        Me.txtAIRSNumberSearch.Name = "txtAIRSNumberSearch"
        Me.txtAIRSNumberSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtAIRSNumberSearch.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "AIRS Number"
        '
        'tpComplianceSearch
        '
        Me.tpComplianceSearch.Controls.Add(Me.btnComplianceSearch)
        Me.tpComplianceSearch.Controls.Add(Me.txtComplianceEngineer)
        Me.tpComplianceSearch.Controls.Add(Me.Label2)
        Me.tpComplianceSearch.Location = New System.Drawing.Point(4, 22)
        Me.tpComplianceSearch.Name = "tpComplianceSearch"
        Me.tpComplianceSearch.Size = New System.Drawing.Size(480, 78)
        Me.tpComplianceSearch.TabIndex = 8
        Me.tpComplianceSearch.Text = "Inspector"
        Me.tpComplianceSearch.UseVisualStyleBackColor = True
        '
        'btnComplianceSearch
        '
        Me.btnComplianceSearch.Location = New System.Drawing.Point(177, 25)
        Me.btnComplianceSearch.Name = "btnComplianceSearch"
        Me.btnComplianceSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnComplianceSearch.TabIndex = 1
        Me.btnComplianceSearch.Text = "Search"
        '
        'txtComplianceEngineer
        '
        Me.txtComplianceEngineer.Location = New System.Drawing.Point(11, 27)
        Me.txtComplianceEngineer.Name = "txtComplianceEngineer"
        Me.txtComplianceEngineer.Size = New System.Drawing.Size(160, 20)
        Me.txtComplianceEngineer.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Compliance Inspector"
        '
        'tpCity
        '
        Me.tpCity.Controls.Add(Me.btnCitySearch)
        Me.tpCity.Controls.Add(Me.txtCityNameSearch)
        Me.tpCity.Controls.Add(Me.Label64)
        Me.tpCity.Location = New System.Drawing.Point(4, 22)
        Me.tpCity.Name = "tpCity"
        Me.tpCity.Size = New System.Drawing.Size(480, 78)
        Me.tpCity.TabIndex = 3
        Me.tpCity.Text = "City"
        Me.tpCity.UseVisualStyleBackColor = True
        '
        'btnCitySearch
        '
        Me.btnCitySearch.Location = New System.Drawing.Point(177, 25)
        Me.btnCitySearch.Name = "btnCitySearch"
        Me.btnCitySearch.Size = New System.Drawing.Size(78, 23)
        Me.btnCitySearch.TabIndex = 1
        Me.btnCitySearch.Text = "Search"
        '
        'txtCityNameSearch
        '
        Me.txtCityNameSearch.Location = New System.Drawing.Point(11, 27)
        Me.txtCityNameSearch.Name = "txtCityNameSearch"
        Me.txtCityNameSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtCityNameSearch.TabIndex = 0
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(8, 11)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(24, 13)
        Me.Label64.TabIndex = 24
        Me.Label64.Text = "City"
        '
        'tpZipCode
        '
        Me.tpZipCode.Controls.Add(Me.btnZipCodeSearch)
        Me.tpZipCode.Controls.Add(Me.txtZipCodeSearch)
        Me.tpZipCode.Controls.Add(Me.Label65)
        Me.tpZipCode.Location = New System.Drawing.Point(4, 22)
        Me.tpZipCode.Name = "tpZipCode"
        Me.tpZipCode.Size = New System.Drawing.Size(480, 78)
        Me.tpZipCode.TabIndex = 4
        Me.tpZipCode.Text = "Zip Code"
        Me.tpZipCode.UseVisualStyleBackColor = True
        '
        'btnZipCodeSearch
        '
        Me.btnZipCodeSearch.Location = New System.Drawing.Point(177, 25)
        Me.btnZipCodeSearch.Name = "btnZipCodeSearch"
        Me.btnZipCodeSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnZipCodeSearch.TabIndex = 1
        Me.btnZipCodeSearch.Text = "Search"
        '
        'txtZipCodeSearch
        '
        Me.txtZipCodeSearch.Location = New System.Drawing.Point(11, 27)
        Me.txtZipCodeSearch.Name = "txtZipCodeSearch"
        Me.txtZipCodeSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtZipCodeSearch.TabIndex = 0
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(8, 11)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(50, 13)
        Me.Label65.TabIndex = 27
        Me.Label65.Text = "Zip Code"
        '
        'tpSIC
        '
        Me.tpSIC.Controls.Add(Me.btnSICCodeSearch)
        Me.tpSIC.Controls.Add(Me.txtSICCodeSearch)
        Me.tpSIC.Controls.Add(Me.Label70)
        Me.tpSIC.Location = New System.Drawing.Point(4, 22)
        Me.tpSIC.Name = "tpSIC"
        Me.tpSIC.Size = New System.Drawing.Size(480, 78)
        Me.tpSIC.TabIndex = 6
        Me.tpSIC.Text = "SIC Code"
        Me.tpSIC.UseVisualStyleBackColor = True
        '
        'btnSICCodeSearch
        '
        Me.btnSICCodeSearch.Location = New System.Drawing.Point(177, 25)
        Me.btnSICCodeSearch.Name = "btnSICCodeSearch"
        Me.btnSICCodeSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnSICCodeSearch.TabIndex = 1
        Me.btnSICCodeSearch.Text = "Search"
        '
        'txtSICCodeSearch
        '
        Me.txtSICCodeSearch.Location = New System.Drawing.Point(11, 27)
        Me.txtSICCodeSearch.Name = "txtSICCodeSearch"
        Me.txtSICCodeSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtSICCodeSearch.TabIndex = 0
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(8, 11)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(52, 13)
        Me.Label70.TabIndex = 27
        Me.Label70.Text = "SIC Code"
        '
        'tpCounty
        '
        Me.tpCounty.Controls.Add(Me.txtCountyNameSearch)
        Me.tpCounty.Controls.Add(Me.btnCountySearch)
        Me.tpCounty.Controls.Add(Me.Label63)
        Me.tpCounty.Location = New System.Drawing.Point(4, 22)
        Me.tpCounty.Name = "tpCounty"
        Me.tpCounty.Size = New System.Drawing.Size(480, 78)
        Me.tpCounty.TabIndex = 2
        Me.tpCounty.Text = "County"
        Me.tpCounty.UseVisualStyleBackColor = True
        '
        'txtCountyNameSearch
        '
        Me.txtCountyNameSearch.Location = New System.Drawing.Point(11, 27)
        Me.txtCountyNameSearch.Name = "txtCountyNameSearch"
        Me.txtCountyNameSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtCountyNameSearch.TabIndex = 0
        '
        'btnCountySearch
        '
        Me.btnCountySearch.Location = New System.Drawing.Point(177, 25)
        Me.btnCountySearch.Name = "btnCountySearch"
        Me.btnCountySearch.Size = New System.Drawing.Size(78, 23)
        Me.btnCountySearch.TabIndex = 0
        Me.btnCountySearch.Text = "Search"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(8, 11)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(40, 13)
        Me.Label63.TabIndex = 24
        Me.Label63.Text = "County"
        '
        'tpSubpart
        '
        Me.tpSubpart.Controls.Add(Me.rdbGASIP)
        Me.tpSubpart.Controls.Add(Me.rdbPart63)
        Me.tpSubpart.Controls.Add(Me.rdbPart60)
        Me.tpSubpart.Controls.Add(Me.rdbPart61)
        Me.tpSubpart.Controls.Add(Me.txtSubpartSearch)
        Me.tpSubpart.Controls.Add(Me.btnSubpartSearch)
        Me.tpSubpart.Controls.Add(Me.Label3)
        Me.tpSubpart.Location = New System.Drawing.Point(4, 22)
        Me.tpSubpart.Name = "tpSubpart"
        Me.tpSubpart.Size = New System.Drawing.Size(480, 78)
        Me.tpSubpart.TabIndex = 9
        Me.tpSubpart.Text = "Subpart "
        Me.tpSubpart.UseVisualStyleBackColor = True
        '
        'rdbGASIP
        '
        Me.rdbGASIP.AutoSize = True
        Me.rdbGASIP.Checked = True
        Me.rdbGASIP.Location = New System.Drawing.Point(11, 53)
        Me.rdbGASIP.Name = "rdbGASIP"
        Me.rdbGASIP.Size = New System.Drawing.Size(86, 17)
        Me.rdbGASIP.TabIndex = 2
        Me.rdbGASIP.TabStop = True
        Me.rdbGASIP.Text = "SIP - GA SIP"
        Me.rdbGASIP.UseVisualStyleBackColor = True
        '
        'rdbPart63
        '
        Me.rdbPart63.AutoSize = True
        Me.rdbPart63.Location = New System.Drawing.Point(324, 53)
        Me.rdbPart63.Name = "rdbPart63"
        Me.rdbPart63.Size = New System.Drawing.Size(98, 17)
        Me.rdbPart63.TabIndex = 5
        Me.rdbPart63.TabStop = True
        Me.rdbPart63.Text = "MACT (Part 63)"
        Me.rdbPart63.UseVisualStyleBackColor = True
        '
        'rdbPart60
        '
        Me.rdbPart60.AutoSize = True
        Me.rdbPart60.Location = New System.Drawing.Point(103, 53)
        Me.rdbPart60.Name = "rdbPart60"
        Me.rdbPart60.Size = New System.Drawing.Size(97, 17)
        Me.rdbPart60.TabIndex = 3
        Me.rdbPart60.Text = "NSPS (Part 60)"
        Me.rdbPart60.UseVisualStyleBackColor = True
        '
        'rdbPart61
        '
        Me.rdbPart61.AutoSize = True
        Me.rdbPart61.Location = New System.Drawing.Point(206, 53)
        Me.rdbPart61.Name = "rdbPart61"
        Me.rdbPart61.Size = New System.Drawing.Size(112, 17)
        Me.rdbPart61.TabIndex = 4
        Me.rdbPart61.Text = "NESHAP (Part 61)"
        Me.rdbPart61.UseVisualStyleBackColor = True
        '
        'txtSubpartSearch
        '
        Me.txtSubpartSearch.Location = New System.Drawing.Point(11, 27)
        Me.txtSubpartSearch.Name = "txtSubpartSearch"
        Me.txtSubpartSearch.Size = New System.Drawing.Size(160, 20)
        Me.txtSubpartSearch.TabIndex = 0
        '
        'btnSubpartSearch
        '
        Me.btnSubpartSearch.Location = New System.Drawing.Point(177, 25)
        Me.btnSubpartSearch.Name = "btnSubpartSearch"
        Me.btnSubpartSearch.Size = New System.Drawing.Size(78, 23)
        Me.btnSubpartSearch.TabIndex = 1
        Me.btnSubpartSearch.Text = "Search"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Regulation Subpart"
        '
        'dgvResults
        '
        Me.dgvResults.AllowUserToAddRows = False
        Me.dgvResults.AllowUserToDeleteRows = False
        Me.dgvResults.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Location = New System.Drawing.Point(0, 215)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.ReadOnly = True
        Me.dgvResults.RowHeadersVisible = False
        Me.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvResults.Size = New System.Drawing.Size(488, 101)
        Me.dgvResults.TabIndex = 3
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(12, 167)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(70, 13)
        Me.Label68.TabIndex = 212
        Me.Label68.Text = "Facility Name"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(90, 164)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(210, 20)
        Me.txtFacilityName.TabIndex = 215
        '
        'lblSearchResults
        '
        Me.lblSearchResults.AutoSize = True
        Me.lblSearchResults.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchResults.Location = New System.Drawing.Point(12, 199)
        Me.lblSearchResults.Name = "lblSearchResults"
        Me.lblSearchResults.Size = New System.Drawing.Size(88, 13)
        Me.lblSearchResults.TabIndex = 216
        Me.lblSearchResults.Text = "Search results"
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(12, 141)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(72, 13)
        Me.Label67.TabIndex = 213
        Me.Label67.Text = "AIRS Number"
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(90, 138)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(210, 20)
        Me.txtAIRSNumber.TabIndex = 214
        '
        'btnUseAIRSNumber
        '
        Me.btnUseAIRSNumber.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnUseAIRSNumber.Enabled = False
        Me.btnUseAIRSNumber.Location = New System.Drawing.Point(306, 138)
        Me.btnUseAIRSNumber.Name = "btnUseAIRSNumber"
        Me.btnUseAIRSNumber.Size = New System.Drawing.Size(70, 46)
        Me.btnUseAIRSNumber.TabIndex = 1
        Me.btnUseAIRSNumber.Text = "Use"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(382, 138)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(70, 46)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClearButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(488, 25)
        Me.ToolStrip1.TabIndex = 217
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ClearButton
        '
        Me.ClearButton.Image = Global.Iaip.My.Resources.Resources.EraseIcon
        Me.ClearButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(54, 22)
        Me.ClearButton.Text = "Clear"
        Me.ClearButton.ToolTipText = "Clear Form"
        '
        'IAIPFacilityLookUpTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 316)
        Me.Controls.Add(Me.dgvResults)
        Me.Controls.Add(Me.Label68)
        Me.Controls.Add(Me.txtFacilityName)
        Me.Controls.Add(Me.lblSearchResults)
        Me.Controls.Add(Me.Label67)
        Me.Controls.Add(Me.txtAIRSNumber)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnUseAIRSNumber)
        Me.Controls.Add(Me.tcSearchOptions)
        Me.Controls.Add(Me.ToolStrip1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(504, 354)
        Me.Name = "IAIPFacilityLookUpTool"
        Me.Text = "Facility Search"
        Me.tcSearchOptions.ResumeLayout(False)
        Me.tpFacilityName.ResumeLayout(False)
        Me.tpFacilityName.PerformLayout()
        Me.tpAIRSNumber.ResumeLayout(False)
        Me.tpAIRSNumber.PerformLayout()
        Me.tpComplianceSearch.ResumeLayout(False)
        Me.tpComplianceSearch.PerformLayout()
        Me.tpCity.ResumeLayout(False)
        Me.tpCity.PerformLayout()
        Me.tpZipCode.ResumeLayout(False)
        Me.tpZipCode.PerformLayout()
        Me.tpSIC.ResumeLayout(False)
        Me.tpSIC.PerformLayout()
        Me.tpCounty.ResumeLayout(False)
        Me.tpCounty.PerformLayout()
        Me.tpSubpart.ResumeLayout(False)
        Me.tpSubpart.PerformLayout()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tcSearchOptions As System.Windows.Forms.TabControl
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
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents lblSearchResults As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnUseAIRSNumber As System.Windows.Forms.Button
    Friend WithEvents chbHistoricalNames As System.Windows.Forms.CheckBox
    Friend WithEvents tpSubpart As System.Windows.Forms.TabPage
    Friend WithEvents txtSubpartSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSubpartSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdbPart63 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPart60 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPart61 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGASIP As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ClearButton As System.Windows.Forms.ToolStripButton
End Class
