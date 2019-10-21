Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Reflection
Imports System.Runtime.CompilerServices

Module EnumExtensions

    Private ReadOnly enumDescriptions As New Dictionary(Of String, String)

    ''' <summary>
    ''' If a Description attribute is present for an enum value, returns the description.
    ''' Otherwise, returns the normal ToString() representation of the enum value.
    ''' </summary>
    ''' <param name="e">The enum value to describe.</param>
    ''' <returns>The value of the Description attribute if present, else
    ''' the normal ToString() representation of the enum value.</returns>
    ''' <remarks>https://stackoverflow.com/a/14772005/212978</remarks>
    <DebuggerStepThrough>
    <Extension>
    Public Function GetDescription(e As [Enum]) As String
        Dim enumType As Type = e.GetType()
        Dim name As String = e.ToString()

        ' Construct a full name for this enum value
        Dim fullName As String = enumType.FullName + "." + name

        ' See if we have looked it up earlier
        Dim enumDescription As String = Nothing
        If enumDescriptions.TryGetValue(fullName, enumDescription) Then
            ' Yes we have - return previous value
            Return enumDescription
        End If

        ' Find the value of the Description attribute on this enum value
        Dim members As MemberInfo() = enumType.GetMember(name)
        If members IsNot Nothing AndAlso members.Length > 0 Then
            Dim descriptions As Object() = members(0).GetCustomAttributes(GetType(DescriptionAttribute), False)
            If descriptions IsNot Nothing AndAlso descriptions.Length > 0 Then
                ' Set name to description found
                name = DirectCast(descriptions(0), DescriptionAttribute).Description
            End If
        End If

        ' Save the name in the dictionary:
        enumDescriptions.Add(fullName, name)

        Return name
    End Function

    ''' <summary>
    ''' For a given flag enum value, returns an iterator of each flag that is set.
    ''' </summary>
    ''' <param name="flagValues">The flag enum value to iterate.</param>
    ''' <returns>An IEnumerable iterator of enums.</returns>
    <Extension>
    Public Iterator Function GetUniqueFlags(flagValues As [Enum]) As IEnumerable(Of [Enum])
        If Convert.ToInt32(flagValues) = 0 Then
            Yield flagValues
        Else
            Dim flag As ULong = 1
            For Each value As [Enum] In [Enum].GetValues(flagValues.GetType())
                Dim bits As ULong = Convert.ToUInt64(value)
                While flag < bits
                    flag <<= 1
                End While
                If flag = bits AndAlso flagValues.HasFlag(value) Then
                    Yield value
                End If
            Next
        End If
    End Function

    ''' <summary>
    ''' For a given flag enum value, returns an iterator of a string description of each flag that is set.
    ''' </summary>
    ''' <param name="flagValues">The flag enum value to iterate.</param>
    ''' <returns>An IEnumerable iterator of enum descriptions.</returns>
    <Extension>
    Public Iterator Function GetUniqueFlagDescriptions(flagValues As [Enum]) As IEnumerable(Of String)
        For Each value As [Enum] In flagValues.GetUniqueFlags
            Yield value.GetDescription
        Next
    End Function

    Public Function EnumToDataTable(enumType As Type) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Key", [Enum].GetUnderlyingType(enumType))
        dt.Columns.Add("Description", GetType(String))

        Dim e As [Enum]
        For Each name As String In [Enum].GetNames(enumType)
            e = [Enum].Parse(enumType, name)
            dt.Rows.Add(e, e.GetDescription)
        Next

        Return dt
    End Function

End Module