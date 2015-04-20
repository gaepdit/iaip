<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DMUDeveloperTool
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
        Me.TCDMUTools = New System.Windows.Forms.TabControl
        Me.TPErrorLog = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.rdbNoLimit = New System.Windows.Forms.RadioButton
        Me.rdbLast60days = New System.Windows.Forms.RadioButton
        Me.rdbLast30Days = New System.Windows.Forms.RadioButton
        Me.Label61 = New System.Windows.Forms.Label
        Me.txtErrorCount = New System.Windows.Forms.TextBox
        Me.txtErrorNumber = New System.Windows.Forms.TextBox
        Me.btnFilterErrors = New System.Windows.Forms.Button
        Me.btnSaveError = New System.Windows.Forms.Button
        Me.rdbViewResolvedErrors = New System.Windows.Forms.RadioButton
        Me.Label62 = New System.Windows.Forms.Label
        Me.rdbViewUnresolvedErrors = New System.Windows.Forms.RadioButton
        Me.Label64 = New System.Windows.Forms.Label
        Me.rdbViewAllErrors = New System.Windows.Forms.RadioButton
        Me.Label65 = New System.Windows.Forms.Label
        Me.txtErrorSolution = New System.Windows.Forms.TextBox
        Me.Label66 = New System.Windows.Forms.Label
        Me.txtErrorMessage = New System.Windows.Forms.TextBox
        Me.Label67 = New System.Windows.Forms.Label
        Me.txtErrorDate = New System.Windows.Forms.TextBox
        Me.txtErrorUser = New System.Windows.Forms.TextBox
        Me.txtErrorLocation = New System.Windows.Forms.TextBox
        Me.dgvErrorList = New System.Windows.Forms.DataGridView
        Me.TPWebErrorLog = New System.Windows.Forms.TabPage
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label95 = New System.Windows.Forms.Label
        Me.Label96 = New System.Windows.Forms.Label
        Me.txtWebErrorNumber = New System.Windows.Forms.TextBox
        Me.txtIPAddress = New System.Windows.Forms.TextBox
        Me.btnSaveWebErrorSolution = New System.Windows.Forms.Button
        Me.txtWebErrorCount = New System.Windows.Forms.TextBox
        Me.Label91 = New System.Windows.Forms.Label
        Me.btnFilterWebErrors = New System.Windows.Forms.Button
        Me.Label90 = New System.Windows.Forms.Label
        Me.rdbResolvedWebErrors = New System.Windows.Forms.RadioButton
        Me.Label88 = New System.Windows.Forms.Label
        Me.rdbUnresolvedWebErrors = New System.Windows.Forms.RadioButton
        Me.Label71 = New System.Windows.Forms.Label
        Me.rdbAllWebErrors = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtWebErrorSolution = New System.Windows.Forms.TextBox
        Me.txtWebErrorUser = New System.Windows.Forms.TextBox
        Me.txtWebErrorMessage = New System.Windows.Forms.TextBox
        Me.txtWebErrorLocation = New System.Windows.Forms.TextBox
        Me.txtWebErrorDate = New System.Windows.Forms.TextBox
        Me.dgrWebErrorList = New System.Windows.Forms.DataGrid
        Me.TCDMUTools.SuspendLayout()
        Me.TPErrorLog.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.dgvErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPWebErrorLog.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgrWebErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TCDMUTools
        '
        Me.TCDMUTools.Controls.Add(Me.TPErrorLog)
        Me.TCDMUTools.Controls.Add(Me.TPWebErrorLog)
        Me.TCDMUTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDMUTools.Location = New System.Drawing.Point(0, 0)
        Me.TCDMUTools.Name = "TCDMUTools"
        Me.TCDMUTools.SelectedIndex = 0
        Me.TCDMUTools.Size = New System.Drawing.Size(792, 687)
        Me.TCDMUTools.TabIndex = 256
        '
        'TPErrorLog
        '
        Me.TPErrorLog.Controls.Add(Me.GroupBox3)
        Me.TPErrorLog.Controls.Add(Me.dgvErrorList)
        Me.TPErrorLog.Location = New System.Drawing.Point(4, 22)
        Me.TPErrorLog.Name = "TPErrorLog"
        Me.TPErrorLog.Size = New System.Drawing.Size(784, 661)
        Me.TPErrorLog.TabIndex = 7
        Me.TPErrorLog.Text = "IAIP Error Log"
        Me.TPErrorLog.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 242)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(784, 419)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "IAIP Error Log"
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Label61)
        Me.Panel4.Controls.Add(Me.txtErrorCount)
        Me.Panel4.Controls.Add(Me.txtErrorNumber)
        Me.Panel4.Controls.Add(Me.btnFilterErrors)
        Me.Panel4.Controls.Add(Me.btnSaveError)
        Me.Panel4.Controls.Add(Me.rdbViewResolvedErrors)
        Me.Panel4.Controls.Add(Me.Label62)
        Me.Panel4.Controls.Add(Me.rdbViewUnresolvedErrors)
        Me.Panel4.Controls.Add(Me.Label64)
        Me.Panel4.Controls.Add(Me.rdbViewAllErrors)
        Me.Panel4.Controls.Add(Me.Label65)
        Me.Panel4.Controls.Add(Me.txtErrorSolution)
        Me.Panel4.Controls.Add(Me.Label66)
        Me.Panel4.Controls.Add(Me.txtErrorMessage)
        Me.Panel4.Controls.Add(Me.Label67)
        Me.Panel4.Controls.Add(Me.txtErrorDate)
        Me.Panel4.Controls.Add(Me.txtErrorUser)
        Me.Panel4.Controls.Add(Me.txtErrorLocation)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 16)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(778, 400)
        Me.Panel4.TabIndex = 18
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rdbNoLimit)
        Me.Panel6.Controls.Add(Me.rdbLast60days)
        Me.Panel6.Controls.Add(Me.rdbLast30Days)
        Me.Panel6.Location = New System.Drawing.Point(243, 239)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(104, 68)
        Me.Panel6.TabIndex = 19
        '
        'rdbNoLimit
        '
        Me.rdbNoLimit.AutoSize = True
        Me.rdbNoLimit.Location = New System.Drawing.Point(3, 44)
        Me.rdbNoLimit.Name = "rdbNoLimit"
        Me.rdbNoLimit.Size = New System.Drawing.Size(59, 17)
        Me.rdbNoLimit.TabIndex = 20
        Me.rdbNoLimit.TabStop = True
        Me.rdbNoLimit.Text = "No limit"
        Me.rdbNoLimit.UseVisualStyleBackColor = True
        '
        'rdbLast60days
        '
        Me.rdbLast60days.AutoSize = True
        Me.rdbLast60days.Location = New System.Drawing.Point(3, 25)
        Me.rdbLast60days.Name = "rdbLast60days"
        Me.rdbLast60days.Size = New System.Drawing.Size(85, 17)
        Me.rdbLast60days.TabIndex = 19
        Me.rdbLast60days.TabStop = True
        Me.rdbLast60days.Text = "Last 60 days"
        Me.rdbLast60days.UseVisualStyleBackColor = True
        '
        'rdbLast30Days
        '
        Me.rdbLast30Days.AutoSize = True
        Me.rdbLast30Days.Checked = True
        Me.rdbLast30Days.Location = New System.Drawing.Point(3, 4)
        Me.rdbLast30Days.Name = "rdbLast30Days"
        Me.rdbLast30Days.Size = New System.Drawing.Size(85, 17)
        Me.rdbLast30Days.TabIndex = 18
        Me.rdbLast30Days.TabStop = True
        Me.rdbLast30Days.Text = "Last 30 days"
        Me.rdbLast30Days.UseVisualStyleBackColor = True
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(11, 10)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(69, 13)
        Me.Label61.TabIndex = 0
        Me.Label61.Text = "Error Number"
        '
        'txtErrorCount
        '
        Me.txtErrorCount.Location = New System.Drawing.Point(619, 7)
        Me.txtErrorCount.Name = "txtErrorCount"
        Me.txtErrorCount.ReadOnly = True
        Me.txtErrorCount.Size = New System.Drawing.Size(34, 20)
        Me.txtErrorCount.TabIndex = 17
        Me.txtErrorCount.Text = "0"
        '
        'txtErrorNumber
        '
        Me.txtErrorNumber.Location = New System.Drawing.Point(91, 8)
        Me.txtErrorNumber.Name = "txtErrorNumber"
        Me.txtErrorNumber.ReadOnly = True
        Me.txtErrorNumber.Size = New System.Drawing.Size(60, 20)
        Me.txtErrorNumber.TabIndex = 1
        '
        'btnFilterErrors
        '
        Me.btnFilterErrors.Location = New System.Drawing.Point(157, 239)
        Me.btnFilterErrors.Name = "btnFilterErrors"
        Me.btnFilterErrors.Size = New System.Drawing.Size(63, 20)
        Me.btnFilterErrors.TabIndex = 16
        Me.btnFilterErrors.Text = "Filter"
        '
        'btnSaveError
        '
        Me.btnSaveError.Location = New System.Drawing.Point(11, 190)
        Me.btnSaveError.Name = "btnSaveError"
        Me.btnSaveError.Size = New System.Drawing.Size(62, 20)
        Me.btnSaveError.TabIndex = 2
        Me.btnSaveError.Text = "Save"
        '
        'rdbViewResolvedErrors
        '
        Me.rdbViewResolvedErrors.AutoSize = True
        Me.rdbViewResolvedErrors.Location = New System.Drawing.Point(17, 280)
        Me.rdbViewResolvedErrors.Name = "rdbViewResolvedErrors"
        Me.rdbViewResolvedErrors.Size = New System.Drawing.Size(121, 17)
        Me.rdbViewResolvedErrors.TabIndex = 15
        Me.rdbViewResolvedErrors.Text = "View Resolved Error"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(171, 10)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(29, 13)
        Me.Label62.TabIndex = 3
        Me.Label62.Text = "User"
        '
        'rdbViewUnresolvedErrors
        '
        Me.rdbViewUnresolvedErrors.AutoSize = True
        Me.rdbViewUnresolvedErrors.Checked = True
        Me.rdbViewUnresolvedErrors.Location = New System.Drawing.Point(17, 259)
        Me.rdbViewUnresolvedErrors.Name = "rdbViewUnresolvedErrors"
        Me.rdbViewUnresolvedErrors.Size = New System.Drawing.Size(135, 17)
        Me.rdbViewUnresolvedErrors.TabIndex = 14
        Me.rdbViewUnresolvedErrors.TabStop = True
        Me.rdbViewUnresolvedErrors.Text = "View Unresolved Errors"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(11, 32)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(76, 13)
        Me.Label64.TabIndex = 4
        Me.Label64.Text = "Error Location "
        '
        'rdbViewAllErrors
        '
        Me.rdbViewAllErrors.AutoSize = True
        Me.rdbViewAllErrors.Location = New System.Drawing.Point(17, 239)
        Me.rdbViewAllErrors.Name = "rdbViewAllErrors"
        Me.rdbViewAllErrors.Size = New System.Drawing.Size(92, 17)
        Me.rdbViewAllErrors.TabIndex = 13
        Me.rdbViewAllErrors.Text = "View All Errors"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(11, 58)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(75, 13)
        Me.Label65.TabIndex = 5
        Me.Label65.Text = "Error Message"
        '
        'txtErrorSolution
        '
        Me.txtErrorSolution.AcceptsReturn = True
        Me.txtErrorSolution.Location = New System.Drawing.Point(91, 169)
        Me.txtErrorSolution.MaxLength = 4000
        Me.txtErrorSolution.Multiline = True
        Me.txtErrorSolution.Name = "txtErrorSolution"
        Me.txtErrorSolution.Size = New System.Drawing.Size(546, 56)
        Me.txtErrorSolution.TabIndex = 12
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(11, 169)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(73, 13)
        Me.Label66.TabIndex = 6
        Me.Label66.Text = "Error Solution "
        '
        'txtErrorMessage
        '
        Me.txtErrorMessage.AcceptsReturn = True
        Me.txtErrorMessage.Location = New System.Drawing.Point(91, 58)
        Me.txtErrorMessage.Multiline = True
        Me.txtErrorMessage.Name = "txtErrorMessage"
        Me.txtErrorMessage.ReadOnly = True
        Me.txtErrorMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtErrorMessage.Size = New System.Drawing.Size(553, 104)
        Me.txtErrorMessage.TabIndex = 11
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(384, 32)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(55, 13)
        Me.Label67.TabIndex = 7
        Me.Label67.Text = "Error Date"
        '
        'txtErrorDate
        '
        Me.txtErrorDate.Location = New System.Drawing.Point(471, 31)
        Me.txtErrorDate.Name = "txtErrorDate"
        Me.txtErrorDate.ReadOnly = True
        Me.txtErrorDate.Size = New System.Drawing.Size(106, 20)
        Me.txtErrorDate.TabIndex = 10
        '
        'txtErrorUser
        '
        Me.txtErrorUser.Location = New System.Drawing.Point(204, 8)
        Me.txtErrorUser.Name = "txtErrorUser"
        Me.txtErrorUser.ReadOnly = True
        Me.txtErrorUser.Size = New System.Drawing.Size(193, 20)
        Me.txtErrorUser.TabIndex = 8
        '
        'txtErrorLocation
        '
        Me.txtErrorLocation.Location = New System.Drawing.Point(91, 31)
        Me.txtErrorLocation.Name = "txtErrorLocation"
        Me.txtErrorLocation.ReadOnly = True
        Me.txtErrorLocation.Size = New System.Drawing.Size(266, 20)
        Me.txtErrorLocation.TabIndex = 9
        '
        'dgvErrorList
        '
        Me.dgvErrorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvErrorList.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvErrorList.Location = New System.Drawing.Point(0, 0)
        Me.dgvErrorList.Name = "dgvErrorList"
        Me.dgvErrorList.Size = New System.Drawing.Size(784, 242)
        Me.dgvErrorList.TabIndex = 21
        '
        'TPWebErrorLog
        '
        Me.TPWebErrorLog.Controls.Add(Me.GroupBox4)
        Me.TPWebErrorLog.Controls.Add(Me.dgrWebErrorList)
        Me.TPWebErrorLog.Location = New System.Drawing.Point(4, 22)
        Me.TPWebErrorLog.Name = "TPWebErrorLog"
        Me.TPWebErrorLog.Size = New System.Drawing.Size(784, 661)
        Me.TPWebErrorLog.TabIndex = 8
        Me.TPWebErrorLog.Text = "Web Site Error Log"
        Me.TPWebErrorLog.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Panel5)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 241)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(784, 420)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Web Error Log"
        '
        'Panel5
        '
        Me.Panel5.AutoScroll = True
        Me.Panel5.Controls.Add(Me.Label95)
        Me.Panel5.Controls.Add(Me.Label96)
        Me.Panel5.Controls.Add(Me.txtWebErrorNumber)
        Me.Panel5.Controls.Add(Me.txtIPAddress)
        Me.Panel5.Controls.Add(Me.btnSaveWebErrorSolution)
        Me.Panel5.Controls.Add(Me.txtWebErrorCount)
        Me.Panel5.Controls.Add(Me.Label91)
        Me.Panel5.Controls.Add(Me.btnFilterWebErrors)
        Me.Panel5.Controls.Add(Me.Label90)
        Me.Panel5.Controls.Add(Me.rdbResolvedWebErrors)
        Me.Panel5.Controls.Add(Me.Label88)
        Me.Panel5.Controls.Add(Me.rdbUnresolvedWebErrors)
        Me.Panel5.Controls.Add(Me.Label71)
        Me.Panel5.Controls.Add(Me.rdbAllWebErrors)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.txtWebErrorSolution)
        Me.Panel5.Controls.Add(Me.txtWebErrorUser)
        Me.Panel5.Controls.Add(Me.txtWebErrorMessage)
        Me.Panel5.Controls.Add(Me.txtWebErrorLocation)
        Me.Panel5.Controls.Add(Me.txtWebErrorDate)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 16)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(778, 401)
        Me.Panel5.TabIndex = 20
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(8, 11)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(69, 13)
        Me.Label95.TabIndex = 0
        Me.Label95.Text = "Error Number"
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(368, 11)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(58, 13)
        Me.Label96.TabIndex = 19
        Me.Label96.Text = "IP Address"
        '
        'txtWebErrorNumber
        '
        Me.txtWebErrorNumber.Location = New System.Drawing.Point(88, 9)
        Me.txtWebErrorNumber.Name = "txtWebErrorNumber"
        Me.txtWebErrorNumber.ReadOnly = True
        Me.txtWebErrorNumber.Size = New System.Drawing.Size(60, 20)
        Me.txtWebErrorNumber.TabIndex = 1
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Location = New System.Drawing.Point(441, 9)
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.ReadOnly = True
        Me.txtIPAddress.Size = New System.Drawing.Size(140, 20)
        Me.txtIPAddress.TabIndex = 18
        '
        'btnSaveWebErrorSolution
        '
        Me.btnSaveWebErrorSolution.Location = New System.Drawing.Point(8, 191)
        Me.btnSaveWebErrorSolution.Name = "btnSaveWebErrorSolution"
        Me.btnSaveWebErrorSolution.Size = New System.Drawing.Size(62, 20)
        Me.btnSaveWebErrorSolution.TabIndex = 2
        Me.btnSaveWebErrorSolution.Text = "Save"
        '
        'txtWebErrorCount
        '
        Me.txtWebErrorCount.Location = New System.Drawing.Point(614, 7)
        Me.txtWebErrorCount.Name = "txtWebErrorCount"
        Me.txtWebErrorCount.ReadOnly = True
        Me.txtWebErrorCount.Size = New System.Drawing.Size(34, 20)
        Me.txtWebErrorCount.TabIndex = 17
        Me.txtWebErrorCount.Text = "0"
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.Location = New System.Drawing.Point(168, 11)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(29, 13)
        Me.Label91.TabIndex = 3
        Me.Label91.Text = "User"
        '
        'btnFilterWebErrors
        '
        Me.btnFilterWebErrors.Location = New System.Drawing.Point(154, 240)
        Me.btnFilterWebErrors.Name = "btnFilterWebErrors"
        Me.btnFilterWebErrors.Size = New System.Drawing.Size(63, 20)
        Me.btnFilterWebErrors.TabIndex = 16
        Me.btnFilterWebErrors.Text = "Filter"
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Location = New System.Drawing.Point(8, 33)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(76, 13)
        Me.Label90.TabIndex = 4
        Me.Label90.Text = "Error Location "
        '
        'rdbResolvedWebErrors
        '
        Me.rdbResolvedWebErrors.AutoSize = True
        Me.rdbResolvedWebErrors.Location = New System.Drawing.Point(14, 281)
        Me.rdbResolvedWebErrors.Name = "rdbResolvedWebErrors"
        Me.rdbResolvedWebErrors.Size = New System.Drawing.Size(121, 17)
        Me.rdbResolvedWebErrors.TabIndex = 15
        Me.rdbResolvedWebErrors.Text = "View Resolved Error"
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Location = New System.Drawing.Point(8, 59)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(75, 13)
        Me.Label88.TabIndex = 5
        Me.Label88.Text = "Error Message"
        '
        'rdbUnresolvedWebErrors
        '
        Me.rdbUnresolvedWebErrors.AutoSize = True
        Me.rdbUnresolvedWebErrors.Location = New System.Drawing.Point(14, 260)
        Me.rdbUnresolvedWebErrors.Name = "rdbUnresolvedWebErrors"
        Me.rdbUnresolvedWebErrors.Size = New System.Drawing.Size(135, 17)
        Me.rdbUnresolvedWebErrors.TabIndex = 14
        Me.rdbUnresolvedWebErrors.Text = "View Unresolved Errors"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(8, 170)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(73, 13)
        Me.Label71.TabIndex = 6
        Me.Label71.Text = "Error Solution "
        '
        'rdbAllWebErrors
        '
        Me.rdbAllWebErrors.AutoSize = True
        Me.rdbAllWebErrors.Location = New System.Drawing.Point(14, 240)
        Me.rdbAllWebErrors.Name = "rdbAllWebErrors"
        Me.rdbAllWebErrors.Size = New System.Drawing.Size(92, 17)
        Me.rdbAllWebErrors.TabIndex = 13
        Me.rdbAllWebErrors.Text = "View All Errors"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(381, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Error Date"
        '
        'txtWebErrorSolution
        '
        Me.txtWebErrorSolution.AcceptsReturn = True
        Me.txtWebErrorSolution.Location = New System.Drawing.Point(88, 170)
        Me.txtWebErrorSolution.MaxLength = 4000
        Me.txtWebErrorSolution.Multiline = True
        Me.txtWebErrorSolution.Name = "txtWebErrorSolution"
        Me.txtWebErrorSolution.Size = New System.Drawing.Size(546, 56)
        Me.txtWebErrorSolution.TabIndex = 12
        '
        'txtWebErrorUser
        '
        Me.txtWebErrorUser.Location = New System.Drawing.Point(201, 9)
        Me.txtWebErrorUser.Name = "txtWebErrorUser"
        Me.txtWebErrorUser.ReadOnly = True
        Me.txtWebErrorUser.Size = New System.Drawing.Size(153, 20)
        Me.txtWebErrorUser.TabIndex = 8
        '
        'txtWebErrorMessage
        '
        Me.txtWebErrorMessage.AcceptsReturn = True
        Me.txtWebErrorMessage.Location = New System.Drawing.Point(88, 59)
        Me.txtWebErrorMessage.Multiline = True
        Me.txtWebErrorMessage.Name = "txtWebErrorMessage"
        Me.txtWebErrorMessage.ReadOnly = True
        Me.txtWebErrorMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWebErrorMessage.Size = New System.Drawing.Size(553, 104)
        Me.txtWebErrorMessage.TabIndex = 11
        '
        'txtWebErrorLocation
        '
        Me.txtWebErrorLocation.Location = New System.Drawing.Point(88, 32)
        Me.txtWebErrorLocation.Name = "txtWebErrorLocation"
        Me.txtWebErrorLocation.ReadOnly = True
        Me.txtWebErrorLocation.Size = New System.Drawing.Size(266, 20)
        Me.txtWebErrorLocation.TabIndex = 9
        '
        'txtWebErrorDate
        '
        Me.txtWebErrorDate.Location = New System.Drawing.Point(468, 32)
        Me.txtWebErrorDate.Name = "txtWebErrorDate"
        Me.txtWebErrorDate.ReadOnly = True
        Me.txtWebErrorDate.Size = New System.Drawing.Size(106, 20)
        Me.txtWebErrorDate.TabIndex = 10
        '
        'dgrWebErrorList
        '
        Me.dgrWebErrorList.DataMember = ""
        Me.dgrWebErrorList.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgrWebErrorList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrWebErrorList.Location = New System.Drawing.Point(0, 0)
        Me.dgrWebErrorList.Name = "dgrWebErrorList"
        Me.dgrWebErrorList.ReadOnly = True
        Me.dgrWebErrorList.Size = New System.Drawing.Size(784, 241)
        Me.dgrWebErrorList.TabIndex = 2
        '
        'DMUDeveloperTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 687)
        Me.Controls.Add(Me.TCDMUTools)
        Me.Name = "DMUDeveloperTool"
        Me.Text = "DMU Error Logs"
        Me.TCDMUTools.ResumeLayout(False)
        Me.TPErrorLog.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.dgvErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPWebErrorLog.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgrWebErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TCDMUTools As System.Windows.Forms.TabControl
    Friend WithEvents TPWebErrorLog As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorCount As System.Windows.Forms.TextBox
    Friend WithEvents btnFilterWebErrors As System.Windows.Forms.Button
    Friend WithEvents rdbResolvedWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnresolvedWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAllWebErrors As System.Windows.Forms.RadioButton
    Friend WithEvents txtWebErrorSolution As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorDate As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtWebErrorUser As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents btnSaveWebErrorSolution As System.Windows.Forms.Button
    Friend WithEvents txtWebErrorNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents dgrWebErrorList As System.Windows.Forms.DataGrid
    Friend WithEvents TPErrorLog As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtErrorCount As System.Windows.Forms.TextBox
    Friend WithEvents btnFilterErrors As System.Windows.Forms.Button
    Friend WithEvents rdbViewResolvedErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbViewUnresolvedErrors As System.Windows.Forms.RadioButton
    Friend WithEvents rdbViewAllErrors As System.Windows.Forms.RadioButton
    Friend WithEvents txtErrorSolution As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorDate As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorUser As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents btnSaveError As System.Windows.Forms.Button
    Friend WithEvents txtErrorNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rdbNoLimit As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLast60days As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLast30Days As System.Windows.Forms.RadioButton
    Friend WithEvents dgvErrorList As System.Windows.Forms.DataGridView
End Class
