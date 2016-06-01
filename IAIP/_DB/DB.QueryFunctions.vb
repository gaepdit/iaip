Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection

Namespace DB
    Module QueryFunctions

#Region " Ping "

        ''' <summary>
        ''' Attempt to access an OracleConnection to determine if it is available and to keep it open if so.
        ''' </summary>
        ''' <param name="conn">The OracleConnection to access.</param>
        ''' <returns>True if DB connection works. Otherwise, false.</returns>
        ''' <remarks>
        ''' This function has a dual purpose. First, to determine if a DB connection is available 
        ''' (and exit gracefully if it is not). Second, to keep that connection perpetually open. 
        ''' This is useful only because the IAIP uses a single OracleConnection that it assumes 
        ''' to always be open (and fails miserably if it is not). Hence, there is no conn.Close() 
        ''' statement after the cmd.ExecuteScalar() statement.
        ''' 
        ''' One day I will fix all database calls to work correctly and add a conn.Close to this 
        ''' function...
        ''' </remarks>
        Public Function PingDBConnection(ByVal conn As OracleConnection) As Boolean
            Dim query As String = "SELECT 1 FROM DUAL"
            Dim success As Boolean = True
            Dim startTime As Date = Date.UtcNow
            Dim timer As Stopwatch = Stopwatch.StartNew

            Using cmd As New OracleCommand(query, conn)
                Try
                    If conn.State = ConnectionState.Closed Then conn.Open()
                    cmd.ExecuteScalar()
                Catch ex As Exception
                    success = False
                Finally
                    timer.Stop()
                End Try
            End Using

            Return success
        End Function

#End Region

#Region " Read (Scalar) "

        ''' <summary>
        ''' Retrieves a boolean value from the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameter">An optional OracleParameter to send.</param>
        ''' <param name="failSilently">If true, OracleExceptions will be suppressed.</param>
        ''' <returns>A boolean value.</returns>
        Public Function GetBoolean(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing, Optional ByVal failSilently As Boolean = False) As Boolean
            Return Convert.ToBoolean(GetSingleValue(Of Boolean)(query, parameter, failSilently))
        End Function

        ''' <summary>
        ''' Retrieves a boolean value from the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameterArray">An optional OracleParameter array to send.</param>
        ''' <param name="failSilently">If true, OracleExceptions will be suppressed.</param>
        ''' <returns>A boolean value.</returns>
        Public Function GetBoolean(ByVal query As String, ByVal parameterArray As OracleParameter(), Optional ByVal failSilently As Boolean = False) As Boolean
            Return Convert.ToBoolean(GetSingleValue(Of Boolean)(query, parameterArray, failSilently))
        End Function

        ''' <summary>
        ''' Retrieves a single value of the specified type from the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameter">An optional OracleParameter to send.</param>
        ''' <param name="failSilently">If true, OracleExceptions will be suppressed.</param>
        ''' <returns>A value of the specified type.</returns>
        Public Function GetSingleValue(Of T)(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing, Optional ByVal failSilently As Boolean = False) As T
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetSingleValue(Of T)(query, parameterArray, failSilently)
        End Function

        ''' <summary>
        ''' Retrieves a single value of the specified type from the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameterArray">An optional OracleParameter array to send.</param>
        ''' <param name="failSilently">If true, OracleExceptions will be suppressed.</param>
        ''' <returns>A value of the specified type.</returns>
        Public Function GetSingleValue(Of T)(ByVal query As String, ByVal parameterArray As OracleParameter(), Optional ByVal failSilently As Boolean = False) As T
            Dim result As Object = Nothing
            Dim success As Boolean = True
            Dim startTime As Date = Date.UtcNow
            Dim timer As Stopwatch = Stopwatch.StartNew

            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(query, connection)
                    command.CommandType = CommandType.Text
                    command.BindByName = True
                    command.Parameters.AddRange(parameterArray)

                    Try
                        command.Connection.Open()
                        result = command.ExecuteScalar()
                        command.Connection.Close()
                    Catch ee As OracleException
                        success = False
                        ErrorReport(ee, query, Reflection.MethodBase.GetCurrentMethod.Name, Not failSilently)
                    Finally
                        timer.Stop()
                    End Try

                End Using
            End Using

            Return GetNullable(Of T)(result)
        End Function

#End Region

#Region " Value Exists "

        ''' <summary>
        ''' Determines whether a value as indicated by the SQL query exists in the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameterArray">An optional OracleParameter array to send.</param>
        ''' <returns>A boolean value signifying whether the indicated value exists.</returns>
        Public Function ValueExists(query As String, parameterArray As OracleParameter()) As Boolean
            Dim result As Object = Nothing
            Dim success As Boolean = True
            Dim startTime As Date = Date.UtcNow
            Dim timer As Stopwatch = Stopwatch.StartNew

            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(query, connection)
                    command.CommandType = CommandType.Text
                    command.BindByName = True
                    command.Parameters.AddRange(parameterArray)
                    Try
                        command.Connection.Open()
                        result = command.ExecuteScalar()
                        command.Connection.Close()
                    Catch ee As OracleException
                        success = False
                        ErrorReport(ee, query, Reflection.MethodBase.GetCurrentMethod.Name)
                    Finally
                        timer.Stop()
                    End Try

                End Using
            End Using

            Return Not (result Is Nothing OrElse IsDBNull(result) OrElse result.ToString = "null")
        End Function

