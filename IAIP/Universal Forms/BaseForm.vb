Imports System.Collections.Generic

Public Class BaseForm

#Region "Properties"

    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _parameters As Dictionary(Of String, String)
    Public Property Parameters() As Dictionary(Of String, String)
        Get
            Return _parameters
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _parameters = value
        End Set
    End Property

#End Region

#Region "Form events"

    Private Sub BaseForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            If MultiForm IsNot Nothing AndAlso _
            MultiForm.ContainsKey(Me.Name) AndAlso _
            MultiForm(Me.Name).ContainsKey(Me.ID) Then

                MultiForm(Me.Name).Remove(Me.ID)

            ElseIf SingleForm IsNot Nothing AndAlso _
            SingleForm.ContainsKey(Me.Name) Then

                SingleForm.Remove(Me.Name)

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub BaseForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

#If Not Debug Then
        ' TestingEnvironment variable is not available in design mode
        If Not Me.DesignMode Then
            If TestingEnvironment Then Me.Icon = My.Resources.TestingIcon
        End If
#End If

        LoadThisFormSettings()
    End Sub

    Private Sub BaseForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SaveThisFormSettings()
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

        If Me.WindowState = FormWindowState.Normal AndAlso Me.SizeGripStyle <> Windows.Forms.SizeGripStyle.Hide Then
            Dim pointConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Point))
            formSettings(FormSetting.Location.ToString) = pointConverter.ConvertToString(Me.Location)

            Dim sizeConverter As System.ComponentModel.TypeConverter = _
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