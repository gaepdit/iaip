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
    Friend Const APP_ROOT_NAMESPACE As String = "Iaip"
    Friend DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
    Friend SupportUrl As New Uri("http://airpermit.dnr.state.ga.us/iaip/")
    Friend ChangelogUrl As New Uri("http://airpermit.dnr.state.ga.us/iaip/changelog.html")
    Friend DateFormat As String = "dd-MMM-yyyy"
    Friend CurrentUser As IaipUser

#End Region

#Region " Old public variables "
    Public OracleDate As String = Format(Date.Today, "dd-MMM-yyyy")
    Public UserGCode As String
    Public UserAccounts As String
    Public UserName As String
    Public UserBranch As String
    Public UserProgram As String
    Public UserUnit As String
    Public AccountFormAccess(150, 4) As String

    Public cmd, cmd2, cmd3 As OracleCommand
    Public dr, dr2, dr3 As OracleDataReader
    Public recExist As Boolean
    Public temp As String
#End Region

#End Region

#Region " All Forms "

#Region " Universal Screens "
    Public PrintOut As IAIPPrintOut
    Public EditContacts As IAIPEditContacts ' TODO DWW: Remove
    Public EditFacilityLocation As IAIPEditFacilityLocation ' TODO DWW: Remove
    Public EditHeaderData As IAIPEditHeaderData ' TODO DWW: Remove
    Public EditAirProgramPollutants As IAIPEditAirProgramPollutants
    Public EditSubParts As IAIPEditSubParts ' TODO DWW: Remove
    Public ListTool As IAIPListTool ' TODO DWW: Remove
    Public ProfileUpdate As IAIPProfileUpdate
    Public FacilityPrintOut As IaipFacilitySummaryPrint ' TODO DWW: Remove
    Public FacilityCreator As IAIPFacilityCreator ' TODO DWW: Remove
#End Region

#Region " ISMP Screens "
    Public ISMPMemoEdit As ISMPMemo ' TODO DWW: Remove
    Public ISMPNotificationLogForm As ISMPNotificationLog ' TODO DWW: Remove
    Public ISMPCloseAndPrint As ISMPClosePrint ' TODO DWW: Remove
    Public ISMPAddPollutant As ISMPAddPollutants ' TODO DWW: Remove
    Public ISMPAddTestingFirm As ISMPAddTestingFirms ' TODO DWW: Remove
    Public ISMPConfidential As ISMPConfidentialData ' TODO DWW: Remove
    Public TestFirmComments As ISMPTestFirmComments ' TODO DWW: Remove
    Public StaffReports As ISMPStaffReports ' TODO DWW: Remove
#End Region

#Region " SSCP Screens "
    Public SSCP_Work As SSCPComplianceLog ' TODO DWW: Remove
    Public SSCPReports As SSCPEvents
    Public SSCPFCE As SSCPFCEWork
    Public SSCPFacAssign As SSCPFacAssignment ' TODO DWW: Remove
    Public SSCPEngWork As SSCPWorkEnTry ' TODO DWW: Remove
    Public SSCPRequest As SSCPInformationRequest ' TODO DWW: Remove
    Public SSCPInspectionscheduleTool As SSCPInspectionscheduleLink ' TODO DWW: Remove
#End Region

#Region " SSPP Screens "
    Public PermitTrackingLog As SSPPApplicationTrackingLog ' TODO DWW: Remove
    Public AttainmentStatus As SSPPAttainmentStatus ' TODO DWW: Remove
    Public FeeContact As SSPP_FeeContact ' TODO DWW: Remove
#End Region

#Region " SBEAP Screens "
    Public ClientSummary As SBEAPClientSummary
    Public CaseWork As SBEAPCaseWork
#End Region

#End Region

End Module