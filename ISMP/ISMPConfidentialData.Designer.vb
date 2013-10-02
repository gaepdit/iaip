<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISMPConfidentialData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPConfidentialData))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Panel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbSave = New System.Windows.Forms.ToolStripButton
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mmiFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiSave = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiBack = New System.Windows.Forms.ToolStripMenuItem
        Me.mmiHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.TCDocuments = New System.Windows.Forms.TabControl
        Me.TPOneStack = New System.Windows.Forms.TabPage
        Me.chbOneStackOtherInfo = New System.Windows.Forms.CheckBox
        Me.chbOneStackPercentAllow = New System.Windows.Forms.CheckBox
        Me.TCOneStack = New System.Windows.Forms.TabControl
        Me.TPTwoRuns = New System.Windows.Forms.TabPage
        Me.chbOneStack2EmissAvg = New System.Windows.Forms.CheckBox
        Me.chbOneStack2PollAvg = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Poll1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Poll2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Emiss1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2EmissUnit = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Emiss2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2DSCFM1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2PollUnit = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Run1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2DSCFM2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2ACFM1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Temp1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Moist1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Temp2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Run2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2ACFM2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack2Moist2 = New System.Windows.Forms.CheckBox
        Me.TPThreeRuns = New System.Windows.Forms.TabPage
        Me.chbOneStack3Poll3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Emiss3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3DSCFM3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Run3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3ACFM3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Temp3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Moist3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Poll2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Emiss2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3DSCFM2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Run2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3ACFM2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Temp2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Moist2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3EmissAvg = New System.Windows.Forms.CheckBox
        Me.chbOneStack3PollAvg = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Poll1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Emiss1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3EmissUnit = New System.Windows.Forms.CheckBox
        Me.chbOneStack3DSCFM1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3PollUnit = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Run1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3ACFM1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Temp1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack3Moist1 = New System.Windows.Forms.CheckBox
        Me.TPFourRuns = New System.Windows.Forms.TabPage
        Me.chbOneStack4Poll4 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Emiss4 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4DSCFM4 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Run4 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4ACFM4 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Temp4 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Moist4 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Poll3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Emiss3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4DSCFM3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Run3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4ACFM3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Temp3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Moist3 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Poll2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Emiss2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4DSCFM2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Run2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4ACFM2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Temp2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Moist2 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4EmissAvg = New System.Windows.Forms.CheckBox
        Me.chbOneStack4PollAvg = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Poll1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Emiss1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4EmissUnit = New System.Windows.Forms.CheckBox
        Me.chbOneStack4DSCFM1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4PollUnit = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Run1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4ACFM1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Temp1 = New System.Windows.Forms.CheckBox
        Me.chbOneStack4Moist1 = New System.Windows.Forms.CheckBox
        Me.chbOneStackAppRequire = New System.Windows.Forms.CheckBox
        Me.chbOneStackAllowEmiss2 = New System.Windows.Forms.CheckBox
        Me.chbOneStackAllowEmiss3 = New System.Windows.Forms.CheckBox
        Me.chbOneStackControlEquip = New System.Windows.Forms.CheckBox
        Me.chbOneStackOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbOneStackMaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbOneStackAllowEmiss1 = New System.Windows.Forms.CheckBox
        Me.TPLoadingRack = New System.Windows.Forms.TabPage
        Me.chbLoadingRackControlEquip = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackEmiss = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackOtherInfo = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackTestDuration = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackDestReduction = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackPollIN = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackPollOUT = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackAppRequire = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackAllowEmiss2 = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackAllowEmiss3 = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackMaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbLoadingRackAllowEmiss1 = New System.Windows.Forms.CheckBox
        Me.TPPulpCondensate = New System.Windows.Forms.TabPage
        Me.chbPulpControlEquip = New System.Windows.Forms.CheckBox
        Me.chbPulpOtherInfo = New System.Windows.Forms.CheckBox
        Me.chbPulpDestructEffic = New System.Windows.Forms.CheckBox
        Me.chbPulpConc3 = New System.Windows.Forms.CheckBox
        Me.chbPulpTreatment3 = New System.Windows.Forms.CheckBox
        Me.chbPulpRun3 = New System.Windows.Forms.CheckBox
        Me.chbPulpConc2 = New System.Windows.Forms.CheckBox
        Me.chbPulpTreatment2 = New System.Windows.Forms.CheckBox
        Me.chbPulpRun2 = New System.Windows.Forms.CheckBox
        Me.chbPulpTreatmentAvg = New System.Windows.Forms.CheckBox
        Me.chbPulpConcAvg = New System.Windows.Forms.CheckBox
        Me.chbPulpConc1 = New System.Windows.Forms.CheckBox
        Me.chbPulpTreatment1 = New System.Windows.Forms.CheckBox
        Me.chbPulpTreatmentUnit = New System.Windows.Forms.CheckBox
        Me.chbPulpConcUnit = New System.Windows.Forms.CheckBox
        Me.chbPulpRun1 = New System.Windows.Forms.CheckBox
        Me.chbPulpAppRequire = New System.Windows.Forms.CheckBox
        Me.chbPulpAllowEmiss2 = New System.Windows.Forms.CheckBox
        Me.chbPulpAllowEmiss3 = New System.Windows.Forms.CheckBox
        Me.chbPulpOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbPulpMaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbPulpAllowEmiss1 = New System.Windows.Forms.CheckBox
        Me.TPGasConcentration = New System.Windows.Forms.TabPage
        Me.chbGasControlEquip = New System.Windows.Forms.CheckBox
        Me.chbGasOtherInfo = New System.Windows.Forms.CheckBox
        Me.chbGasPercentAllow = New System.Windows.Forms.CheckBox
        Me.chbGasPoll3 = New System.Windows.Forms.CheckBox
        Me.chbGasEmiss3 = New System.Windows.Forms.CheckBox
        Me.chbGasRun3 = New System.Windows.Forms.CheckBox
        Me.chbGasPoll2 = New System.Windows.Forms.CheckBox
        Me.chbGasEmiss2 = New System.Windows.Forms.CheckBox
        Me.chbGasRun2 = New System.Windows.Forms.CheckBox
        Me.chbGasEmissAvg = New System.Windows.Forms.CheckBox
        Me.chbGasPollAvg = New System.Windows.Forms.CheckBox
        Me.chbGasPoll1 = New System.Windows.Forms.CheckBox
        Me.chbGasEmiss1 = New System.Windows.Forms.CheckBox
        Me.chbGasEmissUnit = New System.Windows.Forms.CheckBox
        Me.chbGasPollUnit = New System.Windows.Forms.CheckBox
        Me.chbGasRun1 = New System.Windows.Forms.CheckBox
        Me.chbGasAppRequire = New System.Windows.Forms.CheckBox
        Me.chbGasAllowEmiss2 = New System.Windows.Forms.CheckBox
        Me.chbGasAllowEmiss3 = New System.Windows.Forms.CheckBox
        Me.chbGasOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbGasMaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbGasAllowEmiss1 = New System.Windows.Forms.CheckBox
        Me.TPFlare = New System.Windows.Forms.TabPage
        Me.chbFlareOtherInfo = New System.Windows.Forms.CheckBox
        Me.chbFlarePercentAllow = New System.Windows.Forms.CheckBox
        Me.chbFlareHeating3 = New System.Windows.Forms.CheckBox
        Me.chbFlareVelocity3 = New System.Windows.Forms.CheckBox
        Me.chbFlareRun3 = New System.Windows.Forms.CheckBox
        Me.chbFlareHeating2 = New System.Windows.Forms.CheckBox
        Me.chbFlareVelocity2 = New System.Windows.Forms.CheckBox
        Me.chbFlareRun2 = New System.Windows.Forms.CheckBox
        Me.chbFlareVelocityAvg = New System.Windows.Forms.CheckBox
        Me.chbFlareHeatingAvg = New System.Windows.Forms.CheckBox
        Me.chbFlareHeating1 = New System.Windows.Forms.CheckBox
        Me.chbFlareVelocity1 = New System.Windows.Forms.CheckBox
        Me.chbFlareVelocityUnit = New System.Windows.Forms.CheckBox
        Me.chbFlareHeatingUnit = New System.Windows.Forms.CheckBox
        Me.chbFlareRun1 = New System.Windows.Forms.CheckBox
        Me.chbFlareAppRequire = New System.Windows.Forms.CheckBox
        Me.chbFlareHeatContent = New System.Windows.Forms.CheckBox
        Me.chbFlareMonitor = New System.Windows.Forms.CheckBox
        Me.chbFlareOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbFlareMaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbFlareAllowLimitations = New System.Windows.Forms.CheckBox
        Me.TPPEM = New System.Windows.Forms.TabPage
        Me.TPMethod9 = New System.Windows.Forms.TabPage
        Me.TCMethod9 = New System.Windows.Forms.TabControl
        Me.TPMethod9Single = New System.Windows.Forms.TabPage
        Me.chbMethod9ControlEquip = New System.Windows.Forms.CheckBox
        Me.chbMethod9OtherInfo = New System.Windows.Forms.CheckBox
        Me.chbMethod9Opacity = New System.Windows.Forms.CheckBox
        Me.chbMethod9TestDuration = New System.Windows.Forms.CheckBox
        Me.chbMethod9AppRequire = New System.Windows.Forms.CheckBox
        Me.chbMethod9OpCapacity = New System.Windows.Forms.CheckBox
        Me.chbMethod9MaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbMethod9AllowEmiss = New System.Windows.Forms.CheckBox
        Me.TPMethod9Multi = New System.Windows.Forms.TabPage
        Me.chbMethod9MultiAppRequire = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiControlEquip = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiEquip2 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiEquip4 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiEquip5 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiEquip3 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiEquip1 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAvg5 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAvg4 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAvg3 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAvg2 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAllowEmissUnit = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAllowEmiss5 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAllowEmiss4 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAllowEmiss3 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAllowEmiss2 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiOpCapacityUnit = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiOpCapacity2 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiOpCapacity3 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiOpCapacity4 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiOpCapacity5 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiOpCapacity1 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiMaxOpCapacity5 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiMaxOpCapacity4 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiMaxOpCapacity3 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiMaxOpCapacity2 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiOtherInfor = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAvg1 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiMaxOpCapacityUnit = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiMaxOpCapacity1 = New System.Windows.Forms.CheckBox
        Me.chbMethod9MultiAllowEmiss1 = New System.Windows.Forms.CheckBox
        Me.TPMemorandum = New System.Windows.Forms.TabPage
        Me.chbMemoAppRequire = New System.Windows.Forms.CheckBox
        Me.TCMemorandum = New System.Windows.Forms.TabControl
        Me.TPStandard = New System.Windows.Forms.TabPage
        Me.chbMemoStandardMemo = New System.Windows.Forms.CheckBox
        Me.TPToFile = New System.Windows.Forms.TabPage
        Me.chbMemoToFileMemo = New System.Windows.Forms.CheckBox
        Me.chbMemoToFileSerial = New System.Windows.Forms.CheckBox
        Me.chbMemoToFileManufacture = New System.Windows.Forms.CheckBox
        Me.TPPTE = New System.Windows.Forms.TabPage
        Me.chbMemoPTEControlEquip = New System.Windows.Forms.CheckBox
        Me.chbMemoPTEMemo = New System.Windows.Forms.CheckBox
        Me.chbMemoPTEAllowEmiss2 = New System.Windows.Forms.CheckBox
        Me.chbMemoPTEAllowEmiss3 = New System.Windows.Forms.CheckBox
        Me.chbMemoPTEOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbMemoPTEMaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbMemoPTEAllowEmiss1 = New System.Windows.Forms.CheckBox
        Me.TPRATA = New System.Windows.Forms.TabPage
        Me.chbRATAOtherInformation = New System.Windows.Forms.CheckBox
        Me.chbRATACMS9 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS12 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS11 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS10 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS8 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS7 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS5 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS6 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS4 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS3 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS2 = New System.Windows.Forms.CheckBox
        Me.chbRATACMS1 = New System.Windows.Forms.CheckBox
        Me.chbRATARef9 = New System.Windows.Forms.CheckBox
        Me.chbRATARef12 = New System.Windows.Forms.CheckBox
        Me.chbRATARef11 = New System.Windows.Forms.CheckBox
        Me.chbRATARef10 = New System.Windows.Forms.CheckBox
        Me.chbRATARef8 = New System.Windows.Forms.CheckBox
        Me.chbRATARef7 = New System.Windows.Forms.CheckBox
        Me.chbRATARef5 = New System.Windows.Forms.CheckBox
        Me.chbRATARef6 = New System.Windows.Forms.CheckBox
        Me.chbRATARef4 = New System.Windows.Forms.CheckBox
        Me.chbRATARef3 = New System.Windows.Forms.CheckBox
        Me.chbRATARef2 = New System.Windows.Forms.CheckBox
        Me.chbRATARef1 = New System.Windows.Forms.CheckBox
        Me.chbRATARelativeAcc = New System.Windows.Forms.CheckBox
        Me.chbRATAStatement = New System.Windows.Forms.CheckBox
        Me.chbRATAUnits = New System.Windows.Forms.CheckBox
        Me.chbRATADiluent = New System.Windows.Forms.CheckBox
        Me.chbRATAAppStandard = New System.Windows.Forms.CheckBox
        Me.chbRATAAppRegulation = New System.Windows.Forms.CheckBox
        Me.TPTwoStack = New System.Windows.Forms.TabPage
        Me.chbTwoStackControlEquip = New System.Windows.Forms.CheckBox
        Me.chbTwoStackOtherInfo = New System.Windows.Forms.CheckBox
        Me.TCTwoStack = New System.Windows.Forms.TabControl
        Me.TPTwoStackStandard = New System.Windows.Forms.TabPage
        Me.chbTwoStackStandPercentAllow = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTotalAvg = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTotal3 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTotal2 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTotal1 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmissUnit = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPollUnit = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmissAvg2 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmissAvg1 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPollAvg2 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPollAvg1 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPoll3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmiss3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandDSCFM3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandRun3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandACFM3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTemp3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandMoist3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPoll2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmiss2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandDSCFM2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandRun2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandACFM2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTemp2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandMoist2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPoll1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmiss1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandDSCFM1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandRun1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandACFM1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTemp1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandMoist1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPoll3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmiss3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandDSCFM3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandRun3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandACFM3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTemp3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandMoist3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPoll2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmiss2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandDSCFM2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandRun2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandACFM2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTemp2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandMoist2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandName2 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandName1 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandPoll1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandEmiss1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandDSCFM1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandRun1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandACFM1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandTemp1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackStandMoist1a = New System.Windows.Forms.CheckBox
        Me.TPDRE = New System.Windows.Forms.TabPage
        Me.chbTwoStackDREDestructionEff = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmissUnit = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPollUnit = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmissAvg2 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmissAvg1 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPollAvg2 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPollAvg1 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPoll3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmiss3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREDSCFM3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRERun3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREACFM3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRETemp3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREMoist3b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPoll2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmiss2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREDSCFM2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRERun2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREACFM2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRETemp2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREMoist2b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPoll1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmiss1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREDSCFM1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRERun1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREACFM1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRETemp1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREMoist1b = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPoll3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmiss3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREDSCFM3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRERun3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREACFM3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRETEmp3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREMoist3a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPoll2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmiss2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREDSCFM2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRERun2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREACFM2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRETemp2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREMoist2a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREName2 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREName1 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREPoll1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREEmiss1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREDSCFM1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRERun1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREACFM1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDRETemp1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackDREMoist1a = New System.Windows.Forms.CheckBox
        Me.chbTwoStackAppRequire = New System.Windows.Forms.CheckBox
        Me.chbTwoStackAllowEmiss2 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackAllowEmiss3 = New System.Windows.Forms.CheckBox
        Me.chbTwoStackOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbTwoStackMaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbTwoStackAllowEmiss1 = New System.Windows.Forms.CheckBox
        Me.TPMethod22 = New System.Windows.Forms.TabPage
        Me.chbMethod22Emission = New System.Windows.Forms.CheckBox
        Me.chbMethod22OtherInfo = New System.Windows.Forms.CheckBox
        Me.chbMethod22AppReg = New System.Windows.Forms.CheckBox
        Me.chbMethod22TestDuration = New System.Windows.Forms.CheckBox
        Me.chbMethod22OpCapacity = New System.Windows.Forms.CheckBox
        Me.chbMethod22MaxOpCapacity = New System.Windows.Forms.CheckBox
        Me.chbMethod22AllowEmiss = New System.Windows.Forms.CheckBox
        Me.TPSSCPWork = New System.Windows.Forms.TabPage
        Me.chbAIRSNumber = New System.Windows.Forms.CheckBox
        Me.chbFacilityName = New System.Windows.Forms.CheckBox
        Me.chbLocation = New System.Windows.Forms.CheckBox
        Me.chbReportType = New System.Windows.Forms.CheckBox
        Me.chbISMPReviewer = New System.Windows.Forms.CheckBox
        Me.chbISMPUnit = New System.Windows.Forms.CheckBox
        Me.chbISMPProgramManager = New System.Windows.Forms.CheckBox
        Me.chbISMPUnitManager = New System.Windows.Forms.CheckBox
        Me.chbTestNotification = New System.Windows.Forms.CheckBox
        Me.chbWitnessingEngineer = New System.Windows.Forms.CheckBox
        Me.chbOtherWitnessingEngineer = New System.Windows.Forms.CheckBox
        Me.chbCC = New System.Windows.Forms.CheckBox
        Me.chbComplianceManager = New System.Windows.Forms.CheckBox
        Me.chbCompletedByISMP = New System.Windows.Forms.CheckBox
        Me.chbAssignedToEngineer = New System.Windows.Forms.CheckBox
        Me.chbReceivedByAPB = New System.Windows.Forms.CheckBox
        Me.chbDaysInAPB = New System.Windows.Forms.CheckBox
        Me.chbDatesTested = New System.Windows.Forms.CheckBox
        Me.chbISMPComplianceDetermination = New System.Windows.Forms.CheckBox
        Me.chbTestingFirm = New System.Windows.Forms.CheckBox
        Me.chbMethodUsed = New System.Windows.Forms.CheckBox
        Me.chbPollutant = New System.Windows.Forms.CheckBox
        Me.chbSourceTested = New System.Windows.Forms.CheckBox
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TCDocuments.SuspendLayout()
        Me.TPOneStack.SuspendLayout()
        Me.TCOneStack.SuspendLayout()
        Me.TPTwoRuns.SuspendLayout()
        Me.TPThreeRuns.SuspendLayout()
        Me.TPFourRuns.SuspendLayout()
        Me.TPLoadingRack.SuspendLayout()
        Me.TPPulpCondensate.SuspendLayout()
        Me.TPGasConcentration.SuspendLayout()
        Me.TPFlare.SuspendLayout()
        Me.TPMethod9.SuspendLayout()
        Me.TCMethod9.SuspendLayout()
        Me.TPMethod9Single.SuspendLayout()
        Me.TPMethod9Multi.SuspendLayout()
        Me.TPMemorandum.SuspendLayout()
        Me.TCMemorandum.SuspendLayout()
        Me.TPStandard.SuspendLayout()
        Me.TPToFile.SuspendLayout()
        Me.TPPTE.SuspendLayout()
        Me.TPRATA.SuspendLayout()
        Me.TPTwoStack.SuspendLayout()
        Me.TCTwoStack.SuspendLayout()
        Me.TPTwoStackStandard.SuspendLayout()
        Me.TPDRE.SuspendLayout()
        Me.TPMethod22.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Panel1, Me.Panel2, Me.Panel3})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 603)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(794, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(771, 17)
        Me.Panel1.Spring = True
        Me.Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbBack})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(794, 25)
        Me.ToolStrip1.TabIndex = 4
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
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = CType(resources.GetObject("tsbBack.Image"), System.Drawing.Image)
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        Me.tsbBack.Text = "Back"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiFile, Me.mmiHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(794, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mmiFile
        '
        Me.mmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiSave, Me.mmiBack})
        Me.mmiFile.Name = "mmiFile"
        Me.mmiFile.Size = New System.Drawing.Size(37, 20)
        Me.mmiFile.Text = "File"
        '
        'mmiSave
        '
        Me.mmiSave.Name = "mmiSave"
        Me.mmiSave.Size = New System.Drawing.Size(99, 22)
        Me.mmiSave.Text = "Save"
        '
        'mmiBack
        '
        Me.mmiBack.Name = "mmiBack"
        Me.mmiBack.Size = New System.Drawing.Size(99, 22)
        Me.mmiBack.Text = "Back"
        '
        'mmiHelp
        '
        Me.mmiHelp.Name = "mmiHelp"
        Me.mmiHelp.Size = New System.Drawing.Size(44, 20)
        Me.mmiHelp.Text = "Help"
        '
        'TCDocuments
        '
        Me.TCDocuments.Controls.Add(Me.TPOneStack)
        Me.TCDocuments.Controls.Add(Me.TPLoadingRack)
        Me.TCDocuments.Controls.Add(Me.TPPulpCondensate)
        Me.TCDocuments.Controls.Add(Me.TPGasConcentration)
        Me.TCDocuments.Controls.Add(Me.TPFlare)
        Me.TCDocuments.Controls.Add(Me.TPPEM)
        Me.TCDocuments.Controls.Add(Me.TPMethod9)
        Me.TCDocuments.Controls.Add(Me.TPMemorandum)
        Me.TCDocuments.Controls.Add(Me.TPRATA)
        Me.TCDocuments.Controls.Add(Me.TPTwoStack)
        Me.TCDocuments.Controls.Add(Me.TPMethod22)
        Me.TCDocuments.Controls.Add(Me.TPSSCPWork)
        Me.TCDocuments.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TCDocuments.Location = New System.Drawing.Point(0, 262)
        Me.TCDocuments.Name = "TCDocuments"
        Me.TCDocuments.SelectedIndex = 0
        Me.TCDocuments.Size = New System.Drawing.Size(794, 341)
        Me.TCDocuments.TabIndex = 6
        '
        'TPOneStack
        '
        Me.TPOneStack.Controls.Add(Me.chbOneStackOtherInfo)
        Me.TPOneStack.Controls.Add(Me.chbOneStackPercentAllow)
        Me.TPOneStack.Controls.Add(Me.TCOneStack)
        Me.TPOneStack.Controls.Add(Me.chbOneStackAppRequire)
        Me.TPOneStack.Controls.Add(Me.chbOneStackAllowEmiss2)
        Me.TPOneStack.Controls.Add(Me.chbOneStackAllowEmiss3)
        Me.TPOneStack.Controls.Add(Me.chbOneStackControlEquip)
        Me.TPOneStack.Controls.Add(Me.chbOneStackOpCapacity)
        Me.TPOneStack.Controls.Add(Me.chbOneStackMaxOpCapacity)
        Me.TPOneStack.Controls.Add(Me.chbOneStackAllowEmiss1)
        Me.TPOneStack.Location = New System.Drawing.Point(4, 22)
        Me.TPOneStack.Name = "TPOneStack"
        Me.TPOneStack.Padding = New System.Windows.Forms.Padding(3)
        Me.TPOneStack.Size = New System.Drawing.Size(786, 315)
        Me.TPOneStack.TabIndex = 0
        Me.TPOneStack.Text = "One Stack"
        Me.TPOneStack.UseVisualStyleBackColor = True
        '
        'chbOneStackOtherInfo
        '
        Me.chbOneStackOtherInfo.AutoSize = True
        Me.chbOneStackOtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackOtherInfo.Location = New System.Drawing.Point(11, 271)
        Me.chbOneStackOtherInfo.Name = "chbOneStackOtherInfo"
        Me.chbOneStackOtherInfo.Size = New System.Drawing.Size(107, 17)
        Me.chbOneStackOtherInfo.TabIndex = 11
        Me.chbOneStackOtherInfo.Text = "Other Information"
        Me.chbOneStackOtherInfo.UseVisualStyleBackColor = True
        '
        'chbOneStackPercentAllow
        '
        Me.chbOneStackPercentAllow.AutoSize = True
        Me.chbOneStackPercentAllow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackPercentAllow.Location = New System.Drawing.Point(7, 254)
        Me.chbOneStackPercentAllow.Name = "chbOneStackPercentAllow"
        Me.chbOneStackPercentAllow.Size = New System.Drawing.Size(111, 17)
        Me.chbOneStackPercentAllow.TabIndex = 10
        Me.chbOneStackPercentAllow.Text = "Percent Allowable"
        Me.chbOneStackPercentAllow.UseVisualStyleBackColor = True
        '
        'TCOneStack
        '
        Me.TCOneStack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TCOneStack.Controls.Add(Me.TPTwoRuns)
        Me.TCOneStack.Controls.Add(Me.TPThreeRuns)
        Me.TCOneStack.Controls.Add(Me.TPFourRuns)
        Me.TCOneStack.Location = New System.Drawing.Point(0, 93)
        Me.TCOneStack.Name = "TCOneStack"
        Me.TCOneStack.SelectedIndex = 0
        Me.TCOneStack.Size = New System.Drawing.Size(784, 155)
        Me.TCOneStack.TabIndex = 9
        '
        'TPTwoRuns
        '
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2EmissAvg)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2PollAvg)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Poll1)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Poll2)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Emiss1)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2EmissUnit)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Emiss2)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2DSCFM1)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2PollUnit)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Run1)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2DSCFM2)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2ACFM1)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Temp1)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Moist1)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Temp2)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Run2)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2ACFM2)
        Me.TPTwoRuns.Controls.Add(Me.chbOneStack2Moist2)
        Me.TPTwoRuns.Location = New System.Drawing.Point(4, 22)
        Me.TPTwoRuns.Name = "TPTwoRuns"
        Me.TPTwoRuns.Padding = New System.Windows.Forms.Padding(3)
        Me.TPTwoRuns.Size = New System.Drawing.Size(776, 129)
        Me.TPTwoRuns.TabIndex = 0
        Me.TPTwoRuns.Text = "2 Runs"
        Me.TPTwoRuns.UseVisualStyleBackColor = True
        '
        'chbOneStack2EmissAvg
        '
        Me.chbOneStack2EmissAvg.AutoSize = True
        Me.chbOneStack2EmissAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2EmissAvg.Location = New System.Drawing.Point(293, 107)
        Me.chbOneStack2EmissAvg.Name = "chbOneStack2EmissAvg"
        Me.chbOneStack2EmissAvg.Size = New System.Drawing.Size(122, 17)
        Me.chbOneStack2EmissAvg.TabIndex = 19
        Me.chbOneStack2EmissAvg.Text = "Emiss Rate Average"
        Me.chbOneStack2EmissAvg.UseVisualStyleBackColor = True
        '
        'chbOneStack2PollAvg
        '
        Me.chbOneStack2PollAvg.AutoSize = True
        Me.chbOneStack2PollAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2PollAvg.Location = New System.Drawing.Point(301, 90)
        Me.chbOneStack2PollAvg.Name = "chbOneStack2PollAvg"
        Me.chbOneStack2PollAvg.Size = New System.Drawing.Size(114, 17)
        Me.chbOneStack2PollAvg.TabIndex = 18
        Me.chbOneStack2PollAvg.Text = "Poll Conc Average"
        Me.chbOneStack2PollAvg.UseVisualStyleBackColor = True
        '
        'chbOneStack2Poll1
        '
        Me.chbOneStack2Poll1.AutoSize = True
        Me.chbOneStack2Poll1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Poll1.Location = New System.Drawing.Point(10, 90)
        Me.chbOneStack2Poll1.Name = "chbOneStack2Poll1"
        Me.chbOneStack2Poll1.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack2Poll1.TabIndex = 17
        Me.chbOneStack2Poll1.Text = "Poll Conc 1"
        Me.chbOneStack2Poll1.UseVisualStyleBackColor = True
        '
        'chbOneStack2Poll2
        '
        Me.chbOneStack2Poll2.AutoSize = True
        Me.chbOneStack2Poll2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Poll2.Location = New System.Drawing.Point(100, 90)
        Me.chbOneStack2Poll2.Name = "chbOneStack2Poll2"
        Me.chbOneStack2Poll2.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack2Poll2.TabIndex = 16
        Me.chbOneStack2Poll2.Text = "Poll Conc 2"
        Me.chbOneStack2Poll2.UseVisualStyleBackColor = True
        '
        'chbOneStack2Emiss1
        '
        Me.chbOneStack2Emiss1.AutoSize = True
        Me.chbOneStack2Emiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Emiss1.Location = New System.Drawing.Point(2, 107)
        Me.chbOneStack2Emiss1.Name = "chbOneStack2Emiss1"
        Me.chbOneStack2Emiss1.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack2Emiss1.TabIndex = 15
        Me.chbOneStack2Emiss1.Text = "Emiss Rate 1"
        Me.chbOneStack2Emiss1.UseVisualStyleBackColor = True
        '
        'chbOneStack2EmissUnit
        '
        Me.chbOneStack2EmissUnit.AutoSize = True
        Me.chbOneStack2EmissUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2EmissUnit.Location = New System.Drawing.Point(186, 107)
        Me.chbOneStack2EmissUnit.Name = "chbOneStack2EmissUnit"
        Me.chbOneStack2EmissUnit.Size = New System.Drawing.Size(101, 17)
        Me.chbOneStack2EmissUnit.TabIndex = 14
        Me.chbOneStack2EmissUnit.Text = "Emiss Rate Unit"
        Me.chbOneStack2EmissUnit.UseVisualStyleBackColor = True
        '
        'chbOneStack2Emiss2
        '
        Me.chbOneStack2Emiss2.AutoSize = True
        Me.chbOneStack2Emiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Emiss2.Location = New System.Drawing.Point(92, 107)
        Me.chbOneStack2Emiss2.Name = "chbOneStack2Emiss2"
        Me.chbOneStack2Emiss2.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack2Emiss2.TabIndex = 11
        Me.chbOneStack2Emiss2.Text = "Emiss Rate 2"
        Me.chbOneStack2Emiss2.UseVisualStyleBackColor = True
        '
        'chbOneStack2DSCFM1
        '
        Me.chbOneStack2DSCFM1.AutoSize = True
        Me.chbOneStack2DSCFM1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2DSCFM1.Location = New System.Drawing.Point(12, 73)
        Me.chbOneStack2DSCFM1.Name = "chbOneStack2DSCFM1"
        Me.chbOneStack2DSCFM1.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack2DSCFM1.TabIndex = 13
        Me.chbOneStack2DSCFM1.Text = "(DSCFM) 1"
        Me.chbOneStack2DSCFM1.UseVisualStyleBackColor = True
        '
        'chbOneStack2PollUnit
        '
        Me.chbOneStack2PollUnit.AutoSize = True
        Me.chbOneStack2PollUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2PollUnit.Location = New System.Drawing.Point(194, 90)
        Me.chbOneStack2PollUnit.Name = "chbOneStack2PollUnit"
        Me.chbOneStack2PollUnit.Size = New System.Drawing.Size(93, 17)
        Me.chbOneStack2PollUnit.TabIndex = 10
        Me.chbOneStack2PollUnit.Text = "Poll Conc Unit"
        Me.chbOneStack2PollUnit.UseVisualStyleBackColor = True
        '
        'chbOneStack2Run1
        '
        Me.chbOneStack2Run1.AutoSize = True
        Me.chbOneStack2Run1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Run1.Location = New System.Drawing.Point(35, 5)
        Me.chbOneStack2Run1.Name = "chbOneStack2Run1"
        Me.chbOneStack2Run1.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack2Run1.TabIndex = 2
        Me.chbOneStack2Run1.Text = "Run 1"
        Me.chbOneStack2Run1.UseVisualStyleBackColor = True
        '
        'chbOneStack2DSCFM2
        '
        Me.chbOneStack2DSCFM2.AutoSize = True
        Me.chbOneStack2DSCFM2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2DSCFM2.Location = New System.Drawing.Point(102, 73)
        Me.chbOneStack2DSCFM2.Name = "chbOneStack2DSCFM2"
        Me.chbOneStack2DSCFM2.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack2DSCFM2.TabIndex = 12
        Me.chbOneStack2DSCFM2.Text = "(DSCFM) 2"
        Me.chbOneStack2DSCFM2.UseVisualStyleBackColor = True
        '
        'chbOneStack2ACFM1
        '
        Me.chbOneStack2ACFM1.AutoSize = True
        Me.chbOneStack2ACFM1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2ACFM1.Location = New System.Drawing.Point(20, 56)
        Me.chbOneStack2ACFM1.Name = "chbOneStack2ACFM1"
        Me.chbOneStack2ACFM1.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack2ACFM1.TabIndex = 4
        Me.chbOneStack2ACFM1.Text = "(ACFM) 1"
        Me.chbOneStack2ACFM1.UseVisualStyleBackColor = True
        '
        'chbOneStack2Temp1
        '
        Me.chbOneStack2Temp1.AutoSize = True
        Me.chbOneStack2Temp1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Temp1.Location = New System.Drawing.Point(6, 22)
        Me.chbOneStack2Temp1.Name = "chbOneStack2Temp1"
        Me.chbOneStack2Temp1.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack2Temp1.TabIndex = 7
        Me.chbOneStack2Temp1.Text = "Gas Temp 1"
        Me.chbOneStack2Temp1.UseVisualStyleBackColor = True
        '
        'chbOneStack2Moist1
        '
        Me.chbOneStack2Moist1.AutoSize = True
        Me.chbOneStack2Moist1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Moist1.Location = New System.Drawing.Point(8, 39)
        Me.chbOneStack2Moist1.Name = "chbOneStack2Moist1"
        Me.chbOneStack2Moist1.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack2Moist1.TabIndex = 6
        Me.chbOneStack2Moist1.Text = "Gas Moist 1"
        Me.chbOneStack2Moist1.UseVisualStyleBackColor = True
        '
        'chbOneStack2Temp2
        '
        Me.chbOneStack2Temp2.AutoSize = True
        Me.chbOneStack2Temp2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Temp2.Location = New System.Drawing.Point(96, 22)
        Me.chbOneStack2Temp2.Name = "chbOneStack2Temp2"
        Me.chbOneStack2Temp2.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack2Temp2.TabIndex = 8
        Me.chbOneStack2Temp2.Text = "Gas Temp 2"
        Me.chbOneStack2Temp2.UseVisualStyleBackColor = True
        '
        'chbOneStack2Run2
        '
        Me.chbOneStack2Run2.AutoSize = True
        Me.chbOneStack2Run2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Run2.Location = New System.Drawing.Point(125, 5)
        Me.chbOneStack2Run2.Name = "chbOneStack2Run2"
        Me.chbOneStack2Run2.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack2Run2.TabIndex = 3
        Me.chbOneStack2Run2.Text = "Run 2"
        Me.chbOneStack2Run2.UseVisualStyleBackColor = True
        '
        'chbOneStack2ACFM2
        '
        Me.chbOneStack2ACFM2.AutoSize = True
        Me.chbOneStack2ACFM2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2ACFM2.Location = New System.Drawing.Point(110, 56)
        Me.chbOneStack2ACFM2.Name = "chbOneStack2ACFM2"
        Me.chbOneStack2ACFM2.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack2ACFM2.TabIndex = 9
        Me.chbOneStack2ACFM2.Text = "(ACFM) 2"
        Me.chbOneStack2ACFM2.UseVisualStyleBackColor = True
        '
        'chbOneStack2Moist2
        '
        Me.chbOneStack2Moist2.AutoSize = True
        Me.chbOneStack2Moist2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack2Moist2.Location = New System.Drawing.Point(98, 39)
        Me.chbOneStack2Moist2.Name = "chbOneStack2Moist2"
        Me.chbOneStack2Moist2.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack2Moist2.TabIndex = 5
        Me.chbOneStack2Moist2.Text = "Gas Moist 2"
        Me.chbOneStack2Moist2.UseVisualStyleBackColor = True
        '
        'TPThreeRuns
        '
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Poll3)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Emiss3)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3DSCFM3)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Run3)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3ACFM3)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Temp3)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Moist3)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Poll2)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Emiss2)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3DSCFM2)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Run2)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3ACFM2)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Temp2)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Moist2)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3EmissAvg)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3PollAvg)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Poll1)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Emiss1)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3EmissUnit)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3DSCFM1)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3PollUnit)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Run1)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3ACFM1)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Temp1)
        Me.TPThreeRuns.Controls.Add(Me.chbOneStack3Moist1)
        Me.TPThreeRuns.Location = New System.Drawing.Point(4, 22)
        Me.TPThreeRuns.Name = "TPThreeRuns"
        Me.TPThreeRuns.Padding = New System.Windows.Forms.Padding(3)
        Me.TPThreeRuns.Size = New System.Drawing.Size(776, 129)
        Me.TPThreeRuns.TabIndex = 1
        Me.TPThreeRuns.Text = "3 Runs"
        Me.TPThreeRuns.UseVisualStyleBackColor = True
        '
        'chbOneStack3Poll3
        '
        Me.chbOneStack3Poll3.AutoSize = True
        Me.chbOneStack3Poll3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Poll3.Location = New System.Drawing.Point(193, 90)
        Me.chbOneStack3Poll3.Name = "chbOneStack3Poll3"
        Me.chbOneStack3Poll3.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack3Poll3.TabIndex = 51
        Me.chbOneStack3Poll3.Text = "Poll Conc 3"
        Me.chbOneStack3Poll3.UseVisualStyleBackColor = True
        '
        'chbOneStack3Emiss3
        '
        Me.chbOneStack3Emiss3.AutoSize = True
        Me.chbOneStack3Emiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Emiss3.Location = New System.Drawing.Point(185, 107)
        Me.chbOneStack3Emiss3.Name = "chbOneStack3Emiss3"
        Me.chbOneStack3Emiss3.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack3Emiss3.TabIndex = 50
        Me.chbOneStack3Emiss3.Text = "Emiss Rate 3"
        Me.chbOneStack3Emiss3.UseVisualStyleBackColor = True
        '
        'chbOneStack3DSCFM3
        '
        Me.chbOneStack3DSCFM3.AutoSize = True
        Me.chbOneStack3DSCFM3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3DSCFM3.Location = New System.Drawing.Point(195, 73)
        Me.chbOneStack3DSCFM3.Name = "chbOneStack3DSCFM3"
        Me.chbOneStack3DSCFM3.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack3DSCFM3.TabIndex = 49
        Me.chbOneStack3DSCFM3.Text = "(DSCFM) 3"
        Me.chbOneStack3DSCFM3.UseVisualStyleBackColor = True
        '
        'chbOneStack3Run3
        '
        Me.chbOneStack3Run3.AutoSize = True
        Me.chbOneStack3Run3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Run3.Location = New System.Drawing.Point(218, 5)
        Me.chbOneStack3Run3.Name = "chbOneStack3Run3"
        Me.chbOneStack3Run3.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack3Run3.TabIndex = 45
        Me.chbOneStack3Run3.Text = "Run 3"
        Me.chbOneStack3Run3.UseVisualStyleBackColor = True
        '
        'chbOneStack3ACFM3
        '
        Me.chbOneStack3ACFM3.AutoSize = True
        Me.chbOneStack3ACFM3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3ACFM3.Location = New System.Drawing.Point(203, 56)
        Me.chbOneStack3ACFM3.Name = "chbOneStack3ACFM3"
        Me.chbOneStack3ACFM3.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack3ACFM3.TabIndex = 46
        Me.chbOneStack3ACFM3.Text = "(ACFM) 3"
        Me.chbOneStack3ACFM3.UseVisualStyleBackColor = True
        '
        'chbOneStack3Temp3
        '
        Me.chbOneStack3Temp3.AutoSize = True
        Me.chbOneStack3Temp3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Temp3.Location = New System.Drawing.Point(189, 22)
        Me.chbOneStack3Temp3.Name = "chbOneStack3Temp3"
        Me.chbOneStack3Temp3.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack3Temp3.TabIndex = 48
        Me.chbOneStack3Temp3.Text = "Gas Temp 3"
        Me.chbOneStack3Temp3.UseVisualStyleBackColor = True
        '
        'chbOneStack3Moist3
        '
        Me.chbOneStack3Moist3.AutoSize = True
        Me.chbOneStack3Moist3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Moist3.Location = New System.Drawing.Point(191, 39)
        Me.chbOneStack3Moist3.Name = "chbOneStack3Moist3"
        Me.chbOneStack3Moist3.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack3Moist3.TabIndex = 47
        Me.chbOneStack3Moist3.Text = "Gas Moist 3"
        Me.chbOneStack3Moist3.UseVisualStyleBackColor = True
        '
        'chbOneStack3Poll2
        '
        Me.chbOneStack3Poll2.AutoSize = True
        Me.chbOneStack3Poll2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Poll2.Location = New System.Drawing.Point(100, 90)
        Me.chbOneStack3Poll2.Name = "chbOneStack3Poll2"
        Me.chbOneStack3Poll2.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack3Poll2.TabIndex = 44
        Me.chbOneStack3Poll2.Text = "Poll Conc 2"
        Me.chbOneStack3Poll2.UseVisualStyleBackColor = True
        '
        'chbOneStack3Emiss2
        '
        Me.chbOneStack3Emiss2.AutoSize = True
        Me.chbOneStack3Emiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Emiss2.Location = New System.Drawing.Point(92, 107)
        Me.chbOneStack3Emiss2.Name = "chbOneStack3Emiss2"
        Me.chbOneStack3Emiss2.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack3Emiss2.TabIndex = 43
        Me.chbOneStack3Emiss2.Text = "Emiss Rate 2"
        Me.chbOneStack3Emiss2.UseVisualStyleBackColor = True
        '
        'chbOneStack3DSCFM2
        '
        Me.chbOneStack3DSCFM2.AutoSize = True
        Me.chbOneStack3DSCFM2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3DSCFM2.Location = New System.Drawing.Point(102, 73)
        Me.chbOneStack3DSCFM2.Name = "chbOneStack3DSCFM2"
        Me.chbOneStack3DSCFM2.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack3DSCFM2.TabIndex = 42
        Me.chbOneStack3DSCFM2.Text = "(DSCFM) 2"
        Me.chbOneStack3DSCFM2.UseVisualStyleBackColor = True
        '
        'chbOneStack3Run2
        '
        Me.chbOneStack3Run2.AutoSize = True
        Me.chbOneStack3Run2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Run2.Location = New System.Drawing.Point(125, 5)
        Me.chbOneStack3Run2.Name = "chbOneStack3Run2"
        Me.chbOneStack3Run2.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack3Run2.TabIndex = 38
        Me.chbOneStack3Run2.Text = "Run 2"
        Me.chbOneStack3Run2.UseVisualStyleBackColor = True
        '
        'chbOneStack3ACFM2
        '
        Me.chbOneStack3ACFM2.AutoSize = True
        Me.chbOneStack3ACFM2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3ACFM2.Location = New System.Drawing.Point(110, 56)
        Me.chbOneStack3ACFM2.Name = "chbOneStack3ACFM2"
        Me.chbOneStack3ACFM2.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack3ACFM2.TabIndex = 39
        Me.chbOneStack3ACFM2.Text = "(ACFM) 2"
        Me.chbOneStack3ACFM2.UseVisualStyleBackColor = True
        '
        'chbOneStack3Temp2
        '
        Me.chbOneStack3Temp2.AutoSize = True
        Me.chbOneStack3Temp2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Temp2.Location = New System.Drawing.Point(96, 22)
        Me.chbOneStack3Temp2.Name = "chbOneStack3Temp2"
        Me.chbOneStack3Temp2.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack3Temp2.TabIndex = 41
        Me.chbOneStack3Temp2.Text = "Gas Temp 2"
        Me.chbOneStack3Temp2.UseVisualStyleBackColor = True
        '
        'chbOneStack3Moist2
        '
        Me.chbOneStack3Moist2.AutoSize = True
        Me.chbOneStack3Moist2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Moist2.Location = New System.Drawing.Point(98, 39)
        Me.chbOneStack3Moist2.Name = "chbOneStack3Moist2"
        Me.chbOneStack3Moist2.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack3Moist2.TabIndex = 40
        Me.chbOneStack3Moist2.Text = "Gas Moist 2"
        Me.chbOneStack3Moist2.UseVisualStyleBackColor = True
        '
        'chbOneStack3EmissAvg
        '
        Me.chbOneStack3EmissAvg.AutoSize = True
        Me.chbOneStack3EmissAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3EmissAvg.Location = New System.Drawing.Point(386, 107)
        Me.chbOneStack3EmissAvg.Name = "chbOneStack3EmissAvg"
        Me.chbOneStack3EmissAvg.Size = New System.Drawing.Size(122, 17)
        Me.chbOneStack3EmissAvg.TabIndex = 37
        Me.chbOneStack3EmissAvg.Text = "Emiss Rate Average"
        Me.chbOneStack3EmissAvg.UseVisualStyleBackColor = True
        '
        'chbOneStack3PollAvg
        '
        Me.chbOneStack3PollAvg.AutoSize = True
        Me.chbOneStack3PollAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3PollAvg.Location = New System.Drawing.Point(394, 90)
        Me.chbOneStack3PollAvg.Name = "chbOneStack3PollAvg"
        Me.chbOneStack3PollAvg.Size = New System.Drawing.Size(114, 17)
        Me.chbOneStack3PollAvg.TabIndex = 36
        Me.chbOneStack3PollAvg.Text = "Poll Conc Average"
        Me.chbOneStack3PollAvg.UseVisualStyleBackColor = True
        '
        'chbOneStack3Poll1
        '
        Me.chbOneStack3Poll1.AutoSize = True
        Me.chbOneStack3Poll1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Poll1.Location = New System.Drawing.Point(10, 90)
        Me.chbOneStack3Poll1.Name = "chbOneStack3Poll1"
        Me.chbOneStack3Poll1.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack3Poll1.TabIndex = 35
        Me.chbOneStack3Poll1.Text = "Poll Conc 1"
        Me.chbOneStack3Poll1.UseVisualStyleBackColor = True
        '
        'chbOneStack3Emiss1
        '
        Me.chbOneStack3Emiss1.AutoSize = True
        Me.chbOneStack3Emiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Emiss1.Location = New System.Drawing.Point(2, 107)
        Me.chbOneStack3Emiss1.Name = "chbOneStack3Emiss1"
        Me.chbOneStack3Emiss1.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack3Emiss1.TabIndex = 33
        Me.chbOneStack3Emiss1.Text = "Emiss Rate 1"
        Me.chbOneStack3Emiss1.UseVisualStyleBackColor = True
        '
        'chbOneStack3EmissUnit
        '
        Me.chbOneStack3EmissUnit.AutoSize = True
        Me.chbOneStack3EmissUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3EmissUnit.Location = New System.Drawing.Point(279, 107)
        Me.chbOneStack3EmissUnit.Name = "chbOneStack3EmissUnit"
        Me.chbOneStack3EmissUnit.Size = New System.Drawing.Size(101, 17)
        Me.chbOneStack3EmissUnit.TabIndex = 32
        Me.chbOneStack3EmissUnit.Text = "Emiss Rate Unit"
        Me.chbOneStack3EmissUnit.UseVisualStyleBackColor = True
        '
        'chbOneStack3DSCFM1
        '
        Me.chbOneStack3DSCFM1.AutoSize = True
        Me.chbOneStack3DSCFM1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3DSCFM1.Location = New System.Drawing.Point(12, 73)
        Me.chbOneStack3DSCFM1.Name = "chbOneStack3DSCFM1"
        Me.chbOneStack3DSCFM1.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack3DSCFM1.TabIndex = 31
        Me.chbOneStack3DSCFM1.Text = "(DSCFM) 1"
        Me.chbOneStack3DSCFM1.UseVisualStyleBackColor = True
        '
        'chbOneStack3PollUnit
        '
        Me.chbOneStack3PollUnit.AutoSize = True
        Me.chbOneStack3PollUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3PollUnit.Location = New System.Drawing.Point(287, 90)
        Me.chbOneStack3PollUnit.Name = "chbOneStack3PollUnit"
        Me.chbOneStack3PollUnit.Size = New System.Drawing.Size(93, 17)
        Me.chbOneStack3PollUnit.TabIndex = 28
        Me.chbOneStack3PollUnit.Text = "Poll Conc Unit"
        Me.chbOneStack3PollUnit.UseVisualStyleBackColor = True
        '
        'chbOneStack3Run1
        '
        Me.chbOneStack3Run1.AutoSize = True
        Me.chbOneStack3Run1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Run1.Location = New System.Drawing.Point(35, 5)
        Me.chbOneStack3Run1.Name = "chbOneStack3Run1"
        Me.chbOneStack3Run1.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack3Run1.TabIndex = 20
        Me.chbOneStack3Run1.Text = "Run 1"
        Me.chbOneStack3Run1.UseVisualStyleBackColor = True
        '
        'chbOneStack3ACFM1
        '
        Me.chbOneStack3ACFM1.AutoSize = True
        Me.chbOneStack3ACFM1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3ACFM1.Location = New System.Drawing.Point(20, 56)
        Me.chbOneStack3ACFM1.Name = "chbOneStack3ACFM1"
        Me.chbOneStack3ACFM1.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack3ACFM1.TabIndex = 22
        Me.chbOneStack3ACFM1.Text = "(ACFM) 1"
        Me.chbOneStack3ACFM1.UseVisualStyleBackColor = True
        '
        'chbOneStack3Temp1
        '
        Me.chbOneStack3Temp1.AutoSize = True
        Me.chbOneStack3Temp1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Temp1.Location = New System.Drawing.Point(6, 22)
        Me.chbOneStack3Temp1.Name = "chbOneStack3Temp1"
        Me.chbOneStack3Temp1.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack3Temp1.TabIndex = 25
        Me.chbOneStack3Temp1.Text = "Gas Temp 1"
        Me.chbOneStack3Temp1.UseVisualStyleBackColor = True
        '
        'chbOneStack3Moist1
        '
        Me.chbOneStack3Moist1.AutoSize = True
        Me.chbOneStack3Moist1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack3Moist1.Location = New System.Drawing.Point(8, 39)
        Me.chbOneStack3Moist1.Name = "chbOneStack3Moist1"
        Me.chbOneStack3Moist1.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack3Moist1.TabIndex = 24
        Me.chbOneStack3Moist1.Text = "Gas Moist 1"
        Me.chbOneStack3Moist1.UseVisualStyleBackColor = True
        '
        'TPFourRuns
        '
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Poll4)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Emiss4)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4DSCFM4)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Run4)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4ACFM4)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Temp4)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Moist4)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Poll3)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Emiss3)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4DSCFM3)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Run3)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4ACFM3)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Temp3)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Moist3)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Poll2)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Emiss2)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4DSCFM2)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Run2)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4ACFM2)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Temp2)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Moist2)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4EmissAvg)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4PollAvg)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Poll1)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Emiss1)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4EmissUnit)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4DSCFM1)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4PollUnit)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Run1)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4ACFM1)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Temp1)
        Me.TPFourRuns.Controls.Add(Me.chbOneStack4Moist1)
        Me.TPFourRuns.Location = New System.Drawing.Point(4, 22)
        Me.TPFourRuns.Name = "TPFourRuns"
        Me.TPFourRuns.Size = New System.Drawing.Size(776, 129)
        Me.TPFourRuns.TabIndex = 2
        Me.TPFourRuns.Text = "4 Runs"
        Me.TPFourRuns.UseVisualStyleBackColor = True
        '
        'chbOneStack4Poll4
        '
        Me.chbOneStack4Poll4.AutoSize = True
        Me.chbOneStack4Poll4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Poll4.Location = New System.Drawing.Point(283, 90)
        Me.chbOneStack4Poll4.Name = "chbOneStack4Poll4"
        Me.chbOneStack4Poll4.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack4Poll4.TabIndex = 83
        Me.chbOneStack4Poll4.Text = "Poll Conc 4"
        Me.chbOneStack4Poll4.UseVisualStyleBackColor = True
        '
        'chbOneStack4Emiss4
        '
        Me.chbOneStack4Emiss4.AutoSize = True
        Me.chbOneStack4Emiss4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Emiss4.Location = New System.Drawing.Point(275, 107)
        Me.chbOneStack4Emiss4.Name = "chbOneStack4Emiss4"
        Me.chbOneStack4Emiss4.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack4Emiss4.TabIndex = 82
        Me.chbOneStack4Emiss4.Text = "Emiss Rate 4"
        Me.chbOneStack4Emiss4.UseVisualStyleBackColor = True
        '
        'chbOneStack4DSCFM4
        '
        Me.chbOneStack4DSCFM4.AutoSize = True
        Me.chbOneStack4DSCFM4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4DSCFM4.Location = New System.Drawing.Point(285, 73)
        Me.chbOneStack4DSCFM4.Name = "chbOneStack4DSCFM4"
        Me.chbOneStack4DSCFM4.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack4DSCFM4.TabIndex = 81
        Me.chbOneStack4DSCFM4.Text = "(DSCFM) 4"
        Me.chbOneStack4DSCFM4.UseVisualStyleBackColor = True
        '
        'chbOneStack4Run4
        '
        Me.chbOneStack4Run4.AutoSize = True
        Me.chbOneStack4Run4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Run4.Location = New System.Drawing.Point(308, 5)
        Me.chbOneStack4Run4.Name = "chbOneStack4Run4"
        Me.chbOneStack4Run4.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack4Run4.TabIndex = 77
        Me.chbOneStack4Run4.Text = "Run 4"
        Me.chbOneStack4Run4.UseVisualStyleBackColor = True
        '
        'chbOneStack4ACFM4
        '
        Me.chbOneStack4ACFM4.AutoSize = True
        Me.chbOneStack4ACFM4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4ACFM4.Location = New System.Drawing.Point(293, 56)
        Me.chbOneStack4ACFM4.Name = "chbOneStack4ACFM4"
        Me.chbOneStack4ACFM4.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack4ACFM4.TabIndex = 78
        Me.chbOneStack4ACFM4.Text = "(ACFM) 4"
        Me.chbOneStack4ACFM4.UseVisualStyleBackColor = True
        '
        'chbOneStack4Temp4
        '
        Me.chbOneStack4Temp4.AutoSize = True
        Me.chbOneStack4Temp4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Temp4.Location = New System.Drawing.Point(279, 22)
        Me.chbOneStack4Temp4.Name = "chbOneStack4Temp4"
        Me.chbOneStack4Temp4.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack4Temp4.TabIndex = 80
        Me.chbOneStack4Temp4.Text = "Gas Temp 4"
        Me.chbOneStack4Temp4.UseVisualStyleBackColor = True
        '
        'chbOneStack4Moist4
        '
        Me.chbOneStack4Moist4.AutoSize = True
        Me.chbOneStack4Moist4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Moist4.Location = New System.Drawing.Point(281, 39)
        Me.chbOneStack4Moist4.Name = "chbOneStack4Moist4"
        Me.chbOneStack4Moist4.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack4Moist4.TabIndex = 79
        Me.chbOneStack4Moist4.Text = "Gas Moist 4"
        Me.chbOneStack4Moist4.UseVisualStyleBackColor = True
        '
        'chbOneStack4Poll3
        '
        Me.chbOneStack4Poll3.AutoSize = True
        Me.chbOneStack4Poll3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Poll3.Location = New System.Drawing.Point(193, 90)
        Me.chbOneStack4Poll3.Name = "chbOneStack4Poll3"
        Me.chbOneStack4Poll3.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack4Poll3.TabIndex = 76
        Me.chbOneStack4Poll3.Text = "Poll Conc 3"
        Me.chbOneStack4Poll3.UseVisualStyleBackColor = True
        '
        'chbOneStack4Emiss3
        '
        Me.chbOneStack4Emiss3.AutoSize = True
        Me.chbOneStack4Emiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Emiss3.Location = New System.Drawing.Point(185, 107)
        Me.chbOneStack4Emiss3.Name = "chbOneStack4Emiss3"
        Me.chbOneStack4Emiss3.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack4Emiss3.TabIndex = 75
        Me.chbOneStack4Emiss3.Text = "Emiss Rate 3"
        Me.chbOneStack4Emiss3.UseVisualStyleBackColor = True
        '
        'chbOneStack4DSCFM3
        '
        Me.chbOneStack4DSCFM3.AutoSize = True
        Me.chbOneStack4DSCFM3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4DSCFM3.Location = New System.Drawing.Point(195, 73)
        Me.chbOneStack4DSCFM3.Name = "chbOneStack4DSCFM3"
        Me.chbOneStack4DSCFM3.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack4DSCFM3.TabIndex = 74
        Me.chbOneStack4DSCFM3.Text = "(DSCFM) 3"
        Me.chbOneStack4DSCFM3.UseVisualStyleBackColor = True
        '
        'chbOneStack4Run3
        '
        Me.chbOneStack4Run3.AutoSize = True
        Me.chbOneStack4Run3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Run3.Location = New System.Drawing.Point(218, 5)
        Me.chbOneStack4Run3.Name = "chbOneStack4Run3"
        Me.chbOneStack4Run3.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack4Run3.TabIndex = 70
        Me.chbOneStack4Run3.Text = "Run 3"
        Me.chbOneStack4Run3.UseVisualStyleBackColor = True
        '
        'chbOneStack4ACFM3
        '
        Me.chbOneStack4ACFM3.AutoSize = True
        Me.chbOneStack4ACFM3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4ACFM3.Location = New System.Drawing.Point(203, 56)
        Me.chbOneStack4ACFM3.Name = "chbOneStack4ACFM3"
        Me.chbOneStack4ACFM3.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack4ACFM3.TabIndex = 71
        Me.chbOneStack4ACFM3.Text = "(ACFM) 3"
        Me.chbOneStack4ACFM3.UseVisualStyleBackColor = True
        '
        'chbOneStack4Temp3
        '
        Me.chbOneStack4Temp3.AutoSize = True
        Me.chbOneStack4Temp3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Temp3.Location = New System.Drawing.Point(189, 22)
        Me.chbOneStack4Temp3.Name = "chbOneStack4Temp3"
        Me.chbOneStack4Temp3.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack4Temp3.TabIndex = 73
        Me.chbOneStack4Temp3.Text = "Gas Temp 3"
        Me.chbOneStack4Temp3.UseVisualStyleBackColor = True
        '
        'chbOneStack4Moist3
        '
        Me.chbOneStack4Moist3.AutoSize = True
        Me.chbOneStack4Moist3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Moist3.Location = New System.Drawing.Point(191, 39)
        Me.chbOneStack4Moist3.Name = "chbOneStack4Moist3"
        Me.chbOneStack4Moist3.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack4Moist3.TabIndex = 72
        Me.chbOneStack4Moist3.Text = "Gas Moist 3"
        Me.chbOneStack4Moist3.UseVisualStyleBackColor = True
        '
        'chbOneStack4Poll2
        '
        Me.chbOneStack4Poll2.AutoSize = True
        Me.chbOneStack4Poll2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Poll2.Location = New System.Drawing.Point(100, 90)
        Me.chbOneStack4Poll2.Name = "chbOneStack4Poll2"
        Me.chbOneStack4Poll2.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack4Poll2.TabIndex = 69
        Me.chbOneStack4Poll2.Text = "Poll Conc 2"
        Me.chbOneStack4Poll2.UseVisualStyleBackColor = True
        '
        'chbOneStack4Emiss2
        '
        Me.chbOneStack4Emiss2.AutoSize = True
        Me.chbOneStack4Emiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Emiss2.Location = New System.Drawing.Point(92, 107)
        Me.chbOneStack4Emiss2.Name = "chbOneStack4Emiss2"
        Me.chbOneStack4Emiss2.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack4Emiss2.TabIndex = 68
        Me.chbOneStack4Emiss2.Text = "Emiss Rate 2"
        Me.chbOneStack4Emiss2.UseVisualStyleBackColor = True
        '
        'chbOneStack4DSCFM2
        '
        Me.chbOneStack4DSCFM2.AutoSize = True
        Me.chbOneStack4DSCFM2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4DSCFM2.Location = New System.Drawing.Point(102, 73)
        Me.chbOneStack4DSCFM2.Name = "chbOneStack4DSCFM2"
        Me.chbOneStack4DSCFM2.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack4DSCFM2.TabIndex = 67
        Me.chbOneStack4DSCFM2.Text = "(DSCFM) 2"
        Me.chbOneStack4DSCFM2.UseVisualStyleBackColor = True
        '
        'chbOneStack4Run2
        '
        Me.chbOneStack4Run2.AutoSize = True
        Me.chbOneStack4Run2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Run2.Location = New System.Drawing.Point(125, 5)
        Me.chbOneStack4Run2.Name = "chbOneStack4Run2"
        Me.chbOneStack4Run2.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack4Run2.TabIndex = 63
        Me.chbOneStack4Run2.Text = "Run 2"
        Me.chbOneStack4Run2.UseVisualStyleBackColor = True
        '
        'chbOneStack4ACFM2
        '
        Me.chbOneStack4ACFM2.AutoSize = True
        Me.chbOneStack4ACFM2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4ACFM2.Location = New System.Drawing.Point(110, 56)
        Me.chbOneStack4ACFM2.Name = "chbOneStack4ACFM2"
        Me.chbOneStack4ACFM2.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack4ACFM2.TabIndex = 64
        Me.chbOneStack4ACFM2.Text = "(ACFM) 2"
        Me.chbOneStack4ACFM2.UseVisualStyleBackColor = True
        '
        'chbOneStack4Temp2
        '
        Me.chbOneStack4Temp2.AutoSize = True
        Me.chbOneStack4Temp2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Temp2.Location = New System.Drawing.Point(96, 22)
        Me.chbOneStack4Temp2.Name = "chbOneStack4Temp2"
        Me.chbOneStack4Temp2.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack4Temp2.TabIndex = 66
        Me.chbOneStack4Temp2.Text = "Gas Temp 2"
        Me.chbOneStack4Temp2.UseVisualStyleBackColor = True
        '
        'chbOneStack4Moist2
        '
        Me.chbOneStack4Moist2.AutoSize = True
        Me.chbOneStack4Moist2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Moist2.Location = New System.Drawing.Point(98, 39)
        Me.chbOneStack4Moist2.Name = "chbOneStack4Moist2"
        Me.chbOneStack4Moist2.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack4Moist2.TabIndex = 65
        Me.chbOneStack4Moist2.Text = "Gas Moist 2"
        Me.chbOneStack4Moist2.UseVisualStyleBackColor = True
        '
        'chbOneStack4EmissAvg
        '
        Me.chbOneStack4EmissAvg.AutoSize = True
        Me.chbOneStack4EmissAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4EmissAvg.Location = New System.Drawing.Point(476, 107)
        Me.chbOneStack4EmissAvg.Name = "chbOneStack4EmissAvg"
        Me.chbOneStack4EmissAvg.Size = New System.Drawing.Size(122, 17)
        Me.chbOneStack4EmissAvg.TabIndex = 62
        Me.chbOneStack4EmissAvg.Text = "Emiss Rate Average"
        Me.chbOneStack4EmissAvg.UseVisualStyleBackColor = True
        '
        'chbOneStack4PollAvg
        '
        Me.chbOneStack4PollAvg.AutoSize = True
        Me.chbOneStack4PollAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4PollAvg.Location = New System.Drawing.Point(484, 90)
        Me.chbOneStack4PollAvg.Name = "chbOneStack4PollAvg"
        Me.chbOneStack4PollAvg.Size = New System.Drawing.Size(114, 17)
        Me.chbOneStack4PollAvg.TabIndex = 61
        Me.chbOneStack4PollAvg.Text = "Poll Conc Average"
        Me.chbOneStack4PollAvg.UseVisualStyleBackColor = True
        '
        'chbOneStack4Poll1
        '
        Me.chbOneStack4Poll1.AutoSize = True
        Me.chbOneStack4Poll1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Poll1.Location = New System.Drawing.Point(10, 90)
        Me.chbOneStack4Poll1.Name = "chbOneStack4Poll1"
        Me.chbOneStack4Poll1.Size = New System.Drawing.Size(80, 17)
        Me.chbOneStack4Poll1.TabIndex = 60
        Me.chbOneStack4Poll1.Text = "Poll Conc 1"
        Me.chbOneStack4Poll1.UseVisualStyleBackColor = True
        '
        'chbOneStack4Emiss1
        '
        Me.chbOneStack4Emiss1.AutoSize = True
        Me.chbOneStack4Emiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Emiss1.Location = New System.Drawing.Point(2, 107)
        Me.chbOneStack4Emiss1.Name = "chbOneStack4Emiss1"
        Me.chbOneStack4Emiss1.Size = New System.Drawing.Size(88, 17)
        Me.chbOneStack4Emiss1.TabIndex = 59
        Me.chbOneStack4Emiss1.Text = "Emiss Rate 1"
        Me.chbOneStack4Emiss1.UseVisualStyleBackColor = True
        '
        'chbOneStack4EmissUnit
        '
        Me.chbOneStack4EmissUnit.AutoSize = True
        Me.chbOneStack4EmissUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4EmissUnit.Location = New System.Drawing.Point(369, 107)
        Me.chbOneStack4EmissUnit.Name = "chbOneStack4EmissUnit"
        Me.chbOneStack4EmissUnit.Size = New System.Drawing.Size(101, 17)
        Me.chbOneStack4EmissUnit.TabIndex = 58
        Me.chbOneStack4EmissUnit.Text = "Emiss Rate Unit"
        Me.chbOneStack4EmissUnit.UseVisualStyleBackColor = True
        '
        'chbOneStack4DSCFM1
        '
        Me.chbOneStack4DSCFM1.AutoSize = True
        Me.chbOneStack4DSCFM1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4DSCFM1.Location = New System.Drawing.Point(12, 73)
        Me.chbOneStack4DSCFM1.Name = "chbOneStack4DSCFM1"
        Me.chbOneStack4DSCFM1.Size = New System.Drawing.Size(78, 17)
        Me.chbOneStack4DSCFM1.TabIndex = 57
        Me.chbOneStack4DSCFM1.Text = "(DSCFM) 1"
        Me.chbOneStack4DSCFM1.UseVisualStyleBackColor = True
        '
        'chbOneStack4PollUnit
        '
        Me.chbOneStack4PollUnit.AutoSize = True
        Me.chbOneStack4PollUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4PollUnit.Location = New System.Drawing.Point(377, 90)
        Me.chbOneStack4PollUnit.Name = "chbOneStack4PollUnit"
        Me.chbOneStack4PollUnit.Size = New System.Drawing.Size(93, 17)
        Me.chbOneStack4PollUnit.TabIndex = 56
        Me.chbOneStack4PollUnit.Text = "Poll Conc Unit"
        Me.chbOneStack4PollUnit.UseVisualStyleBackColor = True
        '
        'chbOneStack4Run1
        '
        Me.chbOneStack4Run1.AutoSize = True
        Me.chbOneStack4Run1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Run1.Location = New System.Drawing.Point(35, 5)
        Me.chbOneStack4Run1.Name = "chbOneStack4Run1"
        Me.chbOneStack4Run1.Size = New System.Drawing.Size(55, 17)
        Me.chbOneStack4Run1.TabIndex = 52
        Me.chbOneStack4Run1.Text = "Run 1"
        Me.chbOneStack4Run1.UseVisualStyleBackColor = True
        '
        'chbOneStack4ACFM1
        '
        Me.chbOneStack4ACFM1.AutoSize = True
        Me.chbOneStack4ACFM1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4ACFM1.Location = New System.Drawing.Point(20, 56)
        Me.chbOneStack4ACFM1.Name = "chbOneStack4ACFM1"
        Me.chbOneStack4ACFM1.Size = New System.Drawing.Size(70, 17)
        Me.chbOneStack4ACFM1.TabIndex = 53
        Me.chbOneStack4ACFM1.Text = "(ACFM) 1"
        Me.chbOneStack4ACFM1.UseVisualStyleBackColor = True
        '
        'chbOneStack4Temp1
        '
        Me.chbOneStack4Temp1.AutoSize = True
        Me.chbOneStack4Temp1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Temp1.Location = New System.Drawing.Point(6, 22)
        Me.chbOneStack4Temp1.Name = "chbOneStack4Temp1"
        Me.chbOneStack4Temp1.Size = New System.Drawing.Size(84, 17)
        Me.chbOneStack4Temp1.TabIndex = 55
        Me.chbOneStack4Temp1.Text = "Gas Temp 1"
        Me.chbOneStack4Temp1.UseVisualStyleBackColor = True
        '
        'chbOneStack4Moist1
        '
        Me.chbOneStack4Moist1.AutoSize = True
        Me.chbOneStack4Moist1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStack4Moist1.Location = New System.Drawing.Point(8, 39)
        Me.chbOneStack4Moist1.Name = "chbOneStack4Moist1"
        Me.chbOneStack4Moist1.Size = New System.Drawing.Size(82, 17)
        Me.chbOneStack4Moist1.TabIndex = 54
        Me.chbOneStack4Moist1.Text = "Gas Moist 1"
        Me.chbOneStack4Moist1.UseVisualStyleBackColor = True
        '
        'chbOneStackAppRequire
        '
        Me.chbOneStackAppRequire.AutoSize = True
        Me.chbOneStackAppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackAppRequire.Location = New System.Drawing.Point(30, 39)
        Me.chbOneStackAppRequire.Name = "chbOneStackAppRequire"
        Me.chbOneStackAppRequire.Size = New System.Drawing.Size(138, 17)
        Me.chbOneStackAppRequire.TabIndex = 8
        Me.chbOneStackAppRequire.Text = "Applicable Requirement"
        Me.chbOneStackAppRequire.UseVisualStyleBackColor = True
        '
        'chbOneStackAllowEmiss2
        '
        Me.chbOneStackAllowEmiss2.AutoSize = True
        Me.chbOneStackAllowEmiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackAllowEmiss2.Location = New System.Drawing.Point(175, 22)
        Me.chbOneStackAllowEmiss2.Name = "chbOneStackAllowEmiss2"
        Me.chbOneStackAllowEmiss2.Size = New System.Drawing.Size(150, 17)
        Me.chbOneStackAllowEmiss2.TabIndex = 5
        Me.chbOneStackAllowEmiss2.Text = "Allowable Emission Rate 2"
        Me.chbOneStackAllowEmiss2.UseVisualStyleBackColor = True
        '
        'chbOneStackAllowEmiss3
        '
        Me.chbOneStackAllowEmiss3.AutoSize = True
        Me.chbOneStackAllowEmiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackAllowEmiss3.Location = New System.Drawing.Point(331, 22)
        Me.chbOneStackAllowEmiss3.Name = "chbOneStackAllowEmiss3"
        Me.chbOneStackAllowEmiss3.Size = New System.Drawing.Size(150, 17)
        Me.chbOneStackAllowEmiss3.TabIndex = 4
        Me.chbOneStackAllowEmiss3.Text = "Allowable Emission Rate 3"
        Me.chbOneStackAllowEmiss3.UseVisualStyleBackColor = True
        '
        'chbOneStackControlEquip
        '
        Me.chbOneStackControlEquip.AutoSize = True
        Me.chbOneStackControlEquip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackControlEquip.Location = New System.Drawing.Point(40, 57)
        Me.chbOneStackControlEquip.Name = "chbOneStackControlEquip"
        Me.chbOneStackControlEquip.Size = New System.Drawing.Size(128, 30)
        Me.chbOneStackControlEquip.TabIndex = 7
        Me.chbOneStackControlEquip.Text = "Control Equipment " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  and Monitoring Data"
        Me.chbOneStackControlEquip.UseVisualStyleBackColor = True
        '
        'chbOneStackOpCapacity
        '
        Me.chbOneStackOpCapacity.AutoSize = True
        Me.chbOneStackOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackOpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbOneStackOpCapacity.Name = "chbOneStackOpCapacity"
        Me.chbOneStackOpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbOneStackOpCapacity.TabIndex = 3
        Me.chbOneStackOpCapacity.Text = "Operating Capacity"
        Me.chbOneStackOpCapacity.UseVisualStyleBackColor = True
        '
        'chbOneStackMaxOpCapacity
        '
        Me.chbOneStackMaxOpCapacity.AutoSize = True
        Me.chbOneStackMaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackMaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbOneStackMaxOpCapacity.Name = "chbOneStackMaxOpCapacity"
        Me.chbOneStackMaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbOneStackMaxOpCapacity.TabIndex = 2
        Me.chbOneStackMaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbOneStackMaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbOneStackAllowEmiss1
        '
        Me.chbOneStackAllowEmiss1.AutoSize = True
        Me.chbOneStackAllowEmiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOneStackAllowEmiss1.Location = New System.Drawing.Point(15, 22)
        Me.chbOneStackAllowEmiss1.Name = "chbOneStackAllowEmiss1"
        Me.chbOneStackAllowEmiss1.Size = New System.Drawing.Size(153, 17)
        Me.chbOneStackAllowEmiss1.TabIndex = 6
        Me.chbOneStackAllowEmiss1.Text = "Allowable Emission Rate 1 "
        Me.chbOneStackAllowEmiss1.UseVisualStyleBackColor = True
        '
        'TPLoadingRack
        '
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackControlEquip)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackEmiss)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackOtherInfo)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackTestDuration)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackDestReduction)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackPollIN)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackPollOUT)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackAppRequire)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackAllowEmiss2)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackAllowEmiss3)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackOpCapacity)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackMaxOpCapacity)
        Me.TPLoadingRack.Controls.Add(Me.chbLoadingRackAllowEmiss1)
        Me.TPLoadingRack.Location = New System.Drawing.Point(4, 22)
        Me.TPLoadingRack.Name = "TPLoadingRack"
        Me.TPLoadingRack.Padding = New System.Windows.Forms.Padding(3)
        Me.TPLoadingRack.Size = New System.Drawing.Size(786, 315)
        Me.TPLoadingRack.TabIndex = 1
        Me.TPLoadingRack.Text = "Loading Rack"
        Me.TPLoadingRack.UseVisualStyleBackColor = True
        '
        'chbLoadingRackControlEquip
        '
        Me.chbLoadingRackControlEquip.AutoSize = True
        Me.chbLoadingRackControlEquip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackControlEquip.Location = New System.Drawing.Point(40, 57)
        Me.chbLoadingRackControlEquip.Name = "chbLoadingRackControlEquip"
        Me.chbLoadingRackControlEquip.Size = New System.Drawing.Size(128, 30)
        Me.chbLoadingRackControlEquip.TabIndex = 64
        Me.chbLoadingRackControlEquip.Text = "Control Equipment " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  and Monitoring Data"
        Me.chbLoadingRackControlEquip.UseVisualStyleBackColor = True
        '
        'chbLoadingRackEmiss
        '
        Me.chbLoadingRackEmiss.AutoSize = True
        Me.chbLoadingRackEmiss.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackEmiss.Location = New System.Drawing.Point(232, 144)
        Me.chbLoadingRackEmiss.Name = "chbLoadingRackEmiss"
        Me.chbLoadingRackEmiss.Size = New System.Drawing.Size(93, 17)
        Me.chbLoadingRackEmiss.TabIndex = 63
        Me.chbLoadingRackEmiss.Text = "Emission Rate"
        Me.chbLoadingRackEmiss.UseVisualStyleBackColor = True
        '
        'chbLoadingRackOtherInfo
        '
        Me.chbLoadingRackOtherInfo.AutoSize = True
        Me.chbLoadingRackOtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackOtherInfo.Location = New System.Drawing.Point(61, 161)
        Me.chbLoadingRackOtherInfo.Name = "chbLoadingRackOtherInfo"
        Me.chbLoadingRackOtherInfo.Size = New System.Drawing.Size(107, 17)
        Me.chbLoadingRackOtherInfo.TabIndex = 62
        Me.chbLoadingRackOtherInfo.Text = "Other Information"
        Me.chbLoadingRackOtherInfo.UseVisualStyleBackColor = True
        '
        'chbLoadingRackTestDuration
        '
        Me.chbLoadingRackTestDuration.AutoSize = True
        Me.chbLoadingRackTestDuration.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackTestDuration.Location = New System.Drawing.Point(75, 93)
        Me.chbLoadingRackTestDuration.Name = "chbLoadingRackTestDuration"
        Me.chbLoadingRackTestDuration.Size = New System.Drawing.Size(93, 17)
        Me.chbLoadingRackTestDuration.TabIndex = 58
        Me.chbLoadingRackTestDuration.Text = "Test Duration "
        Me.chbLoadingRackTestDuration.UseVisualStyleBackColor = True
        '
        'chbLoadingRackDestReduction
        '
        Me.chbLoadingRackDestReduction.AutoSize = True
        Me.chbLoadingRackDestReduction.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackDestReduction.Location = New System.Drawing.Point(36, 144)
        Me.chbLoadingRackDestReduction.Name = "chbLoadingRackDestReduction"
        Me.chbLoadingRackDestReduction.Size = New System.Drawing.Size(132, 17)
        Me.chbLoadingRackDestReduction.TabIndex = 59
        Me.chbLoadingRackDestReduction.Text = "Destruction Reduction"
        Me.chbLoadingRackDestReduction.UseVisualStyleBackColor = True
        '
        'chbLoadingRackPollIN
        '
        Me.chbLoadingRackPollIN.AutoSize = True
        Me.chbLoadingRackPollIN.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackPollIN.Location = New System.Drawing.Point(83, 110)
        Me.chbLoadingRackPollIN.Name = "chbLoadingRackPollIN"
        Me.chbLoadingRackPollIN.Size = New System.Drawing.Size(85, 17)
        Me.chbLoadingRackPollIN.TabIndex = 61
        Me.chbLoadingRackPollIN.Text = "Poll Conc IN"
        Me.chbLoadingRackPollIN.UseVisualStyleBackColor = True
        '
        'chbLoadingRackPollOUT
        '
        Me.chbLoadingRackPollOUT.AutoSize = True
        Me.chbLoadingRackPollOUT.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackPollOUT.Location = New System.Drawing.Point(71, 127)
        Me.chbLoadingRackPollOUT.Name = "chbLoadingRackPollOUT"
        Me.chbLoadingRackPollOUT.Size = New System.Drawing.Size(97, 17)
        Me.chbLoadingRackPollOUT.TabIndex = 60
        Me.chbLoadingRackPollOUT.Text = "Poll Conc OUT"
        Me.chbLoadingRackPollOUT.UseVisualStyleBackColor = True
        '
        'chbLoadingRackAppRequire
        '
        Me.chbLoadingRackAppRequire.AutoSize = True
        Me.chbLoadingRackAppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackAppRequire.Location = New System.Drawing.Point(30, 39)
        Me.chbLoadingRackAppRequire.Name = "chbLoadingRackAppRequire"
        Me.chbLoadingRackAppRequire.Size = New System.Drawing.Size(138, 17)
        Me.chbLoadingRackAppRequire.TabIndex = 15
        Me.chbLoadingRackAppRequire.Text = "Applicable Requirement"
        Me.chbLoadingRackAppRequire.UseVisualStyleBackColor = True
        '
        'chbLoadingRackAllowEmiss2
        '
        Me.chbLoadingRackAllowEmiss2.AutoSize = True
        Me.chbLoadingRackAllowEmiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackAllowEmiss2.Location = New System.Drawing.Point(175, 22)
        Me.chbLoadingRackAllowEmiss2.Name = "chbLoadingRackAllowEmiss2"
        Me.chbLoadingRackAllowEmiss2.Size = New System.Drawing.Size(150, 17)
        Me.chbLoadingRackAllowEmiss2.TabIndex = 12
        Me.chbLoadingRackAllowEmiss2.Text = "Allowable Emission Rate 2"
        Me.chbLoadingRackAllowEmiss2.UseVisualStyleBackColor = True
        '
        'chbLoadingRackAllowEmiss3
        '
        Me.chbLoadingRackAllowEmiss3.AutoSize = True
        Me.chbLoadingRackAllowEmiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackAllowEmiss3.Location = New System.Drawing.Point(331, 22)
        Me.chbLoadingRackAllowEmiss3.Name = "chbLoadingRackAllowEmiss3"
        Me.chbLoadingRackAllowEmiss3.Size = New System.Drawing.Size(150, 17)
        Me.chbLoadingRackAllowEmiss3.TabIndex = 11
        Me.chbLoadingRackAllowEmiss3.Text = "Allowable Emission Rate 3"
        Me.chbLoadingRackAllowEmiss3.UseVisualStyleBackColor = True
        '
        'chbLoadingRackOpCapacity
        '
        Me.chbLoadingRackOpCapacity.AutoSize = True
        Me.chbLoadingRackOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackOpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbLoadingRackOpCapacity.Name = "chbLoadingRackOpCapacity"
        Me.chbLoadingRackOpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbLoadingRackOpCapacity.TabIndex = 10
        Me.chbLoadingRackOpCapacity.Text = "Operating Capacity"
        Me.chbLoadingRackOpCapacity.UseVisualStyleBackColor = True
        '
        'chbLoadingRackMaxOpCapacity
        '
        Me.chbLoadingRackMaxOpCapacity.AutoSize = True
        Me.chbLoadingRackMaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackMaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbLoadingRackMaxOpCapacity.Name = "chbLoadingRackMaxOpCapacity"
        Me.chbLoadingRackMaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbLoadingRackMaxOpCapacity.TabIndex = 9
        Me.chbLoadingRackMaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbLoadingRackMaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbLoadingRackAllowEmiss1
        '
        Me.chbLoadingRackAllowEmiss1.AutoSize = True
        Me.chbLoadingRackAllowEmiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLoadingRackAllowEmiss1.Location = New System.Drawing.Point(15, 22)
        Me.chbLoadingRackAllowEmiss1.Name = "chbLoadingRackAllowEmiss1"
        Me.chbLoadingRackAllowEmiss1.Size = New System.Drawing.Size(153, 17)
        Me.chbLoadingRackAllowEmiss1.TabIndex = 13
        Me.chbLoadingRackAllowEmiss1.Text = "Allowable Emission Rate 1 "
        Me.chbLoadingRackAllowEmiss1.UseVisualStyleBackColor = True
        '
        'TPPulpCondensate
        '
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpControlEquip)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpOtherInfo)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpDestructEffic)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpConc3)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpTreatment3)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpRun3)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpConc2)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpTreatment2)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpRun2)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpTreatmentAvg)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpConcAvg)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpConc1)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpTreatment1)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpTreatmentUnit)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpConcUnit)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpRun1)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpAppRequire)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpAllowEmiss2)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpAllowEmiss3)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpOpCapacity)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpMaxOpCapacity)
        Me.TPPulpCondensate.Controls.Add(Me.chbPulpAllowEmiss1)
        Me.TPPulpCondensate.Location = New System.Drawing.Point(4, 22)
        Me.TPPulpCondensate.Name = "TPPulpCondensate"
        Me.TPPulpCondensate.Size = New System.Drawing.Size(786, 315)
        Me.TPPulpCondensate.TabIndex = 2
        Me.TPPulpCondensate.Text = "Pulping Process Condenstate"
        Me.TPPulpCondensate.UseVisualStyleBackColor = True
        '
        'chbPulpControlEquip
        '
        Me.chbPulpControlEquip.AutoSize = True
        Me.chbPulpControlEquip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpControlEquip.Location = New System.Drawing.Point(40, 57)
        Me.chbPulpControlEquip.Name = "chbPulpControlEquip"
        Me.chbPulpControlEquip.Size = New System.Drawing.Size(128, 30)
        Me.chbPulpControlEquip.TabIndex = 79
        Me.chbPulpControlEquip.Text = "Control Equipment " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  and Monitoring Data"
        Me.chbPulpControlEquip.UseVisualStyleBackColor = True
        '
        'chbPulpOtherInfo
        '
        Me.chbPulpOtherInfo.AutoSize = True
        Me.chbPulpOtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpOtherInfo.Location = New System.Drawing.Point(61, 161)
        Me.chbPulpOtherInfo.Name = "chbPulpOtherInfo"
        Me.chbPulpOtherInfo.Size = New System.Drawing.Size(107, 17)
        Me.chbPulpOtherInfo.TabIndex = 78
        Me.chbPulpOtherInfo.Text = "Other Information"
        Me.chbPulpOtherInfo.UseVisualStyleBackColor = True
        '
        'chbPulpDestructEffic
        '
        Me.chbPulpDestructEffic.AutoSize = True
        Me.chbPulpDestructEffic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpDestructEffic.Location = New System.Drawing.Point(41, 144)
        Me.chbPulpDestructEffic.Name = "chbPulpDestructEffic"
        Me.chbPulpDestructEffic.Size = New System.Drawing.Size(127, 17)
        Me.chbPulpDestructEffic.TabIndex = 77
        Me.chbPulpDestructEffic.Text = "Destruction Efficancy"
        Me.chbPulpDestructEffic.UseVisualStyleBackColor = True
        '
        'chbPulpConc3
        '
        Me.chbPulpConc3.AutoSize = True
        Me.chbPulpConc3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpConc3.Location = New System.Drawing.Point(318, 110)
        Me.chbPulpConc3.Name = "chbPulpConc3"
        Me.chbPulpConc3.Size = New System.Drawing.Size(80, 17)
        Me.chbPulpConc3.TabIndex = 76
        Me.chbPulpConc3.Text = "Poll Conc 3"
        Me.chbPulpConc3.UseVisualStyleBackColor = True
        '
        'chbPulpTreatment3
        '
        Me.chbPulpTreatment3.AutoSize = True
        Me.chbPulpTreatment3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpTreatment3.Location = New System.Drawing.Point(289, 127)
        Me.chbPulpTreatment3.Name = "chbPulpTreatment3"
        Me.chbPulpTreatment3.Size = New System.Drawing.Size(109, 17)
        Me.chbPulpTreatment3.TabIndex = 75
        Me.chbPulpTreatment3.Text = "Treatment Rate 3"
        Me.chbPulpTreatment3.UseVisualStyleBackColor = True
        '
        'chbPulpRun3
        '
        Me.chbPulpRun3.AutoSize = True
        Me.chbPulpRun3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpRun3.Location = New System.Drawing.Point(343, 93)
        Me.chbPulpRun3.Name = "chbPulpRun3"
        Me.chbPulpRun3.Size = New System.Drawing.Size(55, 17)
        Me.chbPulpRun3.TabIndex = 70
        Me.chbPulpRun3.Text = "Run 3"
        Me.chbPulpRun3.UseVisualStyleBackColor = True
        '
        'chbPulpConc2
        '
        Me.chbPulpConc2.AutoSize = True
        Me.chbPulpConc2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpConc2.Location = New System.Drawing.Point(203, 110)
        Me.chbPulpConc2.Name = "chbPulpConc2"
        Me.chbPulpConc2.Size = New System.Drawing.Size(80, 17)
        Me.chbPulpConc2.TabIndex = 69
        Me.chbPulpConc2.Text = "Poll Conc 2"
        Me.chbPulpConc2.UseVisualStyleBackColor = True
        '
        'chbPulpTreatment2
        '
        Me.chbPulpTreatment2.AutoSize = True
        Me.chbPulpTreatment2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpTreatment2.Location = New System.Drawing.Point(174, 127)
        Me.chbPulpTreatment2.Name = "chbPulpTreatment2"
        Me.chbPulpTreatment2.Size = New System.Drawing.Size(109, 17)
        Me.chbPulpTreatment2.TabIndex = 68
        Me.chbPulpTreatment2.Text = "Treatment Rate 2"
        Me.chbPulpTreatment2.UseVisualStyleBackColor = True
        '
        'chbPulpRun2
        '
        Me.chbPulpRun2.AutoSize = True
        Me.chbPulpRun2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpRun2.Location = New System.Drawing.Point(228, 93)
        Me.chbPulpRun2.Name = "chbPulpRun2"
        Me.chbPulpRun2.Size = New System.Drawing.Size(55, 17)
        Me.chbPulpRun2.TabIndex = 63
        Me.chbPulpRun2.Text = "Run 2"
        Me.chbPulpRun2.UseVisualStyleBackColor = True
        '
        'chbPulpTreatmentAvg
        '
        Me.chbPulpTreatmentAvg.AutoSize = True
        Me.chbPulpTreatmentAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpTreatmentAvg.Location = New System.Drawing.Point(532, 127)
        Me.chbPulpTreatmentAvg.Name = "chbPulpTreatmentAvg"
        Me.chbPulpTreatmentAvg.Size = New System.Drawing.Size(143, 17)
        Me.chbPulpTreatmentAvg.TabIndex = 62
        Me.chbPulpTreatmentAvg.Text = "Treatment Rate Average"
        Me.chbPulpTreatmentAvg.UseVisualStyleBackColor = True
        '
        'chbPulpConcAvg
        '
        Me.chbPulpConcAvg.AutoSize = True
        Me.chbPulpConcAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpConcAvg.Location = New System.Drawing.Point(561, 110)
        Me.chbPulpConcAvg.Name = "chbPulpConcAvg"
        Me.chbPulpConcAvg.Size = New System.Drawing.Size(114, 17)
        Me.chbPulpConcAvg.TabIndex = 61
        Me.chbPulpConcAvg.Text = "Poll Conc Average"
        Me.chbPulpConcAvg.UseVisualStyleBackColor = True
        '
        'chbPulpConc1
        '
        Me.chbPulpConc1.AutoSize = True
        Me.chbPulpConc1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpConc1.Location = New System.Drawing.Point(88, 110)
        Me.chbPulpConc1.Name = "chbPulpConc1"
        Me.chbPulpConc1.Size = New System.Drawing.Size(80, 17)
        Me.chbPulpConc1.TabIndex = 60
        Me.chbPulpConc1.Text = "Poll Conc 1"
        Me.chbPulpConc1.UseVisualStyleBackColor = True
        '
        'chbPulpTreatment1
        '
        Me.chbPulpTreatment1.AutoSize = True
        Me.chbPulpTreatment1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpTreatment1.Location = New System.Drawing.Point(59, 127)
        Me.chbPulpTreatment1.Name = "chbPulpTreatment1"
        Me.chbPulpTreatment1.Size = New System.Drawing.Size(109, 17)
        Me.chbPulpTreatment1.TabIndex = 59
        Me.chbPulpTreatment1.Text = "Treatment Rate 1"
        Me.chbPulpTreatment1.UseVisualStyleBackColor = True
        '
        'chbPulpTreatmentUnit
        '
        Me.chbPulpTreatmentUnit.AutoSize = True
        Me.chbPulpTreatmentUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpTreatmentUnit.Location = New System.Drawing.Point(404, 127)
        Me.chbPulpTreatmentUnit.Name = "chbPulpTreatmentUnit"
        Me.chbPulpTreatmentUnit.Size = New System.Drawing.Size(122, 17)
        Me.chbPulpTreatmentUnit.TabIndex = 58
        Me.chbPulpTreatmentUnit.Text = "Treatment Rate Unit"
        Me.chbPulpTreatmentUnit.UseVisualStyleBackColor = True
        '
        'chbPulpConcUnit
        '
        Me.chbPulpConcUnit.AutoSize = True
        Me.chbPulpConcUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpConcUnit.Location = New System.Drawing.Point(433, 110)
        Me.chbPulpConcUnit.Name = "chbPulpConcUnit"
        Me.chbPulpConcUnit.Size = New System.Drawing.Size(93, 17)
        Me.chbPulpConcUnit.TabIndex = 56
        Me.chbPulpConcUnit.Text = "Poll Conc Unit"
        Me.chbPulpConcUnit.UseVisualStyleBackColor = True
        '
        'chbPulpRun1
        '
        Me.chbPulpRun1.AutoSize = True
        Me.chbPulpRun1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpRun1.Location = New System.Drawing.Point(113, 93)
        Me.chbPulpRun1.Name = "chbPulpRun1"
        Me.chbPulpRun1.Size = New System.Drawing.Size(55, 17)
        Me.chbPulpRun1.TabIndex = 52
        Me.chbPulpRun1.Text = "Run 1"
        Me.chbPulpRun1.UseVisualStyleBackColor = True
        '
        'chbPulpAppRequire
        '
        Me.chbPulpAppRequire.AutoSize = True
        Me.chbPulpAppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpAppRequire.Location = New System.Drawing.Point(30, 39)
        Me.chbPulpAppRequire.Name = "chbPulpAppRequire"
        Me.chbPulpAppRequire.Size = New System.Drawing.Size(138, 17)
        Me.chbPulpAppRequire.TabIndex = 15
        Me.chbPulpAppRequire.Text = "Applicable Requirement"
        Me.chbPulpAppRequire.UseVisualStyleBackColor = True
        '
        'chbPulpAllowEmiss2
        '
        Me.chbPulpAllowEmiss2.AutoSize = True
        Me.chbPulpAllowEmiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpAllowEmiss2.Location = New System.Drawing.Point(175, 22)
        Me.chbPulpAllowEmiss2.Name = "chbPulpAllowEmiss2"
        Me.chbPulpAllowEmiss2.Size = New System.Drawing.Size(150, 17)
        Me.chbPulpAllowEmiss2.TabIndex = 12
        Me.chbPulpAllowEmiss2.Text = "Allowable Emission Rate 2"
        Me.chbPulpAllowEmiss2.UseVisualStyleBackColor = True
        '
        'chbPulpAllowEmiss3
        '
        Me.chbPulpAllowEmiss3.AutoSize = True
        Me.chbPulpAllowEmiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpAllowEmiss3.Location = New System.Drawing.Point(331, 22)
        Me.chbPulpAllowEmiss3.Name = "chbPulpAllowEmiss3"
        Me.chbPulpAllowEmiss3.Size = New System.Drawing.Size(150, 17)
        Me.chbPulpAllowEmiss3.TabIndex = 11
        Me.chbPulpAllowEmiss3.Text = "Allowable Emission Rate 3"
        Me.chbPulpAllowEmiss3.UseVisualStyleBackColor = True
        '
        'chbPulpOpCapacity
        '
        Me.chbPulpOpCapacity.AutoSize = True
        Me.chbPulpOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpOpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbPulpOpCapacity.Name = "chbPulpOpCapacity"
        Me.chbPulpOpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbPulpOpCapacity.TabIndex = 10
        Me.chbPulpOpCapacity.Text = "Operating Capacity"
        Me.chbPulpOpCapacity.UseVisualStyleBackColor = True
        '
        'chbPulpMaxOpCapacity
        '
        Me.chbPulpMaxOpCapacity.AutoSize = True
        Me.chbPulpMaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpMaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbPulpMaxOpCapacity.Name = "chbPulpMaxOpCapacity"
        Me.chbPulpMaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbPulpMaxOpCapacity.TabIndex = 9
        Me.chbPulpMaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbPulpMaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbPulpAllowEmiss1
        '
        Me.chbPulpAllowEmiss1.AutoSize = True
        Me.chbPulpAllowEmiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPulpAllowEmiss1.Location = New System.Drawing.Point(15, 22)
        Me.chbPulpAllowEmiss1.Name = "chbPulpAllowEmiss1"
        Me.chbPulpAllowEmiss1.Size = New System.Drawing.Size(153, 17)
        Me.chbPulpAllowEmiss1.TabIndex = 13
        Me.chbPulpAllowEmiss1.Text = "Allowable Emission Rate 1 "
        Me.chbPulpAllowEmiss1.UseVisualStyleBackColor = True
        '
        'TPGasConcentration
        '
        Me.TPGasConcentration.Controls.Add(Me.chbGasControlEquip)
        Me.TPGasConcentration.Controls.Add(Me.chbGasOtherInfo)
        Me.TPGasConcentration.Controls.Add(Me.chbGasPercentAllow)
        Me.TPGasConcentration.Controls.Add(Me.chbGasPoll3)
        Me.TPGasConcentration.Controls.Add(Me.chbGasEmiss3)
        Me.TPGasConcentration.Controls.Add(Me.chbGasRun3)
        Me.TPGasConcentration.Controls.Add(Me.chbGasPoll2)
        Me.TPGasConcentration.Controls.Add(Me.chbGasEmiss2)
        Me.TPGasConcentration.Controls.Add(Me.chbGasRun2)
        Me.TPGasConcentration.Controls.Add(Me.chbGasEmissAvg)
        Me.TPGasConcentration.Controls.Add(Me.chbGasPollAvg)
        Me.TPGasConcentration.Controls.Add(Me.chbGasPoll1)
        Me.TPGasConcentration.Controls.Add(Me.chbGasEmiss1)
        Me.TPGasConcentration.Controls.Add(Me.chbGasEmissUnit)
        Me.TPGasConcentration.Controls.Add(Me.chbGasPollUnit)
        Me.TPGasConcentration.Controls.Add(Me.chbGasRun1)
        Me.TPGasConcentration.Controls.Add(Me.chbGasAppRequire)
        Me.TPGasConcentration.Controls.Add(Me.chbGasAllowEmiss2)
        Me.TPGasConcentration.Controls.Add(Me.chbGasAllowEmiss3)
        Me.TPGasConcentration.Controls.Add(Me.chbGasOpCapacity)
        Me.TPGasConcentration.Controls.Add(Me.chbGasMaxOpCapacity)
        Me.TPGasConcentration.Controls.Add(Me.chbGasAllowEmiss1)
        Me.TPGasConcentration.Location = New System.Drawing.Point(4, 22)
        Me.TPGasConcentration.Name = "TPGasConcentration"
        Me.TPGasConcentration.Size = New System.Drawing.Size(786, 315)
        Me.TPGasConcentration.TabIndex = 3
        Me.TPGasConcentration.Text = "Gas Concentration"
        Me.TPGasConcentration.UseVisualStyleBackColor = True
        '
        'chbGasControlEquip
        '
        Me.chbGasControlEquip.AutoSize = True
        Me.chbGasControlEquip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasControlEquip.Location = New System.Drawing.Point(40, 57)
        Me.chbGasControlEquip.Name = "chbGasControlEquip"
        Me.chbGasControlEquip.Size = New System.Drawing.Size(128, 30)
        Me.chbGasControlEquip.TabIndex = 101
        Me.chbGasControlEquip.Text = "Control Equipment " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  and Monitoring Data"
        Me.chbGasControlEquip.UseVisualStyleBackColor = True
        '
        'chbGasOtherInfo
        '
        Me.chbGasOtherInfo.AutoSize = True
        Me.chbGasOtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasOtherInfo.Location = New System.Drawing.Point(61, 161)
        Me.chbGasOtherInfo.Name = "chbGasOtherInfo"
        Me.chbGasOtherInfo.Size = New System.Drawing.Size(107, 17)
        Me.chbGasOtherInfo.TabIndex = 100
        Me.chbGasOtherInfo.Text = "Other Information"
        Me.chbGasOtherInfo.UseVisualStyleBackColor = True
        '
        'chbGasPercentAllow
        '
        Me.chbGasPercentAllow.AutoSize = True
        Me.chbGasPercentAllow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasPercentAllow.Location = New System.Drawing.Point(57, 144)
        Me.chbGasPercentAllow.Name = "chbGasPercentAllow"
        Me.chbGasPercentAllow.Size = New System.Drawing.Size(111, 17)
        Me.chbGasPercentAllow.TabIndex = 99
        Me.chbGasPercentAllow.Text = "Percent Allowable"
        Me.chbGasPercentAllow.UseVisualStyleBackColor = True
        '
        'chbGasPoll3
        '
        Me.chbGasPoll3.AutoSize = True
        Me.chbGasPoll3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasPoll3.Location = New System.Drawing.Point(318, 110)
        Me.chbGasPoll3.Name = "chbGasPoll3"
        Me.chbGasPoll3.Size = New System.Drawing.Size(80, 17)
        Me.chbGasPoll3.TabIndex = 98
        Me.chbGasPoll3.Text = "Poll Conc 3"
        Me.chbGasPoll3.UseVisualStyleBackColor = True
        '
        'chbGasEmiss3
        '
        Me.chbGasEmiss3.AutoSize = True
        Me.chbGasEmiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasEmiss3.Location = New System.Drawing.Point(310, 127)
        Me.chbGasEmiss3.Name = "chbGasEmiss3"
        Me.chbGasEmiss3.Size = New System.Drawing.Size(88, 17)
        Me.chbGasEmiss3.TabIndex = 97
        Me.chbGasEmiss3.Text = "Emiss Rate 3"
        Me.chbGasEmiss3.UseVisualStyleBackColor = True
        '
        'chbGasRun3
        '
        Me.chbGasRun3.AutoSize = True
        Me.chbGasRun3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasRun3.Location = New System.Drawing.Point(343, 93)
        Me.chbGasRun3.Name = "chbGasRun3"
        Me.chbGasRun3.Size = New System.Drawing.Size(55, 17)
        Me.chbGasRun3.TabIndex = 96
        Me.chbGasRun3.Text = "Run 3"
        Me.chbGasRun3.UseVisualStyleBackColor = True
        '
        'chbGasPoll2
        '
        Me.chbGasPoll2.AutoSize = True
        Me.chbGasPoll2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasPoll2.Location = New System.Drawing.Point(203, 110)
        Me.chbGasPoll2.Name = "chbGasPoll2"
        Me.chbGasPoll2.Size = New System.Drawing.Size(80, 17)
        Me.chbGasPoll2.TabIndex = 95
        Me.chbGasPoll2.Text = "Poll Conc 2"
        Me.chbGasPoll2.UseVisualStyleBackColor = True
        '
        'chbGasEmiss2
        '
        Me.chbGasEmiss2.AutoSize = True
        Me.chbGasEmiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasEmiss2.Location = New System.Drawing.Point(195, 127)
        Me.chbGasEmiss2.Name = "chbGasEmiss2"
        Me.chbGasEmiss2.Size = New System.Drawing.Size(88, 17)
        Me.chbGasEmiss2.TabIndex = 94
        Me.chbGasEmiss2.Text = "Emiss Rate 2"
        Me.chbGasEmiss2.UseVisualStyleBackColor = True
        '
        'chbGasRun2
        '
        Me.chbGasRun2.AutoSize = True
        Me.chbGasRun2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasRun2.Location = New System.Drawing.Point(228, 93)
        Me.chbGasRun2.Name = "chbGasRun2"
        Me.chbGasRun2.Size = New System.Drawing.Size(55, 17)
        Me.chbGasRun2.TabIndex = 93
        Me.chbGasRun2.Text = "Run 2"
        Me.chbGasRun2.UseVisualStyleBackColor = True
        '
        'chbGasEmissAvg
        '
        Me.chbGasEmissAvg.AutoSize = True
        Me.chbGasEmissAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasEmissAvg.Location = New System.Drawing.Point(553, 127)
        Me.chbGasEmissAvg.Name = "chbGasEmissAvg"
        Me.chbGasEmissAvg.Size = New System.Drawing.Size(122, 17)
        Me.chbGasEmissAvg.TabIndex = 92
        Me.chbGasEmissAvg.Text = "Emiss Rate Average"
        Me.chbGasEmissAvg.UseVisualStyleBackColor = True
        '
        'chbGasPollAvg
        '
        Me.chbGasPollAvg.AutoSize = True
        Me.chbGasPollAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasPollAvg.Location = New System.Drawing.Point(561, 110)
        Me.chbGasPollAvg.Name = "chbGasPollAvg"
        Me.chbGasPollAvg.Size = New System.Drawing.Size(114, 17)
        Me.chbGasPollAvg.TabIndex = 91
        Me.chbGasPollAvg.Text = "Poll Conc Average"
        Me.chbGasPollAvg.UseVisualStyleBackColor = True
        '
        'chbGasPoll1
        '
        Me.chbGasPoll1.AutoSize = True
        Me.chbGasPoll1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasPoll1.Location = New System.Drawing.Point(88, 110)
        Me.chbGasPoll1.Name = "chbGasPoll1"
        Me.chbGasPoll1.Size = New System.Drawing.Size(80, 17)
        Me.chbGasPoll1.TabIndex = 90
        Me.chbGasPoll1.Text = "Poll Conc 1"
        Me.chbGasPoll1.UseVisualStyleBackColor = True
        '
        'chbGasEmiss1
        '
        Me.chbGasEmiss1.AutoSize = True
        Me.chbGasEmiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasEmiss1.Location = New System.Drawing.Point(80, 127)
        Me.chbGasEmiss1.Name = "chbGasEmiss1"
        Me.chbGasEmiss1.Size = New System.Drawing.Size(88, 17)
        Me.chbGasEmiss1.TabIndex = 89
        Me.chbGasEmiss1.Text = "Emiss Rate 1"
        Me.chbGasEmiss1.UseVisualStyleBackColor = True
        '
        'chbGasEmissUnit
        '
        Me.chbGasEmissUnit.AutoSize = True
        Me.chbGasEmissUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasEmissUnit.Location = New System.Drawing.Point(425, 127)
        Me.chbGasEmissUnit.Name = "chbGasEmissUnit"
        Me.chbGasEmissUnit.Size = New System.Drawing.Size(101, 17)
        Me.chbGasEmissUnit.TabIndex = 88
        Me.chbGasEmissUnit.Text = "Emiss Rate Unit"
        Me.chbGasEmissUnit.UseVisualStyleBackColor = True
        '
        'chbGasPollUnit
        '
        Me.chbGasPollUnit.AutoSize = True
        Me.chbGasPollUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasPollUnit.Location = New System.Drawing.Point(433, 110)
        Me.chbGasPollUnit.Name = "chbGasPollUnit"
        Me.chbGasPollUnit.Size = New System.Drawing.Size(93, 17)
        Me.chbGasPollUnit.TabIndex = 87
        Me.chbGasPollUnit.Text = "Poll Conc Unit"
        Me.chbGasPollUnit.UseVisualStyleBackColor = True
        '
        'chbGasRun1
        '
        Me.chbGasRun1.AutoSize = True
        Me.chbGasRun1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasRun1.Location = New System.Drawing.Point(113, 93)
        Me.chbGasRun1.Name = "chbGasRun1"
        Me.chbGasRun1.Size = New System.Drawing.Size(55, 17)
        Me.chbGasRun1.TabIndex = 86
        Me.chbGasRun1.Text = "Run 1"
        Me.chbGasRun1.UseVisualStyleBackColor = True
        '
        'chbGasAppRequire
        '
        Me.chbGasAppRequire.AutoSize = True
        Me.chbGasAppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasAppRequire.Location = New System.Drawing.Point(30, 39)
        Me.chbGasAppRequire.Name = "chbGasAppRequire"
        Me.chbGasAppRequire.Size = New System.Drawing.Size(138, 17)
        Me.chbGasAppRequire.TabIndex = 85
        Me.chbGasAppRequire.Text = "Applicable Requirement"
        Me.chbGasAppRequire.UseVisualStyleBackColor = True
        '
        'chbGasAllowEmiss2
        '
        Me.chbGasAllowEmiss2.AutoSize = True
        Me.chbGasAllowEmiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasAllowEmiss2.Location = New System.Drawing.Point(175, 22)
        Me.chbGasAllowEmiss2.Name = "chbGasAllowEmiss2"
        Me.chbGasAllowEmiss2.Size = New System.Drawing.Size(150, 17)
        Me.chbGasAllowEmiss2.TabIndex = 82
        Me.chbGasAllowEmiss2.Text = "Allowable Emission Rate 2"
        Me.chbGasAllowEmiss2.UseVisualStyleBackColor = True
        '
        'chbGasAllowEmiss3
        '
        Me.chbGasAllowEmiss3.AutoSize = True
        Me.chbGasAllowEmiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasAllowEmiss3.Location = New System.Drawing.Point(331, 22)
        Me.chbGasAllowEmiss3.Name = "chbGasAllowEmiss3"
        Me.chbGasAllowEmiss3.Size = New System.Drawing.Size(150, 17)
        Me.chbGasAllowEmiss3.TabIndex = 81
        Me.chbGasAllowEmiss3.Text = "Allowable Emission Rate 3"
        Me.chbGasAllowEmiss3.UseVisualStyleBackColor = True
        '
        'chbGasOpCapacity
        '
        Me.chbGasOpCapacity.AutoSize = True
        Me.chbGasOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasOpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbGasOpCapacity.Name = "chbGasOpCapacity"
        Me.chbGasOpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbGasOpCapacity.TabIndex = 80
        Me.chbGasOpCapacity.Text = "Operating Capacity"
        Me.chbGasOpCapacity.UseVisualStyleBackColor = True
        '
        'chbGasMaxOpCapacity
        '
        Me.chbGasMaxOpCapacity.AutoSize = True
        Me.chbGasMaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasMaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbGasMaxOpCapacity.Name = "chbGasMaxOpCapacity"
        Me.chbGasMaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbGasMaxOpCapacity.TabIndex = 79
        Me.chbGasMaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbGasMaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbGasAllowEmiss1
        '
        Me.chbGasAllowEmiss1.AutoSize = True
        Me.chbGasAllowEmiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbGasAllowEmiss1.Location = New System.Drawing.Point(15, 22)
        Me.chbGasAllowEmiss1.Name = "chbGasAllowEmiss1"
        Me.chbGasAllowEmiss1.Size = New System.Drawing.Size(153, 17)
        Me.chbGasAllowEmiss1.TabIndex = 83
        Me.chbGasAllowEmiss1.Text = "Allowable Emission Rate 1 "
        Me.chbGasAllowEmiss1.UseVisualStyleBackColor = True
        '
        'TPFlare
        '
        Me.TPFlare.Controls.Add(Me.chbFlareOtherInfo)
        Me.TPFlare.Controls.Add(Me.chbFlarePercentAllow)
        Me.TPFlare.Controls.Add(Me.chbFlareHeating3)
        Me.TPFlare.Controls.Add(Me.chbFlareVelocity3)
        Me.TPFlare.Controls.Add(Me.chbFlareRun3)
        Me.TPFlare.Controls.Add(Me.chbFlareHeating2)
        Me.TPFlare.Controls.Add(Me.chbFlareVelocity2)
        Me.TPFlare.Controls.Add(Me.chbFlareRun2)
        Me.TPFlare.Controls.Add(Me.chbFlareVelocityAvg)
        Me.TPFlare.Controls.Add(Me.chbFlareHeatingAvg)
        Me.TPFlare.Controls.Add(Me.chbFlareHeating1)
        Me.TPFlare.Controls.Add(Me.chbFlareVelocity1)
        Me.TPFlare.Controls.Add(Me.chbFlareVelocityUnit)
        Me.TPFlare.Controls.Add(Me.chbFlareHeatingUnit)
        Me.TPFlare.Controls.Add(Me.chbFlareRun1)
        Me.TPFlare.Controls.Add(Me.chbFlareAppRequire)
        Me.TPFlare.Controls.Add(Me.chbFlareHeatContent)
        Me.TPFlare.Controls.Add(Me.chbFlareMonitor)
        Me.TPFlare.Controls.Add(Me.chbFlareOpCapacity)
        Me.TPFlare.Controls.Add(Me.chbFlareMaxOpCapacity)
        Me.TPFlare.Controls.Add(Me.chbFlareAllowLimitations)
        Me.TPFlare.Location = New System.Drawing.Point(4, 22)
        Me.TPFlare.Name = "TPFlare"
        Me.TPFlare.Size = New System.Drawing.Size(786, 315)
        Me.TPFlare.TabIndex = 4
        Me.TPFlare.Text = "Flare"
        Me.TPFlare.UseVisualStyleBackColor = True
        '
        'chbFlareOtherInfo
        '
        Me.chbFlareOtherInfo.AutoSize = True
        Me.chbFlareOtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareOtherInfo.Location = New System.Drawing.Point(61, 161)
        Me.chbFlareOtherInfo.Name = "chbFlareOtherInfo"
        Me.chbFlareOtherInfo.Size = New System.Drawing.Size(107, 17)
        Me.chbFlareOtherInfo.TabIndex = 100
        Me.chbFlareOtherInfo.Text = "Other Information"
        Me.chbFlareOtherInfo.UseVisualStyleBackColor = True
        '
        'chbFlarePercentAllow
        '
        Me.chbFlarePercentAllow.AutoSize = True
        Me.chbFlarePercentAllow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlarePercentAllow.Location = New System.Drawing.Point(57, 144)
        Me.chbFlarePercentAllow.Name = "chbFlarePercentAllow"
        Me.chbFlarePercentAllow.Size = New System.Drawing.Size(111, 17)
        Me.chbFlarePercentAllow.TabIndex = 99
        Me.chbFlarePercentAllow.Text = "Percent Allowable"
        Me.chbFlarePercentAllow.UseVisualStyleBackColor = True
        '
        'chbFlareHeating3
        '
        Me.chbFlareHeating3.AutoSize = True
        Me.chbFlareHeating3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareHeating3.Location = New System.Drawing.Point(296, 110)
        Me.chbFlareHeating3.Name = "chbFlareHeating3"
        Me.chbFlareHeating3.Size = New System.Drawing.Size(102, 17)
        Me.chbFlareHeating3.TabIndex = 98
        Me.chbFlareHeating3.Text = "Heating Value 3"
        Me.chbFlareHeating3.UseVisualStyleBackColor = True
        '
        'chbFlareVelocity3
        '
        Me.chbFlareVelocity3.AutoSize = True
        Me.chbFlareVelocity3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareVelocity3.Location = New System.Drawing.Point(326, 127)
        Me.chbFlareVelocity3.Name = "chbFlareVelocity3"
        Me.chbFlareVelocity3.Size = New System.Drawing.Size(72, 17)
        Me.chbFlareVelocity3.TabIndex = 97
        Me.chbFlareVelocity3.Text = "Velocity 3"
        Me.chbFlareVelocity3.UseVisualStyleBackColor = True
        '
        'chbFlareRun3
        '
        Me.chbFlareRun3.AutoSize = True
        Me.chbFlareRun3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareRun3.Location = New System.Drawing.Point(343, 93)
        Me.chbFlareRun3.Name = "chbFlareRun3"
        Me.chbFlareRun3.Size = New System.Drawing.Size(55, 17)
        Me.chbFlareRun3.TabIndex = 96
        Me.chbFlareRun3.Text = "Run 3"
        Me.chbFlareRun3.UseVisualStyleBackColor = True
        '
        'chbFlareHeating2
        '
        Me.chbFlareHeating2.AutoSize = True
        Me.chbFlareHeating2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareHeating2.Location = New System.Drawing.Point(181, 110)
        Me.chbFlareHeating2.Name = "chbFlareHeating2"
        Me.chbFlareHeating2.Size = New System.Drawing.Size(102, 17)
        Me.chbFlareHeating2.TabIndex = 95
        Me.chbFlareHeating2.Text = "Heating Value 2"
        Me.chbFlareHeating2.UseVisualStyleBackColor = True
        '
        'chbFlareVelocity2
        '
        Me.chbFlareVelocity2.AutoSize = True
        Me.chbFlareVelocity2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareVelocity2.Location = New System.Drawing.Point(211, 127)
        Me.chbFlareVelocity2.Name = "chbFlareVelocity2"
        Me.chbFlareVelocity2.Size = New System.Drawing.Size(72, 17)
        Me.chbFlareVelocity2.TabIndex = 94
        Me.chbFlareVelocity2.Text = "Velocity 2"
        Me.chbFlareVelocity2.UseVisualStyleBackColor = True
        '
        'chbFlareRun2
        '
        Me.chbFlareRun2.AutoSize = True
        Me.chbFlareRun2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareRun2.Location = New System.Drawing.Point(228, 93)
        Me.chbFlareRun2.Name = "chbFlareRun2"
        Me.chbFlareRun2.Size = New System.Drawing.Size(55, 17)
        Me.chbFlareRun2.TabIndex = 93
        Me.chbFlareRun2.Text = "Run 2"
        Me.chbFlareRun2.UseVisualStyleBackColor = True
        '
        'chbFlareVelocityAvg
        '
        Me.chbFlareVelocityAvg.AutoSize = True
        Me.chbFlareVelocityAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareVelocityAvg.Location = New System.Drawing.Point(591, 127)
        Me.chbFlareVelocityAvg.Name = "chbFlareVelocityAvg"
        Me.chbFlareVelocityAvg.Size = New System.Drawing.Size(106, 17)
        Me.chbFlareVelocityAvg.TabIndex = 92
        Me.chbFlareVelocityAvg.Text = "Velocity Average"
        Me.chbFlareVelocityAvg.UseVisualStyleBackColor = True
        '
        'chbFlareHeatingAvg
        '
        Me.chbFlareHeatingAvg.AutoSize = True
        Me.chbFlareHeatingAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareHeatingAvg.Location = New System.Drawing.Point(561, 110)
        Me.chbFlareHeatingAvg.Name = "chbFlareHeatingAvg"
        Me.chbFlareHeatingAvg.Size = New System.Drawing.Size(136, 17)
        Me.chbFlareHeatingAvg.TabIndex = 91
        Me.chbFlareHeatingAvg.Text = "Heating Value Average"
        Me.chbFlareHeatingAvg.UseVisualStyleBackColor = True
        '
        'chbFlareHeating1
        '
        Me.chbFlareHeating1.AutoSize = True
        Me.chbFlareHeating1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareHeating1.Location = New System.Drawing.Point(66, 110)
        Me.chbFlareHeating1.Name = "chbFlareHeating1"
        Me.chbFlareHeating1.Size = New System.Drawing.Size(102, 17)
        Me.chbFlareHeating1.TabIndex = 90
        Me.chbFlareHeating1.Text = "Heating Value 1"
        Me.chbFlareHeating1.UseVisualStyleBackColor = True
        '
        'chbFlareVelocity1
        '
        Me.chbFlareVelocity1.AutoSize = True
        Me.chbFlareVelocity1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareVelocity1.Location = New System.Drawing.Point(96, 127)
        Me.chbFlareVelocity1.Name = "chbFlareVelocity1"
        Me.chbFlareVelocity1.Size = New System.Drawing.Size(72, 17)
        Me.chbFlareVelocity1.TabIndex = 89
        Me.chbFlareVelocity1.Text = "Velocity 1"
        Me.chbFlareVelocity1.UseVisualStyleBackColor = True
        '
        'chbFlareVelocityUnit
        '
        Me.chbFlareVelocityUnit.AutoSize = True
        Me.chbFlareVelocityUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareVelocityUnit.Location = New System.Drawing.Point(463, 127)
        Me.chbFlareVelocityUnit.Name = "chbFlareVelocityUnit"
        Me.chbFlareVelocityUnit.Size = New System.Drawing.Size(85, 17)
        Me.chbFlareVelocityUnit.TabIndex = 88
        Me.chbFlareVelocityUnit.Text = "Velocity Unit"
        Me.chbFlareVelocityUnit.UseVisualStyleBackColor = True
        '
        'chbFlareHeatingUnit
        '
        Me.chbFlareHeatingUnit.AutoSize = True
        Me.chbFlareHeatingUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareHeatingUnit.Location = New System.Drawing.Point(433, 110)
        Me.chbFlareHeatingUnit.Name = "chbFlareHeatingUnit"
        Me.chbFlareHeatingUnit.Size = New System.Drawing.Size(115, 17)
        Me.chbFlareHeatingUnit.TabIndex = 87
        Me.chbFlareHeatingUnit.Text = "Heating Value Unit"
        Me.chbFlareHeatingUnit.UseVisualStyleBackColor = True
        '
        'chbFlareRun1
        '
        Me.chbFlareRun1.AutoSize = True
        Me.chbFlareRun1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareRun1.Location = New System.Drawing.Point(113, 93)
        Me.chbFlareRun1.Name = "chbFlareRun1"
        Me.chbFlareRun1.Size = New System.Drawing.Size(55, 17)
        Me.chbFlareRun1.TabIndex = 86
        Me.chbFlareRun1.Text = "Run 1"
        Me.chbFlareRun1.UseVisualStyleBackColor = True
        '
        'chbFlareAppRequire
        '
        Me.chbFlareAppRequire.AutoSize = True
        Me.chbFlareAppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareAppRequire.Location = New System.Drawing.Point(30, 39)
        Me.chbFlareAppRequire.Name = "chbFlareAppRequire"
        Me.chbFlareAppRequire.Size = New System.Drawing.Size(138, 17)
        Me.chbFlareAppRequire.TabIndex = 85
        Me.chbFlareAppRequire.Text = "Applicable Requirement"
        Me.chbFlareAppRequire.UseVisualStyleBackColor = True
        '
        'chbFlareHeatContent
        '
        Me.chbFlareHeatContent.AutoSize = True
        Me.chbFlareHeatContent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareHeatContent.Location = New System.Drawing.Point(237, 22)
        Me.chbFlareHeatContent.Name = "chbFlareHeatContent"
        Me.chbFlareHeatContent.Size = New System.Drawing.Size(88, 17)
        Me.chbFlareHeatContent.TabIndex = 82
        Me.chbFlareHeatContent.Text = "Heat content"
        Me.chbFlareHeatContent.UseVisualStyleBackColor = True
        '
        'chbFlareMonitor
        '
        Me.chbFlareMonitor.AutoSize = True
        Me.chbFlareMonitor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareMonitor.Location = New System.Drawing.Point(67, 57)
        Me.chbFlareMonitor.Name = "chbFlareMonitor"
        Me.chbFlareMonitor.Size = New System.Drawing.Size(101, 17)
        Me.chbFlareMonitor.TabIndex = 84
        Me.chbFlareMonitor.Text = "Monitoring Data"
        Me.chbFlareMonitor.UseVisualStyleBackColor = True
        '
        'chbFlareOpCapacity
        '
        Me.chbFlareOpCapacity.AutoSize = True
        Me.chbFlareOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareOpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbFlareOpCapacity.Name = "chbFlareOpCapacity"
        Me.chbFlareOpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbFlareOpCapacity.TabIndex = 80
        Me.chbFlareOpCapacity.Text = "Operating Capacity"
        Me.chbFlareOpCapacity.UseVisualStyleBackColor = True
        '
        'chbFlareMaxOpCapacity
        '
        Me.chbFlareMaxOpCapacity.AutoSize = True
        Me.chbFlareMaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareMaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbFlareMaxOpCapacity.Name = "chbFlareMaxOpCapacity"
        Me.chbFlareMaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbFlareMaxOpCapacity.TabIndex = 79
        Me.chbFlareMaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbFlareMaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbFlareAllowLimitations
        '
        Me.chbFlareAllowLimitations.AutoSize = True
        Me.chbFlareAllowLimitations.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFlareAllowLimitations.Location = New System.Drawing.Point(45, 22)
        Me.chbFlareAllowLimitations.Name = "chbFlareAllowLimitations"
        Me.chbFlareAllowLimitations.Size = New System.Drawing.Size(123, 17)
        Me.chbFlareAllowLimitations.TabIndex = 83
        Me.chbFlareAllowLimitations.Text = "Allowable Limitations"
        Me.chbFlareAllowLimitations.UseVisualStyleBackColor = True
        '
        'TPPEM
        '
        Me.TPPEM.Location = New System.Drawing.Point(4, 22)
        Me.TPPEM.Name = "TPPEM"
        Me.TPPEM.Size = New System.Drawing.Size(786, 315)
        Me.TPPEM.TabIndex = 5
        Me.TPPEM.Text = "PEM"
        Me.TPPEM.UseVisualStyleBackColor = True
        '
        'TPMethod9
        '
        Me.TPMethod9.Controls.Add(Me.TCMethod9)
        Me.TPMethod9.Location = New System.Drawing.Point(4, 22)
        Me.TPMethod9.Name = "TPMethod9"
        Me.TPMethod9.Size = New System.Drawing.Size(786, 315)
        Me.TPMethod9.TabIndex = 6
        Me.TPMethod9.Text = "Method 9"
        Me.TPMethod9.UseVisualStyleBackColor = True
        '
        'TCMethod9
        '
        Me.TCMethod9.Controls.Add(Me.TPMethod9Single)
        Me.TCMethod9.Controls.Add(Me.TPMethod9Multi)
        Me.TCMethod9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCMethod9.Location = New System.Drawing.Point(0, 0)
        Me.TCMethod9.Name = "TCMethod9"
        Me.TCMethod9.SelectedIndex = 0
        Me.TCMethod9.Size = New System.Drawing.Size(786, 315)
        Me.TCMethod9.TabIndex = 0
        '
        'TPMethod9Single
        '
        Me.TPMethod9Single.Controls.Add(Me.chbMethod9ControlEquip)
        Me.TPMethod9Single.Controls.Add(Me.chbMethod9OtherInfo)
        Me.TPMethod9Single.Controls.Add(Me.chbMethod9Opacity)
        Me.TPMethod9Single.Controls.Add(Me.chbMethod9TestDuration)
        Me.TPMethod9Single.Controls.Add(Me.chbMethod9AppRequire)
        Me.TPMethod9Single.Controls.Add(Me.chbMethod9OpCapacity)
        Me.TPMethod9Single.Controls.Add(Me.chbMethod9MaxOpCapacity)
        Me.TPMethod9Single.Controls.Add(Me.chbMethod9AllowEmiss)
        Me.TPMethod9Single.Location = New System.Drawing.Point(4, 22)
        Me.TPMethod9Single.Name = "TPMethod9Single"
        Me.TPMethod9Single.Padding = New System.Windows.Forms.Padding(3)
        Me.TPMethod9Single.Size = New System.Drawing.Size(778, 289)
        Me.TPMethod9Single.TabIndex = 0
        Me.TPMethod9Single.Text = "Single"
        Me.TPMethod9Single.UseVisualStyleBackColor = True
        '
        'chbMethod9ControlEquip
        '
        Me.chbMethod9ControlEquip.AutoSize = True
        Me.chbMethod9ControlEquip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9ControlEquip.Location = New System.Drawing.Point(40, 57)
        Me.chbMethod9ControlEquip.Name = "chbMethod9ControlEquip"
        Me.chbMethod9ControlEquip.Size = New System.Drawing.Size(128, 30)
        Me.chbMethod9ControlEquip.TabIndex = 80
        Me.chbMethod9ControlEquip.Text = "Control Equipment " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  and Monitoring Data"
        Me.chbMethod9ControlEquip.UseVisualStyleBackColor = True
        '
        'chbMethod9OtherInfo
        '
        Me.chbMethod9OtherInfo.AutoSize = True
        Me.chbMethod9OtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9OtherInfo.Location = New System.Drawing.Point(61, 127)
        Me.chbMethod9OtherInfo.Name = "chbMethod9OtherInfo"
        Me.chbMethod9OtherInfo.Size = New System.Drawing.Size(107, 17)
        Me.chbMethod9OtherInfo.TabIndex = 18
        Me.chbMethod9OtherInfo.Text = "Other Information"
        Me.chbMethod9OtherInfo.UseVisualStyleBackColor = True
        '
        'chbMethod9Opacity
        '
        Me.chbMethod9Opacity.AutoSize = True
        Me.chbMethod9Opacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9Opacity.Location = New System.Drawing.Point(106, 110)
        Me.chbMethod9Opacity.Name = "chbMethod9Opacity"
        Me.chbMethod9Opacity.Size = New System.Drawing.Size(62, 17)
        Me.chbMethod9Opacity.TabIndex = 17
        Me.chbMethod9Opacity.Text = "Opacity"
        Me.chbMethod9Opacity.UseVisualStyleBackColor = True
        '
        'chbMethod9TestDuration
        '
        Me.chbMethod9TestDuration.AutoSize = True
        Me.chbMethod9TestDuration.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9TestDuration.Location = New System.Drawing.Point(78, 93)
        Me.chbMethod9TestDuration.Name = "chbMethod9TestDuration"
        Me.chbMethod9TestDuration.Size = New System.Drawing.Size(90, 17)
        Me.chbMethod9TestDuration.TabIndex = 16
        Me.chbMethod9TestDuration.Text = "Test Duration"
        Me.chbMethod9TestDuration.UseVisualStyleBackColor = True
        '
        'chbMethod9AppRequire
        '
        Me.chbMethod9AppRequire.AutoSize = True
        Me.chbMethod9AppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9AppRequire.Location = New System.Drawing.Point(30, 39)
        Me.chbMethod9AppRequire.Name = "chbMethod9AppRequire"
        Me.chbMethod9AppRequire.Size = New System.Drawing.Size(138, 17)
        Me.chbMethod9AppRequire.TabIndex = 15
        Me.chbMethod9AppRequire.Text = "Applicable Requirement"
        Me.chbMethod9AppRequire.UseVisualStyleBackColor = True
        '
        'chbMethod9OpCapacity
        '
        Me.chbMethod9OpCapacity.AutoSize = True
        Me.chbMethod9OpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9OpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbMethod9OpCapacity.Name = "chbMethod9OpCapacity"
        Me.chbMethod9OpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9OpCapacity.TabIndex = 10
        Me.chbMethod9OpCapacity.Text = "Operating Capacity"
        Me.chbMethod9OpCapacity.UseVisualStyleBackColor = True
        '
        'chbMethod9MaxOpCapacity
        '
        Me.chbMethod9MaxOpCapacity.AutoSize = True
        Me.chbMethod9MaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbMethod9MaxOpCapacity.Name = "chbMethod9MaxOpCapacity"
        Me.chbMethod9MaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbMethod9MaxOpCapacity.TabIndex = 9
        Me.chbMethod9MaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbMethod9MaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbMethod9AllowEmiss
        '
        Me.chbMethod9AllowEmiss.AutoSize = True
        Me.chbMethod9AllowEmiss.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9AllowEmiss.Location = New System.Drawing.Point(27, 22)
        Me.chbMethod9AllowEmiss.Name = "chbMethod9AllowEmiss"
        Me.chbMethod9AllowEmiss.Size = New System.Drawing.Size(141, 17)
        Me.chbMethod9AllowEmiss.TabIndex = 13
        Me.chbMethod9AllowEmiss.Text = "Allowable Emission Rate"
        Me.chbMethod9AllowEmiss.UseVisualStyleBackColor = True
        '
        'TPMethod9Multi
        '
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAppRequire)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiControlEquip)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiEquip2)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiEquip4)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiEquip5)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiEquip3)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiEquip1)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAvg5)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAvg4)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAvg3)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAvg2)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAllowEmissUnit)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAllowEmiss5)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAllowEmiss4)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAllowEmiss3)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAllowEmiss2)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiOpCapacityUnit)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiOpCapacity2)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiOpCapacity3)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiOpCapacity4)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiOpCapacity5)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiOpCapacity1)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiMaxOpCapacity5)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiMaxOpCapacity4)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiMaxOpCapacity3)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiMaxOpCapacity2)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiOtherInfor)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAvg1)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiMaxOpCapacityUnit)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiMaxOpCapacity1)
        Me.TPMethod9Multi.Controls.Add(Me.chbMethod9MultiAllowEmiss1)
        Me.TPMethod9Multi.Location = New System.Drawing.Point(4, 22)
        Me.TPMethod9Multi.Name = "TPMethod9Multi"
        Me.TPMethod9Multi.Padding = New System.Windows.Forms.Padding(3)
        Me.TPMethod9Multi.Size = New System.Drawing.Size(778, 289)
        Me.TPMethod9Multi.TabIndex = 1
        Me.TPMethod9Multi.Text = "Multiple"
        Me.TPMethod9Multi.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAppRequire
        '
        Me.chbMethod9MultiAppRequire.AutoSize = True
        Me.chbMethod9MultiAppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAppRequire.Location = New System.Drawing.Point(35, 58)
        Me.chbMethod9MultiAppRequire.Name = "chbMethod9MultiAppRequire"
        Me.chbMethod9MultiAppRequire.Size = New System.Drawing.Size(98, 30)
        Me.chbMethod9MultiAppRequire.TabIndex = 81
        Me.chbMethod9MultiAppRequire.Text = "Applicable " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    Requirement"
        Me.chbMethod9MultiAppRequire.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiControlEquip
        '
        Me.chbMethod9MultiControlEquip.AutoSize = True
        Me.chbMethod9MultiControlEquip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiControlEquip.Location = New System.Drawing.Point(5, 90)
        Me.chbMethod9MultiControlEquip.Name = "chbMethod9MultiControlEquip"
        Me.chbMethod9MultiControlEquip.Size = New System.Drawing.Size(128, 30)
        Me.chbMethod9MultiControlEquip.TabIndex = 80
        Me.chbMethod9MultiControlEquip.Text = "Control Equipment " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  and Monitoring Data"
        Me.chbMethod9MultiControlEquip.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiEquip2
        '
        Me.chbMethod9MultiEquip2.AutoSize = True
        Me.chbMethod9MultiEquip2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiEquip2.Location = New System.Drawing.Point(25, 174)
        Me.chbMethod9MultiEquip2.Name = "chbMethod9MultiEquip2"
        Me.chbMethod9MultiEquip2.Size = New System.Drawing.Size(108, 17)
        Me.chbMethod9MultiEquip2.TabIndex = 50
        Me.chbMethod9MultiEquip2.Text = "Equipment Item 2"
        Me.chbMethod9MultiEquip2.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiEquip4
        '
        Me.chbMethod9MultiEquip4.AutoSize = True
        Me.chbMethod9MultiEquip4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiEquip4.Location = New System.Drawing.Point(25, 208)
        Me.chbMethod9MultiEquip4.Name = "chbMethod9MultiEquip4"
        Me.chbMethod9MultiEquip4.Size = New System.Drawing.Size(108, 17)
        Me.chbMethod9MultiEquip4.TabIndex = 49
        Me.chbMethod9MultiEquip4.Text = "Equipment Item 4"
        Me.chbMethod9MultiEquip4.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiEquip5
        '
        Me.chbMethod9MultiEquip5.AutoSize = True
        Me.chbMethod9MultiEquip5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiEquip5.Location = New System.Drawing.Point(25, 225)
        Me.chbMethod9MultiEquip5.Name = "chbMethod9MultiEquip5"
        Me.chbMethod9MultiEquip5.Size = New System.Drawing.Size(108, 17)
        Me.chbMethod9MultiEquip5.TabIndex = 48
        Me.chbMethod9MultiEquip5.Text = "Equipment Item 5"
        Me.chbMethod9MultiEquip5.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiEquip3
        '
        Me.chbMethod9MultiEquip3.AutoSize = True
        Me.chbMethod9MultiEquip3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiEquip3.Location = New System.Drawing.Point(25, 191)
        Me.chbMethod9MultiEquip3.Name = "chbMethod9MultiEquip3"
        Me.chbMethod9MultiEquip3.Size = New System.Drawing.Size(108, 17)
        Me.chbMethod9MultiEquip3.TabIndex = 47
        Me.chbMethod9MultiEquip3.Text = "Equipment Item 3"
        Me.chbMethod9MultiEquip3.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiEquip1
        '
        Me.chbMethod9MultiEquip1.AutoSize = True
        Me.chbMethod9MultiEquip1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiEquip1.Location = New System.Drawing.Point(25, 157)
        Me.chbMethod9MultiEquip1.Name = "chbMethod9MultiEquip1"
        Me.chbMethod9MultiEquip1.Size = New System.Drawing.Size(108, 17)
        Me.chbMethod9MultiEquip1.TabIndex = 46
        Me.chbMethod9MultiEquip1.Text = "Equipment Item 1"
        Me.chbMethod9MultiEquip1.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAvg5
        '
        Me.chbMethod9MultiAvg5.AutoSize = True
        Me.chbMethod9MultiAvg5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAvg5.Location = New System.Drawing.Point(550, 123)
        Me.chbMethod9MultiAvg5.Name = "chbMethod9MultiAvg5"
        Me.chbMethod9MultiAvg5.Size = New System.Drawing.Size(83, 17)
        Me.chbMethod9MultiAvg5.TabIndex = 45
        Me.chbMethod9MultiAvg5.Text = "6 Min Avg 5"
        Me.chbMethod9MultiAvg5.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAvg4
        '
        Me.chbMethod9MultiAvg4.AutoSize = True
        Me.chbMethod9MultiAvg4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAvg4.Location = New System.Drawing.Point(428, 123)
        Me.chbMethod9MultiAvg4.Name = "chbMethod9MultiAvg4"
        Me.chbMethod9MultiAvg4.Size = New System.Drawing.Size(83, 17)
        Me.chbMethod9MultiAvg4.TabIndex = 44
        Me.chbMethod9MultiAvg4.Text = "6 Min Avg 4"
        Me.chbMethod9MultiAvg4.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAvg3
        '
        Me.chbMethod9MultiAvg3.AutoSize = True
        Me.chbMethod9MultiAvg3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAvg3.Location = New System.Drawing.Point(306, 123)
        Me.chbMethod9MultiAvg3.Name = "chbMethod9MultiAvg3"
        Me.chbMethod9MultiAvg3.Size = New System.Drawing.Size(83, 17)
        Me.chbMethod9MultiAvg3.TabIndex = 43
        Me.chbMethod9MultiAvg3.Text = "6 Min Avg 3"
        Me.chbMethod9MultiAvg3.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAvg2
        '
        Me.chbMethod9MultiAvg2.AutoSize = True
        Me.chbMethod9MultiAvg2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAvg2.Location = New System.Drawing.Point(179, 123)
        Me.chbMethod9MultiAvg2.Name = "chbMethod9MultiAvg2"
        Me.chbMethod9MultiAvg2.Size = New System.Drawing.Size(83, 17)
        Me.chbMethod9MultiAvg2.TabIndex = 42
        Me.chbMethod9MultiAvg2.Text = "6 Min Avg 2"
        Me.chbMethod9MultiAvg2.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAllowEmissUnit
        '
        Me.chbMethod9MultiAllowEmissUnit.AutoSize = True
        Me.chbMethod9MultiAllowEmissUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAllowEmissUnit.Location = New System.Drawing.Point(639, 39)
        Me.chbMethod9MultiAllowEmissUnit.Name = "chbMethod9MultiAllowEmissUnit"
        Me.chbMethod9MultiAllowEmissUnit.Size = New System.Drawing.Size(129, 17)
        Me.chbMethod9MultiAllowEmissUnit.TabIndex = 41
        Me.chbMethod9MultiAllowEmissUnit.Text = "Allow Emiss Rate Unit"
        Me.chbMethod9MultiAllowEmissUnit.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAllowEmiss5
        '
        Me.chbMethod9MultiAllowEmiss5.AutoSize = True
        Me.chbMethod9MultiAllowEmiss5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAllowEmiss5.Location = New System.Drawing.Point(517, 39)
        Me.chbMethod9MultiAllowEmiss5.Name = "chbMethod9MultiAllowEmiss5"
        Me.chbMethod9MultiAllowEmiss5.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiAllowEmiss5.TabIndex = 40
        Me.chbMethod9MultiAllowEmiss5.Text = "Allow Emiss Rate 5"
        Me.chbMethod9MultiAllowEmiss5.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAllowEmiss4
        '
        Me.chbMethod9MultiAllowEmiss4.AutoSize = True
        Me.chbMethod9MultiAllowEmiss4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAllowEmiss4.Location = New System.Drawing.Point(395, 39)
        Me.chbMethod9MultiAllowEmiss4.Name = "chbMethod9MultiAllowEmiss4"
        Me.chbMethod9MultiAllowEmiss4.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiAllowEmiss4.TabIndex = 39
        Me.chbMethod9MultiAllowEmiss4.Text = "Allow Emiss Rate 4"
        Me.chbMethod9MultiAllowEmiss4.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAllowEmiss3
        '
        Me.chbMethod9MultiAllowEmiss3.AutoSize = True
        Me.chbMethod9MultiAllowEmiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAllowEmiss3.Location = New System.Drawing.Point(273, 39)
        Me.chbMethod9MultiAllowEmiss3.Name = "chbMethod9MultiAllowEmiss3"
        Me.chbMethod9MultiAllowEmiss3.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiAllowEmiss3.TabIndex = 38
        Me.chbMethod9MultiAllowEmiss3.Text = "Allow Emiss Rate 3"
        Me.chbMethod9MultiAllowEmiss3.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAllowEmiss2
        '
        Me.chbMethod9MultiAllowEmiss2.AutoSize = True
        Me.chbMethod9MultiAllowEmiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAllowEmiss2.Location = New System.Drawing.Point(146, 39)
        Me.chbMethod9MultiAllowEmiss2.Name = "chbMethod9MultiAllowEmiss2"
        Me.chbMethod9MultiAllowEmiss2.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiAllowEmiss2.TabIndex = 37
        Me.chbMethod9MultiAllowEmiss2.Text = "Allow Emiss Rate 2"
        Me.chbMethod9MultiAllowEmiss2.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiOpCapacityUnit
        '
        Me.chbMethod9MultiOpCapacityUnit.AutoSize = True
        Me.chbMethod9MultiOpCapacityUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiOpCapacityUnit.Location = New System.Drawing.Point(662, 22)
        Me.chbMethod9MultiOpCapacityUnit.Name = "chbMethod9MultiOpCapacityUnit"
        Me.chbMethod9MultiOpCapacityUnit.Size = New System.Drawing.Size(106, 17)
        Me.chbMethod9MultiOpCapacityUnit.TabIndex = 36
        Me.chbMethod9MultiOpCapacityUnit.Text = "Op Capacity Unit"
        Me.chbMethod9MultiOpCapacityUnit.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiOpCapacity2
        '
        Me.chbMethod9MultiOpCapacity2.AutoSize = True
        Me.chbMethod9MultiOpCapacity2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiOpCapacity2.Location = New System.Drawing.Point(169, 22)
        Me.chbMethod9MultiOpCapacity2.Name = "chbMethod9MultiOpCapacity2"
        Me.chbMethod9MultiOpCapacity2.Size = New System.Drawing.Size(93, 17)
        Me.chbMethod9MultiOpCapacity2.TabIndex = 35
        Me.chbMethod9MultiOpCapacity2.Text = "Op Capacity 2"
        Me.chbMethod9MultiOpCapacity2.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiOpCapacity3
        '
        Me.chbMethod9MultiOpCapacity3.AutoSize = True
        Me.chbMethod9MultiOpCapacity3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiOpCapacity3.Location = New System.Drawing.Point(296, 22)
        Me.chbMethod9MultiOpCapacity3.Name = "chbMethod9MultiOpCapacity3"
        Me.chbMethod9MultiOpCapacity3.Size = New System.Drawing.Size(93, 17)
        Me.chbMethod9MultiOpCapacity3.TabIndex = 34
        Me.chbMethod9MultiOpCapacity3.Text = "Op Capacity 3"
        Me.chbMethod9MultiOpCapacity3.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiOpCapacity4
        '
        Me.chbMethod9MultiOpCapacity4.AutoSize = True
        Me.chbMethod9MultiOpCapacity4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiOpCapacity4.Location = New System.Drawing.Point(418, 22)
        Me.chbMethod9MultiOpCapacity4.Name = "chbMethod9MultiOpCapacity4"
        Me.chbMethod9MultiOpCapacity4.Size = New System.Drawing.Size(93, 17)
        Me.chbMethod9MultiOpCapacity4.TabIndex = 33
        Me.chbMethod9MultiOpCapacity4.Text = "Op Capacity 4"
        Me.chbMethod9MultiOpCapacity4.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiOpCapacity5
        '
        Me.chbMethod9MultiOpCapacity5.AutoSize = True
        Me.chbMethod9MultiOpCapacity5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiOpCapacity5.Location = New System.Drawing.Point(540, 22)
        Me.chbMethod9MultiOpCapacity5.Name = "chbMethod9MultiOpCapacity5"
        Me.chbMethod9MultiOpCapacity5.Size = New System.Drawing.Size(93, 17)
        Me.chbMethod9MultiOpCapacity5.TabIndex = 32
        Me.chbMethod9MultiOpCapacity5.Text = "Op Capacity 5"
        Me.chbMethod9MultiOpCapacity5.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiOpCapacity1
        '
        Me.chbMethod9MultiOpCapacity1.AutoSize = True
        Me.chbMethod9MultiOpCapacity1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiOpCapacity1.Location = New System.Drawing.Point(40, 22)
        Me.chbMethod9MultiOpCapacity1.Name = "chbMethod9MultiOpCapacity1"
        Me.chbMethod9MultiOpCapacity1.Size = New System.Drawing.Size(93, 17)
        Me.chbMethod9MultiOpCapacity1.TabIndex = 31
        Me.chbMethod9MultiOpCapacity1.Text = "Op Capacity 1"
        Me.chbMethod9MultiOpCapacity1.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiMaxOpCapacity5
        '
        Me.chbMethod9MultiMaxOpCapacity5.AutoSize = True
        Me.chbMethod9MultiMaxOpCapacity5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiMaxOpCapacity5.Location = New System.Drawing.Point(517, 5)
        Me.chbMethod9MultiMaxOpCapacity5.Name = "chbMethod9MultiMaxOpCapacity5"
        Me.chbMethod9MultiMaxOpCapacity5.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiMaxOpCapacity5.TabIndex = 30
        Me.chbMethod9MultiMaxOpCapacity5.Text = "Max Op Capacity 5"
        Me.chbMethod9MultiMaxOpCapacity5.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiMaxOpCapacity4
        '
        Me.chbMethod9MultiMaxOpCapacity4.AutoSize = True
        Me.chbMethod9MultiMaxOpCapacity4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiMaxOpCapacity4.Location = New System.Drawing.Point(395, 5)
        Me.chbMethod9MultiMaxOpCapacity4.Name = "chbMethod9MultiMaxOpCapacity4"
        Me.chbMethod9MultiMaxOpCapacity4.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiMaxOpCapacity4.TabIndex = 29
        Me.chbMethod9MultiMaxOpCapacity4.Text = "Max Op Capacity 4"
        Me.chbMethod9MultiMaxOpCapacity4.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiMaxOpCapacity3
        '
        Me.chbMethod9MultiMaxOpCapacity3.AutoSize = True
        Me.chbMethod9MultiMaxOpCapacity3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiMaxOpCapacity3.Location = New System.Drawing.Point(273, 5)
        Me.chbMethod9MultiMaxOpCapacity3.Name = "chbMethod9MultiMaxOpCapacity3"
        Me.chbMethod9MultiMaxOpCapacity3.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiMaxOpCapacity3.TabIndex = 28
        Me.chbMethod9MultiMaxOpCapacity3.Text = "Max Op Capacity 3"
        Me.chbMethod9MultiMaxOpCapacity3.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiMaxOpCapacity2
        '
        Me.chbMethod9MultiMaxOpCapacity2.AutoSize = True
        Me.chbMethod9MultiMaxOpCapacity2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiMaxOpCapacity2.Location = New System.Drawing.Point(146, 5)
        Me.chbMethod9MultiMaxOpCapacity2.Name = "chbMethod9MultiMaxOpCapacity2"
        Me.chbMethod9MultiMaxOpCapacity2.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiMaxOpCapacity2.TabIndex = 27
        Me.chbMethod9MultiMaxOpCapacity2.Text = "Max Op Capacity 2"
        Me.chbMethod9MultiMaxOpCapacity2.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiOtherInfor
        '
        Me.chbMethod9MultiOtherInfor.AutoSize = True
        Me.chbMethod9MultiOtherInfor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiOtherInfor.Location = New System.Drawing.Point(26, 140)
        Me.chbMethod9MultiOtherInfor.Name = "chbMethod9MultiOtherInfor"
        Me.chbMethod9MultiOtherInfor.Size = New System.Drawing.Size(107, 17)
        Me.chbMethod9MultiOtherInfor.TabIndex = 26
        Me.chbMethod9MultiOtherInfor.Text = "Other Information"
        Me.chbMethod9MultiOtherInfor.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAvg1
        '
        Me.chbMethod9MultiAvg1.AutoSize = True
        Me.chbMethod9MultiAvg1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAvg1.Location = New System.Drawing.Point(50, 123)
        Me.chbMethod9MultiAvg1.Name = "chbMethod9MultiAvg1"
        Me.chbMethod9MultiAvg1.Size = New System.Drawing.Size(83, 17)
        Me.chbMethod9MultiAvg1.TabIndex = 24
        Me.chbMethod9MultiAvg1.Text = "6 Min Avg 1"
        Me.chbMethod9MultiAvg1.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiMaxOpCapacityUnit
        '
        Me.chbMethod9MultiMaxOpCapacityUnit.AutoSize = True
        Me.chbMethod9MultiMaxOpCapacityUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiMaxOpCapacityUnit.Location = New System.Drawing.Point(639, 5)
        Me.chbMethod9MultiMaxOpCapacityUnit.Name = "chbMethod9MultiMaxOpCapacityUnit"
        Me.chbMethod9MultiMaxOpCapacityUnit.Size = New System.Drawing.Size(129, 17)
        Me.chbMethod9MultiMaxOpCapacityUnit.TabIndex = 20
        Me.chbMethod9MultiMaxOpCapacityUnit.Text = "Max Op Capacity Unit"
        Me.chbMethod9MultiMaxOpCapacityUnit.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiMaxOpCapacity1
        '
        Me.chbMethod9MultiMaxOpCapacity1.AutoSize = True
        Me.chbMethod9MultiMaxOpCapacity1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiMaxOpCapacity1.Location = New System.Drawing.Point(17, 5)
        Me.chbMethod9MultiMaxOpCapacity1.Name = "chbMethod9MultiMaxOpCapacity1"
        Me.chbMethod9MultiMaxOpCapacity1.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiMaxOpCapacity1.TabIndex = 19
        Me.chbMethod9MultiMaxOpCapacity1.Text = "Max Op Capacity 1"
        Me.chbMethod9MultiMaxOpCapacity1.UseVisualStyleBackColor = True
        '
        'chbMethod9MultiAllowEmiss1
        '
        Me.chbMethod9MultiAllowEmiss1.AutoSize = True
        Me.chbMethod9MultiAllowEmiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod9MultiAllowEmiss1.Location = New System.Drawing.Point(17, 39)
        Me.chbMethod9MultiAllowEmiss1.Name = "chbMethod9MultiAllowEmiss1"
        Me.chbMethod9MultiAllowEmiss1.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod9MultiAllowEmiss1.TabIndex = 21
        Me.chbMethod9MultiAllowEmiss1.Text = "Allow Emiss Rate 1"
        Me.chbMethod9MultiAllowEmiss1.UseVisualStyleBackColor = True
        '
        'TPMemorandum
        '
        Me.TPMemorandum.Controls.Add(Me.chbMemoAppRequire)
        Me.TPMemorandum.Controls.Add(Me.TCMemorandum)
        Me.TPMemorandum.Location = New System.Drawing.Point(4, 22)
        Me.TPMemorandum.Name = "TPMemorandum"
        Me.TPMemorandum.Size = New System.Drawing.Size(786, 315)
        Me.TPMemorandum.TabIndex = 7
        Me.TPMemorandum.Text = "Memorandum"
        Me.TPMemorandum.UseVisualStyleBackColor = True
        '
        'chbMemoAppRequire
        '
        Me.chbMemoAppRequire.AutoSize = True
        Me.chbMemoAppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoAppRequire.Location = New System.Drawing.Point(5, 5)
        Me.chbMemoAppRequire.Name = "chbMemoAppRequire"
        Me.chbMemoAppRequire.Size = New System.Drawing.Size(138, 17)
        Me.chbMemoAppRequire.TabIndex = 47
        Me.chbMemoAppRequire.Text = "Applicable Requirement"
        Me.chbMemoAppRequire.UseVisualStyleBackColor = True
        '
        'TCMemorandum
        '
        Me.TCMemorandum.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TCMemorandum.Controls.Add(Me.TPStandard)
        Me.TCMemorandum.Controls.Add(Me.TPToFile)
        Me.TCMemorandum.Controls.Add(Me.TPPTE)
        Me.TCMemorandum.Location = New System.Drawing.Point(0, 26)
        Me.TCMemorandum.Name = "TCMemorandum"
        Me.TCMemorandum.SelectedIndex = 0
        Me.TCMemorandum.Size = New System.Drawing.Size(761, 293)
        Me.TCMemorandum.TabIndex = 0
        '
        'TPStandard
        '
        Me.TPStandard.Controls.Add(Me.chbMemoStandardMemo)
        Me.TPStandard.Location = New System.Drawing.Point(4, 22)
        Me.TPStandard.Name = "TPStandard"
        Me.TPStandard.Padding = New System.Windows.Forms.Padding(3)
        Me.TPStandard.Size = New System.Drawing.Size(753, 267)
        Me.TPStandard.TabIndex = 0
        Me.TPStandard.Text = "Standard"
        Me.TPStandard.UseVisualStyleBackColor = True
        '
        'chbMemoStandardMemo
        '
        Me.chbMemoStandardMemo.AutoSize = True
        Me.chbMemoStandardMemo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoStandardMemo.Location = New System.Drawing.Point(5, 5)
        Me.chbMemoStandardMemo.Name = "chbMemoStandardMemo"
        Me.chbMemoStandardMemo.Size = New System.Drawing.Size(90, 17)
        Me.chbMemoStandardMemo.TabIndex = 47
        Me.chbMemoStandardMemo.Text = "Memorandum"
        Me.chbMemoStandardMemo.UseVisualStyleBackColor = True
        '
        'TPToFile
        '
        Me.TPToFile.Controls.Add(Me.chbMemoToFileMemo)
        Me.TPToFile.Controls.Add(Me.chbMemoToFileSerial)
        Me.TPToFile.Controls.Add(Me.chbMemoToFileManufacture)
        Me.TPToFile.Location = New System.Drawing.Point(4, 22)
        Me.TPToFile.Name = "TPToFile"
        Me.TPToFile.Padding = New System.Windows.Forms.Padding(3)
        Me.TPToFile.Size = New System.Drawing.Size(753, 267)
        Me.TPToFile.TabIndex = 1
        Me.TPToFile.Text = "To File"
        Me.TPToFile.UseVisualStyleBackColor = True
        '
        'chbMemoToFileMemo
        '
        Me.chbMemoToFileMemo.AutoSize = True
        Me.chbMemoToFileMemo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoToFileMemo.Location = New System.Drawing.Point(42, 39)
        Me.chbMemoToFileMemo.Name = "chbMemoToFileMemo"
        Me.chbMemoToFileMemo.Size = New System.Drawing.Size(90, 17)
        Me.chbMemoToFileMemo.TabIndex = 49
        Me.chbMemoToFileMemo.Text = "Memorandum"
        Me.chbMemoToFileMemo.UseVisualStyleBackColor = True
        '
        'chbMemoToFileSerial
        '
        Me.chbMemoToFileSerial.AutoSize = True
        Me.chbMemoToFileSerial.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoToFileSerial.Location = New System.Drawing.Point(42, 22)
        Me.chbMemoToFileSerial.Name = "chbMemoToFileSerial"
        Me.chbMemoToFileSerial.Size = New System.Drawing.Size(90, 17)
        Me.chbMemoToFileSerial.TabIndex = 48
        Me.chbMemoToFileSerial.Text = "Monitor Serial"
        Me.chbMemoToFileSerial.UseVisualStyleBackColor = True
        '
        'chbMemoToFileManufacture
        '
        Me.chbMemoToFileManufacture.AutoSize = True
        Me.chbMemoToFileManufacture.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoToFileManufacture.Location = New System.Drawing.Point(5, 5)
        Me.chbMemoToFileManufacture.Name = "chbMemoToFileManufacture"
        Me.chbMemoToFileManufacture.Size = New System.Drawing.Size(127, 17)
        Me.chbMemoToFileManufacture.TabIndex = 47
        Me.chbMemoToFileManufacture.Text = "Monitor Manufacturer"
        Me.chbMemoToFileManufacture.UseVisualStyleBackColor = True
        '
        'TPPTE
        '
        Me.TPPTE.Controls.Add(Me.chbMemoPTEControlEquip)
        Me.TPPTE.Controls.Add(Me.chbMemoPTEMemo)
        Me.TPPTE.Controls.Add(Me.chbMemoPTEAllowEmiss2)
        Me.TPPTE.Controls.Add(Me.chbMemoPTEAllowEmiss3)
        Me.TPPTE.Controls.Add(Me.chbMemoPTEOpCapacity)
        Me.TPPTE.Controls.Add(Me.chbMemoPTEMaxOpCapacity)
        Me.TPPTE.Controls.Add(Me.chbMemoPTEAllowEmiss1)
        Me.TPPTE.Location = New System.Drawing.Point(4, 22)
        Me.TPPTE.Name = "TPPTE"
        Me.TPPTE.Size = New System.Drawing.Size(753, 267)
        Me.TPPTE.TabIndex = 2
        Me.TPPTE.Text = "PTE"
        Me.TPPTE.UseVisualStyleBackColor = True
        '
        'chbMemoPTEControlEquip
        '
        Me.chbMemoPTEControlEquip.AutoSize = True
        Me.chbMemoPTEControlEquip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoPTEControlEquip.Location = New System.Drawing.Point(40, 43)
        Me.chbMemoPTEControlEquip.Name = "chbMemoPTEControlEquip"
        Me.chbMemoPTEControlEquip.Size = New System.Drawing.Size(128, 30)
        Me.chbMemoPTEControlEquip.TabIndex = 65
        Me.chbMemoPTEControlEquip.Text = "Control Equipment " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  and Monitoring Data"
        Me.chbMemoPTEControlEquip.UseVisualStyleBackColor = True
        '
        'chbMemoPTEMemo
        '
        Me.chbMemoPTEMemo.AutoSize = True
        Me.chbMemoPTEMemo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoPTEMemo.Location = New System.Drawing.Point(78, 79)
        Me.chbMemoPTEMemo.Name = "chbMemoPTEMemo"
        Me.chbMemoPTEMemo.Size = New System.Drawing.Size(90, 17)
        Me.chbMemoPTEMemo.TabIndex = 15
        Me.chbMemoPTEMemo.Text = "Memorandum"
        Me.chbMemoPTEMemo.UseVisualStyleBackColor = True
        '
        'chbMemoPTEAllowEmiss2
        '
        Me.chbMemoPTEAllowEmiss2.AutoSize = True
        Me.chbMemoPTEAllowEmiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoPTEAllowEmiss2.Location = New System.Drawing.Point(176, 22)
        Me.chbMemoPTEAllowEmiss2.Name = "chbMemoPTEAllowEmiss2"
        Me.chbMemoPTEAllowEmiss2.Size = New System.Drawing.Size(150, 17)
        Me.chbMemoPTEAllowEmiss2.TabIndex = 12
        Me.chbMemoPTEAllowEmiss2.Text = "Allowable Emission Rate 2"
        Me.chbMemoPTEAllowEmiss2.UseVisualStyleBackColor = True
        '
        'chbMemoPTEAllowEmiss3
        '
        Me.chbMemoPTEAllowEmiss3.AutoSize = True
        Me.chbMemoPTEAllowEmiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoPTEAllowEmiss3.Location = New System.Drawing.Point(331, 22)
        Me.chbMemoPTEAllowEmiss3.Name = "chbMemoPTEAllowEmiss3"
        Me.chbMemoPTEAllowEmiss3.Size = New System.Drawing.Size(150, 17)
        Me.chbMemoPTEAllowEmiss3.TabIndex = 11
        Me.chbMemoPTEAllowEmiss3.Text = "Allowable Emission Rate 3"
        Me.chbMemoPTEAllowEmiss3.UseVisualStyleBackColor = True
        '
        'chbMemoPTEOpCapacity
        '
        Me.chbMemoPTEOpCapacity.AutoSize = True
        Me.chbMemoPTEOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoPTEOpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbMemoPTEOpCapacity.Name = "chbMemoPTEOpCapacity"
        Me.chbMemoPTEOpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbMemoPTEOpCapacity.TabIndex = 10
        Me.chbMemoPTEOpCapacity.Text = "Operating Capacity"
        Me.chbMemoPTEOpCapacity.UseVisualStyleBackColor = True
        '
        'chbMemoPTEMaxOpCapacity
        '
        Me.chbMemoPTEMaxOpCapacity.AutoSize = True
        Me.chbMemoPTEMaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoPTEMaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbMemoPTEMaxOpCapacity.Name = "chbMemoPTEMaxOpCapacity"
        Me.chbMemoPTEMaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbMemoPTEMaxOpCapacity.TabIndex = 9
        Me.chbMemoPTEMaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbMemoPTEMaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbMemoPTEAllowEmiss1
        '
        Me.chbMemoPTEAllowEmiss1.AutoSize = True
        Me.chbMemoPTEAllowEmiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMemoPTEAllowEmiss1.Location = New System.Drawing.Point(15, 22)
        Me.chbMemoPTEAllowEmiss1.Name = "chbMemoPTEAllowEmiss1"
        Me.chbMemoPTEAllowEmiss1.Size = New System.Drawing.Size(153, 17)
        Me.chbMemoPTEAllowEmiss1.TabIndex = 13
        Me.chbMemoPTEAllowEmiss1.Text = "Allowable Emission Rate 1 "
        Me.chbMemoPTEAllowEmiss1.UseVisualStyleBackColor = True
        '
        'TPRATA
        '
        Me.TPRATA.Controls.Add(Me.chbRATAOtherInformation)
        Me.TPRATA.Controls.Add(Me.chbRATACMS9)
        Me.TPRATA.Controls.Add(Me.chbRATACMS12)
        Me.TPRATA.Controls.Add(Me.chbRATACMS11)
        Me.TPRATA.Controls.Add(Me.chbRATACMS10)
        Me.TPRATA.Controls.Add(Me.chbRATACMS8)
        Me.TPRATA.Controls.Add(Me.chbRATACMS7)
        Me.TPRATA.Controls.Add(Me.chbRATACMS5)
        Me.TPRATA.Controls.Add(Me.chbRATACMS6)
        Me.TPRATA.Controls.Add(Me.chbRATACMS4)
        Me.TPRATA.Controls.Add(Me.chbRATACMS3)
        Me.TPRATA.Controls.Add(Me.chbRATACMS2)
        Me.TPRATA.Controls.Add(Me.chbRATACMS1)
        Me.TPRATA.Controls.Add(Me.chbRATARef9)
        Me.TPRATA.Controls.Add(Me.chbRATARef12)
        Me.TPRATA.Controls.Add(Me.chbRATARef11)
        Me.TPRATA.Controls.Add(Me.chbRATARef10)
        Me.TPRATA.Controls.Add(Me.chbRATARef8)
        Me.TPRATA.Controls.Add(Me.chbRATARef7)
        Me.TPRATA.Controls.Add(Me.chbRATARef5)
        Me.TPRATA.Controls.Add(Me.chbRATARef6)
        Me.TPRATA.Controls.Add(Me.chbRATARef4)
        Me.TPRATA.Controls.Add(Me.chbRATARef3)
        Me.TPRATA.Controls.Add(Me.chbRATARef2)
        Me.TPRATA.Controls.Add(Me.chbRATARef1)
        Me.TPRATA.Controls.Add(Me.chbRATARelativeAcc)
        Me.TPRATA.Controls.Add(Me.chbRATAStatement)
        Me.TPRATA.Controls.Add(Me.chbRATAUnits)
        Me.TPRATA.Controls.Add(Me.chbRATADiluent)
        Me.TPRATA.Controls.Add(Me.chbRATAAppStandard)
        Me.TPRATA.Controls.Add(Me.chbRATAAppRegulation)
        Me.TPRATA.Location = New System.Drawing.Point(4, 22)
        Me.TPRATA.Name = "TPRATA"
        Me.TPRATA.Size = New System.Drawing.Size(786, 315)
        Me.TPRATA.TabIndex = 8
        Me.TPRATA.Text = "RATA"
        Me.TPRATA.UseVisualStyleBackColor = True
        '
        'chbRATAOtherInformation
        '
        Me.chbRATAOtherInformation.AutoSize = True
        Me.chbRATAOtherInformation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATAOtherInformation.Location = New System.Drawing.Point(366, 243)
        Me.chbRATAOtherInformation.Name = "chbRATAOtherInformation"
        Me.chbRATAOtherInformation.Size = New System.Drawing.Size(107, 17)
        Me.chbRATAOtherInformation.TabIndex = 39
        Me.chbRATAOtherInformation.Text = "Other Information"
        Me.chbRATAOtherInformation.UseVisualStyleBackColor = True
        '
        'chbRATACMS9
        '
        Me.chbRATACMS9.AutoSize = True
        Me.chbRATACMS9.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS9.Location = New System.Drawing.Point(171, 192)
        Me.chbRATACMS9.Name = "chbRATACMS9"
        Me.chbRATACMS9.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS9.TabIndex = 38
        Me.chbRATACMS9.Text = "CMS 9"
        Me.chbRATACMS9.UseVisualStyleBackColor = True
        '
        'chbRATACMS12
        '
        Me.chbRATACMS12.AutoSize = True
        Me.chbRATACMS12.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS12.Location = New System.Drawing.Point(165, 243)
        Me.chbRATACMS12.Name = "chbRATACMS12"
        Me.chbRATACMS12.Size = New System.Drawing.Size(64, 17)
        Me.chbRATACMS12.TabIndex = 37
        Me.chbRATACMS12.Text = "CMS 12"
        Me.chbRATACMS12.UseVisualStyleBackColor = True
        '
        'chbRATACMS11
        '
        Me.chbRATACMS11.AutoSize = True
        Me.chbRATACMS11.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS11.Location = New System.Drawing.Point(165, 226)
        Me.chbRATACMS11.Name = "chbRATACMS11"
        Me.chbRATACMS11.Size = New System.Drawing.Size(64, 17)
        Me.chbRATACMS11.TabIndex = 36
        Me.chbRATACMS11.Text = "CMS 11"
        Me.chbRATACMS11.UseVisualStyleBackColor = True
        '
        'chbRATACMS10
        '
        Me.chbRATACMS10.AutoSize = True
        Me.chbRATACMS10.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS10.Location = New System.Drawing.Point(165, 209)
        Me.chbRATACMS10.Name = "chbRATACMS10"
        Me.chbRATACMS10.Size = New System.Drawing.Size(64, 17)
        Me.chbRATACMS10.TabIndex = 35
        Me.chbRATACMS10.Text = "CMS 10"
        Me.chbRATACMS10.UseVisualStyleBackColor = True
        '
        'chbRATACMS8
        '
        Me.chbRATACMS8.AutoSize = True
        Me.chbRATACMS8.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS8.Location = New System.Drawing.Point(171, 175)
        Me.chbRATACMS8.Name = "chbRATACMS8"
        Me.chbRATACMS8.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS8.TabIndex = 34
        Me.chbRATACMS8.Text = "CMS 8"
        Me.chbRATACMS8.UseVisualStyleBackColor = True
        '
        'chbRATACMS7
        '
        Me.chbRATACMS7.AutoSize = True
        Me.chbRATACMS7.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS7.Location = New System.Drawing.Point(171, 158)
        Me.chbRATACMS7.Name = "chbRATACMS7"
        Me.chbRATACMS7.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS7.TabIndex = 33
        Me.chbRATACMS7.Text = "CMS 7"
        Me.chbRATACMS7.UseVisualStyleBackColor = True
        '
        'chbRATACMS5
        '
        Me.chbRATACMS5.AutoSize = True
        Me.chbRATACMS5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS5.Location = New System.Drawing.Point(171, 124)
        Me.chbRATACMS5.Name = "chbRATACMS5"
        Me.chbRATACMS5.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS5.TabIndex = 32
        Me.chbRATACMS5.Text = "CMS 5"
        Me.chbRATACMS5.UseVisualStyleBackColor = True
        '
        'chbRATACMS6
        '
        Me.chbRATACMS6.AutoSize = True
        Me.chbRATACMS6.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS6.Location = New System.Drawing.Point(171, 141)
        Me.chbRATACMS6.Name = "chbRATACMS6"
        Me.chbRATACMS6.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS6.TabIndex = 31
        Me.chbRATACMS6.Text = "CMS 6"
        Me.chbRATACMS6.UseVisualStyleBackColor = True
        '
        'chbRATACMS4
        '
        Me.chbRATACMS4.AutoSize = True
        Me.chbRATACMS4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS4.Location = New System.Drawing.Point(171, 107)
        Me.chbRATACMS4.Name = "chbRATACMS4"
        Me.chbRATACMS4.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS4.TabIndex = 30
        Me.chbRATACMS4.Text = "CMS 4"
        Me.chbRATACMS4.UseVisualStyleBackColor = True
        '
        'chbRATACMS3
        '
        Me.chbRATACMS3.AutoSize = True
        Me.chbRATACMS3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS3.Location = New System.Drawing.Point(171, 90)
        Me.chbRATACMS3.Name = "chbRATACMS3"
        Me.chbRATACMS3.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS3.TabIndex = 29
        Me.chbRATACMS3.Text = "CMS 3"
        Me.chbRATACMS3.UseVisualStyleBackColor = True
        '
        'chbRATACMS2
        '
        Me.chbRATACMS2.AutoSize = True
        Me.chbRATACMS2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS2.Location = New System.Drawing.Point(171, 73)
        Me.chbRATACMS2.Name = "chbRATACMS2"
        Me.chbRATACMS2.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS2.TabIndex = 28
        Me.chbRATACMS2.Text = "CMS 2"
        Me.chbRATACMS2.UseVisualStyleBackColor = True
        '
        'chbRATACMS1
        '
        Me.chbRATACMS1.AutoSize = True
        Me.chbRATACMS1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATACMS1.Location = New System.Drawing.Point(171, 56)
        Me.chbRATACMS1.Name = "chbRATACMS1"
        Me.chbRATACMS1.Size = New System.Drawing.Size(58, 17)
        Me.chbRATACMS1.TabIndex = 27
        Me.chbRATACMS1.Text = "CMS 1"
        Me.chbRATACMS1.UseVisualStyleBackColor = True
        '
        'chbRATARef9
        '
        Me.chbRATARef9.AutoSize = True
        Me.chbRATARef9.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef9.Location = New System.Drawing.Point(13, 192)
        Me.chbRATARef9.Name = "chbRATARef9"
        Me.chbRATARef9.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef9.TabIndex = 26
        Me.chbRATARef9.Text = "Reference Method 9"
        Me.chbRATARef9.UseVisualStyleBackColor = True
        '
        'chbRATARef12
        '
        Me.chbRATARef12.AutoSize = True
        Me.chbRATARef12.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef12.Location = New System.Drawing.Point(7, 243)
        Me.chbRATARef12.Name = "chbRATARef12"
        Me.chbRATARef12.Size = New System.Drawing.Size(130, 17)
        Me.chbRATARef12.TabIndex = 25
        Me.chbRATARef12.Text = "Reference Method 12"
        Me.chbRATARef12.UseVisualStyleBackColor = True
        '
        'chbRATARef11
        '
        Me.chbRATARef11.AutoSize = True
        Me.chbRATARef11.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef11.Location = New System.Drawing.Point(7, 226)
        Me.chbRATARef11.Name = "chbRATARef11"
        Me.chbRATARef11.Size = New System.Drawing.Size(130, 17)
        Me.chbRATARef11.TabIndex = 24
        Me.chbRATARef11.Text = "Reference Method 11"
        Me.chbRATARef11.UseVisualStyleBackColor = True
        '
        'chbRATARef10
        '
        Me.chbRATARef10.AutoSize = True
        Me.chbRATARef10.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef10.Location = New System.Drawing.Point(7, 209)
        Me.chbRATARef10.Name = "chbRATARef10"
        Me.chbRATARef10.Size = New System.Drawing.Size(130, 17)
        Me.chbRATARef10.TabIndex = 23
        Me.chbRATARef10.Text = "Reference Method 10"
        Me.chbRATARef10.UseVisualStyleBackColor = True
        '
        'chbRATARef8
        '
        Me.chbRATARef8.AutoSize = True
        Me.chbRATARef8.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef8.Location = New System.Drawing.Point(13, 175)
        Me.chbRATARef8.Name = "chbRATARef8"
        Me.chbRATARef8.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef8.TabIndex = 22
        Me.chbRATARef8.Text = "Reference Method 8"
        Me.chbRATARef8.UseVisualStyleBackColor = True
        '
        'chbRATARef7
        '
        Me.chbRATARef7.AutoSize = True
        Me.chbRATARef7.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef7.Location = New System.Drawing.Point(13, 158)
        Me.chbRATARef7.Name = "chbRATARef7"
        Me.chbRATARef7.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef7.TabIndex = 21
        Me.chbRATARef7.Text = "Reference Method 7"
        Me.chbRATARef7.UseVisualStyleBackColor = True
        '
        'chbRATARef5
        '
        Me.chbRATARef5.AutoSize = True
        Me.chbRATARef5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef5.Location = New System.Drawing.Point(13, 124)
        Me.chbRATARef5.Name = "chbRATARef5"
        Me.chbRATARef5.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef5.TabIndex = 20
        Me.chbRATARef5.Text = "Reference Method 5"
        Me.chbRATARef5.UseVisualStyleBackColor = True
        '
        'chbRATARef6
        '
        Me.chbRATARef6.AutoSize = True
        Me.chbRATARef6.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef6.Location = New System.Drawing.Point(13, 141)
        Me.chbRATARef6.Name = "chbRATARef6"
        Me.chbRATARef6.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef6.TabIndex = 19
        Me.chbRATARef6.Text = "Reference Method 6"
        Me.chbRATARef6.UseVisualStyleBackColor = True
        '
        'chbRATARef4
        '
        Me.chbRATARef4.AutoSize = True
        Me.chbRATARef4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef4.Location = New System.Drawing.Point(13, 107)
        Me.chbRATARef4.Name = "chbRATARef4"
        Me.chbRATARef4.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef4.TabIndex = 18
        Me.chbRATARef4.Text = "Reference Method 4"
        Me.chbRATARef4.UseVisualStyleBackColor = True
        '
        'chbRATARef3
        '
        Me.chbRATARef3.AutoSize = True
        Me.chbRATARef3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef3.Location = New System.Drawing.Point(13, 90)
        Me.chbRATARef3.Name = "chbRATARef3"
        Me.chbRATARef3.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef3.TabIndex = 17
        Me.chbRATARef3.Text = "Reference Method 3"
        Me.chbRATARef3.UseVisualStyleBackColor = True
        '
        'chbRATARef2
        '
        Me.chbRATARef2.AutoSize = True
        Me.chbRATARef2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef2.Location = New System.Drawing.Point(13, 73)
        Me.chbRATARef2.Name = "chbRATARef2"
        Me.chbRATARef2.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef2.TabIndex = 16
        Me.chbRATARef2.Text = "Reference Method 2"
        Me.chbRATARef2.UseVisualStyleBackColor = True
        '
        'chbRATARef1
        '
        Me.chbRATARef1.AutoSize = True
        Me.chbRATARef1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARef1.Location = New System.Drawing.Point(13, 56)
        Me.chbRATARef1.Name = "chbRATARef1"
        Me.chbRATARef1.Size = New System.Drawing.Size(124, 17)
        Me.chbRATARef1.TabIndex = 15
        Me.chbRATARef1.Text = "Reference Method 1"
        Me.chbRATARef1.UseVisualStyleBackColor = True
        '
        'chbRATARelativeAcc
        '
        Me.chbRATARelativeAcc.AutoSize = True
        Me.chbRATARelativeAcc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATARelativeAcc.Location = New System.Drawing.Point(360, 56)
        Me.chbRATARelativeAcc.Name = "chbRATARelativeAcc"
        Me.chbRATARelativeAcc.Size = New System.Drawing.Size(113, 17)
        Me.chbRATARelativeAcc.TabIndex = 12
        Me.chbRATARelativeAcc.Text = "Relative Accuracy"
        Me.chbRATARelativeAcc.UseVisualStyleBackColor = True
        '
        'chbRATAStatement
        '
        Me.chbRATAStatement.AutoSize = True
        Me.chbRATAStatement.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATAStatement.Location = New System.Drawing.Point(367, 90)
        Me.chbRATAStatement.Name = "chbRATAStatement"
        Me.chbRATAStatement.Size = New System.Drawing.Size(106, 17)
        Me.chbRATAStatement.TabIndex = 11
        Me.chbRATAStatement.Text = "RATA Statement"
        Me.chbRATAStatement.UseVisualStyleBackColor = True
        '
        'chbRATAUnits
        '
        Me.chbRATAUnits.AutoSize = True
        Me.chbRATAUnits.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATAUnits.Location = New System.Drawing.Point(236, 56)
        Me.chbRATAUnits.Name = "chbRATAUnits"
        Me.chbRATAUnits.Size = New System.Drawing.Size(82, 17)
        Me.chbRATAUnits.TabIndex = 14
        Me.chbRATAUnits.Text = "RATA Units"
        Me.chbRATAUnits.UseVisualStyleBackColor = True
        '
        'chbRATADiluent
        '
        Me.chbRATADiluent.AutoSize = True
        Me.chbRATADiluent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATADiluent.Location = New System.Drawing.Point(209, 5)
        Me.chbRATADiluent.Name = "chbRATADiluent"
        Me.chbRATADiluent.Size = New System.Drawing.Size(109, 17)
        Me.chbRATADiluent.TabIndex = 10
        Me.chbRATADiluent.Text = "Diluent Monitored"
        Me.chbRATADiluent.UseVisualStyleBackColor = True
        '
        'chbRATAAppStandard
        '
        Me.chbRATAAppStandard.AutoSize = True
        Me.chbRATAAppStandard.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATAAppStandard.Location = New System.Drawing.Point(16, 5)
        Me.chbRATAAppStandard.Name = "chbRATAAppStandard"
        Me.chbRATAAppStandard.Size = New System.Drawing.Size(121, 17)
        Me.chbRATAAppStandard.TabIndex = 9
        Me.chbRATAAppStandard.Text = "Applicable Standard"
        Me.chbRATAAppStandard.UseVisualStyleBackColor = True
        '
        'chbRATAAppRegulation
        '
        Me.chbRATAAppRegulation.AutoSize = True
        Me.chbRATAAppRegulation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbRATAAppRegulation.Location = New System.Drawing.Point(8, 22)
        Me.chbRATAAppRegulation.Name = "chbRATAAppRegulation"
        Me.chbRATAAppRegulation.Size = New System.Drawing.Size(129, 17)
        Me.chbRATAAppRegulation.TabIndex = 13
        Me.chbRATAAppRegulation.Text = "Applicable Regulation"
        Me.chbRATAAppRegulation.UseVisualStyleBackColor = True
        '
        'TPTwoStack
        '
        Me.TPTwoStack.Controls.Add(Me.chbTwoStackControlEquip)
        Me.TPTwoStack.Controls.Add(Me.chbTwoStackOtherInfo)
        Me.TPTwoStack.Controls.Add(Me.TCTwoStack)
        Me.TPTwoStack.Controls.Add(Me.chbTwoStackAppRequire)
        Me.TPTwoStack.Controls.Add(Me.chbTwoStackAllowEmiss2)
        Me.TPTwoStack.Controls.Add(Me.chbTwoStackAllowEmiss3)
        Me.TPTwoStack.Controls.Add(Me.chbTwoStackOpCapacity)
        Me.TPTwoStack.Controls.Add(Me.chbTwoStackMaxOpCapacity)
        Me.TPTwoStack.Controls.Add(Me.chbTwoStackAllowEmiss1)
        Me.TPTwoStack.Location = New System.Drawing.Point(4, 22)
        Me.TPTwoStack.Name = "TPTwoStack"
        Me.TPTwoStack.Size = New System.Drawing.Size(786, 315)
        Me.TPTwoStack.TabIndex = 9
        Me.TPTwoStack.Text = "TwoStack"
        Me.TPTwoStack.UseVisualStyleBackColor = True
        '
        'chbTwoStackControlEquip
        '
        Me.chbTwoStackControlEquip.AutoSize = True
        Me.chbTwoStackControlEquip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackControlEquip.Location = New System.Drawing.Point(40, 57)
        Me.chbTwoStackControlEquip.Name = "chbTwoStackControlEquip"
        Me.chbTwoStackControlEquip.Size = New System.Drawing.Size(128, 30)
        Me.chbTwoStackControlEquip.TabIndex = 72
        Me.chbTwoStackControlEquip.Text = "Control Equipment " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  and Monitoring Data"
        Me.chbTwoStackControlEquip.UseVisualStyleBackColor = True
        '
        'chbTwoStackOtherInfo
        '
        Me.chbTwoStackOtherInfo.AutoSize = True
        Me.chbTwoStackOtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackOtherInfo.Location = New System.Drawing.Point(5, 293)
        Me.chbTwoStackOtherInfo.Name = "chbTwoStackOtherInfo"
        Me.chbTwoStackOtherInfo.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackOtherInfo.TabIndex = 71
        Me.chbTwoStackOtherInfo.Text = "Other Info."
        Me.chbTwoStackOtherInfo.UseVisualStyleBackColor = True
        '
        'TCTwoStack
        '
        Me.TCTwoStack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TCTwoStack.Controls.Add(Me.TPTwoStackStandard)
        Me.TCTwoStack.Controls.Add(Me.TPDRE)
        Me.TCTwoStack.Location = New System.Drawing.Point(3, 89)
        Me.TCTwoStack.Name = "TCTwoStack"
        Me.TCTwoStack.SelectedIndex = 0
        Me.TCTwoStack.Size = New System.Drawing.Size(758, 202)
        Me.TCTwoStack.TabIndex = 16
        '
        'TPTwoStackStandard
        '
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPercentAllow)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTotalAvg)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTotal3)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTotal2)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTotal1)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmissUnit)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPollUnit)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmissAvg2)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmissAvg1)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPollAvg2)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPollAvg1)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPoll3b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmiss3b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandDSCFM3b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandRun3b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandACFM3b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTemp3b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandMoist3b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPoll2b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmiss2b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandDSCFM2b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandRun2b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandACFM2b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTemp2b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandMoist2b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPoll1b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmiss1b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandDSCFM1b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandRun1b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandACFM1b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTemp1b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandMoist1b)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPoll3a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmiss3a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandDSCFM3a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandRun3a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandACFM3a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTemp3a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandMoist3a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPoll2a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmiss2a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandDSCFM2a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandRun2a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandACFM2a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTemp2a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandMoist2a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandName2)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandName1)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandPoll1a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandEmiss1a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandDSCFM1a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandRun1a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandACFM1a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandTemp1a)
        Me.TPTwoStackStandard.Controls.Add(Me.chbTwoStackStandMoist1a)
        Me.TPTwoStackStandard.Location = New System.Drawing.Point(4, 22)
        Me.TPTwoStackStandard.Name = "TPTwoStackStandard"
        Me.TPTwoStackStandard.Padding = New System.Windows.Forms.Padding(3)
        Me.TPTwoStackStandard.Size = New System.Drawing.Size(750, 176)
        Me.TPTwoStackStandard.TabIndex = 0
        Me.TPTwoStackStandard.Text = "Standard"
        Me.TPTwoStackStandard.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPercentAllow
        '
        Me.chbTwoStackStandPercentAllow.AutoSize = True
        Me.chbTwoStackStandPercentAllow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPercentAllow.Location = New System.Drawing.Point(33, 159)
        Me.chbTwoStackStandPercentAllow.Name = "chbTwoStackStandPercentAllow"
        Me.chbTwoStackStandPercentAllow.Size = New System.Drawing.Size(111, 17)
        Me.chbTwoStackStandPercentAllow.TabIndex = 72
        Me.chbTwoStackStandPercentAllow.Text = "Percent Allowable"
        Me.chbTwoStackStandPercentAllow.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTotalAvg
        '
        Me.chbTwoStackStandTotalAvg.AutoSize = True
        Me.chbTwoStackStandTotalAvg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTotalAvg.Location = New System.Drawing.Point(402, 142)
        Me.chbTwoStackStandTotalAvg.Name = "chbTwoStackStandTotalAvg"
        Me.chbTwoStackStandTotalAvg.Size = New System.Drawing.Size(72, 17)
        Me.chbTwoStackStandTotalAvg.TabIndex = 71
        Me.chbTwoStackStandTotalAvg.Text = "Total Avg"
        Me.chbTwoStackStandTotalAvg.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTotal3
        '
        Me.chbTwoStackStandTotal3.AutoSize = True
        Me.chbTwoStackStandTotal3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTotal3.Location = New System.Drawing.Point(228, 142)
        Me.chbTwoStackStandTotal3.Name = "chbTwoStackStandTotal3"
        Me.chbTwoStackStandTotal3.Size = New System.Drawing.Size(59, 17)
        Me.chbTwoStackStandTotal3.TabIndex = 70
        Me.chbTwoStackStandTotal3.Text = "Total 3"
        Me.chbTwoStackStandTotal3.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTotal2
        '
        Me.chbTwoStackStandTotal2.AutoSize = True
        Me.chbTwoStackStandTotal2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTotal2.Location = New System.Drawing.Point(133, 142)
        Me.chbTwoStackStandTotal2.Name = "chbTwoStackStandTotal2"
        Me.chbTwoStackStandTotal2.Size = New System.Drawing.Size(59, 17)
        Me.chbTwoStackStandTotal2.TabIndex = 69
        Me.chbTwoStackStandTotal2.Text = "Total 2"
        Me.chbTwoStackStandTotal2.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTotal1
        '
        Me.chbTwoStackStandTotal1.AutoSize = True
        Me.chbTwoStackStandTotal1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTotal1.Location = New System.Drawing.Point(39, 142)
        Me.chbTwoStackStandTotal1.Name = "chbTwoStackStandTotal1"
        Me.chbTwoStackStandTotal1.Size = New System.Drawing.Size(59, 17)
        Me.chbTwoStackStandTotal1.TabIndex = 68
        Me.chbTwoStackStandTotal1.Text = "Total 1"
        Me.chbTwoStackStandTotal1.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmissUnit
        '
        Me.chbTwoStackStandEmissUnit.AutoSize = True
        Me.chbTwoStackStandEmissUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmissUnit.Location = New System.Drawing.Point(578, 125)
        Me.chbTwoStackStandEmissUnit.Name = "chbTwoStackStandEmissUnit"
        Me.chbTwoStackStandEmissUnit.Size = New System.Drawing.Size(75, 17)
        Me.chbTwoStackStandEmissUnit.TabIndex = 67
        Me.chbTwoStackStandEmissUnit.Text = "Emiss Unit"
        Me.chbTwoStackStandEmissUnit.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPollUnit
        '
        Me.chbTwoStackStandPollUnit.AutoSize = True
        Me.chbTwoStackStandPollUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPollUnit.Location = New System.Drawing.Point(588, 108)
        Me.chbTwoStackStandPollUnit.Name = "chbTwoStackStandPollUnit"
        Me.chbTwoStackStandPollUnit.Size = New System.Drawing.Size(65, 17)
        Me.chbTwoStackStandPollUnit.TabIndex = 66
        Me.chbTwoStackStandPollUnit.Text = "Poll Unit"
        Me.chbTwoStackStandPollUnit.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmissAvg2
        '
        Me.chbTwoStackStandEmissAvg2.AutoSize = True
        Me.chbTwoStackStandEmissAvg2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmissAvg2.Location = New System.Drawing.Point(653, 142)
        Me.chbTwoStackStandEmissAvg2.Name = "chbTwoStackStandEmissAvg2"
        Me.chbTwoStackStandEmissAvg2.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackStandEmissAvg2.TabIndex = 65
        Me.chbTwoStackStandEmissAvg2.Text = "Emiss Avg 2"
        Me.chbTwoStackStandEmissAvg2.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmissAvg1
        '
        Me.chbTwoStackStandEmissAvg1.AutoSize = True
        Me.chbTwoStackStandEmissAvg1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmissAvg1.Location = New System.Drawing.Point(653, 125)
        Me.chbTwoStackStandEmissAvg1.Name = "chbTwoStackStandEmissAvg1"
        Me.chbTwoStackStandEmissAvg1.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackStandEmissAvg1.TabIndex = 64
        Me.chbTwoStackStandEmissAvg1.Text = "Emiss Avg 1"
        Me.chbTwoStackStandEmissAvg1.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPollAvg2
        '
        Me.chbTwoStackStandPollAvg2.AutoSize = True
        Me.chbTwoStackStandPollAvg2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPollAvg2.Location = New System.Drawing.Point(663, 108)
        Me.chbTwoStackStandPollAvg2.Name = "chbTwoStackStandPollAvg2"
        Me.chbTwoStackStandPollAvg2.Size = New System.Drawing.Size(74, 17)
        Me.chbTwoStackStandPollAvg2.TabIndex = 63
        Me.chbTwoStackStandPollAvg2.Text = "Poll Avg 2"
        Me.chbTwoStackStandPollAvg2.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPollAvg1
        '
        Me.chbTwoStackStandPollAvg1.AutoSize = True
        Me.chbTwoStackStandPollAvg1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPollAvg1.Location = New System.Drawing.Point(663, 91)
        Me.chbTwoStackStandPollAvg1.Name = "chbTwoStackStandPollAvg1"
        Me.chbTwoStackStandPollAvg1.Size = New System.Drawing.Size(74, 17)
        Me.chbTwoStackStandPollAvg1.TabIndex = 62
        Me.chbTwoStackStandPollAvg1.Text = "Poll Avg 1"
        Me.chbTwoStackStandPollAvg1.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPoll3b
        '
        Me.chbTwoStackStandPoll3b.AutoSize = True
        Me.chbTwoStackStandPoll3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPoll3b.Location = New System.Drawing.Point(492, 108)
        Me.chbTwoStackStandPoll3b.Name = "chbTwoStackStandPoll3b"
        Me.chbTwoStackStandPoll3b.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackStandPoll3b.TabIndex = 61
        Me.chbTwoStackStandPoll3b.Text = "Poll Conc 3b"
        Me.chbTwoStackStandPoll3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmiss3b
        '
        Me.chbTwoStackStandEmiss3b.AutoSize = True
        Me.chbTwoStackStandEmiss3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmiss3b.Location = New System.Drawing.Point(484, 125)
        Me.chbTwoStackStandEmiss3b.Name = "chbTwoStackStandEmiss3b"
        Me.chbTwoStackStandEmiss3b.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackStandEmiss3b.TabIndex = 60
        Me.chbTwoStackStandEmiss3b.Text = "Emiss Rate 3b"
        Me.chbTwoStackStandEmiss3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandDSCFM3b
        '
        Me.chbTwoStackStandDSCFM3b.AutoSize = True
        Me.chbTwoStackStandDSCFM3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandDSCFM3b.Location = New System.Drawing.Point(494, 91)
        Me.chbTwoStackStandDSCFM3b.Name = "chbTwoStackStandDSCFM3b"
        Me.chbTwoStackStandDSCFM3b.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackStandDSCFM3b.TabIndex = 59
        Me.chbTwoStackStandDSCFM3b.Text = "(DSCFM) 3b"
        Me.chbTwoStackStandDSCFM3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandRun3b
        '
        Me.chbTwoStackStandRun3b.AutoSize = True
        Me.chbTwoStackStandRun3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandRun3b.Location = New System.Drawing.Point(517, 23)
        Me.chbTwoStackStandRun3b.Name = "chbTwoStackStandRun3b"
        Me.chbTwoStackStandRun3b.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackStandRun3b.TabIndex = 55
        Me.chbTwoStackStandRun3b.Text = "Run 3b"
        Me.chbTwoStackStandRun3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandACFM3b
        '
        Me.chbTwoStackStandACFM3b.AutoSize = True
        Me.chbTwoStackStandACFM3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandACFM3b.Location = New System.Drawing.Point(502, 74)
        Me.chbTwoStackStandACFM3b.Name = "chbTwoStackStandACFM3b"
        Me.chbTwoStackStandACFM3b.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackStandACFM3b.TabIndex = 56
        Me.chbTwoStackStandACFM3b.Text = "(ACFM) 3b"
        Me.chbTwoStackStandACFM3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTemp3b
        '
        Me.chbTwoStackStandTemp3b.AutoSize = True
        Me.chbTwoStackStandTemp3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTemp3b.Location = New System.Drawing.Point(488, 40)
        Me.chbTwoStackStandTemp3b.Name = "chbTwoStackStandTemp3b"
        Me.chbTwoStackStandTemp3b.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackStandTemp3b.TabIndex = 58
        Me.chbTwoStackStandTemp3b.Text = "Gas Temp 3b"
        Me.chbTwoStackStandTemp3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandMoist3b
        '
        Me.chbTwoStackStandMoist3b.AutoSize = True
        Me.chbTwoStackStandMoist3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandMoist3b.Location = New System.Drawing.Point(490, 57)
        Me.chbTwoStackStandMoist3b.Name = "chbTwoStackStandMoist3b"
        Me.chbTwoStackStandMoist3b.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackStandMoist3b.TabIndex = 57
        Me.chbTwoStackStandMoist3b.Text = "Gas Moist 3b"
        Me.chbTwoStackStandMoist3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPoll2b
        '
        Me.chbTwoStackStandPoll2b.AutoSize = True
        Me.chbTwoStackStandPoll2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPoll2b.Location = New System.Drawing.Point(388, 108)
        Me.chbTwoStackStandPoll2b.Name = "chbTwoStackStandPoll2b"
        Me.chbTwoStackStandPoll2b.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackStandPoll2b.TabIndex = 54
        Me.chbTwoStackStandPoll2b.Text = "Poll Conc 2b"
        Me.chbTwoStackStandPoll2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmiss2b
        '
        Me.chbTwoStackStandEmiss2b.AutoSize = True
        Me.chbTwoStackStandEmiss2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmiss2b.Location = New System.Drawing.Point(380, 125)
        Me.chbTwoStackStandEmiss2b.Name = "chbTwoStackStandEmiss2b"
        Me.chbTwoStackStandEmiss2b.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackStandEmiss2b.TabIndex = 53
        Me.chbTwoStackStandEmiss2b.Text = "Emiss Rate 2b"
        Me.chbTwoStackStandEmiss2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandDSCFM2b
        '
        Me.chbTwoStackStandDSCFM2b.AutoSize = True
        Me.chbTwoStackStandDSCFM2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandDSCFM2b.Location = New System.Drawing.Point(390, 91)
        Me.chbTwoStackStandDSCFM2b.Name = "chbTwoStackStandDSCFM2b"
        Me.chbTwoStackStandDSCFM2b.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackStandDSCFM2b.TabIndex = 52
        Me.chbTwoStackStandDSCFM2b.Text = "(DSCFM) 2b"
        Me.chbTwoStackStandDSCFM2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandRun2b
        '
        Me.chbTwoStackStandRun2b.AutoSize = True
        Me.chbTwoStackStandRun2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandRun2b.Location = New System.Drawing.Point(413, 23)
        Me.chbTwoStackStandRun2b.Name = "chbTwoStackStandRun2b"
        Me.chbTwoStackStandRun2b.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackStandRun2b.TabIndex = 48
        Me.chbTwoStackStandRun2b.Text = "Run 2b"
        Me.chbTwoStackStandRun2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandACFM2b
        '
        Me.chbTwoStackStandACFM2b.AutoSize = True
        Me.chbTwoStackStandACFM2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandACFM2b.Location = New System.Drawing.Point(398, 74)
        Me.chbTwoStackStandACFM2b.Name = "chbTwoStackStandACFM2b"
        Me.chbTwoStackStandACFM2b.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackStandACFM2b.TabIndex = 49
        Me.chbTwoStackStandACFM2b.Text = "(ACFM) 2b"
        Me.chbTwoStackStandACFM2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTemp2b
        '
        Me.chbTwoStackStandTemp2b.AutoSize = True
        Me.chbTwoStackStandTemp2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTemp2b.Location = New System.Drawing.Point(384, 40)
        Me.chbTwoStackStandTemp2b.Name = "chbTwoStackStandTemp2b"
        Me.chbTwoStackStandTemp2b.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackStandTemp2b.TabIndex = 51
        Me.chbTwoStackStandTemp2b.Text = "Gas Temp 2b"
        Me.chbTwoStackStandTemp2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandMoist2b
        '
        Me.chbTwoStackStandMoist2b.AutoSize = True
        Me.chbTwoStackStandMoist2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandMoist2b.Location = New System.Drawing.Point(386, 57)
        Me.chbTwoStackStandMoist2b.Name = "chbTwoStackStandMoist2b"
        Me.chbTwoStackStandMoist2b.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackStandMoist2b.TabIndex = 50
        Me.chbTwoStackStandMoist2b.Text = "Gas Moist 2b"
        Me.chbTwoStackStandMoist2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPoll1b
        '
        Me.chbTwoStackStandPoll1b.AutoSize = True
        Me.chbTwoStackStandPoll1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPoll1b.Location = New System.Drawing.Point(295, 108)
        Me.chbTwoStackStandPoll1b.Name = "chbTwoStackStandPoll1b"
        Me.chbTwoStackStandPoll1b.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackStandPoll1b.TabIndex = 47
        Me.chbTwoStackStandPoll1b.Text = "Poll Conc 1b"
        Me.chbTwoStackStandPoll1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmiss1b
        '
        Me.chbTwoStackStandEmiss1b.AutoSize = True
        Me.chbTwoStackStandEmiss1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmiss1b.Location = New System.Drawing.Point(287, 125)
        Me.chbTwoStackStandEmiss1b.Name = "chbTwoStackStandEmiss1b"
        Me.chbTwoStackStandEmiss1b.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackStandEmiss1b.TabIndex = 46
        Me.chbTwoStackStandEmiss1b.Text = "Emiss Rate 1b"
        Me.chbTwoStackStandEmiss1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandDSCFM1b
        '
        Me.chbTwoStackStandDSCFM1b.AutoSize = True
        Me.chbTwoStackStandDSCFM1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandDSCFM1b.Location = New System.Drawing.Point(297, 91)
        Me.chbTwoStackStandDSCFM1b.Name = "chbTwoStackStandDSCFM1b"
        Me.chbTwoStackStandDSCFM1b.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackStandDSCFM1b.TabIndex = 45
        Me.chbTwoStackStandDSCFM1b.Text = "(DSCFM) 1b"
        Me.chbTwoStackStandDSCFM1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandRun1b
        '
        Me.chbTwoStackStandRun1b.AutoSize = True
        Me.chbTwoStackStandRun1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandRun1b.Location = New System.Drawing.Point(320, 23)
        Me.chbTwoStackStandRun1b.Name = "chbTwoStackStandRun1b"
        Me.chbTwoStackStandRun1b.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackStandRun1b.TabIndex = 41
        Me.chbTwoStackStandRun1b.Text = "Run 1b"
        Me.chbTwoStackStandRun1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandACFM1b
        '
        Me.chbTwoStackStandACFM1b.AutoSize = True
        Me.chbTwoStackStandACFM1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandACFM1b.Location = New System.Drawing.Point(305, 74)
        Me.chbTwoStackStandACFM1b.Name = "chbTwoStackStandACFM1b"
        Me.chbTwoStackStandACFM1b.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackStandACFM1b.TabIndex = 42
        Me.chbTwoStackStandACFM1b.Text = "(ACFM) 1b"
        Me.chbTwoStackStandACFM1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTemp1b
        '
        Me.chbTwoStackStandTemp1b.AutoSize = True
        Me.chbTwoStackStandTemp1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTemp1b.Location = New System.Drawing.Point(291, 40)
        Me.chbTwoStackStandTemp1b.Name = "chbTwoStackStandTemp1b"
        Me.chbTwoStackStandTemp1b.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackStandTemp1b.TabIndex = 44
        Me.chbTwoStackStandTemp1b.Text = "Gas Temp 1b"
        Me.chbTwoStackStandTemp1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandMoist1b
        '
        Me.chbTwoStackStandMoist1b.AutoSize = True
        Me.chbTwoStackStandMoist1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandMoist1b.Location = New System.Drawing.Point(293, 57)
        Me.chbTwoStackStandMoist1b.Name = "chbTwoStackStandMoist1b"
        Me.chbTwoStackStandMoist1b.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackStandMoist1b.TabIndex = 43
        Me.chbTwoStackStandMoist1b.Text = "Gas Moist 1b"
        Me.chbTwoStackStandMoist1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPoll3a
        '
        Me.chbTwoStackStandPoll3a.AutoSize = True
        Me.chbTwoStackStandPoll3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPoll3a.Location = New System.Drawing.Point(201, 108)
        Me.chbTwoStackStandPoll3a.Name = "chbTwoStackStandPoll3a"
        Me.chbTwoStackStandPoll3a.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackStandPoll3a.TabIndex = 40
        Me.chbTwoStackStandPoll3a.Text = "Poll Conc 3a"
        Me.chbTwoStackStandPoll3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmiss3a
        '
        Me.chbTwoStackStandEmiss3a.AutoSize = True
        Me.chbTwoStackStandEmiss3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmiss3a.Location = New System.Drawing.Point(193, 125)
        Me.chbTwoStackStandEmiss3a.Name = "chbTwoStackStandEmiss3a"
        Me.chbTwoStackStandEmiss3a.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackStandEmiss3a.TabIndex = 39
        Me.chbTwoStackStandEmiss3a.Text = "Emiss Rate 3a"
        Me.chbTwoStackStandEmiss3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandDSCFM3a
        '
        Me.chbTwoStackStandDSCFM3a.AutoSize = True
        Me.chbTwoStackStandDSCFM3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandDSCFM3a.Location = New System.Drawing.Point(203, 91)
        Me.chbTwoStackStandDSCFM3a.Name = "chbTwoStackStandDSCFM3a"
        Me.chbTwoStackStandDSCFM3a.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackStandDSCFM3a.TabIndex = 38
        Me.chbTwoStackStandDSCFM3a.Text = "(DSCFM) 3a"
        Me.chbTwoStackStandDSCFM3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandRun3a
        '
        Me.chbTwoStackStandRun3a.AutoSize = True
        Me.chbTwoStackStandRun3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandRun3a.Location = New System.Drawing.Point(226, 23)
        Me.chbTwoStackStandRun3a.Name = "chbTwoStackStandRun3a"
        Me.chbTwoStackStandRun3a.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackStandRun3a.TabIndex = 34
        Me.chbTwoStackStandRun3a.Text = "Run 3a"
        Me.chbTwoStackStandRun3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandACFM3a
        '
        Me.chbTwoStackStandACFM3a.AutoSize = True
        Me.chbTwoStackStandACFM3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandACFM3a.Location = New System.Drawing.Point(211, 74)
        Me.chbTwoStackStandACFM3a.Name = "chbTwoStackStandACFM3a"
        Me.chbTwoStackStandACFM3a.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackStandACFM3a.TabIndex = 35
        Me.chbTwoStackStandACFM3a.Text = "(ACFM) 3a"
        Me.chbTwoStackStandACFM3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTemp3a
        '
        Me.chbTwoStackStandTemp3a.AutoSize = True
        Me.chbTwoStackStandTemp3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTemp3a.Location = New System.Drawing.Point(197, 40)
        Me.chbTwoStackStandTemp3a.Name = "chbTwoStackStandTemp3a"
        Me.chbTwoStackStandTemp3a.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackStandTemp3a.TabIndex = 37
        Me.chbTwoStackStandTemp3a.Text = "Gas Temp 3a"
        Me.chbTwoStackStandTemp3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandMoist3a
        '
        Me.chbTwoStackStandMoist3a.AutoSize = True
        Me.chbTwoStackStandMoist3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandMoist3a.Location = New System.Drawing.Point(199, 57)
        Me.chbTwoStackStandMoist3a.Name = "chbTwoStackStandMoist3a"
        Me.chbTwoStackStandMoist3a.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackStandMoist3a.TabIndex = 36
        Me.chbTwoStackStandMoist3a.Text = "Gas Moist 3a"
        Me.chbTwoStackStandMoist3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPoll2a
        '
        Me.chbTwoStackStandPoll2a.AutoSize = True
        Me.chbTwoStackStandPoll2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPoll2a.Location = New System.Drawing.Point(106, 108)
        Me.chbTwoStackStandPoll2a.Name = "chbTwoStackStandPoll2a"
        Me.chbTwoStackStandPoll2a.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackStandPoll2a.TabIndex = 33
        Me.chbTwoStackStandPoll2a.Text = "Poll Conc 2a"
        Me.chbTwoStackStandPoll2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmiss2a
        '
        Me.chbTwoStackStandEmiss2a.AutoSize = True
        Me.chbTwoStackStandEmiss2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmiss2a.Location = New System.Drawing.Point(98, 125)
        Me.chbTwoStackStandEmiss2a.Name = "chbTwoStackStandEmiss2a"
        Me.chbTwoStackStandEmiss2a.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackStandEmiss2a.TabIndex = 32
        Me.chbTwoStackStandEmiss2a.Text = "Emiss Rate 2a"
        Me.chbTwoStackStandEmiss2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandDSCFM2a
        '
        Me.chbTwoStackStandDSCFM2a.AutoSize = True
        Me.chbTwoStackStandDSCFM2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandDSCFM2a.Location = New System.Drawing.Point(108, 91)
        Me.chbTwoStackStandDSCFM2a.Name = "chbTwoStackStandDSCFM2a"
        Me.chbTwoStackStandDSCFM2a.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackStandDSCFM2a.TabIndex = 31
        Me.chbTwoStackStandDSCFM2a.Text = "(DSCFM) 2a"
        Me.chbTwoStackStandDSCFM2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandRun2a
        '
        Me.chbTwoStackStandRun2a.AutoSize = True
        Me.chbTwoStackStandRun2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandRun2a.Location = New System.Drawing.Point(131, 23)
        Me.chbTwoStackStandRun2a.Name = "chbTwoStackStandRun2a"
        Me.chbTwoStackStandRun2a.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackStandRun2a.TabIndex = 27
        Me.chbTwoStackStandRun2a.Text = "Run 2a"
        Me.chbTwoStackStandRun2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandACFM2a
        '
        Me.chbTwoStackStandACFM2a.AutoSize = True
        Me.chbTwoStackStandACFM2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandACFM2a.Location = New System.Drawing.Point(116, 74)
        Me.chbTwoStackStandACFM2a.Name = "chbTwoStackStandACFM2a"
        Me.chbTwoStackStandACFM2a.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackStandACFM2a.TabIndex = 28
        Me.chbTwoStackStandACFM2a.Text = "(ACFM) 2a"
        Me.chbTwoStackStandACFM2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTemp2a
        '
        Me.chbTwoStackStandTemp2a.AutoSize = True
        Me.chbTwoStackStandTemp2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTemp2a.Location = New System.Drawing.Point(102, 40)
        Me.chbTwoStackStandTemp2a.Name = "chbTwoStackStandTemp2a"
        Me.chbTwoStackStandTemp2a.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackStandTemp2a.TabIndex = 30
        Me.chbTwoStackStandTemp2a.Text = "Gas Temp 2a"
        Me.chbTwoStackStandTemp2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandMoist2a
        '
        Me.chbTwoStackStandMoist2a.AutoSize = True
        Me.chbTwoStackStandMoist2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandMoist2a.Location = New System.Drawing.Point(104, 57)
        Me.chbTwoStackStandMoist2a.Name = "chbTwoStackStandMoist2a"
        Me.chbTwoStackStandMoist2a.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackStandMoist2a.TabIndex = 29
        Me.chbTwoStackStandMoist2a.Text = "Gas Moist 2a"
        Me.chbTwoStackStandMoist2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandName2
        '
        Me.chbTwoStackStandName2.AutoSize = True
        Me.chbTwoStackStandName2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandName2.Location = New System.Drawing.Point(348, 3)
        Me.chbTwoStackStandName2.Name = "chbTwoStackStandName2"
        Me.chbTwoStackStandName2.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackStandName2.TabIndex = 26
        Me.chbTwoStackStandName2.Text = "Stack Name 2"
        Me.chbTwoStackStandName2.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandName1
        '
        Me.chbTwoStackStandName1.AutoSize = True
        Me.chbTwoStackStandName1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandName1.Location = New System.Drawing.Point(78, 3)
        Me.chbTwoStackStandName1.Name = "chbTwoStackStandName1"
        Me.chbTwoStackStandName1.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackStandName1.TabIndex = 25
        Me.chbTwoStackStandName1.Text = "Stack Name 1"
        Me.chbTwoStackStandName1.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandPoll1a
        '
        Me.chbTwoStackStandPoll1a.AutoSize = True
        Me.chbTwoStackStandPoll1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandPoll1a.Location = New System.Drawing.Point(12, 108)
        Me.chbTwoStackStandPoll1a.Name = "chbTwoStackStandPoll1a"
        Me.chbTwoStackStandPoll1a.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackStandPoll1a.TabIndex = 24
        Me.chbTwoStackStandPoll1a.Text = "Poll Conc 1a"
        Me.chbTwoStackStandPoll1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandEmiss1a
        '
        Me.chbTwoStackStandEmiss1a.AutoSize = True
        Me.chbTwoStackStandEmiss1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandEmiss1a.Location = New System.Drawing.Point(4, 125)
        Me.chbTwoStackStandEmiss1a.Name = "chbTwoStackStandEmiss1a"
        Me.chbTwoStackStandEmiss1a.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackStandEmiss1a.TabIndex = 23
        Me.chbTwoStackStandEmiss1a.Text = "Emiss Rate 1a"
        Me.chbTwoStackStandEmiss1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandDSCFM1a
        '
        Me.chbTwoStackStandDSCFM1a.AutoSize = True
        Me.chbTwoStackStandDSCFM1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandDSCFM1a.Location = New System.Drawing.Point(14, 91)
        Me.chbTwoStackStandDSCFM1a.Name = "chbTwoStackStandDSCFM1a"
        Me.chbTwoStackStandDSCFM1a.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackStandDSCFM1a.TabIndex = 22
        Me.chbTwoStackStandDSCFM1a.Text = "(DSCFM) 1a"
        Me.chbTwoStackStandDSCFM1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandRun1a
        '
        Me.chbTwoStackStandRun1a.AutoSize = True
        Me.chbTwoStackStandRun1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandRun1a.Location = New System.Drawing.Point(37, 23)
        Me.chbTwoStackStandRun1a.Name = "chbTwoStackStandRun1a"
        Me.chbTwoStackStandRun1a.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackStandRun1a.TabIndex = 18
        Me.chbTwoStackStandRun1a.Text = "Run 1a"
        Me.chbTwoStackStandRun1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandACFM1a
        '
        Me.chbTwoStackStandACFM1a.AutoSize = True
        Me.chbTwoStackStandACFM1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandACFM1a.Location = New System.Drawing.Point(22, 74)
        Me.chbTwoStackStandACFM1a.Name = "chbTwoStackStandACFM1a"
        Me.chbTwoStackStandACFM1a.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackStandACFM1a.TabIndex = 19
        Me.chbTwoStackStandACFM1a.Text = "(ACFM) 1a"
        Me.chbTwoStackStandACFM1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandTemp1a
        '
        Me.chbTwoStackStandTemp1a.AutoSize = True
        Me.chbTwoStackStandTemp1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandTemp1a.Location = New System.Drawing.Point(8, 40)
        Me.chbTwoStackStandTemp1a.Name = "chbTwoStackStandTemp1a"
        Me.chbTwoStackStandTemp1a.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackStandTemp1a.TabIndex = 21
        Me.chbTwoStackStandTemp1a.Text = "Gas Temp 1a"
        Me.chbTwoStackStandTemp1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackStandMoist1a
        '
        Me.chbTwoStackStandMoist1a.AutoSize = True
        Me.chbTwoStackStandMoist1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackStandMoist1a.Location = New System.Drawing.Point(10, 57)
        Me.chbTwoStackStandMoist1a.Name = "chbTwoStackStandMoist1a"
        Me.chbTwoStackStandMoist1a.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackStandMoist1a.TabIndex = 20
        Me.chbTwoStackStandMoist1a.Text = "Gas Moist 1a"
        Me.chbTwoStackStandMoist1a.UseVisualStyleBackColor = True
        '
        'TPDRE
        '
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREDestructionEff)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmissUnit)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPollUnit)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmissAvg2)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmissAvg1)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPollAvg2)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPollAvg1)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPoll3b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmiss3b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREDSCFM3b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRERun3b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREACFM3b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRETemp3b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREMoist3b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPoll2b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmiss2b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREDSCFM2b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRERun2b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREACFM2b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRETemp2b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREMoist2b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPoll1b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmiss1b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREDSCFM1b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRERun1b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREACFM1b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRETemp1b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREMoist1b)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPoll3a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmiss3a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREDSCFM3a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRERun3a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREACFM3a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRETEmp3a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREMoist3a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPoll2a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmiss2a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREDSCFM2a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRERun2a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREACFM2a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRETemp2a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREMoist2a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREName2)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREName1)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREPoll1a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREEmiss1a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREDSCFM1a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRERun1a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREACFM1a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDRETemp1a)
        Me.TPDRE.Controls.Add(Me.chbTwoStackDREMoist1a)
        Me.TPDRE.Location = New System.Drawing.Point(4, 22)
        Me.TPDRE.Name = "TPDRE"
        Me.TPDRE.Padding = New System.Windows.Forms.Padding(3)
        Me.TPDRE.Size = New System.Drawing.Size(750, 176)
        Me.TPDRE.TabIndex = 1
        Me.TPDRE.Text = "DRE"
        Me.TPDRE.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREDestructionEff
        '
        Me.chbTwoStackDREDestructionEff.AutoSize = True
        Me.chbTwoStackDREDestructionEff.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREDestructionEff.Location = New System.Drawing.Point(8, 142)
        Me.chbTwoStackDREDestructionEff.Name = "chbTwoStackDREDestructionEff"
        Me.chbTwoStackDREDestructionEff.Size = New System.Drawing.Size(146, 17)
        Me.chbTwoStackDREDestructionEff.TabIndex = 127
        Me.chbTwoStackDREDestructionEff.Text = "Destruction Efficiency (%)"
        Me.chbTwoStackDREDestructionEff.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmissUnit
        '
        Me.chbTwoStackDREEmissUnit.AutoSize = True
        Me.chbTwoStackDREEmissUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmissUnit.Location = New System.Drawing.Point(578, 125)
        Me.chbTwoStackDREEmissUnit.Name = "chbTwoStackDREEmissUnit"
        Me.chbTwoStackDREEmissUnit.Size = New System.Drawing.Size(75, 17)
        Me.chbTwoStackDREEmissUnit.TabIndex = 117
        Me.chbTwoStackDREEmissUnit.Text = "Emiss Unit"
        Me.chbTwoStackDREEmissUnit.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPollUnit
        '
        Me.chbTwoStackDREPollUnit.AutoSize = True
        Me.chbTwoStackDREPollUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPollUnit.Location = New System.Drawing.Point(588, 108)
        Me.chbTwoStackDREPollUnit.Name = "chbTwoStackDREPollUnit"
        Me.chbTwoStackDREPollUnit.Size = New System.Drawing.Size(65, 17)
        Me.chbTwoStackDREPollUnit.TabIndex = 116
        Me.chbTwoStackDREPollUnit.Text = "Poll Unit"
        Me.chbTwoStackDREPollUnit.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmissAvg2
        '
        Me.chbTwoStackDREEmissAvg2.AutoSize = True
        Me.chbTwoStackDREEmissAvg2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmissAvg2.Location = New System.Drawing.Point(653, 142)
        Me.chbTwoStackDREEmissAvg2.Name = "chbTwoStackDREEmissAvg2"
        Me.chbTwoStackDREEmissAvg2.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackDREEmissAvg2.TabIndex = 115
        Me.chbTwoStackDREEmissAvg2.Text = "Emiss Avg 2"
        Me.chbTwoStackDREEmissAvg2.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmissAvg1
        '
        Me.chbTwoStackDREEmissAvg1.AutoSize = True
        Me.chbTwoStackDREEmissAvg1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmissAvg1.Location = New System.Drawing.Point(653, 125)
        Me.chbTwoStackDREEmissAvg1.Name = "chbTwoStackDREEmissAvg1"
        Me.chbTwoStackDREEmissAvg1.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackDREEmissAvg1.TabIndex = 114
        Me.chbTwoStackDREEmissAvg1.Text = "Emiss Avg 1"
        Me.chbTwoStackDREEmissAvg1.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPollAvg2
        '
        Me.chbTwoStackDREPollAvg2.AutoSize = True
        Me.chbTwoStackDREPollAvg2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPollAvg2.Location = New System.Drawing.Point(663, 108)
        Me.chbTwoStackDREPollAvg2.Name = "chbTwoStackDREPollAvg2"
        Me.chbTwoStackDREPollAvg2.Size = New System.Drawing.Size(74, 17)
        Me.chbTwoStackDREPollAvg2.TabIndex = 113
        Me.chbTwoStackDREPollAvg2.Text = "Poll Avg 2"
        Me.chbTwoStackDREPollAvg2.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPollAvg1
        '
        Me.chbTwoStackDREPollAvg1.AutoSize = True
        Me.chbTwoStackDREPollAvg1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPollAvg1.Location = New System.Drawing.Point(663, 91)
        Me.chbTwoStackDREPollAvg1.Name = "chbTwoStackDREPollAvg1"
        Me.chbTwoStackDREPollAvg1.Size = New System.Drawing.Size(74, 17)
        Me.chbTwoStackDREPollAvg1.TabIndex = 112
        Me.chbTwoStackDREPollAvg1.Text = "Poll Avg 1"
        Me.chbTwoStackDREPollAvg1.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPoll3b
        '
        Me.chbTwoStackDREPoll3b.AutoSize = True
        Me.chbTwoStackDREPoll3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPoll3b.Location = New System.Drawing.Point(492, 108)
        Me.chbTwoStackDREPoll3b.Name = "chbTwoStackDREPoll3b"
        Me.chbTwoStackDREPoll3b.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackDREPoll3b.TabIndex = 111
        Me.chbTwoStackDREPoll3b.Text = "Poll Conc 3b"
        Me.chbTwoStackDREPoll3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmiss3b
        '
        Me.chbTwoStackDREEmiss3b.AutoSize = True
        Me.chbTwoStackDREEmiss3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmiss3b.Location = New System.Drawing.Point(484, 125)
        Me.chbTwoStackDREEmiss3b.Name = "chbTwoStackDREEmiss3b"
        Me.chbTwoStackDREEmiss3b.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackDREEmiss3b.TabIndex = 110
        Me.chbTwoStackDREEmiss3b.Text = "Emiss Rate 3b"
        Me.chbTwoStackDREEmiss3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREDSCFM3b
        '
        Me.chbTwoStackDREDSCFM3b.AutoSize = True
        Me.chbTwoStackDREDSCFM3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREDSCFM3b.Location = New System.Drawing.Point(494, 91)
        Me.chbTwoStackDREDSCFM3b.Name = "chbTwoStackDREDSCFM3b"
        Me.chbTwoStackDREDSCFM3b.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackDREDSCFM3b.TabIndex = 109
        Me.chbTwoStackDREDSCFM3b.Text = "(DSCFM) 3b"
        Me.chbTwoStackDREDSCFM3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRERun3b
        '
        Me.chbTwoStackDRERun3b.AutoSize = True
        Me.chbTwoStackDRERun3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRERun3b.Location = New System.Drawing.Point(517, 23)
        Me.chbTwoStackDRERun3b.Name = "chbTwoStackDRERun3b"
        Me.chbTwoStackDRERun3b.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackDRERun3b.TabIndex = 105
        Me.chbTwoStackDRERun3b.Text = "Run 3b"
        Me.chbTwoStackDRERun3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREACFM3b
        '
        Me.chbTwoStackDREACFM3b.AutoSize = True
        Me.chbTwoStackDREACFM3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREACFM3b.Location = New System.Drawing.Point(502, 74)
        Me.chbTwoStackDREACFM3b.Name = "chbTwoStackDREACFM3b"
        Me.chbTwoStackDREACFM3b.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackDREACFM3b.TabIndex = 106
        Me.chbTwoStackDREACFM3b.Text = "(ACFM) 3b"
        Me.chbTwoStackDREACFM3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRETemp3b
        '
        Me.chbTwoStackDRETemp3b.AutoSize = True
        Me.chbTwoStackDRETemp3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRETemp3b.Location = New System.Drawing.Point(488, 40)
        Me.chbTwoStackDRETemp3b.Name = "chbTwoStackDRETemp3b"
        Me.chbTwoStackDRETemp3b.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackDRETemp3b.TabIndex = 108
        Me.chbTwoStackDRETemp3b.Text = "Gas Temp 3b"
        Me.chbTwoStackDRETemp3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREMoist3b
        '
        Me.chbTwoStackDREMoist3b.AutoSize = True
        Me.chbTwoStackDREMoist3b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREMoist3b.Location = New System.Drawing.Point(490, 57)
        Me.chbTwoStackDREMoist3b.Name = "chbTwoStackDREMoist3b"
        Me.chbTwoStackDREMoist3b.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackDREMoist3b.TabIndex = 107
        Me.chbTwoStackDREMoist3b.Text = "Gas Moist 3b"
        Me.chbTwoStackDREMoist3b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPoll2b
        '
        Me.chbTwoStackDREPoll2b.AutoSize = True
        Me.chbTwoStackDREPoll2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPoll2b.Location = New System.Drawing.Point(388, 108)
        Me.chbTwoStackDREPoll2b.Name = "chbTwoStackDREPoll2b"
        Me.chbTwoStackDREPoll2b.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackDREPoll2b.TabIndex = 104
        Me.chbTwoStackDREPoll2b.Text = "Poll Conc 2b"
        Me.chbTwoStackDREPoll2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmiss2b
        '
        Me.chbTwoStackDREEmiss2b.AutoSize = True
        Me.chbTwoStackDREEmiss2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmiss2b.Location = New System.Drawing.Point(380, 125)
        Me.chbTwoStackDREEmiss2b.Name = "chbTwoStackDREEmiss2b"
        Me.chbTwoStackDREEmiss2b.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackDREEmiss2b.TabIndex = 103
        Me.chbTwoStackDREEmiss2b.Text = "Emiss Rate 2b"
        Me.chbTwoStackDREEmiss2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREDSCFM2b
        '
        Me.chbTwoStackDREDSCFM2b.AutoSize = True
        Me.chbTwoStackDREDSCFM2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREDSCFM2b.Location = New System.Drawing.Point(390, 91)
        Me.chbTwoStackDREDSCFM2b.Name = "chbTwoStackDREDSCFM2b"
        Me.chbTwoStackDREDSCFM2b.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackDREDSCFM2b.TabIndex = 102
        Me.chbTwoStackDREDSCFM2b.Text = "(DSCFM) 2b"
        Me.chbTwoStackDREDSCFM2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRERun2b
        '
        Me.chbTwoStackDRERun2b.AutoSize = True
        Me.chbTwoStackDRERun2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRERun2b.Location = New System.Drawing.Point(413, 23)
        Me.chbTwoStackDRERun2b.Name = "chbTwoStackDRERun2b"
        Me.chbTwoStackDRERun2b.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackDRERun2b.TabIndex = 98
        Me.chbTwoStackDRERun2b.Text = "Run 2b"
        Me.chbTwoStackDRERun2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREACFM2b
        '
        Me.chbTwoStackDREACFM2b.AutoSize = True
        Me.chbTwoStackDREACFM2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREACFM2b.Location = New System.Drawing.Point(398, 74)
        Me.chbTwoStackDREACFM2b.Name = "chbTwoStackDREACFM2b"
        Me.chbTwoStackDREACFM2b.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackDREACFM2b.TabIndex = 99
        Me.chbTwoStackDREACFM2b.Text = "(ACFM) 2b"
        Me.chbTwoStackDREACFM2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRETemp2b
        '
        Me.chbTwoStackDRETemp2b.AutoSize = True
        Me.chbTwoStackDRETemp2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRETemp2b.Location = New System.Drawing.Point(384, 40)
        Me.chbTwoStackDRETemp2b.Name = "chbTwoStackDRETemp2b"
        Me.chbTwoStackDRETemp2b.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackDRETemp2b.TabIndex = 101
        Me.chbTwoStackDRETemp2b.Text = "Gas Temp 2b"
        Me.chbTwoStackDRETemp2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREMoist2b
        '
        Me.chbTwoStackDREMoist2b.AutoSize = True
        Me.chbTwoStackDREMoist2b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREMoist2b.Location = New System.Drawing.Point(386, 57)
        Me.chbTwoStackDREMoist2b.Name = "chbTwoStackDREMoist2b"
        Me.chbTwoStackDREMoist2b.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackDREMoist2b.TabIndex = 100
        Me.chbTwoStackDREMoist2b.Text = "Gas Moist 2b"
        Me.chbTwoStackDREMoist2b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPoll1b
        '
        Me.chbTwoStackDREPoll1b.AutoSize = True
        Me.chbTwoStackDREPoll1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPoll1b.Location = New System.Drawing.Point(295, 108)
        Me.chbTwoStackDREPoll1b.Name = "chbTwoStackDREPoll1b"
        Me.chbTwoStackDREPoll1b.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackDREPoll1b.TabIndex = 97
        Me.chbTwoStackDREPoll1b.Text = "Poll Conc 1b"
        Me.chbTwoStackDREPoll1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmiss1b
        '
        Me.chbTwoStackDREEmiss1b.AutoSize = True
        Me.chbTwoStackDREEmiss1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmiss1b.Location = New System.Drawing.Point(287, 125)
        Me.chbTwoStackDREEmiss1b.Name = "chbTwoStackDREEmiss1b"
        Me.chbTwoStackDREEmiss1b.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackDREEmiss1b.TabIndex = 96
        Me.chbTwoStackDREEmiss1b.Text = "Emiss Rate 1b"
        Me.chbTwoStackDREEmiss1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREDSCFM1b
        '
        Me.chbTwoStackDREDSCFM1b.AutoSize = True
        Me.chbTwoStackDREDSCFM1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREDSCFM1b.Location = New System.Drawing.Point(297, 91)
        Me.chbTwoStackDREDSCFM1b.Name = "chbTwoStackDREDSCFM1b"
        Me.chbTwoStackDREDSCFM1b.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackDREDSCFM1b.TabIndex = 95
        Me.chbTwoStackDREDSCFM1b.Text = "(DSCFM) 1b"
        Me.chbTwoStackDREDSCFM1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRERun1b
        '
        Me.chbTwoStackDRERun1b.AutoSize = True
        Me.chbTwoStackDRERun1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRERun1b.Location = New System.Drawing.Point(320, 23)
        Me.chbTwoStackDRERun1b.Name = "chbTwoStackDRERun1b"
        Me.chbTwoStackDRERun1b.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackDRERun1b.TabIndex = 91
        Me.chbTwoStackDRERun1b.Text = "Run 1b"
        Me.chbTwoStackDRERun1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREACFM1b
        '
        Me.chbTwoStackDREACFM1b.AutoSize = True
        Me.chbTwoStackDREACFM1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREACFM1b.Location = New System.Drawing.Point(305, 74)
        Me.chbTwoStackDREACFM1b.Name = "chbTwoStackDREACFM1b"
        Me.chbTwoStackDREACFM1b.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackDREACFM1b.TabIndex = 92
        Me.chbTwoStackDREACFM1b.Text = "(ACFM) 1b"
        Me.chbTwoStackDREACFM1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRETemp1b
        '
        Me.chbTwoStackDRETemp1b.AutoSize = True
        Me.chbTwoStackDRETemp1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRETemp1b.Location = New System.Drawing.Point(291, 40)
        Me.chbTwoStackDRETemp1b.Name = "chbTwoStackDRETemp1b"
        Me.chbTwoStackDRETemp1b.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackDRETemp1b.TabIndex = 94
        Me.chbTwoStackDRETemp1b.Text = "Gas Temp 1b"
        Me.chbTwoStackDRETemp1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREMoist1b
        '
        Me.chbTwoStackDREMoist1b.AutoSize = True
        Me.chbTwoStackDREMoist1b.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREMoist1b.Location = New System.Drawing.Point(293, 57)
        Me.chbTwoStackDREMoist1b.Name = "chbTwoStackDREMoist1b"
        Me.chbTwoStackDREMoist1b.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackDREMoist1b.TabIndex = 93
        Me.chbTwoStackDREMoist1b.Text = "Gas Moist 1b"
        Me.chbTwoStackDREMoist1b.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPoll3a
        '
        Me.chbTwoStackDREPoll3a.AutoSize = True
        Me.chbTwoStackDREPoll3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPoll3a.Location = New System.Drawing.Point(201, 108)
        Me.chbTwoStackDREPoll3a.Name = "chbTwoStackDREPoll3a"
        Me.chbTwoStackDREPoll3a.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackDREPoll3a.TabIndex = 90
        Me.chbTwoStackDREPoll3a.Text = "Poll Conc 3a"
        Me.chbTwoStackDREPoll3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmiss3a
        '
        Me.chbTwoStackDREEmiss3a.AutoSize = True
        Me.chbTwoStackDREEmiss3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmiss3a.Location = New System.Drawing.Point(193, 125)
        Me.chbTwoStackDREEmiss3a.Name = "chbTwoStackDREEmiss3a"
        Me.chbTwoStackDREEmiss3a.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackDREEmiss3a.TabIndex = 89
        Me.chbTwoStackDREEmiss3a.Text = "Emiss Rate 3a"
        Me.chbTwoStackDREEmiss3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREDSCFM3a
        '
        Me.chbTwoStackDREDSCFM3a.AutoSize = True
        Me.chbTwoStackDREDSCFM3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREDSCFM3a.Location = New System.Drawing.Point(203, 91)
        Me.chbTwoStackDREDSCFM3a.Name = "chbTwoStackDREDSCFM3a"
        Me.chbTwoStackDREDSCFM3a.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackDREDSCFM3a.TabIndex = 88
        Me.chbTwoStackDREDSCFM3a.Text = "(DSCFM) 3a"
        Me.chbTwoStackDREDSCFM3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRERun3a
        '
        Me.chbTwoStackDRERun3a.AutoSize = True
        Me.chbTwoStackDRERun3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRERun3a.Location = New System.Drawing.Point(226, 23)
        Me.chbTwoStackDRERun3a.Name = "chbTwoStackDRERun3a"
        Me.chbTwoStackDRERun3a.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackDRERun3a.TabIndex = 84
        Me.chbTwoStackDRERun3a.Text = "Run 3a"
        Me.chbTwoStackDRERun3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREACFM3a
        '
        Me.chbTwoStackDREACFM3a.AutoSize = True
        Me.chbTwoStackDREACFM3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREACFM3a.Location = New System.Drawing.Point(211, 74)
        Me.chbTwoStackDREACFM3a.Name = "chbTwoStackDREACFM3a"
        Me.chbTwoStackDREACFM3a.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackDREACFM3a.TabIndex = 85
        Me.chbTwoStackDREACFM3a.Text = "(ACFM) 3a"
        Me.chbTwoStackDREACFM3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRETEmp3a
        '
        Me.chbTwoStackDRETEmp3a.AutoSize = True
        Me.chbTwoStackDRETEmp3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRETEmp3a.Location = New System.Drawing.Point(197, 40)
        Me.chbTwoStackDRETEmp3a.Name = "chbTwoStackDRETEmp3a"
        Me.chbTwoStackDRETEmp3a.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackDRETEmp3a.TabIndex = 87
        Me.chbTwoStackDRETEmp3a.Text = "Gas Temp 3a"
        Me.chbTwoStackDRETEmp3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREMoist3a
        '
        Me.chbTwoStackDREMoist3a.AutoSize = True
        Me.chbTwoStackDREMoist3a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREMoist3a.Location = New System.Drawing.Point(199, 57)
        Me.chbTwoStackDREMoist3a.Name = "chbTwoStackDREMoist3a"
        Me.chbTwoStackDREMoist3a.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackDREMoist3a.TabIndex = 86
        Me.chbTwoStackDREMoist3a.Text = "Gas Moist 3a"
        Me.chbTwoStackDREMoist3a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPoll2a
        '
        Me.chbTwoStackDREPoll2a.AutoSize = True
        Me.chbTwoStackDREPoll2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPoll2a.Location = New System.Drawing.Point(106, 108)
        Me.chbTwoStackDREPoll2a.Name = "chbTwoStackDREPoll2a"
        Me.chbTwoStackDREPoll2a.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackDREPoll2a.TabIndex = 83
        Me.chbTwoStackDREPoll2a.Text = "Poll Conc 2a"
        Me.chbTwoStackDREPoll2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmiss2a
        '
        Me.chbTwoStackDREEmiss2a.AutoSize = True
        Me.chbTwoStackDREEmiss2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmiss2a.Location = New System.Drawing.Point(98, 125)
        Me.chbTwoStackDREEmiss2a.Name = "chbTwoStackDREEmiss2a"
        Me.chbTwoStackDREEmiss2a.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackDREEmiss2a.TabIndex = 82
        Me.chbTwoStackDREEmiss2a.Text = "Emiss Rate 2a"
        Me.chbTwoStackDREEmiss2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREDSCFM2a
        '
        Me.chbTwoStackDREDSCFM2a.AutoSize = True
        Me.chbTwoStackDREDSCFM2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREDSCFM2a.Location = New System.Drawing.Point(108, 91)
        Me.chbTwoStackDREDSCFM2a.Name = "chbTwoStackDREDSCFM2a"
        Me.chbTwoStackDREDSCFM2a.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackDREDSCFM2a.TabIndex = 81
        Me.chbTwoStackDREDSCFM2a.Text = "(DSCFM) 2a"
        Me.chbTwoStackDREDSCFM2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRERun2a
        '
        Me.chbTwoStackDRERun2a.AutoSize = True
        Me.chbTwoStackDRERun2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRERun2a.Location = New System.Drawing.Point(131, 23)
        Me.chbTwoStackDRERun2a.Name = "chbTwoStackDRERun2a"
        Me.chbTwoStackDRERun2a.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackDRERun2a.TabIndex = 77
        Me.chbTwoStackDRERun2a.Text = "Run 2a"
        Me.chbTwoStackDRERun2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREACFM2a
        '
        Me.chbTwoStackDREACFM2a.AutoSize = True
        Me.chbTwoStackDREACFM2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREACFM2a.Location = New System.Drawing.Point(116, 74)
        Me.chbTwoStackDREACFM2a.Name = "chbTwoStackDREACFM2a"
        Me.chbTwoStackDREACFM2a.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackDREACFM2a.TabIndex = 78
        Me.chbTwoStackDREACFM2a.Text = "(ACFM) 2a"
        Me.chbTwoStackDREACFM2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRETemp2a
        '
        Me.chbTwoStackDRETemp2a.AutoSize = True
        Me.chbTwoStackDRETemp2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRETemp2a.Location = New System.Drawing.Point(102, 40)
        Me.chbTwoStackDRETemp2a.Name = "chbTwoStackDRETemp2a"
        Me.chbTwoStackDRETemp2a.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackDRETemp2a.TabIndex = 80
        Me.chbTwoStackDRETemp2a.Text = "Gas Temp 2a"
        Me.chbTwoStackDRETemp2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREMoist2a
        '
        Me.chbTwoStackDREMoist2a.AutoSize = True
        Me.chbTwoStackDREMoist2a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREMoist2a.Location = New System.Drawing.Point(104, 57)
        Me.chbTwoStackDREMoist2a.Name = "chbTwoStackDREMoist2a"
        Me.chbTwoStackDREMoist2a.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackDREMoist2a.TabIndex = 79
        Me.chbTwoStackDREMoist2a.Text = "Gas Moist 2a"
        Me.chbTwoStackDREMoist2a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREName2
        '
        Me.chbTwoStackDREName2.AutoSize = True
        Me.chbTwoStackDREName2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREName2.Location = New System.Drawing.Point(348, 3)
        Me.chbTwoStackDREName2.Name = "chbTwoStackDREName2"
        Me.chbTwoStackDREName2.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackDREName2.TabIndex = 76
        Me.chbTwoStackDREName2.Text = "Stack Name 2"
        Me.chbTwoStackDREName2.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREName1
        '
        Me.chbTwoStackDREName1.AutoSize = True
        Me.chbTwoStackDREName1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREName1.Location = New System.Drawing.Point(78, 3)
        Me.chbTwoStackDREName1.Name = "chbTwoStackDREName1"
        Me.chbTwoStackDREName1.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackDREName1.TabIndex = 75
        Me.chbTwoStackDREName1.Text = "Stack Name 1"
        Me.chbTwoStackDREName1.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREPoll1a
        '
        Me.chbTwoStackDREPoll1a.AutoSize = True
        Me.chbTwoStackDREPoll1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREPoll1a.Location = New System.Drawing.Point(12, 108)
        Me.chbTwoStackDREPoll1a.Name = "chbTwoStackDREPoll1a"
        Me.chbTwoStackDREPoll1a.Size = New System.Drawing.Size(86, 17)
        Me.chbTwoStackDREPoll1a.TabIndex = 74
        Me.chbTwoStackDREPoll1a.Text = "Poll Conc 1a"
        Me.chbTwoStackDREPoll1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREEmiss1a
        '
        Me.chbTwoStackDREEmiss1a.AutoSize = True
        Me.chbTwoStackDREEmiss1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREEmiss1a.Location = New System.Drawing.Point(4, 125)
        Me.chbTwoStackDREEmiss1a.Name = "chbTwoStackDREEmiss1a"
        Me.chbTwoStackDREEmiss1a.Size = New System.Drawing.Size(94, 17)
        Me.chbTwoStackDREEmiss1a.TabIndex = 73
        Me.chbTwoStackDREEmiss1a.Text = "Emiss Rate 1a"
        Me.chbTwoStackDREEmiss1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREDSCFM1a
        '
        Me.chbTwoStackDREDSCFM1a.AutoSize = True
        Me.chbTwoStackDREDSCFM1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREDSCFM1a.Location = New System.Drawing.Point(14, 91)
        Me.chbTwoStackDREDSCFM1a.Name = "chbTwoStackDREDSCFM1a"
        Me.chbTwoStackDREDSCFM1a.Size = New System.Drawing.Size(84, 17)
        Me.chbTwoStackDREDSCFM1a.TabIndex = 72
        Me.chbTwoStackDREDSCFM1a.Text = "(DSCFM) 1a"
        Me.chbTwoStackDREDSCFM1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRERun1a
        '
        Me.chbTwoStackDRERun1a.AutoSize = True
        Me.chbTwoStackDRERun1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRERun1a.Location = New System.Drawing.Point(37, 23)
        Me.chbTwoStackDRERun1a.Name = "chbTwoStackDRERun1a"
        Me.chbTwoStackDRERun1a.Size = New System.Drawing.Size(61, 17)
        Me.chbTwoStackDRERun1a.TabIndex = 68
        Me.chbTwoStackDRERun1a.Text = "Run 1a"
        Me.chbTwoStackDRERun1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREACFM1a
        '
        Me.chbTwoStackDREACFM1a.AutoSize = True
        Me.chbTwoStackDREACFM1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREACFM1a.Location = New System.Drawing.Point(22, 74)
        Me.chbTwoStackDREACFM1a.Name = "chbTwoStackDREACFM1a"
        Me.chbTwoStackDREACFM1a.Size = New System.Drawing.Size(76, 17)
        Me.chbTwoStackDREACFM1a.TabIndex = 69
        Me.chbTwoStackDREACFM1a.Text = "(ACFM) 1a"
        Me.chbTwoStackDREACFM1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDRETemp1a
        '
        Me.chbTwoStackDRETemp1a.AutoSize = True
        Me.chbTwoStackDRETemp1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDRETemp1a.Location = New System.Drawing.Point(8, 40)
        Me.chbTwoStackDRETemp1a.Name = "chbTwoStackDRETemp1a"
        Me.chbTwoStackDRETemp1a.Size = New System.Drawing.Size(90, 17)
        Me.chbTwoStackDRETemp1a.TabIndex = 71
        Me.chbTwoStackDRETemp1a.Text = "Gas Temp 1a"
        Me.chbTwoStackDRETemp1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackDREMoist1a
        '
        Me.chbTwoStackDREMoist1a.AutoSize = True
        Me.chbTwoStackDREMoist1a.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackDREMoist1a.Location = New System.Drawing.Point(10, 57)
        Me.chbTwoStackDREMoist1a.Name = "chbTwoStackDREMoist1a"
        Me.chbTwoStackDREMoist1a.Size = New System.Drawing.Size(88, 17)
        Me.chbTwoStackDREMoist1a.TabIndex = 70
        Me.chbTwoStackDREMoist1a.Text = "Gas Moist 1a"
        Me.chbTwoStackDREMoist1a.UseVisualStyleBackColor = True
        '
        'chbTwoStackAppRequire
        '
        Me.chbTwoStackAppRequire.AutoSize = True
        Me.chbTwoStackAppRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackAppRequire.Location = New System.Drawing.Point(30, 39)
        Me.chbTwoStackAppRequire.Name = "chbTwoStackAppRequire"
        Me.chbTwoStackAppRequire.Size = New System.Drawing.Size(138, 17)
        Me.chbTwoStackAppRequire.TabIndex = 15
        Me.chbTwoStackAppRequire.Text = "Applicable Requirement"
        Me.chbTwoStackAppRequire.UseVisualStyleBackColor = True
        '
        'chbTwoStackAllowEmiss2
        '
        Me.chbTwoStackAllowEmiss2.AutoSize = True
        Me.chbTwoStackAllowEmiss2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackAllowEmiss2.Location = New System.Drawing.Point(175, 22)
        Me.chbTwoStackAllowEmiss2.Name = "chbTwoStackAllowEmiss2"
        Me.chbTwoStackAllowEmiss2.Size = New System.Drawing.Size(150, 17)
        Me.chbTwoStackAllowEmiss2.TabIndex = 12
        Me.chbTwoStackAllowEmiss2.Text = "Allowable Emission Rate 2"
        Me.chbTwoStackAllowEmiss2.UseVisualStyleBackColor = True
        '
        'chbTwoStackAllowEmiss3
        '
        Me.chbTwoStackAllowEmiss3.AutoSize = True
        Me.chbTwoStackAllowEmiss3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackAllowEmiss3.Location = New System.Drawing.Point(331, 22)
        Me.chbTwoStackAllowEmiss3.Name = "chbTwoStackAllowEmiss3"
        Me.chbTwoStackAllowEmiss3.Size = New System.Drawing.Size(150, 17)
        Me.chbTwoStackAllowEmiss3.TabIndex = 11
        Me.chbTwoStackAllowEmiss3.Text = "Allowable Emission Rate 3"
        Me.chbTwoStackAllowEmiss3.UseVisualStyleBackColor = True
        '
        'chbTwoStackOpCapacity
        '
        Me.chbTwoStackOpCapacity.AutoSize = True
        Me.chbTwoStackOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackOpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbTwoStackOpCapacity.Name = "chbTwoStackOpCapacity"
        Me.chbTwoStackOpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbTwoStackOpCapacity.TabIndex = 10
        Me.chbTwoStackOpCapacity.Text = "Operating Capacity"
        Me.chbTwoStackOpCapacity.UseVisualStyleBackColor = True
        '
        'chbTwoStackMaxOpCapacity
        '
        Me.chbTwoStackMaxOpCapacity.AutoSize = True
        Me.chbTwoStackMaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackMaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbTwoStackMaxOpCapacity.Name = "chbTwoStackMaxOpCapacity"
        Me.chbTwoStackMaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbTwoStackMaxOpCapacity.TabIndex = 9
        Me.chbTwoStackMaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbTwoStackMaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbTwoStackAllowEmiss1
        '
        Me.chbTwoStackAllowEmiss1.AutoSize = True
        Me.chbTwoStackAllowEmiss1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTwoStackAllowEmiss1.Location = New System.Drawing.Point(15, 22)
        Me.chbTwoStackAllowEmiss1.Name = "chbTwoStackAllowEmiss1"
        Me.chbTwoStackAllowEmiss1.Size = New System.Drawing.Size(153, 17)
        Me.chbTwoStackAllowEmiss1.TabIndex = 13
        Me.chbTwoStackAllowEmiss1.Text = "Allowable Emission Rate 1 "
        Me.chbTwoStackAllowEmiss1.UseVisualStyleBackColor = True
        '
        'TPMethod22
        '
        Me.TPMethod22.Controls.Add(Me.chbMethod22Emission)
        Me.TPMethod22.Controls.Add(Me.chbMethod22OtherInfo)
        Me.TPMethod22.Controls.Add(Me.chbMethod22AppReg)
        Me.TPMethod22.Controls.Add(Me.chbMethod22TestDuration)
        Me.TPMethod22.Controls.Add(Me.chbMethod22OpCapacity)
        Me.TPMethod22.Controls.Add(Me.chbMethod22MaxOpCapacity)
        Me.TPMethod22.Controls.Add(Me.chbMethod22AllowEmiss)
        Me.TPMethod22.Location = New System.Drawing.Point(4, 22)
        Me.TPMethod22.Name = "TPMethod22"
        Me.TPMethod22.Size = New System.Drawing.Size(786, 315)
        Me.TPMethod22.TabIndex = 10
        Me.TPMethod22.Text = "Method 22"
        Me.TPMethod22.UseVisualStyleBackColor = True
        '
        'chbMethod22Emission
        '
        Me.chbMethod22Emission.AutoSize = True
        Me.chbMethod22Emission.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod22Emission.Location = New System.Drawing.Point(36, 90)
        Me.chbMethod22Emission.Name = "chbMethod22Emission"
        Me.chbMethod22Emission.Size = New System.Drawing.Size(132, 17)
        Me.chbMethod22Emission.TabIndex = 24
        Me.chbMethod22Emission.Text = "Accumulated Emission"
        Me.chbMethod22Emission.UseVisualStyleBackColor = True
        '
        'chbMethod22OtherInfo
        '
        Me.chbMethod22OtherInfo.AutoSize = True
        Me.chbMethod22OtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod22OtherInfo.Location = New System.Drawing.Point(61, 107)
        Me.chbMethod22OtherInfo.Name = "chbMethod22OtherInfo"
        Me.chbMethod22OtherInfo.Size = New System.Drawing.Size(107, 17)
        Me.chbMethod22OtherInfo.TabIndex = 23
        Me.chbMethod22OtherInfo.Text = "Other Information"
        Me.chbMethod22OtherInfo.UseVisualStyleBackColor = True
        '
        'chbMethod22AppReg
        '
        Me.chbMethod22AppReg.AutoSize = True
        Me.chbMethod22AppReg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod22AppReg.Location = New System.Drawing.Point(30, 39)
        Me.chbMethod22AppReg.Name = "chbMethod22AppReg"
        Me.chbMethod22AppReg.Size = New System.Drawing.Size(138, 17)
        Me.chbMethod22AppReg.TabIndex = 22
        Me.chbMethod22AppReg.Text = "Applicable Requirement"
        Me.chbMethod22AppReg.UseVisualStyleBackColor = True
        '
        'chbMethod22TestDuration
        '
        Me.chbMethod22TestDuration.AutoSize = True
        Me.chbMethod22TestDuration.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod22TestDuration.Location = New System.Drawing.Point(78, 73)
        Me.chbMethod22TestDuration.Name = "chbMethod22TestDuration"
        Me.chbMethod22TestDuration.Size = New System.Drawing.Size(90, 17)
        Me.chbMethod22TestDuration.TabIndex = 21
        Me.chbMethod22TestDuration.Text = "Test Duration"
        Me.chbMethod22TestDuration.UseVisualStyleBackColor = True
        '
        'chbMethod22OpCapacity
        '
        Me.chbMethod22OpCapacity.AutoSize = True
        Me.chbMethod22OpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod22OpCapacity.Location = New System.Drawing.Point(209, 5)
        Me.chbMethod22OpCapacity.Name = "chbMethod22OpCapacity"
        Me.chbMethod22OpCapacity.Size = New System.Drawing.Size(116, 17)
        Me.chbMethod22OpCapacity.TabIndex = 17
        Me.chbMethod22OpCapacity.Text = "Operating Capacity"
        Me.chbMethod22OpCapacity.UseVisualStyleBackColor = True
        '
        'chbMethod22MaxOpCapacity
        '
        Me.chbMethod22MaxOpCapacity.AutoSize = True
        Me.chbMethod22MaxOpCapacity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod22MaxOpCapacity.Location = New System.Drawing.Point(5, 5)
        Me.chbMethod22MaxOpCapacity.Name = "chbMethod22MaxOpCapacity"
        Me.chbMethod22MaxOpCapacity.Size = New System.Drawing.Size(163, 17)
        Me.chbMethod22MaxOpCapacity.TabIndex = 16
        Me.chbMethod22MaxOpCapacity.Text = "Maximum Operating Capacity"
        Me.chbMethod22MaxOpCapacity.UseVisualStyleBackColor = True
        '
        'chbMethod22AllowEmiss
        '
        Me.chbMethod22AllowEmiss.AutoSize = True
        Me.chbMethod22AllowEmiss.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethod22AllowEmiss.Location = New System.Drawing.Point(27, 22)
        Me.chbMethod22AllowEmiss.Name = "chbMethod22AllowEmiss"
        Me.chbMethod22AllowEmiss.Size = New System.Drawing.Size(141, 17)
        Me.chbMethod22AllowEmiss.TabIndex = 20
        Me.chbMethod22AllowEmiss.Text = "Allowable Emission Rate"
        Me.chbMethod22AllowEmiss.UseVisualStyleBackColor = True
        '
        'TPSSCPWork
        '
        Me.TPSSCPWork.Location = New System.Drawing.Point(4, 22)
        Me.TPSSCPWork.Name = "TPSSCPWork"
        Me.TPSSCPWork.Size = New System.Drawing.Size(786, 315)
        Me.TPSSCPWork.TabIndex = 11
        Me.TPSSCPWork.Text = "SSCP Work"
        Me.TPSSCPWork.UseVisualStyleBackColor = True
        '
        'chbAIRSNumber
        '
        Me.chbAIRSNumber.AutoSize = True
        Me.chbAIRSNumber.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbAIRSNumber.Location = New System.Drawing.Point(72, 69)
        Me.chbAIRSNumber.Name = "chbAIRSNumber"
        Me.chbAIRSNumber.Size = New System.Drawing.Size(91, 17)
        Me.chbAIRSNumber.TabIndex = 2
        Me.chbAIRSNumber.Text = "AIRS Number"
        Me.chbAIRSNumber.UseVisualStyleBackColor = True
        '
        'chbFacilityName
        '
        Me.chbFacilityName.AutoSize = True
        Me.chbFacilityName.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbFacilityName.Location = New System.Drawing.Point(74, 86)
        Me.chbFacilityName.Name = "chbFacilityName"
        Me.chbFacilityName.Size = New System.Drawing.Size(89, 17)
        Me.chbFacilityName.TabIndex = 3
        Me.chbFacilityName.Text = "Facility Name"
        Me.chbFacilityName.UseVisualStyleBackColor = True
        '
        'chbLocation
        '
        Me.chbLocation.AutoSize = True
        Me.chbLocation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbLocation.Location = New System.Drawing.Point(96, 103)
        Me.chbLocation.Name = "chbLocation"
        Me.chbLocation.Size = New System.Drawing.Size(67, 17)
        Me.chbLocation.TabIndex = 4
        Me.chbLocation.Text = "Location"
        Me.chbLocation.UseVisualStyleBackColor = True
        '
        'chbReportType
        '
        Me.chbReportType.AutoSize = True
        Me.chbReportType.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbReportType.Location = New System.Drawing.Point(78, 120)
        Me.chbReportType.Name = "chbReportType"
        Me.chbReportType.Size = New System.Drawing.Size(85, 17)
        Me.chbReportType.TabIndex = 5
        Me.chbReportType.Text = "Report Type"
        Me.chbReportType.UseVisualStyleBackColor = True
        '
        'chbISMPReviewer
        '
        Me.chbISMPReviewer.AutoSize = True
        Me.chbISMPReviewer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbISMPReviewer.Location = New System.Drawing.Point(63, 137)
        Me.chbISMPReviewer.Name = "chbISMPReviewer"
        Me.chbISMPReviewer.Size = New System.Drawing.Size(100, 17)
        Me.chbISMPReviewer.TabIndex = 6
        Me.chbISMPReviewer.Text = "ISMP Reviewer"
        Me.chbISMPReviewer.UseVisualStyleBackColor = True
        '
        'chbISMPUnit
        '
        Me.chbISMPUnit.AutoSize = True
        Me.chbISMPUnit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbISMPUnit.Location = New System.Drawing.Point(89, 154)
        Me.chbISMPUnit.Name = "chbISMPUnit"
        Me.chbISMPUnit.Size = New System.Drawing.Size(74, 17)
        Me.chbISMPUnit.TabIndex = 7
        Me.chbISMPUnit.Text = "ISMP Unit"
        Me.chbISMPUnit.UseVisualStyleBackColor = True
        '
        'chbISMPProgramManager
        '
        Me.chbISMPProgramManager.AutoSize = True
        Me.chbISMPProgramManager.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbISMPProgramManager.Location = New System.Drawing.Point(24, 171)
        Me.chbISMPProgramManager.Name = "chbISMPProgramManager"
        Me.chbISMPProgramManager.Size = New System.Drawing.Size(139, 17)
        Me.chbISMPProgramManager.TabIndex = 8
        Me.chbISMPProgramManager.Text = "ISMP Program Manager"
        Me.chbISMPProgramManager.UseVisualStyleBackColor = True
        '
        'chbISMPUnitManager
        '
        Me.chbISMPUnitManager.AutoSize = True
        Me.chbISMPUnitManager.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbISMPUnitManager.Location = New System.Drawing.Point(44, 188)
        Me.chbISMPUnitManager.Name = "chbISMPUnitManager"
        Me.chbISMPUnitManager.Size = New System.Drawing.Size(119, 17)
        Me.chbISMPUnitManager.TabIndex = 9
        Me.chbISMPUnitManager.Text = "ISMP Unit Manager"
        Me.chbISMPUnitManager.UseVisualStyleBackColor = True
        '
        'chbTestNotification
        '
        Me.chbTestNotification.AutoSize = True
        Me.chbTestNotification.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTestNotification.Location = New System.Drawing.Point(20, 205)
        Me.chbTestNotification.Name = "chbTestNotification"
        Me.chbTestNotification.Size = New System.Drawing.Size(143, 17)
        Me.chbTestNotification.TabIndex = 10
        Me.chbTestNotification.Text = "Test Notification Number"
        Me.chbTestNotification.UseVisualStyleBackColor = True
        '
        'chbWitnessingEngineer
        '
        Me.chbWitnessingEngineer.AutoSize = True
        Me.chbWitnessingEngineer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbWitnessingEngineer.Location = New System.Drawing.Point(48, 222)
        Me.chbWitnessingEngineer.Name = "chbWitnessingEngineer"
        Me.chbWitnessingEngineer.Size = New System.Drawing.Size(115, 17)
        Me.chbWitnessingEngineer.TabIndex = 11
        Me.chbWitnessingEngineer.Text = "Test Witnessed By"
        Me.chbWitnessingEngineer.UseVisualStyleBackColor = True
        '
        'chbOtherWitnessingEngineer
        '
        Me.chbOtherWitnessingEngineer.AutoSize = True
        Me.chbOtherWitnessingEngineer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbOtherWitnessingEngineer.Location = New System.Drawing.Point(11, 239)
        Me.chbOtherWitnessingEngineer.Name = "chbOtherWitnessingEngineer"
        Me.chbOtherWitnessingEngineer.Size = New System.Drawing.Size(152, 17)
        Me.chbOtherWitnessingEngineer.TabIndex = 12
        Me.chbOtherWitnessingEngineer.Text = "Other Witnessing Engiener"
        Me.chbOtherWitnessingEngineer.UseVisualStyleBackColor = True
        '
        'chbCC
        '
        Me.chbCC.AutoSize = True
        Me.chbCC.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbCC.Location = New System.Drawing.Point(374, 239)
        Me.chbCC.Name = "chbCC"
        Me.chbCC.Size = New System.Drawing.Size(38, 17)
        Me.chbCC.TabIndex = 24
        Me.chbCC.Text = "cc"
        Me.chbCC.UseVisualStyleBackColor = True
        '
        'chbComplianceManager
        '
        Me.chbComplianceManager.AutoSize = True
        Me.chbComplianceManager.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbComplianceManager.Location = New System.Drawing.Point(286, 222)
        Me.chbComplianceManager.Name = "chbComplianceManager"
        Me.chbComplianceManager.Size = New System.Drawing.Size(126, 17)
        Me.chbComplianceManager.TabIndex = 23
        Me.chbComplianceManager.Text = "Compliance Manager"
        Me.chbComplianceManager.UseVisualStyleBackColor = True
        '
        'chbCompletedByISMP
        '
        Me.chbCompletedByISMP.AutoSize = True
        Me.chbCompletedByISMP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbCompletedByISMP.Location = New System.Drawing.Point(293, 205)
        Me.chbCompletedByISMP.Name = "chbCompletedByISMP"
        Me.chbCompletedByISMP.Size = New System.Drawing.Size(119, 17)
        Me.chbCompletedByISMP.TabIndex = 22
        Me.chbCompletedByISMP.Text = "Completed by ISMP"
        Me.chbCompletedByISMP.UseVisualStyleBackColor = True
        '
        'chbAssignedToEngineer
        '
        Me.chbAssignedToEngineer.AutoSize = True
        Me.chbAssignedToEngineer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbAssignedToEngineer.Location = New System.Drawing.Point(257, 188)
        Me.chbAssignedToEngineer.Name = "chbAssignedToEngineer"
        Me.chbAssignedToEngineer.Size = New System.Drawing.Size(155, 17)
        Me.chbAssignedToEngineer.TabIndex = 21
        Me.chbAssignedToEngineer.Text = "Assigned to ISMP Engineer"
        Me.chbAssignedToEngineer.UseVisualStyleBackColor = True
        '
        'chbReceivedByAPB
        '
        Me.chbReceivedByAPB.AutoSize = True
        Me.chbReceivedByAPB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbReceivedByAPB.Location = New System.Drawing.Point(259, 171)
        Me.chbReceivedByAPB.Name = "chbReceivedByAPB"
        Me.chbReceivedByAPB.Size = New System.Drawing.Size(153, 17)
        Me.chbReceivedByAPB.TabIndex = 20
        Me.chbReceivedByAPB.Text = "Received by APB by ISMP"
        Me.chbReceivedByAPB.UseVisualStyleBackColor = True
        '
        'chbDaysInAPB
        '
        Me.chbDaysInAPB.AutoSize = True
        Me.chbDaysInAPB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbDaysInAPB.Location = New System.Drawing.Point(280, 154)
        Me.chbDaysInAPB.Name = "chbDaysInAPB"
        Me.chbDaysInAPB.Size = New System.Drawing.Size(132, 17)
        Me.chbDaysInAPB.TabIndex = 19
        Me.chbDaysInAPB.Text = "Days in APB/Engineer"
        Me.chbDaysInAPB.UseVisualStyleBackColor = True
        '
        'chbDatesTested
        '
        Me.chbDatesTested.AutoSize = True
        Me.chbDatesTested.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbDatesTested.Location = New System.Drawing.Point(316, 137)
        Me.chbDatesTested.Name = "chbDatesTested"
        Me.chbDatesTested.Size = New System.Drawing.Size(96, 17)
        Me.chbDatesTested.TabIndex = 18
        Me.chbDatesTested.Text = "Date(s) Tested"
        Me.chbDatesTested.UseVisualStyleBackColor = True
        '
        'chbISMPComplianceDetermination
        '
        Me.chbISMPComplianceDetermination.AutoSize = True
        Me.chbISMPComplianceDetermination.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbISMPComplianceDetermination.Location = New System.Drawing.Point(234, 120)
        Me.chbISMPComplianceDetermination.Name = "chbISMPComplianceDetermination"
        Me.chbISMPComplianceDetermination.Size = New System.Drawing.Size(178, 17)
        Me.chbISMPComplianceDetermination.TabIndex = 17
        Me.chbISMPComplianceDetermination.Text = "ISMP Compliance Determination"
        Me.chbISMPComplianceDetermination.UseVisualStyleBackColor = True
        '
        'chbTestingFirm
        '
        Me.chbTestingFirm.AutoSize = True
        Me.chbTestingFirm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbTestingFirm.Location = New System.Drawing.Point(329, 103)
        Me.chbTestingFirm.Name = "chbTestingFirm"
        Me.chbTestingFirm.Size = New System.Drawing.Size(83, 17)
        Me.chbTestingFirm.TabIndex = 16
        Me.chbTestingFirm.Text = "Testing Firm"
        Me.chbTestingFirm.UseVisualStyleBackColor = True
        '
        'chbMethodUsed
        '
        Me.chbMethodUsed.AutoSize = True
        Me.chbMethodUsed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbMethodUsed.Location = New System.Drawing.Point(259, 86)
        Me.chbMethodUsed.Name = "chbMethodUsed"
        Me.chbMethodUsed.Size = New System.Drawing.Size(153, 17)
        Me.chbMethodUsed.TabIndex = 15
        Me.chbMethodUsed.Text = "Method Used to Determine"
        Me.chbMethodUsed.UseVisualStyleBackColor = True
        '
        'chbPollutant
        '
        Me.chbPollutant.AutoSize = True
        Me.chbPollutant.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbPollutant.Location = New System.Drawing.Point(288, 69)
        Me.chbPollutant.Name = "chbPollutant"
        Me.chbPollutant.Size = New System.Drawing.Size(124, 17)
        Me.chbPollutant.TabIndex = 14
        Me.chbPollutant.Text = "Pollutant Determined"
        Me.chbPollutant.UseVisualStyleBackColor = True
        '
        'chbSourceTested
        '
        Me.chbSourceTested.AutoSize = True
        Me.chbSourceTested.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chbSourceTested.Location = New System.Drawing.Point(316, 52)
        Me.chbSourceTested.Name = "chbSourceTested"
        Me.chbSourceTested.Size = New System.Drawing.Size(96, 17)
        Me.chbSourceTested.TabIndex = 13
        Me.chbSourceTested.Text = "Source Tested"
        Me.chbSourceTested.UseVisualStyleBackColor = True
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(169, 50)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.ReadOnly = True
        Me.txtReferenceNumber.Size = New System.Drawing.Size(66, 20)
        Me.txtReferenceNumber.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(66, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Reference Number"
        '
        'ISMPConfidentialData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 625)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtReferenceNumber)
        Me.Controls.Add(Me.TCDocuments)
        Me.Controls.Add(Me.chbComplianceManager)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.chbCompletedByISMP)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.chbAssignedToEngineer)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.chbReceivedByAPB)
        Me.Controls.Add(Me.chbDaysInAPB)
        Me.Controls.Add(Me.chbAIRSNumber)
        Me.Controls.Add(Me.chbDatesTested)
        Me.Controls.Add(Me.chbFacilityName)
        Me.Controls.Add(Me.chbISMPComplianceDetermination)
        Me.Controls.Add(Me.chbLocation)
        Me.Controls.Add(Me.chbTestingFirm)
        Me.Controls.Add(Me.chbReportType)
        Me.Controls.Add(Me.chbMethodUsed)
        Me.Controls.Add(Me.chbISMPReviewer)
        Me.Controls.Add(Me.chbPollutant)
        Me.Controls.Add(Me.chbISMPUnit)
        Me.Controls.Add(Me.chbSourceTested)
        Me.Controls.Add(Me.chbISMPProgramManager)
        Me.Controls.Add(Me.chbOtherWitnessingEngineer)
        Me.Controls.Add(Me.chbISMPUnitManager)
        Me.Controls.Add(Me.chbWitnessingEngineer)
        Me.Controls.Add(Me.chbTestNotification)
        Me.Controls.Add(Me.chbCC)
        Me.Location = New System.Drawing.Point(25, 0)
        Me.Name = "ISMPConfidentialData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Confidential Data"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TCDocuments.ResumeLayout(False)
        Me.TPOneStack.ResumeLayout(False)
        Me.TPOneStack.PerformLayout()
        Me.TCOneStack.ResumeLayout(False)
        Me.TPTwoRuns.ResumeLayout(False)
        Me.TPTwoRuns.PerformLayout()
        Me.TPThreeRuns.ResumeLayout(False)
        Me.TPThreeRuns.PerformLayout()
        Me.TPFourRuns.ResumeLayout(False)
        Me.TPFourRuns.PerformLayout()
        Me.TPLoadingRack.ResumeLayout(False)
        Me.TPLoadingRack.PerformLayout()
        Me.TPPulpCondensate.ResumeLayout(False)
        Me.TPPulpCondensate.PerformLayout()
        Me.TPGasConcentration.ResumeLayout(False)
        Me.TPGasConcentration.PerformLayout()
        Me.TPFlare.ResumeLayout(False)
        Me.TPFlare.PerformLayout()
        Me.TPMethod9.ResumeLayout(False)
        Me.TCMethod9.ResumeLayout(False)
        Me.TPMethod9Single.ResumeLayout(False)
        Me.TPMethod9Single.PerformLayout()
        Me.TPMethod9Multi.ResumeLayout(False)
        Me.TPMethod9Multi.PerformLayout()
        Me.TPMemorandum.ResumeLayout(False)
        Me.TPMemorandum.PerformLayout()
        Me.TCMemorandum.ResumeLayout(False)
        Me.TPStandard.ResumeLayout(False)
        Me.TPStandard.PerformLayout()
        Me.TPToFile.ResumeLayout(False)
        Me.TPToFile.PerformLayout()
        Me.TPPTE.ResumeLayout(False)
        Me.TPPTE.PerformLayout()
        Me.TPRATA.ResumeLayout(False)
        Me.TPRATA.PerformLayout()
        Me.TPTwoStack.ResumeLayout(False)
        Me.TPTwoStack.PerformLayout()
        Me.TCTwoStack.ResumeLayout(False)
        Me.TPTwoStackStandard.ResumeLayout(False)
        Me.TPTwoStackStandard.PerformLayout()
        Me.TPDRE.ResumeLayout(False)
        Me.TPDRE.PerformLayout()
        Me.TPMethod22.ResumeLayout(False)
        Me.TPMethod22.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiBack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TCDocuments As System.Windows.Forms.TabControl
    Friend WithEvents TPOneStack As System.Windows.Forms.TabPage
    Friend WithEvents TPLoadingRack As System.Windows.Forms.TabPage
    Friend WithEvents chbOtherWitnessingEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents chbWitnessingEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents chbTestNotification As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPUnitManager As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPProgramManager As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPReviewer As System.Windows.Forms.CheckBox
    Friend WithEvents chbReportType As System.Windows.Forms.CheckBox
    Friend WithEvents chbLocation As System.Windows.Forms.CheckBox
    Friend WithEvents chbFacilityName As System.Windows.Forms.CheckBox
    Friend WithEvents chbAIRSNumber As System.Windows.Forms.CheckBox
    Friend WithEvents chbCC As System.Windows.Forms.CheckBox
    Friend WithEvents chbComplianceManager As System.Windows.Forms.CheckBox
    Friend WithEvents chbCompletedByISMP As System.Windows.Forms.CheckBox
    Friend WithEvents chbAssignedToEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents chbReceivedByAPB As System.Windows.Forms.CheckBox
    Friend WithEvents chbDaysInAPB As System.Windows.Forms.CheckBox
    Friend WithEvents chbDatesTested As System.Windows.Forms.CheckBox
    Friend WithEvents chbISMPComplianceDetermination As System.Windows.Forms.CheckBox
    Friend WithEvents chbTestingFirm As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethodUsed As System.Windows.Forms.CheckBox
    Friend WithEvents chbPollutant As System.Windows.Forms.CheckBox
    Friend WithEvents chbSourceTested As System.Windows.Forms.CheckBox
    Friend WithEvents TCOneStack As System.Windows.Forms.TabControl
    Friend WithEvents TPTwoRuns As System.Windows.Forms.TabPage
    Friend WithEvents chbOneStack2Temp2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Temp1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Moist1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Moist2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2ACFM1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Run2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Run1 As System.Windows.Forms.CheckBox
    Friend WithEvents TPThreeRuns As System.Windows.Forms.TabPage
    Friend WithEvents TPFourRuns As System.Windows.Forms.TabPage
    Friend WithEvents chbOneStackAppRequire As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackControlEquip As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackAllowEmiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackAllowEmiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackAllowEmiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackMaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents TPPulpCondensate As System.Windows.Forms.TabPage
    Friend WithEvents TPGasConcentration As System.Windows.Forms.TabPage
    Friend WithEvents TPFlare As System.Windows.Forms.TabPage
    Friend WithEvents TPPEM As System.Windows.Forms.TabPage
    Friend WithEvents TPMethod9 As System.Windows.Forms.TabPage
    Friend WithEvents TPMemorandum As System.Windows.Forms.TabPage
    Friend WithEvents TPRATA As System.Windows.Forms.TabPage
    Friend WithEvents TPTwoStack As System.Windows.Forms.TabPage
    Friend WithEvents TPMethod22 As System.Windows.Forms.TabPage
    Friend WithEvents TPSSCPWork As System.Windows.Forms.TabPage
    Friend WithEvents chbOneStackOtherInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStackPercentAllow As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Poll1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Poll2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Emiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2EmissUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2DSCFM1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2DSCFM2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2Emiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2PollUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2ACFM2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2EmissAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack2PollAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3EmissAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3PollAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Poll1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Emiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3EmissUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3DSCFM1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3PollUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Run1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3ACFM1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Temp1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Moist1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Poll3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Emiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3DSCFM3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Run3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3ACFM3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Temp3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Moist3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Poll2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Emiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3DSCFM2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Run2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3ACFM2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Temp2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack3Moist2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackOtherInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackTestDuration As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackDestReduction As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackPollIN As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackPollOUT As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackAppRequire As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackAllowEmiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackAllowEmiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackMaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackAllowEmiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackEmiss As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpAppRequire As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpAllowEmiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpAllowEmiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpMaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpAllowEmiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpOtherInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpDestructEffic As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpConc3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpTreatment3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpRun3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpConc2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpTreatment2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpRun2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpTreatmentAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpConcAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpConc1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpTreatment1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpTreatmentUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpConcUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpRun1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasOtherInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasPercentAllow As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasPoll3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasEmiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasRun3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasPoll2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasEmiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasRun2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasEmissAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasPollAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasPoll1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasEmiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasEmissUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasPollUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasRun1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasAppRequire As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasAllowEmiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasAllowEmiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasMaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasAllowEmiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareOtherInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlarePercentAllow As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareHeating3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareVelocity3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareRun3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareHeating2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareVelocity2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareRun2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareVelocityAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareHeatingAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareHeating1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareVelocity1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareVelocityUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareHeatingUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareRun1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareAppRequire As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareHeatContent As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareMonitor As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareMaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbFlareAllowLimitations As System.Windows.Forms.CheckBox
    Friend WithEvents TCMethod9 As System.Windows.Forms.TabControl
    Friend WithEvents TPMethod9Single As System.Windows.Forms.TabPage
    Friend WithEvents TPMethod9Multi As System.Windows.Forms.TabPage
    Friend WithEvents chbMethod9AppRequire As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9OpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9AllowEmiss As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9OtherInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9Opacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9TestDuration As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiEquip2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiEquip4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiEquip5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiEquip3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiEquip1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAvg5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAvg4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAvg3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAvg2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAllowEmissUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAllowEmiss5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAllowEmiss4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAllowEmiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAllowEmiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiOpCapacityUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiOpCapacity2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiOpCapacity3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiOpCapacity4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiOpCapacity5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiOpCapacity1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiMaxOpCapacity5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiMaxOpCapacity4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiMaxOpCapacity3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiMaxOpCapacity2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiOtherInfor As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAvg1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiMaxOpCapacityUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiMaxOpCapacity1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiAllowEmiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoAppRequire As System.Windows.Forms.CheckBox
    Friend WithEvents TCMemorandum As System.Windows.Forms.TabControl
    Friend WithEvents TPStandard As System.Windows.Forms.TabPage
    Friend WithEvents chbMemoStandardMemo As System.Windows.Forms.CheckBox
    Friend WithEvents TPToFile As System.Windows.Forms.TabPage
    Friend WithEvents chbMemoToFileMemo As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoToFileSerial As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoToFileManufacture As System.Windows.Forms.CheckBox
    Friend WithEvents TPPTE As System.Windows.Forms.TabPage
    Friend WithEvents chbMemoPTEMemo As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoPTEAllowEmiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoPTEAllowEmiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoPTEOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoPTEMaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoPTEAllowEmiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef12 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef11 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef10 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef8 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARef1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATARelativeAcc As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATAStatement As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATAUnits As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATADiluent As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATAAppStandard As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATAAppRegulation As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS9 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS12 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS11 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS10 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS8 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS7 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS5 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS6 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATACMS1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbRATAOtherInformation As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackAppRequire As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackAllowEmiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackAllowEmiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackMaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackAllowEmiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents TCTwoStack As System.Windows.Forms.TabControl
    Friend WithEvents TPTwoStackStandard As System.Windows.Forms.TabPage
    Friend WithEvents chbTwoStackStandName2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandName1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPoll1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmiss1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandDSCFM1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandRun1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandACFM1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTemp1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandMoist1a As System.Windows.Forms.CheckBox
    Friend WithEvents TPDRE As System.Windows.Forms.TabPage
    Friend WithEvents chbTwoStackStandPollAvg2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPollAvg1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPoll3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmiss3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandDSCFM3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandRun3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandACFM3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTemp3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandMoist3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPoll2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmiss2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandDSCFM2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandRun2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandACFM2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTemp2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandMoist2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPoll1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmiss1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandDSCFM1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandRun1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandACFM1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTemp1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandMoist1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPoll3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmiss3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandDSCFM3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandRun3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandACFM3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTemp3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandMoist3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPoll2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmiss2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandDSCFM2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandRun2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandACFM2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTemp2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandMoist2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackOtherInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPercentAllow As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTotalAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTotal3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTotal2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandTotal1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmissUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandPollUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmissAvg2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackStandEmissAvg1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREDestructionEff As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22Emission As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22OtherInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22AppReg As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22TestDuration As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22OpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22MaxOpCapacity As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod22AllowEmiss As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Poll4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Emiss4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4DSCFM4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Run4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4ACFM4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Temp4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Moist4 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Poll3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Emiss3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4DSCFM3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Run3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4ACFM3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Temp3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Moist3 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Poll2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Emiss2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4DSCFM2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Run2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4ACFM2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Temp2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Moist2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4EmissAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4PollAvg As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Poll1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Emiss1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4EmissUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4DSCFM1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4PollUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Run1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4ACFM1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Temp1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbOneStack4Moist1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbLoadingRackControlEquip As System.Windows.Forms.CheckBox
    Friend WithEvents chbPulpControlEquip As System.Windows.Forms.CheckBox
    Friend WithEvents chbGasControlEquip As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9ControlEquip As System.Windows.Forms.CheckBox
    Friend WithEvents chbMethod9MultiControlEquip As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackControlEquip As System.Windows.Forms.CheckBox
    Friend WithEvents chbMemoPTEControlEquip As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmissUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPollUnit As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmissAvg2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmissAvg1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPollAvg2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPollAvg1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPoll3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmiss3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREDSCFM3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRERun3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREACFM3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRETemp3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREMoist3b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPoll2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmiss2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREDSCFM2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRERun2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREACFM2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRETemp2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREMoist2b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPoll1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmiss1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREDSCFM1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRERun1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREACFM1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRETemp1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREMoist1b As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPoll3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmiss3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREDSCFM3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRERun3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREACFM3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRETEmp3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREMoist3a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPoll2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmiss2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREDSCFM2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRERun2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREACFM2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRETemp2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREMoist2a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREName2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREName1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREPoll1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREEmiss1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREDSCFM1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRERun1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREACFM1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDRETemp1a As System.Windows.Forms.CheckBox
    Friend WithEvents chbTwoStackDREMoist1a As System.Windows.Forms.CheckBox
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chbMethod9MultiAppRequire As System.Windows.Forms.CheckBox
End Class
