Imports System.Collections.Generic
Imports Iaip.BaseForm

Module SsppFormHelpers

    Public Function OpenFormPermitApplication(appNumber As String) As SSPPApplicationTrackingLog
        Dim appNumberInt As Integer

        If String.IsNullOrWhiteSpace(appNumber) OrElse Not Integer.TryParse(appNumber, appNumberInt) Then
            MessageBox.Show("Application number is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        Return OpenFormPermitApplication(appNumberInt)
    End Function

    Public Function OpenFormNewPermitApplication() As Form
        Return CType(OpenMultiForm(SSPPApplicationTrackingLog, 0), SSPPApplicationTrackingLog)
    End Function

    Public Function OpenFormPermitApplication(appNumber As Integer) As SSPPApplicationTrackingLog
        If DAL.Sspp.ApplicationExists(appNumber) Then
            Dim parameters As New Dictionary(Of FormParameter, String)
            parameters(FormParameter.AppNumber) = appNumber.ToString
            Return CType(OpenMultiForm(SSPPApplicationTrackingLog, appNumber, parameters), SSPPApplicationTrackingLog)
        Else
            MessageBox.Show("Application number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
    End Function

End Module