#End Region

#Region " Read (Lookup Dictionary) "

        ''' <summary>
        ''' Retrieves a dictionary of (integer -> string) values from the database
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameter">An optional OracleParameter to send.</param>
        ''' <returns>A lookup dictionary.</returns>
        Public Function GetLookupDictionary(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) _
        As Dictionary(Of Integer, String)
            Dim d As New Dictionary(Of Integer, String)

            Dim dataTable As DataTable = GetDataTable(query, parameter)

            For Each row As DataRow In dataTable.Rows
                d.Add(row.Item(0), row.Item(1))
            Next

            Return d
        End Function

#End Region

#Region " Read (DataRow) "

        ''' <summary>
        ''' Retrieves a single row of values from the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameter">An optional OracleParameter to send.</param>
        ''' <returns>A DataRow of values.</returns>
        Public Function GetDataRow(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As DataRow
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetDataRow(query, parameterArray)
        End Function

        ''' <summary>
        ''' Retrieves a single row of values from the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameterArray">An optional OracleParameter array to send.</param>
        ''' <returns>A DataRow of values.</returns>
        Public Function GetDataRow(ByVal query As String, ByVal parameterArray As OracleParameter()) As DataRow
            Dim resultTable As DataTable = GetDataTable(query, parameterArray)
            If resultTable IsNot Nothing And resultTable.Rows.Count = 1 Then
                Return resultTable.Rows(0)
            Else
                Return Nothing
            End If
        End Function

#End Region

#Region " Read (DataTable) "

        ''' <summary>
        ''' Retrieves a DataTable of values from the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameter">An optional OracleParameter to send.</param>
        ''' <returns>A DataTable of values.</returns>
        Public Function GetDataTable(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As DataTable
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetDataTable(query, parameterArray)
        End Function

        ''' <summary>
        ''' Retrieves a DataTable of values from the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameterArray">An OracleParameter array to send.</param>
        ''' <returns>A DataTable of values.</returns>
        Public Function GetDataTable(ByVal query As String, ByVal parameterArray As OracleParameter()) As DataTable
            Dim table As New DataTable
            Dim success As Boolean = True
            Dim startTime As Date = Date.UtcNow
            Dim timer As Stopwatch = Stopwatch.StartNew

            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(query, connection)
                    command.CommandType = CommandType.Text
                    command.BindByName = True
                    command.Parameters.AddRange(parameterArray)
                    Using adapter As New OracleDataAdapter(command)

                        Try
                            command.Connection.Open()
                            adapter.Fill(table)
                            command.Connection.Close()
                        Catch ee As OracleException
                            success = False
                            ErrorReport(ee, query, Reflection.MethodBase.GetCurrentMethod.Name)
                            table = Nothing
                        Finally
                            timer.Stop()
                        End Try

                    End Using
                End Using
            End Using

            Return table
        End Function

#End Region

#Region " Read (ByteArray) "

        Public Function SaveBinaryFileFromDB(filePath As String, query As String, Optional ByVal parameter As OracleParameter = Nothing) As Boolean
            Dim parameterArray As OracleParameter() = {parameter}
            Return SaveBinaryFileFromDB(filePath, query, parameterArray)
        End Function

        Public Function SaveBinaryFileFromDB(filePath As String, query As String, ByVal parameterArray As OracleParameter()) As Boolean
            Dim byteArray As Byte() = GetByteArrayFromBlob(query, parameterArray)

            Try
                Using fs As New FileStream(filePath, FileMode.Create, FileAccess.Write)
                    Using bw As New BinaryWriter(fs)
                        bw.Write(byteArray)
                    End Using ' bw
                End Using ' fs

                Return True
            Catch ex As Exception
                ErrorReport(ex, filePath, Reflection.MethodBase.GetCurrentMethod.Name)
                Return False
            End Try
        End Function

        Private Function GetByteArrayFromBlob(ByVal query As String, ByVal parameterArray As OracleParameter()) As Byte()
            Dim success As Boolean = True
            Dim startTime As Date = Date.UtcNow
            Dim timer As Stopwatch = Stopwatch.StartNew

            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(query, connection)
                    command.CommandType = CommandType.Text
                    command.BindByName = True
                    command.Parameters.AddRange(parameterArray)

                    Try
                        command.Connection.Open()
                        Dim dr As OracleDataReader = command.ExecuteReader()
                        dr.Read()

                        Dim length As Integer = dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue)
                        Dim byteArray(length) As Byte
                        dr.GetBytes(0, 0, byteArray, 0, length)

                        dr.Dispose()
                        command.Connection.Close()

                        Return byteArray
                    Catch ee As OracleException
                        success = False
                        ErrorReport(ee, query, Reflection.MethodBase.GetCurrentMethod.Name)
                        Return Nothing
                    Catch ex As Exception
                        success = False
                        ErrorReport(ex, query, Reflection.MethodBase.GetCurrentMethod.Name)
                        Return Nothing
                    Finally
                        timer.Stop()
                    End Try

                End Using
            End Using
        End Function

#End Region

#Region " Run commands (ExecuteNonQuery) "

        ''' <summary>
        ''' Executes a SQL statement on the database.
        ''' </summary>
        ''' <param name="query">The SQL statement to execute.</param>
        ''' <param name="parameter">An optional OracleParameter to send.</param>
        ''' <param name="rowsAffected">For UPDATE, INSERT, and DELETE statements, stores the number of rows affected by the command.</param>
        ''' <param name="failSilently">If true, suppresses error messages displayed to the user.</param>
        ''' <returns>True if command ran successfully. Otherwise, false.</returns>
        Public Function RunCommand(ByVal query As String,
                                   Optional ByVal parameter As OracleParameter = Nothing,
                                   Optional ByRef rowsAffected As Integer = 0,
                                   Optional ByVal failSilently As Boolean = False
                                   ) As Boolean
            rowsAffected = 0
            Dim parameterArray As OracleParameter() = {parameter}
            Return RunCommand(query, parameterArray, rowsAffected, failSilently)
        End Function

        ''' <summary>
        ''' Executes a SQL statement on the database.
        ''' </summary>
        ''' <param name="query">The SQL statement to execute.</param>
        ''' <param name="parameters">An OracleParameter array to send.</param>
        ''' <param name="rowsAffected">For UPDATE, INSERT, and DELETE statements, stores the number of rows affected by the command.</param>
        ''' <param name="failSilently">If true, suppresses error messages displayed to the user.</param>
        ''' <returns>True if command ran successfully. Otherwise, false.</returns>
        Public Function RunCommand(ByVal query As String,
                                   ByVal parameters As OracleParameter(),
                                   Optional ByRef rowsAffected As Integer = 0,
                                   Optional ByVal failSilently As Boolean = False
                                   ) As Boolean
            rowsAffected = 0
            Dim queryList As New List(Of String)
            queryList.Add(query)

            Dim parametersList As New List(Of OracleParameter())
            parametersList.Add(parameters)

            Dim countList As New List(Of Integer)

            Dim result As Boolean = RunCommand(queryList, parametersList, countList, failSilently)

            If result AndAlso countList.Count > 0 Then rowsAffected = countList(0)

            Return result
        End Function

        ''' <summary>
        ''' Executes a set of SQL statements on the database.
        ''' </summary>
        ''' <param name="queryList">The SQL statements to execute.</param>
        ''' <param name="parametersList">A List of OracleParameter arrays to send.</param>
        ''' <param name="countList">A List of rows affected by each SQL statement.</param>
        ''' <param name="failSilently"></param>
        ''' <returns>True if command ran successfully. Otherwise, false.</returns>
        Public Function RunCommand(ByVal queryList As List(Of String),
                                   ByVal parametersList As List(Of OracleParameter()),
                                   Optional ByRef countList As List(Of Integer) = Nothing,
                                   Optional ByVal failSilently As Boolean = False
                                   ) As Boolean
            If countList Is Nothing Then countList = New List(Of Integer)
            countList.Clear()
            If queryList.Count <> parametersList.Count Then Return False
            Dim success As Boolean = True
            Dim startTime As Date = Date.UtcNow
            Dim timer As Stopwatch = Stopwatch.StartNew

            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As OracleCommand = connection.CreateCommand
                    command.CommandType = CommandType.Text
                    command.BindByName = True
                    Dim transaction As OracleTransaction = Nothing

                    Try
                        command.Connection.Open()
                        transaction = connection.BeginTransaction
                        command.Transaction = transaction

                        Try
                            For index As Integer = 0 To queryList.Count - 1
                                command.Parameters.Clear()
                                command.CommandText = queryList(index)
                                command.Parameters.AddRange(parametersList(index))
                                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                                countList.Insert(index, rowsAffected)
                            Next
                            transaction.Commit()
                        Catch ee As OracleException
                            success = False
                            countList.Clear()
                            Try
                                transaction.Rollback()
                            Catch
                            End Try
                            ErrorReport(ee, command.CommandText, Reflection.MethodBase.GetCurrentMethod.Name, Not failSilently)
                        End Try

                        command.Connection.Close()
                    Catch ee As OracleException
                        success = False
                        ErrorReport(ee, "There was an error connecting to the database.", Reflection.MethodBase.GetCurrentMethod.Name, Not failSilently)
                    Finally
                        If transaction IsNot Nothing Then transaction.Dispose()
                        timer.Stop()
                    End Try

                End Using
            End Using

            Return success
        End Function

        Public Function RunCommandIgnoreErrors(query As String, parameters As OracleParameter()) As Boolean
            Try
                Using connection As New OracleConnection(CurrentConnectionString)
                    Using command As New OracleCommand(query, connection)
                        command.CommandType = CommandType.Text
                        command.BindByName = True
                        command.Parameters.AddRange(parameters)
                        command.Connection.Open()
                        command.ExecuteScalar()
                        command.Connection.Close()
                    End Using
                End Using
                Return True
            Catch
                Return False
            End Try
        End Function

#End Region

    End Module
End Namespace