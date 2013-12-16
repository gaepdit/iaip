<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IAIPFeeAuditTool
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
Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
Me.pnl1 = New System.Windows.Forms.ToolStripStatusLabel
Me.pnl2 = New System.Windows.Forms.ToolStripStatusLabel
Me.pnl3 = New System.Windows.Forms.ToolStripStatusLabel
Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
Me.TCNonResponders = New System.Windows.Forms.TabControl
Me.TPTrackingData = New System.Windows.Forms.TabPage
Me.Panel5 = New System.Windows.Forms.Panel
Me.TCFeeAuditTracking = New System.Windows.Forms.TabControl
Me.TP_Tracking_CY2008 = New System.Windows.Forms.TabPage
Me.Panel6 = New System.Windows.Forms.Panel
Me.Label34 = New System.Windows.Forms.Label
Me.Label33 = New System.Windows.Forms.Label
Me.Panel51 = New System.Windows.Forms.Panel
Me.btnManagerSignOff_CY2008 = New System.Windows.Forms.Button
Me.lblSignOffDat_08 = New System.Windows.Forms.Label
Me.lblManagerSignOff_08 = New System.Windows.Forms.Label
Me.Panel50 = New System.Windows.Forms.Panel
Me.lblLastModified_08 = New System.Windows.Forms.Label
Me.lblStaffAssigned_08 = New System.Windows.Forms.Label
Me.txtComments_CY2008 = New System.Windows.Forms.TextBox
Me.DTPCloseOut_CY2008 = New System.Windows.Forms.DateTimePicker
Me.lblAmountPaid_CY2008 = New System.Windows.Forms.Label
Me.DTPFeesPaid_CY2008 = New System.Windows.Forms.DateTimePicker
Me.DTPAOSent_CY2008 = New System.Windows.Forms.DateTimePicker
Me.DTPCOSent_CY2008 = New System.Windows.Forms.DateTimePicker
Me.DTPNOVSent_CY2008 = New System.Windows.Forms.DateTimePicker
Me.Panel47 = New System.Windows.Forms.Panel
Me.rdbUnabletoContactNo_CY2008 = New System.Windows.Forms.RadioButton
Me.rdbUnabletoContactYes_CY2008 = New System.Windows.Forms.RadioButton
Me.Panel46 = New System.Windows.Forms.Panel
Me.rdbBankruptcyNo_CY2008 = New System.Windows.Forms.RadioButton
Me.rdbBankruptcyYes_CY2008 = New System.Windows.Forms.RadioButton
Me.Panel28 = New System.Windows.Forms.Panel
Me.rdbDataCorrectNo_CY2008 = New System.Windows.Forms.RadioButton
Me.rdbDataCorrectYes_CY2008 = New System.Windows.Forms.RadioButton
Me.DTPLetterRemailed_CY2008 = New System.Windows.Forms.DateTimePicker
Me.Panel26 = New System.Windows.Forms.Panel
Me.rdbAddressUnknownNo_CY2008 = New System.Windows.Forms.RadioButton
Me.rdbAddressUnknownYes_CY2008 = New System.Windows.Forms.RadioButton
Me.DTPLetterReturned_CY2008 = New System.Windows.Forms.DateTimePicker
Me.DTPInitialLetter_2008 = New System.Windows.Forms.DateTimePicker
Me.Label24 = New System.Windows.Forms.Label
Me.Label23 = New System.Windows.Forms.Label
Me.Label22 = New System.Windows.Forms.Label
Me.Label21 = New System.Windows.Forms.Label
Me.Label20 = New System.Windows.Forms.Label
Me.Label19 = New System.Windows.Forms.Label
Me.Label18 = New System.Windows.Forms.Label
Me.Label17 = New System.Windows.Forms.Label
Me.Label16 = New System.Windows.Forms.Label
Me.Label15 = New System.Windows.Forms.Label
Me.Label14 = New System.Windows.Forms.Label
Me.Label13 = New System.Windows.Forms.Label
Me.Label12 = New System.Windows.Forms.Label
Me.Panel48 = New System.Windows.Forms.Panel
Me.lblAuditType_CY2008 = New System.Windows.Forms.Label
Me.btnSaveFeeAudit_CY2008 = New System.Windows.Forms.Button
Me.TP_Tracking_CY2007 = New System.Windows.Forms.TabPage
Me.Panel58 = New System.Windows.Forms.Panel
Me.Label37 = New System.Windows.Forms.Label
Me.Label38 = New System.Windows.Forms.Label
Me.Panel59 = New System.Windows.Forms.Panel
Me.btnManagerSignOff_CY2007 = New System.Windows.Forms.Button
Me.lblSignOffDat_07 = New System.Windows.Forms.Label
Me.lblManagerSignOff_07 = New System.Windows.Forms.Label
Me.Panel60 = New System.Windows.Forms.Panel
Me.lblLastModified_07 = New System.Windows.Forms.Label
Me.lblStaffAssigned_07 = New System.Windows.Forms.Label
Me.txtComments_CY2007 = New System.Windows.Forms.TextBox
Me.DTPCloseOut_CY2007 = New System.Windows.Forms.DateTimePicker
Me.lblAmountPaid_CY2007 = New System.Windows.Forms.Label
Me.DTPFeesPaid_CY2007 = New System.Windows.Forms.DateTimePicker
Me.DTPAOSent_CY2007 = New System.Windows.Forms.DateTimePicker
Me.DTPCOSent_CY2007 = New System.Windows.Forms.DateTimePicker
Me.DTPNOVSent_CY2007 = New System.Windows.Forms.DateTimePicker
Me.Panel61 = New System.Windows.Forms.Panel
Me.rdbUnabletoContactNo_CY2007 = New System.Windows.Forms.RadioButton
Me.rdbUnabletoContactYes_CY2007 = New System.Windows.Forms.RadioButton
Me.Panel62 = New System.Windows.Forms.Panel
Me.rdbBankruptcyNo_CY2007 = New System.Windows.Forms.RadioButton
Me.rdbBankruptcyYes_CY2007 = New System.Windows.Forms.RadioButton
Me.Panel63 = New System.Windows.Forms.Panel
Me.rdbDataCorrectNo_CY2007 = New System.Windows.Forms.RadioButton
Me.rdbDataCorrectYes_CY2007 = New System.Windows.Forms.RadioButton
Me.DTPLetterRemailed_CY2007 = New System.Windows.Forms.DateTimePicker
Me.Panel64 = New System.Windows.Forms.Panel
Me.rdbAddressUnknownNo_CY2007 = New System.Windows.Forms.RadioButton
Me.rdbAddressUnknownYes_CY2007 = New System.Windows.Forms.RadioButton
Me.DTPLetterReturned_CY2007 = New System.Windows.Forms.DateTimePicker
Me.DTPInitialLetter_2007 = New System.Windows.Forms.DateTimePicker
Me.Label67 = New System.Windows.Forms.Label
Me.Label68 = New System.Windows.Forms.Label
Me.Label69 = New System.Windows.Forms.Label
Me.Label70 = New System.Windows.Forms.Label
Me.Label71 = New System.Windows.Forms.Label
Me.Label72 = New System.Windows.Forms.Label
Me.Label73 = New System.Windows.Forms.Label
Me.Label74 = New System.Windows.Forms.Label
Me.Label75 = New System.Windows.Forms.Label
Me.Label76 = New System.Windows.Forms.Label
Me.Label77 = New System.Windows.Forms.Label
Me.Label78 = New System.Windows.Forms.Label
Me.Label79 = New System.Windows.Forms.Label
Me.Panel52 = New System.Windows.Forms.Panel
Me.lblAuditType_CY2007 = New System.Windows.Forms.Label
Me.btnSaveFeeAudit_CY2007 = New System.Windows.Forms.Button
Me.TP_Tracking_CY2006 = New System.Windows.Forms.TabPage
Me.Panel65 = New System.Windows.Forms.Panel
Me.Label39 = New System.Windows.Forms.Label
Me.Label40 = New System.Windows.Forms.Label
Me.Panel66 = New System.Windows.Forms.Panel
Me.btnManagerSignOff_CY2006 = New System.Windows.Forms.Button
Me.lblSignOffDat_06 = New System.Windows.Forms.Label
Me.lblManagerSignOff_06 = New System.Windows.Forms.Label
Me.Panel67 = New System.Windows.Forms.Panel
Me.lblLastModified_06 = New System.Windows.Forms.Label
Me.lblStaffAssigned_06 = New System.Windows.Forms.Label
Me.txtComments_CY2006 = New System.Windows.Forms.TextBox
Me.DTPCloseOut_CY2006 = New System.Windows.Forms.DateTimePicker
Me.lblAmountPaid_CY2006 = New System.Windows.Forms.Label
Me.DTPFeesPaid_CY2006 = New System.Windows.Forms.DateTimePicker
Me.DTPAOSent_CY2006 = New System.Windows.Forms.DateTimePicker
Me.DTPCOSent_CY2006 = New System.Windows.Forms.DateTimePicker
Me.DTPNOVSent_CY2006 = New System.Windows.Forms.DateTimePicker
Me.Panel68 = New System.Windows.Forms.Panel
Me.rdbUnabletoContactNo_CY2006 = New System.Windows.Forms.RadioButton
Me.rdbUnabletoContactYes_CY2006 = New System.Windows.Forms.RadioButton
Me.Panel69 = New System.Windows.Forms.Panel
Me.rdbBankruptcyNo_CY2006 = New System.Windows.Forms.RadioButton
Me.rdbBankruptcyYes_CY2006 = New System.Windows.Forms.RadioButton
Me.Panel70 = New System.Windows.Forms.Panel
Me.rdbDataCorrectNo_CY2006 = New System.Windows.Forms.RadioButton
Me.rdbDataCorrectYes_CY2006 = New System.Windows.Forms.RadioButton
Me.DTPLetterRemailed_CY2006 = New System.Windows.Forms.DateTimePicker
Me.Panel71 = New System.Windows.Forms.Panel
Me.rdbAddressUnknownNo_CY2006 = New System.Windows.Forms.RadioButton
Me.rdbAddressUnknownYes_CY2006 = New System.Windows.Forms.RadioButton
Me.DTPLetterReturned_CY2006 = New System.Windows.Forms.DateTimePicker
Me.DTPInitialLetter_2006 = New System.Windows.Forms.DateTimePicker
Me.Label85 = New System.Windows.Forms.Label
Me.Label86 = New System.Windows.Forms.Label
Me.Label87 = New System.Windows.Forms.Label
Me.Label88 = New System.Windows.Forms.Label
Me.Label89 = New System.Windows.Forms.Label
Me.Label90 = New System.Windows.Forms.Label
Me.Label91 = New System.Windows.Forms.Label
Me.Label92 = New System.Windows.Forms.Label
Me.Label93 = New System.Windows.Forms.Label
Me.Label94 = New System.Windows.Forms.Label
Me.Label95 = New System.Windows.Forms.Label
Me.Label96 = New System.Windows.Forms.Label
Me.Label97 = New System.Windows.Forms.Label
Me.Panel53 = New System.Windows.Forms.Panel
Me.lblAuditType_CY2006 = New System.Windows.Forms.Label
Me.btnSaveFeeAudit_CY2006 = New System.Windows.Forms.Button
Me.TP_Tracking_CY2005 = New System.Windows.Forms.TabPage
Me.Panel72 = New System.Windows.Forms.Panel
Me.Label41 = New System.Windows.Forms.Label
Me.Label42 = New System.Windows.Forms.Label
Me.Panel73 = New System.Windows.Forms.Panel
Me.btnManagerSignOff_CY2005 = New System.Windows.Forms.Button
Me.lblSignOffDat_05 = New System.Windows.Forms.Label
Me.lblManagerSignOff_05 = New System.Windows.Forms.Label
Me.Panel74 = New System.Windows.Forms.Panel
Me.lblLastModified_05 = New System.Windows.Forms.Label
Me.lblStaffAssigned_05 = New System.Windows.Forms.Label
Me.txtComments_CY2005 = New System.Windows.Forms.TextBox
Me.DTPCloseOut_CY2005 = New System.Windows.Forms.DateTimePicker
Me.lblAmountPaid_CY2005 = New System.Windows.Forms.Label
Me.DTPFeesPaid_CY2005 = New System.Windows.Forms.DateTimePicker
Me.DTPAOSent_CY2005 = New System.Windows.Forms.DateTimePicker
Me.DTPCOSent_CY2005 = New System.Windows.Forms.DateTimePicker
Me.DTPNOVSent_CY2005 = New System.Windows.Forms.DateTimePicker
Me.Panel75 = New System.Windows.Forms.Panel
Me.rdbUnabletoContactNo_CY2005 = New System.Windows.Forms.RadioButton
Me.rdbUnabletoContactYes_CY2005 = New System.Windows.Forms.RadioButton
Me.Panel76 = New System.Windows.Forms.Panel
Me.rdbBankruptcyNo_CY2005 = New System.Windows.Forms.RadioButton
Me.rdbBankruptcyYes_CY2005 = New System.Windows.Forms.RadioButton
Me.Panel77 = New System.Windows.Forms.Panel
Me.rdbDataCorrectNo_CY2005 = New System.Windows.Forms.RadioButton
Me.rdbDataCorrectYes_CY2005 = New System.Windows.Forms.RadioButton
Me.DTPLetterRemailed_CY2005 = New System.Windows.Forms.DateTimePicker
Me.Panel78 = New System.Windows.Forms.Panel
Me.rdbAddressUnknownNo_CY2005 = New System.Windows.Forms.RadioButton
Me.rdbAddressUnknownYes_CY2005 = New System.Windows.Forms.RadioButton
Me.DTPLetterReturned_CY2005 = New System.Windows.Forms.DateTimePicker
Me.DTPInitialLetter_2005 = New System.Windows.Forms.DateTimePicker
Me.Label103 = New System.Windows.Forms.Label
Me.Label104 = New System.Windows.Forms.Label
Me.Label105 = New System.Windows.Forms.Label
Me.Label106 = New System.Windows.Forms.Label
Me.Label107 = New System.Windows.Forms.Label
Me.Label108 = New System.Windows.Forms.Label
Me.Label109 = New System.Windows.Forms.Label
Me.Label110 = New System.Windows.Forms.Label
Me.Label111 = New System.Windows.Forms.Label
Me.Label112 = New System.Windows.Forms.Label
Me.Label113 = New System.Windows.Forms.Label
Me.Label114 = New System.Windows.Forms.Label
Me.Label115 = New System.Windows.Forms.Label
Me.Panel54 = New System.Windows.Forms.Panel
Me.lblAuditType_CY2005 = New System.Windows.Forms.Label
Me.btnSaveFeeAudit_CY2005 = New System.Windows.Forms.Button
Me.TP_Tracking_CY2004 = New System.Windows.Forms.TabPage
Me.Panel79 = New System.Windows.Forms.Panel
Me.Label43 = New System.Windows.Forms.Label
Me.Label44 = New System.Windows.Forms.Label
Me.Panel80 = New System.Windows.Forms.Panel
Me.btnManagerSignOff_CY2004 = New System.Windows.Forms.Button
Me.lblSignOffDat_04 = New System.Windows.Forms.Label
Me.lblManagerSignOff_04 = New System.Windows.Forms.Label
Me.Panel81 = New System.Windows.Forms.Panel
Me.lblLastModified_04 = New System.Windows.Forms.Label
Me.lblStaffAssigned_04 = New System.Windows.Forms.Label
Me.txtComments_CY2004 = New System.Windows.Forms.TextBox
Me.DTPCloseOut_CY2004 = New System.Windows.Forms.DateTimePicker
Me.lblAmountPaid_CY2004 = New System.Windows.Forms.Label
Me.DTPFeesPaid_CY2004 = New System.Windows.Forms.DateTimePicker
Me.DTPAOSent_CY2004 = New System.Windows.Forms.DateTimePicker
Me.DTPCOSent_CY2004 = New System.Windows.Forms.DateTimePicker
Me.DTPNOVSent_CY2004 = New System.Windows.Forms.DateTimePicker
Me.Panel82 = New System.Windows.Forms.Panel
Me.rdbUnabletoContactNo_CY2004 = New System.Windows.Forms.RadioButton
Me.rdbUnabletoContactYes_CY2004 = New System.Windows.Forms.RadioButton
Me.Panel83 = New System.Windows.Forms.Panel
Me.rdbBankruptcyNo_CY2004 = New System.Windows.Forms.RadioButton
Me.rdbBankruptcyYes_CY2004 = New System.Windows.Forms.RadioButton
Me.Panel84 = New System.Windows.Forms.Panel
Me.rdbDataCorrectNo_CY2004 = New System.Windows.Forms.RadioButton
Me.rdbDataCorrectYes_CY2004 = New System.Windows.Forms.RadioButton
Me.DTPLetterRemailed_CY2004 = New System.Windows.Forms.DateTimePicker
Me.Panel85 = New System.Windows.Forms.Panel
Me.rdbAddressUnknownNo_CY2004 = New System.Windows.Forms.RadioButton
Me.rdbAddressUnknownYes_CY2004 = New System.Windows.Forms.RadioButton
Me.DTPLetterReturned_CY2004 = New System.Windows.Forms.DateTimePicker
Me.DTPInitialLetter_2004 = New System.Windows.Forms.DateTimePicker
Me.Label121 = New System.Windows.Forms.Label
Me.Label122 = New System.Windows.Forms.Label
Me.Label123 = New System.Windows.Forms.Label
Me.Label124 = New System.Windows.Forms.Label
Me.Label125 = New System.Windows.Forms.Label
Me.Label126 = New System.Windows.Forms.Label
Me.Label127 = New System.Windows.Forms.Label
Me.Label128 = New System.Windows.Forms.Label
Me.Label129 = New System.Windows.Forms.Label
Me.Label130 = New System.Windows.Forms.Label
Me.Label131 = New System.Windows.Forms.Label
Me.Label132 = New System.Windows.Forms.Label
Me.Label133 = New System.Windows.Forms.Label
Me.Panel55 = New System.Windows.Forms.Panel
Me.lblAuditType_CY2004 = New System.Windows.Forms.Label
Me.btnSaveFeeAudit_CY2004 = New System.Windows.Forms.Button
Me.TP_Tracking_CY2003 = New System.Windows.Forms.TabPage
Me.Panel86 = New System.Windows.Forms.Panel
Me.Label45 = New System.Windows.Forms.Label
Me.Label46 = New System.Windows.Forms.Label
Me.Panel87 = New System.Windows.Forms.Panel
Me.btnManagerSignOff_CY2003 = New System.Windows.Forms.Button
Me.lblSignOffDat_03 = New System.Windows.Forms.Label
Me.lblManagerSignOff_03 = New System.Windows.Forms.Label
Me.Panel88 = New System.Windows.Forms.Panel
Me.lblLastModified_03 = New System.Windows.Forms.Label
Me.lblStaffAssigned_03 = New System.Windows.Forms.Label
Me.txtComments_CY2003 = New System.Windows.Forms.TextBox
Me.DTPCloseOut_CY2003 = New System.Windows.Forms.DateTimePicker
Me.lblAmountPaid_CY2003 = New System.Windows.Forms.Label
Me.DTPFeesPaid_CY2003 = New System.Windows.Forms.DateTimePicker
Me.DTPAOSent_CY2003 = New System.Windows.Forms.DateTimePicker
Me.DTPCOSent_CY2003 = New System.Windows.Forms.DateTimePicker
Me.DTPNOVSent_CY2003 = New System.Windows.Forms.DateTimePicker
Me.Panel89 = New System.Windows.Forms.Panel
Me.rdbUnabletoContactNo_CY2003 = New System.Windows.Forms.RadioButton
Me.rdbUnabletoContactYes_CY2003 = New System.Windows.Forms.RadioButton
Me.Panel90 = New System.Windows.Forms.Panel
Me.rdbBankruptcyNo_CY2003 = New System.Windows.Forms.RadioButton
Me.rdbBankruptcyYes_CY2003 = New System.Windows.Forms.RadioButton
Me.Panel91 = New System.Windows.Forms.Panel
Me.rdbDataCorrectNo_CY2003 = New System.Windows.Forms.RadioButton
Me.rdbDataCorrectYes_CY2003 = New System.Windows.Forms.RadioButton
Me.DTPLetterRemailed_CY2003 = New System.Windows.Forms.DateTimePicker
Me.Panel92 = New System.Windows.Forms.Panel
Me.rdbAddressUnknownNo_CY2003 = New System.Windows.Forms.RadioButton
Me.rdbAddressUnknownYes_CY2003 = New System.Windows.Forms.RadioButton
Me.DTPLetterReturned_CY2003 = New System.Windows.Forms.DateTimePicker
Me.DTPInitialLetter_2003 = New System.Windows.Forms.DateTimePicker
Me.Label139 = New System.Windows.Forms.Label
Me.Label140 = New System.Windows.Forms.Label
Me.Label141 = New System.Windows.Forms.Label
Me.Label142 = New System.Windows.Forms.Label
Me.Label143 = New System.Windows.Forms.Label
Me.Label144 = New System.Windows.Forms.Label
Me.Label145 = New System.Windows.Forms.Label
Me.Label146 = New System.Windows.Forms.Label
Me.Label147 = New System.Windows.Forms.Label
Me.Label148 = New System.Windows.Forms.Label
Me.Label149 = New System.Windows.Forms.Label
Me.Label150 = New System.Windows.Forms.Label
Me.Label151 = New System.Windows.Forms.Label
Me.Panel56 = New System.Windows.Forms.Panel
Me.lblAuditType_CY2003 = New System.Windows.Forms.Label
Me.btnSaveFeeAudit_CY2003 = New System.Windows.Forms.Button
Me.TP_Tracking_CY2002 = New System.Windows.Forms.TabPage
Me.Panel93 = New System.Windows.Forms.Panel
Me.Label63 = New System.Windows.Forms.Label
Me.Label64 = New System.Windows.Forms.Label
Me.Panel94 = New System.Windows.Forms.Panel
Me.btnManagerSignOff_CY2002 = New System.Windows.Forms.Button
Me.lblSignOffDat_02 = New System.Windows.Forms.Label
Me.lblManagerSignOff_02 = New System.Windows.Forms.Label
Me.Panel95 = New System.Windows.Forms.Panel
Me.lblLastModified_02 = New System.Windows.Forms.Label
Me.lblStaffAssigned_02 = New System.Windows.Forms.Label
Me.txtComments_CY2002 = New System.Windows.Forms.TextBox
Me.DTPCloseOut_CY2002 = New System.Windows.Forms.DateTimePicker
Me.lblAmountPaid_CY2002 = New System.Windows.Forms.Label
Me.DTPFeesPaid_CY2002 = New System.Windows.Forms.DateTimePicker
Me.DTPAOSent_CY2002 = New System.Windows.Forms.DateTimePicker
Me.DTPCOSent_CY2002 = New System.Windows.Forms.DateTimePicker
Me.DTPNOVSent_CY2002 = New System.Windows.Forms.DateTimePicker
Me.Panel96 = New System.Windows.Forms.Panel
Me.rdbUnabletoContactNo_CY2002 = New System.Windows.Forms.RadioButton
Me.rdbUnabletoContactYes_CY2002 = New System.Windows.Forms.RadioButton
Me.Panel97 = New System.Windows.Forms.Panel
Me.rdbBankruptcyNo_CY2002 = New System.Windows.Forms.RadioButton
Me.rdbBankruptcyYes_CY2002 = New System.Windows.Forms.RadioButton
Me.Panel98 = New System.Windows.Forms.Panel
Me.rdbDataCorrectNo_CY2002 = New System.Windows.Forms.RadioButton
Me.rdbDataCorrectYes_CY2002 = New System.Windows.Forms.RadioButton
Me.DTPLetterRemailed_CY2002 = New System.Windows.Forms.DateTimePicker
Me.Panel99 = New System.Windows.Forms.Panel
Me.rdbAddressUnknownNo_CY2002 = New System.Windows.Forms.RadioButton
Me.rdbAddressUnknownYes_CY2002 = New System.Windows.Forms.RadioButton
Me.DTPLetterReturned_CY2002 = New System.Windows.Forms.DateTimePicker
Me.DTPInitialLetter_2002 = New System.Windows.Forms.DateTimePicker
Me.Label157 = New System.Windows.Forms.Label
Me.Label158 = New System.Windows.Forms.Label
Me.Label159 = New System.Windows.Forms.Label
Me.Label160 = New System.Windows.Forms.Label
Me.Label161 = New System.Windows.Forms.Label
Me.Label162 = New System.Windows.Forms.Label
Me.Label163 = New System.Windows.Forms.Label
Me.Label164 = New System.Windows.Forms.Label
Me.Label165 = New System.Windows.Forms.Label
Me.Label166 = New System.Windows.Forms.Label
Me.Label167 = New System.Windows.Forms.Label
Me.Label168 = New System.Windows.Forms.Label
Me.Label169 = New System.Windows.Forms.Label
Me.Panel57 = New System.Windows.Forms.Panel
Me.lblAuditType_CY2002 = New System.Windows.Forms.Label
Me.btnSaveFeeAudit_CY2002 = New System.Windows.Forms.Button
Me.TP_Tracking_OtherComments = New System.Windows.Forms.TabPage
Me.btnSaveAuditComments = New System.Windows.Forms.Button
Me.txtAuditComments = New System.Windows.Forms.TextBox
Me.Label35 = New System.Windows.Forms.Label
Me.TPNonResponders = New System.Windows.Forms.TabPage
Me.Panel1 = New System.Windows.Forms.Panel
Me.TCNonRespondersData = New System.Windows.Forms.TabControl
Me.TP_CY2008 = New System.Windows.Forms.TabPage
Me.Panel25 = New System.Windows.Forms.Panel
Me.gbCY2008 = New System.Windows.Forms.GroupBox
Me.Panel24 = New System.Windows.Forms.Panel
Me.btnSaveCurrentChange_CY2008 = New System.Windows.Forms.Button
Me.txtCurrentComments_CY2008 = New System.Windows.Forms.TextBox
Me.Label32 = New System.Windows.Forms.Label
Me.Panel19 = New System.Windows.Forms.Panel
Me.Panel23 = New System.Windows.Forms.Panel
Me.txtEditContactState_CY2008 = New System.Windows.Forms.TextBox
Me.btnEditContactInfo_CY2008 = New System.Windows.Forms.Button
Me.mtbEditContactZipCode_CY2008 = New System.Windows.Forms.MaskedTextBox
Me.txtEditContactCity_CY2008 = New System.Windows.Forms.TextBox
Me.txtEditContactAddress_CY2008 = New System.Windows.Forms.TextBox
Me.txtEditContactCompany_CY2008 = New System.Windows.Forms.TextBox
Me.txtEditContactLastName_CY2008 = New System.Windows.Forms.TextBox
Me.txtEditContactFirstName_CY2008 = New System.Windows.Forms.TextBox
Me.Label31 = New System.Windows.Forms.Label
Me.Panel20 = New System.Windows.Forms.Panel
Me.txtEditSourceClass_CY2008 = New System.Windows.Forms.TextBox
Me.cboOperatingStatus_CY2008 = New System.Windows.Forms.ComboBox
Me.Panel21 = New System.Windows.Forms.Panel
Me.rdbNSPSNo_CY2008 = New System.Windows.Forms.RadioButton
Me.rdbNSPSYes_CY2008 = New System.Windows.Forms.RadioButton
Me.Panel22 = New System.Windows.Forms.Panel
Me.rdbTVNo_CY2008 = New System.Windows.Forms.RadioButton
Me.rdbTVYes_CY2008 = New System.Windows.Forms.RadioButton
Me.Label25 = New System.Windows.Forms.Label
Me.Label26 = New System.Windows.Forms.Label
Me.Label27 = New System.Windows.Forms.Label
Me.Label28 = New System.Windows.Forms.Label
Me.btnEditFacilityInfo_CY2008 = New System.Windows.Forms.Button
Me.mtbEditZipCode_CY2008 = New System.Windows.Forms.MaskedTextBox
Me.Label29 = New System.Windows.Forms.Label
Me.txtEditFacilityCity_CY2008 = New System.Windows.Forms.TextBox
Me.txtEditFacilityAddress_CY2008 = New System.Windows.Forms.TextBox
Me.txtEditFacilityName_CY2008 = New System.Windows.Forms.TextBox
Me.Label30 = New System.Windows.Forms.Label
Me.Panel16 = New System.Windows.Forms.Panel
Me.Panel18 = New System.Windows.Forms.Panel
Me.lblCY2008Status = New System.Windows.Forms.Label
Me.lblContactAddress2_CY2008 = New System.Windows.Forms.Label
Me.lblContactCompany_CY2008 = New System.Windows.Forms.Label
Me.lblContactName_CY2008 = New System.Windows.Forms.Label
Me.lblContactAddress_CY2008 = New System.Windows.Forms.Label
Me.Panel17 = New System.Windows.Forms.Panel
Me.txtAIRSNumber_08 = New System.Windows.Forms.TextBox
Me.lblNSPS_CY2008 = New System.Windows.Forms.Label
Me.lblTitleV_CY2008 = New System.Windows.Forms.Label
Me.lblSourceClass_CY2008 = New System.Windows.Forms.Label
Me.lblOperatingStatus_CY2008 = New System.Windows.Forms.Label
Me.lblFacilityAddress2_CY2008 = New System.Windows.Forms.Label
Me.lblFacilityAddress_CY2008 = New System.Windows.Forms.Label
Me.llbNoteChanges_CY2008 = New System.Windows.Forms.LinkLabel
Me.lblFacilityName_CY2008 = New System.Windows.Forms.Label
Me.TP_CY2007 = New System.Windows.Forms.TabPage
Me.Panel2 = New System.Windows.Forms.Panel
Me.gbCY2007 = New System.Windows.Forms.GroupBox
Me.Panel44 = New System.Windows.Forms.Panel
Me.btnSaveCurrentChange_CY2007 = New System.Windows.Forms.Button
Me.txtCurrentComments_CY2007 = New System.Windows.Forms.TextBox
Me.Label61 = New System.Windows.Forms.Label
Me.Panel34 = New System.Windows.Forms.Panel
Me.Panel36 = New System.Windows.Forms.Panel
Me.txtEditContactState_CY2007 = New System.Windows.Forms.TextBox
Me.btnEditContactInfo_CY2007 = New System.Windows.Forms.Button
Me.mtbEditContactZipCode_CY2007 = New System.Windows.Forms.MaskedTextBox
Me.txtEditContactCity_CY2007 = New System.Windows.Forms.TextBox
Me.txtEditContactAddress_CY2007 = New System.Windows.Forms.TextBox
Me.txtEditContactCompany_CY2007 = New System.Windows.Forms.TextBox
Me.txtEditContactLastName_CY2007 = New System.Windows.Forms.TextBox
Me.txtEditContactFirstName_CY2007 = New System.Windows.Forms.TextBox
Me.Label47 = New System.Windows.Forms.Label
Me.Panel37 = New System.Windows.Forms.Panel
Me.txtEditSourceClass_CY2007 = New System.Windows.Forms.TextBox
Me.cboOperatingStatus_CY2007 = New System.Windows.Forms.ComboBox
Me.Panel38 = New System.Windows.Forms.Panel
Me.rdbNSPSNo_CY2007 = New System.Windows.Forms.RadioButton
Me.rdbNSPSYes_CY2007 = New System.Windows.Forms.RadioButton
Me.Panel39 = New System.Windows.Forms.Panel
Me.rdbTVNo_CY2007 = New System.Windows.Forms.RadioButton
Me.rdbTVYes_CY2007 = New System.Windows.Forms.RadioButton
Me.Label48 = New System.Windows.Forms.Label
Me.Label49 = New System.Windows.Forms.Label
Me.Label50 = New System.Windows.Forms.Label
Me.Label51 = New System.Windows.Forms.Label
Me.btnEditFacilityInfo_CY2007 = New System.Windows.Forms.Button
Me.mtbEditZipCode_CY2007 = New System.Windows.Forms.MaskedTextBox
Me.Label52 = New System.Windows.Forms.Label
Me.txtEditFacilityCity_CY2007 = New System.Windows.Forms.TextBox
Me.txtEditFacilityAddress_CY2007 = New System.Windows.Forms.TextBox
Me.txtEditFacilityName_CY2007 = New System.Windows.Forms.TextBox
Me.Label53 = New System.Windows.Forms.Label
Me.Panel27 = New System.Windows.Forms.Panel
Me.Panel30 = New System.Windows.Forms.Panel
Me.lblCY2007Status = New System.Windows.Forms.Label
Me.lblContactAddress2_CY2007 = New System.Windows.Forms.Label
Me.lblContactCompany_CY2007 = New System.Windows.Forms.Label
Me.lblContactName_CY2007 = New System.Windows.Forms.Label
Me.lblContactAddress_CY2007 = New System.Windows.Forms.Label
Me.Panel31 = New System.Windows.Forms.Panel
Me.txtAIRSNumber_07 = New System.Windows.Forms.TextBox
Me.lblNSPS_CY2007 = New System.Windows.Forms.Label
Me.lblTitleV_CY2007 = New System.Windows.Forms.Label
Me.lblSourceClass_CY2007 = New System.Windows.Forms.Label
Me.lblOperatingStatus_CY2007 = New System.Windows.Forms.Label
Me.lblFacilityAddress2_CY2007 = New System.Windows.Forms.Label
Me.lblFacilityAddress_CY2007 = New System.Windows.Forms.Label
Me.llbNoteChanges_CY2007 = New System.Windows.Forms.LinkLabel
Me.lblFacilityName_CY2007 = New System.Windows.Forms.Label
Me.TP_CY2006 = New System.Windows.Forms.TabPage
Me.Panel3 = New System.Windows.Forms.Panel
Me.gbCY2006 = New System.Windows.Forms.GroupBox
Me.Panel45 = New System.Windows.Forms.Panel
Me.btnSaveCurrentChange_CY2006 = New System.Windows.Forms.Button
Me.txtCurrentComments_CY2006 = New System.Windows.Forms.TextBox
Me.Label62 = New System.Windows.Forms.Label
Me.Panel35 = New System.Windows.Forms.Panel
Me.Panel40 = New System.Windows.Forms.Panel
Me.txtEditContactState_CY2006 = New System.Windows.Forms.TextBox
Me.btnEditContactInfo_CY2006 = New System.Windows.Forms.Button
Me.mtbEditContactZipCode_CY2006 = New System.Windows.Forms.MaskedTextBox
Me.txtEditContactCity_CY2006 = New System.Windows.Forms.TextBox
Me.txtEditContactAddress_CY2006 = New System.Windows.Forms.TextBox
Me.txtEditContactCompany_CY2006 = New System.Windows.Forms.TextBox
Me.txtEditContactLastName_CY2006 = New System.Windows.Forms.TextBox
Me.txtEditContactFirstName_CY2006 = New System.Windows.Forms.TextBox
Me.Label54 = New System.Windows.Forms.Label
Me.Panel41 = New System.Windows.Forms.Panel
Me.txtEditSourceClass_CY2006 = New System.Windows.Forms.TextBox
Me.cboOperatingStatus_CY2006 = New System.Windows.Forms.ComboBox
Me.Panel42 = New System.Windows.Forms.Panel
Me.rdbNSPSNo_CY2006 = New System.Windows.Forms.RadioButton
Me.rdbNSPSYes_CY2006 = New System.Windows.Forms.RadioButton
Me.Panel43 = New System.Windows.Forms.Panel
Me.rdbTVNo_CY2006 = New System.Windows.Forms.RadioButton
Me.rdbTVYes_CY2006 = New System.Windows.Forms.RadioButton
Me.Label55 = New System.Windows.Forms.Label
Me.Label56 = New System.Windows.Forms.Label
Me.Label57 = New System.Windows.Forms.Label
Me.Label58 = New System.Windows.Forms.Label
Me.btnEditFacilityInfo_CY2006 = New System.Windows.Forms.Button
Me.mtbEditZipCode_CY2006 = New System.Windows.Forms.MaskedTextBox
Me.Label59 = New System.Windows.Forms.Label
Me.txtEditFacilityCity_CY2006 = New System.Windows.Forms.TextBox
Me.txtEditFacilityAddress_CY2006 = New System.Windows.Forms.TextBox
Me.txtEditFacilityName_CY2006 = New System.Windows.Forms.TextBox
Me.Label60 = New System.Windows.Forms.Label
Me.Panel29 = New System.Windows.Forms.Panel
Me.Panel32 = New System.Windows.Forms.Panel
Me.lblCY2006Status = New System.Windows.Forms.Label
Me.lblContactAddress2_CY2006 = New System.Windows.Forms.Label
Me.lblContactCompany_CY2006 = New System.Windows.Forms.Label
Me.lblContactName_CY2006 = New System.Windows.Forms.Label
Me.lblContactAddress_CY2006 = New System.Windows.Forms.Label
Me.Panel33 = New System.Windows.Forms.Panel
Me.txtAIRSNumber_06 = New System.Windows.Forms.TextBox
Me.lblNSPS_CY2006 = New System.Windows.Forms.Label
Me.lblTitleV_CY2006 = New System.Windows.Forms.Label
Me.lblSourceClass_CY2006 = New System.Windows.Forms.Label
Me.lblOperatingStatus_CY2006 = New System.Windows.Forms.Label
Me.lblFacilityAddress2_CY2006 = New System.Windows.Forms.Label
Me.lblFacilityAddress_CY2006 = New System.Windows.Forms.Label
Me.llbNoteChanges_CY2006 = New System.Windows.Forms.LinkLabel
Me.lblFacilityName_CY2006 = New System.Windows.Forms.Label
Me.TP_Change_Questions = New System.Windows.Forms.TabPage
Me.GroupBox1 = New System.Windows.Forms.GroupBox
Me.Panel49 = New System.Windows.Forms.Panel
Me.rdbSourceClassChangeNO = New System.Windows.Forms.RadioButton
Me.rdbSourceClassChangeYes = New System.Windows.Forms.RadioButton
Me.btnSaveSourceClassificationChanges = New System.Windows.Forms.Button
Me.txtSourceClassificationChangeComment = New System.Windows.Forms.TextBox
Me.Label36 = New System.Windows.Forms.Label
Me.gbOwnership = New System.Windows.Forms.GroupBox
Me.btnSaveOwnershipChanges = New System.Windows.Forms.Button
Me.txtOwnershipChangeComments = New System.Windows.Forms.TextBox
Me.Panel4 = New System.Windows.Forms.Panel
Me.rdbOwnershipChangeNo = New System.Windows.Forms.RadioButton
Me.rdbOwnershipChangeYes = New System.Windows.Forms.RadioButton
Me.Label11 = New System.Windows.Forms.Label
Me.TP_Comments = New System.Windows.Forms.TabPage
Me.gbComments = New System.Windows.Forms.GroupBox
Me.btnSaveComments = New System.Windows.Forms.Button
Me.txtComments = New System.Windows.Forms.TextBox
Me.gbTopData = New System.Windows.Forms.GroupBox
Me.Panel13 = New System.Windows.Forms.Panel
Me.btnFlagNonResponder = New System.Windows.Forms.Button
Me.Panel105 = New System.Windows.Forms.Panel
Me.rdbNonResponderActive = New System.Windows.Forms.RadioButton
Me.rdbNonResponderInactive = New System.Windows.Forms.RadioButton
Me.Label65 = New System.Windows.Forms.Label
Me.btnSaveCurrentChange = New System.Windows.Forms.Button
Me.txtCurrentComments = New System.Windows.Forms.TextBox
Me.Label4 = New System.Windows.Forms.Label
Me.Panel10 = New System.Windows.Forms.Panel
Me.Panel11 = New System.Windows.Forms.Panel
Me.txtEditContactEmailAddress = New System.Windows.Forms.TextBox
Me.txtEditContactState = New System.Windows.Forms.TextBox
Me.btnEditContactInfo = New System.Windows.Forms.Button
Me.txtEditContactPhoneNumber = New System.Windows.Forms.TextBox
Me.mtbEditContactZipCode = New System.Windows.Forms.MaskedTextBox
Me.txtEditContactCity = New System.Windows.Forms.TextBox
Me.txtEditContactAddress = New System.Windows.Forms.TextBox
Me.txtEditContactCompany = New System.Windows.Forms.TextBox
Me.txtEditContactTitle = New System.Windows.Forms.TextBox
Me.txtEditContactLastName = New System.Windows.Forms.TextBox
Me.txtEditContactFirstName = New System.Windows.Forms.TextBox
Me.Label5 = New System.Windows.Forms.Label
Me.Panel12 = New System.Windows.Forms.Panel
Me.txtEditSourceClass = New System.Windows.Forms.TextBox
Me.cboOperatingStatus = New System.Windows.Forms.ComboBox
Me.Panel15 = New System.Windows.Forms.Panel
Me.rdbNSPSNo = New System.Windows.Forms.RadioButton
Me.rdbNSPSYes = New System.Windows.Forms.RadioButton
Me.Panel14 = New System.Windows.Forms.Panel
Me.rdbTVNo = New System.Windows.Forms.RadioButton
Me.rdbTVYes = New System.Windows.Forms.RadioButton
Me.Label7 = New System.Windows.Forms.Label
Me.Label8 = New System.Windows.Forms.Label
Me.Label9 = New System.Windows.Forms.Label
Me.Label10 = New System.Windows.Forms.Label
Me.btnEditFacilityInfo = New System.Windows.Forms.Button
Me.mtbEditZipCode = New System.Windows.Forms.MaskedTextBox
Me.Label3 = New System.Windows.Forms.Label
Me.txtEditFacilityCity = New System.Windows.Forms.TextBox
Me.txtEditFacilityAddress = New System.Windows.Forms.TextBox
Me.txtEditFacilityName = New System.Windows.Forms.TextBox
Me.Label6 = New System.Windows.Forms.Label
Me.Panel9 = New System.Windows.Forms.Panel
Me.Panel8 = New System.Windows.Forms.Panel
Me.lblContactEmailAddress = New System.Windows.Forms.Label
Me.lblContactPhoneNumber = New System.Windows.Forms.Label
Me.lblContactAddress2 = New System.Windows.Forms.Label
Me.lblContactCompany = New System.Windows.Forms.Label
Me.lblContactTitle = New System.Windows.Forms.Label
Me.lblContactName = New System.Windows.Forms.Label
Me.lblContactAddress = New System.Windows.Forms.Label
Me.Panel7 = New System.Windows.Forms.Panel
Me.lblNonResponderStaff = New System.Windows.Forms.Label
Me.lblNSPS = New System.Windows.Forms.Label
Me.lblTitleV = New System.Windows.Forms.Label
Me.lblSourceClass = New System.Windows.Forms.Label
Me.lblOperatingStatus = New System.Windows.Forms.Label
Me.lblFacilityAddress2 = New System.Windows.Forms.Label
Me.lblFacilityAddress = New System.Windows.Forms.Label
Me.llbNoteChanges = New System.Windows.Forms.LinkLabel
Me.lblFacilityName = New System.Windows.Forms.Label
Me.Label2 = New System.Windows.Forms.Label
Me.TPNonPayers = New System.Windows.Forms.TabPage
Me.GBContactInformation = New System.Windows.Forms.GroupBox
Me.txtNonPayerEmail = New System.Windows.Forms.TextBox
Me.txtNonPayerState = New System.Windows.Forms.TextBox
Me.btnNonPayerSave = New System.Windows.Forms.Button
Me.txtNonPayerPhoneNumber = New System.Windows.Forms.TextBox
Me.mtbNonPayerZipCode = New System.Windows.Forms.MaskedTextBox
Me.txtNonPayerCity = New System.Windows.Forms.TextBox
Me.txtNonPayerAddress = New System.Windows.Forms.TextBox
Me.txtNonPayerCompany = New System.Windows.Forms.TextBox
Me.txtNonPayerTitle = New System.Windows.Forms.TextBox
Me.txtNonPayerLastName = New System.Windows.Forms.TextBox
Me.txtNonPayerFirstname = New System.Windows.Forms.TextBox
Me.GroupBox2 = New System.Windows.Forms.GroupBox
Me.GBNonPayer_CY02 = New System.Windows.Forms.GroupBox
Me.lblNonPayerBalance_CY02 = New System.Windows.Forms.Label
Me.lblNonPayerTotalPaid_CY02 = New System.Windows.Forms.Label
Me.lblNonPayerTotalDue_CY02 = New System.Windows.Forms.Label
Me.GBNonPayer_CY03 = New System.Windows.Forms.GroupBox
Me.lblNonPayerBalance_CY03 = New System.Windows.Forms.Label
Me.lblNonPayerTotalPaid_CY03 = New System.Windows.Forms.Label
Me.lblNonPayerTotalDue_CY03 = New System.Windows.Forms.Label
Me.GBNonPayer_CY04 = New System.Windows.Forms.GroupBox
Me.lblNonPayerBalance_CY04 = New System.Windows.Forms.Label
Me.lblNonPayerTotalPaid_CY04 = New System.Windows.Forms.Label
Me.lblNonPayerTotalDue_CY04 = New System.Windows.Forms.Label
Me.GBNonPayer_CY05 = New System.Windows.Forms.GroupBox
Me.lblNonPayerBalance_CY05 = New System.Windows.Forms.Label
Me.lblNonPayerTotalPaid_CY05 = New System.Windows.Forms.Label
Me.lblNonPayerTotalDue_CY05 = New System.Windows.Forms.Label
Me.GBNonPayer_CY06 = New System.Windows.Forms.GroupBox
Me.lblNonPayerBalance_CY06 = New System.Windows.Forms.Label
Me.lblNonPayerTotalPaid_CY06 = New System.Windows.Forms.Label
Me.lblNonPayerTotalDue_CY06 = New System.Windows.Forms.Label
Me.GBNonPayer_CY07 = New System.Windows.Forms.GroupBox
Me.lblNonPayerBalance_CY07 = New System.Windows.Forms.Label
Me.lblNonPayerTotalPaid_CY07 = New System.Windows.Forms.Label
Me.lblNonPayerTotalDue_CY07 = New System.Windows.Forms.Label
Me.GBNonPayer_CY08 = New System.Windows.Forms.GroupBox
Me.lblNonPayerBalance_CY08 = New System.Windows.Forms.Label
Me.lblNonPayerTotalPaid_CY08 = New System.Windows.Forms.Label
Me.lblNonPayerTotalDue_CY08 = New System.Windows.Forms.Label
Me.Panel102 = New System.Windows.Forms.Panel
Me.Panel104 = New System.Windows.Forms.Panel
Me.btnCopyData = New System.Windows.Forms.Button
Me.btnSaveAndUpdate = New System.Windows.Forms.Button
Me.lblNonPayerContactEmail = New System.Windows.Forms.Label
Me.lblNonPayerContactPhoneNumber = New System.Windows.Forms.Label
Me.lblNonPayerContactAddress = New System.Windows.Forms.Label
Me.lblNonPayerContactCompany = New System.Windows.Forms.Label
Me.lblNonPayerContactTitle = New System.Windows.Forms.Label
Me.lblNonPayerContactName = New System.Windows.Forms.Label
Me.Label117 = New System.Windows.Forms.Label
Me.Panel103 = New System.Windows.Forms.Panel
Me.lblNonPayerStaff = New System.Windows.Forms.Label
Me.btnFlagNonPayer = New System.Windows.Forms.Button
Me.Panel107 = New System.Windows.Forms.Panel
Me.rdbNonPayerActive = New System.Windows.Forms.RadioButton
Me.rdbNonPayerInactive = New System.Windows.Forms.RadioButton
Me.Label66 = New System.Windows.Forms.Label
Me.lblNonPayerNSPSStatus = New System.Windows.Forms.Label
Me.lblNonPayerTVStatus = New System.Windows.Forms.Label
Me.lblNonPayerSourceClass = New System.Windows.Forms.Label
Me.lblNonPayerOpStatus = New System.Windows.Forms.Label
Me.lblNonPayerFacilityAddress = New System.Windows.Forms.Label
Me.Label83 = New System.Windows.Forms.Label
Me.lblNonPayerFacilityName = New System.Windows.Forms.Label
Me.Panel108 = New System.Windows.Forms.Panel
Me.btnSaveNonPayerComments = New System.Windows.Forms.Button
Me.Label81 = New System.Windows.Forms.Label
Me.txtNonPayersComments = New System.Windows.Forms.TextBox
Me.TPAuditReport = New System.Windows.Forms.TabPage
Me.Panel101 = New System.Windows.Forms.Panel
Me.dgvFeeAuditReport = New System.Windows.Forms.DataGridView
Me.Panel100 = New System.Windows.Forms.Panel
Me.GroupBox3 = New System.Windows.Forms.GroupBox
Me.txtOpenAudits = New System.Windows.Forms.TextBox
Me.Label116 = New System.Windows.Forms.Label
Me.txtClosedOutAudits = New System.Windows.Forms.TextBox
Me.Label102 = New System.Windows.Forms.Label
Me.txtPossibleNOV = New System.Windows.Forms.TextBox
Me.Label101 = New System.Windows.Forms.Label
Me.txtNonPayerSent = New System.Windows.Forms.TextBox
Me.txtNonRespondersSent = New System.Windows.Forms.TextBox
Me.Label100 = New System.Windows.Forms.Label
Me.Label99 = New System.Windows.Forms.Label
Me.btnRunStats = New System.Windows.Forms.Button
Me.txtTotalLetterSent = New System.Windows.Forms.TextBox
Me.Label82 = New System.Windows.Forms.Label
Me.Panel111 = New System.Windows.Forms.Panel
Me.Panel106 = New System.Windows.Forms.Panel
Me.rdbActiveAll = New System.Windows.Forms.RadioButton
Me.rdbInactive = New System.Windows.Forms.RadioButton
Me.rdbActive = New System.Windows.Forms.RadioButton
Me.btnNonRespondersData = New System.Windows.Forms.Button
Me.Panel110 = New System.Windows.Forms.Panel
Me.rdbAllClosedOut = New System.Windows.Forms.RadioButton
Me.rdbAuditNotClosedout = New System.Windows.Forms.RadioButton
Me.rdbAuditClosedOut = New System.Windows.Forms.RadioButton
Me.btnViewFullAuditData = New System.Windows.Forms.Button
Me.Panel109 = New System.Windows.Forms.Panel
Me.rdbAll = New System.Windows.Forms.RadioButton
Me.rdbNonPayers = New System.Windows.Forms.RadioButton
Me.rdbNonResponders = New System.Windows.Forms.RadioButton
Me.btnExportToExcel = New System.Windows.Forms.Button
Me.Label80 = New System.Windows.Forms.Label
Me.btnViewAllNonPayerData = New System.Windows.Forms.Button
Me.txtCount = New System.Windows.Forms.TextBox
Me.btnGetEmailAddresses = New System.Windows.Forms.Button
Me.pnlTop1 = New System.Windows.Forms.Panel
Me.txtNonPayerStaff = New System.Windows.Forms.TextBox
Me.txtNonPayerID = New System.Windows.Forms.TextBox
Me.txtNonResponderStaff = New System.Windows.Forms.TextBox
Me.txtAuditID = New System.Windows.Forms.TextBox
Me.lblFacilityNameTop = New System.Windows.Forms.Label
Me.txtNonRespondersID = New System.Windows.Forms.TextBox
Me.btnSearchForData = New System.Windows.Forms.Button
Me.Label1 = New System.Windows.Forms.Label
Me.mtbAIRSNumber = New System.Windows.Forms.MaskedTextBox
Me.StatusStrip1.SuspendLayout
Me.MenuStrip1.SuspendLayout
Me.TCNonResponders.SuspendLayout
Me.TPTrackingData.SuspendLayout
Me.Panel5.SuspendLayout
Me.TCFeeAuditTracking.SuspendLayout
Me.TP_Tracking_CY2008.SuspendLayout
Me.Panel6.SuspendLayout
Me.Panel51.SuspendLayout
Me.Panel50.SuspendLayout
Me.Panel47.SuspendLayout
Me.Panel46.SuspendLayout
Me.Panel28.SuspendLayout
Me.Panel26.SuspendLayout
Me.Panel48.SuspendLayout
Me.TP_Tracking_CY2007.SuspendLayout
Me.Panel58.SuspendLayout
Me.Panel59.SuspendLayout
Me.Panel60.SuspendLayout
Me.Panel61.SuspendLayout
Me.Panel62.SuspendLayout
Me.Panel63.SuspendLayout
Me.Panel64.SuspendLayout
Me.Panel52.SuspendLayout
Me.TP_Tracking_CY2006.SuspendLayout
Me.Panel65.SuspendLayout
Me.Panel66.SuspendLayout
Me.Panel67.SuspendLayout
Me.Panel68.SuspendLayout
Me.Panel69.SuspendLayout
Me.Panel70.SuspendLayout
Me.Panel71.SuspendLayout
Me.Panel53.SuspendLayout
Me.TP_Tracking_CY2005.SuspendLayout
Me.Panel72.SuspendLayout
Me.Panel73.SuspendLayout
Me.Panel74.SuspendLayout
Me.Panel75.SuspendLayout
Me.Panel76.SuspendLayout
Me.Panel77.SuspendLayout
Me.Panel78.SuspendLayout
Me.Panel54.SuspendLayout
Me.TP_Tracking_CY2004.SuspendLayout
Me.Panel79.SuspendLayout
Me.Panel80.SuspendLayout
Me.Panel81.SuspendLayout
Me.Panel82.SuspendLayout
Me.Panel83.SuspendLayout
Me.Panel84.SuspendLayout
Me.Panel85.SuspendLayout
Me.Panel55.SuspendLayout
Me.TP_Tracking_CY2003.SuspendLayout
Me.Panel86.SuspendLayout
Me.Panel87.SuspendLayout
Me.Panel88.SuspendLayout
Me.Panel89.SuspendLayout
Me.Panel90.SuspendLayout
Me.Panel91.SuspendLayout
Me.Panel92.SuspendLayout
Me.Panel56.SuspendLayout
Me.TP_Tracking_CY2002.SuspendLayout
Me.Panel93.SuspendLayout
Me.Panel94.SuspendLayout
Me.Panel95.SuspendLayout
Me.Panel96.SuspendLayout
Me.Panel97.SuspendLayout
Me.Panel98.SuspendLayout
Me.Panel99.SuspendLayout
Me.Panel57.SuspendLayout
Me.TP_Tracking_OtherComments.SuspendLayout
Me.TPNonResponders.SuspendLayout
Me.Panel1.SuspendLayout
Me.TCNonRespondersData.SuspendLayout
Me.TP_CY2008.SuspendLayout
Me.Panel25.SuspendLayout
Me.gbCY2008.SuspendLayout
Me.Panel24.SuspendLayout
Me.Panel19.SuspendLayout
Me.Panel23.SuspendLayout
Me.Panel20.SuspendLayout
Me.Panel21.SuspendLayout
Me.Panel22.SuspendLayout
Me.Panel16.SuspendLayout
Me.Panel18.SuspendLayout
Me.Panel17.SuspendLayout
Me.TP_CY2007.SuspendLayout
Me.Panel2.SuspendLayout
Me.gbCY2007.SuspendLayout
Me.Panel44.SuspendLayout
Me.Panel34.SuspendLayout
Me.Panel36.SuspendLayout
Me.Panel37.SuspendLayout
Me.Panel38.SuspendLayout
Me.Panel39.SuspendLayout
Me.Panel27.SuspendLayout
Me.Panel30.SuspendLayout
Me.Panel31.SuspendLayout
Me.TP_CY2006.SuspendLayout
Me.Panel3.SuspendLayout
Me.gbCY2006.SuspendLayout
Me.Panel45.SuspendLayout
Me.Panel35.SuspendLayout
Me.Panel40.SuspendLayout
Me.Panel41.SuspendLayout
Me.Panel42.SuspendLayout
Me.Panel43.SuspendLayout
Me.Panel29.SuspendLayout
Me.Panel32.SuspendLayout
Me.Panel33.SuspendLayout
Me.TP_Change_Questions.SuspendLayout
Me.GroupBox1.SuspendLayout
Me.Panel49.SuspendLayout
Me.gbOwnership.SuspendLayout
Me.Panel4.SuspendLayout
Me.TP_Comments.SuspendLayout
Me.gbComments.SuspendLayout
Me.gbTopData.SuspendLayout
Me.Panel13.SuspendLayout
Me.Panel105.SuspendLayout
Me.Panel10.SuspendLayout
Me.Panel11.SuspendLayout
Me.Panel12.SuspendLayout
Me.Panel15.SuspendLayout
Me.Panel14.SuspendLayout
Me.Panel9.SuspendLayout
Me.Panel8.SuspendLayout
Me.Panel7.SuspendLayout
Me.TPNonPayers.SuspendLayout
Me.GBContactInformation.SuspendLayout
Me.GroupBox2.SuspendLayout
Me.GBNonPayer_CY02.SuspendLayout
Me.GBNonPayer_CY03.SuspendLayout
Me.GBNonPayer_CY04.SuspendLayout
Me.GBNonPayer_CY05.SuspendLayout
Me.GBNonPayer_CY06.SuspendLayout
Me.GBNonPayer_CY07.SuspendLayout
Me.GBNonPayer_CY08.SuspendLayout
Me.Panel102.SuspendLayout
Me.Panel104.SuspendLayout
Me.Panel103.SuspendLayout
Me.Panel107.SuspendLayout
Me.Panel108.SuspendLayout
Me.TPAuditReport.SuspendLayout
Me.Panel101.SuspendLayout
CType(Me.dgvFeeAuditReport,System.ComponentModel.ISupportInitialize).BeginInit
Me.Panel100.SuspendLayout
Me.GroupBox3.SuspendLayout
Me.Panel111.SuspendLayout
Me.Panel106.SuspendLayout
Me.Panel110.SuspendLayout
Me.Panel109.SuspendLayout
Me.pnlTop1.SuspendLayout
Me.SuspendLayout
'
'StatusStrip1
'
Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pnl1, Me.pnl2, Me.pnl3})
Me.StatusStrip1.Location = New System.Drawing.Point(0, 724)
Me.StatusStrip1.Name = "StatusStrip1"
Me.StatusStrip1.Size = New System.Drawing.Size(792, 22)
Me.StatusStrip1.TabIndex = 8
Me.StatusStrip1.Text = "StatusStrip1"
'
'pnl1
'
Me.pnl1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)  _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)  _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom),System.Windows.Forms.ToolStripStatusLabelBorderSides)
Me.pnl1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
Me.pnl1.Name = "pnl1"
Me.pnl1.Size = New System.Drawing.Size(769, 17)
Me.pnl1.Spring = true
Me.pnl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'pnl2
'
Me.pnl2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)  _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)  _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom),System.Windows.Forms.ToolStripStatusLabelBorderSides)
Me.pnl2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
Me.pnl2.Name = "pnl2"
Me.pnl2.Size = New System.Drawing.Size(4, 17)
'
'pnl3
'
Me.pnl3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)  _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)  _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom),System.Windows.Forms.ToolStripStatusLabelBorderSides)
Me.pnl3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
Me.pnl3.Name = "pnl3"
Me.pnl3.Size = New System.Drawing.Size(4, 17)
'
'MenuStrip1
'
Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem})
Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
Me.MenuStrip1.Name = "MenuStrip1"
Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
Me.MenuStrip1.TabIndex = 7
Me.MenuStrip1.Text = "MenuStrip1"
'
'FileToolStripMenuItem
'
Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
Me.FileToolStripMenuItem.Text = "File"
'
'EditToolStripMenuItem
'
Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
Me.EditToolStripMenuItem.Text = "Edit"
'
'HelpToolStripMenuItem
'
Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
Me.HelpToolStripMenuItem.Text = "Help"
'
'TCNonResponders
'
Me.TCNonResponders.Controls.Add(Me.TPTrackingData)
Me.TCNonResponders.Controls.Add(Me.TPNonResponders)
Me.TCNonResponders.Controls.Add(Me.TPNonPayers)
Me.TCNonResponders.Controls.Add(Me.TPAuditReport)
Me.TCNonResponders.Dock = System.Windows.Forms.DockStyle.Fill
Me.TCNonResponders.Location = New System.Drawing.Point(0, 63)
Me.TCNonResponders.Name = "TCNonResponders"
Me.TCNonResponders.SelectedIndex = 0
Me.TCNonResponders.Size = New System.Drawing.Size(792, 661)
Me.TCNonResponders.TabIndex = 9
'
'TPTrackingData
'
Me.TPTrackingData.Controls.Add(Me.Panel5)
Me.TPTrackingData.Location = New System.Drawing.Point(4, 22)
Me.TPTrackingData.Name = "TPTrackingData"
Me.TPTrackingData.Padding = New System.Windows.Forms.Padding(3)
Me.TPTrackingData.Size = New System.Drawing.Size(784, 635)
Me.TPTrackingData.TabIndex = 1
Me.TPTrackingData.Text = "Tracking Data"
Me.TPTrackingData.UseVisualStyleBackColor = true
'
'Panel5
'
Me.Panel5.Controls.Add(Me.TCFeeAuditTracking)
Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel5.Location = New System.Drawing.Point(3, 3)
Me.Panel5.Name = "Panel5"
Me.Panel5.Size = New System.Drawing.Size(778, 629)
Me.Panel5.TabIndex = 0
'
'TCFeeAuditTracking
'
Me.TCFeeAuditTracking.Controls.Add(Me.TP_Tracking_CY2008)
Me.TCFeeAuditTracking.Controls.Add(Me.TP_Tracking_CY2007)
Me.TCFeeAuditTracking.Controls.Add(Me.TP_Tracking_CY2006)
Me.TCFeeAuditTracking.Controls.Add(Me.TP_Tracking_CY2005)
Me.TCFeeAuditTracking.Controls.Add(Me.TP_Tracking_CY2004)
Me.TCFeeAuditTracking.Controls.Add(Me.TP_Tracking_CY2003)
Me.TCFeeAuditTracking.Controls.Add(Me.TP_Tracking_CY2002)
Me.TCFeeAuditTracking.Controls.Add(Me.TP_Tracking_OtherComments)
Me.TCFeeAuditTracking.Dock = System.Windows.Forms.DockStyle.Fill
Me.TCFeeAuditTracking.Location = New System.Drawing.Point(0, 0)
Me.TCFeeAuditTracking.Name = "TCFeeAuditTracking"
Me.TCFeeAuditTracking.SelectedIndex = 0
Me.TCFeeAuditTracking.Size = New System.Drawing.Size(778, 629)
Me.TCFeeAuditTracking.TabIndex = 0
'
'TP_Tracking_CY2008
'
Me.TP_Tracking_CY2008.Controls.Add(Me.Panel6)
Me.TP_Tracking_CY2008.Controls.Add(Me.Panel48)
Me.TP_Tracking_CY2008.Location = New System.Drawing.Point(4, 22)
Me.TP_Tracking_CY2008.Name = "TP_Tracking_CY2008"
Me.TP_Tracking_CY2008.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Tracking_CY2008.Size = New System.Drawing.Size(770, 603)
Me.TP_Tracking_CY2008.TabIndex = 0
Me.TP_Tracking_CY2008.Text = "CY2008"
Me.TP_Tracking_CY2008.UseVisualStyleBackColor = true
'
'Panel6
'
Me.Panel6.AutoScroll = true
Me.Panel6.Controls.Add(Me.Label34)
Me.Panel6.Controls.Add(Me.Label33)
Me.Panel6.Controls.Add(Me.Panel51)
Me.Panel6.Controls.Add(Me.Panel50)
Me.Panel6.Controls.Add(Me.txtComments_CY2008)
Me.Panel6.Controls.Add(Me.DTPCloseOut_CY2008)
Me.Panel6.Controls.Add(Me.lblAmountPaid_CY2008)
Me.Panel6.Controls.Add(Me.DTPFeesPaid_CY2008)
Me.Panel6.Controls.Add(Me.DTPAOSent_CY2008)
Me.Panel6.Controls.Add(Me.DTPCOSent_CY2008)
Me.Panel6.Controls.Add(Me.DTPNOVSent_CY2008)
Me.Panel6.Controls.Add(Me.Panel47)
Me.Panel6.Controls.Add(Me.Panel46)
Me.Panel6.Controls.Add(Me.Panel28)
Me.Panel6.Controls.Add(Me.DTPLetterRemailed_CY2008)
Me.Panel6.Controls.Add(Me.Panel26)
Me.Panel6.Controls.Add(Me.DTPLetterReturned_CY2008)
Me.Panel6.Controls.Add(Me.DTPInitialLetter_2008)
Me.Panel6.Controls.Add(Me.Label24)
Me.Panel6.Controls.Add(Me.Label23)
Me.Panel6.Controls.Add(Me.Label22)
Me.Panel6.Controls.Add(Me.Label21)
Me.Panel6.Controls.Add(Me.Label20)
Me.Panel6.Controls.Add(Me.Label19)
Me.Panel6.Controls.Add(Me.Label18)
Me.Panel6.Controls.Add(Me.Label17)
Me.Panel6.Controls.Add(Me.Label16)
Me.Panel6.Controls.Add(Me.Label15)
Me.Panel6.Controls.Add(Me.Label14)
Me.Panel6.Controls.Add(Me.Label13)
Me.Panel6.Controls.Add(Me.Label12)
Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel6.Location = New System.Drawing.Point(3, 35)
Me.Panel6.Name = "Panel6"
Me.Panel6.Size = New System.Drawing.Size(764, 565)
Me.Panel6.TabIndex = 1
'
'Label34
'
Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label34.Location = New System.Drawing.Point(0, 269)
Me.Label34.Name = "Label34"
Me.Label34.Size = New System.Drawing.Size(764, 2)
Me.Label34.TabIndex = 393
'
'Label33
'
Me.Label33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label33.Location = New System.Drawing.Point(0, 183)
Me.Label33.Name = "Label33"
Me.Label33.Size = New System.Drawing.Size(764, 2)
Me.Label33.TabIndex = 392
'
'Panel51
'
Me.Panel51.Controls.Add(Me.btnManagerSignOff_CY2008)
Me.Panel51.Controls.Add(Me.lblSignOffDat_08)
Me.Panel51.Controls.Add(Me.lblManagerSignOff_08)
Me.Panel51.Location = New System.Drawing.Point(465, 197)
Me.Panel51.Name = "Panel51"
Me.Panel51.Size = New System.Drawing.Size(246, 61)
Me.Panel51.TabIndex = 391
'
'btnManagerSignOff_CY2008
'
Me.btnManagerSignOff_CY2008.AutoSize = true
Me.btnManagerSignOff_CY2008.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnManagerSignOff_CY2008.Location = New System.Drawing.Point(6, 26)
Me.btnManagerSignOff_CY2008.Name = "btnManagerSignOff_CY2008"
Me.btnManagerSignOff_CY2008.Size = New System.Drawing.Size(55, 23)
Me.btnManagerSignOff_CY2008.TabIndex = 394
Me.btnManagerSignOff_CY2008.Text = "Sign-Off"
Me.btnManagerSignOff_CY2008.UseVisualStyleBackColor = true
'
'lblSignOffDat_08
'
Me.lblSignOffDat_08.AutoSize = true
Me.lblSignOffDat_08.Location = New System.Drawing.Point(67, 31)
Me.lblSignOffDat_08.Name = "lblSignOffDat_08"
Me.lblSignOffDat_08.Size = New System.Drawing.Size(39, 13)
Me.lblSignOffDat_08.TabIndex = 2
Me.lblSignOffDat_08.Text = "Date - "
'
'lblManagerSignOff_08
'
Me.lblManagerSignOff_08.AutoSize = true
Me.lblManagerSignOff_08.Location = New System.Drawing.Point(3, 10)
Me.lblManagerSignOff_08.Name = "lblManagerSignOff_08"
Me.lblManagerSignOff_08.Size = New System.Drawing.Size(88, 13)
Me.lblManagerSignOff_08.TabIndex = 1
Me.lblManagerSignOff_08.Text = "Manager Sign-off"
'
'Panel50
'
Me.Panel50.Controls.Add(Me.lblLastModified_08)
Me.Panel50.Controls.Add(Me.lblStaffAssigned_08)
Me.Panel50.Location = New System.Drawing.Point(465, 111)
Me.Panel50.Name = "Panel50"
Me.Panel50.Size = New System.Drawing.Size(246, 61)
Me.Panel50.TabIndex = 390
'
'lblLastModified_08
'
Me.lblLastModified_08.AutoSize = true
Me.lblLastModified_08.Location = New System.Drawing.Point(3, 32)
Me.lblLastModified_08.Name = "lblLastModified_08"
Me.lblLastModified_08.Size = New System.Drawing.Size(70, 13)
Me.lblLastModified_08.TabIndex = 2
Me.lblLastModified_08.Text = "Last Modified"
'
'lblStaffAssigned_08
'
Me.lblStaffAssigned_08.AutoSize = true
Me.lblStaffAssigned_08.Location = New System.Drawing.Point(3, 10)
Me.lblStaffAssigned_08.Name = "lblStaffAssigned_08"
Me.lblStaffAssigned_08.Size = New System.Drawing.Size(95, 13)
Me.lblStaffAssigned_08.TabIndex = 1
Me.lblStaffAssigned_08.Text = "Staff Last Modified"
'
'txtComments_CY2008
'
Me.txtComments_CY2008.Location = New System.Drawing.Point(62, 348)
Me.txtComments_CY2008.Multiline = true
Me.txtComments_CY2008.Name = "txtComments_CY2008"
Me.txtComments_CY2008.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtComments_CY2008.Size = New System.Drawing.Size(383, 64)
Me.txtComments_CY2008.TabIndex = 388
'
'DTPCloseOut_CY2008
'
Me.DTPCloseOut_CY2008.Checked = false
Me.DTPCloseOut_CY2008.CustomFormat = "dd-MMM-yyyy"
Me.DTPCloseOut_CY2008.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCloseOut_CY2008.Location = New System.Drawing.Point(116, 314)
Me.DTPCloseOut_CY2008.Name = "DTPCloseOut_CY2008"
Me.DTPCloseOut_CY2008.ShowCheckBox = true
Me.DTPCloseOut_CY2008.Size = New System.Drawing.Size(102, 20)
Me.DTPCloseOut_CY2008.TabIndex = 387
Me.DTPCloseOut_CY2008.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'lblAmountPaid_CY2008
'
Me.lblAmountPaid_CY2008.AutoSize = true
Me.lblAmountPaid_CY2008.Location = New System.Drawing.Point(202, 279)
Me.lblAmountPaid_CY2008.Name = "lblAmountPaid_CY2008"
Me.lblAmountPaid_CY2008.Size = New System.Drawing.Size(63, 13)
Me.lblAmountPaid_CY2008.TabIndex = 386
Me.lblAmountPaid_CY2008.Text = "Fees Paid: -"
'
'DTPFeesPaid_CY2008
'
Me.DTPFeesPaid_CY2008.Checked = false
Me.DTPFeesPaid_CY2008.CustomFormat = "dd-MMM-yyyy"
Me.DTPFeesPaid_CY2008.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPFeesPaid_CY2008.Location = New System.Drawing.Point(93, 275)
Me.DTPFeesPaid_CY2008.Name = "DTPFeesPaid_CY2008"
Me.DTPFeesPaid_CY2008.ShowCheckBox = true
Me.DTPFeesPaid_CY2008.Size = New System.Drawing.Size(102, 20)
Me.DTPFeesPaid_CY2008.TabIndex = 384
Me.DTPFeesPaid_CY2008.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPAOSent_CY2008
'
Me.DTPAOSent_CY2008.Checked = false
Me.DTPAOSent_CY2008.CustomFormat = "dd-MMM-yyyy"
Me.DTPAOSent_CY2008.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPAOSent_CY2008.Location = New System.Drawing.Point(93, 245)
Me.DTPAOSent_CY2008.Name = "DTPAOSent_CY2008"
Me.DTPAOSent_CY2008.ShowCheckBox = true
Me.DTPAOSent_CY2008.Size = New System.Drawing.Size(102, 20)
Me.DTPAOSent_CY2008.TabIndex = 383
Me.DTPAOSent_CY2008.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPCOSent_CY2008
'
Me.DTPCOSent_CY2008.Checked = false
Me.DTPCOSent_CY2008.CustomFormat = "dd-MMM-yyyy"
Me.DTPCOSent_CY2008.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCOSent_CY2008.Location = New System.Drawing.Point(93, 219)
Me.DTPCOSent_CY2008.Name = "DTPCOSent_CY2008"
Me.DTPCOSent_CY2008.ShowCheckBox = true
Me.DTPCOSent_CY2008.Size = New System.Drawing.Size(102, 20)
Me.DTPCOSent_CY2008.TabIndex = 382
Me.DTPCOSent_CY2008.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPNOVSent_CY2008
'
Me.DTPNOVSent_CY2008.Checked = false
Me.DTPNOVSent_CY2008.CustomFormat = "dd-MMM-yyyy"
Me.DTPNOVSent_CY2008.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPNOVSent_CY2008.Location = New System.Drawing.Point(93, 193)
Me.DTPNOVSent_CY2008.Name = "DTPNOVSent_CY2008"
Me.DTPNOVSent_CY2008.ShowCheckBox = true
Me.DTPNOVSent_CY2008.Size = New System.Drawing.Size(102, 20)
Me.DTPNOVSent_CY2008.TabIndex = 381
Me.DTPNOVSent_CY2008.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel47
'
Me.Panel47.AutoSize = true
Me.Panel47.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel47.Controls.Add(Me.rdbUnabletoContactNo_CY2008)
Me.Panel47.Controls.Add(Me.rdbUnabletoContactYes_CY2008)
Me.Panel47.Location = New System.Drawing.Point(276, 154)
Me.Panel47.Name = "Panel47"
Me.Panel47.Size = New System.Drawing.Size(94, 23)
Me.Panel47.TabIndex = 380
'
'rdbUnabletoContactNo_CY2008
'
Me.rdbUnabletoContactNo_CY2008.AutoSize = true
Me.rdbUnabletoContactNo_CY2008.Location = New System.Drawing.Point(50, 3)
Me.rdbUnabletoContactNo_CY2008.Name = "rdbUnabletoContactNo_CY2008"
Me.rdbUnabletoContactNo_CY2008.Size = New System.Drawing.Size(41, 17)
Me.rdbUnabletoContactNo_CY2008.TabIndex = 1
Me.rdbUnabletoContactNo_CY2008.TabStop = true
Me.rdbUnabletoContactNo_CY2008.Text = "NO"
Me.rdbUnabletoContactNo_CY2008.UseVisualStyleBackColor = true
'
'rdbUnabletoContactYes_CY2008
'
Me.rdbUnabletoContactYes_CY2008.AutoSize = true
Me.rdbUnabletoContactYes_CY2008.Location = New System.Drawing.Point(3, 3)
Me.rdbUnabletoContactYes_CY2008.Name = "rdbUnabletoContactYes_CY2008"
Me.rdbUnabletoContactYes_CY2008.Size = New System.Drawing.Size(46, 17)
Me.rdbUnabletoContactYes_CY2008.TabIndex = 0
Me.rdbUnabletoContactYes_CY2008.TabStop = true
Me.rdbUnabletoContactYes_CY2008.Text = "YES"
Me.rdbUnabletoContactYes_CY2008.UseVisualStyleBackColor = true
'
'Panel46
'
Me.Panel46.AutoSize = true
Me.Panel46.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel46.Controls.Add(Me.rdbBankruptcyNo_CY2008)
Me.Panel46.Controls.Add(Me.rdbBankruptcyYes_CY2008)
Me.Panel46.Location = New System.Drawing.Point(84, 128)
Me.Panel46.Name = "Panel46"
Me.Panel46.Size = New System.Drawing.Size(94, 23)
Me.Panel46.TabIndex = 379
'
'rdbBankruptcyNo_CY2008
'
Me.rdbBankruptcyNo_CY2008.AutoSize = true
Me.rdbBankruptcyNo_CY2008.Location = New System.Drawing.Point(50, 3)
Me.rdbBankruptcyNo_CY2008.Name = "rdbBankruptcyNo_CY2008"
Me.rdbBankruptcyNo_CY2008.Size = New System.Drawing.Size(41, 17)
Me.rdbBankruptcyNo_CY2008.TabIndex = 1
Me.rdbBankruptcyNo_CY2008.TabStop = true
Me.rdbBankruptcyNo_CY2008.Text = "NO"
Me.rdbBankruptcyNo_CY2008.UseVisualStyleBackColor = true
'
'rdbBankruptcyYes_CY2008
'
Me.rdbBankruptcyYes_CY2008.AutoSize = true
Me.rdbBankruptcyYes_CY2008.Location = New System.Drawing.Point(3, 3)
Me.rdbBankruptcyYes_CY2008.Name = "rdbBankruptcyYes_CY2008"
Me.rdbBankruptcyYes_CY2008.Size = New System.Drawing.Size(46, 17)
Me.rdbBankruptcyYes_CY2008.TabIndex = 0
Me.rdbBankruptcyYes_CY2008.TabStop = true
Me.rdbBankruptcyYes_CY2008.Text = "YES"
Me.rdbBankruptcyYes_CY2008.UseVisualStyleBackColor = true
'
'Panel28
'
Me.Panel28.AutoSize = true
Me.Panel28.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel28.Controls.Add(Me.rdbDataCorrectNo_CY2008)
Me.Panel28.Controls.Add(Me.rdbDataCorrectYes_CY2008)
Me.Panel28.Location = New System.Drawing.Point(214, 92)
Me.Panel28.Name = "Panel28"
Me.Panel28.Size = New System.Drawing.Size(94, 23)
Me.Panel28.TabIndex = 378
'
'rdbDataCorrectNo_CY2008
'
Me.rdbDataCorrectNo_CY2008.AutoSize = true
Me.rdbDataCorrectNo_CY2008.Location = New System.Drawing.Point(50, 3)
Me.rdbDataCorrectNo_CY2008.Name = "rdbDataCorrectNo_CY2008"
Me.rdbDataCorrectNo_CY2008.Size = New System.Drawing.Size(41, 17)
Me.rdbDataCorrectNo_CY2008.TabIndex = 1
Me.rdbDataCorrectNo_CY2008.TabStop = true
Me.rdbDataCorrectNo_CY2008.Text = "NO"
Me.rdbDataCorrectNo_CY2008.UseVisualStyleBackColor = true
'
'rdbDataCorrectYes_CY2008
'
Me.rdbDataCorrectYes_CY2008.AutoSize = true
Me.rdbDataCorrectYes_CY2008.Location = New System.Drawing.Point(3, 3)
Me.rdbDataCorrectYes_CY2008.Name = "rdbDataCorrectYes_CY2008"
Me.rdbDataCorrectYes_CY2008.Size = New System.Drawing.Size(46, 17)
Me.rdbDataCorrectYes_CY2008.TabIndex = 0
Me.rdbDataCorrectYes_CY2008.TabStop = true
Me.rdbDataCorrectYes_CY2008.Text = "YES"
Me.rdbDataCorrectYes_CY2008.UseVisualStyleBackColor = true
'
'DTPLetterRemailed_CY2008
'
Me.DTPLetterRemailed_CY2008.Checked = false
Me.DTPLetterRemailed_CY2008.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterRemailed_CY2008.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterRemailed_CY2008.Location = New System.Drawing.Point(465, 64)
Me.DTPLetterRemailed_CY2008.Name = "DTPLetterRemailed_CY2008"
Me.DTPLetterRemailed_CY2008.ShowCheckBox = true
Me.DTPLetterRemailed_CY2008.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterRemailed_CY2008.TabIndex = 377
Me.DTPLetterRemailed_CY2008.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel26
'
Me.Panel26.AutoSize = true
Me.Panel26.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel26.Controls.Add(Me.rdbAddressUnknownNo_CY2008)
Me.Panel26.Controls.Add(Me.rdbAddressUnknownYes_CY2008)
Me.Panel26.Location = New System.Drawing.Point(214, 63)
Me.Panel26.Name = "Panel26"
Me.Panel26.Size = New System.Drawing.Size(94, 23)
Me.Panel26.TabIndex = 376
'
'rdbAddressUnknownNo_CY2008
'
Me.rdbAddressUnknownNo_CY2008.AutoSize = true
Me.rdbAddressUnknownNo_CY2008.Location = New System.Drawing.Point(50, 3)
Me.rdbAddressUnknownNo_CY2008.Name = "rdbAddressUnknownNo_CY2008"
Me.rdbAddressUnknownNo_CY2008.Size = New System.Drawing.Size(41, 17)
Me.rdbAddressUnknownNo_CY2008.TabIndex = 1
Me.rdbAddressUnknownNo_CY2008.TabStop = true
Me.rdbAddressUnknownNo_CY2008.Text = "NO"
Me.rdbAddressUnknownNo_CY2008.UseVisualStyleBackColor = true
'
'rdbAddressUnknownYes_CY2008
'
Me.rdbAddressUnknownYes_CY2008.AutoSize = true
Me.rdbAddressUnknownYes_CY2008.Location = New System.Drawing.Point(3, 3)
Me.rdbAddressUnknownYes_CY2008.Name = "rdbAddressUnknownYes_CY2008"
Me.rdbAddressUnknownYes_CY2008.Size = New System.Drawing.Size(46, 17)
Me.rdbAddressUnknownYes_CY2008.TabIndex = 0
Me.rdbAddressUnknownYes_CY2008.TabStop = true
Me.rdbAddressUnknownYes_CY2008.Text = "YES"
Me.rdbAddressUnknownYes_CY2008.UseVisualStyleBackColor = true
'
'DTPLetterReturned_CY2008
'
Me.DTPLetterReturned_CY2008.Checked = false
Me.DTPLetterReturned_CY2008.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterReturned_CY2008.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterReturned_CY2008.Location = New System.Drawing.Point(193, 33)
Me.DTPLetterReturned_CY2008.Name = "DTPLetterReturned_CY2008"
Me.DTPLetterReturned_CY2008.ShowCheckBox = true
Me.DTPLetterReturned_CY2008.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterReturned_CY2008.TabIndex = 375
Me.DTPLetterReturned_CY2008.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPInitialLetter_2008
'
Me.DTPInitialLetter_2008.Checked = false
Me.DTPInitialLetter_2008.CustomFormat = "dd-MMM-yyyy"
Me.DTPInitialLetter_2008.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPInitialLetter_2008.Location = New System.Drawing.Point(98, 7)
Me.DTPInitialLetter_2008.Name = "DTPInitialLetter_2008"
Me.DTPInitialLetter_2008.ShowCheckBox = true
Me.DTPInitialLetter_2008.Size = New System.Drawing.Size(102, 20)
Me.DTPInitialLetter_2008.TabIndex = 374
Me.DTPInitialLetter_2008.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Label24
'
Me.Label24.AutoSize = true
Me.Label24.Location = New System.Drawing.Point(4, 348)
Me.Label24.Name = "Label24"
Me.Label24.Size = New System.Drawing.Size(56, 13)
Me.Label24.TabIndex = 12
Me.Label24.Text = "Comments"
'
'Label23
'
Me.Label23.AutoSize = true
Me.Label23.Location = New System.Drawing.Point(4, 318)
Me.Label23.Name = "Label23"
Me.Label23.Size = New System.Drawing.Size(101, 13)
Me.Label23.TabIndex = 11
Me.Label23.Text = "Close Out Fee Audit"
'
'Label22
'
Me.Label22.AutoSize = true
Me.Label22.Location = New System.Drawing.Point(4, 223)
Me.Label22.Name = "Label22"
Me.Label22.Size = New System.Drawing.Size(77, 13)
Me.Label22.TabIndex = 10
Me.Label22.Text = "CO Letter Sent"
'
'Label21
'
Me.Label21.AutoSize = true
Me.Label21.Location = New System.Drawing.Point(4, 279)
Me.Label21.Name = "Label21"
Me.Label21.Size = New System.Drawing.Size(87, 13)
Me.Label21.TabIndex = 9
Me.Label21.Text = "Facilty Paid Fees"
'
'Label20
'
Me.Label20.AutoSize = true
Me.Label20.Location = New System.Drawing.Point(4, 249)
Me.Label20.Name = "Label20"
Me.Label20.Size = New System.Drawing.Size(77, 13)
Me.Label20.TabIndex = 8
Me.Label20.Text = "AO Letter Sent"
'
'Label19
'
Me.Label19.AutoSize = true
Me.Label19.Location = New System.Drawing.Point(4, 133)
Me.Label19.Name = "Label19"
Me.Label19.Size = New System.Drawing.Size(61, 13)
Me.Label19.TabIndex = 7
Me.Label19.Text = "Bankruptcy"
'
'Label18
'
Me.Label18.AutoSize = true
Me.Label18.Location = New System.Drawing.Point(4, 159)
Me.Label18.Name = "Label18"
Me.Label18.Size = New System.Drawing.Size(252, 13)
Me.Label18.TabIndex = 6
Me.Label18.Text = "Unable to Contact Facility Or Facility Representative"
'
'Label17
'
Me.Label17.AutoSize = true
Me.Label17.Location = New System.Drawing.Point(4, 197)
Me.Label17.Name = "Label17"
Me.Label17.Size = New System.Drawing.Size(85, 13)
Me.Label17.TabIndex = 5
Me.Label17.Text = "NOV Letter Sent"
'
'Label16
'
Me.Label16.AutoSize = true
Me.Label16.Location = New System.Drawing.Point(4, 37)
Me.Label16.Name = "Label16"
Me.Label16.Size = New System.Drawing.Size(183, 13)
Me.Label16.TabIndex = 4
Me.Label16.Text = "Letter Returned/Response Received"
'
'Label15
'
Me.Label15.AutoSize = true
Me.Label15.Location = New System.Drawing.Point(83, 68)
Me.Label15.Name = "Label15"
Me.Label15.Size = New System.Drawing.Size(125, 13)
Me.Label15.TabIndex = 3
Me.Label15.Text = "Was Address Unknown?"
'
'Label14
'
Me.Label14.AutoSize = true
Me.Label14.Location = New System.Drawing.Point(342, 68)
Me.Label14.Name = "Label14"
Me.Label14.Size = New System.Drawing.Size(112, 13)
Me.Label14.TabIndex = 2
Me.Label14.Text = "Initial Letter Re-Mailed"
'
'Label13
'
Me.Label13.AutoSize = true
Me.Label13.Location = New System.Drawing.Point(4, 97)
Me.Label13.Name = "Label13"
Me.Label13.Size = New System.Drawing.Size(196, 13)
Me.Label13.TabIndex = 1
Me.Label13.Text = "Facility Responded with Data Correction"
'
'Label12
'
Me.Label12.AutoSize = true
Me.Label12.Location = New System.Drawing.Point(4, 11)
Me.Label12.Name = "Label12"
Me.Label12.Size = New System.Drawing.Size(95, 13)
Me.Label12.TabIndex = 0
Me.Label12.Text = "Initial Letter Mailed"
'
'Panel48
'
Me.Panel48.Controls.Add(Me.lblAuditType_CY2008)
Me.Panel48.Controls.Add(Me.btnSaveFeeAudit_CY2008)
Me.Panel48.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel48.Location = New System.Drawing.Point(3, 3)
Me.Panel48.Name = "Panel48"
Me.Panel48.Size = New System.Drawing.Size(764, 32)
Me.Panel48.TabIndex = 2
'
'lblAuditType_CY2008
'
Me.lblAuditType_CY2008.AutoSize = true
Me.lblAuditType_CY2008.Location = New System.Drawing.Point(49, 8)
Me.lblAuditType_CY2008.Name = "lblAuditType_CY2008"
Me.lblAuditType_CY2008.Size = New System.Drawing.Size(58, 13)
Me.lblAuditType_CY2008.TabIndex = 4
Me.lblAuditType_CY2008.Text = "Audit Type"
'
'btnSaveFeeAudit_CY2008
'
Me.btnSaveFeeAudit_CY2008.AutoSize = true
Me.btnSaveFeeAudit_CY2008.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveFeeAudit_CY2008.Location = New System.Drawing.Point(3, 3)
Me.btnSaveFeeAudit_CY2008.Name = "btnSaveFeeAudit_CY2008"
Me.btnSaveFeeAudit_CY2008.Size = New System.Drawing.Size(42, 23)
Me.btnSaveFeeAudit_CY2008.TabIndex = 389
Me.btnSaveFeeAudit_CY2008.Text = "Save"
Me.btnSaveFeeAudit_CY2008.UseVisualStyleBackColor = true
'
'TP_Tracking_CY2007
'
Me.TP_Tracking_CY2007.Controls.Add(Me.Panel58)
Me.TP_Tracking_CY2007.Controls.Add(Me.Panel52)
Me.TP_Tracking_CY2007.Location = New System.Drawing.Point(4, 22)
Me.TP_Tracking_CY2007.Name = "TP_Tracking_CY2007"
Me.TP_Tracking_CY2007.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Tracking_CY2007.Size = New System.Drawing.Size(770, 603)
Me.TP_Tracking_CY2007.TabIndex = 1
Me.TP_Tracking_CY2007.Text = "CY2007"
Me.TP_Tracking_CY2007.UseVisualStyleBackColor = true
'
'Panel58
'
Me.Panel58.AutoScroll = true
Me.Panel58.Controls.Add(Me.Label37)
Me.Panel58.Controls.Add(Me.Label38)
Me.Panel58.Controls.Add(Me.Panel59)
Me.Panel58.Controls.Add(Me.Panel60)
Me.Panel58.Controls.Add(Me.txtComments_CY2007)
Me.Panel58.Controls.Add(Me.DTPCloseOut_CY2007)
Me.Panel58.Controls.Add(Me.lblAmountPaid_CY2007)
Me.Panel58.Controls.Add(Me.DTPFeesPaid_CY2007)
Me.Panel58.Controls.Add(Me.DTPAOSent_CY2007)
Me.Panel58.Controls.Add(Me.DTPCOSent_CY2007)
Me.Panel58.Controls.Add(Me.DTPNOVSent_CY2007)
Me.Panel58.Controls.Add(Me.Panel61)
Me.Panel58.Controls.Add(Me.Panel62)
Me.Panel58.Controls.Add(Me.Panel63)
Me.Panel58.Controls.Add(Me.DTPLetterRemailed_CY2007)
Me.Panel58.Controls.Add(Me.Panel64)
Me.Panel58.Controls.Add(Me.DTPLetterReturned_CY2007)
Me.Panel58.Controls.Add(Me.DTPInitialLetter_2007)
Me.Panel58.Controls.Add(Me.Label67)
Me.Panel58.Controls.Add(Me.Label68)
Me.Panel58.Controls.Add(Me.Label69)
Me.Panel58.Controls.Add(Me.Label70)
Me.Panel58.Controls.Add(Me.Label71)
Me.Panel58.Controls.Add(Me.Label72)
Me.Panel58.Controls.Add(Me.Label73)
Me.Panel58.Controls.Add(Me.Label74)
Me.Panel58.Controls.Add(Me.Label75)
Me.Panel58.Controls.Add(Me.Label76)
Me.Panel58.Controls.Add(Me.Label77)
Me.Panel58.Controls.Add(Me.Label78)
Me.Panel58.Controls.Add(Me.Label79)
Me.Panel58.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel58.Location = New System.Drawing.Point(3, 35)
Me.Panel58.Name = "Panel58"
Me.Panel58.Size = New System.Drawing.Size(764, 565)
Me.Panel58.TabIndex = 4
'
'Label37
'
Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label37.Location = New System.Drawing.Point(0, 269)
Me.Label37.Name = "Label37"
Me.Label37.Size = New System.Drawing.Size(764, 2)
Me.Label37.TabIndex = 395
'
'Label38
'
Me.Label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label38.Location = New System.Drawing.Point(0, 183)
Me.Label38.Name = "Label38"
Me.Label38.Size = New System.Drawing.Size(764, 2)
Me.Label38.TabIndex = 394
'
'Panel59
'
Me.Panel59.Controls.Add(Me.btnManagerSignOff_CY2007)
Me.Panel59.Controls.Add(Me.lblSignOffDat_07)
Me.Panel59.Controls.Add(Me.lblManagerSignOff_07)
Me.Panel59.Location = New System.Drawing.Point(465, 197)
Me.Panel59.Name = "Panel59"
Me.Panel59.Size = New System.Drawing.Size(246, 61)
Me.Panel59.TabIndex = 391
'
'btnManagerSignOff_CY2007
'
Me.btnManagerSignOff_CY2007.AutoSize = true
Me.btnManagerSignOff_CY2007.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnManagerSignOff_CY2007.Location = New System.Drawing.Point(6, 26)
Me.btnManagerSignOff_CY2007.Name = "btnManagerSignOff_CY2007"
Me.btnManagerSignOff_CY2007.Size = New System.Drawing.Size(55, 23)
Me.btnManagerSignOff_CY2007.TabIndex = 395
Me.btnManagerSignOff_CY2007.Text = "Sign-Off"
Me.btnManagerSignOff_CY2007.UseVisualStyleBackColor = true
'
'lblSignOffDat_07
'
Me.lblSignOffDat_07.AutoSize = true
Me.lblSignOffDat_07.Location = New System.Drawing.Point(67, 31)
Me.lblSignOffDat_07.Name = "lblSignOffDat_07"
Me.lblSignOffDat_07.Size = New System.Drawing.Size(39, 13)
Me.lblSignOffDat_07.TabIndex = 2
Me.lblSignOffDat_07.Text = "Date - "
'
'lblManagerSignOff_07
'
Me.lblManagerSignOff_07.AutoSize = true
Me.lblManagerSignOff_07.Location = New System.Drawing.Point(3, 10)
Me.lblManagerSignOff_07.Name = "lblManagerSignOff_07"
Me.lblManagerSignOff_07.Size = New System.Drawing.Size(88, 13)
Me.lblManagerSignOff_07.TabIndex = 1
Me.lblManagerSignOff_07.Text = "Manager Sign-off"
'
'Panel60
'
Me.Panel60.Controls.Add(Me.lblLastModified_07)
Me.Panel60.Controls.Add(Me.lblStaffAssigned_07)
Me.Panel60.Location = New System.Drawing.Point(465, 111)
Me.Panel60.Name = "Panel60"
Me.Panel60.Size = New System.Drawing.Size(246, 61)
Me.Panel60.TabIndex = 390
'
'lblLastModified_07
'
Me.lblLastModified_07.AutoSize = true
Me.lblLastModified_07.Location = New System.Drawing.Point(3, 32)
Me.lblLastModified_07.Name = "lblLastModified_07"
Me.lblLastModified_07.Size = New System.Drawing.Size(70, 13)
Me.lblLastModified_07.TabIndex = 2
Me.lblLastModified_07.Text = "Last Modified"
'
'lblStaffAssigned_07
'
Me.lblStaffAssigned_07.AutoSize = true
Me.lblStaffAssigned_07.Location = New System.Drawing.Point(3, 10)
Me.lblStaffAssigned_07.Name = "lblStaffAssigned_07"
Me.lblStaffAssigned_07.Size = New System.Drawing.Size(95, 13)
Me.lblStaffAssigned_07.TabIndex = 1
Me.lblStaffAssigned_07.Text = "Staff Last Modified"
'
'txtComments_CY2007
'
Me.txtComments_CY2007.Location = New System.Drawing.Point(62, 348)
Me.txtComments_CY2007.Multiline = true
Me.txtComments_CY2007.Name = "txtComments_CY2007"
Me.txtComments_CY2007.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtComments_CY2007.Size = New System.Drawing.Size(383, 64)
Me.txtComments_CY2007.TabIndex = 388
'
'DTPCloseOut_CY2007
'
Me.DTPCloseOut_CY2007.Checked = false
Me.DTPCloseOut_CY2007.CustomFormat = "dd-MMM-yyyy"
Me.DTPCloseOut_CY2007.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCloseOut_CY2007.Location = New System.Drawing.Point(116, 314)
Me.DTPCloseOut_CY2007.Name = "DTPCloseOut_CY2007"
Me.DTPCloseOut_CY2007.ShowCheckBox = true
Me.DTPCloseOut_CY2007.Size = New System.Drawing.Size(102, 20)
Me.DTPCloseOut_CY2007.TabIndex = 387
Me.DTPCloseOut_CY2007.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'lblAmountPaid_CY2007
'
Me.lblAmountPaid_CY2007.AutoSize = true
Me.lblAmountPaid_CY2007.Location = New System.Drawing.Point(202, 279)
Me.lblAmountPaid_CY2007.Name = "lblAmountPaid_CY2007"
Me.lblAmountPaid_CY2007.Size = New System.Drawing.Size(63, 13)
Me.lblAmountPaid_CY2007.TabIndex = 386
Me.lblAmountPaid_CY2007.Text = "Fees Paid: -"
'
'DTPFeesPaid_CY2007
'
Me.DTPFeesPaid_CY2007.Checked = false
Me.DTPFeesPaid_CY2007.CustomFormat = "dd-MMM-yyyy"
Me.DTPFeesPaid_CY2007.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPFeesPaid_CY2007.Location = New System.Drawing.Point(93, 275)
Me.DTPFeesPaid_CY2007.Name = "DTPFeesPaid_CY2007"
Me.DTPFeesPaid_CY2007.ShowCheckBox = true
Me.DTPFeesPaid_CY2007.Size = New System.Drawing.Size(102, 20)
Me.DTPFeesPaid_CY2007.TabIndex = 384
Me.DTPFeesPaid_CY2007.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPAOSent_CY2007
'
Me.DTPAOSent_CY2007.Checked = false
Me.DTPAOSent_CY2007.CustomFormat = "dd-MMM-yyyy"
Me.DTPAOSent_CY2007.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPAOSent_CY2007.Location = New System.Drawing.Point(93, 245)
Me.DTPAOSent_CY2007.Name = "DTPAOSent_CY2007"
Me.DTPAOSent_CY2007.ShowCheckBox = true
Me.DTPAOSent_CY2007.Size = New System.Drawing.Size(102, 20)
Me.DTPAOSent_CY2007.TabIndex = 383
Me.DTPAOSent_CY2007.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPCOSent_CY2007
'
Me.DTPCOSent_CY2007.Checked = false
Me.DTPCOSent_CY2007.CustomFormat = "dd-MMM-yyyy"
Me.DTPCOSent_CY2007.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCOSent_CY2007.Location = New System.Drawing.Point(93, 219)
Me.DTPCOSent_CY2007.Name = "DTPCOSent_CY2007"
Me.DTPCOSent_CY2007.ShowCheckBox = true
Me.DTPCOSent_CY2007.Size = New System.Drawing.Size(102, 20)
Me.DTPCOSent_CY2007.TabIndex = 382
Me.DTPCOSent_CY2007.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPNOVSent_CY2007
'
Me.DTPNOVSent_CY2007.Checked = false
Me.DTPNOVSent_CY2007.CustomFormat = "dd-MMM-yyyy"
Me.DTPNOVSent_CY2007.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPNOVSent_CY2007.Location = New System.Drawing.Point(93, 193)
Me.DTPNOVSent_CY2007.Name = "DTPNOVSent_CY2007"
Me.DTPNOVSent_CY2007.ShowCheckBox = true
Me.DTPNOVSent_CY2007.Size = New System.Drawing.Size(102, 20)
Me.DTPNOVSent_CY2007.TabIndex = 381
Me.DTPNOVSent_CY2007.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel61
'
Me.Panel61.AutoSize = true
Me.Panel61.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel61.Controls.Add(Me.rdbUnabletoContactNo_CY2007)
Me.Panel61.Controls.Add(Me.rdbUnabletoContactYes_CY2007)
Me.Panel61.Location = New System.Drawing.Point(276, 154)
Me.Panel61.Name = "Panel61"
Me.Panel61.Size = New System.Drawing.Size(94, 23)
Me.Panel61.TabIndex = 380
'
'rdbUnabletoContactNo_CY2007
'
Me.rdbUnabletoContactNo_CY2007.AutoSize = true
Me.rdbUnabletoContactNo_CY2007.Location = New System.Drawing.Point(50, 3)
Me.rdbUnabletoContactNo_CY2007.Name = "rdbUnabletoContactNo_CY2007"
Me.rdbUnabletoContactNo_CY2007.Size = New System.Drawing.Size(41, 17)
Me.rdbUnabletoContactNo_CY2007.TabIndex = 1
Me.rdbUnabletoContactNo_CY2007.TabStop = true
Me.rdbUnabletoContactNo_CY2007.Text = "NO"
Me.rdbUnabletoContactNo_CY2007.UseVisualStyleBackColor = true
'
'rdbUnabletoContactYes_CY2007
'
Me.rdbUnabletoContactYes_CY2007.AutoSize = true
Me.rdbUnabletoContactYes_CY2007.Location = New System.Drawing.Point(3, 3)
Me.rdbUnabletoContactYes_CY2007.Name = "rdbUnabletoContactYes_CY2007"
Me.rdbUnabletoContactYes_CY2007.Size = New System.Drawing.Size(46, 17)
Me.rdbUnabletoContactYes_CY2007.TabIndex = 0
Me.rdbUnabletoContactYes_CY2007.TabStop = true
Me.rdbUnabletoContactYes_CY2007.Text = "YES"
Me.rdbUnabletoContactYes_CY2007.UseVisualStyleBackColor = true
'
'Panel62
'
Me.Panel62.AutoSize = true
Me.Panel62.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel62.Controls.Add(Me.rdbBankruptcyNo_CY2007)
Me.Panel62.Controls.Add(Me.rdbBankruptcyYes_CY2007)
Me.Panel62.Location = New System.Drawing.Point(84, 128)
Me.Panel62.Name = "Panel62"
Me.Panel62.Size = New System.Drawing.Size(94, 23)
Me.Panel62.TabIndex = 379
'
'rdbBankruptcyNo_CY2007
'
Me.rdbBankruptcyNo_CY2007.AutoSize = true
Me.rdbBankruptcyNo_CY2007.Location = New System.Drawing.Point(50, 3)
Me.rdbBankruptcyNo_CY2007.Name = "rdbBankruptcyNo_CY2007"
Me.rdbBankruptcyNo_CY2007.Size = New System.Drawing.Size(41, 17)
Me.rdbBankruptcyNo_CY2007.TabIndex = 1
Me.rdbBankruptcyNo_CY2007.TabStop = true
Me.rdbBankruptcyNo_CY2007.Text = "NO"
Me.rdbBankruptcyNo_CY2007.UseVisualStyleBackColor = true
'
'rdbBankruptcyYes_CY2007
'
Me.rdbBankruptcyYes_CY2007.AutoSize = true
Me.rdbBankruptcyYes_CY2007.Location = New System.Drawing.Point(3, 3)
Me.rdbBankruptcyYes_CY2007.Name = "rdbBankruptcyYes_CY2007"
Me.rdbBankruptcyYes_CY2007.Size = New System.Drawing.Size(46, 17)
Me.rdbBankruptcyYes_CY2007.TabIndex = 0
Me.rdbBankruptcyYes_CY2007.TabStop = true
Me.rdbBankruptcyYes_CY2007.Text = "YES"
Me.rdbBankruptcyYes_CY2007.UseVisualStyleBackColor = true
'
'Panel63
'
Me.Panel63.AutoSize = true
Me.Panel63.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel63.Controls.Add(Me.rdbDataCorrectNo_CY2007)
Me.Panel63.Controls.Add(Me.rdbDataCorrectYes_CY2007)
Me.Panel63.Location = New System.Drawing.Point(214, 92)
Me.Panel63.Name = "Panel63"
Me.Panel63.Size = New System.Drawing.Size(94, 23)
Me.Panel63.TabIndex = 378
'
'rdbDataCorrectNo_CY2007
'
Me.rdbDataCorrectNo_CY2007.AutoSize = true
Me.rdbDataCorrectNo_CY2007.Location = New System.Drawing.Point(50, 3)
Me.rdbDataCorrectNo_CY2007.Name = "rdbDataCorrectNo_CY2007"
Me.rdbDataCorrectNo_CY2007.Size = New System.Drawing.Size(41, 17)
Me.rdbDataCorrectNo_CY2007.TabIndex = 1
Me.rdbDataCorrectNo_CY2007.TabStop = true
Me.rdbDataCorrectNo_CY2007.Text = "NO"
Me.rdbDataCorrectNo_CY2007.UseVisualStyleBackColor = true
'
'rdbDataCorrectYes_CY2007
'
Me.rdbDataCorrectYes_CY2007.AutoSize = true
Me.rdbDataCorrectYes_CY2007.Location = New System.Drawing.Point(3, 3)
Me.rdbDataCorrectYes_CY2007.Name = "rdbDataCorrectYes_CY2007"
Me.rdbDataCorrectYes_CY2007.Size = New System.Drawing.Size(46, 17)
Me.rdbDataCorrectYes_CY2007.TabIndex = 0
Me.rdbDataCorrectYes_CY2007.TabStop = true
Me.rdbDataCorrectYes_CY2007.Text = "YES"
Me.rdbDataCorrectYes_CY2007.UseVisualStyleBackColor = true
'
'DTPLetterRemailed_CY2007
'
Me.DTPLetterRemailed_CY2007.Checked = false
Me.DTPLetterRemailed_CY2007.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterRemailed_CY2007.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterRemailed_CY2007.Location = New System.Drawing.Point(465, 63)
Me.DTPLetterRemailed_CY2007.Name = "DTPLetterRemailed_CY2007"
Me.DTPLetterRemailed_CY2007.ShowCheckBox = true
Me.DTPLetterRemailed_CY2007.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterRemailed_CY2007.TabIndex = 377
Me.DTPLetterRemailed_CY2007.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel64
'
Me.Panel64.AutoSize = true
Me.Panel64.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel64.Controls.Add(Me.rdbAddressUnknownNo_CY2007)
Me.Panel64.Controls.Add(Me.rdbAddressUnknownYes_CY2007)
Me.Panel64.Location = New System.Drawing.Point(214, 62)
Me.Panel64.Name = "Panel64"
Me.Panel64.Size = New System.Drawing.Size(94, 23)
Me.Panel64.TabIndex = 376
'
'rdbAddressUnknownNo_CY2007
'
Me.rdbAddressUnknownNo_CY2007.AutoSize = true
Me.rdbAddressUnknownNo_CY2007.Location = New System.Drawing.Point(50, 3)
Me.rdbAddressUnknownNo_CY2007.Name = "rdbAddressUnknownNo_CY2007"
Me.rdbAddressUnknownNo_CY2007.Size = New System.Drawing.Size(41, 17)
Me.rdbAddressUnknownNo_CY2007.TabIndex = 1
Me.rdbAddressUnknownNo_CY2007.TabStop = true
Me.rdbAddressUnknownNo_CY2007.Text = "NO"
Me.rdbAddressUnknownNo_CY2007.UseVisualStyleBackColor = true
'
'rdbAddressUnknownYes_CY2007
'
Me.rdbAddressUnknownYes_CY2007.AutoSize = true
Me.rdbAddressUnknownYes_CY2007.Location = New System.Drawing.Point(3, 3)
Me.rdbAddressUnknownYes_CY2007.Name = "rdbAddressUnknownYes_CY2007"
Me.rdbAddressUnknownYes_CY2007.Size = New System.Drawing.Size(46, 17)
Me.rdbAddressUnknownYes_CY2007.TabIndex = 0
Me.rdbAddressUnknownYes_CY2007.TabStop = true
Me.rdbAddressUnknownYes_CY2007.Text = "YES"
Me.rdbAddressUnknownYes_CY2007.UseVisualStyleBackColor = true
'
'DTPLetterReturned_CY2007
'
Me.DTPLetterReturned_CY2007.Checked = false
Me.DTPLetterReturned_CY2007.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterReturned_CY2007.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterReturned_CY2007.Location = New System.Drawing.Point(193, 33)
Me.DTPLetterReturned_CY2007.Name = "DTPLetterReturned_CY2007"
Me.DTPLetterReturned_CY2007.ShowCheckBox = true
Me.DTPLetterReturned_CY2007.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterReturned_CY2007.TabIndex = 375
Me.DTPLetterReturned_CY2007.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPInitialLetter_2007
'
Me.DTPInitialLetter_2007.Checked = false
Me.DTPInitialLetter_2007.CustomFormat = "dd-MMM-yyyy"
Me.DTPInitialLetter_2007.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPInitialLetter_2007.Location = New System.Drawing.Point(98, 7)
Me.DTPInitialLetter_2007.Name = "DTPInitialLetter_2007"
Me.DTPInitialLetter_2007.ShowCheckBox = true
Me.DTPInitialLetter_2007.Size = New System.Drawing.Size(102, 20)
Me.DTPInitialLetter_2007.TabIndex = 374
Me.DTPInitialLetter_2007.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Label67
'
Me.Label67.AutoSize = true
Me.Label67.Location = New System.Drawing.Point(4, 348)
Me.Label67.Name = "Label67"
Me.Label67.Size = New System.Drawing.Size(56, 13)
Me.Label67.TabIndex = 12
Me.Label67.Text = "Comments"
'
'Label68
'
Me.Label68.AutoSize = true
Me.Label68.Location = New System.Drawing.Point(4, 318)
Me.Label68.Name = "Label68"
Me.Label68.Size = New System.Drawing.Size(101, 13)
Me.Label68.TabIndex = 11
Me.Label68.Text = "Close Out Fee Audit"
'
'Label69
'
Me.Label69.AutoSize = true
Me.Label69.Location = New System.Drawing.Point(4, 223)
Me.Label69.Name = "Label69"
Me.Label69.Size = New System.Drawing.Size(77, 13)
Me.Label69.TabIndex = 10
Me.Label69.Text = "CO Letter Sent"
'
'Label70
'
Me.Label70.AutoSize = true
Me.Label70.Location = New System.Drawing.Point(4, 279)
Me.Label70.Name = "Label70"
Me.Label70.Size = New System.Drawing.Size(87, 13)
Me.Label70.TabIndex = 9
Me.Label70.Text = "Facilty Paid Fees"
'
'Label71
'
Me.Label71.AutoSize = true
Me.Label71.Location = New System.Drawing.Point(4, 249)
Me.Label71.Name = "Label71"
Me.Label71.Size = New System.Drawing.Size(77, 13)
Me.Label71.TabIndex = 8
Me.Label71.Text = "AO Letter Sent"
'
'Label72
'
Me.Label72.AutoSize = true
Me.Label72.Location = New System.Drawing.Point(4, 133)
Me.Label72.Name = "Label72"
Me.Label72.Size = New System.Drawing.Size(61, 13)
Me.Label72.TabIndex = 7
Me.Label72.Text = "Bankruptcy"
'
'Label73
'
Me.Label73.AutoSize = true
Me.Label73.Location = New System.Drawing.Point(4, 159)
Me.Label73.Name = "Label73"
Me.Label73.Size = New System.Drawing.Size(252, 13)
Me.Label73.TabIndex = 6
Me.Label73.Text = "Unable to Contact Facility Or Facility Representative"
'
'Label74
'
Me.Label74.AutoSize = true
Me.Label74.Location = New System.Drawing.Point(4, 197)
Me.Label74.Name = "Label74"
Me.Label74.Size = New System.Drawing.Size(85, 13)
Me.Label74.TabIndex = 5
Me.Label74.Text = "NOV Letter Sent"
'
'Label75
'
Me.Label75.AutoSize = true
Me.Label75.Location = New System.Drawing.Point(4, 37)
Me.Label75.Name = "Label75"
Me.Label75.Size = New System.Drawing.Size(183, 13)
Me.Label75.TabIndex = 4
Me.Label75.Text = "Letter Returned/Response Received"
'
'Label76
'
Me.Label76.AutoSize = true
Me.Label76.Location = New System.Drawing.Point(83, 67)
Me.Label76.Name = "Label76"
Me.Label76.Size = New System.Drawing.Size(125, 13)
Me.Label76.TabIndex = 3
Me.Label76.Text = "Was Address Unknown?"
'
'Label77
'
Me.Label77.AutoSize = true
Me.Label77.Location = New System.Drawing.Point(342, 67)
Me.Label77.Name = "Label77"
Me.Label77.Size = New System.Drawing.Size(112, 13)
Me.Label77.TabIndex = 2
Me.Label77.Text = "Initial Letter Re-Mailed"
'
'Label78
'
Me.Label78.AutoSize = true
Me.Label78.Location = New System.Drawing.Point(4, 97)
Me.Label78.Name = "Label78"
Me.Label78.Size = New System.Drawing.Size(196, 13)
Me.Label78.TabIndex = 1
Me.Label78.Text = "Facility Responded with Data Correction"
'
'Label79
'
Me.Label79.AutoSize = true
Me.Label79.Location = New System.Drawing.Point(4, 11)
Me.Label79.Name = "Label79"
Me.Label79.Size = New System.Drawing.Size(95, 13)
Me.Label79.TabIndex = 0
Me.Label79.Text = "Initial Letter Mailed"
'
'Panel52
'
Me.Panel52.Controls.Add(Me.lblAuditType_CY2007)
Me.Panel52.Controls.Add(Me.btnSaveFeeAudit_CY2007)
Me.Panel52.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel52.Location = New System.Drawing.Point(3, 3)
Me.Panel52.Name = "Panel52"
Me.Panel52.Size = New System.Drawing.Size(764, 32)
Me.Panel52.TabIndex = 3
'
'lblAuditType_CY2007
'
Me.lblAuditType_CY2007.AutoSize = true
Me.lblAuditType_CY2007.Location = New System.Drawing.Point(49, 8)
Me.lblAuditType_CY2007.Name = "lblAuditType_CY2007"
Me.lblAuditType_CY2007.Size = New System.Drawing.Size(58, 13)
Me.lblAuditType_CY2007.TabIndex = 4
Me.lblAuditType_CY2007.Text = "Audit Type"
'
'btnSaveFeeAudit_CY2007
'
Me.btnSaveFeeAudit_CY2007.AutoSize = true
Me.btnSaveFeeAudit_CY2007.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveFeeAudit_CY2007.Location = New System.Drawing.Point(3, 3)
Me.btnSaveFeeAudit_CY2007.Name = "btnSaveFeeAudit_CY2007"
Me.btnSaveFeeAudit_CY2007.Size = New System.Drawing.Size(42, 23)
Me.btnSaveFeeAudit_CY2007.TabIndex = 389
Me.btnSaveFeeAudit_CY2007.Text = "Save"
Me.btnSaveFeeAudit_CY2007.UseVisualStyleBackColor = true
'
'TP_Tracking_CY2006
'
Me.TP_Tracking_CY2006.Controls.Add(Me.Panel65)
Me.TP_Tracking_CY2006.Controls.Add(Me.Panel53)
Me.TP_Tracking_CY2006.Location = New System.Drawing.Point(4, 22)
Me.TP_Tracking_CY2006.Name = "TP_Tracking_CY2006"
Me.TP_Tracking_CY2006.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Tracking_CY2006.Size = New System.Drawing.Size(770, 603)
Me.TP_Tracking_CY2006.TabIndex = 2
Me.TP_Tracking_CY2006.Text = "CY2006"
Me.TP_Tracking_CY2006.UseVisualStyleBackColor = true
'
'Panel65
'
Me.Panel65.AutoScroll = true
Me.Panel65.Controls.Add(Me.Label39)
Me.Panel65.Controls.Add(Me.Label40)
Me.Panel65.Controls.Add(Me.Panel66)
Me.Panel65.Controls.Add(Me.Panel67)
Me.Panel65.Controls.Add(Me.txtComments_CY2006)
Me.Panel65.Controls.Add(Me.DTPCloseOut_CY2006)
Me.Panel65.Controls.Add(Me.lblAmountPaid_CY2006)
Me.Panel65.Controls.Add(Me.DTPFeesPaid_CY2006)
Me.Panel65.Controls.Add(Me.DTPAOSent_CY2006)
Me.Panel65.Controls.Add(Me.DTPCOSent_CY2006)
Me.Panel65.Controls.Add(Me.DTPNOVSent_CY2006)
Me.Panel65.Controls.Add(Me.Panel68)
Me.Panel65.Controls.Add(Me.Panel69)
Me.Panel65.Controls.Add(Me.Panel70)
Me.Panel65.Controls.Add(Me.DTPLetterRemailed_CY2006)
Me.Panel65.Controls.Add(Me.Panel71)
Me.Panel65.Controls.Add(Me.DTPLetterReturned_CY2006)
Me.Panel65.Controls.Add(Me.DTPInitialLetter_2006)
Me.Panel65.Controls.Add(Me.Label85)
Me.Panel65.Controls.Add(Me.Label86)
Me.Panel65.Controls.Add(Me.Label87)
Me.Panel65.Controls.Add(Me.Label88)
Me.Panel65.Controls.Add(Me.Label89)
Me.Panel65.Controls.Add(Me.Label90)
Me.Panel65.Controls.Add(Me.Label91)
Me.Panel65.Controls.Add(Me.Label92)
Me.Panel65.Controls.Add(Me.Label93)
Me.Panel65.Controls.Add(Me.Label94)
Me.Panel65.Controls.Add(Me.Label95)
Me.Panel65.Controls.Add(Me.Label96)
Me.Panel65.Controls.Add(Me.Label97)
Me.Panel65.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel65.Location = New System.Drawing.Point(3, 35)
Me.Panel65.Name = "Panel65"
Me.Panel65.Size = New System.Drawing.Size(764, 565)
Me.Panel65.TabIndex = 4
'
'Label39
'
Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label39.Location = New System.Drawing.Point(0, 269)
Me.Label39.Name = "Label39"
Me.Label39.Size = New System.Drawing.Size(764, 2)
Me.Label39.TabIndex = 395
'
'Label40
'
Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label40.Location = New System.Drawing.Point(0, 183)
Me.Label40.Name = "Label40"
Me.Label40.Size = New System.Drawing.Size(764, 2)
Me.Label40.TabIndex = 394
'
'Panel66
'
Me.Panel66.Controls.Add(Me.btnManagerSignOff_CY2006)
Me.Panel66.Controls.Add(Me.lblSignOffDat_06)
Me.Panel66.Controls.Add(Me.lblManagerSignOff_06)
Me.Panel66.Location = New System.Drawing.Point(465, 197)
Me.Panel66.Name = "Panel66"
Me.Panel66.Size = New System.Drawing.Size(246, 61)
Me.Panel66.TabIndex = 391
'
'btnManagerSignOff_CY2006
'
Me.btnManagerSignOff_CY2006.AutoSize = true
Me.btnManagerSignOff_CY2006.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnManagerSignOff_CY2006.Location = New System.Drawing.Point(6, 26)
Me.btnManagerSignOff_CY2006.Name = "btnManagerSignOff_CY2006"
Me.btnManagerSignOff_CY2006.Size = New System.Drawing.Size(55, 23)
Me.btnManagerSignOff_CY2006.TabIndex = 396
Me.btnManagerSignOff_CY2006.Text = "Sign-Off"
Me.btnManagerSignOff_CY2006.UseVisualStyleBackColor = true
'
'lblSignOffDat_06
'
Me.lblSignOffDat_06.AutoSize = true
Me.lblSignOffDat_06.Location = New System.Drawing.Point(67, 31)
Me.lblSignOffDat_06.Name = "lblSignOffDat_06"
Me.lblSignOffDat_06.Size = New System.Drawing.Size(39, 13)
Me.lblSignOffDat_06.TabIndex = 2
Me.lblSignOffDat_06.Text = "Date - "
'
'lblManagerSignOff_06
'
Me.lblManagerSignOff_06.AutoSize = true
Me.lblManagerSignOff_06.Location = New System.Drawing.Point(3, 10)
Me.lblManagerSignOff_06.Name = "lblManagerSignOff_06"
Me.lblManagerSignOff_06.Size = New System.Drawing.Size(88, 13)
Me.lblManagerSignOff_06.TabIndex = 1
Me.lblManagerSignOff_06.Text = "Manager Sign-off"
'
'Panel67
'
Me.Panel67.Controls.Add(Me.lblLastModified_06)
Me.Panel67.Controls.Add(Me.lblStaffAssigned_06)
Me.Panel67.Location = New System.Drawing.Point(465, 111)
Me.Panel67.Name = "Panel67"
Me.Panel67.Size = New System.Drawing.Size(246, 61)
Me.Panel67.TabIndex = 390
'
'lblLastModified_06
'
Me.lblLastModified_06.AutoSize = true
Me.lblLastModified_06.Location = New System.Drawing.Point(3, 32)
Me.lblLastModified_06.Name = "lblLastModified_06"
Me.lblLastModified_06.Size = New System.Drawing.Size(70, 13)
Me.lblLastModified_06.TabIndex = 2
Me.lblLastModified_06.Text = "Last Modified"
'
'lblStaffAssigned_06
'
Me.lblStaffAssigned_06.AutoSize = true
Me.lblStaffAssigned_06.Location = New System.Drawing.Point(3, 10)
Me.lblStaffAssigned_06.Name = "lblStaffAssigned_06"
Me.lblStaffAssigned_06.Size = New System.Drawing.Size(95, 13)
Me.lblStaffAssigned_06.TabIndex = 1
Me.lblStaffAssigned_06.Text = "Staff Last Modified"
'
'txtComments_CY2006
'
Me.txtComments_CY2006.Location = New System.Drawing.Point(62, 348)
Me.txtComments_CY2006.Multiline = true
Me.txtComments_CY2006.Name = "txtComments_CY2006"
Me.txtComments_CY2006.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtComments_CY2006.Size = New System.Drawing.Size(383, 64)
Me.txtComments_CY2006.TabIndex = 388
'
'DTPCloseOut_CY2006
'
Me.DTPCloseOut_CY2006.Checked = false
Me.DTPCloseOut_CY2006.CustomFormat = "dd-MMM-yyyy"
Me.DTPCloseOut_CY2006.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCloseOut_CY2006.Location = New System.Drawing.Point(116, 314)
Me.DTPCloseOut_CY2006.Name = "DTPCloseOut_CY2006"
Me.DTPCloseOut_CY2006.ShowCheckBox = true
Me.DTPCloseOut_CY2006.Size = New System.Drawing.Size(102, 20)
Me.DTPCloseOut_CY2006.TabIndex = 387
Me.DTPCloseOut_CY2006.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'lblAmountPaid_CY2006
'
Me.lblAmountPaid_CY2006.AutoSize = true
Me.lblAmountPaid_CY2006.Location = New System.Drawing.Point(202, 279)
Me.lblAmountPaid_CY2006.Name = "lblAmountPaid_CY2006"
Me.lblAmountPaid_CY2006.Size = New System.Drawing.Size(63, 13)
Me.lblAmountPaid_CY2006.TabIndex = 386
Me.lblAmountPaid_CY2006.Text = "Fees Paid: -"
'
'DTPFeesPaid_CY2006
'
Me.DTPFeesPaid_CY2006.Checked = false
Me.DTPFeesPaid_CY2006.CustomFormat = "dd-MMM-yyyy"
Me.DTPFeesPaid_CY2006.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPFeesPaid_CY2006.Location = New System.Drawing.Point(93, 275)
Me.DTPFeesPaid_CY2006.Name = "DTPFeesPaid_CY2006"
Me.DTPFeesPaid_CY2006.ShowCheckBox = true
Me.DTPFeesPaid_CY2006.Size = New System.Drawing.Size(102, 20)
Me.DTPFeesPaid_CY2006.TabIndex = 384
Me.DTPFeesPaid_CY2006.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPAOSent_CY2006
'
Me.DTPAOSent_CY2006.Checked = false
Me.DTPAOSent_CY2006.CustomFormat = "dd-MMM-yyyy"
Me.DTPAOSent_CY2006.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPAOSent_CY2006.Location = New System.Drawing.Point(93, 245)
Me.DTPAOSent_CY2006.Name = "DTPAOSent_CY2006"
Me.DTPAOSent_CY2006.ShowCheckBox = true
Me.DTPAOSent_CY2006.Size = New System.Drawing.Size(102, 20)
Me.DTPAOSent_CY2006.TabIndex = 383
Me.DTPAOSent_CY2006.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPCOSent_CY2006
'
Me.DTPCOSent_CY2006.Checked = false
Me.DTPCOSent_CY2006.CustomFormat = "dd-MMM-yyyy"
Me.DTPCOSent_CY2006.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCOSent_CY2006.Location = New System.Drawing.Point(93, 219)
Me.DTPCOSent_CY2006.Name = "DTPCOSent_CY2006"
Me.DTPCOSent_CY2006.ShowCheckBox = true
Me.DTPCOSent_CY2006.Size = New System.Drawing.Size(102, 20)
Me.DTPCOSent_CY2006.TabIndex = 382
Me.DTPCOSent_CY2006.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPNOVSent_CY2006
'
Me.DTPNOVSent_CY2006.Checked = false
Me.DTPNOVSent_CY2006.CustomFormat = "dd-MMM-yyyy"
Me.DTPNOVSent_CY2006.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPNOVSent_CY2006.Location = New System.Drawing.Point(93, 193)
Me.DTPNOVSent_CY2006.Name = "DTPNOVSent_CY2006"
Me.DTPNOVSent_CY2006.ShowCheckBox = true
Me.DTPNOVSent_CY2006.Size = New System.Drawing.Size(102, 20)
Me.DTPNOVSent_CY2006.TabIndex = 381
Me.DTPNOVSent_CY2006.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel68
'
Me.Panel68.AutoSize = true
Me.Panel68.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel68.Controls.Add(Me.rdbUnabletoContactNo_CY2006)
Me.Panel68.Controls.Add(Me.rdbUnabletoContactYes_CY2006)
Me.Panel68.Location = New System.Drawing.Point(276, 154)
Me.Panel68.Name = "Panel68"
Me.Panel68.Size = New System.Drawing.Size(94, 23)
Me.Panel68.TabIndex = 380
'
'rdbUnabletoContactNo_CY2006
'
Me.rdbUnabletoContactNo_CY2006.AutoSize = true
Me.rdbUnabletoContactNo_CY2006.Location = New System.Drawing.Point(50, 3)
Me.rdbUnabletoContactNo_CY2006.Name = "rdbUnabletoContactNo_CY2006"
Me.rdbUnabletoContactNo_CY2006.Size = New System.Drawing.Size(41, 17)
Me.rdbUnabletoContactNo_CY2006.TabIndex = 1
Me.rdbUnabletoContactNo_CY2006.TabStop = true
Me.rdbUnabletoContactNo_CY2006.Text = "NO"
Me.rdbUnabletoContactNo_CY2006.UseVisualStyleBackColor = true
'
'rdbUnabletoContactYes_CY2006
'
Me.rdbUnabletoContactYes_CY2006.AutoSize = true
Me.rdbUnabletoContactYes_CY2006.Location = New System.Drawing.Point(3, 3)
Me.rdbUnabletoContactYes_CY2006.Name = "rdbUnabletoContactYes_CY2006"
Me.rdbUnabletoContactYes_CY2006.Size = New System.Drawing.Size(46, 17)
Me.rdbUnabletoContactYes_CY2006.TabIndex = 0
Me.rdbUnabletoContactYes_CY2006.TabStop = true
Me.rdbUnabletoContactYes_CY2006.Text = "YES"
Me.rdbUnabletoContactYes_CY2006.UseVisualStyleBackColor = true
'
'Panel69
'
Me.Panel69.AutoSize = true
Me.Panel69.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel69.Controls.Add(Me.rdbBankruptcyNo_CY2006)
Me.Panel69.Controls.Add(Me.rdbBankruptcyYes_CY2006)
Me.Panel69.Location = New System.Drawing.Point(84, 128)
Me.Panel69.Name = "Panel69"
Me.Panel69.Size = New System.Drawing.Size(94, 23)
Me.Panel69.TabIndex = 379
'
'rdbBankruptcyNo_CY2006
'
Me.rdbBankruptcyNo_CY2006.AutoSize = true
Me.rdbBankruptcyNo_CY2006.Location = New System.Drawing.Point(50, 3)
Me.rdbBankruptcyNo_CY2006.Name = "rdbBankruptcyNo_CY2006"
Me.rdbBankruptcyNo_CY2006.Size = New System.Drawing.Size(41, 17)
Me.rdbBankruptcyNo_CY2006.TabIndex = 1
Me.rdbBankruptcyNo_CY2006.TabStop = true
Me.rdbBankruptcyNo_CY2006.Text = "NO"
Me.rdbBankruptcyNo_CY2006.UseVisualStyleBackColor = true
'
'rdbBankruptcyYes_CY2006
'
Me.rdbBankruptcyYes_CY2006.AutoSize = true
Me.rdbBankruptcyYes_CY2006.Location = New System.Drawing.Point(3, 3)
Me.rdbBankruptcyYes_CY2006.Name = "rdbBankruptcyYes_CY2006"
Me.rdbBankruptcyYes_CY2006.Size = New System.Drawing.Size(46, 17)
Me.rdbBankruptcyYes_CY2006.TabIndex = 0
Me.rdbBankruptcyYes_CY2006.TabStop = true
Me.rdbBankruptcyYes_CY2006.Text = "YES"
Me.rdbBankruptcyYes_CY2006.UseVisualStyleBackColor = true
'
'Panel70
'
Me.Panel70.AutoSize = true
Me.Panel70.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel70.Controls.Add(Me.rdbDataCorrectNo_CY2006)
Me.Panel70.Controls.Add(Me.rdbDataCorrectYes_CY2006)
Me.Panel70.Location = New System.Drawing.Point(214, 92)
Me.Panel70.Name = "Panel70"
Me.Panel70.Size = New System.Drawing.Size(94, 23)
Me.Panel70.TabIndex = 378
'
'rdbDataCorrectNo_CY2006
'
Me.rdbDataCorrectNo_CY2006.AutoSize = true
Me.rdbDataCorrectNo_CY2006.Location = New System.Drawing.Point(50, 3)
Me.rdbDataCorrectNo_CY2006.Name = "rdbDataCorrectNo_CY2006"
Me.rdbDataCorrectNo_CY2006.Size = New System.Drawing.Size(41, 17)
Me.rdbDataCorrectNo_CY2006.TabIndex = 1
Me.rdbDataCorrectNo_CY2006.TabStop = true
Me.rdbDataCorrectNo_CY2006.Text = "NO"
Me.rdbDataCorrectNo_CY2006.UseVisualStyleBackColor = true
'
'rdbDataCorrectYes_CY2006
'
Me.rdbDataCorrectYes_CY2006.AutoSize = true
Me.rdbDataCorrectYes_CY2006.Location = New System.Drawing.Point(3, 3)
Me.rdbDataCorrectYes_CY2006.Name = "rdbDataCorrectYes_CY2006"
Me.rdbDataCorrectYes_CY2006.Size = New System.Drawing.Size(46, 17)
Me.rdbDataCorrectYes_CY2006.TabIndex = 0
Me.rdbDataCorrectYes_CY2006.TabStop = true
Me.rdbDataCorrectYes_CY2006.Text = "YES"
Me.rdbDataCorrectYes_CY2006.UseVisualStyleBackColor = true
'
'DTPLetterRemailed_CY2006
'
Me.DTPLetterRemailed_CY2006.Checked = false
Me.DTPLetterRemailed_CY2006.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterRemailed_CY2006.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterRemailed_CY2006.Location = New System.Drawing.Point(465, 64)
Me.DTPLetterRemailed_CY2006.Name = "DTPLetterRemailed_CY2006"
Me.DTPLetterRemailed_CY2006.ShowCheckBox = true
Me.DTPLetterRemailed_CY2006.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterRemailed_CY2006.TabIndex = 377
Me.DTPLetterRemailed_CY2006.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel71
'
Me.Panel71.AutoSize = true
Me.Panel71.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel71.Controls.Add(Me.rdbAddressUnknownNo_CY2006)
Me.Panel71.Controls.Add(Me.rdbAddressUnknownYes_CY2006)
Me.Panel71.Location = New System.Drawing.Point(214, 63)
Me.Panel71.Name = "Panel71"
Me.Panel71.Size = New System.Drawing.Size(94, 23)
Me.Panel71.TabIndex = 376
'
'rdbAddressUnknownNo_CY2006
'
Me.rdbAddressUnknownNo_CY2006.AutoSize = true
Me.rdbAddressUnknownNo_CY2006.Location = New System.Drawing.Point(50, 3)
Me.rdbAddressUnknownNo_CY2006.Name = "rdbAddressUnknownNo_CY2006"
Me.rdbAddressUnknownNo_CY2006.Size = New System.Drawing.Size(41, 17)
Me.rdbAddressUnknownNo_CY2006.TabIndex = 1
Me.rdbAddressUnknownNo_CY2006.TabStop = true
Me.rdbAddressUnknownNo_CY2006.Text = "NO"
Me.rdbAddressUnknownNo_CY2006.UseVisualStyleBackColor = true
'
'rdbAddressUnknownYes_CY2006
'
Me.rdbAddressUnknownYes_CY2006.AutoSize = true
Me.rdbAddressUnknownYes_CY2006.Location = New System.Drawing.Point(3, 3)
Me.rdbAddressUnknownYes_CY2006.Name = "rdbAddressUnknownYes_CY2006"
Me.rdbAddressUnknownYes_CY2006.Size = New System.Drawing.Size(46, 17)
Me.rdbAddressUnknownYes_CY2006.TabIndex = 0
Me.rdbAddressUnknownYes_CY2006.TabStop = true
Me.rdbAddressUnknownYes_CY2006.Text = "YES"
Me.rdbAddressUnknownYes_CY2006.UseVisualStyleBackColor = true
'
'DTPLetterReturned_CY2006
'
Me.DTPLetterReturned_CY2006.Checked = false
Me.DTPLetterReturned_CY2006.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterReturned_CY2006.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterReturned_CY2006.Location = New System.Drawing.Point(193, 33)
Me.DTPLetterReturned_CY2006.Name = "DTPLetterReturned_CY2006"
Me.DTPLetterReturned_CY2006.ShowCheckBox = true
Me.DTPLetterReturned_CY2006.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterReturned_CY2006.TabIndex = 375
Me.DTPLetterReturned_CY2006.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPInitialLetter_2006
'
Me.DTPInitialLetter_2006.Checked = false
Me.DTPInitialLetter_2006.CustomFormat = "dd-MMM-yyyy"
Me.DTPInitialLetter_2006.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPInitialLetter_2006.Location = New System.Drawing.Point(98, 7)
Me.DTPInitialLetter_2006.Name = "DTPInitialLetter_2006"
Me.DTPInitialLetter_2006.ShowCheckBox = true
Me.DTPInitialLetter_2006.Size = New System.Drawing.Size(102, 20)
Me.DTPInitialLetter_2006.TabIndex = 374
Me.DTPInitialLetter_2006.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Label85
'
Me.Label85.AutoSize = true
Me.Label85.Location = New System.Drawing.Point(4, 348)
Me.Label85.Name = "Label85"
Me.Label85.Size = New System.Drawing.Size(56, 13)
Me.Label85.TabIndex = 12
Me.Label85.Text = "Comments"
'
'Label86
'
Me.Label86.AutoSize = true
Me.Label86.Location = New System.Drawing.Point(4, 318)
Me.Label86.Name = "Label86"
Me.Label86.Size = New System.Drawing.Size(101, 13)
Me.Label86.TabIndex = 11
Me.Label86.Text = "Close Out Fee Audit"
'
'Label87
'
Me.Label87.AutoSize = true
Me.Label87.Location = New System.Drawing.Point(4, 223)
Me.Label87.Name = "Label87"
Me.Label87.Size = New System.Drawing.Size(77, 13)
Me.Label87.TabIndex = 10
Me.Label87.Text = "CO Letter Sent"
'
'Label88
'
Me.Label88.AutoSize = true
Me.Label88.Location = New System.Drawing.Point(4, 279)
Me.Label88.Name = "Label88"
Me.Label88.Size = New System.Drawing.Size(87, 13)
Me.Label88.TabIndex = 9
Me.Label88.Text = "Facilty Paid Fees"
'
'Label89
'
Me.Label89.AutoSize = true
Me.Label89.Location = New System.Drawing.Point(4, 249)
Me.Label89.Name = "Label89"
Me.Label89.Size = New System.Drawing.Size(77, 13)
Me.Label89.TabIndex = 8
Me.Label89.Text = "AO Letter Sent"
'
'Label90
'
Me.Label90.AutoSize = true
Me.Label90.Location = New System.Drawing.Point(4, 133)
Me.Label90.Name = "Label90"
Me.Label90.Size = New System.Drawing.Size(61, 13)
Me.Label90.TabIndex = 7
Me.Label90.Text = "Bankruptcy"
'
'Label91
'
Me.Label91.AutoSize = true
Me.Label91.Location = New System.Drawing.Point(4, 159)
Me.Label91.Name = "Label91"
Me.Label91.Size = New System.Drawing.Size(252, 13)
Me.Label91.TabIndex = 6
Me.Label91.Text = "Unable to Contact Facility Or Facility Representative"
'
'Label92
'
Me.Label92.AutoSize = true
Me.Label92.Location = New System.Drawing.Point(4, 197)
Me.Label92.Name = "Label92"
Me.Label92.Size = New System.Drawing.Size(85, 13)
Me.Label92.TabIndex = 5
Me.Label92.Text = "NOV Letter Sent"
'
'Label93
'
Me.Label93.AutoSize = true
Me.Label93.Location = New System.Drawing.Point(4, 37)
Me.Label93.Name = "Label93"
Me.Label93.Size = New System.Drawing.Size(183, 13)
Me.Label93.TabIndex = 4
Me.Label93.Text = "Letter Returned/Response Received"
'
'Label94
'
Me.Label94.AutoSize = true
Me.Label94.Location = New System.Drawing.Point(83, 68)
Me.Label94.Name = "Label94"
Me.Label94.Size = New System.Drawing.Size(125, 13)
Me.Label94.TabIndex = 3
Me.Label94.Text = "Was Address Unknown?"
'
'Label95
'
Me.Label95.AutoSize = true
Me.Label95.Location = New System.Drawing.Point(342, 68)
Me.Label95.Name = "Label95"
Me.Label95.Size = New System.Drawing.Size(112, 13)
Me.Label95.TabIndex = 2
Me.Label95.Text = "Initial Letter Re-Mailed"
'
'Label96
'
Me.Label96.AutoSize = true
Me.Label96.Location = New System.Drawing.Point(4, 97)
Me.Label96.Name = "Label96"
Me.Label96.Size = New System.Drawing.Size(196, 13)
Me.Label96.TabIndex = 1
Me.Label96.Text = "Facility Responded with Data Correction"
'
'Label97
'
Me.Label97.AutoSize = true
Me.Label97.Location = New System.Drawing.Point(4, 11)
Me.Label97.Name = "Label97"
Me.Label97.Size = New System.Drawing.Size(95, 13)
Me.Label97.TabIndex = 0
Me.Label97.Text = "Initial Letter Mailed"
'
'Panel53
'
Me.Panel53.Controls.Add(Me.lblAuditType_CY2006)
Me.Panel53.Controls.Add(Me.btnSaveFeeAudit_CY2006)
Me.Panel53.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel53.Location = New System.Drawing.Point(3, 3)
Me.Panel53.Name = "Panel53"
Me.Panel53.Size = New System.Drawing.Size(764, 32)
Me.Panel53.TabIndex = 3
'
'lblAuditType_CY2006
'
Me.lblAuditType_CY2006.AutoSize = true
Me.lblAuditType_CY2006.Location = New System.Drawing.Point(49, 8)
Me.lblAuditType_CY2006.Name = "lblAuditType_CY2006"
Me.lblAuditType_CY2006.Size = New System.Drawing.Size(58, 13)
Me.lblAuditType_CY2006.TabIndex = 4
Me.lblAuditType_CY2006.Text = "Audit Type"
'
'btnSaveFeeAudit_CY2006
'
Me.btnSaveFeeAudit_CY2006.AutoSize = true
Me.btnSaveFeeAudit_CY2006.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveFeeAudit_CY2006.Location = New System.Drawing.Point(3, 3)
Me.btnSaveFeeAudit_CY2006.Name = "btnSaveFeeAudit_CY2006"
Me.btnSaveFeeAudit_CY2006.Size = New System.Drawing.Size(42, 23)
Me.btnSaveFeeAudit_CY2006.TabIndex = 389
Me.btnSaveFeeAudit_CY2006.Text = "Save"
Me.btnSaveFeeAudit_CY2006.UseVisualStyleBackColor = true
'
'TP_Tracking_CY2005
'
Me.TP_Tracking_CY2005.Controls.Add(Me.Panel72)
Me.TP_Tracking_CY2005.Controls.Add(Me.Panel54)
Me.TP_Tracking_CY2005.Location = New System.Drawing.Point(4, 22)
Me.TP_Tracking_CY2005.Name = "TP_Tracking_CY2005"
Me.TP_Tracking_CY2005.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Tracking_CY2005.Size = New System.Drawing.Size(770, 603)
Me.TP_Tracking_CY2005.TabIndex = 4
Me.TP_Tracking_CY2005.Text = "CY2005"
Me.TP_Tracking_CY2005.UseVisualStyleBackColor = true
'
'Panel72
'
Me.Panel72.AutoScroll = true
Me.Panel72.Controls.Add(Me.Label41)
Me.Panel72.Controls.Add(Me.Label42)
Me.Panel72.Controls.Add(Me.Panel73)
Me.Panel72.Controls.Add(Me.Panel74)
Me.Panel72.Controls.Add(Me.txtComments_CY2005)
Me.Panel72.Controls.Add(Me.DTPCloseOut_CY2005)
Me.Panel72.Controls.Add(Me.lblAmountPaid_CY2005)
Me.Panel72.Controls.Add(Me.DTPFeesPaid_CY2005)
Me.Panel72.Controls.Add(Me.DTPAOSent_CY2005)
Me.Panel72.Controls.Add(Me.DTPCOSent_CY2005)
Me.Panel72.Controls.Add(Me.DTPNOVSent_CY2005)
Me.Panel72.Controls.Add(Me.Panel75)
Me.Panel72.Controls.Add(Me.Panel76)
Me.Panel72.Controls.Add(Me.Panel77)
Me.Panel72.Controls.Add(Me.DTPLetterRemailed_CY2005)
Me.Panel72.Controls.Add(Me.Panel78)
Me.Panel72.Controls.Add(Me.DTPLetterReturned_CY2005)
Me.Panel72.Controls.Add(Me.DTPInitialLetter_2005)
Me.Panel72.Controls.Add(Me.Label103)
Me.Panel72.Controls.Add(Me.Label104)
Me.Panel72.Controls.Add(Me.Label105)
Me.Panel72.Controls.Add(Me.Label106)
Me.Panel72.Controls.Add(Me.Label107)
Me.Panel72.Controls.Add(Me.Label108)
Me.Panel72.Controls.Add(Me.Label109)
Me.Panel72.Controls.Add(Me.Label110)
Me.Panel72.Controls.Add(Me.Label111)
Me.Panel72.Controls.Add(Me.Label112)
Me.Panel72.Controls.Add(Me.Label113)
Me.Panel72.Controls.Add(Me.Label114)
Me.Panel72.Controls.Add(Me.Label115)
Me.Panel72.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel72.Location = New System.Drawing.Point(3, 35)
Me.Panel72.Name = "Panel72"
Me.Panel72.Size = New System.Drawing.Size(764, 565)
Me.Panel72.TabIndex = 4
'
'Label41
'
Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label41.Location = New System.Drawing.Point(0, 269)
Me.Label41.Name = "Label41"
Me.Label41.Size = New System.Drawing.Size(764, 2)
Me.Label41.TabIndex = 395
'
'Label42
'
Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label42.Location = New System.Drawing.Point(0, 183)
Me.Label42.Name = "Label42"
Me.Label42.Size = New System.Drawing.Size(764, 2)
Me.Label42.TabIndex = 394
'
'Panel73
'
Me.Panel73.Controls.Add(Me.btnManagerSignOff_CY2005)
Me.Panel73.Controls.Add(Me.lblSignOffDat_05)
Me.Panel73.Controls.Add(Me.lblManagerSignOff_05)
Me.Panel73.Location = New System.Drawing.Point(465, 197)
Me.Panel73.Name = "Panel73"
Me.Panel73.Size = New System.Drawing.Size(246, 61)
Me.Panel73.TabIndex = 391
'
'btnManagerSignOff_CY2005
'
Me.btnManagerSignOff_CY2005.AutoSize = true
Me.btnManagerSignOff_CY2005.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnManagerSignOff_CY2005.Location = New System.Drawing.Point(6, 26)
Me.btnManagerSignOff_CY2005.Name = "btnManagerSignOff_CY2005"
Me.btnManagerSignOff_CY2005.Size = New System.Drawing.Size(55, 23)
Me.btnManagerSignOff_CY2005.TabIndex = 397
Me.btnManagerSignOff_CY2005.Text = "Sign-Off"
Me.btnManagerSignOff_CY2005.UseVisualStyleBackColor = true
'
'lblSignOffDat_05
'
Me.lblSignOffDat_05.AutoSize = true
Me.lblSignOffDat_05.Location = New System.Drawing.Point(67, 31)
Me.lblSignOffDat_05.Name = "lblSignOffDat_05"
Me.lblSignOffDat_05.Size = New System.Drawing.Size(39, 13)
Me.lblSignOffDat_05.TabIndex = 2
Me.lblSignOffDat_05.Text = "Date - "
'
'lblManagerSignOff_05
'
Me.lblManagerSignOff_05.AutoSize = true
Me.lblManagerSignOff_05.Location = New System.Drawing.Point(3, 10)
Me.lblManagerSignOff_05.Name = "lblManagerSignOff_05"
Me.lblManagerSignOff_05.Size = New System.Drawing.Size(88, 13)
Me.lblManagerSignOff_05.TabIndex = 1
Me.lblManagerSignOff_05.Text = "Manager Sign-off"
'
'Panel74
'
Me.Panel74.Controls.Add(Me.lblLastModified_05)
Me.Panel74.Controls.Add(Me.lblStaffAssigned_05)
Me.Panel74.Location = New System.Drawing.Point(465, 111)
Me.Panel74.Name = "Panel74"
Me.Panel74.Size = New System.Drawing.Size(246, 61)
Me.Panel74.TabIndex = 390
'
'lblLastModified_05
'
Me.lblLastModified_05.AutoSize = true
Me.lblLastModified_05.Location = New System.Drawing.Point(3, 32)
Me.lblLastModified_05.Name = "lblLastModified_05"
Me.lblLastModified_05.Size = New System.Drawing.Size(70, 13)
Me.lblLastModified_05.TabIndex = 2
Me.lblLastModified_05.Text = "Last Modified"
'
'lblStaffAssigned_05
'
Me.lblStaffAssigned_05.AutoSize = true
Me.lblStaffAssigned_05.Location = New System.Drawing.Point(3, 10)
Me.lblStaffAssigned_05.Name = "lblStaffAssigned_05"
Me.lblStaffAssigned_05.Size = New System.Drawing.Size(95, 13)
Me.lblStaffAssigned_05.TabIndex = 1
Me.lblStaffAssigned_05.Text = "Staff Last Modified"
'
'txtComments_CY2005
'
Me.txtComments_CY2005.Location = New System.Drawing.Point(62, 348)
Me.txtComments_CY2005.Multiline = true
Me.txtComments_CY2005.Name = "txtComments_CY2005"
Me.txtComments_CY2005.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtComments_CY2005.Size = New System.Drawing.Size(383, 64)
Me.txtComments_CY2005.TabIndex = 388
'
'DTPCloseOut_CY2005
'
Me.DTPCloseOut_CY2005.Checked = false
Me.DTPCloseOut_CY2005.CustomFormat = "dd-MMM-yyyy"
Me.DTPCloseOut_CY2005.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCloseOut_CY2005.Location = New System.Drawing.Point(116, 314)
Me.DTPCloseOut_CY2005.Name = "DTPCloseOut_CY2005"
Me.DTPCloseOut_CY2005.ShowCheckBox = true
Me.DTPCloseOut_CY2005.Size = New System.Drawing.Size(102, 20)
Me.DTPCloseOut_CY2005.TabIndex = 387
Me.DTPCloseOut_CY2005.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'lblAmountPaid_CY2005
'
Me.lblAmountPaid_CY2005.AutoSize = true
Me.lblAmountPaid_CY2005.Location = New System.Drawing.Point(202, 279)
Me.lblAmountPaid_CY2005.Name = "lblAmountPaid_CY2005"
Me.lblAmountPaid_CY2005.Size = New System.Drawing.Size(63, 13)
Me.lblAmountPaid_CY2005.TabIndex = 386
Me.lblAmountPaid_CY2005.Text = "Fees Paid: -"
'
'DTPFeesPaid_CY2005
'
Me.DTPFeesPaid_CY2005.Checked = false
Me.DTPFeesPaid_CY2005.CustomFormat = "dd-MMM-yyyy"
Me.DTPFeesPaid_CY2005.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPFeesPaid_CY2005.Location = New System.Drawing.Point(93, 275)
Me.DTPFeesPaid_CY2005.Name = "DTPFeesPaid_CY2005"
Me.DTPFeesPaid_CY2005.ShowCheckBox = true
Me.DTPFeesPaid_CY2005.Size = New System.Drawing.Size(102, 20)
Me.DTPFeesPaid_CY2005.TabIndex = 384
Me.DTPFeesPaid_CY2005.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPAOSent_CY2005
'
Me.DTPAOSent_CY2005.Checked = false
Me.DTPAOSent_CY2005.CustomFormat = "dd-MMM-yyyy"
Me.DTPAOSent_CY2005.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPAOSent_CY2005.Location = New System.Drawing.Point(93, 245)
Me.DTPAOSent_CY2005.Name = "DTPAOSent_CY2005"
Me.DTPAOSent_CY2005.ShowCheckBox = true
Me.DTPAOSent_CY2005.Size = New System.Drawing.Size(102, 20)
Me.DTPAOSent_CY2005.TabIndex = 383
Me.DTPAOSent_CY2005.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPCOSent_CY2005
'
Me.DTPCOSent_CY2005.Checked = false
Me.DTPCOSent_CY2005.CustomFormat = "dd-MMM-yyyy"
Me.DTPCOSent_CY2005.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCOSent_CY2005.Location = New System.Drawing.Point(93, 219)
Me.DTPCOSent_CY2005.Name = "DTPCOSent_CY2005"
Me.DTPCOSent_CY2005.ShowCheckBox = true
Me.DTPCOSent_CY2005.Size = New System.Drawing.Size(102, 20)
Me.DTPCOSent_CY2005.TabIndex = 382
Me.DTPCOSent_CY2005.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPNOVSent_CY2005
'
Me.DTPNOVSent_CY2005.Checked = false
Me.DTPNOVSent_CY2005.CustomFormat = "dd-MMM-yyyy"
Me.DTPNOVSent_CY2005.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPNOVSent_CY2005.Location = New System.Drawing.Point(93, 193)
Me.DTPNOVSent_CY2005.Name = "DTPNOVSent_CY2005"
Me.DTPNOVSent_CY2005.ShowCheckBox = true
Me.DTPNOVSent_CY2005.Size = New System.Drawing.Size(102, 20)
Me.DTPNOVSent_CY2005.TabIndex = 381
Me.DTPNOVSent_CY2005.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel75
'
Me.Panel75.AutoSize = true
Me.Panel75.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel75.Controls.Add(Me.rdbUnabletoContactNo_CY2005)
Me.Panel75.Controls.Add(Me.rdbUnabletoContactYes_CY2005)
Me.Panel75.Location = New System.Drawing.Point(276, 154)
Me.Panel75.Name = "Panel75"
Me.Panel75.Size = New System.Drawing.Size(94, 23)
Me.Panel75.TabIndex = 380
'
'rdbUnabletoContactNo_CY2005
'
Me.rdbUnabletoContactNo_CY2005.AutoSize = true
Me.rdbUnabletoContactNo_CY2005.Location = New System.Drawing.Point(50, 3)
Me.rdbUnabletoContactNo_CY2005.Name = "rdbUnabletoContactNo_CY2005"
Me.rdbUnabletoContactNo_CY2005.Size = New System.Drawing.Size(41, 17)
Me.rdbUnabletoContactNo_CY2005.TabIndex = 1
Me.rdbUnabletoContactNo_CY2005.TabStop = true
Me.rdbUnabletoContactNo_CY2005.Text = "NO"
Me.rdbUnabletoContactNo_CY2005.UseVisualStyleBackColor = true
'
'rdbUnabletoContactYes_CY2005
'
Me.rdbUnabletoContactYes_CY2005.AutoSize = true
Me.rdbUnabletoContactYes_CY2005.Location = New System.Drawing.Point(3, 3)
Me.rdbUnabletoContactYes_CY2005.Name = "rdbUnabletoContactYes_CY2005"
Me.rdbUnabletoContactYes_CY2005.Size = New System.Drawing.Size(46, 17)
Me.rdbUnabletoContactYes_CY2005.TabIndex = 0
Me.rdbUnabletoContactYes_CY2005.TabStop = true
Me.rdbUnabletoContactYes_CY2005.Text = "YES"
Me.rdbUnabletoContactYes_CY2005.UseVisualStyleBackColor = true
'
'Panel76
'
Me.Panel76.AutoSize = true
Me.Panel76.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel76.Controls.Add(Me.rdbBankruptcyNo_CY2005)
Me.Panel76.Controls.Add(Me.rdbBankruptcyYes_CY2005)
Me.Panel76.Location = New System.Drawing.Point(84, 128)
Me.Panel76.Name = "Panel76"
Me.Panel76.Size = New System.Drawing.Size(94, 23)
Me.Panel76.TabIndex = 379
'
'rdbBankruptcyNo_CY2005
'
Me.rdbBankruptcyNo_CY2005.AutoSize = true
Me.rdbBankruptcyNo_CY2005.Location = New System.Drawing.Point(50, 3)
Me.rdbBankruptcyNo_CY2005.Name = "rdbBankruptcyNo_CY2005"
Me.rdbBankruptcyNo_CY2005.Size = New System.Drawing.Size(41, 17)
Me.rdbBankruptcyNo_CY2005.TabIndex = 1
Me.rdbBankruptcyNo_CY2005.TabStop = true
Me.rdbBankruptcyNo_CY2005.Text = "NO"
Me.rdbBankruptcyNo_CY2005.UseVisualStyleBackColor = true
'
'rdbBankruptcyYes_CY2005
'
Me.rdbBankruptcyYes_CY2005.AutoSize = true
Me.rdbBankruptcyYes_CY2005.Location = New System.Drawing.Point(3, 3)
Me.rdbBankruptcyYes_CY2005.Name = "rdbBankruptcyYes_CY2005"
Me.rdbBankruptcyYes_CY2005.Size = New System.Drawing.Size(46, 17)
Me.rdbBankruptcyYes_CY2005.TabIndex = 0
Me.rdbBankruptcyYes_CY2005.TabStop = true
Me.rdbBankruptcyYes_CY2005.Text = "YES"
Me.rdbBankruptcyYes_CY2005.UseVisualStyleBackColor = true
'
'Panel77
'
Me.Panel77.AutoSize = true
Me.Panel77.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel77.Controls.Add(Me.rdbDataCorrectNo_CY2005)
Me.Panel77.Controls.Add(Me.rdbDataCorrectYes_CY2005)
Me.Panel77.Location = New System.Drawing.Point(214, 92)
Me.Panel77.Name = "Panel77"
Me.Panel77.Size = New System.Drawing.Size(94, 23)
Me.Panel77.TabIndex = 378
'
'rdbDataCorrectNo_CY2005
'
Me.rdbDataCorrectNo_CY2005.AutoSize = true
Me.rdbDataCorrectNo_CY2005.Location = New System.Drawing.Point(50, 3)
Me.rdbDataCorrectNo_CY2005.Name = "rdbDataCorrectNo_CY2005"
Me.rdbDataCorrectNo_CY2005.Size = New System.Drawing.Size(41, 17)
Me.rdbDataCorrectNo_CY2005.TabIndex = 1
Me.rdbDataCorrectNo_CY2005.TabStop = true
Me.rdbDataCorrectNo_CY2005.Text = "NO"
Me.rdbDataCorrectNo_CY2005.UseVisualStyleBackColor = true
'
'rdbDataCorrectYes_CY2005
'
Me.rdbDataCorrectYes_CY2005.AutoSize = true
Me.rdbDataCorrectYes_CY2005.Location = New System.Drawing.Point(3, 3)
Me.rdbDataCorrectYes_CY2005.Name = "rdbDataCorrectYes_CY2005"
Me.rdbDataCorrectYes_CY2005.Size = New System.Drawing.Size(46, 17)
Me.rdbDataCorrectYes_CY2005.TabIndex = 0
Me.rdbDataCorrectYes_CY2005.TabStop = true
Me.rdbDataCorrectYes_CY2005.Text = "YES"
Me.rdbDataCorrectYes_CY2005.UseVisualStyleBackColor = true
'
'DTPLetterRemailed_CY2005
'
Me.DTPLetterRemailed_CY2005.Checked = false
Me.DTPLetterRemailed_CY2005.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterRemailed_CY2005.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterRemailed_CY2005.Location = New System.Drawing.Point(465, 60)
Me.DTPLetterRemailed_CY2005.Name = "DTPLetterRemailed_CY2005"
Me.DTPLetterRemailed_CY2005.ShowCheckBox = true
Me.DTPLetterRemailed_CY2005.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterRemailed_CY2005.TabIndex = 377
Me.DTPLetterRemailed_CY2005.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel78
'
Me.Panel78.AutoSize = true
Me.Panel78.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel78.Controls.Add(Me.rdbAddressUnknownNo_CY2005)
Me.Panel78.Controls.Add(Me.rdbAddressUnknownYes_CY2005)
Me.Panel78.Location = New System.Drawing.Point(214, 59)
Me.Panel78.Name = "Panel78"
Me.Panel78.Size = New System.Drawing.Size(94, 23)
Me.Panel78.TabIndex = 376
'
'rdbAddressUnknownNo_CY2005
'
Me.rdbAddressUnknownNo_CY2005.AutoSize = true
Me.rdbAddressUnknownNo_CY2005.Location = New System.Drawing.Point(50, 3)
Me.rdbAddressUnknownNo_CY2005.Name = "rdbAddressUnknownNo_CY2005"
Me.rdbAddressUnknownNo_CY2005.Size = New System.Drawing.Size(41, 17)
Me.rdbAddressUnknownNo_CY2005.TabIndex = 1
Me.rdbAddressUnknownNo_CY2005.TabStop = true
Me.rdbAddressUnknownNo_CY2005.Text = "NO"
Me.rdbAddressUnknownNo_CY2005.UseVisualStyleBackColor = true
'
'rdbAddressUnknownYes_CY2005
'
Me.rdbAddressUnknownYes_CY2005.AutoSize = true
Me.rdbAddressUnknownYes_CY2005.Location = New System.Drawing.Point(3, 3)
Me.rdbAddressUnknownYes_CY2005.Name = "rdbAddressUnknownYes_CY2005"
Me.rdbAddressUnknownYes_CY2005.Size = New System.Drawing.Size(46, 17)
Me.rdbAddressUnknownYes_CY2005.TabIndex = 0
Me.rdbAddressUnknownYes_CY2005.TabStop = true
Me.rdbAddressUnknownYes_CY2005.Text = "YES"
Me.rdbAddressUnknownYes_CY2005.UseVisualStyleBackColor = true
'
'DTPLetterReturned_CY2005
'
Me.DTPLetterReturned_CY2005.Checked = false
Me.DTPLetterReturned_CY2005.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterReturned_CY2005.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterReturned_CY2005.Location = New System.Drawing.Point(193, 33)
Me.DTPLetterReturned_CY2005.Name = "DTPLetterReturned_CY2005"
Me.DTPLetterReturned_CY2005.ShowCheckBox = true
Me.DTPLetterReturned_CY2005.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterReturned_CY2005.TabIndex = 375
Me.DTPLetterReturned_CY2005.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPInitialLetter_2005
'
Me.DTPInitialLetter_2005.Checked = false
Me.DTPInitialLetter_2005.CustomFormat = "dd-MMM-yyyy"
Me.DTPInitialLetter_2005.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPInitialLetter_2005.Location = New System.Drawing.Point(98, 7)
Me.DTPInitialLetter_2005.Name = "DTPInitialLetter_2005"
Me.DTPInitialLetter_2005.ShowCheckBox = true
Me.DTPInitialLetter_2005.Size = New System.Drawing.Size(102, 20)
Me.DTPInitialLetter_2005.TabIndex = 374
Me.DTPInitialLetter_2005.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Label103
'
Me.Label103.AutoSize = true
Me.Label103.Location = New System.Drawing.Point(4, 348)
Me.Label103.Name = "Label103"
Me.Label103.Size = New System.Drawing.Size(56, 13)
Me.Label103.TabIndex = 12
Me.Label103.Text = "Comments"
'
'Label104
'
Me.Label104.AutoSize = true
Me.Label104.Location = New System.Drawing.Point(4, 318)
Me.Label104.Name = "Label104"
Me.Label104.Size = New System.Drawing.Size(101, 13)
Me.Label104.TabIndex = 11
Me.Label104.Text = "Close Out Fee Audit"
'
'Label105
'
Me.Label105.AutoSize = true
Me.Label105.Location = New System.Drawing.Point(4, 223)
Me.Label105.Name = "Label105"
Me.Label105.Size = New System.Drawing.Size(77, 13)
Me.Label105.TabIndex = 10
Me.Label105.Text = "CO Letter Sent"
'
'Label106
'
Me.Label106.AutoSize = true
Me.Label106.Location = New System.Drawing.Point(4, 279)
Me.Label106.Name = "Label106"
Me.Label106.Size = New System.Drawing.Size(87, 13)
Me.Label106.TabIndex = 9
Me.Label106.Text = "Facilty Paid Fees"
'
'Label107
'
Me.Label107.AutoSize = true
Me.Label107.Location = New System.Drawing.Point(4, 249)
Me.Label107.Name = "Label107"
Me.Label107.Size = New System.Drawing.Size(77, 13)
Me.Label107.TabIndex = 8
Me.Label107.Text = "AO Letter Sent"
'
'Label108
'
Me.Label108.AutoSize = true
Me.Label108.Location = New System.Drawing.Point(4, 133)
Me.Label108.Name = "Label108"
Me.Label108.Size = New System.Drawing.Size(61, 13)
Me.Label108.TabIndex = 7
Me.Label108.Text = "Bankruptcy"
'
'Label109
'
Me.Label109.AutoSize = true
Me.Label109.Location = New System.Drawing.Point(4, 159)
Me.Label109.Name = "Label109"
Me.Label109.Size = New System.Drawing.Size(252, 13)
Me.Label109.TabIndex = 6
Me.Label109.Text = "Unable to Contact Facility Or Facility Representative"
'
'Label110
'
Me.Label110.AutoSize = true
Me.Label110.Location = New System.Drawing.Point(4, 197)
Me.Label110.Name = "Label110"
Me.Label110.Size = New System.Drawing.Size(85, 13)
Me.Label110.TabIndex = 5
Me.Label110.Text = "NOV Letter Sent"
'
'Label111
'
Me.Label111.AutoSize = true
Me.Label111.Location = New System.Drawing.Point(4, 37)
Me.Label111.Name = "Label111"
Me.Label111.Size = New System.Drawing.Size(183, 13)
Me.Label111.TabIndex = 4
Me.Label111.Text = "Letter Returned/Response Received"
'
'Label112
'
Me.Label112.AutoSize = true
Me.Label112.Location = New System.Drawing.Point(83, 64)
Me.Label112.Name = "Label112"
Me.Label112.Size = New System.Drawing.Size(125, 13)
Me.Label112.TabIndex = 3
Me.Label112.Text = "Was Address Unknown?"
'
'Label113
'
Me.Label113.AutoSize = true
Me.Label113.Location = New System.Drawing.Point(342, 64)
Me.Label113.Name = "Label113"
Me.Label113.Size = New System.Drawing.Size(112, 13)
Me.Label113.TabIndex = 2
Me.Label113.Text = "Initial Letter Re-Mailed"
'
'Label114
'
Me.Label114.AutoSize = true
Me.Label114.Location = New System.Drawing.Point(4, 97)
Me.Label114.Name = "Label114"
Me.Label114.Size = New System.Drawing.Size(196, 13)
Me.Label114.TabIndex = 1
Me.Label114.Text = "Facility Responded with Data Correction"
'
'Label115
'
Me.Label115.AutoSize = true
Me.Label115.Location = New System.Drawing.Point(4, 11)
Me.Label115.Name = "Label115"
Me.Label115.Size = New System.Drawing.Size(95, 13)
Me.Label115.TabIndex = 0
Me.Label115.Text = "Initial Letter Mailed"
'
'Panel54
'
Me.Panel54.Controls.Add(Me.lblAuditType_CY2005)
Me.Panel54.Controls.Add(Me.btnSaveFeeAudit_CY2005)
Me.Panel54.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel54.Location = New System.Drawing.Point(3, 3)
Me.Panel54.Name = "Panel54"
Me.Panel54.Size = New System.Drawing.Size(764, 32)
Me.Panel54.TabIndex = 3
'
'lblAuditType_CY2005
'
Me.lblAuditType_CY2005.AutoSize = true
Me.lblAuditType_CY2005.Location = New System.Drawing.Point(49, 8)
Me.lblAuditType_CY2005.Name = "lblAuditType_CY2005"
Me.lblAuditType_CY2005.Size = New System.Drawing.Size(58, 13)
Me.lblAuditType_CY2005.TabIndex = 4
Me.lblAuditType_CY2005.Text = "Audit Type"
'
'btnSaveFeeAudit_CY2005
'
Me.btnSaveFeeAudit_CY2005.AutoSize = true
Me.btnSaveFeeAudit_CY2005.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveFeeAudit_CY2005.Location = New System.Drawing.Point(3, 3)
Me.btnSaveFeeAudit_CY2005.Name = "btnSaveFeeAudit_CY2005"
Me.btnSaveFeeAudit_CY2005.Size = New System.Drawing.Size(42, 23)
Me.btnSaveFeeAudit_CY2005.TabIndex = 389
Me.btnSaveFeeAudit_CY2005.Text = "Save"
Me.btnSaveFeeAudit_CY2005.UseVisualStyleBackColor = true
'
'TP_Tracking_CY2004
'
Me.TP_Tracking_CY2004.Controls.Add(Me.Panel79)
Me.TP_Tracking_CY2004.Controls.Add(Me.Panel55)
Me.TP_Tracking_CY2004.Location = New System.Drawing.Point(4, 22)
Me.TP_Tracking_CY2004.Name = "TP_Tracking_CY2004"
Me.TP_Tracking_CY2004.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Tracking_CY2004.Size = New System.Drawing.Size(770, 603)
Me.TP_Tracking_CY2004.TabIndex = 5
Me.TP_Tracking_CY2004.Text = "CY2004"
Me.TP_Tracking_CY2004.UseVisualStyleBackColor = true
'
'Panel79
'
Me.Panel79.AutoScroll = true
Me.Panel79.Controls.Add(Me.Label43)
Me.Panel79.Controls.Add(Me.Label44)
Me.Panel79.Controls.Add(Me.Panel80)
Me.Panel79.Controls.Add(Me.Panel81)
Me.Panel79.Controls.Add(Me.txtComments_CY2004)
Me.Panel79.Controls.Add(Me.DTPCloseOut_CY2004)
Me.Panel79.Controls.Add(Me.lblAmountPaid_CY2004)
Me.Panel79.Controls.Add(Me.DTPFeesPaid_CY2004)
Me.Panel79.Controls.Add(Me.DTPAOSent_CY2004)
Me.Panel79.Controls.Add(Me.DTPCOSent_CY2004)
Me.Panel79.Controls.Add(Me.DTPNOVSent_CY2004)
Me.Panel79.Controls.Add(Me.Panel82)
Me.Panel79.Controls.Add(Me.Panel83)
Me.Panel79.Controls.Add(Me.Panel84)
Me.Panel79.Controls.Add(Me.DTPLetterRemailed_CY2004)
Me.Panel79.Controls.Add(Me.Panel85)
Me.Panel79.Controls.Add(Me.DTPLetterReturned_CY2004)
Me.Panel79.Controls.Add(Me.DTPInitialLetter_2004)
Me.Panel79.Controls.Add(Me.Label121)
Me.Panel79.Controls.Add(Me.Label122)
Me.Panel79.Controls.Add(Me.Label123)
Me.Panel79.Controls.Add(Me.Label124)
Me.Panel79.Controls.Add(Me.Label125)
Me.Panel79.Controls.Add(Me.Label126)
Me.Panel79.Controls.Add(Me.Label127)
Me.Panel79.Controls.Add(Me.Label128)
Me.Panel79.Controls.Add(Me.Label129)
Me.Panel79.Controls.Add(Me.Label130)
Me.Panel79.Controls.Add(Me.Label131)
Me.Panel79.Controls.Add(Me.Label132)
Me.Panel79.Controls.Add(Me.Label133)
Me.Panel79.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel79.Location = New System.Drawing.Point(3, 35)
Me.Panel79.Name = "Panel79"
Me.Panel79.Size = New System.Drawing.Size(764, 565)
Me.Panel79.TabIndex = 4
'
'Label43
'
Me.Label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label43.Location = New System.Drawing.Point(0, 269)
Me.Label43.Name = "Label43"
Me.Label43.Size = New System.Drawing.Size(764, 2)
Me.Label43.TabIndex = 395
'
'Label44
'
Me.Label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label44.Location = New System.Drawing.Point(0, 183)
Me.Label44.Name = "Label44"
Me.Label44.Size = New System.Drawing.Size(764, 2)
Me.Label44.TabIndex = 394
'
'Panel80
'
Me.Panel80.Controls.Add(Me.btnManagerSignOff_CY2004)
Me.Panel80.Controls.Add(Me.lblSignOffDat_04)
Me.Panel80.Controls.Add(Me.lblManagerSignOff_04)
Me.Panel80.Location = New System.Drawing.Point(465, 197)
Me.Panel80.Name = "Panel80"
Me.Panel80.Size = New System.Drawing.Size(246, 61)
Me.Panel80.TabIndex = 391
'
'btnManagerSignOff_CY2004
'
Me.btnManagerSignOff_CY2004.AutoSize = true
Me.btnManagerSignOff_CY2004.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnManagerSignOff_CY2004.Location = New System.Drawing.Point(6, 26)
Me.btnManagerSignOff_CY2004.Name = "btnManagerSignOff_CY2004"
Me.btnManagerSignOff_CY2004.Size = New System.Drawing.Size(55, 23)
Me.btnManagerSignOff_CY2004.TabIndex = 398
Me.btnManagerSignOff_CY2004.Text = "Sign-Off"
Me.btnManagerSignOff_CY2004.UseVisualStyleBackColor = true
'
'lblSignOffDat_04
'
Me.lblSignOffDat_04.AutoSize = true
Me.lblSignOffDat_04.Location = New System.Drawing.Point(67, 31)
Me.lblSignOffDat_04.Name = "lblSignOffDat_04"
Me.lblSignOffDat_04.Size = New System.Drawing.Size(39, 13)
Me.lblSignOffDat_04.TabIndex = 2
Me.lblSignOffDat_04.Text = "Date - "
'
'lblManagerSignOff_04
'
Me.lblManagerSignOff_04.AutoSize = true
Me.lblManagerSignOff_04.Location = New System.Drawing.Point(3, 10)
Me.lblManagerSignOff_04.Name = "lblManagerSignOff_04"
Me.lblManagerSignOff_04.Size = New System.Drawing.Size(88, 13)
Me.lblManagerSignOff_04.TabIndex = 1
Me.lblManagerSignOff_04.Text = "Manager Sign-off"
'
'Panel81
'
Me.Panel81.Controls.Add(Me.lblLastModified_04)
Me.Panel81.Controls.Add(Me.lblStaffAssigned_04)
Me.Panel81.Location = New System.Drawing.Point(465, 111)
Me.Panel81.Name = "Panel81"
Me.Panel81.Size = New System.Drawing.Size(246, 61)
Me.Panel81.TabIndex = 390
'
'lblLastModified_04
'
Me.lblLastModified_04.AutoSize = true
Me.lblLastModified_04.Location = New System.Drawing.Point(3, 32)
Me.lblLastModified_04.Name = "lblLastModified_04"
Me.lblLastModified_04.Size = New System.Drawing.Size(70, 13)
Me.lblLastModified_04.TabIndex = 2
Me.lblLastModified_04.Text = "Last Modified"
'
'lblStaffAssigned_04
'
Me.lblStaffAssigned_04.AutoSize = true
Me.lblStaffAssigned_04.Location = New System.Drawing.Point(3, 10)
Me.lblStaffAssigned_04.Name = "lblStaffAssigned_04"
Me.lblStaffAssigned_04.Size = New System.Drawing.Size(95, 13)
Me.lblStaffAssigned_04.TabIndex = 1
Me.lblStaffAssigned_04.Text = "Staff Last Modified"
'
'txtComments_CY2004
'
Me.txtComments_CY2004.Location = New System.Drawing.Point(62, 348)
Me.txtComments_CY2004.Multiline = true
Me.txtComments_CY2004.Name = "txtComments_CY2004"
Me.txtComments_CY2004.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtComments_CY2004.Size = New System.Drawing.Size(383, 64)
Me.txtComments_CY2004.TabIndex = 388
'
'DTPCloseOut_CY2004
'
Me.DTPCloseOut_CY2004.Checked = false
Me.DTPCloseOut_CY2004.CustomFormat = "dd-MMM-yyyy"
Me.DTPCloseOut_CY2004.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCloseOut_CY2004.Location = New System.Drawing.Point(116, 314)
Me.DTPCloseOut_CY2004.Name = "DTPCloseOut_CY2004"
Me.DTPCloseOut_CY2004.ShowCheckBox = true
Me.DTPCloseOut_CY2004.Size = New System.Drawing.Size(102, 20)
Me.DTPCloseOut_CY2004.TabIndex = 387
Me.DTPCloseOut_CY2004.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'lblAmountPaid_CY2004
'
Me.lblAmountPaid_CY2004.AutoSize = true
Me.lblAmountPaid_CY2004.Location = New System.Drawing.Point(202, 279)
Me.lblAmountPaid_CY2004.Name = "lblAmountPaid_CY2004"
Me.lblAmountPaid_CY2004.Size = New System.Drawing.Size(63, 13)
Me.lblAmountPaid_CY2004.TabIndex = 386
Me.lblAmountPaid_CY2004.Text = "Fees Paid: -"
'
'DTPFeesPaid_CY2004
'
Me.DTPFeesPaid_CY2004.Checked = false
Me.DTPFeesPaid_CY2004.CustomFormat = "dd-MMM-yyyy"
Me.DTPFeesPaid_CY2004.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPFeesPaid_CY2004.Location = New System.Drawing.Point(93, 275)
Me.DTPFeesPaid_CY2004.Name = "DTPFeesPaid_CY2004"
Me.DTPFeesPaid_CY2004.ShowCheckBox = true
Me.DTPFeesPaid_CY2004.Size = New System.Drawing.Size(102, 20)
Me.DTPFeesPaid_CY2004.TabIndex = 384
Me.DTPFeesPaid_CY2004.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPAOSent_CY2004
'
Me.DTPAOSent_CY2004.Checked = false
Me.DTPAOSent_CY2004.CustomFormat = "dd-MMM-yyyy"
Me.DTPAOSent_CY2004.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPAOSent_CY2004.Location = New System.Drawing.Point(93, 245)
Me.DTPAOSent_CY2004.Name = "DTPAOSent_CY2004"
Me.DTPAOSent_CY2004.ShowCheckBox = true
Me.DTPAOSent_CY2004.Size = New System.Drawing.Size(102, 20)
Me.DTPAOSent_CY2004.TabIndex = 383
Me.DTPAOSent_CY2004.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPCOSent_CY2004
'
Me.DTPCOSent_CY2004.Checked = false
Me.DTPCOSent_CY2004.CustomFormat = "dd-MMM-yyyy"
Me.DTPCOSent_CY2004.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCOSent_CY2004.Location = New System.Drawing.Point(93, 219)
Me.DTPCOSent_CY2004.Name = "DTPCOSent_CY2004"
Me.DTPCOSent_CY2004.ShowCheckBox = true
Me.DTPCOSent_CY2004.Size = New System.Drawing.Size(102, 20)
Me.DTPCOSent_CY2004.TabIndex = 382
Me.DTPCOSent_CY2004.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPNOVSent_CY2004
'
Me.DTPNOVSent_CY2004.Checked = false
Me.DTPNOVSent_CY2004.CustomFormat = "dd-MMM-yyyy"
Me.DTPNOVSent_CY2004.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPNOVSent_CY2004.Location = New System.Drawing.Point(93, 193)
Me.DTPNOVSent_CY2004.Name = "DTPNOVSent_CY2004"
Me.DTPNOVSent_CY2004.ShowCheckBox = true
Me.DTPNOVSent_CY2004.Size = New System.Drawing.Size(102, 20)
Me.DTPNOVSent_CY2004.TabIndex = 381
Me.DTPNOVSent_CY2004.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel82
'
Me.Panel82.AutoSize = true
Me.Panel82.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel82.Controls.Add(Me.rdbUnabletoContactNo_CY2004)
Me.Panel82.Controls.Add(Me.rdbUnabletoContactYes_CY2004)
Me.Panel82.Location = New System.Drawing.Point(276, 154)
Me.Panel82.Name = "Panel82"
Me.Panel82.Size = New System.Drawing.Size(94, 23)
Me.Panel82.TabIndex = 380
'
'rdbUnabletoContactNo_CY2004
'
Me.rdbUnabletoContactNo_CY2004.AutoSize = true
Me.rdbUnabletoContactNo_CY2004.Location = New System.Drawing.Point(50, 3)
Me.rdbUnabletoContactNo_CY2004.Name = "rdbUnabletoContactNo_CY2004"
Me.rdbUnabletoContactNo_CY2004.Size = New System.Drawing.Size(41, 17)
Me.rdbUnabletoContactNo_CY2004.TabIndex = 1
Me.rdbUnabletoContactNo_CY2004.TabStop = true
Me.rdbUnabletoContactNo_CY2004.Text = "NO"
Me.rdbUnabletoContactNo_CY2004.UseVisualStyleBackColor = true
'
'rdbUnabletoContactYes_CY2004
'
Me.rdbUnabletoContactYes_CY2004.AutoSize = true
Me.rdbUnabletoContactYes_CY2004.Location = New System.Drawing.Point(3, 3)
Me.rdbUnabletoContactYes_CY2004.Name = "rdbUnabletoContactYes_CY2004"
Me.rdbUnabletoContactYes_CY2004.Size = New System.Drawing.Size(46, 17)
Me.rdbUnabletoContactYes_CY2004.TabIndex = 0
Me.rdbUnabletoContactYes_CY2004.TabStop = true
Me.rdbUnabletoContactYes_CY2004.Text = "YES"
Me.rdbUnabletoContactYes_CY2004.UseVisualStyleBackColor = true
'
'Panel83
'
Me.Panel83.AutoSize = true
Me.Panel83.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel83.Controls.Add(Me.rdbBankruptcyNo_CY2004)
Me.Panel83.Controls.Add(Me.rdbBankruptcyYes_CY2004)
Me.Panel83.Location = New System.Drawing.Point(84, 128)
Me.Panel83.Name = "Panel83"
Me.Panel83.Size = New System.Drawing.Size(94, 23)
Me.Panel83.TabIndex = 379
'
'rdbBankruptcyNo_CY2004
'
Me.rdbBankruptcyNo_CY2004.AutoSize = true
Me.rdbBankruptcyNo_CY2004.Location = New System.Drawing.Point(50, 3)
Me.rdbBankruptcyNo_CY2004.Name = "rdbBankruptcyNo_CY2004"
Me.rdbBankruptcyNo_CY2004.Size = New System.Drawing.Size(41, 17)
Me.rdbBankruptcyNo_CY2004.TabIndex = 1
Me.rdbBankruptcyNo_CY2004.TabStop = true
Me.rdbBankruptcyNo_CY2004.Text = "NO"
Me.rdbBankruptcyNo_CY2004.UseVisualStyleBackColor = true
'
'rdbBankruptcyYes_CY2004
'
Me.rdbBankruptcyYes_CY2004.AutoSize = true
Me.rdbBankruptcyYes_CY2004.Location = New System.Drawing.Point(3, 3)
Me.rdbBankruptcyYes_CY2004.Name = "rdbBankruptcyYes_CY2004"
Me.rdbBankruptcyYes_CY2004.Size = New System.Drawing.Size(46, 17)
Me.rdbBankruptcyYes_CY2004.TabIndex = 0
Me.rdbBankruptcyYes_CY2004.TabStop = true
Me.rdbBankruptcyYes_CY2004.Text = "YES"
Me.rdbBankruptcyYes_CY2004.UseVisualStyleBackColor = true
'
'Panel84
'
Me.Panel84.AutoSize = true
Me.Panel84.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel84.Controls.Add(Me.rdbDataCorrectNo_CY2004)
Me.Panel84.Controls.Add(Me.rdbDataCorrectYes_CY2004)
Me.Panel84.Location = New System.Drawing.Point(214, 92)
Me.Panel84.Name = "Panel84"
Me.Panel84.Size = New System.Drawing.Size(94, 23)
Me.Panel84.TabIndex = 378
'
'rdbDataCorrectNo_CY2004
'
Me.rdbDataCorrectNo_CY2004.AutoSize = true
Me.rdbDataCorrectNo_CY2004.Location = New System.Drawing.Point(50, 3)
Me.rdbDataCorrectNo_CY2004.Name = "rdbDataCorrectNo_CY2004"
Me.rdbDataCorrectNo_CY2004.Size = New System.Drawing.Size(41, 17)
Me.rdbDataCorrectNo_CY2004.TabIndex = 1
Me.rdbDataCorrectNo_CY2004.TabStop = true
Me.rdbDataCorrectNo_CY2004.Text = "NO"
Me.rdbDataCorrectNo_CY2004.UseVisualStyleBackColor = true
'
'rdbDataCorrectYes_CY2004
'
Me.rdbDataCorrectYes_CY2004.AutoSize = true
Me.rdbDataCorrectYes_CY2004.Location = New System.Drawing.Point(3, 3)
Me.rdbDataCorrectYes_CY2004.Name = "rdbDataCorrectYes_CY2004"
Me.rdbDataCorrectYes_CY2004.Size = New System.Drawing.Size(46, 17)
Me.rdbDataCorrectYes_CY2004.TabIndex = 0
Me.rdbDataCorrectYes_CY2004.TabStop = true
Me.rdbDataCorrectYes_CY2004.Text = "YES"
Me.rdbDataCorrectYes_CY2004.UseVisualStyleBackColor = true
'
'DTPLetterRemailed_CY2004
'
Me.DTPLetterRemailed_CY2004.Checked = false
Me.DTPLetterRemailed_CY2004.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterRemailed_CY2004.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterRemailed_CY2004.Location = New System.Drawing.Point(465, 62)
Me.DTPLetterRemailed_CY2004.Name = "DTPLetterRemailed_CY2004"
Me.DTPLetterRemailed_CY2004.ShowCheckBox = true
Me.DTPLetterRemailed_CY2004.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterRemailed_CY2004.TabIndex = 377
Me.DTPLetterRemailed_CY2004.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel85
'
Me.Panel85.AutoSize = true
Me.Panel85.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel85.Controls.Add(Me.rdbAddressUnknownNo_CY2004)
Me.Panel85.Controls.Add(Me.rdbAddressUnknownYes_CY2004)
Me.Panel85.Location = New System.Drawing.Point(214, 61)
Me.Panel85.Name = "Panel85"
Me.Panel85.Size = New System.Drawing.Size(94, 23)
Me.Panel85.TabIndex = 376
'
'rdbAddressUnknownNo_CY2004
'
Me.rdbAddressUnknownNo_CY2004.AutoSize = true
Me.rdbAddressUnknownNo_CY2004.Location = New System.Drawing.Point(50, 3)
Me.rdbAddressUnknownNo_CY2004.Name = "rdbAddressUnknownNo_CY2004"
Me.rdbAddressUnknownNo_CY2004.Size = New System.Drawing.Size(41, 17)
Me.rdbAddressUnknownNo_CY2004.TabIndex = 1
Me.rdbAddressUnknownNo_CY2004.TabStop = true
Me.rdbAddressUnknownNo_CY2004.Text = "NO"
Me.rdbAddressUnknownNo_CY2004.UseVisualStyleBackColor = true
'
'rdbAddressUnknownYes_CY2004
'
Me.rdbAddressUnknownYes_CY2004.AutoSize = true
Me.rdbAddressUnknownYes_CY2004.Location = New System.Drawing.Point(3, 3)
Me.rdbAddressUnknownYes_CY2004.Name = "rdbAddressUnknownYes_CY2004"
Me.rdbAddressUnknownYes_CY2004.Size = New System.Drawing.Size(46, 17)
Me.rdbAddressUnknownYes_CY2004.TabIndex = 0
Me.rdbAddressUnknownYes_CY2004.TabStop = true
Me.rdbAddressUnknownYes_CY2004.Text = "YES"
Me.rdbAddressUnknownYes_CY2004.UseVisualStyleBackColor = true
'
'DTPLetterReturned_CY2004
'
Me.DTPLetterReturned_CY2004.Checked = false
Me.DTPLetterReturned_CY2004.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterReturned_CY2004.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterReturned_CY2004.Location = New System.Drawing.Point(193, 33)
Me.DTPLetterReturned_CY2004.Name = "DTPLetterReturned_CY2004"
Me.DTPLetterReturned_CY2004.ShowCheckBox = true
Me.DTPLetterReturned_CY2004.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterReturned_CY2004.TabIndex = 375
Me.DTPLetterReturned_CY2004.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPInitialLetter_2004
'
Me.DTPInitialLetter_2004.Checked = false
Me.DTPInitialLetter_2004.CustomFormat = "dd-MMM-yyyy"
Me.DTPInitialLetter_2004.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPInitialLetter_2004.Location = New System.Drawing.Point(98, 7)
Me.DTPInitialLetter_2004.Name = "DTPInitialLetter_2004"
Me.DTPInitialLetter_2004.ShowCheckBox = true
Me.DTPInitialLetter_2004.Size = New System.Drawing.Size(102, 20)
Me.DTPInitialLetter_2004.TabIndex = 374
Me.DTPInitialLetter_2004.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Label121
'
Me.Label121.AutoSize = true
Me.Label121.Location = New System.Drawing.Point(4, 348)
Me.Label121.Name = "Label121"
Me.Label121.Size = New System.Drawing.Size(56, 13)
Me.Label121.TabIndex = 12
Me.Label121.Text = "Comments"
'
'Label122
'
Me.Label122.AutoSize = true
Me.Label122.Location = New System.Drawing.Point(4, 318)
Me.Label122.Name = "Label122"
Me.Label122.Size = New System.Drawing.Size(101, 13)
Me.Label122.TabIndex = 11
Me.Label122.Text = "Close Out Fee Audit"
'
'Label123
'
Me.Label123.AutoSize = true
Me.Label123.Location = New System.Drawing.Point(4, 223)
Me.Label123.Name = "Label123"
Me.Label123.Size = New System.Drawing.Size(77, 13)
Me.Label123.TabIndex = 10
Me.Label123.Text = "CO Letter Sent"
'
'Label124
'
Me.Label124.AutoSize = true
Me.Label124.Location = New System.Drawing.Point(4, 279)
Me.Label124.Name = "Label124"
Me.Label124.Size = New System.Drawing.Size(87, 13)
Me.Label124.TabIndex = 9
Me.Label124.Text = "Facilty Paid Fees"
'
'Label125
'
Me.Label125.AutoSize = true
Me.Label125.Location = New System.Drawing.Point(4, 249)
Me.Label125.Name = "Label125"
Me.Label125.Size = New System.Drawing.Size(77, 13)
Me.Label125.TabIndex = 8
Me.Label125.Text = "AO Letter Sent"
'
'Label126
'
Me.Label126.AutoSize = true
Me.Label126.Location = New System.Drawing.Point(4, 133)
Me.Label126.Name = "Label126"
Me.Label126.Size = New System.Drawing.Size(61, 13)
Me.Label126.TabIndex = 7
Me.Label126.Text = "Bankruptcy"
'
'Label127
'
Me.Label127.AutoSize = true
Me.Label127.Location = New System.Drawing.Point(4, 159)
Me.Label127.Name = "Label127"
Me.Label127.Size = New System.Drawing.Size(252, 13)
Me.Label127.TabIndex = 6
Me.Label127.Text = "Unable to Contact Facility Or Facility Representative"
'
'Label128
'
Me.Label128.AutoSize = true
Me.Label128.Location = New System.Drawing.Point(4, 197)
Me.Label128.Name = "Label128"
Me.Label128.Size = New System.Drawing.Size(85, 13)
Me.Label128.TabIndex = 5
Me.Label128.Text = "NOV Letter Sent"
'
'Label129
'
Me.Label129.AutoSize = true
Me.Label129.Location = New System.Drawing.Point(4, 37)
Me.Label129.Name = "Label129"
Me.Label129.Size = New System.Drawing.Size(183, 13)
Me.Label129.TabIndex = 4
Me.Label129.Text = "Letter Returned/Response Received"
'
'Label130
'
Me.Label130.AutoSize = true
Me.Label130.Location = New System.Drawing.Point(83, 66)
Me.Label130.Name = "Label130"
Me.Label130.Size = New System.Drawing.Size(125, 13)
Me.Label130.TabIndex = 3
Me.Label130.Text = "Was Address Unknown?"
'
'Label131
'
Me.Label131.AutoSize = true
Me.Label131.Location = New System.Drawing.Point(342, 66)
Me.Label131.Name = "Label131"
Me.Label131.Size = New System.Drawing.Size(112, 13)
Me.Label131.TabIndex = 2
Me.Label131.Text = "Initial Letter Re-Mailed"
'
'Label132
'
Me.Label132.AutoSize = true
Me.Label132.Location = New System.Drawing.Point(4, 97)
Me.Label132.Name = "Label132"
Me.Label132.Size = New System.Drawing.Size(196, 13)
Me.Label132.TabIndex = 1
Me.Label132.Text = "Facility Responded with Data Correction"
'
'Label133
'
Me.Label133.AutoSize = true
Me.Label133.Location = New System.Drawing.Point(4, 11)
Me.Label133.Name = "Label133"
Me.Label133.Size = New System.Drawing.Size(95, 13)
Me.Label133.TabIndex = 0
Me.Label133.Text = "Initial Letter Mailed"
'
'Panel55
'
Me.Panel55.Controls.Add(Me.lblAuditType_CY2004)
Me.Panel55.Controls.Add(Me.btnSaveFeeAudit_CY2004)
Me.Panel55.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel55.Location = New System.Drawing.Point(3, 3)
Me.Panel55.Name = "Panel55"
Me.Panel55.Size = New System.Drawing.Size(764, 32)
Me.Panel55.TabIndex = 3
'
'lblAuditType_CY2004
'
Me.lblAuditType_CY2004.AutoSize = true
Me.lblAuditType_CY2004.Location = New System.Drawing.Point(49, 8)
Me.lblAuditType_CY2004.Name = "lblAuditType_CY2004"
Me.lblAuditType_CY2004.Size = New System.Drawing.Size(58, 13)
Me.lblAuditType_CY2004.TabIndex = 4
Me.lblAuditType_CY2004.Text = "Audit Type"
'
'btnSaveFeeAudit_CY2004
'
Me.btnSaveFeeAudit_CY2004.AutoSize = true
Me.btnSaveFeeAudit_CY2004.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveFeeAudit_CY2004.Location = New System.Drawing.Point(3, 3)
Me.btnSaveFeeAudit_CY2004.Name = "btnSaveFeeAudit_CY2004"
Me.btnSaveFeeAudit_CY2004.Size = New System.Drawing.Size(42, 23)
Me.btnSaveFeeAudit_CY2004.TabIndex = 389
Me.btnSaveFeeAudit_CY2004.Text = "Save"
Me.btnSaveFeeAudit_CY2004.UseVisualStyleBackColor = true
'
'TP_Tracking_CY2003
'
Me.TP_Tracking_CY2003.Controls.Add(Me.Panel86)
Me.TP_Tracking_CY2003.Controls.Add(Me.Panel56)
Me.TP_Tracking_CY2003.Location = New System.Drawing.Point(4, 22)
Me.TP_Tracking_CY2003.Name = "TP_Tracking_CY2003"
Me.TP_Tracking_CY2003.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Tracking_CY2003.Size = New System.Drawing.Size(770, 603)
Me.TP_Tracking_CY2003.TabIndex = 6
Me.TP_Tracking_CY2003.Text = "CY2003"
Me.TP_Tracking_CY2003.UseVisualStyleBackColor = true
'
'Panel86
'
Me.Panel86.AutoScroll = true
Me.Panel86.Controls.Add(Me.Label45)
Me.Panel86.Controls.Add(Me.Label46)
Me.Panel86.Controls.Add(Me.Panel87)
Me.Panel86.Controls.Add(Me.Panel88)
Me.Panel86.Controls.Add(Me.txtComments_CY2003)
Me.Panel86.Controls.Add(Me.DTPCloseOut_CY2003)
Me.Panel86.Controls.Add(Me.lblAmountPaid_CY2003)
Me.Panel86.Controls.Add(Me.DTPFeesPaid_CY2003)
Me.Panel86.Controls.Add(Me.DTPAOSent_CY2003)
Me.Panel86.Controls.Add(Me.DTPCOSent_CY2003)
Me.Panel86.Controls.Add(Me.DTPNOVSent_CY2003)
Me.Panel86.Controls.Add(Me.Panel89)
Me.Panel86.Controls.Add(Me.Panel90)
Me.Panel86.Controls.Add(Me.Panel91)
Me.Panel86.Controls.Add(Me.DTPLetterRemailed_CY2003)
Me.Panel86.Controls.Add(Me.Panel92)
Me.Panel86.Controls.Add(Me.DTPLetterReturned_CY2003)
Me.Panel86.Controls.Add(Me.DTPInitialLetter_2003)
Me.Panel86.Controls.Add(Me.Label139)
Me.Panel86.Controls.Add(Me.Label140)
Me.Panel86.Controls.Add(Me.Label141)
Me.Panel86.Controls.Add(Me.Label142)
Me.Panel86.Controls.Add(Me.Label143)
Me.Panel86.Controls.Add(Me.Label144)
Me.Panel86.Controls.Add(Me.Label145)
Me.Panel86.Controls.Add(Me.Label146)
Me.Panel86.Controls.Add(Me.Label147)
Me.Panel86.Controls.Add(Me.Label148)
Me.Panel86.Controls.Add(Me.Label149)
Me.Panel86.Controls.Add(Me.Label150)
Me.Panel86.Controls.Add(Me.Label151)
Me.Panel86.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel86.Location = New System.Drawing.Point(3, 35)
Me.Panel86.Name = "Panel86"
Me.Panel86.Size = New System.Drawing.Size(764, 565)
Me.Panel86.TabIndex = 4
'
'Label45
'
Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label45.Location = New System.Drawing.Point(0, 269)
Me.Label45.Name = "Label45"
Me.Label45.Size = New System.Drawing.Size(764, 2)
Me.Label45.TabIndex = 395
'
'Label46
'
Me.Label46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label46.Location = New System.Drawing.Point(0, 183)
Me.Label46.Name = "Label46"
Me.Label46.Size = New System.Drawing.Size(764, 2)
Me.Label46.TabIndex = 394
'
'Panel87
'
Me.Panel87.Controls.Add(Me.btnManagerSignOff_CY2003)
Me.Panel87.Controls.Add(Me.lblSignOffDat_03)
Me.Panel87.Controls.Add(Me.lblManagerSignOff_03)
Me.Panel87.Location = New System.Drawing.Point(465, 197)
Me.Panel87.Name = "Panel87"
Me.Panel87.Size = New System.Drawing.Size(246, 61)
Me.Panel87.TabIndex = 391
'
'btnManagerSignOff_CY2003
'
Me.btnManagerSignOff_CY2003.AutoSize = true
Me.btnManagerSignOff_CY2003.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnManagerSignOff_CY2003.Location = New System.Drawing.Point(6, 26)
Me.btnManagerSignOff_CY2003.Name = "btnManagerSignOff_CY2003"
Me.btnManagerSignOff_CY2003.Size = New System.Drawing.Size(55, 23)
Me.btnManagerSignOff_CY2003.TabIndex = 399
Me.btnManagerSignOff_CY2003.Text = "Sign-Off"
Me.btnManagerSignOff_CY2003.UseVisualStyleBackColor = true
'
'lblSignOffDat_03
'
Me.lblSignOffDat_03.AutoSize = true
Me.lblSignOffDat_03.Location = New System.Drawing.Point(67, 31)
Me.lblSignOffDat_03.Name = "lblSignOffDat_03"
Me.lblSignOffDat_03.Size = New System.Drawing.Size(39, 13)
Me.lblSignOffDat_03.TabIndex = 2
Me.lblSignOffDat_03.Text = "Date - "
'
'lblManagerSignOff_03
'
Me.lblManagerSignOff_03.AutoSize = true
Me.lblManagerSignOff_03.Location = New System.Drawing.Point(3, 10)
Me.lblManagerSignOff_03.Name = "lblManagerSignOff_03"
Me.lblManagerSignOff_03.Size = New System.Drawing.Size(88, 13)
Me.lblManagerSignOff_03.TabIndex = 1
Me.lblManagerSignOff_03.Text = "Manager Sign-off"
'
'Panel88
'
Me.Panel88.Controls.Add(Me.lblLastModified_03)
Me.Panel88.Controls.Add(Me.lblStaffAssigned_03)
Me.Panel88.Location = New System.Drawing.Point(465, 111)
Me.Panel88.Name = "Panel88"
Me.Panel88.Size = New System.Drawing.Size(246, 61)
Me.Panel88.TabIndex = 390
'
'lblLastModified_03
'
Me.lblLastModified_03.AutoSize = true
Me.lblLastModified_03.Location = New System.Drawing.Point(3, 32)
Me.lblLastModified_03.Name = "lblLastModified_03"
Me.lblLastModified_03.Size = New System.Drawing.Size(70, 13)
Me.lblLastModified_03.TabIndex = 2
Me.lblLastModified_03.Text = "Last Modified"
'
'lblStaffAssigned_03
'
Me.lblStaffAssigned_03.AutoSize = true
Me.lblStaffAssigned_03.Location = New System.Drawing.Point(3, 10)
Me.lblStaffAssigned_03.Name = "lblStaffAssigned_03"
Me.lblStaffAssigned_03.Size = New System.Drawing.Size(95, 13)
Me.lblStaffAssigned_03.TabIndex = 1
Me.lblStaffAssigned_03.Text = "Staff Last Modified"
'
'txtComments_CY2003
'
Me.txtComments_CY2003.Location = New System.Drawing.Point(62, 348)
Me.txtComments_CY2003.Multiline = true
Me.txtComments_CY2003.Name = "txtComments_CY2003"
Me.txtComments_CY2003.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtComments_CY2003.Size = New System.Drawing.Size(383, 64)
Me.txtComments_CY2003.TabIndex = 388
'
'DTPCloseOut_CY2003
'
Me.DTPCloseOut_CY2003.Checked = false
Me.DTPCloseOut_CY2003.CustomFormat = "dd-MMM-yyyy"
Me.DTPCloseOut_CY2003.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCloseOut_CY2003.Location = New System.Drawing.Point(116, 314)
Me.DTPCloseOut_CY2003.Name = "DTPCloseOut_CY2003"
Me.DTPCloseOut_CY2003.ShowCheckBox = true
Me.DTPCloseOut_CY2003.Size = New System.Drawing.Size(102, 20)
Me.DTPCloseOut_CY2003.TabIndex = 387
Me.DTPCloseOut_CY2003.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'lblAmountPaid_CY2003
'
Me.lblAmountPaid_CY2003.AutoSize = true
Me.lblAmountPaid_CY2003.Location = New System.Drawing.Point(202, 279)
Me.lblAmountPaid_CY2003.Name = "lblAmountPaid_CY2003"
Me.lblAmountPaid_CY2003.Size = New System.Drawing.Size(63, 13)
Me.lblAmountPaid_CY2003.TabIndex = 386
Me.lblAmountPaid_CY2003.Text = "Fees Paid: -"
'
'DTPFeesPaid_CY2003
'
Me.DTPFeesPaid_CY2003.Checked = false
Me.DTPFeesPaid_CY2003.CustomFormat = "dd-MMM-yyyy"
Me.DTPFeesPaid_CY2003.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPFeesPaid_CY2003.Location = New System.Drawing.Point(93, 275)
Me.DTPFeesPaid_CY2003.Name = "DTPFeesPaid_CY2003"
Me.DTPFeesPaid_CY2003.ShowCheckBox = true
Me.DTPFeesPaid_CY2003.Size = New System.Drawing.Size(102, 20)
Me.DTPFeesPaid_CY2003.TabIndex = 384
Me.DTPFeesPaid_CY2003.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPAOSent_CY2003
'
Me.DTPAOSent_CY2003.Checked = false
Me.DTPAOSent_CY2003.CustomFormat = "dd-MMM-yyyy"
Me.DTPAOSent_CY2003.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPAOSent_CY2003.Location = New System.Drawing.Point(93, 245)
Me.DTPAOSent_CY2003.Name = "DTPAOSent_CY2003"
Me.DTPAOSent_CY2003.ShowCheckBox = true
Me.DTPAOSent_CY2003.Size = New System.Drawing.Size(102, 20)
Me.DTPAOSent_CY2003.TabIndex = 383
Me.DTPAOSent_CY2003.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPCOSent_CY2003
'
Me.DTPCOSent_CY2003.Checked = false
Me.DTPCOSent_CY2003.CustomFormat = "dd-MMM-yyyy"
Me.DTPCOSent_CY2003.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCOSent_CY2003.Location = New System.Drawing.Point(93, 219)
Me.DTPCOSent_CY2003.Name = "DTPCOSent_CY2003"
Me.DTPCOSent_CY2003.ShowCheckBox = true
Me.DTPCOSent_CY2003.Size = New System.Drawing.Size(102, 20)
Me.DTPCOSent_CY2003.TabIndex = 382
Me.DTPCOSent_CY2003.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPNOVSent_CY2003
'
Me.DTPNOVSent_CY2003.Checked = false
Me.DTPNOVSent_CY2003.CustomFormat = "dd-MMM-yyyy"
Me.DTPNOVSent_CY2003.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPNOVSent_CY2003.Location = New System.Drawing.Point(93, 193)
Me.DTPNOVSent_CY2003.Name = "DTPNOVSent_CY2003"
Me.DTPNOVSent_CY2003.ShowCheckBox = true
Me.DTPNOVSent_CY2003.Size = New System.Drawing.Size(102, 20)
Me.DTPNOVSent_CY2003.TabIndex = 381
Me.DTPNOVSent_CY2003.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel89
'
Me.Panel89.AutoSize = true
Me.Panel89.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel89.Controls.Add(Me.rdbUnabletoContactNo_CY2003)
Me.Panel89.Controls.Add(Me.rdbUnabletoContactYes_CY2003)
Me.Panel89.Location = New System.Drawing.Point(276, 154)
Me.Panel89.Name = "Panel89"
Me.Panel89.Size = New System.Drawing.Size(94, 23)
Me.Panel89.TabIndex = 380
'
'rdbUnabletoContactNo_CY2003
'
Me.rdbUnabletoContactNo_CY2003.AutoSize = true
Me.rdbUnabletoContactNo_CY2003.Location = New System.Drawing.Point(50, 3)
Me.rdbUnabletoContactNo_CY2003.Name = "rdbUnabletoContactNo_CY2003"
Me.rdbUnabletoContactNo_CY2003.Size = New System.Drawing.Size(41, 17)
Me.rdbUnabletoContactNo_CY2003.TabIndex = 1
Me.rdbUnabletoContactNo_CY2003.TabStop = true
Me.rdbUnabletoContactNo_CY2003.Text = "NO"
Me.rdbUnabletoContactNo_CY2003.UseVisualStyleBackColor = true
'
'rdbUnabletoContactYes_CY2003
'
Me.rdbUnabletoContactYes_CY2003.AutoSize = true
Me.rdbUnabletoContactYes_CY2003.Location = New System.Drawing.Point(3, 3)
Me.rdbUnabletoContactYes_CY2003.Name = "rdbUnabletoContactYes_CY2003"
Me.rdbUnabletoContactYes_CY2003.Size = New System.Drawing.Size(46, 17)
Me.rdbUnabletoContactYes_CY2003.TabIndex = 0
Me.rdbUnabletoContactYes_CY2003.TabStop = true
Me.rdbUnabletoContactYes_CY2003.Text = "YES"
Me.rdbUnabletoContactYes_CY2003.UseVisualStyleBackColor = true
'
'Panel90
'
Me.Panel90.AutoSize = true
Me.Panel90.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel90.Controls.Add(Me.rdbBankruptcyNo_CY2003)
Me.Panel90.Controls.Add(Me.rdbBankruptcyYes_CY2003)
Me.Panel90.Location = New System.Drawing.Point(84, 128)
Me.Panel90.Name = "Panel90"
Me.Panel90.Size = New System.Drawing.Size(94, 23)
Me.Panel90.TabIndex = 379
'
'rdbBankruptcyNo_CY2003
'
Me.rdbBankruptcyNo_CY2003.AutoSize = true
Me.rdbBankruptcyNo_CY2003.Location = New System.Drawing.Point(50, 3)
Me.rdbBankruptcyNo_CY2003.Name = "rdbBankruptcyNo_CY2003"
Me.rdbBankruptcyNo_CY2003.Size = New System.Drawing.Size(41, 17)
Me.rdbBankruptcyNo_CY2003.TabIndex = 1
Me.rdbBankruptcyNo_CY2003.TabStop = true
Me.rdbBankruptcyNo_CY2003.Text = "NO"
Me.rdbBankruptcyNo_CY2003.UseVisualStyleBackColor = true
'
'rdbBankruptcyYes_CY2003
'
Me.rdbBankruptcyYes_CY2003.AutoSize = true
Me.rdbBankruptcyYes_CY2003.Location = New System.Drawing.Point(3, 3)
Me.rdbBankruptcyYes_CY2003.Name = "rdbBankruptcyYes_CY2003"
Me.rdbBankruptcyYes_CY2003.Size = New System.Drawing.Size(46, 17)
Me.rdbBankruptcyYes_CY2003.TabIndex = 0
Me.rdbBankruptcyYes_CY2003.TabStop = true
Me.rdbBankruptcyYes_CY2003.Text = "YES"
Me.rdbBankruptcyYes_CY2003.UseVisualStyleBackColor = true
'
'Panel91
'
Me.Panel91.AutoSize = true
Me.Panel91.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel91.Controls.Add(Me.rdbDataCorrectNo_CY2003)
Me.Panel91.Controls.Add(Me.rdbDataCorrectYes_CY2003)
Me.Panel91.Location = New System.Drawing.Point(214, 92)
Me.Panel91.Name = "Panel91"
Me.Panel91.Size = New System.Drawing.Size(94, 23)
Me.Panel91.TabIndex = 378
'
'rdbDataCorrectNo_CY2003
'
Me.rdbDataCorrectNo_CY2003.AutoSize = true
Me.rdbDataCorrectNo_CY2003.Location = New System.Drawing.Point(50, 3)
Me.rdbDataCorrectNo_CY2003.Name = "rdbDataCorrectNo_CY2003"
Me.rdbDataCorrectNo_CY2003.Size = New System.Drawing.Size(41, 17)
Me.rdbDataCorrectNo_CY2003.TabIndex = 1
Me.rdbDataCorrectNo_CY2003.TabStop = true
Me.rdbDataCorrectNo_CY2003.Text = "NO"
Me.rdbDataCorrectNo_CY2003.UseVisualStyleBackColor = true
'
'rdbDataCorrectYes_CY2003
'
Me.rdbDataCorrectYes_CY2003.AutoSize = true
Me.rdbDataCorrectYes_CY2003.Location = New System.Drawing.Point(3, 3)
Me.rdbDataCorrectYes_CY2003.Name = "rdbDataCorrectYes_CY2003"
Me.rdbDataCorrectYes_CY2003.Size = New System.Drawing.Size(46, 17)
Me.rdbDataCorrectYes_CY2003.TabIndex = 0
Me.rdbDataCorrectYes_CY2003.TabStop = true
Me.rdbDataCorrectYes_CY2003.Text = "YES"
Me.rdbDataCorrectYes_CY2003.UseVisualStyleBackColor = true
'
'DTPLetterRemailed_CY2003
'
Me.DTPLetterRemailed_CY2003.Checked = false
Me.DTPLetterRemailed_CY2003.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterRemailed_CY2003.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterRemailed_CY2003.Location = New System.Drawing.Point(465, 62)
Me.DTPLetterRemailed_CY2003.Name = "DTPLetterRemailed_CY2003"
Me.DTPLetterRemailed_CY2003.ShowCheckBox = true
Me.DTPLetterRemailed_CY2003.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterRemailed_CY2003.TabIndex = 377
Me.DTPLetterRemailed_CY2003.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel92
'
Me.Panel92.AutoSize = true
Me.Panel92.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel92.Controls.Add(Me.rdbAddressUnknownNo_CY2003)
Me.Panel92.Controls.Add(Me.rdbAddressUnknownYes_CY2003)
Me.Panel92.Location = New System.Drawing.Point(214, 61)
Me.Panel92.Name = "Panel92"
Me.Panel92.Size = New System.Drawing.Size(94, 23)
Me.Panel92.TabIndex = 376
'
'rdbAddressUnknownNo_CY2003
'
Me.rdbAddressUnknownNo_CY2003.AutoSize = true
Me.rdbAddressUnknownNo_CY2003.Location = New System.Drawing.Point(50, 3)
Me.rdbAddressUnknownNo_CY2003.Name = "rdbAddressUnknownNo_CY2003"
Me.rdbAddressUnknownNo_CY2003.Size = New System.Drawing.Size(41, 17)
Me.rdbAddressUnknownNo_CY2003.TabIndex = 1
Me.rdbAddressUnknownNo_CY2003.TabStop = true
Me.rdbAddressUnknownNo_CY2003.Text = "NO"
Me.rdbAddressUnknownNo_CY2003.UseVisualStyleBackColor = true
'
'rdbAddressUnknownYes_CY2003
'
Me.rdbAddressUnknownYes_CY2003.AutoSize = true
Me.rdbAddressUnknownYes_CY2003.Location = New System.Drawing.Point(3, 3)
Me.rdbAddressUnknownYes_CY2003.Name = "rdbAddressUnknownYes_CY2003"
Me.rdbAddressUnknownYes_CY2003.Size = New System.Drawing.Size(46, 17)
Me.rdbAddressUnknownYes_CY2003.TabIndex = 0
Me.rdbAddressUnknownYes_CY2003.TabStop = true
Me.rdbAddressUnknownYes_CY2003.Text = "YES"
Me.rdbAddressUnknownYes_CY2003.UseVisualStyleBackColor = true
'
'DTPLetterReturned_CY2003
'
Me.DTPLetterReturned_CY2003.Checked = false
Me.DTPLetterReturned_CY2003.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterReturned_CY2003.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterReturned_CY2003.Location = New System.Drawing.Point(193, 33)
Me.DTPLetterReturned_CY2003.Name = "DTPLetterReturned_CY2003"
Me.DTPLetterReturned_CY2003.ShowCheckBox = true
Me.DTPLetterReturned_CY2003.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterReturned_CY2003.TabIndex = 375
Me.DTPLetterReturned_CY2003.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPInitialLetter_2003
'
Me.DTPInitialLetter_2003.Checked = false
Me.DTPInitialLetter_2003.CustomFormat = "dd-MMM-yyyy"
Me.DTPInitialLetter_2003.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPInitialLetter_2003.Location = New System.Drawing.Point(98, 7)
Me.DTPInitialLetter_2003.Name = "DTPInitialLetter_2003"
Me.DTPInitialLetter_2003.ShowCheckBox = true
Me.DTPInitialLetter_2003.Size = New System.Drawing.Size(102, 20)
Me.DTPInitialLetter_2003.TabIndex = 374
Me.DTPInitialLetter_2003.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Label139
'
Me.Label139.AutoSize = true
Me.Label139.Location = New System.Drawing.Point(4, 348)
Me.Label139.Name = "Label139"
Me.Label139.Size = New System.Drawing.Size(56, 13)
Me.Label139.TabIndex = 12
Me.Label139.Text = "Comments"
'
'Label140
'
Me.Label140.AutoSize = true
Me.Label140.Location = New System.Drawing.Point(4, 318)
Me.Label140.Name = "Label140"
Me.Label140.Size = New System.Drawing.Size(101, 13)
Me.Label140.TabIndex = 11
Me.Label140.Text = "Close Out Fee Audit"
'
'Label141
'
Me.Label141.AutoSize = true
Me.Label141.Location = New System.Drawing.Point(4, 223)
Me.Label141.Name = "Label141"
Me.Label141.Size = New System.Drawing.Size(77, 13)
Me.Label141.TabIndex = 10
Me.Label141.Text = "CO Letter Sent"
'
'Label142
'
Me.Label142.AutoSize = true
Me.Label142.Location = New System.Drawing.Point(4, 279)
Me.Label142.Name = "Label142"
Me.Label142.Size = New System.Drawing.Size(87, 13)
Me.Label142.TabIndex = 9
Me.Label142.Text = "Facilty Paid Fees"
'
'Label143
'
Me.Label143.AutoSize = true
Me.Label143.Location = New System.Drawing.Point(4, 249)
Me.Label143.Name = "Label143"
Me.Label143.Size = New System.Drawing.Size(77, 13)
Me.Label143.TabIndex = 8
Me.Label143.Text = "AO Letter Sent"
'
'Label144
'
Me.Label144.AutoSize = true
Me.Label144.Location = New System.Drawing.Point(4, 133)
Me.Label144.Name = "Label144"
Me.Label144.Size = New System.Drawing.Size(61, 13)
Me.Label144.TabIndex = 7
Me.Label144.Text = "Bankruptcy"
'
'Label145
'
Me.Label145.AutoSize = true
Me.Label145.Location = New System.Drawing.Point(4, 159)
Me.Label145.Name = "Label145"
Me.Label145.Size = New System.Drawing.Size(252, 13)
Me.Label145.TabIndex = 6
Me.Label145.Text = "Unable to Contact Facility Or Facility Representative"
'
'Label146
'
Me.Label146.AutoSize = true
Me.Label146.Location = New System.Drawing.Point(4, 197)
Me.Label146.Name = "Label146"
Me.Label146.Size = New System.Drawing.Size(85, 13)
Me.Label146.TabIndex = 5
Me.Label146.Text = "NOV Letter Sent"
'
'Label147
'
Me.Label147.AutoSize = true
Me.Label147.Location = New System.Drawing.Point(4, 37)
Me.Label147.Name = "Label147"
Me.Label147.Size = New System.Drawing.Size(183, 13)
Me.Label147.TabIndex = 4
Me.Label147.Text = "Letter Returned/Response Received"
'
'Label148
'
Me.Label148.AutoSize = true
Me.Label148.Location = New System.Drawing.Point(83, 66)
Me.Label148.Name = "Label148"
Me.Label148.Size = New System.Drawing.Size(125, 13)
Me.Label148.TabIndex = 3
Me.Label148.Text = "Was Address Unknown?"
'
'Label149
'
Me.Label149.AutoSize = true
Me.Label149.Location = New System.Drawing.Point(342, 66)
Me.Label149.Name = "Label149"
Me.Label149.Size = New System.Drawing.Size(112, 13)
Me.Label149.TabIndex = 2
Me.Label149.Text = "Initial Letter Re-Mailed"
'
'Label150
'
Me.Label150.AutoSize = true
Me.Label150.Location = New System.Drawing.Point(4, 97)
Me.Label150.Name = "Label150"
Me.Label150.Size = New System.Drawing.Size(196, 13)
Me.Label150.TabIndex = 1
Me.Label150.Text = "Facility Responded with Data Correction"
'
'Label151
'
Me.Label151.AutoSize = true
Me.Label151.Location = New System.Drawing.Point(4, 11)
Me.Label151.Name = "Label151"
Me.Label151.Size = New System.Drawing.Size(95, 13)
Me.Label151.TabIndex = 0
Me.Label151.Text = "Initial Letter Mailed"
'
'Panel56
'
Me.Panel56.Controls.Add(Me.lblAuditType_CY2003)
Me.Panel56.Controls.Add(Me.btnSaveFeeAudit_CY2003)
Me.Panel56.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel56.Location = New System.Drawing.Point(3, 3)
Me.Panel56.Name = "Panel56"
Me.Panel56.Size = New System.Drawing.Size(764, 32)
Me.Panel56.TabIndex = 3
'
'lblAuditType_CY2003
'
Me.lblAuditType_CY2003.AutoSize = true
Me.lblAuditType_CY2003.Location = New System.Drawing.Point(49, 8)
Me.lblAuditType_CY2003.Name = "lblAuditType_CY2003"
Me.lblAuditType_CY2003.Size = New System.Drawing.Size(58, 13)
Me.lblAuditType_CY2003.TabIndex = 4
Me.lblAuditType_CY2003.Text = "Audit Type"
'
'btnSaveFeeAudit_CY2003
'
Me.btnSaveFeeAudit_CY2003.AutoSize = true
Me.btnSaveFeeAudit_CY2003.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveFeeAudit_CY2003.Location = New System.Drawing.Point(3, 3)
Me.btnSaveFeeAudit_CY2003.Name = "btnSaveFeeAudit_CY2003"
Me.btnSaveFeeAudit_CY2003.Size = New System.Drawing.Size(42, 23)
Me.btnSaveFeeAudit_CY2003.TabIndex = 389
Me.btnSaveFeeAudit_CY2003.Text = "Save"
Me.btnSaveFeeAudit_CY2003.UseVisualStyleBackColor = true
'
'TP_Tracking_CY2002
'
Me.TP_Tracking_CY2002.Controls.Add(Me.Panel93)
Me.TP_Tracking_CY2002.Controls.Add(Me.Panel57)
Me.TP_Tracking_CY2002.Location = New System.Drawing.Point(4, 22)
Me.TP_Tracking_CY2002.Name = "TP_Tracking_CY2002"
Me.TP_Tracking_CY2002.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Tracking_CY2002.Size = New System.Drawing.Size(770, 603)
Me.TP_Tracking_CY2002.TabIndex = 7
Me.TP_Tracking_CY2002.Text = "CY2002"
Me.TP_Tracking_CY2002.UseVisualStyleBackColor = true
'
'Panel93
'
Me.Panel93.AutoScroll = true
Me.Panel93.Controls.Add(Me.Label63)
Me.Panel93.Controls.Add(Me.Label64)
Me.Panel93.Controls.Add(Me.Panel94)
Me.Panel93.Controls.Add(Me.Panel95)
Me.Panel93.Controls.Add(Me.txtComments_CY2002)
Me.Panel93.Controls.Add(Me.DTPCloseOut_CY2002)
Me.Panel93.Controls.Add(Me.lblAmountPaid_CY2002)
Me.Panel93.Controls.Add(Me.DTPFeesPaid_CY2002)
Me.Panel93.Controls.Add(Me.DTPAOSent_CY2002)
Me.Panel93.Controls.Add(Me.DTPCOSent_CY2002)
Me.Panel93.Controls.Add(Me.DTPNOVSent_CY2002)
Me.Panel93.Controls.Add(Me.Panel96)
Me.Panel93.Controls.Add(Me.Panel97)
Me.Panel93.Controls.Add(Me.Panel98)
Me.Panel93.Controls.Add(Me.DTPLetterRemailed_CY2002)
Me.Panel93.Controls.Add(Me.Panel99)
Me.Panel93.Controls.Add(Me.DTPLetterReturned_CY2002)
Me.Panel93.Controls.Add(Me.DTPInitialLetter_2002)
Me.Panel93.Controls.Add(Me.Label157)
Me.Panel93.Controls.Add(Me.Label158)
Me.Panel93.Controls.Add(Me.Label159)
Me.Panel93.Controls.Add(Me.Label160)
Me.Panel93.Controls.Add(Me.Label161)
Me.Panel93.Controls.Add(Me.Label162)
Me.Panel93.Controls.Add(Me.Label163)
Me.Panel93.Controls.Add(Me.Label164)
Me.Panel93.Controls.Add(Me.Label165)
Me.Panel93.Controls.Add(Me.Label166)
Me.Panel93.Controls.Add(Me.Label167)
Me.Panel93.Controls.Add(Me.Label168)
Me.Panel93.Controls.Add(Me.Label169)
Me.Panel93.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel93.Location = New System.Drawing.Point(3, 35)
Me.Panel93.Name = "Panel93"
Me.Panel93.Size = New System.Drawing.Size(764, 565)
Me.Panel93.TabIndex = 4
'
'Label63
'
Me.Label63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label63.Location = New System.Drawing.Point(0, 269)
Me.Label63.Name = "Label63"
Me.Label63.Size = New System.Drawing.Size(764, 2)
Me.Label63.TabIndex = 395
'
'Label64
'
Me.Label64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Label64.Location = New System.Drawing.Point(0, 183)
Me.Label64.Name = "Label64"
Me.Label64.Size = New System.Drawing.Size(764, 2)
Me.Label64.TabIndex = 394
'
'Panel94
'
Me.Panel94.Controls.Add(Me.btnManagerSignOff_CY2002)
Me.Panel94.Controls.Add(Me.lblSignOffDat_02)
Me.Panel94.Controls.Add(Me.lblManagerSignOff_02)
Me.Panel94.Location = New System.Drawing.Point(465, 197)
Me.Panel94.Name = "Panel94"
Me.Panel94.Size = New System.Drawing.Size(246, 61)
Me.Panel94.TabIndex = 391
'
'btnManagerSignOff_CY2002
'
Me.btnManagerSignOff_CY2002.AutoSize = true
Me.btnManagerSignOff_CY2002.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnManagerSignOff_CY2002.Location = New System.Drawing.Point(6, 26)
Me.btnManagerSignOff_CY2002.Name = "btnManagerSignOff_CY2002"
Me.btnManagerSignOff_CY2002.Size = New System.Drawing.Size(55, 23)
Me.btnManagerSignOff_CY2002.TabIndex = 399
Me.btnManagerSignOff_CY2002.Text = "Sign-Off"
Me.btnManagerSignOff_CY2002.UseVisualStyleBackColor = true
'
'lblSignOffDat_02
'
Me.lblSignOffDat_02.AutoSize = true
Me.lblSignOffDat_02.Location = New System.Drawing.Point(67, 31)
Me.lblSignOffDat_02.Name = "lblSignOffDat_02"
Me.lblSignOffDat_02.Size = New System.Drawing.Size(39, 13)
Me.lblSignOffDat_02.TabIndex = 2
Me.lblSignOffDat_02.Text = "Date - "
'
'lblManagerSignOff_02
'
Me.lblManagerSignOff_02.AutoSize = true
Me.lblManagerSignOff_02.Location = New System.Drawing.Point(3, 10)
Me.lblManagerSignOff_02.Name = "lblManagerSignOff_02"
Me.lblManagerSignOff_02.Size = New System.Drawing.Size(88, 13)
Me.lblManagerSignOff_02.TabIndex = 1
Me.lblManagerSignOff_02.Text = "Manager Sign-off"
'
'Panel95
'
Me.Panel95.Controls.Add(Me.lblLastModified_02)
Me.Panel95.Controls.Add(Me.lblStaffAssigned_02)
Me.Panel95.Location = New System.Drawing.Point(465, 111)
Me.Panel95.Name = "Panel95"
Me.Panel95.Size = New System.Drawing.Size(246, 61)
Me.Panel95.TabIndex = 390
'
'lblLastModified_02
'
Me.lblLastModified_02.AutoSize = true
Me.lblLastModified_02.Location = New System.Drawing.Point(3, 32)
Me.lblLastModified_02.Name = "lblLastModified_02"
Me.lblLastModified_02.Size = New System.Drawing.Size(70, 13)
Me.lblLastModified_02.TabIndex = 2
Me.lblLastModified_02.Text = "Last Modified"
'
'lblStaffAssigned_02
'
Me.lblStaffAssigned_02.AutoSize = true
Me.lblStaffAssigned_02.Location = New System.Drawing.Point(3, 10)
Me.lblStaffAssigned_02.Name = "lblStaffAssigned_02"
Me.lblStaffAssigned_02.Size = New System.Drawing.Size(95, 13)
Me.lblStaffAssigned_02.TabIndex = 1
Me.lblStaffAssigned_02.Text = "Staff Last Modified"
'
'txtComments_CY2002
'
Me.txtComments_CY2002.Location = New System.Drawing.Point(62, 348)
Me.txtComments_CY2002.Multiline = true
Me.txtComments_CY2002.Name = "txtComments_CY2002"
Me.txtComments_CY2002.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtComments_CY2002.Size = New System.Drawing.Size(383, 64)
Me.txtComments_CY2002.TabIndex = 388
'
'DTPCloseOut_CY2002
'
Me.DTPCloseOut_CY2002.Checked = false
Me.DTPCloseOut_CY2002.CustomFormat = "dd-MMM-yyyy"
Me.DTPCloseOut_CY2002.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCloseOut_CY2002.Location = New System.Drawing.Point(116, 314)
Me.DTPCloseOut_CY2002.Name = "DTPCloseOut_CY2002"
Me.DTPCloseOut_CY2002.ShowCheckBox = true
Me.DTPCloseOut_CY2002.Size = New System.Drawing.Size(102, 20)
Me.DTPCloseOut_CY2002.TabIndex = 387
Me.DTPCloseOut_CY2002.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'lblAmountPaid_CY2002
'
Me.lblAmountPaid_CY2002.AutoSize = true
Me.lblAmountPaid_CY2002.Location = New System.Drawing.Point(202, 279)
Me.lblAmountPaid_CY2002.Name = "lblAmountPaid_CY2002"
Me.lblAmountPaid_CY2002.Size = New System.Drawing.Size(63, 13)
Me.lblAmountPaid_CY2002.TabIndex = 386
Me.lblAmountPaid_CY2002.Text = "Fees Paid: -"
'
'DTPFeesPaid_CY2002
'
Me.DTPFeesPaid_CY2002.Checked = false
Me.DTPFeesPaid_CY2002.CustomFormat = "dd-MMM-yyyy"
Me.DTPFeesPaid_CY2002.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPFeesPaid_CY2002.Location = New System.Drawing.Point(93, 275)
Me.DTPFeesPaid_CY2002.Name = "DTPFeesPaid_CY2002"
Me.DTPFeesPaid_CY2002.ShowCheckBox = true
Me.DTPFeesPaid_CY2002.Size = New System.Drawing.Size(102, 20)
Me.DTPFeesPaid_CY2002.TabIndex = 384
Me.DTPFeesPaid_CY2002.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPAOSent_CY2002
'
Me.DTPAOSent_CY2002.Checked = false
Me.DTPAOSent_CY2002.CustomFormat = "dd-MMM-yyyy"
Me.DTPAOSent_CY2002.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPAOSent_CY2002.Location = New System.Drawing.Point(93, 245)
Me.DTPAOSent_CY2002.Name = "DTPAOSent_CY2002"
Me.DTPAOSent_CY2002.ShowCheckBox = true
Me.DTPAOSent_CY2002.Size = New System.Drawing.Size(102, 20)
Me.DTPAOSent_CY2002.TabIndex = 383
Me.DTPAOSent_CY2002.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPCOSent_CY2002
'
Me.DTPCOSent_CY2002.Checked = false
Me.DTPCOSent_CY2002.CustomFormat = "dd-MMM-yyyy"
Me.DTPCOSent_CY2002.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPCOSent_CY2002.Location = New System.Drawing.Point(93, 219)
Me.DTPCOSent_CY2002.Name = "DTPCOSent_CY2002"
Me.DTPCOSent_CY2002.ShowCheckBox = true
Me.DTPCOSent_CY2002.Size = New System.Drawing.Size(102, 20)
Me.DTPCOSent_CY2002.TabIndex = 382
Me.DTPCOSent_CY2002.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPNOVSent_CY2002
'
Me.DTPNOVSent_CY2002.Checked = false
Me.DTPNOVSent_CY2002.CustomFormat = "dd-MMM-yyyy"
Me.DTPNOVSent_CY2002.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPNOVSent_CY2002.Location = New System.Drawing.Point(93, 193)
Me.DTPNOVSent_CY2002.Name = "DTPNOVSent_CY2002"
Me.DTPNOVSent_CY2002.ShowCheckBox = true
Me.DTPNOVSent_CY2002.Size = New System.Drawing.Size(102, 20)
Me.DTPNOVSent_CY2002.TabIndex = 381
Me.DTPNOVSent_CY2002.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel96
'
Me.Panel96.AutoSize = true
Me.Panel96.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel96.Controls.Add(Me.rdbUnabletoContactNo_CY2002)
Me.Panel96.Controls.Add(Me.rdbUnabletoContactYes_CY2002)
Me.Panel96.Location = New System.Drawing.Point(276, 154)
Me.Panel96.Name = "Panel96"
Me.Panel96.Size = New System.Drawing.Size(94, 23)
Me.Panel96.TabIndex = 380
'
'rdbUnabletoContactNo_CY2002
'
Me.rdbUnabletoContactNo_CY2002.AutoSize = true
Me.rdbUnabletoContactNo_CY2002.Location = New System.Drawing.Point(50, 3)
Me.rdbUnabletoContactNo_CY2002.Name = "rdbUnabletoContactNo_CY2002"
Me.rdbUnabletoContactNo_CY2002.Size = New System.Drawing.Size(41, 17)
Me.rdbUnabletoContactNo_CY2002.TabIndex = 1
Me.rdbUnabletoContactNo_CY2002.TabStop = true
Me.rdbUnabletoContactNo_CY2002.Text = "NO"
Me.rdbUnabletoContactNo_CY2002.UseVisualStyleBackColor = true
'
'rdbUnabletoContactYes_CY2002
'
Me.rdbUnabletoContactYes_CY2002.AutoSize = true
Me.rdbUnabletoContactYes_CY2002.Location = New System.Drawing.Point(3, 3)
Me.rdbUnabletoContactYes_CY2002.Name = "rdbUnabletoContactYes_CY2002"
Me.rdbUnabletoContactYes_CY2002.Size = New System.Drawing.Size(46, 17)
Me.rdbUnabletoContactYes_CY2002.TabIndex = 0
Me.rdbUnabletoContactYes_CY2002.TabStop = true
Me.rdbUnabletoContactYes_CY2002.Text = "YES"
Me.rdbUnabletoContactYes_CY2002.UseVisualStyleBackColor = true
'
'Panel97
'
Me.Panel97.AutoSize = true
Me.Panel97.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel97.Controls.Add(Me.rdbBankruptcyNo_CY2002)
Me.Panel97.Controls.Add(Me.rdbBankruptcyYes_CY2002)
Me.Panel97.Location = New System.Drawing.Point(84, 128)
Me.Panel97.Name = "Panel97"
Me.Panel97.Size = New System.Drawing.Size(94, 23)
Me.Panel97.TabIndex = 379
'
'rdbBankruptcyNo_CY2002
'
Me.rdbBankruptcyNo_CY2002.AutoSize = true
Me.rdbBankruptcyNo_CY2002.Location = New System.Drawing.Point(50, 3)
Me.rdbBankruptcyNo_CY2002.Name = "rdbBankruptcyNo_CY2002"
Me.rdbBankruptcyNo_CY2002.Size = New System.Drawing.Size(41, 17)
Me.rdbBankruptcyNo_CY2002.TabIndex = 1
Me.rdbBankruptcyNo_CY2002.TabStop = true
Me.rdbBankruptcyNo_CY2002.Text = "NO"
Me.rdbBankruptcyNo_CY2002.UseVisualStyleBackColor = true
'
'rdbBankruptcyYes_CY2002
'
Me.rdbBankruptcyYes_CY2002.AutoSize = true
Me.rdbBankruptcyYes_CY2002.Location = New System.Drawing.Point(3, 3)
Me.rdbBankruptcyYes_CY2002.Name = "rdbBankruptcyYes_CY2002"
Me.rdbBankruptcyYes_CY2002.Size = New System.Drawing.Size(46, 17)
Me.rdbBankruptcyYes_CY2002.TabIndex = 0
Me.rdbBankruptcyYes_CY2002.TabStop = true
Me.rdbBankruptcyYes_CY2002.Text = "YES"
Me.rdbBankruptcyYes_CY2002.UseVisualStyleBackColor = true
'
'Panel98
'
Me.Panel98.AutoSize = true
Me.Panel98.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel98.Controls.Add(Me.rdbDataCorrectNo_CY2002)
Me.Panel98.Controls.Add(Me.rdbDataCorrectYes_CY2002)
Me.Panel98.Location = New System.Drawing.Point(214, 92)
Me.Panel98.Name = "Panel98"
Me.Panel98.Size = New System.Drawing.Size(94, 23)
Me.Panel98.TabIndex = 378
'
'rdbDataCorrectNo_CY2002
'
Me.rdbDataCorrectNo_CY2002.AutoSize = true
Me.rdbDataCorrectNo_CY2002.Location = New System.Drawing.Point(50, 3)
Me.rdbDataCorrectNo_CY2002.Name = "rdbDataCorrectNo_CY2002"
Me.rdbDataCorrectNo_CY2002.Size = New System.Drawing.Size(41, 17)
Me.rdbDataCorrectNo_CY2002.TabIndex = 1
Me.rdbDataCorrectNo_CY2002.TabStop = true
Me.rdbDataCorrectNo_CY2002.Text = "NO"
Me.rdbDataCorrectNo_CY2002.UseVisualStyleBackColor = true
'
'rdbDataCorrectYes_CY2002
'
Me.rdbDataCorrectYes_CY2002.AutoSize = true
Me.rdbDataCorrectYes_CY2002.Location = New System.Drawing.Point(3, 3)
Me.rdbDataCorrectYes_CY2002.Name = "rdbDataCorrectYes_CY2002"
Me.rdbDataCorrectYes_CY2002.Size = New System.Drawing.Size(46, 17)
Me.rdbDataCorrectYes_CY2002.TabIndex = 0
Me.rdbDataCorrectYes_CY2002.TabStop = true
Me.rdbDataCorrectYes_CY2002.Text = "YES"
Me.rdbDataCorrectYes_CY2002.UseVisualStyleBackColor = true
'
'DTPLetterRemailed_CY2002
'
Me.DTPLetterRemailed_CY2002.Checked = false
Me.DTPLetterRemailed_CY2002.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterRemailed_CY2002.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterRemailed_CY2002.Location = New System.Drawing.Point(465, 64)
Me.DTPLetterRemailed_CY2002.Name = "DTPLetterRemailed_CY2002"
Me.DTPLetterRemailed_CY2002.ShowCheckBox = true
Me.DTPLetterRemailed_CY2002.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterRemailed_CY2002.TabIndex = 377
Me.DTPLetterRemailed_CY2002.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Panel99
'
Me.Panel99.AutoSize = true
Me.Panel99.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel99.Controls.Add(Me.rdbAddressUnknownNo_CY2002)
Me.Panel99.Controls.Add(Me.rdbAddressUnknownYes_CY2002)
Me.Panel99.Location = New System.Drawing.Point(214, 63)
Me.Panel99.Name = "Panel99"
Me.Panel99.Size = New System.Drawing.Size(94, 23)
Me.Panel99.TabIndex = 376
'
'rdbAddressUnknownNo_CY2002
'
Me.rdbAddressUnknownNo_CY2002.AutoSize = true
Me.rdbAddressUnknownNo_CY2002.Location = New System.Drawing.Point(50, 3)
Me.rdbAddressUnknownNo_CY2002.Name = "rdbAddressUnknownNo_CY2002"
Me.rdbAddressUnknownNo_CY2002.Size = New System.Drawing.Size(41, 17)
Me.rdbAddressUnknownNo_CY2002.TabIndex = 1
Me.rdbAddressUnknownNo_CY2002.TabStop = true
Me.rdbAddressUnknownNo_CY2002.Text = "NO"
Me.rdbAddressUnknownNo_CY2002.UseVisualStyleBackColor = true
'
'rdbAddressUnknownYes_CY2002
'
Me.rdbAddressUnknownYes_CY2002.AutoSize = true
Me.rdbAddressUnknownYes_CY2002.Location = New System.Drawing.Point(3, 3)
Me.rdbAddressUnknownYes_CY2002.Name = "rdbAddressUnknownYes_CY2002"
Me.rdbAddressUnknownYes_CY2002.Size = New System.Drawing.Size(46, 17)
Me.rdbAddressUnknownYes_CY2002.TabIndex = 0
Me.rdbAddressUnknownYes_CY2002.TabStop = true
Me.rdbAddressUnknownYes_CY2002.Text = "YES"
Me.rdbAddressUnknownYes_CY2002.UseVisualStyleBackColor = true
'
'DTPLetterReturned_CY2002
'
Me.DTPLetterReturned_CY2002.Checked = false
Me.DTPLetterReturned_CY2002.CustomFormat = "dd-MMM-yyyy"
Me.DTPLetterReturned_CY2002.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPLetterReturned_CY2002.Location = New System.Drawing.Point(193, 33)
Me.DTPLetterReturned_CY2002.Name = "DTPLetterReturned_CY2002"
Me.DTPLetterReturned_CY2002.ShowCheckBox = true
Me.DTPLetterReturned_CY2002.Size = New System.Drawing.Size(102, 20)
Me.DTPLetterReturned_CY2002.TabIndex = 375
Me.DTPLetterReturned_CY2002.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'DTPInitialLetter_2002
'
Me.DTPInitialLetter_2002.Checked = false
Me.DTPInitialLetter_2002.CustomFormat = "dd-MMM-yyyy"
Me.DTPInitialLetter_2002.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.DTPInitialLetter_2002.Location = New System.Drawing.Point(98, 7)
Me.DTPInitialLetter_2002.Name = "DTPInitialLetter_2002"
Me.DTPInitialLetter_2002.ShowCheckBox = true
Me.DTPInitialLetter_2002.Size = New System.Drawing.Size(102, 20)
Me.DTPInitialLetter_2002.TabIndex = 374
Me.DTPInitialLetter_2002.Value = New Date(2010, 2, 25, 0, 0, 0, 0)
'
'Label157
'
Me.Label157.AutoSize = true
Me.Label157.Location = New System.Drawing.Point(4, 348)
Me.Label157.Name = "Label157"
Me.Label157.Size = New System.Drawing.Size(56, 13)
Me.Label157.TabIndex = 12
Me.Label157.Text = "Comments"
'
'Label158
'
Me.Label158.AutoSize = true
Me.Label158.Location = New System.Drawing.Point(4, 318)
Me.Label158.Name = "Label158"
Me.Label158.Size = New System.Drawing.Size(101, 13)
Me.Label158.TabIndex = 11
Me.Label158.Text = "Close Out Fee Audit"
'
'Label159
'
Me.Label159.AutoSize = true
Me.Label159.Location = New System.Drawing.Point(4, 223)
Me.Label159.Name = "Label159"
Me.Label159.Size = New System.Drawing.Size(77, 13)
Me.Label159.TabIndex = 10
Me.Label159.Text = "CO Letter Sent"
'
'Label160
'
Me.Label160.AutoSize = true
Me.Label160.Location = New System.Drawing.Point(4, 279)
Me.Label160.Name = "Label160"
Me.Label160.Size = New System.Drawing.Size(87, 13)
Me.Label160.TabIndex = 9
Me.Label160.Text = "Facilty Paid Fees"
'
'Label161
'
Me.Label161.AutoSize = true
Me.Label161.Location = New System.Drawing.Point(4, 249)
Me.Label161.Name = "Label161"
Me.Label161.Size = New System.Drawing.Size(77, 13)
Me.Label161.TabIndex = 8
Me.Label161.Text = "AO Letter Sent"
'
'Label162
'
Me.Label162.AutoSize = true
Me.Label162.Location = New System.Drawing.Point(4, 133)
Me.Label162.Name = "Label162"
Me.Label162.Size = New System.Drawing.Size(61, 13)
Me.Label162.TabIndex = 7
Me.Label162.Text = "Bankruptcy"
'
'Label163
'
Me.Label163.AutoSize = true
Me.Label163.Location = New System.Drawing.Point(4, 159)
Me.Label163.Name = "Label163"
Me.Label163.Size = New System.Drawing.Size(252, 13)
Me.Label163.TabIndex = 6
Me.Label163.Text = "Unable to Contact Facility Or Facility Representative"
'
'Label164
'
Me.Label164.AutoSize = true
Me.Label164.Location = New System.Drawing.Point(4, 197)
Me.Label164.Name = "Label164"
Me.Label164.Size = New System.Drawing.Size(85, 13)
Me.Label164.TabIndex = 5
Me.Label164.Text = "NOV Letter Sent"
'
'Label165
'
Me.Label165.AutoSize = true
Me.Label165.Location = New System.Drawing.Point(4, 37)
Me.Label165.Name = "Label165"
Me.Label165.Size = New System.Drawing.Size(183, 13)
Me.Label165.TabIndex = 4
Me.Label165.Text = "Letter Returned/Response Received"
'
'Label166
'
Me.Label166.AutoSize = true
Me.Label166.Location = New System.Drawing.Point(83, 68)
Me.Label166.Name = "Label166"
Me.Label166.Size = New System.Drawing.Size(125, 13)
Me.Label166.TabIndex = 3
Me.Label166.Text = "Was Address Unknown?"
'
'Label167
'
Me.Label167.AutoSize = true
Me.Label167.Location = New System.Drawing.Point(342, 68)
Me.Label167.Name = "Label167"
Me.Label167.Size = New System.Drawing.Size(112, 13)
Me.Label167.TabIndex = 2
Me.Label167.Text = "Initial Letter Re-Mailed"
'
'Label168
'
Me.Label168.AutoSize = true
Me.Label168.Location = New System.Drawing.Point(4, 97)
Me.Label168.Name = "Label168"
Me.Label168.Size = New System.Drawing.Size(196, 13)
Me.Label168.TabIndex = 1
Me.Label168.Text = "Facility Responded with Data Correction"
'
'Label169
'
Me.Label169.AutoSize = true
Me.Label169.Location = New System.Drawing.Point(4, 11)
Me.Label169.Name = "Label169"
Me.Label169.Size = New System.Drawing.Size(95, 13)
Me.Label169.TabIndex = 0
Me.Label169.Text = "Initial Letter Mailed"
'
'Panel57
'
Me.Panel57.Controls.Add(Me.lblAuditType_CY2002)
Me.Panel57.Controls.Add(Me.btnSaveFeeAudit_CY2002)
Me.Panel57.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel57.Location = New System.Drawing.Point(3, 3)
Me.Panel57.Name = "Panel57"
Me.Panel57.Size = New System.Drawing.Size(764, 32)
Me.Panel57.TabIndex = 3
'
'lblAuditType_CY2002
'
Me.lblAuditType_CY2002.AutoSize = true
Me.lblAuditType_CY2002.Location = New System.Drawing.Point(49, 8)
Me.lblAuditType_CY2002.Name = "lblAuditType_CY2002"
Me.lblAuditType_CY2002.Size = New System.Drawing.Size(58, 13)
Me.lblAuditType_CY2002.TabIndex = 4
Me.lblAuditType_CY2002.Text = "Audit Type"
'
'btnSaveFeeAudit_CY2002
'
Me.btnSaveFeeAudit_CY2002.AutoSize = true
Me.btnSaveFeeAudit_CY2002.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveFeeAudit_CY2002.Location = New System.Drawing.Point(3, 3)
Me.btnSaveFeeAudit_CY2002.Name = "btnSaveFeeAudit_CY2002"
Me.btnSaveFeeAudit_CY2002.Size = New System.Drawing.Size(42, 23)
Me.btnSaveFeeAudit_CY2002.TabIndex = 389
Me.btnSaveFeeAudit_CY2002.Text = "Save"
Me.btnSaveFeeAudit_CY2002.UseVisualStyleBackColor = true
'
'TP_Tracking_OtherComments
'
Me.TP_Tracking_OtherComments.Controls.Add(Me.btnSaveAuditComments)
Me.TP_Tracking_OtherComments.Controls.Add(Me.txtAuditComments)
Me.TP_Tracking_OtherComments.Controls.Add(Me.Label35)
Me.TP_Tracking_OtherComments.Location = New System.Drawing.Point(4, 22)
Me.TP_Tracking_OtherComments.Name = "TP_Tracking_OtherComments"
Me.TP_Tracking_OtherComments.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Tracking_OtherComments.Size = New System.Drawing.Size(770, 603)
Me.TP_Tracking_OtherComments.TabIndex = 3
Me.TP_Tracking_OtherComments.Text = "Other Comments"
Me.TP_Tracking_OtherComments.UseVisualStyleBackColor = true
'
'btnSaveAuditComments
'
Me.btnSaveAuditComments.AutoSize = true
Me.btnSaveAuditComments.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveAuditComments.Location = New System.Drawing.Point(28, 201)
Me.btnSaveAuditComments.Name = "btnSaveAuditComments"
Me.btnSaveAuditComments.Size = New System.Drawing.Size(42, 23)
Me.btnSaveAuditComments.TabIndex = 392
Me.btnSaveAuditComments.Text = "Save"
Me.btnSaveAuditComments.UseVisualStyleBackColor = true
'
'txtAuditComments
'
Me.txtAuditComments.Location = New System.Drawing.Point(28, 25)
Me.txtAuditComments.Multiline = true
Me.txtAuditComments.Name = "txtAuditComments"
Me.txtAuditComments.Size = New System.Drawing.Size(538, 170)
Me.txtAuditComments.TabIndex = 391
'
'Label35
'
Me.Label35.AutoSize = true
Me.Label35.Location = New System.Drawing.Point(6, 9)
Me.Label35.Name = "Label35"
Me.Label35.Size = New System.Drawing.Size(105, 13)
Me.Label35.TabIndex = 390
Me.Label35.Text = "Additional Comments"
'
'TPNonResponders
'
Me.TPNonResponders.Controls.Add(Me.Panel1)
Me.TPNonResponders.Location = New System.Drawing.Point(4, 22)
Me.TPNonResponders.Name = "TPNonResponders"
Me.TPNonResponders.Padding = New System.Windows.Forms.Padding(3)
Me.TPNonResponders.Size = New System.Drawing.Size(784, 635)
Me.TPNonResponders.TabIndex = 0
Me.TPNonResponders.Text = "Non-Responders"
Me.TPNonResponders.UseVisualStyleBackColor = true
'
'Panel1
'
Me.Panel1.AutoScroll = true
Me.Panel1.Controls.Add(Me.TCNonRespondersData)
Me.Panel1.Controls.Add(Me.gbTopData)
Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel1.Location = New System.Drawing.Point(3, 3)
Me.Panel1.Name = "Panel1"
Me.Panel1.Size = New System.Drawing.Size(778, 629)
Me.Panel1.TabIndex = 2
'
'TCNonRespondersData
'
Me.TCNonRespondersData.Controls.Add(Me.TP_CY2008)
Me.TCNonRespondersData.Controls.Add(Me.TP_CY2007)
Me.TCNonRespondersData.Controls.Add(Me.TP_CY2006)
Me.TCNonRespondersData.Controls.Add(Me.TP_Change_Questions)
Me.TCNonRespondersData.Controls.Add(Me.TP_Comments)
Me.TCNonRespondersData.Dock = System.Windows.Forms.DockStyle.Fill
Me.TCNonRespondersData.Location = New System.Drawing.Point(0, 425)
Me.TCNonRespondersData.Name = "TCNonRespondersData"
Me.TCNonRespondersData.SelectedIndex = 0
Me.TCNonRespondersData.Size = New System.Drawing.Size(778, 204)
Me.TCNonRespondersData.TabIndex = 1
'
'TP_CY2008
'
Me.TP_CY2008.AutoScroll = true
Me.TP_CY2008.Controls.Add(Me.Panel25)
Me.TP_CY2008.Location = New System.Drawing.Point(4, 22)
Me.TP_CY2008.Name = "TP_CY2008"
Me.TP_CY2008.Padding = New System.Windows.Forms.Padding(3)
Me.TP_CY2008.Size = New System.Drawing.Size(770, 178)
Me.TP_CY2008.TabIndex = 0
Me.TP_CY2008.Text = "CY2008"
Me.TP_CY2008.UseVisualStyleBackColor = true
'
'Panel25
'
Me.Panel25.AutoScroll = true
Me.Panel25.Controls.Add(Me.gbCY2008)
Me.Panel25.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel25.Location = New System.Drawing.Point(3, 3)
Me.Panel25.Name = "Panel25"
Me.Panel25.Size = New System.Drawing.Size(764, 172)
Me.Panel25.TabIndex = 3
'
'gbCY2008
'
Me.gbCY2008.AutoSize = true
Me.gbCY2008.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.gbCY2008.Controls.Add(Me.Panel24)
Me.gbCY2008.Controls.Add(Me.Panel19)
Me.gbCY2008.Controls.Add(Me.Panel16)
Me.gbCY2008.Dock = System.Windows.Forms.DockStyle.Top
Me.gbCY2008.Location = New System.Drawing.Point(0, 0)
Me.gbCY2008.Name = "gbCY2008"
Me.gbCY2008.Size = New System.Drawing.Size(747, 404)
Me.gbCY2008.TabIndex = 2
Me.gbCY2008.TabStop = false
Me.gbCY2008.Text = "CY2008 Fee Mailout Information - due on Sept. 1, 2009"
'
'Panel24
'
Me.Panel24.Controls.Add(Me.btnSaveCurrentChange_CY2008)
Me.Panel24.Controls.Add(Me.txtCurrentComments_CY2008)
Me.Panel24.Controls.Add(Me.Label32)
Me.Panel24.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel24.Location = New System.Drawing.Point(3, 306)
Me.Panel24.Name = "Panel24"
Me.Panel24.Size = New System.Drawing.Size(741, 95)
Me.Panel24.TabIndex = 73
Me.Panel24.Visible = false
'
'btnSaveCurrentChange_CY2008
'
Me.btnSaveCurrentChange_CY2008.AutoSize = true
Me.btnSaveCurrentChange_CY2008.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveCurrentChange_CY2008.Location = New System.Drawing.Point(509, 19)
Me.btnSaveCurrentChange_CY2008.Name = "btnSaveCurrentChange_CY2008"
Me.btnSaveCurrentChange_CY2008.Size = New System.Drawing.Size(94, 23)
Me.btnSaveCurrentChange_CY2008.TabIndex = 7
Me.btnSaveCurrentChange_CY2008.Text = "Save Comments"
Me.btnSaveCurrentChange_CY2008.UseVisualStyleBackColor = true
'
'txtCurrentComments_CY2008
'
Me.txtCurrentComments_CY2008.Location = New System.Drawing.Point(10, 19)
Me.txtCurrentComments_CY2008.Multiline = true
Me.txtCurrentComments_CY2008.Name = "txtCurrentComments_CY2008"
Me.txtCurrentComments_CY2008.Size = New System.Drawing.Size(493, 64)
Me.txtCurrentComments_CY2008.TabIndex = 6
'
'Label32
'
Me.Label32.AutoSize = true
Me.Label32.Location = New System.Drawing.Point(3, 3)
Me.Label32.Name = "Label32"
Me.Label32.Size = New System.Drawing.Size(231, 13)
Me.Label32.TabIndex = 5
Me.Label32.Text = "Comments for Current Facility and Contact Info: "
'
'Panel19
'
Me.Panel19.Controls.Add(Me.Panel23)
Me.Panel19.Controls.Add(Me.Panel20)
Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel19.Location = New System.Drawing.Point(3, 141)
Me.Panel19.Name = "Panel19"
Me.Panel19.Size = New System.Drawing.Size(741, 165)
Me.Panel19.TabIndex = 1
'
'Panel23
'
Me.Panel23.Controls.Add(Me.txtEditContactState_CY2008)
Me.Panel23.Controls.Add(Me.btnEditContactInfo_CY2008)
Me.Panel23.Controls.Add(Me.mtbEditContactZipCode_CY2008)
Me.Panel23.Controls.Add(Me.txtEditContactCity_CY2008)
Me.Panel23.Controls.Add(Me.txtEditContactAddress_CY2008)
Me.Panel23.Controls.Add(Me.txtEditContactCompany_CY2008)
Me.Panel23.Controls.Add(Me.txtEditContactLastName_CY2008)
Me.Panel23.Controls.Add(Me.txtEditContactFirstName_CY2008)
Me.Panel23.Controls.Add(Me.Label31)
Me.Panel23.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel23.Location = New System.Drawing.Point(343, 0)
Me.Panel23.Name = "Panel23"
Me.Panel23.Size = New System.Drawing.Size(398, 165)
Me.Panel23.TabIndex = 72
'
'txtEditContactState_CY2008
'
Me.txtEditContactState_CY2008.Location = New System.Drawing.Point(146, 113)
Me.txtEditContactState_CY2008.MaxLength = 2
Me.txtEditContactState_CY2008.Name = "txtEditContactState_CY2008"
Me.txtEditContactState_CY2008.Size = New System.Drawing.Size(25, 20)
Me.txtEditContactState_CY2008.TabIndex = 6
'
'btnEditContactInfo_CY2008
'
Me.btnEditContactInfo_CY2008.AutoSize = true
Me.btnEditContactInfo_CY2008.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnEditContactInfo_CY2008.Location = New System.Drawing.Point(247, 16)
Me.btnEditContactInfo_CY2008.Name = "btnEditContactInfo_CY2008"
Me.btnEditContactInfo_CY2008.Size = New System.Drawing.Size(106, 23)
Me.btnEditContactInfo_CY2008.TabIndex = 9
Me.btnEditContactInfo_CY2008.Text = "Save Contact Info."
Me.btnEditContactInfo_CY2008.UseVisualStyleBackColor = true
'
'mtbEditContactZipCode_CY2008
'
Me.mtbEditContactZipCode_CY2008.Location = New System.Drawing.Point(174, 113)
Me.mtbEditContactZipCode_CY2008.Mask = "00000-9999"
Me.mtbEditContactZipCode_CY2008.Name = "mtbEditContactZipCode_CY2008"
Me.mtbEditContactZipCode_CY2008.Size = New System.Drawing.Size(67, 20)
Me.mtbEditContactZipCode_CY2008.TabIndex = 7
Me.mtbEditContactZipCode_CY2008.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'txtEditContactCity_CY2008
'
Me.txtEditContactCity_CY2008.Location = New System.Drawing.Point(15, 113)
Me.txtEditContactCity_CY2008.Name = "txtEditContactCity_CY2008"
Me.txtEditContactCity_CY2008.Size = New System.Drawing.Size(125, 20)
Me.txtEditContactCity_CY2008.TabIndex = 5
Me.txtEditContactCity_CY2008.Text = "Contact City"
'
'txtEditContactAddress_CY2008
'
Me.txtEditContactAddress_CY2008.Location = New System.Drawing.Point(15, 89)
Me.txtEditContactAddress_CY2008.Name = "txtEditContactAddress_CY2008"
Me.txtEditContactAddress_CY2008.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactAddress_CY2008.TabIndex = 4
Me.txtEditContactAddress_CY2008.Text = "Contact Address"
'
'txtEditContactCompany_CY2008
'
Me.txtEditContactCompany_CY2008.Location = New System.Drawing.Point(15, 65)
Me.txtEditContactCompany_CY2008.Name = "txtEditContactCompany_CY2008"
Me.txtEditContactCompany_CY2008.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactCompany_CY2008.TabIndex = 3
Me.txtEditContactCompany_CY2008.Text = "Contact Company"
'
'txtEditContactLastName_CY2008
'
Me.txtEditContactLastName_CY2008.Location = New System.Drawing.Point(130, 19)
Me.txtEditContactLastName_CY2008.Name = "txtEditContactLastName_CY2008"
Me.txtEditContactLastName_CY2008.Size = New System.Drawing.Size(111, 20)
Me.txtEditContactLastName_CY2008.TabIndex = 1
Me.txtEditContactLastName_CY2008.Text = "Contact Last Name"
'
'txtEditContactFirstName_CY2008
'
Me.txtEditContactFirstName_CY2008.Location = New System.Drawing.Point(15, 19)
Me.txtEditContactFirstName_CY2008.Name = "txtEditContactFirstName_CY2008"
Me.txtEditContactFirstName_CY2008.Size = New System.Drawing.Size(109, 20)
Me.txtEditContactFirstName_CY2008.TabIndex = 0
Me.txtEditContactFirstName_CY2008.Text = "Contact First Name"
'
'Label31
'
Me.Label31.AutoSize = true
Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label31.Location = New System.Drawing.Point(3, 3)
Me.Label31.Name = "Label31"
Me.Label31.Size = New System.Drawing.Size(118, 13)
Me.Label31.TabIndex = 1
Me.Label31.Text = "Contact Information"
'
'Panel20
'
Me.Panel20.Controls.Add(Me.txtEditSourceClass_CY2008)
Me.Panel20.Controls.Add(Me.cboOperatingStatus_CY2008)
Me.Panel20.Controls.Add(Me.Panel21)
Me.Panel20.Controls.Add(Me.Panel22)
Me.Panel20.Controls.Add(Me.Label25)
Me.Panel20.Controls.Add(Me.Label26)
Me.Panel20.Controls.Add(Me.Label27)
Me.Panel20.Controls.Add(Me.Label28)
Me.Panel20.Controls.Add(Me.btnEditFacilityInfo_CY2008)
Me.Panel20.Controls.Add(Me.mtbEditZipCode_CY2008)
Me.Panel20.Controls.Add(Me.Label29)
Me.Panel20.Controls.Add(Me.txtEditFacilityCity_CY2008)
Me.Panel20.Controls.Add(Me.txtEditFacilityAddress_CY2008)
Me.Panel20.Controls.Add(Me.txtEditFacilityName_CY2008)
Me.Panel20.Controls.Add(Me.Label30)
Me.Panel20.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel20.Location = New System.Drawing.Point(0, 0)
Me.Panel20.Name = "Panel20"
Me.Panel20.Size = New System.Drawing.Size(343, 165)
Me.Panel20.TabIndex = 71
'
'txtEditSourceClass_CY2008
'
Me.txtEditSourceClass_CY2008.Location = New System.Drawing.Point(85, 120)
Me.txtEditSourceClass_CY2008.Name = "txtEditSourceClass_CY2008"
Me.txtEditSourceClass_CY2008.Size = New System.Drawing.Size(50, 20)
Me.txtEditSourceClass_CY2008.TabIndex = 19
'
'cboOperatingStatus_CY2008
'
Me.cboOperatingStatus_CY2008.FormattingEnabled = true
Me.cboOperatingStatus_CY2008.Location = New System.Drawing.Point(72, 93)
Me.cboOperatingStatus_CY2008.Name = "cboOperatingStatus_CY2008"
Me.cboOperatingStatus_CY2008.Size = New System.Drawing.Size(133, 21)
Me.cboOperatingStatus_CY2008.TabIndex = 17
'
'Panel21
'
Me.Panel21.AutoSize = true
Me.Panel21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel21.Controls.Add(Me.rdbNSPSNo_CY2008)
Me.Panel21.Controls.Add(Me.rdbNSPSYes_CY2008)
Me.Panel21.Location = New System.Drawing.Point(246, 119)
Me.Panel21.Name = "Panel21"
Me.Panel21.Size = New System.Drawing.Size(94, 23)
Me.Panel21.TabIndex = 15
'
'rdbNSPSNo_CY2008
'
Me.rdbNSPSNo_CY2008.AutoSize = true
Me.rdbNSPSNo_CY2008.Location = New System.Drawing.Point(50, 3)
Me.rdbNSPSNo_CY2008.Name = "rdbNSPSNo_CY2008"
Me.rdbNSPSNo_CY2008.Size = New System.Drawing.Size(41, 17)
Me.rdbNSPSNo_CY2008.TabIndex = 1
Me.rdbNSPSNo_CY2008.TabStop = true
Me.rdbNSPSNo_CY2008.Text = "NO"
Me.rdbNSPSNo_CY2008.UseVisualStyleBackColor = true
'
'rdbNSPSYes_CY2008
'
Me.rdbNSPSYes_CY2008.AutoSize = true
Me.rdbNSPSYes_CY2008.Location = New System.Drawing.Point(3, 3)
Me.rdbNSPSYes_CY2008.Name = "rdbNSPSYes_CY2008"
Me.rdbNSPSYes_CY2008.Size = New System.Drawing.Size(46, 17)
Me.rdbNSPSYes_CY2008.TabIndex = 0
Me.rdbNSPSYes_CY2008.TabStop = true
Me.rdbNSPSYes_CY2008.Text = "YES"
Me.rdbNSPSYes_CY2008.UseVisualStyleBackColor = true
'
'Panel22
'
Me.Panel22.AutoSize = true
Me.Panel22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel22.Controls.Add(Me.rdbTVNo_CY2008)
Me.Panel22.Controls.Add(Me.rdbTVYes_CY2008)
Me.Panel22.Location = New System.Drawing.Point(246, 92)
Me.Panel22.Name = "Panel22"
Me.Panel22.Size = New System.Drawing.Size(94, 23)
Me.Panel22.TabIndex = 14
'
'rdbTVNo_CY2008
'
Me.rdbTVNo_CY2008.AutoSize = true
Me.rdbTVNo_CY2008.Location = New System.Drawing.Point(50, 3)
Me.rdbTVNo_CY2008.Name = "rdbTVNo_CY2008"
Me.rdbTVNo_CY2008.Size = New System.Drawing.Size(41, 17)
Me.rdbTVNo_CY2008.TabIndex = 1
Me.rdbTVNo_CY2008.TabStop = true
Me.rdbTVNo_CY2008.Text = "NO"
Me.rdbTVNo_CY2008.UseVisualStyleBackColor = true
'
'rdbTVYes_CY2008
'
Me.rdbTVYes_CY2008.AutoSize = true
Me.rdbTVYes_CY2008.Location = New System.Drawing.Point(3, 3)
Me.rdbTVYes_CY2008.Name = "rdbTVYes_CY2008"
Me.rdbTVYes_CY2008.Size = New System.Drawing.Size(46, 17)
Me.rdbTVYes_CY2008.TabIndex = 0
Me.rdbTVYes_CY2008.TabStop = true
Me.rdbTVYes_CY2008.Text = "YES"
Me.rdbTVYes_CY2008.UseVisualStyleBackColor = true
'
'Label25
'
Me.Label25.AutoSize = true
Me.Label25.Location = New System.Drawing.Point(204, 124)
Me.Label25.Name = "Label25"
Me.Label25.Size = New System.Drawing.Size(36, 13)
Me.Label25.TabIndex = 13
Me.Label25.Text = "NSPS"
'
'Label26
'
Me.Label26.AutoSize = true
Me.Label26.Location = New System.Drawing.Point(206, 97)
Me.Label26.Name = "Label26"
Me.Label26.Size = New System.Drawing.Size(37, 13)
Me.Label26.TabIndex = 12
Me.Label26.Text = "Title V"
'
'Label27
'
Me.Label27.AutoSize = true
Me.Label27.Location = New System.Drawing.Point(17, 123)
Me.Label27.Name = "Label27"
Me.Label27.Size = New System.Drawing.Size(69, 13)
Me.Label27.TabIndex = 11
Me.Label27.Text = "Source Class"
'
'Label28
'
Me.Label28.AutoSize = true
Me.Label28.Location = New System.Drawing.Point(17, 96)
Me.Label28.Name = "Label28"
Me.Label28.Size = New System.Drawing.Size(54, 13)
Me.Label28.TabIndex = 10
Me.Label28.Text = "Op Status"
'
'btnEditFacilityInfo_CY2008
'
Me.btnEditFacilityInfo_CY2008.AutoSize = true
Me.btnEditFacilityInfo_CY2008.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnEditFacilityInfo_CY2008.Location = New System.Drawing.Point(211, 19)
Me.btnEditFacilityInfo_CY2008.Name = "btnEditFacilityInfo_CY2008"
Me.btnEditFacilityInfo_CY2008.Size = New System.Drawing.Size(98, 23)
Me.btnEditFacilityInfo_CY2008.TabIndex = 4
Me.btnEditFacilityInfo_CY2008.Text = "Save Facility Info"
Me.btnEditFacilityInfo_CY2008.UseVisualStyleBackColor = true
'
'mtbEditZipCode_CY2008
'
Me.mtbEditZipCode_CY2008.Location = New System.Drawing.Point(138, 65)
Me.mtbEditZipCode_CY2008.Mask = "00000-9999"
Me.mtbEditZipCode_CY2008.Name = "mtbEditZipCode_CY2008"
Me.mtbEditZipCode_CY2008.Size = New System.Drawing.Size(67, 20)
Me.mtbEditZipCode_CY2008.TabIndex = 3
Me.mtbEditZipCode_CY2008.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'Label29
'
Me.Label29.AutoSize = true
Me.Label29.Location = New System.Drawing.Point(113, 69)
Me.Label29.Name = "Label29"
Me.Label29.Size = New System.Drawing.Size(22, 13)
Me.Label29.TabIndex = 6
Me.Label29.Text = "GA"
'
'txtEditFacilityCity_CY2008
'
Me.txtEditFacilityCity_CY2008.Location = New System.Drawing.Point(15, 65)
Me.txtEditFacilityCity_CY2008.Name = "txtEditFacilityCity_CY2008"
Me.txtEditFacilityCity_CY2008.Size = New System.Drawing.Size(95, 20)
Me.txtEditFacilityCity_CY2008.TabIndex = 2
Me.txtEditFacilityCity_CY2008.Text = "Facility City"
'
'txtEditFacilityAddress_CY2008
'
Me.txtEditFacilityAddress_CY2008.Location = New System.Drawing.Point(15, 42)
Me.txtEditFacilityAddress_CY2008.Name = "txtEditFacilityAddress_CY2008"
Me.txtEditFacilityAddress_CY2008.Size = New System.Drawing.Size(190, 20)
Me.txtEditFacilityAddress_CY2008.TabIndex = 1
Me.txtEditFacilityAddress_CY2008.Text = "Facility Address"
'
'txtEditFacilityName_CY2008
'
Me.txtEditFacilityName_CY2008.Location = New System.Drawing.Point(15, 19)
Me.txtEditFacilityName_CY2008.Name = "txtEditFacilityName_CY2008"
Me.txtEditFacilityName_CY2008.Size = New System.Drawing.Size(190, 20)
Me.txtEditFacilityName_CY2008.TabIndex = 0
Me.txtEditFacilityName_CY2008.Text = "Facility Name"
'
'Label30
'
Me.Label30.AutoSize = true
Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label30.Location = New System.Drawing.Point(3, 3)
Me.Label30.Name = "Label30"
Me.Label30.Size = New System.Drawing.Size(111, 13)
Me.Label30.TabIndex = 1
Me.Label30.Text = "Facilty Information"
'
'Panel16
'
Me.Panel16.Controls.Add(Me.Panel18)
Me.Panel16.Controls.Add(Me.Panel17)
Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel16.Location = New System.Drawing.Point(3, 16)
Me.Panel16.Name = "Panel16"
Me.Panel16.Size = New System.Drawing.Size(741, 125)
Me.Panel16.TabIndex = 0
'
'Panel18
'
Me.Panel18.Controls.Add(Me.lblCY2008Status)
Me.Panel18.Controls.Add(Me.lblContactAddress2_CY2008)
Me.Panel18.Controls.Add(Me.lblContactCompany_CY2008)
Me.Panel18.Controls.Add(Me.lblContactName_CY2008)
Me.Panel18.Controls.Add(Me.lblContactAddress_CY2008)
Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel18.Location = New System.Drawing.Point(343, 0)
Me.Panel18.Name = "Panel18"
Me.Panel18.Size = New System.Drawing.Size(398, 125)
Me.Panel18.TabIndex = 70
'
'lblCY2008Status
'
Me.lblCY2008Status.AutoSize = true
Me.lblCY2008Status.Location = New System.Drawing.Point(10, 80)
Me.lblCY2008Status.Name = "lblCY2008Status"
Me.lblCY2008Status.Size = New System.Drawing.Size(43, 13)
Me.lblCY2008Status.TabIndex = 11
Me.lblCY2008Status.Text = "-Status-"
'
'lblContactAddress2_CY2008
'
Me.lblContactAddress2_CY2008.AutoSize = true
Me.lblContactAddress2_CY2008.Location = New System.Drawing.Point(44, 46)
Me.lblContactAddress2_CY2008.Name = "lblContactAddress2_CY2008"
Me.lblContactAddress2_CY2008.Size = New System.Drawing.Size(7, 13)
Me.lblContactAddress2_CY2008.TabIndex = 9
Me.lblContactAddress2_CY2008.Text = ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
'
'lblContactCompany_CY2008
'
Me.lblContactCompany_CY2008.AutoSize = true
Me.lblContactCompany_CY2008.Location = New System.Drawing.Point(10, 18)
Me.lblContactCompany_CY2008.Name = "lblContactCompany_CY2008"
Me.lblContactCompany_CY2008.Size = New System.Drawing.Size(91, 13)
Me.lblContactCompany_CY2008.TabIndex = 8
Me.lblContactCompany_CY2008.Text = "Contact Company"
'
'lblContactName_CY2008
'
Me.lblContactName_CY2008.AutoSize = true
Me.lblContactName_CY2008.Location = New System.Drawing.Point(10, 4)
Me.lblContactName_CY2008.Name = "lblContactName_CY2008"
Me.lblContactName_CY2008.Size = New System.Drawing.Size(75, 13)
Me.lblContactName_CY2008.TabIndex = 6
Me.lblContactName_CY2008.Text = "Contact Name"
'
'lblContactAddress_CY2008
'
Me.lblContactAddress_CY2008.AutoSize = true
Me.lblContactAddress_CY2008.Location = New System.Drawing.Point(10, 32)
Me.lblContactAddress_CY2008.Name = "lblContactAddress_CY2008"
Me.lblContactAddress_CY2008.Size = New System.Drawing.Size(85, 13)
Me.lblContactAddress_CY2008.TabIndex = 1
Me.lblContactAddress_CY2008.Text = "Contact Address"
'
'Panel17
'
Me.Panel17.Controls.Add(Me.txtAIRSNumber_08)
Me.Panel17.Controls.Add(Me.lblNSPS_CY2008)
Me.Panel17.Controls.Add(Me.lblTitleV_CY2008)
Me.Panel17.Controls.Add(Me.lblSourceClass_CY2008)
Me.Panel17.Controls.Add(Me.lblOperatingStatus_CY2008)
Me.Panel17.Controls.Add(Me.lblFacilityAddress2_CY2008)
Me.Panel17.Controls.Add(Me.lblFacilityAddress_CY2008)
Me.Panel17.Controls.Add(Me.llbNoteChanges_CY2008)
Me.Panel17.Controls.Add(Me.lblFacilityName_CY2008)
Me.Panel17.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel17.Location = New System.Drawing.Point(0, 0)
Me.Panel17.Name = "Panel17"
Me.Panel17.Size = New System.Drawing.Size(343, 125)
Me.Panel17.TabIndex = 69
'
'txtAIRSNumber_08
'
Me.txtAIRSNumber_08.Location = New System.Drawing.Point(240, 5)
Me.txtAIRSNumber_08.Name = "txtAIRSNumber_08"
Me.txtAIRSNumber_08.Size = New System.Drawing.Size(100, 20)
Me.txtAIRSNumber_08.TabIndex = 10
Me.txtAIRSNumber_08.Visible = false
'
'lblNSPS_CY2008
'
Me.lblNSPS_CY2008.AutoSize = true
Me.lblNSPS_CY2008.Location = New System.Drawing.Point(197, 80)
Me.lblNSPS_CY2008.Name = "lblNSPS_CY2008"
Me.lblNSPS_CY2008.Size = New System.Drawing.Size(36, 13)
Me.lblNSPS_CY2008.TabIndex = 9
Me.lblNSPS_CY2008.Text = "NSPS"
'
'lblTitleV_CY2008
'
Me.lblTitleV_CY2008.AutoSize = true
Me.lblTitleV_CY2008.Location = New System.Drawing.Point(197, 65)
Me.lblTitleV_CY2008.Name = "lblTitleV_CY2008"
Me.lblTitleV_CY2008.Size = New System.Drawing.Size(37, 13)
Me.lblTitleV_CY2008.TabIndex = 8
Me.lblTitleV_CY2008.Text = "Title V"
'
'lblSourceClass_CY2008
'
Me.lblSourceClass_CY2008.AutoSize = true
Me.lblSourceClass_CY2008.Location = New System.Drawing.Point(7, 80)
Me.lblSourceClass_CY2008.Name = "lblSourceClass_CY2008"
Me.lblSourceClass_CY2008.Size = New System.Drawing.Size(69, 13)
Me.lblSourceClass_CY2008.TabIndex = 7
Me.lblSourceClass_CY2008.Text = "Source Class"
'
'lblOperatingStatus_CY2008
'
Me.lblOperatingStatus_CY2008.AutoSize = true
Me.lblOperatingStatus_CY2008.Location = New System.Drawing.Point(7, 65)
Me.lblOperatingStatus_CY2008.Name = "lblOperatingStatus_CY2008"
Me.lblOperatingStatus_CY2008.Size = New System.Drawing.Size(86, 13)
Me.lblOperatingStatus_CY2008.TabIndex = 6
Me.lblOperatingStatus_CY2008.Text = "Operating Status"
'
'lblFacilityAddress2_CY2008
'
Me.lblFacilityAddress2_CY2008.AutoSize = true
Me.lblFacilityAddress2_CY2008.Location = New System.Drawing.Point(32, 33)
Me.lblFacilityAddress2_CY2008.Name = "lblFacilityAddress2_CY2008"
Me.lblFacilityAddress2_CY2008.Size = New System.Drawing.Size(0, 13)
Me.lblFacilityAddress2_CY2008.TabIndex = 5
'
'lblFacilityAddress_CY2008
'
Me.lblFacilityAddress_CY2008.AutoSize = true
Me.lblFacilityAddress_CY2008.Location = New System.Drawing.Point(3, 19)
Me.lblFacilityAddress_CY2008.Name = "lblFacilityAddress_CY2008"
Me.lblFacilityAddress_CY2008.Size = New System.Drawing.Size(78, 13)
Me.lblFacilityAddress_CY2008.TabIndex = 4
Me.lblFacilityAddress_CY2008.Text = "Facilty Address"
'
'llbNoteChanges_CY2008
'
Me.llbNoteChanges_CY2008.AutoSize = true
Me.llbNoteChanges_CY2008.Location = New System.Drawing.Point(7, 100)
Me.llbNoteChanges_CY2008.Name = "llbNoteChanges_CY2008"
Me.llbNoteChanges_CY2008.Size = New System.Drawing.Size(75, 13)
Me.llbNoteChanges_CY2008.TabIndex = 0
Me.llbNoteChanges_CY2008.TabStop = true
Me.llbNoteChanges_CY2008.Text = "Note Changes"
'
'lblFacilityName_CY2008
'
Me.lblFacilityName_CY2008.AutoSize = true
Me.lblFacilityName_CY2008.Location = New System.Drawing.Point(3, 5)
Me.lblFacilityName_CY2008.Name = "lblFacilityName_CY2008"
Me.lblFacilityName_CY2008.Size = New System.Drawing.Size(68, 13)
Me.lblFacilityName_CY2008.TabIndex = 1
Me.lblFacilityName_CY2008.Text = "Facilty Name"
'
'TP_CY2007
'
Me.TP_CY2007.AutoScroll = true
Me.TP_CY2007.Controls.Add(Me.Panel2)
Me.TP_CY2007.Location = New System.Drawing.Point(4, 22)
Me.TP_CY2007.Name = "TP_CY2007"
Me.TP_CY2007.Padding = New System.Windows.Forms.Padding(3)
Me.TP_CY2007.Size = New System.Drawing.Size(770, 198)
Me.TP_CY2007.TabIndex = 1
Me.TP_CY2007.Text = "CY2007"
Me.TP_CY2007.UseVisualStyleBackColor = true
'
'Panel2
'
Me.Panel2.AutoScroll = true
Me.Panel2.Controls.Add(Me.gbCY2007)
Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel2.Location = New System.Drawing.Point(3, 3)
Me.Panel2.Name = "Panel2"
Me.Panel2.Size = New System.Drawing.Size(764, 192)
Me.Panel2.TabIndex = 5
'
'gbCY2007
'
Me.gbCY2007.AutoSize = true
Me.gbCY2007.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.gbCY2007.Controls.Add(Me.Panel44)
Me.gbCY2007.Controls.Add(Me.Panel34)
Me.gbCY2007.Controls.Add(Me.Panel27)
Me.gbCY2007.Dock = System.Windows.Forms.DockStyle.Top
Me.gbCY2007.Location = New System.Drawing.Point(0, 0)
Me.gbCY2007.Name = "gbCY2007"
Me.gbCY2007.Size = New System.Drawing.Size(747, 404)
Me.gbCY2007.TabIndex = 4
Me.gbCY2007.TabStop = false
Me.gbCY2007.Text = "CY2007 Fee Mailout Information - due on Sept. 1, 2008"
'
'Panel44
'
Me.Panel44.Controls.Add(Me.btnSaveCurrentChange_CY2007)
Me.Panel44.Controls.Add(Me.txtCurrentComments_CY2007)
Me.Panel44.Controls.Add(Me.Label61)
Me.Panel44.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel44.Location = New System.Drawing.Point(3, 306)
Me.Panel44.Name = "Panel44"
Me.Panel44.Size = New System.Drawing.Size(741, 95)
Me.Panel44.TabIndex = 74
Me.Panel44.Visible = false
'
'btnSaveCurrentChange_CY2007
'
Me.btnSaveCurrentChange_CY2007.AutoSize = true
Me.btnSaveCurrentChange_CY2007.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveCurrentChange_CY2007.Location = New System.Drawing.Point(509, 19)
Me.btnSaveCurrentChange_CY2007.Name = "btnSaveCurrentChange_CY2007"
Me.btnSaveCurrentChange_CY2007.Size = New System.Drawing.Size(94, 23)
Me.btnSaveCurrentChange_CY2007.TabIndex = 7
Me.btnSaveCurrentChange_CY2007.Text = "Save Comments"
Me.btnSaveCurrentChange_CY2007.UseVisualStyleBackColor = true
'
'txtCurrentComments_CY2007
'
Me.txtCurrentComments_CY2007.Location = New System.Drawing.Point(10, 19)
Me.txtCurrentComments_CY2007.Multiline = true
Me.txtCurrentComments_CY2007.Name = "txtCurrentComments_CY2007"
Me.txtCurrentComments_CY2007.Size = New System.Drawing.Size(493, 64)
Me.txtCurrentComments_CY2007.TabIndex = 6
'
'Label61
'
Me.Label61.AutoSize = true
Me.Label61.Location = New System.Drawing.Point(3, 3)
Me.Label61.Name = "Label61"
Me.Label61.Size = New System.Drawing.Size(231, 13)
Me.Label61.TabIndex = 5
Me.Label61.Text = "Comments for Current Facility and Contact Info: "
'
'Panel34
'
Me.Panel34.Controls.Add(Me.Panel36)
Me.Panel34.Controls.Add(Me.Panel37)
Me.Panel34.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel34.Location = New System.Drawing.Point(3, 141)
Me.Panel34.Name = "Panel34"
Me.Panel34.Size = New System.Drawing.Size(741, 165)
Me.Panel34.TabIndex = 1
'
'Panel36
'
Me.Panel36.Controls.Add(Me.txtEditContactState_CY2007)
Me.Panel36.Controls.Add(Me.btnEditContactInfo_CY2007)
Me.Panel36.Controls.Add(Me.mtbEditContactZipCode_CY2007)
Me.Panel36.Controls.Add(Me.txtEditContactCity_CY2007)
Me.Panel36.Controls.Add(Me.txtEditContactAddress_CY2007)
Me.Panel36.Controls.Add(Me.txtEditContactCompany_CY2007)
Me.Panel36.Controls.Add(Me.txtEditContactLastName_CY2007)
Me.Panel36.Controls.Add(Me.txtEditContactFirstName_CY2007)
Me.Panel36.Controls.Add(Me.Label47)
Me.Panel36.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel36.Location = New System.Drawing.Point(343, 0)
Me.Panel36.Name = "Panel36"
Me.Panel36.Size = New System.Drawing.Size(398, 165)
Me.Panel36.TabIndex = 74
'
'txtEditContactState_CY2007
'
Me.txtEditContactState_CY2007.Location = New System.Drawing.Point(146, 113)
Me.txtEditContactState_CY2007.MaxLength = 2
Me.txtEditContactState_CY2007.Name = "txtEditContactState_CY2007"
Me.txtEditContactState_CY2007.Size = New System.Drawing.Size(25, 20)
Me.txtEditContactState_CY2007.TabIndex = 6
'
'btnEditContactInfo_CY2007
'
Me.btnEditContactInfo_CY2007.AutoSize = true
Me.btnEditContactInfo_CY2007.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnEditContactInfo_CY2007.Location = New System.Drawing.Point(247, 16)
Me.btnEditContactInfo_CY2007.Name = "btnEditContactInfo_CY2007"
Me.btnEditContactInfo_CY2007.Size = New System.Drawing.Size(106, 23)
Me.btnEditContactInfo_CY2007.TabIndex = 9
Me.btnEditContactInfo_CY2007.Text = "Save Contact Info."
Me.btnEditContactInfo_CY2007.UseVisualStyleBackColor = true
'
'mtbEditContactZipCode_CY2007
'
Me.mtbEditContactZipCode_CY2007.Location = New System.Drawing.Point(174, 113)
Me.mtbEditContactZipCode_CY2007.Mask = "00000-9999"
Me.mtbEditContactZipCode_CY2007.Name = "mtbEditContactZipCode_CY2007"
Me.mtbEditContactZipCode_CY2007.Size = New System.Drawing.Size(67, 20)
Me.mtbEditContactZipCode_CY2007.TabIndex = 7
Me.mtbEditContactZipCode_CY2007.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'txtEditContactCity_CY2007
'
Me.txtEditContactCity_CY2007.Location = New System.Drawing.Point(15, 113)
Me.txtEditContactCity_CY2007.Name = "txtEditContactCity_CY2007"
Me.txtEditContactCity_CY2007.Size = New System.Drawing.Size(125, 20)
Me.txtEditContactCity_CY2007.TabIndex = 5
Me.txtEditContactCity_CY2007.Text = "Contact City"
'
'txtEditContactAddress_CY2007
'
Me.txtEditContactAddress_CY2007.Location = New System.Drawing.Point(15, 89)
Me.txtEditContactAddress_CY2007.Name = "txtEditContactAddress_CY2007"
Me.txtEditContactAddress_CY2007.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactAddress_CY2007.TabIndex = 4
Me.txtEditContactAddress_CY2007.Text = "Contact Address"
'
'txtEditContactCompany_CY2007
'
Me.txtEditContactCompany_CY2007.Location = New System.Drawing.Point(15, 65)
Me.txtEditContactCompany_CY2007.Name = "txtEditContactCompany_CY2007"
Me.txtEditContactCompany_CY2007.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactCompany_CY2007.TabIndex = 3
Me.txtEditContactCompany_CY2007.Text = "Contact Company"
'
'txtEditContactLastName_CY2007
'
Me.txtEditContactLastName_CY2007.Location = New System.Drawing.Point(130, 19)
Me.txtEditContactLastName_CY2007.Name = "txtEditContactLastName_CY2007"
Me.txtEditContactLastName_CY2007.Size = New System.Drawing.Size(111, 20)
Me.txtEditContactLastName_CY2007.TabIndex = 1
Me.txtEditContactLastName_CY2007.Text = "Contact Last Name"
'
'txtEditContactFirstName_CY2007
'
Me.txtEditContactFirstName_CY2007.Location = New System.Drawing.Point(15, 19)
Me.txtEditContactFirstName_CY2007.Name = "txtEditContactFirstName_CY2007"
Me.txtEditContactFirstName_CY2007.Size = New System.Drawing.Size(109, 20)
Me.txtEditContactFirstName_CY2007.TabIndex = 0
Me.txtEditContactFirstName_CY2007.Text = "Contact First Name"
'
'Label47
'
Me.Label47.AutoSize = true
Me.Label47.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label47.Location = New System.Drawing.Point(3, 3)
Me.Label47.Name = "Label47"
Me.Label47.Size = New System.Drawing.Size(118, 13)
Me.Label47.TabIndex = 1
Me.Label47.Text = "Contact Information"
'
'Panel37
'
Me.Panel37.Controls.Add(Me.txtEditSourceClass_CY2007)
Me.Panel37.Controls.Add(Me.cboOperatingStatus_CY2007)
Me.Panel37.Controls.Add(Me.Panel38)
Me.Panel37.Controls.Add(Me.Panel39)
Me.Panel37.Controls.Add(Me.Label48)
Me.Panel37.Controls.Add(Me.Label49)
Me.Panel37.Controls.Add(Me.Label50)
Me.Panel37.Controls.Add(Me.Label51)
Me.Panel37.Controls.Add(Me.btnEditFacilityInfo_CY2007)
Me.Panel37.Controls.Add(Me.mtbEditZipCode_CY2007)
Me.Panel37.Controls.Add(Me.Label52)
Me.Panel37.Controls.Add(Me.txtEditFacilityCity_CY2007)
Me.Panel37.Controls.Add(Me.txtEditFacilityAddress_CY2007)
Me.Panel37.Controls.Add(Me.txtEditFacilityName_CY2007)
Me.Panel37.Controls.Add(Me.Label53)
Me.Panel37.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel37.Location = New System.Drawing.Point(0, 0)
Me.Panel37.Name = "Panel37"
Me.Panel37.Size = New System.Drawing.Size(343, 165)
Me.Panel37.TabIndex = 73
'
'txtEditSourceClass_CY2007
'
Me.txtEditSourceClass_CY2007.Location = New System.Drawing.Point(85, 119)
Me.txtEditSourceClass_CY2007.Name = "txtEditSourceClass_CY2007"
Me.txtEditSourceClass_CY2007.Size = New System.Drawing.Size(50, 20)
Me.txtEditSourceClass_CY2007.TabIndex = 20
'
'cboOperatingStatus_CY2007
'
Me.cboOperatingStatus_CY2007.FormattingEnabled = true
Me.cboOperatingStatus_CY2007.Location = New System.Drawing.Point(72, 93)
Me.cboOperatingStatus_CY2007.Name = "cboOperatingStatus_CY2007"
Me.cboOperatingStatus_CY2007.Size = New System.Drawing.Size(133, 21)
Me.cboOperatingStatus_CY2007.TabIndex = 17
'
'Panel38
'
Me.Panel38.AutoSize = true
Me.Panel38.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel38.Controls.Add(Me.rdbNSPSNo_CY2007)
Me.Panel38.Controls.Add(Me.rdbNSPSYes_CY2007)
Me.Panel38.Location = New System.Drawing.Point(246, 119)
Me.Panel38.Name = "Panel38"
Me.Panel38.Size = New System.Drawing.Size(94, 23)
Me.Panel38.TabIndex = 15
'
'rdbNSPSNo_CY2007
'
Me.rdbNSPSNo_CY2007.AutoSize = true
Me.rdbNSPSNo_CY2007.Location = New System.Drawing.Point(50, 3)
Me.rdbNSPSNo_CY2007.Name = "rdbNSPSNo_CY2007"
Me.rdbNSPSNo_CY2007.Size = New System.Drawing.Size(41, 17)
Me.rdbNSPSNo_CY2007.TabIndex = 1
Me.rdbNSPSNo_CY2007.TabStop = true
Me.rdbNSPSNo_CY2007.Text = "NO"
Me.rdbNSPSNo_CY2007.UseVisualStyleBackColor = true
'
'rdbNSPSYes_CY2007
'
Me.rdbNSPSYes_CY2007.AutoSize = true
Me.rdbNSPSYes_CY2007.Location = New System.Drawing.Point(3, 3)
Me.rdbNSPSYes_CY2007.Name = "rdbNSPSYes_CY2007"
Me.rdbNSPSYes_CY2007.Size = New System.Drawing.Size(46, 17)
Me.rdbNSPSYes_CY2007.TabIndex = 0
Me.rdbNSPSYes_CY2007.TabStop = true
Me.rdbNSPSYes_CY2007.Text = "YES"
Me.rdbNSPSYes_CY2007.UseVisualStyleBackColor = true
'
'Panel39
'
Me.Panel39.AutoSize = true
Me.Panel39.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel39.Controls.Add(Me.rdbTVNo_CY2007)
Me.Panel39.Controls.Add(Me.rdbTVYes_CY2007)
Me.Panel39.Location = New System.Drawing.Point(246, 92)
Me.Panel39.Name = "Panel39"
Me.Panel39.Size = New System.Drawing.Size(94, 23)
Me.Panel39.TabIndex = 14
'
'rdbTVNo_CY2007
'
Me.rdbTVNo_CY2007.AutoSize = true
Me.rdbTVNo_CY2007.Location = New System.Drawing.Point(50, 3)
Me.rdbTVNo_CY2007.Name = "rdbTVNo_CY2007"
Me.rdbTVNo_CY2007.Size = New System.Drawing.Size(41, 17)
Me.rdbTVNo_CY2007.TabIndex = 1
Me.rdbTVNo_CY2007.TabStop = true
Me.rdbTVNo_CY2007.Text = "NO"
Me.rdbTVNo_CY2007.UseVisualStyleBackColor = true
'
'rdbTVYes_CY2007
'
Me.rdbTVYes_CY2007.AutoSize = true
Me.rdbTVYes_CY2007.Location = New System.Drawing.Point(3, 3)
Me.rdbTVYes_CY2007.Name = "rdbTVYes_CY2007"
Me.rdbTVYes_CY2007.Size = New System.Drawing.Size(46, 17)
Me.rdbTVYes_CY2007.TabIndex = 0
Me.rdbTVYes_CY2007.TabStop = true
Me.rdbTVYes_CY2007.Text = "YES"
Me.rdbTVYes_CY2007.UseVisualStyleBackColor = true
'
'Label48
'
Me.Label48.AutoSize = true
Me.Label48.Location = New System.Drawing.Point(204, 124)
Me.Label48.Name = "Label48"
Me.Label48.Size = New System.Drawing.Size(36, 13)
Me.Label48.TabIndex = 13
Me.Label48.Text = "NSPS"
'
'Label49
'
Me.Label49.AutoSize = true
Me.Label49.Location = New System.Drawing.Point(206, 97)
Me.Label49.Name = "Label49"
Me.Label49.Size = New System.Drawing.Size(37, 13)
Me.Label49.TabIndex = 12
Me.Label49.Text = "Title V"
'
'Label50
'
Me.Label50.AutoSize = true
Me.Label50.Location = New System.Drawing.Point(17, 123)
Me.Label50.Name = "Label50"
Me.Label50.Size = New System.Drawing.Size(69, 13)
Me.Label50.TabIndex = 11
Me.Label50.Text = "Source Class"
'
'Label51
'
Me.Label51.AutoSize = true
Me.Label51.Location = New System.Drawing.Point(17, 96)
Me.Label51.Name = "Label51"
Me.Label51.Size = New System.Drawing.Size(54, 13)
Me.Label51.TabIndex = 10
Me.Label51.Text = "Op Status"
'
'btnEditFacilityInfo_CY2007
'
Me.btnEditFacilityInfo_CY2007.AutoSize = true
Me.btnEditFacilityInfo_CY2007.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnEditFacilityInfo_CY2007.Location = New System.Drawing.Point(211, 19)
Me.btnEditFacilityInfo_CY2007.Name = "btnEditFacilityInfo_CY2007"
Me.btnEditFacilityInfo_CY2007.Size = New System.Drawing.Size(98, 23)
Me.btnEditFacilityInfo_CY2007.TabIndex = 4
Me.btnEditFacilityInfo_CY2007.Text = "Save Facility Info"
Me.btnEditFacilityInfo_CY2007.UseVisualStyleBackColor = true
'
'mtbEditZipCode_CY2007
'
Me.mtbEditZipCode_CY2007.Location = New System.Drawing.Point(138, 65)
Me.mtbEditZipCode_CY2007.Mask = "00000-9999"
Me.mtbEditZipCode_CY2007.Name = "mtbEditZipCode_CY2007"
Me.mtbEditZipCode_CY2007.Size = New System.Drawing.Size(67, 20)
Me.mtbEditZipCode_CY2007.TabIndex = 3
Me.mtbEditZipCode_CY2007.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'Label52
'
Me.Label52.AutoSize = true
Me.Label52.Location = New System.Drawing.Point(113, 69)
Me.Label52.Name = "Label52"
Me.Label52.Size = New System.Drawing.Size(22, 13)
Me.Label52.TabIndex = 6
Me.Label52.Text = "GA"
'
'txtEditFacilityCity_CY2007
'
Me.txtEditFacilityCity_CY2007.Location = New System.Drawing.Point(15, 65)
Me.txtEditFacilityCity_CY2007.Name = "txtEditFacilityCity_CY2007"
Me.txtEditFacilityCity_CY2007.Size = New System.Drawing.Size(95, 20)
Me.txtEditFacilityCity_CY2007.TabIndex = 2
Me.txtEditFacilityCity_CY2007.Text = "Facility City"
'
'txtEditFacilityAddress_CY2007
'
Me.txtEditFacilityAddress_CY2007.Location = New System.Drawing.Point(15, 42)
Me.txtEditFacilityAddress_CY2007.Name = "txtEditFacilityAddress_CY2007"
Me.txtEditFacilityAddress_CY2007.Size = New System.Drawing.Size(190, 20)
Me.txtEditFacilityAddress_CY2007.TabIndex = 1
Me.txtEditFacilityAddress_CY2007.Text = "Facility Address"
'
'txtEditFacilityName_CY2007
'
Me.txtEditFacilityName_CY2007.Location = New System.Drawing.Point(15, 19)
Me.txtEditFacilityName_CY2007.Name = "txtEditFacilityName_CY2007"
Me.txtEditFacilityName_CY2007.Size = New System.Drawing.Size(190, 20)
Me.txtEditFacilityName_CY2007.TabIndex = 0
Me.txtEditFacilityName_CY2007.Text = "Facility Name"
'
'Label53
'
Me.Label53.AutoSize = true
Me.Label53.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label53.Location = New System.Drawing.Point(3, 3)
Me.Label53.Name = "Label53"
Me.Label53.Size = New System.Drawing.Size(111, 13)
Me.Label53.TabIndex = 1
Me.Label53.Text = "Facilty Information"
'
'Panel27
'
Me.Panel27.Controls.Add(Me.Panel30)
Me.Panel27.Controls.Add(Me.Panel31)
Me.Panel27.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel27.Location = New System.Drawing.Point(3, 16)
Me.Panel27.Name = "Panel27"
Me.Panel27.Size = New System.Drawing.Size(741, 125)
Me.Panel27.TabIndex = 0
'
'Panel30
'
Me.Panel30.Controls.Add(Me.lblCY2007Status)
Me.Panel30.Controls.Add(Me.lblContactAddress2_CY2007)
Me.Panel30.Controls.Add(Me.lblContactCompany_CY2007)
Me.Panel30.Controls.Add(Me.lblContactName_CY2007)
Me.Panel30.Controls.Add(Me.lblContactAddress_CY2007)
Me.Panel30.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel30.Location = New System.Drawing.Point(343, 0)
Me.Panel30.Name = "Panel30"
Me.Panel30.Size = New System.Drawing.Size(398, 125)
Me.Panel30.TabIndex = 72
'
'lblCY2007Status
'
Me.lblCY2007Status.AutoSize = true
Me.lblCY2007Status.Location = New System.Drawing.Point(10, 80)
Me.lblCY2007Status.Name = "lblCY2007Status"
Me.lblCY2007Status.Size = New System.Drawing.Size(43, 13)
Me.lblCY2007Status.TabIndex = 12
Me.lblCY2007Status.Text = "-Status-"
'
'lblContactAddress2_CY2007
'
Me.lblContactAddress2_CY2007.AutoSize = true
Me.lblContactAddress2_CY2007.Location = New System.Drawing.Point(44, 46)
Me.lblContactAddress2_CY2007.Name = "lblContactAddress2_CY2007"
Me.lblContactAddress2_CY2007.Size = New System.Drawing.Size(7, 13)
Me.lblContactAddress2_CY2007.TabIndex = 9
Me.lblContactAddress2_CY2007.Text = ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
'
'lblContactCompany_CY2007
'
Me.lblContactCompany_CY2007.AutoSize = true
Me.lblContactCompany_CY2007.Location = New System.Drawing.Point(10, 18)
Me.lblContactCompany_CY2007.Name = "lblContactCompany_CY2007"
Me.lblContactCompany_CY2007.Size = New System.Drawing.Size(91, 13)
Me.lblContactCompany_CY2007.TabIndex = 8
Me.lblContactCompany_CY2007.Text = "Contact Company"
'
'lblContactName_CY2007
'
Me.lblContactName_CY2007.AutoSize = true
Me.lblContactName_CY2007.Location = New System.Drawing.Point(10, 4)
Me.lblContactName_CY2007.Name = "lblContactName_CY2007"
Me.lblContactName_CY2007.Size = New System.Drawing.Size(75, 13)
Me.lblContactName_CY2007.TabIndex = 6
Me.lblContactName_CY2007.Text = "Contact Name"
'
'lblContactAddress_CY2007
'
Me.lblContactAddress_CY2007.AutoSize = true
Me.lblContactAddress_CY2007.Location = New System.Drawing.Point(10, 32)
Me.lblContactAddress_CY2007.Name = "lblContactAddress_CY2007"
Me.lblContactAddress_CY2007.Size = New System.Drawing.Size(85, 13)
Me.lblContactAddress_CY2007.TabIndex = 1
Me.lblContactAddress_CY2007.Text = "Contact Address"
'
'Panel31
'
Me.Panel31.Controls.Add(Me.txtAIRSNumber_07)
Me.Panel31.Controls.Add(Me.lblNSPS_CY2007)
Me.Panel31.Controls.Add(Me.lblTitleV_CY2007)
Me.Panel31.Controls.Add(Me.lblSourceClass_CY2007)
Me.Panel31.Controls.Add(Me.lblOperatingStatus_CY2007)
Me.Panel31.Controls.Add(Me.lblFacilityAddress2_CY2007)
Me.Panel31.Controls.Add(Me.lblFacilityAddress_CY2007)
Me.Panel31.Controls.Add(Me.llbNoteChanges_CY2007)
Me.Panel31.Controls.Add(Me.lblFacilityName_CY2007)
Me.Panel31.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel31.Location = New System.Drawing.Point(0, 0)
Me.Panel31.Name = "Panel31"
Me.Panel31.Size = New System.Drawing.Size(343, 125)
Me.Panel31.TabIndex = 71
'
'txtAIRSNumber_07
'
Me.txtAIRSNumber_07.Location = New System.Drawing.Point(237, 5)
Me.txtAIRSNumber_07.Name = "txtAIRSNumber_07"
Me.txtAIRSNumber_07.Size = New System.Drawing.Size(100, 20)
Me.txtAIRSNumber_07.TabIndex = 11
Me.txtAIRSNumber_07.Visible = false
'
'lblNSPS_CY2007
'
Me.lblNSPS_CY2007.AutoSize = true
Me.lblNSPS_CY2007.Location = New System.Drawing.Point(197, 80)
Me.lblNSPS_CY2007.Name = "lblNSPS_CY2007"
Me.lblNSPS_CY2007.Size = New System.Drawing.Size(36, 13)
Me.lblNSPS_CY2007.TabIndex = 9
Me.lblNSPS_CY2007.Text = "NSPS"
'
'lblTitleV_CY2007
'
Me.lblTitleV_CY2007.AutoSize = true
Me.lblTitleV_CY2007.Location = New System.Drawing.Point(197, 65)
Me.lblTitleV_CY2007.Name = "lblTitleV_CY2007"
Me.lblTitleV_CY2007.Size = New System.Drawing.Size(37, 13)
Me.lblTitleV_CY2007.TabIndex = 8
Me.lblTitleV_CY2007.Text = "Title V"
'
'lblSourceClass_CY2007
'
Me.lblSourceClass_CY2007.AutoSize = true
Me.lblSourceClass_CY2007.Location = New System.Drawing.Point(7, 80)
Me.lblSourceClass_CY2007.Name = "lblSourceClass_CY2007"
Me.lblSourceClass_CY2007.Size = New System.Drawing.Size(69, 13)
Me.lblSourceClass_CY2007.TabIndex = 7
Me.lblSourceClass_CY2007.Text = "Source Class"
'
'lblOperatingStatus_CY2007
'
Me.lblOperatingStatus_CY2007.AutoSize = true
Me.lblOperatingStatus_CY2007.Location = New System.Drawing.Point(7, 65)
Me.lblOperatingStatus_CY2007.Name = "lblOperatingStatus_CY2007"
Me.lblOperatingStatus_CY2007.Size = New System.Drawing.Size(86, 13)
Me.lblOperatingStatus_CY2007.TabIndex = 6
Me.lblOperatingStatus_CY2007.Text = "Operating Status"
'
'lblFacilityAddress2_CY2007
'
Me.lblFacilityAddress2_CY2007.AutoSize = true
Me.lblFacilityAddress2_CY2007.Location = New System.Drawing.Point(32, 33)
Me.lblFacilityAddress2_CY2007.Name = "lblFacilityAddress2_CY2007"
Me.lblFacilityAddress2_CY2007.Size = New System.Drawing.Size(0, 13)
Me.lblFacilityAddress2_CY2007.TabIndex = 5
'
'lblFacilityAddress_CY2007
'
Me.lblFacilityAddress_CY2007.AutoSize = true
Me.lblFacilityAddress_CY2007.Location = New System.Drawing.Point(3, 19)
Me.lblFacilityAddress_CY2007.Name = "lblFacilityAddress_CY2007"
Me.lblFacilityAddress_CY2007.Size = New System.Drawing.Size(78, 13)
Me.lblFacilityAddress_CY2007.TabIndex = 4
Me.lblFacilityAddress_CY2007.Text = "Facilty Address"
'
'llbNoteChanges_CY2007
'
Me.llbNoteChanges_CY2007.AutoSize = true
Me.llbNoteChanges_CY2007.Location = New System.Drawing.Point(7, 100)
Me.llbNoteChanges_CY2007.Name = "llbNoteChanges_CY2007"
Me.llbNoteChanges_CY2007.Size = New System.Drawing.Size(75, 13)
Me.llbNoteChanges_CY2007.TabIndex = 0
Me.llbNoteChanges_CY2007.TabStop = true
Me.llbNoteChanges_CY2007.Text = "Note Changes"
'
'lblFacilityName_CY2007
'
Me.lblFacilityName_CY2007.AutoSize = true
Me.lblFacilityName_CY2007.Location = New System.Drawing.Point(3, 5)
Me.lblFacilityName_CY2007.Name = "lblFacilityName_CY2007"
Me.lblFacilityName_CY2007.Size = New System.Drawing.Size(68, 13)
Me.lblFacilityName_CY2007.TabIndex = 1
Me.lblFacilityName_CY2007.Text = "Facilty Name"
'
'TP_CY2006
'
Me.TP_CY2006.AutoScroll = true
Me.TP_CY2006.Controls.Add(Me.Panel3)
Me.TP_CY2006.Location = New System.Drawing.Point(4, 22)
Me.TP_CY2006.Name = "TP_CY2006"
Me.TP_CY2006.Padding = New System.Windows.Forms.Padding(3)
Me.TP_CY2006.Size = New System.Drawing.Size(770, 198)
Me.TP_CY2006.TabIndex = 2
Me.TP_CY2006.Text = "CY2006"
Me.TP_CY2006.UseVisualStyleBackColor = true
'
'Panel3
'
Me.Panel3.AutoScroll = true
Me.Panel3.Controls.Add(Me.gbCY2006)
Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel3.Location = New System.Drawing.Point(3, 3)
Me.Panel3.Name = "Panel3"
Me.Panel3.Size = New System.Drawing.Size(764, 192)
Me.Panel3.TabIndex = 7
'
'gbCY2006
'
Me.gbCY2006.AutoSize = true
Me.gbCY2006.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.gbCY2006.Controls.Add(Me.Panel45)
Me.gbCY2006.Controls.Add(Me.Panel35)
Me.gbCY2006.Controls.Add(Me.Panel29)
Me.gbCY2006.Dock = System.Windows.Forms.DockStyle.Top
Me.gbCY2006.Location = New System.Drawing.Point(0, 0)
Me.gbCY2006.Name = "gbCY2006"
Me.gbCY2006.Size = New System.Drawing.Size(747, 404)
Me.gbCY2006.TabIndex = 6
Me.gbCY2006.TabStop = false
Me.gbCY2006.Text = "CY2006 Fee Mailout Information - due on Sept. 1, 2007"
'
'Panel45
'
Me.Panel45.Controls.Add(Me.btnSaveCurrentChange_CY2006)
Me.Panel45.Controls.Add(Me.txtCurrentComments_CY2006)
Me.Panel45.Controls.Add(Me.Label62)
Me.Panel45.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel45.Location = New System.Drawing.Point(3, 306)
Me.Panel45.Name = "Panel45"
Me.Panel45.Size = New System.Drawing.Size(741, 95)
Me.Panel45.TabIndex = 74
Me.Panel45.Visible = false
'
'btnSaveCurrentChange_CY2006
'
Me.btnSaveCurrentChange_CY2006.AutoSize = true
Me.btnSaveCurrentChange_CY2006.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveCurrentChange_CY2006.Location = New System.Drawing.Point(509, 19)
Me.btnSaveCurrentChange_CY2006.Name = "btnSaveCurrentChange_CY2006"
Me.btnSaveCurrentChange_CY2006.Size = New System.Drawing.Size(94, 23)
Me.btnSaveCurrentChange_CY2006.TabIndex = 7
Me.btnSaveCurrentChange_CY2006.Text = "Save Comments"
Me.btnSaveCurrentChange_CY2006.UseVisualStyleBackColor = true
'
'txtCurrentComments_CY2006
'
Me.txtCurrentComments_CY2006.Location = New System.Drawing.Point(10, 19)
Me.txtCurrentComments_CY2006.Multiline = true
Me.txtCurrentComments_CY2006.Name = "txtCurrentComments_CY2006"
Me.txtCurrentComments_CY2006.Size = New System.Drawing.Size(493, 64)
Me.txtCurrentComments_CY2006.TabIndex = 6
'
'Label62
'
Me.Label62.AutoSize = true
Me.Label62.Location = New System.Drawing.Point(3, 3)
Me.Label62.Name = "Label62"
Me.Label62.Size = New System.Drawing.Size(231, 13)
Me.Label62.TabIndex = 5
Me.Label62.Text = "Comments for Current Facility and Contact Info: "
'
'Panel35
'
Me.Panel35.Controls.Add(Me.Panel40)
Me.Panel35.Controls.Add(Me.Panel41)
Me.Panel35.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel35.Location = New System.Drawing.Point(3, 141)
Me.Panel35.Name = "Panel35"
Me.Panel35.Size = New System.Drawing.Size(741, 165)
Me.Panel35.TabIndex = 2
'
'Panel40
'
Me.Panel40.Controls.Add(Me.txtEditContactState_CY2006)
Me.Panel40.Controls.Add(Me.btnEditContactInfo_CY2006)
Me.Panel40.Controls.Add(Me.mtbEditContactZipCode_CY2006)
Me.Panel40.Controls.Add(Me.txtEditContactCity_CY2006)
Me.Panel40.Controls.Add(Me.txtEditContactAddress_CY2006)
Me.Panel40.Controls.Add(Me.txtEditContactCompany_CY2006)
Me.Panel40.Controls.Add(Me.txtEditContactLastName_CY2006)
Me.Panel40.Controls.Add(Me.txtEditContactFirstName_CY2006)
Me.Panel40.Controls.Add(Me.Label54)
Me.Panel40.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel40.Location = New System.Drawing.Point(343, 0)
Me.Panel40.Name = "Panel40"
Me.Panel40.Size = New System.Drawing.Size(398, 165)
Me.Panel40.TabIndex = 74
'
'txtEditContactState_CY2006
'
Me.txtEditContactState_CY2006.Location = New System.Drawing.Point(146, 113)
Me.txtEditContactState_CY2006.MaxLength = 2
Me.txtEditContactState_CY2006.Name = "txtEditContactState_CY2006"
Me.txtEditContactState_CY2006.Size = New System.Drawing.Size(25, 20)
Me.txtEditContactState_CY2006.TabIndex = 6
'
'btnEditContactInfo_CY2006
'
Me.btnEditContactInfo_CY2006.AutoSize = true
Me.btnEditContactInfo_CY2006.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnEditContactInfo_CY2006.Location = New System.Drawing.Point(247, 16)
Me.btnEditContactInfo_CY2006.Name = "btnEditContactInfo_CY2006"
Me.btnEditContactInfo_CY2006.Size = New System.Drawing.Size(106, 23)
Me.btnEditContactInfo_CY2006.TabIndex = 9
Me.btnEditContactInfo_CY2006.Text = "Save Contact Info."
Me.btnEditContactInfo_CY2006.UseVisualStyleBackColor = true
'
'mtbEditContactZipCode_CY2006
'
Me.mtbEditContactZipCode_CY2006.Location = New System.Drawing.Point(174, 113)
Me.mtbEditContactZipCode_CY2006.Mask = "00000-9999"
Me.mtbEditContactZipCode_CY2006.Name = "mtbEditContactZipCode_CY2006"
Me.mtbEditContactZipCode_CY2006.Size = New System.Drawing.Size(67, 20)
Me.mtbEditContactZipCode_CY2006.TabIndex = 7
Me.mtbEditContactZipCode_CY2006.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'txtEditContactCity_CY2006
'
Me.txtEditContactCity_CY2006.Location = New System.Drawing.Point(15, 113)
Me.txtEditContactCity_CY2006.Name = "txtEditContactCity_CY2006"
Me.txtEditContactCity_CY2006.Size = New System.Drawing.Size(125, 20)
Me.txtEditContactCity_CY2006.TabIndex = 5
Me.txtEditContactCity_CY2006.Text = "Contact City"
'
'txtEditContactAddress_CY2006
'
Me.txtEditContactAddress_CY2006.Location = New System.Drawing.Point(15, 89)
Me.txtEditContactAddress_CY2006.Name = "txtEditContactAddress_CY2006"
Me.txtEditContactAddress_CY2006.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactAddress_CY2006.TabIndex = 4
Me.txtEditContactAddress_CY2006.Text = "Contact Address"
'
'txtEditContactCompany_CY2006
'
Me.txtEditContactCompany_CY2006.Location = New System.Drawing.Point(15, 65)
Me.txtEditContactCompany_CY2006.Name = "txtEditContactCompany_CY2006"
Me.txtEditContactCompany_CY2006.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactCompany_CY2006.TabIndex = 3
Me.txtEditContactCompany_CY2006.Text = "Contact Company"
'
'txtEditContactLastName_CY2006
'
Me.txtEditContactLastName_CY2006.Location = New System.Drawing.Point(130, 19)
Me.txtEditContactLastName_CY2006.Name = "txtEditContactLastName_CY2006"
Me.txtEditContactLastName_CY2006.Size = New System.Drawing.Size(111, 20)
Me.txtEditContactLastName_CY2006.TabIndex = 1
Me.txtEditContactLastName_CY2006.Text = "Contact Last Name"
'
'txtEditContactFirstName_CY2006
'
Me.txtEditContactFirstName_CY2006.Location = New System.Drawing.Point(15, 19)
Me.txtEditContactFirstName_CY2006.Name = "txtEditContactFirstName_CY2006"
Me.txtEditContactFirstName_CY2006.Size = New System.Drawing.Size(109, 20)
Me.txtEditContactFirstName_CY2006.TabIndex = 0
Me.txtEditContactFirstName_CY2006.Text = "Contact First Name"
'
'Label54
'
Me.Label54.AutoSize = true
Me.Label54.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label54.Location = New System.Drawing.Point(3, 3)
Me.Label54.Name = "Label54"
Me.Label54.Size = New System.Drawing.Size(118, 13)
Me.Label54.TabIndex = 1
Me.Label54.Text = "Contact Information"
'
'Panel41
'
Me.Panel41.Controls.Add(Me.txtEditSourceClass_CY2006)
Me.Panel41.Controls.Add(Me.cboOperatingStatus_CY2006)
Me.Panel41.Controls.Add(Me.Panel42)
Me.Panel41.Controls.Add(Me.Panel43)
Me.Panel41.Controls.Add(Me.Label55)
Me.Panel41.Controls.Add(Me.Label56)
Me.Panel41.Controls.Add(Me.Label57)
Me.Panel41.Controls.Add(Me.Label58)
Me.Panel41.Controls.Add(Me.btnEditFacilityInfo_CY2006)
Me.Panel41.Controls.Add(Me.mtbEditZipCode_CY2006)
Me.Panel41.Controls.Add(Me.Label59)
Me.Panel41.Controls.Add(Me.txtEditFacilityCity_CY2006)
Me.Panel41.Controls.Add(Me.txtEditFacilityAddress_CY2006)
Me.Panel41.Controls.Add(Me.txtEditFacilityName_CY2006)
Me.Panel41.Controls.Add(Me.Label60)
Me.Panel41.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel41.Location = New System.Drawing.Point(0, 0)
Me.Panel41.Name = "Panel41"
Me.Panel41.Size = New System.Drawing.Size(343, 165)
Me.Panel41.TabIndex = 73
'
'txtEditSourceClass_CY2006
'
Me.txtEditSourceClass_CY2006.Location = New System.Drawing.Point(85, 119)
Me.txtEditSourceClass_CY2006.Name = "txtEditSourceClass_CY2006"
Me.txtEditSourceClass_CY2006.Size = New System.Drawing.Size(50, 20)
Me.txtEditSourceClass_CY2006.TabIndex = 20
'
'cboOperatingStatus_CY2006
'
Me.cboOperatingStatus_CY2006.FormattingEnabled = true
Me.cboOperatingStatus_CY2006.Location = New System.Drawing.Point(72, 93)
Me.cboOperatingStatus_CY2006.Name = "cboOperatingStatus_CY2006"
Me.cboOperatingStatus_CY2006.Size = New System.Drawing.Size(133, 21)
Me.cboOperatingStatus_CY2006.TabIndex = 17
'
'Panel42
'
Me.Panel42.AutoSize = true
Me.Panel42.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel42.Controls.Add(Me.rdbNSPSNo_CY2006)
Me.Panel42.Controls.Add(Me.rdbNSPSYes_CY2006)
Me.Panel42.Location = New System.Drawing.Point(246, 119)
Me.Panel42.Name = "Panel42"
Me.Panel42.Size = New System.Drawing.Size(94, 23)
Me.Panel42.TabIndex = 15
'
'rdbNSPSNo_CY2006
'
Me.rdbNSPSNo_CY2006.AutoSize = true
Me.rdbNSPSNo_CY2006.Location = New System.Drawing.Point(50, 3)
Me.rdbNSPSNo_CY2006.Name = "rdbNSPSNo_CY2006"
Me.rdbNSPSNo_CY2006.Size = New System.Drawing.Size(41, 17)
Me.rdbNSPSNo_CY2006.TabIndex = 1
Me.rdbNSPSNo_CY2006.TabStop = true
Me.rdbNSPSNo_CY2006.Text = "NO"
Me.rdbNSPSNo_CY2006.UseVisualStyleBackColor = true
'
'rdbNSPSYes_CY2006
'
Me.rdbNSPSYes_CY2006.AutoSize = true
Me.rdbNSPSYes_CY2006.Location = New System.Drawing.Point(3, 3)
Me.rdbNSPSYes_CY2006.Name = "rdbNSPSYes_CY2006"
Me.rdbNSPSYes_CY2006.Size = New System.Drawing.Size(46, 17)
Me.rdbNSPSYes_CY2006.TabIndex = 0
Me.rdbNSPSYes_CY2006.TabStop = true
Me.rdbNSPSYes_CY2006.Text = "YES"
Me.rdbNSPSYes_CY2006.UseVisualStyleBackColor = true
'
'Panel43
'
Me.Panel43.AutoSize = true
Me.Panel43.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel43.Controls.Add(Me.rdbTVNo_CY2006)
Me.Panel43.Controls.Add(Me.rdbTVYes_CY2006)
Me.Panel43.Location = New System.Drawing.Point(246, 92)
Me.Panel43.Name = "Panel43"
Me.Panel43.Size = New System.Drawing.Size(94, 23)
Me.Panel43.TabIndex = 14
'
'rdbTVNo_CY2006
'
Me.rdbTVNo_CY2006.AutoSize = true
Me.rdbTVNo_CY2006.Location = New System.Drawing.Point(50, 3)
Me.rdbTVNo_CY2006.Name = "rdbTVNo_CY2006"
Me.rdbTVNo_CY2006.Size = New System.Drawing.Size(41, 17)
Me.rdbTVNo_CY2006.TabIndex = 1
Me.rdbTVNo_CY2006.TabStop = true
Me.rdbTVNo_CY2006.Text = "NO"
Me.rdbTVNo_CY2006.UseVisualStyleBackColor = true
'
'rdbTVYes_CY2006
'
Me.rdbTVYes_CY2006.AutoSize = true
Me.rdbTVYes_CY2006.Location = New System.Drawing.Point(3, 3)
Me.rdbTVYes_CY2006.Name = "rdbTVYes_CY2006"
Me.rdbTVYes_CY2006.Size = New System.Drawing.Size(46, 17)
Me.rdbTVYes_CY2006.TabIndex = 0
Me.rdbTVYes_CY2006.TabStop = true
Me.rdbTVYes_CY2006.Text = "YES"
Me.rdbTVYes_CY2006.UseVisualStyleBackColor = true
'
'Label55
'
Me.Label55.AutoSize = true
Me.Label55.Location = New System.Drawing.Point(204, 124)
Me.Label55.Name = "Label55"
Me.Label55.Size = New System.Drawing.Size(36, 13)
Me.Label55.TabIndex = 13
Me.Label55.Text = "NSPS"
'
'Label56
'
Me.Label56.AutoSize = true
Me.Label56.Location = New System.Drawing.Point(206, 97)
Me.Label56.Name = "Label56"
Me.Label56.Size = New System.Drawing.Size(37, 13)
Me.Label56.TabIndex = 12
Me.Label56.Text = "Title V"
'
'Label57
'
Me.Label57.AutoSize = true
Me.Label57.Location = New System.Drawing.Point(17, 123)
Me.Label57.Name = "Label57"
Me.Label57.Size = New System.Drawing.Size(69, 13)
Me.Label57.TabIndex = 11
Me.Label57.Text = "Source Class"
'
'Label58
'
Me.Label58.AutoSize = true
Me.Label58.Location = New System.Drawing.Point(17, 96)
Me.Label58.Name = "Label58"
Me.Label58.Size = New System.Drawing.Size(54, 13)
Me.Label58.TabIndex = 10
Me.Label58.Text = "Op Status"
'
'btnEditFacilityInfo_CY2006
'
Me.btnEditFacilityInfo_CY2006.AutoSize = true
Me.btnEditFacilityInfo_CY2006.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnEditFacilityInfo_CY2006.Location = New System.Drawing.Point(211, 19)
Me.btnEditFacilityInfo_CY2006.Name = "btnEditFacilityInfo_CY2006"
Me.btnEditFacilityInfo_CY2006.Size = New System.Drawing.Size(98, 23)
Me.btnEditFacilityInfo_CY2006.TabIndex = 4
Me.btnEditFacilityInfo_CY2006.Text = "Save Facility Info"
Me.btnEditFacilityInfo_CY2006.UseVisualStyleBackColor = true
'
'mtbEditZipCode_CY2006
'
Me.mtbEditZipCode_CY2006.Location = New System.Drawing.Point(138, 65)
Me.mtbEditZipCode_CY2006.Mask = "00000-9999"
Me.mtbEditZipCode_CY2006.Name = "mtbEditZipCode_CY2006"
Me.mtbEditZipCode_CY2006.Size = New System.Drawing.Size(67, 20)
Me.mtbEditZipCode_CY2006.TabIndex = 3
Me.mtbEditZipCode_CY2006.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'Label59
'
Me.Label59.AutoSize = true
Me.Label59.Location = New System.Drawing.Point(113, 69)
Me.Label59.Name = "Label59"
Me.Label59.Size = New System.Drawing.Size(22, 13)
Me.Label59.TabIndex = 6
Me.Label59.Text = "GA"
'
'txtEditFacilityCity_CY2006
'
Me.txtEditFacilityCity_CY2006.Location = New System.Drawing.Point(15, 65)
Me.txtEditFacilityCity_CY2006.Name = "txtEditFacilityCity_CY2006"
Me.txtEditFacilityCity_CY2006.Size = New System.Drawing.Size(95, 20)
Me.txtEditFacilityCity_CY2006.TabIndex = 2
Me.txtEditFacilityCity_CY2006.Text = "Facility City"
'
'txtEditFacilityAddress_CY2006
'
Me.txtEditFacilityAddress_CY2006.Location = New System.Drawing.Point(15, 42)
Me.txtEditFacilityAddress_CY2006.Name = "txtEditFacilityAddress_CY2006"
Me.txtEditFacilityAddress_CY2006.Size = New System.Drawing.Size(190, 20)
Me.txtEditFacilityAddress_CY2006.TabIndex = 1
Me.txtEditFacilityAddress_CY2006.Text = "Facility Address"
'
'txtEditFacilityName_CY2006
'
Me.txtEditFacilityName_CY2006.Location = New System.Drawing.Point(15, 19)
Me.txtEditFacilityName_CY2006.Name = "txtEditFacilityName_CY2006"
Me.txtEditFacilityName_CY2006.Size = New System.Drawing.Size(190, 20)
Me.txtEditFacilityName_CY2006.TabIndex = 0
Me.txtEditFacilityName_CY2006.Text = "Facility Name"
'
'Label60
'
Me.Label60.AutoSize = true
Me.Label60.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label60.Location = New System.Drawing.Point(3, 3)
Me.Label60.Name = "Label60"
Me.Label60.Size = New System.Drawing.Size(111, 13)
Me.Label60.TabIndex = 1
Me.Label60.Text = "Facilty Information"
'
'Panel29
'
Me.Panel29.Controls.Add(Me.Panel32)
Me.Panel29.Controls.Add(Me.Panel33)
Me.Panel29.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel29.Location = New System.Drawing.Point(3, 16)
Me.Panel29.Name = "Panel29"
Me.Panel29.Size = New System.Drawing.Size(741, 125)
Me.Panel29.TabIndex = 0
'
'Panel32
'
Me.Panel32.Controls.Add(Me.lblCY2006Status)
Me.Panel32.Controls.Add(Me.lblContactAddress2_CY2006)
Me.Panel32.Controls.Add(Me.lblContactCompany_CY2006)
Me.Panel32.Controls.Add(Me.lblContactName_CY2006)
Me.Panel32.Controls.Add(Me.lblContactAddress_CY2006)
Me.Panel32.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel32.Location = New System.Drawing.Point(343, 0)
Me.Panel32.Name = "Panel32"
Me.Panel32.Size = New System.Drawing.Size(398, 125)
Me.Panel32.TabIndex = 72
'
'lblCY2006Status
'
Me.lblCY2006Status.AutoSize = true
Me.lblCY2006Status.Location = New System.Drawing.Point(10, 80)
Me.lblCY2006Status.Name = "lblCY2006Status"
Me.lblCY2006Status.Size = New System.Drawing.Size(43, 13)
Me.lblCY2006Status.TabIndex = 13
Me.lblCY2006Status.Text = "-Status-"
'
'lblContactAddress2_CY2006
'
Me.lblContactAddress2_CY2006.AutoSize = true
Me.lblContactAddress2_CY2006.Location = New System.Drawing.Point(44, 46)
Me.lblContactAddress2_CY2006.Name = "lblContactAddress2_CY2006"
Me.lblContactAddress2_CY2006.Size = New System.Drawing.Size(7, 13)
Me.lblContactAddress2_CY2006.TabIndex = 9
Me.lblContactAddress2_CY2006.Text = ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
'
'lblContactCompany_CY2006
'
Me.lblContactCompany_CY2006.AutoSize = true
Me.lblContactCompany_CY2006.Location = New System.Drawing.Point(10, 18)
Me.lblContactCompany_CY2006.Name = "lblContactCompany_CY2006"
Me.lblContactCompany_CY2006.Size = New System.Drawing.Size(91, 13)
Me.lblContactCompany_CY2006.TabIndex = 8
Me.lblContactCompany_CY2006.Text = "Contact Company"
'
'lblContactName_CY2006
'
Me.lblContactName_CY2006.AutoSize = true
Me.lblContactName_CY2006.Location = New System.Drawing.Point(10, 4)
Me.lblContactName_CY2006.Name = "lblContactName_CY2006"
Me.lblContactName_CY2006.Size = New System.Drawing.Size(75, 13)
Me.lblContactName_CY2006.TabIndex = 6
Me.lblContactName_CY2006.Text = "Contact Name"
'
'lblContactAddress_CY2006
'
Me.lblContactAddress_CY2006.AutoSize = true
Me.lblContactAddress_CY2006.Location = New System.Drawing.Point(10, 32)
Me.lblContactAddress_CY2006.Name = "lblContactAddress_CY2006"
Me.lblContactAddress_CY2006.Size = New System.Drawing.Size(85, 13)
Me.lblContactAddress_CY2006.TabIndex = 1
Me.lblContactAddress_CY2006.Text = "Contact Address"
'
'Panel33
'
Me.Panel33.Controls.Add(Me.txtAIRSNumber_06)
Me.Panel33.Controls.Add(Me.lblNSPS_CY2006)
Me.Panel33.Controls.Add(Me.lblTitleV_CY2006)
Me.Panel33.Controls.Add(Me.lblSourceClass_CY2006)
Me.Panel33.Controls.Add(Me.lblOperatingStatus_CY2006)
Me.Panel33.Controls.Add(Me.lblFacilityAddress2_CY2006)
Me.Panel33.Controls.Add(Me.lblFacilityAddress_CY2006)
Me.Panel33.Controls.Add(Me.llbNoteChanges_CY2006)
Me.Panel33.Controls.Add(Me.lblFacilityName_CY2006)
Me.Panel33.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel33.Location = New System.Drawing.Point(0, 0)
Me.Panel33.Name = "Panel33"
Me.Panel33.Size = New System.Drawing.Size(343, 125)
Me.Panel33.TabIndex = 71
'
'txtAIRSNumber_06
'
Me.txtAIRSNumber_06.Location = New System.Drawing.Point(237, 3)
Me.txtAIRSNumber_06.Name = "txtAIRSNumber_06"
Me.txtAIRSNumber_06.Size = New System.Drawing.Size(100, 20)
Me.txtAIRSNumber_06.TabIndex = 11
Me.txtAIRSNumber_06.Visible = false
'
'lblNSPS_CY2006
'
Me.lblNSPS_CY2006.AutoSize = true
Me.lblNSPS_CY2006.Location = New System.Drawing.Point(197, 80)
Me.lblNSPS_CY2006.Name = "lblNSPS_CY2006"
Me.lblNSPS_CY2006.Size = New System.Drawing.Size(36, 13)
Me.lblNSPS_CY2006.TabIndex = 9
Me.lblNSPS_CY2006.Text = "NSPS"
'
'lblTitleV_CY2006
'
Me.lblTitleV_CY2006.AutoSize = true
Me.lblTitleV_CY2006.Location = New System.Drawing.Point(197, 65)
Me.lblTitleV_CY2006.Name = "lblTitleV_CY2006"
Me.lblTitleV_CY2006.Size = New System.Drawing.Size(37, 13)
Me.lblTitleV_CY2006.TabIndex = 8
Me.lblTitleV_CY2006.Text = "Title V"
'
'lblSourceClass_CY2006
'
Me.lblSourceClass_CY2006.AutoSize = true
Me.lblSourceClass_CY2006.Location = New System.Drawing.Point(7, 80)
Me.lblSourceClass_CY2006.Name = "lblSourceClass_CY2006"
Me.lblSourceClass_CY2006.Size = New System.Drawing.Size(69, 13)
Me.lblSourceClass_CY2006.TabIndex = 7
Me.lblSourceClass_CY2006.Text = "Source Class"
'
'lblOperatingStatus_CY2006
'
Me.lblOperatingStatus_CY2006.AutoSize = true
Me.lblOperatingStatus_CY2006.Location = New System.Drawing.Point(7, 65)
Me.lblOperatingStatus_CY2006.Name = "lblOperatingStatus_CY2006"
Me.lblOperatingStatus_CY2006.Size = New System.Drawing.Size(86, 13)
Me.lblOperatingStatus_CY2006.TabIndex = 6
Me.lblOperatingStatus_CY2006.Text = "Operating Status"
'
'lblFacilityAddress2_CY2006
'
Me.lblFacilityAddress2_CY2006.AutoSize = true
Me.lblFacilityAddress2_CY2006.Location = New System.Drawing.Point(32, 33)
Me.lblFacilityAddress2_CY2006.Name = "lblFacilityAddress2_CY2006"
Me.lblFacilityAddress2_CY2006.Size = New System.Drawing.Size(0, 13)
Me.lblFacilityAddress2_CY2006.TabIndex = 5
'
'lblFacilityAddress_CY2006
'
Me.lblFacilityAddress_CY2006.AutoSize = true
Me.lblFacilityAddress_CY2006.Location = New System.Drawing.Point(3, 19)
Me.lblFacilityAddress_CY2006.Name = "lblFacilityAddress_CY2006"
Me.lblFacilityAddress_CY2006.Size = New System.Drawing.Size(78, 13)
Me.lblFacilityAddress_CY2006.TabIndex = 4
Me.lblFacilityAddress_CY2006.Text = "Facilty Address"
'
'llbNoteChanges_CY2006
'
Me.llbNoteChanges_CY2006.AutoSize = true
Me.llbNoteChanges_CY2006.Location = New System.Drawing.Point(7, 100)
Me.llbNoteChanges_CY2006.Name = "llbNoteChanges_CY2006"
Me.llbNoteChanges_CY2006.Size = New System.Drawing.Size(75, 13)
Me.llbNoteChanges_CY2006.TabIndex = 0
Me.llbNoteChanges_CY2006.TabStop = true
Me.llbNoteChanges_CY2006.Text = "Note Changes"
'
'lblFacilityName_CY2006
'
Me.lblFacilityName_CY2006.AutoSize = true
Me.lblFacilityName_CY2006.Location = New System.Drawing.Point(3, 5)
Me.lblFacilityName_CY2006.Name = "lblFacilityName_CY2006"
Me.lblFacilityName_CY2006.Size = New System.Drawing.Size(68, 13)
Me.lblFacilityName_CY2006.TabIndex = 1
Me.lblFacilityName_CY2006.Text = "Facilty Name"
'
'TP_Change_Questions
'
Me.TP_Change_Questions.AutoScroll = true
Me.TP_Change_Questions.Controls.Add(Me.GroupBox1)
Me.TP_Change_Questions.Controls.Add(Me.gbOwnership)
Me.TP_Change_Questions.Location = New System.Drawing.Point(4, 22)
Me.TP_Change_Questions.Name = "TP_Change_Questions"
Me.TP_Change_Questions.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Change_Questions.Size = New System.Drawing.Size(770, 198)
Me.TP_Change_Questions.TabIndex = 3
Me.TP_Change_Questions.Text = "Change Questions"
Me.TP_Change_Questions.UseVisualStyleBackColor = true
'
'GroupBox1
'
Me.GroupBox1.Controls.Add(Me.Panel49)
Me.GroupBox1.Controls.Add(Me.btnSaveSourceClassificationChanges)
Me.GroupBox1.Controls.Add(Me.txtSourceClassificationChangeComment)
Me.GroupBox1.Controls.Add(Me.Label36)
Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
Me.GroupBox1.Location = New System.Drawing.Point(3, 122)
Me.GroupBox1.Name = "GroupBox1"
Me.GroupBox1.Size = New System.Drawing.Size(747, 119)
Me.GroupBox1.TabIndex = 9
Me.GroupBox1.TabStop = false
Me.GroupBox1.Text = "Source Classification"
'
'Panel49
'
Me.Panel49.AutoSize = true
Me.Panel49.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel49.Controls.Add(Me.rdbSourceClassChangeNO)
Me.Panel49.Controls.Add(Me.rdbSourceClassChangeYes)
Me.Panel49.Location = New System.Drawing.Point(351, 12)
Me.Panel49.Name = "Panel49"
Me.Panel49.Size = New System.Drawing.Size(94, 23)
Me.Panel49.TabIndex = 19
'
'rdbSourceClassChangeNO
'
Me.rdbSourceClassChangeNO.AutoSize = true
Me.rdbSourceClassChangeNO.Location = New System.Drawing.Point(50, 3)
Me.rdbSourceClassChangeNO.Name = "rdbSourceClassChangeNO"
Me.rdbSourceClassChangeNO.Size = New System.Drawing.Size(41, 17)
Me.rdbSourceClassChangeNO.TabIndex = 1
Me.rdbSourceClassChangeNO.TabStop = true
Me.rdbSourceClassChangeNO.Text = "NO"
Me.rdbSourceClassChangeNO.UseVisualStyleBackColor = true
'
'rdbSourceClassChangeYes
'
Me.rdbSourceClassChangeYes.AutoSize = true
Me.rdbSourceClassChangeYes.Location = New System.Drawing.Point(3, 3)
Me.rdbSourceClassChangeYes.Name = "rdbSourceClassChangeYes"
Me.rdbSourceClassChangeYes.Size = New System.Drawing.Size(46, 17)
Me.rdbSourceClassChangeYes.TabIndex = 0
Me.rdbSourceClassChangeYes.TabStop = true
Me.rdbSourceClassChangeYes.Text = "YES"
Me.rdbSourceClassChangeYes.UseVisualStyleBackColor = true
'
'btnSaveSourceClassificationChanges
'
Me.btnSaveSourceClassificationChanges.AutoSize = true
Me.btnSaveSourceClassificationChanges.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveSourceClassificationChanges.Location = New System.Drawing.Point(454, 11)
Me.btnSaveSourceClassificationChanges.Name = "btnSaveSourceClassificationChanges"
Me.btnSaveSourceClassificationChanges.Size = New System.Drawing.Size(42, 23)
Me.btnSaveSourceClassificationChanges.TabIndex = 18
Me.btnSaveSourceClassificationChanges.Text = "Save"
Me.btnSaveSourceClassificationChanges.UseVisualStyleBackColor = true
'
'txtSourceClassificationChangeComment
'
Me.txtSourceClassificationChangeComment.Location = New System.Drawing.Point(17, 41)
Me.txtSourceClassificationChangeComment.Multiline = true
Me.txtSourceClassificationChangeComment.Name = "txtSourceClassificationChangeComment"
Me.txtSourceClassificationChangeComment.Size = New System.Drawing.Size(428, 64)
Me.txtSourceClassificationChangeComment.TabIndex = 17
'
'Label36
'
Me.Label36.AutoSize = true
Me.Label36.Location = New System.Drawing.Point(6, 16)
Me.Label36.Name = "Label36"
Me.Label36.Size = New System.Drawing.Size(345, 13)
Me.Label36.TabIndex = 15
Me.Label36.Text = "Has there been a Source Classification change since January 1, 2006? "
'
'gbOwnership
'
Me.gbOwnership.Controls.Add(Me.btnSaveOwnershipChanges)
Me.gbOwnership.Controls.Add(Me.txtOwnershipChangeComments)
Me.gbOwnership.Controls.Add(Me.Panel4)
Me.gbOwnership.Controls.Add(Me.Label11)
Me.gbOwnership.Dock = System.Windows.Forms.DockStyle.Top
Me.gbOwnership.Location = New System.Drawing.Point(3, 3)
Me.gbOwnership.Name = "gbOwnership"
Me.gbOwnership.Size = New System.Drawing.Size(747, 119)
Me.gbOwnership.TabIndex = 8
Me.gbOwnership.TabStop = false
Me.gbOwnership.Text = "Ownership"
'
'btnSaveOwnershipChanges
'
Me.btnSaveOwnershipChanges.AutoSize = true
Me.btnSaveOwnershipChanges.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveOwnershipChanges.Location = New System.Drawing.Point(454, 11)
Me.btnSaveOwnershipChanges.Name = "btnSaveOwnershipChanges"
Me.btnSaveOwnershipChanges.Size = New System.Drawing.Size(42, 23)
Me.btnSaveOwnershipChanges.TabIndex = 18
Me.btnSaveOwnershipChanges.Text = "Save"
Me.btnSaveOwnershipChanges.UseVisualStyleBackColor = true
'
'txtOwnershipChangeComments
'
Me.txtOwnershipChangeComments.Location = New System.Drawing.Point(17, 41)
Me.txtOwnershipChangeComments.Multiline = true
Me.txtOwnershipChangeComments.Name = "txtOwnershipChangeComments"
Me.txtOwnershipChangeComments.Size = New System.Drawing.Size(390, 64)
Me.txtOwnershipChangeComments.TabIndex = 17
'
'Panel4
'
Me.Panel4.AutoSize = true
Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel4.Controls.Add(Me.rdbOwnershipChangeNo)
Me.Panel4.Controls.Add(Me.rdbOwnershipChangeYes)
Me.Panel4.Location = New System.Drawing.Point(313, 11)
Me.Panel4.Name = "Panel4"
Me.Panel4.Size = New System.Drawing.Size(94, 23)
Me.Panel4.TabIndex = 16
'
'rdbOwnershipChangeNo
'
Me.rdbOwnershipChangeNo.AutoSize = true
Me.rdbOwnershipChangeNo.Location = New System.Drawing.Point(50, 3)
Me.rdbOwnershipChangeNo.Name = "rdbOwnershipChangeNo"
Me.rdbOwnershipChangeNo.Size = New System.Drawing.Size(41, 17)
Me.rdbOwnershipChangeNo.TabIndex = 1
Me.rdbOwnershipChangeNo.TabStop = true
Me.rdbOwnershipChangeNo.Text = "NO"
Me.rdbOwnershipChangeNo.UseVisualStyleBackColor = true
'
'rdbOwnershipChangeYes
'
Me.rdbOwnershipChangeYes.AutoSize = true
Me.rdbOwnershipChangeYes.Location = New System.Drawing.Point(3, 3)
Me.rdbOwnershipChangeYes.Name = "rdbOwnershipChangeYes"
Me.rdbOwnershipChangeYes.Size = New System.Drawing.Size(46, 17)
Me.rdbOwnershipChangeYes.TabIndex = 0
Me.rdbOwnershipChangeYes.TabStop = true
Me.rdbOwnershipChangeYes.Text = "YES"
Me.rdbOwnershipChangeYes.UseVisualStyleBackColor = true
'
'Label11
'
Me.Label11.AutoSize = true
Me.Label11.Location = New System.Drawing.Point(6, 16)
Me.Label11.Name = "Label11"
Me.Label11.Size = New System.Drawing.Size(301, 13)
Me.Label11.TabIndex = 15
Me.Label11.Text = "Has there been an ownership change since January 1, 2007? "
'
'TP_Comments
'
Me.TP_Comments.AutoScroll = true
Me.TP_Comments.Controls.Add(Me.gbComments)
Me.TP_Comments.Location = New System.Drawing.Point(4, 22)
Me.TP_Comments.Name = "TP_Comments"
Me.TP_Comments.Padding = New System.Windows.Forms.Padding(3)
Me.TP_Comments.Size = New System.Drawing.Size(770, 198)
Me.TP_Comments.TabIndex = 4
Me.TP_Comments.Text = "Other Comments"
Me.TP_Comments.UseVisualStyleBackColor = true
'
'gbComments
'
Me.gbComments.Controls.Add(Me.btnSaveComments)
Me.gbComments.Controls.Add(Me.txtComments)
Me.gbComments.Dock = System.Windows.Forms.DockStyle.Top
Me.gbComments.Location = New System.Drawing.Point(3, 3)
Me.gbComments.Name = "gbComments"
Me.gbComments.Size = New System.Drawing.Size(764, 173)
Me.gbComments.TabIndex = 10
Me.gbComments.TabStop = false
Me.gbComments.Text = "Additional Fee Comments"
'
'btnSaveComments
'
Me.btnSaveComments.AutoSize = true
Me.btnSaveComments.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveComments.Location = New System.Drawing.Point(622, 19)
Me.btnSaveComments.Name = "btnSaveComments"
Me.btnSaveComments.Size = New System.Drawing.Size(94, 23)
Me.btnSaveComments.TabIndex = 8
Me.btnSaveComments.Text = "Save Comments"
Me.btnSaveComments.UseVisualStyleBackColor = true
'
'txtComments
'
Me.txtComments.Location = New System.Drawing.Point(6, 19)
Me.txtComments.Multiline = true
Me.txtComments.Name = "txtComments"
Me.txtComments.Size = New System.Drawing.Size(610, 121)
Me.txtComments.TabIndex = 7
'
'gbTopData
'
Me.gbTopData.AutoSize = true
Me.gbTopData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.gbTopData.Controls.Add(Me.Panel13)
Me.gbTopData.Controls.Add(Me.Panel10)
Me.gbTopData.Controls.Add(Me.Panel9)
Me.gbTopData.Controls.Add(Me.Label2)
Me.gbTopData.Dock = System.Windows.Forms.DockStyle.Top
Me.gbTopData.Location = New System.Drawing.Point(0, 0)
Me.gbTopData.Name = "gbTopData"
Me.gbTopData.Size = New System.Drawing.Size(778, 425)
Me.gbTopData.TabIndex = 0
Me.gbTopData.TabStop = false
Me.gbTopData.Text = "Current Facility and Fees Contact Information"
'
'Panel13
'
Me.Panel13.Controls.Add(Me.btnFlagNonResponder)
Me.Panel13.Controls.Add(Me.Panel105)
Me.Panel13.Controls.Add(Me.Label65)
Me.Panel13.Controls.Add(Me.btnSaveCurrentChange)
Me.Panel13.Controls.Add(Me.txtCurrentComments)
Me.Panel13.Controls.Add(Me.Label4)
Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel13.Location = New System.Drawing.Point(3, 304)
Me.Panel13.Name = "Panel13"
Me.Panel13.Size = New System.Drawing.Size(772, 118)
Me.Panel13.TabIndex = 72
Me.Panel13.Visible = false
'
'btnFlagNonResponder
'
Me.btnFlagNonResponder.AutoSize = true
Me.btnFlagNonResponder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnFlagNonResponder.Location = New System.Drawing.Point(192, 89)
Me.btnFlagNonResponder.Name = "btnFlagNonResponder"
Me.btnFlagNonResponder.Size = New System.Drawing.Size(42, 23)
Me.btnFlagNonResponder.TabIndex = 379
Me.btnFlagNonResponder.Text = "Save"
Me.btnFlagNonResponder.UseVisualStyleBackColor = true
'
'Panel105
'
Me.Panel105.AutoSize = true
Me.Panel105.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel105.Controls.Add(Me.rdbNonResponderActive)
Me.Panel105.Controls.Add(Me.rdbNonResponderInactive)
Me.Panel105.Location = New System.Drawing.Point(92, 89)
Me.Panel105.Name = "Panel105"
Me.Panel105.Size = New System.Drawing.Size(94, 23)
Me.Panel105.TabIndex = 378
'
'rdbNonResponderActive
'
Me.rdbNonResponderActive.AutoSize = true
Me.rdbNonResponderActive.Location = New System.Drawing.Point(50, 3)
Me.rdbNonResponderActive.Name = "rdbNonResponderActive"
Me.rdbNonResponderActive.Size = New System.Drawing.Size(41, 17)
Me.rdbNonResponderActive.TabIndex = 1
Me.rdbNonResponderActive.TabStop = true
Me.rdbNonResponderActive.Text = "NO"
Me.rdbNonResponderActive.UseVisualStyleBackColor = true
'
'rdbNonResponderInactive
'
Me.rdbNonResponderInactive.AutoSize = true
Me.rdbNonResponderInactive.Location = New System.Drawing.Point(3, 3)
Me.rdbNonResponderInactive.Name = "rdbNonResponderInactive"
Me.rdbNonResponderInactive.Size = New System.Drawing.Size(46, 17)
Me.rdbNonResponderInactive.TabIndex = 0
Me.rdbNonResponderInactive.TabStop = true
Me.rdbNonResponderInactive.Text = "YES"
Me.rdbNonResponderInactive.UseVisualStyleBackColor = true
'
'Label65
'
Me.Label65.AutoSize = true
Me.Label65.Location = New System.Drawing.Point(7, 94)
Me.Label65.Name = "Label65"
Me.Label65.Size = New System.Drawing.Size(82, 13)
Me.Label65.TabIndex = 377
Me.Label65.Text = "Flag as Inactive"
'
'btnSaveCurrentChange
'
Me.btnSaveCurrentChange.AutoSize = true
Me.btnSaveCurrentChange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveCurrentChange.Location = New System.Drawing.Point(509, 19)
Me.btnSaveCurrentChange.Name = "btnSaveCurrentChange"
Me.btnSaveCurrentChange.Size = New System.Drawing.Size(94, 23)
Me.btnSaveCurrentChange.TabIndex = 7
Me.btnSaveCurrentChange.Text = "Save Comments"
Me.btnSaveCurrentChange.UseVisualStyleBackColor = true
'
'txtCurrentComments
'
Me.txtCurrentComments.Location = New System.Drawing.Point(10, 19)
Me.txtCurrentComments.Multiline = true
Me.txtCurrentComments.Name = "txtCurrentComments"
Me.txtCurrentComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtCurrentComments.Size = New System.Drawing.Size(493, 64)
Me.txtCurrentComments.TabIndex = 6
'
'Label4
'
Me.Label4.AutoSize = true
Me.Label4.Location = New System.Drawing.Point(3, 3)
Me.Label4.Name = "Label4"
Me.Label4.Size = New System.Drawing.Size(231, 13)
Me.Label4.TabIndex = 5
Me.Label4.Text = "Comments for Current Facility and Contact Info: "
'
'Panel10
'
Me.Panel10.Controls.Add(Me.Panel11)
Me.Panel10.Controls.Add(Me.Panel12)
Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel10.Location = New System.Drawing.Point(3, 138)
Me.Panel10.Name = "Panel10"
Me.Panel10.Size = New System.Drawing.Size(772, 166)
Me.Panel10.TabIndex = 71
Me.Panel10.Visible = false
'
'Panel11
'
Me.Panel11.Controls.Add(Me.txtEditContactEmailAddress)
Me.Panel11.Controls.Add(Me.txtEditContactState)
Me.Panel11.Controls.Add(Me.btnEditContactInfo)
Me.Panel11.Controls.Add(Me.txtEditContactPhoneNumber)
Me.Panel11.Controls.Add(Me.mtbEditContactZipCode)
Me.Panel11.Controls.Add(Me.txtEditContactCity)
Me.Panel11.Controls.Add(Me.txtEditContactAddress)
Me.Panel11.Controls.Add(Me.txtEditContactCompany)
Me.Panel11.Controls.Add(Me.txtEditContactTitle)
Me.Panel11.Controls.Add(Me.txtEditContactLastName)
Me.Panel11.Controls.Add(Me.txtEditContactFirstName)
Me.Panel11.Controls.Add(Me.Label5)
Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel11.Location = New System.Drawing.Point(343, 0)
Me.Panel11.Name = "Panel11"
Me.Panel11.Size = New System.Drawing.Size(429, 166)
Me.Panel11.TabIndex = 71
'
'txtEditContactEmailAddress
'
Me.txtEditContactEmailAddress.Location = New System.Drawing.Point(174, 138)
Me.txtEditContactEmailAddress.Name = "txtEditContactEmailAddress"
Me.txtEditContactEmailAddress.Size = New System.Drawing.Size(179, 20)
Me.txtEditContactEmailAddress.TabIndex = 10
Me.txtEditContactEmailAddress.Text = "Contact Email Address"
'
'txtEditContactState
'
Me.txtEditContactState.Location = New System.Drawing.Point(146, 113)
Me.txtEditContactState.MaxLength = 2
Me.txtEditContactState.Name = "txtEditContactState"
Me.txtEditContactState.Size = New System.Drawing.Size(25, 20)
Me.txtEditContactState.TabIndex = 6
'
'btnEditContactInfo
'
Me.btnEditContactInfo.AutoSize = true
Me.btnEditContactInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnEditContactInfo.Location = New System.Drawing.Point(247, 16)
Me.btnEditContactInfo.Name = "btnEditContactInfo"
Me.btnEditContactInfo.Size = New System.Drawing.Size(106, 23)
Me.btnEditContactInfo.TabIndex = 9
Me.btnEditContactInfo.Text = "Save Contact Info."
Me.btnEditContactInfo.UseVisualStyleBackColor = true
'
'txtEditContactPhoneNumber
'
Me.txtEditContactPhoneNumber.Location = New System.Drawing.Point(15, 138)
Me.txtEditContactPhoneNumber.Name = "txtEditContactPhoneNumber"
Me.txtEditContactPhoneNumber.Size = New System.Drawing.Size(142, 20)
Me.txtEditContactPhoneNumber.TabIndex = 8
Me.txtEditContactPhoneNumber.Text = "Contact Phone Number"
'
'mtbEditContactZipCode
'
Me.mtbEditContactZipCode.Location = New System.Drawing.Point(174, 113)
Me.mtbEditContactZipCode.Mask = "00000-9999"
Me.mtbEditContactZipCode.Name = "mtbEditContactZipCode"
Me.mtbEditContactZipCode.Size = New System.Drawing.Size(67, 20)
Me.mtbEditContactZipCode.TabIndex = 7
Me.mtbEditContactZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'txtEditContactCity
'
Me.txtEditContactCity.Location = New System.Drawing.Point(15, 113)
Me.txtEditContactCity.Name = "txtEditContactCity"
Me.txtEditContactCity.Size = New System.Drawing.Size(125, 20)
Me.txtEditContactCity.TabIndex = 5
Me.txtEditContactCity.Text = "Contact City"
'
'txtEditContactAddress
'
Me.txtEditContactAddress.Location = New System.Drawing.Point(15, 89)
Me.txtEditContactAddress.Name = "txtEditContactAddress"
Me.txtEditContactAddress.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactAddress.TabIndex = 4
Me.txtEditContactAddress.Text = "Contact Address"
'
'txtEditContactCompany
'
Me.txtEditContactCompany.Location = New System.Drawing.Point(15, 65)
Me.txtEditContactCompany.Name = "txtEditContactCompany"
Me.txtEditContactCompany.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactCompany.TabIndex = 3
Me.txtEditContactCompany.Text = "Contact Company"
'
'txtEditContactTitle
'
Me.txtEditContactTitle.Location = New System.Drawing.Point(15, 42)
Me.txtEditContactTitle.Name = "txtEditContactTitle"
Me.txtEditContactTitle.Size = New System.Drawing.Size(226, 20)
Me.txtEditContactTitle.TabIndex = 2
Me.txtEditContactTitle.Text = "Contact Title"
'
'txtEditContactLastName
'
Me.txtEditContactLastName.Location = New System.Drawing.Point(130, 19)
Me.txtEditContactLastName.Name = "txtEditContactLastName"
Me.txtEditContactLastName.Size = New System.Drawing.Size(111, 20)
Me.txtEditContactLastName.TabIndex = 1
Me.txtEditContactLastName.Text = "Contact Last Name"
'
'txtEditContactFirstName
'
Me.txtEditContactFirstName.Location = New System.Drawing.Point(15, 19)
Me.txtEditContactFirstName.Name = "txtEditContactFirstName"
Me.txtEditContactFirstName.Size = New System.Drawing.Size(109, 20)
Me.txtEditContactFirstName.TabIndex = 0
Me.txtEditContactFirstName.Text = "Contact First Name"
'
'Label5
'
Me.Label5.AutoSize = true
Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label5.Location = New System.Drawing.Point(3, 3)
Me.Label5.Name = "Label5"
Me.Label5.Size = New System.Drawing.Size(118, 13)
Me.Label5.TabIndex = 1
Me.Label5.Text = "Contact Information"
'
'Panel12
'
Me.Panel12.Controls.Add(Me.txtEditSourceClass)
Me.Panel12.Controls.Add(Me.cboOperatingStatus)
Me.Panel12.Controls.Add(Me.Panel15)
Me.Panel12.Controls.Add(Me.Panel14)
Me.Panel12.Controls.Add(Me.Label7)
Me.Panel12.Controls.Add(Me.Label8)
Me.Panel12.Controls.Add(Me.Label9)
Me.Panel12.Controls.Add(Me.Label10)
Me.Panel12.Controls.Add(Me.btnEditFacilityInfo)
Me.Panel12.Controls.Add(Me.mtbEditZipCode)
Me.Panel12.Controls.Add(Me.Label3)
Me.Panel12.Controls.Add(Me.txtEditFacilityCity)
Me.Panel12.Controls.Add(Me.txtEditFacilityAddress)
Me.Panel12.Controls.Add(Me.txtEditFacilityName)
Me.Panel12.Controls.Add(Me.Label6)
Me.Panel12.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel12.Location = New System.Drawing.Point(0, 0)
Me.Panel12.Name = "Panel12"
Me.Panel12.Size = New System.Drawing.Size(343, 166)
Me.Panel12.TabIndex = 70
'
'txtEditSourceClass
'
Me.txtEditSourceClass.Location = New System.Drawing.Point(92, 119)
Me.txtEditSourceClass.Name = "txtEditSourceClass"
Me.txtEditSourceClass.Size = New System.Drawing.Size(50, 20)
Me.txtEditSourceClass.TabIndex = 18
'
'cboOperatingStatus
'
Me.cboOperatingStatus.FormattingEnabled = true
Me.cboOperatingStatus.Location = New System.Drawing.Point(72, 93)
Me.cboOperatingStatus.Name = "cboOperatingStatus"
Me.cboOperatingStatus.Size = New System.Drawing.Size(133, 21)
Me.cboOperatingStatus.TabIndex = 17
'
'Panel15
'
Me.Panel15.AutoSize = true
Me.Panel15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel15.Controls.Add(Me.rdbNSPSNo)
Me.Panel15.Controls.Add(Me.rdbNSPSYes)
Me.Panel15.Location = New System.Drawing.Point(246, 119)
Me.Panel15.Name = "Panel15"
Me.Panel15.Size = New System.Drawing.Size(94, 23)
Me.Panel15.TabIndex = 15
'
'rdbNSPSNo
'
Me.rdbNSPSNo.AutoSize = true
Me.rdbNSPSNo.Location = New System.Drawing.Point(50, 3)
Me.rdbNSPSNo.Name = "rdbNSPSNo"
Me.rdbNSPSNo.Size = New System.Drawing.Size(41, 17)
Me.rdbNSPSNo.TabIndex = 1
Me.rdbNSPSNo.TabStop = true
Me.rdbNSPSNo.Text = "NO"
Me.rdbNSPSNo.UseVisualStyleBackColor = true
'
'rdbNSPSYes
'
Me.rdbNSPSYes.AutoSize = true
Me.rdbNSPSYes.Location = New System.Drawing.Point(3, 3)
Me.rdbNSPSYes.Name = "rdbNSPSYes"
Me.rdbNSPSYes.Size = New System.Drawing.Size(46, 17)
Me.rdbNSPSYes.TabIndex = 0
Me.rdbNSPSYes.TabStop = true
Me.rdbNSPSYes.Text = "YES"
Me.rdbNSPSYes.UseVisualStyleBackColor = true
'
'Panel14
'
Me.Panel14.AutoSize = true
Me.Panel14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel14.Controls.Add(Me.rdbTVNo)
Me.Panel14.Controls.Add(Me.rdbTVYes)
Me.Panel14.Location = New System.Drawing.Point(246, 92)
Me.Panel14.Name = "Panel14"
Me.Panel14.Size = New System.Drawing.Size(94, 23)
Me.Panel14.TabIndex = 14
'
'rdbTVNo
'
Me.rdbTVNo.AutoSize = true
Me.rdbTVNo.Location = New System.Drawing.Point(50, 3)
Me.rdbTVNo.Name = "rdbTVNo"
Me.rdbTVNo.Size = New System.Drawing.Size(41, 17)
Me.rdbTVNo.TabIndex = 1
Me.rdbTVNo.TabStop = true
Me.rdbTVNo.Text = "NO"
Me.rdbTVNo.UseVisualStyleBackColor = true
'
'rdbTVYes
'
Me.rdbTVYes.AutoSize = true
Me.rdbTVYes.Location = New System.Drawing.Point(3, 3)
Me.rdbTVYes.Name = "rdbTVYes"
Me.rdbTVYes.Size = New System.Drawing.Size(46, 17)
Me.rdbTVYes.TabIndex = 0
Me.rdbTVYes.TabStop = true
Me.rdbTVYes.Text = "YES"
Me.rdbTVYes.UseVisualStyleBackColor = true
'
'Label7
'
Me.Label7.AutoSize = true
Me.Label7.Location = New System.Drawing.Point(204, 124)
Me.Label7.Name = "Label7"
Me.Label7.Size = New System.Drawing.Size(36, 13)
Me.Label7.TabIndex = 13
Me.Label7.Text = "NSPS"
'
'Label8
'
Me.Label8.AutoSize = true
Me.Label8.Location = New System.Drawing.Point(206, 97)
Me.Label8.Name = "Label8"
Me.Label8.Size = New System.Drawing.Size(37, 13)
Me.Label8.TabIndex = 12
Me.Label8.Text = "Title V"
'
'Label9
'
Me.Label9.AutoSize = true
Me.Label9.Location = New System.Drawing.Point(17, 123)
Me.Label9.Name = "Label9"
Me.Label9.Size = New System.Drawing.Size(69, 13)
Me.Label9.TabIndex = 11
Me.Label9.Text = "Source Class"
'
'Label10
'
Me.Label10.AutoSize = true
Me.Label10.Location = New System.Drawing.Point(17, 96)
Me.Label10.Name = "Label10"
Me.Label10.Size = New System.Drawing.Size(54, 13)
Me.Label10.TabIndex = 10
Me.Label10.Text = "Op Status"
'
'btnEditFacilityInfo
'
Me.btnEditFacilityInfo.AutoSize = true
Me.btnEditFacilityInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnEditFacilityInfo.Location = New System.Drawing.Point(211, 19)
Me.btnEditFacilityInfo.Name = "btnEditFacilityInfo"
Me.btnEditFacilityInfo.Size = New System.Drawing.Size(98, 23)
Me.btnEditFacilityInfo.TabIndex = 4
Me.btnEditFacilityInfo.Text = "Save Facility Info"
Me.btnEditFacilityInfo.UseVisualStyleBackColor = true
'
'mtbEditZipCode
'
Me.mtbEditZipCode.Location = New System.Drawing.Point(138, 65)
Me.mtbEditZipCode.Mask = "00000-9999"
Me.mtbEditZipCode.Name = "mtbEditZipCode"
Me.mtbEditZipCode.Size = New System.Drawing.Size(67, 20)
Me.mtbEditZipCode.TabIndex = 3
Me.mtbEditZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'Label3
'
Me.Label3.AutoSize = true
Me.Label3.Location = New System.Drawing.Point(113, 69)
Me.Label3.Name = "Label3"
Me.Label3.Size = New System.Drawing.Size(22, 13)
Me.Label3.TabIndex = 6
Me.Label3.Text = "GA"
'
'txtEditFacilityCity
'
Me.txtEditFacilityCity.Location = New System.Drawing.Point(15, 65)
Me.txtEditFacilityCity.Name = "txtEditFacilityCity"
Me.txtEditFacilityCity.Size = New System.Drawing.Size(95, 20)
Me.txtEditFacilityCity.TabIndex = 2
Me.txtEditFacilityCity.Text = "Facility City"
'
'txtEditFacilityAddress
'
Me.txtEditFacilityAddress.Location = New System.Drawing.Point(15, 42)
Me.txtEditFacilityAddress.Name = "txtEditFacilityAddress"
Me.txtEditFacilityAddress.Size = New System.Drawing.Size(190, 20)
Me.txtEditFacilityAddress.TabIndex = 1
Me.txtEditFacilityAddress.Text = "Facility Address"
'
'txtEditFacilityName
'
Me.txtEditFacilityName.Location = New System.Drawing.Point(15, 19)
Me.txtEditFacilityName.Name = "txtEditFacilityName"
Me.txtEditFacilityName.Size = New System.Drawing.Size(190, 20)
Me.txtEditFacilityName.TabIndex = 0
Me.txtEditFacilityName.Text = "Facility Name"
'
'Label6
'
Me.Label6.AutoSize = true
Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label6.Location = New System.Drawing.Point(3, 3)
Me.Label6.Name = "Label6"
Me.Label6.Size = New System.Drawing.Size(111, 13)
Me.Label6.TabIndex = 1
Me.Label6.Text = "Facilty Information"
'
'Panel9
'
Me.Panel9.Controls.Add(Me.Panel8)
Me.Panel9.Controls.Add(Me.Panel7)
Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel9.Location = New System.Drawing.Point(3, 16)
Me.Panel9.Name = "Panel9"
Me.Panel9.Size = New System.Drawing.Size(772, 122)
Me.Panel9.TabIndex = 70
'
'Panel8
'
Me.Panel8.Controls.Add(Me.lblContactEmailAddress)
Me.Panel8.Controls.Add(Me.lblContactPhoneNumber)
Me.Panel8.Controls.Add(Me.lblContactAddress2)
Me.Panel8.Controls.Add(Me.lblContactCompany)
Me.Panel8.Controls.Add(Me.lblContactTitle)
Me.Panel8.Controls.Add(Me.lblContactName)
Me.Panel8.Controls.Add(Me.lblContactAddress)
Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel8.Location = New System.Drawing.Point(343, 0)
Me.Panel8.Name = "Panel8"
Me.Panel8.Size = New System.Drawing.Size(429, 122)
Me.Panel8.TabIndex = 69
'
'lblContactEmailAddress
'
Me.lblContactEmailAddress.AutoSize = true
Me.lblContactEmailAddress.Location = New System.Drawing.Point(10, 100)
Me.lblContactEmailAddress.Name = "lblContactEmailAddress"
Me.lblContactEmailAddress.Size = New System.Drawing.Size(113, 13)
Me.lblContactEmailAddress.TabIndex = 11
Me.lblContactEmailAddress.Text = "Contact Email Address"
'
'lblContactPhoneNumber
'
Me.lblContactPhoneNumber.AutoSize = true
Me.lblContactPhoneNumber.Location = New System.Drawing.Point(10, 87)
Me.lblContactPhoneNumber.Name = "lblContactPhoneNumber"
Me.lblContactPhoneNumber.Size = New System.Drawing.Size(118, 13)
Me.lblContactPhoneNumber.TabIndex = 10
Me.lblContactPhoneNumber.Text = "Contact Phone Number"
'
'lblContactAddress2
'
Me.lblContactAddress2.AutoSize = true
Me.lblContactAddress2.Location = New System.Drawing.Point(44, 59)
Me.lblContactAddress2.Name = "lblContactAddress2"
Me.lblContactAddress2.Size = New System.Drawing.Size(7, 13)
Me.lblContactAddress2.TabIndex = 9
Me.lblContactAddress2.Text = ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
'
'lblContactCompany
'
Me.lblContactCompany.AutoSize = true
Me.lblContactCompany.Location = New System.Drawing.Point(10, 32)
Me.lblContactCompany.Name = "lblContactCompany"
Me.lblContactCompany.Size = New System.Drawing.Size(91, 13)
Me.lblContactCompany.TabIndex = 8
Me.lblContactCompany.Text = "Contact Company"
'
'lblContactTitle
'
Me.lblContactTitle.AutoSize = true
Me.lblContactTitle.Location = New System.Drawing.Point(10, 18)
Me.lblContactTitle.Name = "lblContactTitle"
Me.lblContactTitle.Size = New System.Drawing.Size(67, 13)
Me.lblContactTitle.TabIndex = 7
Me.lblContactTitle.Text = "Contact Title"
'
'lblContactName
'
Me.lblContactName.AutoSize = true
Me.lblContactName.Location = New System.Drawing.Point(10, 4)
Me.lblContactName.Name = "lblContactName"
Me.lblContactName.Size = New System.Drawing.Size(75, 13)
Me.lblContactName.TabIndex = 6
Me.lblContactName.Text = "Contact Name"
'
'lblContactAddress
'
Me.lblContactAddress.AutoSize = true
Me.lblContactAddress.Location = New System.Drawing.Point(10, 46)
Me.lblContactAddress.Name = "lblContactAddress"
Me.lblContactAddress.Size = New System.Drawing.Size(85, 13)
Me.lblContactAddress.TabIndex = 1
Me.lblContactAddress.Text = "Contact Address"
'
'Panel7
'
Me.Panel7.Controls.Add(Me.lblNonResponderStaff)
Me.Panel7.Controls.Add(Me.lblNSPS)
Me.Panel7.Controls.Add(Me.lblTitleV)
Me.Panel7.Controls.Add(Me.lblSourceClass)
Me.Panel7.Controls.Add(Me.lblOperatingStatus)
Me.Panel7.Controls.Add(Me.lblFacilityAddress2)
Me.Panel7.Controls.Add(Me.lblFacilityAddress)
Me.Panel7.Controls.Add(Me.llbNoteChanges)
Me.Panel7.Controls.Add(Me.lblFacilityName)
Me.Panel7.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel7.Location = New System.Drawing.Point(0, 0)
Me.Panel7.Name = "Panel7"
Me.Panel7.Size = New System.Drawing.Size(343, 122)
Me.Panel7.TabIndex = 68
'
'lblNonResponderStaff
'
Me.lblNonResponderStaff.AutoSize = true
Me.lblNonResponderStaff.Location = New System.Drawing.Point(3, 5)
Me.lblNonResponderStaff.Name = "lblNonResponderStaff"
Me.lblNonResponderStaff.Size = New System.Drawing.Size(90, 13)
Me.lblNonResponderStaff.TabIndex = 384
Me.lblNonResponderStaff.Text = "Staff Responsible"
'
'lblNSPS
'
Me.lblNSPS.AutoSize = true
Me.lblNSPS.Location = New System.Drawing.Point(197, 85)
Me.lblNSPS.Name = "lblNSPS"
Me.lblNSPS.Size = New System.Drawing.Size(36, 13)
Me.lblNSPS.TabIndex = 9
Me.lblNSPS.Text = "NSPS"
'
'lblTitleV
'
Me.lblTitleV.AutoSize = true
Me.lblTitleV.Location = New System.Drawing.Point(197, 70)
Me.lblTitleV.Name = "lblTitleV"
Me.lblTitleV.Size = New System.Drawing.Size(37, 13)
Me.lblTitleV.TabIndex = 8
Me.lblTitleV.Text = "Title V"
'
'lblSourceClass
'
Me.lblSourceClass.AutoSize = true
Me.lblSourceClass.Location = New System.Drawing.Point(7, 85)
Me.lblSourceClass.Name = "lblSourceClass"
Me.lblSourceClass.Size = New System.Drawing.Size(69, 13)
Me.lblSourceClass.TabIndex = 7
Me.lblSourceClass.Text = "Source Class"
'
'lblOperatingStatus
'
Me.lblOperatingStatus.AutoSize = true
Me.lblOperatingStatus.Location = New System.Drawing.Point(7, 70)
Me.lblOperatingStatus.Name = "lblOperatingStatus"
Me.lblOperatingStatus.Size = New System.Drawing.Size(86, 13)
Me.lblOperatingStatus.TabIndex = 6
Me.lblOperatingStatus.Text = "Operating Status"
'
'lblFacilityAddress2
'
Me.lblFacilityAddress2.AutoSize = true
Me.lblFacilityAddress2.Location = New System.Drawing.Point(32, 47)
Me.lblFacilityAddress2.Name = "lblFacilityAddress2"
Me.lblFacilityAddress2.Size = New System.Drawing.Size(0, 13)
Me.lblFacilityAddress2.TabIndex = 5
'
'lblFacilityAddress
'
Me.lblFacilityAddress.AutoSize = true
Me.lblFacilityAddress.Location = New System.Drawing.Point(3, 33)
Me.lblFacilityAddress.Name = "lblFacilityAddress"
Me.lblFacilityAddress.Size = New System.Drawing.Size(78, 13)
Me.lblFacilityAddress.TabIndex = 4
Me.lblFacilityAddress.Text = "Facilty Address"
'
'llbNoteChanges
'
Me.llbNoteChanges.AutoSize = true
Me.llbNoteChanges.Location = New System.Drawing.Point(7, 100)
Me.llbNoteChanges.Name = "llbNoteChanges"
Me.llbNoteChanges.Size = New System.Drawing.Size(75, 13)
Me.llbNoteChanges.TabIndex = 0
Me.llbNoteChanges.TabStop = true
Me.llbNoteChanges.Text = "Note Changes"
'
'lblFacilityName
'
Me.lblFacilityName.AutoSize = true
Me.lblFacilityName.Location = New System.Drawing.Point(3, 19)
Me.lblFacilityName.Name = "lblFacilityName"
Me.lblFacilityName.Size = New System.Drawing.Size(68, 13)
Me.lblFacilityName.TabIndex = 1
Me.lblFacilityName.Text = "Facilty Name"
'
'Label2
'
Me.Label2.AutoSize = true
Me.Label2.Location = New System.Drawing.Point(378, 16)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(65, 13)
Me.Label2.TabIndex = 3
Me.Label2.Text = "Fee Contact"
'
'TPNonPayers
'
Me.TPNonPayers.Controls.Add(Me.GBContactInformation)
Me.TPNonPayers.Controls.Add(Me.GroupBox2)
Me.TPNonPayers.Location = New System.Drawing.Point(4, 22)
Me.TPNonPayers.Name = "TPNonPayers"
Me.TPNonPayers.Padding = New System.Windows.Forms.Padding(3)
Me.TPNonPayers.Size = New System.Drawing.Size(784, 635)
Me.TPNonPayers.TabIndex = 3
Me.TPNonPayers.Text = "Non-Payers"
Me.TPNonPayers.UseVisualStyleBackColor = true
'
'GBContactInformation
'
Me.GBContactInformation.Controls.Add(Me.txtNonPayerEmail)
Me.GBContactInformation.Controls.Add(Me.txtNonPayerState)
Me.GBContactInformation.Controls.Add(Me.btnNonPayerSave)
Me.GBContactInformation.Controls.Add(Me.txtNonPayerPhoneNumber)
Me.GBContactInformation.Controls.Add(Me.mtbNonPayerZipCode)
Me.GBContactInformation.Controls.Add(Me.txtNonPayerCity)
Me.GBContactInformation.Controls.Add(Me.txtNonPayerAddress)
Me.GBContactInformation.Controls.Add(Me.txtNonPayerCompany)
Me.GBContactInformation.Controls.Add(Me.txtNonPayerTitle)
Me.GBContactInformation.Controls.Add(Me.txtNonPayerLastName)
Me.GBContactInformation.Controls.Add(Me.txtNonPayerFirstname)
Me.GBContactInformation.Dock = System.Windows.Forms.DockStyle.Top
Me.GBContactInformation.Location = New System.Drawing.Point(3, 649)
Me.GBContactInformation.Name = "GBContactInformation"
Me.GBContactInformation.Size = New System.Drawing.Size(778, 166)
Me.GBContactInformation.TabIndex = 1
Me.GBContactInformation.TabStop = false
Me.GBContactInformation.Text = "Save && Edit Contact Information"
Me.GBContactInformation.Visible = false
'
'txtNonPayerEmail
'
Me.txtNonPayerEmail.Location = New System.Drawing.Point(168, 138)
Me.txtNonPayerEmail.Name = "txtNonPayerEmail"
Me.txtNonPayerEmail.Size = New System.Drawing.Size(179, 20)
Me.txtNonPayerEmail.TabIndex = 405
Me.txtNonPayerEmail.Text = "Contact Email Address"
'
'txtNonPayerState
'
Me.txtNonPayerState.Location = New System.Drawing.Point(140, 113)
Me.txtNonPayerState.MaxLength = 2
Me.txtNonPayerState.Name = "txtNonPayerState"
Me.txtNonPayerState.Size = New System.Drawing.Size(25, 20)
Me.txtNonPayerState.TabIndex = 401
'
'btnNonPayerSave
'
Me.btnNonPayerSave.AutoSize = true
Me.btnNonPayerSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnNonPayerSave.Location = New System.Drawing.Point(241, 16)
Me.btnNonPayerSave.Name = "btnNonPayerSave"
Me.btnNonPayerSave.Size = New System.Drawing.Size(106, 23)
Me.btnNonPayerSave.TabIndex = 404
Me.btnNonPayerSave.Text = "Save Contact Info."
Me.btnNonPayerSave.UseVisualStyleBackColor = true
'
'txtNonPayerPhoneNumber
'
Me.txtNonPayerPhoneNumber.Location = New System.Drawing.Point(9, 138)
Me.txtNonPayerPhoneNumber.Name = "txtNonPayerPhoneNumber"
Me.txtNonPayerPhoneNumber.Size = New System.Drawing.Size(142, 20)
Me.txtNonPayerPhoneNumber.TabIndex = 403
Me.txtNonPayerPhoneNumber.Text = "Contact Phone Number"
'
'mtbNonPayerZipCode
'
Me.mtbNonPayerZipCode.Location = New System.Drawing.Point(168, 113)
Me.mtbNonPayerZipCode.Mask = "00000-9999"
Me.mtbNonPayerZipCode.Name = "mtbNonPayerZipCode"
Me.mtbNonPayerZipCode.Size = New System.Drawing.Size(67, 20)
Me.mtbNonPayerZipCode.TabIndex = 402
Me.mtbNonPayerZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'txtNonPayerCity
'
Me.txtNonPayerCity.Location = New System.Drawing.Point(9, 113)
Me.txtNonPayerCity.Name = "txtNonPayerCity"
Me.txtNonPayerCity.Size = New System.Drawing.Size(125, 20)
Me.txtNonPayerCity.TabIndex = 400
Me.txtNonPayerCity.Text = "Contact City"
'
'txtNonPayerAddress
'
Me.txtNonPayerAddress.Location = New System.Drawing.Point(9, 89)
Me.txtNonPayerAddress.Name = "txtNonPayerAddress"
Me.txtNonPayerAddress.Size = New System.Drawing.Size(226, 20)
Me.txtNonPayerAddress.TabIndex = 399
Me.txtNonPayerAddress.Text = "Contact Address"
'
'txtNonPayerCompany
'
Me.txtNonPayerCompany.Location = New System.Drawing.Point(9, 65)
Me.txtNonPayerCompany.Name = "txtNonPayerCompany"
Me.txtNonPayerCompany.Size = New System.Drawing.Size(226, 20)
Me.txtNonPayerCompany.TabIndex = 398
Me.txtNonPayerCompany.Text = "Contact Company"
'
'txtNonPayerTitle
'
Me.txtNonPayerTitle.Location = New System.Drawing.Point(9, 42)
Me.txtNonPayerTitle.Name = "txtNonPayerTitle"
Me.txtNonPayerTitle.Size = New System.Drawing.Size(226, 20)
Me.txtNonPayerTitle.TabIndex = 397
Me.txtNonPayerTitle.Text = "Contact Title"
'
'txtNonPayerLastName
'
Me.txtNonPayerLastName.Location = New System.Drawing.Point(124, 19)
Me.txtNonPayerLastName.Name = "txtNonPayerLastName"
Me.txtNonPayerLastName.Size = New System.Drawing.Size(111, 20)
Me.txtNonPayerLastName.TabIndex = 396
Me.txtNonPayerLastName.Text = "Contact Last Name"
'
'txtNonPayerFirstname
'
Me.txtNonPayerFirstname.Location = New System.Drawing.Point(9, 19)
Me.txtNonPayerFirstname.Name = "txtNonPayerFirstname"
Me.txtNonPayerFirstname.Size = New System.Drawing.Size(109, 20)
Me.txtNonPayerFirstname.TabIndex = 395
Me.txtNonPayerFirstname.Text = "Contact First Name"
'
'GroupBox2
'
Me.GroupBox2.AutoSize = true
Me.GroupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.GroupBox2.Controls.Add(Me.GBNonPayer_CY02)
Me.GroupBox2.Controls.Add(Me.GBNonPayer_CY03)
Me.GroupBox2.Controls.Add(Me.GBNonPayer_CY04)
Me.GroupBox2.Controls.Add(Me.GBNonPayer_CY05)
Me.GroupBox2.Controls.Add(Me.GBNonPayer_CY06)
Me.GroupBox2.Controls.Add(Me.GBNonPayer_CY07)
Me.GroupBox2.Controls.Add(Me.GBNonPayer_CY08)
Me.GroupBox2.Controls.Add(Me.Panel102)
Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
Me.GroupBox2.Name = "GroupBox2"
Me.GroupBox2.Size = New System.Drawing.Size(778, 646)
Me.GroupBox2.TabIndex = 0
Me.GroupBox2.TabStop = false
Me.GroupBox2.Text = "Current Facility Information"
'
'GBNonPayer_CY02
'
Me.GBNonPayer_CY02.Controls.Add(Me.lblNonPayerBalance_CY02)
Me.GBNonPayer_CY02.Controls.Add(Me.lblNonPayerTotalPaid_CY02)
Me.GBNonPayer_CY02.Controls.Add(Me.lblNonPayerTotalDue_CY02)
Me.GBNonPayer_CY02.Dock = System.Windows.Forms.DockStyle.Top
Me.GBNonPayer_CY02.Location = New System.Drawing.Point(3, 585)
Me.GBNonPayer_CY02.Name = "GBNonPayer_CY02"
Me.GBNonPayer_CY02.Size = New System.Drawing.Size(772, 58)
Me.GBNonPayer_CY02.TabIndex = 8
Me.GBNonPayer_CY02.TabStop = false
Me.GBNonPayer_CY02.Text = "CY 2002"
Me.GBNonPayer_CY02.Visible = false
'
'lblNonPayerBalance_CY02
'
Me.lblNonPayerBalance_CY02.AutoSize = true
Me.lblNonPayerBalance_CY02.Location = New System.Drawing.Point(167, 16)
Me.lblNonPayerBalance_CY02.Name = "lblNonPayerBalance_CY02"
Me.lblNonPayerBalance_CY02.Size = New System.Drawing.Size(46, 13)
Me.lblNonPayerBalance_CY02.TabIndex = 14
Me.lblNonPayerBalance_CY02.Text = "Balance"
'
'lblNonPayerTotalPaid_CY02
'
Me.lblNonPayerTotalPaid_CY02.AutoSize = true
Me.lblNonPayerTotalPaid_CY02.Location = New System.Drawing.Point(7, 35)
Me.lblNonPayerTotalPaid_CY02.Name = "lblNonPayerTotalPaid_CY02"
Me.lblNonPayerTotalPaid_CY02.Size = New System.Drawing.Size(55, 13)
Me.lblNonPayerTotalPaid_CY02.TabIndex = 13
Me.lblNonPayerTotalPaid_CY02.Text = "Total Paid"
'
'lblNonPayerTotalDue_CY02
'
Me.lblNonPayerTotalDue_CY02.AutoSize = true
Me.lblNonPayerTotalDue_CY02.Location = New System.Drawing.Point(7, 16)
Me.lblNonPayerTotalDue_CY02.Name = "lblNonPayerTotalDue_CY02"
Me.lblNonPayerTotalDue_CY02.Size = New System.Drawing.Size(54, 13)
Me.lblNonPayerTotalDue_CY02.TabIndex = 12
Me.lblNonPayerTotalDue_CY02.Text = "Total Due"
'
'GBNonPayer_CY03
'
Me.GBNonPayer_CY03.Controls.Add(Me.lblNonPayerBalance_CY03)
Me.GBNonPayer_CY03.Controls.Add(Me.lblNonPayerTotalPaid_CY03)
Me.GBNonPayer_CY03.Controls.Add(Me.lblNonPayerTotalDue_CY03)
Me.GBNonPayer_CY03.Dock = System.Windows.Forms.DockStyle.Top
Me.GBNonPayer_CY03.Location = New System.Drawing.Point(3, 527)
Me.GBNonPayer_CY03.Name = "GBNonPayer_CY03"
Me.GBNonPayer_CY03.Size = New System.Drawing.Size(772, 58)
Me.GBNonPayer_CY03.TabIndex = 7
Me.GBNonPayer_CY03.TabStop = false
Me.GBNonPayer_CY03.Text = "CY 2003"
Me.GBNonPayer_CY03.Visible = false
'
'lblNonPayerBalance_CY03
'
Me.lblNonPayerBalance_CY03.AutoSize = true
Me.lblNonPayerBalance_CY03.Location = New System.Drawing.Point(165, 16)
Me.lblNonPayerBalance_CY03.Name = "lblNonPayerBalance_CY03"
Me.lblNonPayerBalance_CY03.Size = New System.Drawing.Size(46, 13)
Me.lblNonPayerBalance_CY03.TabIndex = 14
Me.lblNonPayerBalance_CY03.Text = "Balance"
'
'lblNonPayerTotalPaid_CY03
'
Me.lblNonPayerTotalPaid_CY03.AutoSize = true
Me.lblNonPayerTotalPaid_CY03.Location = New System.Drawing.Point(5, 35)
Me.lblNonPayerTotalPaid_CY03.Name = "lblNonPayerTotalPaid_CY03"
Me.lblNonPayerTotalPaid_CY03.Size = New System.Drawing.Size(55, 13)
Me.lblNonPayerTotalPaid_CY03.TabIndex = 13
Me.lblNonPayerTotalPaid_CY03.Text = "Total Paid"
'
'lblNonPayerTotalDue_CY03
'
Me.lblNonPayerTotalDue_CY03.AutoSize = true
Me.lblNonPayerTotalDue_CY03.Location = New System.Drawing.Point(7, 16)
Me.lblNonPayerTotalDue_CY03.Name = "lblNonPayerTotalDue_CY03"
Me.lblNonPayerTotalDue_CY03.Size = New System.Drawing.Size(54, 13)
Me.lblNonPayerTotalDue_CY03.TabIndex = 12
Me.lblNonPayerTotalDue_CY03.Text = "Total Due"
'
'GBNonPayer_CY04
'
Me.GBNonPayer_CY04.Controls.Add(Me.lblNonPayerBalance_CY04)
Me.GBNonPayer_CY04.Controls.Add(Me.lblNonPayerTotalPaid_CY04)
Me.GBNonPayer_CY04.Controls.Add(Me.lblNonPayerTotalDue_CY04)
Me.GBNonPayer_CY04.Dock = System.Windows.Forms.DockStyle.Top
Me.GBNonPayer_CY04.Location = New System.Drawing.Point(3, 469)
Me.GBNonPayer_CY04.Name = "GBNonPayer_CY04"
Me.GBNonPayer_CY04.Size = New System.Drawing.Size(772, 58)
Me.GBNonPayer_CY04.TabIndex = 6
Me.GBNonPayer_CY04.TabStop = false
Me.GBNonPayer_CY04.Text = "CY 2004"
Me.GBNonPayer_CY04.Visible = false
'
'lblNonPayerBalance_CY04
'
Me.lblNonPayerBalance_CY04.AutoSize = true
Me.lblNonPayerBalance_CY04.Location = New System.Drawing.Point(167, 16)
Me.lblNonPayerBalance_CY04.Name = "lblNonPayerBalance_CY04"
Me.lblNonPayerBalance_CY04.Size = New System.Drawing.Size(46, 13)
Me.lblNonPayerBalance_CY04.TabIndex = 14
Me.lblNonPayerBalance_CY04.Text = "Balance"
'
'lblNonPayerTotalPaid_CY04
'
Me.lblNonPayerTotalPaid_CY04.AutoSize = true
Me.lblNonPayerTotalPaid_CY04.Location = New System.Drawing.Point(7, 35)
Me.lblNonPayerTotalPaid_CY04.Name = "lblNonPayerTotalPaid_CY04"
Me.lblNonPayerTotalPaid_CY04.Size = New System.Drawing.Size(55, 13)
Me.lblNonPayerTotalPaid_CY04.TabIndex = 13
Me.lblNonPayerTotalPaid_CY04.Text = "Total Paid"
'
'lblNonPayerTotalDue_CY04
'
Me.lblNonPayerTotalDue_CY04.AutoSize = true
Me.lblNonPayerTotalDue_CY04.Location = New System.Drawing.Point(7, 16)
Me.lblNonPayerTotalDue_CY04.Name = "lblNonPayerTotalDue_CY04"
Me.lblNonPayerTotalDue_CY04.Size = New System.Drawing.Size(54, 13)
Me.lblNonPayerTotalDue_CY04.TabIndex = 12
Me.lblNonPayerTotalDue_CY04.Text = "Total Due"
'
'GBNonPayer_CY05
'
Me.GBNonPayer_CY05.Controls.Add(Me.lblNonPayerBalance_CY05)
Me.GBNonPayer_CY05.Controls.Add(Me.lblNonPayerTotalPaid_CY05)
Me.GBNonPayer_CY05.Controls.Add(Me.lblNonPayerTotalDue_CY05)
Me.GBNonPayer_CY05.Dock = System.Windows.Forms.DockStyle.Top
Me.GBNonPayer_CY05.Location = New System.Drawing.Point(3, 411)
Me.GBNonPayer_CY05.Name = "GBNonPayer_CY05"
Me.GBNonPayer_CY05.Size = New System.Drawing.Size(772, 58)
Me.GBNonPayer_CY05.TabIndex = 5
Me.GBNonPayer_CY05.TabStop = false
Me.GBNonPayer_CY05.Text = "CY 2005"
Me.GBNonPayer_CY05.Visible = false
'
'lblNonPayerBalance_CY05
'
Me.lblNonPayerBalance_CY05.AutoSize = true
Me.lblNonPayerBalance_CY05.Location = New System.Drawing.Point(167, 16)
Me.lblNonPayerBalance_CY05.Name = "lblNonPayerBalance_CY05"
Me.lblNonPayerBalance_CY05.Size = New System.Drawing.Size(46, 13)
Me.lblNonPayerBalance_CY05.TabIndex = 14
Me.lblNonPayerBalance_CY05.Text = "Balance"
'
'lblNonPayerTotalPaid_CY05
'
Me.lblNonPayerTotalPaid_CY05.AutoSize = true
Me.lblNonPayerTotalPaid_CY05.Location = New System.Drawing.Point(7, 35)
Me.lblNonPayerTotalPaid_CY05.Name = "lblNonPayerTotalPaid_CY05"
Me.lblNonPayerTotalPaid_CY05.Size = New System.Drawing.Size(55, 13)
Me.lblNonPayerTotalPaid_CY05.TabIndex = 13
Me.lblNonPayerTotalPaid_CY05.Text = "Total Paid"
'
'lblNonPayerTotalDue_CY05
'
Me.lblNonPayerTotalDue_CY05.AutoSize = true
Me.lblNonPayerTotalDue_CY05.Location = New System.Drawing.Point(7, 16)
Me.lblNonPayerTotalDue_CY05.Name = "lblNonPayerTotalDue_CY05"
Me.lblNonPayerTotalDue_CY05.Size = New System.Drawing.Size(54, 13)
Me.lblNonPayerTotalDue_CY05.TabIndex = 12
Me.lblNonPayerTotalDue_CY05.Text = "Total Due"
'
'GBNonPayer_CY06
'
Me.GBNonPayer_CY06.Controls.Add(Me.lblNonPayerBalance_CY06)
Me.GBNonPayer_CY06.Controls.Add(Me.lblNonPayerTotalPaid_CY06)
Me.GBNonPayer_CY06.Controls.Add(Me.lblNonPayerTotalDue_CY06)
Me.GBNonPayer_CY06.Dock = System.Windows.Forms.DockStyle.Top
Me.GBNonPayer_CY06.Location = New System.Drawing.Point(3, 353)
Me.GBNonPayer_CY06.Name = "GBNonPayer_CY06"
Me.GBNonPayer_CY06.Size = New System.Drawing.Size(772, 58)
Me.GBNonPayer_CY06.TabIndex = 4
Me.GBNonPayer_CY06.TabStop = false
Me.GBNonPayer_CY06.Text = "CY 2006"
Me.GBNonPayer_CY06.Visible = false
'
'lblNonPayerBalance_CY06
'
Me.lblNonPayerBalance_CY06.AutoSize = true
Me.lblNonPayerBalance_CY06.Location = New System.Drawing.Point(167, 16)
Me.lblNonPayerBalance_CY06.Name = "lblNonPayerBalance_CY06"
Me.lblNonPayerBalance_CY06.Size = New System.Drawing.Size(46, 13)
Me.lblNonPayerBalance_CY06.TabIndex = 14
Me.lblNonPayerBalance_CY06.Text = "Balance"
'
'lblNonPayerTotalPaid_CY06
'
Me.lblNonPayerTotalPaid_CY06.AutoSize = true
Me.lblNonPayerTotalPaid_CY06.Location = New System.Drawing.Point(7, 35)
Me.lblNonPayerTotalPaid_CY06.Name = "lblNonPayerTotalPaid_CY06"
Me.lblNonPayerTotalPaid_CY06.Size = New System.Drawing.Size(55, 13)
Me.lblNonPayerTotalPaid_CY06.TabIndex = 13
Me.lblNonPayerTotalPaid_CY06.Text = "Total Paid"
'
'lblNonPayerTotalDue_CY06
'
Me.lblNonPayerTotalDue_CY06.AutoSize = true
Me.lblNonPayerTotalDue_CY06.Location = New System.Drawing.Point(7, 16)
Me.lblNonPayerTotalDue_CY06.Name = "lblNonPayerTotalDue_CY06"
Me.lblNonPayerTotalDue_CY06.Size = New System.Drawing.Size(54, 13)
Me.lblNonPayerTotalDue_CY06.TabIndex = 12
Me.lblNonPayerTotalDue_CY06.Text = "Total Due"
'
'GBNonPayer_CY07
'
Me.GBNonPayer_CY07.Controls.Add(Me.lblNonPayerBalance_CY07)
Me.GBNonPayer_CY07.Controls.Add(Me.lblNonPayerTotalPaid_CY07)
Me.GBNonPayer_CY07.Controls.Add(Me.lblNonPayerTotalDue_CY07)
Me.GBNonPayer_CY07.Dock = System.Windows.Forms.DockStyle.Top
Me.GBNonPayer_CY07.Location = New System.Drawing.Point(3, 295)
Me.GBNonPayer_CY07.Name = "GBNonPayer_CY07"
Me.GBNonPayer_CY07.Size = New System.Drawing.Size(772, 58)
Me.GBNonPayer_CY07.TabIndex = 3
Me.GBNonPayer_CY07.TabStop = false
Me.GBNonPayer_CY07.Text = "CY 2007"
Me.GBNonPayer_CY07.Visible = false
'
'lblNonPayerBalance_CY07
'
Me.lblNonPayerBalance_CY07.AutoSize = true
Me.lblNonPayerBalance_CY07.Location = New System.Drawing.Point(167, 16)
Me.lblNonPayerBalance_CY07.Name = "lblNonPayerBalance_CY07"
Me.lblNonPayerBalance_CY07.Size = New System.Drawing.Size(46, 13)
Me.lblNonPayerBalance_CY07.TabIndex = 14
Me.lblNonPayerBalance_CY07.Text = "Balance"
'
'lblNonPayerTotalPaid_CY07
'
Me.lblNonPayerTotalPaid_CY07.AutoSize = true
Me.lblNonPayerTotalPaid_CY07.Location = New System.Drawing.Point(7, 35)
Me.lblNonPayerTotalPaid_CY07.Name = "lblNonPayerTotalPaid_CY07"
Me.lblNonPayerTotalPaid_CY07.Size = New System.Drawing.Size(55, 13)
Me.lblNonPayerTotalPaid_CY07.TabIndex = 13
Me.lblNonPayerTotalPaid_CY07.Text = "Total Paid"
'
'lblNonPayerTotalDue_CY07
'
Me.lblNonPayerTotalDue_CY07.AutoSize = true
Me.lblNonPayerTotalDue_CY07.Location = New System.Drawing.Point(7, 16)
Me.lblNonPayerTotalDue_CY07.Name = "lblNonPayerTotalDue_CY07"
Me.lblNonPayerTotalDue_CY07.Size = New System.Drawing.Size(54, 13)
Me.lblNonPayerTotalDue_CY07.TabIndex = 12
Me.lblNonPayerTotalDue_CY07.Text = "Total Due"
'
'GBNonPayer_CY08
'
Me.GBNonPayer_CY08.Controls.Add(Me.lblNonPayerBalance_CY08)
Me.GBNonPayer_CY08.Controls.Add(Me.lblNonPayerTotalPaid_CY08)
Me.GBNonPayer_CY08.Controls.Add(Me.lblNonPayerTotalDue_CY08)
Me.GBNonPayer_CY08.Dock = System.Windows.Forms.DockStyle.Top
Me.GBNonPayer_CY08.Location = New System.Drawing.Point(3, 244)
Me.GBNonPayer_CY08.Name = "GBNonPayer_CY08"
Me.GBNonPayer_CY08.Size = New System.Drawing.Size(772, 51)
Me.GBNonPayer_CY08.TabIndex = 2
Me.GBNonPayer_CY08.TabStop = false
Me.GBNonPayer_CY08.Text = "CY 2008"
Me.GBNonPayer_CY08.Visible = false
'
'lblNonPayerBalance_CY08
'
Me.lblNonPayerBalance_CY08.AutoSize = true
Me.lblNonPayerBalance_CY08.Location = New System.Drawing.Point(167, 16)
Me.lblNonPayerBalance_CY08.Name = "lblNonPayerBalance_CY08"
Me.lblNonPayerBalance_CY08.Size = New System.Drawing.Size(46, 13)
Me.lblNonPayerBalance_CY08.TabIndex = 10
Me.lblNonPayerBalance_CY08.Text = "Balance"
'
'lblNonPayerTotalPaid_CY08
'
Me.lblNonPayerTotalPaid_CY08.AutoSize = true
Me.lblNonPayerTotalPaid_CY08.Location = New System.Drawing.Point(7, 35)
Me.lblNonPayerTotalPaid_CY08.Name = "lblNonPayerTotalPaid_CY08"
Me.lblNonPayerTotalPaid_CY08.Size = New System.Drawing.Size(55, 13)
Me.lblNonPayerTotalPaid_CY08.TabIndex = 9
Me.lblNonPayerTotalPaid_CY08.Text = "Total Paid"
'
'lblNonPayerTotalDue_CY08
'
Me.lblNonPayerTotalDue_CY08.AutoSize = true
Me.lblNonPayerTotalDue_CY08.Location = New System.Drawing.Point(7, 16)
Me.lblNonPayerTotalDue_CY08.Name = "lblNonPayerTotalDue_CY08"
Me.lblNonPayerTotalDue_CY08.Size = New System.Drawing.Size(54, 13)
Me.lblNonPayerTotalDue_CY08.TabIndex = 8
Me.lblNonPayerTotalDue_CY08.Text = "Total Due"
'
'Panel102
'
Me.Panel102.Controls.Add(Me.Panel104)
Me.Panel102.Controls.Add(Me.Panel103)
Me.Panel102.Controls.Add(Me.Panel108)
Me.Panel102.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel102.Location = New System.Drawing.Point(3, 16)
Me.Panel102.Name = "Panel102"
Me.Panel102.Size = New System.Drawing.Size(772, 228)
Me.Panel102.TabIndex = 0
'
'Panel104
'
Me.Panel104.Controls.Add(Me.btnCopyData)
Me.Panel104.Controls.Add(Me.btnSaveAndUpdate)
Me.Panel104.Controls.Add(Me.lblNonPayerContactEmail)
Me.Panel104.Controls.Add(Me.lblNonPayerContactPhoneNumber)
Me.Panel104.Controls.Add(Me.lblNonPayerContactAddress)
Me.Panel104.Controls.Add(Me.lblNonPayerContactCompany)
Me.Panel104.Controls.Add(Me.lblNonPayerContactTitle)
Me.Panel104.Controls.Add(Me.lblNonPayerContactName)
Me.Panel104.Controls.Add(Me.Label117)
Me.Panel104.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel104.Location = New System.Drawing.Point(248, 0)
Me.Panel104.Name = "Panel104"
Me.Panel104.Size = New System.Drawing.Size(524, 145)
Me.Panel104.TabIndex = 70
'
'btnCopyData
'
Me.btnCopyData.AutoSize = true
Me.btnCopyData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnCopyData.Location = New System.Drawing.Point(6, 116)
Me.btnCopyData.Name = "btnCopyData"
Me.btnCopyData.Size = New System.Drawing.Size(171, 23)
Me.btnCopyData.TabIndex = 384
Me.btnCopyData.Text = "Copy Contact Data to Comments"
Me.btnCopyData.UseVisualStyleBackColor = true
'
'btnSaveAndUpdate
'
Me.btnSaveAndUpdate.AutoSize = true
Me.btnSaveAndUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveAndUpdate.Location = New System.Drawing.Point(203, 116)
Me.btnSaveAndUpdate.Name = "btnSaveAndUpdate"
Me.btnSaveAndUpdate.Size = New System.Drawing.Size(101, 23)
Me.btnSaveAndUpdate.TabIndex = 383
Me.btnSaveAndUpdate.Text = "Edit Contact Data"
Me.btnSaveAndUpdate.UseVisualStyleBackColor = true
'
'lblNonPayerContactEmail
'
Me.lblNonPayerContactEmail.AutoSize = true
Me.lblNonPayerContactEmail.Location = New System.Drawing.Point(1, 100)
Me.lblNonPayerContactEmail.Name = "lblNonPayerContactEmail"
Me.lblNonPayerContactEmail.Size = New System.Drawing.Size(113, 13)
Me.lblNonPayerContactEmail.TabIndex = 11
Me.lblNonPayerContactEmail.Text = "Contact Email Address"
'
'lblNonPayerContactPhoneNumber
'
Me.lblNonPayerContactPhoneNumber.AutoSize = true
Me.lblNonPayerContactPhoneNumber.Location = New System.Drawing.Point(1, 87)
Me.lblNonPayerContactPhoneNumber.Name = "lblNonPayerContactPhoneNumber"
Me.lblNonPayerContactPhoneNumber.Size = New System.Drawing.Size(118, 13)
Me.lblNonPayerContactPhoneNumber.TabIndex = 10
Me.lblNonPayerContactPhoneNumber.Text = "Contact Phone Number"
'
'lblNonPayerContactAddress
'
Me.lblNonPayerContactAddress.AutoSize = true
Me.lblNonPayerContactAddress.Location = New System.Drawing.Point(35, 59)
Me.lblNonPayerContactAddress.Name = "lblNonPayerContactAddress"
Me.lblNonPayerContactAddress.Size = New System.Drawing.Size(7, 13)
Me.lblNonPayerContactAddress.TabIndex = 9
Me.lblNonPayerContactAddress.Text = ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
'
'lblNonPayerContactCompany
'
Me.lblNonPayerContactCompany.AutoSize = true
Me.lblNonPayerContactCompany.Location = New System.Drawing.Point(1, 32)
Me.lblNonPayerContactCompany.Name = "lblNonPayerContactCompany"
Me.lblNonPayerContactCompany.Size = New System.Drawing.Size(91, 13)
Me.lblNonPayerContactCompany.TabIndex = 8
Me.lblNonPayerContactCompany.Text = "Contact Company"
'
'lblNonPayerContactTitle
'
Me.lblNonPayerContactTitle.AutoSize = true
Me.lblNonPayerContactTitle.Location = New System.Drawing.Point(1, 18)
Me.lblNonPayerContactTitle.Name = "lblNonPayerContactTitle"
Me.lblNonPayerContactTitle.Size = New System.Drawing.Size(67, 13)
Me.lblNonPayerContactTitle.TabIndex = 7
Me.lblNonPayerContactTitle.Text = "Contact Title"
'
'lblNonPayerContactName
'
Me.lblNonPayerContactName.AutoSize = true
Me.lblNonPayerContactName.Location = New System.Drawing.Point(1, 4)
Me.lblNonPayerContactName.Name = "lblNonPayerContactName"
Me.lblNonPayerContactName.Size = New System.Drawing.Size(75, 13)
Me.lblNonPayerContactName.TabIndex = 6
Me.lblNonPayerContactName.Text = "Contact Name"
'
'Label117
'
Me.Label117.AutoSize = true
Me.Label117.Location = New System.Drawing.Point(1, 46)
Me.Label117.Name = "Label117"
Me.Label117.Size = New System.Drawing.Size(85, 13)
Me.Label117.TabIndex = 1
Me.Label117.Text = "Contact Address"
'
'Panel103
'
Me.Panel103.Controls.Add(Me.lblNonPayerStaff)
Me.Panel103.Controls.Add(Me.btnFlagNonPayer)
Me.Panel103.Controls.Add(Me.Panel107)
Me.Panel103.Controls.Add(Me.Label66)
Me.Panel103.Controls.Add(Me.lblNonPayerNSPSStatus)
Me.Panel103.Controls.Add(Me.lblNonPayerTVStatus)
Me.Panel103.Controls.Add(Me.lblNonPayerSourceClass)
Me.Panel103.Controls.Add(Me.lblNonPayerOpStatus)
Me.Panel103.Controls.Add(Me.lblNonPayerFacilityAddress)
Me.Panel103.Controls.Add(Me.Label83)
Me.Panel103.Controls.Add(Me.lblNonPayerFacilityName)
Me.Panel103.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel103.Location = New System.Drawing.Point(0, 0)
Me.Panel103.Name = "Panel103"
Me.Panel103.Size = New System.Drawing.Size(248, 145)
Me.Panel103.TabIndex = 69
'
'lblNonPayerStaff
'
Me.lblNonPayerStaff.AutoSize = true
Me.lblNonPayerStaff.Location = New System.Drawing.Point(3, 4)
Me.lblNonPayerStaff.Name = "lblNonPayerStaff"
Me.lblNonPayerStaff.Size = New System.Drawing.Size(90, 13)
Me.lblNonPayerStaff.TabIndex = 383
Me.lblNonPayerStaff.Text = "Staff Responsible"
'
'btnFlagNonPayer
'
Me.btnFlagNonPayer.AutoSize = true
Me.btnFlagNonPayer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnFlagNonPayer.Location = New System.Drawing.Point(188, 113)
Me.btnFlagNonPayer.Name = "btnFlagNonPayer"
Me.btnFlagNonPayer.Size = New System.Drawing.Size(42, 23)
Me.btnFlagNonPayer.TabIndex = 382
Me.btnFlagNonPayer.Text = "Save"
Me.btnFlagNonPayer.UseVisualStyleBackColor = true
'
'Panel107
'
Me.Panel107.AutoSize = true
Me.Panel107.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel107.Controls.Add(Me.rdbNonPayerActive)
Me.Panel107.Controls.Add(Me.rdbNonPayerInactive)
Me.Panel107.Location = New System.Drawing.Point(88, 115)
Me.Panel107.Name = "Panel107"
Me.Panel107.Size = New System.Drawing.Size(94, 23)
Me.Panel107.TabIndex = 381
'
'rdbNonPayerActive
'
Me.rdbNonPayerActive.AutoSize = true
Me.rdbNonPayerActive.Location = New System.Drawing.Point(50, 3)
Me.rdbNonPayerActive.Name = "rdbNonPayerActive"
Me.rdbNonPayerActive.Size = New System.Drawing.Size(41, 17)
Me.rdbNonPayerActive.TabIndex = 1
Me.rdbNonPayerActive.TabStop = true
Me.rdbNonPayerActive.Text = "NO"
Me.rdbNonPayerActive.UseVisualStyleBackColor = true
'
'rdbNonPayerInactive
'
Me.rdbNonPayerInactive.AutoSize = true
Me.rdbNonPayerInactive.Location = New System.Drawing.Point(3, 3)
Me.rdbNonPayerInactive.Name = "rdbNonPayerInactive"
Me.rdbNonPayerInactive.Size = New System.Drawing.Size(46, 17)
Me.rdbNonPayerInactive.TabIndex = 0
Me.rdbNonPayerInactive.TabStop = true
Me.rdbNonPayerInactive.Text = "YES"
Me.rdbNonPayerInactive.UseVisualStyleBackColor = true
'
'Label66
'
Me.Label66.AutoSize = true
Me.Label66.Location = New System.Drawing.Point(3, 120)
Me.Label66.Name = "Label66"
Me.Label66.Size = New System.Drawing.Size(82, 13)
Me.Label66.TabIndex = 380
Me.Label66.Text = "Flag as Inactive"
'
'lblNonPayerNSPSStatus
'
Me.lblNonPayerNSPSStatus.AutoSize = true
Me.lblNonPayerNSPSStatus.Location = New System.Drawing.Point(197, 92)
Me.lblNonPayerNSPSStatus.Name = "lblNonPayerNSPSStatus"
Me.lblNonPayerNSPSStatus.Size = New System.Drawing.Size(36, 13)
Me.lblNonPayerNSPSStatus.TabIndex = 9
Me.lblNonPayerNSPSStatus.Text = "NSPS"
'
'lblNonPayerTVStatus
'
Me.lblNonPayerTVStatus.AutoSize = true
Me.lblNonPayerTVStatus.Location = New System.Drawing.Point(197, 77)
Me.lblNonPayerTVStatus.Name = "lblNonPayerTVStatus"
Me.lblNonPayerTVStatus.Size = New System.Drawing.Size(37, 13)
Me.lblNonPayerTVStatus.TabIndex = 8
Me.lblNonPayerTVStatus.Text = "Title V"
'
'lblNonPayerSourceClass
'
Me.lblNonPayerSourceClass.AutoSize = true
Me.lblNonPayerSourceClass.Location = New System.Drawing.Point(7, 92)
Me.lblNonPayerSourceClass.Name = "lblNonPayerSourceClass"
Me.lblNonPayerSourceClass.Size = New System.Drawing.Size(69, 13)
Me.lblNonPayerSourceClass.TabIndex = 7
Me.lblNonPayerSourceClass.Text = "Source Class"
'
'lblNonPayerOpStatus
'
Me.lblNonPayerOpStatus.AutoSize = true
Me.lblNonPayerOpStatus.Location = New System.Drawing.Point(7, 77)
Me.lblNonPayerOpStatus.Name = "lblNonPayerOpStatus"
Me.lblNonPayerOpStatus.Size = New System.Drawing.Size(86, 13)
Me.lblNonPayerOpStatus.TabIndex = 6
Me.lblNonPayerOpStatus.Text = "Operating Status"
'
'lblNonPayerFacilityAddress
'
Me.lblNonPayerFacilityAddress.AutoSize = true
Me.lblNonPayerFacilityAddress.Location = New System.Drawing.Point(32, 48)
Me.lblNonPayerFacilityAddress.Name = "lblNonPayerFacilityAddress"
Me.lblNonPayerFacilityAddress.Size = New System.Drawing.Size(0, 13)
Me.lblNonPayerFacilityAddress.TabIndex = 5
'
'Label83
'
Me.Label83.AutoSize = true
Me.Label83.Location = New System.Drawing.Point(3, 34)
Me.Label83.Name = "Label83"
Me.Label83.Size = New System.Drawing.Size(78, 13)
Me.Label83.TabIndex = 4
Me.Label83.Text = "Facilty Address"
'
'lblNonPayerFacilityName
'
Me.lblNonPayerFacilityName.AutoSize = true
Me.lblNonPayerFacilityName.Location = New System.Drawing.Point(3, 20)
Me.lblNonPayerFacilityName.Name = "lblNonPayerFacilityName"
Me.lblNonPayerFacilityName.Size = New System.Drawing.Size(68, 13)
Me.lblNonPayerFacilityName.TabIndex = 1
Me.lblNonPayerFacilityName.Text = "Facilty Name"
'
'Panel108
'
Me.Panel108.Controls.Add(Me.btnSaveNonPayerComments)
Me.Panel108.Controls.Add(Me.Label81)
Me.Panel108.Controls.Add(Me.txtNonPayersComments)
Me.Panel108.Dock = System.Windows.Forms.DockStyle.Bottom
Me.Panel108.Location = New System.Drawing.Point(0, 145)
Me.Panel108.Name = "Panel108"
Me.Panel108.Size = New System.Drawing.Size(772, 83)
Me.Panel108.TabIndex = 71
'
'btnSaveNonPayerComments
'
Me.btnSaveNonPayerComments.AutoSize = true
Me.btnSaveNonPayerComments.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSaveNonPayerComments.Location = New System.Drawing.Point(553, 6)
Me.btnSaveNonPayerComments.Name = "btnSaveNonPayerComments"
Me.btnSaveNonPayerComments.Size = New System.Drawing.Size(94, 23)
Me.btnSaveNonPayerComments.TabIndex = 383
Me.btnSaveNonPayerComments.Text = "Save Comments"
Me.btnSaveNonPayerComments.UseVisualStyleBackColor = true
'
'Label81
'
Me.Label81.AutoSize = true
Me.Label81.Location = New System.Drawing.Point(7, 3)
Me.Label81.Name = "Label81"
Me.Label81.Size = New System.Drawing.Size(56, 13)
Me.Label81.TabIndex = 7
Me.Label81.Text = "Comments"
'
'txtNonPayersComments
'
Me.txtNonPayersComments.Location = New System.Drawing.Point(69, 3)
Me.txtNonPayersComments.Multiline = true
Me.txtNonPayersComments.Name = "txtNonPayersComments"
Me.txtNonPayersComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtNonPayersComments.Size = New System.Drawing.Size(478, 65)
Me.txtNonPayersComments.TabIndex = 0
'
'TPAuditReport
'
Me.TPAuditReport.Controls.Add(Me.Panel101)
Me.TPAuditReport.Controls.Add(Me.Panel100)
Me.TPAuditReport.Location = New System.Drawing.Point(4, 22)
Me.TPAuditReport.Name = "TPAuditReport"
Me.TPAuditReport.Size = New System.Drawing.Size(784, 635)
Me.TPAuditReport.TabIndex = 2
Me.TPAuditReport.Text = "Reports"
Me.TPAuditReport.UseVisualStyleBackColor = true
'
'Panel101
'
Me.Panel101.Controls.Add(Me.dgvFeeAuditReport)
Me.Panel101.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel101.Location = New System.Drawing.Point(0, 165)
Me.Panel101.Name = "Panel101"
Me.Panel101.Size = New System.Drawing.Size(784, 470)
Me.Panel101.TabIndex = 1
'
'dgvFeeAuditReport
'
Me.dgvFeeAuditReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
Me.dgvFeeAuditReport.Dock = System.Windows.Forms.DockStyle.Fill
Me.dgvFeeAuditReport.Location = New System.Drawing.Point(0, 0)
Me.dgvFeeAuditReport.Name = "dgvFeeAuditReport"
Me.dgvFeeAuditReport.ReadOnly = true
Me.dgvFeeAuditReport.Size = New System.Drawing.Size(784, 470)
Me.dgvFeeAuditReport.TabIndex = 0
'
'Panel100
'
Me.Panel100.Controls.Add(Me.GroupBox3)
Me.Panel100.Controls.Add(Me.Panel111)
Me.Panel100.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel100.Location = New System.Drawing.Point(0, 0)
Me.Panel100.Name = "Panel100"
Me.Panel100.Size = New System.Drawing.Size(784, 165)
Me.Panel100.TabIndex = 0
'
'GroupBox3
'
Me.GroupBox3.Controls.Add(Me.txtOpenAudits)
Me.GroupBox3.Controls.Add(Me.Label116)
Me.GroupBox3.Controls.Add(Me.txtClosedOutAudits)
Me.GroupBox3.Controls.Add(Me.Label102)
Me.GroupBox3.Controls.Add(Me.txtPossibleNOV)
Me.GroupBox3.Controls.Add(Me.Label101)
Me.GroupBox3.Controls.Add(Me.txtNonPayerSent)
Me.GroupBox3.Controls.Add(Me.txtNonRespondersSent)
Me.GroupBox3.Controls.Add(Me.Label100)
Me.GroupBox3.Controls.Add(Me.Label99)
Me.GroupBox3.Controls.Add(Me.btnRunStats)
Me.GroupBox3.Controls.Add(Me.txtTotalLetterSent)
Me.GroupBox3.Controls.Add(Me.Label82)
Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
Me.GroupBox3.Location = New System.Drawing.Point(0, 71)
Me.GroupBox3.Name = "GroupBox3"
Me.GroupBox3.Size = New System.Drawing.Size(784, 94)
Me.GroupBox3.TabIndex = 10
Me.GroupBox3.TabStop = false
Me.GroupBox3.Text = "Stats"
'
'txtOpenAudits
'
Me.txtOpenAudits.Location = New System.Drawing.Point(328, 16)
Me.txtOpenAudits.Name = "txtOpenAudits"
Me.txtOpenAudits.ReadOnly = true
Me.txtOpenAudits.Size = New System.Drawing.Size(41, 20)
Me.txtOpenAudits.TabIndex = 12
'
'Label116
'
Me.Label116.AutoSize = true
Me.Label116.Location = New System.Drawing.Point(219, 20)
Me.Label116.Name = "Label116"
Me.Label116.Size = New System.Drawing.Size(79, 13)
Me.Label116.TabIndex = 11
Me.Label116.Text = "All Open Audits"
'
'txtClosedOutAudits
'
Me.txtClosedOutAudits.Location = New System.Drawing.Point(328, 38)
Me.txtClosedOutAudits.Name = "txtClosedOutAudits"
Me.txtClosedOutAudits.ReadOnly = true
Me.txtClosedOutAudits.Size = New System.Drawing.Size(41, 20)
Me.txtClosedOutAudits.TabIndex = 10
'
'Label102
'
Me.Label102.AutoSize = true
Me.Label102.Location = New System.Drawing.Point(219, 42)
Me.Label102.Name = "Label102"
Me.Label102.Size = New System.Drawing.Size(105, 13)
Me.Label102.TabIndex = 9
Me.Label102.Text = "All Closed Out Audits"
'
'txtPossibleNOV
'
Me.txtPossibleNOV.Location = New System.Drawing.Point(516, 16)
Me.txtPossibleNOV.Name = "txtPossibleNOV"
Me.txtPossibleNOV.ReadOnly = true
Me.txtPossibleNOV.Size = New System.Drawing.Size(41, 20)
Me.txtPossibleNOV.TabIndex = 8
Me.txtPossibleNOV.Visible = false
'
'Label101
'
Me.Label101.AutoSize = true
Me.Label101.Location = New System.Drawing.Point(383, 19)
Me.Label101.Name = "Label101"
Me.Label101.Size = New System.Drawing.Size(131, 13)
Me.Label101.TabIndex = 7
Me.Label101.Text = "Unable to Contact (NOV?)"
Me.Label101.Visible = false
'
'txtNonPayerSent
'
Me.txtNonPayerSent.Location = New System.Drawing.Point(157, 60)
Me.txtNonPayerSent.Name = "txtNonPayerSent"
Me.txtNonPayerSent.ReadOnly = true
Me.txtNonPayerSent.Size = New System.Drawing.Size(41, 20)
Me.txtNonPayerSent.TabIndex = 6
'
'txtNonRespondersSent
'
Me.txtNonRespondersSent.Location = New System.Drawing.Point(157, 38)
Me.txtNonRespondersSent.Name = "txtNonRespondersSent"
Me.txtNonRespondersSent.ReadOnly = true
Me.txtNonRespondersSent.Size = New System.Drawing.Size(41, 20)
Me.txtNonRespondersSent.TabIndex = 5
'
'Label100
'
Me.Label100.AutoSize = true
Me.Label100.Location = New System.Drawing.Point(89, 63)
Me.Label100.Name = "Label100"
Me.Label100.Size = New System.Drawing.Size(62, 13)
Me.Label100.TabIndex = 4
Me.Label100.Text = "Non Payers"
'
'Label99
'
Me.Label99.AutoSize = true
Me.Label99.Location = New System.Drawing.Point(64, 42)
Me.Label99.Name = "Label99"
Me.Label99.Size = New System.Drawing.Size(87, 13)
Me.Label99.TabIndex = 3
Me.Label99.Text = "Non Responders"
'
'btnRunStats
'
Me.btnRunStats.AutoSize = true
Me.btnRunStats.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnRunStats.Location = New System.Drawing.Point(694, 9)
Me.btnRunStats.Name = "btnRunStats"
Me.btnRunStats.Size = New System.Drawing.Size(64, 23)
Me.btnRunStats.TabIndex = 2
Me.btnRunStats.Text = "Run Stats"
Me.btnRunStats.UseVisualStyleBackColor = true
'
'txtTotalLetterSent
'
Me.txtTotalLetterSent.Location = New System.Drawing.Point(157, 16)
Me.txtTotalLetterSent.Name = "txtTotalLetterSent"
Me.txtTotalLetterSent.ReadOnly = true
Me.txtTotalLetterSent.Size = New System.Drawing.Size(41, 20)
Me.txtTotalLetterSent.TabIndex = 1
'
'Label82
'
Me.Label82.AutoSize = true
Me.Label82.Location = New System.Drawing.Point(8, 20)
Me.Label82.Name = "Label82"
Me.Label82.Size = New System.Drawing.Size(143, 13)
Me.Label82.TabIndex = 0
Me.Label82.Text = "Total Number of Letters Sent"
'
'Panel111
'
Me.Panel111.Controls.Add(Me.Panel106)
Me.Panel111.Controls.Add(Me.btnNonRespondersData)
Me.Panel111.Controls.Add(Me.Panel110)
Me.Panel111.Controls.Add(Me.btnViewFullAuditData)
Me.Panel111.Controls.Add(Me.Panel109)
Me.Panel111.Controls.Add(Me.btnExportToExcel)
Me.Panel111.Controls.Add(Me.Label80)
Me.Panel111.Controls.Add(Me.btnViewAllNonPayerData)
Me.Panel111.Controls.Add(Me.txtCount)
Me.Panel111.Controls.Add(Me.btnGetEmailAddresses)
Me.Panel111.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel111.Location = New System.Drawing.Point(0, 0)
Me.Panel111.Name = "Panel111"
Me.Panel111.Size = New System.Drawing.Size(784, 71)
Me.Panel111.TabIndex = 11
'
'Panel106
'
Me.Panel106.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel106.Controls.Add(Me.rdbActiveAll)
Me.Panel106.Controls.Add(Me.rdbInactive)
Me.Panel106.Controls.Add(Me.rdbActive)
Me.Panel106.Location = New System.Drawing.Point(3, 3)
Me.Panel106.Name = "Panel106"
Me.Panel106.Size = New System.Drawing.Size(73, 61)
Me.Panel106.TabIndex = 3
'
'rdbActiveAll
'
Me.rdbActiveAll.AutoSize = true
Me.rdbActiveAll.Location = New System.Drawing.Point(3, 38)
Me.rdbActiveAll.Name = "rdbActiveAll"
Me.rdbActiveAll.Size = New System.Drawing.Size(36, 17)
Me.rdbActiveAll.TabIndex = 2
Me.rdbActiveAll.TabStop = true
Me.rdbActiveAll.Text = "All"
Me.rdbActiveAll.UseVisualStyleBackColor = true
'
'rdbInactive
'
Me.rdbInactive.AutoSize = true
Me.rdbInactive.Location = New System.Drawing.Point(3, 20)
Me.rdbInactive.Name = "rdbInactive"
Me.rdbInactive.Size = New System.Drawing.Size(67, 17)
Me.rdbInactive.TabIndex = 1
Me.rdbInactive.TabStop = true
Me.rdbInactive.Text = "In Active"
Me.rdbInactive.UseVisualStyleBackColor = true
'
'rdbActive
'
Me.rdbActive.AutoSize = true
Me.rdbActive.Location = New System.Drawing.Point(3, 3)
Me.rdbActive.Name = "rdbActive"
Me.rdbActive.Size = New System.Drawing.Size(55, 17)
Me.rdbActive.TabIndex = 0
Me.rdbActive.TabStop = true
Me.rdbActive.Text = "Active"
Me.rdbActive.UseVisualStyleBackColor = true
'
'btnNonRespondersData
'
Me.btnNonRespondersData.AutoSize = true
Me.btnNonRespondersData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnNonRespondersData.Location = New System.Drawing.Point(79, 6)
Me.btnNonRespondersData.Name = "btnNonRespondersData"
Me.btnNonRespondersData.Size = New System.Drawing.Size(166, 23)
Me.btnNonRespondersData.TabIndex = 0
Me.btnNonRespondersData.Text = "View All Non_Responders Data"
Me.btnNonRespondersData.UseVisualStyleBackColor = true
'
'Panel110
'
Me.Panel110.AutoSize = true
Me.Panel110.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel110.Controls.Add(Me.rdbAllClosedOut)
Me.Panel110.Controls.Add(Me.rdbAuditNotClosedout)
Me.Panel110.Controls.Add(Me.rdbAuditClosedOut)
Me.Panel110.Location = New System.Drawing.Point(391, 4)
Me.Panel110.Name = "Panel110"
Me.Panel110.Size = New System.Drawing.Size(110, 58)
Me.Panel110.TabIndex = 9
'
'rdbAllClosedOut
'
Me.rdbAllClosedOut.AutoSize = true
Me.rdbAllClosedOut.Location = New System.Drawing.Point(3, 38)
Me.rdbAllClosedOut.Name = "rdbAllClosedOut"
Me.rdbAllClosedOut.Size = New System.Drawing.Size(36, 17)
Me.rdbAllClosedOut.TabIndex = 2
Me.rdbAllClosedOut.TabStop = true
Me.rdbAllClosedOut.Text = "All"
Me.rdbAllClosedOut.UseVisualStyleBackColor = true
'
'rdbAuditNotClosedout
'
Me.rdbAuditNotClosedout.AutoSize = true
Me.rdbAuditNotClosedout.Location = New System.Drawing.Point(3, 20)
Me.rdbAuditNotClosedout.Name = "rdbAuditNotClosedout"
Me.rdbAuditNotClosedout.Size = New System.Drawing.Size(78, 17)
Me.rdbAuditNotClosedout.TabIndex = 1
Me.rdbAuditNotClosedout.TabStop = true
Me.rdbAuditNotClosedout.Text = "Audit Open"
Me.rdbAuditNotClosedout.UseVisualStyleBackColor = true
'
'rdbAuditClosedOut
'
Me.rdbAuditClosedOut.AutoSize = true
Me.rdbAuditClosedOut.Location = New System.Drawing.Point(3, 1)
Me.rdbAuditClosedOut.Name = "rdbAuditClosedOut"
Me.rdbAuditClosedOut.Size = New System.Drawing.Size(104, 17)
Me.rdbAuditClosedOut.TabIndex = 0
Me.rdbAuditClosedOut.TabStop = true
Me.rdbAuditClosedOut.Text = "Audit Closed Out"
Me.rdbAuditClosedOut.UseVisualStyleBackColor = true
'
'btnViewFullAuditData
'
Me.btnViewFullAuditData.AutoSize = true
Me.btnViewFullAuditData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnViewFullAuditData.Location = New System.Drawing.Point(508, 5)
Me.btnViewFullAuditData.Name = "btnViewFullAuditData"
Me.btnViewFullAuditData.Size = New System.Drawing.Size(107, 23)
Me.btnViewFullAuditData.TabIndex = 1
Me.btnViewFullAuditData.Text = "View All Audit Data"
Me.btnViewFullAuditData.UseVisualStyleBackColor = true
'
'Panel109
'
Me.Panel109.AutoSize = true
Me.Panel109.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.Panel109.Controls.Add(Me.rdbAll)
Me.Panel109.Controls.Add(Me.rdbNonPayers)
Me.Panel109.Controls.Add(Me.rdbNonResponders)
Me.Panel109.Location = New System.Drawing.Point(274, 3)
Me.Panel109.Name = "Panel109"
Me.Panel109.Size = New System.Drawing.Size(111, 58)
Me.Panel109.TabIndex = 8
'
'rdbAll
'
Me.rdbAll.AutoSize = true
Me.rdbAll.Location = New System.Drawing.Point(3, 38)
Me.rdbAll.Name = "rdbAll"
Me.rdbAll.Size = New System.Drawing.Size(36, 17)
Me.rdbAll.TabIndex = 2
Me.rdbAll.TabStop = true
Me.rdbAll.Text = "All"
Me.rdbAll.UseVisualStyleBackColor = true
'
'rdbNonPayers
'
Me.rdbNonPayers.AutoSize = true
Me.rdbNonPayers.Location = New System.Drawing.Point(3, 20)
Me.rdbNonPayers.Name = "rdbNonPayers"
Me.rdbNonPayers.Size = New System.Drawing.Size(80, 17)
Me.rdbNonPayers.TabIndex = 1
Me.rdbNonPayers.TabStop = true
Me.rdbNonPayers.Text = "Non-Payers"
Me.rdbNonPayers.UseVisualStyleBackColor = true
'
'rdbNonResponders
'
Me.rdbNonResponders.AutoSize = true
Me.rdbNonResponders.Location = New System.Drawing.Point(3, 1)
Me.rdbNonResponders.Name = "rdbNonResponders"
Me.rdbNonResponders.Size = New System.Drawing.Size(105, 17)
Me.rdbNonResponders.TabIndex = 0
Me.rdbNonResponders.TabStop = true
Me.rdbNonResponders.Text = "Non-Responders"
Me.rdbNonResponders.UseVisualStyleBackColor = true
'
'btnExportToExcel
'
Me.btnExportToExcel.AutoSize = true
Me.btnExportToExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnExportToExcel.Location = New System.Drawing.Point(670, 35)
Me.btnExportToExcel.Name = "btnExportToExcel"
Me.btnExportToExcel.Size = New System.Drawing.Size(88, 23)
Me.btnExportToExcel.TabIndex = 2
Me.btnExportToExcel.Text = "Export to Excel"
Me.btnExportToExcel.UseVisualStyleBackColor = true
'
'Label80
'
Me.Label80.AutoSize = true
Me.Label80.Location = New System.Drawing.Point(550, 45)
Me.Label80.Name = "Label80"
Me.Label80.Size = New System.Drawing.Size(35, 13)
Me.Label80.TabIndex = 7
Me.Label80.Text = "Count"
'
'btnViewAllNonPayerData
'
Me.btnViewAllNonPayerData.AutoSize = true
Me.btnViewAllNonPayerData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnViewAllNonPayerData.Location = New System.Drawing.Point(82, 41)
Me.btnViewAllNonPayerData.Name = "btnViewAllNonPayerData"
Me.btnViewAllNonPayerData.Size = New System.Drawing.Size(141, 23)
Me.btnViewAllNonPayerData.TabIndex = 4
Me.btnViewAllNonPayerData.Text = "View All Non_Payers Data"
Me.btnViewAllNonPayerData.UseVisualStyleBackColor = true
'
'txtCount
'
Me.txtCount.Location = New System.Drawing.Point(591, 42)
Me.txtCount.Name = "txtCount"
Me.txtCount.ReadOnly = true
Me.txtCount.Size = New System.Drawing.Size(47, 20)
Me.txtCount.TabIndex = 6
'
'btnGetEmailAddresses
'
Me.btnGetEmailAddresses.AutoSize = true
Me.btnGetEmailAddresses.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnGetEmailAddresses.Location = New System.Drawing.Point(655, 6)
Me.btnGetEmailAddresses.Name = "btnGetEmailAddresses"
Me.btnGetEmailAddresses.Size = New System.Drawing.Size(103, 23)
Me.btnGetEmailAddresses.TabIndex = 5
Me.btnGetEmailAddresses.Text = "Get Email Address"
Me.btnGetEmailAddresses.UseVisualStyleBackColor = true
'
'pnlTop1
'
Me.pnlTop1.Controls.Add(Me.txtNonPayerStaff)
Me.pnlTop1.Controls.Add(Me.txtNonPayerID)
Me.pnlTop1.Controls.Add(Me.txtNonResponderStaff)
Me.pnlTop1.Controls.Add(Me.txtAuditID)
Me.pnlTop1.Controls.Add(Me.lblFacilityNameTop)
Me.pnlTop1.Controls.Add(Me.txtNonRespondersID)
Me.pnlTop1.Controls.Add(Me.btnSearchForData)
Me.pnlTop1.Controls.Add(Me.Label1)
Me.pnlTop1.Controls.Add(Me.mtbAIRSNumber)
Me.pnlTop1.Dock = System.Windows.Forms.DockStyle.Top
Me.pnlTop1.Location = New System.Drawing.Point(0, 24)
Me.pnlTop1.Name = "pnlTop1"
Me.pnlTop1.Size = New System.Drawing.Size(792, 39)
Me.pnlTop1.TabIndex = 66
'
'txtNonPayerStaff
'
Me.txtNonPayerStaff.Location = New System.Drawing.Point(633, 12)
Me.txtNonPayerStaff.Name = "txtNonPayerStaff"
Me.txtNonPayerStaff.Size = New System.Drawing.Size(16, 20)
Me.txtNonPayerStaff.TabIndex = 393
Me.txtNonPayerStaff.Visible = false
'
'txtNonPayerID
'
Me.txtNonPayerID.Location = New System.Drawing.Point(582, 12)
Me.txtNonPayerID.Name = "txtNonPayerID"
Me.txtNonPayerID.Size = New System.Drawing.Size(16, 20)
Me.txtNonPayerID.TabIndex = 392
Me.txtNonPayerID.Visible = false
'
'txtNonResponderStaff
'
Me.txtNonResponderStaff.Location = New System.Drawing.Point(616, 12)
Me.txtNonResponderStaff.Name = "txtNonResponderStaff"
Me.txtNonResponderStaff.Size = New System.Drawing.Size(16, 20)
Me.txtNonResponderStaff.TabIndex = 391
Me.txtNonResponderStaff.Visible = false
'
'txtAuditID
'
Me.txtAuditID.Location = New System.Drawing.Point(599, 12)
Me.txtAuditID.Name = "txtAuditID"
Me.txtAuditID.Size = New System.Drawing.Size(16, 20)
Me.txtAuditID.TabIndex = 390
Me.txtAuditID.Visible = false
'
'lblFacilityNameTop
'
Me.lblFacilityNameTop.AutoSize = true
Me.lblFacilityNameTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lblFacilityNameTop.Location = New System.Drawing.Point(220, 12)
Me.lblFacilityNameTop.Name = "lblFacilityNameTop"
Me.lblFacilityNameTop.Size = New System.Drawing.Size(99, 16)
Me.lblFacilityNameTop.TabIndex = 4
Me.lblFacilityNameTop.Text = "Facilty Name"
'
'txtNonRespondersID
'
Me.txtNonRespondersID.Location = New System.Drawing.Point(198, 9)
Me.txtNonRespondersID.Name = "txtNonRespondersID"
Me.txtNonRespondersID.Size = New System.Drawing.Size(16, 20)
Me.txtNonRespondersID.TabIndex = 3
Me.txtNonRespondersID.Visible = false
'
'btnSearchForData
'
Me.btnSearchForData.AutoSize = true
Me.btnSearchForData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
Me.btnSearchForData.Location = New System.Drawing.Point(141, 9)
Me.btnSearchForData.Name = "btnSearchForData"
Me.btnSearchForData.Size = New System.Drawing.Size(51, 23)
Me.btnSearchForData.TabIndex = 1
Me.btnSearchForData.Text = "Search"
Me.btnSearchForData.UseVisualStyleBackColor = true
'
'Label1
'
Me.Label1.AutoSize = true
Me.Label1.Location = New System.Drawing.Point(11, 14)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(55, 13)
Me.Label1.TabIndex = 0
Me.Label1.Text = "AIRS No: "
'
'mtbAIRSNumber
'
Me.mtbAIRSNumber.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
Me.mtbAIRSNumber.Location = New System.Drawing.Point(68, 10)
Me.mtbAIRSNumber.Mask = "000-00000"
Me.mtbAIRSNumber.Name = "mtbAIRSNumber"
Me.mtbAIRSNumber.Size = New System.Drawing.Size(67, 20)
Me.mtbAIRSNumber.TabIndex = 0
Me.mtbAIRSNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
'
'IAIPFeeAuditTool
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(792, 746)
Me.Controls.Add(Me.TCNonResponders)
Me.Controls.Add(Me.StatusStrip1)
Me.Controls.Add(Me.pnlTop1)
Me.Controls.Add(Me.MenuStrip1)
Me.Name = "IAIPFeeAuditTool"
Me.Text = "IAIP Fee Audit Tool"
Me.StatusStrip1.ResumeLayout(false)
Me.StatusStrip1.PerformLayout
Me.MenuStrip1.ResumeLayout(false)
Me.MenuStrip1.PerformLayout
Me.TCNonResponders.ResumeLayout(false)
Me.TPTrackingData.ResumeLayout(false)
Me.Panel5.ResumeLayout(false)
Me.TCFeeAuditTracking.ResumeLayout(false)
Me.TP_Tracking_CY2008.ResumeLayout(false)
Me.Panel6.ResumeLayout(false)
Me.Panel6.PerformLayout
Me.Panel51.ResumeLayout(false)
Me.Panel51.PerformLayout
Me.Panel50.ResumeLayout(false)
Me.Panel50.PerformLayout
Me.Panel47.ResumeLayout(false)
Me.Panel47.PerformLayout
Me.Panel46.ResumeLayout(false)
Me.Panel46.PerformLayout
Me.Panel28.ResumeLayout(false)
Me.Panel28.PerformLayout
Me.Panel26.ResumeLayout(false)
Me.Panel26.PerformLayout
Me.Panel48.ResumeLayout(false)
Me.Panel48.PerformLayout
Me.TP_Tracking_CY2007.ResumeLayout(false)
Me.Panel58.ResumeLayout(false)
Me.Panel58.PerformLayout
Me.Panel59.ResumeLayout(false)
Me.Panel59.PerformLayout
Me.Panel60.ResumeLayout(false)
Me.Panel60.PerformLayout
Me.Panel61.ResumeLayout(false)
Me.Panel61.PerformLayout
Me.Panel62.ResumeLayout(false)
Me.Panel62.PerformLayout
Me.Panel63.ResumeLayout(false)
Me.Panel63.PerformLayout
Me.Panel64.ResumeLayout(false)
Me.Panel64.PerformLayout
Me.Panel52.ResumeLayout(false)
Me.Panel52.PerformLayout
Me.TP_Tracking_CY2006.ResumeLayout(false)
Me.Panel65.ResumeLayout(false)
Me.Panel65.PerformLayout
Me.Panel66.ResumeLayout(false)
Me.Panel66.PerformLayout
Me.Panel67.ResumeLayout(false)
Me.Panel67.PerformLayout
Me.Panel68.ResumeLayout(false)
Me.Panel68.PerformLayout
Me.Panel69.ResumeLayout(false)
Me.Panel69.PerformLayout
Me.Panel70.ResumeLayout(false)
Me.Panel70.PerformLayout
Me.Panel71.ResumeLayout(false)
Me.Panel71.PerformLayout
Me.Panel53.ResumeLayout(false)
Me.Panel53.PerformLayout
Me.TP_Tracking_CY2005.ResumeLayout(false)
Me.Panel72.ResumeLayout(false)
Me.Panel72.PerformLayout
Me.Panel73.ResumeLayout(false)
Me.Panel73.PerformLayout
Me.Panel74.ResumeLayout(false)
Me.Panel74.PerformLayout
Me.Panel75.ResumeLayout(false)
Me.Panel75.PerformLayout
Me.Panel76.ResumeLayout(false)
Me.Panel76.PerformLayout
Me.Panel77.ResumeLayout(false)
Me.Panel77.PerformLayout
Me.Panel78.ResumeLayout(false)
Me.Panel78.PerformLayout
Me.Panel54.ResumeLayout(false)
Me.Panel54.PerformLayout
Me.TP_Tracking_CY2004.ResumeLayout(false)
Me.Panel79.ResumeLayout(false)
Me.Panel79.PerformLayout
Me.Panel80.ResumeLayout(false)
Me.Panel80.PerformLayout
Me.Panel81.ResumeLayout(false)
Me.Panel81.PerformLayout
Me.Panel82.ResumeLayout(false)
Me.Panel82.PerformLayout
Me.Panel83.ResumeLayout(false)
Me.Panel83.PerformLayout
Me.Panel84.ResumeLayout(false)
Me.Panel84.PerformLayout
Me.Panel85.ResumeLayout(false)
Me.Panel85.PerformLayout
Me.Panel55.ResumeLayout(false)
Me.Panel55.PerformLayout
Me.TP_Tracking_CY2003.ResumeLayout(false)
Me.Panel86.ResumeLayout(false)
Me.Panel86.PerformLayout
Me.Panel87.ResumeLayout(false)
Me.Panel87.PerformLayout
Me.Panel88.ResumeLayout(false)
Me.Panel88.PerformLayout
Me.Panel89.ResumeLayout(false)
Me.Panel89.PerformLayout
Me.Panel90.ResumeLayout(false)
Me.Panel90.PerformLayout
Me.Panel91.ResumeLayout(false)
Me.Panel91.PerformLayout
Me.Panel92.ResumeLayout(false)
Me.Panel92.PerformLayout
Me.Panel56.ResumeLayout(false)
Me.Panel56.PerformLayout
Me.TP_Tracking_CY2002.ResumeLayout(false)
Me.Panel93.ResumeLayout(false)
Me.Panel93.PerformLayout
Me.Panel94.ResumeLayout(false)
Me.Panel94.PerformLayout
Me.Panel95.ResumeLayout(false)
Me.Panel95.PerformLayout
Me.Panel96.ResumeLayout(false)
Me.Panel96.PerformLayout
Me.Panel97.ResumeLayout(false)
Me.Panel97.PerformLayout
Me.Panel98.ResumeLayout(false)
Me.Panel98.PerformLayout
Me.Panel99.ResumeLayout(false)
Me.Panel99.PerformLayout
Me.Panel57.ResumeLayout(false)
Me.Panel57.PerformLayout
Me.TP_Tracking_OtherComments.ResumeLayout(false)
Me.TP_Tracking_OtherComments.PerformLayout
Me.TPNonResponders.ResumeLayout(false)
Me.Panel1.ResumeLayout(false)
Me.Panel1.PerformLayout
Me.TCNonRespondersData.ResumeLayout(false)
Me.TP_CY2008.ResumeLayout(false)
Me.Panel25.ResumeLayout(false)
Me.Panel25.PerformLayout
Me.gbCY2008.ResumeLayout(false)
Me.Panel24.ResumeLayout(false)
Me.Panel24.PerformLayout
Me.Panel19.ResumeLayout(false)
Me.Panel23.ResumeLayout(false)
Me.Panel23.PerformLayout
Me.Panel20.ResumeLayout(false)
Me.Panel20.PerformLayout
Me.Panel21.ResumeLayout(false)
Me.Panel21.PerformLayout
Me.Panel22.ResumeLayout(false)
Me.Panel22.PerformLayout
Me.Panel16.ResumeLayout(false)
Me.Panel18.ResumeLayout(false)
Me.Panel18.PerformLayout
Me.Panel17.ResumeLayout(false)
Me.Panel17.PerformLayout
Me.TP_CY2007.ResumeLayout(false)
Me.Panel2.ResumeLayout(false)
Me.Panel2.PerformLayout
Me.gbCY2007.ResumeLayout(false)
Me.Panel44.ResumeLayout(false)
Me.Panel44.PerformLayout
Me.Panel34.ResumeLayout(false)
Me.Panel36.ResumeLayout(false)
Me.Panel36.PerformLayout
Me.Panel37.ResumeLayout(false)
Me.Panel37.PerformLayout
Me.Panel38.ResumeLayout(false)
Me.Panel38.PerformLayout
Me.Panel39.ResumeLayout(false)
Me.Panel39.PerformLayout
Me.Panel27.ResumeLayout(false)
Me.Panel30.ResumeLayout(false)
Me.Panel30.PerformLayout
Me.Panel31.ResumeLayout(false)
Me.Panel31.PerformLayout
Me.TP_CY2006.ResumeLayout(false)
Me.Panel3.ResumeLayout(false)
Me.Panel3.PerformLayout
Me.gbCY2006.ResumeLayout(false)
Me.Panel45.ResumeLayout(false)
Me.Panel45.PerformLayout
Me.Panel35.ResumeLayout(false)
Me.Panel40.ResumeLayout(false)
Me.Panel40.PerformLayout
Me.Panel41.ResumeLayout(false)
Me.Panel41.PerformLayout
Me.Panel42.ResumeLayout(false)
Me.Panel42.PerformLayout
Me.Panel43.ResumeLayout(false)
Me.Panel43.PerformLayout
Me.Panel29.ResumeLayout(false)
Me.Panel32.ResumeLayout(false)
Me.Panel32.PerformLayout
Me.Panel33.ResumeLayout(false)
Me.Panel33.PerformLayout
Me.TP_Change_Questions.ResumeLayout(false)
Me.GroupBox1.ResumeLayout(false)
Me.GroupBox1.PerformLayout
Me.Panel49.ResumeLayout(false)
Me.Panel49.PerformLayout
Me.gbOwnership.ResumeLayout(false)
Me.gbOwnership.PerformLayout
Me.Panel4.ResumeLayout(false)
Me.Panel4.PerformLayout
Me.TP_Comments.ResumeLayout(false)
Me.gbComments.ResumeLayout(false)
Me.gbComments.PerformLayout
Me.gbTopData.ResumeLayout(false)
Me.gbTopData.PerformLayout
Me.Panel13.ResumeLayout(false)
Me.Panel13.PerformLayout
Me.Panel105.ResumeLayout(false)
Me.Panel105.PerformLayout
Me.Panel10.ResumeLayout(false)
Me.Panel11.ResumeLayout(false)
Me.Panel11.PerformLayout
Me.Panel12.ResumeLayout(false)
Me.Panel12.PerformLayout
Me.Panel15.ResumeLayout(false)
Me.Panel15.PerformLayout
Me.Panel14.ResumeLayout(false)
Me.Panel14.PerformLayout
Me.Panel9.ResumeLayout(false)
Me.Panel8.ResumeLayout(false)
Me.Panel8.PerformLayout
Me.Panel7.ResumeLayout(false)
Me.Panel7.PerformLayout
Me.TPNonPayers.ResumeLayout(false)
Me.TPNonPayers.PerformLayout
Me.GBContactInformation.ResumeLayout(false)
Me.GBContactInformation.PerformLayout
Me.GroupBox2.ResumeLayout(false)
Me.GBNonPayer_CY02.ResumeLayout(false)
Me.GBNonPayer_CY02.PerformLayout
Me.GBNonPayer_CY03.ResumeLayout(false)
Me.GBNonPayer_CY03.PerformLayout
Me.GBNonPayer_CY04.ResumeLayout(false)
Me.GBNonPayer_CY04.PerformLayout
Me.GBNonPayer_CY05.ResumeLayout(false)
Me.GBNonPayer_CY05.PerformLayout
Me.GBNonPayer_CY06.ResumeLayout(false)
Me.GBNonPayer_CY06.PerformLayout
Me.GBNonPayer_CY07.ResumeLayout(false)
Me.GBNonPayer_CY07.PerformLayout
Me.GBNonPayer_CY08.ResumeLayout(false)
Me.GBNonPayer_CY08.PerformLayout
Me.Panel102.ResumeLayout(false)
Me.Panel104.ResumeLayout(false)
Me.Panel104.PerformLayout
Me.Panel103.ResumeLayout(false)
Me.Panel103.PerformLayout
Me.Panel107.ResumeLayout(false)
Me.Panel107.PerformLayout
Me.Panel108.ResumeLayout(false)
Me.Panel108.PerformLayout
Me.TPAuditReport.ResumeLayout(false)
Me.Panel101.ResumeLayout(false)
CType(Me.dgvFeeAuditReport,System.ComponentModel.ISupportInitialize).EndInit
Me.Panel100.ResumeLayout(false)
Me.GroupBox3.ResumeLayout(false)
Me.GroupBox3.PerformLayout
Me.Panel111.ResumeLayout(false)
Me.Panel111.PerformLayout
Me.Panel106.ResumeLayout(false)
Me.Panel106.PerformLayout
Me.Panel110.ResumeLayout(false)
Me.Panel110.PerformLayout
Me.Panel109.ResumeLayout(false)
Me.Panel109.PerformLayout
Me.pnlTop1.ResumeLayout(false)
Me.pnlTop1.PerformLayout
Me.ResumeLayout(false)
Me.PerformLayout

End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents pnl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnl3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TCNonResponders As System.Windows.Forms.TabControl
    Friend WithEvents TPNonResponders As System.Windows.Forms.TabPage
    Friend WithEvents TPTrackingData As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents gbTopData As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbOwnership As System.Windows.Forms.GroupBox
    Friend WithEvents gbCY2006 As System.Windows.Forms.GroupBox
    Friend WithEvents gbCY2007 As System.Windows.Forms.GroupBox
    Friend WithEvents gbCY2008 As System.Windows.Forms.GroupBox
    Friend WithEvents gbComments As System.Windows.Forms.GroupBox
    Friend WithEvents mtbAIRSNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSearchForData As System.Windows.Forms.Button
    Friend WithEvents pnlTop1 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents lblContactAddress As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents lblFacilityName As System.Windows.Forms.Label
    Friend WithEvents llbNoteChanges As System.Windows.Forms.LinkLabel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityAddress2 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityAddress As System.Windows.Forms.Label
    Friend WithEvents lblContactCompany As System.Windows.Forms.Label
    Friend WithEvents lblContactTitle As System.Windows.Forms.Label
    Friend WithEvents lblContactName As System.Windows.Forms.Label
    Friend WithEvents lblContactPhoneNumber As System.Windows.Forms.Label
    Friend WithEvents lblContactAddress2 As System.Windows.Forms.Label
    Friend WithEvents txtEditFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents btnEditFacilityInfo As System.Windows.Forms.Button
    Friend WithEvents mtbEditZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEditFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents txtEditFacilityAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactCompany As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactFirstName As System.Windows.Forms.TextBox
    Friend WithEvents btnEditContactInfo As System.Windows.Forms.Button
    Friend WithEvents txtEditContactPhoneNumber As System.Windows.Forms.TextBox
    Friend WithEvents mtbEditContactZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEditContactCity As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtNonRespondersID As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactState As System.Windows.Forms.TextBox
    Friend WithEvents lblContactEmailAddress As System.Windows.Forms.Label
    Friend WithEvents txtEditContactEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSaveCurrentChange As System.Windows.Forms.Button
    Friend WithEvents txtCurrentComments As System.Windows.Forms.TextBox
    Friend WithEvents lblNSPS As System.Windows.Forms.Label
    Friend WithEvents lblTitleV As System.Windows.Forms.Label
    Friend WithEvents lblSourceClass As System.Windows.Forms.Label
    Friend WithEvents lblOperatingStatus As System.Windows.Forms.Label
    Friend WithEvents cboOperatingStatus As System.Windows.Forms.ComboBox
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents rdbNSPSNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNSPSYes As System.Windows.Forms.RadioButton
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents rdbTVNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTVYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents lblContactAddress2_CY2008 As System.Windows.Forms.Label
    Friend WithEvents lblContactCompany_CY2008 As System.Windows.Forms.Label
    Friend WithEvents lblContactName_CY2008 As System.Windows.Forms.Label
    Friend WithEvents lblContactAddress_CY2008 As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents lblNSPS_CY2008 As System.Windows.Forms.Label
    Friend WithEvents lblTitleV_CY2008 As System.Windows.Forms.Label
    Friend WithEvents lblSourceClass_CY2008 As System.Windows.Forms.Label
    Friend WithEvents lblOperatingStatus_CY2008 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityAddress2_CY2008 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityAddress_CY2008 As System.Windows.Forms.Label
    Friend WithEvents llbNoteChanges_CY2008 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblFacilityName_CY2008 As System.Windows.Forms.Label
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveCurrentChange_CY2008 As System.Windows.Forms.Button
    Friend WithEvents txtCurrentComments_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents txtEditContactState_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents btnEditContactInfo_CY2008 As System.Windows.Forms.Button
    Friend WithEvents mtbEditContactZipCode_CY2008 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEditContactCity_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactAddress_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactCompany_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactLastName_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactFirstName_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents cboOperatingStatus_CY2008 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents rdbNSPSNo_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNSPSYes_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents rdbTVNo_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTVYes_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents btnEditFacilityInfo_CY2008 As System.Windows.Forms.Button
    Friend WithEvents mtbEditZipCode_CY2008 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtEditFacilityCity_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditFacilityAddress_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditFacilityName_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents TCNonRespondersData As System.Windows.Forms.TabControl
    Friend WithEvents TP_CY2008 As System.Windows.Forms.TabPage
    Friend WithEvents TP_CY2007 As System.Windows.Forms.TabPage
    Friend WithEvents TP_CY2006 As System.Windows.Forms.TabPage
    Friend WithEvents TP_Change_Questions As System.Windows.Forms.TabPage
    Friend WithEvents TP_Comments As System.Windows.Forms.TabPage
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Friend WithEvents Panel34 As System.Windows.Forms.Panel
    Friend WithEvents Panel36 As System.Windows.Forms.Panel
    Friend WithEvents txtEditContactState_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents btnEditContactInfo_CY2007 As System.Windows.Forms.Button
    Friend WithEvents mtbEditContactZipCode_CY2007 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEditContactCity_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactAddress_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactCompany_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactLastName_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactFirstName_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Panel37 As System.Windows.Forms.Panel
    Friend WithEvents cboOperatingStatus_CY2007 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel38 As System.Windows.Forms.Panel
    Friend WithEvents rdbNSPSNo_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNSPSYes_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel39 As System.Windows.Forms.Panel
    Friend WithEvents rdbTVNo_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTVYes_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents btnEditFacilityInfo_CY2007 As System.Windows.Forms.Button
    Friend WithEvents mtbEditZipCode_CY2007 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtEditFacilityCity_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditFacilityAddress_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditFacilityName_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents lblContactAddress2_CY2007 As System.Windows.Forms.Label
    Friend WithEvents lblContactCompany_CY2007 As System.Windows.Forms.Label
    Friend WithEvents lblContactName_CY2007 As System.Windows.Forms.Label
    Friend WithEvents lblContactAddress_CY2007 As System.Windows.Forms.Label
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents lblNSPS_CY2007 As System.Windows.Forms.Label
    Friend WithEvents lblTitleV_CY2007 As System.Windows.Forms.Label
    Friend WithEvents lblSourceClass_CY2007 As System.Windows.Forms.Label
    Friend WithEvents lblOperatingStatus_CY2007 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityAddress2_CY2007 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityAddress_CY2007 As System.Windows.Forms.Label
    Friend WithEvents llbNoteChanges_CY2007 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblFacilityName_CY2007 As System.Windows.Forms.Label
    Friend WithEvents Panel35 As System.Windows.Forms.Panel
    Friend WithEvents Panel40 As System.Windows.Forms.Panel
    Friend WithEvents txtEditContactState_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents btnEditContactInfo_CY2006 As System.Windows.Forms.Button
    Friend WithEvents mtbEditContactZipCode_CY2006 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtEditContactCity_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactAddress_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactCompany_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactLastName_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditContactFirstName_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Panel41 As System.Windows.Forms.Panel
    Friend WithEvents cboOperatingStatus_CY2006 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel42 As System.Windows.Forms.Panel
    Friend WithEvents rdbNSPSNo_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNSPSYes_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel43 As System.Windows.Forms.Panel
    Friend WithEvents rdbTVNo_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTVYes_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents btnEditFacilityInfo_CY2006 As System.Windows.Forms.Button
    Friend WithEvents mtbEditZipCode_CY2006 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents txtEditFacilityCity_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditFacilityAddress_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditFacilityName_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents Panel32 As System.Windows.Forms.Panel
    Friend WithEvents lblContactAddress2_CY2006 As System.Windows.Forms.Label
    Friend WithEvents lblContactCompany_CY2006 As System.Windows.Forms.Label
    Friend WithEvents lblContactName_CY2006 As System.Windows.Forms.Label
    Friend WithEvents lblContactAddress_CY2006 As System.Windows.Forms.Label
    Friend WithEvents Panel33 As System.Windows.Forms.Panel
    Friend WithEvents lblNSPS_CY2006 As System.Windows.Forms.Label
    Friend WithEvents lblTitleV_CY2006 As System.Windows.Forms.Label
    Friend WithEvents lblSourceClass_CY2006 As System.Windows.Forms.Label
    Friend WithEvents lblOperatingStatus_CY2006 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityAddress2_CY2006 As System.Windows.Forms.Label
    Friend WithEvents lblFacilityAddress_CY2006 As System.Windows.Forms.Label
    Friend WithEvents llbNoteChanges_CY2006 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblFacilityName_CY2006 As System.Windows.Forms.Label
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveCurrentChange_CY2007 As System.Windows.Forms.Button
    Friend WithEvents txtCurrentComments_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Panel45 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveCurrentChange_CY2006 As System.Windows.Forms.Button
    Friend WithEvents txtCurrentComments_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtAIRSNumber_08 As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber_07 As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber_06 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditSourceClass As System.Windows.Forms.TextBox
    Friend WithEvents txtEditSourceClass_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditSourceClass_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents txtEditSourceClass_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveOwnershipChanges As System.Windows.Forms.Button
    Friend WithEvents txtOwnershipChangeComments As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbOwnershipChangeNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOwnershipChangeYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnSaveComments As System.Windows.Forms.Button
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents TCFeeAuditTracking As System.Windows.Forms.TabControl
    Friend WithEvents TP_Tracking_CY2008 As System.Windows.Forms.TabPage
    Friend WithEvents TP_Tracking_CY2007 As System.Windows.Forms.TabPage
    Friend WithEvents TP_Tracking_CY2006 As System.Windows.Forms.TabPage
    Friend WithEvents TP_Tracking_OtherComments As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DTPInitialLetter_2008 As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSaveFeeAudit_CY2008 As System.Windows.Forms.Button
    Friend WithEvents txtComments_CY2008 As System.Windows.Forms.TextBox
    Friend WithEvents DTPCloseOut_CY2008 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAmountPaid_CY2008 As System.Windows.Forms.Label
    Friend WithEvents DTPFeesPaid_CY2008 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPAOSent_CY2008 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPCOSent_CY2008 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPNOVSent_CY2008 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel47 As System.Windows.Forms.Panel
    Friend WithEvents rdbUnabletoContactNo_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnabletoContactYes_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel46 As System.Windows.Forms.Panel
    Friend WithEvents rdbBankruptcyNo_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBankruptcyYes_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Friend WithEvents rdbDataCorrectNo_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDataCorrectYes_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterRemailed_CY2008 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Friend WithEvents rdbAddressUnknownNo_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAddressUnknownYes_CY2008 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterReturned_CY2008 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel48 As System.Windows.Forms.Panel
    Friend WithEvents lblAuditType_CY2008 As System.Windows.Forms.Label
    Friend WithEvents btnSaveAuditComments As System.Windows.Forms.Button
    Friend WithEvents txtAuditComments As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents TP_Tracking_CY2005 As System.Windows.Forms.TabPage
    Friend WithEvents TP_Tracking_CY2004 As System.Windows.Forms.TabPage
    Friend WithEvents TP_Tracking_CY2003 As System.Windows.Forms.TabPage
    Friend WithEvents TP_Tracking_CY2002 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSaveSourceClassificationChanges As System.Windows.Forms.Button
    Friend WithEvents txtSourceClassificationChangeComment As System.Windows.Forms.TextBox
    Friend WithEvents RadioButton9 As System.Windows.Forms.RadioButton
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Panel49 As System.Windows.Forms.Panel
    Friend WithEvents rdbSourceClassChangeNO As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSourceClassChangeYes As System.Windows.Forms.RadioButton
    Friend WithEvents lblFacilityNameTop As System.Windows.Forms.Label
    Friend WithEvents Panel50 As System.Windows.Forms.Panel
    Friend WithEvents lblStaffAssigned_08 As System.Windows.Forms.Label
    Friend WithEvents Panel51 As System.Windows.Forms.Panel
    Friend WithEvents lblSignOffDat_08 As System.Windows.Forms.Label
    Friend WithEvents lblManagerSignOff_08 As System.Windows.Forms.Label
    Friend WithEvents lblLastModified_08 As System.Windows.Forms.Label
    Friend WithEvents Panel58 As System.Windows.Forms.Panel
    Friend WithEvents Panel59 As System.Windows.Forms.Panel
    Friend WithEvents lblSignOffDat_07 As System.Windows.Forms.Label
    Friend WithEvents lblManagerSignOff_07 As System.Windows.Forms.Label
    Friend WithEvents Panel60 As System.Windows.Forms.Panel
    Friend WithEvents lblLastModified_07 As System.Windows.Forms.Label
    Friend WithEvents lblStaffAssigned_07 As System.Windows.Forms.Label
    Friend WithEvents txtComments_CY2007 As System.Windows.Forms.TextBox
    Friend WithEvents DTPCloseOut_CY2007 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAmountPaid_CY2007 As System.Windows.Forms.Label
    Friend WithEvents DTPFeesPaid_CY2007 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPAOSent_CY2007 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPCOSent_CY2007 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPNOVSent_CY2007 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel61 As System.Windows.Forms.Panel
    Friend WithEvents rdbUnabletoContactNo_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnabletoContactYes_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel62 As System.Windows.Forms.Panel
    Friend WithEvents rdbBankruptcyNo_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBankruptcyYes_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel63 As System.Windows.Forms.Panel
    Friend WithEvents rdbDataCorrectNo_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDataCorrectYes_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterRemailed_CY2007 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel64 As System.Windows.Forms.Panel
    Friend WithEvents rdbAddressUnknownNo_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAddressUnknownYes_CY2007 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterReturned_CY2007 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPInitialLetter_2007 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents Panel52 As System.Windows.Forms.Panel
    Friend WithEvents lblAuditType_CY2007 As System.Windows.Forms.Label
    Friend WithEvents btnSaveFeeAudit_CY2007 As System.Windows.Forms.Button
    Friend WithEvents Panel65 As System.Windows.Forms.Panel
    Friend WithEvents Panel66 As System.Windows.Forms.Panel
    Friend WithEvents lblSignOffDat_06 As System.Windows.Forms.Label
    Friend WithEvents lblManagerSignOff_06 As System.Windows.Forms.Label
    Friend WithEvents Panel67 As System.Windows.Forms.Panel
    Friend WithEvents lblLastModified_06 As System.Windows.Forms.Label
    Friend WithEvents lblStaffAssigned_06 As System.Windows.Forms.Label
    Friend WithEvents txtComments_CY2006 As System.Windows.Forms.TextBox
    Friend WithEvents DTPCloseOut_CY2006 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAmountPaid_CY2006 As System.Windows.Forms.Label
    Friend WithEvents DTPFeesPaid_CY2006 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPAOSent_CY2006 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPCOSent_CY2006 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPNOVSent_CY2006 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel68 As System.Windows.Forms.Panel
    Friend WithEvents rdbUnabletoContactNo_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnabletoContactYes_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel69 As System.Windows.Forms.Panel
    Friend WithEvents rdbBankruptcyNo_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBankruptcyYes_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel70 As System.Windows.Forms.Panel
    Friend WithEvents rdbDataCorrectNo_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDataCorrectYes_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterRemailed_CY2006 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel71 As System.Windows.Forms.Panel
    Friend WithEvents rdbAddressUnknownNo_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAddressUnknownYes_CY2006 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterReturned_CY2006 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPInitialLetter_2006 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Panel53 As System.Windows.Forms.Panel
    Friend WithEvents lblAuditType_CY2006 As System.Windows.Forms.Label
    Friend WithEvents btnSaveFeeAudit_CY2006 As System.Windows.Forms.Button
    Friend WithEvents Panel72 As System.Windows.Forms.Panel
    Friend WithEvents Panel73 As System.Windows.Forms.Panel
    Friend WithEvents lblSignOffDat_05 As System.Windows.Forms.Label
    Friend WithEvents lblManagerSignOff_05 As System.Windows.Forms.Label
    Friend WithEvents Panel74 As System.Windows.Forms.Panel
    Friend WithEvents lblLastModified_05 As System.Windows.Forms.Label
    Friend WithEvents lblStaffAssigned_05 As System.Windows.Forms.Label
    Friend WithEvents txtComments_CY2005 As System.Windows.Forms.TextBox
    Friend WithEvents DTPCloseOut_CY2005 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAmountPaid_CY2005 As System.Windows.Forms.Label
    Friend WithEvents DTPFeesPaid_CY2005 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPAOSent_CY2005 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPCOSent_CY2005 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPNOVSent_CY2005 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel75 As System.Windows.Forms.Panel
    Friend WithEvents rdbUnabletoContactNo_CY2005 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnabletoContactYes_CY2005 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel76 As System.Windows.Forms.Panel
    Friend WithEvents rdbBankruptcyNo_CY2005 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBankruptcyYes_CY2005 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel77 As System.Windows.Forms.Panel
    Friend WithEvents rdbDataCorrectNo_CY2005 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDataCorrectYes_CY2005 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterRemailed_CY2005 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel78 As System.Windows.Forms.Panel
    Friend WithEvents rdbAddressUnknownNo_CY2005 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAddressUnknownYes_CY2005 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterReturned_CY2005 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPInitialLetter_2005 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents Panel54 As System.Windows.Forms.Panel
    Friend WithEvents lblAuditType_CY2005 As System.Windows.Forms.Label
    Friend WithEvents btnSaveFeeAudit_CY2005 As System.Windows.Forms.Button
    Friend WithEvents Panel79 As System.Windows.Forms.Panel
    Friend WithEvents Panel80 As System.Windows.Forms.Panel
    Friend WithEvents lblSignOffDat_04 As System.Windows.Forms.Label
    Friend WithEvents lblManagerSignOff_04 As System.Windows.Forms.Label
    Friend WithEvents Panel81 As System.Windows.Forms.Panel
    Friend WithEvents lblLastModified_04 As System.Windows.Forms.Label
    Friend WithEvents lblStaffAssigned_04 As System.Windows.Forms.Label
    Friend WithEvents txtComments_CY2004 As System.Windows.Forms.TextBox
    Friend WithEvents DTPCloseOut_CY2004 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAmountPaid_CY2004 As System.Windows.Forms.Label
    Friend WithEvents DTPFeesPaid_CY2004 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPAOSent_CY2004 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPCOSent_CY2004 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPNOVSent_CY2004 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel82 As System.Windows.Forms.Panel
    Friend WithEvents rdbUnabletoContactNo_CY2004 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnabletoContactYes_CY2004 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel83 As System.Windows.Forms.Panel
    Friend WithEvents rdbBankruptcyNo_CY2004 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBankruptcyYes_CY2004 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel84 As System.Windows.Forms.Panel
    Friend WithEvents rdbDataCorrectNo_CY2004 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDataCorrectYes_CY2004 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterRemailed_CY2004 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel85 As System.Windows.Forms.Panel
    Friend WithEvents rdbAddressUnknownNo_CY2004 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAddressUnknownYes_CY2004 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterReturned_CY2004 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPInitialLetter_2004 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label121 As System.Windows.Forms.Label
    Friend WithEvents Label122 As System.Windows.Forms.Label
    Friend WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents Label128 As System.Windows.Forms.Label
    Friend WithEvents Label129 As System.Windows.Forms.Label
    Friend WithEvents Label130 As System.Windows.Forms.Label
    Friend WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents Label132 As System.Windows.Forms.Label
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents Panel55 As System.Windows.Forms.Panel
    Friend WithEvents lblAuditType_CY2004 As System.Windows.Forms.Label
    Friend WithEvents btnSaveFeeAudit_CY2004 As System.Windows.Forms.Button
    Friend WithEvents Panel56 As System.Windows.Forms.Panel
    Friend WithEvents lblAuditType_CY2003 As System.Windows.Forms.Label
    Friend WithEvents btnSaveFeeAudit_CY2003 As System.Windows.Forms.Button
    Friend WithEvents Panel57 As System.Windows.Forms.Panel
    Friend WithEvents lblAuditType_CY2002 As System.Windows.Forms.Label
    Friend WithEvents btnSaveFeeAudit_CY2002 As System.Windows.Forms.Button
    Friend WithEvents Panel86 As System.Windows.Forms.Panel
    Friend WithEvents Panel87 As System.Windows.Forms.Panel
    Friend WithEvents lblSignOffDat_03 As System.Windows.Forms.Label
    Friend WithEvents lblManagerSignOff_03 As System.Windows.Forms.Label
    Friend WithEvents Panel88 As System.Windows.Forms.Panel
    Friend WithEvents lblLastModified_03 As System.Windows.Forms.Label
    Friend WithEvents lblStaffAssigned_03 As System.Windows.Forms.Label
    Friend WithEvents txtComments_CY2003 As System.Windows.Forms.TextBox
    Friend WithEvents DTPCloseOut_CY2003 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAmountPaid_CY2003 As System.Windows.Forms.Label
    Friend WithEvents DTPFeesPaid_CY2003 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPAOSent_CY2003 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPCOSent_CY2003 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPNOVSent_CY2003 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel89 As System.Windows.Forms.Panel
    Friend WithEvents rdbUnabletoContactNo_CY2003 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnabletoContactYes_CY2003 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel90 As System.Windows.Forms.Panel
    Friend WithEvents rdbBankruptcyNo_CY2003 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBankruptcyYes_CY2003 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel91 As System.Windows.Forms.Panel
    Friend WithEvents rdbDataCorrectNo_CY2003 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDataCorrectYes_CY2003 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterRemailed_CY2003 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel92 As System.Windows.Forms.Panel
    Friend WithEvents rdbAddressUnknownNo_CY2003 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAddressUnknownYes_CY2003 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterReturned_CY2003 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPInitialLetter_2003 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents Label142 As System.Windows.Forms.Label
    Friend WithEvents Label143 As System.Windows.Forms.Label
    Friend WithEvents Label144 As System.Windows.Forms.Label
    Friend WithEvents Label145 As System.Windows.Forms.Label
    Friend WithEvents Label146 As System.Windows.Forms.Label
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents Panel93 As System.Windows.Forms.Panel
    Friend WithEvents Panel94 As System.Windows.Forms.Panel
    Friend WithEvents lblSignOffDat_02 As System.Windows.Forms.Label
    Friend WithEvents lblManagerSignOff_02 As System.Windows.Forms.Label
    Friend WithEvents Panel95 As System.Windows.Forms.Panel
    Friend WithEvents lblLastModified_02 As System.Windows.Forms.Label
    Friend WithEvents lblStaffAssigned_02 As System.Windows.Forms.Label
    Friend WithEvents txtComments_CY2002 As System.Windows.Forms.TextBox
    Friend WithEvents DTPCloseOut_CY2002 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAmountPaid_CY2002 As System.Windows.Forms.Label
    Friend WithEvents DTPFeesPaid_CY2002 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPAOSent_CY2002 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPCOSent_CY2002 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPNOVSent_CY2002 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel96 As System.Windows.Forms.Panel
    Friend WithEvents rdbUnabletoContactNo_CY2002 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnabletoContactYes_CY2002 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel97 As System.Windows.Forms.Panel
    Friend WithEvents rdbBankruptcyNo_CY2002 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBankruptcyYes_CY2002 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel98 As System.Windows.Forms.Panel
    Friend WithEvents rdbDataCorrectNo_CY2002 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDataCorrectYes_CY2002 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterRemailed_CY2002 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel99 As System.Windows.Forms.Panel
    Friend WithEvents rdbAddressUnknownNo_CY2002 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAddressUnknownYes_CY2002 As System.Windows.Forms.RadioButton
    Friend WithEvents DTPLetterReturned_CY2002 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPInitialLetter_2002 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents Label161 As System.Windows.Forms.Label
    Friend WithEvents Label162 As System.Windows.Forms.Label
    Friend WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents Label166 As System.Windows.Forms.Label
    Friend WithEvents Label167 As System.Windows.Forms.Label
    Friend WithEvents Label168 As System.Windows.Forms.Label
    Friend WithEvents Label169 As System.Windows.Forms.Label
    Friend WithEvents txtAuditID As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents btnManagerSignOff_CY2008 As System.Windows.Forms.Button
    Friend WithEvents btnManagerSignOff_CY2007 As System.Windows.Forms.Button
    Friend WithEvents btnManagerSignOff_CY2006 As System.Windows.Forms.Button
    Friend WithEvents btnManagerSignOff_CY2005 As System.Windows.Forms.Button
    Friend WithEvents btnManagerSignOff_CY2004 As System.Windows.Forms.Button
    Friend WithEvents btnManagerSignOff_CY2003 As System.Windows.Forms.Button
    Friend WithEvents btnManagerSignOff_CY2002 As System.Windows.Forms.Button
    Friend WithEvents TPAuditReport As System.Windows.Forms.TabPage
    Friend WithEvents Panel101 As System.Windows.Forms.Panel
    Friend WithEvents Panel100 As System.Windows.Forms.Panel
    Friend WithEvents dgvFeeAuditReport As System.Windows.Forms.DataGridView
    Friend WithEvents btnNonRespondersData As System.Windows.Forms.Button
    Friend WithEvents btnViewFullAuditData As System.Windows.Forms.Button
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents TPNonPayers As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel102 As System.Windows.Forms.Panel
    Friend WithEvents Panel104 As System.Windows.Forms.Panel
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerContactPhoneNumber As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerContactAddress As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerContactCompany As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerContactTitle As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerContactName As System.Windows.Forms.Label
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents Panel103 As System.Windows.Forms.Panel
    Friend WithEvents lblNonPayerNSPSStatus As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTVStatus As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerSourceClass As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerOpStatus As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerFacilityAddress As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerFacilityName As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerContactEmail As System.Windows.Forms.Label
    Friend WithEvents Panel105 As System.Windows.Forms.Panel
    Friend WithEvents rdbNonResponderActive As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNonResponderInactive As System.Windows.Forms.RadioButton
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents btnFlagNonResponder As System.Windows.Forms.Button
    Friend WithEvents txtNonResponderStaff As System.Windows.Forms.TextBox
    Friend WithEvents Panel106 As System.Windows.Forms.Panel
    Friend WithEvents rdbActive As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInactive As System.Windows.Forms.RadioButton
    Friend WithEvents GBNonPayer_CY07 As System.Windows.Forms.GroupBox
    Friend WithEvents GBNonPayer_CY08 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNonPayerBalance_CY08 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalPaid_CY08 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalDue_CY08 As System.Windows.Forms.Label
    Friend WithEvents GBNonPayer_CY02 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNonPayerBalance_CY02 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalPaid_CY02 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalDue_CY02 As System.Windows.Forms.Label
    Friend WithEvents GBNonPayer_CY03 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNonPayerBalance_CY03 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalPaid_CY03 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalDue_CY03 As System.Windows.Forms.Label
    Friend WithEvents GBNonPayer_CY04 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNonPayerBalance_CY04 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalPaid_CY04 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalDue_CY04 As System.Windows.Forms.Label
    Friend WithEvents GBNonPayer_CY05 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNonPayerBalance_CY05 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalPaid_CY05 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalDue_CY05 As System.Windows.Forms.Label
    Friend WithEvents GBNonPayer_CY06 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNonPayerBalance_CY06 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalPaid_CY06 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalDue_CY06 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerBalance_CY07 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalPaid_CY07 As System.Windows.Forms.Label
    Friend WithEvents lblNonPayerTotalDue_CY07 As System.Windows.Forms.Label
    Friend WithEvents btnViewAllNonPayerData As System.Windows.Forms.Button
    Friend WithEvents btnGetEmailAddresses As System.Windows.Forms.Button
    Friend WithEvents btnFlagNonPayer As System.Windows.Forms.Button
    Friend WithEvents Panel107 As System.Windows.Forms.Panel
    Friend WithEvents rdbNonPayerActive As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNonPayerInactive As System.Windows.Forms.RadioButton
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents txtNonPayerID As System.Windows.Forms.TextBox
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents Panel108 As System.Windows.Forms.Panel
    Friend WithEvents txtNonPayersComments As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveNonPayerComments As System.Windows.Forms.Button
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents txtNonPayerStaff As System.Windows.Forms.TextBox
    Friend WithEvents lblNonPayerStaff As System.Windows.Forms.Label
    Friend WithEvents lblNonResponderStaff As System.Windows.Forms.Label
    Friend WithEvents Panel109 As System.Windows.Forms.Panel
    Friend WithEvents rdbNonPayers As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNonResponders As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents lblCY2008Status As System.Windows.Forms.Label
    Friend WithEvents lblCY2007Status As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents lblCY2006Status As System.Windows.Forms.Label
    Friend WithEvents rdbActiveAll As System.Windows.Forms.RadioButton
    Friend WithEvents Panel110 As System.Windows.Forms.Panel
    Friend WithEvents rdbAllClosedOut As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAuditNotClosedout As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAuditClosedOut As System.Windows.Forms.RadioButton
    Friend WithEvents btnSaveAndUpdate As System.Windows.Forms.Button
    Friend WithEvents GBContactInformation As System.Windows.Forms.GroupBox
    Friend WithEvents txtNonPayerEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtNonPayerState As System.Windows.Forms.TextBox
    Friend WithEvents btnNonPayerSave As System.Windows.Forms.Button
    Friend WithEvents txtNonPayerPhoneNumber As System.Windows.Forms.TextBox
    Friend WithEvents mtbNonPayerZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtNonPayerCity As System.Windows.Forms.TextBox
    Friend WithEvents txtNonPayerAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtNonPayerCompany As System.Windows.Forms.TextBox
    Friend WithEvents txtNonPayerTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtNonPayerLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtNonPayerFirstname As System.Windows.Forms.TextBox
    Friend WithEvents Panel111 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNonPayerSent As System.Windows.Forms.TextBox
    Friend WithEvents txtNonRespondersSent As System.Windows.Forms.TextBox
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents btnRunStats As System.Windows.Forms.Button
    Friend WithEvents txtTotalLetterSent As System.Windows.Forms.TextBox
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents txtPossibleNOV As System.Windows.Forms.TextBox
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents txtClosedOutAudits As System.Windows.Forms.TextBox
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents btnCopyData As System.Windows.Forms.Button
    Friend WithEvents txtOpenAudits As System.Windows.Forms.TextBox
    Friend WithEvents Label116 As System.Windows.Forms.Label
End Class
