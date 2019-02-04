Friend Module ControlHelpers

#Region " Enable/disable "

    ''' <summary>
    ''' Disables Controls in an array by setting their .Enabled properties to False
    ''' </summary>
    ''' <param name="controls">An array of Controls to disable</param>
    Public Sub DisableControls(controls As Control())
        For Each control As Control In controls
            control.Enabled = False
        Next
    End Sub

    ''' <summary>
    ''' Enables all Controls in an array by setting their .Enabled property to True
    ''' </summary>
    ''' <param name="controls">An array of Controls to enable</param>
    Public Sub EnableControls(controls As Control())
        For Each control As Control In controls
            control.Enabled = True
        Next
    End Sub

#End Region

#Region " Hide/show "

    ''' <summary>
    ''' Hides Controls in an array by setting their .Visible property to False
    ''' </summary>
    ''' <param name="controls">An array of Controls to hide</param>
    Public Sub HideControls(controls As Control())
        For Each control As Control In controls
            control.Visible = False
        Next
    End Sub

    ''' <summary>
    ''' Shows all Controls in an array by setting their .Visible property to True
    ''' </summary>
    ''' <param name="controls">An array of Controls to show</param>
    Public Sub ShowControls(controls As Control())
        For Each control As Control In controls
            control.Visible = True
        Next
    End Sub

#End Region

#Region " Prevent/allow "

    ''' <summary>
    ''' Prevents use of Controls in an array by setting their .Enabled or .ReadOnly properties to False, depending on the Object type
    ''' </summary>
    ''' <param name="controls">An array of Controls to prevent use of</param>
    ''' <remarks>Does not disable textboxes so they can still be easily read or selected.</remarks>
    Public Sub PreventControls(controls As Control())
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
    ''' Allows use of Controls in an array by setting their .Enabled or .ReadOnly properties to True, depending on the Object type
    ''' </summary>
    ''' <param name="controls">An array of Controls to allow use of</param>
    ''' <remarks>Opposite of the PreventControls procedure</remarks>
    Public Sub AllowControls(controls As Control())
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

End Module
