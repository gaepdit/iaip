Imports Oracle.DataAccess.Client
Imports System.IO
Imports System.Collections.Generic

Module subMain

#Region "DB Connection Strings"
    Friend Const DBNameSpace As String = "AIRBRANCH"

    Friend PrdConnString As String = "Data Source = PRD; User ID = AIRBRANCH_APP_USER; Password = " & SimpleCrypt("ÁÚ·Ú±Ï") & ";"
    'Public TestConnString As String = "Data Source = TEST; User ID = AIRBRANCH_APP_USER; Password = " & SimpleCrypt("¡…“¡––’”≈“∞≥") & ";"
    Friend DevConnString As String = "Data Source = DEV; User ID = AIRBRANCH; Password = " & SimpleCrypt("ÛÌÔÁ·ÏÂÚÙ") & ";"
    Friend CurrentConnString As String = PrdConnString

    Public TVProjectConnString As String = "Data Source = leia.dnr.state.ga.us:1521/DEV; User ID = airtvproject; Password = airproject;"
    Public TVApplicationConnString As String = "Data Source = leia.dnr.state.ga.us:1521/DEV; User ID = airtvapplication; Password = airapp;"

    Public Conn As New OracleConnection(PrdConnString)
    Public ConnTVProject As New OracleConnection(TVProjectConnString)
    Public ConnTVApplication As New OracleConnection(TVApplicationConnString)

    Public PRDCRLogIn As String = "AirBranch_App_User"
    Public PRDCRPassWord As String = SimpleCrypt("ÁÚ·Ú±Ï")

    'Public TESTCRLogIn As String = "AirBranch_App_User"
    'Public TESTCRPassWord As String = SimpleCrypt("¡…“¡––’”≈“∞≥")

    Public DEVCRLogIn As String = "AirBranch"
    Public DEVCRPassWord As String = SimpleCrypt("ÛÌÔÁ·ÏÂÚÙ")

    Public CRLogIn As String = PRDCRLogIn
    Public CRPassWord As String = PRDCRPassWord
#End Region

#Region "App-wide public variables"

#Region "New public variables (by Doug)"
    Friend Const AppName As String = "IAIP"
    Friend HelpUrl As String = "https://sites.google.com/site/iaipdocs/"
    'Friend DownloadUrl As String = "http://airpermit.dnr.state.ga.us/iaip/IAIP.update2_6_4.exe"
    Friend AppPath As String = Path.GetDirectoryName(Application.ExecutablePath)
    Friend AboutUrl As String = AppPath & "\docs\ABOUT.html"
    Friend DateFormat As String = "dd-MMM-yyyy"
    'Friend Today As Date = Date.Today
    Friend TodayString As String = Format(Date.Today, DateFormat)
    Friend TestingEnvironment As Boolean = False
#End Region

#Region "Old public variables"
    Public OracleDate As String = Format(Date.Today, "dd-MMM-yyyy")
    Public UserGCode As String
    Public Permissions As String
    Public UserName As String
    Public UserBranch As String
    Public UserProgram As String
    Public UserUnit As String

    Public SQL, SQL2, SQL3 As String
    Public cmd, cmd2, cmd3 As OracleCommand
    Public dr, dr2, dr3 As OracleDataReader
    Public recExist As Boolean
    Public temp As String
    Public AccountArray(150, 4) As String
    Public j As Integer
    Public i As Integer
    'Public DefaultX As Integer
    'Public DefaultY As Integer
    Public Oracledll As String

    'Public t As New System.Timers.Timer(14400000)
    'Public t2 As New System.Timers.Timer(300000)
#End Region

#End Region

#Region "All Forms"
#Region "Universal Screens"
    'Public APB110 As IAIPLogIn

    Public NavigationScreen As IAIPNavigation
    Public FacilityLookUpTool As IAIPFacilityLookUpTool
    Public PrintOut As IAIPPrintOut
    Public FacilitySummary As IAIPFacilitySummary

    Public QueryGenerator As IAIPQueryGenerator
    Public EditContacts As IAIPEditContacts
    'Public EditContacts2 As DEVEditContacts
    Public EditFacilityLocation As IAIPEditFacilityLocation
    Public EditHeaderData As IAIPEditHeaderData
    Public EditAirProgramPollutants As IAIPEditAirProgramPollutants
    Public EditSubParts As IAIPEditSubParts
    'Public DevelopmentTeam As IAIPDevelopmentTeam
    Public IAIPDistrictTool As IAIPDistrictSourceTool
    Public PermitUploader As IAIPPermitUploader
    Public Validator As AFSValidator
    Public UserAdminTool As IAIPUserAdminTool
    Public ListTool As IAIPListTool
    Public PhoneList As IAIPPhoneList
    Public ProfileUpdate As IAIPProfileUpdate
    Public AFSCompare As IAIPAFSCompare
    Public FacilityPrintOut As IaipFacilitySummaryPrint
    Public LookUpTables As IAIPLookUpTables
    Public FacilityCreator As IAIPFacilityCreator
    Public EISLog As IAIP_EIS_Log
    'Public TitleVProject As DMU_TITLEV_PROJECT

