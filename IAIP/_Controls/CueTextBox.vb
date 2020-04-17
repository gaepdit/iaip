Imports System.ComponentModel

''' <summary>
''' Textbox with cue text
''' </summary>
''' <remarks>See https://stackoverflow.com/a/5450496/212978 </remarks>
Public Class CueTextBox
    Inherits TextBox

    Private NotInheritable Class NativeMethods
        Private Sub New()
        End Sub

        Private Const ECM_FIRST As UInteger = &H1500
        Friend Const EM_SETCUEBANNER As UInteger = ECM_FIRST + 1

        <CodeAnalysis.SuppressMessage("Critical Code Smell", "S1186:Methods should not be empty")>
        <Runtime.InteropServices.DllImport("user32.dll", CharSet:=Runtime.InteropServices.CharSet.Unicode)>
        Public Shared Function SendMessage(hWnd As IntPtr, Msg As UInt32, wParam As IntPtr, lParam As String) As IntPtr
        End Function
    End Class

    Private _cue As String

    <Category("Appearance"), Description("Specifies the placeholder text to display in the TextBox.")>
    Public Overridable Property Cue() As String
        Get
            Return _cue
        End Get
        Set(value As String)
            _cue = value
            UpdateCue()
        End Set
    End Property

    Private Sub UpdateCue()
        If IsHandleCreated AndAlso Cue IsNot Nothing Then
            NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCUEBANNER, New IntPtr(1), Cue)
        End If
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        UpdateCue()
    End Sub

End Class
