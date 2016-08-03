Module FileSystem

    Public Sub DeleteFileIfPossible(fileHandle As String, Optional recycle As Boolean = True)
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

End Module
