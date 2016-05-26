Module AppVariables

    ' App-wide public variables 

#Region " DB Connections "

    Friend CurrentServerEnvironment As ServerEnvironment = ServerEnvironment.PRD
    Friend CurrentConnection As SqlClient.SqlConnection
    Friend DB As EpdIt.DBHelper

#End Region

#Region " URLs "

    Friend DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
    Friend SupportUrl As New Uri("http://dmu.georgiaair.org/iaip/")
    Friend ChangelogUrl As New Uri("http://dmu.georgiaair.org/iaip/changelog.html")
    Friend MapUrlFragment As New String("http://maps.google.com/maps?q=")
    Friend PermitSearchUrlFragment As New String("http://search.georgiaair.org/?AirsNumber=")

#End Region

#Region " App info "

    Friend CurrentUser As IaipUser
    Friend AppFirstRun As Boolean = False
    Friend AppUpdated As Boolean = False

    Friend Const APP_NAME As String = "IAIP"
    Friend Const APP_FRIENDLY_NAME As String = "Integrated Air Information Platform"
    Friend Const APP_ROOT_NAMESPACE As String = "Iaip"
    Friend Const MIN_USERNAME_LENGTH As Integer = 3
    Friend Const MIN_PASSWORD_LENGTH As Integer = 3

#End Region

#Region " API Keys "

    Friend Const EQATEC_ANALYTICS_API_KEY As String = "F635C42ABA7B4DE886EEFCEE31C98000"
    Friend Const GOOGLE_MAPS_API_KEY As String = "AIzaSyAOMeyIrtZeEJb1Pci5jgtn_Uh3wr0NP14"

#End Region

#Region " String formats "

    Friend Const DateParseExactFormat As String = "yyyy-MM-dd HH:mm tt"
    Friend Const DateFormat As String = "dd-MMM-yyyy"
    Friend Const DateStringFormat As String = "{0:dd-MMM-yyyy}"
    'Friend Const DateTimeFormat As String = "dd-MMM-yyyy h:mm tt"
    'Friend Const DateTimeStringFormat As String = "{0:dd-MMM-yyyy h:mm tt}"
    Friend Const DisplayZeroAsBlank As String = "0;; "
    Friend Const DisplayZeroAsNA As String = "0;;N/A"

#End Region

#Region " Regional constants "

    Friend Const GA_STATE_CODE As String = "GA"
    Friend Const GA_STATE_NUMERIC_CODE As String = "13"
    Friend Const GA_EPA_REGION_CODE As String = "04"

#End Region

#Region " Business logic constants "

    Public Const FCE_DATA_PERIOD As Integer = 1
    Public Const FCE_ENFORCEMENT_DATA_PERIOD As Integer = 5
    Public Const MIN_FCE_SPAN_CLASS_A As Integer = 2
    Public Const MIN_FCE_SPAN_CLASS_SM As Integer = 5

#End Region

#Region " Old public variables "
    ' TODO: Destroy these

    Public OracleDate As String = Format(Today, DateFormat) ' Replace in SQL with SYSDATE; replace in code with Today
    Public AccountFormAccess(150, 4) As String

    Public cmd, cmd2 As SqlClient.SqlCommand
    Public dr, dr2 As SqlClient.SqlDataReader
    Public recExist As Boolean
    Public temp As String

#End Region

#Region " All Forms "
    ' TODO DWW: Remove global form variables

#Region " Universal Screens "
    Public PrintOut As IAIPPrintOut
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
    Public SSCPFCE As SSCPFCEWork
#End Region

#Region " SSPP Screens "
    Public FeeContact As SSPP_FeeContact
#End Region

#Region " SBEAP Screens "
    Public ClientSummary As SBEAPClientSummary
    Public CaseWork As SBEAPCaseWork
#End Region

#End Region

End Module