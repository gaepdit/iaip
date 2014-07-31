Imports Oracle.DataAccess.Client
Imports System.Collections.Generic

Public Class IAIPFacilitySummary
    Dim SQL As String
    Dim dsFacilityWideData As DataSet
    Dim daFacilityWideData As OracleDataAdapter
    Dim dsBottomData As DataSet
    Dim daBottomData As OracleDataAdapter
    Dim dsFees As DataSet
    Dim daFees As OracleDataAdapter
    Dim dsISMP As DataSet
    Dim daISMP As OracleDataAdapter
    Dim dsSSCP As DataSet
    Dim daSSCP As OracleDataAdapter
    Dim dsSSPP As DataSet
    Dim daSSPP As OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim year As String
    Dim inventoryYear As Integer
    Dim recExist2 As Boolean
    Dim SQLLine As String
    Dim count As Integer


    Private Sub DEVFacilitySummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            TCFacilitySummary.TabPages.Remove(TPContactInformation)
            TCFacilitySummary.TabPages.Remove(TPEmissionInventory)
            TCFacilitySummary.TabPages.Remove(TPISMPTestingWork)
            TCFacilitySummary.TabPages.Remove(TPComplianceWork)
            TCFacilitySummary.TabPages.Remove(TPPermittingData)
            TCFacilitySummary.TabPages.Remove(TPPlanningSupportData)

            mmiPrintFacilitySummary.Visible = True
            btnOpenSubpartEditior.Visible = False
            btnEditAirProgramPollutants.Enabled = False

            LoadPermissions()
            LoadToolBars()
            mmiPrintFacilitySummary.Visible = True

            If (UserGCode = "1" Or UserGCode = "345") Then
                mmiAddAFS.Visible = True
                mmiUpdateAFSData.Visible = True
            Else
                mmiAddAFS.Visible = False
                mmiUpdateAFSData.Visible = False
            End If

            ParseParameters()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        mtbAIRSNumber.Focus()
    End Sub

    Private Sub ParseParameters()
        If Parameters IsNot Nothing AndAlso Parameters.ContainsKey("airsnumber") Then
            If (Apb.Facility.NormalizeAirsNumber(Parameters("airsnumber"))) Then
                mtbAIRSNumber.Text = Parameters("airsnumber")
                LoadInitialData()
            End If
        End If
    End Sub

    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll.LinkClicked
        Try
            If mtbAIRSNumber.Text = "" And mtbAIRSNumber.Text.Length <> 8 Then
                mtbAIRSNumber.BackColor = Color.Tomato
                MsgBox("Please enter a valid 8 digit AIRS Number.", MsgBoxStyle.Information, "Facility Summary")
                Exit Sub
            End If
            mtbAIRSNumber.BackColor = Color.White

            ClearForm()
            LoadInitialData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadPermissions()
        Try


            mmiISMP.Visible = False
            mmiSSCP.Visible = False
            mmiEditContactInformation.Visible = False

            Select Case UserBranch
                Case "1" 'Air Protection
                    mmiEditContactInformation.Visible = True
                    Select Case UserProgram
                        Case "1" 'Mobile & Area

                        Case "2" 'Planning & Support 

                        Case "3" 'ISMP 
                            mmiISMP.Visible = True

                            If UserUnit = "---" Then 'Program Manager
                                mmiISMPNewLogEnTry.Visible = True
                                llbClosePrintTestReport.Visible = True
                                mmiSeperator.Visible = True
                            Else
                                If AccountFormAccess(17, 3) = "1" Then 'Unit Manager 
                                    mmiISMPNewLogEnTry.Visible = True
                                    llbClosePrintTestReport.Visible = False
                                    mmiSeperator.Visible = False
                                Else
                                    If AccountFormAccess(68, 3) = "1" Then 'ISMP Administrator
                                        mmiISMPNewLogEnTry.Visible = True
                                        llbClosePrintTestReport.Visible = True
                                        mmiSeperator.Visible = False
                                    Else
                                        If AccountFormAccess(68, 2) = "1" Then 'ISMP Specialist
                                            mmiISMPNewLogEnTry.Visible = True
                                            llbClosePrintTestReport.Visible = False
                                            mmiSeperator.Visible = False
                                        Else
                                            mmiISMPNewLogEnTry.Visible = True
                                            llbClosePrintTestReport.Visible = False
                                            mmiSeperator.Visible = False
                                        End If
                                    End If
                                End If
                            End If
                        Case "4" 'SSCP
                            mmiSSCP.Visible = True

                            If UserUnit = "---" Then 'Program Manager 
                                'mmiSSCPAssignEngineer.Visible = True
                                mmiSSCPAssignEngineer.Visible = False
                                mmiSSCPNewWork.Visible = True
                                mmiSSCPFCE.Visible = True
                            Else
                                If AccountFormAccess(22, 3) = "1" Then 'Unit Manager 
                                    'mmiSSCPAssignEngineer.Visible = True
                                    mmiSSCPAssignEngineer.Visible = False
                                    mmiSSCPNewWork.Visible = True
                                    mmiSSCPFCE.Visible = True
                                Else
                                    If AccountFormAccess(10, 3) = "1" Then 'District Liason 
                                        mmiSSCPAssignEngineer.Visible = False
                                        mmiSSCPNewWork.Visible = True
                                        mmiSSCPFCE.Visible = True
                                    Else
                                        mmiSSCPAssignEngineer.Visible = False
                                        mmiSSCPNewWork.Visible = True
                                        mmiSSCPFCE.Visible = True
                                    End If
                                End If
                            End If
                        Case "5" 'SSPP 

                        Case "6" 'Ambient 

                    End Select
                Case "2" 'Watershed

                Case "3" 'Hazard Waste

                Case "4" 'Land Protection

                Case "5" 'Program Coordination 

                Case "6" 'Directors Office 

            End Select

            mmiNewFacility.Visible = False
            If AccountFormAccess(138, 0) Is Nothing Then
            Else
                If AccountFormAccess(138, 0) = "138" Then
                    If AccountFormAccess(138, 1) = "1" Or AccountFormAccess(138, 2) = "1" Or AccountFormAccess(138, 3) = "1" Or AccountFormAccess(138, 4) = "1" Then
                        mmiNewFacility.Visible = True
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    Sub LoadToolBars()
        pnlLocationEditor.Visible = False
        pnlEditHeaderData.Visible = False

        If UserUnit = "---" Or AccountFormAccess(22, 3) = "1" Or AccountFormAccess(1, 3) = "1" Then
            pnlLocationEditor.Visible = True
            pnlEditHeaderData.Visible = True
        End If
    End Sub

    Sub ClearPage()
        ClearForm()
        mtbAIRSNumber.Clear()
    End Sub

    Sub ClearForm()
        Try
            txtFacilityName.Clear()
            txtStreetAddress.Clear()
            txtStreetAddress2.Clear()
            txtFacilityCity.Clear()
            txtFacilityState.Clear()
            txtFacilityZipCode.Clear()
            txtFacilityLatitude.Clear()
            txtFacilityLongitude.Clear()
            txtFacilityCounty.Clear()
            txtDistrict.Clear()
            txtOffice.Clear()
            txtClassification.Clear()
            txtSICCode.Clear()
            txtOperationalStatus.Clear()
            txtCMSState.Clear()
            txtStartUpDate.Clear()
            txtDateClosed.Clear()
            txtPhysicalShutDownDate.Clear()
            txt1hour.Clear()
            txt8HROzone.Clear()
            txtPM.Clear()
            txtPlantDescription.Clear()
            txtPollutantStatus.Clear()
            txtPollutantStatus.BackColor = Color.Gray
            txtNAICSCode.Clear()

            chbAPC0.Checked = False
            chbAPC1.Checked = False
            chbAPC3.Checked = False
            chbAPC4.Checked = False
            chbAPC6.Checked = False
            chbAPC7.Checked = False
            chbAPC8.Checked = False
            chbAPC9.Checked = False
            chbAPCA.Checked = False
            chbAPCF.Checked = False
            chbAPCI.Checked = False
            chbAPCM.Checked = False
            chbAPCV.Checked = False
            chbAPCRMP.Checked = False
            chbHAPsMajor.Checked = False
            chbNSRMajor.Checked = False

            btnEditAirProgramPollutants.Enabled = False
            If TCFacilitySummary.TabPages.Contains(TPContactInformation) = True Then
                txtSSCPContact.Clear()
                txtSSCPUnit.Clear()
                txtSSPPContact.Clear()
                txtSSPPUnit.Clear()
                txtISMPContact.Clear()
                txtISMPUnit.Clear()
                txtDistrictEngineer.Clear()
                txtDistrictUnit.Clear()
                TCFacilitySummary.TabPages.Remove(TPContactInformation)
            End If
            If TCFacilitySummary.TabPages.Contains(TPEmissionInventory) = True Then
                TCFacilitySummary.TabPages.Remove(TPEmissionInventory)
            End If
            If TCFacilitySummary.TabPages.Contains(TPISMPTestingWork) = True Then
                TCFacilitySummary.TabPages.Remove(TPISMPTestingWork)
            End If
            If TCFacilitySummary.TabPages.Contains(TPComplianceWork) = True Then
                TCFacilitySummary.TabPages.Remove(TPComplianceWork)
            End If
            If TCFacilitySummary.TabPages.Contains(TPPermittingData) = True Then
                TCFacilitySummary.TabPages.Remove(TPPermittingData)
            End If
            If TCFacilitySummary.TabPages.Contains(TPPlanningSupportData) = True Then
                TCFacilitySummary.TabPages.Remove(TPPlanningSupportData)
            End If

            txtReferenceNumber.Clear()
            txtTestingNumber.Clear()
            txtReferenceNumber2.Clear()
            txtTrackingNumber.Clear()
            txtFCEYear.Clear()
            txtEnforcementNumber.Clear()
            txtApplicationNumber.Clear()
            btnOpenSubpartEditior.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            mtbAIRSNumber.Text = Value
        End Set
    End Property

    Private Sub OpenEditContactInformationTool()
        Dim parameters As New Dictionary(Of String, String)
        parameters("airsnumber") = mtbAIRSNumber.Text
        parameters("facilityname") = txtFacilityName.Text
        OpenMultiForm("IAIPEditContacts", mtbAIRSNumber.Text, parameters)
    End Sub

    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = Windows.Forms.DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                Me.ValueFromFacilityLookUp = facilityLookupDialog.SelectedAirsNumber
                ClearForm()
                LoadInitialData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mtbAIRSNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mtbAIRSNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                If mtbAIRSNumber.Text = "" And mtbAIRSNumber.Text.Length <> 8 Then
                    mtbAIRSNumber.BackColor = Color.Tomato
                    MsgBox("Please enter a valid 8 digit AIRS Number.", MsgBoxStyle.Information, "Facility Summary")
                    Exit Sub
                End If
                mtbAIRSNumber.BackColor = Color.White

                ClearForm()
                LoadInitialData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbAPC0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC0.CheckedChanged
        Try

            EditSubPart()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbAPC8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC8.CheckedChanged
        Try

            EditSubPart()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbAPC9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC9.CheckedChanged
        Try

            EditSubPart()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPCM_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPCM.CheckedChanged
        Try

            EditSubPart()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub btnCopyWebSiteContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyWebSiteContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvWebSiteContacts.RowCount > 0 Then
                i = dgvWebSiteContacts.CurrentCell.RowIndex
                MailingAddress = dgvWebSiteContacts(1, i).Value & vbCrLf & _
                dgvWebSiteContacts(2, i).Value & vbCrLf & _
                dgvWebSiteContacts(7, i).Value & vbCrLf & _
                dgvWebSiteContacts(9, i).Value & " " & dgvWebSiteContacts(10, i).Value & ", " & _
                dgvWebSiteContacts(11, i).Value

                Clipboard.SetDataObject(MailingAddress, True)

                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyPermittingContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyPermittingContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvSSPPContacts.RowCount > 0 Then
                i = dgvSSPPContacts.CurrentCell.RowIndex
                MailingAddress = dgvSSPPContacts(1, i).Value & vbCrLf & _
                dgvSSPPContacts(2, i).Value & vbCrLf & _
                dgvSSPPContacts(7, i).Value & vbCrLf & _
                dgvSSPPContacts(9, i).Value & " " & dgvSSPPContacts(10, i).Value & ", " & _
                dgvSSPPContacts(11, i).Value

                Clipboard.SetDataObject(MailingAddress, True)

                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyMointoringContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyMointoringContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvISMPContacts.RowCount > 0 Then
                i = dgvISMPContacts.CurrentCell.RowIndex
                MailingAddress = dgvISMPContacts(1, i).Value & vbCrLf & _
                dgvISMPContacts(2, i).Value & vbCrLf & _
                dgvISMPContacts(7, i).Value & vbCrLf & _
                dgvISMPContacts(9, i).Value & " " & dgvISMPContacts(10, i).Value & ", " & _
                dgvISMPContacts(11, i).Value

                Clipboard.SetDataObject(MailingAddress, True)

                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyComplianceContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyComplianceContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvSSCPContacts.RowCount > 0 Then
                i = dgvSSCPContacts.CurrentCell.RowIndex
                MailingAddress = dgvSSCPContacts(1, i).Value & vbCrLf & _
                dgvSSCPContacts(2, i).Value & vbCrLf & _
                dgvSSCPContacts(7, i).Value & vbCrLf & _
                dgvSSCPContacts(9, i).Value & " " & dgvSSCPContacts(10, i).Value & ", " & _
                dgvSSCPContacts(11, i).Value

                Clipboard.SetDataObject(MailingAddress, True)

                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyGECOContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyGECOContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvGECOContacts.RowCount > 0 Then
                i = dgvGECOContacts.CurrentCell.RowIndex
                MailingAddress = dgvGECOContacts(2, i).Value & vbCrLf & _
                dgvGECOContacts(6, i).Value & vbCrLf & _
                dgvGECOContacts(7, i).Value & vbCrLf & _
                dgvGECOContacts(8, i).Value & " " & dgvGECOContacts(9, i).Value & ", " & _
                dgvGECOContacts(10, i).Value

                Clipboard.SetDataObject(MailingAddress, True)

                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub OpenFacilitySummaryPrintTool()
        Try
            If mtbAIRSNumber.Text = "" Or mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Enter a valid AIRS # first", MsgBoxStyle.Information, "Facility Summary")
                Exit Sub
            End If
            If FacilityPrintOut Is Nothing Then
                If FacilityPrintOut Is Nothing Then FacilityPrintOut = New IaipFacilitySummaryPrint
                FacilityPrintOut.Show()
            Else
                FacilityPrintOut.Dispose()
                FacilityPrintOut = New IaipFacilitySummaryPrint
                If FacilityPrintOut Is Nothing Then FacilityPrintOut = New IaipFacilitySummaryPrint
                FacilityPrintOut.Show()
            End If
            FacilityPrintOut.AirsNumber.Text = mtbAIRSNumber.Text
            FacilityPrintOut.FacilityName.Text = txtFacilityName.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadInitialData()
        Try
            Dim PollutantStatus As String = ""
            Dim dtFacilityWideData As New DataTable
            Dim drDSRow As DataRow

            SQL = "select " & _
            "" & DBNameSpace & ".VW_APBFacilityLocation.strAIRSnumber, " & _
            "" & DBNameSpace & ".VW_APBFacilityLocation.strFacilityName, " & _
            "strFacilityStreet1, strFacilityStreet2, " & _
            "strFacilityCity, strFacilityState, " & _
            "strFacilityZipCode, " & _
            "numFacilityLongitude, numFacilityLatitude, " & _
            "strCountyName, strDistrictName, " & _
            "strOperationalStatus, " & _
            "strClass, strAirProgramCodes, " & _
            "strSICCode, strAttainmentStatus, " & _
            "datStartUpDate, datShutDownDate, " & _
            "strCMSMember, strPlantDescription, " & _
            "strStateProgramCodes, strNAICSCode, " & _
            "STRRMPID " & _
            "from " & _
            "" & DBNameSpace & ".VW_APBFacilityLocation, " & _
            "" & DBNameSpace & ".VW_APBFacilityHeader " & _
            "where " & DBNameSpace & ".VW_APBFacilityLocation.strAIRSNumber = " & DBNameSpace & ".VW_APBFacilityHeader.strAIRSNumber " & _
            "and " & DBNameSpace & ".VW_APBFacilityLocation.strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                MsgBox("No data ", MsgBoxStyle.Exclamation, "Facility Summary")
                Exit Sub
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtFacilityName.Clear()
                Else
                    txtFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    txtStreetAddress.Clear()
                Else
                    txtStreetAddress.Text = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityStreet2")) Then
                    txtStreetAddress2.Clear()
                Else
                    txtStreetAddress2.Text = dr.Item("strFacilityStreet2")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    txtFacilityCity.Clear()
                Else
                    txtFacilityCity.Text = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityState")) Then
                    txtFacilityState.Clear()
                Else
                    txtFacilityState.Text = dr.Item("strFacilityState")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    txtFacilityZipCode.Clear()
                Else
                    txtFacilityZipCode.Text = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("numFacilityLongitude")) Then
                    txtFacilityLongitude.Clear()
                Else
                    txtFacilityLongitude.Text = dr.Item("numFacilityLongitude")
                End If
                If IsDBNull(dr.Item("numFacilityLatitude")) Then
                    txtFacilityLatitude.Clear()
                Else
                    txtFacilityLatitude.Text = dr.Item("numFacilityLatitude")
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    txtFacilityCounty.Clear()
                Else
                    txtFacilityCounty.Text = dr.Item("strCountyName")
                End If
                If IsDBNull(dr.Item("strDistrictName")) Then
                    txtDistrict.Clear()
                Else
                    txtDistrict.Text = dr.Item("strDistrictName")
                End If
                'If IsDBNull(dr.Item("strOfficeName")) Then
                '    txtOffice.Clear()
                'Else
                '    txtOffice.Text = dr.Item("strOfficeName")
                'End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    txtOperationalStatus.Clear()
                Else
                    temp = dr.Item("strOperationalStatus")
                    Select Case temp
                        Case "O"
                            txtOperationalStatus.Text = "O - Operational"
                        Case "P"
                            txtOperationalStatus.Text = "P - Planned"
                        Case "C"
                            txtOperationalStatus.Text = "C - Under Construction"
                        Case "T"
                            txtOperationalStatus.Text = "T - Temporarily Closed"
                        Case "X"
                            txtOperationalStatus.Text = "X - Closed/Dismantled"
                        Case "I"
                            txtOperationalStatus.Text = "I - Seasonal Operation"
                        Case Else
                            txtOperationalStatus.Text = "Unknown - Please Fix"
                    End Select
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    txtClassification.Clear()
                Else
                    txtClassification.Text = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strAirProgramCodes")) Then
                    chbAPC0.Checked = False
                    chbAPC1.Checked = False
                    chbAPC3.Checked = False
                    chbAPC4.Checked = False
                    chbAPC6.Checked = False
                    chbAPC7.Checked = False
                    chbAPC8.Checked = False
                    chbAPC9.Checked = False
                    chbAPCA.Checked = False
                    chbAPCF.Checked = False
                    chbAPCI.Checked = False
                    chbAPCM.Checked = False
                    chbAPCV.Checked = False
                    chbAPCRMP.Checked = False
                Else
                    temp = dr.Item("strAirProgramCodes")
                    If Mid(temp, 1, 1) = 1 Then
                        chbAPC0.Checked = True
                    End If
                    If Mid(temp, 2, 1) = 1 Then
                        chbAPC1.Checked = True
                    End If
                    If Mid(temp, 3, 1) = 1 Then
                        chbAPC3.Checked = True
                    End If
                    If Mid(temp, 4, 1) = 1 Then
                        chbAPC4.Checked = True
                    End If
                    If Mid(temp, 5, 1) = 1 Then
                        chbAPC6.Checked = True
                    End If
                    If Mid(temp, 6, 1) = 1 Then
                        chbAPC7.Checked = True
                    End If
                    If Mid(temp, 7, 1) = 1 Then
                        chbAPC8.Checked = True
                    End If
                    If Mid(temp, 8, 1) = 1 Then
                        chbAPC9.Checked = True
                    End If
                    If Mid(temp, 9, 1) = 1 Then
                        chbAPCF.Checked = True
                    End If
                    If Mid(temp, 10, 1) = 1 Then
                        chbAPCA.Checked = True
                    End If
                    If Mid(temp, 11, 1) = 1 Then
                        chbAPCI.Checked = True
                    End If
                    If Mid(temp, 12, 1) = 1 Then
                        chbAPCM.Checked = True
                    End If
                    If Mid(temp, 13, 1) = 1 Then
                        chbAPCV.Checked = True
                    End If
                    If Mid(temp, 14, 1) = 1 Then
                        chbAPCRMP.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strSICCode")) Then
                    txtSICCode.Clear()
                Else
                    txtSICCode.Text = dr.Item("strSICCode")
                End If
                If IsDBNull(dr.Item("strAttainmentStatus")) Then
                    txt1hour.Text = "No"
                    txt8HROzone.Text = "No"
                    txtPM.Text = "No"
                Else
                    temp = dr.Item("strAttainmentStatus")

                    Select Case Mid(temp, 2, 1)
                        Case 0
                            txt1hour.Text = "No"
                        Case 1
                            txt1hour.Text = "Yes"
                        Case 2
                            txt1hour.Text = "Contributing"
                        Case Else
                            txt1hour.Text = "No"
                    End Select
                    Select Case Mid(temp, 3, 1)
                        Case 0
                            txt8HROzone.Text = "No"
                        Case 1
                            txt8HROzone.Text = "Atlanta"
                        Case 2
                            txt8HROzone.Text = "Macon"
                        Case Else
                            txt8HROzone.Text = "No"
                    End Select
                    Select Case Mid(temp, 4, 1)
                        Case 0
                            txtPM.Text = "No"
                        Case 1
                            txtPM.Text = "Atlanta"
                        Case 2
                            txtPM.Text = "Chattanooga"
                        Case 3
                            txtPM.Text = "Floyd"
                        Case 4
                            txtPM.Text = "Macon"
                        Case Else
                            txtPM.Text = "No"
                    End Select
                End If
                If IsDBNull(dr.Item("datStartUpDate")) Then
                    txtStartUpDate.Clear()
                Else
                    txtStartUpDate.Text = Format(dr.Item("datStartUpDate"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("datShutDownDate")) Then
                    txtDateClosed.Clear()
                Else
                    txtDateClosed.Text = Format(dr.Item("datShutDownDate"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strCMSMember")) Then
                    txtCMSState.Clear()
                Else
                    txtCMSState.Text = dr.Item("strCMSMember")
                End If
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    txtPlantDescription.Clear()
                Else
                    txtPlantDescription.Text = dr.Item("strPlantDescription")
                End If
                If IsDBNull(dr.Item("strStateProgramCodes")) Then
                    chbNSRMajor.Checked = False
                    chbHAPsMajor.Checked = False
                Else
                    temp = dr.Item("strStateProgramCodes")
                    If Mid(temp, 1, 1) = "1" Then
                        chbNSRMajor.Checked = True
                    Else
                        chbNSRMajor.Checked = False
                    End If
                    If Mid(temp, 2, 1) = "1" Then
                        chbHAPsMajor.Checked = True
                    Else
                        chbHAPsMajor.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strNAICSCode")) Then
                    txtNAICSCode.Clear()
                Else
                    txtNAICSCode.Text = dr.Item("strNAICSCode")
                End If
                If IsDBNull(dr.Item("STRRMPID")) Then
                    txtRMPID.Clear()
                Else
                    txtRMPID.Text = dr.Item("strRMPID")
                End If
            End While
            dr.Close()

            SQL = "select distinct(strComplianceStatus) as PollutantStatus " & _
            "from AIRBranch.APBAirProgramPollutants  " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("PollutantStatus")) Then
                    PollutantStatus = PollutantStatus
                Else
                    PollutantStatus = PollutantStatus & dr.Item("PollutantStatus")
                End If
            End While
            dr.Close()

            If PollutantStatus.Contains("B") Then
                txtPollutantStatus.Text = "B - In violation, Procedural  Emissions"
                txtPollutantStatus.BackColor = Color.Pink
            Else
                If PollutantStatus.Contains("1") Then
                    txtPollutantStatus.Text = "1 - In violation, No Schedule"
                    txtPollutantStatus.BackColor = Color.Pink
                Else
                    If PollutantStatus.Contains("6") Then
                        txtPollutantStatus.Text = "6 - In violation, Not Meeting Schedule"
                        txtPollutantStatus.BackColor = Color.Pink
                    Else
                        If PollutantStatus.Contains("W") Then
                            txtPollutantStatus.Text = "W - In violation, procedural"
                            txtPollutantStatus.BackColor = Color.Pink
                        Else
                            If PollutantStatus.Contains("0") Then
                                txtPollutantStatus.Text = "0 - Unknown Compliance Status (SCAP)"
                                txtPollutantStatus.BackColor = Color.Pink
                            Else
                                If PollutantStatus.Contains("5") Then
                                    txtPollutantStatus.Text = "5 - Meeting Compliance Schedule"
                                    txtPollutantStatus.BackColor = Color.LightGreen
                                Else
                                    If PollutantStatus.Contains("8") Then
                                        txtPollutantStatus.Text = "8 - No Applicable State Reg."
                                        txtPollutantStatus.BackColor = Color.Pink
                                    Else
                                        If PollutantStatus.Contains("2") Then
                                            txtPollutantStatus.Text = "2 - In Compliance, Source Test"
                                            txtPollutantStatus.BackColor = Color.LightGreen
                                        Else
                                            If PollutantStatus.Contains("3") Then
                                                txtPollutantStatus.Text = "3 - In Compliance, Inspection"
                                                txtPollutantStatus.BackColor = Color.LightGreen
                                            Else
                                                If PollutantStatus.Contains("4") Then
                                                    txtPollutantStatus.Text = "4 - In Compliance, Certification"
                                                    txtPollutantStatus.BackColor = Color.LightGreen
                                                Else
                                                    If PollutantStatus.Contains("9") Then
                                                        txtPollutantStatus.Text = "9 - In Compliance, Shut Down"
                                                        txtPollutantStatus.BackColor = Color.LightGreen
                                                    Else
                                                        If PollutantStatus.Contains("C") Then
                                                            txtPollutantStatus.Text = "C - In Compliance, Procedural"
                                                            txtPollutantStatus.BackColor = Color.LightGreen
                                                        Else
                                                            If PollutantStatus.Contains("M") Then
                                                                txtPollutantStatus.Text = "M - In Complinace, CEMS Data"
                                                                txtPollutantStatus.BackColor = Color.LightGreen
                                                            Else
                                                                txtPollutantStatus.Text = ""
                                                                txtPollutantStatus.BackColor = Color.White
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            dsFacilityWideData = New DataSet

            SQL = "Select " & _
            "" & DBNameSpace & ".VW_APBFacilityFees.*, " & _
            "(numTotalFee - TotalPaid) as Balance " & _
            "from " & DBNameSpace & ".VW_APBFacilityFees " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  " & _
            "order by intYear DESC "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "Fees")

            cboFeeYear.DataBindings.Clear()
            txtFeesClassification.DataBindings.Clear()
            txtFeesTotal.DataBindings.Clear()
            txtTotalFeesPaid.DataBindings.Clear()
            txtDateSubmitted.DataBindings.Clear()
            txtFeesPart70.DataBindings.Clear()
            txtFeesSM.DataBindings.Clear()
            txtFeesNSPS.DataBindings.Clear()
            txtAdminFee.DataBindings.Clear()
            txtFeesVOC.DataBindings.Clear()
            txtFeesPM.DataBindings.Clear()
            txtFeesSO2.DataBindings.Clear()
            txtFeesNOx.DataBindings.Clear()
            txtFeesRate.DataBindings.Clear()
            txtFeesPollutantFee.DataBindings.Clear()
            chbFeesOperating.DataBindings.Clear()
            chbFeesPart70.DataBindings.Clear()
            chbNSPSExempt.DataBindings.Clear()
            txtBalance.DataBindings.Clear()
            lblFeeStatus.DataBindings.Clear()

            Dim dtFees As New DataTable
            Dim drNewRow As DataRow

            If dsFacilityWideData.Tables("Fees").Rows.Count = 0 Then
                cboFeeYear.Text = ""
                txtFeesClassification.Text = ""
                txtFeesTotal.Text = ""
                txtTotalFeesPaid.Text = ""
                txtDateSubmitted.Text = ""
                txtFeesPart70.Text = ""
                txtFeesSM.Text = ""
                txtFeesNSPS.Text = ""
                txtAdminFee.Text = ""
                txtFeesVOC.Text = ""
                txtFeesPM.Text = ""
                txtFeesSO2.Text = ""
                txtFeesNOx.Text = ""
                txtFeesRate.Text = ""
                txtFeesPollutantFee.Text = ""
                chbFeesOperating.Checked = False
                chbFeesPart70.Checked = False
                chbNSPSExempt.Checked = False
                txtBalance.Text = ""
                lblFeeStatus.Text = ""
            Else
                dtFees.Columns.Add("intYear", GetType(System.String))
                dtFees.Columns.Add("strClass", GetType(System.String))
                dtFees.Columns.Add("intVOCTons", GetType(System.String))
                dtFees.Columns.Add("intPMTons", GetType(System.String))
                dtFees.Columns.Add("intSO2Tons", GetType(System.String))
                dtFees.Columns.Add("intNOXtons", GetType(System.String))
                dtFees.Columns.Add("NumPart70Fee", GetType(System.String))
                dtFees.Columns.Add("NumSMFee", GetType(System.String))
                dtFees.Columns.Add("NumNSPSFee", GetType(System.String))
                dtFees.Columns.Add("NumAdminFee", GetType(System.String))
                dtFees.Columns.Add("NumTotalFee", GetType(System.String))
                dtFees.Columns.Add("strNSPSExempt", GetType(System.String))
                dtFees.Columns.Add("strOperate", GetType(System.String))
                dtFees.Columns.Add("NumFeeRate", GetType(System.String))
                dtFees.Columns.Add("numCalculatedFee", GetType(System.String))
                dtFees.Columns.Add("strPart70", GetType(System.String))
                dtFees.Columns.Add("TotalPaid", GetType(System.String))
                dtFees.Columns.Add("DateSubmit", GetType(System.String))
                dtFees.Columns.Add("Balance", GetType(System.String))
                dtFees.Columns.Add("strIAIPDesc", GetType(System.String))

                For Each drDSRow In dsFacilityWideData.Tables("Fees").Rows()
                    drNewRow = dtFees.NewRow()
                    drNewRow("intYear") = drDSRow("intYear")
                    drNewRow("strClass") = drDSRow("strClass")
                    drNewRow("intVOCTons") = drDSRow("intVOCTons")
                    drNewRow("intPMTons") = drDSRow("intPMTons")
                    drNewRow("intSO2Tons") = drDSRow("intSO2Tons")
                    drNewRow("intNOXtons") = drDSRow("intNOXtons")
                    drNewRow("NumPart70Fee") = drDSRow("NumPart70Fee")
                    drNewRow("NumSMFee") = drDSRow("NumSMFee")
                    drNewRow("NumNSPSFee") = drDSRow("NumNSPSFee")
                    drNewRow("NumAdminFee") = drDSRow("NumAdminFee")
                    drNewRow("NumTotalFee") = drDSRow("NumTotalFee")
                    drNewRow("strNSPSExempt") = drDSRow("strNSPSExempt")
                    drNewRow("strOperate") = drDSRow("strOperate")
                    drNewRow("NumFeeRate") = drDSRow("NumFeeRate")
                    drNewRow("numCalculatedFee") = drDSRow("numCalculatedFee")
                    drNewRow("strPart70") = drDSRow("strPart70")
                    drNewRow("TotalPaid") = drDSRow("TotalPaid")
                    drNewRow("DateSubmit") = drDSRow("DateSubmit")
                    drNewRow("Balance") = drDSRow("Balance")
                    drNewRow("strIAIPDesc") = drDSRow("strIAIPDesc")
                    dtFees.Rows.Add(drNewRow)
                Next

                With txtFeesClassification
                    .DataBindings.Add(New Binding("Text", dtFees, "strClass"))
                End With

                With txtFeesTotal
                    .DataBindings.Add(New Binding("Text", dtFees, "NumTotalFee"))
                End With

                With txtTotalFeesPaid
                    .DataBindings.Add(New Binding("Text", dtFees, "TotalPaid"))
                End With

                With txtDateSubmitted
                    .DataBindings.Add(New Binding("Text", dtFees, "DateSubmit"))
                End With

                With txtFeesPart70
                    .DataBindings.Add(New Binding("Text", dtFees, "NumPart70Fee"))
                End With

                With txtFeesSM
                    .DataBindings.Add(New Binding("Text", dtFees, "NumSMFee"))
                End With

                With txtFeesNSPS
                    .DataBindings.Add(New Binding("Text", dtFees, "NumNSPSFee"))
                End With

                With txtAdminFee
                    .DataBindings.Add(New Binding("Text", dtFees, "NumAdminFee"))
                End With

                With txtFeesVOC
                    .DataBindings.Add(New Binding("Text", dtFees, "intVOCTons"))
                End With

                With txtFeesPM
                    .DataBindings.Add(New Binding("Text", dtFees, "intPMTons"))
                End With

                With txtFeesSO2
                    .DataBindings.Add(New Binding("Text", dtFees, "intSO2Tons"))
                End With

                With txtFeesNOx
                    .DataBindings.Add(New Binding("Text", dtFees, "intNOXtons"))
                End With

                With txtFeesRate
                    .DataBindings.Add(New Binding("Text", dtFees, "NumFeeRate"))
                End With

                With txtFeesPollutantFee
                    .DataBindings.Add(New Binding("Text", dtFees, "numCalculatedFee"))
                End With

                With chbFeesOperating
                    .DataBindings.Add(New Binding("Checked", dtFees, "strOperate"))
                End With

                With chbFeesPart70
                    .DataBindings.Add(New Binding("Checked", dtFees, "strPart70"))
                End With

                With chbNSPSExempt
                    .DataBindings.Add(New Binding("Checked", dtFees, "strNSPSExempt"))
                End With

                With cboFeeYear
                    .DataSource = dtFees
                    .DisplayMember = "FeesData"
                    .ValueMember = "intYear"
                    .SelectedIndex = 0
                End With

                With txtBalance
                    .DataBindings.Add(New Binding("Text", dtFees, "Balance"))
                End With

                With lblFeeStatus
                    .DataBindings.Add(New Binding("Text", dtFees, "strIAIPDesc"))
                End With

            End If

            SQL = "select strDistrictResponsible " & _
            "from " & DBNameSpace & ".SSCPDistrictResponsible " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strDistrictResponsible")) Then
                    lblDistrictSource.Visible = False
                Else
                    If dr.Item("strDistrictResponsible") = "True" Then
                        lblDistrictSource.Visible = True
                    Else
                        lblDistrictSource.Visible = False
                    End If
                End If
            Else
                lblDistrictSource.Visible = False
            End If
            dr.Close()

            EditSubPart()
            btnEditAirProgramPollutants.Enabled = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Main Load Work"
    Private Sub llbViewGoogleMap_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewGoogleMap.LinkClicked
        Try

            Dim StreetAddress As String = "4244 International Parkway"
            Dim City As String = "Atlanta"
            Dim State As String = "GA"
            Dim ZipCode As String = "30354"

            If txtStreetAddress.Text <> "" Then
                StreetAddress = txtStreetAddress.Text
            End If
            If txtFacilityCity.Text <> "" Then
                City = txtFacilityCity.Text
            End If
            If txtFacilityState.Text <> "" Then
                State = txtFacilityState.Text
            End If
            If txtFacilityZipCode.Text <> "" Then
                ZipCode = txtFacilityZipCode.Text
            End If

            Dim url As String = "http://maps.google.com/maps?q=" & StreetAddress & "+" & _
                      City & "+" & State & "+" & ZipCode & "&z=14"
            OpenUri(url, Me)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewAirPermits_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAirPermits.LinkClicked
        Try


            If mtbAIRSNumber.Text <> "" Then
                Dim url As String = "http://search.georgiaair.org/?AirsNumber=" & mtbAIRSNumber.Text
                OpenUri(url, Me)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtDateSubmitted_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateSubmitted.TextChanged
        Try
            If txtBalance.Text <> "" Then
                If CInt(txtBalance.Text) > 0 Then
                    txtTotalFeesPaid.BackColor = Color.Tomato
                Else
                    txtTotalFeesPaid.BackColor = Color.LightGray
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditAirProgramPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditAirProgramPollutants.Click
        Try


            EditAirProgramPollutants = Nothing
            If EditAirProgramPollutants Is Nothing Then EditAirProgramPollutants = New IAIPEditAirProgramPollutants
            EditAirProgramPollutants.txtAirsNumber.Text = Me.mtbAIRSNumber.Text
            EditAirProgramPollutants.Show()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnOpenSubpartEditior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenSubpartEditior.Click
        Try


            If EditSubParts Is Nothing Then
                If EditSubParts Is Nothing Then EditSubParts = New IAIPEditSubParts
            Else
                EditSubParts.Dispose()
                EditSubParts = New IAIPEditSubParts
            End If
            EditSubParts.Show()
            If mtbAIRSNumber.Text <> "" Then
                EditSubParts.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub EditSubPart()
        Try

            If chbAPC8.Checked = True Or chbAPC9.Checked = True Or chbAPCM.Checked = True Or chbAPC0.Checked = True Then
                btnOpenSubpartEditior.Visible = True
            Else
                btnOpenSubpartEditior.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Private Sub llbContactInformation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbContactInformation.LinkClicked
        Try
            If mtbAIRSNumber.Text = "" Or mtbAIRSNumber.Text.Length <> 8 Or txtFacilityName.Text = "" Then
                mtbAIRSNumber.BackColor = Color.Tomato
                MsgBox("Please select a valid AIRS # first.", MsgBoxStyle.Exclamation, Me.Text)
                If TCFacilitySummary.TabPages.Contains(TPContactInformation) = True Then
                    TCFacilitySummary.TabPages.Remove(TPContactInformation)
                End If
                Exit Sub
            End If
            mtbAIRSNumber.BackColor = Color.White

            If TCFacilitySummary.TabPages.Contains(TPContactInformation) = False Then
                TCFacilitySummary.TabPages.Add(TPContactInformation)
            End If

            Dim dtFacilityWideData As New DataTable
            Dim drDSRow As DataRow

            dsFacilityWideData = New DataSet

            SQL = "select " & _
           "distinct(" & DBNameSpace & ".OLAPUserAccess.numUserID), " & _
           "strUserType, " & _
           "(strSalutation||' '||strFirstName||' '||strLastName||', '||strTitle) as GECOContact, " & _
           "" & DBNameSpace & ".OLAPUserLogIN.strUserEmail, " & _
           "strPhoneNumber, strFaxNumber, " & _
           "strCompanyName, strAddress, " & _
           "strCity, strState,  " & _
           "strZip " & _
           "from " & DBNameSpace & ".OLAPUserAccess, " & DBNameSpace & ".OLAPUserProfile,  " & _
           "" & DBNameSpace & ".OLAPUserLogIN  " & _
           "where " & DBNameSpace & ".OLAPUserAccess.numUserID = " & DBNameSpace & ".OLAPUserProfile.NumUserID  " & _
           "and " & DBNameSpace & ".OLAPUserAccess.numUserID = " & DBNameSpace & ".OLAPUserLogIN.numUserID (+) " & _
           "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "GECOContacts")

            SQL = "Select strContactKey, " & _
            "(strContactPrefix||' '||strContactFirstName||' '||strContactLastName||' '||strContactSuffix||', '||strContactTitle) as ContactName, " & _
            "strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & DBNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and strKey like '1%' " & _
            "order by strContactKey "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "ISMPContacts")

            SQL = "Select strContactKey, " & _
            "(strContactPrefix||' '||strContactFirstName||' '||strContactLastName||' '||strContactSuffix||', '||strContactTitle) as ContactName, " & _
            "strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & DBNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and strKey like '2%' " & _
            "order by strContactKey "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "SSCPContacts")

            SQL = "Select strContactKey, " & _
            "(strContactPrefix||' '||strContactFirstName||' '||strContactLastName||' '||strContactSuffix||', '||strContactTitle) as ContactName, " & _
            "strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & DBNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and strKey like '3%' " & _
            "order by strContactKey "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "SSPPContacts")

            SQL = "Select strContactKey, " & _
            "(strContactPrefix||' '||strContactFirstName||' '||strContactLastName||' '||strContactSuffix||', '||strContactTitle) as ContactName, " & _
            "strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & DBNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and strKey like '4%' " & _
            "order by strContactKey "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "WebContacts")

            'SQL = "Select (strLastName||', '||strFirstName) as SSCPEngineer, strUnitDesc " & _
            '"from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SSCPFacilityAssignment,  " & _
            '"" & DBNameSpace & ".LookUpEPDUnits  " & _
            '"where " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            '"and numUserID = strSSCPEngineer   " & _
            '"and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  "

            SQL = "select " & _
