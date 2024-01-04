Imports System.Data.SqlClient

Module DbConnections

    ''' <summary>
    ''' ServerEnvironment can be Production, Staging (UAT), or Development
    ''' </summary>
    Public Enum ServerEnvironment
        Production
        Staging
        Development
    End Enum

    ''' <summary>
    ''' Returns the database connection string for the current database connection environment
    ''' </summary>
    ''' <returns>A database connection string</returns>
    Public ReadOnly Property CurrentConnectionString As String
        Get
            If CurrentAppConfig.DatabaseIp = Nothing Then Return Nothing

            Return New SqlConnectionStringBuilder() With {
                .DataSource = $"{CurrentAppConfig.DatabaseIp},{CurrentAppConfig.DatabasePort}",
                .UserID = CurrentAppConfig.DatabaseUser,
                .Password = CurrentAppConfig.DatabasePassword,
                .PersistSecurityInfo = True,
                .InitialCatalog = "airbranch"
            }.ConnectionString
        End Get
    End Property

End Module
