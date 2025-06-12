Namespace ApiCalls.IaipCx
    ' API request models
    Friend Module CxApiRequestModels

        Public Class LoginCredentials
            Public Property Username As String
            Public Property Password As String
        End Class

        Public Class SessionCredentials
            Public Property UserId As Integer
            Public Property Token As String
            Public Property MachineName As String
            Public Property WindowsUserName As String
            Public Property WindowsDomainName As String
        End Class

        Public Class UsernameRequest
            Public Property Email As String
        End Class

        Public Class PasswordUpdate
            Public Property Username As String
            Public Property OldPassword As String
            Public Property NewPassword As String
        End Class

        Public Class PasswordResetRequest
            Public Property Username As String
        End Class

        Public Class PasswordReset
            Public Property Username As String
            Public Property NewPassword As String
            Public Property ResetToken As String
        End Class

    End Module
End Namespace
