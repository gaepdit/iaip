Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic
Imports System.IO

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
            Dim sql As String = "SELECT 1 FROM DUAL"
            Using cmd As New OracleCommand(sql, conn)
                Try
                    If conn.State = ConnectionState.Closed Then conn.Open()
                    cmd.ExecuteScalar()
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Using
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
                        If Not failSilently Then
                            MessageBox.Show("Database error: " & ee.ToString)
                        End If
                    End Try

                    Return GetNullable(Of T)(result)
                End Using
            End Using
        End Function

#End Region

#Region " Value Exists "

        ''' <summary>
        ''' Determines whether a value as indicated by the SQL query exists in the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameter">An optional OracleParameter to send.</param>
        ''' <returns>A boolean value signifying whether the indicated value exists.</returns>
        Public Function ValueExists(query As String, Optional parameter As OracleParameter = Nothing) As Boolean
            Dim parameterArray As OracleParameter() = {parameter}
            Return ValueExists(query, parameterArray)
        End Function

        ''' <summary>
        ''' Determines whether a value as indicated by the SQL query exists in the database.
        ''' </summary>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameterArray">An optional OracleParameter array to send.</param>
        ''' <returns>A boolean value signifying whether the indicated value exists.</returns>
        Public Function ValueExists(query As String, parameterArray As OracleParameter()) As Boolean
            Dim result As Object = Nothing
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
                        MessageBox.Show("Database error: " & ee.ToString)
                    End Try

                    Return Not (result Is Nothing OrElse IsDBNull(result) OrElse result.ToString = "null")
                End Using
            End Using
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
        ''' <param name="parameterArray">An optional OracleParameter array to send.</param>
        ''' <returns>A DataTable of values.</returns>
        Public Function GetDataTable(ByVal query As String, ByVal parameterArray As OracleParameter()) As DataTable
            Dim table As New DataTable
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
                            Return table
                        Catch ee As OracleException
                            ErrorReport(ee, System.Reflection.MethodBase.GetCurrentMethod.Name)
                            Return Nothing
                        End Try
                    End Using
                End Using
            End Using
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
                        bw.Close()
                    End Using ' bw
                    fs.Close()
                End Using ' fs

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Function GetByteArrayFromBlob(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As Byte()
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetByteArrayFromBlob(query, parameterArray)
        End Function

        Private Function GetByteArrayFromBlob(ByVal query As String, ByVal parameterArray As OracleParameter()) As Byte()
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

                        dr.Close()
                        dr.Dispose()
                        command.Connection.Close()

                        Return byteArray
                    Catch ee As OracleException
                        MessageBox.Show("Database error: " & ee.ToString)
                        Return Nothing
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.ToString)
                        Return Nothing
                    End Try

                End Using
            End Using
        End Function

#End Region

#Region " Read (Lists) "

        ''' <summary>
        ''' Returns a list of values from the database
        ''' </summary>
        ''' <typeparam name="T">The list item type to return</typeparam>
        ''' <param name="query">The SQL query to send.</param>
        ''' <param name="parameter">A single Oracle Parameter to pass in</param>
        ''' <returns>List of items of the specified type</returns>
        Public Function GetList(Of T)(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As List(Of T)
            Dim l As New List(Of T)
            Dim dt As DataTable = GetDataTable(query, parameter)

            For Each r As DataRow In dt.Rows
                l.Add(GetNullable(Of T)(r.Item(0)))
            Next

            Return l
        End Function

#End Region

#Region " Write (ExecuteNonQuery) "

        Public Function RunCommand(ByVal query As String,
                                   Optional ByVal parameter As OracleParameter = Nothing,
                                   Optional ByRef rowsAffected As Integer = 0,
                                   Optional ByVal failSilently As Boolean = False
                                   ) As Boolean
            rowsAffected = 0
            Dim parameterArray As OracleParameter() = {parameter}
            Return RunCommand(query, parameterArray, rowsAffected, failSilently)
        End Function

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

        Public Function RunCommand(ByVal queryList As List(Of String),
                                   ByVal parametersList As List(Of OracleParameter()),
                                   Optional ByRef countList As List(Of Integer) = Nothing,
                                   Optional ByVal failSilently As Boolean = False
                                   ) As Boolean
            If countList Is Nothing Then countList = New List(Of Integer)
            countList.Clear()
            If queryList.Count <> parametersList.Count Then Return False

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
                            Return True
                        Catch ee As OracleException
                            countList.Clear()
                            transaction.Rollback()
                            If Not failSilently Then
                                MessageBox.Show("There was an error updating the database.")
                            End If
                            Return False
                        End Try

                        command.Connection.Close()
                    Catch ee As OracleException
                        If Not failSilently Then
                            MessageBox.Show("There was an error connecting to the database.")
                        End If
                        Return False
                    Finally
                        If transaction IsNot Nothing Then transaction.Dispose()
                    End Try

                End Using
            End Using
        End Function

#End Region

    End Module
End Namespace