Public Module Guard

    Public Sub ArgumentNotNull(Of T)(value As T, parameterName As String)
        If value Is Nothing Then
            Throw New ArgumentNullException(parameterName)
        End If
    End Sub

    Public Sub ArgumentNotNullOrEmpty(value As String, parameterName As String)
        If value Is Nothing Then
            Throw New ArgumentNullException(parameterName)
        End If

        If String.IsNullOrEmpty(value) Then
            Throw New ArgumentException($"{parameterName} can not be null or empty.", parameterName)
        End If
    End Sub

    'Public Sub ArgumentNotNullOrWhiteSpace(value As String, parameterName As String)
    '    If value Is Nothing Then
    '        Throw New ArgumentNullException(parameterName)
    '    End If

    '    If String.IsNullOrWhiteSpace(value) Then
    '        Throw New ArgumentException($"{parameterName} can not be null, empty, or white space.", parameterName)
    '    End If
    'End Sub

    'Public Sub ArgumentNotNullOrEmpty(value As ICollection, parameterName As String)
    '    If value Is Nothing Then
    '        Throw New ArgumentNullException(parameterName)
    '    End If

    '    If value.CollectionIsNullOrEmpty() Then
    '        Throw New ArgumentException($"{parameterName} can not be null or empty.", parameterName)
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

    '''' <summary>
    '''' Checks whatever given collection object is null or has no item.
    '''' </summary>
    '<Extension()>
    'Private Function CollectionIsNullOrEmpty(source As ICollection) As Boolean
    '    Return source Is Nothing OrElse source.Count <= 0
    'End Function

End Module
