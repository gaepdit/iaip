﻿Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection

Namespace DB
    Module SPFunctions

#Region " Stored Procedure Functions that return a single ReturnValue "
        ' Oracle FUNCTIONS that return a single value (must specify value type and size)
        ' Currently not used ---
        ' It was easier to just use RunCommand and add the ReturnValue parameter manually

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
        '                ErrorReport(ee, query, Reflection.MethodBase.GetCurrentMethod.Name)
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

#Region " Stored Procedures that return a SYS_REFCURSOR "
        ' These functions call Oracle FUNCTIONs that return an Oracle SYS_REFCURSOR.

#Region " DataRow "

        ''' <summary>
        ''' Retrieves a single row of values from the database.
        ''' </summary>
        ''' <param name="spName">The Oracle Stored Procedure to call (SP must be a function that returns a REFCURSOR)</param>
        ''' <param name="parameter">An optional Oracle Parameter to send.</param>
        ''' <returns>A DataRow.</returns>
        Public Function SPGetDataRow(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As DataRow
            Dim parameterArray As OracleParameter() = {parameter}
            Return SPGetDataRow(spName, parameterArray)
        End Function

        ''' <summary>
        ''' Retrieves a single row of values from the database.
        ''' </summary>
        ''' <param name="spName">The Oracle Stored Procedure to call (SP must be a function that returns a REFCURSOR)</param>
        ''' <param name="parameterArray">An optional Oracle Parameter to send.</param>
        ''' <returns>A DataRow</returns>
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

        ''' <summary>
        ''' Retrieves a DataTable of values from the database.
        ''' </summary>
        ''' <param name="spName">The Oracle Stored Procedure to call (SP must be a function that returns a REFCURSOR)</param>
        ''' <param name="parameter">An optional Oracle Parameter to send.</param>
        ''' <returns>A DataTable</returns>
        Public Function SPGetDataTable(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) As DataTable
            Dim parameterArray As OracleParameter() = {parameter}
            Return SPGetDataTable(spName, parameterArray)
        End Function

        ''' <summary>
        ''' Retrieves a DataTable of values from the database.
        ''' </summary>
        ''' <param name="spName">The Oracle Stored Procedure to call (SP must be a function that returns a REFCURSOR)</param>
        ''' <param name="parameterArray">An Oracle Parameter array to send.</param>
        ''' <returns>A DataTable</returns>
        Public Function SPGetDataTable(ByVal spName As String, ByVal parameterArray As OracleParameter()) As DataTable
            If String.IsNullOrEmpty(spName) Then
                Return Nothing
            End If

            AddRefCursorParameter(parameterArray)

            Dim table As New DataTable
            Dim success As Boolean = True
            Dim startTime As Date = Date.UtcNow
            Dim timer As Stopwatch = Stopwatch.StartNew

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
                        Catch ee As OracleException
                            success = False
                            ErrorReport(ee, spName, Reflection.MethodBase.GetCurrentMethod.Name)
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

#Region " Lists, Keys & Values "

        ''' <summary>
        ''' Calls an Oracle Stored Procedure and returns a List of KeyValuePairs with Integer keys and 
        ''' String values. Useful for creating DropDownList ComboBoxes.
        ''' </summary>
        ''' <param name="spName">The Oracle Stored Procedure to call (SP must be a function that returns a REFCURSOR)</param>
        ''' <param name="parameter">A single Oracle Parameter to pass in</param>
        ''' <returns>List of Integer keys and String value pairs</returns>
        ''' <remarks>Use List returned with ComboBox.BindToKeyValuePairs</remarks>
        Public Function SPGetListOfKeyValuePair(ByVal spName As String, Optional ByVal parameter As OracleParameter = Nothing) _
        As List(Of KeyValuePair(Of Integer, String))
            Dim l As New List(Of KeyValuePair(Of Integer, String))
            Dim dt As DataTable = SPGetDataTable(spName, parameter)

            For Each r As DataRow In dt.Rows
                l.Add(New KeyValuePair(Of Integer, String)(r.Item(0), GetNullable(Of String)(r.Item(1))))
            Next

            Return l
        End Function

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

#Region " Stored Procedures that run commands (ExecuteNonQuery) "
        ' These functions call Oracle Stored Procedures using IN and/or OUT parameters.
        ' If successful, any OUT parameters are available to the calling procedure as 
        ' returned by the Oracle database.

        ''' <summary>
        ''' Executes a Stored Procedure on the database.
        ''' </summary>
        ''' <param name="spName">The name of the Stored Procedure to execute.</param>
        ''' <param name="parameter">An optional OracleParameter to send.</param>
        ''' <returns>True if the Stored Procedure ran successfully. Otherwise, false.</returns>
        Public Function SPRunCommand(ByVal spName As String, Optional ByRef parameter As OracleParameter = Nothing) As Boolean
            Dim parameterArray As OracleParameter() = {parameter}
            Dim result As Boolean = SPRunCommand(spName, parameterArray)
            If result Then
                parameter = parameterArray(0)
            End If
            Return result
        End Function

        ''' <summary>
        ''' Executes a Stored Procedure on the database.
        ''' </summary>
        ''' <param name="spName">The name of the Stored Procedure to execute.</param>
        ''' <param name="parameterArray">An OracleParameter array to send.</param>
        ''' <returns>True if the Stored Procedure ran successfully. Otherwise, false.</returns>
        Public Function SPRunCommand(ByVal spName As String, ByRef parameterArray As OracleParameter()) As Boolean
            Dim success As Boolean = True
            Dim startTime As Date = Date.UtcNow
            Dim timer As Stopwatch = Stopwatch.StartNew

            Using connection As New OracleConnection(CurrentConnectionString)
                Using command As New OracleCommand(spName, connection)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.AddRange(parameterArray)
                    Try
                        command.Connection.Open()
                        command.ExecuteNonQuery()
                        command.Connection.Close()
                        command.Parameters.CopyTo(parameterArray, 0)
                    Catch ee As OracleException
                        success = False
                        ErrorReport(ee, spName, Reflection.MethodBase.GetCurrentMethod.Name)
                    Finally
                        timer.Stop()
                    End Try
                End Using
            End Using

            Return success
        End Function

#End Region

    End Module
End Namespace