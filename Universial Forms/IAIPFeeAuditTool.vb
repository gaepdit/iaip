Imports System.Data.OracleClient

Public Class IAIPFeeAuditTool
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Private Sub IAIPFeeAuditTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
         
            cboOperatingStatus.Items.Add("")
            cboOperatingStatus.Items.Add("O-Operating")
            cboOperatingStatus.Items.Add("P-Planned")
            cboOperatingStatus.Items.Add("C - Under Construction")
            cboOperatingStatus.Items.Add("T - Temporarily Closed")
            cboOperatingStatus.Items.Add("X - Closed/Dismantled")
            cboOperatingStatus.Items.Add("I - Seasonal Operation")

            cboOperatingStatus_CY2008.Items.Add("")
            cboOperatingStatus_CY2008.Items.Add("O-Operating")
            cboOperatingStatus_CY2008.Items.Add("P-Planned")
            cboOperatingStatus_CY2008.Items.Add("C - Under Construction")
            cboOperatingStatus_CY2008.Items.Add("T - Temporarily Closed")
            cboOperatingStatus_CY2008.Items.Add("X - Closed/Dismantled")
            cboOperatingStatus_CY2008.Items.Add("I - Seasonal Operation")

            cboOperatingStatus_CY2007.Items.Add("")
            cboOperatingStatus_CY2007.Items.Add("O-Operating")
            cboOperatingStatus_CY2007.Items.Add("P-Planned")
            cboOperatingStatus_CY2007.Items.Add("C - Under Construction")
            cboOperatingStatus_CY2007.Items.Add("T - Temporarily Closed")
            cboOperatingStatus_CY2007.Items.Add("X - Closed/Dismantled")
            cboOperatingStatus_CY2007.Items.Add("I - Seasonal Operation")

            cboOperatingStatus_CY2006.Items.Add("")
            cboOperatingStatus_CY2006.Items.Add("O-Operating")
            cboOperatingStatus_CY2006.Items.Add("P-Planned")
            cboOperatingStatus_CY2006.Items.Add("C - Under Construction")
            cboOperatingStatus_CY2006.Items.Add("T - Temporarily Closed")
            cboOperatingStatus_CY2006.Items.Add("X - Closed/Dismantled")
            cboOperatingStatus_CY2006.Items.Add("I - Seasonal Operation")

            Panel24.Visible = False
            Panel19.Visible = False
            Panel44.Visible = False
            Panel34.Visible = False
            Panel45.Visible = False
            Panel35.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbNoteChanges_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbNoteChanges.LinkClicked
        Try
            If Panel13.Visible = True Then
                Panel13.Visible = False
            Else
                Panel13.Visible = True
            End If
            If Panel10.Visible = True Then
                Panel10.Visible = False
            Else
                Panel10.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbNoteChanges_CY2008_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbNoteChanges_CY2008.LinkClicked
        Try
            If Panel24.Visible = True Then
                Panel24.Visible = False
            Else
                Panel24.Visible = True
            End If
            If Panel19.Visible = True Then
                Panel19.Visible = False
            Else
                Panel19.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbNoteChanges_CY2007_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbNoteChanges_CY2007.LinkClicked
        Try
            If Panel44.Visible = True Then
                Panel44.Visible = False
            Else
                Panel44.Visible = True
            End If
            If Panel34.Visible = True Then
                Panel34.Visible = False
            Else
                Panel34.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbNoteChanges_CY2006_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbNoteChanges_CY2006.LinkClicked
        Try
            If Panel45.Visible = True Then
                Panel45.Visible = False
            Else
                Panel45.Visible = True
            End If
            If Panel35.Visible = True Then
                Panel35.Visible = False
            Else
                Panel35.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ResetForm()
        Try
            txtNonRespondersID.Clear()
            lblFacilityName.Text = "-Facility Name-"
            lblFacilityAddress.Text = "-Facility Address-"
            lblFacilityAddress2.Text = ""
            lblContactName.Text = "-Contact Name-"
            lblContactTitle.Text = "-Contact Title-"
            lblContactCompany.Text = "-Contact Company-"
            lblContactAddress.Text = "-Contact Address-"
            lblContactAddress2.Text = ""
            lblContactPhoneNumber.Text = "-Conatct Phone Number-"
            lblContactEmailAddress.Text = "-Contact Email Address-"
            lblOperatingStatus.Text = "-Operating Stauts-"
            lblSourceClass.Text = "-Source Class-"
            lblNSPS.Text = "-NSPS-"
            lblTitleV.Text = "-Title V-"
            txtAIRSNumber_08.Clear()
            txtAIRSNumber_07.Clear()
            txtAIRSNumber_06.Clear()

            txtEditFacilityName.Text = "Facility Name"
            txtEditFacilityAddress.Text = "Facility Address"
            txtEditFacilityCity.Text = "Facility City"
            mtbEditZipCode.Clear()
            cboOperatingStatus.Text = ""
            txtEditSourceClass.Clear()
            rdbTVYes.Checked = False
            rdbTVNo.Checked = False
            rdbNSPSYes.Checked = False
            rdbNSPSNo.Checked = False

            txtEditContactFirstName.Text = "Contact First Name"
            txtEditContactLastName.Text = "Contact Last Name"
            txtEditContactTitle.Text = "Contact Title"
            txtEditContactCompany.Text = "Contact Company"
            txtEditContactAddress.Text = "Contact Address"
            txtEditContactCity.Text = "Contact City"
            txtEditContactState.Clear()
            mtbEditContactZipCode.Clear()
            txtEditContactPhoneNumber.Text = "Contact Phone Number"
            txtEditContactEmailAddress.Text = "Contact Email Address"

            lblFacilityName_CY2008.Text = "-Facility Name-"
            lblFacilityAddress_CY2008.Text = "Facility Address"
            lblFacilityAddress2_CY2008.Text = ""
            lblContactName_CY2008.Text = "-Contact Name-"
            lblContactCompany_CY2008.Text = "-Contact Company-"
            lblContactAddress_CY2008.Text = "Contact Address"
            lblContactAddress2_CY2008.Text = ""
            lblOperatingStatus_CY2008.Text = "-Operating Stauts-"
            lblSourceClass_CY2008.Text = "-Source Class-"
            lblNSPS_CY2008.Text = "-NSPS-"
            lblTitleV_CY2008.Text = "-Title V-"

            txtEditFacilityName_CY2008.Text = "Facility Name"
            txtEditFacilityAddress_CY2008.Text = "Facility Address"
            txtEditFacilityCity_CY2008.Text = "Facility City"
            mtbEditZipCode_CY2008.Clear()
            cboOperatingStatus_CY2008.Text = ""
            txtEditSourceClass_CY2008.Clear()
            rdbTVYes_CY2008.Checked = False
            rdbTVNo_CY2008.Checked = False
            rdbNSPSYes_CY2008.Checked = False
            rdbNSPSNo_CY2008.Checked = False

            lblFacilityName_CY2007.Text = "-Facility Name-"
            lblFacilityAddress_CY2007.Text = "Facility Address"
            lblFacilityAddress2_CY2007.Text = ""
            lblContactName_CY2007.Text = "-Contact Name-"
            lblContactCompany_CY2007.Text = "-Contact Company-"
            lblContactAddress_CY2007.Text = "Contact Address"
            lblContactAddress2_CY2007.Text = ""
            lblOperatingStatus_CY2007.Text = "-Operating Stauts-"
            lblSourceClass_CY2007.Text = "-Source Class-"
            lblNSPS_CY2007.Text = "-NSPS-"
            lblTitleV_CY2007.Text = "-Title V-"

            txtEditFacilityName_CY2007.Text = "Facility Name"
            txtEditFacilityAddress_CY2007.Text = "Facility Address"
            txtEditFacilityCity_CY2007.Text = "Facility City"
            mtbEditZipCode_CY2007.Clear()
            cboOperatingStatus_CY2007.Text = ""
            txtEditSourceClass_CY2007.Clear()
            rdbTVYes_CY2007.Checked = False
            rdbTVNo_CY2007.Checked = False
            rdbNSPSYes_CY2007.Checked = False
            rdbNSPSNo_CY2007.Checked = False

            lblFacilityName_CY2006.Text = "-Facility Name-"
            lblFacilityAddress_CY2006.Text = "Facility Address"
            lblFacilityAddress2_CY2006.Text = ""
            lblContactName_CY2006.Text = "-Contact Name-"
            lblContactCompany_CY2006.Text = "-Contact Company-"
            lblContactAddress_CY2006.Text = "Contact Address"
            lblContactAddress2_CY2006.Text = ""
            lblOperatingStatus_CY2006.Text = "-Operating Stauts-"
            lblSourceClass_CY2006.Text = "-Source Class-"
            lblNSPS_CY2006.Text = "-NSPS-"
            lblTitleV_CY2006.Text = "-Title V-"

            txtEditFacilityName_CY2006.Text = "Facility Name"
            txtEditFacilityAddress_CY2006.Text = "Facility Address"
            txtEditFacilityCity_CY2006.Text = "Facility City"
            mtbEditZipCode_CY2006.Clear()
            cboOperatingStatus_CY2006.Text = ""
            txtEditSourceClass_CY2006.Clear()
            rdbTVYes_CY2006.Checked = False
            rdbTVNo_CY2006.Checked = False
            rdbNSPSYes_CY2006.Checked = False
            rdbNSPSNo_CY2006.Checked = False

            rdbOwnershipChangeYes.Checked = False
            rdbOwnershipChangeNo.Checked = False
            txtOwnershipChangeComments.Clear()
            rdbSourceClassChangeYes.Checked = False
            rdbSourceClassChangeNO.Checked = False
            txtSourceClassificationChangeComment.Clear()

            lblFacilityName_CY2008.Text = "-Facility Name-"
            lblFacilityAddress_CY2008.Text = "Facility Address"
            lblFacilityAddress2_CY2008.Text = ""
            lblContactName_CY2008.Text = "-Contact Name-"
            lblContactCompany_CY2008.Text = "-Contact Company-"
            lblContactAddress_CY2008.Text = "Contact Address"
            lblContactAddress2_CY2008.Text = ""
            lblOperatingStatus_CY2008.Text = "-Operating Stauts-"
            lblSourceClass_CY2008.Text = "-Source Class-"
            lblNSPS_CY2008.Text = "-NSPS-"
            lblTitleV_CY2008.Text = "-Title V-"

            txtEditFacilityName_CY2008.Text = "Facility Name"
            txtEditFacilityAddress_CY2008.Text = "Facility Address"
            txtEditFacilityCity_CY2008.Text = "Facility City"
            mtbEditZipCode_CY2008.Clear()
            cboOperatingStatus_CY2008.Text = ""
            txtEditSourceClass_CY2008.Clear()
            rdbTVYes_CY2008.Checked = False
            rdbTVNo_CY2008.Checked = False
            rdbNSPSYes_CY2008.Checked = False
            rdbNSPSNo_CY2008.Checked = False

            lblFacilityName_CY2007.Text = "-Facility Name-"
            lblFacilityAddress_CY2007.Text = "Facility Address"
            lblFacilityAddress2_CY2007.Text = ""
            lblContactName_CY2007.Text = "-Contact Name-"
            lblContactCompany_CY2007.Text = "-Contact Company-"
            lblContactAddress_CY2007.Text = "Contact Address"
            lblContactAddress2_CY2007.Text = ""
            lblOperatingStatus_CY2007.Text = "-Operating Stauts-"
            lblSourceClass_CY2007.Text = "-Source Class-"
            lblNSPS_CY2007.Text = "-NSPS-"
            lblTitleV_CY2007.Text = "-Title V-"

            txtEditFacilityName_CY2007.Text = "Facility Name"
            txtEditFacilityAddress_CY2007.Text = "Facility Address"
            txtEditFacilityCity_CY2007.Text = "Facility City"
            mtbEditZipCode_CY2007.Clear()
            cboOperatingStatus_CY2007.Text = ""
            txtEditSourceClass_CY2007.Clear()
            rdbTVYes_CY2007.Checked = False
            rdbTVNo_CY2007.Checked = False
            rdbNSPSYes_CY2007.Checked = False
            rdbNSPSNo_CY2007.Checked = False

            lblFacilityName_CY2006.Text = "-Facility Name-"
            lblFacilityAddress_CY2006.Text = "Facility Address"
            lblFacilityAddress2_CY2006.Text = ""
            lblContactName_CY2006.Text = "-Contact Name-"
            lblContactCompany_CY2006.Text = "-Contact Company-"
            lblContactAddress_CY2006.Text = "Contact Address"
            lblContactAddress2_CY2006.Text = ""
            lblOperatingStatus_CY2006.Text = "-Operating Stauts-"
            lblSourceClass_CY2006.Text = "-Source Class-"
            lblNSPS_CY2006.Text = "-NSPS-"
            lblTitleV_CY2006.Text = "-Title V-"

            txtEditFacilityName_CY2006.Text = "Facility Name"
            txtEditFacilityAddress_CY2006.Text = "Facility Address"
            txtEditFacilityCity_CY2006.Text = "Facility City"
            mtbEditZipCode_CY2006.Clear()
            cboOperatingStatus_CY2006.Text = ""
            txtEditSourceClass_CY2006.Clear()
            rdbTVYes_CY2006.Checked = False
            rdbTVNo_CY2006.Checked = False
            rdbNSPSYes_CY2006.Checked = False
            rdbNSPSNo_CY2006.Checked = False

            rdbOwnershipChangeYes.Checked = False
            rdbOwnershipChangeNo.Checked = False
            rdbSourceClassChangeYes.Checked = False
            rdbSourceClassChangeNO.Checked = False

            txtComments.Clear()

            txtNonPayerID.Clear()
            lblNonPayerFacilityName.Text = "Facility Name"
            lblNonPayerFacilityAddress.Text = ""
            lblNonPayerOpStatus.Text = "Operating Status"
            lblNonPayerSourceClass.Text = "Source Class"
            lblNonPayerTVStatus.Text = "Title V"
            lblNonPayerNSPSStatus.Text = "NSPS"
            lblNonPayerContactName.Text = "Contact Name"
            lblNonPayerContactTitle.Text = "Contact Title"
            lblNonPayerContactCompany.Text = "Contact Company"
            lblNonPayerContactAddress.Text = ""
            lblNonPayerContactPhoneNumber.Text = "Contact Phone Number"
            lblNonPayerContactEmail.Text = "Contact Email Address"
            GBNonPayer_CY08.Visible = False
            GBNonPayer_CY07.Visible = False
            GBNonPayer_CY06.Visible = False
            GBNonPayer_CY05.Visible = False
            GBNonPayer_CY04.Visible = False
            GBNonPayer_CY03.Visible = False
            GBNonPayer_CY02.Visible = False
            rdbNonPayerActive.Checked = False
            rdbNonPayerInactive.Checked = False

            lblAmountPaid_CY2008.Text = "Fees Paid: -"
            DTPInitialLetter_2008.Text = OracleDate
            DTPInitialLetter_2008.Checked = False
            DTPLetterReturned_CY2008.Text = OracleDate
            DTPLetterReturned_CY2008.Checked = False
            rdbAddressUnknownYes_CY2008.Checked = False
            rdbAddressUnknownNo_CY2008.Checked = False
            DTPLetterRemailed_CY2008.Text = OracleDate
            DTPLetterRemailed_CY2008.Checked = False
            rdbDataCorrectYes_CY2008.Checked = False
            rdbDataCorrectNo_CY2008.Checked = False
            rdbBankruptcyYes_CY2008.Checked = False
            rdbBankruptcyNo_CY2008.Checked = False
            rdbUnabletoContactYes_CY2008.Checked = False
            rdbUnabletoContactNo_CY2008.Checked = False
            DTPNOVSent_CY2008.Text = OracleDate
            DTPNOVSent_CY2008.Checked = False
            DTPCOSent_CY2008.Text = OracleDate
            DTPCOSent_CY2008.Checked = False
            DTPAOSent_CY2008.Text = OracleDate
            DTPAOSent_CY2008.Checked = False
            DTPFeesPaid_CY2008.Text = OracleDate
            DTPFeesPaid_CY2008.Checked = False
            DTPCloseOut_CY2008.Text = OracleDate
            DTPCloseOut_CY2008.Checked = False
            lblStaffAssigned_08.Text = "Staff Last Modified: - "
            lblLastModified_08.Text = "Last Modified: - "
            lblManagerSignOff_08.Text = "Manager Sign Off: - "
            lblSignOffDat_08.Text = "Last Modified: - "
            txtComments_CY2008.Clear()

            lblAmountPaid_CY2007.Text = "Fees Paid: -"
            DTPInitialLetter_2007.Text = OracleDate
            DTPInitialLetter_2007.Checked = False
            DTPLetterReturned_CY2007.Text = OracleDate
            DTPLetterReturned_CY2007.Checked = False
            rdbAddressUnknownYes_CY2007.Checked = False
            rdbAddressUnknownNo_CY2007.Checked = False
            DTPLetterRemailed_CY2007.Text = OracleDate
            DTPLetterRemailed_CY2007.Checked = False
            rdbDataCorrectYes_CY2007.Checked = False
            rdbDataCorrectNo_CY2007.Checked = False
            rdbBankruptcyYes_CY2007.Checked = False
            rdbBankruptcyNo_CY2007.Checked = False
            rdbUnabletoContactYes_CY2007.Checked = False
            rdbUnabletoContactNo_CY2007.Checked = False
            DTPNOVSent_CY2007.Text = OracleDate
            DTPNOVSent_CY2007.Checked = False
            DTPCOSent_CY2007.Text = OracleDate
            DTPCOSent_CY2007.Checked = False
            DTPAOSent_CY2007.Text = OracleDate
            DTPAOSent_CY2007.Checked = False
            DTPFeesPaid_CY2007.Text = OracleDate
            DTPFeesPaid_CY2007.Checked = False
            DTPCloseOut_CY2007.Text = OracleDate
            DTPCloseOut_CY2007.Checked = False
            lblStaffAssigned_07.Text = "Staff Last Modified: - "
            lblLastModified_07.Text = "Last Modified: - "
            lblManagerSignOff_07.Text = "Manager Sign Off: - "
            lblSignOffDat_07.Text = "Last Modified: - "
            txtComments_CY2007.Clear()

            lblAmountPaid_CY2006.Text = "Fees Paid: -"
            DTPInitialLetter_2006.Text = OracleDate
            DTPInitialLetter_2006.Checked = False
            DTPLetterReturned_CY2006.Text = OracleDate
            DTPLetterReturned_CY2006.Checked = False
            rdbAddressUnknownYes_CY2006.Checked = False
            rdbAddressUnknownNo_CY2006.Checked = False
            DTPLetterRemailed_CY2006.Text = OracleDate
            DTPLetterRemailed_CY2006.Checked = False
            rdbDataCorrectYes_CY2006.Checked = False
            rdbDataCorrectNo_CY2006.Checked = False
            rdbBankruptcyYes_CY2006.Checked = False
            rdbBankruptcyNo_CY2006.Checked = False
            rdbUnabletoContactYes_CY2006.Checked = False
            rdbUnabletoContactNo_CY2006.Checked = False
            DTPNOVSent_CY2006.Text = OracleDate
            DTPNOVSent_CY2006.Checked = False
            DTPCOSent_CY2006.Text = OracleDate
            DTPCOSent_CY2006.Checked = False
            DTPAOSent_CY2006.Text = OracleDate
            DTPAOSent_CY2006.Checked = False
            DTPFeesPaid_CY2006.Text = OracleDate
            DTPFeesPaid_CY2006.Checked = False
            DTPCloseOut_CY2006.Text = OracleDate
            DTPCloseOut_CY2006.Checked = False
            lblStaffAssigned_06.Text = "Staff Last Modified: - "
            lblLastModified_06.Text = "Last Modified: - "
            lblManagerSignOff_06.Text = "Manager Sign Off: - "
            lblSignOffDat_06.Text = "Last Modified: - "
            txtComments_CY2006.Clear()

            lblAmountPaid_CY2005.Text = "Fees Paid: -"
            DTPInitialLetter_2005.Text = OracleDate
            DTPInitialLetter_2005.Checked = False
            DTPLetterReturned_CY2005.Text = OracleDate
            DTPLetterReturned_CY2005.Checked = False
            rdbAddressUnknownYes_CY2005.Checked = False
            rdbAddressUnknownNo_CY2005.Checked = False
            DTPLetterRemailed_CY2005.Text = OracleDate
            DTPLetterRemailed_CY2005.Checked = False
            rdbDataCorrectYes_CY2005.Checked = False
            rdbDataCorrectNo_CY2005.Checked = False
            rdbBankruptcyYes_CY2005.Checked = False
            rdbBankruptcyNo_CY2005.Checked = False
            rdbUnabletoContactYes_CY2005.Checked = False
            rdbUnabletoContactNo_CY2005.Checked = False
            DTPNOVSent_CY2005.Text = OracleDate
            DTPNOVSent_CY2005.Checked = False
            DTPCOSent_CY2005.Text = OracleDate
            DTPCOSent_CY2005.Checked = False
            DTPAOSent_CY2005.Text = OracleDate
            DTPAOSent_CY2005.Checked = False
            DTPFeesPaid_CY2005.Text = OracleDate
            DTPFeesPaid_CY2005.Checked = False
            DTPCloseOut_CY2005.Text = OracleDate
            DTPCloseOut_CY2005.Checked = False
            lblStaffAssigned_05.Text = "Staff Last Modified: - "
            lblLastModified_05.Text = "Last Modified: - "
            lblManagerSignOff_05.Text = "Manager Sign Off: - "
            lblSignOffDat_05.Text = "Last Modified: - "
            txtComments_CY2005.Clear()

            lblAmountPaid_CY2004.Text = "Fees Paid: -"
            DTPInitialLetter_2004.Text = OracleDate
            DTPInitialLetter_2004.Checked = False
            DTPLetterReturned_CY2004.Text = OracleDate
            DTPLetterReturned_CY2004.Checked = False
            rdbAddressUnknownYes_CY2004.Checked = False
            rdbAddressUnknownNo_CY2004.Checked = False
            DTPLetterRemailed_CY2004.Text = OracleDate
            DTPLetterRemailed_CY2004.Checked = False
            rdbDataCorrectYes_CY2004.Checked = False
            rdbDataCorrectNo_CY2004.Checked = False
            rdbBankruptcyYes_CY2004.Checked = False
            rdbBankruptcyNo_CY2004.Checked = False
            rdbUnabletoContactYes_CY2004.Checked = False
            rdbUnabletoContactNo_CY2004.Checked = False
            DTPNOVSent_CY2004.Text = OracleDate
            DTPNOVSent_CY2004.Checked = False
            DTPCOSent_CY2004.Text = OracleDate
            DTPCOSent_CY2004.Checked = False
            DTPAOSent_CY2004.Text = OracleDate
            DTPAOSent_CY2004.Checked = False
            DTPFeesPaid_CY2004.Text = OracleDate
            DTPFeesPaid_CY2004.Checked = False
            DTPCloseOut_CY2004.Text = OracleDate
            DTPCloseOut_CY2004.Checked = False
            lblStaffAssigned_04.Text = "Staff Last Modified: - "
            lblLastModified_04.Text = "Last Modified: - "
            lblManagerSignOff_04.Text = "Manager Sign Off: - "
            lblSignOffDat_04.Text = "Last Modified: - "
            txtComments_CY2004.Clear()

            lblAmountPaid_CY2003.Text = "Fees Paid: -"
            DTPInitialLetter_2003.Text = OracleDate
            DTPInitialLetter_2003.Checked = False
            DTPLetterReturned_CY2003.Text = OracleDate
            DTPLetterReturned_CY2003.Checked = False
            rdbAddressUnknownYes_CY2003.Checked = False
            rdbAddressUnknownNo_CY2003.Checked = False
            DTPLetterRemailed_CY2003.Text = OracleDate
            DTPLetterRemailed_CY2003.Checked = False
            rdbDataCorrectYes_CY2003.Checked = False
            rdbDataCorrectNo_CY2003.Checked = False
            rdbBankruptcyYes_CY2003.Checked = False
            rdbBankruptcyNo_CY2003.Checked = False
            rdbUnabletoContactYes_CY2003.Checked = False
            rdbUnabletoContactNo_CY2003.Checked = False
            DTPNOVSent_CY2003.Text = OracleDate
            DTPNOVSent_CY2003.Checked = False
            DTPCOSent_CY2003.Text = OracleDate
            DTPCOSent_CY2003.Checked = False
            DTPAOSent_CY2003.Text = OracleDate
            DTPAOSent_CY2003.Checked = False
            DTPFeesPaid_CY2003.Text = OracleDate
            DTPFeesPaid_CY2003.Checked = False
            DTPCloseOut_CY2003.Text = OracleDate
            DTPCloseOut_CY2003.Checked = False
            lblStaffAssigned_03.Text = "Staff Last Modified: - "
            lblLastModified_03.Text = "Last Modified: - "
            lblManagerSignOff_03.Text = "Manager Sign Off: - "
            lblSignOffDat_03.Text = "Last Modified: - "
            txtComments_CY2003.Clear()

            lblAmountPaid_CY2002.Text = "Fees Paid: -"
            DTPInitialLetter_2002.Text = OracleDate
            DTPInitialLetter_2002.Checked = False
            DTPLetterReturned_CY2002.Text = OracleDate
            DTPLetterReturned_CY2002.Checked = False
            rdbAddressUnknownYes_CY2002.Checked = False
            rdbAddressUnknownNo_CY2002.Checked = False
            DTPLetterRemailed_CY2002.Text = OracleDate
            DTPLetterRemailed_CY2002.Checked = False
            rdbDataCorrectYes_CY2002.Checked = False
            rdbDataCorrectNo_CY2002.Checked = False
            rdbBankruptcyYes_CY2002.Checked = False
            rdbBankruptcyNo_CY2002.Checked = False
            rdbUnabletoContactYes_CY2002.Checked = False
            rdbUnabletoContactNo_CY2002.Checked = False
            DTPNOVSent_CY2002.Text = OracleDate
            DTPNOVSent_CY2002.Checked = False
            DTPCOSent_CY2002.Text = OracleDate
            DTPCOSent_CY2002.Checked = False
            DTPAOSent_CY2002.Text = OracleDate
            DTPAOSent_CY2002.Checked = False
            DTPFeesPaid_CY2002.Text = OracleDate
            DTPFeesPaid_CY2002.Checked = False
            DTPCloseOut_CY2002.Text = OracleDate
            DTPCloseOut_CY2002.Checked = False
            lblStaffAssigned_02.Text = "Staff Last Modified: - "
            lblLastModified_02.Text = "Last Modified: - "
            lblManagerSignOff_02.Text = "Manager Sign Off: - "
            lblSignOffDat_02.Text = "Last Modified: - "
            txtComments_CY2002.Clear()
            txtAuditComments.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadCY2008()
        Try
            If txtNonRespondersID.Text <> "" Then
                lblFacilityName_CY2008.Text = "-Facility Name-"
                lblFacilityAddress_CY2008.Text = "Facility Address"
                lblFacilityAddress2_CY2008.Text = ""
                lblContactName_CY2008.Text = "-Contact Name-"
                lblContactCompany_CY2008.Text = "-Contact Company-"
                lblContactAddress_CY2008.Text = "Contact Address"
                lblContactAddress2_CY2008.Text = ""
                lblOperatingStatus_CY2008.Text = "-Operating Stauts-"
                lblSourceClass_CY2008.Text = "-Source Class-"
                lblNSPS_CY2008.Text = "-NSPS-"
                lblTitleV_CY2008.Text = "-Title V-"

                txtEditFacilityName_CY2008.Text = "Facility Name"
                txtEditFacilityAddress_CY2008.Text = "Facility Address"
                txtEditFacilityCity_CY2008.Text = "Facility City"
                mtbEditZipCode_CY2008.Clear()
                cboOperatingStatus_CY2008.Text = ""
                txtEditSourceClass_CY2008.Clear()
                rdbTVYes_CY2008.Checked = False
                rdbTVNo_CY2008.Checked = False
                rdbNSPSYes_CY2008.Checked = False
                rdbNSPSNo_CY2008.Checked = False

                SQL = "select " & _
                "strAIRSNumber_08, strFacilityName_08,  " & _
                "strFacilityStreet_08, strFacilityCity_08,  " & _
                "strFacilityState_08, strFacilityZipCode_08,  " & _
                "strContactFirstName_08, strContactlastName_08,  " & _
                "strContactCompany_08,  " & _
                "strContactAddress_08, strContactCity_08,  " & _
                "strContactState_08, strContactZipCode_08,  " & _
                "strOperatingStatus_08, strClassification_08, " & _
                "strNSPSStatus_08, strTVStatus_08, " & _
                "strFacilityName_08_edit, strFacilityStreet_08_Edit, " & _
                "strFacilityCity_08_Edit, strFacilitystate_08_edit, " & _
                "strFacilityZipCode_08_edit, strClassification_08_edit, " & _
                "strOperatingStatus_08_Edit, strNSPSStatus_08_Edit, " & _
                "strTVStatus_08_Edit, " & _
                "strContactFirstname_08_Edit, strContactLastName_08_Edit, " & _
                "strContactCompany_08_Edit, strContactAddress_08_Edit, " & _
                "strContactCity_08_Edit, strContactState_08_Edit, " & _
                "strContactZipCode_08_Edit, strTotalPaid_08, " & _
                "str2008Comments " & _
                "from " & connNameSpace & ".Fee_NonResponders_2010  " & _
                "where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityName_08")) Then
                            lblFacilityName_CY2008.Text = "-Facility Name-"
                        Else
                            lblFacilityName_CY2008.Text = dr.Item("strFacilityName_08")
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet_08")) Then
                            lblFacilityAddress2_CY2008.Text = ""
                        Else
                            lblFacilityAddress2_CY2008.Text = dr.Item("strFacilityStreet_08") & vbCrLf
                        End If
                        If IsDBNull(dr.Item("strFacilityCity_08")) Then
                            lblFacilityAddress2_CY2008.Text = lblFacilityAddress2_CY2008.Text
                        Else
                            lblFacilityAddress2_CY2008.Text = lblFacilityAddress2_CY2008.Text & dr.Item("strFacilityCity_08")
                        End If
                        If IsDBNull(dr.Item("strFacilityState_08")) Then
                            lblFacilityAddress2_CY2008.Text = lblFacilityAddress2_CY2008.Text
                        Else
                            lblFacilityAddress2_CY2008.Text = lblFacilityAddress2_CY2008.Text & ", " & dr.Item("strFacilityState_08")
                        End If
                        If IsDBNull(dr.Item("strFacilityZipCode_08")) Then
                            lblFacilityAddress2_CY2008.Text = lblFacilityAddress2_CY2008.Text
                        Else
                            If dr.Item("strFacilityZipCode_08").ToString.Length > 5 Then
                                lblFacilityAddress2_CY2008.Text = lblFacilityAddress2_CY2008.Text & " " & Mid(dr.Item("strFacilityZipCode_08"), 1, 5) & _
                                                    "-" & Mid(dr.Item("strFacilityZipCode_08"), 6)
                            Else
                                lblFacilityAddress2_CY2008.Text = lblFacilityAddress2_CY2008.Text & " " & dr.Item("strFacilityZipCode_08")
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactFirstName_08")) Then
                            lblContactName_CY2008.Text = "-Contact Name-"
                        Else
                            lblContactName_CY2008.Text = dr.Item("strContactFirstName_08")
                        End If
                        If IsDBNull(dr.Item("strContactlastName_08")) Then
                            lblContactName_CY2008.Text = lblContactName_CY2008.Text
                        Else
                            lblContactName_CY2008.Text = lblContactName_CY2008.Text & " " & dr.Item("strContactlastName_08")
                        End If
                        If IsDBNull(dr.Item("strContactCompany_08")) Then
                            lblContactCompany_CY2008.Text = "-Contact Company-"
                        Else
                            lblContactCompany_CY2008.Text = dr.Item("strContactCompany_08")
                        End If
                        If IsDBNull(dr.Item("strContactAddress_08")) Then
                            lblContactAddress2_CY2008.Text = ""
                        Else
                            lblContactAddress2_CY2008.Text = dr.Item("strContactAddress_08") & vbCrLf
                        End If
                        If IsDBNull(dr.Item("strContactCity_08")) Then
                            lblContactAddress2_CY2008.Text = lblContactAddress2_CY2008.Text
                        Else
                            lblContactAddress2_CY2008.Text = lblContactAddress2_CY2008.Text & dr.Item("strContactCity_08")
                        End If
                        If IsDBNull(dr.Item("strContactState_08")) Then
                            lblContactAddress2_CY2008.Text = lblContactAddress2_CY2008.Text
                        Else
                            lblContactAddress2_CY2008.Text = lblContactAddress2_CY2008.Text & ", " & dr.Item("strContactState_08")
                        End If
                        If IsDBNull(dr.Item("strContactZipCode_08")) Then
                            lblContactAddress2_CY2008.Text = lblContactAddress2_CY2008.Text
                        Else
                            If dr.Item("strContactZipCode_08").ToString.Length > 5 Then
                                lblContactAddress2_CY2008.Text = lblContactAddress2_CY2008.Text & " " & Mid(dr.Item("strContactZipCode_08"), 1, 5) & _
                                                    "-" & Mid(dr.Item("strContactZipCode_08"), 6)
                            Else
                                lblContactAddress2_CY2008.Text = lblContactAddress2_CY2008.Text & " " & dr.Item("strContactZipCode_08")
                            End If
                        End If
                        If IsDBNull(dr.Item("strOperatingStatus_08")) Then
                            lblOperatingStatus_CY2008.Text = "-Operating Stauts-"
                        Else
                            lblOperatingStatus_CY2008.Text = "Op. Status: " & dr.Item("strOperatingStatus_08")
                        End If
                        If IsDBNull(dr.Item("strClassification_08")) Then
                            lblSourceClass_CY2008.Text = "-Source Class-"
                        Else
                            lblSourceClass_CY2008.Text = "Source Class: " & dr.Item("strClassification_08")
                        End If
                        If IsDBNull(dr.Item("strNSPSStatus_08")) Then
                            lblNSPS_CY2008.Text = "-NSPS-"
                        Else
                            lblNSPS_CY2008.Text = "NSPS: " & dr.Item("strNSPSStatus_08")
                        End If
                        If IsDBNull(dr.Item("strTVStatus_08")) Then
                            lblTitleV_CY2008.Text = "-Title V-"
                        Else
                            lblTitleV_CY2008.Text = "Title V: " & dr.Item("strTVStatus_08")
                        End If






                        If IsDBNull(dr.Item("strFacilityName_08_edit")) Then
                            txtEditFacilityName_CY2008.Text = "Facility Name"
                        Else
                            txtEditFacilityName_CY2008.Text = dr.Item("strFacilityName_08_edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet_08_Edit")) Then
                            txtEditFacilityAddress_CY2008.Text = "Facility Address"
                        Else
                            txtEditFacilityAddress_CY2008.Text = dr.Item("strFacilityStreet_08_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity_08_Edit")) Then
                            txtEditFacilityCity_CY2008.Text = "Facility City"
                        Else
                            txtEditFacilityCity_CY2008.Text = dr.Item("strFacilityCity_08_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityZipCode_08_edit")) Then
                            mtbEditZipCode_CY2008.Clear()
                        Else
                            mtbEditZipCode_CY2008.Text = dr.Item("strFacilityZipCode_08_edit")
                        End If
                        If IsDBNull(dr.Item("strClassification_08_edit")) Then
                            txtEditSourceClass_CY2008.Clear()
                        Else
                            txtEditSourceClass_CY2008.Text = dr.Item("strClassification_08_edit")
                        End If
                        If IsDBNull(dr.Item("strOperatingStatus_08_Edit")) Then
                            cboOperatingStatus_CY2008.Text = ""
                        Else
                            cboOperatingStatus_CY2008.Text = dr.Item("strOperatingStatus_08_Edit")
                        End If
                        If IsDBNull(dr.Item("strNSPSStatus_08_Edit")) Then
                            rdbNSPSYes_CY2008.Checked = False
                            rdbNSPSNo_CY2008.Checked = False
                        Else
                            If dr.Item("strNSPSStatus_08_Edit") = "Yes" Then
                                rdbNSPSYes_CY2008.Checked = True
                            Else
                                rdbNSPSNo_CY2008.Checked = True
                            End If
                        End If
                        If IsDBNull(dr.Item("strTVStatus_08_Edit")) Then
                            rdbTVYes_CY2008.Checked = False
                            rdbTVNo_CY2008.Checked = False
                        Else
                            If dr.Item("strTVStatus_08_Edit") = "Yes" Then
                                rdbTVYes_CY2008.Checked = True
                            Else
                                rdbTVNo_CY2008.Checked = True
                            End If
                        End If

                    End While
                    dr.Close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadCY2007()
        Try
            If txtNonRespondersID.Text <> "" Then
                lblFacilityName_CY2007.Text = "-Facility Name-"
                lblFacilityAddress_CY2007.Text = "Facility Address"
                lblFacilityAddress2_CY2007.Text = ""
                lblContactName_CY2007.Text = "-Contact Name-"
                lblContactCompany_CY2007.Text = "-Contact Company-"
                lblContactAddress_CY2007.Text = "Contact Address"
                lblContactAddress2_CY2007.Text = ""
                lblOperatingStatus_CY2007.Text = "-Operating Stauts-"
                lblSourceClass_CY2007.Text = "-Source Class-"
                lblNSPS_CY2007.Text = "-NSPS-"
                lblTitleV_CY2007.Text = "-Title V-"

                txtEditFacilityName_CY2007.Text = "Facility Name"
                txtEditFacilityAddress_CY2007.Text = "Facility Address"
                txtEditFacilityCity_CY2007.Text = "Facility City"
                mtbEditZipCode_CY2007.Clear()
                cboOperatingStatus_CY2007.Text = ""
                txtEditSourceClass_CY2007.Clear()
                rdbTVYes_CY2007.Checked = False
                rdbTVNo_CY2007.Checked = False
                rdbNSPSYes_CY2007.Checked = False
                rdbNSPSNo_CY2007.Checked = False

                SQL = "select " & _
                "strAIRSNumber_07, strFacilityName_07,  " & _
                "strFacilityStreet_07, strFacilityCity_07,  " & _
                "strFacilityState_07, strFacilityZipCode_07,  " & _
                "strContactFirstName_07, strContactlastName_07,  " & _
                "strContactCompany_07,  " & _
                "strContactAddress_07, strContactCity_07,  " & _
                "strContactState_07, strContactZipCode_07,  " & _
                "strOperatingStatus_07, strClassification_07, " & _
                "strNSPSStatus_07, strTVStatus_07, " & _
                "strFacilityName_07_edit, strFacilityStreet_07_Edit, " & _
                "strFacilityCity_07_Edit, strFacilitystate_07_edit, " & _
                "strFacilityZipCode_07_edit, strClassification_07_edit, " & _
                "strOperatingStatus_07_Edit, strNSPSStatus_07_Edit, " & _
                "strTVStatus_07_Edit, " & _
                "strContactFirstname_07_Edit, strContactLastName_07_Edit, " & _
                "strContactCompany_07_Edit, strContactAddress_07_Edit, " & _
                "strContactCity_07_Edit, strContactState_07_Edit, " & _
                "strContactZipCode_07_Edit, strTotalPaid_07, " & _
                "str2007Comments " & _
                "from " & connNameSpace & ".Fee_NonResponders_2010  " & _
                "where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityName_07")) Then
                            lblFacilityName_CY2007.Text = "-Facility Name-"
                        Else
                            lblFacilityName_CY2007.Text = dr.Item("strFacilityName_07")
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet_07")) Then
                            lblFacilityAddress2_CY2007.Text = ""
                        Else
                            lblFacilityAddress2_CY2007.Text = dr.Item("strFacilityStreet_07") & vbCrLf
                        End If
                        If IsDBNull(dr.Item("strFacilityCity_07")) Then
                            lblFacilityAddress2_CY2007.Text = lblFacilityAddress2_CY2007.Text
                        Else
                            lblFacilityAddress2_CY2007.Text = lblFacilityAddress2_CY2007.Text & dr.Item("strFacilityCity_07")
                        End If
                        If IsDBNull(dr.Item("strFacilityState_07")) Then
                            lblFacilityAddress2_CY2007.Text = lblFacilityAddress2_CY2007.Text
                        Else
                            lblFacilityAddress2_CY2007.Text = lblFacilityAddress2_CY2007.Text & ", " & dr.Item("strFacilityState_07")
                        End If
                        If IsDBNull(dr.Item("strFacilityZipCode_07")) Then
                            lblFacilityAddress2_CY2007.Text = lblFacilityAddress2_CY2007.Text
                        Else
                            If dr.Item("strFacilityZipCode_07").ToString.Length > 5 Then
                                lblFacilityAddress2_CY2007.Text = lblFacilityAddress2_CY2007.Text & " " & Mid(dr.Item("strFacilityZipCode_07"), 1, 5) & _
                                                    "-" & Mid(dr.Item("strFacilityZipCode_07"), 6)
                            Else
                                lblFacilityAddress2_CY2007.Text = lblFacilityAddress2_CY2007.Text & " " & dr.Item("strFacilityZipCode_07")
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactFirstName_07")) Then
                            lblContactName_CY2007.Text = "-Contact Name-"
                        Else
                            lblContactName_CY2007.Text = dr.Item("strContactFirstName_07")
                        End If
                        If IsDBNull(dr.Item("strContactlastName_07")) Then
                            lblContactName_CY2007.Text = lblContactName_CY2007.Text
                        Else
                            lblContactName_CY2007.Text = lblContactName_CY2007.Text & " " & dr.Item("strContactlastName_07")
                        End If
                        If IsDBNull(dr.Item("strContactCompany_07")) Then
                            lblContactCompany_CY2007.Text = "-Contact Company-"
                        Else
                            lblContactCompany_CY2007.Text = dr.Item("strContactCompany_07")
                        End If
                        If IsDBNull(dr.Item("strContactAddress_07")) Then
                            lblContactAddress2_CY2007.Text = ""
                        Else
                            lblContactAddress2_CY2007.Text = dr.Item("strContactAddress_07") & vbCrLf
                        End If
                        If IsDBNull(dr.Item("strContactCity_07")) Then
                            lblContactAddress2_CY2007.Text = lblContactAddress2_CY2007.Text
                        Else
                            lblContactAddress2_CY2007.Text = lblContactAddress2_CY2007.Text & dr.Item("strContactCity_07")
                        End If
                        If IsDBNull(dr.Item("strContactState_07")) Then
                            lblContactAddress2_CY2007.Text = lblContactAddress2_CY2007.Text
                        Else
                            lblContactAddress2_CY2007.Text = lblContactAddress2_CY2007.Text & ", " & dr.Item("strContactState_07")
                        End If
                        If IsDBNull(dr.Item("strContactZipCode_07")) Then
                            lblContactAddress2_CY2007.Text = lblContactAddress2_CY2007.Text
                        Else
                            If dr.Item("strContactZipCode_07").ToString.Length > 5 Then
                                lblContactAddress2_CY2007.Text = lblContactAddress2_CY2007.Text & " " & Mid(dr.Item("strContactZipCode_07"), 1, 5) & _
                                                    "-" & Mid(dr.Item("strContactZipCode_07"), 6)
                            Else
                                lblContactAddress2_CY2007.Text = lblContactAddress2_CY2007.Text & " " & dr.Item("strContactZipCode_07")
                            End If
                        End If
                        If IsDBNull(dr.Item("strOperatingStatus_07")) Then
                            lblOperatingStatus_CY2007.Text = "-Operating Stauts-"
                        Else
                            lblOperatingStatus_CY2007.Text = "Op. Status: " & dr.Item("strOperatingStatus_07")
                        End If
                        If IsDBNull(dr.Item("strClassification_07")) Then
                            lblSourceClass_CY2007.Text = "-Source Class-"
                        Else
                            lblSourceClass_CY2007.Text = "Source Class: " & dr.Item("strClassification_07")
                        End If
                        If IsDBNull(dr.Item("strNSPSStatus_07")) Then
                            lblNSPS_CY2007.Text = "-NSPS-"
                        Else
                            lblNSPS_CY2007.Text = "NSPS: " & dr.Item("strNSPSStatus_07")
                        End If
                        If IsDBNull(dr.Item("strTVStatus_07")) Then
                            lblTitleV_CY2007.Text = "-Title V-"
                        Else
                            lblTitleV_CY2007.Text = "Title V: " & dr.Item("strTVStatus_07")
                        End If

                        If IsDBNull(dr.Item("strFacilityName_07_edit")) Then
                            txtEditFacilityName_CY2007.Text = "Facility Name"
                        Else
                            txtEditFacilityName_CY2007.Text = dr.Item("strFacilityName_07_edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet_07_Edit")) Then
                            txtEditFacilityAddress_CY2007.Text = "Facility Address"
                        Else
                            txtEditFacilityAddress_CY2007.Text = dr.Item("strFacilityStreet_07_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity_07_Edit")) Then
                            txtEditFacilityCity_CY2007.Text = "Facility City"
                        Else
                            txtEditFacilityCity_CY2007.Text = dr.Item("strFacilityCity_07_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityZipCode_07_edit")) Then
                            mtbEditZipCode_CY2007.Clear()
                        Else
                            mtbEditZipCode_CY2007.Text = dr.Item("strFacilityZipCode_07_edit")
                        End If
                        If IsDBNull(dr.Item("strClassification_07_edit")) Then
                            txtEditSourceClass_CY2007.Clear()
                        Else
                            txtEditSourceClass_CY2007.Text = dr.Item("strClassification_07_edit")
                        End If
                        If IsDBNull(dr.Item("strOperatingStatus_07_Edit")) Then
                            cboOperatingStatus_CY2007.Text = ""
                        Else
                            cboOperatingStatus_CY2007.Text = dr.Item("strOperatingStatus_07_Edit")
                        End If
                        If IsDBNull(dr.Item("strNSPSStatus_07_Edit")) Then
                            rdbNSPSYes_CY2007.Checked = False
                            rdbNSPSNo_CY2007.Checked = False
                        Else
                            If dr.Item("strNSPSStatus_07_Edit") = "Yes" Then
                                rdbNSPSYes_CY2007.Checked = True
                            Else
                                rdbNSPSNo_CY2007.Checked = True
                            End If
                        End If
                        If IsDBNull(dr.Item("strTVStatus_07_Edit")) Then
                            rdbTVYes_CY2007.Checked = False
                            rdbTVNo_CY2007.Checked = False
                        Else
                            If dr.Item("strTVStatus_07_Edit") = "Yes" Then
                                rdbTVYes_CY2007.Checked = True
                            Else
                                rdbTVNo_CY2007.Checked = True
                            End If
                        End If

                    End While
                    dr.Close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadCY2006()
        Try
            If txtNonRespondersID.Text <> "" Then
                lblFacilityName_CY2006.Text = "-Facility Name-"
                lblFacilityAddress_CY2006.Text = "Facility Address"
                lblFacilityAddress2_CY2006.Text = ""
                lblContactName_CY2006.Text = "-Contact Name-"
                lblContactCompany_CY2006.Text = "-Contact Company-"
                lblContactAddress_CY2006.Text = "Contact Address"
                lblContactAddress2_CY2006.Text = ""
                lblOperatingStatus_CY2006.Text = "-Operating Stauts-"
                lblSourceClass_CY2006.Text = "-Source Class-"
                lblNSPS_CY2006.Text = "-NSPS-"
                lblTitleV_CY2006.Text = "-Title V-"

                txtEditFacilityName_CY2006.Text = "Facility Name"
                txtEditFacilityAddress_CY2006.Text = "Facility Address"
                txtEditFacilityCity_CY2006.Text = "Facility City"
                mtbEditZipCode_CY2006.Clear()
                cboOperatingStatus_CY2006.Text = ""
                txtEditSourceClass_CY2006.Clear()
                rdbTVYes_CY2006.Checked = False
                rdbTVNo_CY2006.Checked = False
                rdbNSPSYes_CY2006.Checked = False
                rdbNSPSNo_CY2006.Checked = False

                SQL = "select " & _
                "strAIRSNumber_06, strFacilityName_06,  " & _
                "strFacilityStreet_06, strFacilityCity_06,  " & _
                "strFacilityState_06, strFacilityZipCode_06,  " & _
                "strContactFirstName_06, strContactlastName_06,  " & _
                "strContactCompany_06,  " & _
                "strContactAddress_06, strContactCity_06,  " & _
                "strContactState_06, strContactZipCode_06,  " & _
                "strOperatingStatus_06, strClassification_06, " & _
                "strNSPSStatus_06, strTVStatus_06, " & _
                "strFacilityName_06_edit, strFacilityStreet_06_Edit, " & _
                "strFacilityCity_06_Edit, strFacilitystate_06_edit, " & _
                "strFacilityZipCode_06_edit, strClassification_06_edit, " & _
                "strOperatingStatus_06_Edit, strNSPSStatus_06_Edit, " & _
                "strTVStatus_06_Edit, " & _
                "strContactFirstname_06_Edit, strContactLastName_06_Edit, " & _
                "strContactCompany_06_Edit, strContactAddress_06_Edit, " & _
                "strContactCity_06_Edit, strContactState_06_Edit, " & _
                "strContactZipCode_06_Edit, strTotalPaid_06, " & _
                "str2006Comments " & _
                "from " & connNameSpace & ".Fee_NonResponders_2010  " & _
                "where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityName_06")) Then
                            lblFacilityName_CY2006.Text = "-Facility Name-"
                        Else
                            lblFacilityName_CY2006.Text = dr.Item("strFacilityName_06")
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet_06")) Then
                            lblFacilityAddress2_CY2006.Text = ""
                        Else
                            lblFacilityAddress2_CY2006.Text = dr.Item("strFacilityStreet_06") & vbCrLf
                        End If
                        If IsDBNull(dr.Item("strFacilityCity_06")) Then
                            lblFacilityAddress2_CY2006.Text = lblFacilityAddress2_CY2006.Text
                        Else
                            lblFacilityAddress2_CY2006.Text = lblFacilityAddress2_CY2006.Text & dr.Item("strFacilityCity_06")
                        End If
                        If IsDBNull(dr.Item("strFacilityState_06")) Then
                            lblFacilityAddress2_CY2006.Text = lblFacilityAddress2_CY2006.Text
                        Else
                            lblFacilityAddress2_CY2006.Text = lblFacilityAddress2_CY2006.Text & ", " & dr.Item("strFacilityState_06")
                        End If
                        If IsDBNull(dr.Item("strFacilityZipCode_06")) Then
                            lblFacilityAddress2_CY2006.Text = lblFacilityAddress2_CY2006.Text
                        Else
                            If dr.Item("strFacilityZipCode_06").ToString.Length > 5 Then
                                lblFacilityAddress2_CY2006.Text = lblFacilityAddress2_CY2006.Text & " " & Mid(dr.Item("strFacilityZipCode_06"), 1, 5) & _
                                                    "-" & Mid(dr.Item("strFacilityZipCode_06"), 6)
                            Else
                                lblFacilityAddress2_CY2006.Text = lblFacilityAddress2_CY2006.Text & " " & dr.Item("strFacilityZipCode_06")
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactFirstName_06")) Then
                            lblContactName_CY2006.Text = "-Contact Name-"
                        Else
                            lblContactName_CY2006.Text = dr.Item("strContactFirstName_06")
                        End If
                        If IsDBNull(dr.Item("strContactlastName_06")) Then
                            lblContactName_CY2006.Text = lblContactName_CY2006.Text
                        Else
                            lblContactName_CY2006.Text = lblContactName_CY2006.Text & " " & dr.Item("strContactlastName_06")
                        End If
                        If IsDBNull(dr.Item("strContactCompany_06")) Then
                            lblContactCompany_CY2006.Text = "-Contact Company-"
                        Else
                            lblContactCompany_CY2006.Text = dr.Item("strContactCompany_06")
                        End If
                        If IsDBNull(dr.Item("strContactAddress_06")) Then
                            lblContactAddress2_CY2006.Text = ""
                        Else
                            lblContactAddress2_CY2006.Text = dr.Item("strContactAddress_06") & vbCrLf
                        End If
                        If IsDBNull(dr.Item("strContactCity_06")) Then
                            lblContactAddress2_CY2006.Text = lblContactAddress2_CY2006.Text
                        Else
                            lblContactAddress2_CY2006.Text = lblContactAddress2_CY2006.Text & dr.Item("strContactCity_06")
                        End If
                        If IsDBNull(dr.Item("strContactState_06")) Then
                            lblContactAddress2_CY2006.Text = lblContactAddress2_CY2006.Text
                        Else
                            lblContactAddress2_CY2006.Text = lblContactAddress2_CY2006.Text & ", " & dr.Item("strContactState_06")
                        End If
                        If IsDBNull(dr.Item("strContactZipCode_06")) Then
                            lblContactAddress2_CY2006.Text = lblContactAddress2_CY2006.Text
                        Else
                            If dr.Item("strContactZipCode_06").ToString.Length > 5 Then
                                lblContactAddress2_CY2006.Text = lblContactAddress2_CY2006.Text & " " & Mid(dr.Item("strContactZipCode_06"), 1, 5) & _
                                                    "-" & Mid(dr.Item("strContactZipCode_06"), 6)
                            Else
                                lblContactAddress2_CY2006.Text = lblContactAddress2_CY2006.Text & " " & dr.Item("strContactZipCode_06")
                            End If
                        End If
                        If IsDBNull(dr.Item("strOperatingStatus_06")) Then
                            lblOperatingStatus_CY2006.Text = "-Operating Stauts-"
                        Else
                            lblOperatingStatus_CY2006.Text = "Op. Status: " & dr.Item("strOperatingStatus_06")
                        End If
                        If IsDBNull(dr.Item("strClassification_06")) Then
                            lblSourceClass_CY2006.Text = "-Source Class-"
                        Else
                            lblSourceClass_CY2006.Text = "Source Class: " & dr.Item("strClassification_06")
                        End If
                        If IsDBNull(dr.Item("strNSPSStatus_06")) Then
                            lblNSPS_CY2006.Text = "-NSPS-"
                        Else
                            lblNSPS_CY2006.Text = "NSPS: " & dr.Item("strNSPSStatus_06")
                        End If
                        If IsDBNull(dr.Item("strTVStatus_06")) Then
                            lblTitleV_CY2006.Text = "-Title V-"
                        Else
                            lblTitleV_CY2006.Text = "Title V: " & dr.Item("strTVStatus_06")
                        End If

                        If IsDBNull(dr.Item("strFacilityName_06_edit")) Then
                            txtEditFacilityName_CY2006.Text = "Facility Name"
                        Else
                            txtEditFacilityName_CY2006.Text = dr.Item("strFacilityName_06_edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet_06_Edit")) Then
                            txtEditFacilityAddress_CY2006.Text = "Facility Address"
                        Else
                            txtEditFacilityAddress_CY2006.Text = dr.Item("strFacilityStreet_06_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity_06_Edit")) Then
                            txtEditFacilityCity_CY2006.Text = "Facility City"
                        Else
                            txtEditFacilityCity_CY2006.Text = dr.Item("strFacilityCity_06_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityZipCode_06_edit")) Then
                            mtbEditZipCode_CY2006.Clear()
                        Else
                            mtbEditZipCode_CY2006.Text = dr.Item("strFacilityZipCode_06_edit")
                        End If
                        If IsDBNull(dr.Item("strClassification_06_edit")) Then
                            txtEditSourceClass_CY2006.Clear()
                        Else
                            txtEditSourceClass_CY2006.Text = dr.Item("strClassification_06_edit")
                        End If
                        If IsDBNull(dr.Item("strOperatingStatus_06_Edit")) Then
                            cboOperatingStatus_CY2006.Text = ""
                        Else
                            cboOperatingStatus_CY2006.Text = dr.Item("strOperatingStatus_06_Edit")
                        End If
                        If IsDBNull(dr.Item("strNSPSStatus_06_Edit")) Then
                            rdbNSPSYes_CY2006.Checked = False
                            rdbNSPSNo_CY2006.Checked = False
                        Else
                            If dr.Item("strNSPSStatus_06_Edit") = "Yes" Then
                                rdbNSPSYes_CY2006.Checked = True
                            Else
                                rdbNSPSNo_CY2006.Checked = True
                            End If
                        End If
                        If IsDBNull(dr.Item("strTVStatus_06_Edit")) Then
                            rdbTVYes_CY2006.Checked = False
                            rdbTVNo_CY2006.Checked = False
                        Else
                            If dr.Item("strTVStatus_06_Edit") = "Yes" Then
                                rdbTVYes_CY2006.Checked = True
                            Else
                                rdbTVNo_CY2006.Checked = True
                            End If
                        End If

                    End While
                    dr.Close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadChangeQuestions()
        Try
            rdbOwnershipChangeYes.Checked = False
            rdbOwnershipChangeNo.Checked = False
            rdbSourceClassChangeYes.Checked = False
            rdbSourceClassChangeNO.Checked = False

            SQL = "Select " & _
            "strOwnershipChange, strOwnershipComments, " & _
            "strSourceClassChange, strSourceClassComments " & _
            "from " & connNameSpace & ".Fee_NonResponders_2010 " & _
            "where numNonrespondersID = '" & txtNonRespondersID.Text & "' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strOwnershipChange")) Then
                    rdbOwnershipChangeYes.Checked = False
                    rdbOwnershipChangeNo.Checked = False
                Else
                    If dr.Item("strOwnershipChange") = "Yes" Then
                        rdbOwnershipChangeYes.Checked = True
                    Else
                        rdbOwnershipChangeNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strOwnershipComments")) Then
                    txtOwnershipChangeComments.Clear()
                Else
                    txtOwnershipChangeComments.Text = dr.Item("strOwnershipComments")
                End If
                If IsDBNull(dr.Item("strSourceClassChange")) Then
                    rdbSourceClassChangeYes.Checked = False
                    rdbSourceClassChangeNO.Checked = False
                Else
                    If dr.Item("strSourceClassChange") = "Yes" Then
                        rdbSourceClassChangeYes.Checked = True
                    Else
                        rdbSourceClassChangeNO.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strSourceClassComments")) Then
                    txtSourceClassificationChangeComment.Clear()
                Else
                    txtSourceClassificationChangeComment.Text = dr.Item("strSourceClassComments")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadComments()
        Try
            txtComments.Clear()
            If txtNonRespondersID.Text <> "" Then
                SQL = "Select " & _
                "strcomments " & _
                "from " & connNameSpace & ".Fee_NonResponders_2010 " & _
                "where numNonrespondersID = '" & txtNonRespondersID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strcomments")) Then
                        txtComments.Clear()
                    Else
                        txtComments.Text = dr.Item("strcomments")
                    End If
                End While
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadDataByAIRS()
        Try
            SQL = "select " & _
            "numNonRespondersID,  " & _
            "strAIRSNumber, strFacilityName,  " & _
            "strFacilityStreet, strFacilityCity,  " & _
            "strFacilityState, strFacilityZipCode,  " & _
            "strContactFirstName, strContactlastName,  " & _
            "strContactTitle, strContactCompany,  " & _
            "strContactAddress, strContactCity,  " & _
            "strContactState, strContactZipCode,  " & _
            "strContactPhoneNumber, strContactEmail,  " & _
            "strOperatingStatus, strFacilityClass, " & _
            "strNSPSStatus, strTVStatus, " & _
            "strAIRSNumber_08, strAIRSNumber_07, " & _
            "strAIRSNumber_06, strStaffResponsible, " & _
            "(strLastName||', '||strFirstName) as Staff, " & _
            "strActive, " & _
            "Status_08, Status_07, " & _
            "Status_06 " & _
            "from " & connNameSpace & ".Fee_NonResponders_2010,  " & _
            "" & connNameSpace & ".EPDUserProfiles " & _
            "where " & connNameSpace & ".Fee_NonResponders_2010.strStaffResponsible = " & connNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            "and strAIRSNumber = '" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = False Then
                lblNonResponderStaff.Text = "Staff Responsible: "
                txtNonRespondersID.Clear()
                lblFacilityName.Text = "-Facility Name-"
                lblFacilityAddress.Text = "-Facility Address-"
                lblFacilityAddress2.Text = ""
                lblContactName.Text = "-Contact Name-"
                lblContactTitle.Text = "-Contact Title-"
                lblContactCompany.Text = "-Contact Company-"
                lblContactAddress.Text = "-Contact Address-"
                lblContactAddress2.Text = ""
                lblContactPhoneNumber.Text = "-Conatct Phone Number-"
                lblContactEmailAddress.Text = "-Contact Email Address-"
                lblOperatingStatus.Text = "-Operating Stauts-"
                lblSourceClass.Text = "-Source Class-"
                lblNSPS.Text = "-NSPS-"
                lblTitleV.Text = "-Title V-"
                txtAIRSNumber_08.Clear()
                txtAIRSNumber_07.Clear()
                txtAIRSNumber_06.Clear()
                txtNonResponderStaff.Clear()
                rdbNonResponderInactive.Checked = False
                rdbNonResponderActive.Checked = False
                lblCY2008Status.Text = "-Status-"
                lblCY2007Status.Text = "-Status-"
                lblCY2006Status.Text = "-Status-"
            Else
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("numNonRespondersID")) Then
                        txtNonRespondersID.Clear()
                    Else
                        txtNonRespondersID.Text = dr.Item("numNonRespondersID")
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        lblFacilityName.Text = "-Facility Name-"
                    Else
                        lblFacilityName.Text = "Facility Name: " & dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet")) Then
                        lblFacilityAddress.Text = "-Facility Address-"
                    Else
                        lblFacilityAddress2.Text = dr.Item("strFacilityStreet") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        lblFacilityAddress2.Text = lblFacilityAddress2.Text
                    Else
                        lblFacilityAddress2.Text = lblFacilityAddress2.Text & dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityState")) Then
                        lblFacilityAddress2.Text = lblFacilityAddress2.Text
                    Else
                        lblFacilityAddress2.Text = lblFacilityAddress2.Text & ", " & dr.Item("strFacilityState")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        lblFacilityAddress2.Text = lblFacilityAddress2.Text
                    Else
                        If dr.Item("strFacilityZipCode").ToString.Length > 5 Then
                            lblFacilityAddress2.Text = lblFacilityAddress2.Text & " " & Mid(dr.Item("strFacilityZipCode"), 1, 5) & _
                                                "-" & Mid(dr.Item("strFacilityZipCode"), 6)
                        Else
                            lblFacilityAddress2.Text = lblFacilityAddress2.Text & " " & dr.Item("strFacilityZipCode")
                        End If
                    End If
                    If lblFacilityAddress2.Text = "" Then
                        lblFacilityAddress2.Text = ""
                    End If

                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        lblContactName.Text = ""
                    Else
                        lblContactName.Text = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactlastName")) Then
                        lblContactName.Text = lblContactName.Text
                    Else
                        lblContactName.Text = lblContactName.Text & " " & dr.Item("strContactLastName")
                    End If
                    If lblContactName.Text = "" Then
                        lblContactName.Text = "-Contact Name-"
                    Else
                        lblContactName.Text = "Contact Name: " & lblContactName.Text
                    End If

                    If IsDBNull(dr.Item("strContactTitle")) Then
                        lblContactTitle.Text = "-Contact Title-"
                    Else
                        lblContactTitle.Text = "Contact Title: " & dr.Item("strContactTitle")
                    End If
                    If IsDBNull(dr.Item("strContactCompany")) Then
                        lblContactCompany.Text = "-Contact Company-"
                    Else
                        lblContactCompany.Text = "Contact Company: " & dr.Item("strContactCompany")
                    End If
                    If IsDBNull(dr.Item("strContactAddress")) Then
                        lblContactAddress2.Text = "-Contact Address-"
                    Else
                        lblContactAddress2.Text = dr.Item("strContactAddress") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        lblContactAddress2.Text = lblContactAddress2.Text
                    Else
                        lblContactAddress2.Text = lblContactAddress2.Text & dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        lblContactAddress2.Text = lblContactAddress2.Text
                    Else
                        lblContactAddress2.Text = lblContactAddress2.Text & ", " & dr.Item("strContactState")
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        lblContactAddress2.Text = lblContactAddress2.Text
                    Else
                        If dr.Item("strContactZipCode").ToString.Length > 5 Then
                            lblContactAddress2.Text = lblContactAddress2.Text & " " & Mid(dr.Item("strContactZipCode"), 1, 5) & _
                                                "-" & Mid(dr.Item("strContactZipCode"), 6)
                        Else
                            lblContactAddress2.Text = lblContactAddress2.Text & " " & dr.Item("strContactZipCode")
                        End If
                    End If
                    If lblContactAddress2.Text = "" Then
                        lblContactAddress.Text = ""
                    End If
                    If IsDBNull(dr.Item("strContactPhoneNumber")) Then
                        lblContactPhoneNumber.Text = "-Conatct Phone Number-"
                    Else
                        Select Case dr.Item("strContactPhoneNumber").ToString.Length
                            Case 1 To 3
                                lblContactPhoneNumber.Text = "Conatct Phone Number: (" & dr.Item("strContactPhoneNumber") & ")"
                            Case 4 To 6
                                lblContactPhoneNumber.Text = "Conatct Phone Number: (" & Mid(dr.Item("strContactPhoneNumber"), 1, 3) & ") " & _
                                          Mid(dr.Item("strContactPhoneNumber"), 4)
                            Case 7 To 10
                                lblContactPhoneNumber.Text = "Conatct Phone Number: (" & Mid(dr.Item("strContactPhoneNumber"), 1, 3) & ") " & _
                                          Mid(dr.Item("strContactPhoneNumber"), 4, 3) & "-" & Mid(dr.Item("strContactPhoneNumber"), 7)
                            Case Is > 10
                                lblContactPhoneNumber.Text = "Conatct Phone Number: (" & Mid(dr.Item("strContactPhoneNumber"), 1, 3) & ") " & _
                                          Mid(dr.Item("strContactPhoneNumber"), 4, 3) & "-" & Mid(dr.Item("strContactPhoneNumber"), 7, 4) & " ext." & _
                                          Mid(dr.Item("strContactPhoneNumber"), 11)
                            Case Else
                                lblContactPhoneNumber.Text = "Conatct Phone #: (" & dr.Item("strContactPhoneNumber")
                        End Select
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        lblContactEmailAddress.Text = "-Contact Email Address-"
                    Else
                        lblContactEmailAddress.Text = "Contact Email: " & dr.Item("strContactEmail")
                    End If

                    If IsDBNull(dr.Item("strOperatingStatus")) Then
                        lblOperatingStatus.Text = "-Operating Status-"
                    Else
                        lblOperatingStatus.Text = "Op. Status: " & dr.Item("strOperatingStatus")
                    End If
                    If IsDBNull(dr.Item("strFacilityClass")) Then
                        lblSourceClass.Text = "-Source Class-"
                    Else
                        lblSourceClass.Text = "Source Class: " & dr.Item("strFacilityClass")
                    End If
                    If IsDBNull(dr.Item("strNSPSStatus")) Then
                        lblNSPS.Text = "-NSPS-"
                    Else
                        lblNSPS.Text = "NSPS: " & dr.Item("strNSPSStatus")
                    End If
                    If IsDBNull(dr.Item("strTVStatus")) Then
                        lblTitleV.Text = "-Title V-"
                    Else
                        lblTitleV.Text = "Title V: " & dr.Item("strTVStatus")
                    End If
                    If IsDBNull(dr.Item("strAIRSNumber_08")) Then
                        txtAIRSNumber_08.Clear()
                    Else
                        txtAIRSNumber_08.Text = dr.Item("strAIRSNumber_08")
                    End If
                    If IsDBNull(dr.Item("strAIRSNumber_07")) Then
                        txtAIRSNumber_07.Clear()
                    Else
                        txtAIRSNumber_07.Text = dr.Item("strAIRSNumber_07")
                    End If
                    If IsDBNull(dr.Item("strAIRSNumber_06")) Then
                        txtAIRSNumber_06.Clear()
                    Else
                        txtAIRSNumber_06.Text = dr.Item("strAIRSNumber_06")
                    End If

                    If IsDBNull(dr.Item("Staff")) Then
                        txtNonResponderStaff.Clear()
                        lblNonResponderStaff.Text = "Staff Responsible"
                    Else
                        txtNonResponderStaff.Text = dr.Item("Staff")
                        lblNonResponderStaff.Text = "Staff Responsible: " & dr.Item("Staff")
                    End If
                    If IsDBNull(dr.Item("strActive")) Then
                        rdbNonResponderInactive.Checked = False
                        rdbNonResponderActive.Checked = False
                    Else
                        If dr.Item("strActive") = "False" Then
                            rdbNonResponderInactive.Checked = True
                        Else
                            rdbNonResponderActive.Checked = True
                        End If
                    End If
                    If IsDBNull(dr.Item("Status_08")) Then
                        lblCY2008Status.Text = "-Status-"
                    Else
                        lblCY2008Status.Text = "2008 Mailout Status Message: " & dr.Item("Status_08")
                    End If
                    If IsDBNull(dr.Item("Status_07")) Then
                        lblCY2007Status.Text = "-Status-"
                    Else
                        lblCY2007Status.Text = "2007 Mailout Status Message: " & dr.Item("Status_07")
                    End If
                    If IsDBNull(dr.Item("Status_06")) Then
                        lblCY2006Status.Text = "-Status-"
                    Else
                        lblCY2006Status.Text = "2006 Mailout Status Message: " & dr.Item("Status_06")
                    End If

                    If TCNonRespondersData.TabPages.Contains(TP_CY2008) Then
                        TCNonRespondersData.TabPages.Remove(TP_CY2008)
                    End If
                    If TCNonRespondersData.TabPages.Contains(TP_CY2007) Then
                        TCNonRespondersData.TabPages.Remove(TP_CY2007)
                    End If
                    If TCNonRespondersData.TabPages.Contains(TP_CY2006) Then
                        TCNonRespondersData.TabPages.Remove(TP_CY2006)
                    End If
                    If TCNonRespondersData.TabPages.Contains(TP_Change_Questions) Then
                        TCNonRespondersData.TabPages.Remove(TP_Change_Questions)
                    End If
                    If TCNonRespondersData.TabPages.Contains(TP_Comments) Then
                        TCNonRespondersData.TabPages.Remove(TP_Comments)
                    End If

                    If IsDBNull(dr.Item("Status_08")) Then
                    Else
                        Select Case dr.Item("Status_08").ToString
                            Case "Not Subject to Fees"
                            Case "Removed from Fees"
                            Case Else
                                TCNonRespondersData.TabPages.Add(TP_CY2008)
                        End Select
                    End If
                    If IsDBNull(dr.Item("Status_07")) Then
                    Else
                        Select Case dr.Item("Status_07").ToString
                            Case "Not Subject to Fees"
                            Case "Removed from Fees"
                            Case Else
                                TCNonRespondersData.TabPages.Add(TP_CY2007)
                        End Select
                    End If
                    If IsDBNull(dr.Item("Status_06")) Then
                    Else
                        Select Case dr.Item("Status_06").ToString
                            Case "Not Subject to Fees"
                            Case "Removed from Fees"
                            Case Else
                                TCNonRespondersData.TabPages.Add(TP_CY2006)
                        End Select
                    End If
                    TCNonRespondersData.TabPages.Add(TP_Change_Questions)
                    TCNonRespondersData.TabPages.Add(TP_Comments)
                End While
                dr.Close()
            End If

            If txtNonRespondersID.Text <> "" Then
                SQL = "Select " & _
                "strFacilityName_Edit, strFacilityStreet_Edit, " & _
                "strFacilityCity_Edit, strFacilityState_Edit, " & _
                "strFacilityZIpCode_Edit, strFacilityClass_Edit, " & _
                "strOperatingStatus_Edit, strFacilityClass_Edit, " & _
                "strNSPSStatus_Edit, strTVStatus_Edit " & _
                "From " & connNameSpace & ".Fee_NonResponders_2010 " & _
                "where numNonRespondersID = '" & txtNonRespondersID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    txtEditFacilityName.Text = "Facility Name"
                    txtEditFacilityAddress.Text = "Facility Address"
                    txtEditFacilityCity.Text = "Facility City"
                    mtbEditZipCode.Clear()
                    cboOperatingStatus.Text = ""
                    txtEditSourceClass.Clear()
                    rdbTVYes.Checked = False
                    rdbTVNo.Checked = False
                    rdbNSPSYes.Checked = False
                    rdbNSPSNo.Checked = False
                Else
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityName_Edit")) Then
                            txtEditFacilityName.Text = "Facility Name"
                        Else
                            txtEditFacilityName.Text = dr.Item("strFacilityName_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityStreet_Edit")) Then
                            txtEditFacilityAddress.Text = "Facility Address"
                        Else
                            txtEditFacilityAddress.Text = dr.Item("strFacilityStreet_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity_Edit")) Then
                            txtEditFacilityCity.Text = "Facility City"
                        Else
                            txtEditFacilityCity.Text = dr.Item("strFacilityCity_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityZIpCode_Edit")) Then
                            mtbEditZipCode.Clear()
                        Else
                            mtbEditZipCode.Text = dr.Item("strFacilityZIpCode_Edit")
                        End If

                        If IsDBNull(dr.Item("strOperatingStatus_Edit")) Then
                            cboOperatingStatus.Text = ""
                        Else
                            cboOperatingStatus.Text = dr.Item("strOperatingStatus_Edit")
                        End If
                        If IsDBNull(dr.Item("strFacilityClass_Edit")) Then
                            txtEditSourceClass.Clear()
                        Else
                            txtEditSourceClass.Text = dr.Item("strFacilityClass_Edit")
                        End If
                        If IsDBNull(dr.Item("strNSPSStatus_Edit")) Then
                            rdbNSPSYes.Checked = False
                            rdbNSPSNo.Checked = False
                        Else
                            If dr.Item("strNSPSStatus_Edit") = "Yes" Then
                                rdbNSPSYes.Checked = True
                            Else
                                rdbNSPSNo.Checked = True
                            End If
                        End If
                        If IsDBNull(dr.Item("strTVStatus_Edit")) Then
                            rdbTVYes.Checked = False
                            rdbTVNo.Checked = False
                        Else
                            If dr.Item("strTVStatus_Edit") = "Yes" Then
                                rdbTVYes.Checked = True
                            Else
                                rdbTVNo.Checked = True
                            End If
                        End If
                    End While
                    dr.Close()
                End If
            Else
                txtEditFacilityName.Text = "Facility Name"
                txtEditFacilityAddress.Text = "Facility Address"
                txtEditFacilityCity.Text = "Facility City"
                mtbEditZipCode.Clear()
                cboOperatingStatus.Text = ""
                txtEditSourceClass.Clear()
                rdbTVYes.Checked = False
                rdbTVNo.Checked = False
                rdbNSPSYes.Checked = False
                rdbNSPSNo.Checked = False
            End If
            If txtNonRespondersID.Text <> "" Then
                SQL = "Select " & _
                "strContactFirstname_Edit, strContactLastname_Edit, " & _
                "strContactTitle_Edit, strContactCompany_Edit, " & _
                "strContactEmail_Edit, strContactPhoneNumber_Edit, " & _
                "strContactAddress_Edit, strContactCity_Edit, " & _
                "strContactState_Edit, strContactZIpCode_Edit " & _
                "from " & connNameSpace & ".Fee_NonResponders_2010 " & _
                "where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strContactFirstname_Edit")) Then
                            txtEditContactFirstName.Text = "Contact First Name"
                        Else
                            txtEditContactFirstName.Text = dr.Item("strContactFirstname_Edit")
                        End If
                        If IsDBNull(dr.Item("strContactLastname_Edit")) Then
                            txtEditContactLastName.Text = "Contact Last Name"
                        Else
                            txtEditContactLastName.Text = dr.Item("strContactLastname_Edit")
                        End If
                        If IsDBNull(dr.Item("strContactTitle_Edit")) Then
                            txtEditContactTitle.Text = "Contact Title"
                        Else
                            txtEditContactTitle.Text = dr.Item("strContactTitle_Edit")
                        End If
                        If IsDBNull(dr.Item("strContactCompany_Edit")) Then
                            txtEditContactCompany.Text = "Contact Company"
                        Else
                            txtEditContactCompany.Text = dr.Item("strContactCompany_Edit")
                        End If
                        If IsDBNull(dr.Item("strContactEmail_Edit")) Then
                            txtEditContactEmailAddress.Text = "Contact Email Address"
                        Else
                            txtEditContactEmailAddress.Text = dr.Item("strContactEmail_Edit")
                        End If
                        If IsDBNull(dr.Item("strContactPhoneNumber_Edit")) Then
                            txtEditContactPhoneNumber.Text = "Contact Phone Number"
                        Else
                            txtEditContactPhoneNumber.Text = dr.Item("strContactPhoneNumber_Edit")
                        End If
                        If IsDBNull(dr.Item("strContactAddress_Edit")) Then
                            txtEditContactAddress.Text = "Contact Address"
                        Else
                            txtEditContactAddress.Text = dr.Item("strContactAddress_Edit")
                        End If
                        If IsDBNull(dr.Item("strContactCity_Edit")) Then
                            txtEditContactCity.Text = "Contact City"
                        Else
                            txtEditContactCity.Text = dr.Item("strContactCity_Edit")
                        End If

                        If IsDBNull(dr.Item("strContactState_Edit")) Then
                            txtEditContactState.Clear()
                        Else
                            txtEditContactState.Text = dr.Item("strContactState_Edit")
                        End If

                        If IsDBNull(dr.Item("strContactZIpCode_Edit")) Then
                            mtbEditContactZipCode.Clear()
                        Else
                            mtbEditContactZipCode.Text = dr.Item("strContactZIpCode_Edit")
                        End If
                    End While
                    dr.Close()
                Else
                    txtEditContactFirstName.Text = "Contact First Name"
                    txtEditContactLastName.Text = "Contact Last Name"
                    txtEditContactTitle.Text = "Contact Title"
                    txtEditContactCompany.Text = "Contact Company"
                    txtEditContactAddress.Text = "Contact Address"
                    txtEditContactCity.Text = "Contact City"
                    txtEditContactState.Clear()
                    mtbEditContactZipCode.Clear()
                    txtEditContactPhoneNumber.Text = "Contact Phone Number"
                    txtEditContactEmailAddress.Text = "Contact Email Address"
                End If
            Else
                txtEditContactFirstName.Text = "Contact First Name"
                txtEditContactLastName.Text = "Contact Last Name"
                txtEditContactTitle.Text = "Contact Title"
                txtEditContactCompany.Text = "Contact Company"
                txtEditContactAddress.Text = "Contact Address"
                txtEditContactCity.Text = "Contact City"
                txtEditContactState.Clear()
                mtbEditContactZipCode.Clear()
                txtEditContactPhoneNumber.Text = "Contact Phone Number"
                txtEditContactEmailAddress.Text = "Contact Email Address"
            End If
            If txtNonRespondersID.Text <> "" Then
                SQL = "Select " & _
                "strCurrentInfoComments " & _
                "From " & connNameSpace & ".Fee_NonResponders_2010 " & _
                "where numNonRespondersID = '" & txtNonRespondersID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strCurrentInfoComments")) Then
                            txtCurrentComments.Clear()
                        Else
                            txtCurrentComments.Text = dr.Item("strCurrentInfoComments")
                        End If
                    End While
                    dr.Close()
                Else
                    txtCurrentComments.Clear()
                End If
            Else
                txtCurrentComments.Clear()
            End If

            If txtAIRSNumber_08.Text <> "" Then
                LoadCY2008()
            End If
            If txtAIRSNumber_07.Text <> "" Then
                LoadCY2007()
            End If
            If txtAIRSNumber_06.Text <> "" Then
                LoadCY2006()
            End If

            LoadChangeQuestions()
            LoadComments()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadNonPayer()
        Try
            txtNonPayerID.Clear()
            lblNonPayerStaff.Text = "Staff Responsible"
            lblNonPayerFacilityName.Text = "Facility Name"
            lblNonPayerFacilityAddress.Text = ""
            lblNonPayerOpStatus.Text = "Operating Status"
            lblNonPayerSourceClass.Text = "Source Class"
            lblNonPayerTVStatus.Text = "Title V"
            lblNonPayerNSPSStatus.Text = "NSPS"
            lblNonPayerContactName.Text = "Contact Name"
            lblNonPayerContactTitle.Text = "Contact Title"
            lblNonPayerContactCompany.Text = "Contact Company"
            lblNonPayerContactAddress.Text = ""
            lblNonPayerContactPhoneNumber.Text = "Contact Phone Number"
            lblNonPayerContactEmail.Text = "Contact Email Address"
            GBNonPayer_CY08.Visible = False
            GBNonPayer_CY07.Visible = False
            GBNonPayer_CY06.Visible = False
            GBNonPayer_CY05.Visible = False
            GBNonPayer_CY04.Visible = False
            GBNonPayer_CY03.Visible = False
            GBNonPayer_CY02.Visible = False
            rdbNonPayerActive.Checked = False
            rdbNonPayerInactive.Checked = False
            txtNonPayerFirstname.Clear()
            txtNonPayerLastName.Clear()
            txtNonPayerTitle.Clear()
            txtNonPayerCompany.Clear()
            txtNonPayerAddress.Clear()
            txtNonPayerCity.Clear()
            txtNonPayerState.Clear()
            mtbNonPayerZipCode.Clear()
            txtNonPayerPhoneNumber.Clear()
            txtNonPayerEmail.Clear()

            SQL = "Select * " & _
            "from " & connNameSpace & ".Fee_NonPayers_2010 " & _
            "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "

            SQL = "Select " & _
"numNonPayersID,  " & _
"strAIRSNumber, strFacilityName,  " & _
"strFacilityStreet, strFacilityCity,  " & _
"strFacilityState, strFacilityZipCode,  " & _
"strOperationalStatus, strClassification,  " & _
"strSICCode, datShutDownDate,  " & _
"strNSPSStatus, strTVStatus,  " & _
"strStatus_08, strTotalDue_08,  " & _
"strTotalPaid_08, strBalance_08,  " & _
"strStatus_07, strTotalDue_07,  " & _
"strTotalPaid_07, strBalance_07,  " & _
"strStatus_06, strTotalDue_06,  " & _
"strTotalPaid_06, strBalance_06,  " & _
"strStatus_05, strTotalDue_05,  " & _
"strTotalPaid_05, strBalance_05,  " & _
"strStatus_04, strTotalDue_04,  " & _
"strTotalPaid_04, strBalance_04,  " & _
"strStatus_03, strTotalDue_03,  " & _
"strTotalPaid_03, strBalance_03,  " & _
"strStatus_02, strTotalDue_02,  " & _
"strTotalPaid_02, strBalance_02,  " & _
"strContactFirstName, strContactLastName,  " & _
"strContactTitle, strContactCOmpanyName,  " & _
"strContactPhoneNumber, strContactFaxNumber,  " & _
"strContactEmail, strContactAddress,  " & _
"strContactCity, strContactState,  " & _
"strContactZipCode, strContactDescription,  " & _
"numStaffResponsible, strActive,  " & _
"strComments,  " & _
"(strLastName||', '||strFirstName) as Staff    " & _
"from " & connNameSpace & ".Fee_NonPayers_2010, " & connNameSpace & ".EPDUserProfiles   " & _
"where " & connNameSpace & ".Fee_NonPayers_2010.numStaffResponsible = " & connNameSpace & ".EPDUserProfiles.numUserID  (+) " & _
"and strAIRSNumber = '" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("numNonPayersID")) Then
                        txtNonPayerID.Clear()
                    Else
                        txtNonPayerID.Text = dr.Item("numNonPayersID")
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        lblNonPayerFacilityName.Text = "Facility Name"
                    Else
                        lblNonPayerFacilityName.Text = "Facility Name: " & dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet")) Then
                        lblNonPayerFacilityAddress.Text = "" & vbCrLf
                    Else
                        lblNonPayerFacilityAddress.Text = dr.Item("strFacilityStreet") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        lblNonPayerFacilityAddress.Text = lblNonPayerFacilityAddress.Text
                    Else
                        lblNonPayerFacilityAddress.Text = lblNonPayerFacilityAddress.Text & dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityState")) Then
                        lblNonPayerFacilityAddress.Text = lblNonPayerFacilityAddress.Text
                    Else
                        lblNonPayerFacilityAddress.Text = lblNonPayerFacilityAddress.Text & ", " & dr.Item("strFacilityState")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        lblNonPayerFacilityAddress.Text = lblNonPayerFacilityAddress.Text
                    Else
                        If dr.Item("strFacilityZipCode").ToString.Length > 5 Then
                            lblNonPayerFacilityAddress.Text = lblNonPayerFacilityAddress.Text & " " & Mid(dr.Item("strFacilityZipCode"), 1, 5) & _
                                                "-" & Mid(dr.Item("strFacilityZipCode"), 6)
                        Else
                            lblNonPayerFacilityAddress.Text = lblNonPayerFacilityAddress.Text & " " & dr.Item("strFacilityZipCode")
                        End If
                    End If
                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        lblNonPayerOpStatus.Text = "Operating Status"
                    Else
                        lblNonPayerOpStatus.Text = "Op. Status: " & dr.Item("strOperationalStatus")
                    End If
                    If IsDBNull(dr.Item("datShutDownDate")) Then
                        lblNonPayerOpStatus.Text = lblNonPayerOpStatus.Text
                    Else
                        lblNonPayerOpStatus.Text = lblNonPayerOpStatus.Text & " - " & dr.Item("datShutDownDate")
                    End If
                    If IsDBNull(dr.Item("strClassification")) Then
                        lblNonPayerSourceClass.Text = "Source Class"
                    Else
                        lblNonPayerSourceClass.Text = "Source Class: " & dr.Item("strClassification")
                    End If
                    If IsDBNull(dr.Item("strNSPSStatus")) Then
                        lblNonPayerNSPSStatus.Text = "NSPS"
                    Else
                        lblNonPayerNSPSStatus.Text = "NSPS: " & dr.Item("strNSPSStatus")
                    End If
                    If IsDBNull(dr.Item("strTVStatus")) Then
                        lblNonPayerTVStatus.Text = "Title V"
                    Else
                        lblNonPayerTVStatus.Text = "Title V: " & dr.Item("strTVStatus")
                    End If
                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        lblNonPayerContactName.Text = "Contact Name"
                        txtNonPayerFirstname.Text = "Contact First Name"
                    Else
                        lblNonPayerContactName.Text = "Contact Name: " & dr.Item("strContactFirstName")
                        txtNonPayerFirstname.Text = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        lblNonPayerContactName.Text = lblNonPayerContactName.Text
                        txtNonPayerLastName.Text = "Contact Last Name"
                    Else
                        lblNonPayerContactName.Text = lblNonPayerContactName.Text & " " & dr.Item("strContactLastName")
                        txtNonPayerLastName.Text = dr.Item("strContactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactTitle")) Then
                        lblNonPayerContactTitle.Text = "Contact Title"
                        txtNonPayerTitle.Text = "Contact Title"
                    Else
                        lblNonPayerContactTitle.Text = "Contact Title: " & dr.Item("strContactTitle")
                        txtNonPayerTitle.Text = dr.Item("strContactTitle")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        lblNonPayerContactCompany.Text = "Contact Company"
                        txtNonPayerCompany.Text = "Contact Company"
                    Else
                        lblNonPayerContactCompany.Text = "Contact Company: " & dr.Item("strContactCompanyName")
                        txtNonPayerCompany.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress")) Then
                        lblNonPayerContactAddress.Text = ""
                        txtNonPayerAddress.Text = "Company Address"
                    Else
                        lblNonPayerContactAddress.Text = dr.Item("strContactAddress") & vbCrLf
                        txtNonPayerAddress.Text = dr.Item("strContactAddress")
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        lblNonPayerContactAddress.Text = lblNonPayerContactAddress.Text
                        txtNonPayerCity.Text = "Contact City"
                    Else
                        lblNonPayerContactAddress.Text = lblNonPayerContactAddress.Text & dr.Item("strContactCity")
                        txtNonPayerCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        lblNonPayerContactAddress.Text = lblNonPayerContactAddress.Text
                        txtNonPayerState.Clear()
                    Else
                        lblNonPayerContactAddress.Text = lblNonPayerContactAddress.Text & ", " & dr.Item("strContactState")
                        txtNonPayerState.Text = dr.Item("strContactState")
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        lblNonPayerContactAddress.Text = lblNonPayerContactAddress.Text
                        mtbNonPayerZipCode.Clear()
                    Else
                        If dr.Item("strContactZipCode").ToString.Length > 5 Then
                            lblNonPayerContactAddress.Text = lblNonPayerContactAddress.Text & " " & Mid(dr.Item("strContactZipCode"), 1, 5) & _
                                                "-" & Mid(dr.Item("strContactZipCode"), 6)
                        Else
                            lblNonPayerContactAddress.Text = lblNonPayerContactAddress.Text & " " & dr.Item("strContactZipCode")
                        End If
                        mtbNonPayerZipCode.Text = dr.Item("strContactZipCode")
                    End If
                    If IsDBNull(dr.Item("strContactPhoneNumber")) Then
                        lblNonPayerContactPhoneNumber.Text = "Contact Phone Number"
                        txtNonPayerPhoneNumber.Text = "Contact Phone Number"
                    Else
                        Select Case dr.Item("strContactPhoneNumber").ToString.Length
                            Case 1 To 3
                                lblNonPayerContactPhoneNumber.Text = "Conatct Phone Number: (" & dr.Item("strContactPhoneNumber") & ")"
                            Case 4 To 6
                                lblNonPayerContactPhoneNumber.Text = "Conatct Phone Number: (" & Mid(dr.Item("strContactPhoneNumber"), 1, 3) & ") " & _
                                          Mid(dr.Item("strContactPhoneNumber"), 4)
                            Case 7 To 10
                                lblNonPayerContactPhoneNumber.Text = "Conatct Phone Number: (" & Mid(dr.Item("strContactPhoneNumber"), 1, 3) & ") " & _
                                          Mid(dr.Item("strContactPhoneNumber"), 4, 3) & "-" & Mid(dr.Item("strContactPhoneNumber"), 7)
                            Case Is > 10
                                lblNonPayerContactPhoneNumber.Text = "Conatct Phone Number: (" & Mid(dr.Item("strContactPhoneNumber"), 1, 3) & ") " & _
                                          Mid(dr.Item("strContactPhoneNumber"), 4, 3) & "-" & Mid(dr.Item("strContactPhoneNumber"), 7, 4) & " ext." & _
                                          Mid(dr.Item("strContactPhoneNumber"), 11)
                            Case Else
                                lblNonPayerContactPhoneNumber.Text = "Conatct Phone #: (" & dr.Item("strContactPhoneNumber")
                        End Select
                        txtNonPayerPhoneNumber.Text = dr.Item("strContactPhoneNumber")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        lblNonPayerContactEmail.Text = "Contact Email Address"
                        txtNonPayerEmail.Text = "Contact Email Address"
                    Else
                        lblNonPayerContactEmail.Text = "Contact Email Address: " & dr.Item("strContactEmail")
                        txtNonPayerEmail.Text = dr.Item("strContactEmail")
                    End If
                    If IsDBNull(dr.Item("strStatus_08")) Then
                        GBNonPayer_CY08.Visible = False
                    Else
                        GBNonPayer_CY08.Visible = True
                        If IsDBNull(dr.Item("strTotalDue_08")) Then
                            lblNonPayerTotalDue_CY08.Text = "Total Due"
                        Else
                            lblNonPayerTotalDue_CY08.Text = "Total Due: " & dr.Item("strTotalDue_08")
                        End If
                        If IsDBNull(dr.Item("strTotalPaid_08")) Then
                            lblNonPayerTotalPaid_CY08.Text = "Total Paid"
                        Else
                            lblNonPayerTotalPaid_CY08.Text = "Total Paid: " & dr.Item("strTotalPaid_08")
                        End If
                        If IsDBNull(dr.Item("strBalance_08")) Then
                            lblNonPayerBalance_CY08.Text = "Balance"
                        Else
                            lblNonPayerBalance_CY08.Text = "Balance: " & dr.Item("strBalance_08")
                        End If
                    End If
                    If IsDBNull(dr.Item("strStatus_07")) Then
                        GBNonPayer_CY07.Visible = False
                    Else
                        GBNonPayer_CY07.Visible = True
                        If IsDBNull(dr.Item("strTotalDue_07")) Then
                            lblNonPayerTotalDue_CY07.Text = "Total Due"
                        Else
                            lblNonPayerTotalDue_CY07.Text = "Total Due: " & dr.Item("strTotalDue_07")
                        End If
                        If IsDBNull(dr.Item("strTotalPaid_07")) Then
                            lblNonPayerTotalPaid_CY07.Text = "Total Paid"
                        Else
                            lblNonPayerTotalPaid_CY07.Text = "Total Paid: " & dr.Item("strTotalPaid_07")
                        End If
                        If IsDBNull(dr.Item("strBalance_07")) Then
                            lblNonPayerBalance_CY07.Text = "Balance"
                        Else
                            lblNonPayerBalance_CY07.Text = "Balance: " & dr.Item("strBalance_07")
                        End If
                    End If
                    If IsDBNull(dr.Item("strStatus_06")) Then
                        GBNonPayer_CY06.Visible = False
                    Else
                        GBNonPayer_CY06.Visible = True
                        If IsDBNull(dr.Item("strTotalDue_06")) Then
                            lblNonPayerTotalDue_CY06.Text = "Total Due"
                        Else
                            lblNonPayerTotalDue_CY06.Text = "Total Due: " & dr.Item("strTotalDue_06")
                        End If
                        If IsDBNull(dr.Item("strTotalPaid_06")) Then
                            lblNonPayerTotalPaid_CY06.Text = "Total Paid"
                        Else
                            lblNonPayerTotalPaid_CY06.Text = "Total Paid: " & dr.Item("strTotalPaid_06")
                        End If
                        If IsDBNull(dr.Item("strBalance_06")) Then
                            lblNonPayerBalance_CY06.Text = "Balance"
                        Else
                            lblNonPayerBalance_CY06.Text = "Balance: " & dr.Item("strBalance_06")
                        End If
                    End If
                    If IsDBNull(dr.Item("strStatus_05")) Then
                        GBNonPayer_CY05.Visible = False
                    Else
                        GBNonPayer_CY05.Visible = True
                        If IsDBNull(dr.Item("strTotalDue_05")) Then
                            lblNonPayerTotalDue_CY05.Text = "Total Due"
                        Else
                            lblNonPayerTotalDue_CY05.Text = "Total Due: " & dr.Item("strTotalDue_05")
                        End If
                        If IsDBNull(dr.Item("strTotalPaid_05")) Then
                            lblNonPayerTotalPaid_CY05.Text = "Total Paid"
                        Else
                            lblNonPayerTotalPaid_CY05.Text = "Total Paid: " & dr.Item("strTotalPaid_05")
                        End If
                        If IsDBNull(dr.Item("strBalance_05")) Then
                            lblNonPayerBalance_CY05.Text = "Balance"
                        Else
                            lblNonPayerBalance_CY05.Text = "Balance: " & dr.Item("strBalance_05")
                        End If
                    End If
                    If IsDBNull(dr.Item("strStatus_04")) Then
                        GBNonPayer_CY04.Visible = False
                    Else
                        GBNonPayer_CY04.Visible = True
                        If IsDBNull(dr.Item("strTotalDue_04")) Then
                            lblNonPayerTotalDue_CY04.Text = "Total Due"
                        Else
                            lblNonPayerTotalDue_CY04.Text = "Total Due: " & dr.Item("strTotalDue_04")
                        End If
                        If IsDBNull(dr.Item("strTotalPaid_04")) Then
                            lblNonPayerTotalPaid_CY04.Text = "Total Paid"
                        Else
                            lblNonPayerTotalPaid_CY04.Text = "Total Paid: " & dr.Item("strTotalPaid_04")
                        End If
                        If IsDBNull(dr.Item("strBalance_04")) Then
                            lblNonPayerBalance_CY04.Text = "Balance"
                        Else
                            lblNonPayerBalance_CY04.Text = "Balance: " & dr.Item("strBalance_04")
                        End If
                    End If
                    If IsDBNull(dr.Item("strStatus_03")) Then
                        GBNonPayer_CY03.Visible = False
                    Else
                        GBNonPayer_CY03.Visible = True
                        If IsDBNull(dr.Item("strTotalDue_03")) Then
                            lblNonPayerTotalDue_CY03.Text = "Total Due"
                        Else
                            lblNonPayerTotalDue_CY03.Text = "Total Due: " & dr.Item("strTotalDue_03")
                        End If
                        If IsDBNull(dr.Item("strTotalPaid_03")) Then
                            lblNonPayerTotalPaid_CY03.Text = "Total Paid"
                        Else
                            lblNonPayerTotalPaid_CY03.Text = "Total Paid: " & dr.Item("strTotalPaid_03")
                        End If
                        If IsDBNull(dr.Item("strBalance_03")) Then
                            lblNonPayerBalance_CY03.Text = "Balance"
                        Else
                            lblNonPayerBalance_CY03.Text = "Balance: " & dr.Item("strBalance_03")
                        End If
                    End If
                    If IsDBNull(dr.Item("strStatus_02")) Then
                        GBNonPayer_CY02.Visible = False
                    Else
                        GBNonPayer_CY02.Visible = True
                        If IsDBNull(dr.Item("strTotalDue_02")) Then
                            lblNonPayerTotalDue_CY02.Text = "Total Due"
                        Else
                            lblNonPayerTotalDue_CY02.Text = "Total Due: " & dr.Item("strTotalDue_02")
                        End If
                        If IsDBNull(dr.Item("strTotalPaid_02")) Then
                            lblNonPayerTotalPaid_CY02.Text = "Total Paid"
                        Else
                            lblNonPayerTotalPaid_CY02.Text = "Total Paid: " & dr.Item("strTotalPaid_02")
                        End If
                        If IsDBNull(dr.Item("strBalance_02")) Then
                            lblNonPayerBalance_CY02.Text = "Balance"
                        Else
                            lblNonPayerBalance_CY02.Text = "Balance: " & dr.Item("strBalance_02")
                        End If
                    End If
                    If IsDBNull(dr.Item("strACtive")) Then
                        rdbNonPayerActive.Checked = False
                        rdbNonPayerInactive.Checked = False
                    Else
                        If dr.Item("strActive") = "True" Then
                            rdbNonPayerActive.Checked = True
                        Else
                            rdbNonPayerInactive.Checked = True
                        End If
                    End If
                    If IsDBNull(dr.Item("strComments")) Then
                        txtNonPayersComments.Clear()
                    Else
                        txtNonPayersComments.Text = dr.Item("strComments")
                    End If
                    If IsDBNull(dr.Item("Staff")) Then
                        txtNonPayerStaff.Clear()
                        lblNonPayerStaff.Text = "Staff Responsible - "
                    Else
                        txtNonPayerStaff.Text = dr.Item("Staff")
                        lblNonPayerStaff.Text = "Staff Responsible: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAuditData()
        Try
            Dim Status_08 As String = ""
            Dim Status_07 As String = ""
            Dim Status_06 As String = ""
            Dim Status_05 As String = ""
            Dim Status_04 As String = ""
            Dim Status_03 As String = ""
            Dim Status_02 As String = ""
            Dim Staff_08 As String = ""
            Dim Staff_07 As String = ""
            Dim Staff_06 As String = ""
            Dim Staff_05 As String = ""
            Dim Staff_04 As String = ""
            Dim Staff_03 As String = ""
            Dim Staff_02 As String = ""
            Dim Manager_08 As String = ""
            Dim Manager_07 As String = ""
            Dim Manager_06 As String = ""
            Dim Manager_05 As String = ""
            Dim Manager_04 As String = ""
            Dim Manager_03 As String = ""
            Dim Manager_02 As String = ""

            If txtAIRSNumber_08.Text <> "" Then
                Status_08 = "NonResponder"
            Else
                Status_08 = ""
            End If
            If txtAIRSNumber_07.Text <> "" Then
                Status_07 = "NonResponder"
            Else
                Status_07 = ""
            End If
            If txtAIRSNumber_06.Text <> "" Then
                Status_06 = "NonResponder"
            Else
                Status_06 = ""
            End If

            SQL = "Select " & _
            "strStatus_08, strStatus_07, " & _
            "strStatus_06, strStatus_05, " & _
            "strStatus_04, strStatus_03, " & _
            "strStatus_02 " & _
            "from " & connNameSpace & ".Fee_NonPayers_2010 " & _
            "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strStatus_08")) Then
                    'Status_08 = Status_08
                Else
                    Status_08 = "NonPayer"
                End If
                If IsDBNull(dr.Item("strStatus_07")) Then
                    ' Status_07 = Status_07
                Else
                    Status_07 = "NonPayer"
                End If
                If IsDBNull(dr.Item("strStatus_06")) Then
                    'Status_06 = Status_06
                Else
                    Status_06 = "NonPayer"
                End If
                If IsDBNull(dr.Item("strStatus_05")) Then
                    ' Status_05 = Status_05
                Else
                    Status_05 = "NonPayer"
                End If
                If IsDBNull(dr.Item("strStatus_04")) Then
                    ' Status_04 = Status_04
                Else
                    Status_04 = "NonPayer"
                End If
                If IsDBNull(dr.Item("strStatus_03")) Then
                    ' Status_03 = Status_03
                Else
                    Status_03 = "NonPayer"
                End If
                If IsDBNull(dr.Item("strStatus_02")) Then
                    'Status_02 = Status_02
                Else
                    Status_02 = "NonPayer"
                End If
            End While
            dr.Close()

            If Status_08 <> "" Then
                TCFeeAuditTracking.TabPages.Add(TP_Tracking_CY2008)
                If Status_08 = "NonResponder" Then
                    lblAuditType_CY2008.Text = "Nonresponder for CY 2008 - Staff Responsible: " & txtNonResponderStaff.Text
                Else
                    lblAuditType_CY2008.Text = "Nonpayer for CY 2008"
                End If

                lblAmountPaid_CY2008.Text = "Fees Paid: -"
                SQL = "Select " & _
                "sum(numPayment) as FeesPaid " & _
                "from " & connNameSpace & ".FSAddPaid " & _
                "where intYear = '2008' " & _
                "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("FeesPaid")) Then
                        lblAmountPaid_CY2008.Text = "Fees Paid: -"
                    Else
                        lblAmountPaid_CY2008.Text = "Fees Paid: " & dr.Item("FeesPaid")
                    End If
                End While
                dr.Close()
            End If
            If Status_07 <> "" Then
                TCFeeAuditTracking.TabPages.Add(TP_Tracking_CY2007)
                If Status_07 = "NonResponder" Then
                    lblAuditType_CY2007.Text = "Nonresponder for CY 2007 - Staff Responsible: " & txtNonResponderStaff.Text
                Else
                    lblAuditType_CY2007.Text = "Nonpayer for CY 2007"
                End If

                lblAmountPaid_CY2007.Text = "Fees Paid: -"
                SQL = "Select " & _
                "sum(numPayment) as FeesPaid " & _
                "from " & connNameSpace & ".FSAddPaid " & _
                "where intYear = '2007' " & _
                "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("FeesPaid")) Then
                        lblAmountPaid_CY2007.Text = "Fees Paid: -"
                    Else
                        lblAmountPaid_CY2007.Text = "Fees Paid: " & dr.Item("FeesPaid")
                    End If
                End While
                dr.Close()
            End If
            If Status_06 <> "" Then
                TCFeeAuditTracking.TabPages.Add(TP_Tracking_CY2006)
                If Status_06 = "NonResponder" Then
                    lblAuditType_CY2006.Text = "Nonresponder for CY 2006 - Staff Responsible: " & txtNonResponderStaff.Text
                Else
                    lblAuditType_CY2006.Text = "Nonpayer for CY 2006"
                End If

                lblAmountPaid_CY2006.Text = "Fees Paid: -"
                SQL = "Select " & _
                "sum(numPayment) as FeesPaid " & _
                "from " & connNameSpace & ".FSAddPaid " & _
                "where intYear = '2006' " & _
                "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("FeesPaid")) Then
                        lblAmountPaid_CY2006.Text = "Fees Paid: -"
                    Else
                        lblAmountPaid_CY2006.Text = "Fees Paid: " & dr.Item("FeesPaid")
                    End If
                End While
                dr.Close()
            End If
            If Status_05 <> "" Then
                TCFeeAuditTracking.TabPages.Add(TP_Tracking_CY2005)
                If Status_05 = "NonResponder" Then
                    lblAuditType_CY2005.Text = "Nonresponder for CY 2005 - Staff Responsible: " & txtNonResponderStaff.Text
                Else
                    lblAuditType_CY2005.Text = "Nonpayer for CY 2005"
                End If

                lblAmountPaid_CY2005.Text = "Fees Paid: -"
                SQL = "Select " & _
                "sum(numPayment) as FeesPaid " & _
                "from " & connNameSpace & ".FSAddPaid " & _
                "where intYear = '2005' " & _
                "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("FeesPaid")) Then
                        lblAmountPaid_CY2005.Text = "Fees Paid: -"
                    Else
                        lblAmountPaid_CY2005.Text = "Fees Paid: " & dr.Item("FeesPaid")
                    End If
                End While
                dr.Close()
            End If
            If Status_04 <> "" Then
                TCFeeAuditTracking.TabPages.Add(TP_Tracking_CY2004)
                If Status_04 = "NonResponder" Then
                    lblAuditType_CY2004.Text = "Nonresponder for CY 2004 - Staff Responsible: " & txtNonResponderStaff.Text
                Else
                    lblAuditType_CY2004.Text = "Nonpayer for CY 2004"
                End If

                lblAmountPaid_CY2004.Text = "Fees Paid: -"
                SQL = "Select " & _
                "sum(numPayment) as FeesPaid " & _
                "from " & connNameSpace & ".FSAddPaid " & _
                "where intYear = '2004' " & _
                "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("FeesPaid")) Then
                        lblAmountPaid_CY2004.Text = "Fees Paid: -"
                    Else
                        lblAmountPaid_CY2004.Text = "Fees Paid: " & dr.Item("FeesPaid")
                    End If
                End While
                dr.Close()
            End If
            If Status_03 <> "" Then
                TCFeeAuditTracking.TabPages.Add(TP_Tracking_CY2003)
                If Status_03 = "NonResponder" Then
                    lblAuditType_CY2003.Text = "Nonresponder for CY 2003 - Staff Responsible: " & txtNonResponderStaff.Text
                Else
                    lblAuditType_CY2003.Text = "Nonpayer for CY 2003"
                End If

                lblAmountPaid_CY2003.Text = "Fees Paid: -"
                SQL = "Select " & _
                "sum(numPayment) as FeesPaid " & _
                "from " & connNameSpace & ".FSAddPaid " & _
                "where intYear = '2003' " & _
                "and strAIRSNumber = '0313" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("FeesPaid")) Then
                        lblAmountPaid_CY2003.Text = "Fees Paid: -"
                    Else
                        lblAmountPaid_CY2003.Text = "Fees Paid: " & dr.Item("FeesPaid")
                    End If
                End While
                dr.Close()
            End If
            If Status_02 <> "" Then
                TCFeeAuditTracking.TabPages.Add(TP_Tracking_CY2002)
                If Status_02 = "NonResponder" Then
                    lblAuditType_CY2002.Text = "Nonresponder for CY 2002 - Staff Responsible: " & txtNonResponderStaff.Text
                Else
                    lblAuditType_CY2002.Text = "Nonpayer for CY 2002"
                End If

                lblAmountPaid_CY2002.Text = "Fees Paid: -"
                SQL = "Select " & _
                "sum(numPayment) as FeesPaid " & _
                "from " & connNameSpace & ".FSAddPaid " & _
                "where intYear = '2002' " & _
                "and strAIRSNumber = '0213" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("FeesPaid")) Then
                        lblAmountPaid_CY2002.Text = "Fees Paid: -"
                    Else
                        lblAmountPaid_CY2002.Text = "Fees Paid: " & dr.Item("FeesPaid")
                    End If
                End While
                dr.Close()
            End If

            TCFeeAuditTracking.TabPages.Add(TP_Tracking_OtherComments)

            SQL = "Select " & _
              "numAuditID, " & _
              "strAIRSNumber, " & _
              "datInitialLetterMailed_08, datLetterReturned_08, " & _
              "strAddressUnknown_08, datInitialLetterRemailed_08, " & _
              "strFacilityResponded_08, STRfACILITYbANKRUPT_08, " & _
              "strUnableToContact_08, " & _
              "datNOVLetterSent_08, datCOLetterSent_08, " & _
              "datAOLetterSent_08, datFacilityPaidFees_08, " & _
              "datClosedOutFeeAudit_08, numStaffAssigned_08, " & _
              "datLastModified_08, numManagerSignOff_08, " & _
              "datManagerSignOff_08, strComments_08, " & _
              "datInitialLetterMailed_07, datLetterReturned_07, " & _
              "strAddressUnknown_07, datInitialLetterRemailed_07, " & _
              "strFacilityResponded_07, STRfACILITYbANKRUPT_07, " & _
              "strUnableToContact_07, " & _
              "datNOVLetterSent_07, datCOLetterSent_07, " & _
              "datAOLetterSent_07, datFacilityPaidFees_07, " & _
              "datClosedOutFeeAudit_07, numStaffAssigned_07, " & _
              "datLastModified_07, numManagerSignOff_07, " & _
              "datManagerSignOff_07,strComments_07,  " & _
              "datInitialLetterMailed_06, datLetterReturned_06, " & _
              "strAddressUnknown_06, datInitialLetterRemailed_06, " & _
              "strFacilityResponded_06, STRfACILITYbANKRUPT_06, " & _
              "strUnableToContact_06, " & _
              "datNOVLetterSent_06, datCOLetterSent_06, " & _
              "datAOLetterSent_06, datFacilityPaidFees_06, " & _
              "datClosedOutFeeAudit_06, numStaffAssigned_06, " & _
              "datLastModified_06, numManagerSignOff_06, " & _
              "datManagerSignOff_06,strComments_06,  " & _
              "datInitialLetterMailed_05, datLetterReturned_05, " & _
              "strAddressUnknown_05, datInitialLetterRemailed_05, " & _
              "strFacilityResponded_05, STRfACILITYbANKRUPT_05, " & _
              "strUnableToContact_05, " & _
              "datNOVLetterSent_05, datCOLetterSent_05, " & _
              "datAOLetterSent_05, datFacilityPaidFees_05, " & _
              "datClosedOutFeeAudit_05, numStaffAssigned_05, " & _
              "datLastModified_05, numManagerSignOff_05, " & _
              "datManagerSignOff_05,strComments_05,  " & _
              "datInitialLetterMailed_04, datLetterReturned_04, " & _
              "strAddressUnknown_04, datInitialLetterRemailed_04, " & _
              "strFacilityResponded_04, STRfACILITYbANKRUPT_04, " & _
              "strUnableToContact_04, " & _
              "datNOVLetterSent_04, datCOLetterSent_04, " & _
              "datAOLetterSent_04, datFacilityPaidFees_04, " & _
              "datClosedOutFeeAudit_04, numStaffAssigned_04, " & _
              "datLastModified_04, numManagerSignOff_04, " & _
              "datManagerSignOff_04,strComments_04,  " & _
              "datInitialLetterMailed_03, datLetterReturned_03, " & _
              "strAddressUnknown_03, datInitialLetterRemailed_03, " & _
              "strFacilityResponded_03, STRfACILITYbANKRUPT_03, " & _
              "strUnableToContact_03, " & _
              "datNOVLetterSent_03, datCOLetterSent_03, " & _
              "datAOLetterSent_03, datFacilityPaidFees_03, " & _
              "datClosedOutFeeAudit_03, numStaffAssigned_03, " & _
              "datLastModified_03, numManagerSignOff_03, " & _
              "datManagerSignOff_03,strComments_03,  " & _
              "datInitialLetterMailed_02, datLetterReturned_02, " & _
              "strAddressUnknown_02, datInitialLetterRemailed_02, " & _
              "strFacilityResponded_02, STRfACILITYbANKRUPT_02, " & _
              "strUnableToContact_02, " & _
              "datNOVLetterSent_02, datCOLetterSent_02, " & _
              "datAOLetterSent_02, datFacilityPaidFees_02, " & _
              "datClosedOutFeeAudit_02, numStaffAssigned_02, " & _
              "datLastModified_02, numManagerSignOff_02, " & _
              "datManagerSignOff_02,strComments_02,  " & _
              "strComments " & _
              "from " & connNameSpace & ".Fee_Audit_2010 " & _
              "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numAuditID")) Then
                    txtAuditID.Clear()
                Else
                    txtAuditID.Text = dr.Item("numAuditID")
                End If
                If IsDBNull(dr.Item("datInitialLetterMailed_08")) Then
                    DTPInitialLetter_2008.Text = OracleDate
                    DTPInitialLetter_2008.Checked = False
                Else
                    DTPInitialLetter_2008.Text = dr.Item("datInitialLetterMailed_08")
                    DTPInitialLetter_2008.Checked = True
                End If
                If IsDBNull(dr.Item("datLetterReturned_08")) Then
                    DTPLetterReturned_CY2008.Text = OracleDate
                    DTPLetterReturned_CY2008.Checked = False
                Else
                    DTPLetterReturned_CY2008.Text = dr.Item("datLetterReturned_08")
                    DTPLetterReturned_CY2008.Checked = True
                End If
                If IsDBNull(dr.Item("strAddressUnknown_08")) Then
                    rdbAddressUnknownYes_CY2008.Checked = False
                    rdbAddressUnknownNo_CY2008.Checked = False
                Else
                    If dr.Item("strAddressUnknown_08") = "True" Then
                        rdbAddressUnknownYes_CY2008.Checked = True
                    Else
                        rdbAddressUnknownNo_CY2008.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datInitialLetterRemailed_08")) Then
                    DTPLetterRemailed_CY2008.Text = OracleDate
                    DTPLetterRemailed_CY2008.Checked = False
                Else
                    DTPLetterRemailed_CY2008.Text = dr.Item("datInitialLetterRemailed_08")
                    DTPLetterRemailed_CY2008.Checked = True
                End If

                If IsDBNull(dr.Item("strFacilityResponded_08")) Then
                    rdbDataCorrectYes_CY2008.Checked = False
                    rdbDataCorrectNo_CY2008.Checked = False
                Else
                    If dr.Item("strFacilityResponded_08") = "True" Then
                        rdbDataCorrectYes_CY2008.Checked = True
                    Else
                        rdbDataCorrectNo_CY2008.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("STRfACILITYbANKRUPT_08")) Then
                    rdbBankruptcyYes_CY2008.Checked = False
                    rdbBankruptcyNo_CY2008.Checked = False
                Else
                    If dr.Item("STRfACILITYbANKRUPT_08") = True Then
                        rdbBankruptcyYes_CY2008.Checked = True
                    Else
                        rdbBankruptcyNo_CY2008.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strUnableToContact_08")) Then
                    rdbUnabletoContactYes_CY2008.Checked = False
                    rdbUnabletoContactNo_CY2008.Checked = False
                Else
                    If dr.Item("strUnableToContact_08") = True Then
                        rdbUnabletoContactYes_CY2008.Checked = True
                    Else
                        rdbUnabletoContactNo_CY2008.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datNOVLetterSent_08")) Then
                    DTPNOVSent_CY2008.Text = OracleDate
                    DTPNOVSent_CY2008.Checked = False
                Else
                    DTPNOVSent_CY2008.Text = dr.Item("datNOVLetterSent_08")
                    DTPNOVSent_CY2008.Checked = True
                End If
                If IsDBNull(dr.Item("datCOLetterSent_08")) Then
                    DTPCOSent_CY2008.Text = OracleDate
                    DTPCOSent_CY2008.Checked = False
                Else
                    DTPCOSent_CY2008.Text = dr.Item("datCOLetterSent_08")
                    DTPCOSent_CY2008.Checked = True
                End If
                If IsDBNull(dr.Item("datAOLetterSent_08")) Then
                    DTPAOSent_CY2008.Text = OracleDate
                    DTPAOSent_CY2008.Checked = False
                Else
                    DTPAOSent_CY2008.Text = dr.Item("datAOLetterSent_08")
                    DTPAOSent_CY2008.Checked = True
                End If

                If IsDBNull(dr.Item("datFacilityPaidFees_08")) Then
                    DTPFeesPaid_CY2008.Text = OracleDate
                    DTPFeesPaid_CY2008.Checked = False
                Else
                    DTPFeesPaid_CY2008.Text = dr.Item("datFacilityPaidFees_08")
                    DTPFeesPaid_CY2008.Checked = True
                End If
                If IsDBNull(dr.Item("datClosedOutFeeAudit_08")) Then
                    DTPCloseOut_CY2008.Text = OracleDate
                    DTPCloseOut_CY2008.Checked = False
                Else
                    DTPCloseOut_CY2008.Text = dr.Item("datClosedOutFeeAudit_08")
                    DTPCloseOut_CY2008.Checked = True
                End If
                If IsDBNull(dr.Item("numStaffAssigned_08")) Then
                    Staff_08 = ""
                    lblStaffAssigned_08.Text = "Staff Last Modified: - "
                Else
                    Staff_08 = dr.Item("numStaffAssigned_08")
                    ' lblStaffAssigned_08.Text = "Staff Assigned: " & dr.Item("numStaffAssigned_08")
                End If
                If IsDBNull(dr.Item("datLastModified_08")) Then
                    lblLastModified_08.Text = "Last Modified: - "
                Else
                    lblLastModified_08.Text = "Last Modified: " & Format(dr.Item("datLastModified_08"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("numManagerSignOff_08")) Then
                    Manager_08 = ""
                    lblManagerSignOff_08.Text = "Manager Sign Off: - "
                Else
                    Manager_08 = dr.Item("numManagerSignOff_08")
                    ' lblManagerSignOff_08.Text = "Manager Sign Off: " & dr.Item("numManagerSignOff")
                End If
                If IsDBNull(dr.Item("datManagerSignOff_08")) Then
                    lblSignOffDat_08.Text = "Last Modified: - "
                Else
                    lblSignOffDat_08.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_08"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("strComments_08")) Then
                    txtComments_CY2008.Clear()
                Else
                    txtComments_CY2008.Text = dr.Item("strComments_08")
                End If
                If IsDBNull(dr.Item("datInitialLetterMailed_07")) Then
                    DTPInitialLetter_2007.Text = OracleDate
                    DTPInitialLetter_2007.Checked = False
                Else
                    DTPInitialLetter_2007.Text = dr.Item("datInitialLetterMailed_07")
                    DTPInitialLetter_2007.Checked = True
                End If
                If IsDBNull(dr.Item("datLetterReturned_07")) Then
                    DTPLetterReturned_CY2007.Text = OracleDate
                    DTPLetterReturned_CY2007.Checked = False
                Else
                    DTPLetterReturned_CY2007.Text = dr.Item("datLetterReturned_07")
                    DTPLetterReturned_CY2007.Checked = True
                End If
                If IsDBNull(dr.Item("strAddressUnknown_07")) Then
                    rdbAddressUnknownYes_CY2007.Checked = False
                    rdbAddressUnknownNo_CY2007.Checked = False
                Else
                    If dr.Item("strAddressUnknown_07") = "True" Then
                        rdbAddressUnknownYes_CY2007.Checked = True
                    Else
                        rdbAddressUnknownNo_CY2007.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datInitialLetterRemailed_07")) Then
                    DTPLetterRemailed_CY2007.Text = OracleDate
                    DTPLetterRemailed_CY2007.Checked = False
                Else
                    DTPLetterRemailed_CY2007.Text = dr.Item("datInitialLetterRemailed_07")
                    DTPLetterRemailed_CY2007.Checked = True
                End If

                If IsDBNull(dr.Item("strFacilityResponded_07")) Then
                    rdbDataCorrectYes_CY2007.Checked = False
                    rdbDataCorrectNo_CY2007.Checked = False
                Else
                    If dr.Item("strFacilityResponded_07") = "True" Then
                        rdbDataCorrectYes_CY2007.Checked = True
                    Else
                        rdbDataCorrectNo_CY2007.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("STRfACILITYbANKRUPT_07")) Then
                    rdbBankruptcyYes_CY2007.Checked = False
                    rdbBankruptcyNo_CY2007.Checked = False
                Else
                    If dr.Item("STRfACILITYbANKRUPT_07") = True Then
                        rdbBankruptcyYes_CY2007.Checked = True
                    Else
                        rdbBankruptcyNo_CY2007.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strUnableToContact_07")) Then
                    rdbUnabletoContactYes_CY2007.Checked = False
                    rdbUnabletoContactNo_CY2007.Checked = False
                Else
                    If dr.Item("strUnableToContact_07") = True Then
                        rdbUnabletoContactYes_CY2007.Checked = True
                    Else
                        rdbUnabletoContactNo_CY2007.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datNOVLetterSent_07")) Then
                    DTPNOVSent_CY2007.Text = OracleDate
                    DTPNOVSent_CY2007.Checked = False
                Else
                    DTPNOVSent_CY2007.Text = dr.Item("datNOVLetterSent_07")
                    DTPNOVSent_CY2007.Checked = True
                End If
                If IsDBNull(dr.Item("datCOLetterSent_07")) Then
                    DTPCOSent_CY2007.Text = OracleDate
                    DTPCOSent_CY2007.Checked = False
                Else
                    DTPCOSent_CY2007.Text = dr.Item("datCOLetterSent_07")
                    DTPCOSent_CY2007.Checked = True
                End If
                If IsDBNull(dr.Item("datAOLetterSent_07")) Then
                    DTPAOSent_CY2007.Text = OracleDate
                    DTPAOSent_CY2007.Checked = False
                Else
                    DTPAOSent_CY2007.Text = dr.Item("datAOLetterSent_07")
                    DTPAOSent_CY2007.Checked = True
                End If

                If IsDBNull(dr.Item("datFacilityPaidFees_07")) Then
                    DTPFeesPaid_CY2007.Text = OracleDate
                    DTPFeesPaid_CY2007.Checked = False
                Else
                    DTPFeesPaid_CY2007.Text = dr.Item("datFacilityPaidFees_07")
                    DTPFeesPaid_CY2007.Checked = True
                End If
                If IsDBNull(dr.Item("datClosedOutFeeAudit_07")) Then
                    DTPCloseOut_CY2007.Text = OracleDate
                    DTPCloseOut_CY2007.Checked = False
                Else
                    DTPCloseOut_CY2007.Text = dr.Item("datClosedOutFeeAudit_07")
                    DTPCloseOut_CY2007.Checked = True
                End If
                If IsDBNull(dr.Item("numStaffAssigned_07")) Then
                    Staff_07 = ""
                    lblStaffAssigned_07.Text = "Staff Last Modified: - "
                Else
                    Staff_07 = dr.Item("numStaffAssigned_07")
                    ' lblStaffAssigned_07.Text = "Staff Assigned: " & dr.Item("numStaffAssigned_07")
                End If
                If IsDBNull(dr.Item("datLastModified_07")) Then
                    lblLastModified_07.Text = "Last Modified: - "
                Else
                    lblLastModified_07.Text = "Last Modified: " & Format(dr.Item("datLastModified_07"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("numManagerSignOff_07")) Then
                    Manager_07 = ""
                    lblManagerSignOff_07.Text = "Manager Sign Off: - "
                Else
                    Manager_07 = dr.Item("numManagerSignOff_07")
                    ' lblManagerSignOff_07.Text = "Manager Sign Off: " & dr.Item("numManagerSignOff")
                End If
                If IsDBNull(dr.Item("datManagerSignOff_07")) Then
                    lblSignOffDat_07.Text = "Last Modified: - "
                Else
                    lblSignOffDat_07.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_07"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("strComments_07")) Then
                    txtComments_CY2007.Clear()
                Else
                    txtComments_CY2007.Text = dr.Item("strComments_07")
                End If
                If IsDBNull(dr.Item("datInitialLetterMailed_06")) Then
                    DTPInitialLetter_2006.Text = OracleDate
                    DTPInitialLetter_2006.Checked = False
                Else
                    DTPInitialLetter_2006.Text = dr.Item("datInitialLetterMailed_06")
                    DTPInitialLetter_2006.Checked = True
                End If
                If IsDBNull(dr.Item("datLetterReturned_06")) Then
                    DTPLetterReturned_CY2006.Text = OracleDate
                    DTPLetterReturned_CY2006.Checked = False
                Else
                    DTPLetterReturned_CY2006.Text = dr.Item("datLetterReturned_06")
                    DTPLetterReturned_CY2006.Checked = True
                End If
                If IsDBNull(dr.Item("strAddressUnknown_06")) Then
                    rdbAddressUnknownYes_CY2006.Checked = False
                    rdbAddressUnknownNo_CY2006.Checked = False
                Else
                    If dr.Item("strAddressUnknown_06") = "True" Then
                        rdbAddressUnknownYes_CY2006.Checked = True
                    Else
                        rdbAddressUnknownNo_CY2006.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datInitialLetterRemailed_06")) Then
                    DTPLetterRemailed_CY2006.Text = OracleDate
                    DTPLetterRemailed_CY2006.Checked = False
                Else
                    DTPLetterRemailed_CY2006.Text = dr.Item("datInitialLetterRemailed_06")
                    DTPLetterRemailed_CY2006.Checked = True
                End If

                If IsDBNull(dr.Item("strFacilityResponded_06")) Then
                    rdbDataCorrectYes_CY2006.Checked = False
                    rdbDataCorrectNo_CY2006.Checked = False
                Else
                    If dr.Item("strFacilityResponded_06") = "True" Then
                        rdbDataCorrectYes_CY2006.Checked = True
                    Else
                        rdbDataCorrectNo_CY2006.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("STRfACILITYbANKRUPT_06")) Then
                    rdbBankruptcyYes_CY2006.Checked = False
                    rdbBankruptcyNo_CY2006.Checked = False
                Else
                    If dr.Item("STRfACILITYbANKRUPT_06") = True Then
                        rdbBankruptcyYes_CY2006.Checked = True
                    Else
                        rdbBankruptcyNo_CY2006.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strUnableToContact_06")) Then
                    rdbUnabletoContactYes_CY2006.Checked = False
                    rdbUnabletoContactNo_CY2006.Checked = False
                Else
                    If dr.Item("strUnableToContact_06") = True Then
                        rdbUnabletoContactYes_CY2006.Checked = True
                    Else
                        rdbUnabletoContactNo_CY2006.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datNOVLetterSent_06")) Then
                    DTPNOVSent_CY2006.Text = OracleDate
                    DTPNOVSent_CY2006.Checked = False
                Else
                    DTPNOVSent_CY2006.Text = dr.Item("datNOVLetterSent_06")
                    DTPNOVSent_CY2006.Checked = True
                End If
                If IsDBNull(dr.Item("datCOLetterSent_06")) Then
                    DTPCOSent_CY2006.Text = OracleDate
                    DTPCOSent_CY2006.Checked = False
                Else
                    DTPCOSent_CY2006.Text = dr.Item("datCOLetterSent_06")
                    DTPCOSent_CY2006.Checked = True
                End If
                If IsDBNull(dr.Item("datAOLetterSent_06")) Then
                    DTPAOSent_CY2006.Text = OracleDate
                    DTPAOSent_CY2006.Checked = False
                Else
                    DTPAOSent_CY2006.Text = dr.Item("datAOLetterSent_06")
                    DTPAOSent_CY2006.Checked = True
                End If

                If IsDBNull(dr.Item("datFacilityPaidFees_06")) Then
                    DTPFeesPaid_CY2006.Text = OracleDate
                    DTPFeesPaid_CY2006.Checked = False
                Else
                    DTPFeesPaid_CY2006.Text = dr.Item("datFacilityPaidFees_06")
                    DTPFeesPaid_CY2006.Checked = True
                End If
                If IsDBNull(dr.Item("datClosedOutFeeAudit_06")) Then
                    DTPCloseOut_CY2006.Text = OracleDate
                    DTPCloseOut_CY2006.Checked = False
                Else
                    DTPCloseOut_CY2006.Text = dr.Item("datClosedOutFeeAudit_06")
                    DTPCloseOut_CY2006.Checked = True
                End If
                If IsDBNull(dr.Item("numStaffAssigned_06")) Then
                    Staff_06 = ""
                    lblStaffAssigned_06.Text = "Staff Last Modified: - "
                Else
                    Staff_06 = dr.Item("numStaffAssigned_06")
                    ' lblStaffAssigned_06.Text = "Staff Assigned: " & dr.Item("numStaffAssigned_06")
                End If
                If IsDBNull(dr.Item("datLastModified_06")) Then
                    lblLastModified_06.Text = "Last Modified: - "
                Else
                    lblLastModified_06.Text = "Last Modified: " & Format(dr.Item("datLastModified_06"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("numManagerSignOff_06")) Then
                    Manager_06 = ""
                    lblManagerSignOff_06.Text = "Manager Sign Off: - "
                Else
                    Manager_06 = dr.Item("numManagerSignOff_06")
                    ' lblManagerSignOff_06.Text = "Manager Sign Off: " & dr.Item("numManagerSignOff")
                End If
                If IsDBNull(dr.Item("datManagerSignOff_06")) Then
                    lblSignOffDat_06.Text = "Last Modified: - "
                Else
                    lblSignOffDat_06.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_06"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("strComments_06")) Then
                    txtComments_CY2006.Clear()
                Else
                    txtComments_CY2006.Text = dr.Item("strComments_06")
                End If
                If IsDBNull(dr.Item("datInitialLetterMailed_05")) Then
                    DTPInitialLetter_2005.Text = OracleDate
                    DTPInitialLetter_2005.Checked = False
                Else
                    DTPInitialLetter_2005.Text = dr.Item("datInitialLetterMailed_05")
                    DTPInitialLetter_2005.Checked = True
                End If
                If IsDBNull(dr.Item("datLetterReturned_05")) Then
                    DTPLetterReturned_CY2005.Text = OracleDate
                    DTPLetterReturned_CY2005.Checked = False
                Else
                    DTPLetterReturned_CY2005.Text = dr.Item("datLetterReturned_05")
                    DTPLetterReturned_CY2005.Checked = True
                End If
                If IsDBNull(dr.Item("strAddressUnknown_05")) Then
                    rdbAddressUnknownYes_CY2005.Checked = False
                    rdbAddressUnknownNo_CY2005.Checked = False
                Else
                    If dr.Item("strAddressUnknown_05") = "True" Then
                        rdbAddressUnknownYes_CY2005.Checked = True
                    Else
                        rdbAddressUnknownNo_CY2005.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datInitialLetterRemailed_05")) Then
                    DTPLetterRemailed_CY2005.Text = OracleDate
                    DTPLetterRemailed_CY2005.Checked = False
                Else
                    DTPLetterRemailed_CY2005.Text = dr.Item("datInitialLetterRemailed_05")
                    DTPLetterRemailed_CY2005.Checked = True
                End If

                If IsDBNull(dr.Item("strFacilityResponded_05")) Then
                    rdbDataCorrectYes_CY2005.Checked = False
                    rdbDataCorrectNo_CY2005.Checked = False
                Else
                    If dr.Item("strFacilityResponded_05") = "True" Then
                        rdbDataCorrectYes_CY2005.Checked = True
                    Else
                        rdbDataCorrectNo_CY2005.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("STRfACILITYbANKRUPT_05")) Then
                    rdbBankruptcyYes_CY2005.Checked = False
                    rdbBankruptcyNo_CY2005.Checked = False
                Else
                    If dr.Item("STRfACILITYbANKRUPT_05") = True Then
                        rdbBankruptcyYes_CY2005.Checked = True
                    Else
                        rdbBankruptcyNo_CY2005.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strUnableToContact_05")) Then
                    rdbUnabletoContactYes_CY2005.Checked = False
                    rdbUnabletoContactNo_CY2005.Checked = False
                Else
                    If dr.Item("strUnableToContact_05") = True Then
                        rdbUnabletoContactYes_CY2005.Checked = True
                    Else
                        rdbUnabletoContactNo_CY2005.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datNOVLetterSent_05")) Then
                    DTPNOVSent_CY2005.Text = OracleDate
                    DTPNOVSent_CY2005.Checked = False
                Else
                    DTPNOVSent_CY2005.Text = dr.Item("datNOVLetterSent_05")
                    DTPNOVSent_CY2005.Checked = True
                End If
                If IsDBNull(dr.Item("datCOLetterSent_05")) Then
                    DTPCOSent_CY2005.Text = OracleDate
                    DTPCOSent_CY2005.Checked = False
                Else
                    DTPCOSent_CY2005.Text = dr.Item("datCOLetterSent_05")
                    DTPCOSent_CY2005.Checked = True
                End If
                If IsDBNull(dr.Item("datAOLetterSent_05")) Then
                    DTPAOSent_CY2005.Text = OracleDate
                    DTPAOSent_CY2005.Checked = False
                Else
                    DTPAOSent_CY2005.Text = dr.Item("datAOLetterSent_05")
                    DTPAOSent_CY2005.Checked = True
                End If

                If IsDBNull(dr.Item("datFacilityPaidFees_05")) Then
                    DTPFeesPaid_CY2005.Text = OracleDate
                    DTPFeesPaid_CY2005.Checked = False
                Else
                    DTPFeesPaid_CY2005.Text = dr.Item("datFacilityPaidFees_05")
                    DTPFeesPaid_CY2005.Checked = True
                End If
                If IsDBNull(dr.Item("datClosedOutFeeAudit_05")) Then
                    DTPCloseOut_CY2005.Text = OracleDate
                    DTPCloseOut_CY2005.Checked = False
                Else
                    DTPCloseOut_CY2005.Text = dr.Item("datClosedOutFeeAudit_05")
                    DTPCloseOut_CY2005.Checked = True
                End If
                If IsDBNull(dr.Item("numStaffAssigned_05")) Then
                    Staff_05 = ""
                    lblStaffAssigned_05.Text = "Staff Last Modified: - "
                Else
                    Staff_05 = dr.Item("numStaffAssigned_05")
                    ' lblStaffAssigned_05.Text = "Staff Assigned: " & dr.Item("numStaffAssigned_05")
                End If
                If IsDBNull(dr.Item("datLastModified_05")) Then
                    lblLastModified_05.Text = "Last Modified: - "
                Else
                    lblLastModified_05.Text = "Last Modified: " & Format(dr.Item("datLastModified_05"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("numManagerSignOff_05")) Then
                    Manager_05 = ""
                    lblManagerSignOff_05.Text = "Manager Sign Off: - "
                Else
                    Manager_05 = dr.Item("numManagerSignOff_05")
                    ' lblManagerSignOff_05.Text = "Manager Sign Off: " & dr.Item("numManagerSignOff")
                End If
                If IsDBNull(dr.Item("datManagerSignOff_05")) Then
                    lblSignOffDat_05.Text = "Last Modified: - "
                Else
                    lblSignOffDat_05.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_05"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("strComments_05")) Then
                    txtComments_CY2005.Clear()
                Else
                    txtComments_CY2005.Text = dr.Item("strComments_05")
                End If
                If IsDBNull(dr.Item("datInitialLetterMailed_04")) Then
                    DTPInitialLetter_2004.Text = OracleDate
                    DTPInitialLetter_2004.Checked = False
                Else
                    DTPInitialLetter_2004.Text = dr.Item("datInitialLetterMailed_04")
                    DTPInitialLetter_2004.Checked = True
                End If
                If IsDBNull(dr.Item("datLetterReturned_04")) Then
                    DTPLetterReturned_CY2004.Text = OracleDate
                    DTPLetterReturned_CY2004.Checked = False
                Else
                    DTPLetterReturned_CY2004.Text = dr.Item("datLetterReturned_04")
                    DTPLetterReturned_CY2004.Checked = True
                End If
                If IsDBNull(dr.Item("strAddressUnknown_04")) Then
                    rdbAddressUnknownYes_CY2004.Checked = False
                    rdbAddressUnknownNo_CY2004.Checked = False
                Else
                    If dr.Item("strAddressUnknown_04") = "True" Then
                        rdbAddressUnknownYes_CY2004.Checked = True
                    Else
                        rdbAddressUnknownNo_CY2004.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datInitialLetterRemailed_04")) Then
                    DTPLetterRemailed_CY2004.Text = OracleDate
                    DTPLetterRemailed_CY2004.Checked = False
                Else
                    DTPLetterRemailed_CY2004.Text = dr.Item("datInitialLetterRemailed_04")
                    DTPLetterRemailed_CY2004.Checked = True
                End If

                If IsDBNull(dr.Item("strFacilityResponded_04")) Then
                    rdbDataCorrectYes_CY2004.Checked = False
                    rdbDataCorrectNo_CY2004.Checked = False
                Else
                    If dr.Item("strFacilityResponded_04") = "True" Then
                        rdbDataCorrectYes_CY2004.Checked = True
                    Else
                        rdbDataCorrectNo_CY2004.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("STRfACILITYbANKRUPT_04")) Then
                    rdbBankruptcyYes_CY2004.Checked = False
                    rdbBankruptcyNo_CY2004.Checked = False
                Else
                    If dr.Item("STRfACILITYbANKRUPT_04") = True Then
                        rdbBankruptcyYes_CY2004.Checked = True
                    Else
                        rdbBankruptcyNo_CY2004.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strUnableToContact_04")) Then
                    rdbUnabletoContactYes_CY2004.Checked = False
                    rdbUnabletoContactNo_CY2004.Checked = False
                Else
                    If dr.Item("strUnableToContact_04") = True Then
                        rdbUnabletoContactYes_CY2004.Checked = True
                    Else
                        rdbUnabletoContactNo_CY2004.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datNOVLetterSent_04")) Then
                    DTPNOVSent_CY2004.Text = OracleDate
                    DTPNOVSent_CY2004.Checked = False
                Else
                    DTPNOVSent_CY2004.Text = dr.Item("datNOVLetterSent_04")
                    DTPNOVSent_CY2004.Checked = True
                End If
                If IsDBNull(dr.Item("datCOLetterSent_04")) Then
                    DTPCOSent_CY2004.Text = OracleDate
                    DTPCOSent_CY2004.Checked = False
                Else
                    DTPCOSent_CY2004.Text = dr.Item("datCOLetterSent_04")
                    DTPCOSent_CY2004.Checked = True
                End If
                If IsDBNull(dr.Item("datAOLetterSent_04")) Then
                    DTPAOSent_CY2004.Text = OracleDate
                    DTPAOSent_CY2004.Checked = False
                Else
                    DTPAOSent_CY2004.Text = dr.Item("datAOLetterSent_04")
                    DTPAOSent_CY2004.Checked = True
                End If

                If IsDBNull(dr.Item("datFacilityPaidFees_04")) Then
                    DTPFeesPaid_CY2004.Text = OracleDate
                    DTPFeesPaid_CY2004.Checked = False
                Else
                    DTPFeesPaid_CY2004.Text = dr.Item("datFacilityPaidFees_04")
                    DTPFeesPaid_CY2004.Checked = True
                End If
                If IsDBNull(dr.Item("datClosedOutFeeAudit_04")) Then
                    DTPCloseOut_CY2004.Text = OracleDate
                    DTPCloseOut_CY2004.Checked = False
                Else
                    DTPCloseOut_CY2004.Text = dr.Item("datClosedOutFeeAudit_04")
                    DTPCloseOut_CY2004.Checked = True
                End If
                If IsDBNull(dr.Item("numStaffAssigned_04")) Then
                    Staff_04 = ""
                    lblStaffAssigned_04.Text = "Staff Last Modified: - "
                Else
                    Staff_04 = dr.Item("numStaffAssigned_04")
                    ' lblStaffAssigned_04.Text = "Staff Assigned: " & dr.Item("numStaffAssigned_04")
                End If
                If IsDBNull(dr.Item("datLastModified_04")) Then
                    lblLastModified_04.Text = "Last Modified: - "
                Else
                    lblLastModified_04.Text = "Last Modified: " & Format(dr.Item("datLastModified_04"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("numManagerSignOff_04")) Then
                    Manager_04 = ""
                    lblManagerSignOff_04.Text = "Manager Sign Off: - "
                Else
                    Manager_04 = dr.Item("numManagerSignOff_04")
                    ' lblManagerSignOff_04.Text = "Manager Sign Off: " & dr.Item("numManagerSignOff")
                End If
                If IsDBNull(dr.Item("datManagerSignOff_04")) Then
                    lblSignOffDat_04.Text = "Last Modified: - "
                Else
                    lblSignOffDat_04.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_04"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("strComments_04")) Then
                    txtComments_CY2004.Clear()
                Else
                    txtComments_CY2004.Text = dr.Item("strComments_04")
                End If
                If IsDBNull(dr.Item("datInitialLetterMailed_03")) Then
                    DTPInitialLetter_2003.Text = OracleDate
                    DTPInitialLetter_2003.Checked = False
                Else
                    DTPInitialLetter_2003.Text = dr.Item("datInitialLetterMailed_03")
                    DTPInitialLetter_2003.Checked = True
                End If
                If IsDBNull(dr.Item("datLetterReturned_03")) Then
                    DTPLetterReturned_CY2003.Text = OracleDate
                    DTPLetterReturned_CY2003.Checked = False
                Else
                    DTPLetterReturned_CY2003.Text = dr.Item("datLetterReturned_03")
                    DTPLetterReturned_CY2003.Checked = True
                End If
                If IsDBNull(dr.Item("strAddressUnknown_03")) Then
                    rdbAddressUnknownYes_CY2003.Checked = False
                    rdbAddressUnknownNo_CY2003.Checked = False
                Else
                    If dr.Item("strAddressUnknown_03") = "True" Then
                        rdbAddressUnknownYes_CY2003.Checked = True
                    Else
                        rdbAddressUnknownNo_CY2003.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datInitialLetterRemailed_03")) Then
                    DTPLetterRemailed_CY2003.Text = OracleDate
                    DTPLetterRemailed_CY2003.Checked = False
                Else
                    DTPLetterRemailed_CY2003.Text = dr.Item("datInitialLetterRemailed_03")
                    DTPLetterRemailed_CY2003.Checked = True
                End If

                If IsDBNull(dr.Item("strFacilityResponded_03")) Then
                    rdbDataCorrectYes_CY2003.Checked = False
                    rdbDataCorrectNo_CY2003.Checked = False
                Else
                    If dr.Item("strFacilityResponded_03") = "True" Then
                        rdbDataCorrectYes_CY2003.Checked = True
                    Else
                        rdbDataCorrectNo_CY2003.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("STRfACILITYbANKRUPT_03")) Then
                    rdbBankruptcyYes_CY2003.Checked = False
                    rdbBankruptcyNo_CY2003.Checked = False
                Else
                    If dr.Item("STRfACILITYbANKRUPT_03") = True Then
                        rdbBankruptcyYes_CY2003.Checked = True
                    Else
                        rdbBankruptcyNo_CY2003.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strUnableToContact_03")) Then
                    rdbUnabletoContactYes_CY2003.Checked = False
                    rdbUnabletoContactNo_CY2003.Checked = False
                Else
                    If dr.Item("strUnableToContact_03") = True Then
                        rdbUnabletoContactYes_CY2003.Checked = True
                    Else
                        rdbUnabletoContactNo_CY2003.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datNOVLetterSent_03")) Then
                    DTPNOVSent_CY2003.Text = OracleDate
                    DTPNOVSent_CY2003.Checked = False
                Else
                    DTPNOVSent_CY2003.Text = dr.Item("datNOVLetterSent_03")
                    DTPNOVSent_CY2003.Checked = True
                End If
                If IsDBNull(dr.Item("datCOLetterSent_03")) Then
                    DTPCOSent_CY2003.Text = OracleDate
                    DTPCOSent_CY2003.Checked = False
                Else
                    DTPCOSent_CY2003.Text = dr.Item("datCOLetterSent_03")
                    DTPCOSent_CY2003.Checked = True
                End If
                If IsDBNull(dr.Item("datAOLetterSent_03")) Then
                    DTPAOSent_CY2003.Text = OracleDate
                    DTPAOSent_CY2003.Checked = False
                Else
                    DTPAOSent_CY2003.Text = dr.Item("datAOLetterSent_03")
                    DTPAOSent_CY2003.Checked = True
                End If

                If IsDBNull(dr.Item("datFacilityPaidFees_03")) Then
                    DTPFeesPaid_CY2003.Text = OracleDate
                    DTPFeesPaid_CY2003.Checked = False
                Else
                    DTPFeesPaid_CY2003.Text = dr.Item("datFacilityPaidFees_03")
                    DTPFeesPaid_CY2003.Checked = True
                End If
                If IsDBNull(dr.Item("datClosedOutFeeAudit_03")) Then
                    DTPCloseOut_CY2003.Text = OracleDate
                    DTPCloseOut_CY2003.Checked = False
                Else
                    DTPCloseOut_CY2003.Text = dr.Item("datClosedOutFeeAudit_03")
                    DTPCloseOut_CY2003.Checked = True
                End If
                If IsDBNull(dr.Item("numStaffAssigned_03")) Then
                    Staff_03 = ""
                    lblStaffAssigned_03.Text = "Staff Last Modified: - "
                Else
                    Staff_03 = dr.Item("numStaffAssigned_03")
                    ' lblStaffAssigned_03.Text = "Staff Assigned: " & dr.Item("numStaffAssigned_03")
                End If
                If IsDBNull(dr.Item("datLastModified_03")) Then
                    lblLastModified_03.Text = "Last Modified: - "
                Else
                    lblLastModified_03.Text = "Last Modified: " & Format(dr.Item("datLastModified_03"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("numManagerSignOff_03")) Then
                    Manager_03 = ""
                    lblManagerSignOff_03.Text = "Manager Sign Off: - "
                Else
                    Manager_03 = dr.Item("numManagerSignOff_03")
                    ' lblManagerSignOff_03.Text = "Manager Sign Off: " & dr.Item("numManagerSignOff")
                End If
                If IsDBNull(dr.Item("datManagerSignOff_03")) Then
                    lblSignOffDat_03.Text = "Last Modified: - "
                Else
                    lblSignOffDat_03.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_03"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("strComments_03")) Then
                    txtComments_CY2003.Clear()
                Else
                    txtComments_CY2003.Text = dr.Item("strComments_03")
                End If
                If IsDBNull(dr.Item("datInitialLetterMailed_02")) Then
                    DTPInitialLetter_2002.Text = OracleDate
                    DTPInitialLetter_2002.Checked = False
                Else
                    DTPInitialLetter_2002.Text = dr.Item("datInitialLetterMailed_02")
                    DTPInitialLetter_2002.Checked = True
                End If
                If IsDBNull(dr.Item("datLetterReturned_02")) Then
                    DTPLetterReturned_CY2002.Text = OracleDate
                    DTPLetterReturned_CY2002.Checked = False
                Else
                    DTPLetterReturned_CY2002.Text = dr.Item("datLetterReturned_02")
                    DTPLetterReturned_CY2002.Checked = True
                End If
                If IsDBNull(dr.Item("strAddressUnknown_02")) Then
                    rdbAddressUnknownYes_CY2002.Checked = False
                    rdbAddressUnknownNo_CY2002.Checked = False
                Else
                    If dr.Item("strAddressUnknown_02") = "True" Then
                        rdbAddressUnknownYes_CY2002.Checked = True
                    Else
                        rdbAddressUnknownNo_CY2002.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datInitialLetterRemailed_02")) Then
                    DTPLetterRemailed_CY2002.Text = OracleDate
                    DTPLetterRemailed_CY2002.Checked = False
                Else
                    DTPLetterRemailed_CY2002.Text = dr.Item("datInitialLetterRemailed_02")
                    DTPLetterRemailed_CY2002.Checked = True
                End If

                If IsDBNull(dr.Item("strFacilityResponded_02")) Then
                    rdbDataCorrectYes_CY2002.Checked = False
                    rdbDataCorrectNo_CY2002.Checked = False
                Else
                    If dr.Item("strFacilityResponded_02") = "True" Then
                        rdbDataCorrectYes_CY2002.Checked = True
                    Else
                        rdbDataCorrectNo_CY2002.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("STRfACILITYbANKRUPT_02")) Then
                    rdbBankruptcyYes_CY2002.Checked = False
                    rdbBankruptcyNo_CY2002.Checked = False
                Else
                    If dr.Item("STRfACILITYbANKRUPT_02") = True Then
                        rdbBankruptcyYes_CY2002.Checked = True
                    Else
                        rdbBankruptcyNo_CY2002.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strUnableToContact_02")) Then
                    rdbUnabletoContactYes_CY2002.Checked = False
                    rdbUnabletoContactNo_CY2002.Checked = False
                Else
                    If dr.Item("strUnableToContact_02") = True Then
                        rdbUnabletoContactYes_CY2002.Checked = True
                    Else
                        rdbUnabletoContactNo_CY2002.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datNOVLetterSent_02")) Then
                    DTPNOVSent_CY2002.Text = OracleDate
                    DTPNOVSent_CY2002.Checked = False
                Else
                    DTPNOVSent_CY2002.Text = dr.Item("datNOVLetterSent_02")
                    DTPNOVSent_CY2002.Checked = True
                End If
                If IsDBNull(dr.Item("datCOLetterSent_02")) Then
                    DTPCOSent_CY2002.Text = OracleDate
                    DTPCOSent_CY2002.Checked = False
                Else
                    DTPCOSent_CY2002.Text = dr.Item("datCOLetterSent_02")
                    DTPCOSent_CY2002.Checked = True
                End If
                If IsDBNull(dr.Item("datAOLetterSent_02")) Then
                    DTPAOSent_CY2002.Text = OracleDate
                    DTPAOSent_CY2002.Checked = False
                Else
                    DTPAOSent_CY2002.Text = dr.Item("datAOLetterSent_02")
                    DTPAOSent_CY2002.Checked = True
                End If

                If IsDBNull(dr.Item("datFacilityPaidFees_02")) Then
                    DTPFeesPaid_CY2002.Text = OracleDate
                    DTPFeesPaid_CY2002.Checked = False
                Else
                    DTPFeesPaid_CY2002.Text = dr.Item("datFacilityPaidFees_02")
                    DTPFeesPaid_CY2002.Checked = True
                End If
                If IsDBNull(dr.Item("datClosedOutFeeAudit_02")) Then
                    DTPCloseOut_CY2002.Text = OracleDate
                    DTPCloseOut_CY2002.Checked = False
                Else
                    DTPCloseOut_CY2002.Text = dr.Item("datClosedOutFeeAudit_02")
                    DTPCloseOut_CY2002.Checked = True
                End If
                If IsDBNull(dr.Item("numStaffAssigned_02")) Then
                    Staff_02 = ""
                    lblStaffAssigned_02.Text = "Staff Last Modified: - "
                Else
                    Staff_02 = dr.Item("numStaffAssigned_02")
                    ' lblStaffAssigned_02.Text = "Staff Assigned: " & dr.Item("numStaffAssigned_02")
                End If
                If IsDBNull(dr.Item("datLastModified_02")) Then
                    lblLastModified_02.Text = "Last Modified: - "
                Else
                    lblLastModified_02.Text = "Last Modified: " & Format(dr.Item("datLastModified_02"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("numManagerSignOff_02")) Then
                    Manager_02 = ""
                    lblManagerSignOff_02.Text = "Manager Sign Off: - "
                Else
                    Manager_02 = dr.Item("numManagerSignOff_02")
                    ' lblManagerSignOff_02.Text = "Manager Sign Off: " & dr.Item("numManagerSignOff")
                End If
                If IsDBNull(dr.Item("datManagerSignOff_02")) Then
                    lblSignOffDat_02.Text = "Last Modified: - "
                Else
                    lblSignOffDat_02.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_02"), "dd-MMM-yyyy")
                End If

                If IsDBNull(dr.Item("strComments_02")) Then
                    txtComments_CY2002.Clear()
                Else
                    txtComments_CY2002.Text = dr.Item("strComments_02")
                End If

                If IsDBNull(dr.Item("strComments")) Then
                    txtAuditComments.Clear()
                Else
                    txtAuditComments.Text = dr.Item("strComments")
                End If
            End While
            dr.Close()

            If Staff_08 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Staff_08 & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblStaffAssigned_08.Text = "Staff Last Modified: - "
                    Else
                        lblStaffAssigned_08.Text = "Staff Last Modified: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Manager_08 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Manager_08 & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_08.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_08.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Staff_07 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Staff_07 & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblStaffAssigned_07.Text = "Staff Last Modified: - "
                    Else
                        lblStaffAssigned_07.Text = "Staff Last Modified: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Manager_07 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Manager_07 & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_07.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_07.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Staff_06 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Staff_06 & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblStaffAssigned_06.Text = "Staff Last Modified: - "
                    Else
                        lblStaffAssigned_06.Text = "Staff Last Modified: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Manager_06 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Manager_06 & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_06.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_06.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Staff_05 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Staff_05 & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblStaffAssigned_05.Text = "Staff Last Modified: - "
                    Else
                        lblStaffAssigned_05.Text = "Staff Last Modified: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Manager_05 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Manager_05 & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_05.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_05.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Staff_04 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Staff_04 & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblStaffAssigned_04.Text = "Staff Last Modified: - "
                    Else
                        lblStaffAssigned_04.Text = "Staff Last Modified: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Manager_04 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Manager_04 & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_04.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_04.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Staff_03 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Staff_03 & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblStaffAssigned_03.Text = "Staff Last Modified: - "
                    Else
                        lblStaffAssigned_03.Text = "Staff Last Modified: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Manager_03 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Manager_03 & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_03.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_03.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Staff_02 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Staff_02 & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblStaffAssigned_02.Text = "Staff Last Modified: - "
                    Else
                        lblStaffAssigned_02.Text = "Staff Last Modified: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If
            If Manager_02 <> "" Then
                SQL = "Select " & _
                "(strLastname|| ', ' ||strFirstName) as Staff " & _
                "from " & connNameSpace & ".EPDUserProfiles " & _
                "where numUserID = '" & Manager_02 & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_02.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_02.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                End While
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(mtbAIRSNumber.Text & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchForData_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchForData.Enter
        'Try
        '    If mtbAIRSNumber.Text <> "" Then
        '        lblFacilityNameTop.Text = "Facility Name: BAD AIRS #"
        '        ResetForm()

        '        SQL = "Select " & _
        '        "strFacilityName " & _
        '        "from " & connNameSpace & ".APBFacilityInformation " & _
        '        "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
        '        cmd = New OracleCommand(SQL, conn)
        '        If conn.State = ConnectionState.Closed Then
        '            conn.Open()
        '        End If
        '        dr = cmd.ExecuteReader
        '        While dr.Read
        '            If IsDBNull(dr.Item("strFacilityName")) Then
        '                lblFacilityNameTop.Text = "Facility Name: BAD AIRS #"
        '                Exit Sub
        '            Else
        '                lblFacilityNameTop.Text = dr.Item("strFacilityName")
        '            End If
        '        End While
        '        dr.Close()

        '        TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2008)
        '        TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2007)
        '        TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2006)
        '        TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2005)
        '        TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2004)
        '        TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2003)
        '        TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2002)
        '        TCFeeAuditTracking.TabPages.Remove(TP_Tracking_OtherComments)

        '        LoadDataByAIRS()
        '        LoadNonPayer()
        '        LoadAuditData()

        '    End If
        'Catch ex As Exception
        '    ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        'End Try
    End Sub

#Region "Current Info"
    Private Sub txtEditFacilityName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityName.Enter
        Try
            If txtEditFacilityName.Text = "Facility Name" Then
                txtEditFacilityName.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditFacilityAddress_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityAddress.Enter
        Try
            If txtEditFacilityAddress.Text = "Facility Address" Then
                txtEditFacilityAddress.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditFacilityCity_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityCity.Enter
        Try
            If txtEditFacilityCity.Text = "Facility City" Then
                txtEditFacilityCity.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactFirstName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactFirstName.Enter
        Try
            If txtEditContactFirstName.Text = "Contact First Name" Then
                txtEditContactFirstName.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactLastName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactLastName.Enter
        Try
            If txtEditContactLastName.Text = "Contact Last Name" Then
                txtEditContactLastName.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactTitle_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactTitle.Enter
        Try
            If txtEditContactTitle.Text = "Contact Title" Then
                txtEditContactTitle.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactCompany_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactCompany.Enter
        Try
            If txtEditContactCompany.Text = "Contact Company" Then
                txtEditContactCompany.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactAddress_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactAddress.Enter
        Try
            If txtEditContactAddress.Text = "Contact Address" Then
                txtEditContactAddress.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactCity_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactCity.Enter
        Try
            If txtEditContactCity.Text = "Contact City" Then
                txtEditContactCity.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactPhoneNumber_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactPhoneNumber.Enter
        Try
            If txtEditContactPhoneNumber.Text = "Contact Phone Number" Then
                txtEditContactPhoneNumber.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactEmailAddress_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEditContactEmailAddress.Enter
        Try
            txtEditContactEmailAddress.BackColor = Color.White
            If txtEditContactEmailAddress.Text = "Contact Email Address" Then
                txtEditContactEmailAddress.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditFacilityInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditFacilityInfo.Click
        Try

            Dim SQLLine As String = ""

            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtEditFacilityName.Text = "Facility Name" Then
                SQLLine = "   "
            Else
                SQLLine = " strFacilityName_Edit = '" & Replace(txtEditFacilityName.Text, "'", "''") & "' , "
            End If
            If txtEditFacilityAddress.Text = "Facility Address" Then
                '   SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityStreet_Edit = '" & Replace(txtEditFacilityAddress.Text, "'", "''") & "' , "
            End If
            If txtEditFacilityCity.Text = "Facility City" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityCity_Edit = '" & Replace(txtEditFacilityCity.Text, "'", "''") & "' , "
            End If

            SQLLine = SQLLine & " strFacilityZipCode_Edit = '" & Replace(mtbEditZipCode.Text, "'", "''") & "' , "
            SQLLine = SQLLine & " strOperatingStatus_Edit = '" & Replace(cboOperatingStatus.Text, "'", "''") & "' , "
            SQLLine = SQLLine & " strFacilityClass_Edit = '" & Replace(txtEditSourceClass.Text, "'", "''") & "' , "

            If rdbTVYes.Checked = False And rdbTVNo.Checked = False Then
                ' SQLLine = SQLLine
            Else
                If rdbTVYes.Checked = True Then
                    SQLLine = SQLLine & " strTVStatus_Edit = 'Yes' , "
                Else
                    SQLLine = SQLLine & " strTVStatus_Edit = 'No' , "
                End If
            End If
            If rdbNSPSYes.Checked = False And rdbNSPSNo.Checked = False Then
                '    SQLLine = SQLLine
            Else
                If rdbNSPSYes.Checked = True Then
                    SQLLine = SQLLine & " strNSPSStatus_Edit = 'Yes' , "
                Else
                    SQLLine = SQLLine & " strNSPSStatus_Edit = 'No' , "
                End If
            End If

            If SQLLine <> "   " Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                SQLLine & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("Current Facilty Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditContactInfo.Click
        Try
            Dim SQLLine As String = ""

            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtEditContactFirstName.Text = "Contact First Name" Then
                SQLLine = "   "
            Else
                SQLLine = " strContactFirstName_Edit = '" & Replace(txtEditContactFirstName.Text, "'", "''") & "' , "
            End If
            If txtEditContactLastName.Text = "Contact Last Name" Then
                '     SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactLastName_Edit = '" & Replace(txtEditContactLastName.Text, "'", "''") & "' , "
            End If
            If txtEditContactTitle.Text = "Contact Title" Then
                '   SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactTitle_Edit = '" & Replace(txtEditContactTitle.Text, "'", "''") & "' , "
            End If
            If txtEditContactCompany.Text = "Contact Company" Then
                '   SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactCompany_Edit = '" & Replace(txtEditContactCompany.Text, "'", "''") & "' , "
            End If
            If txtEditContactAddress.Text = "Contact Address" Then
                '  SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactAddress_Edit = '" & Replace(txtEditContactAddress.Text, "'", "''") & "' , "
            End If
            If txtEditContactCity.Text = "Contact City" Then
                '    SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactCity_Edit = '" & Replace(txtEditContactCity.Text, "'", "''") & "' , "
            End If
            'If txtEditContactState.Text = "" Then
            '    SQLLine = SQLLine
            'Else
            SQLLine = SQLLine & " strContactState_Edit = '" & Replace(txtEditContactState.Text, "'", "''") & "' , "
            'End If
            'If mtbEditContactZipCode.Text = "" Then
            '    SQLLine = SQLLine
            'Else
            SQLLine = SQLLine & " strContactZipCode_Edit = '" & Replace(mtbEditContactZipCode.Text, "'", "''") & "' , "
            'End If
            If txtEditContactPhoneNumber.Text = "Contact Phone Number" Then
                '   SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactPhoneNumber_Edit = '" & txtEditContactPhoneNumber.Text & "' , "
            End If
            If txtEditContactEmailAddress.Text = "Contact Email Address" Then
                '    SQLLine = SQLLine
            Else
                Dim myInput As String = txtEditContactEmailAddress.Text.Trim()
                Dim pattern As String = "^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$"
                Dim myRegEx As New System.Text.RegularExpressions.Regex(pattern)

                If myRegEx.IsMatch(myInput) Then
                    SQLLine = SQLLine & " strContactEmail_Edit = '" & Replace(txtEditContactEmailAddress.Text, "'", "''") & "' , "
                Else
                    MsgBox("Invalid Email Address")
                    txtEditContactEmailAddress.BackColor = Color.Tomato
                End If
            End If
            If SQLLine <> "   " Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "strContactDescription_Edit = 'Fee_nonresponders tool', " & _
                SQLLine & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("Current Contact Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveCurrentChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCurrentChange.Click
        Try
            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
            "strCurrentInfoComments = '" & Replace(txtCurrentComments.Text, "'", "''") & "' " & _
            " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Comments Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveOwnershipChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveOwnershipChanges.Click
        Try
            Dim OwnershipChange As String = "No"

            If txtNonRespondersID.Text <> "" Then
                If rdbOwnershipChangeYes.Checked = True Then
                    OwnershipChange = "Yes"
                Else
                    OwnershipChange = "No"
                End If

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "strOwnershipChange = '" & OwnershipChange & "', " & _
                "strOwnershipComments = '" & Replace(txtOwnershipChangeComments.Text, "'", "''") & "' " & _
                "where numNonrespondersID = '" & txtNonRespondersID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("Ownership Changes Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveSourceClassificationChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSourceClassificationChanges.Click
        Try
            Dim ClassChange As String = "No"

            If txtNonRespondersID.Text <> "" Then
                If rdbSourceClassChangeYes.Checked = True Then
                    ClassChange = "Yes"
                Else
                    ClassChange = "No"
                End If

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "strSourceClassChange = '" & ClassChange & "', " & _
                "strSourceClassComments = '" & Replace(txtSourceClassificationChangeComment.Text, "'", "''") & "' " & _
                "where numNonrespondersID = '" & txtNonRespondersID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("Sourse Classificaiton Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveComments.Click
        Try

            If txtNonRespondersID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "strComments = '" & Replace(txtComments.Text, "'", "''") & "'  " & _
                "where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("Additional Comments Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFlagNonResponder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlagNonResponder.Click
        Try
            Dim ActiveStatus As String = ""

            If rdbNonResponderInactive.Checked = True Then
                ActiveStatus = "False"
            Else
                ActiveStatus = "True"
            End If

            SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
            "strActive = '" & ActiveStatus & "' " & _
            "where numNonRespondersID = '" & txtNonRespondersID.Text & "' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If txtAuditID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                "datClosedOutFeeAudit_08 = '" & OracleDate & "', " & _
                "datClosedOutFeeAudit_07 = '" & OracleDate & "', " & _
                "datClosedOutFeeAudit_06 = '" & OracleDate & "' " & _
                "where numAuditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            MsgBox("Status Saved", MsgBoxStyle.Information, "Fee Audit Tool")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "CY2008"
    Private Sub txtEditFacilityName_CY2008_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityName_CY2008.Enter
        Try
            If txtEditFacilityName_CY2008.Text = "Facility Name" Then
                txtEditFacilityName_CY2008.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditFacilityAddress_CY2008_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityAddress_CY2008.Enter
        Try
            If txtEditFacilityAddress_CY2008.Text = "Facility Address" Then
                txtEditFacilityAddress_CY2008.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditFacilityCity_CY2008_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityCity_CY2008.Enter
        Try
            If txtEditFacilityCity_CY2008.Text = "Facility City" Then
                txtEditFacilityCity_CY2008.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactFirstName_CY2008_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactFirstName_CY2008.Enter
        Try
            If txtEditContactFirstName_CY2008.Text = "Contact First Name" Then
                txtEditContactFirstName_CY2008.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactLastName_CY2008_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactLastName_CY2008.Enter
        Try
            If txtEditContactLastName_CY2008.Text = "Contact Last Name" Then
                txtEditContactLastName_CY2008.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactCompany_CY2008_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactCompany_CY2008.Enter
        Try
            If txtEditContactCompany_CY2008.Text = "Contact Company" Then
                txtEditContactCompany_CY2008.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactAddress_CY2008_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactAddress_CY2008.Enter
        Try
            If txtEditContactAddress_CY2008.Text = "Contact Address" Then
                txtEditContactAddress_CY2008.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactCity_CY2008_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactCity_CY2008.Enter
        Try
            If txtEditContactCity_CY2008.Text = "Contact City" Then
                txtEditContactCity_CY2008.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditFacilityInfo_CY2008_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditFacilityInfo_CY2008.Click
        Try
            Dim SQLLine As String = ""

            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtEditFacilityName_CY2008.Text = "Facility Name" Or txtEditFacilityName_CY2008.Text = "" Then
                SQLLine = "   "
            Else
                SQLLine = " strFacilityName_08_Edit = '" & Replace(txtEditFacilityName_CY2008.Text, "'", "''") & "' , "
            End If
            If txtEditFacilityAddress_CY2008.Text = "Facility Address" Or txtEditFacilityAddress_CY2008.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityStreet_08_Edit = '" & Replace(txtEditFacilityAddress_CY2008.Text, "'", "''") & "' , "
            End If
            If txtEditFacilityCity_CY2008.Text = "Facility City" Or txtEditFacilityCity_CY2008.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityCity_08_Edit = '" & Replace(txtEditFacilityCity_CY2008.Text, "'", "''") & "' , "
            End If
            If mtbEditZipCode_CY2008.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityZipCode_08_Edit = '" & Replace(mtbEditZipCode_CY2008.Text, "'", "''") & "' , "
            End If

            If cboOperatingStatus_CY2008.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strOperatingStatus_08_Edit = '" & Replace(cboOperatingStatus_CY2008.Text, "'", "''") & "' , "
            End If
            If txtEditSourceClass_CY2008.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strClassification_08_Edit = '" & Replace(txtEditSourceClass_CY2008.Text, "'", "''") & "' , "
            End If
            If rdbTVYes_CY2008.Checked = False And rdbTVNo_CY2008.Checked = False Then
                'SQLLine = SQLLine
            Else
                If rdbTVYes_CY2008.Checked = True Then
                    SQLLine = SQLLine & " strTVStatus_08_Edit = 'Yes' , "
                Else
                    SQLLine = SQLLine & " strTVStatus_08_Edit = 'No' , "
                End If
            End If
            If rdbNSPSYes_CY2008.Checked = False And rdbNSPSNo_CY2008.Checked = False Then
               ' SQLLine = SQLLine
            Else
                If rdbNSPSYes_CY2008.Checked = True Then
                    SQLLine = SQLLine & " strNSPSStatus_08_Edit = 'Yes' , "
                Else
                    SQLLine = SQLLine & " strNSPSStatus_08_Edit = 'No' , "
                End If
            End If

            If SQLLine <> "   " Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                SQLLine & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2008 Facilty Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditContactInfo_CY2008_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditContactInfo_CY2008.Click
        Try
            Dim SQLLine As String = ""

            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtEditContactFirstName_CY2008.Text = "Contact First Name" Or txtEditContactFirstName_CY2008.Text = "" Then
                SQLLine = "   "
            Else
                SQLLine = " strContactFirstName_08_Edit = '" & Replace(txtEditContactFirstName_CY2008.Text, "'", "''") & "' , "
            End If
            If txtEditContactLastName_CY2008.Text = "Contact Last Name" Or txtEditContactLastName_CY2008.Text = "" Then
                '   SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactLastName_08_Edit = '" & Replace(txtEditContactLastName_CY2008.Text, "'", "''") & "' , "
            End If
            If txtEditContactCompany_CY2008.Text = "Contact Company" Or txtEditContactCompany_CY2008.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactCompany_08_Edit = '" & Replace(txtEditContactCompany_CY2008.Text, "'", "''") & "' , "
            End If
            If txtEditContactAddress_CY2008.Text = "Contact Address" Or txtEditContactAddress_CY2008.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactAddress_08_Edit = '" & Replace(txtEditContactAddress_CY2008.Text, "'", "''") & "' , "
            End If
            If txtEditContactCity_CY2008.Text = "Contact City" Or txtEditContactCity_CY2008.Text = "" Then
                '  SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactCity_08_Edit = '" & Replace(txtEditContactCity_CY2008.Text, "'", "''") & "' , "
            End If
            If txtEditContactState_CY2008.Text = "" Then
                '  SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactState_08_Edit = '" & Replace(txtEditContactState_CY2008.Text, "'", "''") & "' , "
            End If
            If mtbEditContactZipCode_CY2008.Text = "" Then
              '  SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactZipCode_08_Edit = '" & Replace(mtbEditContactZipCode_CY2008.Text, "'", "''") & "' , "
            End If

            If SQLLine <> "   " Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                SQLLine & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2008 Contact Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveCurrentChange_CY2008_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCurrentChange_CY2008.Click
        Try
            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtCurrentComments_CY2008.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "str2008Comments = '" & Replace(txtCurrentComments_CY2008.Text, "'", "''") & "' " & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2008 Comments Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "CY2007"
    Private Sub txtEditFacilityName_CY2007_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityName_CY2007.Enter
        Try
            If txtEditFacilityName_CY2007.Text = "Facility Name" Then
                txtEditFacilityName_CY2007.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditFacilityAddress_CY2007_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityAddress_CY2007.Enter
        Try
            If txtEditFacilityAddress_CY2007.Text = "Facility Address" Then
                txtEditFacilityAddress_CY2007.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditFacilityCity_CY2007_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityCity_CY2007.Enter
        Try
            If txtEditFacilityCity_CY2007.Text = "Facility City" Then
                txtEditFacilityCity_CY2007.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactFirstName_CY2007_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactFirstName_CY2007.Enter
        Try
            If txtEditContactFirstName_CY2007.Text = "Contact First Name" Then
                txtEditContactFirstName_CY2007.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactLastName_CY2007_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactLastName_CY2007.Enter
        Try
            If txtEditContactLastName_CY2007.Text = "Contact Last Name" Then
                txtEditContactLastName_CY2007.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactCompany_CY2007_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactCompany_CY2007.Enter
        Try
            If txtEditContactCompany_CY2007.Text = "Contact Company" Then
                txtEditContactCompany_CY2007.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactAddress_CY2007_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactAddress_CY2007.Enter
        Try
            If txtEditContactAddress_CY2007.Text = "Contact Address" Then
                txtEditContactAddress_CY2007.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactCity_CY2007_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactCity_CY2007.Enter
        Try
            If txtEditContactCity_CY2007.Text = "Contact City" Then
                txtEditContactCity_CY2007.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditFacilityInfo_CY2007_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditFacilityInfo_CY2007.Click
        Try
            Dim SQLLine As String = ""

            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtEditFacilityName_CY2007.Text = "Facility Name" Or txtEditFacilityName_CY2007.Text = "" Then
                SQLLine = "   "
            Else
                SQLLine = " strFacilityName_07_Edit = '" & Replace(txtEditFacilityName_CY2007.Text, "'", "''") & "' , "
            End If
            If txtEditFacilityAddress_CY2007.Text = "Facility Address" Or txtEditFacilityAddress_CY2007.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityStreet_07_Edit = '" & Replace(txtEditFacilityAddress_CY2007.Text, "'", "''") & "' , "
            End If
            If txtEditFacilityCity_CY2007.Text = "Facility City" Or txtEditFacilityCity_CY2007.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityCity_07_Edit = '" & Replace(txtEditFacilityCity_CY2007.Text, "'", "''") & "' , "
            End If
            If mtbEditZipCode_CY2007.Text = "" Then
                '  SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityZipCode_07_Edit = '" & Replace(mtbEditZipCode_CY2007.Text, "'", "''") & "' , "
            End If

            If cboOperatingStatus_CY2007.Text = "" Then
                '   SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strOperatingStatus_07_Edit = '" & Replace(cboOperatingStatus_CY2007.Text, "'", "''") & "' , "
            End If
            If txtEditSourceClass_CY2007.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strClassification_07_Edit = '" & Replace(txtEditSourceClass_CY2007.Text, "'", "''") & "' , "
            End If
            If rdbTVYes_CY2007.Checked = False And rdbTVNo_CY2007.Checked = False Then
                ' SQLLine = SQLLine
            Else
                If rdbTVYes_CY2007.Checked = True Then
                    SQLLine = SQLLine & " strTVStatus_07_Edit = 'Yes' , "
                Else
                    SQLLine = SQLLine & " strTVStatus_07_Edit = 'No' , "
                End If
            End If
            If rdbNSPSYes_CY2007.Checked = False And rdbNSPSNo_CY2007.Checked = False Then
                '  SQLLine = SQLLine
            Else
                If rdbNSPSYes_CY2007.Checked = True Then
                    SQLLine = SQLLine & " strNSPSStatus_07_Edit = 'Yes' , "
                Else
                    SQLLine = SQLLine & " strNSPSStatus_07_Edit = 'No' , "
                End If
            End If

            If SQLLine <> "   " Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                SQLLine & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2007 Facilty Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditContactInfo_CY2007_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditContactInfo_CY2007.Click
        Try
            Dim SQLLine As String = ""

            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtEditContactFirstName_CY2007.Text = "Contact First Name" Or txtEditContactFirstName_CY2007.Text = "" Then
                SQLLine = "   "
            Else
                SQLLine = " strContactFirstName_07_Edit = '" & Replace(txtEditContactFirstName_CY2007.Text, "'", "''") & "' , "
            End If
            If txtEditContactLastName_CY2007.Text = "Contact Last Name" Or txtEditContactLastName_CY2007.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactLastName_07_Edit = '" & Replace(txtEditContactLastName_CY2007.Text, "'", "''") & "' , "
            End If
            If txtEditContactCompany_CY2007.Text = "Contact Company" Or txtEditContactCompany_CY2007.Text = "" Then
                '  SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactCompany_07_Edit = '" & Replace(txtEditContactCompany_CY2007.Text, "'", "''") & "' , "
            End If
            If txtEditContactAddress_CY2007.Text = "Contact Address" Or txtEditContactAddress_CY2007.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactAddress_07_Edit = '" & Replace(txtEditContactAddress_CY2007.Text, "'", "''") & "' , "
            End If
            If txtEditContactCity_CY2007.Text = "Contact City" Or txtEditContactCity_CY2007.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactCity_07_Edit = '" & Replace(txtEditContactCity_CY2007.Text, "'", "''") & "' , "
            End If
            If txtEditContactState_CY2007.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactState_07_Edit = '" & Replace(txtEditContactState_CY2007.Text, "'", "''") & "' , "
            End If
            If mtbEditContactZipCode_CY2007.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactZipCode_07_Edit = '" & Replace(mtbEditContactZipCode_CY2007.Text, "'", "''") & "' , "
            End If

            If SQLLine <> "   " Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "strContactDescription_07_Edit = 'Fee_nonresponders tool', " & _
                SQLLine & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2007 Contact Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveCurrentChange_CY2007_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCurrentChange_CY2007.Click
        Try
            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtCurrentComments_CY2007.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "str2007Comments = '" & Replace(txtCurrentComments_CY2007.Text, "'", "''") & "' " & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2007 Comments Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "CY2006"
    Private Sub txtEditFacilityName_CY2006_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityName_CY2006.Enter
        Try
            If txtEditFacilityName_CY2006.Text = "Facility Name" Then
                txtEditFacilityName_CY2006.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditFacilityAddress_CY2006_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityAddress_CY2006.Enter
        Try
            If txtEditFacilityAddress_CY2006.Text = "Facility Address" Then
                txtEditFacilityAddress_CY2006.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditFacilityCity_CY2006_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditFacilityCity_CY2006.Enter
        Try
            If txtEditFacilityCity_CY2006.Text = "Facility City" Then
                txtEditFacilityCity_CY2006.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactFirstName_CY2006_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactFirstName_CY2006.Enter
        Try
            If txtEditContactFirstName_CY2006.Text = "Contact First Name" Then
                txtEditContactFirstName_CY2006.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactLastName_CY2006_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactLastName_CY2006.Enter
        Try
            If txtEditContactLastName_CY2006.Text = "Contact Last Name" Then
                txtEditContactLastName_CY2006.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactCompany_CY2006_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactCompany_CY2006.Enter
        Try
            If txtEditContactCompany_CY2006.Text = "Contact Company" Then
                txtEditContactCompany_CY2006.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactAddress_CY2006_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactAddress_CY2006.Enter
        Try
            If txtEditContactAddress_CY2006.Text = "Contact Address" Then
                txtEditContactAddress_CY2006.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtEditContactCity_CY2006_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditContactCity_CY2006.Enter
        Try
            If txtEditContactCity_CY2006.Text = "Contact City" Then
                txtEditContactCity_CY2006.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditFacilityInfo_CY2006_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditFacilityInfo_CY2006.Click
        Try
            Dim SQLLine As String = ""

            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtEditFacilityName_CY2006.Text = "Facility Name" Or txtEditFacilityName_CY2006.Text = "" Then
                SQLLine = "   "
            Else
                SQLLine = " strFacilityName_06_Edit = '" & Replace(txtEditFacilityName_CY2006.Text, "'", "''") & "' , "
            End If
            If txtEditFacilityAddress_CY2006.Text = "Facility Address" Or txtEditFacilityAddress_CY2006.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityStreet_06_Edit = '" & Replace(txtEditFacilityAddress_CY2006.Text, "'", "''") & "' , "
            End If
            If txtEditFacilityCity_CY2006.Text = "Facility City" Or txtEditFacilityCity_CY2006.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityCity_06_Edit = '" & Replace(txtEditFacilityCity_CY2006.Text, "'", "''") & "' , "
            End If
            If mtbEditZipCode_CY2006.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strFacilityZipCode_06_Edit = '" & Replace(mtbEditZipCode_CY2006.Text, "'", "''") & "' , "
            End If

            If cboOperatingStatus_CY2006.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strOperatingStatus_06_Edit = '" & Replace(cboOperatingStatus_CY2006.Text, "'", "''") & "' , "
            End If
            If txtEditSourceClass_CY2006.Text = "" Then
                'SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strClassification_06_Edit = '" & Replace(txtEditSourceClass_CY2006.Text, "'", "''") & "' , "
            End If
            If rdbTVYes_CY2006.Checked = False And rdbTVNo_CY2006.Checked = False Then
                'SQLLine = SQLLine
            Else
                If rdbTVYes_CY2006.Checked = True Then
                    SQLLine = SQLLine & " strTVStatus_06_Edit = 'Yes' , "
                Else
                    SQLLine = SQLLine & " strTVStatus_06_Edit = 'No' , "
                End If
            End If
            If rdbNSPSYes_CY2006.Checked = False And rdbNSPSNo_CY2006.Checked = False Then
                'SQLLine = SQLLine
            Else
                If rdbNSPSYes_CY2006.Checked = True Then
                    SQLLine = SQLLine & " strNSPSStatus_06_Edit = 'Yes' , "
                Else
                    SQLLine = SQLLine & " strNSPSStatus_06_Edit = 'No' , "
                End If
            End If

            If SQLLine <> "   " Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                SQLLine & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2006 Facilty Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditContactInfo_CY2006_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditContactInfo_CY2006.Click
        Try
            Dim SQLLine As String = ""

            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtEditContactFirstName_CY2006.Text = "Contact First Name" Or txtEditContactFirstName_CY2006.Text = "" Then
                SQLLine = "   "
            Else
                SQLLine = " strContactFirstName_06_Edit = '" & Replace(txtEditContactFirstName_CY2006.Text, "'", "''") & "' , "
            End If
            If txtEditContactLastName_CY2006.Text = "Contact Last Name" Or txtEditContactLastName_CY2006.Text = "" Then
                '  SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactLastName_06_Edit = '" & Replace(txtEditContactLastName_CY2006.Text, "'", "''") & "' , "
            End If
            If txtEditContactCompany_CY2006.Text = "Contact Company" Or txtEditContactCompany_CY2006.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactCompany_06_Edit = '" & Replace(txtEditContactCompany_CY2006.Text, "'", "''") & "' , "
            End If
            If txtEditContactAddress_CY2006.Text = "Contact Address" Or txtEditContactAddress_CY2006.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactAddress_06_Edit = '" & Replace(txtEditContactAddress_CY2006.Text, "'", "''") & "' , "
            End If
            If txtEditContactCity_CY2006.Text = "Contact City" Or txtEditContactCity_CY2006.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactCity_06_Edit = '" & Replace(txtEditContactCity_CY2006.Text, "'", "''") & "' , "
            End If
            If txtEditContactState_CY2006.Text = "" Then
                ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactState_06_Edit = '" & Replace(txtEditContactState_CY2006.Text, "'", "''") & "' , "
            End If
            If mtbEditContactZipCode_CY2006.Text = "" Then
               ' SQLLine = SQLLine
            Else
                SQLLine = SQLLine & " strContactZipCode_06_Edit = '" & Replace(mtbEditContactZipCode_CY2006.Text, "'", "''") & "' , "
            End If

            If SQLLine <> "   " Then
                SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))

                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "strContactDescription_06_Edit = 'Fee_nonresponders tool', " & _
                SQLLine & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2006 Contact Information Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveCurrentChange_CY2006_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCurrentChange_CY2006.Click
        Try
            If txtNonRespondersID.Text = "" Then
                MsgBox("Select a valid Facility before saving.", MsgBoxStyle.Information, "Fee Audit Tool")
                Exit Sub
            End If

            If txtCurrentComments_CY2006.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_NonResponders_2010 set " & _
                "str2006Comments = '" & Replace(txtCurrentComments_CY2006.Text, "'", "''") & "' " & _
                " where numNonRespondersID = '" & txtNonRespondersID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            MsgBox("CY_2006 Comments Saved.", MsgBoxStyle.Information, "Fee Audit Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Audit CY2008"
    Private Sub btnSaveFeeAudit_CY2008_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFeeAudit_CY2008.Click
        Try
            Dim InitialLetter As String = ""
            Dim LetterReturned As String = ""
            Dim AddressUnknown As String = ""
            Dim LetterRemailed As String = ""
            Dim DataCorrected As String = ""
            Dim Backruptcy As String = ""
            Dim UnableToContact As String = ""
            Dim NOVSent As String = ""
            Dim COSent As String = ""
            Dim AOSent As String = ""
            Dim FeesPaid As String = ""
            Dim AmountPaid As String = ""
            Dim ClosedOut As String = ""
            Dim Comments As String = ""

            If mtbAIRSNumber.Text <> "" And lblFacilityNameTop.Text <> "Facility Name: BAD AIRS #" Then
                If DTPInitialLetter_2008.Checked = False Then
                    InitialLetter = ""
                Else
                    InitialLetter = DTPInitialLetter_2008.Text
                End If
                If DTPLetterReturned_CY2008.Checked = False Then
                    LetterReturned = ""
                Else
                    LetterReturned = DTPLetterReturned_CY2008.Text
                End If
                If rdbAddressUnknownYes_CY2008.Checked = False And rdbAddressUnknownNo_CY2008.Checked = False Then
                    AddressUnknown = ""
                Else
                    If rdbAddressUnknownYes_CY2008.Checked = True Then
                        AddressUnknown = "True"
                    Else
                        AddressUnknown = "False"
                    End If
                End If
                If DTPLetterRemailed_CY2008.Checked = False Then
                    LetterRemailed = ""
                Else
                    LetterRemailed = DTPLetterRemailed_CY2008.Text
                End If
                If rdbDataCorrectYes_CY2008.Checked = False And rdbDataCorrectNo_CY2008.Checked = False Then
                    DataCorrected = "False"
                Else
                    If rdbDataCorrectYes_CY2008.Checked = True Then
                        DataCorrected = "True"
                    Else
                        DataCorrected = "False"
                    End If
                End If
                If rdbBankruptcyYes_CY2008.Checked = False And rdbBankruptcyNo_CY2008.Checked = False Then
                    Backruptcy = ""
                Else
                    If rdbBankruptcyYes_CY2008.Checked = True Then
                        Backruptcy = "True"
                    Else
                        Backruptcy = "False"
                    End If
                End If
                If rdbUnabletoContactYes_CY2008.Checked = False And rdbUnabletoContactNo_CY2008.Checked = False Then
                    UnableToContact = ""
                Else
                    If rdbUnabletoContactYes_CY2008.Checked = True Then
                        UnableToContact = "True"
                    Else
                        UnableToContact = "False"
                    End If
                End If
                If DTPNOVSent_CY2008.Checked = False Then
                    NOVSent = ""
                Else
                    NOVSent = DTPNOVSent_CY2008.Text
                End If
                If DTPCOSent_CY2008.Checked = False Then
                    COSent = ""
                Else
                    COSent = DTPCOSent_CY2008.Text
                End If

                If DTPAOSent_CY2008.Checked = False Then
                    AOSent = ""
                Else
                    AOSent = DTPAOSent_CY2008.Text
                End If
                If DTPFeesPaid_CY2008.Checked = False Then
                    FeesPaid = ""
                Else
                    FeesPaid = DTPFeesPaid_CY2008.Text
                End If
                If lblAmountPaid_CY2008.Text = "Fees Paid: -" Then
                    AmountPaid = Replace(lblAmountPaid_CY2008.Text, "Fees Paid: ", "")
                End If

                If DTPCloseOut_CY2008.Checked = False Then
                    ClosedOut = ""
                Else
                    ClosedOut = DTPCloseOut_CY2008.Text
                End If
                If txtComments_CY2008.Text = "" Then
                    Comments = ""
                Else
                    Comments = txtComments_CY2008.Text
                End If

                If txtAuditID.Text = "" Then
                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".FEE_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        MsgBox("WARNING" & vbCrLf & _
                               "I'm sorry but you will have to select the search button above and re-enter your current data", _
                                MsgBoxStyle.Exclamation, "Fee Audit Tool")
                        Exit Sub

                        SQL = "Select " & _
                        "numAuditID " & _
                        "from " & connNameSpace & ".FEE_Audit_2010 " & _
                        "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("numAuditID")) Then
                            Else
                                txtAuditID.Text = dr.Item("numAuditID")
                            End If
                        End While
                        dr.Close()
                    End If
                End If

                If txtAuditID.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                    "datInitialLetterMailed_08 = '" & InitialLetter & "', " & _
                    "datLetterReturned_08 = '" & LetterReturned & "', " & _
                    "strAddressUnknown_08 = '" & AddressUnknown & "', " & _
                    "datInitialLetterRemailed_08 = '" & LetterRemailed & "', " & _
                    "strFacilityResponded_08 = '" & DataCorrected & "', " & _
                    "strFacilityBankrupt_08 = '" & Backruptcy & "', " & _
                    "strUnableToContact_08 = '" & UnableToContact & "', " & _
                    "datNOVLetterSent_08 = '" & NOVSent & "', " & _
                    "datCOLetterSent_08 = '" & COSent & "', " & _
                    "datAOLetterSent_08 = '" & AOSent & "', " & _
                    "datFacilityPaidFees_08 = '" & FeesPaid & "', " & _
                    "strAmountPaid_08 = '" & Replace(AmountPaid, "'", "''") & "',  " & _
                    "datClosedOutFeeAudit_08 = '" & ClosedOut & "', " & _
                    "strComments_08 = '" & Replace(Comments, "'", "''") & "', " & _
                    "numStaffAssigned_08 = '" & UserGCode & "', " & _
                    "datLastModified_08 = '" & OracleDate & "' " & _
                    "where numAuditId = '" & txtAuditID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into " & connNameSpace & ".Fee_Audit_2010 " & _
                    "(numAuditID, strAIRSNumber, " & _
                    "datInitialLetterMailed_08, datLetterReturned_08, " & _
                    "strAddressUnknown_08, datInitialLetterRemailed_08, " & _
                    "strFacilityResponded_08, strUnableToContact_08, " & _
                    "strFacilityBankrupt_08, " & _
                    "datNOVLetterSent_08, datCOLetterSent_08, " & _
                    "datAOLetterSent_08, datFacilityPaidFees_08, " & _
                    "strAmountPaid_08, " & _
                    "datClosedOutFeeAudit_08, strComments_08,  " & _
                    "numStaffAssigned_08, datLastModified_08) " & _
                    "values " & _
                    "((select (max(numAuditID) + 1) from " & connNameSpace & ".Fee_Audit_2010), " & _
                    "'" & mtbAIRSNumber.Text & "', " & _
                    "'" & InitialLetter & "', '" & LetterReturned & "', " & _
                    "'" & AddressUnknown & "', '" & LetterRemailed & "', " & _
                    "'" & DataCorrected & "', '" & UnableToContact & "', " & _
                    "'" & Backruptcy & "', " & _
                    "'" & NOVSent & "', '" & COSent & "', " & _
                    "'" & AOSent & "', '" & FeesPaid & "', " & _
                    "'" & Replace(AmountPaid, "'", "''") & "', " & _
                    "'" & ClosedOut & "', '" & Comments & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".Fee_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("numAuditID")) Then
                            txtAuditID.Clear()
                        Else
                            txtAuditID.Text = dr.Item("numAuditID")
                        End If
                    End While
                    dr.Close()
                End If
                MsgBox("CY_2008 Audit tracking saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                Exit Sub
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnManagerSignOff_CY2008_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManagerSignOff_CY2008.Click
        Try
            If txtAuditID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                "numManagerSignOff_08 = '" & UserGCode & "', " & _
                "datManagerSignOff_08 = '" & OracleDate & "' " & _
                "where numAuditID = '" & txtAuditID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "datManagerSignOff_08 " & _
                "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".Fee_Audit_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.numManagerSignOff_08 = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
                "and numAuditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_08.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_08.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                    If IsDBNull(dr.Item("datManagerSignOff_08")) Then
                        lblSignOffDat_08.Text = "Last Modified: - "
                    Else
                        lblSignOffDat_08.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_08"), "dd-MMM-yyyy")
                    End If
                End While
                dr.Close()

                MsgBox("CY 2008 Manager Sign-Off.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                MsgBox("Audit information must be Saved first, before you can sign-off.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "Audit CY2007"
    Private Sub btnSaveFeeAudit_CY2007_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFeeAudit_CY2007.Click
        Try
            Dim InitialLetter As String = ""
            Dim LetterReturned As String = ""
            Dim AddressUnknown As String = ""
            Dim LetterRemailed As String = ""
            Dim DataCorrected As String = ""
            Dim Backruptcy As String = ""
            Dim UnableToContact As String = ""
            Dim NOVSent As String = ""
            Dim COSent As String = ""
            Dim AOSent As String = ""
            Dim FeesPaid As String = ""
            Dim AmountPaid As String = ""
            Dim ClosedOut As String = ""
            Dim Comments As String = ""

            If mtbAIRSNumber.Text <> "" And lblFacilityNameTop.Text <> "Facility Name: BAD AIRS #" Then
                If DTPInitialLetter_2007.Checked = False Then
                    InitialLetter = ""
                Else
                    InitialLetter = DTPInitialLetter_2007.Text
                End If
                If DTPLetterReturned_CY2007.Checked = False Then
                    LetterReturned = ""
                Else
                    LetterReturned = DTPLetterReturned_CY2007.Text
                End If
                If rdbAddressUnknownYes_CY2007.Checked = False And rdbAddressUnknownNo_CY2007.Checked = False Then
                    AddressUnknown = ""
                Else
                    If rdbAddressUnknownYes_CY2007.Checked = True Then
                        AddressUnknown = "True"
                    Else
                        AddressUnknown = "False"
                    End If
                End If
                If DTPLetterRemailed_CY2007.Checked = False Then
                    LetterRemailed = ""
                Else
                    LetterRemailed = DTPLetterRemailed_CY2007.Text
                End If
                If rdbDataCorrectYes_CY2007.Checked = False And rdbDataCorrectNo_CY2007.Checked = False Then
                    DataCorrected = "False"
                Else
                    If rdbDataCorrectYes_CY2007.Checked = True Then
                        DataCorrected = "True"
                    Else
                        DataCorrected = "False"
                    End If
                End If
                If rdbBankruptcyYes_CY2007.Checked = False And rdbBankruptcyNo_CY2007.Checked = False Then
                    Backruptcy = ""
                Else
                    If rdbBankruptcyYes_CY2007.Checked = True Then
                        Backruptcy = "True"
                    Else
                        Backruptcy = "False"
                    End If
                End If
                If rdbUnabletoContactYes_CY2007.Checked = False And rdbUnabletoContactNo_CY2007.Checked = False Then
                    UnableToContact = ""
                Else
                    If rdbUnabletoContactYes_CY2007.Checked = True Then
                        UnableToContact = "True"
                    Else
                        UnableToContact = "False"
                    End If
                End If
                If DTPNOVSent_CY2007.Checked = False Then
                    NOVSent = ""
                Else
                    NOVSent = DTPNOVSent_CY2007.Text
                End If
                If DTPCOSent_CY2007.Checked = False Then
                    COSent = ""
                Else
                    COSent = DTPCOSent_CY2007.Text
                End If

                If DTPAOSent_CY2007.Checked = False Then
                    AOSent = ""
                Else
                    AOSent = DTPAOSent_CY2007.Text
                End If
                If DTPFeesPaid_CY2007.Checked = False Then
                    FeesPaid = ""
                Else
                    FeesPaid = DTPFeesPaid_CY2007.Text
                End If
                If lblAmountPaid_CY2007.Text = "Fees Paid: -" Then
                    AmountPaid = Replace(lblAmountPaid_CY2007.Text, "Fees Paid: ", "")
                End If
                If DTPCloseOut_CY2007.Checked = False Then
                    ClosedOut = ""
                Else
                    ClosedOut = DTPCloseOut_CY2007.Text
                End If
                If txtComments_CY2007.Text = "" Then
                    Comments = ""
                Else
                    Comments = txtComments_CY2007.Text
                End If

                If txtAuditID.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                    "datInitialLetterMailed_07 = '" & InitialLetter & "', " & _
                    "datLetterReturned_07 = '" & LetterReturned & "', " & _
                    "strAddressUnknown_07 = '" & AddressUnknown & "', " & _
                    "datInitialLetterRemailed_07 = '" & LetterRemailed & "', " & _
                    "strFacilityResponded_07 = '" & DataCorrected & "', " & _
                    "strFacilityBankrupt_07 = '" & Backruptcy & "', " & _
                    "strUnableToContact_07 = '" & UnableToContact & "', " & _
                    "datNOVLetterSent_07 = '" & NOVSent & "', " & _
                    "datCOLetterSent_07 = '" & COSent & "', " & _
                    "datAOLetterSent_07 = '" & AOSent & "', " & _
                    "datFacilityPaidFees_07 = '" & FeesPaid & "', " & _
                    "strAmountPaid_07 = '" & Replace(AmountPaid, "'", "''") & "',  " & _
                    "datClosedOutFeeAudit_07 = '" & ClosedOut & "', " & _
                    "strComments_07 = '" & Replace(Comments, "'", "''") & "', " & _
                    "numStaffAssigned_07 = '" & UserGCode & "', " & _
                    "datLastModified_07 = '" & OracleDate & "' " & _
                    "where numAuditId = '" & txtAuditID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into " & connNameSpace & ".Fee_Audit_2010 " & _
                    "(numAuditID, strAIRSNumber, " & _
                    "datInitialLetterMailed_07, datLetterReturned_07, " & _
                    "strAddressUnknown_07, datInitialLetterRemailed_07, " & _
                    "strFacilityResponded_07, strUnableToContact_07, " & _
                    "strFacilityBankrupt_07, " & _
                    "datNOVLetterSent_07, datCOLetterSent_07, " & _
                    "datAOLetterSent_07, datFacilityPaidFees_07, " & _
                    "strAmountPaid_07, " & _
                    "datClosedOutFeeAudit_07, strComments_07,  " & _
                    "numStaffAssigned_07, datLastModified_07) " & _
                    "values " & _
                    "((select (max(numAuditID) + 1) from " & connNameSpace & ".Fee_Audit_2010), " & _
                    "'" & mtbAIRSNumber.Text & "', " & _
                    "'" & InitialLetter & "', '" & LetterReturned & "', " & _
                    "'" & AddressUnknown & "', '" & LetterRemailed & "', " & _
                    "'" & DataCorrected & "', '" & UnableToContact & "', " & _
                    "'" & Backruptcy & "', " & _
                    "'" & NOVSent & "', '" & COSent & "', " & _
                    "'" & AOSent & "', '" & FeesPaid & "', " & _
                     "'" & Replace(AmountPaid, "'", "''") & "', " & _
                    "'" & ClosedOut & "', '" & Comments & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".Fee_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("numAuditID")) Then
                            txtAuditID.Clear()
                        Else
                            txtAuditID.Text = dr.Item("numAuditID")
                        End If
                    End While
                    dr.Close()
                End If
                MsgBox("CY_2007 Audit tracking saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                Exit Sub
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnManagerSignOff_CY2007_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManagerSignOff_CY2007.Click
        Try
            If txtAuditID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                "numManagerSignOff_07 = '" & UserGCode & "', " & _
                "datManagerSignOff_07 = '" & OracleDate & "' " & _
                "where numAuditID = '" & txtAuditID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "datManagerSignOff_07 " & _
                "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".Fee_Audit_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.numManagerSignOff_07 = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
                "and numAuditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_07.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_07.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                    If IsDBNull(dr.Item("datManagerSignOff_07")) Then
                        lblSignOffDat_07.Text = "Last Modified: - "
                    Else
                        lblSignOffDat_07.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_07"), "dd-MMM-yyyy")
                    End If
                End While
                dr.Close()

                MsgBox("CY 2007 Manager Sign-Off.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                MsgBox("Audit information must be Saved first, before you can sign-off.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "Audit CY2006"
    Private Sub btnSaveFeeAudit_CY2006_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFeeAudit_CY2006.Click
        Try
            Dim InitialLetter As String = ""
            Dim LetterReturned As String = ""
            Dim AddressUnknown As String = ""
            Dim LetterRemailed As String = ""
            Dim DataCorrected As String = ""
            Dim Backruptcy As String = ""
            Dim UnableToContact As String = ""
            Dim NOVSent As String = ""
            Dim COSent As String = ""
            Dim AOSent As String = ""
            Dim FeesPaid As String = ""
            Dim AmountPaid As String = ""
            Dim ClosedOut As String = ""
            Dim Comments As String = ""

            If mtbAIRSNumber.Text <> "" And lblFacilityNameTop.Text <> "Facility Name: BAD AIRS #" Then
                If DTPInitialLetter_2006.Checked = False Then
                    InitialLetter = ""
                Else
                    InitialLetter = DTPInitialLetter_2006.Text
                End If
                If DTPLetterReturned_CY2006.Checked = False Then
                    LetterReturned = ""
                Else
                    LetterReturned = DTPLetterReturned_CY2006.Text
                End If
                If rdbAddressUnknownYes_CY2006.Checked = False And rdbAddressUnknownNo_CY2006.Checked = False Then
                    AddressUnknown = ""
                Else
                    If rdbAddressUnknownYes_CY2006.Checked = True Then
                        AddressUnknown = "True"
                    Else
                        AddressUnknown = "False"
                    End If
                End If
                If DTPLetterRemailed_CY2006.Checked = False Then
                    LetterRemailed = ""
                Else
                    LetterRemailed = DTPLetterRemailed_CY2006.Text
                End If
                If rdbDataCorrectYes_CY2006.Checked = False And rdbDataCorrectNo_CY2006.Checked = False Then
                    DataCorrected = "False"
                Else
                    If rdbDataCorrectYes_CY2006.Checked = True Then
                        DataCorrected = "True"
                    Else
                        DataCorrected = "False"
                    End If
                End If
                If rdbBankruptcyYes_CY2006.Checked = False And rdbBankruptcyNo_CY2006.Checked = False Then
                    Backruptcy = ""
                Else
                    If rdbBankruptcyYes_CY2006.Checked = True Then
                        Backruptcy = "True"
                    Else
                        Backruptcy = "False"
                    End If
                End If
                If rdbUnabletoContactYes_CY2006.Checked = False And rdbUnabletoContactNo_CY2006.Checked = False Then
                    UnableToContact = ""
                Else
                    If rdbUnabletoContactYes_CY2006.Checked = True Then
                        UnableToContact = "True"
                    Else
                        UnableToContact = "False"
                    End If
                End If
                If DTPNOVSent_CY2006.Checked = False Then
                    NOVSent = ""
                Else
                    NOVSent = DTPNOVSent_CY2006.Text
                End If
                If DTPCOSent_CY2006.Checked = False Then
                    COSent = ""
                Else
                    COSent = DTPCOSent_CY2006.Text
                End If

                If DTPAOSent_CY2006.Checked = False Then
                    AOSent = ""
                Else
                    AOSent = DTPAOSent_CY2006.Text
                End If
                If DTPFeesPaid_CY2006.Checked = False Then
                    FeesPaid = ""
                Else
                    FeesPaid = DTPFeesPaid_CY2006.Text
                End If
                If lblAmountPaid_CY2006.Text = "Fees Paid: -" Then
                    AmountPaid = Replace(lblAmountPaid_CY2006.Text, "Fees Paid: ", "")
                End If
                If DTPCloseOut_CY2006.Checked = False Then
                    ClosedOut = ""
                Else
                    ClosedOut = DTPCloseOut_CY2006.Text
                End If
                If txtComments_CY2006.Text = "" Then
                    Comments = ""
                Else
                    Comments = txtComments_CY2006.Text
                End If

                If txtAuditID.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                    "datInitialLetterMailed_06 = '" & InitialLetter & "', " & _
                    "datLetterReturned_06 = '" & LetterReturned & "', " & _
                    "strAddressUnknown_06 = '" & AddressUnknown & "', " & _
                    "datInitialLetterRemailed_06 = '" & LetterRemailed & "', " & _
                    "strFacilityResponded_06 = '" & DataCorrected & "', " & _
                    "strFacilityBankrupt_06 = '" & Backruptcy & "', " & _
                    "strUnableToContact_06 = '" & UnableToContact & "', " & _
                    "datNOVLetterSent_06 = '" & NOVSent & "', " & _
                    "datCOLetterSent_06 = '" & COSent & "', " & _
                    "datAOLetterSent_06 = '" & AOSent & "', " & _
                    "datFacilityPaidFees_06 = '" & FeesPaid & "', " & _
                    "strAmountPaid_06 = '" & Replace(AmountPaid, "'", "''") & "',  " & _
                    "datClosedOutFeeAudit_06 = '" & ClosedOut & "', " & _
                    "strComments_06 = '" & Replace(Comments, "'", "''") & "', " & _
                    "numStaffAssigned_06 = '" & UserGCode & "', " & _
                    "datLastModified_06 = '" & OracleDate & "' " & _
                    "where numAuditId = '" & txtAuditID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into " & connNameSpace & ".Fee_Audit_2010 " & _
                    "(numAuditID, strAIRSNumber, " & _
                    "datInitialLetterMailed_06, datLetterReturned_06, " & _
                    "strAddressUnknown_06, datInitialLetterRemailed_06, " & _
                    "strFacilityResponded_06, strUnableToContact_06, " & _
                    "strFacilityBankrupt_06, " & _
                    "datNOVLetterSent_06, datCOLetterSent_06, " & _
                    "datAOLetterSent_06, datFacilityPaidFees_06, " & _
                    "strAmountPaid_06, " & _
                    "datClosedOutFeeAudit_06, strComments_06,  " & _
                    "numStaffAssigned_06, datLastModified_06) " & _
                    "values " & _
                    "((select (max(numAuditID) + 1) from " & connNameSpace & ".Fee_Audit_2010), " & _
                    "'" & mtbAIRSNumber.Text & "', " & _
                    "'" & InitialLetter & "', '" & LetterReturned & "', " & _
                    "'" & AddressUnknown & "', '" & LetterRemailed & "', " & _
                    "'" & DataCorrected & "', '" & UnableToContact & "', " & _
                    "'" & Backruptcy & "', " & _
                    "'" & NOVSent & "', '" & COSent & "', " & _
                    "'" & AOSent & "', '" & FeesPaid & "', " & _
                     "'" & Replace(AmountPaid, "'", "''") & "', " & _
                    "'" & ClosedOut & "', '" & Comments & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".Fee_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("numAuditID")) Then
                            txtAuditID.Clear()
                        Else
                            txtAuditID.Text = dr.Item("numAuditID")
                        End If
                    End While
                    dr.Close()
                End If
                MsgBox("CY_2006 Audit tracking saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                Exit Sub
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnManagerSignOff_CY2006_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManagerSignOff_CY2006.Click
        Try
            If txtAuditID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                "numManagerSignOff_06 = '" & UserGCode & "', " & _
                "datManagerSignOff_06 = '" & OracleDate & "' " & _
                "where numAuditID = '" & txtAuditID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "datManagerSignOff_06 " & _
                "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".Fee_Audit_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.numManagerSignOff_06 = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
                "and numAuditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_06.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_06.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                    If IsDBNull(dr.Item("datManagerSignOff_06")) Then
                        lblSignOffDat_06.Text = "Last Modified: - "
                    Else
                        lblSignOffDat_06.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_06"), "dd-MMM-yyyy")
                    End If
                End While
                dr.Close()

                MsgBox("CY 2006 Manager Sign-Off.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                MsgBox("Audit information must be Saved first, before you can sign-off.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Audit CY2005"
    Private Sub btnSaveFeeAudit_CY2005_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFeeAudit_CY2005.Click
        Try
            Dim InitialLetter As String = ""
            Dim LetterReturned As String = ""
            Dim AddressUnknown As String = ""
            Dim LetterRemailed As String = ""
            Dim DataCorrected As String = ""
            Dim Backruptcy As String = ""
            Dim UnableToContact As String = ""
            Dim NOVSent As String = ""
            Dim COSent As String = ""
            Dim AOSent As String = ""
            Dim FeesPaid As String = ""
            Dim AmountPaid As String = ""
            Dim ClosedOut As String = ""
            Dim Comments As String = ""

            If mtbAIRSNumber.Text <> "" And lblFacilityNameTop.Text <> "Facility Name: BAD AIRS #" Then
                If DTPInitialLetter_2005.Checked = False Then
                    InitialLetter = ""
                Else
                    InitialLetter = DTPInitialLetter_2005.Text
                End If
                If DTPLetterReturned_CY2005.Checked = False Then
                    LetterReturned = ""
                Else
                    LetterReturned = DTPLetterReturned_CY2005.Text
                End If
                If rdbAddressUnknownYes_CY2005.Checked = False And rdbAddressUnknownNo_CY2005.Checked = False Then
                    AddressUnknown = ""
                Else
                    If rdbAddressUnknownYes_CY2005.Checked = True Then
                        AddressUnknown = "True"
                    Else
                        AddressUnknown = "False"
                    End If
                End If
                If DTPLetterRemailed_CY2005.Checked = False Then
                    LetterRemailed = ""
                Else
                    LetterRemailed = DTPLetterRemailed_CY2005.Text
                End If
                If rdbDataCorrectYes_CY2005.Checked = False And rdbDataCorrectNo_CY2005.Checked = False Then
                    DataCorrected = "False"
                Else
                    If rdbDataCorrectYes_CY2005.Checked = True Then
                        DataCorrected = "True"
                    Else
                        DataCorrected = "False"
                    End If
                End If
                If rdbBankruptcyYes_CY2005.Checked = False And rdbBankruptcyNo_CY2005.Checked = False Then
                    Backruptcy = ""
                Else
                    If rdbBankruptcyYes_CY2005.Checked = True Then
                        Backruptcy = "True"
                    Else
                        Backruptcy = "False"
                    End If
                End If
                If rdbUnabletoContactYes_CY2005.Checked = False And rdbUnabletoContactNo_CY2005.Checked = False Then
                    UnableToContact = ""
                Else
                    If rdbUnabletoContactYes_CY2005.Checked = True Then
                        UnableToContact = "True"
                    Else
                        UnableToContact = "False"
                    End If
                End If
                If DTPNOVSent_CY2005.Checked = False Then
                    NOVSent = ""
                Else
                    NOVSent = DTPNOVSent_CY2005.Text
                End If
                If DTPCOSent_CY2005.Checked = False Then
                    COSent = ""
                Else
                    COSent = DTPCOSent_CY2005.Text
                End If

                If DTPAOSent_CY2005.Checked = False Then
                    AOSent = ""
                Else
                    AOSent = DTPAOSent_CY2005.Text
                End If
                If DTPFeesPaid_CY2005.Checked = False Then
                    FeesPaid = ""
                Else
                    FeesPaid = DTPFeesPaid_CY2005.Text
                End If
                If lblAmountPaid_CY2005.Text = "Fees Paid: -" Then
                    AmountPaid = Replace(lblAmountPaid_CY2005.Text, "Fees Paid: ", "")
                End If
                If DTPCloseOut_CY2005.Checked = False Then
                    ClosedOut = ""
                Else
                    ClosedOut = DTPCloseOut_CY2005.Text
                End If
                If txtComments_CY2005.Text = "" Then
                    Comments = ""
                Else
                    Comments = txtComments_CY2005.Text
                End If

                If txtAuditID.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                    "datInitialLetterMailed_05 = '" & InitialLetter & "', " & _
                    "datLetterReturned_05 = '" & LetterReturned & "', " & _
                    "strAddressUnknown_05 = '" & AddressUnknown & "', " & _
                    "datInitialLetterRemailed_05 = '" & LetterRemailed & "', " & _
                    "strFacilityResponded_05 = '" & DataCorrected & "', " & _
                    "strFacilityBankrupt_05 = '" & Backruptcy & "', " & _
                    "strUnableToContact_05 = '" & UnableToContact & "', " & _
                    "datNOVLetterSent_05 = '" & NOVSent & "', " & _
                    "datCOLetterSent_05 = '" & COSent & "', " & _
                    "datAOLetterSent_05 = '" & AOSent & "', " & _
                    "datFacilityPaidFees_05 = '" & FeesPaid & "', " & _
                    "strAmountPaid_05 = '" & Replace(AmountPaid, "'", "''") & "',  " & _
                    "datClosedOutFeeAudit_05 = '" & ClosedOut & "', " & _
                    "strComments_05 = '" & Replace(Comments, "'", "''") & "', " & _
                    "numStaffAssigned_05 = '" & UserGCode & "', " & _
                    "datLastModified_05 = '" & OracleDate & "' " & _
                    "where numAuditId = '" & txtAuditID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into " & connNameSpace & ".Fee_Audit_2010 " & _
                    "(numAuditID, strAIRSNumber, " & _
                    "datInitialLetterMailed_05, datLetterReturned_05, " & _
                    "strAddressUnknown_05, datInitialLetterRemailed_05, " & _
                    "strFacilityResponded_05, strUnableToContact_05, " & _
                    "strFacilityBankrupt_05, " & _
                    "datNOVLetterSent_05, datCOLetterSent_05, " & _
                    "datAOLetterSent_05, datFacilityPaidFees_05, " & _
                    "strAmountPaid_05, " & _
                    "datClosedOutFeeAudit_05, strComments_05,  " & _
                    "numStaffAssigned_05, datLastModified_05) " & _
                    "values " & _
                    "((select (max(numAuditID) + 1) from " & connNameSpace & ".Fee_Audit_2010), " & _
                    "'" & mtbAIRSNumber.Text & "', " & _
                    "'" & InitialLetter & "', '" & LetterReturned & "', " & _
                    "'" & AddressUnknown & "', '" & LetterRemailed & "', " & _
                    "'" & DataCorrected & "', '" & UnableToContact & "', " & _
                    "'" & Backruptcy & "', " & _
                    "'" & NOVSent & "', '" & COSent & "', " & _
                    "'" & AOSent & "', '" & FeesPaid & "', " & _
                     "'" & Replace(AmountPaid, "'", "''") & "', " & _
                    "'" & ClosedOut & "', '" & Comments & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".Fee_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("numAuditID")) Then
                            txtAuditID.Clear()
                        Else
                            txtAuditID.Text = dr.Item("numAuditID")
                        End If
                    End While
                    dr.Close()
                End If
                MsgBox("CY_2005 Audit tracking saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                Exit Sub
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnManagerSignOff_CY2005_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManagerSignOff_CY2005.Click
        Try
            If txtAuditID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                "numManagerSignOff_05 = '" & UserGCode & "', " & _
                "datManagerSignOff_05 = '" & OracleDate & "' " & _
                "where numAuditID = '" & txtAuditID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "datManagerSignOff_05 " & _
                "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".Fee_Audit_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.numManagerSignOff_05 = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
                "and numAuditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_05.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_05.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                    If IsDBNull(dr.Item("datManagerSignOff_05")) Then
                        lblSignOffDat_05.Text = "Last Modified: - "
                    Else
                        lblSignOffDat_05.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_05"), "dd-MMM-yyyy")
                    End If
                End While
                dr.Close()

                MsgBox("CY 2005 Manager Sign-Off.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                MsgBox("Audit information must be Saved first, before you can sign-off.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Audit CY2004"
    Private Sub btnSaveFeeAudit_CY2004_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFeeAudit_CY2004.Click
        Try
            Dim InitialLetter As String = ""
            Dim LetterReturned As String = ""
            Dim AddressUnknown As String = ""
            Dim LetterRemailed As String = ""
            Dim DataCorrected As String = ""
            Dim Backruptcy As String = ""
            Dim UnableToContact As String = ""
            Dim NOVSent As String = ""
            Dim COSent As String = ""
            Dim AOSent As String = ""
            Dim FeesPaid As String = ""
            Dim AmountPaid As String = ""
            Dim ClosedOut As String = ""
            Dim Comments As String = ""

            If mtbAIRSNumber.Text <> "" And lblFacilityNameTop.Text <> "Facility Name: BAD AIRS #" Then
                If DTPInitialLetter_2004.Checked = False Then
                    InitialLetter = ""
                Else
                    InitialLetter = DTPInitialLetter_2004.Text
                End If
                If DTPLetterReturned_CY2004.Checked = False Then
                    LetterReturned = ""
                Else
                    LetterReturned = DTPLetterReturned_CY2004.Text
                End If
                If rdbAddressUnknownYes_CY2004.Checked = False And rdbAddressUnknownNo_CY2004.Checked = False Then
                    AddressUnknown = ""
                Else
                    If rdbAddressUnknownYes_CY2004.Checked = True Then
                        AddressUnknown = "True"
                    Else
                        AddressUnknown = "False"
                    End If
                End If
                If DTPLetterRemailed_CY2004.Checked = False Then
                    LetterRemailed = ""
                Else
                    LetterRemailed = DTPLetterRemailed_CY2004.Text
                End If
                If rdbDataCorrectYes_CY2004.Checked = False And rdbDataCorrectNo_CY2004.Checked = False Then
                    DataCorrected = "False"
                Else
                    If rdbDataCorrectYes_CY2004.Checked = True Then
                        DataCorrected = "True"
                    Else
                        DataCorrected = "False"
                    End If
                End If
                If rdbBankruptcyYes_CY2004.Checked = False And rdbBankruptcyNo_CY2004.Checked = False Then
                    Backruptcy = ""
                Else
                    If rdbBankruptcyYes_CY2004.Checked = True Then
                        Backruptcy = "True"
                    Else
                        Backruptcy = "False"
                    End If
                End If
                If rdbUnabletoContactYes_CY2004.Checked = False And rdbUnabletoContactNo_CY2004.Checked = False Then
                    UnableToContact = ""
                Else
                    If rdbUnabletoContactYes_CY2004.Checked = True Then
                        UnableToContact = "True"
                    Else
                        UnableToContact = "False"
                    End If
                End If
                If DTPNOVSent_CY2004.Checked = False Then
                    NOVSent = ""
                Else
                    NOVSent = DTPNOVSent_CY2004.Text
                End If
                If DTPCOSent_CY2004.Checked = False Then
                    COSent = ""
                Else
                    COSent = DTPCOSent_CY2004.Text
                End If

                If DTPAOSent_CY2004.Checked = False Then
                    AOSent = ""
                Else
                    AOSent = DTPAOSent_CY2004.Text
                End If
                If DTPFeesPaid_CY2004.Checked = False Then
                    FeesPaid = ""
                Else
                    FeesPaid = DTPFeesPaid_CY2004.Text
                End If
                If lblAmountPaid_CY2004.Text = "Fees Paid: -" Then
                    AmountPaid = Replace(lblAmountPaid_CY2004.Text, "Fees Paid: ", "")
                End If
                If DTPCloseOut_CY2004.Checked = False Then
                    ClosedOut = ""
                Else
                    ClosedOut = DTPCloseOut_CY2004.Text
                End If
                If txtComments_CY2004.Text = "" Then
                    Comments = ""
                Else
                    Comments = txtComments_CY2004.Text
                End If

                If txtAuditID.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                    "datInitialLetterMailed_04 = '" & InitialLetter & "', " & _
                    "datLetterReturned_04 = '" & LetterReturned & "', " & _
                    "strAddressUnknown_04 = '" & AddressUnknown & "', " & _
                    "datInitialLetterRemailed_04 = '" & LetterRemailed & "', " & _
                    "strFacilityResponded_04 = '" & DataCorrected & "', " & _
                    "strFacilityBankrupt_04 = '" & Backruptcy & "', " & _
                    "strUnableToContact_04 = '" & UnableToContact & "', " & _
                    "datNOVLetterSent_04 = '" & NOVSent & "', " & _
                    "datCOLetterSent_04 = '" & COSent & "', " & _
                    "datAOLetterSent_04 = '" & AOSent & "', " & _
                    "datFacilityPaidFees_04 = '" & FeesPaid & "', " & _
                    "strAmountPaid_04 = '" & Replace(AmountPaid, "'", "''") & "',  " & _
                    "datClosedOutFeeAudit_04 = '" & ClosedOut & "', " & _
                    "strComments_04 = '" & Replace(Comments, "'", "''") & "', " & _
                    "numStaffAssigned_04 = '" & UserGCode & "', " & _
                    "datLastModified_04 = '" & OracleDate & "' " & _
                    "where numAuditId = '" & txtAuditID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into " & connNameSpace & ".Fee_Audit_2010 " & _
                    "(numAuditID, strAIRSNumber, " & _
                    "datInitialLetterMailed_04, datLetterReturned_04, " & _
                    "strAddressUnknown_04, datInitialLetterRemailed_04, " & _
                    "strFacilityResponded_04, strUnableToContact_04, " & _
                    "strFacilityBankrupt_04, " & _
                    "datNOVLetterSent_04, datCOLetterSent_04, " & _
                    "datAOLetterSent_04, datFacilityPaidFees_04, " & _
                    "strAmountPaid_04, " & _
                    "datClosedOutFeeAudit_04, strComments_04,  " & _
                    "numStaffAssigned_04, datLastModified_04) " & _
                    "values " & _
                    "((select (max(numAuditID) + 1) from " & connNameSpace & ".Fee_Audit_2010), " & _
                    "'" & mtbAIRSNumber.Text & "', " & _
                    "'" & InitialLetter & "', '" & LetterReturned & "', " & _
                    "'" & AddressUnknown & "', '" & LetterRemailed & "', " & _
                    "'" & DataCorrected & "', '" & UnableToContact & "', " & _
                    "'" & Backruptcy & "', " & _
                    "'" & NOVSent & "', '" & COSent & "', " & _
                    "'" & AOSent & "', '" & FeesPaid & "', " & _
                    "'" & Replace(AmountPaid, "'", "''") & "', " & _
                    "'" & ClosedOut & "', '" & Comments & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".Fee_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("numAuditID")) Then
                            txtAuditID.Clear()
                        Else
                            txtAuditID.Text = dr.Item("numAuditID")
                        End If
                    End While
                    dr.Close()
                End If
                MsgBox("CY_2004 Audit tracking saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                Exit Sub
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnManagerSignOff_CY2004_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManagerSignOff_CY2004.Click
        Try
            If txtAuditID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                "numManagerSignOff_04 = '" & UserGCode & "', " & _
                "datManagerSignOff_04 = '" & OracleDate & "' " & _
                "where numAuditID = '" & txtAuditID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "datManagerSignOff_04 " & _
                "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".Fee_Audit_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.numManagerSignOff_04 = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
                "and numAuditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_04.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_04.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                    If IsDBNull(dr.Item("datManagerSignOff_04")) Then
                        lblSignOffDat_04.Text = "Last Modified: - "
                    Else
                        lblSignOffDat_04.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_04"), "dd-MMM-yyyy")
                    End If
                End While
                dr.Close()

                MsgBox("CY 2004 Manager Sign-Off.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                MsgBox("Audit information must be Saved first, before you can sign-off.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Audit CY2003"
    Private Sub btnSaveFeeAudit_CY2003_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFeeAudit_CY2003.Click
        Try
            Dim InitialLetter As String = ""
            Dim LetterReturned As String = ""
            Dim AddressUnknown As String = ""
            Dim LetterRemailed As String = ""
            Dim DataCorrected As String = ""
            Dim Backruptcy As String = ""
            Dim UnableToContact As String = ""
            Dim NOVSent As String = ""
            Dim COSent As String = ""
            Dim AOSent As String = ""
            Dim FeesPaid As String = ""
            Dim AmountPaid As String = ""
            Dim ClosedOut As String = ""
            Dim Comments As String = ""

            If mtbAIRSNumber.Text <> "" And lblFacilityNameTop.Text <> "Facility Name: BAD AIRS #" Then
                If DTPInitialLetter_2003.Checked = False Then
                    InitialLetter = ""
                Else
                    InitialLetter = DTPInitialLetter_2003.Text
                End If
                If DTPLetterReturned_CY2003.Checked = False Then
                    LetterReturned = ""
                Else
                    LetterReturned = DTPLetterReturned_CY2003.Text
                End If
                If rdbAddressUnknownYes_CY2003.Checked = False And rdbAddressUnknownNo_CY2003.Checked = False Then
                    AddressUnknown = ""
                Else
                    If rdbAddressUnknownYes_CY2003.Checked = True Then
                        AddressUnknown = "True"
                    Else
                        AddressUnknown = "False"
                    End If
                End If
                If DTPLetterRemailed_CY2003.Checked = False Then
                    LetterRemailed = ""
                Else
                    LetterRemailed = DTPLetterRemailed_CY2003.Text
                End If
                If rdbDataCorrectYes_CY2003.Checked = False And rdbDataCorrectNo_CY2003.Checked = False Then
                    DataCorrected = "False"
                Else
                    If rdbDataCorrectYes_CY2003.Checked = True Then
                        DataCorrected = "True"
                    Else
                        DataCorrected = "False"
                    End If
                End If
                If rdbBankruptcyYes_CY2003.Checked = False And rdbBankruptcyNo_CY2003.Checked = False Then
                    Backruptcy = ""
                Else
                    If rdbBankruptcyYes_CY2003.Checked = True Then
                        Backruptcy = "True"
                    Else
                        Backruptcy = "False"
                    End If
                End If
                If rdbUnabletoContactYes_CY2003.Checked = False And rdbUnabletoContactNo_CY2003.Checked = False Then
                    UnableToContact = ""
                Else
                    If rdbUnabletoContactYes_CY2003.Checked = True Then
                        UnableToContact = "True"
                    Else
                        UnableToContact = "False"
                    End If
                End If
                If DTPNOVSent_CY2003.Checked = False Then
                    NOVSent = ""
                Else
                    NOVSent = DTPNOVSent_CY2003.Text
                End If
                If DTPCOSent_CY2003.Checked = False Then
                    COSent = ""
                Else
                    COSent = DTPCOSent_CY2003.Text
                End If

                If DTPAOSent_CY2003.Checked = False Then
                    AOSent = ""
                Else
                    AOSent = DTPAOSent_CY2003.Text
                End If
                If DTPFeesPaid_CY2003.Checked = False Then
                    FeesPaid = ""
                Else
                    FeesPaid = DTPFeesPaid_CY2003.Text
                End If
                If lblAmountPaid_CY2003.Text = "Fees Paid: -" Then
                    AmountPaid = Replace(lblAmountPaid_CY2003.Text, "Fees Paid: ", "")
                End If
                If DTPCloseOut_CY2003.Checked = False Then
                    ClosedOut = ""
                Else
                    ClosedOut = DTPCloseOut_CY2003.Text
                End If
                If txtComments_CY2003.Text = "" Then
                    Comments = ""
                Else
                    Comments = txtComments_CY2003.Text
                End If

                If txtAuditID.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                    "datInitialLetterMailed_03 = '" & InitialLetter & "', " & _
                    "datLetterReturned_03 = '" & LetterReturned & "', " & _
                    "strAddressUnknown_03 = '" & AddressUnknown & "', " & _
                    "datInitialLetterRemailed_03 = '" & LetterRemailed & "', " & _
                    "strFacilityResponded_03 = '" & DataCorrected & "', " & _
                    "strFacilityBankrupt_03 = '" & Backruptcy & "', " & _
                    "strUnableToContact_03 = '" & UnableToContact & "', " & _
                    "datNOVLetterSent_03 = '" & NOVSent & "', " & _
                    "datCOLetterSent_03 = '" & COSent & "', " & _
                    "datAOLetterSent_03 = '" & AOSent & "', " & _
                    "datFacilityPaidFees_03 = '" & FeesPaid & "', " & _
                    "strAmountPaid_03 = '" & Replace(AmountPaid, "'", "''") & "',  " & _
                    "datClosedOutFeeAudit_03 = '" & ClosedOut & "', " & _
                    "strComments_03 = '" & Replace(Comments, "'", "''") & "', " & _
                    "numStaffAssigned_03 = '" & UserGCode & "', " & _
                    "datLastModified_03 = '" & OracleDate & "' " & _
                    "where numAuditId = '" & txtAuditID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into " & connNameSpace & ".Fee_Audit_2010 " & _
                    "(numAuditID, strAIRSNumber, " & _
                    "datInitialLetterMailed_03, datLetterReturned_03, " & _
                    "strAddressUnknown_03, datInitialLetterRemailed_03, " & _
                    "strFacilityResponded_03, strUnableToContact_03, " & _
                    "strFacilityBankrupt_03, " & _
                    "datNOVLetterSent_03, datCOLetterSent_03, " & _
                    "datAOLetterSent_03, datFacilityPaidFees_03, " & _
                    "strAmountPaid_03, " & _
                    "datClosedOutFeeAudit_03, strComments_03,  " & _
                    "numStaffAssigned_03, datLastModified_03) " & _
                    "values " & _
                    "((select (max(numAuditID) + 1) from " & connNameSpace & ".Fee_Audit_2010), " & _
                    "'" & mtbAIRSNumber.Text & "', " & _
                    "'" & InitialLetter & "', '" & LetterReturned & "', " & _
                    "'" & AddressUnknown & "', '" & LetterRemailed & "', " & _
                    "'" & DataCorrected & "', '" & UnableToContact & "', " & _
                    "'" & Backruptcy & "', " & _
                    "'" & NOVSent & "', '" & COSent & "', " & _
                    "'" & AOSent & "', '" & FeesPaid & "', " & _
                     "'" & Replace(AmountPaid, "'", "''") & "', " & _
                    "'" & ClosedOut & "', '" & Comments & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".Fee_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("numAuditID")) Then
                            txtAuditID.Clear()
                        Else
                            txtAuditID.Text = dr.Item("numAuditID")
                        End If
                    End While
                    dr.Close()
                End If
                MsgBox("CY_2003 Audit tracking saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                Exit Sub
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnManagerSignOff_CY2003_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManagerSignOff_CY2003.Click
        Try
            If txtAuditID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                "numManagerSignOff_03 = '" & UserGCode & "', " & _
                "datManagerSignOff_03 = '" & OracleDate & "' " & _
                "where numAuditID = '" & txtAuditID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "datManagerSignOff_03 " & _
                "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".Fee_Audit_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.numManagerSignOff_03 = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
                "and numAuditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_03.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_03.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                    If IsDBNull(dr.Item("datManagerSignOff_03")) Then
                        lblSignOffDat_03.Text = "Last Modified: - "
                    Else
                        lblSignOffDat_03.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_03"), "dd-MMM-yyyy")
                    End If
                End While
                dr.Close()

                MsgBox("CY 2003 Manager Sign-Off.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                MsgBox("Audit information must be Saved first, before you can sign-off.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Audit CY2002"
    Private Sub btnSaveFeeAudit_CY2002_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFeeAudit_CY2002.Click
        Try
            Dim InitialLetter As String = ""
            Dim LetterReturned As String = ""
            Dim AddressUnknown As String = ""
            Dim LetterRemailed As String = ""
            Dim DataCorrected As String = ""
            Dim Backruptcy As String = ""
            Dim UnableToContact As String = ""
            Dim NOVSent As String = ""
            Dim COSent As String = ""
            Dim AOSent As String = ""
            Dim FeesPaid As String = ""
            Dim AmountPaid As String = ""
            Dim ClosedOut As String = ""
            Dim Comments As String = ""

            If mtbAIRSNumber.Text <> "" And lblFacilityNameTop.Text <> "Facility Name: BAD AIRS #" Then
                If DTPInitialLetter_2002.Checked = False Then
                    InitialLetter = ""
                Else
                    InitialLetter = DTPInitialLetter_2002.Text
                End If
                If DTPLetterReturned_CY2002.Checked = False Then
                    LetterReturned = ""
                Else
                    LetterReturned = DTPLetterReturned_CY2002.Text
                End If
                If rdbAddressUnknownYes_CY2002.Checked = False And rdbAddressUnknownNo_CY2002.Checked = False Then
                    AddressUnknown = ""
                Else
                    If rdbAddressUnknownYes_CY2002.Checked = True Then
                        AddressUnknown = "True"
                    Else
                        AddressUnknown = "False"
                    End If
                End If
                If DTPLetterRemailed_CY2002.Checked = False Then
                    LetterRemailed = ""
                Else
                    LetterRemailed = DTPLetterRemailed_CY2002.Text
                End If
                If rdbDataCorrectYes_CY2002.Checked = False And rdbDataCorrectNo_CY2002.Checked = False Then
                    DataCorrected = "False"
                Else
                    If rdbDataCorrectYes_CY2002.Checked = True Then
                        DataCorrected = "True"
                    Else
                        DataCorrected = "False"
                    End If
                End If
                If rdbBankruptcyYes_CY2002.Checked = False And rdbBankruptcyNo_CY2002.Checked = False Then
                    Backruptcy = ""
                Else
                    If rdbBankruptcyYes_CY2002.Checked = True Then
                        Backruptcy = "True"
                    Else
                        Backruptcy = "False"
                    End If
                End If
                If rdbUnabletoContactYes_CY2002.Checked = False And rdbUnabletoContactNo_CY2002.Checked = False Then
                    UnableToContact = ""
                Else
                    If rdbUnabletoContactYes_CY2002.Checked = True Then
                        UnableToContact = "True"
                    Else
                        UnableToContact = "False"
                    End If
                End If
                If DTPNOVSent_CY2002.Checked = False Then
                    NOVSent = ""
                Else
                    NOVSent = DTPNOVSent_CY2002.Text
                End If
                If DTPCOSent_CY2002.Checked = False Then
                    COSent = ""
                Else
                    COSent = DTPCOSent_CY2002.Text
                End If

                If DTPAOSent_CY2002.Checked = False Then
                    AOSent = ""
                Else
                    AOSent = DTPAOSent_CY2002.Text
                End If
                If DTPFeesPaid_CY2002.Checked = False Then
                    FeesPaid = ""
                Else
                    FeesPaid = DTPFeesPaid_CY2002.Text
                End If
                If lblAmountPaid_CY2002.Text = "Fees Paid: -" Then
                    AmountPaid = Replace(lblAmountPaid_CY2002.Text, "Fees Paid: ", "")
                End If
                If DTPCloseOut_CY2002.Checked = False Then
                    ClosedOut = ""
                Else
                    ClosedOut = DTPCloseOut_CY2002.Text
                End If
                If txtComments_CY2002.Text = "" Then
                    Comments = ""
                Else
                    Comments = txtComments_CY2002.Text
                End If

                If txtAuditID.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                    "datInitialLetterMailed_02 = '" & InitialLetter & "', " & _
                    "datLetterReturned_02 = '" & LetterReturned & "', " & _
                    "strAddressUnknown_02 = '" & AddressUnknown & "', " & _
                    "datInitialLetterRemailed_02 = '" & LetterRemailed & "', " & _
                    "strFacilityResponded_02 = '" & DataCorrected & "', " & _
                    "strFacilityBankrupt_02 = '" & Backruptcy & "', " & _
                    "strUnableToContact_02 = '" & UnableToContact & "', " & _
                    "datNOVLetterSent_02 = '" & NOVSent & "', " & _
                    "datCOLetterSent_02 = '" & COSent & "', " & _
                    "datAOLetterSent_02 = '" & AOSent & "', " & _
                    "datFacilityPaidFees_02 = '" & FeesPaid & "', " & _
                    "strAmountPaid_02 = '" & Replace(AmountPaid, "'", "''") & "',  " & _
                    "datClosedOutFeeAudit_02 = '" & ClosedOut & "', " & _
                    "strComments_02 = '" & Replace(Comments, "'", "''") & "', " & _
                    "numStaffAssigned_02 = '" & UserGCode & "', " & _
                    "datLastModified_02 = '" & OracleDate & "' " & _
                    "where numAuditId = '" & txtAuditID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into " & connNameSpace & ".Fee_Audit_2010 " & _
                    "(numAuditID, strAIRSNumber, " & _
                    "datInitialLetterMailed_02, datLetterReturned_02, " & _
                    "strAddressUnknown_02, datInitialLetterRemailed_02, " & _
                    "strFacilityResponded_02, strUnableToContact_02, " & _
                    "strFacilityBankrupt_02, " & _
                    "datNOVLetterSent_02, datCOLetterSent_02, " & _
                    "datAOLetterSent_02, datFacilityPaidFees_02, " & _
                    "strAmountPaid_02, " & _
                    "datClosedOutFeeAudit_02, strComments_02,  " & _
                    "numStaffAssigned_02, datLastModified_02) " & _
                    "values " & _
                    "((select (max(numAuditID) + 1) from " & connNameSpace & ".Fee_Audit_2010), " & _
                    "'" & mtbAIRSNumber.Text & "', " & _
                    "'" & InitialLetter & "', '" & LetterReturned & "', " & _
                    "'" & AddressUnknown & "', '" & LetterRemailed & "', " & _
                    "'" & DataCorrected & "', '" & UnableToContact & "', " & _
                    "'" & Backruptcy & "', " & _
                    "'" & NOVSent & "', '" & COSent & "', " & _
                    "'" & AOSent & "', '" & FeesPaid & "', " & _
                     "'" & Replace(AmountPaid, "'", "''") & "', " & _
                    "'" & ClosedOut & "', '" & Comments & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".Fee_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("numAuditID")) Then
                            txtAuditID.Clear()
                        Else
                            txtAuditID.Text = dr.Item("numAuditID")
                        End If
                    End While
                    dr.Close()
                End If
                MsgBox("CY_2002 Audit tracking saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnManagerSignOff_CY2002_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManagerSignOff_CY2002.Click
        Try
            If txtAuditID.Text <> "" Then
                SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                "numManagerSignOff_02 = '" & UserGCode & "', " & _
                "datManagerSignOff_02 = '" & OracleDate & "' " & _
                "where numAuditID = '" & txtAuditID.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "datManagerSignOff_02 " & _
                "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".Fee_Audit_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.numManagerSignOff_02 = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
                "and numAuditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Staff")) Then
                        lblManagerSignOff_02.Text = "Manager Sign Off: - "
                    Else
                        lblManagerSignOff_02.Text = "Manager Sign Off: " & dr.Item("Staff")
                    End If
                    If IsDBNull(dr.Item("datManagerSignOff_02")) Then
                        lblSignOffDat_02.Text = "Last Modified: - "
                    Else
                        lblSignOffDat_02.Text = "Last Modified: " & Format(dr.Item("datManagerSignOff_02"), "dd-MMM-yyyy")
                    End If
                End While
                dr.Close()

                MsgBox("CY 2002 Manager Sign-Off.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                MsgBox("Audit information must be Saved first, before you can sign-off.", MsgBoxStyle.Information, "Fee Audit Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub btnSaveAuditComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAuditComments.Click
        Try

            Dim Comments As String = ""

            Comments = txtAuditComments.Text

            If mtbAIRSNumber.Text <> "" And lblFacilityNameTop.Text <> "Facility Name: BAD AIRS #" Then
                If txtAuditID.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".Fee_Audit_2010 set " & _
                   "strComments = '" & Replace(Comments, "'", "''") & "' " & _
                    "where numAuditId = '" & txtAuditID.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Insert into " & connNameSpace & ".Fee_Audit_2010 " & _
                    "(numAuditID, strAIRSNumber, " & _
                    "strComments) " & _
                    "values " & _
                    "((select (max(numAuditID) + 1) from " & connNameSpace & ".Fee_Audit_2010), " & _
                    "'" & Comments & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & _
                    "numAuditID " & _
                    "from " & connNameSpace & ".Fee_Audit_2010 " & _
                    "where strAIRSNumber = '" & mtbAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("numAuditID")) Then
                            txtAuditID.Clear()
                        Else
                            txtAuditID.Text = dr.Item("numAuditID")
                        End If
                    End While
                    dr.Close()
                End If
                MsgBox("Additional Comments saved.", MsgBoxStyle.Information, "Fee Audit Tool")
            Else
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNonRespondersData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNonRespondersData.Click
        Try
            Dim SQLLine As String = ""

            If rdbInactive.Checked = True Then
                SQLLine = "and strActive = 'False' "
            End If
            If rdbActive.Checked = True Then
                SQLLine = "and (strActive = 'True' or strActive is null) "
            End If
            If rdbActiveAll.Checked = True Then
                SQLLine = " "
            End If

            SQL = "select " & _
"NUMNONRESPONDERSID, " & _
"STRAIRSNUMBER, " & _
"STRFACILITYNAME, " & _
"STRCOUNTYNAME, " & _
"STRFACILITYSTREET, " & _
"STRFACILITYCITY, " & _
"STRFACILITYSTATE, " & _
"STRFACILITYZIPCODE, " & _
"STRFACILITYCLASS, " & _
"STROPERATINGSTATUS, " & _
"STRSICCODE, " & _
"STRNSPSSTATUS, " & _
"STRTVSTATUS, " & _
"STRCONTACTFIRSTNAME, " & _
"STRCONTACTLASTNAME, " & _
"STRCONTACTPREFIX, " & _
"STRCONTACTTITLE, " & _
"STRCONTACTCOMPANY, " & _
"STRCONTACTEMAIL, " & _
"STRCONTACTPHONENUMBER, " & _
"STRCONTACTADDRESS, " & _
"STRCONTACTCITY, " & _
"STRCONTACTSTATE, " & _
"STRCONTACTZIPCODE, " & _
"STRCONTACTDESCRIPTION, " & _
"STRCONTACTKEY, " & _
"STRAIRSNUMBER_08, " & _
"STRFACILITYNAME_08, " & _
"STRFACILITYSTREET_08, " & _
"STRFACILITYCITY_08, " & _
"STRFACILITYSTATE_08, " & _
"STRFACILITYZIPCODE_08, " & _
"STRCLASSIFICATION_08, " & _
"STROPERATINGSTATUS_08, " & _
 "STRNSPSSTATUS_08, " & _
 "STRTVSTATUS_08, " & _
 "STRCONTACTFIRSTNAME_08, " & _
 "STRCONTACTLASTNAME_08, " & _
 "STRCONTACTCOMPANY_08, " & _
 "STRCONTACTADDRESS_08, " & _
 "STRCONTACTCITY_08, " & _
 "STRCONTACTSTATE_08, " & _
 "STRCONTACTZIPCODE_08, " & _
 "STATUS_08, " & _
 "STRAIRSNUMBER_07, " & _
 "STRFACILITYNAME_07, " & _
 "STRFACILITYSTREET_07, " & _
 "STRFACILITYCITY_07, " & _
 "STRFACILITYSTATE_07, " & _
 "STRFACILITYZIPCODE_07, " & _
 "STRCLASSIFICATION_07, " & _
 "STROPERATINGSTATUS_07, " & _
 "STRNSPSSTATUS_07, " & _
 "STRTVSTATUS_07, " & _
 "STRCONTACTFIRSTNAME_07, " & _
 "STRCONTACTLASTNAME_07, " & _
 "STRCONTACTCOMPANY_07, " & _
 "STRCONTACTADDRESS_07, " & _
 "STRCONTACTCITY_07, " & _
 "STRCONTACTSTATE_07, " & _
 "STRCONTACTZIPCODE_07, " & _
 "STATUS_07, " & _
 "STRAIRSNUMBER_06, " & _
"STRFACILITYNAME_06, " & _
"STRFACILITYSTREET_06, " & _
"STRFACILITYCITY_06, " & _
"STRFACILITYSTATE_06, " & _
"STRFACILITYZIPCODE_06, " & _
"STRCLASSIFICATION_06, " & _
"STROPERATINGSTATUS_06, " & _
"STRNSPSSTATUS_06, " & _
"STRTVSTATUS_06, " & _
"STRCONTACTFIRSTNAME_06, " & _
"STRCONTACTLASTNAME_06, " & _
"STRCONTACTCOMPANY_06, " & _
"STRCONTACTADDRESS_06, " & _
"STRCONTACTCITY_06, " & _
"STRCONTACTSTATE_06, " & _
"STRCONTACTZIPCODE_06, " & _
"STATUS_06, " & _
"strStaffResponsible, " & _
"(strLastName||', '||strFirstName) as Staff, " & _
"STROWNERSHIPCHANGE, " & _
"STROWNERSHIPCOMMENTS, " & _
"STRSOURCECLASSCHANGE, " & _
"STRSOURCECLASSCOMMENTS, " & _
"STRCOMMENTS, " & _
"STRFACILITYNAME_EDIT, " & _
"STRFACILITYSTREET_EDIT, " & _
"STRFACILITYCITY_EDIT, " & _
"STRFACILITYSTATE_EDIT, " & _
"STRFACILITYZIPCODE_EDIT, " & _
"STRFACILITYCLASS_EDIT, " & _
"STROPERATINGSTATUS_EDIT, " & _
"STRSICCODE_EDIT, " & _
"STRNSPSSTATUS_EDIT, " & _
"STRTVSTATUS_EDIT, " & _
"STRCONTACTFIRSTNAME_EDIT, " & _
"STRCONTACTLASTNAME_EDIT, " & _
"STRCONTACTPREFIX_EDIT, " & _
"STRCONTACTTITLE_EDIT, " & _
"STRCONTACTCOMPANY_EDIT, " & _
"STRCONTACTEMAIL_EDIT, " & _
"STRCONTACTPHONENUMBER_EDIT, " & _
"STRCONTACTADDRESS_EDIT, " & _
"STRCONTACTCITY_EDIT, " & _
"STRCONTACTSTATE_EDIT, " & _
"STRCONTACTZIPCODE_EDIT, " & _
"STRCONTACTDESCRIPTION_EDIT, " & _
"STRCURRENTINFOCOMMENTS, " & _
"STRFACILITYNAME_08_EDIT, " & _
"STRFACILITYSTREET_08_EDIT, " & _
"STRFACILITYCITY_08_EDIT, " & _
"STRFACILITYSTATE_08_EDIT, " & _
"STRFACILITYZIPCODE_08_EDIT, " & _
"STRCLASSIFICATION_08_EDIT, " & _
"STROPERATINGSTATUS_08_EDIT, " & _
"STRNSPSSTATUS_08_EDIT, " & _
"STRTVSTATUS_08_EDIT, " & _
"STRCONTACTFIRSTNAME_08_EDIT, " & _
"STRCONTACTLASTNAME_08_EDIT, " & _
"STRCONTACTCOMPANY_08_EDIT, " & _
"STRCONTACTADDRESS_08_EDIT, " & _
"STRCONTACTCITY_08_EDIT, " & _
"STRCONTACTSTATE_08_EDIT, " & _
"STRCONTACTZIPCODE_08_EDIT, " & _
"STRTOTALPAID_08_EDIT, " & _
"STR2008COMMENTS, " & _
"STRFACILITYNAME_07_EDIT, " & _
"STRFACILITYSTREET_07_EDIT, " & _
"STRFACILITYCITY_07_EDIT, " & _
"STRFACILITYSTATE_07_EDIT, " & _
"STRFACILITYZIPCODE_07_EDIT, " & _
"STRCLASSIFICATION_07_EDIT, " & _
"STROPERATINGSTATUS_07_EDIT, " & _
"STRNSPSSTATUS_07_EDIT, " & _
"STRTVSTATUS_07_EDIT, " & _
"STRCONTACTFIRSTNAME_07_EDIT, " & _
"STRCONTACTLASTNAME_07_EDIT, " & _
"STRCONTACTCOMPANY_07_EDIT, " & _
"STRCONTACTADDRESS_07_EDIT, " & _
"STRCONTACTCITY_07_EDIT, " & _
"STRCONTACTSTATE_07_EDIT, " & _
"STRCONTACTZIPCODE_07_EDIT, " & _
"STRTOTALPAID_07_EDIT, " & _
"STR2007COMMENTS, " & _
"STRFACILITYNAME_06_EDIT, " & _
"STRFACILITYSTREET_06_EDIT, " & _
"STRFACILITYCITY_06_EDIT, " & _
"STRFACILITYSTATE_06_EDIT, " & _
"STRFACILITYZIPCODE_06_EDIT, " & _
"STRCLASSIFICATION_06_EDIT, " & _
"STROPERATINGSTATUS_06_EDIT, " & _
"STRNSPSSTATUS_06_EDIT, " & _
"STRTVSTATUS_06_EDIT, " & _
"STRCONTACTFIRSTNAME_06_EDIT, " & _
"STRCONTACTLASTNAME_06_EDIT, " & _
"STRCONTACTCOMPANY_06_EDIT, " & _
"STRCONTACTADDRESS_06_EDIT, " & _
"STRCONTACTCITY_06_EDIT, " & _
"STRCONTACTSTATE_06_EDIT, " & _
"STRCONTACTZIPCODE_06_EDIT, " & _
"STRTOTALPAID_06_EDIT, " & _
"STR2006COMMENTS,  " & _
"STRACTIVE " & _
"from " & connNameSpace & ".Fee_nonResponders_2010, " & connNameSpace & ".EPDUSerProfiles " & _
"where " & connNameSpace & ".Fee_NonResponders_2010.strStaffResponsible = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
SQLLine & _
"order by STRAIRSnumber "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "NonResponders")
            dgvFeeAuditReport.DataSource = ds
            dgvFeeAuditReport.DataMember = "NonResponders"

            dgvFeeAuditReport.RowHeadersVisible = False
            dgvFeeAuditReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeAuditReport.AllowUserToResizeColumns = True
            dgvFeeAuditReport.AllowUserToAddRows = False
            dgvFeeAuditReport.AllowUserToDeleteRows = False
            dgvFeeAuditReport.AllowUserToOrderColumns = True
            dgvFeeAuditReport.AllowUserToResizeRows = True
            dgvFeeAuditReport.ColumnHeadersHeight = "35"

            dgvFeeAuditReport.Columns("numNonRespondersID").HeaderText = "ID"
            dgvFeeAuditReport.Columns("numNonRespondersID").DisplayIndex = 0
            dgvFeeAuditReport.Columns("numNonRespondersID").Width = 50
            dgvFeeAuditReport.Columns("strAIRSNumber").HeaderText = "AIRS #"
            dgvFeeAuditReport.Columns("strAIRSNumber").DisplayIndex = 1
            dgvFeeAuditReport.Columns("strAIRSNumber").Width = 75
            dgvFeeAuditReport.Columns("strFacilityName").HeaderText = "Current Facility Name"
            dgvFeeAuditReport.Columns("strFacilityName").DisplayIndex = 2
            dgvFeeAuditReport.Columns("strFacilityName").Width = 125
            dgvFeeAuditReport.Columns("strCountyName").HeaderText = "County Name"
            dgvFeeAuditReport.Columns("strCountyName").DisplayIndex = 3
            dgvFeeAuditReport.Columns("strFacilityStreet").HeaderText = "Current Facility Street"
            dgvFeeAuditReport.Columns("strFacilityStreet").DisplayIndex = 4
            dgvFeeAuditReport.Columns("strFacilityCity").HeaderText = "Current Facility City"
            dgvFeeAuditReport.Columns("strFacilityCity").DisplayIndex = 5
            dgvFeeAuditReport.Columns("strFacilityState").HeaderText = "Current Facility State"
            dgvFeeAuditReport.Columns("strFacilityState").DisplayIndex = 6
            dgvFeeAuditReport.Columns("strFacilityZipCode").HeaderText = "Current Facility Zip"
            dgvFeeAuditReport.Columns("strFacilityZipCode").DisplayIndex = 7
            dgvFeeAuditReport.Columns("strFacilityClass").HeaderText = "Current Classification"
            dgvFeeAuditReport.Columns("strFacilityClass").DisplayIndex = 8
            dgvFeeAuditReport.Columns("strOperatingStatus").HeaderText = "Current Op. Status"
            dgvFeeAuditReport.Columns("strOperatingStatus").DisplayIndex = 9
            dgvFeeAuditReport.Columns("strSICCode").HeaderText = "Current SIC Code"
            dgvFeeAuditReport.Columns("strSICCode").DisplayIndex = 10
            dgvFeeAuditReport.Columns("strNSPSStatus").HeaderText = "Current NSPS Status"
            dgvFeeAuditReport.Columns("strNSPSStatus").DisplayIndex = 11
            dgvFeeAuditReport.Columns("strTVStatus").HeaderText = "Current TV Status"
            dgvFeeAuditReport.Columns("strTVStatus").DisplayIndex = 12
            dgvFeeAuditReport.Columns("strContactFirstName").HeaderText = "Contact First Name"
            dgvFeeAuditReport.Columns("strContactFirstName").DisplayIndex = 13
            dgvFeeAuditReport.Columns("strContactLastName").HeaderText = "Contact Last Name"
            dgvFeeAuditReport.Columns("strContactLastName").DisplayIndex = 14
            dgvFeeAuditReport.Columns("strContactPrefix").HeaderText = "Contact Prefix"
            dgvFeeAuditReport.Columns("strContactPrefix").DisplayIndex = 15
            dgvFeeAuditReport.Columns("strContactTitle").HeaderText = "Contact Title"
            dgvFeeAuditReport.Columns("strContactTitle").DisplayIndex = 16
            dgvFeeAuditReport.Columns("strContactCompany").HeaderText = "Contact Company"
            dgvFeeAuditReport.Columns("strContactCompany").DisplayIndex = 17
            dgvFeeAuditReport.Columns("strContactEmail").HeaderText = "Contact Email"
            dgvFeeAuditReport.Columns("strContactEmail").DisplayIndex = 18
            dgvFeeAuditReport.Columns("strContactPhoneNumber").HeaderText = "Phone Number"
            dgvFeeAuditReport.Columns("strContactPhoneNumber").DisplayIndex = 19
            dgvFeeAuditReport.Columns("strContactaddress").HeaderText = "Contact Address"
            dgvFeeAuditReport.Columns("strContactaddress").DisplayIndex = 20
            dgvFeeAuditReport.Columns("strContactCity").HeaderText = "Contact City"
            dgvFeeAuditReport.Columns("strContactCity").DisplayIndex = 21
            dgvFeeAuditReport.Columns("strContactState").HeaderText = "Contact State"
            dgvFeeAuditReport.Columns("strContactState").DisplayIndex = 22
            dgvFeeAuditReport.Columns("strContactZipCode").HeaderText = "Contact Zip Code"
            dgvFeeAuditReport.Columns("strContactZipCode").DisplayIndex = 23
            dgvFeeAuditReport.Columns("strContactDescription").HeaderText = "Contact Desc."
            dgvFeeAuditReport.Columns("strContactDescription").DisplayIndex = 24
            dgvFeeAuditReport.Columns("strContactKey").HeaderText = "Contact Key"
            dgvFeeAuditReport.Columns("strContactKey").DisplayIndex = 25
            dgvFeeAuditReport.Columns("strAIRSNumber_08").HeaderText = "AIRS #_08"
            dgvFeeAuditReport.Columns("strAIRSNumber_08").DisplayIndex = 26
            dgvFeeAuditReport.Columns("strFacilityName_08").HeaderText = "Facility Name_08"
            dgvFeeAuditReport.Columns("strFacilityName_08").DisplayIndex = 27
            dgvFeeAuditReport.Columns("strFacilityStreet_08").HeaderText = "Facility Street_08"
            dgvFeeAuditReport.Columns("strFacilityStreet_08").DisplayIndex = 28
            dgvFeeAuditReport.Columns("strFacilityCity_08").HeaderText = "Facility City_08"
            dgvFeeAuditReport.Columns("strFacilityCity_08").DisplayIndex = 29
            dgvFeeAuditReport.Columns("strFacilityState_08").HeaderText = "Facility State_08"
            dgvFeeAuditReport.Columns("strFacilityState_08").DisplayIndex = 30
            dgvFeeAuditReport.Columns("strFacilityZipCode_08").HeaderText = "Facility Zip Code_08"
            dgvFeeAuditReport.Columns("strFacilityZipCode_08").DisplayIndex = 31
            dgvFeeAuditReport.Columns("strClassification_08").HeaderText = "Classification_08"
            dgvFeeAuditReport.Columns("strClassification_08").DisplayIndex = 32
            dgvFeeAuditReport.Columns("strOperatingStatus_08").HeaderText = "Op. Status_08"
            dgvFeeAuditReport.Columns("strOperatingStatus_08").DisplayIndex = 33
            dgvFeeAuditReport.Columns("strNSPSStatus_08").HeaderText = "NSPS Status_08"
            dgvFeeAuditReport.Columns("strNSPSStatus_08").DisplayIndex = 34
            dgvFeeAuditReport.Columns("strTVStatus_08").HeaderText = "TV Status_08"
            dgvFeeAuditReport.Columns("strTVStatus_08").DisplayIndex = 35
            dgvFeeAuditReport.Columns("strContactFirstName_08").HeaderText = "Contact First Name_08"
            dgvFeeAuditReport.Columns("strContactFirstName_08").DisplayIndex = 36
            dgvFeeAuditReport.Columns("strContactLastName_08").HeaderText = "Contact Last Name_08"
            dgvFeeAuditReport.Columns("strContactLastName_08").DisplayIndex = 37
            dgvFeeAuditReport.Columns("strContactCompany_08").HeaderText = "Contact Company_08"
            dgvFeeAuditReport.Columns("strContactCompany_08").DisplayIndex = 38
            dgvFeeAuditReport.Columns("strContactAddress_08").HeaderText = "Contact Address_08"
            dgvFeeAuditReport.Columns("strContactAddress_08").DisplayIndex = 39
            dgvFeeAuditReport.Columns("strContactCity_08").HeaderText = "Contact City_08"
            dgvFeeAuditReport.Columns("strContactCity_08").DisplayIndex = 40
            dgvFeeAuditReport.Columns("strContactState_08").HeaderText = "Contact State_08"
            dgvFeeAuditReport.Columns("strContactState_08").DisplayIndex = 41
            dgvFeeAuditReport.Columns("strContactZipCode_08").HeaderText = "Contact Zip Code_08"
            dgvFeeAuditReport.Columns("strContactZipCode_08").DisplayIndex = 42
            dgvFeeAuditReport.Columns("STATUS_08").HeaderText = "Status_08"
            dgvFeeAuditReport.Columns("STATUS_08").DisplayIndex = 43
            dgvFeeAuditReport.Columns("strAIRSNumber_07").HeaderText = "AIRS #_07"
            dgvFeeAuditReport.Columns("strAIRSNumber_07").DisplayIndex = 44
            dgvFeeAuditReport.Columns("strFacilityName_07").HeaderText = "Facility Name_07"
            dgvFeeAuditReport.Columns("strFacilityName_07").DisplayIndex = 45
            dgvFeeAuditReport.Columns("strFacilityStreet_07").HeaderText = "Facility Street_07"
            dgvFeeAuditReport.Columns("strFacilityStreet_07").DisplayIndex = 46
            dgvFeeAuditReport.Columns("strFacilityCity_07").HeaderText = "Facility City_07"
            dgvFeeAuditReport.Columns("strFacilityCity_07").DisplayIndex = 47
            dgvFeeAuditReport.Columns("strFacilityState_07").HeaderText = "Facility State_07"
            dgvFeeAuditReport.Columns("strFacilityState_07").DisplayIndex = 48
            dgvFeeAuditReport.Columns("strFacilityZipCode_07").HeaderText = "Facility Zip Code_07"
            dgvFeeAuditReport.Columns("strFacilityZipCode_07").DisplayIndex = 49
            dgvFeeAuditReport.Columns("strClassification_07").HeaderText = "Classification_07"
            dgvFeeAuditReport.Columns("strClassification_07").DisplayIndex = 50
            dgvFeeAuditReport.Columns("strOperatingStatus_07").HeaderText = "Op. Status_07"
            dgvFeeAuditReport.Columns("strOperatingStatus_07").DisplayIndex = 51
            dgvFeeAuditReport.Columns("strNSPSStatus_07").HeaderText = "NSPS Status_07"
            dgvFeeAuditReport.Columns("strNSPSStatus_07").DisplayIndex = 52
            dgvFeeAuditReport.Columns("strTVStatus_07").HeaderText = "TV Status_07"
            dgvFeeAuditReport.Columns("strTVStatus_07").DisplayIndex = 53
            dgvFeeAuditReport.Columns("strContactFirstName_07").HeaderText = "Contact First Name_07"
            dgvFeeAuditReport.Columns("strContactFirstName_07").DisplayIndex = 54
            dgvFeeAuditReport.Columns("strContactLastName_07").HeaderText = "Contact Last Name_07"
            dgvFeeAuditReport.Columns("strContactLastName_07").DisplayIndex = 55
            dgvFeeAuditReport.Columns("strContactCompany_07").HeaderText = "Contact Company_07"
            dgvFeeAuditReport.Columns("strContactCompany_07").DisplayIndex = 56
            dgvFeeAuditReport.Columns("strContactAddress_07").HeaderText = "Contact Address_07"
            dgvFeeAuditReport.Columns("strContactAddress_07").DisplayIndex = 57
            dgvFeeAuditReport.Columns("strContactCity_07").HeaderText = "Contact City_07"
            dgvFeeAuditReport.Columns("strContactCity_07").DisplayIndex = 58
            dgvFeeAuditReport.Columns("strContactState_07").HeaderText = "Contact State_07"
            dgvFeeAuditReport.Columns("strContactState_07").DisplayIndex = 59
            dgvFeeAuditReport.Columns("strContactZipCode_07").HeaderText = "Contact Zip Code_07"
            dgvFeeAuditReport.Columns("strContactZipCode_07").DisplayIndex = 60
            dgvFeeAuditReport.Columns("STATUS_07").HeaderText = "STATUS_07"
            dgvFeeAuditReport.Columns("STATUS_07").DisplayIndex = 61
            dgvFeeAuditReport.Columns("strAIRSNumber_06").HeaderText = "AIRS #_06"
            dgvFeeAuditReport.Columns("strAIRSNumber_06").DisplayIndex = 62
            dgvFeeAuditReport.Columns("strFacilityName_06").HeaderText = "Facility Name_06"
            dgvFeeAuditReport.Columns("strFacilityName_06").DisplayIndex = 63
            dgvFeeAuditReport.Columns("strFacilityStreet_06").HeaderText = "Facility Street_06"
            dgvFeeAuditReport.Columns("strFacilityStreet_06").DisplayIndex = 64
            dgvFeeAuditReport.Columns("strFacilityCity_06").HeaderText = "Facility City_06"
            dgvFeeAuditReport.Columns("strFacilityCity_06").DisplayIndex = 65
            dgvFeeAuditReport.Columns("strFacilityState_06").HeaderText = "Facility State_06"
            dgvFeeAuditReport.Columns("strFacilityState_06").DisplayIndex = 66
            dgvFeeAuditReport.Columns("strFacilityZipCode_06").HeaderText = "Facility Zip Code_06"
            dgvFeeAuditReport.Columns("strFacilityZipCode_06").DisplayIndex = 67
            dgvFeeAuditReport.Columns("strClassification_06").HeaderText = "Classification_06"
            dgvFeeAuditReport.Columns("strClassification_06").DisplayIndex = 68
            dgvFeeAuditReport.Columns("strOperatingStatus_06").HeaderText = "Op. Status_06"
            dgvFeeAuditReport.Columns("strOperatingStatus_06").DisplayIndex = 69
            dgvFeeAuditReport.Columns("strNSPSStatus_06").HeaderText = "NSPS Status_06"
            dgvFeeAuditReport.Columns("strNSPSStatus_06").DisplayIndex = 70
            dgvFeeAuditReport.Columns("strTVStatus_06").HeaderText = "TV Status_06"
            dgvFeeAuditReport.Columns("strTVStatus_06").DisplayIndex = 71
            dgvFeeAuditReport.Columns("strContactFirstName_06").HeaderText = "Contact First Name_06"
            dgvFeeAuditReport.Columns("strContactFirstName_06").DisplayIndex = 72
            dgvFeeAuditReport.Columns("strContactLastName_06").HeaderText = "Contact Last Name_06"
            dgvFeeAuditReport.Columns("strContactLastName_06").DisplayIndex = 73
            dgvFeeAuditReport.Columns("strContactCompany_06").HeaderText = "Contact Company_06"
            dgvFeeAuditReport.Columns("strContactCompany_06").DisplayIndex = 74
            dgvFeeAuditReport.Columns("strContactAddress_06").HeaderText = "Contact Address_06"
            dgvFeeAuditReport.Columns("strContactAddress_06").DisplayIndex = 75
            dgvFeeAuditReport.Columns("strContactCity_06").HeaderText = "Contact City_06"
            dgvFeeAuditReport.Columns("strContactCity_06").DisplayIndex = 76
            dgvFeeAuditReport.Columns("strContactState_06").HeaderText = "Contact State_06"
            dgvFeeAuditReport.Columns("strContactState_06").DisplayIndex = 77
            dgvFeeAuditReport.Columns("strContactZipCode_06").HeaderText = "Contact Zip Code_06"
            dgvFeeAuditReport.Columns("strContactZipCode_06").DisplayIndex = 78
            dgvFeeAuditReport.Columns("STATUS_06").HeaderText = "STATUS_06"
            dgvFeeAuditReport.Columns("STATUS_06").DisplayIndex = 79
            dgvFeeAuditReport.Columns("strStaffResponsible").HeaderText = "Staff Responsible"
            dgvFeeAuditReport.Columns("strStaffResponsible").DisplayIndex = 80
            dgvFeeAuditReport.Columns("strStaffResponsible").Visible = False
            dgvFeeAuditReport.Columns("Staff").HeaderText = "Staff Responsible"
            dgvFeeAuditReport.Columns("Staff").DisplayIndex = 81
            dgvFeeAuditReport.Columns("strOwnershipChange").HeaderText = "Ownership Change"
            dgvFeeAuditReport.Columns("strOwnershipChange").DisplayIndex = 82
            dgvFeeAuditReport.Columns("strOwnershipComments").HeaderText = "Ownership Comments"
            dgvFeeAuditReport.Columns("strOwnershipComments").DisplayIndex = 83
            dgvFeeAuditReport.Columns("strSourceClassChange").HeaderText = "Source Class Change"
            dgvFeeAuditReport.Columns("strSourceClassChange").DisplayIndex = 84
            dgvFeeAuditReport.Columns("strSourceClassComments").HeaderText = "Source Class Change Comments"
            dgvFeeAuditReport.Columns("strSourceClassComments").DisplayIndex = 85
            dgvFeeAuditReport.Columns("strComments").HeaderText = "Comments"
            dgvFeeAuditReport.Columns("strComments").DisplayIndex = 86
            dgvFeeAuditReport.Columns("strFacilityName_Edit").HeaderText = "Current Name_Edit"
            dgvFeeAuditReport.Columns("strFacilityName_Edit").DisplayIndex = 87
            dgvFeeAuditReport.Columns("strFacilityStreet_Edit").HeaderText = "Current Street_Edit"
            dgvFeeAuditReport.Columns("strFacilityStreet_Edit").DisplayIndex = 88
            dgvFeeAuditReport.Columns("strFacilityCity_Edit").HeaderText = "Current City_Edit"
            dgvFeeAuditReport.Columns("strFacilityCity_Edit").DisplayIndex = 89
            dgvFeeAuditReport.Columns("strFacilityState_Edit").HeaderText = "Current State_Edit"
            dgvFeeAuditReport.Columns("strFacilityState_Edit").DisplayIndex = 90
            dgvFeeAuditReport.Columns("strFacilityZipCode_Edit").HeaderText = "Current Zip Code_Edit"
            dgvFeeAuditReport.Columns("strFacilityZipCode_Edit").DisplayIndex = 91
            dgvFeeAuditReport.Columns("strFacilityClass_Edit").HeaderText = "Current Classification_Edit"
            dgvFeeAuditReport.Columns("strFacilityClass_Edit").DisplayIndex = 92
            dgvFeeAuditReport.Columns("strOperatingStatus_Edit").HeaderText = "Current Op. Status_Edit"
            dgvFeeAuditReport.Columns("strOperatingStatus_Edit").DisplayIndex = 93
            dgvFeeAuditReport.Columns("strSICCode_Edit").HeaderText = "Current SIC Code_Edit"
            dgvFeeAuditReport.Columns("strSICCode_Edit").DisplayIndex = 94
            dgvFeeAuditReport.Columns("strNSPSStatus_Edit").HeaderText = "Current NSPS Status_Edit"
            dgvFeeAuditReport.Columns("strNSPSStatus_Edit").DisplayIndex = 95
            dgvFeeAuditReport.Columns("strTVStatus_Edit").HeaderText = "Current TV Status_Edit"
            dgvFeeAuditReport.Columns("strTVStatus_Edit").DisplayIndex = 96
            dgvFeeAuditReport.Columns("strContactFirstName_Edit").HeaderText = "Contact First Name_Edit"
            dgvFeeAuditReport.Columns("strContactFirstName_Edit").DisplayIndex = 97
            dgvFeeAuditReport.Columns("strContactLastName_Edit").HeaderText = "Contact Last Name_Edit"
            dgvFeeAuditReport.Columns("strContactLastName_Edit").DisplayIndex = 98
            dgvFeeAuditReport.Columns("strContactPrefix_Edit").HeaderText = "Contact Prefix_Edit"
            dgvFeeAuditReport.Columns("strContactPrefix_Edit").DisplayIndex = 99
            dgvFeeAuditReport.Columns("strContactTitle_Edit").HeaderText = "Contact Title_Edit"
            dgvFeeAuditReport.Columns("strContactTitle_Edit").DisplayIndex = 100
            dgvFeeAuditReport.Columns("strContactCompany_Edit").HeaderText = "Contact Company_Edit"
            dgvFeeAuditReport.Columns("strContactCompany_Edit").DisplayIndex = 101
            dgvFeeAuditReport.Columns("strContactEmail_Edit").HeaderText = "Contact Email_Edit"
            dgvFeeAuditReport.Columns("strContactEmail_Edit").DisplayIndex = 102
            dgvFeeAuditReport.Columns("strContactPhoneNumber_Edit").HeaderText = "Contact Phone Number_Edit"
            dgvFeeAuditReport.Columns("strContactPhoneNumber_Edit").DisplayIndex = 103
            dgvFeeAuditReport.Columns("strContactAddress_Edit").HeaderText = "Contact Address_Edit"
            dgvFeeAuditReport.Columns("strContactAddress_Edit").DisplayIndex = 104
            dgvFeeAuditReport.Columns("strContactCity_Edit").HeaderText = "Contact City_Edit"
            dgvFeeAuditReport.Columns("strContactCity_Edit").DisplayIndex = 105
            dgvFeeAuditReport.Columns("strContactState_Edit").HeaderText = "Contact State_Edit"
            dgvFeeAuditReport.Columns("strContactState_Edit").DisplayIndex = 106
            dgvFeeAuditReport.Columns("strContactZipCode_Edit").HeaderText = "Contact Zip Code_Edit"
            dgvFeeAuditReport.Columns("strContactZipCode_Edit").DisplayIndex = 107
            dgvFeeAuditReport.Columns("strContactDescription_Edit").HeaderText = "Contact Desc. Edit"
            dgvFeeAuditReport.Columns("strContactDescription_Edit").DisplayIndex = 108
            dgvFeeAuditReport.Columns("strCurrentInfoComments").HeaderText = "Current Info. Comments"
            dgvFeeAuditReport.Columns("strCurrentInfoComments").DisplayIndex = 109
            dgvFeeAuditReport.Columns("strFacilityname_08_Edit").HeaderText = "Facility Name_08 Edit"
            dgvFeeAuditReport.Columns("strFacilityname_08_Edit").DisplayIndex = 110
            dgvFeeAuditReport.Columns("strFacilityStreet_08_Edit").HeaderText = "Facility Street_08 Edit"
            dgvFeeAuditReport.Columns("strFacilityStreet_08_Edit").DisplayIndex = 111
            dgvFeeAuditReport.Columns("strFacilityCity_08_Edit").HeaderText = "Facility City_08 Edit"
            dgvFeeAuditReport.Columns("strFacilityCity_08_Edit").DisplayIndex = 112
            dgvFeeAuditReport.Columns("strFacilityState_08_Edit").HeaderText = "Facility State_08 Edit"
            dgvFeeAuditReport.Columns("strFacilityState_08_Edit").DisplayIndex = 113
            dgvFeeAuditReport.Columns("strFacilityZipCode_08_Edit").HeaderText = "Facility Zip Code_08 Edit"
            dgvFeeAuditReport.Columns("strFacilityZipCode_08_Edit").DisplayIndex = 114
            dgvFeeAuditReport.Columns("strClassification_08_Edit").HeaderText = "Classification_08 Edit"
            dgvFeeAuditReport.Columns("strClassification_08_Edit").DisplayIndex = 115
            dgvFeeAuditReport.Columns("strOperatingStatus_08_Edit").HeaderText = "Op. Status_08 Edit"
            dgvFeeAuditReport.Columns("strOperatingStatus_08_Edit").DisplayIndex = 116
            dgvFeeAuditReport.Columns("strNSPSStatus_08_Edit").HeaderText = "NSPS Status_08 Edit"
            dgvFeeAuditReport.Columns("strNSPSStatus_08_Edit").DisplayIndex = 117
            dgvFeeAuditReport.Columns("strTVStatus_08_Edit").HeaderText = "TV Status_08 Edit"
            dgvFeeAuditReport.Columns("strTVStatus_08_Edit").DisplayIndex = 118
            dgvFeeAuditReport.Columns("strContactFirstName_08_Edit").HeaderText = "Contact First Name_08 Edit"
            dgvFeeAuditReport.Columns("strContactFirstName_08_Edit").DisplayIndex = 119
            dgvFeeAuditReport.Columns("strContactLastName_08_Edit").HeaderText = "Contact Last Name_08 Edit"
            dgvFeeAuditReport.Columns("strContactLastName_08_Edit").DisplayIndex = 120
            dgvFeeAuditReport.Columns("strContactCompany_08_Edit").HeaderText = "Contact Company_08 Edit"
            dgvFeeAuditReport.Columns("strContactCompany_08_Edit").DisplayIndex = 121
            dgvFeeAuditReport.Columns("strContactAddress_08_Edit").HeaderText = "Contact Address_08 Edit"
            dgvFeeAuditReport.Columns("strContactAddress_08_Edit").DisplayIndex = 122
            dgvFeeAuditReport.Columns("strContactCity_08_Edit").HeaderText = "Contact City_08 Edit"
            dgvFeeAuditReport.Columns("strContactCity_08_Edit").DisplayIndex = 123
            dgvFeeAuditReport.Columns("strContactState_08_Edit").HeaderText = "Contact State_08 Edit"
            dgvFeeAuditReport.Columns("strContactState_08_Edit").DisplayIndex = 124
            dgvFeeAuditReport.Columns("strContactZipCode_08_Edit").HeaderText = "Contact Zip Code_08 Edit"
            dgvFeeAuditReport.Columns("strContactZipCode_08_Edit").DisplayIndex = 125
            dgvFeeAuditReport.Columns("strTotalPaid_08_Edit").HeaderText = "Total Paid_08 Edit"
            dgvFeeAuditReport.Columns("strTotalPaid_08_Edit").DisplayIndex = 126
            dgvFeeAuditReport.Columns("str2008Comments").HeaderText = "Comments_08 Edit"
            dgvFeeAuditReport.Columns("str2008Comments").DisplayIndex = 127
            dgvFeeAuditReport.Columns("strFacilityname_07_Edit").HeaderText = "Facility Name_07 Edit"
            dgvFeeAuditReport.Columns("strFacilityname_07_Edit").DisplayIndex = 128
            dgvFeeAuditReport.Columns("strFacilityStreet_07_Edit").HeaderText = "Facility Street_07 Edit"
            dgvFeeAuditReport.Columns("strFacilityStreet_07_Edit").DisplayIndex = 129
            dgvFeeAuditReport.Columns("strFacilityCity_07_Edit").HeaderText = "Facility City_07 Edit"
            dgvFeeAuditReport.Columns("strFacilityCity_07_Edit").DisplayIndex = 130
            dgvFeeAuditReport.Columns("strFacilityState_07_Edit").HeaderText = "Facility State_07 Edit"
            dgvFeeAuditReport.Columns("strFacilityState_07_Edit").DisplayIndex = 131
            dgvFeeAuditReport.Columns("strFacilityZipCode_07_Edit").HeaderText = "Facility Zip Code_07 Edit"
            dgvFeeAuditReport.Columns("strFacilityZipCode_07_Edit").DisplayIndex = 132
            dgvFeeAuditReport.Columns("strClassification_07_Edit").HeaderText = "Classification_07 Edit"
            dgvFeeAuditReport.Columns("strClassification_07_Edit").DisplayIndex = 133
            dgvFeeAuditReport.Columns("strOperatingStatus_07_Edit").HeaderText = "Op. Status_07 Edit"
            dgvFeeAuditReport.Columns("strOperatingStatus_07_Edit").DisplayIndex = 134
            dgvFeeAuditReport.Columns("strNSPSStatus_07_Edit").HeaderText = "NSPS Status_07 Edit"
            dgvFeeAuditReport.Columns("strNSPSStatus_07_Edit").DisplayIndex = 135
            dgvFeeAuditReport.Columns("strTVStatus_07_Edit").HeaderText = "TV Status_07 Edit"
            dgvFeeAuditReport.Columns("strTVStatus_07_Edit").DisplayIndex = 136
            dgvFeeAuditReport.Columns("strContactFirstName_07_Edit").HeaderText = "Contact First Name_07 Edit"
            dgvFeeAuditReport.Columns("strContactFirstName_07_Edit").DisplayIndex = 137
            dgvFeeAuditReport.Columns("strContactLastName_07_Edit").HeaderText = "Contact Last Name_07 Edit"
            dgvFeeAuditReport.Columns("strContactLastName_07_Edit").DisplayIndex = 138
            dgvFeeAuditReport.Columns("strContactCompany_07_Edit").HeaderText = "Contact Company_07 Edit"
            dgvFeeAuditReport.Columns("strContactCompany_07_Edit").DisplayIndex = 139
            dgvFeeAuditReport.Columns("strContactAddress_07_Edit").HeaderText = "Contact Address_07 Edit"
            dgvFeeAuditReport.Columns("strContactAddress_07_Edit").DisplayIndex = 140
            dgvFeeAuditReport.Columns("strContactCity_07_Edit").HeaderText = "Contact City_07 Edit"
            dgvFeeAuditReport.Columns("strContactCity_07_Edit").DisplayIndex = 141
            dgvFeeAuditReport.Columns("strContactState_07_Edit").HeaderText = "Contact State_07 Edit"
            dgvFeeAuditReport.Columns("strContactState_07_Edit").DisplayIndex = 142
            dgvFeeAuditReport.Columns("strContactZipCode_07_Edit").HeaderText = "Contact Zip Code_07 Edit"
            dgvFeeAuditReport.Columns("strContactZipCode_07_Edit").DisplayIndex = 143
            dgvFeeAuditReport.Columns("strTotalPaid_07_Edit").HeaderText = "Total Paid_07 Edit"
            dgvFeeAuditReport.Columns("strTotalPaid_07_Edit").DisplayIndex = 144
            dgvFeeAuditReport.Columns("str2007Comments").HeaderText = "Comments_07 Edit"
            dgvFeeAuditReport.Columns("str2007Comments").DisplayIndex = 145
            dgvFeeAuditReport.Columns("strFacilityname_06_Edit").HeaderText = "Facility Name_06 Edit"
            dgvFeeAuditReport.Columns("strFacilityname_06_Edit").DisplayIndex = 146
            dgvFeeAuditReport.Columns("strFacilityStreet_06_Edit").HeaderText = "Facility Street_06 Edit"
            dgvFeeAuditReport.Columns("strFacilityStreet_06_Edit").DisplayIndex = 147
            dgvFeeAuditReport.Columns("strFacilityCity_06_Edit").HeaderText = "Facility City_06 Edit"
            dgvFeeAuditReport.Columns("strFacilityCity_06_Edit").DisplayIndex = 148
            dgvFeeAuditReport.Columns("strFacilityState_06_Edit").HeaderText = "Facility State_06 Edit"
            dgvFeeAuditReport.Columns("strFacilityState_06_Edit").DisplayIndex = 149
            dgvFeeAuditReport.Columns("strFacilityZipCode_06_Edit").HeaderText = "Facility Zip Code_06 Edit"
            dgvFeeAuditReport.Columns("strFacilityZipCode_06_Edit").DisplayIndex = 150
            dgvFeeAuditReport.Columns("strClassification_06_Edit").HeaderText = "Classification_06 Edit"
            dgvFeeAuditReport.Columns("strClassification_06_Edit").DisplayIndex = 151
            dgvFeeAuditReport.Columns("strOperatingStatus_06_Edit").HeaderText = "Op. Status_06 Edit"
            dgvFeeAuditReport.Columns("strOperatingStatus_06_Edit").DisplayIndex = 152
            dgvFeeAuditReport.Columns("strNSPSStatus_06_Edit").HeaderText = "NSPS Status_06 Edit"
            dgvFeeAuditReport.Columns("strNSPSStatus_06_Edit").DisplayIndex = 153
            dgvFeeAuditReport.Columns("strTVStatus_06_Edit").HeaderText = "TV Status_06 Edit"
            dgvFeeAuditReport.Columns("strTVStatus_06_Edit").DisplayIndex = 154
            dgvFeeAuditReport.Columns("strContactFirstName_06_Edit").HeaderText = "Contact First Name_06 Edit"
            dgvFeeAuditReport.Columns("strContactFirstName_06_Edit").DisplayIndex = 155
            dgvFeeAuditReport.Columns("strContactLastName_06_Edit").HeaderText = "Contact Last Name_06 Edit"
            dgvFeeAuditReport.Columns("strContactLastName_06_Edit").DisplayIndex = 156
            dgvFeeAuditReport.Columns("strContactCompany_06_Edit").HeaderText = "Contact Company_06 Edit"
            dgvFeeAuditReport.Columns("strContactCompany_06_Edit").DisplayIndex = 157
            dgvFeeAuditReport.Columns("strContactAddress_06_Edit").HeaderText = "Contact Address_06 Edit"
            dgvFeeAuditReport.Columns("strContactAddress_06_Edit").DisplayIndex = 158
            dgvFeeAuditReport.Columns("strContactCity_06_Edit").HeaderText = "Contact City_06 Edit"
            dgvFeeAuditReport.Columns("strContactCity_06_Edit").DisplayIndex = 159
            dgvFeeAuditReport.Columns("strContactState_06_Edit").HeaderText = "Contact State_06 Edit"
            dgvFeeAuditReport.Columns("strContactState_06_Edit").DisplayIndex = 160
            dgvFeeAuditReport.Columns("strContactZipCode_06_Edit").HeaderText = "Contact Zip Code_06 Edit"
            dgvFeeAuditReport.Columns("strContactZipCode_06_Edit").DisplayIndex = 161
            dgvFeeAuditReport.Columns("strTotalPaid_06_Edit").HeaderText = "Total Paid_06 Edit"
            dgvFeeAuditReport.Columns("strTotalPaid_06_Edit").DisplayIndex = 162
            dgvFeeAuditReport.Columns("str2006Comments").HeaderText = "Comments_06 Edit"
            dgvFeeAuditReport.Columns("str2006Comments").DisplayIndex = 163
            dgvFeeAuditReport.Columns("STRACTIVE").HeaderText = "Active"
            dgvFeeAuditReport.Columns("STRACTIVE").DisplayIndex = 164

            txtCount.Text = dgvFeeAuditReport.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewFullAuditData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewFullAuditData.Click
        Try
            Dim SQLLine As String = ""
            Dim SQLline2 As String = ""

            If rdbInactive.Checked = True Then
                SQLLine = "and strActive = 'False' "
            End If
            If rdbActive.Checked = True Then
                SQLLine = "and (strActive = 'True' or strActive is null) "
            End If
            If rdbActiveAll.Checked = True Then
                SQLLine = " "
            End If

            If rdbAuditClosedOut.Checked = True Then
                If rdbAll.Checked = True Then
                    SQLline2 = " where ( " & _
                    "(datclosedoutfeeaudit_08 is not null or datInitialLetterMailed_08 is null)  " & _
                    "and (datclosedoutfeeaudit_07 is not null or datInitialLetterMailed_07 is null) " & _
                    "and (datclosedoutfeeaudit_06 is not null or datInitialLetterMailed_06 is null) " & _
                    "and (datclosedoutfeeaudit_05 is not null or datInitialLetterMailed_05 is null) " & _
                    "and (datclosedoutfeeaudit_04 is not null or datInitialLetterMailed_04 is null) " & _
                    "and (datclosedoutfeeaudit_03 is not null or datInitialLetterMailed_03 is null) " & _
                    "and (datclosedoutfeeaudit_02 is not null or datInitialLetterMailed_02 is null)  )  "
                Else
                    SQLline2 = " and (" & _
                    "(datclosedoutfeeaudit_08 is not null or datInitialLetterMailed_08 is null)  " & _
                 "and (datclosedoutfeeaudit_07 is not null or datInitialLetterMailed_07 is null) " & _
                 "and (datclosedoutfeeaudit_06 is not null or datInitialLetterMailed_06 is null) " & _
                 "and (datclosedoutfeeaudit_05 is not null or datInitialLetterMailed_05 is null) " & _
                 "and (datclosedoutfeeaudit_04 is not null or datInitialLetterMailed_04 is null) " & _
                 "and (datclosedoutfeeaudit_03 is not null or datInitialLetterMailed_03 is null) " & _
                 "and (datclosedoutfeeaudit_02 is not null or datInitialLetterMailed_02 is null) " & _
                 " )  "
                End If
            End If
            If rdbAuditNotClosedout.Checked = True Then
                If rdbAll.Checked = True Then
                    SQLline2 = " where (" & _
                    "datInitialLetterMailed_08 is not null and datclosedoutfeeaudit_08 is null " & _
                    "or datInitialLetterMailed_07 is not null and datclosedoutfeeaudit_07 is null " & _
                    "or datInitialLetterMailed_06 is not null and datclosedoutfeeaudit_06 is null " & _
                    "or datInitialLetterMailed_05 is not null and  datclosedoutfeeaudit_05 is null " & _
                    "or datInitialLetterMailed_04 is not null and  datclosedoutfeeaudit_04 is null " & _
                    "or datInitialLetterMailed_03 is not null and  datclosedoutfeeaudit_03 is null " & _
                    "or datInitialLetterMailed_02 is not null and  datclosedoutfeeaudit_02 is null )  "
                Else
                    'Open 
                    SQLline2 = " and (" & _
                    "datInitialLetterMailed_08 is not null and datclosedoutfeeaudit_08 is null " & _
                   "or datInitialLetterMailed_07 is not null and datclosedoutfeeaudit_07 is null " & _
                   "or datInitialLetterMailed_06 is not null and datclosedoutfeeaudit_06 is null " & _
                   "or datInitialLetterMailed_05 is not null and datclosedoutfeeaudit_05 is null " & _
                   "or datInitialLetterMailed_04 is not null and datclosedoutfeeaudit_04 is null " & _
                   "or datInitialLetterMailed_03 is not null and datclosedoutfeeaudit_03 is null " & _
                   "or datInitialLetterMailed_02 is not null and datclosedoutfeeaudit_02 is null )  "
                End If
            End If

            If rdbAllClosedOut.Checked = True Then
                SQLline2 = " "
            End If

            SQL = ""
            If rdbNonResponders.Checked = True Then
                SQL = "Select " & _
                 "* " & _
                 "from AIRBranch.Fee_Audit_2010  " & _
                  "where Exists (select * " & _
                  "from AIRBranch.Fee_NonResponders_2010 " & _
                  "where AIRBranch.Fee_Audit_2010.strAIRSnumber = AIRBranch.Fee_NonResponders_2010.strAIRSnumber " & _
                  SQLLine & " ) " & _
                  SQLline2 & _
                 " and numAuditID <> '0' "
            End If
            If rdbNonPayers.Checked = True Then
                SQL = "Select " & _
                 "* " & _
                 "from AIRBranch.Fee_Audit_2010  " & _
                  "where Exists (select * " & _
                  "from AIRBranch.Fee_NonPayers_2010 " & _
                  "where AIRBranch.Fee_Audit_2010.strAIRSnumber = AIRBranch.Fee_NonPayers_2010.strAIRSnumber " & _
                  SQLLine & " ) " & _
                  SQLline2 & _
                  " and numAuditID <> '0' "
            End If
            If rdbAll.Checked = True Or SQL = "" Then
                SQL = "select * " & _
                "from " & _
                "(Select " & _
                "* " & _
                "from " & connNameSpace & ".Fee_Audit_2010  " & _
                "where Exists (select * " & _
                "from " & connNameSpace & ".Fee_NonResponders_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.strAIRSnumber = " & connNameSpace & ".Fee_NonResponders_2010.strAIRSnumber " & _
                 SQLLine & " ) " & _
                "and numAuditID <> '0' " & _
                "union " & _
                "Select " & _
                "* " & _
                "from " & connNameSpace & ".Fee_Audit_2010  " & _
                "where Exists (select * " & _
                "from " & connNameSpace & ".Fee_NonPayers_2010 " & _
                "where " & connNameSpace & ".Fee_Audit_2010.strAIRSnumber = " & connNameSpace & ".Fee_NonPayers_2010.strAIRSnumber " & _
                SQLLine & " ) " & _
                "and numAuditID <> '0') " & _
                SQLline2 & _
                "order by strAIRSNumber  "
            End If

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "AuditData")
            dgvFeeAuditReport.DataSource = ds
            dgvFeeAuditReport.DataMember = "AuditData"

            dgvFeeAuditReport.RowHeadersVisible = False
            dgvFeeAuditReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeAuditReport.AllowUserToResizeColumns = True
            dgvFeeAuditReport.AllowUserToAddRows = False
            dgvFeeAuditReport.AllowUserToDeleteRows = False
            dgvFeeAuditReport.AllowUserToOrderColumns = True
            dgvFeeAuditReport.AllowUserToResizeRows = True
            dgvFeeAuditReport.ColumnHeadersHeight = "35"

            dgvFeeAuditReport.Columns("numAuditID").HeaderText = "ID"
            dgvFeeAuditReport.Columns("numAuditID").DisplayIndex = 0
            dgvFeeAuditReport.Columns("numAuditID").Width = 50
            dgvFeeAuditReport.Columns("strAIRSNumber").HeaderText = "AIRS #"
            dgvFeeAuditReport.Columns("strAIRSNumber").DisplayIndex = 1
            dgvFeeAuditReport.Columns("strAIRSNumber").Width = 75

            dgvFeeAuditReport.Columns("datInitialLetterMailed_08").HeaderText = "Initial Letter_08"
            dgvFeeAuditReport.Columns("datInitialLetterMailed_08").DisplayIndex = 2
            dgvFeeAuditReport.Columns("datInitialLetterMailed_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datLetterReturned_08").HeaderText = "Letter Returned_08"
            dgvFeeAuditReport.Columns("datLetterReturned_08").DisplayIndex = 3
            dgvFeeAuditReport.Columns("datLetterReturned_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAddressUnknown_08").HeaderText = "Address Unknown_08"
            dgvFeeAuditReport.Columns("strAddressUnknown_08").DisplayIndex = 4
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_08").HeaderText = "Letter Remailed_08"
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_08").DisplayIndex = 5
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strFacilityResponded_08").HeaderText = "Facility Responded w/ Corrections_08"
            dgvFeeAuditReport.Columns("strFacilityResponded_08").DisplayIndex = 6
            dgvFeeAuditReport.Columns("strFacilityBankrupt_08").HeaderText = "Facility Bankrupt_08"
            dgvFeeAuditReport.Columns("strFacilityBankrupt_08").DisplayIndex = 7
            dgvFeeAuditReport.Columns("strUnableToContact_08").HeaderText = "Unable to Contact_08"
            dgvFeeAuditReport.Columns("strUnableToContact_08").DisplayIndex = 8
            dgvFeeAuditReport.Columns("datNOVLetterSent_08").HeaderText = "NOV Sent_08"
            dgvFeeAuditReport.Columns("datNOVLetterSent_08").DisplayIndex = 9
            dgvFeeAuditReport.Columns("datNOVLetterSent_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datCOLetterSent_08").HeaderText = "CO Sent_08"
            dgvFeeAuditReport.Columns("datCOLetterSent_08").DisplayIndex = 10
            dgvFeeAuditReport.Columns("datCOLetterSent_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datAOLetterSent_08").HeaderText = "AO Sent_08"
            dgvFeeAuditReport.Columns("datAOLetterSent_08").DisplayIndex = 11
            dgvFeeAuditReport.Columns("datAOLetterSent_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_08").HeaderText = "Fees Paid_08"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_08").DisplayIndex = 12
            dgvFeeAuditReport.Columns("datFacilityPaidFees_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAmountPaid_08").HeaderText = "Amount Paid_08"
            dgvFeeAuditReport.Columns("strAmountPaid_08").DisplayIndex = 13
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_08").HeaderText = "Closed Out_08"
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_08").DisplayIndex = 14
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numStaffAssigned_08").HeaderText = "Staff ID_08"
            dgvFeeAuditReport.Columns("numStaffAssigned_08").DisplayIndex = 15
            dgvFeeAuditReport.Columns("datLastModified_08").HeaderText = "Last Modified by Staff_08"
            dgvFeeAuditReport.Columns("datLastModified_08").DisplayIndex = 16
            dgvFeeAuditReport.Columns("datLastModified_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numManagerSignOff_08").HeaderText = "Managers ID_08"
            dgvFeeAuditReport.Columns("numManagerSignOff_08").DisplayIndex = 17
            dgvFeeAuditReport.Columns("datManagerSignOff_08").HeaderText = "Last Modified by Manager_08"
            dgvFeeAuditReport.Columns("datManagerSignOff_08").DisplayIndex = 18
            dgvFeeAuditReport.Columns("datManagerSignOff_08").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strComments_08").HeaderText = "Comments_08"
            dgvFeeAuditReport.Columns("strComments_08").DisplayIndex = 19


            dgvFeeAuditReport.Columns("datInitialLetterMailed_07").HeaderText = "Initial Letter_07"
            dgvFeeAuditReport.Columns("datInitialLetterMailed_07").DisplayIndex = 20
            dgvFeeAuditReport.Columns("datInitialLetterMailed_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datLetterReturned_07").HeaderText = "Letter Returned_07"
            dgvFeeAuditReport.Columns("datLetterReturned_07").DisplayIndex = 21
            dgvFeeAuditReport.Columns("datLetterReturned_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAddressUnknown_07").HeaderText = "Address Unknown_07"
            dgvFeeAuditReport.Columns("strAddressUnknown_07").DisplayIndex = 22
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_07").HeaderText = "Letter Remailed_07"
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_07").DisplayIndex = 23
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strFacilityResponded_07").HeaderText = "Facility Responded w/ Corrections_07"
            dgvFeeAuditReport.Columns("strFacilityResponded_07").DisplayIndex = 24
            dgvFeeAuditReport.Columns("strFacilityBankrupt_07").HeaderText = "Facility Bankrupt_07"
            dgvFeeAuditReport.Columns("strFacilityBankrupt_07").DisplayIndex = 25
            dgvFeeAuditReport.Columns("strUnableToContact_07").HeaderText = "Unable to Contact_07"
            dgvFeeAuditReport.Columns("strUnableToContact_07").DisplayIndex = 26
            dgvFeeAuditReport.Columns("datNOVLetterSent_07").HeaderText = "NOV Sent_07"
            dgvFeeAuditReport.Columns("datNOVLetterSent_07").DisplayIndex = 27
            dgvFeeAuditReport.Columns("datNOVLetterSent_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datCOLetterSent_07").HeaderText = "CO Sent_07"
            dgvFeeAuditReport.Columns("datCOLetterSent_07").DisplayIndex = 28
            dgvFeeAuditReport.Columns("datCOLetterSent_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datAOLetterSent_07").HeaderText = "AO Sent_07"
            dgvFeeAuditReport.Columns("datAOLetterSent_07").DisplayIndex = 29
            dgvFeeAuditReport.Columns("datAOLetterSent_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_07").HeaderText = "Fees Paid_07"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_07").DisplayIndex = 30
            dgvFeeAuditReport.Columns("datFacilityPaidFees_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAmountPaid_07").HeaderText = "Amount Paid_07"
            dgvFeeAuditReport.Columns("strAmountPaid_07").DisplayIndex = 31
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_07").HeaderText = "Closed Out_07"
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_07").DisplayIndex = 32
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numStaffAssigned_07").HeaderText = "Staff ID_07"
            dgvFeeAuditReport.Columns("numStaffAssigned_07").DisplayIndex = 33
            dgvFeeAuditReport.Columns("datLastModified_07").HeaderText = "Last Modified by Staff_07"
            dgvFeeAuditReport.Columns("datLastModified_07").DisplayIndex = 34
            dgvFeeAuditReport.Columns("datLastModified_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numManagerSignOff_07").HeaderText = "Managers ID_07"
            dgvFeeAuditReport.Columns("numManagerSignOff_07").DisplayIndex = 35
            dgvFeeAuditReport.Columns("datManagerSignOff_07").HeaderText = "Last Modified by Manager_07"
            dgvFeeAuditReport.Columns("datManagerSignOff_07").DisplayIndex = 36
            dgvFeeAuditReport.Columns("datManagerSignOff_07").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strComments_07").HeaderText = "Comments_07"
            dgvFeeAuditReport.Columns("strComments_07").DisplayIndex = 37
            dgvFeeAuditReport.Columns("datInitialLetterMailed_06").HeaderText = "Initial Letter_06"
            dgvFeeAuditReport.Columns("datInitialLetterMailed_06").DisplayIndex = 38
            dgvFeeAuditReport.Columns("datInitialLetterMailed_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datLetterReturned_06").HeaderText = "Letter Returned_06"
            dgvFeeAuditReport.Columns("datLetterReturned_06").DisplayIndex = 39
            dgvFeeAuditReport.Columns("datLetterReturned_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAddressUnknown_06").HeaderText = "Address Unknown_06"
            dgvFeeAuditReport.Columns("strAddressUnknown_06").DisplayIndex = 40
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_06").HeaderText = "Letter Remailed_06"
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_06").DisplayIndex = 41
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strFacilityResponded_06").HeaderText = "Facility Responded w/ Corrections_06"
            dgvFeeAuditReport.Columns("strFacilityResponded_06").DisplayIndex = 42
            dgvFeeAuditReport.Columns("strFacilityBankrupt_06").HeaderText = "Facility Bankrupt_06"
            dgvFeeAuditReport.Columns("strFacilityBankrupt_06").DisplayIndex = 43
            dgvFeeAuditReport.Columns("strUnableToContact_06").HeaderText = "Unable to Contact_06"
            dgvFeeAuditReport.Columns("strUnableToContact_06").DisplayIndex = 44
            dgvFeeAuditReport.Columns("datNOVLetterSent_06").HeaderText = "NOV Sent_06"
            dgvFeeAuditReport.Columns("datNOVLetterSent_06").DisplayIndex = 45
            dgvFeeAuditReport.Columns("datNOVLetterSent_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datCOLetterSent_06").HeaderText = "CO Sent_06"
            dgvFeeAuditReport.Columns("datCOLetterSent_06").DisplayIndex = 46
            dgvFeeAuditReport.Columns("datCOLetterSent_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datAOLetterSent_06").HeaderText = "AO Sent_06"
            dgvFeeAuditReport.Columns("datAOLetterSent_06").DisplayIndex = 47
            dgvFeeAuditReport.Columns("datAOLetterSent_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_06").HeaderText = "Fees Paid_06"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_06").DisplayIndex = 48
            dgvFeeAuditReport.Columns("datFacilityPaidFees_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAmountPaid_06").HeaderText = "Amount Paid_06"
            dgvFeeAuditReport.Columns("strAmountPaid_06").DisplayIndex = 49
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_06").HeaderText = "Closed Out_06"
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_06").DisplayIndex = 50
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numStaffAssigned_06").HeaderText = "Staff ID_06"
            dgvFeeAuditReport.Columns("numStaffAssigned_06").DisplayIndex = 51
            dgvFeeAuditReport.Columns("datLastModified_06").HeaderText = "Managers ID_06"
            dgvFeeAuditReport.Columns("datLastModified_06").DisplayIndex = 52
            dgvFeeAuditReport.Columns("datLastModified_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numManagerSignOff_06").HeaderText = "Last Modified by Staff_06"
            dgvFeeAuditReport.Columns("numManagerSignOff_06").DisplayIndex = 53
            dgvFeeAuditReport.Columns("datManagerSignOff_06").HeaderText = "Last Modified by Manager_06"
            dgvFeeAuditReport.Columns("datManagerSignOff_06").DisplayIndex = 54
            dgvFeeAuditReport.Columns("datManagerSignOff_06").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strComments_06").HeaderText = "Comments_06"
            dgvFeeAuditReport.Columns("strComments_06").DisplayIndex = 55



            dgvFeeAuditReport.Columns("datInitialLetterMailed_05").HeaderText = "Initial Letter_05"
            dgvFeeAuditReport.Columns("datInitialLetterMailed_05").DisplayIndex = 56
            dgvFeeAuditReport.Columns("datInitialLetterMailed_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datLetterReturned_05").HeaderText = "Letter Returned_05"
            dgvFeeAuditReport.Columns("datLetterReturned_05").DisplayIndex = 57
            dgvFeeAuditReport.Columns("datLetterReturned_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAddressUnknown_05").HeaderText = "Address Unknown_05"
            dgvFeeAuditReport.Columns("strAddressUnknown_05").DisplayIndex = 58
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_05").HeaderText = "Letter Remailed_05"
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_05").DisplayIndex = 59
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strFacilityResponded_05").HeaderText = "Facility Responded w/ Corrections_05"
            dgvFeeAuditReport.Columns("strFacilityResponded_05").DisplayIndex = 60
            dgvFeeAuditReport.Columns("strFacilityBankrupt_05").HeaderText = "Facility Bankrupt_05"
            dgvFeeAuditReport.Columns("strFacilityBankrupt_05").DisplayIndex = 61
            dgvFeeAuditReport.Columns("strUnableToContact_05").HeaderText = "Unable to Contact_05"
            dgvFeeAuditReport.Columns("strUnableToContact_05").DisplayIndex = 62
            dgvFeeAuditReport.Columns("datNOVLetterSent_05").HeaderText = "NOV Sent_05"
            dgvFeeAuditReport.Columns("datNOVLetterSent_05").DisplayIndex = 63
            dgvFeeAuditReport.Columns("datNOVLetterSent_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datCOLetterSent_05").HeaderText = "CO Sent_05"
            dgvFeeAuditReport.Columns("datCOLetterSent_05").DisplayIndex = 64
            dgvFeeAuditReport.Columns("datCOLetterSent_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datAOLetterSent_05").HeaderText = "AO Sent_05"
            dgvFeeAuditReport.Columns("datAOLetterSent_05").DisplayIndex = 65
            dgvFeeAuditReport.Columns("datAOLetterSent_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_05").HeaderText = "Fees Paid_05"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_05").DisplayIndex = 66
            dgvFeeAuditReport.Columns("datFacilityPaidFees_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAmountPaid_05").HeaderText = "Amount Paid_05"
            dgvFeeAuditReport.Columns("strAmountPaid_05").DisplayIndex = 67
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_05").HeaderText = "Closed Out_05"
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_05").DisplayIndex = 68
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numStaffAssigned_05").HeaderText = "Staff ID_05"
            dgvFeeAuditReport.Columns("numStaffAssigned_05").DisplayIndex = 69
            dgvFeeAuditReport.Columns("datLastModified_05").HeaderText = "Last Modified by Staff_05"
            dgvFeeAuditReport.Columns("datLastModified_05").DisplayIndex = 70
            dgvFeeAuditReport.Columns("datLastModified_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numManagerSignOff_05").HeaderText = "Managers ID_05"
            dgvFeeAuditReport.Columns("numManagerSignOff_05").DisplayIndex = 71
            dgvFeeAuditReport.Columns("datManagerSignOff_05").HeaderText = "Last Modified by Manager_05"
            dgvFeeAuditReport.Columns("datManagerSignOff_05").DisplayIndex = 72
            dgvFeeAuditReport.Columns("datManagerSignOff_05").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strComments_05").HeaderText = "Comments_05"
            dgvFeeAuditReport.Columns("strComments_05").DisplayIndex = 73
            dgvFeeAuditReport.Columns("datInitialLetterMailed_04").HeaderText = "Initial Letter_04"
            dgvFeeAuditReport.Columns("datInitialLetterMailed_04").DisplayIndex = 74
            dgvFeeAuditReport.Columns("datInitialLetterMailed_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datLetterReturned_04").HeaderText = "Letter Returned_04"
            dgvFeeAuditReport.Columns("datLetterReturned_04").DisplayIndex = 75
            dgvFeeAuditReport.Columns("datLetterReturned_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAddressUnknown_04").HeaderText = "Address Unknown_04"
            dgvFeeAuditReport.Columns("strAddressUnknown_04").DisplayIndex = 76
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_04").HeaderText = "Letter Remailed_04"
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_04").DisplayIndex = 77
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strFacilityResponded_04").HeaderText = "Facility Responded w/ Corrections_04"
            dgvFeeAuditReport.Columns("strFacilityResponded_04").DisplayIndex = 78
            dgvFeeAuditReport.Columns("strFacilityBankrupt_04").HeaderText = "Facility Bankrupt_04"
            dgvFeeAuditReport.Columns("strFacilityBankrupt_04").DisplayIndex = 79
            dgvFeeAuditReport.Columns("strUnableToContact_04").HeaderText = "Unable to Contact_04"
            dgvFeeAuditReport.Columns("strUnableToContact_04").DisplayIndex = 80
            dgvFeeAuditReport.Columns("datNOVLetterSent_04").HeaderText = "NOV Sent_04"
            dgvFeeAuditReport.Columns("datNOVLetterSent_04").DisplayIndex = 81
            dgvFeeAuditReport.Columns("datNOVLetterSent_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datCOLetterSent_04").HeaderText = "CO Sent_04"
            dgvFeeAuditReport.Columns("datCOLetterSent_04").DisplayIndex = 82
            dgvFeeAuditReport.Columns("datCOLetterSent_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datAOLetterSent_04").HeaderText = "AO Sent_04"
            dgvFeeAuditReport.Columns("datAOLetterSent_04").DisplayIndex = 83
            dgvFeeAuditReport.Columns("datAOLetterSent_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_04").HeaderText = "Fees Paid_04"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_04").DisplayIndex = 84
            dgvFeeAuditReport.Columns("datFacilityPaidFees_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAmountPaid_04").HeaderText = "Amount Paid_04"
            dgvFeeAuditReport.Columns("strAmountPaid_04").DisplayIndex = 85
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_04").HeaderText = "Closed Out_04"
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_04").DisplayIndex = 86
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numStaffAssigned_04").HeaderText = "Staff ID_04"
            dgvFeeAuditReport.Columns("numStaffAssigned_04").DisplayIndex = 87
            dgvFeeAuditReport.Columns("datLastModified_04").HeaderText = "Last Modified by Staff_04"
            dgvFeeAuditReport.Columns("datLastModified_04").DisplayIndex = 88
            dgvFeeAuditReport.Columns("datLastModified_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numManagerSignOff_04").HeaderText = "Managers ID_04"
            dgvFeeAuditReport.Columns("numManagerSignOff_04").DisplayIndex = 89
            dgvFeeAuditReport.Columns("datManagerSignOff_04").HeaderText = "Last Modified by Manager_04"
            dgvFeeAuditReport.Columns("datManagerSignOff_04").DisplayIndex = 90
            dgvFeeAuditReport.Columns("datManagerSignOff_04").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strComments_04").HeaderText = "Comments_04"
            dgvFeeAuditReport.Columns("strComments_04").DisplayIndex = 91



            dgvFeeAuditReport.Columns("datInitialLetterMailed_03").HeaderText = "Initial Letter_03"
            dgvFeeAuditReport.Columns("datInitialLetterMailed_03").DisplayIndex = 92
            dgvFeeAuditReport.Columns("datInitialLetterMailed_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datLetterReturned_03").HeaderText = "Letter Returned_03"
            dgvFeeAuditReport.Columns("datLetterReturned_03").DisplayIndex = 93
            dgvFeeAuditReport.Columns("datLetterReturned_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAddressUnknown_03").HeaderText = "Address Unknown_03"
            dgvFeeAuditReport.Columns("strAddressUnknown_03").DisplayIndex = 94
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_03").HeaderText = "Letter Remailed_03"
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_03").DisplayIndex = 95
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strFacilityResponded_03").HeaderText = "Facility Responded w/ Corrections_03"
            dgvFeeAuditReport.Columns("strFacilityResponded_03").DisplayIndex = 96
            dgvFeeAuditReport.Columns("strFacilityBankrupt_03").HeaderText = "Facility Bankrupt_03"
            dgvFeeAuditReport.Columns("strFacilityBankrupt_03").DisplayIndex = 97
            dgvFeeAuditReport.Columns("strUnableToContact_03").HeaderText = "Unable to Contact_03"
            dgvFeeAuditReport.Columns("strUnableToContact_03").DisplayIndex = 98
            dgvFeeAuditReport.Columns("datNOVLetterSent_03").HeaderText = "NOV Sent_03"
            dgvFeeAuditReport.Columns("datNOVLetterSent_03").DisplayIndex = 99
            dgvFeeAuditReport.Columns("datNOVLetterSent_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datCOLetterSent_03").HeaderText = "CO Sent_03"
            dgvFeeAuditReport.Columns("datCOLetterSent_03").DisplayIndex = 100
            dgvFeeAuditReport.Columns("datCOLetterSent_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datAOLetterSent_03").HeaderText = "AO Sent_03"
            dgvFeeAuditReport.Columns("datAOLetterSent_03").DisplayIndex = 101
            dgvFeeAuditReport.Columns("datAOLetterSent_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_03").HeaderText = "Fees Paid_03"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_03").DisplayIndex = 102
            dgvFeeAuditReport.Columns("datFacilityPaidFees_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAmountPaid_03").HeaderText = "Amount Paid_03"
            dgvFeeAuditReport.Columns("strAmountPaid_03").DisplayIndex = 103
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_03").HeaderText = "Closed Out_03"
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_03").DisplayIndex = 104
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numStaffAssigned_03").HeaderText = "Staff ID_03"
            dgvFeeAuditReport.Columns("numStaffAssigned_03").DisplayIndex = 105
            dgvFeeAuditReport.Columns("datLastModified_03").HeaderText = "Last Modified by Staff_03"
            dgvFeeAuditReport.Columns("datLastModified_03").DisplayIndex = 106
            dgvFeeAuditReport.Columns("datLastModified_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numManagerSignOff_03").HeaderText = "Managers ID_03"
            dgvFeeAuditReport.Columns("numManagerSignOff_03").DisplayIndex = 107
            dgvFeeAuditReport.Columns("datManagerSignOff_03").HeaderText = "Last Modified by Manager_03"
            dgvFeeAuditReport.Columns("datManagerSignOff_03").DisplayIndex = 108
            dgvFeeAuditReport.Columns("datManagerSignOff_03").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strComments_03").HeaderText = "Comments_03"
            dgvFeeAuditReport.Columns("strComments_03").DisplayIndex = 109


            dgvFeeAuditReport.Columns("datInitialLetterMailed_02").HeaderText = "Initial Letter_02"
            dgvFeeAuditReport.Columns("datInitialLetterMailed_02").DisplayIndex = 110
            dgvFeeAuditReport.Columns("datInitialLetterMailed_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datLetterReturned_02").HeaderText = "Letter Returned_02"
            dgvFeeAuditReport.Columns("datLetterReturned_02").DisplayIndex = 111
            dgvFeeAuditReport.Columns("datLetterReturned_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAddressUnknown_02").HeaderText = "Address Unknown_02"
            dgvFeeAuditReport.Columns("strAddressUnknown_02").DisplayIndex = 112
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_02").HeaderText = "Letter Remailed_02"
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_02").DisplayIndex = 113
            dgvFeeAuditReport.Columns("datInitialLetterRemailed_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strFacilityResponded_02").HeaderText = "Facility Responded w/ Corrections_02"
            dgvFeeAuditReport.Columns("strFacilityResponded_02").DisplayIndex = 114
            dgvFeeAuditReport.Columns("strFacilityBankrupt_02").HeaderText = "Facility Bankrupt_02"
            dgvFeeAuditReport.Columns("strFacilityBankrupt_02").DisplayIndex = 115
            dgvFeeAuditReport.Columns("strUnableToContact_02").HeaderText = "Unable to Contact_02"
            dgvFeeAuditReport.Columns("strUnableToContact_02").DisplayIndex = 116
            dgvFeeAuditReport.Columns("datNOVLetterSent_02").HeaderText = "NOV Sent_02"
            dgvFeeAuditReport.Columns("datNOVLetterSent_02").DisplayIndex = 117
            dgvFeeAuditReport.Columns("datNOVLetterSent_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datCOLetterSent_02").HeaderText = "CO Sent_02"
            dgvFeeAuditReport.Columns("datCOLetterSent_02").DisplayIndex = 118
            dgvFeeAuditReport.Columns("datCOLetterSent_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datAOLetterSent_02").HeaderText = "AO Sent_02"
            dgvFeeAuditReport.Columns("datAOLetterSent_02").DisplayIndex = 119
            dgvFeeAuditReport.Columns("datAOLetterSent_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_02").HeaderText = "Fees Paid_02"
            dgvFeeAuditReport.Columns("datFacilityPaidFees_02").DisplayIndex = 120
            dgvFeeAuditReport.Columns("datFacilityPaidFees_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strAmountPaid_02").HeaderText = "Amount Paid_02"
            dgvFeeAuditReport.Columns("strAmountPaid_02").DisplayIndex = 121
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_02").HeaderText = "Closed Out_02"
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_02").DisplayIndex = 122
            dgvFeeAuditReport.Columns("datClosedOutFeeAudit_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numStaffAssigned_02").HeaderText = "Staff ID_02"
            dgvFeeAuditReport.Columns("numStaffAssigned_02").DisplayIndex = 123
            dgvFeeAuditReport.Columns("datLastModified_02").HeaderText = "Last Modified by Staff_02"
            dgvFeeAuditReport.Columns("datLastModified_02").DisplayIndex = 124
            dgvFeeAuditReport.Columns("datLastModified_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("numManagerSignOff_02").HeaderText = "Managers ID_02"
            dgvFeeAuditReport.Columns("numManagerSignOff_02").DisplayIndex = 125
            dgvFeeAuditReport.Columns("datManagerSignOff_02").HeaderText = "Last Modified by Manager_02"
            dgvFeeAuditReport.Columns("datManagerSignOff_02").DisplayIndex = 126
            dgvFeeAuditReport.Columns("datManagerSignOff_02").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeAuditReport.Columns("strComments_02").HeaderText = "Comments_02"
            dgvFeeAuditReport.Columns("strComments_02").DisplayIndex = 127

            dgvFeeAuditReport.Columns("strComments").HeaderText = "Additional Comments"
            dgvFeeAuditReport.Columns("strComments").DisplayIndex = 128

            txtCount.Text = dgvFeeAuditReport.RowCount.ToString
            '  Exit Sub
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ExportToExcel()
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As New Excel.Application
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvFeeAuditReport.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvFeeAuditReport.ColumnCount - 1
                        .Cells(1, i + 1) = dgvFeeAuditReport.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvFeeAuditReport.ColumnCount - 1
                        For j = 0 To dgvFeeAuditReport.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvFeeAuditReport.Item(i, j).Value.ToString
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
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        ExportToExcel()
    End Sub
    Private Sub btnViewAllNonPayerData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewAllNonPayerData.Click
        Try
            Dim SQLLine As String = ""

            If rdbInactive.Checked = True Then
                SQLLine = "and strActive = 'False' "
            End If
            If rdbActive.Checked = True Then
                SQLLine = "and (strActive = 'True' or strActive is null) "
            End If
            If rdbActiveAll.Checked = True Then
                SQLLine = " "
            End If
           
            SQL = "Select " & _
"NUMNONPAYERSID, " & _
"STRAIRSNUMBER, " & _
"STRFACILITYNAME, " & _
"STRFACILITYSTREET, " & _
"STRFACILITYCITY, " & _
"STRFACILITYSTATE, " & _
"STRFACILITYZIPCODE, " & _
"STROPERATIONALSTATUS, " & _
"STRCLASSIFICATION, " & _
"STRSICCODE, " & _
"DATSHUTDOWNDATE, " & _
"STRNSPSSTATUS, " & _
"STRTVSTATUS, " & _
"STRSTATUS_08, " & _
"STRTOTALDUE_08, " & _
"STRTOTALPAID_08, " & _
"STRBALANCE_08, " & _
"STRSTATUS_07, " & _
"STRTOTALDUE_07, " & _
"STRTOTALPAID_07, " & _
"STRBALANCE_07, " & _
"STRSTATUS_06, " & _
"STRTOTALDUE_06, " & _
"STRTOTALPAID_06, " & _
"STRBALANCE_06, " & _
"STRSTATUS_05, " & _
"STRTOTALDUE_05, " & _
"STRTOTALPAID_05, " & _
"STRBALANCE_05, " & _
"STRSTATUS_04, " & _
"STRTOTALDUE_04, " & _
"STRTOTALPAID_04, " & _
"STRBALANCE_04, " & _
"STRSTATUS_03, " & _
"STRTOTALDUE_03, " & _
"STRTOTALPAID_03, " & _
"STRBALANCE_03, " & _
"STRSTATUS_02, " & _
"STRTOTALDUE_02, " & _
"STRTOTALPAID_02, " & _
"STRBALANCE_02, " & _
"STRCONTACTFIRSTNAME, " & _
"STRCONTACTLASTNAME, " & _
"STRCONTACTTITLE, " & _
"STRCONTACTCOMPANYNAME,  " & _
"STRCONTACTPHONENUMBER , " & _
"STRCONTACTFAXNUMBER, " & _
"STRCONTACTEMAIL, " & _
"STRCONTACTADDRESS, " & _
"STRCONTACTCITY, " & _
"STRCONTACTSTATE, " & _
"STRCONTACTZIPCODE, " & _
"STRCONTACTDESCRIPTION, " & _
"(strLastName||', '||strFirstName) as Staff,  " & _
"strActive " & _
"from AIRBranch.Fee_NonPayers_2010, AIRBranch.EPDUSerProfiles  " & _
"where AIRBranch.Fee_NonPayers_2010.numStaffResponsible = AIRBranch.EPDUSerProfiles.numUserID (+)  " & _
SQLLine & _
"order by strAIRSNumber "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "NonPayers")
            dgvFeeAuditReport.DataSource = ds
            dgvFeeAuditReport.DataMember = "NonPayers"

            dgvFeeAuditReport.RowHeadersVisible = False
            dgvFeeAuditReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeAuditReport.AllowUserToResizeColumns = True
            dgvFeeAuditReport.AllowUserToAddRows = False
            dgvFeeAuditReport.AllowUserToDeleteRows = False
            dgvFeeAuditReport.AllowUserToOrderColumns = True
            dgvFeeAuditReport.AllowUserToResizeRows = True
            dgvFeeAuditReport.ColumnHeadersHeight = "35"

            dgvFeeAuditReport.Columns("NUMNONPAYERSID").HeaderText = "ID"
            dgvFeeAuditReport.Columns("NUMNONPAYERSID").DisplayIndex = 0
            dgvFeeAuditReport.Columns("NUMNONPAYERSID").Width = 50
            dgvFeeAuditReport.Columns("strAIRSNumber").HeaderText = "AIRS #"
            dgvFeeAuditReport.Columns("strAIRSNumber").DisplayIndex = 1
            dgvFeeAuditReport.Columns("strAIRSNumber").Width = 75

            dgvFeeAuditReport.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeAuditReport.Columns("strFacilityName").DisplayIndex = 2
            dgvFeeAuditReport.Columns("strFacilityStreet").HeaderText = "Facility Street"
            dgvFeeAuditReport.Columns("strFacilityStreet").DisplayIndex = 3
            dgvFeeAuditReport.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeAuditReport.Columns("STRFACILITYCITY").DisplayIndex = 4
            dgvFeeAuditReport.Columns("STRFACILITYSTATE").HeaderText = "Facility State"
            dgvFeeAuditReport.Columns("STRFACILITYSTATE").DisplayIndex = 5
            dgvFeeAuditReport.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeAuditReport.Columns("STRFACILITYZIPCODE").DisplayIndex = 6
            dgvFeeAuditReport.Columns("STROPERATIONALSTATUS").HeaderText = "Op Status"
            dgvFeeAuditReport.Columns("STROPERATIONALSTATUS").DisplayIndex = 7
            dgvFeeAuditReport.Columns("STRCLASSIFICATION").HeaderText = "Classification"
            dgvFeeAuditReport.Columns("STRCLASSIFICATION").DisplayIndex = 8
            dgvFeeAuditReport.Columns("STRSICCODE").HeaderText = "SIC Code"
            dgvFeeAuditReport.Columns("STRSICCODE").DisplayIndex = 9
            dgvFeeAuditReport.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
            dgvFeeAuditReport.Columns("DATSHUTDOWNDATE").DisplayIndex = 10
            dgvFeeAuditReport.Columns("STRNSPSSTATUS").HeaderText = "NSPS Status"
            dgvFeeAuditReport.Columns("STRNSPSSTATUS").DisplayIndex = 11
            dgvFeeAuditReport.Columns("STRTVSTATUS").HeaderText = "TV Status"
            dgvFeeAuditReport.Columns("STRTVSTATUS").DisplayIndex = 12
            dgvFeeAuditReport.Columns("STRSTATUS_08").HeaderText = "Status_08"
            dgvFeeAuditReport.Columns("STRSTATUS_08").DisplayIndex = 13
            dgvFeeAuditReport.Columns("STRTOTALDUE_08").HeaderText = "Total Due_08"
            dgvFeeAuditReport.Columns("STRTOTALDUE_08").DisplayIndex = 14
            dgvFeeAuditReport.Columns("STRTOTALPAID_08").HeaderText = "Total Paid_08"
            dgvFeeAuditReport.Columns("STRTOTALPAID_08").DisplayIndex = 15
            dgvFeeAuditReport.Columns("STRBALANCE_08").HeaderText = "Balance_08"
            dgvFeeAuditReport.Columns("STRBALANCE_08").DisplayIndex = 16
            dgvFeeAuditReport.Columns("STRSTATUS_07").HeaderText = "Status_07"
            dgvFeeAuditReport.Columns("STRSTATUS_07").DisplayIndex = 17
            dgvFeeAuditReport.Columns("STRTOTALDUE_07").HeaderText = "Total Due_07"
            dgvFeeAuditReport.Columns("STRTOTALDUE_07").DisplayIndex = 18
            dgvFeeAuditReport.Columns("STRTOTALPAID_07").HeaderText = "Total Paid_07"
            dgvFeeAuditReport.Columns("STRTOTALPAID_07").DisplayIndex = 19
            dgvFeeAuditReport.Columns("STRBALANCE_07").HeaderText = "Balance_07"
            dgvFeeAuditReport.Columns("STRBALANCE_07").DisplayIndex = 20
            dgvFeeAuditReport.Columns("STRSTATUS_06").HeaderText = "Status_06"
            dgvFeeAuditReport.Columns("STRSTATUS_06").DisplayIndex = 21
            dgvFeeAuditReport.Columns("STRTOTALDUE_06").HeaderText = "Total Due_06"
            dgvFeeAuditReport.Columns("STRTOTALDUE_06").DisplayIndex = 22
            dgvFeeAuditReport.Columns("STRTOTALPAID_06").HeaderText = "Total Paid_06"
            dgvFeeAuditReport.Columns("STRTOTALPAID_06").DisplayIndex = 23
            dgvFeeAuditReport.Columns("STRBALANCE_06").HeaderText = "Balance_06"
            dgvFeeAuditReport.Columns("STRBALANCE_06").DisplayIndex = 24
            dgvFeeAuditReport.Columns("STRSTATUS_05").HeaderText = "Status_05"
            dgvFeeAuditReport.Columns("STRSTATUS_05").DisplayIndex = 25
            dgvFeeAuditReport.Columns("STRTOTALDUE_05").HeaderText = "Total Due_05"
            dgvFeeAuditReport.Columns("STRTOTALDUE_05").DisplayIndex = 26
            dgvFeeAuditReport.Columns("STRTOTALPAID_05").HeaderText = "Total Paid_05"
            dgvFeeAuditReport.Columns("STRTOTALPAID_05").DisplayIndex = 27
            dgvFeeAuditReport.Columns("STRBALANCE_05").HeaderText = "Balance_05"
            dgvFeeAuditReport.Columns("STRBALANCE_05").DisplayIndex = 28
            dgvFeeAuditReport.Columns("STRSTATUS_04").HeaderText = "Status_04"
            dgvFeeAuditReport.Columns("STRSTATUS_04").DisplayIndex = 29
            dgvFeeAuditReport.Columns("STRTOTALDUE_04").HeaderText = "Total Due_04"
            dgvFeeAuditReport.Columns("STRTOTALDUE_04").DisplayIndex = 30
            dgvFeeAuditReport.Columns("STRTOTALPAID_04").HeaderText = "Total Paid_04"
            dgvFeeAuditReport.Columns("STRTOTALPAID_04").DisplayIndex = 31
            dgvFeeAuditReport.Columns("STRBALANCE_04").HeaderText = "Balance_04"
            dgvFeeAuditReport.Columns("STRBALANCE_04").DisplayIndex = 32
            dgvFeeAuditReport.Columns("STRSTATUS_03").HeaderText = "Status_03"
            dgvFeeAuditReport.Columns("STRSTATUS_03").DisplayIndex = 33
            dgvFeeAuditReport.Columns("STRTOTALDUE_03").HeaderText = "Total Due_03"
            dgvFeeAuditReport.Columns("STRTOTALDUE_03").DisplayIndex = 34
            dgvFeeAuditReport.Columns("STRTOTALPAID_03").HeaderText = "Total Paid_03"
            dgvFeeAuditReport.Columns("STRTOTALPAID_03").DisplayIndex = 35
            dgvFeeAuditReport.Columns("STRBALANCE_03").HeaderText = "Balance_03"
            dgvFeeAuditReport.Columns("STRBALANCE_03").DisplayIndex = 36
            dgvFeeAuditReport.Columns("STRSTATUS_02").HeaderText = "Status_02"
            dgvFeeAuditReport.Columns("STRSTATUS_02").DisplayIndex = 37
            dgvFeeAuditReport.Columns("STRTOTALDUE_02").HeaderText = "Total Due_02"
            dgvFeeAuditReport.Columns("STRTOTALDUE_02").DisplayIndex = 38
            dgvFeeAuditReport.Columns("STRTOTALPAID_02").HeaderText = "Total Paid_02"
            dgvFeeAuditReport.Columns("STRTOTALPAID_02").DisplayIndex = 39
            dgvFeeAuditReport.Columns("STRBALANCE_02").HeaderText = "Balance_02"
            dgvFeeAuditReport.Columns("STRBALANCE_02").DisplayIndex = 40
            dgvFeeAuditReport.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeAuditReport.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 41
            dgvFeeAuditReport.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeAuditReport.Columns("STRCONTACTLASTNAME").DisplayIndex = 42
            dgvFeeAuditReport.Columns("STRCONTACTTITLE").HeaderText = "Contact Title"
            dgvFeeAuditReport.Columns("STRCONTACTTITLE").DisplayIndex = 43
            dgvFeeAuditReport.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeAuditReport.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 44
            dgvFeeAuditReport.Columns("STRCONTACTPHONENUMBER").HeaderText = "Contact Phone Number"
            dgvFeeAuditReport.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 45
            dgvFeeAuditReport.Columns("STRCONTACTFAXNUMBER").HeaderText = "Contact Fax Number"
            dgvFeeAuditReport.Columns("STRCONTACTFAXNUMBER").DisplayIndex = 46
            dgvFeeAuditReport.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeAuditReport.Columns("STRCONTACTEMAIL").DisplayIndex = 47
            dgvFeeAuditReport.Columns("STRCONTACTADDRESS").HeaderText = "Contact Address"
            dgvFeeAuditReport.Columns("STRCONTACTADDRESS").DisplayIndex = 48
            dgvFeeAuditReport.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvFeeAuditReport.Columns("STRCONTACTCITY").DisplayIndex = 49
            dgvFeeAuditReport.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvFeeAuditReport.Columns("STRCONTACTSTATE").DisplayIndex = 50
            dgvFeeAuditReport.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip Code"
            dgvFeeAuditReport.Columns("STRCONTACTZIPCODE").DisplayIndex = 51
            dgvFeeAuditReport.Columns("STRCONTACTDESCRIPTION").HeaderText = "Contact Desc."
            dgvFeeAuditReport.Columns("STRCONTACTDESCRIPTION").DisplayIndex = 52
            dgvFeeAuditReport.Columns("Staff").HeaderText = "Staff"
            dgvFeeAuditReport.Columns("Staff").DisplayIndex = 53
            dgvFeeAuditReport.Columns("strActive").HeaderText = "Active"
            dgvFeeAuditReport.Columns("strActive").DisplayIndex = 54

            txtCount.Text = dgvFeeAuditReport.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnGetEmailAddresses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailAddresses.Click
        Try
            Dim i As Integer
            Dim SQLLine As String = ""

            For i = 0 To dgvFeeAuditReport.RowCount - 1
                SQLLine = SQLLine & " strAIRSNumber = '0413" & dgvFeeAuditReport(1, i).Value & "' or "
            Next

            SQL = "Select distinct * from " & _
            "(Select distinct " & _
            "substr(strAIRSNumber, 5) as AIRSNumber, strContactEmail " & _
            "from " & connNameSpace & ".APBContactInformation " & _
            "where strContactEmail is not null " & _
            "and strContactEmail <> 'N/A' " & _
            "and Upper(strContactEmail) <> 'NO@EMAIL.COM' " & _
            "and Upper(strContactEmail) not like '%@DNR.STATE.GA.US' " & _
            "and strContactEmail <> ' ' " & _
            "and (" & Mid(SQLLine, 1, SQLLine.Length - 3) & " ) " & _
            "union " & _
            "Select distinct " & _
            "substr(strAIRSNumber, 5) as AIRSNumber, strContactEmail " & _
            "from " & connNameSpace & ".FSContactInfo " & _
            "where Upper(strContactEmail) not like '%N/A%' " & _
            "and Upper(strContactEmail) not like '%@DNR.STATE.GA.US%' " & _
            "AND (" & Mid(SQLLine, 1, SQLLine.Length - 3) & " )) "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            da.Fill(ds, "Emails")
            dgvFeeAuditReport.DataSource = ds
            dgvFeeAuditReport.DataMember = "Emails"

            dgvFeeAuditReport.RowHeadersVisible = False
            dgvFeeAuditReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeAuditReport.AllowUserToResizeColumns = True
            dgvFeeAuditReport.AllowUserToAddRows = False
            dgvFeeAuditReport.AllowUserToDeleteRows = False
            dgvFeeAuditReport.AllowUserToOrderColumns = True
            dgvFeeAuditReport.AllowUserToResizeRows = True
            dgvFeeAuditReport.ColumnHeadersHeight = "35"

            dgvFeeAuditReport.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvFeeAuditReport.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeAuditReport.Columns("AIRSNumber").Width = 75
            dgvFeeAuditReport.Columns("strContactEmail").HeaderText = "Email Address"
            dgvFeeAuditReport.Columns("strContactEmail").DisplayIndex = 1
            dgvFeeAuditReport.Columns("strContactEmail").Width = 250

            txtCount.Text = dgvFeeAuditReport.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFlagNonPayer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlagNonPayer.Click
        Try
            Dim ActiveStatus As String = ""
            If txtNonPayerID.Text = "" Then
                Exit Sub
            End If

            If rdbNonPayerInactive.Checked = True Then
                ActiveStatus = "False"
            Else
                ActiveStatus = "True"
            End If

            SQL = "Update " & connNameSpace & ".Fee_NonPayers_2010 set " & _
            "strActive = '" & ActiveStatus & "' " & _
            "where numNonPayersID = '" & txtNonPayerID.Text & "' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            MsgBox("Status Saved", MsgBoxStyle.Information, "Fee Audit Tool")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchForData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchForData.Click
        Try
            If mtbAIRSNumber.Text <> "" Then
                lblFacilityNameTop.Text = "Facility Name: BAD AIRS #"
                ResetForm()

                SQL = "Select " & _
                "strFacilityName " & _
                "from " & connNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        lblFacilityNameTop.Text = "Facility Name: BAD AIRS #"
                        Exit Sub
                    Else
                        lblFacilityNameTop.Text = dr.Item("strFacilityName")
                    End If
                End While
                dr.Close()

                TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2008)
                TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2007)
                TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2006)
                TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2005)
                TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2004)
                TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2003)
                TCFeeAuditTracking.TabPages.Remove(TP_Tracking_CY2002)
                TCFeeAuditTracking.TabPages.Remove(TP_Tracking_OtherComments)

                LoadDataByAIRS()
                LoadNonPayer()
                LoadAuditData()

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveNonPayerComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNonPayerComments.Click
        Try
            SQL = "Update " & connNameSpace & ".Fee_NonPayers_2010 set " & _
            "strComments = '" & Replace(txtNonPayersComments.Text, "'", "''") & "' " & _
            "where numNonpayersId = '" & txtNonPayerID.Text & "' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Comments Saved", MsgBoxStyle.Exclamation, "Fee Audit Tool")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveAndUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAndUpdate.Click
        Try
            If GBContactInformation.Visible = False Then
                GBContactInformation.Visible = True
            Else
                GBContactInformation.Visible = False
            End If

            'txtAuditComments.Text = txtAuditComments.Text & vbCrLf & _
            '"Original Contact Information for Non-Payer" & vbCrLf & _
            'txtNonPayerFirstname.Text & " " & txtNonPayerLastName.Text & vbCrLf & _
            'txtNonPayerTitle.Text & vbCrLf & _
            'txtNonPayerCompany.Text & vbCrLf & _
            'txtNonPayerAddress.Text & vbCrLf & _
            'txtNonPayerCity.Text & ", " & txtNonPayerState.Text & " " & mtbNonPayerZipCode.Text & vbCrLf & _
            'txtNonPayerPhoneNumber.Text & vbCrLf & _
            'txtNonPayerEmail.Text

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNonPayerSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNonPayerSave.Click
        Try
            SQL = "Update " & connNameSpace & ".Fee_NonPayers_2010 set " & _
            "strContactFirstName = '" & Replace(txtNonPayerFirstname.Text, "'", "''") & "', " & _
            "strContactLastname = '" & Replace(txtNonPayerLastName.Text, "'", "''") & "', " & _
            "strContactTitle = '" & Replace(txtNonPayerTitle.Text, "'", "''") & "', " & _
            "strContactCompanyName = '" & Replace(txtNonPayerCompany.Text, "'", "''") & "', " & _
            "strContactPhoneNumber = '" & Replace(txtNonPayerPhoneNumber.Text, "'", "''") & "', " & _
            "strContactEmail = '" & Replace(txtNonPayerEmail.Text, "'", "''") & "', " & _
            "strContactAddress = '" & Replace(txtNonPayerAddress.Text, "'", "''") & "', " & _
            "strContactCity = '" & Replace(txtNonPayerCity.Text, "'", "''") & "', " & _
            "strContactState = '" & Replace(txtNonPayerState.Text, "'", "''") & "', " & _
            "strContactZipCode = '" & Replace(mtbNonPayerZipCode.Text, "'", "''") & "' " & _
            "where strAIRSnumber = '" & Replace(mtbAIRSNumber.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Data Saved", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunStats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunStats.Click
        Try
            SQL = "select count(*) as TotalSent " & _
            "from " & connNameSpace & ".Fee_Audit_2010 "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("TotalSent")) Then
                    txtTotalLetterSent.Text = "0"
                Else
                    txtTotalLetterSent.Text = dr.Item("TotalSent")
                End If
            End While
            dr.Close()

            SQL = "select count(*) as NonPayerSent " & _
            "from " & connNameSpace & ".Fee_Audit_2010 " & _
            "where exists (select * " & _
            "from " & connNameSpace & ".Fee_NonPayers_2010 " & _
            "where " & connNameSpace & ".Fee_Audit_2010.strAIRSNumber = " & connNameSpace & ".Fee_NonPayers_2010.strAIRSNumber) "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("NonPayerSent")) Then
                    txtNonPayerSent.Text = "0"
                Else
                    txtNonPayerSent.Text = dr.Item("NonPayerSent")
                End If
            End While
            dr.Close()

            SQL = "select count(*) as NonRespondersSent " & _
           "from " & connNameSpace & ".Fee_Audit_2010 " & _
           "where exists (select * " & _
           "from " & connNameSpace & ".Fee_NonResponders_2010 " & _
           "where " & connNameSpace & ".Fee_Audit_2010.strAIRSNumber = " & connNameSpace & ".Fee_NonResponders_2010.strAIRSNumber) "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("NonRespondersSent")) Then
                    txtNonRespondersSent.Text = "0"
                Else
                    txtNonRespondersSent.Text = dr.Item("NonRespondersSent")
                End If
            End While
            dr.Close()

            SQL = "select count(*) as NotResponding " & _
            "from " & connNameSpace & ".Fee_Audit_2010 " & _
            "where strUnableToContact_08 = 'True' " & _
            "or strUnableToContact_07 = 'True' " & _
            "or strUnableToContact_06 = 'True' " & _
            "or strUnableToContact_05 = 'True' " & _
            "or strUnableToContact_04 = 'True' " & _
            "or strUnableToContact_03 = 'True' " & _
            "or strUnableToContact_02 = 'True'  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("NotResponding")) Then
                    txtPossibleNOV.Text = "0"
                Else
                    txtPossibleNOV.Text = dr.Item("NotResponding")
                End If
            End While
            dr.Close()

            SQL = "select count(*) as ClosedAudits " & _
            "from " & connNameSpace & ".Fee_Audit_2010 " & _
            "where datClosedOutFeeAudit_08 is not NULL " & _
            "or datClosedOutFeeAudit_07 is not NULL " & _
            "or datClosedOutFeeAudit_06 is not NULL " & _
            "or datClosedOutFeeAudit_05 is not NULL " & _
            "or datClosedOutFeeAudit_04 is not NULL " & _
            "or datClosedOutFeeAudit_03 is not NULL " & _
            "or datClosedOutFeeAudit_02 is not NULL  "


            SQL = "select count(*) as ClosedAudits  " & _
            "from " & connNameSpace & ".Fee_Audit_2010  " & _
            "where (datInitialLetterMailed_08 is null or " & _
            "(datInitialLetterMailed_08 is not null and datClosedOutFeeAudit_08 is not null)) " & _
            "and (datInitialLetterMailed_07 is null or " & _
            "(datInitialLetterMailed_07 is not null and datClosedOutFeeAudit_07 is not null))  " & _
            "and (datInitialLetterMailed_06 is null or " & _
            "(datInitialLetterMailed_06 is not null and datClosedOutFeeAudit_06 is not null))  " & _
            "and (datInitialLetterMailed_05 is null or " & _
            "(datInitialLetterMailed_05 is not null and datClosedOutFeeAudit_05 is not null))  " & _
            "and (datInitialLetterMailed_04 is null or " & _
            "(datInitialLetterMailed_04 is not null and datClosedOutFeeAudit_04 is not null))  " & _
            "and (datInitialLetterMailed_03 is null or " & _
            "(datInitialLetterMailed_03 is not null and datClosedOutFeeAudit_03 is not null))  " & _
            "and (datInitialLetterMailed_02 is null or " & _
            "(datInitialLetterMailed_02 is not null and datClosedOutFeeAudit_02 is not null))  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("ClosedAudits")) Then
                    txtClosedOutAudits.Text = "0"
                Else
                    txtClosedOutAudits.Text = dr.Item("ClosedAudits")
                End If
            End While
            dr.Close()

            SQL = "select " & _
            "count(*) as OpenAudtis   " & _
            "from AIRBranch.Fee_Audit_2010   " & _
            "where (datInitialLetterMailed_08 is not null and datclosedoutfeeaudit_08 is null  " & _
            "or datInitialLetterMailed_07 is not null and datclosedoutfeeaudit_07 is null  " & _
            "or datInitialLetterMailed_06 is not null and datclosedoutfeeaudit_06 is null  " & _
            "or datInitialLetterMailed_05 is not null and  datclosedoutfeeaudit_05 is null  " & _
            "or datInitialLetterMailed_04 is not null and  datclosedoutfeeaudit_04 is null  " & _
            "or datInitialLetterMailed_03 is not null and  datclosedoutfeeaudit_03 is null  " & _
            "or datInitialLetterMailed_02 is not null and  datclosedoutfeeaudit_02 is null ) "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("OpenAudtis")) Then
                    txtOpenAudits.Text = "0"
                Else
                    txtOpenAudits.Text = dr.Item("OpenAudtis")
                End If
            End While
            dr.Close()



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCopyData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyData.Click
        Try
            txtAuditComments.Text = txtAuditComments.Text & vbCrLf & _
         "Original Contact Information for Non-Payer" & vbCrLf & _
         txtNonPayerFirstname.Text & " " & txtNonPayerLastName.Text & vbCrLf & _
         txtNonPayerTitle.Text & vbCrLf & _
         txtNonPayerCompany.Text & vbCrLf & _
         txtNonPayerAddress.Text & vbCrLf & _
         txtNonPayerCity.Text & ", " & txtNonPayerState.Text & " " & mtbNonPayerZipCode.Text & vbCrLf & _
         txtNonPayerPhoneNumber.Text & vbCrLf & _
         txtNonPayerEmail.Text


        Catch ex As Exception

        End Try
    End Sub
End Class