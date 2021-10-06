<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IaipDataGridView
    Inherits System.Windows.Forms.DataGridView

    'Control overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Control Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  Do not modify it
    ' using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ExportToExcelButton = New System.Windows.Forms.Button()
        Me.ButtonToolTip = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExportToExcelButton
        '
        Me.ExportToExcelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExportToExcelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.ExportToExcelButton.Image = Global.Iaip.My.Resources.Resources.SpreadsheetIcon
        Me.ExportToExcelButton.Location = New System.Drawing.Point(0, 0)
        Me.ExportToExcelButton.Name = "ExportToExcelButton"
        Me.ExportToExcelButton.Size = New System.Drawing.Size(75, 23)
        Me.ExportToExcelButton.TabIndex = 0
        Me.ButtonToolTip.SetToolTip(Me.ExportToExcelButton, "Export to Excel")
        Me.ExportToExcelButton.UseVisualStyleBackColor = True
        '
        'IaipDataGridView
        '
        Me.AllowUserToAddRows = False
        Me.AllowUserToDeleteRows = False
        Me.AllowUserToOrderColumns = True
        Me.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GridColor = System.Drawing.SystemColors.ControlLight
        Me.MultiSelect = False
        Me.ReadOnly = True
        Me.RowHeadersVisible = False
        Me.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.StandardTab = True
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExportToExcelButton As Button
    Friend WithEvents ButtonToolTip As ToolTip
End Class

