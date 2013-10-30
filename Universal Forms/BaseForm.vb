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
        
        Dim formSettings As New SerializableDictionary(Of String, String)
        formSettings = GetDictionarySetting(Me.Name)

        Dim windowState As FormWindowState = Me.WindowState
        formSettings("windowState") = windowState

        If windowState = FormWindowState.Normal Then
            Dim pointConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Point))
            formSettings("location") = pointConverter.ConvertToString(Me.Location)

            Dim sizeConverter As System.ComponentModel.TypeConverter = _
                System.ComponentModel.TypeDescriptor.GetConverter(GetType(Size))
            formSettings("size") = sizeConverter.ConvertToString(Me.Size)
        End If

        SaveDictionarySetting(Me.Name, formSettings)
    End Sub

    Overridable Sub LoadFormDimensions() Handles Me.Load
        Dim formSettings As New SerializableDictionary(Of String, String)
        formSettings = GetDictionarySetting(Me.Name)
        Try
            If formSettings IsNot Nothing Then
                Me.WindowState = [Enum].Parse(GetType(FormWindowState), formSettings("windowState"))
                Dim pointConverter As System.ComponentModel.TypeConverter = _
                    System.ComponentModel.TypeDescriptor.GetConverter(GetType(Point))
                Me.Location = pointConverter.ConvertFromString(formSettings("location"))
                Dim sizeConverter As System.ComponentModel.TypeConverter = _
                    System.ComponentModel.TypeDescriptor.GetConverter(GetType(Size))
                Me.Size = sizeConverter.ConvertFromString(formSettings("size"))
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class