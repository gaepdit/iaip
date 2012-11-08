<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DMUTool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DMUTool))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.SCDMUTool = New System.Windows.Forms.SplitContainer
        Me.txtSQL = New System.Windows.Forms.TextBox
        Me.Button21 = New System.Windows.Forms.Button
        Me.Button20 = New System.Windows.Forms.Button
        Me.Button19 = New System.Windows.Forms.Button
        Me.Button18 = New System.Windows.Forms.Button
        Me.Button17 = New System.Windows.Forms.Button
        Me.Button16 = New System.Windows.Forms.Button
        Me.Button15 = New System.Windows.Forms.Button
        Me.Button14 = New System.Windows.Forms.Button
        Me.Button13 = New System.Windows.Forms.Button
        Me.Button12 = New System.Windows.Forms.Button
        Me.Button11 = New System.Windows.Forms.Button
        Me.Button10 = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnUpdateComplianceContacts = New System.Windows.Forms.Button
        Me.btnFixStacks = New System.Windows.Forms.Button
        Me.btnFixInvoice = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtNewAIRS = New System.Windows.Forms.TextBox
        Me.txtOldAIRS = New System.Windows.Forms.TextBox
        Me.btnMoveAIRSData = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnUpdateVersionNumber = New System.Windows.Forms.Button
        Me.txtVersionNumber = New System.Windows.Forms.TextBox
        Me.txtACCNumber = New System.Windows.Forms.TextBox
        Me.btnDeleteACC = New System.Windows.Forms.Button
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox
        Me.btnDeleteEnforcement = New System.Windows.Forms.Button
        Me.btnClearSQL = New System.Windows.Forms.Button
        Me.btnRunSQL = New System.Windows.Forms.Button
        Me.Button22 = New System.Windows.Forms.Button
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SCDMUTool.Panel1.SuspendLayout()
        Me.SCDMUTool.Panel2.SuspendLayout()
        Me.SCDMUTool.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(715, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
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
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 463)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(715, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(692, 17)
        Me.Panel1.Spring = True
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
        'SCDMUTool
        '
        Me.SCDMUTool.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCDMUTool.Location = New System.Drawing.Point(0, 25)
        Me.SCDMUTool.Name = "SCDMUTool"
        Me.SCDMUTool.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SCDMUTool.Panel1
        '
        Me.SCDMUTool.Panel1.Controls.Add(Me.txtSQL)
        '
        'SCDMUTool.Panel2
        '
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button22)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button21)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button20)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button19)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button18)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button17)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button16)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button15)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button14)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button13)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button12)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button11)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button10)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button9)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button8)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button7)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button6)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button5)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button4)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button3)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button2)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnUpdateComplianceContacts)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnFixStacks)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnFixInvoice)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Label2)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Label1)
        Me.SCDMUTool.Panel2.Controls.Add(Me.txtNewAIRS)
        Me.SCDMUTool.Panel2.Controls.Add(Me.txtOldAIRS)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnMoveAIRSData)
        Me.SCDMUTool.Panel2.Controls.Add(Me.Button1)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnUpdateVersionNumber)
        Me.SCDMUTool.Panel2.Controls.Add(Me.txtVersionNumber)
        Me.SCDMUTool.Panel2.Controls.Add(Me.txtACCNumber)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnDeleteACC)
        Me.SCDMUTool.Panel2.Controls.Add(Me.txtEnforcementNumber)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnDeleteEnforcement)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnClearSQL)
        Me.SCDMUTool.Panel2.Controls.Add(Me.btnRunSQL)
        Me.SCDMUTool.Size = New System.Drawing.Size(715, 438)
        Me.SCDMUTool.SplitterDistance = 274
        Me.SCDMUTool.TabIndex = 2
        '
        'txtSQL
        '
        Me.txtSQL.AcceptsReturn = True
        Me.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSQL.Location = New System.Drawing.Point(0, 0)
        Me.txtSQL.Multiline = True
        Me.txtSQL.Name = "txtSQL"
        Me.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSQL.Size = New System.Drawing.Size(715, 274)
        Me.txtSQL.TabIndex = 0
        '
        'Button21
        '
        Me.Button21.Location = New System.Drawing.Point(594, 57)
        Me.Button21.Name = "Button21"
        Me.Button21.Size = New System.Drawing.Size(15, 22)
        Me.Button21.TabIndex = 42
        Me.Button21.Text = "Button21"
        Me.Button21.UseVisualStyleBackColor = True
        Me.Button21.Visible = False
        '
        'Button20
        '
        Me.Button20.Location = New System.Drawing.Point(612, 57)
        Me.Button20.Name = "Button20"
        Me.Button20.Size = New System.Drawing.Size(17, 23)
        Me.Button20.TabIndex = 41
        Me.Button20.Text = "Button20"
        Me.Button20.UseVisualStyleBackColor = True
        Me.Button20.Visible = False
        '
        'Button19
        '
        Me.Button19.Location = New System.Drawing.Point(594, 80)
        Me.Button19.Name = "Button19"
        Me.Button19.Size = New System.Drawing.Size(17, 23)
        Me.Button19.TabIndex = 40
        Me.Button19.Text = "Button19"
        Me.Button19.UseVisualStyleBackColor = True
        Me.Button19.Visible = False
        '
        'Button18
        '
        Me.Button18.Location = New System.Drawing.Point(612, 80)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(17, 23)
        Me.Button18.TabIndex = 39
        Me.Button18.Text = "Button18"
        Me.Button18.UseVisualStyleBackColor = True
        Me.Button18.Visible = False
        '
        'Button17
        '
        Me.Button17.Location = New System.Drawing.Point(631, 57)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(17, 23)
        Me.Button17.TabIndex = 38
        Me.Button17.Text = "Button17"
        Me.Button17.UseVisualStyleBackColor = True
        Me.Button17.Visible = False
        '
        'Button16
        '
        Me.Button16.Location = New System.Drawing.Point(594, 102)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(17, 23)
        Me.Button16.TabIndex = 37
        Me.Button16.Text = "Button16"
        Me.Button16.UseVisualStyleBackColor = True
        Me.Button16.Visible = False
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(594, 125)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(17, 23)
        Me.Button15.TabIndex = 36
        Me.Button15.Text = "Button15"
        Me.Button15.UseVisualStyleBackColor = True
        Me.Button15.Visible = False
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(612, 102)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(17, 23)
        Me.Button14.TabIndex = 35
        Me.Button14.Text = "Button14"
        Me.Button14.UseVisualStyleBackColor = True
        Me.Button14.Visible = False
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(612, 125)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(17, 23)
        Me.Button13.TabIndex = 34
        Me.Button13.Text = "Button13"
        Me.Button13.UseVisualStyleBackColor = True
        Me.Button13.Visible = False
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(648, 102)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(17, 23)
        Me.Button12.TabIndex = 33
        Me.Button12.Text = "Button12"
        Me.Button12.UseVisualStyleBackColor = True
        Me.Button12.Visible = False
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(648, 57)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(17, 23)
        Me.Button11.TabIndex = 32
        Me.Button11.Text = "Button11"
        Me.Button11.UseVisualStyleBackColor = True
        Me.Button11.Visible = False
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(631, 102)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(17, 23)
        Me.Button10.TabIndex = 31
        Me.Button10.Text = "fee stat"
        Me.Button10.UseVisualStyleBackColor = True
        Me.Button10.Visible = False
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(648, 80)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(17, 23)
        Me.Button9.TabIndex = 30
        Me.Button9.Text = "Close Test Reports"
        Me.Button9.UseVisualStyleBackColor = True
        Me.Button9.Visible = False
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(631, 125)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(17, 23)
        Me.Button8.TabIndex = 29
        Me.Button8.Text = "PRd- Dev"
        Me.Button8.UseVisualStyleBackColor = True
        Me.Button8.Visible = False
        '
        'Button7
        '
        Me.Button7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button7.Location = New System.Drawing.Point(631, 80)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(17, 23)
        Me.Button7.TabIndex = 28
        Me.Button7.Text = "Poplulate Fee System"
        Me.Button7.UseVisualStyleBackColor = True
        Me.Button7.Visible = False
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(666, 125)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(17, 23)
        Me.Button6.TabIndex = 27
        Me.Button6.Text = "pop SSCP Comments"
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(666, 57)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(17, 23)
        Me.Button5.TabIndex = 26
        Me.Button5.Text = "Pop. SSCP"
        Me.Button5.UseVisualStyleBackColor = True
        Me.Button5.Visible = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(666, 80)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(17, 23)
        Me.Button4.TabIndex = 25
        Me.Button4.Text = "Pop. SSCP"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(648, 125)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(17, 23)
        Me.Button3.TabIndex = 24
        Me.Button3.Text = "PRd- Dev"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(666, 102)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(17, 23)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "Pop. SSCP"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'btnUpdateComplianceContacts
        '
        Me.btnUpdateComplianceContacts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateComplianceContacts.Location = New System.Drawing.Point(684, 125)
        Me.btnUpdateComplianceContacts.Name = "btnUpdateComplianceContacts"
        Me.btnUpdateComplianceContacts.Size = New System.Drawing.Size(17, 23)
        Me.btnUpdateComplianceContacts.TabIndex = 22
        Me.btnUpdateComplianceContacts.Text = "Update Compliance Contacts"
        Me.btnUpdateComplianceContacts.UseVisualStyleBackColor = True
        Me.btnUpdateComplianceContacts.Visible = False
        '
        'btnFixStacks
        '
        Me.btnFixStacks.Location = New System.Drawing.Point(684, 57)
        Me.btnFixStacks.Name = "btnFixStacks"
        Me.btnFixStacks.Size = New System.Drawing.Size(17, 23)
        Me.btnFixStacks.TabIndex = 21
        Me.btnFixStacks.Text = "Fix Stacks"
        Me.btnFixStacks.UseVisualStyleBackColor = True
        Me.btnFixStacks.Visible = False
        '
        'btnFixInvoice
        '
        Me.btnFixInvoice.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFixInvoice.Location = New System.Drawing.Point(684, 80)
        Me.btnFixInvoice.Name = "btnFixInvoice"
        Me.btnFixInvoice.Size = New System.Drawing.Size(17, 23)
        Me.btnFixInvoice.TabIndex = 20
        Me.btnFixInvoice.Text = "Fix Invoice issues"
        Me.btnFixInvoice.UseVisualStyleBackColor = True
        Me.btnFixInvoice.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(433, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "New AIRS #"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(314, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Old AIRS #"
        '
        'txtNewAIRS
        '
        Me.txtNewAIRS.Location = New System.Drawing.Point(436, 56)
        Me.txtNewAIRS.Name = "txtNewAIRS"
        Me.txtNewAIRS.Size = New System.Drawing.Size(100, 20)
        Me.txtNewAIRS.TabIndex = 17
        '
        'txtOldAIRS
        '
        Me.txtOldAIRS.Location = New System.Drawing.Point(317, 57)
        Me.txtOldAIRS.Name = "txtOldAIRS"
        Me.txtOldAIRS.Size = New System.Drawing.Size(100, 20)
        Me.txtOldAIRS.TabIndex = 16
        '
        'btnMoveAIRSData
        '
        Me.btnMoveAIRSData.AutoSize = True
        Me.btnMoveAIRSData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMoveAIRSData.Location = New System.Drawing.Point(156, 54)
        Me.btnMoveAIRSData.Name = "btnMoveAIRSData"
        Me.btnMoveAIRSData.Size = New System.Drawing.Size(155, 23)
        Me.btnMoveAIRSData.TabIndex = 15
        Me.btnMoveAIRSData.Text = "Move Data between AIRS #:"
        Me.btnMoveAIRSData.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(684, 102)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(17, 23)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Fix Final On Web"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnUpdateVersionNumber
        '
        Me.btnUpdateVersionNumber.AutoSize = True
        Me.btnUpdateVersionNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdateVersionNumber.Location = New System.Drawing.Point(306, 106)
        Me.btnUpdateVersionNumber.Name = "btnUpdateVersionNumber"
        Me.btnUpdateVersionNumber.Size = New System.Drawing.Size(100, 23)
        Me.btnUpdateVersionNumber.TabIndex = 13
        Me.btnUpdateVersionNumber.Text = "Update Version #"
        Me.btnUpdateVersionNumber.UseVisualStyleBackColor = True
        '
        'txtVersionNumber
        '
        Me.txtVersionNumber.Location = New System.Drawing.Point(412, 107)
        Me.txtVersionNumber.Name = "txtVersionNumber"
        Me.txtVersionNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtVersionNumber.TabIndex = 12
        '
        'txtACCNumber
        '
        Me.txtACCNumber.Location = New System.Drawing.Point(95, 132)
        Me.txtACCNumber.Name = "txtACCNumber"
        Me.txtACCNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtACCNumber.TabIndex = 11
        '
        'btnDeleteACC
        '
        Me.btnDeleteACC.AutoSize = True
        Me.btnDeleteACC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteACC.Location = New System.Drawing.Point(17, 129)
        Me.btnDeleteACC.Name = "btnDeleteACC"
        Me.btnDeleteACC.Size = New System.Drawing.Size(72, 23)
        Me.btnDeleteACC.TabIndex = 10
        Me.btnDeleteACC.Text = "Delete ACC"
        Me.btnDeleteACC.UseVisualStyleBackColor = True
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(134, 106)
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtEnforcementNumber.TabIndex = 9
        '
        'btnDeleteEnforcement
        '
        Me.btnDeleteEnforcement.AutoSize = True
        Me.btnDeleteEnforcement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteEnforcement.Location = New System.Drawing.Point(17, 104)
        Me.btnDeleteEnforcement.Name = "btnDeleteEnforcement"
        Me.btnDeleteEnforcement.Size = New System.Drawing.Size(111, 23)
        Me.btnDeleteEnforcement.TabIndex = 8
        Me.btnDeleteEnforcement.Text = "Delete Enforcement"
        Me.btnDeleteEnforcement.UseVisualStyleBackColor = True
        '
        'btnClearSQL
        '
        Me.btnClearSQL.AutoSize = True
        Me.btnClearSQL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearSQL.Location = New System.Drawing.Point(639, 13)
        Me.btnClearSQL.Name = "btnClearSQL"
        Me.btnClearSQL.Size = New System.Drawing.Size(65, 23)
        Me.btnClearSQL.TabIndex = 1
        Me.btnClearSQL.Text = "Clear SQL"
        Me.btnClearSQL.UseVisualStyleBackColor = True
        '
        'btnRunSQL
        '
        Me.btnRunSQL.AutoSize = True
        Me.btnRunSQL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRunSQL.Location = New System.Drawing.Point(12, 13)
        Me.btnRunSQL.Name = "btnRunSQL"
        Me.btnRunSQL.Size = New System.Drawing.Size(64, 23)
        Me.btnRunSQL.TabIndex = 0
        Me.btnRunSQL.Text = "Run SQL "
        Me.btnRunSQL.UseVisualStyleBackColor = True
        '
        'Button22
        '
        Me.Button22.Location = New System.Drawing.Point(572, 125)
        Me.Button22.Name = "Button22"
        Me.Button22.Size = New System.Drawing.Size(16, 23)
        Me.Button22.TabIndex = 43
        Me.Button22.Text = "Button22"
        Me.Button22.UseVisualStyleBackColor = True
        Me.Button22.Visible = False
        '
        'DMUTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(715, 485)
        Me.Controls.Add(Me.SCDMUTool)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DMUTool"
        Me.Text = "DMU Tool"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SCDMUTool.Panel1.ResumeLayout(False)
        Me.SCDMUTool.Panel1.PerformLayout()
        Me.SCDMUTool.Panel2.ResumeLayout(False)
        Me.SCDMUTool.Panel2.PerformLayout()
        Me.SCDMUTool.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SCDMUTool As System.Windows.Forms.SplitContainer
    Friend WithEvents txtSQL As System.Windows.Forms.TextBox
    Friend WithEvents btnClearSQL As System.Windows.Forms.Button
    Friend WithEvents btnRunSQL As System.Windows.Forms.Button
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnDeleteEnforcement As System.Windows.Forms.Button
    Friend WithEvents txtACCNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnDeleteACC As System.Windows.Forms.Button
    Friend WithEvents btnUpdateVersionNumber As System.Windows.Forms.Button
    Friend WithEvents txtVersionNumber As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtNewAIRS As System.Windows.Forms.TextBox
    Friend WithEvents txtOldAIRS As System.Windows.Forms.TextBox
    Friend WithEvents btnMoveAIRSData As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnFixInvoice As System.Windows.Forms.Button
    Friend WithEvents btnFixStacks As System.Windows.Forms.Button
    Friend WithEvents btnUpdateComplianceContacts As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Button18 As System.Windows.Forms.Button
    Friend WithEvents Button19 As System.Windows.Forms.Button
    Friend WithEvents Button20 As System.Windows.Forms.Button
    Friend WithEvents Button21 As System.Windows.Forms.Button
    Friend WithEvents Button22 As System.Windows.Forms.Button
End Class
