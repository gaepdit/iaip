Imports System.Configuration
Imports System.Threading.Tasks

Namespace DAL

    Module CxApi

        Private ReadOnly ApiUrl As New Uri(New Uri(ConfigurationManager.AppSettings("CxApiUrl")), $"api/{CurrentServerEnvironment}/")

        Const StatusEndpoint As String = "status"
        Const LoginEndpoint As String = "login"
        Const SessionEndpoint As String = "session"
        Const RequestUsernameEndpoint As String = "request-username"
        Const RequestUserPasswordResetEndpoint As String = "request-password-reset"
        Const ResetUserPasswordEndpoint As String = "reset-password"

        ' API public functions

        Public Async Function CheckIaipStatusApiAsync() As Task(Of IaipStatusResult)
            Return IaipStatusResult.ParseStatusResult(Await GetApiAsync($"{ApiUrl.AbsoluteUri}{StatusEndpoint}").ConfigureAwait(False))
        End Function

        Public Async Function ValidateLoginApiAsync(request As LoginCredentials) As Task(Of String)
            Return ParseAuthAndConfig(Await PostApiAsync($"{ApiUrl.AbsoluteUri}{LoginEndpoint}", request).ConfigureAwait(False))
        End Function

        Public Async Function ValidateSessionApiAsync(request As SessionCredentials) As Task(Of String)
            Return ParseAuthAndConfig((Await PostApiAsync($"{ApiUrl.AbsoluteUri}{SessionEndpoint}", request).ConfigureAwait(False)))
        End Function

        Public Async Function RequestUsernameApiAsync(request As UsernameRequest) As Task(Of String)
            Return Await PostApiAsync($"{ApiUrl.AbsoluteUri}{RequestUsernameEndpoint}", request).ConfigureAwait(False)
        End Function

        Public Async Function RequestUserPasswordResetApiAsync(request As PasswordResetRequest) As Task(Of String)
            Return Await PostApiAsync($"{ApiUrl.AbsoluteUri}{RequestUserPasswordResetEndpoint}", request).ConfigureAwait(False)
        End Function

        Public Function ResetUserPasswordApiAsync(request As PasswordReset) As Task(Of String)
            Return PostApiAsync($"{ApiUrl.AbsoluteUri}{ResetUserPasswordEndpoint}", request)
        End Function

        ' Auth/app config parsing

        Private Function ParseAuthAndConfig(result As String) As String
            Dim parsedResult As IaipAuthResult = IaipAuthResult.ParseAuthResult(result)

            If parsedResult.Success Then
                CurrentAppConfig = parsedResult.IaipConfig
                DB = New GaEpd.DBHelper(CurrentConnectionString)
            End If

            Return parsedResult.Message
        End Function

    End Module
End NameSpace
