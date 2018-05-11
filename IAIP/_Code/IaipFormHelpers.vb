Imports System.Collections.Generic
Imports Iaip.Apb
Imports Iaip.BaseForm

Module IaipFormHelpers

#Region " Facility Summary "

    Public Function OpenFormFacilitySummary() As Form
        Dim facilitySummary As IAIPFacilitySummary = New IAIPFacilitySummary

        If facilitySummary IsNot Nothing AndAlso Not facilitySummary.IsDisposed Then
            facilitySummary.Show()
            Return facilitySummary
        Else
            MessageBox.Show("There was an error opening the Facility Summary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End If
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

            If facilitySummary IsNot Nothing AndAlso Not facilitySummary.IsDisposed Then
                facilitySummary.Show()
                facilitySummary.AirsNumber = airsNumber
                Return facilitySummary
            Else
                MessageBox.Show("There was an error opening the Facility Summary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End If
        End If
    End Function

#End Region

#Region " SSCP "

#Region " Work Item "

    Public Function OpenFormSscpWorkItem(id As String) As Form
        Dim idInt As Integer

        If Integer.TryParse(id, idInt) AndAlso DAL.Sscp.WorkItemExists(idInt) Then
            Dim refNum As String = ""
            If DAL.Sscp.TryGetRefNumForWorkItem(idInt, refNum) Then
                Return OpenFormTestReportEntry(refNum)
            ElseIf SingleFormIsOpen(SSCPEvents) _
                    AndAlso CType(SingleForm(SSCPEvents.Name), SSCPEvents).TrackingNumber = idInt Then
                SingleForm(SSCPEvents.Name).Activate()
                Return SingleForm(SSCPEvents.Name)
            Else
                Dim sscpReport As SSCPEvents = CType(OpenSingleForm(SSCPEvents, idInt, closeFirst:=True), SSCPEvents)
                sscpReport.TrackingNumber = idInt
                Return sscpReport
            End If
        Else
            MessageBox.Show("Tracking number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

#End Region

#Region " FCE "

    Public Function OpenFormFce(airsNumber As ApbFacilityId, Optional year As String = "") As SSCPFCEWork
        If Not DAL.AirsNumberExists(airsNumber) Then
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        Else
            Dim SSCPFCE As SSCPFCEWork = New SSCPFCEWork With {
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

    Public Function OpenFormFce(fceNumber As String) As SSCPFCEWork
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
            parameters(FormParameter.AirsNumber) = airsNumber.ToString
            Return CType(OpenMultiForm(SscpEnforcement, -Convert.ToInt32(airsNumber.ToString), parameters), SscpEnforcement)
        Else
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

    Public Function OpenFormEnforcement(airsNumber As ApbFacilityId, trackingNumber As Integer) As SscpEnforcement
        If DAL.AirsNumberExists(airsNumber) Then
            Dim parameters As New Dictionary(Of FormParameter, String)
            parameters(FormParameter.AirsNumber) = airsNumber.ToString()
            parameters(FormParameter.TrackingNumber) = trackingNumber.ToString()
            Return CType(OpenMultiForm(SscpEnforcement, -Convert.ToInt32(airsNumber.ToString), parameters), SscpEnforcement)
        Else
            MessageBox.Show("AIRS number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

#End Region

#End Region

#Region " ISMP "

    Public Function OpenFormTestReport(referenceNumber As String) As ISMPTestReports
        If CurrentUser.ProgramID = 3 Then
            Return OpenFormTestReportEntry(referenceNumber)
        End If

        If Not DAL.Ismp.StackTestIsClosedOut(referenceNumber) Then
            MessageBox.Show("Test report has not been closed out by ISMP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        OpenFormTestReportPrintout(referenceNumber)
        Return Nothing
    End Function

    Public Function OpenFormTestReportEntry(referenceNumber As String) As ISMPTestReports
        If String.IsNullOrEmpty(referenceNumber) Then
            MessageBox.Show("Reference number is blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        If Not DAL.Ismp.StackTestExists(referenceNumber) Then
            MessageBox.Show("Reference number does not exist in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        Dim parameters As New Dictionary(Of FormParameter, String)
        parameters(FormParameter.ReferenceNumber) = referenceNumber
        Return CType(OpenMultiForm(ISMPTestReports, NormalizeReferenceId(referenceNumber), Parameters), ISMPTestReports)
    End Function

    Public Function OpenFormTestNotification(id As String) As ISMPNotificationLog
        Dim ISMPNotificationLogForm As ISMPNotificationLog = CType(OpenSingleForm(ISMPNotificationLog, closeFirst:=True), ISMPNotificationLog)

        If ISMPNotificationLogForm IsNot Nothing AndAlso Not ISMPNotificationLogForm.IsDisposed Then
            If DAL.Ismp.TestNotificationExists(id) Then
                ISMPNotificationLogForm.txtTestNotificationNumber.Text = id
            End If

            ISMPNotificationLogForm.Show()
            Return ISMPNotificationLogForm
        Else
            MessageBox.Show("There was an error displaying the test notification.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End If
    End Function

    Public Function OpenFormTestMemo(referenceNumber As String) As ISMPMemo
        If DAL.Ismp.StackTestExists(referenceNumber) Then
            Dim ISMPMemoEdit As ISMPMemo = CType(OpenMultiForm(ISMPMemo, NormalizeReferenceId(referenceNumber)), ISMPMemo)
            ISMPMemoEdit.txtReferenceNumber.Text = referenceNumber
            ISMPMemoEdit.Show()
            Return ISMPMemoEdit
        Else
            Return Nothing
        End If
    End Function

    Public Function OpenFormTestReportPrintout(referenceNumber As String, Optional noConf As Boolean = False) As IAIPPrintOut
        If String.IsNullOrEmpty(referenceNumber) Then
            MessageBox.Show("Reference number is blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        If Not DAL.Ismp.StackTestExists(referenceNumber) Then
            MessageBox.Show("Reference number does not exist in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        Dim PrintOut As New IAIPPrintOut With {
            .ReferenceValue = referenceNumber,
            .PrintoutType = IAIPPrintOut.PrintType.IsmpTestReport,
            .PrintoutSubtype = If(noConf, IAIPPrintOut.PrintSubtype.ToFile, IAIPPrintOut.PrintSubtype.Other)
        }

        If PrintOut IsNot Nothing AndAlso Not PrintOut.IsDisposed Then
            PrintOut.Show()
            Return PrintOut
        Else
            MessageBox.Show("There was an error displaying the printout.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End If
    End Function

    Public Function OpenFormConfidentialTestData(referenceNumber As String) As ISMPConfidentialData
        Dim conf As ISMPConfidentialData = CType(OpenMultiForm(ISMPConfidentialData, NormalizeReferenceId(referenceNumber)), ISMPConfidentialData)
        conf.txtReferenceNumber.Text = referenceNumber
        conf.LoadData()
        Return conf
    End Function

    ''' <summary>
    ''' Returns a unique ID for a given reference number.
    ''' </summary>
    ''' <param name="referenceNumber">The reference number for an ISMP stack test</param>
    ''' <returns>A unique integer for the reference number.</returns>
    ''' <remarks>Required because reference numbers are strings, some have leading zeroes, 
    ''' and some of those have duplicates in the system without the leading zeroes. 
    ''' This function returns the reference number as an integer if it does not have leading
    ''' zeroes. If the reference number has leading zeroes, this function returns the 
    ''' reference number as a negative integer.</remarks>
    Public Function NormalizeReferenceId(referenceNumber As String) As Integer
        If String.IsNullOrEmpty(referenceNumber) OrElse Not Integer.TryParse(referenceNumber, Nothing) Then
            Return 0
        End If
        If referenceNumber.Chars(0) = "0"c Then
            Return -Convert.ToInt32(referenceNumber)
        End If
        Return Convert.ToInt32(referenceNumber)
    End Function

#End Region

#Region " SSPP "

    Public Function OpenFormPermitApplication(applicationNumber As String) As SSPPApplicationTrackingLog
        If DAL.Sspp.ApplicationExists(applicationNumber) Then
            Dim app As SSPPApplicationTrackingLog = CType(OpenSingleForm(SSPPApplicationTrackingLog, CInt(applicationNumber)), SSPPApplicationTrackingLog)
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
        Return CType(OpenSingleForm(SSPPApplicationTrackingLog, closeFirst:=True), SSPPApplicationTrackingLog)
    End Function

#End Region

End Module
