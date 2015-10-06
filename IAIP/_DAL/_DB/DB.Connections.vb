Namespace DB
    Module Connections

        ''' <summary>
        ''' ServerEnvironment can be either PRD (production) or DEV (development)
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum ServerEnvironment
            PRD
            DEV
        End Enum

        ''' <summary>
        ''' Return the default connection environment based on setting of DEBUG compiler flag
        ''' </summary>
        ''' <value>The default connection environment</value>
        ''' <remarks>Does not depend on selected server location or connection environment at any given time; 
        ''' only returns default connection environment.</remarks>
        Public ReadOnly Property DefaultServerEnvironment() As ServerEnvironment
            Get
                Return ServerEnvironment.PRD
            End Get
        End Property

        ''' <summary>
        ''' Returns database connection parameters as DatabaseConnectionParameters for a given ConnectionEnvironment enum
        ''' </summary>
        ''' <param name="env">A ConnectionEnvironment enum designating which connection string is desired</param>
        ''' <returns>Database connection parameters</returns>
        ''' <remarks>Currently built to return Oracle connection parameters</remarks>
        Private Function GetDatabaseConnectionParameters(ByVal env As ServerEnvironment) As DatabaseConnectionParameters
            Select Case env
                Case ServerEnvironment.PRD
                    Return New DatabaseConnectionParameters("167.195.93.68", "1521", "PRD", "AIRBRANCH_APP_USER", SimpleCrypt("çòáðò±ì"))
                Case ServerEnvironment.DEV
                    Return New DatabaseConnectionParameters("167.195.93.100", "1521", "DEV", "AIRBRANCH", SimpleCrypt("óíïçáìåòô"))
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Structure DatabaseConnectionParameters
            Public Sub New(ByVal host As String, ByVal port As String, ByVal sid As String, _
                           ByVal user As String, ByVal pwd As String)
                Me.Host = host
                Me.Port = port
                Me.SID = sid
                Me.User = user
                Me.Password = pwd
            End Sub
            Public Host As String
            Public Port As String
            Public SID As String
            Public User As String
            Public Password As String
        End Structure

        ''' <summary>
        ''' Returns the database connection string for the current database connection environment
        ''' </summary>
        ''' <returns>A database connection string</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentConnectionString() As String
            Get
                Return GetConnectionString(CurrentServerEnvironment)
            End Get
        End Property


        ''' <summary>
        ''' Returns a database connection string based on the provided ConnectionEnvironment enum
        ''' </summary>
        ''' <param name="env">A ConnectionEnvironment enum designating which connection string is desired</param>
        ''' <returns>A database connection string</returns>
        ''' <remarks>Currently built to return an Oracle connection string</remarks>
        Private Function GetConnectionString(ByVal env As ServerEnvironment) As String

            ' Oracle connection method without tnsnames.ora
            Dim oracleConnectionStringTemplate As String = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" & _
                "(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SID={2})));" & _
                "User Id={3}; Password = {4};"

            ' Standard Oracle connection method (requires tnsnames.ora on client)
            'Private oracleConnectionStringTemplate As String = "Data Source = {2}; User ID = {3}; Password = {4};"

            ' Oracle EZ Connect method (maybe requires EZCONNECT enabled in sqlnet.ora file?)
            'Private oracleConnectionStringTemplate As String = "{3}/{4}@//{0}:{1}/{2}"

            Dim dbParams As DatabaseConnectionParameters = GetDatabaseConnectionParameters(env)
            Return String.Format(oracleConnectionStringTemplate, dbParams.Host, dbParams.Port, dbParams.SID, dbParams.User, dbParams.Password)
        End Function

    End Module
End Namespace
