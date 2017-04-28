Imports System.Data.SqlClient
Imports System.IO

Module SaveFile

    ''' <summary>
    ''' Retrieves a binary file from a database and saves it to disk
    ''' </summary>
    ''' <param name="filePath">The path for saving the file.</param>
    ''' <param name="query">The DB query for retrieving the file from the DB.</param>
    ''' <param name="parameter">An optional SqlParameter to send.</param>
    ''' <returns></returns>
    Public Function SaveBinaryFileFromDB(filePath As String, query As String, Optional parameter As SqlParameter = Nothing) As Boolean
        Dim parameterArray As SqlParameter() = Nothing
        If parameter IsNot Nothing Then
            parameterArray = {parameter}
        End If
        Return SaveBinaryFileFromDB(filePath, query, parameterArray)
    End Function

    ''' <summary>
    ''' Retrieves a binary file from a database and saves it to disk
    ''' </summary>
    ''' <param name="filePath">The path for saving the file.</param>
    ''' <param name="query">The DB query for retrieving the file from the DB.</param>
    ''' <param name="parameterArray">A SqlParameter array to send.</param>
    ''' <returns></returns>
    Public Function SaveBinaryFileFromDB(filePath As String, query As String, parameterArray As SqlParameter()) As Boolean
        Dim byteArray As Byte() = DB.GetSingleValue(Of Byte())(query, parameterArray)

        Try
            File.WriteAllBytes(filePath, byteArray)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Reads a binary file into a Byte array.
    ''' </summary>
    ''' <param name="pathToFile">The path to the file to read.</param>
    ''' <returns>A Byte array.</returns>
    Public Function ReadByteArrayFromFile(pathToFile As String) As Byte()
        Return File.ReadAllBytes(pathToFile)
    End Function

End Module
