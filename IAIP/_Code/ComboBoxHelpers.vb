Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports System.Linq

Module ComboBoxHelpers

    <Extension()>
    Public Sub BindToDictionary(c As ComboBox, d As Dictionary(Of Integer, String))
        With c
            .DataSource = New BindingSource(d, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    <Extension()>
    Public Sub BindToSortedDictionary(c As ComboBox, d As SortedDictionary(Of Integer, String))
        With c
            .DataSource = New BindingSource(d, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    <Extension()>
    Public Sub BindToDictionary(Of T)(c As ComboBox, d As Dictionary(Of T, String))
        With c
            .DataSource = New BindingSource(d, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    <Extension()>
    Public Sub BindToKeyValuePairs(c As ComboBox, l As Dictionary(Of Integer, String))
        With c
            .DataSource = New BindingSource(l, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    ''' <summary>
    ''' Populates the combobox with the values and text descriptions of an Enum.
    ''' </summary>
    ''' <typeparam name="TEnum">The Enum to bind to.</typeparam>
    ''' <param name="c">The Combobox to bind.</param>
    <Extension()>
    Public Sub BindToEnum(Of TEnum)(c As ComboBox)
        Dim enumType As Type = GetType(TEnum)

        If enumType.BaseType IsNot GetType([Enum]) Then
            Throw New ArgumentException("TEnum must be of type System.Enum")
        End If

        Dim enumItems As Array = [Enum].GetValues(enumType)
        Dim enumDict As New Dictionary(Of [Enum], String)

        For Each enumItem As [Enum] In enumItems
            enumDict(enumItem) = enumItem.GetDescription
        Next

        c.BindToDictionary(enumDict)
    End Sub

    <Extension()>
    Public Sub SetDropDownWidth(c As ComboBox)
        If c Is Nothing OrElse c.Items.Count = 0 Then
            Return
        End If

        Dim scrollBarWidth As Integer = SystemInformation.VerticalScrollBarWidth
        c.DropDownWidth = Math.Max(c.Width, c.Items.Cast(Of Object).Max(Function(o) TextRenderer.MeasureText(c.GetItemText(o), c.Font).Width) + scrollBarWidth)
    End Sub

    Public Sub SetComboBoxFilter(cbo As ComboBox, rowFilter As String)
        Dim view As DataView = CType(cbo.DataSource, DataView)
        view.RowFilter = rowFilter
        cbo.SelectedValue = 0
    End Sub

End Module