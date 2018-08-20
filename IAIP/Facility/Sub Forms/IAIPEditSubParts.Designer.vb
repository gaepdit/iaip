<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPEditSubParts
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
        Me.btnSaveNSPSSubpart = New System.Windows.Forms.Button()
        Me.btnRemoveNSPSSubpart = New System.Windows.Forms.Button()
        Me.cboNSPSSubpart = New System.Windows.Forms.ComboBox()
        Me.clbNSPS = New System.Windows.Forms.CheckedListBox()
        Me.btnSaveNESHAPSubpart = New System.Windows.Forms.Button()
        Me.btnRemoveNESHAPSubpart = New System.Windows.Forms.Button()
        Me.cboNESHAPSubpart = New System.Windows.Forms.ComboBox()
        Me.clbNESHAP = New System.Windows.Forms.CheckedListBox()
        Me.btnAddMACTSubpart = New System.Windows.Forms.Button()
        Me.btnRemoveMACTSubPart = New System.Windows.Forms.Button()
        Me.cboMACTSubPart = New System.Windows.Forms.ComboBox()
        Me.clbMACT = New System.Windows.Forms.CheckedListBox()
        Me.TCSubparts = New System.Windows.Forms.TabControl()
        Me.TPSIP = New System.Windows.Forms.TabPage()
        Me.btnSaveSIPSubpart = New System.Windows.Forms.Button()
        Me.btnRemoveSIPSubpart = New System.Windows.Forms.Button()
        Me.cboSIPSubpart = New System.Windows.Forms.ComboBox()
        Me.clbSIP = New System.Windows.Forms.CheckedListBox()
        Me.TPPart60 = New System.Windows.Forms.TabPage()
        Me.TPPart61 = New System.Windows.Forms.TabPage()
        Me.TPPart63 = New System.Windows.Forms.TabPage()
        Me.AirsNumberDisplay = New System.Windows.Forms.Label()
        Me.txtFacilityName = New System.Windows.Forms.Label()
        Me.TCSubparts.SuspendLayout()
        Me.TPSIP.SuspendLayout()
        Me.TPPart60.SuspendLayout()
        Me.TPPart61.SuspendLayout()
        Me.TPPart63.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSaveNSPSSubpart
        '
        Me.btnSaveNSPSSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveNSPSSubpart.AutoSize = True
        Me.btnSaveNSPSSubpart.Location = New System.Drawing.Point(550, 298)
        Me.btnSaveNSPSSubpart.Name = "btnSaveNSPSSubpart"
        Me.btnSaveNSPSSubpart.Size = New System.Drawing.Size(141, 23)
        Me.btnSaveNSPSSubpart.TabIndex = 3
        Me.btnSaveNSPSSubpart.Text = "Add Subpart to Above List"
        Me.btnSaveNSPSSubpart.UseVisualStyleBackColor = True
        '
        'btnRemoveNSPSSubpart
        '
        Me.btnRemoveNSPSSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveNSPSSubpart.AutoSize = True
        Me.btnRemoveNSPSSubpart.Location = New System.Drawing.Point(8, 262)
        Me.btnRemoveNSPSSubpart.Name = "btnRemoveNSPSSubpart"
        Me.btnRemoveNSPSSubpart.Size = New System.Drawing.Size(193, 23)
        Me.btnRemoveNSPSSubpart.TabIndex = 1
        Me.btnRemoveNSPSSubpart.Text = "Remove selected subpart from facility"
        Me.btnRemoveNSPSSubpart.UseVisualStyleBackColor = True
        '
        'cboNSPSSubpart
        '
        Me.cboNSPSSubpart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboNSPSSubpart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNSPSSubpart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNSPSSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNSPSSubpart.FormattingEnabled = True
        Me.cboNSPSSubpart.Location = New System.Drawing.Point(8, 299)
        Me.cboNSPSSubpart.Name = "cboNSPSSubpart"
        Me.cboNSPSSubpart.Size = New System.Drawing.Size(536, 21)
        Me.cboNSPSSubpart.TabIndex = 2
        '
        'clbNSPS
        '
        Me.clbNSPS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbNSPS.CheckOnClick = True
        Me.clbNSPS.FormattingEnabled = True
        Me.clbNSPS.HorizontalScrollbar = True
        Me.clbNSPS.Location = New System.Drawing.Point(8, 12)
        Me.clbNSPS.Name = "clbNSPS"
        Me.clbNSPS.Size = New System.Drawing.Size(683, 244)
        Me.clbNSPS.TabIndex = 0
        '
        'btnSaveNESHAPSubpart
        '
        Me.btnSaveNESHAPSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveNESHAPSubpart.AutoSize = True
        Me.btnSaveNESHAPSubpart.Location = New System.Drawing.Point(550, 298)
        Me.btnSaveNESHAPSubpart.Name = "btnSaveNESHAPSubpart"
        Me.btnSaveNESHAPSubpart.Size = New System.Drawing.Size(141, 23)
        Me.btnSaveNESHAPSubpart.TabIndex = 3
        Me.btnSaveNESHAPSubpart.Text = "Add subpart to facility"
        Me.btnSaveNESHAPSubpart.UseVisualStyleBackColor = True
        '
        'btnRemoveNESHAPSubpart
        '
        Me.btnRemoveNESHAPSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveNESHAPSubpart.AutoSize = True
        Me.btnRemoveNESHAPSubpart.Location = New System.Drawing.Point(8, 262)
        Me.btnRemoveNESHAPSubpart.Name = "btnRemoveNESHAPSubpart"
        Me.btnRemoveNESHAPSubpart.Size = New System.Drawing.Size(193, 23)
        Me.btnRemoveNESHAPSubpart.TabIndex = 1
        Me.btnRemoveNESHAPSubpart.Text = "Remove selected subpart from facility"
        Me.btnRemoveNESHAPSubpart.UseVisualStyleBackColor = True
        '
        'cboNESHAPSubpart
        '
        Me.cboNESHAPSubpart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboNESHAPSubpart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNESHAPSubpart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNESHAPSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNESHAPSubpart.FormattingEnabled = True
        Me.cboNESHAPSubpart.Location = New System.Drawing.Point(8, 299)
        Me.cboNESHAPSubpart.Name = "cboNESHAPSubpart"
        Me.cboNESHAPSubpart.Size = New System.Drawing.Size(536, 21)
        Me.cboNESHAPSubpart.TabIndex = 2
        '
        'clbNESHAP
        '
        Me.clbNESHAP.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbNESHAP.CheckOnClick = True
        Me.clbNESHAP.FormattingEnabled = True
        Me.clbNESHAP.HorizontalScrollbar = True
        Me.clbNESHAP.Location = New System.Drawing.Point(8, 12)
        Me.clbNESHAP.Name = "clbNESHAP"
        Me.clbNESHAP.Size = New System.Drawing.Size(683, 244)
        Me.clbNESHAP.TabIndex = 0
        '
        'btnAddMACTSubpart
        '
        Me.btnAddMACTSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddMACTSubpart.AutoSize = True
        Me.btnAddMACTSubpart.Location = New System.Drawing.Point(550, 298)
        Me.btnAddMACTSubpart.Name = "btnAddMACTSubpart"
        Me.btnAddMACTSubpart.Size = New System.Drawing.Size(141, 23)
        Me.btnAddMACTSubpart.TabIndex = 3
        Me.btnAddMACTSubpart.Text = "Add subpart to facility"
        Me.btnAddMACTSubpart.UseVisualStyleBackColor = True
        '
        'btnRemoveMACTSubPart
        '
        Me.btnRemoveMACTSubPart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveMACTSubPart.AutoSize = True
        Me.btnRemoveMACTSubPart.Location = New System.Drawing.Point(8, 262)
        Me.btnRemoveMACTSubPart.Name = "btnRemoveMACTSubPart"
        Me.btnRemoveMACTSubPart.Size = New System.Drawing.Size(203, 23)
        Me.btnRemoveMACTSubPart.TabIndex = 1
        Me.btnRemoveMACTSubPart.Text = "Remove selected subpart from facility"
        Me.btnRemoveMACTSubPart.UseVisualStyleBackColor = True
        '
        'cboMACTSubPart
        '
        Me.cboMACTSubPart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboMACTSubPart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMACTSubPart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMACTSubPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMACTSubPart.FormattingEnabled = True
        Me.cboMACTSubPart.Location = New System.Drawing.Point(8, 299)
        Me.cboMACTSubPart.Name = "cboMACTSubPart"
        Me.cboMACTSubPart.Size = New System.Drawing.Size(536, 21)
        Me.cboMACTSubPart.TabIndex = 2
        '
        'clbMACT
        '
        Me.clbMACT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbMACT.CheckOnClick = True
        Me.clbMACT.FormattingEnabled = True
        Me.clbMACT.HorizontalScrollbar = True
        Me.clbMACT.Location = New System.Drawing.Point(8, 12)
        Me.clbMACT.Name = "clbMACT"
        Me.clbMACT.Size = New System.Drawing.Size(683, 244)
        Me.clbMACT.TabIndex = 0
        '
        'TCSubparts
        '
        Me.TCSubparts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TCSubparts.Controls.Add(Me.TPSIP)
        Me.TCSubparts.Controls.Add(Me.TPPart60)
        Me.TCSubparts.Controls.Add(Me.TPPart61)
        Me.TCSubparts.Controls.Add(Me.TPPart63)
        Me.TCSubparts.Enabled = False
        Me.TCSubparts.Location = New System.Drawing.Point(0, 46)
        Me.TCSubparts.Name = "TCSubparts"
        Me.TCSubparts.SelectedIndex = 0
        Me.TCSubparts.Size = New System.Drawing.Size(707, 356)
        Me.TCSubparts.TabIndex = 2
        '
        'TPSIP
        '
        Me.TPSIP.Controls.Add(Me.btnSaveSIPSubpart)
        Me.TPSIP.Controls.Add(Me.btnRemoveSIPSubpart)
        Me.TPSIP.Controls.Add(Me.cboSIPSubpart)
        Me.TPSIP.Controls.Add(Me.clbSIP)
        Me.TPSIP.Location = New System.Drawing.Point(4, 22)
        Me.TPSIP.Name = "TPSIP"
        Me.TPSIP.Size = New System.Drawing.Size(699, 330)
        Me.TPSIP.TabIndex = 4
        Me.TPSIP.Text = "SIP"
        Me.TPSIP.UseVisualStyleBackColor = True
        '
        'btnSaveSIPSubpart
        '
        Me.btnSaveSIPSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSIPSubpart.AutoSize = True
        Me.btnSaveSIPSubpart.Location = New System.Drawing.Point(550, 298)
        Me.btnSaveSIPSubpart.Name = "btnSaveSIPSubpart"
        Me.btnSaveSIPSubpart.Size = New System.Drawing.Size(141, 23)
        Me.btnSaveSIPSubpart.TabIndex = 3
        Me.btnSaveSIPSubpart.Text = "Add Subpart to Above List"
        Me.btnSaveSIPSubpart.UseVisualStyleBackColor = True
        '
        'btnRemoveSIPSubpart
        '
        Me.btnRemoveSIPSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveSIPSubpart.AutoSize = True
        Me.btnRemoveSIPSubpart.Location = New System.Drawing.Point(8, 262)
        Me.btnRemoveSIPSubpart.Name = "btnRemoveSIPSubpart"
        Me.btnRemoveSIPSubpart.Size = New System.Drawing.Size(193, 23)
        Me.btnRemoveSIPSubpart.TabIndex = 1
        Me.btnRemoveSIPSubpart.Text = "Remove selected subpart from facility"
        Me.btnRemoveSIPSubpart.UseVisualStyleBackColor = True
        '
        'cboSIPSubpart
        '
        Me.cboSIPSubpart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSIPSubpart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSIPSubpart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSIPSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSIPSubpart.FormattingEnabled = True
        Me.cboSIPSubpart.Location = New System.Drawing.Point(8, 299)
        Me.cboSIPSubpart.Name = "cboSIPSubpart"
        Me.cboSIPSubpart.Size = New System.Drawing.Size(536, 21)
        Me.cboSIPSubpart.TabIndex = 2
        '
        'clbSIP
        '
        Me.clbSIP.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbSIP.CheckOnClick = True
        Me.clbSIP.FormattingEnabled = True
        Me.clbSIP.HorizontalScrollbar = True
        Me.clbSIP.Location = New System.Drawing.Point(8, 12)
        Me.clbSIP.Name = "clbSIP"
        Me.clbSIP.Size = New System.Drawing.Size(683, 244)
        Me.clbSIP.TabIndex = 0
        '
        'TPPart60
        '
        Me.TPPart60.Controls.Add(Me.btnSaveNSPSSubpart)
        Me.TPPart60.Controls.Add(Me.btnRemoveNSPSSubpart)
        Me.TPPart60.Controls.Add(Me.cboNSPSSubpart)
        Me.TPPart60.Controls.Add(Me.clbNSPS)
        Me.TPPart60.Location = New System.Drawing.Point(4, 22)
        Me.TPPart60.Name = "TPPart60"
        Me.TPPart60.Size = New System.Drawing.Size(699, 330)
        Me.TPPart60.TabIndex = 0
        Me.TPPart60.Text = "NSPS (Part 60)"
        Me.TPPart60.UseVisualStyleBackColor = True
        '
        'TPPart61
        '
        Me.TPPart61.Controls.Add(Me.btnSaveNESHAPSubpart)
        Me.TPPart61.Controls.Add(Me.btnRemoveNESHAPSubpart)
        Me.TPPart61.Controls.Add(Me.cboNESHAPSubpart)
        Me.TPPart61.Controls.Add(Me.clbNESHAP)
        Me.TPPart61.Location = New System.Drawing.Point(4, 22)
        Me.TPPart61.Name = "TPPart61"
        Me.TPPart61.Size = New System.Drawing.Size(699, 330)
        Me.TPPart61.TabIndex = 1
        Me.TPPart61.Text = "NESHAP (Part 61) "
        Me.TPPart61.UseVisualStyleBackColor = True
        '
        'TPPart63
        '
        Me.TPPart63.Controls.Add(Me.btnAddMACTSubpart)
        Me.TPPart63.Controls.Add(Me.btnRemoveMACTSubPart)
        Me.TPPart63.Controls.Add(Me.cboMACTSubPart)
        Me.TPPart63.Controls.Add(Me.clbMACT)
        Me.TPPart63.Location = New System.Drawing.Point(4, 22)
        Me.TPPart63.Name = "TPPart63"
        Me.TPPart63.Size = New System.Drawing.Size(699, 330)
        Me.TPPart63.TabIndex = 2
        Me.TPPart63.Text = "MACT (Part 63)"
        Me.TPPart63.UseVisualStyleBackColor = True
        '
        'AirsNumberDisplay
        '
        Me.AirsNumberDisplay.AutoSize = True
        Me.AirsNumberDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AirsNumberDisplay.Location = New System.Drawing.Point(12, 20)
        Me.AirsNumberDisplay.Name = "AirsNumberDisplay"
        Me.AirsNumberDisplay.Size = New System.Drawing.Size(39, 16)
        Me.AirsNumberDisplay.TabIndex = 0
        Me.AirsNumberDisplay.Text = "AIRS"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.AutoSize = True
        Me.txtFacilityName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityName.Location = New System.Drawing.Point(111, 20)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.Size = New System.Drawing.Size(90, 16)
        Me.txtFacilityName.TabIndex = 1
        Me.txtFacilityName.Text = "Facility Name"
        '
        'IAIPEditSubParts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(707, 401)
        Me.Controls.Add(Me.txtFacilityName)
        Me.Controls.Add(Me.AirsNumberDisplay)
        Me.Controls.Add(Me.TCSubparts)
        Me.MinimumSize = New System.Drawing.Size(568, 349)
        Me.Name = "IAIPEditSubParts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Edit Subparts"
        Me.TCSubparts.ResumeLayout(False)
        Me.TPSIP.ResumeLayout(False)
        Me.TPSIP.PerformLayout()
        Me.TPPart60.ResumeLayout(False)
        Me.TPPart60.PerformLayout()
        Me.TPPart61.ResumeLayout(False)
        Me.TPPart61.PerformLayout()
        Me.TPPart63.ResumeLayout(False)
        Me.TPPart63.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRemoveNSPSSubpart As System.Windows.Forms.Button
    Friend WithEvents cboNSPSSubpart As System.Windows.Forms.ComboBox
    Friend WithEvents clbNSPS As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnSaveNSPSSubpart As System.Windows.Forms.Button
    Friend WithEvents btnSaveNESHAPSubpart As System.Windows.Forms.Button
    Friend WithEvents btnRemoveNESHAPSubpart As System.Windows.Forms.Button
    Friend WithEvents cboNESHAPSubpart As System.Windows.Forms.ComboBox
    Friend WithEvents clbNESHAP As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnAddMACTSubpart As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMACTSubPart As System.Windows.Forms.Button
    Friend WithEvents cboMACTSubPart As System.Windows.Forms.ComboBox
    Friend WithEvents clbMACT As System.Windows.Forms.CheckedListBox
    Friend WithEvents TCSubparts As System.Windows.Forms.TabControl
    Friend WithEvents TPPart60 As System.Windows.Forms.TabPage
    Friend WithEvents TPPart61 As System.Windows.Forms.TabPage
    Friend WithEvents TPPart63 As System.Windows.Forms.TabPage
    Friend WithEvents TPSIP As System.Windows.Forms.TabPage
    Friend WithEvents btnSaveSIPSubpart As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSIPSubpart As System.Windows.Forms.Button
    Friend WithEvents cboSIPSubpart As System.Windows.Forms.ComboBox
    Friend WithEvents clbSIP As System.Windows.Forms.CheckedListBox
    Friend WithEvents AirsNumberDisplay As Label
    Friend WithEvents txtFacilityName As Label
End Class
