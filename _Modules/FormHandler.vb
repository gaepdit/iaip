Imports System.Collections.Generic

Module FormHandler
    Public MultiForm As Dictionary(Of String, Dictionary(Of Integer, BaseForm))
    Public SingleForm As Dictionary(Of String, BaseForm)

    Public Sub OpenMultiForm(ByVal formClass As BaseForm, ByVal id As Integer, Optional ByVal parameters As Dictionary(Of String, String) = Nothing)
        If MultiForm Is Nothing Then MultiForm = New Dictionary(Of String, Dictionary(Of Integer, BaseForm))

        Dim name As String = formClass.Name

        If Not MultiForm.ContainsKey(name) OrElse MultiForm(name) Is Nothing Then
            MultiForm(name) = New Dictionary(Of Integer, BaseForm)
        End If

        If Not MultiForm(name).ContainsKey(id) _
            OrElse MultiForm(name)(id) Is Nothing _
            OrElse MultiForm(name)(id).IsDisposed Then

            MultiForm(name)(id) = Activator.CreateInstance(formClass.GetType)
            MultiForm(name)(id).ID = id
            If parameters IsNot Nothing Then MultiForm(name)(id).Parameters = parameters
            MultiForm(name)(id).Show()
        End If

        MultiForm(name)(id).Activate()
    End Sub

    Public Sub OpenForm(ByVal formClass As BaseForm, Optional ByVal id As Integer = -1, Optional ByVal parameters As Dictionary(Of String, String) = Nothing)
        If SingleForm Is Nothing Then SingleForm = New Dictionary(Of String, BaseForm)

        Dim name As String = formClass.Name

        If Not SingleForm.ContainsKey(name) _
            OrElse SingleForm(name) Is Nothing _
            OrElse SingleForm(name).IsDisposed Then

            SingleForm(name) = Activator.CreateInstance(formClass.GetType)
            SingleForm(name).Show()
        End If

        If id <> -1 Then SingleForm(name).ID = id
        If parameters IsNot Nothing Then MultiForm(name)(id).Parameters = parameters
        SingleForm(name).Activate()
    End Sub

End Module
