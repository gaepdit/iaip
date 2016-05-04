Imports System.Collections.Generic
Imports Microsoft.ApplicationInsights.DataContracts

Public Class BaseForm

#Region "Properties"

    Public Property ID() As Integer
    Public Property Parameters() As Dictionary(Of FormParameter, String)
    Private whenOpened As Date = Date.Now
    
    Public Enum FormParameter
        AirsNumber
        FeeYear
        TrackingNumber
        EnforcementId
        FacilityName
        Key
    End Enum


#End Region

#Region "Form events"

    Private Sub BaseForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

#If DEBUG Then
        Me.Icon = My.Resources.DevIcon
#ElseIf UAT Then
        Me.Icon = My.Resources.UatIcon
#End If

        LoadThisFormSettings()
    End Sub

    Private Sub BaseForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        LogPageView()

        SaveThisFormSettings()

        If MultiForm IsNot Nothing AndAlso
        MultiForm.ContainsKey(Me.Name) AndAlso
        MultiForm(Me.Name).ContainsKey(Me.ID) Then
            MultiForm(Me.Name).Remove(Me.ID)
        ElseIf SingleForm IsNot Nothing AndAlso
        SingleForm.ContainsKey(Me.Name) Then
            SingleForm.Remove(Me.Name)
        End If
    End Sub

    Private Sub LogPageView()
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipForms, Me.Name, Date.Now - whenOpened)
        monitor.TrackFeature("Forms." & Me.Name)
    End Sub

#End Region

#Region "Form settings - location and dimensions"

    ''' <summary>
    ''' On closing a form, save the window state, location, and size
    ''' </summary>
    ''' <remarks>Override this Sub on individual forms as needed to save different settings</remarks>
    Overridable Sub SaveThisFormSettings()
        Dim formSettings As Dictionary(Of String, String) = GetFormSettings(Me.Name)

        If Me.MaximizeBox AndAlso Me.WindowState <> FormWindowState.Minimized Then
            ' Don't save WindowState if form is minimized (forms get lost that way)
            formSettings(FormSetting.WindowState.ToString) = Me.WindowState.ToString
        End If

        If Me.WindowState = FormWindowState.Normal AndAlso Me.SizeGripStyle <> SizeGripStyle.Hide Then
            Dim pointConverter As System.ComponentModel.TypeConverter =
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Point))
            formSettings(FormSetting.Location.ToString) = pointConverter.ConvertToString(Me.Location)

            Dim sizeConverter As System.ComponentModel.TypeConverter =
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Size))
            formSettings(FormSetting.Size.ToString) = sizeConverter.ConvertToString(Me.Size)
        End If

        If formSettings.Count > 0 Then SaveFormSettings(Me.Name, formSettings)
    End Sub

    ''' <summary>
    ''' On loading a form, retrieve and use the forms previously saved settings
    ''' </summary>
    ''' <remarks>Should be overridden if Sub SaveThisFormSettings is overridden</remarks>
    Overridable Sub LoadThisFormSettings()
        Dim thisFormSettings As Dictionary(Of String, String) = GetFormSettings(Me.Name)

        If thisFormSettings.ContainsKey(FormSetting.WindowState.ToString) Then
            Dim ws As FormWindowState = [Enum].Parse(GetType(FormWindowState), thisFormSettings(FormSetting.WindowState.ToString))
            ' Don't restore WindowState to be minimized (forms get lost that way)
            If ws <> FormWindowState.Minimized Then Me.WindowState = ws
        End If

        If thisFormSettings.ContainsKey(FormSetting.Location.ToString) Then
            Dim pointConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Point))
            Dim p As Point = pointConverter.ConvertFromString(thisFormSettings(FormSetting.Location.ToString))
            ' Don't move form off screen
            If Not PointIsOnAConnectedScreen(p) Then p = New Point(0, 0)
            Me.Location = p
        End If

        If thisFormSettings.ContainsKey(FormSetting.Size.ToString) Then
            Dim sizeConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Size))
            Dim s As Size = sizeConverter.ConvertFromString(thisFormSettings(FormSetting.Size.ToString))
            s.Width = Math.Max(s.Width, Me.MinimumSize.Width)
            s.Height = Math.Max(s.Height, Me.MinimumSize.Height)
            Me.Size = s
        End If
    End Sub

    ''' <summary>
    ''' Determines whether a point is located within a connected screen
    ''' </summary>
    ''' <param name="pt">The System.Drawing.Point to test</param>
    ''' <returns>True if the Point is located within the bounds of a connected screen; otherwise, false.</returns>
    Private Function PointIsOnAConnectedScreen(ByVal pt As Point) As Boolean
        For Each s As Screen In Screen.AllScreens
            If s.Bounds.Contains(pt) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Determines whether a form is reasonably located within a screen
    ''' </summary>
    ''' <param name="pt">The upper-left coordinates of the form to test (i.e., Form.Location)</param>
    ''' <returns>True if the form is reasonably located within the bounds of a connected screen; otherwise, false.</returns>
    ''' <remarks>Analyzes upper-left coordinates of form and also a point some reasonable distance 
    ''' from the upper-left, based on the minimum form width and height (set in subMain)</remarks>
    Private Function FormIsOnAConnectedScreen(ByVal pt As Point) As Boolean
        ' First, check if upper-left corner of form is on a screen
        ' Then, check if a point some reasonable distance from upper-left corner of form is on a screen
        If PointIsOnAConnectedScreen(pt) AndAlso _
        PointIsOnAConnectedScreen(New Point(pt.X + Me.MinimumSize.Width / 2, pt.Y + Me.MinimumSize.Height / 2)) Then
            Return True
        End If

        Return False
    End Function

#End Region

End Class