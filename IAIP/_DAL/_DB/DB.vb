Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic
Imports System.IO

Namespace DB
    Module DB

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

        Public Function GetBoolean(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing, Optional ByVal failSilently As Boolean = False) As Boolean
            Return Convert.ToBoolean(GetSingleValue(Of Boolean)(query, parameter, failSilently))
        End Function

        Public Function GetBoolean(ByVal query As String, ByVal parameterArray As OracleParameter(), Optional ByVal failSilently As Boolean = False) As Boolean
            Return Convert.ToBoolean(GetSingleValue(Of Boolean)(query, parameterArray, failSilently))
        End Function

        Public Function GetSingleValue(Of T)(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing, Optional ByVal failSilently As Boolean = False) As T
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetSingleValue(Of T)(query, parameterArray, failSilently)
        End Function

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

        Public Function ValueExists(query As String, Optional parameter As OracleParameter = Nothing) As Boolean
            Dim parameterArray As OracleParameter() = {parameter}
            Return ValueExists(query, parameterArray)
        End Function

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

        Public Function GetLookupDictionary(ByVal query As String) _
        As Dictionary(Of Integer, String)
            Dim d As New Dictionary(Of Integer, String)

            Dim dataTable As DataTable = DB.GetDataTable(query)

            For Each row As DataRow In dataTable.Rows
                d.Add(row.Item(0), row.Item(1))
            Next

            Return d
        End Function

#End Region

