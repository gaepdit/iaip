Imports System.Collections.Generic
Imports System.Runtime.Serialization

Module UserSettings

    ' Define user settings here
    Friend Enum UserSetting As Byte
        ExcelExportLocation
        PermitUploadLocation
        FileDownloadLocation
        PrefillLoginId
        NavigationFormLocation
    End Enum

    ' Define default value for above user settings here
    Private Function DefaultSetting(ByVal whichSetting As UserSetting) As String
        Select Case whichSetting

            Case UserSetting.ExcelExportLocation, _
                UserSetting.PermitUploadLocation, _
                UserSetting.FileDownloadLocation
                Return Environment.GetFolderPath(Environment.SpecialFolder.Personal)

            Case UserSetting.NavigationFormLocation
                Return "0,0"

            Case UserSetting.PrefillLoginId
                Return ""

            Case Else
                Return ""

        End Select
    End Function

    ' Public function for retrieving a setting
    Friend Function GetUserSetting(ByVal whichSetting As UserSetting) As String
        If UserSettingsHelper.KeySettingsDictionary.ContainsKey(whichSetting.ToString) Then
            Return UserSettingsHelper.KeySettingsDictionary(whichSetting.ToString)
        Else
            Return DefaultSetting(whichSetting)
        End If
    End Function

    ' Public function for saving a setting
    Friend Sub SaveUserSetting(ByVal whichSetting As UserSetting, ByVal value As String)
        UserSettingsHelper.KeySettingsDictionary(whichSetting.ToString) = value
    End Sub

    ' Public function for deleting a setting (resetting to default)
    Friend Sub ResetUserSetting(ByVal whichSetting As UserSetting)
        UserSettingsHelper.KeySettingsDictionary.Remove(whichSetting.ToString)
    End Sub

    ' Adapted from http://stackoverflow.com/a/11801369/212978
    Public Class UserSettingsHelper
        Private Shared _keySettingsDictionary As Dictionary(Of String, String)
        Private Shared _initLock As Object = New Object()

        Public Shared ReadOnly Property KeySettingsDictionary() As Dictionary(Of String, String)
            Get
                If (_keySettingsDictionary Is Nothing) Then
                    InitializeDictionary()
                End If
                Return _keySettingsDictionary
            End Get
        End Property

        Shared Sub New()
            AddHandler My.Settings.SettingsLoaded, AddressOf HandleSettingsLoad
            AddHandler My.Settings.SettingsSaving, AddressOf HandleSettingsSaving
        End Sub

        Private Shared Sub InitializeDictionary()
            ' Load dictionary from User Setting.
            SyncLock _initLock
                If (_keySettingsDictionary Is Nothing) Then
                    If (String.IsNullOrEmpty(My.Settings.SerializedUserSettingsDictionary)) Then
                        _keySettingsDictionary = New Dictionary(Of String, String)()
                    Else
                        Dim ser As New System.Runtime.Serialization.Json.DataContractJsonSerializer(GetType(Dictionary(Of String, String)))
                        Using memStream As New System.IO.MemoryStream()
                            Using writer As New System.IO.StreamWriter(memStream)
                                writer.Write(My.Settings.SerializedUserSettingsDictionary)
                                writer.Flush()
                                memStream.Position = 0
                                _keySettingsDictionary = CType(ser.ReadObject(memStream), Dictionary(Of String, String))
                            End Using
                        End Using
                    End If
                End If
            End SyncLock
        End Sub

        Private Shared Sub HandleSettingsLoad(ByVal sender As Object, ByVal e As EventArgs)
            If (_keySettingsDictionary Is Nothing) Then
                InitializeDictionary()
            End If
        End Sub

        Private Shared Sub HandleSettingsSaving(ByVal sender As Object, ByVal e As EventArgs)
            ' Ensure User Setting value is updated before save.
            Dim ser As New System.Runtime.Serialization.Json.DataContractJsonSerializer(GetType(Dictionary(Of String, String)))
            Using memStream As New System.IO.MemoryStream()
                ser.WriteObject(memStream, _keySettingsDictionary)
                memStream.Position = 0
                Using reader As New System.IO.StreamReader(memStream)
                    My.Settings.SerializedUserSettingsDictionary = reader.ReadToEnd()
                End Using
            End Using
        End Sub
    End Class

End Module
