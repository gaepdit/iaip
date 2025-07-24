Imports Microsoft.Data.SqlClient

Namespace DAL
    Module IaipLogging

        Public Sub LogReportUsage(type As String, url As Uri)
            ArgumentNotNullOrEmpty(type, NameOf(type))
            ArgumentNotNull(url, NameOf(url))

            AddBreadcrumb($"Report type {type} opened: {url}")

            Dim parameters As SqlParameter() = {
                New SqlParameter("@type", type),
                New SqlParameter("@path", url.PathAndQuery),
                New SqlParameter("@host", url.Authority),
                New SqlParameter("@userId", (CurrentUser?.UserID))
            }

            Try
                DB.SPRunCommand("dbo.LogIaipReport", parameters)
            Catch ex As Exception
                Return
            End Try
        End Sub

        Public Sub LogFormUsage(formName As String)
            Dim query As String = "insert into dbo.IAIP_FormsLog (FormName, UserId) values (@formName, @userId)"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@formName", formName),
                New SqlParameter("@userId", CurrentUser?.UserID)
            }

            Try
                DB.RunCommand(query, parameters)
            Catch ex As Exception
                Return
            End Try
        End Sub

        Public Function LogSystemProperties(isVpn As Boolean) As Boolean
            Dim parameters As SqlParameter() = {
                New SqlParameter("@UserId", CurrentUser.UserID),
                New SqlParameter("@DotNetVersion", Get45PlusFromRegistry()),
                New SqlParameter("@OSVersion", OSFriendlyName()),
                New SqlParameter("@NetworkStatus", If(isVpn, "On VPN", "In Network")),
                New SqlParameter("@IaipVersion", GetCurrentVersion().ToString),
                New SqlParameter("@Comment", If(RetryProviderEnabled, "DB Conn Retry enabled", "DB Conn Retry disabled"))
            }

            Try
                Return DB.SPRunCommand("iaip_user.LogSystemProperties", parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Module
End Namespace