#Region " Read (DataRow) "

        Public Function GetDataRow(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As DataRow
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetDataRow(query, parameterArray)
        End Function

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

        Public Function GetDataTable(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As DataTable
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetDataTable(query, parameterArray)
        End Function

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
            Dim byteArray As Byte() = DB.GetByteArrayFromBlob(query, parameterArray)

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

#Region " Write (ExecuteNonQuery) "

        Public Function RunCommand(ByVal query As String, _
                                   Optional ByVal parameter As OracleParameter = Nothing, _
                                   Optional ByRef rowsAffected As Integer = 0, _
                                   Optional ByVal failSilently As Boolean = False _
                                   ) As Boolean
            rowsAffected = 0
            Dim parameterArray As OracleParameter() = {parameter}
            Return RunCommand(query, parameterArray, rowsAffected, failSilently)
        End Function

        Public Function RunCommand(ByVal query As String, _
                                   ByVal parameters As OracleParameter(), _
                                   Optional ByRef rowsAffected As Integer = 0, _
                                   Optional ByVal failSilently As Boolean = False _
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

        Public Function RunCommand(ByVal queryList As List(Of String), _
                                   ByVal parametersList As List(Of OracleParameter()), _
                                   Optional ByRef countList As List(Of Integer) = Nothing, _
                                   Optional ByVal failSilently As Boolean = False _
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

#Region " SP (Specific ReturnValue) "
        ' Oracle Stored Procedures that return a single value (must specify value type and size)
        ' Currently not used --- SYS_REFCURSOR return value was easier to work with (see below)

        'Public Function SPGetBooleanReturnValue(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As Boolean
        '    Dim parameterArray As OracleParameter() = {parameter}
        '    Return SPGetBooleanReturnValue(spName, parameterArray)
        'End Function

        'Public Function SPGetBooleanReturnValue(ByVal spName As String, ByVal parameterArray As OracleParameter()) As Boolean
        '    AddReturnValueParameter(parameterArray, 5)
        '    Return SPGetReturnValue(Of Boolean)(spName, parameterArray)
        'End Function

        'Public Function SPGetStringReturnValue(ByVal spName As String, ByVal size As Integer, Optional ByVal parameter As OracleParameter = Nothing) As String
        '    Dim parameterArray As OracleParameter() = {parameter}
        '    Return SPGetStringReturnValue(spName, size, parameterArray)
        'End Function

        'Public Function SPGetStringReturnValue(ByVal spName As String, ByVal size As Integer, ByVal parameterArray As OracleParameter()) As String
        '    AddReturnValueParameter(parameterArray, size)
        '    Return SPGetReturnValue(Of String)(spName, parameterArray)
        'End Function

        'Public Function SPGetReturnValue(Of T)(ByVal spName As String, ByVal parameterArray As OracleParameter()) As T
        '    Using connection As New OracleConnection(CurrentConnectionString)
        '        Using command As New OracleCommand(spName, connection)
        '            command.CommandType = CommandType.StoredProcedure
        '            command.BindByName = True
        '            command.Parameters.AddRange(parameterArray)
        '            Try
        '                command.Connection.Open()
        '                command.ExecuteNonQuery()
        '                command.Connection.Close()
        '            Catch ee As OracleException
        '                MessageBox.Show("Database error: " & ee.ToString)
        '            End Try

        '            Return GetNullable(Of T)(command.Parameters("ReturnValue").Value.ToString)
        '        End Using
        '    End Using
        'End Function

        'Private Sub AddReturnValueParameter(ByRef parameterArray As OracleParameter(), ByVal size As Integer)
        '    Dim pReturnValue As New OracleParameter("ReturnValue", OracleDbType.Varchar2, size)
        '    pReturnValue.Direction = ParameterDirection.ReturnValue

        '    If parameterArray Is Nothing Then
        '        Array.Resize(parameterArray, 1)
        '    ElseIf parameterArray(0) IsNot Nothing Then
        '        Array.Resize(parameterArray, parameterArray.Length + 1)
        '    End If

        '    parameterArray(parameterArray.GetUpperBound(0)) = pReturnValue
        'End Sub

#End Region

#Region " SP (SYS_REFCURSOR ReturnValue) "
        ' These functions call Oracle Functions that return an Oracle SYS_REFCURSOR.

#Region " Single Value "

        Public Function SPGetBoolean(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As Boolean
            Dim parameterArray As OracleParameter() = {parameter}
            Return SPGetBoolean(spName, parameterArray)
        End Function

        Public Function SPGetBoolean(ByVal spName As String, ByVal parameterArray As OracleParameter()) As Boolean
            Return SPGetSingleValue(Of Boolean)(spName, parameterArray)
        End Function

        '' Not currently used, but may be useful in the future
        'Public Function SPGetSingleValue(Of T)(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As T
        '    Dim parameterArray As OracleParameter() = {parameter}
        '    Return SPGetSingleValue(Of T)(spName, parameterArray)
        'End Function

        Public Function SPGetSingleValue(Of T)(ByVal spName As String, ByVal parameterArray As OracleParameter()) As T
            Dim table As DataTable = SPGetDataTable(spName, parameterArray)
            If table IsNot Nothing AndAlso table.Rows.Count = 1 Then
                Return GetNullable(Of T)(table.Rows(0)(0))
            Else
                Return Nothing
            End If
        End Function

#End Region

#Region " DataRow "

        Public Function SPGetDataRow(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As DataRow
            Dim parameterArray As OracleParameter() = {parameter}
            Return SPGetDataRow(spName, parameterArray)
        End Function

        Public Function SPGetDataRow(ByVal spName As String, ByVal parameterArray As OracleParameter()) As DataRow
            Dim resultTable As DataTable = SPGetDataTable(spName, parameterArray)
            If resultTable IsNot Nothing And resultTable.Rows.Count = 1 Then
                Return resultTable.Rows(0)
            Else
                Return Nothing
            End If
        End Function

#End Region

#Region " DataTable "

        Public Function SPGetDataTable(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As DataTable
            Dim parameterArray As OracleParameter() = {parameter}
            Return SPGetDataTable(spName, parameterArray)
        End Function

        Public Function SPGetDataTable(ByVal spName As String, ByVal parameterArray As OracleParameter()) As DataTable
            If String.IsNullOrEmpty(spName) Then
                Return Nothing
            End If

            AddRefCursorParameter(parameterArray)

            Dim table As New DataTable

            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(spName, connection)
                    command.CommandType = CommandType.StoredProcedure
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

#Region " Lists, Keys & Values "

        ''' <summary>
        ''' Calls an Oracle Stored Procedure and returns a List of KeyValuePairs with Integer keys and 
        ''' String values. Useful for creating DropDownList ComboBoxes.
        ''' </summary>
        ''' <param name="spName">The Oracle Stored Procedure to call</param>
        ''' <param name="parameter">A single Oracle Parameter to pass in</param>
        ''' <returns>List of Integer keys and String value pairs</returns>
        ''' <remarks>Use List returned with ComboBox.BindToKeyValuePairs</remarks>
        Public Function SPGetListOfKeyValuePair(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) _
        As List(Of KeyValuePair(Of Integer, String))
            Dim l As New List(Of KeyValuePair(Of Integer, String))
            Dim dt As DataTable = DB.SPGetDataTable(spName, parameter)

            For Each r As DataRow In dt.Rows
                l.Add(New KeyValuePair(Of Integer, String)(r.Item(0), GetNullable(Of String)(r.Item(1))))
            Next

            Return l
        End Function

        Public Function SPGetList(Of T)(ByVal spname As String, Optional ByVal parameter As OracleParameter = Nothing) As List(Of T)
            Dim l As New List(Of T)
            Dim dt As DataTable = DB.SPGetDataTable(spname, parameter)

            For Each r As DataRow In dt.Rows
                l.Add(GetNullable(Of T)(r.Item(0)))
            Next

            Return l
        End Function

        '''' <summary>
        '''' Calls an Oracle Stored Procedure and returns a Dictionary of keys and values
        '''' </summary>
        '''' <typeparam name="TKey">The type of the keys in the dictionary</typeparam>
        '''' <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        '''' <param name="spName">The Oracle Stored Procedure to call</param>
        '''' <param name="parameter">A single Oracle Parameter to pass in</param>
        '''' <returns>Dictionary of keys and values</returns>
        '''' <remarks>Presumes that the stored procedure returns a table with two columns -- 
        '''' keys and values -- and furthermore that the keys are never null.</remarks>
        'Public Function SPGetDictionary(Of TKey, TValue)(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As Dictionary(Of TKey, TValue)
        '    Dim d As New Dictionary(Of TKey, TValue)
        '    Dim t As DataTable = DB.SPGetDataTable(spName, parameter)

        '    For Each r As DataRow In t.Rows
        '        d.Add(r.Item(0), GetNullable(Of TValue)(r.Item(1)))
        '    Next

        '    Return d
        'End Function

#End Region

#Region " SYS_REFCURSOR Utility "

        Private Sub AddRefCursorParameter(ByRef parameterArray As OracleParameter())
            Dim pRefCursor As New OracleParameter
            pRefCursor.Direction = ParameterDirection.ReturnValue
            pRefCursor.OracleDbType = OracleDbType.RefCursor

            If parameterArray Is Nothing Then
                Array.Resize(parameterArray, 1)
            ElseIf parameterArray(0) IsNot Nothing Then
                Array.Resize(parameterArray, parameterArray.Length + 1)
            End If

            parameterArray(parameterArray.GetUpperBound(0)) = pRefCursor
        End Sub

#End Region

#End Region

#Region " SP (In/Out Parameters) "
        ' These functions call Oracle Stored Procedures using IN and/or OUT parameters.
        ' If successful, the OUT parameters are available to the calling procedure as 
        ' returned by the Oracle database.

        Public Function SPRunCommand(ByVal spName As String, Optional ByRef parameter As OracleParameter = Nothing) As Boolean
            Dim parameterArray As OracleParameter() = {parameter}
            Dim result As Boolean = SPRunCommand(spName, parameterArray)
            If result Then
                parameter = parameterArray(0)
            End If
            Return result
        End Function

        Public Function SPRunCommand(ByVal spName As String, ByRef parameterArray As OracleParameter()) As Boolean
            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(spName, connection)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.AddRange(parameterArray)
                    Try
                        command.Connection.Open()
                        command.ExecuteNonQuery()
                        command.Connection.Close()
                        command.Parameters.CopyTo(parameterArray, 0)
                        Return True
                    Catch ee As OracleException
                        MessageBox.Show("Database error: " & ee.ToString)
                        Return False
                    End Try
                End Using
            End Using
        End Function

#End Region

    End Module
End Namespace