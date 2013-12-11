Imports System.Collections.Generic

Public Class BaseForm

#Region "Properties"

    Private _id As String
    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
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
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub BaseForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

#If Not Debug Then
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

    ' On closing the form, save the window state, location, and size
    ' Override this Sub on individual forms if you need to save different settings
    Overridable Sub SaveThisFormSettings()
        Dim formSettings As Dictionary(Of String, String) = GetFormSettings(Me.Name)

        If Me.MaximizeBox OrElse Me.MinimizeBox Then
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

    Overridable Sub LoadThisFormSettings()
        Dim thisFormSettings As Dictionary(Of String, String) = GetFormSettings(Me.Name)

        If thisFormSettings.ContainsKey(FormSetting.WindowState.ToString) Then
            Me.WindowState = [Enum].Parse(GetType(FormWindowState), thisFormSettings(FormSetting.WindowState.ToString))
        End If

        If thisFormSettings.ContainsKey(FormSetting.Location.ToString) Then
            Dim pointConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Point))
            Me.Location = pointConverter.ConvertFromString(thisFormSettings(FormSetting.Location.ToString))
        End If

        If thisFormSettings.ContainsKey(FormSetting.Size.ToString) Then
            Dim sizeConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Size))
            Me.Size = sizeConverter.ConvertFromString(thisFormSettings(FormSetting.Size.ToString))
        End If
    End Sub

#End Region

End Class