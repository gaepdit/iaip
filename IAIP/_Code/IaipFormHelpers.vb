Imports System.Collections.Generic
Imports Iaip.Apb
Imports Iaip.BaseForm

Module IaipFormHelpers

#Region " Facility Summary "

    Public Function OpenFormFacilitySummary() As Form
        Dim facilitySummary As IAIPFacilitySummary = New IAIPFacilitySummary
        facilitySummary.Show()
        Return facilitySummary
    End Function

    Public Function OpenFormFacilitySummary(airsNumber As String) As Form
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

    Public Function OpenFormFacilitySummary(airsNumber As ApbFacilityId) As Form
        If Not DAL.FacilityData.AirsNumberExists(airsNumber) Then
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        Else
            Dim facilitySummary As IAIPFacilitySummary = New IAIPFacilitySummary
            facilitySummary.Show()
            facilitySummary.AirsNumber = airsNumber
            Return facilitySummary
        End If
    End Function

#End Region

#Region " SSCP "

#Region " Work Item "

    Public Function OpenFormSscpWorkItem(id As String) As Form
        If DAL.Sscp.WorkItemExists(id) Then
            Dim refNum As String = ""
            If DAL.Sscp.TryGetRefNumForWorkItem(id, refNum) Then
                Return OpenMultiForm(ISMPTestReports, refNum)
            ElseIf SingleFormIsOpen(SSCPEvents) _
                AndAlso CType(SingleForm(SSCPEvents.Name), SSCPEvents).txtTrackingNumber.Text = id Then
                SingleForm(SSCPEvents.Name).Activate()
                Return SingleForm(SSCPEvents.Name)
            Else
                Dim sscpReport As SSCPEvents = OpenSingleForm(SSCPEvents, id, closeFirst:=True)
                sscpReport.txtTrackingNumber.Text = id
                Return sscpReport
            End If
        Else
            MessageBox.Show("Tracking number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

#End Region

#Region " FCE "

    Public Sub OpenFormFce(airsNumber As ApbFacilityId, Optional year As String = "")
        SSCPFCE = New SSCPFCEWork
        SSCPFCE.txtAirsNumber.Text = airsNumber.ToString
        SSCPFCE.Show()
        If Not String.IsNullOrEmpty(year) Then
            SSCPFCE.cboFCEYear.Text = year
        End If
    End Sub

    Public Sub OpenFormFce(fceNumber As String)
        If Not String.IsNullOrEmpty(fceNumber) Then
            Dim airsNumber As ApbFacilityId = DAL.Sscp.GetFacilityIdByFceId(fceNumber)
            If airsNumber IsNot Nothing Then
                OpenFormFce(airsNumber)
                SSCPFCE.txtFCENumber.Text = fceNumber
            End If
        End If
    End Sub

#End Region

#Region " Enforcement "

    Public Function OpenFormEnforcement(enforcementId As String) As Form
        Dim parameters As New Dictionary(Of FormParameter, String)
        If DAL.Sscp.EnforcementExists(enforcementId) Then
            parameters(FormParameter.EnforcementId) = enforcementId
            Return OpenMultiForm(SscpEnforcement, enforcementId, parameters)
        Else
            MessageBox.Show("Enforcement number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

    Public Function OpenFormEnforcement(airsNumber As ApbFacilityId, Optional trackingNumber As String = Nothing) As Form
        Dim parameters As New Dictionary(Of FormParameter, String)
        If DAL.AirsNumberExists(airsNumber) Then
            parameters(FormParameter.AirsNumber) = airsNumber.ToString
            If trackingNumber IsNot Nothing AndAlso DAL.Sscp.WorkItemExists(trackingNumber) Then
                parameters(FormParameter.TrackingNumber) = trackingNumber
            End If
            Return OpenMultiForm(SscpEnforcement, -Convert.ToInt32(airsNumber.ToString), parameters)
        Else
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

#End Region

#End Region

#Region " ISMP "

    Public Sub OpenFormTestPrintout(referenceNumber As String)
        If DAL.Ismp.StackTestExists(referenceNumber) Then
            If CurrentUser.ProgramID = 3 Then
                OpenMultiForm(ISMPTestReports, referenceNumber)
            Else
                If DAL.Ismp.StackTestIsClosedOut(referenceNumber) Then
                    PrintOut = New IAIPPrintOut
                    PrintOut.txtReferenceNumber.Text = referenceNumber
                    PrintOut.txtPrintType.Text = "SSCP"
                    PrintOut.Show()
                Else
                    MessageBox.Show("Test report has not been closed out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Else
            MessageBox.Show("Reference number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Public Sub OpenFormTestNotification(id As String)
        If DAL.Ismp.TestNotificationExists(id) Then
            ISMPNotificationLogForm = New ISMPNotificationLog
            ISMPNotificationLogForm.txtTestNotificationNumber.Text = id
            ISMPNotificationLogForm.Show()
        Else
            MessageBox.Show("Test notification number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Public Sub OpenFormTestMemo(referenceNumber As String)
        If DAL.Ismp.StackTestExists(referenceNumber) Then
            ISMPMemoEdit = New ISMPMemo
            ISMPMemoEdit.txtReferenceNumber.Text = referenceNumber
            ISMPMemoEdit.Show()
        End If

    End Sub

#End Region

#Region " SSPP "

    Public Function OpenFormPermitApplication(applicationNumber As String) As Form
        If DAL.Sspp.ApplicationExists(applicationNumber) Then
            Dim app As SSPPApplicationTrackingLog = OpenSingleForm(SSPPApplicationTrackingLog, applicationNumber)
            app.txtApplicationNumber.Text = applicationNumber
            app.LoadApplication()
            app.TPTrackingLog.Focus()
            Return app
        Else
            MessageBox.Show("Application number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

    Public Function OpenFormNewPermitApplication() As Form
        Dim app As SSPPApplicationTrackingLog = OpenSingleForm(SSPPApplicationTrackingLog, closeFirst:=True)
        Return app
    End Function

#End Region

End Module
