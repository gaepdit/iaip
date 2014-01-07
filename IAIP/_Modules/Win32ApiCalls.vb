Module Win32ApiCalls

    Public Declare Function SHGetFolderPath Lib "shfolder.dll" Alias "SHGetFolderPathA" _
    (ByVal hwndOwner As IntPtr, ByVal nFolder As Integer, ByVal hToken As IntPtr, _
     ByVal dwFlags As Integer, ByVal lpszPath As System.Text.StringBuilder) As Integer
    Public Const MAX_PATH As Integer = 260
    Public Enum CSIDL
        COMMON_DESKTOPDIRECTORY = &H19
        'USER_PROGRAMS = &H2
        COMMON_PROGRAMS = &H17
    End Enum

    Public Declare Function SendMessage Lib "user32" Alias _
       "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As _
       Integer, ByVal wParam As Integer, ByRef lParam As _
       Integer) As Integer
    Public Const EM_SETTABSTOPS As Integer = &HCB

End Module
