Imports System.Collections.Generic
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization

'http://stackoverflow.com/questions/15072953/serializing-lists-with-an-object-serializer

<XmlRoot("dictionary")> _
Public Class SerializableDictionary(Of TKey, TValue)
    Inherits Dictionary(Of TKey, TValue)
    Implements IXmlSerializable

#Region " IXmlSerializable Members "

    Public Sub WriteXml(ByVal writer As XmlWriter) Implements IXmlSerializable.WriteXml
        ' Base types
        Dim baseKeyType As String = GetType(TKey).AssemblyQualifiedName
        Dim baseValueType As String = GetType(TValue).AssemblyQualifiedName
        writer.WriteAttributeString("keyType", baseKeyType)
        writer.WriteAttributeString("valueType", baseValueType)
        Try

            For Each key As TKey In Me.Keys
                ' Start
                writer.WriteStartElement("item")

                ' Key
                Dim keyType As Type = key.GetType
                Dim keySerializer As XmlSerializer = GetTypeSerializer(keyType.AssemblyQualifiedName)

                writer.WriteStartElement("key")
                If keyType Is GetType(TKey) Then
                    writer.WriteAttributeString("type", keyType.AssemblyQualifiedName)
                End If
                keySerializer.Serialize(writer, key)
                writer.WriteEndElement()

                ' Value
                Dim value As TValue = Me(key)
                Dim valueType As Type = value.GetType
                Dim valueSerializer As XmlSerializer = GetTypeSerializer(valueType.AssemblyQualifiedName)

                writer.WriteStartElement("value")
                If valueType Is GetType(TValue) Then
                    writer.WriteAttributeString("type", valueType.AssemblyQualifiedName)
                End If
                valueSerializer.Serialize(writer, value)
                writer.WriteEndElement()

                ' End
                writer.WriteEndElement()
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ReadXml(ByVal reader As XmlReader) Implements IXmlSerializable.ReadXml
        Dim wasEmpty As Boolean = reader.IsEmptyElement
        reader.Read()

        If wasEmpty Then
            Return
        End If

        ' Base types
        Dim baseKeyType As String = GetType(TKey).AssemblyQualifiedName
        Dim baseValueType As String = GetType(TValue).AssemblyQualifiedName

        While reader.NodeType <> XmlNodeType.EndElement
            ' Start
            reader.ReadStartElement("item")

            ' Key
            Dim keySerializer As XmlSerializer = GetTypeSerializer(If(reader("type"), baseKeyType))
            reader.ReadStartElement("key")
            Dim key As TKey = DirectCast(keySerializer.Deserialize(reader), TKey)
            reader.ReadEndElement()

            ' Value
            Dim valueSerializer As XmlSerializer = GetTypeSerializer(If(reader("type"), baseValueType))
            reader.ReadStartElement("value")
            Dim value As TValue = DirectCast(valueSerializer.Deserialize(reader), TValue)
            reader.ReadEndElement()

            ' Store
            Me.Add(key, value)

            ' End
            reader.ReadEndElement()
            reader.MoveToContent()
        End While
        reader.ReadEndElement()
    End Sub

    Public Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
        Return Nothing
    End Function

#End Region

#Region " GetTypeSerializer "

    Private Shared ReadOnly _serializers As New Dictionary(Of String, XmlSerializer)()
    Private Shared ReadOnly _deadbolt As New Object()
    Private Function GetTypeSerializer(ByVal type As String) As XmlSerializer
        If Not _serializers.ContainsKey(type) Then
            SyncLock _deadbolt
                If Not _serializers.ContainsKey(type) Then
                    _serializers.Add(type, New XmlSerializer(type.GetType))
                End If
            End SyncLock
        End If
        Return _serializers(type)
    End Function

#End Region

End Class
