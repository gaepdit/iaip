Imports System.Collections.Generic
Imports Jil

Module FormSettings

    ' Define allowed form settings here
    Friend Enum FormSetting
        WindowState
        Location
        Size
    End Enum

    Friend AllFormSettings As New Dictionary(Of String, Dictionary(Of String, String))

    Friend Function GetAllFormSettings() As Dictionary(Of String, Dictionary(Of String, String))
        If My.Settings.SerializedFormSettings <> "" Then
            Return JSON.Deserialize(Of Dictionary(Of String, Dictionary(Of String, String)))(My.Settings.SerializedFormSettings)
        End If
        Return New Dictionary(Of String, Dictionary(Of String, String))
    End Function

    Friend Sub SaveAllFormSettings()
        My.Settings.SerializedFormSettings = JSON.Serialize(AllFormSettings)
    End Sub

    ' Public function for retrieving form settings
    Friend Function GetFormSettings(formName As String) As Dictionary(Of String, String)
        If AllFormSettings.ContainsKey(formName) Then
            Return AllFormSettings(formName)
        End If
        Return New Dictionary(Of String, String)
    End Function

    ' Public function for saving individual form settings
    Friend Sub SaveFormSettings(formName As String, formSettings As Dictionary(Of String, String))
        AllFormSettings(formName) = formSettings
    End Sub

    ' Public function for deleting all form settings
    Friend Sub ResetAllFormSettings()
        AllFormSettings = New Dictionary(Of String, Dictionary(Of String, String))
    End Sub

End Module
