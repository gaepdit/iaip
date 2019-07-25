Imports System.Collections.Generic
Imports Iaip.BaseForm

Module IsmuFormHelpers

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
        Return CType(OpenMultiForm(ISMPTestReports, NormalizeReferenceId(referenceNumber), parameters), ISMPTestReports)
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
            ISMPMemoEdit.lblReferenceNumber.Text = referenceNumber
            ISMPMemoEdit.LoadMemo()
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
    ''' <remarks>Required because reference numbers are strings, some have leading zeros, 
    ''' and some of those have duplicates in the system without the leading zeros. 
    ''' This function returns the reference number as an integer if it does not have leading
    ''' zeroed. If the reference number has leading zeros, this function returns the 
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

End Module
