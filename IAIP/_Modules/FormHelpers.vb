Imports Iaip.Apb

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

    Public Function OpenFormSscpWorkItem(ByVal id As String) As Form
        If DAL.SSCP.WorkItemExists(id) Then
            Dim refNum As String = ""
            If DAL.SSCP.TryGetRefNumForWorkItem(id, refNum) Then
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

#Region " FCE "

    Public Sub OpenFormFce(ByVal airsNumber As ApbFacilityId, Optional ByVal year As String = "")
        SSCPFCE = New SSCPFCEWork
        SSCPFCE.txtAirsNumber.Text = airsNumber.ToString
        SSCPFCE.Show()
        If Not String.IsNullOrEmpty(year) Then
            SSCPFCE.cboFCEYear.Text = year
        End If
    End Sub

    Public Sub OpenFormFceByID(ByVal id As String, Optional ByVal airsNumber As ApbFacilityId = Nothing)
        If Not String.IsNullOrEmpty(id) Then
            If airsNumber Is Nothing Then
                airsNumber = DAL.Sscp.GetFacilityIdByFceId(id)
            End If
            If airsNumber IsNot Nothing Then
                OpenFormFce(airsNumber)
                SSCPFCE.txtFCENumber.Text = id
            End If
        End If
    End Sub

#End Region

    Public Function OpenFormEnforcement(ByVal id As String) As Form
        If DAL.Sscp.EnforcementExists(id) Then
            Return OpenMultiForm("SscpEnforcement", id)
        Else
            MessageBox.Show("Enforcement number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

#End Region

#Region " ISMP "

    Public Sub OpenFormTestPrintout(ByVal referenceNumber As String)
        If DAL.Ismp.StackTestExists(referenceNumber) Then
            If UserProgram = "3" Then
                OpenMultiForm("ISMPTestReports", referenceNumber)
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

    Public Sub OpenFormTestNotification(ByVal id As String)
        If DAL.Ismp.TestNotificationExists(id) Then
            ISMPNotificationLogForm = New ISMPNotificationLog
            ISMPNotificationLogForm.txtTestNotificationNumber.Text = id
            ISMPNotificationLogForm.Show()
        Else
            MessageBox.Show("Test notification number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Public Sub OpenFormTestMemo(ByVal referenceNumber As String)
        If DAL.Ismp.StackTestExists(referenceNumber) Then
            ISMPMemoEdit = New ISMPMemo
            ISMPMemoEdit.txtReferenceNumber.Text = referenceNumber
            ISMPMemoEdit.Show()
        End If

    End Sub

#End Region

#Region " SSPP "

    Public Function OpenFormPermitApplication(ByVal applicationNumber As String) As Form
        If DAL.Sspp.ApplicationExists(applicationNumber) Then
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
