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

#Region " SSCP "

    Public Function OpenFormSscpWorkItem(ByVal id As String) As Form
        If DAL.SSCP.WorkItemExists(id) Then
            Dim refNum As String = ""
            If DAL.SSCP.WorkItemIsAStackTest(id, refNum) Then
                Return OpenMultiForm("ISMPTestReports", refNum)
            ElseIf SingleFormIsOpen("SSCPEvents") _
            AndAlso CType(SingleForm("SSCPEvents"), SSCPEvents).txtTrackingNumber.Text = id Then
                SingleForm("SSCPEvents").Activate()
                Return SingleForm("SSCPEvents")
            Else
                Dim sscpReport As SSCPEvents = OpenSingleForm("SSCPEvents", id, closeFirst:=True)
                sscpReport.txtTrackingNumber.Text = id
                Return sscpReport
            End If
        Else
            MessageBox.Show("Tracking number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

    Private Sub OpenFormFce(ByVal airsNumber As ApbFacilityId)
        ' TODO: better FCE form handling (requires plenty of work in SSCPFCEWork.vb)
        SSCPFCE = New SSCPFCEWork
        SSCPFCE.txtAirsNumber.Text = airsNumber.ToString
        SSCPFCE.Show()
    End Sub

    Public Sub OpenFormFceByYear(ByVal airsNumber As ApbFacilityId, ByVal year As String)
        OpenFormFce(airsNumber)
        SSCPFCE.cboFCEYear.Text = year
    End Sub

    Public Sub OpenFormFceByID(ByVal airsNumber As ApbFacilityId, Optional ByVal id As String = "")
        OpenFormFce(airsNumber)
        If Not String.IsNullOrEmpty(id) Then SSCPFCE.txtFCENumber.Text = id
    End Sub

    Public Function OpenFormEnforcement(ByVal id As String) As Form
        If DAL.SSCP.EnforcementExists(id) Then
            Return OpenMultiForm("SscpEnforcement", id)
        Else
            MessageBox.Show("Enforcement number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

#End Region

End Module
