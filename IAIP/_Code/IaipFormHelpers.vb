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
        If Not ApbFacilityId.IsValidAirsNumberFormat(airsNumber) Then
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

    Public Function OpenFormSscpWorkItem(id As Integer) As Form
        If DAL.Sscp.WorkItemExists(id) Then
            Dim refNum As String = ""
            If DAL.Sscp.TryGetRefNumForWorkItem(id, refNum) Then
                Return OpenMultiForm(ISMPTestReports, refNum)
            ElseIf SingleFormIsOpen(SSCPEvents) _
                AndAlso CType(SingleForm(SSCPEvents.Name), SSCPEvents).TrackingNumber = id Then
                SingleForm(SSCPEvents.Name).Activate()
                Return SingleForm(SSCPEvents.Name)
            Else
                Dim sscpReport As SSCPEvents = OpenSingleForm(SSCPEvents, id, closeFirst:=True)
                sscpReport.TrackingNumber = id
                Return sscpReport
            End If
        Else
            MessageBox.Show("Tracking number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

#End Region

#Region " FCE "

    Public Function OpenFormFce(airsNumber As ApbFacilityId, Optional year As String = "") As Form
        If Not DAL.AirsNumberExists(airsNumber) Then
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        Else
            Dim SSCPFCE As SSCPFCEWork = New SSCPFCEWork
            SSCPFCE.AirsNumber = airsNumber
            SSCPFCE.Show()
            If Not String.IsNullOrEmpty(year) Then
                SSCPFCE.SetFceYear(year)
            End If
            Return SSCPFCE
        End If
    End Function

    Public Function OpenFormFce(fceNumber As String) As Form
        If String.IsNullOrEmpty(fceNumber) Then
            Return Nothing
        Else
            Dim airsNumber As ApbFacilityId = DAL.Sscp.GetFacilityIdByFceId(fceNumber)
            If airsNumber Is Nothing Then
                Return Nothing
            Else
                Dim SSCPFCE As SSCPFCEWork = OpenFormFce(airsNumber)
                SSCPFCE.txtFCENumber.Text = fceNumber
                Return SSCPFCE
            End If
        End If
    End Function

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

    Public Function OpenFormEnforcement(airsNumber As ApbFacilityId, Optional trackingNumber As Integer = 0) As Form
        Dim parameters As New Dictionary(Of FormParameter, String)
        If DAL.AirsNumberExists(airsNumber) Then
            parameters(FormParameter.AirsNumber) = airsNumber.ToString
            If trackingNumber <> 0 AndAlso DAL.Sscp.WorkItemExists(trackingNumber) Then
                parameters(FormParameter.TrackingNumber) = trackingNumber.ToString
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
                    Dim PrintOut As New IAIPPrintOut
                    PrintOut.ReferenceValue = referenceNumber
                    PrintOut.PrintoutType = IAIPPrintOut.PrintType.IsmpTestReport
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
            Dim ISMPNotificationLogForm As New ISMPNotificationLog
            ISMPNotificationLogForm.txtTestNotificationNumber.Text = id
            ISMPNotificationLogForm.Show()
        Else
            MessageBox.Show("Test notification number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Public Sub OpenFormTestMemo(referenceNumber As String)
        If DAL.Ismp.StackTestExists(referenceNumber) Then
            Dim ISMPMemoEdit As New ISMPMemo
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