"SSCPENGINEER, " & _
"STRUNITDESC " & _
"from " & _
"(select " & _
"NUMSSCPENGINEER, " & _
"(STRLASTNAME||', '||STRFIRSTNAME) as SSCPENGINEER, " & _
"strUnitDesc " & _
"from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED, " & _
"" & DBNameSpace & ".EPDUSERPROFILES, " & DBNameSpace & ".LookUpEPDUnits,  " & _
"(select max(intyear) as MaxYear, " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER  " & _
"from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED " & _
"where " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER = '0413" & mtbAIRSNumber.Text & "' " & _
"group by " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER ) MaxResults " & _
"where " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.NUMSSCPENGINEER = " & DBNameSpace & ".EPDUSERPROFILES.NUMUSERID " & _
"and " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.intyear = maxResults.maxYear  " & _
"and " & DBNameSpace & ".EPDUSERPROFILES.NUMUNIT = " & DBNameSpace & ".LOOKUPEPDUNITS.NUMUNITCODE (+) " & _
"and " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  " & _
"group by NUMSSCPENGINEER, (STRLASTNAME||', '||STRFIRSTNAME), STRUNITDESC)  "


            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "ComplianceContact")

            SQL = "Select " & _
            "Distinct((strLastName||', '||strFirstName)) as ISMPEngineer, strUnitDesc   " & _
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".ISMPReportInformation,   " & _
            "" & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".LookUpEPDUnits    " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDunits.numunitCode (+) " & _
            "and numUserID = strReviewingEngineer   " & _
            "AND " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber   " & _
            "and strClosed = 'True'  " & _
            "and datCompleteDate = (Select Distinct(Max(datCompleteDate)) as CompleteDate  " & _
            "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPMaster  " & _
            "where " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber = " & DBNameSpace & ".ISMPMaster.strReferenceNumber   " & _
            "and " & DBNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  " & _
            "and strClosed = 'True')  " & _
            "and " & DBNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "MonitoringContact")

            SQL = "select  " & _
            "Distinct((strLastName||', '||strFirstName)) as SSPPStaffResponsible, strUnitDesc   " & _
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SSPPApplicationMaster, " & _
            "" & DBNameSpace & ".LookUpEPDUnits " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and numUserID = strStaffResponsible  " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "(select distinct(max(to_number(strApplicationNumber))) as GreatestApplication  " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster   " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "')  " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strAIRSnumber = '0413" & mtbAIRSNumber.Text & "'  "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "PermittingContact")

            dgvGECOContacts.DataSource = dsFacilityWideData
            dgvGECOContacts.DataMember = "GECOContacts"

            dgvGECOContacts.RowHeadersVisible = False
            dgvGECOContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvGECOContacts.AllowUserToResizeColumns = True
            dgvGECOContacts.AllowUserToAddRows = False
            dgvGECOContacts.AllowUserToDeleteRows = False
            dgvGECOContacts.AllowUserToOrderColumns = True
            dgvGECOContacts.AllowUserToResizeRows = True
            dgvGECOContacts.ColumnHeadersHeight = "35"

            dgvGECOContacts.Columns("numUserID").HeaderText = "User ID"
            dgvGECOContacts.Columns("numUserID").DisplayIndex = 0
            dgvGECOContacts.Columns("numUserID").Visible = False
            dgvGECOContacts.Columns("strUserType").HeaderText = "User Type"
            dgvGECOContacts.Columns("strUserType").DisplayIndex = 2
            dgvGECOContacts.Columns("strUserType").Width = 100
            dgvGECOContacts.Columns("GECOContact").HeaderText = "Contact Name"
            dgvGECOContacts.Columns("GECOContact").DisplayIndex = 1
            dgvGECOContacts.Columns("GECOContact").Width = 200
            dgvGECOContacts.Columns("strUserEmail").HeaderText = "User Email"
            dgvGECOContacts.Columns("strUserEmail").DisplayIndex = 3
            dgvGECOContacts.Columns("strUserEmail").Width = 200
            dgvGECOContacts.Columns("strPhoneNumber").HeaderText = "Phone #"
            dgvGECOContacts.Columns("strPhoneNumber").DisplayIndex = 4
            dgvGECOContacts.Columns("strFaxNumber").HeaderText = "Fax #"
            dgvGECOContacts.Columns("strFaxNumber").DisplayIndex = 5
            dgvGECOContacts.Columns("strCompanyName").HeaderText = "Company Name"
            dgvGECOContacts.Columns("strCompanyName").DisplayIndex = 6
            dgvGECOContacts.Columns("strAddress").HeaderText = "Street Address"
            dgvGECOContacts.Columns("strAddress").DisplayIndex = 7
            dgvGECOContacts.Columns("strCity").HeaderText = "City"
            dgvGECOContacts.Columns("strCity").DisplayIndex = 8
            dgvGECOContacts.Columns("strState").HeaderText = "State"
            dgvGECOContacts.Columns("strState").DisplayIndex = 9
            dgvGECOContacts.Columns("strZip").HeaderText = "Zip Code"
            dgvGECOContacts.Columns("strZip").DisplayIndex = 10

            dgvISMPContacts.DataSource = dsFacilityWideData
            dgvISMPContacts.DataMember = "ISMPContacts"

            dgvISMPContacts.RowHeadersVisible = False
            dgvISMPContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvISMPContacts.AllowUserToResizeColumns = True
            dgvISMPContacts.AllowUserToAddRows = False
            dgvISMPContacts.AllowUserToDeleteRows = False
            dgvISMPContacts.AllowUserToOrderColumns = True
            dgvISMPContacts.AllowUserToResizeRows = True
            dgvISMPContacts.ColumnHeadersHeight = "35"

            dgvISMPContacts.Columns("strContactKey").HeaderText = "Contact Key"
            dgvISMPContacts.Columns("strContactKey").DisplayIndex = 0
            dgvISMPContacts.Columns("strContactKey").Visible = False
            dgvISMPContacts.Columns("strContactDescription").HeaderText = "User Type"
            dgvISMPContacts.Columns("strContactDescription").DisplayIndex = 2
            dgvISMPContacts.Columns("strContactDescription").Width = 100
            dgvISMPContacts.Columns("ContactName").HeaderText = "Contact Name"
            dgvISMPContacts.Columns("ContactName").DisplayIndex = 1
            dgvISMPContacts.Columns("ContactName").Width = 200
            dgvISMPContacts.Columns("strContactEmail").HeaderText = "User Email"
            dgvISMPContacts.Columns("strContactEmail").DisplayIndex = 3
            dgvISMPContacts.Columns("strContactEmail").Width = 200
            dgvISMPContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
            dgvISMPContacts.Columns("strContactPhoneNumber1").DisplayIndex = 4
            dgvISMPContacts.Columns("strContactPhoneNumber2").HeaderText = "Phone #2"
            dgvISMPContacts.Columns("strContactPhoneNumber2").DisplayIndex = 5
            dgvISMPContacts.Columns("strContactFaxNumber").HeaderText = "Fax #"
            dgvISMPContacts.Columns("strContactFaxNumber").DisplayIndex = 6
            dgvISMPContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
            dgvISMPContacts.Columns("strContactCompanyName").DisplayIndex = 7
            dgvISMPContacts.Columns("strContactAddress1").HeaderText = "Street Address"
            dgvISMPContacts.Columns("strContactAddress1").DisplayIndex = 8
            dgvISMPContacts.Columns("strContactAddress2").HeaderText = "Street Address"
            dgvISMPContacts.Columns("strContactAddress2").DisplayIndex = 12
            dgvISMPContacts.Columns("strContactAddress2").Visible = False
            dgvISMPContacts.Columns("strContactCity").HeaderText = "City"
            dgvISMPContacts.Columns("strContactCity").DisplayIndex = 9
            dgvISMPContacts.Columns("strContactState").HeaderText = "State"
            dgvISMPContacts.Columns("strContactState").DisplayIndex = 10
            dgvISMPContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
            dgvISMPContacts.Columns("strContactZipCode").DisplayIndex = 11

            dgvSSCPContacts.DataSource = dsFacilityWideData
            dgvSSCPContacts.DataMember = "SSCPContacts"

            dgvSSCPContacts.RowHeadersVisible = False
            dgvSSCPContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSSCPContacts.AllowUserToResizeColumns = True
            dgvSSCPContacts.AllowUserToAddRows = False
            dgvSSCPContacts.AllowUserToDeleteRows = False
            dgvSSCPContacts.AllowUserToOrderColumns = True
            dgvSSCPContacts.AllowUserToResizeRows = True
            dgvSSCPContacts.ColumnHeadersHeight = "35"

            dgvSSCPContacts.Columns("strContactKey").HeaderText = "Contact Key"
            dgvSSCPContacts.Columns("strContactKey").DisplayIndex = 0
            dgvSSCPContacts.Columns("strContactKey").Visible = False
            dgvSSCPContacts.Columns("strContactDescription").HeaderText = "User Type"
            dgvSSCPContacts.Columns("strContactDescription").DisplayIndex = 2
            dgvSSCPContacts.Columns("strContactDescription").Width = 100
            dgvSSCPContacts.Columns("ContactName").HeaderText = "Contact Name"
            dgvSSCPContacts.Columns("ContactName").DisplayIndex = 1
            dgvSSCPContacts.Columns("ContactName").Width = 200
            dgvSSCPContacts.Columns("strContactEmail").HeaderText = "User Email"
            dgvSSCPContacts.Columns("strContactEmail").DisplayIndex = 3
            dgvSSCPContacts.Columns("strContactEmail").Width = 200
            dgvSSCPContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
            dgvSSCPContacts.Columns("strContactPhoneNumber1").DisplayIndex = 4
            dgvSSCPContacts.Columns("strContactPhoneNumber2").HeaderText = "Phone #2"
            dgvSSCPContacts.Columns("strContactPhoneNumber2").DisplayIndex = 5
            dgvSSCPContacts.Columns("strContactFaxNumber").HeaderText = "Fax #"
            dgvSSCPContacts.Columns("strContactFaxNumber").DisplayIndex = 6
            dgvSSCPContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
            dgvSSCPContacts.Columns("strContactCompanyName").DisplayIndex = 7
            dgvSSCPContacts.Columns("strContactAddress1").HeaderText = "Street Address"
            dgvSSCPContacts.Columns("strContactAddress1").DisplayIndex = 8
            dgvSSCPContacts.Columns("strContactAddress2").HeaderText = "Street Address"
            dgvSSCPContacts.Columns("strContactAddress2").DisplayIndex = 12
            dgvSSCPContacts.Columns("strContactAddress2").Visible = False
            dgvSSCPContacts.Columns("strContactCity").HeaderText = "City"
            dgvSSCPContacts.Columns("strContactCity").DisplayIndex = 9
            dgvSSCPContacts.Columns("strContactState").HeaderText = "State"
            dgvSSCPContacts.Columns("strContactState").DisplayIndex = 10
            dgvSSCPContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
            dgvSSCPContacts.Columns("strContactZipCode").DisplayIndex = 11

            dgvSSPPContacts.DataSource = dsFacilityWideData
            dgvSSPPContacts.DataMember = "SSPPContacts"

            dgvSSPPContacts.RowHeadersVisible = False
            dgvSSPPContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSSPPContacts.AllowUserToResizeColumns = True
            dgvSSPPContacts.AllowUserToAddRows = False
            dgvSSPPContacts.AllowUserToDeleteRows = False
            dgvSSPPContacts.AllowUserToOrderColumns = True
            dgvSSPPContacts.AllowUserToResizeRows = True
            dgvSSPPContacts.ColumnHeadersHeight = "35"

            dgvSSPPContacts.Columns("strContactKey").HeaderText = "Contact Key"
            dgvSSPPContacts.Columns("strContactKey").DisplayIndex = 0
            dgvSSPPContacts.Columns("strContactKey").Visible = False
            dgvSSPPContacts.Columns("strContactDescription").HeaderText = "User Type"
            dgvSSPPContacts.Columns("strContactDescription").DisplayIndex = 2
            dgvSSPPContacts.Columns("strContactDescription").Width = 100
            dgvSSPPContacts.Columns("ContactName").HeaderText = "Contact Name"
            dgvSSPPContacts.Columns("ContactName").DisplayIndex = 1
            dgvSSPPContacts.Columns("ContactName").Width = 200
            dgvSSPPContacts.Columns("strContactEmail").HeaderText = "User Email"
            dgvSSPPContacts.Columns("strContactEmail").DisplayIndex = 3
            dgvSSPPContacts.Columns("strContactEmail").Width = 200
            dgvSSPPContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
            dgvSSPPContacts.Columns("strContactPhoneNumber1").DisplayIndex = 4
            dgvSSPPContacts.Columns("strContactPhoneNumber2").HeaderText = "Phone #2"
            dgvSSPPContacts.Columns("strContactPhoneNumber2").DisplayIndex = 5
            dgvSSPPContacts.Columns("strContactFaxNumber").HeaderText = "Fax #"
            dgvSSPPContacts.Columns("strContactFaxNumber").DisplayIndex = 6
            dgvSSPPContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
            dgvSSPPContacts.Columns("strContactCompanyName").DisplayIndex = 7
            dgvSSPPContacts.Columns("strContactAddress1").HeaderText = "Street Address"
            dgvSSPPContacts.Columns("strContactAddress1").DisplayIndex = 8
            dgvSSPPContacts.Columns("strContactAddress2").HeaderText = "Street Address"
            dgvSSPPContacts.Columns("strContactAddress2").DisplayIndex = 12
            dgvSSPPContacts.Columns("strContactAddress2").Visible = False
            dgvSSPPContacts.Columns("strContactCity").HeaderText = "City"
            dgvSSPPContacts.Columns("strContactCity").DisplayIndex = 9
            dgvSSPPContacts.Columns("strContactState").HeaderText = "State"
            dgvSSPPContacts.Columns("strContactState").DisplayIndex = 10
            dgvSSPPContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
            dgvSSPPContacts.Columns("strContactZipCode").DisplayIndex = 11

            dgvWebSiteContacts.DataSource = dsFacilityWideData
            dgvWebSiteContacts.DataMember = "WebContacts"

            dgvWebSiteContacts.RowHeadersVisible = False
            dgvWebSiteContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWebSiteContacts.AllowUserToResizeColumns = True
            dgvWebSiteContacts.AllowUserToAddRows = False
            dgvWebSiteContacts.AllowUserToDeleteRows = False
            dgvWebSiteContacts.AllowUserToOrderColumns = True
            dgvWebSiteContacts.AllowUserToResizeRows = True
            dgvWebSiteContacts.ColumnHeadersHeight = "35"

            dgvWebSiteContacts.Columns("strContactKey").HeaderText = "Contact Key"
            dgvWebSiteContacts.Columns("strContactKey").DisplayIndex = 0
            dgvWebSiteContacts.Columns("strContactKey").Visible = False
            dgvWebSiteContacts.Columns("strContactDescription").HeaderText = "User Type"
            dgvWebSiteContacts.Columns("strContactDescription").DisplayIndex = 2
            dgvWebSiteContacts.Columns("strContactDescription").Width = 100
            dgvWebSiteContacts.Columns("ContactName").HeaderText = "Contact Name"
            dgvWebSiteContacts.Columns("ContactName").DisplayIndex = 1
            dgvWebSiteContacts.Columns("ContactName").Width = 200
            dgvWebSiteContacts.Columns("strContactEmail").HeaderText = "User Email"
            dgvWebSiteContacts.Columns("strContactEmail").DisplayIndex = 3
            dgvWebSiteContacts.Columns("strContactEmail").Width = 200
            dgvWebSiteContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
            dgvWebSiteContacts.Columns("strContactPhoneNumber1").DisplayIndex = 4
            dgvWebSiteContacts.Columns("strContactPhoneNumber2").HeaderText = "Phone #2"
            dgvWebSiteContacts.Columns("strContactPhoneNumber2").DisplayIndex = 5
            dgvWebSiteContacts.Columns("strContactFaxNumber").HeaderText = "Fax #"
            dgvWebSiteContacts.Columns("strContactFaxNumber").DisplayIndex = 6
            dgvWebSiteContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
            dgvWebSiteContacts.Columns("strContactCompanyName").DisplayIndex = 7
            dgvWebSiteContacts.Columns("strContactAddress1").HeaderText = "Street Address"
            dgvWebSiteContacts.Columns("strContactAddress1").DisplayIndex = 8
            dgvWebSiteContacts.Columns("strContactAddress2").HeaderText = "Street Address"
            dgvWebSiteContacts.Columns("strContactAddress2").DisplayIndex = 12
            dgvWebSiteContacts.Columns("strContactAddress2").Visible = False
            dgvWebSiteContacts.Columns("strContactCity").HeaderText = "City"
            dgvWebSiteContacts.Columns("strContactCity").DisplayIndex = 9
            dgvWebSiteContacts.Columns("strContactState").HeaderText = "State"
            dgvWebSiteContacts.Columns("strContactState").DisplayIndex = 10
            dgvWebSiteContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
            dgvWebSiteContacts.Columns("strContactZipCode").DisplayIndex = 11

            dtFacilityWideData.Columns.Add("SSCPEngineer", GetType(System.String))
            dtFacilityWideData.Columns.Add("strUnitDesc", GetType(System.String))

            For Each drDSRow In dsFacilityWideData.Tables("ComplianceContact").Rows()
                If IsDBNull(drDSRow("SSCPEngineer")) Then
                    txtSSCPContact.Clear()
                Else
                    txtSSCPContact.Text = drDSRow("SSCPEngineer")
                End If
                If IsDBNull(drDSRow("strUnitDesc")) Then
                    txtSSCPUnit.Clear()
                Else
                    txtSSCPUnit.Text = drDSRow("strUnitDesc")
                End If
            Next

            dtFacilityWideData.Columns.Add("ISMPEngineer", GetType(System.String))

            For Each drDSRow In dsFacilityWideData.Tables("MonitoringContact").Rows()
                If IsDBNull(drDSRow("ISMPEngineer")) Then
                    txtISMPContact.Clear()
                Else
                    txtISMPContact.Text = drDSRow("ISMPEngineer")
                End If
                If IsDBNull(drDSRow("strUnitDesc")) Then
                    txtISMPUnit.Clear()
                Else
                    txtISMPUnit.Text = drDSRow("strUnitDesc")
                End If
            Next

            dtFacilityWideData.Columns.Add("SSPPStaffResponsible", GetType(System.String))

            For Each drDSRow In dsFacilityWideData.Tables("PermittingContact").Rows()
                If IsDBNull(drDSRow("SSPPStaffResponsible")) Then
                    txtSSPPContact.Clear()
                Else
                    txtSSPPContact.Text = drDSRow("SSPPStaffResponsible")
                End If
                If IsDBNull(drDSRow("strUnitDesc")) Then
                    txtSSPPUnit.Clear()
                Else
                    txtSSPPUnit.Text = drDSRow("strUnitDesc")
                End If
            Next

            If TCFacilitySummary.TabPages.Contains(TPContactInformation) Then
                temp = TCFacilitySummary.TabPages.IndexOf(TPContactInformation)
                If TCFacilitySummary.TabPages.IndexOf(TPContactInformation) <> -1 Then
                    TCFacilitySummary.SelectedIndex = TCFacilitySummary.TabPages.IndexOf(TPContactInformation)
                End If
            End If

        Catch ex As Exception
            ErrorReport(mtbAIRSNumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Contact Information"


#End Region

    Private Sub llbEmissionInventory_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEmissionInventory.LinkClicked
        Try
            If mtbAIRSNumber.Text = "" Or mtbAIRSNumber.Text.Length <> 8 Then
                mtbAIRSNumber.BackColor = Color.Tomato
                MsgBox("Please select a valid AIRS # first.", MsgBoxStyle.Exclamation, Me.Text)
                If TCFacilitySummary.TabPages.Contains(TPEmissionInventory) = True Then
                    TCFacilitySummary.TabPages.Remove(TPEmissionInventory)
                End If
                Exit Sub
            End If
            mtbAIRSNumber.BackColor = Color.White

            If TCFacilitySummary.TabPages.Contains(TPEmissionInventory) = False Then
                TCFacilitySummary.TabPages.Add(TPEmissionInventory)
            End If

            cboEIYear.Items.Clear()

            SQL = "select distinct(strInventoryYear)  as EIYear " & _
            "from " & DBNameSpace & ".EISI " & _
            "where strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "' " & _
            "order by EIYear desc "

            SQL = "select * from " & _
            "(select  " & _
            "distinct(inventoryyear) as EIYear  " & _
            "from airbranch.eis_admin  " & _
            "where airbranch.eis_admin.facilitysiteid = '" & mtbAIRSNumber.Text & "'  " & _
            "union  " & _
            "Select   " & _
            "distinct(to_number(strInventoryYear)) as EIYear  " & _
            "from AIRBranch.EISI  " & _
            "where strInventoryYear < 2010   " & _
            "and strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  ) " & _
            "order by EIYear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                cboEIYear.Items.Add(dr.Item("EIYear"))
            End While
            dr.Close()

            If mtbAIRSNumber.Text <> "" Then
                chkNotNonAttain.Checked = False
                chkLessThan25.Checked = False

                SQL = "Select * from " & DBNameSpace & ".eiSI where strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "' " ' & _ 

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Select " & _
                    "distinct(EIEM.strInventoryYear),  " & _
                    "case  " & _
                    "when COTable.TotalEmissions is Null then  0 " & _
                    "else COTable.TotalEmissions " & _
                    "End CO,  " & _
                    "case  " & _
                    "when LeadTable.TotalEmissions is Null then 0 " & _
                    "else LeadTable. TotalEmissions " & _
                    "END Lead,   " & _
                    "case  " & _
                    "when NH3Table.TotalEmissions is Null then 0 " & _
                    "else NH3Table.TotalEmissions  " & _
                    "END NH3,   " & _
                    "case  " & _
                    "When NOXTable.TotalEmissions is Null then 0 " & _
                    "else NOXTable.TotalEmissions  " & _
                    "END NOX,  " & _
                    "case  " & _
                    "when PMTable.TotalEmissions is null then 0  " & _
                    "else PMTable.TotalEmissions  " & _
                    "end PM,   " & _
                    "case  " & _
                    "when PM10Table.TotalEmissions is NUll then 0  " & _
                    "else PM10Table.TotalEmissions  " & _
                    "end PM10,  " & _
                    "case  " & _
                    "when PM25Table.TotalEmissions is null then 0  " & _
                    "else PM25Table.TotalEmissions  " & _
                    "end PM25,  " & _
                    "case  " & _
                    "when SO2Table.TotalEmissions is NUll then 0  " & _
                    "else SO2Table.TotalEmissions  " & _
                    "End SO2,  " & _
                    "case  " & _
                    "when VOCTable.TotalEmissions is Null then 0  " & _
                    "else VOCTable.TotalEmissions  " & _
                    "end VOC,  " & _
                    "case  " & _
                    "when PMFILTable.TotalEmissions is Null then 0  " & _
                    "else PMFILTable.TotalEmissions  " & _
                    "end PMFIL " & _
                    "from " & DBNameSpace & ".EIEM,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'CO'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) COTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = '7439921'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) LeadTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'NH3'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) NH3Table,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'NOX'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) NOXTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM-PRI'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PMTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM10-PMI'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PM10Table,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM25-PMI'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PM25Table,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'SO2'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) SO2Table,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'VOC'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) VOCTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM-FIL'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PMFILTable " & _
                    "where EIEM.strInventoryYear = COTable.strInventoryYear (+)   " & _
                    "and EIEM.strInventoryYear = LeadTable.strInventoryYear (+)   " & _
                    "and EIEM.strInventoryYear = NH3Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = NOXTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PMTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PM10Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PM25Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = SO2Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = VOCTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PMFILTable.strInventoryYear  (+) " & _
                    "and EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "' " & _
                    "order by EIEM.strInventoryYear DESC "

                    ds = New DataSet
                    da = New OracleDataAdapter(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    da.Fill(ds, "EM")
                    dgvEIData.DataSource = ds
                    dgvEIData.DataMember = "EM"

                    dgvEIData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIData.ReadOnly = True
                    dgvEIData.AllowUserToOrderColumns = True
                    dgvEIData.AllowUserToOrderColumns = True
                    dgvEIData.RowHeadersVisible = False
                    dgvEIData.Columns("strInventoryYear").HeaderText = "Year"
                    dgvEIData.Columns("strInventoryYear").DisplayIndex = 0
                    dgvEIData.Columns("strInventoryYear").Width = 50
                    dgvEIData.Columns("CO").HeaderText = "CO"
                    dgvEIData.Columns("CO").DisplayIndex = 1
                    dgvEIData.Columns("CO").Width = 50
                    dgvEIData.Columns("LEAD").HeaderText = "LEAD"
                    dgvEIData.Columns("LEAD").DisplayIndex = 2
                    dgvEIData.Columns("LEAD").Width = 50
                    dgvEIData.Columns("NH3").HeaderText = "NH3"
                    dgvEIData.Columns("NH3").DisplayIndex = 3
                    dgvEIData.Columns("NH3").Width = 50
                    dgvEIData.Columns("NOX").HeaderText = "NOX"
                    dgvEIData.Columns("NOX").DisplayIndex = 4
                    dgvEIData.Columns("NOX").Width = 50
                    dgvEIData.Columns("PM").HeaderText = "PM"
                    dgvEIData.Columns("PM").DisplayIndex = 5
                    dgvEIData.Columns("PM").Width = 50
                    dgvEIData.Columns("PM10").HeaderText = "PM-10"
                    dgvEIData.Columns("PM10").DisplayIndex = 6
                    dgvEIData.Columns("PM10").Width = 50
                    dgvEIData.Columns("PM25").HeaderText = "PM-2.5"
                    dgvEIData.Columns("PM25").DisplayIndex = 7
                    dgvEIData.Columns("PM25").Width = 50
                    dgvEIData.Columns("SO2").HeaderText = "SO2"
                    dgvEIData.Columns("SO2").DisplayIndex = 8
                    dgvEIData.Columns("SO2").Width = 50
                    dgvEIData.Columns("VOC").HeaderText = "VOC"
                    dgvEIData.Columns("VOC").DisplayIndex = 9
                    dgvEIData.Columns("VOC").Width = 50
                    dgvEIData.Columns("PMFIL").HeaderText = "PM-FIL"
                    dgvEIData.Columns("PMFIL").DisplayIndex = 10
                    dgvEIData.Columns("PMFIL").Width = 50
                Else
                    MsgBox("There is no data available", MsgBoxStyle.OkOnly, "No EI data for this year, choose another year")
                    chkNotNonAttain.Checked = False
                End If

                If cboEIYear.Text <> "" Then
                    inventoryYear = CInt(cboEIYear.Text)
                Else
                    'inventoryYear = Now.Year
                    'cboEIYear.Text = Now.Year
                End If

                SQL = "select * " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where strAirsNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and intESYear = '" & inventoryYear & "' "

                If CurrentConnection.State = ConnectionState.Open Then
                Else
                    CurrentConnection.Open()
                End If

                Dim county As String = Mid(mtbAIRSNumber.Text, 1, 3)

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    If county = "057" Or county = "063" Or county = "067" Or county = "077" Or county = "089" _
                                                Or county = "097" Or county = "113" Or county = "117" Or county = "121" _
                                                Or county = "135" Or county = "151" Or county = "223" Or county = "247" Then
                        SQL = "Select dblVOCEmission, dblNOXEmission, strOptOut " & _
                        "from " & DBNameSpace & ".ESSchema " & _
                        "where intESYear = '" & inventoryYear & "' " & _
                        "and strAirsNumber = '0413" & mtbAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If dr.Item("strOptOut") = "YES" Then
                                chkLessThan25.Checked = True
                            Else
                                chkLessThan25.Checked = False
                                txtNOXEmission.Text = dr.Item("dblNOXEmission")
                                txtVOCEmission.Text = dr.Item("dblVOCEmission")
                            End If
                        End While
                        dr.Close()
                    Else
                        chkNotNonAttain.Checked = True
                    End If
                Else
                    chkNotNonAttain.Checked = True
                    txtNOXEmission.Text = ""
                    txtVOCEmission.Text = ""
                End If

            End If

            SQL = "select inventoryyear from airbranch.eis_admin where facilitysiteId = '" & mtbAIRSNumber.Text & "' " & _
            "and inventoryyear > 2009 "


            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "select * from Airbranch.VW_EIS_EmissionSummary  " & _
                "where FacilitySiteID = '" & mtbAIRSNumber.Text & "' "

                ds = New DataSet
                da = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da.Fill(ds, "EIS")
                dgvEISData.DataSource = ds
                dgvEISData.DataMember = "EIS"

                dgvEISData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvEISData.ReadOnly = True
                dgvEISData.AllowUserToOrderColumns = True
                dgvEISData.AllowUserToOrderColumns = True
                dgvEISData.RowHeadersVisible = False

                dgvEISData.Columns("intinventoryyear").HeaderText = "Year"
                dgvEISData.Columns("intinventoryyear").DisplayIndex = 0
                dgvEISData.Columns("intinventoryyear").Width = 50
                'facilitysiteId
                dgvEISData.Columns("facilitysiteId").HeaderText = "AIRS #"
                dgvEISData.Columns("facilitysiteId").DisplayIndex = 1
                dgvEISData.Columns("facilitysiteId").Visible = False

                dgvEISData.Columns("COEmissions").HeaderText = "Carbon Monoxide"
                dgvEISData.Columns("COEmissions").DisplayIndex = 2
                'dgvEISData.Columns("COEmissions").Width = 50
                dgvEISData.Columns("LeadEmissions").HeaderText = "Lead"
                dgvEISData.Columns("LeadEmissions").DisplayIndex = 3
                'dgvEISData.Columns("LeadEmissions").Width = 50
                dgvEISData.Columns("NH3Emissions").HeaderText = "Ammonia"
                dgvEISData.Columns("NH3Emissions").DisplayIndex = 4
                'dgvEISData.Columns("NH3Emissions").Width = 50
                dgvEISData.Columns("NOxEmissions").HeaderText = "Nitrogen Oxides"
                dgvEISData.Columns("NOxEmissions").DisplayIndex = 5
                ' dgvEISData.Columns("NOxEmissions").Width = 50
                dgvEISData.Columns("PMConEmissions").HeaderText = "Condensible PM (All less than 1 micron)"
                dgvEISData.Columns("PMConEmissions").DisplayIndex = 6
                'dgvEISData.Columns("PMConEmissions").Width = 50
                dgvEISData.Columns("PM10FilEmissions").HeaderText = "Filterable PM10"
                dgvEISData.Columns("PM10FilEmissions").DisplayIndex = 7
                'dgvEISData.Columns("PM10FilEmissions").Width = 50
                dgvEISData.Columns("PM10PriEmissions").HeaderText = "Primary PM10 (Includes Filterables + Condensibles)"
                dgvEISData.Columns("PM10PriEmissions").DisplayIndex = 8
                'dgvEISData.Columns("PM10PriEmissions").Width = 50

                dgvEISData.Columns("PM25FilEmissions").HeaderText = "Filterable PM2.5"
                dgvEISData.Columns("PM25FilEmissions").DisplayIndex = 9
                'dgvEISData.Columns("PM25FilEmissions").Width = 50
                dgvEISData.Columns("PM25PriEmissions").HeaderText = "Primary PM2.5 (Includes Filterables + Condensibles)"
                dgvEISData.Columns("PM25PriEmissions").DisplayIndex = 10
                'dgvEISData.Columns("PM25PriEmissions").Width = 50

                dgvEISData.Columns("SO2Emissions").HeaderText = "Sulfur Dioxide"
                dgvEISData.Columns("SO2Emissions").DisplayIndex = 11
                'dgvEISData.Columns("SO2Emissions").Width = 50
                dgvEISData.Columns("VOCEmissions").HeaderText = "Volatile Organic Compounds"
                dgvEISData.Columns("VOCEmissions").DisplayIndex = 12
                'dgvEISData.Columns("VOCEmissions").Width = 50


            End If

            If TCFacilitySummary.TabPages.Contains(TPEmissionInventory) Then
                If TCFacilitySummary.TabPages.IndexOf(TPEmissionInventory) <> -1 Then
                    TCFacilitySummary.SelectedIndex = TCFacilitySummary.TabPages.IndexOf(TPEmissionInventory)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#Region "Emission Summary"
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i As Integer
            Dim j As Integer

            If dgvEIData.RowCount <> 0 Then
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()


                    For i = 0 To dgvEIData.ColumnCount - 1
                        .Cells(1, i + 1) = dgvEIData.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvEIData.ColumnCount - 1
                        For j = 0 To dgvEIData.RowCount - 2
                            .Cells(j + 2, i + 1).value = dgvEIData.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

        End Try

    End Sub
    Private Sub btnExportEIExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportEIExport.Click
        Try
            If mtbAIRSNumber.Text <> "" And Me.cboEIYear.Text <> "" Then
                BindDataGridSI()
                BindDataGridEU()
                BindDataGridER()
                BindDataGridEP()
                BindDataGridEM()
                export2Excel()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#Region "EI Export Routines"
    Private Sub BindDataGridSI()
        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strFacilityName, " & _
                            "strLocationAddress, " & _
                            "strCity, " & _
                            "strState, " & _
                            "strZipCode, " & _
                            "strCounty, " & _
                            "dblXCoordinate, " & _
                            "dblYCoordinate, " & _
                            "strHorizontalCollectionCode, " & _
                            "(Select STRHORIZCOLLECTIONMETHODDESC " & _
                               "from " & DBNameSpace & ".EILOOKUPHORIZCOLMETHOD " & _
                               "where " & DBNameSpace & ".EISI.STRHORIZONTALCOLLECTIONCODE = " & _
                               "" & DBNameSpace & ".EILOOKUPHORIZCOLMETHOD.STRHORIZCOLLECTIONMETHODCODE) as HMCdesc, " & _
                            "strHorizontalReferenceCode, " & _
                            "strHorizontalAccuracyMeasure, " & _
                            "(Select STRHORIZONTALREFERENCEDESC " & _
                               "from " & DBNameSpace & ".EILOOKUPHORIZREFDATUM " & _
                               "where " & DBNameSpace & ".EISI.STRHORIZONTALREFERENCECODE = " & _
                               "" & DBNameSpace & ".EILOOKUPHORIZREFDATUM.STRHORIZONTALREFERENCEDATUM) as HDRCdesc, " & _
                            "strContactPrefix, " & _
                            "strContactFirstName, " & _
                            "strContactLastName, " & _
                            "strContactTitle, " & _
                            "strContactEmail, " & _
                            "strContactPhoneNumber1, " & _
                            "strContactPhoneNumber2, " & _
                            "strContactFaxNumber, " & _
                            "strContactCompanyName, " & _
                            "strContactAddress1, " & _
                            "strContactCity, " & _
                            "strContactState, " & _
                            "strContactZipCode, " & _
                            "strSiteDescription, " & _
                            "strSICPrimary, " & _
                            "strNAICSPrimary " & _
                     "from " & DBNameSpace & ".eiSI where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvSI.Rows.Clear()
            dgvSI.Columns.Clear()

            dgvSI.Columns.Add("strFacilityName", "Facility")
            dgvSI.Columns.Add("strLocationAddress", "Facility Location")
            dgvSI.Columns.Add("strCity", "City")
            dgvSI.Columns.Add("strState", "State")
            dgvSI.Columns.Add("strZipCode", "Zip Code")
            dgvSI.Columns.Add("strCounty", "County")
            dgvSI.Columns.Add("dblXCoordinate", "Longitude")
            dgvSI.Columns.Add("dblYCoordinate", "Latitude")
            dgvSI.Columns.Add("strHorizontalCollectionCode", "Horizontal Collection Code")
            dgvSI.Columns.Add("HMCdesc", "Horizontal Collection Desc")
            dgvSI.Columns.Add("strHorizontalAccuracyMeasure", "Horizontal Accuracy Measure")
            dgvSI.Columns.Add("strHorizontalReferenceCode", "Horizontal Datum Reference Code")
            dgvSI.Columns.Add("HDRCdesc", "Horizontal Datum Reference Desc")
            dgvSI.Columns.Add("strContactPrefix", "Contact Prefix")
            dgvSI.Columns.Add("strContactFirstName", "Contact First Name")
            dgvSI.Columns.Add("strContactLastName", "Contact Last Name")
            dgvSI.Columns.Add("strContactTitle", "Contact Title")
            dgvSI.Columns.Add("strContactEmail", "Contact Email")
            dgvSI.Columns.Add("strContactPhoneNumber1", "Contact Phone Number")
            dgvSI.Columns.Add("strContactPhoneNumber2", "Contact Phone Other")
            dgvSI.Columns.Add("strContactFaxNumber", "Contact Fax Number")
            dgvSI.Columns.Add("strContactCompanyName", "Contact Company Name")
            dgvSI.Columns.Add("strContactAddress1", "Contact Address")
            dgvSI.Columns.Add("strContactCity", "Contact City")
            dgvSI.Columns.Add("strContactState", "Contact State")
            dgvSI.Columns.Add("strContactZipCode", "Contact Zip Code")
            dgvSI.Columns.Add("strSiteDescription", "Site Description")
            dgvSI.Columns.Add("strSICPrimary", "SIC Primary")
            dgvSI.Columns.Add("strNAICSPrimary", "NAICS Primary")

            dgvSI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvSI.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvSI.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvSI.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEU()
        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strEmissionUnitID, " & _
                         "sngDesignCapacity, " & _
                         "strDesignCapUnitNum, " & _
                         "(Select STRUNITDESCRIPTION " & _
                               "from " & DBNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & DBNameSpace & ".EIEU.strDesignCapUnitNum = " & _
                               "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as numDesc, " & _
                         "strDesignCapUnitDenom, " & _
                         "(Select STRUNITDESCRIPTION " & _
                               "from " & DBNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & DBNameSpace & ".EIEU.strDesignCapUnitDenom = " & _
                               "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as denomDesc, " & _
                         "sngMaxNameplateCapacity, " & _
                         "strEmissionUnitDesc " & _
                    "from " & DBNameSpace & ".eiEU where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEU.Rows.Clear()
            dgvEU.Columns.Clear()

            dgvEU.Columns.Add("strEmissionUnitID", "Emission Unit ID")
            dgvEU.Columns.Add("sngDesignCapacity", "Design Capacity")
            dgvEU.Columns.Add("strDesignCapUnitNum", "Design Capacity Unit Numerator")
            dgvEU.Columns.Add("numDesc", "Design Capacity Unit Numerator Desc")
            dgvEU.Columns.Add("strDesignCapUnitDenom", "Design Capacity Unit Denominator")
            dgvEU.Columns.Add("denomDesc", "Design Capacity Unit Denominator Desc")
            dgvEU.Columns.Add("sngMaxNameplateCapacity", "Max Name plate Capacity")
            dgvEU.Columns.Add("strEmissionUnitDesc", "Emission Unit Desc")

            dgvEU.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEU.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvEU.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvEU.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridER()
        Try
            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strEmissionReleasePointID, " & _
            "strEmissionReleaseType, " & _
            "(Select STREMISSIONTYPEDESC " & _
            "from " & DBNameSpace & ".EILOOKUPEMISSIONTYPES " & _
            "where " & DBNameSpace & ".EIER.STREMISSIONRELEASETYPE = " & _
            "" & DBNameSpace & ".EILOOKUPEMISSIONTYPES.STREMISSIONTYPECODE) as stackType, " & _
            "sngStackHeight, " & _
            "sngStackDiameter, " & _
            "sngExitGasTemperature, " & _
            "sngExitGasVelocity, " & _
            "sngExitGasFlowRate, " & _
            "dblXCoordinate, " & _
            "dblYCoordinate, " & _
            "strEmissionReleasePtDesc, " & _
            "strHorizontalCollectionCode, " & _
            "(Select STRHORIZCOLLECTIONMETHODDESC " & _
            "from " & DBNameSpace & ".EILOOKUPHORIZCOLMETHOD " & _
            "where " & DBNameSpace & ".EIER.STRHORIZONTALCOLLECTIONCODE = " & _
            "" & DBNameSpace & ".EILOOKUPHORIZCOLMETHOD.STRHORIZCOLLECTIONMETHODCODE) as HMCdesc, " & _
            "strHorizontalAccuracyMeasure, " & _
            "strHorizontalReferenceCode, " & _
            "(Select STRHORIZONTALREFERENCEDESC " & _
            "from " & DBNameSpace & ".EILOOKUPHORIZREFDATUM " & _
            "where " & DBNameSpace & ".EIER.STRHORIZONTALREFERENCECODE = " & _
            "" & DBNameSpace & ".EILOOKUPHORIZREFDATUM.STRHORIZONTALREFERENCEDATUM) as HDRCdesc " & _
            "from " & DBNameSpace & ".eiER where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvER.Rows.Clear()
            dgvER.Columns.Clear()

            dgvER.Columns.Add("strEmissionReleasePointID", "Emission Release Point ID")
            dgvER.Columns.Add("strEmissionReleaseType", "Emission Release Type")
            dgvER.Columns.Add("stackType", "Stack Type")
            dgvER.Columns.Add("sngStackHeight", "Stack Height")
            dgvER.Columns.Add("sngStackDiameter", "Stack Diameter")
            dgvER.Columns.Add("sngExitGasTemperature", "Exit Gas Temperature")
            dgvER.Columns.Add("sngExitGasVelocity", "Exit Gas Velocity")
            dgvER.Columns.Add("sngExitGasFlowRate", "Exit Gas Flow Rate")
            dgvER.Columns.Add("dblXCoordinate", "Longitude")
            dgvER.Columns.Add("dblYCoordinate", "Latitude")
            dgvER.Columns.Add("strEmissionReleasePtDesc", "Emission Release Point Desc")
            dgvER.Columns.Add("strHorizontalCollectionCode", "Horizontal Collection Code")
            dgvER.Columns.Add("HMCdesc", "Horizontal Collection Code Desc")
            dgvER.Columns.Add("strHorizontalAccuracyMeasure", "Horizontal Accuracy Measure")
            dgvER.Columns.Add("strHorizontalReferenceCode", "Horizontal Datum Reference Code")
            dgvER.Columns.Add("HDRCdesc", "Horizontal Datum Reference Code Desc")

            dgvER.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvER.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvER.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvER.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEP()
        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strSCC, " & _
                            "strEmissionProcessDescription, " & _
                            "strEmissionUnitID, " & _
                            "strEmissionReleasePointID, " & _
                            "strProcessID, " & _
                            "intWinterThroughputPCT, " & _
                            "intSpringThroughputPCT, " & _
                            "intSummerThroughputPCT, " & _
                            "intFallThroughputPCT, " & _
                            "intAnnualAvgDaysPerWeek, " & _
                            "intAnnualAvgWeeksPerYear, " & _
                            "intAnnualAvgHoursPerDay, " & _
                            "intAnnualAvgHoursPerYear, " & _
                            "sngHeatContent, " & _
                            "sngSulfurContent, " & _
                            "sngAshContent, " & _
                            "sngDailySummerProcessTPut, " & _
                            "strDailySummerProcessTPutNum, " & _
                            "(Select STRUNITDESCRIPTION " & _
                               "from " & DBNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & DBNameSpace & ".EIEP.strDailySummerProcessTPutNum = " & _
                               "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as DailySummerTputNumDesc, " & _
                            "sngActualThroughput, " & _
                            "strThroughputUnitNumerator, " & _
                            "(Select STRUNITDESCRIPTION " & _
                               "from " & DBNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & DBNameSpace & ".EIEP.strThroughputUnitNumerator = " & _
                               "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as TputNumDesc, " & _
                            "strStartTime " & _
                       "from " & DBNameSpace & ".eiEP " & _
                      "where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEP.Rows.Clear()
            dgvEP.Columns.Clear()

            dgvEP.Columns.Add("strSCC", "SCC")
            dgvEP.Columns.Add("strEmissionProcessDescription", "Emission Process Description")
            dgvEP.Columns.Add("strEmissionUnitID", "Emission Unit ID")
            dgvEP.Columns.Add("strEmissionReleasePointID", "Emission Release Point ID")
            dgvEP.Columns.Add("strProcessID", "Process ID")
            dgvEP.Columns.Add("intWinterThroughputPCT", "Winter Throughput Percent")
            dgvEP.Columns.Add("intSpringThroughputPCT", "Spring Throughput Percent")
            dgvEP.Columns.Add("intSummerThroughputPCT", "Summer Throughput Percent")
            dgvEP.Columns.Add("intFallThroughputPCT", "Fall Throughput Percent")
            dgvEP.Columns.Add("intAnnualAvgDaysPerWeek", "Annual Average Days Per Week")
            dgvEP.Columns.Add("intAnnualAvgWeeksPerYear", "Annual Average Weeks Per Year")
            dgvEP.Columns.Add("intAnnualAvgHoursPerDay", "Annual Average Hours Per Day")
            dgvEP.Columns.Add("intAnnualAvgHoursPerYear", "Annual Average Hours Per Year")
            dgvEP.Columns.Add("sngHeatContent", "Heat Content")
            dgvEP.Columns.Add("sngSulfurContent", "Sulfur Content")
            dgvEP.Columns.Add("sngAshContent", "Ash Content")
            dgvEP.Columns.Add("sngDailySummerProcessTPut", "Daily Summer Process Throughput")
            dgvEP.Columns.Add("strDailySummerProcessTPutNum", "Daily Summer Process Throughput Numerator")
            dgvEP.Columns.Add("DailySummerTputNumDesc", "Daily Summer Process Throughput Numerator Desc")
            dgvEP.Columns.Add("sngActualThroughput", "Actual Throughput")
            dgvEP.Columns.Add("strThroughputUnitNumerator", "Actual Throughput Unit Numerator")
            dgvEP.Columns.Add("TputNumDesc", "Actual Throughput Unit Numerator Desc")
            dgvEP.Columns.Add("strStartTime", "Start Time")


            dgvEP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEP.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvEP.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvEP.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEM()
        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select STREMISSIONUNITID, "
            SQL += "STREMISSIONRELEASEPOINTID, "
            SQL += "STRPROCESSID, "
            SQL += "strPollutantCode, "
            SQL += "(Select STRPOLLUTANTDESC "
            SQL += "from " & DBNameSpace & ".EILOOKUPPOLLUTANTCODES "
            SQL += "where " & DBNameSpace & ".EIEM.STRPOLLUTANTCODE = "
            SQL += "" & DBNameSpace & ".EILOOKUPPOLLUTANTCODES.STRPOLLUTANTCODE) as pollutantDesc, "
            SQL += "DBLEMISSIONNUMERICVALUE, "
            SQL += "STREMISSIONUNITNUMERATOR, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & DBNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & DBNameSpace & ".EIEM.STREMISSIONUNITNUMERATOR = "
            SQL += "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as EMISSIONUNITNUMERATORDesc, "
            SQL += "sngFactorNumericValue, "
            SQL += "strFactorUnitNumerator, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & DBNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & DBNameSpace & ".EIEM.strFactorUnitNumerator = "
            SQL += "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as FactorUnitNumeratorDesc, "
            SQL += "strFactorUnitDenominator, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & DBNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & DBNameSpace & ".EIEM.strFactorUnitDenominator = "
            SQL += "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as FactorUnitDenominatorDesc, "
            SQL += "strEmissionCalculationMetCode, "
            SQL += "(Select STREMISSIONCALCMETHODDESC "
            SQL += "from " & DBNameSpace & ".EILOOKUPEMISSIONCALCMETHOD "
            SQL += "where " & DBNameSpace & ".EIEM.strEmissionCalculationMetCode = "
            SQL += "" & DBNameSpace & ".EILOOKUPEMISSIONCALCMETHOD.STREMISSIONCALCMETHODCODE) as EMISSIONCALCMETHODDESC, "
            SQL += "strControlStatus, "
            SQL += "strControlSystemDescription, "
            SQL += "strPrimaryDeviceTypeCode, "
            SQL += "(Select STRCONTROLDEVICEDesc "
            SQL += "from " & DBNameSpace & ".EILOOKUPCONTROLDEVICE "
            SQL += "where " & DBNameSpace & ".EIEM.strPrimaryDeviceTypeCode = "
            SQL += "" & DBNameSpace & ".EILOOKUPCONTROLDEVICE.STRCONTROLDEVICECODE) as PrimaryDeviceTypeDesc, "
            SQL += "sngPrimaryPCTControlEffic, "
            SQL += "strSecondaryDeviceTypeCode, "
            SQL += "(Select STRCONTROLDEVICEDesc "
            SQL += "from " & DBNameSpace & ".EILOOKUPCONTROLDEVICE "
            SQL += "where " & DBNameSpace & ".EIEM.strSecondaryDeviceTypeCode = "
            SQL += "" & DBNameSpace & ".EILOOKUPCONTROLDEVICE.STRCONTROLDEVICECODE) as SecondaryDeviceTypeDesc, "
            SQL += "sngPCTCaptureEfficiency, "
            SQL += "sngTotalCaptureControlEffic "
            SQL += "from " & DBNameSpace & ".eiEM "
            SQL += "where strAirsYear = '" & airsYear & "'"

            'SQL = "Select * from " & DBNameSpace & ".eiEM where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEM.Rows.Clear()
            dgvEM.Columns.Clear()

            dgvEM.Columns.Add("STREMISSIONUNITID", "EMISSION UNIT ID")
            dgvEM.Columns.Add("STREMISSIONRELEASEPOINTID", "EMISSION RELEASE POINT ID")
            dgvEM.Columns.Add("STRPROCESSID", "PROCESS ID")
            dgvEM.Columns.Add("strPollutantCode", "Pollutant Code")
            dgvEM.Columns.Add("pollutantDesc", "pollutant Desc")
            dgvEM.Columns.Add("DBLEMISSIONNUMERICVALUE", "EMISSION NUMERIC VALUE")
            dgvEM.Columns.Add("STREMISSIONUNITNUMERATOR", "EMISSION UNIT NUMERATOR")
            dgvEM.Columns.Add("EMISSIONUNITNUMERATORDesc", "EMISSION UNIT NUMERATOR Desc")
            dgvEM.Columns.Add("sngFactorNumericValue", "Factor Numeric Value")
            dgvEM.Columns.Add("strFactorUnitNumerator", "Factor Unit Numerator")
            dgvEM.Columns.Add("FactorUnitNumeratorDesc", "Factor Unit Numerator Desc")
            dgvEM.Columns.Add("strFactorUnitDenominator", "Factor Unit Denominator")
            dgvEM.Columns.Add("FactorUnitDenominatorDesc", "Factor Unit Denominator Desc")
            dgvEM.Columns.Add("strEmissionCalculationMetCode", "Emission Calculation Method Code")
            dgvEM.Columns.Add("EMISSIONCALCMETHODDESC", "Emission Calculation Method Code Desc")
            dgvEM.Columns.Add("strControlStatus", "Control Status")
            dgvEM.Columns.Add("strControlSystemDescription", "Control System Description")
            dgvEM.Columns.Add("strPrimaryDeviceTypeCode", "Primary Device Type Code")
            dgvEM.Columns.Add("PrimaryDeviceTypeDesc", "Primary Device Type Code Desc")
            dgvEM.Columns.Add("sngPrimaryPCTControlEffic", "Primary Percentage Control Efficiency")
            dgvEM.Columns.Add("strSecondaryDeviceTypeCode", "Secondary Device Type Code")
            dgvEM.Columns.Add("SecondaryDeviceTypeDesc", "Secondary Device Type Code Desc")
            dgvEM.Columns.Add("sngPCTCaptureEfficiency", "Percent Capture Efficiency")
            dgvEM.Columns.Add("sngTotalCaptureControlEffic", "Total Capture Control Efficiency")



            dgvEM.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEM.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvEM.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvEM.Rows.Add(objCells)
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub export2Excel()
        Try
            exportSI()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub exportSI()
        Try
            'Response.AddHeader("content-disposition", "attachment;filename=EIData.xls")
            '' Set MIME type to Excel.
            'Response.ContentType = "application/vnd.ms-excel"
            '' Remove the charset from the Content-Type header.
            'Response.Charset = ""
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As Excel.Application = New Excel.ApplicationClass
            Dim col, row As Integer
            Dim x As String
            Dim y As String
            Dim a As Integer
            Dim b As Integer
            Dim c As Integer
            Dim d As Integer
            Dim startRow As Integer = 1



            'load SI data into Excel
            If dgvSI.RowCount <> 0 Then

                ExcelApp.SheetsInNewWorkbook = 1
                ExcelApp.Workbooks.Add()

                ExcelApp.Cells(startRow, 1).value = "Facility Information"

                'For displaying the column name in the the excel file.
                '29 columns in SI 
                'cells(x,y) x=row  y=col
                For col = 0 To dgvSI.ColumnCount - 1
                    y = dgvSI.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow + 1, col + 1).value = y
                Next

                For row = 0 To 0
                    For col = 0 To dgvSI.ColumnCount - 1
                        If IsDBNull(dgvSI.Item(col, row).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvSI.Item(col, row).Value.ToString
                        End If
                        'x = dgvSI.Item(col, row).Value.ToString
                        ExcelApp.Cells(startRow + 2, col + 1).value = x
                    Next
                Next
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If

            End If

            txtRow.Text = startRow + 3

            'load EU data into Excel
            If dgvEU.RowCount <> 0 Then

                startRow = CInt(txtRow.Text) + 2
                ExcelApp.Cells(startRow, 1).value = "Emission Units"
                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEU.ColumnCount - 1
                    y = dgvEU.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                a = dgvEU.ColumnCount - 1
                b = dgvEU.RowCount - 1

                'For col = 0 To dgvEU.RowCount - 1
                '    For row = 0 To dgvEU.ColumnCount - 1
                startRow = startRow + 1
                d = dgvEU.RowCount - 2
                For row = 0 To d

                    c = dgvEU.ColumnCount - 1
                    For col = 0 To c
                        If IsDBNull(dgvEU.Item(col, row).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvEU.Item(col, row).Value.ToString
                        End If
                        'x = dgvEU.Item(col, row).Value.ToString
                        ExcelApp.Cells(startRow, col + 1).value = x
                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load ER data into Excel
            If dgvER.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)
                ExcelApp.Cells(startRow, 1).value = "Stacks"
                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvER.ColumnCount - 1
                    y = dgvER.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                a = dgvER.ColumnCount - 1
                b = dgvER.RowCount - 1

                startRow = startRow + 1
                d = dgvER.RowCount - 2
                For row = 0 To d

                    c = dgvER.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvER.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvER.Item(col, row).Value) Then
                        Else
                            'x = dgvER.Item(col, row).Value.ToString
                            x = dgvER.Item(col, row).Value
                        End If
                        ExcelApp.Cells(startRow, col + 1).value = x
                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load EP data into Excel
            If dgvEP.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)
                ExcelApp.Cells(startRow, 1).value = "Processes"
                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEP.ColumnCount - 1
                    y = dgvEP.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                a = dgvEP.ColumnCount - 1
                b = dgvEP.RowCount - 1

                startRow = startRow + 1
                d = dgvEP.RowCount - 2
                For row = 0 To d

                    c = dgvEP.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvEP.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvEP.Item(col, row).Value) Then
                        Else
                            'x = dgvEP.Item(col, row).Value.ToString
                            x = dgvEP.Item(col, row).Value
                        End If
                        ExcelApp.Cells(startRow, col + 1).value = x
                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load EM data into Excel
            If dgvEM.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)
                ExcelApp.Cells(startRow, 1).value = "Pollutants and Control Equipment"
                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEM.ColumnCount - 1
                    y = dgvEM.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                a = dgvEM.ColumnCount - 1
                b = dgvEM.RowCount - 1

                startRow = startRow + 1
                d = dgvEM.RowCount - 2
                For row = 0 To d

                    c = dgvEM.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvEM.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvEM.Item(col, row).Value) Then
                        Else
                            'x = dgvEM.Item(col, row).Value.ToString
                            x = dgvEM.Item(col, row).Value
                        End If
                        ExcelApp.Cells(startRow, col + 1).value = x
                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            'txtRow.Text = startRow + 2

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                MsgBox(ex.ToString)
            End If
        Finally

        End Try

    End Sub
