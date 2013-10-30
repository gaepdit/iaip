Imports System.Xml
Imports System.Xml.Serialization

Module FormSettings

    ' Public function for retrieving form settings
    Friend Function GetFormSettings(ByVal whichForm As String) As XmlSerializableDictionary(Of String, String)
        Dim thisFormSettings As XmlSerializableDictionary(Of String, String) = _
            New XmlSerializableDictionary(Of String, String)

        Dim allFormSettingsString As String = GetUserSetting(UserSetting.FormSettings)
        Dim allFormSettings As XmlSerializableDictionary(Of String, String) 

        If allFormSettingsString <> "" Then
            Dim reader As XmlReader = XmlReader.Create(New System.IO.StringReader(allFormSettingsString))
            Dim serializer As New XmlSerializer(GetType(XmlSerializableDictionary(Of String, String)))

            allFormSettings = serializer.Deserialize(reader)
            If allFormSettings.ContainsKey(whichForm) Then
                Dim thisFormSettingsString As String = allFormSettings(whichForm)

                If thisFormSettingsString <> "" Then
                    reader = XmlReader.Create(New System.IO.StringReader(thisFormSettingsString))
                    thisFormSettings = serializer.Deserialize(reader)
                End If
            End If
        End If

        Return thisFormSettings
    End Function

    ' Public function for saving individual form settings
    Friend Sub SaveFormSettings(ByVal whichForm As String, ByVal formSettings As XmlSerializableDictionary(Of String, String))
        Dim allFormSettingsString As String = GetUserSetting(UserSetting.FormSettings)
        Dim allFormSettings As XmlSerializableDictionary(Of String, String) = _
            New XmlSerializableDictionary(Of String, String)
        Dim serializer As New XmlSerializer(GetType(XmlSerializableDictionary(Of String, String)))

        If allFormSettingsString <> "" Then
            Dim reader As XmlReader = XmlReader.Create(New System.IO.StringReader(allFormSettingsString))
            allFormSettings = serializer.Deserialize(reader)
        End If

        Dim sb As New System.Text.StringBuilder()
        Using writer As XmlWriter = XmlWriter.Create(sb)
            serializer.Serialize(writer, formSettings)
        End Using
        allFormSettings(whichForm) = sb.ToString

        sb = New System.Text.StringBuilder()
        Using writer As XmlWriter = XmlWriter.Create(sb)
            serializer.Serialize(writer, allFormSettings)
        End Using

        SaveUserSetting(UserSetting.FormSettings, sb.ToString)
    End Sub

    ' Public function for deleting all form settings
    Friend Sub ResetAllFormSettings()
        ResetUserSetting(UserSetting.FormSettings)
    End Sub

End Module
