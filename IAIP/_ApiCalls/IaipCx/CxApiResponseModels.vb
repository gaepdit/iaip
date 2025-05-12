Imports System.Text.Json
Imports Iaip.ApiCalls.ApiUtils

Namespace ApiCalls.IaipCx
    ' API response models
    Friend Module CxApiResponseModels

        Public Class IaipAuthResult
            Public Property Success As Boolean
            Public Property Message As String
            Public Property IaipConfig As AppConfig

            Public Shared Function ParseAuthResult(jsonValue As String) As IaipAuthResult
                Return JsonSerializer.Deserialize(Of IaipAuthResult)(jsonValue, JsonOptions)
            End Function
        End Class

        Public Class IaipStatusResult
            Public Property Enabled As Boolean
            Public Property MinimumVersion As String

            Public Shared Function ParseStatusResult(jsonValue As String) As IaipStatusResult
                Return JsonSerializer.Deserialize(Of IaipStatusResult)(jsonValue, JsonOptions)
            End Function
        End Class

    End Module
End Namespace
