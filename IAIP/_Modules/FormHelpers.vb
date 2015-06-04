﻿Imports Iaip.Apb

Module FormHelpers

#Region " Facility Summary "

    Public Function OpenFormFacilitySummary() As Form
        Dim facilitySummary As IAIPFacilitySummary = New IAIPFacilitySummary
        facilitySummary.Show()
        Return facilitySummary
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
            Dim facilitySummary As IAIPFacilitySummary = New IAIPFacilitySummary
            facilitySummary.Show()
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

#Region " ISMP "

    Public Sub OpenFormTestPrintout(ByVal referenceNumber As String)
        If DAL.ISMP.StackTestExists(referenceNumber) Then
            If UserProgram = "3" Then
                OpenMultiForm("ISMPTestReports", referenceNumber)
            Else
                If DAL.ISMP.StackTestIsClosedOut(referenceNumber) Then
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

    Public Sub OpenFormTestNotification(ByVal id As String)
        If DAL.ISMP.TestNotificationExists(id) Then
            ISMPNotificationLogForm = New ISMPNotificationLog
            ISMPNotificationLogForm.txtTestNotificationNumber.Text = id
            ISMPNotificationLogForm.Show()
        Else
            MessageBox.Show("Test notification number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Public Sub OpenFormTestMemo(ByVal referenceNumber As String)
        If DAL.ISMP.StackTestExists(referenceNumber) Then
            ISMPMemoEdit = New ISMPMemo
            ISMPMemoEdit.txtReferenceNumber.Text = referenceNumber
            ISMPMemoEdit.Show()
        End If

    End Sub

#End Region

#Region " SSPP "

    Public Function OpenFormPermitApplication(ByVal applicationNumber As String) As Form
        If DAL.SSPP.ApplicationExists(applicationNumber) Then
            Dim app As SSPPApplicationTrackingLog = OpenSingleForm("SSPPApplicationTrackingLog", applicationNumber)
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
        Dim app As SSPPApplicationTrackingLog = OpenSingleForm("SSPPApplicationTrackingLog", closeFirst:=True)
        Return app
    End Function

#End Region

End Module