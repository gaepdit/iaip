Imports Oracle.DataAccess.Client
Imports System.IO
Imports System.Collections.Generic

Module subMain

#Region " DB Connections "

    Friend Const DBNameSpace As String = "AIRBRANCH"
    Friend CurrentServerEnvironment As DB.ServerEnvironment = DB.DefaultServerEnvironment
    Friend CurrentConnection As New OracleConnection(DB.CurrentConnectionString)

#End Region

#Region " App-wide public variables "

#Region " New public variables (by Doug) "

    Friend Const APP_NAME As String = "IAIP"
    Friend Const APP_FRIENDLY_NAME As String = "Integrated Air Information Platform"
    Friend DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
    Friend SupportUrl As New Uri("http://airpermit.dnr.state.ga.us/iaip/")
    Friend ChangelogUrl As New Uri("http://airpermit.dnr.state.ga.us/iaip/changelog.html")
    Friend DateFormat As String = "dd-MMM-yyyy"
    Friend CurrentUser As IaipUser

#End Region

#Region " Old public variables "
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

#End Region

#End Region

#Region " All Forms "

#Region "Universal Screens"
    'Public APB110 As IAIPLogIn

    'Public NavigationScreen As IAIPNavigation
    'Public FacilityLookUpTool As IAIPFacilityLookUpTool
    Public PrintOut As IAIPPrintOut
    'Public FacilitySummary As IAIPFacilitySummary

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
    'Public ISMPTestReportsEntry As ISMPTestReports
    Public TestFirmComments As ISMPTestFirmComments
    'Public DevelopersTools As DMUDeveloperTools
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
    'Public SSCPFCESelector As SSCPFCESelectorTool
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

#Region " SBEAP Screens "
    Public ClientSummary As SBEAPClientSummary
    Public CaseWork As SBEAPCaseWork
    'Public PrintForm As SBEAPPrintForms
    Public ClientSearchTool As SBEAPClientSearchTool
    Public CaseLog As SBEAPCaseLog
    Public ReportTool As SBEAPReports
    Public PhoneLog As SBEAPPhoneLog
    Public MiscTools As SBEAPMiscTools
#End Region

#End Region

End Module