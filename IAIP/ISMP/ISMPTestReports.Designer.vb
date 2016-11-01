<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPTestReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPTestReports))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiView = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiOpenMemo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiTool = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiOpenTestLogNotification = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiOpenExcelFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiPrePopulate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiDefaultCompliance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiPrintNonConf = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbPrePopulate = New System.Windows.Forms.ToolStripButton()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsbClear = New System.Windows.Forms.ToolStripButton()
        Me.tsbResize = New System.Windows.Forms.ToolStripButton()
        Me.tsbMemo = New System.Windows.Forms.ToolStripButton()
        Me.tsbTestLogLink = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.tsbConfidentialData = New System.Windows.Forms.ToolStripButton()
        Me.SCTestReports = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblMemoEntered = New System.Windows.Forms.Label()
        Me.lblPreComplianceStatus = New System.Windows.Forms.Label()
        Me.txtDaysfromTestToAPB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.clbWitnessingEngineers = New System.Windows.Forms.CheckedListBox()
        Me.llbTestNotifiactionNumber = New System.Windows.Forms.LinkLabel()
        Me.cboTestNotificationNumber = New System.Windows.Forms.ComboBox()
        Me.labTestNotificationNumber = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtAssignedToEngineer = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cboComplianceStatus = New System.Windows.Forms.ComboBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDaysInAPB = New System.Windows.Forms.TextBox()
        Me.cboccBox = New System.Windows.Forms.ComboBox()
        Me.txtDaysAssigned = New System.Windows.Forms.TextBox()
        Me.Label297 = New System.Windows.Forms.Label()
        Me.cboTestingFirm = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cboPollutantDetermined = New System.Windows.Forms.ComboBox()
        Me.cboReportType = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cboComplianceManager = New System.Windows.Forms.ComboBox()
        Me.DTPTestDateComplete = New System.Windows.Forms.DateTimePicker()
        Me.DTPTestDateStart = New System.Windows.Forms.DateTimePicker()
        Me.cboReviewingEngineer = New System.Windows.Forms.ComboBox()
        Me.cboUnitManager = New System.Windows.Forms.ComboBox()
        Me.cboISMPUnit = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtProgramManager = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboMethodDetermined = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCompleteDate = New System.Windows.Forms.TextBox()
        Me.txtReceivedByAPB = New System.Windows.Forms.TextBox()
        Me.cboWitnessingEngineer = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtFacilityState = New System.Windows.Forms.TextBox()
        Me.txtSourceTested = New System.Windows.Forms.TextBox()
        Me.txtFacilityCity = New System.Windows.Forms.TextBox()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.txtAirsNumber = New System.Windows.Forms.TextBox()
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox()
        Me.lblDatesTested = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.labReferenceNumber = New System.Windows.Forms.Label()
        Me.TCDocumentTypes = New System.Windows.Forms.TabControl()
        Me.TPOneStack = New System.Windows.Forms.TabPage()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtOtherInformationOneStack = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TCOneStack = New System.Windows.Forms.TabControl()
        Me.TPOneStackTwoRun = New System.Windows.Forms.TabPage()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.Label158 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtEmissRateAvgOneStackTwoRun = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgOneStackTwoRun = New System.Windows.Forms.TextBox()
        Me.cboEmissRateUnitOneStackTwoRun = New System.Windows.Forms.ComboBox()
        Me.cboPollConUnitOneStackTwoRun = New System.Windows.Forms.ComboBox()
        Me.txtEmissRateOneStackTwoRun1B = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackTwoRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackTwoRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackTwoRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackTwoRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackTwoRun1B = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackTwoRun1B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateOneStackTwoRun1A = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackTwoRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackTwoRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackTwoRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackTwoRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackTwoRun1A = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackTwoRun1A = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.btnClearOneStackTwoRun2 = New System.Windows.Forms.Button()
        Me.btnClearOneStackTwoRun1 = New System.Windows.Forms.Button()
        Me.TPOneStackThreeRun = New System.Windows.Forms.TabPage()
        Me.Label206 = New System.Windows.Forms.Label()
        Me.Label205 = New System.Windows.Forms.Label()
        Me.Label204 = New System.Windows.Forms.Label()
        Me.Label203 = New System.Windows.Forms.Label()
        Me.Label202 = New System.Windows.Forms.Label()
        Me.Label201 = New System.Windows.Forms.Label()
        Me.Label200 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtEmissRateAvgOneStackThreeRun = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgOneStackThreeRun = New System.Windows.Forms.TextBox()
        Me.txtEmissRateOneStackThreeRun1C = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackThreeRun1C = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackThreeRun1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackThreeRun1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackThreeRun1C = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackThreeRun1C = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackThreeRun1C = New System.Windows.Forms.TextBox()
        Me.txtEmissRateOneStackThreeRun1B = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackThreeRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackThreeRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackThreeRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackThreeRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackThreeRun1B = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackThreeRun1B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateOneStackThreeRun1A = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackThreeRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackThreeRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackThreeRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackThreeRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackThreeRun1A = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackThreeRun1A = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cboEmissRateUnitOneStackThreeRun = New System.Windows.Forms.ComboBox()
        Me.cboPollConUnitOneStackThreeRun = New System.Windows.Forms.ComboBox()
        Me.btnClearOneStackThreeRun3 = New System.Windows.Forms.Button()
        Me.btnClearOneStackThreeRun2 = New System.Windows.Forms.Button()
        Me.btnClearOneStackThreeRun1 = New System.Windows.Forms.Button()
        Me.TPOneStackFourRun = New System.Windows.Forms.TabPage()
        Me.Label213 = New System.Windows.Forms.Label()
        Me.Label212 = New System.Windows.Forms.Label()
        Me.Label211 = New System.Windows.Forms.Label()
        Me.Label210 = New System.Windows.Forms.Label()
        Me.Label209 = New System.Windows.Forms.Label()
        Me.Label208 = New System.Windows.Forms.Label()
        Me.Label207 = New System.Windows.Forms.Label()
        Me.txtEmissRateOneStackFourRun1D = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackFourRun1D = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackFourRun1D = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackFourRun1D = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackFourRun1D = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackFourRun1D = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackFourRun1D = New System.Windows.Forms.TextBox()
        Me.btnClearOneStackFourRun4 = New System.Windows.Forms.Button()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtEmissRateAvgOneStackFourRun = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgOneStackFourRun = New System.Windows.Forms.TextBox()
        Me.txtEmissRateOneStackFourRun1C = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackFourRun1C = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackFourRun1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackFourRun1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackFourRun1C = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackFourRun1C = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackFourRun1C = New System.Windows.Forms.TextBox()
        Me.txtEmissRateOneStackFourRun1B = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackFourRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackFourRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackFourRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackFourRun1B = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackFourRun1B = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackFourRun1B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateOneStackFourRun1A = New System.Windows.Forms.TextBox()
        Me.txtPollConcOneStackFourRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasMoistOneStackFourRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMOneStackFourRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMOneStackFourRun1A = New System.Windows.Forms.TextBox()
        Me.txtGasTempOneStackFourRun1A = New System.Windows.Forms.TextBox()
        Me.txtRunNumOneStackFourRun1A = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.cboEmissRateUnitOneStackFourRun = New System.Windows.Forms.ComboBox()
        Me.cboPollConUnitOneStackFourRun = New System.Windows.Forms.ComboBox()
        Me.btnClearOneStackFourRun3 = New System.Windows.Forms.Button()
        Me.btnClearOneStackFourRun2 = New System.Windows.Forms.Button()
        Me.btnClearOneStackFourRun1 = New System.Windows.Forms.Button()
        Me.txtControlEquipmentOperatingDataOneStack = New System.Windows.Forms.TextBox()
        Me.txtApplicableRegulationOneStack = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityOneStack = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRate2OneStack = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate3OneStack = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate1OneStack = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityOneStack = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.cboOperatingCapacityUnitsOneStack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits2OneStack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits3OneStack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits1OneStack = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsOneStack = New System.Windows.Forms.ComboBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtPercentAllowableOneStack = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label193 = New System.Windows.Forms.Label()
        Me.Label197 = New System.Windows.Forms.Label()
        Me.Label198 = New System.Windows.Forms.Label()
        Me.Label199 = New System.Windows.Forms.Label()
        Me.TPLoadingRack = New System.Windows.Forms.TabPage()
        Me.Label292 = New System.Windows.Forms.Label()
        Me.Label291 = New System.Windows.Forms.Label()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.Label290 = New System.Windows.Forms.Label()
        Me.Label289 = New System.Windows.Forms.Label()
        Me.Label288 = New System.Windows.Forms.Label()
        Me.Label287 = New System.Windows.Forms.Label()
        Me.txtDestructionEfficiencyLoadingRack = New System.Windows.Forms.TextBox()
        Me.Label171 = New System.Windows.Forms.Label()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.cboEmissRateUnitLoadingRack = New System.Windows.Forms.ComboBox()
        Me.cboPollConUnitOUTLoadingRack = New System.Windows.Forms.ComboBox()
        Me.txtOtherInformationLoadingRack = New System.Windows.Forms.TextBox()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.txtEmissRateLoadingRack = New System.Windows.Forms.TextBox()
        Me.txtTestDurationLoadingRack = New System.Windows.Forms.TextBox()
        Me.txtPollConcOUTLoadingRack = New System.Windows.Forms.TextBox()
        Me.txtPollConcINLoadingRack = New System.Windows.Forms.TextBox()
        Me.Label156 = New System.Windows.Forms.Label()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.cboPollConUnitINLoadingRack = New System.Windows.Forms.ComboBox()
        Me.cboTestDurationUnitsLoadingRack = New System.Windows.Forms.ComboBox()
        Me.txtControlEquipmentOperatingDataLoadingRack = New System.Windows.Forms.TextBox()
        Me.txtApplicableRegulationLoadingRack = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityLoadingRack = New System.Windows.Forms.TextBox()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRate2LoadingRack = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate3LoadingRack = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate1LoadingRack = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityLoadingRack = New System.Windows.Forms.TextBox()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.cboOperatingCapacityUnitsLoadingRack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits2LoadingRack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits3LoadingRack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits1LoadingRack = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack = New System.Windows.Forms.ComboBox()
        Me.Label169 = New System.Windows.Forms.Label()
        Me.Label170 = New System.Windows.Forms.Label()
        Me.TPPondTreatment = New System.Windows.Forms.TabPage()
        Me.Label286 = New System.Windows.Forms.Label()
        Me.Label285 = New System.Windows.Forms.Label()
        Me.Label284 = New System.Windows.Forms.Label()
        Me.Label283 = New System.Windows.Forms.Label()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.Label282 = New System.Windows.Forms.Label()
        Me.Label281 = New System.Windows.Forms.Label()
        Me.Label280 = New System.Windows.Forms.Label()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.txtOtherInformationPond = New System.Windows.Forms.TextBox()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.txtDestructionEfficancyPond = New System.Windows.Forms.TextBox()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.txtTreatmentRateAvgPond = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgPond = New System.Windows.Forms.TextBox()
        Me.txtTreatmentRatePond1C = New System.Windows.Forms.TextBox()
        Me.txtPollConcPond1C = New System.Windows.Forms.TextBox()
        Me.txtRunNumPond1C = New System.Windows.Forms.TextBox()
        Me.txtTreatmentRatePond1B = New System.Windows.Forms.TextBox()
        Me.txtPollConcPond1B = New System.Windows.Forms.TextBox()
        Me.txtRunNumPond1B = New System.Windows.Forms.TextBox()
        Me.txtTreatmentRatePond1A = New System.Windows.Forms.TextBox()
        Me.txtPollConcPond1A = New System.Windows.Forms.TextBox()
        Me.txtRunNumPond1A = New System.Windows.Forms.TextBox()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.cboTreatmentRateUnitPond = New System.Windows.Forms.ComboBox()
        Me.cboPollConUnitPond = New System.Windows.Forms.ComboBox()
        Me.btnClearPondTreatment3 = New System.Windows.Forms.Button()
        Me.btnClearPondTreatment2 = New System.Windows.Forms.Button()
        Me.btnClearPondTreatment1 = New System.Windows.Forms.Button()
        Me.txtControlEquipmentOperatingDataPond = New System.Windows.Forms.TextBox()
        Me.txtApplicableRegulationPond = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityPond = New System.Windows.Forms.TextBox()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRate2Pond = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate3Pond = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate1Pond = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityPond = New System.Windows.Forms.TextBox()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.cboOperatingCapacityUnitsPond = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits2Pond = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits3Pond = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits1Pond = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsPond = New System.Windows.Forms.ComboBox()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.TPGasConcentration = New System.Windows.Forms.TabPage()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label279 = New System.Windows.Forms.Label()
        Me.Label278 = New System.Windows.Forms.Label()
        Me.Label277 = New System.Windows.Forms.Label()
        Me.Label276 = New System.Windows.Forms.Label()
        Me.Label275 = New System.Windows.Forms.Label()
        Me.Label274 = New System.Windows.Forms.Label()
        Me.Label273 = New System.Windows.Forms.Label()
        Me.txtOtherInformationGas = New System.Windows.Forms.TextBox()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.txtPercentAllowableGas = New System.Windows.Forms.TextBox()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.txtEmissRateAvgGas = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgGas = New System.Windows.Forms.TextBox()
        Me.txtEmissRateGas1C = New System.Windows.Forms.TextBox()
        Me.txtPollConcGas1C = New System.Windows.Forms.TextBox()
        Me.txtRunNumGas1C = New System.Windows.Forms.TextBox()
        Me.txtEmissRateGas1B = New System.Windows.Forms.TextBox()
        Me.txtPollConcGas1B = New System.Windows.Forms.TextBox()
        Me.txtRunNumGas1B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateGas1A = New System.Windows.Forms.TextBox()
        Me.txtPollConcGas1A = New System.Windows.Forms.TextBox()
        Me.txtRunNumGas1A = New System.Windows.Forms.TextBox()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.cboEmissRateUnitGas = New System.Windows.Forms.ComboBox()
        Me.cboPollConUnitGas = New System.Windows.Forms.ComboBox()
        Me.btnClearGasConcentration3 = New System.Windows.Forms.Button()
        Me.btnClearGasConcentration2 = New System.Windows.Forms.Button()
        Me.btnClearGasConcentration1 = New System.Windows.Forms.Button()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.txtControlEquipmentOperatingDataGas = New System.Windows.Forms.TextBox()
        Me.txtApplicableRegulationGas = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityGas = New System.Windows.Forms.TextBox()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRate2Gas = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate3Gas = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate1Gas = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityGas = New System.Windows.Forms.TextBox()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.cboOperatingCapacityUnitsGas = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits2Gas = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits3Gas = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits1Gas = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsGas = New System.Windows.Forms.ComboBox()
        Me.Label132 = New System.Windows.Forms.Label()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.TPFlare = New System.Windows.Forms.TabPage()
        Me.Label272 = New System.Windows.Forms.Label()
        Me.Label271 = New System.Windows.Forms.Label()
        Me.Label270 = New System.Windows.Forms.Label()
        Me.Label269 = New System.Windows.Forms.Label()
        Me.Label268 = New System.Windows.Forms.Label()
        Me.Label267 = New System.Windows.Forms.Label()
        Me.txtOtherInformationFlare = New System.Windows.Forms.TextBox()
        Me.txtPercentAllowableFlare = New System.Windows.Forms.TextBox()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.txtHeatingValuesAvgFlare = New System.Windows.Forms.TextBox()
        Me.txtVelocityAvgFlare = New System.Windows.Forms.TextBox()
        Me.txtVelocity1BFlare = New System.Windows.Forms.TextBox()
        Me.cboHeatingValueUnits = New System.Windows.Forms.ComboBox()
        Me.cboVelocityUnitsFlare = New System.Windows.Forms.ComboBox()
        Me.txtHeatingValue1AFlare = New System.Windows.Forms.TextBox()
        Me.txtHeatingValue1BFlare = New System.Windows.Forms.TextBox()
        Me.txtHeatingValue1CFlare = New System.Windows.Forms.TextBox()
        Me.txtVelocity1AFlare = New System.Windows.Forms.TextBox()
        Me.txtVelocity1CFlare = New System.Windows.Forms.TextBox()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.txtHeatContentFlare = New System.Windows.Forms.TextBox()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.txtMonitoringDataFlare = New System.Windows.Forms.TextBox()
        Me.txtApplicableRegulationFlare = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityFlare = New System.Windows.Forms.TextBox()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.txtVelocityFlare = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityFlare = New System.Windows.Forms.TextBox()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.cboOperatingCapacityUnitsFlare = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsFlare = New System.Windows.Forms.ComboBox()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.TPMethodNine = New System.Windows.Forms.TabPage()
        Me.txtOtherInformationMethod9 = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.TCMethodNine = New System.Windows.Forms.TabControl()
        Me.TPMethodNineSingle = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rdbMethod9Average30 = New System.Windows.Forms.RadioButton()
        Me.rdbMethod9HighestAvg = New System.Windows.Forms.RadioButton()
        Me.Label256 = New System.Windows.Forms.Label()
        Me.Label255 = New System.Windows.Forms.Label()
        Me.Label254 = New System.Windows.Forms.Label()
        Me.Label253 = New System.Windows.Forms.Label()
        Me.Label252 = New System.Windows.Forms.Label()
        Me.txtOpacityMethod9Single = New System.Windows.Forms.TextBox()
        Me.txtTestDurationMethod9Single = New System.Windows.Forms.TextBox()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single = New System.Windows.Forms.ComboBox()
        Me.cboOperatingCapacityUnitsMethod9Single = New System.Windows.Forms.ComboBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.txtOperatingCapacityMethod9Single = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate1Method9Single = New System.Windows.Forms.TextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.txtApplicableRegulationMethod9Single = New System.Windows.Forms.TextBox()
        Me.txtControlEquipmentOperatingDataMethod9Single = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityMethod9Single = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.cboAllowableEmissionRateUnits1Method9Single = New System.Windows.Forms.ComboBox()
        Me.TPMethodNineMultiple = New System.Windows.Forms.TabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdbMethod9MultiAverage30 = New System.Windows.Forms.RadioButton()
        Me.rdbMethod9MultiHighestAvg = New System.Windows.Forms.RadioButton()
        Me.Label261 = New System.Windows.Forms.Label()
        Me.Label260 = New System.Windows.Forms.Label()
        Me.Label259 = New System.Windows.Forms.Label()
        Me.Label258 = New System.Windows.Forms.Label()
        Me.Label257 = New System.Windows.Forms.Label()
        Me.txt6minuteAvg1EMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txt6minuteAvg1DMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txt6minuteAvg1AMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txt6minuteAvg1BMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txt6minuteAvg1CMethod9Multi = New System.Windows.Forms.TextBox()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRate3Method9Multi = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate4Method9Multi = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate5Method9Multi = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityMethod9Multi5 = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityMethod9Multi2 = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityMethod9Multi3 = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityMethod9Multi4 = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi3 = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi2 = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi4 = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi5 = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityMethod9Multi1 = New System.Windows.Forms.TextBox()
        Me.cboOperatingCapacityUnitsMethod9Multi = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi = New System.Windows.Forms.ComboBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRate1Method9Multi = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate2Method9Multi = New System.Windows.Forms.TextBox()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.txtApplicableRegulationMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txtControlEquipmentOperatingDataMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi1 = New System.Windows.Forms.TextBox()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.cboAllowableEmissionRateUnitsMethod9Multi = New System.Windows.Forms.ComboBox()
        Me.TPMethodNineMultiple2 = New System.Windows.Forms.TabPage()
        Me.Label266 = New System.Windows.Forms.Label()
        Me.Label265 = New System.Windows.Forms.Label()
        Me.Label264 = New System.Windows.Forms.Label()
        Me.Label263 = New System.Windows.Forms.Label()
        Me.Label262 = New System.Windows.Forms.Label()
        Me.txtEquipmentItem1EMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txtEquipmentItem1DMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txtEquipmentItem1BMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txtEquipmentItem1CMethod9Multi = New System.Windows.Forms.TextBox()
        Me.txtEquipmentItem1AMethod9Multi = New System.Windows.Forms.TextBox()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.TPMemorandum = New System.Windows.Forms.TabPage()
        Me.Label247 = New System.Windows.Forms.Label()
        Me.txtApplicableRegulationMemorandum = New System.Windows.Forms.TextBox()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.TCMemorandum = New System.Windows.Forms.TabControl()
        Me.TPMemoStandard = New System.Windows.Forms.TabPage()
        Me.txtMemorandumStandard = New System.Windows.Forms.TextBox()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.TPMemoToFile = New System.Windows.Forms.TabPage()
        Me.Label251 = New System.Windows.Forms.Label()
        Me.Label191 = New System.Windows.Forms.Label()
        Me.txtSerialNumberMemorandumToFile = New System.Windows.Forms.TextBox()
        Me.txtModelMemorandumToFile = New System.Windows.Forms.TextBox()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.txtMemorandumToFile = New System.Windows.Forms.TextBox()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.TPMemoPTE = New System.Windows.Forms.TabPage()
        Me.Label250 = New System.Windows.Forms.Label()
        Me.Label249 = New System.Windows.Forms.Label()
        Me.Label248 = New System.Windows.Forms.Label()
        Me.txtMemorandumPTE = New System.Windows.Forms.TextBox()
        Me.Label190 = New System.Windows.Forms.Label()
        Me.txtControlEquipmentOperatingDataMemorandumPTE = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityMemorandumPTE = New System.Windows.Forms.TextBox()
        Me.Label192 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRate2MemorandumPTE = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate3MemorandumPTE = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate1MemorandumPTE = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityMemorandumPTE = New System.Windows.Forms.TextBox()
        Me.Label194 = New System.Windows.Forms.Label()
        Me.cboOperatingCapacityUnitsMemorandumPTE = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits2MemorandumPTE = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits3MemorandumPTE = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits1MemorandumPTE = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE = New System.Windows.Forms.ComboBox()
        Me.Label195 = New System.Windows.Forms.Label()
        Me.Label196 = New System.Windows.Forms.Label()
        Me.TPRata = New System.Windows.Forms.TabPage()
        Me.txtPart75Statement = New System.Windows.Forms.TextBox()
        Me.lblRATAPart75 = New System.Windows.Forms.Label()
        Me.chbOmitRunRata5 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata4 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata3 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata2 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata12 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata11 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata10 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata9 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata8 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata7 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata6 = New System.Windows.Forms.CheckBox()
        Me.chbOmitRunRata1 = New System.Windows.Forms.CheckBox()
        Me.Label319 = New System.Windows.Forms.Label()
        Me.Label318 = New System.Windows.Forms.Label()
        Me.Label317 = New System.Windows.Forms.Label()
        Me.txtCMSRata11 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata12 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata10 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata9 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata8 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata7 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata6 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata5 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata4 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata3 = New System.Windows.Forms.TextBox()
        Me.txtCMSRata2 = New System.Windows.Forms.TextBox()
        Me.Label316 = New System.Windows.Forms.Label()
        Me.txtRefMethodRata8 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata9 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata12 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata11 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata10 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata7 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata6 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata5 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata4 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata3 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata2 = New System.Windows.Forms.TextBox()
        Me.Label314 = New System.Windows.Forms.Label()
        Me.Label313 = New System.Windows.Forms.Label()
        Me.Label312 = New System.Windows.Forms.Label()
        Me.Label311 = New System.Windows.Forms.Label()
        Me.Label310 = New System.Windows.Forms.Label()
        Me.Label309 = New System.Windows.Forms.Label()
        Me.Label308 = New System.Windows.Forms.Label()
        Me.Label307 = New System.Windows.Forms.Label()
        Me.Label306 = New System.Windows.Forms.Label()
        Me.Label305 = New System.Windows.Forms.Label()
        Me.Label304 = New System.Windows.Forms.Label()
        Me.Label303 = New System.Windows.Forms.Label()
        Me.Label302 = New System.Windows.Forms.Label()
        Me.Label301 = New System.Windows.Forms.Label()
        Me.Label300 = New System.Windows.Forms.Label()
        Me.Label299 = New System.Windows.Forms.Label()
        Me.txtCMSRata1 = New System.Windows.Forms.TextBox()
        Me.txtRefMethodRata1 = New System.Windows.Forms.TextBox()
        Me.Label114b = New System.Windows.Forms.Label()
        Me.cboUnitsRata = New System.Windows.Forms.ComboBox()
        Me.lable3b = New System.Windows.Forms.Label()
        Me.Label112b = New System.Windows.Forms.Label()
        Me.Label111b = New System.Windows.Forms.Label()
        Me.cboDiluentRata = New System.Windows.Forms.ComboBox()
        Me.lblDiluentRata = New System.Windows.Forms.Label()
        Me.txtApplicableStandardPercentRata = New System.Windows.Forms.TextBox()
        Me.txtRefMethodPercentRata = New System.Windows.Forms.TextBox()
        Me.txtRelativeAccuracy = New System.Windows.Forms.TextBox()
        Me.Label187 = New System.Windows.Forms.Label()
        Me.lblStandardRata = New System.Windows.Forms.Label()
        Me.lblRefMethodRata = New System.Windows.Forms.Label()
        Me.txtOtherInformationRata = New System.Windows.Forms.TextBox()
        Me.Label185 = New System.Windows.Forms.Label()
        Me.Label183 = New System.Windows.Forms.Label()
        Me.cboDilutentMonitoredRata = New System.Windows.Forms.ComboBox()
        Me.Label177 = New System.Windows.Forms.Label()
        Me.txtApplicableRegulationRata = New System.Windows.Forms.TextBox()
        Me.Label175 = New System.Windows.Forms.Label()
        Me.txtApplicableStandardRata = New System.Windows.Forms.TextBox()
        Me.Label173 = New System.Windows.Forms.Label()
        Me.TPTwoStack = New System.Windows.Forms.TabPage()
        Me.Label227 = New System.Windows.Forms.Label()
        Me.Label226 = New System.Windows.Forms.Label()
        Me.Label225 = New System.Windows.Forms.Label()
        Me.Label224 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.txtOtherInformationTwoStack = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.TCTwoStack = New System.Windows.Forms.TabControl()
        Me.TPTwoStackStandard = New System.Windows.Forms.TabPage()
        Me.Label298 = New System.Windows.Forms.Label()
        Me.Label237 = New System.Windows.Forms.Label()
        Me.Label236 = New System.Windows.Forms.Label()
        Me.Label235 = New System.Windows.Forms.Label()
        Me.Label234 = New System.Windows.Forms.Label()
        Me.Label233 = New System.Windows.Forms.Label()
        Me.Label232 = New System.Windows.Forms.Label()
        Me.Label231 = New System.Windows.Forms.Label()
        Me.Label230 = New System.Windows.Forms.Label()
        Me.Label229 = New System.Windows.Forms.Label()
        Me.Label228 = New System.Windows.Forms.Label()
        Me.txtEmissRateTotalAvgTwoStackStandard = New System.Windows.Forms.TextBox()
        Me.txtEmissRateAvgTwoStackStandard2 = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgTwoStackStandard1 = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTotalTwoStackStandard3 = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTotalTwoStackStandard2 = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTotalTwoStackStandard1 = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtRunNumTwoStackStandard2C = New System.Windows.Forms.TextBox()
        Me.btnClearTwoStackStandard2C = New System.Windows.Forms.Button()
        Me.txtGasMoistTwoStackStandard2C = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackStandard2C = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackStandard2C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackStandard2C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackStandard2C = New System.Windows.Forms.TextBox()
        Me.txtGasMoistTwoStackStandard2B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackStandard2B = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackStandard2B = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackStandard2C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackStandard2B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackStandard2B = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackStandard2B = New System.Windows.Forms.TextBox()
        Me.txtRunNumTwoStackStandard2B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackStandard2A = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackStandard2A = New System.Windows.Forms.TextBox()
        Me.txtGasMoistTwoStackStandard2A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackStandard2A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackStandard2A = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackStandard2A = New System.Windows.Forms.TextBox()
        Me.txtRunNumTwoStackStandard2A = New System.Windows.Forms.TextBox()
        Me.btnClearTwoStackStandard2A = New System.Windows.Forms.Button()
        Me.btnClearTwoStackStandard2B = New System.Windows.Forms.Button()
        Me.txtRunNumTwoStackStandard1C = New System.Windows.Forms.TextBox()
        Me.btnClearTwoStackStandard1C = New System.Windows.Forms.Button()
        Me.txtGasMoistTwoStackStandard1C = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackStandard1C = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackStandard1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackStandard1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackStandard1C = New System.Windows.Forms.TextBox()
        Me.txtGasMoistTwoStackStandard1B = New System.Windows.Forms.TextBox()
        Me.txtStackOneNameTwoStackStandard = New System.Windows.Forms.TextBox()
        Me.txtStackTwoNameTwoStackStandard = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtEmissRateAvgTwoStackStandard1 = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgTwoStackStandard2 = New System.Windows.Forms.TextBox()
        Me.cboEmissRateUnitTwoStackStandard = New System.Windows.Forms.ComboBox()
        Me.cboPollConUnitTwoStackStandard = New System.Windows.Forms.ComboBox()
        Me.txtEmissRateTwoStackStandard1B = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackStandard1B = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackStandard1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackStandard1B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackStandard1B = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackStandard1B = New System.Windows.Forms.TextBox()
        Me.txtRunNumTwoStackStandard1B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackStandard1A = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackStandard1A = New System.Windows.Forms.TextBox()
        Me.txtGasMoistTwoStackStandard1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackStandard1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackStandard1A = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackStandard1A = New System.Windows.Forms.TextBox()
        Me.txtRunNumTwoStackStandard1A = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.btnClearTwoStackStandard1A = New System.Windows.Forms.Button()
        Me.btnClearTwoStackStandard1B = New System.Windows.Forms.Button()
        Me.txtPercentAllowableTwoStack = New System.Windows.Forms.TextBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.TPTwoStackDRE = New System.Windows.Forms.TabPage()
        Me.Label246 = New System.Windows.Forms.Label()
        Me.Label245 = New System.Windows.Forms.Label()
        Me.Label244 = New System.Windows.Forms.Label()
        Me.Label243 = New System.Windows.Forms.Label()
        Me.Label242 = New System.Windows.Forms.Label()
        Me.Label241 = New System.Windows.Forms.Label()
        Me.Label240 = New System.Windows.Forms.Label()
        Me.Label239 = New System.Windows.Forms.Label()
        Me.Label238 = New System.Windows.Forms.Label()
        Me.txtEmissRateAvgTwoStackDRE2 = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgTwoStackDRE1 = New System.Windows.Forms.TextBox()
        Me.txtDestructionEfficiencyTwoStackDRE = New System.Windows.Forms.TextBox()
        Me.Label172 = New System.Windows.Forms.Label()
        Me.txtRunNumTwoStackDRE2C = New System.Windows.Forms.TextBox()
        Me.btnClearTwoStackDRE2C = New System.Windows.Forms.Button()
        Me.txtGasMoistTwoStackDRE2C = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackDRE2C = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackDRE2C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackDRE2C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackDRE2C = New System.Windows.Forms.TextBox()
        Me.txtGasMoistTwoStackDRE2B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackDRE2B = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackDRE2B = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackDRE2C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackDRE2B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackDRE2B = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackDRE2B = New System.Windows.Forms.TextBox()
        Me.txtRunNumTwoStackDRE2B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackDRE2A = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackDRE2A = New System.Windows.Forms.TextBox()
        Me.txtGasMoistTwoStackDRE2A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackDRE2A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackDRE2A = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackDRE2A = New System.Windows.Forms.TextBox()
        Me.txtRunNumTwoStackDRE2A = New System.Windows.Forms.TextBox()
        Me.btnClearTwoStackDRE2A = New System.Windows.Forms.Button()
        Me.btnClearTwoStackDRE2B = New System.Windows.Forms.Button()
        Me.txtRunNumTwoStackDRE1C = New System.Windows.Forms.TextBox()
        Me.btnClearTwoStackDRE1C = New System.Windows.Forms.Button()
        Me.txtGasMoistTwoStackDRE1C = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackDRE1C = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackDRE1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackDRE1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackDRE1C = New System.Windows.Forms.TextBox()
        Me.txtGasMoistTwoStackDRE1B = New System.Windows.Forms.TextBox()
        Me.txtStackTwoNameTwoStackDRE = New System.Windows.Forms.TextBox()
        Me.Label174 = New System.Windows.Forms.Label()
        Me.Label176 = New System.Windows.Forms.Label()
        Me.txtEmissRateAvgTwoStackDRE1 = New System.Windows.Forms.TextBox()
        Me.txtPollConcAvgTwoStackDRE2 = New System.Windows.Forms.TextBox()
        Me.cboEmissRateUnitTwoStackDRE = New System.Windows.Forms.ComboBox()
        Me.cboPollConUnitTwoStackDRE = New System.Windows.Forms.ComboBox()
        Me.txtEmissRateTwoStackDRE1B = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackDRE1B = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackDRE1C = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackDRE1B = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackDRE1B = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackDRE1B = New System.Windows.Forms.TextBox()
        Me.txtRunNumTwoStackDRE1B = New System.Windows.Forms.TextBox()
        Me.txtEmissRateTwoStackDRE1A = New System.Windows.Forms.TextBox()
        Me.txtPollConcTwoStackDRE1A = New System.Windows.Forms.TextBox()
        Me.txtGasTempTwoStackDRE1A = New System.Windows.Forms.TextBox()
        Me.txtRunNumTwoStackDRE1A = New System.Windows.Forms.TextBox()
        Me.Label178 = New System.Windows.Forms.Label()
        Me.Label179 = New System.Windows.Forms.Label()
        Me.Label180 = New System.Windows.Forms.Label()
        Me.Label181 = New System.Windows.Forms.Label()
        Me.Label182 = New System.Windows.Forms.Label()
        Me.Label184 = New System.Windows.Forms.Label()
        Me.Label186 = New System.Windows.Forms.Label()
        Me.btnClearTwoStackDRE1A = New System.Windows.Forms.Button()
        Me.btnClearTwoStackDRE1B = New System.Windows.Forms.Button()
        Me.txtGasMoistTwoStackDRE1A = New System.Windows.Forms.TextBox()
        Me.txtStackOneNameTwoStackDRE = New System.Windows.Forms.TextBox()
        Me.txtGasFlowDSCFMTwoStackDRE1A = New System.Windows.Forms.TextBox()
        Me.txtGasFlowACFMTwoStackDRE1A = New System.Windows.Forms.TextBox()
        Me.Label188 = New System.Windows.Forms.Label()
        Me.txtControlEquipmentOperatingDataTwoStack = New System.Windows.Forms.TextBox()
        Me.txtApplicableRegulationTwoStack = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityTwoStack = New System.Windows.Forms.TextBox()
        Me.Label189 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRate2TwoStack = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate3TwoStack = New System.Windows.Forms.TextBox()
        Me.txtAllowableEmissionRate1TwoStack = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityTwoStack = New System.Windows.Forms.TextBox()
        Me.cboOperatingCapacityUnitsTwoStack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits2TwoStack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits3TwoStack = New System.Windows.Forms.ComboBox()
        Me.cboAllowableEmissionRateUnits1TwoStack = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack = New System.Windows.Forms.ComboBox()
        Me.TPMethodTwentyTwo = New System.Windows.Forms.TabPage()
        Me.Label296 = New System.Windows.Forms.Label()
        Me.Label214 = New System.Windows.Forms.Label()
        Me.Label295 = New System.Windows.Forms.Label()
        Me.Label294 = New System.Windows.Forms.Label()
        Me.Label293 = New System.Windows.Forms.Label()
        Me.Label215 = New System.Windows.Forms.Label()
        Me.txtOtherInformationMethod22 = New System.Windows.Forms.TextBox()
        Me.Label216 = New System.Windows.Forms.Label()
        Me.txtTestDurationMethod22 = New System.Windows.Forms.TextBox()
        Me.txtAccumulatedEmissionMethod22 = New System.Windows.Forms.TextBox()
        Me.Label217 = New System.Windows.Forms.Label()
        Me.Label218 = New System.Windows.Forms.Label()
        Me.txtApplicableRegulationMethod22 = New System.Windows.Forms.TextBox()
        Me.txtOperatingCapacityMethod22 = New System.Windows.Forms.TextBox()
        Me.Label219 = New System.Windows.Forms.Label()
        Me.txtAllowableEmissionRateMethod22 = New System.Windows.Forms.TextBox()
        Me.txtMaximumExpectedOperatingCapacityMethod22 = New System.Windows.Forms.TextBox()
        Me.Label220 = New System.Windows.Forms.Label()
        Me.Label221 = New System.Windows.Forms.Label()
        Me.cboOperatingCapacityUnitsMethod22 = New System.Windows.Forms.ComboBox()
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod22 = New System.Windows.Forms.ComboBox()
        Me.Label222 = New System.Windows.Forms.Label()
        Me.TPSSCPWork = New System.Windows.Forms.TabPage()
        Me.cboStaffResponsible = New System.Windows.Forms.ComboBox()
        Me.Label223 = New System.Windows.Forms.Label()
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox()
        Me.Label315 = New System.Windows.Forms.Label()
        Me.chbAcknoledgmentLetterSent = New System.Windows.Forms.CheckBox()
        Me.DTPAcknoledgmentLetterSent = New System.Windows.Forms.DateTimePicker()
        Me.txtEnforcementNumber = New System.Windows.Forms.TextBox()
        Me.chbEventComplete = New System.Windows.Forms.CheckBox()
        Me.DTPEventCompleteDate = New System.Windows.Forms.DateTimePicker()
        Me.btnEnforcementProcess = New System.Windows.Forms.Button()
        Me.btnSaveSSCPData = New System.Windows.Forms.Button()
        Me.txtTestReportReceivedbySSCPDate = New System.Windows.Forms.TextBox()
        Me.Label320 = New System.Windows.Forms.Label()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.rdbTestReportFollowUpYes = New System.Windows.Forms.RadioButton()
        Me.rdbTestReportFollowUpNo = New System.Windows.Forms.RadioButton()
        Me.Label321 = New System.Windows.Forms.Label()
        Me.Label322 = New System.Windows.Forms.Label()
        Me.Label323 = New System.Windows.Forms.Label()
        Me.DTPTestReportDueDate = New System.Windows.Forms.DateTimePicker()
        Me.chbTestReportChangeDueDate = New System.Windows.Forms.CheckBox()
        Me.DTPTestReportNewDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label325 = New System.Windows.Forms.Label()
        Me.txtTestReportComments = New System.Windows.Forms.TextBox()
        Me.Label326 = New System.Windows.Forms.Label()
        Me.txtTestReportDueDate = New System.Windows.Forms.TextBox()
        Me.DeletedTestFlag = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SCTestReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCTestReports.Panel1.SuspendLayout()
        Me.SCTestReports.Panel2.SuspendLayout()
        Me.SCTestReports.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TCDocumentTypes.SuspendLayout()
        Me.TPOneStack.SuspendLayout()
        Me.TCOneStack.SuspendLayout()
        Me.TPOneStackTwoRun.SuspendLayout()
        Me.TPOneStackThreeRun.SuspendLayout()
        Me.TPOneStackFourRun.SuspendLayout()
        Me.TPLoadingRack.SuspendLayout()
        Me.TPPondTreatment.SuspendLayout()
        Me.TPGasConcentration.SuspendLayout()
        Me.TPFlare.SuspendLayout()
        Me.TPMethodNine.SuspendLayout()
        Me.TCMethodNine.SuspendLayout()
        Me.TPMethodNineSingle.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TPMethodNineMultiple.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TPMethodNineMultiple2.SuspendLayout()
        Me.TPMemorandum.SuspendLayout()
        Me.TCMemorandum.SuspendLayout()
        Me.TPMemoStandard.SuspendLayout()
        Me.TPMemoToFile.SuspendLayout()
        Me.TPMemoPTE.SuspendLayout()
        Me.TPRata.SuspendLayout()
        Me.TPTwoStack.SuspendLayout()
        Me.TCTwoStack.SuspendLayout()
        Me.TPTwoStackStandard.SuspendLayout()
        Me.TPTwoStackDRE.SuspendLayout()
        Me.TPMethodTwentyTwo.SuspendLayout()
        Me.TPSSCPWork.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiFile, Me.mmiView, Me.mmiTool})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(790, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mmiFile
        '
        Me.mmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiSave, Me.mmiClose})
        Me.mmiFile.Name = "mmiFile"
        Me.mmiFile.Size = New System.Drawing.Size(37, 20)
        Me.mmiFile.Text = "File"
        '
        'mmiSave
        '
        Me.mmiSave.Name = "mmiSave"
        Me.mmiSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mmiSave.Size = New System.Drawing.Size(152, 22)
        Me.mmiSave.Text = "Save"
        '
        'mmiClose
        '
        Me.mmiClose.Name = "mmiClose"
        Me.mmiClose.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.mmiClose.Size = New System.Drawing.Size(152, 22)
        Me.mmiClose.Text = "Close"
        '
        'mmiView
        '
        Me.mmiView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiOpenMemo})
        Me.mmiView.Name = "mmiView"
        Me.mmiView.Size = New System.Drawing.Size(44, 20)
        Me.mmiView.Text = "View"
        '
        'mmiOpenMemo
        '
        Me.mmiOpenMemo.Name = "mmiOpenMemo"
        Me.mmiOpenMemo.Size = New System.Drawing.Size(152, 22)
        Me.mmiOpenMemo.Text = "Open Memo"
        '
        'mmiTool
        '
        Me.mmiTool.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiOpenTestLogNotification, Me.mmiOpenExcelFile, Me.mmiPrePopulate, Me.mmiDefaultCompliance, Me.mmiPrintNonConf})
        Me.mmiTool.Name = "mmiTool"
        Me.mmiTool.Size = New System.Drawing.Size(48, 20)
        Me.mmiTool.Text = "Tools"
        '
        'mmiOpenTestLogNotification
        '
        Me.mmiOpenTestLogNotification.Name = "mmiOpenTestLogNotification"
        Me.mmiOpenTestLogNotification.Size = New System.Drawing.Size(233, 22)
        Me.mmiOpenTestLogNotification.Text = "Test Log Notification"
        Me.mmiOpenTestLogNotification.ToolTipText = "Open Test Notification"
        '
        'mmiOpenExcelFile
        '
        Me.mmiOpenExcelFile.Name = "mmiOpenExcelFile"
        Me.mmiOpenExcelFile.Size = New System.Drawing.Size(233, 22)
        Me.mmiOpenExcelFile.Text = "Open Excel File"
        '
        'mmiPrePopulate
        '
        Me.mmiPrePopulate.Name = "mmiPrePopulate"
        Me.mmiPrePopulate.Size = New System.Drawing.Size(233, 22)
        Me.mmiPrePopulate.Text = "Pre-Populate"
        '
        'mmiDefaultCompliance
        '
        Me.mmiDefaultCompliance.Name = "mmiDefaultCompliance"
        Me.mmiDefaultCompliance.Size = New System.Drawing.Size(233, 22)
        Me.mmiDefaultCompliance.Text = "Default Compliance Manager"
        '
        'mmiPrintNonConf
        '
        Me.mmiPrintNonConf.Name = "mmiPrintNonConf"
        Me.mmiPrintNonConf.Size = New System.Drawing.Size(233, 22)
        Me.mmiPrintNonConf.Text = "Print Non-Confidential Report"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbSearch, Me.tsbPrePopulate, Me.tsbPrint, Me.tsbClear, Me.tsbResize, Me.tsbMemo, Me.tsbTestLogLink, Me.tsbDelete, Me.tsbConfidentialData})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(790, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), System.Drawing.Image)
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(23, 22)
        Me.tsbSave.Text = "Save"
        '
        'tsbSearch
        '
        Me.tsbSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSearch.Image = CType(resources.GetObject("tsbSearch.Image"), System.Drawing.Image)
        Me.tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSearch.Name = "tsbSearch"
        Me.tsbSearch.Size = New System.Drawing.Size(23, 22)
        Me.tsbSearch.Text = "Find Test Report"
        '
        'tsbPrePopulate
        '
        Me.tsbPrePopulate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPrePopulate.Image = CType(resources.GetObject("tsbPrePopulate.Image"), System.Drawing.Image)
        Me.tsbPrePopulate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrePopulate.Name = "tsbPrePopulate"
        Me.tsbPrePopulate.Size = New System.Drawing.Size(23, 22)
        Me.tsbPrePopulate.Text = "Copy another Test Report"
        '
        'tsbPrint
        '
        Me.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPrint.Image = CType(resources.GetObject("tsbPrint.Image"), System.Drawing.Image)
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(23, 22)
        Me.tsbPrint.Text = "Print"
        '
        'tsbClear
        '
        Me.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClear.Image = CType(resources.GetObject("tsbClear.Image"), System.Drawing.Image)
        Me.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClear.Name = "tsbClear"
        Me.tsbClear.Size = New System.Drawing.Size(23, 22)
        Me.tsbClear.Text = "Clear"
        Me.tsbClear.Visible = False
        '
        'tsbResize
        '
        Me.tsbResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbResize.Image = CType(resources.GetObject("tsbResize.Image"), System.Drawing.Image)
        Me.tsbResize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbResize.Name = "tsbResize"
        Me.tsbResize.Size = New System.Drawing.Size(23, 22)
        Me.tsbResize.Text = "Resize"
        '
        'tsbMemo
        '
        Me.tsbMemo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbMemo.Image = CType(resources.GetObject("tsbMemo.Image"), System.Drawing.Image)
        Me.tsbMemo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbMemo.Name = "tsbMemo"
        Me.tsbMemo.Size = New System.Drawing.Size(23, 22)
        Me.tsbMemo.Text = "View Memo"
        '
        'tsbTestLogLink
        '
        Me.tsbTestLogLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbTestLogLink.Image = CType(resources.GetObject("tsbTestLogLink.Image"), System.Drawing.Image)
        Me.tsbTestLogLink.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTestLogLink.Name = "tsbTestLogLink"
        Me.tsbTestLogLink.Size = New System.Drawing.Size(23, 22)
        Me.tsbTestLogLink.Text = "Open Test Notification"
        '
        'tsbDelete
        '
        Me.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbDelete.Image = CType(resources.GetObject("tsbDelete.Image"), System.Drawing.Image)
        Me.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDelete.Name = "tsbDelete"
        Me.tsbDelete.Size = New System.Drawing.Size(23, 22)
        Me.tsbDelete.Text = "Delete Test Report Data"
        '
        'tsbConfidentialData
        '
        Me.tsbConfidentialData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbConfidentialData.Image = CType(resources.GetObject("tsbConfidentialData.Image"), System.Drawing.Image)
        Me.tsbConfidentialData.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfidentialData.Name = "tsbConfidentialData"
        Me.tsbConfidentialData.Size = New System.Drawing.Size(23, 22)
        Me.tsbConfidentialData.ToolTipText = "Confidential Data"
        '
        'SCTestReports
        '
        Me.SCTestReports.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.SCTestReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCTestReports.Location = New System.Drawing.Point(0, 49)
        Me.SCTestReports.Name = "SCTestReports"
        Me.SCTestReports.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SCTestReports.Panel1
        '
        Me.SCTestReports.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.SCTestReports.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SCTestReports.Panel2
        '
        Me.SCTestReports.Panel2.AutoScroll = True
        Me.SCTestReports.Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SCTestReports.Panel2.Controls.Add(Me.TCDocumentTypes)
        Me.SCTestReports.Size = New System.Drawing.Size(790, 693)
        Me.SCTestReports.SplitterDistance = 375
        Me.SCTestReports.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblMemoEntered)
        Me.GroupBox1.Controls.Add(Me.lblPreComplianceStatus)
        Me.GroupBox1.Controls.Add(Me.txtDaysfromTestToAPB)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.clbWitnessingEngineers)
        Me.GroupBox1.Controls.Add(Me.llbTestNotifiactionNumber)
        Me.GroupBox1.Controls.Add(Me.cboTestNotificationNumber)
        Me.GroupBox1.Controls.Add(Me.labTestNotificationNumber)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.txtAssignedToEngineer)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.cboComplianceStatus)
        Me.GroupBox1.Controls.Add(Me.Label65)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtDaysInAPB)
        Me.GroupBox1.Controls.Add(Me.cboccBox)
        Me.GroupBox1.Controls.Add(Me.txtDaysAssigned)
        Me.GroupBox1.Controls.Add(Me.Label297)
        Me.GroupBox1.Controls.Add(Me.cboTestingFirm)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.cboPollutantDetermined)
        Me.GroupBox1.Controls.Add(Me.cboReportType)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.cboComplianceManager)
        Me.GroupBox1.Controls.Add(Me.DTPTestDateComplete)
        Me.GroupBox1.Controls.Add(Me.DTPTestDateStart)
        Me.GroupBox1.Controls.Add(Me.cboReviewingEngineer)
        Me.GroupBox1.Controls.Add(Me.cboUnitManager)
        Me.GroupBox1.Controls.Add(Me.cboISMPUnit)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtProgramManager)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.cboMethodDetermined)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtCompleteDate)
        Me.GroupBox1.Controls.Add(Me.txtReceivedByAPB)
        Me.GroupBox1.Controls.Add(Me.cboWitnessingEngineer)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.txtFacilityState)
        Me.GroupBox1.Controls.Add(Me.txtSourceTested)
        Me.GroupBox1.Controls.Add(Me.txtFacilityCity)
        Me.GroupBox1.Controls.Add(Me.txtFacilityName)
        Me.GroupBox1.Controls.Add(Me.txtAirsNumber)
        Me.GroupBox1.Controls.Add(Me.txtReferenceNumber)
        Me.GroupBox1.Controls.Add(Me.lblDatesTested)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.labReferenceNumber)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(790, 375)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report Information "
        '
        'lblMemoEntered
        '
        Me.lblMemoEntered.AutoSize = True
        Me.lblMemoEntered.Location = New System.Drawing.Point(190, 23)
        Me.lblMemoEntered.Name = "lblMemoEntered"
        Me.lblMemoEntered.Size = New System.Drawing.Size(76, 13)
        Me.lblMemoEntered.TabIndex = 465
        Me.lblMemoEntered.Text = "Memo Entered"
        '
        'lblPreComplianceStatus
        '
        Me.lblPreComplianceStatus.AutoSize = True
        Me.lblPreComplianceStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreComplianceStatus.Location = New System.Drawing.Point(431, 310)
        Me.lblPreComplianceStatus.Name = "lblPreComplianceStatus"
        Me.lblPreComplianceStatus.Size = New System.Drawing.Size(327, 13)
        Me.lblPreComplianceStatus.TabIndex = 50
        Me.lblPreComplianceStatus.Text = "This test report was flagged as potentially non-compliant"
        Me.lblPreComplianceStatus.Visible = False
        '
        'txtDaysfromTestToAPB
        '
        Me.txtDaysfromTestToAPB.AcceptsTab = True
        Me.txtDaysfromTestToAPB.Location = New System.Drawing.Point(429, 165)
        Me.txtDaysfromTestToAPB.Name = "txtDaysfromTestToAPB"
        Me.txtDaysfromTestToAPB.ReadOnly = True
        Me.txtDaysfromTestToAPB.Size = New System.Drawing.Size(40, 20)
        Me.txtDaysfromTestToAPB.TabIndex = 49
        Me.txtDaysfromTestToAPB.TabStop = False
        Me.txtDaysfromTestToAPB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(306, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Days from Test to APB:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'clbWitnessingEngineers
        '
        Me.clbWitnessingEngineers.CheckOnClick = True
        Me.clbWitnessingEngineers.FormattingEnabled = True
        Me.clbWitnessingEngineers.Location = New System.Drawing.Point(166, 288)
        Me.clbWitnessingEngineers.Name = "clbWitnessingEngineers"
        Me.clbWitnessingEngineers.Size = New System.Drawing.Size(136, 94)
        Me.clbWitnessingEngineers.TabIndex = 14
        Me.clbWitnessingEngineers.TabStop = False
        '
        'llbTestNotifiactionNumber
        '
        Me.llbTestNotifiactionNumber.AutoSize = True
        Me.llbTestNotifiactionNumber.Location = New System.Drawing.Point(11, 241)
        Me.llbTestNotifiactionNumber.Name = "llbTestNotifiactionNumber"
        Me.llbTestNotifiactionNumber.Size = New System.Drawing.Size(124, 13)
        Me.llbTestNotifiactionNumber.TabIndex = 53
        Me.llbTestNotifiactionNumber.TabStop = True
        Me.llbTestNotifiactionNumber.Text = "Test Notification Number"
        Me.llbTestNotifiactionNumber.Visible = False
        '
        'cboTestNotificationNumber
        '
        Me.cboTestNotificationNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTestNotificationNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTestNotificationNumber.Location = New System.Drawing.Point(152, 237)
        Me.cboTestNotificationNumber.Name = "cboTestNotificationNumber"
        Me.cboTestNotificationNumber.Size = New System.Drawing.Size(150, 21)
        Me.cboTestNotificationNumber.TabIndex = 3
        '
        'labTestNotificationNumber
        '
        Me.labTestNotificationNumber.AutoSize = True
        Me.labTestNotificationNumber.Location = New System.Drawing.Point(11, 241)
        Me.labTestNotificationNumber.Name = "labTestNotificationNumber"
        Me.labTestNotificationNumber.Size = New System.Drawing.Size(127, 13)
        Me.labTestNotificationNumber.TabIndex = 464
        Me.labTestNotificationNumber.Text = "Test Notification Number:"
        Me.labTestNotificationNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(518, 217)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(139, 13)
        Me.Label21.TabIndex = 13
        Me.Label21.Text = "Assigned to ISMP Engineer:"
        '
        'txtAssignedToEngineer
        '
        Me.txtAssignedToEngineer.BackColor = System.Drawing.SystemColors.Control
        Me.txtAssignedToEngineer.Location = New System.Drawing.Point(661, 213)
        Me.txtAssignedToEngineer.Name = "txtAssignedToEngineer"
        Me.txtAssignedToEngineer.ReadOnly = True
        Me.txtAssignedToEngineer.Size = New System.Drawing.Size(97, 20)
        Me.txtAssignedToEngineer.TabIndex = 37
        Me.txtAssignedToEngineer.TabStop = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(518, 241)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(103, 13)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "Completed by ISMP:"
        '
        'cboComplianceStatus
        '
        Me.cboComplianceStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboComplianceStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboComplianceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComplianceStatus.Location = New System.Drawing.Point(590, 113)
        Me.cboComplianceStatus.Name = "cboComplianceStatus"
        Me.cboComplianceStatus.Size = New System.Drawing.Size(168, 21)
        Me.cboComplianceStatus.TabIndex = 9
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(533, 169)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(116, 13)
        Me.Label65.TabIndex = 15
        Me.Label65.Text = "Days in APB/Engineer:"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(609, 290)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(22, 13)
        Me.Label20.TabIndex = 34
        Me.Label20.Text = "cc:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtDaysInAPB
        '
        Me.txtDaysInAPB.AcceptsTab = True
        Me.txtDaysInAPB.Location = New System.Drawing.Point(675, 165)
        Me.txtDaysInAPB.Name = "txtDaysInAPB"
        Me.txtDaysInAPB.ReadOnly = True
        Me.txtDaysInAPB.Size = New System.Drawing.Size(40, 20)
        Me.txtDaysInAPB.TabIndex = 39
        Me.txtDaysInAPB.TabStop = False
        Me.txtDaysInAPB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cboccBox
        '
        Me.cboccBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboccBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboccBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboccBox.Location = New System.Drawing.Point(639, 286)
        Me.cboccBox.Name = "cboccBox"
        Me.cboccBox.Size = New System.Drawing.Size(119, 21)
        Me.cboccBox.TabIndex = 13
        '
        'txtDaysAssigned
        '
        Me.txtDaysAssigned.Location = New System.Drawing.Point(718, 165)
        Me.txtDaysAssigned.Name = "txtDaysAssigned"
        Me.txtDaysAssigned.ReadOnly = True
        Me.txtDaysAssigned.Size = New System.Drawing.Size(40, 20)
        Me.txtDaysAssigned.TabIndex = 40
        Me.txtDaysAssigned.TabStop = False
        Me.txtDaysAssigned.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label297
        '
        Me.Label297.AutoSize = True
        Me.Label297.Location = New System.Drawing.Point(428, 117)
        Me.Label297.Name = "Label297"
        Me.Label297.Size = New System.Drawing.Size(162, 13)
        Me.Label297.TabIndex = 16
        Me.Label297.Text = "ISMP Compliance Determination:"
        Me.Label297.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboTestingFirm
        '
        Me.cboTestingFirm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTestingFirm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTestingFirm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTestingFirm.Location = New System.Drawing.Point(414, 88)
        Me.cboTestingFirm.Name = "cboTestingFirm"
        Me.cboTestingFirm.Size = New System.Drawing.Size(344, 21)
        Me.cboTestingFirm.TabIndex = 8
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(341, 91)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(67, 13)
        Me.Label19.TabIndex = 17
        Me.Label19.Text = "Testing Firm:"
        '
        'cboPollutantDetermined
        '
        Me.cboPollutantDetermined.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollutantDetermined.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollutantDetermined.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollutantDetermined.Location = New System.Drawing.Point(456, 40)
        Me.cboPollutantDetermined.Name = "cboPollutantDetermined"
        Me.cboPollutantDetermined.Size = New System.Drawing.Size(302, 21)
        Me.cboPollutantDetermined.TabIndex = 6
        '
        'cboReportType
        '
        Me.cboReportType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboReportType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReportType.Location = New System.Drawing.Point(115, 113)
        Me.cboReportType.Name = "cboReportType"
        Me.cboReportType.Size = New System.Drawing.Size(136, 21)
        Me.cboReportType.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 117)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(69, 13)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "Report Type:"
        '
        'cboComplianceManager
        '
        Me.cboComplianceManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboComplianceManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboComplianceManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComplianceManager.Location = New System.Drawing.Point(639, 261)
        Me.cboComplianceManager.Name = "cboComplianceManager"
        Me.cboComplianceManager.Size = New System.Drawing.Size(119, 21)
        Me.cboComplianceManager.TabIndex = 12
        '
        'DTPTestDateComplete
        '
        Me.DTPTestDateComplete.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestDateComplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPTestDateComplete.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestDateComplete.Location = New System.Drawing.Point(639, 138)
        Me.DTPTestDateComplete.Name = "DTPTestDateComplete"
        Me.DTPTestDateComplete.ShowCheckBox = True
        Me.DTPTestDateComplete.Size = New System.Drawing.Size(119, 22)
        Me.DTPTestDateComplete.TabIndex = 11
        Me.DTPTestDateComplete.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'DTPTestDateStart
        '
        Me.DTPTestDateStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestDateStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPTestDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestDateStart.Location = New System.Drawing.Point(516, 138)
        Me.DTPTestDateStart.Name = "DTPTestDateStart"
        Me.DTPTestDateStart.ShowCheckBox = True
        Me.DTPTestDateStart.Size = New System.Drawing.Size(117, 22)
        Me.DTPTestDateStart.TabIndex = 10
        Me.DTPTestDateStart.Value = New Date(2005, 8, 18, 0, 0, 0, 0)
        '
        'cboReviewingEngineer
        '
        Me.cboReviewingEngineer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboReviewingEngineer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboReviewingEngineer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReviewingEngineer.Location = New System.Drawing.Point(114, 139)
        Me.cboReviewingEngineer.Name = "cboReviewingEngineer"
        Me.cboReviewingEngineer.Size = New System.Drawing.Size(186, 21)
        Me.cboReviewingEngineer.TabIndex = 1
        '
        'cboUnitManager
        '
        Me.cboUnitManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboUnitManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUnitManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnitManager.Location = New System.Drawing.Point(133, 214)
        Me.cboUnitManager.Name = "cboUnitManager"
        Me.cboUnitManager.Size = New System.Drawing.Size(169, 21)
        Me.cboUnitManager.TabIndex = 2
        '
        'cboISMPUnit
        '
        Me.cboISMPUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboISMPUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboISMPUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboISMPUnit.Location = New System.Drawing.Point(115, 165)
        Me.cboISMPUnit.Name = "cboISMPUnit"
        Me.cboISMPUnit.Size = New System.Drawing.Size(185, 21)
        Me.cboISMPUnit.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(10, 169)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(58, 13)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "ISMP Unit:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(523, 265)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(110, 13)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "Compliance Manager:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(10, 217)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(103, 13)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "ISMP Unit Manager:"
        '
        'txtProgramManager
        '
        Me.txtProgramManager.BackColor = System.Drawing.SystemColors.Control
        Me.txtProgramManager.Location = New System.Drawing.Point(133, 190)
        Me.txtProgramManager.Name = "txtProgramManager"
        Me.txtProgramManager.ReadOnly = True
        Me.txtProgramManager.Size = New System.Drawing.Size(169, 20)
        Me.txtProgramManager.TabIndex = 28
        Me.txtProgramManager.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 194)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(123, 13)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "ISMP Program Manager:"
        '
        'cboMethodDetermined
        '
        Me.cboMethodDetermined.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMethodDetermined.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMethodDetermined.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMethodDetermined.Location = New System.Drawing.Point(482, 64)
        Me.cboMethodDetermined.Name = "cboMethodDetermined"
        Me.cboMethodDetermined.Size = New System.Drawing.Size(276, 21)
        Me.cboMethodDetermined.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(341, 67)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(135, 13)
        Me.Label12.TabIndex = 52
        Me.Label12.Text = "Method used to Determine:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 290)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(147, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Other Witnessing Engineer(s):"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtCompleteDate
        '
        Me.txtCompleteDate.BackColor = System.Drawing.SystemColors.Control
        Me.txtCompleteDate.Location = New System.Drawing.Point(661, 237)
        Me.txtCompleteDate.Name = "txtCompleteDate"
        Me.txtCompleteDate.ReadOnly = True
        Me.txtCompleteDate.Size = New System.Drawing.Size(97, 20)
        Me.txtCompleteDate.TabIndex = 36
        Me.txtCompleteDate.TabStop = False
        '
        'txtReceivedByAPB
        '
        Me.txtReceivedByAPB.BackColor = System.Drawing.SystemColors.Control
        Me.txtReceivedByAPB.Location = New System.Drawing.Point(661, 190)
        Me.txtReceivedByAPB.Name = "txtReceivedByAPB"
        Me.txtReceivedByAPB.ReadOnly = True
        Me.txtReceivedByAPB.Size = New System.Drawing.Size(97, 20)
        Me.txtReceivedByAPB.TabIndex = 38
        Me.txtReceivedByAPB.TabStop = False
        '
        'cboWitnessingEngineer
        '
        Me.cboWitnessingEngineer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboWitnessingEngineer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboWitnessingEngineer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWitnessingEngineer.Location = New System.Drawing.Point(135, 263)
        Me.cboWitnessingEngineer.Name = "cboWitnessingEngineer"
        Me.cboWitnessingEngineer.Size = New System.Drawing.Size(167, 21)
        Me.cboWitnessingEngineer.TabIndex = 4
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(518, 194)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(137, 13)
        Me.Label27.TabIndex = 14
        Me.Label27.Text = "Received by APB by ISMP:"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(12, 267)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(99, 13)
        Me.Label28.TabIndex = 9
        Me.Label28.Text = "Test Witnessed By:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtFacilityState
        '
        Me.txtFacilityState.BackColor = System.Drawing.SystemColors.Control
        Me.txtFacilityState.Location = New System.Drawing.Point(286, 92)
        Me.txtFacilityState.Name = "txtFacilityState"
        Me.txtFacilityState.ReadOnly = True
        Me.txtFacilityState.Size = New System.Drawing.Size(32, 20)
        Me.txtFacilityState.TabIndex = 20
        Me.txtFacilityState.TabStop = False
        Me.txtFacilityState.Visible = False
        '
        'txtSourceTested
        '
        Me.txtSourceTested.BackColor = System.Drawing.SystemColors.Window
        Me.txtSourceTested.Location = New System.Drawing.Point(427, 16)
        Me.txtSourceTested.MaxLength = 100
        Me.txtSourceTested.Name = "txtSourceTested"
        Me.txtSourceTested.Size = New System.Drawing.Size(331, 20)
        Me.txtSourceTested.TabIndex = 5
        '
        'txtFacilityCity
        '
        Me.txtFacilityCity.BackColor = System.Drawing.SystemColors.Control
        Me.txtFacilityCity.Location = New System.Drawing.Point(115, 92)
        Me.txtFacilityCity.Name = "txtFacilityCity"
        Me.txtFacilityCity.ReadOnly = True
        Me.txtFacilityCity.Size = New System.Drawing.Size(167, 20)
        Me.txtFacilityCity.TabIndex = 24
        Me.txtFacilityCity.TabStop = False
        '
        'txtFacilityName
        '
        Me.txtFacilityName.BackColor = System.Drawing.SystemColors.Control
        Me.txtFacilityName.Location = New System.Drawing.Point(114, 68)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(204, 20)
        Me.txtFacilityName.TabIndex = 23
        Me.txtFacilityName.TabStop = False
        '
        'txtAirsNumber
        '
        Me.txtAirsNumber.BackColor = System.Drawing.SystemColors.Control
        Me.txtAirsNumber.Location = New System.Drawing.Point(114, 44)
        Me.txtAirsNumber.Name = "txtAirsNumber"
        Me.txtAirsNumber.ReadOnly = True
        Me.txtAirsNumber.Size = New System.Drawing.Size(70, 20)
        Me.txtAirsNumber.TabIndex = 22
        Me.txtAirsNumber.TabStop = False
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.BackColor = System.Drawing.SystemColors.Control
        Me.txtReferenceNumber.Location = New System.Drawing.Point(114, 20)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.ReadOnly = True
        Me.txtReferenceNumber.Size = New System.Drawing.Size(70, 20)
        Me.txtReferenceNumber.TabIndex = 21
        Me.txtReferenceNumber.TabStop = False
        '
        'lblDatesTested
        '
        Me.lblDatesTested.AutoSize = True
        Me.lblDatesTested.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDatesTested.Location = New System.Drawing.Point(428, 143)
        Me.lblDatesTested.Name = "lblDatesTested"
        Me.lblDatesTested.Size = New System.Drawing.Size(80, 13)
        Me.lblDatesTested.TabIndex = 48
        Me.lblDatesTested.Text = "Date(s) Tested:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(341, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Source Tested:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 92)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Location:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Facility Name:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(341, 43)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(108, 13)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Pollutant Determined:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 147)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 13)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "ISMP Reviewer:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "AIRS Number:"
        '
        'labReferenceNumber
        '
        Me.labReferenceNumber.AutoSize = True
        Me.labReferenceNumber.Location = New System.Drawing.Point(9, 20)
        Me.labReferenceNumber.Name = "labReferenceNumber"
        Me.labReferenceNumber.Size = New System.Drawing.Size(100, 13)
        Me.labReferenceNumber.TabIndex = 0
        Me.labReferenceNumber.Text = "Reference Number:"
        Me.labReferenceNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TCDocumentTypes
        '
        Me.TCDocumentTypes.Controls.Add(Me.TPOneStack)
        Me.TCDocumentTypes.Controls.Add(Me.TPLoadingRack)
        Me.TCDocumentTypes.Controls.Add(Me.TPPondTreatment)
        Me.TCDocumentTypes.Controls.Add(Me.TPGasConcentration)
        Me.TCDocumentTypes.Controls.Add(Me.TPFlare)
        Me.TCDocumentTypes.Controls.Add(Me.TPMethodNine)
        Me.TCDocumentTypes.Controls.Add(Me.TPMemorandum)
        Me.TCDocumentTypes.Controls.Add(Me.TPRata)
        Me.TCDocumentTypes.Controls.Add(Me.TPTwoStack)
        Me.TCDocumentTypes.Controls.Add(Me.TPMethodTwentyTwo)
        Me.TCDocumentTypes.Controls.Add(Me.TPSSCPWork)
        Me.TCDocumentTypes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCDocumentTypes.Location = New System.Drawing.Point(0, 0)
        Me.TCDocumentTypes.Name = "TCDocumentTypes"
        Me.TCDocumentTypes.SelectedIndex = 0
        Me.TCDocumentTypes.Size = New System.Drawing.Size(790, 314)
        Me.TCDocumentTypes.TabIndex = 1
        '
        'TPOneStack
        '
        Me.TPOneStack.AutoScroll = True
        Me.TPOneStack.Controls.Add(Me.Label22)
        Me.TPOneStack.Controls.Add(Me.txtOtherInformationOneStack)
        Me.TPOneStack.Controls.Add(Me.Label24)
        Me.TPOneStack.Controls.Add(Me.TCOneStack)
        Me.TPOneStack.Controls.Add(Me.txtControlEquipmentOperatingDataOneStack)
        Me.TPOneStack.Controls.Add(Me.txtApplicableRegulationOneStack)
        Me.TPOneStack.Controls.Add(Me.txtOperatingCapacityOneStack)
        Me.TPOneStack.Controls.Add(Me.Label50)
        Me.TPOneStack.Controls.Add(Me.txtAllowableEmissionRate2OneStack)
        Me.TPOneStack.Controls.Add(Me.txtAllowableEmissionRate3OneStack)
        Me.TPOneStack.Controls.Add(Me.txtAllowableEmissionRate1OneStack)
        Me.TPOneStack.Controls.Add(Me.txtMaximumExpectedOperatingCapacityOneStack)
        Me.TPOneStack.Controls.Add(Me.Label51)
        Me.TPOneStack.Controls.Add(Me.Label52)
        Me.TPOneStack.Controls.Add(Me.cboOperatingCapacityUnitsOneStack)
        Me.TPOneStack.Controls.Add(Me.cboAllowableEmissionRateUnits2OneStack)
        Me.TPOneStack.Controls.Add(Me.cboAllowableEmissionRateUnits3OneStack)
        Me.TPOneStack.Controls.Add(Me.cboAllowableEmissionRateUnits1OneStack)
        Me.TPOneStack.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsOneStack)
        Me.TPOneStack.Controls.Add(Me.Label53)
        Me.TPOneStack.Controls.Add(Me.Label54)
        Me.TPOneStack.Controls.Add(Me.txtPercentAllowableOneStack)
        Me.TPOneStack.Controls.Add(Me.Label55)
        Me.TPOneStack.Controls.Add(Me.Label193)
        Me.TPOneStack.Controls.Add(Me.Label197)
        Me.TPOneStack.Controls.Add(Me.Label198)
        Me.TPOneStack.Controls.Add(Me.Label199)
        Me.TPOneStack.Location = New System.Drawing.Point(4, 22)
        Me.TPOneStack.Name = "TPOneStack"
        Me.TPOneStack.Padding = New System.Windows.Forms.Padding(3)
        Me.TPOneStack.Size = New System.Drawing.Size(782, 288)
        Me.TPOneStack.TabIndex = 0
        Me.TPOneStack.Text = "One Stack"
        Me.TPOneStack.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Location = New System.Drawing.Point(6, 347)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(212, 1)
        Me.Label22.TabIndex = 776
        '
        'txtOtherInformationOneStack
        '
        Me.txtOtherInformationOneStack.AcceptsReturn = True
        Me.txtOtherInformationOneStack.Location = New System.Drawing.Point(138, 348)
        Me.txtOtherInformationOneStack.Multiline = True
        Me.txtOtherInformationOneStack.Name = "txtOtherInformationOneStack"
        Me.txtOtherInformationOneStack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationOneStack.Size = New System.Drawing.Size(616, 80)
        Me.txtOtherInformationOneStack.TabIndex = 121
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 348)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(91, 13)
        Me.Label24.TabIndex = 771
        Me.Label24.Text = "Other Information:"
        '
        'TCOneStack
        '
        Me.TCOneStack.Controls.Add(Me.TPOneStackTwoRun)
        Me.TCOneStack.Controls.Add(Me.TPOneStackThreeRun)
        Me.TCOneStack.Controls.Add(Me.TPOneStackFourRun)
        Me.TCOneStack.Location = New System.Drawing.Point(2, 126)
        Me.TCOneStack.Name = "TCOneStack"
        Me.TCOneStack.SelectedIndex = 0
        Me.TCOneStack.Size = New System.Drawing.Size(782, 190)
        Me.TCOneStack.TabIndex = 767
        Me.TCOneStack.TabStop = False
        '
        'TPOneStackTwoRun
        '
        Me.TPOneStackTwoRun.Controls.Add(Me.Label163)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label162)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label161)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label160)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label159)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label158)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label152)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label42)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtEmissRateAvgOneStackTwoRun)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtPollConcAvgOneStackTwoRun)
        Me.TPOneStackTwoRun.Controls.Add(Me.cboEmissRateUnitOneStackTwoRun)
        Me.TPOneStackTwoRun.Controls.Add(Me.cboPollConUnitOneStackTwoRun)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtEmissRateOneStackTwoRun1B)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtPollConcOneStackTwoRun1B)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtGasMoistOneStackTwoRun1B)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtGasFlowDSCFMOneStackTwoRun1B)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtGasFlowACFMOneStackTwoRun1B)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtGasTempOneStackTwoRun1B)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtRunNumOneStackTwoRun1B)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtEmissRateOneStackTwoRun1A)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtPollConcOneStackTwoRun1A)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtGasMoistOneStackTwoRun1A)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtGasFlowDSCFMOneStackTwoRun1A)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtGasFlowACFMOneStackTwoRun1A)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtGasTempOneStackTwoRun1A)
        Me.TPOneStackTwoRun.Controls.Add(Me.txtRunNumOneStackTwoRun1A)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label35)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label36)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label37)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label38)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label39)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label40)
        Me.TPOneStackTwoRun.Controls.Add(Me.Label41)
        Me.TPOneStackTwoRun.Controls.Add(Me.btnClearOneStackTwoRun2)
        Me.TPOneStackTwoRun.Controls.Add(Me.btnClearOneStackTwoRun1)
        Me.TPOneStackTwoRun.Location = New System.Drawing.Point(4, 22)
        Me.TPOneStackTwoRun.Name = "TPOneStackTwoRun"
        Me.TPOneStackTwoRun.Size = New System.Drawing.Size(774, 164)
        Me.TPOneStackTwoRun.TabIndex = 2
        Me.TPOneStackTwoRun.Text = "2 Runs"
        '
        'Label163
        '
        Me.Label163.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label163.Location = New System.Drawing.Point(4, 163)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(562, 1)
        Me.Label163.TabIndex = 227
        '
        'Label162
        '
        Me.Label162.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label162.Location = New System.Drawing.Point(4, 141)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(562, 1)
        Me.Label162.TabIndex = 226
        '
        'Label161
        '
        Me.Label161.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label161.Location = New System.Drawing.Point(4, 119)
        Me.Label161.Name = "Label161"
        Me.Label161.Size = New System.Drawing.Size(334, 1)
        Me.Label161.TabIndex = 225
        '
        'Label160
        '
        Me.Label160.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label160.Location = New System.Drawing.Point(4, 97)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(334, 1)
        Me.Label160.TabIndex = 224
        '
        'Label159
        '
        Me.Label159.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label159.Location = New System.Drawing.Point(4, 53)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(334, 1)
        Me.Label159.TabIndex = 223
        '
        'Label158
        '
        Me.Label158.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label158.Location = New System.Drawing.Point(4, 75)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(334, 1)
        Me.Label158.TabIndex = 222
        '
        'Label152
        '
        Me.Label152.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label152.Location = New System.Drawing.Point(36, 28)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(294, 1)
        Me.Label152.TabIndex = 221
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(494, 104)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(65, 13)
        Me.Label42.TabIndex = 217
        Me.Label42.Text = "AVERAGES"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txtEmissRateAvgOneStackTwoRun
        '
        Me.txtEmissRateAvgOneStackTwoRun.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateAvgOneStackTwoRun.Location = New System.Drawing.Point(494, 142)
        Me.txtEmissRateAvgOneStackTwoRun.MaxLength = 11
        Me.txtEmissRateAvgOneStackTwoRun.Name = "txtEmissRateAvgOneStackTwoRun"
        Me.txtEmissRateAvgOneStackTwoRun.Size = New System.Drawing.Size(72, 20)
        Me.txtEmissRateAvgOneStackTwoRun.TabIndex = 49
        Me.txtEmissRateAvgOneStackTwoRun.TabStop = False
        '
        'txtPollConcAvgOneStackTwoRun
        '
        Me.txtPollConcAvgOneStackTwoRun.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgOneStackTwoRun.Location = New System.Drawing.Point(494, 120)
        Me.txtPollConcAvgOneStackTwoRun.MaxLength = 11
        Me.txtPollConcAvgOneStackTwoRun.Name = "txtPollConcAvgOneStackTwoRun"
        Me.txtPollConcAvgOneStackTwoRun.Size = New System.Drawing.Size(72, 20)
        Me.txtPollConcAvgOneStackTwoRun.TabIndex = 45
        Me.txtPollConcAvgOneStackTwoRun.TabStop = False
        '
        'cboEmissRateUnitOneStackTwoRun
        '
        Me.cboEmissRateUnitOneStackTwoRun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEmissRateUnitOneStackTwoRun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEmissRateUnitOneStackTwoRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmissRateUnitOneStackTwoRun.Location = New System.Drawing.Point(348, 142)
        Me.cboEmissRateUnitOneStackTwoRun.Name = "cboEmissRateUnitOneStackTwoRun"
        Me.cboEmissRateUnitOneStackTwoRun.Size = New System.Drawing.Size(136, 21)
        Me.cboEmissRateUnitOneStackTwoRun.TabIndex = 48
        '
        'cboPollConUnitOneStackTwoRun
        '
        Me.cboPollConUnitOneStackTwoRun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitOneStackTwoRun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitOneStackTwoRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollConUnitOneStackTwoRun.Location = New System.Drawing.Point(348, 120)
        Me.cboPollConUnitOneStackTwoRun.Name = "cboPollConUnitOneStackTwoRun"
        Me.cboPollConUnitOneStackTwoRun.Size = New System.Drawing.Size(136, 21)
        Me.cboPollConUnitOneStackTwoRun.TabIndex = 44
        '
        'txtEmissRateOneStackTwoRun1B
        '
        Me.txtEmissRateOneStackTwoRun1B.Location = New System.Drawing.Point(242, 142)
        Me.txtEmissRateOneStackTwoRun1B.MaxLength = 11
        Me.txtEmissRateOneStackTwoRun1B.Name = "txtEmissRateOneStackTwoRun1B"
        Me.txtEmissRateOneStackTwoRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackTwoRun1B.TabIndex = 47
        '
        'txtPollConcOneStackTwoRun1B
        '
        Me.txtPollConcOneStackTwoRun1B.Location = New System.Drawing.Point(242, 120)
        Me.txtPollConcOneStackTwoRun1B.MaxLength = 11
        Me.txtPollConcOneStackTwoRun1B.Name = "txtPollConcOneStackTwoRun1B"
        Me.txtPollConcOneStackTwoRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackTwoRun1B.TabIndex = 43
        '
        'txtGasMoistOneStackTwoRun1B
        '
        Me.txtGasMoistOneStackTwoRun1B.Location = New System.Drawing.Point(242, 54)
        Me.txtGasMoistOneStackTwoRun1B.MaxLength = 11
        Me.txtGasMoistOneStackTwoRun1B.Name = "txtGasMoistOneStackTwoRun1B"
        Me.txtGasMoistOneStackTwoRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackTwoRun1B.TabIndex = 37
        '
        'txtGasFlowDSCFMOneStackTwoRun1B
        '
        Me.txtGasFlowDSCFMOneStackTwoRun1B.Location = New System.Drawing.Point(242, 98)
        Me.txtGasFlowDSCFMOneStackTwoRun1B.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackTwoRun1B.Name = "txtGasFlowDSCFMOneStackTwoRun1B"
        Me.txtGasFlowDSCFMOneStackTwoRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackTwoRun1B.TabIndex = 41
        '
        'txtGasFlowACFMOneStackTwoRun1B
        '
        Me.txtGasFlowACFMOneStackTwoRun1B.Location = New System.Drawing.Point(242, 76)
        Me.txtGasFlowACFMOneStackTwoRun1B.MaxLength = 11
        Me.txtGasFlowACFMOneStackTwoRun1B.Name = "txtGasFlowACFMOneStackTwoRun1B"
        Me.txtGasFlowACFMOneStackTwoRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackTwoRun1B.TabIndex = 39
        '
        'txtGasTempOneStackTwoRun1B
        '
        Me.txtGasTempOneStackTwoRun1B.Location = New System.Drawing.Point(242, 32)
        Me.txtGasTempOneStackTwoRun1B.MaxLength = 11
        Me.txtGasTempOneStackTwoRun1B.Name = "txtGasTempOneStackTwoRun1B"
        Me.txtGasTempOneStackTwoRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackTwoRun1B.TabIndex = 35
        '
        'txtRunNumOneStackTwoRun1B
        '
        Me.txtRunNumOneStackTwoRun1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackTwoRun1B.Location = New System.Drawing.Point(248, 8)
        Me.txtRunNumOneStackTwoRun1B.MaxLength = 3
        Me.txtRunNumOneStackTwoRun1B.Name = "txtRunNumOneStackTwoRun1B"
        Me.txtRunNumOneStackTwoRun1B.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackTwoRun1B.TabIndex = 33
        '
        'txtEmissRateOneStackTwoRun1A
        '
        Me.txtEmissRateOneStackTwoRun1A.Location = New System.Drawing.Point(136, 142)
        Me.txtEmissRateOneStackTwoRun1A.MaxLength = 11
        Me.txtEmissRateOneStackTwoRun1A.Name = "txtEmissRateOneStackTwoRun1A"
        Me.txtEmissRateOneStackTwoRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackTwoRun1A.TabIndex = 46
        '
        'txtPollConcOneStackTwoRun1A
        '
        Me.txtPollConcOneStackTwoRun1A.Location = New System.Drawing.Point(136, 120)
        Me.txtPollConcOneStackTwoRun1A.MaxLength = 11
        Me.txtPollConcOneStackTwoRun1A.Name = "txtPollConcOneStackTwoRun1A"
        Me.txtPollConcOneStackTwoRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackTwoRun1A.TabIndex = 42
        '
        'txtGasMoistOneStackTwoRun1A
        '
        Me.txtGasMoistOneStackTwoRun1A.Location = New System.Drawing.Point(136, 54)
        Me.txtGasMoistOneStackTwoRun1A.MaxLength = 11
        Me.txtGasMoistOneStackTwoRun1A.Name = "txtGasMoistOneStackTwoRun1A"
        Me.txtGasMoistOneStackTwoRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackTwoRun1A.TabIndex = 36
        '
        'txtGasFlowDSCFMOneStackTwoRun1A
        '
        Me.txtGasFlowDSCFMOneStackTwoRun1A.Location = New System.Drawing.Point(136, 98)
        Me.txtGasFlowDSCFMOneStackTwoRun1A.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackTwoRun1A.Name = "txtGasFlowDSCFMOneStackTwoRun1A"
        Me.txtGasFlowDSCFMOneStackTwoRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackTwoRun1A.TabIndex = 40
        '
        'txtGasFlowACFMOneStackTwoRun1A
        '
        Me.txtGasFlowACFMOneStackTwoRun1A.Location = New System.Drawing.Point(136, 76)
        Me.txtGasFlowACFMOneStackTwoRun1A.MaxLength = 11
        Me.txtGasFlowACFMOneStackTwoRun1A.Name = "txtGasFlowACFMOneStackTwoRun1A"
        Me.txtGasFlowACFMOneStackTwoRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackTwoRun1A.TabIndex = 38
        '
        'txtGasTempOneStackTwoRun1A
        '
        Me.txtGasTempOneStackTwoRun1A.Location = New System.Drawing.Point(136, 32)
        Me.txtGasTempOneStackTwoRun1A.MaxLength = 11
        Me.txtGasTempOneStackTwoRun1A.Name = "txtGasTempOneStackTwoRun1A"
        Me.txtGasTempOneStackTwoRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackTwoRun1A.TabIndex = 34
        '
        'txtRunNumOneStackTwoRun1A
        '
        Me.txtRunNumOneStackTwoRun1A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackTwoRun1A.Location = New System.Drawing.Point(142, 8)
        Me.txtRunNumOneStackTwoRun1A.MaxLength = 3
        Me.txtRunNumOneStackTwoRun1A.Name = "txtRunNumOneStackTwoRun1A"
        Me.txtRunNumOneStackTwoRun1A.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackTwoRun1A.TabIndex = 32
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(4, 54)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(86, 13)
        Me.Label35.TabIndex = 188
        Me.Label35.Text = "Gas Moisture (%)"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(4, 76)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(115, 13)
        Me.Label36.TabIndex = 187
        Me.Label36.Text = "Gas Flow Rate (ACFM)"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(4, 98)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(123, 13)
        Me.Label37.TabIndex = 186
        Me.Label37.Text = "Gas Flow Rate (DSCFM)"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(4, 120)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(120, 13)
        Me.Label38.TabIndex = 185
        Me.Label38.Text = "Pollutant Concentration:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(4, 142)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(77, 13)
        Me.Label39.TabIndex = 184
        Me.Label39.Text = "Emission Rate:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(4, 32)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(107, 13)
        Me.Label40.TabIndex = 183
        Me.Label40.Text = "Gas Temperature (F):"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(36, 8)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(61, 13)
        Me.Label41.TabIndex = 181
        Me.Label41.Text = "Test Run #"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'btnClearOneStackTwoRun2
        '
        Me.btnClearOneStackTwoRun2.Location = New System.Drawing.Point(290, 8)
        Me.btnClearOneStackTwoRun2.Name = "btnClearOneStackTwoRun2"
        Me.btnClearOneStackTwoRun2.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackTwoRun2.TabIndex = 190
        Me.btnClearOneStackTwoRun2.TabStop = False
        Me.btnClearOneStackTwoRun2.Text = "Clear"
        '
        'btnClearOneStackTwoRun1
        '
        Me.btnClearOneStackTwoRun1.Location = New System.Drawing.Point(184, 8)
        Me.btnClearOneStackTwoRun1.Name = "btnClearOneStackTwoRun1"
        Me.btnClearOneStackTwoRun1.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackTwoRun1.TabIndex = 189
        Me.btnClearOneStackTwoRun1.TabStop = False
        Me.btnClearOneStackTwoRun1.Text = "Clear"
        '
        'TPOneStackThreeRun
        '
        Me.TPOneStackThreeRun.Controls.Add(Me.Label206)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label205)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label204)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label203)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label202)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label201)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label200)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label26)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtEmissRateAvgOneStackThreeRun)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtPollConcAvgOneStackThreeRun)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtEmissRateOneStackThreeRun1C)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtPollConcOneStackThreeRun1C)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasMoistOneStackThreeRun1C)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasFlowDSCFMOneStackThreeRun1C)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasFlowACFMOneStackThreeRun1C)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasTempOneStackThreeRun1C)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtRunNumOneStackThreeRun1C)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtEmissRateOneStackThreeRun1B)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtPollConcOneStackThreeRun1B)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasMoistOneStackThreeRun1B)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasFlowDSCFMOneStackThreeRun1B)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasFlowACFMOneStackThreeRun1B)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasTempOneStackThreeRun1B)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtRunNumOneStackThreeRun1B)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtEmissRateOneStackThreeRun1A)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtPollConcOneStackThreeRun1A)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasMoistOneStackThreeRun1A)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasFlowDSCFMOneStackThreeRun1A)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasFlowACFMOneStackThreeRun1A)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtGasTempOneStackThreeRun1A)
        Me.TPOneStackThreeRun.Controls.Add(Me.txtRunNumOneStackThreeRun1A)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label23)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label25)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label29)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label30)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label31)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label32)
        Me.TPOneStackThreeRun.Controls.Add(Me.Label33)
        Me.TPOneStackThreeRun.Controls.Add(Me.cboEmissRateUnitOneStackThreeRun)
        Me.TPOneStackThreeRun.Controls.Add(Me.cboPollConUnitOneStackThreeRun)
        Me.TPOneStackThreeRun.Controls.Add(Me.btnClearOneStackThreeRun3)
        Me.TPOneStackThreeRun.Controls.Add(Me.btnClearOneStackThreeRun2)
        Me.TPOneStackThreeRun.Controls.Add(Me.btnClearOneStackThreeRun1)
        Me.TPOneStackThreeRun.Location = New System.Drawing.Point(4, 22)
        Me.TPOneStackThreeRun.Name = "TPOneStackThreeRun"
        Me.TPOneStackThreeRun.Size = New System.Drawing.Size(774, 164)
        Me.TPOneStackThreeRun.TabIndex = 0
        Me.TPOneStackThreeRun.Text = "3 Runs"
        '
        'Label206
        '
        Me.Label206.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label206.Location = New System.Drawing.Point(4, 163)
        Me.Label206.Name = "Label206"
        Me.Label206.Size = New System.Drawing.Size(668, 1)
        Me.Label206.TabIndex = 228
        '
        'Label205
        '
        Me.Label205.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label205.Location = New System.Drawing.Point(4, 141)
        Me.Label205.Name = "Label205"
        Me.Label205.Size = New System.Drawing.Size(668, 1)
        Me.Label205.TabIndex = 227
        '
        'Label204
        '
        Me.Label204.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label204.Location = New System.Drawing.Point(4, 119)
        Me.Label204.Name = "Label204"
        Me.Label204.Size = New System.Drawing.Size(440, 1)
        Me.Label204.TabIndex = 226
        '
        'Label203
        '
        Me.Label203.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label203.Location = New System.Drawing.Point(4, 97)
        Me.Label203.Name = "Label203"
        Me.Label203.Size = New System.Drawing.Size(440, 1)
        Me.Label203.TabIndex = 225
        '
        'Label202
        '
        Me.Label202.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label202.Location = New System.Drawing.Point(4, 75)
        Me.Label202.Name = "Label202"
        Me.Label202.Size = New System.Drawing.Size(440, 1)
        Me.Label202.TabIndex = 224
        '
        'Label201
        '
        Me.Label201.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label201.Location = New System.Drawing.Point(4, 53)
        Me.Label201.Name = "Label201"
        Me.Label201.Size = New System.Drawing.Size(440, 1)
        Me.Label201.TabIndex = 223
        '
        'Label200
        '
        Me.Label200.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label200.Location = New System.Drawing.Point(36, 28)
        Me.Label200.Name = "Label200"
        Me.Label200.Size = New System.Drawing.Size(400, 1)
        Me.Label200.TabIndex = 222
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(600, 104)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(65, 13)
        Me.Label26.TabIndex = 148
        Me.Label26.Text = "AVERAGES"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txtEmissRateAvgOneStackThreeRun
        '
        Me.txtEmissRateAvgOneStackThreeRun.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateAvgOneStackThreeRun.Location = New System.Drawing.Point(600, 142)
        Me.txtEmissRateAvgOneStackThreeRun.MaxLength = 11
        Me.txtEmissRateAvgOneStackThreeRun.Name = "txtEmissRateAvgOneStackThreeRun"
        Me.txtEmissRateAvgOneStackThreeRun.ReadOnly = True
        Me.txtEmissRateAvgOneStackThreeRun.Size = New System.Drawing.Size(72, 20)
        Me.txtEmissRateAvgOneStackThreeRun.TabIndex = 74
        Me.txtEmissRateAvgOneStackThreeRun.TabStop = False
        '
        'txtPollConcAvgOneStackThreeRun
        '
        Me.txtPollConcAvgOneStackThreeRun.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgOneStackThreeRun.Location = New System.Drawing.Point(600, 120)
        Me.txtPollConcAvgOneStackThreeRun.MaxLength = 11
        Me.txtPollConcAvgOneStackThreeRun.Name = "txtPollConcAvgOneStackThreeRun"
        Me.txtPollConcAvgOneStackThreeRun.ReadOnly = True
        Me.txtPollConcAvgOneStackThreeRun.Size = New System.Drawing.Size(72, 20)
        Me.txtPollConcAvgOneStackThreeRun.TabIndex = 69
        Me.txtPollConcAvgOneStackThreeRun.TabStop = False
        '
        'txtEmissRateOneStackThreeRun1C
        '
        Me.txtEmissRateOneStackThreeRun1C.Location = New System.Drawing.Point(348, 142)
        Me.txtEmissRateOneStackThreeRun1C.MaxLength = 11
        Me.txtEmissRateOneStackThreeRun1C.Name = "txtEmissRateOneStackThreeRun1C"
        Me.txtEmissRateOneStackThreeRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackThreeRun1C.TabIndex = 72
        '
        'txtPollConcOneStackThreeRun1C
        '
        Me.txtPollConcOneStackThreeRun1C.Location = New System.Drawing.Point(348, 120)
        Me.txtPollConcOneStackThreeRun1C.MaxLength = 11
        Me.txtPollConcOneStackThreeRun1C.Name = "txtPollConcOneStackThreeRun1C"
        Me.txtPollConcOneStackThreeRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackThreeRun1C.TabIndex = 67
        '
        'txtGasMoistOneStackThreeRun1C
        '
        Me.txtGasMoistOneStackThreeRun1C.Location = New System.Drawing.Point(348, 54)
        Me.txtGasMoistOneStackThreeRun1C.MaxLength = 11
        Me.txtGasMoistOneStackThreeRun1C.Name = "txtGasMoistOneStackThreeRun1C"
        Me.txtGasMoistOneStackThreeRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackThreeRun1C.TabIndex = 58
        '
        'txtGasFlowDSCFMOneStackThreeRun1C
        '
        Me.txtGasFlowDSCFMOneStackThreeRun1C.Location = New System.Drawing.Point(348, 98)
        Me.txtGasFlowDSCFMOneStackThreeRun1C.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackThreeRun1C.Name = "txtGasFlowDSCFMOneStackThreeRun1C"
        Me.txtGasFlowDSCFMOneStackThreeRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackThreeRun1C.TabIndex = 64
        '
        'txtGasFlowACFMOneStackThreeRun1C
        '
        Me.txtGasFlowACFMOneStackThreeRun1C.Location = New System.Drawing.Point(348, 76)
        Me.txtGasFlowACFMOneStackThreeRun1C.MaxLength = 11
        Me.txtGasFlowACFMOneStackThreeRun1C.Name = "txtGasFlowACFMOneStackThreeRun1C"
        Me.txtGasFlowACFMOneStackThreeRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackThreeRun1C.TabIndex = 61
        '
        'txtGasTempOneStackThreeRun1C
        '
        Me.txtGasTempOneStackThreeRun1C.Location = New System.Drawing.Point(348, 32)
        Me.txtGasTempOneStackThreeRun1C.MaxLength = 11
        Me.txtGasTempOneStackThreeRun1C.Name = "txtGasTempOneStackThreeRun1C"
        Me.txtGasTempOneStackThreeRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackThreeRun1C.TabIndex = 55
        '
        'txtRunNumOneStackThreeRun1C
        '
        Me.txtRunNumOneStackThreeRun1C.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackThreeRun1C.Location = New System.Drawing.Point(354, 8)
        Me.txtRunNumOneStackThreeRun1C.MaxLength = 3
        Me.txtRunNumOneStackThreeRun1C.Name = "txtRunNumOneStackThreeRun1C"
        Me.txtRunNumOneStackThreeRun1C.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackThreeRun1C.TabIndex = 52
        '
        'txtEmissRateOneStackThreeRun1B
        '
        Me.txtEmissRateOneStackThreeRun1B.Location = New System.Drawing.Point(242, 142)
        Me.txtEmissRateOneStackThreeRun1B.MaxLength = 11
        Me.txtEmissRateOneStackThreeRun1B.Name = "txtEmissRateOneStackThreeRun1B"
        Me.txtEmissRateOneStackThreeRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackThreeRun1B.TabIndex = 71
        '
        'txtPollConcOneStackThreeRun1B
        '
        Me.txtPollConcOneStackThreeRun1B.Location = New System.Drawing.Point(242, 120)
        Me.txtPollConcOneStackThreeRun1B.MaxLength = 11
        Me.txtPollConcOneStackThreeRun1B.Name = "txtPollConcOneStackThreeRun1B"
        Me.txtPollConcOneStackThreeRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackThreeRun1B.TabIndex = 66
        '
        'txtGasMoistOneStackThreeRun1B
        '
        Me.txtGasMoistOneStackThreeRun1B.Location = New System.Drawing.Point(242, 54)
        Me.txtGasMoistOneStackThreeRun1B.MaxLength = 11
        Me.txtGasMoistOneStackThreeRun1B.Name = "txtGasMoistOneStackThreeRun1B"
        Me.txtGasMoistOneStackThreeRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackThreeRun1B.TabIndex = 57
        '
        'txtGasFlowDSCFMOneStackThreeRun1B
        '
        Me.txtGasFlowDSCFMOneStackThreeRun1B.Location = New System.Drawing.Point(242, 98)
        Me.txtGasFlowDSCFMOneStackThreeRun1B.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackThreeRun1B.Name = "txtGasFlowDSCFMOneStackThreeRun1B"
        Me.txtGasFlowDSCFMOneStackThreeRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackThreeRun1B.TabIndex = 63
        '
        'txtGasFlowACFMOneStackThreeRun1B
        '
        Me.txtGasFlowACFMOneStackThreeRun1B.Location = New System.Drawing.Point(242, 76)
        Me.txtGasFlowACFMOneStackThreeRun1B.MaxLength = 11
        Me.txtGasFlowACFMOneStackThreeRun1B.Name = "txtGasFlowACFMOneStackThreeRun1B"
        Me.txtGasFlowACFMOneStackThreeRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackThreeRun1B.TabIndex = 60
        '
        'txtGasTempOneStackThreeRun1B
        '
        Me.txtGasTempOneStackThreeRun1B.Location = New System.Drawing.Point(242, 32)
        Me.txtGasTempOneStackThreeRun1B.MaxLength = 11
        Me.txtGasTempOneStackThreeRun1B.Name = "txtGasTempOneStackThreeRun1B"
        Me.txtGasTempOneStackThreeRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackThreeRun1B.TabIndex = 54
        '
        'txtRunNumOneStackThreeRun1B
        '
        Me.txtRunNumOneStackThreeRun1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackThreeRun1B.Location = New System.Drawing.Point(248, 8)
        Me.txtRunNumOneStackThreeRun1B.MaxLength = 3
        Me.txtRunNumOneStackThreeRun1B.Name = "txtRunNumOneStackThreeRun1B"
        Me.txtRunNumOneStackThreeRun1B.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackThreeRun1B.TabIndex = 51
        '
        'txtEmissRateOneStackThreeRun1A
        '
        Me.txtEmissRateOneStackThreeRun1A.Location = New System.Drawing.Point(136, 142)
        Me.txtEmissRateOneStackThreeRun1A.MaxLength = 11
        Me.txtEmissRateOneStackThreeRun1A.Name = "txtEmissRateOneStackThreeRun1A"
        Me.txtEmissRateOneStackThreeRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackThreeRun1A.TabIndex = 70
        '
        'txtPollConcOneStackThreeRun1A
        '
        Me.txtPollConcOneStackThreeRun1A.Location = New System.Drawing.Point(136, 120)
        Me.txtPollConcOneStackThreeRun1A.MaxLength = 11
        Me.txtPollConcOneStackThreeRun1A.Name = "txtPollConcOneStackThreeRun1A"
        Me.txtPollConcOneStackThreeRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackThreeRun1A.TabIndex = 65
        '
        'txtGasMoistOneStackThreeRun1A
        '
        Me.txtGasMoistOneStackThreeRun1A.Location = New System.Drawing.Point(136, 54)
        Me.txtGasMoistOneStackThreeRun1A.MaxLength = 11
        Me.txtGasMoistOneStackThreeRun1A.Name = "txtGasMoistOneStackThreeRun1A"
        Me.txtGasMoistOneStackThreeRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackThreeRun1A.TabIndex = 56
        '
        'txtGasFlowDSCFMOneStackThreeRun1A
        '
        Me.txtGasFlowDSCFMOneStackThreeRun1A.Location = New System.Drawing.Point(136, 98)
        Me.txtGasFlowDSCFMOneStackThreeRun1A.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackThreeRun1A.Name = "txtGasFlowDSCFMOneStackThreeRun1A"
        Me.txtGasFlowDSCFMOneStackThreeRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackThreeRun1A.TabIndex = 62
        '
        'txtGasFlowACFMOneStackThreeRun1A
        '
        Me.txtGasFlowACFMOneStackThreeRun1A.Location = New System.Drawing.Point(136, 76)
        Me.txtGasFlowACFMOneStackThreeRun1A.MaxLength = 11
        Me.txtGasFlowACFMOneStackThreeRun1A.Name = "txtGasFlowACFMOneStackThreeRun1A"
        Me.txtGasFlowACFMOneStackThreeRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackThreeRun1A.TabIndex = 59
        '
        'txtGasTempOneStackThreeRun1A
        '
        Me.txtGasTempOneStackThreeRun1A.Location = New System.Drawing.Point(136, 32)
        Me.txtGasTempOneStackThreeRun1A.MaxLength = 11
        Me.txtGasTempOneStackThreeRun1A.Name = "txtGasTempOneStackThreeRun1A"
        Me.txtGasTempOneStackThreeRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackThreeRun1A.TabIndex = 53
        '
        'txtRunNumOneStackThreeRun1A
        '
        Me.txtRunNumOneStackThreeRun1A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackThreeRun1A.Location = New System.Drawing.Point(142, 8)
        Me.txtRunNumOneStackThreeRun1A.MaxLength = 3
        Me.txtRunNumOneStackThreeRun1A.Name = "txtRunNumOneStackThreeRun1A"
        Me.txtRunNumOneStackThreeRun1A.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackThreeRun1A.TabIndex = 50
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(4, 54)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(86, 13)
        Me.Label23.TabIndex = 142
        Me.Label23.Text = "Gas Moisture (%)"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(4, 76)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(115, 13)
        Me.Label25.TabIndex = 141
        Me.Label25.Text = "Gas Flow Rate (ACFM)"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(4, 98)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(123, 13)
        Me.Label29.TabIndex = 140
        Me.Label29.Text = "Gas Flow Rate (DSCFM)"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(4, 120)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(120, 13)
        Me.Label30.TabIndex = 138
        Me.Label30.Text = "Pollutant Concentration:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(4, 142)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(77, 13)
        Me.Label31.TabIndex = 136
        Me.Label31.Text = "Emission Rate:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(4, 32)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(107, 13)
        Me.Label32.TabIndex = 135
        Me.Label32.Text = "Gas Temperature (F):"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(36, 8)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(61, 13)
        Me.Label33.TabIndex = 132
        Me.Label33.Text = "Test Run #"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboEmissRateUnitOneStackThreeRun
        '
        Me.cboEmissRateUnitOneStackThreeRun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEmissRateUnitOneStackThreeRun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEmissRateUnitOneStackThreeRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmissRateUnitOneStackThreeRun.Location = New System.Drawing.Point(454, 142)
        Me.cboEmissRateUnitOneStackThreeRun.Name = "cboEmissRateUnitOneStackThreeRun"
        Me.cboEmissRateUnitOneStackThreeRun.Size = New System.Drawing.Size(136, 21)
        Me.cboEmissRateUnitOneStackThreeRun.TabIndex = 73
        '
        'cboPollConUnitOneStackThreeRun
        '
        Me.cboPollConUnitOneStackThreeRun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitOneStackThreeRun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitOneStackThreeRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollConUnitOneStackThreeRun.Location = New System.Drawing.Point(454, 120)
        Me.cboPollConUnitOneStackThreeRun.Name = "cboPollConUnitOneStackThreeRun"
        Me.cboPollConUnitOneStackThreeRun.Size = New System.Drawing.Size(136, 21)
        Me.cboPollConUnitOneStackThreeRun.TabIndex = 68
        '
        'btnClearOneStackThreeRun3
        '
        Me.btnClearOneStackThreeRun3.Location = New System.Drawing.Point(396, 8)
        Me.btnClearOneStackThreeRun3.Name = "btnClearOneStackThreeRun3"
        Me.btnClearOneStackThreeRun3.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackThreeRun3.TabIndex = 168
        Me.btnClearOneStackThreeRun3.TabStop = False
        Me.btnClearOneStackThreeRun3.Text = "Clear"
        '
        'btnClearOneStackThreeRun2
        '
        Me.btnClearOneStackThreeRun2.Location = New System.Drawing.Point(290, 8)
        Me.btnClearOneStackThreeRun2.Name = "btnClearOneStackThreeRun2"
        Me.btnClearOneStackThreeRun2.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackThreeRun2.TabIndex = 167
        Me.btnClearOneStackThreeRun2.TabStop = False
        Me.btnClearOneStackThreeRun2.Text = "Clear"
        '
        'btnClearOneStackThreeRun1
        '
        Me.btnClearOneStackThreeRun1.Location = New System.Drawing.Point(184, 8)
        Me.btnClearOneStackThreeRun1.Name = "btnClearOneStackThreeRun1"
        Me.btnClearOneStackThreeRun1.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackThreeRun1.TabIndex = 166
        Me.btnClearOneStackThreeRun1.TabStop = False
        Me.btnClearOneStackThreeRun1.Text = "Clear"
        '
        'TPOneStackFourRun
        '
        Me.TPOneStackFourRun.Controls.Add(Me.Label213)
        Me.TPOneStackFourRun.Controls.Add(Me.Label212)
        Me.TPOneStackFourRun.Controls.Add(Me.Label211)
        Me.TPOneStackFourRun.Controls.Add(Me.Label210)
        Me.TPOneStackFourRun.Controls.Add(Me.Label209)
        Me.TPOneStackFourRun.Controls.Add(Me.Label208)
        Me.TPOneStackFourRun.Controls.Add(Me.Label207)
        Me.TPOneStackFourRun.Controls.Add(Me.txtEmissRateOneStackFourRun1D)
        Me.TPOneStackFourRun.Controls.Add(Me.txtPollConcOneStackFourRun1D)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasMoistOneStackFourRun1D)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasFlowDSCFMOneStackFourRun1D)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasFlowACFMOneStackFourRun1D)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasTempOneStackFourRun1D)
        Me.TPOneStackFourRun.Controls.Add(Me.txtRunNumOneStackFourRun1D)
        Me.TPOneStackFourRun.Controls.Add(Me.btnClearOneStackFourRun4)
        Me.TPOneStackFourRun.Controls.Add(Me.Label34)
        Me.TPOneStackFourRun.Controls.Add(Me.txtEmissRateAvgOneStackFourRun)
        Me.TPOneStackFourRun.Controls.Add(Me.txtPollConcAvgOneStackFourRun)
        Me.TPOneStackFourRun.Controls.Add(Me.txtEmissRateOneStackFourRun1C)
        Me.TPOneStackFourRun.Controls.Add(Me.txtPollConcOneStackFourRun1C)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasMoistOneStackFourRun1C)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasFlowDSCFMOneStackFourRun1C)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasFlowACFMOneStackFourRun1C)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasTempOneStackFourRun1C)
        Me.TPOneStackFourRun.Controls.Add(Me.txtRunNumOneStackFourRun1C)
        Me.TPOneStackFourRun.Controls.Add(Me.txtEmissRateOneStackFourRun1B)
        Me.TPOneStackFourRun.Controls.Add(Me.txtPollConcOneStackFourRun1B)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasMoistOneStackFourRun1B)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasFlowDSCFMOneStackFourRun1B)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasFlowACFMOneStackFourRun1B)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasTempOneStackFourRun1B)
        Me.TPOneStackFourRun.Controls.Add(Me.txtRunNumOneStackFourRun1B)
        Me.TPOneStackFourRun.Controls.Add(Me.txtEmissRateOneStackFourRun1A)
        Me.TPOneStackFourRun.Controls.Add(Me.txtPollConcOneStackFourRun1A)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasMoistOneStackFourRun1A)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasFlowDSCFMOneStackFourRun1A)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasFlowACFMOneStackFourRun1A)
        Me.TPOneStackFourRun.Controls.Add(Me.txtGasTempOneStackFourRun1A)
        Me.TPOneStackFourRun.Controls.Add(Me.txtRunNumOneStackFourRun1A)
        Me.TPOneStackFourRun.Controls.Add(Me.Label43)
        Me.TPOneStackFourRun.Controls.Add(Me.Label44)
        Me.TPOneStackFourRun.Controls.Add(Me.Label45)
        Me.TPOneStackFourRun.Controls.Add(Me.Label46)
        Me.TPOneStackFourRun.Controls.Add(Me.Label47)
        Me.TPOneStackFourRun.Controls.Add(Me.Label48)
        Me.TPOneStackFourRun.Controls.Add(Me.Label49)
        Me.TPOneStackFourRun.Controls.Add(Me.cboEmissRateUnitOneStackFourRun)
        Me.TPOneStackFourRun.Controls.Add(Me.cboPollConUnitOneStackFourRun)
        Me.TPOneStackFourRun.Controls.Add(Me.btnClearOneStackFourRun3)
        Me.TPOneStackFourRun.Controls.Add(Me.btnClearOneStackFourRun2)
        Me.TPOneStackFourRun.Controls.Add(Me.btnClearOneStackFourRun1)
        Me.TPOneStackFourRun.Location = New System.Drawing.Point(4, 22)
        Me.TPOneStackFourRun.Name = "TPOneStackFourRun"
        Me.TPOneStackFourRun.Size = New System.Drawing.Size(774, 164)
        Me.TPOneStackFourRun.TabIndex = 1
        Me.TPOneStackFourRun.Text = "4 Runs"
        '
        'Label213
        '
        Me.Label213.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label213.Location = New System.Drawing.Point(4, 163)
        Me.Label213.Name = "Label213"
        Me.Label213.Size = New System.Drawing.Size(774, 1)
        Me.Label213.TabIndex = 220
        '
        'Label212
        '
        Me.Label212.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label212.Location = New System.Drawing.Point(4, 141)
        Me.Label212.Name = "Label212"
        Me.Label212.Size = New System.Drawing.Size(774, 1)
        Me.Label212.TabIndex = 219
        '
        'Label211
        '
        Me.Label211.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label211.Location = New System.Drawing.Point(4, 119)
        Me.Label211.Name = "Label211"
        Me.Label211.Size = New System.Drawing.Size(546, 1)
        Me.Label211.TabIndex = 218
        '
        'Label210
        '
        Me.Label210.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label210.Location = New System.Drawing.Point(4, 97)
        Me.Label210.Name = "Label210"
        Me.Label210.Size = New System.Drawing.Size(546, 1)
        Me.Label210.TabIndex = 217
        '
        'Label209
        '
        Me.Label209.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label209.Location = New System.Drawing.Point(4, 75)
        Me.Label209.Name = "Label209"
        Me.Label209.Size = New System.Drawing.Size(546, 1)
        Me.Label209.TabIndex = 216
        '
        'Label208
        '
        Me.Label208.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label208.Location = New System.Drawing.Point(4, 53)
        Me.Label208.Name = "Label208"
        Me.Label208.Size = New System.Drawing.Size(546, 1)
        Me.Label208.TabIndex = 215
        '
        'Label207
        '
        Me.Label207.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label207.Location = New System.Drawing.Point(36, 28)
        Me.Label207.Name = "Label207"
        Me.Label207.Size = New System.Drawing.Size(506, 1)
        Me.Label207.TabIndex = 214
        '
        'txtEmissRateOneStackFourRun1D
        '
        Me.txtEmissRateOneStackFourRun1D.Location = New System.Drawing.Point(454, 142)
        Me.txtEmissRateOneStackFourRun1D.MaxLength = 11
        Me.txtEmissRateOneStackFourRun1D.Name = "txtEmissRateOneStackFourRun1D"
        Me.txtEmissRateOneStackFourRun1D.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackFourRun1D.TabIndex = 109
        '
        'txtPollConcOneStackFourRun1D
        '
        Me.txtPollConcOneStackFourRun1D.Location = New System.Drawing.Point(454, 120)
        Me.txtPollConcOneStackFourRun1D.MaxLength = 11
        Me.txtPollConcOneStackFourRun1D.Name = "txtPollConcOneStackFourRun1D"
        Me.txtPollConcOneStackFourRun1D.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackFourRun1D.TabIndex = 103
        '
        'txtGasMoistOneStackFourRun1D
        '
        Me.txtGasMoistOneStackFourRun1D.Location = New System.Drawing.Point(454, 54)
        Me.txtGasMoistOneStackFourRun1D.MaxLength = 11
        Me.txtGasMoistOneStackFourRun1D.Name = "txtGasMoistOneStackFourRun1D"
        Me.txtGasMoistOneStackFourRun1D.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackFourRun1D.TabIndex = 91
        '
        'txtGasFlowDSCFMOneStackFourRun1D
        '
        Me.txtGasFlowDSCFMOneStackFourRun1D.Location = New System.Drawing.Point(454, 98)
        Me.txtGasFlowDSCFMOneStackFourRun1D.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackFourRun1D.Name = "txtGasFlowDSCFMOneStackFourRun1D"
        Me.txtGasFlowDSCFMOneStackFourRun1D.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackFourRun1D.TabIndex = 99
        '
        'txtGasFlowACFMOneStackFourRun1D
        '
        Me.txtGasFlowACFMOneStackFourRun1D.Location = New System.Drawing.Point(454, 76)
        Me.txtGasFlowACFMOneStackFourRun1D.MaxLength = 11
        Me.txtGasFlowACFMOneStackFourRun1D.Name = "txtGasFlowACFMOneStackFourRun1D"
        Me.txtGasFlowACFMOneStackFourRun1D.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackFourRun1D.TabIndex = 95
        '
        'txtGasTempOneStackFourRun1D
        '
        Me.txtGasTempOneStackFourRun1D.Location = New System.Drawing.Point(454, 32)
        Me.txtGasTempOneStackFourRun1D.MaxLength = 11
        Me.txtGasTempOneStackFourRun1D.Name = "txtGasTempOneStackFourRun1D"
        Me.txtGasTempOneStackFourRun1D.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackFourRun1D.TabIndex = 87
        '
        'txtRunNumOneStackFourRun1D
        '
        Me.txtRunNumOneStackFourRun1D.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackFourRun1D.Location = New System.Drawing.Point(460, 8)
        Me.txtRunNumOneStackFourRun1D.MaxLength = 3
        Me.txtRunNumOneStackFourRun1D.Name = "txtRunNumOneStackFourRun1D"
        Me.txtRunNumOneStackFourRun1D.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackFourRun1D.TabIndex = 83
        '
        'btnClearOneStackFourRun4
        '
        Me.btnClearOneStackFourRun4.Location = New System.Drawing.Point(502, 8)
        Me.btnClearOneStackFourRun4.Name = "btnClearOneStackFourRun4"
        Me.btnClearOneStackFourRun4.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackFourRun4.TabIndex = 212
        Me.btnClearOneStackFourRun4.TabStop = False
        Me.btnClearOneStackFourRun4.Text = "Clear"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(706, 104)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(65, 13)
        Me.Label34.TabIndex = 199
        Me.Label34.Text = "AVERAGES"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txtEmissRateAvgOneStackFourRun
        '
        Me.txtEmissRateAvgOneStackFourRun.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateAvgOneStackFourRun.Location = New System.Drawing.Point(706, 142)
        Me.txtEmissRateAvgOneStackFourRun.MaxLength = 11
        Me.txtEmissRateAvgOneStackFourRun.Name = "txtEmissRateAvgOneStackFourRun"
        Me.txtEmissRateAvgOneStackFourRun.ReadOnly = True
        Me.txtEmissRateAvgOneStackFourRun.Size = New System.Drawing.Size(62, 20)
        Me.txtEmissRateAvgOneStackFourRun.TabIndex = 111
        Me.txtEmissRateAvgOneStackFourRun.TabStop = False
        '
        'txtPollConcAvgOneStackFourRun
        '
        Me.txtPollConcAvgOneStackFourRun.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgOneStackFourRun.Location = New System.Drawing.Point(706, 120)
        Me.txtPollConcAvgOneStackFourRun.MaxLength = 11
        Me.txtPollConcAvgOneStackFourRun.Name = "txtPollConcAvgOneStackFourRun"
        Me.txtPollConcAvgOneStackFourRun.ReadOnly = True
        Me.txtPollConcAvgOneStackFourRun.Size = New System.Drawing.Size(62, 20)
        Me.txtPollConcAvgOneStackFourRun.TabIndex = 105
        Me.txtPollConcAvgOneStackFourRun.TabStop = False
        '
        'txtEmissRateOneStackFourRun1C
        '
        Me.txtEmissRateOneStackFourRun1C.Location = New System.Drawing.Point(348, 142)
        Me.txtEmissRateOneStackFourRun1C.MaxLength = 11
        Me.txtEmissRateOneStackFourRun1C.Name = "txtEmissRateOneStackFourRun1C"
        Me.txtEmissRateOneStackFourRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackFourRun1C.TabIndex = 108
        '
        'txtPollConcOneStackFourRun1C
        '
        Me.txtPollConcOneStackFourRun1C.Location = New System.Drawing.Point(348, 120)
        Me.txtPollConcOneStackFourRun1C.MaxLength = 11
        Me.txtPollConcOneStackFourRun1C.Name = "txtPollConcOneStackFourRun1C"
        Me.txtPollConcOneStackFourRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackFourRun1C.TabIndex = 102
        '
        'txtGasMoistOneStackFourRun1C
        '
        Me.txtGasMoistOneStackFourRun1C.Location = New System.Drawing.Point(348, 54)
        Me.txtGasMoistOneStackFourRun1C.MaxLength = 11
        Me.txtGasMoistOneStackFourRun1C.Name = "txtGasMoistOneStackFourRun1C"
        Me.txtGasMoistOneStackFourRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackFourRun1C.TabIndex = 90
        '
        'txtGasFlowDSCFMOneStackFourRun1C
        '
        Me.txtGasFlowDSCFMOneStackFourRun1C.Location = New System.Drawing.Point(348, 98)
        Me.txtGasFlowDSCFMOneStackFourRun1C.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackFourRun1C.Name = "txtGasFlowDSCFMOneStackFourRun1C"
        Me.txtGasFlowDSCFMOneStackFourRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackFourRun1C.TabIndex = 98
        '
        'txtGasFlowACFMOneStackFourRun1C
        '
        Me.txtGasFlowACFMOneStackFourRun1C.Location = New System.Drawing.Point(348, 76)
        Me.txtGasFlowACFMOneStackFourRun1C.MaxLength = 11
        Me.txtGasFlowACFMOneStackFourRun1C.Name = "txtGasFlowACFMOneStackFourRun1C"
        Me.txtGasFlowACFMOneStackFourRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackFourRun1C.TabIndex = 94
        '
        'txtGasTempOneStackFourRun1C
        '
        Me.txtGasTempOneStackFourRun1C.Location = New System.Drawing.Point(348, 32)
        Me.txtGasTempOneStackFourRun1C.MaxLength = 11
        Me.txtGasTempOneStackFourRun1C.Name = "txtGasTempOneStackFourRun1C"
        Me.txtGasTempOneStackFourRun1C.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackFourRun1C.TabIndex = 86
        '
        'txtRunNumOneStackFourRun1C
        '
        Me.txtRunNumOneStackFourRun1C.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackFourRun1C.Location = New System.Drawing.Point(354, 8)
        Me.txtRunNumOneStackFourRun1C.MaxLength = 3
        Me.txtRunNumOneStackFourRun1C.Name = "txtRunNumOneStackFourRun1C"
        Me.txtRunNumOneStackFourRun1C.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackFourRun1C.TabIndex = 82
        '
        'txtEmissRateOneStackFourRun1B
        '
        Me.txtEmissRateOneStackFourRun1B.Location = New System.Drawing.Point(242, 142)
        Me.txtEmissRateOneStackFourRun1B.MaxLength = 11
        Me.txtEmissRateOneStackFourRun1B.Name = "txtEmissRateOneStackFourRun1B"
        Me.txtEmissRateOneStackFourRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackFourRun1B.TabIndex = 107
        '
        'txtPollConcOneStackFourRun1B
        '
        Me.txtPollConcOneStackFourRun1B.Location = New System.Drawing.Point(242, 120)
        Me.txtPollConcOneStackFourRun1B.MaxLength = 11
        Me.txtPollConcOneStackFourRun1B.Name = "txtPollConcOneStackFourRun1B"
        Me.txtPollConcOneStackFourRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackFourRun1B.TabIndex = 101
        '
        'txtGasMoistOneStackFourRun1B
        '
        Me.txtGasMoistOneStackFourRun1B.Location = New System.Drawing.Point(242, 54)
        Me.txtGasMoistOneStackFourRun1B.MaxLength = 11
        Me.txtGasMoistOneStackFourRun1B.Name = "txtGasMoistOneStackFourRun1B"
        Me.txtGasMoistOneStackFourRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackFourRun1B.TabIndex = 89
        '
        'txtGasFlowDSCFMOneStackFourRun1B
        '
        Me.txtGasFlowDSCFMOneStackFourRun1B.Location = New System.Drawing.Point(242, 98)
        Me.txtGasFlowDSCFMOneStackFourRun1B.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackFourRun1B.Name = "txtGasFlowDSCFMOneStackFourRun1B"
        Me.txtGasFlowDSCFMOneStackFourRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackFourRun1B.TabIndex = 97
        '
        'txtGasFlowACFMOneStackFourRun1B
        '
        Me.txtGasFlowACFMOneStackFourRun1B.Location = New System.Drawing.Point(242, 76)
        Me.txtGasFlowACFMOneStackFourRun1B.MaxLength = 11
        Me.txtGasFlowACFMOneStackFourRun1B.Name = "txtGasFlowACFMOneStackFourRun1B"
        Me.txtGasFlowACFMOneStackFourRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackFourRun1B.TabIndex = 93
        '
        'txtGasTempOneStackFourRun1B
        '
        Me.txtGasTempOneStackFourRun1B.Location = New System.Drawing.Point(242, 32)
        Me.txtGasTempOneStackFourRun1B.MaxLength = 11
        Me.txtGasTempOneStackFourRun1B.Name = "txtGasTempOneStackFourRun1B"
        Me.txtGasTempOneStackFourRun1B.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackFourRun1B.TabIndex = 85
        '
        'txtRunNumOneStackFourRun1B
        '
        Me.txtRunNumOneStackFourRun1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackFourRun1B.Location = New System.Drawing.Point(248, 8)
        Me.txtRunNumOneStackFourRun1B.MaxLength = 3
        Me.txtRunNumOneStackFourRun1B.Name = "txtRunNumOneStackFourRun1B"
        Me.txtRunNumOneStackFourRun1B.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackFourRun1B.TabIndex = 81
        '
        'txtEmissRateOneStackFourRun1A
        '
        Me.txtEmissRateOneStackFourRun1A.Location = New System.Drawing.Point(136, 142)
        Me.txtEmissRateOneStackFourRun1A.MaxLength = 11
        Me.txtEmissRateOneStackFourRun1A.Name = "txtEmissRateOneStackFourRun1A"
        Me.txtEmissRateOneStackFourRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateOneStackFourRun1A.TabIndex = 106
        '
        'txtPollConcOneStackFourRun1A
        '
        Me.txtPollConcOneStackFourRun1A.Location = New System.Drawing.Point(136, 120)
        Me.txtPollConcOneStackFourRun1A.MaxLength = 11
        Me.txtPollConcOneStackFourRun1A.Name = "txtPollConcOneStackFourRun1A"
        Me.txtPollConcOneStackFourRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOneStackFourRun1A.TabIndex = 100
        '
        'txtGasMoistOneStackFourRun1A
        '
        Me.txtGasMoistOneStackFourRun1A.Location = New System.Drawing.Point(136, 54)
        Me.txtGasMoistOneStackFourRun1A.MaxLength = 11
        Me.txtGasMoistOneStackFourRun1A.Name = "txtGasMoistOneStackFourRun1A"
        Me.txtGasMoistOneStackFourRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasMoistOneStackFourRun1A.TabIndex = 88
        '
        'txtGasFlowDSCFMOneStackFourRun1A
        '
        Me.txtGasFlowDSCFMOneStackFourRun1A.Location = New System.Drawing.Point(136, 98)
        Me.txtGasFlowDSCFMOneStackFourRun1A.MaxLength = 11
        Me.txtGasFlowDSCFMOneStackFourRun1A.Name = "txtGasFlowDSCFMOneStackFourRun1A"
        Me.txtGasFlowDSCFMOneStackFourRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowDSCFMOneStackFourRun1A.TabIndex = 96
        '
        'txtGasFlowACFMOneStackFourRun1A
        '
        Me.txtGasFlowACFMOneStackFourRun1A.Location = New System.Drawing.Point(136, 76)
        Me.txtGasFlowACFMOneStackFourRun1A.MaxLength = 11
        Me.txtGasFlowACFMOneStackFourRun1A.Name = "txtGasFlowACFMOneStackFourRun1A"
        Me.txtGasFlowACFMOneStackFourRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasFlowACFMOneStackFourRun1A.TabIndex = 92
        '
        'txtGasTempOneStackFourRun1A
        '
        Me.txtGasTempOneStackFourRun1A.Location = New System.Drawing.Point(136, 32)
        Me.txtGasTempOneStackFourRun1A.MaxLength = 11
        Me.txtGasTempOneStackFourRun1A.Name = "txtGasTempOneStackFourRun1A"
        Me.txtGasTempOneStackFourRun1A.Size = New System.Drawing.Size(96, 20)
        Me.txtGasTempOneStackFourRun1A.TabIndex = 84
        '
        'txtRunNumOneStackFourRun1A
        '
        Me.txtRunNumOneStackFourRun1A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumOneStackFourRun1A.Location = New System.Drawing.Point(142, 9)
        Me.txtRunNumOneStackFourRun1A.MaxLength = 3
        Me.txtRunNumOneStackFourRun1A.Name = "txtRunNumOneStackFourRun1A"
        Me.txtRunNumOneStackFourRun1A.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumOneStackFourRun1A.TabIndex = 80
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(4, 54)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(86, 13)
        Me.Label43.TabIndex = 198
        Me.Label43.Text = "Gas Moisture (%)"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(4, 76)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(115, 13)
        Me.Label44.TabIndex = 197
        Me.Label44.Text = "Gas Flow Rate (ACFM)"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(4, 98)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(123, 13)
        Me.Label45.TabIndex = 196
        Me.Label45.Text = "Gas Flow Rate (DSCFM)"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(4, 120)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(120, 13)
        Me.Label46.TabIndex = 194
        Me.Label46.Text = "Pollutant Concentration:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(4, 142)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(77, 13)
        Me.Label47.TabIndex = 192
        Me.Label47.Text = "Emission Rate:"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(4, 32)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(107, 13)
        Me.Label48.TabIndex = 191
        Me.Label48.Text = "Gas Temperature (F):"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(36, 8)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(61, 13)
        Me.Label49.TabIndex = 188
        Me.Label49.Text = "Test Run #"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboEmissRateUnitOneStackFourRun
        '
        Me.cboEmissRateUnitOneStackFourRun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEmissRateUnitOneStackFourRun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEmissRateUnitOneStackFourRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmissRateUnitOneStackFourRun.Location = New System.Drawing.Point(560, 142)
        Me.cboEmissRateUnitOneStackFourRun.Name = "cboEmissRateUnitOneStackFourRun"
        Me.cboEmissRateUnitOneStackFourRun.Size = New System.Drawing.Size(136, 21)
        Me.cboEmissRateUnitOneStackFourRun.TabIndex = 110
        '
        'cboPollConUnitOneStackFourRun
        '
        Me.cboPollConUnitOneStackFourRun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitOneStackFourRun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitOneStackFourRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollConUnitOneStackFourRun.Location = New System.Drawing.Point(560, 120)
        Me.cboPollConUnitOneStackFourRun.Name = "cboPollConUnitOneStackFourRun"
        Me.cboPollConUnitOneStackFourRun.Size = New System.Drawing.Size(136, 21)
        Me.cboPollConUnitOneStackFourRun.TabIndex = 104
        '
        'btnClearOneStackFourRun3
        '
        Me.btnClearOneStackFourRun3.Location = New System.Drawing.Point(396, 8)
        Me.btnClearOneStackFourRun3.Name = "btnClearOneStackFourRun3"
        Me.btnClearOneStackFourRun3.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackFourRun3.TabIndex = 204
        Me.btnClearOneStackFourRun3.TabStop = False
        Me.btnClearOneStackFourRun3.Text = "Clear"
        '
        'btnClearOneStackFourRun2
        '
        Me.btnClearOneStackFourRun2.Location = New System.Drawing.Point(290, 8)
        Me.btnClearOneStackFourRun2.Name = "btnClearOneStackFourRun2"
        Me.btnClearOneStackFourRun2.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackFourRun2.TabIndex = 203
        Me.btnClearOneStackFourRun2.TabStop = False
        Me.btnClearOneStackFourRun2.Text = "Clear"
        '
        'btnClearOneStackFourRun1
        '
        Me.btnClearOneStackFourRun1.Location = New System.Drawing.Point(184, 8)
        Me.btnClearOneStackFourRun1.Name = "btnClearOneStackFourRun1"
        Me.btnClearOneStackFourRun1.Size = New System.Drawing.Size(40, 20)
        Me.btnClearOneStackFourRun1.TabIndex = 202
        Me.btnClearOneStackFourRun1.TabStop = False
        Me.btnClearOneStackFourRun1.Text = "Clear"
        '
        'txtControlEquipmentOperatingDataOneStack
        '
        Me.txtControlEquipmentOperatingDataOneStack.Location = New System.Drawing.Point(138, 82)
        Me.txtControlEquipmentOperatingDataOneStack.MaxLength = 4000
        Me.txtControlEquipmentOperatingDataOneStack.Multiline = True
        Me.txtControlEquipmentOperatingDataOneStack.Name = "txtControlEquipmentOperatingDataOneStack"
        Me.txtControlEquipmentOperatingDataOneStack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlEquipmentOperatingDataOneStack.Size = New System.Drawing.Size(616, 40)
        Me.txtControlEquipmentOperatingDataOneStack.TabIndex = 31
        '
        'txtApplicableRegulationOneStack
        '
        Me.txtApplicableRegulationOneStack.AcceptsReturn = True
        Me.txtApplicableRegulationOneStack.Location = New System.Drawing.Point(138, 50)
        Me.txtApplicableRegulationOneStack.MaxLength = 200
        Me.txtApplicableRegulationOneStack.Multiline = True
        Me.txtApplicableRegulationOneStack.Name = "txtApplicableRegulationOneStack"
        Me.txtApplicableRegulationOneStack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationOneStack.Size = New System.Drawing.Size(616, 30)
        Me.txtApplicableRegulationOneStack.TabIndex = 30
        '
        'txtOperatingCapacityOneStack
        '
        Me.txtOperatingCapacityOneStack.Location = New System.Drawing.Point(498, 6)
        Me.txtOperatingCapacityOneStack.MaxLength = 11
        Me.txtOperatingCapacityOneStack.Name = "txtOperatingCapacityOneStack"
        Me.txtOperatingCapacityOneStack.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityOneStack.TabIndex = 22
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(394, 6)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(100, 13)
        Me.Label50.TabIndex = 766
        Me.Label50.Text = "Operating Capacity:"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAllowableEmissionRate2OneStack
        '
        Me.txtAllowableEmissionRate2OneStack.Location = New System.Drawing.Point(346, 28)
        Me.txtAllowableEmissionRate2OneStack.MaxLength = 11
        Me.txtAllowableEmissionRate2OneStack.Name = "txtAllowableEmissionRate2OneStack"
        Me.txtAllowableEmissionRate2OneStack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate2OneStack.TabIndex = 26
        '
        'txtAllowableEmissionRate3OneStack
        '
        Me.txtAllowableEmissionRate3OneStack.Location = New System.Drawing.Point(554, 28)
        Me.txtAllowableEmissionRate3OneStack.MaxLength = 11
        Me.txtAllowableEmissionRate3OneStack.Name = "txtAllowableEmissionRate3OneStack"
        Me.txtAllowableEmissionRate3OneStack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate3OneStack.TabIndex = 28
        '
        'txtAllowableEmissionRate1OneStack
        '
        Me.txtAllowableEmissionRate1OneStack.Location = New System.Drawing.Point(138, 28)
        Me.txtAllowableEmissionRate1OneStack.MaxLength = 11
        Me.txtAllowableEmissionRate1OneStack.Name = "txtAllowableEmissionRate1OneStack"
        Me.txtAllowableEmissionRate1OneStack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate1OneStack.TabIndex = 24
        '
        'txtMaximumExpectedOperatingCapacityOneStack
        '
        Me.txtMaximumExpectedOperatingCapacityOneStack.Location = New System.Drawing.Point(138, 6)
        Me.txtMaximumExpectedOperatingCapacityOneStack.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityOneStack.Name = "txtMaximumExpectedOperatingCapacityOneStack"
        Me.txtMaximumExpectedOperatingCapacityOneStack.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityOneStack.TabIndex = 20
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(6, 50)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(122, 13)
        Me.Label51.TabIndex = 764
        Me.Label51.Text = "Applicable Requirement:"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(6, 32)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(125, 13)
        Me.Label52.TabIndex = 763
        Me.Label52.Text = "Allowable Emission Rate:"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboOperatingCapacityUnitsOneStack
        '
        Me.cboOperatingCapacityUnitsOneStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsOneStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsOneStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsOneStack.Location = New System.Drawing.Point(586, 6)
        Me.cboOperatingCapacityUnitsOneStack.Name = "cboOperatingCapacityUnitsOneStack"
        Me.cboOperatingCapacityUnitsOneStack.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsOneStack.TabIndex = 23
        '
        'cboAllowableEmissionRateUnits2OneStack
        '
        Me.cboAllowableEmissionRateUnits2OneStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits2OneStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits2OneStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits2OneStack.Location = New System.Drawing.Point(434, 28)
        Me.cboAllowableEmissionRateUnits2OneStack.Name = "cboAllowableEmissionRateUnits2OneStack"
        Me.cboAllowableEmissionRateUnits2OneStack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits2OneStack.TabIndex = 27
        '
        'cboAllowableEmissionRateUnits3OneStack
        '
        Me.cboAllowableEmissionRateUnits3OneStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits3OneStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits3OneStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits3OneStack.Location = New System.Drawing.Point(642, 28)
        Me.cboAllowableEmissionRateUnits3OneStack.Name = "cboAllowableEmissionRateUnits3OneStack"
        Me.cboAllowableEmissionRateUnits3OneStack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits3OneStack.TabIndex = 29
        '
        'cboAllowableEmissionRateUnits1OneStack
        '
        Me.cboAllowableEmissionRateUnits1OneStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits1OneStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits1OneStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits1OneStack.Location = New System.Drawing.Point(226, 28)
        Me.cboAllowableEmissionRateUnits1OneStack.Name = "cboAllowableEmissionRateUnits1OneStack"
        Me.cboAllowableEmissionRateUnits1OneStack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits1OneStack.TabIndex = 25
        '
        'cboMaximumExpectedOperatingCapacityUnitsOneStack
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsOneStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsOneStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsOneStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsOneStack.Location = New System.Drawing.Point(226, 6)
        Me.cboMaximumExpectedOperatingCapacityUnitsOneStack.Name = "cboMaximumExpectedOperatingCapacityUnitsOneStack"
        Me.cboMaximumExpectedOperatingCapacityUnitsOneStack.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsOneStack.TabIndex = 21
        '
        'Label53
        '
        Me.Label53.Location = New System.Drawing.Point(6, 82)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(112, 32)
        Me.Label53.TabIndex = 765
        Me.Label53.Text = "Control Equipment and Monitoring Data:"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label54
        '
        Me.Label54.Location = New System.Drawing.Point(6, 4)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(104, 26)
        Me.Label54.TabIndex = 0
        Me.Label54.Text = "Maximum Expected Operating Capacity:"
        '
        'txtPercentAllowableOneStack
        '
        Me.txtPercentAllowableOneStack.Location = New System.Drawing.Point(138, 326)
        Me.txtPercentAllowableOneStack.MaxLength = 11
        Me.txtPercentAllowableOneStack.Name = "txtPercentAllowableOneStack"
        Me.txtPercentAllowableOneStack.Size = New System.Drawing.Size(80, 20)
        Me.txtPercentAllowableOneStack.TabIndex = 120
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(6, 326)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(95, 13)
        Me.Label55.TabIndex = 770
        Me.Label55.Text = "Percent Allowable:"
        '
        'Label193
        '
        Me.Label193.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label193.Location = New System.Drawing.Point(110, 27)
        Me.Label193.Name = "Label193"
        Me.Label193.Size = New System.Drawing.Size(588, 1)
        Me.Label193.TabIndex = 772
        '
        'Label197
        '
        Me.Label197.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label197.Location = New System.Drawing.Point(6, 49)
        Me.Label197.Name = "Label197"
        Me.Label197.Size = New System.Drawing.Size(748, 1)
        Me.Label197.TabIndex = 774
        '
        'Label198
        '
        Me.Label198.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label198.Location = New System.Drawing.Point(6, 80)
        Me.Label198.Name = "Label198"
        Me.Label198.Size = New System.Drawing.Size(748, 1)
        Me.Label198.TabIndex = 773
        '
        'Label199
        '
        Me.Label199.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label199.Location = New System.Drawing.Point(6, 123)
        Me.Label199.Name = "Label199"
        Me.Label199.Size = New System.Drawing.Size(748, 1)
        Me.Label199.TabIndex = 775
        '
        'TPLoadingRack
        '
        Me.TPLoadingRack.AutoScroll = True
        Me.TPLoadingRack.Controls.Add(Me.Label292)
        Me.TPLoadingRack.Controls.Add(Me.Label291)
        Me.TPLoadingRack.Controls.Add(Me.Label165)
        Me.TPLoadingRack.Controls.Add(Me.Label290)
        Me.TPLoadingRack.Controls.Add(Me.Label289)
        Me.TPLoadingRack.Controls.Add(Me.Label288)
        Me.TPLoadingRack.Controls.Add(Me.Label287)
        Me.TPLoadingRack.Controls.Add(Me.txtDestructionEfficiencyLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.Label171)
        Me.TPLoadingRack.Controls.Add(Me.Label154)
        Me.TPLoadingRack.Controls.Add(Me.cboEmissRateUnitLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.cboPollConUnitOUTLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtOtherInformationLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.Label153)
        Me.TPLoadingRack.Controls.Add(Me.Label155)
        Me.TPLoadingRack.Controls.Add(Me.txtEmissRateLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtTestDurationLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtPollConcOUTLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtPollConcINLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.Label156)
        Me.TPLoadingRack.Controls.Add(Me.Label157)
        Me.TPLoadingRack.Controls.Add(Me.Label164)
        Me.TPLoadingRack.Controls.Add(Me.cboPollConUnitINLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.cboTestDurationUnitsLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtControlEquipmentOperatingDataLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtApplicableRegulationLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtOperatingCapacityLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.Label166)
        Me.TPLoadingRack.Controls.Add(Me.txtAllowableEmissionRate2LoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtAllowableEmissionRate3LoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtAllowableEmissionRate1LoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.txtMaximumExpectedOperatingCapacityLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.Label167)
        Me.TPLoadingRack.Controls.Add(Me.Label168)
        Me.TPLoadingRack.Controls.Add(Me.cboOperatingCapacityUnitsLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.cboAllowableEmissionRateUnits2LoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.cboAllowableEmissionRateUnits3LoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.cboAllowableEmissionRateUnits1LoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack)
        Me.TPLoadingRack.Controls.Add(Me.Label169)
        Me.TPLoadingRack.Controls.Add(Me.Label170)
        Me.TPLoadingRack.Location = New System.Drawing.Point(4, 22)
        Me.TPLoadingRack.Name = "TPLoadingRack"
        Me.TPLoadingRack.Padding = New System.Windows.Forms.Padding(3)
        Me.TPLoadingRack.Size = New System.Drawing.Size(782, 288)
        Me.TPLoadingRack.TabIndex = 1
        Me.TPLoadingRack.Text = "Loading Rack"
        Me.TPLoadingRack.UseVisualStyleBackColor = True
        '
        'Label292
        '
        Me.Label292.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label292.Location = New System.Drawing.Point(3, 219)
        Me.Label292.Name = "Label292"
        Me.Label292.Size = New System.Drawing.Size(692, 1)
        Me.Label292.TabIndex = 349
        Me.Label292.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label291
        '
        Me.Label291.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label291.Location = New System.Drawing.Point(3, 194)
        Me.Label291.Name = "Label291"
        Me.Label291.Size = New System.Drawing.Size(389, 1)
        Me.Label291.TabIndex = 348
        Me.Label291.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label165
        '
        Me.Label165.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label165.Location = New System.Drawing.Point(47, 150)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(308, 1)
        Me.Label165.TabIndex = 347
        Me.Label165.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label290
        '
        Me.Label290.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label290.Location = New System.Drawing.Point(3, 124)
        Me.Label290.Name = "Label290"
        Me.Label290.Size = New System.Drawing.Size(748, 1)
        Me.Label290.TabIndex = 346
        Me.Label290.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label289
        '
        Me.Label289.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label289.Location = New System.Drawing.Point(3, 82)
        Me.Label289.Name = "Label289"
        Me.Label289.Size = New System.Drawing.Size(748, 1)
        Me.Label289.TabIndex = 345
        Me.Label289.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label288
        '
        Me.Label288.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label288.Location = New System.Drawing.Point(3, 50)
        Me.Label288.Name = "Label288"
        Me.Label288.Size = New System.Drawing.Size(748, 1)
        Me.Label288.TabIndex = 344
        Me.Label288.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label287
        '
        Me.Label287.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label287.Location = New System.Drawing.Point(107, 28)
        Me.Label287.Name = "Label287"
        Me.Label287.Size = New System.Drawing.Size(588, 1)
        Me.Label287.TabIndex = 343
        Me.Label287.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDestructionEfficiencyLoadingRack
        '
        Me.txtDestructionEfficiencyLoadingRack.BackColor = System.Drawing.SystemColors.Window
        Me.txtDestructionEfficiencyLoadingRack.Location = New System.Drawing.Point(135, 197)
        Me.txtDestructionEfficiencyLoadingRack.MaxLength = 11
        Me.txtDestructionEfficiencyLoadingRack.Name = "txtDestructionEfficiencyLoadingRack"
        Me.txtDestructionEfficiencyLoadingRack.ReadOnly = True
        Me.txtDestructionEfficiencyLoadingRack.Size = New System.Drawing.Size(96, 20)
        Me.txtDestructionEfficiencyLoadingRack.TabIndex = 148
        Me.txtDestructionEfficiencyLoadingRack.TabStop = False
        '
        'Label171
        '
        Me.Label171.AutoSize = True
        Me.Label171.Location = New System.Drawing.Point(364, 152)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(18, 13)
        Me.Label171.TabIndex = 341
        Me.Label171.Text = "IN"
        Me.Label171.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Label154
        '
        Me.Label154.AutoSize = True
        Me.Label154.Location = New System.Drawing.Point(364, 175)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(30, 13)
        Me.Label154.TabIndex = 340
        Me.Label154.Text = "OUT"
        Me.Label154.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'cboEmissRateUnitLoadingRack
        '
        Me.cboEmissRateUnitLoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEmissRateUnitLoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEmissRateUnitLoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmissRateUnitLoadingRack.Location = New System.Drawing.Point(583, 197)
        Me.cboEmissRateUnitLoadingRack.Name = "cboEmissRateUnitLoadingRack"
        Me.cboEmissRateUnitLoadingRack.Size = New System.Drawing.Size(112, 21)
        Me.cboEmissRateUnitLoadingRack.TabIndex = 150
        '
        'cboPollConUnitOUTLoadingRack
        '
        Me.cboPollConUnitOUTLoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitOUTLoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitOUTLoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollConUnitOUTLoadingRack.Location = New System.Drawing.Point(231, 173)
        Me.cboPollConUnitOUTLoadingRack.Name = "cboPollConUnitOUTLoadingRack"
        Me.cboPollConUnitOUTLoadingRack.Size = New System.Drawing.Size(128, 21)
        Me.cboPollConUnitOUTLoadingRack.TabIndex = 147
        '
        'txtOtherInformationLoadingRack
        '
        Me.txtOtherInformationLoadingRack.AcceptsReturn = True
        Me.txtOtherInformationLoadingRack.Location = New System.Drawing.Point(135, 220)
        Me.txtOtherInformationLoadingRack.MaxLength = 4000
        Me.txtOtherInformationLoadingRack.Multiline = True
        Me.txtOtherInformationLoadingRack.Name = "txtOtherInformationLoadingRack"
        Me.txtOtherInformationLoadingRack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationLoadingRack.Size = New System.Drawing.Size(616, 128)
        Me.txtOtherInformationLoadingRack.TabIndex = 151
        '
        'Label153
        '
        Me.Label153.AutoSize = True
        Me.Label153.Location = New System.Drawing.Point(3, 220)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(91, 13)
        Me.Label153.TabIndex = 339
        Me.Label153.Text = "Other Information:"
        Me.Label153.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label155
        '
        Me.Label155.AutoSize = True
        Me.Label155.Location = New System.Drawing.Point(391, 197)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(77, 13)
        Me.Label155.TabIndex = 338
        Me.Label155.Text = "Emission Rate:"
        Me.Label155.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtEmissRateLoadingRack
        '
        Me.txtEmissRateLoadingRack.Location = New System.Drawing.Point(495, 197)
        Me.txtEmissRateLoadingRack.MaxLength = 11
        Me.txtEmissRateLoadingRack.Name = "txtEmissRateLoadingRack"
        Me.txtEmissRateLoadingRack.Size = New System.Drawing.Size(88, 20)
        Me.txtEmissRateLoadingRack.TabIndex = 149
        '
        'txtTestDurationLoadingRack
        '
        Me.txtTestDurationLoadingRack.Location = New System.Drawing.Point(135, 129)
        Me.txtTestDurationLoadingRack.MaxLength = 11
        Me.txtTestDurationLoadingRack.Name = "txtTestDurationLoadingRack"
        Me.txtTestDurationLoadingRack.Size = New System.Drawing.Size(96, 20)
        Me.txtTestDurationLoadingRack.TabIndex = 142
        '
        'txtPollConcOUTLoadingRack
        '
        Me.txtPollConcOUTLoadingRack.Location = New System.Drawing.Point(135, 173)
        Me.txtPollConcOUTLoadingRack.MaxLength = 11
        Me.txtPollConcOUTLoadingRack.Name = "txtPollConcOUTLoadingRack"
        Me.txtPollConcOUTLoadingRack.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcOUTLoadingRack.TabIndex = 146
        '
        'txtPollConcINLoadingRack
        '
        Me.txtPollConcINLoadingRack.Location = New System.Drawing.Point(135, 151)
        Me.txtPollConcINLoadingRack.MaxLength = 11
        Me.txtPollConcINLoadingRack.Name = "txtPollConcINLoadingRack"
        Me.txtPollConcINLoadingRack.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcINLoadingRack.TabIndex = 144
        '
        'Label156
        '
        Me.Label156.AutoSize = True
        Me.Label156.Location = New System.Drawing.Point(3, 151)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(120, 13)
        Me.Label156.TabIndex = 337
        Me.Label156.Text = "Pollutant Concentration:"
        Me.Label156.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label157
        '
        Me.Label157.Location = New System.Drawing.Point(3, 197)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(120, 24)
        Me.Label157.TabIndex = 336
        Me.Label157.Text = "Destruction Reduction Efficiency (%)"
        Me.Label157.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label164
        '
        Me.Label164.AutoSize = True
        Me.Label164.Location = New System.Drawing.Point(47, 129)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(74, 13)
        Me.Label164.TabIndex = 335
        Me.Label164.Text = "Test Duration:"
        Me.Label164.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboPollConUnitINLoadingRack
        '
        Me.cboPollConUnitINLoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitINLoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitINLoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollConUnitINLoadingRack.Location = New System.Drawing.Point(231, 151)
        Me.cboPollConUnitINLoadingRack.Name = "cboPollConUnitINLoadingRack"
        Me.cboPollConUnitINLoadingRack.Size = New System.Drawing.Size(128, 21)
        Me.cboPollConUnitINLoadingRack.TabIndex = 145
        '
        'cboTestDurationUnitsLoadingRack
        '
        Me.cboTestDurationUnitsLoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTestDurationUnitsLoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTestDurationUnitsLoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTestDurationUnitsLoadingRack.Location = New System.Drawing.Point(231, 129)
        Me.cboTestDurationUnitsLoadingRack.Name = "cboTestDurationUnitsLoadingRack"
        Me.cboTestDurationUnitsLoadingRack.Size = New System.Drawing.Size(128, 21)
        Me.cboTestDurationUnitsLoadingRack.TabIndex = 143
        '
        'txtControlEquipmentOperatingDataLoadingRack
        '
        Me.txtControlEquipmentOperatingDataLoadingRack.Location = New System.Drawing.Point(135, 83)
        Me.txtControlEquipmentOperatingDataLoadingRack.MaxLength = 4000
        Me.txtControlEquipmentOperatingDataLoadingRack.Multiline = True
        Me.txtControlEquipmentOperatingDataLoadingRack.Name = "txtControlEquipmentOperatingDataLoadingRack"
        Me.txtControlEquipmentOperatingDataLoadingRack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlEquipmentOperatingDataLoadingRack.Size = New System.Drawing.Size(616, 41)
        Me.txtControlEquipmentOperatingDataLoadingRack.TabIndex = 141
        '
        'txtApplicableRegulationLoadingRack
        '
        Me.txtApplicableRegulationLoadingRack.AcceptsReturn = True
        Me.txtApplicableRegulationLoadingRack.Location = New System.Drawing.Point(135, 51)
        Me.txtApplicableRegulationLoadingRack.MaxLength = 200
        Me.txtApplicableRegulationLoadingRack.Multiline = True
        Me.txtApplicableRegulationLoadingRack.Name = "txtApplicableRegulationLoadingRack"
        Me.txtApplicableRegulationLoadingRack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationLoadingRack.Size = New System.Drawing.Size(616, 32)
        Me.txtApplicableRegulationLoadingRack.TabIndex = 140
        '
        'txtOperatingCapacityLoadingRack
        '
        Me.txtOperatingCapacityLoadingRack.Location = New System.Drawing.Point(495, 7)
        Me.txtOperatingCapacityLoadingRack.MaxLength = 11
        Me.txtOperatingCapacityLoadingRack.Name = "txtOperatingCapacityLoadingRack"
        Me.txtOperatingCapacityLoadingRack.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityLoadingRack.TabIndex = 132
        '
        'Label166
        '
        Me.Label166.AutoSize = True
        Me.Label166.Location = New System.Drawing.Point(391, 7)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(100, 13)
        Me.Label166.TabIndex = 334
        Me.Label166.Text = "Operating Capacity:"
        Me.Label166.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAllowableEmissionRate2LoadingRack
        '
        Me.txtAllowableEmissionRate2LoadingRack.Location = New System.Drawing.Point(343, 29)
        Me.txtAllowableEmissionRate2LoadingRack.MaxLength = 11
        Me.txtAllowableEmissionRate2LoadingRack.Name = "txtAllowableEmissionRate2LoadingRack"
        Me.txtAllowableEmissionRate2LoadingRack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate2LoadingRack.TabIndex = 136
        '
        'txtAllowableEmissionRate3LoadingRack
        '
        Me.txtAllowableEmissionRate3LoadingRack.Location = New System.Drawing.Point(551, 29)
        Me.txtAllowableEmissionRate3LoadingRack.MaxLength = 11
        Me.txtAllowableEmissionRate3LoadingRack.Name = "txtAllowableEmissionRate3LoadingRack"
        Me.txtAllowableEmissionRate3LoadingRack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate3LoadingRack.TabIndex = 138
        '
        'txtAllowableEmissionRate1LoadingRack
        '
        Me.txtAllowableEmissionRate1LoadingRack.Location = New System.Drawing.Point(135, 29)
        Me.txtAllowableEmissionRate1LoadingRack.MaxLength = 11
        Me.txtAllowableEmissionRate1LoadingRack.Name = "txtAllowableEmissionRate1LoadingRack"
        Me.txtAllowableEmissionRate1LoadingRack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate1LoadingRack.TabIndex = 134
        '
        'txtMaximumExpectedOperatingCapacityLoadingRack
        '
        Me.txtMaximumExpectedOperatingCapacityLoadingRack.Location = New System.Drawing.Point(135, 7)
        Me.txtMaximumExpectedOperatingCapacityLoadingRack.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityLoadingRack.Name = "txtMaximumExpectedOperatingCapacityLoadingRack"
        Me.txtMaximumExpectedOperatingCapacityLoadingRack.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityLoadingRack.TabIndex = 130
        '
        'Label167
        '
        Me.Label167.AutoSize = True
        Me.Label167.Location = New System.Drawing.Point(3, 51)
        Me.Label167.Name = "Label167"
        Me.Label167.Size = New System.Drawing.Size(122, 13)
        Me.Label167.TabIndex = 332
        Me.Label167.Text = "Applicable Requirement:"
        Me.Label167.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label168
        '
        Me.Label168.AutoSize = True
        Me.Label168.Location = New System.Drawing.Point(3, 31)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(125, 13)
        Me.Label168.TabIndex = 331
        Me.Label168.Text = "Allowable Emission Rate:"
        Me.Label168.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboOperatingCapacityUnitsLoadingRack
        '
        Me.cboOperatingCapacityUnitsLoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsLoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsLoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsLoadingRack.Location = New System.Drawing.Point(583, 7)
        Me.cboOperatingCapacityUnitsLoadingRack.Name = "cboOperatingCapacityUnitsLoadingRack"
        Me.cboOperatingCapacityUnitsLoadingRack.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsLoadingRack.TabIndex = 133
        '
        'cboAllowableEmissionRateUnits2LoadingRack
        '
        Me.cboAllowableEmissionRateUnits2LoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits2LoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits2LoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits2LoadingRack.Location = New System.Drawing.Point(431, 29)
        Me.cboAllowableEmissionRateUnits2LoadingRack.Name = "cboAllowableEmissionRateUnits2LoadingRack"
        Me.cboAllowableEmissionRateUnits2LoadingRack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits2LoadingRack.TabIndex = 137
        '
        'cboAllowableEmissionRateUnits3LoadingRack
        '
        Me.cboAllowableEmissionRateUnits3LoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits3LoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits3LoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits3LoadingRack.Location = New System.Drawing.Point(639, 29)
        Me.cboAllowableEmissionRateUnits3LoadingRack.Name = "cboAllowableEmissionRateUnits3LoadingRack"
        Me.cboAllowableEmissionRateUnits3LoadingRack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits3LoadingRack.TabIndex = 139
        '
        'cboAllowableEmissionRateUnits1LoadingRack
        '
        Me.cboAllowableEmissionRateUnits1LoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits1LoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits1LoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits1LoadingRack.Location = New System.Drawing.Point(223, 29)
        Me.cboAllowableEmissionRateUnits1LoadingRack.Name = "cboAllowableEmissionRateUnits1LoadingRack"
        Me.cboAllowableEmissionRateUnits1LoadingRack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits1LoadingRack.TabIndex = 135
        '
        'cboMaximumExpectedOperatingCapacityUnitsLoadingRack
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack.Location = New System.Drawing.Point(223, 7)
        Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack.Name = "cboMaximumExpectedOperatingCapacityUnitsLoadingRack"
        Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsLoadingRack.TabIndex = 131
        '
        'Label169
        '
        Me.Label169.Location = New System.Drawing.Point(3, 83)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(112, 32)
        Me.Label169.TabIndex = 333
        Me.Label169.Text = "Control Equipment and Monitoring Data:"
        Me.Label169.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label170
        '
        Me.Label170.Location = New System.Drawing.Point(3, 5)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(104, 24)
        Me.Label170.TabIndex = 330
        Me.Label170.Text = "Maximum Expected Operating Capacity:"
        Me.Label170.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPPondTreatment
        '
        Me.TPPondTreatment.AutoScroll = True
        Me.TPPondTreatment.Controls.Add(Me.Label286)
        Me.TPPondTreatment.Controls.Add(Me.Label285)
        Me.TPPondTreatment.Controls.Add(Me.Label284)
        Me.TPPondTreatment.Controls.Add(Me.Label283)
        Me.TPPondTreatment.Controls.Add(Me.Label146)
        Me.TPPondTreatment.Controls.Add(Me.Label282)
        Me.TPPondTreatment.Controls.Add(Me.Label281)
        Me.TPPondTreatment.Controls.Add(Me.Label280)
        Me.TPPondTreatment.Controls.Add(Me.Label150)
        Me.TPPondTreatment.Controls.Add(Me.txtOtherInformationPond)
        Me.TPPondTreatment.Controls.Add(Me.Label137)
        Me.TPPondTreatment.Controls.Add(Me.txtDestructionEfficancyPond)
        Me.TPPondTreatment.Controls.Add(Me.Label140)
        Me.TPPondTreatment.Controls.Add(Me.Label142)
        Me.TPPondTreatment.Controls.Add(Me.txtTreatmentRateAvgPond)
        Me.TPPondTreatment.Controls.Add(Me.txtPollConcAvgPond)
        Me.TPPondTreatment.Controls.Add(Me.txtTreatmentRatePond1C)
        Me.TPPondTreatment.Controls.Add(Me.txtPollConcPond1C)
        Me.TPPondTreatment.Controls.Add(Me.txtRunNumPond1C)
        Me.TPPondTreatment.Controls.Add(Me.txtTreatmentRatePond1B)
        Me.TPPondTreatment.Controls.Add(Me.txtPollConcPond1B)
        Me.TPPondTreatment.Controls.Add(Me.txtRunNumPond1B)
        Me.TPPondTreatment.Controls.Add(Me.txtTreatmentRatePond1A)
        Me.TPPondTreatment.Controls.Add(Me.txtPollConcPond1A)
        Me.TPPondTreatment.Controls.Add(Me.txtRunNumPond1A)
        Me.TPPondTreatment.Controls.Add(Me.Label143)
        Me.TPPondTreatment.Controls.Add(Me.Label144)
        Me.TPPondTreatment.Controls.Add(Me.Label145)
        Me.TPPondTreatment.Controls.Add(Me.cboTreatmentRateUnitPond)
        Me.TPPondTreatment.Controls.Add(Me.cboPollConUnitPond)
        Me.TPPondTreatment.Controls.Add(Me.btnClearPondTreatment3)
        Me.TPPondTreatment.Controls.Add(Me.btnClearPondTreatment2)
        Me.TPPondTreatment.Controls.Add(Me.btnClearPondTreatment1)
        Me.TPPondTreatment.Controls.Add(Me.txtControlEquipmentOperatingDataPond)
        Me.TPPondTreatment.Controls.Add(Me.txtApplicableRegulationPond)
        Me.TPPondTreatment.Controls.Add(Me.txtOperatingCapacityPond)
        Me.TPPondTreatment.Controls.Add(Me.Label147)
        Me.TPPondTreatment.Controls.Add(Me.txtAllowableEmissionRate2Pond)
        Me.TPPondTreatment.Controls.Add(Me.txtAllowableEmissionRate3Pond)
        Me.TPPondTreatment.Controls.Add(Me.txtAllowableEmissionRate1Pond)
        Me.TPPondTreatment.Controls.Add(Me.txtMaximumExpectedOperatingCapacityPond)
        Me.TPPondTreatment.Controls.Add(Me.Label148)
        Me.TPPondTreatment.Controls.Add(Me.Label149)
        Me.TPPondTreatment.Controls.Add(Me.cboOperatingCapacityUnitsPond)
        Me.TPPondTreatment.Controls.Add(Me.cboAllowableEmissionRateUnits2Pond)
        Me.TPPondTreatment.Controls.Add(Me.cboAllowableEmissionRateUnits3Pond)
        Me.TPPondTreatment.Controls.Add(Me.cboAllowableEmissionRateUnits1Pond)
        Me.TPPondTreatment.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsPond)
        Me.TPPondTreatment.Controls.Add(Me.Label151)
        Me.TPPondTreatment.Location = New System.Drawing.Point(4, 22)
        Me.TPPondTreatment.Name = "TPPondTreatment"
        Me.TPPondTreatment.Size = New System.Drawing.Size(782, 288)
        Me.TPPondTreatment.TabIndex = 2
        Me.TPPondTreatment.Text = "Pulping Process Condensate"
        Me.TPPondTreatment.UseVisualStyleBackColor = True
        '
        'Label286
        '
        Me.Label286.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label286.Location = New System.Drawing.Point(3, 216)
        Me.Label286.Name = "Label286"
        Me.Label286.Size = New System.Drawing.Size(212, 1)
        Me.Label286.TabIndex = 354
        Me.Label286.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label285
        '
        Me.Label285.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label285.Location = New System.Drawing.Point(3, 195)
        Me.Label285.Name = "Label285"
        Me.Label285.Size = New System.Drawing.Size(668, 1)
        Me.Label285.TabIndex = 353
        Me.Label285.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label284
        '
        Me.Label284.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label284.Location = New System.Drawing.Point(3, 173)
        Me.Label284.Name = "Label284"
        Me.Label284.Size = New System.Drawing.Size(668, 1)
        Me.Label284.TabIndex = 352
        Me.Label284.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label283
        '
        Me.Label283.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label283.Location = New System.Drawing.Point(47, 151)
        Me.Label283.Name = "Label283"
        Me.Label283.Size = New System.Drawing.Size(392, 1)
        Me.Label283.TabIndex = 351
        Me.Label283.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label146
        '
        Me.Label146.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label146.Location = New System.Drawing.Point(3, 125)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(748, 1)
        Me.Label146.TabIndex = 350
        Me.Label146.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label282
        '
        Me.Label282.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label282.Location = New System.Drawing.Point(3, 83)
        Me.Label282.Name = "Label282"
        Me.Label282.Size = New System.Drawing.Size(748, 1)
        Me.Label282.TabIndex = 349
        Me.Label282.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label281
        '
        Me.Label281.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label281.Location = New System.Drawing.Point(3, 51)
        Me.Label281.Name = "Label281"
        Me.Label281.Size = New System.Drawing.Size(748, 1)
        Me.Label281.TabIndex = 348
        Me.Label281.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label280
        '
        Me.Label280.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label280.Location = New System.Drawing.Point(107, 29)
        Me.Label280.Name = "Label280"
        Me.Label280.Size = New System.Drawing.Size(588, 1)
        Me.Label280.TabIndex = 347
        Me.Label280.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label150
        '
        Me.Label150.Location = New System.Drawing.Point(3, 84)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(112, 32)
        Me.Label150.TabIndex = 346
        Me.Label150.Text = "Control Equipment and Monitoring Data:"
        Me.Label150.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtOtherInformationPond
        '
        Me.txtOtherInformationPond.AcceptsReturn = True
        Me.txtOtherInformationPond.Location = New System.Drawing.Point(135, 218)
        Me.txtOtherInformationPond.MaxLength = 4000
        Me.txtOtherInformationPond.Multiline = True
        Me.txtOtherInformationPond.Name = "txtOtherInformationPond"
        Me.txtOtherInformationPond.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationPond.Size = New System.Drawing.Size(616, 128)
        Me.txtOtherInformationPond.TabIndex = 186
        '
        'Label137
        '
        Me.Label137.AutoSize = True
        Me.Label137.Location = New System.Drawing.Point(3, 218)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(91, 13)
        Me.Label137.TabIndex = 345
        Me.Label137.Text = "Other Information:"
        Me.Label137.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtDestructionEfficancyPond
        '
        Me.txtDestructionEfficancyPond.Location = New System.Drawing.Point(135, 196)
        Me.txtDestructionEfficancyPond.MaxLength = 11
        Me.txtDestructionEfficancyPond.Name = "txtDestructionEfficancyPond"
        Me.txtDestructionEfficancyPond.Size = New System.Drawing.Size(80, 20)
        Me.txtDestructionEfficancyPond.TabIndex = 185
        '
        'Label140
        '
        Me.Label140.AutoSize = True
        Me.Label140.Location = New System.Drawing.Point(3, 196)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(128, 13)
        Me.Label140.TabIndex = 344
        Me.Label140.Text = "Destruction Efficancy (%):"
        Me.Label140.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label142
        '
        Me.Label142.AutoSize = True
        Me.Label142.Location = New System.Drawing.Point(599, 130)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(65, 13)
        Me.Label142.TabIndex = 338
        Me.Label142.Text = "AVERAGES"
        Me.Label142.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtTreatmentRateAvgPond
        '
        Me.txtTreatmentRateAvgPond.BackColor = System.Drawing.SystemColors.Window
        Me.txtTreatmentRateAvgPond.Location = New System.Drawing.Point(599, 174)
        Me.txtTreatmentRateAvgPond.MaxLength = 11
        Me.txtTreatmentRateAvgPond.Name = "txtTreatmentRateAvgPond"
        Me.txtTreatmentRateAvgPond.ReadOnly = True
        Me.txtTreatmentRateAvgPond.Size = New System.Drawing.Size(72, 20)
        Me.txtTreatmentRateAvgPond.TabIndex = 184
        Me.txtTreatmentRateAvgPond.TabStop = False
        '
        'txtPollConcAvgPond
        '
        Me.txtPollConcAvgPond.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgPond.Location = New System.Drawing.Point(599, 152)
        Me.txtPollConcAvgPond.MaxLength = 11
        Me.txtPollConcAvgPond.Name = "txtPollConcAvgPond"
        Me.txtPollConcAvgPond.ReadOnly = True
        Me.txtPollConcAvgPond.Size = New System.Drawing.Size(72, 20)
        Me.txtPollConcAvgPond.TabIndex = 179
        Me.txtPollConcAvgPond.TabStop = False
        '
        'txtTreatmentRatePond1C
        '
        Me.txtTreatmentRatePond1C.Location = New System.Drawing.Point(347, 174)
        Me.txtTreatmentRatePond1C.MaxLength = 11
        Me.txtTreatmentRatePond1C.Name = "txtTreatmentRatePond1C"
        Me.txtTreatmentRatePond1C.Size = New System.Drawing.Size(96, 20)
        Me.txtTreatmentRatePond1C.TabIndex = 182
        '
        'txtPollConcPond1C
        '
        Me.txtPollConcPond1C.Location = New System.Drawing.Point(347, 152)
        Me.txtPollConcPond1C.MaxLength = 11
        Me.txtPollConcPond1C.Name = "txtPollConcPond1C"
        Me.txtPollConcPond1C.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcPond1C.TabIndex = 177
        '
        'txtRunNumPond1C
        '
        Me.txtRunNumPond1C.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumPond1C.Location = New System.Drawing.Point(359, 130)
        Me.txtRunNumPond1C.MaxLength = 3
        Me.txtRunNumPond1C.Name = "txtRunNumPond1C"
        Me.txtRunNumPond1C.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumPond1C.TabIndex = 174
        '
        'txtTreatmentRatePond1B
        '
        Me.txtTreatmentRatePond1B.Location = New System.Drawing.Point(241, 174)
        Me.txtTreatmentRatePond1B.MaxLength = 11
        Me.txtTreatmentRatePond1B.Name = "txtTreatmentRatePond1B"
        Me.txtTreatmentRatePond1B.Size = New System.Drawing.Size(96, 20)
        Me.txtTreatmentRatePond1B.TabIndex = 181
        '
        'txtPollConcPond1B
        '
        Me.txtPollConcPond1B.Location = New System.Drawing.Point(241, 152)
        Me.txtPollConcPond1B.MaxLength = 11
        Me.txtPollConcPond1B.Name = "txtPollConcPond1B"
        Me.txtPollConcPond1B.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcPond1B.TabIndex = 176
        '
        'txtRunNumPond1B
        '
        Me.txtRunNumPond1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumPond1B.Location = New System.Drawing.Point(255, 130)
        Me.txtRunNumPond1B.MaxLength = 3
        Me.txtRunNumPond1B.Name = "txtRunNumPond1B"
        Me.txtRunNumPond1B.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumPond1B.TabIndex = 173
        '
        'txtTreatmentRatePond1A
        '
        Me.txtTreatmentRatePond1A.Location = New System.Drawing.Point(135, 174)
        Me.txtTreatmentRatePond1A.MaxLength = 11
        Me.txtTreatmentRatePond1A.Name = "txtTreatmentRatePond1A"
        Me.txtTreatmentRatePond1A.Size = New System.Drawing.Size(96, 20)
        Me.txtTreatmentRatePond1A.TabIndex = 180
        '
        'txtPollConcPond1A
        '
        Me.txtPollConcPond1A.Location = New System.Drawing.Point(135, 152)
        Me.txtPollConcPond1A.MaxLength = 11
        Me.txtPollConcPond1A.Name = "txtPollConcPond1A"
        Me.txtPollConcPond1A.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcPond1A.TabIndex = 175
        '
        'txtRunNumPond1A
        '
        Me.txtRunNumPond1A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumPond1A.Location = New System.Drawing.Point(151, 130)
        Me.txtRunNumPond1A.MaxLength = 3
        Me.txtRunNumPond1A.Name = "txtRunNumPond1A"
        Me.txtRunNumPond1A.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumPond1A.TabIndex = 172
        '
        'Label143
        '
        Me.Label143.AutoSize = True
        Me.Label143.Location = New System.Drawing.Point(3, 152)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(126, 13)
        Me.Label143.TabIndex = 337
        Me.Label143.Text = "Pollutant Collection Rate:"
        Me.Label143.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label144
        '
        Me.Label144.AutoSize = True
        Me.Label144.Location = New System.Drawing.Point(3, 176)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(84, 13)
        Me.Label144.TabIndex = 336
        Me.Label144.Text = "Treatment Rate:"
        Me.Label144.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label145
        '
        Me.Label145.AutoSize = True
        Me.Label145.Location = New System.Drawing.Point(47, 130)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(61, 13)
        Me.Label145.TabIndex = 335
        Me.Label145.Text = "Test Run #"
        Me.Label145.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboTreatmentRateUnitPond
        '
        Me.cboTreatmentRateUnitPond.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTreatmentRateUnitPond.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTreatmentRateUnitPond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTreatmentRateUnitPond.Location = New System.Drawing.Point(453, 174)
        Me.cboTreatmentRateUnitPond.Name = "cboTreatmentRateUnitPond"
        Me.cboTreatmentRateUnitPond.Size = New System.Drawing.Size(136, 21)
        Me.cboTreatmentRateUnitPond.TabIndex = 183
        '
        'cboPollConUnitPond
        '
        Me.cboPollConUnitPond.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitPond.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitPond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollConUnitPond.Location = New System.Drawing.Point(453, 152)
        Me.cboPollConUnitPond.Name = "cboPollConUnitPond"
        Me.cboPollConUnitPond.Size = New System.Drawing.Size(136, 21)
        Me.cboPollConUnitPond.TabIndex = 178
        '
        'btnClearPondTreatment3
        '
        Me.btnClearPondTreatment3.Location = New System.Drawing.Point(399, 130)
        Me.btnClearPondTreatment3.Name = "btnClearPondTreatment3"
        Me.btnClearPondTreatment3.Size = New System.Drawing.Size(40, 20)
        Me.btnClearPondTreatment3.TabIndex = 343
        Me.btnClearPondTreatment3.TabStop = False
        Me.btnClearPondTreatment3.Text = "Clear"
        '
        'btnClearPondTreatment2
        '
        Me.btnClearPondTreatment2.Location = New System.Drawing.Point(295, 130)
        Me.btnClearPondTreatment2.Name = "btnClearPondTreatment2"
        Me.btnClearPondTreatment2.Size = New System.Drawing.Size(40, 20)
        Me.btnClearPondTreatment2.TabIndex = 342
        Me.btnClearPondTreatment2.TabStop = False
        Me.btnClearPondTreatment2.Text = "Clear"
        '
        'btnClearPondTreatment1
        '
        Me.btnClearPondTreatment1.Location = New System.Drawing.Point(191, 130)
        Me.btnClearPondTreatment1.Name = "btnClearPondTreatment1"
        Me.btnClearPondTreatment1.Size = New System.Drawing.Size(40, 20)
        Me.btnClearPondTreatment1.TabIndex = 341
        Me.btnClearPondTreatment1.TabStop = False
        Me.btnClearPondTreatment1.Text = "Clear"
        '
        'txtControlEquipmentOperatingDataPond
        '
        Me.txtControlEquipmentOperatingDataPond.Location = New System.Drawing.Point(135, 84)
        Me.txtControlEquipmentOperatingDataPond.MaxLength = 4000
        Me.txtControlEquipmentOperatingDataPond.Multiline = True
        Me.txtControlEquipmentOperatingDataPond.Name = "txtControlEquipmentOperatingDataPond"
        Me.txtControlEquipmentOperatingDataPond.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlEquipmentOperatingDataPond.Size = New System.Drawing.Size(616, 41)
        Me.txtControlEquipmentOperatingDataPond.TabIndex = 171
        '
        'txtApplicableRegulationPond
        '
        Me.txtApplicableRegulationPond.AcceptsReturn = True
        Me.txtApplicableRegulationPond.Location = New System.Drawing.Point(135, 52)
        Me.txtApplicableRegulationPond.MaxLength = 200
        Me.txtApplicableRegulationPond.Multiline = True
        Me.txtApplicableRegulationPond.Name = "txtApplicableRegulationPond"
        Me.txtApplicableRegulationPond.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationPond.Size = New System.Drawing.Size(616, 32)
        Me.txtApplicableRegulationPond.TabIndex = 170
        '
        'txtOperatingCapacityPond
        '
        Me.txtOperatingCapacityPond.Location = New System.Drawing.Point(495, 8)
        Me.txtOperatingCapacityPond.MaxLength = 11
        Me.txtOperatingCapacityPond.Name = "txtOperatingCapacityPond"
        Me.txtOperatingCapacityPond.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityPond.TabIndex = 162
        '
        'Label147
        '
        Me.Label147.AutoSize = True
        Me.Label147.Location = New System.Drawing.Point(391, 8)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(100, 13)
        Me.Label147.TabIndex = 334
        Me.Label147.Text = "Operating Capacity:"
        Me.Label147.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAllowableEmissionRate2Pond
        '
        Me.txtAllowableEmissionRate2Pond.Location = New System.Drawing.Point(343, 30)
        Me.txtAllowableEmissionRate2Pond.MaxLength = 11
        Me.txtAllowableEmissionRate2Pond.Name = "txtAllowableEmissionRate2Pond"
        Me.txtAllowableEmissionRate2Pond.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate2Pond.TabIndex = 166
        '
        'txtAllowableEmissionRate3Pond
        '
        Me.txtAllowableEmissionRate3Pond.Location = New System.Drawing.Point(551, 30)
        Me.txtAllowableEmissionRate3Pond.MaxLength = 11
        Me.txtAllowableEmissionRate3Pond.Name = "txtAllowableEmissionRate3Pond"
        Me.txtAllowableEmissionRate3Pond.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate3Pond.TabIndex = 168
        '
        'txtAllowableEmissionRate1Pond
        '
        Me.txtAllowableEmissionRate1Pond.Location = New System.Drawing.Point(135, 30)
        Me.txtAllowableEmissionRate1Pond.MaxLength = 11
        Me.txtAllowableEmissionRate1Pond.Name = "txtAllowableEmissionRate1Pond"
        Me.txtAllowableEmissionRate1Pond.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate1Pond.TabIndex = 164
        '
        'txtMaximumExpectedOperatingCapacityPond
        '
        Me.txtMaximumExpectedOperatingCapacityPond.Location = New System.Drawing.Point(135, 8)
        Me.txtMaximumExpectedOperatingCapacityPond.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityPond.Name = "txtMaximumExpectedOperatingCapacityPond"
        Me.txtMaximumExpectedOperatingCapacityPond.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityPond.TabIndex = 160
        '
        'Label148
        '
        Me.Label148.AutoSize = True
        Me.Label148.Location = New System.Drawing.Point(3, 52)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(122, 13)
        Me.Label148.TabIndex = 333
        Me.Label148.Text = "Applicable Requirement:"
        Me.Label148.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label149
        '
        Me.Label149.AutoSize = True
        Me.Label149.Location = New System.Drawing.Point(3, 32)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(128, 13)
        Me.Label149.TabIndex = 332
        Me.Label149.Text = "Minimum Treatment Rate:"
        Me.Label149.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboOperatingCapacityUnitsPond
        '
        Me.cboOperatingCapacityUnitsPond.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsPond.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsPond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsPond.Location = New System.Drawing.Point(583, 8)
        Me.cboOperatingCapacityUnitsPond.Name = "cboOperatingCapacityUnitsPond"
        Me.cboOperatingCapacityUnitsPond.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsPond.TabIndex = 163
        '
        'cboAllowableEmissionRateUnits2Pond
        '
        Me.cboAllowableEmissionRateUnits2Pond.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits2Pond.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits2Pond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits2Pond.Location = New System.Drawing.Point(431, 30)
        Me.cboAllowableEmissionRateUnits2Pond.Name = "cboAllowableEmissionRateUnits2Pond"
        Me.cboAllowableEmissionRateUnits2Pond.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits2Pond.TabIndex = 167
        '
        'cboAllowableEmissionRateUnits3Pond
        '
        Me.cboAllowableEmissionRateUnits3Pond.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits3Pond.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits3Pond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits3Pond.Location = New System.Drawing.Point(639, 30)
        Me.cboAllowableEmissionRateUnits3Pond.Name = "cboAllowableEmissionRateUnits3Pond"
        Me.cboAllowableEmissionRateUnits3Pond.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits3Pond.TabIndex = 169
        '
        'cboAllowableEmissionRateUnits1Pond
        '
        Me.cboAllowableEmissionRateUnits1Pond.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits1Pond.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits1Pond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits1Pond.Location = New System.Drawing.Point(223, 30)
        Me.cboAllowableEmissionRateUnits1Pond.Name = "cboAllowableEmissionRateUnits1Pond"
        Me.cboAllowableEmissionRateUnits1Pond.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits1Pond.TabIndex = 165
        '
        'cboMaximumExpectedOperatingCapacityUnitsPond
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsPond.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsPond.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsPond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsPond.Location = New System.Drawing.Point(223, 8)
        Me.cboMaximumExpectedOperatingCapacityUnitsPond.Name = "cboMaximumExpectedOperatingCapacityUnitsPond"
        Me.cboMaximumExpectedOperatingCapacityUnitsPond.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsPond.TabIndex = 161
        '
        'Label151
        '
        Me.Label151.Location = New System.Drawing.Point(3, 6)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(104, 24)
        Me.Label151.TabIndex = 331
        Me.Label151.Text = "Maximum Expected Operating Capacity:"
        Me.Label151.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPGasConcentration
        '
        Me.TPGasConcentration.AutoScroll = True
        Me.TPGasConcentration.Controls.Add(Me.Label134)
        Me.TPGasConcentration.Controls.Add(Me.Label279)
        Me.TPGasConcentration.Controls.Add(Me.Label278)
        Me.TPGasConcentration.Controls.Add(Me.Label277)
        Me.TPGasConcentration.Controls.Add(Me.Label276)
        Me.TPGasConcentration.Controls.Add(Me.Label275)
        Me.TPGasConcentration.Controls.Add(Me.Label274)
        Me.TPGasConcentration.Controls.Add(Me.Label273)
        Me.TPGasConcentration.Controls.Add(Me.txtOtherInformationGas)
        Me.TPGasConcentration.Controls.Add(Me.Label136)
        Me.TPGasConcentration.Controls.Add(Me.txtPercentAllowableGas)
        Me.TPGasConcentration.Controls.Add(Me.Label135)
        Me.TPGasConcentration.Controls.Add(Me.txtEmissRateAvgGas)
        Me.TPGasConcentration.Controls.Add(Me.txtPollConcAvgGas)
        Me.TPGasConcentration.Controls.Add(Me.txtEmissRateGas1C)
        Me.TPGasConcentration.Controls.Add(Me.txtPollConcGas1C)
        Me.TPGasConcentration.Controls.Add(Me.txtRunNumGas1C)
        Me.TPGasConcentration.Controls.Add(Me.txtEmissRateGas1B)
        Me.TPGasConcentration.Controls.Add(Me.txtPollConcGas1B)
        Me.TPGasConcentration.Controls.Add(Me.txtRunNumGas1B)
        Me.TPGasConcentration.Controls.Add(Me.txtEmissRateGas1A)
        Me.TPGasConcentration.Controls.Add(Me.txtPollConcGas1A)
        Me.TPGasConcentration.Controls.Add(Me.txtRunNumGas1A)
        Me.TPGasConcentration.Controls.Add(Me.Label138)
        Me.TPGasConcentration.Controls.Add(Me.Label139)
        Me.TPGasConcentration.Controls.Add(Me.Label141)
        Me.TPGasConcentration.Controls.Add(Me.cboEmissRateUnitGas)
        Me.TPGasConcentration.Controls.Add(Me.cboPollConUnitGas)
        Me.TPGasConcentration.Controls.Add(Me.btnClearGasConcentration3)
        Me.TPGasConcentration.Controls.Add(Me.btnClearGasConcentration2)
        Me.TPGasConcentration.Controls.Add(Me.btnClearGasConcentration1)
        Me.TPGasConcentration.Controls.Add(Me.Label124)
        Me.TPGasConcentration.Controls.Add(Me.txtControlEquipmentOperatingDataGas)
        Me.TPGasConcentration.Controls.Add(Me.txtApplicableRegulationGas)
        Me.TPGasConcentration.Controls.Add(Me.txtOperatingCapacityGas)
        Me.TPGasConcentration.Controls.Add(Me.Label129)
        Me.TPGasConcentration.Controls.Add(Me.txtAllowableEmissionRate2Gas)
        Me.TPGasConcentration.Controls.Add(Me.txtAllowableEmissionRate3Gas)
        Me.TPGasConcentration.Controls.Add(Me.txtAllowableEmissionRate1Gas)
        Me.TPGasConcentration.Controls.Add(Me.txtMaximumExpectedOperatingCapacityGas)
        Me.TPGasConcentration.Controls.Add(Me.Label130)
        Me.TPGasConcentration.Controls.Add(Me.Label131)
        Me.TPGasConcentration.Controls.Add(Me.cboOperatingCapacityUnitsGas)
        Me.TPGasConcentration.Controls.Add(Me.cboAllowableEmissionRateUnits2Gas)
        Me.TPGasConcentration.Controls.Add(Me.cboAllowableEmissionRateUnits3Gas)
        Me.TPGasConcentration.Controls.Add(Me.cboAllowableEmissionRateUnits1Gas)
        Me.TPGasConcentration.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsGas)
        Me.TPGasConcentration.Controls.Add(Me.Label132)
        Me.TPGasConcentration.Controls.Add(Me.Label133)
        Me.TPGasConcentration.Location = New System.Drawing.Point(4, 22)
        Me.TPGasConcentration.Name = "TPGasConcentration"
        Me.TPGasConcentration.Size = New System.Drawing.Size(782, 288)
        Me.TPGasConcentration.TabIndex = 3
        Me.TPGasConcentration.Text = "Gas Concentration"
        Me.TPGasConcentration.UseVisualStyleBackColor = True
        '
        'Label134
        '
        Me.Label134.AutoSize = True
        Me.Label134.Location = New System.Drawing.Point(599, 130)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(65, 13)
        Me.Label134.TabIndex = 337
        Me.Label134.Text = "AVERAGES"
        Me.Label134.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label279
        '
        Me.Label279.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label279.Location = New System.Drawing.Point(47, 151)
        Me.Label279.Name = "Label279"
        Me.Label279.Size = New System.Drawing.Size(392, 1)
        Me.Label279.TabIndex = 336
        Me.Label279.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label278
        '
        Me.Label278.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label278.Location = New System.Drawing.Point(3, 173)
        Me.Label278.Name = "Label278"
        Me.Label278.Size = New System.Drawing.Size(668, 1)
        Me.Label278.TabIndex = 335
        Me.Label278.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label277
        '
        Me.Label277.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label277.Location = New System.Drawing.Point(3, 195)
        Me.Label277.Name = "Label277"
        Me.Label277.Size = New System.Drawing.Size(668, 1)
        Me.Label277.TabIndex = 334
        Me.Label277.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label276
        '
        Me.Label276.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label276.Location = New System.Drawing.Point(3, 216)
        Me.Label276.Name = "Label276"
        Me.Label276.Size = New System.Drawing.Size(212, 1)
        Me.Label276.TabIndex = 333
        Me.Label276.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label275
        '
        Me.Label275.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label275.Location = New System.Drawing.Point(3, 125)
        Me.Label275.Name = "Label275"
        Me.Label275.Size = New System.Drawing.Size(748, 1)
        Me.Label275.TabIndex = 332
        Me.Label275.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label274
        '
        Me.Label274.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label274.Location = New System.Drawing.Point(3, 83)
        Me.Label274.Name = "Label274"
        Me.Label274.Size = New System.Drawing.Size(748, 1)
        Me.Label274.TabIndex = 331
        Me.Label274.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label273
        '
        Me.Label273.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label273.Location = New System.Drawing.Point(3, 51)
        Me.Label273.Name = "Label273"
        Me.Label273.Size = New System.Drawing.Size(748, 1)
        Me.Label273.TabIndex = 330
        Me.Label273.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOtherInformationGas
        '
        Me.txtOtherInformationGas.AcceptsReturn = True
        Me.txtOtherInformationGas.Location = New System.Drawing.Point(135, 218)
        Me.txtOtherInformationGas.MaxLength = 4000
        Me.txtOtherInformationGas.Multiline = True
        Me.txtOtherInformationGas.Name = "txtOtherInformationGas"
        Me.txtOtherInformationGas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationGas.Size = New System.Drawing.Size(616, 128)
        Me.txtOtherInformationGas.TabIndex = 216
        '
        'Label136
        '
        Me.Label136.AutoSize = True
        Me.Label136.Location = New System.Drawing.Point(3, 218)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(91, 13)
        Me.Label136.TabIndex = 329
        Me.Label136.Text = "Other Information:"
        Me.Label136.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtPercentAllowableGas
        '
        Me.txtPercentAllowableGas.Location = New System.Drawing.Point(135, 196)
        Me.txtPercentAllowableGas.MaxLength = 11
        Me.txtPercentAllowableGas.Name = "txtPercentAllowableGas"
        Me.txtPercentAllowableGas.Size = New System.Drawing.Size(80, 20)
        Me.txtPercentAllowableGas.TabIndex = 215
        '
        'Label135
        '
        Me.Label135.AutoSize = True
        Me.Label135.Location = New System.Drawing.Point(3, 196)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(95, 13)
        Me.Label135.TabIndex = 328
        Me.Label135.Text = "Percent Allowable:"
        Me.Label135.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtEmissRateAvgGas
        '
        Me.txtEmissRateAvgGas.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateAvgGas.Location = New System.Drawing.Point(599, 174)
        Me.txtEmissRateAvgGas.MaxLength = 11
        Me.txtEmissRateAvgGas.Name = "txtEmissRateAvgGas"
        Me.txtEmissRateAvgGas.ReadOnly = True
        Me.txtEmissRateAvgGas.Size = New System.Drawing.Size(72, 20)
        Me.txtEmissRateAvgGas.TabIndex = 214
        Me.txtEmissRateAvgGas.TabStop = False
        '
        'txtPollConcAvgGas
        '
        Me.txtPollConcAvgGas.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgGas.Location = New System.Drawing.Point(599, 152)
        Me.txtPollConcAvgGas.MaxLength = 11
        Me.txtPollConcAvgGas.Name = "txtPollConcAvgGas"
        Me.txtPollConcAvgGas.ReadOnly = True
        Me.txtPollConcAvgGas.Size = New System.Drawing.Size(72, 20)
        Me.txtPollConcAvgGas.TabIndex = 209
        Me.txtPollConcAvgGas.TabStop = False
        '
        'txtEmissRateGas1C
        '
        Me.txtEmissRateGas1C.Location = New System.Drawing.Point(347, 174)
        Me.txtEmissRateGas1C.MaxLength = 11
        Me.txtEmissRateGas1C.Name = "txtEmissRateGas1C"
        Me.txtEmissRateGas1C.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateGas1C.TabIndex = 212
        '
        'txtPollConcGas1C
        '
        Me.txtPollConcGas1C.Location = New System.Drawing.Point(347, 152)
        Me.txtPollConcGas1C.MaxLength = 11
        Me.txtPollConcGas1C.Name = "txtPollConcGas1C"
        Me.txtPollConcGas1C.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcGas1C.TabIndex = 207
        '
        'txtRunNumGas1C
        '
        Me.txtRunNumGas1C.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumGas1C.Location = New System.Drawing.Point(359, 130)
        Me.txtRunNumGas1C.MaxLength = 3
        Me.txtRunNumGas1C.Name = "txtRunNumGas1C"
        Me.txtRunNumGas1C.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumGas1C.TabIndex = 204
        '
        'txtEmissRateGas1B
        '
        Me.txtEmissRateGas1B.Location = New System.Drawing.Point(241, 174)
        Me.txtEmissRateGas1B.MaxLength = 11
        Me.txtEmissRateGas1B.Name = "txtEmissRateGas1B"
        Me.txtEmissRateGas1B.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateGas1B.TabIndex = 211
        '
        'txtPollConcGas1B
        '
        Me.txtPollConcGas1B.Location = New System.Drawing.Point(241, 152)
        Me.txtPollConcGas1B.MaxLength = 11
        Me.txtPollConcGas1B.Name = "txtPollConcGas1B"
        Me.txtPollConcGas1B.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcGas1B.TabIndex = 206
        '
        'txtRunNumGas1B
        '
        Me.txtRunNumGas1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumGas1B.Location = New System.Drawing.Point(255, 130)
        Me.txtRunNumGas1B.MaxLength = 3
        Me.txtRunNumGas1B.Name = "txtRunNumGas1B"
        Me.txtRunNumGas1B.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumGas1B.TabIndex = 203
        '
        'txtEmissRateGas1A
        '
        Me.txtEmissRateGas1A.Location = New System.Drawing.Point(135, 174)
        Me.txtEmissRateGas1A.MaxLength = 11
        Me.txtEmissRateGas1A.Name = "txtEmissRateGas1A"
        Me.txtEmissRateGas1A.Size = New System.Drawing.Size(96, 20)
        Me.txtEmissRateGas1A.TabIndex = 210
        '
        'txtPollConcGas1A
        '
        Me.txtPollConcGas1A.Location = New System.Drawing.Point(135, 152)
        Me.txtPollConcGas1A.MaxLength = 11
        Me.txtPollConcGas1A.Name = "txtPollConcGas1A"
        Me.txtPollConcGas1A.Size = New System.Drawing.Size(96, 20)
        Me.txtPollConcGas1A.TabIndex = 205
        '
        'txtRunNumGas1A
        '
        Me.txtRunNumGas1A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumGas1A.Location = New System.Drawing.Point(151, 130)
        Me.txtRunNumGas1A.MaxLength = 3
        Me.txtRunNumGas1A.Name = "txtRunNumGas1A"
        Me.txtRunNumGas1A.Size = New System.Drawing.Size(32, 20)
        Me.txtRunNumGas1A.TabIndex = 202
        '
        'Label138
        '
        Me.Label138.AutoSize = True
        Me.Label138.Location = New System.Drawing.Point(3, 152)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(120, 13)
        Me.Label138.TabIndex = 322
        Me.Label138.Text = "Pollutant Concentration:"
        Me.Label138.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label139
        '
        Me.Label139.AutoSize = True
        Me.Label139.Location = New System.Drawing.Point(3, 176)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(77, 13)
        Me.Label139.TabIndex = 321
        Me.Label139.Text = "Emission Rate:"
        Me.Label139.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label141
        '
        Me.Label141.AutoSize = True
        Me.Label141.Location = New System.Drawing.Point(47, 130)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(61, 13)
        Me.Label141.TabIndex = 320
        Me.Label141.Text = "Test Run #"
        Me.Label141.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboEmissRateUnitGas
        '
        Me.cboEmissRateUnitGas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEmissRateUnitGas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEmissRateUnitGas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmissRateUnitGas.Location = New System.Drawing.Point(453, 174)
        Me.cboEmissRateUnitGas.Name = "cboEmissRateUnitGas"
        Me.cboEmissRateUnitGas.Size = New System.Drawing.Size(136, 21)
        Me.cboEmissRateUnitGas.TabIndex = 213
        '
        'cboPollConUnitGas
        '
        Me.cboPollConUnitGas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitGas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitGas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollConUnitGas.Location = New System.Drawing.Point(453, 152)
        Me.cboPollConUnitGas.Name = "cboPollConUnitGas"
        Me.cboPollConUnitGas.Size = New System.Drawing.Size(136, 21)
        Me.cboPollConUnitGas.TabIndex = 208
        '
        'btnClearGasConcentration3
        '
        Me.btnClearGasConcentration3.Location = New System.Drawing.Point(399, 130)
        Me.btnClearGasConcentration3.Name = "btnClearGasConcentration3"
        Me.btnClearGasConcentration3.Size = New System.Drawing.Size(40, 20)
        Me.btnClearGasConcentration3.TabIndex = 327
        Me.btnClearGasConcentration3.TabStop = False
        Me.btnClearGasConcentration3.Text = "Clear"
        '
        'btnClearGasConcentration2
        '
        Me.btnClearGasConcentration2.Location = New System.Drawing.Point(295, 130)
        Me.btnClearGasConcentration2.Name = "btnClearGasConcentration2"
        Me.btnClearGasConcentration2.Size = New System.Drawing.Size(40, 20)
        Me.btnClearGasConcentration2.TabIndex = 326
        Me.btnClearGasConcentration2.TabStop = False
        Me.btnClearGasConcentration2.Text = "Clear"
        '
        'btnClearGasConcentration1
        '
        Me.btnClearGasConcentration1.Location = New System.Drawing.Point(191, 130)
        Me.btnClearGasConcentration1.Name = "btnClearGasConcentration1"
        Me.btnClearGasConcentration1.Size = New System.Drawing.Size(40, 20)
        Me.btnClearGasConcentration1.TabIndex = 325
        Me.btnClearGasConcentration1.TabStop = False
        Me.btnClearGasConcentration1.Text = "Clear"
        '
        'Label124
        '
        Me.Label124.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label124.Location = New System.Drawing.Point(107, 29)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(588, 1)
        Me.Label124.TabIndex = 297
        Me.Label124.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtControlEquipmentOperatingDataGas
        '
        Me.txtControlEquipmentOperatingDataGas.Location = New System.Drawing.Point(135, 84)
        Me.txtControlEquipmentOperatingDataGas.MaxLength = 4000
        Me.txtControlEquipmentOperatingDataGas.Multiline = True
        Me.txtControlEquipmentOperatingDataGas.Name = "txtControlEquipmentOperatingDataGas"
        Me.txtControlEquipmentOperatingDataGas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlEquipmentOperatingDataGas.Size = New System.Drawing.Size(616, 41)
        Me.txtControlEquipmentOperatingDataGas.TabIndex = 201
        '
        'txtApplicableRegulationGas
        '
        Me.txtApplicableRegulationGas.AcceptsReturn = True
        Me.txtApplicableRegulationGas.Location = New System.Drawing.Point(135, 52)
        Me.txtApplicableRegulationGas.MaxLength = 200
        Me.txtApplicableRegulationGas.Multiline = True
        Me.txtApplicableRegulationGas.Name = "txtApplicableRegulationGas"
        Me.txtApplicableRegulationGas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationGas.Size = New System.Drawing.Size(616, 32)
        Me.txtApplicableRegulationGas.TabIndex = 200
        '
        'txtOperatingCapacityGas
        '
        Me.txtOperatingCapacityGas.Location = New System.Drawing.Point(495, 8)
        Me.txtOperatingCapacityGas.MaxLength = 11
        Me.txtOperatingCapacityGas.Name = "txtOperatingCapacityGas"
        Me.txtOperatingCapacityGas.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityGas.TabIndex = 192
        '
        'Label129
        '
        Me.Label129.AutoSize = True
        Me.Label129.Location = New System.Drawing.Point(391, 8)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(100, 13)
        Me.Label129.TabIndex = 294
        Me.Label129.Text = "Operating Capacity:"
        Me.Label129.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAllowableEmissionRate2Gas
        '
        Me.txtAllowableEmissionRate2Gas.Location = New System.Drawing.Point(343, 30)
        Me.txtAllowableEmissionRate2Gas.MaxLength = 11
        Me.txtAllowableEmissionRate2Gas.Name = "txtAllowableEmissionRate2Gas"
        Me.txtAllowableEmissionRate2Gas.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate2Gas.TabIndex = 196
        '
        'txtAllowableEmissionRate3Gas
        '
        Me.txtAllowableEmissionRate3Gas.Location = New System.Drawing.Point(551, 30)
        Me.txtAllowableEmissionRate3Gas.MaxLength = 11
        Me.txtAllowableEmissionRate3Gas.Name = "txtAllowableEmissionRate3Gas"
        Me.txtAllowableEmissionRate3Gas.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate3Gas.TabIndex = 198
        '
        'txtAllowableEmissionRate1Gas
        '
        Me.txtAllowableEmissionRate1Gas.Location = New System.Drawing.Point(135, 30)
        Me.txtAllowableEmissionRate1Gas.MaxLength = 11
        Me.txtAllowableEmissionRate1Gas.Name = "txtAllowableEmissionRate1Gas"
        Me.txtAllowableEmissionRate1Gas.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate1Gas.TabIndex = 194
        '
        'txtMaximumExpectedOperatingCapacityGas
        '
        Me.txtMaximumExpectedOperatingCapacityGas.Location = New System.Drawing.Point(135, 8)
        Me.txtMaximumExpectedOperatingCapacityGas.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityGas.Name = "txtMaximumExpectedOperatingCapacityGas"
        Me.txtMaximumExpectedOperatingCapacityGas.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityGas.TabIndex = 190
        '
        'Label130
        '
        Me.Label130.AutoSize = True
        Me.Label130.Location = New System.Drawing.Point(3, 52)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(122, 13)
        Me.Label130.TabIndex = 292
        Me.Label130.Text = "Applicable Requirement:"
        Me.Label130.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label131
        '
        Me.Label131.AutoSize = True
        Me.Label131.Location = New System.Drawing.Point(3, 32)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(125, 13)
        Me.Label131.TabIndex = 291
        Me.Label131.Text = "Allowable Emission Rate:"
        Me.Label131.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboOperatingCapacityUnitsGas
        '
        Me.cboOperatingCapacityUnitsGas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsGas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsGas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsGas.Location = New System.Drawing.Point(583, 8)
        Me.cboOperatingCapacityUnitsGas.Name = "cboOperatingCapacityUnitsGas"
        Me.cboOperatingCapacityUnitsGas.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsGas.TabIndex = 193
        '
        'cboAllowableEmissionRateUnits2Gas
        '
        Me.cboAllowableEmissionRateUnits2Gas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits2Gas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits2Gas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits2Gas.Location = New System.Drawing.Point(431, 30)
        Me.cboAllowableEmissionRateUnits2Gas.Name = "cboAllowableEmissionRateUnits2Gas"
        Me.cboAllowableEmissionRateUnits2Gas.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits2Gas.TabIndex = 197
        '
        'cboAllowableEmissionRateUnits3Gas
        '
        Me.cboAllowableEmissionRateUnits3Gas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits3Gas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits3Gas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits3Gas.Location = New System.Drawing.Point(639, 30)
        Me.cboAllowableEmissionRateUnits3Gas.Name = "cboAllowableEmissionRateUnits3Gas"
        Me.cboAllowableEmissionRateUnits3Gas.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits3Gas.TabIndex = 199
        '
        'cboAllowableEmissionRateUnits1Gas
        '
        Me.cboAllowableEmissionRateUnits1Gas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits1Gas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits1Gas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits1Gas.Location = New System.Drawing.Point(223, 30)
        Me.cboAllowableEmissionRateUnits1Gas.Name = "cboAllowableEmissionRateUnits1Gas"
        Me.cboAllowableEmissionRateUnits1Gas.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits1Gas.TabIndex = 195
        '
        'cboMaximumExpectedOperatingCapacityUnitsGas
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsGas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsGas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsGas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsGas.Location = New System.Drawing.Point(223, 8)
        Me.cboMaximumExpectedOperatingCapacityUnitsGas.Name = "cboMaximumExpectedOperatingCapacityUnitsGas"
        Me.cboMaximumExpectedOperatingCapacityUnitsGas.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsGas.TabIndex = 191
        '
        'Label132
        '
        Me.Label132.Location = New System.Drawing.Point(3, 84)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(112, 32)
        Me.Label132.TabIndex = 293
        Me.Label132.Text = "Control Equipment and Monitoring Data:"
        Me.Label132.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label133
        '
        Me.Label133.Location = New System.Drawing.Point(3, 6)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(104, 24)
        Me.Label133.TabIndex = 290
        Me.Label133.Text = "Maximum Expected Operating Capacity:"
        Me.Label133.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPFlare
        '
        Me.TPFlare.AutoScroll = True
        Me.TPFlare.Controls.Add(Me.Label272)
        Me.TPFlare.Controls.Add(Me.Label271)
        Me.TPFlare.Controls.Add(Me.Label270)
        Me.TPFlare.Controls.Add(Me.Label269)
        Me.TPFlare.Controls.Add(Me.Label268)
        Me.TPFlare.Controls.Add(Me.Label267)
        Me.TPFlare.Controls.Add(Me.txtOtherInformationFlare)
        Me.TPFlare.Controls.Add(Me.txtPercentAllowableFlare)
        Me.TPFlare.Controls.Add(Me.Label113)
        Me.TPFlare.Controls.Add(Me.txtHeatingValuesAvgFlare)
        Me.TPFlare.Controls.Add(Me.txtVelocityAvgFlare)
        Me.TPFlare.Controls.Add(Me.txtVelocity1BFlare)
        Me.TPFlare.Controls.Add(Me.cboHeatingValueUnits)
        Me.TPFlare.Controls.Add(Me.cboVelocityUnitsFlare)
        Me.TPFlare.Controls.Add(Me.txtHeatingValue1AFlare)
        Me.TPFlare.Controls.Add(Me.txtHeatingValue1BFlare)
        Me.TPFlare.Controls.Add(Me.txtHeatingValue1CFlare)
        Me.TPFlare.Controls.Add(Me.txtVelocity1AFlare)
        Me.TPFlare.Controls.Add(Me.txtVelocity1CFlare)
        Me.TPFlare.Controls.Add(Me.Label121)
        Me.TPFlare.Controls.Add(Me.Label120)
        Me.TPFlare.Controls.Add(Me.Label119)
        Me.TPFlare.Controls.Add(Me.Label118)
        Me.TPFlare.Controls.Add(Me.Label116)
        Me.TPFlare.Controls.Add(Me.Label115)
        Me.TPFlare.Controls.Add(Me.Label114)
        Me.TPFlare.Controls.Add(Me.Label112)
        Me.TPFlare.Controls.Add(Me.Label111)
        Me.TPFlare.Controls.Add(Me.txtHeatContentFlare)
        Me.TPFlare.Controls.Add(Me.Label110)
        Me.TPFlare.Controls.Add(Me.Label109)
        Me.TPFlare.Controls.Add(Me.Label103)
        Me.TPFlare.Controls.Add(Me.txtMonitoringDataFlare)
        Me.TPFlare.Controls.Add(Me.txtApplicableRegulationFlare)
        Me.TPFlare.Controls.Add(Me.txtOperatingCapacityFlare)
        Me.TPFlare.Controls.Add(Me.Label104)
        Me.TPFlare.Controls.Add(Me.txtVelocityFlare)
        Me.TPFlare.Controls.Add(Me.txtMaximumExpectedOperatingCapacityFlare)
        Me.TPFlare.Controls.Add(Me.Label105)
        Me.TPFlare.Controls.Add(Me.Label106)
        Me.TPFlare.Controls.Add(Me.cboOperatingCapacityUnitsFlare)
        Me.TPFlare.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsFlare)
        Me.TPFlare.Controls.Add(Me.Label107)
        Me.TPFlare.Controls.Add(Me.Label108)
        Me.TPFlare.Location = New System.Drawing.Point(4, 22)
        Me.TPFlare.Name = "TPFlare"
        Me.TPFlare.Size = New System.Drawing.Size(782, 288)
        Me.TPFlare.TabIndex = 4
        Me.TPFlare.Text = "Flare"
        Me.TPFlare.UseVisualStyleBackColor = True
        '
        'Label272
        '
        Me.Label272.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label272.Location = New System.Drawing.Point(3, 212)
        Me.Label272.Name = "Label272"
        Me.Label272.Size = New System.Drawing.Size(316, 1)
        Me.Label272.TabIndex = 364
        Me.Label272.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label271
        '
        Me.Label271.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label271.Location = New System.Drawing.Point(3, 190)
        Me.Label271.Name = "Label271"
        Me.Label271.Size = New System.Drawing.Size(644, 1)
        Me.Label271.TabIndex = 363
        Me.Label271.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label270
        '
        Me.Label270.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label270.Location = New System.Drawing.Point(3, 168)
        Me.Label270.Name = "Label270"
        Me.Label270.Size = New System.Drawing.Size(644, 1)
        Me.Label270.TabIndex = 362
        Me.Label270.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label269
        '
        Me.Label269.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label269.Location = New System.Drawing.Point(3, 127)
        Me.Label269.Name = "Label269"
        Me.Label269.Size = New System.Drawing.Size(748, 1)
        Me.Label269.TabIndex = 361
        Me.Label269.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label268
        '
        Me.Label268.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label268.Location = New System.Drawing.Point(3, 85)
        Me.Label268.Name = "Label268"
        Me.Label268.Size = New System.Drawing.Size(748, 1)
        Me.Label268.TabIndex = 360
        Me.Label268.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label267
        '
        Me.Label267.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label267.Location = New System.Drawing.Point(3, 53)
        Me.Label267.Name = "Label267"
        Me.Label267.Size = New System.Drawing.Size(596, 1)
        Me.Label267.TabIndex = 359
        Me.Label267.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOtherInformationFlare
        '
        Me.txtOtherInformationFlare.AcceptsReturn = True
        Me.txtOtherInformationFlare.Location = New System.Drawing.Point(135, 214)
        Me.txtOtherInformationFlare.MaxLength = 4000
        Me.txtOtherInformationFlare.Multiline = True
        Me.txtOtherInformationFlare.Name = "txtOtherInformationFlare"
        Me.txtOtherInformationFlare.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationFlare.Size = New System.Drawing.Size(592, 136)
        Me.txtOtherInformationFlare.TabIndex = 239
        '
        'txtPercentAllowableFlare
        '
        Me.txtPercentAllowableFlare.Location = New System.Drawing.Point(135, 191)
        Me.txtPercentAllowableFlare.MaxLength = 11
        Me.txtPercentAllowableFlare.Name = "txtPercentAllowableFlare"
        Me.txtPercentAllowableFlare.Size = New System.Drawing.Size(184, 20)
        Me.txtPercentAllowableFlare.TabIndex = 238
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.Location = New System.Drawing.Point(577, 132)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(52, 13)
        Me.Label113.TabIndex = 358
        Me.Label113.Text = "Averages"
        Me.Label113.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeatingValuesAvgFlare
        '
        Me.txtHeatingValuesAvgFlare.BackColor = System.Drawing.SystemColors.Window
        Me.txtHeatingValuesAvgFlare.Location = New System.Drawing.Point(559, 147)
        Me.txtHeatingValuesAvgFlare.MaxLength = 11
        Me.txtHeatingValuesAvgFlare.Name = "txtHeatingValuesAvgFlare"
        Me.txtHeatingValuesAvgFlare.ReadOnly = True
        Me.txtHeatingValuesAvgFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtHeatingValuesAvgFlare.TabIndex = 232
        Me.txtHeatingValuesAvgFlare.TabStop = False
        '
        'txtVelocityAvgFlare
        '
        Me.txtVelocityAvgFlare.BackColor = System.Drawing.SystemColors.Window
        Me.txtVelocityAvgFlare.Location = New System.Drawing.Point(559, 169)
        Me.txtVelocityAvgFlare.MaxLength = 11
        Me.txtVelocityAvgFlare.Name = "txtVelocityAvgFlare"
        Me.txtVelocityAvgFlare.ReadOnly = True
        Me.txtVelocityAvgFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtVelocityAvgFlare.TabIndex = 237
        Me.txtVelocityAvgFlare.TabStop = False
        '
        'txtVelocity1BFlare
        '
        Me.txtVelocity1BFlare.Location = New System.Drawing.Point(231, 169)
        Me.txtVelocity1BFlare.MaxLength = 11
        Me.txtVelocity1BFlare.Name = "txtVelocity1BFlare"
        Me.txtVelocity1BFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtVelocity1BFlare.TabIndex = 234
        '
        'cboHeatingValueUnits
        '
        Me.cboHeatingValueUnits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboHeatingValueUnits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboHeatingValueUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHeatingValueUnits.Location = New System.Drawing.Point(423, 147)
        Me.cboHeatingValueUnits.Name = "cboHeatingValueUnits"
        Me.cboHeatingValueUnits.Size = New System.Drawing.Size(112, 21)
        Me.cboHeatingValueUnits.TabIndex = 231
        '
        'cboVelocityUnitsFlare
        '
        Me.cboVelocityUnitsFlare.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboVelocityUnitsFlare.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboVelocityUnitsFlare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVelocityUnitsFlare.Location = New System.Drawing.Point(423, 169)
        Me.cboVelocityUnitsFlare.Name = "cboVelocityUnitsFlare"
        Me.cboVelocityUnitsFlare.Size = New System.Drawing.Size(112, 21)
        Me.cboVelocityUnitsFlare.TabIndex = 236
        '
        'txtHeatingValue1AFlare
        '
        Me.txtHeatingValue1AFlare.Location = New System.Drawing.Point(135, 147)
        Me.txtHeatingValue1AFlare.MaxLength = 11
        Me.txtHeatingValue1AFlare.Name = "txtHeatingValue1AFlare"
        Me.txtHeatingValue1AFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtHeatingValue1AFlare.TabIndex = 228
        '
        'txtHeatingValue1BFlare
        '
        Me.txtHeatingValue1BFlare.Location = New System.Drawing.Point(231, 147)
        Me.txtHeatingValue1BFlare.MaxLength = 11
        Me.txtHeatingValue1BFlare.Name = "txtHeatingValue1BFlare"
        Me.txtHeatingValue1BFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtHeatingValue1BFlare.TabIndex = 229
        '
        'txtHeatingValue1CFlare
        '
        Me.txtHeatingValue1CFlare.Location = New System.Drawing.Point(327, 147)
        Me.txtHeatingValue1CFlare.MaxLength = 11
        Me.txtHeatingValue1CFlare.Name = "txtHeatingValue1CFlare"
        Me.txtHeatingValue1CFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtHeatingValue1CFlare.TabIndex = 230
        '
        'txtVelocity1AFlare
        '
        Me.txtVelocity1AFlare.Location = New System.Drawing.Point(135, 169)
        Me.txtVelocity1AFlare.MaxLength = 11
        Me.txtVelocity1AFlare.Name = "txtVelocity1AFlare"
        Me.txtVelocity1AFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtVelocity1AFlare.TabIndex = 233
        '
        'txtVelocity1CFlare
        '
        Me.txtVelocity1CFlare.Location = New System.Drawing.Point(327, 169)
        Me.txtVelocity1CFlare.MaxLength = 11
        Me.txtVelocity1CFlare.Name = "txtVelocity1CFlare"
        Me.txtVelocity1CFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtVelocity1CFlare.TabIndex = 235
        '
        'Label121
        '
        Me.Label121.AutoSize = True
        Me.Label121.Location = New System.Drawing.Point(3, 214)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(91, 13)
        Me.Label121.TabIndex = 355
        Me.Label121.Text = "Other Information:"
        Me.Label121.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label120
        '
        Me.Label120.AutoSize = True
        Me.Label120.Location = New System.Drawing.Point(3, 191)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(95, 13)
        Me.Label120.TabIndex = 354
        Me.Label120.Text = "Percent Allowable:"
        Me.Label120.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label119
        '
        Me.Label119.AutoSize = True
        Me.Label119.Location = New System.Drawing.Point(3, 169)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(47, 13)
        Me.Label119.TabIndex = 353
        Me.Label119.Text = "Velocity:"
        Me.Label119.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label118
        '
        Me.Label118.AutoSize = True
        Me.Label118.Location = New System.Drawing.Point(3, 147)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(77, 13)
        Me.Label118.TabIndex = 352
        Me.Label118.Text = "Heating Value:"
        Me.Label118.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.Location = New System.Drawing.Point(179, 132)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(13, 13)
        Me.Label116.TabIndex = 351
        Me.Label116.Text = "1"
        Me.Label116.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Location = New System.Drawing.Point(275, 132)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(13, 13)
        Me.Label115.TabIndex = 350
        Me.Label115.Text = "2"
        Me.Label115.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label114
        '
        Me.Label114.AutoSize = True
        Me.Label114.Location = New System.Drawing.Point(371, 132)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(13, 13)
        Me.Label114.TabIndex = 349
        Me.Label114.Text = "3"
        Me.Label114.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.Location = New System.Drawing.Point(47, 132)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(64, 13)
        Me.Label112.TabIndex = 348
        Me.Label112.Text = "Test Run #:"
        Me.Label112.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.Location = New System.Drawing.Point(551, 32)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(48, 13)
        Me.Label111.TabIndex = 347
        Me.Label111.Text = "BTU/scf"
        '
        'txtHeatContentFlare
        '
        Me.txtHeatContentFlare.Location = New System.Drawing.Point(503, 32)
        Me.txtHeatContentFlare.MaxLength = 11
        Me.txtHeatContentFlare.Name = "txtHeatContentFlare"
        Me.txtHeatContentFlare.Size = New System.Drawing.Size(40, 20)
        Me.txtHeatContentFlare.TabIndex = 225
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.Location = New System.Drawing.Point(279, 32)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(217, 13)
        Me.Label110.TabIndex = 346
        Me.Label110.Text = "ft/sec; Heat Content greater than or equal to"
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Location = New System.Drawing.Point(135, 32)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(92, 13)
        Me.Label109.TabIndex = 345
        Me.Label109.Text = "Velocity less than "
        '
        'Label103
        '
        Me.Label103.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label103.Location = New System.Drawing.Point(107, 29)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(588, 1)
        Me.Label103.TabIndex = 344
        Me.Label103.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMonitoringDataFlare
        '
        Me.txtMonitoringDataFlare.Location = New System.Drawing.Point(135, 86)
        Me.txtMonitoringDataFlare.MaxLength = 4000
        Me.txtMonitoringDataFlare.Multiline = True
        Me.txtMonitoringDataFlare.Name = "txtMonitoringDataFlare"
        Me.txtMonitoringDataFlare.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMonitoringDataFlare.Size = New System.Drawing.Size(616, 40)
        Me.txtMonitoringDataFlare.TabIndex = 227
        '
        'txtApplicableRegulationFlare
        '
        Me.txtApplicableRegulationFlare.AcceptsReturn = True
        Me.txtApplicableRegulationFlare.Location = New System.Drawing.Point(135, 54)
        Me.txtApplicableRegulationFlare.MaxLength = 200
        Me.txtApplicableRegulationFlare.Multiline = True
        Me.txtApplicableRegulationFlare.Name = "txtApplicableRegulationFlare"
        Me.txtApplicableRegulationFlare.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationFlare.Size = New System.Drawing.Size(616, 30)
        Me.txtApplicableRegulationFlare.TabIndex = 226
        '
        'txtOperatingCapacityFlare
        '
        Me.txtOperatingCapacityFlare.Location = New System.Drawing.Point(495, 8)
        Me.txtOperatingCapacityFlare.MaxLength = 11
        Me.txtOperatingCapacityFlare.Name = "txtOperatingCapacityFlare"
        Me.txtOperatingCapacityFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityFlare.TabIndex = 222
        '
        'Label104
        '
        Me.Label104.AutoSize = True
        Me.Label104.Location = New System.Drawing.Point(391, 8)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(100, 13)
        Me.Label104.TabIndex = 343
        Me.Label104.Text = "Operating Capacity:"
        Me.Label104.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtVelocityFlare
        '
        Me.txtVelocityFlare.Location = New System.Drawing.Point(231, 32)
        Me.txtVelocityFlare.MaxLength = 11
        Me.txtVelocityFlare.Name = "txtVelocityFlare"
        Me.txtVelocityFlare.Size = New System.Drawing.Size(40, 20)
        Me.txtVelocityFlare.TabIndex = 224
        '
        'txtMaximumExpectedOperatingCapacityFlare
        '
        Me.txtMaximumExpectedOperatingCapacityFlare.Location = New System.Drawing.Point(135, 8)
        Me.txtMaximumExpectedOperatingCapacityFlare.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityFlare.Name = "txtMaximumExpectedOperatingCapacityFlare"
        Me.txtMaximumExpectedOperatingCapacityFlare.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityFlare.TabIndex = 220
        '
        'Label105
        '
        Me.Label105.AutoSize = True
        Me.Label105.Location = New System.Drawing.Point(3, 54)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(122, 13)
        Me.Label105.TabIndex = 341
        Me.Label105.Text = "Applicable Requirement:"
        Me.Label105.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.Location = New System.Drawing.Point(3, 32)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(107, 13)
        Me.Label106.TabIndex = 340
        Me.Label106.Text = "Allowable Limitations:"
        Me.Label106.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboOperatingCapacityUnitsFlare
        '
        Me.cboOperatingCapacityUnitsFlare.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsFlare.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsFlare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsFlare.Location = New System.Drawing.Point(583, 8)
        Me.cboOperatingCapacityUnitsFlare.Name = "cboOperatingCapacityUnitsFlare"
        Me.cboOperatingCapacityUnitsFlare.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsFlare.TabIndex = 223
        '
        'cboMaximumExpectedOperatingCapacityUnitsFlare
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsFlare.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsFlare.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsFlare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsFlare.Location = New System.Drawing.Point(223, 8)
        Me.cboMaximumExpectedOperatingCapacityUnitsFlare.Name = "cboMaximumExpectedOperatingCapacityUnitsFlare"
        Me.cboMaximumExpectedOperatingCapacityUnitsFlare.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsFlare.TabIndex = 221
        '
        'Label107
        '
        Me.Label107.AutoSize = True
        Me.Label107.Location = New System.Drawing.Point(3, 86)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(85, 13)
        Me.Label107.TabIndex = 342
        Me.Label107.Text = "Monitoring Data:"
        Me.Label107.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label108
        '
        Me.Label108.Location = New System.Drawing.Point(3, 6)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(104, 24)
        Me.Label108.TabIndex = 339
        Me.Label108.Text = "Maximum Expected Operating Capacity:"
        Me.Label108.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPMethodNine
        '
        Me.TPMethodNine.AutoScroll = True
        Me.TPMethodNine.Controls.Add(Me.txtOtherInformationMethod9)
        Me.TPMethodNine.Controls.Add(Me.Label79)
        Me.TPMethodNine.Controls.Add(Me.TCMethodNine)
        Me.TPMethodNine.Location = New System.Drawing.Point(4, 22)
        Me.TPMethodNine.Name = "TPMethodNine"
        Me.TPMethodNine.Size = New System.Drawing.Size(782, 288)
        Me.TPMethodNine.TabIndex = 6
        Me.TPMethodNine.Text = "Method 9"
        Me.TPMethodNine.UseVisualStyleBackColor = True
        '
        'txtOtherInformationMethod9
        '
        Me.txtOtherInformationMethod9.AcceptsReturn = True
        Me.txtOtherInformationMethod9.Location = New System.Drawing.Point(139, 285)
        Me.txtOtherInformationMethod9.MaxLength = 4000
        Me.txtOtherInformationMethod9.Multiline = True
        Me.txtOtherInformationMethod9.Name = "txtOtherInformationMethod9"
        Me.txtOtherInformationMethod9.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationMethod9.Size = New System.Drawing.Size(616, 96)
        Me.txtOtherInformationMethod9.TabIndex = 280
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Location = New System.Drawing.Point(7, 285)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(91, 13)
        Me.Label79.TabIndex = 259
        Me.Label79.Text = "Other Information:"
        '
        'TCMethodNine
        '
        Me.TCMethodNine.Controls.Add(Me.TPMethodNineSingle)
        Me.TCMethodNine.Controls.Add(Me.TPMethodNineMultiple)
        Me.TCMethodNine.Controls.Add(Me.TPMethodNineMultiple2)
        Me.TCMethodNine.Location = New System.Drawing.Point(3, 3)
        Me.TCMethodNine.Name = "TCMethodNine"
        Me.TCMethodNine.SelectedIndex = 0
        Me.TCMethodNine.Size = New System.Drawing.Size(764, 272)
        Me.TCMethodNine.TabIndex = 258
        Me.TCMethodNine.TabStop = False
        '
        'TPMethodNineSingle
        '
        Me.TPMethodNineSingle.Controls.Add(Me.Panel4)
        Me.TPMethodNineSingle.Controls.Add(Me.Label256)
        Me.TPMethodNineSingle.Controls.Add(Me.Label255)
        Me.TPMethodNineSingle.Controls.Add(Me.Label254)
        Me.TPMethodNineSingle.Controls.Add(Me.Label253)
        Me.TPMethodNineSingle.Controls.Add(Me.Label252)
        Me.TPMethodNineSingle.Controls.Add(Me.txtOpacityMethod9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.txtTestDurationMethod9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.Label84)
        Me.TPMethodNineSingle.Controls.Add(Me.Label83)
        Me.TPMethodNineSingle.Controls.Add(Me.Label80)
        Me.TPMethodNineSingle.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.cboOperatingCapacityUnitsMethod9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.Label76)
        Me.TPMethodNineSingle.Controls.Add(Me.Label77)
        Me.TPMethodNineSingle.Controls.Add(Me.txtOperatingCapacityMethod9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.txtAllowableEmissionRate1Method9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.Label66)
        Me.TPMethodNineSingle.Controls.Add(Me.Label78)
        Me.TPMethodNineSingle.Controls.Add(Me.txtApplicableRegulationMethod9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.txtControlEquipmentOperatingDataMethod9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.txtMaximumExpectedOperatingCapacityMethod9Single)
        Me.TPMethodNineSingle.Controls.Add(Me.Label67)
        Me.TPMethodNineSingle.Controls.Add(Me.Label68)
        Me.TPMethodNineSingle.Controls.Add(Me.cboAllowableEmissionRateUnits1Method9Single)
        Me.TPMethodNineSingle.Location = New System.Drawing.Point(4, 22)
        Me.TPMethodNineSingle.Name = "TPMethodNineSingle"
        Me.TPMethodNineSingle.Size = New System.Drawing.Size(756, 246)
        Me.TPMethodNineSingle.TabIndex = 0
        Me.TPMethodNineSingle.Text = "Single"
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.rdbMethod9Average30)
        Me.Panel4.Controls.Add(Me.rdbMethod9HighestAvg)
        Me.Panel4.Location = New System.Drawing.Point(286, 152)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(152, 46)
        Me.Panel4.TabIndex = 263
        '
        'rdbMethod9Average30
        '
        Me.rdbMethod9Average30.AutoSize = True
        Me.rdbMethod9Average30.Location = New System.Drawing.Point(3, 26)
        Me.rdbMethod9Average30.Name = "rdbMethod9Average30"
        Me.rdbMethod9Average30.Size = New System.Drawing.Size(113, 17)
        Me.rdbMethod9Average30.TabIndex = 1
        Me.rdbMethod9Average30.Text = "30-minute average"
        Me.rdbMethod9Average30.UseVisualStyleBackColor = True
        '
        'rdbMethod9HighestAvg
        '
        Me.rdbMethod9HighestAvg.AutoSize = True
        Me.rdbMethod9HighestAvg.Checked = True
        Me.rdbMethod9HighestAvg.Location = New System.Drawing.Point(3, 3)
        Me.rdbMethod9HighestAvg.Name = "rdbMethod9HighestAvg"
        Me.rdbMethod9HighestAvg.Size = New System.Drawing.Size(146, 17)
        Me.rdbMethod9HighestAvg.TabIndex = 0
        Me.rdbMethod9HighestAvg.TabStop = True
        Me.rdbMethod9HighestAvg.Text = "Highest 6-minute average"
        Me.rdbMethod9HighestAvg.UseVisualStyleBackColor = True
        '
        'Label256
        '
        Me.Label256.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label256.Location = New System.Drawing.Point(4, 198)
        Me.Label256.Name = "Label256"
        Me.Label256.Size = New System.Drawing.Size(435, 1)
        Me.Label256.TabIndex = 262
        '
        'Label255
        '
        Me.Label255.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label255.Location = New System.Drawing.Point(4, 151)
        Me.Label255.Name = "Label255"
        Me.Label255.Size = New System.Drawing.Size(277, 1)
        Me.Label255.TabIndex = 261
        '
        'Label254
        '
        Me.Label254.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label254.Location = New System.Drawing.Point(4, 125)
        Me.Label254.Name = "Label254"
        Me.Label254.Size = New System.Drawing.Size(748, 1)
        Me.Label254.TabIndex = 260
        Me.Label254.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label253
        '
        Me.Label253.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label253.Location = New System.Drawing.Point(4, 82)
        Me.Label253.Name = "Label253"
        Me.Label253.Size = New System.Drawing.Size(748, 1)
        Me.Label253.TabIndex = 259
        Me.Label253.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label252
        '
        Me.Label252.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label252.Location = New System.Drawing.Point(4, 51)
        Me.Label252.Name = "Label252"
        Me.Label252.Size = New System.Drawing.Size(748, 1)
        Me.Label252.TabIndex = 258
        Me.Label252.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOpacityMethod9Single
        '
        Me.txtOpacityMethod9Single.Location = New System.Drawing.Point(136, 152)
        Me.txtOpacityMethod9Single.MaxLength = 11
        Me.txtOpacityMethod9Single.Name = "txtOpacityMethod9Single"
        Me.txtOpacityMethod9Single.Size = New System.Drawing.Size(144, 20)
        Me.txtOpacityMethod9Single.TabIndex = 249
        '
        'txtTestDurationMethod9Single
        '
        Me.txtTestDurationMethod9Single.Location = New System.Drawing.Point(136, 130)
        Me.txtTestDurationMethod9Single.MaxLength = 11
        Me.txtTestDurationMethod9Single.Name = "txtTestDurationMethod9Single"
        Me.txtTestDurationMethod9Single.Size = New System.Drawing.Size(96, 20)
        Me.txtTestDurationMethod9Single.TabIndex = 248
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Location = New System.Drawing.Point(237, 130)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(43, 13)
        Me.Label84.TabIndex = 255
        Me.Label84.Text = "minutes"
        Me.Label84.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.Location = New System.Drawing.Point(4, 152)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(46, 13)
        Me.Label83.TabIndex = 254
        Me.Label83.Text = "Opacity:"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Location = New System.Drawing.Point(4, 130)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(74, 13)
        Me.Label80.TabIndex = 251
        Me.Label80.Text = "Test Duration:"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboMaximumExpectedOperatingCapacityUnitsMethod9Single
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single.Location = New System.Drawing.Point(224, 8)
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single.Name = "cboMaximumExpectedOperatingCapacityUnitsMethod9Single"
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Single.TabIndex = 241
        '
        'cboOperatingCapacityUnitsMethod9Single
        '
        Me.cboOperatingCapacityUnitsMethod9Single.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsMethod9Single.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsMethod9Single.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsMethod9Single.Location = New System.Drawing.Point(584, 8)
        Me.cboOperatingCapacityUnitsMethod9Single.Name = "cboOperatingCapacityUnitsMethod9Single"
        Me.cboOperatingCapacityUnitsMethod9Single.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsMethod9Single.TabIndex = 243
        '
        'Label76
        '
        Me.Label76.Location = New System.Drawing.Point(4, 84)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(112, 32)
        Me.Label76.TabIndex = 240
        Me.Label76.Text = "Control Equipment and Monitoring Data:"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label77
        '
        Me.Label77.Location = New System.Drawing.Point(4, 6)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(104, 24)
        Me.Label77.TabIndex = 235
        Me.Label77.Text = "Maximum Expected Operating Capacity:"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtOperatingCapacityMethod9Single
        '
        Me.txtOperatingCapacityMethod9Single.Location = New System.Drawing.Point(496, 8)
        Me.txtOperatingCapacityMethod9Single.MaxLength = 11
        Me.txtOperatingCapacityMethod9Single.Name = "txtOperatingCapacityMethod9Single"
        Me.txtOperatingCapacityMethod9Single.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityMethod9Single.TabIndex = 242
        '
        'txtAllowableEmissionRate1Method9Single
        '
        Me.txtAllowableEmissionRate1Method9Single.Location = New System.Drawing.Point(136, 30)
        Me.txtAllowableEmissionRate1Method9Single.MaxLength = 11
        Me.txtAllowableEmissionRate1Method9Single.Name = "txtAllowableEmissionRate1Method9Single"
        Me.txtAllowableEmissionRate1Method9Single.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate1Method9Single.TabIndex = 244
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(392, 8)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(100, 13)
        Me.Label66.TabIndex = 244
        Me.Label66.Text = "Operating Capacity:"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label78
        '
        Me.Label78.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label78.Location = New System.Drawing.Point(108, 29)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(588, 1)
        Me.Label78.TabIndex = 250
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtApplicableRegulationMethod9Single
        '
        Me.txtApplicableRegulationMethod9Single.AcceptsReturn = True
        Me.txtApplicableRegulationMethod9Single.Location = New System.Drawing.Point(136, 52)
        Me.txtApplicableRegulationMethod9Single.MaxLength = 200
        Me.txtApplicableRegulationMethod9Single.Multiline = True
        Me.txtApplicableRegulationMethod9Single.Name = "txtApplicableRegulationMethod9Single"
        Me.txtApplicableRegulationMethod9Single.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationMethod9Single.Size = New System.Drawing.Size(616, 30)
        Me.txtApplicableRegulationMethod9Single.TabIndex = 246
        '
        'txtControlEquipmentOperatingDataMethod9Single
        '
        Me.txtControlEquipmentOperatingDataMethod9Single.AcceptsReturn = True
        Me.txtControlEquipmentOperatingDataMethod9Single.Location = New System.Drawing.Point(136, 84)
        Me.txtControlEquipmentOperatingDataMethod9Single.MaxLength = 4000
        Me.txtControlEquipmentOperatingDataMethod9Single.Multiline = True
        Me.txtControlEquipmentOperatingDataMethod9Single.Name = "txtControlEquipmentOperatingDataMethod9Single"
        Me.txtControlEquipmentOperatingDataMethod9Single.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlEquipmentOperatingDataMethod9Single.Size = New System.Drawing.Size(616, 40)
        Me.txtControlEquipmentOperatingDataMethod9Single.TabIndex = 247
        '
        'txtMaximumExpectedOperatingCapacityMethod9Single
        '
        Me.txtMaximumExpectedOperatingCapacityMethod9Single.Location = New System.Drawing.Point(136, 8)
        Me.txtMaximumExpectedOperatingCapacityMethod9Single.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityMethod9Single.Name = "txtMaximumExpectedOperatingCapacityMethod9Single"
        Me.txtMaximumExpectedOperatingCapacityMethod9Single.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityMethod9Single.TabIndex = 240
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(4, 52)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(122, 13)
        Me.Label67.TabIndex = 238
        Me.Label67.Text = "Applicable Requirement:"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(4, 32)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(125, 13)
        Me.Label68.TabIndex = 237
        Me.Label68.Text = "Allowable Emission Rate:"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboAllowableEmissionRateUnits1Method9Single
        '
        Me.cboAllowableEmissionRateUnits1Method9Single.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits1Method9Single.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits1Method9Single.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits1Method9Single.Location = New System.Drawing.Point(224, 30)
        Me.cboAllowableEmissionRateUnits1Method9Single.Name = "cboAllowableEmissionRateUnits1Method9Single"
        Me.cboAllowableEmissionRateUnits1Method9Single.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits1Method9Single.TabIndex = 245
        '
        'TPMethodNineMultiple
        '
        Me.TPMethodNineMultiple.Controls.Add(Me.Panel5)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label261)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label260)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label259)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label258)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label257)
        Me.TPMethodNineMultiple.Controls.Add(Me.txt6minuteAvg1EMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txt6minuteAvg1DMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txt6minuteAvg1AMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txt6minuteAvg1BMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txt6minuteAvg1CMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label96)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label95)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label94)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label93)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label92)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label91)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label90)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label89)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtAllowableEmissionRate3Method9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtAllowableEmissionRate4Method9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtAllowableEmissionRate5Method9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtOperatingCapacityMethod9Multi5)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtOperatingCapacityMethod9Multi2)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtOperatingCapacityMethod9Multi3)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtOperatingCapacityMethod9Multi4)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtMaximumExpectedOperatingCapacityMethod9Multi3)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtMaximumExpectedOperatingCapacityMethod9Multi2)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtMaximumExpectedOperatingCapacityMethod9Multi4)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtMaximumExpectedOperatingCapacityMethod9Multi5)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtOperatingCapacityMethod9Multi1)
        Me.TPMethodNineMultiple.Controls.Add(Me.cboOperatingCapacityUnitsMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label81)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label85)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtAllowableEmissionRate1Method9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtAllowableEmissionRate2Method9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label86)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtApplicableRegulationMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtControlEquipmentOperatingDataMethod9Multi)
        Me.TPMethodNineMultiple.Controls.Add(Me.txtMaximumExpectedOperatingCapacityMethod9Multi1)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label87)
        Me.TPMethodNineMultiple.Controls.Add(Me.Label88)
        Me.TPMethodNineMultiple.Controls.Add(Me.cboAllowableEmissionRateUnitsMethod9Multi)
        Me.TPMethodNineMultiple.Location = New System.Drawing.Point(4, 22)
        Me.TPMethodNineMultiple.Name = "TPMethodNineMultiple"
        Me.TPMethodNineMultiple.Size = New System.Drawing.Size(756, 246)
        Me.TPMethodNineMultiple.TabIndex = 1
        Me.TPMethodNineMultiple.Text = "Multiple"
        '
        'Panel5
        '
        Me.Panel5.AutoSize = True
        Me.Panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel5.Controls.Add(Me.rdbMethod9MultiAverage30)
        Me.Panel5.Controls.Add(Me.rdbMethod9MultiHighestAvg)
        Me.Panel5.Location = New System.Drawing.Point(136, 195)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(152, 46)
        Me.Panel5.TabIndex = 291
        '
        'rdbMethod9MultiAverage30
        '
        Me.rdbMethod9MultiAverage30.AutoSize = True
        Me.rdbMethod9MultiAverage30.Location = New System.Drawing.Point(3, 26)
        Me.rdbMethod9MultiAverage30.Name = "rdbMethod9MultiAverage30"
        Me.rdbMethod9MultiAverage30.Size = New System.Drawing.Size(113, 17)
        Me.rdbMethod9MultiAverage30.TabIndex = 1
        Me.rdbMethod9MultiAverage30.Text = "30-minute average"
        Me.rdbMethod9MultiAverage30.UseVisualStyleBackColor = True
        '
        'rdbMethod9MultiHighestAvg
        '
        Me.rdbMethod9MultiHighestAvg.AutoSize = True
        Me.rdbMethod9MultiHighestAvg.Checked = True
        Me.rdbMethod9MultiHighestAvg.Location = New System.Drawing.Point(3, 3)
        Me.rdbMethod9MultiHighestAvg.Name = "rdbMethod9MultiHighestAvg"
        Me.rdbMethod9MultiHighestAvg.Size = New System.Drawing.Size(146, 17)
        Me.rdbMethod9MultiHighestAvg.TabIndex = 0
        Me.rdbMethod9MultiHighestAvg.TabStop = True
        Me.rdbMethod9MultiHighestAvg.Text = "Highest 6-minute average"
        Me.rdbMethod9MultiHighestAvg.UseVisualStyleBackColor = True
        '
        'Label261
        '
        Me.Label261.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label261.Location = New System.Drawing.Point(4, 191)
        Me.Label261.Name = "Label261"
        Me.Label261.Size = New System.Drawing.Size(612, 1)
        Me.Label261.TabIndex = 290
        Me.Label261.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label260
        '
        Me.Label260.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label260.Location = New System.Drawing.Point(4, 148)
        Me.Label260.Name = "Label260"
        Me.Label260.Size = New System.Drawing.Size(734, 1)
        Me.Label260.TabIndex = 289
        Me.Label260.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label259
        '
        Me.Label259.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label259.Location = New System.Drawing.Point(4, 106)
        Me.Label259.Name = "Label259"
        Me.Label259.Size = New System.Drawing.Size(734, 1)
        Me.Label259.TabIndex = 288
        Me.Label259.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label258
        '
        Me.Label258.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label258.Location = New System.Drawing.Point(4, 73)
        Me.Label258.Name = "Label258"
        Me.Label258.Size = New System.Drawing.Size(734, 1)
        Me.Label258.TabIndex = 287
        Me.Label258.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label257
        '
        Me.Label257.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label257.Location = New System.Drawing.Point(4, 51)
        Me.Label257.Name = "Label257"
        Me.Label257.Size = New System.Drawing.Size(734, 1)
        Me.Label257.TabIndex = 286
        Me.Label257.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt6minuteAvg1EMethod9Multi
        '
        Me.txt6minuteAvg1EMethod9Multi.Location = New System.Drawing.Point(528, 168)
        Me.txt6minuteAvg1EMethod9Multi.MaxLength = 11
        Me.txt6minuteAvg1EMethod9Multi.Name = "txt6minuteAvg1EMethod9Multi"
        Me.txt6minuteAvg1EMethod9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txt6minuteAvg1EMethod9Multi.TabIndex = 274
        '
        'txt6minuteAvg1DMethod9Multi
        '
        Me.txt6minuteAvg1DMethod9Multi.Location = New System.Drawing.Point(430, 168)
        Me.txt6minuteAvg1DMethod9Multi.MaxLength = 11
        Me.txt6minuteAvg1DMethod9Multi.Name = "txt6minuteAvg1DMethod9Multi"
        Me.txt6minuteAvg1DMethod9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txt6minuteAvg1DMethod9Multi.TabIndex = 273
        '
        'txt6minuteAvg1AMethod9Multi
        '
        Me.txt6minuteAvg1AMethod9Multi.Location = New System.Drawing.Point(136, 168)
        Me.txt6minuteAvg1AMethod9Multi.MaxLength = 11
        Me.txt6minuteAvg1AMethod9Multi.Name = "txt6minuteAvg1AMethod9Multi"
        Me.txt6minuteAvg1AMethod9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txt6minuteAvg1AMethod9Multi.TabIndex = 270
        '
        'txt6minuteAvg1BMethod9Multi
        '
        Me.txt6minuteAvg1BMethod9Multi.Location = New System.Drawing.Point(234, 168)
        Me.txt6minuteAvg1BMethod9Multi.MaxLength = 11
        Me.txt6minuteAvg1BMethod9Multi.Name = "txt6minuteAvg1BMethod9Multi"
        Me.txt6minuteAvg1BMethod9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txt6minuteAvg1BMethod9Multi.TabIndex = 271
        '
        'txt6minuteAvg1CMethod9Multi
        '
        Me.txt6minuteAvg1CMethod9Multi.Location = New System.Drawing.Point(332, 168)
        Me.txt6minuteAvg1CMethod9Multi.MaxLength = 11
        Me.txt6minuteAvg1CMethod9Multi.Name = "txt6minuteAvg1CMethod9Multi"
        Me.txt6minuteAvg1CMethod9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txt6minuteAvg1CMethod9Multi.TabIndex = 272
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(278, 153)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(13, 13)
        Me.Label96.TabIndex = 280
        Me.Label96.Text = "2"
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(376, 153)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(13, 13)
        Me.Label95.TabIndex = 279
        Me.Label95.Text = "3"
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.Location = New System.Drawing.Point(474, 153)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(13, 13)
        Me.Label94.TabIndex = 278
        Me.Label94.Text = "4"
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.Location = New System.Drawing.Point(572, 153)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(13, 13)
        Me.Label93.TabIndex = 277
        Me.Label93.Text = "5"
        '
        'Label92
        '
        Me.Label92.AutoSize = True
        Me.Label92.Location = New System.Drawing.Point(180, 153)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(13, 13)
        Me.Label92.TabIndex = 276
        Me.Label92.Text = "1"
        '
        'Label91
        '
        Me.Label91.Location = New System.Drawing.Point(12, 200)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(96, 24)
        Me.Label91.TabIndex = 275
        Me.Label91.Text = "Highest 6-minute Average"
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Location = New System.Drawing.Point(48, 153)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(75, 13)
        Me.Label90.TabIndex = 274
        Me.Label90.Text = "Test Point #'s:"
        '
        'Label89
        '
        Me.Label89.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label89.Location = New System.Drawing.Point(108, 29)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(630, 1)
        Me.Label89.TabIndex = 273
        Me.Label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAllowableEmissionRate3Method9Multi
        '
        Me.txtAllowableEmissionRate3Method9Multi.Location = New System.Drawing.Point(332, 52)
        Me.txtAllowableEmissionRate3Method9Multi.MaxLength = 11
        Me.txtAllowableEmissionRate3Method9Multi.Name = "txtAllowableEmissionRate3Method9Multi"
        Me.txtAllowableEmissionRate3Method9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate3Method9Multi.TabIndex = 264
        '
        'txtAllowableEmissionRate4Method9Multi
        '
        Me.txtAllowableEmissionRate4Method9Multi.Location = New System.Drawing.Point(430, 52)
        Me.txtAllowableEmissionRate4Method9Multi.MaxLength = 11
        Me.txtAllowableEmissionRate4Method9Multi.Name = "txtAllowableEmissionRate4Method9Multi"
        Me.txtAllowableEmissionRate4Method9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate4Method9Multi.TabIndex = 265
        '
        'txtAllowableEmissionRate5Method9Multi
        '
        Me.txtAllowableEmissionRate5Method9Multi.Location = New System.Drawing.Point(528, 52)
        Me.txtAllowableEmissionRate5Method9Multi.MaxLength = 11
        Me.txtAllowableEmissionRate5Method9Multi.Name = "txtAllowableEmissionRate5Method9Multi"
        Me.txtAllowableEmissionRate5Method9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate5Method9Multi.TabIndex = 266
        '
        'txtOperatingCapacityMethod9Multi5
        '
        Me.txtOperatingCapacityMethod9Multi5.Location = New System.Drawing.Point(528, 30)
        Me.txtOperatingCapacityMethod9Multi5.MaxLength = 11
        Me.txtOperatingCapacityMethod9Multi5.Name = "txtOperatingCapacityMethod9Multi5"
        Me.txtOperatingCapacityMethod9Multi5.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityMethod9Multi5.TabIndex = 260
        '
        'txtOperatingCapacityMethod9Multi2
        '
        Me.txtOperatingCapacityMethod9Multi2.Location = New System.Drawing.Point(234, 30)
        Me.txtOperatingCapacityMethod9Multi2.MaxLength = 11
        Me.txtOperatingCapacityMethod9Multi2.Name = "txtOperatingCapacityMethod9Multi2"
        Me.txtOperatingCapacityMethod9Multi2.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityMethod9Multi2.TabIndex = 257
        '
        'txtOperatingCapacityMethod9Multi3
        '
        Me.txtOperatingCapacityMethod9Multi3.Location = New System.Drawing.Point(332, 30)
        Me.txtOperatingCapacityMethod9Multi3.MaxLength = 11
        Me.txtOperatingCapacityMethod9Multi3.Name = "txtOperatingCapacityMethod9Multi3"
        Me.txtOperatingCapacityMethod9Multi3.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityMethod9Multi3.TabIndex = 258
        '
        'txtOperatingCapacityMethod9Multi4
        '
        Me.txtOperatingCapacityMethod9Multi4.Location = New System.Drawing.Point(430, 30)
        Me.txtOperatingCapacityMethod9Multi4.MaxLength = 11
        Me.txtOperatingCapacityMethod9Multi4.Name = "txtOperatingCapacityMethod9Multi4"
        Me.txtOperatingCapacityMethod9Multi4.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityMethod9Multi4.TabIndex = 259
        '
        'txtMaximumExpectedOperatingCapacityMethod9Multi3
        '
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi3.Location = New System.Drawing.Point(332, 8)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi3.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi3.Name = "txtMaximumExpectedOperatingCapacityMethod9Multi3"
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi3.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi3.TabIndex = 252
        '
        'txtMaximumExpectedOperatingCapacityMethod9Multi2
        '
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi2.Location = New System.Drawing.Point(234, 8)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi2.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi2.Name = "txtMaximumExpectedOperatingCapacityMethod9Multi2"
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi2.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi2.TabIndex = 251
        '
        'txtMaximumExpectedOperatingCapacityMethod9Multi4
        '
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi4.Location = New System.Drawing.Point(430, 8)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi4.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi4.Name = "txtMaximumExpectedOperatingCapacityMethod9Multi4"
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi4.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi4.TabIndex = 253
        '
        'txtMaximumExpectedOperatingCapacityMethod9Multi5
        '
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi5.Location = New System.Drawing.Point(528, 8)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi5.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi5.Name = "txtMaximumExpectedOperatingCapacityMethod9Multi5"
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi5.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi5.TabIndex = 254
        '
        'txtOperatingCapacityMethod9Multi1
        '
        Me.txtOperatingCapacityMethod9Multi1.Location = New System.Drawing.Point(136, 30)
        Me.txtOperatingCapacityMethod9Multi1.MaxLength = 11
        Me.txtOperatingCapacityMethod9Multi1.Name = "txtOperatingCapacityMethod9Multi1"
        Me.txtOperatingCapacityMethod9Multi1.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityMethod9Multi1.TabIndex = 256
        '
        'cboOperatingCapacityUnitsMethod9Multi
        '
        Me.cboOperatingCapacityUnitsMethod9Multi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsMethod9Multi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsMethod9Multi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsMethod9Multi.Location = New System.Drawing.Point(626, 30)
        Me.cboOperatingCapacityUnitsMethod9Multi.Name = "cboOperatingCapacityUnitsMethod9Multi"
        Me.cboOperatingCapacityUnitsMethod9Multi.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsMethod9Multi.TabIndex = 261
        '
        'cboMaximumExpectedOperatingCapacityUnitsMethod9Multi
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi.Location = New System.Drawing.Point(626, 8)
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi.Name = "cboMaximumExpectedOperatingCapacityUnitsMethod9Multi"
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod9Multi.TabIndex = 255
        '
        'Label81
        '
        Me.Label81.Location = New System.Drawing.Point(4, 112)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(112, 24)
        Me.Label81.TabIndex = 254
        Me.Label81.Text = "Control Equipment and Monitoring Data:"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label85
        '
        Me.Label85.Location = New System.Drawing.Point(4, 6)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(104, 24)
        Me.Label85.TabIndex = 250
        Me.Label85.Text = "Maximum Expected Operating Capacity:"
        Me.Label85.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAllowableEmissionRate1Method9Multi
        '
        Me.txtAllowableEmissionRate1Method9Multi.Location = New System.Drawing.Point(136, 52)
        Me.txtAllowableEmissionRate1Method9Multi.MaxLength = 11
        Me.txtAllowableEmissionRate1Method9Multi.Name = "txtAllowableEmissionRate1Method9Multi"
        Me.txtAllowableEmissionRate1Method9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate1Method9Multi.TabIndex = 262
        '
        'txtAllowableEmissionRate2Method9Multi
        '
        Me.txtAllowableEmissionRate2Method9Multi.Location = New System.Drawing.Point(234, 52)
        Me.txtAllowableEmissionRate2Method9Multi.MaxLength = 11
        Me.txtAllowableEmissionRate2Method9Multi.Name = "txtAllowableEmissionRate2Method9Multi"
        Me.txtAllowableEmissionRate2Method9Multi.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate2Method9Multi.TabIndex = 263
        '
        'Label86
        '
        Me.Label86.AutoSize = True
        Me.Label86.Location = New System.Drawing.Point(4, 32)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(100, 13)
        Me.Label86.TabIndex = 257
        Me.Label86.Text = "Operating Capacity:"
        Me.Label86.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtApplicableRegulationMethod9Multi
        '
        Me.txtApplicableRegulationMethod9Multi.AcceptsReturn = True
        Me.txtApplicableRegulationMethod9Multi.Location = New System.Drawing.Point(136, 75)
        Me.txtApplicableRegulationMethod9Multi.MaxLength = 200
        Me.txtApplicableRegulationMethod9Multi.Multiline = True
        Me.txtApplicableRegulationMethod9Multi.Name = "txtApplicableRegulationMethod9Multi"
        Me.txtApplicableRegulationMethod9Multi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationMethod9Multi.Size = New System.Drawing.Size(602, 30)
        Me.txtApplicableRegulationMethod9Multi.TabIndex = 268
        '
        'txtControlEquipmentOperatingDataMethod9Multi
        '
        Me.txtControlEquipmentOperatingDataMethod9Multi.Location = New System.Drawing.Point(136, 108)
        Me.txtControlEquipmentOperatingDataMethod9Multi.MaxLength = 4000
        Me.txtControlEquipmentOperatingDataMethod9Multi.Multiline = True
        Me.txtControlEquipmentOperatingDataMethod9Multi.Name = "txtControlEquipmentOperatingDataMethod9Multi"
        Me.txtControlEquipmentOperatingDataMethod9Multi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlEquipmentOperatingDataMethod9Multi.Size = New System.Drawing.Size(602, 41)
        Me.txtControlEquipmentOperatingDataMethod9Multi.TabIndex = 269
        '
        'txtMaximumExpectedOperatingCapacityMethod9Multi1
        '
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi1.Location = New System.Drawing.Point(136, 8)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi1.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi1.Name = "txtMaximumExpectedOperatingCapacityMethod9Multi1"
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi1.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityMethod9Multi1.TabIndex = 250
        '
        'Label87
        '
        Me.Label87.AutoSize = True
        Me.Label87.Location = New System.Drawing.Point(4, 74)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(122, 13)
        Me.Label87.TabIndex = 253
        Me.Label87.Text = "Applicable Requirement:"
        Me.Label87.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Location = New System.Drawing.Point(4, 52)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(125, 13)
        Me.Label88.TabIndex = 252
        Me.Label88.Text = "Allowable Emission Rate:"
        Me.Label88.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboAllowableEmissionRateUnitsMethod9Multi
        '
        Me.cboAllowableEmissionRateUnitsMethod9Multi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnitsMethod9Multi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnitsMethod9Multi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnitsMethod9Multi.Location = New System.Drawing.Point(626, 52)
        Me.cboAllowableEmissionRateUnitsMethod9Multi.Name = "cboAllowableEmissionRateUnitsMethod9Multi"
        Me.cboAllowableEmissionRateUnitsMethod9Multi.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnitsMethod9Multi.TabIndex = 267
        '
        'TPMethodNineMultiple2
        '
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label266)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label265)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label264)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label263)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label262)
        Me.TPMethodNineMultiple2.Controls.Add(Me.txtEquipmentItem1EMethod9Multi)
        Me.TPMethodNineMultiple2.Controls.Add(Me.txtEquipmentItem1DMethod9Multi)
        Me.TPMethodNineMultiple2.Controls.Add(Me.txtEquipmentItem1BMethod9Multi)
        Me.TPMethodNineMultiple2.Controls.Add(Me.txtEquipmentItem1CMethod9Multi)
        Me.TPMethodNineMultiple2.Controls.Add(Me.txtEquipmentItem1AMethod9Multi)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label102)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label101)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label100)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label99)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label98)
        Me.TPMethodNineMultiple2.Controls.Add(Me.Label97)
        Me.TPMethodNineMultiple2.Location = New System.Drawing.Point(4, 22)
        Me.TPMethodNineMultiple2.Name = "TPMethodNineMultiple2"
        Me.TPMethodNineMultiple2.Size = New System.Drawing.Size(756, 246)
        Me.TPMethodNineMultiple2.TabIndex = 2
        Me.TPMethodNineMultiple2.Text = "Multiple (cont.)"
        '
        'Label266
        '
        Me.Label266.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label266.Location = New System.Drawing.Point(4, 241)
        Me.Label266.Name = "Label266"
        Me.Label266.Size = New System.Drawing.Size(748, 1)
        Me.Label266.TabIndex = 291
        Me.Label266.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label265
        '
        Me.Label265.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label265.Location = New System.Drawing.Point(4, 198)
        Me.Label265.Name = "Label265"
        Me.Label265.Size = New System.Drawing.Size(748, 1)
        Me.Label265.TabIndex = 290
        Me.Label265.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label264
        '
        Me.Label264.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label264.Location = New System.Drawing.Point(4, 156)
        Me.Label264.Name = "Label264"
        Me.Label264.Size = New System.Drawing.Size(748, 1)
        Me.Label264.TabIndex = 289
        Me.Label264.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label263
        '
        Me.Label263.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label263.Location = New System.Drawing.Point(4, 114)
        Me.Label263.Name = "Label263"
        Me.Label263.Size = New System.Drawing.Size(748, 1)
        Me.Label263.TabIndex = 288
        Me.Label263.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label262
        '
        Me.Label262.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label262.Location = New System.Drawing.Point(4, 72)
        Me.Label262.Name = "Label262"
        Me.Label262.Size = New System.Drawing.Size(748, 1)
        Me.Label262.TabIndex = 287
        Me.Label262.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEquipmentItem1EMethod9Multi
        '
        Me.txtEquipmentItem1EMethod9Multi.AcceptsReturn = True
        Me.txtEquipmentItem1EMethod9Multi.Location = New System.Drawing.Point(136, 200)
        Me.txtEquipmentItem1EMethod9Multi.MaxLength = 4000
        Me.txtEquipmentItem1EMethod9Multi.Multiline = True
        Me.txtEquipmentItem1EMethod9Multi.Name = "txtEquipmentItem1EMethod9Multi"
        Me.txtEquipmentItem1EMethod9Multi.Size = New System.Drawing.Size(616, 40)
        Me.txtEquipmentItem1EMethod9Multi.TabIndex = 279
        '
        'txtEquipmentItem1DMethod9Multi
        '
        Me.txtEquipmentItem1DMethod9Multi.AcceptsReturn = True
        Me.txtEquipmentItem1DMethod9Multi.Location = New System.Drawing.Point(136, 158)
        Me.txtEquipmentItem1DMethod9Multi.MaxLength = 4000
        Me.txtEquipmentItem1DMethod9Multi.Multiline = True
        Me.txtEquipmentItem1DMethod9Multi.Name = "txtEquipmentItem1DMethod9Multi"
        Me.txtEquipmentItem1DMethod9Multi.Size = New System.Drawing.Size(616, 40)
        Me.txtEquipmentItem1DMethod9Multi.TabIndex = 278
        '
        'txtEquipmentItem1BMethod9Multi
        '
        Me.txtEquipmentItem1BMethod9Multi.AcceptsReturn = True
        Me.txtEquipmentItem1BMethod9Multi.Location = New System.Drawing.Point(136, 74)
        Me.txtEquipmentItem1BMethod9Multi.MaxLength = 4000
        Me.txtEquipmentItem1BMethod9Multi.Multiline = True
        Me.txtEquipmentItem1BMethod9Multi.Name = "txtEquipmentItem1BMethod9Multi"
        Me.txtEquipmentItem1BMethod9Multi.Size = New System.Drawing.Size(616, 40)
        Me.txtEquipmentItem1BMethod9Multi.TabIndex = 276
        '
        'txtEquipmentItem1CMethod9Multi
        '
        Me.txtEquipmentItem1CMethod9Multi.AcceptsReturn = True
        Me.txtEquipmentItem1CMethod9Multi.Location = New System.Drawing.Point(136, 116)
        Me.txtEquipmentItem1CMethod9Multi.MaxLength = 4000
        Me.txtEquipmentItem1CMethod9Multi.Multiline = True
        Me.txtEquipmentItem1CMethod9Multi.Name = "txtEquipmentItem1CMethod9Multi"
        Me.txtEquipmentItem1CMethod9Multi.Size = New System.Drawing.Size(616, 40)
        Me.txtEquipmentItem1CMethod9Multi.TabIndex = 277
        '
        'txtEquipmentItem1AMethod9Multi
        '
        Me.txtEquipmentItem1AMethod9Multi.AcceptsReturn = True
        Me.txtEquipmentItem1AMethod9Multi.Location = New System.Drawing.Point(136, 32)
        Me.txtEquipmentItem1AMethod9Multi.MaxLength = 4000
        Me.txtEquipmentItem1AMethod9Multi.Multiline = True
        Me.txtEquipmentItem1AMethod9Multi.Name = "txtEquipmentItem1AMethod9Multi"
        Me.txtEquipmentItem1AMethod9Multi.Size = New System.Drawing.Size(616, 40)
        Me.txtEquipmentItem1AMethod9Multi.TabIndex = 275
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.Location = New System.Drawing.Point(4, 74)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(92, 13)
        Me.Label102.TabIndex = 5
        Me.Label102.Text = "Equipment Item 2:"
        '
        'Label101
        '
        Me.Label101.AutoSize = True
        Me.Label101.Location = New System.Drawing.Point(4, 116)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(92, 13)
        Me.Label101.TabIndex = 4
        Me.Label101.Text = "Equipment Item 3:"
        '
        'Label100
        '
        Me.Label100.AutoSize = True
        Me.Label100.Location = New System.Drawing.Point(4, 158)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(92, 13)
        Me.Label100.TabIndex = 3
        Me.Label100.Text = "Equipment Item 4:"
        '
        'Label99
        '
        Me.Label99.AutoSize = True
        Me.Label99.Location = New System.Drawing.Point(4, 200)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(92, 13)
        Me.Label99.TabIndex = 2
        Me.Label99.Text = "Equipment Item 5:"
        '
        'Label98
        '
        Me.Label98.AutoSize = True
        Me.Label98.Location = New System.Drawing.Point(4, 32)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(92, 13)
        Me.Label98.TabIndex = 1
        Me.Label98.Text = "Equipment Item 1:"
        '
        'Label97
        '
        Me.Label97.AutoSize = True
        Me.Label97.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(4, 8)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(112, 13)
        Me.Label97.TabIndex = 0
        Me.Label97.Text = "Control Equipment List"
        '
        'TPMemorandum
        '
        Me.TPMemorandum.AutoScroll = True
        Me.TPMemorandum.Controls.Add(Me.Label247)
        Me.TPMemorandum.Controls.Add(Me.txtApplicableRegulationMemorandum)
        Me.TPMemorandum.Controls.Add(Me.Label117)
        Me.TPMemorandum.Controls.Add(Me.TCMemorandum)
        Me.TPMemorandum.Location = New System.Drawing.Point(4, 22)
        Me.TPMemorandum.Name = "TPMemorandum"
        Me.TPMemorandum.Size = New System.Drawing.Size(782, 288)
        Me.TPMemorandum.TabIndex = 7
        Me.TPMemorandum.Text = "Memorandum"
        Me.TPMemorandum.UseVisualStyleBackColor = True
        '
        'Label247
        '
        Me.Label247.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label247.Location = New System.Drawing.Point(7, 26)
        Me.Label247.Name = "Label247"
        Me.Label247.Size = New System.Drawing.Size(745, 1)
        Me.Label247.TabIndex = 180
        '
        'txtApplicableRegulationMemorandum
        '
        Me.txtApplicableRegulationMemorandum.Location = New System.Drawing.Point(139, 5)
        Me.txtApplicableRegulationMemorandum.MaxLength = 200
        Me.txtApplicableRegulationMemorandum.Name = "txtApplicableRegulationMemorandum"
        Me.txtApplicableRegulationMemorandum.Size = New System.Drawing.Size(616, 20)
        Me.txtApplicableRegulationMemorandum.TabIndex = 290
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.Location = New System.Drawing.Point(7, 5)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(122, 13)
        Me.Label117.TabIndex = 179
        Me.Label117.Text = "Applicable Requirement:"
        Me.Label117.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TCMemorandum
        '
        Me.TCMemorandum.Controls.Add(Me.TPMemoStandard)
        Me.TCMemorandum.Controls.Add(Me.TPMemoToFile)
        Me.TCMemorandum.Controls.Add(Me.TPMemoPTE)
        Me.TCMemorandum.Location = New System.Drawing.Point(3, 29)
        Me.TCMemorandum.Name = "TCMemorandum"
        Me.TCMemorandum.SelectedIndex = 0
        Me.TCMemorandum.Size = New System.Drawing.Size(764, 448)
        Me.TCMemorandum.TabIndex = 178
        Me.TCMemorandum.TabStop = False
        '
        'TPMemoStandard
        '
        Me.TPMemoStandard.Controls.Add(Me.txtMemorandumStandard)
        Me.TPMemoStandard.Controls.Add(Me.Label122)
        Me.TPMemoStandard.Location = New System.Drawing.Point(4, 22)
        Me.TPMemoStandard.Name = "TPMemoStandard"
        Me.TPMemoStandard.Size = New System.Drawing.Size(756, 422)
        Me.TPMemoStandard.TabIndex = 0
        Me.TPMemoStandard.Text = "Standard"
        '
        'txtMemorandumStandard
        '
        Me.txtMemorandumStandard.AcceptsReturn = True
        Me.txtMemorandumStandard.Location = New System.Drawing.Point(8, 32)
        Me.txtMemorandumStandard.MaxLength = 4000
        Me.txtMemorandumStandard.Multiline = True
        Me.txtMemorandumStandard.Name = "txtMemorandumStandard"
        Me.txtMemorandumStandard.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMemorandumStandard.Size = New System.Drawing.Size(740, 288)
        Me.txtMemorandumStandard.TabIndex = 291
        '
        'Label122
        '
        Me.Label122.AutoSize = True
        Me.Label122.Location = New System.Drawing.Point(4, 8)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(74, 13)
        Me.Label122.TabIndex = 0
        Me.Label122.Text = "Memorandum:"
        Me.Label122.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPMemoToFile
        '
        Me.TPMemoToFile.Controls.Add(Me.Label251)
        Me.TPMemoToFile.Controls.Add(Me.Label191)
        Me.TPMemoToFile.Controls.Add(Me.txtSerialNumberMemorandumToFile)
        Me.TPMemoToFile.Controls.Add(Me.txtModelMemorandumToFile)
        Me.TPMemoToFile.Controls.Add(Me.Label128)
        Me.TPMemoToFile.Controls.Add(Me.Label127)
        Me.TPMemoToFile.Controls.Add(Me.Label126)
        Me.TPMemoToFile.Controls.Add(Me.Label125)
        Me.TPMemoToFile.Controls.Add(Me.txtMemorandumToFile)
        Me.TPMemoToFile.Controls.Add(Me.Label123)
        Me.TPMemoToFile.Location = New System.Drawing.Point(4, 22)
        Me.TPMemoToFile.Name = "TPMemoToFile"
        Me.TPMemoToFile.Size = New System.Drawing.Size(756, 422)
        Me.TPMemoToFile.TabIndex = 1
        Me.TPMemoToFile.Text = "To File"
        '
        'Label251
        '
        Me.Label251.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label251.Location = New System.Drawing.Point(4, 57)
        Me.Label251.Name = "Label251"
        Me.Label251.Size = New System.Drawing.Size(460, 1)
        Me.Label251.TabIndex = 12
        '
        'Label191
        '
        Me.Label191.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label191.Location = New System.Drawing.Point(4, 29)
        Me.Label191.Name = "Label191"
        Me.Label191.Size = New System.Drawing.Size(460, 1)
        Me.Label191.TabIndex = 11
        '
        'txtSerialNumberMemorandumToFile
        '
        Me.txtSerialNumberMemorandumToFile.Location = New System.Drawing.Point(136, 36)
        Me.txtSerialNumberMemorandumToFile.MaxLength = 200
        Me.txtSerialNumberMemorandumToFile.Name = "txtSerialNumberMemorandumToFile"
        Me.txtSerialNumberMemorandumToFile.Size = New System.Drawing.Size(240, 20)
        Me.txtSerialNumberMemorandumToFile.TabIndex = 293
        '
        'txtModelMemorandumToFile
        '
        Me.txtModelMemorandumToFile.Location = New System.Drawing.Point(136, 8)
        Me.txtModelMemorandumToFile.MaxLength = 200
        Me.txtModelMemorandumToFile.Name = "txtModelMemorandumToFile"
        Me.txtModelMemorandumToFile.Size = New System.Drawing.Size(240, 20)
        Me.txtModelMemorandumToFile.TabIndex = 292
        '
        'Label128
        '
        Me.Label128.Location = New System.Drawing.Point(376, 6)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(88, 24)
        Me.Label128.TabIndex = 8
        Me.Label128.Text = "If none required put N/A"
        Me.Label128.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label127
        '
        Me.Label127.Location = New System.Drawing.Point(376, 34)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(88, 24)
        Me.Label127.TabIndex = 7
        Me.Label127.Text = "If none required put N/A"
        Me.Label127.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label126
        '
        Me.Label126.Location = New System.Drawing.Point(4, 6)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(112, 24)
        Me.Label126.TabIndex = 6
        Me.Label126.Text = "Monitor Manufacturer and Model:"
        Me.Label126.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label125
        '
        Me.Label125.Location = New System.Drawing.Point(4, 34)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(80, 24)
        Me.Label125.TabIndex = 5
        Me.Label125.Text = "Monitor Serial Number:"
        Me.Label125.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtMemorandumToFile
        '
        Me.txtMemorandumToFile.AcceptsReturn = True
        Me.txtMemorandumToFile.Location = New System.Drawing.Point(8, 86)
        Me.txtMemorandumToFile.MaxLength = 4000
        Me.txtMemorandumToFile.Multiline = True
        Me.txtMemorandumToFile.Name = "txtMemorandumToFile"
        Me.txtMemorandumToFile.Size = New System.Drawing.Size(740, 240)
        Me.txtMemorandumToFile.TabIndex = 294
        '
        'Label123
        '
        Me.Label123.AutoSize = True
        Me.Label123.Location = New System.Drawing.Point(4, 62)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(74, 13)
        Me.Label123.TabIndex = 2
        Me.Label123.Text = "Memorandum:"
        Me.Label123.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPMemoPTE
        '
        Me.TPMemoPTE.Controls.Add(Me.Label250)
        Me.TPMemoPTE.Controls.Add(Me.Label249)
        Me.TPMemoPTE.Controls.Add(Me.Label248)
        Me.TPMemoPTE.Controls.Add(Me.txtMemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.Label190)
        Me.TPMemoPTE.Controls.Add(Me.txtControlEquipmentOperatingDataMemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.txtOperatingCapacityMemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.Label192)
        Me.TPMemoPTE.Controls.Add(Me.txtAllowableEmissionRate2MemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.txtAllowableEmissionRate3MemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.txtAllowableEmissionRate1MemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.txtMaximumExpectedOperatingCapacityMemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.Label194)
        Me.TPMemoPTE.Controls.Add(Me.cboOperatingCapacityUnitsMemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.cboAllowableEmissionRateUnits2MemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.cboAllowableEmissionRateUnits3MemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.cboAllowableEmissionRateUnits1MemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE)
        Me.TPMemoPTE.Controls.Add(Me.Label195)
        Me.TPMemoPTE.Controls.Add(Me.Label196)
        Me.TPMemoPTE.Location = New System.Drawing.Point(4, 22)
        Me.TPMemoPTE.Name = "TPMemoPTE"
        Me.TPMemoPTE.Size = New System.Drawing.Size(756, 422)
        Me.TPMemoPTE.TabIndex = 2
        Me.TPMemoPTE.Text = "PTE"
        '
        'Label250
        '
        Me.Label250.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label250.Location = New System.Drawing.Point(4, 93)
        Me.Label250.Name = "Label250"
        Me.Label250.Size = New System.Drawing.Size(748, 1)
        Me.Label250.TabIndex = 319
        '
        'Label249
        '
        Me.Label249.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label249.Location = New System.Drawing.Point(4, 51)
        Me.Label249.Name = "Label249"
        Me.Label249.Size = New System.Drawing.Size(748, 1)
        Me.Label249.TabIndex = 318
        '
        'Label248
        '
        Me.Label248.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label248.Location = New System.Drawing.Point(108, 29)
        Me.Label248.Name = "Label248"
        Me.Label248.Size = New System.Drawing.Size(588, 1)
        Me.Label248.TabIndex = 317
        '
        'txtMemorandumPTE
        '
        Me.txtMemorandumPTE.AcceptsReturn = True
        Me.txtMemorandumPTE.Location = New System.Drawing.Point(8, 120)
        Me.txtMemorandumPTE.MaxLength = 4000
        Me.txtMemorandumPTE.Multiline = True
        Me.txtMemorandumPTE.Name = "txtMemorandumPTE"
        Me.txtMemorandumPTE.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMemorandumPTE.Size = New System.Drawing.Size(740, 230)
        Me.txtMemorandumPTE.TabIndex = 311
        '
        'Label190
        '
        Me.Label190.AutoSize = True
        Me.Label190.Location = New System.Drawing.Point(4, 96)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(71, 13)
        Me.Label190.TabIndex = 316
        Me.Label190.Text = "Memorandum"
        Me.Label190.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtControlEquipmentOperatingDataMemorandumPTE
        '
        Me.txtControlEquipmentOperatingDataMemorandumPTE.Location = New System.Drawing.Point(136, 52)
        Me.txtControlEquipmentOperatingDataMemorandumPTE.MaxLength = 200
        Me.txtControlEquipmentOperatingDataMemorandumPTE.Multiline = True
        Me.txtControlEquipmentOperatingDataMemorandumPTE.Name = "txtControlEquipmentOperatingDataMemorandumPTE"
        Me.txtControlEquipmentOperatingDataMemorandumPTE.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlEquipmentOperatingDataMemorandumPTE.Size = New System.Drawing.Size(616, 40)
        Me.txtControlEquipmentOperatingDataMemorandumPTE.TabIndex = 310
        '
        'txtOperatingCapacityMemorandumPTE
        '
        Me.txtOperatingCapacityMemorandumPTE.Location = New System.Drawing.Point(496, 8)
        Me.txtOperatingCapacityMemorandumPTE.MaxLength = 11
        Me.txtOperatingCapacityMemorandumPTE.Name = "txtOperatingCapacityMemorandumPTE"
        Me.txtOperatingCapacityMemorandumPTE.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityMemorandumPTE.TabIndex = 302
        '
        'Label192
        '
        Me.Label192.AutoSize = True
        Me.Label192.Location = New System.Drawing.Point(392, 8)
        Me.Label192.Name = "Label192"
        Me.Label192.Size = New System.Drawing.Size(100, 13)
        Me.Label192.TabIndex = 308
        Me.Label192.Text = "Operating Capacity:"
        Me.Label192.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAllowableEmissionRate2MemorandumPTE
        '
        Me.txtAllowableEmissionRate2MemorandumPTE.Location = New System.Drawing.Point(344, 30)
        Me.txtAllowableEmissionRate2MemorandumPTE.MaxLength = 11
        Me.txtAllowableEmissionRate2MemorandumPTE.Name = "txtAllowableEmissionRate2MemorandumPTE"
        Me.txtAllowableEmissionRate2MemorandumPTE.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate2MemorandumPTE.TabIndex = 306
        '
        'txtAllowableEmissionRate3MemorandumPTE
        '
        Me.txtAllowableEmissionRate3MemorandumPTE.Location = New System.Drawing.Point(552, 30)
        Me.txtAllowableEmissionRate3MemorandumPTE.MaxLength = 11
        Me.txtAllowableEmissionRate3MemorandumPTE.Name = "txtAllowableEmissionRate3MemorandumPTE"
        Me.txtAllowableEmissionRate3MemorandumPTE.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate3MemorandumPTE.TabIndex = 308
        '
        'txtAllowableEmissionRate1MemorandumPTE
        '
        Me.txtAllowableEmissionRate1MemorandumPTE.Location = New System.Drawing.Point(136, 30)
        Me.txtAllowableEmissionRate1MemorandumPTE.MaxLength = 11
        Me.txtAllowableEmissionRate1MemorandumPTE.Name = "txtAllowableEmissionRate1MemorandumPTE"
        Me.txtAllowableEmissionRate1MemorandumPTE.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate1MemorandumPTE.TabIndex = 304
        '
        'txtMaximumExpectedOperatingCapacityMemorandumPTE
        '
        Me.txtMaximumExpectedOperatingCapacityMemorandumPTE.Location = New System.Drawing.Point(136, 8)
        Me.txtMaximumExpectedOperatingCapacityMemorandumPTE.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityMemorandumPTE.Name = "txtMaximumExpectedOperatingCapacityMemorandumPTE"
        Me.txtMaximumExpectedOperatingCapacityMemorandumPTE.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityMemorandumPTE.TabIndex = 300
        '
        'Label194
        '
        Me.Label194.AutoSize = True
        Me.Label194.Location = New System.Drawing.Point(4, 34)
        Me.Label194.Name = "Label194"
        Me.Label194.Size = New System.Drawing.Size(125, 13)
        Me.Label194.TabIndex = 301
        Me.Label194.Text = "Allowable Emission Rate:"
        Me.Label194.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboOperatingCapacityUnitsMemorandumPTE
        '
        Me.cboOperatingCapacityUnitsMemorandumPTE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsMemorandumPTE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsMemorandumPTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsMemorandumPTE.Location = New System.Drawing.Point(584, 8)
        Me.cboOperatingCapacityUnitsMemorandumPTE.Name = "cboOperatingCapacityUnitsMemorandumPTE"
        Me.cboOperatingCapacityUnitsMemorandumPTE.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsMemorandumPTE.TabIndex = 303
        '
        'cboAllowableEmissionRateUnits2MemorandumPTE
        '
        Me.cboAllowableEmissionRateUnits2MemorandumPTE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits2MemorandumPTE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits2MemorandumPTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits2MemorandumPTE.Location = New System.Drawing.Point(432, 30)
        Me.cboAllowableEmissionRateUnits2MemorandumPTE.Name = "cboAllowableEmissionRateUnits2MemorandumPTE"
        Me.cboAllowableEmissionRateUnits2MemorandumPTE.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits2MemorandumPTE.TabIndex = 307
        '
        'cboAllowableEmissionRateUnits3MemorandumPTE
        '
        Me.cboAllowableEmissionRateUnits3MemorandumPTE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits3MemorandumPTE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits3MemorandumPTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits3MemorandumPTE.Location = New System.Drawing.Point(640, 30)
        Me.cboAllowableEmissionRateUnits3MemorandumPTE.Name = "cboAllowableEmissionRateUnits3MemorandumPTE"
        Me.cboAllowableEmissionRateUnits3MemorandumPTE.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits3MemorandumPTE.TabIndex = 309
        '
        'cboAllowableEmissionRateUnits1MemorandumPTE
        '
        Me.cboAllowableEmissionRateUnits1MemorandumPTE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits1MemorandumPTE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits1MemorandumPTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits1MemorandumPTE.Location = New System.Drawing.Point(224, 30)
        Me.cboAllowableEmissionRateUnits1MemorandumPTE.Name = "cboAllowableEmissionRateUnits1MemorandumPTE"
        Me.cboAllowableEmissionRateUnits1MemorandumPTE.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits1MemorandumPTE.TabIndex = 305
        '
        'cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE.Location = New System.Drawing.Point(224, 8)
        Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE.Name = "cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE"
        Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE.TabIndex = 301
        '
        'Label195
        '
        Me.Label195.Location = New System.Drawing.Point(4, 52)
        Me.Label195.Name = "Label195"
        Me.Label195.Size = New System.Drawing.Size(112, 32)
        Me.Label195.TabIndex = 304
        Me.Label195.Text = "Control Equipment and Monitoring Data:"
        Me.Label195.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label196
        '
        Me.Label196.Location = New System.Drawing.Point(4, 6)
        Me.Label196.Name = "Label196"
        Me.Label196.Size = New System.Drawing.Size(104, 24)
        Me.Label196.TabIndex = 299
        Me.Label196.Text = "Maximum Expected Operating Capacity:"
        Me.Label196.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPRata
        '
        Me.TPRata.AutoScroll = True
        Me.TPRata.Controls.Add(Me.txtPart75Statement)
        Me.TPRata.Controls.Add(Me.lblRATAPart75)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata5)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata4)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata3)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata2)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata12)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata11)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata10)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata9)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata8)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata7)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata6)
        Me.TPRata.Controls.Add(Me.chbOmitRunRata1)
        Me.TPRata.Controls.Add(Me.Label319)
        Me.TPRata.Controls.Add(Me.Label318)
        Me.TPRata.Controls.Add(Me.Label317)
        Me.TPRata.Controls.Add(Me.txtCMSRata11)
        Me.TPRata.Controls.Add(Me.txtCMSRata12)
        Me.TPRata.Controls.Add(Me.txtCMSRata10)
        Me.TPRata.Controls.Add(Me.txtCMSRata9)
        Me.TPRata.Controls.Add(Me.txtCMSRata8)
        Me.TPRata.Controls.Add(Me.txtCMSRata7)
        Me.TPRata.Controls.Add(Me.txtCMSRata6)
        Me.TPRata.Controls.Add(Me.txtCMSRata5)
        Me.TPRata.Controls.Add(Me.txtCMSRata4)
        Me.TPRata.Controls.Add(Me.txtCMSRata3)
        Me.TPRata.Controls.Add(Me.txtCMSRata2)
        Me.TPRata.Controls.Add(Me.Label316)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata8)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata9)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata12)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata11)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata10)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata7)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata6)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata5)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata4)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata3)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata2)
        Me.TPRata.Controls.Add(Me.Label314)
        Me.TPRata.Controls.Add(Me.Label313)
        Me.TPRata.Controls.Add(Me.Label312)
        Me.TPRata.Controls.Add(Me.Label311)
        Me.TPRata.Controls.Add(Me.Label310)
        Me.TPRata.Controls.Add(Me.Label309)
        Me.TPRata.Controls.Add(Me.Label308)
        Me.TPRata.Controls.Add(Me.Label307)
        Me.TPRata.Controls.Add(Me.Label306)
        Me.TPRata.Controls.Add(Me.Label305)
        Me.TPRata.Controls.Add(Me.Label304)
        Me.TPRata.Controls.Add(Me.Label303)
        Me.TPRata.Controls.Add(Me.Label302)
        Me.TPRata.Controls.Add(Me.Label301)
        Me.TPRata.Controls.Add(Me.Label300)
        Me.TPRata.Controls.Add(Me.Label299)
        Me.TPRata.Controls.Add(Me.txtCMSRata1)
        Me.TPRata.Controls.Add(Me.txtRefMethodRata1)
        Me.TPRata.Controls.Add(Me.Label114b)
        Me.TPRata.Controls.Add(Me.cboUnitsRata)
        Me.TPRata.Controls.Add(Me.lable3b)
        Me.TPRata.Controls.Add(Me.Label112b)
        Me.TPRata.Controls.Add(Me.Label111b)
        Me.TPRata.Controls.Add(Me.cboDiluentRata)
        Me.TPRata.Controls.Add(Me.lblDiluentRata)
        Me.TPRata.Controls.Add(Me.txtApplicableStandardPercentRata)
        Me.TPRata.Controls.Add(Me.txtRefMethodPercentRata)
        Me.TPRata.Controls.Add(Me.txtRelativeAccuracy)
        Me.TPRata.Controls.Add(Me.Label187)
        Me.TPRata.Controls.Add(Me.lblStandardRata)
        Me.TPRata.Controls.Add(Me.lblRefMethodRata)
        Me.TPRata.Controls.Add(Me.txtOtherInformationRata)
        Me.TPRata.Controls.Add(Me.Label185)
        Me.TPRata.Controls.Add(Me.Label183)
        Me.TPRata.Controls.Add(Me.cboDilutentMonitoredRata)
        Me.TPRata.Controls.Add(Me.Label177)
        Me.TPRata.Controls.Add(Me.txtApplicableRegulationRata)
        Me.TPRata.Controls.Add(Me.Label175)
        Me.TPRata.Controls.Add(Me.txtApplicableStandardRata)
        Me.TPRata.Controls.Add(Me.Label173)
        Me.TPRata.Location = New System.Drawing.Point(4, 22)
        Me.TPRata.Name = "TPRata"
        Me.TPRata.Size = New System.Drawing.Size(782, 288)
        Me.TPRata.TabIndex = 8
        Me.TPRata.Text = "RATA"
        Me.TPRata.UseVisualStyleBackColor = True
        '
        'txtPart75Statement
        '
        Me.txtPart75Statement.Location = New System.Drawing.Point(484, 222)
        Me.txtPart75Statement.Name = "txtPart75Statement"
        Me.txtPart75Statement.Size = New System.Drawing.Size(40, 20)
        Me.txtPart75Statement.TabIndex = 364
        Me.txtPart75Statement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblRATAPart75
        '
        Me.lblRATAPart75.AutoSize = True
        Me.lblRATAPart75.Location = New System.Drawing.Point(484, 230)
        Me.lblRATAPart75.Name = "lblRATAPart75"
        Me.lblRATAPart75.Size = New System.Drawing.Size(150, 13)
        Me.lblRATAPart75.TabIndex = 575
        Me.lblRATAPart75.Text = "xxxxxxx % for Part 75 Sources."
        '
        'chbOmitRunRata5
        '
        Me.chbOmitRunRata5.Location = New System.Drawing.Point(287, 198)
        Me.chbOmitRunRata5.Name = "chbOmitRunRata5"
        Me.chbOmitRunRata5.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata5.TabIndex = 337
        Me.chbOmitRunRata5.TabStop = False
        '
        'chbOmitRunRata4
        '
        Me.chbOmitRunRata4.Location = New System.Drawing.Point(287, 176)
        Me.chbOmitRunRata4.Name = "chbOmitRunRata4"
        Me.chbOmitRunRata4.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata4.TabIndex = 334
        Me.chbOmitRunRata4.TabStop = False
        '
        'chbOmitRunRata3
        '
        Me.chbOmitRunRata3.Location = New System.Drawing.Point(287, 154)
        Me.chbOmitRunRata3.Name = "chbOmitRunRata3"
        Me.chbOmitRunRata3.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata3.TabIndex = 331
        Me.chbOmitRunRata3.TabStop = False
        '
        'chbOmitRunRata2
        '
        Me.chbOmitRunRata2.Location = New System.Drawing.Point(287, 132)
        Me.chbOmitRunRata2.Name = "chbOmitRunRata2"
        Me.chbOmitRunRata2.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata2.TabIndex = 328
        Me.chbOmitRunRata2.TabStop = False
        '
        'chbOmitRunRata12
        '
        Me.chbOmitRunRata12.Location = New System.Drawing.Point(287, 352)
        Me.chbOmitRunRata12.Name = "chbOmitRunRata12"
        Me.chbOmitRunRata12.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata12.TabIndex = 359
        Me.chbOmitRunRata12.TabStop = False
        '
        'chbOmitRunRata11
        '
        Me.chbOmitRunRata11.Location = New System.Drawing.Point(287, 330)
        Me.chbOmitRunRata11.Name = "chbOmitRunRata11"
        Me.chbOmitRunRata11.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata11.TabIndex = 356
        Me.chbOmitRunRata11.TabStop = False
        '
        'chbOmitRunRata10
        '
        Me.chbOmitRunRata10.Location = New System.Drawing.Point(287, 308)
        Me.chbOmitRunRata10.Name = "chbOmitRunRata10"
        Me.chbOmitRunRata10.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata10.TabIndex = 353
        Me.chbOmitRunRata10.TabStop = False
        '
        'chbOmitRunRata9
        '
        Me.chbOmitRunRata9.Location = New System.Drawing.Point(287, 286)
        Me.chbOmitRunRata9.Name = "chbOmitRunRata9"
        Me.chbOmitRunRata9.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata9.TabIndex = 350
        Me.chbOmitRunRata9.TabStop = False
        '
        'chbOmitRunRata8
        '
        Me.chbOmitRunRata8.Location = New System.Drawing.Point(287, 264)
        Me.chbOmitRunRata8.Name = "chbOmitRunRata8"
        Me.chbOmitRunRata8.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata8.TabIndex = 347
        Me.chbOmitRunRata8.TabStop = False
        '
        'chbOmitRunRata7
        '
        Me.chbOmitRunRata7.Location = New System.Drawing.Point(287, 242)
        Me.chbOmitRunRata7.Name = "chbOmitRunRata7"
        Me.chbOmitRunRata7.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata7.TabIndex = 343
        Me.chbOmitRunRata7.TabStop = False
        '
        'chbOmitRunRata6
        '
        Me.chbOmitRunRata6.Location = New System.Drawing.Point(287, 220)
        Me.chbOmitRunRata6.Name = "chbOmitRunRata6"
        Me.chbOmitRunRata6.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata6.TabIndex = 340
        Me.chbOmitRunRata6.TabStop = False
        '
        'chbOmitRunRata1
        '
        Me.chbOmitRunRata1.Location = New System.Drawing.Point(287, 110)
        Me.chbOmitRunRata1.Name = "chbOmitRunRata1"
        Me.chbOmitRunRata1.Size = New System.Drawing.Size(16, 16)
        Me.chbOmitRunRata1.TabIndex = 325
        Me.chbOmitRunRata1.TabStop = False
        '
        'Label319
        '
        Me.Label319.AutoSize = True
        Me.Label319.BackColor = System.Drawing.SystemColors.Control
        Me.Label319.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label319.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label319.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label319.Location = New System.Drawing.Point(279, 86)
        Me.Label319.Name = "Label319"
        Me.Label319.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label319.Size = New System.Drawing.Size(31, 14)
        Me.Label319.TabIndex = 573
        Me.Label319.Text = "RUN:"
        Me.Label319.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label318
        '
        Me.Label318.AutoSize = True
        Me.Label318.BackColor = System.Drawing.SystemColors.Control
        Me.Label318.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label318.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label318.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label318.Location = New System.Drawing.Point(279, 70)
        Me.Label318.Name = "Label318"
        Me.Label318.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label318.Size = New System.Drawing.Size(33, 14)
        Me.Label318.TabIndex = 572
        Me.Label318.Text = "OMIT:"
        Me.Label318.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label317
        '
        Me.Label317.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label317.Location = New System.Drawing.Point(319, 70)
        Me.Label317.Name = "Label317"
        Me.Label317.Size = New System.Drawing.Size(1, 305)
        Me.Label317.TabIndex = 571
        '
        'txtCMSRata11
        '
        Me.txtCMSRata11.AcceptsReturn = True
        Me.txtCMSRata11.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata11.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata11.Location = New System.Drawing.Point(183, 329)
        Me.txtCMSRata11.MaxLength = 11
        Me.txtCMSRata11.Name = "txtCMSRata11"
        Me.txtCMSRata11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata11.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata11.TabIndex = 355
        Me.txtCMSRata11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata12
        '
        Me.txtCMSRata12.AcceptsReturn = True
        Me.txtCMSRata12.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata12.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata12.Location = New System.Drawing.Point(183, 351)
        Me.txtCMSRata12.MaxLength = 11
        Me.txtCMSRata12.Name = "txtCMSRata12"
        Me.txtCMSRata12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata12.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata12.TabIndex = 358
        Me.txtCMSRata12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata10
        '
        Me.txtCMSRata10.AcceptsReturn = True
        Me.txtCMSRata10.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata10.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata10.Location = New System.Drawing.Point(183, 307)
        Me.txtCMSRata10.MaxLength = 11
        Me.txtCMSRata10.Name = "txtCMSRata10"
        Me.txtCMSRata10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata10.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata10.TabIndex = 352
        Me.txtCMSRata10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata9
        '
        Me.txtCMSRata9.AcceptsReturn = True
        Me.txtCMSRata9.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata9.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata9.Location = New System.Drawing.Point(183, 285)
        Me.txtCMSRata9.MaxLength = 11
        Me.txtCMSRata9.Name = "txtCMSRata9"
        Me.txtCMSRata9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata9.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata9.TabIndex = 349
        Me.txtCMSRata9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata8
        '
        Me.txtCMSRata8.AcceptsReturn = True
        Me.txtCMSRata8.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata8.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata8.Location = New System.Drawing.Point(183, 263)
        Me.txtCMSRata8.MaxLength = 11
        Me.txtCMSRata8.Name = "txtCMSRata8"
        Me.txtCMSRata8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata8.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata8.TabIndex = 346
        Me.txtCMSRata8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata7
        '
        Me.txtCMSRata7.AcceptsReturn = True
        Me.txtCMSRata7.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata7.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata7.Location = New System.Drawing.Point(183, 241)
        Me.txtCMSRata7.MaxLength = 11
        Me.txtCMSRata7.Name = "txtCMSRata7"
        Me.txtCMSRata7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata7.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata7.TabIndex = 342
        Me.txtCMSRata7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata6
        '
        Me.txtCMSRata6.AcceptsReturn = True
        Me.txtCMSRata6.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata6.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata6.Location = New System.Drawing.Point(183, 219)
        Me.txtCMSRata6.MaxLength = 11
        Me.txtCMSRata6.Name = "txtCMSRata6"
        Me.txtCMSRata6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata6.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata6.TabIndex = 339
        Me.txtCMSRata6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata5
        '
        Me.txtCMSRata5.AcceptsReturn = True
        Me.txtCMSRata5.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata5.Location = New System.Drawing.Point(183, 197)
        Me.txtCMSRata5.MaxLength = 11
        Me.txtCMSRata5.Name = "txtCMSRata5"
        Me.txtCMSRata5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata5.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata5.TabIndex = 336
        Me.txtCMSRata5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata4
        '
        Me.txtCMSRata4.AcceptsReturn = True
        Me.txtCMSRata4.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata4.Location = New System.Drawing.Point(183, 175)
        Me.txtCMSRata4.MaxLength = 11
        Me.txtCMSRata4.Name = "txtCMSRata4"
        Me.txtCMSRata4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata4.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata4.TabIndex = 333
        Me.txtCMSRata4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata3
        '
        Me.txtCMSRata3.AcceptsReturn = True
        Me.txtCMSRata3.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata3.Location = New System.Drawing.Point(183, 153)
        Me.txtCMSRata3.MaxLength = 11
        Me.txtCMSRata3.Name = "txtCMSRata3"
        Me.txtCMSRata3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata3.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata3.TabIndex = 330
        Me.txtCMSRata3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMSRata2
        '
        Me.txtCMSRata2.AcceptsReturn = True
        Me.txtCMSRata2.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata2.Location = New System.Drawing.Point(183, 131)
        Me.txtCMSRata2.MaxLength = 11
        Me.txtCMSRata2.Name = "txtCMSRata2"
        Me.txtCMSRata2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata2.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata2.TabIndex = 327
        Me.txtCMSRata2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label316
        '
        Me.Label316.AutoSize = True
        Me.Label316.BackColor = System.Drawing.SystemColors.Control
        Me.Label316.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label316.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label316.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label316.Location = New System.Drawing.Point(90, 86)
        Me.Label316.Name = "Label316"
        Me.Label316.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label316.Size = New System.Drawing.Size(55, 14)
        Me.Label316.TabIndex = 570
        Me.Label316.Text = "METHOD: "
        Me.Label316.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRefMethodRata8
        '
        Me.txtRefMethodRata8.AcceptsReturn = True
        Me.txtRefMethodRata8.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata8.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata8.Location = New System.Drawing.Point(87, 263)
        Me.txtRefMethodRata8.MaxLength = 11
        Me.txtRefMethodRata8.Name = "txtRefMethodRata8"
        Me.txtRefMethodRata8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata8.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata8.TabIndex = 345
        Me.txtRefMethodRata8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata9
        '
        Me.txtRefMethodRata9.AcceptsReturn = True
        Me.txtRefMethodRata9.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata9.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata9.Location = New System.Drawing.Point(87, 285)
        Me.txtRefMethodRata9.MaxLength = 11
        Me.txtRefMethodRata9.Name = "txtRefMethodRata9"
        Me.txtRefMethodRata9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata9.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata9.TabIndex = 348
        Me.txtRefMethodRata9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata12
        '
        Me.txtRefMethodRata12.AcceptsReturn = True
        Me.txtRefMethodRata12.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata12.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata12.Location = New System.Drawing.Point(87, 351)
        Me.txtRefMethodRata12.MaxLength = 11
        Me.txtRefMethodRata12.Name = "txtRefMethodRata12"
        Me.txtRefMethodRata12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata12.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata12.TabIndex = 357
        Me.txtRefMethodRata12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata11
        '
        Me.txtRefMethodRata11.AcceptsReturn = True
        Me.txtRefMethodRata11.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata11.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata11.Location = New System.Drawing.Point(87, 329)
        Me.txtRefMethodRata11.MaxLength = 11
        Me.txtRefMethodRata11.Name = "txtRefMethodRata11"
        Me.txtRefMethodRata11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata11.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata11.TabIndex = 354
        Me.txtRefMethodRata11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata10
        '
        Me.txtRefMethodRata10.AcceptsReturn = True
        Me.txtRefMethodRata10.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata10.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata10.Location = New System.Drawing.Point(87, 307)
        Me.txtRefMethodRata10.MaxLength = 11
        Me.txtRefMethodRata10.Name = "txtRefMethodRata10"
        Me.txtRefMethodRata10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata10.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata10.TabIndex = 351
        Me.txtRefMethodRata10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata7
        '
        Me.txtRefMethodRata7.AcceptsReturn = True
        Me.txtRefMethodRata7.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata7.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata7.Location = New System.Drawing.Point(87, 241)
        Me.txtRefMethodRata7.MaxLength = 11
        Me.txtRefMethodRata7.Name = "txtRefMethodRata7"
        Me.txtRefMethodRata7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata7.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata7.TabIndex = 341
        Me.txtRefMethodRata7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata6
        '
        Me.txtRefMethodRata6.AcceptsReturn = True
        Me.txtRefMethodRata6.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata6.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata6.Location = New System.Drawing.Point(87, 219)
        Me.txtRefMethodRata6.MaxLength = 11
        Me.txtRefMethodRata6.Name = "txtRefMethodRata6"
        Me.txtRefMethodRata6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata6.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata6.TabIndex = 338
        Me.txtRefMethodRata6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata5
        '
        Me.txtRefMethodRata5.AcceptsReturn = True
        Me.txtRefMethodRata5.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata5.Location = New System.Drawing.Point(87, 197)
        Me.txtRefMethodRata5.MaxLength = 11
        Me.txtRefMethodRata5.Name = "txtRefMethodRata5"
        Me.txtRefMethodRata5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata5.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata5.TabIndex = 335
        Me.txtRefMethodRata5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata4
        '
        Me.txtRefMethodRata4.AcceptsReturn = True
        Me.txtRefMethodRata4.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata4.Location = New System.Drawing.Point(87, 175)
        Me.txtRefMethodRata4.MaxLength = 11
        Me.txtRefMethodRata4.Name = "txtRefMethodRata4"
        Me.txtRefMethodRata4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata4.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata4.TabIndex = 332
        Me.txtRefMethodRata4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata3
        '
        Me.txtRefMethodRata3.AcceptsReturn = True
        Me.txtRefMethodRata3.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata3.Location = New System.Drawing.Point(87, 153)
        Me.txtRefMethodRata3.MaxLength = 11
        Me.txtRefMethodRata3.Name = "txtRefMethodRata3"
        Me.txtRefMethodRata3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata3.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata3.TabIndex = 329
        Me.txtRefMethodRata3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata2
        '
        Me.txtRefMethodRata2.AcceptsReturn = True
        Me.txtRefMethodRata2.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata2.Location = New System.Drawing.Point(87, 131)
        Me.txtRefMethodRata2.MaxLength = 11
        Me.txtRefMethodRata2.Name = "txtRefMethodRata2"
        Me.txtRefMethodRata2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata2.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata2.TabIndex = 326
        Me.txtRefMethodRata2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label314
        '
        Me.Label314.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label314.Location = New System.Drawing.Point(23, 102)
        Me.Label314.Name = "Label314"
        Me.Label314.Size = New System.Drawing.Size(450, 1)
        Me.Label314.TabIndex = 569
        '
        'Label313
        '
        Me.Label313.AutoSize = True
        Me.Label313.BackColor = System.Drawing.SystemColors.Control
        Me.Label313.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label313.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label313.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label313.Location = New System.Drawing.Point(33, 352)
        Me.Label313.Name = "Label313"
        Me.Label313.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label313.Size = New System.Drawing.Size(19, 14)
        Me.Label313.TabIndex = 568
        Me.Label313.Text = "12"
        Me.Label313.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label312
        '
        Me.Label312.AutoSize = True
        Me.Label312.BackColor = System.Drawing.SystemColors.Control
        Me.Label312.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label312.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label312.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label312.Location = New System.Drawing.Point(33, 330)
        Me.Label312.Name = "Label312"
        Me.Label312.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label312.Size = New System.Drawing.Size(18, 14)
        Me.Label312.TabIndex = 567
        Me.Label312.Text = "11"
        Me.Label312.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label311
        '
        Me.Label311.AutoSize = True
        Me.Label311.BackColor = System.Drawing.SystemColors.Control
        Me.Label311.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label311.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label311.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label311.Location = New System.Drawing.Point(33, 308)
        Me.Label311.Name = "Label311"
        Me.Label311.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label311.Size = New System.Drawing.Size(19, 14)
        Me.Label311.TabIndex = 566
        Me.Label311.Text = "10"
        Me.Label311.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label310
        '
        Me.Label310.AutoSize = True
        Me.Label310.BackColor = System.Drawing.SystemColors.Control
        Me.Label310.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label310.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label310.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label310.Location = New System.Drawing.Point(39, 286)
        Me.Label310.Name = "Label310"
        Me.Label310.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label310.Size = New System.Drawing.Size(13, 14)
        Me.Label310.TabIndex = 565
        Me.Label310.Text = "9"
        Me.Label310.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label309
        '
        Me.Label309.AutoSize = True
        Me.Label309.BackColor = System.Drawing.SystemColors.Control
        Me.Label309.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label309.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label309.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label309.Location = New System.Drawing.Point(39, 264)
        Me.Label309.Name = "Label309"
        Me.Label309.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label309.Size = New System.Drawing.Size(13, 14)
        Me.Label309.TabIndex = 564
        Me.Label309.Text = "8"
        Me.Label309.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label308
        '
        Me.Label308.AutoSize = True
        Me.Label308.BackColor = System.Drawing.SystemColors.Control
        Me.Label308.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label308.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label308.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label308.Location = New System.Drawing.Point(39, 242)
        Me.Label308.Name = "Label308"
        Me.Label308.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label308.Size = New System.Drawing.Size(13, 14)
        Me.Label308.TabIndex = 563
        Me.Label308.Text = "7"
        Me.Label308.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label307
        '
        Me.Label307.AutoSize = True
        Me.Label307.BackColor = System.Drawing.SystemColors.Control
        Me.Label307.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label307.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label307.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label307.Location = New System.Drawing.Point(39, 220)
        Me.Label307.Name = "Label307"
        Me.Label307.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label307.Size = New System.Drawing.Size(13, 14)
        Me.Label307.TabIndex = 562
        Me.Label307.Text = "6"
        Me.Label307.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label306
        '
        Me.Label306.AutoSize = True
        Me.Label306.BackColor = System.Drawing.SystemColors.Control
        Me.Label306.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label306.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label306.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label306.Location = New System.Drawing.Point(39, 198)
        Me.Label306.Name = "Label306"
        Me.Label306.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label306.Size = New System.Drawing.Size(13, 14)
        Me.Label306.TabIndex = 561
        Me.Label306.Text = "5"
        Me.Label306.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label305
        '
        Me.Label305.AutoSize = True
        Me.Label305.BackColor = System.Drawing.SystemColors.Control
        Me.Label305.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label305.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label305.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label305.Location = New System.Drawing.Point(39, 176)
        Me.Label305.Name = "Label305"
        Me.Label305.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label305.Size = New System.Drawing.Size(13, 14)
        Me.Label305.TabIndex = 560
        Me.Label305.Text = "4"
        Me.Label305.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label304
        '
        Me.Label304.AutoSize = True
        Me.Label304.BackColor = System.Drawing.SystemColors.Control
        Me.Label304.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label304.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label304.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label304.Location = New System.Drawing.Point(39, 132)
        Me.Label304.Name = "Label304"
        Me.Label304.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label304.Size = New System.Drawing.Size(13, 14)
        Me.Label304.TabIndex = 559
        Me.Label304.Text = "2"
        Me.Label304.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label303
        '
        Me.Label303.AutoSize = True
        Me.Label303.BackColor = System.Drawing.SystemColors.Control
        Me.Label303.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label303.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label303.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label303.Location = New System.Drawing.Point(39, 154)
        Me.Label303.Name = "Label303"
        Me.Label303.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label303.Size = New System.Drawing.Size(13, 14)
        Me.Label303.TabIndex = 558
        Me.Label303.Text = "3"
        Me.Label303.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label302
        '
        Me.Label302.AutoSize = True
        Me.Label302.BackColor = System.Drawing.SystemColors.Control
        Me.Label302.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label302.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label302.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label302.Location = New System.Drawing.Point(39, 110)
        Me.Label302.Name = "Label302"
        Me.Label302.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label302.Size = New System.Drawing.Size(13, 14)
        Me.Label302.TabIndex = 557
        Me.Label302.Text = "1"
        Me.Label302.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label301
        '
        Me.Label301.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label301.Location = New System.Drawing.Point(263, 70)
        Me.Label301.Name = "Label301"
        Me.Label301.Size = New System.Drawing.Size(1, 305)
        Me.Label301.TabIndex = 556
        '
        'Label300
        '
        Me.Label300.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label300.Location = New System.Drawing.Point(167, 70)
        Me.Label300.Name = "Label300"
        Me.Label300.Size = New System.Drawing.Size(1, 305)
        Me.Label300.TabIndex = 555
        '
        'Label299
        '
        Me.Label299.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label299.Location = New System.Drawing.Point(71, 70)
        Me.Label299.Name = "Label299"
        Me.Label299.Size = New System.Drawing.Size(1, 305)
        Me.Label299.TabIndex = 554
        '
        'txtCMSRata1
        '
        Me.txtCMSRata1.AcceptsReturn = True
        Me.txtCMSRata1.BackColor = System.Drawing.SystemColors.Window
        Me.txtCMSRata1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCMSRata1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMSRata1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCMSRata1.Location = New System.Drawing.Point(183, 109)
        Me.txtCMSRata1.MaxLength = 11
        Me.txtCMSRata1.Name = "txtCMSRata1"
        Me.txtCMSRata1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCMSRata1.Size = New System.Drawing.Size(65, 20)
        Me.txtCMSRata1.TabIndex = 324
        Me.txtCMSRata1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodRata1
        '
        Me.txtRefMethodRata1.AcceptsReturn = True
        Me.txtRefMethodRata1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefMethodRata1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefMethodRata1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefMethodRata1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefMethodRata1.Location = New System.Drawing.Point(87, 109)
        Me.txtRefMethodRata1.MaxLength = 11
        Me.txtRefMethodRata1.Name = "txtRefMethodRata1"
        Me.txtRefMethodRata1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefMethodRata1.Size = New System.Drawing.Size(65, 20)
        Me.txtRefMethodRata1.TabIndex = 323
        Me.txtRefMethodRata1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label114b
        '
        Me.Label114b.AutoSize = True
        Me.Label114b.BackColor = System.Drawing.SystemColors.Control
        Me.Label114b.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label114b.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114b.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label114b.Location = New System.Drawing.Point(79, 70)
        Me.Label114b.Name = "Label114b"
        Me.Label114b.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label114b.Size = New System.Drawing.Size(74, 14)
        Me.Label114b.TabIndex = 553
        Me.Label114b.Text = "REFERENCE   "
        Me.Label114b.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboUnitsRata
        '
        Me.cboUnitsRata.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboUnitsRata.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUnitsRata.BackColor = System.Drawing.SystemColors.Window
        Me.cboUnitsRata.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboUnitsRata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnitsRata.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUnitsRata.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboUnitsRata.Location = New System.Drawing.Point(327, 107)
        Me.cboUnitsRata.Name = "cboUnitsRata"
        Me.cboUnitsRata.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboUnitsRata.Size = New System.Drawing.Size(144, 22)
        Me.cboUnitsRata.TabIndex = 360
        '
        'lable3b
        '
        Me.lable3b.AutoSize = True
        Me.lable3b.BackColor = System.Drawing.SystemColors.Control
        Me.lable3b.Cursor = System.Windows.Forms.Cursors.Default
        Me.lable3b.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lable3b.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lable3b.Location = New System.Drawing.Point(199, 78)
        Me.lable3b.Name = "lable3b"
        Me.lable3b.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lable3b.Size = New System.Drawing.Size(32, 14)
        Me.lable3b.TabIndex = 552
        Me.lable3b.Text = "CMS:"
        Me.lable3b.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label112b
        '
        Me.Label112b.AutoSize = True
        Me.Label112b.BackColor = System.Drawing.SystemColors.Control
        Me.Label112b.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label112b.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112b.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label112b.Location = New System.Drawing.Point(351, 78)
        Me.Label112b.Name = "Label112b"
        Me.Label112b.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label112b.Size = New System.Drawing.Size(39, 14)
        Me.Label112b.TabIndex = 551
        Me.Label112b.Text = "UNITS:"
        Me.Label112b.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label111b
        '
        Me.Label111b.AutoSize = True
        Me.Label111b.BackColor = System.Drawing.SystemColors.Control
        Me.Label111b.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label111b.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111b.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label111b.Location = New System.Drawing.Point(29, 86)
        Me.Label111b.Name = "Label111b"
        Me.Label111b.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label111b.Size = New System.Drawing.Size(31, 14)
        Me.Label111b.TabIndex = 550
        Me.Label111b.Text = "RUN:"
        Me.Label111b.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboDiluentRata
        '
        Me.cboDiluentRata.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDiluentRata.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDiluentRata.Location = New System.Drawing.Point(508, 260)
        Me.cboDiluentRata.Name = "cboDiluentRata"
        Me.cboDiluentRata.Size = New System.Drawing.Size(121, 21)
        Me.cboDiluentRata.TabIndex = 365
        '
        'lblDiluentRata
        '
        Me.lblDiluentRata.AutoSize = True
        Me.lblDiluentRata.Location = New System.Drawing.Point(484, 260)
        Me.lblDiluentRata.Name = "lblDiluentRata"
        Me.lblDiluentRata.Size = New System.Drawing.Size(21, 13)
        Me.lblDiluentRata.TabIndex = 549
        Me.lblDiluentRata.Text = "1%"
        '
        'txtApplicableStandardPercentRata
        '
        Me.txtApplicableStandardPercentRata.Location = New System.Drawing.Point(484, 170)
        Me.txtApplicableStandardPercentRata.Name = "txtApplicableStandardPercentRata"
        Me.txtApplicableStandardPercentRata.Size = New System.Drawing.Size(40, 20)
        Me.txtApplicableStandardPercentRata.TabIndex = 363
        Me.txtApplicableStandardPercentRata.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRefMethodPercentRata
        '
        Me.txtRefMethodPercentRata.Location = New System.Drawing.Point(484, 114)
        Me.txtRefMethodPercentRata.Name = "txtRefMethodPercentRata"
        Me.txtRefMethodPercentRata.Size = New System.Drawing.Size(40, 20)
        Me.txtRefMethodPercentRata.TabIndex = 362
        Me.txtRefMethodPercentRata.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRelativeAccuracy
        '
        Me.txtRelativeAccuracy.AcceptsReturn = True
        Me.txtRelativeAccuracy.BackColor = System.Drawing.SystemColors.Window
        Me.txtRelativeAccuracy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRelativeAccuracy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRelativeAccuracy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRelativeAccuracy.Location = New System.Drawing.Point(652, 82)
        Me.txtRelativeAccuracy.MaxLength = 11
        Me.txtRelativeAccuracy.Name = "txtRelativeAccuracy"
        Me.txtRelativeAccuracy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRelativeAccuracy.Size = New System.Drawing.Size(81, 20)
        Me.txtRelativeAccuracy.TabIndex = 361
        Me.txtRelativeAccuracy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label187
        '
        Me.Label187.AutoSize = True
        Me.Label187.BackColor = System.Drawing.SystemColors.Control
        Me.Label187.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label187.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label187.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label187.Location = New System.Drawing.Point(524, 82)
        Me.Label187.Name = "Label187"
        Me.Label187.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label187.Size = New System.Drawing.Size(118, 14)
        Me.Label187.TabIndex = 546
        Me.Label187.Text = "RELATIVE ACCURACY:"
        Me.Label187.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStandardRata
        '
        Me.lblStandardRata.Location = New System.Drawing.Point(484, 178)
        Me.lblStandardRata.Name = "lblStandardRata"
        Me.lblStandardRata.Size = New System.Drawing.Size(280, 48)
        Me.lblStandardRata.TabIndex = 548
        Me.lblStandardRata.Text = "xxxxxxx % of the applicable standard (when the average of the (RM) test data is l" &
    "ess than 50% of the applicable standard)."
        '
        'lblRefMethodRata
        '
        Me.lblRefMethodRata.Location = New System.Drawing.Point(484, 122)
        Me.lblRefMethodRata.Name = "lblRefMethodRata"
        Me.lblRefMethodRata.Size = New System.Drawing.Size(280, 48)
        Me.lblRefMethodRata.TabIndex = 547
        Me.lblRefMethodRata.Text = "xxxxxxx % of the Average value of the Reference Method (RM) test data (when the a" &
    "verage RM test data is greater then 50% of the applicable standard) ."
        '
        'txtOtherInformationRata
        '
        Me.txtOtherInformationRata.AcceptsReturn = True
        Me.txtOtherInformationRata.Location = New System.Drawing.Point(127, 382)
        Me.txtOtherInformationRata.MaxLength = 4000
        Me.txtOtherInformationRata.Multiline = True
        Me.txtOtherInformationRata.Name = "txtOtherInformationRata"
        Me.txtOtherInformationRata.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationRata.Size = New System.Drawing.Size(620, 109)
        Me.txtOtherInformationRata.TabIndex = 366
        '
        'Label185
        '
        Me.Label185.AutoSize = True
        Me.Label185.Location = New System.Drawing.Point(31, 382)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(91, 13)
        Me.Label185.TabIndex = 500
        Me.Label185.Text = "Other Information:"
        '
        'Label183
        '
        Me.Label183.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label183.Location = New System.Drawing.Point(7, 62)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(760, 1)
        Me.Label183.TabIndex = 499
        Me.Label183.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDilutentMonitoredRata
        '
        Me.cboDilutentMonitoredRata.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDilutentMonitoredRata.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDilutentMonitoredRata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDilutentMonitoredRata.Location = New System.Drawing.Point(487, 6)
        Me.cboDilutentMonitoredRata.Name = "cboDilutentMonitoredRata"
        Me.cboDilutentMonitoredRata.Size = New System.Drawing.Size(176, 21)
        Me.cboDilutentMonitoredRata.TabIndex = 321
        '
        'Label177
        '
        Me.Label177.AutoSize = True
        Me.Label177.Location = New System.Drawing.Point(383, 8)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(93, 13)
        Me.Label177.TabIndex = 498
        Me.Label177.Text = "Diluent Monitored:"
        '
        'txtApplicableRegulationRata
        '
        Me.txtApplicableRegulationRata.Location = New System.Drawing.Point(167, 38)
        Me.txtApplicableRegulationRata.MaxLength = 200
        Me.txtApplicableRegulationRata.Name = "txtApplicableRegulationRata"
        Me.txtApplicableRegulationRata.Size = New System.Drawing.Size(272, 20)
        Me.txtApplicableRegulationRata.TabIndex = 322
        '
        'Label175
        '
        Me.Label175.AutoSize = True
        Me.Label175.Location = New System.Drawing.Point(47, 40)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(113, 13)
        Me.Label175.TabIndex = 497
        Me.Label175.Text = "Applicable Regulation:"
        '
        'txtApplicableStandardRata
        '
        Me.txtApplicableStandardRata.Location = New System.Drawing.Point(167, 7)
        Me.txtApplicableStandardRata.MaxLength = 50
        Me.txtApplicableStandardRata.Name = "txtApplicableStandardRata"
        Me.txtApplicableStandardRata.Size = New System.Drawing.Size(168, 20)
        Me.txtApplicableStandardRata.TabIndex = 320
        '
        'Label173
        '
        Me.Label173.AutoSize = True
        Me.Label173.Location = New System.Drawing.Point(47, 9)
        Me.Label173.Name = "Label173"
        Me.Label173.Size = New System.Drawing.Size(105, 13)
        Me.Label173.TabIndex = 496
        Me.Label173.Text = "Applicable Standard:"
        '
        'TPTwoStack
        '
        Me.TPTwoStack.AutoScroll = True
        Me.TPTwoStack.Controls.Add(Me.Label227)
        Me.TPTwoStack.Controls.Add(Me.Label226)
        Me.TPTwoStack.Controls.Add(Me.Label225)
        Me.TPTwoStack.Controls.Add(Me.Label224)
        Me.TPTwoStack.Controls.Add(Me.Label71)
        Me.TPTwoStack.Controls.Add(Me.Label72)
        Me.TPTwoStack.Controls.Add(Me.Label73)
        Me.TPTwoStack.Controls.Add(Me.Label74)
        Me.TPTwoStack.Controls.Add(Me.txtOtherInformationTwoStack)
        Me.TPTwoStack.Controls.Add(Me.Label56)
        Me.TPTwoStack.Controls.Add(Me.TCTwoStack)
        Me.TPTwoStack.Controls.Add(Me.Label188)
        Me.TPTwoStack.Controls.Add(Me.txtControlEquipmentOperatingDataTwoStack)
        Me.TPTwoStack.Controls.Add(Me.txtApplicableRegulationTwoStack)
        Me.TPTwoStack.Controls.Add(Me.txtOperatingCapacityTwoStack)
        Me.TPTwoStack.Controls.Add(Me.Label189)
        Me.TPTwoStack.Controls.Add(Me.txtAllowableEmissionRate2TwoStack)
        Me.TPTwoStack.Controls.Add(Me.txtAllowableEmissionRate3TwoStack)
        Me.TPTwoStack.Controls.Add(Me.txtAllowableEmissionRate1TwoStack)
        Me.TPTwoStack.Controls.Add(Me.txtMaximumExpectedOperatingCapacityTwoStack)
        Me.TPTwoStack.Controls.Add(Me.cboOperatingCapacityUnitsTwoStack)
        Me.TPTwoStack.Controls.Add(Me.cboAllowableEmissionRateUnits2TwoStack)
        Me.TPTwoStack.Controls.Add(Me.cboAllowableEmissionRateUnits3TwoStack)
        Me.TPTwoStack.Controls.Add(Me.cboAllowableEmissionRateUnits1TwoStack)
        Me.TPTwoStack.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack)
        Me.TPTwoStack.Location = New System.Drawing.Point(4, 22)
        Me.TPTwoStack.Name = "TPTwoStack"
        Me.TPTwoStack.Size = New System.Drawing.Size(782, 288)
        Me.TPTwoStack.TabIndex = 9
        Me.TPTwoStack.Text = "Two Stack"
        Me.TPTwoStack.UseVisualStyleBackColor = True
        '
        'Label227
        '
        Me.Label227.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label227.Location = New System.Drawing.Point(5, 122)
        Me.Label227.Name = "Label227"
        Me.Label227.Size = New System.Drawing.Size(748, 1)
        Me.Label227.TabIndex = 343
        '
        'Label226
        '
        Me.Label226.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label226.Location = New System.Drawing.Point(5, 79)
        Me.Label226.Name = "Label226"
        Me.Label226.Size = New System.Drawing.Size(748, 1)
        Me.Label226.TabIndex = 342
        '
        'Label225
        '
        Me.Label225.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label225.Location = New System.Drawing.Point(5, 48)
        Me.Label225.Name = "Label225"
        Me.Label225.Size = New System.Drawing.Size(748, 1)
        Me.Label225.TabIndex = 341
        '
        'Label224
        '
        Me.Label224.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label224.Location = New System.Drawing.Point(109, 26)
        Me.Label224.Name = "Label224"
        Me.Label224.Size = New System.Drawing.Size(588, 1)
        Me.Label224.TabIndex = 340
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(5, 49)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(122, 13)
        Me.Label71.TabIndex = 338
        Me.Label71.Text = "Applicable Requirement:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(5, 31)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(125, 13)
        Me.Label72.TabIndex = 337
        Me.Label72.Text = "Allowable Emission Rate:"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label73
        '
        Me.Label73.Location = New System.Drawing.Point(5, 81)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(112, 32)
        Me.Label73.TabIndex = 339
        Me.Label73.Text = "Control Equipment and Monitoring Data:"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label74
        '
        Me.Label74.Location = New System.Drawing.Point(5, 3)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(104, 26)
        Me.Label74.TabIndex = 336
        Me.Label74.Text = "Maximum Expected Operating Capacity:"
        '
        'txtOtherInformationTwoStack
        '
        Me.txtOtherInformationTwoStack.AcceptsReturn = True
        Me.txtOtherInformationTwoStack.Location = New System.Drawing.Point(137, 395)
        Me.txtOtherInformationTwoStack.MaxLength = 4000
        Me.txtOtherInformationTwoStack.Multiline = True
        Me.txtOtherInformationTwoStack.Name = "txtOtherInformationTwoStack"
        Me.txtOtherInformationTwoStack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationTwoStack.Size = New System.Drawing.Size(599, 106)
        Me.txtOtherInformationTwoStack.TabIndex = 488
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(5, 395)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(91, 13)
        Me.Label56.TabIndex = 335
        Me.Label56.Text = "Other Information:"
        '
        'TCTwoStack
        '
        Me.TCTwoStack.Controls.Add(Me.TPTwoStackStandard)
        Me.TCTwoStack.Controls.Add(Me.TPTwoStackDRE)
        Me.TCTwoStack.Location = New System.Drawing.Point(1, 125)
        Me.TCTwoStack.Name = "TCTwoStack"
        Me.TCTwoStack.SelectedIndex = 0
        Me.TCTwoStack.Size = New System.Drawing.Size(790, 260)
        Me.TCTwoStack.TabIndex = 344
        Me.TCTwoStack.TabStop = False
        '
        'TPTwoStackStandard
        '
        Me.TPTwoStackStandard.Controls.Add(Me.Label298)
        Me.TPTwoStackStandard.Controls.Add(Me.Label237)
        Me.TPTwoStackStandard.Controls.Add(Me.Label236)
        Me.TPTwoStackStandard.Controls.Add(Me.Label235)
        Me.TPTwoStackStandard.Controls.Add(Me.Label234)
        Me.TPTwoStackStandard.Controls.Add(Me.Label233)
        Me.TPTwoStackStandard.Controls.Add(Me.Label232)
        Me.TPTwoStackStandard.Controls.Add(Me.Label231)
        Me.TPTwoStackStandard.Controls.Add(Me.Label230)
        Me.TPTwoStackStandard.Controls.Add(Me.Label229)
        Me.TPTwoStackStandard.Controls.Add(Me.Label228)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTotalAvgTwoStackStandard)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateAvgTwoStackStandard2)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPollConcAvgTwoStackStandard1)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTotalTwoStackStandard3)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTotalTwoStackStandard2)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTotalTwoStackStandard1)
        Me.TPTwoStackStandard.Controls.Add(Me.Label62)
        Me.TPTwoStackStandard.Controls.Add(Me.txtRunNumTwoStackStandard2C)
        Me.TPTwoStackStandard.Controls.Add(Me.btnClearTwoStackStandard2C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasMoistTwoStackStandard2C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTwoStackStandard2C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPollConcTwoStackStandard2C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowDSCFMTwoStackStandard2C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowACFMTwoStackStandard2C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasMoistTwoStackStandard2B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTwoStackStandard2B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPollConcTwoStackStandard2B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasTempTwoStackStandard2C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowDSCFMTwoStackStandard2B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowACFMTwoStackStandard2B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasTempTwoStackStandard2B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtRunNumTwoStackStandard2B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTwoStackStandard2A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPollConcTwoStackStandard2A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasMoistTwoStackStandard2A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowDSCFMTwoStackStandard2A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowACFMTwoStackStandard2A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasTempTwoStackStandard2A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtRunNumTwoStackStandard2A)
        Me.TPTwoStackStandard.Controls.Add(Me.btnClearTwoStackStandard2A)
        Me.TPTwoStackStandard.Controls.Add(Me.btnClearTwoStackStandard2B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtRunNumTwoStackStandard1C)
        Me.TPTwoStackStandard.Controls.Add(Me.btnClearTwoStackStandard1C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasMoistTwoStackStandard1C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTwoStackStandard1C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPollConcTwoStackStandard1C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowDSCFMTwoStackStandard1C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowACFMTwoStackStandard1C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasMoistTwoStackStandard1B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtStackOneNameTwoStackStandard)
        Me.TPTwoStackStandard.Controls.Add(Me.txtStackTwoNameTwoStackStandard)
        Me.TPTwoStackStandard.Controls.Add(Me.Label61)
        Me.TPTwoStackStandard.Controls.Add(Me.Label57)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateAvgTwoStackStandard1)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPollConcAvgTwoStackStandard2)
        Me.TPTwoStackStandard.Controls.Add(Me.cboEmissRateUnitTwoStackStandard)
        Me.TPTwoStackStandard.Controls.Add(Me.cboPollConUnitTwoStackStandard)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTwoStackStandard1B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPollConcTwoStackStandard1B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasTempTwoStackStandard1C)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowDSCFMTwoStackStandard1B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowACFMTwoStackStandard1B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasTempTwoStackStandard1B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtRunNumTwoStackStandard1B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtEmissRateTwoStackStandard1A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPollConcTwoStackStandard1A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasMoistTwoStackStandard1A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowDSCFMTwoStackStandard1A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasFlowACFMTwoStackStandard1A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtGasTempTwoStackStandard1A)
        Me.TPTwoStackStandard.Controls.Add(Me.txtRunNumTwoStackStandard1A)
        Me.TPTwoStackStandard.Controls.Add(Me.Label58)
        Me.TPTwoStackStandard.Controls.Add(Me.Label59)
        Me.TPTwoStackStandard.Controls.Add(Me.Label60)
        Me.TPTwoStackStandard.Controls.Add(Me.Label63)
        Me.TPTwoStackStandard.Controls.Add(Me.Label64)
        Me.TPTwoStackStandard.Controls.Add(Me.Label69)
        Me.TPTwoStackStandard.Controls.Add(Me.Label70)
        Me.TPTwoStackStandard.Controls.Add(Me.btnClearTwoStackStandard1A)
        Me.TPTwoStackStandard.Controls.Add(Me.btnClearTwoStackStandard1B)
        Me.TPTwoStackStandard.Controls.Add(Me.txtPercentAllowableTwoStack)
        Me.TPTwoStackStandard.Controls.Add(Me.Label75)
        Me.TPTwoStackStandard.Location = New System.Drawing.Point(4, 22)
        Me.TPTwoStackStandard.Name = "TPTwoStackStandard"
        Me.TPTwoStackStandard.Size = New System.Drawing.Size(782, 234)
        Me.TPTwoStackStandard.TabIndex = 2
        Me.TPTwoStackStandard.Text = "Standard"
        '
        'Label298
        '
        Me.Label298.AutoSize = True
        Me.Label298.Location = New System.Drawing.Point(360, 192)
        Me.Label298.Name = "Label298"
        Me.Label298.Size = New System.Drawing.Size(47, 13)
        Me.Label298.TabIndex = 273
        Me.Label298.Text = "Average"
        Me.Label298.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label237
        '
        Me.Label237.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label237.Location = New System.Drawing.Point(4, 231)
        Me.Label237.Name = "Label237"
        Me.Label237.Size = New System.Drawing.Size(200, 1)
        Me.Label237.TabIndex = 272
        '
        'Label236
        '
        Me.Label236.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label236.Location = New System.Drawing.Point(4, 209)
        Me.Label236.Name = "Label236"
        Me.Label236.Size = New System.Drawing.Size(488, 1)
        Me.Label236.TabIndex = 271
        '
        'Label235
        '
        Me.Label235.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label235.Location = New System.Drawing.Point(4, 187)
        Me.Label235.Name = "Label235"
        Me.Label235.Size = New System.Drawing.Size(560, 1)
        Me.Label235.TabIndex = 270
        '
        'Label234
        '
        Me.Label234.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label234.Location = New System.Drawing.Point(4, 165)
        Me.Label234.Name = "Label234"
        Me.Label234.Size = New System.Drawing.Size(762, 1)
        Me.Label234.TabIndex = 269
        '
        'Label233
        '
        Me.Label233.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label233.Location = New System.Drawing.Point(4, 143)
        Me.Label233.Name = "Label233"
        Me.Label233.Size = New System.Drawing.Size(560, 1)
        Me.Label233.TabIndex = 268
        '
        'Label232
        '
        Me.Label232.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label232.Location = New System.Drawing.Point(4, 121)
        Me.Label232.Name = "Label232"
        Me.Label232.Size = New System.Drawing.Size(560, 1)
        Me.Label232.TabIndex = 267
        '
        'Label231
        '
        Me.Label231.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label231.Location = New System.Drawing.Point(4, 99)
        Me.Label231.Name = "Label231"
        Me.Label231.Size = New System.Drawing.Size(560, 1)
        Me.Label231.TabIndex = 266
        '
        'Label230
        '
        Me.Label230.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label230.Location = New System.Drawing.Point(4, 77)
        Me.Label230.Name = "Label230"
        Me.Label230.Size = New System.Drawing.Size(560, 1)
        Me.Label230.TabIndex = 265
        '
        'Label229
        '
        Me.Label229.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label229.Location = New System.Drawing.Point(36, 53)
        Me.Label229.Name = "Label229"
        Me.Label229.Size = New System.Drawing.Size(532, 1)
        Me.Label229.TabIndex = 264
        '
        'Label228
        '
        Me.Label228.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label228.Location = New System.Drawing.Point(36, 29)
        Me.Label228.Name = "Label228"
        Me.Label228.Size = New System.Drawing.Size(532, 1)
        Me.Label228.TabIndex = 263
        '
        'txtEmissRateTotalAvgTwoStackStandard
        '
        Me.txtEmissRateTotalAvgTwoStackStandard.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateTotalAvgTwoStackStandard.Location = New System.Drawing.Point(424, 188)
        Me.txtEmissRateTotalAvgTwoStackStandard.Name = "txtEmissRateTotalAvgTwoStackStandard"
        Me.txtEmissRateTotalAvgTwoStackStandard.ReadOnly = True
        Me.txtEmissRateTotalAvgTwoStackStandard.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTotalAvgTwoStackStandard.TabIndex = 435
        Me.txtEmissRateTotalAvgTwoStackStandard.TabStop = False
        '
        'txtEmissRateAvgTwoStackStandard2
        '
        Me.txtEmissRateAvgTwoStackStandard2.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateAvgTwoStackStandard2.Location = New System.Drawing.Point(704, 188)
        Me.txtEmissRateAvgTwoStackStandard2.MaxLength = 11
        Me.txtEmissRateAvgTwoStackStandard2.Name = "txtEmissRateAvgTwoStackStandard2"
        Me.txtEmissRateAvgTwoStackStandard2.ReadOnly = True
        Me.txtEmissRateAvgTwoStackStandard2.Size = New System.Drawing.Size(62, 20)
        Me.txtEmissRateAvgTwoStackStandard2.TabIndex = 431
        Me.txtEmissRateAvgTwoStackStandard2.TabStop = False
        '
        'txtPollConcAvgTwoStackStandard1
        '
        Me.txtPollConcAvgTwoStackStandard1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgTwoStackStandard1.Location = New System.Drawing.Point(704, 122)
        Me.txtPollConcAvgTwoStackStandard1.MaxLength = 11
        Me.txtPollConcAvgTwoStackStandard1.Name = "txtPollConcAvgTwoStackStandard1"
        Me.txtPollConcAvgTwoStackStandard1.ReadOnly = True
        Me.txtPollConcAvgTwoStackStandard1.Size = New System.Drawing.Size(62, 20)
        Me.txtPollConcAvgTwoStackStandard1.TabIndex = 421
        Me.txtPollConcAvgTwoStackStandard1.TabStop = False
        '
        'txtEmissRateTotalTwoStackStandard3
        '
        Me.txtEmissRateTotalTwoStackStandard3.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateTotalTwoStackStandard3.Location = New System.Drawing.Point(282, 188)
        Me.txtEmissRateTotalTwoStackStandard3.MaxLength = 11
        Me.txtEmissRateTotalTwoStackStandard3.Name = "txtEmissRateTotalTwoStackStandard3"
        Me.txtEmissRateTotalTwoStackStandard3.ReadOnly = True
        Me.txtEmissRateTotalTwoStackStandard3.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTotalTwoStackStandard3.TabIndex = 434
        Me.txtEmissRateTotalTwoStackStandard3.TabStop = False
        '
        'txtEmissRateTotalTwoStackStandard2
        '
        Me.txtEmissRateTotalTwoStackStandard2.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateTotalTwoStackStandard2.Location = New System.Drawing.Point(209, 188)
        Me.txtEmissRateTotalTwoStackStandard2.MaxLength = 11
        Me.txtEmissRateTotalTwoStackStandard2.Name = "txtEmissRateTotalTwoStackStandard2"
        Me.txtEmissRateTotalTwoStackStandard2.ReadOnly = True
        Me.txtEmissRateTotalTwoStackStandard2.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTotalTwoStackStandard2.TabIndex = 433
        Me.txtEmissRateTotalTwoStackStandard2.TabStop = False
        '
        'txtEmissRateTotalTwoStackStandard1
        '
        Me.txtEmissRateTotalTwoStackStandard1.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateTotalTwoStackStandard1.Location = New System.Drawing.Point(136, 188)
        Me.txtEmissRateTotalTwoStackStandard1.MaxLength = 11
        Me.txtEmissRateTotalTwoStackStandard1.Name = "txtEmissRateTotalTwoStackStandard1"
        Me.txtEmissRateTotalTwoStackStandard1.ReadOnly = True
        Me.txtEmissRateTotalTwoStackStandard1.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTotalTwoStackStandard1.TabIndex = 432
        Me.txtEmissRateTotalTwoStackStandard1.TabStop = False
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(4, 188)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(119, 13)
        Me.Label62.TabIndex = 256
        Me.Label62.Text = "Total for Stack 1 and 2:"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtRunNumTwoStackStandard2C
        '
        Me.txtRunNumTwoStackStandard2C.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackStandard2C.Location = New System.Drawing.Point(502, 32)
        Me.txtRunNumTwoStackStandard2C.MaxLength = 3
        Me.txtRunNumTwoStackStandard2C.Name = "txtRunNumTwoStackStandard2C"
        Me.txtRunNumTwoStackStandard2C.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackStandard2C.TabIndex = 389
        '
        'btnClearTwoStackStandard2C
        '
        Me.btnClearTwoStackStandard2C.Location = New System.Drawing.Point(527, 32)
        Me.btnClearTwoStackStandard2C.Name = "btnClearTwoStackStandard2C"
        Me.btnClearTwoStackStandard2C.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackStandard2C.TabIndex = 255
        Me.btnClearTwoStackStandard2C.Text = "Clear"
        '
        'txtGasMoistTwoStackStandard2C
        '
        Me.txtGasMoistTwoStackStandard2C.Location = New System.Drawing.Point(496, 78)
        Me.txtGasMoistTwoStackStandard2C.MaxLength = 11
        Me.txtGasMoistTwoStackStandard2C.Name = "txtGasMoistTwoStackStandard2C"
        Me.txtGasMoistTwoStackStandard2C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackStandard2C.TabIndex = 401
        '
        'txtEmissRateTwoStackStandard2C
        '
        Me.txtEmissRateTwoStackStandard2C.Location = New System.Drawing.Point(496, 166)
        Me.txtEmissRateTwoStackStandard2C.MaxLength = 11
        Me.txtEmissRateTwoStackStandard2C.Name = "txtEmissRateTwoStackStandard2C"
        Me.txtEmissRateTwoStackStandard2C.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackStandard2C.TabIndex = 428
        '
        'txtPollConcTwoStackStandard2C
        '
        Me.txtPollConcTwoStackStandard2C.Location = New System.Drawing.Point(496, 144)
        Me.txtPollConcTwoStackStandard2C.MaxLength = 11
        Me.txtPollConcTwoStackStandard2C.Name = "txtPollConcTwoStackStandard2C"
        Me.txtPollConcTwoStackStandard2C.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackStandard2C.TabIndex = 419
        '
        'txtGasFlowDSCFMTwoStackStandard2C
        '
        Me.txtGasFlowDSCFMTwoStackStandard2C.Location = New System.Drawing.Point(496, 122)
        Me.txtGasFlowDSCFMTwoStackStandard2C.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackStandard2C.Name = "txtGasFlowDSCFMTwoStackStandard2C"
        Me.txtGasFlowDSCFMTwoStackStandard2C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackStandard2C.TabIndex = 413
        '
        'txtGasFlowACFMTwoStackStandard2C
        '
        Me.txtGasFlowACFMTwoStackStandard2C.Location = New System.Drawing.Point(496, 100)
        Me.txtGasFlowACFMTwoStackStandard2C.MaxLength = 11
        Me.txtGasFlowACFMTwoStackStandard2C.Name = "txtGasFlowACFMTwoStackStandard2C"
        Me.txtGasFlowACFMTwoStackStandard2C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackStandard2C.TabIndex = 407
        '
        'txtGasMoistTwoStackStandard2B
        '
        Me.txtGasMoistTwoStackStandard2B.Location = New System.Drawing.Point(424, 78)
        Me.txtGasMoistTwoStackStandard2B.MaxLength = 11
        Me.txtGasMoistTwoStackStandard2B.Name = "txtGasMoistTwoStackStandard2B"
        Me.txtGasMoistTwoStackStandard2B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackStandard2B.TabIndex = 400
        '
        'txtEmissRateTwoStackStandard2B
        '
        Me.txtEmissRateTwoStackStandard2B.Location = New System.Drawing.Point(424, 166)
        Me.txtEmissRateTwoStackStandard2B.MaxLength = 11
        Me.txtEmissRateTwoStackStandard2B.Name = "txtEmissRateTwoStackStandard2B"
        Me.txtEmissRateTwoStackStandard2B.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackStandard2B.TabIndex = 427
        '
        'txtPollConcTwoStackStandard2B
        '
        Me.txtPollConcTwoStackStandard2B.Location = New System.Drawing.Point(424, 144)
        Me.txtPollConcTwoStackStandard2B.MaxLength = 11
        Me.txtPollConcTwoStackStandard2B.Name = "txtPollConcTwoStackStandard2B"
        Me.txtPollConcTwoStackStandard2B.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackStandard2B.TabIndex = 418
        '
        'txtGasTempTwoStackStandard2C
        '
        Me.txtGasTempTwoStackStandard2C.Location = New System.Drawing.Point(496, 56)
        Me.txtGasTempTwoStackStandard2C.MaxLength = 11
        Me.txtGasTempTwoStackStandard2C.Name = "txtGasTempTwoStackStandard2C"
        Me.txtGasTempTwoStackStandard2C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackStandard2C.TabIndex = 395
        '
        'txtGasFlowDSCFMTwoStackStandard2B
        '
        Me.txtGasFlowDSCFMTwoStackStandard2B.Location = New System.Drawing.Point(424, 122)
        Me.txtGasFlowDSCFMTwoStackStandard2B.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackStandard2B.Name = "txtGasFlowDSCFMTwoStackStandard2B"
        Me.txtGasFlowDSCFMTwoStackStandard2B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackStandard2B.TabIndex = 412
        '
        'txtGasFlowACFMTwoStackStandard2B
        '
        Me.txtGasFlowACFMTwoStackStandard2B.Location = New System.Drawing.Point(424, 100)
        Me.txtGasFlowACFMTwoStackStandard2B.MaxLength = 11
        Me.txtGasFlowACFMTwoStackStandard2B.Name = "txtGasFlowACFMTwoStackStandard2B"
        Me.txtGasFlowACFMTwoStackStandard2B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackStandard2B.TabIndex = 406
        '
        'txtGasTempTwoStackStandard2B
        '
        Me.txtGasTempTwoStackStandard2B.Location = New System.Drawing.Point(424, 56)
        Me.txtGasTempTwoStackStandard2B.MaxLength = 11
        Me.txtGasTempTwoStackStandard2B.Name = "txtGasTempTwoStackStandard2B"
        Me.txtGasTempTwoStackStandard2B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackStandard2B.TabIndex = 394
        '
        'txtRunNumTwoStackStandard2B
        '
        Me.txtRunNumTwoStackStandard2B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackStandard2B.Location = New System.Drawing.Point(430, 32)
        Me.txtRunNumTwoStackStandard2B.MaxLength = 3
        Me.txtRunNumTwoStackStandard2B.Name = "txtRunNumTwoStackStandard2B"
        Me.txtRunNumTwoStackStandard2B.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackStandard2B.TabIndex = 388
        '
        'txtEmissRateTwoStackStandard2A
        '
        Me.txtEmissRateTwoStackStandard2A.Location = New System.Drawing.Point(352, 166)
        Me.txtEmissRateTwoStackStandard2A.MaxLength = 11
        Me.txtEmissRateTwoStackStandard2A.Name = "txtEmissRateTwoStackStandard2A"
        Me.txtEmissRateTwoStackStandard2A.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackStandard2A.TabIndex = 426
        '
        'txtPollConcTwoStackStandard2A
        '
        Me.txtPollConcTwoStackStandard2A.Location = New System.Drawing.Point(352, 144)
        Me.txtPollConcTwoStackStandard2A.MaxLength = 11
        Me.txtPollConcTwoStackStandard2A.Name = "txtPollConcTwoStackStandard2A"
        Me.txtPollConcTwoStackStandard2A.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackStandard2A.TabIndex = 417
        '
        'txtGasMoistTwoStackStandard2A
        '
        Me.txtGasMoistTwoStackStandard2A.Location = New System.Drawing.Point(352, 78)
        Me.txtGasMoistTwoStackStandard2A.MaxLength = 11
        Me.txtGasMoistTwoStackStandard2A.Name = "txtGasMoistTwoStackStandard2A"
        Me.txtGasMoistTwoStackStandard2A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackStandard2A.TabIndex = 399
        '
        'txtGasFlowDSCFMTwoStackStandard2A
        '
        Me.txtGasFlowDSCFMTwoStackStandard2A.Location = New System.Drawing.Point(352, 122)
        Me.txtGasFlowDSCFMTwoStackStandard2A.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackStandard2A.Name = "txtGasFlowDSCFMTwoStackStandard2A"
        Me.txtGasFlowDSCFMTwoStackStandard2A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackStandard2A.TabIndex = 411
        '
        'txtGasFlowACFMTwoStackStandard2A
        '
        Me.txtGasFlowACFMTwoStackStandard2A.Location = New System.Drawing.Point(352, 100)
        Me.txtGasFlowACFMTwoStackStandard2A.MaxLength = 11
        Me.txtGasFlowACFMTwoStackStandard2A.Name = "txtGasFlowACFMTwoStackStandard2A"
        Me.txtGasFlowACFMTwoStackStandard2A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackStandard2A.TabIndex = 405
        '
        'txtGasTempTwoStackStandard2A
        '
        Me.txtGasTempTwoStackStandard2A.Location = New System.Drawing.Point(352, 56)
        Me.txtGasTempTwoStackStandard2A.MaxLength = 11
        Me.txtGasTempTwoStackStandard2A.Name = "txtGasTempTwoStackStandard2A"
        Me.txtGasTempTwoStackStandard2A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackStandard2A.TabIndex = 393
        '
        'txtRunNumTwoStackStandard2A
        '
        Me.txtRunNumTwoStackStandard2A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackStandard2A.Location = New System.Drawing.Point(358, 32)
        Me.txtRunNumTwoStackStandard2A.MaxLength = 3
        Me.txtRunNumTwoStackStandard2A.Name = "txtRunNumTwoStackStandard2A"
        Me.txtRunNumTwoStackStandard2A.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackStandard2A.TabIndex = 387
        '
        'btnClearTwoStackStandard2A
        '
        Me.btnClearTwoStackStandard2A.Location = New System.Drawing.Point(383, 32)
        Me.btnClearTwoStackStandard2A.Name = "btnClearTwoStackStandard2A"
        Me.btnClearTwoStackStandard2A.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackStandard2A.TabIndex = 247
        Me.btnClearTwoStackStandard2A.Text = "Clear"
        '
        'btnClearTwoStackStandard2B
        '
        Me.btnClearTwoStackStandard2B.Location = New System.Drawing.Point(455, 32)
        Me.btnClearTwoStackStandard2B.Name = "btnClearTwoStackStandard2B"
        Me.btnClearTwoStackStandard2B.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackStandard2B.TabIndex = 246
        Me.btnClearTwoStackStandard2B.Text = "Clear"
        '
        'txtRunNumTwoStackStandard1C
        '
        Me.txtRunNumTwoStackStandard1C.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackStandard1C.Location = New System.Drawing.Point(288, 32)
        Me.txtRunNumTwoStackStandard1C.MaxLength = 3
        Me.txtRunNumTwoStackStandard1C.Name = "txtRunNumTwoStackStandard1C"
        Me.txtRunNumTwoStackStandard1C.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackStandard1C.TabIndex = 386
        '
        'btnClearTwoStackStandard1C
        '
        Me.btnClearTwoStackStandard1C.Location = New System.Drawing.Point(313, 32)
        Me.btnClearTwoStackStandard1C.Name = "btnClearTwoStackStandard1C"
        Me.btnClearTwoStackStandard1C.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackStandard1C.TabIndex = 231
        Me.btnClearTwoStackStandard1C.Text = "Clear"
        '
        'txtGasMoistTwoStackStandard1C
        '
        Me.txtGasMoistTwoStackStandard1C.Location = New System.Drawing.Point(282, 78)
        Me.txtGasMoistTwoStackStandard1C.MaxLength = 11
        Me.txtGasMoistTwoStackStandard1C.Name = "txtGasMoistTwoStackStandard1C"
        Me.txtGasMoistTwoStackStandard1C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackStandard1C.TabIndex = 398
        '
        'txtEmissRateTwoStackStandard1C
        '
        Me.txtEmissRateTwoStackStandard1C.Location = New System.Drawing.Point(282, 166)
        Me.txtEmissRateTwoStackStandard1C.MaxLength = 11
        Me.txtEmissRateTwoStackStandard1C.Name = "txtEmissRateTwoStackStandard1C"
        Me.txtEmissRateTwoStackStandard1C.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackStandard1C.TabIndex = 425
        '
        'txtPollConcTwoStackStandard1C
        '
        Me.txtPollConcTwoStackStandard1C.Location = New System.Drawing.Point(282, 144)
        Me.txtPollConcTwoStackStandard1C.MaxLength = 11
        Me.txtPollConcTwoStackStandard1C.Name = "txtPollConcTwoStackStandard1C"
        Me.txtPollConcTwoStackStandard1C.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackStandard1C.TabIndex = 416
        '
        'txtGasFlowDSCFMTwoStackStandard1C
        '
        Me.txtGasFlowDSCFMTwoStackStandard1C.Location = New System.Drawing.Point(282, 122)
        Me.txtGasFlowDSCFMTwoStackStandard1C.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackStandard1C.Name = "txtGasFlowDSCFMTwoStackStandard1C"
        Me.txtGasFlowDSCFMTwoStackStandard1C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackStandard1C.TabIndex = 410
        '
        'txtGasFlowACFMTwoStackStandard1C
        '
        Me.txtGasFlowACFMTwoStackStandard1C.Location = New System.Drawing.Point(282, 100)
        Me.txtGasFlowACFMTwoStackStandard1C.MaxLength = 11
        Me.txtGasFlowACFMTwoStackStandard1C.Name = "txtGasFlowACFMTwoStackStandard1C"
        Me.txtGasFlowACFMTwoStackStandard1C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackStandard1C.TabIndex = 404
        '
        'txtGasMoistTwoStackStandard1B
        '
        Me.txtGasMoistTwoStackStandard1B.Location = New System.Drawing.Point(209, 78)
        Me.txtGasMoistTwoStackStandard1B.MaxLength = 11
        Me.txtGasMoistTwoStackStandard1B.Name = "txtGasMoistTwoStackStandard1B"
        Me.txtGasMoistTwoStackStandard1B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackStandard1B.TabIndex = 397
        '
        'txtStackOneNameTwoStackStandard
        '
        Me.txtStackOneNameTwoStackStandard.BackColor = System.Drawing.SystemColors.Window
        Me.txtStackOneNameTwoStackStandard.Location = New System.Drawing.Point(136, 8)
        Me.txtStackOneNameTwoStackStandard.MaxLength = 30
        Me.txtStackOneNameTwoStackStandard.Name = "txtStackOneNameTwoStackStandard"
        Me.txtStackOneNameTwoStackStandard.Size = New System.Drawing.Size(216, 20)
        Me.txtStackOneNameTwoStackStandard.TabIndex = 382
        '
        'txtStackTwoNameTwoStackStandard
        '
        Me.txtStackTwoNameTwoStackStandard.BackColor = System.Drawing.SystemColors.Window
        Me.txtStackTwoNameTwoStackStandard.Location = New System.Drawing.Point(352, 8)
        Me.txtStackTwoNameTwoStackStandard.MaxLength = 30
        Me.txtStackTwoNameTwoStackStandard.Name = "txtStackTwoNameTwoStackStandard"
        Me.txtStackTwoNameTwoStackStandard.Size = New System.Drawing.Size(216, 20)
        Me.txtStackTwoNameTwoStackStandard.TabIndex = 383
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(36, 8)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(69, 13)
        Me.Label61.TabIndex = 221
        Me.Label61.Text = "Stack Name:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(704, 100)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(65, 13)
        Me.Label57.TabIndex = 217
        Me.Label57.Text = "AVERAGES"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txtEmissRateAvgTwoStackStandard1
        '
        Me.txtEmissRateAvgTwoStackStandard1.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateAvgTwoStackStandard1.Location = New System.Drawing.Point(704, 165)
        Me.txtEmissRateAvgTwoStackStandard1.MaxLength = 11
        Me.txtEmissRateAvgTwoStackStandard1.Name = "txtEmissRateAvgTwoStackStandard1"
        Me.txtEmissRateAvgTwoStackStandard1.ReadOnly = True
        Me.txtEmissRateAvgTwoStackStandard1.Size = New System.Drawing.Size(62, 20)
        Me.txtEmissRateAvgTwoStackStandard1.TabIndex = 430
        Me.txtEmissRateAvgTwoStackStandard1.TabStop = False
        '
        'txtPollConcAvgTwoStackStandard2
        '
        Me.txtPollConcAvgTwoStackStandard2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgTwoStackStandard2.Location = New System.Drawing.Point(704, 144)
        Me.txtPollConcAvgTwoStackStandard2.MaxLength = 11
        Me.txtPollConcAvgTwoStackStandard2.Name = "txtPollConcAvgTwoStackStandard2"
        Me.txtPollConcAvgTwoStackStandard2.ReadOnly = True
        Me.txtPollConcAvgTwoStackStandard2.Size = New System.Drawing.Size(62, 20)
        Me.txtPollConcAvgTwoStackStandard2.TabIndex = 422
        Me.txtPollConcAvgTwoStackStandard2.TabStop = False
        '
        'cboEmissRateUnitTwoStackStandard
        '
        Me.cboEmissRateUnitTwoStackStandard.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEmissRateUnitTwoStackStandard.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEmissRateUnitTwoStackStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmissRateUnitTwoStackStandard.Location = New System.Drawing.Point(574, 165)
        Me.cboEmissRateUnitTwoStackStandard.Name = "cboEmissRateUnitTwoStackStandard"
        Me.cboEmissRateUnitTwoStackStandard.Size = New System.Drawing.Size(120, 21)
        Me.cboEmissRateUnitTwoStackStandard.TabIndex = 429
        '
        'cboPollConUnitTwoStackStandard
        '
        Me.cboPollConUnitTwoStackStandard.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitTwoStackStandard.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitTwoStackStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPollConUnitTwoStackStandard.Location = New System.Drawing.Point(574, 143)
        Me.cboPollConUnitTwoStackStandard.Name = "cboPollConUnitTwoStackStandard"
        Me.cboPollConUnitTwoStackStandard.Size = New System.Drawing.Size(120, 21)
        Me.cboPollConUnitTwoStackStandard.TabIndex = 420
        '
        'txtEmissRateTwoStackStandard1B
        '
        Me.txtEmissRateTwoStackStandard1B.Location = New System.Drawing.Point(209, 166)
        Me.txtEmissRateTwoStackStandard1B.MaxLength = 11
        Me.txtEmissRateTwoStackStandard1B.Name = "txtEmissRateTwoStackStandard1B"
        Me.txtEmissRateTwoStackStandard1B.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackStandard1B.TabIndex = 424
        '
        'txtPollConcTwoStackStandard1B
        '
        Me.txtPollConcTwoStackStandard1B.Location = New System.Drawing.Point(209, 144)
        Me.txtPollConcTwoStackStandard1B.MaxLength = 11
        Me.txtPollConcTwoStackStandard1B.Name = "txtPollConcTwoStackStandard1B"
        Me.txtPollConcTwoStackStandard1B.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackStandard1B.TabIndex = 415
        '
        'txtGasTempTwoStackStandard1C
        '
        Me.txtGasTempTwoStackStandard1C.Location = New System.Drawing.Point(282, 56)
        Me.txtGasTempTwoStackStandard1C.MaxLength = 11
        Me.txtGasTempTwoStackStandard1C.Name = "txtGasTempTwoStackStandard1C"
        Me.txtGasTempTwoStackStandard1C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackStandard1C.TabIndex = 392
        '
        'txtGasFlowDSCFMTwoStackStandard1B
        '
        Me.txtGasFlowDSCFMTwoStackStandard1B.Location = New System.Drawing.Point(209, 122)
        Me.txtGasFlowDSCFMTwoStackStandard1B.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackStandard1B.Name = "txtGasFlowDSCFMTwoStackStandard1B"
        Me.txtGasFlowDSCFMTwoStackStandard1B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackStandard1B.TabIndex = 409
        '
        'txtGasFlowACFMTwoStackStandard1B
        '
        Me.txtGasFlowACFMTwoStackStandard1B.Location = New System.Drawing.Point(209, 100)
        Me.txtGasFlowACFMTwoStackStandard1B.MaxLength = 11
        Me.txtGasFlowACFMTwoStackStandard1B.Name = "txtGasFlowACFMTwoStackStandard1B"
        Me.txtGasFlowACFMTwoStackStandard1B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackStandard1B.TabIndex = 403
        '
        'txtGasTempTwoStackStandard1B
        '
        Me.txtGasTempTwoStackStandard1B.Location = New System.Drawing.Point(209, 56)
        Me.txtGasTempTwoStackStandard1B.MaxLength = 11
        Me.txtGasTempTwoStackStandard1B.Name = "txtGasTempTwoStackStandard1B"
        Me.txtGasTempTwoStackStandard1B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackStandard1B.TabIndex = 391
        '
        'txtRunNumTwoStackStandard1B
        '
        Me.txtRunNumTwoStackStandard1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackStandard1B.Location = New System.Drawing.Point(215, 32)
        Me.txtRunNumTwoStackStandard1B.MaxLength = 3
        Me.txtRunNumTwoStackStandard1B.Name = "txtRunNumTwoStackStandard1B"
        Me.txtRunNumTwoStackStandard1B.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackStandard1B.TabIndex = 385
        '
        'txtEmissRateTwoStackStandard1A
        '
        Me.txtEmissRateTwoStackStandard1A.Location = New System.Drawing.Point(136, 166)
        Me.txtEmissRateTwoStackStandard1A.MaxLength = 11
        Me.txtEmissRateTwoStackStandard1A.Name = "txtEmissRateTwoStackStandard1A"
        Me.txtEmissRateTwoStackStandard1A.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackStandard1A.TabIndex = 423
        '
        'txtPollConcTwoStackStandard1A
        '
        Me.txtPollConcTwoStackStandard1A.Location = New System.Drawing.Point(136, 144)
        Me.txtPollConcTwoStackStandard1A.MaxLength = 11
        Me.txtPollConcTwoStackStandard1A.Name = "txtPollConcTwoStackStandard1A"
        Me.txtPollConcTwoStackStandard1A.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackStandard1A.TabIndex = 414
        '
        'txtGasMoistTwoStackStandard1A
        '
        Me.txtGasMoistTwoStackStandard1A.Location = New System.Drawing.Point(136, 78)
        Me.txtGasMoistTwoStackStandard1A.MaxLength = 11
        Me.txtGasMoistTwoStackStandard1A.Name = "txtGasMoistTwoStackStandard1A"
        Me.txtGasMoistTwoStackStandard1A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackStandard1A.TabIndex = 396
        '
        'txtGasFlowDSCFMTwoStackStandard1A
        '
        Me.txtGasFlowDSCFMTwoStackStandard1A.Location = New System.Drawing.Point(136, 122)
        Me.txtGasFlowDSCFMTwoStackStandard1A.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackStandard1A.Name = "txtGasFlowDSCFMTwoStackStandard1A"
        Me.txtGasFlowDSCFMTwoStackStandard1A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackStandard1A.TabIndex = 408
        '
        'txtGasFlowACFMTwoStackStandard1A
        '
        Me.txtGasFlowACFMTwoStackStandard1A.Location = New System.Drawing.Point(136, 100)
        Me.txtGasFlowACFMTwoStackStandard1A.MaxLength = 11
        Me.txtGasFlowACFMTwoStackStandard1A.Name = "txtGasFlowACFMTwoStackStandard1A"
        Me.txtGasFlowACFMTwoStackStandard1A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackStandard1A.TabIndex = 402
        '
        'txtGasTempTwoStackStandard1A
        '
        Me.txtGasTempTwoStackStandard1A.Location = New System.Drawing.Point(136, 56)
        Me.txtGasTempTwoStackStandard1A.MaxLength = 11
        Me.txtGasTempTwoStackStandard1A.Name = "txtGasTempTwoStackStandard1A"
        Me.txtGasTempTwoStackStandard1A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackStandard1A.TabIndex = 390
        '
        'txtRunNumTwoStackStandard1A
        '
        Me.txtRunNumTwoStackStandard1A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackStandard1A.Location = New System.Drawing.Point(142, 32)
        Me.txtRunNumTwoStackStandard1A.MaxLength = 3
        Me.txtRunNumTwoStackStandard1A.Name = "txtRunNumTwoStackStandard1A"
        Me.txtRunNumTwoStackStandard1A.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackStandard1A.TabIndex = 384
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(4, 78)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(86, 13)
        Me.Label58.TabIndex = 188
        Me.Label58.Text = "Gas Moisture (%)"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(4, 100)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(115, 13)
        Me.Label59.TabIndex = 187
        Me.Label59.Text = "Gas Flow Rate (ACFM)"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(4, 122)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(123, 13)
        Me.Label60.TabIndex = 186
        Me.Label60.Text = "Gas Flow Rate (DSCFM)"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(4, 144)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(120, 13)
        Me.Label63.TabIndex = 185
        Me.Label63.Text = "Pollutant Concentration:"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(4, 166)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(77, 13)
        Me.Label64.TabIndex = 184
        Me.Label64.Text = "Emission Rate:"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(4, 56)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(107, 13)
        Me.Label69.TabIndex = 183
        Me.Label69.Text = "Gas Temperature (F):"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(36, 32)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(61, 13)
        Me.Label70.TabIndex = 181
        Me.Label70.Text = "Test Run #"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'btnClearTwoStackStandard1A
        '
        Me.btnClearTwoStackStandard1A.Location = New System.Drawing.Point(167, 32)
        Me.btnClearTwoStackStandard1A.Name = "btnClearTwoStackStandard1A"
        Me.btnClearTwoStackStandard1A.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackStandard1A.TabIndex = 190
        Me.btnClearTwoStackStandard1A.Text = "Clear"
        '
        'btnClearTwoStackStandard1B
        '
        Me.btnClearTwoStackStandard1B.Location = New System.Drawing.Point(240, 32)
        Me.btnClearTwoStackStandard1B.Name = "btnClearTwoStackStandard1B"
        Me.btnClearTwoStackStandard1B.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackStandard1B.TabIndex = 189
        Me.btnClearTwoStackStandard1B.Text = "Clear"
        '
        'txtPercentAllowableTwoStack
        '
        Me.txtPercentAllowableTwoStack.Location = New System.Drawing.Point(136, 210)
        Me.txtPercentAllowableTwoStack.MaxLength = 11
        Me.txtPercentAllowableTwoStack.Name = "txtPercentAllowableTwoStack"
        Me.txtPercentAllowableTwoStack.Size = New System.Drawing.Size(68, 20)
        Me.txtPercentAllowableTwoStack.TabIndex = 436
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(4, 210)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(95, 13)
        Me.Label75.TabIndex = 236
        Me.Label75.Text = "Percent Allowable:"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPTwoStackDRE
        '
        Me.TPTwoStackDRE.Controls.Add(Me.Label246)
        Me.TPTwoStackDRE.Controls.Add(Me.Label245)
        Me.TPTwoStackDRE.Controls.Add(Me.Label244)
        Me.TPTwoStackDRE.Controls.Add(Me.Label243)
        Me.TPTwoStackDRE.Controls.Add(Me.Label242)
        Me.TPTwoStackDRE.Controls.Add(Me.Label241)
        Me.TPTwoStackDRE.Controls.Add(Me.Label240)
        Me.TPTwoStackDRE.Controls.Add(Me.Label239)
        Me.TPTwoStackDRE.Controls.Add(Me.Label238)
        Me.TPTwoStackDRE.Controls.Add(Me.txtEmissRateAvgTwoStackDRE2)
        Me.TPTwoStackDRE.Controls.Add(Me.txtPollConcAvgTwoStackDRE1)
        Me.TPTwoStackDRE.Controls.Add(Me.txtDestructionEfficiencyTwoStackDRE)
        Me.TPTwoStackDRE.Controls.Add(Me.Label172)
        Me.TPTwoStackDRE.Controls.Add(Me.txtRunNumTwoStackDRE2C)
        Me.TPTwoStackDRE.Controls.Add(Me.btnClearTwoStackDRE2C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasMoistTwoStackDRE2C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtEmissRateTwoStackDRE2C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtPollConcTwoStackDRE2C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowDSCFMTwoStackDRE2C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowACFMTwoStackDRE2C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasMoistTwoStackDRE2B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtEmissRateTwoStackDRE2B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtPollConcTwoStackDRE2B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasTempTwoStackDRE2C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowDSCFMTwoStackDRE2B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowACFMTwoStackDRE2B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasTempTwoStackDRE2B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtRunNumTwoStackDRE2B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtEmissRateTwoStackDRE2A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtPollConcTwoStackDRE2A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasMoistTwoStackDRE2A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowDSCFMTwoStackDRE2A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowACFMTwoStackDRE2A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasTempTwoStackDRE2A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtRunNumTwoStackDRE2A)
        Me.TPTwoStackDRE.Controls.Add(Me.btnClearTwoStackDRE2A)
        Me.TPTwoStackDRE.Controls.Add(Me.btnClearTwoStackDRE2B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtRunNumTwoStackDRE1C)
        Me.TPTwoStackDRE.Controls.Add(Me.btnClearTwoStackDRE1C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasMoistTwoStackDRE1C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtEmissRateTwoStackDRE1C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtPollConcTwoStackDRE1C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowDSCFMTwoStackDRE1C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowACFMTwoStackDRE1C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasMoistTwoStackDRE1B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtStackTwoNameTwoStackDRE)
        Me.TPTwoStackDRE.Controls.Add(Me.Label174)
        Me.TPTwoStackDRE.Controls.Add(Me.Label176)
        Me.TPTwoStackDRE.Controls.Add(Me.txtEmissRateAvgTwoStackDRE1)
        Me.TPTwoStackDRE.Controls.Add(Me.txtPollConcAvgTwoStackDRE2)
        Me.TPTwoStackDRE.Controls.Add(Me.cboEmissRateUnitTwoStackDRE)
        Me.TPTwoStackDRE.Controls.Add(Me.cboPollConUnitTwoStackDRE)
        Me.TPTwoStackDRE.Controls.Add(Me.txtEmissRateTwoStackDRE1B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtPollConcTwoStackDRE1B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasTempTwoStackDRE1C)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowDSCFMTwoStackDRE1B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowACFMTwoStackDRE1B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasTempTwoStackDRE1B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtRunNumTwoStackDRE1B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtEmissRateTwoStackDRE1A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtPollConcTwoStackDRE1A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasTempTwoStackDRE1A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtRunNumTwoStackDRE1A)
        Me.TPTwoStackDRE.Controls.Add(Me.Label178)
        Me.TPTwoStackDRE.Controls.Add(Me.Label179)
        Me.TPTwoStackDRE.Controls.Add(Me.Label180)
        Me.TPTwoStackDRE.Controls.Add(Me.Label181)
        Me.TPTwoStackDRE.Controls.Add(Me.Label182)
        Me.TPTwoStackDRE.Controls.Add(Me.Label184)
        Me.TPTwoStackDRE.Controls.Add(Me.Label186)
        Me.TPTwoStackDRE.Controls.Add(Me.btnClearTwoStackDRE1A)
        Me.TPTwoStackDRE.Controls.Add(Me.btnClearTwoStackDRE1B)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasMoistTwoStackDRE1A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtStackOneNameTwoStackDRE)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowDSCFMTwoStackDRE1A)
        Me.TPTwoStackDRE.Controls.Add(Me.txtGasFlowACFMTwoStackDRE1A)
        Me.TPTwoStackDRE.Location = New System.Drawing.Point(4, 22)
        Me.TPTwoStackDRE.Name = "TPTwoStackDRE"
        Me.TPTwoStackDRE.Size = New System.Drawing.Size(782, 234)
        Me.TPTwoStackDRE.TabIndex = 0
        Me.TPTwoStackDRE.Text = "DRE"
        Me.TPTwoStackDRE.Visible = False
        '
        'Label246
        '
        Me.Label246.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label246.Location = New System.Drawing.Point(4, 209)
        Me.Label246.Name = "Label246"
        Me.Label246.Size = New System.Drawing.Size(200, 1)
        Me.Label246.TabIndex = 340
        '
        'Label245
        '
        Me.Label245.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label245.Location = New System.Drawing.Point(4, 187)
        Me.Label245.Name = "Label245"
        Me.Label245.Size = New System.Drawing.Size(560, 1)
        Me.Label245.TabIndex = 339
        '
        'Label244
        '
        Me.Label244.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label244.Location = New System.Drawing.Point(4, 165)
        Me.Label244.Name = "Label244"
        Me.Label244.Size = New System.Drawing.Size(762, 1)
        Me.Label244.TabIndex = 338
        '
        'Label243
        '
        Me.Label243.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label243.Location = New System.Drawing.Point(4, 143)
        Me.Label243.Name = "Label243"
        Me.Label243.Size = New System.Drawing.Size(560, 1)
        Me.Label243.TabIndex = 337
        '
        'Label242
        '
        Me.Label242.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label242.Location = New System.Drawing.Point(4, 121)
        Me.Label242.Name = "Label242"
        Me.Label242.Size = New System.Drawing.Size(560, 1)
        Me.Label242.TabIndex = 336
        '
        'Label241
        '
        Me.Label241.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label241.Location = New System.Drawing.Point(4, 99)
        Me.Label241.Name = "Label241"
        Me.Label241.Size = New System.Drawing.Size(560, 1)
        Me.Label241.TabIndex = 335
        '
        'Label240
        '
        Me.Label240.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label240.Location = New System.Drawing.Point(4, 77)
        Me.Label240.Name = "Label240"
        Me.Label240.Size = New System.Drawing.Size(560, 1)
        Me.Label240.TabIndex = 334
        '
        'Label239
        '
        Me.Label239.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label239.Location = New System.Drawing.Point(36, 53)
        Me.Label239.Name = "Label239"
        Me.Label239.Size = New System.Drawing.Size(532, 1)
        Me.Label239.TabIndex = 333
        '
        'Label238
        '
        Me.Label238.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label238.Location = New System.Drawing.Point(36, 29)
        Me.Label238.Name = "Label238"
        Me.Label238.Size = New System.Drawing.Size(532, 1)
        Me.Label238.TabIndex = 332
        '
        'txtEmissRateAvgTwoStackDRE2
        '
        Me.txtEmissRateAvgTwoStackDRE2.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateAvgTwoStackDRE2.Location = New System.Drawing.Point(704, 188)
        Me.txtEmissRateAvgTwoStackDRE2.MaxLength = 11
        Me.txtEmissRateAvgTwoStackDRE2.Name = "txtEmissRateAvgTwoStackDRE2"
        Me.txtEmissRateAvgTwoStackDRE2.ReadOnly = True
        Me.txtEmissRateAvgTwoStackDRE2.Size = New System.Drawing.Size(62, 20)
        Me.txtEmissRateAvgTwoStackDRE2.TabIndex = 486
        Me.txtEmissRateAvgTwoStackDRE2.TabStop = False
        '
        'txtPollConcAvgTwoStackDRE1
        '
        Me.txtPollConcAvgTwoStackDRE1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgTwoStackDRE1.Location = New System.Drawing.Point(704, 122)
        Me.txtPollConcAvgTwoStackDRE1.MaxLength = 11
        Me.txtPollConcAvgTwoStackDRE1.Name = "txtPollConcAvgTwoStackDRE1"
        Me.txtPollConcAvgTwoStackDRE1.ReadOnly = True
        Me.txtPollConcAvgTwoStackDRE1.Size = New System.Drawing.Size(62, 20)
        Me.txtPollConcAvgTwoStackDRE1.TabIndex = 476
        Me.txtPollConcAvgTwoStackDRE1.TabStop = False
        '
        'txtDestructionEfficiencyTwoStackDRE
        '
        Me.txtDestructionEfficiencyTwoStackDRE.BackColor = System.Drawing.SystemColors.Window
        Me.txtDestructionEfficiencyTwoStackDRE.Location = New System.Drawing.Point(136, 188)
        Me.txtDestructionEfficiencyTwoStackDRE.MaxLength = 11
        Me.txtDestructionEfficiencyTwoStackDRE.Name = "txtDestructionEfficiencyTwoStackDRE"
        Me.txtDestructionEfficiencyTwoStackDRE.ReadOnly = True
        Me.txtDestructionEfficiencyTwoStackDRE.Size = New System.Drawing.Size(68, 20)
        Me.txtDestructionEfficiencyTwoStackDRE.TabIndex = 487
        Me.txtDestructionEfficiencyTwoStackDRE.TabStop = False
        '
        'Label172
        '
        Me.Label172.AutoSize = True
        Me.Label172.Location = New System.Drawing.Point(4, 188)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(127, 13)
        Me.Label172.TabIndex = 326
        Me.Label172.Text = "Destruction Efficiency (%)"
        Me.Label172.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtRunNumTwoStackDRE2C
        '
        Me.txtRunNumTwoStackDRE2C.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackDRE2C.Location = New System.Drawing.Point(502, 32)
        Me.txtRunNumTwoStackDRE2C.MaxLength = 3
        Me.txtRunNumTwoStackDRE2C.Name = "txtRunNumTwoStackDRE2C"
        Me.txtRunNumTwoStackDRE2C.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackDRE2C.TabIndex = 444
        '
        'btnClearTwoStackDRE2C
        '
        Me.btnClearTwoStackDRE2C.Location = New System.Drawing.Point(527, 32)
        Me.btnClearTwoStackDRE2C.Name = "btnClearTwoStackDRE2C"
        Me.btnClearTwoStackDRE2C.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackDRE2C.TabIndex = 325
        Me.btnClearTwoStackDRE2C.Text = "Clear"
        '
        'txtGasMoistTwoStackDRE2C
        '
        Me.txtGasMoistTwoStackDRE2C.Location = New System.Drawing.Point(496, 78)
        Me.txtGasMoistTwoStackDRE2C.MaxLength = 11
        Me.txtGasMoistTwoStackDRE2C.Name = "txtGasMoistTwoStackDRE2C"
        Me.txtGasMoistTwoStackDRE2C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackDRE2C.TabIndex = 456
        '
        'txtEmissRateTwoStackDRE2C
        '
        Me.txtEmissRateTwoStackDRE2C.Location = New System.Drawing.Point(496, 166)
        Me.txtEmissRateTwoStackDRE2C.MaxLength = 11
        Me.txtEmissRateTwoStackDRE2C.Name = "txtEmissRateTwoStackDRE2C"
        Me.txtEmissRateTwoStackDRE2C.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackDRE2C.TabIndex = 483
        '
        'txtPollConcTwoStackDRE2C
        '
        Me.txtPollConcTwoStackDRE2C.Location = New System.Drawing.Point(496, 144)
        Me.txtPollConcTwoStackDRE2C.MaxLength = 11
        Me.txtPollConcTwoStackDRE2C.Name = "txtPollConcTwoStackDRE2C"
        Me.txtPollConcTwoStackDRE2C.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackDRE2C.TabIndex = 474
        '
        'txtGasFlowDSCFMTwoStackDRE2C
        '
        Me.txtGasFlowDSCFMTwoStackDRE2C.Location = New System.Drawing.Point(496, 122)
        Me.txtGasFlowDSCFMTwoStackDRE2C.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackDRE2C.Name = "txtGasFlowDSCFMTwoStackDRE2C"
        Me.txtGasFlowDSCFMTwoStackDRE2C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackDRE2C.TabIndex = 468
        '
        'txtGasFlowACFMTwoStackDRE2C
        '
        Me.txtGasFlowACFMTwoStackDRE2C.Location = New System.Drawing.Point(496, 100)
        Me.txtGasFlowACFMTwoStackDRE2C.MaxLength = 11
        Me.txtGasFlowACFMTwoStackDRE2C.Name = "txtGasFlowACFMTwoStackDRE2C"
        Me.txtGasFlowACFMTwoStackDRE2C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackDRE2C.TabIndex = 462
        '
        'txtGasMoistTwoStackDRE2B
        '
        Me.txtGasMoistTwoStackDRE2B.Location = New System.Drawing.Point(424, 78)
        Me.txtGasMoistTwoStackDRE2B.MaxLength = 11
        Me.txtGasMoistTwoStackDRE2B.Name = "txtGasMoistTwoStackDRE2B"
        Me.txtGasMoistTwoStackDRE2B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackDRE2B.TabIndex = 455
        '
        'txtEmissRateTwoStackDRE2B
        '
        Me.txtEmissRateTwoStackDRE2B.Location = New System.Drawing.Point(424, 166)
        Me.txtEmissRateTwoStackDRE2B.MaxLength = 11
        Me.txtEmissRateTwoStackDRE2B.Name = "txtEmissRateTwoStackDRE2B"
        Me.txtEmissRateTwoStackDRE2B.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackDRE2B.TabIndex = 482
        '
        'txtPollConcTwoStackDRE2B
        '
        Me.txtPollConcTwoStackDRE2B.Location = New System.Drawing.Point(424, 144)
        Me.txtPollConcTwoStackDRE2B.MaxLength = 11
        Me.txtPollConcTwoStackDRE2B.Name = "txtPollConcTwoStackDRE2B"
        Me.txtPollConcTwoStackDRE2B.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackDRE2B.TabIndex = 473
        '
        'txtGasTempTwoStackDRE2C
        '
        Me.txtGasTempTwoStackDRE2C.Location = New System.Drawing.Point(496, 56)
        Me.txtGasTempTwoStackDRE2C.MaxLength = 11
        Me.txtGasTempTwoStackDRE2C.Name = "txtGasTempTwoStackDRE2C"
        Me.txtGasTempTwoStackDRE2C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackDRE2C.TabIndex = 450
        '
        'txtGasFlowDSCFMTwoStackDRE2B
        '
        Me.txtGasFlowDSCFMTwoStackDRE2B.Location = New System.Drawing.Point(424, 122)
        Me.txtGasFlowDSCFMTwoStackDRE2B.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackDRE2B.Name = "txtGasFlowDSCFMTwoStackDRE2B"
        Me.txtGasFlowDSCFMTwoStackDRE2B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackDRE2B.TabIndex = 467
        '
        'txtGasFlowACFMTwoStackDRE2B
        '
        Me.txtGasFlowACFMTwoStackDRE2B.Location = New System.Drawing.Point(424, 100)
        Me.txtGasFlowACFMTwoStackDRE2B.MaxLength = 11
        Me.txtGasFlowACFMTwoStackDRE2B.Name = "txtGasFlowACFMTwoStackDRE2B"
        Me.txtGasFlowACFMTwoStackDRE2B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackDRE2B.TabIndex = 461
        '
        'txtGasTempTwoStackDRE2B
        '
        Me.txtGasTempTwoStackDRE2B.Location = New System.Drawing.Point(424, 56)
        Me.txtGasTempTwoStackDRE2B.MaxLength = 11
        Me.txtGasTempTwoStackDRE2B.Name = "txtGasTempTwoStackDRE2B"
        Me.txtGasTempTwoStackDRE2B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackDRE2B.TabIndex = 449
        '
        'txtRunNumTwoStackDRE2B
        '
        Me.txtRunNumTwoStackDRE2B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackDRE2B.Location = New System.Drawing.Point(430, 32)
        Me.txtRunNumTwoStackDRE2B.MaxLength = 3
        Me.txtRunNumTwoStackDRE2B.Name = "txtRunNumTwoStackDRE2B"
        Me.txtRunNumTwoStackDRE2B.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackDRE2B.TabIndex = 443
        '
        'txtEmissRateTwoStackDRE2A
        '
        Me.txtEmissRateTwoStackDRE2A.Location = New System.Drawing.Point(352, 166)
        Me.txtEmissRateTwoStackDRE2A.MaxLength = 11
        Me.txtEmissRateTwoStackDRE2A.Name = "txtEmissRateTwoStackDRE2A"
        Me.txtEmissRateTwoStackDRE2A.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackDRE2A.TabIndex = 481
        '
        'txtPollConcTwoStackDRE2A
        '
        Me.txtPollConcTwoStackDRE2A.Location = New System.Drawing.Point(352, 144)
        Me.txtPollConcTwoStackDRE2A.MaxLength = 11
        Me.txtPollConcTwoStackDRE2A.Name = "txtPollConcTwoStackDRE2A"
        Me.txtPollConcTwoStackDRE2A.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackDRE2A.TabIndex = 472
        '
        'txtGasMoistTwoStackDRE2A
        '
        Me.txtGasMoistTwoStackDRE2A.Location = New System.Drawing.Point(352, 78)
        Me.txtGasMoistTwoStackDRE2A.MaxLength = 11
        Me.txtGasMoistTwoStackDRE2A.Name = "txtGasMoistTwoStackDRE2A"
        Me.txtGasMoistTwoStackDRE2A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackDRE2A.TabIndex = 454
        '
        'txtGasFlowDSCFMTwoStackDRE2A
        '
        Me.txtGasFlowDSCFMTwoStackDRE2A.Location = New System.Drawing.Point(352, 122)
        Me.txtGasFlowDSCFMTwoStackDRE2A.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackDRE2A.Name = "txtGasFlowDSCFMTwoStackDRE2A"
        Me.txtGasFlowDSCFMTwoStackDRE2A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackDRE2A.TabIndex = 466
        '
        'txtGasFlowACFMTwoStackDRE2A
        '
        Me.txtGasFlowACFMTwoStackDRE2A.Location = New System.Drawing.Point(352, 100)
        Me.txtGasFlowACFMTwoStackDRE2A.MaxLength = 11
        Me.txtGasFlowACFMTwoStackDRE2A.Name = "txtGasFlowACFMTwoStackDRE2A"
        Me.txtGasFlowACFMTwoStackDRE2A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackDRE2A.TabIndex = 460
        '
        'txtGasTempTwoStackDRE2A
        '
        Me.txtGasTempTwoStackDRE2A.Location = New System.Drawing.Point(352, 56)
        Me.txtGasTempTwoStackDRE2A.MaxLength = 11
        Me.txtGasTempTwoStackDRE2A.Name = "txtGasTempTwoStackDRE2A"
        Me.txtGasTempTwoStackDRE2A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackDRE2A.TabIndex = 448
        '
        'txtRunNumTwoStackDRE2A
        '
        Me.txtRunNumTwoStackDRE2A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackDRE2A.Location = New System.Drawing.Point(358, 32)
        Me.txtRunNumTwoStackDRE2A.MaxLength = 3
        Me.txtRunNumTwoStackDRE2A.Name = "txtRunNumTwoStackDRE2A"
        Me.txtRunNumTwoStackDRE2A.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackDRE2A.TabIndex = 442
        '
        'btnClearTwoStackDRE2A
        '
        Me.btnClearTwoStackDRE2A.Location = New System.Drawing.Point(383, 32)
        Me.btnClearTwoStackDRE2A.Name = "btnClearTwoStackDRE2A"
        Me.btnClearTwoStackDRE2A.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackDRE2A.TabIndex = 317
        Me.btnClearTwoStackDRE2A.Text = "Clear"
        '
        'btnClearTwoStackDRE2B
        '
        Me.btnClearTwoStackDRE2B.Location = New System.Drawing.Point(455, 32)
        Me.btnClearTwoStackDRE2B.Name = "btnClearTwoStackDRE2B"
        Me.btnClearTwoStackDRE2B.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackDRE2B.TabIndex = 316
        Me.btnClearTwoStackDRE2B.Text = "Clear"
        '
        'txtRunNumTwoStackDRE1C
        '
        Me.txtRunNumTwoStackDRE1C.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackDRE1C.Location = New System.Drawing.Point(288, 32)
        Me.txtRunNumTwoStackDRE1C.MaxLength = 3
        Me.txtRunNumTwoStackDRE1C.Name = "txtRunNumTwoStackDRE1C"
        Me.txtRunNumTwoStackDRE1C.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackDRE1C.TabIndex = 441
        '
        'btnClearTwoStackDRE1C
        '
        Me.btnClearTwoStackDRE1C.Location = New System.Drawing.Point(313, 32)
        Me.btnClearTwoStackDRE1C.Name = "btnClearTwoStackDRE1C"
        Me.btnClearTwoStackDRE1C.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackDRE1C.TabIndex = 301
        Me.btnClearTwoStackDRE1C.Text = "Clear"
        '
        'txtGasMoistTwoStackDRE1C
        '
        Me.txtGasMoistTwoStackDRE1C.Location = New System.Drawing.Point(282, 78)
        Me.txtGasMoistTwoStackDRE1C.MaxLength = 11
        Me.txtGasMoistTwoStackDRE1C.Name = "txtGasMoistTwoStackDRE1C"
        Me.txtGasMoistTwoStackDRE1C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackDRE1C.TabIndex = 453
        '
        'txtEmissRateTwoStackDRE1C
        '
        Me.txtEmissRateTwoStackDRE1C.Location = New System.Drawing.Point(282, 166)
        Me.txtEmissRateTwoStackDRE1C.MaxLength = 11
        Me.txtEmissRateTwoStackDRE1C.Name = "txtEmissRateTwoStackDRE1C"
        Me.txtEmissRateTwoStackDRE1C.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackDRE1C.TabIndex = 480
        '
        'txtPollConcTwoStackDRE1C
        '
        Me.txtPollConcTwoStackDRE1C.Location = New System.Drawing.Point(282, 144)
        Me.txtPollConcTwoStackDRE1C.MaxLength = 11
        Me.txtPollConcTwoStackDRE1C.Name = "txtPollConcTwoStackDRE1C"
        Me.txtPollConcTwoStackDRE1C.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackDRE1C.TabIndex = 471
        '
        'txtGasFlowDSCFMTwoStackDRE1C
        '
        Me.txtGasFlowDSCFMTwoStackDRE1C.Location = New System.Drawing.Point(282, 122)
        Me.txtGasFlowDSCFMTwoStackDRE1C.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackDRE1C.Name = "txtGasFlowDSCFMTwoStackDRE1C"
        Me.txtGasFlowDSCFMTwoStackDRE1C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackDRE1C.TabIndex = 465
        '
        'txtGasFlowACFMTwoStackDRE1C
        '
        Me.txtGasFlowACFMTwoStackDRE1C.Location = New System.Drawing.Point(282, 100)
        Me.txtGasFlowACFMTwoStackDRE1C.MaxLength = 11
        Me.txtGasFlowACFMTwoStackDRE1C.Name = "txtGasFlowACFMTwoStackDRE1C"
        Me.txtGasFlowACFMTwoStackDRE1C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackDRE1C.TabIndex = 459
        '
        'txtGasMoistTwoStackDRE1B
        '
        Me.txtGasMoistTwoStackDRE1B.Location = New System.Drawing.Point(209, 78)
        Me.txtGasMoistTwoStackDRE1B.MaxLength = 11
        Me.txtGasMoistTwoStackDRE1B.Name = "txtGasMoistTwoStackDRE1B"
        Me.txtGasMoistTwoStackDRE1B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackDRE1B.TabIndex = 452
        '
        'txtStackTwoNameTwoStackDRE
        '
        Me.txtStackTwoNameTwoStackDRE.BackColor = System.Drawing.SystemColors.Window
        Me.txtStackTwoNameTwoStackDRE.Location = New System.Drawing.Point(352, 8)
        Me.txtStackTwoNameTwoStackDRE.MaxLength = 30
        Me.txtStackTwoNameTwoStackDRE.Name = "txtStackTwoNameTwoStackDRE"
        Me.txtStackTwoNameTwoStackDRE.Size = New System.Drawing.Size(216, 20)
        Me.txtStackTwoNameTwoStackDRE.TabIndex = 438
        '
        'Label174
        '
        Me.Label174.AutoSize = True
        Me.Label174.Location = New System.Drawing.Point(36, 8)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(69, 13)
        Me.Label174.TabIndex = 291
        Me.Label174.Text = "Stack Name:"
        Me.Label174.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label176
        '
        Me.Label176.AutoSize = True
        Me.Label176.Location = New System.Drawing.Point(704, 100)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(65, 13)
        Me.Label176.TabIndex = 287
        Me.Label176.Text = "AVERAGES"
        Me.Label176.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txtEmissRateAvgTwoStackDRE1
        '
        Me.txtEmissRateAvgTwoStackDRE1.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmissRateAvgTwoStackDRE1.Location = New System.Drawing.Point(704, 166)
        Me.txtEmissRateAvgTwoStackDRE1.MaxLength = 11
        Me.txtEmissRateAvgTwoStackDRE1.Name = "txtEmissRateAvgTwoStackDRE1"
        Me.txtEmissRateAvgTwoStackDRE1.ReadOnly = True
        Me.txtEmissRateAvgTwoStackDRE1.Size = New System.Drawing.Size(62, 20)
        Me.txtEmissRateAvgTwoStackDRE1.TabIndex = 485
        Me.txtEmissRateAvgTwoStackDRE1.TabStop = False
        '
        'txtPollConcAvgTwoStackDRE2
        '
        Me.txtPollConcAvgTwoStackDRE2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPollConcAvgTwoStackDRE2.Location = New System.Drawing.Point(704, 144)
        Me.txtPollConcAvgTwoStackDRE2.MaxLength = 11
        Me.txtPollConcAvgTwoStackDRE2.Name = "txtPollConcAvgTwoStackDRE2"
        Me.txtPollConcAvgTwoStackDRE2.ReadOnly = True
        Me.txtPollConcAvgTwoStackDRE2.Size = New System.Drawing.Size(62, 20)
        Me.txtPollConcAvgTwoStackDRE2.TabIndex = 477
        Me.txtPollConcAvgTwoStackDRE2.TabStop = False
        '
        'cboEmissRateUnitTwoStackDRE
        '
        Me.cboEmissRateUnitTwoStackDRE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEmissRateUnitTwoStackDRE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEmissRateUnitTwoStackDRE.Location = New System.Drawing.Point(574, 165)
        Me.cboEmissRateUnitTwoStackDRE.Name = "cboEmissRateUnitTwoStackDRE"
        Me.cboEmissRateUnitTwoStackDRE.Size = New System.Drawing.Size(120, 21)
        Me.cboEmissRateUnitTwoStackDRE.TabIndex = 484
        Me.cboEmissRateUnitTwoStackDRE.Text = "UNITS"
        '
        'cboPollConUnitTwoStackDRE
        '
        Me.cboPollConUnitTwoStackDRE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPollConUnitTwoStackDRE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPollConUnitTwoStackDRE.Location = New System.Drawing.Point(574, 143)
        Me.cboPollConUnitTwoStackDRE.Name = "cboPollConUnitTwoStackDRE"
        Me.cboPollConUnitTwoStackDRE.Size = New System.Drawing.Size(120, 21)
        Me.cboPollConUnitTwoStackDRE.TabIndex = 475
        Me.cboPollConUnitTwoStackDRE.Text = "UNITS"
        '
        'txtEmissRateTwoStackDRE1B
        '
        Me.txtEmissRateTwoStackDRE1B.Location = New System.Drawing.Point(209, 166)
        Me.txtEmissRateTwoStackDRE1B.MaxLength = 11
        Me.txtEmissRateTwoStackDRE1B.Name = "txtEmissRateTwoStackDRE1B"
        Me.txtEmissRateTwoStackDRE1B.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackDRE1B.TabIndex = 479
        '
        'txtPollConcTwoStackDRE1B
        '
        Me.txtPollConcTwoStackDRE1B.Location = New System.Drawing.Point(209, 144)
        Me.txtPollConcTwoStackDRE1B.MaxLength = 11
        Me.txtPollConcTwoStackDRE1B.Name = "txtPollConcTwoStackDRE1B"
        Me.txtPollConcTwoStackDRE1B.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackDRE1B.TabIndex = 470
        '
        'txtGasTempTwoStackDRE1C
        '
        Me.txtGasTempTwoStackDRE1C.Location = New System.Drawing.Point(282, 56)
        Me.txtGasTempTwoStackDRE1C.MaxLength = 11
        Me.txtGasTempTwoStackDRE1C.Name = "txtGasTempTwoStackDRE1C"
        Me.txtGasTempTwoStackDRE1C.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackDRE1C.TabIndex = 447
        '
        'txtGasFlowDSCFMTwoStackDRE1B
        '
        Me.txtGasFlowDSCFMTwoStackDRE1B.Location = New System.Drawing.Point(209, 122)
        Me.txtGasFlowDSCFMTwoStackDRE1B.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackDRE1B.Name = "txtGasFlowDSCFMTwoStackDRE1B"
        Me.txtGasFlowDSCFMTwoStackDRE1B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackDRE1B.TabIndex = 464
        '
        'txtGasFlowACFMTwoStackDRE1B
        '
        Me.txtGasFlowACFMTwoStackDRE1B.Location = New System.Drawing.Point(209, 100)
        Me.txtGasFlowACFMTwoStackDRE1B.MaxLength = 11
        Me.txtGasFlowACFMTwoStackDRE1B.Name = "txtGasFlowACFMTwoStackDRE1B"
        Me.txtGasFlowACFMTwoStackDRE1B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackDRE1B.TabIndex = 458
        '
        'txtGasTempTwoStackDRE1B
        '
        Me.txtGasTempTwoStackDRE1B.Location = New System.Drawing.Point(209, 56)
        Me.txtGasTempTwoStackDRE1B.MaxLength = 11
        Me.txtGasTempTwoStackDRE1B.Name = "txtGasTempTwoStackDRE1B"
        Me.txtGasTempTwoStackDRE1B.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackDRE1B.TabIndex = 446
        '
        'txtRunNumTwoStackDRE1B
        '
        Me.txtRunNumTwoStackDRE1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackDRE1B.Location = New System.Drawing.Point(215, 32)
        Me.txtRunNumTwoStackDRE1B.MaxLength = 3
        Me.txtRunNumTwoStackDRE1B.Name = "txtRunNumTwoStackDRE1B"
        Me.txtRunNumTwoStackDRE1B.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackDRE1B.TabIndex = 440
        '
        'txtEmissRateTwoStackDRE1A
        '
        Me.txtEmissRateTwoStackDRE1A.Location = New System.Drawing.Point(136, 166)
        Me.txtEmissRateTwoStackDRE1A.MaxLength = 11
        Me.txtEmissRateTwoStackDRE1A.Name = "txtEmissRateTwoStackDRE1A"
        Me.txtEmissRateTwoStackDRE1A.Size = New System.Drawing.Size(68, 20)
        Me.txtEmissRateTwoStackDRE1A.TabIndex = 478
        '
        'txtPollConcTwoStackDRE1A
        '
        Me.txtPollConcTwoStackDRE1A.Location = New System.Drawing.Point(136, 144)
        Me.txtPollConcTwoStackDRE1A.MaxLength = 11
        Me.txtPollConcTwoStackDRE1A.Name = "txtPollConcTwoStackDRE1A"
        Me.txtPollConcTwoStackDRE1A.Size = New System.Drawing.Size(68, 20)
        Me.txtPollConcTwoStackDRE1A.TabIndex = 469
        '
        'txtGasTempTwoStackDRE1A
        '
        Me.txtGasTempTwoStackDRE1A.Location = New System.Drawing.Point(136, 56)
        Me.txtGasTempTwoStackDRE1A.MaxLength = 11
        Me.txtGasTempTwoStackDRE1A.Name = "txtGasTempTwoStackDRE1A"
        Me.txtGasTempTwoStackDRE1A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasTempTwoStackDRE1A.TabIndex = 445
        '
        'txtRunNumTwoStackDRE1A
        '
        Me.txtRunNumTwoStackDRE1A.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunNumTwoStackDRE1A.Location = New System.Drawing.Point(142, 32)
        Me.txtRunNumTwoStackDRE1A.MaxLength = 3
        Me.txtRunNumTwoStackDRE1A.Name = "txtRunNumTwoStackDRE1A"
        Me.txtRunNumTwoStackDRE1A.Size = New System.Drawing.Size(25, 20)
        Me.txtRunNumTwoStackDRE1A.TabIndex = 439
        '
        'Label178
        '
        Me.Label178.AutoSize = True
        Me.Label178.Location = New System.Drawing.Point(4, 78)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(86, 13)
        Me.Label178.TabIndex = 282
        Me.Label178.Text = "Gas Moisture (%)"
        Me.Label178.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label179
        '
        Me.Label179.AutoSize = True
        Me.Label179.Location = New System.Drawing.Point(4, 100)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(115, 13)
        Me.Label179.TabIndex = 281
        Me.Label179.Text = "Gas Flow Rate (ACFM)"
        Me.Label179.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label180
        '
        Me.Label180.AutoSize = True
        Me.Label180.Location = New System.Drawing.Point(4, 122)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(123, 13)
        Me.Label180.TabIndex = 280
        Me.Label180.Text = "Gas Flow Rate (DSCFM)"
        Me.Label180.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label181
        '
        Me.Label181.AutoSize = True
        Me.Label181.Location = New System.Drawing.Point(4, 144)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(120, 13)
        Me.Label181.TabIndex = 279
        Me.Label181.Text = "Pollutant Concentration:"
        Me.Label181.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label182
        '
        Me.Label182.AutoSize = True
        Me.Label182.Location = New System.Drawing.Point(4, 166)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(77, 13)
        Me.Label182.TabIndex = 278
        Me.Label182.Text = "Emission Rate:"
        Me.Label182.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label184
        '
        Me.Label184.AutoSize = True
        Me.Label184.Location = New System.Drawing.Point(4, 56)
        Me.Label184.Name = "Label184"
        Me.Label184.Size = New System.Drawing.Size(107, 13)
        Me.Label184.TabIndex = 277
        Me.Label184.Text = "Gas Temperature (F):"
        Me.Label184.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label186
        '
        Me.Label186.AutoSize = True
        Me.Label186.Location = New System.Drawing.Point(36, 32)
        Me.Label186.Name = "Label186"
        Me.Label186.Size = New System.Drawing.Size(61, 13)
        Me.Label186.TabIndex = 275
        Me.Label186.Text = "Test Run #"
        Me.Label186.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'btnClearTwoStackDRE1A
        '
        Me.btnClearTwoStackDRE1A.Location = New System.Drawing.Point(167, 32)
        Me.btnClearTwoStackDRE1A.Name = "btnClearTwoStackDRE1A"
        Me.btnClearTwoStackDRE1A.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackDRE1A.TabIndex = 284
        Me.btnClearTwoStackDRE1A.Text = "Clear"
        '
        'btnClearTwoStackDRE1B
        '
        Me.btnClearTwoStackDRE1B.Location = New System.Drawing.Point(240, 32)
        Me.btnClearTwoStackDRE1B.Name = "btnClearTwoStackDRE1B"
        Me.btnClearTwoStackDRE1B.Size = New System.Drawing.Size(40, 20)
        Me.btnClearTwoStackDRE1B.TabIndex = 283
        Me.btnClearTwoStackDRE1B.Text = "Clear"
        '
        'txtGasMoistTwoStackDRE1A
        '
        Me.txtGasMoistTwoStackDRE1A.Location = New System.Drawing.Point(136, 78)
        Me.txtGasMoistTwoStackDRE1A.MaxLength = 11
        Me.txtGasMoistTwoStackDRE1A.Name = "txtGasMoistTwoStackDRE1A"
        Me.txtGasMoistTwoStackDRE1A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasMoistTwoStackDRE1A.TabIndex = 451
        '
        'txtStackOneNameTwoStackDRE
        '
        Me.txtStackOneNameTwoStackDRE.BackColor = System.Drawing.SystemColors.Window
        Me.txtStackOneNameTwoStackDRE.Location = New System.Drawing.Point(136, 8)
        Me.txtStackOneNameTwoStackDRE.MaxLength = 30
        Me.txtStackOneNameTwoStackDRE.Name = "txtStackOneNameTwoStackDRE"
        Me.txtStackOneNameTwoStackDRE.Size = New System.Drawing.Size(216, 20)
        Me.txtStackOneNameTwoStackDRE.TabIndex = 437
        '
        'txtGasFlowDSCFMTwoStackDRE1A
        '
        Me.txtGasFlowDSCFMTwoStackDRE1A.Location = New System.Drawing.Point(136, 122)
        Me.txtGasFlowDSCFMTwoStackDRE1A.MaxLength = 11
        Me.txtGasFlowDSCFMTwoStackDRE1A.Name = "txtGasFlowDSCFMTwoStackDRE1A"
        Me.txtGasFlowDSCFMTwoStackDRE1A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowDSCFMTwoStackDRE1A.TabIndex = 463
        '
        'txtGasFlowACFMTwoStackDRE1A
        '
        Me.txtGasFlowACFMTwoStackDRE1A.Location = New System.Drawing.Point(136, 100)
        Me.txtGasFlowACFMTwoStackDRE1A.MaxLength = 11
        Me.txtGasFlowACFMTwoStackDRE1A.Name = "txtGasFlowACFMTwoStackDRE1A"
        Me.txtGasFlowACFMTwoStackDRE1A.Size = New System.Drawing.Size(68, 20)
        Me.txtGasFlowACFMTwoStackDRE1A.TabIndex = 457
        '
        'Label188
        '
        Me.Label188.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label188.Location = New System.Drawing.Point(-8, 133)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(800, 1)
        Me.Label188.TabIndex = 334
        Me.Label188.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtControlEquipmentOperatingDataTwoStack
        '
        Me.txtControlEquipmentOperatingDataTwoStack.Location = New System.Drawing.Point(137, 81)
        Me.txtControlEquipmentOperatingDataTwoStack.MaxLength = 4000
        Me.txtControlEquipmentOperatingDataTwoStack.Multiline = True
        Me.txtControlEquipmentOperatingDataTwoStack.Name = "txtControlEquipmentOperatingDataTwoStack"
        Me.txtControlEquipmentOperatingDataTwoStack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlEquipmentOperatingDataTwoStack.Size = New System.Drawing.Size(616, 40)
        Me.txtControlEquipmentOperatingDataTwoStack.TabIndex = 381
        '
        'txtApplicableRegulationTwoStack
        '
        Me.txtApplicableRegulationTwoStack.AcceptsReturn = True
        Me.txtApplicableRegulationTwoStack.Location = New System.Drawing.Point(137, 49)
        Me.txtApplicableRegulationTwoStack.MaxLength = 200
        Me.txtApplicableRegulationTwoStack.Multiline = True
        Me.txtApplicableRegulationTwoStack.Name = "txtApplicableRegulationTwoStack"
        Me.txtApplicableRegulationTwoStack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationTwoStack.Size = New System.Drawing.Size(616, 30)
        Me.txtApplicableRegulationTwoStack.TabIndex = 380
        '
        'txtOperatingCapacityTwoStack
        '
        Me.txtOperatingCapacityTwoStack.Location = New System.Drawing.Point(497, 5)
        Me.txtOperatingCapacityTwoStack.MaxLength = 11
        Me.txtOperatingCapacityTwoStack.Name = "txtOperatingCapacityTwoStack"
        Me.txtOperatingCapacityTwoStack.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityTwoStack.TabIndex = 372
        '
        'Label189
        '
        Me.Label189.AutoSize = True
        Me.Label189.Location = New System.Drawing.Point(393, 5)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(100, 13)
        Me.Label189.TabIndex = 333
        Me.Label189.Text = "Operating Capacity:"
        Me.Label189.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAllowableEmissionRate2TwoStack
        '
        Me.txtAllowableEmissionRate2TwoStack.Location = New System.Drawing.Point(345, 27)
        Me.txtAllowableEmissionRate2TwoStack.MaxLength = 11
        Me.txtAllowableEmissionRate2TwoStack.Name = "txtAllowableEmissionRate2TwoStack"
        Me.txtAllowableEmissionRate2TwoStack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate2TwoStack.TabIndex = 376
        '
        'txtAllowableEmissionRate3TwoStack
        '
        Me.txtAllowableEmissionRate3TwoStack.Location = New System.Drawing.Point(553, 27)
        Me.txtAllowableEmissionRate3TwoStack.MaxLength = 11
        Me.txtAllowableEmissionRate3TwoStack.Name = "txtAllowableEmissionRate3TwoStack"
        Me.txtAllowableEmissionRate3TwoStack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate3TwoStack.TabIndex = 378
        '
        'txtAllowableEmissionRate1TwoStack
        '
        Me.txtAllowableEmissionRate1TwoStack.Location = New System.Drawing.Point(137, 27)
        Me.txtAllowableEmissionRate1TwoStack.MaxLength = 11
        Me.txtAllowableEmissionRate1TwoStack.Name = "txtAllowableEmissionRate1TwoStack"
        Me.txtAllowableEmissionRate1TwoStack.Size = New System.Drawing.Size(88, 20)
        Me.txtAllowableEmissionRate1TwoStack.TabIndex = 374
        '
        'txtMaximumExpectedOperatingCapacityTwoStack
        '
        Me.txtMaximumExpectedOperatingCapacityTwoStack.Location = New System.Drawing.Point(137, 5)
        Me.txtMaximumExpectedOperatingCapacityTwoStack.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityTwoStack.Name = "txtMaximumExpectedOperatingCapacityTwoStack"
        Me.txtMaximumExpectedOperatingCapacityTwoStack.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityTwoStack.TabIndex = 370
        '
        'cboOperatingCapacityUnitsTwoStack
        '
        Me.cboOperatingCapacityUnitsTwoStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsTwoStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsTwoStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsTwoStack.Location = New System.Drawing.Point(585, 5)
        Me.cboOperatingCapacityUnitsTwoStack.Name = "cboOperatingCapacityUnitsTwoStack"
        Me.cboOperatingCapacityUnitsTwoStack.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsTwoStack.TabIndex = 373
        '
        'cboAllowableEmissionRateUnits2TwoStack
        '
        Me.cboAllowableEmissionRateUnits2TwoStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits2TwoStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits2TwoStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits2TwoStack.Location = New System.Drawing.Point(433, 27)
        Me.cboAllowableEmissionRateUnits2TwoStack.Name = "cboAllowableEmissionRateUnits2TwoStack"
        Me.cboAllowableEmissionRateUnits2TwoStack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits2TwoStack.TabIndex = 377
        '
        'cboAllowableEmissionRateUnits3TwoStack
        '
        Me.cboAllowableEmissionRateUnits3TwoStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits3TwoStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits3TwoStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits3TwoStack.Location = New System.Drawing.Point(641, 27)
        Me.cboAllowableEmissionRateUnits3TwoStack.Name = "cboAllowableEmissionRateUnits3TwoStack"
        Me.cboAllowableEmissionRateUnits3TwoStack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits3TwoStack.TabIndex = 379
        '
        'cboAllowableEmissionRateUnits1TwoStack
        '
        Me.cboAllowableEmissionRateUnits1TwoStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAllowableEmissionRateUnits1TwoStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAllowableEmissionRateUnits1TwoStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAllowableEmissionRateUnits1TwoStack.Location = New System.Drawing.Point(225, 27)
        Me.cboAllowableEmissionRateUnits1TwoStack.Name = "cboAllowableEmissionRateUnits1TwoStack"
        Me.cboAllowableEmissionRateUnits1TwoStack.Size = New System.Drawing.Size(112, 21)
        Me.cboAllowableEmissionRateUnits1TwoStack.TabIndex = 375
        '
        'cboMaximumExpectedOperatingCapacityUnitsTwoStack
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack.Location = New System.Drawing.Point(225, 5)
        Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack.Name = "cboMaximumExpectedOperatingCapacityUnitsTwoStack"
        Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsTwoStack.TabIndex = 371
        '
        'TPMethodTwentyTwo
        '
        Me.TPMethodTwentyTwo.AutoScroll = True
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label296)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label214)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label295)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label294)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label293)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label215)
        Me.TPMethodTwentyTwo.Controls.Add(Me.txtOtherInformationMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label216)
        Me.TPMethodTwentyTwo.Controls.Add(Me.txtTestDurationMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.txtAccumulatedEmissionMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label217)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label218)
        Me.TPMethodTwentyTwo.Controls.Add(Me.txtApplicableRegulationMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.txtOperatingCapacityMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label219)
        Me.TPMethodTwentyTwo.Controls.Add(Me.txtAllowableEmissionRateMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.txtMaximumExpectedOperatingCapacityMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label220)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label221)
        Me.TPMethodTwentyTwo.Controls.Add(Me.cboOperatingCapacityUnitsMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.cboMaximumExpectedOperatingCapacityUnitsMethod22)
        Me.TPMethodTwentyTwo.Controls.Add(Me.Label222)
        Me.TPMethodTwentyTwo.Location = New System.Drawing.Point(4, 22)
        Me.TPMethodTwentyTwo.Name = "TPMethodTwentyTwo"
        Me.TPMethodTwentyTwo.Size = New System.Drawing.Size(782, 288)
        Me.TPMethodTwentyTwo.TabIndex = 10
        Me.TPMethodTwentyTwo.Text = "Method 22"
        Me.TPMethodTwentyTwo.UseVisualStyleBackColor = True
        '
        'Label296
        '
        Me.Label296.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label296.Location = New System.Drawing.Point(86, 126)
        Me.Label296.Name = "Label296"
        Me.Label296.Size = New System.Drawing.Size(243, 1)
        Me.Label296.TabIndex = 405
        Me.Label296.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label214
        '
        Me.Label214.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label214.Location = New System.Drawing.Point(50, 104)
        Me.Label214.Name = "Label214"
        Me.Label214.Size = New System.Drawing.Size(227, 1)
        Me.Label214.TabIndex = 404
        Me.Label214.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label295
        '
        Me.Label295.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label295.Location = New System.Drawing.Point(5, 83)
        Me.Label295.Name = "Label295"
        Me.Label295.Size = New System.Drawing.Size(748, 1)
        Me.Label295.TabIndex = 403
        Me.Label295.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label294
        '
        Me.Label294.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label294.Location = New System.Drawing.Point(5, 51)
        Me.Label294.Name = "Label294"
        Me.Label294.Size = New System.Drawing.Size(748, 1)
        Me.Label294.TabIndex = 402
        Me.Label294.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label293
        '
        Me.Label293.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label293.Location = New System.Drawing.Point(109, 29)
        Me.Label293.Name = "Label293"
        Me.Label293.Size = New System.Drawing.Size(588, 1)
        Me.Label293.TabIndex = 401
        Me.Label293.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label215
        '
        Me.Label215.AutoSize = True
        Me.Label215.Location = New System.Drawing.Point(233, 84)
        Me.Label215.Name = "Label215"
        Me.Label215.Size = New System.Drawing.Size(43, 13)
        Me.Label215.TabIndex = 400
        Me.Label215.Text = "minutes"
        Me.Label215.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtOtherInformationMethod22
        '
        Me.txtOtherInformationMethod22.AcceptsReturn = True
        Me.txtOtherInformationMethod22.Location = New System.Drawing.Point(137, 130)
        Me.txtOtherInformationMethod22.MaxLength = 4000
        Me.txtOtherInformationMethod22.Multiline = True
        Me.txtOtherInformationMethod22.Name = "txtOtherInformationMethod22"
        Me.txtOtherInformationMethod22.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOtherInformationMethod22.Size = New System.Drawing.Size(616, 176)
        Me.txtOtherInformationMethod22.TabIndex = 498
        '
        'Label216
        '
        Me.Label216.AutoSize = True
        Me.Label216.Location = New System.Drawing.Point(5, 130)
        Me.Label216.Name = "Label216"
        Me.Label216.Size = New System.Drawing.Size(91, 13)
        Me.Label216.TabIndex = 399
        Me.Label216.Text = "Other Information:"
        Me.Label216.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtTestDurationMethod22
        '
        Me.txtTestDurationMethod22.Location = New System.Drawing.Point(137, 84)
        Me.txtTestDurationMethod22.MaxLength = 11
        Me.txtTestDurationMethod22.Name = "txtTestDurationMethod22"
        Me.txtTestDurationMethod22.Size = New System.Drawing.Size(96, 20)
        Me.txtTestDurationMethod22.TabIndex = 496
        '
        'txtAccumulatedEmissionMethod22
        '
        Me.txtAccumulatedEmissionMethod22.Location = New System.Drawing.Point(137, 105)
        Me.txtAccumulatedEmissionMethod22.MaxLength = 11
        Me.txtAccumulatedEmissionMethod22.Name = "txtAccumulatedEmissionMethod22"
        Me.txtAccumulatedEmissionMethod22.Size = New System.Drawing.Size(192, 20)
        Me.txtAccumulatedEmissionMethod22.TabIndex = 497
        '
        'Label217
        '
        Me.Label217.Location = New System.Drawing.Point(5, 106)
        Me.Label217.Name = "Label217"
        Me.Label217.Size = New System.Drawing.Size(125, 24)
        Me.Label217.TabIndex = 398
        Me.Label217.Text = "Accumulated Emission Time (min:sec)"
        Me.Label217.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label218
        '
        Me.Label218.AutoSize = True
        Me.Label218.Location = New System.Drawing.Point(50, 84)
        Me.Label218.Name = "Label218"
        Me.Label218.Size = New System.Drawing.Size(74, 13)
        Me.Label218.TabIndex = 397
        Me.Label218.Text = "Test Duration:"
        Me.Label218.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtApplicableRegulationMethod22
        '
        Me.txtApplicableRegulationMethod22.AcceptsReturn = True
        Me.txtApplicableRegulationMethod22.Location = New System.Drawing.Point(137, 52)
        Me.txtApplicableRegulationMethod22.MaxLength = 200
        Me.txtApplicableRegulationMethod22.Multiline = True
        Me.txtApplicableRegulationMethod22.Name = "txtApplicableRegulationMethod22"
        Me.txtApplicableRegulationMethod22.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtApplicableRegulationMethod22.Size = New System.Drawing.Size(616, 32)
        Me.txtApplicableRegulationMethod22.TabIndex = 495
        '
        'txtOperatingCapacityMethod22
        '
        Me.txtOperatingCapacityMethod22.Location = New System.Drawing.Point(497, 8)
        Me.txtOperatingCapacityMethod22.MaxLength = 11
        Me.txtOperatingCapacityMethod22.Name = "txtOperatingCapacityMethod22"
        Me.txtOperatingCapacityMethod22.Size = New System.Drawing.Size(88, 20)
        Me.txtOperatingCapacityMethod22.TabIndex = 492
        '
        'Label219
        '
        Me.Label219.AutoSize = True
        Me.Label219.Location = New System.Drawing.Point(393, 8)
        Me.Label219.Name = "Label219"
        Me.Label219.Size = New System.Drawing.Size(100, 13)
        Me.Label219.TabIndex = 396
        Me.Label219.Text = "Operating Capacity:"
        Me.Label219.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtAllowableEmissionRateMethod22
        '
        Me.txtAllowableEmissionRateMethod22.Location = New System.Drawing.Point(137, 30)
        Me.txtAllowableEmissionRateMethod22.MaxLength = 11
        Me.txtAllowableEmissionRateMethod22.Name = "txtAllowableEmissionRateMethod22"
        Me.txtAllowableEmissionRateMethod22.Size = New System.Drawing.Size(200, 20)
        Me.txtAllowableEmissionRateMethod22.TabIndex = 494
        '
        'txtMaximumExpectedOperatingCapacityMethod22
        '
        Me.txtMaximumExpectedOperatingCapacityMethod22.Location = New System.Drawing.Point(137, 8)
        Me.txtMaximumExpectedOperatingCapacityMethod22.MaxLength = 11
        Me.txtMaximumExpectedOperatingCapacityMethod22.Name = "txtMaximumExpectedOperatingCapacityMethod22"
        Me.txtMaximumExpectedOperatingCapacityMethod22.Size = New System.Drawing.Size(88, 20)
        Me.txtMaximumExpectedOperatingCapacityMethod22.TabIndex = 490
        '
        'Label220
        '
        Me.Label220.AutoSize = True
        Me.Label220.Location = New System.Drawing.Point(5, 52)
        Me.Label220.Name = "Label220"
        Me.Label220.Size = New System.Drawing.Size(122, 13)
        Me.Label220.TabIndex = 395
        Me.Label220.Text = "Applicable Requirement:"
        Me.Label220.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label221
        '
        Me.Label221.AutoSize = True
        Me.Label221.Location = New System.Drawing.Point(5, 32)
        Me.Label221.Name = "Label221"
        Me.Label221.Size = New System.Drawing.Size(125, 13)
        Me.Label221.TabIndex = 394
        Me.Label221.Text = "Allowable Emission Rate:"
        Me.Label221.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cboOperatingCapacityUnitsMethod22
        '
        Me.cboOperatingCapacityUnitsMethod22.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOperatingCapacityUnitsMethod22.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOperatingCapacityUnitsMethod22.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperatingCapacityUnitsMethod22.Location = New System.Drawing.Point(585, 8)
        Me.cboOperatingCapacityUnitsMethod22.Name = "cboOperatingCapacityUnitsMethod22"
        Me.cboOperatingCapacityUnitsMethod22.Size = New System.Drawing.Size(112, 21)
        Me.cboOperatingCapacityUnitsMethod22.TabIndex = 493
        '
        'cboMaximumExpectedOperatingCapacityUnitsMethod22
        '
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod22.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod22.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod22.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod22.Location = New System.Drawing.Point(225, 8)
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod22.Name = "cboMaximumExpectedOperatingCapacityUnitsMethod22"
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod22.Size = New System.Drawing.Size(112, 21)
        Me.cboMaximumExpectedOperatingCapacityUnitsMethod22.TabIndex = 491
        '
        'Label222
        '
        Me.Label222.Location = New System.Drawing.Point(5, 6)
        Me.Label222.Name = "Label222"
        Me.Label222.Size = New System.Drawing.Size(104, 24)
        Me.Label222.TabIndex = 393
        Me.Label222.Text = "Maximum Expected Operating Capacity:"
        Me.Label222.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TPSSCPWork
        '
        Me.TPSSCPWork.AutoScroll = True
        Me.TPSSCPWork.Controls.Add(Me.cboStaffResponsible)
        Me.TPSSCPWork.Controls.Add(Me.Label223)
        Me.TPSSCPWork.Controls.Add(Me.txtTrackingNumber)
        Me.TPSSCPWork.Controls.Add(Me.Label315)
        Me.TPSSCPWork.Controls.Add(Me.chbAcknoledgmentLetterSent)
        Me.TPSSCPWork.Controls.Add(Me.DTPAcknoledgmentLetterSent)
        Me.TPSSCPWork.Controls.Add(Me.txtEnforcementNumber)
        Me.TPSSCPWork.Controls.Add(Me.chbEventComplete)
        Me.TPSSCPWork.Controls.Add(Me.DTPEventCompleteDate)
        Me.TPSSCPWork.Controls.Add(Me.btnEnforcementProcess)
        Me.TPSSCPWork.Controls.Add(Me.btnSaveSSCPData)
        Me.TPSSCPWork.Controls.Add(Me.txtTestReportReceivedbySSCPDate)
        Me.TPSSCPWork.Controls.Add(Me.Label320)
        Me.TPSSCPWork.Controls.Add(Me.Panel22)
        Me.TPSSCPWork.Controls.Add(Me.Label321)
        Me.TPSSCPWork.Controls.Add(Me.Label322)
        Me.TPSSCPWork.Controls.Add(Me.Label323)
        Me.TPSSCPWork.Controls.Add(Me.DTPTestReportDueDate)
        Me.TPSSCPWork.Controls.Add(Me.chbTestReportChangeDueDate)
        Me.TPSSCPWork.Controls.Add(Me.DTPTestReportNewDueDate)
        Me.TPSSCPWork.Controls.Add(Me.Label325)
        Me.TPSSCPWork.Controls.Add(Me.txtTestReportComments)
        Me.TPSSCPWork.Controls.Add(Me.Label326)
        Me.TPSSCPWork.Controls.Add(Me.txtTestReportDueDate)
        Me.TPSSCPWork.Location = New System.Drawing.Point(4, 22)
        Me.TPSSCPWork.Name = "TPSSCPWork"
        Me.TPSSCPWork.Size = New System.Drawing.Size(782, 288)
        Me.TPSSCPWork.TabIndex = 11
        Me.TPSSCPWork.Text = "Compliance Work"
        Me.TPSSCPWork.UseVisualStyleBackColor = True
        '
        'cboStaffResponsible
        '
        Me.cboStaffResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaffResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaffResponsible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStaffResponsible.Location = New System.Drawing.Point(118, 61)
        Me.cboStaffResponsible.Name = "cboStaffResponsible"
        Me.cboStaffResponsible.Size = New System.Drawing.Size(204, 21)
        Me.cboStaffResponsible.TabIndex = 502
        '
        'Label223
        '
        Me.Label223.AutoSize = True
        Me.Label223.Location = New System.Drawing.Point(8, 65)
        Me.Label223.Name = "Label223"
        Me.Label223.Size = New System.Drawing.Size(93, 13)
        Me.Label223.TabIndex = 421
        Me.Label223.Text = "Staff Responsible:"
        Me.Label223.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(118, 34)
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.ReadOnly = True
        Me.txtTrackingNumber.Size = New System.Drawing.Size(69, 20)
        Me.txtTrackingNumber.TabIndex = 420
        '
        'Label315
        '
        Me.Label315.AutoSize = True
        Me.Label315.Location = New System.Drawing.Point(8, 38)
        Me.Label315.Name = "Label315"
        Me.Label315.Size = New System.Drawing.Size(92, 13)
        Me.Label315.TabIndex = 419
        Me.Label315.Text = "Tracking Number:"
        '
        'chbAcknoledgmentLetterSent
        '
        Me.chbAcknoledgmentLetterSent.AutoSize = True
        Me.chbAcknoledgmentLetterSent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbAcknoledgmentLetterSent.Location = New System.Drawing.Point(397, 65)
        Me.chbAcknoledgmentLetterSent.Name = "chbAcknoledgmentLetterSent"
        Me.chbAcknoledgmentLetterSent.Size = New System.Drawing.Size(133, 17)
        Me.chbAcknoledgmentLetterSent.TabIndex = 505
        Me.chbAcknoledgmentLetterSent.Text = "Acknowledgment Sent"
        '
        'DTPAcknoledgmentLetterSent
        '
        Me.DTPAcknoledgmentLetterSent.CustomFormat = "dd-MMM-yyyy"
        Me.DTPAcknoledgmentLetterSent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPAcknoledgmentLetterSent.Location = New System.Drawing.Point(542, 63)
        Me.DTPAcknoledgmentLetterSent.Name = "DTPAcknoledgmentLetterSent"
        Me.DTPAcknoledgmentLetterSent.Size = New System.Drawing.Size(100, 20)
        Me.DTPAcknoledgmentLetterSent.TabIndex = 506
        Me.DTPAcknoledgmentLetterSent.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'txtEnforcementNumber
        '
        Me.txtEnforcementNumber.Location = New System.Drawing.Point(541, 35)
        Me.txtEnforcementNumber.Name = "txtEnforcementNumber"
        Me.txtEnforcementNumber.ReadOnly = True
        Me.txtEnforcementNumber.Size = New System.Drawing.Size(77, 20)
        Me.txtEnforcementNumber.TabIndex = 412
        '
        'chbEventComplete
        '
        Me.chbEventComplete.AutoSize = True
        Me.chbEventComplete.Location = New System.Drawing.Point(11, 10)
        Me.chbEventComplete.Name = "chbEventComplete"
        Me.chbEventComplete.Size = New System.Drawing.Size(73, 17)
        Me.chbEventComplete.TabIndex = 500
        Me.chbEventComplete.Text = "Complete:"
        '
        'DTPEventCompleteDate
        '
        Me.DTPEventCompleteDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPEventCompleteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPEventCompleteDate.Location = New System.Drawing.Point(84, 9)
        Me.DTPEventCompleteDate.Name = "DTPEventCompleteDate"
        Me.DTPEventCompleteDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPEventCompleteDate.TabIndex = 501
        '
        'btnEnforcementProcess
        '
        Me.btnEnforcementProcess.Location = New System.Drawing.Point(386, 34)
        Me.btnEnforcementProcess.Name = "btnEnforcementProcess"
        Me.btnEnforcementProcess.Size = New System.Drawing.Size(144, 23)
        Me.btnEnforcementProcess.TabIndex = 413
        Me.btnEnforcementProcess.Text = "Enforcement Process"
        '
        'btnSaveSSCPData
        '
        Me.btnSaveSSCPData.AutoSize = True
        Me.btnSaveSSCPData.Location = New System.Drawing.Point(8, 334)
        Me.btnSaveSSCPData.Name = "btnSaveSSCPData"
        Me.btnSaveSSCPData.Size = New System.Drawing.Size(126, 23)
        Me.btnSaveSSCPData.TabIndex = 511
        Me.btnSaveSSCPData.Text = "Save Compliance Data"
        Me.btnSaveSSCPData.UseVisualStyleBackColor = True
        '
        'txtTestReportReceivedbySSCPDate
        '
        Me.txtTestReportReceivedbySSCPDate.Location = New System.Drawing.Point(208, 96)
        Me.txtTestReportReceivedbySSCPDate.Name = "txtTestReportReceivedbySSCPDate"
        Me.txtTestReportReceivedbySSCPDate.ReadOnly = True
        Me.txtTestReportReceivedbySSCPDate.Size = New System.Drawing.Size(137, 20)
        Me.txtTestReportReceivedbySSCPDate.TabIndex = 408
        '
        'Label320
        '
        Me.Label320.AutoSize = True
        Me.Label320.Location = New System.Drawing.Point(8, 99)
        Me.Label320.Name = "Label320"
        Me.Label320.Size = New System.Drawing.Size(198, 13)
        Me.Label320.TabIndex = 407
        Me.Label320.Text = "Test Summary Received by Compliance:"
        '
        'Panel22
        '
        Me.Panel22.Controls.Add(Me.rdbTestReportFollowUpYes)
        Me.Panel22.Controls.Add(Me.rdbTestReportFollowUpNo)
        Me.Panel22.Location = New System.Drawing.Point(160, 296)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(96, 16)
        Me.Panel22.TabIndex = 406
        '
        'rdbTestReportFollowUpYes
        '
        Me.rdbTestReportFollowUpYes.Location = New System.Drawing.Point(0, 0)
        Me.rdbTestReportFollowUpYes.Name = "rdbTestReportFollowUpYes"
        Me.rdbTestReportFollowUpYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbTestReportFollowUpYes.TabIndex = 509
        Me.rdbTestReportFollowUpYes.Text = "Yes"
        '
        'rdbTestReportFollowUpNo
        '
        Me.rdbTestReportFollowUpNo.Location = New System.Drawing.Point(48, 0)
        Me.rdbTestReportFollowUpNo.Name = "rdbTestReportFollowUpNo"
        Me.rdbTestReportFollowUpNo.Size = New System.Drawing.Size(48, 16)
        Me.rdbTestReportFollowUpNo.TabIndex = 510
        Me.rdbTestReportFollowUpNo.Text = "No"
        '
        'Label321
        '
        Me.Label321.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label321.Location = New System.Drawing.Point(8, 312)
        Me.Label321.Name = "Label321"
        Me.Label321.Size = New System.Drawing.Size(235, 1)
        Me.Label321.TabIndex = 405
        '
        'Label322
        '
        Me.Label322.AutoSize = True
        Me.Label322.Location = New System.Drawing.Point(8, 296)
        Me.Label322.Name = "Label322"
        Me.Label322.Size = New System.Drawing.Size(121, 13)
        Me.Label322.TabIndex = 404
        Me.Label322.Text = "Follow-Up Action Taken"
        '
        'Label323
        '
        Me.Label323.AutoSize = True
        Me.Label323.Location = New System.Drawing.Point(8, 136)
        Me.Label323.Name = "Label323"
        Me.Label323.Size = New System.Drawing.Size(166, 13)
        Me.Label323.TabIndex = 396
        Me.Label323.Text = "Date Performance Test Required:"
        '
        'DTPTestReportDueDate
        '
        Me.DTPTestReportDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestReportDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestReportDueDate.Location = New System.Drawing.Point(180, 156)
        Me.DTPTestReportDueDate.Name = "DTPTestReportDueDate"
        Me.DTPTestReportDueDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPTestReportDueDate.TabIndex = 504
        Me.DTPTestReportDueDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'chbTestReportChangeDueDate
        '
        Me.chbTestReportChangeDueDate.AutoSize = True
        Me.chbTestReportChangeDueDate.Location = New System.Drawing.Point(39, 156)
        Me.chbTestReportChangeDueDate.Name = "chbTestReportChangeDueDate"
        Me.chbTestReportChangeDueDate.Size = New System.Drawing.Size(135, 17)
        Me.chbTestReportChangeDueDate.TabIndex = 503
        Me.chbTestReportChangeDueDate.Text = "Change Required Date"
        '
        'DTPTestReportNewDueDate
        '
        Me.DTPTestReportNewDueDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTestReportNewDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTestReportNewDueDate.Location = New System.Drawing.Point(558, 131)
        Me.DTPTestReportNewDueDate.Name = "DTPTestReportNewDueDate"
        Me.DTPTestReportNewDueDate.Size = New System.Drawing.Size(100, 20)
        Me.DTPTestReportNewDueDate.TabIndex = 507
        Me.DTPTestReportNewDueDate.Value = New Date(2005, 4, 21, 0, 0, 0, 0)
        '
        'Label325
        '
        Me.Label325.AutoSize = True
        Me.Label325.Location = New System.Drawing.Point(386, 136)
        Me.Label325.Name = "Label325"
        Me.Label325.Size = New System.Drawing.Size(160, 13)
        Me.Label325.TabIndex = 400
        Me.Label325.Text = "Date for Next Performance Test:"
        '
        'txtTestReportComments
        '
        Me.txtTestReportComments.AcceptsReturn = True
        Me.txtTestReportComments.Location = New System.Drawing.Point(130, 192)
        Me.txtTestReportComments.MaxLength = 4000
        Me.txtTestReportComments.Multiline = True
        Me.txtTestReportComments.Name = "txtTestReportComments"
        Me.txtTestReportComments.Size = New System.Drawing.Size(615, 88)
        Me.txtTestReportComments.TabIndex = 508
        '
        'Label326
        '
        Me.Label326.AutoSize = True
        Me.Label326.Location = New System.Drawing.Point(8, 192)
        Me.Label326.Name = "Label326"
        Me.Label326.Size = New System.Drawing.Size(118, 13)
        Me.Label326.TabIndex = 398
        Me.Label326.Text = "Test Report Comments:"
        '
        'txtTestReportDueDate
        '
        Me.txtTestReportDueDate.Location = New System.Drawing.Point(180, 132)
        Me.txtTestReportDueDate.Name = "txtTestReportDueDate"
        Me.txtTestReportDueDate.ReadOnly = True
        Me.txtTestReportDueDate.Size = New System.Drawing.Size(120, 20)
        Me.txtTestReportDueDate.TabIndex = 397
        '
        'DeletedTestFlag
        '
        Me.DeletedTestFlag.AutoSize = True
        Me.DeletedTestFlag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeletedTestFlag.Location = New System.Drawing.Point(341, 36)
        Me.DeletedTestFlag.Name = "DeletedTestFlag"
        Me.DeletedTestFlag.Size = New System.Drawing.Size(252, 13)
        Me.DeletedTestFlag.TabIndex = 465
        Me.DeletedTestFlag.Text = "TEST REPORT IS FLAGGED AS DELETED"
        Me.DeletedTestFlag.Visible = False
        '
        'ISMPTestReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 742)
        Me.Controls.Add(Me.DeletedTestFlag)
        Me.Controls.Add(Me.SCTestReports)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Location = New System.Drawing.Point(25, 0)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "ISMPTestReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Performance Monitoring Test Reports"
        Me.MenuStrip1.ResumeLayout(false)
        Me.MenuStrip1.PerformLayout
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.SCTestReports.Panel1.ResumeLayout(false)
        Me.SCTestReports.Panel2.ResumeLayout(false)
        CType(Me.SCTestReports,System.ComponentModel.ISupportInitialize).EndInit
        Me.SCTestReports.ResumeLayout(false)
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.TCDocumentTypes.ResumeLayout(false)
        Me.TPOneStack.ResumeLayout(false)
        Me.TPOneStack.PerformLayout
        Me.TCOneStack.ResumeLayout(false)
        Me.TPOneStackTwoRun.ResumeLayout(false)
        Me.TPOneStackTwoRun.PerformLayout
        Me.TPOneStackThreeRun.ResumeLayout(false)
        Me.TPOneStackThreeRun.PerformLayout
        Me.TPOneStackFourRun.ResumeLayout(false)
        Me.TPOneStackFourRun.PerformLayout
        Me.TPLoadingRack.ResumeLayout(false)
        Me.TPLoadingRack.PerformLayout
        Me.TPPondTreatment.ResumeLayout(false)
        Me.TPPondTreatment.PerformLayout
        Me.TPGasConcentration.ResumeLayout(false)
        Me.TPGasConcentration.PerformLayout
        Me.TPFlare.ResumeLayout(false)
        Me.TPFlare.PerformLayout
        Me.TPMethodNine.ResumeLayout(false)
        Me.TPMethodNine.PerformLayout
        Me.TCMethodNine.ResumeLayout(false)
        Me.TPMethodNineSingle.ResumeLayout(false)
        Me.TPMethodNineSingle.PerformLayout
        Me.Panel4.ResumeLayout(false)
        Me.Panel4.PerformLayout
        Me.TPMethodNineMultiple.ResumeLayout(false)
        Me.TPMethodNineMultiple.PerformLayout
        Me.Panel5.ResumeLayout(false)
        Me.Panel5.PerformLayout
        Me.TPMethodNineMultiple2.ResumeLayout(false)
        Me.TPMethodNineMultiple2.PerformLayout
        Me.TPMemorandum.ResumeLayout(false)
        Me.TPMemorandum.PerformLayout
        Me.TCMemorandum.ResumeLayout(false)
        Me.TPMemoStandard.ResumeLayout(false)
        Me.TPMemoStandard.PerformLayout
        Me.TPMemoToFile.ResumeLayout(false)
        Me.TPMemoToFile.PerformLayout
        Me.TPMemoPTE.ResumeLayout(false)
        Me.TPMemoPTE.PerformLayout
        Me.TPRata.ResumeLayout(false)
        Me.TPRata.PerformLayout
        Me.TPTwoStack.ResumeLayout(false)
        Me.TPTwoStack.PerformLayout
        Me.TCTwoStack.ResumeLayout(false)
        Me.TPTwoStackStandard.ResumeLayout(false)
        Me.TPTwoStackStandard.PerformLayout
        Me.TPTwoStackDRE.ResumeLayout(false)
        Me.TPTwoStackDRE.PerformLayout
        Me.TPMethodTwentyTwo.ResumeLayout(false)
        Me.TPMethodTwentyTwo.PerformLayout
        Me.TPSSCPWork.ResumeLayout(false)
        Me.TPSSCPWork.PerformLayout
        Me.Panel22.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiOpenMemo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiTool As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiOpenTestLogNotification As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiOpenExcelFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiPrePopulate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbMemo As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbTestLogLink As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents SCTestReports As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboComplianceStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtDaysInAPB As System.Windows.Forms.TextBox
    Friend WithEvents cboccBox As System.Windows.Forms.ComboBox
    Friend WithEvents txtDaysAssigned As System.Windows.Forms.TextBox
    Friend WithEvents Label297 As System.Windows.Forms.Label
    Friend WithEvents cboTestingFirm As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cboPollutantDetermined As System.Windows.Forms.ComboBox
    Friend WithEvents cboReportType As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cboComplianceManager As System.Windows.Forms.ComboBox
    Friend WithEvents DTPTestDateComplete As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPTestDateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboReviewingEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents cboISMPUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtProgramManager As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cboMethodDetermined As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCompleteDate As System.Windows.Forms.TextBox
    Friend WithEvents txtReceivedByAPB As System.Windows.Forms.TextBox
    Friend WithEvents cboWitnessingEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityState As System.Windows.Forms.TextBox
    Friend WithEvents txtSourceTested As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityCity As System.Windows.Forms.TextBox
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents txtAirsNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblDatesTested As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents labReferenceNumber As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtAssignedToEngineer As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TCDocumentTypes As System.Windows.Forms.TabControl
    Friend WithEvents TPOneStack As System.Windows.Forms.TabPage
    Friend WithEvents TPLoadingRack As System.Windows.Forms.TabPage
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtOtherInformationOneStack As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TCOneStack As System.Windows.Forms.TabControl
    Friend WithEvents TPOneStackTwoRun As System.Windows.Forms.TabPage
    Friend WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents Label162 As System.Windows.Forms.Label
    Friend WithEvents Label161 As System.Windows.Forms.Label
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents Label152 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateAvgOneStackTwoRun As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgOneStackTwoRun As System.Windows.Forms.TextBox
    Friend WithEvents cboEmissRateUnitOneStackTwoRun As System.Windows.Forms.ComboBox
    Friend WithEvents cboPollConUnitOneStackTwoRun As System.Windows.Forms.ComboBox
    Friend WithEvents txtEmissRateOneStackTwoRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackTwoRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackTwoRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackTwoRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackTwoRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackTwoRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackTwoRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateOneStackTwoRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackTwoRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackTwoRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackTwoRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackTwoRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackTwoRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackTwoRun1A As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents btnClearOneStackTwoRun2 As System.Windows.Forms.Button
    Friend WithEvents btnClearOneStackTwoRun1 As System.Windows.Forms.Button
    Friend WithEvents TPOneStackThreeRun As System.Windows.Forms.TabPage
    Friend WithEvents Label206 As System.Windows.Forms.Label
    Friend WithEvents Label205 As System.Windows.Forms.Label
    Friend WithEvents Label204 As System.Windows.Forms.Label
    Friend WithEvents Label203 As System.Windows.Forms.Label
    Friend WithEvents Label202 As System.Windows.Forms.Label
    Friend WithEvents Label201 As System.Windows.Forms.Label
    Friend WithEvents Label200 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateAvgOneStackThreeRun As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgOneStackThreeRun As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateOneStackThreeRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackThreeRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackThreeRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackThreeRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackThreeRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackThreeRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackThreeRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateOneStackThreeRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackThreeRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackThreeRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackThreeRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackThreeRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackThreeRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackThreeRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateOneStackThreeRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackThreeRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackThreeRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackThreeRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackThreeRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackThreeRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackThreeRun1A As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cboEmissRateUnitOneStackThreeRun As System.Windows.Forms.ComboBox
    Friend WithEvents cboPollConUnitOneStackThreeRun As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearOneStackThreeRun3 As System.Windows.Forms.Button
    Friend WithEvents btnClearOneStackThreeRun2 As System.Windows.Forms.Button
    Friend WithEvents btnClearOneStackThreeRun1 As System.Windows.Forms.Button
    Friend WithEvents TPOneStackFourRun As System.Windows.Forms.TabPage
    Friend WithEvents Label213 As System.Windows.Forms.Label
    Friend WithEvents Label212 As System.Windows.Forms.Label
    Friend WithEvents Label211 As System.Windows.Forms.Label
    Friend WithEvents Label210 As System.Windows.Forms.Label
    Friend WithEvents Label209 As System.Windows.Forms.Label
    Friend WithEvents Label208 As System.Windows.Forms.Label
    Friend WithEvents Label207 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateOneStackFourRun1D As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackFourRun1D As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackFourRun1D As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackFourRun1D As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackFourRun1D As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackFourRun1D As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackFourRun1D As System.Windows.Forms.TextBox
    Friend WithEvents btnClearOneStackFourRun4 As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateAvgOneStackFourRun As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgOneStackFourRun As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateOneStackFourRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackFourRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackFourRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackFourRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackFourRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackFourRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackFourRun1C As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateOneStackFourRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackFourRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackFourRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackFourRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackFourRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackFourRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackFourRun1B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateOneStackFourRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOneStackFourRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistOneStackFourRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMOneStackFourRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMOneStackFourRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempOneStackFourRun1A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumOneStackFourRun1A As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents cboEmissRateUnitOneStackFourRun As System.Windows.Forms.ComboBox
    Friend WithEvents cboPollConUnitOneStackFourRun As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearOneStackFourRun3 As System.Windows.Forms.Button
    Friend WithEvents btnClearOneStackFourRun2 As System.Windows.Forms.Button
    Friend WithEvents btnClearOneStackFourRun1 As System.Windows.Forms.Button
    Friend WithEvents txtControlEquipmentOperatingDataOneStack As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicableRegulationOneStack As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityOneStack As System.Windows.Forms.TextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRate2OneStack As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate3OneStack As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate1OneStack As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityOneStack As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents cboOperatingCapacityUnitsOneStack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits2OneStack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits3OneStack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits1OneStack As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsOneStack As System.Windows.Forms.ComboBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtPercentAllowableOneStack As System.Windows.Forms.TextBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label193 As System.Windows.Forms.Label
    Friend WithEvents Label197 As System.Windows.Forms.Label
    Friend WithEvents Label198 As System.Windows.Forms.Label
    Friend WithEvents Label199 As System.Windows.Forms.Label
    Friend WithEvents TPPondTreatment As System.Windows.Forms.TabPage
    Friend WithEvents TPGasConcentration As System.Windows.Forms.TabPage
    Friend WithEvents TPFlare As System.Windows.Forms.TabPage
    Friend WithEvents TPMethodNine As System.Windows.Forms.TabPage
    Friend WithEvents TPMemorandum As System.Windows.Forms.TabPage
    Friend WithEvents TPRata As System.Windows.Forms.TabPage
    Friend WithEvents TPTwoStack As System.Windows.Forms.TabPage
    Friend WithEvents TPMethodTwentyTwo As System.Windows.Forms.TabPage
    Friend WithEvents TPSSCPWork As System.Windows.Forms.TabPage
    Friend WithEvents Label292 As System.Windows.Forms.Label
    Friend WithEvents Label291 As System.Windows.Forms.Label
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents Label290 As System.Windows.Forms.Label
    Friend WithEvents Label289 As System.Windows.Forms.Label
    Friend WithEvents Label288 As System.Windows.Forms.Label
    Friend WithEvents Label287 As System.Windows.Forms.Label
    Friend WithEvents txtDestructionEfficiencyLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents Label171 As System.Windows.Forms.Label
    Friend WithEvents Label154 As System.Windows.Forms.Label
    Friend WithEvents cboEmissRateUnitLoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents cboPollConUnitOUTLoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents txtOtherInformationLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents Label153 As System.Windows.Forms.Label
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents txtTestDurationLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcOUTLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcINLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents Label156 As System.Windows.Forms.Label
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents cboPollConUnitINLoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents cboTestDurationUnitsLoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents txtControlEquipmentOperatingDataLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicableRegulationLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents Label166 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRate2LoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate3LoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate1LoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityLoadingRack As System.Windows.Forms.TextBox
    Friend WithEvents Label167 As System.Windows.Forms.Label
    Friend WithEvents Label168 As System.Windows.Forms.Label
    Friend WithEvents cboOperatingCapacityUnitsLoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits2LoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits3LoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits1LoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsLoadingRack As System.Windows.Forms.ComboBox
    Friend WithEvents Label169 As System.Windows.Forms.Label
    Friend WithEvents Label170 As System.Windows.Forms.Label
    Friend WithEvents Label286 As System.Windows.Forms.Label
    Friend WithEvents Label285 As System.Windows.Forms.Label
    Friend WithEvents Label284 As System.Windows.Forms.Label
    Friend WithEvents Label283 As System.Windows.Forms.Label
    Friend WithEvents Label146 As System.Windows.Forms.Label
    Friend WithEvents Label282 As System.Windows.Forms.Label
    Friend WithEvents Label281 As System.Windows.Forms.Label
    Friend WithEvents Label280 As System.Windows.Forms.Label
    Friend WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents txtOtherInformationPond As System.Windows.Forms.TextBox
    Friend WithEvents Label137 As System.Windows.Forms.Label
    Friend WithEvents txtDestructionEfficancyPond As System.Windows.Forms.TextBox
    Friend WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents Label142 As System.Windows.Forms.Label
    Friend WithEvents txtTreatmentRateAvgPond As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgPond As System.Windows.Forms.TextBox
    Friend WithEvents txtTreatmentRatePond1C As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcPond1C As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumPond1C As System.Windows.Forms.TextBox
    Friend WithEvents txtTreatmentRatePond1B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcPond1B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumPond1B As System.Windows.Forms.TextBox
    Friend WithEvents txtTreatmentRatePond1A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcPond1A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumPond1A As System.Windows.Forms.TextBox
    Friend WithEvents Label143 As System.Windows.Forms.Label
    Friend WithEvents Label144 As System.Windows.Forms.Label
    Friend WithEvents Label145 As System.Windows.Forms.Label
    Friend WithEvents cboTreatmentRateUnitPond As System.Windows.Forms.ComboBox
    Friend WithEvents cboPollConUnitPond As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearPondTreatment3 As System.Windows.Forms.Button
    Friend WithEvents btnClearPondTreatment2 As System.Windows.Forms.Button
    Friend WithEvents btnClearPondTreatment1 As System.Windows.Forms.Button
    Friend WithEvents txtControlEquipmentOperatingDataPond As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicableRegulationPond As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityPond As System.Windows.Forms.TextBox
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRate2Pond As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate3Pond As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate1Pond As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityPond As System.Windows.Forms.TextBox
    Friend WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents cboOperatingCapacityUnitsPond As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits2Pond As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits3Pond As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits1Pond As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsPond As System.Windows.Forms.ComboBox
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents Label134 As System.Windows.Forms.Label
    Friend WithEvents Label279 As System.Windows.Forms.Label
    Friend WithEvents Label278 As System.Windows.Forms.Label
    Friend WithEvents Label277 As System.Windows.Forms.Label
    Friend WithEvents Label276 As System.Windows.Forms.Label
    Friend WithEvents Label275 As System.Windows.Forms.Label
    Friend WithEvents Label274 As System.Windows.Forms.Label
    Friend WithEvents Label273 As System.Windows.Forms.Label
    Friend WithEvents txtOtherInformationGas As System.Windows.Forms.TextBox
    Friend WithEvents Label136 As System.Windows.Forms.Label
    Friend WithEvents txtPercentAllowableGas As System.Windows.Forms.TextBox
    Friend WithEvents Label135 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateAvgGas As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgGas As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateGas1C As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcGas1C As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumGas1C As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateGas1B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcGas1B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumGas1B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateGas1A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcGas1A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumGas1A As System.Windows.Forms.TextBox
    Friend WithEvents Label138 As System.Windows.Forms.Label
    Friend WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents cboEmissRateUnitGas As System.Windows.Forms.ComboBox
    Friend WithEvents cboPollConUnitGas As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearGasConcentration3 As System.Windows.Forms.Button
    Friend WithEvents btnClearGasConcentration2 As System.Windows.Forms.Button
    Friend WithEvents btnClearGasConcentration1 As System.Windows.Forms.Button
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents txtControlEquipmentOperatingDataGas As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicableRegulationGas As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityGas As System.Windows.Forms.TextBox
    Friend WithEvents Label129 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRate2Gas As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate3Gas As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate1Gas As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityGas As System.Windows.Forms.TextBox
    Friend WithEvents Label130 As System.Windows.Forms.Label
    Friend WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents cboOperatingCapacityUnitsGas As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits2Gas As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits3Gas As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits1Gas As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsGas As System.Windows.Forms.ComboBox
    Friend WithEvents Label132 As System.Windows.Forms.Label
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents Label272 As System.Windows.Forms.Label
    Friend WithEvents Label271 As System.Windows.Forms.Label
    Friend WithEvents Label270 As System.Windows.Forms.Label
    Friend WithEvents Label269 As System.Windows.Forms.Label
    Friend WithEvents Label268 As System.Windows.Forms.Label
    Friend WithEvents Label267 As System.Windows.Forms.Label
    Friend WithEvents txtOtherInformationFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtPercentAllowableFlare As System.Windows.Forms.TextBox
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents txtHeatingValuesAvgFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtVelocityAvgFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtVelocity1BFlare As System.Windows.Forms.TextBox
    Friend WithEvents cboHeatingValueUnits As System.Windows.Forms.ComboBox
    Friend WithEvents cboVelocityUnitsFlare As System.Windows.Forms.ComboBox
    Friend WithEvents txtHeatingValue1AFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtHeatingValue1BFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtHeatingValue1CFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtVelocity1AFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtVelocity1CFlare As System.Windows.Forms.TextBox
    Friend WithEvents Label121 As System.Windows.Forms.Label
    Friend WithEvents Label120 As System.Windows.Forms.Label
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents txtHeatContentFlare As System.Windows.Forms.TextBox
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents txtMonitoringDataFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicableRegulationFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityFlare As System.Windows.Forms.TextBox
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents txtVelocityFlare As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityFlare As System.Windows.Forms.TextBox
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents cboOperatingCapacityUnitsFlare As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsFlare As System.Windows.Forms.ComboBox
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents txtOtherInformationMethod9 As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents TCMethodNine As System.Windows.Forms.TabControl
    Friend WithEvents TPMethodNineSingle As System.Windows.Forms.TabPage
    Friend WithEvents Label256 As System.Windows.Forms.Label
    Friend WithEvents Label255 As System.Windows.Forms.Label
    Friend WithEvents Label254 As System.Windows.Forms.Label
    Friend WithEvents Label253 As System.Windows.Forms.Label
    Friend WithEvents Label252 As System.Windows.Forms.Label
    Friend WithEvents txtOpacityMethod9Single As System.Windows.Forms.TextBox
    Friend WithEvents txtTestDurationMethod9Single As System.Windows.Forms.TextBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsMethod9Single As System.Windows.Forms.ComboBox
    Friend WithEvents cboOperatingCapacityUnitsMethod9Single As System.Windows.Forms.ComboBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents txtOperatingCapacityMethod9Single As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate1Method9Single As System.Windows.Forms.TextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents txtApplicableRegulationMethod9Single As System.Windows.Forms.TextBox
    Friend WithEvents txtControlEquipmentOperatingDataMethod9Single As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityMethod9Single As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents cboAllowableEmissionRateUnits1Method9Single As System.Windows.Forms.ComboBox
    Friend WithEvents TPMethodNineMultiple As System.Windows.Forms.TabPage
    Friend WithEvents Label261 As System.Windows.Forms.Label
    Friend WithEvents Label260 As System.Windows.Forms.Label
    Friend WithEvents Label259 As System.Windows.Forms.Label
    Friend WithEvents Label258 As System.Windows.Forms.Label
    Friend WithEvents Label257 As System.Windows.Forms.Label
    Friend WithEvents txt6minuteAvg1EMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txt6minuteAvg1DMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txt6minuteAvg1AMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txt6minuteAvg1BMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txt6minuteAvg1CMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRate3Method9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate4Method9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate5Method9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityMethod9Multi5 As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityMethod9Multi2 As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityMethod9Multi3 As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityMethod9Multi4 As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityMethod9Multi3 As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityMethod9Multi2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityMethod9Multi4 As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityMethod9Multi5 As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityMethod9Multi1 As System.Windows.Forms.TextBox
    Friend WithEvents cboOperatingCapacityUnitsMethod9Multi As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsMethod9Multi As System.Windows.Forms.ComboBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRate1Method9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate2Method9Multi As System.Windows.Forms.TextBox
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents txtApplicableRegulationMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtControlEquipmentOperatingDataMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityMethod9Multi1 As System.Windows.Forms.TextBox
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents cboAllowableEmissionRateUnitsMethod9Multi As System.Windows.Forms.ComboBox
    Friend WithEvents TPMethodNineMultiple2 As System.Windows.Forms.TabPage
    Friend WithEvents Label266 As System.Windows.Forms.Label
    Friend WithEvents Label265 As System.Windows.Forms.Label
    Friend WithEvents Label264 As System.Windows.Forms.Label
    Friend WithEvents Label263 As System.Windows.Forms.Label
    Friend WithEvents Label262 As System.Windows.Forms.Label
    Friend WithEvents txtEquipmentItem1EMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtEquipmentItem1DMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtEquipmentItem1BMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtEquipmentItem1CMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents txtEquipmentItem1AMethod9Multi As System.Windows.Forms.TextBox
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label247 As System.Windows.Forms.Label
    Friend WithEvents txtApplicableRegulationMemorandum As System.Windows.Forms.TextBox
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents TCMemorandum As System.Windows.Forms.TabControl
    Friend WithEvents TPMemoStandard As System.Windows.Forms.TabPage
    Friend WithEvents txtMemorandumStandard As System.Windows.Forms.TextBox
    Friend WithEvents Label122 As System.Windows.Forms.Label
    Friend WithEvents TPMemoToFile As System.Windows.Forms.TabPage
    Friend WithEvents Label251 As System.Windows.Forms.Label
    Friend WithEvents Label191 As System.Windows.Forms.Label
    Friend WithEvents txtSerialNumberMemorandumToFile As System.Windows.Forms.TextBox
    Friend WithEvents txtModelMemorandumToFile As System.Windows.Forms.TextBox
    Friend WithEvents Label128 As System.Windows.Forms.Label
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents txtMemorandumToFile As System.Windows.Forms.TextBox
    Friend WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents TPMemoPTE As System.Windows.Forms.TabPage
    Friend WithEvents Label250 As System.Windows.Forms.Label
    Friend WithEvents Label249 As System.Windows.Forms.Label
    Friend WithEvents Label248 As System.Windows.Forms.Label
    Friend WithEvents txtMemorandumPTE As System.Windows.Forms.TextBox
    Friend WithEvents Label190 As System.Windows.Forms.Label
    Friend WithEvents txtControlEquipmentOperatingDataMemorandumPTE As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityMemorandumPTE As System.Windows.Forms.TextBox
    Friend WithEvents Label192 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRate2MemorandumPTE As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate3MemorandumPTE As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate1MemorandumPTE As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityMemorandumPTE As System.Windows.Forms.TextBox
    Friend WithEvents Label194 As System.Windows.Forms.Label
    Friend WithEvents cboOperatingCapacityUnitsMemorandumPTE As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits2MemorandumPTE As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits3MemorandumPTE As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits1MemorandumPTE As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsMemorandumPTE As System.Windows.Forms.ComboBox
    Friend WithEvents Label195 As System.Windows.Forms.Label
    Friend WithEvents Label196 As System.Windows.Forms.Label
    Friend WithEvents chbOmitRunRata5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata12 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata11 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata10 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata8 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOmitRunRata1 As System.Windows.Forms.CheckBox
    Public WithEvents Label319 As System.Windows.Forms.Label
    Public WithEvents Label318 As System.Windows.Forms.Label
    Friend WithEvents Label317 As System.Windows.Forms.Label
    Public WithEvents txtCMSRata11 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata12 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata10 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata9 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata8 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata7 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata6 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata5 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata4 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata3 As System.Windows.Forms.TextBox
    Public WithEvents txtCMSRata2 As System.Windows.Forms.TextBox
    Public WithEvents Label316 As System.Windows.Forms.Label
    Public WithEvents txtRefMethodRata8 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata9 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata12 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata11 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata10 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata7 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata6 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata5 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata4 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata3 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata2 As System.Windows.Forms.TextBox
    Friend WithEvents Label314 As System.Windows.Forms.Label
    Public WithEvents Label313 As System.Windows.Forms.Label
    Public WithEvents Label312 As System.Windows.Forms.Label
    Public WithEvents Label311 As System.Windows.Forms.Label
    Public WithEvents Label310 As System.Windows.Forms.Label
    Public WithEvents Label309 As System.Windows.Forms.Label
    Public WithEvents Label308 As System.Windows.Forms.Label
    Public WithEvents Label307 As System.Windows.Forms.Label
    Public WithEvents Label306 As System.Windows.Forms.Label
    Public WithEvents Label305 As System.Windows.Forms.Label
    Public WithEvents Label304 As System.Windows.Forms.Label
    Public WithEvents Label303 As System.Windows.Forms.Label
    Public WithEvents Label302 As System.Windows.Forms.Label
    Friend WithEvents Label301 As System.Windows.Forms.Label
    Friend WithEvents Label300 As System.Windows.Forms.Label
    Friend WithEvents Label299 As System.Windows.Forms.Label
    Public WithEvents txtCMSRata1 As System.Windows.Forms.TextBox
    Public WithEvents txtRefMethodRata1 As System.Windows.Forms.TextBox
    Public WithEvents Label114b As System.Windows.Forms.Label
    Public WithEvents cboUnitsRata As System.Windows.Forms.ComboBox
    Public WithEvents lable3b As System.Windows.Forms.Label
    Public WithEvents Label112b As System.Windows.Forms.Label
    Public WithEvents Label111b As System.Windows.Forms.Label
    Friend WithEvents cboDiluentRata As System.Windows.Forms.ComboBox
    Friend WithEvents lblDiluentRata As System.Windows.Forms.Label
    Friend WithEvents txtApplicableStandardPercentRata As System.Windows.Forms.TextBox
    Friend WithEvents txtRefMethodPercentRata As System.Windows.Forms.TextBox
    Public WithEvents txtRelativeAccuracy As System.Windows.Forms.TextBox
    Public WithEvents Label187 As System.Windows.Forms.Label
    Friend WithEvents lblStandardRata As System.Windows.Forms.Label
    Friend WithEvents lblRefMethodRata As System.Windows.Forms.Label
    Friend WithEvents txtOtherInformationRata As System.Windows.Forms.TextBox
    Friend WithEvents Label185 As System.Windows.Forms.Label
    Friend WithEvents Label183 As System.Windows.Forms.Label
    Friend WithEvents cboDilutentMonitoredRata As System.Windows.Forms.ComboBox
    Friend WithEvents Label177 As System.Windows.Forms.Label
    Friend WithEvents txtApplicableRegulationRata As System.Windows.Forms.TextBox
    Friend WithEvents Label175 As System.Windows.Forms.Label
    Friend WithEvents txtApplicableStandardRata As System.Windows.Forms.TextBox
    Friend WithEvents Label173 As System.Windows.Forms.Label
    Friend WithEvents Label227 As System.Windows.Forms.Label
    Friend WithEvents Label226 As System.Windows.Forms.Label
    Friend WithEvents Label225 As System.Windows.Forms.Label
    Friend WithEvents Label224 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtOtherInformationTwoStack As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents TCTwoStack As System.Windows.Forms.TabControl
    Friend WithEvents TPTwoStackStandard As System.Windows.Forms.TabPage
    Friend WithEvents Label298 As System.Windows.Forms.Label
    Friend WithEvents Label237 As System.Windows.Forms.Label
    Friend WithEvents Label236 As System.Windows.Forms.Label
    Friend WithEvents Label235 As System.Windows.Forms.Label
    Friend WithEvents Label234 As System.Windows.Forms.Label
    Friend WithEvents Label233 As System.Windows.Forms.Label
    Friend WithEvents Label232 As System.Windows.Forms.Label
    Friend WithEvents Label231 As System.Windows.Forms.Label
    Friend WithEvents Label230 As System.Windows.Forms.Label
    Friend WithEvents Label229 As System.Windows.Forms.Label
    Friend WithEvents Label228 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateTotalAvgTwoStackStandard As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateAvgTwoStackStandard2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgTwoStackStandard1 As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTotalTwoStackStandard3 As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTotalTwoStackStandard2 As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTotalTwoStackStandard1 As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtRunNumTwoStackStandard2C As System.Windows.Forms.TextBox
    Friend WithEvents btnClearTwoStackStandard2C As System.Windows.Forms.Button
    Friend WithEvents txtGasMoistTwoStackStandard2C As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackStandard2C As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackStandard2C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackStandard2C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackStandard2C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistTwoStackStandard2B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackStandard2B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackStandard2B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackStandard2C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackStandard2B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackStandard2B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackStandard2B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumTwoStackStandard2B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackStandard2A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackStandard2A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistTwoStackStandard2A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackStandard2A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackStandard2A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackStandard2A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumTwoStackStandard2A As System.Windows.Forms.TextBox
    Friend WithEvents btnClearTwoStackStandard2A As System.Windows.Forms.Button
    Friend WithEvents btnClearTwoStackStandard2B As System.Windows.Forms.Button
    Friend WithEvents txtRunNumTwoStackStandard1C As System.Windows.Forms.TextBox
    Friend WithEvents btnClearTwoStackStandard1C As System.Windows.Forms.Button
    Friend WithEvents txtGasMoistTwoStackStandard1C As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackStandard1C As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackStandard1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackStandard1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackStandard1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistTwoStackStandard1B As System.Windows.Forms.TextBox
    Friend WithEvents txtStackOneNameTwoStackStandard As System.Windows.Forms.TextBox
    Friend WithEvents txtStackTwoNameTwoStackStandard As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateAvgTwoStackStandard1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgTwoStackStandard2 As System.Windows.Forms.TextBox
    Friend WithEvents cboEmissRateUnitTwoStackStandard As System.Windows.Forms.ComboBox
    Friend WithEvents cboPollConUnitTwoStackStandard As System.Windows.Forms.ComboBox
    Friend WithEvents txtEmissRateTwoStackStandard1B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackStandard1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackStandard1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackStandard1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackStandard1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackStandard1B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumTwoStackStandard1B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackStandard1A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackStandard1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistTwoStackStandard1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackStandard1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackStandard1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackStandard1A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumTwoStackStandard1A As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents btnClearTwoStackStandard1A As System.Windows.Forms.Button
    Friend WithEvents btnClearTwoStackStandard1B As System.Windows.Forms.Button
    Friend WithEvents txtPercentAllowableTwoStack As System.Windows.Forms.TextBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents TPTwoStackDRE As System.Windows.Forms.TabPage
    Friend WithEvents Label246 As System.Windows.Forms.Label
    Friend WithEvents Label245 As System.Windows.Forms.Label
    Friend WithEvents Label244 As System.Windows.Forms.Label
    Friend WithEvents Label243 As System.Windows.Forms.Label
    Friend WithEvents Label242 As System.Windows.Forms.Label
    Friend WithEvents Label241 As System.Windows.Forms.Label
    Friend WithEvents Label240 As System.Windows.Forms.Label
    Friend WithEvents Label239 As System.Windows.Forms.Label
    Friend WithEvents Label238 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateAvgTwoStackDRE2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgTwoStackDRE1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDestructionEfficiencyTwoStackDRE As System.Windows.Forms.TextBox
    Friend WithEvents Label172 As System.Windows.Forms.Label
    Friend WithEvents txtRunNumTwoStackDRE2C As System.Windows.Forms.TextBox
    Friend WithEvents btnClearTwoStackDRE2C As System.Windows.Forms.Button
    Friend WithEvents txtGasMoistTwoStackDRE2C As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackDRE2C As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackDRE2C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackDRE2C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackDRE2C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistTwoStackDRE2B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackDRE2B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackDRE2B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackDRE2C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackDRE2B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackDRE2B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackDRE2B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumTwoStackDRE2B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackDRE2A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackDRE2A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistTwoStackDRE2A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackDRE2A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackDRE2A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackDRE2A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumTwoStackDRE2A As System.Windows.Forms.TextBox
    Friend WithEvents btnClearTwoStackDRE2A As System.Windows.Forms.Button
    Friend WithEvents btnClearTwoStackDRE2B As System.Windows.Forms.Button
    Friend WithEvents txtRunNumTwoStackDRE1C As System.Windows.Forms.TextBox
    Friend WithEvents btnClearTwoStackDRE1C As System.Windows.Forms.Button
    Friend WithEvents txtGasMoistTwoStackDRE1C As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackDRE1C As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackDRE1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackDRE1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackDRE1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasMoistTwoStackDRE1B As System.Windows.Forms.TextBox
    Friend WithEvents txtStackTwoNameTwoStackDRE As System.Windows.Forms.TextBox
    Friend WithEvents Label174 As System.Windows.Forms.Label
    Friend WithEvents Label176 As System.Windows.Forms.Label
    Friend WithEvents txtEmissRateAvgTwoStackDRE1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcAvgTwoStackDRE2 As System.Windows.Forms.TextBox
    Friend WithEvents cboEmissRateUnitTwoStackDRE As System.Windows.Forms.ComboBox
    Friend WithEvents cboPollConUnitTwoStackDRE As System.Windows.Forms.ComboBox
    Friend WithEvents txtEmissRateTwoStackDRE1B As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackDRE1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackDRE1C As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackDRE1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackDRE1B As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackDRE1B As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumTwoStackDRE1B As System.Windows.Forms.TextBox
    Friend WithEvents txtEmissRateTwoStackDRE1A As System.Windows.Forms.TextBox
    Friend WithEvents txtPollConcTwoStackDRE1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasTempTwoStackDRE1A As System.Windows.Forms.TextBox
    Friend WithEvents txtRunNumTwoStackDRE1A As System.Windows.Forms.TextBox
    Friend WithEvents Label178 As System.Windows.Forms.Label
    Friend WithEvents Label179 As System.Windows.Forms.Label
    Friend WithEvents Label180 As System.Windows.Forms.Label
    Friend WithEvents Label181 As System.Windows.Forms.Label
    Friend WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents Label184 As System.Windows.Forms.Label
    Friend WithEvents Label186 As System.Windows.Forms.Label
    Friend WithEvents btnClearTwoStackDRE1A As System.Windows.Forms.Button
    Friend WithEvents btnClearTwoStackDRE1B As System.Windows.Forms.Button
    Friend WithEvents txtGasMoistTwoStackDRE1A As System.Windows.Forms.TextBox
    Friend WithEvents txtStackOneNameTwoStackDRE As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowDSCFMTwoStackDRE1A As System.Windows.Forms.TextBox
    Friend WithEvents txtGasFlowACFMTwoStackDRE1A As System.Windows.Forms.TextBox
    Friend WithEvents Label188 As System.Windows.Forms.Label
    Friend WithEvents txtControlEquipmentOperatingDataTwoStack As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicableRegulationTwoStack As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityTwoStack As System.Windows.Forms.TextBox
    Friend WithEvents Label189 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRate2TwoStack As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate3TwoStack As System.Windows.Forms.TextBox
    Friend WithEvents txtAllowableEmissionRate1TwoStack As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityTwoStack As System.Windows.Forms.TextBox
    Friend WithEvents cboOperatingCapacityUnitsTwoStack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits2TwoStack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits3TwoStack As System.Windows.Forms.ComboBox
    Friend WithEvents cboAllowableEmissionRateUnits1TwoStack As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsTwoStack As System.Windows.Forms.ComboBox
    Friend WithEvents Label296 As System.Windows.Forms.Label
    Friend WithEvents Label214 As System.Windows.Forms.Label
    Friend WithEvents Label295 As System.Windows.Forms.Label
    Friend WithEvents Label294 As System.Windows.Forms.Label
    Friend WithEvents Label293 As System.Windows.Forms.Label
    Friend WithEvents Label215 As System.Windows.Forms.Label
    Friend WithEvents txtOtherInformationMethod22 As System.Windows.Forms.TextBox
    Friend WithEvents Label216 As System.Windows.Forms.Label
    Friend WithEvents txtTestDurationMethod22 As System.Windows.Forms.TextBox
    Friend WithEvents txtAccumulatedEmissionMethod22 As System.Windows.Forms.TextBox
    Friend WithEvents Label217 As System.Windows.Forms.Label
    Friend WithEvents Label218 As System.Windows.Forms.Label
    Friend WithEvents txtApplicableRegulationMethod22 As System.Windows.Forms.TextBox
    Friend WithEvents txtOperatingCapacityMethod22 As System.Windows.Forms.TextBox
    Friend WithEvents Label219 As System.Windows.Forms.Label
    Friend WithEvents txtAllowableEmissionRateMethod22 As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximumExpectedOperatingCapacityMethod22 As System.Windows.Forms.TextBox
    Friend WithEvents Label220 As System.Windows.Forms.Label
    Friend WithEvents Label221 As System.Windows.Forms.Label
    Friend WithEvents cboOperatingCapacityUnitsMethod22 As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaximumExpectedOperatingCapacityUnitsMethod22 As System.Windows.Forms.ComboBox
    Friend WithEvents Label222 As System.Windows.Forms.Label
    Friend WithEvents cboStaffResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents Label223 As System.Windows.Forms.Label
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label315 As System.Windows.Forms.Label
    Friend WithEvents chbAcknoledgmentLetterSent As System.Windows.Forms.CheckBox
    Friend WithEvents DTPAcknoledgmentLetterSent As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtEnforcementNumber As System.Windows.Forms.TextBox
    Friend WithEvents chbEventComplete As System.Windows.Forms.CheckBox
    Friend WithEvents DTPEventCompleteDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnEnforcementProcess As System.Windows.Forms.Button
    Friend WithEvents btnSaveSSCPData As System.Windows.Forms.Button
    Friend WithEvents txtTestReportReceivedbySSCPDate As System.Windows.Forms.TextBox
    Friend WithEvents Label320 As System.Windows.Forms.Label
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents rdbTestReportFollowUpYes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTestReportFollowUpNo As System.Windows.Forms.RadioButton
    Friend WithEvents Label321 As System.Windows.Forms.Label
    Friend WithEvents Label322 As System.Windows.Forms.Label
    Friend WithEvents Label323 As System.Windows.Forms.Label
    Friend WithEvents DTPTestReportDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chbTestReportChangeDueDate As System.Windows.Forms.CheckBox
    Friend WithEvents DTPTestReportNewDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label325 As System.Windows.Forms.Label
    Friend WithEvents txtTestReportComments As System.Windows.Forms.TextBox
    Friend WithEvents Label326 As System.Windows.Forms.Label
    Friend WithEvents txtTestReportDueDate As System.Windows.Forms.TextBox
    Friend WithEvents tsbResize As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPrePopulate As System.Windows.Forms.ToolStripButton
    Friend WithEvents llbTestNotifiactionNumber As System.Windows.Forms.LinkLabel
    Friend WithEvents cboTestNotificationNumber As System.Windows.Forms.ComboBox
    Friend WithEvents labTestNotificationNumber As System.Windows.Forms.Label
    Friend WithEvents clbWitnessingEngineers As System.Windows.Forms.CheckedListBox
    Friend WithEvents tsbConfidentialData As System.Windows.Forms.ToolStripButton
    Friend WithEvents mmiDefaultCompliance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtPart75Statement As System.Windows.Forms.TextBox
    Friend WithEvents lblRATAPart75 As System.Windows.Forms.Label
    Friend WithEvents mmiPrintNonConf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtDaysfromTestToAPB As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPreComplianceStatus As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdbMethod9Average30 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbMethod9HighestAvg As System.Windows.Forms.RadioButton
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdbMethod9MultiAverage30 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbMethod9MultiHighestAvg As System.Windows.Forms.RadioButton
    Friend WithEvents lblMemoEntered As System.Windows.Forms.Label
    Friend WithEvents DeletedTestFlag As System.Windows.Forms.Label
    Friend WithEvents cboUnitManager As ComboBox
End Class
