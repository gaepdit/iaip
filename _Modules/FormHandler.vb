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

        If Not MultiFormIsOpen(name, id) Then
            MultiForm(name)(id) = Activator.CreateInstance(formClass.GetType)
            MultiForm(name)(id).ID = id
        End If

        If parameters IsNot Nothing Then MultiForm(name)(id).Parameters = parameters
        MultiForm(name)(id).Show()
        MultiForm(name)(id).Activate()
    End Sub

    Public Function MultiFormIsOpen(ByVal formClass As BaseForm, ByVal id As Integer) As Boolean
        Return MultiFormIsOpen(formClass.Name, id)
    End Function

    Public Function MultiFormIsOpen(ByVal formName As String, ByVal id As Integer) As Boolean
        Return (MultiForm IsNot Nothing _
        AndAlso MultiForm.ContainsKey(formName) _
        AndAlso MultiForm(formName) IsNot Nothing _
        AndAlso MultiForm(formName).ContainsKey(id) _
        AndAlso MultiForm(formName)(id) IsNot Nothing _
        AndAlso Not MultiForm(formName)(id).IsDisposed)
    End Function

    Public Sub OpenSingleForm(ByVal formClass As BaseForm, Optional ByVal id As Integer = -1, Optional ByVal parameters As Dictionary(Of String, String) = Nothing, Optional ByVal closeFirst As Boolean = False)
        If SingleForm Is Nothing Then SingleForm = New Dictionary(Of String, BaseForm)

        Dim name As String = formClass.Name

        If closeFirst AndAlso SingleFormIsOpen(name) Then
            SingleForm(name).Dispose()
            SingleForm.Remove(name)
        End If

        If Not SingleFormIsOpen(name) Then
            SingleForm(name) = Activator.CreateInstance(formClass.GetType)
        End If

        If id <> -1 Then SingleForm(name).ID = id
        If parameters IsNot Nothing Then SingleForm(name).Parameters = parameters

        SingleForm(name).Show()
        SingleForm(name).Activate()
    End Sub

    Public Function SingleFormIsOpen(ByVal formClass As BaseForm) As Boolean
        Return SingleFormIsOpen(formClass.Name)
    End Function

    Public Function SingleFormIsOpen(ByVal formName As String) As Boolean
        Return (SingleForm IsNot Nothing _
        AndAlso SingleForm.ContainsKey(formName) _
        AndAlso SingleForm(formName) IsNot Nothing _
        AndAlso Not SingleForm(formName).IsDisposed)
    End Function

End Module
