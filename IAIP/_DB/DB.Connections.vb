Imports System.Configuration

Namespace DB
    Module Connections

        ''' <summary>
        ''' ServerEnvironment can be PRD (production), UAT (testing), or DEV (development)
        ''' </summary>
        ''' <remarks>Until UAT database instance is created, DEV and UAT will connect to DEV database</remarks>
        Public Enum ServerEnvironment
            PRD
            UAT
            DEV
        End Enum

        ''' <summary>
        ''' Returns the database connection string for the current database connection environment
        ''' </summary>
        ''' <returns>A database connection string</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentConnectionString() As String
            Get
                Dim cs As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("Iaip.My.MySettings." & CurrentServerEnvironment.ToString)
                If Not cs Is Nothing Then
                    Return cs.ConnectionString
                Else
                    Return Nothing
                End If
            End Get
        End Property

    End Module
End Namespace
