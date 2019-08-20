Imports Microsoft.Win32

Public Module GetDotNetVersion
    ' From https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed#net_d

    Public Function Get45PlusFromRegistry() As String
        Const subkey As String = "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"

        Using baseKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
            Using ndpKey As RegistryKey = baseKey.OpenSubKey(subkey)
                If ndpKey IsNot Nothing AndAlso ndpKey.GetValue("Release") IsNot Nothing Then
                    Return CheckFor45PlusVersion(CInt(ndpKey.GetValue("Release")))
                End If

                Return "No 4.5 or later version detected"
            End Using
        End Using
    End Function

    Private Function CheckFor45PlusVersion(releaseKey As Integer) As String
        ' Checking the version using >= enables forward compatibility.
        Select Case releaseKey
            Case >= 528040
                Return "4.8 or later"
            Case >= 461808
                Return "4.7.2"
            Case >= 461308
                Return "4.7.1"
            Case >= 460798
                Return "4.7"
            Case >= 394802
                Return "4.6.2"
            Case >= 394254
                Return "4.6.1"
            Case >= 393295
                Return "4.6"
            Case >= 379893
                Return "4.5.2"
            Case >= 378675
                Return "4.5.1"
            Case >= 378389
                Return "4.5"
            Case Else
                ' This code should never execute. A non-null release key should mean
                ' that 4.5 Or later Is installed.
                Return "Error"
        End Select
    End Function

End Module
