<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBEAPPrintForms
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SBEAPPrintForms))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbClear = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenCRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenMRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TCPrintForms = New System.Windows.Forms.TabControl
        Me.TPPrintOuts = New System.Windows.Forms.TabPage
        Me.CRVDemo = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.TP2 = New System.Windows.Forms.TabPage
        Me.TP3 = New System.Windows.Forms.TabPage
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.SBEAPCLIENTSBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtSource = New System.Windows.Forms.TextBox
        Me.txtOrigin = New System.Windows.Forms.TextBox
        Me.DsEPDUserProfiles = New Sbeap.dsEPDUserProfiles
        Me.EPDUSERPROFILESBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EPDUSERPROFILESTableAdapter = New Sbeap.dsEPDUserProfilesTableAdapters.EPDUSERPROFILESTableAdapter
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TCPrintForms.SuspendLayout()
        Me.TPPrintOuts.SuspendLayout()
        Me.TP3.SuspendLayout()
        CType(Me.SBEAPCLIENTSBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsEPDUserProfiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EPDUSERPROFILESBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbClear, Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(688, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbClear
        '
        Me.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClear.Image = CType(resources.GetObject("tsbClear.Image"), System.Drawing.Image)
        Me.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClear.Name = "tsbClear"
        Me.tsbClear.Size = New System.Drawing.Size(23, 22)
        Me.tsbClear.Text = "ToolStripButton1"
        '
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = CType(resources.GetObject("tsbBack.Image"), System.Drawing.Image)
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        Me.tsbBack.Text = "Back"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 457)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(688, 22)
        Me.StatusStrip1.TabIndex = 7
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(115, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(115, 17)
        Me.ToolStripStatusLabel2.Text = "ToolStripStatusLabel2"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(115, 17)
        Me.ToolStripStatusLabel3.Text = "ToolStripStatusLabel3"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(688, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenCRToolStripMenuItem, Me.OpenMRToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenCRToolStripMenuItem
        '
        Me.OpenCRToolStripMenuItem.Name = "OpenCRToolStripMenuItem"
        Me.OpenCRToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.OpenCRToolStripMenuItem.Text = "Open CR"
        '
        'OpenMRToolStripMenuItem
        '
        Me.OpenMRToolStripMenuItem.Name = "OpenMRToolStripMenuItem"
        Me.OpenMRToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.OpenMRToolStripMenuItem.Text = "Open MR"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'TCPrintForms
        '
        Me.TCPrintForms.Controls.Add(Me.TPPrintOuts)
        Me.TCPrintForms.Controls.Add(Me.TP2)
        Me.TCPrintForms.Controls.Add(Me.TP3)
        Me.TCPrintForms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCPrintForms.Location = New System.Drawing.Point(0, 49)
        Me.TCPrintForms.Name = "TCPrintForms"
        Me.TCPrintForms.SelectedIndex = 0
        Me.TCPrintForms.Size = New System.Drawing.Size(688, 408)
        Me.TCPrintForms.TabIndex = 9
        '
        'TPPrintOuts
        '
        Me.TPPrintOuts.Controls.Add(Me.CRVDemo)
        Me.TPPrintOuts.Location = New System.Drawing.Point(4, 22)
        Me.TPPrintOuts.Name = "TPPrintOuts"
        Me.TPPrintOuts.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPrintOuts.Size = New System.Drawing.Size(680, 382)
        Me.TPPrintOuts.TabIndex = 0
        Me.TPPrintOuts.Text = "Print Out"
        Me.TPPrintOuts.UseVisualStyleBackColor = True
        '
        'CRVDemo
        '
        Me.CRVDemo.ActiveViewIndex = -1
        Me.CRVDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRVDemo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRVDemo.Location = New System.Drawing.Point(3, 3)
        Me.CRVDemo.Name = "CRVDemo"
        Me.CRVDemo.SelectionFormula = ""
        Me.CRVDemo.Size = New System.Drawing.Size(674, 376)
        Me.CRVDemo.TabIndex = 0
        Me.CRVDemo.ViewTimeSelectionFormula = ""
        '
        'TP2
        '
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Padding = New System.Windows.Forms.Padding(3)
        Me.TP2.Size = New System.Drawing.Size(680, 382)
        Me.TP2.TabIndex = 1
        Me.TP2.UseVisualStyleBackColor = True
        '
        'TP3
        '
        Me.TP3.Controls.Add(Me.WebBrowser1)
        Me.TP3.Location = New System.Drawing.Point(4, 22)
        Me.TP3.Name = "TP3"
        Me.TP3.Size = New System.Drawing.Size(680, 382)
        Me.TP3.TabIndex = 2
        Me.TP3.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(680, 382)
        Me.WebBrowser1.TabIndex = 0
        Me.WebBrowser1.Url = New System.Uri("http://www.google.com", System.UriKind.Absolute)
        '
        'txtSource
        '
        Me.txtSource.Location = New System.Drawing.Point(383, 430)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(39, 20)
        Me.txtSource.TabIndex = 10
        Me.txtSource.Visible = False
        '
        'txtOrigin
        '
        Me.txtOrigin.Location = New System.Drawing.Point(428, 430)
        Me.txtOrigin.Name = "txtOrigin"
        Me.txtOrigin.Size = New System.Drawing.Size(39, 20)
        Me.txtOrigin.TabIndex = 11
        Me.txtOrigin.Visible = False
        '
        'DsEPDUserProfiles
        '
        Me.DsEPDUserProfiles.DataSetName = "dsEPDUserProfiles"
        Me.DsEPDUserProfiles.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'EPDUSERPROFILESBindingSource
        '
        Me.EPDUSERPROFILESBindingSource.DataMember = "EPDUSERPROFILES"
        Me.EPDUSERPROFILESBindingSource.DataSource = Me.DsEPDUserProfiles
        '
        'EPDUSERPROFILESTableAdapter
        '
        Me.EPDUSERPROFILESTableAdapter.ClearBeforeFill = True
        '
        'SBEAPPrintForms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 479)
        Me.Controls.Add(Me.TCPrintForms)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.txtOrigin)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SBEAPPrintForms"
        Me.Text = "Print Forms"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TCPrintForms.ResumeLayout(False)
        Me.TPPrintOuts.ResumeLayout(False)
        Me.TP3.ResumeLayout(False)
        CType(Me.SBEAPCLIENTSBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsEPDUserProfiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EPDUSERPROFILESBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TCPrintForms As System.Windows.Forms.TabControl
    Friend WithEvents TPPrintOuts As System.Windows.Forms.TabPage
    Friend WithEvents CRVDemo As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents TP2 As System.Windows.Forms.TabPage
    Friend WithEvents TP3 As System.Windows.Forms.TabPage
    Friend WithEvents OpenCRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenMRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SBEAPCLIENTSBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents txtOrigin As System.Windows.Forms.TextBox
    Friend WithEvents DsEPDUserProfiles As Sbeap.dsEPDUserProfiles
    Friend WithEvents EPDUSERPROFILESBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents EPDUSERPROFILESTableAdapter As Sbeap.dsEPDUserProfilesTableAdapters.EPDUSERPROFILESTableAdapter
End Class
