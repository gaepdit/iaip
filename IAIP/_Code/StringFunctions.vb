Imports System.Collections.Generic
Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions

Public Module StringFunctions

    ''' <summary>
    ''' Implodes a String array to a single string, concatenating the items using the separator, and ignoring null or empty string items
    ''' </summary>
    ''' <param name="separator">The separator string to include between each item</param>
    ''' <param name="items">An array of strings to concatenate</param>
    ''' <returns>A concatenated string separated by the specified separator. Null or empty strings are ignored.</returns>
    Public Function ConcatNonEmptyStrings(separator As String, items As String()) As String
        Return String.Join(separator, Array.FindAll(items, Function(s) Not String.IsNullOrEmpty(s)))
    End Function

    ''' <summary>
    ''' Implodes a List of Strings to a single string, concatenating the items using the separator, and ignoring null or empty string items
    ''' </summary>
    ''' <param name="separator">The separator string to include between each item</param>
    ''' <param name="items">A List of Strings to concatenate</param>
    ''' <returns>A concatenated string separated by the specified separator. Null or empty strings are ignored.</returns>
    Public Function ConcatNonEmptyStrings(separator As String, items As List(Of String)) As String
        ArgumentNotNull(items, NameOf(items))
        Return ConcatNonEmptyStrings(separator, items.ToArray())
    End Function

    Public Function TrimArray(items As String()) As String()
        ArgumentNotNull(items, NameOf(items))

        Dim s As New List(Of String)
        For Each item As String In items
            s.Add(Trim(item))
        Next
        Return s.ToArray()
    End Function

    ''' <summary>
    ''' Takes a string of either 7 or 10 digits and formats it as a telephone number. Any other style string is returned as-is.
    ''' </summary>
    ''' <param name="p">The phone number string to format.</param>
    ''' <param name="formal">Whether to format the telephone number as "(999) 999-9999" (if True) or "999-999-9999" (if False).</param>
    ''' <returns>A formatted telephone number.</returns>
    ''' <remarks></remarks>
    Public Function FormatDigitsAsPhoneNumber(p As String, Optional formal As Boolean = True) As String
        If p Is Nothing Then Return p
        If Not Regex.IsMatch(p, NumericPattern) Then Return p
        If Not (p.Length = 7 OrElse p.Length >= 10) Then Return p

        If p.Length = 7 Then
            Return p.Substring(0, 3) & "-" & p.Substring(4, 4)
        ElseIf p.Length = 10 Then
            If formal Then
                Return "(" & p.Substring(0, 3) & ") " & p.Substring(3, 3) & "-" & p.Substring(6, 4)
            Else
                Return p.Substring(0, 3) & "-" & p.Substring(3, 3) & "-" & p.Substring(6, 4)
            End If
        Else
            If formal Then
                Return "(" & p.Substring(0, 3) & ") " & p.Substring(3, 3) & "-" & p.Substring(6, 4) & " Ext. " & p.Substring(10, p.Length - 10)
            Else
                Return p.Substring(0, 3) & "-" & p.Substring(3, 3) & "-" & p.Substring(6, 4) & " Ext. " & p.Substring(10, p.Length - 10)
            End If
        End If
    End Function

    <Extension>
    Public Function RealStringOrNothing(s As String) As String
        Return If(String.IsNullOrEmpty(s), Nothing, s)
    End Function

    <Extension>
    Public Function IfEmpty(s As String, alternate As String) As String
        Return If(String.IsNullOrEmpty(s), alternate, s)
    End Function

    <Extension>
    Public Function Truncate(value As String, maxLength As Integer) As String
        Return If(String.IsNullOrEmpty(value), value, value.Substring(0, Math.Min(maxLength, value.Length)))
    End Function

    <Extension>
    Public Function LineCount(str As String) As Integer
        Return Regex.Matches(str, Environment.NewLine).Count() + 1
    End Function

    Public Function StringValidatesAsCurrency(s As String) As Boolean
        Return Decimal.TryParse(s, NumberStyles.Currency, CultureInfo.CurrentCulture, Nothing)
    End Function

    Public Function ConvertCurrencyStringToDecimal(s As String) As Decimal
        Dim result As Decimal
        If Decimal.TryParse(s, NumberStyles.Currency, CultureInfo.CurrentCulture, result) Then
            Return result
        Else
            Return 0
        End If
    End Function

End Module