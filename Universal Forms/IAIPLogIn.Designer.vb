<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPLogIn
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPLogIn))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiLogIn = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MmiExit = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.mmiOnlineHelp = New System.Windows.Forms.MenuItem
        Me.mmiTestingEnvior = New System.Windows.Forms.MenuItem
        Me.mmiLukeEnviornment = New System.Windows.Forms.MenuItem
        Me.mmiTestingDatabase = New System.Windows.Forms.MenuItem
        Me.mmiRefreshUserID = New System.Windows.Forms.MenuItem
        Me.mmiUpdate = New System.Windows.Forms.MenuItem
        Me.mmiRefreshDefaultLoc = New System.Windows.Forms.MenuItem
        Me.PictureStateSeal = New System.Windows.Forms.PictureBox
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UserIDLabel = New System.Windows.Forms.Label
        Me.UserPassword = New System.Windows.Forms.TextBox
        Me.UserID = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.UpdateIaipLink = New System.Windows.Forms.LinkLabel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LogInButton = New System.Windows.Forms.Button
        Me.WarningLabel = New System.Windows.Forms.Label
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.IaipPatchLink = New System.Windows.Forms.LinkLabel
        Me.CurrentVersionMessage = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.AvailableVersionMessage = New System.Windows.Forms.Label
        Me.GeneralMessage = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
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
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiLogIn, Me.MenuItem3, Me.MmiExit})
        Me.MenuItem1.Text = "File "
        '
        'MmiLogIn
        '
        Me.MmiLogIn.Index = 0
        Me.MmiLogIn.Text = "Log In"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.Text = "-"
        '
        'MmiExit
        '
        Me.MmiExit.Index = 2
        Me.MmiExit.Text = "Exit"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 1
        Me.mmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiOnlineHelp, Me.mmiTestingEnvior, Me.mmiLukeEnviornment, Me.mmiTestingDatabase, Me.mmiRefreshUserID, Me.mmiUpdate, Me.mmiRefreshDefaultLoc})
        Me.mmiHelp.Text = "Help"
        '
        'mmiOnlineHelp
        '
        Me.mmiOnlineHelp.Index = 0
        Me.mmiOnlineHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.mmiOnlineHelp.Text = "Online Help "
        '
        'mmiTestingEnvior
        '
        Me.mmiTestingEnvior.Index = 1
        Me.mmiTestingEnvior.Shortcut = System.Windows.Forms.Shortcut.CtrlT
        Me.mmiTestingEnvior.Text = "Testing Environment"
        '
        'mmiLukeEnviornment
        '
        Me.mmiLukeEnviornment.Index = 2
        Me.mmiLukeEnviornment.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.mmiLukeEnviornment.Text = "*"
        Me.mmiLukeEnviornment.Visible = False
        '
        'mmiTestingDatabase
        '
        Me.mmiTestingDatabase.Index = 3
        Me.mmiTestingDatabase.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftT
        Me.mmiTestingDatabase.Text = "Testing Database"
        Me.mmiTestingDatabase.Visible = False
        '
        'mmiRefreshUserID
        '
        Me.mmiRefreshUserID.Index = 4
        Me.mmiRefreshUserID.Text = "Refresh Default User"
        '
        'mmiUpdate
        '
        Me.mmiUpdate.Index = 5
        Me.mmiUpdate.Text = "Update IAIP (manual)"
        '
        'mmiRefreshDefaultLoc
        '
        Me.mmiRefreshDefaultLoc.Index = 6
        Me.mmiRefreshDefaultLoc.Text = "Refresh Default Location"
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
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordLabel.Location = New System.Drawing.Point(466, 330)
        Me.PasswordLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(82, 20)
        Me.PasswordLabel.TabIndex = 36
        Me.PasswordLabel.Text = "Password:"
        '
        'UserIDLabel
        '
        Me.UserIDLabel.AutoSize = True
        Me.UserIDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserIDLabel.Location = New System.Drawing.Point(466, 298)
        Me.UserIDLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.UserIDLabel.Name = "UserIDLabel"
        Me.UserIDLabel.Size = New System.Drawing.Size(68, 20)
        Me.UserIDLabel.TabIndex = 35
        Me.UserIDLabel.Text = "User ID:"
        '
        'UserPassword
        '
        Me.UserPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserPassword.Location = New System.Drawing.Point(555, 327)
        Me.UserPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.UserPassword.Name = "UserPassword"
        Me.UserPassword.Size = New System.Drawing.Size(209, 26)
        Me.UserPassword.TabIndex = 1
        Me.UserPassword.UseSystemPasswordChar = True
        Me.UserPassword.WordWrap = False
        '
        'UserID
        '
        Me.UserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserID.Location = New System.Drawing.Point(555, 295)
        Me.UserID.Margin = New System.Windows.Forms.Padding(2)
        Me.UserID.Name = "UserID"
        Me.UserID.Size = New System.Drawing.Size(209, 26)
        Me.UserID.TabIndex = 3
        Me.UserID.WordWrap = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(392, 42)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(372, 29)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Environmental Protection Division"
        '
        'UpdateIaipLink
        '
        Me.UpdateIaipLink.AutoSize = True
        Me.UpdateIaipLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdateIaipLink.Location = New System.Drawing.Point(595, 274)
        Me.UpdateIaipLink.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.UpdateIaipLink.Name = "UpdateIaipLink"
        Me.UpdateIaipLink.Size = New System.Drawing.Size(88, 18)
        Me.UpdateIaipLink.TabIndex = 4
        Me.UpdateIaipLink.TabStop = True
        Me.UpdateIaipLink.Text = "Update IAIP "
        Me.UpdateIaipLink.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 9)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(741, 33)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "GEORGIA DEPARTMENT OF NATURAL RESOURCES"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(526, 71)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(238, 29)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Air Protection Branch"
        '
        'LogInButton
        '
        Me.LogInButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogInButton.Location = New System.Drawing.Point(555, 366)
        Me.LogInButton.Margin = New System.Windows.Forms.Padding(2)
        Me.LogInButton.Name = "LogInButton"
        Me.LogInButton.Size = New System.Drawing.Size(88, 31)
        Me.LogInButton.TabIndex = 2
        Me.LogInButton.Text = "Log In"
        '
        'WarningLabel
        '
        Me.WarningLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WarningLabel.Location = New System.Drawing.Point(60, 415)
        Me.WarningLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.WarningLabel.Name = "WarningLabel"
        Me.WarningLabel.Size = New System.Drawing.Size(288, 48)
        Me.WarningLabel.TabIndex = 37
        Me.WarningLabel.Text = "Warning: This Product is licensed to DNR/EPD/APB Employees Only"
        Me.WarningLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar, Me.Panel1, Me.Panel2, Me.Panel3, Me.Panel4})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 476)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(792, 25)
        Me.StatusStrip1.TabIndex = 39
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(75, 19)
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
        Me.Panel1.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(615, 20)
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
        Me.Panel2.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(44, 20)
        Me.Panel2.Text = "Hello "
        '
        'Panel3
        '
        Me.Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Panel3.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(45, 20)
        Me.Panel3.Text = "World"
        Me.Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1, 20)
        Me.Panel4.Spring = True
        '
        'IaipPatchLink
        '
        Me.IaipPatchLink.AutoSize = True
        Me.IaipPatchLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IaipPatchLink.Location = New System.Drawing.Point(683, 274)
        Me.IaipPatchLink.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.IaipPatchLink.Name = "IaipPatchLink"
        Me.IaipPatchLink.Size = New System.Drawing.Size(79, 18)
        Me.IaipPatchLink.TabIndex = 40
        Me.IaipPatchLink.TabStop = True
        Me.IaipPatchLink.Text = "IAIP Patch "
        Me.IaipPatchLink.Visible = False
        '
        'CurrentVersionMessage
        '
        Me.CurrentVersionMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurrentVersionMessage.Location = New System.Drawing.Point(467, 415)
        Me.CurrentVersionMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.CurrentVersionMessage.Name = "CurrentVersionMessage"
        Me.CurrentVersionMessage.Size = New System.Drawing.Size(245, 18)
        Me.CurrentVersionMessage.TabIndex = 38
        Me.CurrentVersionMessage.Text = "Current Version Placeholder"
        Me.CurrentVersionMessage.Visible = False
        '
        'Button2
        '
        Me.Button2.AutoSize = True
        Me.Button2.Location = New System.Drawing.Point(608, 103)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(83, 23)
        Me.Button2.TabIndex = 42
        Me.Button2.Text = "adjust intranet"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(434, 103)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 43
        Me.Button1.Text = "Add EIS"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(515, 103)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 44
        Me.Button3.Text = "Delete EIS"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'AvailableVersionMessage
        '
        Me.AvailableVersionMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AvailableVersionMessage.ForeColor = System.Drawing.SystemColors.InfoText
        Me.AvailableVersionMessage.Location = New System.Drawing.Point(467, 433)
        Me.AvailableVersionMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.AvailableVersionMessage.Name = "AvailableVersionMessage"
        Me.AvailableVersionMessage.Size = New System.Drawing.Size(245, 43)
        Me.AvailableVersionMessage.TabIndex = 45
        Me.AvailableVersionMessage.Text = "Available Version Placeholder"
        Me.AvailableVersionMessage.Visible = False
        '
        'GeneralMessage
        '
        Me.GeneralMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GeneralMessage.ForeColor = System.Drawing.SystemColors.InfoText
        Me.GeneralMessage.Location = New System.Drawing.Point(467, 178)
        Me.GeneralMessage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.GeneralMessage.Name = "GeneralMessage"
        Me.GeneralMessage.Size = New System.Drawing.Size(297, 96)
        Me.GeneralMessage.TabIndex = 38
        Me.GeneralMessage.Text = "Message Placeholder"
        Me.GeneralMessage.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(388, 129)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(376, 29)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Integrated Air Information Platform"
        '
        'IAIPLogIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 501)
        Me.Controls.Add(Me.AvailableVersionMessage)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.IaipPatchLink)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GeneralMessage)
        Me.Controls.Add(Me.CurrentVersionMessage)
        Me.Controls.Add(Me.WarningLabel)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UserIDLabel)
        Me.Controls.Add(Me.UserPassword)
        Me.Controls.Add(Me.UserID)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.UpdateIaipLink)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LogInButton)
        Me.Controls.Add(Me.PictureStateSeal)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
    Friend WithEvents MmiLogIn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents PictureStateSeal As System.Windows.Forms.PictureBox
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UserIDLabel As System.Windows.Forms.Label
    Friend WithEvents UserPassword As System.Windows.Forms.TextBox
    Friend WithEvents UserID As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents UpdateIaipLink As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LogInButton As System.Windows.Forms.Button
    Friend WithEvents WarningLabel As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mmiTestingEnvior As System.Windows.Forms.MenuItem
    Friend WithEvents mmiLukeEnviornment As System.Windows.Forms.MenuItem
    Friend WithEvents mmiTestingDatabase As System.Windows.Forms.MenuItem
    Friend WithEvents IaipPatchLink As System.Windows.Forms.LinkLabel
    Friend WithEvents mmiRefreshUserID As System.Windows.Forms.MenuItem
    Friend WithEvents mmiOnlineHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mmiUpdate As System.Windows.Forms.MenuItem
    Friend WithEvents mmiRefreshDefaultLoc As System.Windows.Forms.MenuItem
    Friend WithEvents CurrentVersionMessage As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents AvailableVersionMessage As System.Windows.Forms.Label
    Friend WithEvents GeneralMessage As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
