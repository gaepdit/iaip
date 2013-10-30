Imports System.Xml
Imports System.Xml.Serialization

Module DictionarySettings

    ' Public function for retrieving a setting
    Friend Function GetDictionarySetting(ByVal key As String) As SerializableDictionary(Of String, String)
        Dim settingsString As String
        Dim settings As SerializableDictionary(Of String, String)

        If SettingsHelper.KeySettingsDictionary.ContainsKey(key) Then
            settingsString = SettingsHelper.KeySettingsDictionary(key)
        Else
            settingsString = ""
        End If

        If settingsString <> "" Then
            Dim reader As XmlReader = XmlReader.Create(New System.IO.StringReader(settingsString))
            Dim serializer As New XmlSerializer(GetType(SerializableDictionary(Of String, Object)))

            settings = serializer.Deserialize(reader)
        Else
            settings = Nothing
        End If

        Return settings
    End Function

    ' Public function for saving a setting
    Friend Sub SaveDictionarySetting(ByVal key As String, ByVal settings As SerializableDictionary(Of String, String))
        Dim sb As New System.Text.StringBuilder()
        Using writer As XmlWriter = XmlWriter.Create(sb)
            Dim serializer As New XmlSerializer(GetType(SerializableDictionary(Of String, Object)))
            serializer.Serialize(writer, settings)
        End Using

        SettingsHelper.KeySettingsDictionary(key) = sb.ToString
    End Sub

    ' Public function for deleting a setting (resetting it to default)
    Friend Sub ResetDictionarySetting(ByVal key As String)
        SettingsHelper.KeySettingsDictionary.Remove(key)
    End Sub

End Module
