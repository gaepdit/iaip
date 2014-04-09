Namespace DB

    Module Connections

        Public Enum ConnectionEnvironment
            Production
            Development
            NADC_Production
            NADC_Development
        End Enum

        ''' <summary>
        ''' Returns database connection parameters as DatabaseConnectionParameters for a given ConnectionEnvironment enum
        ''' </summary>
        ''' <param name="env">A ConnectionEnvironment enum designating which connection string is desired</param>
        ''' <returns>Database connection parameters</returns>
        ''' <remarks>Currently built to return Oracle connection parameters</remarks>
        Private Function GetDatabaseConnectionParameters(ByVal env As ConnectionEnvironment) As DatabaseConnectionParameters
            Select Case env

                Case ConnectionEnvironment.Production
                    Return New DatabaseConnectionParameters("luke.dnr.state.ga.us", "1521", "PRD", "AIRBRANCH_APP_USER", SimpleCrypt("çòáðò±ì"))

                Case ConnectionEnvironment.Development
                    Return New DatabaseConnectionParameters("leia.dnr.state.ga.us", "1521", "DEV", "AIRBRANCH", SimpleCrypt("óíïçáìåòô"))

                Case ConnectionEnvironment.NADC_Production
                    Return New DatabaseConnectionParameters("167.195.93.68", "1521", "PRD", "AIRBRANCH_APP_USER", SimpleCrypt("çòáðò±ì"))

                Case ConnectionEnvironment.NADC_Development
                    Return New DatabaseConnectionParameters("167.195.93.100", "1521", "DEV", "AIRBRANCH", "123")

                Case Else
                    Return Nothing

            End Select
        End Function

        Private Structure DatabaseConnectionParameters
            Public Sub New(ByVal host As String, ByVal port As String, ByVal sid As String, ByVal user As String, ByVal pwd As String)
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
        <DebuggerStepThrough()> _
        Public Function GetCurrentConnectionString() As String
            Return GetConnectionString(CurrentConnectionEnvironment)
        End Function

        ''' <summary>
        ''' Returns a database connection string based on the provided ConnectionEnvironment enum
        ''' </summary>
        ''' <param name="env">A ConnectionEnvironment enum designating which connection string is desired</param>
        ''' <returns>A database connection string</returns>
        ''' <remarks>Currently built to return an Oracle connection string</remarks>
        Public Function GetConnectionString(ByVal env As ConnectionEnvironment) As String

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

