Public Module FormControlsProcedures

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
    ''' Disables Controls in an array by setting their .Enabled properties to False
    ''' </summary>
    ''' <param name="controls">An array of Controls to disable</param>
    Public Sub DisableControls(ByVal controls As Control())
        For Each control As Control In controls
            control.Enabled = False
        Next
    End Sub

    ''' <summary>
    ''' Prevents use of Controls in an array by setting their .Enabled or .ReadOnly properties to False, depending on the Object type
    ''' </summary>
    ''' <param name="controls">An array of Controls to prevent use of</param>
    ''' <remarks>Does not disable textboxes so they can still be easily read or selected.</remarks>
    Public Sub PreventControls(ByVal controls As Control())
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
    ''' Enables all Controls in an array by setting their .Enabled properties to True
    ''' </summary>
    ''' <param name="controls">An array of Controls to enable</param>
    Public Sub EnableControls(ByVal controls As Control())
        For Each control As Control In controls
            control.Enabled = True
        Next
    End Sub

    ''' <summary>
    ''' Allows use of Controls in an array by setting their .Enabled or .ReadOnly properties to True, depending on the Object type
    ''' </summary>
    ''' <param name="controls">An array of Controls to allow use of</param>
    ''' <remarks>Opposite of the PreventControls procedure</remarks>
    Public Sub AllowControls(ByVal controls As Control())
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

End Module
