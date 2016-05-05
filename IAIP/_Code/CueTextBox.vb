''' <summary>
''' Textbox with cue text
''' </summary>
''' <remarks>See http://stackoverflow.com/a/5450496/212978 </remarks>
Friend Class CueTextBox
    Inherits TextBox

    Private NotInheritable Class NativeMethods
        Private Sub New()
        End Sub
        Private Const ECM_FIRST As UInteger = &H1500
        Friend Const EM_SETCUEBANNER As UInteger = ECM_FIRST + 1

        <Runtime.InteropServices.DllImport("user32.dll", CharSet:=Runtime.InteropServices.CharSet.Unicode)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInt32, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr
        End Function
    End Class

    Private _cue As String

    Public Property Cue() As String
        Get
            Return _cue
        End Get
        Set(ByVal value As String)
            _cue = value
            UpdateCue()
        End Set
    End Property

    Private Sub UpdateCue()
        If IsHandleCreated AndAlso Cue IsNot Nothing Then
            NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCUEBANNER, New IntPtr(1), Cue)
        End If
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)
        UpdateCue()
    End Sub

End Class
