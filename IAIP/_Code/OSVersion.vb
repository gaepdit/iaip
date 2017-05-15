Friend Module OSVersion
    Private Function HKLM_GetString(path As String, key As String) As String
        Try
            Dim rk As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(path)
            If rk Is Nothing Then
                Return Nothing
            End If
            Return rk.GetValue(key).ToString
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function OSFriendlyName() As String
        Dim ProductName As String = HKLM_GetString("SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName")
        Dim CSDVersion As String = HKLM_GetString("SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion")
        Return ConcatNonEmptyStrings(" ", {ProductName, CSDVersion})
    End Function
End Module
