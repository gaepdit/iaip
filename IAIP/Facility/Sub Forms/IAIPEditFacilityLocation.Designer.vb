<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPEditFacilityLocation
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvFacilityInformationHistory = New System.Windows.Forms.DataGridView()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.labReferenceNumber = New System.Windows.Forms.Label()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStreetAddress = New System.Windows.Forms.TextBox()
        Me.txtStreetAddress2 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtFacilityCity = New System.Windows.Forms.TextBox()
        Me.txtFacilityLatitude = New System.Windows.Forms.TextBox()
        Me.txtFacilityLongitude = New System.Windows.Forms.TextBox()
        Me.txtFacilityState = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mtbFacilityZipCode = New System.Windows.Forms.MaskedTextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.SaveButton = New System.Windows.Forms.ToolStripButton()
        Me.lblAirsNumber = New System.Windows.Forms.Label()
        Me.lblModifiedNote = New System.Windows.Forms.Label()
        CType(Me.dgvFacilityInformationHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvFacilityInformationHistory
        '
        Me.dgvFacilityInformationHistory.AllowUserToAddRows = False
        Me.dgvFacilityInformationHistory.AllowUserToDeleteRows = False
        Me.dgvFacilityInformationHistory.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvFacilityInformationHistory.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvFacilityInformationHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFacilityInformationHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFacilityInformationHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvFacilityInformationHistory.Location = New System.Drawing.Point(2, 274)
        Me.dgvFacilityInformationHistory.Name = "dgvFacilityInformationHistory"
        Me.dgvFacilityInformationHistory.ReadOnly = True
        Me.dgvFacilityInformationHistory.RowHeadersVisible = False
        Me.dgvFacilityInformationHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFacilityInformationHistory.Size = New System.Drawing.Size(490, 270)
        Me.dgvFacilityInformationHistory.TabIndex = 227
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = True
        Me.txtComments.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(87, 185)
        Me.txtComments.MaxLength = 4000
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(393, 55)
        Me.txtComments.TabIndex = 226
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 188)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 225
        Me.Label3.Text = "Comments"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'labReferenceNumber
        '
        Me.labReferenceNumber.AutoSize = True
        Me.labReferenceNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labReferenceNumber.Location = New System.Drawing.Point(5, 58)
        Me.labReferenceNumber.Name = "labReferenceNumber"
        Me.labReferenceNumber.Size = New System.Drawing.Size(70, 13)
        Me.labReferenceNumber.TabIndex = 216
        Me.labReferenceNumber.Text = "Facility Name"
        Me.labReferenceNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityName.Location = New System.Drawing.Point(87, 55)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.Size = New System.Drawing.Size(393, 20)
        Me.txtFacilityName.TabIndex = 217
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 205
        Me.Label4.Text = "Street Address"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtStreetAddress
        '
        Me.txtStreetAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStreetAddress.Location = New System.Drawing.Point(87, 81)
        Me.txtStreetAddress.MaxLength = 250
        Me.txtStreetAddress.Name = "txtStreetAddress"
        Me.txtStreetAddress.Size = New System.Drawing.Size(393, 20)
        Me.txtStreetAddress.TabIndex = 207
        '
        'txtStreetAddress2
        '
        Me.txtStreetAddress2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStreetAddress2.Location = New System.Drawing.Point(87, 107)
        Me.txtStreetAddress2.MaxLength = 250
        Me.txtStreetAddress2.Name = "txtStreetAddress2"
        Me.txtStreetAddress2.Size = New System.Drawing.Size(393, 20)
        Me.txtStreetAddress2.TabIndex = 208
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 13)
        Me.Label5.TabIndex = 206
        Me.Label5.Text = "City"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(194, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 218
        Me.Label2.Text = "State"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(265, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 13)
        Me.Label6.TabIndex = 219
        Me.Label6.Text = "Zip Code"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(5, 162)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 213
        Me.Label12.Text = "Latitude"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(194, 162)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(54, 13)
        Me.Label11.TabIndex = 212
        Me.Label11.Text = "Longitude"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtFacilityCity
        '
        Me.txtFacilityCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityCity.Location = New System.Drawing.Point(87, 133)
        Me.txtFacilityCity.Name = "txtFacilityCity"
        Me.txtFacilityCity.Size = New System.Drawing.Size(101, 20)
        Me.txtFacilityCity.TabIndex = 209
        '
        'txtFacilityLatitude
        '
        Me.txtFacilityLatitude.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityLatitude.Location = New System.Drawing.Point(87, 159)
        Me.txtFacilityLatitude.MaxLength = 20
        Me.txtFacilityLatitude.Name = "txtFacilityLatitude"
        Me.txtFacilityLatitude.Size = New System.Drawing.Size(101, 20)
        Me.txtFacilityLatitude.TabIndex = 215
        '
        'txtFacilityLongitude
        '
        Me.txtFacilityLongitude.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityLongitude.Location = New System.Drawing.Point(254, 159)
        Me.txtFacilityLongitude.MaxLength = 20
        Me.txtFacilityLongitude.Name = "txtFacilityLongitude"
        Me.txtFacilityLongitude.Size = New System.Drawing.Size(101, 20)
        Me.txtFacilityLongitude.TabIndex = 214
        '
        'txtFacilityState
        '
        Me.txtFacilityState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityState.Location = New System.Drawing.Point(232, 133)
        Me.txtFacilityState.MaxLength = 2
        Me.txtFacilityState.Name = "txtFacilityState"
        Me.txtFacilityState.Size = New System.Drawing.Size(27, 20)
        Me.txtFacilityState.TabIndex = 211
        Me.txtFacilityState.Text = "GA"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 203
        Me.Label1.Text = "AIRS Number"
        '
        'mtbFacilityZipCode
        '
        Me.mtbFacilityZipCode.Location = New System.Drawing.Point(321, 133)
        Me.mtbFacilityZipCode.Mask = "00000-9999"
        Me.mtbFacilityZipCode.Name = "mtbFacilityZipCode"
        Me.mtbFacilityZipCode.Size = New System.Drawing.Size(68, 20)
        Me.mtbFacilityZipCode.TabIndex = 367
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(492, 25)
        Me.ToolStrip1.TabIndex = 368
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'SaveButton
        '
        Me.SaveButton.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(51, 22)
        Me.SaveButton.Text = "&Save"
        '
        'lblAirsNumber
        '
        Me.lblAirsNumber.AutoSize = True
        Me.lblAirsNumber.Location = New System.Drawing.Point(86, 31)
        Me.lblAirsNumber.Name = "lblAirsNumber"
        Me.lblAirsNumber.Size = New System.Drawing.Size(0, 13)
        Me.lblAirsNumber.TabIndex = 369
        '
        'lblModifiedNote
        '
        Me.lblModifiedNote.AutoSize = True
        Me.lblModifiedNote.Location = New System.Drawing.Point(86, 249)
        Me.lblModifiedNote.Name = "lblModifiedNote"
        Me.lblModifiedNote.Size = New System.Drawing.Size(0, 13)
        Me.lblModifiedNote.TabIndex = 370
        '
        'IAIPEditFacilityLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 545)
        Me.Controls.Add(Me.lblModifiedNote)
        Me.Controls.Add(Me.lblAirsNumber)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.mtbFacilityZipCode)
        Me.Controls.Add(Me.dgvFacilityInformationHistory)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.labReferenceNumber)
        Me.Controls.Add(Me.txtFacilityName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtStreetAddress)
        Me.Controls.Add(Me.txtStreetAddress2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtFacilityCity)
        Me.Controls.Add(Me.txtFacilityLatitude)
        Me.Controls.Add(Me.txtFacilityLongitude)
        Me.Controls.Add(Me.txtFacilityState)
        Me.Controls.Add(Me.Label1)
        Me.Name = "IAIPEditFacilityLocation"
        Me.Text = "Edit Facility Location"
        CType(Me.dgvFacilityInformationHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvFacilityInformationHistory As System.Windows.Forms.DataGridView
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents labReferenceNumber As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtStreetAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityLatitude As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityLongitude As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityState As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mtbFacilityZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblAirsNumber As Label
    Friend WithEvents lblModifiedNote As Label
End Class
