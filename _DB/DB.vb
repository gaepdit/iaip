Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Collections.Generic

Namespace DB
    Module DB

#Region "Read (Scalar)"

        Public Function GetSingleValue(Of T)(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As T
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetSingleValue(Of T)(query, parameterArray)
        End Function

        Public Function GetSingleValue(Of T)(ByVal query As String, ByVal parameterArray As OracleParameter()) As T
            Dim result As Object = Nothing
            Using connection As New OracleConnection(CurrentConnString)
                Using command As New OracleCommand(query, connection)
                    command.CommandType = CommandType.Text
                    command.Parameters.AddRange(parameterArray)
                    Try
                        command.Connection.Open()
                        result = command.ExecuteScalar()
                        command.Connection.Close()
                    Catch ee As OracleException
                        MessageBox.Show("Database error: " & ee.ToString)
                    End Try

                    If result Is Nothing OrElse IsDBNull(result) Then
                        ' returns the default value for the type
                        Return CType(Nothing, T)
                    Else
                        Return CType(result, T)
                    End If
                End Using
            End Using
        End Function

#End Region

#Region "Read (DataTable)"

        Public Function GetDataTable(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As DataTable
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetDataTable(query, parameterArray)
        End Function

        Public Function GetDataTable(ByVal query As String, ByVal parameterArray As OracleParameter()) As DataTable
            Dim table As New DataTable
            Using connection As New OracleConnection(CurrentConnString)
                Using command As New OracleCommand(query, connection)
                    command.CommandType = CommandType.Text
                    command.Parameters.AddRange(parameterArray)
                    Using adapter As New OracleDataAdapter(command)
                        Try
                            connection.Open()
                            adapter.Fill(table)

                            Return table
                        Catch ee As OracleException
                            MessageBox.Show("Database error: " & ee.ToString)
                            Return Nothing
                        End Try
                    End Using
                End Using
            End Using
        End Function

#End Region

#Region "Read (ByteArray)"

        Public Function GetByteArrayFromBlob(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As Byte()
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetByteArrayFromBlob(query, parameterArray)
        End Function

        Public Function GetByteArrayFromBlob(ByVal query As String, ByVal parameterArray As OracleParameter()) As Byte()
            Using connection As New OracleConnection(CurrentConnString)
                Using command As New OracleCommand(query, connection)

                    command.CommandType = CommandType.Text
                    command.Parameters.AddRange(parameterArray)

                    Try
                        Using dr As OracleDataReader = command.ExecuteReader()
                            dr.Read()
                            Dim blob As OracleBlob = dr.GetOracleBlob(0)
                            Dim byteArray(blob.Length) As Byte
                            blob.Read(byteArray, 0, blob.Length)

                            blob.Dispose()
                            Return byteArray
                        End Using
                    Catch ee As OracleException
                        MessageBox.Show("Database error: " & ee.ToString)
                        Return Nothing
                    End Try

                End Using
            End Using
        End Function

#End Region

#Region "Write (ExecuteNonQuery)"

        Public Function RunCommand(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing, Optional ByRef count As Integer = 0) As Boolean
            Dim parameterArray As OracleParameter() = {parameter}
            Return RunCommand(query, parameterArray, count)
        End Function

        Public Function RunCommand(ByVal query As String, ByVal parameters As OracleParameter(), Optional ByRef count As Integer = 0) As Boolean
            Dim queryList As New List(Of String)
            queryList.Add(query)

            Dim parametersList As New List(Of OracleParameter())
            parametersList.Add(parameters)

            Dim countList As New List(Of Integer)

            Dim result As Boolean = RunCommandList(queryList, parametersList, countList)

            If result AndAlso countList.Count > 0 Then count = countList(0)

            Return result
        End Function

        Public Function RunCommandList(ByVal queryList As List(Of String), ByVal parametersList As List(Of OracleParameter()), Optional ByRef countList As List(Of Integer) = Nothing) As Boolean
            If countList Is Nothing Then countList = New List(Of Integer)
            countList.Clear()
            If queryList.Count <> parametersList.Count Then Return False

            Using connection As New OracleConnection(CurrentConnString)
                Using command As OracleCommand = connection.CreateCommand
                    command.CommandType = CommandType.Text
                    Dim transaction As OracleTransaction = Nothing

                    Try
                        connection.Open()
                        transaction = connection.BeginTransaction
                        command.Transaction = transaction
                        Try
                            For index As Integer = 0 To queryList.Count - 1
                                command.Parameters.Clear()
                                command.CommandText = queryList(index)
                                command.Parameters.AddRange(parametersList(index))
                                Dim result As Object = command.ExecuteNonQuery()
                                countList.Insert(index, CInt(result))
                            Next
                            transaction.Commit()
                            Return True
                        Catch ee As OracleException
                            countList.Clear()
                            transaction.Rollback()
                            MessageBox.Show("There was an error updating the database.")
                            Return False
                        End Try

                    Catch ee As OracleException
                        MessageBox.Show("There was an error connecting to the database.")
                        Return False
                    Finally
                        If transaction IsNot Nothing Then transaction.Dispose()
                    End Try

                End Using
            End Using
        End Function

#End Region

#Region "Utilities"

        Public Function GetNullable(Of T)(ByVal obj As Object) As T
            ' http://stackoverflow.com/a/870771/212978
            ' http://stackoverflow.com/a/9953399/212978
            If obj Is Nothing OrElse IsDBNull(obj) Then
                ' returns the default value for the type
                Return CType(Nothing, T)
            Else
                Return CType(obj, T)
            End If
        End Function

#End Region

    End Module
End Namespace