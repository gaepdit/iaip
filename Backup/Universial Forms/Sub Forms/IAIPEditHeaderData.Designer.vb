<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPEditHeaderData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPEditHeaderData))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mmiSave = New System.Windows.Forms.MenuItem
        Me.mmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TBEditHeaderData = New System.Windows.Forms.ToolBar
        Me.TbbSave = New System.Windows.Forms.ToolBarButton
        Me.TbbBack = New System.Windows.Forms.ToolBarButton
        Me.txtPlantDescription = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtKey = New System.Windows.Forms.TextBox
        Me.dgvHeaderDataHistory = New System.Windows.Forms.DataGridView
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DTPStartUpDate = New System.Windows.Forms.DateTimePicker
        Me.DTPShutdown = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboClassification = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboOperationalStatus = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSICCode = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chbAPCRMP = New System.Windows.Forms.CheckBox
        Me.chbHAPsMajor = New System.Windows.Forms.CheckBox
        Me.chbNSRMajor = New System.Windows.Forms.CheckBox
        Me.chbAPCA = New System.Windows.Forms.CheckBox
        Me.chbAPCF = New System.Windows.Forms.CheckBox
        Me.chbAPCV = New System.Windows.Forms.CheckBox
        Me.chbAPCM = New System.Windows.Forms.CheckBox
        Me.chbAPCI = New System.Windows.Forms.CheckBox
        Me.chbAPC9 = New System.Windows.Forms.CheckBox
        Me.chbAPC8 = New System.Windows.Forms.CheckBox
        Me.chbAPC7 = New System.Windows.Forms.CheckBox
        Me.chbAPC6 = New System.Windows.Forms.CheckBox
        Me.chbAPC4 = New System.Windows.Forms.CheckBox
        Me.chbAPC3 = New System.Windows.Forms.CheckBox
        Me.chbAPC1 = New System.Windows.Forms.CheckBox
        Me.chbAPC0 = New System.Windows.Forms.CheckBox
        Me.txtModifingComments = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.labReferenceNumber = New System.Windows.Forms.Label
        Me.txtAirsNumber = New System.Windows.Forms.TextBox
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.llbCurrentData = New System.Windows.Forms.LinkLabel
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.cboPMFine = New System.Windows.Forms.ComboBox
        Me.cboEightHour = New System.Windows.Forms.ComboBox
        Me.cboOneHour = New System.Windows.Forms.ComboBox
        Me.Label97 = New System.Windows.Forms.Label
        Me.Label96 = New System.Windows.Forms.Label
        Me.Label94 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtNAICSCode = New System.Windows.Forms.TextBox
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgvHeaderDataHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiSave, Me.mmiBack})
        Me.MenuItem1.Text = "File"
        '
        'mmiSave
        '
        Me.mmiSave.Index = 0
        Me.mmiSave.Text = "Save"
        '
        'mmiBack
        '
        Me.mmiBack.Index = 1
        Me.mmiBack.Text = "Back"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste})
        Me.MenuItem2.Text = "Edit"
        '
        'mmiCut
        '
        Me.mmiCut.Index = 0
        Me.mmiCut.Text = "Cut"
        '
        'mmiCopy
        '
        Me.mmiCopy.Index = 1
        Me.mmiCopy.Text = "Copy"
        '
        'mmiPaste
        '
        Me.mmiPaste.Index = 2
        Me.mmiPaste.Text = "Paste"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
        Me.mmiHelp.Text = "Help"
        '
        'Image_List_All
        '
        Me.Image_List_All.ImageStream = CType(resources.GetObject("Image_List_All.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Image_List_All.TransparentColor = System.Drawing.Color.Transparent
        Me.Image_List_All.Images.SetKeyName(0, "")
        Me.Image_List_All.Images.SetKeyName(1, "")
        Me.Image_List_All.Images.SetKeyName(2, "")
        Me.Image_List_All.Images.SetKeyName(3, "")
        Me.Image_List_All.Images.SetKeyName(4, "")
        Me.Image_List_All.Images.SetKeyName(5, "")
        Me.Image_List_All.Images.SetKeyName(6, "")
        Me.Image_List_All.Images.SetKeyName(7, "")
        Me.Image_List_All.Images.SetKeyName(8, "")
        Me.Image_List_All.Images.SetKeyName(9, "")
        Me.Image_List_All.Images.SetKeyName(10, "")
        Me.Image_List_All.Images.SetKeyName(11, "")
        Me.Image_List_All.Images.SetKeyName(12, "")
        Me.Image_List_All.Images.SetKeyName(13, "")
        Me.Image_List_All.Images.SetKeyName(14, "")
        Me.Image_List_All.Images.SetKeyName(15, "")
        Me.Image_List_All.Images.SetKeyName(16, "")
        Me.Image_List_All.Images.SetKeyName(17, "")
        Me.Image_List_All.Images.SetKeyName(18, "")
        Me.Image_List_All.Images.SetKeyName(19, "")
        Me.Image_List_All.Images.SetKeyName(20, "")
        Me.Image_List_All.Images.SetKeyName(21, "")
        Me.Image_List_All.Images.SetKeyName(22, "")
        Me.Image_List_All.Images.SetKeyName(23, "")
        Me.Image_List_All.Images.SetKeyName(24, "")
        Me.Image_List_All.Images.SetKeyName(25, "")
        Me.Image_List_All.Images.SetKeyName(26, "")
        Me.Image_List_All.Images.SetKeyName(27, "")
        Me.Image_List_All.Images.SetKeyName(28, "")
        Me.Image_List_All.Images.SetKeyName(29, "")
        Me.Image_List_All.Images.SetKeyName(30, "")
        Me.Image_List_All.Images.SetKeyName(31, "")
        Me.Image_List_All.Images.SetKeyName(32, "")
        Me.Image_List_All.Images.SetKeyName(33, "")
        Me.Image_List_All.Images.SetKeyName(34, "")
        Me.Image_List_All.Images.SetKeyName(35, "")
        Me.Image_List_All.Images.SetKeyName(36, "")
        Me.Image_List_All.Images.SetKeyName(37, "")
        Me.Image_List_All.Images.SetKeyName(38, "")
        Me.Image_List_All.Images.SetKeyName(39, "")
        Me.Image_List_All.Images.SetKeyName(40, "")
        Me.Image_List_All.Images.SetKeyName(41, "")
        Me.Image_List_All.Images.SetKeyName(42, "")
        Me.Image_List_All.Images.SetKeyName(43, "")
        Me.Image_List_All.Images.SetKeyName(44, "")
        Me.Image_List_All.Images.SetKeyName(45, "")
        Me.Image_List_All.Images.SetKeyName(46, "")
        Me.Image_List_All.Images.SetKeyName(47, "")
        Me.Image_List_All.Images.SetKeyName(48, "")
        Me.Image_List_All.Images.SetKeyName(49, "")
        Me.Image_List_All.Images.SetKeyName(50, "")
        Me.Image_List_All.Images.SetKeyName(51, "")
        Me.Image_List_All.Images.SetKeyName(52, "")
        Me.Image_List_All.Images.SetKeyName(53, "")
        Me.Image_List_All.Images.SetKeyName(54, "")
        Me.Image_List_All.Images.SetKeyName(55, "")
        Me.Image_List_All.Images.SetKeyName(56, "")
        Me.Image_List_All.Images.SetKeyName(57, "")
        Me.Image_List_All.Images.SetKeyName(58, "")
        Me.Image_List_All.Images.SetKeyName(59, "")
        Me.Image_List_All.Images.SetKeyName(60, "")
        Me.Image_List_All.Images.SetKeyName(61, "")
        Me.Image_List_All.Images.SetKeyName(62, "")
        Me.Image_List_All.Images.SetKeyName(63, "")
        Me.Image_List_All.Images.SetKeyName(64, "")
        Me.Image_List_All.Images.SetKeyName(65, "")
        Me.Image_List_All.Images.SetKeyName(66, "")
        Me.Image_List_All.Images.SetKeyName(67, "")
        Me.Image_List_All.Images.SetKeyName(68, "")
        Me.Image_List_All.Images.SetKeyName(69, "")
        Me.Image_List_All.Images.SetKeyName(70, "")
        Me.Image_List_All.Images.SetKeyName(71, "")
        Me.Image_List_All.Images.SetKeyName(72, "")
        Me.Image_List_All.Images.SetKeyName(73, "")
        Me.Image_List_All.Images.SetKeyName(74, "")
        Me.Image_List_All.Images.SetKeyName(75, "")
        Me.Image_List_All.Images.SetKeyName(76, "")
        Me.Image_List_All.Images.SetKeyName(77, "")
        Me.Image_List_All.Images.SetKeyName(78, "")
        Me.Image_List_All.Images.SetKeyName(79, "")
        Me.Image_List_All.Images.SetKeyName(80, "")
        Me.Image_List_All.Images.SetKeyName(81, "")
        Me.Image_List_All.Images.SetKeyName(82, "")
        Me.Image_List_All.Images.SetKeyName(83, "")
        Me.Image_List_All.Images.SetKeyName(84, "")
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 523)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(612, 22)
        Me.StatusStrip1.TabIndex = 201
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(487, 17)
        Me.Panel1.Spring = True
        '
        'Panel2
        '
        Me.Panel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(4, 17)
        '
        'Panel3
        '
        Me.Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(4, 17)
        '
        'TBEditHeaderData
        '
        Me.TBEditHeaderData.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbSave, Me.TbbBack})
        Me.TBEditHeaderData.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBEditHeaderData.DropDownArrows = True
        Me.TBEditHeaderData.ImageList = Me.Image_List_All
        Me.TBEditHeaderData.Location = New System.Drawing.Point(0, 0)
        Me.TBEditHeaderData.Name = "TBEditHeaderData"
        Me.TBEditHeaderData.ShowToolTips = True
        Me.TBEditHeaderData.Size = New System.Drawing.Size(612, 28)
        Me.TBEditHeaderData.TabIndex = 202
        '
        'TbbSave
        '
        Me.TbbSave.ImageIndex = 65
        Me.TbbSave.Name = "TbbSave"
        Me.TbbSave.ToolTipText = "Save"
        '
        'TbbBack
        '
        Me.TbbBack.ImageIndex = 2
        Me.TbbBack.Name = "TbbBack"
        Me.TbbBack.ToolTipText = "Back"
        '
        'txtPlantDescription
        '
        Me.txtPlantDescription.AcceptsReturn = True
        Me.txtPlantDescription.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtPlantDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlantDescription.Location = New System.Drawing.Point(95, 249)
        Me.txtPlantDescription.MaxLength = 4000
        Me.txtPlantDescription.Multiline = True
        Me.txtPlantDescription.Name = "txtPlantDescription"
        Me.txtPlantDescription.Size = New System.Drawing.Size(499, 20)
        Me.txtPlantDescription.TabIndex = 377
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 253)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 13)
        Me.Label7.TabIndex = 376
        Me.Label7.Text = "Plant Description:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'txtKey
        '
        Me.txtKey.Location = New System.Drawing.Point(353, 25)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(10, 20)
        Me.txtKey.TabIndex = 375
        Me.txtKey.Visible = False
        '
        'dgvHeaderDataHistory
        '
        Me.dgvHeaderDataHistory.AllowUserToOrderColumns = True
        Me.dgvHeaderDataHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvHeaderDataHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHeaderDataHistory.Location = New System.Drawing.Point(0, 366)
        Me.dgvHeaderDataHistory.Name = "dgvHeaderDataHistory"
        Me.dgvHeaderDataHistory.ReadOnly = True
        Me.dgvHeaderDataHistory.Size = New System.Drawing.Size(612, 153)
        Me.dgvHeaderDataHistory.TabIndex = 374
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = True
        Me.txtComments.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtComments.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(77, 275)
        Me.txtComments.MaxLength = 4000
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(517, 59)
        Me.txtComments.TabIndex = 373
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 275)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 372
        Me.Label6.Text = "Comments:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(236, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 13)
        Me.Label5.TabIndex = 371
        Me.Label5.Text = "Procedural Shut Down"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 370
        Me.Label1.Text = "Date Start Up"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'DTPStartUpDate
        '
        Me.DTPStartUpDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPStartUpDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPStartUpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPStartUpDate.Location = New System.Drawing.Point(97, 98)
        Me.DTPStartUpDate.Name = "DTPStartUpDate"
        Me.DTPStartUpDate.ShowCheckBox = True
        Me.DTPStartUpDate.Size = New System.Drawing.Size(120, 22)
        Me.DTPStartUpDate.TabIndex = 369
        Me.DTPStartUpDate.Value = New Date(2006, 11, 3, 0, 0, 0, 0)
        '
        'DTPShutdown
        '
        Me.DTPShutdown.CustomFormat = "dd-MMM-yyyy"
        Me.DTPShutdown.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPShutdown.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPShutdown.Location = New System.Drawing.Point(353, 98)
        Me.DTPShutdown.Name = "DTPShutdown"
        Me.DTPShutdown.ShowCheckBox = True
        Me.DTPShutdown.Size = New System.Drawing.Size(124, 22)
        Me.DTPShutdown.TabIndex = 368
        Me.DTPShutdown.Value = New Date(2006, 11, 3, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 356
        Me.Label3.Text = "Classification:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboClassification
        '
        Me.cboClassification.Location = New System.Drawing.Point(79, 73)
        Me.cboClassification.Name = "cboClassification"
        Me.cboClassification.Size = New System.Drawing.Size(101, 21)
        Me.cboClassification.TabIndex = 363
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(186, 77)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 13)
        Me.Label10.TabIndex = 359
        Me.Label10.Text = "Operating Status:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboOperationalStatus
        '
        Me.cboOperationalStatus.Location = New System.Drawing.Point(279, 73)
        Me.cboOperationalStatus.Name = "cboOperationalStatus"
        Me.cboOperationalStatus.Size = New System.Drawing.Size(156, 21)
        Me.cboOperationalStatus.TabIndex = 364
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(459, 53)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 357
        Me.Label8.Text = "SIC Code:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtSICCode
        '
        Me.txtSICCode.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtSICCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSICCode.Location = New System.Drawing.Point(517, 50)
        Me.txtSICCode.Name = "txtSICCode"
        Me.txtSICCode.Size = New System.Drawing.Size(71, 20)
        Me.txtSICCode.TabIndex = 360
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chbAPCRMP)
        Me.GroupBox2.Controls.Add(Me.chbHAPsMajor)
        Me.GroupBox2.Controls.Add(Me.chbNSRMajor)
        Me.GroupBox2.Controls.Add(Me.chbAPCA)
        Me.GroupBox2.Controls.Add(Me.chbAPCF)
        Me.GroupBox2.Controls.Add(Me.chbAPCV)
        Me.GroupBox2.Controls.Add(Me.chbAPCM)
        Me.GroupBox2.Controls.Add(Me.chbAPCI)
        Me.GroupBox2.Controls.Add(Me.chbAPC9)
        Me.GroupBox2.Controls.Add(Me.chbAPC8)
        Me.GroupBox2.Controls.Add(Me.chbAPC7)
        Me.GroupBox2.Controls.Add(Me.chbAPC6)
        Me.GroupBox2.Controls.Add(Me.chbAPC4)
        Me.GroupBox2.Controls.Add(Me.chbAPC3)
        Me.GroupBox2.Controls.Add(Me.chbAPC1)
        Me.GroupBox2.Controls.Add(Me.chbAPC0)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(8, 122)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(404, 119)
        Me.GroupBox2.TabIndex = 362
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Air Program Code(s) "
        '
        'chbAPCRMP
        '
        Me.chbAPCRMP.AutoSize = True
        Me.chbAPCRMP.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chbAPCRMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCRMP.Location = New System.Drawing.Point(271, 32)
        Me.chbAPCRMP.Margin = New System.Windows.Forms.Padding(2)
        Me.chbAPCRMP.Name = "chbAPCRMP"
        Me.chbAPCRMP.Size = New System.Drawing.Size(112, 30)
        Me.chbAPCRMP.TabIndex = 147
        Me.chbAPCRMP.Text = "RMP - " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Risk Mgmt. Plan"
        '
        'chbHAPsMajor
        '
        Me.chbHAPsMajor.AutoSize = True
        Me.chbHAPsMajor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbHAPsMajor.Location = New System.Drawing.Point(271, 80)
        Me.chbHAPsMajor.Margin = New System.Windows.Forms.Padding(2)
        Me.chbHAPsMajor.Name = "chbHAPsMajor"
        Me.chbHAPsMajor.Size = New System.Drawing.Size(82, 17)
        Me.chbHAPsMajor.TabIndex = 146
        Me.chbHAPsMajor.Text = "HAPs Major"
        '
        'chbNSRMajor
        '
        Me.chbNSRMajor.AutoSize = True
        Me.chbNSRMajor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbNSRMajor.Location = New System.Drawing.Point(271, 64)
        Me.chbNSRMajor.Margin = New System.Windows.Forms.Padding(2)
        Me.chbNSRMajor.Name = "chbNSRMajor"
        Me.chbNSRMajor.Size = New System.Drawing.Size(105, 17)
        Me.chbNSRMajor.TabIndex = 145
        Me.chbNSRMajor.Text = "NSR/PSD Major"
        '
        'chbAPCA
        '
        Me.chbAPCA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCA.Location = New System.Drawing.Point(126, 48)
        Me.chbAPCA.Name = "chbAPCA"
        Me.chbAPCA.Size = New System.Drawing.Size(131, 16)
        Me.chbAPCA.TabIndex = 142
        Me.chbAPCA.Text = "A - Acid Precipitation"
        '
        'chbAPCF
        '
        Me.chbAPCF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCF.Location = New System.Drawing.Point(126, 64)
        Me.chbAPCF.Name = "chbAPCF"
        Me.chbAPCF.Size = New System.Drawing.Size(128, 16)
        Me.chbAPCF.TabIndex = 140
        Me.chbAPCF.Text = "F - FESOP"
        '
        'chbAPCV
        '
        Me.chbAPCV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCV.Location = New System.Drawing.Point(271, 16)
        Me.chbAPCV.Name = "chbAPCV"
        Me.chbAPCV.Size = New System.Drawing.Size(120, 16)
        Me.chbAPCV.TabIndex = 139
        Me.chbAPCV.Text = "V - Title V"
        '
        'chbAPCM
        '
        Me.chbAPCM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCM.Location = New System.Drawing.Point(126, 97)
        Me.chbAPCM.Name = "chbAPCM"
        Me.chbAPCM.Size = New System.Drawing.Size(120, 16)
        Me.chbAPCM.TabIndex = 138
        Me.chbAPCM.Text = "M - MACT (Part 63)"
        '
        'chbAPCI
        '
        Me.chbAPCI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPCI.Location = New System.Drawing.Point(126, 80)
        Me.chbAPCI.Name = "chbAPCI"
        Me.chbAPCI.Size = New System.Drawing.Size(120, 16)
        Me.chbAPCI.TabIndex = 137
        Me.chbAPCI.Text = "I - Native American"
        '
        'chbAPC9
        '
        Me.chbAPC9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC9.Location = New System.Drawing.Point(126, 32)
        Me.chbAPC9.Name = "chbAPC9"
        Me.chbAPC9.Size = New System.Drawing.Size(128, 16)
        Me.chbAPC9.TabIndex = 136
        Me.chbAPC9.Text = "9 - NSPS"
        '
        'chbAPC8
        '
        Me.chbAPC8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC8.Location = New System.Drawing.Point(126, 16)
        Me.chbAPC8.Name = "chbAPC8"
        Me.chbAPC8.Size = New System.Drawing.Size(128, 16)
        Me.chbAPC8.TabIndex = 135
        Me.chbAPC8.Text = "8 - NESHAP (Part 61)"
        '
        'chbAPC7
        '
        Me.chbAPC7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC7.Location = New System.Drawing.Point(8, 97)
        Me.chbAPC7.Name = "chbAPC7"
        Me.chbAPC7.Size = New System.Drawing.Size(128, 16)
        Me.chbAPC7.TabIndex = 134
        Me.chbAPC7.Text = "7 - NSR"
        '
        'chbAPC6
        '
        Me.chbAPC6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC6.Location = New System.Drawing.Point(8, 80)
        Me.chbAPC6.Name = "chbAPC6"
        Me.chbAPC6.Size = New System.Drawing.Size(128, 16)
        Me.chbAPC6.TabIndex = 133
        Me.chbAPC6.Text = "6 - PSD"
        '
        'chbAPC4
        '
        Me.chbAPC4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC4.Location = New System.Drawing.Point(8, 64)
        Me.chbAPC4.Name = "chbAPC4"
        Me.chbAPC4.Size = New System.Drawing.Size(128, 16)
        Me.chbAPC4.TabIndex = 132
        Me.chbAPC4.Text = "4 - CFC Tracking"
        '
        'chbAPC3
        '
        Me.chbAPC3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC3.Location = New System.Drawing.Point(8, 48)
        Me.chbAPC3.Name = "chbAPC3"
        Me.chbAPC3.Size = New System.Drawing.Size(128, 16)
        Me.chbAPC3.TabIndex = 131
        Me.chbAPC3.Text = "3 - Non-Federal SIP"
        '
        'chbAPC1
        '
        Me.chbAPC1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC1.Location = New System.Drawing.Point(8, 32)
        Me.chbAPC1.Name = "chbAPC1"
        Me.chbAPC1.Size = New System.Drawing.Size(128, 16)
        Me.chbAPC1.TabIndex = 130
        Me.chbAPC1.Text = "1 - Federal SIP"
        '
        'chbAPC0
        '
        Me.chbAPC0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbAPC0.Location = New System.Drawing.Point(8, 16)
        Me.chbAPC0.Name = "chbAPC0"
        Me.chbAPC0.Size = New System.Drawing.Size(128, 16)
        Me.chbAPC0.TabIndex = 129
        Me.chbAPC0.Text = "0 - SIP"
        '
        'txtModifingComments
        '
        Me.txtModifingComments.Location = New System.Drawing.Point(75, 340)
        Me.txtModifingComments.Name = "txtModifingComments"
        Me.txtModifingComments.ReadOnly = True
        Me.txtModifingComments.Size = New System.Drawing.Size(519, 20)
        Me.txtModifingComments.TabIndex = 365
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 343)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 366
        Me.Label4.Text = "Last Modified:"
        '
        'labReferenceNumber
        '
        Me.labReferenceNumber.AutoSize = True
        Me.labReferenceNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labReferenceNumber.Location = New System.Drawing.Point(5, 29)
        Me.labReferenceNumber.Name = "labReferenceNumber"
        Me.labReferenceNumber.Size = New System.Drawing.Size(73, 13)
        Me.labReferenceNumber.TabIndex = 351
        Me.labReferenceNumber.Text = "Facility Name:"
        Me.labReferenceNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAirsNumber
        '
        Me.txtAirsNumber.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtAirsNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAirsNumber.Location = New System.Drawing.Point(79, 46)
        Me.txtAirsNumber.MaxLength = 8
        Me.txtAirsNumber.Name = "txtAirsNumber"
        Me.txtAirsNumber.ReadOnly = True
        Me.txtAirsNumber.Size = New System.Drawing.Size(71, 20)
        Me.txtAirsNumber.TabIndex = 354
        '
        'txtFacilityName
        '
        Me.txtFacilityName.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtFacilityName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityName.Location = New System.Drawing.Point(79, 25)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(271, 20)
        Me.txtFacilityName.TabIndex = 353
        '
        'llbCurrentData
        '
        Me.llbCurrentData.AutoSize = True
        Me.llbCurrentData.Location = New System.Drawing.Point(369, 25)
        Me.llbCurrentData.Name = "llbCurrentData"
        Me.llbCurrentData.Size = New System.Drawing.Size(93, 13)
        Me.llbCurrentData.TabIndex = 355
        Me.llbCurrentData.TabStop = True
        Me.llbCurrentData.Text = "View Current Data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 352
        Me.Label2.Text = "AIRS Number:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cboPMFine)
        Me.GroupBox5.Controls.Add(Me.cboEightHour)
        Me.GroupBox5.Controls.Add(Me.cboOneHour)
        Me.GroupBox5.Controls.Add(Me.Label97)
        Me.GroupBox5.Controls.Add(Me.Label96)
        Me.GroupBox5.Controls.Add(Me.Label94)
        Me.GroupBox5.Location = New System.Drawing.Point(418, 122)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(182, 119)
        Me.GroupBox5.TabIndex = 378
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Non attainment"
        '
        'cboPMFine
        '
        Me.cboPMFine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPMFine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPMFine.FormattingEnabled = True
        Me.cboPMFine.Location = New System.Drawing.Point(68, 68)
        Me.cboPMFine.Name = "cboPMFine"
        Me.cboPMFine.Size = New System.Drawing.Size(108, 21)
        Me.cboPMFine.TabIndex = 8
        '
        'cboEightHour
        '
        Me.cboEightHour.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEightHour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEightHour.FormattingEnabled = True
        Me.cboEightHour.Location = New System.Drawing.Point(68, 43)
        Me.cboEightHour.Name = "cboEightHour"
        Me.cboEightHour.Size = New System.Drawing.Size(108, 21)
        Me.cboEightHour.TabIndex = 7
        '
        'cboOneHour
        '
        Me.cboOneHour.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOneHour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOneHour.FormattingEnabled = True
        Me.cboOneHour.Location = New System.Drawing.Point(68, 20)
        Me.cboOneHour.Name = "cboOneHour"
        Me.cboOneHour.Size = New System.Drawing.Size(108, 21)
        Me.cboOneHour.TabIndex = 6
        '
        'Label97
        '
        Me.Label97.AutoSize = True
        Me.Label97.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(2, 71)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(44, 13)
        Me.Label97.TabIndex = 2
        Me.Label97.Text = "PM 2.5:"
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(2, 47)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(60, 13)
        Me.Label96.TabIndex = 1
        Me.Label96.Text = "8-hr ozone:"
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.Location = New System.Drawing.Point(2, 23)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(60, 13)
        Me.Label94.TabIndex = 0
        Me.Label94.Text = "1-hr ozone:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(444, 76)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 379
        Me.Label9.Text = "NAICS Code:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtNAICSCode
        '
        Me.txtNAICSCode.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtNAICSCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNAICSCode.Location = New System.Drawing.Point(517, 73)
        Me.txtNAICSCode.Name = "txtNAICSCode"
        Me.txtNAICSCode.Size = New System.Drawing.Size(71, 20)
        Me.txtNAICSCode.TabIndex = 380
        '
        'IAIPEditHeaderData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 545)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtNAICSCode)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.txtPlantDescription)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtKey)
        Me.Controls.Add(Me.dgvHeaderDataHistory)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DTPStartUpDate)
        Me.Controls.Add(Me.DTPShutdown)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboClassification)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboOperationalStatus)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtSICCode)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtModifingComments)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.labReferenceNumber)
        Me.Controls.Add(Me.txtAirsNumber)
        Me.Controls.Add(Me.txtFacilityName)
        Me.Controls.Add(Me.llbCurrentData)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TBEditHeaderData)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPEditHeaderData"
        Me.Text = "Edit Header Data"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgvHeaderDataHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents mmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TBEditHeaderData As System.Windows.Forms.ToolBar
    Friend WithEvents TbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtPlantDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtKey As System.Windows.Forms.TextBox
    Friend WithEvents dgvHeaderDataHistory As System.Windows.Forms.DataGridView
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DTPStartUpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPShutdown As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboClassification As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboOperationalStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSICCode As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chbAPCA As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCF As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCV As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCM As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPCI As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC8 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbAPC0 As System.Windows.Forms.CheckBox
    Friend WithEvents txtModifingComments As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents labReferenceNumber As System.Windows.Forms.Label
    Friend WithEvents txtAirsNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents llbCurrentData As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chbHAPsMajor As System.Windows.Forms.CheckBox
    Friend WithEvents chbNSRMajor As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents cboOneHour As System.Windows.Forms.ComboBox
    Friend WithEvents cboPMFine As System.Windows.Forms.ComboBox
    Friend WithEvents cboEightHour As System.Windows.Forms.ComboBox
    Friend WithEvents chbAPCRMP As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNAICSCode As System.Windows.Forms.TextBox
End Class
