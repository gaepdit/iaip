Imports System.Text
Imports System.Security.Cryptography

Module MD5

    Public Function HashData(ByVal s As String) As String

        'Convert the string to a byte array

        Dim bytDataToHash As Byte() = (New UnicodeEncoding).GetBytes(s)

        'Compute the MD5 hash algorithm

        Dim bytHashValue As Byte() = New MD5CryptoServiceProvider().ComputeHash(bytDataToHash)

        Return BitConverter.ToString(bytHashValue)

    End Function

    ' Hash an input string and return the hash as
    ' a 32 character hexadecimal string.
    Function getMd5Hash(ByVal input As String) As String
        ' Create a new instance of the MD5CryptoServiceProvider object.
        Dim md5Hasher As New MD5CryptoServiceProvider()

        ' Convert the input string to a byte array and compute the hash.
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))

        ' Create a new Stringbuilder to collect the bytes
        ' and create a string.
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data 
        ' and format each one as a hexadecimal string.
        For i As Integer = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string.
        Return sBuilder.ToString()

    End Function

    ' Verify a hash against a string.
    Function verifyMd5Hash(ByVal input As String, ByVal hash As String) As Boolean
        ' Hash the input.
        Dim hashOfInput As String = getMd5Hash(input)

        ' Create a StringComparer an compare the hashes.
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase

        If 0 = comparer.Compare(hashOfInput, hash) Then
            Return True
        Else
            Return False
        End If

    End Function

End Module
