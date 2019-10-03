Imports System.Configuration

Module DbConnections

    ''' <summary>
    ''' ServerEnvironment can be Production, Staging (UAT), or Development
    ''' </summary>
    Public Enum ServerEnvironment
        Production
        Staging
        Development
        DisasterRecovery
    End Enum

    ''' <summary>
    ''' Returns the database connection string for the current database connection environment
    ''' </summary>
    ''' <returns>A database connection string</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentConnectionString() As String
        Get
            Dim cs As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("AirbranchDB")

            If cs Is Nothing Then
                CloseIaip()
            End If

            Return cs.ConnectionString
        End Get
    End Property

End Module
