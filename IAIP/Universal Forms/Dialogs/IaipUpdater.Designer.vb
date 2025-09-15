<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipUpdater
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
        Me.UpdaterStatus = New System.Windows.Forms.Label()
        Me.UpdaterButton = New System.Windows.Forms.Button()
        Me.DownloadProgress = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'UpdaterStatus
        '
        Me.UpdaterStatus.AutoSize = True
        Me.UpdaterStatus.Location = New System.Drawing.Point(34, 34)
        Me.UpdaterStatus.Name = "UpdaterStatus"
        Me.UpdaterStatus.Size = New System.Drawing.Size(114, 13)
        Me.UpdaterStatus.TabIndex = 1
        Me.UpdaterStatus.Text = "Downloading update..."
        '
        'UpdaterButton
        '
        Me.UpdaterButton.Location = New System.Drawing.Point(37, 105)
        Me.UpdaterButton.Name = "UpdaterButton"
        Me.UpdaterButton.Size = New System.Drawing.Size(75, 23)
        Me.UpdaterButton.TabIndex = 2
        Me.UpdaterButton.Text = "Close"
        Me.UpdaterButton.UseVisualStyleBackColor = True
        Me.UpdaterButton.Visible = False
        '
        'DownloadProgress
        '
        Me.DownloadProgress.Location = New System.Drawing.Point(37, 63)
        Me.DownloadProgress.Name = "DownloadProgress"
        Me.DownloadProgress.Size = New System.Drawing.Size(279, 23)
        Me.DownloadProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.DownloadProgress.TabIndex = 3
        '
        'IaipUpdater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(352, 161)
        Me.ControlBox = False
        Me.Controls.Add(Me.DownloadProgress)
        Me.Controls.Add(Me.UpdaterButton)
        Me.Controls.Add(Me.UpdaterStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "IaipUpdater"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IAIP Updating"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UpdaterStatus As Label
    Friend WithEvents UpdaterButton As Button
    Friend WithEvents DownloadProgress As ProgressBar
End Class