#End Region
#Region "Ambient Monitoring Screens"

#End Region
#Region "ISMP Screens"
    Public ISMPFacility As ISMPTestReportAdministrative
    Public ISMPTestReportInfo As ISMPFacilityInfo
    Public ISMPManagers As ISMPManagersTools
    Public ISMPReportViewer As ISMPMonitoringLog
    Public ISMPMemoViewer As ISMPTestMemoViewer
    Public ISMPRefNum As ISMPReferenceNumber
    Public ISMPMemoEdit As ISMPMemo
    Public ISMPNotificationLogForm As ISMPNotificationLog
    Public ISMPCloseAndPrint As ISMPClosePrint
    'Public ISMPExcelFilePage As ISMPExcelFiles
    Public ISMPAddPollutant As ISMPAddPollutants
    Public ISMPAddTestingFirm As ISMPAddTestingFirms
    'Public ISMPDMU As ISMPDataManagementTools
    Public DMUOnly As DMUTool
    Public ISMPConfidential As ISMPConfidentialData
    Public ISMPTestReportsEntry As ISMPTestReports
    Public TestFirmComments As ISMPTestFirmComments
    Public DevelopersTools As DMUDeveloperTools
    Public StaffTools As DMUStaffTools
    Public TitleVTools As DMUTitleVTools
    Public StaffReports As ISMPStaffReports
    'Public FeeAuditTool As IAIPFeeAuditTool

#End Region
#Region "Mobile & Area Screens"
    'Public RegistrationTool As MASPRegistrationTool

#End Region
#Region "Planning & Support Screens"
    Public FacilityFeeForm As PASPFacilityFee
    Public FeeDeposits As PASPFeeDeposits
    Public Modifications As PASPModifications
    Public FeesReports As PASPFeeReports
    Public VarianceTool As PASPFeeVarianceCheck
    Public WebAppUser As PASPWebApplicationUser
    Public DepositsAmendments As PASPDepositsAmendments
    Public MailoutAndStats As PASPFeeStatistics
    'Public MailoutAndStats As DEVMailoutAndStats
    'Public FeeStat As DEVMailoutAndStats

    Public FeeTools As PASPFeeTools
    'Public ComputerInventory As PASPInventory
    Public FeeStats As PASPFeeAuditLog
    Public FeesLog As PASPFeesLog
    Public FeeManagement As PASPFeeManagement

#End Region
#Region "SSCP Screens"
    Public SSCP_Work As SSCPComplianceLog
    Public ManagersTools As SSCPManagersTools
    Public SSCPReports As SSCPEvents
    'Public SSCP_Enforcement As SSCPEnforcementAudit
    Public SSCPFCE As SSCPFCEWork
    Public SSCPFacAssign As SSCPFacAssignment
    Public SSCPEngWork As SSCPWorkEnTry
    'Public SSCPTemplates As SSCPLetterTemplates
    Public SSCPRequest As SSCPInformationRequest
    Public SSCPFCESelector As SSCPFCESelectorTool
    'Public SSCPSelectEnforcement As SSCPEnforcementSelector
    Public EnforcementChecklist As SSCPEnforcementChecklist
    Public SSCPInspectionsTool As SSCPEngineerInspectionTool
    Public SSCPInspectionscheduleTool As SSCPInspectionscheduleLink
    Public EmissionSummary As SSCPEmissionSummaryTool
    Public SSCPAdmin As SSCPAdministrator
    Public InspectionTool As SSCPInspectionTool
#End Region
#Region "SSPP Screens"
    Public ApplicationLog As SSPPApplicationLog
    Public PermitTrackingLog As SSPPApplicationTrackingLog
    Public AttainmentStatus As SSPPAttainmentStatus
    Public PublicLetter2 As SSPPPublicNoticiesAndAdvisories
    Public StatisticalTools As SSPPStatisticalTools
    Public FeeContact As SSPP_FeeContact

#End Region
#End Region

#Region "App timeouts"

    'Public Sub TimerFired(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
    '    Dim Result As DialogResult

    '    AddHandler t2.Elapsed, AddressOf TimerFired2
    '    t2.Enabled = True

    '    Result = MessageBox.Show("The Integrated Air Information Platform has been open for 4 hours." & vbCrLf & _
    '    "Do you want to continue to have it open?" & vbCrLf & vbCrLf & "IAIP will be terminated in 5 minutes.", "IAIP Connection Time Out.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

    '    Select Case Result
    '        Case DialogResult.Yes
    '            t2.Enabled = False
    '        Case DialogResult.No
    '            Conn.Dispose()
    '            Application.Exit()
    '        Case Else
    '            t2.Enabled = False
    '    End Select
    'End Sub

    'Public Sub TimerFired2(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
    '    Conn.Dispose()
    '    Application.Exit()
    'End Sub

#End Region

End Module