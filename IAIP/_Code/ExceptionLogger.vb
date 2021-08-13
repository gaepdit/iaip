Imports System.Collections.Generic
Imports System.Configuration
Imports Mindscape.Raygun4Net
Imports Mindscape.Raygun4Net.Messages

Friend Module ExceptionLogger

    Friend Function LogException(
            ex As Exception,
            context As String,
            supplementalMessage As String,
            unrecoverable As Boolean) As Boolean

#If DEBUG Then
        ' Only log if UAT or Prod
        Return False
#End If

        Dim client As New RaygunClient(ConfigurationManager.AppSettings("RAYGUN_API_KEY")) With {
            .ApplicationVersion = GetCurrentVersionAsMajorMinorBuild().ToString
        }

        If CurrentUser IsNot Nothing Then
            client.UserInfo = New RaygunIdentifierMessage(CurrentUser.UserID.ToString) With {
                .Email = CurrentUser.EmailAddress,
                .FirstName = CurrentUser.Username,
                .FullName = CurrentUser.FullName,
                .IsAnonymous = False,
                .UUID = Environment.MachineName
            }
        Else
            client.UserInfo = New RaygunIdentifierMessage("") With {
                .IsAnonymous = True,
                .UUID = Environment.MachineName
            }
        End If

        Dim tags As New List(Of String) From {CurrentServerEnvironment.ToString, context}
        If unrecoverable Then
            tags.Add("Unrecoverable")
        End If

        Dim customData As New Dictionary(Of String, Object) From {
            {"Context", context},
            {"Supplemental message", supplementalMessage},
            {"Initial Network Status", NetworkStatus.GetDescription},
            {"Is VPN", IsVpnConnected()}
        }

        Try
            If unrecoverable Then
                client.Send(ex, tags, customData)
            Else
                client.SendInBackground(ex, tags, customData)
            End If

            Return True
        Catch rex As Exception
            Return False
        End Try
    End Function

    Friend Sub AddBreadcrumb(message As String, Optional sender As Object = Nothing)
        AddBreadcrumb(message, New Dictionary(Of String, Object), sender)
    End Sub

    Friend Sub AddBreadcrumb(message As String, dataDictionary As Dictionary(Of String, Object), Optional sender As Object = Nothing)
        If dataDictionary Is Nothing Then dataDictionary = New Dictionary(Of String, Object)
        If TypeOf sender Is Control Then dataDictionary.Add("Sender", CType(sender, Control).Name)
        RaygunClient.RecordBreadcrumb(New RaygunBreadcrumb With {.Message = message, .CustomData = dataDictionary})
    End Sub

    Friend Sub AddBreadcrumb(message As String, name As String, data As Object, Optional sender As Object = Nothing)
        Dim dataDictionary As New Dictionary(Of String, Object) From {{name, data}}
        If TypeOf sender Is Control Then dataDictionary.Add("Sender", CType(sender, Control).Name)
        RaygunClient.RecordBreadcrumb(New RaygunBreadcrumb With {.Message = message, .CustomData = dataDictionary})
    End Sub

End Module
