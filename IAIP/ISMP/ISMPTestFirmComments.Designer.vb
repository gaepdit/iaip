<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPTestFirmComments
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbLookUpAirNumber = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCommentID = New System.Windows.Forms.TextBox()
        Me.btnRefreshAIRSNumber = New System.Windows.Forms.Button()
        Me.btnRefreshReportNumber = New System.Windows.Forms.Button()
        Me.btnRefreshNotifications = New System.Windows.Forms.Button()
        Me.cboTestingFirm = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox()
        Me.txtAddComments = New System.Windows.Forms.TextBox()
        Me.btnDeleteComment = New System.Windows.Forms.Button()
        Me.btnEditComment = New System.Windows.Forms.Button()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.cboCommentNumber = New System.Windows.Forms.ComboBox()
        Me.txtTestDateEnd = New System.Windows.Forms.TextBox()
        Me.txtTestDateStart = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTestNotificationNumber = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTestReportNumber = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFacilityTested = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOpenManagerTools = New System.Windows.Forms.Button()
        Me.btnSaveTestReport = New System.Windows.Forms.Button()
        Me.btnSaveDayOf = New System.Windows.Forms.Button()
        Me.btnSavePreTest = New System.Windows.Forms.Button()
        Me.txtAllComments = New System.Windows.Forms.TextBox()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbLookUpAirNumber})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSave.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(23, 22)
        Me.tsbSave.Text = "Save"
        '
        'tsbLookUpAirNumber
        '
        Me.tsbLookUpAirNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbLookUpAirNumber.Image = Global.Iaip.My.Resources.Resources.FindIcon
        Me.tsbLookUpAirNumber.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbLookUpAirNumber.Name = "tsbLookUpAirNumber"
        Me.tsbLookUpAirNumber.Size = New System.Drawing.Size(23, 22)
        Me.tsbLookUpAirNumber.Text = "Search for AIRS #"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCommentID)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRefreshAIRSNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRefreshReportNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRefreshNotifications)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboTestingFirm)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAIRSNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAddComments)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnDeleteComment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnEditComment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblComment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboCommentNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTestDateEnd)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTestDateStart)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTestNotificationNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTestReportNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFacilityTested)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnOpenManagerTools)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSaveTestReport)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSaveDayOf)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSavePreTest)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtAllComments)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 541)
        Me.SplitContainer1.SplitterDistance = 263
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(569, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Comment ID:"
        '
        'txtCommentID
        '
        Me.txtCommentID.Location = New System.Drawing.Point(637, 69)
        Me.txtCommentID.Name = "txtCommentID"
        Me.txtCommentID.ReadOnly = True
        Me.txtCommentID.Size = New System.Drawing.Size(38, 20)
        Me.txtCommentID.TabIndex = 26
        '
        'btnRefreshAIRSNumber
        '
        Me.btnRefreshAIRSNumber.AutoSize = True
        Me.btnRefreshAIRSNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshAIRSNumber.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefreshAIRSNumber.Location = New System.Drawing.Point(488, 6)
        Me.btnRefreshAIRSNumber.Name = "btnRefreshAIRSNumber"
        Me.btnRefreshAIRSNumber.Size = New System.Drawing.Size(22, 22)
        Me.btnRefreshAIRSNumber.TabIndex = 25
        Me.btnRefreshAIRSNumber.UseVisualStyleBackColor = True
        '
        'btnRefreshReportNumber
        '
        Me.btnRefreshReportNumber.AutoSize = True
        Me.btnRefreshReportNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshReportNumber.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefreshReportNumber.Location = New System.Drawing.Point(464, 33)
        Me.btnRefreshReportNumber.Name = "btnRefreshReportNumber"
        Me.btnRefreshReportNumber.Size = New System.Drawing.Size(22, 22)
        Me.btnRefreshReportNumber.TabIndex = 24
        Me.btnRefreshReportNumber.UseVisualStyleBackColor = True
        '
        'btnRefreshNotifications
        '
        Me.btnRefreshNotifications.AutoSize = True
        Me.btnRefreshNotifications.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshNotifications.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefreshNotifications.Location = New System.Drawing.Point(222, 32)
        Me.btnRefreshNotifications.Name = "btnRefreshNotifications"
        Me.btnRefreshNotifications.Size = New System.Drawing.Size(22, 22)
        Me.btnRefreshNotifications.TabIndex = 23
        Me.btnRefreshNotifications.UseVisualStyleBackColor = True
        '
        'cboTestingFirm
        '
        Me.cboTestingFirm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboTestingFirm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTestingFirm.FormattingEnabled = True
        Me.cboTestingFirm.Location = New System.Drawing.Point(74, 7)
        Me.cboTestingFirm.Name = "cboTestingFirm"
        Me.cboTestingFirm.Size = New System.Drawing.Size(297, 21)
        Me.cboTestingFirm.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(372, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "AIRS #:"
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(418, 7)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(66, 20)
        Me.txtAIRSNumber.TabIndex = 20
        '
        'txtAddComments
        '
        Me.txtAddComments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAddComments.Location = New System.Drawing.Point(12, 97)
        Me.txtAddComments.Multiline = True
        Me.txtAddComments.Name = "txtAddComments"
        Me.txtAddComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddComments.Size = New System.Drawing.Size(768, 153)
        Me.txtAddComments.TabIndex = 3
        '
        'btnDeleteComment
        '
        Me.btnDeleteComment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteComment.AutoSize = True
        Me.btnDeleteComment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDeleteComment.Location = New System.Drawing.Point(609, 186)
        Me.btnDeleteComment.Name = "btnDeleteComment"
        Me.btnDeleteComment.Size = New System.Drawing.Size(95, 23)
        Me.btnDeleteComment.TabIndex = 19
        Me.btnDeleteComment.Text = "Delete Comment"
        Me.btnDeleteComment.UseVisualStyleBackColor = True
        '
        'btnEditComment
        '
        Me.btnEditComment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditComment.AutoSize = True
        Me.btnEditComment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEditComment.Location = New System.Drawing.Point(609, 141)
        Me.btnEditComment.Name = "btnEditComment"
        Me.btnEditComment.Size = New System.Drawing.Size(99, 23)
        Me.btnEditComment.TabIndex = 18
        Me.btnEditComment.Text = "Update Comment"
        Me.btnEditComment.UseVisualStyleBackColor = True
        '
        'lblComment
        '
        Me.lblComment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblComment.AutoSize = True
        Me.lblComment.Location = New System.Drawing.Point(606, 105)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(95, 13)
        Me.lblComment.TabIndex = 17
        Me.lblComment.Text = "Select a comment:"
        '
        'cboCommentNumber
        '
        Me.cboCommentNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboCommentNumber.FormattingEnabled = True
        Me.cboCommentNumber.Location = New System.Drawing.Point(704, 102)
        Me.cboCommentNumber.Name = "cboCommentNumber"
        Me.cboCommentNumber.Size = New System.Drawing.Size(76, 21)
        Me.cboCommentNumber.TabIndex = 16
        '
        'txtTestDateEnd
        '
        Me.txtTestDateEnd.Location = New System.Drawing.Point(654, 34)
        Me.txtTestDateEnd.Name = "txtTestDateEnd"
        Me.txtTestDateEnd.ReadOnly = True
        Me.txtTestDateEnd.Size = New System.Drawing.Size(74, 20)
        Me.txtTestDateEnd.TabIndex = 15
        '
        'txtTestDateStart
        '
        Me.txtTestDateStart.Location = New System.Drawing.Point(574, 34)
        Me.txtTestDateStart.Name = "txtTestDateStart"
        Me.txtTestDateStart.ReadOnly = True
        Me.txtTestDateStart.Size = New System.Drawing.Size(74, 20)
        Me.txtTestDateStart.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(500, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Test Dates:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Testing Firm:"
        '
        'txtTestNotificationNumber
        '
        Me.txtTestNotificationNumber.Location = New System.Drawing.Point(136, 34)
        Me.txtTestNotificationNumber.Name = "txtTestNotificationNumber"
        Me.txtTestNotificationNumber.Size = New System.Drawing.Size(80, 20)
        Me.txtTestNotificationNumber.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(514, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Facility Tested:"
        '
        'txtTestReportNumber
        '
        Me.txtTestReportNumber.Location = New System.Drawing.Point(358, 35)
        Me.txtTestReportNumber.Name = "txtTestReportNumber"
        Me.txtTestReportNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtTestReportNumber.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(250, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Test Report Number:"
        '
        'txtFacilityTested
        '
        Me.txtFacilityTested.Location = New System.Drawing.Point(592, 7)
        Me.txtFacilityTested.Name = "txtFacilityTested"
        Me.txtFacilityTested.ReadOnly = True
        Me.txtFacilityTested.Size = New System.Drawing.Size(188, 20)
        Me.txtFacilityTested.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Test Notification Number:"
        '
        'btnOpenManagerTools
        '
        Me.btnOpenManagerTools.AutoSize = True
        Me.btnOpenManagerTools.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenManagerTools.Location = New System.Drawing.Point(680, 68)
        Me.btnOpenManagerTools.Name = "btnOpenManagerTools"
        Me.btnOpenManagerTools.Size = New System.Drawing.Size(94, 23)
        Me.btnOpenManagerTools.TabIndex = 4
        Me.btnOpenManagerTools.Text = "View PM2 Tools"
        Me.btnOpenManagerTools.UseVisualStyleBackColor = True
        '
        'btnSaveTestReport
        '
        Me.btnSaveTestReport.AutoSize = True
        Me.btnSaveTestReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveTestReport.Location = New System.Drawing.Point(395, 68)
        Me.btnSaveTestReport.Name = "btnSaveTestReport"
        Me.btnSaveTestReport.Size = New System.Drawing.Size(162, 23)
        Me.btnSaveTestReport.TabIndex = 2
        Me.btnSaveTestReport.Text = "Commit-Test Report Comments"
        Me.btnSaveTestReport.UseVisualStyleBackColor = True
        '
        'btnSaveDayOf
        '
        Me.btnSaveDayOf.AutoSize = True
        Me.btnSaveDayOf.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveDayOf.Location = New System.Drawing.Point(212, 68)
        Me.btnSaveDayOf.Name = "btnSaveDayOf"
        Me.btnSaveDayOf.Size = New System.Drawing.Size(161, 23)
        Me.btnSaveDayOf.TabIndex = 1
        Me.btnSaveDayOf.Text = "Commit-Day of Test Comments"
        Me.btnSaveDayOf.UseVisualStyleBackColor = True
        '
        'btnSavePreTest
        '
        Me.btnSavePreTest.AutoSize = True
        Me.btnSavePreTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSavePreTest.Location = New System.Drawing.Point(12, 68)
        Me.btnSavePreTest.Name = "btnSavePreTest"
        Me.btnSavePreTest.Size = New System.Drawing.Size(175, 23)
        Me.btnSavePreTest.TabIndex = 0
        Me.btnSavePreTest.Text = "Commit-Preday of Test Comments"
        Me.btnSavePreTest.UseVisualStyleBackColor = True
        '
        'txtAllComments
        '
        Me.txtAllComments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAllComments.Location = New System.Drawing.Point(12, 13)
        Me.txtAllComments.Multiline = True
        Me.txtAllComments.Name = "txtAllComments"
        Me.txtAllComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAllComments.Size = New System.Drawing.Size(768, 241)
        Me.txtAllComments.TabIndex = 4
        '
        'ISMPTestFirmComments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "ISMPTestFirmComments"
        Me.Text = "ISMP Test Firm Comments"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtAddComments As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveTestReport As System.Windows.Forms.Button
    Friend WithEvents btnSaveDayOf As System.Windows.Forms.Button
    Friend WithEvents btnSavePreTest As System.Windows.Forms.Button
    Friend WithEvents txtAllComments As System.Windows.Forms.TextBox
    Friend WithEvents btnOpenManagerTools As System.Windows.Forms.Button
    Friend WithEvents txtFacilityTested As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTestDateEnd As System.Windows.Forms.TextBox
    Friend WithEvents txtTestDateStart As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTestNotificationNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTestReportNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteComment As System.Windows.Forms.Button
    Friend WithEvents btnEditComment As System.Windows.Forms.Button
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents cboCommentNumber As System.Windows.Forms.ComboBox
    Friend WithEvents tsbLookUpAirNumber As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents cboTestingFirm As System.Windows.Forms.ComboBox
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefreshNotifications As System.Windows.Forms.Button
    Friend WithEvents btnRefreshReportNumber As System.Windows.Forms.Button
    Friend WithEvents btnRefreshAIRSNumber As System.Windows.Forms.Button
    Friend WithEvents txtCommentID As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
