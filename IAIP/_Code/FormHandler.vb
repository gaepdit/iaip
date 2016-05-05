Imports System.Collections.Generic
Imports Iaip.BaseForm

Module FormHandler
    Public MultiForm As Dictionary(Of String, Dictionary(Of Integer, BaseForm))
    Public SingleForm As Dictionary(Of String, BaseForm)

    ' Deprecated
    'Public Function OpenMultiForm(ByVal formName As String,
    '                              ByVal id As Integer,
    '                              Optional ByVal parameters As Dictionary(Of FormParameter, String) = Nothing) As Form
    '    Dim formType As Type = GetFormTypeByName(formName)
    '    Return OpenMultiForm(formType, formName, id, parameters)
    'End Function

    Public Function OpenMultiForm(ByVal formClass As BaseForm,
                                  ByVal id As Integer,
                                  Optional ByVal parameters As Dictionary(Of FormParameter, String) = Nothing) As Form
        Return OpenMultiForm(formClass.GetType, formClass.Name, id, parameters)
    End Function

    Private Function OpenMultiForm(ByVal formType As Type,
                                   ByVal formName As String,
                                   ByVal id As Integer,
                                   Optional ByVal parameters As Dictionary(Of FormParameter, String) = Nothing) As Form

        If MultiForm Is Nothing Then MultiForm = New Dictionary(Of String, Dictionary(Of Integer, BaseForm))

        If formName Is Nothing Then formName = formType.Name

        If Not MultiForm.ContainsKey(formName) OrElse MultiForm(formName) Is Nothing Then
            MultiForm(formName) = New Dictionary(Of Integer, BaseForm)
        End If

        If Not MultiFormIsOpen(formName, id) Then
            MultiForm(formName)(id) = Activator.CreateInstance(formType)
            MultiForm(formName)(id).ID = id
        End If

        If parameters IsNot Nothing Then MultiForm(formName)(id).Parameters = parameters
        MultiForm(formName)(id).Show()
        MultiForm(formName)(id).Activate()

        Return MultiForm(formName)(id)
    End Function

    Public Function MultiFormIsOpen(ByVal formClass As BaseForm, ByVal id As Integer) As Boolean
        Return MultiFormIsOpen(formClass.Name, id)
    End Function

    Private Function MultiFormIsOpen(ByVal formName As String, ByVal id As Integer) As Boolean
        Return (MultiForm IsNot Nothing AndAlso
            MultiForm.ContainsKey(formName) AndAlso
            MultiForm(formName) IsNot Nothing AndAlso
            MultiForm(formName).ContainsKey(id) AndAlso
            MultiForm(formName)(id) IsNot Nothing AndAlso
        Not MultiForm(formName)(id).IsDisposed)
    End Function

    Public Function OpenSingleForm(ByVal formName As String,
                                   Optional ByVal id As Integer = -1,
                                   Optional ByVal parameters As Dictionary(Of FormParameter, String) = Nothing,
                                   Optional ByVal closeFirst As Boolean = False) As Form
        Dim formType As Type = GetFormTypeByName(formName)
        Return OpenSingleForm(formType, formName, id, parameters, closeFirst)
    End Function

    Public Function OpenSingleForm(ByVal formClass As BaseForm,
                                   Optional ByVal id As Integer = -1,
                                   Optional ByVal parameters As Dictionary(Of FormParameter, String) = Nothing,
                                   Optional ByVal closeFirst As Boolean = False) As Form
        Return OpenSingleForm(formClass.GetType, formClass.Name, id, parameters, closeFirst)
    End Function

    Private Function OpenSingleForm(ByVal formType As Type,
                                    ByVal formName As String,
                                    Optional ByVal id As Integer = -1,
                                    Optional ByVal parameters As Dictionary(Of FormParameter, String) = Nothing,
                                    Optional ByVal closeFirst As Boolean = False) As Form

        If SingleForm Is Nothing Then SingleForm = New Dictionary(Of String, BaseForm)

        If formName Is Nothing Then formName = formType.Name

        If closeFirst AndAlso SingleFormIsOpen(formName) Then
            SingleForm(formName).Dispose()
            SingleForm.Remove(formName)
        End If

        If Not SingleFormIsOpen(formName) Then
            SingleForm(formName) = Activator.CreateInstance(formType)
        End If

        If id <> -1 Then SingleForm(formName).ID = id
        If parameters IsNot Nothing Then SingleForm(formName).Parameters = parameters

        SingleForm(formName).Show()
        SingleForm(formName).Activate()

        Return SingleForm(formName)
    End Function

    Public Function SingleFormIsOpen(ByVal formClass As BaseForm) As Boolean
        Return SingleFormIsOpen(formClass.Name)
    End Function

    Private Function SingleFormIsOpen(ByVal formName As String) As Boolean
        Return (SingleForm IsNot Nothing AndAlso
            SingleForm.ContainsKey(formName) AndAlso
            SingleForm(formName) IsNot Nothing AndAlso
            Not SingleForm(formName).IsDisposed)
    End Function

    Private Function GetFormTypeByName(ByVal formName As String) As Type
        ' See: http://vbcity.com/forums/t/32930.aspx
        ' First try: in case the full namespace has been provided
        Dim formType As Type = Type.GetType(formName, False)

        ' If not found prepend default namespace
        If formType Is Nothing Then
            Dim Fullname As String = APP_ROOT_NAMESPACE & "." & formName
            formType = Type.GetType(Fullname, True, True)
        End If

        Return formType
    End Function

End Module
