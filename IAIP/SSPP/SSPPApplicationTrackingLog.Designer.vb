<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SSPPApplicationTrackingLog
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnRefreshAIRSNo = New System.Windows.Forms.Button()
        Me.lblLinkWarning = New System.Windows.Forms.Label()
        Me.rtbFacilityInformation = New System.Windows.Forms.RichTextBox()
        Me.txtAIRSNumber = New Iaip.AirNumberEntryForm()
        Me.lblAppNumber = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblApplicationUnit = New System.Windows.Forms.Label()
        Me.lblEngineer = New System.Windows.Forms.Label()
        Me.cboApplicationUnit = New System.Windows.Forms.ComboBox()
        Me.cboEngineer = New System.Windows.Forms.ComboBox()
        Me.lblApplicationType = New System.Windows.Forms.Label()
        Me.cboApplicationType = New System.Windows.Forms.ComboBox()
        Me.chbClosedOut = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtOutstandingApplication = New System.Windows.Forms.TextBox()
        Me.TPSubPartEditor = New System.Windows.Forms.TabPage()
        Me.TCSupParts = New System.Windows.Forms.TabControl()
        Me.TPSIP = New System.Windows.Forms.TabPage()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.dgvSIPSubParts = New System.Windows.Forms.DataGridView()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.btnSaveSIPSubpart = New System.Windows.Forms.Button()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.dgvSIPSubpartAddEdit = New System.Windows.Forms.DataGridView()
        Me.btnSIPEditAll = New System.Windows.Forms.Button()
        Me.btnSIPUneditAll = New System.Windows.Forms.Button()
        Me.btnClearAddModifiedSIPs = New System.Windows.Forms.Button()
        Me.btnSIPUnedit = New System.Windows.Forms.Button()
        Me.btnSIPEdit = New System.Windows.Forms.Button()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.dgvSIPSubPartDelete = New System.Windows.Forms.DataGridView()
        Me.btnClearSIPDeletes = New System.Windows.Forms.Button()
        Me.btnSIPDelete = New System.Windows.Forms.Button()
        Me.btnSIPUndeleteAll = New System.Windows.Forms.Button()
        Me.btnSIPDeleteAll = New System.Windows.Forms.Button()
        Me.btnSIPUndelete = New System.Windows.Forms.Button()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.btnAddNewSIPSubpart = New System.Windows.Forms.Button()
        Me.cboSIPSubpart = New System.Windows.Forms.ComboBox()
        Me.TPPart60 = New System.Windows.Forms.TabPage()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.dgvNSPSSubParts = New System.Windows.Forms.DataGridView()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.btnSaveNSPSSubpart = New System.Windows.Forms.Button()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.dgvNSPSSubpartAddEdit = New System.Windows.Forms.DataGridView()
        Me.btnNSPSEditAll = New System.Windows.Forms.Button()
        Me.btnNSPSUneditAll = New System.Windows.Forms.Button()
        Me.btnClearAddModifiedNSPSs = New System.Windows.Forms.Button()
        Me.btnNSPSUnedit = New System.Windows.Forms.Button()
        Me.btnNSPSEdit = New System.Windows.Forms.Button()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.dgvNSPSSubPartDelete = New System.Windows.Forms.DataGridView()
        Me.btnClearNSPSDeletes = New System.Windows.Forms.Button()
        Me.btnNSPSDelete = New System.Windows.Forms.Button()
        Me.btnNSPSUndeleteAll = New System.Windows.Forms.Button()
        Me.btnNSPSDeleteAll = New System.Windows.Forms.Button()
        Me.btnNSPSUndelete = New System.Windows.Forms.Button()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.btnAddNewNSPSSubpart = New System.Windows.Forms.Button()
        Me.cboNSPSSubpart = New System.Windows.Forms.ComboBox()
        Me.TPPart61 = New System.Windows.Forms.TabPage()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.dgvNESHAPSubParts = New System.Windows.Forms.DataGridView()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.btnSaveNESHAPSubpart = New System.Windows.Forms.Button()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.dgvNESHAPSubpartAddEdit = New System.Windows.Forms.DataGridView()
        Me.btnNESHAPEditAll = New System.Windows.Forms.Button()
        Me.btnNESHAPUneditAll = New System.Windows.Forms.Button()
        Me.btnClearAddModifiedNESHAPs = New System.Windows.Forms.Button()
        Me.btnNESHAPUnedit = New System.Windows.Forms.Button()
        Me.btnNESHAPEdit = New System.Windows.Forms.Button()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.dgvNESHAPSubPartDelete = New System.Windows.Forms.DataGridView()
        Me.btnClearNESHAPDeletes = New System.Windows.Forms.Button()
        Me.btnNESHAPDelete = New System.Windows.Forms.Button()
        Me.btnNESHAPUndeleteAll = New System.Windows.Forms.Button()
        Me.btnNESHAPDeleteAll = New System.Windows.Forms.Button()
        Me.btnNESHAPUndelete = New System.Windows.Forms.Button()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.btnAddNewNESHAPSubpart = New System.Windows.Forms.Button()
        Me.cboNESHAPSubpart = New System.Windows.Forms.ComboBox()
        Me.TPPart63 = New System.Windows.Forms.TabPage()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.dgvMACTSubParts = New System.Windows.Forms.DataGridView()
        Me.Panel29 = New System.Windows.Forms.Panel()
        Me.btnSaveMACTSubpart = New System.Windows.Forms.Button()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Panel30 = New System.Windows.Forms.Panel()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.dgvMACTSubpartAddEdit = New System.Windows.Forms.DataGridView()
        Me.btnMACTEditAll = New System.Windows.Forms.Button()
        Me.btnMACTUneditAll = New System.Windows.Forms.Button()
        Me.btnClearAddModifiedMACTs = New System.Windows.Forms.Button()
        Me.btnMACTUnedit = New System.Windows.Forms.Button()
        Me.btnMACTEdit = New System.Windows.Forms.Button()
        Me.Panel31 = New System.Windows.Forms.Panel()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.dgvMACTSubPartDelete = New System.Windows.Forms.DataGridView()
        Me.btnClearMACTDeletes = New System.Windows.Forms.Button()
        Me.btnMACTDelete = New System.Windows.Forms.Button()
        Me.btnMACTUndeleteAll = New System.Windows.Forms.Button()
        Me.btnMACTDeleteAll = New System.Windows.Forms.Button()
        Me.btnMACTUndelete = New System.Windows.Forms.Button()
        Me.Panel32 = New System.Windows.Forms.Panel()
        Me.btnAddNewMACTSubpart = New System.Windows.Forms.Button()
        Me.cboMACTSubpart = New System.Windows.Forms.ComboBox()
        Me.TPDocuments = New System.Windows.Forms.TabPage()
        Me.PanelTitleV = New System.Windows.Forms.Panel()
        Me.btnTVFinalDownload = New System.Windows.Forms.Button()
        Me.btnTVPublicNoticeDownload = New System.Windows.Forms.Button()
        Me.btnTVDraftDownload = New System.Windows.Forms.Button()
        Me.btnTVNarrativeDownload = New System.Windows.Forms.Button()
        Me.lblTVFinalDUPDF = New System.Windows.Forms.Label()
        Me.lblTVFinalSRPDF = New System.Windows.Forms.Label()
        Me.lblTVFinalDUDoc = New System.Windows.Forms.Label()
        Me.lblTVFinalSRDoc = New System.Windows.Forms.Label()
        Me.lblTVPublicNoticeDUPDF = New System.Windows.Forms.Label()
        Me.lblTVPublicNoticeSRPDF = New System.Windows.Forms.Label()
        Me.lblTVPublicNoticeDUDoc = New System.Windows.Forms.Label()
        Me.lblTVPublicNoticeSRDoc = New System.Windows.Forms.Label()
        Me.lblTVDraftDUPDF = New System.Windows.Forms.Label()
        Me.lblTVDraftSRPDF = New System.Windows.Forms.Label()
        Me.lblTVDraftDUDoc = New System.Windows.Forms.Label()
        Me.lblTVDraftSRDoc = New System.Windows.Forms.Label()
        Me.lblTVNarrativeDUPDF = New System.Windows.Forms.Label()
        Me.lblTVNarrativeSRPDF = New System.Windows.Forms.Label()
        Me.lblTVNarrativeDUDoc = New System.Windows.Forms.Label()
        Me.lblTVNarrativeSRDoc = New System.Windows.Forms.Label()
        Me.txtTVFinalPDF = New System.Windows.Forms.TextBox()
        Me.txtTVPublicNoticePDF = New System.Windows.Forms.TextBox()
        Me.txtTVDraftPDF = New System.Windows.Forms.TextBox()
        Me.txtTVNarrativePDF = New System.Windows.Forms.TextBox()
        Me.txtTVFinalDoc = New System.Windows.Forms.TextBox()
        Me.txtTVPublicNoticeDoc = New System.Windows.Forms.TextBox()
        Me.txtTVDraftDoc = New System.Windows.Forms.TextBox()
        Me.txtTVNarrativeDoc = New System.Windows.Forms.TextBox()
        Me.chbTVFinal = New System.Windows.Forms.CheckBox()
        Me.chbTVPublicNotice = New System.Windows.Forms.CheckBox()
        Me.chbTVDraft = New System.Windows.Forms.CheckBox()
        Me.chbTVNarrative = New System.Windows.Forms.CheckBox()
        Me.PanelPSD = New System.Windows.Forms.Panel()
        Me.btnPSDNarrativeDownload = New System.Windows.Forms.Button()
        Me.lblPSDNarrativeDUPDF = New System.Windows.Forms.Label()
        Me.lblPSDNarrativeDUDoc = New System.Windows.Forms.Label()
        Me.txtPSDNarrativePDF = New System.Windows.Forms.TextBox()
        Me.txtPSDNarrativeDoc = New System.Windows.Forms.TextBox()
        Me.chbPSDNarrative = New System.Windows.Forms.CheckBox()
        Me.lblPSDNarrativeSRPDF = New System.Windows.Forms.Label()
        Me.lblPSDNarrativeSRDoc = New System.Windows.Forms.Label()
        Me.btnPSDPublicNoticeDownload = New System.Windows.Forms.Button()
        Me.btnPSDDraftPermitDownload = New System.Windows.Forms.Button()
        Me.btnPSDPrelimDetDownload = New System.Windows.Forms.Button()
        Me.btnPSDHearingNoticeDownload = New System.Windows.Forms.Button()
        Me.btnPSDFinalDetDownload = New System.Windows.Forms.Button()
        Me.btnPSDFinalPermitDownload = New System.Windows.Forms.Button()
        Me.btnPSDAppSummaryDownload = New System.Windows.Forms.Button()
        Me.lblPSDFinalPermitDUPDF = New System.Windows.Forms.Label()
        Me.lblPSDFinalPermitSRPDF = New System.Windows.Forms.Label()
        Me.lblPSDFinalPermitDUDoc = New System.Windows.Forms.Label()
        Me.lblPSDFinalPermitSRDoc = New System.Windows.Forms.Label()
        Me.lblPSDFinalDetDUPDF = New System.Windows.Forms.Label()
        Me.lblPSDFinalDetSRPDF = New System.Windows.Forms.Label()
        Me.lblPSDFinalDetDUDoc = New System.Windows.Forms.Label()
        Me.lblPSDFinalDetSRDoc = New System.Windows.Forms.Label()
        Me.lblPSDHearingNoticeDUPDF = New System.Windows.Forms.Label()
        Me.lblPSDHearingNoticeSRPDF = New System.Windows.Forms.Label()
        Me.lblPSDHearingNoticeDUDoc = New System.Windows.Forms.Label()
        Me.lblPSDHearingNoticeSRDoc = New System.Windows.Forms.Label()
        Me.lblPSDPublicNoticeDUPDF = New System.Windows.Forms.Label()
        Me.lblPSDPublicNoticeSRPDF = New System.Windows.Forms.Label()
        Me.lblPSDPublicNoticeDUDoc = New System.Windows.Forms.Label()
        Me.lblPSDPublicNoticeSRDoc = New System.Windows.Forms.Label()
        Me.lblPSDDraftPermitDUPDF = New System.Windows.Forms.Label()
        Me.lblPSDDraftPermitSRPDF = New System.Windows.Forms.Label()
        Me.lblPSDDraftPermitDUDoc = New System.Windows.Forms.Label()
        Me.lblPSDDraftPermitSRDoc = New System.Windows.Forms.Label()
        Me.lblPSDPrelimDetDUPDF = New System.Windows.Forms.Label()
        Me.lblPSDPrelimDetSRPDF = New System.Windows.Forms.Label()
        Me.lblPSDPrelimDetDUDoc = New System.Windows.Forms.Label()
        Me.lblPSDPrelimDetSRDoc = New System.Windows.Forms.Label()
        Me.lblPSDAppSummaryDUPDF = New System.Windows.Forms.Label()
        Me.lblPSDAppSummarySRPDF = New System.Windows.Forms.Label()
        Me.lblPSDAppSummaryDUDoc = New System.Windows.Forms.Label()
        Me.lblPSDAppSummarySRDoc = New System.Windows.Forms.Label()
        Me.txtPSDPrelimDetPDF = New System.Windows.Forms.TextBox()
        Me.txtPSDDraftPermitPDF = New System.Windows.Forms.TextBox()
        Me.txtPSDPublicNoticePDF = New System.Windows.Forms.TextBox()
        Me.txtPSDHearingNoticePDF = New System.Windows.Forms.TextBox()
        Me.txtPSDFinalDetPDF = New System.Windows.Forms.TextBox()
        Me.txtPSDAppSummaryPDF = New System.Windows.Forms.TextBox()
        Me.txtPSDFinalPermitPDF = New System.Windows.Forms.TextBox()
        Me.txtPSDFinalPermitDoc = New System.Windows.Forms.TextBox()
        Me.chbPSDFinalPermit = New System.Windows.Forms.CheckBox()
        Me.txtPSDFinalDetDoc = New System.Windows.Forms.TextBox()
        Me.chbPSDFinalDet = New System.Windows.Forms.CheckBox()
        Me.txtPSDHearingNoticeDoc = New System.Windows.Forms.TextBox()
        Me.chbPSDHearingNotice = New System.Windows.Forms.CheckBox()
        Me.txtPSDPublicNoticeDoc = New System.Windows.Forms.TextBox()
        Me.txtPSDDraftPermitDoc = New System.Windows.Forms.TextBox()
        Me.txtPSDPrelimDetDoc = New System.Windows.Forms.TextBox()
        Me.txtPSDAppSummaryDoc = New System.Windows.Forms.TextBox()
        Me.chbPSDPublicNotice = New System.Windows.Forms.CheckBox()
        Me.chbPSDDraftPermit = New System.Windows.Forms.CheckBox()
        Me.chbPSDPrelimDet = New System.Windows.Forms.CheckBox()
        Me.chbPSDApplicationSummary = New System.Windows.Forms.CheckBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.rdbOtherPermit = New System.Windows.Forms.RadioButton()
        Me.rdbPSDPermit = New System.Windows.Forms.RadioButton()
        Me.rdbTitleVPermit = New System.Windows.Forms.RadioButton()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblPDF = New System.Windows.Forms.Label()
        Me.lblWord = New System.Windows.Forms.Label()
        Me.PanelOther = New System.Windows.Forms.Panel()
        Me.btnOtherPermitDownload = New System.Windows.Forms.Button()
        Me.btnOtherNarrativeDownload = New System.Windows.Forms.Button()
        Me.lblOtherPermitDUPDF = New System.Windows.Forms.Label()
        Me.lblOtherPermitSRPDF = New System.Windows.Forms.Label()
        Me.lblOtherPermitDUDoc = New System.Windows.Forms.Label()
        Me.lblOtherPermitSRDoc = New System.Windows.Forms.Label()
        Me.lblOtherNarrativeDUPDF = New System.Windows.Forms.Label()
        Me.lblOtherNarrativeSRPDF = New System.Windows.Forms.Label()
        Me.lblOtherNarrativeDUDoc = New System.Windows.Forms.Label()
        Me.lblOtherNarrativeSRDoc = New System.Windows.Forms.Label()
        Me.txtOtherNarrativePDF = New System.Windows.Forms.TextBox()
        Me.txtOtherPermitPDF = New System.Windows.Forms.TextBox()
        Me.txtOtherNarrativeDoc = New System.Windows.Forms.TextBox()
        Me.chbOtherNarrative = New System.Windows.Forms.CheckBox()
        Me.txtOtherPermitDoc = New System.Windows.Forms.TextBox()
        Me.chbOtherPermit = New System.Windows.Forms.CheckBox()
        Me.TPContactInformation = New System.Windows.Forms.TabPage()
        Me.txtContactPhoneNumber = New System.Windows.Forms.TextBox()
        Me.btnEmailAcknowledgmentLetter = New System.Windows.Forms.Button()
        Me.btnGetCurrentPermittingContact = New System.Windows.Forms.Button()
        Me.mtbContactZipCode = New System.Windows.Forms.MaskedTextBox()
        Me.mtbContactFaxNumber = New System.Windows.Forms.MaskedTextBox()
        Me.txtContactDescription = New System.Windows.Forms.TextBox()
        Me.txtContactEmailAddress = New System.Windows.Forms.TextBox()
        Me.txtContactState = New System.Windows.Forms.TextBox()
        Me.txtContactCity = New System.Windows.Forms.TextBox()
        Me.txtContactStreetAddress = New System.Windows.Forms.TextBox()
        Me.txtContactTitle = New System.Windows.Forms.TextBox()
        Me.txtContactCompanyName = New System.Windows.Forms.TextBox()
        Me.txtContactPedigree = New System.Windows.Forms.TextBox()
        Me.txtContactSocialTitle = New System.Windows.Forms.TextBox()
        Me.txtContactLastName = New System.Windows.Forms.TextBox()
        Me.txtContactFirstName = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TPWebPublisher = New System.Windows.Forms.TabPage()
        Me.lblPNExpires = New System.Windows.Forms.Label()
        Me.DTPPNExpires = New System.Windows.Forms.DateTimePicker()
        Me.lblEffectiveDateofPermit = New System.Windows.Forms.Label()
        Me.lblExperationDate = New System.Windows.Forms.Label()
        Me.lblEPANotifiedFinalOnWeb = New System.Windows.Forms.Label()
        Me.lblFinalOnWeb = New System.Windows.Forms.Label()
        Me.lbEPAStatesNotified = New System.Windows.Forms.Label()
        Me.lblDraftOnWeb = New System.Windows.Forms.Label()
        Me.lblNotifiedAppReceived = New System.Windows.Forms.Label()
        Me.DTPExperationDate = New System.Windows.Forms.DateTimePicker()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.DTPNotifiedAppReceived = New System.Windows.Forms.DateTimePicker()
        Me.btnSaveWebPublisher = New System.Windows.Forms.Button()
        Me.txtEPATargetedComments = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.DTPEffectiveDateofPermit = New System.Windows.Forms.DateTimePicker()
        Me.DTPEPANotifiedPermitOnWeb = New System.Windows.Forms.DateTimePicker()
        Me.DTPFinalOnWeb = New System.Windows.Forms.DateTimePicker()
        Me.DTPEPAStatesNotified = New System.Windows.Forms.DateTimePicker()
        Me.DTPDraftOnWeb = New System.Windows.Forms.DateTimePicker()
        Me.TPInformationRequests = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblInformationReceived = New System.Windows.Forms.Label()
        Me.lblInformationRequested = New System.Windows.Forms.Label()
        Me.btnDeleteInformationRequest = New System.Windows.Forms.Button()
        Me.btnClearInformationRequest = New System.Windows.Forms.Button()
        Me.txtInformationRequestedKey = New System.Windows.Forms.TextBox()
        Me.btnSaveInformationRequest = New System.Windows.Forms.Button()
        Me.txtInformationReceived = New System.Windows.Forms.TextBox()
        Me.txtInformationRequested = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.DTPInformationReceived = New System.Windows.Forms.DateTimePicker()
        Me.DTPInformationRequested = New System.Windows.Forms.DateTimePicker()
        Me.dgvInformationRequested = New System.Windows.Forms.DataGridView()
        Me.TPApplicationHistory = New System.Windows.Forms.TabPage()
        Me.dgvFacilityAppHistory = New System.Windows.Forms.DataGridView()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtApplicationCount = New System.Windows.Forms.TextBox()
        Me.txtMasterAppLock = New System.Windows.Forms.TextBox()
        Me.btnClearLinks = New System.Windows.Forms.Button()
        Me.btnLinkApplications = New System.Windows.Forms.Button()
        Me.btnAddApplicationToList = New System.Windows.Forms.Button()
        Me.lbLinkApplications = New System.Windows.Forms.ListBox()
        Me.txtMasterApp = New System.Windows.Forms.TextBox()
        Me.txtEngineerHistory = New System.Windows.Forms.TextBox()
        Me.txtApplicationDatedHistory = New System.Windows.Forms.TextBox()
        Me.txtApplicationTypeHistory = New System.Windows.Forms.TextBox()
        Me.txtApplicationUnitHistory = New System.Windows.Forms.TextBox()
        Me.txtApplicationNumberHistory = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.chbClosedOutHistory = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtHistoryComments = New System.Windows.Forms.TextBox()
        Me.txtHistoryAppComments = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.TPReviews = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblISMPReview = New System.Windows.Forms.Label()
        Me.txtISMPComments = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rdbISMPNo = New System.Windows.Forms.RadioButton()
        Me.rdbISMPYes = New System.Windows.Forms.RadioButton()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.DTPISMPReview = New System.Windows.Forms.DateTimePicker()
        Me.cboISMPStaff = New System.Windows.Forms.ComboBox()
        Me.lblISMPStaff = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblSSCPReview = New System.Windows.Forms.Label()
        Me.txtSSCPComments = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdbSSCPNo = New System.Windows.Forms.RadioButton()
        Me.rdbSSCPYes = New System.Windows.Forms.RadioButton()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.DTPSSCPReview = New System.Windows.Forms.DateTimePicker()
        Me.cboSSCPStaff = New System.Windows.Forms.ComboBox()
        Me.lblSSCPStaff = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblReviewSubmitted = New System.Windows.Forms.Label()
        Me.cboISMPUnits = New System.Windows.Forms.ComboBox()
        Me.lblISMPUnits = New System.Windows.Forms.Label()
        Me.DTPReviewSubmitted = New System.Windows.Forms.DateTimePicker()
        Me.lblSSCPUnit = New System.Windows.Forms.Label()
        Me.cboSSCPUnits = New System.Windows.Forms.ComboBox()
        Me.TPOtherInfo = New System.Windows.Forms.TabPage()
        Me.OtherInfoGroup = New System.Windows.Forms.GroupBox()
        Me.chbNspsFeeExempt = New System.Windows.Forms.CheckBox()
        Me.chbFederallyOwned = New System.Windows.Forms.CheckBox()
        Me.chbConfidential = New System.Windows.Forms.CheckBox()
        Me.GBSignificationComments = New System.Windows.Forms.GroupBox()
        Me.txtSignificantComments = New System.Windows.Forms.TextBox()
        Me.ApplicableRulesGroup = New System.Windows.Forms.GroupBox()
        Me.chbPal = New System.Windows.Forms.CheckBox()
        Me.chbRuleyy = New System.Windows.Forms.CheckBox()
        Me.chbRulett = New System.Windows.Forms.CheckBox()
        Me.chb112g = New System.Windows.Forms.CheckBox()
        Me.chbNAANSR = New System.Windows.Forms.CheckBox()
        Me.chbPSD = New System.Windows.Forms.CheckBox()
        Me.TPTrackingLog = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDated = New System.Windows.Forms.Label()
        Me.txtNAICSCode = New System.Windows.Forms.TextBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.llbPermitNumber = New System.Windows.Forms.LinkLabel()
        Me.txtDistrict = New System.Windows.Forms.TextBox()
        Me.lblPAReady = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblPNReady = New System.Windows.Forms.Label()
        Me.lblReceived = New System.Windows.Forms.Label()
        Me.GBOther = New System.Windows.Forms.GroupBox()
        Me.chbHAPsMajor = New System.Windows.Forms.CheckBox()
        Me.chbNSRMajor = New System.Windows.Forms.CheckBox()
        Me.lblPermitNumber = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.txt1HourOzone = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtPM = New System.Windows.Forms.TextBox()
        Me.txt8HROzone = New System.Windows.Forms.TextBox()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.lblPermitAction = New System.Windows.Forms.Label()
        Me.chbPNReady = New System.Windows.Forms.CheckBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.GBAirProgramCodes = New System.Windows.Forms.GroupBox()
        Me.chbCDS_RMP = New System.Windows.Forms.CheckBox()
        Me.chbCDS_0 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_6 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_7 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_8 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_9 = New System.Windows.Forms.CheckBox()
        Me.chbCDS_M = New System.Windows.Forms.CheckBox()
        Me.chbCDS_V = New System.Windows.Forms.CheckBox()
        Me.chbCDS_A = New System.Windows.Forms.CheckBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lblDateToDO = New System.Windows.Forms.Label()
        Me.lblPublicAdvisory = New System.Windows.Forms.Label()
        Me.DTPDateToDO = New System.Windows.Forms.DateTimePicker()
        Me.cboCounty = New System.Windows.Forms.ComboBox()
        Me.lblDatetoBC = New System.Windows.Forms.Label()
        Me.cboFacilityCity = New System.Windows.Forms.ComboBox()
        Me.DTPDateToBC = New System.Windows.Forms.DateTimePicker()
        Me.lblCounty = New System.Windows.Forms.Label()
        Me.lblEPAEnds = New System.Windows.Forms.Label()
        Me.DTPDateSent = New System.Windows.Forms.DateTimePicker()
        Me.DTPEPAEnds = New System.Windows.Forms.DateTimePicker()
        Me.DTPDateReceived = New System.Windows.Forms.DateTimePicker()
        Me.lblEPAWaived = New System.Windows.Forms.Label()
        Me.DTPDateAcknowledge = New System.Windows.Forms.DateTimePicker()
        Me.DTPEPAWaived = New System.Windows.Forms.DateTimePicker()
        Me.DTPDateReassigned = New System.Windows.Forms.DateTimePicker()
        Me.chbPAReady = New System.Windows.Forms.CheckBox()
        Me.DTPDateAssigned = New System.Windows.Forms.DateTimePicker()
        Me.lblDraftIssued = New System.Windows.Forms.Label()
        Me.DTPDatePAExpires = New System.Windows.Forms.DateTimePicker()
        Me.lblFinalAction = New System.Windows.Forms.Label()
        Me.DTPDatePNExpires = New System.Windows.Forms.DateTimePicker()
        Me.lblDateToPM = New System.Windows.Forms.Label()
        Me.DTPDeadline = New System.Windows.Forms.DateTimePicker()
        Me.lblDateToUC = New System.Windows.Forms.Label()
        Me.DTPDateToUC = New System.Windows.Forms.DateTimePicker()
        Me.lblDeadline = New System.Windows.Forms.Label()
        Me.DTPDateToPM = New System.Windows.Forms.DateTimePicker()
        Me.lblDatePNExpires = New System.Windows.Forms.Label()
        Me.DTPFinalAction = New System.Windows.Forms.DateTimePicker()
        Me.lblDatePAExpires = New System.Windows.Forms.Label()
        Me.DTPDraftIssued = New System.Windows.Forms.DateTimePicker()
        Me.lblDateAcknowledge = New System.Windows.Forms.Label()
        Me.txtPermitNumber = New System.Windows.Forms.TextBox()
        Me.lblDateReassigned = New System.Windows.Forms.Label()
        Me.cboPermitAction = New System.Windows.Forms.ComboBox()
        Me.lblDateAssigned = New System.Windows.Forms.Label()
        Me.cboPublicAdvisory = New System.Windows.Forms.ComboBox()
        Me.txtFacilityZipCode = New System.Windows.Forms.TextBox()
        Me.txtReasonAppSubmitted = New System.Windows.Forms.TextBox()
        Me.txtFacilityStreetAddress = New System.Windows.Forms.TextBox()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.txtSICCode = New System.Windows.Forms.TextBox()
        Me.txtPlantDescription = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.lblClassification = New System.Windows.Forms.Label()
        Me.cboOperationalStatus = New System.Windows.Forms.ComboBox()
        Me.cboClassification = New System.Windows.Forms.ComboBox()
        Me.lblOperationalStatus = New System.Windows.Forms.Label()
        Me.TCApplicationTrackingLog = New System.Windows.Forms.TabControl()
        Me.TPFees = New System.Windows.Forms.TabPage()
        Me.pnlFeeDataFinalized = New System.Windows.Forms.Panel()
        Me.lblFeeDataFinalized = New System.Windows.Forms.Label()
        Me.lklGenerateEmail = New System.Windows.Forms.LinkLabel()
        Me.dtpFacilityFeeNotified = New System.Windows.Forms.DateTimePicker()
        Me.lblFacilityFeeNotified = New System.Windows.Forms.Label()
        Me.dtpFeeDataFinalized = New System.Windows.Forms.DateTimePicker()
        Me.lblFeeChangesNotAllowedWarning = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblInvoices = New System.Windows.Forms.Label()
        Me.txtFeeTotalInvoiced = New Iaip.CurrencyTextBox()
        Me.lblFeeTotalInvoiced = New System.Windows.Forms.Label()
        Me.dgvApplicationInvoices = New Iaip.IaipDataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblPayments = New System.Windows.Forms.Label()
        Me.txtFeeTotalPaid = New Iaip.CurrencyTextBox()
        Me.lblFeeTotalPaid = New System.Windows.Forms.Label()
        Me.dgvApplicationPayments = New Iaip.IaipDataGridView()
        Me.txtAppFeeAmount = New Iaip.CurrencyTextBox()
        Me.txtExpFeeAmount = New Iaip.CurrencyTextBox()
        Me.chbAppFee = New System.Windows.Forms.CheckBox()
        Me.chbExpFee = New System.Windows.Forms.CheckBox()
        Me.txtExpFeeOverrideReason = New System.Windows.Forms.TextBox()
        Me.txtAppFeeOverrideReason = New System.Windows.Forms.TextBox()
        Me.chbExpFeeOverride = New System.Windows.Forms.CheckBox()
        Me.chbFeeDataFinalized = New System.Windows.Forms.CheckBox()
        Me.chbAppFeeOverride = New System.Windows.Forms.CheckBox()
        Me.txtFeeTotal = New System.Windows.Forms.TextBox()
        Me.lblExpFee = New System.Windows.Forms.Label()
        Me.lblAppFee = New System.Windows.Forms.Label()
        Me.lblTotalFee = New System.Windows.Forms.Label()
        Me.cmbExpFeeType = New System.Windows.Forms.ComboBox()
        Me.cmbAppFeeType = New System.Windows.Forms.ComboBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.SaveButton = New System.Windows.Forms.ToolStripButton()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.pnlAssignments = New System.Windows.Forms.Panel()
        Me.lklOpenAppOnline = New System.Windows.Forms.LinkLabel()
        Me.txtNewApplicationNumber = New System.Windows.Forms.TextBox()
        Me.btnFetchNewAppNumber = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TPSubPartEditor.SuspendLayout()
        Me.TCSupParts.SuspendLayout()
        Me.TPSIP.SuspendLayout()
        Me.Panel13.SuspendLayout()
        CType(Me.dgvSIPSubParts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel17.SuspendLayout()
        Me.Panel15.SuspendLayout()
        CType(Me.dgvSIPSubpartAddEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel14.SuspendLayout()
        CType(Me.dgvSIPSubPartDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel16.SuspendLayout()
        Me.TPPart60.SuspendLayout()
        Me.Panel18.SuspendLayout()
        CType(Me.dgvNSPSSubParts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel19.SuspendLayout()
        Me.Panel20.SuspendLayout()
        CType(Me.dgvNSPSSubpartAddEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel21.SuspendLayout()
        CType(Me.dgvNSPSSubPartDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel22.SuspendLayout()
        Me.TPPart61.SuspendLayout()
        Me.Panel23.SuspendLayout()
        CType(Me.dgvNESHAPSubParts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel24.SuspendLayout()
        Me.Panel25.SuspendLayout()
        CType(Me.dgvNESHAPSubpartAddEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel26.SuspendLayout()
        CType(Me.dgvNESHAPSubPartDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel27.SuspendLayout()
        Me.TPPart63.SuspendLayout()
        Me.Panel28.SuspendLayout()
        CType(Me.dgvMACTSubParts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel29.SuspendLayout()
        Me.Panel30.SuspendLayout()
        CType(Me.dgvMACTSubpartAddEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel31.SuspendLayout()
        CType(Me.dgvMACTSubPartDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel32.SuspendLayout()
        Me.TPDocuments.SuspendLayout()
        Me.PanelTitleV.SuspendLayout()
        Me.PanelPSD.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.PanelOther.SuspendLayout()
        Me.TPContactInformation.SuspendLayout()
        Me.TPWebPublisher.SuspendLayout()
        Me.TPInformationRequests.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvInformationRequested, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPApplicationHistory.SuspendLayout()
        CType(Me.dgvFacilityAppHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.TPReviews.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TPOtherInfo.SuspendLayout()
        Me.OtherInfoGroup.SuspendLayout()
        Me.GBSignificationComments.SuspendLayout()
        Me.ApplicableRulesGroup.SuspendLayout()
        Me.TPTrackingLog.SuspendLayout()
        Me.GBOther.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GBAirProgramCodes.SuspendLayout()
        Me.TCApplicationTrackingLog.SuspendLayout()
        Me.TPFees.SuspendLayout()
        Me.pnlFeeDataFinalized.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvApplicationInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvApplicationPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        Me.pnlAssignments.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRefreshAIRSNo
        '
        Me.btnRefreshAIRSNo.AutoSize = True
        Me.btnRefreshAIRSNo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshAIRSNo.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnRefreshAIRSNo.Location = New System.Drawing.Point(177, 30)
        Me.btnRefreshAIRSNo.Name = "btnRefreshAIRSNo"
        Me.btnRefreshAIRSNo.Size = New System.Drawing.Size(22, 22)
        Me.btnRefreshAIRSNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnRefreshAIRSNo, "Reload facility information")
        Me.btnRefreshAIRSNo.UseVisualStyleBackColor = True
        Me.btnRefreshAIRSNo.Visible = False
        '
        'lblLinkWarning
        '
        Me.lblLinkWarning.AutoSize = True
        Me.lblLinkWarning.ForeColor = System.Drawing.Color.Tomato
        Me.lblLinkWarning.Location = New System.Drawing.Point(12, 87)
        Me.lblLinkWarning.Name = "lblLinkWarning"
        Me.lblLinkWarning.Size = New System.Drawing.Size(94, 13)
        Me.lblLinkWarning.TabIndex = 334
        Me.lblLinkWarning.Text = "Application Linked"
        Me.lblLinkWarning.Visible = False
        '
        'rtbFacilityInformation
        '
        Me.rtbFacilityInformation.Location = New System.Drawing.Point(239, 22)
        Me.rtbFacilityInformation.Name = "rtbFacilityInformation"
        Me.rtbFacilityInformation.ReadOnly = True
        Me.rtbFacilityInformation.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbFacilityInformation.Size = New System.Drawing.Size(301, 78)
        Me.rtbFacilityInformation.TabIndex = 4
        Me.rtbFacilityInformation.Text = ""
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.AirsNumber = Nothing
        Me.txtAIRSNumber.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.txtAIRSNumber.BackColor = System.Drawing.Color.Transparent
        Me.txtAIRSNumber.ErrorMessageLabel = Nothing
        Me.txtAIRSNumber.FacilityMustExist = True
        Me.txtAIRSNumber.InvalidFormatMessage = "Invalid AIRS #."
        Me.txtAIRSNumber.Location = New System.Drawing.Point(92, 31)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(79, 20)
        Me.txtAIRSNumber.TabIndex = 0
        Me.txtAIRSNumber.TextBoxBackColor = System.Drawing.SystemColors.Window
        '
        'lblAppNumber
        '
        Me.lblAppNumber.AutoSize = True
        Me.lblAppNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppNumber.Location = New System.Drawing.Point(12, 4)
        Me.lblAppNumber.Name = "lblAppNumber"
        Me.lblAppNumber.Size = New System.Drawing.Size(128, 17)
        Me.lblAppNumber.TabIndex = 249
        Me.lblAppNumber.Text = "New Application:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 250
        Me.Label2.Text = "AIRS Number"
        '
        'lblApplicationUnit
        '
        Me.lblApplicationUnit.AutoSize = True
        Me.lblApplicationUnit.Location = New System.Drawing.Point(10, 33)
        Me.lblApplicationUnit.Name = "lblApplicationUnit"
        Me.lblApplicationUnit.Size = New System.Drawing.Size(57, 13)
        Me.lblApplicationUnit.TabIndex = 253
        Me.lblApplicationUnit.Text = "SSPP Unit"
        '
        'lblEngineer
        '
        Me.lblEngineer.AutoSize = True
        Me.lblEngineer.Location = New System.Drawing.Point(10, 6)
        Me.lblEngineer.Name = "lblEngineer"
        Me.lblEngineer.Size = New System.Drawing.Size(49, 13)
        Me.lblEngineer.TabIndex = 252
        Me.lblEngineer.Text = "Engineer"
        '
        'cboApplicationUnit
        '
        Me.cboApplicationUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboApplicationUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboApplicationUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cboApplicationUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboApplicationUnit.Enabled = False
        Me.cboApplicationUnit.Location = New System.Drawing.Point(73, 30)
        Me.cboApplicationUnit.Name = "cboApplicationUnit"
        Me.cboApplicationUnit.Size = New System.Drawing.Size(160, 21)
        Me.cboApplicationUnit.TabIndex = 6
        '
        'cboEngineer
        '
        Me.cboEngineer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEngineer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEngineer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEngineer.Enabled = False
        Me.cboEngineer.Location = New System.Drawing.Point(73, 3)
        Me.cboEngineer.Name = "cboEngineer"
        Me.cboEngineer.Size = New System.Drawing.Size(160, 21)
        Me.cboEngineer.TabIndex = 5
        '
        'lblApplicationType
        '
        Me.lblApplicationType.AutoSize = True
        Me.lblApplicationType.Location = New System.Drawing.Point(10, 60)
        Me.lblApplicationType.Name = "lblApplicationType"
        Me.lblApplicationType.Size = New System.Drawing.Size(50, 13)
        Me.lblApplicationType.TabIndex = 251
        Me.lblApplicationType.Text = "APL type"
        '
        'cboApplicationType
        '
        Me.cboApplicationType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboApplicationType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboApplicationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboApplicationType.Enabled = False
        Me.cboApplicationType.Location = New System.Drawing.Point(73, 57)
        Me.cboApplicationType.Name = "cboApplicationType"
        Me.cboApplicationType.Size = New System.Drawing.Size(160, 21)
        Me.cboApplicationType.TabIndex = 7
        '
        'chbClosedOut
        '
        Me.chbClosedOut.Enabled = False
        Me.chbClosedOut.Location = New System.Drawing.Point(139, 59)
        Me.chbClosedOut.Name = "chbClosedOut"
        Me.chbClosedOut.Size = New System.Drawing.Size(80, 16)
        Me.chbClosedOut.TabIndex = 3
        Me.chbClosedOut.Text = "Closed Out"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 60)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 13)
        Me.Label10.TabIndex = 258
        Me.Label10.Text = "Outstanding Apps"
        '
        'txtOutstandingApplication
        '
        Me.txtOutstandingApplication.Location = New System.Drawing.Point(109, 57)
        Me.txtOutstandingApplication.Name = "txtOutstandingApplication"
        Me.txtOutstandingApplication.ReadOnly = True
        Me.txtOutstandingApplication.Size = New System.Drawing.Size(24, 20)
        Me.txtOutstandingApplication.TabIndex = 2
        '
        'TPSubPartEditor
        '
        Me.TPSubPartEditor.Controls.Add(Me.TCSupParts)
        Me.TPSubPartEditor.Location = New System.Drawing.Point(4, 22)
        Me.TPSubPartEditor.Name = "TPSubPartEditor"
        Me.TPSubPartEditor.Size = New System.Drawing.Size(784, 477)
        Me.TPSubPartEditor.TabIndex = 8
        Me.TPSubPartEditor.Text = "Rule Applicability"
        Me.TPSubPartEditor.UseVisualStyleBackColor = True
        '
        'TCSupParts
        '
        Me.TCSupParts.Controls.Add(Me.TPSIP)
        Me.TCSupParts.Controls.Add(Me.TPPart60)
        Me.TCSupParts.Controls.Add(Me.TPPart61)
        Me.TCSupParts.Controls.Add(Me.TPPart63)
        Me.TCSupParts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCSupParts.Location = New System.Drawing.Point(0, 0)
        Me.TCSupParts.Name = "TCSupParts"
        Me.TCSupParts.SelectedIndex = 0
        Me.TCSupParts.Size = New System.Drawing.Size(784, 477)
        Me.TCSupParts.TabIndex = 5
        '
        'TPSIP
        '
        Me.TPSIP.Controls.Add(Me.Panel13)
        Me.TPSIP.Location = New System.Drawing.Point(4, 22)
        Me.TPSIP.Name = "TPSIP"
        Me.TPSIP.Size = New System.Drawing.Size(776, 451)
        Me.TPSIP.TabIndex = 4
        Me.TPSIP.Text = "SIP"
        Me.TPSIP.UseVisualStyleBackColor = True
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.dgvSIPSubParts)
        Me.Panel13.Controls.Add(Me.Panel17)
        Me.Panel13.Controls.Add(Me.Panel15)
        Me.Panel13.Controls.Add(Me.Panel14)
        Me.Panel13.Controls.Add(Me.Panel16)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(776, 451)
        Me.Panel13.TabIndex = 5
        '
        'dgvSIPSubParts
        '
        Me.dgvSIPSubParts.AllowUserToOrderColumns = True
        Me.dgvSIPSubParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSIPSubParts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSIPSubParts.Location = New System.Drawing.Point(231, 34)
        Me.dgvSIPSubParts.Name = "dgvSIPSubParts"
        Me.dgvSIPSubParts.ReadOnly = True
        Me.dgvSIPSubParts.Size = New System.Drawing.Size(301, 293)
        Me.dgvSIPSubParts.TabIndex = 2
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.btnSaveSIPSubpart)
        Me.Panel17.Controls.Add(Me.Label60)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(231, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(301, 34)
        Me.Panel17.TabIndex = 1
        '
        'btnSaveSIPSubpart
        '
        Me.btnSaveSIPSubpart.AutoSize = True
        Me.btnSaveSIPSubpart.BackColor = System.Drawing.Color.Tomato
        Me.btnSaveSIPSubpart.Location = New System.Drawing.Point(174, 10)
        Me.btnSaveSIPSubpart.Name = "btnSaveSIPSubpart"
        Me.btnSaveSIPSubpart.Size = New System.Drawing.Size(115, 23)
        Me.btnSaveSIPSubpart.TabIndex = 0
        Me.btnSaveSIPSubpart.Text = "Save SIP Data"
        Me.btnSaveSIPSubpart.UseVisualStyleBackColor = False
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(3, 15)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(79, 13)
        Me.Label60.TabIndex = 432
        Me.Label60.Text = "Current Facility:"
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.Label59)
        Me.Panel15.Controls.Add(Me.dgvSIPSubpartAddEdit)
        Me.Panel15.Controls.Add(Me.btnSIPEditAll)
        Me.Panel15.Controls.Add(Me.btnSIPUneditAll)
        Me.Panel15.Controls.Add(Me.btnClearAddModifiedSIPs)
        Me.Panel15.Controls.Add(Me.btnSIPUnedit)
        Me.Panel15.Controls.Add(Me.btnSIPEdit)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel15.Location = New System.Drawing.Point(532, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(244, 327)
        Me.Panel15.TabIndex = 3
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(39, 15)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(155, 13)
        Me.Label59.TabIndex = 431
        Me.Label59.Text = "Added/Modified by Application:"
        '
        'dgvSIPSubpartAddEdit
        '
        Me.dgvSIPSubpartAddEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSIPSubpartAddEdit.Location = New System.Drawing.Point(42, 34)
        Me.dgvSIPSubpartAddEdit.Name = "dgvSIPSubpartAddEdit"
        Me.dgvSIPSubpartAddEdit.ReadOnly = True
        Me.dgvSIPSubpartAddEdit.Size = New System.Drawing.Size(199, 266)
        Me.dgvSIPSubpartAddEdit.TabIndex = 0
        '
        'btnSIPEditAll
        '
        Me.btnSIPEditAll.AutoSize = True
        Me.btnSIPEditAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSIPEditAll.Image = Global.Iaip.My.Resources.Resources.MoveAllRightIcon
        Me.btnSIPEditAll.Location = New System.Drawing.Point(6, 148)
        Me.btnSIPEditAll.Name = "btnSIPEditAll"
        Me.btnSIPEditAll.Size = New System.Drawing.Size(30, 28)
        Me.btnSIPEditAll.TabIndex = 3
        Me.btnSIPEditAll.UseVisualStyleBackColor = True
        '
        'btnSIPUneditAll
        '
        Me.btnSIPUneditAll.AutoSize = True
        Me.btnSIPUneditAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSIPUneditAll.Image = Global.Iaip.My.Resources.Resources.MoveAllLeftIcon
        Me.btnSIPUneditAll.Location = New System.Drawing.Point(6, 184)
        Me.btnSIPUneditAll.Name = "btnSIPUneditAll"
        Me.btnSIPUneditAll.Size = New System.Drawing.Size(30, 28)
        Me.btnSIPUneditAll.TabIndex = 4
        Me.btnSIPUneditAll.UseVisualStyleBackColor = True
        '
        'btnClearAddModifiedSIPs
        '
        Me.btnClearAddModifiedSIPs.AutoSize = True
        Me.btnClearAddModifiedSIPs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAddModifiedSIPs.Location = New System.Drawing.Point(52, 307)
        Me.btnClearAddModifiedSIPs.Name = "btnClearAddModifiedSIPs"
        Me.btnClearAddModifiedSIPs.Size = New System.Drawing.Size(86, 23)
        Me.btnClearAddModifiedSIPs.TabIndex = 431
        Me.btnClearAddModifiedSIPs.Text = "Clear Selected"
        Me.btnClearAddModifiedSIPs.UseVisualStyleBackColor = True
        '
        'btnSIPUnedit
        '
        Me.btnSIPUnedit.AutoSize = True
        Me.btnSIPUnedit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSIPUnedit.Image = Global.Iaip.My.Resources.Resources.MoveLeftIcon
        Me.btnSIPUnedit.Location = New System.Drawing.Point(6, 92)
        Me.btnSIPUnedit.Name = "btnSIPUnedit"
        Me.btnSIPUnedit.Size = New System.Drawing.Size(30, 28)
        Me.btnSIPUnedit.TabIndex = 2
        Me.btnSIPUnedit.UseVisualStyleBackColor = True
        '
        'btnSIPEdit
        '
        Me.btnSIPEdit.AutoSize = True
        Me.btnSIPEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSIPEdit.Image = Global.Iaip.My.Resources.Resources.MoveRightIcon
        Me.btnSIPEdit.Location = New System.Drawing.Point(6, 48)
        Me.btnSIPEdit.Name = "btnSIPEdit"
        Me.btnSIPEdit.Size = New System.Drawing.Size(30, 28)
        Me.btnSIPEdit.TabIndex = 1
        Me.btnSIPEdit.UseVisualStyleBackColor = True
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Label58)
        Me.Panel14.Controls.Add(Me.dgvSIPSubPartDelete)
        Me.Panel14.Controls.Add(Me.btnClearSIPDeletes)
        Me.Panel14.Controls.Add(Me.btnSIPDelete)
        Me.Panel14.Controls.Add(Me.btnSIPUndeleteAll)
        Me.Panel14.Controls.Add(Me.btnSIPDeleteAll)
        Me.Panel14.Controls.Add(Me.btnSIPUndelete)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(231, 327)
        Me.Panel14.TabIndex = 0
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(7, 15)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(125, 13)
        Me.Label58.TabIndex = 430
        Me.Label58.Text = "Removed by Application:"
        '
        'dgvSIPSubPartDelete
        '
        Me.dgvSIPSubPartDelete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSIPSubPartDelete.Location = New System.Drawing.Point(4, 34)
        Me.dgvSIPSubPartDelete.Name = "dgvSIPSubPartDelete"
        Me.dgvSIPSubPartDelete.ReadOnly = True
        Me.dgvSIPSubPartDelete.Size = New System.Drawing.Size(180, 266)
        Me.dgvSIPSubPartDelete.TabIndex = 0
        '
        'btnClearSIPDeletes
        '
        Me.btnClearSIPDeletes.AutoSize = True
        Me.btnClearSIPDeletes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearSIPDeletes.Location = New System.Drawing.Point(12, 304)
        Me.btnClearSIPDeletes.Name = "btnClearSIPDeletes"
        Me.btnClearSIPDeletes.Size = New System.Drawing.Size(86, 23)
        Me.btnClearSIPDeletes.TabIndex = 430
        Me.btnClearSIPDeletes.Text = "Clear Selected"
        Me.btnClearSIPDeletes.UseVisualStyleBackColor = True
        '
        'btnSIPDelete
        '
        Me.btnSIPDelete.AutoSize = True
        Me.btnSIPDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSIPDelete.Image = Global.Iaip.My.Resources.Resources.MoveLeftIcon
        Me.btnSIPDelete.Location = New System.Drawing.Point(190, 46)
        Me.btnSIPDelete.Name = "btnSIPDelete"
        Me.btnSIPDelete.Size = New System.Drawing.Size(30, 28)
        Me.btnSIPDelete.TabIndex = 1
        Me.btnSIPDelete.UseVisualStyleBackColor = True
        '
        'btnSIPUndeleteAll
        '
        Me.btnSIPUndeleteAll.AutoSize = True
        Me.btnSIPUndeleteAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSIPUndeleteAll.Image = Global.Iaip.My.Resources.Resources.MoveAllRightIcon
        Me.btnSIPUndeleteAll.Location = New System.Drawing.Point(190, 182)
        Me.btnSIPUndeleteAll.Name = "btnSIPUndeleteAll"
        Me.btnSIPUndeleteAll.Size = New System.Drawing.Size(30, 28)
        Me.btnSIPUndeleteAll.TabIndex = 4
        Me.btnSIPUndeleteAll.UseVisualStyleBackColor = True
        '
        'btnSIPDeleteAll
        '
        Me.btnSIPDeleteAll.AutoSize = True
        Me.btnSIPDeleteAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSIPDeleteAll.Image = Global.Iaip.My.Resources.Resources.MoveAllLeftIcon
        Me.btnSIPDeleteAll.Location = New System.Drawing.Point(190, 146)
        Me.btnSIPDeleteAll.Name = "btnSIPDeleteAll"
        Me.btnSIPDeleteAll.Size = New System.Drawing.Size(30, 28)
        Me.btnSIPDeleteAll.TabIndex = 3
        Me.btnSIPDeleteAll.UseVisualStyleBackColor = True
        '
        'btnSIPUndelete
        '
        Me.btnSIPUndelete.AutoSize = True
        Me.btnSIPUndelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSIPUndelete.Image = Global.Iaip.My.Resources.Resources.MoveRightIcon
        Me.btnSIPUndelete.Location = New System.Drawing.Point(190, 90)
        Me.btnSIPUndelete.Name = "btnSIPUndelete"
        Me.btnSIPUndelete.Size = New System.Drawing.Size(30, 28)
        Me.btnSIPUndelete.TabIndex = 2
        Me.btnSIPUndelete.UseVisualStyleBackColor = True
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.btnAddNewSIPSubpart)
        Me.Panel16.Controls.Add(Me.cboSIPSubpart)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel16.Location = New System.Drawing.Point(0, 327)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(776, 124)
        Me.Panel16.TabIndex = 4
        '
        'btnAddNewSIPSubpart
        '
        Me.btnAddNewSIPSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNewSIPSubpart.AutoSize = True
        Me.btnAddNewSIPSubpart.Location = New System.Drawing.Point(610, 12)
        Me.btnAddNewSIPSubpart.Name = "btnAddNewSIPSubpart"
        Me.btnAddNewSIPSubpart.Size = New System.Drawing.Size(141, 23)
        Me.btnAddNewSIPSubpart.TabIndex = 1
        Me.btnAddNewSIPSubpart.Text = "Add Subpart to Above List"
        Me.btnAddNewSIPSubpart.UseVisualStyleBackColor = True
        '
        'cboSIPSubpart
        '
        Me.cboSIPSubpart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSIPSubpart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSIPSubpart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSIPSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSIPSubpart.FormattingEnabled = True
        Me.cboSIPSubpart.Location = New System.Drawing.Point(6, 12)
        Me.cboSIPSubpart.Name = "cboSIPSubpart"
        Me.cboSIPSubpart.Size = New System.Drawing.Size(598, 21)
        Me.cboSIPSubpart.TabIndex = 0
        '
        'TPPart60
        '
        Me.TPPart60.Controls.Add(Me.Panel18)
        Me.TPPart60.Location = New System.Drawing.Point(4, 22)
        Me.TPPart60.Name = "TPPart60"
        Me.TPPart60.Size = New System.Drawing.Size(776, 451)
        Me.TPPart60.TabIndex = 0
        Me.TPPart60.Text = "NSPS (Part 60)"
        Me.TPPart60.UseVisualStyleBackColor = True
        '
        'Panel18
        '
        Me.Panel18.Controls.Add(Me.dgvNSPSSubParts)
        Me.Panel18.Controls.Add(Me.Panel19)
        Me.Panel18.Controls.Add(Me.Panel20)
        Me.Panel18.Controls.Add(Me.Panel21)
        Me.Panel18.Controls.Add(Me.Panel22)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Location = New System.Drawing.Point(0, 0)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(776, 451)
        Me.Panel18.TabIndex = 5
        '
        'dgvNSPSSubParts
        '
        Me.dgvNSPSSubParts.AllowUserToOrderColumns = True
        Me.dgvNSPSSubParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNSPSSubParts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvNSPSSubParts.Location = New System.Drawing.Point(231, 34)
        Me.dgvNSPSSubParts.Name = "dgvNSPSSubParts"
        Me.dgvNSPSSubParts.ReadOnly = True
        Me.dgvNSPSSubParts.Size = New System.Drawing.Size(301, 293)
        Me.dgvNSPSSubParts.TabIndex = 2
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.btnSaveNSPSSubpart)
        Me.Panel19.Controls.Add(Me.Label61)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(231, 0)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(301, 34)
        Me.Panel19.TabIndex = 1
        '
        'btnSaveNSPSSubpart
        '
        Me.btnSaveNSPSSubpart.AutoSize = True
        Me.btnSaveNSPSSubpart.BackColor = System.Drawing.Color.Tomato
        Me.btnSaveNSPSSubpart.Location = New System.Drawing.Point(174, 10)
        Me.btnSaveNSPSSubpart.Name = "btnSaveNSPSSubpart"
        Me.btnSaveNSPSSubpart.Size = New System.Drawing.Size(115, 23)
        Me.btnSaveNSPSSubpart.TabIndex = 0
        Me.btnSaveNSPSSubpart.Text = "Save NSPS Data"
        Me.btnSaveNSPSSubpart.UseVisualStyleBackColor = False
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(3, 15)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(79, 13)
        Me.Label61.TabIndex = 432
        Me.Label61.Text = "Current Facility:"
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.Label62)
        Me.Panel20.Controls.Add(Me.dgvNSPSSubpartAddEdit)
        Me.Panel20.Controls.Add(Me.btnNSPSEditAll)
        Me.Panel20.Controls.Add(Me.btnNSPSUneditAll)
        Me.Panel20.Controls.Add(Me.btnClearAddModifiedNSPSs)
        Me.Panel20.Controls.Add(Me.btnNSPSUnedit)
        Me.Panel20.Controls.Add(Me.btnNSPSEdit)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel20.Location = New System.Drawing.Point(532, 0)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(244, 327)
        Me.Panel20.TabIndex = 0
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(39, 15)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(155, 13)
        Me.Label62.TabIndex = 431
        Me.Label62.Text = "Added/Modified by Application:"
        '
        'dgvNSPSSubpartAddEdit
        '
        Me.dgvNSPSSubpartAddEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNSPSSubpartAddEdit.Location = New System.Drawing.Point(42, 34)
        Me.dgvNSPSSubpartAddEdit.Name = "dgvNSPSSubpartAddEdit"
        Me.dgvNSPSSubpartAddEdit.ReadOnly = True
        Me.dgvNSPSSubpartAddEdit.Size = New System.Drawing.Size(190, 266)
        Me.dgvNSPSSubpartAddEdit.TabIndex = 0
        '
        'btnNSPSEditAll
        '
        Me.btnNSPSEditAll.AutoSize = True
        Me.btnNSPSEditAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNSPSEditAll.Image = Global.Iaip.My.Resources.Resources.MoveAllRightIcon
        Me.btnNSPSEditAll.Location = New System.Drawing.Point(6, 148)
        Me.btnNSPSEditAll.Name = "btnNSPSEditAll"
        Me.btnNSPSEditAll.Size = New System.Drawing.Size(30, 28)
        Me.btnNSPSEditAll.TabIndex = 3
        Me.btnNSPSEditAll.UseVisualStyleBackColor = True
        '
        'btnNSPSUneditAll
        '
        Me.btnNSPSUneditAll.AutoSize = True
        Me.btnNSPSUneditAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNSPSUneditAll.Image = Global.Iaip.My.Resources.Resources.MoveAllLeftIcon
        Me.btnNSPSUneditAll.Location = New System.Drawing.Point(6, 184)
        Me.btnNSPSUneditAll.Name = "btnNSPSUneditAll"
        Me.btnNSPSUneditAll.Size = New System.Drawing.Size(30, 28)
        Me.btnNSPSUneditAll.TabIndex = 4
        Me.btnNSPSUneditAll.UseVisualStyleBackColor = True
        '
        'btnClearAddModifiedNSPSs
        '
        Me.btnClearAddModifiedNSPSs.AutoSize = True
        Me.btnClearAddModifiedNSPSs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAddModifiedNSPSs.Location = New System.Drawing.Point(52, 307)
        Me.btnClearAddModifiedNSPSs.Name = "btnClearAddModifiedNSPSs"
        Me.btnClearAddModifiedNSPSs.Size = New System.Drawing.Size(86, 23)
        Me.btnClearAddModifiedNSPSs.TabIndex = 5
        Me.btnClearAddModifiedNSPSs.Text = "Clear Selected"
        Me.btnClearAddModifiedNSPSs.UseVisualStyleBackColor = True
        '
        'btnNSPSUnedit
        '
        Me.btnNSPSUnedit.AutoSize = True
        Me.btnNSPSUnedit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNSPSUnedit.Image = Global.Iaip.My.Resources.Resources.MoveLeftIcon
        Me.btnNSPSUnedit.Location = New System.Drawing.Point(6, 92)
        Me.btnNSPSUnedit.Name = "btnNSPSUnedit"
        Me.btnNSPSUnedit.Size = New System.Drawing.Size(30, 28)
        Me.btnNSPSUnedit.TabIndex = 2
        Me.btnNSPSUnedit.UseVisualStyleBackColor = True
        '
        'btnNSPSEdit
        '
        Me.btnNSPSEdit.AutoSize = True
        Me.btnNSPSEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNSPSEdit.Image = Global.Iaip.My.Resources.Resources.MoveRightIcon
        Me.btnNSPSEdit.Location = New System.Drawing.Point(6, 48)
        Me.btnNSPSEdit.Name = "btnNSPSEdit"
        Me.btnNSPSEdit.Size = New System.Drawing.Size(30, 28)
        Me.btnNSPSEdit.TabIndex = 1
        Me.btnNSPSEdit.UseVisualStyleBackColor = True
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.Label64)
        Me.Panel21.Controls.Add(Me.dgvNSPSSubPartDelete)
        Me.Panel21.Controls.Add(Me.btnClearNSPSDeletes)
        Me.Panel21.Controls.Add(Me.btnNSPSDelete)
        Me.Panel21.Controls.Add(Me.btnNSPSUndeleteAll)
        Me.Panel21.Controls.Add(Me.btnNSPSDeleteAll)
        Me.Panel21.Controls.Add(Me.btnNSPSUndelete)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel21.Location = New System.Drawing.Point(0, 0)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(231, 327)
        Me.Panel21.TabIndex = 0
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(7, 15)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(125, 13)
        Me.Label64.TabIndex = 430
        Me.Label64.Text = "Removed by Application:"
        '
        'dgvNSPSSubPartDelete
        '
        Me.dgvNSPSSubPartDelete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNSPSSubPartDelete.Location = New System.Drawing.Point(4, 34)
        Me.dgvNSPSSubPartDelete.Name = "dgvNSPSSubPartDelete"
        Me.dgvNSPSSubPartDelete.ReadOnly = True
        Me.dgvNSPSSubPartDelete.Size = New System.Drawing.Size(180, 266)
        Me.dgvNSPSSubPartDelete.TabIndex = 0
        '
        'btnClearNSPSDeletes
        '
        Me.btnClearNSPSDeletes.AutoSize = True
        Me.btnClearNSPSDeletes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearNSPSDeletes.Location = New System.Drawing.Point(12, 304)
        Me.btnClearNSPSDeletes.Name = "btnClearNSPSDeletes"
        Me.btnClearNSPSDeletes.Size = New System.Drawing.Size(86, 23)
        Me.btnClearNSPSDeletes.TabIndex = 430
        Me.btnClearNSPSDeletes.Text = "Clear Selected"
        Me.btnClearNSPSDeletes.UseVisualStyleBackColor = True
        '
        'btnNSPSDelete
        '
        Me.btnNSPSDelete.AutoSize = True
        Me.btnNSPSDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNSPSDelete.Image = Global.Iaip.My.Resources.Resources.MoveLeftIcon
        Me.btnNSPSDelete.Location = New System.Drawing.Point(190, 46)
        Me.btnNSPSDelete.Name = "btnNSPSDelete"
        Me.btnNSPSDelete.Size = New System.Drawing.Size(30, 28)
        Me.btnNSPSDelete.TabIndex = 1
        Me.btnNSPSDelete.UseVisualStyleBackColor = True
        '
        'btnNSPSUndeleteAll
        '
        Me.btnNSPSUndeleteAll.AutoSize = True
        Me.btnNSPSUndeleteAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNSPSUndeleteAll.Image = Global.Iaip.My.Resources.Resources.MoveAllRightIcon
        Me.btnNSPSUndeleteAll.Location = New System.Drawing.Point(190, 182)
        Me.btnNSPSUndeleteAll.Name = "btnNSPSUndeleteAll"
        Me.btnNSPSUndeleteAll.Size = New System.Drawing.Size(30, 28)
        Me.btnNSPSUndeleteAll.TabIndex = 3
        Me.btnNSPSUndeleteAll.UseVisualStyleBackColor = True
        '
        'btnNSPSDeleteAll
        '
        Me.btnNSPSDeleteAll.AutoSize = True
        Me.btnNSPSDeleteAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNSPSDeleteAll.Image = Global.Iaip.My.Resources.Resources.MoveAllLeftIcon
        Me.btnNSPSDeleteAll.Location = New System.Drawing.Point(190, 146)
        Me.btnNSPSDeleteAll.Name = "btnNSPSDeleteAll"
        Me.btnNSPSDeleteAll.Size = New System.Drawing.Size(30, 28)
        Me.btnNSPSDeleteAll.TabIndex = 2
        Me.btnNSPSDeleteAll.UseVisualStyleBackColor = True
        '
        'btnNSPSUndelete
        '
        Me.btnNSPSUndelete.AutoSize = True
        Me.btnNSPSUndelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNSPSUndelete.Image = Global.Iaip.My.Resources.Resources.MoveRightIcon
        Me.btnNSPSUndelete.Location = New System.Drawing.Point(190, 90)
        Me.btnNSPSUndelete.Name = "btnNSPSUndelete"
        Me.btnNSPSUndelete.Size = New System.Drawing.Size(30, 28)
        Me.btnNSPSUndelete.TabIndex = 418
        Me.btnNSPSUndelete.UseVisualStyleBackColor = True
        '
        'Panel22
        '
        Me.Panel22.Controls.Add(Me.btnAddNewNSPSSubpart)
        Me.Panel22.Controls.Add(Me.cboNSPSSubpart)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel22.Location = New System.Drawing.Point(0, 327)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(776, 124)
        Me.Panel22.TabIndex = 1
        '
        'btnAddNewNSPSSubpart
        '
        Me.btnAddNewNSPSSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNewNSPSSubpart.AutoSize = True
        Me.btnAddNewNSPSSubpart.Location = New System.Drawing.Point(610, 12)
        Me.btnAddNewNSPSSubpart.Name = "btnAddNewNSPSSubpart"
        Me.btnAddNewNSPSSubpart.Size = New System.Drawing.Size(141, 23)
        Me.btnAddNewNSPSSubpart.TabIndex = 1
        Me.btnAddNewNSPSSubpart.Text = "Add Subpart to Above List"
        Me.btnAddNewNSPSSubpart.UseVisualStyleBackColor = True
        '
        'cboNSPSSubpart
        '
        Me.cboNSPSSubpart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboNSPSSubpart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNSPSSubpart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNSPSSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNSPSSubpart.FormattingEnabled = True
        Me.cboNSPSSubpart.Location = New System.Drawing.Point(6, 12)
        Me.cboNSPSSubpart.Name = "cboNSPSSubpart"
        Me.cboNSPSSubpart.Size = New System.Drawing.Size(598, 21)
        Me.cboNSPSSubpart.TabIndex = 0
        '
        'TPPart61
        '
        Me.TPPart61.Controls.Add(Me.Panel23)
        Me.TPPart61.Location = New System.Drawing.Point(4, 22)
        Me.TPPart61.Name = "TPPart61"
        Me.TPPart61.Size = New System.Drawing.Size(776, 451)
        Me.TPPart61.TabIndex = 1
        Me.TPPart61.Text = "NESHAP (Part 61) "
        Me.TPPart61.UseVisualStyleBackColor = True
        '
        'Panel23
        '
        Me.Panel23.Controls.Add(Me.dgvNESHAPSubParts)
        Me.Panel23.Controls.Add(Me.Panel24)
        Me.Panel23.Controls.Add(Me.Panel25)
        Me.Panel23.Controls.Add(Me.Panel26)
        Me.Panel23.Controls.Add(Me.Panel27)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel23.Location = New System.Drawing.Point(0, 0)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(776, 451)
        Me.Panel23.TabIndex = 5
        '
        'dgvNESHAPSubParts
        '
        Me.dgvNESHAPSubParts.AllowUserToOrderColumns = True
        Me.dgvNESHAPSubParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNESHAPSubParts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvNESHAPSubParts.Location = New System.Drawing.Point(231, 34)
        Me.dgvNESHAPSubParts.Name = "dgvNESHAPSubParts"
        Me.dgvNESHAPSubParts.ReadOnly = True
        Me.dgvNESHAPSubParts.Size = New System.Drawing.Size(301, 293)
        Me.dgvNESHAPSubParts.TabIndex = 0
        '
        'Panel24
        '
        Me.Panel24.Controls.Add(Me.btnSaveNESHAPSubpart)
        Me.Panel24.Controls.Add(Me.Label65)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel24.Location = New System.Drawing.Point(231, 0)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(301, 34)
        Me.Panel24.TabIndex = 1
        '
        'btnSaveNESHAPSubpart
        '
        Me.btnSaveNESHAPSubpart.AutoSize = True
        Me.btnSaveNESHAPSubpart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveNESHAPSubpart.BackColor = System.Drawing.Color.Tomato
        Me.btnSaveNESHAPSubpart.Location = New System.Drawing.Point(174, 10)
        Me.btnSaveNESHAPSubpart.Name = "btnSaveNESHAPSubpart"
        Me.btnSaveNESHAPSubpart.Size = New System.Drawing.Size(115, 23)
        Me.btnSaveNESHAPSubpart.TabIndex = 1
        Me.btnSaveNESHAPSubpart.Text = "Save NESHAP Data"
        Me.btnSaveNESHAPSubpart.UseVisualStyleBackColor = False
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(3, 15)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(79, 13)
        Me.Label65.TabIndex = 432
        Me.Label65.Text = "Current Facility:"
        '
        'Panel25
        '
        Me.Panel25.Controls.Add(Me.Label66)
        Me.Panel25.Controls.Add(Me.dgvNESHAPSubpartAddEdit)
        Me.Panel25.Controls.Add(Me.btnNESHAPEditAll)
        Me.Panel25.Controls.Add(Me.btnNESHAPUneditAll)
        Me.Panel25.Controls.Add(Me.btnClearAddModifiedNESHAPs)
        Me.Panel25.Controls.Add(Me.btnNESHAPUnedit)
        Me.Panel25.Controls.Add(Me.btnNESHAPEdit)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel25.Location = New System.Drawing.Point(532, 0)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(244, 327)
        Me.Panel25.TabIndex = 2
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(39, 15)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(155, 13)
        Me.Label66.TabIndex = 431
        Me.Label66.Text = "Added/Modified by Application:"
        '
        'dgvNESHAPSubpartAddEdit
        '
        Me.dgvNESHAPSubpartAddEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNESHAPSubpartAddEdit.Location = New System.Drawing.Point(42, 34)
        Me.dgvNESHAPSubpartAddEdit.Name = "dgvNESHAPSubpartAddEdit"
        Me.dgvNESHAPSubpartAddEdit.ReadOnly = True
        Me.dgvNESHAPSubpartAddEdit.Size = New System.Drawing.Size(198, 266)
        Me.dgvNESHAPSubpartAddEdit.TabIndex = 0
        '
        'btnNESHAPEditAll
        '
        Me.btnNESHAPEditAll.AutoSize = True
        Me.btnNESHAPEditAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNESHAPEditAll.Image = Global.Iaip.My.Resources.Resources.MoveAllRightIcon
        Me.btnNESHAPEditAll.Location = New System.Drawing.Point(6, 148)
        Me.btnNESHAPEditAll.Name = "btnNESHAPEditAll"
        Me.btnNESHAPEditAll.Size = New System.Drawing.Size(30, 28)
        Me.btnNESHAPEditAll.TabIndex = 423
        Me.btnNESHAPEditAll.UseVisualStyleBackColor = True
        '
        'btnNESHAPUneditAll
        '
        Me.btnNESHAPUneditAll.AutoSize = True
        Me.btnNESHAPUneditAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNESHAPUneditAll.Image = Global.Iaip.My.Resources.Resources.MoveAllLeftIcon
        Me.btnNESHAPUneditAll.Location = New System.Drawing.Point(6, 184)
        Me.btnNESHAPUneditAll.Name = "btnNESHAPUneditAll"
        Me.btnNESHAPUneditAll.Size = New System.Drawing.Size(30, 28)
        Me.btnNESHAPUneditAll.TabIndex = 4
        Me.btnNESHAPUneditAll.UseVisualStyleBackColor = True
        '
        'btnClearAddModifiedNESHAPs
        '
        Me.btnClearAddModifiedNESHAPs.AutoSize = True
        Me.btnClearAddModifiedNESHAPs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAddModifiedNESHAPs.Location = New System.Drawing.Point(52, 307)
        Me.btnClearAddModifiedNESHAPs.Name = "btnClearAddModifiedNESHAPs"
        Me.btnClearAddModifiedNESHAPs.Size = New System.Drawing.Size(86, 23)
        Me.btnClearAddModifiedNESHAPs.TabIndex = 5
        Me.btnClearAddModifiedNESHAPs.Text = "Clear Selected"
        Me.btnClearAddModifiedNESHAPs.UseVisualStyleBackColor = True
        '
        'btnNESHAPUnedit
        '
        Me.btnNESHAPUnedit.AutoSize = True
        Me.btnNESHAPUnedit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNESHAPUnedit.Image = Global.Iaip.My.Resources.Resources.MoveLeftIcon
        Me.btnNESHAPUnedit.Location = New System.Drawing.Point(6, 92)
        Me.btnNESHAPUnedit.Name = "btnNESHAPUnedit"
        Me.btnNESHAPUnedit.Size = New System.Drawing.Size(30, 28)
        Me.btnNESHAPUnedit.TabIndex = 3
        Me.btnNESHAPUnedit.UseVisualStyleBackColor = True
        '
        'btnNESHAPEdit
        '
        Me.btnNESHAPEdit.AutoSize = True
        Me.btnNESHAPEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNESHAPEdit.Image = Global.Iaip.My.Resources.Resources.MoveRightIcon
        Me.btnNESHAPEdit.Location = New System.Drawing.Point(6, 48)
        Me.btnNESHAPEdit.Name = "btnNESHAPEdit"
        Me.btnNESHAPEdit.Size = New System.Drawing.Size(30, 28)
        Me.btnNESHAPEdit.TabIndex = 1
        Me.btnNESHAPEdit.UseVisualStyleBackColor = True
        '
        'Panel26
        '
        Me.Panel26.Controls.Add(Me.Label67)
        Me.Panel26.Controls.Add(Me.dgvNESHAPSubPartDelete)
        Me.Panel26.Controls.Add(Me.btnClearNESHAPDeletes)
        Me.Panel26.Controls.Add(Me.btnNESHAPDelete)
        Me.Panel26.Controls.Add(Me.btnNESHAPUndeleteAll)
        Me.Panel26.Controls.Add(Me.btnNESHAPDeleteAll)
        Me.Panel26.Controls.Add(Me.btnNESHAPUndelete)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel26.Location = New System.Drawing.Point(0, 0)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(231, 327)
        Me.Panel26.TabIndex = 0
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(7, 15)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(125, 13)
        Me.Label67.TabIndex = 430
        Me.Label67.Text = "Removed by Application:"
        '
        'dgvNESHAPSubPartDelete
        '
        Me.dgvNESHAPSubPartDelete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNESHAPSubPartDelete.Location = New System.Drawing.Point(4, 34)
        Me.dgvNESHAPSubPartDelete.Name = "dgvNESHAPSubPartDelete"
        Me.dgvNESHAPSubPartDelete.ReadOnly = True
        Me.dgvNESHAPSubPartDelete.Size = New System.Drawing.Size(180, 266)
        Me.dgvNESHAPSubPartDelete.TabIndex = 0
        '
        'btnClearNESHAPDeletes
        '
        Me.btnClearNESHAPDeletes.AutoSize = True
        Me.btnClearNESHAPDeletes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearNESHAPDeletes.Location = New System.Drawing.Point(12, 304)
        Me.btnClearNESHAPDeletes.Name = "btnClearNESHAPDeletes"
        Me.btnClearNESHAPDeletes.Size = New System.Drawing.Size(86, 23)
        Me.btnClearNESHAPDeletes.TabIndex = 430
        Me.btnClearNESHAPDeletes.Text = "Clear Selected"
        Me.btnClearNESHAPDeletes.UseVisualStyleBackColor = True
        '
        'btnNESHAPDelete
        '
        Me.btnNESHAPDelete.AutoSize = True
        Me.btnNESHAPDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNESHAPDelete.Image = Global.Iaip.My.Resources.Resources.MoveLeftIcon
        Me.btnNESHAPDelete.Location = New System.Drawing.Point(190, 46)
        Me.btnNESHAPDelete.Name = "btnNESHAPDelete"
        Me.btnNESHAPDelete.Size = New System.Drawing.Size(30, 28)
        Me.btnNESHAPDelete.TabIndex = 1
        Me.btnNESHAPDelete.UseVisualStyleBackColor = True
        '
        'btnNESHAPUndeleteAll
        '
        Me.btnNESHAPUndeleteAll.AutoSize = True
        Me.btnNESHAPUndeleteAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNESHAPUndeleteAll.Image = Global.Iaip.My.Resources.Resources.MoveAllRightIcon
        Me.btnNESHAPUndeleteAll.Location = New System.Drawing.Point(190, 182)
        Me.btnNESHAPUndeleteAll.Name = "btnNESHAPUndeleteAll"
        Me.btnNESHAPUndeleteAll.Size = New System.Drawing.Size(30, 28)
        Me.btnNESHAPUndeleteAll.TabIndex = 4
        Me.btnNESHAPUndeleteAll.UseVisualStyleBackColor = True
        '
        'btnNESHAPDeleteAll
        '
        Me.btnNESHAPDeleteAll.AutoSize = True
        Me.btnNESHAPDeleteAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNESHAPDeleteAll.Image = Global.Iaip.My.Resources.Resources.MoveAllLeftIcon
        Me.btnNESHAPDeleteAll.Location = New System.Drawing.Point(190, 146)
        Me.btnNESHAPDeleteAll.Name = "btnNESHAPDeleteAll"
        Me.btnNESHAPDeleteAll.Size = New System.Drawing.Size(30, 28)
        Me.btnNESHAPDeleteAll.TabIndex = 3
        Me.btnNESHAPDeleteAll.UseVisualStyleBackColor = True
        '
        'btnNESHAPUndelete
        '
        Me.btnNESHAPUndelete.AutoSize = True
        Me.btnNESHAPUndelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNESHAPUndelete.Image = Global.Iaip.My.Resources.Resources.MoveRightIcon
        Me.btnNESHAPUndelete.Location = New System.Drawing.Point(190, 90)
        Me.btnNESHAPUndelete.Name = "btnNESHAPUndelete"
        Me.btnNESHAPUndelete.Size = New System.Drawing.Size(30, 28)
        Me.btnNESHAPUndelete.TabIndex = 2
        Me.btnNESHAPUndelete.UseVisualStyleBackColor = True
        '
        'Panel27
        '
        Me.Panel27.Controls.Add(Me.btnAddNewNESHAPSubpart)
        Me.Panel27.Controls.Add(Me.cboNESHAPSubpart)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel27.Location = New System.Drawing.Point(0, 327)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Size = New System.Drawing.Size(776, 124)
        Me.Panel27.TabIndex = 3
        '
        'btnAddNewNESHAPSubpart
        '
        Me.btnAddNewNESHAPSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNewNESHAPSubpart.AutoSize = True
        Me.btnAddNewNESHAPSubpart.Location = New System.Drawing.Point(610, 12)
        Me.btnAddNewNESHAPSubpart.Name = "btnAddNewNESHAPSubpart"
        Me.btnAddNewNESHAPSubpart.Size = New System.Drawing.Size(141, 23)
        Me.btnAddNewNESHAPSubpart.TabIndex = 1
        Me.btnAddNewNESHAPSubpart.Text = "Add Subpart to Above List"
        Me.btnAddNewNESHAPSubpart.UseVisualStyleBackColor = True
        '
        'cboNESHAPSubpart
        '
        Me.cboNESHAPSubpart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboNESHAPSubpart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNESHAPSubpart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNESHAPSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNESHAPSubpart.FormattingEnabled = True
        Me.cboNESHAPSubpart.Location = New System.Drawing.Point(6, 12)
        Me.cboNESHAPSubpart.Name = "cboNESHAPSubpart"
        Me.cboNESHAPSubpart.Size = New System.Drawing.Size(598, 21)
        Me.cboNESHAPSubpart.TabIndex = 0
        '
        'TPPart63
        '
        Me.TPPart63.Controls.Add(Me.Panel28)
        Me.TPPart63.Location = New System.Drawing.Point(4, 22)
        Me.TPPart63.Name = "TPPart63"
        Me.TPPart63.Size = New System.Drawing.Size(776, 451)
        Me.TPPart63.TabIndex = 2
        Me.TPPart63.Text = "MACT (Part 63)"
        Me.TPPart63.UseVisualStyleBackColor = True
        '
        'Panel28
        '
        Me.Panel28.Controls.Add(Me.dgvMACTSubParts)
        Me.Panel28.Controls.Add(Me.Panel29)
        Me.Panel28.Controls.Add(Me.Panel30)
        Me.Panel28.Controls.Add(Me.Panel31)
        Me.Panel28.Controls.Add(Me.Panel32)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel28.Location = New System.Drawing.Point(0, 0)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Size = New System.Drawing.Size(776, 451)
        Me.Panel28.TabIndex = 5
        '
        'dgvMACTSubParts
        '
        Me.dgvMACTSubParts.AllowUserToOrderColumns = True
        Me.dgvMACTSubParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMACTSubParts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMACTSubParts.Location = New System.Drawing.Point(231, 34)
        Me.dgvMACTSubParts.Name = "dgvMACTSubParts"
        Me.dgvMACTSubParts.ReadOnly = True
        Me.dgvMACTSubParts.Size = New System.Drawing.Size(301, 293)
        Me.dgvMACTSubParts.TabIndex = 2
        '
        'Panel29
        '
        Me.Panel29.Controls.Add(Me.btnSaveMACTSubpart)
        Me.Panel29.Controls.Add(Me.Label69)
        Me.Panel29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel29.Location = New System.Drawing.Point(231, 0)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Size = New System.Drawing.Size(301, 34)
        Me.Panel29.TabIndex = 1
        '
        'btnSaveMACTSubpart
        '
        Me.btnSaveMACTSubpart.AutoSize = True
        Me.btnSaveMACTSubpart.BackColor = System.Drawing.Color.Tomato
        Me.btnSaveMACTSubpart.Location = New System.Drawing.Point(174, 10)
        Me.btnSaveMACTSubpart.Name = "btnSaveMACTSubpart"
        Me.btnSaveMACTSubpart.Size = New System.Drawing.Size(115, 23)
        Me.btnSaveMACTSubpart.TabIndex = 0
        Me.btnSaveMACTSubpart.Text = "Save MACT Data"
        Me.btnSaveMACTSubpart.UseVisualStyleBackColor = False
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(3, 15)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(79, 13)
        Me.Label69.TabIndex = 432
        Me.Label69.Text = "Current Facility:"
        '
        'Panel30
        '
        Me.Panel30.Controls.Add(Me.Label70)
        Me.Panel30.Controls.Add(Me.dgvMACTSubpartAddEdit)
        Me.Panel30.Controls.Add(Me.btnMACTEditAll)
        Me.Panel30.Controls.Add(Me.btnMACTUneditAll)
        Me.Panel30.Controls.Add(Me.btnClearAddModifiedMACTs)
        Me.Panel30.Controls.Add(Me.btnMACTUnedit)
        Me.Panel30.Controls.Add(Me.btnMACTEdit)
        Me.Panel30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel30.Location = New System.Drawing.Point(532, 0)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Size = New System.Drawing.Size(244, 327)
        Me.Panel30.TabIndex = 0
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(39, 15)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(155, 13)
        Me.Label70.TabIndex = 431
        Me.Label70.Text = "Added/Modified by Application:"
        '
        'dgvMACTSubpartAddEdit
        '
        Me.dgvMACTSubpartAddEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMACTSubpartAddEdit.Location = New System.Drawing.Point(42, 34)
        Me.dgvMACTSubpartAddEdit.Name = "dgvMACTSubpartAddEdit"
        Me.dgvMACTSubpartAddEdit.ReadOnly = True
        Me.dgvMACTSubpartAddEdit.Size = New System.Drawing.Size(198, 266)
        Me.dgvMACTSubpartAddEdit.TabIndex = 427
        '
        'btnMACTEditAll
        '
        Me.btnMACTEditAll.AutoSize = True
        Me.btnMACTEditAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMACTEditAll.Image = Global.Iaip.My.Resources.Resources.MoveAllRightIcon
        Me.btnMACTEditAll.Location = New System.Drawing.Point(6, 148)
        Me.btnMACTEditAll.Name = "btnMACTEditAll"
        Me.btnMACTEditAll.Size = New System.Drawing.Size(30, 28)
        Me.btnMACTEditAll.TabIndex = 2
        Me.btnMACTEditAll.UseVisualStyleBackColor = True
        '
        'btnMACTUneditAll
        '
        Me.btnMACTUneditAll.AutoSize = True
        Me.btnMACTUneditAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMACTUneditAll.Image = Global.Iaip.My.Resources.Resources.MoveAllLeftIcon
        Me.btnMACTUneditAll.Location = New System.Drawing.Point(6, 184)
        Me.btnMACTUneditAll.Name = "btnMACTUneditAll"
        Me.btnMACTUneditAll.Size = New System.Drawing.Size(30, 28)
        Me.btnMACTUneditAll.TabIndex = 3
        Me.btnMACTUneditAll.UseVisualStyleBackColor = True
        '
        'btnClearAddModifiedMACTs
        '
        Me.btnClearAddModifiedMACTs.AutoSize = True
        Me.btnClearAddModifiedMACTs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearAddModifiedMACTs.Location = New System.Drawing.Point(52, 307)
        Me.btnClearAddModifiedMACTs.Name = "btnClearAddModifiedMACTs"
        Me.btnClearAddModifiedMACTs.Size = New System.Drawing.Size(86, 23)
        Me.btnClearAddModifiedMACTs.TabIndex = 5
        Me.btnClearAddModifiedMACTs.Text = "Clear Selected"
        Me.btnClearAddModifiedMACTs.UseVisualStyleBackColor = True
        '
        'btnMACTUnedit
        '
        Me.btnMACTUnedit.AutoSize = True
        Me.btnMACTUnedit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMACTUnedit.Image = Global.Iaip.My.Resources.Resources.MoveLeftIcon
        Me.btnMACTUnedit.Location = New System.Drawing.Point(6, 92)
        Me.btnMACTUnedit.Name = "btnMACTUnedit"
        Me.btnMACTUnedit.Size = New System.Drawing.Size(30, 28)
        Me.btnMACTUnedit.TabIndex = 1
        Me.btnMACTUnedit.UseVisualStyleBackColor = True
        '
        'btnMACTEdit
        '
        Me.btnMACTEdit.AutoSize = True
        Me.btnMACTEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMACTEdit.Image = Global.Iaip.My.Resources.Resources.MoveRightIcon
        Me.btnMACTEdit.Location = New System.Drawing.Point(6, 48)
        Me.btnMACTEdit.Name = "btnMACTEdit"
        Me.btnMACTEdit.Size = New System.Drawing.Size(30, 28)
        Me.btnMACTEdit.TabIndex = 0
        Me.btnMACTEdit.UseVisualStyleBackColor = True
        '
        'Panel31
        '
        Me.Panel31.Controls.Add(Me.Label71)
        Me.Panel31.Controls.Add(Me.dgvMACTSubPartDelete)
        Me.Panel31.Controls.Add(Me.btnClearMACTDeletes)
        Me.Panel31.Controls.Add(Me.btnMACTDelete)
        Me.Panel31.Controls.Add(Me.btnMACTUndeleteAll)
        Me.Panel31.Controls.Add(Me.btnMACTDeleteAll)
        Me.Panel31.Controls.Add(Me.btnMACTUndelete)
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel31.Location = New System.Drawing.Point(0, 0)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(231, 327)
        Me.Panel31.TabIndex = 0
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(7, 15)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(125, 13)
        Me.Label71.TabIndex = 430
        Me.Label71.Text = "Removed by Application:"
        '
        'dgvMACTSubPartDelete
        '
        Me.dgvMACTSubPartDelete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMACTSubPartDelete.Location = New System.Drawing.Point(4, 34)
        Me.dgvMACTSubPartDelete.Name = "dgvMACTSubPartDelete"
        Me.dgvMACTSubPartDelete.ReadOnly = True
        Me.dgvMACTSubPartDelete.Size = New System.Drawing.Size(180, 266)
        Me.dgvMACTSubPartDelete.TabIndex = 0
        '
        'btnClearMACTDeletes
        '
        Me.btnClearMACTDeletes.AutoSize = True
        Me.btnClearMACTDeletes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClearMACTDeletes.Location = New System.Drawing.Point(12, 304)
        Me.btnClearMACTDeletes.Name = "btnClearMACTDeletes"
        Me.btnClearMACTDeletes.Size = New System.Drawing.Size(86, 23)
        Me.btnClearMACTDeletes.TabIndex = 430
        Me.btnClearMACTDeletes.Text = "Clear Selected"
        Me.btnClearMACTDeletes.UseVisualStyleBackColor = True
        '
        'btnMACTDelete
        '
        Me.btnMACTDelete.AutoSize = True
        Me.btnMACTDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMACTDelete.Image = Global.Iaip.My.Resources.Resources.MoveLeftIcon
        Me.btnMACTDelete.Location = New System.Drawing.Point(190, 46)
        Me.btnMACTDelete.Name = "btnMACTDelete"
        Me.btnMACTDelete.Size = New System.Drawing.Size(30, 28)
        Me.btnMACTDelete.TabIndex = 1
        Me.btnMACTDelete.UseVisualStyleBackColor = True
        '
        'btnMACTUndeleteAll
        '
        Me.btnMACTUndeleteAll.AutoSize = True
        Me.btnMACTUndeleteAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMACTUndeleteAll.Image = Global.Iaip.My.Resources.Resources.MoveAllRightIcon
        Me.btnMACTUndeleteAll.Location = New System.Drawing.Point(190, 182)
        Me.btnMACTUndeleteAll.Name = "btnMACTUndeleteAll"
        Me.btnMACTUndeleteAll.Size = New System.Drawing.Size(30, 28)
        Me.btnMACTUndeleteAll.TabIndex = 419
        Me.btnMACTUndeleteAll.UseVisualStyleBackColor = True
        '
        'btnMACTDeleteAll
        '
        Me.btnMACTDeleteAll.AutoSize = True
        Me.btnMACTDeleteAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMACTDeleteAll.Image = Global.Iaip.My.Resources.Resources.MoveAllLeftIcon
        Me.btnMACTDeleteAll.Location = New System.Drawing.Point(190, 146)
        Me.btnMACTDeleteAll.Name = "btnMACTDeleteAll"
        Me.btnMACTDeleteAll.Size = New System.Drawing.Size(30, 28)
        Me.btnMACTDeleteAll.TabIndex = 3
        Me.btnMACTDeleteAll.UseVisualStyleBackColor = True
        '
        'btnMACTUndelete
        '
        Me.btnMACTUndelete.AutoSize = True
        Me.btnMACTUndelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMACTUndelete.Image = Global.Iaip.My.Resources.Resources.MoveRightIcon
        Me.btnMACTUndelete.Location = New System.Drawing.Point(190, 90)
        Me.btnMACTUndelete.Name = "btnMACTUndelete"
        Me.btnMACTUndelete.Size = New System.Drawing.Size(30, 28)
        Me.btnMACTUndelete.TabIndex = 2
        Me.btnMACTUndelete.UseVisualStyleBackColor = True
        '
        'Panel32
        '
        Me.Panel32.Controls.Add(Me.btnAddNewMACTSubpart)
        Me.Panel32.Controls.Add(Me.cboMACTSubpart)
        Me.Panel32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel32.Location = New System.Drawing.Point(0, 327)
        Me.Panel32.Name = "Panel32"
        Me.Panel32.Size = New System.Drawing.Size(776, 124)
        Me.Panel32.TabIndex = 1
        '
        'btnAddNewMACTSubpart
        '
        Me.btnAddNewMACTSubpart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNewMACTSubpart.AutoSize = True
        Me.btnAddNewMACTSubpart.Location = New System.Drawing.Point(610, 12)
        Me.btnAddNewMACTSubpart.Name = "btnAddNewMACTSubpart"
        Me.btnAddNewMACTSubpart.Size = New System.Drawing.Size(141, 23)
        Me.btnAddNewMACTSubpart.TabIndex = 429
        Me.btnAddNewMACTSubpart.Text = "Add Subpart to Above List"
        Me.btnAddNewMACTSubpart.UseVisualStyleBackColor = True
        '
        'cboMACTSubpart
        '
        Me.cboMACTSubpart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboMACTSubpart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMACTSubpart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMACTSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMACTSubpart.FormattingEnabled = True
        Me.cboMACTSubpart.Location = New System.Drawing.Point(6, 12)
        Me.cboMACTSubpart.Name = "cboMACTSubpart"
        Me.cboMACTSubpart.Size = New System.Drawing.Size(598, 21)
        Me.cboMACTSubpart.TabIndex = 0
        '
        'TPDocuments
        '
        Me.TPDocuments.Controls.Add(Me.PanelTitleV)
        Me.TPDocuments.Controls.Add(Me.PanelPSD)
        Me.TPDocuments.Controls.Add(Me.Panel7)
        Me.TPDocuments.Controls.Add(Me.Label47)
        Me.TPDocuments.Controls.Add(Me.lblPDF)
        Me.TPDocuments.Controls.Add(Me.lblWord)
        Me.TPDocuments.Controls.Add(Me.PanelOther)
        Me.TPDocuments.Location = New System.Drawing.Point(4, 22)
        Me.TPDocuments.Name = "TPDocuments"
        Me.TPDocuments.Size = New System.Drawing.Size(784, 477)
        Me.TPDocuments.TabIndex = 7
        Me.TPDocuments.Text = "Documents"
        Me.TPDocuments.UseVisualStyleBackColor = True
        '
        'PanelTitleV
        '
        Me.PanelTitleV.AutoSize = True
        Me.PanelTitleV.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelTitleV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTitleV.Controls.Add(Me.btnTVFinalDownload)
        Me.PanelTitleV.Controls.Add(Me.btnTVPublicNoticeDownload)
        Me.PanelTitleV.Controls.Add(Me.btnTVDraftDownload)
        Me.PanelTitleV.Controls.Add(Me.btnTVNarrativeDownload)
        Me.PanelTitleV.Controls.Add(Me.lblTVFinalDUPDF)
        Me.PanelTitleV.Controls.Add(Me.lblTVFinalSRPDF)
        Me.PanelTitleV.Controls.Add(Me.lblTVFinalDUDoc)
        Me.PanelTitleV.Controls.Add(Me.lblTVFinalSRDoc)
        Me.PanelTitleV.Controls.Add(Me.lblTVPublicNoticeDUPDF)
        Me.PanelTitleV.Controls.Add(Me.lblTVPublicNoticeSRPDF)
        Me.PanelTitleV.Controls.Add(Me.lblTVPublicNoticeDUDoc)
        Me.PanelTitleV.Controls.Add(Me.lblTVPublicNoticeSRDoc)
        Me.PanelTitleV.Controls.Add(Me.lblTVDraftDUPDF)
        Me.PanelTitleV.Controls.Add(Me.lblTVDraftSRPDF)
        Me.PanelTitleV.Controls.Add(Me.lblTVDraftDUDoc)
        Me.PanelTitleV.Controls.Add(Me.lblTVDraftSRDoc)
        Me.PanelTitleV.Controls.Add(Me.lblTVNarrativeDUPDF)
        Me.PanelTitleV.Controls.Add(Me.lblTVNarrativeSRPDF)
        Me.PanelTitleV.Controls.Add(Me.lblTVNarrativeDUDoc)
        Me.PanelTitleV.Controls.Add(Me.lblTVNarrativeSRDoc)
        Me.PanelTitleV.Controls.Add(Me.txtTVFinalPDF)
        Me.PanelTitleV.Controls.Add(Me.txtTVPublicNoticePDF)
        Me.PanelTitleV.Controls.Add(Me.txtTVDraftPDF)
        Me.PanelTitleV.Controls.Add(Me.txtTVNarrativePDF)
        Me.PanelTitleV.Controls.Add(Me.txtTVFinalDoc)
        Me.PanelTitleV.Controls.Add(Me.txtTVPublicNoticeDoc)
        Me.PanelTitleV.Controls.Add(Me.txtTVDraftDoc)
        Me.PanelTitleV.Controls.Add(Me.txtTVNarrativeDoc)
        Me.PanelTitleV.Controls.Add(Me.chbTVFinal)
        Me.PanelTitleV.Controls.Add(Me.chbTVPublicNotice)
        Me.PanelTitleV.Controls.Add(Me.chbTVDraft)
        Me.PanelTitleV.Controls.Add(Me.chbTVNarrative)
        Me.PanelTitleV.Location = New System.Drawing.Point(97, 25)
        Me.PanelTitleV.Name = "PanelTitleV"
        Me.PanelTitleV.Size = New System.Drawing.Size(652, 224)
        Me.PanelTitleV.TabIndex = 1
        Me.PanelTitleV.Visible = False
        '
        'btnTVFinalDownload
        '
        Me.btnTVFinalDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnTVFinalDownload.Location = New System.Drawing.Point(623, 166)
        Me.btnTVFinalDownload.Name = "btnTVFinalDownload"
        Me.btnTVFinalDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnTVFinalDownload.TabIndex = 13
        Me.btnTVFinalDownload.UseVisualStyleBackColor = True
        Me.btnTVFinalDownload.Visible = False
        '
        'btnTVPublicNoticeDownload
        '
        Me.btnTVPublicNoticeDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnTVPublicNoticeDownload.Location = New System.Drawing.Point(623, 113)
        Me.btnTVPublicNoticeDownload.Name = "btnTVPublicNoticeDownload"
        Me.btnTVPublicNoticeDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnTVPublicNoticeDownload.TabIndex = 9
        Me.btnTVPublicNoticeDownload.UseVisualStyleBackColor = True
        Me.btnTVPublicNoticeDownload.Visible = False
        '
        'btnTVDraftDownload
        '
        Me.btnTVDraftDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnTVDraftDownload.Location = New System.Drawing.Point(623, 58)
        Me.btnTVDraftDownload.Name = "btnTVDraftDownload"
        Me.btnTVDraftDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnTVDraftDownload.TabIndex = 5
        Me.btnTVDraftDownload.UseVisualStyleBackColor = True
        Me.btnTVDraftDownload.Visible = False
        '
        'btnTVNarrativeDownload
        '
        Me.btnTVNarrativeDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnTVNarrativeDownload.Location = New System.Drawing.Point(623, 4)
        Me.btnTVNarrativeDownload.Name = "btnTVNarrativeDownload"
        Me.btnTVNarrativeDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnTVNarrativeDownload.TabIndex = 2
        Me.btnTVNarrativeDownload.UseVisualStyleBackColor = True
        Me.btnTVNarrativeDownload.Visible = False
        '
        'lblTVFinalDUPDF
        '
        Me.lblTVFinalDUPDF.AutoSize = True
        Me.lblTVFinalDUPDF.Location = New System.Drawing.Point(385, 207)
        Me.lblTVFinalDUPDF.Name = "lblTVFinalDUPDF"
        Me.lblTVFinalDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblTVFinalDUPDF.TabIndex = 40
        Me.lblTVFinalDUPDF.Text = "DateUploaded"
        Me.lblTVFinalDUPDF.Visible = False
        '
        'lblTVFinalSRPDF
        '
        Me.lblTVFinalSRPDF.AutoSize = True
        Me.lblTVFinalSRPDF.Location = New System.Drawing.Point(385, 190)
        Me.lblTVFinalSRPDF.Name = "lblTVFinalSRPDF"
        Me.lblTVFinalSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblTVFinalSRPDF.TabIndex = 39
        Me.lblTVFinalSRPDF.Text = "Staff Responsible"
        Me.lblTVFinalSRPDF.Visible = False
        '
        'lblTVFinalDUDoc
        '
        Me.lblTVFinalDUDoc.AutoSize = True
        Me.lblTVFinalDUDoc.Location = New System.Drawing.Point(135, 209)
        Me.lblTVFinalDUDoc.Name = "lblTVFinalDUDoc"
        Me.lblTVFinalDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblTVFinalDUDoc.TabIndex = 38
        Me.lblTVFinalDUDoc.Text = "DateUploaded"
        Me.lblTVFinalDUDoc.Visible = False
        '
        'lblTVFinalSRDoc
        '
        Me.lblTVFinalSRDoc.AutoSize = True
        Me.lblTVFinalSRDoc.Location = New System.Drawing.Point(135, 190)
        Me.lblTVFinalSRDoc.Name = "lblTVFinalSRDoc"
        Me.lblTVFinalSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblTVFinalSRDoc.TabIndex = 37
        Me.lblTVFinalSRDoc.Text = "Staff Responsible"
        Me.lblTVFinalSRDoc.Visible = False
        '
        'lblTVPublicNoticeDUPDF
        '
        Me.lblTVPublicNoticeDUPDF.AutoSize = True
        Me.lblTVPublicNoticeDUPDF.Location = New System.Drawing.Point(385, 151)
        Me.lblTVPublicNoticeDUPDF.Name = "lblTVPublicNoticeDUPDF"
        Me.lblTVPublicNoticeDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblTVPublicNoticeDUPDF.TabIndex = 36
        Me.lblTVPublicNoticeDUPDF.Text = "DateUploaded"
        Me.lblTVPublicNoticeDUPDF.Visible = False
        '
        'lblTVPublicNoticeSRPDF
        '
        Me.lblTVPublicNoticeSRPDF.AutoSize = True
        Me.lblTVPublicNoticeSRPDF.Location = New System.Drawing.Point(385, 137)
        Me.lblTVPublicNoticeSRPDF.Name = "lblTVPublicNoticeSRPDF"
        Me.lblTVPublicNoticeSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblTVPublicNoticeSRPDF.TabIndex = 35
        Me.lblTVPublicNoticeSRPDF.Text = "Staff Responsible"
        Me.lblTVPublicNoticeSRPDF.Visible = False
        '
        'lblTVPublicNoticeDUDoc
        '
        Me.lblTVPublicNoticeDUDoc.AutoSize = True
        Me.lblTVPublicNoticeDUDoc.Location = New System.Drawing.Point(135, 152)
        Me.lblTVPublicNoticeDUDoc.Name = "lblTVPublicNoticeDUDoc"
        Me.lblTVPublicNoticeDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblTVPublicNoticeDUDoc.TabIndex = 34
        Me.lblTVPublicNoticeDUDoc.Text = "DateUploaded"
        Me.lblTVPublicNoticeDUDoc.Visible = False
        '
        'lblTVPublicNoticeSRDoc
        '
        Me.lblTVPublicNoticeSRDoc.AutoSize = True
        Me.lblTVPublicNoticeSRDoc.Location = New System.Drawing.Point(135, 137)
        Me.lblTVPublicNoticeSRDoc.Name = "lblTVPublicNoticeSRDoc"
        Me.lblTVPublicNoticeSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblTVPublicNoticeSRDoc.TabIndex = 33
        Me.lblTVPublicNoticeSRDoc.Text = "Staff Responsible"
        Me.lblTVPublicNoticeSRDoc.Visible = False
        '
        'lblTVDraftDUPDF
        '
        Me.lblTVDraftDUPDF.AutoSize = True
        Me.lblTVDraftDUPDF.Location = New System.Drawing.Point(385, 97)
        Me.lblTVDraftDUPDF.Name = "lblTVDraftDUPDF"
        Me.lblTVDraftDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblTVDraftDUPDF.TabIndex = 32
        Me.lblTVDraftDUPDF.Text = "DateUploaded"
        Me.lblTVDraftDUPDF.Visible = False
        '
        'lblTVDraftSRPDF
        '
        Me.lblTVDraftSRPDF.AutoSize = True
        Me.lblTVDraftSRPDF.Location = New System.Drawing.Point(385, 82)
        Me.lblTVDraftSRPDF.Name = "lblTVDraftSRPDF"
        Me.lblTVDraftSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblTVDraftSRPDF.TabIndex = 31
        Me.lblTVDraftSRPDF.Text = "Staff Responsible"
        Me.lblTVDraftSRPDF.Visible = False
        '
        'lblTVDraftDUDoc
        '
        Me.lblTVDraftDUDoc.AutoSize = True
        Me.lblTVDraftDUDoc.Location = New System.Drawing.Point(135, 97)
        Me.lblTVDraftDUDoc.Name = "lblTVDraftDUDoc"
        Me.lblTVDraftDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblTVDraftDUDoc.TabIndex = 30
        Me.lblTVDraftDUDoc.Text = "DateUploaded"
        Me.lblTVDraftDUDoc.Visible = False
        '
        'lblTVDraftSRDoc
        '
        Me.lblTVDraftSRDoc.AutoSize = True
        Me.lblTVDraftSRDoc.Location = New System.Drawing.Point(135, 82)
        Me.lblTVDraftSRDoc.Name = "lblTVDraftSRDoc"
        Me.lblTVDraftSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblTVDraftSRDoc.TabIndex = 29
        Me.lblTVDraftSRDoc.Text = "Staff Responsible"
        Me.lblTVDraftSRDoc.Visible = False
        '
        'lblTVNarrativeDUPDF
        '
        Me.lblTVNarrativeDUPDF.AutoSize = True
        Me.lblTVNarrativeDUPDF.Location = New System.Drawing.Point(385, 44)
        Me.lblTVNarrativeDUPDF.Name = "lblTVNarrativeDUPDF"
        Me.lblTVNarrativeDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblTVNarrativeDUPDF.TabIndex = 28
        Me.lblTVNarrativeDUPDF.Text = "DateUploaded"
        Me.lblTVNarrativeDUPDF.Visible = False
        '
        'lblTVNarrativeSRPDF
        '
        Me.lblTVNarrativeSRPDF.AutoSize = True
        Me.lblTVNarrativeSRPDF.Location = New System.Drawing.Point(385, 28)
        Me.lblTVNarrativeSRPDF.Name = "lblTVNarrativeSRPDF"
        Me.lblTVNarrativeSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblTVNarrativeSRPDF.TabIndex = 27
        Me.lblTVNarrativeSRPDF.Text = "Staff Responsible"
        Me.lblTVNarrativeSRPDF.Visible = False
        '
        'lblTVNarrativeDUDoc
        '
        Me.lblTVNarrativeDUDoc.AutoSize = True
        Me.lblTVNarrativeDUDoc.Location = New System.Drawing.Point(135, 43)
        Me.lblTVNarrativeDUDoc.Name = "lblTVNarrativeDUDoc"
        Me.lblTVNarrativeDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblTVNarrativeDUDoc.TabIndex = 26
        Me.lblTVNarrativeDUDoc.Text = "DateUploaded"
        Me.lblTVNarrativeDUDoc.Visible = False
        '
        'lblTVNarrativeSRDoc
        '
        Me.lblTVNarrativeSRDoc.AutoSize = True
        Me.lblTVNarrativeSRDoc.Location = New System.Drawing.Point(135, 28)
        Me.lblTVNarrativeSRDoc.Name = "lblTVNarrativeSRDoc"
        Me.lblTVNarrativeSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblTVNarrativeSRDoc.TabIndex = 25
        Me.lblTVNarrativeSRDoc.Text = "Staff Responsible"
        Me.lblTVNarrativeSRDoc.Visible = False
        '
        'txtTVFinalPDF
        '
        Me.txtTVFinalPDF.Location = New System.Drawing.Point(380, 169)
        Me.txtTVFinalPDF.Name = "txtTVFinalPDF"
        Me.txtTVFinalPDF.ReadOnly = True
        Me.txtTVFinalPDF.Size = New System.Drawing.Size(241, 20)
        Me.txtTVFinalPDF.TabIndex = 12
        Me.txtTVFinalPDF.Visible = False
        '
        'txtTVPublicNoticePDF
        '
        Me.txtTVPublicNoticePDF.Location = New System.Drawing.Point(376, 114)
        Me.txtTVPublicNoticePDF.Name = "txtTVPublicNoticePDF"
        Me.txtTVPublicNoticePDF.ReadOnly = True
        Me.txtTVPublicNoticePDF.Size = New System.Drawing.Size(241, 20)
        Me.txtTVPublicNoticePDF.TabIndex = 8
        Me.txtTVPublicNoticePDF.Visible = False
        '
        'txtTVDraftPDF
        '
        Me.txtTVDraftPDF.Location = New System.Drawing.Point(376, 60)
        Me.txtTVDraftPDF.Name = "txtTVDraftPDF"
        Me.txtTVDraftPDF.ReadOnly = True
        Me.txtTVDraftPDF.Size = New System.Drawing.Size(241, 20)
        Me.txtTVDraftPDF.TabIndex = 4
        Me.txtTVDraftPDF.Visible = False
        '
        'txtTVNarrativePDF
        '
        Me.txtTVNarrativePDF.Location = New System.Drawing.Point(376, 5)
        Me.txtTVNarrativePDF.Name = "txtTVNarrativePDF"
        Me.txtTVNarrativePDF.ReadOnly = True
        Me.txtTVNarrativePDF.Size = New System.Drawing.Size(241, 20)
        Me.txtTVNarrativePDF.TabIndex = 1
        Me.txtTVNarrativePDF.Visible = False
        '
        'txtTVFinalDoc
        '
        Me.txtTVFinalDoc.Location = New System.Drawing.Point(129, 169)
        Me.txtTVFinalDoc.Name = "txtTVFinalDoc"
        Me.txtTVFinalDoc.ReadOnly = True
        Me.txtTVFinalDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtTVFinalDoc.TabIndex = 11
        Me.txtTVFinalDoc.Visible = False
        '
        'txtTVPublicNoticeDoc
        '
        Me.txtTVPublicNoticeDoc.Location = New System.Drawing.Point(129, 114)
        Me.txtTVPublicNoticeDoc.Name = "txtTVPublicNoticeDoc"
        Me.txtTVPublicNoticeDoc.ReadOnly = True
        Me.txtTVPublicNoticeDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtTVPublicNoticeDoc.TabIndex = 7
        Me.txtTVPublicNoticeDoc.Visible = False
        '
        'txtTVDraftDoc
        '
        Me.txtTVDraftDoc.Location = New System.Drawing.Point(129, 58)
        Me.txtTVDraftDoc.Name = "txtTVDraftDoc"
        Me.txtTVDraftDoc.ReadOnly = True
        Me.txtTVDraftDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtTVDraftDoc.TabIndex = 3
        Me.txtTVDraftDoc.Visible = False
        '
        'txtTVNarrativeDoc
        '
        Me.txtTVNarrativeDoc.Location = New System.Drawing.Point(129, 5)
        Me.txtTVNarrativeDoc.Name = "txtTVNarrativeDoc"
        Me.txtTVNarrativeDoc.ReadOnly = True
        Me.txtTVNarrativeDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtTVNarrativeDoc.TabIndex = 0
        Me.txtTVNarrativeDoc.Visible = False
        '
        'chbTVFinal
        '
        Me.chbTVFinal.AutoSize = True
        Me.chbTVFinal.Location = New System.Drawing.Point(4, 170)
        Me.chbTVFinal.Name = "chbTVFinal"
        Me.chbTVFinal.Size = New System.Drawing.Size(80, 17)
        Me.chbTVFinal.TabIndex = 10
        Me.chbTVFinal.Text = "Final Permit"
        Me.chbTVFinal.UseVisualStyleBackColor = True
        '
        'chbTVPublicNotice
        '
        Me.chbTVPublicNotice.AutoSize = True
        Me.chbTVPublicNotice.Location = New System.Drawing.Point(4, 116)
        Me.chbTVPublicNotice.Name = "chbTVPublicNotice"
        Me.chbTVPublicNotice.Size = New System.Drawing.Size(89, 17)
        Me.chbTVPublicNotice.TabIndex = 6
        Me.chbTVPublicNotice.Text = "Public Notice"
        Me.chbTVPublicNotice.UseVisualStyleBackColor = True
        '
        'chbTVDraft
        '
        Me.chbTVDraft.AutoSize = True
        Me.chbTVDraft.Location = New System.Drawing.Point(4, 62)
        Me.chbTVDraft.Name = "chbTVDraft"
        Me.chbTVDraft.Size = New System.Drawing.Size(81, 17)
        Me.chbTVDraft.TabIndex = 4
        Me.chbTVDraft.Text = "Draft Permit"
        Me.chbTVDraft.UseVisualStyleBackColor = True
        '
        'chbTVNarrative
        '
        Me.chbTVNarrative.AutoSize = True
        Me.chbTVNarrative.Location = New System.Drawing.Point(4, 7)
        Me.chbTVNarrative.Name = "chbTVNarrative"
        Me.chbTVNarrative.Size = New System.Drawing.Size(69, 17)
        Me.chbTVNarrative.TabIndex = 3
        Me.chbTVNarrative.Text = "Narrative"
        Me.chbTVNarrative.UseVisualStyleBackColor = True
        '
        'PanelPSD
        '
        Me.PanelPSD.AutoSize = True
        Me.PanelPSD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelPSD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelPSD.Controls.Add(Me.btnPSDNarrativeDownload)
        Me.PanelPSD.Controls.Add(Me.lblPSDNarrativeDUPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDNarrativeDUDoc)
        Me.PanelPSD.Controls.Add(Me.txtPSDNarrativePDF)
        Me.PanelPSD.Controls.Add(Me.txtPSDNarrativeDoc)
        Me.PanelPSD.Controls.Add(Me.chbPSDNarrative)
        Me.PanelPSD.Controls.Add(Me.lblPSDNarrativeSRPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDNarrativeSRDoc)
        Me.PanelPSD.Controls.Add(Me.btnPSDPublicNoticeDownload)
        Me.PanelPSD.Controls.Add(Me.btnPSDDraftPermitDownload)
        Me.PanelPSD.Controls.Add(Me.btnPSDPrelimDetDownload)
        Me.PanelPSD.Controls.Add(Me.btnPSDHearingNoticeDownload)
        Me.PanelPSD.Controls.Add(Me.btnPSDFinalDetDownload)
        Me.PanelPSD.Controls.Add(Me.btnPSDFinalPermitDownload)
        Me.PanelPSD.Controls.Add(Me.btnPSDAppSummaryDownload)
        Me.PanelPSD.Controls.Add(Me.lblPSDFinalPermitDUPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDFinalPermitSRPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDFinalPermitDUDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDFinalPermitSRDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDFinalDetDUPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDFinalDetSRPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDFinalDetDUDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDFinalDetSRDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDHearingNoticeDUPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDHearingNoticeSRPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDHearingNoticeDUDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDHearingNoticeSRDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDPublicNoticeDUPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDPublicNoticeSRPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDPublicNoticeDUDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDPublicNoticeSRDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDDraftPermitDUPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDDraftPermitSRPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDDraftPermitDUDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDDraftPermitSRDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDPrelimDetDUPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDPrelimDetSRPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDPrelimDetDUDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDPrelimDetSRDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDAppSummaryDUPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDAppSummarySRPDF)
        Me.PanelPSD.Controls.Add(Me.lblPSDAppSummaryDUDoc)
        Me.PanelPSD.Controls.Add(Me.lblPSDAppSummarySRDoc)
        Me.PanelPSD.Controls.Add(Me.txtPSDPrelimDetPDF)
        Me.PanelPSD.Controls.Add(Me.txtPSDDraftPermitPDF)
        Me.PanelPSD.Controls.Add(Me.txtPSDPublicNoticePDF)
        Me.PanelPSD.Controls.Add(Me.txtPSDHearingNoticePDF)
        Me.PanelPSD.Controls.Add(Me.txtPSDFinalDetPDF)
        Me.PanelPSD.Controls.Add(Me.txtPSDAppSummaryPDF)
        Me.PanelPSD.Controls.Add(Me.txtPSDFinalPermitPDF)
        Me.PanelPSD.Controls.Add(Me.txtPSDFinalPermitDoc)
        Me.PanelPSD.Controls.Add(Me.chbPSDFinalPermit)
        Me.PanelPSD.Controls.Add(Me.txtPSDFinalDetDoc)
        Me.PanelPSD.Controls.Add(Me.chbPSDFinalDet)
        Me.PanelPSD.Controls.Add(Me.txtPSDHearingNoticeDoc)
        Me.PanelPSD.Controls.Add(Me.chbPSDHearingNotice)
        Me.PanelPSD.Controls.Add(Me.txtPSDPublicNoticeDoc)
        Me.PanelPSD.Controls.Add(Me.txtPSDDraftPermitDoc)
        Me.PanelPSD.Controls.Add(Me.txtPSDPrelimDetDoc)
        Me.PanelPSD.Controls.Add(Me.txtPSDAppSummaryDoc)
        Me.PanelPSD.Controls.Add(Me.chbPSDPublicNotice)
        Me.PanelPSD.Controls.Add(Me.chbPSDDraftPermit)
        Me.PanelPSD.Controls.Add(Me.chbPSDPrelimDet)
        Me.PanelPSD.Controls.Add(Me.chbPSDApplicationSummary)
        Me.PanelPSD.Location = New System.Drawing.Point(101, 25)
        Me.PanelPSD.Name = "PanelPSD"
        Me.PanelPSD.Size = New System.Drawing.Size(652, 439)
        Me.PanelPSD.TabIndex = 2
        Me.PanelPSD.Visible = False
        '
        'btnPSDNarrativeDownload
        '
        Me.btnPSDNarrativeDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnPSDNarrativeDownload.Location = New System.Drawing.Point(623, 114)
        Me.btnPSDNarrativeDownload.Name = "btnPSDNarrativeDownload"
        Me.btnPSDNarrativeDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnPSDNarrativeDownload.TabIndex = 11
        Me.btnPSDNarrativeDownload.UseVisualStyleBackColor = True
        Me.btnPSDNarrativeDownload.Visible = False
        '
        'lblPSDNarrativeDUPDF
        '
        Me.lblPSDNarrativeDUPDF.AutoSize = True
        Me.lblPSDNarrativeDUPDF.Location = New System.Drawing.Point(385, 154)
        Me.lblPSDNarrativeDUPDF.Name = "lblPSDNarrativeDUPDF"
        Me.lblPSDNarrativeDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDNarrativeDUPDF.TabIndex = 84
        Me.lblPSDNarrativeDUPDF.Text = "DateUploaded"
        Me.lblPSDNarrativeDUPDF.Visible = False
        '
        'lblPSDNarrativeDUDoc
        '
        Me.lblPSDNarrativeDUDoc.AutoSize = True
        Me.lblPSDNarrativeDUDoc.Location = New System.Drawing.Point(135, 154)
        Me.lblPSDNarrativeDUDoc.Name = "lblPSDNarrativeDUDoc"
        Me.lblPSDNarrativeDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDNarrativeDUDoc.TabIndex = 82
        Me.lblPSDNarrativeDUDoc.Text = "DateUploaded"
        Me.lblPSDNarrativeDUDoc.Visible = False
        '
        'txtPSDNarrativePDF
        '
        Me.txtPSDNarrativePDF.Location = New System.Drawing.Point(376, 115)
        Me.txtPSDNarrativePDF.Name = "txtPSDNarrativePDF"
        Me.txtPSDNarrativePDF.ReadOnly = True
        Me.txtPSDNarrativePDF.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDNarrativePDF.TabIndex = 10
        Me.txtPSDNarrativePDF.Visible = False
        '
        'txtPSDNarrativeDoc
        '
        Me.txtPSDNarrativeDoc.Location = New System.Drawing.Point(129, 115)
        Me.txtPSDNarrativeDoc.Name = "txtPSDNarrativeDoc"
        Me.txtPSDNarrativeDoc.ReadOnly = True
        Me.txtPSDNarrativeDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDNarrativeDoc.TabIndex = 9
        Me.txtPSDNarrativeDoc.Visible = False
        '
        'chbPSDNarrative
        '
        Me.chbPSDNarrative.AutoSize = True
        Me.chbPSDNarrative.Location = New System.Drawing.Point(4, 117)
        Me.chbPSDNarrative.Name = "chbPSDNarrative"
        Me.chbPSDNarrative.Size = New System.Drawing.Size(69, 17)
        Me.chbPSDNarrative.TabIndex = 8
        Me.chbPSDNarrative.Text = "Narrative"
        Me.chbPSDNarrative.UseVisualStyleBackColor = True
        '
        'lblPSDNarrativeSRPDF
        '
        Me.lblPSDNarrativeSRPDF.AutoSize = True
        Me.lblPSDNarrativeSRPDF.Location = New System.Drawing.Point(385, 138)
        Me.lblPSDNarrativeSRPDF.Name = "lblPSDNarrativeSRPDF"
        Me.lblPSDNarrativeSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDNarrativeSRPDF.TabIndex = 83
        Me.lblPSDNarrativeSRPDF.Text = "Staff Responsible"
        Me.lblPSDNarrativeSRPDF.Visible = False
        '
        'lblPSDNarrativeSRDoc
        '
        Me.lblPSDNarrativeSRDoc.AutoSize = True
        Me.lblPSDNarrativeSRDoc.Location = New System.Drawing.Point(135, 138)
        Me.lblPSDNarrativeSRDoc.Name = "lblPSDNarrativeSRDoc"
        Me.lblPSDNarrativeSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDNarrativeSRDoc.TabIndex = 81
        Me.lblPSDNarrativeSRDoc.Text = "Staff Responsible"
        Me.lblPSDNarrativeSRDoc.Visible = False
        '
        'btnPSDPublicNoticeDownload
        '
        Me.btnPSDPublicNoticeDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnPSDPublicNoticeDownload.Location = New System.Drawing.Point(623, 225)
        Me.btnPSDPublicNoticeDownload.Name = "btnPSDPublicNoticeDownload"
        Me.btnPSDPublicNoticeDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnPSDPublicNoticeDownload.TabIndex = 20
        Me.btnPSDPublicNoticeDownload.UseVisualStyleBackColor = True
        Me.btnPSDPublicNoticeDownload.Visible = False
        '
        'btnPSDDraftPermitDownload
        '
        Me.btnPSDDraftPermitDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnPSDDraftPermitDownload.Location = New System.Drawing.Point(623, 169)
        Me.btnPSDDraftPermitDownload.Name = "btnPSDDraftPermitDownload"
        Me.btnPSDDraftPermitDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnPSDDraftPermitDownload.TabIndex = 16
        Me.btnPSDDraftPermitDownload.UseVisualStyleBackColor = True
        Me.btnPSDDraftPermitDownload.Visible = False
        '
        'btnPSDPrelimDetDownload
        '
        Me.btnPSDPrelimDetDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnPSDPrelimDetDownload.Location = New System.Drawing.Point(623, 55)
        Me.btnPSDPrelimDetDownload.Name = "btnPSDPrelimDetDownload"
        Me.btnPSDPrelimDetDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnPSDPrelimDetDownload.TabIndex = 7
        Me.btnPSDPrelimDetDownload.UseVisualStyleBackColor = True
        Me.btnPSDPrelimDetDownload.Visible = False
        '
        'btnPSDHearingNoticeDownload
        '
        Me.btnPSDHearingNoticeDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnPSDHearingNoticeDownload.Location = New System.Drawing.Point(623, 280)
        Me.btnPSDHearingNoticeDownload.Name = "btnPSDHearingNoticeDownload"
        Me.btnPSDHearingNoticeDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnPSDHearingNoticeDownload.TabIndex = 24
        Me.btnPSDHearingNoticeDownload.UseVisualStyleBackColor = True
        Me.btnPSDHearingNoticeDownload.Visible = False
        '
        'btnPSDFinalDetDownload
        '
        Me.btnPSDFinalDetDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnPSDFinalDetDownload.Location = New System.Drawing.Point(623, 332)
        Me.btnPSDFinalDetDownload.Name = "btnPSDFinalDetDownload"
        Me.btnPSDFinalDetDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnPSDFinalDetDownload.TabIndex = 28
        Me.btnPSDFinalDetDownload.UseVisualStyleBackColor = True
        Me.btnPSDFinalDetDownload.Visible = False
        '
        'btnPSDFinalPermitDownload
        '
        Me.btnPSDFinalPermitDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnPSDFinalPermitDownload.Location = New System.Drawing.Point(623, 387)
        Me.btnPSDFinalPermitDownload.Name = "btnPSDFinalPermitDownload"
        Me.btnPSDFinalPermitDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnPSDFinalPermitDownload.TabIndex = 32
        Me.btnPSDFinalPermitDownload.UseVisualStyleBackColor = True
        Me.btnPSDFinalPermitDownload.Visible = False
        '
        'btnPSDAppSummaryDownload
        '
        Me.btnPSDAppSummaryDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnPSDAppSummaryDownload.Location = New System.Drawing.Point(623, 5)
        Me.btnPSDAppSummaryDownload.Name = "btnPSDAppSummaryDownload"
        Me.btnPSDAppSummaryDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnPSDAppSummaryDownload.TabIndex = 2
        Me.btnPSDAppSummaryDownload.UseVisualStyleBackColor = True
        Me.btnPSDAppSummaryDownload.Visible = False
        '
        'lblPSDFinalPermitDUPDF
        '
        Me.lblPSDFinalPermitDUPDF.AutoSize = True
        Me.lblPSDFinalPermitDUPDF.Location = New System.Drawing.Point(386, 424)
        Me.lblPSDFinalPermitDUPDF.Name = "lblPSDFinalPermitDUPDF"
        Me.lblPSDFinalPermitDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDFinalPermitDUPDF.TabIndex = 60
        Me.lblPSDFinalPermitDUPDF.Text = "DateUploaded"
        Me.lblPSDFinalPermitDUPDF.Visible = False
        '
        'lblPSDFinalPermitSRPDF
        '
        Me.lblPSDFinalPermitSRPDF.AutoSize = True
        Me.lblPSDFinalPermitSRPDF.Location = New System.Drawing.Point(385, 409)
        Me.lblPSDFinalPermitSRPDF.Name = "lblPSDFinalPermitSRPDF"
        Me.lblPSDFinalPermitSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDFinalPermitSRPDF.TabIndex = 59
        Me.lblPSDFinalPermitSRPDF.Text = "Staff Responsible"
        Me.lblPSDFinalPermitSRPDF.Visible = False
        '
        'lblPSDFinalPermitDUDoc
        '
        Me.lblPSDFinalPermitDUDoc.AutoSize = True
        Me.lblPSDFinalPermitDUDoc.Location = New System.Drawing.Point(135, 424)
        Me.lblPSDFinalPermitDUDoc.Name = "lblPSDFinalPermitDUDoc"
        Me.lblPSDFinalPermitDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDFinalPermitDUDoc.TabIndex = 58
        Me.lblPSDFinalPermitDUDoc.Text = "DateUploaded"
        Me.lblPSDFinalPermitDUDoc.Visible = False
        '
        'lblPSDFinalPermitSRDoc
        '
        Me.lblPSDFinalPermitSRDoc.AutoSize = True
        Me.lblPSDFinalPermitSRDoc.Location = New System.Drawing.Point(135, 409)
        Me.lblPSDFinalPermitSRDoc.Name = "lblPSDFinalPermitSRDoc"
        Me.lblPSDFinalPermitSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDFinalPermitSRDoc.TabIndex = 57
        Me.lblPSDFinalPermitSRDoc.Text = "Staff Responsible"
        Me.lblPSDFinalPermitSRDoc.Visible = False
        '
        'lblPSDFinalDetDUPDF
        '
        Me.lblPSDFinalDetDUPDF.AutoSize = True
        Me.lblPSDFinalDetDUPDF.Location = New System.Drawing.Point(385, 371)
        Me.lblPSDFinalDetDUPDF.Name = "lblPSDFinalDetDUPDF"
        Me.lblPSDFinalDetDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDFinalDetDUPDF.TabIndex = 56
        Me.lblPSDFinalDetDUPDF.Text = "DateUploaded"
        Me.lblPSDFinalDetDUPDF.Visible = False
        '
        'lblPSDFinalDetSRPDF
        '
        Me.lblPSDFinalDetSRPDF.AutoSize = True
        Me.lblPSDFinalDetSRPDF.Location = New System.Drawing.Point(385, 355)
        Me.lblPSDFinalDetSRPDF.Name = "lblPSDFinalDetSRPDF"
        Me.lblPSDFinalDetSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDFinalDetSRPDF.TabIndex = 55
        Me.lblPSDFinalDetSRPDF.Text = "Staff Responsible"
        Me.lblPSDFinalDetSRPDF.Visible = False
        '
        'lblPSDFinalDetDUDoc
        '
        Me.lblPSDFinalDetDUDoc.AutoSize = True
        Me.lblPSDFinalDetDUDoc.Location = New System.Drawing.Point(135, 371)
        Me.lblPSDFinalDetDUDoc.Name = "lblPSDFinalDetDUDoc"
        Me.lblPSDFinalDetDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDFinalDetDUDoc.TabIndex = 54
        Me.lblPSDFinalDetDUDoc.Text = "DateUploaded"
        Me.lblPSDFinalDetDUDoc.Visible = False
        '
        'lblPSDFinalDetSRDoc
        '
        Me.lblPSDFinalDetSRDoc.AutoSize = True
        Me.lblPSDFinalDetSRDoc.Location = New System.Drawing.Point(135, 355)
        Me.lblPSDFinalDetSRDoc.Name = "lblPSDFinalDetSRDoc"
        Me.lblPSDFinalDetSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDFinalDetSRDoc.TabIndex = 53
        Me.lblPSDFinalDetSRDoc.Text = "Staff Responsible"
        Me.lblPSDFinalDetSRDoc.Visible = False
        '
        'lblPSDHearingNoticeDUPDF
        '
        Me.lblPSDHearingNoticeDUPDF.AutoSize = True
        Me.lblPSDHearingNoticeDUPDF.Location = New System.Drawing.Point(385, 318)
        Me.lblPSDHearingNoticeDUPDF.Name = "lblPSDHearingNoticeDUPDF"
        Me.lblPSDHearingNoticeDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDHearingNoticeDUPDF.TabIndex = 52
        Me.lblPSDHearingNoticeDUPDF.Text = "DateUploaded"
        Me.lblPSDHearingNoticeDUPDF.Visible = False
        '
        'lblPSDHearingNoticeSRPDF
        '
        Me.lblPSDHearingNoticeSRPDF.AutoSize = True
        Me.lblPSDHearingNoticeSRPDF.Location = New System.Drawing.Point(385, 303)
        Me.lblPSDHearingNoticeSRPDF.Name = "lblPSDHearingNoticeSRPDF"
        Me.lblPSDHearingNoticeSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDHearingNoticeSRPDF.TabIndex = 51
        Me.lblPSDHearingNoticeSRPDF.Text = "Staff Responsible"
        Me.lblPSDHearingNoticeSRPDF.Visible = False
        '
        'lblPSDHearingNoticeDUDoc
        '
        Me.lblPSDHearingNoticeDUDoc.AutoSize = True
        Me.lblPSDHearingNoticeDUDoc.Location = New System.Drawing.Point(135, 318)
        Me.lblPSDHearingNoticeDUDoc.Name = "lblPSDHearingNoticeDUDoc"
        Me.lblPSDHearingNoticeDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDHearingNoticeDUDoc.TabIndex = 50
        Me.lblPSDHearingNoticeDUDoc.Text = "DateUploaded"
        Me.lblPSDHearingNoticeDUDoc.Visible = False
        '
        'lblPSDHearingNoticeSRDoc
        '
        Me.lblPSDHearingNoticeSRDoc.AutoSize = True
        Me.lblPSDHearingNoticeSRDoc.Location = New System.Drawing.Point(135, 303)
        Me.lblPSDHearingNoticeSRDoc.Name = "lblPSDHearingNoticeSRDoc"
        Me.lblPSDHearingNoticeSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDHearingNoticeSRDoc.TabIndex = 49
        Me.lblPSDHearingNoticeSRDoc.Text = "Staff Responsible"
        Me.lblPSDHearingNoticeSRDoc.Visible = False
        '
        'lblPSDPublicNoticeDUPDF
        '
        Me.lblPSDPublicNoticeDUPDF.AutoSize = True
        Me.lblPSDPublicNoticeDUPDF.Location = New System.Drawing.Point(385, 265)
        Me.lblPSDPublicNoticeDUPDF.Name = "lblPSDPublicNoticeDUPDF"
        Me.lblPSDPublicNoticeDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDPublicNoticeDUPDF.TabIndex = 48
        Me.lblPSDPublicNoticeDUPDF.Text = "DateUploaded"
        Me.lblPSDPublicNoticeDUPDF.Visible = False
        '
        'lblPSDPublicNoticeSRPDF
        '
        Me.lblPSDPublicNoticeSRPDF.AutoSize = True
        Me.lblPSDPublicNoticeSRPDF.Location = New System.Drawing.Point(385, 249)
        Me.lblPSDPublicNoticeSRPDF.Name = "lblPSDPublicNoticeSRPDF"
        Me.lblPSDPublicNoticeSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDPublicNoticeSRPDF.TabIndex = 47
        Me.lblPSDPublicNoticeSRPDF.Text = "Staff Responsible"
        Me.lblPSDPublicNoticeSRPDF.Visible = False
        '
        'lblPSDPublicNoticeDUDoc
        '
        Me.lblPSDPublicNoticeDUDoc.AutoSize = True
        Me.lblPSDPublicNoticeDUDoc.Location = New System.Drawing.Point(135, 265)
        Me.lblPSDPublicNoticeDUDoc.Name = "lblPSDPublicNoticeDUDoc"
        Me.lblPSDPublicNoticeDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDPublicNoticeDUDoc.TabIndex = 46
        Me.lblPSDPublicNoticeDUDoc.Text = "DateUploaded"
        Me.lblPSDPublicNoticeDUDoc.Visible = False
        '
        'lblPSDPublicNoticeSRDoc
        '
        Me.lblPSDPublicNoticeSRDoc.AutoSize = True
        Me.lblPSDPublicNoticeSRDoc.Location = New System.Drawing.Point(135, 249)
        Me.lblPSDPublicNoticeSRDoc.Name = "lblPSDPublicNoticeSRDoc"
        Me.lblPSDPublicNoticeSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDPublicNoticeSRDoc.TabIndex = 45
        Me.lblPSDPublicNoticeSRDoc.Text = "Staff Responsible"
        Me.lblPSDPublicNoticeSRDoc.Visible = False
        '
        'lblPSDDraftPermitDUPDF
        '
        Me.lblPSDDraftPermitDUPDF.AutoSize = True
        Me.lblPSDDraftPermitDUPDF.Location = New System.Drawing.Point(385, 211)
        Me.lblPSDDraftPermitDUPDF.Name = "lblPSDDraftPermitDUPDF"
        Me.lblPSDDraftPermitDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDDraftPermitDUPDF.TabIndex = 44
        Me.lblPSDDraftPermitDUPDF.Text = "DateUploaded"
        Me.lblPSDDraftPermitDUPDF.Visible = False
        '
        'lblPSDDraftPermitSRPDF
        '
        Me.lblPSDDraftPermitSRPDF.AutoSize = True
        Me.lblPSDDraftPermitSRPDF.Location = New System.Drawing.Point(385, 195)
        Me.lblPSDDraftPermitSRPDF.Name = "lblPSDDraftPermitSRPDF"
        Me.lblPSDDraftPermitSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDDraftPermitSRPDF.TabIndex = 43
        Me.lblPSDDraftPermitSRPDF.Text = "Staff Responsible"
        Me.lblPSDDraftPermitSRPDF.Visible = False
        '
        'lblPSDDraftPermitDUDoc
        '
        Me.lblPSDDraftPermitDUDoc.AutoSize = True
        Me.lblPSDDraftPermitDUDoc.Location = New System.Drawing.Point(135, 211)
        Me.lblPSDDraftPermitDUDoc.Name = "lblPSDDraftPermitDUDoc"
        Me.lblPSDDraftPermitDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDDraftPermitDUDoc.TabIndex = 42
        Me.lblPSDDraftPermitDUDoc.Text = "DateUploaded"
        Me.lblPSDDraftPermitDUDoc.Visible = False
        '
        'lblPSDDraftPermitSRDoc
        '
        Me.lblPSDDraftPermitSRDoc.AutoSize = True
        Me.lblPSDDraftPermitSRDoc.Location = New System.Drawing.Point(135, 195)
        Me.lblPSDDraftPermitSRDoc.Name = "lblPSDDraftPermitSRDoc"
        Me.lblPSDDraftPermitSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDDraftPermitSRDoc.TabIndex = 41
        Me.lblPSDDraftPermitSRDoc.Text = "Staff Responsible"
        Me.lblPSDDraftPermitSRDoc.Visible = False
        '
        'lblPSDPrelimDetDUPDF
        '
        Me.lblPSDPrelimDetDUPDF.AutoSize = True
        Me.lblPSDPrelimDetDUPDF.Location = New System.Drawing.Point(385, 98)
        Me.lblPSDPrelimDetDUPDF.Name = "lblPSDPrelimDetDUPDF"
        Me.lblPSDPrelimDetDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDPrelimDetDUPDF.TabIndex = 40
        Me.lblPSDPrelimDetDUPDF.Text = "DateUploaded"
        Me.lblPSDPrelimDetDUPDF.Visible = False
        '
        'lblPSDPrelimDetSRPDF
        '
        Me.lblPSDPrelimDetSRPDF.AutoSize = True
        Me.lblPSDPrelimDetSRPDF.Location = New System.Drawing.Point(385, 81)
        Me.lblPSDPrelimDetSRPDF.Name = "lblPSDPrelimDetSRPDF"
        Me.lblPSDPrelimDetSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDPrelimDetSRPDF.TabIndex = 39
        Me.lblPSDPrelimDetSRPDF.Text = "Staff Responsible"
        Me.lblPSDPrelimDetSRPDF.Visible = False
        '
        'lblPSDPrelimDetDUDoc
        '
        Me.lblPSDPrelimDetDUDoc.AutoSize = True
        Me.lblPSDPrelimDetDUDoc.Location = New System.Drawing.Point(135, 98)
        Me.lblPSDPrelimDetDUDoc.Name = "lblPSDPrelimDetDUDoc"
        Me.lblPSDPrelimDetDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDPrelimDetDUDoc.TabIndex = 38
        Me.lblPSDPrelimDetDUDoc.Text = "DateUploaded"
        Me.lblPSDPrelimDetDUDoc.Visible = False
        '
        'lblPSDPrelimDetSRDoc
        '
        Me.lblPSDPrelimDetSRDoc.AutoSize = True
        Me.lblPSDPrelimDetSRDoc.Location = New System.Drawing.Point(135, 81)
        Me.lblPSDPrelimDetSRDoc.Name = "lblPSDPrelimDetSRDoc"
        Me.lblPSDPrelimDetSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDPrelimDetSRDoc.TabIndex = 37
        Me.lblPSDPrelimDetSRDoc.Text = "Staff Responsible"
        Me.lblPSDPrelimDetSRDoc.Visible = False
        '
        'lblPSDAppSummaryDUPDF
        '
        Me.lblPSDAppSummaryDUPDF.AutoSize = True
        Me.lblPSDAppSummaryDUPDF.Location = New System.Drawing.Point(385, 42)
        Me.lblPSDAppSummaryDUPDF.Name = "lblPSDAppSummaryDUPDF"
        Me.lblPSDAppSummaryDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDAppSummaryDUPDF.TabIndex = 36
        Me.lblPSDAppSummaryDUPDF.Text = "DateUploaded"
        Me.lblPSDAppSummaryDUPDF.Visible = False
        '
        'lblPSDAppSummarySRPDF
        '
        Me.lblPSDAppSummarySRPDF.AutoSize = True
        Me.lblPSDAppSummarySRPDF.Location = New System.Drawing.Point(385, 28)
        Me.lblPSDAppSummarySRPDF.Name = "lblPSDAppSummarySRPDF"
        Me.lblPSDAppSummarySRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDAppSummarySRPDF.TabIndex = 35
        Me.lblPSDAppSummarySRPDF.Text = "Staff Responsible"
        Me.lblPSDAppSummarySRPDF.Visible = False
        '
        'lblPSDAppSummaryDUDoc
        '
        Me.lblPSDAppSummaryDUDoc.AutoSize = True
        Me.lblPSDAppSummaryDUDoc.Location = New System.Drawing.Point(135, 43)
        Me.lblPSDAppSummaryDUDoc.Name = "lblPSDAppSummaryDUDoc"
        Me.lblPSDAppSummaryDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblPSDAppSummaryDUDoc.TabIndex = 34
        Me.lblPSDAppSummaryDUDoc.Text = "DateUploaded"
        Me.lblPSDAppSummaryDUDoc.Visible = False
        '
        'lblPSDAppSummarySRDoc
        '
        Me.lblPSDAppSummarySRDoc.AutoSize = True
        Me.lblPSDAppSummarySRDoc.Location = New System.Drawing.Point(135, 28)
        Me.lblPSDAppSummarySRDoc.Name = "lblPSDAppSummarySRDoc"
        Me.lblPSDAppSummarySRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblPSDAppSummarySRDoc.TabIndex = 33
        Me.lblPSDAppSummarySRDoc.Text = "Staff Responsible"
        Me.lblPSDAppSummarySRDoc.Visible = False
        '
        'txtPSDPrelimDetPDF
        '
        Me.txtPSDPrelimDetPDF.Location = New System.Drawing.Point(376, 59)
        Me.txtPSDPrelimDetPDF.Name = "txtPSDPrelimDetPDF"
        Me.txtPSDPrelimDetPDF.ReadOnly = True
        Me.txtPSDPrelimDetPDF.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDPrelimDetPDF.TabIndex = 6
        Me.txtPSDPrelimDetPDF.Visible = False
        '
        'txtPSDDraftPermitPDF
        '
        Me.txtPSDDraftPermitPDF.Location = New System.Drawing.Point(376, 172)
        Me.txtPSDDraftPermitPDF.Name = "txtPSDDraftPermitPDF"
        Me.txtPSDDraftPermitPDF.ReadOnly = True
        Me.txtPSDDraftPermitPDF.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDDraftPermitPDF.TabIndex = 15
        Me.txtPSDDraftPermitPDF.Visible = False
        '
        'txtPSDPublicNoticePDF
        '
        Me.txtPSDPublicNoticePDF.Location = New System.Drawing.Point(376, 226)
        Me.txtPSDPublicNoticePDF.Name = "txtPSDPublicNoticePDF"
        Me.txtPSDPublicNoticePDF.ReadOnly = True
        Me.txtPSDPublicNoticePDF.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDPublicNoticePDF.TabIndex = 19
        Me.txtPSDPublicNoticePDF.Visible = False
        '
        'txtPSDHearingNoticePDF
        '
        Me.txtPSDHearingNoticePDF.Location = New System.Drawing.Point(376, 282)
        Me.txtPSDHearingNoticePDF.Name = "txtPSDHearingNoticePDF"
        Me.txtPSDHearingNoticePDF.ReadOnly = True
        Me.txtPSDHearingNoticePDF.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDHearingNoticePDF.TabIndex = 23
        Me.txtPSDHearingNoticePDF.Visible = False
        '
        'txtPSDFinalDetPDF
        '
        Me.txtPSDFinalDetPDF.Location = New System.Drawing.Point(376, 334)
        Me.txtPSDFinalDetPDF.Name = "txtPSDFinalDetPDF"
        Me.txtPSDFinalDetPDF.ReadOnly = True
        Me.txtPSDFinalDetPDF.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDFinalDetPDF.TabIndex = 27
        Me.txtPSDFinalDetPDF.Visible = False
        '
        'txtPSDAppSummaryPDF
        '
        Me.txtPSDAppSummaryPDF.Location = New System.Drawing.Point(376, 5)
        Me.txtPSDAppSummaryPDF.Name = "txtPSDAppSummaryPDF"
        Me.txtPSDAppSummaryPDF.ReadOnly = True
        Me.txtPSDAppSummaryPDF.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDAppSummaryPDF.TabIndex = 1
        Me.txtPSDAppSummaryPDF.Visible = False
        '
        'txtPSDFinalPermitPDF
        '
        Me.txtPSDFinalPermitPDF.Location = New System.Drawing.Point(376, 387)
        Me.txtPSDFinalPermitPDF.Name = "txtPSDFinalPermitPDF"
        Me.txtPSDFinalPermitPDF.ReadOnly = True
        Me.txtPSDFinalPermitPDF.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDFinalPermitPDF.TabIndex = 31
        Me.txtPSDFinalPermitPDF.Visible = False
        '
        'txtPSDFinalPermitDoc
        '
        Me.txtPSDFinalPermitDoc.Location = New System.Drawing.Point(129, 387)
        Me.txtPSDFinalPermitDoc.Name = "txtPSDFinalPermitDoc"
        Me.txtPSDFinalPermitDoc.ReadOnly = True
        Me.txtPSDFinalPermitDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDFinalPermitDoc.TabIndex = 30
        Me.txtPSDFinalPermitDoc.Visible = False
        '
        'chbPSDFinalPermit
        '
        Me.chbPSDFinalPermit.AutoSize = True
        Me.chbPSDFinalPermit.Location = New System.Drawing.Point(4, 389)
        Me.chbPSDFinalPermit.Name = "chbPSDFinalPermit"
        Me.chbPSDFinalPermit.Size = New System.Drawing.Size(80, 17)
        Me.chbPSDFinalPermit.TabIndex = 29
        Me.chbPSDFinalPermit.Text = "Final Permit"
        Me.chbPSDFinalPermit.UseVisualStyleBackColor = True
        '
        'txtPSDFinalDetDoc
        '
        Me.txtPSDFinalDetDoc.Location = New System.Drawing.Point(129, 334)
        Me.txtPSDFinalDetDoc.Name = "txtPSDFinalDetDoc"
        Me.txtPSDFinalDetDoc.ReadOnly = True
        Me.txtPSDFinalDetDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDFinalDetDoc.TabIndex = 26
        Me.txtPSDFinalDetDoc.Visible = False
        '
        'chbPSDFinalDet
        '
        Me.chbPSDFinalDet.AutoSize = True
        Me.chbPSDFinalDet.Location = New System.Drawing.Point(4, 337)
        Me.chbPSDFinalDet.Name = "chbPSDFinalDet"
        Me.chbPSDFinalDet.Size = New System.Drawing.Size(71, 17)
        Me.chbPSDFinalDet.TabIndex = 25
        Me.chbPSDFinalDet.Text = "Final Det."
        Me.chbPSDFinalDet.UseVisualStyleBackColor = True
        '
        'txtPSDHearingNoticeDoc
        '
        Me.txtPSDHearingNoticeDoc.Location = New System.Drawing.Point(129, 282)
        Me.txtPSDHearingNoticeDoc.Name = "txtPSDHearingNoticeDoc"
        Me.txtPSDHearingNoticeDoc.ReadOnly = True
        Me.txtPSDHearingNoticeDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDHearingNoticeDoc.TabIndex = 22
        Me.txtPSDHearingNoticeDoc.Visible = False
        '
        'chbPSDHearingNotice
        '
        Me.chbPSDHearingNotice.AutoSize = True
        Me.chbPSDHearingNotice.Location = New System.Drawing.Point(4, 284)
        Me.chbPSDHearingNotice.Name = "chbPSDHearingNotice"
        Me.chbPSDHearingNotice.Size = New System.Drawing.Size(97, 17)
        Me.chbPSDHearingNotice.TabIndex = 21
        Me.chbPSDHearingNotice.Text = "Hearing Notice"
        Me.chbPSDHearingNotice.UseVisualStyleBackColor = True
        '
        'txtPSDPublicNoticeDoc
        '
        Me.txtPSDPublicNoticeDoc.Location = New System.Drawing.Point(129, 227)
        Me.txtPSDPublicNoticeDoc.Name = "txtPSDPublicNoticeDoc"
        Me.txtPSDPublicNoticeDoc.ReadOnly = True
        Me.txtPSDPublicNoticeDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDPublicNoticeDoc.TabIndex = 18
        Me.txtPSDPublicNoticeDoc.Visible = False
        '
        'txtPSDDraftPermitDoc
        '
        Me.txtPSDDraftPermitDoc.Location = New System.Drawing.Point(129, 173)
        Me.txtPSDDraftPermitDoc.Name = "txtPSDDraftPermitDoc"
        Me.txtPSDDraftPermitDoc.ReadOnly = True
        Me.txtPSDDraftPermitDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDDraftPermitDoc.TabIndex = 14
        Me.txtPSDDraftPermitDoc.Visible = False
        '
        'txtPSDPrelimDetDoc
        '
        Me.txtPSDPrelimDetDoc.Location = New System.Drawing.Point(129, 59)
        Me.txtPSDPrelimDetDoc.Name = "txtPSDPrelimDetDoc"
        Me.txtPSDPrelimDetDoc.ReadOnly = True
        Me.txtPSDPrelimDetDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDPrelimDetDoc.TabIndex = 5
        Me.txtPSDPrelimDetDoc.Visible = False
        '
        'txtPSDAppSummaryDoc
        '
        Me.txtPSDAppSummaryDoc.Location = New System.Drawing.Point(129, 5)
        Me.txtPSDAppSummaryDoc.Name = "txtPSDAppSummaryDoc"
        Me.txtPSDAppSummaryDoc.ReadOnly = True
        Me.txtPSDAppSummaryDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtPSDAppSummaryDoc.TabIndex = 0
        Me.txtPSDAppSummaryDoc.Visible = False
        '
        'chbPSDPublicNotice
        '
        Me.chbPSDPublicNotice.AutoSize = True
        Me.chbPSDPublicNotice.Location = New System.Drawing.Point(4, 229)
        Me.chbPSDPublicNotice.Name = "chbPSDPublicNotice"
        Me.chbPSDPublicNotice.Size = New System.Drawing.Size(89, 17)
        Me.chbPSDPublicNotice.TabIndex = 17
        Me.chbPSDPublicNotice.Text = "Public Notice"
        Me.chbPSDPublicNotice.UseVisualStyleBackColor = True
        '
        'chbPSDDraftPermit
        '
        Me.chbPSDDraftPermit.AutoSize = True
        Me.chbPSDDraftPermit.Location = New System.Drawing.Point(4, 175)
        Me.chbPSDDraftPermit.Name = "chbPSDDraftPermit"
        Me.chbPSDDraftPermit.Size = New System.Drawing.Size(81, 17)
        Me.chbPSDDraftPermit.TabIndex = 12
        Me.chbPSDDraftPermit.Text = "Draft Permit"
        Me.chbPSDDraftPermit.UseVisualStyleBackColor = True
        '
        'chbPSDPrelimDet
        '
        Me.chbPSDPrelimDet.AutoSize = True
        Me.chbPSDPrelimDet.Location = New System.Drawing.Point(4, 61)
        Me.chbPSDPrelimDet.Name = "chbPSDPrelimDet"
        Me.chbPSDPrelimDet.Size = New System.Drawing.Size(77, 17)
        Me.chbPSDPrelimDet.TabIndex = 4
        Me.chbPSDPrelimDet.Text = "Prelim. Det"
        Me.chbPSDPrelimDet.UseVisualStyleBackColor = True
        '
        'chbPSDApplicationSummary
        '
        Me.chbPSDApplicationSummary.AutoSize = True
        Me.chbPSDApplicationSummary.Location = New System.Drawing.Point(4, 7)
        Me.chbPSDApplicationSummary.Name = "chbPSDApplicationSummary"
        Me.chbPSDApplicationSummary.Size = New System.Drawing.Size(94, 17)
        Me.chbPSDApplicationSummary.TabIndex = 3
        Me.chbPSDApplicationSummary.Text = "App. Summary"
        Me.chbPSDApplicationSummary.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.AutoSize = True
        Me.Panel7.Controls.Add(Me.rdbOtherPermit)
        Me.Panel7.Controls.Add(Me.rdbPSDPermit)
        Me.Panel7.Controls.Add(Me.rdbTitleVPermit)
        Me.Panel7.Location = New System.Drawing.Point(15, 23)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(83, 69)
        Me.Panel7.TabIndex = 0
        '
        'rdbOtherPermit
        '
        Me.rdbOtherPermit.AutoSize = True
        Me.rdbOtherPermit.Location = New System.Drawing.Point(3, 49)
        Me.rdbOtherPermit.Name = "rdbOtherPermit"
        Me.rdbOtherPermit.Size = New System.Drawing.Size(77, 17)
        Me.rdbOtherPermit.TabIndex = 2
        Me.rdbOtherPermit.TabStop = True
        Me.rdbOtherPermit.Text = "Other (SIP)"
        Me.rdbOtherPermit.UseVisualStyleBackColor = True
        '
        'rdbPSDPermit
        '
        Me.rdbPSDPermit.AutoSize = True
        Me.rdbPSDPermit.Location = New System.Drawing.Point(3, 26)
        Me.rdbPSDPermit.Name = "rdbPSDPermit"
        Me.rdbPSDPermit.Size = New System.Drawing.Size(75, 17)
        Me.rdbPSDPermit.TabIndex = 1
        Me.rdbPSDPermit.TabStop = True
        Me.rdbPSDPermit.Text = "PSD/NSR"
        Me.rdbPSDPermit.UseVisualStyleBackColor = True
        '
        'rdbTitleVPermit
        '
        Me.rdbTitleVPermit.AutoSize = True
        Me.rdbTitleVPermit.Location = New System.Drawing.Point(3, 3)
        Me.rdbTitleVPermit.Name = "rdbTitleVPermit"
        Me.rdbTitleVPermit.Size = New System.Drawing.Size(55, 17)
        Me.rdbTitleVPermit.TabIndex = 0
        Me.rdbTitleVPermit.TabStop = True
        Me.rdbTitleVPermit.Text = "Title V"
        Me.rdbTitleVPermit.UseVisualStyleBackColor = True
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(4, 8)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(63, 13)
        Me.Label47.TabIndex = 12
        Me.Label47.Text = "Permit Type"
        '
        'lblPDF
        '
        Me.lblPDF.AutoSize = True
        Me.lblPDF.Location = New System.Drawing.Point(474, 9)
        Me.lblPDF.Name = "lblPDF"
        Me.lblPDF.Size = New System.Drawing.Size(47, 13)
        Me.lblPDF.TabIndex = 21
        Me.lblPDF.Text = "PDF File"
        Me.lblPDF.Visible = False
        '
        'lblWord
        '
        Me.lblWord.AutoSize = True
        Me.lblWord.Location = New System.Drawing.Point(227, 9)
        Me.lblWord.Name = "lblWord"
        Me.lblWord.Size = New System.Drawing.Size(52, 13)
        Me.lblWord.TabIndex = 20
        Me.lblWord.Text = "Word File"
        Me.lblWord.Visible = False
        '
        'PanelOther
        '
        Me.PanelOther.AutoSize = True
        Me.PanelOther.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelOther.Controls.Add(Me.btnOtherPermitDownload)
        Me.PanelOther.Controls.Add(Me.btnOtherNarrativeDownload)
        Me.PanelOther.Controls.Add(Me.lblOtherPermitDUPDF)
        Me.PanelOther.Controls.Add(Me.lblOtherPermitSRPDF)
        Me.PanelOther.Controls.Add(Me.lblOtherPermitDUDoc)
        Me.PanelOther.Controls.Add(Me.lblOtherPermitSRDoc)
        Me.PanelOther.Controls.Add(Me.lblOtherNarrativeDUPDF)
        Me.PanelOther.Controls.Add(Me.lblOtherNarrativeSRPDF)
        Me.PanelOther.Controls.Add(Me.lblOtherNarrativeDUDoc)
        Me.PanelOther.Controls.Add(Me.lblOtherNarrativeSRDoc)
        Me.PanelOther.Controls.Add(Me.txtOtherNarrativePDF)
        Me.PanelOther.Controls.Add(Me.txtOtherPermitPDF)
        Me.PanelOther.Controls.Add(Me.txtOtherNarrativeDoc)
        Me.PanelOther.Controls.Add(Me.chbOtherNarrative)
        Me.PanelOther.Controls.Add(Me.txtOtherPermitDoc)
        Me.PanelOther.Controls.Add(Me.chbOtherPermit)
        Me.PanelOther.Location = New System.Drawing.Point(100, 25)
        Me.PanelOther.Name = "PanelOther"
        Me.PanelOther.Size = New System.Drawing.Size(652, 114)
        Me.PanelOther.TabIndex = 17
        Me.PanelOther.Visible = False
        '
        'btnOtherPermitDownload
        '
        Me.btnOtherPermitDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnOtherPermitDownload.Location = New System.Drawing.Point(623, 59)
        Me.btnOtherPermitDownload.Name = "btnOtherPermitDownload"
        Me.btnOtherPermitDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnOtherPermitDownload.TabIndex = 30
        Me.btnOtherPermitDownload.UseVisualStyleBackColor = True
        Me.btnOtherPermitDownload.Visible = False
        '
        'btnOtherNarrativeDownload
        '
        Me.btnOtherNarrativeDownload.Image = Global.Iaip.My.Resources.Resources.DownloadIcon
        Me.btnOtherNarrativeDownload.Location = New System.Drawing.Point(623, 4)
        Me.btnOtherNarrativeDownload.Name = "btnOtherNarrativeDownload"
        Me.btnOtherNarrativeDownload.Size = New System.Drawing.Size(24, 23)
        Me.btnOtherNarrativeDownload.TabIndex = 29
        Me.btnOtherNarrativeDownload.UseVisualStyleBackColor = True
        Me.btnOtherNarrativeDownload.Visible = False
        '
        'lblOtherPermitDUPDF
        '
        Me.lblOtherPermitDUPDF.AutoSize = True
        Me.lblOtherPermitDUPDF.Location = New System.Drawing.Point(386, 99)
        Me.lblOtherPermitDUPDF.Name = "lblOtherPermitDUPDF"
        Me.lblOtherPermitDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblOtherPermitDUPDF.TabIndex = 28
        Me.lblOtherPermitDUPDF.Text = "DateUploaded"
        Me.lblOtherPermitDUPDF.Visible = False
        '
        'lblOtherPermitSRPDF
        '
        Me.lblOtherPermitSRPDF.AutoSize = True
        Me.lblOtherPermitSRPDF.Location = New System.Drawing.Point(386, 85)
        Me.lblOtherPermitSRPDF.Name = "lblOtherPermitSRPDF"
        Me.lblOtherPermitSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblOtherPermitSRPDF.TabIndex = 27
        Me.lblOtherPermitSRPDF.Text = "Staff Responsible"
        Me.lblOtherPermitSRPDF.Visible = False
        '
        'lblOtherPermitDUDoc
        '
        Me.lblOtherPermitDUDoc.AutoSize = True
        Me.lblOtherPermitDUDoc.Location = New System.Drawing.Point(135, 99)
        Me.lblOtherPermitDUDoc.Name = "lblOtherPermitDUDoc"
        Me.lblOtherPermitDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblOtherPermitDUDoc.TabIndex = 26
        Me.lblOtherPermitDUDoc.Text = "DateUploaded"
        Me.lblOtherPermitDUDoc.Visible = False
        '
        'lblOtherPermitSRDoc
        '
        Me.lblOtherPermitSRDoc.AutoSize = True
        Me.lblOtherPermitSRDoc.Location = New System.Drawing.Point(135, 85)
        Me.lblOtherPermitSRDoc.Name = "lblOtherPermitSRDoc"
        Me.lblOtherPermitSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblOtherPermitSRDoc.TabIndex = 25
        Me.lblOtherPermitSRDoc.Text = "Staff Responsible"
        Me.lblOtherPermitSRDoc.Visible = False
        '
        'lblOtherNarrativeDUPDF
        '
        Me.lblOtherNarrativeDUPDF.AutoSize = True
        Me.lblOtherNarrativeDUPDF.Location = New System.Drawing.Point(387, 42)
        Me.lblOtherNarrativeDUPDF.Name = "lblOtherNarrativeDUPDF"
        Me.lblOtherNarrativeDUPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblOtherNarrativeDUPDF.TabIndex = 24
        Me.lblOtherNarrativeDUPDF.Text = "DateUploaded"
        Me.lblOtherNarrativeDUPDF.Visible = False
        '
        'lblOtherNarrativeSRPDF
        '
        Me.lblOtherNarrativeSRPDF.AutoSize = True
        Me.lblOtherNarrativeSRPDF.Location = New System.Drawing.Point(385, 27)
        Me.lblOtherNarrativeSRPDF.Name = "lblOtherNarrativeSRPDF"
        Me.lblOtherNarrativeSRPDF.Size = New System.Drawing.Size(90, 13)
        Me.lblOtherNarrativeSRPDF.TabIndex = 23
        Me.lblOtherNarrativeSRPDF.Text = "Staff Responsible"
        Me.lblOtherNarrativeSRPDF.Visible = False
        '
        'lblOtherNarrativeDUDoc
        '
        Me.lblOtherNarrativeDUDoc.AutoSize = True
        Me.lblOtherNarrativeDUDoc.Location = New System.Drawing.Point(135, 44)
        Me.lblOtherNarrativeDUDoc.Name = "lblOtherNarrativeDUDoc"
        Me.lblOtherNarrativeDUDoc.Size = New System.Drawing.Size(76, 13)
        Me.lblOtherNarrativeDUDoc.TabIndex = 22
        Me.lblOtherNarrativeDUDoc.Text = "DateUploaded"
        Me.lblOtherNarrativeDUDoc.Visible = False
        '
        'lblOtherNarrativeSRDoc
        '
        Me.lblOtherNarrativeSRDoc.AutoSize = True
        Me.lblOtherNarrativeSRDoc.Location = New System.Drawing.Point(135, 29)
        Me.lblOtherNarrativeSRDoc.Name = "lblOtherNarrativeSRDoc"
        Me.lblOtherNarrativeSRDoc.Size = New System.Drawing.Size(90, 13)
        Me.lblOtherNarrativeSRDoc.TabIndex = 21
        Me.lblOtherNarrativeSRDoc.Text = "Staff Responsible"
        Me.lblOtherNarrativeSRDoc.Visible = False
        '
        'txtOtherNarrativePDF
        '
        Me.txtOtherNarrativePDF.Location = New System.Drawing.Point(376, 5)
        Me.txtOtherNarrativePDF.Name = "txtOtherNarrativePDF"
        Me.txtOtherNarrativePDF.ReadOnly = True
        Me.txtOtherNarrativePDF.Size = New System.Drawing.Size(241, 20)
        Me.txtOtherNarrativePDF.TabIndex = 20
        Me.txtOtherNarrativePDF.Visible = False
        '
        'txtOtherPermitPDF
        '
        Me.txtOtherPermitPDF.Location = New System.Drawing.Point(376, 62)
        Me.txtOtherPermitPDF.Name = "txtOtherPermitPDF"
        Me.txtOtherPermitPDF.ReadOnly = True
        Me.txtOtherPermitPDF.Size = New System.Drawing.Size(241, 20)
        Me.txtOtherPermitPDF.TabIndex = 19
        Me.txtOtherPermitPDF.Visible = False
        '
        'txtOtherNarrativeDoc
        '
        Me.txtOtherNarrativeDoc.Location = New System.Drawing.Point(129, 6)
        Me.txtOtherNarrativeDoc.Name = "txtOtherNarrativeDoc"
        Me.txtOtherNarrativeDoc.ReadOnly = True
        Me.txtOtherNarrativeDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtOtherNarrativeDoc.TabIndex = 17
        Me.txtOtherNarrativeDoc.Visible = False
        '
        'chbOtherNarrative
        '
        Me.chbOtherNarrative.AutoSize = True
        Me.chbOtherNarrative.Location = New System.Drawing.Point(4, 10)
        Me.chbOtherNarrative.Name = "chbOtherNarrative"
        Me.chbOtherNarrative.Size = New System.Drawing.Size(69, 17)
        Me.chbOtherNarrative.TabIndex = 0
        Me.chbOtherNarrative.Text = "Narrative"
        Me.chbOtherNarrative.UseVisualStyleBackColor = True
        '
        'txtOtherPermitDoc
        '
        Me.txtOtherPermitDoc.Location = New System.Drawing.Point(129, 62)
        Me.txtOtherPermitDoc.Name = "txtOtherPermitDoc"
        Me.txtOtherPermitDoc.ReadOnly = True
        Me.txtOtherPermitDoc.Size = New System.Drawing.Size(241, 20)
        Me.txtOtherPermitDoc.TabIndex = 14
        Me.txtOtherPermitDoc.Visible = False
        '
        'chbOtherPermit
        '
        Me.chbOtherPermit.AutoSize = True
        Me.chbOtherPermit.Location = New System.Drawing.Point(4, 64)
        Me.chbOtherPermit.Name = "chbOtherPermit"
        Me.chbOtherPermit.Size = New System.Drawing.Size(48, 17)
        Me.chbOtherPermit.TabIndex = 0
        Me.chbOtherPermit.Text = "Final"
        Me.chbOtherPermit.UseVisualStyleBackColor = True
        '
        'TPContactInformation
        '
        Me.TPContactInformation.Controls.Add(Me.txtContactPhoneNumber)
        Me.TPContactInformation.Controls.Add(Me.btnEmailAcknowledgmentLetter)
        Me.TPContactInformation.Controls.Add(Me.btnGetCurrentPermittingContact)
        Me.TPContactInformation.Controls.Add(Me.mtbContactZipCode)
        Me.TPContactInformation.Controls.Add(Me.mtbContactFaxNumber)
        Me.TPContactInformation.Controls.Add(Me.txtContactDescription)
        Me.TPContactInformation.Controls.Add(Me.txtContactEmailAddress)
        Me.TPContactInformation.Controls.Add(Me.txtContactState)
        Me.TPContactInformation.Controls.Add(Me.txtContactCity)
        Me.TPContactInformation.Controls.Add(Me.txtContactStreetAddress)
        Me.TPContactInformation.Controls.Add(Me.txtContactTitle)
        Me.TPContactInformation.Controls.Add(Me.txtContactCompanyName)
        Me.TPContactInformation.Controls.Add(Me.txtContactPedigree)
        Me.TPContactInformation.Controls.Add(Me.txtContactSocialTitle)
        Me.TPContactInformation.Controls.Add(Me.txtContactLastName)
        Me.TPContactInformation.Controls.Add(Me.txtContactFirstName)
        Me.TPContactInformation.Controls.Add(Me.Label46)
        Me.TPContactInformation.Controls.Add(Me.Label45)
        Me.TPContactInformation.Controls.Add(Me.Label40)
        Me.TPContactInformation.Controls.Add(Me.Label39)
        Me.TPContactInformation.Controls.Add(Me.Label44)
        Me.TPContactInformation.Controls.Add(Me.Label41)
        Me.TPContactInformation.Controls.Add(Me.Label38)
        Me.TPContactInformation.Controls.Add(Me.Label32)
        Me.TPContactInformation.Controls.Add(Me.Label27)
        Me.TPContactInformation.Controls.Add(Me.Label26)
        Me.TPContactInformation.Controls.Add(Me.Label20)
        Me.TPContactInformation.Controls.Add(Me.Label18)
        Me.TPContactInformation.Controls.Add(Me.Label12)
        Me.TPContactInformation.Controls.Add(Me.Label11)
        Me.TPContactInformation.Location = New System.Drawing.Point(4, 22)
        Me.TPContactInformation.Name = "TPContactInformation"
        Me.TPContactInformation.Size = New System.Drawing.Size(784, 477)
        Me.TPContactInformation.TabIndex = 6
        Me.TPContactInformation.Text = "Contact"
        Me.TPContactInformation.UseVisualStyleBackColor = True
        '
        'txtContactPhoneNumber
        '
        Me.txtContactPhoneNumber.Location = New System.Drawing.Point(92, 151)
        Me.txtContactPhoneNumber.Name = "txtContactPhoneNumber"
        Me.txtContactPhoneNumber.Size = New System.Drawing.Size(149, 20)
        Me.txtContactPhoneNumber.TabIndex = 366
        '
        'btnEmailAcknowledgmentLetter
        '
        Me.btnEmailAcknowledgmentLetter.AutoSize = True
        Me.btnEmailAcknowledgmentLetter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEmailAcknowledgmentLetter.Location = New System.Drawing.Point(404, 206)
        Me.btnEmailAcknowledgmentLetter.Name = "btnEmailAcknowledgmentLetter"
        Me.btnEmailAcknowledgmentLetter.Size = New System.Drawing.Size(157, 23)
        Me.btnEmailAcknowledgmentLetter.TabIndex = 17
        Me.btnEmailAcknowledgmentLetter.Text = "Email Acknowledgment Letter"
        Me.btnEmailAcknowledgmentLetter.UseVisualStyleBackColor = True
        '
        'btnGetCurrentPermittingContact
        '
        Me.btnGetCurrentPermittingContact.AutoSize = True
        Me.btnGetCurrentPermittingContact.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnGetCurrentPermittingContact.Location = New System.Drawing.Point(15, 5)
        Me.btnGetCurrentPermittingContact.Name = "btnGetCurrentPermittingContact"
        Me.btnGetCurrentPermittingContact.Size = New System.Drawing.Size(146, 23)
        Me.btnGetCurrentPermittingContact.TabIndex = 0
        Me.btnGetCurrentPermittingContact.Text = "Get Current Facility Contact"
        Me.btnGetCurrentPermittingContact.UseVisualStyleBackColor = True
        '
        'mtbContactZipCode
        '
        Me.mtbContactZipCode.Location = New System.Drawing.Point(652, 116)
        Me.mtbContactZipCode.Mask = "00000-9999"
        Me.mtbContactZipCode.Name = "mtbContactZipCode"
        Me.mtbContactZipCode.Size = New System.Drawing.Size(68, 20)
        Me.mtbContactZipCode.TabIndex = 11
        Me.mtbContactZipCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mtbContactFaxNumber
        '
        Me.mtbContactFaxNumber.Location = New System.Drawing.Point(92, 176)
        Me.mtbContactFaxNumber.Mask = "(999) 000-0000"
        Me.mtbContactFaxNumber.Name = "mtbContactFaxNumber"
        Me.mtbContactFaxNumber.Size = New System.Drawing.Size(89, 20)
        Me.mtbContactFaxNumber.TabIndex = 13
        Me.mtbContactFaxNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtContactDescription
        '
        Me.txtContactDescription.Location = New System.Drawing.Point(92, 242)
        Me.txtContactDescription.MaxLength = 4000
        Me.txtContactDescription.Multiline = True
        Me.txtContactDescription.Name = "txtContactDescription"
        Me.txtContactDescription.Size = New System.Drawing.Size(628, 96)
        Me.txtContactDescription.TabIndex = 15
        '
        'txtContactEmailAddress
        '
        Me.txtContactEmailAddress.Location = New System.Drawing.Point(92, 207)
        Me.txtContactEmailAddress.MaxLength = 100
        Me.txtContactEmailAddress.Name = "txtContactEmailAddress"
        Me.txtContactEmailAddress.Size = New System.Drawing.Size(284, 20)
        Me.txtContactEmailAddress.TabIndex = 14
        '
        'txtContactState
        '
        Me.txtContactState.Location = New System.Drawing.Point(550, 116)
        Me.txtContactState.MaxLength = 2
        Me.txtContactState.Name = "txtContactState"
        Me.txtContactState.Size = New System.Drawing.Size(40, 20)
        Me.txtContactState.TabIndex = 10
        '
        'txtContactCity
        '
        Me.txtContactCity.Location = New System.Drawing.Point(335, 116)
        Me.txtContactCity.MaxLength = 50
        Me.txtContactCity.Name = "txtContactCity"
        Me.txtContactCity.Size = New System.Drawing.Size(171, 20)
        Me.txtContactCity.TabIndex = 9
        '
        'txtContactStreetAddress
        '
        Me.txtContactStreetAddress.Location = New System.Drawing.Point(335, 88)
        Me.txtContactStreetAddress.MaxLength = 100
        Me.txtContactStreetAddress.Name = "txtContactStreetAddress"
        Me.txtContactStreetAddress.Size = New System.Drawing.Size(385, 20)
        Me.txtContactStreetAddress.TabIndex = 8
        '
        'txtContactTitle
        '
        Me.txtContactTitle.Location = New System.Drawing.Point(335, 60)
        Me.txtContactTitle.MaxLength = 100
        Me.txtContactTitle.Name = "txtContactTitle"
        Me.txtContactTitle.Size = New System.Drawing.Size(385, 20)
        Me.txtContactTitle.TabIndex = 7
        '
        'txtContactCompanyName
        '
        Me.txtContactCompanyName.Location = New System.Drawing.Point(335, 34)
        Me.txtContactCompanyName.MaxLength = 100
        Me.txtContactCompanyName.Name = "txtContactCompanyName"
        Me.txtContactCompanyName.Size = New System.Drawing.Size(385, 20)
        Me.txtContactCompanyName.TabIndex = 6
        '
        'txtContactPedigree
        '
        Me.txtContactPedigree.Location = New System.Drawing.Point(71, 116)
        Me.txtContactPedigree.MaxLength = 15
        Me.txtContactPedigree.Name = "txtContactPedigree"
        Me.txtContactPedigree.Size = New System.Drawing.Size(72, 20)
        Me.txtContactPedigree.TabIndex = 5
        '
        'txtContactSocialTitle
        '
        Me.txtContactSocialTitle.Location = New System.Drawing.Point(71, 34)
        Me.txtContactSocialTitle.MaxLength = 15
        Me.txtContactSocialTitle.Name = "txtContactSocialTitle"
        Me.txtContactSocialTitle.Size = New System.Drawing.Size(72, 20)
        Me.txtContactSocialTitle.TabIndex = 2
        '
        'txtContactLastName
        '
        Me.txtContactLastName.Location = New System.Drawing.Point(71, 88)
        Me.txtContactLastName.MaxLength = 35
        Me.txtContactLastName.Name = "txtContactLastName"
        Me.txtContactLastName.Size = New System.Drawing.Size(170, 20)
        Me.txtContactLastName.TabIndex = 4
        '
        'txtContactFirstName
        '
        Me.txtContactFirstName.Location = New System.Drawing.Point(71, 60)
        Me.txtContactFirstName.MaxLength = 35
        Me.txtContactFirstName.Name = "txtContactFirstName"
        Me.txtContactFirstName.Size = New System.Drawing.Size(170, 20)
        Me.txtContactFirstName.TabIndex = 3
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(9, 245)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(60, 13)
        Me.Label46.TabIndex = 365
        Me.Label46.Text = "Description"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(247, 38)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(82, 13)
        Me.Label45.TabIndex = 364
        Me.Label45.Text = "Company Name"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(9, 38)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(59, 13)
        Me.Label40.TabIndex = 363
        Me.Label40.Text = "Social Title"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(9, 120)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(33, 13)
        Me.Label39.TabIndex = 362
        Me.Label39.Text = "Suffix"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(512, 120)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(32, 13)
        Me.Label44.TabIndex = 360
        Me.Label44.Text = "State"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(596, 120)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(50, 13)
        Me.Label41.TabIndex = 359
        Me.Label41.Text = "Zip Code"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(9, 92)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(58, 13)
        Me.Label38.TabIndex = 356
        Me.Label38.Text = "Last Name"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(247, 64)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(27, 13)
        Me.Label32.TabIndex = 355
        Me.Label32.Text = "Title"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(8, 154)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(78, 13)
        Me.Label27.TabIndex = 354
        Me.Label27.Text = "Phone Number"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(9, 179)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(64, 13)
        Me.Label26.TabIndex = 353
        Me.Label26.Text = "Fax Number"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 210)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 13)
        Me.Label20.TabIndex = 352
        Me.Label20.Text = "Email Address"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(247, 92)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(76, 13)
        Me.Label18.TabIndex = 351
        Me.Label18.Text = "Street Address"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(247, 120)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 13)
        Me.Label12.TabIndex = 350
        Me.Label12.Text = "City"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(57, 13)
        Me.Label11.TabIndex = 348
        Me.Label11.Text = "First Name"
        '
        'TPWebPublisher
        '
        Me.TPWebPublisher.Controls.Add(Me.lblPNExpires)
        Me.TPWebPublisher.Controls.Add(Me.DTPPNExpires)
        Me.TPWebPublisher.Controls.Add(Me.lblEffectiveDateofPermit)
        Me.TPWebPublisher.Controls.Add(Me.lblExperationDate)
        Me.TPWebPublisher.Controls.Add(Me.lblEPANotifiedFinalOnWeb)
        Me.TPWebPublisher.Controls.Add(Me.lblFinalOnWeb)
        Me.TPWebPublisher.Controls.Add(Me.lbEPAStatesNotified)
        Me.TPWebPublisher.Controls.Add(Me.lblDraftOnWeb)
        Me.TPWebPublisher.Controls.Add(Me.lblNotifiedAppReceived)
        Me.TPWebPublisher.Controls.Add(Me.DTPExperationDate)
        Me.TPWebPublisher.Controls.Add(Me.Label68)
        Me.TPWebPublisher.Controls.Add(Me.DTPNotifiedAppReceived)
        Me.TPWebPublisher.Controls.Add(Me.btnSaveWebPublisher)
        Me.TPWebPublisher.Controls.Add(Me.txtEPATargetedComments)
        Me.TPWebPublisher.Controls.Add(Me.Label43)
        Me.TPWebPublisher.Controls.Add(Me.DTPEffectiveDateofPermit)
        Me.TPWebPublisher.Controls.Add(Me.DTPEPANotifiedPermitOnWeb)
        Me.TPWebPublisher.Controls.Add(Me.DTPFinalOnWeb)
        Me.TPWebPublisher.Controls.Add(Me.DTPEPAStatesNotified)
        Me.TPWebPublisher.Controls.Add(Me.DTPDraftOnWeb)
        Me.TPWebPublisher.Location = New System.Drawing.Point(4, 22)
        Me.TPWebPublisher.Name = "TPWebPublisher"
        Me.TPWebPublisher.Size = New System.Drawing.Size(784, 477)
        Me.TPWebPublisher.TabIndex = 4
        Me.TPWebPublisher.Text = "Web Publisher"
        Me.TPWebPublisher.UseVisualStyleBackColor = True
        '
        'lblPNExpires
        '
        Me.lblPNExpires.AutoSize = True
        Me.lblPNExpires.Location = New System.Drawing.Point(11, 82)
        Me.lblPNExpires.Name = "lblPNExpires"
        Me.lblPNExpires.Size = New System.Drawing.Size(59, 13)
        Me.lblPNExpires.TabIndex = 386
        Me.lblPNExpires.Text = "PN Expires"
        '
        'DTPPNExpires
        '
        Me.DTPPNExpires.Checked = False
        Me.DTPPNExpires.CustomFormat = "dd-MMM-yyyy"
        Me.DTPPNExpires.Enabled = False
        Me.DTPPNExpires.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPPNExpires.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPPNExpires.Location = New System.Drawing.Point(209, 77)
        Me.DTPPNExpires.Name = "DTPPNExpires"
        Me.DTPPNExpires.ShowCheckBox = True
        Me.DTPPNExpires.Size = New System.Drawing.Size(116, 21)
        Me.DTPPNExpires.TabIndex = 2
        Me.DTPPNExpires.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblEffectiveDateofPermit
        '
        Me.lblEffectiveDateofPermit.AutoSize = True
        Me.lblEffectiveDateofPermit.Location = New System.Drawing.Point(11, 210)
        Me.lblEffectiveDateofPermit.Name = "lblEffectiveDateofPermit"
        Me.lblEffectiveDateofPermit.Size = New System.Drawing.Size(119, 13)
        Me.lblEffectiveDateofPermit.TabIndex = 384
        Me.lblEffectiveDateofPermit.Text = "Effective Date of Permit"
        '
        'lblExperationDate
        '
        Me.lblExperationDate.AutoSize = True
        Me.lblExperationDate.Location = New System.Drawing.Point(11, 253)
        Me.lblExperationDate.Name = "lblExperationDate"
        Me.lblExperationDate.Size = New System.Drawing.Size(79, 13)
        Me.lblExperationDate.TabIndex = 383
        Me.lblExperationDate.Text = "Expiration Date"
        '
        'lblEPANotifiedFinalOnWeb
        '
        Me.lblEPANotifiedFinalOnWeb.AutoSize = True
        Me.lblEPANotifiedFinalOnWeb.Location = New System.Drawing.Point(11, 178)
        Me.lblEPANotifiedFinalOnWeb.Name = "lblEPANotifiedFinalOnWeb"
        Me.lblEPANotifiedFinalOnWeb.Size = New System.Drawing.Size(165, 13)
        Me.lblEPANotifiedFinalOnWeb.TabIndex = 382
        Me.lblEPANotifiedFinalOnWeb.Text = "EPA Notified Final Permit on Web"
        '
        'lblFinalOnWeb
        '
        Me.lblFinalOnWeb.AutoSize = True
        Me.lblFinalOnWeb.Location = New System.Drawing.Point(11, 146)
        Me.lblFinalOnWeb.Name = "lblFinalOnWeb"
        Me.lblFinalOnWeb.Size = New System.Drawing.Size(72, 13)
        Me.lblFinalOnWeb.TabIndex = 381
        Me.lblFinalOnWeb.Text = "Final On Web"
        '
        'lbEPAStatesNotified
        '
        Me.lbEPAStatesNotified.AutoSize = True
        Me.lbEPAStatesNotified.Location = New System.Drawing.Point(11, 114)
        Me.lbEPAStatesNotified.Name = "lbEPAStatesNotified"
        Me.lbEPAStatesNotified.Size = New System.Drawing.Size(121, 13)
        Me.lbEPAStatesNotified.TabIndex = 380
        Me.lbEPAStatesNotified.Text = "EPA and States Notified"
        '
        'lblDraftOnWeb
        '
        Me.lblDraftOnWeb.AutoSize = True
        Me.lblDraftOnWeb.Location = New System.Drawing.Point(11, 50)
        Me.lblDraftOnWeb.Name = "lblDraftOnWeb"
        Me.lblDraftOnWeb.Size = New System.Drawing.Size(71, 13)
        Me.lblDraftOnWeb.TabIndex = 379
        Me.lblDraftOnWeb.Text = "Draft on Web"
        '
        'lblNotifiedAppReceived
        '
        Me.lblNotifiedAppReceived.AutoSize = True
        Me.lblNotifiedAppReceived.Location = New System.Drawing.Point(8, 15)
        Me.lblNotifiedAppReceived.Name = "lblNotifiedAppReceived"
        Me.lblNotifiedAppReceived.Size = New System.Drawing.Size(173, 13)
        Me.lblNotifiedAppReceived.TabIndex = 378
        Me.lblNotifiedAppReceived.Text = "EPA/States Notified App Received"
        '
        'DTPExperationDate
        '
        Me.DTPExperationDate.Checked = False
        Me.DTPExperationDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPExperationDate.Enabled = False
        Me.DTPExperationDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPExperationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPExperationDate.Location = New System.Drawing.Point(209, 248)
        Me.DTPExperationDate.Name = "DTPExperationDate"
        Me.DTPExperationDate.ShowCheckBox = True
        Me.DTPExperationDate.Size = New System.Drawing.Size(116, 21)
        Me.DTPExperationDate.TabIndex = 7
        Me.DTPExperationDate.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        Me.DTPExperationDate.Visible = False
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(24, 232)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(42, 13)
        Me.Label68.TabIndex = 376
        Me.Label68.Text = "And/Or"
        '
        'DTPNotifiedAppReceived
        '
        Me.DTPNotifiedAppReceived.Checked = False
        Me.DTPNotifiedAppReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNotifiedAppReceived.Enabled = False
        Me.DTPNotifiedAppReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPNotifiedAppReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNotifiedAppReceived.Location = New System.Drawing.Point(209, 10)
        Me.DTPNotifiedAppReceived.Name = "DTPNotifiedAppReceived"
        Me.DTPNotifiedAppReceived.ShowCheckBox = True
        Me.DTPNotifiedAppReceived.Size = New System.Drawing.Size(116, 21)
        Me.DTPNotifiedAppReceived.TabIndex = 0
        Me.DTPNotifiedAppReceived.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'btnSaveWebPublisher
        '
        Me.btnSaveWebPublisher.Enabled = False
        Me.btnSaveWebPublisher.Location = New System.Drawing.Point(444, 11)
        Me.btnSaveWebPublisher.Name = "btnSaveWebPublisher"
        Me.btnSaveWebPublisher.Size = New System.Drawing.Size(63, 20)
        Me.btnSaveWebPublisher.TabIndex = 9
        Me.btnSaveWebPublisher.Text = "Save"
        '
        'txtEPATargetedComments
        '
        Me.txtEPATargetedComments.AcceptsReturn = True
        Me.txtEPATargetedComments.Location = New System.Drawing.Point(14, 313)
        Me.txtEPATargetedComments.MaxLength = 4000
        Me.txtEPATargetedComments.Multiline = True
        Me.txtEPATargetedComments.Name = "txtEPATargetedComments"
        Me.txtEPATargetedComments.ReadOnly = True
        Me.txtEPATargetedComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEPATargetedComments.Size = New System.Drawing.Size(493, 70)
        Me.txtEPATargetedComments.TabIndex = 8
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(0, 296)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(129, 13)
        Me.Label43.TabIndex = 341
        Me.Label43.Text = "EPA Targeted Comments:"
        '
        'DTPEffectiveDateofPermit
        '
        Me.DTPEffectiveDateofPermit.Checked = False
        Me.DTPEffectiveDateofPermit.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEffectiveDateofPermit.Enabled = False
        Me.DTPEffectiveDateofPermit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPEffectiveDateofPermit.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEffectiveDateofPermit.Location = New System.Drawing.Point(209, 205)
        Me.DTPEffectiveDateofPermit.Name = "DTPEffectiveDateofPermit"
        Me.DTPEffectiveDateofPermit.ShowCheckBox = True
        Me.DTPEffectiveDateofPermit.Size = New System.Drawing.Size(116, 21)
        Me.DTPEffectiveDateofPermit.TabIndex = 6
        Me.DTPEffectiveDateofPermit.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPEPANotifiedPermitOnWeb
        '
        Me.DTPEPANotifiedPermitOnWeb.Checked = False
        Me.DTPEPANotifiedPermitOnWeb.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEPANotifiedPermitOnWeb.Enabled = False
        Me.DTPEPANotifiedPermitOnWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPEPANotifiedPermitOnWeb.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEPANotifiedPermitOnWeb.Location = New System.Drawing.Point(209, 173)
        Me.DTPEPANotifiedPermitOnWeb.Name = "DTPEPANotifiedPermitOnWeb"
        Me.DTPEPANotifiedPermitOnWeb.ShowCheckBox = True
        Me.DTPEPANotifiedPermitOnWeb.Size = New System.Drawing.Size(116, 21)
        Me.DTPEPANotifiedPermitOnWeb.TabIndex = 5
        Me.DTPEPANotifiedPermitOnWeb.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPFinalOnWeb
        '
        Me.DTPFinalOnWeb.Checked = False
        Me.DTPFinalOnWeb.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFinalOnWeb.Enabled = False
        Me.DTPFinalOnWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPFinalOnWeb.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFinalOnWeb.Location = New System.Drawing.Point(209, 141)
        Me.DTPFinalOnWeb.Name = "DTPFinalOnWeb"
        Me.DTPFinalOnWeb.ShowCheckBox = True
        Me.DTPFinalOnWeb.Size = New System.Drawing.Size(116, 21)
        Me.DTPFinalOnWeb.TabIndex = 4
        Me.DTPFinalOnWeb.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPEPAStatesNotified
        '
        Me.DTPEPAStatesNotified.Checked = False
        Me.DTPEPAStatesNotified.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEPAStatesNotified.Enabled = False
        Me.DTPEPAStatesNotified.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPEPAStatesNotified.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEPAStatesNotified.Location = New System.Drawing.Point(209, 109)
        Me.DTPEPAStatesNotified.Name = "DTPEPAStatesNotified"
        Me.DTPEPAStatesNotified.ShowCheckBox = True
        Me.DTPEPAStatesNotified.Size = New System.Drawing.Size(116, 21)
        Me.DTPEPAStatesNotified.TabIndex = 3
        Me.DTPEPAStatesNotified.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPDraftOnWeb
        '
        Me.DTPDraftOnWeb.Checked = False
        Me.DTPDraftOnWeb.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDraftOnWeb.Enabled = False
        Me.DTPDraftOnWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDraftOnWeb.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDraftOnWeb.Location = New System.Drawing.Point(209, 45)
        Me.DTPDraftOnWeb.Name = "DTPDraftOnWeb"
        Me.DTPDraftOnWeb.ShowCheckBox = True
        Me.DTPDraftOnWeb.Size = New System.Drawing.Size(116, 21)
        Me.DTPDraftOnWeb.TabIndex = 1
        Me.DTPDraftOnWeb.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'TPInformationRequests
        '
        Me.TPInformationRequests.Controls.Add(Me.GroupBox3)
        Me.TPInformationRequests.Controls.Add(Me.dgvInformationRequested)
        Me.TPInformationRequests.Location = New System.Drawing.Point(4, 22)
        Me.TPInformationRequests.Name = "TPInformationRequests"
        Me.TPInformationRequests.Size = New System.Drawing.Size(784, 477)
        Me.TPInformationRequests.TabIndex = 2
        Me.TPInformationRequests.Text = "Information Requests"
        Me.TPInformationRequests.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblInformationReceived)
        Me.GroupBox3.Controls.Add(Me.lblInformationRequested)
        Me.GroupBox3.Controls.Add(Me.btnDeleteInformationRequest)
        Me.GroupBox3.Controls.Add(Me.btnClearInformationRequest)
        Me.GroupBox3.Controls.Add(Me.txtInformationRequestedKey)
        Me.GroupBox3.Controls.Add(Me.btnSaveInformationRequest)
        Me.GroupBox3.Controls.Add(Me.txtInformationReceived)
        Me.GroupBox3.Controls.Add(Me.txtInformationRequested)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.DTPInformationReceived)
        Me.GroupBox3.Controls.Add(Me.DTPInformationRequested)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Location = New System.Drawing.Point(0, 248)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(784, 229)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'lblInformationReceived
        '
        Me.lblInformationReceived.AutoSize = True
        Me.lblInformationReceived.Location = New System.Drawing.Point(254, 24)
        Me.lblInformationReceived.Name = "lblInformationReceived"
        Me.lblInformationReceived.Size = New System.Drawing.Size(82, 13)
        Me.lblInformationReceived.TabIndex = 340
        Me.lblInformationReceived.Text = "Date Received:"
        '
        'lblInformationRequested
        '
        Me.lblInformationRequested.AutoSize = True
        Me.lblInformationRequested.Location = New System.Drawing.Point(8, 24)
        Me.lblInformationRequested.Name = "lblInformationRequested"
        Me.lblInformationRequested.Size = New System.Drawing.Size(88, 13)
        Me.lblInformationRequested.TabIndex = 339
        Me.lblInformationRequested.Text = "Date Requested:"
        '
        'btnDeleteInformationRequest
        '
        Me.btnDeleteInformationRequest.Enabled = False
        Me.btnDeleteInformationRequest.Location = New System.Drawing.Point(616, 113)
        Me.btnDeleteInformationRequest.Name = "btnDeleteInformationRequest"
        Me.btnDeleteInformationRequest.Size = New System.Drawing.Size(104, 23)
        Me.btnDeleteInformationRequest.TabIndex = 6
        Me.btnDeleteInformationRequest.Text = "Delete Request"
        '
        'btnClearInformationRequest
        '
        Me.btnClearInformationRequest.Enabled = False
        Me.btnClearInformationRequest.Location = New System.Drawing.Point(616, 19)
        Me.btnClearInformationRequest.Name = "btnClearInformationRequest"
        Me.btnClearInformationRequest.Size = New System.Drawing.Size(104, 23)
        Me.btnClearInformationRequest.TabIndex = 4
        Me.btnClearInformationRequest.Text = "New Request"
        '
        'txtInformationRequestedKey
        '
        Me.txtInformationRequestedKey.Location = New System.Drawing.Point(620, 157)
        Me.txtInformationRequestedKey.Name = "txtInformationRequestedKey"
        Me.txtInformationRequestedKey.Size = New System.Drawing.Size(33, 20)
        Me.txtInformationRequestedKey.TabIndex = 333
        Me.txtInformationRequestedKey.Visible = False
        '
        'btnSaveInformationRequest
        '
        Me.btnSaveInformationRequest.Enabled = False
        Me.btnSaveInformationRequest.Location = New System.Drawing.Point(616, 66)
        Me.btnSaveInformationRequest.Name = "btnSaveInformationRequest"
        Me.btnSaveInformationRequest.Size = New System.Drawing.Size(104, 23)
        Me.btnSaveInformationRequest.TabIndex = 5
        Me.btnSaveInformationRequest.Text = "Save Request"
        '
        'txtInformationReceived
        '
        Me.txtInformationReceived.AcceptsReturn = True
        Me.txtInformationReceived.Location = New System.Drawing.Point(8, 149)
        Me.txtInformationReceived.Multiline = True
        Me.txtInformationReceived.Name = "txtInformationReceived"
        Me.txtInformationReceived.ReadOnly = True
        Me.txtInformationReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInformationReceived.Size = New System.Drawing.Size(593, 63)
        Me.txtInformationReceived.TabIndex = 3
        '
        'txtInformationRequested
        '
        Me.txtInformationRequested.AcceptsReturn = True
        Me.txtInformationRequested.Location = New System.Drawing.Point(8, 68)
        Me.txtInformationRequested.Multiline = True
        Me.txtInformationRequested.Name = "txtInformationRequested"
        Me.txtInformationRequested.ReadOnly = True
        Me.txtInformationRequested.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInformationRequested.Size = New System.Drawing.Size(593, 62)
        Me.txtInformationRequested.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(8, 133)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(74, 13)
        Me.Label17.TabIndex = 304
        Me.Label17.Text = "Info Received"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 52)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(80, 13)
        Me.Label16.TabIndex = 303
        Me.Label16.Text = "Info Requested"
        '
        'DTPInformationReceived
        '
        Me.DTPInformationReceived.Checked = False
        Me.DTPInformationReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPInformationReceived.Enabled = False
        Me.DTPInformationReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPInformationReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPInformationReceived.Location = New System.Drawing.Point(342, 19)
        Me.DTPInformationReceived.Name = "DTPInformationReceived"
        Me.DTPInformationReceived.ShowCheckBox = True
        Me.DTPInformationReceived.Size = New System.Drawing.Size(116, 21)
        Me.DTPInformationReceived.TabIndex = 1
        Me.DTPInformationReceived.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPInformationRequested
        '
        Me.DTPInformationRequested.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPInformationRequested.Checked = False
        Me.DTPInformationRequested.CustomFormat = "dd-MMM-yyyy"
        Me.DTPInformationRequested.Enabled = False
        Me.DTPInformationRequested.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPInformationRequested.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPInformationRequested.Location = New System.Drawing.Point(102, 19)
        Me.DTPInformationRequested.Name = "DTPInformationRequested"
        Me.DTPInformationRequested.ShowCheckBox = True
        Me.DTPInformationRequested.Size = New System.Drawing.Size(116, 21)
        Me.DTPInformationRequested.TabIndex = 0
        Me.DTPInformationRequested.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'dgvInformationRequested
        '
        Me.dgvInformationRequested.AllowUserToAddRows = False
        Me.dgvInformationRequested.AllowUserToDeleteRows = False
        Me.dgvInformationRequested.AllowUserToOrderColumns = True
        Me.dgvInformationRequested.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInformationRequested.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvInformationRequested.Location = New System.Drawing.Point(0, 0)
        Me.dgvInformationRequested.Name = "dgvInformationRequested"
        Me.dgvInformationRequested.ReadOnly = True
        Me.dgvInformationRequested.Size = New System.Drawing.Size(784, 477)
        Me.dgvInformationRequested.TabIndex = 2
        '
        'TPApplicationHistory
        '
        Me.TPApplicationHistory.Controls.Add(Me.dgvFacilityAppHistory)
        Me.TPApplicationHistory.Controls.Add(Me.GroupBox6)
        Me.TPApplicationHistory.Location = New System.Drawing.Point(4, 22)
        Me.TPApplicationHistory.Name = "TPApplicationHistory"
        Me.TPApplicationHistory.Size = New System.Drawing.Size(784, 477)
        Me.TPApplicationHistory.TabIndex = 1
        Me.TPApplicationHistory.Text = "History"
        Me.TPApplicationHistory.UseVisualStyleBackColor = True
        '
        'dgvFacilityAppHistory
        '
        Me.dgvFacilityAppHistory.AllowUserToAddRows = False
        Me.dgvFacilityAppHistory.AllowUserToDeleteRows = False
        Me.dgvFacilityAppHistory.AllowUserToOrderColumns = True
        Me.dgvFacilityAppHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFacilityAppHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFacilityAppHistory.Location = New System.Drawing.Point(0, 0)
        Me.dgvFacilityAppHistory.Name = "dgvFacilityAppHistory"
        Me.dgvFacilityAppHistory.ReadOnly = True
        Me.dgvFacilityAppHistory.Size = New System.Drawing.Size(784, 233)
        Me.dgvFacilityAppHistory.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtApplicationCount)
        Me.GroupBox6.Controls.Add(Me.txtMasterAppLock)
        Me.GroupBox6.Controls.Add(Me.btnClearLinks)
        Me.GroupBox6.Controls.Add(Me.btnLinkApplications)
        Me.GroupBox6.Controls.Add(Me.btnAddApplicationToList)
        Me.GroupBox6.Controls.Add(Me.lbLinkApplications)
        Me.GroupBox6.Controls.Add(Me.txtMasterApp)
        Me.GroupBox6.Controls.Add(Me.txtEngineerHistory)
        Me.GroupBox6.Controls.Add(Me.txtApplicationDatedHistory)
        Me.GroupBox6.Controls.Add(Me.txtApplicationTypeHistory)
        Me.GroupBox6.Controls.Add(Me.txtApplicationUnitHistory)
        Me.GroupBox6.Controls.Add(Me.txtApplicationNumberHistory)
        Me.GroupBox6.Controls.Add(Me.Label35)
        Me.GroupBox6.Controls.Add(Me.Label34)
        Me.GroupBox6.Controls.Add(Me.chbClosedOutHistory)
        Me.GroupBox6.Controls.Add(Me.Label15)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.txtHistoryComments)
        Me.GroupBox6.Controls.Add(Me.txtHistoryAppComments)
        Me.GroupBox6.Controls.Add(Me.Label29)
        Me.GroupBox6.Controls.Add(Me.Label33)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox6.Location = New System.Drawing.Point(0, 233)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(784, 244)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        '
        'txtApplicationCount
        '
        Me.txtApplicationCount.Location = New System.Drawing.Point(507, 173)
        Me.txtApplicationCount.Name = "txtApplicationCount"
        Me.txtApplicationCount.ReadOnly = True
        Me.txtApplicationCount.Size = New System.Drawing.Size(7, 20)
        Me.txtApplicationCount.TabIndex = 351
        Me.txtApplicationCount.Visible = False
        '
        'txtMasterAppLock
        '
        Me.txtMasterAppLock.Location = New System.Drawing.Point(500, 173)
        Me.txtMasterAppLock.Name = "txtMasterAppLock"
        Me.txtMasterAppLock.ReadOnly = True
        Me.txtMasterAppLock.Size = New System.Drawing.Size(8, 20)
        Me.txtMasterAppLock.TabIndex = 350
        Me.txtMasterAppLock.Visible = False
        '
        'btnClearLinks
        '
        Me.btnClearLinks.Enabled = False
        Me.btnClearLinks.Location = New System.Drawing.Point(660, 97)
        Me.btnClearLinks.Name = "btnClearLinks"
        Me.btnClearLinks.Size = New System.Drawing.Size(104, 23)
        Me.btnClearLinks.TabIndex = 10
        Me.btnClearLinks.Text = "Unlink Application"
        '
        'btnLinkApplications
        '
        Me.btnLinkApplications.Enabled = False
        Me.btnLinkApplications.Location = New System.Drawing.Point(660, 35)
        Me.btnLinkApplications.Name = "btnLinkApplications"
        Me.btnLinkApplications.Size = New System.Drawing.Size(104, 23)
        Me.btnLinkApplications.TabIndex = 9
        Me.btnLinkApplications.Text = "Link Applications"
        '
        'btnAddApplicationToList
        '
        Me.btnAddApplicationToList.Enabled = False
        Me.btnAddApplicationToList.Location = New System.Drawing.Point(427, 35)
        Me.btnAddApplicationToList.Name = "btnAddApplicationToList"
        Me.btnAddApplicationToList.Size = New System.Drawing.Size(104, 44)
        Me.btnAddApplicationToList.TabIndex = 8
        Me.btnAddApplicationToList.Text = "Add Application to  List"
        '
        'lbLinkApplications
        '
        Me.lbLinkApplications.Location = New System.Drawing.Point(540, 35)
        Me.lbLinkApplications.Name = "lbLinkApplications"
        Me.lbLinkApplications.Size = New System.Drawing.Size(104, 134)
        Me.lbLinkApplications.TabIndex = 11
        '
        'txtMasterApp
        '
        Me.txtMasterApp.Location = New System.Drawing.Point(520, 175)
        Me.txtMasterApp.Name = "txtMasterApp"
        Me.txtMasterApp.ReadOnly = True
        Me.txtMasterApp.Size = New System.Drawing.Size(20, 20)
        Me.txtMasterApp.TabIndex = 344
        Me.txtMasterApp.Visible = False
        '
        'txtEngineerHistory
        '
        Me.txtEngineerHistory.Location = New System.Drawing.Point(296, 59)
        Me.txtEngineerHistory.Name = "txtEngineerHistory"
        Me.txtEngineerHistory.ReadOnly = True
        Me.txtEngineerHistory.Size = New System.Drawing.Size(120, 20)
        Me.txtEngineerHistory.TabIndex = 4
        '
        'txtApplicationDatedHistory
        '
        Me.txtApplicationDatedHistory.Location = New System.Drawing.Point(88, 83)
        Me.txtApplicationDatedHistory.Name = "txtApplicationDatedHistory"
        Me.txtApplicationDatedHistory.ReadOnly = True
        Me.txtApplicationDatedHistory.Size = New System.Drawing.Size(120, 20)
        Me.txtApplicationDatedHistory.TabIndex = 2
        '
        'txtApplicationTypeHistory
        '
        Me.txtApplicationTypeHistory.Location = New System.Drawing.Point(88, 59)
        Me.txtApplicationTypeHistory.Name = "txtApplicationTypeHistory"
        Me.txtApplicationTypeHistory.ReadOnly = True
        Me.txtApplicationTypeHistory.Size = New System.Drawing.Size(120, 20)
        Me.txtApplicationTypeHistory.TabIndex = 1
        '
        'txtApplicationUnitHistory
        '
        Me.txtApplicationUnitHistory.Location = New System.Drawing.Point(296, 35)
        Me.txtApplicationUnitHistory.Name = "txtApplicationUnitHistory"
        Me.txtApplicationUnitHistory.ReadOnly = True
        Me.txtApplicationUnitHistory.Size = New System.Drawing.Size(120, 20)
        Me.txtApplicationUnitHistory.TabIndex = 3
        '
        'txtApplicationNumberHistory
        '
        Me.txtApplicationNumberHistory.Location = New System.Drawing.Point(88, 35)
        Me.txtApplicationNumberHistory.Name = "txtApplicationNumberHistory"
        Me.txtApplicationNumberHistory.ReadOnly = True
        Me.txtApplicationNumberHistory.Size = New System.Drawing.Size(120, 20)
        Me.txtApplicationNumberHistory.TabIndex = 0
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(24, 87)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(60, 13)
        Me.Label35.TabIndex = 338
        Me.Label35.Text = "APP Dated"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(240, 61)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(49, 13)
        Me.Label34.TabIndex = 337
        Me.Label34.Text = "Engineer"
        '
        'chbClosedOutHistory
        '
        Me.chbClosedOutHistory.Enabled = False
        Me.chbClosedOutHistory.Location = New System.Drawing.Point(232, 85)
        Me.chbClosedOutHistory.Name = "chbClosedOutHistory"
        Me.chbClosedOutHistory.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chbClosedOutHistory.Size = New System.Drawing.Size(80, 16)
        Me.chbClosedOutHistory.TabIndex = 5
        Me.chbClosedOutHistory.Text = "Closed Out"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 36)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(79, 13)
        Me.Label15.TabIndex = 335
        Me.Label15.Text = "Application No."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(264, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 13)
        Me.Label6.TabIndex = 334
        Me.Label6.Text = "Unit"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(32, 61)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 13)
        Me.Label14.TabIndex = 333
        Me.Label14.Text = "APP type"
        '
        'txtHistoryComments
        '
        Me.txtHistoryComments.Location = New System.Drawing.Point(16, 173)
        Me.txtHistoryComments.Multiline = True
        Me.txtHistoryComments.Name = "txtHistoryComments"
        Me.txtHistoryComments.ReadOnly = True
        Me.txtHistoryComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHistoryComments.Size = New System.Drawing.Size(480, 41)
        Me.txtHistoryComments.TabIndex = 7
        '
        'txtHistoryAppComments
        '
        Me.txtHistoryAppComments.Location = New System.Drawing.Point(16, 125)
        Me.txtHistoryAppComments.Multiline = True
        Me.txtHistoryAppComments.Name = "txtHistoryAppComments"
        Me.txtHistoryAppComments.ReadOnly = True
        Me.txtHistoryAppComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHistoryAppComments.Size = New System.Drawing.Size(480, 32)
        Me.txtHistoryAppComments.TabIndex = 6
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(8, 159)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(56, 13)
        Me.Label29.TabIndex = 330
        Me.Label29.Text = "Comments"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(8, 111)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(149, 13)
        Me.Label33.TabIndex = 329
        Me.Label33.Text = "Reason Application Submitted"
        '
        'TPReviews
        '
        Me.TPReviews.Controls.Add(Me.GroupBox5)
        Me.TPReviews.Controls.Add(Me.GroupBox4)
        Me.TPReviews.Controls.Add(Me.Panel4)
        Me.TPReviews.Location = New System.Drawing.Point(4, 22)
        Me.TPReviews.Name = "TPReviews"
        Me.TPReviews.Size = New System.Drawing.Size(784, 477)
        Me.TPReviews.TabIndex = 3
        Me.TPReviews.Text = "Compliance Review"
        Me.TPReviews.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblISMPReview)
        Me.GroupBox5.Controls.Add(Me.txtISMPComments)
        Me.GroupBox5.Controls.Add(Me.Label23)
        Me.GroupBox5.Controls.Add(Me.Panel6)
        Me.GroupBox5.Controls.Add(Me.Label24)
        Me.GroupBox5.Controls.Add(Me.DTPISMPReview)
        Me.GroupBox5.Controls.Add(Me.cboISMPStaff)
        Me.GroupBox5.Controls.Add(Me.lblISMPStaff)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(0, 192)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(784, 285)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "ISMP Review"
        '
        'lblISMPReview
        '
        Me.lblISMPReview.AutoSize = True
        Me.lblISMPReview.Location = New System.Drawing.Point(4, 25)
        Me.lblISMPReview.Name = "lblISMPReview"
        Me.lblISMPReview.Size = New System.Drawing.Size(120, 13)
        Me.lblISMPReview.TabIndex = 344
        Me.lblISMPReview.Text = "Date Returned to SSPP"
        '
        'txtISMPComments
        '
        Me.txtISMPComments.AcceptsReturn = True
        Me.txtISMPComments.Enabled = False
        Me.txtISMPComments.Location = New System.Drawing.Point(72, 96)
        Me.txtISMPComments.MaxLength = 4000
        Me.txtISMPComments.Multiline = True
        Me.txtISMPComments.Name = "txtISMPComments"
        Me.txtISMPComments.ReadOnly = True
        Me.txtISMPComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtISMPComments.Size = New System.Drawing.Size(328, 48)
        Me.txtISMPComments.TabIndex = 3
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(8, 96)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(56, 13)
        Me.Label23.TabIndex = 339
        Me.Label23.Text = "Comments"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rdbISMPNo)
        Me.Panel6.Controls.Add(Me.rdbISMPYes)
        Me.Panel6.Location = New System.Drawing.Point(280, 72)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(88, 16)
        Me.Panel6.TabIndex = 2
        '
        'rdbISMPNo
        '
        Me.rdbISMPNo.Enabled = False
        Me.rdbISMPNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbISMPNo.Name = "rdbISMPNo"
        Me.rdbISMPNo.Size = New System.Drawing.Size(40, 16)
        Me.rdbISMPNo.TabIndex = 1
        Me.rdbISMPNo.Text = "No"
        '
        'rdbISMPYes
        '
        Me.rdbISMPYes.Enabled = False
        Me.rdbISMPYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbISMPYes.Name = "rdbISMPYes"
        Me.rdbISMPYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbISMPYes.TabIndex = 0
        Me.rdbISMPYes.Text = "Yes"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(8, 72)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(257, 13)
        Me.Label24.TabIndex = 337
        Me.Label24.Text = "Did ISMP make any specific comments on the draft? "
        '
        'DTPISMPReview
        '
        Me.DTPISMPReview.Checked = False
        Me.DTPISMPReview.CustomFormat = "dd-MMM-yyyy"
        Me.DTPISMPReview.Enabled = False
        Me.DTPISMPReview.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPISMPReview.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPISMPReview.Location = New System.Drawing.Point(135, 21)
        Me.DTPISMPReview.Name = "DTPISMPReview"
        Me.DTPISMPReview.ShowCheckBox = True
        Me.DTPISMPReview.Size = New System.Drawing.Size(116, 21)
        Me.DTPISMPReview.TabIndex = 0
        Me.DTPISMPReview.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'cboISMPStaff
        '
        Me.cboISMPStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboISMPStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboISMPStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboISMPStaff.Enabled = False
        Me.cboISMPStaff.Location = New System.Drawing.Point(127, 46)
        Me.cboISMPStaff.Name = "cboISMPStaff"
        Me.cboISMPStaff.Size = New System.Drawing.Size(160, 21)
        Me.cboISMPStaff.TabIndex = 1
        '
        'lblISMPStaff
        '
        Me.lblISMPStaff.AutoSize = True
        Me.lblISMPStaff.Location = New System.Drawing.Point(7, 49)
        Me.lblISMPStaff.Name = "lblISMPStaff"
        Me.lblISMPStaff.Size = New System.Drawing.Size(111, 13)
        Me.lblISMPStaff.TabIndex = 333
        Me.lblISMPStaff.Text = "ISMP Reviewing Staff"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblSSCPReview)
        Me.GroupBox4.Controls.Add(Me.txtSSCPComments)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.Panel5)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.DTPSSCPReview)
        Me.GroupBox4.Controls.Add(Me.cboSSCPStaff)
        Me.GroupBox4.Controls.Add(Me.lblSSCPStaff)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(0, 40)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(784, 152)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Compliance Review"
        '
        'lblSSCPReview
        '
        Me.lblSSCPReview.AutoSize = True
        Me.lblSSCPReview.Location = New System.Drawing.Point(4, 18)
        Me.lblSSCPReview.Name = "lblSSCPReview"
        Me.lblSSCPReview.Size = New System.Drawing.Size(120, 13)
        Me.lblSSCPReview.TabIndex = 343
        Me.lblSSCPReview.Text = "Date Returned to SSPP"
        '
        'txtSSCPComments
        '
        Me.txtSSCPComments.AcceptsReturn = True
        Me.txtSSCPComments.Enabled = False
        Me.txtSSCPComments.Location = New System.Drawing.Point(72, 88)
        Me.txtSSCPComments.MaxLength = 4000
        Me.txtSSCPComments.Multiline = True
        Me.txtSSCPComments.Name = "txtSSCPComments"
        Me.txtSSCPComments.ReadOnly = True
        Me.txtSSCPComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSSCPComments.Size = New System.Drawing.Size(328, 48)
        Me.txtSSCPComments.TabIndex = 3
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(8, 88)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(56, 13)
        Me.Label22.TabIndex = 308
        Me.Label22.Text = "Comments"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.rdbSSCPNo)
        Me.Panel5.Controls.Add(Me.rdbSSCPYes)
        Me.Panel5.Location = New System.Drawing.Point(292, 62)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(88, 16)
        Me.Panel5.TabIndex = 2
        '
        'rdbSSCPNo
        '
        Me.rdbSSCPNo.Enabled = False
        Me.rdbSSCPNo.Location = New System.Drawing.Point(45, 0)
        Me.rdbSSCPNo.Name = "rdbSSCPNo"
        Me.rdbSSCPNo.Size = New System.Drawing.Size(40, 16)
        Me.rdbSSCPNo.TabIndex = 1
        Me.rdbSSCPNo.Text = "No"
        '
        'rdbSSCPYes
        '
        Me.rdbSSCPYes.Enabled = False
        Me.rdbSSCPYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbSSCPYes.Name = "rdbSSCPYes"
        Me.rdbSSCPYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbSSCPYes.TabIndex = 0
        Me.rdbSSCPYes.Text = "Yes"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(8, 64)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(286, 13)
        Me.Label21.TabIndex = 306
        Me.Label21.Text = "Did Compliance make any specific comments on the draft? "
        '
        'DTPSSCPReview
        '
        Me.DTPSSCPReview.Checked = False
        Me.DTPSSCPReview.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSSCPReview.Enabled = False
        Me.DTPSSCPReview.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPSSCPReview.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSSCPReview.Location = New System.Drawing.Point(130, 13)
        Me.DTPSSCPReview.Name = "DTPSSCPReview"
        Me.DTPSSCPReview.ShowCheckBox = True
        Me.DTPSSCPReview.Size = New System.Drawing.Size(116, 21)
        Me.DTPSSCPReview.TabIndex = 0
        Me.DTPSSCPReview.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'cboSSCPStaff
        '
        Me.cboSSCPStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSSCPStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSSCPStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSSCPStaff.Enabled = False
        Me.cboSSCPStaff.Location = New System.Drawing.Point(148, 38)
        Me.cboSSCPStaff.Name = "cboSSCPStaff"
        Me.cboSSCPStaff.Size = New System.Drawing.Size(160, 21)
        Me.cboSSCPStaff.TabIndex = 1
        '
        'lblSSCPStaff
        '
        Me.lblSSCPStaff.AutoSize = True
        Me.lblSSCPStaff.Location = New System.Drawing.Point(7, 42)
        Me.lblSSCPStaff.Name = "lblSSCPStaff"
        Me.lblSSCPStaff.Size = New System.Drawing.Size(140, 13)
        Me.lblSSCPStaff.TabIndex = 262
        Me.lblSSCPStaff.Text = "Compliance Reviewing Staff"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.lblReviewSubmitted)
        Me.Panel4.Controls.Add(Me.cboISMPUnits)
        Me.Panel4.Controls.Add(Me.lblISMPUnits)
        Me.Panel4.Controls.Add(Me.DTPReviewSubmitted)
        Me.Panel4.Controls.Add(Me.lblSSCPUnit)
        Me.Panel4.Controls.Add(Me.cboSSCPUnits)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(784, 40)
        Me.Panel4.TabIndex = 0
        '
        'lblReviewSubmitted
        '
        Me.lblReviewSubmitted.AutoSize = True
        Me.lblReviewSubmitted.Location = New System.Drawing.Point(4, 12)
        Me.lblReviewSubmitted.Name = "lblReviewSubmitted"
        Me.lblReviewSubmitted.Size = New System.Drawing.Size(131, 13)
        Me.lblReviewSubmitted.TabIndex = 335
        Me.lblReviewSubmitted.Text = "Date Submitted to Review"
        '
        'cboISMPUnits
        '
        Me.cboISMPUnits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboISMPUnits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboISMPUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboISMPUnits.Enabled = False
        Me.cboISMPUnits.Location = New System.Drawing.Point(517, 8)
        Me.cboISMPUnits.Name = "cboISMPUnits"
        Me.cboISMPUnits.Size = New System.Drawing.Size(160, 21)
        Me.cboISMPUnits.TabIndex = 2
        '
        'lblISMPUnits
        '
        Me.lblISMPUnits.AutoSize = True
        Me.lblISMPUnits.Location = New System.Drawing.Point(477, 12)
        Me.lblISMPUnits.Name = "lblISMPUnits"
        Me.lblISMPUnits.Size = New System.Drawing.Size(36, 13)
        Me.lblISMPUnits.TabIndex = 335
        Me.lblISMPUnits.Text = "ISMP:"
        '
        'DTPReviewSubmitted
        '
        Me.DTPReviewSubmitted.Checked = False
        Me.DTPReviewSubmitted.CustomFormat = "dd-MMM-yyyy"
        Me.DTPReviewSubmitted.Enabled = False
        Me.DTPReviewSubmitted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPReviewSubmitted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPReviewSubmitted.Location = New System.Drawing.Point(135, 8)
        Me.DTPReviewSubmitted.Name = "DTPReviewSubmitted"
        Me.DTPReviewSubmitted.ShowCheckBox = True
        Me.DTPReviewSubmitted.Size = New System.Drawing.Size(116, 21)
        Me.DTPReviewSubmitted.TabIndex = 0
        Me.DTPReviewSubmitted.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblSSCPUnit
        '
        Me.lblSSCPUnit.AutoSize = True
        Me.lblSSCPUnit.Location = New System.Drawing.Point(270, 12)
        Me.lblSSCPUnit.Name = "lblSSCPUnit"
        Me.lblSSCPUnit.Size = New System.Drawing.Size(38, 13)
        Me.lblSSCPUnit.TabIndex = 334
        Me.lblSSCPUnit.Text = "SSCP:"
        '
        'cboSSCPUnits
        '
        Me.cboSSCPUnits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSSCPUnits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSSCPUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSSCPUnits.Enabled = False
        Me.cboSSCPUnits.Location = New System.Drawing.Point(310, 8)
        Me.cboSSCPUnits.Name = "cboSSCPUnits"
        Me.cboSSCPUnits.Size = New System.Drawing.Size(160, 21)
        Me.cboSSCPUnits.TabIndex = 1
        '
        'TPOtherInfo
        '
        Me.TPOtherInfo.Controls.Add(Me.OtherInfoGroup)
        Me.TPOtherInfo.Controls.Add(Me.GBSignificationComments)
        Me.TPOtherInfo.Controls.Add(Me.ApplicableRulesGroup)
        Me.TPOtherInfo.Location = New System.Drawing.Point(4, 22)
        Me.TPOtherInfo.Name = "TPOtherInfo"
        Me.TPOtherInfo.Size = New System.Drawing.Size(784, 477)
        Me.TPOtherInfo.TabIndex = 5
        Me.TPOtherInfo.Text = "Other Information"
        Me.TPOtherInfo.UseVisualStyleBackColor = True
        '
        'OtherInfoGroup
        '
        Me.OtherInfoGroup.Controls.Add(Me.chbNspsFeeExempt)
        Me.OtherInfoGroup.Controls.Add(Me.chbFederallyOwned)
        Me.OtherInfoGroup.Controls.Add(Me.chbConfidential)
        Me.OtherInfoGroup.Location = New System.Drawing.Point(3, 150)
        Me.OtherInfoGroup.Name = "OtherInfoGroup"
        Me.OtherInfoGroup.Size = New System.Drawing.Size(189, 87)
        Me.OtherInfoGroup.TabIndex = 2
        Me.OtherInfoGroup.TabStop = False
        Me.OtherInfoGroup.Text = "Other Information"
        '
        'chbNspsFeeExempt
        '
        Me.chbNspsFeeExempt.AutoSize = True
        Me.chbNspsFeeExempt.Location = New System.Drawing.Point(6, 55)
        Me.chbNspsFeeExempt.Name = "chbNspsFeeExempt"
        Me.chbNspsFeeExempt.Size = New System.Drawing.Size(114, 17)
        Me.chbNspsFeeExempt.TabIndex = 8
        Me.chbNspsFeeExempt.Text = "NSPS Fee Exempt"
        Me.chbNspsFeeExempt.UseVisualStyleBackColor = True
        '
        'chbFederallyOwned
        '
        Me.chbFederallyOwned.AutoSize = True
        Me.chbFederallyOwned.Location = New System.Drawing.Point(6, 37)
        Me.chbFederallyOwned.Name = "chbFederallyOwned"
        Me.chbFederallyOwned.Size = New System.Drawing.Size(135, 17)
        Me.chbFederallyOwned.TabIndex = 8
        Me.chbFederallyOwned.Text = "Federally-owned facility"
        Me.chbFederallyOwned.UseVisualStyleBackColor = True
        '
        'chbConfidential
        '
        Me.chbConfidential.AutoSize = True
        Me.chbConfidential.Enabled = False
        Me.chbConfidential.Location = New System.Drawing.Point(6, 19)
        Me.chbConfidential.Name = "chbConfidential"
        Me.chbConfidential.Size = New System.Drawing.Size(183, 17)
        Me.chbConfidential.TabIndex = 7
        Me.chbConfidential.Text = "Confidential information submitted"
        Me.chbConfidential.UseVisualStyleBackColor = True
        '
        'GBSignificationComments
        '
        Me.GBSignificationComments.Controls.Add(Me.txtSignificantComments)
        Me.GBSignificationComments.Location = New System.Drawing.Point(215, 3)
        Me.GBSignificationComments.Name = "GBSignificationComments"
        Me.GBSignificationComments.Size = New System.Drawing.Size(565, 178)
        Me.GBSignificationComments.TabIndex = 1
        Me.GBSignificationComments.TabStop = False
        Me.GBSignificationComments.Text = "Significant Modifications Emission Increase/Decrease for Public Notice"
        '
        'txtSignificantComments
        '
        Me.txtSignificantComments.AcceptsReturn = True
        Me.txtSignificantComments.Location = New System.Drawing.Point(6, 19)
        Me.txtSignificantComments.MaxLength = 4000
        Me.txtSignificantComments.Multiline = True
        Me.txtSignificantComments.Name = "txtSignificantComments"
        Me.txtSignificantComments.ReadOnly = True
        Me.txtSignificantComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSignificantComments.Size = New System.Drawing.Size(553, 146)
        Me.txtSignificantComments.TabIndex = 0
        '
        'ApplicableRulesGroup
        '
        Me.ApplicableRulesGroup.Controls.Add(Me.chbPal)
        Me.ApplicableRulesGroup.Controls.Add(Me.chbRuleyy)
        Me.ApplicableRulesGroup.Controls.Add(Me.chbRulett)
        Me.ApplicableRulesGroup.Controls.Add(Me.chb112g)
        Me.ApplicableRulesGroup.Controls.Add(Me.chbNAANSR)
        Me.ApplicableRulesGroup.Controls.Add(Me.chbPSD)
        Me.ApplicableRulesGroup.Location = New System.Drawing.Point(3, 3)
        Me.ApplicableRulesGroup.Name = "ApplicableRulesGroup"
        Me.ApplicableRulesGroup.Size = New System.Drawing.Size(189, 141)
        Me.ApplicableRulesGroup.TabIndex = 0
        Me.ApplicableRulesGroup.TabStop = False
        Me.ApplicableRulesGroup.Text = "Applicable Rules"
        '
        'chbPal
        '
        Me.chbPal.AutoSize = True
        Me.chbPal.Enabled = False
        Me.chbPal.Location = New System.Drawing.Point(6, 110)
        Me.chbPal.Name = "chbPal"
        Me.chbPal.Size = New System.Drawing.Size(84, 17)
        Me.chbPal.TabIndex = 5
        Me.chbPal.Text = "Actuals PAL"
        Me.chbPal.UseVisualStyleBackColor = True
        '
        'chbRuleyy
        '
        Me.chbRuleyy.AutoSize = True
        Me.chbRuleyy.Enabled = False
        Me.chbRuleyy.Location = New System.Drawing.Point(6, 91)
        Me.chbRuleyy.Name = "chbRuleyy"
        Me.chbRuleyy.Size = New System.Drawing.Size(99, 17)
        Me.chbRuleyy.TabIndex = 4
        Me.chbRuleyy.Text = "Rule (yy) RACT"
        Me.chbRuleyy.UseVisualStyleBackColor = True
        '
        'chbRulett
        '
        Me.chbRulett.AutoSize = True
        Me.chbRulett.Enabled = False
        Me.chbRulett.Location = New System.Drawing.Point(6, 73)
        Me.chbRulett.Name = "chbRulett"
        Me.chbRulett.Size = New System.Drawing.Size(95, 17)
        Me.chbRulett.TabIndex = 3
        Me.chbRulett.Text = "Rule (tt) RACT"
        Me.chbRulett.UseVisualStyleBackColor = True
        '
        'chb112g
        '
        Me.chb112g.AutoSize = True
        Me.chb112g.Enabled = False
        Me.chb112g.Location = New System.Drawing.Point(6, 55)
        Me.chb112g.Name = "chb112g"
        Me.chb112g.Size = New System.Drawing.Size(56, 17)
        Me.chb112g.TabIndex = 2
        Me.chb112g.Text = "112(g)"
        Me.chb112g.UseVisualStyleBackColor = True
        '
        'chbNAANSR
        '
        Me.chbNAANSR.AutoSize = True
        Me.chbNAANSR.Enabled = False
        Me.chbNAANSR.Location = New System.Drawing.Point(6, 37)
        Me.chbNAANSR.Name = "chbNAANSR"
        Me.chbNAANSR.Size = New System.Drawing.Size(74, 17)
        Me.chbNAANSR.TabIndex = 1
        Me.chbNAANSR.Text = "NAA NSR"
        Me.chbNAANSR.UseVisualStyleBackColor = True
        '
        'chbPSD
        '
        Me.chbPSD.AutoSize = True
        Me.chbPSD.Enabled = False
        Me.chbPSD.Location = New System.Drawing.Point(6, 19)
        Me.chbPSD.Name = "chbPSD"
        Me.chbPSD.Size = New System.Drawing.Size(48, 17)
        Me.chbPSD.TabIndex = 0
        Me.chbPSD.Text = "PSD"
        Me.chbPSD.UseVisualStyleBackColor = True
        '
        'TPTrackingLog
        '
        Me.TPTrackingLog.Controls.Add(Me.Label1)
        Me.TPTrackingLog.Controls.Add(Me.lblDated)
        Me.TPTrackingLog.Controls.Add(Me.txtNAICSCode)
        Me.TPTrackingLog.Controls.Add(Me.Label72)
        Me.TPTrackingLog.Controls.Add(Me.txtFacilityName)
        Me.TPTrackingLog.Controls.Add(Me.llbPermitNumber)
        Me.TPTrackingLog.Controls.Add(Me.txtDistrict)
        Me.TPTrackingLog.Controls.Add(Me.lblPAReady)
        Me.TPTrackingLog.Controls.Add(Me.Label7)
        Me.TPTrackingLog.Controls.Add(Me.lblPNReady)
        Me.TPTrackingLog.Controls.Add(Me.lblReceived)
        Me.TPTrackingLog.Controls.Add(Me.GBOther)
        Me.TPTrackingLog.Controls.Add(Me.lblPermitNumber)
        Me.TPTrackingLog.Controls.Add(Me.GroupBox7)
        Me.TPTrackingLog.Controls.Add(Me.lblPermitAction)
        Me.TPTrackingLog.Controls.Add(Me.chbPNReady)
        Me.TPTrackingLog.Controls.Add(Me.Label30)
        Me.TPTrackingLog.Controls.Add(Me.GBAirProgramCodes)
        Me.TPTrackingLog.Controls.Add(Me.Label31)
        Me.TPTrackingLog.Controls.Add(Me.lblDateToDO)
        Me.TPTrackingLog.Controls.Add(Me.lblPublicAdvisory)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateToDO)
        Me.TPTrackingLog.Controls.Add(Me.cboCounty)
        Me.TPTrackingLog.Controls.Add(Me.lblDatetoBC)
        Me.TPTrackingLog.Controls.Add(Me.cboFacilityCity)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateToBC)
        Me.TPTrackingLog.Controls.Add(Me.lblCounty)
        Me.TPTrackingLog.Controls.Add(Me.lblEPAEnds)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateSent)
        Me.TPTrackingLog.Controls.Add(Me.DTPEPAEnds)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateReceived)
        Me.TPTrackingLog.Controls.Add(Me.lblEPAWaived)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateAcknowledge)
        Me.TPTrackingLog.Controls.Add(Me.DTPEPAWaived)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateReassigned)
        Me.TPTrackingLog.Controls.Add(Me.chbPAReady)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateAssigned)
        Me.TPTrackingLog.Controls.Add(Me.lblDraftIssued)
        Me.TPTrackingLog.Controls.Add(Me.DTPDatePAExpires)
        Me.TPTrackingLog.Controls.Add(Me.lblFinalAction)
        Me.TPTrackingLog.Controls.Add(Me.DTPDatePNExpires)
        Me.TPTrackingLog.Controls.Add(Me.lblDateToPM)
        Me.TPTrackingLog.Controls.Add(Me.DTPDeadline)
        Me.TPTrackingLog.Controls.Add(Me.lblDateToUC)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateToUC)
        Me.TPTrackingLog.Controls.Add(Me.lblDeadline)
        Me.TPTrackingLog.Controls.Add(Me.DTPDateToPM)
        Me.TPTrackingLog.Controls.Add(Me.lblDatePNExpires)
        Me.TPTrackingLog.Controls.Add(Me.DTPFinalAction)
        Me.TPTrackingLog.Controls.Add(Me.lblDatePAExpires)
        Me.TPTrackingLog.Controls.Add(Me.DTPDraftIssued)
        Me.TPTrackingLog.Controls.Add(Me.lblDateAcknowledge)
        Me.TPTrackingLog.Controls.Add(Me.txtPermitNumber)
        Me.TPTrackingLog.Controls.Add(Me.lblDateReassigned)
        Me.TPTrackingLog.Controls.Add(Me.cboPermitAction)
        Me.TPTrackingLog.Controls.Add(Me.lblDateAssigned)
        Me.TPTrackingLog.Controls.Add(Me.cboPublicAdvisory)
        Me.TPTrackingLog.Controls.Add(Me.txtFacilityZipCode)
        Me.TPTrackingLog.Controls.Add(Me.txtReasonAppSubmitted)
        Me.TPTrackingLog.Controls.Add(Me.txtFacilityStreetAddress)
        Me.TPTrackingLog.Controls.Add(Me.txtComments)
        Me.TPTrackingLog.Controls.Add(Me.txtSICCode)
        Me.TPTrackingLog.Controls.Add(Me.txtPlantDescription)
        Me.TPTrackingLog.Controls.Add(Me.Label42)
        Me.TPTrackingLog.Controls.Add(Me.Label63)
        Me.TPTrackingLog.Controls.Add(Me.lblClassification)
        Me.TPTrackingLog.Controls.Add(Me.cboOperationalStatus)
        Me.TPTrackingLog.Controls.Add(Me.cboClassification)
        Me.TPTrackingLog.Controls.Add(Me.lblOperationalStatus)
        Me.TPTrackingLog.Location = New System.Drawing.Point(4, 22)
        Me.TPTrackingLog.Name = "TPTrackingLog"
        Me.TPTrackingLog.Size = New System.Drawing.Size(784, 477)
        Me.TPTrackingLog.TabIndex = 0
        Me.TPTrackingLog.Text = "Tracking Log"
        Me.TPTrackingLog.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 262
        Me.Label1.Text = "Faciliy Name && Address"
        '
        'lblDated
        '
        Me.lblDated.AutoSize = True
        Me.lblDated.Location = New System.Drawing.Point(-1, 104)
        Me.lblDated.Name = "lblDated"
        Me.lblDated.Size = New System.Drawing.Size(36, 13)
        Me.lblDated.TabIndex = 262
        Me.lblDated.Text = "Dated"
        '
        'txtNAICSCode
        '
        Me.txtNAICSCode.Location = New System.Drawing.Point(575, 25)
        Me.txtNAICSCode.MaxLength = 10
        Me.txtNAICSCode.Name = "txtNAICSCode"
        Me.txtNAICSCode.ReadOnly = True
        Me.txtNAICSCode.Size = New System.Drawing.Size(52, 20)
        Me.txtNAICSCode.TabIndex = 9
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(502, 29)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(67, 13)
        Me.Label72.TabIndex = 8
        Me.Label72.Text = "NAICS Code"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(3, 25)
        Me.txtFacilityName.MaxLength = 100
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityName.TabIndex = 0
        '
        'llbPermitNumber
        '
        Me.llbPermitNumber.AutoSize = True
        Me.llbPermitNumber.Location = New System.Drawing.Point(432, 251)
        Me.llbPermitNumber.Name = "llbPermitNumber"
        Me.llbPermitNumber.Size = New System.Drawing.Size(76, 13)
        Me.llbPermitNumber.TabIndex = 33
        Me.llbPermitNumber.TabStop = True
        Me.llbPermitNumber.Text = "Permit Number"
        Me.llbPermitNumber.Visible = False
        '
        'txtDistrict
        '
        Me.txtDistrict.Location = New System.Drawing.Point(255, 49)
        Me.txtDistrict.Name = "txtDistrict"
        Me.txtDistrict.ReadOnly = True
        Me.txtDistrict.Size = New System.Drawing.Size(119, 20)
        Me.txtDistrict.TabIndex = 5
        '
        'lblPAReady
        '
        Me.lblPAReady.Location = New System.Drawing.Point(103, 249)
        Me.lblPAReady.Name = "lblPAReady"
        Me.lblPAReady.Size = New System.Drawing.Size(96, 18)
        Me.lblPAReady.TabIndex = 375
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(213, 51)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 255
        Me.Label7.Text = "District"
        '
        'lblPNReady
        '
        Me.lblPNReady.Location = New System.Drawing.Point(331, 173)
        Me.lblPNReady.Name = "lblPNReady"
        Me.lblPNReady.Size = New System.Drawing.Size(95, 18)
        Me.lblPNReady.TabIndex = 374
        '
        'lblReceived
        '
        Me.lblReceived.AutoSize = True
        Me.lblReceived.Location = New System.Drawing.Point(-1, 125)
        Me.lblReceived.Name = "lblReceived"
        Me.lblReceived.Size = New System.Drawing.Size(53, 13)
        Me.lblReceived.TabIndex = 263
        Me.lblReceived.Text = "Received"
        '
        'GBOther
        '
        Me.GBOther.Controls.Add(Me.chbHAPsMajor)
        Me.GBOther.Controls.Add(Me.chbNSRMajor)
        Me.GBOther.Location = New System.Drawing.Point(639, 326)
        Me.GBOther.Name = "GBOther"
        Me.GBOther.Size = New System.Drawing.Size(136, 64)
        Me.GBOther.TabIndex = 39
        Me.GBOther.TabStop = False
        Me.GBOther.Text = "Other"
        '
        'chbHAPsMajor
        '
        Me.chbHAPsMajor.AutoSize = True
        Me.chbHAPsMajor.Enabled = False
        Me.chbHAPsMajor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbHAPsMajor.Location = New System.Drawing.Point(6, 38)
        Me.chbHAPsMajor.Margin = New System.Windows.Forms.Padding(2)
        Me.chbHAPsMajor.Name = "chbHAPsMajor"
        Me.chbHAPsMajor.Size = New System.Drawing.Size(82, 17)
        Me.chbHAPsMajor.TabIndex = 2
        Me.chbHAPsMajor.Text = "HAPs Major"
        '
        'chbNSRMajor
        '
        Me.chbNSRMajor.AutoSize = True
        Me.chbNSRMajor.Enabled = False
        Me.chbNSRMajor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbNSRMajor.Location = New System.Drawing.Point(6, 18)
        Me.chbNSRMajor.Margin = New System.Windows.Forms.Padding(2)
        Me.chbNSRMajor.Name = "chbNSRMajor"
        Me.chbNSRMajor.Size = New System.Drawing.Size(105, 17)
        Me.chbNSRMajor.TabIndex = 1
        Me.chbNSRMajor.Text = "NSR/PSD Major"
        '
        'lblPermitNumber
        '
        Me.lblPermitNumber.AutoSize = True
        Me.lblPermitNumber.Location = New System.Drawing.Point(432, 251)
        Me.lblPermitNumber.Name = "lblPermitNumber"
        Me.lblPermitNumber.Size = New System.Drawing.Size(76, 13)
        Me.lblPermitNumber.TabIndex = 274
        Me.lblPermitNumber.Text = "Permit Number"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txt1HourOzone)
        Me.GroupBox7.Controls.Add(Me.Label25)
        Me.GroupBox7.Controls.Add(Me.txtPM)
        Me.GroupBox7.Controls.Add(Me.txt8HROzone)
        Me.GroupBox7.Controls.Add(Me.Label97)
        Me.GroupBox7.Controls.Add(Me.Label96)
        Me.GroupBox7.Location = New System.Drawing.Point(639, 19)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(136, 90)
        Me.GroupBox7.TabIndex = 37
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Non attainment"
        '
        'txt1HourOzone
        '
        Me.txt1HourOzone.BackColor = System.Drawing.SystemColors.Control
        Me.txt1HourOzone.Location = New System.Drawing.Point(66, 16)
        Me.txt1HourOzone.Name = "txt1HourOzone"
        Me.txt1HourOzone.ReadOnly = True
        Me.txt1HourOzone.Size = New System.Drawing.Size(69, 20)
        Me.txt1HourOzone.TabIndex = 0
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(3, 19)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(60, 13)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "1-hr ozone:"
        '
        'txtPM
        '
        Me.txtPM.BackColor = System.Drawing.SystemColors.Control
        Me.txtPM.Location = New System.Drawing.Point(66, 58)
        Me.txtPM.Name = "txtPM"
        Me.txtPM.ReadOnly = True
        Me.txtPM.Size = New System.Drawing.Size(69, 20)
        Me.txtPM.TabIndex = 2
        '
        'txt8HROzone
        '
        Me.txt8HROzone.BackColor = System.Drawing.SystemColors.Control
        Me.txt8HROzone.Location = New System.Drawing.Point(66, 37)
        Me.txt8HROzone.Name = "txt8HROzone"
        Me.txt8HROzone.ReadOnly = True
        Me.txt8HROzone.Size = New System.Drawing.Size(69, 20)
        Me.txt8HROzone.TabIndex = 1
        '
        'Label97
        '
        Me.Label97.AutoSize = True
        Me.Label97.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(3, 61)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(44, 13)
        Me.Label97.TabIndex = 2
        Me.Label97.Text = "PM 2.5:"
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(3, 40)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(60, 13)
        Me.Label96.TabIndex = 1
        Me.Label96.Text = "8-hr ozone:"
        '
        'lblPermitAction
        '
        Me.lblPermitAction.AutoSize = True
        Me.lblPermitAction.Location = New System.Drawing.Point(432, 278)
        Me.lblPermitAction.Name = "lblPermitAction"
        Me.lblPermitAction.Size = New System.Drawing.Size(64, 13)
        Me.lblPermitAction.TabIndex = 275
        Me.lblPermitAction.Text = "Action Type"
        '
        'chbPNReady
        '
        Me.chbPNReady.AutoSize = True
        Me.chbPNReady.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPNReady.Enabled = False
        Me.chbPNReady.Location = New System.Drawing.Point(205, 172)
        Me.chbPNReady.Name = "chbPNReady"
        Me.chbPNReady.Size = New System.Drawing.Size(120, 17)
        Me.chbPNReady.TabIndex = 23
        Me.chbPNReady.Text = "PN Ready               "
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(0, 299)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(201, 13)
        Me.Label30.TabIndex = 278
        Me.Label30.Text = "Reason for Application (on Public Notice)"
        '
        'GBAirProgramCodes
        '
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_RMP)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_0)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_6)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_7)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_8)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_9)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_M)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_V)
        Me.GBAirProgramCodes.Controls.Add(Me.chbCDS_A)
        Me.GBAirProgramCodes.Location = New System.Drawing.Point(639, 115)
        Me.GBAirProgramCodes.Name = "GBAirProgramCodes"
        Me.GBAirProgramCodes.Size = New System.Drawing.Size(136, 205)
        Me.GBAirProgramCodes.TabIndex = 38
        Me.GBAirProgramCodes.TabStop = False
        Me.GBAirProgramCodes.Text = "Air Program Codes"
        '
        'chbCDS_RMP
        '
        Me.chbCDS_RMP.AutoSize = True
        Me.chbCDS_RMP.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chbCDS_RMP.Enabled = False
        Me.chbCDS_RMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbCDS_RMP.Location = New System.Drawing.Point(6, 179)
        Me.chbCDS_RMP.Margin = New System.Windows.Forms.Padding(2)
        Me.chbCDS_RMP.Name = "chbCDS_RMP"
        Me.chbCDS_RMP.Size = New System.Drawing.Size(136, 17)
        Me.chbCDS_RMP.TabIndex = 8
        Me.chbCDS_RMP.Text = "Risk Management Plan"
        '
        'chbCDS_0
        '
        Me.chbCDS_0.AutoSize = True
        Me.chbCDS_0.Checked = True
        Me.chbCDS_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbCDS_0.Enabled = False
        Me.chbCDS_0.Location = New System.Drawing.Point(6, 19)
        Me.chbCDS_0.Name = "chbCDS_0"
        Me.chbCDS_0.Size = New System.Drawing.Size(43, 17)
        Me.chbCDS_0.TabIndex = 0
        Me.chbCDS_0.Text = "SIP"
        '
        'chbCDS_6
        '
        Me.chbCDS_6.AutoSize = True
        Me.chbCDS_6.Enabled = False
        Me.chbCDS_6.Location = New System.Drawing.Point(6, 39)
        Me.chbCDS_6.Name = "chbCDS_6"
        Me.chbCDS_6.Size = New System.Drawing.Size(48, 17)
        Me.chbCDS_6.TabIndex = 1
        Me.chbCDS_6.Text = "PSD"
        '
        'chbCDS_7
        '
        Me.chbCDS_7.AutoSize = True
        Me.chbCDS_7.Enabled = False
        Me.chbCDS_7.Location = New System.Drawing.Point(6, 59)
        Me.chbCDS_7.Name = "chbCDS_7"
        Me.chbCDS_7.Size = New System.Drawing.Size(49, 17)
        Me.chbCDS_7.TabIndex = 2
        Me.chbCDS_7.Text = "NSR"
        '
        'chbCDS_8
        '
        Me.chbCDS_8.AutoSize = True
        Me.chbCDS_8.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chbCDS_8.Enabled = False
        Me.chbCDS_8.Location = New System.Drawing.Point(6, 78)
        Me.chbCDS_8.Name = "chbCDS_8"
        Me.chbCDS_8.Size = New System.Drawing.Size(113, 17)
        Me.chbCDS_8.TabIndex = 3
        Me.chbCDS_8.Text = "NESHAP (Part 61)"
        '
        'chbCDS_9
        '
        Me.chbCDS_9.AutoSize = True
        Me.chbCDS_9.Enabled = False
        Me.chbCDS_9.Location = New System.Drawing.Point(6, 99)
        Me.chbCDS_9.Name = "chbCDS_9"
        Me.chbCDS_9.Size = New System.Drawing.Size(98, 17)
        Me.chbCDS_9.TabIndex = 4
        Me.chbCDS_9.Text = "NSPS (Part 60)"
        '
        'chbCDS_M
        '
        Me.chbCDS_M.AutoSize = True
        Me.chbCDS_M.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chbCDS_M.Enabled = False
        Me.chbCDS_M.Location = New System.Drawing.Point(6, 119)
        Me.chbCDS_M.Name = "chbCDS_M"
        Me.chbCDS_M.Size = New System.Drawing.Size(99, 17)
        Me.chbCDS_M.TabIndex = 5
        Me.chbCDS_M.Text = "MACT (Part 63)"
        '
        'chbCDS_V
        '
        Me.chbCDS_V.AutoSize = True
        Me.chbCDS_V.Enabled = False
        Me.chbCDS_V.Location = New System.Drawing.Point(6, 140)
        Me.chbCDS_V.Name = "chbCDS_V"
        Me.chbCDS_V.Size = New System.Drawing.Size(99, 17)
        Me.chbCDS_V.TabIndex = 6
        Me.chbCDS_V.Text = "Title V (Part 70)"
        '
        'chbCDS_A
        '
        Me.chbCDS_A.AutoSize = True
        Me.chbCDS_A.Enabled = False
        Me.chbCDS_A.Location = New System.Drawing.Point(6, 160)
        Me.chbCDS_A.Name = "chbCDS_A"
        Me.chbCDS_A.Size = New System.Drawing.Size(108, 17)
        Me.chbCDS_A.TabIndex = 7
        Me.chbCDS_A.Text = "Acid Precipitation"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(-1, 358)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(56, 13)
        Me.Label31.TabIndex = 279
        Me.Label31.Text = "Comments"
        '
        'lblDateToDO
        '
        Me.lblDateToDO.AutoSize = True
        Me.lblDateToDO.Location = New System.Drawing.Point(432, 175)
        Me.lblDateToDO.Name = "lblDateToDO"
        Me.lblDateToDO.Size = New System.Drawing.Size(61, 13)
        Me.lblDateToDO.TabIndex = 370
        Me.lblDateToDO.Text = "Date to DO"
        '
        'lblPublicAdvisory
        '
        Me.lblPublicAdvisory.AutoSize = True
        Me.lblPublicAdvisory.Location = New System.Drawing.Point(-1, 225)
        Me.lblPublicAdvisory.Name = "lblPublicAdvisory"
        Me.lblPublicAdvisory.Size = New System.Drawing.Size(79, 13)
        Me.lblPublicAdvisory.TabIndex = 280
        Me.lblPublicAdvisory.Text = "Public Advisory"
        '
        'DTPDateToDO
        '
        Me.DTPDateToDO.Checked = False
        Me.DTPDateToDO.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateToDO.Enabled = False
        Me.DTPDateToDO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateToDO.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateToDO.Location = New System.Drawing.Point(516, 172)
        Me.DTPDateToDO.Name = "DTPDateToDO"
        Me.DTPDateToDO.ShowCheckBox = True
        Me.DTPDateToDO.Size = New System.Drawing.Size(116, 21)
        Me.DTPDateToDO.TabIndex = 29
        Me.DTPDateToDO.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'cboCounty
        '
        Me.cboCounty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCounty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCounty.Enabled = False
        Me.cboCounty.Location = New System.Drawing.Point(255, 25)
        Me.cboCounty.Name = "cboCounty"
        Me.cboCounty.Size = New System.Drawing.Size(119, 21)
        Me.cboCounty.TabIndex = 4
        '
        'lblDatetoBC
        '
        Me.lblDatetoBC.AutoSize = True
        Me.lblDatetoBC.Location = New System.Drawing.Point(205, 278)
        Me.lblDatetoBC.Name = "lblDatetoBC"
        Me.lblDatetoBC.Size = New System.Drawing.Size(59, 13)
        Me.lblDatetoBC.TabIndex = 368
        Me.lblDatetoBC.Text = "Date to BC"
        '
        'cboFacilityCity
        '
        Me.cboFacilityCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFacilityCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFacilityCity.BackColor = System.Drawing.SystemColors.Window
        Me.cboFacilityCity.Enabled = False
        Me.cboFacilityCity.Location = New System.Drawing.Point(3, 73)
        Me.cboFacilityCity.MaxLength = 50
        Me.cboFacilityCity.Name = "cboFacilityCity"
        Me.cboFacilityCity.Size = New System.Drawing.Size(128, 21)
        Me.cboFacilityCity.TabIndex = 2
        '
        'DTPDateToBC
        '
        Me.DTPDateToBC.Checked = False
        Me.DTPDateToBC.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateToBC.Enabled = False
        Me.DTPDateToBC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateToBC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateToBC.Location = New System.Drawing.Point(309, 273)
        Me.DTPDateToBC.Name = "DTPDateToBC"
        Me.DTPDateToBC.ShowCheckBox = True
        Me.DTPDateToBC.Size = New System.Drawing.Size(116, 21)
        Me.DTPDateToBC.TabIndex = 27
        Me.DTPDateToBC.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblCounty
        '
        Me.lblCounty.AutoSize = True
        Me.lblCounty.Location = New System.Drawing.Point(213, 29)
        Me.lblCounty.Name = "lblCounty"
        Me.lblCounty.Size = New System.Drawing.Size(40, 13)
        Me.lblCounty.TabIndex = 297
        Me.lblCounty.Text = "County"
        '
        'lblEPAEnds
        '
        Me.lblEPAEnds.AutoSize = True
        Me.lblEPAEnds.Location = New System.Drawing.Point(205, 251)
        Me.lblEPAEnds.Name = "lblEPAEnds"
        Me.lblEPAEnds.Size = New System.Drawing.Size(90, 13)
        Me.lblEPAEnds.TabIndex = 366
        Me.lblEPAEnds.Text = "EPA 45-day Ends"
        '
        'DTPDateSent
        '
        Me.DTPDateSent.Checked = False
        Me.DTPDateSent.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateSent.Enabled = False
        Me.DTPDateSent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateSent.Location = New System.Drawing.Point(79, 99)
        Me.DTPDateSent.Name = "DTPDateSent"
        Me.DTPDateSent.Size = New System.Drawing.Size(117, 21)
        Me.DTPDateSent.TabIndex = 12
        Me.DTPDateSent.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPEPAEnds
        '
        Me.DTPEPAEnds.Checked = False
        Me.DTPEPAEnds.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEPAEnds.Enabled = False
        Me.DTPEPAEnds.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPEPAEnds.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEPAEnds.Location = New System.Drawing.Point(309, 246)
        Me.DTPEPAEnds.Name = "DTPEPAEnds"
        Me.DTPEPAEnds.ShowCheckBox = True
        Me.DTPEPAEnds.Size = New System.Drawing.Size(116, 21)
        Me.DTPEPAEnds.TabIndex = 26
        Me.DTPEPAEnds.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPDateReceived
        '
        Me.DTPDateReceived.Checked = False
        Me.DTPDateReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateReceived.Enabled = False
        Me.DTPDateReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateReceived.Location = New System.Drawing.Point(79, 122)
        Me.DTPDateReceived.Name = "DTPDateReceived"
        Me.DTPDateReceived.Size = New System.Drawing.Size(117, 21)
        Me.DTPDateReceived.TabIndex = 13
        '
        'lblEPAWaived
        '
        Me.lblEPAWaived.AutoSize = True
        Me.lblEPAWaived.Location = New System.Drawing.Point(205, 226)
        Me.lblEPAWaived.Name = "lblEPAWaived"
        Me.lblEPAWaived.Size = New System.Drawing.Size(103, 13)
        Me.lblEPAWaived.TabIndex = 364
        Me.lblEPAWaived.Text = "EPA 45-day Waived"
        '
        'DTPDateAcknowledge
        '
        Me.DTPDateAcknowledge.Checked = False
        Me.DTPDateAcknowledge.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateAcknowledge.Enabled = False
        Me.DTPDateAcknowledge.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateAcknowledge.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateAcknowledge.Location = New System.Drawing.Point(79, 194)
        Me.DTPDateAcknowledge.Name = "DTPDateAcknowledge"
        Me.DTPDateAcknowledge.ShowCheckBox = True
        Me.DTPDateAcknowledge.Size = New System.Drawing.Size(116, 21)
        Me.DTPDateAcknowledge.TabIndex = 16
        Me.DTPDateAcknowledge.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPEPAWaived
        '
        Me.DTPEPAWaived.Checked = False
        Me.DTPEPAWaived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEPAWaived.Enabled = False
        Me.DTPEPAWaived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPEPAWaived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEPAWaived.Location = New System.Drawing.Point(309, 221)
        Me.DTPEPAWaived.Name = "DTPEPAWaived"
        Me.DTPEPAWaived.ShowCheckBox = True
        Me.DTPEPAWaived.Size = New System.Drawing.Size(116, 21)
        Me.DTPEPAWaived.TabIndex = 25
        Me.DTPEPAWaived.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPDateReassigned
        '
        Me.DTPDateReassigned.Checked = False
        Me.DTPDateReassigned.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateReassigned.Enabled = False
        Me.DTPDateReassigned.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateReassigned.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateReassigned.Location = New System.Drawing.Point(79, 170)
        Me.DTPDateReassigned.Name = "DTPDateReassigned"
        Me.DTPDateReassigned.ShowCheckBox = True
        Me.DTPDateReassigned.Size = New System.Drawing.Size(116, 21)
        Me.DTPDateReassigned.TabIndex = 15
        Me.DTPDateReassigned.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'chbPAReady
        '
        Me.chbPAReady.AutoSize = True
        Me.chbPAReady.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPAReady.Enabled = False
        Me.chbPAReady.Location = New System.Drawing.Point(-2, 251)
        Me.chbPAReady.Name = "chbPAReady"
        Me.chbPAReady.Size = New System.Drawing.Size(98, 17)
        Me.chbPAReady.TabIndex = 18
        Me.chbPAReady.Text = "PA Ready        "
        '
        'DTPDateAssigned
        '
        Me.DTPDateAssigned.Checked = False
        Me.DTPDateAssigned.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateAssigned.Enabled = False
        Me.DTPDateAssigned.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateAssigned.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateAssigned.Location = New System.Drawing.Point(79, 146)
        Me.DTPDateAssigned.Name = "DTPDateAssigned"
        Me.DTPDateAssigned.ShowCheckBox = True
        Me.DTPDateAssigned.Size = New System.Drawing.Size(116, 21)
        Me.DTPDateAssigned.TabIndex = 14
        Me.DTPDateAssigned.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblDraftIssued
        '
        Me.lblDraftIssued.AutoSize = True
        Me.lblDraftIssued.Location = New System.Drawing.Point(205, 151)
        Me.lblDraftIssued.Name = "lblDraftIssued"
        Me.lblDraftIssued.Size = New System.Drawing.Size(64, 13)
        Me.lblDraftIssued.TabIndex = 361
        Me.lblDraftIssued.Text = "Draft Issued"
        '
        'DTPDatePAExpires
        '
        Me.DTPDatePAExpires.Checked = False
        Me.DTPDatePAExpires.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDatePAExpires.Enabled = False
        Me.DTPDatePAExpires.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDatePAExpires.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDatePAExpires.Location = New System.Drawing.Point(79, 273)
        Me.DTPDatePAExpires.Name = "DTPDatePAExpires"
        Me.DTPDatePAExpires.ShowCheckBox = True
        Me.DTPDatePAExpires.Size = New System.Drawing.Size(116, 21)
        Me.DTPDatePAExpires.TabIndex = 19
        Me.DTPDatePAExpires.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblFinalAction
        '
        Me.lblFinalAction.AutoSize = True
        Me.lblFinalAction.Location = New System.Drawing.Point(432, 199)
        Me.lblFinalAction.Name = "lblFinalAction"
        Me.lblFinalAction.Size = New System.Drawing.Size(62, 13)
        Me.lblFinalAction.TabIndex = 360
        Me.lblFinalAction.Text = "Final Action"
        '
        'DTPDatePNExpires
        '
        Me.DTPDatePNExpires.Checked = False
        Me.DTPDatePNExpires.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDatePNExpires.Enabled = False
        Me.DTPDatePNExpires.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDatePNExpires.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDatePNExpires.Location = New System.Drawing.Point(309, 194)
        Me.DTPDatePNExpires.Name = "DTPDatePNExpires"
        Me.DTPDatePNExpires.ShowCheckBox = True
        Me.DTPDatePNExpires.Size = New System.Drawing.Size(116, 21)
        Me.DTPDatePNExpires.TabIndex = 24
        Me.DTPDatePNExpires.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblDateToPM
        '
        Me.lblDateToPM.AutoSize = True
        Me.lblDateToPM.Location = New System.Drawing.Point(205, 127)
        Me.lblDateToPM.Name = "lblDateToPM"
        Me.lblDateToPM.Size = New System.Drawing.Size(61, 13)
        Me.lblDateToPM.TabIndex = 359
        Me.lblDateToPM.Text = "Date to PM"
        '
        'DTPDeadline
        '
        Me.DTPDeadline.CalendarMonthBackground = System.Drawing.SystemColors.Menu
        Me.DTPDeadline.Checked = False
        Me.DTPDeadline.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDeadline.Enabled = False
        Me.DTPDeadline.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDeadline.Location = New System.Drawing.Point(516, 223)
        Me.DTPDeadline.Name = "DTPDeadline"
        Me.DTPDeadline.ShowCheckBox = True
        Me.DTPDeadline.Size = New System.Drawing.Size(116, 21)
        Me.DTPDeadline.TabIndex = 31
        Me.DTPDeadline.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblDateToUC
        '
        Me.lblDateToUC.AutoSize = True
        Me.lblDateToUC.Location = New System.Drawing.Point(205, 104)
        Me.lblDateToUC.Name = "lblDateToUC"
        Me.lblDateToUC.Size = New System.Drawing.Size(60, 13)
        Me.lblDateToUC.TabIndex = 358
        Me.lblDateToUC.Text = "Date to UC"
        '
        'DTPDateToUC
        '
        Me.DTPDateToUC.Checked = False
        Me.DTPDateToUC.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateToUC.Enabled = False
        Me.DTPDateToUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateToUC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateToUC.Location = New System.Drawing.Point(309, 99)
        Me.DTPDateToUC.Name = "DTPDateToUC"
        Me.DTPDateToUC.ShowCheckBox = True
        Me.DTPDateToUC.Size = New System.Drawing.Size(116, 21)
        Me.DTPDateToUC.TabIndex = 20
        Me.DTPDateToUC.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblDeadline
        '
        Me.lblDeadline.AutoSize = True
        Me.lblDeadline.Location = New System.Drawing.Point(432, 226)
        Me.lblDeadline.Name = "lblDeadline"
        Me.lblDeadline.Size = New System.Drawing.Size(49, 13)
        Me.lblDeadline.TabIndex = 356
        Me.lblDeadline.Text = "Deadline"
        '
        'DTPDateToPM
        '
        Me.DTPDateToPM.Checked = False
        Me.DTPDateToPM.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateToPM.Enabled = False
        Me.DTPDateToPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateToPM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateToPM.Location = New System.Drawing.Point(309, 122)
        Me.DTPDateToPM.Name = "DTPDateToPM"
        Me.DTPDateToPM.ShowCheckBox = True
        Me.DTPDateToPM.Size = New System.Drawing.Size(116, 21)
        Me.DTPDateToPM.TabIndex = 21
        Me.DTPDateToPM.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblDatePNExpires
        '
        Me.lblDatePNExpires.AutoSize = True
        Me.lblDatePNExpires.Location = New System.Drawing.Point(207, 199)
        Me.lblDatePNExpires.Name = "lblDatePNExpires"
        Me.lblDatePNExpires.Size = New System.Drawing.Size(59, 13)
        Me.lblDatePNExpires.TabIndex = 355
        Me.lblDatePNExpires.Text = "PN Expires"
        '
        'DTPFinalAction
        '
        Me.DTPFinalAction.Checked = False
        Me.DTPFinalAction.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFinalAction.Enabled = False
        Me.DTPFinalAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPFinalAction.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFinalAction.Location = New System.Drawing.Point(516, 196)
        Me.DTPFinalAction.Name = "DTPFinalAction"
        Me.DTPFinalAction.ShowCheckBox = True
        Me.DTPFinalAction.Size = New System.Drawing.Size(116, 21)
        Me.DTPFinalAction.TabIndex = 30
        Me.DTPFinalAction.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblDatePAExpires
        '
        Me.lblDatePAExpires.AutoSize = True
        Me.lblDatePAExpires.Location = New System.Drawing.Point(-1, 278)
        Me.lblDatePAExpires.Name = "lblDatePAExpires"
        Me.lblDatePAExpires.Size = New System.Drawing.Size(58, 13)
        Me.lblDatePAExpires.TabIndex = 354
        Me.lblDatePAExpires.Text = "PA Expires"
        '
        'DTPDraftIssued
        '
        Me.DTPDraftIssued.Checked = False
        Me.DTPDraftIssued.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDraftIssued.Enabled = False
        Me.DTPDraftIssued.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDraftIssued.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDraftIssued.Location = New System.Drawing.Point(309, 146)
        Me.DTPDraftIssued.Name = "DTPDraftIssued"
        Me.DTPDraftIssued.ShowCheckBox = True
        Me.DTPDraftIssued.Size = New System.Drawing.Size(116, 21)
        Me.DTPDraftIssued.TabIndex = 22
        Me.DTPDraftIssued.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'lblDateAcknowledge
        '
        Me.lblDateAcknowledge.AutoSize = True
        Me.lblDateAcknowledge.Location = New System.Drawing.Point(-1, 197)
        Me.lblDateAcknowledge.Name = "lblDateAcknowledge"
        Me.lblDateAcknowledge.Size = New System.Drawing.Size(78, 13)
        Me.lblDateAcknowledge.TabIndex = 352
        Me.lblDateAcknowledge.Text = "Acknowledged"
        '
        'txtPermitNumber
        '
        Me.txtPermitNumber.Location = New System.Drawing.Point(508, 247)
        Me.txtPermitNumber.MaxLength = 20
        Me.txtPermitNumber.Name = "txtPermitNumber"
        Me.txtPermitNumber.ReadOnly = True
        Me.txtPermitNumber.Size = New System.Drawing.Size(125, 20)
        Me.txtPermitNumber.TabIndex = 32
        '
        'lblDateReassigned
        '
        Me.lblDateReassigned.AutoSize = True
        Me.lblDateReassigned.Location = New System.Drawing.Point(-1, 173)
        Me.lblDateReassigned.Name = "lblDateReassigned"
        Me.lblDateReassigned.Size = New System.Drawing.Size(63, 13)
        Me.lblDateReassigned.TabIndex = 351
        Me.lblDateReassigned.Text = "Reassigned"
        '
        'cboPermitAction
        '
        Me.cboPermitAction.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPermitAction.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPermitAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPermitAction.Enabled = False
        Me.cboPermitAction.Location = New System.Drawing.Point(508, 274)
        Me.cboPermitAction.Name = "cboPermitAction"
        Me.cboPermitAction.Size = New System.Drawing.Size(125, 21)
        Me.cboPermitAction.TabIndex = 34
        '
        'lblDateAssigned
        '
        Me.lblDateAssigned.AutoSize = True
        Me.lblDateAssigned.Location = New System.Drawing.Point(-1, 149)
        Me.lblDateAssigned.Name = "lblDateAssigned"
        Me.lblDateAssigned.Size = New System.Drawing.Size(50, 13)
        Me.lblDateAssigned.TabIndex = 350
        Me.lblDateAssigned.Text = "Assigned"
        '
        'cboPublicAdvisory
        '
        Me.cboPublicAdvisory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPublicAdvisory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPublicAdvisory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPublicAdvisory.Enabled = False
        Me.cboPublicAdvisory.Location = New System.Drawing.Point(79, 222)
        Me.cboPublicAdvisory.Name = "cboPublicAdvisory"
        Me.cboPublicAdvisory.Size = New System.Drawing.Size(117, 21)
        Me.cboPublicAdvisory.TabIndex = 17
        '
        'txtFacilityZipCode
        '
        Me.txtFacilityZipCode.Location = New System.Drawing.Point(137, 73)
        Me.txtFacilityZipCode.MaxLength = 10
        Me.txtFacilityZipCode.Name = "txtFacilityZipCode"
        Me.txtFacilityZipCode.ReadOnly = True
        Me.txtFacilityZipCode.Size = New System.Drawing.Size(74, 20)
        Me.txtFacilityZipCode.TabIndex = 3
        '
        'txtReasonAppSubmitted
        '
        Me.txtReasonAppSubmitted.AcceptsReturn = True
        Me.txtReasonAppSubmitted.Location = New System.Drawing.Point(18, 315)
        Me.txtReasonAppSubmitted.MaxLength = 4000
        Me.txtReasonAppSubmitted.Multiline = True
        Me.txtReasonAppSubmitted.Name = "txtReasonAppSubmitted"
        Me.txtReasonAppSubmitted.ReadOnly = True
        Me.txtReasonAppSubmitted.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReasonAppSubmitted.Size = New System.Drawing.Size(615, 40)
        Me.txtReasonAppSubmitted.TabIndex = 35
        '
        'txtFacilityStreetAddress
        '
        Me.txtFacilityStreetAddress.Location = New System.Drawing.Point(3, 49)
        Me.txtFacilityStreetAddress.MaxLength = 100
        Me.txtFacilityStreetAddress.Name = "txtFacilityStreetAddress"
        Me.txtFacilityStreetAddress.ReadOnly = True
        Me.txtFacilityStreetAddress.Size = New System.Drawing.Size(208, 20)
        Me.txtFacilityStreetAddress.TabIndex = 1
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = True
        Me.txtComments.Location = New System.Drawing.Point(18, 374)
        Me.txtComments.MaxLength = 4000
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(615, 46)
        Me.txtComments.TabIndex = 36
        '
        'txtSICCode
        '
        Me.txtSICCode.Location = New System.Drawing.Point(456, 25)
        Me.txtSICCode.MaxLength = 4
        Me.txtSICCode.Name = "txtSICCode"
        Me.txtSICCode.ReadOnly = True
        Me.txtSICCode.Size = New System.Drawing.Size(40, 20)
        Me.txtSICCode.TabIndex = 7
        '
        'txtPlantDescription
        '
        Me.txtPlantDescription.AcceptsReturn = True
        Me.txtPlantDescription.Location = New System.Drawing.Point(434, 115)
        Me.txtPlantDescription.MaxLength = 4000
        Me.txtPlantDescription.Multiline = True
        Me.txtPlantDescription.Name = "txtPlantDescription"
        Me.txtPlantDescription.ReadOnly = True
        Me.txtPlantDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlantDescription.Size = New System.Drawing.Size(199, 53)
        Me.txtPlantDescription.TabIndex = 28
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(380, 29)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(52, 13)
        Me.Label42.TabIndex = 333
        Me.Label42.Text = "SIC Code"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(431, 99)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(174, 13)
        Me.Label63.TabIndex = 344
        Me.Label63.Text = "Plant Description (on Public Notice)"
        '
        'lblClassification
        '
        Me.lblClassification.AutoSize = True
        Me.lblClassification.Location = New System.Drawing.Point(380, 75)
        Me.lblClassification.Name = "lblClassification"
        Me.lblClassification.Size = New System.Drawing.Size(68, 13)
        Me.lblClassification.TabIndex = 334
        Me.lblClassification.Text = "Classification"
        '
        'cboOperationalStatus
        '
        Me.cboOperationalStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperationalStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperationalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperationalStatus.Enabled = False
        Me.cboOperationalStatus.Location = New System.Drawing.Point(456, 49)
        Me.cboOperationalStatus.Name = "cboOperationalStatus"
        Me.cboOperationalStatus.Size = New System.Drawing.Size(177, 21)
        Me.cboOperationalStatus.TabIndex = 10
        '
        'cboClassification
        '
        Me.cboClassification.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboClassification.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboClassification.Enabled = False
        Me.cboClassification.Location = New System.Drawing.Point(456, 73)
        Me.cboClassification.Name = "cboClassification"
        Me.cboClassification.Size = New System.Drawing.Size(177, 21)
        Me.cboClassification.TabIndex = 11
        '
        'lblOperationalStatus
        '
        Me.lblOperationalStatus.AutoSize = True
        Me.lblOperationalStatus.Location = New System.Drawing.Point(380, 51)
        Me.lblOperationalStatus.Name = "lblOperationalStatus"
        Me.lblOperationalStatus.Size = New System.Drawing.Size(54, 13)
        Me.lblOperationalStatus.TabIndex = 335
        Me.lblOperationalStatus.Text = "Op Status"
        '
        'TCApplicationTrackingLog
        '
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPTrackingLog)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPReviews)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPApplicationHistory)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPInformationRequests)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPWebPublisher)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPOtherInfo)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPContactInformation)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPDocuments)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPSubPartEditor)
        Me.TCApplicationTrackingLog.Controls.Add(Me.TPFees)
        Me.TCApplicationTrackingLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCApplicationTrackingLog.Location = New System.Drawing.Point(0, 130)
        Me.TCApplicationTrackingLog.Name = "TCApplicationTrackingLog"
        Me.TCApplicationTrackingLog.SelectedIndex = 0
        Me.TCApplicationTrackingLog.Size = New System.Drawing.Size(792, 503)
        Me.TCApplicationTrackingLog.TabIndex = 2
        '
        'TPFees
        '
        Me.TPFees.Controls.Add(Me.pnlFeeDataFinalized)
        Me.TPFees.Controls.Add(Me.lblFeeChangesNotAllowedWarning)
        Me.TPFees.Controls.Add(Me.SplitContainer1)
        Me.TPFees.Controls.Add(Me.txtAppFeeAmount)
        Me.TPFees.Controls.Add(Me.txtExpFeeAmount)
        Me.TPFees.Controls.Add(Me.chbAppFee)
        Me.TPFees.Controls.Add(Me.chbExpFee)
        Me.TPFees.Controls.Add(Me.txtExpFeeOverrideReason)
        Me.TPFees.Controls.Add(Me.txtAppFeeOverrideReason)
        Me.TPFees.Controls.Add(Me.chbExpFeeOverride)
        Me.TPFees.Controls.Add(Me.chbFeeDataFinalized)
        Me.TPFees.Controls.Add(Me.chbAppFeeOverride)
        Me.TPFees.Controls.Add(Me.txtFeeTotal)
        Me.TPFees.Controls.Add(Me.lblExpFee)
        Me.TPFees.Controls.Add(Me.lblAppFee)
        Me.TPFees.Controls.Add(Me.lblTotalFee)
        Me.TPFees.Controls.Add(Me.cmbExpFeeType)
        Me.TPFees.Controls.Add(Me.cmbAppFeeType)
        Me.TPFees.Location = New System.Drawing.Point(4, 22)
        Me.TPFees.Name = "TPFees"
        Me.TPFees.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFees.Size = New System.Drawing.Size(784, 477)
        Me.TPFees.TabIndex = 9
        Me.TPFees.Text = "Fees"
        Me.TPFees.UseVisualStyleBackColor = True
        '
        'pnlFeeDataFinalized
        '
        Me.pnlFeeDataFinalized.Controls.Add(Me.lblFeeDataFinalized)
        Me.pnlFeeDataFinalized.Controls.Add(Me.lklGenerateEmail)
        Me.pnlFeeDataFinalized.Controls.Add(Me.dtpFacilityFeeNotified)
        Me.pnlFeeDataFinalized.Controls.Add(Me.lblFacilityFeeNotified)
        Me.pnlFeeDataFinalized.Controls.Add(Me.dtpFeeDataFinalized)
        Me.pnlFeeDataFinalized.Location = New System.Drawing.Point(11, 231)
        Me.pnlFeeDataFinalized.Name = "pnlFeeDataFinalized"
        Me.pnlFeeDataFinalized.Size = New System.Drawing.Size(280, 76)
        Me.pnlFeeDataFinalized.TabIndex = 3
        Me.pnlFeeDataFinalized.Visible = False
        '
        'lblFeeDataFinalized
        '
        Me.lblFeeDataFinalized.AutoSize = True
        Me.lblFeeDataFinalized.Location = New System.Drawing.Point(-3, 4)
        Me.lblFeeDataFinalized.Name = "lblFeeDataFinalized"
        Me.lblFeeDataFinalized.Size = New System.Drawing.Size(95, 13)
        Me.lblFeeDataFinalized.TabIndex = 9
        Me.lblFeeDataFinalized.Text = "Fee Data Finalized"
        '
        'lklGenerateEmail
        '
        Me.lklGenerateEmail.AutoSize = True
        Me.lklGenerateEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lklGenerateEmail.Location = New System.Drawing.Point(-3, 25)
        Me.lklGenerateEmail.Name = "lklGenerateEmail"
        Me.lklGenerateEmail.Size = New System.Drawing.Size(78, 13)
        Me.lklGenerateEmail.TabIndex = 12
        Me.lklGenerateEmail.TabStop = True
        Me.lklGenerateEmail.Text = "Generate email"
        '
        'dtpFacilityFeeNotified
        '
        Me.dtpFacilityFeeNotified.Checked = False
        Me.dtpFacilityFeeNotified.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFacilityFeeNotified.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFacilityFeeNotified.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFacilityFeeNotified.Location = New System.Drawing.Point(164, 44)
        Me.dtpFacilityFeeNotified.Name = "dtpFacilityFeeNotified"
        Me.dtpFacilityFeeNotified.ShowCheckBox = True
        Me.dtpFacilityFeeNotified.Size = New System.Drawing.Size(116, 21)
        Me.dtpFacilityFeeNotified.TabIndex = 14
        '
        'lblFacilityFeeNotified
        '
        Me.lblFacilityFeeNotified.AutoSize = True
        Me.lblFacilityFeeNotified.Location = New System.Drawing.Point(-3, 48)
        Me.lblFacilityFeeNotified.Name = "lblFacilityFeeNotified"
        Me.lblFacilityFeeNotified.Size = New System.Drawing.Size(78, 13)
        Me.lblFacilityFeeNotified.TabIndex = 9
        Me.lblFacilityFeeNotified.Text = "Facility Notified"
        '
        'dtpFeeDataFinalized
        '
        Me.dtpFeeDataFinalized.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFeeDataFinalized.Enabled = False
        Me.dtpFeeDataFinalized.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFeeDataFinalized.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFeeDataFinalized.Location = New System.Drawing.Point(182, 0)
        Me.dtpFeeDataFinalized.Name = "dtpFeeDataFinalized"
        Me.dtpFeeDataFinalized.Size = New System.Drawing.Size(98, 21)
        Me.dtpFeeDataFinalized.TabIndex = 13
        '
        'lblFeeChangesNotAllowedWarning
        '
        Me.lblFeeChangesNotAllowedWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblFeeChangesNotAllowedWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFeeChangesNotAllowedWarning.Location = New System.Drawing.Point(8, 253)
        Me.lblFeeChangesNotAllowedWarning.Name = "lblFeeChangesNotAllowedWarning"
        Me.lblFeeChangesNotAllowedWarning.Size = New System.Drawing.Size(410, 71)
        Me.lblFeeChangesNotAllowedWarning.TabIndex = 16
        Me.lblFeeChangesNotAllowedWarning.Text = "An invoice will be generated when this application is saved. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Fees cannot be c" &
    "hanged after an invoice has been generated unless the invoice is voided first."
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.SplitContainer1.Location = New System.Drawing.Point(-3, 340)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(787, 137)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 17
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel2.Controls.Add(Me.lblInvoices)
        Me.Panel2.Controls.Add(Me.txtFeeTotalInvoiced)
        Me.Panel2.Controls.Add(Me.lblFeeTotalInvoiced)
        Me.Panel2.Controls.Add(Me.dgvApplicationInvoices)
        Me.Panel2.Location = New System.Drawing.Point(0, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(421, 137)
        Me.Panel2.TabIndex = 0
        '
        'lblInvoices
        '
        Me.lblInvoices.AutoSize = True
        Me.lblInvoices.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblInvoices.Location = New System.Drawing.Point(10, 10)
        Me.lblInvoices.Name = "lblInvoices"
        Me.lblInvoices.Size = New System.Drawing.Size(100, 13)
        Me.lblInvoices.TabIndex = 11
        Me.lblInvoices.Text = "Invoices Generated"
        '
        'txtFeeTotalInvoiced
        '
        Me.txtFeeTotalInvoiced.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtFeeTotalInvoiced.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFeeTotalInvoiced.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtFeeTotalInvoiced.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFeeTotalInvoiced.Cue = "$ 0"
        Me.txtFeeTotalInvoiced.Location = New System.Drawing.Point(346, 10)
        Me.txtFeeTotalInvoiced.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtFeeTotalInvoiced.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtFeeTotalInvoiced.Name = "txtFeeTotalInvoiced"
        Me.txtFeeTotalInvoiced.ReadOnly = True
        Me.txtFeeTotalInvoiced.Size = New System.Drawing.Size(57, 13)
        Me.txtFeeTotalInvoiced.TabIndex = 15
        Me.txtFeeTotalInvoiced.Text = "$0"
        Me.txtFeeTotalInvoiced.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFeeTotalInvoiced
        '
        Me.lblFeeTotalInvoiced.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFeeTotalInvoiced.AutoSize = True
        Me.lblFeeTotalInvoiced.Location = New System.Drawing.Point(306, 10)
        Me.lblFeeTotalInvoiced.Name = "lblFeeTotalInvoiced"
        Me.lblFeeTotalInvoiced.Size = New System.Drawing.Size(34, 13)
        Me.lblFeeTotalInvoiced.TabIndex = 2
        Me.lblFeeTotalInvoiced.Text = "Total:"
        '
        'dgvApplicationInvoices
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvApplicationInvoices.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvApplicationInvoices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvApplicationInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvApplicationInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvApplicationInvoices.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvApplicationInvoices.LinkifyColumnByName = Nothing
        Me.dgvApplicationInvoices.LinkifyFirstColumn = True
        Me.dgvApplicationInvoices.Location = New System.Drawing.Point(10, 29)
        Me.dgvApplicationInvoices.Name = "dgvApplicationInvoices"
        Me.dgvApplicationInvoices.ResultsCountLabel = Nothing
        Me.dgvApplicationInvoices.ResultsCountLabelFormat = "{0} found"
        Me.dgvApplicationInvoices.Size = New System.Drawing.Size(396, 99)
        Me.dgvApplicationInvoices.StandardTab = True
        Me.dgvApplicationInvoices.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.Controls.Add(Me.lblPayments)
        Me.Panel1.Controls.Add(Me.txtFeeTotalPaid)
        Me.Panel1.Controls.Add(Me.lblFeeTotalPaid)
        Me.Panel1.Controls.Add(Me.dgvApplicationPayments)
        Me.Panel1.Location = New System.Drawing.Point(0, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(364, 137)
        Me.Panel1.TabIndex = 0
        '
        'lblPayments
        '
        Me.lblPayments.AutoSize = True
        Me.lblPayments.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblPayments.Location = New System.Drawing.Point(16, 10)
        Me.lblPayments.Name = "lblPayments"
        Me.lblPayments.Size = New System.Drawing.Size(53, 13)
        Me.lblPayments.TabIndex = 11
        Me.lblPayments.Text = "Payments"
        '
        'txtFeeTotalPaid
        '
        Me.txtFeeTotalPaid.Amount = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.txtFeeTotalPaid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFeeTotalPaid.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtFeeTotalPaid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFeeTotalPaid.Cue = "$ 0"
        Me.txtFeeTotalPaid.Location = New System.Drawing.Point(296, 10)
        Me.txtFeeTotalPaid.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtFeeTotalPaid.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtFeeTotalPaid.Name = "txtFeeTotalPaid"
        Me.txtFeeTotalPaid.ReadOnly = True
        Me.txtFeeTotalPaid.Size = New System.Drawing.Size(57, 13)
        Me.txtFeeTotalPaid.TabIndex = 15
        Me.txtFeeTotalPaid.Text = "$0"
        Me.txtFeeTotalPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFeeTotalPaid
        '
        Me.lblFeeTotalPaid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFeeTotalPaid.AutoSize = True
        Me.lblFeeTotalPaid.Location = New System.Drawing.Point(256, 10)
        Me.lblFeeTotalPaid.Name = "lblFeeTotalPaid"
        Me.lblFeeTotalPaid.Size = New System.Drawing.Size(34, 13)
        Me.lblFeeTotalPaid.TabIndex = 2
        Me.lblFeeTotalPaid.Text = "Total:"
        '
        'dgvApplicationPayments
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvApplicationPayments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvApplicationPayments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvApplicationPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvApplicationPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvApplicationPayments.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvApplicationPayments.LinkifyColumnByName = Nothing
        Me.dgvApplicationPayments.LinkifyFirstColumn = True
        Me.dgvApplicationPayments.Location = New System.Drawing.Point(16, 29)
        Me.dgvApplicationPayments.Name = "dgvApplicationPayments"
        Me.dgvApplicationPayments.ResultsCountLabel = Nothing
        Me.dgvApplicationPayments.ResultsCountLabelFormat = "{0} found"
        Me.dgvApplicationPayments.Size = New System.Drawing.Size(340, 99)
        Me.dgvApplicationPayments.StandardTab = True
        Me.dgvApplicationPayments.TabIndex = 15
        '
        'txtAppFeeAmount
        '
        Me.txtAppFeeAmount.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAppFeeAmount.Cue = "$ 0"
        Me.txtAppFeeAmount.Location = New System.Drawing.Point(214, 71)
        Me.txtAppFeeAmount.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtAppFeeAmount.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtAppFeeAmount.Name = "txtAppFeeAmount"
        Me.txtAppFeeAmount.ReadOnly = True
        Me.txtAppFeeAmount.Size = New System.Drawing.Size(77, 20)
        Me.txtAppFeeAmount.TabIndex = 4
        Me.txtAppFeeAmount.Text = "$0"
        Me.txtAppFeeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExpFeeAmount
        '
        Me.txtExpFeeAmount.Amount = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtExpFeeAmount.Cue = "$ 0"
        Me.txtExpFeeAmount.Location = New System.Drawing.Point(214, 162)
        Me.txtExpFeeAmount.MaxValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtExpFeeAmount.MinValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtExpFeeAmount.Name = "txtExpFeeAmount"
        Me.txtExpFeeAmount.ReadOnly = True
        Me.txtExpFeeAmount.Size = New System.Drawing.Size(77, 20)
        Me.txtExpFeeAmount.TabIndex = 9
        Me.txtExpFeeAmount.Text = "$0"
        Me.txtExpFeeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chbAppFee
        '
        Me.chbAppFee.AutoSize = True
        Me.chbAppFee.Enabled = False
        Me.chbAppFee.Location = New System.Drawing.Point(11, 17)
        Me.chbAppFee.Name = "chbAppFee"
        Me.chbAppFee.Size = New System.Drawing.Size(131, 17)
        Me.chbAppFee.TabIndex = 0
        Me.chbAppFee.Text = "Permit Application Fee"
        Me.chbAppFee.UseVisualStyleBackColor = True
        '
        'chbExpFee
        '
        Me.chbExpFee.AutoSize = True
        Me.chbExpFee.Enabled = False
        Me.chbExpFee.Location = New System.Drawing.Point(11, 108)
        Me.chbExpFee.Name = "chbExpFee"
        Me.chbExpFee.Size = New System.Drawing.Size(112, 17)
        Me.chbExpFee.TabIndex = 5
        Me.chbExpFee.Text = "Expedited Review"
        Me.chbExpFee.UseVisualStyleBackColor = True
        '
        'txtExpFeeOverrideReason
        '
        Me.txtExpFeeOverrideReason.Location = New System.Drawing.Point(337, 131)
        Me.txtExpFeeOverrideReason.Multiline = True
        Me.txtExpFeeOverrideReason.Name = "txtExpFeeOverrideReason"
        Me.txtExpFeeOverrideReason.Size = New System.Drawing.Size(280, 51)
        Me.txtExpFeeOverrideReason.TabIndex = 8
        '
        'txtAppFeeOverrideReason
        '
        Me.txtAppFeeOverrideReason.Location = New System.Drawing.Point(337, 40)
        Me.txtAppFeeOverrideReason.Multiline = True
        Me.txtAppFeeOverrideReason.Name = "txtAppFeeOverrideReason"
        Me.txtAppFeeOverrideReason.Size = New System.Drawing.Size(280, 51)
        Me.txtAppFeeOverrideReason.TabIndex = 3
        '
        'chbExpFeeOverride
        '
        Me.chbExpFeeOverride.AutoSize = True
        Me.chbExpFeeOverride.Location = New System.Drawing.Point(337, 108)
        Me.chbExpFeeOverride.Name = "chbExpFeeOverride"
        Me.chbExpFeeOverride.Size = New System.Drawing.Size(122, 17)
        Me.chbExpFeeOverride.TabIndex = 7
        Me.chbExpFeeOverride.Text = "Override fee amount"
        Me.chbExpFeeOverride.UseVisualStyleBackColor = True
        '
        'chbFeeDataFinalized
        '
        Me.chbFeeDataFinalized.AutoSize = True
        Me.chbFeeDataFinalized.Location = New System.Drawing.Point(11, 231)
        Me.chbFeeDataFinalized.Name = "chbFeeDataFinalized"
        Me.chbFeeDataFinalized.Size = New System.Drawing.Size(127, 17)
        Me.chbFeeDataFinalized.TabIndex = 11
        Me.chbFeeDataFinalized.Text = "Ready to be invoiced"
        Me.chbFeeDataFinalized.UseVisualStyleBackColor = True
        '
        'chbAppFeeOverride
        '
        Me.chbAppFeeOverride.AutoSize = True
        Me.chbAppFeeOverride.Location = New System.Drawing.Point(337, 17)
        Me.chbAppFeeOverride.Name = "chbAppFeeOverride"
        Me.chbAppFeeOverride.Size = New System.Drawing.Size(122, 17)
        Me.chbAppFeeOverride.TabIndex = 2
        Me.chbAppFeeOverride.Text = "Override fee amount"
        Me.chbAppFeeOverride.UseVisualStyleBackColor = True
        '
        'txtFeeTotal
        '
        Me.txtFeeTotal.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtFeeTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFeeTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFeeTotal.Location = New System.Drawing.Point(211, 199)
        Me.txtFeeTotal.Name = "txtFeeTotal"
        Me.txtFeeTotal.ReadOnly = True
        Me.txtFeeTotal.Size = New System.Drawing.Size(77, 13)
        Me.txtFeeTotal.TabIndex = 10
        Me.txtFeeTotal.Text = "$0"
        Me.txtFeeTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblExpFee
        '
        Me.lblExpFee.AutoSize = True
        Me.lblExpFee.Location = New System.Drawing.Point(8, 165)
        Me.lblExpFee.Name = "lblExpFee"
        Me.lblExpFee.Size = New System.Drawing.Size(25, 13)
        Me.lblExpFee.TabIndex = 2
        Me.lblExpFee.Text = "Fee"
        '
        'lblAppFee
        '
        Me.lblAppFee.AutoSize = True
        Me.lblAppFee.Location = New System.Drawing.Point(8, 74)
        Me.lblAppFee.Name = "lblAppFee"
        Me.lblAppFee.Size = New System.Drawing.Size(25, 13)
        Me.lblAppFee.TabIndex = 2
        Me.lblAppFee.Text = "Fee"
        '
        'lblTotalFee
        '
        Me.lblTotalFee.AutoSize = True
        Me.lblTotalFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalFee.Location = New System.Drawing.Point(8, 199)
        Me.lblTotalFee.Name = "lblTotalFee"
        Me.lblTotalFee.Size = New System.Drawing.Size(61, 13)
        Me.lblTotalFee.TabIndex = 2
        Me.lblTotalFee.Text = "Total Fee"
        '
        'cmbExpFeeType
        '
        Me.cmbExpFeeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExpFeeType.FormattingEnabled = True
        Me.cmbExpFeeType.Location = New System.Drawing.Point(11, 131)
        Me.cmbExpFeeType.Name = "cmbExpFeeType"
        Me.cmbExpFeeType.Size = New System.Drawing.Size(280, 21)
        Me.cmbExpFeeType.TabIndex = 6
        '
        'cmbAppFeeType
        '
        Me.cmbAppFeeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAppFeeType.FormattingEnabled = True
        Me.cmbAppFeeType.Location = New System.Drawing.Point(11, 40)
        Me.cmbAppFeeType.Name = "cmbAppFeeType"
        Me.cmbAppFeeType.Size = New System.Drawing.Size(280, 21)
        Me.cmbAppFeeType.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip1.TabIndex = 0
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
        'HeaderPanel
        '
        Me.HeaderPanel.Controls.Add(Me.pnlAssignments)
        Me.HeaderPanel.Controls.Add(Me.txtNewApplicationNumber)
        Me.HeaderPanel.Controls.Add(Me.btnFetchNewAppNumber)
        Me.HeaderPanel.Controls.Add(Me.btnRefreshAIRSNo)
        Me.HeaderPanel.Controls.Add(Me.lblAppNumber)
        Me.HeaderPanel.Controls.Add(Me.lblLinkWarning)
        Me.HeaderPanel.Controls.Add(Me.txtOutstandingApplication)
        Me.HeaderPanel.Controls.Add(Me.rtbFacilityInformation)
        Me.HeaderPanel.Controls.Add(Me.Label10)
        Me.HeaderPanel.Controls.Add(Me.txtAIRSNumber)
        Me.HeaderPanel.Controls.Add(Me.chbClosedOut)
        Me.HeaderPanel.Controls.Add(Me.Label3)
        Me.HeaderPanel.Controls.Add(Me.Label2)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 25)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(792, 105)
        Me.HeaderPanel.TabIndex = 1
        '
        'pnlAssignments
        '
        Me.pnlAssignments.Controls.Add(Me.cboEngineer)
        Me.pnlAssignments.Controls.Add(Me.lklOpenAppOnline)
        Me.pnlAssignments.Controls.Add(Me.lblEngineer)
        Me.pnlAssignments.Controls.Add(Me.cboApplicationUnit)
        Me.pnlAssignments.Controls.Add(Me.lblApplicationUnit)
        Me.pnlAssignments.Controls.Add(Me.lblApplicationType)
        Me.pnlAssignments.Controls.Add(Me.cboApplicationType)
        Me.pnlAssignments.Location = New System.Drawing.Point(550, 0)
        Me.pnlAssignments.Name = "pnlAssignments"
        Me.pnlAssignments.Size = New System.Drawing.Size(242, 105)
        Me.pnlAssignments.TabIndex = 336
        '
        'lklOpenAppOnline
        '
        Me.lklOpenAppOnline.AutoSize = True
        Me.lklOpenAppOnline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lklOpenAppOnline.Location = New System.Drawing.Point(10, 83)
        Me.lklOpenAppOnline.Name = "lklOpenAppOnline"
        Me.lklOpenAppOnline.Size = New System.Drawing.Size(115, 13)
        Me.lklOpenAppOnline.TabIndex = 335
        Me.lklOpenAppOnline.TabStop = True
        Me.lklOpenAppOnline.Text = "Public application view"
        Me.lklOpenAppOnline.Visible = False
        '
        'txtNewApplicationNumber
        '
        Me.txtNewApplicationNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewApplicationNumber.Location = New System.Drawing.Point(146, 3)
        Me.txtNewApplicationNumber.Name = "txtNewApplicationNumber"
        Me.txtNewApplicationNumber.Size = New System.Drawing.Size(59, 23)
        Me.txtNewApplicationNumber.TabIndex = 0
        Me.txtNewApplicationNumber.Visible = False
        '
        'btnFetchNewAppNumber
        '
        Me.btnFetchNewAppNumber.AutoSize = True
        Me.btnFetchNewAppNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFetchNewAppNumber.Image = Global.Iaip.My.Resources.Resources.RefreshIcon
        Me.btnFetchNewAppNumber.Location = New System.Drawing.Point(211, 3)
        Me.btnFetchNewAppNumber.Name = "btnFetchNewAppNumber"
        Me.btnFetchNewAppNumber.Size = New System.Drawing.Size(22, 22)
        Me.btnFetchNewAppNumber.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnFetchNewAppNumber, "Fetch new application number")
        Me.btnFetchNewAppNumber.UseVisualStyleBackColor = True
        Me.btnFetchNewAppNumber.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(242, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 250
        Me.Label3.Text = "Current Facility Info"
        '
        'SSPPApplicationTrackingLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 633)
        Me.Controls.Add(Me.TCApplicationTrackingLog)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Controls.Add(Me.ToolStrip1)
        Me.MinimumSize = New System.Drawing.Size(808, 672)
        Me.Name = "SSPPApplicationTrackingLog"
        Me.Text = "Application Tracking Log"
        Me.TPSubPartEditor.ResumeLayout(False)
        Me.TCSupParts.ResumeLayout(False)
        Me.TPSIP.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        CType(Me.dgvSIPSubParts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        CType(Me.dgvSIPSubpartAddEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        CType(Me.dgvSIPSubPartDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.TPPart60.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        CType(Me.dgvNSPSSubParts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        CType(Me.dgvNSPSSubpartAddEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel21.ResumeLayout(False)
        Me.Panel21.PerformLayout()
        CType(Me.dgvNSPSSubPartDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel22.ResumeLayout(False)
        Me.Panel22.PerformLayout()
        Me.TPPart61.ResumeLayout(False)
        Me.Panel23.ResumeLayout(False)
        CType(Me.dgvNESHAPSubParts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel24.ResumeLayout(False)
        Me.Panel24.PerformLayout()
        Me.Panel25.ResumeLayout(False)
        Me.Panel25.PerformLayout()
        CType(Me.dgvNESHAPSubpartAddEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel26.ResumeLayout(False)
        Me.Panel26.PerformLayout()
        CType(Me.dgvNESHAPSubPartDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel27.ResumeLayout(False)
        Me.Panel27.PerformLayout()
        Me.TPPart63.ResumeLayout(False)
        Me.Panel28.ResumeLayout(False)
        CType(Me.dgvMACTSubParts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel29.ResumeLayout(False)
        Me.Panel29.PerformLayout()
        Me.Panel30.ResumeLayout(False)
        Me.Panel30.PerformLayout()
        CType(Me.dgvMACTSubpartAddEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel31.ResumeLayout(False)
        Me.Panel31.PerformLayout()
        CType(Me.dgvMACTSubPartDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel32.ResumeLayout(False)
        Me.Panel32.PerformLayout()
        Me.TPDocuments.ResumeLayout(False)
        Me.TPDocuments.PerformLayout()
        Me.PanelTitleV.ResumeLayout(False)
        Me.PanelTitleV.PerformLayout()
        Me.PanelPSD.ResumeLayout(False)
        Me.PanelPSD.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.PanelOther.ResumeLayout(False)
        Me.PanelOther.PerformLayout()
        Me.TPContactInformation.ResumeLayout(False)
        Me.TPContactInformation.PerformLayout()
        Me.TPWebPublisher.ResumeLayout(False)
        Me.TPWebPublisher.PerformLayout()
        Me.TPInformationRequests.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgvInformationRequested, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPApplicationHistory.ResumeLayout(False)
        CType(Me.dgvFacilityAppHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.TPReviews.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.TPOtherInfo.ResumeLayout(False)
        Me.OtherInfoGroup.ResumeLayout(False)
        Me.OtherInfoGroup.PerformLayout()
        Me.GBSignificationComments.ResumeLayout(False)
        Me.GBSignificationComments.PerformLayout()
        Me.ApplicableRulesGroup.ResumeLayout(False)
        Me.ApplicableRulesGroup.PerformLayout()
        Me.TPTrackingLog.ResumeLayout(False)
        Me.TPTrackingLog.PerformLayout()
        Me.GBOther.ResumeLayout(False)
        Me.GBOther.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GBAirProgramCodes.ResumeLayout(False)
        Me.GBAirProgramCodes.PerformLayout()
        Me.TCApplicationTrackingLog.ResumeLayout(False)
        Me.TPFees.ResumeLayout(False)
        Me.TPFees.PerformLayout()
        Me.pnlFeeDataFinalized.ResumeLayout(False)
        Me.pnlFeeDataFinalized.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvApplicationInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvApplicationPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        Me.pnlAssignments.ResumeLayout(False)
        Me.pnlAssignments.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLinkWarning As System.Windows.Forms.Label
    Friend WithEvents rtbFacilityInformation As System.Windows.Forms.RichTextBox
    Friend WithEvents txtAIRSNumber As AirNumberEntryForm
    Friend WithEvents lblAppNumber As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblApplicationUnit As System.Windows.Forms.Label
    Friend WithEvents lblEngineer As System.Windows.Forms.Label
    Friend WithEvents cboApplicationUnit As System.Windows.Forms.ComboBox
    Friend WithEvents cboEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents lblApplicationType As System.Windows.Forms.Label
    Friend WithEvents cboApplicationType As System.Windows.Forms.ComboBox
    Friend WithEvents chbClosedOut As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtOutstandingApplication As System.Windows.Forms.TextBox
    Friend WithEvents btnRefreshAIRSNo As System.Windows.Forms.Button
    Friend WithEvents TPSubPartEditor As System.Windows.Forms.TabPage
    Friend WithEvents TCSupParts As System.Windows.Forms.TabControl
    Friend WithEvents TPSIP As System.Windows.Forms.TabPage
    Friend WithEvents cboSIPSubpart As System.Windows.Forms.ComboBox
    Friend WithEvents TPPart60 As System.Windows.Forms.TabPage
    Friend WithEvents TPPart61 As System.Windows.Forms.TabPage
    Friend WithEvents TPPart63 As System.Windows.Forms.TabPage
    Friend WithEvents TPDocuments As System.Windows.Forms.TabPage
    Friend WithEvents PanelPSD As System.Windows.Forms.Panel
    Friend WithEvents btnPSDNarrativeDownload As System.Windows.Forms.Button
    Friend WithEvents lblPSDNarrativeDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDNarrativeDUDoc As System.Windows.Forms.Label
    Friend WithEvents txtPSDNarrativePDF As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDNarrativeDoc As System.Windows.Forms.TextBox
    Friend WithEvents chbPSDNarrative As System.Windows.Forms.CheckBox
    Friend WithEvents lblPSDNarrativeSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDNarrativeSRDoc As System.Windows.Forms.Label
    Friend WithEvents btnPSDPublicNoticeDownload As System.Windows.Forms.Button
    Friend WithEvents btnPSDDraftPermitDownload As System.Windows.Forms.Button
    Friend WithEvents btnPSDPrelimDetDownload As System.Windows.Forms.Button
    Friend WithEvents btnPSDHearingNoticeDownload As System.Windows.Forms.Button
    Friend WithEvents btnPSDFinalDetDownload As System.Windows.Forms.Button
    Friend WithEvents btnPSDFinalPermitDownload As System.Windows.Forms.Button
    Friend WithEvents btnPSDAppSummaryDownload As System.Windows.Forms.Button
    Friend WithEvents lblPSDFinalPermitDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDFinalPermitSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDFinalPermitDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDFinalPermitSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDFinalDetDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDFinalDetSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDFinalDetDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDFinalDetSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDHearingNoticeDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDHearingNoticeSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDHearingNoticeDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDHearingNoticeSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDPublicNoticeDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDPublicNoticeSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDPublicNoticeDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDPublicNoticeSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDDraftPermitDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDDraftPermitSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDDraftPermitDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDDraftPermitSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDPrelimDetDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDPrelimDetSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDPrelimDetDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDPrelimDetSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDAppSummaryDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDAppSummarySRPDF As System.Windows.Forms.Label
    Friend WithEvents lblPSDAppSummaryDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblPSDAppSummarySRDoc As System.Windows.Forms.Label
    Friend WithEvents txtPSDPrelimDetPDF As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDDraftPermitPDF As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDPublicNoticePDF As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDHearingNoticePDF As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDFinalDetPDF As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDAppSummaryPDF As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDFinalPermitPDF As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDFinalPermitDoc As System.Windows.Forms.TextBox
    Friend WithEvents chbPSDFinalPermit As System.Windows.Forms.CheckBox
    Friend WithEvents txtPSDFinalDetDoc As System.Windows.Forms.TextBox
    Friend WithEvents chbPSDFinalDet As System.Windows.Forms.CheckBox
    Friend WithEvents txtPSDHearingNoticeDoc As System.Windows.Forms.TextBox
    Friend WithEvents chbPSDHearingNotice As System.Windows.Forms.CheckBox
    Friend WithEvents txtPSDPublicNoticeDoc As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDDraftPermitDoc As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDPrelimDetDoc As System.Windows.Forms.TextBox
    Friend WithEvents txtPSDAppSummaryDoc As System.Windows.Forms.TextBox
    Friend WithEvents chbPSDPublicNotice As System.Windows.Forms.CheckBox
    Friend WithEvents chbPSDDraftPermit As System.Windows.Forms.CheckBox
    Friend WithEvents chbPSDPrelimDet As System.Windows.Forms.CheckBox
    Friend WithEvents chbPSDApplicationSummary As System.Windows.Forms.CheckBox
    Friend WithEvents PanelTitleV As System.Windows.Forms.Panel
    Friend WithEvents btnTVFinalDownload As System.Windows.Forms.Button
    Friend WithEvents btnTVPublicNoticeDownload As System.Windows.Forms.Button
    Friend WithEvents btnTVDraftDownload As System.Windows.Forms.Button
    Friend WithEvents btnTVNarrativeDownload As System.Windows.Forms.Button
    Friend WithEvents lblTVFinalDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblTVFinalSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblTVFinalDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblTVFinalSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblTVPublicNoticeDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblTVPublicNoticeSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblTVPublicNoticeDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblTVPublicNoticeSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblTVDraftDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblTVDraftSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblTVDraftDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblTVDraftSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblTVNarrativeDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblTVNarrativeSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblTVNarrativeDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblTVNarrativeSRDoc As System.Windows.Forms.Label
    Friend WithEvents txtTVFinalPDF As System.Windows.Forms.TextBox
    Friend WithEvents txtTVPublicNoticePDF As System.Windows.Forms.TextBox
    Friend WithEvents txtTVDraftPDF As System.Windows.Forms.TextBox
    Friend WithEvents txtTVNarrativePDF As System.Windows.Forms.TextBox
    Friend WithEvents txtTVFinalDoc As System.Windows.Forms.TextBox
    Friend WithEvents txtTVPublicNoticeDoc As System.Windows.Forms.TextBox
    Friend WithEvents txtTVDraftDoc As System.Windows.Forms.TextBox
    Friend WithEvents txtTVNarrativeDoc As System.Windows.Forms.TextBox
    Friend WithEvents chbTVFinal As System.Windows.Forms.CheckBox
    Friend WithEvents chbTVPublicNotice As System.Windows.Forms.CheckBox
    Friend WithEvents chbTVDraft As System.Windows.Forms.CheckBox
    Friend WithEvents chbTVNarrative As System.Windows.Forms.CheckBox
    Friend WithEvents PanelOther As System.Windows.Forms.Panel
    Friend WithEvents btnOtherPermitDownload As System.Windows.Forms.Button
    Friend WithEvents btnOtherNarrativeDownload As System.Windows.Forms.Button
    Friend WithEvents lblOtherPermitDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblOtherPermitSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblOtherPermitDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblOtherPermitSRDoc As System.Windows.Forms.Label
    Friend WithEvents lblOtherNarrativeDUPDF As System.Windows.Forms.Label
    Friend WithEvents lblOtherNarrativeSRPDF As System.Windows.Forms.Label
    Friend WithEvents lblOtherNarrativeDUDoc As System.Windows.Forms.Label
    Friend WithEvents lblOtherNarrativeSRDoc As System.Windows.Forms.Label
    Friend WithEvents txtOtherNarrativePDF As System.Windows.Forms.TextBox
    Friend WithEvents txtOtherPermitPDF As System.Windows.Forms.TextBox
    Friend WithEvents txtOtherNarrativeDoc As System.Windows.Forms.TextBox
    Friend WithEvents chbOtherNarrative As System.Windows.Forms.CheckBox
    Friend WithEvents txtOtherPermitDoc As System.Windows.Forms.TextBox
    Friend WithEvents chbOtherPermit As System.Windows.Forms.CheckBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rdbOtherPermit As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPSDPermit As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTitleVPermit As System.Windows.Forms.RadioButton
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents lblPDF As System.Windows.Forms.Label
    Friend WithEvents lblWord As System.Windows.Forms.Label
    Friend WithEvents TPContactInformation As System.Windows.Forms.TabPage
    Friend WithEvents btnGetCurrentPermittingContact As System.Windows.Forms.Button
    Friend WithEvents mtbContactZipCode As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mtbContactFaxNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtContactDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtContactEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtContactState As System.Windows.Forms.TextBox
    Friend WithEvents txtContactCity As System.Windows.Forms.TextBox
    Friend WithEvents txtContactStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtContactTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtContactCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents txtContactPedigree As System.Windows.Forms.TextBox
    Friend WithEvents txtContactSocialTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtContactLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtContactFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TPWebPublisher As System.Windows.Forms.TabPage
    Friend WithEvents lblPNExpires As System.Windows.Forms.Label
    Friend WithEvents DTPPNExpires As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEffectiveDateofPermit As System.Windows.Forms.Label
    Friend WithEvents lblExperationDate As System.Windows.Forms.Label
    Friend WithEvents lblEPANotifiedFinalOnWeb As System.Windows.Forms.Label
    Friend WithEvents lblFinalOnWeb As System.Windows.Forms.Label
    Friend WithEvents lbEPAStatesNotified As System.Windows.Forms.Label
    Friend WithEvents lblDraftOnWeb As System.Windows.Forms.Label
    Friend WithEvents lblNotifiedAppReceived As System.Windows.Forms.Label
    Friend WithEvents DTPExperationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents DTPNotifiedAppReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSaveWebPublisher As System.Windows.Forms.Button
    Friend WithEvents txtEPATargetedComments As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents DTPEffectiveDateofPermit As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPEPANotifiedPermitOnWeb As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPFinalOnWeb As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPEPAStatesNotified As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDraftOnWeb As System.Windows.Forms.DateTimePicker
    Friend WithEvents TPInformationRequests As System.Windows.Forms.TabPage
    Friend WithEvents dgvInformationRequested As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblInformationReceived As System.Windows.Forms.Label
    Friend WithEvents lblInformationRequested As System.Windows.Forms.Label
    Friend WithEvents btnDeleteInformationRequest As System.Windows.Forms.Button
    Friend WithEvents btnClearInformationRequest As System.Windows.Forms.Button
    Friend WithEvents txtInformationRequestedKey As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveInformationRequest As System.Windows.Forms.Button
    Friend WithEvents txtInformationReceived As System.Windows.Forms.TextBox
    Friend WithEvents txtInformationRequested As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents DTPInformationReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPInformationRequested As System.Windows.Forms.DateTimePicker
    Friend WithEvents TPApplicationHistory As System.Windows.Forms.TabPage
    Friend WithEvents dgvFacilityAppHistory As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtApplicationCount As System.Windows.Forms.TextBox
    Friend WithEvents txtMasterAppLock As System.Windows.Forms.TextBox
    Friend WithEvents btnClearLinks As System.Windows.Forms.Button
    Friend WithEvents btnLinkApplications As System.Windows.Forms.Button
    Friend WithEvents btnAddApplicationToList As System.Windows.Forms.Button
    Friend WithEvents lbLinkApplications As System.Windows.Forms.ListBox
    Friend WithEvents txtMasterApp As System.Windows.Forms.TextBox
    Friend WithEvents txtEngineerHistory As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicationDatedHistory As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicationTypeHistory As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicationUnitHistory As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicationNumberHistory As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents chbClosedOutHistory As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtHistoryComments As System.Windows.Forms.TextBox
    Friend WithEvents txtHistoryAppComments As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents TPReviews As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblISMPReview As System.Windows.Forms.Label
    Friend WithEvents txtISMPComments As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rdbISMPNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbISMPYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents DTPISMPReview As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboISMPStaff As System.Windows.Forms.ComboBox
    Friend WithEvents lblISMPStaff As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSSCPReview As System.Windows.Forms.Label
    Friend WithEvents txtSSCPComments As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdbSSCPNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSSCPYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents DTPSSCPReview As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboSSCPStaff As System.Windows.Forms.ComboBox
    Friend WithEvents lblSSCPStaff As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblReviewSubmitted As System.Windows.Forms.Label
    Friend WithEvents cboISMPUnits As System.Windows.Forms.ComboBox
    Friend WithEvents lblISMPUnits As System.Windows.Forms.Label
    Friend WithEvents DTPReviewSubmitted As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSSCPUnit As System.Windows.Forms.Label
    Friend WithEvents cboSSCPUnits As System.Windows.Forms.ComboBox
    Friend WithEvents TPOtherInfo As System.Windows.Forms.TabPage
    Friend WithEvents GBSignificationComments As System.Windows.Forms.GroupBox
    Friend WithEvents txtSignificantComments As System.Windows.Forms.TextBox
    Friend WithEvents ApplicableRulesGroup As System.Windows.Forms.GroupBox
    Friend WithEvents chbPal As System.Windows.Forms.CheckBox
    Friend WithEvents chbRuleyy As System.Windows.Forms.CheckBox
    Friend WithEvents chbRulett As System.Windows.Forms.CheckBox
    Friend WithEvents chb112g As System.Windows.Forms.CheckBox
    Friend WithEvents chbNAANSR As System.Windows.Forms.CheckBox
    Friend WithEvents chbPSD As System.Windows.Forms.CheckBox
    Friend WithEvents TPTrackingLog As System.Windows.Forms.TabPage
    Friend WithEvents llbPermitNumber As System.Windows.Forms.LinkLabel
    Friend WithEvents lblPAReady As System.Windows.Forms.Label
    Friend WithEvents lblPNReady As System.Windows.Forms.Label
    Friend WithEvents GBOther As System.Windows.Forms.GroupBox
    Friend WithEvents chbHAPsMajor As System.Windows.Forms.CheckBox
    Friend WithEvents chbNSRMajor As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txt1HourOzone As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtPM As System.Windows.Forms.TextBox
    Friend WithEvents txt8HROzone As System.Windows.Forms.TextBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents chbPNReady As System.Windows.Forms.CheckBox
    Friend WithEvents GBAirProgramCodes As System.Windows.Forms.GroupBox
    Friend WithEvents chbCDS_0 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_8 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_M As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_V As System.Windows.Forms.CheckBox
    Friend WithEvents chbCDS_A As System.Windows.Forms.CheckBox
    Friend WithEvents lblDateToDO As System.Windows.Forms.Label
    Friend WithEvents DTPDateToDO As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDatetoBC As System.Windows.Forms.Label
    Friend WithEvents DTPDateToBC As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEPAEnds As System.Windows.Forms.Label
    Friend WithEvents DTPEPAEnds As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEPAWaived As System.Windows.Forms.Label
    Friend WithEvents DTPEPAWaived As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbPAReady As System.Windows.Forms.CheckBox
    Friend WithEvents lblDraftIssued As System.Windows.Forms.Label
    Friend WithEvents lblFinalAction As System.Windows.Forms.Label
    Friend WithEvents lblDateToPM As System.Windows.Forms.Label
    Friend WithEvents lblDateToUC As System.Windows.Forms.Label
    Friend WithEvents lblDeadline As System.Windows.Forms.Label
    Friend WithEvents lblDatePNExpires As System.Windows.Forms.Label
    Friend WithEvents lblDatePAExpires As System.Windows.Forms.Label
    Friend WithEvents lblDateAcknowledge As System.Windows.Forms.Label
    Friend WithEvents lblDateReassigned As System.Windows.Forms.Label
    Friend WithEvents lblDateAssigned As System.Windows.Forms.Label
    Friend WithEvents txtFacilityZipCode As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents txtPlantDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents cboOperationalStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblOperationalStatus As System.Windows.Forms.Label
    Friend WithEvents cboClassification As System.Windows.Forms.ComboBox
    Friend WithEvents lblClassification As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtSICCode As System.Windows.Forms.TextBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents txtReasonAppSubmitted As System.Windows.Forms.TextBox
    Friend WithEvents cboPublicAdvisory As System.Windows.Forms.ComboBox
    Friend WithEvents cboPermitAction As System.Windows.Forms.ComboBox
    Friend WithEvents txtPermitNumber As System.Windows.Forms.TextBox
    Friend WithEvents DTPDraftIssued As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPFinalAction As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDateToPM As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDateToUC As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDeadline As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDatePNExpires As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDatePAExpires As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDateAssigned As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDateReassigned As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDateAcknowledge As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPDateSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCounty As System.Windows.Forms.Label
    Friend WithEvents cboFacilityCity As System.Windows.Forms.ComboBox
    Friend WithEvents cboCounty As System.Windows.Forms.ComboBox
    Friend WithEvents lblPublicAdvisory As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblPermitAction As System.Windows.Forms.Label
    Friend WithEvents lblPermitNumber As System.Windows.Forms.Label
    Friend WithEvents lblReceived As System.Windows.Forms.Label
    Friend WithEvents lblDated As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDistrict As System.Windows.Forms.TextBox
    Friend WithEvents TCApplicationTrackingLog As System.Windows.Forms.TabControl
    Friend WithEvents btnEmailAcknowledgmentLetter As System.Windows.Forms.Button
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents dgvSIPSubParts As System.Windows.Forms.DataGridView
    Friend WithEvents btnAddNewSIPSubpart As System.Windows.Forms.Button
    Friend WithEvents dgvSIPSubpartAddEdit As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSIPSubPartDelete As System.Windows.Forms.DataGridView
    Friend WithEvents btnSIPEdit As System.Windows.Forms.Button
    Friend WithEvents btnSIPUnedit As System.Windows.Forms.Button
    Friend WithEvents btnSIPUneditAll As System.Windows.Forms.Button
    Friend WithEvents btnSIPEditAll As System.Windows.Forms.Button
    Friend WithEvents btnSIPUndelete As System.Windows.Forms.Button
    Friend WithEvents btnSIPDelete As System.Windows.Forms.Button
    Friend WithEvents btnSIPDeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnSIPUndeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnClearAddModifiedSIPs As System.Windows.Forms.Button
    Friend WithEvents btnClearSIPDeletes As System.Windows.Forms.Button
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents btnSaveSIPSubpart As System.Windows.Forms.Button
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents dgvNSPSSubParts As System.Windows.Forms.DataGridView
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveNSPSSubpart As System.Windows.Forms.Button
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents dgvNSPSSubpartAddEdit As System.Windows.Forms.DataGridView
    Friend WithEvents btnNSPSEditAll As System.Windows.Forms.Button
    Friend WithEvents btnNSPSUneditAll As System.Windows.Forms.Button
    Friend WithEvents btnClearAddModifiedNSPSs As System.Windows.Forms.Button
    Friend WithEvents btnNSPSUnedit As System.Windows.Forms.Button
    Friend WithEvents btnNSPSEdit As System.Windows.Forms.Button
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents dgvNSPSSubPartDelete As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearNSPSDeletes As System.Windows.Forms.Button
    Friend WithEvents btnNSPSDelete As System.Windows.Forms.Button
    Friend WithEvents btnNSPSUndeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnNSPSDeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnNSPSUndelete As System.Windows.Forms.Button
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents btnAddNewNSPSSubpart As System.Windows.Forms.Button
    Friend WithEvents cboNSPSSubpart As System.Windows.Forms.ComboBox
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents dgvNESHAPSubParts As System.Windows.Forms.DataGridView
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveNESHAPSubpart As System.Windows.Forms.Button
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents dgvNESHAPSubpartAddEdit As System.Windows.Forms.DataGridView
    Friend WithEvents btnNESHAPEditAll As System.Windows.Forms.Button
    Friend WithEvents btnNESHAPUneditAll As System.Windows.Forms.Button
    Friend WithEvents btnClearAddModifiedNESHAPs As System.Windows.Forms.Button
    Friend WithEvents btnNESHAPUnedit As System.Windows.Forms.Button
    Friend WithEvents btnNESHAPEdit As System.Windows.Forms.Button
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents dgvNESHAPSubPartDelete As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearNESHAPDeletes As System.Windows.Forms.Button
    Friend WithEvents btnNESHAPDelete As System.Windows.Forms.Button
    Friend WithEvents btnNESHAPUndeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnNESHAPDeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnNESHAPUndelete As System.Windows.Forms.Button
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Friend WithEvents btnAddNewNESHAPSubpart As System.Windows.Forms.Button
    Friend WithEvents cboNESHAPSubpart As System.Windows.Forms.ComboBox
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Friend WithEvents dgvMACTSubParts As System.Windows.Forms.DataGridView
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveMACTSubpart As System.Windows.Forms.Button
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents dgvMACTSubpartAddEdit As System.Windows.Forms.DataGridView
    Friend WithEvents btnMACTEditAll As System.Windows.Forms.Button
    Friend WithEvents btnMACTUneditAll As System.Windows.Forms.Button
    Friend WithEvents btnClearAddModifiedMACTs As System.Windows.Forms.Button
    Friend WithEvents btnMACTUnedit As System.Windows.Forms.Button
    Friend WithEvents btnMACTEdit As System.Windows.Forms.Button
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents dgvMACTSubPartDelete As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearMACTDeletes As System.Windows.Forms.Button
    Friend WithEvents btnMACTDelete As System.Windows.Forms.Button
    Friend WithEvents btnMACTUndeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnMACTDeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnMACTUndelete As System.Windows.Forms.Button
    Friend WithEvents Panel32 As System.Windows.Forms.Panel
    Friend WithEvents btnAddNewMACTSubpart As System.Windows.Forms.Button
    Friend WithEvents cboMACTSubpart As System.Windows.Forms.ComboBox
    Friend WithEvents txtNAICSCode As System.Windows.Forms.TextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents chbCDS_RMP As System.Windows.Forms.CheckBox
    Friend WithEvents chbConfidential As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents HeaderPanel As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents txtNewApplicationNumber As TextBox
    Friend WithEvents btnFetchNewAppNumber As Button
    Friend WithEvents TPFees As TabPage
    Friend WithEvents dtpFacilityFeeNotified As DateTimePicker
    Friend WithEvents lblFeeDataFinalized As Label
    Friend WithEvents lklGenerateEmail As LinkLabel
    Friend WithEvents chbAppFee As CheckBox
    Friend WithEvents chbExpFee As CheckBox
    Friend WithEvents txtExpFeeOverrideReason As TextBox
    Friend WithEvents txtAppFeeOverrideReason As TextBox
    Friend WithEvents chbExpFeeOverride As CheckBox
    Friend WithEvents chbFeeDataFinalized As CheckBox
    Friend WithEvents chbAppFeeOverride As CheckBox
    Friend WithEvents txtFeeTotal As TextBox
    Friend WithEvents lblTotalFee As Label
    Friend WithEvents cmbExpFeeType As ComboBox
    Friend WithEvents cmbAppFeeType As ComboBox
    Friend WithEvents dgvApplicationPayments As IaipDataGridView
    Friend WithEvents lblInvoices As Label
    Friend WithEvents lblExpFee As Label
    Friend WithEvents lblAppFee As Label
    Friend WithEvents dtpFeeDataFinalized As DateTimePicker
    Friend WithEvents lblFacilityFeeNotified As Label
    Friend WithEvents lblFeeChangesNotAllowedWarning As Label
    Friend WithEvents txtExpFeeAmount As CurrencyTextBox
    Friend WithEvents txtAppFeeAmount As CurrencyTextBox
    Friend WithEvents dgvApplicationInvoices As IaipDataGridView
    Friend WithEvents lklOpenAppOnline As LinkLabel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lblFeeTotalInvoiced As Label
    Friend WithEvents lblFeeTotalPaid As Label
    Friend WithEvents txtFeeTotalInvoiced As CurrencyTextBox
    Friend WithEvents txtFeeTotalPaid As CurrencyTextBox
    Friend WithEvents lblPayments As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtContactPhoneNumber As TextBox
    Friend WithEvents OtherInfoGroup As GroupBox
    Friend WithEvents chbNspsFeeExempt As CheckBox
    Friend WithEvents chbFederallyOwned As CheckBox
    Friend WithEvents pnlFeeDataFinalized As Panel
    Friend WithEvents pnlAssignments As Panel
End Class
