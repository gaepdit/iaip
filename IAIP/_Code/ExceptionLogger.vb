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
        ' Only log if UAT or Prod
#If DEBUG Then
        Return False
#End If

        Dim raygunClient As New RaygunClient(ConfigurationManager.AppSettings("RAYGUN_API_KEY")) With {
            .ApplicationVersion = GetCurrentVersionAsMajorMinorBuild().ToString
        }

        If CurrentUser IsNot Nothing Then
            raygunClient.UserInfo = New RaygunIdentifierMessage(CurrentUser.UserID.ToString) With {
                .Email = CurrentUser.EmailAddress,
                .FirstName = CurrentUser.Username,
                .FullName = CurrentUser.FullName,
                .IsAnonymous = False,
                .UUID = Environment.MachineName
            }
        Else
            raygunClient.UserInfo = New RaygunIdentifierMessage("") With {
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
                raygunClient.Send(ex, tags, customData)
            Else
                raygunClient.SendInBackground(ex, tags, customData)
            End If

            Return True
        Catch rex As Exception
            Return False
        End Try
    End Function

End Module
