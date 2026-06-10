Imports System.Collections.Generic
Imports System.Configuration
Imports Iaip
Imports Sentry

Friend Module ExceptionLogger
    Private ReadOnly SentryDsn As String = ConfigurationManager.AppSettings("SentryDsn")

    Friend Function LogException(ex As Exception,
                                 context As String,
                                 supplementalMessage As String,
                                 unrecoverable As Boolean) As Boolean

        If CurrentServerEnvironment = ServerEnvironment.Development Then
            ' Only log if UAT or Prod
            Return False
        End If

        If String.IsNullOrEmpty(SentryDsn) Then Return False

        SentrySdk.ConfigureScope(
            Sub(scope)
                scope.Contexts("Context Info") = New With {context, supplementalMessage}
                scope.User = IIf(CurrentUser Is Nothing, New SentryUser(),
                                 New SentryUser With {.Email = CurrentUser.EmailAddress, .Id = CurrentUser.UserID})
                scope.SetTag("Unrecoverable", unrecoverable)
                scope.SetTag("NetworkStatus", NetworkStatus.GetDescription())
                scope.SetTag("VpnInterfaceAdapter", VpnInterfaceAdapter)
                scope.SetTag("ServerEnvironment", CurrentServerEnvironment.GetDescription())
            End Sub)

        Try
            SentrySdk.CaptureException(ex)
            Return True
        Catch rex As Exception
            Return False
        End Try

    End Function

    Friend Sub AddBreadcrumb(message As String, category As String)
        SentrySdk.AddBreadcrumb(message, category)
    End Sub

    Friend Sub AddBreadcrumb(message As String, sender As Object)
        SentrySdk.AddBreadcrumb(message, ParseSender(sender))
    End Sub

    Friend Sub AddBreadcrumb(message As String, dataDictionary As Dictionary(Of String, String), sender As Object)
        SentrySdk.AddBreadcrumb(message, ParseSender(sender), , dataDictionary)
    End Sub

    Friend Sub AddBreadcrumb(message As String, name As String, value As String, sender As Object)
        AddBreadcrumb(message, New Dictionary(Of String, String) From {{name, value}}, sender)
    End Sub

    Private Function ParseSender(sender As Object) As String
        If TypeOf sender Is Control Then Return CType(sender, Control).Name
        Return sender.ToString
    End Function

End Module
