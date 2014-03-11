<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipAbout
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
        Me.lblSubTitle = New System.Windows.Forms.Label
        Me.lblLicenseLabel = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.LogoBox = New System.Windows.Forms.PictureBox
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblSupport = New System.Windows.Forms.LinkLabel
        Me.lblDocumentation = New System.Windows.Forms.LinkLabel
        Me.lblChangelog = New System.Windows.Forms.LinkLabel
        Me.lblUpdateCheck = New System.Windows.Forms.LinkLabel
        CType(Me.LogoBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTitle.Location = New System.Drawing.Point(325, 134)
        Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(274, 48)
        Me.lblSubTitle.TabIndex = 32
        Me.lblSubTitle.Text = "Georgia Department of Natural Resources" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Environmental Protection Division" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Air P" & _
            "rotection Branch"
        '
        'lblLicenseLabel
        '
        Me.lblLicenseLabel.AutoSize = True
        Me.lblLicenseLabel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseLabel.Location = New System.Drawing.Point(75, 288)
        Me.lblLicenseLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblLicenseLabel.Name = "lblLicenseLabel"
        Me.lblLicenseLabel.Size = New System.Drawing.Size(164, 28)
        Me.lblLicenseLabel.TabIndex = 37
        Me.lblLicenseLabel.Text = "This product is licensed to State " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "of Georgia employees only"
        Me.lblLicenseLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(323, 58)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(343, 25)
        Me.lblTitle.TabIndex = 32
        Me.lblTitle.Text = "Integrated Air Information Platform" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'LogoBox
        '
        Me.LogoBox.Image = Global.Iaip.My.Resources.Resources.Logo
        Me.LogoBox.InitialImage = Nothing
        Me.LogoBox.Location = New System.Drawing.Point(29, 29)
        Me.LogoBox.Name = "LogoBox"
        Me.LogoBox.Size = New System.Drawing.Size(256, 256)
        Me.LogoBox.TabIndex = 0
        Me.LogoBox.TabStop = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(325, 103)
        Me.lblVersion.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(61, 16)
        Me.lblVersion.TabIndex = 32
        Me.lblVersion.Text = "Version"
        '
        'lblSupport
        '
        Me.lblSupport.AutoSize = True
        Me.lblSupport.Location = New System.Drawing.Point(325, 204)
        Me.lblSupport.Name = "lblSupport"
        Me.lblSupport.Size = New System.Drawing.Size(44, 13)
        Me.lblSupport.TabIndex = 0
        Me.lblSupport.TabStop = True
        Me.lblSupport.Text = "Support"
        '
        'lblDocumentation
        '
        Me.lblDocumentation.AutoSize = True
        Me.lblDocumentation.Location = New System.Drawing.Point(325, 226)
        Me.lblDocumentation.Name = "lblDocumentation"
        Me.lblDocumentation.Size = New System.Drawing.Size(110, 13)
        Me.lblDocumentation.TabIndex = 1
        Me.lblDocumentation.TabStop = True
        Me.lblDocumentation.Text = "Online documentation"
        '
        'lblChangelog
        '
        Me.lblChangelog.AutoSize = True
        Me.lblChangelog.Location = New System.Drawing.Point(325, 248)
        Me.lblChangelog.Name = "lblChangelog"
        Me.lblChangelog.Size = New System.Drawing.Size(61, 13)
        Me.lblChangelog.TabIndex = 2
        Me.lblChangelog.TabStop = True
        Me.lblChangelog.Text = "Change log"
        '
        'lblUpdateCheck
        '
        Me.lblUpdateCheck.AutoSize = True
        Me.lblUpdateCheck.Location = New System.Drawing.Point(325, 270)
        Me.lblUpdateCheck.Name = "lblUpdateCheck"
        Me.lblUpdateCheck.Size = New System.Drawing.Size(94, 13)
        Me.lblUpdateCheck.TabIndex = 3
        Me.lblUpdateCheck.TabStop = True
        Me.lblUpdateCheck.Text = "Check for updates"
        '
        'IaipAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(681, 338)
        Me.Controls.Add(Me.lblUpdateCheck)
        Me.Controls.Add(Me.lblChangelog)
        Me.Controls.Add(Me.lblDocumentation)
        Me.Controls.Add(Me.lblSupport)
        Me.Controls.Add(Me.LogoBox)
        Me.Controls.Add(Me.lblLicenseLabel)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblSubTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IaipAbout"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IAIP — About"
        CType(Me.LogoBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSubTitle As System.Windows.Forms.Label
    Friend WithEvents lblLicenseLabel As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents LogoBox As System.Windows.Forms.PictureBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblSupport As System.Windows.Forms.LinkLabel
    Friend WithEvents lblDocumentation As System.Windows.Forms.LinkLabel
    Friend WithEvents lblChangelog As System.Windows.Forms.LinkLabel
    Friend WithEvents lblUpdateCheck As System.Windows.Forms.LinkLabel
End Class
