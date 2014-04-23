Imports System.Collections.Generic
Imports System.IO

Namespace DB
    Module Utilities

        <DebuggerStepThrough()> _
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

        Public Sub AddBlankRowToDictionary(ByRef d As Dictionary(Of Integer, String), Optional ByVal blankPrompt As String = "")
            d.Add(0, blankPrompt)
        End Sub

        Public Sub BindDictionaryToComboBox(ByVal d As Dictionary(Of Integer, String), ByVal c As ComboBox)
            With c
                .DataSource = New BindingSource(d, Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
        End Sub

        Public Sub BindSortedDictionaryToComboBox(ByVal d As SortedDictionary(Of Integer, String), ByVal c As ComboBox)
            With c
                .DataSource = New BindingSource(d, Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
        End Sub

        Public Function ReadByteArrayFromFile(ByVal pathToFile As String) As Byte()
            Dim fs As New FileStream(pathToFile, FileMode.Open, FileAccess.Read)

            Dim byteArray As Byte() = File.ReadAllBytes(pathToFile)

            fs.Close()
            fs.Dispose()

            Return byteArray
        End Function


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
        ''' </remarks>
        Public Function PingDBConnection(ByVal conn As Oracle.DataAccess.Client.OracleConnection) As Boolean
            Dim sql As String = "SELECT 1 FROM DUAL"
            Using cmd As New Oracle.DataAccess.Client.OracleCommand(sql, conn)
                Dim result As Object = Nothing
                Try
                    If conn.State = ConnectionState.Closed Then conn.Open()
                    result = cmd.ExecuteScalar()
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Using
        End Function

#End Region

    End Module
End Namespace
