Imports Iaip.Apb

Namespace UrlHelpers

    Public Module PermitSearchUrls

        Private Const PermitSearchUrl As String = "https://permitsearch.gaepd.org"

        Public Function GetPermitFileLink(permitFileName As String) As String
            If String.IsNullOrEmpty(permitFileName) Then Return Nothing

            Return $"{PermitSearchUrl}/Permit/{permitFileName}"
        End Function

        Public Function GetPermitAirsSearchLink(airs As ApbFacilityId) As String
            If airs Is Nothing Then Return Nothing
            Return $"{PermitSearchUrl}/AirsNumber/{airs.ShortString}"
        End Function

        Public Sub OpenPermitFileLink(permitFileName As String, Optional sender As Form = Nothing)
            NotNull(permitFileName, NameOf(permitFileName))
            OpenUriString(GetPermitFileLink(permitFileName), sender)
        End Sub

        Public Sub OpenPermitAirsSearchLink(airsNumber As Apb.ApbFacilityId, Optional sender As Form = Nothing)
            NotNull(airsNumber, NameOf(airsNumber))
            OpenUriString(GetPermitAirsSearchLink(airsNumber), sender)
        End Sub

    End Module

End Namespace
