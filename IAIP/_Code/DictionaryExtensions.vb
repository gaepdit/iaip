Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Module DictionaryExtensions

    <Extension>
    Public Sub AddBlankRow(ByRef d As Dictionary(Of Integer, String), Optional blankPrompt As String = "")
        d.Add(0, blankPrompt)
    End Sub

    ''' <summary>
    ''' Adds an element with the provided key and value to the System.Collections.Generic.IDictionary 
    ''' object with a String key. If an element with that key already exists, the key is appended with 
    ''' a Guid string to encourage uniqueness.
    ''' </summary>
    ''' <param name="d">The IDictionary object to add the element to.</param>
    ''' <param name="key">The String to use as the key of the element to add.</param>
    ''' <param name="value">The System.Object to use as the value of the element to add.</param>
    ''' <exception cref="ArgumentNullException">key is null.</exception>
    ''' <exception cref="ArgumentException">key is an empty string.</exception>
    ''' <exception cref="NotSupportedException">The System.Collections.IDictionary is read-only. -or- The System.Collections.IDictionary
    ''' has a fixed size.</exception>
    <Extension>
    Public Sub AddAsUniqueIfExists(Of TValue)(ByRef d As IDictionary(Of String, TValue), key As String, value As TValue)
        NotNullOrEmpty(key, NameOf(key))

        If d.ContainsKey(key) Then
            key &= "-" & Guid.NewGuid.ToString
        End If

        d.Add(key, value)
    End Sub

    ''' <summary>
    ''' Adds an element with the provided String key and value to the System.Collections.IDictionary 
    ''' object. If an element with that key already exists, the key is appended with a Guid string
    ''' to encourage uniqueness.
    ''' </summary>
    ''' <param name="d">The IDictionary object to add the element to.</param>
    ''' <param name="key">The String to use as the key of the element to add.</param>
    ''' <param name="value">The System.Object to use as the value of the element to add.</param>
    ''' <exception cref="ArgumentNullException">key is null.</exception>
    ''' <exception cref="ArgumentException">key is an empty string.</exception>
    ''' <exception cref="NotSupportedException">The System.Collections.IDictionary is read-only. -or- The System.Collections.IDictionary
    ''' has a fixed size.</exception>
    <Extension>
    Public Sub AddAsUniqueIfExists(ByRef d As IDictionary, key As String, value As Object)
        NotNullOrEmpty(key, NameOf(key))

        If d.Contains(key) Then
            key &= "-" & Guid.NewGuid.ToString
        End If

        d.Add(key, value)
    End Sub

    ''' <summary>
    ''' Adds an element with the provided key and value to the System.Collections.IDictionary 
    ''' object if an element with that key does not yet exist.
    ''' </summary>
    ''' <param name="d">The IDictionary object to add the element to.</param>
    ''' <param name="key">The System.Object to use as the key of the element to add.</param>
    ''' <param name="value">The System.Object to use as the value of the element to add.</param>
    ''' <exception cref="ArgumentNullException">key is null.</exception>
    ''' <exception cref="NotSupportedException">The System.Collections.IDictionary is read-only. -or- The System.Collections.IDictionary
    ''' has a fixed size.</exception>
    <Extension>
    Public Sub AddIfNotExists(ByRef d As IDictionary, key As Object, value As Object)
        NotNull(key, NameOf(key))

        If Not d.Contains(key) Then
            d.Add(key, value)
        End If
    End Sub

End Module