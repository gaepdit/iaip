<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipUserSecurity
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblIntro = New System.Windows.Forms.Label()
        Me.dgvSavedSessions = New System.Windows.Forms.DataGridView()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.btnRevokeAll = New System.Windows.Forms.Button()
        Me.BottomPanel = New System.Windows.Forms.Panel()
        Me.btnRevokeSelection = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        CType(Me.dgvSavedSessions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        Me.BottomPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblIntro
        '
        Me.lblIntro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIntro.Location = New System.Drawing.Point(12, 12)
        Me.lblIntro.Name = "lblIntro"
        Me.lblIntro.Size = New System.Drawing.Size(423, 46)
        Me.lblIntro.TabIndex = 1
        Me.lblIntro.Text = "This is a list of saved logins for your account. Revoke any sessions you do not r" &
    "ecognize or no longer need."
        '
        'dgvSavedSessions
        '
        Me.dgvSavedSessions.AllowUserToAddRows = False
        Me.dgvSavedSessions.AllowUserToDeleteRows = False
        Me.dgvSavedSessions.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvSavedSessions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSavedSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSavedSessions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSavedSessions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvSavedSessions.Location = New System.Drawing.Point(0, 64)
        Me.dgvSavedSessions.MultiSelect = False
        Me.dgvSavedSessions.Name = "dgvSavedSessions"
        Me.dgvSavedSessions.ReadOnly = True
        Me.dgvSavedSessions.RowHeadersVisible = False
        Me.dgvSavedSessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSavedSessions.Size = New System.Drawing.Size(560, 366)
        Me.dgvSavedSessions.TabIndex = 1
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.btnRevokeAll)
        Me.TopPanel.Controls.Add(Me.lblIntro)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(560, 64)
        Me.TopPanel.TabIndex = 0
        '
        'btnRevokeAll
        '
        Me.btnRevokeAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRevokeAll.Enabled = False
        Me.btnRevokeAll.Location = New System.Drawing.Point(441, 12)
        Me.btnRevokeAll.Name = "btnRevokeAll"
        Me.btnRevokeAll.Size = New System.Drawing.Size(107, 36)
        Me.btnRevokeAll.TabIndex = 0
        Me.btnRevokeAll.Text = "Revoke all sessions"
        Me.btnRevokeAll.UseVisualStyleBackColor = True
        '
        'BottomPanel
        '
        Me.BottomPanel.Controls.Add(Me.btnRevokeSelection)
        Me.BottomPanel.Controls.Add(Me.lblStatus)
        Me.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BottomPanel.Location = New System.Drawing.Point(0, 430)
        Me.BottomPanel.Name = "BottomPanel"
        Me.BottomPanel.Size = New System.Drawing.Size(560, 58)
        Me.BottomPanel.TabIndex = 2
        '
        'btnRevokeSelection
        '
        Me.btnRevokeSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRevokeSelection.Enabled = False
        Me.btnRevokeSelection.Location = New System.Drawing.Point(441, 10)
        Me.btnRevokeSelection.Name = "btnRevokeSelection"
        Me.btnRevokeSelection.Size = New System.Drawing.Size(107, 36)
        Me.btnRevokeSelection.TabIndex = 2
        Me.btnRevokeSelection.Text = "Revoke selected session"
        Me.btnRevokeSelection.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.Location = New System.Drawing.Point(12, 10)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(423, 47)
        Me.lblStatus.TabIndex = 1
        '
        'IaipUserSecurity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(560, 488)
        Me.Controls.Add(Me.dgvSavedSessions)
        Me.Controls.Add(Me.TopPanel)
        Me.Controls.Add(Me.BottomPanel)
        Me.MinimumSize = New System.Drawing.Size(327, 289)
        Me.Name = "IaipUserSecurity"
        Me.Text = "User Saved Logins"
        CType(Me.dgvSavedSessions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.BottomPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblIntro As Label
    Friend WithEvents dgvSavedSessions As DataGridView
    Friend WithEvents TopPanel As Panel
    Friend WithEvents btnRevokeAll As Button
    Friend WithEvents BottomPanel As Panel
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnRevokeSelection As Button
End Class
