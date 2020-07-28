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
        Me.updaterStatus = New System.Windows.Forms.Label()
        Me.updaterButton = New System.Windows.Forms.Button()
        Me.downloadProgress = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'updaterStatus
        '
        Me.updaterStatus.AutoSize = True
        Me.updaterStatus.Location = New System.Drawing.Point(34, 34)
        Me.updaterStatus.Name = "updaterStatus"
        Me.updaterStatus.Size = New System.Drawing.Size(114, 13)
        Me.updaterStatus.TabIndex = 1
        Me.updaterStatus.Text = "Downloading update..."
        '
        'updaterButton
        '
        Me.updaterButton.Location = New System.Drawing.Point(37, 105)
        Me.updaterButton.Name = "updaterButton"
        Me.updaterButton.Size = New System.Drawing.Size(75, 23)
        Me.updaterButton.TabIndex = 2
        Me.updaterButton.Text = "Cancel"
        Me.updaterButton.UseVisualStyleBackColor = True
        '
        'downloadProgress
        '
        Me.downloadProgress.Location = New System.Drawing.Point(37, 63)
        Me.downloadProgress.Name = "downloadProgress"
        Me.downloadProgress.Size = New System.Drawing.Size(279, 23)
        Me.downloadProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.downloadProgress.TabIndex = 3
        '
        'IaipUpdater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(352, 161)
        Me.ControlBox = False
        Me.Controls.Add(Me.downloadProgress)
        Me.Controls.Add(Me.updaterButton)
        Me.Controls.Add(Me.updaterStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "IaipUpdater"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IAIP Updating"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents updaterStatus As Label
    Friend WithEvents updaterButton As Button
    Friend WithEvents downloadProgress As ProgressBar
End Class
