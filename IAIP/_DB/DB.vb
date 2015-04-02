Imports Oracle.DataAccess.Client
Imports System.Collections.Generic

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

#End Region

#Region " Read (Lookup Dictionary) "

        Public Function GetLookupDictionary(ByVal query As String) _
        As Dictionary(Of Integer, String)
            Dim d As New Dictionary(Of Integer, String)

            Dim dataTable As DataTable = DB.GetDataTable(query)

            For Each row As DataRow In dataTable.Rows
                d.Add(row.Item(0), row(1))
            Next

            Return d
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

        Public Function GetByteArrayFromBlob(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As Byte()
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetByteArrayFromBlob(query, parameterArray)
        End Function

        Public Function GetByteArrayFromBlob(ByVal query As String, ByVal parameterArray As OracleParameter()) As Byte()
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

#Region " Stored Procedures "

#Region " SP Read (Scalar) "

        Public Function SPGetBoolean(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing, Optional ByVal failSilently As Boolean = False) As Boolean
            Return Convert.ToBoolean(SPGetSingleValue(Of Boolean)(spName, parameter, failSilently))
        End Function

        Public Function SPGetBoolean(ByVal spName As String, ByVal parameterArray As OracleParameter(), Optional ByVal failSilently As Boolean = False) As Boolean
            Return Convert.ToBoolean(SPGetSingleValue(Of Boolean)(spName, parameterArray, failSilently))
        End Function

        Public Function SPGetSingleValue(Of T)(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing, Optional ByVal failSilently As Boolean = False) As T
            Dim parameterArray As OracleParameter() = {parameter}
            Return SPGetSingleValue(Of T)(spName, parameterArray, failSilently)
        End Function

        Public Function SPGetSingleValue(Of T)(ByVal spName As String, ByVal parameterArray As OracleParameter(), Optional ByVal failSilently As Boolean = False) As T
            Dim result As Object = Nothing
            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(spName, connection)
                    command.CommandType = CommandType.StoredProcedure
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

#Region " SP Read (DataTable) "

        Public Function SPGetDataTable(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As DataTable
            Dim parameterArray As OracleParameter() = {parameter}
            Return SPGetDataTable(spName, parameterArray)
        End Function

        Public Function SPGetDataTable(ByVal spName As String, ByVal parameterArray As OracleParameter()) As DataTable
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

#Region " SP Write (ExecuteNonQuery) "

        Public Function SPRunCommand(ByVal spName As String, _
                                     Optional ByVal parameter As OracleParameter = Nothing, _
                                     Optional ByRef rowsAffected As Integer = 0 _
                                     ) As Boolean
            rowsAffected = 0
            Dim parameterArray As OracleParameter() = {parameter}
            Return SPRunCommand(spName, parameterArray, rowsAffected)
        End Function

        Public Function SPRunCommand(ByVal spName As String, _
                                     ByVal parameterArray As OracleParameter(), _
                                     Optional ByRef rowsAffected As Integer = 0 _
                                     ) As Boolean
            rowsAffected = 0
            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(spName, connection)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.AddRange(parameterArray)
                    Try
                        command.Connection.Open()
                        rowsAffected = command.ExecuteNonQuery()
                        command.Connection.Close()
                        Return True
                    Catch ee As OracleException
                        MessageBox.Show("Database error: " & ee.ToString)
                        Return False
                    End Try
                End Using
            End Using
        End Function

#End Region

#End Region

    End Module
End Namespace