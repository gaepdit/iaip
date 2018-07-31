Imports System.Configuration

Module AppVariables

    ' Global variables 

#Region " DB Connections "

    Friend CurrentServerEnvironment As ServerEnvironment = ServerEnvironment.Production
    Friend DB As EpdIt.DBHelper
    Friend ExceptionLogger As SharpRaven.RavenClient

#End Region

#Region " URLs "

    Friend DocumentationUrl As New Uri("https://sites.google.com/site/iaipdocs/")
    Friend SupportUrl As New Uri("https://iaip.gaepd.org/")
    Friend ChangelogUrl As New Uri("https://iaip.gaepd.org/changelog/")
    Friend PrereqInstallUrl As New Uri("https://iaip.gaepd.org/pre-install/")

    Friend MapUrlFragment As String = "http://maps.google.com/maps?q="
    Friend PermitSearchUrlFragment As String = "http://permitsearch.gaepd.org/?AirsNumber="
    Friend VesaUrl As New Uri("https://vnap.cloudapp.net/vnap")
    Friend ReadOnly GecoUrl As New Uri(ConfigurationManager.AppSettings("GecoUrl"))

#End Region

#Region " App info "

    Friend CurrentUser As IaipUser
    Friend AppFirstRun As Boolean = False
    Friend AppUpdated As Boolean = False
    Friend AccountFormAccess(150, 4) As String

    Friend Const APP_NAME As String = "IAIP"
    Friend Const APP_FRIENDLY_NAME As String = "Integrated Air Information Platform"
    Friend Const APP_ROOT_NAMESPACE As String = "Iaip"
    Friend Const MIN_USERNAME_LENGTH As Integer = 3
    Friend Const MIN_PASSWORD_LENGTH As Integer = 3

#End Region

#Region " API Keys "

    Friend ReadOnly SENTRY_DSN As String = ConfigurationManager.AppSettings("SENTRY_DSN")
    Friend ReadOnly GOOGLE_MAPS_API_KEY As String = ConfigurationManager.AppSettings("GOOGLE_MAPS_API_KEY")

#End Region

#Region " String formats "

    Friend Const DateParseExactFormat As String = "yyyy-MM-dd HH:mm tt"
    Friend Const DateFormat As String = "dd-MMM-yyyy"
    Friend Const DateStringFormat As String = "{0:dd-MMM-yyyy}"
    Friend Const DisplayZeroAsBlank As String = "0;; "
    Friend Const DisplayZeroAsNA As String = "0;;N/A"

    Friend ReadOnly Property TodayFormatted As String
        Get
            Return Format(Today, DateFormat)
        End Get
    End Property

#End Region

#Region " Geographic constants "

    Friend Const GA_STATE_CODE As String = "GA"
    Friend Const GA_STATE_NUMERIC_CODE As String = "13"
    Friend Const GA_EPA_REGION_CODE As String = "04"

#End Region

#Region " Business logic constants "

    Public Const FCE_DATA_PERIOD As Integer = 1 ' Year
    Public Const FCE_ENFORCEMENT_DATA_PERIOD As Integer = 5 ' Years
    Public Const MIN_FCE_SPAN_CLASS_A As Integer = 2 ' Years
    Public Const MIN_FCE_SPAN_CLASS_SM As Integer = 5 ' Years
    Public Const MIN_FCE_SPAN_CLASS_M As Integer = 7 ' Years

#End Region

#Region " All Forms "
    ' TODO DWW: Remove global form variables

    Public ClientSummary As SBEAPClientSummary
    Public CaseWork As SBEAPCaseWork

#End Region

End Module