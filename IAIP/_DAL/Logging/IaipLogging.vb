Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Namespace DAL
    Module IaipLogging

        Public Function LogCrystalReportsUsage(report As ReportClass) As Boolean
            If report Is Nothing Then
                Return False
            End If

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

        Public Function LogSystemProperties() As Boolean
            Try
                Dim query As String = "insert into IAIP_SystemLog (DotNetVersion, OSVersion, UserId) values (@DotNetVersion, @OSVersion, @UserId)"

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@UserId", CurrentUser.UserID),
                    New SqlParameter("@DotNetVersion", Get45PlusFromRegistry()),
                    New SqlParameter("@OSVersion", OSFriendlyName())
                }

                Return DB.RunCommand(query, parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Module
End Namespace
