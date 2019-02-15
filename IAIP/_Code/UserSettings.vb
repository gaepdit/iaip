Imports Syroot.Windows.IO

Module UserSettings

    ' Define user settings here
    Friend Enum UserSetting
        FileUploadLocation
        FileDownloadLocation
        PrefillLoginId
        PasswordResetRequestedDate
        SelectedNavWorkListContext
        SelectedNavWorkListScope
    End Enum

    ' Define default value for above user settings here
    Private Function DefaultSetting(whichSetting As UserSetting) As String
        Select Case whichSetting

            Case UserSetting.FileUploadLocation
                Try
                    Return New KnownFolder(KnownFolderType.Documents).Path
                Catch ex As Exception
                    Return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                End Try

            Case UserSetting.FileDownloadLocation
                Try
                    Return New KnownFolder(KnownFolderType.Downloads).Path
                Catch ex As Exception
                    Return Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                End Try

            Case UserSetting.SelectedNavWorkListContext
                Return IAIPNavigation.NavWorkListContext.PermitApplications.ToString

            Case UserSetting.SelectedNavWorkListScope
                Return IAIPNavigation.NavWorkListScope.StaffView.ToString

            Case Else
                Return ""

        End Select
    End Function

    ' Public function for retrieving a setting
    Friend Function GetUserSetting(whichSetting As UserSetting) As String
        If SettingsHelper.KeySettingsDictionary.ContainsKey(whichSetting.ToString) Then
            Return SettingsHelper.KeySettingsDictionary(whichSetting.ToString)
        Else
            Return DefaultSetting(whichSetting)
        End If
    End Function

    ' Public function for saving a setting
    Friend Sub SaveUserSetting(whichSetting As UserSetting, value As String)
        SettingsHelper.KeySettingsDictionary(whichSetting.ToString) = value
    End Sub

    ' Public function for deleting a setting (resetting to default)
    Friend Sub ResetUserSetting(whichSetting As UserSetting)
        SettingsHelper.KeySettingsDictionary.Remove(whichSetting.ToString)
    End Sub

End Module
