<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBEAPClientSearchTool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SBEAPClientSearchTool))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbClear = New System.Windows.Forms.ToolStripButton
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TPClientCompanyName = New System.Windows.Forms.TabPage
        Me.chbSearchHistoricalNames = New System.Windows.Forms.CheckBox
        Me.btnSearchCompanyName = New System.Windows.Forms.Button
        Me.txtSearchCompanyName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TPAddressSearch = New System.Windows.Forms.TabPage
        Me.btnSearchStreet = New System.Windows.Forms.Button
        Me.txtSearchStreet = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TPCitySearch = New System.Windows.Forms.TabPage
        Me.btnSearchCity = New System.Windows.Forms.Button
        Me.txtSearchCity = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TPZipCodeSearch = New System.Windows.Forms.TabPage
        Me.btnSearchZipCode = New System.Windows.Forms.Button
        Me.txtSearchZipCode = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TPCountySearch = New System.Windows.Forms.TabPage
        Me.btnSearchCounty = New System.Windows.Forms.Button
        Me.txtSearchCounty = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TPSICSearch = New System.Windows.Forms.TabPage
        Me.btnSearchSIC = New System.Windows.Forms.Button
        Me.txtSearchSIC = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TPNAICSSearch = New System.Windows.Forms.TabPage
        Me.btnSearchNAICS = New System.Windows.Forms.Button
        Me.txtSearchNAICS = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TPAIRSNumberSearch = New System.Windows.Forms.TabPage
        Me.btnSearchAIRSNumber = New System.Windows.Forms.Button
        Me.txtSearchAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TPNumberOfEmployees = New System.Windows.Forms.TabPage
        Me.mtbSearchNumberOfEmployees = New System.Windows.Forms.MaskedTextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rdbEmployeeGreaterThan = New System.Windows.Forms.RadioButton
        Me.rdbEmployeeLessThan = New System.Windows.Forms.RadioButton
        Me.btnSearchNumberOfEmployees = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.dgvClientInformation = New System.Windows.Forms.DataGridView
        Me.txtClientCompanyName = New System.Windows.Forms.TextBox
        Me.CompanyNameLabel = New System.Windows.Forms.Label
        Me.UseSelection = New System.Windows.Forms.Button
        Me.txtClientID = New System.Windows.Forms.TextBox
        Me.CustomerIDLabel = New System.Windows.Forms.Label
        Me.Cancel = New System.Windows.Forms.Button
        Me.lblSearchResults = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TPClientCompanyName.SuspendLayout()
        Me.TPAddressSearch.SuspendLayout()
        Me.TPCitySearch.SuspendLayout()
        Me.TPZipCodeSearch.SuspendLayout()
        Me.TPCountySearch.SuspendLayout()
        Me.TPSICSearch.SuspendLayout()
        Me.TPNAICSSearch.SuspendLayout()
        Me.TPAIRSNumberSearch.SuspendLayout()
        Me.TPNumberOfEmployees.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvClientInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbClear})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(492, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbClear
        '
        Me.tsbClear.Image = CType(resources.GetObject("tsbClear.Image"), System.Drawing.Image)
        Me.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClear.Name = "tsbClear"
        Me.tsbClear.Size = New System.Drawing.Size(54, 22)
        Me.tsbClear.Text = "Clear"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPClientCompanyName)
        Me.TabControl1.Controls.Add(Me.TPAddressSearch)
        Me.TabControl1.Controls.Add(Me.TPCitySearch)
        Me.TabControl1.Controls.Add(Me.TPZipCodeSearch)
        Me.TabControl1.Controls.Add(Me.TPCountySearch)
        Me.TabControl1.Controls.Add(Me.TPSICSearch)
        Me.TabControl1.Controls.Add(Me.TPNAICSSearch)
        Me.TabControl1.Controls.Add(Me.TPAIRSNumberSearch)
        Me.TabControl1.Controls.Add(Me.TPNumberOfEmployees)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TabControl1.Location = New System.Drawing.Point(0, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(492, 86)
        Me.TabControl1.TabIndex = 0
        '
        'TPClientCompanyName
        '
        Me.TPClientCompanyName.Controls.Add(Me.chbSearchHistoricalNames)
        Me.TPClientCompanyName.Controls.Add(Me.btnSearchCompanyName)
        Me.TPClientCompanyName.Controls.Add(Me.txtSearchCompanyName)
        Me.TPClientCompanyName.Controls.Add(Me.Label1)
        Me.TPClientCompanyName.Location = New System.Drawing.Point(4, 22)
        Me.TPClientCompanyName.Name = "TPClientCompanyName"
        Me.TPClientCompanyName.Padding = New System.Windows.Forms.Padding(3)
        Me.TPClientCompanyName.Size = New System.Drawing.Size(484, 60)
        Me.TPClientCompanyName.TabIndex = 0
        Me.TPClientCompanyName.Text = "Customer Company Name"
        Me.TPClientCompanyName.UseVisualStyleBackColor = True
        '
        'chbSearchHistoricalNames
        '
        Me.chbSearchHistoricalNames.AutoSize = True
        Me.chbSearchHistoricalNames.Location = New System.Drawing.Point(138, 32)
        Me.chbSearchHistoricalNames.Name = "chbSearchHistoricalNames"
        Me.chbSearchHistoricalNames.Size = New System.Drawing.Size(105, 17)
        Me.chbSearchHistoricalNames.TabIndex = 3
        Me.chbSearchHistoricalNames.Text = "Historical Names"
        Me.chbSearchHistoricalNames.UseVisualStyleBackColor = True
        '
        'btnSearchCompanyName
        '
        Me.btnSearchCompanyName.Location = New System.Drawing.Point(305, 3)
        Me.btnSearchCompanyName.Name = "btnSearchCompanyName"
        Me.btnSearchCompanyName.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchCompanyName.TabIndex = 2
        Me.btnSearchCompanyName.Text = "Search"
        Me.btnSearchCompanyName.UseVisualStyleBackColor = True
        '
        'txtSearchCompanyName
        '
        Me.txtSearchCompanyName.Location = New System.Drawing.Point(138, 6)
        Me.txtSearchCompanyName.Name = "txtSearchCompanyName"
        Me.txtSearchCompanyName.Size = New System.Drawing.Size(161, 20)
        Me.txtSearchCompanyName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer Company Name:"
        '
        'TPAddressSearch
        '
        Me.TPAddressSearch.Controls.Add(Me.btnSearchStreet)
        Me.TPAddressSearch.Controls.Add(Me.txtSearchStreet)
        Me.TPAddressSearch.Controls.Add(Me.Label4)
        Me.TPAddressSearch.Location = New System.Drawing.Point(4, 22)
        Me.TPAddressSearch.Name = "TPAddressSearch"
        Me.TPAddressSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.TPAddressSearch.Size = New System.Drawing.Size(484, 60)
        Me.TPAddressSearch.TabIndex = 1
        Me.TPAddressSearch.Text = "Street Address"
        Me.TPAddressSearch.UseVisualStyleBackColor = True
        '
        'btnSearchStreet
        '
        Me.btnSearchStreet.Location = New System.Drawing.Point(256, 3)
        Me.btnSearchStreet.Name = "btnSearchStreet"
        Me.btnSearchStreet.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchStreet.TabIndex = 5
        Me.btnSearchStreet.Text = "Search"
        Me.btnSearchStreet.UseVisualStyleBackColor = True
        '
        'txtSearchStreet
        '
        Me.txtSearchStreet.Location = New System.Drawing.Point(89, 6)
        Me.txtSearchStreet.Name = "txtSearchStreet"
        Me.txtSearchStreet.Size = New System.Drawing.Size(161, 20)
        Me.txtSearchStreet.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Street Address:"
        '
        'TPCitySearch
        '
        Me.TPCitySearch.Controls.Add(Me.btnSearchCity)
        Me.TPCitySearch.Controls.Add(Me.txtSearchCity)
        Me.TPCitySearch.Controls.Add(Me.Label5)
        Me.TPCitySearch.Location = New System.Drawing.Point(4, 22)
        Me.TPCitySearch.Name = "TPCitySearch"
        Me.TPCitySearch.Size = New System.Drawing.Size(484, 60)
        Me.TPCitySearch.TabIndex = 2
        Me.TPCitySearch.Text = "City Search"
        Me.TPCitySearch.UseVisualStyleBackColor = True
        '
        'btnSearchCity
        '
        Me.btnSearchCity.Location = New System.Drawing.Point(204, 3)
        Me.btnSearchCity.Name = "btnSearchCity"
        Me.btnSearchCity.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchCity.TabIndex = 5
        Me.btnSearchCity.Text = "Search"
        Me.btnSearchCity.UseVisualStyleBackColor = True
        '
        'txtSearchCity
        '
        Me.txtSearchCity.Location = New System.Drawing.Point(37, 6)
        Me.txtSearchCity.Name = "txtSearchCity"
        Me.txtSearchCity.Size = New System.Drawing.Size(161, 20)
        Me.txtSearchCity.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "City:"
        '
        'TPZipCodeSearch
        '
        Me.TPZipCodeSearch.Controls.Add(Me.btnSearchZipCode)
        Me.TPZipCodeSearch.Controls.Add(Me.txtSearchZipCode)
        Me.TPZipCodeSearch.Controls.Add(Me.Label6)
        Me.TPZipCodeSearch.Location = New System.Drawing.Point(4, 22)
        Me.TPZipCodeSearch.Name = "TPZipCodeSearch"
        Me.TPZipCodeSearch.Size = New System.Drawing.Size(484, 60)
        Me.TPZipCodeSearch.TabIndex = 3
        Me.TPZipCodeSearch.Text = "Zip Code Search"
        Me.TPZipCodeSearch.UseVisualStyleBackColor = True
        '
        'btnSearchZipCode
        '
        Me.btnSearchZipCode.Location = New System.Drawing.Point(230, 3)
        Me.btnSearchZipCode.Name = "btnSearchZipCode"
        Me.btnSearchZipCode.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchZipCode.TabIndex = 5
        Me.btnSearchZipCode.Text = "Search"
        Me.btnSearchZipCode.UseVisualStyleBackColor = True
        '
        'txtSearchZipCode
        '
        Me.txtSearchZipCode.Location = New System.Drawing.Point(63, 6)
        Me.txtSearchZipCode.Name = "txtSearchZipCode"
        Me.txtSearchZipCode.Size = New System.Drawing.Size(161, 20)
        Me.txtSearchZipCode.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Zip Code:"
        '
        'TPCountySearch
        '
        Me.TPCountySearch.Controls.Add(Me.btnSearchCounty)
        Me.TPCountySearch.Controls.Add(Me.txtSearchCounty)
        Me.TPCountySearch.Controls.Add(Me.Label7)
        Me.TPCountySearch.Location = New System.Drawing.Point(4, 22)
        Me.TPCountySearch.Name = "TPCountySearch"
        Me.TPCountySearch.Size = New System.Drawing.Size(484, 60)
        Me.TPCountySearch.TabIndex = 4
        Me.TPCountySearch.Text = "County Search"
        Me.TPCountySearch.UseVisualStyleBackColor = True
        '
        'btnSearchCounty
        '
        Me.btnSearchCounty.Location = New System.Drawing.Point(220, 3)
        Me.btnSearchCounty.Name = "btnSearchCounty"
        Me.btnSearchCounty.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchCounty.TabIndex = 5
        Me.btnSearchCounty.Text = "Search"
        Me.btnSearchCounty.UseVisualStyleBackColor = True
        '
        'txtSearchCounty
        '
        Me.txtSearchCounty.Location = New System.Drawing.Point(53, 6)
        Me.txtSearchCounty.Name = "txtSearchCounty"
        Me.txtSearchCounty.Size = New System.Drawing.Size(161, 20)
        Me.txtSearchCounty.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "County:"
        '
        'TPSICSearch
        '
        Me.TPSICSearch.Controls.Add(Me.btnSearchSIC)
        Me.TPSICSearch.Controls.Add(Me.txtSearchSIC)
        Me.TPSICSearch.Controls.Add(Me.Label8)
        Me.TPSICSearch.Location = New System.Drawing.Point(4, 22)
        Me.TPSICSearch.Name = "TPSICSearch"
        Me.TPSICSearch.Size = New System.Drawing.Size(484, 60)
        Me.TPSICSearch.TabIndex = 5
        Me.TPSICSearch.Text = "SIC Search"
        Me.TPSICSearch.UseVisualStyleBackColor = True
        '
        'btnSearchSIC
        '
        Me.btnSearchSIC.Location = New System.Drawing.Point(204, 3)
        Me.btnSearchSIC.Name = "btnSearchSIC"
        Me.btnSearchSIC.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchSIC.TabIndex = 5
        Me.btnSearchSIC.Text = "Search"
        Me.btnSearchSIC.UseVisualStyleBackColor = True
        '
        'txtSearchSIC
        '
        Me.txtSearchSIC.Location = New System.Drawing.Point(37, 6)
        Me.txtSearchSIC.Name = "txtSearchSIC"
        Me.txtSearchSIC.Size = New System.Drawing.Size(161, 20)
        Me.txtSearchSIC.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 13)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "SIC:"
        '
        'TPNAICSSearch
        '
        Me.TPNAICSSearch.Controls.Add(Me.btnSearchNAICS)
        Me.TPNAICSSearch.Controls.Add(Me.txtSearchNAICS)
        Me.TPNAICSSearch.Controls.Add(Me.Label9)
        Me.TPNAICSSearch.Location = New System.Drawing.Point(4, 22)
        Me.TPNAICSSearch.Name = "TPNAICSSearch"
        Me.TPNAICSSearch.Size = New System.Drawing.Size(484, 60)
        Me.TPNAICSSearch.TabIndex = 6
        Me.TPNAICSSearch.Text = "NAICS Search"
        Me.TPNAICSSearch.UseVisualStyleBackColor = True
        '
        'btnSearchNAICS
        '
        Me.btnSearchNAICS.Location = New System.Drawing.Point(219, 3)
        Me.btnSearchNAICS.Name = "btnSearchNAICS"
        Me.btnSearchNAICS.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchNAICS.TabIndex = 5
        Me.btnSearchNAICS.Text = "Search"
        Me.btnSearchNAICS.UseVisualStyleBackColor = True
        '
        'txtSearchNAICS
        '
        Me.txtSearchNAICS.Location = New System.Drawing.Point(52, 6)
        Me.txtSearchNAICS.Name = "txtSearchNAICS"
        Me.txtSearchNAICS.Size = New System.Drawing.Size(161, 20)
        Me.txtSearchNAICS.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(4, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "NAICS:"
        '
        'TPAIRSNumberSearch
        '
        Me.TPAIRSNumberSearch.Controls.Add(Me.btnSearchAIRSNumber)
        Me.TPAIRSNumberSearch.Controls.Add(Me.txtSearchAIRSNumber)
        Me.TPAIRSNumberSearch.Controls.Add(Me.Label10)
        Me.TPAIRSNumberSearch.Location = New System.Drawing.Point(4, 22)
        Me.TPAIRSNumberSearch.Name = "TPAIRSNumberSearch"
        Me.TPAIRSNumberSearch.Size = New System.Drawing.Size(484, 60)
        Me.TPAIRSNumberSearch.TabIndex = 7
        Me.TPAIRSNumberSearch.Text = "AIRS Number Search"
        Me.TPAIRSNumberSearch.UseVisualStyleBackColor = True
        '
        'btnSearchAIRSNumber
        '
        Me.btnSearchAIRSNumber.Location = New System.Drawing.Point(252, 3)
        Me.btnSearchAIRSNumber.Name = "btnSearchAIRSNumber"
        Me.btnSearchAIRSNumber.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchAIRSNumber.TabIndex = 5
        Me.btnSearchAIRSNumber.Text = "Search"
        Me.btnSearchAIRSNumber.UseVisualStyleBackColor = True
        '
        'txtSearchAIRSNumber
        '
        Me.txtSearchAIRSNumber.Location = New System.Drawing.Point(85, 6)
        Me.txtSearchAIRSNumber.Name = "txtSearchAIRSNumber"
        Me.txtSearchAIRSNumber.Size = New System.Drawing.Size(161, 20)
        Me.txtSearchAIRSNumber.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "AIRS Number:"
        '
        'TPNumberOfEmployees
        '
        Me.TPNumberOfEmployees.Controls.Add(Me.mtbSearchNumberOfEmployees)
        Me.TPNumberOfEmployees.Controls.Add(Me.Panel2)
        Me.TPNumberOfEmployees.Controls.Add(Me.btnSearchNumberOfEmployees)
        Me.TPNumberOfEmployees.Controls.Add(Me.Label11)
        Me.TPNumberOfEmployees.Location = New System.Drawing.Point(4, 22)
        Me.TPNumberOfEmployees.Name = "TPNumberOfEmployees"
        Me.TPNumberOfEmployees.Size = New System.Drawing.Size(484, 60)
        Me.TPNumberOfEmployees.TabIndex = 8
        Me.TPNumberOfEmployees.Text = "# of Employees Search"
        Me.TPNumberOfEmployees.UseVisualStyleBackColor = True
        '
        'mtbSearchNumberOfEmployees
        '
        Me.mtbSearchNumberOfEmployees.Location = New System.Drawing.Point(93, 6)
        Me.mtbSearchNumberOfEmployees.Mask = "00000"
        Me.mtbSearchNumberOfEmployees.Name = "mtbSearchNumberOfEmployees"
        Me.mtbSearchNumberOfEmployees.Size = New System.Drawing.Size(40, 20)
        Me.mtbSearchNumberOfEmployees.TabIndex = 7
        Me.mtbSearchNumberOfEmployees.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mtbSearchNumberOfEmployees.ValidatingType = GetType(Integer)
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.Controls.Add(Me.rdbEmployeeGreaterThan)
        Me.Panel2.Controls.Add(Me.rdbEmployeeLessThan)
        Me.Panel2.Location = New System.Drawing.Point(139, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(86, 23)
        Me.Panel2.TabIndex = 6
        '
        'rdbEmployeeGreaterThan
        '
        Me.rdbEmployeeGreaterThan.AutoSize = True
        Me.rdbEmployeeGreaterThan.Location = New System.Drawing.Point(46, 3)
        Me.rdbEmployeeGreaterThan.Name = "rdbEmployeeGreaterThan"
        Me.rdbEmployeeGreaterThan.Size = New System.Drawing.Size(37, 17)
        Me.rdbEmployeeGreaterThan.TabIndex = 1
        Me.rdbEmployeeGreaterThan.TabStop = True
        Me.rdbEmployeeGreaterThan.Text = ">="
        Me.rdbEmployeeGreaterThan.UseVisualStyleBackColor = True
        '
        'rdbEmployeeLessThan
        '
        Me.rdbEmployeeLessThan.AutoSize = True
        Me.rdbEmployeeLessThan.Location = New System.Drawing.Point(3, 3)
        Me.rdbEmployeeLessThan.Name = "rdbEmployeeLessThan"
        Me.rdbEmployeeLessThan.Size = New System.Drawing.Size(37, 17)
        Me.rdbEmployeeLessThan.TabIndex = 0
        Me.rdbEmployeeLessThan.TabStop = True
        Me.rdbEmployeeLessThan.Text = "<="
        Me.rdbEmployeeLessThan.UseVisualStyleBackColor = True
        '
        'btnSearchNumberOfEmployees
        '
        Me.btnSearchNumberOfEmployees.Location = New System.Drawing.Point(231, 3)
        Me.btnSearchNumberOfEmployees.Name = "btnSearchNumberOfEmployees"
        Me.btnSearchNumberOfEmployees.Size = New System.Drawing.Size(65, 25)
        Me.btnSearchNumberOfEmployees.TabIndex = 5
        Me.btnSearchNumberOfEmployees.Text = "Search"
        Me.btnSearchNumberOfEmployees.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 13)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "# of Employees:"
        '
        'dgvClientInformation
        '
        Me.dgvClientInformation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvClientInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClientInformation.Location = New System.Drawing.Point(0, 194)
        Me.dgvClientInformation.Name = "dgvClientInformation"
        Me.dgvClientInformation.Size = New System.Drawing.Size(492, 272)
        Me.dgvClientInformation.TabIndex = 1
        '
        'txtClientCompanyName
        '
        Me.txtClientCompanyName.Location = New System.Drawing.Point(119, 143)
        Me.txtClientCompanyName.Name = "txtClientCompanyName"
        Me.txtClientCompanyName.ReadOnly = True
        Me.txtClientCompanyName.Size = New System.Drawing.Size(161, 20)
        Me.txtClientCompanyName.TabIndex = 7
        '
        'CompanyNameLabel
        '
        Me.CompanyNameLabel.AutoSize = True
        Me.CompanyNameLabel.Location = New System.Drawing.Point(15, 146)
        Me.CompanyNameLabel.Name = "CompanyNameLabel"
        Me.CompanyNameLabel.Size = New System.Drawing.Size(98, 13)
        Me.CompanyNameLabel.TabIndex = 6
        Me.CompanyNameLabel.Text = "Customer Company"
        '
        'UseSelection
        '
        Me.UseSelection.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.UseSelection.Enabled = False
        Me.UseSelection.Location = New System.Drawing.Point(286, 117)
        Me.UseSelection.Name = "UseSelection"
        Me.UseSelection.Size = New System.Drawing.Size(65, 46)
        Me.UseSelection.TabIndex = 5
        Me.UseSelection.Text = "Use"
        Me.UseSelection.UseVisualStyleBackColor = True
        '
        'txtClientID
        '
        Me.txtClientID.Location = New System.Drawing.Point(119, 117)
        Me.txtClientID.Name = "txtClientID"
        Me.txtClientID.ReadOnly = True
        Me.txtClientID.Size = New System.Drawing.Size(161, 20)
        Me.txtClientID.TabIndex = 4
        '
        'CustomerIDLabel
        '
        Me.CustomerIDLabel.AutoSize = True
        Me.CustomerIDLabel.Location = New System.Drawing.Point(15, 120)
        Me.CustomerIDLabel.Name = "CustomerIDLabel"
        Me.CustomerIDLabel.Size = New System.Drawing.Size(65, 13)
        Me.CustomerIDLabel.TabIndex = 3
        Me.CustomerIDLabel.Text = "Customer ID"
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(357, 117)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(65, 46)
        Me.Cancel.TabIndex = 8
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'lblSearchResults
        '
        Me.lblSearchResults.AutoSize = True
        Me.lblSearchResults.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchResults.Location = New System.Drawing.Point(15, 178)
        Me.lblSearchResults.Name = "lblSearchResults"
        Me.lblSearchResults.Size = New System.Drawing.Size(195, 13)
        Me.lblSearchResults.TabIndex = 217
        Me.lblSearchResults.Text = "Possible Matches to Your Search"
        '
        'SBEAPClientSearchTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 466)
        Me.Controls.Add(Me.lblSearchResults)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.dgvClientInformation)
        Me.Controls.Add(Me.txtClientCompanyName)
        Me.Controls.Add(Me.CustomerIDLabel)
        Me.Controls.Add(Me.CompanyNameLabel)
        Me.Controls.Add(Me.txtClientID)
        Me.Controls.Add(Me.UseSelection)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "SBEAPClientSearchTool"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SBEAP Customer Search Tool"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TPClientCompanyName.ResumeLayout(False)
        Me.TPClientCompanyName.PerformLayout()
        Me.TPAddressSearch.ResumeLayout(False)
        Me.TPAddressSearch.PerformLayout()
        Me.TPCitySearch.ResumeLayout(False)
        Me.TPCitySearch.PerformLayout()
        Me.TPZipCodeSearch.ResumeLayout(False)
        Me.TPZipCodeSearch.PerformLayout()
        Me.TPCountySearch.ResumeLayout(False)
        Me.TPCountySearch.PerformLayout()
        Me.TPSICSearch.ResumeLayout(False)
        Me.TPSICSearch.PerformLayout()
        Me.TPNAICSSearch.ResumeLayout(False)
        Me.TPNAICSSearch.PerformLayout()
        Me.TPAIRSNumberSearch.ResumeLayout(False)
        Me.TPAIRSNumberSearch.PerformLayout()
        Me.TPNumberOfEmployees.ResumeLayout(False)
        Me.TPNumberOfEmployees.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvClientInformation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPClientCompanyName As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TPAddressSearch As System.Windows.Forms.TabPage
    Friend WithEvents TPCitySearch As System.Windows.Forms.TabPage
    Friend WithEvents TPZipCodeSearch As System.Windows.Forms.TabPage
    Friend WithEvents TPCountySearch As System.Windows.Forms.TabPage
    Friend WithEvents dgvClientInformation As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearchCompanyName As System.Windows.Forms.Button
    Friend WithEvents txtSearchCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents txtClientCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents CompanyNameLabel As System.Windows.Forms.Label
    Friend WithEvents UseSelection As System.Windows.Forms.Button
    Friend WithEvents txtClientID As System.Windows.Forms.TextBox
    Friend WithEvents CustomerIDLabel As System.Windows.Forms.Label
    Friend WithEvents chbSearchHistoricalNames As System.Windows.Forms.CheckBox
    Friend WithEvents TPSICSearch As System.Windows.Forms.TabPage
    Friend WithEvents TPNAICSSearch As System.Windows.Forms.TabPage
    Friend WithEvents TPAIRSNumberSearch As System.Windows.Forms.TabPage
    Friend WithEvents TPNumberOfEmployees As System.Windows.Forms.TabPage
    Friend WithEvents btnSearchStreet As System.Windows.Forms.Button
    Friend WithEvents txtSearchStreet As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSearchCity As System.Windows.Forms.Button
    Friend WithEvents txtSearchCity As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSearchZipCode As System.Windows.Forms.Button
    Friend WithEvents txtSearchZipCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnSearchCounty As System.Windows.Forms.Button
    Friend WithEvents txtSearchCounty As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnSearchSIC As System.Windows.Forms.Button
    Friend WithEvents txtSearchSIC As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSearchNAICS As System.Windows.Forms.Button
    Friend WithEvents txtSearchNAICS As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSearchAIRSNumber As System.Windows.Forms.Button
    Friend WithEvents txtSearchAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnSearchNumberOfEmployees As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents rdbEmployeeGreaterThan As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEmployeeLessThan As System.Windows.Forms.RadioButton
    Friend WithEvents mtbSearchNumberOfEmployees As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents lblSearchResults As System.Windows.Forms.Label
End Class
