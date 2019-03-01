Imports System.ComponentModel
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Public Module ControlExtensions

    Private ReadOnly onValidating As MethodInfo = GetType(Control).GetMethod("OnValidating", BindingFlags.Instance Or BindingFlags.NonPublic)
    Private ReadOnly onValidated As MethodInfo = GetType(Control).GetMethod("OnValidated", BindingFlags.Instance Or BindingFlags.NonPublic)

    <Extension>
    Public Function Validate(control As Control) As Boolean
        Dim e As New CancelEventArgs()

        onValidating.Invoke(control, New Object() {e})
        If e.Cancel Then
            Return False
        End If

        onValidated.Invoke(control, New Object() {EventArgs.Empty})

        Return True
    End Function

End Module
