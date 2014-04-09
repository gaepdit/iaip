﻿Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Collections.Generic

Namespace DB
    Module DB

#Region " DB Connection Strings "

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

#End Region

#Region " Read (Scalar) "

        Public Function GetSingleValue(Of T)(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing) As T
            Dim parameterArray As OracleParameter() = {parameter}
            Return GetSingleValue(Of T)(query, parameterArray)
        End Function

        Public Function GetSingleValue(Of T)(ByVal query As String, ByVal parameterArray As OracleParameter()) As T
            Dim result As Object = Nothing
            Using connection As New OracleConnection(GetCurrentConnectionString)
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
            Using connection As New OracleConnection(GetCurrentConnectionString)
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
            Using connection As New OracleConnection(GetCurrentConnectionString)
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

        Public Function RunCommand(ByVal query As String, Optional ByVal parameter As OracleParameter = Nothing, Optional ByRef count As Integer = 0) As Boolean
            count = 0
            Dim parameterArray As OracleParameter() = {parameter}
            Return RunCommand(query, parameterArray, count)
        End Function

        Public Function RunCommand(ByVal query As String, ByVal parameters As OracleParameter(), Optional ByRef count As Integer = 0) As Boolean
            count = 0
            Dim queryList As New List(Of String)
            queryList.Add(query)

            Dim parametersList As New List(Of OracleParameter())
            parametersList.Add(parameters)

            Dim countList As New List(Of Integer)

            Dim result As Boolean = RunCommand(queryList, parametersList, countList)

            If result AndAlso countList.Count > 0 Then count = countList(0)

            Return result
        End Function

        Public Function RunCommand(ByVal queryList As List(Of String), ByVal parametersList As List(Of OracleParameter()), Optional ByRef countList As List(Of Integer) = Nothing) As Boolean
            If countList Is Nothing Then countList = New List(Of Integer)
            countList.Clear()
            If queryList.Count <> parametersList.Count Then Return False

            Using connection As New OracleConnection(GetCurrentConnectionString)
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

                        command.Connection.Close()
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

    End Module
End Namespace