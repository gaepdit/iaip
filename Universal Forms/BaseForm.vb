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
        Dim windowState As FormWindowState = Me.WindowState
        Dim location As Point = Me.Location
        Dim size As Size = Me.Size

        Dim formSettings As New SerializableDictionary(Of String, String)
        formSettings("windowState") = windowState.ToString
        formSettings("location") = location.ToString
        formSettings("size") = size.ToString

        SaveDictionarySetting(Me.Name, formSettings)
    End Sub

    Overridable Sub LoadFormDimensions() Handles Me.Load
        Dim formSettings As New SerializableDictionary(Of String, String)
        formSettings = GetDictionarySetting(Me.Name)

        If formSettings IsNot Nothing Then
            Me.WindowState = [Enum].Parse(GetType(FormWindowState), formSettings("windowState"))
            'Me.Location = dimensions("location")
            'Me.Size = dimensions("size")
        End If
    End Sub

#End Region

End Class