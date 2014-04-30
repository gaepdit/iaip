Namespace DB
    Module Connections

        ''' <summary>
        ''' Date scheduled for legacy servers to be disconnected. 
        ''' </summary>
        ''' <remarks>NADC servers will not necessarily be available yet, but that will be handled by server availability checking.</remarks>
        Public ReadOnly Property NADC_CUTOVER_DATETIME() As DateTime
            Get
                Return New DateTime(2014, 5, 2, 12 + 5, 0, 0) ' 5:00 pm, May 2, 2012
            End Get
        End Property

        ''' <summary>
        ''' Return the default db server location based on scheduled NADC cutover date
        ''' </summary>
        ''' <value>The default server location</value>
        ''' <remarks>Does not depend on selected server location or connection environment at any given time; 
        ''' only returns default location.</remarks>
        Public ReadOnly Property DefaultServerLocation() As ServerLocation
            Get
                If DateTime.Now < NADC_CUTOVER_DATETIME Then
                    Return ServerLocation.Legacy
                Else
                    Return ServerLocation.NADC
                End If
            End Get
        End Property

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

        Public Enum ServerLocation
            Legacy
            NADC
        End Enum

        Public Enum ServerEnvironment
            PRD
            DEV
        End Enum

        ''' <summary>
        ''' Returns database connection parameters as DatabaseConnectionParameters for a given ConnectionEnvironment enum
        ''' </summary>
        ''' <param name="env">A ConnectionEnvironment enum designating which connection string is desired</param>
        ''' <returns>Database connection parameters</returns>
        ''' <remarks>Currently built to return Oracle connection parameters</remarks>
        Private Function GetDatabaseConnectionParameters(ByVal env As ServerEnvironment, ByVal loc As ServerLocation) As DatabaseConnectionParameters
            Select Case loc
                Case ServerLocation.Legacy
                    Select Case env
                        Case ServerEnvironment.PRD
                            Return New DatabaseConnectionParameters("luke.dnr.state.ga.us", "1521", "PRD", "AIRBRANCH_APP_USER", SimpleCrypt("çòáðò±ì"))
                        Case ServerEnvironment.DEV
                            Return New DatabaseConnectionParameters("leia.dnr.state.ga.us", "1521", "DEV", "AIRBRANCH", SimpleCrypt("óíïçáìåòô"))
                        Case Else
                            Return Nothing
                    End Select
                Case ServerLocation.NADC
                    Select Case env
                        Case ServerEnvironment.PRD
                            Return New DatabaseConnectionParameters("167.195.93.68", "1521", "PRD", "AIRBRANCH_APP_USER", SimpleCrypt("çòáðò±ì"))
                        Case ServerEnvironment.DEV
                            Return New DatabaseConnectionParameters("167.195.93.100", "1521", "DEV", "AIRBRANCH", "123")
                        Case Else
                            Return Nothing
                    End Select
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
                Return GetConnectionString(CurrentServerEnvironment, CurrentServerLocation)
            End Get
        End Property


        ''' <summary>
        ''' Returns a database connection string based on the provided ConnectionEnvironment enum
        ''' </summary>
        ''' <param name="env">A ConnectionEnvironment enum designating which connection string is desired</param>
        ''' <returns>A database connection string</returns>
        ''' <remarks>Currently built to return an Oracle connection string</remarks>
        Private Function GetConnectionString(ByVal env As ServerEnvironment, ByVal loc As ServerLocation) As String

            ' Oracle connection method without tnsnames.ora
            Dim oracleConnectionStringTemplate As String = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" & _
                "(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SID={2})));" & _
                "User Id={3}; Password = {4};"

            ' Standard Oracle connection method (requires tnsnames.ora on client)
            'Private oracleConnectionStringTemplate As String = "Data Source = {2}; User ID = {3}; Password = {4};"

            ' Oracle EZ Connect method (maybe requires EZCONNECT enabled in sqlnet.ora file?)
            'Private oracleConnectionStringTemplate As String = "{3}/{4}@//{0}:{1}/{2}"

            Dim dbParams As DatabaseConnectionParameters = GetDatabaseConnectionParameters(env, loc)
            Return String.Format(oracleConnectionStringTemplate, dbParams.Host, dbParams.Port, dbParams.SID, dbParams.User, dbParams.Password)
        End Function

    End Module
End Namespace
