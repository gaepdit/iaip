<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPLogIn
    Inherits DefaultForm

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPLogIn))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mmiExit = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.mmiOnlineHelp = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiTestingEnvironment = New System.Windows.Forms.MenuItem
        Me.mmiRefreshUserID = New System.Windows.Forms.MenuItem
        Me.mmiRefreshDefaultLoc = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mmiForceUpdate = New System.Windows.Forms.MenuItem
        Me.mmiLukeEnvironment = New System.Windows.Forms.MenuItem
        Me.mmiTestingDatabase = New System.Windows.Forms.MenuItem
        Me.PictureStateSeal = New System.Windows.Forms.PictureBox
        Me.lblPassword = New System.Windows.Forms.Label
        Me.lblUserID = New System.Windows.Forms.Label
        Me.txtUserPassword = New System.Windows.Forms.TextBox
        Me.txtUserID = New System.Windows.Forms.TextBox
        Me.lblSubTitle = New System.Windows.Forms.Label
        Me.UpdateLink = New System.Windows.Forms.LinkLabel
        Me.lblTitle = New System.Windows.Forms.Label
        Me.btnLoginButton = New System.Windows.Forms.Button
        Me.lblLicenseLabel = New System.Windows.Forms.Label
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lnkIaipPatch = New System.Windows.Forms.LinkLabel
        Me.lblCurrentVersionMessage = New System.Windows.Forms.Label
        Me.btnAdjustIntranet = New System.Windows.Forms.Button
        Me.btnAddEIS = New System.Windows.Forms.Button
        Me.btnDeleteEIS = New System.Windows.Forms.Button
        Me.lblAvailableVersionMessage = New System.Windows.Forms.Label
        Me.lblGeneralMessage = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.PictureStateSeal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiExit})
        Me.MenuItem1.Text = "&File"
        '
        'mmiExit
        '
        Me.mmiExit.Index = 0
        Me.mmiExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ
        Me.mmiExit.Text = "E&xit"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 1
        Me.mmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiOnlineHelp, Me.MenuItem2, Me.mmiTestingEnvironment, Me.mmiRefreshUserID, Me.mmiRefreshDefaultLoc, Me.MenuItem3, Me.mmiForceUpdate, Me.mmiLukeEnvironment, Me.mmiTestingDatabase})
        Me.mmiHelp.Text = "&Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.Index = 0
        Me.mmiOnlineHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.mmiOnlineHelp.Text = "Online &Help "
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'mmiTestingEnvironment
        '
        Me.mmiTestingEnvironment.Index = 2
        Me.mmiTestingEnvironment.Shortcut = System.Windows.Forms.Shortcut.CtrlT
        Me.mmiTestingEnvironment.Text = "&Testing Environment"
        '
        'mmiRefreshUserID
        '
        Me.mmiRefreshUserID.Index = 3
        Me.mmiRefreshUserID.Text = "Refresh Default User"
        '
        'mmiRefreshDefaultLoc
        '
        Me.mmiRefreshDefaultLoc.Index = 4
        Me.mmiRefreshDefaultLoc.Text = "Refresh Default Location"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 5
        Me.MenuItem3.Text = "-"
        '
        'mmiForceUpdate
        '
        Me.mmiForceUpdate.Index = 6
        Me.mmiForceUpdate.Text = "Force Update"
        '
        'mmiLukeEnvironment
        '
        Me.mmiLukeEnvironment.Index = 7
        Me.mmiLukeEnvironment.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.mmiLukeEnvironment.Text = "*"
        Me.mmiLukeEnvironment.Visible = False
        '
        'mmiTestingDatabase
        '
        Me.mmiTestingDatabase.Index = 8
        Me.mmiTestingDatabase.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftT
        Me.mmiTestingDatabase.Text = "Testing Database"
        Me.mmiTestingDatabase.Visible = False
        '
        'PictureStateSeal
        '
        Me.PictureStateSeal.Image = CType(resources.GetObject("PictureStateSeal.Image"), System.Drawing.Image)
        Me.PictureStateSeal.Location = New System.Drawing.Point(29, 63)
        Me.PictureStateSeal.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureStateSeal.Name = "PictureStateSeal"
        Me.PictureStateSeal.Size = New System.Drawing.Size(350, 350)
        Me.PictureStateSeal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureStateSeal.TabIndex = 1
        Me.PictureStateSeal.TabStop = False
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(466, 300)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(82, 20)
        Me.lblPassword.TabIndex = 36
        Me.lblPassword.Text = "Password:"
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserID.Location = New System.Drawing.Point(466, 261)
        Me.lblUserID.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(68, 20)
        Me.lblUserID.TabIndex = 35
        Me.lblUserID.Text = "User ID:"
        '
        'txtUserPassword
        '
        Me.txtUserPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserPassword.Location = New System.Drawing.Point(552, 297)
        Me.txtUserPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUserPassword.Name = "txtUserPassword"
        Me.txtUserPassword.Size = New System.Drawing.Size(175, 26)
        Me.txtUserPassword.TabIndex = 1
        Me.txtUserPassword.UseSystemPasswordChar = True
        Me.txtUserPassword.WordWrap = False
        '
        'txtUserID
        '
        Me.txtUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserID.Location = New System.Drawing.Point(552, 258)
        Me.txtUserID.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(175, 26)
        Me.txtUserID.TabIndex = 1
        Me.txtUserID.WordWrap = False
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTitle.Location = New System.Drawing.Point(388, 42)
        Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(372, 58)
        Me.lblSubTitle.TabIndex = 32
        Me.lblSubTitle.Text = "Environmental Protection Division" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Air Protection Branch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'UpdateLink
        '
        Me.UpdateLink.AutoSize = True
        Me.UpdateLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdateLink.Location = New System.Drawing.Point(467, 443)
        Me.UpdateLink.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.UpdateLink.Name = "UpdateLink"
        Me.UpdateLink.Size = New System.Drawing.Size(155, 18)
        Me.UpdateLink.TabIndex = 3
        Me.UpdateLink.TabStop = True
        Me.UpdateLink.Text = "Download IAIP Update"
        Me.UpdateLink.Visible = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(23, 9)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(741, 33)
        Me.lblTitle.TabIndex = 28
        Me.lblTitle.Text = "GEORGIA DEPARTMENT OF NATURAL RESOURCES"
        '
        'btnLoginButton
        '
        Me.btnLoginButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoginButton.Location = New System.Drawing.Point(552, 336)
        Me.btnLoginButton.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLoginButton.Name = "btnLoginButton"
        Me.btnLoginButton.Size = New System.Drawing.Size(175, 38)
        Me.btnLoginButton.TabIndex = 2
        Me.btnLoginButton.Text = "Log In"
        '
        'lblLicenseLabel
        '
        Me.lblLicenseLabel.AutoSize = True
        Me.lblLicenseLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseLabel.Location = New System.Drawing.Point(80, 415)
        Me.lblLicenseLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblLicenseLabel.Name = "lblLicenseLabel"
        Me.lblLicenseLabel.Size = New System.Drawing.Size(237, 36)
        Me.lblLicenseLabel.TabIndex = 37
        Me.lblLicenseLabel.Text = "This product is licensed to Georgia" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DNR/EPD/APB employees only"
        Me.lblLicenseLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar, Me.Panel1, Me.Panel2, Me.Panel3, Me.Panel4})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 475)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 26)
        Me.StatusStrip1.TabIndex = 39
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ProgressBar
        '
        Me.ProgressBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(75, 20)
        Me.ProgressBar.Step = 15
        Me.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(608, 21)
        Me.Panel1.Spring = True
        Me.Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(48, 21)
        Me.Panel2.Text = "Hello "
        '
        'Panel3
        '
        Me.Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Panel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(48, 21)
        Me.Panel3.Text = "World"
        Me.Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1, 21)
        Me.Panel4.Spring = True
        '
        'lnkIaipPatch
        '
        Me.lnkIaipPatch.AutoSize = True
        Me.lnkIaipPatch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkIaipPatch.Location = New System.Drawing.Point(26, 458)
        Me.lnkIaipPatch.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lnkIaipPatch.Name = "lnkIaipPatch"
        Me.lnkIaipPatch.Size = New System.Drawing.Size(79, 18)
        Me.lnkIaipPatch.TabIndex = 40
        Me.lnkIaipPatch.TabStop = True
        Me.lnkIaipPatch.Text = "IAIP Patch "
        Me.lnkIaipPatch.Visible = False
        '
        'lblCurrentVersionMessage
        '
        Me.lblCurrentVersionMessage.AutoSize = True
        Me.lblCurrentVersionMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentVersionMessage.Location = New System.Drawing.Point(467, 397)
        Me.lblCurrentVersionMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCurrentVersionMessage.Name = "lblCurrentVersionMessage"
        Me.lblCurrentVersionMessage.Size = New System.Drawing.Size(193, 18)
        Me.lblCurrentVersionMessage.TabIndex = 38
        Me.lblCurrentVersionMessage.Text = "Current Version Placeholder"
        Me.lblCurrentVersionMessage.Visible = False
        '
        'btnAdjustIntranet
        '
        Me.btnAdjustIntranet.AutoSize = True
        Me.btnAdjustIntranet.Location = New System.Drawing.Point(272, 457)
        Me.btnAdjustIntranet.Name = "btnAdjustIntranet"
        Me.btnAdjustIntranet.Size = New System.Drawing.Size(83, 23)
        Me.btnAdjustIntranet.TabIndex = 42
        Me.btnAdjustIntranet.Text = "adjust intranet"
        Me.btnAdjustIntranet.UseVisualStyleBackColor = True
        Me.btnAdjustIntranet.Visible = False
        '
        'btnAddEIS
        '
        Me.btnAddEIS.Location = New System.Drawing.Point(110, 457)
        Me.btnAddEIS.Name = "btnAddEIS"
        Me.btnAddEIS.Size = New System.Drawing.Size(75, 23)
        Me.btnAddEIS.TabIndex = 43
        Me.btnAddEIS.Text = "Add EIS"
        Me.btnAddEIS.UseVisualStyleBackColor = True
        Me.btnAddEIS.Visible = False
        '
        'btnDeleteEIS
        '
        Me.btnDeleteEIS.Location = New System.Drawing.Point(191, 457)
        Me.btnDeleteEIS.Name = "btnDeleteEIS"
        Me.btnDeleteEIS.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteEIS.TabIndex = 44
        Me.btnDeleteEIS.Text = "Delete EIS"
        Me.btnDeleteEIS.UseVisualStyleBackColor = True
        Me.btnDeleteEIS.Visible = False
        '
        'lblAvailableVersionMessage
        '
        Me.lblAvailableVersionMessage.AutoSize = True
        Me.lblAvailableVersionMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvailableVersionMessage.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblAvailableVersionMessage.Location = New System.Drawing.Point(467, 415)
        Me.lblAvailableVersionMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblAvailableVersionMessage.Name = "lblAvailableVersionMessage"
        Me.lblAvailableVersionMessage.Size = New System.Drawing.Size(201, 18)
        Me.lblAvailableVersionMessage.TabIndex = 45
        Me.lblAvailableVersionMessage.Text = "Available Version Placeholder"
        Me.lblAvailableVersionMessage.Visible = False
        '
        'lblGeneralMessage
        '
        Me.lblGeneralMessage.AutoSize = True
        Me.lblGeneralMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralMessage.ForeColor = System.Drawing.Color.Maroon
        Me.lblGeneralMessage.Location = New System.Drawing.Point(390, 171)
        Me.lblGeneralMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblGeneralMessage.Name = "lblGeneralMessage"
        Me.lblGeneralMessage.Size = New System.Drawing.Size(163, 72)
        Me.lblGeneralMessage.TabIndex = 38
        Me.lblGeneralMessage.Text = "Message Placeholder 1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4"
        Me.lblGeneralMessage.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(388, 118)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(376, 29)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Integrated Air Information Platform" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'IAIPLogIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 501)
        Me.Controls.Add(Me.lblAvailableVersionMessage)
        Me.Controls.Add(Me.btnDeleteEIS)
        Me.Controls.Add(Me.btnAddEIS)
        Me.Controls.Add(Me.btnAdjustIntranet)
        Me.Controls.Add(Me.lnkIaipPatch)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lblGeneralMessage)
        Me.Controls.Add(Me.lblCurrentVersionMessage)
        Me.Controls.Add(Me.lblLicenseLabel)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUserID)
        Me.Controls.Add(Me.txtUserPassword)
        Me.Controls.Add(Me.txtUserID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSubTitle)
        Me.Controls.Add(Me.UpdateLink)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnLoginButton)
        Me.Controls.Add(Me.PictureStateSeal)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPLogIn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Integrated Air Information Platform"
        CType(Me.PictureStateSeal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents PictureStateSeal As System.Windows.Forms.PictureBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblUserID As System.Windows.Forms.Label
    Friend WithEvents txtUserPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents lblSubTitle As System.Windows.Forms.Label
    Friend WithEvents UpdateLink As System.Windows.Forms.LinkLabel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnLoginButton As System.Windows.Forms.Button
    Friend WithEvents lblLicenseLabel As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mmiTestingEnvironment As System.Windows.Forms.MenuItem
    Friend WithEvents lnkIaipPatch As System.Windows.Forms.LinkLabel
    Friend WithEvents mmiRefreshUserID As System.Windows.Forms.MenuItem
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiForceUpdate As System.Windows.Forms.MenuItem
    Friend WithEvents mmiRefreshDefaultLoc As System.Windows.Forms.MenuItem
    Friend WithEvents lblCurrentVersionMessage As System.Windows.Forms.Label
    Friend WithEvents btnAdjustIntranet As System.Windows.Forms.Button
    Friend WithEvents btnAddEIS As System.Windows.Forms.Button
    Friend WithEvents btnDeleteEIS As System.Windows.Forms.Button
    Friend WithEvents lblAvailableVersionMessage As System.Windows.Forms.Label
    Friend WithEvents lblGeneralMessage As System.Windows.Forms.Label
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiLukeEnvironment As System.Windows.Forms.MenuItem
    Friend WithEvents mmiTestingDatabase As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
