Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Module Encryption

    ' This is the function used to "hide" the database password in code
    <DebuggerStepThrough()> _
    Public Function SimpleCrypt(ByVal Text As String) As String
        ' Encrypts/decrypts the passed string using
        ' a simple ASCII value-swapping algorithm
        Dim strTempChar As String = ""
        Dim i As Integer

        For i = 1 To Len(Text)
            If Asc(Mid$(Text, i, 1)) < 128 Then
                strTempChar = _
          CType(Asc(Mid$(Text, i, 1)) + 128, String)
            ElseIf Asc(Mid$(Text, i, 1)) > 128 Then
                strTempChar = _
          CType(Asc(Mid$(Text, i, 1)) - 128, String)
            End If
            Mid$(Text, i, 1) = _
                Chr(CType(strTempChar, Integer))
        Next i
        Return Text
    End Function
End Module


' This is the class used to encrypt/decrypt user passwords when storing/retrieving from database
Public Class EncryptDecrypt

    Public Shared Function EncryptText(ByVal strText As String) As String
        Return Encrypt(strText, "&%#@?,:*")
    End Function

    Public Shared Function DecryptText(ByVal strText As String) As String
        Return Decrypt(strText, "&%#@?,:*")
    End Function

    Private Shared Function Encrypt(ByVal strText As String, ByVal strEncrKey _
             As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))

            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Shared Function Decrypt(ByVal strText As String, ByVal sDecrKey _
               As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)

            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

            Return encoding.GetString(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
End Class
