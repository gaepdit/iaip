Imports Iaip.Apb

Module FormHelpers

#Region " Facility Summary "

    Public Function OpenFormFacilitySummary() As Form
        Return OpenSingleForm(IAIPFacilitySummary)
    End Function

    Public Function OpenFormFacilitySummary(ByVal airsNumber As String) As Form
        If airsNumber.Length = 0 Then
            Return OpenFormFacilitySummary()
        End If
        If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then
            MessageBox.Show("AIRS number is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        Else
            Return OpenFormFacilitySummary(New ApbFacilityId(airsNumber))
        End If
    End Function

    Public Function OpenFormFacilitySummary(ByVal airsNumber As ApbFacilityId) As Form
        If Not DAL.FacilityModule.AirsNumberExists(airsNumber) Then
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        Else
            Dim facilitySummary As IAIPFacilitySummary = OpenSingleForm(IAIPFacilitySummary)
            facilitySummary.AirsNumber = airsNumber
            Return facilitySummary
        End If
    End Function

#End Region

End Module
