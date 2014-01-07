Module FileSystem

    Public Sub DeleteFileIfPossible(ByVal fileHandle As String, Optional ByVal recycle As Boolean = True)
        Try
            If FileIO.FileSystem.FileExists(fileHandle) Then
                If recycle Then
                    FileIO.FileSystem.DeleteFile(fileHandle, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                Else
                    FileIO.FileSystem.DeleteFile(fileHandle)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetSpecialFolderPath(ByVal nFolder As CSIDL) As String
        Dim sbPath As New System.Text.StringBuilder(MAX_PATH)
        SHGetFolderPath(IntPtr.Zero, nFolder, IntPtr.Zero, 0, sbPath)
        Return sbPath.ToString()
    End Function

    Public Function GetAllUsersDesktopPath() As String
        Return GetSpecialFolderPath(CSIDL.COMMON_DESKTOPDIRECTORY)
    End Function

    Public Function GetAllUsersStartMenuPath() As String
        Return GetSpecialFolderPath(CSIDL.COMMON_PROGRAMS)
    End Function

End Module
