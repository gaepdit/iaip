Module Win32ApiCalls

    Public Declare Function SendMessage Lib "user32" Alias _
       "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As _
       Integer, ByVal wParam As Integer, ByRef lParam As _
       Integer) As Integer
    Public Const EM_SETTABSTOPS As Integer = &HCB

End Module
