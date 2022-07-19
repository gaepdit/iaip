Imports System.Collections.Generic
Imports Iaip.Apb
Imports Iaip.BaseForm

Module SscpFormHelpers

    ' Work Item

    Public Function OpenFormSscpWorkItem(id As Integer) As Form
        Return OpenFormSscpWorkItem(id.ToString)
    End Function

    Public Function OpenFormSscpWorkItem(id As String) As Form
        Dim idInt As Integer

        If Integer.TryParse(id, idInt) AndAlso DAL.Sscp.WorkItemExists(idInt) Then
            Dim refNum As String = ""

            If DAL.Sscp.TryGetRefNumForWorkItem(idInt, refNum) Then Return OpenFormTestReportEntry(refNum)

            If SingleFormIsOpen(SSCPEvents) Then
                Dim item As SSCPEvents = GetSingleForm(Of SSCPEvents)()

                If item.TrackingNumber = idInt Then
                    item.Activate()
                    Return item
                End If
            End If

            Dim sscpReport As SSCPEvents = CType(OpenSingleForm(SSCPEvents, idInt, closeFirst:=True), SSCPEvents)
            sscpReport.TrackingNumber = idInt
            Return sscpReport

        Else
            MessageBox.Show("Tracking number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

    ' FCE 

    Public Function OpenFormFce(airsNumber As ApbFacilityId, Optional year As String = "") As SSCPFCEWork
        If Not DAL.AirsNumberExists(airsNumber) Then
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        Else
            Dim SSCPFCE As New SSCPFCEWork With {
                .AirsNumber = airsNumber
            }

            If SSCPFCE IsNot Nothing AndAlso Not SSCPFCE.IsDisposed Then
                SSCPFCE.Show()
                If Not String.IsNullOrEmpty(year) Then
                    SSCPFCE.SetFceYear(CInt(year))
                End If
                Return SSCPFCE
            Else
                MessageBox.Show("There was an error opening the FCE.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End If
        End If
    End Function

    Public Function OpenFormFce(fceNumber As Integer) As SSCPFCEWork
        Return OpenFormFce(fceNumber.ToString)
    End Function

    Public Function OpenFormFce(fceNumber As String) As SSCPFCEWork
        Dim intFce As Integer

        If Not String.IsNullOrEmpty(fceNumber) AndAlso
            Integer.TryParse(fceNumber, intFce) AndAlso
            DAL.Sscp.FceExists(intFce) Then

            Dim airsNumber As ApbFacilityId = DAL.Sscp.GetFacilityIdByFceId(intFce)

            If airsNumber IsNot Nothing Then
                Dim SSCPFCE As SSCPFCEWork = OpenFormFce(airsNumber)
                SSCPFCE.txtFCENumber.Text = fceNumber
                Return SSCPFCE
            End If
        End If

        Return Nothing
    End Function

    ' Enforcement 

    Public Function OpenFormEnforcement(enforcementId As Integer) As SscpEnforcement
        Return OpenFormEnforcement(enforcementId.ToString)
    End Function

    Public Function OpenFormEnforcement(enforcementId As String) As SscpEnforcement
        If DAL.Sscp.EnforcementExists(enforcementId) Then
            Dim parameters As New Dictionary(Of FormParameter, String)
            parameters(FormParameter.EnforcementId) = enforcementId
            Return CType(OpenMultiForm(SscpEnforcement, CInt(enforcementId), parameters), SscpEnforcement)
        Else
            MessageBox.Show("Enforcement number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

    Public Function OpenFormEnforcement(airsNumber As ApbFacilityId) As SscpEnforcement
        If DAL.AirsNumberExists(airsNumber) Then
            Dim parameters As New Dictionary(Of FormParameter, String)
            parameters(FormParameter.AirsNumber) = airsNumber.ShortString
            Return CType(OpenMultiForm(SscpEnforcement, -Convert.ToInt32(airsNumber.ShortString), parameters), SscpEnforcement)
        Else
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

    Public Function OpenFormEnforcement(airsNumber As ApbFacilityId, trackingNumber As Integer) As SscpEnforcement
        If DAL.AirsNumberExists(airsNumber) Then
            Dim parameters As New Dictionary(Of FormParameter, String)
            parameters(FormParameter.AirsNumber) = airsNumber.ShortString
            parameters(FormParameter.TrackingNumber) = trackingNumber.ToString()
            Return CType(OpenMultiForm(SscpEnforcement, -Convert.ToInt32(airsNumber.ShortString), parameters), SscpEnforcement)
        Else
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

End Module
