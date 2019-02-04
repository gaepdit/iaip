Imports System.Collections.Generic
Imports System.ComponentModel

Public Class BaseForm

#Region "Properties"

    Public Property ID() As Integer
    Public Property Parameters() As Dictionary(Of FormParameter, String)

    Public Enum FormParameter
        AirsNumber
        FeeYear
        TrackingNumber
        EnforcementId
        FacilityName
        Key
        ReferenceNumber
        AppNumber
    End Enum

#End Region

#Region "Form events"

    Protected Overrides Sub OnLoad(e As EventArgs)

#If DEBUG Then
        Icon = My.Resources.DevIcon
#ElseIf UAT Then
        Icon = My.Resources.UatIcon
#End If

        LoadThisFormSettings()

        MyBase.OnLoad(e)
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        SaveThisFormSettings()

        If MultiForm IsNot Nothing AndAlso MultiForm.ContainsKey(Name) AndAlso MultiForm(Name).ContainsKey(ID) Then
            MultiForm(Name).Remove(ID)
        ElseIf SingleForm IsNot Nothing AndAlso SingleForm.ContainsKey(Name) Then
            SingleForm.Remove(Name)
        End If

        MyBase.OnFormClosed(e)
    End Sub

#End Region

#Region "Form settings - location and dimensions"

    ''' <summary>
    ''' On closing a form, save the window state, location, and size
    ''' </summary>
    ''' <remarks>Override this Sub on individual forms as needed to save different settings</remarks>
    Overridable Sub SaveThisFormSettings()
        Dim formSettings As Dictionary(Of String, String) = GetFormSettings(Name)

        If MaximizeBox AndAlso WindowState <> FormWindowState.Minimized Then
            ' Don't save WindowState if form is minimized (forms get lost that way)
            formSettings(FormSetting.WindowState.ToString) = WindowState.ToString
        End If

        If WindowState = FormWindowState.Normal AndAlso SizeGripStyle <> SizeGripStyle.Hide Then
            Dim pointConverter As TypeConverter = TypeDescriptor.GetConverter(GetType(Point))
            formSettings(FormSetting.Location.ToString) = pointConverter.ConvertToString(Location)

            Dim sizeConverter As TypeConverter = TypeDescriptor.GetConverter(GetType(Size))
            formSettings(FormSetting.Size.ToString) = sizeConverter.ConvertToString(Size)
        End If

        If formSettings.Count > 0 Then SaveFormSettings(Name, formSettings)
    End Sub

    ''' <summary>
    ''' On loading a form, retrieve and use the forms previously saved settings
    ''' </summary>
    ''' <remarks>Should be overridden if Sub SaveThisFormSettings is overridden</remarks>
    Overridable Sub LoadThisFormSettings()
        Dim thisFormSettings As Dictionary(Of String, String) = GetFormSettings(Name)

        If thisFormSettings.ContainsKey(FormSetting.WindowState.ToString) Then
            Dim ws As FormWindowState = CType([Enum].Parse(GetType(FormWindowState), thisFormSettings(FormSetting.WindowState.ToString)), FormWindowState)

            ' Don't restore WindowState to be minimized (forms get lost that way)
            If ws <> FormWindowState.Minimized Then
                WindowState = ws
            End If
        End If

        If thisFormSettings.ContainsKey(FormSetting.Location.ToString) Then
            Dim pointConverter As TypeConverter = TypeDescriptor.GetConverter(GetType(Point))
            Dim p As Point = CType(pointConverter.ConvertFromString(thisFormSettings(FormSetting.Location.ToString)), Point)

            ' Don't move form off screen
            If Not PointIsOnAConnectedScreen(p) Then
                p = New Point(0, 0)
            End If

            Location = p
        End If

        If thisFormSettings.ContainsKey(FormSetting.Size.ToString) Then
            Dim sizeConverter As TypeConverter = TypeDescriptor.GetConverter(GetType(Size))
            Dim s As Size = CType(sizeConverter.ConvertFromString(thisFormSettings(FormSetting.Size.ToString)), Size)

            s.Width = Math.Max(s.Width, MinimumSize.Width)
            s.Height = Math.Max(s.Height, MinimumSize.Height)
            Size = s
        End If
    End Sub

    ''' <summary>
    ''' Determines whether a point is located within a connected screen
    ''' </summary>
    ''' <param name="pt">The System.Drawing.Point to test</param>
    ''' <returns>True if the Point is located within the bounds of a connected screen; otherwise, false.</returns>
    Private Function PointIsOnAConnectedScreen(pt As Point) As Boolean
        For Each s As Screen In Screen.AllScreens
            If s.Bounds.Contains(pt) Then
                Return True
            End If
        Next

        Return False
    End Function

#End Region

End Class