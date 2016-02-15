<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSCPEvents
    Inherits BaseForm


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

    Friend WithEvents chbNotificationReceivedByAPB As System.Windows.Forms.CheckBox
    Friend WithEvents DTPNotificationReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPACCReceivedDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbACCReceivedByAPB As System.Windows.Forms.CheckBox
    Friend WithEvents DTPTestReportReceivedDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbISMPTestReportReceivedByAPB As System.Windows.Forms.CheckBox
    Friend WithEvents DTPReportReceivedDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbReportReceivedByAPB As System.Windows.Forms.CheckBox
    Friend WithEvents lblNotificationOther As System.Windows.Forms.Label
    Friend WithEvents lblNotificationDue As System.Windows.Forms.Label
    Friend WithEvents lblDateSent As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRMPID As System.Windows.Forms.TextBox
    Friend WithEvents lblRMPID As System.Windows.Forms.Label
    Friend WithEvents mmiDelete As System.Windows.Forms.MenuItem
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents mmiPrint As System.Windows.Forms.MenuItem
    Friend WithEvents dtpAccReportingYear As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAccReportingYear As System.Windows.Forms.Label
    Friend WithEvents wrnACCResubmittalRequested As System.Windows.Forms.Label
    Friend WithEvents pnlACCResubmittalRequested As System.Windows.Forms.Panel
    Friend WithEvents rdbACCResubmittalRequestedYes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCResubmittalRequestedNo As System.Windows.Forms.RadioButton
    Friend WithEvents lblACCResubmittalRequested As System.Windows.Forms.Label
    Friend WithEvents wrnACCAllDeviationsReported As System.Windows.Forms.Label
    Friend WithEvents pnlACCAllDeviationsReported As System.Windows.Forms.Panel
    Friend WithEvents rdbACCAllDeviationsReportedYes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCAllDeviationsReportedNo As System.Windows.Forms.RadioButton
    Friend WithEvents lblACCAllDeviationsReported As System.Windows.Forms.Label
    Friend WithEvents rdbACCResubmittalRequestedUnknown As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCAllDeviationsReportedUnknown As System.Windows.Forms.RadioButton
    Friend WithEvents txtPollutantTested As System.Windows.Forms.TextBox
    Friend WithEvents txtUnitTested As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnACCSubmittals As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnViewTestReport As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTestReportReceivedbySSCPDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTestReportISMPCompleteDate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DTPAcknowledgmentLetterSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnReportMoreOptions As System.Windows.Forms.Button
    Friend WithEvents btnEnforcementProcess As System.Windows.Forms.Button
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtEventInformation As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityInformation As System.Windows.Forms.TextBox
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents DTPEventCompleteDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbEventComplete As System.Windows.Forms.CheckBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents rdbNotificationFollowUpNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNotificationFollowUpYes As System.Windows.Forms.RadioButton
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents rdbTestReportFollowUpNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTestReportFollowUpYes As System.Windows.Forms.RadioButton
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents rdbInspectionFollowUpNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInspectionFollowUpYes As System.Windows.Forms.RadioButton
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents PanelSSCPCompliance2 As System.Windows.Forms.Panel
    Friend WithEvents DTPReportPeriodStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPReportPeriodEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtReportsGeneralComments As System.Windows.Forms.TextBox
    Friend WithEvents rdbReportEnforcementNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbReportEnforcementYes As System.Windows.Forms.RadioButton
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents rdbReportDeviationNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbReportDeviationYes As System.Windows.Forms.RadioButton
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents rdbReportCompleteNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbReportCompleteYes As System.Windows.Forms.RadioButton
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPSentDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtReportPeriodComments As System.Windows.Forms.TextBox
    Friend WithEvents cboReportSchedule As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents wrnCompleteReport As System.Windows.Forms.Label
    Friend WithEvents wrnEnforcementNeeded As System.Windows.Forms.Label
    Friend WithEvents wrnShowDeviation As System.Windows.Forms.Label
    Friend WithEvents wrnReportPeriod As System.Windows.Forms.Label
    Friend WithEvents lblResubmittal As System.Windows.Forms.Label
    Friend WithEvents NUPReportSubmittal As System.Windows.Forms.NumericUpDown
    Friend WithEvents wrnReportSubmittal As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PanelSSCPCompliance As System.Windows.Forms.Panel
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents txtWeatherConditions As System.Windows.Forms.TextBox
    Friend WithEvents dtpNotificationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNotificationComments As System.Windows.Forms.TextBox
    Friend WithEvents dtpNotificationDate2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNotificationTypeOther As System.Windows.Forms.TextBox
    Friend WithEvents DTPTestReportDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbTestReportChangeDueDate As System.Windows.Forms.CheckBox
    Friend WithEvents DTPTestReportNewDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtTestReportDueDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTestReportComments As System.Windows.Forms.TextBox
    Friend WithEvents txtISMPReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents cboNotificationType As System.Windows.Forms.ComboBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents lblNotificationDate2 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents DGRACCResubmittal As System.Windows.Forms.DataGrid
    Friend WithEvents wrnACCDatePostmarked As System.Windows.Forms.Label
    Friend WithEvents wrnACCConditions As System.Windows.Forms.Label
    Friend WithEvents PanelACC As System.Windows.Forms.Panel
    Friend WithEvents NUPACCSubmittal As System.Windows.Forms.NumericUpDown
    Friend WithEvents wrnACCSubmittal As System.Windows.Forms.Label
    Friend WithEvents rdbACCPostmarkNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCPostmarkYes As System.Windows.Forms.RadioButton
    Friend WithEvents wrnACCPostmark As System.Windows.Forms.Label
    Friend WithEvents DTPACCPostmarked As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdbACCRONo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCROYes As System.Windows.Forms.RadioButton
    Friend WithEvents wrnACCRO As System.Windows.Forms.Label
    Friend WithEvents rdbACCCorrectACCNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCCorrectACCYes As System.Windows.Forms.RadioButton
    Friend WithEvents wrnACCCorrectACC As System.Windows.Forms.Label
    Friend WithEvents rdbACCConditionsNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCConditionsYes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCCorrectNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCCorrectYes As System.Windows.Forms.RadioButton
    Friend WithEvents wrnACCCorrect As System.Windows.Forms.Label
    Friend WithEvents rdbACCEnforcementNeededNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCEnforcementNeededYes As System.Windows.Forms.RadioButton
    Friend WithEvents wrnACCEnforcementNeeded As System.Windows.Forms.Label
    Friend WithEvents rdbACCDeviationsReportedNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCDeviationsReportedYes As System.Windows.Forms.RadioButton
    Friend WithEvents wrnACCDeviationsReported As System.Windows.Forms.Label
    Friend WithEvents rdbACCPreviouslyUnreportedDeviationsNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbACCPreviouslyUnreportedDeviationsYes As System.Windows.Forms.RadioButton
    Friend WithEvents wrnACCPreviousDeviations As System.Windows.Forms.Label
    Friend WithEvents txtACCComments As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblACCPreviouslyUnreportedDeviations As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents SplitterACC As System.Windows.Forms.Splitter
    Friend WithEvents wrnInspectionDates As System.Windows.Forms.Label
    Friend WithEvents wrnInspectionComplianceStatus As System.Windows.Forms.Label
    Friend WithEvents wrnInspectionOperating As System.Windows.Forms.Label
    Friend WithEvents SplitterReport As System.Windows.Forms.Splitter
    Friend WithEvents PanelReports As System.Windows.Forms.Panel
    Friend WithEvents dgrReportResubmittal As System.Windows.Forms.DataGrid
    Friend WithEvents DTPInspectionDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpInspectionTimeStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpInspectionTimeEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboInspectionComplianceStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtInspectionGuide As System.Windows.Forms.TextBox
    Friend WithEvents txtInspectionConclusion As System.Windows.Forms.TextBox
    Friend WithEvents DTPInspectionDateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboInspectionReason As System.Windows.Forms.ComboBox
    Friend WithEvents rdbInspectionFacilityOperatingNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInspectionFacilityOperatingYes As System.Windows.Forms.RadioButton
    Friend WithEvents TCItems As System.Windows.Forms.TabControl
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents lblNotificationDate As System.Windows.Forms.Label
    Friend WithEvents TPACC As System.Windows.Forms.TabPage
    Friend WithEvents TPNotifications As System.Windows.Forms.TabPage
    Friend WithEvents TPTestReports As System.Windows.Forms.TabPage
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TPReport As System.Windows.Forms.TabPage
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TPInspection As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents mmiTools As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents mmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Private components As System.ComponentModel.IContainer


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mmiFile = New System.Windows.Forms.MenuItem()
        Me.mmiSave = New System.Windows.Forms.MenuItem()
        Me.mmiPrint = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.mmiClose = New System.Windows.Forms.MenuItem()
        Me.mmiTools = New System.Windows.Forms.MenuItem()
        Me.mmiDelete = New System.Windows.Forms.MenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblRMPID = New System.Windows.Forms.Label()
        Me.txtRMPID = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.cboStaffResponsible = New System.Windows.Forms.ComboBox()
        Me.DTPAcknowledgmentLetterSent = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox()
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox()
        Me.chbEventComplete = New System.Windows.Forms.CheckBox()
        Me.DTPEventCompleteDate = New System.Windows.Forms.DateTimePicker()
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFacilityInformation = New System.Windows.Forms.TextBox()
        Me.txtEventInformation = New System.Windows.Forms.TextBox()
        Me.btnEnforcementProcess = New System.Windows.Forms.Button()
        Me.TCItems = New System.Windows.Forms.TabControl()
        Me.TPReport = New System.Windows.Forms.TabPage()
        Me.PanelReports = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.DTPReportReceivedDate = New System.Windows.Forms.DateTimePicker()
        Me.chbReportReceivedByAPB = New System.Windows.Forms.CheckBox()
        Me.btnReportMoreOptions = New System.Windows.Forms.Button()
        Me.wrnReportSubmittal = New System.Windows.Forms.Label()
        Me.NUPReportSubmittal = New System.Windows.Forms.NumericUpDown()
        Me.lblResubmittal = New System.Windows.Forms.Label()
        Me.wrnReportPeriod = New System.Windows.Forms.Label()
        Me.wrnShowDeviation = New System.Windows.Forms.Label()
        Me.wrnEnforcementNeeded = New System.Windows.Forms.Label()
        Me.wrnCompleteReport = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboReportSchedule = New System.Windows.Forms.ComboBox()
        Me.txtReportPeriodComments = New System.Windows.Forms.TextBox()
        Me.DTPSentDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdbReportCompleteYes = New System.Windows.Forms.RadioButton()
        Me.rdbReportCompleteNo = New System.Windows.Forms.RadioButton()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.rdbReportDeviationYes = New System.Windows.Forms.RadioButton()
        Me.rdbReportDeviationNo = New System.Windows.Forms.RadioButton()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.rdbReportEnforcementYes = New System.Windows.Forms.RadioButton()
        Me.rdbReportEnforcementNo = New System.Windows.Forms.RadioButton()
        Me.txtReportsGeneralComments = New System.Windows.Forms.TextBox()
        Me.DTPReportPeriodEnd = New System.Windows.Forms.DateTimePicker()
        Me.DTPReportPeriodStart = New System.Windows.Forms.DateTimePicker()
        Me.SplitterReport = New System.Windows.Forms.Splitter()
        Me.dgrReportResubmittal = New System.Windows.Forms.DataGrid()
        Me.TPTestReports = New System.Windows.Forms.TabPage()
        Me.PanelSSCPCompliance2 = New System.Windows.Forms.Panel()
        Me.DTPTestReportReceivedDate = New System.Windows.Forms.DateTimePicker()
        Me.chbISMPTestReportReceivedByAPB = New System.Windows.Forms.CheckBox()
        Me.txtPollutantTested = New System.Windows.Forms.TextBox()
        Me.txtUnitTested = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnViewTestReport = New System.Windows.Forms.Button()
        Me.txtTestReportReceivedbySSCPDate = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTestReportISMPCompleteDate = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.rdbTestReportFollowUpYes = New System.Windows.Forms.RadioButton()
        Me.rdbTestReportFollowUpNo = New System.Windows.Forms.RadioButton()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.DTPTestReportDueDate = New System.Windows.Forms.DateTimePicker()
        Me.chbTestReportChangeDueDate = New System.Windows.Forms.CheckBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtISMPReferenceNumber = New System.Windows.Forms.TextBox()
        Me.DTPTestReportNewDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txtTestReportComments = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtTestReportDueDate = New System.Windows.Forms.TextBox()
        Me.TPInspection = New System.Windows.Forms.TabPage()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.rdbInspectionFollowUpYes = New System.Windows.Forms.RadioButton()
        Me.rdbInspectionFollowUpNo = New System.Windows.Forms.RadioButton()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DTPInspectionDateStart = New System.Windows.Forms.DateTimePicker()
        Me.cboInspectionComplianceStatus = New System.Windows.Forms.ComboBox()
        Me.txtWeatherConditions = New System.Windows.Forms.TextBox()
        Me.wrnInspectionDates = New System.Windows.Forms.Label()
        Me.wrnInspectionComplianceStatus = New System.Windows.Forms.Label()
        Me.wrnInspectionOperating = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.rdbInspectionFacilityOperatingYes = New System.Windows.Forms.RadioButton()
        Me.rdbInspectionFacilityOperatingNo = New System.Windows.Forms.RadioButton()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.cboInspectionReason = New System.Windows.Forms.ComboBox()
        Me.DTPInspectionDateEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtInspectionConclusion = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtInspectionGuide = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dtpInspectionTimeEnd = New System.Windows.Forms.DateTimePicker()
        Me.dtpInspectionTimeStart = New System.Windows.Forms.DateTimePicker()
        Me.TPNotifications = New System.Windows.Forms.TabPage()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblDateSent = New System.Windows.Forms.Label()
        Me.lblNotificationDue = New System.Windows.Forms.Label()
        Me.lblNotificationOther = New System.Windows.Forms.Label()
        Me.DTPNotificationReceived = New System.Windows.Forms.DateTimePicker()
        Me.chbNotificationReceivedByAPB = New System.Windows.Forms.CheckBox()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.rdbNotificationFollowUpYes = New System.Windows.Forms.RadioButton()
        Me.rdbNotificationFollowUpNo = New System.Windows.Forms.RadioButton()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.txtNotificationTypeOther = New System.Windows.Forms.TextBox()
        Me.cboNotificationType = New System.Windows.Forms.ComboBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.dtpNotificationDate2 = New System.Windows.Forms.DateTimePicker()
        Me.lblNotificationDate2 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.txtNotificationComments = New System.Windows.Forms.TextBox()
        Me.dtpNotificationDate = New System.Windows.Forms.DateTimePicker()
        Me.lblNotificationDate = New System.Windows.Forms.Label()
        Me.TPACC = New System.Windows.Forms.TabPage()
        Me.PanelACC = New System.Windows.Forms.Panel()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.dtpAccReportingYear = New System.Windows.Forms.DateTimePicker()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.DTPACCReceivedDate = New System.Windows.Forms.DateTimePicker()
        Me.chbACCReceivedByAPB = New System.Windows.Forms.CheckBox()
        Me.btnACCSubmittals = New System.Windows.Forms.Button()
        Me.wrnACCCorrectACC = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.rdbACCCorrectACCYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCCorrectACCNo = New System.Windows.Forms.RadioButton()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.wrnACCRO = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.rdbACCROYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCRONo = New System.Windows.Forms.RadioButton()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.DTPACCPostmarked = New System.Windows.Forms.DateTimePicker()
        Me.wrnACCPostmark = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rdbACCPostmarkYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCPostmarkNo = New System.Windows.Forms.RadioButton()
        Me.lblAccReportingYear = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.wrnACCSubmittal = New System.Windows.Forms.Label()
        Me.NUPACCSubmittal = New System.Windows.Forms.NumericUpDown()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.wrnACCDatePostmarked = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtACCComments = New System.Windows.Forms.TextBox()
        Me.wrnACCPreviousDeviations = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.rdbACCPreviouslyUnreportedDeviationsYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCPreviouslyUnreportedDeviationsNo = New System.Windows.Forms.RadioButton()
        Me.lblACCPreviouslyUnreportedDeviations = New System.Windows.Forms.Label()
        Me.wrnACCDeviationsReported = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.rdbACCDeviationsReportedYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCDeviationsReportedNo = New System.Windows.Forms.RadioButton()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.wrnACCResubmittalRequested = New System.Windows.Forms.Label()
        Me.wrnACCEnforcementNeeded = New System.Windows.Forms.Label()
        Me.pnlACCResubmittalRequested = New System.Windows.Forms.Panel()
        Me.rdbACCResubmittalRequestedYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCResubmittalRequestedUnknown = New System.Windows.Forms.RadioButton()
        Me.rdbACCResubmittalRequestedNo = New System.Windows.Forms.RadioButton()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.rdbACCEnforcementNeededYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCEnforcementNeededNo = New System.Windows.Forms.RadioButton()
        Me.lblACCResubmittalRequested = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.wrnACCAllDeviationsReported = New System.Windows.Forms.Label()
        Me.wrnACCCorrect = New System.Windows.Forms.Label()
        Me.pnlACCAllDeviationsReported = New System.Windows.Forms.Panel()
        Me.rdbACCAllDeviationsReportedYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCAllDeviationsReportedUnknown = New System.Windows.Forms.RadioButton()
        Me.rdbACCAllDeviationsReportedNo = New System.Windows.Forms.RadioButton()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.rdbACCCorrectYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCCorrectNo = New System.Windows.Forms.RadioButton()
        Me.lblACCAllDeviationsReported = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.wrnACCConditions = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.rdbACCConditionsYes = New System.Windows.Forms.RadioButton()
        Me.rdbACCConditionsNo = New System.Windows.Forms.RadioButton()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.SplitterACC = New System.Windows.Forms.Splitter()
        Me.DGRACCResubmittal = New System.Windows.Forms.DataGrid()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.PanelSSCPCompliance = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1.SuspendLayout()
        Me.TCItems.SuspendLayout()
        Me.TPReport.SuspendLayout()
        Me.PanelReports.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.NUPReportSubmittal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel18.SuspendLayout()
        CType(Me.dgrReportResubmittal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPTestReports.SuspendLayout()
        Me.PanelSSCPCompliance2.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.TPInspection.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.TPNotifications.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel23.SuspendLayout()
        Me.TPACC.SuspendLayout()
        Me.PanelACC.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.NUPACCSubmittal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel15.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.pnlACCResubmittalRequested.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.pnlACCAllDeviationsReported.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.DGRACCResubmittal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSSCPCompliance.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiFile, Me.mmiTools})
        '
        'mmiFile
        '
        Me.mmiFile.Index = 0
        Me.mmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiSave, Me.mmiPrint, Me.MenuItem10, Me.mmiClose})
        Me.mmiFile.Text = "&File"
        '
        'mmiSave
        '
        Me.mmiSave.Index = 0
        Me.mmiSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mmiSave.Text = "&Save"
        '
        'mmiPrint
        '
        Me.mmiPrint.Index = 1
        Me.mmiPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.mmiPrint.Text = "&Print"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 2
        Me.MenuItem10.Text = "-"
        '
        'mmiClose
        '
        Me.mmiClose.Index = 3
        Me.mmiClose.Shortcut = System.Windows.Forms.Shortcut.CtrlW
        Me.mmiClose.Text = "&Close"
        '
        'mmiTools
        '
        Me.mmiTools.Index = 1
        Me.mmiTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiDelete})
        Me.mmiTools.Text = "&Tools"
        '
        'mmiDelete
        '
        Me.mmiDelete.Index = 0
        Me.mmiDelete.Text = "Delete This Item"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblRMPID)
        Me.GroupBox1.Controls.Add(Me.txtRMPID)
        Me.GroupBox1.Controls.Add(Me.Label35)
        Me.GroupBox1.Controls.Add(Me.cboStaffResponsible)
        Me.GroupBox1.Controls.Add(Me.DTPAcknowledgmentLetterSent)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtAIRSNumber)
        Me.GroupBox1.Controls.Add(Me.txtEnforcementNumber)
        Me.GroupBox1.Controls.Add(Me.chbEventComplete)
        Me.GroupBox1.Controls.Add(Me.DTPEventCompleteDate)
        Me.GroupBox1.Controls.Add(Me.txtTrackingNumber)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtFacilityInformation)
        Me.GroupBox1.Controls.Add(Me.txtEventInformation)
        Me.GroupBox1.Controls.Add(Me.btnEnforcementProcess)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(792, 169)
        Me.GroupBox1.TabIndex = 48
        Me.GroupBox1.TabStop = False
        '
        'lblRMPID
        '
        Me.lblRMPID.AutoSize = True
        Me.lblRMPID.Location = New System.Drawing.Point(571, 148)
        Me.lblRMPID.Name = "lblRMPID"
        Me.lblRMPID.Size = New System.Drawing.Size(45, 13)
        Me.lblRMPID.TabIndex = 347
        Me.lblRMPID.Text = "RMP ID"
        Me.lblRMPID.Visible = False
        '
        'txtRMPID
        '
        Me.txtRMPID.Location = New System.Drawing.Point(622, 145)
        Me.txtRMPID.Name = "txtRMPID"
        Me.txtRMPID.ReadOnly = True
        Me.txtRMPID.Size = New System.Drawing.Size(100, 20)
        Me.txtRMPID.TabIndex = 346
        Me.txtRMPID.Visible = False
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(530, 126)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(90, 13)
        Me.Label35.TabIndex = 344
        Me.Label35.Text = "Staff Responsible"
        '
        'cboStaffResponsible
        '
        Me.cboStaffResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaffResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaffResponsible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStaffResponsible.Location = New System.Drawing.Point(622, 122)
        Me.cboStaffResponsible.Name = "cboStaffResponsible"
        Me.cboStaffResponsible.Size = New System.Drawing.Size(154, 21)
        Me.cboStaffResponsible.TabIndex = 345
        '
        'DTPAcknowledgmentLetterSent
        '
        Me.DTPAcknowledgmentLetterSent.CustomFormat = "dd-MMM-yyyy"
        Me.DTPAcknowledgmentLetterSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPAcknowledgmentLetterSent.Location = New System.Drawing.Point(622, 96)
        Me.DTPAcknowledgmentLetterSent.Name = "DTPAcknowledgmentLetterSent"
        Me.DTPAcknowledgmentLetterSent.ShowCheckBox = True
        Me.DTPAcknowledgmentLetterSent.Size = New System.Drawing.Size(116, 20)
        Me.DTPAcknowledgmentLetterSent.TabIndex = 118
        Me.DTPAcknowledgmentLetterSent.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(499, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 13)
        Me.Label2.TabIndex = 117
        Me.Label2.Text = "Acknowledgment Sent:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(392, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 115
        Me.Label1.Text = "Event Information:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(8, 24)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(16, 20)
        Me.txtAIRSNumber.TabIndex = 114
        Me.txtAIRSNumber.Visible = False
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(174, 145)
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.ReadOnly = True
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(56, 20)
        Me.txtEnforcementNumber.TabIndex = 110
        '
        'chbEventComplete
        '
        Me.chbEventComplete.AutoSize = True
        Me.chbEventComplete.Location = New System.Drawing.Point(11, 98)
        Me.chbEventComplete.Name = "chbEventComplete"
        Me.chbEventComplete.Size = New System.Drawing.Size(73, 17)
        Me.chbEventComplete.TabIndex = 108
        Me.chbEventComplete.Text = "Complete:"
        '
        'DTPEventCompleteDate
        '
        Me.DTPEventCompleteDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEventCompleteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEventCompleteDate.Location = New System.Drawing.Point(84, 96)
        Me.DTPEventCompleteDate.Name = "DTPEventCompleteDate"
        Me.DTPEventCompleteDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPEventCompleteDate.TabIndex = 107
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(30, 24)
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.ReadOnly = True
        Me.txtTrackingNumber.Size = New System.Drawing.Size(12, 20)
        Me.txtTrackingNumber.TabIndex = 105
        Me.txtTrackingNumber.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Facility Information:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtFacilityInformation
        '
        Me.txtFacilityInformation.Location = New System.Drawing.Point(112, 8)
        Me.txtFacilityInformation.Multiline = True
        Me.txtFacilityInformation.Name = "txtFacilityInformation"
        Me.txtFacilityInformation.ReadOnly = True
        Me.txtFacilityInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFacilityInformation.Size = New System.Drawing.Size(272, 86)
        Me.txtFacilityInformation.TabIndex = 110
        '
        'txtEventInformation
        '
        Me.txtEventInformation.Location = New System.Drawing.Point(488, 8)
        Me.txtEventInformation.Multiline = True
        Me.txtEventInformation.Name = "txtEventInformation"
        Me.txtEventInformation.ReadOnly = True
        Me.txtEventInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEventInformation.Size = New System.Drawing.Size(288, 86)
        Me.txtEventInformation.TabIndex = 111
        '
        'btnEnforcementProcess
        '
        Me.btnEnforcementProcess.AutoSize = True
        Me.btnEnforcementProcess.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEnforcementProcess.Location = New System.Drawing.Point(8, 143)
        Me.btnEnforcementProcess.Name = "btnEnforcementProcess"
        Me.btnEnforcementProcess.Size = New System.Drawing.Size(163, 23)
        Me.btnEnforcementProcess.TabIndex = 110
        Me.btnEnforcementProcess.Text = "Add/Edit Enforcement Process"
        '
        'TCItems
        '
        Me.TCItems.Controls.Add(Me.TPReport)
        Me.TCItems.Controls.Add(Me.TPTestReports)
        Me.TCItems.Controls.Add(Me.TPInspection)
        Me.TCItems.Controls.Add(Me.TPNotifications)
        Me.TCItems.Controls.Add(Me.TPACC)
        Me.TCItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCItems.Location = New System.Drawing.Point(0, 0)
        Me.TCItems.Name = "TCItems"
        Me.TCItems.SelectedIndex = 0
        Me.TCItems.Size = New System.Drawing.Size(792, 396)
        Me.TCItems.TabIndex = 49
        '
        'TPReport
        '
        Me.TPReport.Controls.Add(Me.PanelReports)
        Me.TPReport.Controls.Add(Me.SplitterReport)
        Me.TPReport.Controls.Add(Me.dgrReportResubmittal)
        Me.TPReport.Location = New System.Drawing.Point(4, 22)
        Me.TPReport.Name = "TPReport"
        Me.TPReport.Size = New System.Drawing.Size(784, 370)
        Me.TPReport.TabIndex = 0
        Me.TPReport.Text = "Report"
        '
        'PanelReports
        '
        Me.PanelReports.Controls.Add(Me.Panel4)
        Me.PanelReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelReports.Location = New System.Drawing.Point(15, 0)
        Me.PanelReports.Name = "PanelReports"
        Me.PanelReports.Size = New System.Drawing.Size(769, 370)
        Me.PanelReports.TabIndex = 69
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.Controls.Add(Me.DTPReportReceivedDate)
        Me.Panel4.Controls.Add(Me.chbReportReceivedByAPB)
        Me.Panel4.Controls.Add(Me.btnReportMoreOptions)
        Me.Panel4.Controls.Add(Me.wrnReportSubmittal)
        Me.Panel4.Controls.Add(Me.NUPReportSubmittal)
        Me.Panel4.Controls.Add(Me.lblResubmittal)
        Me.Panel4.Controls.Add(Me.wrnReportPeriod)
        Me.Panel4.Controls.Add(Me.wrnShowDeviation)
        Me.Panel4.Controls.Add(Me.wrnEnforcementNeeded)
        Me.Panel4.Controls.Add(Me.wrnCompleteReport)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.cboReportSchedule)
        Me.Panel4.Controls.Add(Me.txtReportPeriodComments)
        Me.Panel4.Controls.Add(Me.DTPSentDate)
        Me.Panel4.Controls.Add(Me.dtpDueDate)
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Controls.Add(Me.Label59)
        Me.Panel4.Controls.Add(Me.Label62)
        Me.Panel4.Controls.Add(Me.Label63)
        Me.Panel4.Controls.Add(Me.Panel17)
        Me.Panel4.Controls.Add(Me.Panel18)
        Me.Panel4.Controls.Add(Me.txtReportsGeneralComments)
        Me.Panel4.Controls.Add(Me.DTPReportPeriodEnd)
        Me.Panel4.Controls.Add(Me.DTPReportPeriodStart)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(769, 370)
        Me.Panel4.TabIndex = 78
        '
        'DTPReportReceivedDate
        '
        Me.DTPReportReceivedDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPReportReceivedDate.Enabled = False
        Me.DTPReportReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPReportReceivedDate.Location = New System.Drawing.Point(152, 110)
        Me.DTPReportReceivedDate.Name = "DTPReportReceivedDate"
        Me.DTPReportReceivedDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPReportReceivedDate.TabIndex = 303
        Me.DTPReportReceivedDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'chbReportReceivedByAPB
        '
        Me.chbReportReceivedByAPB.AutoSize = True
        Me.chbReportReceivedByAPB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbReportReceivedByAPB.Location = New System.Drawing.Point(24, 115)
        Me.chbReportReceivedByAPB.Name = "chbReportReceivedByAPB"
        Me.chbReportReceivedByAPB.Size = New System.Drawing.Size(122, 17)
        Me.chbReportReceivedByAPB.TabIndex = 302
        Me.chbReportReceivedByAPB.Text = "Received by GEPD:"
        Me.chbReportReceivedByAPB.UseVisualStyleBackColor = True
        '
        'btnReportMoreOptions
        '
        Me.btnReportMoreOptions.AutoSize = True
        Me.btnReportMoreOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnReportMoreOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportMoreOptions.ImageIndex = 53
        Me.btnReportMoreOptions.Location = New System.Drawing.Point(0, 0)
        Me.btnReportMoreOptions.Name = "btnReportMoreOptions"
        Me.btnReportMoreOptions.Size = New System.Drawing.Size(74, 19)
        Me.btnReportMoreOptions.TabIndex = 293
        Me.btnReportMoreOptions.Text = "Submittal History"
        '
        'wrnReportSubmittal
        '
        Me.wrnReportSubmittal.AutoSize = True
        Me.wrnReportSubmittal.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnReportSubmittal.ForeColor = System.Drawing.Color.Tomato
        Me.wrnReportSubmittal.Location = New System.Drawing.Point(198, 28)
        Me.wrnReportSubmittal.Name = "wrnReportSubmittal"
        Me.wrnReportSubmittal.Size = New System.Drawing.Size(135, 13)
        Me.wrnReportSubmittal.TabIndex = 108
        Me.wrnReportSubmittal.Text = "Warning-value not selected"
        Me.wrnReportSubmittal.Visible = False
        '
        'NUPReportSubmittal
        '
        Me.NUPReportSubmittal.Location = New System.Drawing.Point(152, 24)
        Me.NUPReportSubmittal.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NUPReportSubmittal.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUPReportSubmittal.Name = "NUPReportSubmittal"
        Me.NUPReportSubmittal.Size = New System.Drawing.Size(40, 20)
        Me.NUPReportSubmittal.TabIndex = 107
        Me.NUPReportSubmittal.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblResubmittal
        '
        Me.lblResubmittal.AutoSize = True
        Me.lblResubmittal.Location = New System.Drawing.Point(53, 26)
        Me.lblResubmittal.Name = "lblResubmittal"
        Me.lblResubmittal.Size = New System.Drawing.Size(93, 13)
        Me.lblResubmittal.TabIndex = 106
        Me.lblResubmittal.Text = "Submittal Number:"
        '
        'wrnReportPeriod
        '
        Me.wrnReportPeriod.AutoSize = True
        Me.wrnReportPeriod.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnReportPeriod.ForeColor = System.Drawing.Color.Tomato
        Me.wrnReportPeriod.Location = New System.Drawing.Point(534, 53)
        Me.wrnReportPeriod.Name = "wrnReportPeriod"
        Me.wrnReportPeriod.Size = New System.Drawing.Size(135, 13)
        Me.wrnReportPeriod.TabIndex = 105
        Me.wrnReportPeriod.Text = "Warning-value not selected"
        Me.wrnReportPeriod.Visible = False
        '
        'wrnShowDeviation
        '
        Me.wrnShowDeviation.AutoSize = True
        Me.wrnShowDeviation.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnShowDeviation.ForeColor = System.Drawing.Color.Tomato
        Me.wrnShowDeviation.Location = New System.Drawing.Point(445, 242)
        Me.wrnShowDeviation.Name = "wrnShowDeviation"
        Me.wrnShowDeviation.Size = New System.Drawing.Size(135, 13)
        Me.wrnShowDeviation.TabIndex = 104
        Me.wrnShowDeviation.Text = "Warning-value not selected"
        Me.wrnShowDeviation.Visible = False
        '
        'wrnEnforcementNeeded
        '
        Me.wrnEnforcementNeeded.AutoSize = True
        Me.wrnEnforcementNeeded.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnEnforcementNeeded.ForeColor = System.Drawing.Color.Tomato
        Me.wrnEnforcementNeeded.Location = New System.Drawing.Point(445, 220)
        Me.wrnEnforcementNeeded.Name = "wrnEnforcementNeeded"
        Me.wrnEnforcementNeeded.Size = New System.Drawing.Size(135, 13)
        Me.wrnEnforcementNeeded.TabIndex = 103
        Me.wrnEnforcementNeeded.Text = "Warning-value not selected"
        Me.wrnEnforcementNeeded.Visible = False
        '
        'wrnCompleteReport
        '
        Me.wrnCompleteReport.AutoSize = True
        Me.wrnCompleteReport.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnCompleteReport.ForeColor = System.Drawing.Color.Tomato
        Me.wrnCompleteReport.Location = New System.Drawing.Point(445, 198)
        Me.wrnCompleteReport.Name = "wrnCompleteReport"
        Me.wrnCompleteReport.Size = New System.Drawing.Size(135, 13)
        Me.wrnCompleteReport.TabIndex = 102
        Me.wrnCompleteReport.Text = "Warning-value not selected"
        Me.wrnCompleteReport.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(77, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 79
        Me.Label7.Text = "Report Type:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(24, 75)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(122, 13)
        Me.Label8.TabIndex = 91
        Me.Label8.Text = "Report Period Comment:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(55, 142)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 13)
        Me.Label11.TabIndex = 85
        Me.Label11.Text = "Report Due Date:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(39, 168)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(107, 13)
        Me.Label9.TabIndex = 83
        Me.Label9.Text = "Date Sent by Facility:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(49, 263)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(97, 13)
        Me.Label15.TabIndex = 89
        Me.Label15.Text = "Report Comments: "
        '
        'cboReportSchedule
        '
        Me.cboReportSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReportSchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cboReportSchedule.Location = New System.Drawing.Point(152, 48)
        Me.cboReportSchedule.Name = "cboReportSchedule"
        Me.cboReportSchedule.Size = New System.Drawing.Size(164, 21)
        Me.cboReportSchedule.TabIndex = 78
        '
        'txtReportPeriodComments
        '
        Me.txtReportPeriodComments.AcceptsReturn = True
        Me.txtReportPeriodComments.Location = New System.Drawing.Point(152, 72)
        Me.txtReportPeriodComments.MaxLength = 4000
        Me.txtReportPeriodComments.Multiline = True
        Me.txtReportPeriodComments.Name = "txtReportPeriodComments"
        Me.txtReportPeriodComments.Size = New System.Drawing.Size(376, 32)
        Me.txtReportPeriodComments.TabIndex = 82
        '
        'DTPSentDate
        '
        Me.DTPSentDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPSentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPSentDate.Location = New System.Drawing.Point(152, 162)
        Me.DTPSentDate.Name = "DTPSentDate"
        Me.DTPSentDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPSentDate.TabIndex = 87
        Me.DTPSentDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.Location = New System.Drawing.Point(152, 136)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpDueDate.TabIndex = 86
        Me.dtpDueDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.rdbReportCompleteYes)
        Me.Panel5.Controls.Add(Me.rdbReportCompleteNo)
        Me.Panel5.Location = New System.Drawing.Point(352, 194)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(96, 16)
        Me.Panel5.TabIndex = 100
        '
        'rdbReportCompleteYes
        '
        Me.rdbReportCompleteYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbReportCompleteYes.Name = "rdbReportCompleteYes"
        Me.rdbReportCompleteYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbReportCompleteYes.TabIndex = 19
        Me.rdbReportCompleteYes.Text = "Yes"
        '
        'rdbReportCompleteNo
        '
        Me.rdbReportCompleteNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbReportCompleteNo.Name = "rdbReportCompleteNo"
        Me.rdbReportCompleteNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbReportCompleteNo.TabIndex = 20
        Me.rdbReportCompleteNo.Text = "No"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(149, 196)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(86, 13)
        Me.Label59.TabIndex = 99
        Me.Label59.Text = "Complete Report"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(149, 218)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(108, 13)
        Me.Label62.TabIndex = 94
        Me.Label62.Text = "Enforcement Needed"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(149, 240)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(82, 13)
        Me.Label63.TabIndex = 95
        Me.Label63.Text = "Show Deviation"
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.rdbReportDeviationYes)
        Me.Panel17.Controls.Add(Me.rdbReportDeviationNo)
        Me.Panel17.Location = New System.Drawing.Point(352, 238)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(96, 16)
        Me.Panel17.TabIndex = 96
        '
        'rdbReportDeviationYes
        '
        Me.rdbReportDeviationYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbReportDeviationYes.Name = "rdbReportDeviationYes"
        Me.rdbReportDeviationYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbReportDeviationYes.TabIndex = 19
        Me.rdbReportDeviationYes.Text = "Yes"
        '
        'rdbReportDeviationNo
        '
        Me.rdbReportDeviationNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbReportDeviationNo.Name = "rdbReportDeviationNo"
        Me.rdbReportDeviationNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbReportDeviationNo.TabIndex = 20
        Me.rdbReportDeviationNo.Text = "No"
        '
        'Panel18
        '
        Me.Panel18.Controls.Add(Me.rdbReportEnforcementYes)
        Me.Panel18.Controls.Add(Me.rdbReportEnforcementNo)
        Me.Panel18.Location = New System.Drawing.Point(352, 216)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(96, 16)
        Me.Panel18.TabIndex = 93
        '
        'rdbReportEnforcementYes
        '
        Me.rdbReportEnforcementYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbReportEnforcementYes.Name = "rdbReportEnforcementYes"
        Me.rdbReportEnforcementYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbReportEnforcementYes.TabIndex = 19
        Me.rdbReportEnforcementYes.Text = "Yes"
        '
        'rdbReportEnforcementNo
        '
        Me.rdbReportEnforcementNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbReportEnforcementNo.Name = "rdbReportEnforcementNo"
        Me.rdbReportEnforcementNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbReportEnforcementNo.TabIndex = 20
        Me.rdbReportEnforcementNo.Text = "No"
        '
        'txtReportsGeneralComments
        '
        Me.txtReportsGeneralComments.AcceptsReturn = True
        Me.txtReportsGeneralComments.Location = New System.Drawing.Point(152, 260)
        Me.txtReportsGeneralComments.MaxLength = 4000
        Me.txtReportsGeneralComments.Multiline = True
        Me.txtReportsGeneralComments.Name = "txtReportsGeneralComments"
        Me.txtReportsGeneralComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReportsGeneralComments.Size = New System.Drawing.Size(376, 72)
        Me.txtReportsGeneralComments.TabIndex = 88
        '
        'DTPReportPeriodEnd
        '
        Me.DTPReportPeriodEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPReportPeriodEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPReportPeriodEnd.Location = New System.Drawing.Point(428, 48)
        Me.DTPReportPeriodEnd.Name = "DTPReportPeriodEnd"
        Me.DTPReportPeriodEnd.Size = New System.Drawing.Size(100, 20)
        Me.DTPReportPeriodEnd.TabIndex = 81
        Me.DTPReportPeriodEnd.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'DTPReportPeriodStart
        '
        Me.DTPReportPeriodStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPReportPeriodStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPReportPeriodStart.Location = New System.Drawing.Point(322, 48)
        Me.DTPReportPeriodStart.Name = "DTPReportPeriodStart"
        Me.DTPReportPeriodStart.Size = New System.Drawing.Size(100, 20)
        Me.DTPReportPeriodStart.TabIndex = 80
        Me.DTPReportPeriodStart.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'SplitterReport
        '
        Me.SplitterReport.BackColor = System.Drawing.SystemColors.Highlight
        Me.SplitterReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitterReport.Location = New System.Drawing.Point(10, 0)
        Me.SplitterReport.Name = "SplitterReport"
        Me.SplitterReport.Size = New System.Drawing.Size(5, 370)
        Me.SplitterReport.TabIndex = 68
        Me.SplitterReport.TabStop = False
        '
        'dgrReportResubmittal
        '
        Me.dgrReportResubmittal.DataMember = ""
        Me.dgrReportResubmittal.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgrReportResubmittal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgrReportResubmittal.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrReportResubmittal.Location = New System.Drawing.Point(0, 0)
        Me.dgrReportResubmittal.Name = "dgrReportResubmittal"
        Me.dgrReportResubmittal.ReadOnly = True
        Me.dgrReportResubmittal.Size = New System.Drawing.Size(10, 370)
        Me.dgrReportResubmittal.TabIndex = 30
        '
        'TPTestReports
        '
        Me.TPTestReports.Controls.Add(Me.PanelSSCPCompliance2)
        Me.TPTestReports.Location = New System.Drawing.Point(4, 22)
        Me.TPTestReports.Name = "TPTestReports"
        Me.TPTestReports.Size = New System.Drawing.Size(784, 342)
        Me.TPTestReports.TabIndex = 2
        Me.TPTestReports.Text = "ISMP Test Report"
        '
        'PanelSSCPCompliance2
        '
        Me.PanelSSCPCompliance2.AutoScroll = True
        Me.PanelSSCPCompliance2.Controls.Add(Me.DTPTestReportReceivedDate)
        Me.PanelSSCPCompliance2.Controls.Add(Me.chbISMPTestReportReceivedByAPB)
        Me.PanelSSCPCompliance2.Controls.Add(Me.txtPollutantTested)
        Me.PanelSSCPCompliance2.Controls.Add(Me.txtUnitTested)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label23)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label19)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label6)
        Me.PanelSSCPCompliance2.Controls.Add(Me.btnViewTestReport)
        Me.PanelSSCPCompliance2.Controls.Add(Me.txtTestReportReceivedbySSCPDate)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label5)
        Me.PanelSSCPCompliance2.Controls.Add(Me.txtTestReportISMPCompleteDate)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label4)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Panel22)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label71)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label38)
        Me.PanelSSCPCompliance2.Controls.Add(Me.DTPTestReportDueDate)
        Me.PanelSSCPCompliance2.Controls.Add(Me.chbTestReportChangeDueDate)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label68)
        Me.PanelSSCPCompliance2.Controls.Add(Me.txtISMPReferenceNumber)
        Me.PanelSSCPCompliance2.Controls.Add(Me.DTPTestReportNewDueDate)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label50)
        Me.PanelSSCPCompliance2.Controls.Add(Me.txtTestReportComments)
        Me.PanelSSCPCompliance2.Controls.Add(Me.Label46)
        Me.PanelSSCPCompliance2.Controls.Add(Me.txtTestReportDueDate)
        Me.PanelSSCPCompliance2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSSCPCompliance2.Location = New System.Drawing.Point(0, 0)
        Me.PanelSSCPCompliance2.Name = "PanelSSCPCompliance2"
        Me.PanelSSCPCompliance2.Size = New System.Drawing.Size(784, 342)
        Me.PanelSSCPCompliance2.TabIndex = 161
        '
        'DTPTestReportReceivedDate
        '
        Me.DTPTestReportReceivedDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestReportReceivedDate.Enabled = False
        Me.DTPTestReportReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestReportReceivedDate.Location = New System.Drawing.Point(208, 32)
        Me.DTPTestReportReceivedDate.Name = "DTPTestReportReceivedDate"
        Me.DTPTestReportReceivedDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPTestReportReceivedDate.TabIndex = 301
        Me.DTPTestReportReceivedDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'chbISMPTestReportReceivedByAPB
        '
        Me.chbISMPTestReportReceivedByAPB.AutoSize = True
        Me.chbISMPTestReportReceivedByAPB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbISMPTestReportReceivedByAPB.Location = New System.Drawing.Point(82, 32)
        Me.chbISMPTestReportReceivedByAPB.Name = "chbISMPTestReportReceivedByAPB"
        Me.chbISMPTestReportReceivedByAPB.Size = New System.Drawing.Size(122, 17)
        Me.chbISMPTestReportReceivedByAPB.TabIndex = 300
        Me.chbISMPTestReportReceivedByAPB.Text = "Received by GEPD:"
        Me.chbISMPTestReportReceivedByAPB.UseVisualStyleBackColor = True
        '
        'txtPollutantTested
        '
        Me.txtPollutantTested.Location = New System.Drawing.Point(208, 104)
        Me.txtPollutantTested.Name = "txtPollutantTested"
        Me.txtPollutantTested.ReadOnly = True
        Me.txtPollutantTested.Size = New System.Drawing.Size(264, 20)
        Me.txtPollutantTested.TabIndex = 299
        '
        'txtUnitTested
        '
        Me.txtUnitTested.Location = New System.Drawing.Point(208, 80)
        Me.txtUnitTested.Name = "txtUnitTested"
        Me.txtUnitTested.ReadOnly = True
        Me.txtUnitTested.Size = New System.Drawing.Size(264, 20)
        Me.txtUnitTested.TabIndex = 298
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(115, 106)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(87, 13)
        Me.Label23.TabIndex = 297
        Me.Label23.Text = "Pollutant Tested:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(136, 82)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(61, 13)
        Me.Label19.TabIndex = 296
        Me.Label19.Text = "Unit tested:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(360, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 12)
        Me.Label6.TabIndex = 295
        Me.Label6.Text = "View Test Report"
        '
        'btnViewTestReport
        '
        Me.btnViewTestReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewTestReport.ImageIndex = 53
        Me.btnViewTestReport.Location = New System.Drawing.Point(330, 7)
        Me.btnViewTestReport.Name = "btnViewTestReport"
        Me.btnViewTestReport.Size = New System.Drawing.Size(24, 23)
        Me.btnViewTestReport.TabIndex = 294
        '
        'txtTestReportReceivedbySSCPDate
        '
        Me.txtTestReportReceivedbySSCPDate.Location = New System.Drawing.Point(208, 128)
        Me.txtTestReportReceivedbySSCPDate.Name = "txtTestReportReceivedbySSCPDate"
        Me.txtTestReportReceivedbySSCPDate.ReadOnly = True
        Me.txtTestReportReceivedbySSCPDate.Size = New System.Drawing.Size(120, 20)
        Me.txtTestReportReceivedbySSCPDate.TabIndex = 167
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 131)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(198, 13)
        Me.Label5.TabIndex = 166
        Me.Label5.Text = "Test Summary Received by Compliance:"
        '
        'txtTestReportISMPCompleteDate
        '
        Me.txtTestReportISMPCompleteDate.Location = New System.Drawing.Point(208, 56)
        Me.txtTestReportISMPCompleteDate.Name = "txtTestReportISMPCompleteDate"
        Me.txtTestReportISMPCompleteDate.ReadOnly = True
        Me.txtTestReportISMPCompleteDate.Size = New System.Drawing.Size(120, 20)
        Me.txtTestReportISMPCompleteDate.TabIndex = 165
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(162, 13)
        Me.Label4.TabIndex = 164
        Me.Label4.Text = "Test Report Completed by ISMP:"
        '
        'Panel22
        '
        Me.Panel22.Controls.Add(Me.rdbTestReportFollowUpYes)
        Me.Panel22.Controls.Add(Me.rdbTestReportFollowUpNo)
        Me.Panel22.Location = New System.Drawing.Point(168, 288)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(96, 16)
        Me.Panel22.TabIndex = 163
        '
        'rdbTestReportFollowUpYes
        '
        Me.rdbTestReportFollowUpYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbTestReportFollowUpYes.Name = "rdbTestReportFollowUpYes"
        Me.rdbTestReportFollowUpYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbTestReportFollowUpYes.TabIndex = 19
        Me.rdbTestReportFollowUpYes.Text = "Yes"
        '
        'rdbTestReportFollowUpNo
        '
        Me.rdbTestReportFollowUpNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbTestReportFollowUpNo.Name = "rdbTestReportFollowUpNo"
        Me.rdbTestReportFollowUpNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbTestReportFollowUpNo.TabIndex = 20
        Me.rdbTestReportFollowUpNo.Text = "No"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(16, 288)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(121, 13)
        Me.Label71.TabIndex = 161
        Me.Label71.Text = "Follow-Up Action Taken"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(352, 34)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(115, 13)
        Me.Label38.TabIndex = 142
        Me.Label38.Text = "Date Test Report Due:"
        '
        'DTPTestReportDueDate
        '
        Me.DTPTestReportDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestReportDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestReportDueDate.Location = New System.Drawing.Point(472, 56)
        Me.DTPTestReportDueDate.Name = "DTPTestReportDueDate"
        Me.DTPTestReportDueDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPTestReportDueDate.TabIndex = 160
        Me.DTPTestReportDueDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        Me.DTPTestReportDueDate.Visible = False
        '
        'chbTestReportChangeDueDate
        '
        Me.chbTestReportChangeDueDate.AutoSize = True
        Me.chbTestReportChangeDueDate.Location = New System.Drawing.Point(600, 34)
        Me.chbTestReportChangeDueDate.Name = "chbTestReportChangeDueDate"
        Me.chbTestReportChangeDueDate.Size = New System.Drawing.Size(112, 17)
        Me.chbTestReportChangeDueDate.TabIndex = 159
        Me.chbTestReportChangeDueDate.Text = "Change Due Date"
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(8, 10)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(188, 13)
        Me.Label68.TabIndex = 154
        Me.Label68.Text = "ISMP Test Report Reference Number:"
        '
        'txtISMPReferenceNumber
        '
        Me.txtISMPReferenceNumber.Location = New System.Drawing.Point(208, 8)
        Me.txtISMPReferenceNumber.MaxLength = 9
        Me.txtISMPReferenceNumber.Name = "txtISMPReferenceNumber"
        Me.txtISMPReferenceNumber.Size = New System.Drawing.Size(120, 20)
        Me.txtISMPReferenceNumber.TabIndex = 153
        '
        'DTPTestReportNewDueDate
        '
        Me.DTPTestReportNewDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestReportNewDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestReportNewDueDate.Location = New System.Drawing.Point(208, 256)
        Me.DTPTestReportNewDueDate.Name = "DTPTestReportNewDueDate"
        Me.DTPTestReportNewDueDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPTestReportNewDueDate.TabIndex = 150
        Me.DTPTestReportNewDueDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(39, 256)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(160, 13)
        Me.Label50.TabIndex = 149
        Me.Label50.Text = "Due Date for Next Test Reports:"
        '
        'txtTestReportComments
        '
        Me.txtTestReportComments.AcceptsReturn = True
        Me.txtTestReportComments.Location = New System.Drawing.Point(208, 160)
        Me.txtTestReportComments.MaxLength = 4000
        Me.txtTestReportComments.Multiline = True
        Me.txtTestReportComments.Name = "txtTestReportComments"
        Me.txtTestReportComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTestReportComments.Size = New System.Drawing.Size(400, 88)
        Me.txtTestReportComments.TabIndex = 148
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(80, 160)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(118, 13)
        Me.Label46.TabIndex = 147
        Me.Label46.Text = "Test Report Comments:"
        '
        'txtTestReportDueDate
        '
        Me.txtTestReportDueDate.Location = New System.Drawing.Point(472, 32)
        Me.txtTestReportDueDate.Name = "txtTestReportDueDate"
        Me.txtTestReportDueDate.ReadOnly = True
        Me.txtTestReportDueDate.Size = New System.Drawing.Size(120, 20)
        Me.txtTestReportDueDate.TabIndex = 143
        '
        'TPInspection
        '
        Me.TPInspection.Controls.Add(Me.Panel11)
        Me.TPInspection.Location = New System.Drawing.Point(4, 22)
        Me.TPInspection.Name = "TPInspection"
        Me.TPInspection.Size = New System.Drawing.Size(784, 342)
        Me.TPInspection.TabIndex = 1
        Me.TPInspection.Text = "Inspection"
        '
        'Panel11
        '
        Me.Panel11.AutoScroll = True
        Me.Panel11.Controls.Add(Me.Panel21)
        Me.Panel11.Controls.Add(Me.Label34)
        Me.Panel11.Controls.Add(Me.Label17)
        Me.Panel11.Controls.Add(Me.Label16)
        Me.Panel11.Controls.Add(Me.Label14)
        Me.Panel11.Controls.Add(Me.Label12)
        Me.Panel11.Controls.Add(Me.DTPInspectionDateStart)
        Me.Panel11.Controls.Add(Me.cboInspectionComplianceStatus)
        Me.Panel11.Controls.Add(Me.txtWeatherConditions)
        Me.Panel11.Controls.Add(Me.wrnInspectionDates)
        Me.Panel11.Controls.Add(Me.wrnInspectionComplianceStatus)
        Me.Panel11.Controls.Add(Me.wrnInspectionOperating)
        Me.Panel11.Controls.Add(Me.Panel19)
        Me.Panel11.Controls.Add(Me.Label64)
        Me.Panel11.Controls.Add(Me.cboInspectionReason)
        Me.Panel11.Controls.Add(Me.DTPInspectionDateEnd)
        Me.Panel11.Controls.Add(Me.Label24)
        Me.Panel11.Controls.Add(Me.Label22)
        Me.Panel11.Controls.Add(Me.txtInspectionConclusion)
        Me.Panel11.Controls.Add(Me.Label21)
        Me.Panel11.Controls.Add(Me.txtInspectionGuide)
        Me.Panel11.Controls.Add(Me.Label20)
        Me.Panel11.Controls.Add(Me.Label18)
        Me.Panel11.Controls.Add(Me.dtpInspectionTimeEnd)
        Me.Panel11.Controls.Add(Me.dtpInspectionTimeStart)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(784, 342)
        Me.Panel11.TabIndex = 81
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.rdbInspectionFollowUpYes)
        Me.Panel21.Controls.Add(Me.rdbInspectionFollowUpNo)
        Me.Panel21.Location = New System.Drawing.Point(160, 304)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(96, 16)
        Me.Panel21.TabIndex = 83
        '
        'rdbInspectionFollowUpYes
        '
        Me.rdbInspectionFollowUpYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbInspectionFollowUpYes.Name = "rdbInspectionFollowUpYes"
        Me.rdbInspectionFollowUpYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbInspectionFollowUpYes.TabIndex = 19
        Me.rdbInspectionFollowUpYes.Text = "Yes"
        '
        'rdbInspectionFollowUpNo
        '
        Me.rdbInspectionFollowUpNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbInspectionFollowUpNo.Name = "rdbInspectionFollowUpNo"
        Me.rdbInspectionFollowUpNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbInspectionFollowUpNo.TabIndex = 20
        Me.rdbInspectionFollowUpNo.Text = "No"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(8, 304)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(124, 13)
        Me.Label34.TabIndex = 81
        Me.Label34.Text = "Follow-Up Action Taken:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(157, 8)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(32, 13)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "Start:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(263, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(29, 13)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "End:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(8, 50)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 13)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Inspection Time: "
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 26)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 13)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Date Inspected:"
        '
        'DTPInspectionDateStart
        '
        Me.DTPInspectionDateStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPInspectionDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPInspectionDateStart.Location = New System.Drawing.Point(160, 24)
        Me.DTPInspectionDateStart.Name = "DTPInspectionDateStart"
        Me.DTPInspectionDateStart.Size = New System.Drawing.Size(100, 20)
        Me.DTPInspectionDateStart.TabIndex = 3
        Me.DTPInspectionDateStart.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'cboInspectionComplianceStatus
        '
        Me.cboInspectionComplianceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInspectionComplianceStatus.Location = New System.Drawing.Point(160, 176)
        Me.cboInspectionComplianceStatus.Name = "cboInspectionComplianceStatus"
        Me.cboInspectionComplianceStatus.Size = New System.Drawing.Size(206, 21)
        Me.cboInspectionComplianceStatus.TabIndex = 15
        '
        'txtWeatherConditions
        '
        Me.txtWeatherConditions.Location = New System.Drawing.Point(160, 96)
        Me.txtWeatherConditions.MaxLength = 100
        Me.txtWeatherConditions.Name = "txtWeatherConditions"
        Me.txtWeatherConditions.Size = New System.Drawing.Size(206, 20)
        Me.txtWeatherConditions.TabIndex = 80
        '
        'wrnInspectionDates
        '
        Me.wrnInspectionDates.AutoSize = True
        Me.wrnInspectionDates.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnInspectionDates.ForeColor = System.Drawing.Color.Tomato
        Me.wrnInspectionDates.Location = New System.Drawing.Point(372, 28)
        Me.wrnInspectionDates.Name = "wrnInspectionDates"
        Me.wrnInspectionDates.Size = New System.Drawing.Size(68, 13)
        Me.wrnInspectionDates.TabIndex = 78
        Me.wrnInspectionDates.Text = "Invalid Dates"
        Me.wrnInspectionDates.Visible = False
        '
        'wrnInspectionComplianceStatus
        '
        Me.wrnInspectionComplianceStatus.AutoSize = True
        Me.wrnInspectionComplianceStatus.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnInspectionComplianceStatus.ForeColor = System.Drawing.Color.Tomato
        Me.wrnInspectionComplianceStatus.Location = New System.Drawing.Point(372, 181)
        Me.wrnInspectionComplianceStatus.Name = "wrnInspectionComplianceStatus"
        Me.wrnInspectionComplianceStatus.Size = New System.Drawing.Size(135, 13)
        Me.wrnInspectionComplianceStatus.TabIndex = 77
        Me.wrnInspectionComplianceStatus.Text = "Warning-value not selected"
        Me.wrnInspectionComplianceStatus.Visible = False
        '
        'wrnInspectionOperating
        '
        Me.wrnInspectionOperating.AutoSize = True
        Me.wrnInspectionOperating.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnInspectionOperating.ForeColor = System.Drawing.Color.Tomato
        Me.wrnInspectionOperating.Location = New System.Drawing.Point(248, 153)
        Me.wrnInspectionOperating.Name = "wrnInspectionOperating"
        Me.wrnInspectionOperating.Size = New System.Drawing.Size(135, 13)
        Me.wrnInspectionOperating.TabIndex = 76
        Me.wrnInspectionOperating.Text = "Warning-value not selected"
        Me.wrnInspectionOperating.Visible = False
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.rdbInspectionFacilityOperatingYes)
        Me.Panel19.Controls.Add(Me.rdbInspectionFacilityOperatingNo)
        Me.Panel19.Location = New System.Drawing.Point(160, 152)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(96, 16)
        Me.Panel19.TabIndex = 69
        '
        'rdbInspectionFacilityOperatingYes
        '
        Me.rdbInspectionFacilityOperatingYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbInspectionFacilityOperatingYes.Name = "rdbInspectionFacilityOperatingYes"
        Me.rdbInspectionFacilityOperatingYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbInspectionFacilityOperatingYes.TabIndex = 19
        Me.rdbInspectionFacilityOperatingYes.Text = "Yes"
        '
        'rdbInspectionFacilityOperatingNo
        '
        Me.rdbInspectionFacilityOperatingNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbInspectionFacilityOperatingNo.Name = "rdbInspectionFacilityOperatingNo"
        Me.rdbInspectionFacilityOperatingNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbInspectionFacilityOperatingNo.TabIndex = 20
        Me.rdbInspectionFacilityOperatingNo.Text = "No"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(8, 152)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(91, 13)
        Me.Label64.TabIndex = 68
        Me.Label64.Text = "Facility Operating:"
        '
        'cboInspectionReason
        '
        Me.cboInspectionReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInspectionReason.Location = New System.Drawing.Point(160, 72)
        Me.cboInspectionReason.Name = "cboInspectionReason"
        Me.cboInspectionReason.Size = New System.Drawing.Size(206, 21)
        Me.cboInspectionReason.TabIndex = 31
        '
        'DTPInspectionDateEnd
        '
        Me.DTPInspectionDateEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPInspectionDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPInspectionDateEnd.Location = New System.Drawing.Point(266, 24)
        Me.DTPInspectionDateEnd.Name = "DTPInspectionDateEnd"
        Me.DTPInspectionDateEnd.Size = New System.Drawing.Size(100, 20)
        Me.DTPInspectionDateEnd.TabIndex = 29
        Me.DTPInspectionDateEnd.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(8, 74)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(99, 13)
        Me.Label24.TabIndex = 27
        Me.Label24.Text = "Inspection Reason:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(8, 98)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(103, 13)
        Me.Label22.TabIndex = 22
        Me.Label22.Text = "Weather Conditions:"
        '
        'txtInspectionConclusion
        '
        Me.txtInspectionConclusion.AcceptsReturn = True
        Me.txtInspectionConclusion.Location = New System.Drawing.Point(160, 208)
        Me.txtInspectionConclusion.MaxLength = 4000
        Me.txtInspectionConclusion.Multiline = True
        Me.txtInspectionConclusion.Name = "txtInspectionConclusion"
        Me.txtInspectionConclusion.Size = New System.Drawing.Size(552, 88)
        Me.txtInspectionConclusion.TabIndex = 21
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(8, 208)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(111, 13)
        Me.Label21.TabIndex = 20
        Me.Label21.Text = "Inspection Comments:"
        '
        'txtInspectionGuide
        '
        Me.txtInspectionGuide.Location = New System.Drawing.Point(160, 120)
        Me.txtInspectionGuide.MaxLength = 100
        Me.txtInspectionGuide.Name = "txtInspectionGuide"
        Me.txtInspectionGuide.Size = New System.Drawing.Size(206, 20)
        Me.txtInspectionGuide.TabIndex = 19
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(8, 122)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(136, 13)
        Me.Label20.TabIndex = 18
        Me.Label20.Text = "Facility Inspection Guide(s):"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(8, 178)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(98, 13)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "Compliance Status:"
        '
        'dtpInspectionTimeEnd
        '
        Me.dtpInspectionTimeEnd.CustomFormat = "HH:mm:ss"
        Me.dtpInspectionTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInspectionTimeEnd.Location = New System.Drawing.Point(266, 48)
        Me.dtpInspectionTimeEnd.Name = "dtpInspectionTimeEnd"
        Me.dtpInspectionTimeEnd.ShowUpDown = True
        Me.dtpInspectionTimeEnd.Size = New System.Drawing.Size(100, 20)
        Me.dtpInspectionTimeEnd.TabIndex = 13
        Me.dtpInspectionTimeEnd.Value = New Date(2005, 6, 5, 12, 0, 0, 0)
        '
        'dtpInspectionTimeStart
        '
        Me.dtpInspectionTimeStart.CustomFormat = "HH:mm:ss"
        Me.dtpInspectionTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInspectionTimeStart.Location = New System.Drawing.Point(160, 48)
        Me.dtpInspectionTimeStart.Name = "dtpInspectionTimeStart"
        Me.dtpInspectionTimeStart.ShowUpDown = True
        Me.dtpInspectionTimeStart.Size = New System.Drawing.Size(100, 20)
        Me.dtpInspectionTimeStart.TabIndex = 12
        Me.dtpInspectionTimeStart.Value = New Date(2005, 6, 5, 12, 0, 0, 0)
        '
        'TPNotifications
        '
        Me.TPNotifications.Controls.Add(Me.Panel16)
        Me.TPNotifications.Location = New System.Drawing.Point(4, 22)
        Me.TPNotifications.Name = "TPNotifications"
        Me.TPNotifications.Size = New System.Drawing.Size(784, 342)
        Me.TPNotifications.TabIndex = 3
        Me.TPNotifications.Text = "Notifications"
        '
        'Panel16
        '
        Me.Panel16.AutoScroll = True
        Me.Panel16.Controls.Add(Me.Label10)
        Me.Panel16.Controls.Add(Me.lblDateSent)
        Me.Panel16.Controls.Add(Me.lblNotificationDue)
        Me.Panel16.Controls.Add(Me.lblNotificationOther)
        Me.Panel16.Controls.Add(Me.DTPNotificationReceived)
        Me.Panel16.Controls.Add(Me.chbNotificationReceivedByAPB)
        Me.Panel16.Controls.Add(Me.Panel23)
        Me.Panel16.Controls.Add(Me.Label72)
        Me.Panel16.Controls.Add(Me.Label73)
        Me.Panel16.Controls.Add(Me.txtNotificationTypeOther)
        Me.Panel16.Controls.Add(Me.cboNotificationType)
        Me.Panel16.Controls.Add(Me.Label66)
        Me.Panel16.Controls.Add(Me.dtpNotificationDate2)
        Me.Panel16.Controls.Add(Me.lblNotificationDate2)
        Me.Panel16.Controls.Add(Me.Label51)
        Me.Panel16.Controls.Add(Me.txtNotificationComments)
        Me.Panel16.Controls.Add(Me.dtpNotificationDate)
        Me.Panel16.Controls.Add(Me.lblNotificationDate)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel16.Location = New System.Drawing.Point(0, 0)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(784, 342)
        Me.Panel16.TabIndex = 144
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(260, 318)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(426, 13)
        Me.Label10.TabIndex = 172
        Me.Label10.Text = "Please note that a notification cannot be the Discovery Event for an Enforcement " &
    "Action."
        '
        'lblDateSent
        '
        Me.lblDateSent.AutoSize = True
        Me.lblDateSent.Location = New System.Drawing.Point(262, 68)
        Me.lblDateSent.Name = "lblDateSent"
        Me.lblDateSent.Size = New System.Drawing.Size(167, 13)
        Me.lblDateSent.TabIndex = 171
        Me.lblDateSent.Text = "(Do not check if date is unknown)"
        '
        'lblNotificationDue
        '
        Me.lblNotificationDue.AutoSize = True
        Me.lblNotificationDue.Location = New System.Drawing.Point(262, 44)
        Me.lblNotificationDue.Name = "lblNotificationDue"
        Me.lblNotificationDue.Size = New System.Drawing.Size(146, 13)
        Me.lblNotificationDue.TabIndex = 170
        Me.lblNotificationDue.Text = "(Do not check if no due date)"
        '
        'lblNotificationOther
        '
        Me.lblNotificationOther.AutoSize = True
        Me.lblNotificationOther.Location = New System.Drawing.Point(8, 90)
        Me.lblNotificationOther.Name = "lblNotificationOther"
        Me.lblNotificationOther.Size = New System.Drawing.Size(575, 13)
        Me.lblNotificationOther.TabIndex = 169
        Me.lblNotificationOther.Text = "NOTE: This will NOT change the facility operating status or CMS status. Your mana" &
    "ger will need to make those changes."
        '
        'DTPNotificationReceived
        '
        Me.DTPNotificationReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPNotificationReceived.Enabled = False
        Me.DTPNotificationReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPNotificationReceived.Location = New System.Drawing.Point(136, 10)
        Me.DTPNotificationReceived.Name = "DTPNotificationReceived"
        Me.DTPNotificationReceived.Size = New System.Drawing.Size(100, 20)
        Me.DTPNotificationReceived.TabIndex = 168
        Me.DTPNotificationReceived.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'chbNotificationReceivedByAPB
        '
        Me.chbNotificationReceivedByAPB.AutoSize = True
        Me.chbNotificationReceivedByAPB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbNotificationReceivedByAPB.Location = New System.Drawing.Point(7, 13)
        Me.chbNotificationReceivedByAPB.Name = "chbNotificationReceivedByAPB"
        Me.chbNotificationReceivedByAPB.Size = New System.Drawing.Size(122, 17)
        Me.chbNotificationReceivedByAPB.TabIndex = 167
        Me.chbNotificationReceivedByAPB.Text = "Received by GEPD:"
        Me.chbNotificationReceivedByAPB.UseVisualStyleBackColor = True
        '
        'Panel23
        '
        Me.Panel23.Controls.Add(Me.rdbNotificationFollowUpYes)
        Me.Panel23.Controls.Add(Me.rdbNotificationFollowUpNo)
        Me.Panel23.Location = New System.Drawing.Point(168, 316)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(96, 16)
        Me.Panel23.TabIndex = 166
        '
        'rdbNotificationFollowUpYes
        '
        Me.rdbNotificationFollowUpYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbNotificationFollowUpYes.Name = "rdbNotificationFollowUpYes"
        Me.rdbNotificationFollowUpYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbNotificationFollowUpYes.TabIndex = 19
        Me.rdbNotificationFollowUpYes.Text = "Yes"
        '
        'rdbNotificationFollowUpNo
        '
        Me.rdbNotificationFollowUpNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbNotificationFollowUpNo.Name = "rdbNotificationFollowUpNo"
        Me.rdbNotificationFollowUpNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbNotificationFollowUpNo.TabIndex = 20
        Me.rdbNotificationFollowUpNo.Text = "No"
        '
        'Label72
        '
        Me.Label72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label72.Location = New System.Drawing.Point(16, 332)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(235, 1)
        Me.Label72.TabIndex = 165
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(16, 316)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(121, 13)
        Me.Label73.TabIndex = 164
        Me.Label73.Text = "Follow-Up Action Taken"
        '
        'txtNotificationTypeOther
        '
        Me.txtNotificationTypeOther.Location = New System.Drawing.Point(568, 64)
        Me.txtNotificationTypeOther.Name = "txtNotificationTypeOther"
        Me.txtNotificationTypeOther.Size = New System.Drawing.Size(192, 20)
        Me.txtNotificationTypeOther.TabIndex = 31
        Me.txtNotificationTypeOther.Visible = False
        '
        'cboNotificationType
        '
        Me.cboNotificationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNotificationType.Location = New System.Drawing.Point(568, 40)
        Me.cboNotificationType.Name = "cboNotificationType"
        Me.cboNotificationType.Size = New System.Drawing.Size(192, 21)
        Me.cboNotificationType.TabIndex = 30
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(472, 44)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(90, 13)
        Me.Label66.TabIndex = 29
        Me.Label66.Text = "Notification Type:"
        '
        'dtpNotificationDate2
        '
        Me.dtpNotificationDate2.Checked = False
        Me.dtpNotificationDate2.CustomFormat = "dd-MMM-yyyy"
        Me.dtpNotificationDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNotificationDate2.Location = New System.Drawing.Point(136, 64)
        Me.dtpNotificationDate2.Name = "dtpNotificationDate2"
        Me.dtpNotificationDate2.ShowCheckBox = True
        Me.dtpNotificationDate2.Size = New System.Drawing.Size(120, 20)
        Me.dtpNotificationDate2.TabIndex = 28
        Me.dtpNotificationDate2.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'lblNotificationDate2
        '
        Me.lblNotificationDate2.AutoSize = True
        Me.lblNotificationDate2.Location = New System.Drawing.Point(8, 66)
        Me.lblNotificationDate2.Name = "lblNotificationDate2"
        Me.lblNotificationDate2.Size = New System.Drawing.Size(107, 13)
        Me.lblNotificationDate2.TabIndex = 27
        Me.lblNotificationDate2.Text = "Date Sent by Facility:"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(8, 112)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(110, 13)
        Me.Label51.TabIndex = 26
        Me.Label51.Text = "Notification Comment:"
        '
        'txtNotificationComments
        '
        Me.txtNotificationComments.AcceptsReturn = True
        Me.txtNotificationComments.AcceptsTab = True
        Me.txtNotificationComments.Location = New System.Drawing.Point(8, 128)
        Me.txtNotificationComments.MaxLength = 4000
        Me.txtNotificationComments.Multiline = True
        Me.txtNotificationComments.Name = "txtNotificationComments"
        Me.txtNotificationComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotificationComments.Size = New System.Drawing.Size(752, 182)
        Me.txtNotificationComments.TabIndex = 18
        '
        'dtpNotificationDate
        '
        Me.dtpNotificationDate.Checked = False
        Me.dtpNotificationDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpNotificationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNotificationDate.Location = New System.Drawing.Point(136, 40)
        Me.dtpNotificationDate.Name = "dtpNotificationDate"
        Me.dtpNotificationDate.ShowCheckBox = True
        Me.dtpNotificationDate.Size = New System.Drawing.Size(120, 20)
        Me.dtpNotificationDate.TabIndex = 16
        Me.dtpNotificationDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'lblNotificationDate
        '
        Me.lblNotificationDate.AutoSize = True
        Me.lblNotificationDate.Location = New System.Drawing.Point(8, 42)
        Me.lblNotificationDate.Name = "lblNotificationDate"
        Me.lblNotificationDate.Size = New System.Drawing.Size(112, 13)
        Me.lblNotificationDate.TabIndex = 12
        Me.lblNotificationDate.Text = "Notification Due Date:"
        '
        'TPACC
        '
        Me.TPACC.Controls.Add(Me.PanelACC)
        Me.TPACC.Controls.Add(Me.SplitterACC)
        Me.TPACC.Controls.Add(Me.DGRACCResubmittal)
        Me.TPACC.Location = New System.Drawing.Point(4, 22)
        Me.TPACC.Name = "TPACC"
        Me.TPACC.Size = New System.Drawing.Size(784, 342)
        Me.TPACC.TabIndex = 4
        Me.TPACC.Text = "Annual Compliance Certifications"
        '
        'PanelACC
        '
        Me.PanelACC.Controls.Add(Me.Panel20)
        Me.PanelACC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelACC.Location = New System.Drawing.Point(15, 0)
        Me.PanelACC.Name = "PanelACC"
        Me.PanelACC.Size = New System.Drawing.Size(769, 342)
        Me.PanelACC.TabIndex = 79
        '
        'Panel20
        '
        Me.Panel20.AutoScroll = True
        Me.Panel20.Controls.Add(Me.dtpAccReportingYear)
        Me.Panel20.Controls.Add(Me.Label25)
        Me.Panel20.Controls.Add(Me.DTPACCReceivedDate)
        Me.Panel20.Controls.Add(Me.chbACCReceivedByAPB)
        Me.Panel20.Controls.Add(Me.btnACCSubmittals)
        Me.Panel20.Controls.Add(Me.wrnACCCorrectACC)
        Me.Panel20.Controls.Add(Me.Panel8)
        Me.Panel20.Controls.Add(Me.Label36)
        Me.Panel20.Controls.Add(Me.wrnACCRO)
        Me.Panel20.Controls.Add(Me.Panel7)
        Me.Panel20.Controls.Add(Me.Label32)
        Me.Panel20.Controls.Add(Me.Label31)
        Me.Panel20.Controls.Add(Me.DTPACCPostmarked)
        Me.Panel20.Controls.Add(Me.wrnACCPostmark)
        Me.Panel20.Controls.Add(Me.Panel6)
        Me.Panel20.Controls.Add(Me.lblAccReportingYear)
        Me.Panel20.Controls.Add(Me.Label27)
        Me.Panel20.Controls.Add(Me.wrnACCSubmittal)
        Me.Panel20.Controls.Add(Me.NUPACCSubmittal)
        Me.Panel20.Controls.Add(Me.Label67)
        Me.Panel20.Controls.Add(Me.wrnACCDatePostmarked)
        Me.Panel20.Controls.Add(Me.Label44)
        Me.Panel20.Controls.Add(Me.txtACCComments)
        Me.Panel20.Controls.Add(Me.wrnACCPreviousDeviations)
        Me.Panel20.Controls.Add(Me.Panel15)
        Me.Panel20.Controls.Add(Me.lblACCPreviouslyUnreportedDeviations)
        Me.Panel20.Controls.Add(Me.wrnACCDeviationsReported)
        Me.Panel20.Controls.Add(Me.Panel14)
        Me.Panel20.Controls.Add(Me.Label56)
        Me.Panel20.Controls.Add(Me.wrnACCResubmittalRequested)
        Me.Panel20.Controls.Add(Me.wrnACCEnforcementNeeded)
        Me.Panel20.Controls.Add(Me.pnlACCResubmittalRequested)
        Me.Panel20.Controls.Add(Me.Panel12)
        Me.Panel20.Controls.Add(Me.lblACCResubmittalRequested)
        Me.Panel20.Controls.Add(Me.Label48)
        Me.Panel20.Controls.Add(Me.wrnACCAllDeviationsReported)
        Me.Panel20.Controls.Add(Me.wrnACCCorrect)
        Me.Panel20.Controls.Add(Me.pnlACCAllDeviationsReported)
        Me.Panel20.Controls.Add(Me.Panel10)
        Me.Panel20.Controls.Add(Me.lblACCAllDeviationsReported)
        Me.Panel20.Controls.Add(Me.Label42)
        Me.Panel20.Controls.Add(Me.wrnACCConditions)
        Me.Panel20.Controls.Add(Me.Panel9)
        Me.Panel20.Controls.Add(Me.Label39)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel20.Location = New System.Drawing.Point(0, 0)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(769, 342)
        Me.Panel20.TabIndex = 0
        '
        'dtpAccReportingYear
        '
        Me.dtpAccReportingYear.CustomFormat = "yyyy"
        Me.dtpAccReportingYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAccReportingYear.Location = New System.Drawing.Point(619, 33)
        Me.dtpAccReportingYear.Name = "dtpAccReportingYear"
        Me.dtpAccReportingYear.ShowCheckBox = True
        Me.dtpAccReportingYear.ShowUpDown = True
        Me.dtpAccReportingYear.Size = New System.Drawing.Size(70, 20)
        Me.dtpAccReportingYear.TabIndex = 13
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(516, 82)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(151, 13)
        Me.Label25.TabIndex = 297
        Me.Label25.Text = "(Applies to initial submittal only)"
        '
        'DTPACCReceivedDate
        '
        Me.DTPACCReceivedDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPACCReceivedDate.Enabled = False
        Me.DTPACCReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPACCReceivedDate.Location = New System.Drawing.Point(639, 59)
        Me.DTPACCReceivedDate.Name = "DTPACCReceivedDate"
        Me.DTPACCReceivedDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPACCReceivedDate.TabIndex = 15
        Me.DTPACCReceivedDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'chbACCReceivedByAPB
        '
        Me.chbACCReceivedByAPB.AutoSize = True
        Me.chbACCReceivedByAPB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbACCReceivedByAPB.Location = New System.Drawing.Point(514, 62)
        Me.chbACCReceivedByAPB.Name = "chbACCReceivedByAPB"
        Me.chbACCReceivedByAPB.Size = New System.Drawing.Size(122, 17)
        Me.chbACCReceivedByAPB.TabIndex = 14
        Me.chbACCReceivedByAPB.Text = "Received by GEPD:"
        Me.chbACCReceivedByAPB.UseVisualStyleBackColor = True
        '
        'btnACCSubmittals
        '
        Me.btnACCSubmittals.AutoSize = True
        Me.btnACCSubmittals.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnACCSubmittals.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnACCSubmittals.ImageIndex = 53
        Me.btnACCSubmittals.Location = New System.Drawing.Point(0, 0)
        Me.btnACCSubmittals.Name = "btnACCSubmittals"
        Me.btnACCSubmittals.Size = New System.Drawing.Size(74, 19)
        Me.btnACCSubmittals.TabIndex = 17
        Me.btnACCSubmittals.Text = "Submittal History"
        '
        'wrnACCCorrectACC
        '
        Me.wrnACCCorrectACC.AutoSize = True
        Me.wrnACCCorrectACC.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCCorrectACC.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCCorrectACC.Location = New System.Drawing.Point(329, 93)
        Me.wrnACCCorrectACC.Name = "wrnACCCorrectACC"
        Me.wrnACCCorrectACC.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCCorrectACC.TabIndex = 93
        Me.wrnACCCorrectACC.Text = "Warning-value not selected"
        Me.wrnACCCorrectACC.Visible = False
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.rdbACCCorrectACCYes)
        Me.Panel8.Controls.Add(Me.rdbACCCorrectACCNo)
        Me.Panel8.Location = New System.Drawing.Point(236, 89)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(96, 16)
        Me.Panel8.TabIndex = 4
        '
        'rdbACCCorrectACCYes
        '
        Me.rdbACCCorrectACCYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCCorrectACCYes.Name = "rdbACCCorrectACCYes"
        Me.rdbACCCorrectACCYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCCorrectACCYes.TabIndex = 0
        Me.rdbACCCorrectACCYes.Text = "Yes"
        '
        'rdbACCCorrectACCNo
        '
        Me.rdbACCCorrectACCNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCCorrectACCNo.Name = "rdbACCCorrectACCNo"
        Me.rdbACCCorrectACCNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCCorrectACCNo.TabIndex = 20
        Me.rdbACCCorrectACCNo.Text = "No"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(108, 91)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(122, 13)
        Me.Label36.TabIndex = 91
        Me.Label36.Text = "Correct ACC forms used:"
        '
        'wrnACCRO
        '
        Me.wrnACCRO.AutoSize = True
        Me.wrnACCRO.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCRO.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCRO.Location = New System.Drawing.Point(329, 69)
        Me.wrnACCRO.Name = "wrnACCRO"
        Me.wrnACCRO.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCRO.TabIndex = 89
        Me.wrnACCRO.Text = "Warning-value not selected"
        Me.wrnACCRO.Visible = False
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rdbACCROYes)
        Me.Panel7.Controls.Add(Me.rdbACCRONo)
        Me.Panel7.Location = New System.Drawing.Point(236, 65)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(96, 16)
        Me.Panel7.TabIndex = 3
        '
        'rdbACCROYes
        '
        Me.rdbACCROYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCROYes.Name = "rdbACCROYes"
        Me.rdbACCROYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCROYes.TabIndex = 0
        Me.rdbACCROYes.Text = "Yes"
        '
        'rdbACCRONo
        '
        Me.rdbACCRONo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCRONo.Name = "rdbACCRONo"
        Me.rdbACCRONo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCRONo.TabIndex = 20
        Me.rdbACCRONo.Text = "No"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(84, 67)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(146, 13)
        Me.Label32.TabIndex = 87
        Me.Label32.Text = "Signed by responsible official:"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(543, 113)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(91, 13)
        Me.Label31.TabIndex = 85
        Me.Label31.Text = "Date postmarked:"
        '
        'DTPACCPostmarked
        '
        Me.DTPACCPostmarked.CustomFormat = "dd-MMM-yyyy"
        Me.DTPACCPostmarked.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPACCPostmarked.Location = New System.Drawing.Point(639, 113)
        Me.DTPACCPostmarked.Name = "DTPACCPostmarked"
        Me.DTPACCPostmarked.Size = New System.Drawing.Size(100, 20)
        Me.DTPACCPostmarked.TabIndex = 16
        Me.DTPACCPostmarked.Value = New Date(2007, 1, 25, 0, 0, 0, 0)
        '
        'wrnACCPostmark
        '
        Me.wrnACCPostmark.AutoSize = True
        Me.wrnACCPostmark.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCPostmark.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCPostmark.Location = New System.Drawing.Point(329, 45)
        Me.wrnACCPostmark.Name = "wrnACCPostmark"
        Me.wrnACCPostmark.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCPostmark.TabIndex = 83
        Me.wrnACCPostmark.Text = "Warning-value not selected"
        Me.wrnACCPostmark.Visible = False
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rdbACCPostmarkYes)
        Me.Panel6.Controls.Add(Me.rdbACCPostmarkNo)
        Me.Panel6.Location = New System.Drawing.Point(236, 41)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(96, 16)
        Me.Panel6.TabIndex = 2
        '
        'rdbACCPostmarkYes
        '
        Me.rdbACCPostmarkYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCPostmarkYes.Name = "rdbACCPostmarkYes"
        Me.rdbACCPostmarkYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCPostmarkYes.TabIndex = 0
        Me.rdbACCPostmarkYes.Text = "Yes"
        '
        'rdbACCPostmarkNo
        '
        Me.rdbACCPostmarkNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCPostmarkNo.Name = "rdbACCPostmarkNo"
        Me.rdbACCPostmarkNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCPostmarkNo.TabIndex = 20
        Me.rdbACCPostmarkNo.Text = "No"
        '
        'lblAccReportingYear
        '
        Me.lblAccReportingYear.AutoSize = True
        Me.lblAccReportingYear.Location = New System.Drawing.Point(512, 34)
        Me.lblAccReportingYear.Name = "lblAccReportingYear"
        Me.lblAccReportingYear.Size = New System.Drawing.Size(101, 13)
        Me.lblAccReportingYear.TabIndex = 81
        Me.lblAccReportingYear.Text = " ACC reporting year:"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(81, 43)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(149, 13)
        Me.Label27.TabIndex = 81
        Me.Label27.Text = " ACC postmarked by deadline:"
        '
        'wrnACCSubmittal
        '
        Me.wrnACCSubmittal.AutoSize = True
        Me.wrnACCSubmittal.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCSubmittal.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCSubmittal.Location = New System.Drawing.Point(148, 24)
        Me.wrnACCSubmittal.Name = "wrnACCSubmittal"
        Me.wrnACCSubmittal.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCSubmittal.TabIndex = 79
        Me.wrnACCSubmittal.Text = "Warning-value not selected"
        Me.wrnACCSubmittal.Visible = False
        '
        'NUPACCSubmittal
        '
        Me.NUPACCSubmittal.Location = New System.Drawing.Point(102, 20)
        Me.NUPACCSubmittal.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NUPACCSubmittal.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUPACCSubmittal.Name = "NUPACCSubmittal"
        Me.NUPACCSubmittal.Size = New System.Drawing.Size(40, 20)
        Me.NUPACCSubmittal.TabIndex = 0
        Me.NUPACCSubmittal.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(6, 22)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(90, 13)
        Me.Label67.TabIndex = 77
        Me.Label67.Text = "Submittal Number"
        '
        'wrnACCDatePostmarked
        '
        Me.wrnACCDatePostmarked.AutoSize = True
        Me.wrnACCDatePostmarked.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCDatePostmarked.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCDatePostmarked.Location = New System.Drawing.Point(639, 137)
        Me.wrnACCDatePostmarked.Name = "wrnACCDatePostmarked"
        Me.wrnACCDatePostmarked.Size = New System.Drawing.Size(96, 13)
        Me.wrnACCDatePostmarked.TabIndex = 137
        Me.wrnACCDatePostmarked.Text = "Warning-Date Error"
        Me.wrnACCDatePostmarked.Visible = False
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(15, 275)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(59, 13)
        Me.Label44.TabIndex = 133
        Me.Label44.Text = "Comments:"
        '
        'txtACCComments
        '
        Me.txtACCComments.AcceptsReturn = True
        Me.txtACCComments.Location = New System.Drawing.Point(80, 272)
        Me.txtACCComments.MaxLength = 4000
        Me.txtACCComments.Multiline = True
        Me.txtACCComments.Name = "txtACCComments"
        Me.txtACCComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtACCComments.Size = New System.Drawing.Size(384, 80)
        Me.txtACCComments.TabIndex = 12
        '
        'wrnACCPreviousDeviations
        '
        Me.wrnACCPreviousDeviations.AutoSize = True
        Me.wrnACCPreviousDeviations.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCPreviousDeviations.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCPreviousDeviations.Location = New System.Drawing.Point(329, 188)
        Me.wrnACCPreviousDeviations.Name = "wrnACCPreviousDeviations"
        Me.wrnACCPreviousDeviations.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCPreviousDeviations.TabIndex = 131
        Me.wrnACCPreviousDeviations.Text = "Warning-value not selected"
        Me.wrnACCPreviousDeviations.Visible = False
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.rdbACCPreviouslyUnreportedDeviationsYes)
        Me.Panel15.Controls.Add(Me.rdbACCPreviouslyUnreportedDeviationsNo)
        Me.Panel15.Location = New System.Drawing.Point(236, 184)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(96, 16)
        Me.Panel15.TabIndex = 8
        '
        'rdbACCPreviouslyUnreportedDeviationsYes
        '
        Me.rdbACCPreviouslyUnreportedDeviationsYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCPreviouslyUnreportedDeviationsYes.Name = "rdbACCPreviouslyUnreportedDeviationsYes"
        Me.rdbACCPreviouslyUnreportedDeviationsYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCPreviouslyUnreportedDeviationsYes.TabIndex = 0
        Me.rdbACCPreviouslyUnreportedDeviationsYes.Text = "Yes"
        '
        'rdbACCPreviouslyUnreportedDeviationsNo
        '
        Me.rdbACCPreviouslyUnreportedDeviationsNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCPreviouslyUnreportedDeviationsNo.Name = "rdbACCPreviouslyUnreportedDeviationsNo"
        Me.rdbACCPreviouslyUnreportedDeviationsNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCPreviouslyUnreportedDeviationsNo.TabIndex = 20
        Me.rdbACCPreviouslyUnreportedDeviationsNo.Text = "No"
        '
        'lblACCPreviouslyUnreportedDeviations
        '
        Me.lblACCPreviouslyUnreportedDeviations.AutoSize = True
        Me.lblACCPreviouslyUnreportedDeviations.Location = New System.Drawing.Point(15, 186)
        Me.lblACCPreviouslyUnreportedDeviations.Name = "lblACCPreviouslyUnreportedDeviations"
        Me.lblACCPreviouslyUnreportedDeviations.Size = New System.Drawing.Size(215, 13)
        Me.lblACCPreviouslyUnreportedDeviations.TabIndex = 129
        Me.lblACCPreviouslyUnreportedDeviations.Text = "Reported deviations not previously reported:"
        '
        'wrnACCDeviationsReported
        '
        Me.wrnACCDeviationsReported.AutoSize = True
        Me.wrnACCDeviationsReported.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCDeviationsReported.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCDeviationsReported.Location = New System.Drawing.Point(329, 164)
        Me.wrnACCDeviationsReported.Name = "wrnACCDeviationsReported"
        Me.wrnACCDeviationsReported.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCDeviationsReported.TabIndex = 127
        Me.wrnACCDeviationsReported.Text = "Warning-value not selected"
        Me.wrnACCDeviationsReported.Visible = False
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.rdbACCDeviationsReportedYes)
        Me.Panel14.Controls.Add(Me.rdbACCDeviationsReportedNo)
        Me.Panel14.Location = New System.Drawing.Point(236, 160)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(96, 16)
        Me.Panel14.TabIndex = 7
        '
        'rdbACCDeviationsReportedYes
        '
        Me.rdbACCDeviationsReportedYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCDeviationsReportedYes.Name = "rdbACCDeviationsReportedYes"
        Me.rdbACCDeviationsReportedYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCDeviationsReportedYes.TabIndex = 0
        Me.rdbACCDeviationsReportedYes.Text = "Yes"
        '
        'rdbACCDeviationsReportedNo
        '
        Me.rdbACCDeviationsReportedNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCDeviationsReportedNo.Name = "rdbACCDeviationsReportedNo"
        Me.rdbACCDeviationsReportedNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCDeviationsReportedNo.TabIndex = 20
        Me.rdbACCDeviationsReportedNo.Text = "No"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(125, 162)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(105, 13)
        Me.Label56.TabIndex = 125
        Me.Label56.Text = "Reported deviations:"
        '
        'wrnACCResubmittalRequested
        '
        Me.wrnACCResubmittalRequested.AutoSize = True
        Me.wrnACCResubmittalRequested.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCResubmittalRequested.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCResubmittalRequested.Location = New System.Drawing.Point(406, 234)
        Me.wrnACCResubmittalRequested.Name = "wrnACCResubmittalRequested"
        Me.wrnACCResubmittalRequested.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCResubmittalRequested.TabIndex = 111
        Me.wrnACCResubmittalRequested.Text = "Warning-value not selected"
        Me.wrnACCResubmittalRequested.Visible = False
        '
        'wrnACCEnforcementNeeded
        '
        Me.wrnACCEnforcementNeeded.AutoSize = True
        Me.wrnACCEnforcementNeeded.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCEnforcementNeeded.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCEnforcementNeeded.Location = New System.Drawing.Point(329, 258)
        Me.wrnACCEnforcementNeeded.Name = "wrnACCEnforcementNeeded"
        Me.wrnACCEnforcementNeeded.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCEnforcementNeeded.TabIndex = 111
        Me.wrnACCEnforcementNeeded.Text = "Warning-value not selected"
        Me.wrnACCEnforcementNeeded.Visible = False
        '
        'pnlACCResubmittalRequested
        '
        Me.pnlACCResubmittalRequested.AutoSize = True
        Me.pnlACCResubmittalRequested.Controls.Add(Me.rdbACCResubmittalRequestedYes)
        Me.pnlACCResubmittalRequested.Controls.Add(Me.rdbACCResubmittalRequestedUnknown)
        Me.pnlACCResubmittalRequested.Controls.Add(Me.rdbACCResubmittalRequestedNo)
        Me.pnlACCResubmittalRequested.Location = New System.Drawing.Point(236, 232)
        Me.pnlACCResubmittalRequested.Name = "pnlACCResubmittalRequested"
        Me.pnlACCResubmittalRequested.Size = New System.Drawing.Size(164, 20)
        Me.pnlACCResubmittalRequested.TabIndex = 10
        '
        'rdbACCResubmittalRequestedYes
        '
        Me.rdbACCResubmittalRequestedYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCResubmittalRequestedYes.Name = "rdbACCResubmittalRequestedYes"
        Me.rdbACCResubmittalRequestedYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCResubmittalRequestedYes.TabIndex = 0
        Me.rdbACCResubmittalRequestedYes.Text = "Yes"
        '
        'rdbACCResubmittalRequestedUnknown
        '
        Me.rdbACCResubmittalRequestedUnknown.AutoSize = True
        Me.rdbACCResubmittalRequestedUnknown.Location = New System.Drawing.Point(90, 0)
        Me.rdbACCResubmittalRequestedUnknown.Name = "rdbACCResubmittalRequestedUnknown"
        Me.rdbACCResubmittalRequestedUnknown.Size = New System.Drawing.Size(71, 17)
        Me.rdbACCResubmittalRequestedUnknown.TabIndex = 30
        Me.rdbACCResubmittalRequestedUnknown.Text = "Unknown"
        Me.rdbACCResubmittalRequestedUnknown.Visible = False
        '
        'rdbACCResubmittalRequestedNo
        '
        Me.rdbACCResubmittalRequestedNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCResubmittalRequestedNo.Name = "rdbACCResubmittalRequestedNo"
        Me.rdbACCResubmittalRequestedNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCResubmittalRequestedNo.TabIndex = 20
        Me.rdbACCResubmittalRequestedNo.Text = "No"
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.rdbACCEnforcementNeededYes)
        Me.Panel12.Controls.Add(Me.rdbACCEnforcementNeededNo)
        Me.Panel12.Location = New System.Drawing.Point(236, 256)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(96, 16)
        Me.Panel12.TabIndex = 11
        '
        'rdbACCEnforcementNeededYes
        '
        Me.rdbACCEnforcementNeededYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCEnforcementNeededYes.Name = "rdbACCEnforcementNeededYes"
        Me.rdbACCEnforcementNeededYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCEnforcementNeededYes.TabIndex = 0
        Me.rdbACCEnforcementNeededYes.Text = "Yes"
        '
        'rdbACCEnforcementNeededNo
        '
        Me.rdbACCEnforcementNeededNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCEnforcementNeededNo.Name = "rdbACCEnforcementNeededNo"
        Me.rdbACCEnforcementNeededNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCEnforcementNeededNo.TabIndex = 20
        Me.rdbACCEnforcementNeededNo.Text = "No"
        '
        'lblACCResubmittalRequested
        '
        Me.lblACCResubmittalRequested.AutoSize = True
        Me.lblACCResubmittalRequested.Location = New System.Drawing.Point(124, 234)
        Me.lblACCResubmittalRequested.Name = "lblACCResubmittalRequested"
        Me.lblACCResubmittalRequested.Size = New System.Drawing.Size(106, 13)
        Me.lblACCResubmittalRequested.TabIndex = 109
        Me.lblACCResubmittalRequested.Text = "Resubmittal required:"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(121, 258)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(109, 13)
        Me.Label48.TabIndex = 109
        Me.Label48.Text = "Enforcement needed:"
        '
        'wrnACCAllDeviationsReported
        '
        Me.wrnACCAllDeviationsReported.AutoSize = True
        Me.wrnACCAllDeviationsReported.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCAllDeviationsReported.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCAllDeviationsReported.Location = New System.Drawing.Point(406, 211)
        Me.wrnACCAllDeviationsReported.Name = "wrnACCAllDeviationsReported"
        Me.wrnACCAllDeviationsReported.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCAllDeviationsReported.TabIndex = 101
        Me.wrnACCAllDeviationsReported.Text = "Warning-value not selected"
        Me.wrnACCAllDeviationsReported.Visible = False
        '
        'wrnACCCorrect
        '
        Me.wrnACCCorrect.AutoSize = True
        Me.wrnACCCorrect.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCCorrect.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCCorrect.Location = New System.Drawing.Point(329, 141)
        Me.wrnACCCorrect.Name = "wrnACCCorrect"
        Me.wrnACCCorrect.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCCorrect.TabIndex = 101
        Me.wrnACCCorrect.Text = "Warning-value not selected"
        Me.wrnACCCorrect.Visible = False
        '
        'pnlACCAllDeviationsReported
        '
        Me.pnlACCAllDeviationsReported.AutoSize = True
        Me.pnlACCAllDeviationsReported.Controls.Add(Me.rdbACCAllDeviationsReportedYes)
        Me.pnlACCAllDeviationsReported.Controls.Add(Me.rdbACCAllDeviationsReportedUnknown)
        Me.pnlACCAllDeviationsReported.Controls.Add(Me.rdbACCAllDeviationsReportedNo)
        Me.pnlACCAllDeviationsReported.Location = New System.Drawing.Point(236, 207)
        Me.pnlACCAllDeviationsReported.Name = "pnlACCAllDeviationsReported"
        Me.pnlACCAllDeviationsReported.Size = New System.Drawing.Size(164, 24)
        Me.pnlACCAllDeviationsReported.TabIndex = 9
        '
        'rdbACCAllDeviationsReportedYes
        '
        Me.rdbACCAllDeviationsReportedYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCAllDeviationsReportedYes.Name = "rdbACCAllDeviationsReportedYes"
        Me.rdbACCAllDeviationsReportedYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCAllDeviationsReportedYes.TabIndex = 0
        Me.rdbACCAllDeviationsReportedYes.Text = "Yes"
        '
        'rdbACCAllDeviationsReportedUnknown
        '
        Me.rdbACCAllDeviationsReportedUnknown.AutoSize = True
        Me.rdbACCAllDeviationsReportedUnknown.Location = New System.Drawing.Point(90, 0)
        Me.rdbACCAllDeviationsReportedUnknown.Name = "rdbACCAllDeviationsReportedUnknown"
        Me.rdbACCAllDeviationsReportedUnknown.Size = New System.Drawing.Size(71, 17)
        Me.rdbACCAllDeviationsReportedUnknown.TabIndex = 30
        Me.rdbACCAllDeviationsReportedUnknown.Text = "Unknown"
        Me.rdbACCAllDeviationsReportedUnknown.Visible = False
        '
        'rdbACCAllDeviationsReportedNo
        '
        Me.rdbACCAllDeviationsReportedNo.AutoSize = True
        Me.rdbACCAllDeviationsReportedNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCAllDeviationsReportedNo.Name = "rdbACCAllDeviationsReportedNo"
        Me.rdbACCAllDeviationsReportedNo.Size = New System.Drawing.Size(39, 17)
        Me.rdbACCAllDeviationsReportedNo.TabIndex = 20
        Me.rdbACCAllDeviationsReportedNo.Text = "No"
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.rdbACCCorrectYes)
        Me.Panel10.Controls.Add(Me.rdbACCCorrectNo)
        Me.Panel10.Location = New System.Drawing.Point(236, 137)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(96, 16)
        Me.Panel10.TabIndex = 6
        '
        'rdbACCCorrectYes
        '
        Me.rdbACCCorrectYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCCorrectYes.Name = "rdbACCCorrectYes"
        Me.rdbACCCorrectYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCCorrectYes.TabIndex = 0
        Me.rdbACCCorrectYes.Text = "Yes"
        '
        'rdbACCCorrectNo
        '
        Me.rdbACCCorrectNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCCorrectNo.Name = "rdbACCCorrectNo"
        Me.rdbACCCorrectNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCCorrectNo.TabIndex = 20
        Me.rdbACCCorrectNo.Text = "No"
        '
        'lblACCAllDeviationsReported
        '
        Me.lblACCAllDeviationsReported.AutoSize = True
        Me.lblACCAllDeviationsReported.Location = New System.Drawing.Point(81, 209)
        Me.lblACCAllDeviationsReported.Name = "lblACCAllDeviationsReported"
        Me.lblACCAllDeviationsReported.Size = New System.Drawing.Size(149, 13)
        Me.lblACCAllDeviationsReported.TabIndex = 99
        Me.lblACCAllDeviationsReported.Text = "All known deviations reported:"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(114, 139)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(116, 13)
        Me.Label42.TabIndex = 99
        Me.Label42.Text = "ACC correctly filled out:"
        '
        'wrnACCConditions
        '
        Me.wrnACCConditions.AutoSize = True
        Me.wrnACCConditions.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.wrnACCConditions.ForeColor = System.Drawing.Color.Tomato
        Me.wrnACCConditions.Location = New System.Drawing.Point(329, 117)
        Me.wrnACCConditions.Name = "wrnACCConditions"
        Me.wrnACCConditions.Size = New System.Drawing.Size(135, 13)
        Me.wrnACCConditions.TabIndex = 97
        Me.wrnACCConditions.Text = "Warning-value not selected"
        Me.wrnACCConditions.Visible = False
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.rdbACCConditionsYes)
        Me.Panel9.Controls.Add(Me.rdbACCConditionsNo)
        Me.Panel9.Location = New System.Drawing.Point(236, 113)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(96, 16)
        Me.Panel9.TabIndex = 5
        '
        'rdbACCConditionsYes
        '
        Me.rdbACCConditionsYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbACCConditionsYes.Name = "rdbACCConditionsYes"
        Me.rdbACCConditionsYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCConditionsYes.TabIndex = 0
        Me.rdbACCConditionsYes.Text = "Yes"
        '
        'rdbACCConditionsNo
        '
        Me.rdbACCConditionsNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbACCConditionsNo.Name = "rdbACCConditionsNo"
        Me.rdbACCConditionsNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbACCConditionsNo.TabIndex = 20
        Me.rdbACCConditionsNo.Text = "No"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(98, 115)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(132, 13)
        Me.Label39.TabIndex = 95
        Me.Label39.Text = "All Title V conditions listed:"
        '
        'SplitterACC
        '
        Me.SplitterACC.BackColor = System.Drawing.SystemColors.Highlight
        Me.SplitterACC.Location = New System.Drawing.Point(10, 0)
        Me.SplitterACC.Name = "SplitterACC"
        Me.SplitterACC.Size = New System.Drawing.Size(5, 342)
        Me.SplitterACC.TabIndex = 78
        Me.SplitterACC.TabStop = False
        '
        'DGRACCResubmittal
        '
        Me.DGRACCResubmittal.DataMember = ""
        Me.DGRACCResubmittal.Dock = System.Windows.Forms.DockStyle.Left
        Me.DGRACCResubmittal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGRACCResubmittal.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGRACCResubmittal.Location = New System.Drawing.Point(0, 0)
        Me.DGRACCResubmittal.Name = "DGRACCResubmittal"
        Me.DGRACCResubmittal.ReadOnly = True
        Me.DGRACCResubmittal.RowHeadersVisible = False
        Me.DGRACCResubmittal.Size = New System.Drawing.Size(10, 342)
        Me.DGRACCResubmittal.TabIndex = 18
        Me.DGRACCResubmittal.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(192, 144)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(152, 32)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(100, 20)
        Me.TextBox6.TabIndex = 1
        Me.TextBox6.Text = "TextBox6"
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(200, 350)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.SystemColors.Control
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Enabled = False
        Me.Splitter3.Location = New System.Drawing.Point(0, 194)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(792, 5)
        Me.Splitter3.TabIndex = 230
        Me.Splitter3.TabStop = False
        '
        'PanelSSCPCompliance
        '
        Me.PanelSSCPCompliance.Controls.Add(Me.TCItems)
        Me.PanelSSCPCompliance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSSCPCompliance.Location = New System.Drawing.Point(0, 199)
        Me.PanelSSCPCompliance.Name = "PanelSSCPCompliance"
        Me.PanelSSCPCompliance.Size = New System.Drawing.Size(792, 396)
        Me.PanelSSCPCompliance.TabIndex = 231
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip1.TabIndex = 232
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.Iaip.My.Resources.Resources.SaveIcon
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "Save"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.Iaip.My.Resources.Resources.PrintPreviewIcon
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "Print"
        '
        'SSCPEvents
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(792, 595)
        Me.Controls.Add(Me.PanelSSCPCompliance)
        Me.Controls.Add(Me.Splitter3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPEvents"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Compliance Events"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TCItems.ResumeLayout(False)
        Me.TPReport.ResumeLayout(False)
        Me.PanelReports.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.NUPReportSubmittal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        CType(Me.dgrReportResubmittal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPTestReports.ResumeLayout(False)
        Me.PanelSSCPCompliance2.ResumeLayout(False)
        Me.PanelSSCPCompliance2.PerformLayout()
        Me.Panel22.ResumeLayout(False)
        Me.TPInspection.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.TPNotifications.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.Panel23.ResumeLayout(False)
        Me.TPACC.ResumeLayout(False)
        Me.PanelACC.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.NUPACCSubmittal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel15.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.pnlACCResubmittalRequested.ResumeLayout(False)
        Me.pnlACCResubmittalRequested.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.pnlACCAllDeviationsReported.ResumeLayout(False)
        Me.pnlACCAllDeviationsReported.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        CType(Me.DGRACCResubmittal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSSCPCompliance.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnSave As ToolStripButton
    Friend WithEvents btnPrint As ToolStripButton
End Class
