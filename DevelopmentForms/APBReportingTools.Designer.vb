<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class APBReportingTools
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(APBReportingTools))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TCBranchTools = New System.Windows.Forms.TabControl
        Me.TPPhoneList = New System.Windows.Forms.TabPage
        Me.TPOrgChart = New System.Windows.Forms.TabPage
        Me.rtbOrgChart = New ExtendedRichTextBox.RichTextBoxPrintCtrl
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnExportOrgChart = New System.Windows.Forms.Button
        Me.btnGenerateOrgChart = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuPageSetup = New System.Windows.Forms.ToolStripMenuItem
        Me.PreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuUndo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRedo = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FindAndReplaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator
        Me.InsertImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectFontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator
        Me.FontColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.BoldToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ItalicToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UnderlineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NormalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator
        Me.PageColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ParagraphToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IndentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuIndent0 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuIndent5 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuIndent10 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuIndent15 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuIndent20 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAlign = New System.Windows.Forms.ToolStripMenuItem
        Me.LeftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CenterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BulletsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddBulletsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveBulletsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tbrNew = New System.Windows.Forms.ToolStripButton
        Me.tbrOpen = New System.Windows.Forms.ToolStripButton
        Me.tbrSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tbrFont = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.tbrLeft = New System.Windows.Forms.ToolStripButton
        Me.tbrCenter = New System.Windows.Forms.ToolStripButton
        Me.tbrRight = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.tbrBold = New System.Windows.Forms.ToolStripButton
        Me.tbrItalic = New System.Windows.Forms.ToolStripButton
        Me.tbrUnderline = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.tbrFind = New System.Windows.Forms.ToolStripButton
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.StatusStrip1.SuspendLayout()
        Me.TCBranchTools.SuspendLayout()
        Me.TPOrgChart.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 664)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1016, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pnl1
        '
        Me.pnl1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(993, 17)
        Me.pnl1.Spring = True
        Me.pnl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl2
        '
        Me.pnl2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl2.Name = "pnl2"
        Me.pnl2.Size = New System.Drawing.Size(4, 17)
        '
        'pnl3
        '
        Me.pnl3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.pnl3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Size = New System.Drawing.Size(4, 17)
        '
        'TCBranchTools
        '
        Me.TCBranchTools.Controls.Add(Me.TPOrgChart)
        Me.TCBranchTools.Controls.Add(Me.TPPhoneList)
        Me.TCBranchTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCBranchTools.Location = New System.Drawing.Point(0, 49)
        Me.TCBranchTools.Name = "TCBranchTools"
        Me.TCBranchTools.SelectedIndex = 0
        Me.TCBranchTools.Size = New System.Drawing.Size(1016, 615)
        Me.TCBranchTools.TabIndex = 6
        '
        'TPPhoneList
        '
        Me.TPPhoneList.Location = New System.Drawing.Point(4, 22)
        Me.TPPhoneList.Name = "TPPhoneList"
        Me.TPPhoneList.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPhoneList.Size = New System.Drawing.Size(1008, 589)
        Me.TPPhoneList.TabIndex = 1
        Me.TPPhoneList.Text = "Phone List"
        Me.TPPhoneList.UseVisualStyleBackColor = True
        '
        'TPOrgChart
        '
        Me.TPOrgChart.Controls.Add(Me.rtbOrgChart)
        Me.TPOrgChart.Controls.Add(Me.Panel1)
        Me.TPOrgChart.Location = New System.Drawing.Point(4, 22)
        Me.TPOrgChart.Name = "TPOrgChart"
        Me.TPOrgChart.Padding = New System.Windows.Forms.Padding(3)
        Me.TPOrgChart.Size = New System.Drawing.Size(1008, 589)
        Me.TPOrgChart.TabIndex = 0
        Me.TPOrgChart.Text = "Organization Chart"
        Me.TPOrgChart.UseVisualStyleBackColor = True
        '
        'rtbOrgChart
        '
        Me.rtbOrgChart.Location = New System.Drawing.Point(29, 115)
        Me.rtbOrgChart.Name = "rtbOrgChart"
        Me.rtbOrgChart.Size = New System.Drawing.Size(953, 452)
        Me.rtbOrgChart.TabIndex = 2
        Me.rtbOrgChart.Text = ""
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExportOrgChart)
        Me.Panel1.Controls.Add(Me.btnGenerateOrgChart)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1002, 86)
        Me.Panel1.TabIndex = 0
        '
        'btnExportOrgChart
        '
        Me.btnExportOrgChart.AutoSize = True
        Me.btnExportOrgChart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExportOrgChart.Location = New System.Drawing.Point(161, 12)
        Me.btnExportOrgChart.Name = "btnExportOrgChart"
        Me.btnExportOrgChart.Size = New System.Drawing.Size(98, 23)
        Me.btnExportOrgChart.TabIndex = 1
        Me.btnExportOrgChart.Text = "Export Org. Chart"
        Me.btnExportOrgChart.UseVisualStyleBackColor = True
        '
        'btnGenerateOrgChart
        '
        Me.btnGenerateOrgChart.AutoSize = True
        Me.btnGenerateOrgChart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnGenerateOrgChart.Location = New System.Drawing.Point(16, 12)
        Me.btnGenerateOrgChart.Name = "btnGenerateOrgChart"
        Me.btnGenerateOrgChart.Size = New System.Drawing.Size(112, 23)
        Me.btnGenerateOrgChart.TabIndex = 0
        Me.btnGenerateOrgChart.Text = "Generate Org. Chart"
        Me.btnGenerateOrgChart.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.FontToolStripMenuItem, Me.ParagraphToolStripMenuItem, Me.BulletsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1016, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.ToolStripMenuItem1, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripMenuItem2, Me.mnuPageSetup, Me.PreviewToolStripMenuItem, Me.PrintToolStripMenuItem, Me.ToolStripMenuItem3, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Image = CType(resources.GetObject("NewToolStripMenuItem.Image"), System.Drawing.Image)
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.NewToolStripMenuItem.Text = "&New"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(149, 6)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Image = CType(resources.GetObject("OpenToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.OpenToolStripMenuItem.Text = "&Open..."
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Image = CType(resources.GetObject("SaveToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SaveToolStripMenuItem.Text = "&Save..."
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save &As..."
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(149, 6)
        '
        'mnuPageSetup
        '
        Me.mnuPageSetup.Name = "mnuPageSetup"
        Me.mnuPageSetup.Size = New System.Drawing.Size(152, 22)
        Me.mnuPageSetup.Text = "Page Setup..."
        '
        'PreviewToolStripMenuItem
        '
        Me.PreviewToolStripMenuItem.Name = "PreviewToolStripMenuItem"
        Me.PreviewToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PreviewToolStripMenuItem.Text = "Pre&view..."
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Image = CType(resources.GetObject("PrintToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PrintToolStripMenuItem.Text = "&Print..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(149, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUndo, Me.mnuRedo, Me.ToolStripSeparator6, Me.FindToolStripMenuItem, Me.FindAndReplaceToolStripMenuItem, Me.ToolStripMenuItem4, Me.SelectAllToolStripMenuItem, Me.ToolStripMenuItem5, Me.CopyToolStripMenuItem, Me.CutToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripMenuItem8, Me.InsertImageToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'mnuUndo
        '
        Me.mnuUndo.Image = CType(resources.GetObject("mnuUndo.Image"), System.Drawing.Image)
        Me.mnuUndo.Name = "mnuUndo"
        Me.mnuUndo.Size = New System.Drawing.Size(179, 22)
        Me.mnuUndo.Text = "&Undo"
        '
        'mnuRedo
        '
        Me.mnuRedo.Image = CType(resources.GetObject("mnuRedo.Image"), System.Drawing.Image)
        Me.mnuRedo.Name = "mnuRedo"
        Me.mnuRedo.Size = New System.Drawing.Size(179, 22)
        Me.mnuRedo.Text = "&Redo"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(176, 6)
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Image = CType(resources.GetObject("FindToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.FindToolStripMenuItem.Text = "Fi&nd..."
        '
        'FindAndReplaceToolStripMenuItem
        '
        Me.FindAndReplaceToolStripMenuItem.Image = CType(resources.GetObject("FindAndReplaceToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FindAndReplaceToolStripMenuItem.Name = "FindAndReplaceToolStripMenuItem"
        Me.FindAndReplaceToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.FindAndReplaceToolStripMenuItem.Text = "Find and &Replace..."
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(176, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select &All"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(176, 6)
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Image = CType(resources.GetObject("CutToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.CutToolStripMenuItem.Text = "C&ut"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Image = CType(resources.GetObject("PasteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.PasteToolStripMenuItem.Text = "Pas&te"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(176, 6)
        '
        'InsertImageToolStripMenuItem
        '
        Me.InsertImageToolStripMenuItem.Name = "InsertImageToolStripMenuItem"
        Me.InsertImageToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.InsertImageToolStripMenuItem.Text = "Insert Image..."
        '
        'FontToolStripMenuItem
        '
        Me.FontToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectFontToolStripMenuItem, Me.ToolStripMenuItem6, Me.FontColorToolStripMenuItem, Me.ToolStripSeparator5, Me.BoldToolStripMenuItem, Me.ItalicToolStripMenuItem, Me.UnderlineToolStripMenuItem, Me.NormalToolStripMenuItem, Me.ToolStripMenuItem7, Me.PageColorToolStripMenuItem})
        Me.FontToolStripMenuItem.Name = "FontToolStripMenuItem"
        Me.FontToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.FontToolStripMenuItem.Text = "F&ont"
        '
        'SelectFontToolStripMenuItem
        '
        Me.SelectFontToolStripMenuItem.Image = CType(resources.GetObject("SelectFontToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SelectFontToolStripMenuItem.Name = "SelectFontToolStripMenuItem"
        Me.SelectFontToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.SelectFontToolStripMenuItem.Text = "Se&lect Font..."
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(148, 6)
        '
        'FontColorToolStripMenuItem
        '
        Me.FontColorToolStripMenuItem.Name = "FontColorToolStripMenuItem"
        Me.FontColorToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.FontColorToolStripMenuItem.Text = "Font &Color..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(148, 6)
        '
        'BoldToolStripMenuItem
        '
        Me.BoldToolStripMenuItem.CheckOnClick = True
        Me.BoldToolStripMenuItem.Image = CType(resources.GetObject("BoldToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BoldToolStripMenuItem.Name = "BoldToolStripMenuItem"
        Me.BoldToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.BoldToolStripMenuItem.Text = "&Bold"
        '
        'ItalicToolStripMenuItem
        '
        Me.ItalicToolStripMenuItem.CheckOnClick = True
        Me.ItalicToolStripMenuItem.Image = CType(resources.GetObject("ItalicToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ItalicToolStripMenuItem.Name = "ItalicToolStripMenuItem"
        Me.ItalicToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ItalicToolStripMenuItem.Text = "&Italic"
        '
        'UnderlineToolStripMenuItem
        '
        Me.UnderlineToolStripMenuItem.CheckOnClick = True
        Me.UnderlineToolStripMenuItem.Image = CType(resources.GetObject("UnderlineToolStripMenuItem.Image"), System.Drawing.Image)
        Me.UnderlineToolStripMenuItem.Name = "UnderlineToolStripMenuItem"
        Me.UnderlineToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.UnderlineToolStripMenuItem.Text = "&Underline"
        '
        'NormalToolStripMenuItem
        '
        Me.NormalToolStripMenuItem.Name = "NormalToolStripMenuItem"
        Me.NormalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.NormalToolStripMenuItem.Text = "&Normal"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(148, 6)
        '
        'PageColorToolStripMenuItem
        '
        Me.PageColorToolStripMenuItem.Name = "PageColorToolStripMenuItem"
        Me.PageColorToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.PageColorToolStripMenuItem.Text = "&Page Color..."
        '
        'ParagraphToolStripMenuItem
        '
        Me.ParagraphToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IndentToolStripMenuItem, Me.mnuAlign})
        Me.ParagraphToolStripMenuItem.Name = "ParagraphToolStripMenuItem"
        Me.ParagraphToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
        Me.ParagraphToolStripMenuItem.Text = "P&aragraph"
        '
        'IndentToolStripMenuItem
        '
        Me.IndentToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuIndent0, Me.mnuIndent5, Me.mnuIndent10, Me.mnuIndent15, Me.mnuIndent20})
        Me.IndentToolStripMenuItem.Name = "IndentToolStripMenuItem"
        Me.IndentToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.IndentToolStripMenuItem.Text = "&Indent"
        '
        'mnuIndent0
        '
        Me.mnuIndent0.Name = "mnuIndent0"
        Me.mnuIndent0.Size = New System.Drawing.Size(115, 22)
        Me.mnuIndent0.Text = "None"
        '
        'mnuIndent5
        '
        Me.mnuIndent5.Name = "mnuIndent5"
        Me.mnuIndent5.Size = New System.Drawing.Size(115, 22)
        Me.mnuIndent5.Text = "5 pts"
        '
        'mnuIndent10
        '
        Me.mnuIndent10.Name = "mnuIndent10"
        Me.mnuIndent10.Size = New System.Drawing.Size(115, 22)
        Me.mnuIndent10.Text = "10 pts"
        '
        'mnuIndent15
        '
        Me.mnuIndent15.Name = "mnuIndent15"
        Me.mnuIndent15.Size = New System.Drawing.Size(115, 22)
        Me.mnuIndent15.Text = "15 pts"
        '
        'mnuIndent20
        '
        Me.mnuIndent20.Name = "mnuIndent20"
        Me.mnuIndent20.Size = New System.Drawing.Size(115, 22)
        Me.mnuIndent20.Text = "20 pts"
        '
        'mnuAlign
        '
        Me.mnuAlign.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LeftToolStripMenuItem, Me.CenterToolStripMenuItem, Me.RightToolStripMenuItem})
        Me.mnuAlign.Name = "mnuAlign"
        Me.mnuAlign.Size = New System.Drawing.Size(152, 22)
        Me.mnuAlign.Text = "&Align"
        '
        'LeftToolStripMenuItem
        '
        Me.LeftToolStripMenuItem.CheckOnClick = True
        Me.LeftToolStripMenuItem.Image = CType(resources.GetObject("LeftToolStripMenuItem.Image"), System.Drawing.Image)
        Me.LeftToolStripMenuItem.Name = "LeftToolStripMenuItem"
        Me.LeftToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.LeftToolStripMenuItem.Text = "Left"
        '
        'CenterToolStripMenuItem
        '
        Me.CenterToolStripMenuItem.CheckOnClick = True
        Me.CenterToolStripMenuItem.Image = CType(resources.GetObject("CenterToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CenterToolStripMenuItem.Name = "CenterToolStripMenuItem"
        Me.CenterToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CenterToolStripMenuItem.Text = "Center"
        '
        'RightToolStripMenuItem
        '
        Me.RightToolStripMenuItem.CheckOnClick = True
        Me.RightToolStripMenuItem.Image = CType(resources.GetObject("RightToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RightToolStripMenuItem.Name = "RightToolStripMenuItem"
        Me.RightToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RightToolStripMenuItem.Text = "Right"
        '
        'BulletsToolStripMenuItem
        '
        Me.BulletsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddBulletsToolStripMenuItem, Me.RemoveBulletsToolStripMenuItem})
        Me.BulletsToolStripMenuItem.Name = "BulletsToolStripMenuItem"
        Me.BulletsToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.BulletsToolStripMenuItem.Text = "&Bullets"
        '
        'AddBulletsToolStripMenuItem
        '
        Me.AddBulletsToolStripMenuItem.Name = "AddBulletsToolStripMenuItem"
        Me.AddBulletsToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.AddBulletsToolStripMenuItem.Text = "A&dd Bullets"
        '
        'RemoveBulletsToolStripMenuItem
        '
        Me.RemoveBulletsToolStripMenuItem.Name = "RemoveBulletsToolStripMenuItem"
        Me.RemoveBulletsToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.RemoveBulletsToolStripMenuItem.Text = "&Remove Bullets"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbrNew, Me.tbrOpen, Me.tbrSave, Me.ToolStripSeparator1, Me.tbrFont, Me.ToolStripSeparator4, Me.tbrLeft, Me.tbrCenter, Me.tbrRight, Me.ToolStripSeparator2, Me.tbrBold, Me.tbrItalic, Me.tbrUnderline, Me.ToolStripSeparator3, Me.tbrFind})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1016, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tbrNew
        '
        Me.tbrNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrNew.Image = CType(resources.GetObject("tbrNew.Image"), System.Drawing.Image)
        Me.tbrNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrNew.Name = "tbrNew"
        Me.tbrNew.Size = New System.Drawing.Size(23, 22)
        Me.tbrNew.Text = "New"
        '
        'tbrOpen
        '
        Me.tbrOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrOpen.Image = CType(resources.GetObject("tbrOpen.Image"), System.Drawing.Image)
        Me.tbrOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrOpen.Name = "tbrOpen"
        Me.tbrOpen.Size = New System.Drawing.Size(23, 22)
        Me.tbrOpen.Text = "Open"
        '
        'tbrSave
        '
        Me.tbrSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrSave.Image = CType(resources.GetObject("tbrSave.Image"), System.Drawing.Image)
        Me.tbrSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrSave.Name = "tbrSave"
        Me.tbrSave.Size = New System.Drawing.Size(23, 22)
        Me.tbrSave.Text = "Save"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tbrFont
        '
        Me.tbrFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrFont.Image = CType(resources.GetObject("tbrFont.Image"), System.Drawing.Image)
        Me.tbrFont.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrFont.Name = "tbrFont"
        Me.tbrFont.Size = New System.Drawing.Size(23, 22)
        Me.tbrFont.Text = "Font"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'tbrLeft
        '
        Me.tbrLeft.CheckOnClick = True
        Me.tbrLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrLeft.Image = CType(resources.GetObject("tbrLeft.Image"), System.Drawing.Image)
        Me.tbrLeft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrLeft.Name = "tbrLeft"
        Me.tbrLeft.Size = New System.Drawing.Size(23, 22)
        Me.tbrLeft.Text = "Left"
        '
        'tbrCenter
        '
        Me.tbrCenter.CheckOnClick = True
        Me.tbrCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrCenter.Image = CType(resources.GetObject("tbrCenter.Image"), System.Drawing.Image)
        Me.tbrCenter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrCenter.Name = "tbrCenter"
        Me.tbrCenter.Size = New System.Drawing.Size(23, 22)
        Me.tbrCenter.Text = "Center"
        '
        'tbrRight
        '
        Me.tbrRight.CheckOnClick = True
        Me.tbrRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrRight.Image = CType(resources.GetObject("tbrRight.Image"), System.Drawing.Image)
        Me.tbrRight.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrRight.Name = "tbrRight"
        Me.tbrRight.Size = New System.Drawing.Size(23, 22)
        Me.tbrRight.Text = "Right"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tbrBold
        '
        Me.tbrBold.CheckOnClick = True
        Me.tbrBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrBold.Image = CType(resources.GetObject("tbrBold.Image"), System.Drawing.Image)
        Me.tbrBold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrBold.Name = "tbrBold"
        Me.tbrBold.Size = New System.Drawing.Size(23, 22)
        Me.tbrBold.Text = "Bold"
        '
        'tbrItalic
        '
        Me.tbrItalic.CheckOnClick = True
        Me.tbrItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrItalic.Image = CType(resources.GetObject("tbrItalic.Image"), System.Drawing.Image)
        Me.tbrItalic.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrItalic.Name = "tbrItalic"
        Me.tbrItalic.Size = New System.Drawing.Size(23, 22)
        Me.tbrItalic.Text = "Italic"
        '
        'tbrUnderline
        '
        Me.tbrUnderline.BackColor = System.Drawing.SystemColors.Control
        Me.tbrUnderline.CheckOnClick = True
        Me.tbrUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrUnderline.Image = CType(resources.GetObject("tbrUnderline.Image"), System.Drawing.Image)
        Me.tbrUnderline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrUnderline.Name = "tbrUnderline"
        Me.tbrUnderline.Size = New System.Drawing.Size(23, 22)
        Me.tbrUnderline.Text = "Underline"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tbrFind
        '
        Me.tbrFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrFind.Image = CType(resources.GetObject("tbrFind.Image"), System.Drawing.Image)
        Me.tbrFind.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrFind.Name = "tbrFind"
        Me.tbrFind.Size = New System.Drawing.Size(23, 22)
        Me.tbrFind.Text = "Find"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PrintDocument1
        '
        '
        'APBReportingTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 686)
        Me.Controls.Add(Me.TCBranchTools)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "APBReportingTools"
        Me.Text = "APB Branch Tools"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TCBranchTools.ResumeLayout(False)
        Me.TPOrgChart.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TCBranchTools As System.Windows.Forms.TabControl
    Friend WithEvents TPOrgChart As System.Windows.Forms.TabPage
    Friend WithEvents TPPhoneList As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnGenerateOrgChart As System.Windows.Forms.Button
    Friend WithEvents btnExportOrgChart As System.Windows.Forms.Button
    Friend WithEvents rtbOrgChart As ExtendedRichTextBox.RichTextBoxPrintCtrl
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPageSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRedo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FindToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindAndReplaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents InsertImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectFontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FontColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BoldToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItalicToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnderlineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NormalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParagraphToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent0 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAlign As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LeftToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CenterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BulletsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddBulletsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveBulletsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tbrNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbrFont As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbrLeft As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrCenter As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrRight As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbrBold As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrItalic As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrUnderline As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbrFind As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
End Class
