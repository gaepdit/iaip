<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPAddPollutants
    Inherits BaseForm


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents dgrPollutants As System.Windows.Forms.DataGrid
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPollutantCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPollutant As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPollutantCode = New System.Windows.Forms.TextBox()
        Me.txtPollutant = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgrPollutants = New System.Windows.Forms.DataGrid()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.dgrPollutants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(191, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Pollutant Code:"
        '
        'txtPollutantCode
        '
        Me.txtPollutantCode.Location = New System.Drawing.Point(194, 35)
        Me.txtPollutantCode.Name = "txtPollutantCode"
        Me.txtPollutantCode.Size = New System.Drawing.Size(100, 20)
        Me.txtPollutantCode.TabIndex = 1
        '
        'txtPollutant
        '
        Me.txtPollutant.Location = New System.Drawing.Point(20, 35)
        Me.txtPollutant.Name = "txtPollutant"
        Me.txtPollutant.Size = New System.Drawing.Size(168, 20)
        Me.txtPollutant.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pollutant:"
        '
        'dgrPollutants
        '
        Me.dgrPollutants.DataMember = ""
        Me.dgrPollutants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrPollutants.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrPollutants.Location = New System.Drawing.Point(0, 78)
        Me.dgrPollutants.Name = "dgrPollutants"
        Me.dgrPollutants.ReadOnly = True
        Me.dgrPollutants.Size = New System.Drawing.Size(495, 323)
        Me.dgrPollutants.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(300, 33)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(102, 23)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save Pollutant"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.txtPollutant)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtPollutantCode)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(495, 78)
        Me.Panel1.TabIndex = 146
        '
        'ISMPAddPollutants
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(495, 401)
        Me.Controls.Add(Me.dgrPollutants)
        Me.Controls.Add(Me.Panel1)
        Me.MinimumSize = New System.Drawing.Size(437, 337)
        Me.Name = "ISMPAddPollutants"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Add Pollutants"
        CType(Me.dgrPollutants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnSave As Button
    Friend WithEvents Panel1 As Panel
End Class
