Module UserSettings

    ' Define user settings here
    Friend Enum UserSetting
        ExcelExportLocation
        PermitUploadLocation
        EnforcementUploadLocation
        FileDownloadLocation
        PrefillLoginId
        PasswordResetRequestedDate
        SelectedNavWorkListContext
        SelectedNavWorkListScope
    End Enum

    ' Define default value for above user settings here
    Private Function DefaultSetting(ByVal whichSetting As UserSetting) As String
        Select Case whichSetting

            Case UserSetting.ExcelExportLocation,
                 UserSetting.PermitUploadLocation,
                 UserSetting.EnforcementUploadLocation,
                 UserSetting.FileDownloadLocation
                Return Environment.GetFolderPath(Environment.SpecialFolder.Personal)

            Case UserSetting.SelectedNavWorkListContext
                Return IAIPNavigation.NavWorkListContext.PermitApplications.ToString

            Case UserSetting.SelectedNavWorkListScope
                Return IAIPNavigation.NavWorkListScope.StaffView.ToString

            Case Else
                Return ""

        End Select
    End Function

    ' Public function for retrieving a setting
    Friend Function GetUserSetting(ByVal whichSetting As UserSetting) As String
        If SettingsHelper.KeySettingsDictionary.ContainsKey(whichSetting.ToString) Then
            Return SettingsHelper.KeySettingsDictionary(whichSetting.ToString)
        Else
            Return DefaultSetting(whichSetting)
        End If
    End Function

    ' Public function for saving a setting
    Friend Sub SaveUserSetting(ByVal whichSetting As UserSetting, ByVal value As String)
        SettingsHelper.KeySettingsDictionary(whichSetting.ToString) = value
    End Sub

    ' Public function for deleting a setting (resetting to default)
    Friend Sub ResetUserSetting(ByVal whichSetting As UserSetting)
        SettingsHelper.KeySettingsDictionary.Remove(whichSetting.ToString)
    End Sub

End Module
