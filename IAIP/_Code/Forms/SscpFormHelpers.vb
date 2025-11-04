Imports System.Collections.Generic
Imports Iaip.Apb
Imports Iaip.BaseForm

Module SscpFormHelpers
    ' TODO-Air-Web: open Air Web page for these

    ' Work Item

    Public Sub OpenFormSscpWorkItem(id As String)
        'Dim idInt As Integer

        'If Integer.TryParse(id, idInt) AndAlso DAL.Sscp.WorkItemExists(idInt) Then
        '    Dim refNum As String = ""

        '    If DAL.Sscp.TryGetRefNumForWorkItem(idInt, refNum) Then Return OpenFormTestReportEntry(refNum)

        '    If SingleFormIsOpen(SSCPEvents) Then
        '        Dim item As SSCPEvents = GetSingleForm(Of SSCPEvents)()

        '        If item.TrackingNumber = idInt Then
        '            item.Activate()
        '            Return item
        '        End If
        '    End If

        '    Dim sscpReport As SSCPEvents = CType(OpenSingleForm(SSCPEvents, idInt, closeFirst:=True), SSCPEvents)
        '    sscpReport.TrackingNumber = idInt
        '    Return sscpReport

        'Else
        '    MessageBox.Show("Tracking number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return Nothing
        'End If
    End Sub

    ' FCE 

    Public Sub OpenFormFce(fceNumber As String)
        'Dim intFce As Integer

        'If Not String.IsNullOrEmpty(fceNumber) AndAlso
        '    Integer.TryParse(fceNumber, intFce) AndAlso
        '    DAL.Sscp.FceExists(intFce) Then

        '    Dim airsNumber As ApbFacilityId = DAL.Sscp.GetFacilityIdByFceId(intFce)

        '    If airsNumber IsNot Nothing Then
        '        Dim SSCPFCE As SSCPFCEWork = OpenFormFce(airsNumber)
        '        SSCPFCE.txtFCENumber.Text = fceNumber
        '        Return SSCPFCE
        '    End If
        'End If

        'Return Nothing
    End Sub

    ' Enforcement 

    Public Sub OpenFormEnforcement(enforcementId As String)
        'If DAL.Sscp.EnforcementExists(enforcementId) Then
        '    Dim parameters As New Dictionary(Of FormParameter, String)
        '    parameters(FormParameter.EnforcementId) = enforcementId
        '    Return CType(OpenMultiForm(SscpEnforcement, CInt(enforcementId), parameters), SscpEnforcement)
        'Else
        '    MessageBox.Show("Enforcement number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return Nothing
        'End If
    End Sub

End Module
