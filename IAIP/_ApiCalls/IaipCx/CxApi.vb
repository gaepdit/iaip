Imports System.Configuration
Imports System.Threading.Tasks
Imports Iaip.ApiCalls.ApiUtils

Namespace ApiCalls.IaipCx

    Module CxApi

        Private ReadOnly ApiUrl As Uri = UriCombine(ConfigurationManager.AppSettings("CxApiUrl"), $"api/{CurrentServerEnvironment}")

        Private ReadOnly StatusEndpoint As Uri = UriCombine(ApiUrl.AbsoluteUri, "status")
        Private ReadOnly LoginEndpoint As Uri = UriCombine(ApiUrl.AbsoluteUri, "login")
        Private ReadOnly SessionEndpoint As Uri = UriCombine(ApiUrl.AbsoluteUri, "session")
        Private ReadOnly RequestUsernameEndpoint As Uri = UriCombine(ApiUrl.AbsoluteUri, "request-username")
        Private ReadOnly RequestUserPasswordResetEndpoint As Uri = UriCombine(ApiUrl.AbsoluteUri, "request-password-reset")
        Private ReadOnly ResetUserPasswordEndpoint As Uri = UriCombine(ApiUrl.AbsoluteUri, "reset-password")

        ' API public functions

        Public Async Function CheckIaipStatusApiAsync() As Task(Of IaipStatusResult)
            Return IaipStatusResult.ParseStatusResult(Await GetApiAsync(StatusEndpoint).ConfigureAwait(False))
        End Function

        Public Async Function ValidateLoginApiAsync(request As LoginCredentials) As Task(Of String)
            Return ParseAuthAndConfig(Await PostApiAsync(LoginEndpoint, request).ConfigureAwait(False))
        End Function

        Public Async Function ValidateSessionApiAsync(request As SessionCredentials) As Task(Of String)
            Return ParseAuthAndConfig((Await PostApiAsync(SessionEndpoint, request).ConfigureAwait(False)))
        End Function

        Public Async Function RequestUsernameApiAsync(request As UsernameRequest) As Task(Of String)
            Return Await PostApiAsync(RequestUsernameEndpoint, request).ConfigureAwait(False)
        End Function

        Public Async Function RequestUserPasswordResetApiAsync(request As PasswordResetRequest) As Task(Of String)
            Return Await PostApiAsync(RequestUserPasswordResetEndpoint, request).ConfigureAwait(False)
        End Function

        Public Function ResetUserPasswordApiAsync(request As PasswordReset) As Task(Of String)
            Return PostApiAsync(ResetUserPasswordEndpoint, request)
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
End Namespace
