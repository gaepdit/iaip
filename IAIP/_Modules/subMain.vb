Imports Oracle.ManagedDataAccess.Client
Imports System.IO
Imports System.Collections.Generic

Module subMain

#Region " DB Connections "

    Friend CurrentServerEnvironment As DB.ServerEnvironment = DB.DefaultServerEnvironment
    Friend CurrentConnection As New OracleConnection(DB.CurrentConnectionString)

#End Region

#Region " App-wide public variables "

#Region " New public variables (by Doug) "

    Friend Const APP_NAME As String = "IAIP"
    Friend Const APP_FRIENDLY_NAME As String = "Integrated Air Information Platform"
    Friend Const APP_ROOT_NAMESPACE As String = "Iaip"

    Friend DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
    Friend SupportUrl As New Uri("http://dmu.georgiaair.org/iaip/")
    Friend ChangelogUrl As New Uri("http://dmu.georgiaair.org/iaip/changelog.html")
    Friend MapUrlFragment As New String("http://maps.google.com/maps?z=14&q=")
    Friend PermitSearchUrlFragment As New String("http://search.georgiaair.org/?AirsNumber=")

    Friend DateFormat As String = "dd-MMM-yyyy"
    Friend DateTimeFormat As String = "dd-MMM-yyyy h:mm tt"
    Friend DateStringFormat As String = "{0:dd-MMM-yyyy}"
    Friend DateTimeStringFormat As String = "{0:dd-MMM-yyyy h:mm tt}"

    Friend CurrentUser As IaipUser
    Friend AppFirstRun As Boolean = False
    Friend AppUpgraded As Boolean = False

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
    ' TODO DWW: Remove global form variables

#Region " Universal Screens "
    Public PrintOut As IAIPPrintOut
    Public ProfileUpdate As IAIPProfileUpdate
#End Region

#Region " ISMP Screens "
    Public ISMPMemoEdit As ISMPMemo
    Public ISMPNotificationLogForm As ISMPNotificationLog
    Public ISMPAddPollutant As ISMPAddPollutants
    Public ISMPAddTestingFirm As ISMPAddTestingFirms
    Public ISMPConfidential As ISMPConfidentialData
    Public TestFirmComments As ISMPTestFirmComments
    Public StaffReports As ISMPStaffReports
#End Region

#Region " SSCP Screens "
    Public SSCP_Work As SSCPComplianceLog
    Public SSCPFCE As SSCPFCEWork
    Public SSCPEngWork As SSCPWorkEnTry
#End Region

#Region " SSPP Screens "
    Public AttainmentStatus As SSPPAttainmentStatus
    Public FeeContact As SSPP_FeeContact
#End Region

#Region " SBEAP Screens "
    Public ClientSummary As SBEAPClientSummary
    Public CaseWork As SBEAPCaseWork
#End Region

#End Region

End Module