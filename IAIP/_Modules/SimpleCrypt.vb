Module SimpleCrypt
    ' This is the function used to "hide" the database password in code
    <DebuggerStepThrough()>
    Public Function SimpleCrypt(ByVal Text As String) As String
        ' Encrypts/decrypts the passed string using
        ' a simple ASCII value-swapping algorithm
        Dim strTempChar As String = ""

        For i As Integer = 1 To Len(Text)
            If Asc(Mid$(Text, i, 1)) < 128 Then
                strTempChar = CType(Asc(Mid$(Text, i, 1)) + 128, String)
            ElseIf Asc(Mid$(Text, i, 1)) > 128 Then
                strTempChar = CType(Asc(Mid$(Text, i, 1)) - 128, String)
            End If
            Mid$(Text, i, 1) = Chr(strTempChar)
        Next i
        Return Text
    End Function
End Module
