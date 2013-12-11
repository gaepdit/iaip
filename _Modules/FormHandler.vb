Imports System.Collections.Generic

Module FormHandler
    Public MultiForm As Dictionary(Of String, Dictionary(Of Integer, BaseForm))

    Public Sub OpenMultiForm(ByVal formClass As BaseForm, ByVal id As Integer)
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
            MultiForm(name)(id).Show()
        Else
            MultiForm(name)(id).Activate()
        End If
    End Sub

End Module
