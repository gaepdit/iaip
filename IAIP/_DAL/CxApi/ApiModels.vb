Imports System.Text.Json

' API request models
Friend Module ApiRequestModels

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

' API response models
Friend Module ApiResponseModels

    Public Class IaipAuthResult
        Public Property Success As Boolean
        Public Property Message As String
        Public Property IaipConfig As AppConfig
        
        Public Shared Function ParseAuthResult(result As String) As IaipAuthResult
            Return JsonSerializer.Deserialize(Of IaipAuthResult)(result, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})
        End Function
    End Class

    Public Class IaipStatusResult
        Public Property Enabled As Boolean
        Public Property MinimumVersion As String

        Public Shared Function ParseStatusResult(jsonValue As String) As IaipStatusResult
            Return JsonSerializer.Deserialize(Of IaipStatusResult)(jsonValue, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})
        End Function
    End Class

End Module
