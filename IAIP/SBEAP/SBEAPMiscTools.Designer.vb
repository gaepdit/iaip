<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBEAPMiscTools
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
        Me.dgvMiscTools = New System.Windows.Forms.DataGridView()
        Me.pnlMiscTools = New System.Windows.Forms.Panel()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnGetContactData = New System.Windows.Forms.Button()
        CType(Me.dgvMiscTools, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMiscTools.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvMiscTools
        '
        Me.dgvMiscTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMiscTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMiscTools.Location = New System.Drawing.Point(0, 60)
        Me.dgvMiscTools.Name = "dgvMiscTools"
        Me.dgvMiscTools.Size = New System.Drawing.Size(682, 392)
        Me.dgvMiscTools.TabIndex = 12
        '
        'pnlMiscTools
        '
        Me.pnlMiscTools.Controls.Add(Me.btnExportToExcel)
        Me.pnlMiscTools.Controls.Add(Me.txtCount)
        Me.pnlMiscTools.Controls.Add(Me.lblCount)
        Me.pnlMiscTools.Controls.Add(Me.btnGetContactData)
        Me.pnlMiscTools.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMiscTools.Location = New System.Drawing.Point(0, 0)
        Me.pnlMiscTools.Name = "pnlMiscTools"
        Me.pnlMiscTools.Size = New System.Drawing.Size(682, 60)
        Me.pnlMiscTools.TabIndex = 13
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportToExcel.Image = Global.Iaip.My.Resources.Resources.SpreadsheetIcon
        Me.btnExportToExcel.Location = New System.Drawing.Point(546, 15)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(124, 27)
        Me.btnExportToExcel.TabIndex = 5
        Me.btnExportToExcel.Text = "Export to Excel"
        Me.btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'txtCount
        '
        Me.txtCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCount.Location = New System.Drawing.Point(440, 19)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(100, 20)
        Me.txtCount.TabIndex = 3
        '
        'lblCount
        '
        Me.lblCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(399, 22)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(35, 13)
        Me.lblCount.TabIndex = 2
        Me.lblCount.Text = "Count"
        '
        'btnGetContactData
        '
        Me.btnGetContactData.AutoSize = True
        Me.btnGetContactData.Location = New System.Drawing.Point(12, 15)
        Me.btnGetContactData.Name = "btnGetContactData"
        Me.btnGetContactData.Size = New System.Drawing.Size(100, 27)
        Me.btnGetContactData.TabIndex = 0
        Me.btnGetContactData.Text = "Get Contact Data"
        Me.btnGetContactData.UseVisualStyleBackColor = True
        '
        'SBEAPMiscTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(682, 452)
        Me.Controls.Add(Me.dgvMiscTools)
        Me.Controls.Add(Me.pnlMiscTools)
        Me.MinimumSize = New System.Drawing.Size(677, 341)
        Me.Name = "SBEAPMiscTools"
        Me.Text = "SBEAP Contact Data"
        CType(Me.dgvMiscTools, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMiscTools.ResumeLayout(False)
        Me.pnlMiscTools.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvMiscTools As System.Windows.Forms.DataGridView
    Friend WithEvents pnlMiscTools As System.Windows.Forms.Panel
    Friend WithEvents btnGetContactData As System.Windows.Forms.Button
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
End Class
