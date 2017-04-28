Imports System.Collections.Generic
Imports System.Runtime.Serialization

' Adapted from http://stackoverflow.com/a/11801369/212978
Friend Class SettingsHelper
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
                    Dim ser As New Json.DataContractJsonSerializer(GetType(Dictionary(Of String, String)))
                    Using memStream As New IO.MemoryStream()
                        Using writer As New IO.StreamWriter(memStream)
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

    Private Shared Sub HandleSettingsLoad(sender As Object, e As EventArgs)
        If (_keySettingsDictionary Is Nothing) Then
            InitializeDictionary()
        End If
    End Sub

    Private Shared Sub HandleSettingsSaving(sender As Object, e As EventArgs)
        ' Ensure User Setting value is updated before save.
        Dim ser As New Json.DataContractJsonSerializer(GetType(Dictionary(Of String, String)))
        Using memStream As New IO.MemoryStream()
            ser.WriteObject(memStream, _keySettingsDictionary)
            memStream.Position = 0
            Using reader As New IO.StreamReader(memStream)
                My.Settings.SerializedUserSettingsDictionary = reader.ReadToEnd()
            End Using
        End Using
    End Sub
End Class
