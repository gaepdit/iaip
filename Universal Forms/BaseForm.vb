'Imports System.Xml
'Imports System.Xml.Serialization

Public Class BaseForm

#If Not Debug Then

    Private Sub BaseForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.DesignMode Then
            If TestingEnvironment Then Me.Icon = My.Resources.TestingIcon
        End If
    End Sub

#End If

#Region "Form location and dimensions"

    ' On closing the form, save the window state, location, and size
    ' Override this Sub on forms where this would not be useful
    Overridable Sub SaveFormDimensions() Handles Me.FormClosing
        Dim formSettings As XmlSerializableDictionary(Of String, String) = GetFormSettings(Me.Name)

        Dim windowState As FormWindowState = Me.WindowState
        formSettings("windowState") = windowState.ToString

        If windowState = FormWindowState.Normal Then
            Dim pointConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Point))
            formSettings("location") = pointConverter.ConvertToString(Me.Location)

            Dim sizeConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Size))
            formSettings("size") = sizeConverter.ConvertToString(Me.Size)
        End If

        SaveFormSettings(Me.Name, formSettings)
    End Sub

    Overridable Sub LoadFormDimensions() Handles Me.Load
        Dim formSettings As XmlSerializableDictionary(Of String, String) = GetFormSettings(Me.Name)

        If formSettings.ContainsKey("windowState") Then
            Me.WindowState = [Enum].Parse(GetType(FormWindowState), formSettings("windowState"))
        End If

        If formSettings.ContainsKey("location") Then
            Dim pointConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Point))
            Me.Location = pointConverter.ConvertFromString(formSettings("location"))
        End If

        If formSettings.ContainsKey("size") Then
            Dim sizeConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Size))
            Me.Size = sizeConverter.ConvertFromString(formSettings("size"))
        End If
    End Sub

#End Region

End Class