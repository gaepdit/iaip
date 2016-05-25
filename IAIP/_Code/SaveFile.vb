Imports System.Data.SqlClient
Imports System.IO

Module SaveFile

    Public Function SaveBinaryFileFromDB(filePath As String, query As String, Optional ByVal parameter As SqlParameter = Nothing) As Boolean
        Dim parameterArray As SqlParameter() = Nothing
        If parameter IsNot Nothing Then
            parameterArray = {parameter}
        End If
        Return SaveBinaryFileFromDB(filePath, query, parameterArray)
    End Function

    Public Function SaveBinaryFileFromDB(filePath As String, query As String, ByVal parameterArray As SqlParameter()) As Boolean
        Dim byteArray As Byte() = DB.GetByteArray(query, parameterArray)

        Try
            Using fs As New FileStream(filePath, FileMode.Create, FileAccess.Write)
                Using bw As New BinaryWriter(fs)
                    bw.Write(byteArray)
                End Using ' bw
            End Using ' fs

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
    Public Function ReadByteArrayFromFile(ByVal pathToFile As String) As Byte()
        Return File.ReadAllBytes(pathToFile)
    End Function

End Module
