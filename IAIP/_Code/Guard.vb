Imports System.Collections.Generic

Public Module Guard

    Public Sub NotNull(Of T)(value As T, parameterName As String)
        If value Is Nothing Then
            Throw New ArgumentNullException(parameterName)
        End If
    End Sub

    Public Sub NotNullOrEmpty(value As String, parameterName As String)
        If value Is Nothing Then
            Throw New ArgumentNullException(parameterName)
        End If

        If String.IsNullOrEmpty(value) Then
            Throw New ArgumentException($"{parameterName} can not be null or empty.", parameterName)
        End If
    End Sub

    Public Function NotNullOrWhiteSpace(value As String, parameterName As String) As String
        If value Is Nothing Then
            Throw New ArgumentNullException(parameterName)
        End If

        If String.IsNullOrWhiteSpace(value) Then
            Throw New ArgumentException($"{parameterName} can not be null, empty, or white space.", parameterName)
        End If

        Return value
    End Function

    Public Function NotNullOrValueless(value As ICollection(Of String), parameterName As String) As ICollection(Of String)
        If value Is Nothing Then
            Throw New ArgumentNullException(parameterName)
        End If

        If value.Count <= 0 Then
            Throw New ArgumentException($"{parameterName} can not be empty.", parameterName)
        End If

        Dim i As Integer = 0
        For Each valueItem As String In value
            NotNullOrWhiteSpace(valueItem, $"{parameterName}_{i}")
            i += 1
        Next

        Return value
    End Function

    'Public Sub NotNullOrEmpty(value As ICollection, parameterName As String)
    '    If value Is Nothing Then
    '        Throw New ArgumentNullException(parameterName)
    '    End If

    '    If value.Count <= 0 Then
    '        Throw New ArgumentException($"{parameterName} can not be empty.", parameterName)
    '    End If
    'End Sub

    'Public Sub ArgumentNotNegative(value As Integer, parameterName As String)
    '    If value < 0 Then
    '        Throw New ArgumentException($"{parameterName} can not be negative.", parameterName)
    '    End If
    'End Sub

    'Public Sub ArgumentIsPositive(value As Integer, parameterName As String)
    '    If value <= 0 Then
    '        Throw New ArgumentException($"{parameterName} must be positive (greater than zero).", parameterName)
    '    End If
    'End Sub

End Module
