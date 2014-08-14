<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBEAPReports
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
        Me.components = New System.ComponentModel.Container
        Me.SBEAPCLIENTDATABindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SBEAPCLIENTSBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnRunSBEAPReport = New System.Windows.Forms.Button
        Me.DTPReportStartDate = New System.Windows.Forms.DateTimePicker
        Me.Label18 = New System.Windows.Forms.Label
        Me.DTPReportEndDate = New System.Windows.Forms.DateTimePicker
        Me.Label17 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.llbFrontDestCalls = New System.Windows.Forms.LinkLabel
        Me.txtFrontDeskCallCount = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cboActionTypes = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.llbActionTypes = New System.Windows.Forms.LinkLabel
        Me.llbTotalActions = New System.Windows.Forms.LinkLabel
        Me.llbCasesClosed = New System.Windows.Forms.LinkLabel
        Me.llbTotalCases = New System.Windows.Forms.LinkLabel
        Me.llbExistingCases = New System.Windows.Forms.LinkLabel
        Me.llbNewCases = New System.Windows.Forms.LinkLabel
        Me.llbClientsAssisted = New System.Windows.Forms.LinkLabel
        Me.txtActionTypeCount = New System.Windows.Forms.TextBox
        Me.txtTotalActionCount = New System.Windows.Forms.TextBox
        Me.txtCaseClosedCount = New System.Windows.Forms.TextBox
        Me.txtTotalCaseCount = New System.Windows.Forms.TextBox
        Me.txtExistingCaseCount = New System.Windows.Forms.TextBox
        Me.txtNewCaseCount = New System.Windows.Forms.TextBox
        Me.txtClientAssistCount = New System.Windows.Forms.TextBox
        Me.llbViewNewClient = New System.Windows.Forms.LinkLabel
        Me.txtNewClientCount = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dgvCaseWork = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnExportToExcel = New System.Windows.Forms.Button
        Me.txtCount = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtCaseID = New System.Windows.Forms.TextBox
        Me.btnViewCase = New System.Windows.Forms.Button
        CType(Me.SBEAPCLIENTDATABindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SBEAPCLIENTSBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvCaseWork, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SBEAPCLIENTDATABindingSource
        '
        Me.SBEAPCLIENTDATABindingSource.DataMember = "SBEAPCLIENTDATA"
        '
        'SBEAPCLIENTSBindingSource
        '
        Me.SBEAPCLIENTSBindingSource.DataMember = "SBEAPCLIENTS"
        '
        'btnRunSBEAPReport
        '
        Me.btnRunSBEAPReport.Location = New System.Drawing.Point(196, 12)
        Me.btnRunSBEAPReport.Name = "btnRunSBEAPReport"
        Me.btnRunSBEAPReport.Size = New System.Drawing.Size(75, 23)
        Me.btnRunSBEAPReport.TabIndex = 9
        Me.btnRunSBEAPReport.Text = "Run Report"
        Me.btnRunSBEAPReport.UseVisualStyleBackColor = True
        '
        'DTPReportStartDate
        '
        Me.DTPReportStartDate.Checked = False
        Me.DTPReportStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPReportStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPReportStartDate.Location = New System.Drawing.Point(24, 13)
        Me.DTPReportStartDate.Name = "DTPReportStartDate"
        Me.DTPReportStartDate.Size = New System.Drawing.Size(93, 20)
        Me.DTPReportStartDate.TabIndex = 33
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(123, 17)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(55, 13)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "Start Date"
        '
        'DTPReportEndDate
        '
        Me.DTPReportEndDate.Checked = False
        Me.DTPReportEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPReportEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPReportEndDate.Location = New System.Drawing.Point(24, 39)
        Me.DTPReportEndDate.Name = "DTPReportEndDate"
        Me.DTPReportEndDate.Size = New System.Drawing.Size(93, 20)
        Me.DTPReportEndDate.TabIndex = 31
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(123, 43)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(52, 13)
        Me.Label17.TabIndex = 30
        Me.Label17.Text = "End Date"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.llbFrontDestCalls)
        Me.Panel1.Controls.Add(Me.txtFrontDeskCallCount)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.cboActionTypes)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.llbActionTypes)
        Me.Panel1.Controls.Add(Me.llbTotalActions)
        Me.Panel1.Controls.Add(Me.DTPReportStartDate)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.llbCasesClosed)
        Me.Panel1.Controls.Add(Me.DTPReportEndDate)
        Me.Panel1.Controls.Add(Me.llbTotalCases)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.llbExistingCases)
        Me.Panel1.Controls.Add(Me.btnRunSBEAPReport)
        Me.Panel1.Controls.Add(Me.llbNewCases)
        Me.Panel1.Controls.Add(Me.llbClientsAssisted)
        Me.Panel1.Controls.Add(Me.txtActionTypeCount)
        Me.Panel1.Controls.Add(Me.txtTotalActionCount)
        Me.Panel1.Controls.Add(Me.txtCaseClosedCount)
        Me.Panel1.Controls.Add(Me.txtTotalCaseCount)
        Me.Panel1.Controls.Add(Me.txtExistingCaseCount)
        Me.Panel1.Controls.Add(Me.txtNewCaseCount)
        Me.Panel1.Controls.Add(Me.txtClientAssistCount)
        Me.Panel1.Controls.Add(Me.llbViewNewClient)
        Me.Panel1.Controls.Add(Me.txtNewClientCount)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(352, 566)
        Me.Panel1.TabIndex = 34
        '
        'llbFrontDestCalls
        '
        Me.llbFrontDestCalls.AutoSize = True
        Me.llbFrontDestCalls.Location = New System.Drawing.Point(296, 314)
        Me.llbFrontDestCalls.Name = "llbFrontDestCalls"
        Me.llbFrontDestCalls.Size = New System.Drawing.Size(30, 13)
        Me.llbFrontDestCalls.TabIndex = 78
        Me.llbFrontDestCalls.TabStop = True
        Me.llbFrontDestCalls.Text = "View"
        '
        'txtFrontDeskCallCount
        '
        Me.txtFrontDeskCallCount.Location = New System.Drawing.Point(204, 310)
        Me.txtFrontDeskCallCount.Name = "txtFrontDeskCallCount"
        Me.txtFrontDeskCallCount.ReadOnly = True
        Me.txtFrontDeskCallCount.Size = New System.Drawing.Size(81, 20)
        Me.txtFrontDeskCallCount.TabIndex = 77
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(21, 314)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(90, 13)
        Me.Label15.TabIndex = 76
        Me.Label15.Text = "Front Desk Call(s)"
        '
        'cboActionTypes
        '
        Me.cboActionTypes.FormattingEnabled = True
        Me.cboActionTypes.Location = New System.Drawing.Point(11, 357)
        Me.cboActionTypes.Name = "cboActionTypes"
        Me.cboActionTypes.Size = New System.Drawing.Size(209, 21)
        Me.cboActionTypes.TabIndex = 75
        '
        'Button1
        '
        Me.Button1.AutoSize = True
        Me.Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button1.Location = New System.Drawing.Point(11, 424)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(162, 23)
        Me.Button1.TabIndex = 74
        Me.Button1.Text = "Populate Action Occured Date"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(21, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 13)
        Me.Label8.TabIndex = 73
        Me.Label8.Text = "Customer(s) Assisted"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(21, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 13)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "New Customer(s)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 16)
        Me.Label6.TabIndex = 71
        Me.Label6.Text = "Customer(s)"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 263)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 16)
        Me.Label16.TabIndex = 70
        Me.Label16.Text = "Action(s)"
        '
        'llbActionTypes
        '
        Me.llbActionTypes.AutoSize = True
        Me.llbActionTypes.Location = New System.Drawing.Point(318, 361)
        Me.llbActionTypes.Name = "llbActionTypes"
        Me.llbActionTypes.Size = New System.Drawing.Size(30, 13)
        Me.llbActionTypes.TabIndex = 68
        Me.llbActionTypes.TabStop = True
        Me.llbActionTypes.Text = "View"
        '
        'llbTotalActions
        '
        Me.llbTotalActions.AutoSize = True
        Me.llbTotalActions.Location = New System.Drawing.Point(296, 286)
        Me.llbTotalActions.Name = "llbTotalActions"
        Me.llbTotalActions.Size = New System.Drawing.Size(30, 13)
        Me.llbTotalActions.TabIndex = 67
        Me.llbTotalActions.TabStop = True
        Me.llbTotalActions.Text = "View"
        '
        'llbCasesClosed
        '
        Me.llbCasesClosed.AutoSize = True
        Me.llbCasesClosed.Location = New System.Drawing.Point(296, 240)
        Me.llbCasesClosed.Name = "llbCasesClosed"
        Me.llbCasesClosed.Size = New System.Drawing.Size(30, 13)
        Me.llbCasesClosed.TabIndex = 66
        Me.llbCasesClosed.TabStop = True
        Me.llbCasesClosed.Text = "View"
        '
        'llbTotalCases
        '
        Me.llbTotalCases.AutoSize = True
        Me.llbTotalCases.Location = New System.Drawing.Point(296, 217)
        Me.llbTotalCases.Name = "llbTotalCases"
        Me.llbTotalCases.Size = New System.Drawing.Size(30, 13)
        Me.llbTotalCases.TabIndex = 65
        Me.llbTotalCases.TabStop = True
        Me.llbTotalCases.Text = "View"
        '
        'llbExistingCases
        '
        Me.llbExistingCases.AutoSize = True
        Me.llbExistingCases.Location = New System.Drawing.Point(296, 193)
        Me.llbExistingCases.Name = "llbExistingCases"
        Me.llbExistingCases.Size = New System.Drawing.Size(30, 13)
        Me.llbExistingCases.TabIndex = 64
        Me.llbExistingCases.TabStop = True
        Me.llbExistingCases.Text = "View"
        '
        'llbNewCases
        '
        Me.llbNewCases.AutoSize = True
        Me.llbNewCases.Location = New System.Drawing.Point(296, 170)
        Me.llbNewCases.Name = "llbNewCases"
        Me.llbNewCases.Size = New System.Drawing.Size(30, 13)
        Me.llbNewCases.TabIndex = 63
        Me.llbNewCases.TabStop = True
        Me.llbNewCases.Text = "View"
        '
        'llbClientsAssisted
        '
        Me.llbClientsAssisted.AutoSize = True
        Me.llbClientsAssisted.Location = New System.Drawing.Point(296, 129)
        Me.llbClientsAssisted.Name = "llbClientsAssisted"
        Me.llbClientsAssisted.Size = New System.Drawing.Size(30, 13)
        Me.llbClientsAssisted.TabIndex = 61
        Me.llbClientsAssisted.TabStop = True
        Me.llbClientsAssisted.Text = "View"
        '
        'txtActionTypeCount
        '
        Me.txtActionTypeCount.Location = New System.Drawing.Point(226, 357)
        Me.txtActionTypeCount.Name = "txtActionTypeCount"
        Me.txtActionTypeCount.ReadOnly = True
        Me.txtActionTypeCount.Size = New System.Drawing.Size(81, 20)
        Me.txtActionTypeCount.TabIndex = 59
        '
        'txtTotalActionCount
        '
        Me.txtTotalActionCount.Location = New System.Drawing.Point(204, 282)
        Me.txtTotalActionCount.Name = "txtTotalActionCount"
        Me.txtTotalActionCount.ReadOnly = True
        Me.txtTotalActionCount.Size = New System.Drawing.Size(81, 20)
        Me.txtTotalActionCount.TabIndex = 57
        '
        'txtCaseClosedCount
        '
        Me.txtCaseClosedCount.Location = New System.Drawing.Point(204, 236)
        Me.txtCaseClosedCount.Name = "txtCaseClosedCount"
        Me.txtCaseClosedCount.ReadOnly = True
        Me.txtCaseClosedCount.Size = New System.Drawing.Size(81, 20)
        Me.txtCaseClosedCount.TabIndex = 55
        '
        'txtTotalCaseCount
        '
        Me.txtTotalCaseCount.Location = New System.Drawing.Point(204, 213)
        Me.txtTotalCaseCount.Name = "txtTotalCaseCount"
        Me.txtTotalCaseCount.ReadOnly = True
        Me.txtTotalCaseCount.Size = New System.Drawing.Size(81, 20)
        Me.txtTotalCaseCount.TabIndex = 53
        '
        'txtExistingCaseCount
        '
        Me.txtExistingCaseCount.Location = New System.Drawing.Point(204, 189)
        Me.txtExistingCaseCount.Name = "txtExistingCaseCount"
        Me.txtExistingCaseCount.ReadOnly = True
        Me.txtExistingCaseCount.Size = New System.Drawing.Size(81, 20)
        Me.txtExistingCaseCount.TabIndex = 51
        '
        'txtNewCaseCount
        '
        Me.txtNewCaseCount.Location = New System.Drawing.Point(204, 166)
        Me.txtNewCaseCount.Name = "txtNewCaseCount"
        Me.txtNewCaseCount.ReadOnly = True
        Me.txtNewCaseCount.Size = New System.Drawing.Size(81, 20)
        Me.txtNewCaseCount.TabIndex = 49
        '
        'txtClientAssistCount
        '
        Me.txtClientAssistCount.Location = New System.Drawing.Point(204, 125)
        Me.txtClientAssistCount.Name = "txtClientAssistCount"
        Me.txtClientAssistCount.ReadOnly = True
        Me.txtClientAssistCount.Size = New System.Drawing.Size(81, 20)
        Me.txtClientAssistCount.TabIndex = 45
        '
        'llbViewNewClient
        '
        Me.llbViewNewClient.AutoSize = True
        Me.llbViewNewClient.Location = New System.Drawing.Point(296, 107)
        Me.llbViewNewClient.Name = "llbViewNewClient"
        Me.llbViewNewClient.Size = New System.Drawing.Size(30, 13)
        Me.llbViewNewClient.TabIndex = 44
        Me.llbViewNewClient.TabStop = True
        Me.llbViewNewClient.Text = "View"
        '
        'txtNewClientCount
        '
        Me.txtNewClientCount.Location = New System.Drawing.Point(204, 103)
        Me.txtNewClientCount.Name = "txtNewClientCount"
        Me.txtNewClientCount.ReadOnly = True
        Me.txtNewClientCount.Size = New System.Drawing.Size(81, 20)
        Me.txtNewClientCount.TabIndex = 42
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(33, 216)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 13)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "Total Case(s)"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(21, 239)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 13)
        Me.Label11.TabIndex = 40
        Me.Label11.Text = "Case(s) Closed"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(21, 286)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "Total Action(s)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(21, 339)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 13)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "Action Type(s)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 193)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Existing Case(s)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 170)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "New Case(s)"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(8, 150)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(62, 16)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Case(s)"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(201, 199)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(131, 13)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "                               "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvCaseWork)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(352, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(541, 566)
        Me.GroupBox1.TabIndex = 35
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "View Case(s)"
        '
        'dgvCaseWork
        '
        Me.dgvCaseWork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCaseWork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCaseWork.Location = New System.Drawing.Point(3, 59)
        Me.dgvCaseWork.Name = "dgvCaseWork"
        Me.dgvCaseWork.Size = New System.Drawing.Size(535, 504)
        Me.dgvCaseWork.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnExportToExcel)
        Me.Panel2.Controls.Add(Me.txtCount)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.txtCaseID)
        Me.Panel2.Controls.Add(Me.btnViewCase)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 16)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(535, 43)
        Me.Panel2.TabIndex = 0
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.AutoSize = True
        Me.btnExportToExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportToExcel.Location = New System.Drawing.Point(324, 6)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(88, 23)
        Me.btnExportToExcel.TabIndex = 46
        Me.btnExportToExcel.Text = "Export to Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(256, 8)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(49, 20)
        Me.txtCount.TabIndex = 45
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(215, 11)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(35, 13)
        Me.Label14.TabIndex = 44
        Me.Label14.Text = "Count"
        '
        'txtCaseID
        '
        Me.txtCaseID.Location = New System.Drawing.Point(102, 8)
        Me.txtCaseID.Name = "txtCaseID"
        Me.txtCaseID.Size = New System.Drawing.Size(81, 20)
        Me.txtCaseID.TabIndex = 43
        '
        'btnViewCase
        '
        Me.btnViewCase.Location = New System.Drawing.Point(21, 8)
        Me.btnViewCase.Name = "btnViewCase"
        Me.btnViewCase.Size = New System.Drawing.Size(75, 23)
        Me.btnViewCase.TabIndex = 10
        Me.btnViewCase.Text = "View Case"
        Me.btnViewCase.UseVisualStyleBackColor = True
        '
        'SBEAPReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(893, 566)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "SBEAPReports"
        Me.Text = "SBEAP Reports"
        CType(Me.SBEAPCLIENTDATABindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SBEAPCLIENTSBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvCaseWork, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRunSBEAPReport As System.Windows.Forms.Button
    Friend WithEvents DTPReportStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents DTPReportEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents llbActionTypes As System.Windows.Forms.LinkLabel
    Friend WithEvents llbTotalActions As System.Windows.Forms.LinkLabel
    Friend WithEvents llbCasesClosed As System.Windows.Forms.LinkLabel
    Friend WithEvents llbTotalCases As System.Windows.Forms.LinkLabel
    Friend WithEvents llbExistingCases As System.Windows.Forms.LinkLabel
    Friend WithEvents llbNewCases As System.Windows.Forms.LinkLabel
    Friend WithEvents llbClientsAssisted As System.Windows.Forms.LinkLabel
    Friend WithEvents txtActionTypeCount As System.Windows.Forms.TextBox
    Friend WithEvents txtCaseClosedCount As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCaseCount As System.Windows.Forms.TextBox
    Friend WithEvents txtExistingCaseCount As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCaseCount As System.Windows.Forms.TextBox
    Friend WithEvents txtClientAssistCount As System.Windows.Forms.TextBox
    Friend WithEvents llbViewNewClient As System.Windows.Forms.LinkLabel
    Friend WithEvents txtNewClientCount As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtCaseID As System.Windows.Forms.TextBox
    Friend WithEvents btnViewCase As System.Windows.Forms.Button
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents dgvCaseWork As System.Windows.Forms.DataGridView
    Friend WithEvents txtTotalActionCount As System.Windows.Forms.TextBox
    Friend WithEvents SBEAPCLIENTDATABindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents dsSBEAP As SBEAP2.dsSBEAP
    Friend WithEvents SBEAPCLIENTSBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents SBEAPCLIENTDATATableAdapter As SBEAP2.dsSBEAPTableAdapters.SBEAPCLIENTDATATableAdapter
    'Friend WithEvents SBEAPCLIENTSTableAdapter As SBEAP2.dsSBEAPTableAdapters.SBEAPCLIENTSTableAdapter
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cboActionTypes As System.Windows.Forms.ComboBox
    Friend WithEvents llbFrontDestCalls As System.Windows.Forms.LinkLabel
    Friend WithEvents txtFrontDeskCallCount As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label

End Class
