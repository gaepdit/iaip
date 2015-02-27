<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DMUDangerousTool
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
        Me.lblEisYear = New System.Windows.Forms.Label
        Me.txtEISYear = New System.Windows.Forms.TextBox
        Me.lblEisAirs = New System.Windows.Forms.Label
        Me.txtEISAIRSNumber = New System.Windows.Forms.TextBox
        Me.PD_EIS_QASTART = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtNewAIRS = New System.Windows.Forms.TextBox
        Me.txtOldAIRS = New System.Windows.Forms.TextBox
        Me.btnMoveAIRSData = New System.Windows.Forms.Button
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox
        Me.btnDeleteEnforcement = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblEisYear
        '
        Me.lblEisYear.AutoSize = True
        Me.lblEisYear.Location = New System.Drawing.Point(185, 85)
        Me.lblEisYear.Name = "lblEisYear"
        Me.lblEisYear.Size = New System.Drawing.Size(49, 13)
        Me.lblEisYear.TabIndex = 50
        Me.lblEisYear.Text = "EIS Year"
        Me.lblEisYear.Visible = False
        '
        'txtEISYear
        '
        Me.txtEISYear.Location = New System.Drawing.Point(240, 81)
        Me.txtEISYear.Name = "txtEISYear"
        Me.txtEISYear.Size = New System.Drawing.Size(55, 20)
        Me.txtEISYear.TabIndex = 49
        Me.txtEISYear.Visible = False
        '
        'lblEisAirs
        '
        Me.lblEisAirs.AutoSize = True
        Me.lblEisAirs.Location = New System.Drawing.Point(12, 85)
        Me.lblEisAirs.Name = "lblEisAirs"
        Me.lblEisAirs.Size = New System.Drawing.Size(62, 13)
        Me.lblEisAirs.TabIndex = 48
        Me.lblEisAirs.Text = "EIS AIRS #"
        Me.lblEisAirs.Visible = False
        '
        'txtEISAIRSNumber
        '
        Me.txtEISAIRSNumber.Location = New System.Drawing.Point(79, 81)
        Me.txtEISAIRSNumber.Name = "txtEISAIRSNumber"
        Me.txtEISAIRSNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtEISAIRSNumber.TabIndex = 47
        Me.txtEISAIRSNumber.Visible = False
        '
        'PD_EIS_QASTART
        '
        Me.PD_EIS_QASTART.Location = New System.Drawing.Point(301, 80)
        Me.PD_EIS_QASTART.Name = "PD_EIS_QASTART"
        Me.PD_EIS_QASTART.Size = New System.Drawing.Size(75, 23)
        Me.PD_EIS_QASTART.TabIndex = 46
        Me.PD_EIS_QASTART.Text = "PD_EIS_QASTART"
        Me.PD_EIS_QASTART.UseVisualStyleBackColor = True
        Me.PD_EIS_QASTART.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(289, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "New AIRS #"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(170, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Old AIRS #"
        '
        'txtNewAIRS
        '
        Me.txtNewAIRS.Location = New System.Drawing.Point(292, 143)
        Me.txtNewAIRS.Name = "txtNewAIRS"
        Me.txtNewAIRS.Size = New System.Drawing.Size(100, 20)
        Me.txtNewAIRS.TabIndex = 17
        '
        'txtOldAIRS
        '
        Me.txtOldAIRS.Location = New System.Drawing.Point(173, 144)
        Me.txtOldAIRS.Name = "txtOldAIRS"
        Me.txtOldAIRS.Size = New System.Drawing.Size(100, 20)
        Me.txtOldAIRS.TabIndex = 16
        '
        'btnMoveAIRSData
        '
        Me.btnMoveAIRSData.AutoSize = True
        Me.btnMoveAIRSData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMoveAIRSData.Location = New System.Drawing.Point(12, 141)
        Me.btnMoveAIRSData.Name = "btnMoveAIRSData"
        Me.btnMoveAIRSData.Size = New System.Drawing.Size(155, 23)
        Me.btnMoveAIRSData.TabIndex = 15
        Me.btnMoveAIRSData.Text = "Move Data between AIRS #:"
        Me.btnMoveAIRSData.UseVisualStyleBackColor = True
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(129, 23)
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtEnforcementNumber.TabIndex = 9
        '
        'btnDeleteEnforcement
        '
        Me.btnDeleteEnforcement.AutoSize = True
        Me.btnDeleteEnforcement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteEnforcement.Location = New System.Drawing.Point(12, 21)
        Me.btnDeleteEnforcement.Name = "btnDeleteEnforcement"
        Me.btnDeleteEnforcement.Size = New System.Drawing.Size(111, 23)
        Me.btnDeleteEnforcement.TabIndex = 8
        Me.btnDeleteEnforcement.Text = "Delete Enforcement"
        Me.btnDeleteEnforcement.UseVisualStyleBackColor = True
        '
        'DMUDangerousTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 306)
        Me.Controls.Add(Me.lblEisYear)
        Me.Controls.Add(Me.txtEISYear)
        Me.Controls.Add(Me.lblEisAirs)
        Me.Controls.Add(Me.btnDeleteEnforcement)
        Me.Controls.Add(Me.txtEISAIRSNumber)
        Me.Controls.Add(Me.txtEnforcementNumber)
        Me.Controls.Add(Me.PD_EIS_QASTART)
        Me.Controls.Add(Me.btnMoveAIRSData)
        Me.Controls.Add(Me.txtOldAIRS)
        Me.Controls.Add(Me.txtNewAIRS)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Name = "DMUDangerousTool"
        Me.Text = "DANGER! DANGER! DANGER!"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnDeleteEnforcement As System.Windows.Forms.Button
    Friend WithEvents txtNewAIRS As System.Windows.Forms.TextBox
    Friend WithEvents txtOldAIRS As System.Windows.Forms.TextBox
    Friend WithEvents btnMoveAIRSData As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PD_EIS_QASTART As System.Windows.Forms.Button
    Friend WithEvents lblEisYear As System.Windows.Forms.Label
    Friend WithEvents txtEISYear As System.Windows.Forms.TextBox
    Friend WithEvents lblEisAirs As System.Windows.Forms.Label
    Friend WithEvents txtEISAIRSNumber As System.Windows.Forms.TextBox
End Class
