Imports System.Collections.Generic
Imports Iaip.BaseForm

Module FormHandler
    Private MultiForm As Dictionary(Of String, Dictionary(Of Integer, BaseForm))
    Private SingleForm As Dictionary(Of String, BaseForm)

    Public Function OpenMultiForm(formClass As BaseForm,
                                  id As Integer,
                                  Optional parameters As Dictionary(Of FormParameter, String) = Nothing) As Form
        Return OpenMultiForm(formClass.GetType, formClass.Name, id, parameters)
    End Function

    Private Function OpenMultiForm(formType As Type,
                                   formName As String,
                                   id As Integer,
                                   Optional parameters As Dictionary(Of FormParameter, String) = Nothing) As Form

        If MultiForm Is Nothing Then MultiForm = New Dictionary(Of String, Dictionary(Of Integer, BaseForm))

        If formName Is Nothing Then formName = formType.Name

        If Not MultiForm.ContainsKey(formName) OrElse MultiForm(formName) Is Nothing Then
            MultiForm(formName) = New Dictionary(Of Integer, BaseForm)
        End If

        If Not MultiFormIsOpen(formName, id) Then
            MultiForm(formName)(id) = CType(Activator.CreateInstance(formType), BaseForm)
            MultiForm(formName)(id).ID = id
        End If

        If parameters IsNot Nothing Then MultiForm(formName)(id).Parameters = parameters
        MultiForm(formName)(id).Show()
        MultiForm(formName)(id).Activate()

        Return MultiForm(formName)(id)
    End Function

    Public Function MultiFormIsOpen(formClass As BaseForm, id As Integer) As Boolean
        Return MultiFormIsOpen(formClass.Name, id)
    End Function

    Private Function MultiFormIsOpen(formName As String, id As Integer) As Boolean
        Return (MultiForm IsNot Nothing AndAlso
            MultiForm.ContainsKey(formName) AndAlso
            MultiForm(formName) IsNot Nothing AndAlso
            MultiForm(formName).ContainsKey(id) AndAlso
            MultiForm(formName)(id) IsNot Nothing AndAlso
        Not MultiForm(formName)(id).IsDisposed)
    End Function

    Public Function OpenSingleForm(formName As String,
                                   Optional id As Integer = -1,
                                   Optional parameters As Dictionary(Of FormParameter, String) = Nothing,
                                   Optional closeFirst As Boolean = False) As Form
        Dim formType As Type = GetFormTypeByName(formName)
        Return OpenSingleForm(formType, formName, id, parameters, closeFirst)
    End Function

    Public Function OpenSingleForm(formClass As BaseForm,
                                   Optional id As Integer = -1,
                                   Optional parameters As Dictionary(Of FormParameter, String) = Nothing,
                                   Optional closeFirst As Boolean = False) As Form
        Return OpenSingleForm(formClass.GetType, formClass.Name, id, parameters, closeFirst)
    End Function

    Private Function OpenSingleForm(formType As Type,
                                    formName As String,
                                    Optional id As Integer = -1,
                                    Optional parameters As Dictionary(Of FormParameter, String) = Nothing,
                                    Optional closeFirst As Boolean = False) As Form

        If SingleForm Is Nothing Then SingleForm = New Dictionary(Of String, BaseForm)

        If formName Is Nothing Then formName = formType.Name

        If closeFirst AndAlso SingleFormIsOpen(formName) Then
            SingleForm(formName).Dispose()
            SingleForm.Remove(formName)
        End If

        If Not SingleFormIsOpen(formName) Then
            SingleForm(formName) = CType(Activator.CreateInstance(formType), BaseForm)
        End If

        If id <> -1 Then SingleForm(formName).ID = id
        If parameters IsNot Nothing Then SingleForm(formName).Parameters = parameters

        SingleForm(formName).Show()
        SingleForm(formName).Activate()

        Return SingleForm(formName)
    End Function

    Public Function SingleFormIsOpen(formClass As BaseForm) As Boolean
        Return SingleFormIsOpen(formClass.Name)
    End Function

    Private Function SingleFormIsOpen(formName As String) As Boolean
        Return (SingleForm IsNot Nothing AndAlso
            SingleForm.ContainsKey(formName) AndAlso
            SingleForm(formName) IsNot Nothing AndAlso
            Not SingleForm(formName).IsDisposed)
    End Function

    Public Function GetSingleForm(Of T As BaseForm)() As T
        If SingleForm Is Nothing Then Return Nothing
        Dim formName As String = GetType(T).Name
        If Not SingleForm.ContainsKey(formName) Then Return Nothing
        Return TryCast(SingleForm(formName), T)
    End Function

    Public Sub RemoveForm(formName As String, Optional id As Integer = -1)
        If MultiForm IsNot Nothing AndAlso MultiForm.ContainsKey(formName) AndAlso MultiForm(formName).ContainsKey(id) Then
            MultiForm(formName).Remove(id)
        ElseIf SingleForm IsNot Nothing AndAlso SingleForm.ContainsKey(formName) Then
            SingleForm.Remove(formName)
        End If
    End Sub

    Private Function GetFormTypeByName(formName As String) As Type
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

    Public Sub ShowAllForms()
        For Each f As Form In Application.OpenForms
            If f.WindowState = FormWindowState.Minimized Then
                f.WindowState = FormWindowState.Normal
            End If

            f.Show()
            f.Activate()
        Next
    End Sub

End Module