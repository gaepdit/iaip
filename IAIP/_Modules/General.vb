Imports System.Collections.Generic

Module General

#Region " String functions "

    ''' <summary>
    ''' Implodes a String array to a single string, concatenating the items using the separator, and ignoring null or empty string items
    ''' </summary>
    ''' <param name="separator">The separator string to include between each item</param>
    ''' <param name="items">An array of strings to concatenate</param>
    ''' <returns>A concatenated string separated by the specified separator. Null or empty strings are ignored.</returns>
    ''' <remarks></remarks>
    Public Function ConcatNonEmptyStrings(ByVal separator As String, ByVal items() As String) As String
        Return String.Join(separator, Array.FindAll(items, Function(s) Not String.IsNullOrEmpty(s)))
    End Function

    ''' <summary>
    ''' Takes a string of either 7 or 10 digits and formats it as a telephone number.
    ''' </summary>
    ''' <param name="p">The phone number string to format.</param>
    ''' <param name="formal">Whether to format the telephone number as "(999) 999-9999" (if True) or "999-999-9999" (if False).</param>
    ''' <returns>A formatted telephone number.</returns>
    ''' <remarks></remarks>
    Public Function FormatStringAsPhoneNumber(ByVal p As String, Optional ByVal formal As Boolean = True) As String
        If p Is Nothing Then Return p
        If Not System.Text.RegularExpressions.Regex.IsMatch(p, "^[0-9 ]+$") Then Return p
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

#End Region

#Region " Date functions "

    ''' <summary>
    ''' Converts a Date object to Nothing if date is equal to #7/4/1776#. 
    ''' </summary>
    ''' <param name="d">The Date to normalize.</param>
    ''' <returns>A nullable Date that has been normalized.</returns>
    ''' <remarks></remarks>
    Public Function NormalizeDate(ByVal d As Date?) As Date?
        ' Converts a date to Nothing if date is equal to #7/4/1776#

        If d.Equals(CType(Nothing, Date)) Then Return d
        If Not IsDate(d) Then Return Nothing
        If d.Equals(New Date(1776, 7, 4)) Then Return Nothing
        Return d
    End Function

    ''' <summary>
    ''' Determines whether any of a list of nullable dates contains a value
    ''' </summary>
    ''' <param name="dates">A list of nullable dates</param>
    ''' <returns>Returns True if any of the nullable dates has a value; otherwise returns False.</returns>
    Public Function AnyOfTheseDatesHasValue(dates As List(Of Date?)) As Boolean
        For Each d As Date? In dates
            If d.HasValue Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region " Form Control procedures "

    ''' <summary>
    ''' Disables and hides a Control by setting its .Enabled and .Visible properties to False
    ''' </summary>
    ''' <param name="control">The Control to disable and hide</param>
    Public Sub DisableAndHide(ByVal control As Control)
        If control IsNot Nothing Then
            With control
                .Enabled = False
                .Visible = False
            End With
        End If
    End Sub

    ''' <summary>
    ''' Disables and hides all Controls in an array by setting their .Enabled and .Visible properties to False
    ''' </summary>
    ''' <param name="controls">An array of Controls to disable and hide</param>
    Public Sub DisableAndHide(ByVal controls As Control())
        For Each control As Control In controls
            DisableAndHide(control)
        Next
    End Sub

    ''' <summary>
    ''' Disables all Controls in an array by setting their .Enabled or .ReadOnly properties to False, depending on the Object type
    ''' </summary>
    ''' <param name="controls">An array of Controls to disable</param>
    Public Sub DisableControls(ByVal controls As Control())
        For Each control As Control In controls
            Dim type As Type = control.GetType
            If type Is GetType(TextBox) Then
                CType(control, TextBox).ReadOnly = True
            ElseIf type Is GetType(MaskedTextBox) Then
                CType(control, MaskedTextBox).ReadOnly = True
            Else
                control.Enabled = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Enables and shows a Control by setting its .Enabled and .Visible properties to True
    ''' </summary>
    ''' <param name="control">The Control to enable and show</param>
    Public Sub EnableAndShow(ByVal control As Control)
        If control IsNot Nothing Then
            With control
                .Enabled = True
                .Visible = True
            End With
        End If
    End Sub

    ''' <summary>
    ''' Enables and shows all Controls in an array by setting their .Enabled and .Visible properties to True
    ''' </summary>
    ''' <param name="controls">An array of Controls to enable and show</param>
    Public Sub EnableAndShow(ByVal controls As Control())
        For Each control As Control In controls
            EnableAndShow(control)
        Next
    End Sub

    ''' <summary>
    ''' Enables all Controls in an array by setting their .Enabled or .ReadOnly properties to True, depending on the Object type
    ''' </summary>
    ''' <param name="controls">An array of Controls to enable</param>
    Public Sub EnableControls(ByVal controls As Control())
        For Each control As Control In controls
            Dim type As Type = control.GetType
            If type Is GetType(TextBox) Then
                CType(control, TextBox).ReadOnly = False
            ElseIf type Is GetType(MaskedTextBox) Then
                CType(control, MaskedTextBox).ReadOnly = False
            Else
                control.Enabled = True
            End If
        Next
    End Sub

#End Region

#Region " MenuItem procedures "

    ''' <summary>
    ''' Disables and hides a MenuItem by setting its .Enabled and .Visible properties to False
    ''' </summary>
    ''' <param name="menuItem">The MenuItem to disable and hide</param>
    Public Sub DisableAndHide(ByVal menuItem As MenuItem)
        If menuItem IsNot Nothing Then
            With menuItem
                .Enabled = False
                .Visible = False
            End With
        End If
    End Sub

    '' Not currently used, but may be useful in the future
    ' ''' <summary>
    ' ''' Disables and hides all MenuItems in an array by setting their .Enabled and .Visible properties to False
    ' ''' </summary>
    ' ''' <param name="menuItems">An array of controls to disable and hide</param>
    'Public Sub DisableAndHide(ByVal menuItems As MenuItem())
    '    For Each menuItem As MenuItem In menuItems
    '        DisableAndHide(menuItem)
    '    Next
    'End Sub

    ''' <summary>
    ''' Enables and shows a MenuItem by setting its .Enabled and .Visible properties to True
    ''' </summary>
    ''' <param name="menuItem">The menuItem to enable and show</param>
    Public Sub EnableAndShow(ByVal menuItem As MenuItem)
        If menuItem IsNot Nothing Then
            With menuItem
                .Enabled = True
                .Visible = True
            End With
        End If
    End Sub

    '' Not currently used, but may be useful in the future
    ' ''' <summary>
    ' ''' Enables and shows all MenuItems in an array by setting their .Enabled and .Visible properties to True
    ' ''' </summary>
    ' ''' <param name="menuItems">An array of controls to enable and show</param>
    'Public Sub EnableAndShow(ByVal menuItems As MenuItem())
    '    For Each menuItem As MenuItem In menuItems
    '        EnableAndShow(menuItem)
    '    Next
    'End Sub

#End Region

End Module
