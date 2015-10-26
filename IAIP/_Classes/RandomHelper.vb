Imports System.Linq

Public Class RandomHelper
    Private Shared ReadOnly random As New Random()
    Private Shared ReadOnly randomLock As New Object()
    Private Shared ReadOnly readableChars As String = "ABCDEFGHKMNPQRTUVWXYZabcdefghijkmnpqrstuvwxyz2346789"

    Public Shared Function RandomNumber(min As Integer, max As Integer) As Integer
        SyncLock randomLock
            Return random.Next(min, max)
        End SyncLock
    End Function

    Public Shared Function RandomString(length As Integer, Optional chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ") As String
        Return New String(Enumerable.Repeat(chars, length).Select(Function(s) s(random.Next(s.Length))).ToArray())
    End Function

    Public Shared Function RandomReadableString(length As Integer) As String
        Return RandomString(length, readableChars)
    End Function

End Class