#End Region
#End Region

    Private Sub llbISMPTestingWork_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbISMPTestingWork.LinkClicked
        Try
            If mtbAIRSNumber.Text = "" Or mtbAIRSNumber.Text.Length <> 8 Then
                mtbAIRSNumber.BackColor = Color.Tomato
                MsgBox("Please select a valid AIRS # first.", MsgBoxStyle.Exclamation, Me.Text)
                If TCFacilitySummary.TabPages.Contains(TPISMPTestingWork) = True Then
                    TCFacilitySummary.TabPages.Remove(TPISMPTestingWork)
                End If
                Exit Sub
            End If
            mtbAIRSNumber.BackColor = Color.White
            dsISMP = New DataSet

            If TCFacilitySummary.TabPages.Contains(TPISMPTestingWork) = False Then
                TCFacilitySummary.TabPages.Add(TPISMPTestingWork)
            End If

            SQL = "Select " & DBNameSpace & ".VW_ISMPWorkDataGrid.*, strPreComplianceStatus  " & _
            "from " & DBNameSpace & ".VW_ISMPWorkDataGrid, " & DBNameSpace & ".ISMPReportInformation " & _
            "where " & DBNameSpace & ".VW_ISMPWorkDataGrid.strReferenceNumber = " & _
            "" & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
            "and strAIRSNUmber = '0413" & mtbAIRSNumber.Text & "' " & _
            SQLLine '& _
            ' "order by " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber DESC "

            daISMP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daISMP.Fill(dsISMP, "ISMPWork")

            SQL = "Select strTestLogNumber, " & _
            "(strLastName||', '||strFirstName) as Engineer,  " & _
            "strEmissionUnit, strUnitDesc, " & _
            "datTestNotification, datProposedstartDate,  " & _
            "datProposedEndDate, strComments  " & _
            "from " & DBNameSpace & ".ISMPTestNotification, " & DBNameSpace & ".EPDUserProfiles, " & _
            "" & DBNameSpace & ".LookUpEPDUnits  " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".ISMPTestNotification.strStaffResponsible  " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  " & _
            "order by strTestLogNumber DESC "

            daISMP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daISMP.Fill(dsISMP, "ISMPTestLog")

            SQL = "Select " & DBNameSpace & ".ISMPTestREportMemo.strReferenceNumber, " & _
            "strMemorandumField " & _
            "from " & DBNameSpace & ".ISMPTestREportMemo, " & DBNameSpace & ".ISMPMaster " & _
            "where " & DBNameSpace & ".ISMPTestREportMemo.strReferenceNumber = " & DBNameSpace & ".ISMPMaster.strReferenceNumber " & _
            "and " & DBNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            SQLLine & _
            "Order by " & DBNameSpace & ".ISMPTestREportMemo.strReferenceNumber DESC "

            daISMP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daISMP.Fill(dsISMP, "ISMPMemo")

            dgvISMPWork.DataSource = dsISMP
            dgvISMPWork.DataMember = "ISMPWork"

            dgvISMPWork.RowHeadersVisible = False
            dgvISMPWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvISMPWork.AllowUserToResizeColumns = True
            dgvISMPWork.AllowUserToAddRows = False
            dgvISMPWork.AllowUserToDeleteRows = False
            dgvISMPWork.AllowUserToOrderColumns = True
            dgvISMPWork.AllowUserToResizeRows = True

            dgvISMPWork.Columns("strAIRSNumber").HeaderText = "AIRS Number"
            dgvISMPWork.Columns("strAIRSNumber").DisplayIndex = 0
            dgvISMPWork.Columns("strAIRSNumber").Visible = False
            dgvISMPWork.Columns("strReferenceNumber").HeaderText = "Reference Number"
            dgvISMPWork.Columns("strReferenceNumber").DisplayIndex = 1
            dgvISMPWork.Columns("strReferenceNumber").Width = 100
            dgvISMPWork.Columns("strEmissionSource").HeaderText = "Emission Source"
            dgvISMPWork.Columns("strEmissionSource").DisplayIndex = 2
            dgvISMPWork.Columns("strEmissionSource").Width = 200
            dgvISMPWork.Columns("strPollutantDescription").HeaderText = "Pollutant"
            dgvISMPWork.Columns("strPollutantDescription").DisplayIndex = 3
            dgvISMPWork.Columns("strReportType").HeaderText = "Report Type"
            dgvISMPWork.Columns("strReportType").DisplayIndex = 4
            dgvISMPWork.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
            dgvISMPWork.Columns("ReviewingEngineer").DisplayIndex = 7
            dgvISMPWork.Columns("TestDateStart").HeaderText = "Test Date"
            dgvISMPWork.Columns("TestDateStart").DisplayIndex = 8
            dgvISMPWork.Columns("TestDateStart").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvISMPWork.Columns("ReceivedDate").HeaderText = "Received Date"
            dgvISMPWork.Columns("ReceivedDate").DisplayIndex = 9
            dgvISMPWork.Columns("ReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvISMPWork.Columns("DatReceivedDate").HeaderText = "Received Date"
            dgvISMPWork.Columns("DatReceivedDate").DisplayIndex = 10
            dgvISMPWork.Columns("DatReceivedDate").Visible = False
            dgvISMPWork.Columns("CompleteDate").HeaderText = "Complete Date"
            dgvISMPWork.Columns("CompleteDate").DisplayIndex = 11
            dgvISMPWork.Columns("CompleteDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvISMPWork.Columns("strComplianceStatus").HeaderText = "Compliance Status"
            dgvISMPWork.Columns("strComplianceStatus").DisplayIndex = 12
            dgvISMPWork.Columns("Status").HeaderText = "Compliance Status"
            dgvISMPWork.Columns("Status").DisplayIndex = 13
            dgvISMPWork.Columns("MMOCommentArea").HeaderText = "Comment Field"
            dgvISMPWork.Columns("MMOCommentArea").DisplayIndex = 14
            dgvISMPWork.Columns("strDocumentType").HeaderText = "Document Type"
            dgvISMPWork.Columns("strDocumentType").DisplayIndex = 5
            dgvISMPWork.Columns("strApplicableRequirement").HeaderText = "Applicable Requirement"
            dgvISMPWork.Columns("strApplicableRequirement").DisplayIndex = 15
            dgvISMPWork.Columns("strUnitTitle").HeaderText = "Reviewing Unit"
            dgvISMPWork.Columns("strUnitTitle").DisplayIndex = 6
            dgvISMPWork.Columns("strPreComplianceStatus").HeaderText = "Pre-Compliance Status"
            dgvISMPWork.Columns("strPreComplianceStatus").DisplayIndex = 16

            dgvISMPTestNotification.DataSource = dsISMP
            dgvISMPTestNotification.DataMember = "ISMPTestLog"

            dgvISMPTestNotification.RowHeadersVisible = False
            dgvISMPTestNotification.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvISMPTestNotification.AllowUserToResizeColumns = True
            dgvISMPTestNotification.AllowUserToAddRows = False
            dgvISMPTestNotification.AllowUserToDeleteRows = False
            dgvISMPTestNotification.AllowUserToOrderColumns = True
            dgvISMPTestNotification.AllowUserToResizeRows = True
            dgvISMPTestNotification.Columns("strTestLogNumber").HeaderText = "Test Log Number"
            dgvISMPTestNotification.Columns("strTestLogNumber").DisplayIndex = 0
            dgvISMPTestNotification.Columns("Engineer").HeaderText = "ISMP Engineer"
            dgvISMPTestNotification.Columns("Engineer").DisplayIndex = 3
            dgvISMPTestNotification.Columns("strEmissionUnit").HeaderText = "Emission Source"
            dgvISMPTestNotification.Columns("strEmissionUnit").DisplayIndex = 1
            dgvISMPTestNotification.Columns("strEmissionUnit").Width = 100
            dgvISMPTestNotification.Columns("strUnitDesc").HeaderText = "ISMP Unit"
            dgvISMPTestNotification.Columns("strUnitDesc").DisplayIndex = 2
            dgvISMPTestNotification.Columns("strUnitDesc").Width = 200
            dgvISMPTestNotification.Columns("datTestNotification").HeaderText = "Date Notified"
            dgvISMPTestNotification.Columns("datTestNotification").DisplayIndex = 4
            dgvISMPTestNotification.Columns("datTestNotification").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvISMPTestNotification.Columns("datProposedstartDate").HeaderText = "Proposed Date"
            dgvISMPTestNotification.Columns("datProposedstartDate").DisplayIndex = 5
            dgvISMPTestNotification.Columns("datProposedstartDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvISMPTestNotification.Columns("datProposedEndDate").HeaderText = "Proposed End Date"
            dgvISMPTestNotification.Columns("datProposedEndDate").DisplayIndex = 6
            dgvISMPTestNotification.Columns("datProposedEndDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvISMPTestNotification.Columns("strComments").HeaderText = "Comments"
            dgvISMPTestNotification.Columns("strComments").DisplayIndex = 7

            dgvISMPMemo.DataSource = dsISMP
            dgvISMPMemo.DataMember = "ISMPMemo"

            dgvISMPMemo.RowHeadersVisible = False
            dgvISMPMemo.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvISMPMemo.AllowUserToResizeColumns = True
            dgvISMPMemo.AllowUserToAddRows = False
            dgvISMPMemo.AllowUserToDeleteRows = False
            dgvISMPMemo.AllowUserToOrderColumns = True
            dgvISMPMemo.AllowUserToResizeRows = True
            dgvISMPMemo.Columns("strReferenceNumber").HeaderText = "Reference Number"
            dgvISMPMemo.Columns("strReferenceNumber").DisplayIndex = 0
            dgvISMPMemo.Columns("strMemorandumField").HeaderText = "Memo Field"
            dgvISMPMemo.Columns("strMemorandumField").DisplayIndex = 1
            dgvISMPMemo.Columns("strMemorandumField").Width = 500

            LoadCompliaceColor()


            If TCFacilitySummary.TabPages.Contains(TPISMPTestingWork) Then
                If TCFacilitySummary.TabPages.IndexOf(TPISMPTestingWork) <> -1 Then
                    TCFacilitySummary.SelectedIndex = TCFacilitySummary.TabPages.IndexOf(TPISMPTestingWork)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "ISMP Monitoring Work"
    Private Sub dgvISMPWork_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvISMPWork.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvISMPWork.HitTest(e.X, e.Y)

        Try
            If dgvISMPWork.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvISMPWork.Columns(1).HeaderText = "Reference Number" Then
                    txtReferenceNumber.Text = dgvISMPWork(1, hti.RowIndex).Value
                End If
            End If
            LoadCompliaceColor()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvISMPTestNotification_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvISMPTestNotification.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvISMPTestNotification.HitTest(e.X, e.Y)

        Try
            If dgvISMPTestNotification.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvISMPTestNotification.Columns(0).HeaderText = "Test Log Number" Then
                    txtTestingNumber.Text = dgvISMPTestNotification(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvISMPMemo_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvISMPMemo.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvISMPMemo.HitTest(e.X, e.Y)

        Try
            If dgvISMPMemo.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvISMPMemo.Columns(0).HeaderText = "Reference Number" Then
                    txtReferenceNumber2.Text = dgvISMPMemo(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadCompliaceColor()
        Try
            For Each row As DataGridViewRow In dgvISMPWork.Rows
                If Not row.IsNewRow Then
                    If Not row.Cells(16).Value Is DBNull.Value Then
                        temp = row.Cells(16).Value
                        If row.Cells(16).Value = "True" Then
                            row.DefaultCellStyle.BackColor = Color.Pink
                        End If
                    End If
                    If Not row.Cells(10).Value Is DBNull.Value Then
                        temp = row.Cells(10).Value
                        If row.Cells(10).Value = "Not In Compliance" Then
                            row.DefaultCellStyle.BackColor = Color.Tomato
                        End If
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub llbISMPTestReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbISMPTestReport.LinkClicked
        Try
            Dim id As String = txtReferenceNumber.Text
            If id = "" Then Exit Sub

            If DAL.ISMP.StackTestExists(id) Then
                If UserProgram = "3" Then
                    OpenMultiForm("ISMPTestReports", id)
                Else
                    If DAL.ISMP.StackTestIsClosedOut(id) Then
                        If PrintOut IsNot Nothing AndAlso Not PrintOut.IsDisposed Then
                            PrintOut.Dispose()
                        End If
                        PrintOut = New IAIPPrintOut
                        PrintOut.txtReferenceNumber.Text = txtReferenceNumber.Text
                        PrintOut.txtPrintType.Text = "SSCP"
                        PrintOut.Show()
                    Else
                        MsgBox("This test has not been completely reviewed by ISMP.", MsgBoxStyle.Information, "Facility Summary")
                    End If
                End If
            Else
                MsgBox("Reference number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbClosePrintTestReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbClosePrintTestReport.LinkClicked
        Try

            If txtReferenceNumber.Text <> "" Then
                SQL = "Select " & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPReportInformation " & _
                 "where " & DBNameSpace & ".ISMPReportInformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKey and " & _
                 "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                Dim cmd As New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                Dim dr As OracleDataReader = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    ISMPCloseAndPrint = Nothing
                    If ISMPCloseAndPrint Is Nothing Then ISMPCloseAndPrint = New ISMPClosePrint
                    ISMPCloseAndPrint.txtTestReportType.Text = dr.Item("strDocumentType")
                    ISMPCloseAndPrint.txtReferenceNumber.Text = txtReferenceNumber.Text
                    ISMPCloseAndPrint.txtAIRSNumber.Text = mtbAIRSNumber.Text
                    ISMPCloseAndPrint.txtFacilityName.Text = txtFacilityName.Text
                    ISMPCloseAndPrint.txtOrigin.Text = "Facility Summary"
                    ISMPCloseAndPrint.Show()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewTestNotification_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewTestNotification.LinkClicked
        Try

            If txtTestingNumber.Text <> "" Then
                ISMPNotificationLogForm = Nothing
                If ISMPNotificationLogForm Is Nothing Then ISMPNotificationLogForm = New ISMPNotificationLog
                ISMPNotificationLogForm.txtTestNotificationNumber.Text = Me.txtTestingNumber.Text
                ISMPNotificationLogForm.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewTestReport2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewTestReportMemo.LinkClicked
        Try

            If txtReferenceNumber2.Text <> "" Then
                ISMPMemoEdit = Nothing
                If ISMPMemoEdit Is Nothing Then ISMPMemoEdit = New ISMPMemo
                ISMPMemoEdit.txtReferenceNumber.Text = Me.txtReferenceNumber2.Text
                ISMPMemoEdit.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Private Sub llbComplianceWork_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbComplianceWork.LinkClicked
        Try
            If mtbAIRSNumber.Text = "" Or mtbAIRSNumber.Text.Length <> 8 Then
                mtbAIRSNumber.BackColor = Color.Tomato
                MsgBox("Please select a valid AIRS # first.", MsgBoxStyle.Exclamation, Me.Text)
                If TCFacilitySummary.TabPages.Contains(TPComplianceWork) = True Then
                    TCFacilitySummary.TabPages.Remove(TPComplianceWork)
                End If
                Exit Sub
            End If
            mtbAIRSNumber.BackColor = Color.White
            dsSSCP = New DataSet

            If TCFacilitySummary.TabPages.Contains(TPComplianceWork) = False Then
                TCFacilitySummary.TabPages.Add(TPComplianceWork)
            End If

            SQL = "Select * " & _
            "From " & DBNameSpace & ".VW_SSCPWorkDataGrid " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "Order by strTrackingNumber DESC "

            daSSCP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daSSCP.Fill(dsSSCP, "SSCPEvents")


            SQL = "Select distinct(strEnforcementNumber), " & _
           "Case  " & _
           "	when datDiscoveryDate is Null then '' " & _
           "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
           "END as Violationdate,  " & _
           "strActionType as HPVStatus, " & _
           "Case " & _
           "	when datEnforcementFinalized Is Not NULL then 'Closed' " & _
           "	when datEnforcementFinalized is NUll then 'Open' " & _
           "Else 'Open' " & _
           "End as Status, " & _
           "substr(strAIRSNumber, 5) as AIRSNumber " & _
           "from " & DBNameSpace & ".SSCP_AuditedEnforcement  " & _
           "Where  strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
           "order by strENforcementNumber DESC "

            daSSCP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daSSCP.Fill(dsSSCP, "SSCPEnforcement")

            SQL = "Select " & DBNameSpace & ".SSCPFCEMaster.strFCENumber, " & _
            "strFCEStatus, " & _
            "(strLastname||', '||strFirstName) as ReviewingEngineer, " & _
            "to_char(DatFCECompleted, 'dd-Mon-yyyy') as FCECompleted, " & _
            "strFCEYear as FCEYear, " & _
            "strFCEComments " & _
            "from " & DBNameSpace & ".SSCPFCE, " & DBNameSpace & ".SSCPFCEMaster, " & DBNameSpace & ".EPDuserProfiles " & _
            "where StrAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
            "and " & DBNameSpace & ".EPDuserProfiles.numUserID = " & DBNameSpace & ".SSCPFCE.strReviewer  " & _
            "order by DatFCECompleted DESC "

            daSSCP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daSSCP.Fill(dsSSCP, "SSCPFCE")


            dgvSSCPEvents.DataSource = dsSSCP
            dgvSSCPEvents.DataMember = "SSCPEvents"

            dgvSSCPEvents.RowHeadersVisible = False
            dgvSSCPEvents.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSSCPEvents.AllowUserToResizeColumns = True
            dgvSSCPEvents.AllowUserToAddRows = False
            dgvSSCPEvents.AllowUserToDeleteRows = False
            dgvSSCPEvents.AllowUserToOrderColumns = True
            dgvSSCPEvents.AllowUserToResizeRows = True
            dgvSSCPEvents.Columns("strAIRSNumber").HeaderText = "AIRS Number"
            dgvSSCPEvents.Columns("strAIRSNumber").DisplayIndex = 0
            dgvSSCPEvents.Columns("strAIRSNumber").Visible = False
            dgvSSCPEvents.Columns("strTrackingNumber").HeaderText = "Tracking Number"
            dgvSSCPEvents.Columns("strTrackingNumber").DisplayIndex = 1
            dgvSSCPEvents.Columns("strActivityName").HeaderText = "Event Type"
            dgvSSCPEvents.Columns("strActivityName").DisplayIndex = 2
            dgvSSCPEvents.Columns("ReceivedDate").HeaderText = "Received Date"
            dgvSSCPEvents.Columns("ReceivedDate").DisplayIndex = 3
            dgvSSCPEvents.Columns("ReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvSSCPEvents.Columns("datReceivedDate").HeaderText = "ReceivedDate"
            dgvSSCPEvents.Columns("datReceivedDate").DisplayIndex = 4
            dgvSSCPEvents.Columns("datReceivedDate").Visible = False

            dgvSSCPEnforcement.DataSource = dsSSCP
            dgvSSCPEnforcement.DataMember = "SSCPEnforcement"

            dgvSSCPEnforcement.RowHeadersVisible = False
            dgvSSCPEnforcement.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSSCPEnforcement.AllowUserToResizeColumns = True
            dgvSSCPEnforcement.AllowUserToAddRows = False
            dgvSSCPEnforcement.AllowUserToDeleteRows = False
            dgvSSCPEnforcement.AllowUserToOrderColumns = True
            dgvSSCPEnforcement.AllowUserToResizeRows = True
            dgvSSCPEnforcement.Columns("strEnforcementNumber").HeaderText = "Enforcement Number"
            dgvSSCPEnforcement.Columns("strEnforcementNumber").DisplayIndex = 0
            dgvSSCPEnforcement.Columns("Violationdate").HeaderText = "Violation Discovery Date"
            dgvSSCPEnforcement.Columns("Violationdate").DisplayIndex = 1
            dgvSSCPEnforcement.Columns("Violationdate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvSSCPEnforcement.Columns("HPVStatus").HeaderText = "HPV Status"
            dgvSSCPEnforcement.Columns("HPVStatus").DisplayIndex = 2
            dgvSSCPEnforcement.Columns("Status").HeaderText = "Open/Closed"
            dgvSSCPEnforcement.Columns("Status").DisplayIndex = 3
            dgvSSCPEnforcement.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvSSCPEnforcement.Columns("AIRSNumber").DisplayIndex = 4
            dgvSSCPEnforcement.Columns("AIRSNumber").Visible = False

            dgvFCEData.DataSource = dsSSCP
            dgvFCEData.DataMember = "SSCPFCE"

            dgvFCEData.RowHeadersVisible = False
            dgvFCEData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFCEData.AllowUserToResizeColumns = True
            dgvFCEData.AllowUserToAddRows = False
            dgvFCEData.AllowUserToDeleteRows = False
            dgvFCEData.AllowUserToOrderColumns = True
            dgvFCEData.AllowUserToResizeRows = True
            dgvFCEData.Columns("strFCENumber").HeaderText = "FCE Number"
            dgvFCEData.Columns("strFCENumber").DisplayIndex = 5
            dgvFCEData.Columns("strFCENumber").Visible = False
            dgvFCEData.Columns("strFCEStatus").HeaderText = "FCE Status"
            dgvFCEData.Columns("strFCEStatus").DisplayIndex = 4
            dgvFCEData.Columns("strFCEStatus").Visible = False
            dgvFCEData.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
            dgvFCEData.Columns("ReviewingEngineer").DisplayIndex = 2
            dgvFCEData.Columns("FCECompleted").HeaderText = "FCE Complete Date"
            dgvFCEData.Columns("FCECompleted").DisplayIndex = 1
            dgvFCEData.Columns("FCECompleted").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFCEData.Columns("FCEYear").HeaderText = "FCE Year"
            dgvFCEData.Columns("FCEYear").DisplayIndex = 0
            dgvFCEData.Columns("strFCEComments").HeaderText = "FCE Comments"
            dgvFCEData.Columns("strFCEComments").DisplayIndex = 3

            If TCFacilitySummary.TabPages.Contains(TPComplianceWork) Then
                If TCFacilitySummary.TabPages.IndexOf(TPComplianceWork) <> -1 Then
                    TCFacilitySummary.SelectedIndex = TCFacilitySummary.TabPages.IndexOf(TPComplianceWork)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "SSCP Compliance Work"
    Private Sub dgvSSCPEvents_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSSCPEvents.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvSSCPEvents.HitTest(e.X, e.Y)

        Try
            If dgvSSCPEvents.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvSSCPEvents.Columns(1).HeaderText = "Tracking Number" Then
                    txtTrackingNumber.Text = dgvSSCPEvents(1, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvSSCPEnforcement_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSSCPEnforcement.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvSSCPEnforcement.HitTest(e.X, e.Y)

        Try
            If dgvSSCPEnforcement.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvSSCPEnforcement.Columns(0).HeaderText = "Enforcement Number" Then
                    txtEnforcementNumber.Text = dgvSSCPEnforcement(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvFCEData_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFCEData.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFCEData.HitTest(e.X, e.Y)

        Try
            If dgvFCEData.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvFCEData.Columns(4).HeaderText = "FCE Year" Then
                    txtFCEYear.Text = dgvFCEData(4, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewComplianceEvent_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewComplianceEvent.LinkClicked
        Try

            If txtTrackingNumber.Text <> "" Then
                SSCPReports = Nothing
                If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                SSCPReports.txtTrackingNumber.Text = txtTrackingNumber.Text
                SSCPReports.txtOrigin.Text = "Facility Summary"
                SSCPReports.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewSSCPEnforcement_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewSSCPEnforcement.LinkClicked
        Try

            Dim enfNum As String = txtEnforcementNumber.Text
            If enfNum = "" Then Exit Sub
            If DAL.SSCP.EnforcementExists(enfNum) Then
                OpenMultiForm("SscpEnforcement", enfNum)
            Else
                MsgBox("Enforcement number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewFCE_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewFCE.LinkClicked
        Try
            If txtFCEYear.Text <> "" Then
                ViewFCE()
                SSCPFCE.cboFCEYear.Text = txtFCEYear.Text
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewFCE()
        Dim AirProgramCodes As String = ""
        Try

            If chbAPC0.Checked = True Then
                AirProgramCodes = "0 - SIP" & vbCrLf
            End If
            If chbAPC1.Checked = True Then
                AirProgramCodes = AirProgramCodes & "1 - Federal SIP" & vbCrLf
            End If
            If chbAPC3.Checked = True Then
                AirProgramCodes = AirProgramCodes & "3 - Non-Federal SIP" & vbCrLf
            End If
            If chbAPC4.Checked = True Then
                AirProgramCodes = AirProgramCodes & "4 - CFC Tracking" & vbCrLf
            End If
            If chbAPC6.Checked = True Then
                AirProgramCodes = AirProgramCodes & "6 - PSD" & vbCrLf
            End If
            If chbAPC7.Checked = True Then
                AirProgramCodes = AirProgramCodes & "7 - NSR" & vbCrLf
            End If
            If chbAPC8.Checked = True Then
                AirProgramCodes = AirProgramCodes & "8 - NESHAP" & vbCrLf
            End If
            If chbAPC9.Checked = True Then
                AirProgramCodes = AirProgramCodes & "9 - NSPS" & vbCrLf
            End If
            If chbAPCF.Checked = True Then
                AirProgramCodes = AirProgramCodes & "F - FESOP" & vbCrLf
            End If
            If chbAPCA.Checked = True Then
                AirProgramCodes = AirProgramCodes & "A - Acid Precipitation" & vbCrLf
            End If
            If chbAPCI.Checked = True Then
                AirProgramCodes = AirProgramCodes & "I - Native American" & vbCrLf
            End If
            If chbAPCM.Checked = True Then
                AirProgramCodes = AirProgramCodes & "M - MACT" & vbCrLf
            End If
            If chbAPCV.Checked = True Then
                AirProgramCodes = AirProgramCodes & "V - Title V Permit" & vbCrLf
            End If
            If chbAPCRMP.Checked = True Then
                AirProgramCodes = AirProgramCodes & "RMP - Risk Mgmt. Plan" & vbCrLf
            End If

            If AirProgramCodes = "" Then
                AirProgramCodes = "No Air Program Codes available" & vbCrLf
            End If

            AirProgramCodes = Mid(AirProgramCodes, 1, (Len(AirProgramCodes) - 2))


            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary Warning")
                Else
                    SSCPFCE = Nothing
                    If SSCPFCE Is Nothing Then SSCPFCE = New SSCPFCEWork
                    SSCPFCE.txtAirsNumber.Text = Me.mtbAIRSNumber.Text
                    SSCPFCE.txtOrigin.Text = "Facility Summary"
                    SSCPFCE.Show()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Private Sub llbPermittingData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPermittingData.LinkClicked
        Try
            If mtbAIRSNumber.Text = "" Or mtbAIRSNumber.Text.Length <> 8 Then
                mtbAIRSNumber.BackColor = Color.Tomato
                MsgBox("Please select a valid AIRS # first.", MsgBoxStyle.Exclamation, Me.Text)
                If TCFacilitySummary.TabPages.Contains(TPPermittingData) = True Then
                    TCFacilitySummary.TabPages.Remove(TPPermittingData)
                End If
                Exit Sub
            End If
            mtbAIRSNumber.BackColor = Color.White
            dsSSPP = New DataSet

            If TCFacilitySummary.TabPages.Contains(TPPermittingData) = False Then
                TCFacilitySummary.TabPages.Add(TPPermittingData)
            End If

            SQL = "Select  " & _
           "distinct(to_Number(" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,   " & _
           "  case   " & _
           "	when strApplicationTypeDesc IS Null then ' '   " & _
           "Else strApplicationTypeDesc   " & _
           "End as strApplicationType,   " & _
           "case   " & _
           "	when datReceivedDate is Null then ' '   " & _
           "Else to_char(datReceivedDate, 'RRRR-MM-dd')   " & _
           "End as datReceivedDate,   " & _
           "case    " & _
        " when strPermitNumber is NULL then ' '    " & _
        "  else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " & _
        " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " & _
        " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " & _
          " end As strPermitNumber,   " & _
           "case   " & _
   "	when datPermitIssued is Null then ' '   " & _
   "else to_char(datPermitIssued, 'RRRR-MM-dd')   " & _
   "end as datPermitIssued,   " & _
   "case   " & _
   "	when strStaffResponsible = '0' then ' '   " & _
   "	when strStaffResponsible is Null then ' '   " & _
   "else (strLastName||', '||strFirstName)   " & _
   "end as StaffResponsible,   " & _
   "case   " & _
   "	when " & DBNameSpace & ".SSPPApplicationData.strFacilityName is Null then ' '   " & _
   "else " & DBNameSpace & ".SSPPApplicationData.strFacilityName   " & _
   "end as strFacilityName,   " & _
   "case   " & _
   "	when " & DBNameSpace & ".SSPPApplicationMaster.strAIRSNumber is Null then ' '   " & _
   "	when " & DBNameSpace & ".SSPPApplicationMaster.strAIRSNumber = '0413' then ' '   " & _
   "else substr(" & DBNameSpace & ".SSPPApplicationMaster.strAIRSNumber, 5)   " & _
   "end as strAIRSNumber,   " & _
  "case   " & _
  "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " & _
  "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '09 - Administrative Review'   " & _
  "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - Administrative Review'   " & _
  "when datEPAEnds is not Null then '08 - EPA 45-day Review'   " & _
  "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'   " & _
  "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'    " & _
  "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'    " & _
  "when dattoPMII is Not Null then '04 - AT PM'    " & _
  "when dattoPMI is Not Null then '03 - At UC'    " & _
  "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'   " & _
  "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'     " & _
  "else '01 - At Engineer'    " & _
  "end as AppStatus,   " & _
  "   case   " & _
  "	when strPermitTypeDescription is Null then ''   " & _
  "else strPermitTypeDescription   " & _
  "End as strPermitType,   " & _
  "case    " & _
  "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " & _
  "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')   " & _
  "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " & _
  "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " & _
  "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " & _
  "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
  "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " & _
  "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " & _
  "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " & _
  "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " & _
  "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " & _
  "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " & _
  "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " & _
  "end as StatusDate    " & _
   "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationTracking,   " & _
   "" & DBNameSpace & ".SSPPApplicationData,   " & _
   "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".LookUPPermitTypes,   " & _
   "" & DBNameSpace & ".EPDUserProfiles  " & _
   "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber (+)    " & _
   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber (+)   " & _
   "and strApplicationType = strApplicationTypeCode (+)   " & _
   "and strPermitType = strPermitTypeCode (+)   " & _
   "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID (+)   " & _
         "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'   "

            SQL = SQL & SQLLine & "order by strApplicationNumber DESC "

            daSSPP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daSSPP.Fill(dsSSPP, "ApplictionLog")

            SQL = "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
            "'0-SIP' as Subpart, " & _
            "" & DBNameSpace & ".LookUpSUBPartSip.strSubpart, " & _
            "" & DBNameSpace & ".LookUpSubpartSIP.strDescription, " & _
            "" & DBNameSpace & ".APBSubpartData.CreateDateTime " & _
            "from " & DBNameSpace & ".APBSubpartData, " & DBNameSpace & ".LookUpSubPartSIP " & _
            "where " & DBNameSpace & ".APBSubpartData.strSubpart = " & DBNameSpace & ".LookUpSubpartSIP.strSubpart " & _
            "and ACTIVE <> '0' " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 13, 1) = '0' " & _
            "Union " & _
            "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
            "'9-NSPS(Part 60)' as Subpart, " & _
            "" & DBNameSpace & ".LookUpSUBPart60.strSubpart, " & _
            "" & DBNameSpace & ".LookUpSubpart60.strDescription, " & _
            "" & DBNameSpace & ".APBSubpartData.CreateDateTime " & _
            "from " & DBNameSpace & ".APBSubpartData, " & DBNameSpace & ".LookUpSubPart60 " & _
            "where " & DBNameSpace & ".APBSubpartData.strSubpart = " & DBNameSpace & ".LookUpSubpart60.strSubpart " & _
            "and ACTIVE <> '0' " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 13, 1) = '9' " & _
            "Union " & _
            "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
            "'8-NESHAP(Part 61)' as Subpart, " & _
            "" & DBNameSpace & ".LookUpSUBPart61.strSubpart, " & _
            "" & DBNameSpace & ".LookUpSubpart61.strDescription, " & _
            "" & DBNameSpace & ".APBSubpartData.CreateDateTime " & _
            "from " & DBNameSpace & ".APBSubpartData, " & DBNameSpace & ".LookUpSubPart61 " & _
            "where " & DBNameSpace & ".APBSubpartData.strSubpart = " & DBNameSpace & ".LookUpSubpart61.strSubpart " & _
            "and ACTIVE <> '0' " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 13, 1) = '8' " & _
            "UNION " & _
            "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
            "'M-MACT(Part 63)' as Subpart, " & _
            "" & DBNameSpace & ".LookUpSUBPart63.strSubpart, " & _
            "" & DBNameSpace & ".LookUpSubpart63.strDescription, " & _
            "" & DBNameSpace & ".APBSubpartData.CreateDateTime " & _
            "from " & DBNameSpace & ".APBSubpartData, " & DBNameSpace & ".LookUpSubPart63 " & _
            "where " & DBNameSpace & ".APBSubpartData.strSubpart = " & DBNameSpace & ".LookUpSubpart63.strSubpart " & _
            "and ACTIVE <> '0' " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 13, 1) = 'M' "

            daSSPP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daSSPP.Fill(dsSSPP, "ActiveRules")

            SQL = "select " & _
            "'0-SIP' as Subpart,  " & _
            "" & DBNameSpace & ".LookUpSubpartSIP.strSubpart,  " & _
            "" & DBNameSpace & ".LookupSubpartSIP.strDescription,  " & _
            "" & DBNameSpace & ".SSPPSubpartData.strApplicationNumber,  " & _
            "" & DBNameSpace & ".SSPPSubpartData.CreateDateTime,  " & _
            "case  " & _
            "when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity = '1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "End AppActivity  " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPSubpartData,  " & _
            "" & DBNameSpace & ".LookUpSubpartSIP  " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "   " & DBNameSpace & ".SSPPSubpartData.strApplicationNumber  " & _
            "and " & DBNameSpace & ".SSPPSubpartData.strSubPart = " & DBNameSpace & ".LookUPSubpartSIP.strSubpart  " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 6,1) = '0'  " & _
            "union  " & _
            "select  " & _
            "'9-NSPS(Part 60)' as Subpart,  " & _
            "" & DBNameSpace & ".LookUpSubpart60.strSubpart,  " & _
            "" & DBNameSpace & ".LookupSubpart60.strDescription,  " & _
            "" & DBNameSpace & ".SSPPSubpartData.strApplicationNumber,  " & _
            "" & DBNameSpace & ".SSPPSubpartData.CreateDateTime,  " & _
            "case  " & _
            "when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity = '1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "End AppActivity  " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPSubpartData,  " & _
            "" & DBNameSpace & ".LookUpSubpart60  " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "   " & DBNameSpace & ".SSPPSubpartData.strApplicationNumber  " & _
            "and " & DBNameSpace & ".SSPPSubpartData.strSubPart = " & DBNameSpace & ".LookUPSubpart60.strSubpart  " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 6,1) = '9' " & _
            "Union  " & _
            "select  " & _
            "'8-NESHAP(Part 61)' as Subpart,  " & _
            "" & DBNameSpace & ".LookUpSubpart61.strSubpart,  " & _
            "" & DBNameSpace & ".LookupSubpart61.strDescription,  " & _
            "" & DBNameSpace & ".SSPPSubpartData.strApplicationNumber,  " & _
            "" & DBNameSpace & ".SSPPSubpartData.CreateDateTime,  " & _
            "case  " & _
            "when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity = '1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "End AppActivity  " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPSubpartData,  " & _
            "" & DBNameSpace & ".LookUpSubpart61  " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "   " & DBNameSpace & ".SSPPSubpartData.strApplicationNumber  " & _
            "and " & DBNameSpace & ".SSPPSubpartData.strSubPart = " & DBNameSpace & ".LookUPSubpart61.strSubpart  " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 6,1) = '8' " & _
            "Union  " & _
            "select  " & _
            "'M-MACT(Part 63)' as Subpart,  " & _
            "" & DBNameSpace & ".LookUpSubpart63.strSubpart,  " & _
            "" & DBNameSpace & ".LookupSubpart63.strDescription,  " & _
            "" & DBNameSpace & ".SSPPSubpartData.strApplicationNumber,  " & _
            "" & DBNameSpace & ".SSPPSubpartData.CreateDateTime,  " & _
            "case  " & _
            "when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity = '1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "End AppActivity  " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPSubpartData,  " & _
            "" & DBNameSpace & ".LookUpSubpart63  " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "   " & DBNameSpace & ".SSPPSubpartData.strApplicationNumber  " & _
            "and " & DBNameSpace & ".SSPPSubpartData.strSubPart = " & DBNameSpace & ".LookUPSubpart63.strSubpart  " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 6,1) = 'M'"

            daSSPP = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daSSPP.Fill(dsSSPP, "RuleHistory")

            dgvApplicationLog.DataSource = dsSSPP
            dgvApplicationLog.DataMember = "ApplictionLog"

            dgvApplicationLog.RowHeadersVisible = False
            dgvApplicationLog.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvApplicationLog.AllowUserToResizeColumns = True
            dgvApplicationLog.AllowUserToAddRows = False
            dgvApplicationLog.AllowUserToDeleteRows = False
            dgvApplicationLog.AllowUserToOrderColumns = True
            dgvApplicationLog.AllowUserToResizeRows = True
            dgvApplicationLog.Columns("strApplicationNumber").HeaderText = "APL #"
            dgvApplicationLog.Columns("strApplicationNumber").DisplayIndex = 0
            dgvApplicationLog.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvApplicationLog.Columns("strFacilityName").DisplayIndex = 1
            dgvApplicationLog.Columns("strAIRSNumber").HeaderText = "AIRS #"
            dgvApplicationLog.Columns("strAIRSNumber").DisplayIndex = 2
            dgvApplicationLog.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvApplicationLog.Columns("StaffResponsible").DisplayIndex = 3
            dgvApplicationLog.Columns("strApplicationType").HeaderText = "APL Type"
            dgvApplicationLog.Columns("strApplicationType").DisplayIndex = 4
            dgvApplicationLog.Columns("datReceivedDate").HeaderText = "APL Rcvd"
            dgvApplicationLog.Columns("datReceivedDate").DisplayIndex = 5
            dgvApplicationLog.Columns("datReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvApplicationLog.Columns("datPermitIssued").HeaderText = "Permit Issued"
            dgvApplicationLog.Columns("datPermitIssued").DisplayIndex = 6
            dgvApplicationLog.Columns("datPermitIssued").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvApplicationLog.Columns("strPermitNumber").HeaderText = "Permit Number"
            dgvApplicationLog.Columns("strPermitNumber").DisplayIndex = 7
            dgvApplicationLog.Columns("strPermitType").HeaderText = "Action Type"
            dgvApplicationLog.Columns("strPermitType").DisplayIndex = 8
            dgvApplicationLog.Columns("AppStatus").HeaderText = "App Status"
            dgvApplicationLog.Columns("AppStatus").DisplayIndex = 9
            dgvApplicationLog.Columns("StatusDate").HeaderText = "Status Date"
            dgvApplicationLog.Columns("StatusDate").DisplayIndex = 10
            dgvApplicationLog.Columns("StatusDate").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvActiveRules.DataSource = dsSSPP
            dgvActiveRules.DataMember = "ActiveRules"

            dgvActiveRules.RowHeadersVisible = False
            dgvActiveRules.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvActiveRules.AllowUserToResizeColumns = True
            dgvActiveRules.AllowUserToAddRows = False
            dgvActiveRules.AllowUserToDeleteRows = False
            dgvActiveRules.AllowUserToOrderColumns = True
            dgvActiveRules.AllowUserToResizeRows = True
            dgvActiveRules.Columns("Subpart").HeaderText = "Subpart"
            dgvActiveRules.Columns("Subpart").DisplayIndex = 0
            dgvActiveRules.Columns("strSubpart").HeaderText = "Rule"
            dgvActiveRules.Columns("strSubpart").DisplayIndex = 1
            dgvActiveRules.Columns("strDescription").HeaderText = "Description"
            dgvActiveRules.Columns("strDescription").DisplayIndex = 2
            dgvActiveRules.Columns("strDescription").Width = 400
            dgvActiveRules.Columns("CreateDateTime").HeaderText = "Initial Applicability"
            dgvActiveRules.Columns("CreateDateTime").DisplayIndex = 3
            dgvActiveRules.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvActiveRules.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvActiveRules.Columns("AIRSNumber").DisplayIndex = 4
            dgvActiveRules.Columns("AIRSNumber").Visible = False

            dgvRuleHistory.DataSource = dsSSPP
            dgvRuleHistory.DataMember = "RuleHistory"

            dgvRuleHistory.RowHeadersVisible = False
            dgvRuleHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvRuleHistory.AllowUserToResizeColumns = True
            dgvRuleHistory.AllowUserToAddRows = False
            dgvRuleHistory.AllowUserToDeleteRows = False
            dgvRuleHistory.AllowUserToOrderColumns = True
            dgvRuleHistory.AllowUserToResizeRows = True
            dgvRuleHistory.Columns("strApplicationNumber").HeaderText = "App #"
            dgvRuleHistory.Columns("strApplicationNumber").DisplayIndex = 0
            dgvRuleHistory.Columns("AppActivity").HeaderText = "Action"
            dgvRuleHistory.Columns("AppActivity").DisplayIndex = 1
            dgvRuleHistory.Columns("Subpart").HeaderText = "Subpart"
            dgvRuleHistory.Columns("Subpart").DisplayIndex = 2
            dgvRuleHistory.Columns("strSubpart").HeaderText = "Rule"
            dgvRuleHistory.Columns("strSubpart").DisplayIndex = 3
            dgvRuleHistory.Columns("strDescription").HeaderText = "Description"
            dgvRuleHistory.Columns("strDescription").DisplayIndex = 4
            dgvRuleHistory.Columns("strDescription").Width = 250
            dgvRuleHistory.Columns("CreateDateTime").HeaderText = "Action Date"
            dgvRuleHistory.Columns("CreateDateTime").DisplayIndex = 5
            dgvRuleHistory.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

            If TCFacilitySummary.TabPages.Contains(TPPermittingData) Then
                If TCFacilitySummary.TabPages.IndexOf(TPPermittingData) <> -1 Then
                    TCFacilitySummary.SelectedIndex = TCFacilitySummary.TabPages.IndexOf(TPPermittingData)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "SSPP Permitting Work"
    Private Sub dgvApplicationLog_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvApplicationLog.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvApplicationLog.HitTest(e.X, e.Y)

        Try
            If dgvApplicationLog.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvApplicationLog.Columns(0).HeaderText = "APL #" Then
                    txtApplicationNumber.Text = dgvApplicationLog(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewApplication_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewApplication.LinkClicked
        Try

            If txtApplicationNumber.Text <> "" Then
                If PermitTrackingLog Is Nothing Then
                    PermitTrackingLog = Nothing
                    If PermitTrackingLog Is Nothing Then PermitTrackingLog = New SSPPApplicationTrackingLog
                    PermitTrackingLog.Show()
                Else
                    PermitTrackingLog.Show()
                End If
                PermitTrackingLog.txtApplicationNumber.Clear()
                PermitTrackingLog.txtApplicationNumber.Text = txtApplicationNumber.Text
                PermitTrackingLog.LoadApplication()
                PermitTrackingLog.BringToFront()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
    Private Sub llbPlanningSupport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPlanningSupport.LinkClicked
        Try
            If mtbAIRSNumber.Text = "" Or mtbAIRSNumber.Text.Length <> 8 Then
                mtbAIRSNumber.BackColor = Color.Tomato
                MsgBox("Please select a valid AIRS # first.", MsgBoxStyle.Exclamation, Me.Text)
                If TCFacilitySummary.TabPages.Contains(TPPlanningSupportData) = True Then
                    TCFacilitySummary.TabPages.Remove(TPPlanningSupportData)
                End If
                Exit Sub
            End If
            mtbAIRSNumber.BackColor = Color.White
            ds = New DataSet

            If TCFacilitySummary.TabPages.Contains(TPPlanningSupportData) = False Then
                TCFacilitySummary.TabPages.Add(TPPlanningSupportData)
            End If

            SQL = "select substr(" & DBNameSpace & ".FS_FeeAuditedData.strAIRSNumber, 5) as AIRSNumber, " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.numFeeYear,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.intVOCTons,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.intPMTons,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.intSO2Tons,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.intNOXTons,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.numPart70Fee,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.numSMFee,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.numTotalFee,  " & _
            "case " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strNSPSExempt = '1' then 'YES' " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strNSPSExempt = '0' then 'NO' " & _
            "end strNSPSExempt, " & _
            "'' as strNSPSReason,  " & _
            "case " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strOperate = '1' then 'YES' " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strOperate = '0' then 'NO' " & _
            "end strOperate, " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.numFeeRate,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.strNSPSExemptReason,  " & _
            "case " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strPart70 = '1' then 'YES' " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strPart70 = '0' then 'NO' " & _
            "end strPart70, " & _
            "case " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strSyntheticMinor = '1' then 'YES' " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strSyntheticMinor = '0' then 'NO' " & _
            "End strSyntheticMinor, " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.numCalculatedFee,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.strClass,  " & _
            "case " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strNSPS = '1' then 'YES' " & _
            "when " & DBNameSpace & ".FS_FeeAuditedData.strNSPS = '0' then 'NO' " & _
            "end strNSPS,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.datShutDown,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.strPaymentPlan,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.strOfficialName,  " & _
            "" & DBNameSpace & ".FS_FeeAuditedData.strOfficialTitle,  " & _
            "case " & _
            "when intSubmittal = '1' then 'YES' " & _
            "when intSubmittal = '0' then 'NO' " & _
            "end intSubmittal, " & _
            "datSubmittal " & _
            "from " & DBNameSpace & ".FS_FeeAuditedData, " & DBNameSpace & ".FS_Admin " & _
            "where " & DBNameSpace & ".FS_FeeAuditedData.strAIRSNUmber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and " & DBNameSpace & ".FS_FeeAuditedData.strAIRSnumber = " & DBNameSpace & ".FS_Admin.strAIRSnumber  " & _
            "and " & DBNameSpace & ".FS_FeeAuditedData.numFeeYear = " & DBNameSpace & ".FS_Admin.numFeeYear " & _
            "and " & DBNameSpace & ".FS_Admin.active = '1' " & _
            "and strEnrolled is not null " & _
            "and strenrolled = '1'" & _
            "order by " & DBNameSpace & ".FS_FeeAuditedData.numFeeYear desc "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "FeeData")


            SQL = "select distinct " & _
            "" & DBNameSpace & ".FS_FeeInvoice.numFeeYear, " & _
           "" & DBNameSpace & ".FS_FeeInvoice.InvoiceID, " & _
           "" & DBNameSpace & ".FS_FeeInvoice.numAmount, " & _
           "datInvoiceDate, " & _
           "case " & _
           "when " & DBNameSpace & ".FS_FeeInvoice.active = '1' then 'Active' " & _
           "when " & DBNameSpace & ".FS_FeeInvoice.active = '0' then 'VOID' " & _
           "end InvoiceStatus, strPayTypeDesc, " & _
           "case " & _
           "when strInvoiceStatus = '1' then 'Paid in Full' " & _
           "when strInvoiceStatus = '0' and " & _
           "(numPayment <> '0' and numPayment is not null and " & DBNameSpace & ".FS_Transactions.active = '1') then 'Partial Payment' " & _
           "when strInvoicestatus = '0' then 'Unpaid' " & _
           "end PayStatus, " & _
           "" & DBNameSpace & ".FS_FeeInvoice.strComment " & _
           "from " & DBNameSpace & ".FS_FeeInvoice, " & DBNameSpace & ".FSLK_PayType, " & _
           "" & DBNameSpace & ".FS_Transactions " & _
           "where " & DBNameSpace & ".FS_FeeInvoice.strPayType = " & DBNameSpace & ".FSLK_PayType.nuMPayTypeID " & _
           "and " & DBNameSpace & ".FS_FeeInvoice.InvoiceID = " & DBNameSpace & ".FS_Transactions.InvoiceID (+) " & _
           "and " & DBNameSpace & ".FS_FeeInvoice.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
           "order by numFeeyear desc, datInvoiceDate desc  "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "FeeInvoice")

            SQL = "Select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber, numFeeYear,  " & _
            "numPayment, dattransactiondate, Invoiceid, strCheckNo,  " & _
            "strDepositNo, " & _
            "case " & _
            "when TRANSACTIONTYPECODE = '1' then 'Deposit' " & _
            "when TRANSACTIONTYPECODE = '2' then 'Refund' " & _
            "else 'N/A' " & _
            "end TRANSACTIONTYPECODE, " & _
            "strBatchNo,  " & _
            "case " & _
            "when strEntryPerson is null then '' " & _
            "else (strLastName||', '||strFirstName) " & _
            "end strEntryPerson, " & _
            "strComment,  " & _
            "transactionid  " & _
            "from " & DBNameSpace & ".FS_Transactions, " & DBNameSpace & ".EPDUserProfiles " & _
            "where " & DBNameSpace & ".FS_Transactions.strEntryPerson = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and Active = '1' " & _
            "order by numFeeYear desc, dattransactiondate desc "

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "FeeDeposits")

            dgvFeeData.DataSource = ds
            dgvFeeData.DataMember = "FeeData"

            dgvFeeData.RowHeadersVisible = False
            dgvFeeData.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvFeeData.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeData.Columns("AIRSNumber").Width = 100
            dgvFeeData.Columns("numFeeYear").HeaderText = "Year"
            dgvFeeData.Columns("numFeeYear").DisplayIndex = 1
            dgvFeeData.Columns("numFeeYear").Width = 80
            dgvFeeData.Columns("intVOCTons").HeaderText = "VOC Tons"
            dgvFeeData.Columns("intVOCTons").DisplayIndex = 2
            dgvFeeData.Columns("intVOCTons").Width = 100
            dgvFeeData.Columns("intPMTons").HeaderText = "PM Tons"
            dgvFeeData.Columns("intPMTons").DisplayIndex = 3
            dgvFeeData.Columns("intPMTons").Width = 80
            dgvFeeData.Columns("intSO2Tons").HeaderText = "SO2 Tons"
            dgvFeeData.Columns("intSO2Tons").DisplayIndex = 4
            dgvFeeData.Columns("intSO2Tons").Width = 100
            dgvFeeData.Columns("intNOXTons").HeaderText = "NOx Tons"
            dgvFeeData.Columns("intNOXTons").DisplayIndex = 5
            dgvFeeData.Columns("intNOXTons").Width = 100
            dgvFeeData.Columns("numPart70Fee").HeaderText = "Part 70 Fees"
            dgvFeeData.Columns("numPart70Fee").DisplayIndex = 6
            dgvFeeData.Columns("numPart70Fee").Width = 100
            dgvFeeData.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            dgvFeeData.Columns("numSMFee").HeaderText = "SM Fees"
            dgvFeeData.Columns("numSMFee").DisplayIndex = 7
            dgvFeeData.Columns("numSMFee").Width = 100
            dgvFeeData.Columns("numSMFee").DefaultCellStyle.Format = "c"
            dgvFeeData.Columns("numTotalFee").HeaderText = "Total Fees"
            dgvFeeData.Columns("numTotalFee").DisplayIndex = 8
            dgvFeeData.Columns("numTotalFee").Width = 100
            dgvFeeData.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvFeeData.Columns("strNSPSExempt").HeaderText = "NSPS Exempt"
            dgvFeeData.Columns("strNSPSExempt").DisplayIndex = 9
            dgvFeeData.Columns("strNSPSExempt").Width = 100
            dgvFeeData.Columns("strNSPSReason").HeaderText = "NSPS Exempt Reason"
            dgvFeeData.Columns("strNSPSReason").DisplayIndex = 10
            dgvFeeData.Columns("strNSPSReason").Width = 200
            dgvFeeData.Columns("strOperate").HeaderText = "Operating"
            dgvFeeData.Columns("strOperate").DisplayIndex = 11
            dgvFeeData.Columns("strOperate").Width = 100
            dgvFeeData.Columns("numFeeRate").HeaderText = "Fee Rate"
            dgvFeeData.Columns("numFeeRate").DisplayIndex = 12
            dgvFeeData.Columns("numFeeRate").Width = 100
            dgvFeeData.Columns("numFeeRate").DefaultCellStyle.Format = "c"
            dgvFeeData.Columns("strNSPSExemptReason").HeaderText = "NSPS Exempt Reason"
            dgvFeeData.Columns("strNSPSExemptReason").DisplayIndex = 13
            dgvFeeData.Columns("strNSPSExemptReason").Width = 200
            dgvFeeData.Columns("strPart70").HeaderText = "Part 70 Status"
            dgvFeeData.Columns("strPart70").DisplayIndex = 14
            dgvFeeData.Columns("strPart70").Width = 100
            dgvFeeData.Columns("strSyntheticMinor").HeaderText = "SM Status"
            dgvFeeData.Columns("strSyntheticMinor").DisplayIndex = 15
            dgvFeeData.Columns("strSyntheticMinor").Width = 100
            dgvFeeData.Columns("numCalculatedFee").HeaderText = "Calculated Fees"
            dgvFeeData.Columns("numCalculatedFee").DisplayIndex = 16
            dgvFeeData.Columns("numCalculatedFee").Width = 100
            dgvFeeData.Columns("numCalculatedFee").DefaultCellStyle.Format = "c"
            dgvFeeData.Columns("strClass").HeaderText = "Classification"
            dgvFeeData.Columns("strClass").DisplayIndex = 17
            dgvFeeData.Columns("strClass").Width = 100
            dgvFeeData.Columns("strNSPS").HeaderText = "NSPS"
            dgvFeeData.Columns("strNSPS").DisplayIndex = 18
            dgvFeeData.Columns("strNSPS").Width = 100
            dgvFeeData.Columns("datShutDown").HeaderText = "Shutdown Date"
            dgvFeeData.Columns("datShutDown").DisplayIndex = 19
            dgvFeeData.Columns("datShutDown").Width = 100

            dgvFeeData.Columns("strPaymentPlan").HeaderText = "Payment Type"
            dgvFeeData.Columns("strPaymentPlan").DisplayIndex = 20
            dgvFeeData.Columns("strPaymentPlan").Width = 100
            dgvFeeData.Columns("strOfficialName").HeaderText = "Official Name"
            dgvFeeData.Columns("strOfficialName").DisplayIndex = 21
            dgvFeeData.Columns("strOfficialName").Width = 100
            dgvFeeData.Columns("strOfficialTitle").HeaderText = "Official Title"
            dgvFeeData.Columns("strOfficialTitle").DisplayIndex = 22
            dgvFeeData.Columns("strOfficialTitle").Width = 100
            dgvFeeData.Columns("intSubmittal").HeaderText = "Submitted"
            dgvFeeData.Columns("intSubmittal").DisplayIndex = 23
            dgvFeeData.Columns("intSubmittal").Width = 75

            dgvFeeData.Columns("datSubmittal").HeaderText = "Date Submitted"
            dgvFeeData.Columns("datSubmittal").DisplayIndex = 24
            dgvFeeData.Columns("datSubmittal").Width = 100
            dgvFeeData.Columns("datSubmittal").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvInvoices.DataSource = ds
            dgvInvoices.DataMember = "FeeInvoice"

            dgvInvoices.RowHeadersVisible = False
            dgvInvoices.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvInvoices.AllowUserToResizeColumns = True
            dgvInvoices.AllowUserToAddRows = False
            dgvInvoices.AllowUserToDeleteRows = False
            dgvInvoices.AllowUserToOrderColumns = True
            dgvInvoices.AllowUserToResizeRows = True

            dgvInvoices.Columns("InvoiceID").HeaderText = "ID"
            dgvInvoices.Columns("InvoiceID").DisplayIndex = 0
            dgvInvoices.Columns("InvoiceID").Width = 40
            dgvInvoices.Columns("numFeeYear").HeaderText = "Fee Year"
            dgvInvoices.Columns("numFeeYear").DisplayIndex = 1
            dgvInvoices.Columns("numFeeYear").Width = 40
            dgvInvoices.Columns("numAmount").HeaderText = "Invoice Amount"
            dgvInvoices.Columns("numAmount").DisplayIndex = 2
            dgvInvoices.Columns("numAmount").Width = 100
            dgvInvoices.Columns("numAmount").DefaultCellStyle.Format = "c"
            dgvInvoices.Columns("datInvoiceDate").HeaderText = "Invoice Date"
            dgvInvoices.Columns("datInvoiceDate").DisplayIndex = 3
            dgvInvoices.Columns("datInvoiceDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvInvoices.Columns("datInvoiceDate").Width = 85
            dgvInvoices.Columns("InvoiceStatus").HeaderText = "Invoice Status"
            dgvInvoices.Columns("InvoiceStatus").DisplayIndex = 4
            dgvInvoices.Columns("strPayTypeDesc").HeaderText = "Invoice Type"
            dgvInvoices.Columns("strPayTypeDesc").DisplayIndex = 5
            dgvInvoices.Columns("PayStatus").HeaderText = "Pay Status"
            dgvInvoices.Columns("PayStatus").DisplayIndex = 6
            dgvInvoices.Columns("strComment").HeaderText = "Comment"
            dgvInvoices.Columns("strComment").DisplayIndex = 7

            dgvFeeDeposits.DataSource = ds
            dgvFeeDeposits.DataMember = "FeeDeposits"

            dgvFeeDeposits.RowHeadersVisible = False
            dgvFeeDeposits.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvFeeDeposits.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeDeposits.Columns("AIRSNumber").Width = 100
            dgvFeeDeposits.Columns("numFeeyEar").HeaderText = "Year"
            dgvFeeDeposits.Columns("numFeeyEar").DisplayIndex = 1
            dgvFeeDeposits.Columns("numFeeyEar").Width = 80
            dgvFeeDeposits.Columns("numPayment").HeaderText = "Amount Paid"
            dgvFeeDeposits.Columns("numPayment").DisplayIndex = 2
            dgvFeeDeposits.Columns("numPayment").Width = 100
            dgvFeeDeposits.Columns("numPayment").DefaultCellStyle.Format = "c"
            dgvFeeDeposits.Columns("dattransactiondate").HeaderText = "Pay date"
            dgvFeeDeposits.Columns("dattransactiondate").DisplayIndex = 3
            dgvFeeDeposits.Columns("dattransactiondate").Width = 80
            dgvFeeDeposits.Columns("dattransactiondate").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvFeeDeposits.Columns("Invoiceid").HeaderText = "Invoice #"
            dgvFeeDeposits.Columns("Invoiceid").DisplayIndex = 4
            dgvFeeDeposits.Columns("Invoiceid").Width = 100
            dgvFeeDeposits.Columns("strCheckNo").HeaderText = "Check No."
            dgvFeeDeposits.Columns("strCheckNo").DisplayIndex = 5
            dgvFeeDeposits.Columns("strCheckNo").Width = 100
            dgvFeeDeposits.Columns("strDepositNo").HeaderText = "Deposit #"
            dgvFeeDeposits.Columns("strDepositNo").DisplayIndex = 6
            dgvFeeDeposits.Columns("strDepositNo").Width = 100
            dgvFeeDeposits.Columns("TRANSACTIONTYPECODE").HeaderText = "Pay Type"
            dgvFeeDeposits.Columns("TRANSACTIONTYPECODE").DisplayIndex = 7
            dgvFeeDeposits.Columns("TRANSACTIONTYPECODE").Width = 150
            dgvFeeDeposits.Columns("strBatchNo").HeaderText = "Batch No"
            dgvFeeDeposits.Columns("strBatchNo").DisplayIndex = 8
            dgvFeeDeposits.Columns("strBatchNo").Width = 100
            dgvFeeDeposits.Columns("strEntryPerson").HeaderText = "Entry Person"
            dgvFeeDeposits.Columns("strEntryPerson").DisplayIndex = 9
            dgvFeeDeposits.Columns("strEntryPerson").Width = 150
            dgvFeeDeposits.Columns("strComment").HeaderText = "Comments"
            dgvFeeDeposits.Columns("strComment").DisplayIndex = 10
            dgvFeeDeposits.Columns("strComment").Width = 200

            dgvFeeDeposits.Columns("transactionid").HeaderText = "Transaction ID"
            dgvFeeDeposits.Columns("transactionid").DisplayIndex = 11
            dgvFeeDeposits.Columns("transactionid").Width = 50

            If TCFacilitySummary.TabPages.Contains(TPPlanningSupportData) Then
                If TCFacilitySummary.TabPages.IndexOf(TPPlanningSupportData) <> -1 Then
                    TCFacilitySummary.SelectedIndex = TCFacilitySummary.TabPages.IndexOf(TPPlanningSupportData)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOpenFacilityLocationEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFacilityLocationEditor.Click
        Try
            If EditFacilityLocation Is Nothing Then
                EditFacilityLocation = New IAIPEditFacilityLocation
                EditFacilityLocation.txtAirsNumber.Text = mtbAIRSNumber.Text
                EditFacilityLocation.Show()
            Else
                EditFacilityLocation.txtAirsNumber.Text = mtbAIRSNumber.Text
                EditFacilityLocation.Show()
                EditFacilityLocation.BringToFront()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditHeaderData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditHeaderData.Click
        If Apb.Facility.ValidAirsNumber(mtbAIRSNumber.Text) Then

            Dim editHeaderDataDialog As New IAIPEditHeaderData
            editHeaderDataDialog.AirsNumber = mtbAIRSNumber.Text
            editHeaderDataDialog.FacilityName = txtFacilityName.Text

            editHeaderDataDialog.ShowDialog()

            If editHeaderDataDialog.SomethingWasSaved Then
                LoadInitialData()
            End If

            editHeaderDataDialog.Dispose()
        End If
    End Sub

    Private Sub btnEditContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditContacts.Click
        OpenEditContactInformationTool()
    End Sub

#Region "SSCP Menu"

    Private Sub mmiSSCPAssignEngineer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSSCPAssignEngineer.Click
        Try
            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary")
                Else
                    SSCPFacAssign = Nothing
                    If SSCPFacAssign Is Nothing Then SSCPFacAssign = New SSCPFacAssignment
                    SSCPFacAssign.txtFacilityName.Text = Me.txtFacilityName.Text
                    SSCPFacAssign.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
                    SSCPFacAssign.Show()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiSSCPNewWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSSCPNewWork.Click
        Try
            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary Warning")
                Else
                    SSCPEngWork = Nothing
                    If SSCPEngWork Is Nothing Then SSCPEngWork = New SSCPWorkEnTry
                    SSCPEngWork.txtFacilityName.Text = Me.txtFacilityName.Text
                    SSCPEngWork.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
                    SSCPEngWork.Show()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiSSCPFCE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSSCPFCE.Click
        Try
            ViewFCE()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "ISMP Menu"

    Private Sub mmiISMPNewLogEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiISMPNewLogEnTry.Click
        Try
            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary")
                Else
                    ISMPNotificationLogForm = Nothing
                    If ISMPNotificationLogForm Is Nothing Then ISMPNotificationLogForm = New ISMPNotificationLog
                    ISMPNotificationLogForm.txtTestNotificationNumber.Text = Me.txtTestingNumber.Text
                    ISMPNotificationLogForm.Show()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiISMPAddMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiISMPAddMemo.Click
        Try
            ISMPMemoEdit = Nothing
            If ISMPMemoEdit Is Nothing Then ISMPMemoEdit = New ISMPMemo
            ISMPMemoEdit.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
            ISMPMemoEdit.Show()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "AFS Updates"
    Private Sub mmiAddAFS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAddAFS.Click
        Try
            SQL = "Update " & DBNameSpace & ".AFSFacilityData set " & _
            "strUpdateStatus = 'A' " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update " & DBNameSpace & ".AFSAirPollutantData set " & _
            "strUpdateStatus = 'A' " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBranch.AFSSSPPRecords set " & _
            "strUpdateStatus = 'A'  " & _
            "where exists  " & _
            "(select AIRBranch.SSPPApplicationmaster.strApplicationNumber  " & _
            "from AIRBranch.SSPPApplicationmaster  " & _
            "where AIRBranch.SSPPApplicationmaster.strapplicationNumber =  " & _
            "airbranch.AFSSSPPrecords.strApplicationNumber  " & _
            "and AIRbranch.SSPPApplicationMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBranch.AFSSSCPRecords set " & _
            "strUpdateStatus = 'A'  " & _
            "where exists  " & _
            "(select AIRBranch.SSCPItemMaster.strTrackingNumber  " & _
            "from AIRBranch.SSCPItemMaster  " & _
            "where AIRBranch.SSCPItemMaster.strTrackingNumber =  " & _
            "       airbranch.AFSSSCPRecords.strTrackingNumber  " & _
            "and AIRbranch.SSCPItemMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBranch.AFSISMPRecords set " & _
            "strUpdateStatus = 'A'  " & _
            "where exists  " & _
            "(select AIRBranch.ISMPMaster.strReferenceNumber  " & _
            "from AIRBranch.ISMPMaster  " & _
            "where AIRBranch.ISMPMaster.strReferenceNumber =  " & _
            "       airbranch.AFSISMPRecords.strReferenceNumber  " & _
            "and AIRbranch.ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "


            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update " & DBNameSpace & ".AFSSSCPEnforcementRecords set " & _
           "strUpdateStatus = 'A' " & _
           "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            SQL = "update AIRBranch.AFSSSCPEnforcementRecords set " & _
           "strUpdateStatus = 'A'  " & _
           "where exists  " & _
           "(select AIRBranch.SSCPEnforcementItems.strEnforcementNumber  " & _
           "from AIRBranch.SSCPEnforcementItems  " & _
           "where AIRBranch.SSCPEnforcementItems.strEnforcementNumber =  " & _
           "       airbranch.AFSSSCPEnforcementRecords.strEnforcementNumber  " & _
           "and AIRbranch.SSCPEnforcementItems.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "


            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update " & DBNameSpace & ".AFSSSCPFCERecords set " & _
    "strUpdateStatus = 'A' " & _
    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            SQL = "update AIRBranch.AFSSSCPFCERecords set " & _
        "strUpdateStatus = 'A'  " & _
        "where exists  " & _
        "(select AIRBranch.SSCPFCEMaster.strFCENumber  " & _
        "from AIRBranch.SSCPFCEMaster  " & _
        "where AIRBranch.SSCPFCEMaster.strFCENumber =  " & _
        "       airbranch.AFSSSCPFCERecords.strFCENumber  " & _
        "and AIRbranch.SSCPFCEMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Done", MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiUpdateAFSData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiUpdateAFSData.Click
        Try
            SQL = "Update " & DBNameSpace & ".AFSFacilityData set " & _
            "strUpdateStatus = 'C' " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update " & DBNameSpace & ".AFSAirPollutantData set " & _
            "strUpdateStatus = 'C' " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBranch.AFSSSPPRecords set " & _
            "strUpdateStatus = 'C'  " & _
            "where exists  " & _
            "(select AIRBranch.SSPPApplicationmaster.strApplicationNumber  " & _
            "from AIRBranch.SSPPApplicationmaster  " & _
            "where AIRBranch.SSPPApplicationmaster.strapplicationNumber =  " & _
            "airbranch.AFSSSPPrecords.strApplicationNumber  " & _
            "and AIRbranch.SSPPApplicationMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBranch.AFSSSCPRecords set " & _
            "strUpdateStatus = 'C'  " & _
            "where exists  " & _
            "(select AIRBranch.SSCPItemMaster.strTrackingNumber  " & _
            "from AIRBranch.SSCPItemMaster  " & _
            "where AIRBranch.SSCPItemMaster.strTrackingNumber =  " & _
            "       airbranch.AFSSSCPRecords.strTrackingNumber  " & _
            "and AIRbranch.SSCPItemMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBranch.AFSISMPRecords set " & _
            "strUpdateStatus = 'C'  " & _
            "where exists  " & _
            "(select AIRBranch.ISMPMaster.strReferenceNumber  " & _
            "from AIRBranch.ISMPMaster  " & _
            "where AIRBranch.ISMPMaster.strReferenceNumber =  " & _
            "       airbranch.AFSISMPRecords.strReferenceNumber  " & _
            "and AIRbranch.ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "


            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBranch.AFSSSCPEnforcementRecords set " & _
           "strUpdateStatus = 'C'  " & _
           "where exists  " & _
           "(select AIRBranch.SSCPEnforcementItems.strEnforcementNumber  " & _
           "from AIRBranch.SSCPEnforcementItems  " & _
           "where AIRBranch.SSCPEnforcementItems.strEnforcementNumber =  " & _
           "       airbranch.AFSSSCPEnforcementRecords.strEnforcementNumber  " & _
           "and AIRbranch.SSCPEnforcementItems.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "


            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBranch.AFSSSCPFCERecords set " & _
        "strUpdateStatus = 'C'  " & _
        "where exists  " & _
        "(select AIRBranch.SSCPFCEMaster.strFCENumber  " & _
        "from AIRBranch.SSCPFCEMaster  " & _
        "where AIRBranch.SSCPFCEMaster.strFCENumber =  " & _
        "       airbranch.AFSSSCPFCERecords.strFCENumber  " & _
        "and AIRbranch.SSCPFCEMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Done", MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

#Region "Menu and toolbar"

    Private Sub TBFacilitySummary_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilitySummary.ButtonClick
        Select Case TBFacilitySummary.Buttons.IndexOf(e.Button)
            Case 0
                OpenFacilityLookupTool()
            Case 1
                ClearPage()
            Case 2
                OpenFacilitySummaryPrintTool()
        End Select
    End Sub

    Private Sub mmiFacilityLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiFacilityLookup.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub mmiPrintFacilitySummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPrintFacilitySummary.Click
        OpenFacilitySummaryPrintTool()
    End Sub

    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        ClearPage()
    End Sub

    Private Sub mmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub

    Private Sub mmiEditContactInformation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiEditContactInformation.Click
        OpenEditContactInformationTool()
    End Sub

    Private Sub mmiNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiNewFacility.Click
        Try
            FacilityCreator = Nothing
            If FacilityCreator Is Nothing Then FacilityCreator = New IAIPFacilityCreator
            FacilityCreator.Show()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

#End Region

End Class