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

        Public Function LogSystemProperties(networkStatus As NetworkCheckResponse) As Boolean
            Try
                Dim query As String = "insert into IAIP_SystemLog (UserId, DotNetVersion, OSVersion, NetworkStatus) 
                    values (@UserId, @DotNetVersion, @OSVersion, @NetworkStatus)"

                Dim status As String = "Error"

                Select Case networkStatus
                    Case NetworkCheckResponse.InNetwork
                        status = "In Network"
                    Case NetworkCheckResponse.OnVpn
                        status = "On VPN"
                End Select

                Dim parameters As SqlParameter() = {
                    New SqlParameter("@UserId", CurrentUser.UserID),
                    New SqlParameter("@DotNetVersion", Get45PlusFromRegistry()),
                    New SqlParameter("@OSVersion", OSFriendlyName()),
                    New SqlParameter("@NetworkStatus", status)
                }

                Return DB.RunCommand(query, parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Module
End Namespace
