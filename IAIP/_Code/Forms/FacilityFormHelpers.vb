Imports Iaip.Apb

Module FacilityFormHelpers

    Public Function OpenFormFacilitySummary() As Form
        Dim facilitySummary As New IAIPFacilitySummary

        If facilitySummary IsNot Nothing AndAlso Not facilitySummary.IsDisposed Then
            facilitySummary.Show()
            Return facilitySummary
        End If

        MessageBox.Show("There was an error opening the Facility Summary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return Nothing
    End Function

    Public Function OpenFormFacilitySummary(airsNumber As String) As Form
        If airsNumber.Length = 0 Then
            Return OpenFormFacilitySummary()
        End If

        If Not ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then
            MessageBox.Show("AIRS number is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        Return OpenFormFacilitySummary(New ApbFacilityId(airsNumber))
    End Function

    Public Function OpenFormFacilitySummary(airsNumber As ApbFacilityId) As Form
        If airsNumber Is Nothing Then
            Return OpenFormFacilitySummary()
        End If

        If Not DAL.AirsNumberExists(airsNumber) Then
            MessageBox.Show("Facility does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        Dim facilitySummary As New IAIPFacilitySummary

        If facilitySummary Is Nothing OrElse facilitySummary.IsDisposed Then
            MessageBox.Show("There was an error opening the Facility Summary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End If

        facilitySummary.Show()
        facilitySummary.AirsNumber = airsNumber
        Return facilitySummary
    End Function

End Module
