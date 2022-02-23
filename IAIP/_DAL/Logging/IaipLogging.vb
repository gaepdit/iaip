Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Namespace DAL
    Module IaipLogging

        Public Function LogCrystalReportsUsage(report As ReportClass) As Boolean
            If report Is Nothing Then
                Return False
            End If

            AddBreadcrumb($"Crystal Report opened: {report.ResourceName}")

            Dim query As String = "insert into IAIP_CrystaLReportsLog (ReportName, UserId) values (@ReportName, @UserId)"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@UserId", CurrentUser.UserID),
                New SqlParameter("@ReportName", report.ResourceName)
            }

            Try
                Return DB.RunCommand(query, parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Sub LogReportUsage(type As String, path As String)
            ArgumentNotNullOrEmpty(type, NameOf(type))
            ArgumentNotNullOrEmpty(path, NameOf(path))

            AddBreadcrumb($"Report type {type} opened: {path}")

            Dim parameters As SqlParameter() = {
                New SqlParameter("@type", type),
                New SqlParameter("@path", path),
                New SqlParameter("@userId", (CurrentUser?.UserID))
            }

            Try
                DB.SPRunCommand("dbo.LogReport", parameters)
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
                New SqlParameter("@IaipVersion", GetCurrentVersion().ToString)
            }

            Try
                Return DB.SPRunCommand("iaip_user.LogSystemProperties", parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Module
End Namespace
