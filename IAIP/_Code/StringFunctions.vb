﻿Imports System.Text.RegularExpressions

Public Module StringFunctions

    ''' <summary>
    ''' Implodes a String array to a single string, concatenating the items using the separator, and ignoring null or empty string items
    ''' </summary>
    ''' <param name="separator">The separator string to include between each item</param>
    ''' <param name="items">An array of strings to concatenate</param>
    ''' <returns>A concatenated string separated by the specified separator. Null or empty strings are ignored.</returns>
    ''' <remarks></remarks>
    Public Function ConcatNonEmptyStrings(ByVal separator As String, ByVal items As String()) As String
        Return String.Join(separator, Array.FindAll(items, Function(s) Not String.IsNullOrEmpty(s)))
    End Function

    ''' <summary>
    ''' Takes a string of either 7 or 10 digits and formats it as a telephone number. Any other style string is returned as-is.
    ''' </summary>
    ''' <param name="p">The phone number string to format.</param>
    ''' <param name="formal">Whether to format the telephone number as "(999) 999-9999" (if True) or "999-999-9999" (if False).</param>
    ''' <returns>A formatted telephone number.</returns>
    ''' <remarks></remarks>
    Public Function FormatDigitsAsPhoneNumber(ByVal p As String, Optional ByVal formal As Boolean = True) As String
        If p Is Nothing Then Return p
        If Not Regex.IsMatch(p, NumericPattern) Then Return p
        If Not (p.Length = 7 Or p.Length >= 10) Then Return p

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

    Public Function RealStringOrNothing(ByVal s As String) As String
        If String.IsNullOrEmpty(s) Then
            Return Nothing
        Else
            Return s
        End If
    End Function

End Module